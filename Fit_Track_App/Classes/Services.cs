using Fit_Track_App.Classes;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace Fit_Track_App.Services
{
    public class UserService
    {
        private string _current2FACode;

        internal string Generate2FACode(DataManagement.User user)
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();
            user.TwoFACode = code;
            user.TwoFACodeExpiry = DateTime.Now.AddMinutes(10); // Code valid for 10 minutes
            return code;
        }
        internal bool Validate2FACode(DataManagement.User user, string enteredCode)
        {
            if (user.TwoFACode == enteredCode && DateTime.Now <= user.TwoFACodeExpiry)
            {
                user.TwoFACode = null;
                user.TwoFACodeExpiry = null;
                return true;
            }
            return false;
        }
        public void SendEmail(string toEmail, string code)
        {
            try // SEND EMAIL
            {
                var fromAddress = new MailAddress("fit.tracker.api@gmail.com", "Fit Tracker PRO");
                var toAddress = new MailAddress(toEmail);
                const string subject = "Your Fit Tracker PRO 2FA Code";
                string body = $"Your 2FA code is: {code}";

                var smtp = new SmtpClient
                {
                    Host = "smtp-relay.brevo.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("7ee2b5001@smtp-brevo.com", "wsdVKMAQSYZW8UPz")
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (SmtpException ex)
            {
                MessageBox.Show("Failed to send email. Please check your email settings.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred. Please try again later.");
            }
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
                return true;
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
            return true;
        }
    }
}
