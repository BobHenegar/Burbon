using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public interface IPersonManager
    {
        Task<IList<Person>> GetAllPeopleAsync(string searchString, string sortOrder);
        Task<Guid> AddPersonAsync(PersonViewModel model);
        Task<bool> DeletePersonAsync(Guid perKey);
        Task<Person> GetPersonByIdAsync(Guid adrKey);
        Task<bool> UpdatePersonAsync(Guid perKey, PersonViewModel model);
    }
}
