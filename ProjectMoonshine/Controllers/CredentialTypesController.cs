using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.Models;

namespace ProjectMoonshine.Controllers
{
    public class CredentialTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CredentialTypes.ToListAsync());
        }

        // GET: CredentialTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialType = await _context.CredentialTypes
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
            return View();
        }

        // POST: CredentialTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ct1Key,Ct1Credential,Ct1Description,Ct1StartDate,Ct1EndDate,Ct1RenewCt2Key,Ct1Order,Ct1AddDate,Ct1ChangeDate,Ct1AddUser,Ct1ChangeUser,RowVersion")] CredentialType credentialType)
        {
            if (ModelState.IsValid)
            {
                credentialType.Ct1Key = Guid.NewGuid();
                _context.Add(credentialType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(credentialType);
        }

        // GET: CredentialTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialType = await _context.CredentialTypes.FindAsync(id);
            if (credentialType == null)
            {
                return NotFound();
            }
            return View(credentialType);
        }

        // POST: CredentialTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ct1Key,Ct1Credential,Ct1Description,Ct1StartDate,Ct1EndDate,Ct1RenewCt2Key,Ct1Order,Ct1AddDate,Ct1ChangeDate,Ct1AddUser,Ct1ChangeUser,RowVersion")] CredentialType credentialType)
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
            return View(credentialType);
        }

        // GET: CredentialTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialType = await _context.CredentialTypes
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
    }
}
