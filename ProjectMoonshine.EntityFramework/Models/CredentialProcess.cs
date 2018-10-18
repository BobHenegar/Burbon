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

        public Guid Cr2Ct2Key { get; set; }

        public Guid Cr2Cr1Key { get; set; }

        //public Guid Cr2PerKey { get; set; }

        //public Guid Cr2Cr2Key { get; set; }

        [DisplayName("Status")]
        public Guid? Cr2Ct6Key { get; set; }


        //public DateTimeOffset Cr2StatusChangeDate { get; set; }

        //public string Cr2StatusChangeUser { get; set; }

        [DisplayName("Start Date")]
        public DateTimeOffset? Cr2StartDate { get; set; }

        [DisplayName("Due Date")]
        public DateTimeOffset? Cr2DueDate { get; set; }

        [DisplayName("Submit Date")]
        public DateTimeOffset? Cr2SubmitDate { get; set; }

        [DisplayName("Completion Date")]
        public DateTimeOffset? Cr2CompletionDate { get; set; }

        [DisplayName("Termination Date")]
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

        //public DateTimeOffset Cr2AuditSelectDate { get; set; }

        //public DateTimeOffset Cr2AuditCompleteDate { get; set; }

        //public DateTimeOffset Cr2AuditCompleteUser { get; set; }

        //public Guid Cr2DataKey { get; set; }

        //public string Cr2ProcessNumber { get; set; }


        public string Cr2AddUser { get; set; }

        public DateTimeOffset Cr2AddDate { get; set; }

        public string Cr2ChangeUser { get; set; }

        public DateTimeOffset? Cr2ChangeDate { get; set; }

        //public virtual List<CredentialRequirement>  CredentialRequirements { get; set; }
        //[ForeignKey("Cr2Cr1Key")]
        public Credential Credential { get; set; }
        //public virtual Person Person { get; set; }

        public List<CredentialRequirement> CredentialRequirements { get; set; }

        [ForeignKey("Cr2Ct6Key")]
        public CredentialStatusType StatusType { get; set; }

        [ForeignKey("Cr2Ct2Key")]
        public CredentialProcessType CredentialProcessType { get; set; }


        public virtual async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {
                Credential = await context.Credentials
                    .Where(c => c.Cr1Key == Cr2Cr1Key)
                    .FirstOrDefaultAsync();

                bool isCurrentProcess = Credential?.Cr1CurrentCr2Key == Cr2Key;

                CredentialProcess originalCredentialProcess = await context.CredentialProcesses
                    .Include(p => p.Credential)
                    
                    .FirstOrDefaultAsync(p => p.Cr2Key == Cr2Key);

                //Credential originalCredential = context.Credentials
                //    
                //    .FirstOrDefault(c => c.Cr1Key == Cr2Cr1Key);

                await CheckForStatusChange(context, isCurrentProcess, originalCredentialProcess);
                await EvaluateProcess(context, isCurrentProcess, originalCredentialProcess, originalCredentialProcess.Credential);

                // TODO / Roadmap: send communication based on status

                SendCommunicationForStatus(originalCredentialProcess);

                // check if this is still the current process for the credential
                isCurrentProcess = Credential?.Cr1CurrentCr2Key == Cr2Key;
                await EvaluateProcessTerminated(context, isCurrentProcess, originalCredentialProcess);

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

        private async Task EvaluateProcessTerminated(ApplicationDbContext context, bool isCurrentProcess, CredentialProcess originalCredentialProcess)
        {
            if ((originalCredentialProcess == null ||
                                 originalCredentialProcess.Cr2TerminationDate != Cr2TerminationDate)
                                && isCurrentProcess
                                && Credential != null)
            {
                await Credential.Update(context);
            }
        }

        private void SendCommunicationForStatus(CredentialProcess originalCredentialProcess)
        {
            if (originalCredentialProcess == null || originalCredentialProcess.Cr2Ct6Key != Cr2Ct6Key)
            {
            }
        }

        private async Task EvaluateProcess(ApplicationDbContext context, bool isCurrentProcess, CredentialProcess originalCredentialProcess, Credential originalCredential)
        {
            if (!originalCredentialProcess.Cr2CompletionDate.IsDateTimeOffsetValid()
                 && Cr2CompletionDate.IsDateTimeOffsetValid())
                await EvaluateProcessCompleted(context, isCurrentProcess, originalCredentialProcess, originalCredential);

            else if (!Cr2CompletionDate.IsDateTimeOffsetValid())
            {
                await EvaluateRequirements(context, isCurrentProcess);

                await EvaluateProcessCompleted(context, isCurrentProcess, originalCredentialProcess, originalCredential);

                await EvaluateProcessUnderReview(context, isCurrentProcess);

                //TODO / Roadmap: set requirement exensions based on process extension date
            }
        }

        private async Task EvaluateProcessUnderReview(ApplicationDbContext context, bool isCurrentProcess)
        {
            bool isReviewReady = IsReviewReady(context);
            if ((Cr2CompletionDate == null || Cr2CompletionDate == DateTimeOffset.MinValue) && isReviewReady)
            {
                if (CredentialProcessType.Ct2SubmittedCt6Key != null &&
                    CredentialProcessType.Ct2SubmittedCt6Key != Guid.Empty)
                {
                    await SetStatusAsync(context, CredentialProcessType.Ct2SubmittedCt6Key, isCurrentProcess);
                }

                //TODO Get Requirements that are submit prereqs and set to under review
            }
        }

        private async Task EvaluateProcessCompleted(ApplicationDbContext context, bool isCurrentProcess, CredentialProcess originalCredentialProcess, Credential originalCredential)
        {
            if (Cr2CompletionDate != null && Cr2CompletionDate != DateTimeOffset.MinValue &&
                                (Cr2TerminationDate == null || Cr2TerminationDate == DateTimeOffset.MinValue))
            {
                CredentialProcessType = await context.CredentialProcessTypes
                    .Where(pt => pt.Ct2Key == Cr2Ct2Key)
                    .FirstOrDefaultAsync();
                if (CredentialProcessType.Ct2CompletedCt6Key != null &&
                    CredentialProcessType.Ct2CompletedCt6Key != Guid.Empty
                    && (originalCredentialProcess == null || originalCredentialProcess.Cr2Ct6Key == Cr2Ct6Key))
                {
                    await SetStatusAsync(context, CredentialProcessType.Ct2CompletedCt6Key, isCurrentProcess);
                    context.Update(this);
                }


                if (CredentialProcessType.Ct2CompletedCr1Ct6Key != null
                    && CredentialProcessType.Ct2CompletedCr1Ct6Key != Guid.Empty
                    && (originalCredential == null || originalCredential.Cr1Ct6Key == Credential.Cr1Ct6Key)
                    && isCurrentProcess
                )
                {
                    Credential.SetStatus(context, CredentialProcessType.Ct2CompletedCr1Ct6Key);
                    //context.Update(this);
                }

                await Credential.Update(context);
            }
        }

        private async Task CheckForStatusChange(ApplicationDbContext context, bool isCurrentProcess, CredentialProcess originalCredentialProcess)
        {
            if (originalCredentialProcess == null || originalCredentialProcess.Cr2Ct6Key != Cr2Ct6Key)
            {
                await SetStatusAsync(context, Cr2Ct6Key, isCurrentProcess);
                context.Update(this);
            }
        }

        private bool IsReviewReady(ApplicationDbContext context)
        {
            return false;
            throw new NotImplementedException();
        }

        public async Task SetStatusAsync(ApplicationDbContext context, Guid? ct6Key, bool updateCredential = false)
        {
            // TODO/Roadmap: status type change fields

            if (ct6Key.IsGuidValid() && Cr2Ct6Key != ct6Key)
            {
                Cr2Ct6Key = ct6Key;
                if (StatusType == null)
                    StatusType = await context.CredentialStatusTypes.Where(st => st.Ct6Key == ct6Key).FirstOrDefaultAsync();

                if (StatusType != null)
                {
                    if (updateCredential)
                    {
                        if (Credential == null) await context.Credentials.Where(c => c.Cr1Key == Cr2Cr1Key).FirstOrDefaultAsync();

                        if (Credential != null)
                        {
                            if (StatusType.Ct6ParentCt6Key != null && StatusType.Ct6ParentCt6Key != Guid.Empty)
                            {
                                Credential.Cr1Ct6Key = StatusType.Ct6ParentCt6Key;
                            }

                            //TODO or Roadmap: Set process owner flag


                            if (StatusType.Ct6SetCredentialNumberFlag && (Credential.Cr1CredentialNumber == null || Credential.Cr1CredentialNumber == 0))
                            {
                                Credential.Cr1CredentialNumber = await CredentialingUtils.GetNextCredentialNumber(context, Credential);
                            }
                        }
                    }
                }
            }
        }

        public virtual async Task<bool> EvaluateRequirements(ApplicationDbContext context, bool updateCredential = false)
        {
            try
            {
                if (CredentialRequirements == null)
                CredentialRequirements = await context.CredentialRequirements
                    .Include(r => r.Status)
                    .ThenInclude(s => s.State)
                    .Where(r => r.Cr3Cr2Key == Cr2Key)
                    .ToListAsync();

                bool allRequirementsMet = false;
                if (CredentialRequirements.Count > 0)
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

                else if (!CommonUtils.IsDateTimeOffsetValid(Cr2TerminationDate)
                         && !CommonUtils.IsDateTimeOffsetValid(Cr2CompletionDate))
                {
                    if (CredentialRequirements.Any(r => r.Status.State.Cu6TerminateProcessFlag))
                        Cr2TerminationDate = DateTimeOffset.Now;
                }

                context.Update(this);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
            //throw new NotImplementedException();
        }

        public async Task<bool> Add(ApplicationDbContext context)
        {
            try
            {
                if (Cr2Cr1Key == Guid.Empty) return false;
                if (Cr2Ct2Key == Guid.Empty) return false;
                if (Cr2Key == Guid.Empty) Cr2Key = Guid.NewGuid();
                CredentialProcessType = await context.CredentialProcessTypes
                    .FirstOrDefaultAsync(pt => pt.Ct2Key == Cr2Ct2Key);

                if (CredentialProcessType == null) return false;

                if (Cr2Ct6Key == null || Cr2Ct6Key == Guid.Empty) Cr2Ct6Key = CredentialProcessType.Ct2StartCt6Key;
                // TODO: set credential status based on ct2cr1startct6key

                await context.AddAsync(this);
                await context.SaveChangesAsync();

                Credential = await context.Credentials
                    .Where(c => Cr2Cr1Key == c.Cr1Key)
                    .Include(c => c.CredentialType)
                    .FirstOrDefaultAsync();

                await InitAsync(context);

                await SetProcessStartDate(context);

                await SetProcessDueDate(context);
                await AddRequirements(context);

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

        private async Task<bool> AddRequirements(ApplicationDbContext context)
        {
            try
            {
                List<Guid> requirementTypesToAdd = await context.CredentialProcessTypeXCredentialRequirementTypes
                    .Where(cxp => cxp.Ct5Ct2Key == Cr2Ct2Key
                                  && (!CommonUtils.IsDateTimeOffsetValid(cxp.Ct5StartDate)
                                      || cxp.Ct5StartDate <= DateTimeOffset.Now)
                                  && (!CommonUtils.IsDateTimeOffsetValid(cxp.Ct5EndDate)
                                      || cxp.Ct5EndDate >= DateTimeOffset.Now))
                    .Select(cxp => cxp.Ct5Ct3Key)
                    .ToListAsync();

                foreach (Guid listReqType in requirementTypesToAdd)
                {
                    CredentialRequirement requirement = new CredentialRequirement()
                    {
                        Cr3Ct3Key = listReqType,
                        Cr3Cr2Key = Cr2Key
                    };
                    if (!await requirement.Add(context)) return false;
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


        private async Task SetProcessDueDate(DbContext context)
        {
            if (Cr2DueDate != null && Cr2DueDate != DateTimeOffset.MinValue) return;

            if (!string.IsNullOrWhiteSpace(CredentialProcessType.Ct2CalculateDueDateSp))
            {
                Cr2DueDate = await CredentialingUtils.CalculateDateFromSp(context, CredentialProcessType.Ct2CalculateDueDateSp, Cr2Key.ToString());
            }

            if (Cr2DueDate != null && Cr2DueDate != DateTimeOffset.MinValue) return;

            if (Cr2StartDate != null && Cr2StartDate != DateTimeOffset.MinValue)
            {
                Cr2DueDate = CredentialingUtils.CalculateDueDate(Cr2StartDate, CredentialProcessType.Ct2DueDateNumber, CredentialProcessType.Ct2DueDateUnit, CredentialProcessType.Ct2DueDateRounding );
            }

            if (Cr2DueDate != null && Cr2DueDate != DateTimeOffset.MinValue) return;

            //TODO: Sync credential logic
            //if (Credential.CredentialType.Ct1SyncFlag) SyncCredential(context);

            Cr2DueDate = CredentialingUtils.CalculateDueDate(DateTimeOffset.Now, CredentialProcessType.Ct2DueDateNumber,
                CredentialProcessType.Ct2DueDateUnit, CredentialProcessType.Ct2DueDateRounding);

            if (Cr2DueDate == null || Cr2DueDate == DateTimeOffset.MinValue) Cr2DueDate = DateTimeOffset.Now;

            //context.Update(this);
        }

        private async Task SyncCredentialAsync(ApplicationDbContext context)
        {
            await Task.Run(() => { return; });
            //throw new NotImplementedException();
        }

        private async Task SetProcessStartDate(ApplicationDbContext context)
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
                if (!Cr2SubmitDate.IsDateTimeOffsetValid()) Cr2SubmitDate = DateTimeOffset.Now;


                if (await GetCredentialProcessTypeAsync(context) && CredentialProcessType.Ct2SubmittedCt6Key.IsGuidValid())
                    await SetStatusAsync(context, CredentialProcessType.Ct2SubmittedCt6Key, true);

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

        private async Task<bool> GetCredentialProcessTypeAsync(ApplicationDbContext context)
        {
            try
            {
                if (CredentialProcessType == null)
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
