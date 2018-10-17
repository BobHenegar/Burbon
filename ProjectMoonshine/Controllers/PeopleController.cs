using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Managers;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class PeopleController : BaseController
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

            ViewData["PerKey"] = id.Value;
            var person = await _context
                .People
                .AsNoTracking()
                .Include(p => p.Customer)
                .ThenInclude(p => p.ExamScores)
                .ThenInclude(p => p.Exam)
                .Include(p => p.Customer)
                .ThenInclude(p => p.Credentials)
                .ThenInclude(p => p.CredentialType)
                .Include(p => p.Customer)
                .ThenInclude(p => p.EventRegistrations)
                .ThenInclude(p => p.Event)
                .Include(p => p.Addresses)
                .Include(p => p.Emails)
                .Include(p => p.Phones)
                .Where(m => m.PerKey == id)
                .FirstOrDefaultAsync();
            if (person == null)
            {
                return NotFound();
            }

            ViewData["ComTemplates"] = new SelectList(_context.CommunicationTemplates, "TemKey", "TemCode");
            ViewData["PerCusKey"] = person.PerCusKey;
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
        public async Task<IActionResult> Create(PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = _context.People.Add(new Person()
                {
                    PerFirstName = model.PerFirstName,
                    PerMiddleName = model.PerMiddleName,
                    PerLastName = model.PerLastName,
                    PerDateOfBirth = model.PerDateOfBirth,
                    Customer = new Customer(),
                    Addresses = new List<Address>()
                    {
                        new Address()
                        {
                            AdrAddress1 = model.AdrAddress1,
                            AdrAddress2 = model.AdrAddress2,
                            AdrAddress3 = model.AdrAddress3,
                            AdrState = model.AdrState,
                            AdrCity = model.AdrCity,
                            AdrZip = model.AdrZip,
                            AdrIsPrimary = true,
                            AdrIsHomeAddress = model.AdrIsHomeAddress,
                            AdrIsWorkAddress = model.AdrIsWorkAddress,
                            AdrIsMailingAddress = model.AdrIsMailingAddress
                        }
                    },
                    Emails = new List<Email>()
                    {
                        new Email()
                        {
                            EmlAddress = model.EmlAddress,
                            EmlIsPersonalEmail = model.EmlIsPersonalEmail,
                            EmlIsWorkEmail = model.EmlIsPersonalEmail,
                            EmlIsPrimary = true
                        }
                    },
                    Phones = new List<Phone>()
                    {
                        new Phone()
                        {
                            PhoIsHome = model.PhoIsHome,
                            PhoIsMobile = model.PhoIsMobile,
                            PhoIsPrimary = true,
                            PhoIsWork = model.PhoIsWork,
                            PhoNumber = model.PhoNumber
                        }
                    }
                });
                await _context.SaveChangesAsync();
                return View(person);
            }
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(Guid id, Person person)
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
                catch (DbUpdateConcurrencyException e)
                {
                    if (!PersonExists(person.PerKey))
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

        [HttpPost]
        public ActionResult Email()
        {

            string emlBody = _context.CommunicationTemplates
                .Where(t => t.TemKey.ToString() == Request.Form["ComTemplates"].ToString())
                .Select(t => t.TemHtml)
                .FirstOrDefault();

            var mailMessage = new MailMessage
            {
                From = new MailAddress("support@credengnext.net"),
                Subject = "Hello World",
                Body = emlBody
            };

            string emladdr = _context.Emails
                .Where(e => e.EmlPerKey.ToString() == Request.Form["perKey"].ToString() && e.EmlIsPrimary == true)
                .Select(e => e.EmlAddress)
                .FirstOrDefault();

            mailMessage.To.Add(emladdr.ToString());

            var smtpClient = new SmtpClient
            {
                Credentials = new NetworkCredential("azure_62dfa7dda67490fad6f34fdee29cbab6@azure.com", "1425Market"),
                Host = "smtp.sendgrid.net",
                Port = 587
            };

            smtpClient.Send(mailMessage);
            return RedirectToAction("Details", new{id=Request.Form["perKey"].ToString()});
        }
    }
}
