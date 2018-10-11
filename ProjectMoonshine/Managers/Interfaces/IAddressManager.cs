using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public interface IAddressManager
    {
        IQueryable<Address> GetAllAddresses(string searchString, string sortOrder);
        Task<Guid> AddAddressAsync(AddressViewModel model);
        Task<bool> DeleteAddressAsync(Guid perKey);
        Task<Address> GetAddressByIdAsync(Guid adrKey);
        Task<bool> UpdateAddressAsync(Guid perKey, AddressViewModel model);
    }
}
