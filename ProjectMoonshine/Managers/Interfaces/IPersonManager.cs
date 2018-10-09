using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.Models;

namespace ProjectMoonshine.Managers
{
    public interface IPersonManager
    {
        Task AddPersonAsync();
        Task DeletePersonAsync();
        Task UpdatePersonAsync(Guid Id, PersonViewModel model);
        Task UpdatePersonPhonesAsync(Guid Id, PersonViewModel model);
        Task UpdatePersonEmailsAsync(Guid Id, PersonViewModel model);
        Task UpdatePersonAddressesAsync(Guid Id, PersonViewModel model);
    }
}
