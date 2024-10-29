using Fit_Track_App.Classes;
using System.ComponentModel;
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

        public string ConfirmPassword
        {
            get => UserViewModel.Instance.ConfirmPassword;
            set => UserViewModel.Instance.ConfirmPassword = value;
        }

        public string Country
        {
            get => UserViewModel.Instance.Country;
            set => UserViewModel.Instance.Country = value;
        }

        public string UserNameError => UserViewModel.Instance.UserNameError;
        public string EmailError => UserViewModel.Instance.EmailError;
        public string PasswordError => UserViewModel.Instance.PasswordError;
        public string ConfirmPasswordError => UserViewModel.Instance.ConfirmPasswordError;
        public string CountryError => UserViewModel.Instance.CountryError;

        public RegisterAccountPageViewModel()
        {
            RegisterCommand = new RelayCommand(OnRegister);
            BackCommand = new RelayCommand(OnBack);

            UserViewModel.Instance.PropertyChanged += UserViewModel_PropertyChanged;
        }

        private void OnRegister(object parameter)
        {
            UserViewModel.Instance.Register();
        }

        private void OnBack(object parameter)
        {
            if (Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is AccountWindow) is AccountWindow accountWindow)
            {
                accountWindow.Close();
            }
        }

        private void UserViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserViewModel.Instance.UserNameError))
                OnPropertyChanged(nameof(UserNameError));
            else if (e.PropertyName == nameof(UserViewModel.Instance.EmailError))
                OnPropertyChanged(nameof(EmailError));
            else if (e.PropertyName == nameof(UserViewModel.Instance.PasswordError))
                OnPropertyChanged(nameof(PasswordError));
            else if (e.PropertyName == nameof(UserViewModel.Instance.ConfirmPasswordError))
                OnPropertyChanged(nameof(ConfirmPasswordError));
            else if (e.PropertyName == nameof(UserViewModel.Instance.CountryError))
                OnPropertyChanged(nameof(CountryError));
        }
    }
}
