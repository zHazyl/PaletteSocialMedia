using System;
using System.Windows.Input;

namespace SocialNetwork.BUS.Commands
{
    public abstract class BaseAbstractCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);
    }
}
