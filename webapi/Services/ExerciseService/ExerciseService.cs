using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.ExerciseService
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
                throw new EntityNotFoundExeption(id, nameof(Exercise));
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
                throw new EntityNotFoundExeption(id,nameof(Exercise));
            }

            return exercise;
        }

        public async Task<Exercise> Update(Exercise entity)
        {
            var foundExercise = await _context.Exercises.AnyAsync(x => x.Id == entity.Id);
            if (!foundExercise)
            {
                throw new EntityNotFoundExeption(entity.Id, nameof(Exercise));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
