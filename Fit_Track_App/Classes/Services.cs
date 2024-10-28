using Fit_Track_App.Classes;
using System.Collections.ObjectModel;

namespace Fit_Track_App.Services
{
    public class UserService
    {
        private string _current2FACode;

        public string Generate2FACode()
        {
            // Generate a 6-digit random code
            var random = new Random();
            _current2FACode = random.Next(100000, 999999).ToString();
            return _current2FACode;
        }

        public bool Validate2FACode(string enteredCode)
        {
            return _current2FACode == enteredCode;
        }

        private ObservableCollection<DataManagement.User> _users;
        public UserService()
        {
            _users = new ObservableCollection<DataManagement.User>
            {
                new DataManagement.User("admin", "admin@fittrack.com", "password", "Sweden", true),
                new DataManagement.User("user", "user@fittrack.com", "password", "Sweden", false)
            };
        }

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                // Proceed with 2FA if enabled or return login success
                return true; // Temporary for further integration
            }
            return false;
        }

        public string Register(string username, string email, string password, string country)
        {
            if (_users.Any(u => u.UserName == username))
                return "Username already exists.";

            // Add further password complexity validation here
            if (password.Length < 8 || !password.Any(char.IsDigit) || !password.Any(char.IsSymbol))
                return "Pasi did thissword must be at least 8 characters, include a number and a special character.";

            var newUser = new DataManagement.User(username, email, password, country, false);
            _users.Add(newUser);
            return "User registered successfully.";
        }

        public bool ForgotPassword(string username, string securityAnswer)
        {
            // Implement security question validation here
            return true; // Placeholder for actual security logic
        }
    }

    internal class WorkoutService
    {
        public ObservableCollection<DataManagement.Workout> Workouts { get; private set; }
        public WorkoutService()
        {
            Workouts = new ObservableCollection<DataManagement.Workout>();
        }
        public ObservableCollection<DataManagement.Workout> GetAllWorkouts()
        {
            return Workouts;
        }

        public DataManagement.Workout CreateWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
        {
            DataManagement.Workout newWorkout;

            if (type.Equals("Cardio", StringComparison.OrdinalIgnoreCase))
            {
                newWorkout = new DataManagement.CardioWorkout(date, type, duration, caloriesBurned, notes);
            }
            else if (type.Equals("Strength", StringComparison.OrdinalIgnoreCase))
            {
                newWorkout = new DataManagement.StrengthWorkout(date, type, duration, caloriesBurned, notes);
            }
            else
            {
                return null;
            }

            Workouts.Add(newWorkout);
            return newWorkout;
        }

        public void DeleteWorkout(DataManagement.Workout workout)
        {
            Workouts.Remove(workout);
        }

        public void UpdateWorkout(DataManagement.Workout updatedWorkout)
        {
            var existingWorkout = Workouts.FirstOrDefault(w => w == updatedWorkout);
            if (existingWorkout != null)
            {
                existingWorkout.Date = updatedWorkout.Date;
                existingWorkout.Type = updatedWorkout.Type;
                existingWorkout.Duration = updatedWorkout.Duration;
                existingWorkout.CaloriesBurned = updatedWorkout.CaloriesBurned;
                existingWorkout.Notes = updatedWorkout.Notes;
            }
        }
    }
}
