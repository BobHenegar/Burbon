using System;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    class AuditRequirement : CredentialRequirement
    {
        //public AuditRequirement()
        //{
        //}

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (errorClass.Number == 0)
        //        {
        //            SetValue("cr3_status", GetValue("ct3_cr3_dependent_status"));
        //            _oData.Update(oConn, oTrans);

        //            Eval(oConn, oTrans);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    if(UtilityFunctions.EmptyString(GetValue("cr3_met_date")) && UtilityFunctions.EmptyString(GetValue("cr2_audit_select_date")))
        //    {
        //        //run sp and set audit field values
        //        //check ct3_eval_procedure for audit selector sp; if no value, return
        //        string sAuditSp = GetValue("ct3_eval_stored_procedure");

        //        if (!string.IsNullOrEmpty(sAuditSp))
        //        {
        //            string sSQL = "exec " + sAuditSp + " '" + GetValue("cr3_key") + "'";
        //            DataSet oDS = DataUtils.GetDataSet(sSQL, oConn, oTrans);

        //            if(oDS.Tables[0].Rows.Count > 0)
        //            {
        //                if (oDS.Tables[0].Rows[0]["audit"].ToString() == "true")
        //                {
        //                    SetValue("cr2_audit_select_date", System.DateTime.Now.ToShortDateString());
        //                    SetValue("cr3_status", GetValue("ct3_cr3_start_status"));
        //                }
        //                else
        //                {
        //                    SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
        //                    SetValue("cr3_met_date", System.DateTime.Now.ToString());
        //                }
        //            }
        //        }
                
        //    }
        //    //else
        //    //{
        //    //    if (!UtilityFunctions.EmptyString(GetValue("cr2_submit_date")) && GetValue("cr3_status") != GetValue("ct3_cr3_submit_status"))
        //    //    {
        //    //        SetValue("cr3_status", GetValue("ct3_cr3_submit_status"));
        //    //    }

        //    //    if (!UtilityFunctions.EmptyString(GetValue("cr2_audit_complete_date")) && GetValue("cr3_status") != GetValue("ct3_cr3_met_status"))
        //    //    {
        //    //        SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
        //    //        SetValue("cr3_met_date", System.DateTime.Now.ToString());
        //    //    }
        //    //}

        //    GetDataObject("cr3").Update(oConn, oTrans);
        //    GetDataObject("cr2").Update(oConn, oTrans);

        //    return errorClass;
        //}

    }
}
