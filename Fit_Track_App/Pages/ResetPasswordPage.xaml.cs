using System.Windows;
using System.Windows.Controls;

namespace Fit_Track_App.Pages
{
    public partial class ResetPasswordPage : Page
    {
        public ResetPasswordPage()
        {
            InitializeComponent();
            DataContext = new ViewModels.ResetPasswordViewModel();
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.ResetPasswordViewModel viewModel)
            {
                viewModel.NewPassword = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModels.ResetPasswordViewModel viewModel)
            {
                viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
