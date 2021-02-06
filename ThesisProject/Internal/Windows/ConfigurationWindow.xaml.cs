using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using ThesisProject.Internal.Helpers;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;
using ListViewItem = System.Windows.Forms.ListViewItem;

namespace ThesisProject.Internal.Windows
{
    /// <summary>
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    internal partial class ConfigurationWindow : Window
    {
        private readonly IUserSettingsProvider _settingsProvider;

        public ConfigurationWindow(IUserSettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;

            InitializeComponent();
        }   

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var userSettings = await _settingsProvider.GetUserSettingsAsync();

            SetCurrentDeviceName(userSettings.CurrentDevice.Name);

            SetBlockedDirectories(userSettings.BlockedDirectories);
        }

        private void SetCurrentDeviceName(string deviceName)
        {
            DeviceNameTextBox.Text = deviceName;
        }

        private void SetBlockedDirectories(DirectoryPathViewModel[] blockedDirectories)
        {
            BlockedDirectoriesListBox.Items.Clear();

            foreach (var blockedDirectory in blockedDirectories)
            {
                BlockedDirectoriesListBox.Items.Add(blockedDirectory.Path);
            }
        }

        private async void SaveConfigurationButtonClickAsync(object sender, RoutedEventArgs e)
        {
            var userSettings = await _settingsProvider.GetUserSettingsAsync();

            if (!string.IsNullOrEmpty(DeviceNameTextBox.Text))
            {
                userSettings.CurrentDevice.Name = DeviceNameTextBox.Text;
                await _settingsProvider.SetUserSettingsAsync(userSettings);
            }

            this.Close();
        }

        private async void AddBlockedDirectory_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var userSettings = await _settingsProvider.GetUserSettingsAsync();
                var blockedDirectories = userSettings.BlockedDirectories.ToList();

                blockedDirectories.Add(new DirectoryPathViewModel(dialog.SelectedPath));

                userSettings.BlockedDirectories = blockedDirectories.ToArray();
                await _settingsProvider.SetUserSettingsAsync(userSettings);

                SetBlockedDirectories(blockedDirectories.ToArray());
            }
        }

        private async void RemoveBlockedDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (BlockedDirectoriesListBox.SelectedItem is not null)
            {
                var selectedDirectory = new DirectoryPathViewModel(BlockedDirectoriesListBox.SelectedItem as string);

                BlockedDirectoriesListBox.Items.Remove(BlockedDirectoriesListBox.SelectedItem);

                var userSettings = await _settingsProvider.GetUserSettingsAsync();
                userSettings.BlockedDirectories = userSettings.BlockedDirectories.Except(selectedDirectory).ToArray();
                await _settingsProvider.SetUserSettingsAsync(userSettings);
            }
        }
    }
}
