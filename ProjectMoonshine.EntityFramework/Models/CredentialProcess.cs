using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    class CredentialProcess
    {
        [Key]
        public Guid cr2Key { get; set; }

        public String cr2Ct2Process { get; set; }

        public Guid cr2Cr1Key { get; set; }

        public Guid cr2PerKey { get; set; }

        public Guid cr2Cr2Key { get; set; }

        public String cr2Status { get; set; }

        public DateTimeOffset cr2StatusChangeDate { get; set; }

        public String cr2StatusChangeUser { get; set; }

        public DateTimeOffset cr2StartDate { get; set; }

        public DateTimeOffset cr2DueDate { get; set; }

        public DateTimeOffset cr2SubmitDate { get; set; }

        public DateTimeOffset cr2CompletionDate { get; set; }

        public DateTimeOffset cr2TerminationDate { get; set; }

        public DateTimeOffset cr2ExtensionDate { get; set; }

        public DateTimeOffset cr2ExtensionGrantedDate { get; set; }

        public String cr2ExtensionGrantedUser { get; set; }

        public String cr2ExtensionGrantedReason { get; set; }

        public String cr2Note { get; set; }

        public String cr2Owner { get; set; }

        public DateTimeOffset cr2Due2Date { get; set; }

        public DateTimeOffset cr2Extension2Date { get; set; }

        public DateTimeOffset cr2Extension2GrantedDate { get; set; }

        public DateTimeOffset cr2Extension2GrantedUser { get; set; }

        public String cr2Extension2GrantedReason { get; set; }

        public DateTimeOffset cr2InactiveStartDate { get; set; }

        public DateTimeOffset cr2InactiveEndDate { get; set; }

        public DateTimeOffset cr2FulfillmentDate { get; set; }

        public String cr2EthicsDisclosure { get; set; }

        public String cr2EthicsSummary { get; set; }

        public DateTimeOffset cr2EthicsClearedDate { get; set; }

        public String cr2Agreement { get; set; }

        public DateTimeOffset cr2AgreementDate { get; set; }

        public DateTimeOffset cr2AuditSelectDate { get; set; }

        public DateTimeOffset cr2AuditCompleteDate { get; set; }

        public DateTimeOffset cr2AuditCompleteUser { get; set; }

        public Guid cr2DataKey { get; set; }

        public String cr2ProcessNumber { get; set; }

        public String cr2AddUser { get; set; }

        public DateTimeOffset cr2AddDate { get; set; }

        public String cr2ChangeUser { get; set; }

        public DateTimeOffset cr2ChangeDate { get; set; }

        public virtual List<CredentialRequirement>  CredentialRequirements { get; set; }
        public virtual Credential Credential { get; set; }
        public virtual Person Person { get; set; }


    }
}
