using webapi.Models;

namespace webapi.Services.TrainingprogramServices
{
    public interface ITrainingprogramService : IServices<Trainingprogram, int>
    {
        public Task<Trainingprogram> Create(Trainingprogram entity, List<int> workouts, List<int> categories);
        public Task UpdateTrainingprogramWorkouts(int trainingprogramId, List<int> workoutIds);

        public Task UpdateTrainingprogramCategories(int trainingprogramId, List<int> categoryIds);

    }
}
