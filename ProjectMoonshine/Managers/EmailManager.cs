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
    public class EmailManager
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
        public IQueryable<Email> GetAllEmails(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var emails = context.Emails.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                emails = emails.Where(s => s.EmlAddress.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "address":
                    {
                        emails = emails.OrderBy(x => x.EmlAddress);
                        break;
                    }
                case "address_desc":
                    {
                        emails = emails.OrderByDescending(x => x.EmlAddress ?? "zzzz");
                        break;
                    }
                case "primary":
                    {
                        emails = emails.OrderBy(x => x.EmlIsPrimaryEmail);
                        break;
                    }
                case "primary_desc":
                    {
                        emails = emails.OrderByDescending(x => x.EmlIsPrimaryEmail);
                        break;
                    }
                case "work":
                    {
                        emails = emails.OrderBy(x => x.EmlIsWorkEmail);
                        break;
                    }
                case "work_desc":
                    {
                        emails = emails.OrderByDescending(x => x.EmlIsWorkEmail);
                        break;
                    }
                case "personal":
                    {
                        emails = emails.OrderBy(x => x.EmlIsPersonalEmail);
                        break;
                    }
                case "personal_desc":
                    {
                        emails = emails.OrderByDescending(x => x.EmlIsPersonalEmail);
                        break;
                    }
                case "addDate":
                    {
                        emails = emails.OrderBy(x => x.EmlAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        emails = emails.OrderByDescending(x => x.EmlAddDate);
                        break;
                    }
                default:
                    {
                        emails = emails.OrderBy(x => x.EmlAddress);
                        break;
                    }
            }
            return emails;
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
