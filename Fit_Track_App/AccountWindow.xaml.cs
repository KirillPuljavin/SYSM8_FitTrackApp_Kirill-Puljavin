using System.Windows;

namespace Fit_Track_App
{
    public partial class AccountWindow : Window
    {
        public AccountWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.RegisterAccountPage());
        }
    }
}
