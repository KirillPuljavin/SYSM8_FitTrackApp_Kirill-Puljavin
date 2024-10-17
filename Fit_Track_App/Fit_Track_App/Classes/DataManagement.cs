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
            public string Country { get; set; }
            public bool IsAdmin { get; set; }

            public User(string userName, string email, string password, string country, bool isAdmin) : base(userName, email, password)
            {
                userName = UserName;
                email = Email;
                password = Password;
                country = Country;
                isAdmin = IsAdmin;
            }

            public override void Signin()
            {
                throw new System.NotImplementedException();
            }
        }
        public class Admin : User
        {
            public Admin(string userName, string email, string password, string country, bool isAdmin) : base(userName, email, password, country, isAdmin)
            {
                userName = UserName;
                email = Email;
                password = Password;
                country = Country;
                isAdmin = IsAdmin;
            }

            public void ManageWorkouts()
            {
                throw new System.NotImplementedException();
            }
            public void ManageUsers()
            {
                throw new System.NotImplementedException();
            }
        }

        public abstract class Workout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes)
        {
            public DateTime Date { get; set; } = date;
            public string Type { get; set; } = type;
            public TimeSpan Duration { get; set; } = duration;
            public int CaloriesBurned { get; set; } = caloriesBurned;
            public string Notes { get; set; } = notes;

            public abstract int CalculateCaloriesBurned();
        }

        public class CardioWorkout : Workout
        {
            public CardioWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes) : base(date, type, duration, caloriesBurned, notes)
            {

            }

            public override int CalculateCaloriesBurned()
            {
                throw new NotImplementedException();
            }
        }

        public class StrenghtWorkout : Workout
        {
            public StrenghtWorkout(DateTime date, string type, TimeSpan duration, int caloriesBurned, string notes) : base(date, type, duration, caloriesBurned, notes)
            {

            }

            public override int CalculateCaloriesBurned()
            {
                throw new NotImplementedException();
            }
        }
    }
}
