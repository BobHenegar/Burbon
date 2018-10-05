using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialStatusType
    {
        public Guid ct6_key { get; set; }
        public String ct6_code { get; set; }
        public String ct6_description { get; set; }
        public String ct6_external_description { get; set; }
        public Guid ct6_type { get; set; }
        public Guid ct6_type_key { get; set; }
        public DateTimeOffset ct6_start_date { get; set; }
        public DateTimeOffset ct6_end_date { get; set; }
        public String ct6_state { get; set; }
        public Boolean ct6_external_flag { get; set; }
        public Guid ct6_cct_key { get; set; }
        public String ct6_add_user { get; set; }
        public DateTimeOffset ct6_add_date { get; set; }
        public String ct6_change_user { get; set; }
        public DateTimeOffset ct6_change_date { get; set; }
        public Guid ct6_st1_key { get; set; }
        public String ct6_parent_ct6_code { get; set; }
        public Boolean ct6_terminate_process_flag { get; set; }
        public Boolean ct6_set_process_owner_flag { get; set; }
        public Boolean ct6_set_credential_number_flag { get; set; }
        public Boolean ct6_eweb_read_only_flag { get; set; }

    }
}
