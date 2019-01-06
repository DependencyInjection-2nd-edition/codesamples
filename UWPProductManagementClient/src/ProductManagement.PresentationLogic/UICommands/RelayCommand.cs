using System;
using System.Windows.Input;

namespace Ploeh.Samples.ProductManagement.PresentationLogic.UICommands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;

        public RelayCommand(Action execute)
        {
            this.execute = parameter => execute();
        }

        public RelayCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) => true;
        public virtual void Execute(object parameter) => this.execute(parameter);
    }
}