using webapi.Models;

namespace webapi.Services.GoalServices
{
    public interface IGoalService:IServices<Goal,int>
    {
        Task<Goal> Create(Goal entity,List<int> workouts);
        Task<ICollection<Goal>> GetCompletedGoals(int id);
        Task<ICollection<Workout>> GetGoalWorkouts(int id);
        Task<ICollection<Workout>> GetGoalCompletedWorkouts(int id);
        
    }
}
