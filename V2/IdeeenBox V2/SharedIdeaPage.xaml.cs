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

namespace IdeeenBox_V2
{
    public partial class SharedIdeaPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;
        private Idea _idea;

        public SharedIdeaPage(Window mainWindow, Page lastPage, Idea idea)
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
        }

        private void UnfollowButton_Click(object sender, RoutedEventArgs e)
        {

            var confirmationWindow = new DeleteIdeaConfirmationWindow("Are you sure you want to unfollow this idea?");
            confirmationWindow.Owner = _mainWindow;
            confirmationWindow.ShowDialog();

            if (confirmationWindow.IsConfirmed)
            {
                _idea.SharedWith.Remove(LoginSystem.CurrentUser);
                LoginSystem.CurrentUser.SharedIdeas.Remove(_idea);
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
