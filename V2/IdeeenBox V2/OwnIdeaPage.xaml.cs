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
    public partial class OwnIdeaPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;
        private Idea _idea;

        public OwnIdeaPage(Window mainWindow, Page lastPage, Idea idea)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
            _idea = idea;

            Refresh();
        }

        public void Refresh()
        {
            NameBox.Text = _idea.Name;
            DescriptionBox.Text = _idea.Description;
            AddEmailBox.Text = "";

            var sharedEmails = new List<String>();
            var counter = 1;
            foreach (var email in _idea.SharedWith.Select(i => i.Email)) sharedEmails.Add(counter + ". " + email);
            foreach (var email in _idea.SharedWithEmail) sharedEmails.Add(counter + ". " + email + " (not registered yet)");

            SharedWithList.ItemsSource = sharedEmails;
        }

        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text;

            if (name == null || name.Equals(""))
            {
                ErrorLabel.Content = "Please enter a valid name";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                _idea.Name = name;
                SaveSystem.Save(LoginSystem.Users);

                Return(sender, e);
            }
        }

        private void EditDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            var description = DescriptionBox.Text;

            if (description == null || description.Equals(""))
            {
                ErrorLabel.Content = "Please enter a valid description";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                _idea.Description = description;
                SaveSystem.Save(LoginSystem.Users);

                Return(sender, e);
            }
        }

        private void AddEmailButton_Click(object sender, RoutedEventArgs e)
        {
            var email = AddEmailBox.Text;

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (email == null || email.Equals("") || !Regex.IsMatch(email, emailRegex))
            {
                ErrorLabel.Content = "Please enter a valid E-mail";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else if (email.Equals(LoginSystem.CurrentUser.Email))
            {
                ErrorLabel.Content = "You cannot share an idea with yourself";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                var user = LoginSystem.Users.FirstOrDefault(u => u.Email.Equals(email));

                if (user != null) {
                    _idea.SharedWith.Add(user);
                    user.SharedIdeas.Add(_idea);
                    EmailSender.SendIdeaSharingEmail(LoginSystem.CurrentUser, user, _idea);
                }
                else
                {
                    _idea.SharedWithEmail.Add(email);
                    EmailSender.SendRegistrationRequest(LoginSystem.CurrentUser.Name, email);

                    ErrorLabel.Content = "User does not exist yet. We have sent a registration invite";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
                SaveSystem.Save(LoginSystem.Users);
                Refresh();
            }
        }

        private void DeleteShareButton_Click(object sender, RoutedEventArgs e)
        {
            if (SharedWithList.SelectedItem == null) {
                ErrorLabel.Content = "Please select an E-mail first";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else {
                string pattern = @"^(\d+)";

                var match = Regex.Match(SharedWithList.SelectedItem.ToString(), pattern);

                if (match.Success)
                {
                    if (Int32.TryParse(match.Groups[1].Value, out int j))
                    {
                        if (j > _idea.SharedWith.Count) _idea.SharedWithEmail.RemoveAt(j - _idea.SharedWith.Count);
                        else
                        {
                            _idea.SharedWith[j - 1].SharedIdeas.Remove(_idea);
                            _idea.SharedWith.RemoveAt(j - 1);
                        }
                        SaveSystem.Save(LoginSystem.Users);
                        Refresh();
                    }
                    else
                    {
                        ErrorLabel.Content = "Something went wrong.";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var confirmationWindow = new DeleteIdeaConfirmationWindow();
            confirmationWindow.Owner = _mainWindow;
            confirmationWindow.ShowDialog();

            if (confirmationWindow.IsConfirmed)
            {
                foreach (var user in _idea.SharedWith) user.SharedIdeas.Remove(_idea);
                LoginSystem.CurrentUser.Ideas.Remove(_idea);
                SaveSystem.Save(LoginSystem.Users);

                Return(sender, e);
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
            ((IdeasPage)_lastPage).Refresh();
        }
    }
}
