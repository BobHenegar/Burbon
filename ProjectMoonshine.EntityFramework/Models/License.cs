using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class License : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ixl";

        [Key]
        public Guid IxlKey { get; set; }

        [DisplayName("Customer")]
        public Guid IxlCusKey { get; set; }

        
        [DisplayName("State")]
        public string IxlState { get; set; }

        [DisplayName("License No")]
        public string IxlLicenseNo { get; set; }

        [DisplayName("License Name")]
        public string IxlLicenseName { get; set; }

        [DisplayName("Issue Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlIssueDate { get; set; }

        [DisplayName("Expire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlExpireDate { get; set; }

        [DisplayName("Add User")]
        public string IxlAddUser { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlAddDate { get; set; }

        [DisplayName("Change User")]
        public string IxlChangeUser { get; set; }

        [DisplayName("Change Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? IxlChangeDate { get; set; }

        [ForeignKey("IxlCusKey")]
        public virtual Customer Customer { get; set; }

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                if (!ValidateFields()) return false;
                if (!CommonUtils.IsGuidValid(IxlKey)) IxlKey = Guid.NewGuid();

                await context.AddAsync(this);
                await context.SaveChangesAsync();


                return await UpdateRequirementsAsync(context, IxlCusKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private bool ValidateFields(bool isUpdate = false)
        {
            return CommonUtils.IsGuidValid(IxlCusKey) 
                   && CommonUtils.IsDateTimeOffsetValid(IxlIssueDate) 
                   && CommonUtils.IsDateTimeOffsetValid(IxlExpireDate) 
                   && (!isUpdate || CommonUtils.IsGuidValid(IxlKey));
        }

        private async Task<bool> UpdateRequirementsAsync(ApplicationDbContext context, Guid customerKey)
        {
            try
            {
                List<CredentialRequirement> requirements = await CredentialingUtils.GetCredentialRequirementsFromCustomerKey(context, customerKey);

                if (requirements == null || requirements.Count <= 0) return true;
                foreach (CredentialRequirement credentialRequirement in requirements)
                {
                    if (credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid() 
                        || (credentialRequirement.Status?.State != null && !CredentialingUtils.IsUnlocked(credentialRequirement.Status.State)) 
                        || IxlExpireDate < DateTimeOffset.Now) continue;

                    credentialRequirement.Cr3IxlKey = IxlKey;
                    if (! await credentialRequirement.UpdateAsync(context)) return false;
                }
                return true;
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
                if (!ValidateFields()) return false;

                Guid originalCusKey = await context.Licenses
                    .AsNoTracking()
                    .Where(l => l.IxlKey == IxlKey)
                    .Select(l => l.IxlCusKey)
                    .FirstOrDefaultAsync();

                context.Update(this);
                await context.SaveChangesAsync();

                if (! await UpdateRequirementsAsync(context, IxlCusKey)) return false;

                if (!CommonUtils.IsGuidValid(originalCusKey) || originalCusKey == IxlCusKey) return true;

                return await UpdateRequirementsAsync(context, originalCusKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
