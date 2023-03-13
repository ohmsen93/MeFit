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
        public async Task<GoalWorkout> Create(GoalWorkout entity)
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
                throw new EntityNotFoundException(id, nameof(GoalWorkout));
            }
            _context.GoalWorkouts.Remove(goalWorkout);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<GoalWorkout>> GetAll()
        {
            return await _context.GoalWorkouts.ToListAsync(); ;
        }

        public async Task<GoalWorkout> GetById(int id)
        {
            var goalWorkout = await _context.GoalWorkouts.FirstOrDefaultAsync(x => x.Id == id);

            if (goalWorkout == null)
            {
                throw new EntityNotFoundException(id, nameof(GoalWorkout));
            }
            return goalWorkout;
        }

        public async Task<GoalWorkout> Update(GoalWorkout entity)
        {
            var foundGoalWorkout = await _context.GoalWorkouts.AnyAsync(x => x.Id == entity.Id);

            if (!foundGoalWorkout)
            {
                throw new EntityNotFoundException(entity.Id, nameof(GoalWorkout));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
