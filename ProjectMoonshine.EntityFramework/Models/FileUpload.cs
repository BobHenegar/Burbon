using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                Console.WriteLine(e);
                throw;
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
                Console.WriteLine(e);
                throw;
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
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<bool> UpdateRequirementsAsync(ApplicationDbContext context, Guid personKey)
        {
            try
            {
                if (!FupDtpKey.IsGuidValid()) return true;

                if (Person == null) Person = await context.People.FindAsync(personKey);
                if (Person == null) return false;

                List<CredentialRequirement> requirements = await
                    CredentialingUtils.GetCredentialRequirementsFromCustomerKey(context, Person.PerCusKey);

                if (requirements == null || requirements.Count <= 0) return true;

                foreach (CredentialRequirement credentialRequirement in requirements)
                {
                    if (!credentialRequirement.CredentialRequirementType.Template.Ct8Code.Equals(GlobalConstants
                        .RequirementTemplateCode.Document)) continue;
                    if (!credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid()
                        && credentialRequirement.CredentialRequirementType.Ct3DtpKey.IsGuidValid()
                        && (credentialRequirement.Status?.State == null
                            || CredentialingUtils.IsUnlocked(credentialRequirement.Status.State))
                        && credentialRequirement.CredentialRequirementType.Ct3DtpKey == FupDtpKey)
                    {
                        credentialRequirement.Cr3FupKey = FupKey;
                        if (!await credentialRequirement.UpdateAsync(context)) return false;
                        await context.SaveChangesAsync();
                        return true;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
