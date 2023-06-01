using IdeeenBox;
using SendGrid;
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
    public partial class ConfirmEmailPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public ConfirmEmailPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Equals("") || !LoginSystem.Users.Any(user => user.Email.Equals(EmailBox.Text)))
            {
                ErrorLabel.Content = "This email does not exists";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                var user = LoginSystem.Users.Single(user => user.Email.Equals(EmailBox.Text));
                if (user.EmailConfirmationCode == null)
                {
                    ErrorLabel.Content = "This email is already confirmed";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    if (LoginSystem.ConfirmEmail(user, CodeBox.Text))
                    {
                        MessageLabel.Content = "Confirmation successful";
                        MessageLabel.Visibility = Visibility.Visible;
                        SaveSystem.Save(LoginSystem.Users);
                    }
                    else
                    {
                        ErrorLabel.Content = "Wrong code. Please try again";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
        }

        private void ResendCodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmailBox.Text.Equals("") || !LoginSystem.Users.Any(user => user.Email.Equals(EmailBox.Text)))
            {
                ErrorLabel.Content = "This email does not exists";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                var user = LoginSystem.Users.Single(user => user.Email.Equals(EmailBox.Text));
                if (user.EmailConfirmationCode == null)
                {
                    ErrorLabel.Content = "This email is already confirmed";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    user.ResendConfirmationCode();
                    MessageLabel.Content = "Please check your email inbox for the code";
                    MessageLabel.Visibility = Visibility.Visible;
                    SaveSystem.Save(LoginSystem.Users);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ErrorLabel.Content != "" || ErrorLabel.Visibility == Visibility.Visible)
            {
                ErrorLabel.Visibility = Visibility.Collapsed;
                ErrorLabel.Content = "";
            }

            if (MessageLabel.Content == "Please check your email inbox for the code")
            {
                MessageLabel.Visibility = Visibility.Collapsed;
                MessageLabel.Content = "";
            }
        }
    }
}
