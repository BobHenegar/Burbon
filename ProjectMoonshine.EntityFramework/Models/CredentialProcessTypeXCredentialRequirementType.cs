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

        [DisplayName("ProcessType")]
        public Guid Ct5Ct2Key { get; set; }

        [DisplayName("RequirementType")]
        public Guid Ct5Ct3Key { get; set; }

        //public DateTimeOffset? Ct5StartDate { get; set; }

        //public DateTimeOffset? Ct5EndDate { get; set; }

        public int Ct5Order { get; set; }

        public bool Ct5SubmitPrereqFlag { get; set; }

        public bool Ct5ExamPrereqFlag { get; set; }

        public bool Ct5ReviewPrereqFlag { get; set; }

        public DateTimeOffset Ct5AddDate { get; set; }

        public DateTimeOffset? Ct5ChangeDate { get; set; }

        public string Ct5AddUser { get; set; }

        public string Ct5ChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("Ct5Ct2Key")]
        public virtual CredentialProcessType CredentialProcessType { get; set; }

        [ForeignKey("Ct5Ct3Key")]
        public virtual CredentialRequirementType CredentialRequirementType { get; set; }

        #endregion Navigation Properties
    }
}
