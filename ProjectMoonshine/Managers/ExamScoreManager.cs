using ProjectMoonshine.EntityFramework;

namespace ProjectMoonshine.Managers
{
    public class ExamScoreManager
    {
        private readonly ApplicationDbContext _context;
        public ExamScoreManager(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
