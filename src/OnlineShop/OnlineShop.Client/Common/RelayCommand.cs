using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineShop.Client.Common
{
    class RelayCommand<TAction, TPredicate>  : ICommand 
        where TAction : class
        where TPredicate :class
    {
        Action<TAction> execute;
        Predicate<TPredicate> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public RelayCommand(Action<TAction> execute, Predicate<TPredicate> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<TAction> execute) : this(execute, null)
        {

        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute.Invoke(parameter as TPredicate);
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter as TAction);
        }
    }
}
