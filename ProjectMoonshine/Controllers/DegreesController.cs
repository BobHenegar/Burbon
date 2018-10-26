using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Controllers
{
    public class DegreesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DegreesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Degrees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Degrees.Include(d => d.Customer).Include(d => d.DegreeType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Degrees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees
                .Include(d => d.Customer)
                .Include(d => d.DegreeType)
                .FirstOrDefaultAsync(m => m.DegKey == id);
            if (degree == null)
            {
                return NotFound();
            }

            return View(degree);
        }

        // GET: Degrees/Create
        public IActionResult Create()
        {
            ViewData["DegCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");
            ViewData["DegDetKey"] = new SelectList(_context.DegreeTypes, "DetKey", "DetCode");
            return View();
        }

        // POST: Degrees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DegKey,DegCusKey,DegInstitution,DegDate,DegAddUser,DegAddDate,DegChangeUser,DegChangeDate,DegDetKey")] Degree degree)
        {
            if (ModelState.IsValid)
            {
                if (await degree.AddAsync(_context))
                    return RedirectToAction(nameof(Index));
            }
            ViewData["DegCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", degree.DegCusKey);
            ViewData["DegDetKey"] = new SelectList(_context.DegreeTypes, "DetKey", "DetCode", degree.DegDetKey);
            return View(degree);
        }

        // GET: Degrees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees.FindAsync(id);
            if (degree == null)
            {
                return NotFound();
            }
            ViewData["DegCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", degree.DegCusKey);
            ViewData["DegDetKey"] = new SelectList(_context.DegreeTypes, "DetKey", "DetCode", degree.DegDetKey);
            return View(degree);
        }

        // POST: Degrees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DegKey,DegCusKey,DegInstitution,DegDate,DegAddUser,DegAddDate,DegChangeUser,DegChangeDate,DegDetKey")] Degree degree)
        {
            if (id != degree.DegKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await degree.UpdateAsync(_context)) return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeExists(degree.DegKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewData["DegCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", degree.DegCusKey);
            ViewData["DegDetKey"] = new SelectList(_context.DegreeTypes, "DetKey", "DetCode", degree.DegDetKey);
            return View(degree);
        }

        // GET: Degrees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees
                .Include(d => d.Customer)
                .Include(d => d.DegreeType)
                .FirstOrDefaultAsync(m => m.DegKey == id);
            if (degree == null)
            {
                return NotFound();
            }

            return View(degree);
        }

        // POST: Degrees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var degree = await _context.Degrees.FindAsync(id);
            _context.Degrees.Remove(degree);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeExists(Guid id)
        {
            return _context.Degrees.Any(e => e.DegKey == id);
        }
    }
}
