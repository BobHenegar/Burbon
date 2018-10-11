using System;
using ags.credentialing;

namespace ags.requirements
{
    public class ExperienceRequirement : CredentialRequirement{
        //public ExperienceRequirement(){
        //    _oCredentialRequirementData = new DynamicData("ags_experience_period", "ags_experience_period_ext", "ex2", "ex2", "ex2_key", "ex2_key_ext", "ex2_delete_flag", "Exprerience Requirement", null, null, null, null);
        //    AddDataObject("ex2", _oCredentialRequirementData, "Exprerience Period");
        //}

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans){
        //    ErrorClass errorClass = new ErrorClass();

        //    try{
        //        Guid guid = Guid.NewGuid();
        //        SetValue("ex2_key", guid.ToString());
        //        SetValue("ex2_cst_key", GetValue("cst_key"));

        //        Credential oCredential = (Credential)DataUtils.InstantiateFacadeObject("AGSCredential");
        //        oCredential.CurrentKey = GetValue("cr2_cr1_key");
        //        oCredential.SelectByKey();

        //        if (UtilityFunctions.EmptyString(GetValue("cr3_start_date"))){
        //            SetValue("cr3_start_date", GetValue("cr2_start_date"));
        //        }

        //        if (UtilityFunctions.EmptyString(GetValue("cr3_due_date"))){
        //            SetValue("cr3_due_date", CredentialingUtils.CalculateDueDate(GetValue("ct3_due_date_number"), GetValue("ct3_due_date_unit"), GetValue("ct3_due_date_rounding")));

        //            if (UtilityFunctions.EmptyString(GetValue("cr3_due_date"))){
        //                SetValue("cr3_due_date", GetValue("cr2_due_date"));
        //            }
        //        }

        //        SetValue("ex2_start_date", GetValue("cr3_start_date"));
        //        SetValue("ex2_end_date", GetValue("cr3_due_date"));

        //        SetValue("ex2_required_unit", GetValue("ct3_experience_units"));
        //        SetValue("ex2_total_required_units", GetValue("ct3_experience_units_required"));
        //        SetValue("ex2_total_reported_units", "0");
        //        SetValue("ex2_total_acexpted_units", "0");
        //        SetValue("ex2_total_balance_units", GetValue("ct3_experience_units_required"));

        //        if (errorClass.Number == 0){
        //            errorClass = _oCredentialRequirementData.Insert(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0){
        //            SetValue("cr3_data_key", guid.ToString());
        //            errorClass = _oData.Update(oConn, oTrans);
        //        }
        //    }
        //    catch (Exception e){
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally{
        //        if (errorClass.Number != 0){
        //            Config.LastError = errorClass;
        //        }
        //    }
        //    return errorClass;
        //}

        //public override ErrorClass Eval(NfDbConnection conn, NfDbTransaction trans){
        //    var arr = EvalulateExperience(conn,trans);

        //    ErrorClass errorClass = new ErrorClass();

        //    if (arr.Length > 0)
        //    {
        //        SetValue("ex2_total_reported_units", arr[0]);
        //        SetValue("ex2_total_accepted_units", arr[1]);
        //        SetValue("ex2_total_balance_units", arr[2]);
        //    }
            
        //    LoadRelatedData(conn, trans);
            
        //    try {
        //        if (errorClass.Number == 0 && UtilityFunctions.EmptyString(GetValue("cr3_met_date"))) {
        //            if (Convert.ToDecimal(GetValue("ex2_total_reported_units")) >= Convert.ToDecimal(GetValue("ex2_total_required_units")) && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")) && GetValue("cr3_status") != GetValue("ct3_cr3_submit_status")) {
        //                SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
        //            }

        //            if ((!UtilityFunctions.EmptyString(GetValue("ex2_total_balance_units")) && Convert.ToDecimal(GetValue("ex2_total_balance_units")) == 0)) {
        //                SetValue("cr3_met_date", DateTime.Now.ToString());
        //                SetValue("cr3_met_user", Config.CurrentUserName);
        //            }
        //        }
        //    }
        //    catch (Exception e) {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally {
        //        if (errorClass.Number != 0) {
        //            Config.LastError = errorClass;
        //        }
        //    }
        //    return errorClass;
        //}

        //private string[] EvalulateExperience(NfDbConnection conn, NfDbTransaction trans) {

        //    string sql = "";
        //    var arr = new string[3];
        //    var ex2Key = GetValue("ex2_key");
        //    switch (GetValue("ex2_required_unit")) {
        //        case "hour":
        //            sql = String.Format("exec ags_calculated_hourly_experience {0}",
        //                DataUtils.ValuePrep(ex2Key, DataUtils.AVType.av_key, false, false));
        //            break;
        //        case "day":
        //            sql = String.Format("exec ags_calculated_daily_experience {0}",
        //                DataUtils.ValuePrep(ex2Key, DataUtils.AVType.av_key, false, false));
        //            break;
        //        case "month":
        //            sql = String.Format("exec ags_calculated_monthly_experience {0}",
        //                DataUtils.ValuePrep(ex2Key, DataUtils.AVType.av_key, false, false));
        //            break;
        //        case "year":
        //            sql = String.Format("exec ags_calculated_yearly_experience {0}",
        //                DataUtils.ValuePrep(ex2Key, DataUtils.AVType.av_key, false, false));
        //            break;
        //    }

        //    var update = DataUtils.ExecuteSql(sql,conn,trans);

        //    string sql1 = String.Format("exec ags_get_calculated_experience {0}",
        //        DataUtils.ValuePrep(ex2Key, DataUtils.AVType.av_key, false, false));

        //    var data = DataUtils.GetDataSet(sql1,conn,trans);

        //    if (data.Tables[0].Rows.Count > 0) {
        //        string value = data.Tables[0].Rows[0]["ex2_total_balance_units"].ToString();
        //        float number;
        //        float balanceUnits = 0;

        //        if (Single.TryParse(value, out number))
        //            balanceUnits = number;
        //        if (balanceUnits < 0)
        //            balanceUnits = 0;

        //        arr[0] = data.Tables[0].Rows[0]["ex2_total_reported_units"].ToString();
        //        arr[1] = data.Tables[0].Rows[0]["ex2_total_accepted_units"].ToString();
        //        arr[2] = balanceUnits.ToString();
        //    }

        //    return arr;
        //}
    }
}
