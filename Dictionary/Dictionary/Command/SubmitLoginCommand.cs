using Dictionary.Model;
using Dictionary.View;
using Dictionary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dictionary.Command
{
    public class SubmitLoginCommand : CommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        public SubmitLoginCommand(LoginViewModel loginModel)
        {
            _loginViewModel = loginModel;
        }

        public override void Execute(object parameter)
        {
            if (CanExecuteLogin(parameter))
                ExecuteLogin(parameter);
            else 
            {
                MessageBox.Show("Invalid username or password", "Login Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            return;
        }

        private bool CanExecuteLogin(object parameter)
        {
            return _loginViewModel.CanExecuteLogin;
        }

        private void ExecuteLogin(object parameter)
        {
            MessageBox.Show("You are now logged in!", "Login success", MessageBoxButton.OK, MessageBoxImage.Information); 
            _loginViewModel.OnLoginSuccess();
        }
    }
}
