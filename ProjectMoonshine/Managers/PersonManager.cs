using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class PersonManager
    {
        private readonly ApplicationDbContext context;
        public PersonManager(ApplicationDbContext _context)
        {
            context = _context;
        }
        // <inheritdoc />
        public Task<Guid> AddPersonAsync(Person model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<bool> DeletePersonAsync(Guid perKey)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public IQueryable<Person> GetAllPeople(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var people = context.People.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                people = people.Where(s => s.PerLastName.ToLower().Contains(searchString)
                                       || s.PerFirstName.ToLower().Contains(searchString)
                                       || s.PerMiddleName.ToLower().Contains(searchString)
                                       || (s.PerFirstName + " " + s.PerLastName).ToLower().Contains(searchString));
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
                case "lastName":
                    {
                        people = people.OrderBy(x => x.PerLastName);
                        break;
                    }
                case "lastName_desc":
                    {
                        people = people.OrderByDescending(x => x.PerLastName);
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
                        people = people.OrderBy(x => x.PerLastName);
                        break;
                    }
            }
            return people;
        }

        // <inheritdoc />
        public Task<Person> GetPersonByIdAsync(Guid adrKey)
        {
            throw new NotImplementedException();
        }

    }
}
