using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("EmailType")]
    public class EmailType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EltKey { get; set; }


        [StringLength(10)]
        public string EltType { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset EltAddDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset? EltModifiedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EltAddUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EltModifiedUser { get; set; }

    }
}
