using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Helpers;
using ProjectMoonshine.Managers;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Controllers
{
    public class FileUploadsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly FileUploadManager _fileUploadManager;

        public FileUploadsController(ApplicationDbContext context
            ,FileUploadManager fileUploadManager)
        {
            _fileUploadManager = fileUploadManager;
            _context = context;
        }

        // GET: FileUploads/People/5
        [HttpGet]
        [Route("FileUploads/People/{perKey:guid}")]
        public async Task<IActionResult> Index(Guid perKey)
        {
            ViewData["PerKey"] = perKey;
            var uploads = _context
                .FileUploads
                .AsNoTracking()
                .Include(f => f.Person)
                .Where(f => f.FupPerKey == perKey)
                .Select(s => new FileUpload
                {
                    FupKey = s.FupKey,
                    FupPerKey = s.FupPerKey,
                    FupFileName = s.FupFileName,
                    FupFileType = s.FupFileType,
                    FupFileDescription = s.FupFileDescription,
                    Person = s.Person
                });
            return View(await uploads.ToListAsync());
        }

        // GET: FileUploads/View/5
        [HttpGet]
        [Route("FileUploads/View/{fupKey:guid}")]
        public async Task<IActionResult> View(Guid fupKey)
        {
            var fileUpload = await _context
                .FileUploads
                .AsNoTracking()
                .Where(m => m.FupKey == fupKey)
                .FirstAsync();
            byte[] byteArray = fileUpload.FupFileData;
            MemoryStream pdfStream = new MemoryStream();
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            Response.ContentType = "application/text";
            Response.Headers.Add("Content-Disposition", $"filename=\"{fileUpload.FupFileName + fileUpload.FupFileType}\"");
            return new FileStreamResult(pdfStream, "plain/text");
        }

        // GET: FileUploads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("FileUploads/Create/{perKey:guid}")]
        public async Task<IActionResult> Create(Guid perKey, IFormFile file, FileUploadViewModel model)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileType = Path.GetExtension(file.FileName);
            if(GlobalConstants.AcceptedFileTypes.Any(a => a == fileType))
            {
                ModelState.AddModelError("file", "File was not of accepted type: " + string.Join(", ", GlobalConstants.AcceptedFileTypes.Select(s => s)));
                return View();
            }
            var fileStream = await file.ReadByteArrayAsync();
            var entity = _context.FileUploads.Add(
                new FileUpload()
                {
                    FupPerKey = perKey,
                    FupFileData = fileStream,
                    FupFileName = fileName,
                    FupFileType = fileType,
                    FupFileDescription = model.FupFileDescription
                }
            );
            await _context.SaveChangesAsync();
            return RedirectToAction("People", "FileUploads", new { id = perKey});
        }

        // GET: FileUploads/Create
        [HttpGet]
        [Route("FileUploads/Create/{perKey:guid}")]
        public IActionResult Create(Guid perKey)
        {
            ViewData["PerKey"] = perKey;
            return View();
        }

        // GET: FileUploads/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            var perKey = fileUpload.FupPerKey;
            _context.FileUploads.Remove(fileUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction("People", "FileUploads", new { id = perKey });
        }
    }
}
