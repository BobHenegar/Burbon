using ags.credentialing;

namespace ags.requirements
{
    class RelationshipRequirement : CredentialRequirement
    {
        //public RelationshipRequirement()
        //{
        //    _oCredentialRequirementData = new DynamicData("co_customer_x_customer", "co_customer_x_customer_ext", "cxc", "cxc", "cxc_key", "cxc_key_ext", "cxc_delete_flag", "CustomerXCustomer", null, null, null, null);
        //    AddDataObject("cxc", _oCredentialRequirementData, "CustomerXCustomer");
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
        //        if (!UtilityFunctions.EmptyString(GetValue("cxc_rlt_code")))
        //        {
        //            SetValue("cxc_cst_key_1", GetValue("cr3_cst_key"));
        //            GetDataObject("cxc").Insert(oConn, oTrans);
        //            SetValue("cr3_data_key", GetValue("cxc_key"));
        //            LoadRelatedData(oConn, oTrans);
        //        }
        //    }

        //    if (!UtilityFunctions.EmptyString(GetValue("cr3_data_key")) && GetValue("cr3_status") != GetValue("ct3_cr3_complete_status"))
        //    {
        //        SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
        //    }

        //    return errorClass;
        //}
   
    }
}
