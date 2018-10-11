using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialRequirementType")]
    public class CredentialRequirementType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct3";

        [Key]
        public Guid Ct3Key { get; set; }

        public string Ct3Requirement { get; set; }

        public string Ct3Description { get; set; }

        //public DateTimeOffset Ct3StartDate { get; set; }

        //public string Ct3TabDisplay { get; set; }

        //public DateTimeOffset Ct3EndDate { get; set; }

        [DisplayName("Template")]
        public Guid Ct3Ct8Key { get; set; }

        //public bool Ct3HasRequiredParentsFlag { get; set; }

        //public Guid Ct3MdtKey { get; set; }

        //public string Ct3InitStoredProcedure { get; set; }

        //public string Ct3EvalStoredProcedure { get; set; }

        //public Guid Ct3PrdKey { get; set; }

        //public bool Ct3GenerateDetailFlag { get; set; }

        //public Guid Ct3EtpKey { get; set; }

        //public Guid Ct3EvtKey { get; set; }

        //public Guid Ct3CexKey { get; set; }

        //public Guid Ct3EfcKey { get; set; }

        //public string Ct3Designation { get; set; }

        //public Guid Ct3CetKey { get; set; }

        //public int Ct3CeCredit { get; set; }

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

        public Guid? Ct3StartCt6Key { get; set; }

        public Guid? Ct3CompleteCt6Key { get; set; }

        public Guid? Ct3SubmitCt6Key { get; set; }

        public Guid? Ct3MetCt6Key { get; set; }

        //public string Ct3Cr3DependentStatus { get; set; }

        //public string Ct3Cr3CompleteStatus { get; set; }

        //public string Ct3Cr3SubmitStatus { get; set; }

        //public string Ct3Cr3MetStatus { get; set; }

        //public string Ct3Cr2StartStatus { get; set; }

        //public string Ct3Cr2MetStatus { get; set; }

        //public int Ct3DueDateNumber { get; set; }

        //public string Ct3DueDateUnit { get; set; }

        //public string Ct3DueDateRounding { get; set; }

        //public bool Ct3ShowOnline { get; set; }

        public string Ct3LongDescription { get; set; }

        //public string Ct3EditImageUrl { get; set; }

        //public string Ct3UnmetImageUrl { get; set; }

        //public string Ct3MetImageUrl { get; set; }

        //public string Ct3StatementContent { get; set; }

        //public int Ct3DefaultOrder { get; set; }

        //public int Ct3MinExamScore { get; set; }

        //public bool Ct3SubmitProcessFlag { get; set; }

        //public bool Ct3IwebGenerateInvoiceFlag { get; set; }

        //public string Ct3LogStoredProcedure { get; set; }

        //public string Ct3ExperienceUnits { get; set; }

        //public int Ct3ExperienceUnitsRequired { get; set; }

        //public string Ct3CalculateStartDateSp { get; set; }

        //public string Ct3calculateDueDateSp { get; set; }

        //public string Ct3Cr3DeniedStatus { get; set; }

        //public int Ct3ExamMaxAttempts { get; set; }

        public DateTimeOffset Ct3AddDate { get; set; }

        public DateTimeOffset? Ct3ChangeDate { get; set; }

        public string Ct3AddUser { get; set; }

        public string Ct3ChangeUser { get; set; }


        #region Navigation Properties
        //[ForeignKey("")]
        //public virtual IList<CredentialProcessTypeXCredentialRequirementType> CredentialProcessTypeXCredentialRequirementTypes { get; set; }


        [ForeignKey("Ct3StartCt6Key")]
        public virtual CredentialStatusType StartStatusType { get; set; }

        [ForeignKey("Ct3CompleteCt6Key")]
        public virtual CredentialStatusType CompleteStatusType { get; set; }

        [ForeignKey("Ct3SubmitCt6Key")]
        public virtual CredentialStatusType SubmitStatusType { get; set; }

        [ForeignKey("Ct3MetCt6Key")]
        public virtual CredentialStatusType MetStatusType { get; set; }

        [ForeignKey("Ct3Ct8Key")]
        public virtual CredentialRequirementTemplate Template { get; set; }
        //public virtual List<CredentialRequirement> CredentialRequirements { get; set; }
        #endregion Navigation Properties
    }
}
