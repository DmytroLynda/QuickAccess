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

        public MainWindow(IMainWindowController controller, IMenuUpdater menuUpdater, IFilesContainer filesContainer, IDevicesContainer devicesContainer)
        {
            _controller = controller;
            _menuUpdater = menuUpdater;

            _filesContainer = filesContainer;

            _devicesContainer = devicesContainer;
            _devicesContainer.DeviceWasSelected += OnDeviceWasSelectedAsync;

            _menuUpdater.Update += OnUpdateMenuAsync;

            InitializeComponent();
        }

        protected async override void OnInitialized(EventArgs e)
        {
            _filesContainer.Initialize(FilesPanel.Children);
            _devicesContainer.Initialize(DevicesPanel.Children);
            _menuUpdater.Start();

            await UpdateMenuAsync();
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
            if (device == _filesContainer.Device)
            {
                directory = _filesContainer.Directory;
            }
            else
            {
                directory = DirectoryPathViewModel.Root;
            }
            
            _filesContainer.Device = device;
            _filesContainer.Directory = directory;
            _filesContainer.Show(await _controller.GetDirectoryAsync(directory, device));
        }
    }
}
