using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.UserProfileServices
{
    public class UserProfileService : IUserProfileService
    {
        private readonly MeFitContext _context;
        public UserProfileService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<UserProfile> Create(UserProfile entity)
        {
            _context.UserProfiles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);

            if (userProfile == null)
            {
                throw new EntityNotFoundException(id, nameof(UserProfile));
            }
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }

        public Task ContributorRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<UserProfile>> GetAll()
        {
            return await _context.UserProfiles
                .Include(x => x.Workouts)
                .Include(x => x.Goals).ToListAsync();
        }

        public async Task<UserProfile> GetById(int id)
        {
            var userProfile = await _context.UserProfiles
                .Include(x => x.Workouts)
                .Include(x => x.Goals).FirstOrDefaultAsync(x => x.Id == id);

            if (userProfile == null)
            {
                throw new EntityNotFoundException(id, nameof(UserProfile));
            }
            return userProfile;
        }

        public async Task<UserProfile> Update(UserProfile entity)
        {
            var foundUserProfile = await _context.UserProfiles.AnyAsync(x => x.Id == entity.Id);
            if (!foundUserProfile)
            {
                throw new EntityNotFoundException(entity.Id, nameof(UserProfile));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
