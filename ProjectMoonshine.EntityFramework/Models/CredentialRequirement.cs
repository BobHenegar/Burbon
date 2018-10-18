using System;
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
    [Table("CredentialRequirement")]
    public class CredentialRequirement : EntityBase
    {

        [NotMapped]
        public const string Prefix = "Cr3";

        [Key]
        public Guid Cr3Key { get; set; }

        [DisplayName("Requirement Type")]
        public Guid Cr3Ct3Key { get; set; }

        [DisplayName("Process")]
        public Guid Cr3Cr2Key { get; set; }

        //public Guid Cr3CPerKey { get; set; }

        [DisplayName("Status")]
        public Guid? Cr3Ct6Key { get; set; }

        //public DateTimeOffset Cr3StatusChangeDate { get; set; }

        //public string Cr3StatusChangeUser { get; set; }

            [DisplayName("Start Date")]
        public DateTimeOffset? Cr3StartDate { get; set; }

        [DisplayName("Due Date")]
        public DateTimeOffset? Cr3DueDate { get; set; }

        [DisplayName("Met Date")]
        public DateTimeOffset? Cr3MetDate { get; set; }

        //public string Cr3MetUser { get; set; }

        //public string Cr3MetReason { get; set; }

        //public DateTimeOffset Cr3ExtensionDate { get; set; }

        //public DateTimeOffset Cr3ExtensionGrantedDate { get; set; }

        //public string Cr3ExtensionGrantedUser { get; set; }

        //public string Cr3ExtensionGrantedReason { get; set; }

        //public string Cr3Note { get; set; }

        //public string Cr3Owner { get; set; }

        //public Guid Cr3Cr3Key { get; set; }

        //public Guid Cr3ChoiceCt3Key { get; set; }

        //public bool Cr3HideOnlineFlag { get; set; }

        [DisplayName("Requirement Data")]
        public Guid? Cr3DataKey { get; set; }

        public string Cr3AddUser { get; set; }

        public DateTimeOffset Cr3AddDate { get; set; }

        public string Cr3ChangeUser { get; set; }

        public DateTimeOffset? Cr3ChangeDate { get; set; }

        //public DateTimeOffset Cr3Extension2Date { get; set; }

        //public DateTimeOffset Cr3Extension2GrantedDate { get; set; }

        //public string Cr3Extension2GrantedUser { get; set; }

        //public string Cr3Extension2GrantedReason { get; set; }

        //public bool Cr3AttestmentFlag { get; set; }

        //public string Cr3SignatureFirstName { get; set; }

        //public string Cr3SignatureLastName { get; set; }

        //public DateTimeOffset Cr3RequestedDate { get; set; }

        //public DateTimeOffset Cr3ReceivedDate { get; set; }

        //public Int16 Cr3ExamAttempts { get; set; }

        [ForeignKey("Cr3Cr2Key")]
        public virtual CredentialProcess CredentialProcess { get; set; }

        [ForeignKey("Cr3Ct3Key")]
        public virtual CredentialRequirementType CredentialRequirementType { get; set; }

        [ForeignKey("Cr3Ct6Key")]
        public CredentialStatusType Status { get; set; }

        public virtual async Task<bool> Add(ApplicationDbContext context)
        {
            try
            {
                if (!CommonUtils.IsGuidValid(Cr3Ct3Key)) return false;
                if (!CommonUtils.IsGuidValid(Cr3Ct3Key)) return false;
                if (!CommonUtils.IsGuidValid(Cr3Key)) Cr3Key = Guid.NewGuid();

                CredentialRequirementType = await context.CredentialRequirementTypes
                    .Where(rt => rt.Ct3Key == Cr3Ct3Key)
                    .FirstOrDefaultAsync();
                SetInitialStatus();

                context.Add(this);
                await context.SaveChangesAsync();

                if (CommonUtils.IsGuidValid(CredentialRequirementType.Ct3StartCr2Ct6Key))
                {
                    await SetProcessStatus(context, CredentialRequirementType.Ct3StartCr2Ct6Key);
                }

                // TODO: Find REquirement Data

                await Init(context);

                if (await SetStartDate(context) && await SetDueDate(context)) context.Update(this);


                // TODO: Send Correspondence functionality for status

                await context.SaveChangesAsync();

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

        private async Task<bool> SetDueDate(ApplicationDbContext context)
        {
            try
            {
                if (CommonUtils.IsDateTimeOffsetValid(Cr3DueDate)) return true;
                if (CredentialRequirementType.Ct3SyncToProcessDatesFlag)
                {
                    if (await GetCredentialProcess(context))
                    {
                        if (CommonUtils.IsDateTimeOffsetValid(CredentialProcess.Cr2DueDate))
                            Cr3DueDate = CredentialProcess.Cr2DueDate;

                    }
                }
                if (!CommonUtils.IsDateTimeOffsetValid(Cr3DueDate)
                    && !string.IsNullOrWhiteSpace(CredentialRequirementType.Ct3CalculateDueDateSp))
                {
                    Cr3DueDate = await CredentialingUtils.CalculateDateFromSp(context,
                        CredentialRequirementType.Ct3CalculateDueDateSp, Cr3Key.ToString());
                }

                if (!CommonUtils.IsDateTimeOffsetValid(Cr3DueDate))
                    Cr3DueDate = CredentialingUtils.CalculateDueDate(
                        CommonUtils.IsDateTimeOffsetValid(Cr3StartDate) ? Cr3StartDate : DateTimeOffset.Now
                        , CredentialRequirementType.Ct3DueDateNumber
                        , CredentialRequirementType.Ct3DueDateUnit
                        , CredentialRequirementType.Ct3DueDateRounding);

                if (!CommonUtils.IsDateTimeOffsetValid(Cr3DueDate)) Cr3DueDate = DateTimeOffset.Now;

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

        private async Task<bool> SetStartDate(ApplicationDbContext context)
        {
            try
            {
                if (CommonUtils.IsDateTimeOffsetValid(Cr3StartDate)) return true;
                if (!string.IsNullOrWhiteSpace(CredentialRequirementType.Ct3CalculateStartDateSp))
                {
                    Cr3StartDate = await CredentialingUtils.CalculateDateFromSp(context,
                        CredentialRequirementType.Ct3CalculateStartDateSp, Cr3Key.ToString());
                }

                else
                {
                    await GetCredentialProcess(context);

                    if (CredentialProcess != null) Cr3StartDate = CredentialProcess.Cr2StartDate;
                }

                if (!CommonUtils.IsDateTimeOffsetValid(Cr3StartDate)) Cr3StartDate = DateTimeOffset.Now;
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

        private async Task<bool> GetCredentialProcess(ApplicationDbContext context)
        {
            try
            {
                if (CredentialProcess == null)
                    CredentialProcess = await CredentialingUtils.GetCredentialProcess(context, Cr3Cr2Key);

                return CredentialProcess != null;
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

        public virtual async Task<bool> Init(ApplicationDbContext context)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return true;
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return await Task.Run(() =>
                {
                    return false;
                });
            }
        }

        private async Task<bool> SetProcessStatus(ApplicationDbContext context, Guid? ct6Key,
            bool updateCredential = false)
        {
            try
            {
                if (!CommonUtils.IsGuidValid(ct6Key)) return true;
                if (CredentialProcess == null)
                    CredentialProcess = await context.CredentialProcesses
                        .Where(p => p.Cr2Key == Cr3Cr2Key)
                        .FirstOrDefaultAsync();

                await CredentialProcess.SetStatusAsync(context, ct6Key, updateCredential);
                context.Update(CredentialProcess);
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

        private bool SetInitialStatus()
        {
            try
            {
                if (CommonUtils.IsGuidValid(Cr3Ct6Key)) return true;
                if (CredentialRequirementType.Ct3HasRequiredParentsFlag
                    && CommonUtils.IsGuidValid(CredentialRequirementType.Ct3DependentCt6Key))
                {
                    Cr3Ct6Key = CredentialRequirementType.Ct3DependentCt6Key;
                }

                else if (CommonUtils.IsGuidValid(CredentialRequirementType.Ct3StartCt6Key))
                {
                    Cr3Ct6Key = CredentialRequirementType.Ct3StartCt6Key;
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

        public void Update(ApplicationDbContext context)
        {
            throw new NotImplementedException();
        }
    }

}

