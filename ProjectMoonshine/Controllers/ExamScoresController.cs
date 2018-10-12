using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Controllers
{
    public class ExamScoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExamManager _examManager;

        public ExamScoresController(ApplicationDbContext context,
            ExamManager examManager)
        {
            _examManager = examManager;
            _context = context;
        }

        // GET: ExamScores
        public async Task<IActionResult> Index(string sortOrder, string lastOrder, string currentFilter, string searchString, int? page)
        {
            int currentPage = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "firstName";
                ViewBag.LastSortParam = sortOrder;
            }
            else
            {
                if (sortOrder == lastOrder)
                {
                    int index = sortOrder.IndexOf("_desc");
                    string cleanPath = (index < 0)
                        ? sortOrder = sortOrder + "_desc"
                        : sortOrder.Remove(index, "_desc".Length);
                }
            }
            ViewBag.LastSortParam = sortOrder;
            return View(_examManager.GetAllExams(searchString, sortOrder).ToPagedList(currentPage, GlobalConstants.PageSize));
        }

        // GET: ExamScores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examScore = await _context.ExamScores
                .Include(e => e.Customer)
                .Include(e => e.Exam)
                .FirstOrDefaultAsync(m => m.ExsKey == id);
            if (examScore == null)
            {
                return NotFound();
            }

            return View(examScore);
        }

        // GET: ExamScores/Create
        public IActionResult Create()
        {
            ViewData["ExsCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");
            ViewData["ExsExmKey"] = new SelectList(_context.Exams, "ExmKey", "ExmKey");
            return View();
        }

        // POST: ExamScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExsKey,ExsExmKey,ExsCusKey,ExsTotalPossible,ExsTotalReceived,ExsAddDate,ExsChangeDate,ExsAddUser,ExsChangeUser,RowVersion")] ExamScore examScore)
        {
            if (ModelState.IsValid)
            {
                examScore.ExsKey = Guid.NewGuid();
                _context.Add(examScore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExsCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", examScore.ExsCusKey);
            ViewData["ExsExmKey"] = new SelectList(_context.Exams, "ExmKey", "ExmKey", examScore.ExsExmKey);
            return View(examScore);
        }

        // GET: ExamScores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examScore = await _context.ExamScores.FindAsync(id);
            if (examScore == null)
            {
                return NotFound();
            }
            ViewData["ExsCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", examScore.ExsCusKey);
            ViewData["ExsExmKey"] = new SelectList(_context.Exams, "ExmKey", "ExmKey", examScore.ExsExmKey);
            return View(examScore);
        }

        // POST: ExamScores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExsKey,ExsExmKey,ExsCusKey,ExsTotalPossible,ExsTotalReceived,ExsAddDate,ExsChangeDate,ExsAddUser,ExsChangeUser,RowVersion")] ExamScore examScore)
        {
            if (id != examScore.ExsKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examScore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamScoreExists(examScore.ExsKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExsCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", examScore.ExsCusKey);
            ViewData["ExsExmKey"] = new SelectList(_context.Exams, "ExmKey", "ExmKey", examScore.ExsExmKey);
            return View(examScore);
        }

        // GET: ExamScores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examScore = await _context.ExamScores
                .Include(e => e.Customer)
                .Include(e => e.Exam)
                .FirstOrDefaultAsync(m => m.ExsKey == id);
            if (examScore == null)
            {
                return NotFound();
            }

            return View(examScore);
        }

        // POST: ExamScores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var examScore = await _context.ExamScores.FindAsync(id);
            _context.ExamScores.Remove(examScore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamScoreExists(Guid id)
        {
            return _context.ExamScores.Any(e => e.ExsKey == id);
        }
    }
}
