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
    public class EmailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Emails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Emails.Include(e => e.EmailType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .Include(e => e.EmailType)
                .FirstOrDefaultAsync(m => m.EmlKey == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            ViewData["EmlEltKey"] = new SelectList(_context.EmailTypes, "EltKey", "EltKey");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmlKey,EmlEltKey,EmlAddress,EmlPrimary,EmlAddDate,EmlModifiedDate,EmlAddUser,EmlModifiedUser")] Email email)
        {
            if (ModelState.IsValid)
            {
                email.EmlKey = Guid.NewGuid();
                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmlEltKey"] = new SelectList(_context.EmailTypes, "EltKey", "EltKey", email.EmlEltKey);
            return View(email);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            ViewData["EmlEltKey"] = new SelectList(_context.EmailTypes, "EltKey", "EltKey", email.EmlEltKey);
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmlKey,EmlEltKey,EmlAddress,EmlPrimary,EmlAddDate,EmlModifiedDate,EmlAddUser,EmlModifiedUser")] Email email)
        {
            if (id != email.EmlKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.EmlKey))
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
            ViewData["EmlEltKey"] = new SelectList(_context.EmailTypes, "EltKey", "EltKey", email.EmlEltKey);
            return View(email);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .Include(e => e.EmailType)
                .FirstOrDefaultAsync(m => m.EmlKey == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var email = await _context.Emails.FindAsync(id);
            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(Guid id)
        {
            return _context.Emails.Any(e => e.EmlKey == id);
        }
    }
}
