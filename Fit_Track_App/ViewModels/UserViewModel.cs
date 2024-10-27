using Fit_Track_App.Classes;
using Fit_Track_App.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class UserViewModel : ViewModelBase
    {
        private static UserViewModel? _instance;
        public static UserViewModel Instance => _instance ??= new UserViewModel();

        private readonly WorkoutService _workoutService;

        // Delegate for handling navigation to Workouts Page
        public Action NavigateToWorkoutsPage { get; set; }

        internal ObservableCollection<DataManagement.User> Users { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        private DataManagement.User _loggedInUser;
        internal DataManagement.User LoggedInUser
        {
            get => _loggedInUser;
            set { _loggedInUser = value; OnPropertyChanged(nameof(LoggedInUser)); }
        }

        // Workout-related properties
        public ObservableCollection<DataManagement.Workout> Workouts { get; }
        private DataManagement.Workout _selectedWorkout;
        public DataManagement.Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set { _selectedWorkout = value; OnPropertyChanged(nameof(SelectedWorkout)); }
        }

        public DateTime NewWorkoutDate { get; set; }
        public string NewWorkoutType { get; set; }
        public TimeSpan NewWorkoutDuration { get; set; }
        public int NewWorkoutCalories { get; set; }
        public string NewWorkoutNotes { get; set; }

        // Commands
        public ICommand AddWorkoutCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        private UserViewModel()
        {
            _workoutService = new WorkoutService();
            Users = new ObservableCollection<DataManagement.User>
            {
                new DataManagement.User("admin", "admin@fittrack.com", "password", "Sweden", true),
                new DataManagement.User("user", "user@fittrack.com", "password", "Sweden", false)
            };

            Workouts = new ObservableCollection<DataManagement.Workout>(_workoutService.GetAllWorkouts());
            AddWorkoutCommand = new RelayCommand(_ => AddWorkout());
            RemoveWorkoutCommand = new RelayCommand(_ => RemoveWorkout());
            RegisterCommand = new RelayCommand(_ => Register());
            LoginCommand = new RelayCommand(_ => Login());
        }

        public void Login()
        {
            var user = Users.FirstOrDefault(u => u.UserName == UserName && u.Password == Password);
            if (user != null)
            {
                LoggedInUser = user;

                // Navigate to Workouts Page after successful login
                NavigateToWorkoutsPage?.Invoke();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
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

        // Workout management methods
        public void AddWorkout()
        {
            var newWorkout = _workoutService.CreateWorkout(NewWorkoutDate, NewWorkoutType, NewWorkoutDuration, NewWorkoutCalories, NewWorkoutNotes);
            if (newWorkout != null)
            {
                Workouts.Add(newWorkout);
                MessageBox.Show("Workout added successfully.");
            }
            else
            {
                MessageBox.Show("Invalid workout type.");
            }
        }

        public void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                _workoutService.DeleteWorkout(SelectedWorkout);
                Workouts.Remove(SelectedWorkout);
                MessageBox.Show("Workout removed successfully.");
            }
            else
            {
                MessageBox.Show("Please select a workout to remove.");
            }
        }
    }
}
