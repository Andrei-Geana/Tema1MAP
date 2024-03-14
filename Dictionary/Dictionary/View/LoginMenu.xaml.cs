using Dictionary.Model;
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
        private readonly DatabaseEmulator emulator;
        public LoginMenu(object dContext)
        {
            InitializeComponent();
            emulator = dContext as DatabaseEmulator;
            LoginViewModel loginViewModel = new LoginViewModel(emulator);
            loginViewModel.LoginSuccess += LoginViewModel_LoginSuccess;
            DataContext = loginViewModel;
        }

        private void LoginViewModel_LoginSuccess(object sender, EventArgs e)
        {
            this.Close();
            AdminMenu loginMenu = new AdminMenu(emulator);
            loginMenu.ShowDialog();
        }
    }
}
