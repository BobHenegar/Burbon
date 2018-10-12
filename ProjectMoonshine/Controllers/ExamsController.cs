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
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExamManager _examManager;

        public ExamsController(ApplicationDbContext context,
            ExamManager examManager)
        {
            _examManager = examManager;
            _context = context;
        }

        // GET: Exams
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

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.ExmKey == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewData["ExmEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExmKey,ExmEvtKey,ExmSeatCount,ExmCanRetake,ExmName,ExmAddDate,ExmChangeDate,ExmAddUser,ExmChangeUser,RowVersion")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                exam.ExmKey = Guid.NewGuid();
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExmEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName", exam.ExmEvtKey);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["ExmEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName", exam.ExmEvtKey);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExmKey,ExmEvtKey,ExmSeatCount,ExmName,ExmCanRetake,ExmAddDate,ExmChangeDate,ExmAddUser,ExmChangeUser,RowVersion")] Exam exam)
        {
            if (id != exam.ExmKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.ExmKey))
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
            ViewData["ExmEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName", exam.ExmEvtKey);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.ExmKey == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(Guid id)
        {
            return _context.Exams.Any(e => e.ExmKey == id);
        }
    }
}
