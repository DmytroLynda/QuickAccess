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
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    internal partial class LoginWindow : Window
    {
        private readonly IUserLoginProvider _loginProvider;
        private readonly IUserSettingsProvider _settingsProvider;
        private readonly ILoginWindowController _controller;

        public IWindowManager WindowManager { get; set; }

        public LoginWindow(IUserLoginProvider loginProvider, IUserSettingsProvider usetSettings, ILoginWindowController controller)
        {
            InitializeComponent();

            _loginProvider = loginProvider;
            _settingsProvider = usetSettings;
            _controller = controller;
        }

        #region events

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Login and password fields can't be empty.");
                return;
            }

            var user = new UserViewModel
            {
                Login = LoginTextBox.Text,
                Password = PasswordTextBox.Text
            };
            
            var userSettings = await _settingsProvider.GetUserSettingsAsync();
            if (_controller.LogIn(user, userSettings.CurrentDevice))
            {
                _loginProvider.User = user;
                await GoToMainWindowAsync();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.");
            }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Login and password fields can't be empty.");
                return;
            }

            var newUser = new UserViewModel
            {
                Login = LoginTextBox.Text,
                Password = PasswordTextBox.Text
            };

            var userSettings = await _settingsProvider.GetUserSettingsAsync();
            if (_controller.Register(newUser, userSettings.CurrentDevice))
            {
                _loginProvider.User = newUser;
                await GoToMainWindowAsync();
            }
            else
            {
                MessageBox.Show("User with this name exists.");
            }
        }

        #endregion

        private async Task GoToMainWindowAsync()
        {
            await WindowManager.ShowMainWindowAsync(this);
        }
    }
}
