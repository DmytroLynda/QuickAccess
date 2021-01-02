using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ClientLogic;
using ClientLogic.DataTypes;
using Microsoft.Extensions.Options;
using ThesisProject.Internal;
using ThesisProject.Internal.Interfaces;

namespace ThesisProject.Internal.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow : Window
    {
        private readonly IFileService _fileService;
        private readonly IDeviceService _deviceService;
        private readonly IDevicesContainer _devicesContainer;
        private readonly MainWindowOptions _options;

        public MainWindow(
            IFileService fileService,
            IDeviceService deviceService, 
            IDevicesContainer devicesContainer,
            IOptions<MainWindowOptions> options)
        {
            InitializeComponent();

            _fileService = fileService;
            _deviceService = deviceService;
            _devicesContainer = devicesContainer;
            _options = options.Value;

            _devicesContainer.Initialize(DevicePanel.Children);
            StartListenForDevices();
        }

        private void StartListenForDevices()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(_options.RefreshPeriod);
            timer.Start();
            timer.Tick += UpdateDevicesAsync;
        }

        private async void UpdateDevicesAsync(object sender, EventArgs e)
        {
            var devices = await _deviceService.GetDevices();

            _devicesContainer.Show(devices);
        }

        private async void ConfigurationButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            await _fileService.DownloadFileAsync(new DomainEntities.Device { Id = 1, Name = "Dima" }, new DomainEntities.FilePath(@"C:\Users\Dmytro\Desktop\SSS.txt"));
        }
    }
}
