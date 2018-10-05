using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialRequirementType
    {
        public Guid ct3_key { get; set; }
        public String ct3_requirement { get; set; }
        public String ct3_description { get; set; }
        public DateTimeOffset ct3_start_date { get; set; }
        public String ct3_tab_display { get; set; }
        public DateTimeOffset ct3_end_date { get; set; }
        public Guid ct3_ct8_key { get; set; }
        public Guid ct3_obj_key { get; set; }
        public Boolean ct3_has_required_parents_flag { get; set; }
        public Guid ct3_mdt_key { get; set; }
        public String ct3_init_stored_procedure { get; set; }
        public String ct3_eval_stored_procedure { get; set; }
        public Guid ct3_prd_key { get; set; }
        public Boolean ct3_generate_detail_flag { get; set; }
        public Guid ct3_etp_key { get; set; }
        public Guid ct3_evt_key { get; set; }
        public Guid ct3_cex_key { get; set; }
        public Guid ct3_efc_key { get; set; }
        public String ct3_designation { get; set; }
        public Guid ct3_cet_key { get; set; }
        public Int16 ct3_ce_credit { get; set; }
        public Guid ct3_mbt_key { get; set; }
        public Guid ct3_cec_key { get; set; }
        public Guid ct3_det_key { get; set; }
        public String ct3_doc_type { get; set; }
        public Guid ct3_rlt_key { get; set; }
        public String ct3_ethics_statement { get; set; }
        public String ct3_agreement_statement { get; set; }
        public Guid ct3_add_dyn_key { get; set; }
        public Guid ct3_edit_dyn_key { get; set; }
        public Guid ct3_eweb_edit_dyn_key { get; set; }
        public Guid ct3_eweb_wbc_key { get; set; }
        public String ct3_check_procedure { get; set; }
        public String ct3_find_procedure { get; set; }
        public String ct3_set_data_key_flag { get; set; }
        public String ct3_cr3_start_status { get; set; }
        public String ct3_cr3_dependent_status { get; set; }
        public String ct3_cr3_complete_status { get; set; }
        public String ct3_cr3_submit_status { get; set; }
        public String ct3_cr3_met_status { get; set; }
        public String ct3_cr2_start_status { get; set; }
        public String ct3_cr2_met_status { get; set; }
        public Int16 ct3_due_date_number { get; set; }
        public String ct3_due_date_unit { get; set; }
        public String ct3_due_date_rounding { get; set; }
        public Boolean ct3_show_online { get; set; }
        public String ct3_long_description { get; set; }
        public String ct3_edit_image_url { get; set; }
        public String ct3_unmet_image_url { get; set; }
        public String ct3_met_image_url { get; set; }
        public String ct3_statement_content { get; set; }
        public Int16 ct3_default_order { get; set; }
        public String ct3_add_user { get; set; }
        public DateTimeOffset ct3_add_date { get; set; }
        public String ct3_change_user { get; set; }
        public DateTimeOffset ct3_change_date { get; set; }
        public Guid ct3_edit_wiz_key { get; set; }
        public Int16 ct3_min_exam_score { get; set; }
        public Boolean ct3_submit_process_flag { get; set; }
        public Boolean ct3_iweb_generate_invoice_flag { get; set; }
        public String ct3_log_stored_procedure { get; set; }
        public String ct3_experience_units { get; set; }
        public Int16 ct3_experience_units_required { get; set; }
        public String ct3_calculate_start_date_sp { get; set; }
        public String ct3_calculate_due_date_sp { get; set; }
        public String ct3_cr3_denied_status { get; set; }
        public Int16 ct3_exam_max_attempts { get; set; }
    }
}
