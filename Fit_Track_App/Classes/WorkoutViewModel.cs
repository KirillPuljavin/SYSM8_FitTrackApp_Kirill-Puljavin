using Fit_Track_App.Classes;
using System.Collections.ObjectModel;

namespace Fit_Track_App.ViewModels
{
    internal class WorkoutViewModel : ViewModelBase
    {
        private static WorkoutViewModel _instance;
        public static WorkoutViewModel Instance => _instance ??= new WorkoutViewModel();
        public ObservableCollection<DataManagement.Workout> Workouts => UserViewModel.Instance.LoggedInUser?.Workouts;

        private WorkoutViewModel()
        {
            LoadWorkouts();
        }

        private DataManagement.Workout[] preloadedWorkouts = new DataManagement.Workout[]
        {
            new DataManagement.CardioWorkout(System.DateTime.Now, "Cardio", new System.TimeSpan(0, 30, 0), 200, "Preloaded Cardio Workout"),
            new DataManagement.StrengthWorkout(System.DateTime.Now, "Strength", new System.TimeSpan(1, 0, 0), 300, "Preloaded Strength Workout")
        };
        public void LoadWorkouts()
        {
            Workouts.Clear();
            foreach (var workout in preloadedWorkouts)
            {
                Workouts.Add(workout);
            }
        }

        public void AddWorkout(DateTime date, WorkoutType type, TimeSpan duration, int caloriesBurned, string notes)
        {
            DataManagement.Workout newWorkout = type switch
            {
                WorkoutType.Cardio => new DataManagement.CardioWorkout(date, type.ToString(), duration, caloriesBurned, notes),
                WorkoutType.Strength => new DataManagement.StrengthWorkout(date, type.ToString(), duration, caloriesBurned, notes),
                _ => throw new ArgumentException("Invalid workout type")
            };

            Workouts?.Add(newWorkout);
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
