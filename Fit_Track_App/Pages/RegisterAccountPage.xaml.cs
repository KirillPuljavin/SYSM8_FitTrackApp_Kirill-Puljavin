using Fit_Track_App.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Fit_Track_App.Pages
{
    public partial class RegisterAccountPage : Page
    {
        public RegisterAccountPage()
        {
            InitializeComponent();
            DataContext = new RegisterAccountPageViewModel();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && DataContext is RegisterAccountPageViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox confirmPasswordBox && DataContext is RegisterAccountPageViewModel viewModel)
            {
                viewModel.ConfirmPassword = confirmPasswordBox.Password;
            }
        }
    }
}
