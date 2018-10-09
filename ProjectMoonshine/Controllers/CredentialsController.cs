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
            return View(await _context.Credentials.ToListAsync());
        }

        // GET: Credentials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credential = await _context.Credentials
                .FirstOrDefaultAsync(m => m.cr1Key == id);
            if (credential == null)
            {
                return NotFound();
            }

            return View(credential);
        }

        // GET: Credentials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("cr1Key,cr1Ct1Credential,cr1CredentialNumber,cr1CstKey,cr1Status,cr1StatusChangeDate,cr1StatusChangeUser,cr1CredentialDate,cr1EffectiveDate,cr1ValidThruDate,cr1Ct2CurrentProcess,cr1CurrentCr2Key,cr1NextCr2Key,cr1ApplicationCr2Key,cr1ExtensionUsedFlag,cr1Note,cr1Owner,cr1DataKey,cr1AddDate,cr1ChangeDate,cr1AddUser,cr1ChangeUser,cr1LegalFirstName,cr1LegalLastName")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                credential.cr1Key = Guid.NewGuid();
                _context.Add(credential);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(credential);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("cr1Key,cr1Ct1Credential,cr1CredentialNumber,cr1CstKey,cr1Status,cr1StatusChangeDate,cr1StatusChangeUser,cr1CredentialDate,cr1EffectiveDate,cr1ValidThruDate,cr1Ct2CurrentProcess,cr1CurrentCr2Key,cr1NextCr2Key,cr1ApplicationCr2Key,cr1ExtensionUsedFlag,cr1Note,cr1Owner,cr1DataKey,cr1AddDate,cr1ChangeDate,cr1AddUser,cr1ChangeUser,cr1LegalFirstName,cr1LegalLastName")] Credential credential)
        {
            if (id != credential.cr1Key)
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
                    if (!CredentialExists(credential.cr1Key))
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
                .FirstOrDefaultAsync(m => m.cr1Key == id);
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
            return _context.Credentials.Any(e => e.cr1Key == id);
        }
    }
}
