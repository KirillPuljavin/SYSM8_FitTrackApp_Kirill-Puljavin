using Fit_Track_App.Classes;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class UserViewModel : ViewModelBase
    {
        private static UserViewModel? _instance;
        public static UserViewModel Instance => _instance ??= new UserViewModel();

        // Delegate for handling navigation to Workouts Page
        public Action NavigateToWorkoutsPage { get; set; }

        internal ObservableCollection<DataManagement.User> Users { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }

        private DataManagement.User _loggedInUser;
        internal DataManagement.User LoggedInUser
        {
            get => _loggedInUser;
            set { _loggedInUser = value; OnPropertyChanged(nameof(LoggedInUser)); }
        }

        // Error message properties
        private string _userNameError;
        public string UserNameError
        {
            get => _userNameError;
            set { _userNameError = value; OnPropertyChanged(nameof(UserNameError)); }
        }
        private string _emailError;
        public string EmailError
        {
            get => _emailError;
            set { _emailError = value; OnPropertyChanged(nameof(EmailError)); }
        }
        private string _passwordError;
        public string PasswordError
        {
            get => _passwordError;
            set { _passwordError = value; OnPropertyChanged(nameof(PasswordError)); }
        }
        private string _confirmPasswordError;
        public string ConfirmPasswordError
        {
            get => _confirmPasswordError;
            set { _confirmPasswordError = value; OnPropertyChanged(nameof(ConfirmPasswordError)); }
        }
        private string _countryError;
        public string CountryError
        {
            get => _countryError;
            set { _countryError = value; OnPropertyChanged(nameof(CountryError)); }
        }

        // Commands
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private UserViewModel()
        {
            // DEFAULT USERS
            Users = new ObservableCollection<DataManagement.User>
            {
                new DataManagement.User("admin", "admin@fittrack.com", "password", "Sweden", true),
                new DataManagement.User("user", "user@fittrack.com", "password", "Sweden", false),
                new DataManagement.User("qwe123", "thelegendaryyouth@gmail.com", "qwe123", "Sweden", false)
            };

            RegisterCommand = new RelayCommand(_ => Register());
            LoginCommand = new RelayCommand(_ => Login());
        }

        public void Login()
        {
            var user = Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user != null)
            {
                LoggedInUser = user;
                NavigateToWorkoutsPage?.Invoke();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        public bool Register()
        {
            // Clear previous error messages
            UserNameError = "";
            EmailError = "";
            PasswordError = "";
            ConfirmPasswordError = "";
            CountryError = "";

            bool isValid = true;

            // Validate UserName
            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserNameError = "Username is required.";
                isValid = false;
            }
            else if (Users.Any(u => u.UserName == UserName))
            {
                UserNameError = "Username already exists.";
                isValid = false;
            }
            else if (UserName.Length < 3 || !Regex.IsMatch(UserName, @"^[a-zA-Z0-9_\-\!\?\[\]]+$"))
            {
                UserNameError = "Username must be at least 3 characters and can contain letters, numbers, and only '_','-','!','?','[',']'.";
                isValid = false;
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email is required.";
                isValid = false;
            }
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailError = "Please enter a valid email address.";
                isValid = false;
            }

            // Validate Password
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Password is required.";
                isValid = false;
            }
            else if (Password.Length < 4 || !Regex.IsMatch(Password, @"^(?=.*[a-zA-Z])(?=.*[0-9]).+$"))
            {
                PasswordError = "Password must be at least 4 characters and contain both letters and numbers.";
                isValid = false;
            }

            // Validate ConfirmPassword
            if (string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ConfirmPasswordError = "Please confirm your password.";
                isValid = false;
            }
            else if (Password != ConfirmPassword)
            {
                ConfirmPasswordError = "Passwords do not match.";
                isValid = false;
            }

            // Validate Country
            if (string.IsNullOrWhiteSpace(Country))
            {
                CountryError = "Country is required.";
                isValid = false;
            }

            if (isValid)
            {
                var newUser = new DataManagement.User(UserName, Email, Password, Country, false);
                Users.Add(newUser);
            }

            return isValid;
        }
    }
}
