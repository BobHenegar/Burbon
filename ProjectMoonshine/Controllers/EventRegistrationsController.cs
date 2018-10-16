using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Controllers
{
    public class EventRegistrationsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly EventRegistrationManager _eventRegistrationManager;
        public EventRegistrationsController(ApplicationDbContext context,
            EventRegistrationManager eventRegistrationManager)
        {
            _eventRegistrationManager = eventRegistrationManager;
            _context = context;
        }

        // GET: EventRegistrations
        public IActionResult Index(string sortOrder, string lastOrder, string currentFilter, string searchString, int? page)
        {
            int currentPage = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "firstName";
                ViewBag.LastSortParam = sortOrder;
            }
            else
            {
                if (sortOrder == lastOrder)
                {
                    int index = sortOrder.IndexOf("_desc");
                    string cleanPath = (index < 0)
                        ? sortOrder = sortOrder + "_desc"
                        : sortOrder.Remove(index, "_desc".Length);
                }
            }
            ViewBag.LastSortParam = sortOrder;
            var result = _eventRegistrationManager.GetAllEventRegistration(searchString, sortOrder);
            return View(result.ToPagedList(currentPage, GlobalConstants.PageSize));
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
        public IActionResult Create(Guid? id)
        {
            ViewData["CusKeyHasValue"] = id.HasValue;
            if(id.HasValue)
            {
                ViewData["CusKey"] = id.Value;
            }
            ViewData["EvrCusKey"] = new SelectList(_context
                .Customers
                .Include(i => i.Person)
                .Where(x => id.HasValue ? x.CusKey == id.Value : true)
                .Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast}), "CusKey", "Name");
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName");
            return View();
        }

        // POST: EventRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventRegistration eventRegistration)
        {
            if (ModelState.IsValid)
            {
                eventRegistration.EvrKey = Guid.NewGuid();
                _context.Add(eventRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EvrCusKey"] = new SelectList(_context.Customers.Include(i => i.Person).Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }), "CusKey", "Name");
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName", eventRegistration.EvrEvtKey);
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
            ViewData["EvrCusKey"] = new SelectList(_context.Customers.Include(i => i.Person).Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }), "CusKey", "Name", eventRegistration.EvrCusKey);
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName", eventRegistration.EvrEvtKey);
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
                catch (DbUpdateConcurrencyException e)
                {
                    if (!EventRegistrationExists(eventRegistration.EvrKey))
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
            ViewData["EvrCusKey"] = new SelectList(_context.Customers.Include(i => i.Person).Select(s => new { s.CusKey, Name = s.Person.PerFirstMiddleLast }), "CusKey", "Name", eventRegistration.EvrCusKey);
            ViewData["EvrEvtKey"] = new SelectList(_context.Events, "EvtKey", "EvtName", eventRegistration.EvrEvtKey);
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
