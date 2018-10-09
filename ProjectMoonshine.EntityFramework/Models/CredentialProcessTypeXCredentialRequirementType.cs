using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialProcessTypeXCredentialRequirementType
    {
        public Guid ct5_key { get; set; }
        public Guid ct5_ct2_key { get; set; }
        public Guid ct5_ct3_key { get; set; }
        public DateTimeOffset ct5_start_date { get; set; }
        public DateTimeOffset ct5_end_date { get; set; }
        public Int16 ct5_order { get; set; }
        public Boolean ct5_submit_prereq_flag { get; set; }
        public Boolean ct5_exam_prereq_flag { get; set; }
        public Boolean ct5_review_prereq_flag { get; set; }
        public String ct5AddUser { get; set; }
        public DateTimeOffset ct5AddDate { get; set; }
        public String ct5ChangeUser { get; set; }
        public DateTimeOffset ct5ChangeDate { get; set; }
    }
}
