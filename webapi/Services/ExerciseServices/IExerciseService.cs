using webapi.Models;

namespace webapi.Services.ExerciseServices
{
    public interface IExerciseService : IServices<Exercise, int>
    {
        public Task<Exercise> Create(Exercise entity, List<int> setsId, List<int> musclegroupsId);
        public Task UpdateExerciseSets(int exerciseId, List<int> setsId);

        public Task UpdateExerciseMusclegroups(int exerciseId, List<int> musclegroupsId);

    }
}
