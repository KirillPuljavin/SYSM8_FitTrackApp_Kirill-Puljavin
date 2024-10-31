using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class UserDetailsPageViewModel : ViewModelBase
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName)); // Notify UI of the change
                }
            }
        }
        public string Email { get; set; }
        public string SelectedCountry { get; set; }
        public List<string> EuropeanCountries { get; }
        public string CurrentPassword { private get; set; }
        public string NewPassword { private get; set; }
        public string ConfirmNewPassword { private get; set; }

        public string UserNameError { get; private set; }
        public string EmailError { get; private set; }
        public string CountryError { get; private set; }
        public string CurrentPasswordError { get; private set; }
        public string NewPasswordError { get; private set; }
        public string ConfirmNewPasswordError { get; private set; }
        public string TotalWorkoutsText => $"Total Workouts: {WorkoutViewModel.Instance.Workouts.Count}";

        public ICommand UpdateProfileCommand { get; }
        public ICommand UpdatePasswordCommand { get; }
        public ICommand ResetWorkoutsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand BackCommand { get; }

        public UserDetailsPageViewModel()
        {
            UpdateProfileCommand = new RelayCommand(_ => UpdateProfile());
            UpdatePasswordCommand = new RelayCommand(_ => UpdatePassword());
            ResetWorkoutsCommand = new RelayCommand(_ => ResetWorkouts());
            BackCommand = new RelayCommand(_ => NavigateBack());
            LogoutCommand = new RelayCommand(_ => Logout());

            EuropeanCountries = new List<string>
            {
                "Sweden", "Norway", "Denmark", "Finland", "Iceland", "Germany", "France", "Spain",
                "Italy", "Netherlands", "Belgium", "Luxembourg", "Austria", "Switzerland", "Ireland",
                "Portugal", "Greece", "Czech Republic", "Poland", "Hungary", "Romania", "Bulgaria",
                "Estonia", "Latvia", "Lithuania", "Slovakia", "Slovenia", "Croatia", "Cyprus", "Malta"
            };

            LoadUserData();
        }

        private void LoadUserData()
        {
            var user = UserViewModel.Instance.LoggedInUser;
            UserName = user.UserName;
            Email = user.Email;
            SelectedCountry = user.Country;
        }

        private void UpdateProfile()
        {
            ClearProfileErrors();

            if (!ValidateUserName() || !ValidateEmail() || string.IsNullOrWhiteSpace(SelectedCountry))
            {
                MessageBox.Show("Please correct the errors before updating the profile.");
                return;
            }

            UserViewModel.Instance.UserName = UserName;
            UserViewModel.Instance.Email = Email;
            UserViewModel.Instance.Country = SelectedCountry;
            MessageBox.Show("Profile updated successfully.");
        }

        private void UpdatePassword()
        {
            ClearPasswordErrors();
            bool isValid = true;

            // Validate Current Password
            if (UserViewModel.Instance.LoggedInUser.Password != CurrentPassword || NewPassword == UserViewModel.Instance.LoggedInUser.Password)
            {
                CurrentPasswordError = "Incorrect current password.";
                OnPropertyChanged(nameof(CurrentPasswordError));
                isValid = false;
            }
            // Validate New Password Criteria
            if (string.IsNullOrWhiteSpace(NewPassword) || NewPassword.Length < 4 || !Regex.IsMatch(NewPassword, @"^(?=.*[a-zA-Z])(?=.*\d).{4,}$"))
            {
                NewPasswordError = "Password must be at least 4 characters, with one letter and one number.";
                OnPropertyChanged(nameof(NewPasswordError));
                isValid = false;
            }
            // Check if New Password Matches Confirm Password
            if (NewPassword != ConfirmNewPassword)
            {
                ConfirmNewPasswordError = "Passwords do not match.";
                OnPropertyChanged(nameof(ConfirmNewPasswordError));
                isValid = false;
            }

            if (!isValid) return;
            UserViewModel.Instance.LoggedInUser.Password = NewPassword;
            MessageBox.Show("Password updated successfully.");
        }

        private bool ValidateUserName()
        {
            // Minimum 3 characters, only allows alphanumeric with _-! symbols
            if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3 ||
                !Regex.IsMatch(UserName, @"^[a-zA-Z0-9_\-!]{3,}$"))
            {
                UserNameError = "Username must be at least 3 characters and can contain letters, numbers, and symbols: _ - !";
                OnPropertyChanged(nameof(UserNameError));
                return false;
            }

            UserNameError = null;
            OnPropertyChanged(nameof(UserNameError));
            return true;
        }


        private bool ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailError = "Invalid email format.";
                OnPropertyChanged(nameof(EmailError));
                return false;
            }

            EmailError = null;
            OnPropertyChanged(nameof(EmailError));
            return true;
        }

        private void ClearProfileErrors()
        {
            UserNameError = null;
            EmailError = null;
            CountryError = null;
            OnPropertyChanged(nameof(UserNameError));
            OnPropertyChanged(nameof(EmailError));
            OnPropertyChanged(nameof(CountryError));
        }

        private void ClearPasswordErrors()
        {
            CurrentPasswordError = null;
            NewPasswordError = null;
            ConfirmNewPasswordError = null;
            OnPropertyChanged(nameof(CurrentPasswordError));
            OnPropertyChanged(nameof(NewPasswordError));
            OnPropertyChanged(nameof(ConfirmNewPasswordError));
        }

        private void ResetWorkouts()
        {
            WorkoutViewModel.Instance.Workouts.Clear();
            MessageBox.Show("All workouts have been reset.");
        }

        private void NavigateBack()
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new WorkoutsPage());
            }
        }
        private void Logout()
        {
            UserViewModel.Instance.LoggedInUser = null;
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new StartPage());
            }
        }
    }
}
