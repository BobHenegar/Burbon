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
    public class PersonManager : IPersonManager
    {
        private readonly ApplicationDbContext context;
        public PersonManager(ApplicationDbContext _context)
        {
            context = _context;
        }
        // <inheritdoc />
        public Task<Guid> AddPersonAsync(Credential model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddPersonAsync(PersonViewModel model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<bool> DeletePersonAsync(Guid perKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<IList<Person>> GetAllPeopleAsync(int? page, string searchString, string sortOrder)
        {
            var toSkip = page.HasValue ? 0 : page.Value * GlobalConstants.PageSize;
            //Defer query executing until our sorting and pagination is finished
            var people = context.People.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                people = people.Where(s => s.PerLastName.ToLower().Contains(searchString)
                                       || s.PerFirstName.ToLower().Contains(searchString)
                                       || s.PerMiddleName.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstName":
                    {
                        people = people.OrderBy(x => x.PerFirstName);
                        break;
                    }
                case "firstName_desc":
                    {
                        people = people.OrderByDescending(x => x.PerFirstName);
                        break;
                    }
                case "middleName":
                    {
                        people = people.OrderBy(x => x.PerMiddleName);
                        break;
                    }
                case "middleName_desc":
                    {
                        people = people.OrderByDescending(x => x.PerMiddleName);
                        break;
                    }
                case "dateOfBirth":
                    {
                        people = people.OrderBy(x => x.PerDateOfBirth);
                        break;
                    }
                case "dateOfBirth_desc":
                    {
                        people = people.OrderByDescending(x => x.PerFirstName);
                        break;
                    }
                case "gender":
                    {
                        people = people.OrderBy(x => x.PerGender);
                        break;
                    }
                case "gender_desc":
                    {
                        people = people.OrderByDescending(x => x.PerGender);
                        break;
                    }
                case "addDate":
                    {
                        people = people.OrderBy(x => x.PerAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        people = people.OrderByDescending(x => x.PerAddDate);
                        break;
                    }
                default:
                    {
                        people = people.OrderBy(x => x.PerFirstName);
                        break;
                    }
            }
            var result = await people.Skip(toSkip).Take(GlobalConstants.PageSize).ToListAsync();
            return result;
        }

        // <inheritdoc />
        public Task<Person> GetPersonByIdAsync(Guid adrKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public async Task<bool> UpdatePersonAsync(Guid perKey, PersonViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
