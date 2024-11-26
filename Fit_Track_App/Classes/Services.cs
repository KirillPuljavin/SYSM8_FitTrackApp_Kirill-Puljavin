using Fit_Track_App.Classes;

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
            user.TwoFACodeExpiry = DateTime.Now.AddMinutes(10);
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
    }
}
