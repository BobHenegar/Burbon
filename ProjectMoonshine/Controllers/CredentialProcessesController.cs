using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class CredentialProcessesController : BaseController
    {
        private readonly ApplicationDbContext _context;


        public CredentialProcessesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialProcesses
        public async Task<IActionResult> Index(string sortOrder, string lastOrder, string currentFilter, string searchString, int? page)
        {
            int currentPage = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "firstName";
                ViewBag.LastSortParam = sortOrder;
            }
            else
            {
                if (sortOrder == lastOrder)
                {
                    int index = sortOrder.IndexOf("_desc");
                    string cleanPath = (index < 0)
                        ? sortOrder = sortOrder + "_desc"
                        : sortOrder.Remove(index, "_desc".Length);
                }
            }
            ViewBag.LastSortParam = sortOrder;

            var credProcess = await _context
                .CredentialProcesses
                .Include(c => c.Credential)
                .Include(c => c.StatusType)
                .ToListAsync();
            return PartialView("_CredentialProcessHomePartialView",credProcess.ToPagedList(currentPage, 10));
        }

        // GET: CredentialProcesses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialProcess = await _context.CredentialProcesses
                .Include(p => p.CredentialProcessType)
                .Include(c => c.Credential)
                    .ThenInclude(c => c.CredentialType)
                .Include(c => c.StatusType)
                .Include(p => p.CredentialRequirements)
                .ThenInclude(r => r.CredentialRequirementType)
                .Include(p => p.CredentialRequirements)
                .ThenInclude(r => r.Status)
                .AsNoTracking()
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
            //ViewData["Cr2Cr1Key"] = new SelectList(_context.Credentials, "Cr1Key", "Cr1Credential");
            //ViewData["Cr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            CredentialProcessViewModel credentialProcessViewModel = new CredentialProcessViewModel(_context);
            return View(credentialProcessViewModel);
        }

        // POST: CredentialProcesses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind(ModelBindingConstants.CredentialProcessAdd)] CredentialProcess credentialProcess)
        {
            if (ModelState.IsValid)
            {
                credentialProcess.Cr2Key = Guid.NewGuid();
                _context.Add(credentialProcess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CredentialProcessViewModel credentialProcessViewModel = new CredentialProcessViewModel(_context, credentialProcess);
            return View(credentialProcessViewModel);

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

            CredentialProcessViewModel credentialProcessViewModel = new CredentialProcessViewModel(_context, credentialProcess);
            return View(credentialProcessViewModel);
        }

        // POST: CredentialProcesses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, //[Bind(ModelBindingConstants.CredentialProcessEdit)]
                                                       CredentialProcess credentialProcess)
        {
            //if (id != viewModel.CredentialProcess.Cr2Key)
            //{
            //    return NotFound();
            //}


            if (ModelState.IsValid)
            {
                try
                {
                    var credentialProcessToUpdate = await _context.CredentialProcesses.FindAsync(id);

                    if (credentialProcessToUpdate == null) return NotFound();

                    credentialProcessToUpdate.Cr2TerminationDate = credentialProcess.Cr2TerminationDate;
                    credentialProcessToUpdate.Cr2Ct6Key = credentialProcess.Cr2Ct6Key;
                    credentialProcessToUpdate.Cr2DueDate = credentialProcess.Cr2DueDate;
                    credentialProcessToUpdate.Cr2StartDate = credentialProcess.Cr2StartDate;
                    credentialProcessToUpdate.Cr2CompletionDate = credentialProcess.Cr2CompletionDate;

                    await credentialProcessToUpdate.UpdateAsync(_context);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialProcessExists(id))
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
                return RedirectToAction(nameof(Details), new {id = credentialProcess.Cr2Key});
            }
            CredentialProcessViewModel credentialProcessViewModel = new CredentialProcessViewModel(_context, credentialProcess);
            return View(credentialProcessViewModel);
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
