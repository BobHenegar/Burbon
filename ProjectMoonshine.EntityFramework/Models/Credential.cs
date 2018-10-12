using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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

        //public string Cr1CredentialNumber { get; set; }
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

        //public DateTimeOffset Cr1CredentialDate { get; set; }

        //public DateTimeOffset Cr1EffectiveDate { get; set; }

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

        public void Add(ApplicationDbContext DbContext)
        {
            try
            {
                if (Cr1StartDate == null) Cr1StartDate = System.DateTimeOffset.Now;

                if (Cr1Ct2Key == null) Cr1Ct2Key = this.CredentialType.InitCredentialProcessType.Ct2Key;
                if (Cr1Ct2Key != null)
                {
                    CurrentCredentialProcess = new CredentialProcess()
                    {
                        Cr2Cr1Key = Cr1Key
                        , Cr2Ct2Key = Guid.Parse(Cr1Ct2Key.ToString())
                        , Cr2Key = Guid.NewGuid()
                        , Cr2Ct6Key = Guid.Parse(CurrentCredentialProcessType.Ct2StartCt6Key.ToString())
                    };

                    Cr1CurrentCr2Key = CurrentCredentialProcess.Cr2Ct2Key;
                }
                DbContext.Add(this);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        }
    }



