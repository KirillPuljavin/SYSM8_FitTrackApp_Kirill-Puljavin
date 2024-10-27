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

    private string _loginFeedback;
    public string LoginFeedback
    {
        get => _loginFeedback;
        set
        {
            _loginFeedback = value;
            OnPropertyChanged(nameof(LoginFeedback));
        }
    }

    public LoginPageViewModel()
    {
        LoginCommand = new RelayCommand(OnLogin);
        BackCommand = new RelayCommand(OnBack);
    }

    private void OnLogin(object parameter)
    {
        var user = UserViewModel.Instance.Users
            .FirstOrDefault(u => u.UserName == UserName);

        if (user == null)
        {
            LoginFeedback = "User does not exist.";
        }
        else if (user.Password != Password)
        {
            LoginFeedback = "Incorrect password.";
        }
        else
        {
            UserViewModel.Instance.LoggedInUser = user;
            LoginFeedback = string.Empty;

            // Navigate to Workouts Page after successful login
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new WorkoutsPage());
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
