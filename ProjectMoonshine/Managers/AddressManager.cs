using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers.Interfaces;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public class AddressManager : IAddressManager
    {
        private readonly ApplicationDbContext context;
        public AddressManager(ApplicationDbContext _context)
        {
            context = _context;
        }

        // <inheritdoc />
        public async Task<Guid> AddPersonAddressAsync(Guid perKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> DeleteAddressAsync(Guid perKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public IQueryable<Address> GetAllAddresses(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var addresses = context.Addresses.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                addresses = addresses.Where(s => s.AdrCity.ToLower().Contains(searchString)
                                       || s.AdrState.ToLower().Contains(searchString)
                                       || s.AdrAddress1.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "city":
                    {
                        addresses = addresses.OrderBy(x => x.AdrCity ?? "zzzz");
                        break;
                    }
                case "city_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrCity);
                        break;
                    }
                case "state":
                    {
                        addresses = addresses.OrderBy(x => x.AdrState ?? "zzzz");
                        break;
                    }
                case "state_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrState);
                        break;
                    }
                case "zip":
                    {
                        addresses = addresses.OrderBy(x => x.AdrZip ?? "zzzz");
                        break;
                    }
                case "zip_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrZip);
                        break;
                    }
                case "adr1":
                    {
                        addresses = addresses.OrderBy(x => x.AdrAddress1 ?? "zzzz");
                        break;
                    }
                case "adr1_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrAddress1);
                        break;
                    }
                case "adr2":
                    {
                        addresses = addresses.OrderBy(x => x.AdrAddress2 ?? "zzzz");
                        break;
                    }
                case "adr2_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrAddress2);
                        break;
                    }
                case "adr3":
                    {
                        addresses = addresses.OrderBy(x => x.AdrAddress3 ?? "zzzz");
                        break;
                    }
                case "adr3_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrAddress3);
                        break;
                    }
                case "primary":
                    {
                        addresses = addresses.OrderBy(x => x.AdrIsPrimaryAddress);
                        break;
                    }
                case "primary_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrIsPrimaryAddress);
                        break;
                    }
                case "mailing":
                    {
                        addresses = addresses.OrderBy(x => x.AdrIsMailingAddress);
                        break;
                    }
                case "mailing_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrIsMailingAddress);
                        break;
                    }
                case "work":
                    {
                        addresses = addresses.OrderBy(x => x.AdrIsWorkAddress);
                        break;
                    }
                case "work_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrIsWorkAddress);
                        break;
                    }
                case "home":
                    {
                        addresses = addresses.OrderBy(x => x.AdrIsHomeAddress);
                        break;
                    }
                case "home_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrIsHomeAddress);
                        break;
                    }
                case "addDate":
                    {
                        addresses = addresses.OrderBy(x => x.AdrAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        addresses = addresses.OrderByDescending(x => x.AdrAddDate);
                        break;
                    }
                default:
                    {
                        addresses = addresses.OrderBy(x => x.AdrCity);
                        break;
                    }
            }
            return addresses;
        }

        // <inheritdoc />
        public async Task<Address> GetAddressByIdAsync(Guid adrKey)
        {
            var result = await context.Addresses.FirstAsync(x => x.AdrKey == adrKey);
            return result;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAddressAsync(Guid perKey, AddressViewModel model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddAddressAsync(AddressViewModel model)
        {
            throw new NotImplementedException();
        }

    }
}
