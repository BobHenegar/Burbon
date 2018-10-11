using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework;

namespace ProjectMoonshine.Managers
{
    public class EventManager
    {
        private readonly ApplicationDbContext _context;
        public EventManager(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
