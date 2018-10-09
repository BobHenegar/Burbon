using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("EmailType")]
    public class EmailType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Elt";
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EltKey { get; set; }


        [StringLength(10)]
        public string EltType { get; set; }

    }
}
