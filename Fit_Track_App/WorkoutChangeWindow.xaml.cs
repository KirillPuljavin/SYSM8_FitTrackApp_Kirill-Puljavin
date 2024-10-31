using Fit_Track_App.Classes;
using Fit_Track_App.ViewModels;
using System.Windows;

namespace Fit_Track_App.Windows
{
    public partial class WorkoutChangeWindow : Window
    {
        internal WorkoutChangeWindow(DataManagement.Workout workout)
        {
            InitializeComponent();
            var viewModel = new WorkoutChangeViewModel(workout);
            viewModel.CloseAction = Close;  // Set the close action to close the window
            DataContext = viewModel;
        }

        public bool DialogResult => ((WorkoutChangeViewModel)DataContext).DialogResult;
    }
}
