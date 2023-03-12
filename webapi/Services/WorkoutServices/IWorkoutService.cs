using webapi.Models;

namespace webapi.Services.WorkoutServices
{
    public interface IWorkoutService : IServices<Workout, int>
    {
        public Task UpdateWorkoutExercises(int WorkoutId, List<int> exercisesId);

    }
}
