using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public interface ICustomerManager
    {
        Task<IList<Customer>> GetAllCustomersAsync(int page, int resultsPerPage);
        Task<Guid> AddCustomerAsync(CustomerViewModel model);
        Task<bool> DeleteCustomerAsync(Guid cusKey);
        Task<Customer> GetCustomerByIdAsync(Guid adrKey);
        Task<bool> UpdateCustomerAsync(Guid cusKey, CustomerViewModel model);
    }
}
