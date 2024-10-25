using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }

        public LoginPageViewModel()
        {
            BackCommand = new RelayCommand(OnBack);
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
