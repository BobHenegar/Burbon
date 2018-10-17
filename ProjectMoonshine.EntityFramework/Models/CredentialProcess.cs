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

        //public DateTimeOffset Cr2SubmitDate { get; set; }

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


        public virtual async Task<bool> Update(ApplicationDbContext context)
        {
            try
            {
                Credential = context.Credentials.Where(c => c.Cr1Key == Cr2Cr1Key).FirstOrDefault();
                bool isCurrentProcess = Credential?.Cr1CurrentCr2Key == Cr2Key;

                CredentialProcess originalCredentialProcess = context.CredentialProcesses
                    .Include(p => p.Credential)
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Cr2Key == Cr2Key);

                //Credential originalCredential = context.Credentials
                //    .AsNoTracking()
                //    .FirstOrDefault(c => c.Cr1Key == Cr2Cr1Key);

                CheckForStatusChange(context, isCurrentProcess, originalCredentialProcess);
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
            if ((originalCredentialProcess.Cr2CompletionDate == null 
                 || originalCredentialProcess.Cr2CompletionDate == DateTimeOffset.MinValue)
                && Cr2CompletionDate != null 
                && Cr2CompletionDate != DateTimeOffset.MinValue)
                await EvaluateProcessCompleted(context, isCurrentProcess, originalCredentialProcess, originalCredential);

            else if (Cr2CompletionDate == null || Cr2CompletionDate == DateTimeOffset.MinValue)
            {
                Eval(context, isCurrentProcess);
                context.Update(this);
                // is the Credential Update necessary?
                //if ()context.Update(this.Credential);

                await EvaluateProcessCompleted(context, isCurrentProcess, originalCredentialProcess, originalCredential);

                // setting as variable here so it can be manipulated during testing
                EvaluateProcessUnderReview(context, isCurrentProcess);

                //TODO / Roadmap: set requirement exensions based on process extension date
            }
        }

        private void EvaluateProcessUnderReview(ApplicationDbContext context, bool isCurrentProcess)
        {
            bool isReviewReady = IsReviewReady(context);
            if ((Cr2CompletionDate == null || Cr2CompletionDate == DateTimeOffset.MinValue) && isReviewReady)
            {
                if (CredentialProcessType.Ct2SubmittedCt6Key != null &&
                    CredentialProcessType.Ct2SubmittedCt6Key != Guid.Empty)
                {
                    SetStatus(context, CredentialProcessType.Ct2SubmittedCt6Key, isCurrentProcess);
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
                    SetStatus(context, CredentialProcessType.Ct2CompletedCt6Key, isCurrentProcess);
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

        private void CheckForStatusChange(ApplicationDbContext context, bool isCurrentProcess, CredentialProcess originalCredentialProcess)
        {
            if (originalCredentialProcess == null || originalCredentialProcess.Cr2Ct6Key != Cr2Ct6Key)
            {
                SetStatus(context, Cr2Ct6Key, isCurrentProcess);
                context.Update(this);
            }
        }

        private bool IsReviewReady(ApplicationDbContext context)
        {
            return false;
            throw new NotImplementedException();
        }

        public void SetStatus(ApplicationDbContext context, Guid? ct6Key, bool updateCredential = false)
        {
            // TODO/Roadmap: status type change fields  
            if (ct6Key != null && ct6Key != Guid.Empty)
            {
                Cr2Ct6Key = ct6Key;
                if (StatusType == null)
                    StatusType = context.CredentialStatusTypes.Where(st => st.Ct6Key == ct6Key).FirstOrDefault();

                if (StatusType != null)
                {
                    if (updateCredential)
                    {
                        if (Credential == null) context.Credentials.Where(c => c.Cr1Key == Cr2Cr1Key).FirstOrDefault();

                        if (Credential != null)
                        {
                            if (StatusType.Ct6ParentCt6Key != null && StatusType.Ct6ParentCt6Key != Guid.Empty)
                            {
                                Credential.Cr1Ct6Key = StatusType.Ct6ParentCt6Key;
                            }

                            //TODO or Roadmap: Set process owner flag


                            if (StatusType.Ct6SetCredentialNumberFlag && (Credential.Cr1CredentialNumber == null || Credential.Cr1CredentialNumber == 0))
                            {
                                Credential.Cr1CredentialNumber = CredentialingUtils.GetNextCredentialNumber(context, Credential);
                            }
                        }
                    }
                }
            }
        }

        private void Eval(ApplicationDbContext context, bool updateCredential = false)
        {
            //throw new NotImplementedException();
        }

        public async Task<bool> Add(ApplicationDbContext context)
        {
            try
            {
                if (Cr2Cr1Key == Guid.Empty) return false;
                if (Cr2Ct2Key == Guid.Empty) return false;
                if (Cr2Key == Guid.Empty) Cr2Key = Guid.NewGuid();
                CredentialProcessType = context.CredentialProcessTypes.FirstOrDefault(pt => pt.Ct2Key == Cr2Ct2Key);

                if (CredentialProcessType == null) return false;
                
                if (Cr2Ct6Key == null || Cr2Ct6Key == Guid.Empty) Cr2Ct6Key =   CredentialProcessType.Ct2StartCt6Key ;
// TODO: set credential status based on ct2cr1startct6key

                context.Add(this);
                await context.SaveChangesAsync();

                Credential = context.Credentials.Where(c => Cr2Cr1Key == c.Cr1Key).Include(c=> c.CredentialType).FirstOrDefault();

                Init();

                SetProcessStartDate(context);

                SetProcessDueDate(context);

                context.Update(this);
                context.SaveChanges();

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

        private void SetProcessDueDate(ApplicationDbContext context)
        {
            if (Cr2DueDate != null && Cr2DueDate != DateTimeOffset.MinValue) return;

            if (!string.IsNullOrWhiteSpace(CredentialProcessType.Ct2CalculateDueDateSp))
            {
                Cr2DueDate = CredentialingUtils.CalculateDateFromSp(context, CredentialProcessType.Ct2CalculateDueDateSp, Cr2Key.ToString());
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

        private void SyncCredential(ApplicationDbContext context)
        {
            throw new NotImplementedException();
        }

//

        private void SetProcessStartDate(ApplicationDbContext context)
        {
            // Check if start date is already set
            if (Cr2StartDate != null && Cr2StartDate != DateTimeOffset.MinValue) return;

            // Try to set based on calculation sproc
            if (!string.IsNullOrWhiteSpace(CredentialProcessType.Ct2CalculateStartDateSp))
            {
                Cr2StartDate = CredentialingUtils.CalculateDateFromSp(context, CredentialProcessType.Ct2CalculateStartDateSp,
                    Cr2Key.ToString());
            }

            if (Cr2StartDate != null && Cr2StartDate != DateTimeOffset.MinValue) return;

            // Try to set based on prior due date
            DateTimeOffset? priorDueDate = context.CredentialProcesses
                .Include(p => p.Credential)
                .Where(p => p.Cr2Key != Cr2Key
                            && p.Cr2Ct2Key == CredentialProcessType.Ct2Key
                            && p.Credential.Cr1CusKey == Credential.Cr1CusKey
                )
                .OrderByDescending(p => p.Cr2DueDate)
                .FirstOrDefault()
                ?.Cr2DueDate;

            if (priorDueDate != null && priorDueDate != DateTimeOffset.MinValue)
                Cr2StartDate = ((DateTimeOffset) priorDueDate).AddDays(1);

            // Try to set based on cr1 valid thru date
            if (Credential.Cr1ValidThruDate != null && Credential.Cr1ValidThruDate != DateTimeOffset.MinValue)
                Cr2StartDate = ((DateTimeOffset) Credential.Cr1ValidThruDate).AddDays(1);

            // Set to current date by default
            if (Cr2StartDate == null || Cr2StartDate == DateTimeOffset.MinValue) Cr2StartDate = DateTimeOffset.Now;

            //context.Update(this);
        }


        public void Init()
        {

        }


    }
}
