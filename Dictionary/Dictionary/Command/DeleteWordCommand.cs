using Dictionary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Command
{
    public class DeleteWordCommand : CommandBase
    {
        private AdminMenuViewModel _adminMenuViewModel;

        public DeleteWordCommand(AdminMenuViewModel adminMenuViewModel)
        {
            _adminMenuViewModel = adminMenuViewModel;
        }

        public override void Execute(object parameter)
        {
            _adminMenuViewModel.DeleteWord();
        }
    }
}
