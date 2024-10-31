using Fit_Track_App.Classes;
using Fit_Track_App.Pages;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class WorkoutTypeOption
    {
        public string DisplayName { get; set; }
        public WorkoutType? Value { get; set; }
    }

    internal class WorkoutsPageViewModel : ViewModelBase
    {
        public ObservableCollection<DataManagement.Workout> Workouts => WorkoutViewModel.Instance.Workouts;

        private ICollectionView _filteredWorkouts;
        public ICollectionView FilteredWorkouts
        {
            get => _filteredWorkouts;
            set
            {
                _filteredWorkouts = value;
                OnPropertyChanged(nameof(FilteredWorkouts));
            }
        }

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

        public string LoggedInUsername { get; } = UserViewModel.Instance.UserName;

        public DateTime NewWorkoutDate { get; set; } = DateTime.Today;
        public WorkoutType NewWorkoutType { get; set; } = WorkoutType.Cardio;
        public int NewWorkoutDuration { get; set; }
        public int NewWorkoutCalories { get; set; }
        public string NewWorkoutNotes { get; set; }


        public IEnumerable<WorkoutType> WorkoutTypes { get; }
        public IEnumerable<WorkoutTypeOption> WorkoutTypesWithAllOption { get; }

        private WorkoutType? _selectedFilterWorkoutType = null;
        public WorkoutType? SelectedFilterWorkoutType
        {
            get => _selectedFilterWorkoutType;
            set
            {
                _selectedFilterWorkoutType = value;
                OnPropertyChanged(nameof(SelectedFilterWorkoutType));
                ApplyFilters();
            }
        }

        private DateTime? _filterStartDate;
        public DateTime? FilterStartDate
        {
            get => _filterStartDate;
            set
            {
                _filterStartDate = value;
                OnPropertyChanged(nameof(FilterStartDate));
                ApplyFilters();
            }
        }

        private DateTime? _filterEndDate;
        public DateTime? FilterEndDate
        {
            get => _filterEndDate;
            set
            {
                _filterEndDate = value;
                OnPropertyChanged(nameof(FilterEndDate));
                ApplyFilters();
            }
        }

        private int? _filterDuration;
        public int? FilterDuration
        {
            get => _filterDuration;
            set
            {
                _filterDuration = value;
                OnPropertyChanged(nameof(FilterDuration));
                ApplyFilters();
            }
        }

        public ICommand AddWorkoutCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand ApplyFiltersCommand { get; }
        public ICommand OpenUserDetailsCommand { get; }
        public ICommand InfoCommand { get; }

        public bool CanEditOrRemove => SelectedWorkout != null;

        public WorkoutsPageViewModel()
        {
            AddWorkoutCommand = new RelayCommand(_ => AddWorkout(), _ => CanAddWorkout());
            RemoveWorkoutCommand = new RelayCommand(_ => RemoveWorkout(), _ => CanEditOrRemove);
            ApplyFiltersCommand = new RelayCommand(_ => ApplyFilters());
            OpenUserDetailsCommand = new RelayCommand(_ => OpenUserDetailsPage());
            InfoCommand = new RelayCommand(_ => ShowInfo());

            _filteredWorkouts = CollectionViewSource.GetDefaultView(Workouts);
            _filteredWorkouts.Filter = FilterWorkouts;

            WorkoutTypes = Enum.GetValues(typeof(WorkoutType)).Cast<WorkoutType>();
            WorkoutTypesWithAllOption = new List<WorkoutTypeOption>
            {
                new WorkoutTypeOption { DisplayName = "All", Value = null }
            }
            .Concat(WorkoutTypes.Select(wt => new WorkoutTypeOption { DisplayName = wt.ToString(), Value = wt }))
            .ToList();
        }

        private bool CanAddWorkout()
        {
            return NewWorkoutDuration > 0 && NewWorkoutCalories > 0;
        }

        private void AddWorkout()
        {
            if (!CanAddWorkout())
            {
                MessageBox.Show("Please fill in all workout details correctly.");
                return;
            }

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

        private void ApplyFilters()
        {
            FilteredWorkouts.Refresh();
        }

        private bool FilterWorkouts(object obj)
        {
            if (obj is DataManagement.Workout workout)
            {
                bool matchesType = !SelectedFilterWorkoutType.HasValue || workout.Type == SelectedFilterWorkoutType.Value.ToString();
                bool matchesStartDate = !FilterStartDate.HasValue || workout.Date >= FilterStartDate.Value;
                bool matchesEndDate = !FilterEndDate.HasValue || workout.Date <= FilterEndDate.Value;
                bool matchesDuration = !FilterDuration.HasValue || workout.Duration.TotalMinutes >= FilterDuration.Value;

                return matchesType && matchesStartDate && matchesEndDate && matchesDuration;
            }
            return false;
        }

        private void OpenUserDetailsPage()
        {
            if (Application.Current.MainWindow is MainWindow window)
            {
                window.MainFrame.Navigate(new UserDetailsPage());
            }
        }
        private void ShowInfo()
        {
            string infoMessage = "Welcome to Fit Tracker PRO!\n\n" +
                                 "In the left sidebar, you can filter your workouts list or access your account management features. " +
                                 "Simply select options to narrow down your workout records or click 'Manage Account' to view and update your profile.\n\n" +
                                 "The main section displays your workout list, where you can view details of each workout. " +
                                 "Click on any workout in the list to select it.\n\n" +
                                 "To add a new workout, fill in the fields below the list and click 'Add Workout'. " +
                                 "To remove a workout, select it from the list and click 'Remove Workout'.\n\n" +
                                 "Enjoy tracking your fitness progress with Fit Tracker PRO!";

            MessageBox.Show(infoMessage, "App Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
