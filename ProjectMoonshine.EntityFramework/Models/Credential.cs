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
using ProjectMoonshine.Models;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Credential")]
    public class Credential : EntityBase
    {

        [NotMapped]
        public const string Prefix = "Cr1";
        [Key]
        public Guid Cr1Key { get; set; }


        [DisplayName("Credential Type")]
        //public string Cr1Ct1Credential { get; set; }
        public Guid Cr1Ct1Key { get; set; }

        [DisplayName("Credential Number")]
        public short? Cr1CredentialNumber { get; set; }

        [DisplayName("Customer")]
        public Guid Cr1CusKey { get; set; }

        [DisplayName("Current Process")]
        public Guid? Cr1CurrentCr2Key { get; set; }

        [NotMapped]
        [DisplayName("Current Process Type")]
        public Guid? Cr1Ct2Key { get; set; }

        [DisplayName("Status")]
        public Guid? Cr1Ct6Key { get; set; }

        [DisplayName("Start Date")]
        public DateTimeOffset? Cr1StartDate { get; set; }
        //public DateTimeOffset Cr1StatusChangeDate { get; set; }

        //public string Cr1StatusChangeUser { get; set; }

        [DisplayName("CredentialDate")]
        public DateTimeOffset? Cr1CredentialDate { get; set; }

        [DisplayName("Effective Date")]
        public DateTimeOffset? Cr1EffectiveDate { get; set; }

        [DisplayName("Valid Through Date")]
        public DateTimeOffset? Cr1ValidThruDate { get; set; }

        //public Guid Cr1Ct2CurrentProcess { get; set; }

        //public Guid Cr1CurrentCr2Key { get; set; }

        //public Guid Cr1NextCr2Key { get; set; }

        //public Guid Cr1ApplicationCr2Key { get; set; }

        //public bool Cr1ExtensionUsedFlag { get; set; }

        //public string Cr1Note { get; set; }

        //public string Cr1Owner { get; set; }

        //public Guid Cr1DataKey { get; set; }

        [DisplayName("Add User")]
        public string Cr1AddUser { get; set; }

        [DisplayName("Add Date")]
        public DateTimeOffset Cr1AddDate { get; set; }

        [DisplayName("Change User")]
        public string Cr1ChangeUser { get; set; }

        [DisplayName("Change Date")]
        public DateTimeOffset? Cr1ChangeDate { get; set; }

        //public string Cr1LegalFirstName { get; set; }

        //public string Cr1LegalLastName { get; set; }


        [ForeignKey("Cr1Ct1Key")]
        public virtual CredentialType CredentialType { get; set; }

        //public virtual List<CredentialProcess> CredentialProcesses { get; set; }

        [ForeignKey("Cr1CusKey")]
        public virtual Customer Customer { get; set; }

        //[InverseProperty("Cr2Cr1Key")]
        public virtual List<CredentialProcess> CredentialProcesses { get; set; }

        [ForeignKey("Cr1CurrentCr2Key")]
        public virtual CredentialProcess CurrentCredentialProcess { get; set; }

        [ForeignKey("Cr1Ct6Key")]
        public virtual CredentialStatusType StatusType { get; set; }

        [ForeignKey("Cr1Ct2Key")]
        public virtual CredentialProcessType CurrentCredentialProcessType { get; set; }

        public virtual async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (!Cr1StartDate.IsDateTimeOffsetValid()) Cr1StartDate = DateTimeOffset.Now;

                    CredentialType = await CredentialingUtils.GetCredentialTypeAsync(context, Cr1Ct1Key);
                        
                        
                    if (! Cr1Ct6Key.IsGuidValid() && CredentialType.Ct1InitCt6Key.IsGuidValid())
                        Cr1Ct6Key = CredentialType.Ct1InitCt6Key;

                    await context.AddAsync(this);
                    await context.SaveChangesAsync();


                    if (!Cr1Ct2Key.IsGuidValid()) Cr1Ct2Key = CredentialType.Ct1InitCt2Key;

                    if (Cr1Ct2Key.IsGuidValid())
                    {
                        CurrentCredentialProcessType =
                            await CredentialingUtils.GetCredentialProcessTypeAsync(context, (Guid) Cr1Ct2Key);
                            
                        CredentialProcess credentialProcess = new CredentialProcess()
                        {
                            Cr2Cr1Key = Cr1Key, Cr2Ct2Key = Guid.Parse(Cr1Ct2Key.ToString()), Cr2Key = Guid.NewGuid(),
                        };

                        if (await credentialProcess.AddAsync(context))
                        {
                            CurrentCredentialProcess = credentialProcess;

                            context.Update(this);
                        }

                        
                    }

                    await context.SaveChangesAsync();

                    transaction.Commit();
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

        public async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    Credential originalCredential = context.Credentials
                        .Include(c => c.CurrentCredentialProcess)
                        .AsNoTracking()
                        .FirstOrDefault(c => c.Cr1Key == Cr1Key);

                    if ((originalCredential == null 
                         || originalCredential.Cr1Ct6Key != Cr1Ct6Key) 
                        && !SetStatus(context, Cr1Ct6Key))
                    {
                        return false;
                    }
                    
                    
                    if (await LoadCurrentCredentialProcess(context))
                    {
                        // check if process is terminated
                        if (CurrentCredentialProcess.Cr2TerminationDate.IsDateTimeOffsetValid()  
                            && (!originalCredential?.CurrentCredentialProcess?.Cr2TerminationDate.IsDateTimeOffsetValid() == null 
                                || originalCredential.CurrentCredentialProcess.Cr2TerminationDate == DateTimeOffset.MinValue ))
                        {
                            TerminateCredential(context);
                        }
                        // check if process is completed
                        else if (CurrentCredentialProcess.Cr2CompletionDate != null &&
                                 CurrentCredentialProcess.Cr2TerminationDate == null)
                        {
                            Cr1EffectiveDate = CurrentCredentialProcess.Cr2CompletionDate;
                            if (Cr1CredentialDate == null)
                                Cr1CredentialDate = CurrentCredentialProcess.Cr2CompletionDate;

                            //TODO: set up credential numbering
                            if (Cr1CredentialNumber == null || Cr1CredentialNumber == 0) Cr1CredentialNumber = await CredentialingUtils.GetNextCredentialNumber(context, this);

                            //TODO: Add designation to person record?

                            context.Update(this);

                            CredentialProcessType nextProcessType = null;

                            CredentialType = await CredentialingUtils.GetCredentialTypeAsync(context, Cr1Ct1Key);

                            if (CredentialType != null)
                            {
                                if (CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2Key.IsGuidValid())
                                    nextProcessType =
                                        await CredentialingUtils.GetCredentialProcessTypeAsync(context,
                                            (Guid) CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2Key);

                                else if (CredentialType.Ct1RenewCt2Key.IsGuidValid())
                                    nextProcessType =
                                        await CredentialingUtils.GetCredentialProcessTypeAsync(context, (Guid) CredentialType.Ct1RenewCt2Key);
                                        
                                        

                                if (nextProcessType != null)
                                {
                                    CredentialProcess nextCredentialProcess = new CredentialProcess()
                                    {
                                        Cr2Ct2Key = nextProcessType.Ct2Key, Cr2Key = Guid.NewGuid(), Cr2Cr1Key = Cr1Key,
                                        Cr2Ct6Key = nextProcessType.Ct2StartCt6Key.IsGuidValid() ? nextProcessType.Ct2StartCt6Key : null
                                    };


                                    if ((await nextCredentialProcess.AddAsync(context)))
                                    {
                                        CurrentCredentialProcess = nextCredentialProcess;

                                        Cr1ValidThruDate = CurrentCredentialProcess.Cr2DueDate;
                                    }

                                    context.Update(this);


                                    //TODO: set valid thru date based on process type / process settings;

                                    if (CredentialType.Ct1RenewCt6Key.IsGuidValid())
                                        SetStatus(context, CredentialType.Ct1RenewCt6Key);
                                }
                            }
                        }
                    }
                    // check if process is complete
                    context.SaveChanges();
                    transaction.Commit();
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

        private void TerminateCredential(ApplicationDbContext context)
        {
            //RemoveDesignation();
            if (Cr1ValidThruDate != null && Cr1ValidThruDate != DateTimeOffset.MinValue)
                Cr1ValidThruDate = CurrentCredentialProcess.Cr2TerminationDate;

            context.Update(this);
        }

        private async Task<bool> LoadCurrentCredentialProcess(ApplicationDbContext context)
        {
            try
            {
                if (Cr1CurrentCr2Key.IsGuidValid() 
                    && (CurrentCredentialProcess == null || CurrentCredentialProcess.Cr2Key != Cr1CurrentCr2Key))
                    CurrentCredentialProcess = await CredentialingUtils.GetCredentialProcessAsync(context, (Guid) Cr1CurrentCr2Key, true);

                return CurrentCredentialProcess != null;
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


        private void RemoveDesignation()
        {
            throw new NotImplementedException();
        }

        public bool SetStatus(ApplicationDbContext context, Guid? ct6Key)
        {
            try
            {
                if (ct6Key.IsGuidValid())
                {
                    Cr1Ct6Key = ct6Key;
                    context.Update(this);
                    //if (StatusType == null)
                    //    StatusType = context.CredentialStatusTypes.Where(st => st.Ct6Key == ct6Key).FirstOrDefault();
                    //if (StatusType != null)
                    //{

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
    }
    }



