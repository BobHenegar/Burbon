using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    [Table("CredentialProcessType")]
    public class CredentialProcessType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct2";
        [Key]
        public Guid Ct2Key { get; set; }

        public string Ct2Process { get; set; }

        public string Ct2Description { get; set; }

        public string Ct2ProcessType { get; set; }

        public DateTimeOffset Ct2StartDate { get; set; }

        public DateTimeOffset? Ct2EndDate { get; set; }

        public Guid Ct2NextCt2key { get; set; }

        public int Ct2DueDateNumber { get; set; }

        public string Ct2DueDateUnit { get; set; }

        public string Ct2DueDateRounding { get; set; }

        public string Ct2LongDescription { get; set; }

        public string Ct2CalculateStartDateSp { get; set; }

        public string Ct2CalculateDueDateSp { get; set; }

        public string Ct2SetOwnerSp { get; set; }

        public DateTimeOffset Ct2AddDate { get; set; }

        public DateTimeOffset? Ct2ChangeDate { get; set; }

        public string Ct2AddUser { get; set; }

        public string Ct2ChangeUser { get; set; }

        #region Navigation Properties
        public virtual IList<CredentialProcessTypeXCredentialRequirementType> CredentialProcessTypeXCredentialRequirementTypes { get; set; }
        #endregion Navigation Properties
    }

}
