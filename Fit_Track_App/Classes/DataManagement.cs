using System.Collections.ObjectModel;

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

        public abstract class Workout
        {
            public DateTime Date { get; set; }
            public string Type { get; set; }
            public TimeSpan Duration { get; set; }
            public int CaloriesBurned { get; set; }
            public string Notes { get; set; }
            public string Summary => $"{Date.ToShortDateString()} - {Type}: {Duration.TotalMinutes} mins, {CaloriesBurned} cal";

            protected Workout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
            {
                Date = date;
                Type = type;
                Duration = duration;
                CaloriesBurned = caloriesBurned;
                Notes = notes;
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
    }

    public enum WorkoutType
    {
        Cardio,
        Strength
    }
}
