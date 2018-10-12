using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<ExamScore> GetAllExamScores(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var people = _context
                .ExamScores
                .Include(i => i.Customer)
                .ThenInclude(i => i.Person)
                .Include(i => i.Exam)
                .ThenInclude(i => i.Event)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                people = people.Where(s => s.Customer.Person.PerFirstName.ToLower().Contains(searchString)
                    || s.Customer.Person.PerLastName.ToLower().Contains(searchString)
                    || s.Customer.Person.PerMiddleName.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "personName":
                    {
                        people = people.OrderBy(x => x.Customer.Person.PerFirstMiddleLast);
                        break;
                    }
                case "personName_desc":
                    {
                        people = people.OrderByDescending(x => x.Customer.Person.PerFirstMiddleLast);
                        break;
                    }
                case "eventName":
                    {
                        people = people.OrderByDescending(x => x.Exam.Event.EvtName);
                        break;
                    }
                case "eventName_desc":
                    {
                        people = people.OrderByDescending(x => x.Exam.Event.EvtName);
                        break;
                    }
                case "addDate":
                    {
                        people = people.OrderBy(x => x.ExsAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        people = people.OrderByDescending(x => x.ExsAddDate);
                        break;
                    }
                default:
                    {
                        people = people.OrderByDescending(x => x.ExsAddDate);
                        break;
                    }
            }
            return people;
        }
    }
}
