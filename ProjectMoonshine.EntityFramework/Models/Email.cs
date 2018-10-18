using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Email")]
    public class Email : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Eml";
        [Key]
        public Guid EmlKey { get; set; }


        [DisplayName("Person")]
        public Guid EmlPerKey { get; set; }

        [DisplayName("Email Address")]
        public string EmlAddress { get; set; }

        [DisplayName("Primary")]
        [DefaultValue(false)]
        public bool EmlIsPrimary { get; set; }

        [DisplayName("Work")]
        [DefaultValue(false)]
        public bool EmlIsWorkEmail { get; set; }

        [DisplayName("Personal")]
        [DefaultValue(false)]
        public bool EmlIsPersonalEmail { get; set; }


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EmlAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? EmlChangeDate { get; set; }

        [DisplayName("Added By")]
        public string EmlAddUser { get; set; }


        [DisplayName("Changed By")]
        public string EmlChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("EmlPerKey")]
        public Person Person { get; set; }
        #endregion Navigation Properties
    }
}
