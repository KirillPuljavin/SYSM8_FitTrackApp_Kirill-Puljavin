using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Fit_Track_App.Classes
{
    internal static class DataManagement
    {
        public abstract class Person(string userName, string email, string password)
        {
            public string UserName { get; set; } = userName;
            public string Email { get; set; } = email;
            public string Password { get; set; } = password;

            public abstract void Signin();
        }

        public class User : Person
        {
            public ObservableCollection<Workout> Workouts { get; } = new ObservableCollection<Workout>();
            public string Country { get; set; }
            public bool IsAdmin { get; set; }
            public string TwoFACode { get; set; }
            public DateTime? TwoFACodeExpiry { get; set; }

            public User(string userName, string email, string password, string country, bool isAdmin) : base(userName, email, password)
            {
                UserName = userName;
                Email = email;
                Password = password;
                Country = country;
                IsAdmin = isAdmin;
            }

            public override void Signin() => throw new System.NotImplementedException();
        }

        public class Admin : User
        {
            public Admin(string userName, string email, string password, string country, bool isAdmin)
                : base(userName, email, password, country, isAdmin) { }

            public void ManageWorkouts() => throw new System.NotImplementedException();
            public void ManageUsers() => throw new System.NotImplementedException();
        }

        public abstract class Workout : INotifyPropertyChanged
        {
            private DateTime _date;
            private string _type;
            private TimeSpan _duration;
            private int _caloriesBurned;
            private string _notes;

            public event PropertyChangedEventHandler? PropertyChanged;
            public DateTime Date
            {
                get => _date;
                set { _date = value; OnPropertyChanged(); }
            }
            public string Type
            {
                get => _type;
                set { _type = value; OnPropertyChanged(); }
            }
            public TimeSpan Duration
            {
                get => _duration;
                set { _duration = value; OnPropertyChanged(); }
            }
            public int CaloriesBurned
            {
                get => _caloriesBurned;
                set { _caloriesBurned = value; OnPropertyChanged(); }
            }
            public string Notes
            {
                get => _notes;
                set { _notes = value; OnPropertyChanged(); }
            }
            protected Workout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
            {
                Date = date;
                Type = type;
                Duration = duration;
                CaloriesBurned = caloriesBurned;
                Notes = notes;
            }

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public abstract int CalculateCaloriesBurned();
        }

        public class CardioWorkout : Workout
        {
            public CardioWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
                : base(date, type, duration, caloriesBurned, notes) { }


            public override int CalculateCaloriesBurned() => (int)(Duration.TotalMinutes * 10);
        }

        public class StrengthWorkout : Workout
        {
            public StrengthWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
                : base(date, type, duration, caloriesBurned, notes) { }

            public override int CalculateCaloriesBurned() => (int)(Duration.TotalMinutes * 8);
        }

        public static List<string> Countries { get; } = new List<string> {
                "Sweden", "Norway", "Denmark", "Finland", "Iceland", "Germany", "France", "Spain",
                "Italy", "Netherlands", "Belgium", "Luxembourg", "Austria", "Switzerland", "Ireland",
                "Portugal", "Greece", "Czech Republic", "Poland", "Hungary", "Romania", "Bulgaria",
                "Estonia", "Latvia", "Lithuania", "Slovakia", "Slovenia", "Croatia", "Cyprus", "Malta"
            };
    }

    public enum WorkoutType
    {
        Cardio,
        Strength
    }
}
