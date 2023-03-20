using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.TrainingprogramServices
{
    public class TrainingprogramService : ITrainingprogramService
    {
        private readonly MeFitContext _context;

        public TrainingprogramService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Trainingprogram> Create(Trainingprogram entity)
        {
            _context.Trainingprograms.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var trainingprogram = await _context.Trainingprograms.FindAsync(id);

            if (trainingprogram == null)
            {
                throw new EntityNotFoundException(id, nameof(Trainingprogram));
            }
            _context.Trainingprograms.Remove(trainingprogram);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Trainingprogram>> GetAll()
        {
            var trainingprogram = await _context.Trainingprograms
                    .Include(x => x.Goals)
                    .Include(x => x.Categories)
                    .Include(x => x.Workouts)
                    .ToListAsync();

            return trainingprogram;
        }

        public async Task<Trainingprogram> GetById(int id)
        {
            var trainingprogram = await _context.Trainingprograms
                .Include(x => x.Goals)
                .Include(x => x.Categories)
                .Include(x => x.Workouts)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (trainingprogram == null)
            {
                throw new EntityNotFoundException(id, nameof(Trainingprogram));
            }

            return trainingprogram;
        }

        public async Task<Trainingprogram> Update(Trainingprogram entity)
        {
            var foundTrainingprogram = await _context.Trainingprograms.AnyAsync(x => x.Id == entity.Id);
            if (!foundTrainingprogram)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Trainingprogram));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateTrainingprogramWorkouts(int trainingprogramId, List<int> workoutsId)
        {
            var foundTrainingprogram = await _context.Trainingprograms.AnyAsync(x => x.Id == trainingprogramId);
            if (!foundTrainingprogram)
            {
                throw new EntityNotFoundException(trainingprogramId, nameof(Trainingprogram));
            }
            // Finding the Trainingprogram with its Workouts
            var trainingprogramToUpdateWorkouts = await _context.Trainingprograms
                .Include(m => m.Goals)
                .Where(m => m.Id == trainingprogramId)
                .FirstAsync();
            // Loop through Workouts, try and assign to Trainingprogram
            var workouts = new List<Workout>();
            foreach (var id in workoutsId)
            {
                var workout = await _context.Workouts.FindAsync(id);
                if (workout == null)
                    // Record doesnt exist: https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms229021(v=vs.100)?redirectedfrom=MSDN
                    throw new KeyNotFoundException($"workout with {id} not found");
                workouts.Add(workout);
            }
            trainingprogramToUpdateWorkouts.Workouts = workouts;
            _context.Entry(trainingprogramToUpdateWorkouts).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrainingprogramCategories(int trainingprogramId, List<int> categoriesId)
        {
            var foundTrainingprogram = await _context.Trainingprograms.AnyAsync(x => x.Id == trainingprogramId);
            if (!foundTrainingprogram)
            {
                throw new EntityNotFoundException(trainingprogramId, nameof(Trainingprogram));
            }
            // Finding the Trainingprogram with its Categories
            var trainingprogramToUpdateCategories = await _context.Trainingprograms
                .Include(m => m.Categories)
                .Where(m => m.Id == trainingprogramId)
                .FirstAsync();
            // Loop through Categories, try and assign to Trainingprogram
            var categories = new List<Category>();
            foreach (var id in categoriesId)
            {
                var set = await _context.Categories.FindAsync(id);
                if (set == null)
                    // Record doesnt exist: https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms229021(v=vs.100)?redirectedfrom=MSDN
                    throw new KeyNotFoundException($"set with {id} not found");
                categories.Add(set);
            }
            trainingprogramToUpdateCategories.Categories = categories;
            _context.Entry(trainingprogramToUpdateCategories).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}
