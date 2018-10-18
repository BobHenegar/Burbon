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

                if (await SetStartDateAsync(context) && await SetDueDate(context)) context.Update(this);


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
                if (Cr3DueDate.IsDateTimeOffsetValid()) return true;
                if (CredentialRequirementType.Ct3SyncToProcessDatesFlag)
                {
                    if (await GetCredentialProcessAsync(context))
                    {
                        if (CredentialProcess.Cr2DueDate.IsDateTimeOffsetValid())
                            Cr3DueDate = CredentialProcess.Cr2DueDate;

                    }
                }
                if (!Cr3DueDate.IsDateTimeOffsetValid()
                    && !string.IsNullOrWhiteSpace(CredentialRequirementType.Ct3CalculateDueDateSp))
                {
                    Cr3DueDate = await CredentialingUtils.CalculateDateFromSp(context,
                        CredentialRequirementType.Ct3CalculateDueDateSp, Cr3Key.ToString());
                }

                if (!Cr3DueDate.IsDateTimeOffsetValid())
                    Cr3DueDate = CredentialingUtils.CalculateDueDate(
                        Cr3StartDate.IsDateTimeOffsetValid() ? Cr3StartDate : DateTimeOffset.Now
                        , CredentialRequirementType.Ct3DueDateNumber
                        , CredentialRequirementType.Ct3DueDateUnit
                        , CredentialRequirementType.Ct3DueDateRounding);

                if (!Cr3DueDate.IsDateTimeOffsetValid()) Cr3DueDate = DateTimeOffset.Now;

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

        private async Task<bool> SetStartDateAsync(ApplicationDbContext context)
        {
            try
            {
                if (Cr3StartDate.IsDateTimeOffsetValid()) return true;
                if (!string.IsNullOrWhiteSpace(CredentialRequirementType.Ct3CalculateStartDateSp))
                {
                    Cr3StartDate = await CredentialingUtils.CalculateDateFromSp(context,
                        CredentialRequirementType.Ct3CalculateStartDateSp, Cr3Key.ToString());
                }

                else
                {
                    await GetCredentialProcessAsync(context);

                    if (CredentialProcess != null) Cr3StartDate = CredentialProcess.Cr2StartDate;
                }

                if (!Cr3StartDate.IsDateTimeOffsetValid()) Cr3StartDate = DateTimeOffset.Now;
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

        private async Task<bool> GetCredentialProcessAsync(ApplicationDbContext context)
        {
            try
            {
                if (CredentialProcess == null)
                    CredentialProcess = await CredentialingUtils.GetCredentialProcessAsync(context, Cr3Cr2Key);

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
                if (!ct6Key.IsGuidValid()) return true;
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
                if (Cr3Ct6Key.IsGuidValid()) return true;
                if (CredentialRequirementType.Ct3HasRequiredParentsFlag
                    && CredentialRequirementType.Ct3DependentCt6Key.IsGuidValid())
                {
                    Cr3Ct6Key = CredentialRequirementType.Ct3DependentCt6Key;
                }

                else if (CredentialRequirementType.Ct3StartCt6Key.IsGuidValid())
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

        public virtual async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {
                if (!await GetRequirementTypeAsync(context)) return false ;


                await GetStatusTypeAsync(context, true);

                if (!await EvaluateUnlocked(context)) return true;

                context.Update(this);

                CredentialRequirement originalCredentialRequirement = await context.CredentialRequirements
                    .Where(r => r.Cr3Key == Cr3Key)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (originalCredentialRequirement.Cr3Ct6Key != Cr3Ct6Key 
                    && Cr3Ct6Key.IsGuidValid())
                    await SetStatusAsync(context, (Guid) Cr3Ct6Key);

                // TODO: find requirement data stuff;




                context.Update(this);
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

        private async Task<bool> SetStatusAsync(ApplicationDbContext context, Guid ct6Key)
        {
            try
            {
                if (Cr3Ct6Key != ct6Key) 
                {
                    Cr3Ct6Key = ct6Key;
                    context.Update(this);
                }
                if (await GetStatusTypeAsync(context) 
                    && Status.Ct6ParentCt6Key.IsGuidValid())
                {
                    await SetProcessStatus(context, Status.Ct6ParentCt6Key, true);
                    context.Update(this);
                };
        
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

        private async Task<bool> EvaluateUnlocked(ApplicationDbContext context)
        {
            try
            {
                if (!CredentialRequirementType.Ct3HasRequiredParentsFlag 
                    || Status?.State == null 
                    || !Status.State.Cu6LockedFlag) return true;

                // TODO: buildout AllRequiredParentsDone for checklists / switch
                // prereqs have not been met
                if (!await AllRequiredParentsDone(context)) return false;

                Cr3Ct6Key = CredentialRequirementType.Ct3StartCt6Key;
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

        // TODO: build out later depending on if we get to checklist / switch requirements.  For now just return true
        private async Task<bool> AllRequiredParentsDone(ApplicationDbContext context)
        {
            try
            {
                // not all reqs know their process, at least in old credeng, so return true if process not found
                if (! await GetCredentialProcessAsync(context) ) return true;

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

        private async Task<bool> GetRequirementTypeAsync(ApplicationDbContext context)
        {
            try
            {
                if (CredentialRequirementType == null)
                {
                    CredentialRequirementType =
                        await CredentialingUtils.GetCredentialRequirementTypeAsync(context, Cr3Ct3Key);
                }

                return CredentialRequirementType != null;
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

        private async Task<bool> GetStatusTypeAsync(ApplicationDbContext context, bool includeState = false)
        {
            try
            {
                if (Status == null && Cr3Ct6Key.IsGuidValid())
                {
                    Status = await CredentialingUtils.GetCredentialStatusTypeAsync(context, (Guid) Cr3Ct6Key, includeState);
                }

                return Status != null;
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

