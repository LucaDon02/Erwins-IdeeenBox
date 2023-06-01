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
    public partial class MenuPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public MenuPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            WelcomeLabel.Content = "Welcome " + LoginSystem.CurrentUser.Name + "!";
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
            LoginSystem.CurrentUser = null;
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new EditProfilePage(_mainWindow, this);
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new ChangePasswordPage(_mainWindow, this);
        }

        private void SaveIdeaButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new NewIdeaPage(_mainWindow, this);
        }

        private void ReadIdeasButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new IdeasPage(_mainWindow, this);
        }
    }
}
