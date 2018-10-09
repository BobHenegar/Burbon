using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    [Table("CredentialType")]
    public class CredentialType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct1";
        [Key]
        public Guid Ct1Key { get; set; }

        public string Ct1Credential { get; set; }
        public string Ct1Description { get; set; }
        public string Ct1LongDescription { get; set; }
        public string Ct1Designation { get; set; }
        public string Ct1DesignationHtml { get; set; }
        public Guid? Ct1OrgCstKey { get; set; }
        public DateTimeOffset Ct1StartDate { get; set; }
        public DateTimeOffset Ct1EndDate { get; set; }
        public Guid? Ct1InitCt2Key { get; set; }
        public string Ct1InitCt2Class { get; set; }
        public string Ct1InitCt2Method { get; set; }
        public string Ct1InitStatus { get; set; }
        public Guid? Ct1RenewCt2Key { get; set; }
        public string Ct1RenewCt2Class { get; set; }
        public string Ct1RenewCt2Method { get; set; }
        public string Ct1RenewStatus { get; set; }
        public Guid? Ct1ReinstateCt2Key { get; set; }
        public string Ct1ReinstateCt2Class { get; set; }
        public string Ct1ReinstateCt2Method { get; set; }
        public string Ct1ReinstateStatus { get; set; }
        public string Ct1DataTableName { get; set; }
        public string Ct1DataTablePrefix { get; set; }
        public string Ct1ClassAssembly { get; set; }
        public string Ct1ClassName { get; set; }
        public Guid? Ct1AddDynKey { get; set; }
        public Guid? Ct1EditDynKey { get; set; }
        public string Ct1CstType { get; set; }
        public string Ct1Order { get; set; }
        public bool Ct1SyncFlag { get; set; }
        public string Ct1SyncCt1Key { get; set; }
        public string Ct1SetCredentialNumberSp { get; set; }
        public DateTimeOffset Ct1AddDate { get; set; }
        public DateTimeOffset? Ct1ChangeDate { get; set; }
        public string Ct1AddUser { get; set; }
        public string Ct1ChangeUser { get; set; }
        #region Navigation Properties
        #endregion Navigation Properties
    }
}
