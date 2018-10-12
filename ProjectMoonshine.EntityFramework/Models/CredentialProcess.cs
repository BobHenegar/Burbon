using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public Guid Cr2Ct6Key { get; set; }


        //public DateTimeOffset Cr2StatusChangeDate { get; set; }

        //public string Cr2StatusChangeUser { get; set; }

        //public DateTimeOffset Cr2StartDate { get; set; }

        //public DateTimeOffset Cr2DueDate { get; set; }

        //public DateTimeOffset Cr2SubmitDate { get; set; }

        //public DateTimeOffset Cr2CompletionDate { get; set; }

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
        public virtual Credential Credential { get; set; }
        //public virtual Person Person { get; set; }

        public virtual List<CredentialRequirement> CredentialRequirements { get; set; }

        [ForeignKey("Cr2Ct6Key")]
        public CredentialStatusType StatusType { get; set; }

    }
}
