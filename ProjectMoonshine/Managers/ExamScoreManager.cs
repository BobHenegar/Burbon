using System.Linq;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class ExamScoreManager
    {
        private readonly ApplicationDbContext _context;
        public ExamScoreManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ExamScore> GetAllPeople(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var people = _context.ExamScores.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                people = people.Where(s => s.Customer.Person.PerFirstName.ToLower().Contains(searchString)
                                       || s.Customer.Person.PerLastName.ToLower().Contains(searchString)
                                       || s.Customer.Person.PerMiddleName.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstName":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerFirstName);
                        break;
                    }
                case "firstName_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerFirstName);
                        break;
                    }
                case "middleName":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerMiddleName);
                        break;
                    }
                case "middleName_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerMiddleName);
                        break;
                    }
                case "lastName":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerLastName);
                        break;
                    }
                case "lastName_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerLastName);
                        break;
                    }
                case "dateOfBirth":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerDateOfBirth);
                        break;
                    }
                case "dateOfBirth_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerFirstName);
                        break;
                    }
                case "gender":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerGender);
                        break;
                    }
                case "gender_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerGender);
                        break;
                    }
                case "addDate":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerAddDate);
                        break;
                    }
                default:
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerFirstName);
                        break;
                    }
            }
            return people;
        }
    }
}
