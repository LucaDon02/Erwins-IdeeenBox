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
using System.Xml.Linq;

namespace IdeeenBox_V2
{
    public partial class NewIdeaPage : Page
    {
        private Window _mainWindow;
        private Page _lastPage;

        public NewIdeaPage(Window mainWindow, Page lastPage)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _lastPage = lastPage;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Equals(""))
            {
                ErrorLabel.Content = "Name can't be empty";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                LoginSystem.CurrentUser.Ideas.Add(new Idea(owner: LoginSystem.CurrentUser, name: NameBox.Text, description: DescriptionBox.Text));
                SaveSystem.Save(LoginSystem.Users);
                Return(sender, e);
            }
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new MenuPage(_mainWindow);
        }
    }
}
