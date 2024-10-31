using Fit_Track_App.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Fit_Track_App.ViewModels
{
    public class WorkoutChangeViewModel : INotifyPropertyChanged
    {
        private DateTime _date;
        private WorkoutType _type;
        private int _durationMinutes;
        private int _caloriesBurned;
        private string _notes;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<WorkoutType> WorkoutTypes { get; } = Enum.GetValues(typeof(WorkoutType)) as WorkoutType[];

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        public WorkoutType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        public int DurationMinutes
        {
            get => _durationMinutes;
            set { _durationMinutes = value; OnPropertyChanged(); }
        }

        public int CaloriesBurned
        {
            get => _caloriesBurned;
            set { _caloriesBurned = value; OnPropertyChanged(); }
        }

        public string Notes
        {
            get => _notes;
            set { _notes = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public Action CloseAction { get; set; }
        public bool DialogResult { get; private set; }

        internal WorkoutChangeViewModel(DataManagement.Workout workout)
        {
            Date = workout.Date;
            Type = Enum.TryParse(workout.Type, out WorkoutType parsedType) ? parsedType : WorkoutType.Cardio;
            DurationMinutes = (int)workout.Duration.TotalMinutes;
            CaloriesBurned = workout.CaloriesBurned;
            Notes = workout.Notes;

            SaveCommand = new RelayCommand(_ => SaveChanges(workout));
            CancelCommand = new RelayCommand(_ => CancelChanges());
        }

        private void SaveChanges(DataManagement.Workout workout)
        {
            workout.Date = Date;
            workout.Type = Type.ToString();
            workout.Duration = TimeSpan.FromMinutes(DurationMinutes);
            workout.CaloriesBurned = CaloriesBurned;
            workout.Notes = Notes;

            DialogResult = true;
            CloseAction?.Invoke();
        }

        private void CancelChanges()
        {
            DialogResult = false;
            CloseAction?.Invoke();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
