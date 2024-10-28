using Fit_Track_App.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    internal class WorkoutsPageViewModel : ViewModelBase
    {
        public ObservableCollection<DataManagement.Workout> Workouts => WorkoutViewModel.Instance.Workouts;

        private DataManagement.Workout _selectedWorkout;
        public DataManagement.Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                OnPropertyChanged(nameof(CanEditOrRemove));
            }
        }

        public DateTime NewWorkoutDate { get; set; } = DateTime.Today;
        internal WorkoutType NewWorkoutType { get; set; } = WorkoutType.Cardio;


        // Duration in minutes
        public int NewWorkoutDuration { get; set; }
        public int NewWorkoutCalories { get; set; }
        public string NewWorkoutNotes { get; set; }

        // Command properties
        public ICommand AddWorkoutCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }

        public bool CanEditOrRemove => SelectedWorkout != null;

        public WorkoutsPageViewModel()
        {
            AddWorkoutCommand = new RelayCommand(_ => AddWorkout(), _ => CanAddWorkout());
            RemoveWorkoutCommand = new RelayCommand(_ => RemoveWorkout(), _ => CanEditOrRemove);
        }

        private bool CanAddWorkout()
        {
            string workoutTypeAsString = Enum.GetName(typeof(WorkoutType), NewWorkoutType);
            return !string.IsNullOrWhiteSpace(workoutTypeAsString) &&
                   NewWorkoutDuration > 0 &&
                   NewWorkoutCalories > 0;
        }

        private void AddWorkout()
        {
            if (!CanAddWorkout())
            {
                MessageBox.Show("Please fill in all workout details correctly.");
                return;
            }

            // Convert minutes to TimeSpan for storage
            var durationTimeSpan = TimeSpan.FromMinutes(NewWorkoutDuration);
            WorkoutViewModel.Instance.AddWorkout(NewWorkoutDate, NewWorkoutType, durationTimeSpan, NewWorkoutCalories, NewWorkoutNotes);

            ClearWorkoutInputFields();
        }

        private void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                WorkoutViewModel.Instance.RemoveWorkout(SelectedWorkout);
                SelectedWorkout = null;
            }
            else
            {
                MessageBox.Show("Please select a workout to remove.");
            }
        }

        private void ClearWorkoutInputFields()
        {
            NewWorkoutDate = DateTime.Today;
            NewWorkoutType = WorkoutType.Cardio;
            NewWorkoutDuration = 0;
            NewWorkoutCalories = 0;
            NewWorkoutNotes = string.Empty;

            OnPropertyChanged(nameof(NewWorkoutDate));
            OnPropertyChanged(nameof(NewWorkoutType));
            OnPropertyChanged(nameof(NewWorkoutDuration));
            OnPropertyChanged(nameof(NewWorkoutCalories));
            OnPropertyChanged(nameof(NewWorkoutNotes));
        }
    }
}
