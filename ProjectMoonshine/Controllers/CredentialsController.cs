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
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cr1Key,Cr1Ct1Credential,Cr1CredentialNumber,Cr1PerKey,Cr1Status,Cr1StatusChangeDate,Cr1StatusChangeUser,Cr1CredentialDate,Cr1EffectiveDate,Cr1ValidThruDate,Cr1Ct2CurrentProcess,Cr1CurrentCr2Key,Cr1NextCr2Key,Cr1ApplicationCr2Key,Cr1ExtensionUsedFlag,Cr1Note,Cr1Owner,Cr1DataKey,Cr1AddUser,Cr1AddDate,Cr1ChangeUser,Cr1ChangeDate,Cr1LegalFirstName,Cr1LegalLastName")] Credential credential)
        {
            if (ModelState.IsValid)
            {
                credential.Cr1Key = Guid.NewGuid();
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Cr1Key,Cr1Ct1Credential,Cr1CredentialNumber,Cr1PerKey,Cr1Status,Cr1StatusChangeDate,Cr1StatusChangeUser,Cr1CredentialDate,Cr1EffectiveDate,Cr1ValidThruDate,Cr1Ct2CurrentProcess,Cr1CurrentCr2Key,Cr1NextCr2Key,Cr1ApplicationCr2Key,Cr1ExtensionUsedFlag,Cr1Note,Cr1Owner,Cr1DataKey,Cr1AddUser,Cr1AddDate,Cr1ChangeUser,Cr1ChangeDate,Cr1LegalFirstName,Cr1LegalLastName")] Credential credential)
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
