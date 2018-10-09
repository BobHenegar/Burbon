using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("PhoneType")]
    public class PhoneType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Pht";
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhtKey { get; set; }
        public string PhtType { get; set; }
    }
}
