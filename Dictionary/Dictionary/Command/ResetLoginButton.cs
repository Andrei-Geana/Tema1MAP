using Dictionary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dictionary.Command
{
    public class ResetLoginButton : CommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        public ResetLoginButton(LoginViewModel loginModel)
        {
            _loginViewModel = loginModel;
        }

        public override void Execute(object parameter)
        {
            _loginViewModel.Username = "";
            _loginViewModel.Password = "";
        }
    }
}
