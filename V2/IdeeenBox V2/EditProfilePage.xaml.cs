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

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text;

            if (name.Equals(""))
            {
                ErrorLabel.Content = "The username cant be empty";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else if (name.Equals(LoginSystem.CurrentUser.Name))
            {
                ErrorLabel.Content = "The changed username cant be the same as the old username";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else if (LoginSystem.Users.Any(user => user.Name.Equals(name)))
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
            var name = NameBox.Text;

            if (!name.Equals("") &&
                !name.Equals(LoginSystem.CurrentUser.Name) &&
                !LoginSystem.Users.Any(user => user.Name.Equals(name)))
            {
                LoginSystem.CurrentUser.Name = name;
                SaveSystem.Save(LoginSystem.Users);
                Return(sender, e);
            }
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmationWindow = new DeleteIdeaConfirmationWindow("Are you sure you want to delete your account?");
            confirmationWindow.Owner = _mainWindow;
            confirmationWindow.ShowDialog();

            if (confirmationWindow.IsConfirmed)
            {
                var currentUser = LoginSystem.CurrentUser;

                foreach (var user in LoginSystem.Users)
                    foreach (var idea in user.Ideas)
                        if (idea.SharedWith.Contains(currentUser))
                            idea.SharedWith.Remove(currentUser);

                LoginSystem.Users.Remove(currentUser);

                SaveSystem.Save(LoginSystem.Users);

                _mainWindow.Content = new MainPage(_mainWindow);
                LoginSystem.CurrentUser = null;
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
        }
    }
}
