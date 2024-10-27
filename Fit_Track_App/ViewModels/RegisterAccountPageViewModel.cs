using Fit_Track_App.Classes;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class RegisterAccountPageViewModel : ViewModelBase
    {
        public ICommand RegisterCommand { get; }
        public ICommand BackCommand { get; }

        public string UserName
        {
            get => UserViewModel.Instance.UserName;
            set => UserViewModel.Instance.UserName = value;
        }

        public string Email
        {
            get => UserViewModel.Instance.Email;
            set => UserViewModel.Instance.Email = value;
        }

        public string Password
        {
            get => UserViewModel.Instance.Password;
            set => UserViewModel.Instance.Password = value;
        }

        public string Country
        {
            get => UserViewModel.Instance.Country;
            set => UserViewModel.Instance.Country = value;
        }

        public RegisterAccountPageViewModel()
        {
            RegisterCommand = new RelayCommand(OnRegister);
            BackCommand = new RelayCommand(OnBack);
        }

        private void OnRegister(object parameter)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Country))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Register user through UserViewModel's Register method
            UserViewModel.Instance.Register();
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
