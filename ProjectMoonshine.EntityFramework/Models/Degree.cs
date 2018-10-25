using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Degree : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Deg";
        [Key]
        public Guid DegKey { get; set; }

        [DisplayName("Customer")]
        public Guid DegCusKey { get; set; }

        [DisplayName("Institution")]
        public string DegInstitution { get; set; }

        [DisplayName("Graduation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DegDate { get; set; }

        [DisplayName("Add User")]
        public string DegAddUser { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DegAddDate { get; set; }

        [DisplayName("Change User")]
        public string DegChangeUser { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DegChangeDate { get; set; }

        [DisplayName("Degree Type")]
        public Guid DegDetKey { get; set; }

        [ForeignKey("DegDetKey")]
        public virtual DegreeType DegreeType { get; set; }

        [ForeignKey("DegCusKey")]
        public virtual Customer Customer { get; set; }

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                if (!ValidateRequiredFields()) return false;
                if (!CommonUtils.IsGuidValid(DegKey)) DegKey = Guid.NewGuid();

                await context.AddAsync(this);
                await context.SaveChangesAsync();

                return await UpdateRequirements(context);
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
                if (!ValidateRequiredFields(true)) return false;

                Guid originalCusKey = await context.Degrees
                    .AsNoTracking()
                    .Where(d => d.DegKey == DegKey)
                    .Select(d => d.DegCusKey)
                    .FirstOrDefaultAsync();

                context.Update(this);
                await context.SaveChangesAsync();

                if (!await UpdateRequirements(context)) return false;

                if (!CommonUtils.IsGuidValid(originalCusKey) || originalCusKey == DegCusKey) return true;
                return await UpdateRequirements(context);
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

        private async Task<bool> UpdateRequirements(ApplicationDbContext context)
        {
            try
            {
                List<CredentialRequirement> requirements = await
                    CredentialingUtils.GetCredentialRequirementsFromCustomerKey(context, DegCusKey);
                  
                if (requirements == null || requirements.Count <= 0) return true;

                foreach (CredentialRequirement credentialRequirement in requirements)
                {
                    if (credentialRequirement.CredentialRequirementType.Ct3DetKey != DegDetKey 
                        || credentialRequirement.Cr3DataKey.IsGuidValid() 
                        || credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid() 
                        || !CredentialingUtils.IsUnlocked(credentialRequirement.Status.State)) continue;

                    credentialRequirement.Cr3DataKey = DegKey;
                    if (!await credentialRequirement.UpdateAsync(context)) return false;
                }

                return true;
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

        private bool ValidateRequiredFields(bool isUpdate = false)
        {
            return CommonUtils.IsGuidValid(DegCusKey) 
                   && CommonUtils.IsGuidValid(DegDetKey) 
                   && CommonUtils.IsDateTimeOffsetValid(DegDate) 
                   && (!isUpdate || CommonUtils.IsGuidValid(DegKey));
        }
    }
}
