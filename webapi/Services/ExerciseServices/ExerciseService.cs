using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.ExerciseServices
{
    public class ExerciseService : IExerciseService
    {
        private readonly MeFitContext _context;

        public ExerciseService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Exercise> Create(Exercise entity)
        {
            _context.Exercises.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);

            if (exercise == null)
            {
                throw new EntityNotFoundException(id, nameof(Exercise));
            }
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Exercise>> GetAll()
        {
            var exercise = await _context.Exercises
                    .Include(x => x.Sets)
                    .Include(x => x.Musclegroups)
                    .Include(x => x.Workouts)
                    .ToListAsync();

            return exercise;
        }

        public async Task<Exercise> GetById(int id)
        {
            var exercise = await _context.Exercises
                .Include(x => x.Sets)
                .Include(x => x.Musclegroups)
                .Include(x => x.Workouts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exercise == null)
            {
                throw new EntityNotFoundException(id, nameof(Exercise));
            }

            return exercise;
        }

        public async Task<Exercise> Update(Exercise entity)
        {
            var foundExercise = await _context.Exercises.AnyAsync(x => x.Id == entity.Id);
            if (!foundExercise)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Exercise));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateExerciseSets(int exerciseId, List<int> setsId)
        {
            var foundExercise = await _context.Exercises.AnyAsync(x => x.Id == exerciseId);
            if (!foundExercise)
            {
                throw new EntityNotFoundException(exerciseId, nameof(Exercise));
            }
            // Finding the Exercise with its Sets
            var exerciseToUpdateSets = await _context.Exercises
                .Include(m => m.Sets)
                .Where(m => m.Id == exerciseId)
                .FirstAsync();
            // Loop through Sets, try and assign to Exercise
            var sets = new List<Set>();
            foreach (var id in setsId)
            {
                var set = await _context.Sets.FindAsync(id);
                if (set == null)
                    // Record doesnt exist: https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms229021(v=vs.100)?redirectedfrom=MSDN
                    throw new KeyNotFoundException($"set with {id} not found");
                sets.Add(set);
            }
            exerciseToUpdateSets.Sets = sets;
            _context.Entry(exerciseToUpdateSets).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExerciseMusclegroups(int exerciseId, List<int> musclegroupsId)
        {
            var foundExercise = await _context.Exercises.AnyAsync(x => x.Id == exerciseId);
            if (!foundExercise)
            {
                throw new EntityNotFoundException(exerciseId, nameof(Exercise));
            }
            // Finding the Exercise with its Musclegroups
            var exerciseToUpdateMusclegroups = await _context.Exercises
                .Include(m => m.Musclegroups)
                .Where(m => m.Id == exerciseId)
                .FirstAsync();
            // Loop through Musclegroups, try and assign to Exercise
            var musclegroups = new List<Musclegroup>();
            foreach (var id in musclegroupsId)
            {
                var set = await _context.Musclegroups.FindAsync(id);
                if (set == null)
                    // Record doesnt exist: https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms229021(v=vs.100)?redirectedfrom=MSDN
                    throw new KeyNotFoundException($"set with {id} not found");
                musclegroups.Add(set);
            }
            exerciseToUpdateMusclegroups.Musclegroups = musclegroups;
            _context.Entry(exerciseToUpdateMusclegroups).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}
