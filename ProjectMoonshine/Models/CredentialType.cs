using System;

namespace ProjectMoonshine.Models
{
    public class CredentialType
    {
        public Guid ct1_key { get; set; }
        public string ct1_credential { get; set; }
        public string ct1_description { get; set; }
        public string ct1_long_description { get; set; }
        public string ct1_designation { get; set; }
        public string ct1_designation_html { get; set; }
        public Guid ct1_org_cst_key { get; set; }
        public DateTimeOffset ct1_start_date { get; set; }
        public DateTimeOffset ct1_end_date { get; set; }
        public Guid ct1_init_ct2_key { get; set; }
        public string ct1_init_ct2_class { get; set; }
        public string ct1_init_ct2_method { get; set; }
        public string ct1_init_status { get; set; }
        public Guid ct1_renew_ct2_key { get; set; }
        public string ct1_renew_ct2_class { get; set; }
        public string ct1_renew_ct2_method { get; set; }
        public string ct1_renew_status { get; set; }
        public Guid ct1_reinstate_ct2_key { get; set; }
        public string ct1_reinstate_ct2_class { get; set; }
        public string ct1_reinstate_ct2_method { get; set; }
        public string ct1_reinstate_status { get; set; }
        public string ct1_data_table_name { get; set; }
        public string ct1_data_table_prefix { get; set; }
        public string ct1_class_assembly { get; set; }
        public string ct1_class_name { get; set; }
        public Guid ct1_add_dyn_key { get; set; }
        public Guid ct1_edit_dyn_key { get; set; }
        public string ct1_cst_type { get; set; }
        public string ct1_order { get; set; }
        public Boolean ct1_sync_flag { get; set; }
        public string ct1_sync_ct1_key { get; set; }
        public string ct1_add_user { get; set; }
        public DateTimeOffset ct1_add_date { get; set; }
        public string ct1_change_user { get; set; }
        public DateTimeOffset ct1_change_date { get; set; }
        public string ct1_set_credential_number_sp { get; set; }
    }
}
