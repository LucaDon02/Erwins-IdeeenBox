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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace IdeeenBox_V2
{
    public partial class ChangePasswordPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;
        public ChangePasswordPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        private void OldPasswordBoxShown_TextChanged(object sender, TextChangedEventArgs e)
        {
            OldPasswordBox.Password = OldPasswordBoxShown.Text;
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            OldPasswordBoxShown.Text = OldPasswordBox.Password;
            OldPasswordBox.Visibility = Visibility.Hidden;
            OldPasswordBoxShown.Visibility = Visibility.Visible;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            OldPasswordBoxShown.Visibility = Visibility.Hidden;
            OldPasswordBox.Visibility = Visibility.Visible;
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewPasswordBox.Password.Equals(""))
            {
                ErrorLabel.Content = "The new password can't be empty";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (!LoginSystem.CurrentUser.ComparePassword(OldPasswordBox.Password))
                {
                    ErrorLabel.Content = "The old password is invalid";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    if (OldPasswordBox.Password.Equals(NewPasswordBox.Password))
                    {
                        ErrorLabel.Content = "The new password can't be the same as the old password";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (!NewPasswordBox.Password.Equals(RepeatPasswordBox.Password))
                        {
                            ErrorLabel.Content = "The new passwords don't match";
                            ErrorLabel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            ErrorLabel.Content = "";
                            ErrorLabel.Visibility = Visibility.Collapsed;
                            LoginSystem.CurrentUser.SetPassword(OldPasswordBox.Password, NewPasswordBox.Password);
                            SaveSystem.Save(LoginSystem.Users);
                            Return(sender, e);
                        }
                    }
                }
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new MenuPage(_mainWindow, this);
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewPasswordBox.Password.Equals(""))
            {
                ErrorLabel.Content = "The new password can't be empty";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (!LoginSystem.CurrentUser.ComparePassword(OldPasswordBox.Password))
                {
                    ErrorLabel.Content = "The old password is invalid";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    if (OldPasswordBox.Password.Equals(NewPasswordBox.Password))
                    {
                        ErrorLabel.Content = "The new password can't be the same as the old password";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        if (!NewPasswordBox.Password.Equals(RepeatPasswordBox.Password))
                        {
                            ErrorLabel.Content = "The new passwords don't match";
                            ErrorLabel.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            ErrorLabel.Content = "";
                            ErrorLabel.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }
    }
}
