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
    public class DegreeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DegreeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DegreeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DegreeTypes.ToListAsync());
        }

        // GET: DegreeTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeType = await _context.DegreeTypes
                .FirstOrDefaultAsync(m => m.DetKey == id);
            if (degreeType == null)
            {
                return NotFound();
            }

            return View(degreeType);
        }

        // GET: DegreeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DegreeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetKey,DetCode,DetDescription,DetOrder,DetAddDate,DetChangeDate,DetAddUser,DetChangeUser")] DegreeType degreeType)
        {
            if (ModelState.IsValid)
            {
                degreeType.DetKey = Guid.NewGuid();
                _context.Add(degreeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(degreeType);
        }

        // GET: DegreeTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeType = await _context.DegreeTypes.FindAsync(id);
            if (degreeType == null)
            {
                return NotFound();
            }
            return View(degreeType);
        }

        // POST: DegreeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DetKey,DetCode,DetDescription,DetOrder,DetAddDate,DetChangeDate,DetAddUser,DetChangeUser")] DegreeType degreeType)
        {
            if (id != degreeType.DetKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degreeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeTypeExists(degreeType.DetKey))
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
            return View(degreeType);
        }

        // GET: DegreeTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeType = await _context.DegreeTypes
                .FirstOrDefaultAsync(m => m.DetKey == id);
            if (degreeType == null)
            {
                return NotFound();
            }

            return View(degreeType);
        }

        // POST: DegreeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var degreeType = await _context.DegreeTypes.FindAsync(id);
            _context.DegreeTypes.Remove(degreeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeTypeExists(Guid id)
        {
            return _context.DegreeTypes.Any(e => e.DetKey == id);
        }
    }
}
