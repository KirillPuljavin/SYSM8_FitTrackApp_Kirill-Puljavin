using Fit_Track_App.ViewModels;
using System.Windows.Controls;

namespace Fit_Track_App.Pages
{
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
            DataContext = new StartPageViewModel();
        }
    }
}
