using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly MeFitContext _context;
        public UserService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new EntityNotFoundException(id, nameof(Set));
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await _context.Users.Include(x => x.UserProfiles).ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.Include(x => x.UserProfiles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new EntityNotFoundException(id, nameof(User));
            }
            return user;
        }

        public async Task<User> Update(User entity)
        {
            var foundUser = await _context.Users.AnyAsync(x => x.Id == entity.Id);
            if (!foundUser)
            {
                throw new EntityNotFoundException(entity.Id, nameof(User));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
