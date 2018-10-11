using System;
using System.Collections;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    public class ChecklistRequirement : CredentialRequirement
    {
        private readonly ArrayList _aCompleteStates = new ArrayList() {"COMPLETED", "UNDER REVIEW", "MET"};
        private bool bAllChildrenComplete { get; set; }
        private bool bAllChildrenMet { get; set; }
        private int iChildrenMet { get; set; }
        private int childrenComplete { get; set; }

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();
        //    try
        //    {
        //        SetValue("cr3_start_date", System.DateTime.Now.ToString());
        //        SetValue("cr3_due_date", System.DateTime.Now.ToString());

        //        errorClass = InsertChildRequirements(oConn, oTrans);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();

        //        errorClass = _oData.Update(oConn, oTrans);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();
        //    }
        //    catch (Exception ex)
        //    {
        //        errorClass = CommonUtils.HandleCaughtException(ex);
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();
        //    try
        //    {
        //        bAllChildrenComplete = true;
        //        bAllChildrenMet = true;

        //        iChildrenMet = 0;
        //        childrenComplete = 0;
        //        int iChildrenRequired = 0;
                
        //        if (!string.IsNullOrWhiteSpace(GetValue("ct3_checklist_required_children")))
        //            int.TryParse(GetValue("ct3_checklist_required_children"), out iChildrenRequired);

        //        errorClass = EvaluateChildrenRequirements(oConn, oTrans);
        //        if(errorClass != null && errorClass.HasError) errorClass.LogError();

        //        errorClass = EvaluateMet(oConn, oTrans, iChildrenRequired);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();

        //        errorClass = EvaluateCompletion(oConn, oTrans, iChildrenRequired);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();

        //        errorClass = _oData.Update(oConn, oTrans);
        //        if (errorClass != null && errorClass.HasError) errorClass.LogError();
        //    }
        //    catch (Exception ex)
        //    {
        //        errorClass = CommonUtils.HandleCaughtException(ex);
        //    }
        //    return errorClass;
        //}

        //private ErrorClass EvaluateChildrenRequirements(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        string strSQL = string.Format("EXEC dbo.ags_get_checklist_children_requirement_keys @parentCr3Key = {0}"
        //            , DataUtils.ValuePrep(GetValue("cr3_key"), DataUtils.AVType.av_key, false));

        //        DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //        if (oDS == null || oDS.Tables.Count <= 0 || oDS.Tables[0].Rows.Count <= 0) return errorClass;
        //        foreach (DataRow row in oDS.Tables[0].Rows)
        //        {
        //            if (!CommonUtils.ValidateGuidString(row["cr3_key"])) continue;
        //            string strChildRequirementKey = row["cr3_key"].ToString();
        //            CredentialRequirement oChildRequirement =
        //                CredentialingUtils.GetCredentialRequirementByKey(strChildRequirementKey, oConn, oTrans);

        //            if (oChildRequirement == null) continue;
        //            if (string.IsNullOrWhiteSpace(oChildRequirement.GetValue("cr3_met_date")))
        //            {
        //                errorClass = oChildRequirement.Eval(oConn, oTrans);
        //                if (errorClass != null && errorClass.HasError) errorClass.LogError();
        //            }

        //            if (string.IsNullOrWhiteSpace(oChildRequirement.GetValue("cr3_met_date")))
        //                bAllChildrenMet = false;
        //            else iChildrenMet++;

        //            if (_aCompleteStates.Contains(oChildRequirement.GetValue("ct6_state")))
        //                childrenComplete++;
        //            else
        //                bAllChildrenComplete = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorClass = CommonUtils.HandleCaughtException(ex);
        //    }


        //    return errorClass;
        //}

        //private ErrorClass InsertChildRequirements(NfDbConnection connection, NfDbTransaction transaction)
        //{
        //    ErrorClass error = new ErrorClass();
        //    try
        //    {
        //        // A Child Requirement instance is created for each Child RequirementType associated to the Parent RequirementType.

        //        string sql =
        //            string.Format("EXEC dbo.ags_get_checklist_children_requirement_type_keys @ct0ParentCt3Key = {0}"
        //                , DataUtils.ValuePrep(GetValue("ct3_key"), DataUtils.AVType.av_key, false));

        //        DataSet oDS = DataUtils.GetDataSet(sql, connection, transaction);

        //        if (oDS == null || oDS.Tables.Count <= 0 || oDS.Tables[0].Rows.Count <= 0) return error;
        //        foreach (DataRow row in oDS.Tables[0].Rows)
        //        {
        //            if (!CommonUtils.ValidateGuidString(row["ct0_child_ct3_key"])) continue;
        //            string strChildRequirementTypeKey = row["ct0_child_ct3_key"].ToString();
        //            string strChildRequirementType = CredentialingUtils.GetCredentialRequirementType(strChildRequirementTypeKey, connection, transaction);

        //            CredentialRequirement oChildRequirement = CredentialingUtils.GetCredentialRequirement(strChildRequirementType);
        //            if (oChildRequirement == null) continue;
        //            oChildRequirement.SetValue("cr3_cr3_key", CurrentKey);
        //            oChildRequirement.SetValue("cr3_cr2_key", GetValue("cr3_cr2_key"));
        //            oChildRequirement.SetValue("cr3_ct3_requirement", strChildRequirementType);
        //            oChildRequirement.SetValue("cr3_cst_key", _oCustomer.GetValue("cst_key"));

        //            oChildRequirement.LoadRelatedData(connection, transaction);
        //            error = oChildRequirement.Insert(connection, transaction);
        //            if (error != null && error.HasError) error.LogError();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error = CommonUtils.HandleCaughtException(ex);
        //    }
        //    return error;
        //}

        //private ErrorClass EvaluateCompletion(NfDbConnection connection, NfDbTransaction transaction, int iChildrenRequired = 0)
        //{
        //    ErrorClass error = new ErrorClass();
        //    try
        //    {
        //        if ((bAllChildrenComplete || (iChildrenRequired > 0 && childrenComplete >= iChildrenRequired))
        //            && GetValue("ct6_state").Equals("UNLOCKED", StringComparison.OrdinalIgnoreCase))
        //        {
        //            if (!string.IsNullOrWhiteSpace(GetValue("ct3_checklist_additional_completion_eval_sp")))
        //            {
        //                string sql = string.Format("EXEC {0} @cr3Key = {1}"
        //                    , GetValue("ct3_checklist_additional_completion_eval_sp")
        //                    , DataUtils.ValuePrep(CurrentKey, DataUtils.AVType.av_key, false));
        //                object rawResult = DataUtils.ExecuteScalar(sql, connection, transaction);
        //                if (rawResult != null && !rawResult.Equals(DBNull.Value) && (bool) rawResult)
        //                {
        //                    SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
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

        //private ErrorClass EvaluateMet(NfDbConnection connection, NfDbTransaction transaction, int iChildrenRequired = 0)
        //{
        //    ErrorClass error = new ErrorClass();
        //    try
        //    {
        //        if (bAllChildrenMet || (iChildrenRequired > 0 && iChildrenMet >= iChildrenRequired))
        //        {
        //            if (!string.IsNullOrWhiteSpace(GetValue("ct3_checklist_additional_met_eval_sp")))
        //            {
        //                string sql = string.Format("EXEC {0} @cr3Key = {1}"
        //                    , GetValue("ct3_checklist_additional_met_eval_sp")
        //                    , DataUtils.ValuePrep(CurrentKey, DataUtils.AVType.av_key, false));
        //                object rawResult = DataUtils.ExecuteScalar(sql, connection, transaction);
        //                if (rawResult != null && !rawResult.Equals(DBNull.Value) && (bool) rawResult)
        //                {
        //                    SetValue("cr3_met_date", DateTime.Now.ToString());
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
