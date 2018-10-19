using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Helpers;

namespace ProjectMoonshine.Managers
{
    public class FileUploadManager
    {
        private readonly ApplicationDbContext _context;
        public FileUploadManager(ApplicationDbContext context)
        {
            context = _context;
        }

        // <inheritdoc />
        public IQueryable<FileUpload> GetAllFiles(string searchString, string sortOrder)
        {
            //Defer query executing until our sorting and pagination is finished
            var fileUploads = _context
                .FileUploads
                .AsNoTracking()
                .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                fileUploads = fileUploads.Where(s => s.FupFileName.ToLower().Contains(searchString)
                    || s.FupFileDescription.ToLower().Contains(searchString));
            }
            return fileUploads;
        }

        // <inheritdoc />
        public async Task<FileUpload> GetFileUploadByIdAsync(Guid fupKey)
        {
            var result = await _context.FileUploads.AsNoTracking().Where(x => x.FupKey == fupKey).FirstAsync();
            return result;
        }
    }
}
