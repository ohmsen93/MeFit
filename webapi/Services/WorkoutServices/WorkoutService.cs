using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.WorkoutServices
{
    public class WorkoutService : IWorkoutService
    {
        private readonly MeFitContext _context;

        public WorkoutService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Workout> Create(Workout entity)
        {
            _context.Workouts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var Workout = await _context.Workouts.FindAsync(id);

            if (Workout == null)
            {
                throw new EntityNotFoundException(id, nameof(Workout));
            }
            _context.Workouts.Remove(Workout);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Workout>> GetAll()
        {
            var Workout = await _context.Workouts
                    .Include(x => x.Exercises)
                    //.Include(x => x.Goals)
                    .ToListAsync();

            return Workout;
        }

        public async Task<Workout> GetById(int id)
        {
            var Workout = await _context.Workouts
                    .Include(x => x.Exercises)
                    //.Include(x => x.Goals)
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (Workout == null)
            {
                throw new EntityNotFoundException(id, nameof(Workout));
            }

            return Workout;
        }

        public async Task<Workout> Update(Workout entity)
        {
            var foundWorkout = await _context.Workouts.AnyAsync(x => x.Id == entity.Id);
            if (!foundWorkout)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Workout));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateWorkoutExercises(int WorkoutId, List<int> exercisesId)
        {
            var foundWorkout = await _context.Workouts.AnyAsync(x => x.Id == WorkoutId);
            if (!foundWorkout)
            {
                throw new EntityNotFoundException(WorkoutId, nameof(Workout));
            }
            // Finding the Workout with its Exercises
            var WorkoutToUpdateExercises = await _context.Workouts
                .Include(m => m.Exercises)
                .Where(m => m.Id == WorkoutId)
                .FirstAsync();
            // Loop through Exercises, try and assign to Workout
            var exercises = new List<Exercise>();
            foreach (var id in exercisesId)
            {
                var exercise = await _context.Exercises.FindAsync(id);
                if (exercise == null)
                    // Record doesnt exist: https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms229021(v=vs.100)?redirectedfrom=MSDN
                    throw new KeyNotFoundException($"Exercise with {id} not found");
                exercises.Add(exercise);
            }
            WorkoutToUpdateExercises.Exercises = exercises;
            _context.Entry(WorkoutToUpdateExercises).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
