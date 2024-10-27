using Fit_Track_App.Pages;
using Fit_Track_App.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.Classes
{
    internal class MainViewModel : ViewModelBase
    {
        public ICommand NavigateToLoginPageCommand { get; }
        public ICommand NavigateToRegisterPageCommand { get; }
        public ICommand NavigateToWorkoutsPageCommand { get; }

        public MainViewModel()
        {
            NavigateToLoginPageCommand = new RelayCommand(OnNavigateToLoginPage);
            NavigateToRegisterPageCommand = new RelayCommand(OnNavigateToRegisterPage);
            NavigateToWorkoutsPageCommand = new RelayCommand(OnNavigateToWorkoutsPage);

            // Set the navigation callback
            UserViewModel.Instance.NavigateToWorkoutsPage = () =>
            {
                if (Application.Current.MainWindow is MainWindow window)
                {
                    window.MainFrame.Navigate(new WorkoutsPage());
                }
            };
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
            var accountWindow = new AccountWindow();
            accountWindow.ShowDialog();
        }

        private void OnNavigateToWorkoutsPage(object parameter)
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new WorkoutsPage());
            }
        }
    }
}
