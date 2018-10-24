using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialRequirementType")]
    public class CredentialRequirementType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct3";

        [Key]
        public Guid Ct3Key { get; set; }

        [DisplayName("Requirement")]
        public string Ct3Requirement { get; set; }

        [DisplayName("Description")]
        public string Ct3Description { get; set; }

        //public DateTimeOffset Ct3StartDate { get; set; }

        //public string Ct3TabDisplay { get; set; }

        //public DateTimeOffset Ct3EndDate { get; set; }

        [DisplayName("Template")]
        public Guid Ct3Ct8Key { get; set; }

        [DisplayName("Has Required Parents")]
        public bool Ct3HasRequiredParentsFlag { get; set; }

        //public Guid Ct3MdtKey { get; set; }

        //public string Ct3InitStoredProcedure { get; set; }

        [DisplayName("Eval Stored Procedure")]
        public string Ct3EvalStoredProcedure { get; set; }

        //public Guid Ct3PrdKey { get; set; }

        //public bool Ct3GenerateDetailFlag { get; set; }

        //public Guid Ct3EtpKey { get; set; }

        //public Guid Ct3EvtKey { get; set; }

        //public Guid Ct3CexKey { get; set; }

        //public Guid Ct3EfcKey { get; set; }

        //public string Ct3Designation { get; set; }

        [DisplayName("CE Type")]
        public Guid? Ct3CetKey { get; set; }

        [DisplayName("CE Units Required")]
        public float? Ct3CEUnitsRequired { get; set; }

        //public Guid Ct3MbtKey { get; set; }

        //public Guid Ct3CecKLey { get; set; }

        //public Guid Ct3DetKey { get; set; }

        //public string Ct3DocType { get; set; }

        //public Guid Ct3RltKey { get; set; }

        //public string Ct3EthicsStatement { get; set; }

        //public string Ct3AgreementStatement { get; set; }

        //public string Ct3CheckProcedure { get; set; }

        //public string Ct3FindProcedure { get; set; }

        //public string Ct3SetDataKeyFlag { get; set; }

        //public string Ct3Cr3StartStatus { get; set; }

        [DisplayName("Start Status")]
        public Guid? Ct3StartCt6Key { get; set; }

        [DisplayName("Complete Status")]
        public Guid? Ct3CompleteCt6Key { get; set; }

        [DisplayName("Submitted Status")]
        public Guid? Ct3SubmitCt6Key { get; set; }

        [DisplayName("Met Status")]
        public Guid? Ct3MetCt6Key { get; set; }

        [DisplayName("Dependent Status")]
        public Guid? Ct3DependentCt6Key { get; set; }

        [DisplayName("Sync to Process Dates")]
        public bool Ct3SyncToProcessDatesFlag { get; set; }

        [DisplayName("Parent Process Start Status")]
        public Guid? Ct3StartCr2Ct6Key { get; set; }

        [DisplayName("Parent Process Met Status")]
        public Guid? Ct3MetCr2Ct6Key { get; set; }

        [DisplayName("Due Date Number")]
        public short? Ct3DueDateNumber { get; set; }

        [DisplayName("Due Date Unit")]
        public GlobalConstants.DueDateUnit Ct3DueDateUnit { get; set; }

        [DisplayName("Due Date Rounding")]
        public GlobalConstants.DueDateRounding Ct3DueDateRounding { get; set; }

        //public bool Ct3ShowOnline { get; set; }

        [DisplayName("Long Description")]
        public string Ct3LongDescription { get; set; }

        //public string Ct3EditImageUrl { get; set; }

        //public string Ct3UnmetImageUrl { get; set; }

        //public string Ct3MetImageUrl { get; set; }

        //public string Ct3StatementContent { get; set; }

        //public int Ct3DefaultOrder { get; set; }

        //public int Ct3MinExamScore { get; set; }

        [DisplayName("Submit Process Flag")]
        public bool Ct3SubmitProcessFlag { get; set; }

        //public bool Ct3IwebGenerateInvoiceFlag { get; set; }

        //public string Ct3LogStoredProcedure { get; set; }

        //public string Ct3ExperienceUnits { get; set; }

        //public int Ct3ExperienceUnitsRequired { get; set; }

        [DisplayName("Calculate Start Date Stored Procedure")]
        public string Ct3CalculateStartDateSp { get; set; }

        [DisplayName("Calculate Due Date Stored Procedure")]
        public string Ct3CalculateDueDateSp { get; set; }

        //public string Ct3Cr3DeniedStatus { get; set; }

        //public int Ct3ExamMaxAttempts { get; set; }
        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset Ct3AddDate { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Ct3ChangeDate { get; set; }


        [DisplayName("Add User")]
        public string Ct3AddUser { get; set; }

        [DisplayName("Change User")]
        public string Ct3ChangeUser { get; set; }


        #region Navigation Properties
        
        public virtual IList<CredentialProcessTypeXCredentialRequirementType> CredentialProcessTypeXCredentialRequirementTypes { get; set; }


        [ForeignKey("Ct3StartCt6Key")]
        public virtual CredentialStatusType StartStatusType { get; set; }

        [ForeignKey("Ct3CompleteCt6Key")]
        public virtual CredentialStatusType CompleteStatusType { get; set; }

        [ForeignKey("Ct3SubmitCt6Key")]
        public virtual CredentialStatusType SubmitStatusType { get; set; }

        [ForeignKey("Ct3MetCt6Key")]
        public virtual CredentialStatusType MetStatusType { get; set; }

        [ForeignKey("Ct3DependentCt6Key")]
        public CredentialStatusType DependentStatusType { get; set; }

        [ForeignKey("Ct3StartCr2Ct6Key")]
        public CredentialStatusType ParentProcessStartStatusType { get; set; }

        [ForeignKey("Ct3MetCr2Ct6Key")]
        public CredentialStatusType ParentProcessMetStatusType { get; set; }

        [ForeignKey("Ct3Ct8Key")]
        public virtual CredentialRequirementTemplate Template { get; set; }

        [ForeignKey("Ct3CetKey")]
        public virtual CEType CEType { get; set; }
        //public virtual List<CredentialRequirement> CredentialRequirements { get; set; }
        #endregion Navigation Properties
    }
}
