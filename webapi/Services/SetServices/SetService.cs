using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.SetServices
{
    public class SetService : ISetService
    {
        private readonly MeFitContext _context;

        public SetService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Set> Create(Set entity)
        {
            _context.Sets.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var set = await _context.Sets.FindAsync(id);

            if (set == null)
            {
                throw new EntityNotFoundException(id, nameof(Set));
            }
            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Set>> GetAll()
        {
            return await _context.Sets.Include(x => x.Exercises).ToListAsync();
        }

        public async Task<Set> GetById(int id)
        {
           var set = await _context.Sets.Include(x=>x.Exercises).FirstOrDefaultAsync(x => x.Id == id);

            if (set == null)
            {
                throw new EntityNotFoundException(id,nameof(Set));
            }
            return set;
        }

        public async Task<Set> Update(Set entity)
        {
            var foundSet = await _context.Sets.AnyAsync(x => x.Id == entity.Id);
            if (!foundSet)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Set));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
