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
    public class StatusStateTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusStateTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StatusStateTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusStateTypes.ToListAsync());
        }

        // GET: StatusStateTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusStateType = await _context.StatusStateTypes
                .FirstOrDefaultAsync(m => m.Cu6Key == id);
            if (statusStateType == null)
            {
                return NotFound();
            }

            return View(statusStateType);
        }

        // GET: StatusStateTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusStateTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cu6Key,Cu6Code,Cu6AddDate,Cu6ChangeDate,Cu6AddUser,Cu6ChangeUser,RowVersion")] StatusStateType statusStateType)
        {
            if (ModelState.IsValid)
            {
                statusStateType.Cu6Key = Guid.NewGuid();
                _context.Add(statusStateType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusStateType);
        }

        // GET: StatusStateTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusStateType = await _context.StatusStateTypes.FindAsync(id);
            if (statusStateType == null)
            {
                return NotFound();
            }
            return View(statusStateType);
        }

        // POST: StatusStateTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Cu6Key,Cu6Code,Cu6AddDate,Cu6ChangeDate,Cu6AddUser,Cu6ChangeUser,RowVersion")] StatusStateType statusStateType)
        {
            if (id != statusStateType.Cu6Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusStateType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusStateTypeExists(statusStateType.Cu6Key))
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
            return View(statusStateType);
        }

        // GET: StatusStateTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusStateType = await _context.StatusStateTypes
                .FirstOrDefaultAsync(m => m.Cu6Key == id);
            if (statusStateType == null)
            {
                return NotFound();
            }

            return View(statusStateType);
        }

        // POST: StatusStateTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var statusStateType = await _context.StatusStateTypes.FindAsync(id);
            _context.StatusStateTypes.Remove(statusStateType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusStateTypeExists(Guid id)
        {
            return _context.StatusStateTypes.Any(e => e.Cu6Key == id);
        }
    }
}
