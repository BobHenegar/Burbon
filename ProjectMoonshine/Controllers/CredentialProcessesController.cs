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
    public class CredentialProcessesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialProcessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialProcesses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialProcesses.Include(c => c.Credential).Include(c => c.StatusType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialProcesses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcess = await _context.CredentialProcesses
                .Include(c => c.Credential)
                .Include(c => c.StatusType)
                .FirstOrDefaultAsync(m => m.Cr2Key == id);
            if (credentialProcess == null)
            {
                return NotFound();
            }

            return View(credentialProcess);
        }

        // GET: CredentialProcesses/Create
        public IActionResult Create()
        {
            ViewData["Cr2Cr1Key"] = new SelectList(_context.Credentials, "Cr1Key", "Cr1Credential");
            ViewData["Cr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            return View();
        }

        // POST: CredentialProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cr2Key,Cr2Ct2Key,Cr2Cr1Key,Cr2Ct6Key,Cr2TerminationDate,Cr2AddUser,Cr2AddDate,Cr2ChangeUser,Cr2ChangeDate")] CredentialProcess credentialProcess)
        {
            if (ModelState.IsValid)
            {
                credentialProcess.Cr2Key = Guid.NewGuid();
                _context.Add(credentialProcess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cr2Cr1Key"] = new SelectList(_context.Credentials, "Cr1Key", "Cr1Credential", credentialProcess.Cr2Cr1Key);
            ViewData["Cr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcess.Cr2Ct6Key);
            return View(credentialProcess);
        }

        // GET: CredentialProcesses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcess = await _context.CredentialProcesses.FindAsync(id);
            if (credentialProcess == null)
            {
                return NotFound();
            }
            ViewData["Cr2Cr1Key"] = new SelectList(_context.Credentials, "Cr1Key", "Cr1Credential", credentialProcess.Cr2Cr1Key);
            ViewData["Cr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcess.Cr2Ct6Key);
            return View(credentialProcess);
        }

        // POST: CredentialProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Cr2Key,Cr2Ct2Key,Cr2Cr1Key,Cr2Ct6Key,Cr2TerminationDate,Cr2AddUser,Cr2AddDate,Cr2ChangeUser,Cr2ChangeDate")] CredentialProcess credentialProcess)
        {
            if (id != credentialProcess.Cr2Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialProcess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialProcessExists(credentialProcess.Cr2Key))
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
            ViewData["Cr2Cr1Key"] = new SelectList(_context.Credentials, "Cr1Key", "Cr1Credential", credentialProcess.Cr2Cr1Key);
            ViewData["Cr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcess.Cr2Ct6Key);
            return View(credentialProcess);
        }

        // GET: CredentialProcesses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcess = await _context.CredentialProcesses
                .Include(c => c.Credential)
                .Include(c => c.StatusType)
                .FirstOrDefaultAsync(m => m.Cr2Key == id);
            if (credentialProcess == null)
            {
                return NotFound();
            }

            return View(credentialProcess);
        }

        // POST: CredentialProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialProcess = await _context.CredentialProcesses.FindAsync(id);
            _context.CredentialProcesses.Remove(credentialProcess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialProcessExists(Guid id)
        {
            return _context.CredentialProcesses.Any(e => e.Cr2Key == id);
        }
    }
}
