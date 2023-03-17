using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.GoalServices
{
    public class GoalService:IGoalService
    {
        private readonly MeFitContext _context;

        public GoalService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Goal> Create(Goal entity)
        {
            _context.Goals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Goal> Create(Goal entity, List<int> workouts)
        {
            // Set default values for the goal
            entity.FkStatusId = 2;// Pending
            entity.FkUserProfileId = 3;

            // Create Goal
            _context.Goals.Add(entity);
            await _context.SaveChangesAsync();

            var goal = await _context.Goals.FindAsync(entity.Id);

            if (goal == null)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Goal));
            }

            var goalWorkoutList = new List<GoalWorkouts>();

            if (goal.FkTrainingprogramId != null)
            {
                var foundTrainingprogram = await _context.Trainingprograms.Include(tp => tp.Workouts).FirstOrDefaultAsync(tp => tp.Id == goal.FkTrainingprogramId);

                if (foundTrainingprogram == null)
                {
                    throw new EntityNotFoundException(foundTrainingprogram.Id, nameof(Trainingprogram));
                }

                foreach (var workout in foundTrainingprogram.Workouts)
                {

                    goalWorkoutList.Add(new GoalWorkouts { FkGoalId = entity.Id, FkWorkoutId = workout.Id, FkStatusId = 2 });
                }
            }
            else {
                // Create GoalWorkouts           
                foreach (var id in workouts)
                {                    
                    var workout = await _context.Workouts.FindAsync(id);

                    if (workout == null)
                        throw new KeyNotFoundException($"Workout with {id} not found");

                    goalWorkoutList.Add(new GoalWorkouts { FkGoalId = entity.Id, FkWorkoutId = id, FkStatusId = 2 });
                }
            }
            
            _context.GoalWorkouts.AddRange(goalWorkoutList);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteById(int id)
        {
            var goal = await _context.Goals.FindAsync(id);

            if (goal == null)
            {
                throw new EntityNotFoundException(id, nameof(Goal));
            }
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Goal>> GetCompletedGoals(int id)
        {
            var userProfile = await _context.UserProfiles.Where(u => u.FkUserId == id).FirstOrDefaultAsync();

            if (userProfile == null)
            {
                throw new EntityNotFoundException(id, nameof(UserProfile));
            }

            var achivedGoals = await _context.Goals
                .Include(g => g.GoalWorkouts)
                .Include(g => g.FkTrainingprogram)
                .Where(x => x.FkStatusId == 1).ToListAsync();

            return achivedGoals;
        }

        public async Task<ICollection<Goal>> GetAll()
        {
            return await _context.Goals
                .Include(g => g.GoalWorkouts)
                .Include(g=>g.FkTrainingprogram)
                .ToListAsync();
        }

        public async Task<Goal> GetById(int id)
        {
            var goal = await _context.Goals
                .Include(g => g.GoalWorkouts)
                .Include(g => g.FkTrainingprogram)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (goal == null)
            {
                throw new EntityNotFoundException(id, nameof(Goal));
            }
            return goal;
        }

        public async Task<ICollection<Workout>> GetGoalCompletedWorkouts(int id)
        {
            var goal = await _context.Goals.Include(x => x.GoalWorkouts.Where(x => x.FkStatusId == 2)).FirstOrDefaultAsync(x => x.Id == id);

            if (goal == null)
            {
                throw new EntityNotFoundException(id, nameof(goal));
            }

            var completedWorkouts = new List<Workout>();

            foreach (var item in goal.GoalWorkouts)
            {
                var workout = await _context.Workouts.FindAsync(item.FkWorkoutId);
                if (workout == null)
                    throw new KeyNotFoundException($"Workout with {id} not found");

                completedWorkouts.Add(workout);
            }

            return completedWorkouts;
        }

        public async Task<ICollection<Workout>> GetGoalWorkouts(int id)
        {
            var goal = await _context.Goals.Include(x => x.GoalWorkouts).FirstOrDefaultAsync(x => x.Id == id);

            if (goal == null)
            {
                throw new EntityNotFoundException(id, nameof(goal));
            }

            var workouts = new List<Workout>();

            foreach (var item in goal.GoalWorkouts)
            {
                var workout = await _context.Workouts.FindAsync(item.FkWorkoutId);
                if (workout == null)
                    throw new KeyNotFoundException($"Workout with id {id} not found");

                workouts.Add(workout);
            }

            return workouts;
        }

        public async Task<Goal> Update(Goal entity)
        {
            var foundGoal = await _context.Goals.AnyAsync(x => x.Id == entity.Id);
            if (!foundGoal)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Goal));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
