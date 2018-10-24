using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Utils;
using System.Linq.Expressions;

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

        [DisplayName("Parent Requirement")]
        public Guid? Cr3Cr3Key { get; set; }

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

        [DisplayName("Attestment Flag")]
        public bool Cr3AttestmentFlag { get; set; }

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
        public virtual CredentialStatusType Status { get; set; }


        [ForeignKey("Cr3Cr3Key")]
        public virtual CredentialRequirement ParentRequirement { get; set; }

        
        public virtual List<CredentialRequirement> DependentRequirements { get; set; }
        

        public virtual async Task<bool> AddAsync(ApplicationDbContext context)
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

                if (CredentialRequirementType.Ct3StartCr2Ct6Key.IsGuidValid())
                {
                    await SetProcessStatusAsync(context, CredentialRequirementType.Ct3StartCr2Ct6Key);
                }

                // TODO: Find REquirement Data

                

                if (await SetStartDateAsync(context) && await SetDueDate(context)) context.Update(this);

                if (! await InitAsync(context)) return false;

                if (! await EvaluateRequirementAsync(context)) return false;

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
                    if (await LoadCredentialProcessAsync(context))
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
                    await LoadCredentialProcessAsync(context);

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

        /// <summary>
        /// Get Credential Process for this requirement.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns true if successful, false if no process is found or on error.</returns>
        private async Task<bool> LoadCredentialProcessAsync(ApplicationDbContext context)
        {
            try
            {
                if (CredentialProcess == null || CredentialProcess.Cr2Key != Cr3Cr2Key)
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

        public virtual async Task<bool> InitAsync(ApplicationDbContext context)
        {
            try
            {
                if (!await LoadRequirementTypeAsync(context, true)) return false;

                if (CredentialRequirementType.Template != null 
                    && !await CredentialRequirementType.Template.InitAsync(context, this)) return false;

                context.Update(this);
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

        private async Task<bool> SetProcessStatusAsync(ApplicationDbContext context, Guid? ct6Key,
            bool updateCredential = false)
        {
            try
            {
                if (!ct6Key.IsGuidValid()) return true;

                if (!await LoadCredentialProcessAsync(context)) return false;
                if (!await CredentialProcess.SetStatusAsync(context, ct6Key, updateCredential)) return false;
                //context.Update(CredentialProcess);
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
                if (!await LoadRequirementTypeAsync(context)) return false;


                await LoadStatusTypeAsync(context, true);

                if (!await EvaluateUnlockedAsync(context)) return true;

                context.Update(this);

                CredentialRequirement originalCredentialRequirement = await context.CredentialRequirements
                    .Where(r => r.Cr3Key == Cr3Key)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                await EvaluateRequirementAsync(context, originalCredentialRequirement);

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

        /// <summary>
        /// Evaluates Requirement
        /// </summary>
        /// <param name="context"></param>
        /// <param name="originalCredentialRequirement"></param>
        /// <returns>Returns true if evaluation succeeds, false on error.</returns>
        private async Task<bool> EvaluateRequirementAsync(ApplicationDbContext context, CredentialRequirement originalCredentialRequirement = null)
        {
            try
            {
                if (
                    originalCredentialRequirement != null &&
                    originalCredentialRequirement.Cr3Ct6Key != Cr3Ct6Key
                    && Cr3Ct6Key.IsGuidValid())
                    await SetStatusAsync(context, (Guid) Cr3Ct6Key);

                // TODO: find requirement data stuff;

                // refresh status

                //if (await EvaluateMetFromMetStatusAsync(context)) return true;
             
                if (originalCredentialRequirement == null || !originalCredentialRequirement.Cr3MetDate.IsDateTimeOffsetValid())
                {
                    if (!Cr3MetDate.IsDateTimeOffsetValid())
                    {
                        if (!await EvalAsync(context)) return false;
                    }
                    
                    if (Cr3MetDate.IsDateTimeOffsetValid())
                    {
                        if (originalCredentialRequirement == null || originalCredentialRequirement.Cr3Ct6Key == Cr3Ct6Key)
                        {
                            if (!await LoadRequirementTypeAsync(context)) return false;
                            if (Cr3Ct6Key != CredentialRequirementType.Ct3MetCt6Key &&
                                CredentialRequirementType.Ct3MetCt6Key.IsGuidValid())
                                if (!await SetStatusAsync(context, (Guid) CredentialRequirementType.Ct3MetCt6Key))
                                    return false;

                            if (CredentialRequirementType.Ct3MetCr2Ct6Key.IsGuidValid())
                                await SetProcessStatusAsync(context, CredentialRequirementType.Ct3MetCr2Ct6Key, true);
                        }

                        if (CredentialRequirementType.Ct3SubmitProcessFlag)
                            {
                                if (await LoadCredentialProcessAsync(context))
                                {
                                    if (!await CredentialProcess.SubmitAsync(context)) return false;

                                }
                            }

                            if (Cr3Cr3Key.IsGuidValid())
                            {
                                if (ParentRequirement == null)
                                    ParentRequirement = await context.CredentialRequirements.FindAsync(Cr3Cr3Key);

                                if (ParentRequirement != null && !ParentRequirement.Cr3MetDate.IsDateTimeOffsetValid())
                                    await ParentRequirement.UpdateAsync(context);
                            }

                            if (!await LoadCredentialProcessAsync(context)) return false;
                            if (!await CredentialProcess.UpdateAsync(context)) return false;

                        }

                    }
                else
                {
                    context.Update(this);

                    // TODO: update requirement data
                }


                if (Cr3Ct6Key.IsGuidValid() && (originalCredentialRequirement == null || originalCredentialRequirement.Cr3Ct6Key != Cr3Ct6Key))
                {
                    // TODO: corespondence logic
                }
                
                // update dependent requirements

                

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

        /// <summary>
        /// Overrideable Eval method for requirements.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns true if Eval succeeds, false on error.</returns>
        public virtual async Task<bool> EvalAsync(ApplicationDbContext context)
        {
            try
            {
                if (!await EvalMetAsync(context)) return false;

                if (Cr3MetDate.IsDateTimeOffsetValid()) return true;

                if (!await LoadRequirementTypeAsync(context)) return false;

                if (!CredentialRequirementType.Ct3CompleteCt6Key.IsGuidValid()) return true;

                return (await EvalCompletedAsync(context));
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

        private async Task<bool> EvalMetAsync(ApplicationDbContext context)
        {
            try
            {
                if (await EvaluateMetFromMetStatusAsync(context)) return true;
                if (!await LoadRequirementTypeAsync(context, true)) return false;

                if (CredentialRequirementType.Template != null
                    && await CredentialRequirementType.Template.EvalMetAsync(context, this))
                {
                    if (CredentialRequirementType.Ct3MetCt6Key.IsGuidValid()
                        && !await SetStatusAsync(context, (Guid) CredentialRequirementType.Ct3MetCt6Key)) return false;

                    if (!CredentialRequirementType.Ct3MetCt6Key.IsGuidValid()) Cr3MetDate = DateTimeOffset.Now;
                }


                if (!Cr3MetDate.IsDateTimeOffsetValid() && await EvaluateMetFromMetStatusAsync(context)) return true;

                if (!Cr3MetDate.IsDateTimeOffsetValid()) return true;

                context.Update(this);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<bool> EvalCompletedAsync(ApplicationDbContext context)
        {
            try
            {
                if (!await LoadRequirementTypeAsync(context, true)) return false;

                await LoadStatusTypeAsync(context, true);
                if (Status?.State != null
                    && (Status.State.Cu6CompletedFlag
                        || Status.State.Cu6UnderReviewFlag
                        || Status.State.Cu6MetFlag)) return true;

                if (CredentialRequirementType.Template != null &&
                    await CredentialRequirementType.Template.EvaluateCompleteAsync(context, this))
                {
                    if (CredentialRequirementType.Ct3CompleteCt6Key.IsGuidValid()
                        && !await SetStatusAsync(context, (Guid) CredentialRequirementType.Ct3CompleteCt6Key))
                        return false;
                }

                context.Update(this);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //private async Task<bool> EvaluateMetRule(ApplicationDbContext context)
        //{
        //    try
        //    {
        //        // TODO: check for met rule on ct3 level & evaluate

        //        Expression expression = Expression.Parameter()
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //        return false;
        //    }
        //}

        /// <summary>
        /// Checks if requirement is met based on status, and if status is met sets met date.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns true if met, false if not met or on error.</returns>
        private async Task<bool> EvaluateMetFromMetStatusAsync(ApplicationDbContext context)
        {
            try
            {
                await LoadStatusTypeAsync(context, true);

                if (Cr3MetDate.IsDateTimeOffsetValid() || Status.State == null || !Status.State.Cu6MetFlag)
                    return false;

                await SetMetDateFromMetStatusAsync(context);

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

        private async Task<bool> SetMetDateFromMetStatusAsync(ApplicationDbContext context)
        {
            try
            {
                Cr3MetDate = DateTimeOffset.Now;
                context.Update(this);

                if (await LoadCredentialProcessAsync(context))
                    await CredentialProcess.UpdateAsync(context);

                // TODO: updating parent requirements for checklists / switches

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

        /// <summary>
        /// Set Requirement status and parent process status if applicable.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusKey"></param>
        /// <returns>Returns true if successful, false on error.</returns>
        private async Task<bool> SetStatusAsync(ApplicationDbContext context, Guid statusKey)
        {
            try
            {
                if (Cr3Ct6Key != statusKey) 
                {
                    Cr3Ct6Key = statusKey;
                    context.Update(this);
                }
                if (await LoadStatusTypeAsync(context) 
                    && Status.Ct6ParentCt6Key.IsGuidValid())
                {
                    if (!await SetProcessStatusAsync(context, Status.Ct6ParentCt6Key, true)) return false;
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

        private async Task<bool> EvaluateUnlockedAsync(ApplicationDbContext context)
        {
            try
            {
                if (!CredentialRequirementType.Ct3HasRequiredParentsFlag 
                    || Status?.State == null 
                    || !Status.State.Cu6LockedFlag) return true;

                // TODO: buildout AllRequiredParentsDone for checklists / switch
                // prereqs have not been met
                if (!await AllRequiredParentsDoneAsync(context)) return false;

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
        private async Task<bool> AllRequiredParentsDoneAsync(ApplicationDbContext context)
        {
            try
            {
                // not all reqs know their process, at least in old credeng, so return true if process not found
                if (! await LoadCredentialProcessAsync(context) ) return true;

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

        /// <summary>
        /// Load Requirement Type if null.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Returns true if load succeeds, false if load fails or on error.</returns>
        private async Task<bool> LoadRequirementTypeAsync(ApplicationDbContext context, bool includeTemplate = false)
        {
            try
            {
                if (CredentialRequirementType == null 
                    || CredentialRequirementType.Ct3Key != Cr3Ct3Key
                    || (CredentialRequirementType.Template == null && includeTemplate))
                {
                    CredentialRequirementType =
                        await CredentialingUtils.GetCredentialRequirementTypeAsync(context, Cr3Ct3Key, includeTemplate);
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

        /// <summary>
        /// Load Status Type if null or if Status.State is null and needed.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="includeState"></param>
        /// <returns>Returns true if load succeeds, false if load fails or on error.</returns>
        private async Task<bool> LoadStatusTypeAsync(ApplicationDbContext context, bool includeState = false)
        {
            try
            {
                if (Cr3Ct6Key.IsGuidValid() 
                    && (Status == null 
                        || Status.Ct6Key != Cr3Ct6Key 
                        || ( Status.State == null && includeState)))
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

        /// <summary>
        /// Set Requirement status to Under Review.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>True if successful, false on failure.</returns>
        public async Task<bool> SetUnderReviewStatusAsync(ApplicationDbContext context)
        {
            try
            {
                if (Cr3MetDate.IsDateTimeOffsetValid()) return true;
                if (!await LoadRequirementTypeAsync(context)) return false;

                if (!CredentialRequirementType.Ct3SubmitCt6Key.IsGuidValid() ||
                    Cr3Ct6Key == CredentialRequirementType.Ct3SubmitCt6Key) return true;

                Cr3Ct6Key = CredentialRequirementType.Ct3SubmitCt6Key;
                context.Update(this);
                
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
    }

}

