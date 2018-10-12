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
    public class CEPeriodsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CEPeriodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CEPeriods
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CEPeriods.Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CEPeriods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEPeriod = await _context.CEPeriods
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Ce2Key == id);
            if (cEPeriod == null)
            {
                return NotFound();
            }

            return View(cEPeriod);
        }

        // GET: CEPeriods/Create
        public IActionResult Create()
        {
            ViewData["Ce2CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");
            return View();
        }

        // POST: CEPeriods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ce2Key,Ce2CusKey,Ce2StartDate,Ce2EndDate,Ce2AddDate,Ce2ChangeDate,Ce2AddUser,Ce2ChangeUser")] CEPeriod cEPeriod)
        {
            if (ModelState.IsValid)
            {
                cEPeriod.Ce2Key = Guid.NewGuid();
                _context.Add(cEPeriod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ce2CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", cEPeriod.Ce2CusKey);
            return View(cEPeriod);
        }

        // GET: CEPeriods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEPeriod = await _context.CEPeriods.FindAsync(id);
            if (cEPeriod == null)
            {
                return NotFound();
            }
            ViewData["Ce2CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", cEPeriod.Ce2CusKey);
            return View(cEPeriod);
        }

        // POST: CEPeriods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ce2Key,Ce2CusKey,Ce2StartDate,Ce2EndDate,Ce2AddDate,Ce2ChangeDate,Ce2AddUser,Ce2ChangeUser")] CEPeriod cEPeriod)
        {
            if (id != cEPeriod.Ce2Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cEPeriod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CEPeriodExists(cEPeriod.Ce2Key))
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
            ViewData["Ce2CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", cEPeriod.Ce2CusKey);
            return View(cEPeriod);
        }

        // GET: CEPeriods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEPeriod = await _context.CEPeriods
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Ce2Key == id);
            if (cEPeriod == null)
            {
                return NotFound();
            }

            return View(cEPeriod);
        }

        // POST: CEPeriods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cEPeriod = await _context.CEPeriods.FindAsync(id);
            _context.CEPeriods.Remove(cEPeriod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CEPeriodExists(Guid id)
        {
            return _context.CEPeriods.Any(e => e.Ce2Key == id);
        }
    }
}
