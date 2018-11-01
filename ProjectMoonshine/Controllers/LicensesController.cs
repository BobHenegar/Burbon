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
using ProjectMoonshine.Helpers;

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
        public async Task<IActionResult> Create(Guid? requirementKey = null, Guid? customerKey = null)
        {

            await ControllerUtils.SetRequirementDataCustomerDropdownAndRedirectFieldsAsync(this, _context, "IxlCusKey", requirementKey, customerKey);
            
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
                    return await ControllerUtils.RedirectFromRequirementDataAsync(this, _context);
                }
                    
            }
            await ControllerUtils.SetRequirementDataCustomerDropdownAndRedirectFieldsAsync(this, _context, "IxlCusKey", null, null, license.IxlCusKey);
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

            await ControllerUtils.SetRequirementDataCustomerDropdownAndRedirectFieldsAsync(this, _context, "IxlCusKey", requirementKey, customerKey, license.IxlCusKey);
            
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
                        return await ControllerUtils.RedirectFromRequirementDataAsync(this, _context);
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
            await ControllerUtils.SetRequirementDataCustomerDropdownAndRedirectFieldsAsync(this, _context, "IxlCusKey", null, null, license.IxlCusKey);
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

        public async Task<IActionResult> GoBack(Guid? requirementKey = null, Guid? customerKey = null)
        {
            return await ControllerUtils.RedirectToRequirementCustomerOrDefault(this, _context, requirementKey, customerKey);
        }
    }
}
