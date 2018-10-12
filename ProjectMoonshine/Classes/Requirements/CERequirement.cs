using ags.credentialing;

namespace ags.requirements
{
    public class CERequirement : CredentialRequirement
    {
        private double dCredit { get; set; }
        private double dTotalReportedCredit { get; set; }
        private double dTotalAcceptedCredit { get; set; }

        private enum CreditType
        {
            CEU,
            SelfReported,
            CEDetail,
            Override
        }


        //public CERequirement()
        //{
        //    _oCredentialRequirementData = new DynamicData("ags_ce_period", "ags_ce_period_ext", "ce2", "ce2", "ce2_key", "ce2_key_ext", "ce2_delete_flag", "CE Requirement", null, null, null, null);
        //    AddDataObject("ce2", _oCredentialRequirementData, "CE Period");
        //}

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        Guid guid = Guid.NewGuid();
        //        SetValue("ce2_key", guid.ToString());
        //        SetValue("ce2_cst_key", GetValue("cst_key"));

        //        if (string.IsNullOrWhiteSpace(GetValue("cr3_start_date")))
        //            SetValue("cr3_start_date", GetValue("cr2_start_date"));

        //        if (string.IsNullOrWhiteSpace(GetValue("ct3_due_date_number")) ||
        //            string.IsNullOrWhiteSpace(GetValue("ct3_due_date_unit")) ||
        //            string.IsNullOrWhiteSpace(GetValue("ct3_due_date_rounding")))
        //        {
        //            SetValue("cr3_due_date",
        //                string.IsNullOrWhiteSpace(GetValue("cr2_due_date"))
        //                    ? DateTime.Now.ToString()
        //                    : GetValue("cr2_due_date"));
        //        }

        //        else
        //            SetValue("cr3_due_date",
        //                CredentialingUtils.CalculateDueDate(GetValue("ct3_due_date_number"),
        //                    GetValue("ct3_due_date_unit"), GetValue("ct3_due_date_rounding")));

        //        SetValue("ce2_start_date", GetValue("cr3_start_date"));
        //        SetValue("ce2_end_date", GetValue("cr3_due_date"));

        //        SetValue("ce2_cet_key", GetValue("ct3_cet_key"));

        //        SetValue("ce2_total_required_credit", GetValue("ct3_ce_credit"));
        //        SetValue("ce2_total_reported_credit", "0");
        //        SetValue("ce2_total_accepted_credit", "0");
        //        SetValue("ce2_total_balance_credit", GetValue("ct3_ce_credit"));


        //        errorClass = _oCredentialRequirementData.Insert(oConn, oTrans);
        //        if (errorClass != null && errorClass.HasError)
        //        {
        //            errorClass.LogError();
        //            return errorClass;
        //        }


        //        SetValue("cr3_data_key", guid.ToString());
        //        errorClass = _oData.Update(oConn, oTrans);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();
        //    }
        //    catch (Exception e)
        //    {
        //        errorClass = CommonUtils.HandleCaughtException(e);
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    { 
        //        errorClass = CalculateCECredit2(oConn, oTrans);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();

        //        if (string.IsNullOrWhiteSpace(GetValue("cr3_met_date")))
        //        {
        //            if (Convert.ToDecimal(GetValue("ce2_total_reported_credit")) >= Convert.ToDecimal(GetValue("ce2_total_required_credit"))
        //                && !string.IsNullOrWhiteSpace(GetValue("ct3_cr3_complete_status")))
        //                SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));

        //            if ((!string.IsNullOrWhiteSpace(GetValue("ce2_total_balance_credit")) 
        //                && Convert.ToDecimal(GetValue("ce2_total_balance_credit")) == 0))
        //            {
        //                SetValue("cr3_met_date", DateTime.Now.ToString());
        //                SetValue("cr3_met_user", Config.CurrentUserName);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        errorClass = CommonUtils.HandleCaughtException(e);
        //    }

        //    return errorClass;
        //}

        //private ErrorClass CalculateCECredit2(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass error = new ErrorClass();
        //    try
        //    {
        //        dCredit = 0;
        //        dTotalReportedCredit = 0;
        //        dTotalAcceptedCredit = 0;

        //        if (!string.IsNullOrWhiteSpace(GetValue("ct3_ce_override_calculation_sp")))
        //        {
        //            error = CalculateCredits(CreditType.Override, oConn, oTrans);
        //            if (error != null && error.HasError) error.LogError();
        //        }

        //        else
        //        {
        //            error = CalculateCredits(CreditType.CEU, oConn, oTrans);
        //            if (error != null && error.HasError) error.LogError();

        //            error = CalculateCredits(CreditType.SelfReported, oConn, oTrans);
        //            if (error != null && error.HasError) error.LogError();

        //            error = CalculateCredits(CreditType.CEDetail, oConn, oTrans);
        //            if (error != null && error.HasError) error.LogError();
        //        }

        //        SetValue("ce2_total_reported_credit", (dTotalReportedCredit).ToString());
        //        SetValue("ce2_total_accepted_credit", (dTotalAcceptedCredit).ToString());

        //        double dTotalRequired = Convert.ToDouble(GetValue("ce2_total_required_credit"));
        //        double dBalanceHours = dTotalRequired - dTotalAcceptedCredit;

        //        if (dBalanceHours < 0.0) dBalanceHours = 0.0;

        //        SetValue("ce2_total_balance_credit", dBalanceHours.ToString());

        //        error = GetDataObject("ce2").Update(oConn, oTrans);
        //        if (error != null && error.HasError) error.LogError();
        //    }
        //    catch (Exception ex)
        //    {
        //        error = CommonUtils.HandleCaughtException(ex);
        //    }
        //    return error;
        //}

        //private void CalculateCeCredit(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass error = CalculateCECredit2(oConn, oTrans);
        //    if (error != null && error.HasError) error.LogError();
        //}


        //private ErrorClass CalculateCredits(CreditType creditType, NfDbConnection connection, NfDbTransaction transaction)
        //{
        //    ErrorClass error = new ErrorClass();
        //    try
        //    {
        //        string sproc = null;
        //        string creditColumn = null;
        //        string verifiedFlag = null;

        //        switch (creditType)
        //        {
        //            case CreditType.CEU:
        //                sproc = !string.IsNullOrWhiteSpace(GetValue("ct3_ce_get_ceu_credits_sp"))
        //                    ? GetValue("ct3_ce_get_ceu_credits_sp")
        //                    : "ags_get_ceu_credits";
        //                creditColumn = "ceu_credit";
        //                verifiedFlag = "ceu_verified_flag_ext";
        //                break;

        //            case CreditType.SelfReported:
        //                sproc = !string.IsNullOrWhiteSpace(GetValue("ct3_ce_get_self_reported_credits_sp"))
        //                    ? GetValue("ct3_ce_get_self_reported_credits_sp")
        //                    : "ags_get_self_reported_credits";
        //                creditColumn = "sce_credit";
        //                verifiedFlag = "sce_verified_flag";
        //                break;

        //            case CreditType.CEDetail:
        //                sproc = !string.IsNullOrWhiteSpace(GetValue("ct3_ce_get_ce_detail_credits_sp"))
        //                    ? GetValue("ct3_ce_get_ce_detail_credits_sp")
        //                    : "ags_get_ce_detail_credits";
        //                creditColumn = "ce1_credit";
        //                verifiedFlag = "ce1_accepted_date";
        //                break;
                        
        //            case CreditType.Override:
        //                sproc = !string.IsNullOrWhiteSpace(GetValue("ct3_ce_override_calculation_sp"))
        //                    ? GetValue("ct3_ce_override_calculation_sp") : null;
        //                creditColumn = "credit";
        //                verifiedFlag = "verifiedFlag";
        //                break;
        //        }

        //        if (string.IsNullOrWhiteSpace(sproc))
        //            error = CommonUtils.AgsLogError("Calculate credit stored procedure name is invalid.");
        //        else if (string.IsNullOrWhiteSpace(creditColumn))
        //            error = CommonUtils.AgsLogError("Credit column name is invalid.");
        //        else if (string.IsNullOrWhiteSpace(verifiedFlag))
        //            error = CommonUtils.AgsLogError("Verified flag is invalid.");
        //        else
        //        {
        //            string sql = string.Format("EXEC {0} @ce2_key = {1}"
        //                , sproc
        //                , DataUtils.ValuePrep(GetValue("ce2_key"), DataUtils.AVType.av_key, false));
        //            DataSet oDS = DataUtils.GetDataSet(sql, connection, transaction);
        //            if (oDS != null && oDS.Tables.Count > 0 && oDS.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow r in oDS.Tables[0].Rows)
        //                {
        //                    double credit;
        //                    if (double.TryParse(r[creditColumn].ToString(), out credit)) dCredit = credit;

        //                    dTotalReportedCredit = dTotalReportedCredit + dCredit;

        //                    if (r[verifiedFlag] != null
        //                        && !r[verifiedFlag].Equals(DBNull.Value)
        //                        && !string.IsNullOrWhiteSpace(r[verifiedFlag].ToString())
        //                        && r[verifiedFlag].ToString() != "0")
        //                        dTotalAcceptedCredit = dTotalAcceptedCredit + dCredit;
        //                }
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        error = CommonUtils.HandleCaughtException(ex);
        //    }
        //    return error;
        //}
    }
}
