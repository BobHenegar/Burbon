using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class CredentialsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CredentialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Credentials
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Credentials
                .Include(c => c.CredentialType)
                .Include(c => c.CurrentCredentialProcess)
                .Include(c => c.CurrentCredentialProcessType)
                .Include(c => c.Customer)
                .Include(c => c.StatusType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Credentials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var credential = await _context.Credentials
                .Include(c => c.CredentialType)
                .Include(c => c.CurrentCredentialProcess)
                .Include(c => c.CurrentCredentialProcessType)
                .Include(c => c.Customer)
                .Include(c => c.StatusType)
                .FirstOrDefaultAsync(m => m.Cr1Key == id);
            if (credential == null)
            {
                return NotFound();
            }

            CredentialViewModel credentialViewModel = new CredentialViewModel(_context, credential);
            return View(credentialViewModel);
        }

        // GET: Credentials/Create
        public IActionResult Create(Guid? id)
        {
            ViewData["CusKeyHasValue"] = id.HasValue;
            if (id.HasValue)
            {
                ViewData["CusKey"] = id.Value;
            }

            CredentialViewModel credentialViewModel = new CredentialViewModel(_context);
            return View(credentialViewModel);
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind(ModelBindingConstants.CredentialAdd)] Credential credential)
        {
            if (ModelState.IsValid)
            {
                try
                {
                credential.Cr1Key = Guid.NewGuid();
                bool success = await credential.Add(_context);
                    if (success) return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.RecursivelyFormatException());
                    if (Environment.UserInteractive)
                    {
                        Debugger.Break();
                    }
                    throw;
                }   
            }
            CredentialViewModel credentialViewModel = new CredentialViewModel(_context, credential);
            return View(credentialViewModel);
        }

        // GET: Credentials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Credential credential = await _context.Credentials.FindAsync(id);
            if (credential == null)
            {
                return NotFound();
            }
            CredentialViewModel credentialViewModel = new CredentialViewModel(_context, credential);
            return View(credentialViewModel);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, //[Bind(ModelBindingConstants.CredentialEdit)]
                                                       Credential credential)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Credential credentialToUpdate = await _context.Credentials.FindAsync(id);

                    if (credentialToUpdate == null) return NotFound();
                    credentialToUpdate.Cr1Ct6Key = credential.Cr1Ct6Key;
                    credentialToUpdate.Cr1StartDate = credential.Cr1StartDate;
                    credentialToUpdate.Cr1CredentialDate = credential.Cr1CredentialDate;
                    credentialToUpdate.Cr1CredentialNumber = credential.Cr1CredentialNumber;
                    credentialToUpdate.Cr1EffectiveDate = credential.Cr1EffectiveDate;
                    credentialToUpdate.Cr1ValidThruDate = credential.Cr1ValidThruDate;

                    await credentialToUpdate.Update(_context);
                    //_context.Update(credential);

                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialExists(id))
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
            CredentialViewModel credentialViewModel = new CredentialViewModel(_context, credential);
            return View(credentialViewModel);
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
                .Include(c => c.CurrentCredentialProcess)
                .Include(c => c.CurrentCredentialProcessType)
                .Include(c => c.Customer)
                .Include(c => c.StatusType)
                .FirstOrDefaultAsync(m => m.Cr1Key == id);
            if (credential == null)
            {
                return NotFound();
            }
            CredentialViewModel credentialViewModel = new CredentialViewModel(_context, credential);

            return View(credentialViewModel);
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
