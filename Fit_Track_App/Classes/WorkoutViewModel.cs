using Fit_Track_App.Classes;
using System.Collections.ObjectModel;

namespace Fit_Track_App.ViewModels
{
    internal class WorkoutViewModel : ViewModelBase
    {
        private static WorkoutViewModel _instance;
        public static WorkoutViewModel Instance => _instance ??= new WorkoutViewModel();

        public ObservableCollection<DataManagement.Workout> Workouts { get; }

        private WorkoutViewModel()
        {
            Workouts = new ObservableCollection<DataManagement.Workout>();
        }

        public void AddWorkout(DateTime date, WorkoutType type, TimeSpan duration, int caloriesBurned, string notes)
        {
            DataManagement.Workout newWorkout = type switch
            {
                WorkoutType.Cardio => new DataManagement.CardioWorkout(date, "Cardio", duration, caloriesBurned, notes),

                WorkoutType.Strength => new DataManagement.StrengthWorkout(date, "Strength", duration, caloriesBurned, notes),
                _ => throw new ArgumentException("Invalid workout type")
            };

            Workouts.Add(newWorkout);
        }

        public void RemoveWorkout(DataManagement.Workout workout)
        {
            if (Workouts.Contains(workout))
            {
                Workouts.Remove(workout);
            }
        }
    }
}
