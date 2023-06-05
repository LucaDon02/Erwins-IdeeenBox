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

namespace IdeeenBox_V2
{
    public partial class MainPage : Page
    {
        private Window _mainWindow;
        public MainPage(Window mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        public void ShowMessage()
        {
            MessageLabel.Visibility = Visibility.Visible;
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Collapsed;
            _mainWindow.Content = new LoginPage(_mainWindow, this);
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Collapsed;
            _mainWindow.Content = new RegisterPage(_mainWindow, this);
        }

        private void ConfirmEmail(object sender, RoutedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Collapsed;
            _mainWindow.Content = new ConfirmEmailPage(_mainWindow, this);
        }

        private void ForgotPassword(object sender, RoutedEventArgs e)
        {
            MessageLabel.Visibility = Visibility.Collapsed;
            _mainWindow.Content = new ForgotPasswordPage(_mainWindow, this );
        }
    }
}
