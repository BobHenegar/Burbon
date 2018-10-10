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
    public class EmailManager : IEmailManager
    {
        private readonly ApplicationDbContext context;
        public EmailManager(ApplicationDbContext _context)
        {
            context = _context;
        }

        // <inheritdoc />
        public Task<Guid> AddEmailAsync(Credential model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddEmailAsync(EmailViewModel model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> DeleteEmailAsync(Guid emlKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<IList<Email>> GetAllEmailsAsync(int page, int resultsPerPage)
        {
            var result = await context
                            .Emails
                            .Skip(page * resultsPerPage)
                            .Take(resultsPerPage)
                            .ToListAsync();
            return result;
        }

        // <inheritdoc />
        public Task<Email> GetEmailByIdAsync(Guid adrKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> UpdateEmailAsync(Guid emlKey, EmailViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
