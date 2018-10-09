using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    [Table("RequirementDedendency")]
    public class RequirementDedendency : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct9";
        [Key]
        public Guid Ct9Key { get; set; }
        public Guid Ct9Ct5Key { get; set; }
        public Guid? Ct9DependsOnCt5Key { get; set; }
        public DateTimeOffset Vt5AddDate { get; set; }
        public DateTimeOffset? Vt5ChangeDate { get; set; }
        public string Ct5AddUser { get; set; }
        public string Ct5ChangeUser { get; set; }


        #region Navigation Properties

        #endregion Navigation Properties
    }
}
