﻿using System;
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
        private readonly FileInfoWindow _fileInfoWindow;
        private readonly IUserLoginProvider _loginProvider;
        private readonly ICurrentDeviceProvider _currentDeviceProvider;

        public MainWindow(
            IMainWindowController controller,
            IMenuUpdater menuUpdater,
            IFilesContainer filesContainer,
            IDevicesContainer devicesContainer,
            FileInfoWindow fileInfoWindow,
            IUserLoginProvider loginProvider,
            ICurrentDeviceProvider currentDeviceProvider)
        {
            _controller = controller;
            _menuUpdater = menuUpdater;
            _fileInfoWindow = fileInfoWindow;
            _filesContainer = filesContainer;
            _devicesContainer = devicesContainer;
            _loginProvider = loginProvider;
            _currentDeviceProvider = currentDeviceProvider;

            _filesContainer.OpenDirectory += OnOpenDirectoryAsync;
            _filesContainer.DownloadFile += OnDownloadFileAsync;
            _filesContainer.FileInfo += OnFileInfoAsync;

            _devicesContainer.SelectDevice += OnDeviceWasSelectedAsync;

            _menuUpdater.Update += OnUpdateMenuAsync;

            InitializeComponent();
        }

        public async Task StartUpdateAsync()
        {
            _menuUpdater.Start();
            await UpdateMenuAsync();
        }

        protected async override void OnInitialized(EventArgs e)
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

        private async void OnFileInfoAsync(object sender, FilePathViewModel filePath)
        {
            var fileInfo = await _controller.GetFileInfoAsync(filePath);

            _fileInfoWindow.DataContext = fileInfo;
            _fileInfoWindow.Show();
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
            _devicesContainer.Show(await _controller.GetDevicesAsync(_loginProvider.User, _currentDeviceProvider.CurrentDevice));

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
