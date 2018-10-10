using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class AddressesController : Controller
    {
        private readonly IAddressManager _addressManager;
        private readonly ApplicationDbContext _context;
        public AddressesController(ApplicationDbContext context,
            IAddressManager addressManager)
        {
            _context = context;
            _addressManager = addressManager;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Addresses.Include(a => a.AdrPerson);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.AdrPerson)
                .FirstOrDefaultAsync(m => m.AdrKey == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["AdrPerKey"] = new SelectList(_context.People.Select(s => new { s.PerKey, Text = s.PerLastName + ", " + s.PerFirstName}), "PerKey", "Text");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddressViewModel address)
        {
            if (ModelState.IsValid)
            {
                address.AdrKey = Guid.NewGuid();
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdrPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", address.AdrPerKey);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["AdrPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", address.AdrPerKey);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AddressViewModel address)
        {
            if (id != address.AdrKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AdrKey))
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
            ViewData["AdrPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", address.AdrPerKey);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.AdrPerson)
                .FirstOrDefaultAsync(m => m.AdrKey == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(Guid id)
        {
            return _context.Addresses.Any(e => e.AdrKey == id);
        }
    }
}
