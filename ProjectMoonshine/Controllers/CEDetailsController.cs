using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class CEDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CEDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CEDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CEDetails.Include(c => c.CeType).Include(c => c.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CEDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEDetail = await _context.CEDetails
                .Include(c => c.CeType)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Ce1Key == id);
            if (cEDetail == null)
            {
                return NotFound();
            }

            return View(cEDetail);
        }

        // GET: CEDetails/Create
        public IActionResult Create()
        {
            //ViewData["Ce1CetKey"] = new SelectList(_context.CEType, "CetKey", "CetCode");
            //ViewData["Ce1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey");

            CEDetailViewModel viewModel = new CEDetailViewModel(_context);
            return View(viewModel);
        }

        // POST: CEDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ce1Key,Ce1CusKey,Ce1ActivityDate,Ce1CetKey,Ce1ActivityDescription,Ce1Units,Ce1VerifiedFlag")] CEDetail cEDetail)
        {
            if (ModelState.IsValid)
            {
                if (await cEDetail.AddAsync(_context)) return RedirectToAction(nameof(Index));
                //cEDetail.Ce1Key = Guid.NewGuid();
                //_context.Add(cEDetail);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["Ce1CetKey"] = new SelectList(_context.CEType, "CetKey", "CetCode", cEDetail.Ce1CetKey);
            //ViewData["Ce1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", cEDetail.Ce1CusKey);

            CEDetailViewModel viewModel = new CEDetailViewModel(_context, cEDetail);
            return View(viewModel);
        }

        // GET: CEDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEDetail = await _context.CEDetails.FindAsync(id);
            if (cEDetail == null)
            {
                return NotFound();
            }
            //ViewData["Ce1CetKey"] = new SelectList(_context.CEType, "CetKey", "CetCode", cEDetail.Ce1CetKey);
            //ViewData["Ce1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", cEDetail.Ce1CusKey);

            CEDetailViewModel viewModel = new CEDetailViewModel(_context, cEDetail);
            return View(viewModel);
        }

        // POST: CEDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Ce1Key,Ce1CusKey,Ce1ActivityDate,Ce1CetKey,Ce1ActivityDescription,Ce1Units,Ce1VerifiedFlag")] CEDetail cEDetail)
        {
            if (id != cEDetail.Ce1Key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (await cEDetail.UpdateAsync(_context)) return RedirectToAction(nameof(Index));
                    //_context.Update(cEDetail);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CEDetailExists(cEDetail.Ce1Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["Ce1CetKey"] = new SelectList(_context.CEType, "CetKey", "CetCode", cEDetail.Ce1CetKey);
            //ViewData["Ce1CusKey"] = new SelectList(_context.Customers, "CusKey", "CusKey", cEDetail.Ce1CusKey);
            CEDetailViewModel viewModel = new CEDetailViewModel(_context, cEDetail);
            return View(viewModel);
        }

        // GET: CEDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEDetail = await _context.CEDetails
                .Include(c => c.CeType)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Ce1Key == id);
            if (cEDetail == null)
            {
                return NotFound();
            }

            return View(cEDetail);
        }

        // POST: CEDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cEDetail = await _context.CEDetails.FindAsync(id);
            _context.CEDetails.Remove(cEDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CEDetailExists(Guid id)
        {
            return _context.CEDetails.Any(e => e.Ce1Key == id);
        }
    }
}
