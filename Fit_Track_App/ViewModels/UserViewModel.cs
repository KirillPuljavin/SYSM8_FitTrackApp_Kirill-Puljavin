using Fit_Track_App.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Fit_Track_App.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private static UserViewModel _instance;
        public static UserViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserViewModel();
                }
                return _instance;
            }
        }

        internal ObservableCollection<DataManagement.User> Users { get; set; }
        internal ObservableCollection<DataManagement.Workout> Workouts { get; set; }

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

        private RelayCommand _loginCommand;

        private UserViewModel()
        {
            Users = new ObservableCollection<DataManagement.User>
            {
                new DataManagement.User("admin", "admin@fittrack.com", "password", "Sweden", true),
                new DataManagement.User("user", "user@fittrack.com", "password", "Sweden", false)
            };

            Workouts = new ObservableCollection<DataManagement.Workout>();
            _loginCommand = new RelayCommand(Register);
        }

        public bool Login()
        {
            var user = Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
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
            if (Users.Any(u => u.UserName == UserName))
            {
                MessageBox.Show("Username already exists.");
            }
            else
            {
                var newUser = new DataManagement.User(UserName, Email, Password, Country, false);
                Users.Add(newUser);
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
