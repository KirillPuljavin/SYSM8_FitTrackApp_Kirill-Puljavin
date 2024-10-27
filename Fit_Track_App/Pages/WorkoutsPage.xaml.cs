using Fit_Track_App.ViewModels;
using System.Windows.Controls;

namespace Fit_Track_App.Pages
{
    public partial class WorkoutsPage : Page
    {
        public WorkoutsPage()
        {
            InitializeComponent();
            DataContext = UserViewModel.Instance;
        }
    }
}
