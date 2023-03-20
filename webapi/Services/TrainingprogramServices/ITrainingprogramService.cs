using webapi.Models;

namespace webapi.Services.TrainingprogramServices
{
    public interface ITrainingprogramService : IServices<Trainingprogram, int>
    {
        public Task UpdateTrainingprogramWorkouts(int trainingprogramId, List<int> workoutIds);

        public Task UpdateTrainingprogramCategories(int trainingprogramId, List<int> categoryIds);

    }
}
