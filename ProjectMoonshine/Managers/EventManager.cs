using System.Linq;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class EventManager
    {
        private readonly ApplicationDbContext _context;
        public EventManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Event> GetAllEvents(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var events = _context.Events.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                events = events.Where(s => s.EvtName.ToLower().Contains(searchString)
                                       || s.EvtDescription.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "eventName":
                    {
                        events = events.OrderBy(x => x.EvtName);
                        break;
                    }
                case "eventName_desc":
                    {
                        events = events.OrderByDescending(x => x.EvtName);
                        break;
                    }
                case "description":
                    {
                        events = events.OrderBy(x => x.EvtDescription);
                        break;
                    }
                case "description_desc":
                    {
                        events = events.OrderByDescending(x => x.EvtDescription);
                        break;
                    }
                case "startDate":
                    {
                        events = events.OrderBy(x => x.EvtStartDate);
                        break;
                    }
                case "startDate_desc":
                    {
                        events = events.OrderByDescending(x => x.EvtStartDate);
                        break;
                    }
                case "endDate":
                    {
                        events = events.OrderBy(x => x.EvtEndDate);
                        break;
                    }
                case "endDate_desc":
                    {
                        events = events.OrderByDescending(x => x.EvtEndDate);
                        break;
                    }
                case "addDate":
                    {
                        events = events.OrderBy(x => x.EvtAddDate);
                        break;
                    }
                case "addDate_desc":
                    {
                        events = events.OrderByDescending(x => x.EvtAddDate);
                        break;
                    }
                default:
                    {
                        events = events.OrderBy(x => x.EvtName);
                        break;
                    }
            }
            return events;
        }
    }
}
