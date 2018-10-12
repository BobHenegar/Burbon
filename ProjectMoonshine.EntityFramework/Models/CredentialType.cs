using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialType")]
    public class CredentialType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct1";
        [Key]
        public Guid Ct1Key { get; set; }

        [DisplayName("Credential Code")]
        public string Ct1Credential { get; set; }

        [DisplayName("Description")]
        public string Ct1Description { get; set; }

        //public string Ct1LongDescription { get; set; }

        //public string Ct1Designation { get; set; }

        //public string Ct1DesignationHtml { get; set; }

        //public Guid? Ct1OrgCstKey { get; set; }

        [DisplayName("Start Date")]
        public DateTimeOffset? Ct1StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTimeOffset? Ct1EndDate { get; set; }

        [DisplayName("Initial Process Type")]
        public Guid? Ct1InitCt2Key { get; set; }

        //public string Ct1InitCt2Class { get; set; }

        //public string Ct1InitCt2Method { get; set; }

        [DisplayName("Initial Status")]
        public Guid? Ct1InitCt6Key { get; set; }

        [DisplayName("Renew Process Type")]
        public Guid? Ct1RenewCt2Key { get; set; }

        //public string Ct1RenewCt2Class { get; set; }

        //public string Ct1RenewCt2Method { get; set; }

        [DisplayName("Renew Status")]
        public Guid? Ct1RenewCt6Key { get; set; }

        //public Guid? Ct1ReinstateCt2Key { get; set; }

        //public string Ct1ReinstateCt2Class { get; set; }

        //public string Ct1ReinstateCt2Method { get; set; }

        //public string Ct1ReinstateStatus { get; set; }

        //public string Ct1DataTableName { get; set; }

        //public string Ct1DataTablePrefix { get; set; }

        //public string Ct1ClassAssembly { get; set; }

        //public string Ct1ClassName { get; set; }

        //public Guid? Ct1AddDynKey { get; set; }

        //public Guid? Ct1EditDynKey { get; set; }

        //public string Ct1CstType { get; set; }

        [DisplayName("Order")]
        public int Ct1Order { get; set; }

        //public bool Ct1SyncFlag { get; set; }

        //public string Ct1SyncCt1Key { get; set; }

        //public string Ct1SetCredentialNumberSp { get; set; }

        [DisplayName("Add Date")]
        public DateTimeOffset Ct1AddDate { get; set; }

        [DisplayName("Change Date")]
        public DateTimeOffset? Ct1ChangeDate { get; set; }

        [DisplayName("Add User")]
        public string Ct1AddUser { get; set; }

        [DisplayName("Change User")]
        public string Ct1ChangeUser { get; set; }
        
        #region Navigation Properties

        public virtual List<Credential> Credentials { get; set; }

        [ForeignKey("Ct1RenewCt6Key")]
        public virtual CredentialStatusType RenewStatusType { get; set; }

        [ForeignKey("Ct1InitCt6Key")]
        public virtual CredentialStatusType InitStatusType { get; set; }

        public virtual List<CredentialTypeXCredentialProcessType> CredentialTypeXCredentialProcessTypes { get; set; }

        [ForeignKey("Ct1InitCt2Key")]
        public virtual CredentialProcessType InitCredentialProcessType { get; set; }

        #endregion Navigation Properties
    }
}
