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
    public class CredentialProcessTypesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CredentialProcessTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialProcessTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialProcessTypes
                .Include(c => c.CompletedStatus)
                .Include(c => c.StartStatus)
                .Include(c => c.SubmittedStatus)
                .Include(c => c.CredentialProcessTypeXCredentialRequirementTypes)
                .ThenInclude(cx => cx.CredentialRequirementType);
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
                .Include(pt => pt.NextCredentialProcessType)
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
            CredentialProcessTypeViewModel credentialProcessTypeViewModel = new CredentialProcessTypeViewModel(_context);

            return View(credentialProcessTypeViewModel);
        }

        // POST: CredentialProcessTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind(ModelBindingConstants.CredentialProcessType)] CredentialProcessType credentialProcessType)
        {
            if (ModelState.IsValid)
            {
                credentialProcessType.Ct2Key = Guid.NewGuid();
                _context.Add(credentialProcessType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CredentialProcessTypeViewModel credentialProcessTypeViewModel = new CredentialProcessTypeViewModel(_context, credentialProcessType);

            return View(credentialProcessTypeViewModel);
        }

        // GET: CredentialProcessTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid? ct4Key = null)
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

            ViewData["ct4Key"] = ct4Key !=null? ct4Key.ToString(): "";

            CredentialProcessTypeViewModel credentialProcessTypeViewModel = new CredentialProcessTypeViewModel(_context, credentialProcessType);

            return View(credentialProcessTypeViewModel);
        }


        // POST: CredentialProcessTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(ModelBindingConstants.CredentialProcessType)] CredentialProcessType credentialProcessType)
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
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialProcessTypeExists(credentialProcessType.Ct2Key))
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
                return Request.Form["source"].ToString().Equals("CredentialType") ? RedirectToAction("Edit", "CredentialTypeXCredentialProcessTypes",new{id=Request.Form["ct4Key"].ToString(), source="ProcessTypes"}) : RedirectToAction(nameof(Index));
            }
            CredentialProcessTypeViewModel credentialProcessTypeViewModel = new CredentialProcessTypeViewModel(_context, credentialProcessType);

            return View(credentialProcessTypeViewModel);
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

        [HttpPost]
        public ActionResult AddRequirementType(string submit)
        {
            if (Request.Form["ct3Name"].ToString().Length > 0)
            {

                Guid g = Guid.NewGuid();
                CredentialRequirementType r = new CredentialRequirementType();
                r.Ct3Key = g;
                r.Ct3Requirement = Request.Form["ct3Name"].ToString();
                r.Ct3Description = Request.Form["ct3Name"].ToString();
                r.Ct3Ct8Key = Guid.Parse(Request.Form["Ct8Templates"]);
                _context.Add(r);
                _context.SaveChanges();


                CredentialProcessTypeXCredentialRequirementType c = new CredentialProcessTypeXCredentialRequirementType();
                c.Ct5Key = new Guid();
                c.Ct5Ct2Key = Guid.Parse(Request.Form["ct2Key"]);
                c.Ct5Ct3Key = g;
                _context.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Edit", "CredentialRequirementTypes", new { id = c.Ct5Ct3Key.ToString(), ct5Key=c.Ct5Key });
            }
            else
            {
                CredentialProcessTypeXCredentialRequirementType c = new CredentialProcessTypeXCredentialRequirementType();
                c.Ct5Key = new Guid();
                c.Ct5Ct2Key = Guid.Parse(Request.Form["ct2Key"]);
                c.Ct5Ct3Key = Guid.Parse(Request.Form["Ct3Requirements"].ToString());
                _context.Add(c);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "CredentialTypes");
        }
    }
}
