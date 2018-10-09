using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public abstract class EntityBase
    {

        [NotMapped]
        public DateTimeOffset AddDate { get; set; }

        [NotMapped]
        public DateTimeOffset? ChangeDate { get; set; }

        [NotMapped]
        public string AddUser { get; set; }

        [NotMapped]
        public string ChangeUser { get; set; }
    }
}
