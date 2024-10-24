using Fit_Track_App.Pages;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.Classes
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand NavigateToLoginPageCommand { get; }
        public ICommand NavigateToRegisterPageCommand { get; }
        public ICommand NavigateToStartPageCommand { get; }

        public MainViewModel()
        {
            NavigateToLoginPageCommand = new RelayCommand(OnNavigateToLoginPage);
            NavigateToRegisterPageCommand = new RelayCommand(OnNavigateToRegisterPage);
            NavigateToStartPageCommand = new RelayCommand(OnNavigateToStartPage);
        }

        private void OnNavigateToLoginPage(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new LoginPage());
            }
        }

        private void OnNavigateToRegisterPage(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new RegisterAccountPage());
            }
        }

        private void OnNavigateToStartPage(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new StartPage());
            }
        }
    }
}
