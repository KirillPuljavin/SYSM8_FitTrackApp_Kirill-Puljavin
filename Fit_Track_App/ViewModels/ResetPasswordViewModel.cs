using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class ResetPasswordViewModel : ViewModelBase
    {
        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set { _newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        private string _newPasswordError;
        public string NewPasswordError
        {
            get => _newPasswordError;
            set { _newPasswordError = value; OnPropertyChanged(nameof(NewPasswordError)); }
        }

        private string _confirmPasswordError;
        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set { _confirmPasswordError = value; OnPropertyChanged(nameof(ConfirmPasswordError)); }
        }

        public ICommand ResetPasswordCommand { get; }

        public ResetPasswordViewModel()
        {
            ResetPasswordCommand = new RelayCommand(_ => ResetPassword());
        }

        private void ResetPassword()
        {
            ClearErrors();
            bool isValid = true;

            if (!Validator.ValidatePassword(NewPassword, out string newPasswordError))
            {
                NewPasswordError = newPasswordError;
                isValid = false;
            }

            if (!Validator.ValidateConfirmPassword(NewPassword, ConfirmPassword, out string confirmPasswordError))
            {
                ConfirmPasswordError = confirmPasswordError;
                isValid = false;
            }

            if (isValid)
            {
                UserViewModel.Instance.LoggedInUser.Password = NewPassword;
                MessageBox.Show("Password has been reset successfully.");
                if (Application.Current.MainWindow is MainWindow window)
                {
                    window.MainFrame.Navigate(new LoginPage());
                }
            }
        }

        private void ClearErrors()
        {
            NewPasswordError = "";
            ConfirmPasswordError = "";
        }
    }
}
