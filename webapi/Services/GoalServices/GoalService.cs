﻿using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.GoalServices
{
    public class GoalService:IGoalService
    {
        private readonly MeFitContext _context;

        public GoalService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Goal> Create(Goal entity)
        {
            _context.Goals.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var goal = await _context.Goals.FindAsync(id);

            if (goal == null)
            {
                throw new EntityNotFoundException(id, nameof(Goal));
            }
            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Goal>> GetAll()
        {
            return await _context.Goals.Include(x => x.Workouts).ToListAsync();
        }

        public async Task<Goal> GetById(int id)
        {
            var goal = await _context.Goals.Include(x => x.Workouts).FirstOrDefaultAsync(x => x.Id == id);

            if (goal == null)
            {
                throw new EntityNotFoundException(id, nameof(Goal));
            }
            return goal;
        }

        public async Task<Goal> Update(Goal entity)
        {
            var foundGoal = await _context.Goals.AnyAsync(x => x.Id == entity.Id);
            if (!foundGoal)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Goal));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
