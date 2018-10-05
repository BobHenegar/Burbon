using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("AddressType")]
    public class AddressType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdtKey { get; set; }

        [StringLength(10)]
        public string AdtType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset AdtAddDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset? AdtModifiedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AdtAddUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AdtModifiedUser { get; set; }

    }
}
