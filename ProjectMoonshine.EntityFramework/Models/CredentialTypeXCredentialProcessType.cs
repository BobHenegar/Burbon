using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    [Table("CredentialTypeXCredentialProcessType")]
    public class CredentialTypeXCredentialProcessType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct4";
        [Key]
        public Guid Ct4Key { get; set; }

        public Guid Ct4Ct1Key { get; set; }

        public Guid Ct4Ct2Key { get; set; }

        public DateTimeOffset Ct4StartDate { get; set; }

        public DateTimeOffset Ct4EndDate { get; set; }

        public int Ct4Order { get; set; }

        public DateTimeOffset Ct4AddDate { get; set; }

        public DateTimeOffset? Ct4ChangeDate { get; set; }

        public string Ct4AddUser { get; set; }

        public string Ct4ChangeUser { get; set; }


        #region Navigation Properties

        #endregion Navigation Properties
    }
}
