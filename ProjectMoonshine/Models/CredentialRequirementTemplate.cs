using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialRequirementTemplate
    {
        public Guid ct8_key { get; set; }
        public String ct8_template { get; set; }
        public Guid ct8_obj_key { get; set; }
        public String ct8_description { get; set; }
        public String ct8_add_user { get; set; }
        public DateTimeOffset ct8_add_date { get; set; }
        public String ct8_change_user { get; set; }
        public DateTimeOffset ct8_change_date { get; set; }
        public String ct8_check_procedure { get; set; }
        public String ct8_find_procedure { get; set; }
        public Boolean ct8_set_data_key_flag { get; set; }
        public Guid ct8_dyn_key { get; set; }
        public Guid ct8_wbc_key { get; set; }
        public String ct8_init_method_behavior { get; set; }
        public String ct8_eval_method_behavior { get; set; }
}
}
