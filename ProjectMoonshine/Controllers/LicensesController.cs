using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.Controllers
{
    public class LicensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LicensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Licenses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Licenses.Include(l => l.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Licenses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.Licenses
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.IxlKey == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // GET: Licenses/Create
        public IActionResult Create(Guid? requirementKey = null, Guid? customerKey = null)
        {

            ViewData["IxlCusKey"] = new SelectList(_context.People
                            .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName }), "PerCusKey", "Text");

            if (requirementKey.IsGuidValid()) ViewData["requirementKey"] = requirementKey;

            if (!customerKey.IsGuidValid()) return View();

            ViewData["customerKey"] = customerKey;
            SelectListItem selected = ((SelectList) ViewData["IxlCusKey"])?.FirstOrDefault(x => x.Value == customerKey.ToString());
            if (selected != null) selected.Selected = true;





            return View();
        }

        // POST: Licenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IxlKey,IxlCusKey,IxlState,IxlLicenseNo,IxlLicenseName,IxlIssueDate,IxlExpireDate,IxlAddUser,IxlAddDate,IxlChangeUser,IxlChangeDate")] License license)
        {
            if (ModelState.IsValid)
            {
                if (await license.AddAsync(_context))
                {
                    if (Request.Form["source"].ToString().Equals("Requirement"))
                    {
                        return RedirectToAction("Edit", "CredentialRequirements",new {id = Request.Form["requirementKey"].ToString()});
                    }

                    if (!Request.Form["source"].ToString().Equals("Person")) return RedirectToAction(nameof(Index));

                    Guid.TryParse(Request.Form["customerKey"].ToString(), out Guid customerKey);

                    if (customerKey == Guid.Empty) return RedirectToAction(nameof(Index));

                    Guid personKey = await _context.People
                        .AsNoTracking()
                        .Where(p => p.PerCusKey == customerKey)
                        .Select(p => p.PerKey)
                        .FirstOrDefaultAsync();

                    return personKey != Guid.Empty ? RedirectToAction("Details", "People", new {id = personKey}) : RedirectToAction(nameof(Index));
                }
                    
            }
            ViewData["IxlCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", license.IxlCusKey);
            return View(license);
        }

        // GET: Licenses/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid? requirementKey = null, Guid? customerKey = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.Licenses.FindAsync(id);
            if (license == null)
            {
                return NotFound();
            }
            ViewData["IxlCusKey"] = new SelectList(_context.People
                .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName }), "PerCusKey", "Text", license.IxlCusKey);

            if (requirementKey.IsGuidValid()) ViewData["requirementKey"] = requirementKey;

            if (!customerKey.IsGuidValid()) return View();

            if (customerKey.IsGuidValid() )ViewData["customerKey"] = customerKey;
          return View(license);
        }

        // POST: Licenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IxlKey,IxlCusKey,IxlState,IxlLicenseNo,IxlLicenseName,IxlIssueDate,IxlExpireDate,IxlAddUser,IxlAddDate,IxlChangeUser,IxlChangeDate")] License license)
        {
            if (id != license.IxlKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await license.UpdateAsync(_context))
                    {
                        if (Request.Form["source"].ToString().Equals("Requirement"))
                        {
                            return RedirectToAction("Edit", "CredentialRequirements",
                                new {id = Request.Form["requirementKey"].ToString()});
                        }

                        if (!Request.Form["source"].ToString().Equals("Person")) return RedirectToAction(nameof(Index));

                        Guid.TryParse(Request.Form["customerKey"].ToString(), out Guid customerKey);

                        if (customerKey == Guid.Empty) return RedirectToAction(nameof(Index));

                        Guid personKey = await _context.People
                            .AsNoTracking()
                            .Where(p => p.PerCusKey == customerKey)
                            .Select(p => p.PerKey)
                            .FirstOrDefaultAsync();

                        return personKey != Guid.Empty
                            ? RedirectToAction("Details", "People", new {id = personKey})
                            : RedirectToAction(nameof(Index));
                    }
                        
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseExists(license.IxlKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewData["IxlCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", license.IxlCusKey);
            return View(license);
        }

        // GET: Licenses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var license = await _context.Licenses
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.IxlKey == id);
            if (license == null)
            {
                return NotFound();
            }

            return View(license);
        }

        // POST: Licenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var license = await _context.Licenses.FindAsync(id);
            _context.Licenses.Remove(license);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseExists(Guid id)
        {
            return _context.Licenses.Any(e => e.IxlKey == id);
        }
    }
}
