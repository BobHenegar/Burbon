using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class ExamManager
    {
        private readonly ApplicationDbContext _context;
        public ExamManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Exam> GetAllExams(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var people = _context
                .Exams
                .Include(i => i.Event)
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                people = people.Where(s => s.Event.EvtName.ToLower().Contains(searchString)
                                       || s.Event.EvtDescription.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "eventName":
                    {
                        people = people.OrderByDescending(x => x.Event.EvtName);
                        break;
                    }
                case "eventName_Desc":
                    {
                        people = people.OrderByDescending(x => x.Event.EvtName);
                        break;
                    }
                case "addDate":
                    {
                        people = people.OrderBy(x => x.ExmAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        people = people.OrderByDescending(x => x.ExmAddDate);
                        break;
                    }
                default:
                    {
                        people = people.OrderByDescending(x => x.ExmAddDate);
                        break;
                    }
            }
            return people;
        }
    }
}
