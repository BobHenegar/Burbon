using System;
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
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class EmailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailManager _emailManager;

        public EmailsController(ApplicationDbContext context,
            EmailManager emailManager)
        {
            _context = context;
            _emailManager = emailManager;
        }

        // GET: Emails
        public async Task<IActionResult> Index(string sortOrder, string lastOrder, string currentFilter, string searchString, int? page)
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
            return View(_emailManager.GetAllEmails(searchString, sortOrder).ToPagedList(currentPage, GlobalConstants.PageSize));
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .FirstOrDefaultAsync(m => m.EmlKey == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            ViewData["EmlPerKey"] = new SelectList(_context.People.Select(s => new { s.PerKey, Text = s.PerLastName + ", " + s.PerFirstName }), "PerKey", "Text");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmlKey,EmlPerKey,EmlAddress,EmlIsPrimaryEmail,EmlIsWorkEmail,EmlIsPersonalEmail,EmlAddDate,EmlChangeDate,EmlAddUser,EmlChangeUser")] Email email)
        {
            if (ModelState.IsValid)
            {
                email.EmlKey = Guid.NewGuid();
                _context.Add(email);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(email);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            SetRefererInfo();
            return View(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EmailViewModel model)
        {
            if (id != model.EmlKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ob = await _context.FindAsync<Email>(model.EmlKey);
                    ob.EmlAddress = model.EmlAddress;
                    ob.EmlIsPersonalEmail = model.EmlIsPersonalEmail;
                    ob.EmlIsPrimaryEmail = model.EmlIsPrimaryEmail;
                    ob.EmlIsWorkEmail = model.EmlIsWorkEmail;
                    _context.Update(ob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(model.EmlKey))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (!string.IsNullOrEmpty(model.RefAction))
                {
                    return RedirectToAction(model.RefAction, model.RefController, new { id = model.RefId });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .FirstOrDefaultAsync(m => m.EmlKey == id);
            if (email == null)
            {
                return NotFound();
            }

            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var email = await _context.Emails.FindAsync(id);
            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(Guid id)
        {
            return _context.Emails.Any(e => e.EmlKey == id);
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
                ViewBag.RefController = "Emails";
                ViewBag.RefId = "";
                ViewBag.RefText = "List";
                ViewBag.RefAction = "Index";

            }
        }
    }
}
