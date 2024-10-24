using Fit_Track_App.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fit_Track_App.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = new LoginPageViewModel();
        }


        // Username Placeholder Logic
        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        // Password Placeholder Logic
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Tag.ToString() == "Password" && PasswordBox.Password == "")
            {
                PasswordBox.Foreground = new SolidColorBrush(Colors.Black);
                PasswordBox.Tag = "";
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Foreground = new SolidColorBrush(Colors.Gray);
                PasswordBox.Tag = "Password";
            }
        }
    }
}
