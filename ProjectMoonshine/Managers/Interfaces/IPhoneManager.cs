using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers.Interfaces
{
    public interface IPhoneManager
    {
        /// <summary>
        /// Gets All phones with database side pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        IQueryable<Phone> GetAllPhones(string searchString, string sortOrder);

        /// <summary>
        /// Adds a new phone based on the view model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddPhoneAsync(PhoneViewModel model);

        /// <summary>
        /// Deletes a phone based on the given Key
        /// </summary>
        /// <param name="phoKey"></param>
        /// <returns></returns>
        Task<bool> DeletePhoneAsync(Guid phoKey);

        /// <summary>
        /// Gets a phones based on the Id
        /// </summary>
        /// <param name="adrKey"></param>
        /// <returns></returns>
        Task<Phone> GetPhoneByIdAsync(Guid adrKey);

        /// <summary>
        /// Updates a phone based on the model and ID
        /// </summary>
        /// <param name="phoKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdatePhoneAsync(Guid phoKey, PhoneViewModel model);
    }
}
