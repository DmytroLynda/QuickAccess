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
using ThesisProject.Internal.Interfaces;

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

            DeviceNameTextBox.Text = userSettings.CurrentDevice.Name;
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
    }
}
