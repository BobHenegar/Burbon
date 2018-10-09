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

        [DisplayName("EmailAddress")]
        public string EmlAddress { get; set; }

        [DefaultValue(false)]
        public bool EmlIsPrimaryEmail { get; set; }

        [DefaultValue(false)]
        public bool EmlIsWorkEmail { get; set; }

        [DefaultValue(false)]
        public bool EmlIsPersonalEmail { get; set; }
        public DateTimeOffset EmlAddDate { get; set; }
        public DateTimeOffset? EmlChangeDate { get; set; }
        public string EmlAddUser { get; set; }
        public string EmlChangeUser { get; set; }

        #region Navigation Properties
        #endregion Navigation Properties
    }
}
