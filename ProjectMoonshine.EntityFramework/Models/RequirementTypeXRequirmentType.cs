using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class RequirementTypeXRequirmentType
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ct0_key { get; set; }

        public String ct0_type { get; set; }

        public Guid ct0_parent_ct3_key { get; set; }

        public Guid ct0_child_ct3_key { get; set; }

        public String ct0_description { get; set; }

        public DateTimeOffset ct0_start_date { get; set; }

        public DateTimeOffset ct0_end_date { get; set; }

        public Int16 ct0_order { get; set; }

        public String ct0_parent_ct3_state { get; set; }

        public String ct0AddUser { get; set; }

        public DateTimeOffset ct0AddDate { get; set; }

        public String ct0ChangeUser { get; set; }

        public DateTimeOffset ct0ChangeDate { get; set; }
    }
}
