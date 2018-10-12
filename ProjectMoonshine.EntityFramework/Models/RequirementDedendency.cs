using System;
using System.ComponentModel;
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

        [DisplayName("Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset Ct9AddDate { get; set; }

        public DateTimeOffset? Ct9ChangeDate { get; set; }

        public string Ct9AddUser { get; set; }

        public string Ct9ChangeUser { get; set; }


        #region Navigation Properties

        #endregion Navigation Properties
    }
}
