using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ClientLogic;
using ClientLogic.DataTypes;
using Microsoft.Extensions.Options;
using ThesisProject.Internal;

namespace ThesisProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IFileService _fileService;
        private readonly IDeviceService _deviceService;
        private readonly MainWindowOptions _options;

        public MainWindow(IFileService fileService, IDeviceService deviceService, IOptions<MainWindowOptions> options)
        {
            InitializeComponent();

            _fileService = fileService;
            _deviceService = deviceService;
            _options = options.Value;

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
            //TODO: Show existing devices for a user.
            DevicePanel.Children.Clear();
            foreach (var device in devices)
            {
                DevicePanel.Children.Add(MakeDevice(device));
            }
        }

        private UIElement MakeDevice(DeviceDTO device)
        {
            var button = new Button();
            button.Width = 50;
            button.Height = 50;
            button.Content = device.Name;
            button.FontSize = 8;

            return button;
        }

        private async void ConfigurationButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            await _fileService.DownloadFileAsync(new DomainEntities.Device { Id = 1, Name = "Dima" }, new DomainEntities.FilePath(@"C:\Users\Dmytro\Desktop\SSS.txt"));
        }
    }
}
