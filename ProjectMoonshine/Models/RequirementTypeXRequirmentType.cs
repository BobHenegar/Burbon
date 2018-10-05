using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class RequirementTypeXRequirmentType
    {
        public Guid ct0_key { get; set; }
        public String ct0_type { get; set; }
        public Guid ct0_parent_ct3_key { get; set; }
        public Guid ct0_child_ct3_key { get; set; }
        public String ct0_description { get; set; }
        public DateTimeOffset ct0_start_date { get; set; }
        public DateTimeOffset ct0_end_date { get; set; }
        public Int16 ct0_order { get; set; }
        public String ct0_parent_ct3_state { get; set; }
        public String ct0_add_user { get; set; }
        public DateTimeOffset ct0_add_date { get; set; }
        public String ct0_change_user { get; set; }
        public DateTimeOffset ct0_change_date { get; set; }
    }
}
