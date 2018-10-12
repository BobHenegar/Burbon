using ags.credentialing;

namespace ags.requirements
{
    class MembershipRequirement : CredentialRequirement
    {
        //       public MembershipRequirement()
        //	{
        //		_oCredentialRequirementData = new DynamicData("mb_membership", "mb_membership_ext", "mbr", "mbr", "mbr_key", "mbr_key_ext", "mbr_delete_flag", "Membership", null, null, null, null);
        //		AddDataObject("mbr", _oCredentialRequirementData, "Membership");
        //	}

        //       public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //       {
        //           ErrorClass errorClass = new ErrorClass();

        //           try
        //           {
        //               if (errorClass.Number == 0)
        //               {
        //                   Eval(oConn, oTrans);
        //               }
        //           }
        //           catch (Exception e)
        //           {
        //               errorClass.Number = -1;
        //               errorClass.Message = e.Message;
        //           }
        //           finally
        //           {
        //               if (errorClass.Number != 0)
        //               {
        //                   Config.LastError = errorClass;
        //               }
        //           }

        //           return errorClass;
        //       }

        //	public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
        //	{
        //		ErrorClass errorClass = new ErrorClass();

        //           // 5/22/17 sspruce: Baseline "bug" where workflow is called before member flag is calculated so need to force it ourselves.
        //           if (!UtilityFunctions.EmptyString(GetValue("mbr_key")))
        //           {
        //               DataUtils.ExecuteSql("exec dbo.mb_set_member_flag_default '" + GetValue("mbr_cst_key") + "'", oConn, oTrans);
        //               GetDataObject("cst").SelectByKey(oConn, oTrans);
        //           }

        //           if (GetValue("cst_member_flag") == "1")
        //           {
        //               if (UtilityFunctions.EmptyString(GetValue("ct3_mbt_key")) || GetValue("mbr_mbt_key") == GetValue("ct3_mbt_key"))
        //               {
        //                   SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
        //                   SetValue("cr3_met_date", System.DateTime.Now.ToString());
        //               }
        //           }

        //		errorClass = _oData.Update(oConn, oTrans);

        //		return errorClass;
        //       }

        //       public static void CheckDroppedMembershipForMembershipRequriement(FacadeClass oMembership, NfDbConnection oConn, NfDbTransaction oTrans)
        //       {
        //           if(!UtilityFunctions.EmptyString(oMembership.GetValue("mbr_terminate_date")))
        //           {
        //               string strSQL = "select cr3_key "
        //                               + " from ags_credential_requirement"
        //                               + " join ags_credential_process on cr3_cr2_key = cr2_key"
        //                               + " where cr3_data_key='" + oMembership.CurrentKey + "'"
        //                               + " and cr2_completion_date is null"
        //                               + " order by cr3_add_date desc";

        //               DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

        //               foreach (DataRow oDR in oDT.Rows)
        //               {
        //                   string strRequirementKey = oDR["cr3_key"].ToString();

        //                   CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);

        //                   oRequirement.LoadRelatedData(oConn, oTrans);

        //                   oRequirement.UndoMetDate("Terminated", oConn, oTrans);

        //                   oRequirement.Update(oConn, oTrans);
        //               }
        //           }
        //       }
    }
}
