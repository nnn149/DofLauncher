using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonsterModify
{
    public class RelayCommand : ICommand
    {
        public Action execute;

        public RelayCommand(Action action)
        {
            execute = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (execute != null)
            {
                execute();
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}