using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.GaolWrokutServices
{
    public class GoalWorkoutService : IGoalWorkoutService
    {
        private readonly MeFitContext _context;

        public GoalWorkoutService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<GoalWorkouts> Create(GoalWorkouts entity)
        {
            _context.GoalWorkouts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var goalWorkout = await _context.GoalWorkouts.FindAsync(id);

            if (goalWorkout == null)
            {
                throw new EntityNotFoundException(id, nameof(GoalWorkouts));
            }
            _context.GoalWorkouts.Remove(goalWorkout);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<GoalWorkouts>> GetAll()
        {
            return await _context.GoalWorkouts.ToListAsync(); ;
        }

        public async Task<GoalWorkouts> GetById(int id)
        {
            var goalWorkout = await _context.GoalWorkouts.FirstOrDefaultAsync(x => x.Id == id);

            if (goalWorkout == null)
            {
                throw new EntityNotFoundException(id, nameof(GoalWorkouts));
            }
            return goalWorkout;
        }

        public async Task<GoalWorkouts> Update(GoalWorkouts entity)
        {
            var foundGoalWorkout = await _context.GoalWorkouts.AnyAsync(x => x.Id == entity.Id);

            if (!foundGoalWorkout)
            {
                throw new EntityNotFoundException(entity.Id, nameof(GoalWorkouts));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
