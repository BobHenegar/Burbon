using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class CustomerManager
    {
        private readonly ApplicationDbContext context;
        public CustomerManager(ApplicationDbContext _context)
        {
            context = _context;
        }

        // <inheritdoc />
        public Task<Guid> AddCustomerAsync(Credential model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddCustomerAsync(Customer model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> DeleteCustomerAsync(Guid cusKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<IList<Customer>> GetAllCustomersAsync(int page, int resultsPerPage)
        {
            var result = await context
                            .Customers
                            .Skip(page * resultsPerPage)
                            .Take(resultsPerPage)
                            .ToListAsync();
            return result;
        }

        // <inheritdoc />
        public Task<Customer> GetCustomerByIdAsync(Guid adrKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> UpdateCustomerAsync(Guid cusKey, Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
