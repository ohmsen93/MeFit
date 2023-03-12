using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.ContributionrequestServices
{
    public class ContributionrequestService : IContributionrequestService
    {
        private readonly MeFitContext _context;

        public ContributionrequestService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Contributionrequest> Create(Contributionrequest entity)
        {
            _context.Contributionrequests.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var contributionrequest = await _context.Contributionrequests.FindAsync(id);

            if (contributionrequest == null)
            {
                throw new EntityNotFoundException(id, nameof(Contributionrequest));
            }
            _context.Contributionrequests.Remove(contributionrequest);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Contributionrequest>> GetAll()
        {
            return await _context.Contributionrequests.Include(x => x.FkUserProfile).ToListAsync();
        }

        public async Task<Contributionrequest> GetById(int id)
        {
           var contributionrequest = await _context.Contributionrequests.Include(x=>x.FkUserProfile).FirstOrDefaultAsync(x => x.Id == id);

            if (contributionrequest == null)
            {
                throw new EntityNotFoundException(id,nameof(Contributionrequest));
            }
            return contributionrequest;
        }

        public async Task<Contributionrequest> Update(Contributionrequest entity)
        {
            var foundContributionrequest = await _context.Contributionrequests.AnyAsync(x => x.Id == entity.Id);
            if (!foundContributionrequest)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Contributionrequest));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
