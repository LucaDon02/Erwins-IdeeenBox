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
    public partial class IdeasPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public IdeasPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;

            Refresh();
        }

        public void Refresh()
        {
            var ownIdeas = new List<String>();
            var counter = 1;
            foreach (var idea in LoginSystem.CurrentUser.Ideas.Select(i => i.Name)) ownIdeas.Add(counter + ". " + idea);

            var sharedIdeas = new List<String>();
            counter = 1;
            foreach (var idea in LoginSystem.CurrentUser.SharedIdeas) sharedIdeas.Add(counter + ". " + idea.Owner.Name + " - " + idea.Name);

            OwnIdeasList.ItemsSource = ownIdeas;
            SharedIdeasList.ItemsSource = sharedIdeas;
        }

        private void OwnIdeasList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OwnIdeasList.SelectedItem == null) return;

            string pattern = @"^(\d+)";
            var match = Regex.Match(OwnIdeasList.SelectedItem.ToString(), pattern);

            if (match.Success)
            {
                if (Int32.TryParse(match.Groups[1].Value, out int j))
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                    OwnIdeasList.SelectedItem = null;

                    _mainWindow.Content = new OwnIdeaPage(_mainWindow, this, LoginSystem.CurrentUser.Ideas[j - 1]);
                }
                else
                {
                    ErrorLabel.Content = "Something went wrong.";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
            }
        }

        private void SharedIdeasList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SharedIdeasList.SelectedItem == null) return;

            string pattern = @"^(\d+)";
            var match = Regex.Match(SharedIdeasList.SelectedItem.ToString(), pattern);

            if (match.Success)
            {
                if (Int32.TryParse(match.Groups[1].Value, out int j))
                {
                    ErrorLabel.Visibility = Visibility.Collapsed;
                    ErrorLabel.Content = "";
                    SharedIdeasList.SelectedItem = null;

                    _mainWindow.Content = new SharedIdeaPage(_mainWindow, this, LoginSystem.CurrentUser.SharedIdeas[j - 1]);
                }
                else
                {
                    ErrorLabel.Content = "Something went wrong.";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = _lastPage;
        }
    }
}
