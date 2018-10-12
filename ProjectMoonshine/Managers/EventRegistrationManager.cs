using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Managers
{
    public class EventRegistrationManager
    {
        private readonly ApplicationDbContext _context;
        public EventRegistrationManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<EventRegistration> GetAllEventRegistration
            (string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var eventRegistrations = _context.EventRegistrations
                    .Include(i => i.Customer)
                        .ThenInclude(i => i.Person)
                    .Include(i => i.Event)
                    .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                eventRegistrations = eventRegistrations
                    .Where(s =>
                    s.Customer.Person.PerLastName.ToLower().Contains(searchString)
                        || s.Event.EvtName.ToLower().Contains(searchString)
                        || s.Event.EvtDescription.ToLower().Contains(searchString)
                        || s.Customer.Person.PerMiddleName.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "eventName":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtName);
                        break;
                    }
                case "eventName_desc":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtName);
                        break;
                    }
                case "personName":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Customer.Person.PerFirstMiddleLast);
                        break;
                    }
                case "personName_desc":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtName);
                        break;
                    }
                case "startDate":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtStartDate);
                        break;
                    }
                case "startDate_desc":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtStartDate);
                        break;
                    }
                case "dropDate":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtStartDate);
                        break;
                    }
                case "dropDate_desc":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtStartDate);
                        break;
                    }
                default:
                    {
                        eventRegistrations = eventRegistrations.OrderByDescending(x => x.EvrAddDate);
                        break;
                    }
            }
            return eventRegistrations;
        }
    }
}
