using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialProcessTypeXCredentialRequirementType")]
    public class CredentialProcessTypeXCredentialRequirementType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct5";
        [Key]
        public Guid Ct5Key { get; set; }

        [DisplayName("Process Type")]
        public Guid Ct5Ct2Key { get; set; }

        [DisplayName("Requirement Type")]
        public Guid Ct5Ct3Key { get; set; }

        //public DateTimeOffset? Ct5StartDate { get; set; }

        //public DateTimeOffset? Ct5EndDate { get; set; }

        [DisplayName("Order")]
        public int Ct5Order { get; set; }

        [DisplayName("Required for Submit")]
        public bool Ct5SubmitPrereqFlag { get; set; }

        [DisplayName("Required for Exam")]
        public bool Ct5ExamPrereqFlag { get; set; }

        [DisplayName("Required for Review")]
        public bool Ct5ReviewPrereqFlag { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset Ct5AddDate { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Ct5ChangeDate { get; set; }

        [DisplayName("Add User")]
        public string Ct5AddUser { get; set; }

        [DisplayName("Change User")]
        public string Ct5ChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("Ct5Ct2Key")]
        public virtual CredentialProcessType CredentialProcessType { get; set; }

        [ForeignKey("Ct5Ct3Key")]
        public virtual CredentialRequirementType CredentialRequirementType { get; set; }

        #endregion Navigation Properties
    }
}
