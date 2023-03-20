using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly MeFitContext _context;

        public AddressService(MeFitContext context)
        {
            _context = context;
        }
        public async Task<Address> Create(Address entity)
        {
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteById(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                throw new EntityNotFoundException(id, nameof(Address));
            }
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
           var address = await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id);

            if (address == null)
            {
                throw new EntityNotFoundException(id,nameof(Set));
            }
            return address;
        }

        public async Task<Address> Update(Address entity)
        {
            var foundAddress = await _context.Addresses.AnyAsync(x => x.Id == entity.Id);
            if (!foundAddress)
            {
                throw new EntityNotFoundException(entity.Id, nameof(Address));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
