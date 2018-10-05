using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class RequirementDedendency
    {
        public Guid ct9_key { get; set; }
        public Guid ct9_ct5_key { get; set; }
        public Guid ct9_depends_on_ct5_key { get; set; }
        public String ct9_add_user { get; set; }
        public DateTimeOffset ct9_add_date { get; set; }
        public String ct9_change_user { get; set; }
        public DateTimeOffset ct9_change_date { get; set; }
    }
}
