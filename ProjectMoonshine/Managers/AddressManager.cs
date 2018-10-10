using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public class AddressManager : IAddressManager
    {
        private readonly ApplicationDbContext context;
        public AddressManager(ApplicationDbContext _context)
        {
            context = _context;
        }

        // <inheritdoc />
        public async Task<Guid> AddPersonAddressAsync(Guid perKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> DeleteAddressAsync(Guid perKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<IList<Address>> GetAllAddressesAsync(int page, int resultsPerPage)
        {
            var result = await context
                            .Addresses
                            .Include(a => a.Person)
                            .Skip(page * resultsPerPage)
                            .Take(resultsPerPage)
                            .ToListAsync();
            return result;
        }

        // <inheritdoc />
        public async Task<Address> GetAddressByIdAsync(Guid adrKey)
        {
            var result = await context.Addresses.FirstAsync(x => x.AdrKey == adrKey);
            return result;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAddressAsync(Guid perKey, AddressViewModel model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddAddressAsync(AddressViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
