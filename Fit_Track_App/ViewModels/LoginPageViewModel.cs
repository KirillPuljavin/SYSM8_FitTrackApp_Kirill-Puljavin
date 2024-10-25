using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
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

        public LoginPageViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);
            BackCommand = new RelayCommand(OnBack);
        }

        private void OnLogin(object parameter)
        {
            if (UserViewModel.Instance.Login())
            {
                // Navigate to the next page
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
}
