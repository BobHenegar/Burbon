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
using ProjectMoonshine.Helpers;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Controllers
{
    public class PhonesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly PhoneManager _phoneManager;

        public PhonesController(ApplicationDbContext context,
            PhoneManager phoneManager)
        {
            _phoneManager = phoneManager;
            _context = context;
        }

        // GET: Phones
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
            return View(_phoneManager.GetAllPhones(searchString, sortOrder).ToPagedList(currentPage, GlobalConstants.PageSize));
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .Where(m => m.PhoKey == id)
                .FirstOrDefaultAsync();
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create(Guid? id)
        {
            if (id.HasValue)
            {
                SetRefererInfo("Emails");
                ViewData["PhoPerKey"] = new SelectList(_context.People.Where(x => x.PerKey == id.Value).Select(s => new { s.PerKey, Text = s.PerLastName + ", " + s.PerFirstName }), "PerKey", "Text");
                ViewData["PhoPerKeyHasValue"] = true;
            }
            else
            {
                ViewData["PhoPerKey"] = new SelectList(_context.People.Select(s => new { s.PerKey, Text = s.PerLastName + ", " + s.PerFirstName }), "PerKey", "Text");
                ViewData["PhoPerKeyHasValue"] = false;
            }
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                phone.PhoKey = Guid.NewGuid();
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!string.IsNullOrEmpty(phone.RefAction))
            {
                return RedirectToAction(phone.RefAction, phone.RefController, new { id = phone.RefId });
            }
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            SetRefererInfo("Emails");
            ViewData["PhoPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", phone.PhoPerKey);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Phone phone)
        {
            if (id != phone.PhoKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!PhoneExists(phone.PhoKey))
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
                if (!string.IsNullOrEmpty(phone.RefAction))
                {
                    return RedirectToAction(phone.RefAction, phone.RefController, new { id = phone.RefId });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", phone.PhoPerKey);
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.Person)
                .Where(m => m.PhoKey == id)
                .FirstOrDefaultAsync();
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var phone = await _context.Phones.FindAsync(id);
            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(Guid id)
        {
            return _context.Phones.Any(e => e.PhoKey == id);
        }
    }
}
