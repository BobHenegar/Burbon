using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialTypeXCredentialProcessType")]
    public class CredentialTypeXCredentialProcessType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct4";
        [Key]
        public Guid Ct4Key { get; set; }

        [DisplayName("Credential Type")]
        public Guid Ct4Ct1Key { get; set; }

        [DisplayName("Process Type")]
        public Guid Ct4Ct2Key { get; set; }

        //public DateTimeOffset Ct4StartDate { get; set; }

        //public DateTimeOffset Ct4EndDate { get; set; }

        public int Ct4Order { get; set; }

        public DateTimeOffset Ct4AddDate { get; set; }

        public DateTimeOffset? Ct4ChangeDate { get; set; }

        public string Ct4AddUser { get; set; }

        public string Ct4ChangeUser { get; set; }


        #region Navigation Properties

        [ForeignKey("Ct4Ct1Key")]
        public virtual CredentialType CredentialType { get; set; }

        [ForeignKey("Ct4Ct2Key")]
        public virtual CredentialProcessType CredentialProcessType { get; set; }


        #endregion Navigation Properties
    }
}
