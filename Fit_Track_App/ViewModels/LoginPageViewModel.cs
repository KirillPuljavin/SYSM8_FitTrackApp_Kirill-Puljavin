using Fit_Track_App;
using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using Fit_Track_App.ViewModels;
using System.Windows;
using System.Windows.Input;

internal class LoginPageViewModel : ViewModelBase
{
    public ICommand LoginCommand { get; }
    public ICommand BackCommand { get; }

    public string UserName
    {
        get => UserViewModel.Instance.UserName;
        set => UserViewModel.Instance.UserName = value;
    }

    public string Password
    {
        get => UserViewModel.Instance.Password;
        set => UserViewModel.Instance.Password = value;
    }

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

    public LoginPageViewModel()
    {
        LoginCommand = new RelayCommand(OnLogin);
        BackCommand = new RelayCommand(OnBack);
    }

    private void OnLogin(object parameter)
    {
        UserNameError = "";
        PasswordError = "";

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

    private void OnBack(object parameter)
    {
        if (Application.Current.MainWindow is MainWindow window)
        {
            window.MainFrame.Navigate(new StartPage());
        }
    }
}
