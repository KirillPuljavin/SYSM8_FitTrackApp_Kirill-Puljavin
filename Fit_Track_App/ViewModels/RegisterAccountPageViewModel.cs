using Fit_Track_App.Classes;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class RegisterAccountPageViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }

        public RegisterAccountPageViewModel()
        {
            BackCommand = new RelayCommand(OnBack);
        }

        private void OnBack(object parameter)
        {
            if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AccountWindow) is AccountWindow accountWindow)
            {
                accountWindow.Close();
            }
        }
    }
}
