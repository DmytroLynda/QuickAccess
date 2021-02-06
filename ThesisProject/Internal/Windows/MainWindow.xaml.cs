using System;
using System.Threading.Tasks;
using System.Windows;
using ThesisProject.Internal.Helpers;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        private readonly IMainWindowController _controller;
        private readonly IMenuUpdater _menuUpdater;
        private readonly IFilesContainer _filesContainer;
        private readonly IDevicesContainer _devicesContainer;
        private readonly IUserLoginProvider _loginProvider;

        public IWindowManager WindowManager { get; set; }

        public MainWindow(
            IMainWindowController controller,
            IMenuUpdater menuUpdater,
            IFilesContainer filesContainer,
            IDevicesContainer devicesContainer,
            IUserLoginProvider loginProvider)
        {
            _controller = controller;
            _menuUpdater = menuUpdater;
            _filesContainer = filesContainer;
            _devicesContainer = devicesContainer;
            _loginProvider = loginProvider;

            _filesContainer.OpenDirectory += OnOpenDirectoryAsync;
            _filesContainer.DownloadFile += OnDownloadFileAsync;
            _filesContainer.FileInfo += OnFileInfoAsync;

            _devicesContainer.SelectDevice += OnDeviceWasSelectedAsync;

            _menuUpdater.AddUpdater(OnUpdateMenuAsync);

            InitializeComponent();
        }

        public async Task StartUpdateAsync()
        {
            _menuUpdater.Start();
            await UpdateMenuAsync();
        }

        protected override void OnInitialized(EventArgs e)
        {
            BackButton.Click += OnBackButtonClickAsync;

            _filesContainer.Initialize(FilesPanel.Children);
            _devicesContainer.Initialize(DevicesPanel.Children);

            base.OnInitialized(e);
        }

        private async void OnBackButtonClickAsync(object sender, RoutedEventArgs e)
        {
            var previousDirectory = _filesContainer.CurentDirectory.Back();
            _filesContainer.CurentDirectory = previousDirectory;

            await UpdateFilesMenuAsync(_filesContainer.CurentDevice, _filesContainer.CurentDirectory);
        }

        private void OnLogOutButtonClick(object sender, RoutedEventArgs e)
        {
            _menuUpdater.Stop();
            _loginProvider.User.Password = null;

            WindowManager.ShowLoginWindow(this);
        }

        private async void OnFileInfoAsync(object sender, FilePathViewModel filePath)
        {
            var fileInfo = await _controller.GetFileInfoAsync(filePath);

            WindowManager.ShowFileInfoWindow(fileInfo);
        }

        private async void OnDownloadFileAsync(object sender, FilePathViewModel filePath)
        {
            await _controller.DownloadFileAsync(_devicesContainer.GetSelectedDevice(), filePath);
        }

        private async void OnOpenDirectoryAsync(object sender, DirectoryPathViewModel directory)
        {
            await UpdateFilesMenuAsync(_devicesContainer.GetSelectedDevice(), directory);
        }

        private async void OnDeviceWasSelectedAsync(object sender, DeviceViewModel device)
        {
            await UpdateFilesMenuAsync(device);
        }

        private async void OnUpdateMenuAsync(object sender, EventArgs e)
        {
            await UpdateMenuAsync();
        }

        private async Task UpdateMenuAsync()
        {
            UserNameLabel.Content = _loginProvider.User.Login;

            var userSettings = await _controller.GetUserSettingsAsync();
            _devicesContainer.Show(await _controller.GetDevicesAsync(_loginProvider.User, userSettings.CurrentDevice));

            if (_devicesContainer.IsSelectedDevice())
            {
                await UpdateFilesMenuAsync(_devicesContainer.GetSelectedDevice());
            }
        }

        private async Task UpdateFilesMenuAsync(DeviceViewModel device)
        {
            DirectoryPathViewModel directory;
            if (device == _filesContainer.CurentDevice)
            {
                directory = _filesContainer.CurentDirectory;
            }
            else
            {
                directory = DirectoryPathViewModel.Root;
            }
            
            await UpdateFilesMenuAsync(device, directory);
        }

        private async Task UpdateFilesMenuAsync(DeviceViewModel device, DirectoryPathViewModel directory)
        {
            _filesContainer.CurentDevice = device;
            _filesContainer.CurentDirectory = directory;
            _filesContainer.Show(await _controller.GetDirectoryAsync(directory, device));
        }

        private void ConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.ShowConfigurationWindow();
        }
    }
}
