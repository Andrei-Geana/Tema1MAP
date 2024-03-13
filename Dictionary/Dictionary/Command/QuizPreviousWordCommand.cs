using Dictionary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Command
{
    public class QuizPreviousWordCommand : CommandBase
    {
        private readonly DivertismentViewModel _divertismentViewModel;
        public QuizPreviousWordCommand(DivertismentViewModel loginModel)
        {
            _divertismentViewModel = loginModel;
        }

        public override void Execute(object parameter)
        {
            if (CanExecuteNextWorkCommand(parameter))
                ExecuteNextWorkCommand(parameter);
        }

        private bool CanExecuteNextWorkCommand(object parameter)
        {
            return true;
        }

        private void ExecuteNextWorkCommand(object parameter)
        {
            _divertismentViewModel.ExecutePreviousWordCommand();
        }
    }
}
