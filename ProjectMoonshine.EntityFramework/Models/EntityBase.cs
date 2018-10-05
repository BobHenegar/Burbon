using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public abstract class EntityBase
    {
        public string Prefix { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset AddDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset? ModifiedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AddUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ModifiedUser { get; set; }

    }
}
