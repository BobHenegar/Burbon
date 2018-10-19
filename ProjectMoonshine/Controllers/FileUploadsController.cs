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
    public class FileUploadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FileUploadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FileUploads
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FileUploads.Include(f => f.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FileUploads/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads
                .Include(f => f.Person)
                .FirstOrDefaultAsync(m => m.FupKey == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // GET: FileUploads/Create
        public IActionResult Create()
        {
            ViewData["FupPerKey"] = new SelectList(_context.People, "PerKey", "PerFirstName");
            return View();
        }

        // POST: FileUploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FupKey,FupPerKey,FupFileData,FupFileType,FupAddUser,FupChangeUser,FupAddDate,FupChangeDate")] FileUpload fileUpload)
        {
            if (ModelState.IsValid)
            {
                fileUpload.FupKey = Guid.NewGuid();
                _context.Add(fileUpload);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FupPerKey"] = new SelectList(_context.People, "PerKey", "PerFirstName", fileUpload.FupPerKey);
            return View(fileUpload);
        }

        // GET: FileUploads/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads.FindAsync(id);
            if (fileUpload == null)
            {
                return NotFound();
            }
            ViewData["FupPerKey"] = new SelectList(_context.People, "PerKey", "PerFirstName", fileUpload.FupPerKey);
            return View(fileUpload);
        }

        // POST: FileUploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FupKey,FupPerKey,FupFileData,FupFileType,FupAddUser,FupChangeUser,FupAddDate,FupChangeDate")] FileUpload fileUpload)
        {
            if (id != fileUpload.FupKey)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileUploadExists(fileUpload.FupKey))
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
            ViewData["FupPerKey"] = new SelectList(_context.People, "PerKey", "PerFirstName", fileUpload.FupPerKey);
            return View(fileUpload);
        }

        // GET: FileUploads/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads
                .Include(f => f.Person)
                .FirstOrDefaultAsync(m => m.FupKey == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // POST: FileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            _context.FileUploads.Remove(fileUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileUploadExists(Guid id)
        {
            return _context.FileUploads.Any(e => e.FupKey == id);
        }
    }
}
