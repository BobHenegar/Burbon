using ags.credentialing;

namespace ags.requirements
{
    class DegreeRequirement : CredentialRequirement
    {
        //public DegreeRequirement()
        //{
        //    _oCredentialRequirementData = new DynamicData("co_degree", "co_degree_ext", "deg", "deg", "deg_key", "deg_key_ext", "deg_delete_flag", "Degree", null, null, null, null);
        //    AddDataObject("deg", _oCredentialRequirementData, "Degree");
        //}

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        Eval(oConn, oTrans);
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

        //    if (!UtilityFunctions.EmptyString(GetValue("cr3_data_key")) && GetValue("ct6_state") == "UNLOCKED" && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")))
        //    {
        //        SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
        //    }

        //    return errorClass;
        //}

    }
}
