using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Thermostat
{
    /// <summary>
    /// Wraps an <see cref="Action"/> as an ICommand.
    /// Actions in C# are basically the same as function pointers in C, but they look much nicer.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action Action;
        private readonly Func<bool> CanExecuteFunc;

        public DelegateCommand(Action action, Func<bool> canExecute = null)
        {
            Action = action;
            CanExecuteFunc = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }



        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            Action.Invoke();
        }
    }


    /// <summary>
    /// Fancier version of <see cref="DelegateCommand"/> that adds a single parameter to the function that is run.
    /// </summary>
    public class DelegateCommand<TParameters> : ICommand
    {
        private readonly Action<TParameters> _Action;
        private readonly Func<TParameters, bool> _CanExecute;

        public DelegateCommand(Action<TParameters> action, Func<TParameters, bool> canExecute)
        {
            _Action = action;
            _CanExecute = canExecute;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }



        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is TParameters)
            {
                return _CanExecute.Invoke((TParameters)parameter);
            }
            else
            {
                throw new ArgumentException("Expected parameters to be of type: " + typeof(TParameters).Name);
            }
        }

        public void Execute(object parameter)
        {
            if (parameter is TParameters)
            {
                _Action.Invoke((TParameters)parameter);
            }
            else
            {
                throw new ArgumentException("Expected parameters to be of type: " + typeof(TParameters).Name);
            }
        }
    }
}
