using IdeeenBox;
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

namespace IdeeenBox_V2
{
    public partial class LoginPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public LoginPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text == "" || PasswordBox.Password == "" || !LoginSystem.Login(EmailBox.Text, PasswordBox.Password))
            {
                ErrorLabel.Content = "Wrong email and/or password or the email isn't confirmed.";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else _mainWindow.Content = new MenuPage(_mainWindow, _lastPage);
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
        }

        private void PasswordBoxShown_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordBox.Password = PasswordBoxShown.Text;
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            PasswordBoxShown.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Hidden;
            PasswordBoxShown.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBoxShown.Visibility = Visibility.Hidden;
            PasswordBox.Visibility = Visibility.Visible;
        }
    }
}
