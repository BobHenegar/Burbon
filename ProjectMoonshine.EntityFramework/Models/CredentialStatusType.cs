using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialStatusType")]
    public class CredentialStatusType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct6";
        [Key]
        public Guid Ct6Key { get; set; }

        [Required]
        [DisplayName("Status Code")]
        public string Ct6Code { get; set; }

        [DisplayName("Description")]
        public string Ct6Description { get; set; }

        //public string Ct6ExternalDescription { get; set; }

        //public Guid Ct6Type { get; set; }

        //public Guid Ct6TypeKey { get; set; }

        //public DateTimeOffset Ct6StartDate { get; set; }

        //public DateTimeOffset Ct6EndDate { get; set; }

        [DisplayName("State")]
        public Guid? Ct6Cu6Key { get; set; }

        //public bool Ct6ExternalFlag { get; set; }

        //public Guid Ct6CctKey { get; set; }

        //public Guid Ct6St1Key { get; set; }

        [DisplayName("Parent Status")]
        public Guid? Ct6ParentCt6Key { get; set; }

        //public bool Ct6TerminateProcessFlag { get; set; }

        //public bool Ct6SetProcessOwnerFlag { get; set; }

        [DisplayName("Set Credential Number Flag")]
        public bool Ct6SetCredentialNumberFlag { get; set; }

        //public bool Ct6EwebReadOnlyFlag { get; set; }

        [DisplayName("Add Date")]
        public DateTimeOffset Ct6AddDate { get; set; }

        [DisplayName("Change Date")]
        public DateTimeOffset? Ct6ChangeDate { get; set; }

        [DisplayName("Add User")]
        public string Ct6AddUser { get; set; }

        [DisplayName("Change User")]
        public string Ct6ChangeUser { get; set; }

        [ForeignKey("Ct6Cu6Key")]
        public virtual StatusStateType State { get; set; }

        [ForeignKey("Ct6ParentCt6Key")]
        public virtual CredentialStatusType ParentStatusType { get; set; }
    }
}
