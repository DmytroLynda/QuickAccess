using System;
using System.Timers;
using System.Windows;
using ClientLogic;
using Microsoft.Extensions.DependencyInjection;

namespace ThesisProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _services;

        public MainWindow(IServiceProvider services)
        {
            InitializeComponent();

            _services = services;

            StartListenForDevices();
        }

        private void StartListenForDevices()
        {
            var timer = new Timer();
        }

        private async void ConfigurationButton_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var fileService = _services.GetRequiredService<IFileService>();
            await fileService.DownloadFileAsync(new DomainEntities.Device { Id = 1, Name = "Dima" }, new DomainEntities.FilePath(@"C:\Users\Dmytro\Desktop\SSS.txt"));
        }
    }
}
