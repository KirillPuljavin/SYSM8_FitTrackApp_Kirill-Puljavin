using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class UserDetailsPageViewModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string CurrentPassword { private get; set; }
        public string NewPassword { private get; set; }
        public string ConfirmNewPassword { private get; set; }

        public string UserNameError => UserViewModel.Instance.UserNameError;
        public string EmailError => UserViewModel.Instance.EmailError;
        public string CountryError => UserViewModel.Instance.CountryError;
        public string CurrentPasswordError { get; private set; }
        public string NewPasswordError { get; private set; }
        public string ConfirmNewPasswordError { get; private set; }
        public string TotalWorkoutsText => $"Total Workouts: {WorkoutViewModel.Instance.Workouts.Count}";

        public ICommand UpdateProfileCommand { get; }
        public ICommand UpdatePasswordCommand { get; }
        public ICommand ResetWorkoutsCommand { get; }
        public ICommand BackCommand { get; }

        public UserDetailsPageViewModel()
        {
            UpdateProfileCommand = new RelayCommand(_ => UpdateProfile());
            UpdatePasswordCommand = new RelayCommand(_ => UpdatePassword(), _ => CanUpdatePassword());
            ResetWorkoutsCommand = new RelayCommand(_ => ResetWorkouts());
            BackCommand = new RelayCommand(_ => NavigateBack());

            LoadUserData();
        }

        private void LoadUserData()
        {
            var user = UserViewModel.Instance.LoggedInUser;
            UserName = user.UserName;
            Email = user.Email;
            Country = user.Country;
        }

        private void UpdateProfile()
        {
            UserViewModel.Instance.UserName = UserName;
            UserViewModel.Instance.Email = Email;
            UserViewModel.Instance.Country = Country;
            MessageBox.Show("Profile updated successfully.");
        }

        private bool CanUpdatePassword() =>
            !string.IsNullOrWhiteSpace(CurrentPassword) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            NewPassword == ConfirmNewPassword;

        private void UpdatePassword()
        {
            ClearPasswordErrors();

            if (UserViewModel.Instance.LoggedInUser.Password != CurrentPassword)
            {
                CurrentPasswordError = "Incorrect current password.";
                OnPropertyChanged(nameof(CurrentPasswordError));
                return;
            }

            if (NewPassword != ConfirmNewPassword)
            {
                ConfirmNewPasswordError = "Passwords do not match.";
                OnPropertyChanged(nameof(ConfirmNewPasswordError));
                return;
            }

            UserViewModel.Instance.LoggedInUser.Password = NewPassword;
            MessageBox.Show("Password updated successfully.");
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

        private void ClearPasswordErrors()
        {
            CurrentPasswordError = "";
            NewPasswordError = "";
            ConfirmNewPasswordError = "";
            OnPropertyChanged(nameof(CurrentPasswordError));
            OnPropertyChanged(nameof(NewPasswordError));
            OnPropertyChanged(nameof(ConfirmNewPasswordError));
        }
    }
}
