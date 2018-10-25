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
    public class CredentialTypeXCredentialProcessTypesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CredentialTypeXCredentialProcessTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialTypeXCredentialProcessTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialTypeXCredentialProcessTypes.Include(c => c.CredentialProcessType).Include(c => c.CredentialType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialTypeXCredentialProcessTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes
                .Include(c => c.CredentialProcessType)
                .Include(c => c.CredentialType)
                .FirstOrDefaultAsync(m => m.Ct4Key == id);
            if (credentialTypeXCredentialProcessType == null)
            {
                return NotFound();
            }

            return View(credentialTypeXCredentialProcessType);
        }

        // GET: CredentialTypeXCredentialProcessTypes/Create
        public IActionResult Create()
        {
            CredentialTypeXCredentialProcessTypeViewModel credentialTypeXCredentialProcessTypeViewModel = new CredentialTypeXCredentialProcessTypeViewModel(_context);
            return View(credentialTypeXCredentialProcessTypeViewModel);
        }

        // POST: CredentialTypeXCredentialProcessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind(ModelBindingConstants.CredentialTypeXCredentialProcessType)] CredentialTypeXCredentialProcessType credentialTypeXCredentialProcessType)
        {
            if (ModelState.IsValid)
            {
                credentialTypeXCredentialProcessType.Ct4Key = Guid.NewGuid();
                _context.Add(credentialTypeXCredentialProcessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CredentialTypeXCredentialProcessTypeViewModel credentialTypeXCredentialProcessTypeViewModel = new CredentialTypeXCredentialProcessTypeViewModel(_context, credentialTypeXCredentialProcessType);
            return View(credentialTypeXCredentialProcessTypeViewModel);
        }

        // GET: CredentialTypeXCredentialProcessTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id, string source = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes.FindAsync(id);
            if (credentialTypeXCredentialProcessType == null)
            {
                return NotFound();
            }

            
            ViewData["source"] = source ?? "";

            CredentialTypeXCredentialProcessTypeViewModel credentialTypeXCredentialProcessTypeViewModel = new CredentialTypeXCredentialProcessTypeViewModel(_context, credentialTypeXCredentialProcessType);
            return View(credentialTypeXCredentialProcessTypeViewModel);
        }

        // POST: CredentialTypeXCredentialProcessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(ModelBindingConstants.CredentialTypeXCredentialProcessType)] CredentialTypeXCredentialProcessType credentialTypeXCredentialProcessType, string submit)
        {
            if (id != credentialTypeXCredentialProcessType.Ct4Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialTypeXCredentialProcessType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialTypeXCredentialProcessTypeExists(credentialTypeXCredentialProcessType.Ct4Key))
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

                return Request.Form["source"].ToString().Equals("CredentialType") ? RedirectToAction(nameof(Index), "CredentialTypes") : RedirectToAction(nameof(Index));
            }
            CredentialTypeXCredentialProcessTypeViewModel credentialTypeXCredentialProcessTypeViewModel = new CredentialTypeXCredentialProcessTypeViewModel(_context, credentialTypeXCredentialProcessType);
            return View(credentialTypeXCredentialProcessTypeViewModel);
        }

        // GET: CredentialTypeXCredentialProcessTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes
                .Include(c => c.CredentialProcessType)
                .Include(c => c.CredentialType)
                .FirstOrDefaultAsync(m => m.Ct4Key == id);
            if (credentialTypeXCredentialProcessType == null)
            {
                return NotFound();
            }

            return View(credentialTypeXCredentialProcessType);
        }

        // POST: CredentialTypeXCredentialProcessTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialTypeXCredentialProcessType = await _context.CredentialTypeXCredentialProcessTypes.FindAsync(id);
            _context.CredentialTypeXCredentialProcessTypes.Remove(credentialTypeXCredentialProcessType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialTypeXCredentialProcessTypeExists(Guid id)
        {
            return _context.CredentialTypeXCredentialProcessTypes.Any(e => e.Ct4Key == id);
        }
    }
}
