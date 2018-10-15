using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PersonManager _personManager;

        public PeopleController(ApplicationDbContext context,
            PersonManager personManager)
        {
            _context = context;
            _personManager = personManager;
        }

        // GET: People
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
                if(sortOrder == lastOrder)
                {
                    int index = sortOrder.IndexOf("_desc");
                    string cleanPath = (index < 0)
                        ? sortOrder = sortOrder + "_desc"
                        : sortOrder.Remove(index, "_desc".Length);
                }
            }
            ViewBag.LastSortParam = sortOrder;
            var result = _personManager.GetAllPeople(searchString, sortOrder);
            return View(result.ToPagedList(currentPage, GlobalConstants.PageSize));
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Customer)
                .Include(p => p.Addresses)
                .Include(p => p.Emails)
                .Include(p => p.Phones)
                .Where(m => m.PerKey == id)
                .FirstOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["PerCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerKey,PerCusKey,PerFirstName,PerMiddleName,PerLastName,PerDateOfBirth,PerGender,PerAddDate,PerChangeDate,PerAddUser,PerChangeUser")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.PerKey = Guid.NewGuid();
                person.Customer = new Customer();
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PerCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", person.PerCusKey);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["PerCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", person.PerCusKey);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PerKey,PerCusKey,PerFirstName,PerMiddleName,PerLastName,PerDateOfBirth,PerGender,PerAddDate,PerChangeDate,PerAddUser,PerChangeUser")] Person person)
        {
            if (id != person.PerKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PerKey))
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
            ViewData["PerCusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", person.PerCusKey);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.Customer)
                .Where(m => m.PerKey == id)
                .FirstOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(Guid id)
        {
            return _context.People.Any(e => e.PerKey == id);
        }
    }
}
