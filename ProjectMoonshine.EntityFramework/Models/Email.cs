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

        public Guid EmlPerKey { get; set; }

        [DisplayName("Email Address")]
        public string EmlAddress { get; set; }

        [DisplayName("Is Primary Email?")]
        [DefaultValue(false)]
        public bool EmlIsPrimaryEmail { get; set; }

        [DisplayName("Is Work Email?")]
        [DefaultValue(false)]
        public bool EmlIsWorkEmail { get; set; }

        [DisplayName("Is Personal Email?")]
        [DefaultValue(false)]
        public bool EmlIsPersonalEmail { get; set; }

        [DisplayName("Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EmlAddDate { get; set; }
        public DateTimeOffset? EmlChangeDate { get; set; }
        public string EmlAddUser { get; set; }
        public string EmlChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("EmlPerKey")]
        public Person Person { get; set; }
        #endregion Navigation Properties
    }
}
