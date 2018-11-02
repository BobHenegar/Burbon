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
    public class CredentialRequirementTypesController : BaseController
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
            //ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            //ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            //ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            //ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code");
            //List<SelectListItem> list = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Template").ToList();
            //list.Insert(0,(new SelectListItem { Text = "Please Select", Value = null }));
            //ViewData["Ct3Ct8Key"] = list;

            CredentialRequirementTypeViewModel viewModel = new CredentialRequirementTypeViewModel(_context);
            return View(viewModel);
        }

        // POST: CredentialRequirementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(ModelBindingConstants.CredentialRequirementType)] CredentialRequirementType credentialRequirementType)
        {
            if (ModelState.IsValid)
            {
                credentialRequirementType.Ct3Key = Guid.NewGuid();
                _context.Add(credentialRequirementType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3CompleteCt6Key);
            //ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3MetCt6Key);
            //ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3StartCt6Key);
            //ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3SubmitCt6Key);
            //ViewData["Ct3Ct8Key"] = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Template", credentialRequirementType.Ct3Ct8Key);
            //ViewData["Ct3DependentCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3DependentCt6Key);
            //ViewData["Ct3StartCr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3StartCr2Ct6Key);
            //ViewData["Ct3MetCr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3MetCr2Ct6Key);
            //ViewData["Ct3CetKey"] = new SelectList(_context.CEType, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3CetKey);
            CredentialRequirementTypeViewModel viewModel = new CredentialRequirementTypeViewModel(_context, credentialRequirementType);
            return View(viewModel);
        }

        // GET: CredentialRequirementTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid? ct5Key = null)
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
            //ViewData["Ct3CompleteCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3CompleteCt6Key);
            //ViewData["Ct3MetCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3MetCt6Key);
            //ViewData["Ct3StartCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3StartCt6Key);
            //ViewData["Ct3SubmitCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3SubmitCt6Key);
            //ViewData["Ct3Ct8Key"] = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Template", credentialRequirementType.Ct3Ct8Key);
            //ViewData["Ct3DependentCt6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3DependentCt6Key);
            //ViewData["Ct3StartCr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3StartCr2Ct6Key);
            //ViewData["Ct3MetCr2Ct6Key"] = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3MetCr2Ct6Key);
            //ViewData["Ct3CetKey"] = new SelectList(_context.CEType, "Ct6Key", "Ct6Code", credentialRequirementType.Ct3CetKey);

            ViewData["ct5Key"] = ct5Key != null? ct5Key.ToString() : "";

            CredentialRequirementTypeViewModel viewModel = new CredentialRequirementTypeViewModel(_context, credentialRequirementType);
            return View(viewModel);
        }

        // POST: CredentialRequirementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(ModelBindingConstants.CredentialRequirementType)] CredentialRequirementType credentialRequirementType)
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
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialRequirementTypeExists(credentialRequirementType.Ct3Key))
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
                return Request.Form["source"].ToString().Equals("CredentialProcessTypes") ? RedirectToAction("Edit", "CredentialProcessTypeXCredentialRequirementTypes", new { id = Request.Form["ct5Key"].ToString(), source = "RequirementTypes" }) : RedirectToAction(nameof(Index));
            }
            CredentialRequirementTypeViewModel viewModel = new CredentialRequirementTypeViewModel(_context, credentialRequirementType);
            return View(viewModel);
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
