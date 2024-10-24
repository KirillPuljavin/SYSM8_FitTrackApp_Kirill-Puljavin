using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        public ICommand SignInCommand { get; }
        public ICommand LogInCommand { get; }

        public StartPageViewModel()
        {
            SignInCommand = new RelayCommand(OnSignIn);
            LogInCommand = new RelayCommand(OnLogIn);
        }

        private void OnSignIn(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new RegisterAccountPage());
            }
        }

        private void OnLogIn(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new LoginPage());
            }
        }
    }
}
