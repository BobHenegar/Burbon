using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialStatusType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public String ct6AddUser { get; set; }
        public DateTimeOffset ct6AddDate { get; set; }
        public String ct6ChangeUser { get; set; }
        public DateTimeOffset ct6ChangeDate { get; set; }
        public Guid ct6_st1_key { get; set; }
        public String ct6_parent_ct6_code { get; set; }
        public Boolean ct6_terminate_process_flag { get; set; }
        public Boolean ct6_set_process_owner_flag { get; set; }
        public Boolean ct6_set_credential_number_flag { get; set; }
        public Boolean ct6_eweb_read_only_flag { get; set; }

    }
}
