using System;
using ags.credentialing;


namespace ags.requirements
{
    public class DocumentRequestRequirement : CredentialRequirement
    {
  //      public DocumentRequestRequirement()
		//{
  //          _oCredentialRequirementData = new DynamicData("co_document", "co_document_ext", "doc", "doc", "doc_key", "doc_key_ext", "doc_delete_flag", "Document", null, null, null, null);
  //          AddDataObject("doc", _oCredentialRequirementData, "Document");
  //      }

		//public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
  //          ErrorClass errorClass = new ErrorClass();

  //          errorClass = Eval(oConn, oTrans);

  //          return errorClass;
		//}
		
		//public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

  //          if (!UtilityFunctions.EmptyString(GetValue("cr3_requested_date"))
  //              && GetValue("ct6_state").Equals("UNLOCKED")
  //              && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")))
  //          {
  //              SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
  //          }

  //          GetDataObject("cr3").Update(oConn, oTrans);

		//	return errorClass;
		//}
    }
}
