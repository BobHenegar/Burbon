using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.Managers.Interfaces;

namespace ProjectMoonshine.Managers
{
    public class ExamScoreManager : IExamScoreManager
    {
        private readonly ApplicationDbContext _context;
        public ExamScoreManager(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
