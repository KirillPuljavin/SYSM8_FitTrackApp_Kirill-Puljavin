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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginPageViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }
    }
}
