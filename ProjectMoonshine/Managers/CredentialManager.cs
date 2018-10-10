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
    public class CredentialManager : ICredentialManager
    {
        //private readonly ApplicationDbContext context;
        //public CredentialManager(ApplicationDbContext _context)
        //{
        //    context = _context;
        //}

        //public async Task<bool> DeleteCredentialAsync(Guid cr1Key)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IList<Credential>> GetAllCredentialsAsync(int page, int resultsPerPage)
        //{
        //    var result = await context
        //                       .Credentials
        //                       .Skip(page * resultsPerPage)
        //                       .Take(resultsPerPage)
        //                       .ToListAsync();
        //    return result;
        //}

        //public async Task<bool> UpdateCredentialAsync(Guid cr1Key, CredentialViewModel model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
