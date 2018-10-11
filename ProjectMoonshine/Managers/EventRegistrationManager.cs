using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.Managers.Interfaces;

namespace ProjectMoonshine.Managers
{
    public class EventRegistrationManager : IEventRegistrationManager
    {
        private readonly ApplicationDbContext _context;
        public EventRegistrationManager(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
