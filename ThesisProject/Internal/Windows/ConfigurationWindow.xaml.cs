using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Windows
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    internal partial class ConfigurationWindow : Window
    {
        private readonly IConfigurationWindowController _controller;

        public ConfigurationWindow(IConfigurationWindowController controller)
        {
            _controller = controller;

            InitializeComponent();
        }   

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var userSettings = await _controller.GetUserSettingsAsync();

            SetCurrentDeviceName(userSettings.CurrentDevice.Name);

            SetBlockedDirectories(userSettings.BlockedDirectories);
        }

        private void SetCurrentDeviceName(string deviceName)
        {
            DeviceNameTextBox.Text = deviceName;
        }

        private void SetBlockedDirectories(List<DirectoryPathViewModel> blockedDirectories)
        {
            BlockedDirectoriesListBox.Items.Clear();

            foreach (var blockedDirectory in blockedDirectories)
            {
                BlockedDirectoriesListBox.Items.Add(blockedDirectory.Path);
            }
        }

        private async void SaveConfigurationButtonClickAsync(object sender, RoutedEventArgs e)
        {
            var userSettings = await _controller.GetUserSettingsAsync();

            if (!string.IsNullOrEmpty(DeviceNameTextBox.Text))
            {
                userSettings.CurrentDevice.Name = DeviceNameTextBox.Text;
            }

            if (BlockedDirectoriesListBox.Items.Count > 0)
            {
                var blockedDirectories = new List<DirectoryPathViewModel>();
                foreach (var directory in BlockedDirectoriesListBox.Items)
                {
                    blockedDirectories.Add(new DirectoryPathViewModel(directory.ToString()));
                }

                userSettings.BlockedDirectories = blockedDirectories;
            }

            await _controller.SetUserSettingsAsync(userSettings);

            this.Close();
        }

        private void AddBlockedDirectory_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                BlockedDirectoriesListBox.Items.Add(dialog.SelectedPath);
            }
        }

        private void RemoveBlockedDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (BlockedDirectoriesListBox.SelectedItem is not null)
            {
                BlockedDirectoriesListBox.Items.Remove(BlockedDirectoriesListBox.SelectedItem);
            }
        }
    }
}
