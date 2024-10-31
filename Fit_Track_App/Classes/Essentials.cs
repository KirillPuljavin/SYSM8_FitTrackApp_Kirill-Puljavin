using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Fit_Track_App.Classes
{
    public class RelayCommand : ICommand
    {
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

        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
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

    internal static class Validator
    {
        public static bool ValidateUserName(string userName, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 3 ||
                !Regex.IsMatch(userName, @"^[a-zA-Z0-9_\-!]{3,}$"))
            {
                errorMessage = "Username must be at least 3 characters and can contain letters, numbers, and symbols: _ - !";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public static bool ValidateEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Please enter a valid email address.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }

        public static bool ValidatePassword(string password, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 ||
                !Regex.IsMatch(password, @"^(?=.*[0-9])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
            {
                errorMessage = "Lösenordet måste uppfylla särskilda krav (minst 8 tecken, minst en siffra och ett specialtecken).";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }


        public static bool ValidateConfirmPassword(string password, string confirmPassword, out string errorMessage)
        {
            if (password != confirmPassword)
            {
                errorMessage = "Passwords do not match.";
                return false;
            }
            errorMessage = string.Empty;
            return true;
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;

            if (parameter != null && parameter.ToString() == "Invert")
                boolValue = !boolValue;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            bool result = visibility == Visibility.Visible;

            if (parameter != null && parameter.ToString() == "Invert")
                result = !result;

            return result;
        }
    }
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
