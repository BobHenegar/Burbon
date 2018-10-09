using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Phone")]
    public class Phone : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Pho";
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhoKey { get; set; }
        public Guid? PhoPerKey { get; set; }

        public string PhoNumber { get; set; }
        public string PhoExtention { get; set; }
        [DefaultValue(false)]
        public bool PhoPrimary { get; set; }


        public virtual PhoneType PhoneType {get;set;}
        public virtual Person Person { get; set; }
    }
}
