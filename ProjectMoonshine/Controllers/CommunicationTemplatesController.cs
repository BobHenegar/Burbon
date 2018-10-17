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
    public class CommunicationTemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommunicationTemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CommunicationTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.CommunicationTemplates.ToListAsync());
        }

        // GET: CommunicationTemplates/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationTemplate = await _context.CommunicationTemplates
                .FirstOrDefaultAsync(m => m.TemKey == id);
            if (communicationTemplate == null)
            {
                return NotFound();
            }

            return View(communicationTemplate);
        }

        // GET: CommunicationTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommunicationTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TemKey,TemCode,TemDescription,TemSubject,TemHtml,TemText,TemAddDate,TemChangeDate,TemAddUser,TemChangeUser")] CommunicationTemplate communicationTemplate)
        {
            if (ModelState.IsValid)
            {
                communicationTemplate.TemKey = Guid.NewGuid();
                _context.Add(communicationTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(communicationTemplate);
        }

        // GET: CommunicationTemplates/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationTemplate = await _context.CommunicationTemplates.FindAsync(id);
            if (communicationTemplate == null)
            {
                return NotFound();
            }
            return View(communicationTemplate);
        }

        // POST: CommunicationTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TemKey,TemCode,TemDescription,TemSubject,TemHtml,TemText,TemAddDate,TemChangeDate,TemAddUser,TemChangeUser")] CommunicationTemplate communicationTemplate)
        {
            if (id != communicationTemplate.TemKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communicationTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunicationTemplateExists(communicationTemplate.TemKey))
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
            return View(communicationTemplate);
        }

        // GET: CommunicationTemplates/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationTemplate = await _context.CommunicationTemplates
                .FirstOrDefaultAsync(m => m.TemKey == id);
            if (communicationTemplate == null)
            {
                return NotFound();
            }

            return View(communicationTemplate);
        }

        // POST: CommunicationTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var communicationTemplate = await _context.CommunicationTemplates.FindAsync(id);
            _context.CommunicationTemplates.Remove(communicationTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunicationTemplateExists(Guid id)
        {
            return _context.CommunicationTemplates.Any(e => e.TemKey == id);
        }
    }
}
