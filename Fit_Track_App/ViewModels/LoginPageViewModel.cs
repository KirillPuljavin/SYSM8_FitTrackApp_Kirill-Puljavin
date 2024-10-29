using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using Fit_Track_App.Services;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        private readonly UserService _userService;

        public ICommand LoginCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand Verify2FACommand { get; }

        public LoginPageViewModel()
        {
            _userService = new UserService();

            LoginCommand = new RelayCommand(OnLogin);
            BackCommand = new RelayCommand(OnBack);
            ForgotPasswordCommand = new RelayCommand(OnForgotPassword);
            Verify2FACommand = new RelayCommand(OnVerify2FA);

            Is2FAMode = false;
        }

        // Properties for Login Fields
        public string UserName
        {
            get => UserViewModel.Instance.UserName;
            set
            {
                UserViewModel.Instance.UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => UserViewModel.Instance.Password;
            set
            {
                UserViewModel.Instance.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get => UserViewModel.Instance.Email;
            set
            {
                UserViewModel.Instance.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        // Properties for 2FA Fields
        private string _twoFACode;
        public string TwoFACode
        {
            get => _twoFACode;
            set { _twoFACode = value; OnPropertyChanged(nameof(TwoFACode)); }
        }

        // Error Messages
        private string _userNameError;
        public string UserNameError
        {
            get => _userNameError;
            set { _userNameError = value; OnPropertyChanged(nameof(UserNameError)); }
        }

        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set { _passwordError = value; OnPropertyChanged(nameof(PasswordError)); }
        }

        private string _twoFACodeError;
        public string TwoFACodeError
        {
            get => _twoFACodeError;
            set { _twoFACodeError = value; OnPropertyChanged(nameof(TwoFACodeError)); }
        }

        // Visibility Controls
        private bool _is2FAMode;
        public bool Is2FAMode
        {
            get => _is2FAMode;
            set { _is2FAMode = value; OnPropertyChanged(nameof(Is2FAMode)); }
        }

        // Command Implementations
        private void OnLogin(object parameter)
        {
            ClearErrors();

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserNameError = "Username is required.";
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Password is required.";
                isValid = false;
            }

            if (isValid)
            {
                var user = UserViewModel.Instance.Users
                    .FirstOrDefault(u => u.UserName == UserName);

                if (user == null)
                {
                    UserNameError = "User does not exist.";
                }
                else if (user.Password != Password)
                {
                    PasswordError = "Incorrect password.";
                }
                else
                {
                    UserViewModel.Instance.LoggedInUser = user;

                    if (Application.Current.MainWindow is MainWindow window)
                    {
                        window.MainFrame.Navigate(new WorkoutsPage());
                    }
                }
            }
        }

        private void OnForgotPassword(object parameter)
        {
            ClearErrors();

            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserNameError = "Please enter your username.";
                return;
            }

            var user = UserViewModel.Instance.Users.FirstOrDefault(u => u.UserName == UserName);

            if (user != null)
            {
                Is2FAMode = true;
                Email = user.Email;
                string code = _userService.Generate2FACode(user);
                _userService.SendEmail(user.Email, code);
            }
            else
            {
                UserNameError = "User does not exist.";
            }
        }


        private void OnVerify2FA(object parameter)
        {
            TwoFACodeError = "";

            if (string.IsNullOrWhiteSpace(TwoFACode))
            {
                TwoFACodeError = "Please enter the 6-digit code.";
                return;
            }

            var user = UserViewModel.Instance.Users.FirstOrDefault(u => u.UserName == UserName);

            if (user != null)
            {
                if (_userService.Validate2FACode(user, TwoFACode))
                {
                    // Code is valid, reset password or navigate to main page
                    UserViewModel.Instance.LoggedInUser = user;
                    Is2FAMode = false;

                    // Navigate to the main page
                    if (Application.Current.MainWindow is MainWindow window)
                    {
                        window.MainFrame.Navigate(new WorkoutsPage());
                    }
                }
                else
                {
                    TwoFACodeError = "Invalid code.";
                }
            }
            else
            {
                UserNameError = "User does not exist.";
            }
        }

        private void OnBack(object parameter)
        {
            if (Is2FAMode)
            {
                Is2FAMode = false;
            }
            else if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new StartPage());
            }
        }

        private void ClearErrors()
        {
            UserNameError = "";
            PasswordError = "";
            TwoFACodeError = "";
        }
    }
}
