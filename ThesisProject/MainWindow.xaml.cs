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

            var fileService = _services.GetService<IFileService>();
            StartListenForDevices();
        }

        private void StartListenForDevices()
        {
            var timer = new Timer();
        }
    }
}
