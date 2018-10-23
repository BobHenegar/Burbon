using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Controllers
{
    public class CredentialRequirementTemplatesController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public CredentialRequirementTemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CredentialRequirementTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.CredentialRequirementTemplates.ToListAsync());
        }

        // GET: CredentialRequirementTemplates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirementTemplate = await _context.CredentialRequirementTemplates
                .FirstOrDefaultAsync(m => m.Ct8Key == id);
            if (credentialRequirementTemplate == null)
            {
                return NotFound();
            }

            return View(credentialRequirementTemplate);
        }

        // GET: CredentialRequirementTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CredentialRequirementTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(ModelBindingConstants.CredentialRequirementTemplate)] CredentialRequirementTemplate credentialRequirementTemplate)
        {
            if (ModelState.IsValid)
            {
                credentialRequirementTemplate.Ct8Key = Guid.NewGuid();
                _context.Add(credentialRequirementTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(credentialRequirementTemplate);
        }

        // GET: CredentialRequirementTemplates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirementTemplate = await _context.CredentialRequirementTemplates.FindAsync(id);
            if (credentialRequirementTemplate == null)
            {
                return NotFound();
            }
            return View(credentialRequirementTemplate);
        }

        // POST: CredentialRequirementTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(ModelBindingConstants.CredentialRequirementTemplate)] CredentialRequirementTemplate credentialRequirementTemplate)
        {
            if (id != credentialRequirementTemplate.Ct8Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentialRequirementTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CredentialRequirementTemplateExists(credentialRequirementTemplate.Ct8Key))
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
                return RedirectToAction(nameof(Index));
            }
            return View(credentialRequirementTemplate);
        }

        // GET: CredentialRequirementTemplates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentialRequirementTemplate = await _context.CredentialRequirementTemplates
                .FirstOrDefaultAsync(m => m.Ct8Key == id);
            if (credentialRequirementTemplate == null)
            {
                return NotFound();
            }

            return View(credentialRequirementTemplate);
        }

        // POST: CredentialRequirementTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var credentialRequirementTemplate = await _context.CredentialRequirementTemplates.FindAsync(id);
            _context.CredentialRequirementTemplates.Remove(credentialRequirementTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CredentialRequirementTemplateExists(Guid id)
        {
            return _context.CredentialRequirementTemplates.Any(e => e.Ct8Key == id);
        }
    }
}
