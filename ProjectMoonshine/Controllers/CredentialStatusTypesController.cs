using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Controllers
{
    public class CredentialStatusTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialStatusTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialStatusTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CredentialStatusTypes.Include(c => c.State);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CredentialStatusTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialStatusType = await _context.CredentialStatusTypes
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Ct6Key == id);
            if (credentialStatusType == null)
            {
                return NotFound();
            }

            return View(credentialStatusType);
        }

        // GET: CredentialStatusTypes/Create
        public IActionResult Create()
        {
            ViewData["Ct6Cu6Key"] = new SelectList(_context.StatusStateTypes, "Cu6Key", "Cu6Code");
            return View();
        }

        // POST: CredentialStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ct6Key,Ct6Code,Ct6Description,Ct6Cu6Key,Ct6AddDate,Ct6ChangeDate,Ct6AddUser,Ct6ChangeUser")] CredentialStatusType credentialStatusType)
        {
            if (ModelState.IsValid)
            {
                credentialStatusType.Ct6Key = Guid.NewGuid();
                _context.Add(credentialStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ct6Cu6Key"] = new SelectList(_context.StatusStateTypes, "Cu6Key", "Cu6Code", credentialStatusType.Ct6Cu6Key);
            return View(credentialStatusType);
        }

        // GET: CredentialStatusTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialStatusType = await _context.CredentialStatusTypes.FindAsync(id);
            if (credentialStatusType == null)
            {
                return NotFound();
            }
            ViewData["Ct6Cu6Key"] = new SelectList(_context.StatusStateTypes, "Cu6Key", "Cu6Code", credentialStatusType.Ct6Cu6Key);
            return View(credentialStatusType);
        }

        // POST: CredentialStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ct6Key,Ct6Code,Ct6Description,Ct6Cu6Key,Ct6AddDate,Ct6ChangeDate,Ct6AddUser,Ct6ChangeUser")] CredentialStatusType credentialStatusType)
        {
            if (id != credentialStatusType.Ct6Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialStatusTypeExists(credentialStatusType.Ct6Key))
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
            ViewData["Ct6Cu6Key"] = new SelectList(_context.StatusStateTypes, "Cu6Key", "Cu6Code", credentialStatusType.Ct6Cu6Key);
            return View(credentialStatusType);
        }

        // GET: CredentialStatusTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialStatusType = await _context.CredentialStatusTypes
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Ct6Key == id);
            if (credentialStatusType == null)
            {
                return NotFound();
            }

            return View(credentialStatusType);
        }

        // POST: CredentialStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialStatusType = await _context.CredentialStatusTypes.FindAsync(id);
            _context.CredentialStatusTypes.Remove(credentialStatusType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialStatusTypeExists(Guid id)
        {
            return _context.CredentialStatusTypes.Any(e => e.Ct6Key == id);
        }
    }
}
