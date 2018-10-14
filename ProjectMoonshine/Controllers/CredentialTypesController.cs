using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

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
            var applicationDbContext = _context.CredentialTypes
                .Include(c => c.InitCredentialProcessType)
                .Include(c => c.InitStatusType)
                .Include(c => c.RenewStatusType)
                .Include(c => c.RenewCredentialProcessType);
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
        public async Task<IActionResult> Create(CredentialType credentialType)
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
            //[Bind("Ct1Key,Ct1Credential,Ct1Description,Ct1StartDate,Ct1EndDate,Ct1InitCt2Key,Ct1InitCt6Key,Ct1RenewCt2Key,Ct1RenewCt6Key,Ct1Order,Ct1AddDate,Ct1ChangeDate,Ct1AddUser,Ct1ChangeUser")]
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
    }
}
