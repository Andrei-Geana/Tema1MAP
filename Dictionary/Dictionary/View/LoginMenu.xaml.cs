using Dictionary.ViewModel;
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
    /// Interaction logic for LoginMenu.xaml
    /// </summary>
    public partial class LoginMenu : Window
    {
        public LoginMenu()
        {
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.LoginSuccess += LoginViewModel_LoginSuccess;
            DataContext = loginViewModel;
        }

        private void LoginViewModel_LoginSuccess(object sender, EventArgs e)
        {
            // Închide fereastra curentă
            this.Close();
            AdminMenu loginMenu = new AdminMenu();
            loginMenu.Show();
        }
    }
}
