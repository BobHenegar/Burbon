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
    public class EventRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventRegistrations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventRegistrations.Include(e => e.Customer).Include(e => e.Event);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventRegistrations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventRegistration = await _context.EventRegistrations
                .Include(e => e.Customer)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EvrKey == id);
            if (eventRegistration == null)
            {
                return NotFound();
            }

            return View(eventRegistration);
        }

        // GET: EventRegistrations/Create
        public IActionResult Create()
        {
            ViewData["EvrCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtDescription");
            return View();
        }

        // POST: EventRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EvrKey,EvrEvtKey,EvrCusKey,EvrDropDate,EvrAddDate,EvrChangeDate,EvrAddUser,EvrChangeUser,RowVersion")] EventRegistration eventRegistration)
        {
            if (ModelState.IsValid)
            {
                eventRegistration.EvrKey = Guid.NewGuid();
                _context.Add(eventRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvrCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", eventRegistration.EvrCusKey);
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtDescription", eventRegistration.EvrEvtKey);
            return View(eventRegistration);
        }

        // GET: EventRegistrations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventRegistration = await _context.EventRegistrations.FindAsync(id);
            if (eventRegistration == null)
            {
                return NotFound();
            }
            ViewData["EvrCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", eventRegistration.EvrCusKey);
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtDescription", eventRegistration.EvrEvtKey);
            return View(eventRegistration);
        }

        // POST: EventRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EvrKey,EvrEvtKey,EvrCusKey,EvrDropDate,EvrAddDate,EvrChangeDate,EvrAddUser,EvrChangeUser,RowVersion")] EventRegistration eventRegistration)
        {
            if (id != eventRegistration.EvrKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventRegistrationExists(eventRegistration.EvrKey))
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
            ViewData["EvrCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", eventRegistration.EvrCusKey);
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtDescription", eventRegistration.EvrEvtKey);
            return View(eventRegistration);
        }

        // GET: EventRegistrations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventRegistration = await _context.EventRegistrations
                .Include(e => e.Customer)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EvrKey == id);
            if (eventRegistration == null)
            {
                return NotFound();
            }

            return View(eventRegistration);
        }

        // POST: EventRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var eventRegistration = await _context.EventRegistrations.FindAsync(id);
            _context.EventRegistrations.Remove(eventRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventRegistrationExists(Guid id)
        {
            return _context.EventRegistrations.Any(e => e.EvrKey == id);
        }
    }
}
