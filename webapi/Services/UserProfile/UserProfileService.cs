﻿using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly MeFitContext _context;
        public UserProfileService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Models.UserProfile> Create(Models.UserProfile entity)
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
                throw new EntityNotFoundExeption(id, nameof(Models.UserProfile));
            }
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Models.UserProfile>> GetAll()
        {
            return await _context.UserProfiles
                .Include(x => x.Workouts)
                .Include(x => x.Goals).ToListAsync();
        }

        public async Task<Models.UserProfile> GetById(int id)
        {
            var userProfile = await _context.UserProfiles
                .Include(x => x.Workouts)
                .Include(x => x.Goals).FirstOrDefaultAsync(x => x.Id == id);

            if (userProfile == null)
            {
                throw new EntityNotFoundExeption(id, nameof(Models.UserProfile));
            }
            return userProfile;
        }

        public async Task<Models.UserProfile> Update(Models.UserProfile entity)
        {
            var foundUserProfile = await _context.UserProfiles.AnyAsync(x => x.Id == entity.Id);
            if (!foundUserProfile)
            {
                throw new EntityNotFoundExeption(entity.Id, nameof(Models.UserProfile));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
