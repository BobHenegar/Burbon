using System;
using ags.credentialing;


namespace ags.requirements
{
    public class DisclosureRequirement : CredentialRequirement
    {
  //      public DisclosureRequirement()
		//{
  //          _oCredentialRequirementData = new DynamicData("ags_disclosure", "ags_disclosure_ext", "ce7", "ce7", "ce7_key", "ce7_key_ext", "ce7_delete_flag", "Disclosure", null, null, null, null);
  //          AddDataObject("ce7", _oCredentialRequirementData, "Disclosure");
  //      }

		//public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
  //          ErrorClass errorClass = new ErrorClass();

  //          string strCe7Key = Guid.NewGuid().ToString();

  //          SetValue("ce7_key", strCe7Key);
  //          SetValue("ce7_cst_key", GetValue("cr3_cst_key"));
  //          SetValue("ce7_cr3_key", GetValue("cr3_key"));

  //          _oCredentialRequirementData.Insert(oConn, oTrans);

  //          SetValue("cr3_data_key", strCe7Key);
  //          GetDataObject("cr3").Update(oConn, oTrans);

  //          return errorClass;
		//}
		
		//public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

  //          if (GetValue("ce7_disclosure") == "no" || GetValue("ce7_disclosure_no_flag") == "1")
  //          {
  //              SetValue("cr3_met_date", System.DateTime.Now.ToString());
  //          }

  //          else if((GetValue("ce7_disclosure") == "yes" || GetValue("ce7_disclosure_yes_flag") == "1") && !UtilityFunctions.EmptyString(GetValue("ce7_cleared_date")))
  //          {
  //              SetValue("cr3_met_date", System.DateTime.Now.ToString());
  //          }

  //          else if ((GetValue("ce7_disclosure") == "yes" || GetValue("ce7_disclosure_yes_flag") == "1") && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_submit_status")))
  //          {
  //              SetValue("cr3_status", GetValue("ct3_cr3_submit_status"));
  //          }

  //          GetDataObject("cr3").Update(oConn, oTrans);

		//	return errorClass;
		//}
    }
}
