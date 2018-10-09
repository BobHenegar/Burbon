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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmlKey { get; set; }

        public Guid? EmlEltKey { get; set; }

        [DisplayName("EmailAddress")]
        public string EmlAddress { get; set; }
        public bool EmlPrimary { get; set; }
        

        [ForeignKey("EmlEltKey")]
        public virtual EmailType EmailType { get; set; }
    }
}
