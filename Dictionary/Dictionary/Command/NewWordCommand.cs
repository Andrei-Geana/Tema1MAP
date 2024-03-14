using Dictionary.Model;
using Dictionary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Command
{
    public class NewWordCommand : CommandBase
    {
        private AdminMenuViewModel _adminMenuViewModel;

        public NewWordCommand(AdminMenuViewModel adminMenuViewModel)
        {
            _adminMenuViewModel = adminMenuViewModel;
        }

        public override void Execute(object parameter)
        {
            _adminMenuViewModel.NewWord();
        }
    }
}
