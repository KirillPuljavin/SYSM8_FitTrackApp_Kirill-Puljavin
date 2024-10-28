using Fit_Track_App.Classes;
using Fit_Track_App.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Fit_Track_App.Pages
{
    internal class WorkoutsPageViewModel : ViewModelBase
    {
        private readonly WorkoutService _workoutService;

        // Collection of workouts, retrieved directly from the WorkoutService
        public ObservableCollection<DataManagement.Workout> Workouts => _workoutService.Workouts;

        // Filtered view to enable sorting and filtering
        public ICollectionView FilteredWorkouts { get; private set; }

        private DataManagement.Workout _selectedWorkout;
        public DataManagement.Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
                CanEditOrRemove = value != null;
            }
        }

        // Flag for enabling edit/remove buttons
        public bool CanEditOrRemove { get; private set; }

        // Filter and sort properties
        public string FilterType { get; set; }
        public DateTime? FilterDateFrom { get; set; }
        public DateTime? FilterDateTo { get; set; }
        public string SortOption { get; set; }

        // Properties for new workout entry
        public DateTime NewWorkoutDate { get; set; } = new DateTime(2024, 1, 1);
        public string NewWorkoutType { get; set; }
        public TimeSpan NewWorkoutDuration { get; set; }
        public int NewWorkoutCalories { get; set; }
        public string NewWorkoutNotes { get; set; }

        // Commands for adding, editing, removing, filtering, and clearing filters
        public ICommand AddWorkoutCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand EditWorkoutCommand { get; }
        public ICommand ApplyFilterCommand { get; }
        public ICommand ClearFilterCommand { get; }

        public WorkoutsPageViewModel()
        {
            _workoutService = new WorkoutService();

            // Initialize filtered view and configure filter
            FilteredWorkouts = CollectionViewSource.GetDefaultView(Workouts);
            FilteredWorkouts.Filter = WorkoutFilter;

            // Initialize commands with lambdas to handle object parameters
            AddWorkoutCommand = new RelayCommand(_ => AddWorkout(), _ => CanAddWorkout());
            RemoveWorkoutCommand = new RelayCommand(_ => RemoveWorkout(), _ => CanEditOrRemove);
            EditWorkoutCommand = new RelayCommand(_ => EditWorkout(), _ => CanEditOrRemove);
            ApplyFilterCommand = new RelayCommand(_ => ApplyFilter());
            ClearFilterCommand = new RelayCommand(_ => ClearFilter());
        }

        private bool WorkoutFilter(object obj)
        {
            if (obj is not DataManagement.Workout workout)
                return false;

            bool matchesType = string.IsNullOrEmpty(FilterType) || workout.Type.Contains(FilterType, StringComparison.OrdinalIgnoreCase);
            bool matchesDateFrom = !FilterDateFrom.HasValue || workout.Date >= FilterDateFrom.Value;
            bool matchesDateTo = !FilterDateTo.HasValue || workout.Date <= FilterDateTo.Value;

            return matchesType && matchesDateFrom && matchesDateTo;
        }

        private void ApplyFilter()
        {
            FilteredWorkouts.Refresh();
            ApplySorting();
        }

        private void ClearFilter()
        {
            FilterType = string.Empty;
            FilterDateFrom = null;
            FilterDateTo = null;
            SortOption = string.Empty;
            FilteredWorkouts.Refresh();
        }

        private void ApplySorting()
        {
            FilteredWorkouts.SortDescriptions.Clear();
            if (SortOption == "Date")
                FilteredWorkouts.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
            else if (SortOption == "Type")
                FilteredWorkouts.SortDescriptions.Add(new SortDescription("Type", ListSortDirection.Ascending));
            else if (SortOption == "Duration")
                FilteredWorkouts.SortDescriptions.Add(new SortDescription("Duration", ListSortDirection.Descending));
        }

        private bool CanAddWorkout()
        {
            return !string.IsNullOrWhiteSpace(NewWorkoutType)
                && NewWorkoutDuration > TimeSpan.Zero
                && NewWorkoutCalories > 0;
        }

        private void AddWorkout()
        {
            // Validate fields before creating a workout
            if (!CanAddWorkout())
            {
                MessageBox.Show("Please fill in all workout details correctly.");
                return;
            }

            // Create the new workout
            var newWorkout = _workoutService.CreateWorkout(NewWorkoutDate, NewWorkoutType, NewWorkoutDuration, NewWorkoutCalories, NewWorkoutNotes);
            if (newWorkout != null)
            {
                // Clear input fields for the next entry
                ClearWorkoutInputFields();

                // Refresh filtered collection in case of active filters or sorting
                FilteredWorkouts.Refresh();
            }
        }

        private void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                _workoutService.DeleteWorkout(SelectedWorkout);
                SelectedWorkout = null;
                FilteredWorkouts.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a workout to remove.");
            }
        }

        private void EditWorkout()
        {
            if (SelectedWorkout != null)
            {
                SelectedWorkout.Date = NewWorkoutDate;
                SelectedWorkout.Type = NewWorkoutType;
                SelectedWorkout.Duration = NewWorkoutDuration;
                SelectedWorkout.CaloriesBurned = NewWorkoutCalories;
                SelectedWorkout.Notes = NewWorkoutNotes;

                _workoutService.UpdateWorkout(SelectedWorkout);
                FilteredWorkouts.Refresh();
                ClearWorkoutInputFields();
            }
            else
            {
                MessageBox.Show("Please select a workout to edit.");
            }
        }

        private void ClearWorkoutInputFields()
        {
            NewWorkoutDate = new DateTime(2024, 1, 1);
            NewWorkoutType = string.Empty;
            NewWorkoutDuration = TimeSpan.Zero;
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
