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

            // Reset the placeholders when the page loads
            Loaded += LoginPage_Loaded;
        }
        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            UsernameTextBox.Text = "Username";
            UsernameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            PasswordBox.Password = string.Empty;
            PasswordBox.Foreground = new SolidColorBrush(Colors.Gray);
            PasswordBox.Tag = "Password";

            if (DataContext is LoginPageViewModel viewModel)
            {
                viewModel.LoginFeedback = string.Empty;
                viewModel.UserName = string.Empty;
                viewModel.Password = string.Empty;
            }
        }


        private void UsernameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Tag = "Password";
                PasswordBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

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

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Tag != null && PasswordBox.Tag.ToString() == "Password" && PasswordBox.Password == "")
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

        // Re-add PasswordChanged event handler to sync password with ViewModel
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
