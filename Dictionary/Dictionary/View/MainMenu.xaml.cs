using Dictionary.Model;
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

namespace Dictionary.View
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private static DatabaseEmulator databaseEmulator = new DatabaseEmulator();
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginMenu loginMenu = new LoginMenu(databaseEmulator);
            loginMenu.ShowDialog();
        }

        private void UserMenu_Click(object sender, RoutedEventArgs e)
        {
            UserMenu lWind = new UserMenu(databaseEmulator);
            lWind.ShowDialog();
        }

        private void Quiz_Click(object sender, RoutedEventArgs e)
        {
            Divertisment lWind = new Divertisment(databaseEmulator);
            lWind.ShowDialog();
        }
    }
}
