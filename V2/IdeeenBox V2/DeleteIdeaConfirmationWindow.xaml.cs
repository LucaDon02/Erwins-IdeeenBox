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
    public partial class DeleteIdeaConfirmationWindow : Window
    {
        public bool IsConfirmed { get; private set; }
        private Window _mainWindow;
        public DeleteIdeaConfirmationWindow(Window mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;

            this.Icon = mainWindow.Icon;
            Topmost = true;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public DeleteIdeaConfirmationWindow(Window mainWindow, string label)
        {
            InitializeComponent();

            _mainWindow = mainWindow;

            this.Icon = mainWindow.Icon;
            Topmost = true;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Label.Text = label;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            Close();
        }
    }
}
