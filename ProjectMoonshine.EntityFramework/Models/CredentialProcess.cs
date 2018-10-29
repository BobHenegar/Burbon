using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Utils;
using SQLitePCL;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialProcess")]
    public class CredentialProcess : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Cr2";
        [Key]
        public Guid Cr2Key { get; set; }

        [DisplayName("Process Type")]
        public Guid Cr2Ct2Key { get; set; }

        [DisplayName("Credential")]
        public Guid Cr2Cr1Key { get; set; }

        //public Guid Cr2PerKey { get; set; }

        //public Guid Cr2Cr2Key { get; set; }

        [DisplayName("Status")]
        public Guid? Cr2Ct6Key { get; set; }


        //public DateTimeOffset Cr2StatusChangeDate { get; set; }

        //public string Cr2StatusChangeUser { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Cr2StartDate { get; set; }

        [DisplayName("Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Cr2DueDate { get; set; }

        [DisplayName("Submit Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Cr2SubmitDate { get; set; }

        [DisplayName("Completion Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Cr2CompletionDate { get; set; }

        [DisplayName("Termination Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Cr2TerminationDate { get; set; }

        //public DateTimeOffset Cr2ExtensionDate { get; set; }

        //public DateTimeOffset Cr2ExtensionGrantedDate { get; set; }

        //public string Cr2ExtensionGrantedUser { get; set; }

        //public string Cr2ExtensionGrantedReason { get; set; }

        //public string Cr2Note { get; set; }

        //public string Cr2Owner { get; set; }

        //public DateTimeOffset Cr2Due2Date { get; set; }

        //public DateTimeOffset Cr2Extension2Date { get; set; }

        //public DateTimeOffset Cr2Extension2GrantedDate { get; set; }

        //public DateTimeOffset Cr2Extension2GrantedUser { get; set; }

        //public string Cr2Extension2GrantedReason { get; set; }

        //public DateTimeOffset Cr2InactiveStartDate { get; set; }

        //public DateTimeOffset Cr2InactiveEndDate { get; set; }

        //public DateTimeOffset Cr2FulfillmentDate { get; set; }

        //public string Cr2EthicsDisclosure { get; set; }

        //public string Cr2EthicsSummary { get; set; }

        //public DateTimeOffset Cr2EthicsClearedDate { get; set; }

        //public string Cr2Agreement { get; set; }

        //public DateTimeOffset Cr2AgreementDate { get; set; }

        [DisplayName("Selected for Audit Date")]
        public DateTimeOffset? Cr2AuditSelectDate { get; set; }

        //public DateTimeOffset Cr2AuditCompleteDate { get; set; }

        //public DateTimeOffset Cr2AuditCompleteUser { get; set; }

        //public Guid Cr2DataKey { get; set; }

        //public string Cr2ProcessNumber { get; set; }


        public string Cr2AddUser { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset Cr2AddDate { get; set; }

        public string Cr2ChangeUser { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset? Cr2ChangeDate { get; set; }

        //public virtual List<CredentialRequirement>  CredentialRequirements { get; set; }
        //[ForeignKey("Cr2Cr1Key")]
        public Credential Credential { get; set; }
        //public virtual Person Person { get; set; }

        public virtual List<CredentialRequirement> CredentialRequirements { get; set; }

        [ForeignKey("Cr2Ct6Key")]
        public CredentialStatusType StatusType { get; set; }

        [ForeignKey("Cr2Ct2Key")]
        public CredentialProcessType CredentialProcessType { get; set; }


        
        public virtual async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {
                if (!await LoadCredentialAsync(context)) return false;

                bool isCurrentProcess = Credential?.Cr1CurrentCr2Key == Cr2Key;

                CredentialProcess originalCredentialProcess = await context.CredentialProcesses
                    .Include(p => p.Credential)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Cr2Key == Cr2Key);

                if (! await CheckForStatusChangeAsync(context, isCurrentProcess, originalCredentialProcess)) return false;
                if (! await EvaluateProcessAsync(context, isCurrentProcess, originalCredentialProcess, originalCredentialProcess.Credential)) return false;

                // TODO / Roadmap: send communication based on status

                SendCommunicationForStatus(originalCredentialProcess);

                // check if this is still the current process for the credential
                isCurrentProcess = Credential?.Cr1CurrentCr2Key == Cr2Key;
                if (!await EvaluateProcessTerminatedAsync(context, isCurrentProcess, originalCredentialProcess)) return false;

                context.Update(this);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if(Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvaluateProcessTerminatedAsync(ApplicationDbContext context, bool isCurrentProcess,
            CredentialProcess originalCredentialProcess)
        {
            try
            {
                if ((originalCredentialProcess == null ||
                     originalCredentialProcess.Cr2TerminationDate != Cr2TerminationDate)
                    && isCurrentProcess
                    && Credential != null)
                {
                    return await Credential.UpdateAsync(context);
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

        private void SendCommunicationForStatus(CredentialProcess originalCredentialProcess)
        {
            if (originalCredentialProcess == null || originalCredentialProcess.Cr2Ct6Key != Cr2Ct6Key)
            {
            }
        }

        private async Task<bool> EvaluateProcessAsync(ApplicationDbContext context, bool isCurrentProcess,
            CredentialProcess originalCredentialProcess, Credential originalCredential)
        {
            try
            {
                if (!originalCredentialProcess.Cr2CompletionDate.IsDateTimeOffsetValid()
                    && Cr2CompletionDate.IsDateTimeOffsetValid())
                {
                    if (!await EvaluateProcessCompletedAsync(context, isCurrentProcess, originalCredentialProcess,
                        originalCredential)) return false;
                }


                else if (!Cr2CompletionDate.IsDateTimeOffsetValid() && !Cr2TerminationDate.IsDateTimeOffsetValid())
                {
                    if (!await EvaluateRequirementsAsync(context, isCurrentProcess)) return false;

                    if (!await EvaluateProcessCompletedAsync(context, isCurrentProcess, originalCredentialProcess,
                        originalCredential)) return false;

                    if (!await EvaluateProcessUnderReviewAsync(context, isCurrentProcess)) return false;

                    //TODO / Roadmap: set requirement exensions based on process extension date
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

        private async Task<bool> EvaluateProcessUnderReviewAsync(ApplicationDbContext context, bool isCurrentProcess)
        {
            try
            {
                if (!Cr2CompletionDate.IsDateTimeOffsetValid()
                    && !Cr2SubmitDate.IsDateTimeOffsetValid()
                    && isCurrentProcess
                    && (await IsReviewReadyAsync(context)))
                {
                    return (await SubmitAsync(context));
                    //if (CredentialProcessType.Ct2SubmittedCt6Key != null &&
                    //    CredentialProcessType.Ct2SubmittedCt6Key != Guid.Empty)
                    //{
                    //    await SetStatusAsync(context, CredentialProcessType.Ct2SubmittedCt6Key, isCurrentProcess);
                    //}
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

        private async Task<bool> EvaluateProcessCompletedAsync(ApplicationDbContext context, bool isCurrentProcess,
            CredentialProcess originalCredentialProcess, Credential originalCredential)
        {
            try
            {
                if (!Cr2CompletionDate.IsDateTimeOffsetValid() || Cr2TerminationDate.IsDateTimeOffsetValid())
                    return true;

                if (!await LoadCredentialProcessTypeAsync(context)) return false;

                if (CredentialProcessType.Ct2CompletedCt6Key.IsGuidValid()
                    && (originalCredentialProcess == null || originalCredentialProcess.Cr2Ct6Key == Cr2Ct6Key))
                {
                    if (!await SetStatusAsync(context, CredentialProcessType.Ct2CompletedCt6Key, isCurrentProcess))
                        return false;
                    context.Update(this);
                }


                if (CredentialProcessType.Ct2CompletedCr1Ct6Key.IsGuidValid()
                    && (originalCredential == null || originalCredential.Cr1Ct6Key == Credential.Cr1Ct6Key)
                    && isCurrentProcess)
                {
                    if (!Credential.SetStatus(context, CredentialProcessType.Ct2CompletedCr1Ct6Key)) return false;
                    //context.Update(this);
                }

                return (await Credential.UpdateAsync(context));

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

        private async Task<bool> CheckForStatusChangeAsync(ApplicationDbContext context, bool isCurrentProcess,
            CredentialProcess originalCredentialProcess)
        {
            try
            {
                if (originalCredentialProcess != null && originalCredentialProcess.Cr2Ct6Key == Cr2Ct6Key) return true;

                if (!await SetStatusAsync(context, Cr2Ct6Key, isCurrentProcess)) return false;
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

        private async Task<bool> IsReviewReadyAsync(ApplicationDbContext context)
        {
            try
            {
                CredentialRequirements =
                    await CredentialingUtils.GetCredentialRequirementsForProcessAsync(context, Cr2Key, true);

                if (CredentialRequirements == null || CredentialRequirements.Count <= 0) return false;

                if (!await LoadCredentialProcessTypeAsync(context)) return false;

                CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes =
                    await CredentialingUtils.GetCredentialProcessTypeXCredentialRequirementTypesForProcessTypeAsync(
                        context, Cr2Ct2Key);

                if (CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes == null ||
                    CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes.Count <= 0) return false;
                {
                    foreach (CredentialRequirement credentialRequirement in CredentialRequirements)
                    {
                        if (!CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes
                                .Where(pxr => pxr.Ct5ReviewPrereqFlag)
                                .Select(pxr => pxr.Ct5Ct3Key)
                                .Contains(credentialRequirement.CredentialRequirementType.Ct3Key)
                            && (!credentialRequirement.Status.State.Cu6UnderReviewFlag
                                && !credentialRequirement.Status.State.Cu6CompletedFlag
                                && !credentialRequirement.Status.State.Cu6MetFlag)) continue;

                        return false;
                    }

                    return true;
                }
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

        public async Task<bool> SetStatusAsync(ApplicationDbContext context, Guid? ct6Key, bool updateCredential = false)
        {
            // TODO/Roadmap: status type change fields
            try
            {
                if (!ct6Key.IsGuidValid() || Cr2Ct6Key == ct6Key) return true;

                if (!await LoadStatusTypeAsync(context)) return false;

                if (updateCredential)
                {
                    if (!await LoadCredentialAsync(context)) return false;

                    {
                        if (StatusType.Ct6ParentCt6Key.IsGuidValid())
                        {
                            Credential.Cr1Ct6Key = StatusType.Ct6ParentCt6Key;
                        }

                        //TODO or Roadmap: Set process owner flag


                        if (StatusType.Ct6SetCredentialNumberFlag &&
                            !((int?) Credential.Cr1CredentialNumber).IsIntValidAndNotZero())
                        {
                            Credential.Cr1CredentialNumber =
                                await CredentialingUtils.GetNextCredentialNumber(context, Credential);
                        }
                        context.Update(Credential);

                    }
                }

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

        private async Task<bool> LoadCredentialAsync(ApplicationDbContext context, bool includeCredentialType = false)
        {
            try
            {
                if (Credential == null || Credential.Cr1Key != Cr2Cr1Key)
                    Credential = await CredentialingUtils.GetCredentialAsync(context, Cr2Cr1Key, includeCredentialType);

                return Credential != null;
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
                if (Cr2Ct6Key.IsGuidValid() && (StatusType == null 
                                                || StatusType.Ct6Key != Cr2Ct6Key 
                                                || (StatusType.State == null && includeState) ) )
                    
                {
                    StatusType = await CredentialingUtils.GetCredentialStatusTypeAsync(context, (Guid)Cr2Ct6Key, includeState);
                }

                return StatusType != null;
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

        public virtual async Task<bool> EvaluateRequirementsAsync(ApplicationDbContext context, bool updateCredential = false)
        {
            try
            {
                
                CredentialRequirements = await CredentialingUtils.GetCredentialRequirementsForProcessAsync(context, Cr2Key);
                   
                bool allRequirementsMet = false;
                if (CredentialRequirements != null && CredentialRequirements.Count > 0)
                {
                    allRequirementsMet = true;
                    foreach (CredentialRequirement credentialRequirement in CredentialRequirements)
                    {
                        if (!credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid())
                            await credentialRequirement.UpdateAsync(context);

                        if (credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid()) continue;
                        allRequirementsMet = false;
                        break;
                    }

                }
                if (allRequirementsMet) Cr2CompletionDate = DateTimeOffset.Now;

                else if (!Cr2TerminationDate.IsDateTimeOffsetValid()
                         && !Cr2CompletionDate.IsDateTimeOffsetValid())
                {
                    CredentialRequirements = await CredentialingUtils.GetCredentialRequirementsForProcessAsync(context, Cr2Key);
                    
                    if (CredentialRequirements != null &&  CredentialRequirements.Any(r => r.Status.State != null &&  r.Status.State.Cu6TerminateProcessFlag))
                        Cr2TerminationDate = DateTimeOffset.Now;
                }

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

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                if (Cr2Cr1Key == Guid.Empty) return false;
                if (Cr2Ct2Key == Guid.Empty) return false;
                if (Cr2Key == Guid.Empty) Cr2Key = Guid.NewGuid();

                if (!await LoadCredentialProcessTypeAsync(context)) return false;

                if (!Cr2Ct6Key.IsGuidValid()) Cr2Ct6Key = CredentialProcessType.Ct2StartCt6Key;
                // TODO: set credential status based on ct2cr1startct6key

                await context.AddAsync(this);
                await context.SaveChangesAsync();

                if (!await LoadCredentialAsync(context, true)) return false;

                await InitAsync(context);

                await SetProcessStartDateAsync(context);

                if (! await SetProcessDueDateAsync(context)) return false;

                if (!await AddRequirementsAsync(context)) return false;

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
            }
            return false;
        }

        private async Task<bool> AddRequirementsAsync(ApplicationDbContext context)
        {
            try
            {
                List<Guid> requirementTypesToAdd = await context.CredentialProcessTypeXCredentialRequirementTypes
                    .Where(cxp => cxp.Ct5Ct2Key == Cr2Ct2Key
                                  && (!cxp.Ct5StartDate.IsDateTimeOffsetValid()
                                      || cxp.Ct5StartDate <= DateTimeOffset.Now)
                                  && (!cxp.Ct5EndDate.IsDateTimeOffsetValid()
                                      || cxp.Ct5EndDate >= DateTimeOffset.Now))
                    .Select(cxp => cxp.Ct5Ct3Key)
                    .ToListAsync();

                if (requirementTypesToAdd == null || requirementTypesToAdd.Count <= 0) return true;
                foreach (Guid listReqType in requirementTypesToAdd)
                {
                    CredentialRequirement requirement = new CredentialRequirement()
                    {
                        Cr3Ct3Key = listReqType,
                        Cr3Cr2Key = Cr2Key
                    };
                    if (!await requirement.AddAsync(context)) return false;
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

        private async Task<bool> SetRequirementsUnderReviewAsync(ApplicationDbContext context)
        {
            try
            {
                CredentialRequirements =
                    await CredentialingUtils.GetCredentialRequirementsForProcessAsync(context, Cr2Key, true);

                if (CredentialRequirements == null || CredentialRequirements.Count <= 0) return true;

                if (!await LoadCredentialProcessTypeAsync(context)) return false;

                CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes =
                    await CredentialingUtils.GetCredentialProcessTypeXCredentialRequirementTypesForProcessTypeAsync(context,
                        Cr2Ct2Key);

                if (CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes == null ||
                    CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes.Count <= 0) return true;

                foreach (CredentialRequirement credentialRequirement in CredentialRequirements)
                {
                    if (!CredentialProcessType.CredentialProcessTypeXCredentialRequirementTypes
                        .Where(pxr => pxr.Ct5ReviewPrereqFlag)
                        .Select(pxr => pxr.Ct5Ct3Key)
                        .Contains(credentialRequirement.CredentialRequirementType.Ct3Key)) continue;

                    if (!await credentialRequirement.SetUnderReviewStatusAsync(context)) return false;

                    if (credentialRequirement.DependentRequirements == null ||
                        credentialRequirement.DependentRequirements.Count <= 0) continue;

                    foreach (CredentialRequirement credentialRequirementDependentRequirement in
                        credentialRequirement.DependentRequirements)
                    {
                        if (!await credentialRequirementDependentRequirement.SetUnderReviewStatusAsync(context))
                            return false;
                    }
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


        private async Task<bool> SetProcessDueDateAsync(DbContext context)
        {
            try
            {
                if (Cr2DueDate.IsDateTimeOffsetValid()) return true;

                if (!string.IsNullOrWhiteSpace(CredentialProcessType.Ct2CalculateDueDateSp))
                {
                    Cr2DueDate = await CredentialingUtils.CalculateDateFromSp(context,
                        CredentialProcessType.Ct2CalculateDueDateSp, Cr2Key.ToString());
                }

                if (Cr2DueDate.IsDateTimeOffsetValid())
                {
                    context.Update(this);
                    return true;
                }

                if (Cr2StartDate.IsDateTimeOffsetValid())
                {
                    Cr2DueDate = CredentialingUtils.CalculateDueDate(Cr2StartDate,
                        CredentialProcessType.Ct2DueDateNumber, CredentialProcessType.Ct2DueDateUnit,
                        CredentialProcessType.Ct2DueDateRounding);
                }

                if (Cr2DueDate.IsDateTimeOffsetValid())
                {
                    context.Update(this);
                    return true;
                }

                //TODO: Sync credential logic
                //if (Credential.CredentialType.Ct1SyncFlag) SyncCredential(context);

                Cr2DueDate = CredentialingUtils.CalculateDueDate(DateTimeOffset.Now,
                    CredentialProcessType.Ct2DueDateNumber,
                    CredentialProcessType.Ct2DueDateUnit, CredentialProcessType.Ct2DueDateRounding);


                if (Cr2DueDate.IsDateTimeOffsetValid())
                {
                    context.Update(this);
                    return true;
                }

                Cr2DueDate = DateTimeOffset.Now;
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

        private async Task SyncCredentialAsync(ApplicationDbContext context)
        {
            await Task.Run(() => { return; });
            //throw new NotImplementedException();
        }

        private async Task SetProcessStartDateAsync(ApplicationDbContext context)
        {
            // Check if start date is already set
            if (Cr2StartDate != null && Cr2StartDate != DateTimeOffset.MinValue) return;

            // Try to set based on calculation sproc
            if (!string.IsNullOrWhiteSpace(CredentialProcessType.Ct2CalculateStartDateSp))
            {
                Cr2StartDate = await CredentialingUtils.CalculateDateFromSp(context, CredentialProcessType.Ct2CalculateStartDateSp,
                    Cr2Key.ToString());
            }

            if (Cr2StartDate != null && Cr2StartDate != DateTimeOffset.MinValue) return;

            // Try to set based on prior due date
            DateTimeOffset? priorDueDate = await context.CredentialProcesses
                .Include(p => p.Credential)
                .Where(p => p.Cr2Key != Cr2Key
                            && p.Cr2Ct2Key == CredentialProcessType.Ct2Key
                            && p.Credential.Cr1CusKey == Credential.Cr1CusKey
                )
                .OrderByDescending(p => p.Cr2DueDate)
                .Select(p => p.Cr2DueDate)
                .FirstOrDefaultAsync();

            if (priorDueDate != null && priorDueDate != DateTimeOffset.MinValue)
                Cr2StartDate = ((DateTimeOffset) priorDueDate).AddDays(1);

            // Try to set based on cr1 valid thru date
            if (Credential.Cr1ValidThruDate != null && Credential.Cr1ValidThruDate != DateTimeOffset.MinValue)
                Cr2StartDate = ((DateTimeOffset) Credential.Cr1ValidThruDate).AddDays(1);

            // Set to current date by default
            if (Cr2StartDate == null || Cr2StartDate == DateTimeOffset.MinValue) Cr2StartDate = DateTimeOffset.Now;

            //context.Update(this);
        }


        public virtual async Task InitAsync(ApplicationDbContext context)
        {
            await Task.Run(() => { return; });
        }


        public async Task<bool> SubmitAsync(ApplicationDbContext context)
        {
            try
            {
                if (!Cr2SubmitDate.IsDateTimeOffsetValid())
                    Cr2SubmitDate = DateTimeOffset.Now;


                if (await LoadCredentialProcessTypeAsync(context) && CredentialProcessType.Ct2SubmittedCt6Key.IsGuidValid())
                    await SetStatusAsync(context, CredentialProcessType.Ct2SubmittedCt6Key, true);

                if (!await SetRequirementsUnderReviewAsync(context)) return false;
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

        private async Task<bool> LoadCredentialProcessTypeAsync(ApplicationDbContext context)
        {
            try
            {
                if (CredentialProcessType == null || CredentialProcessType.Ct2Key != Cr2Ct2Key)
                    CredentialProcessType = await CredentialingUtils.GetCredentialProcessTypeAsync(context, Cr2Ct2Key);

                return CredentialProcessType != null;
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
