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
    public class PhonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Phones.Include(p => p.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PhoKey == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            ViewData["PhoPerKey"] = new SelectList(_context.People.Select(s => new { s.PerKey, Text = s.PerLastName + ", " + s.PerFirstName }), "PerKey", "Text");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoKey,PhoPerKey,PhoNumber,PhoExtention,PhoIsPrimary,PhoIsHome,PhoIsMobile,PhoIsWork,PhoAddDate,PhoChangeDate,PhoAddUser,PhoChangeUser")] Phone phone)
        {
            if (ModelState.IsValid)
            {
                phone.PhoKey = Guid.NewGuid();
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", phone.PhoPerKey);
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["PhoPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", phone.PhoPerKey);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PhoKey,PhoPerKey,PhoNumber,PhoExtention,PhoIsPrimary,PhoIsHome,PhoIsMobile,PhoIsWork,PhoAddDate,PhoChangeDate,PhoAddUser,PhoChangeUser")] Phone phone)
        {
            if (id != phone.PhoKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneExists(phone.PhoKey))
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
            ViewData["PhoPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", phone.PhoPerKey);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PhoKey == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var phone = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(Guid id)
        {
            return _context.Phones.Any(e => e.PhoKey == id);
        }
    }
}
