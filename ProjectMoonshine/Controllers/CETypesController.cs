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
    public class CETypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CETypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CETypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CEType.ToListAsync());
        }

        // GET: CETypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEType = await _context.CEType
                .FirstOrDefaultAsync(m => m.CetKey == id);
            if (cEType == null)
            {
                return NotFound();
            }

            return View(cEType);
        }

        // GET: CETypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CETypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CetKey,CetCode,CetDescription")] CEType cEType)
        {
            if (ModelState.IsValid)
            {
                cEType.CetKey = Guid.NewGuid();
                _context.Add(cEType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cEType);
        }

        // GET: CETypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEType = await _context.CEType.FindAsync(id);
            if (cEType == null)
            {
                return NotFound();
            }
            return View(cEType);
        }

        // POST: CETypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CetKey,CetCode,CetDescription")] CEType cEType)
        {
            if (id != cEType.CetKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cEType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CETypeExists(cEType.CetKey))
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
            return View(cEType);
        }

        // GET: CETypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEType = await _context.CEType
                .FirstOrDefaultAsync(m => m.CetKey == id);
            if (cEType == null)
            {
                return NotFound();
            }

            return View(cEType);
        }

        // POST: CETypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cEType = await _context.CEType.FindAsync(id);
            _context.CEType.Remove(cEType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CETypeExists(Guid id)
        {
            return _context.CEType.Any(e => e.CetKey == id);
        }
    }
}
