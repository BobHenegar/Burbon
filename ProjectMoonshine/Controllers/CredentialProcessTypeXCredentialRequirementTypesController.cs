using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Controllers
{
    public class CredentialProcessTypeXCredentialRequirementTypesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CredentialProcessTypeXCredentialRequirementTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialProcessTypeXCredentialRequirementTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialProcessTypeXCredentialRequirementTypes.Include(c => c.CredentialProcessType).Include(c => c.CredentialRequirementType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialProcessTypeXCredentialRequirementTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcessTypeXCredentialRequirementType = await _context.CredentialProcessTypeXCredentialRequirementTypes
                .Include(c => c.CredentialProcessType)
                .Include(c => c.CredentialRequirementType)
                .FirstOrDefaultAsync(m => m.Ct5Key == id);
            if (credentialProcessTypeXCredentialRequirementType == null)
            {
                return NotFound();
            }

            return View(credentialProcessTypeXCredentialRequirementType);
        }

        // GET: CredentialProcessTypeXCredentialRequirementTypes/Create
        public IActionResult Create()
        {
            ViewData["Ct5Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process");
            ViewData["Ct5Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement");
            return View();
        }

        // POST: CredentialProcessTypeXCredentialRequirementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ct5Key,Ct5Ct2Key,Ct5Ct3Key,Ct5Order,Ct5SubmitPrereqFlag,Ct5ExamPrereqFlag,Ct5ReviewPrereqFlag,Ct5AddDate,Ct5ChangeDate,Ct5AddUser,Ct5ChangeUser")] CredentialProcessTypeXCredentialRequirementType credentialProcessTypeXCredentialRequirementType)
        {
            if (ModelState.IsValid)
            {
                credentialProcessTypeXCredentialRequirementType.Ct5Key = Guid.NewGuid();
                _context.Add(credentialProcessTypeXCredentialRequirementType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ct5Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", credentialProcessTypeXCredentialRequirementType.Ct5Ct2Key);
            ViewData["Ct5Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement", credentialProcessTypeXCredentialRequirementType.Ct5Ct3Key);
            return View(credentialProcessTypeXCredentialRequirementType);
        }

        // GET: CredentialProcessTypeXCredentialRequirementTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcessTypeXCredentialRequirementType = await _context.CredentialProcessTypeXCredentialRequirementTypes.FindAsync(id);
            if (credentialProcessTypeXCredentialRequirementType == null)
            {
                return NotFound();
            }
            ViewData["Ct5Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", credentialProcessTypeXCredentialRequirementType.Ct5Ct2Key);
            ViewData["Ct5Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement", credentialProcessTypeXCredentialRequirementType.Ct5Ct3Key);
            return View(credentialProcessTypeXCredentialRequirementType);
        }

        // POST: CredentialProcessTypeXCredentialRequirementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ct5Key,Ct5Ct2Key,Ct5Ct3Key,Ct5Order,Ct5SubmitPrereqFlag,Ct5ExamPrereqFlag,Ct5ReviewPrereqFlag,Ct5AddDate,Ct5ChangeDate,Ct5AddUser,Ct5ChangeUser")] CredentialProcessTypeXCredentialRequirementType credentialProcessTypeXCredentialRequirementType)
        {
            if (id != credentialProcessTypeXCredentialRequirementType.Ct5Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialProcessTypeXCredentialRequirementType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialProcessTypeXCredentialRequirementTypeExists(credentialProcessTypeXCredentialRequirementType.Ct5Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Debug.WriteLine(e.RecursivelyFormatException());
                        if (Environment.UserInteractive)
                        {
                            Debugger.Break();
                        }
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ct5Ct2Key"] = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", credentialProcessTypeXCredentialRequirementType.Ct5Ct2Key);
            ViewData["Ct5Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement", credentialProcessTypeXCredentialRequirementType.Ct5Ct3Key);
            return View(credentialProcessTypeXCredentialRequirementType);
        }

        // GET: CredentialProcessTypeXCredentialRequirementTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcessTypeXCredentialRequirementType = await _context.CredentialProcessTypeXCredentialRequirementTypes
                .Include(c => c.CredentialProcessType)
                .Include(c => c.CredentialRequirementType)
                .FirstOrDefaultAsync(m => m.Ct5Key == id);
            if (credentialProcessTypeXCredentialRequirementType == null)
            {
                return NotFound();
            }

            return View(credentialProcessTypeXCredentialRequirementType);
        }

        // POST: CredentialProcessTypeXCredentialRequirementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialProcessTypeXCredentialRequirementType = await _context.CredentialProcessTypeXCredentialRequirementTypes.FindAsync(id);
            _context.CredentialProcessTypeXCredentialRequirementTypes.Remove(credentialProcessTypeXCredentialRequirementType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialProcessTypeXCredentialRequirementTypeExists(Guid id)
        {
            return _context.CredentialProcessTypeXCredentialRequirementTypes.Any(e => e.Ct5Key == id);
        }
    }
}
