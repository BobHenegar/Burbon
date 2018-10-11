using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers.Interfaces
{
    public interface IEmailManager
    {
        IQueryable<Email> GetAllEmails(string searchString, string sortOrder);
        Task<Guid> AddEmailAsync(EmailViewModel model);
        Task<bool> DeleteEmailAsync(Guid emlKey);
        Task<Email> GetEmailByIdAsync(Guid adrKey);
        Task<bool> UpdateEmailAsync(Guid emlKey, EmailViewModel model);
    }
}
