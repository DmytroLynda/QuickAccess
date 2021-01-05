using System;
using System.Threading.Tasks;
using System.Windows;
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

        private readonly FileInfoWindow _fileInfoWindow;

        public MainWindow(IMainWindowController controller, IMenuUpdater menuUpdater, IFilesContainer filesContainer, IDevicesContainer devicesContainer, FileInfoWindow fileInfoWindow)
        {
            _controller = controller;
            _menuUpdater = menuUpdater;
            _fileInfoWindow = fileInfoWindow;

            _filesContainer = filesContainer;
            _filesContainer.OpenDirectory += OnOpenDirectoryAsync;
            _filesContainer.DownloadFile += OnDownloadFileAsync;
            _filesContainer.FileInfo += OnFileInfoAsync;

            _devicesContainer = devicesContainer;
            _devicesContainer.SelectDevice += OnDeviceWasSelectedAsync;

            _menuUpdater.Update += OnUpdateMenuAsync;

            InitializeComponent();
        }

        private async void OnFileInfoAsync(object sender, FilePathViewModel filePath)
        {
            var fileInfo = await _controller.GetFileInfoAsync(filePath);

            _fileInfoWindow.DataContext = fileInfo;
            _fileInfoWindow.Show();
        }

        private async void OnDownloadFileAsync(object sender, FilePathViewModel filePath)
        {
            await _controller.DownloadFileAsync(filePath);
        }

        protected async override void OnInitialized(EventArgs e)
        {
            _filesContainer.Initialize(FilesPanel.Children);
            _devicesContainer.Initialize(DevicesPanel.Children);
            _menuUpdater.Start();

            await UpdateMenuAsync();

            base.OnInitialized(e);
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
            _devicesContainer.Show(await _controller.GetDevicesAsync());

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
    }
}
