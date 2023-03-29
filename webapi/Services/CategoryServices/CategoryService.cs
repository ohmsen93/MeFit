using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly MeFitContext _context;
        public CategoryService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(Category entity)
        {
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                throw new EntityNotFoundException(id, nameof(Category));
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Categories.Include(x => x.Trainingprograms).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.Include(x=>x.Trainingprograms).FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                throw new EntityNotFoundException(id, nameof(Category));
            }
            return category;
        }

        public async Task<Category> Update(Category entity)
        {
            var foundCategory = await _context.Categories.AnyAsync(x => x.Id == entity.Id);
            if (!foundCategory)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Category));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
