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
    public class CredentialTypeXCredentialProcessTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialTypeXCredentialProcessTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialTypeXCredentialProcessTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialTypeXCredentialProcessTypes.Include(c => c.CredentialProcessType).Include(c => c.CredentialType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialTypeXCredentialProcessTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes
                .Include(c => c.CredentialProcessType)
                .Include(c => c.CredentialType)
                .FirstOrDefaultAsync(m => m.Ct4Key == id);
            if (credentialTypeXCredentialProcessType == null)
            {
                return NotFound();
            }

            return View(credentialTypeXCredentialProcessType);
        }

        // GET: CredentialTypeXCredentialProcessTypes/Create
        public IActionResult Create()
        {
            ViewData["Ct4Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process");
            ViewData["Ct4Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential");
            return View();
        }

        // POST: CredentialTypeXCredentialProcessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ct4Key,Ct4Ct1Key,Ct4Ct2Key,Ct4Order,Ct4AddDate,Ct4ChangeDate,Ct4AddUser,Ct4ChangeUser")] CredentialTypeXCredentialProcessType credentialTypeXCredentialProcessType)
        {
            if (ModelState.IsValid)
            {
                credentialTypeXCredentialProcessType.Ct4Key = Guid.NewGuid();
                _context.Add(credentialTypeXCredentialProcessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ct4Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", credentialTypeXCredentialProcessType.Ct4Ct2Key);
            ViewData["Ct4Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential", credentialTypeXCredentialProcessType.Ct4Ct1Key);
            return View(credentialTypeXCredentialProcessType);
        }

        // GET: CredentialTypeXCredentialProcessTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes.FindAsync(id);
            if (credentialTypeXCredentialProcessType == null)
            {
                return NotFound();
            }
            ViewData["Ct4Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", credentialTypeXCredentialProcessType.Ct4Ct2Key);
            ViewData["Ct4Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential", credentialTypeXCredentialProcessType.Ct4Ct1Key);
            return View(credentialTypeXCredentialProcessType);
        }

        // POST: CredentialTypeXCredentialProcessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ct4Key,Ct4Ct1Key,Ct4Ct2Key,Ct4Order,Ct4AddDate,Ct4ChangeDate,Ct4AddUser,Ct4ChangeUser")] CredentialTypeXCredentialProcessType credentialTypeXCredentialProcessType)
        {
            if (id != credentialTypeXCredentialProcessType.Ct4Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialTypeXCredentialProcessType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialTypeXCredentialProcessTypeExists(credentialTypeXCredentialProcessType.Ct4Key))
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
            ViewData["Ct4Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", credentialTypeXCredentialProcessType.Ct4Ct2Key);
            ViewData["Ct4Ct1Key"] = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential", credentialTypeXCredentialProcessType.Ct4Ct1Key);
            return View(credentialTypeXCredentialProcessType);
        }

        // GET: CredentialTypeXCredentialProcessTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes
                .Include(c => c.CredentialProcessType)
                .Include(c => c.CredentialType)
                .FirstOrDefaultAsync(m => m.Ct4Key == id);
            if (credentialTypeXCredentialProcessType == null)
            {
                return NotFound();
            }

            return View(credentialTypeXCredentialProcessType);
        }

        // POST: CredentialTypeXCredentialProcessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes.FindAsync(id);
            _context.CredentialTypeXCredentialProcessTypes.Remove(credentialTypeXCredentialProcessType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialTypeXCredentialProcessTypeExists(Guid id)
        {
            return _context.CredentialTypeXCredentialProcessTypes.Any(e => e.Ct4Key == id);
        }
    }
}
