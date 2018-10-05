using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialProcessType
    {
        [Key]
        public Guid ct2_key { get; set; }

        public String ct2_process { get; set; }

        public String ct2_description { get; set; }

        public String ct2_process_type { get; set; }

        public DateTimeOffset ct2_start_date { get; set; }

        public DateTimeOffset ct2_end_date { get; set; }

        public String ct2_class_assembly { get; set; }

        public String ct2_class_name { get; set; }

        public String ct2_data_table_name { get; set; }

        public String ct2_data_table_prefix { get; set; }

        public Guid ct2_add_dyn_key { get; set; }

        public Guid ct2_edit_dyn_key { get; set; }

        public String ct2_cr2_start_status { get; set; }

        public String ct2_cr2_submitted_status { get; set; }

        public String ct2_cr2_completion_status { get; set; }

        public String ct2_cr1_start_status { get; set; }

        public String ct2_cr1_completion_status { get; set; }

        public Guid ct2_initial_cct_key { get; set; }

        public Guid ct2_completion_cct_key { get; set; }

        public Guid ct2_next_ct2_key { get; set; }

        public Int16 ct2_due_date_number { get; set; }

        public String ct2_due_date_unit { get; set; }

        public String ct2_due_date_rounding { get; set; }

        public String ct2_long_description { get; set; }

        public String ct2_add_user { get; set; }

        public DateTimeOffset ct2_add_date { get; set; }

        public String ct2_change_user { get; set; }

        public DateTimeOffset ct2_change_date { get; set; }

        public String ct2_calculate_start_date_sp { get; set; }

        public String ct2_calculate_due_date_sp { get; set; }

        public String ct2_set_owner_sp { get; set; }
    }

}
