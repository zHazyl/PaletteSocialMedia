using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialNetwork.BUS.Commands
{
    public class LikeCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private Action<object> _execute;

        public LikeCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
