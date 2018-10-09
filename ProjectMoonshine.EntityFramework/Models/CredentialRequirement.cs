using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    class CredentialRequirement
    {
        [Key]
        public Guid cr3Key { get; set; }

        public String cr3Ct3Requirement { get; set; }

        public Guid cr3Cr2Key { get; set; }

        public Guid cr3CstKey { get; set; }

        public String cr3Status { get; set; }

        public DateTimeOffset cr3StatusChangeDate { get; set; }

        public String cr3StatusChangeUser { get; set; }

        public DateTimeOffset cr3StartDate { get; set; }

        public DateTimeOffset cr3DueDate { get; set; }

        public DateTimeOffset cr3MetDate { get; set; }

        public String cr3MetUser { get; set; }

        public String cr3MetReason { get; set; }

        public DateTimeOffset cr3ExtensionDate { get; set; }

        public DateTimeOffset cr3ExtensionGrantedDate { get; set; }

        public String cr3ExtensionGrantedUser { get; set; }

        public String cr3ExtensionGrantedReason { get; set; }

        public String cr3Note { get; set; }

        public String cr3Owner { get; set; }

        public Guid cr3Cr3Key { get; set; }

        public Guid cr3ChoiceCt3Key { get; set; }

        public Boolean cr3HideOnlineFlag { get; set; }

        public Guid cr3DataKey { get; set; }

        public String cr3AddUser { get; set; }

        public DateTimeOffset cr3AddDate { get; set; }

        public String cr3ChangeUser { get; set; }

        public DateTimeOffset cr3ChangeDate { get; set; }

        public DateTimeOffset cr3Extension2Date { get; set; }

        public DateTimeOffset cr3Extension2GrantedDate { get; set; }

        public String cr3Extension2GrantedUser { get; set; }

        public String cr3Extension2GrantedReason { get; set; }

        public Boolean cr3AttestmentFlag { get; set; }

        public String cr3SignatureFirstName { get; set; }

        public String cr3SignatureLastName { get; set; }

        public DateTimeOffset cr3RequestedDate { get; set; }

        public DateTimeOffset cr3ReceivedDate { get; set; }

        public Int16 cr3ExamAttempts { get; set; }
    }

}

