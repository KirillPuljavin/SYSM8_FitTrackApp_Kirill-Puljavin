using Fit_Track_App.ViewModels;
using System.Windows.Controls;

namespace Fit_Track_App.Pages
{
    public partial class UserDetailsPage : Page
    {
        public UserDetailsPage()
        {
            InitializeComponent();
            DataContext = new UserDetailsPageViewModel();
        }

        // Handle the PasswordBox events to bind password values to the ViewModel
        private void CurrentPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is UserDetailsPageViewModel viewModel)
            {
                viewModel.CurrentPassword = ((PasswordBox)sender).Password;
            }
        }

        private void NewPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is UserDetailsPageViewModel viewModel)
            {
                viewModel.NewPassword = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmNewPasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is UserDetailsPageViewModel viewModel)
            {
                viewModel.ConfirmNewPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
