using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Email")]
    public class Email
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmlKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmlEltKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string EmlAddress { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool EmlPrimary { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset EmlAddDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset? EmlModifiedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EmlAddUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EmlModifiedUser { get; set; }


        [ForeignKey("EmlEltKey")]
        public virtual EmailType EmailType { get; set; }
    }
}
