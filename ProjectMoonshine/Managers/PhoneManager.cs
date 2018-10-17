using System;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class PhoneManager
    {
        private readonly ApplicationDbContext context;
        public PhoneManager(ApplicationDbContext _context)
        {
            context = _context;
        }

        // <inheritdoc />
        public Task<Guid> AddPhoneAsync(Phone model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<Guid> AddPhoneAsync(Credential model)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public IQueryable<Phone> GetAllPhones(string searchString, string sortOrder)
        {//Defer query executing until our sorting and pagination is finished
            var people = context.Phones.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                people = people.Where(s => s.PhoNumber.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "number":
                    {
                        people = people.OrderBy(x => x.PhoNumber);
                        break;
                    }
                case "number_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoNumber);
                        break;
                    }
                case "ext":
                    {
                        people = people.OrderBy(x => x.PhoExtention);
                        break;
                    }
                case "ext_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoExtention);
                        break;
                    }
                case "primary":
                    {
                        people = people.OrderBy(x => x.PhoIsPrimary);
                        break;
                    }
                case "primary_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoIsPrimary);
                        break;
                    }
                case "work":
                    {
                        people = people.OrderBy(x => x.PhoIsWork);
                        break;
                    }
                case "work_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoIsWork);
                        break;
                    }
                case "home":
                    {
                        people = people.OrderBy(x => x.PhoIsHome);
                        break;
                    }
                case "home_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoIsHome);
                        break;
                    }
                case "mobile":
                    {
                        people = people.OrderBy(x => x.PhoIsMobile);
                        break;
                    }
                case "mobile_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoIsMobile);
                        break;
                    }
                case "addDate":
                    {
                        people = people.OrderBy(x => x.PhoAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        people = people.OrderByDescending(x => x.PhoAddDate);
                        break;
                    }
                default:
                    {
                        people = people.OrderBy(x => x.PhoNumber);
                        break;
                    }
            }
            return people;
        }
    }
}
