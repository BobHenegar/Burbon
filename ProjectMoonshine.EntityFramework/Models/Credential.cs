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
        public Guid Cr1Ct6Key { get; set; }

        [DisplayName("Cr1StartDate")]
        public DateTimeOffset Cr1StartDate { get; set; }
        //public DateTimeOffset Cr1StatusChangeDate { get; set; }

        //public string Cr1StatusChangeUser { get; set; }

        [DisplayName("CredentialDate")]
        public DateTimeOffset? Cr1CredentialDate { get; set; }

        [DisplayName("Effective Date")]
        public DateTimeOffset? Cr1EffectiveDate { get; set; }

        //public DateTimeOffset Cr1ValidThruDate { get; set; }

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

        public async Task<bool> Add(ApplicationDbContext DbContext)
        {
            try
            {
                using (var transaction = DbContext.Database.BeginTransaction())
                {
                    if (Cr1StartDate == DateTimeOffset.MinValue) Cr1StartDate = System.DateTimeOffset.Now;

CredentialType = DbContext.CredentialTypes.Single(ct => ct.Ct1Key == Cr1Ct1Key);
                    if (Cr1Ct6Key == null) Cr1Ct6Key = Guid.Parse(CredentialType.Ct1InitCt6Key.ToString());

                    DbContext.Add(this);
                    DbContext.SaveChanges();

                    


                    if (Cr1Ct2Key == null) Cr1Ct2Key = CredentialType.Ct1InitCt2Key;

                    if (Cr1Ct2Key != null)
                    {
                        CurrentCredentialProcessType =
                            DbContext.CredentialProcessTypes.Single(pt => pt.Ct2Key == Cr1Ct2Key);

                        CurrentCredentialProcess = new CredentialProcess()
                        {
                            Cr2Cr1Key = Cr1Key, Cr2Ct2Key = Guid.Parse(Cr1Ct2Key.ToString()), Cr2Key = Guid.NewGuid(),
                            Cr2Ct6Key = Guid.Parse(CurrentCredentialProcessType.Ct2StartCt6Key.ToString())
                        };
                    }



                    DbContext.SaveChanges();

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
if (CurrentCredentialProcess == null)
                    CurrentCredentialProcess =
                        context.CredentialProcesses.Include(p => p.CredentialProcessType).FirstOrDefault(p => p.Cr2Key == Cr1CurrentCr2Key);

                // check if process is terminated
                if (CurrentCredentialProcess.Cr2TerminationDate != null)
                {
                    //RemoveDesignation();
                }
                

                // check if process is complete

                else if (CurrentCredentialProcess.Cr2CompletionDate != null &&
                    CurrentCredentialProcess.Cr2TerminationDate == null)
                {
                    Cr1EffectiveDate = CurrentCredentialProcess.Cr2CompletionDate;
                    if (Cr1CredentialDate == null)  Cr1CredentialDate = CurrentCredentialProcess.Cr2CompletionDate;

                    //TODO: set up credential numbering
                    //Cr1CredentialNumber = GetNextCredentialNumber(CurrentCredentialProcess);

                    //TODO: Add designation to person record?

                    context.Update(this);

                    CredentialProcessType nextProcessType = null;
                    CredentialType = context.CredentialTypes.FirstOrDefault(ct => ct.Ct1Key == Cr1Ct1Key);


                    if (CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2key != null)
                        nextProcessType = context.CredentialProcessTypes.FirstOrDefault(pt =>
                            pt.Ct2Key == CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2key);
                            
                            //Guid.Parse(CurrentCredentialProcess.CredentialProcessType.Ct2NextCt2key.ToString());

                    else if (CredentialType.Ct1RenewCt2Key != null)
                        nextProcessType = context.CredentialProcessTypes.FirstOrDefault(pt => pt.Ct2Key == Guid.Parse(CredentialType.Ct1InitCt2Key.ToString())) ;

                    if (nextProcessType != null)
                    {
                        CredentialProcess nextCredentialProcess = new CredentialProcess()
                        {
                            Cr2Ct2Key = nextProcessType.Ct2Key, Cr2Key = Guid.NewGuid(), Cr2Cr1Key = Cr1Key,
                            Cr2Ct6Key = Guid.Parse(nextProcessType.Ct2StartCt6Key.ToString())
                        };

                        context.Add(nextCredentialProcess);
                        

                        CurrentCredentialProcess = nextCredentialProcess;

                        context.Update(this);
                        

                        //TODO: set valid thru date based on process type / process settings;
                        Cr1Ct6Key = Guid.Parse(CredentialType.Ct1RenewCt6Key.ToString());

                        context.Update(this);
                        
                    }
                    
                    



                    
                }
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

        private string GetNextCredentialNumber(CredentialProcess currentCredentialProcess)
        {
            throw new NotImplementedException();
        }


        private void RemoveDesignation()
        {
            throw new NotImplementedException();
        }
    }
    }



