using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class CredentialTypesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CredentialTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialTypes
                .Include(c => c.InitCredentialProcessType)
                .Include(c => c.InitStatusType)
                .Include(c => c.RenewStatusType)
                .Include(c => c.RenewCredentialProcessType)
                .Include(c => c.CredentialTypeXCredentialProcessTypes)
                    .ThenInclude(cx => cx.CredentialProcessType)
                    .ThenInclude(cx => cx.CredentialProcessTypeXCredentialRequirementTypes)
                        .ThenInclude(cr => cr.CredentialRequirementType)
                .OrderBy(c => c.Ct1Order);


            List<SelectListItem> processList = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process").ToList();
            processList.Insert(0, (new SelectListItem { Text = "Create New Process", Value = null }));
            processList.Insert(0, (new SelectListItem { Text = "Please Select", Value = null }));
            ViewData["Ct2Processes"] = processList;

            List<SelectListItem> reqList = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement").ToList();
            reqList.Insert(0, (new SelectListItem { Text = "Create New Requirement", Value = null }));
            reqList.Insert(0, (new SelectListItem { Text = "Please Select", Value = null }));
            ViewData["Ct3Requirements"] = reqList;

            List<SelectListItem> ct8List = new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Template").ToList();
            ct8List.Insert(0, (new SelectListItem { Text = "Please Select", Value = null }));
            ViewData["Ct8Templates"] = ct8List;

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialType = await _context.CredentialTypes
                .Include(c => c.InitCredentialProcessType)
                .Include(c => c.InitStatusType)
                .Include(c => c.RenewStatusType)
                .Include(c => c.RenewCredentialProcessType)
                .FirstOrDefaultAsync(m => m.Ct1Key == id);
            if (credentialType == null)
            {
                return NotFound();
            }

            return View(credentialType);
        }

        // GET: CredentialTypes/Create
        public IActionResult Create()
        {
            CredentialTypeViewModel credentialTypeViewModel = new CredentialTypeViewModel(_context);
            return View(credentialTypeViewModel);
        }

        // POST: CredentialTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(ModelBindingConstants.CredentialType)] CredentialType credentialType)
        {

            if (ModelState.IsValid)
            {
                credentialType.Ct1Key = Guid.NewGuid();
                _context.Add(credentialType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CredentialTypeViewModel credentialTypeViewModel = new CredentialTypeViewModel(_context, credentialType);

            return View(credentialTypeViewModel);
        }

        // GET: CredentialTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            CredentialType credentialType = await _context.CredentialTypes.FindAsync(id);
            if (credentialType == null)
            {
                return NotFound();
            }

            CredentialTypeViewModel credentialTypeViewModel = new CredentialTypeViewModel(_context, credentialType);
            return View(credentialTypeViewModel);
        }

        // POST: CredentialTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, 
            [Bind(ModelBindingConstants.CredentialType)]
            CredentialType credentialType)
        {

            if (id != credentialType.Ct1Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialTypeExists(credentialType.Ct1Key))
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

            CredentialTypeViewModel credentialTypeViewModel = new CredentialTypeViewModel(_context, credentialType);

            return View(credentialTypeViewModel);
        }

        // GET: CredentialTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialType = await _context.CredentialTypes
                .Include(c => c.InitCredentialProcessType)
                .Include(c => c.InitStatusType)
                .Include(c => c.RenewStatusType)
                .Include(c => c.RenewCredentialProcessType)
                .FirstOrDefaultAsync(m => m.Ct1Key == id);
            if (credentialType == null)
            {
                return NotFound();
            }

            return View(credentialType);
        }

        // POST: CredentialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialType = await _context.CredentialTypes.FindAsync(id);
            _context.CredentialTypes.Remove(credentialType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialTypeExists(Guid id)
        {
            return _context.CredentialTypes.Any(e => e.Ct1Key == id);
        }

        [HttpPost]
        public ActionResult AddProcessType(string submit)
        {
            if (Request.Form["ct2Name"].ToString().Length > 0)
            {

                Guid g = Guid.NewGuid();
                CredentialProcessType p = new CredentialProcessType();
                p.Ct2Key = g;
                p.Ct2Process = Request.Form["ct2Name"].ToString();
                p.Ct2Description = Request.Form["ct2Name"].ToString();
                _context.Add(p);
                _context.SaveChanges();


                CredentialTypeXCredentialProcessType c = new CredentialTypeXCredentialProcessType();
                c.Ct4Key = new Guid();
                c.Ct4Ct1Key = Guid.Parse(Request.Form["ct1Key"]);
                c.Ct4Ct2Key = g;
                _context.Add(c);
                _context.SaveChanges();
                return RedirectToAction("Edit", "CredentialProcessTypes",new {id=c.Ct4Ct2Key.ToString(),ct4Key = c.Ct4Key.ToString()});
            }
            else
            {
                CredentialTypeXCredentialProcessType c = new CredentialTypeXCredentialProcessType();
                c.Ct4Key = new Guid();
                c.Ct4Ct1Key = Guid.Parse(Request.Form["ct1Key"]);
                c.Ct4Ct2Key = Guid.Parse(Request.Form["Ct2Processes"].ToString());
                _context.Add(c);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
