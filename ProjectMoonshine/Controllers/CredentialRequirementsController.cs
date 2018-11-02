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
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class CredentialRequirementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialRequirementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialRequirements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialRequirements
                .Include(c => c.CredentialProcess)
                    .ThenInclude(p => p.CredentialProcessType)
                .Include(c => c.CredentialProcess)
                    .ThenInclude( p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.Person)
                .Include(c => c.CredentialRequirementType)
                .Include(c => c.Status)
                .AsNoTracking();
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialRequirements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirement = await _context.CredentialRequirements
                .Include(c => c.CredentialProcess)
                    .ThenInclude(p => p.CredentialProcessType)
                .Include(c => c.CredentialRequirementType)
                    .ThenInclude(rt => rt.Template)
                .Include(c => c.Status)
                .Include(r => r.CEPeriod)
                    .ThenInclude(p => p.CeType)
                .Include(r => r.Degree)
                    .ThenInclude(d => d.DegreeType)
                .Include(r => r.License)
                .Include(r => r.ExamScore)
                    .ThenInclude(s => s.Exam)
                .Include(r => r.FileUpload)
                    .ThenInclude(f => f.DocumentType)
                .AsNoTracking()
                .Where(m => m.Cr3Key == id)
                .FirstOrDefaultAsync();
            if (credentialRequirement == null)
            {
                return NotFound();
            }

            return View(credentialRequirement);
        }

        // GET: CredentialRequirements/Create
        public IActionResult Create()
        {
            CredentialRequirementViewModel viewModel = new CredentialRequirementViewModel(_context);
            return View(viewModel);
        }

        // POST: CredentialRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CredentialRequirement credentialRequirement)
        {
            if (ModelState.IsValid)
            {
                await credentialRequirement.AddAsync(_context);
                return RedirectToAction(nameof(Index));
            }
            CredentialRequirementViewModel viewModel = new CredentialRequirementViewModel(_context, credentialRequirement);
            return View(viewModel);
        }

        // GET: CredentialRequirements/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid? processKey = null, Guid? customerKey = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirement = await _context.CredentialRequirements
                .AsNoTracking()
                .Include(c => c.CredentialProcess)
                    .ThenInclude(p => p.CredentialProcessType)
                
                .Include(r => r.CredentialProcess)
                    .ThenInclude(p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.Licenses)

                .Include(r => r.CredentialProcess)
                    .ThenInclude(p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.ExamScores)
                    .ThenInclude(s => s.Exam)

                .Include(r => r.CredentialProcess)
                    .ThenInclude(p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.Person)
                    .ThenInclude(p=>  p.FileUploads)
                    .ThenInclude(f => f.DocumentType)
                
                .Include(r => r.CredentialProcess)
                    .ThenInclude(p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.Degrees)
                    .ThenInclude(d => d.DegreeType)

                .Include(r => r.CredentialProcess)
                    .ThenInclude(p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(c => c.CEDetails)

                .Include(c => c.CredentialRequirementType)
                    .ThenInclude(rt => rt.Template)
                
                .Include(c => c.Status)
                
                .Include(r => r.CEPeriod)
                    .ThenInclude(p => p.CeType)
                
                .Include(r => r.Degree)
                    .ThenInclude(d => d.DegreeType)
                
                .Include(r => r.License)
                
                .Include(r => r.ExamScore)
                    .ThenInclude(s => s.Exam)
                
                .Include(r => r.FileUpload)
                    .ThenInclude(f => f.DocumentType)
                
                .Where(m => m.Cr3Key == id)
                .FirstOrDefaultAsync();
            if (credentialRequirement == null)
            {
                return NotFound();
            }
            CredentialRequirementViewModel viewModel = new CredentialRequirementViewModel(_context, credentialRequirement);
            return View(viewModel);
        }

        // POST: CredentialRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CredentialRequirement credentialRequirement)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    CredentialRequirement requirementToUpdate = await _context.CredentialRequirements.FindAsync(id);

                    if (requirementToUpdate == null) return NotFound();

                    requirementToUpdate.Cr3Cr2Key = credentialRequirement.Cr3Cr2Key;
                    requirementToUpdate.Cr3Ct6Key = credentialRequirement.Cr3Ct6Key;
                    requirementToUpdate.Cr3StartDate = credentialRequirement.Cr3StartDate;
                    requirementToUpdate.Cr3DueDate = credentialRequirement.Cr3DueDate;
                    requirementToUpdate.Cr3MetDate = credentialRequirement.Cr3MetDate;
                    requirementToUpdate.Cr3Ct3Key = credentialRequirement.Cr3Ct3Key;
                    requirementToUpdate.Cr3DegKey = credentialRequirement.Cr3DegKey;
                    requirementToUpdate.Cr3FupKey = credentialRequirement.Cr3FupKey;
                    requirementToUpdate.Cr3ExsKey = credentialRequirement.Cr3ExsKey;
                    requirementToUpdate.Cr3IxlKey = credentialRequirement.Cr3IxlKey;

                    await requirementToUpdate.UpdateAsync(_context);
                    return RedirectToAction("Details", "CredentialProcesses", new {id = requirementToUpdate.Cr3Cr2Key});
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialRequirementExists(id))
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
            }
            CredentialRequirementViewModel viewModel = new CredentialRequirementViewModel(_context, credentialRequirement);
            return View(viewModel);
        }

        // GET: CredentialRequirements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirement = await _context.CredentialRequirements
                .Include(c => c.CredentialProcess)
                .Include(c => c.CredentialRequirementType)
                .Include(c => c.Status)
                .FirstOrDefaultAsync(m => m.Cr3Key == id);
            if (credentialRequirement == null)
            {
                return NotFound();
            }

            return View(credentialRequirement);
        }

        // POST: CredentialRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialRequirement = await _context.CredentialRequirements.FindAsync(id);
            _context.CredentialRequirements.Remove(credentialRequirement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialRequirementExists(Guid id)
        {
            return _context.CredentialRequirements.Any(e => e.Cr3Key == id);
        }
    }
}
