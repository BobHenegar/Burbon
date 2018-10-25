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
    public class ExamScore : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Exs";


        [Key]
        public Guid ExsKey { get; set; }


        [DisplayName("Exam")]
        public Guid ExsExmKey { get; set; }


        [DisplayName("Customer")]
        public Guid ExsCusKey { get; set; }

        [DisplayName("Exam Date")]
        public DateTimeOffset ExsDate { get; set; }

        [DisplayName("Total Possible")]
        public int ExsTotalPossible { get; set; }
        [DisplayName("Total Received")]
        public int ExsTotalReceived { get; set; }

        [DisplayName("Success Flag")]
        public bool ExsSuccessFlag { get; set; }


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ExsAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? ExsChangeDate { get; set; }

        [DisplayName("Added By")]
        public string ExsAddUser { get; set; }


        [DisplayName("Changed By")]
        public string ExsChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("ExsCusKey")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("ExsExmKey")]
        public virtual Exam Exam { get; set; }
        #endregion Navigation Properties

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                if (!ValidateFields()) return false;

                if (!CommonUtils.IsGuidValid(ExsKey)) ExsKey = Guid.NewGuid();
                await context.AddAsync(this);
                await context.SaveChangesAsync();

                return await UpdateRequirementsAsync(context, ExsCusKey);
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

                context.Update(this);
                await context.SaveChangesAsync();

                if (!await UpdateRequirementsAsync(context, ExsCusKey)) return false;

                Guid originalCustomerKey = await context.ExamScores
                    .AsNoTracking()
                    .Where(e => e.ExsKey == ExsKey)
                    .Select(e => ExsCusKey)
                    .FirstOrDefaultAsync();

                if (originalCustomerKey == ExsCusKey) return true;

                return await UpdateRequirementsAsync(context, originalCustomerKey);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool ValidateFields(bool isUpdate= false)
        {
            return CommonUtils.IsGuidValid(ExsCusKey) && CommonUtils.IsGuidValid(ExsExmKey) &&
                   CommonUtils.IsDateTimeOffsetValid(ExsDate) &&
                   (!isUpdate || CommonUtils.IsGuidValid(ExsKey));
        }

        private async Task<bool> UpdateRequirementsAsync(ApplicationDbContext context, Guid customerKey)
        {
            try
            {
                List<CredentialRequirement> requirements = await CredentialingUtils.GetCredentialRequirementsFromCustomerKey(context, customerKey);

                if (requirements == null || requirements.Count <= 0) return true;
                foreach (CredentialRequirement credentialRequirement in requirements)
                {
                    if (ExsExmKey != credentialRequirement.CredentialRequirementType.Ct3ExmKey ||
                        credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid()) continue;
                    credentialRequirement.Cr3ExsKey = ExsKey;
                    if (!await credentialRequirement.UpdateAsync(context)) return false;
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
