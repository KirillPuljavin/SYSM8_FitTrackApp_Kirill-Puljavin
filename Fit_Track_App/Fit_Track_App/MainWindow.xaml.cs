using Fit_Track_App.ViewModels;
using System.Windows;

namespace Fit_Track_App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
            MainFrame.Navigate(new Pages.StartPage());
        }
    }
}