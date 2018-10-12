using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    [Table("RequirementTypeXRequirmentType")]
    public class RequirementTypeXRequirmentType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct0";
        [Key]
        public Guid Ct0Key { get; set; }

        public string Ct0Type { get; set; }

        public Guid? Ct0ParentCt3Key { get; set; }

        public Guid? Ct0ChildCt3Key { get; set; }

        public string Ct0Description { get; set; }

        public DateTimeOffset Ct0StartDate { get; set; }

        public DateTimeOffset? Ct0EndDate { get; set; }

        public int Ct0Order { get; set; }

        public string Ct0ParentCt3State { get; set; }

        [DisplayName("Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTimeOffset Ct0AddDate { get; set; }

        public DateTimeOffset? Ct0ChangeDate { get; set; }

        public string Ct0AddUser { get; set; }

        public string Ct0ChangeUser { get; set; }


        #region Navigation Properties

        #endregion Navigation Properties
    }
}
