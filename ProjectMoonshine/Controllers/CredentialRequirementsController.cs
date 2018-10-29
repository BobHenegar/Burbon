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
                .FirstOrDefaultAsync(m => m.Cr3Key == id);
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
            //ViewData["Cr3Cr2Key"] = new SelectList(_context.CredentialProcesses, "Cr2Key", "Cr2Key");
            //ViewData["Cr3Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Key");
            //ViewData["Cr3Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
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
                //credentialRequirement.Cr3Key = Guid.NewGuid();
                await credentialRequirement.AddAsync(_context);
                //_context.Add(credentialRequirement);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Cr3Cr2Key"] = new SelectList(_context.CredentialProcesses, "Cr2Key", "Cr2Key", credentialRequirement.Cr3Cr2Key);
            //ViewData["Cr3Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Key", credentialRequirement.Cr3Ct3Key);
            //ViewData["Cr3Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirement.Cr3Ct6Key);
            CredentialRequirementViewModel viewModel = new CredentialRequirementViewModel(_context, credentialRequirement);
            return View(viewModel);
        }

        // GET: CredentialRequirements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirement = await _context.CredentialRequirements.FindAsync(id);
            if (credentialRequirement == null)
            {
                return NotFound();
            }
            //ViewData["Cr3Cr2Key"] = new SelectList(_context.CredentialProcesses, "Cr2Key", "Cr2Key", credentialRequirement.Cr3Cr2Key);
            //ViewData["Cr3Ct3Key"] = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Key", credentialRequirement.Cr3Ct3Key);
            //ViewData["Cr3Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirement.Cr3Ct6Key);
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

                    await requirementToUpdate.UpdateAsync(_context);
                    //_context.Update(requirementToUpdate);
                    //await _context.SaveChangesAsync();
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

                if (Request.Form["source"].ToString().Length > 0)
                {
                    return RedirectToAction("Details", "Credentials", new {id = _context.CredentialProcesses
                                                                                .Where(p => p.Cr2Key == credentialRequirement.Cr3Cr2Key)
                                                                                .Select(p => p.Cr2Cr1Key)
                                                                                .FirstOrDefault()
                                                                                .ToString()});
                }
                return RedirectToAction(nameof(Index));
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
