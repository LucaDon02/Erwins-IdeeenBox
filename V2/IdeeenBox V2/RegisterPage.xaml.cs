using IdeeenBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace IdeeenBox_V2
{
    public partial class RegisterPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public RegisterPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        /*private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ErrorLabel.Content.Equals("This username already exists"))
            {
                if (!EmailBox.Text.Equals("") && !LoginSystem.Users.Any(user => user.Email.Equals(EmailBox.Text)))
                {
                    ErrorLabel.Content = "This email already exists";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                }
            }
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var name = ((TextBox)sender).Text;
            if (!name.Equals("") && LoginSystem.Users.Any(user => user.Name.Equals(name)))
            {
                ErrorLabel.Content = "This username already exists";
                ErrorLabel.Visibility = Visibility.Visible;
            } else
            {
                if (!EmailBox.Text.Equals("") && !LoginSystem.Users.Any(user => user.Email.Equals(EmailBox.Text)))
                {
                    ErrorLabel.Content = "This email already exists";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                }
            }
        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ErrorLabel.Content.Equals("This email already exists"))
            {
                if (!NameBox.Equals("") && !LoginSystem.Users.Any(user => user.Name.Equals(NameBox.Text)))
                {
                    ErrorLabel.Content = "This username already exists";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                }
            }
        }

        private void EmailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var email = ((TextBox)sender).Text;
            if (!email.Equals("") && !LoginSystem.Users.Any(user => user.Email.Equals(email)))
            {
                ErrorLabel.Content = "This email already exists";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (!NameBox.Equals("") && !LoginSystem.Users.Any(user => user.Name.Equals(NameBox.Text)))
                {
                    ErrorLabel.Content = "This username already exists";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                }
            }
        }*/

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ToDo: Fix
            if (!NameBox.Equals("") && LoginSystem.Users.Any(user => user.Name.Equals(NameBox.Text)))
            {
                ErrorLabel.Content = "This username already exists";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorLabel.Visibility = Visibility.Collapsed;
                ErrorLabel.Content = "";
            }
        }

        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(EmailBox.Text, emailRegex))
            {
                ErrorLabel.Content = "This email is invalid";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                // ToDo: Fix
                if (!EmailBox.Text.Equals("") && LoginSystem.Users.Any(user => user.Email.Equals(EmailBox.Text)))
                {
                    ErrorLabel.Content = "This email already exists";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                }
            }
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

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Equals(""))
            {
                ErrorLabel.Content = "Password can't be empty";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (Regex.IsMatch(EmailBox.Text, emailRegex) && !LoginSystem.Users.Any(user => user.Name.Equals(NameBox.Text)) && !LoginSystem.Users.Any(user => user.Email.Equals(EmailBox.Text)))
                {
                    LoginSystem.Register(NameBox.Text, EmailBox.Text, PasswordBox.Password);
                    Return(sender, e);
                    // ToDo: Screen for email confirmation sent
                }
            }
        }
    }
}
