using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public string Cr1CredentialNumber { get; set; }

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

        public async Task<bool> Add(ApplicationDbContext context)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    if (Cr1StartDate == DateTimeOffset.MinValue) Cr1StartDate = System.DateTimeOffset.Now;

                    CredentialType = context.CredentialTypes.Single(ct => ct.Ct1Key == Cr1Ct1Key);
                    if (Cr1Ct6Key == null || Cr1Ct6Key == Guid.Empty)
                        Cr1Ct6Key = Guid.Parse(CredentialType.Ct1InitCt6Key.ToString());

                    context.Add(this);
                    context.SaveChanges();


                    if (Cr1Ct2Key == null) Cr1Ct2Key = CredentialType.Ct1InitCt2Key;

                    if (Cr1Ct2Key != null)
                    {
                        CurrentCredentialProcessType =
                            context.CredentialProcessTypes.Single(pt => pt.Ct2Key == Cr1Ct2Key);


                        CredentialProcess credentialProcess = new CredentialProcess()
                        {
                            Cr2Cr1Key = Cr1Key, Cr2Ct2Key = Guid.Parse(Cr1Ct2Key.ToString()), Cr2Key = Guid.NewGuid(),
                        };

                        if (await credentialProcess.Add(context))
                        {
                            CurrentCredentialProcess = credentialProcess;

                            context.Update(this);
                        }

                        ;
                    }

                    context.SaveChanges();

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception e)
            {
                throw;
                return false;
            }
        }

        public async Task<bool> Update(ApplicationDbContext context)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    GetCurrentCredentialProcess(context);
                    
                    if (CurrentCredentialProcess != null)
                    {
                        // check if process is terminated
                        if (CurrentCredentialProcess.Cr2TerminationDate != null)
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
                            //Cr1CredentialNumber = GetNextCredentialNumber(CurrentCredentialProcess);

                            //TODO: Add designation to person record?

                            context.Update(this);

                            CredentialProcessType nextProcessType = null;
                            CredentialType = context.CredentialTypes.FirstOrDefault(ct => ct.Ct1Key == Cr1Ct1Key);

                            if (CredentialType != null)
                            {
                                if (CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2Key != null)
                                    nextProcessType = context.CredentialProcessTypes
                                        .FirstOrDefault(pt =>
                                            pt.Ct2Key == CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2Key);

                                //Guid.Parse(CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2key.ToString());

                                else if (CredentialType.Ct1RenewCt2Key != null)
                                    nextProcessType = context.CredentialProcessTypes
                                        .FirstOrDefault(pt =>
                                            pt.Ct2Key == Guid.Parse(CredentialType.Ct1InitCt2Key.ToString()));

                                if (nextProcessType != null)
                                {
                                    CredentialProcess nextCredentialProcess = new CredentialProcess()
                                    {
                                        Cr2Ct2Key = nextProcessType.Ct2Key, Cr2Key = Guid.NewGuid(), Cr2Cr1Key = Cr1Key,
                                        Cr2Ct6Key = Guid.Parse(nextProcessType.Ct2StartCt6Key.ToString())
                                    };


                                    if ((await nextCredentialProcess.Add(context)))
                                    {
                                        CurrentCredentialProcess = nextCredentialProcess;

                                        Cr1ValidThruDate = CurrentCredentialProcess.Cr2DueDate;
                                    }

                                    context.Update(this);


                                    //TODO: set valid thru date based on process type / process settings;
                                    Cr1Ct6Key = Guid.Parse(CredentialType.Ct1RenewCt6Key.ToString());

                                    context.Update(this);
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
                Console.WriteLine(e);
                throw;
                return false;
            }
        }

        private void TerminateCredential(ApplicationDbContext context)
        {
            //RemoveDesignation();
        }

        private void GetCurrentCredentialProcess(ApplicationDbContext context)
        {
            if (CurrentCredentialProcess == null)
                CurrentCredentialProcess =
                    context.CredentialProcesses.Include(p => p.CredentialProcessType)
                        .FirstOrDefault(p => p.Cr2Key == Cr1CurrentCr2Key);
        }

        private string GetNextCredentialNumber(CredentialProcess currentCredentialProcess)
        {
            throw new NotImplementedException();
        }


        private void RemoveDesignation()
        {
            throw new NotImplementedException();
        }

        public void SetStatus(ApplicationDbContext context, Guid? ct6Key)
        {
            if (ct6Key != null && ct6Key != Guid.Empty)
            {
                Cr1Ct6Key = ct6Key;
                context.Update(this);
                //if (StatusType == null)
                //    StatusType = context.CredentialStatusTypes.Where(st => st.Ct6Key == ct6Key).FirstOrDefault();
                //if (StatusType != null)
                //{

                //}
            }
        }
    }
    }



