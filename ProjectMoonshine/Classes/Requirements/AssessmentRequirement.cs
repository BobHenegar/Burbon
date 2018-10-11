using System;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    class AssessmentRequirement : CredentialRequirement
    {
        //public DynamicData _oAssessmentType;

        //public AssessmentRequirement()
        //{
        //    _oCredentialRequirementData = new DynamicData("client_naccho_application", null, "am2", null, "am2_key", null, "am2_delete_flag", "Assessment", null, null, null, null);
        //    AddDataObject("am2", _oCredentialRequirementData, "Assessment");

        //    _oAssessmentType = new DynamicData("client_naccho_application_type", null, "am1", null, "am1_key", null, "am1_delete_flag", "AssessmentType", null, null, null, null);
        //    AddDataObject("am1", _oAssessmentType, "AssessmentType");
        //}

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (errorClass.Number == 0)
        //        {
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

        //    if (UtilityFunctions.EmptyString(GetValue("cr3_data_key")))
        //    {
        //        string strCurrentAssessmentKey = FindCurrentAssessment(oConn, oTrans);

        //        if (!UtilityFunctions.EmptyString(strCurrentAssessmentKey))
        //        {
        //            SetValue("cr3_data_key", strCurrentAssessmentKey);
        //            GetDataObject("cr3").Update(oConn, oTrans);

        //            LoadRelatedData(oConn, oTrans);
        //        }
        //    }

        //    if (!UtilityFunctions.EmptyString(GetValue("cr3_data_key")) && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")))
        //    {
        //        SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
        //    }

        //    return errorClass;
        //}

        //private string FindCurrentAssessment(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strCurrentAssessmentKey = string.Empty;

        //    string strSQL = "exec ags_find_current_assessment @cst_key='" + GetValue("cr3_cst_key") + "', @am1_key='" + GetValue("ct3_app_am1_key") + "'";
        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    if (oDS.Tables[0].Rows.Count > 0)
        //    {
        //        strCurrentAssessmentKey = oDS.Tables[0].Rows[0]["deg_key"].ToString();
        //    }

        //    return strCurrentAssessmentKey;
        //}

        //public static void CheckForAssessmentRequirements(FacadeClass oAssessment, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "exec ags_check_for_assessment_requirement @am1_key='" + oAssessment.GetValue("am2_am1_key") + "'";

        //    DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

        //    if (oDT.Rows.Count > 0)
        //    {
        //        string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();

        //        CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
        //        oRequirement.SetValue("cr3_data_key", oAssessment.GetValue("am2_key"));
        //        oRequirement.Update(oConn, oTrans);
        //    }
        //}

        
        //public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();
        //    try
        //    {
        //        base.SelectByKey(oConn, oTrans);

        //        if (!UtilityFunctions.EmptyString(_oCredentialRequirementData.GetValue("am2_am1_key")))
        //        {
        //            _oAssessmentType.CurrentKey = _oCredentialRequirementData.GetValue("am2_am1_key");
        //            _oAssessmentType.SelectByKey(oConn, oTrans);
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

        //}

        //public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();
        //    try
        //    {
        //        base.LoadRelatedData(oConn, oTrans);

        //        if (_oAssessmentType.GetValue("am1_key") != _oCredentialRequirementData.GetValue("am2_am1_key"))
        //        {
        //            _oAssessmentType.CurrentKey = _oCredentialRequirementData.GetValue("am2_am1_key");

        //            if (UtilityFunctions.EmptyString(_oAssessmentType.CurrentKey))
        //            {
        //                _oAssessmentType.ClearValues();
        //            }
        //            else
        //            {
        //                _oAssessmentType.SelectByKey(oConn, oTrans);
        //            }
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

        //}
    }
}
