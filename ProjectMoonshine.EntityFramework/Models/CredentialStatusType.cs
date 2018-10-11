using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    [Table("CredentialStatusType")]
    public class CredentialStatusType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct6";
        [Key]
        public Guid Ct6Key { get; set; }

        public string Ct6Code { get; set; }

        public string Ct6Description { get; set; }

        public string Ct6ExternalDescription { get; set; }

        public Guid Ct6Type { get; set; }

        public Guid Ct6TypeKey { get; set; }

        public DateTimeOffset Ct6StartDate { get; set; }

        public DateTimeOffset Ct6EndDate { get; set; }

        public string Ct6State { get; set; }

        public bool Ct6ExternalFlag { get; set; }

        public Guid Ct6CctKey { get; set; }

        public Guid Ct6St1Key { get; set; }

        public string Ct6ParentCt6Code { get; set; }

        public bool Ct6TerminateProcessFlag { get; set; }

        public bool Ct6SetProcessOwnerFlag { get; set; }

        public bool Ct6SetCredentialNumberFlag { get; set; }

        public bool Ct6EwebReadOnlyFlag { get; set; }

        public DateTimeOffset ct6AddDate { get; set; }

        public DateTimeOffset? ct6ChangeDate { get; set; }

        public string Ct6AddUser { get; set; }

        public string Ct6ChangeUser { get; set; }

    }
}
