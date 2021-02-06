using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Windows
{
    internal class WindowsManager : IWindowsManager
    {
        private readonly LoginWindow _loginWindow;
        private readonly MainWindow _mainWindow;
        private readonly IServiceProvider _serviceProvider;

        private ConfigurationWindow ConfigurationWindow
        {
            get
            {
                return _serviceProvider.GetRequiredService<ConfigurationWindow>();
            }
        }

        private FileInfoWindow FileInfoWindow
        {
            get
            {
                return _serviceProvider.GetRequiredService<FileInfoWindow>();
            }
        }

        public WindowsManager(
            LoginWindow loginWindow,
            MainWindow mainWindow,
            IServiceProvider serviceProvider)
        {
            _loginWindow = loginWindow;
            _mainWindow = mainWindow;
            _serviceProvider = serviceProvider;

            _loginWindow.WindowsManager = this;
            _mainWindow.WindowsManager = this;
        }

        public void ShowConfigurationWindow()
        {
            ConfigurationWindow.Show();
        }

        public void ShowFileInfoWindow(FileInfoViewModel fileInfo)
        {
            FileInfoWindow.Show(fileInfo);
        }

        public void ShowLoginWindow(Window caller)
        {
            if (caller is not null)
            { 
                caller.Visibility = Visibility.Collapsed;
            }

            _loginWindow.Show();
        }

        public async Task ShowMainWindowAsync(Window caller)
        {
            if (caller is not null)
            {
                caller.Visibility = Visibility.Collapsed;
            }

            await _mainWindow.StartUpdateAsync();
            _mainWindow.Show();
        }
    }
}
