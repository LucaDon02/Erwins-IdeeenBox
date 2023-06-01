using IdeeenBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;

namespace IdeeenBox_V2
{
    public partial class ForgotPasswordPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public ForgotPasswordPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var input = InputBox.Text;
            var user = LoginSystem.Users.SingleOrDefault(user => user.Name.Equals(input)) ??
                LoginSystem.Users.SingleOrDefault(user => user.Email.Equals(input));

            if (user != null)
            {
                ErrorLabel.Visibility = Visibility.Collapsed;
                ErrorLabel.Content = "";
                MessageLabel.Content = EmailSender.SendPassword(user).Result.StatusCode == HttpStatusCode.Accepted
                                           ? "An email has been sent."
                                           : "An error has occurred";
                MessageLabel.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorLabel.Content = "This email or username does not exist. Please try again.";
                ErrorLabel.Visibility = Visibility.Visible;
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
        }
    }
}
