using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace mvvm_genomgång.MVVM
{
    public class RelayCommand : ICommand
    {
        //Fält för att hålla referenser till metoder som definerar vad som ska göras
        private Action<object> execute;
        private Func<object, bool> canExecute;
        private Func<bool> login;
        private Action register;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Func<bool> login)
        {
            this.login = login;
        }

        public RelayCommand(Action register)
        {
            this.register = register;
        }

        // bestämmer om kommandot kan köras eller inte
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        //Kör den logik som tilldelats via execute metoden
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}