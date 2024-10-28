using Fit_Track_App.Classes;
using System.Windows;

namespace Fit_Track_App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MainFrame.Navigate(new Pages.StartPage());
        }
    }
}
