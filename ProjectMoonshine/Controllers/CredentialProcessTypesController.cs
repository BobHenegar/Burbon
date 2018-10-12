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
    public class CredentialProcessTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialProcessTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialProcessTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialProcessTypes.Include(c => c.CompletedStatus).Include(c => c.StartStatus).Include(c => c.SubmittedStatus);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialProcessTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcessType = await _context.CredentialProcessTypes
                .Include(c => c.CompletedStatus)
                .Include(c => c.StartStatus)
                .Include(c => c.SubmittedStatus)
                .FirstOrDefaultAsync(m => m.Ct2Key == id);
            if (credentialProcessType == null)
            {
                return NotFound();
            }

            return View(credentialProcessType);
        }

        // GET: CredentialProcessTypes/Create
        public IActionResult Create()
        {
            ViewData["Ct2CompletedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            ViewData["Ct2StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            ViewData["Ct2SubmittedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            return View();
        }

        // POST: CredentialProcessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ct2Key,Ct2Process,Ct2Description,Ct2StartCt6Key,Ct2CompletedCt6Key,Ct2SubmittedCt6Key,Ct2AddDate,Ct2ChangeDate,Ct2AddUser,Ct2ChangeUser")] CredentialProcessType credentialProcessType)
        {
            if (ModelState.IsValid)
            {
                credentialProcessType.Ct2Key = Guid.NewGuid();
                _context.Add(credentialProcessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ct2CompletedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2CompletedCt6Key);
            ViewData["Ct2StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2StartCt6Key);
            ViewData["Ct2SubmittedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2SubmittedCt6Key);
            return View(credentialProcessType);
        }

        // GET: CredentialProcessTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcessType = await _context.CredentialProcessTypes.FindAsync(id);
            if (credentialProcessType == null)
            {
                return NotFound();
            }
            ViewData["Ct2CompletedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2CompletedCt6Key);
            ViewData["Ct2StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2StartCt6Key);
            ViewData["Ct2SubmittedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2SubmittedCt6Key);
            return View(credentialProcessType);
        }

        // POST: CredentialProcessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ct2Key,Ct2Process,Ct2Description,Ct2StartCt6Key,Ct2CompletedCt6Key,Ct2SubmittedCt6Key,Ct2AddDate,Ct2ChangeDate,Ct2AddUser,Ct2ChangeUser")] CredentialProcessType credentialProcessType)
        {
            if (id != credentialProcessType.Ct2Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialProcessType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialProcessTypeExists(credentialProcessType.Ct2Key))
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
            ViewData["Ct2CompletedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2CompletedCt6Key);
            ViewData["Ct2StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2StartCt6Key);
            ViewData["Ct2SubmittedCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialProcessType.Ct2SubmittedCt6Key);
            return View(credentialProcessType);
        }

        // GET: CredentialProcessTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcessType = await _context.CredentialProcessTypes
                .Include(c => c.CompletedStatus)
                .Include(c => c.StartStatus)
                .Include(c => c.SubmittedStatus)
                .FirstOrDefaultAsync(m => m.Ct2Key == id);
            if (credentialProcessType == null)
            {
                return NotFound();
            }

            return View(credentialProcessType);
        }

        // POST: CredentialProcessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialProcessType = await _context.CredentialProcessTypes.FindAsync(id);
            _context.CredentialProcessTypes.Remove(credentialProcessType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialProcessTypeExists(Guid id)
        {
            return _context.CredentialProcessTypes.Any(e => e.Ct2Key == id);
        }
    }
}
