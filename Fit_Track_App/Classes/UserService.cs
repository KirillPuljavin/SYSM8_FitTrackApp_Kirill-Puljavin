using Fit_Track_App.Classes;
using System.Collections.ObjectModel;

namespace Fit_Track_App.Services
{
    public class UserService
    {
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
                return "Password must be at least 8 characters, include a number and a special character.";

            var newUser = new DataManagement.User(username, email, password, country, false);
            _users.Add(newUser);
            return "User registered successfully.";
        }

        public bool ForgotPassword(string username, string securityAnswer)
        {
            // Implement security question validation here
            return true; // Placeholder for actual security logic
        }

        public string Generate2FACode()
        {
            // Generates a random 6-digit code for 2FA simulation
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
