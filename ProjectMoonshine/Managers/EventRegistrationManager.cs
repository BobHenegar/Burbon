using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var eventRegistrations = _context.EventRegistrations.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                eventRegistrations = eventRegistrations.Where(s => s.PerLastName.ToLower().Contains(searchString)
                                       || s.PerFirstName.ToLower().Contains(searchString)
                                       || s.PerMiddleName.ToLower().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "firstName":
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.Event.EvtDescription);
                        break;
                    }
                default:
                    {
                        eventRegistrations = eventRegistrations.OrderBy(x => x.PerFirstName);
                        break;
                    }
            }
            return eventRegistrations;
        }
    }
}
