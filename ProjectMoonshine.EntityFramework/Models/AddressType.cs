using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("AddressType")]
    public class AddressType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Adt";
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdtKey { get; set; }

        [StringLength(10)]
        public string AdtType { get; set; }
    }
}
