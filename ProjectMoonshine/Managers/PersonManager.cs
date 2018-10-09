using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Models;

namespace ProjectMoonshine.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IAddressManager addressManager;
        private readonly ApplicationDbContext context;
        public PersonManager(ApplicationDbContext _context,
            IAddressManager _addressManager)
        {
            context = _context;
            addressManager = _addressManager;
        }

        public async Task AddPersonAsync()
        {
            throw new NotImplementedException();
            await context.Persons.AddAsync(new Person()
            {
                //Implement Person Here
            });
            await context.SaveChangesAsync();
        }

        public async Task DeletePersonAsync(Guid Id)
        {
            context.Addresses.RemoveRange(context.Addresses.Where(x => x.AdrPerKey == Id));
            context.Emails.RemoveRange(context.Emails.Where(x => x.EmlPerKey == Id));
            context.Phones.RemoveRange(context.Phones.Where(x => x.PhoPerKey == Id));
            context.Persons.Remove(context.Persons.First(x => x.PerKey == Id));
            await context.SaveChangesAsync();
        }

        public async Task UpdatePersonAddressesAsync(Guid Id, PersonViewModel model)
        {

            var person = context.Persons.AsTracking()
                .Include(i => i.Addresses)
                .First(x => x.PerKey == Id);

            await context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(Guid Id, PersonViewModel model)
        {

            var person = context.Persons.AsTracking()
                .Include(i => i.Phones)
                .Include(i => i.Addresses)
                .Include(i => i.Customer)
                .Include(i => i.Emails)
                .First(x => x.PerKey == Id);

            await context.SaveChangesAsync();
        }

        public async Task UpdatePersonEmailsAsync(Guid Id, PersonViewModel model)
        {

            var person = context.Persons.AsTracking()
                .Include(i => i.Phones)
                .Include(i => i.Addresses)
                .Include(i => i.Customer)
                .Include(i => i.Emails)
                .First(x => x.PerKey == Id);

            await context.SaveChangesAsync();
        }

        public async Task UpdatePersonPhonesAsync(Guid Id, PersonViewModel model)
        {

            var person = context.Persons.AsTracking()
                .Include(i => i.Phones)
                .Include(i => i.Addresses)
                .Include(i => i.Customer)
                .Include(i => i.Emails)
                .First(x => x.PerKey == Id);

            await context.SaveChangesAsync();
        }
    }
}
