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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IdeeenBox_V2
{
    public partial class EditProfilePage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public EditProfilePage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
            NameBox.Text = LoginSystem.CurrentUser.Name;
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            // ToDo: clean this
            _mainWindow.Content = new MenuPage(_mainWindow, this);
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!NameBox.Equals("") && NameBox.Text.Equals(LoginSystem.CurrentUser.Name))
            {
                ErrorLabel.Content = "The changed username cant be the same as the old username";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else if (!NameBox.Equals("") && LoginSystem.Users.Any(user => user.Name.Equals(NameBox.Text)))
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!LoginSystem.Users.Any(user => user.Name.Equals(NameBox.Text)))
            {
                LoginSystem.CurrentUser.Name = NameBox.Text;
                SaveSystem.Save(LoginSystem.Users);
                Return(sender, e);
            }
        }
    }
}
