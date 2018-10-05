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
    public class EmailTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmailTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmailTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailTypes.ToListAsync());
        }

        // GET: EmailTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailType = await _context.EmailTypes
                .FirstOrDefaultAsync(m => m.EltKey == id);
            if (emailType == null)
            {
                return NotFound();
            }

            return View(emailType);
        }

        // GET: EmailTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EltKey,EltType,EltAddDate,EltModifiedDate,EltAddUser,EltModifiedUser")] EmailType emailType)
        {
            if (ModelState.IsValid)
            {
                emailType.EltKey = Guid.NewGuid();
                _context.Add(emailType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailType);
        }

        // GET: EmailTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailType = await _context.EmailTypes.FindAsync(id);
            if (emailType == null)
            {
                return NotFound();
            }
            return View(emailType);
        }

        // POST: EmailTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EltKey,EltType,EltAddDate,EltModifiedDate,EltAddUser,EltModifiedUser")] EmailType emailType)
        {
            if (id != emailType.EltKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailTypeExists(emailType.EltKey))
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
            return View(emailType);
        }

        // GET: EmailTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailType = await _context.EmailTypes
                .FirstOrDefaultAsync(m => m.EltKey == id);
            if (emailType == null)
            {
                return NotFound();
            }

            return View(emailType);
        }

        // POST: EmailTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var emailType = await _context.EmailTypes.FindAsync(id);
            _context.EmailTypes.Remove(emailType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailTypeExists(Guid id)
        {
            return _context.EmailTypes.Any(e => e.EltKey == id);
        }
    }
}
