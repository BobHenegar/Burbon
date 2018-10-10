using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public class PhoneManager : IPhoneManager
    {
        private readonly ApplicationDbContext context;
        public PhoneManager(ApplicationDbContext _context)
        {
            context = _context;
        }

        // <inheritdoc />
        public Task<Guid> AddPhoneAsync(PhoneViewModel model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddPhoneAsync(Credential model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> DeletePhoneAsync(Guid phoKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<IList<Phone>> GetAllPhonesAsync(int page, int resultsPerPage)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Phone> GetPhoneByIdAsync(Guid adrKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> UpdatePhoneAsync(Guid phoKey, PhoneViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
