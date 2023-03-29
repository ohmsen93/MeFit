using webapi.Models;

namespace webapi.Services.WorkoutServices
{
    public interface IWorkoutService : IServices<Workout, int>
    {
        Task<UserProfile> GetUserProfile(string id);
        Task<Workout> Create(Workout entity, List<int> exercises, int userprofileId);
        public Task UpdateWorkoutExercises(int WorkoutId, List<int> exercisesId);

        //public Task<ICollection<Workout>> GetAllNoCustom();
        public Task<ICollection<Workout>> GetAll(string userId);

        public Task<ICollection<Workout>> GetWorkoutsByTrainingprogramId(int id);

    }
}
