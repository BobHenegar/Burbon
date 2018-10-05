using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Phone")]
    public class Phone
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhoKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid PhoPerKey { get; set; }

        public string PhoNumber { get; set; }
        public string PhoExtention { get; set; }
        public bool PhoPrimary { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset PhoAddDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset? PhoModifiedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PhoAddUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PhoModifiedUser { get; set; }


        public PhoneType PhoneType {get;set;}
        public virtual Person Person { get; set; }
    }
}
