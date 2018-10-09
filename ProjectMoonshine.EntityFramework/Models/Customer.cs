using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Customer")]
    public class Customer : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Cus";
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CusKey { get; set; }
        public Guid? CusPerKey { get; set; }

        public virtual Person Person { get; set; }
    }
}
