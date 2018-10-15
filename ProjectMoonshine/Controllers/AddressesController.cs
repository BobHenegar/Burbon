using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Helpers;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Controllers
{
    public class AddressesController : Controller
    {
        private readonly AddressManager _addressManager;
        private readonly ApplicationDbContext _context;
        public AddressesController(ApplicationDbContext context,
            AddressManager addressManager)
        {
            _context = context;
            _addressManager = addressManager;
        }

        // GET: Addresses
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
            return View(_addressManager.GetAllAddresses(searchString, sortOrder).ToPagedList(currentPage, GlobalConstants.PageSize));
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Person)
                .Where(x => x.AdrKey == id)
                .FirstOrDefaultAsync();
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["AdrPerKey"] = new SelectList(_context.People.Select(s => new { s.PerKey, Text = s.PerFirstMiddleLast }), "AdrPerKey", "Text");
            ViewData["StateList"] = GetStateSelectList(string.Empty);
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdrKey,AdrPerKey,AdrCity,AdrState,AdrZip,AdrAddress1,AdrAddress2,AdrAddress3,AdrIsPrimaryAddress,AdrIsMailingAddress,AdrIsWorkAddress,AdrIsHomeAddress,AdrAddDate,AdrChangeDate,AdrAddUser,AdrChangeUser")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.AdrKey = Guid.NewGuid();
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdrPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", address.AdrPerKey);
            ViewData["StateList"] = GetStateSelectList(address.AdrState);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }
            SetRefererInfo();
            ViewData["AdrPerKey"] = new SelectList(_context.People, "PerKey", "PerFirstMiddleLast", address.AdrPerKey);
            ViewData["StateList"] = GetStateSelectList(address.AdrState);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AdrKey,AdrPerKey,AdrCity,AdrState,AdrZip,AdrAddress1,AdrAddress2,AdrAddress3,AdrIsPrimaryAddress,AdrIsMailingAddress,AdrIsWorkAddress,AdrIsHomeAddress,AdrAddDate,AdrChangeDate,AdrAddUser,AdrChangeUser")] Address address)
        {
            if (id != address.AdrKey)
            {
                return NotFound();
            }

            if (address.AdrIsMailingAddress)
            {
                var Addresses = await _context
                    .Addresses
                    .AsTracking()
                    .Where(x => x.AdrPerKey == address.AdrPerKey
                        && x.AdrPerKey != address.AdrPerKey).ToListAsync();
                Addresses.ForEach(a => a.AdrIsPrimaryAddress = false);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AdrKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (!string.IsNullOrEmpty(address.RefAction))
                {
                    return RedirectToAction(address.RefAction, address.RefController, new { id = address.RefId });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdrPerKey"] = new SelectList(_context.People, "PerKey", "PerKey", address.AdrPerKey);
            ViewData["StateList"] = GetStateSelectList(address.AdrState);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.Person)
                .Where(m => m.AdrKey == id)
                .FirstOrDefaultAsync(m => m.AdrKey == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(Guid id)
        {
            return _context.Addresses.Any(e => e.AdrKey == id);
        }


        private void SetRefererInfo()
        {
            var refererHeader = Request.Headers[GlobalConstants.RefererHeader];
            if (!string.IsNullOrEmpty(refererHeader))
            {
                var localPath = new Uri(refererHeader).LocalPath;
                ViewBag.RefAction = localPath.GetActionFromUrl();
                ViewBag.RefController = localPath.GetControllerFromUrl();
                ViewBag.RefId = GlobalConstants.IsValidGuidRegex.GetRegexMatch(localPath);
            }
            else
            {
                ViewBag.RefController = "Addresses";
                ViewBag.RefId = "";
                ViewBag.RefText = "List";
                ViewBag.RefAction = "Index";

            }
        }
        public SelectList GetStateSelectList(string currentSelect)
        {
            return new SelectList(GlobalConstants.StateList.Select(s => new { State = s }), "State", "State", string.IsNullOrEmpty(currentSelect) ? "Please Select" : currentSelect);
        }
    }
}
