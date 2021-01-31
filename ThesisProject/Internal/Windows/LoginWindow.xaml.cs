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
        private readonly ICurrentDeviceProvider _currentDevice;
        private readonly ILoginWindowController _controller;
        private readonly MainWindow _mainWindow;

        public LoginWindow(IUserLoginProvider loginProvider, ICurrentDeviceProvider currentDevice, ILoginWindowController controller, MainWindow mainWindow)
        {
            InitializeComponent();

            _loginProvider = loginProvider;
            _currentDevice = currentDevice;
            _controller = controller;
            _mainWindow = mainWindow;
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
            
            if (_controller.LogIn(user, _currentDevice.CurrentDevice))
            {
                _loginProvider.User = user;
                await GoToMainWindowAsync();
            }
            else
            {
                MessageBox.Show("Incorrect username or password.");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private async Task GoToMainWindowAsync()
        {
            await _mainWindow.StartUpdateAsync();
            _mainWindow.Show();

            this.Close();
        }
    }
}
