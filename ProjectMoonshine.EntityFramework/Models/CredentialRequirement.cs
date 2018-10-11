using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ProjectMoonshine.Models;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class CredentialRequirement : EntityBase
    {

        [NotMapped]
        public const string Prefix = "Cr3";

        [Key]
        public Guid Cr3Key { get; set; }

        public Guid Cr3Ct3Key { get; set; }

        public Guid Cr3Cr2Key { get; set; }

        //public Guid Cr3CPerKey { get; set; }

        //public string Cr3Status { get; set; }

        //public DateTimeOffset Cr3StatusChangeDate { get; set; }

        //public string Cr3StatusChangeUser { get; set; }

        //public DateTimeOffset Cr3StartDate { get; set; }

        //public DateTimeOffset Cr3DueDate { get; set; }

        //public DateTimeOffset Cr3MetDate { get; set; }

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

        //public virtual Credential Credential { get; set; }
        [ForeignKey("Cr3Cr2Key")]
        public virtual CredentialProcess CredentialProcess { get; set; }
        //public virtual Person Person { get; set; }

        [ForeignKey("Cr3Ct3Key")]
        public virtual CredentialRequirementType CredentialRequirementType { get; set; }



    }

}

