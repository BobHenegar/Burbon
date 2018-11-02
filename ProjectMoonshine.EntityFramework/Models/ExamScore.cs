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
        public int? ExsTotalPossible { get; set; }
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

                return await CredentialingUtils.UpdateRequirementsFromDataAsync(context, ExsCusKey, GlobalConstants.RequirementTemplateCode.Exam);
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

                Guid originalCustomerKey = await context.ExamScores
                    .AsNoTracking()
                    .Where(e => e.ExsKey == ExsKey)
                    .Select(e => ExsCusKey)
                    .FirstOrDefaultAsync();

                context.Update(this);
                await context.SaveChangesAsync();

                if (!await CredentialingUtils.UpdateRequirementsFromDataAsync(context, ExsCusKey, GlobalConstants.RequirementTemplateCode.Exam)) return false;

                if (originalCustomerKey == ExsCusKey) return true;

                return await CredentialingUtils.UpdateRequirementsFromDataAsync(context, originalCustomerKey,
                    GlobalConstants.RequirementTemplateCode.Exam);
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

        private bool ValidateFields(bool isUpdate= false)
        {
            return CommonUtils.IsGuidValid(ExsCusKey) && CommonUtils.IsGuidValid(ExsExmKey) &&
                   CommonUtils.IsDateTimeOffsetValid(ExsDate) &&
                   (!isUpdate || CommonUtils.IsGuidValid(ExsKey));
        }
    }
}
