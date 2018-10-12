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
    public class CredentialRequirementTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialRequirementTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialRequirementTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialRequirementTypes.Include(c => c.CompleteStatusType).Include(c => c.MetStatusType).Include(c => c.StartStatusType).Include(c => c.SubmitStatusType).Include(c => c.Template);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialRequirementTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirementType = await _context.CredentialRequirementTypes
                .Include(c => c.CompleteStatusType)
                .Include(c => c.MetStatusType)
                .Include(c => c.StartStatusType)
                .Include(c => c.SubmitStatusType)
                .Include(c => c.Template)
                .FirstOrDefaultAsync(m => m.Ct3Key == id);
            if (credentialRequirementType == null)
            {
                return NotFound();
            }

            return View(credentialRequirementType);
        }

        // GET: CredentialRequirementTypes/Create
        public IActionResult Create()
        {
            ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key");
            ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key");
            ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key");
            ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key");
            ViewData["Ct3Ct8Key"] = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Template");
            return View();
        }

        // POST: CredentialRequirementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ct3Key,Ct3Requirement,Ct3Description,Ct3Ct8Key,Ct3StartCt6Key,Ct3CompleteCt6Key,Ct3SubmitCt6Key,Ct3MetCt6Key,Ct3LongDescription,Ct3AddDate,Ct3ChangeDate,Ct3AddUser,Ct3ChangeUser")] CredentialRequirementType credentialRequirementType)
        {
            if (ModelState.IsValid)
            {
                credentialRequirementType.Ct3Key = Guid.NewGuid();
                _context.Add(credentialRequirementType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3CompleteCt6Key);
            ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3MetCt6Key);
            ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3StartCt6Key);
            ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3SubmitCt6Key);
            ViewData["Ct3Ct8Key"] = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Key", credentialRequirementType.Ct3Ct8Key);
            return View(credentialRequirementType);
        }

        // GET: CredentialRequirementTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirementType = await _context.CredentialRequirementTypes.FindAsync(id);
            if (credentialRequirementType == null)
            {
                return NotFound();
            }
            ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3CompleteCt6Key);
            ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3MetCt6Key);
            ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3StartCt6Key);
            ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3SubmitCt6Key);
            ViewData["Ct3Ct8Key"] = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Key", credentialRequirementType.Ct3Ct8Key);
            return View(credentialRequirementType);
        }

        // POST: CredentialRequirementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ct3Key,Ct3Requirement,Ct3Description,Ct3Ct8Key,Ct3StartCt6Key,Ct3CompleteCt6Key,Ct3SubmitCt6Key,Ct3MetCt6Key,Ct3LongDescription,Ct3AddDate,Ct3ChangeDate,Ct3AddUser,Ct3ChangeUser")] CredentialRequirementType credentialRequirementType)
        {
            if (id != credentialRequirementType.Ct3Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialRequirementType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialRequirementTypeExists(credentialRequirementType.Ct3Key))
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
            ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3CompleteCt6Key);
            ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3MetCt6Key);
            ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3StartCt6Key);
            ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Key", credentialRequirementType.Ct3SubmitCt6Key);
            ViewData["Ct3Ct8Key"] = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Key", credentialRequirementType.Ct3Ct8Key);
            return View(credentialRequirementType);
        }

        // GET: CredentialRequirementTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirementType = await _context.CredentialRequirementTypes
                .Include(c => c.CompleteStatusType)
                .Include(c => c.MetStatusType)
                .Include(c => c.StartStatusType)
                .Include(c => c.SubmitStatusType)
                .Include(c => c.Template)
                .FirstOrDefaultAsync(m => m.Ct3Key == id);
            if (credentialRequirementType == null)
            {
                return NotFound();
            }

            return View(credentialRequirementType);
        }

        // POST: CredentialRequirementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialRequirementType = await _context.CredentialRequirementTypes.FindAsync(id);
            _context.CredentialRequirementTypes.Remove(credentialRequirementType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialRequirementTypeExists(Guid id)
        {
            return _context.CredentialRequirementTypes.Any(e => e.Ct3Key == id);
        }
    }
}
