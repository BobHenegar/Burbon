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
    public class AddressTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddressTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddressTypes.ToListAsync());
        }

        // GET: AddressTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressTypes
                .FirstOrDefaultAsync(m => m.AdtKey == id);
            if (addressType == null)
            {
                return NotFound();
            }

            return View(addressType);
        }

        // GET: AddressTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdtKey,AdtType,AdtAddDate,AdtModifiedDate,AdtAddUser,AdtModifiedUser")] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                addressType.AdtKey = Guid.NewGuid();
                _context.Add(addressType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressType);
        }

        // GET: AddressTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressTypes.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }
            return View(addressType);
        }

        // POST: AddressTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdtKey,AdtType,AdtAddDate,AdtModifiedDate,AdtAddUser,AdtModifiedUser")] AddressType addressType)
        {
            if (id != addressType.AdtKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressTypeExists(addressType.AdtKey))
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
            return View(addressType);
        }

        // GET: AddressTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressType = await _context.AddressTypes
                .FirstOrDefaultAsync(m => m.AdtKey == id);
            if (addressType == null)
            {
                return NotFound();
            }

            return View(addressType);
        }

        // POST: AddressTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var addressType = await _context.AddressTypes.FindAsync(id);
            _context.AddressTypes.Remove(addressType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressTypeExists(Guid id)
        {
            return _context.AddressTypes.Any(e => e.AdtKey == id);
        }
    }
}
