using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class FileUpload : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Fup";

        [Key]
        public Guid FupKey { get; set; }

        [DisplayName("Person")]
        public Guid FupPerKey { get; set; }

        [DisplayName("File")]
        public byte[] FupFileData { get; set; }

        [DisplayName("File Type")]
        public string FupFileType { get; set; }

        [DisplayName("File Name")]
        public string FupFileName { get; set; }
        [DisplayName("Description")]
        public string FupFileDescription { get; set; }

        [DisplayName("Document Type")]
        public Guid? FupDtpKey { get; set; }

        [DisplayName("Added By")]
        public string FupAddUser { get; set; }

        [DisplayName("Changed By")]
        public string FupChangeUser { get; set; }
        [DisplayName("Add Date")]
        public DateTimeOffset FupAddDate { get; set; }

        [DisplayName("Change Date")]
        public DateTimeOffset? FupChangeDate { get; set; }


        [ForeignKey("FupPerKey")]
        public virtual Person Person { get; set; }

        [ForeignKey("FupDtpKey")]
        public virtual DocumentType DocumentType { get; set; }

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                if (!ValidateFields()) return false;
                if (!CommonUtils.IsGuidValid(FupKey)) FupKey = Guid.NewGuid();

                await context.AddAsync(this);
                await context.SaveChangesAsync();

                if (!FupDtpKey.IsGuidValid()) return true;

                return await UpdateRequirementsAsync(context, FupPerKey);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }


        public async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {
                if (!ValidateFields(true)) return false;


                Guid originalPerKey = await context.FileUploads
                    .AsNoTracking()
                    .Where(f => f.FupKey == FupKey)
                    .Select(f => f.FupPerKey)
                    .FirstOrDefaultAsync();

                context.Update(this);
                await context.SaveChangesAsync();

                if (!FupDtpKey.IsGuidValid()) return true;

                if (! await UpdateRequirementsAsync(context, FupPerKey)) return false;

                if (originalPerKey == FupPerKey) return true;

                return await UpdateRequirementsAsync(context, originalPerKey);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }
        private bool ValidateFields(bool isUpdate = false)
        {
            try
            {
                return (CommonUtils.IsGuidValid(FupPerKey) && (!isUpdate || CommonUtils.IsGuidValid(FupKey)));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> UpdateRequirementsAsync(ApplicationDbContext context, Guid personKey)
        {
            try
            {
                if (!FupDtpKey.IsGuidValid()) return true;

                Person person = await context.People.FindAsync(personKey);
                if (person == null) return false;

                return await CredentialingUtils.UpdateRequirementsFromDataAsync(context, person.PerCusKey,
                    GlobalConstants.RequirementTemplateCode.Document, true);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }


    }
}
