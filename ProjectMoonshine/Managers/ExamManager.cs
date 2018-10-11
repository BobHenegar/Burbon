using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework;

namespace ProjectMoonshine.Managers
{
    public class ExamManager
    {
        private readonly ApplicationDbContext _context;
        public ExamManager(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
