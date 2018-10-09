using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class RequirementDedendency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ct9_key { get; set; }
        public Guid ct9_ct5_key { get; set; }
        public Guid ct9_depends_on_ct5_key { get; set; }
        public String ct9AddUser { get; set; }
        public DateTimeOffset ct9AddDate { get; set; }
        public String ct9ChangeUser { get; set; }
        public DateTimeOffset ct9ChangeDate { get; set; }
    }
}
