using Fit_Track_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Fit_Track_App.ViewModels
{
    internal static class Data
    {
        internal static ObservableCollection<DataManagement.User> Users { get; set; } = new ObservableCollection<DataManagement.User>();
        internal static ObservableCollection<DataManagement.Workout> Workouts { get; set; } = new ObservableCollection<DataManagement.Workout>();
    }

    public class UserViewModel : INotifyPropertyChanged
    {
        // VARIABLES
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        private DataManagement.User _loggedInUser;
        internal DataManagement.User LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }

        // COMMANDS
        private RelayCommand _loginCommand;

        // METHODS
        public UserViewModel()
        {
            Data.Users.Add(new DataManagement.User("admin", "admin@fittrack.com", "password", "Sweden", true));
            Data.Users.Add(new DataManagement.User("user", "user@fittrack.com", "password", "Sweden", false));

            _loginCommand = new RelayCommand(Register);
        }

        public bool Login()
        {
            var user = Data.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user != null)
            {
                LoggedInUser = user;
                MessageBox.Show($"Welcome, {LoggedInUser.UserName}!");
                return true;
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                return false;
            }
        }

        public void Register()
        {
            if (Data.Users.Any(u => u.UserName == UserName))
            {
                MessageBox.Show("Username already exists.");
            }
            else
            {
                var newUser = new DataManagement.User(UserName, Email, Password, Country, false);
                Data.Users.Add(newUser);
                MessageBox.Show("User registered successfully.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
