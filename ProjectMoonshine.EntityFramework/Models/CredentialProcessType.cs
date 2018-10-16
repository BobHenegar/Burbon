using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialProcessType")]
    public class CredentialProcessType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct2";
        [Key]
        public Guid Ct2Key { get; set; }

        [Required]
        [DisplayName("Process Code")]
        public string Ct2Process { get; set; }

        [Required]
        [DisplayName("Process Description")]
        public string Ct2Description { get; set; }

        [DisplayName("Start Status")]
        public Guid? Ct2StartCt6Key { get; set; }

        [DisplayName("Completed Status")]
        public Guid? Ct2CompletedCt6Key { get; set; }

        [DisplayName("Submitted Status")]
        public Guid? Ct2SubmittedCt6Key { get; set; }

        [DisplayName("Process Type")]
        public string Ct2ProcessType { get; set; }

        //public DateTimeOffset Ct2StartDate { get; set; }

        //public DateTimeOffset? Ct2EndDate { get; set; }

        [DisplayName("Next Process Type")]
        public Guid? Ct2NextCt2Key { get; set; }

        [DisplayName("Due Date Number")]
        public short? Ct2DueDateNumber { get; set; }

        [DisplayName("Due Date Unit")]
        public GlobalConstants.DueDateUnit Ct2DueDateUnit { get; set; }

        [DisplayName("Due Date Rounding")]
        public GlobalConstants.DueDateRounding Ct2DueDateRounding { get; set; }

        //public string Ct2LongDescription { get; set; }

        [DisplayName("Custom Start Date Stored Procedure")]
        public string Ct2CalculateStartDateSp { get; set; }

        [DisplayName("Custom Due Date Stored Procedure")]
        public string Ct2CalculateDueDateSp { get; set; }

        [DisplayName("Credential Completed Status")]
        public Guid? Ct2CompletedCr1Ct6Key { get; set; }

        //public string Ct2SetOwnerSp { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset Ct2AddDate { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Ct2ChangeDate { get; set; }

        [DisplayName("Add User")]
        public string Ct2AddUser { get; set; }

        [DisplayName("Change User")]
        public string Ct2ChangeUser { get; set; }

        #region Navigation Properties
        public virtual IList<CredentialProcessTypeXCredentialRequirementType> CredentialProcessTypeXCredentialRequirementTypes { get; set; }

        [ForeignKey("Ct2StartCt6Key")]
        public virtual CredentialStatusType StartStatus { get; set; }

        [ForeignKey("Ct2SubmittedCt6Key")]
        public virtual  CredentialStatusType SubmittedStatus { get; set; }

        [ForeignKey("Ct2CompletedCt6Key")]
        public virtual CredentialStatusType CompletedStatus { get; set; }

        public virtual List<CredentialTypeXCredentialProcessType> CredentialTypeXCredentialProcessTypes { get; set; }

        [ForeignKey("Ct2NextCt2Key")]
        public virtual CredentialProcessType NextCredentialProcessType { get; set; }



        [ForeignKey("Ct2CompletedCr1Ct6Key")]
        public virtual CredentialStatusType CredentialCompletedStatus { get; set; }
        #endregion Navigation Properties
    }

}
