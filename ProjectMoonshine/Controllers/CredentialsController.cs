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
    public class CredentialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Credentials
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Credentials.Include(c => c.CredentialType).Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Credentials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credential = await _context.Credentials
                .Include(c => c.CredentialType)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Cr1Key == id);
            if (credential == null)
            {
                return NotFound();
            }

            return View(credential);
        }

        // GET: Credentials/Create
        public IActionResult Create()
        {
            ViewData["Cr1Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential");
            ViewData["Cr1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cr1Key,Cr1Ct1Key,Cr1CusKey,Cr1AddUser,Cr1AddDate,Cr1ChangeUser,Cr1ChangeDate")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                credential.Cr1Key = Guid.NewGuid();
                _context.Add(credential);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cr1Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Key", credential.Cr1Ct1Key);
            ViewData["Cr1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", credential.Cr1CusKey);
            return View(credential);
        }

        // GET: Credentials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credential = await _context.Credentials.FindAsync(id);
            if (credential == null)
            {
                return NotFound();
            }
            ViewData["Cr1Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Key", credential.Cr1Ct1Key);
            ViewData["Cr1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", credential.Cr1CusKey);
            return View(credential);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Cr1Key,Cr1Ct1Key,Cr1CusKey,Cr1AddUser,Cr1AddDate,Cr1ChangeUser,Cr1ChangeDate")] Credential credential)
        {
            if (id != credential.Cr1Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credential);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialExists(credential.Cr1Key))
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
            ViewData["Cr1Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Key", credential.Cr1Ct1Key);
            ViewData["Cr1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", credential.Cr1CusKey);
            return View(credential);
        }

        // GET: Credentials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credential = await _context.Credentials
                .Include(c => c.CredentialType)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Cr1Key == id);
            if (credential == null)
            {
                return NotFound();
            }

            return View(credential);
        }

        // POST: Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credential = await _context.Credentials.FindAsync(id);
            _context.Credentials.Remove(credential);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialExists(Guid id)
        {
            return _context.Credentials.Any(e => e.Cr1Key == id);
        }
    }
}
