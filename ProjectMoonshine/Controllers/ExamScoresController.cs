using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Controllers
{
    public class ExamScoresController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ExamScoreManager _examScoreManager;

        public ExamScoresController(ApplicationDbContext context,
            ExamScoreManager examScoreManager)
        {
            _examScoreManager = examScoreManager;
            _context = context;
        }

        // GET: ExamScores
        public IActionResult Index(string sortOrder, string lastOrder, string currentFilter, string searchString, int? page)
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
            return View(_examScoreManager.GetAllExamScores(searchString, sortOrder).ToPagedList(currentPage, GlobalConstants.PageSize));
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
                .ThenInclude(i => i.Person)
                .Include(e => e.Exam)
                .Where(x => x.ExsKey == id)
                .FirstOrDefaultAsync();
            if (examScore == null)
            {
                return NotFound();
            }

            return View(examScore);
        }

        // GET: ExamScores/Create
        public async Task<IActionResult> Create(Guid? id)
        {
            ViewData["CusKeyHasValue"] = id.HasValue;
            var query = _context
                .Customers
                .AsNoTracking()
                .Include(i => i.Person)
                .Include(i => i.EventRegistrations)
                .ThenInclude(i => i.Event)
                .ThenInclude(i => i.Exam)
                .AsQueryable();
            if(id.HasValue)
            {
                ViewData["CusKey"] = id.Value;
                query = query.Where(x => id.HasValue ? x.CusKey == id.Value : x.EventRegistrations.Count() > 0);
            }
            var customerlist = await query.Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }).ToListAsync();
            var examList = await _context.Exams.AsNoTracking().Select(s => new { s.ExmKey, Name = s.ExmName }).ToListAsync();

            ViewData["ExsCusKey"] = new SelectList(customerlist, "CusKey", "Name");
            ViewData["ExsExmKey"] = new SelectList(examList, "ExmKey", "Name");
            return View();
        }

        // POST: ExamScores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExamScore examScore)
        {
            if (ModelState.IsValid
                && await _context.Events
                    .AsNoTracking()
                    .Include(i => i.Exam)
                    .Include(i => i.EventRegistrations)
                    .AnyAsync(x => x.EvtExmKey == examScore.ExsExmKey
                              && x.EventRegistrations
                                       .Any(a => a.EvrCusKey == examScore.ExsCusKey)))
            {

                if (await examScore.AddAsync(_context))
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Error"] = "Customer is not registered for the selected exam";
            }
            ViewData["ExsCusKey"] = new SelectList(_context.Customers.Include(i => i.Person).Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }), "CusKey", "Name", examScore.ExsCusKey);
            ViewData["ExsExmKey"] = new SelectList(_context.Exams.Select(s => new { s.ExmKey, Name = s.ExmName }), "ExmKey", "Name", examScore.ExsExmKey);
            return View(examScore);
        }

        // GET: ExamScores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examScore = await _context.ExamScores.AsNoTracking().Where(x => x.ExsKey == id).FirstOrDefaultAsync();
            if (examScore == null)
            {
                return NotFound();
            }
            ViewData["ExsCusKey"] = new SelectList(_context.Customers.AsNoTracking().Include(i => i.Person).Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }), "CusKey", "Name", examScore.ExsCusKey);
            ViewData["ExsExmKey"] = new SelectList(_context.Exams.AsNoTracking().Select(s => new { s.ExmKey, Name = s.ExmName }), "ExmKey", "Name", examScore.ExsExmKey);
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
                    if ( await examScore.UpdateAsync(_context))
                        return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!ExamScoreExists(examScore.ExsKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Debug.WriteLine(e.RecursivelyFormatException());
                        if (Environment.UserInteractive)
                        {
                            Debugger.Break();
                        }
                        throw;
                    }
                }
            }
            ViewData["ExsCusKey"] = new SelectList(_context.Customers.Include(i => i.Person).Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }), "CusKey", "Name", examScore.ExsCusKey);
            ViewData["ExsExmKey"] = new SelectList(_context.Exams, "ExmKey", "ExmName");
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
                .Where(x => x.ExsKey == id)
                .FirstOrDefaultAsync();
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
