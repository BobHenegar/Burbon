using System;
using ags.credentialing;


namespace ags.requirements
{
    public class SignatureRequirement : CredentialRequirement
    {
  //      public SignatureRequirement()
		//{
  //          _oCustomerDetail = new DynamicData("co_individual", "co_individual_ext", "ind", "ind", "ind_cst_key", "ind_cst_key_ext", "ind_delete_flag", "Individual", null, null, null, null);
  //          AddDataObject("ind", _oCustomerDetail, "Individual");
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


  //          // JAR = if we have a legal first & last name then we don't need to
  //          // JAR - fetch any CO_Individual data
  //          string strCustomerFirstName = GetValue("cr1_legal_first_name");
  //          string strCustomerLastName = GetValue("cr1_legal_last_name");
  //          if (string.IsNullOrEmpty(strCustomerFirstName) || string.IsNullOrEmpty(strCustomerLastName))
  //          {
  //              string strCustomerKey = _oCustomer.GetValue("cst_key");
  //              CO_Individual oInd = (CO_Individual)DataUtils.InstantiateFacadeObject("Individual");
  //              oInd.CurrentKey = strCustomerKey;
  //              oInd.SelectByKey(oConn, oTrans);

  //              strCustomerFirstName = oInd.GetValue("ind_first_name");
  //              strCustomerLastName = oInd.GetValue("ind_last_name");
  //          }
  //          if (GetValue("cr3_signature_first_name").ToLower() == strCustomerFirstName.ToLower() 
  //                  && GetValue("cr3_signature_last_name").ToLower() == strCustomerLastName.ToLower())
  //          {
  //              SetValue("cr3_met_date", System.DateTime.Now.ToString());
  //          }

  //          GetDataObject("cr3").Update(oConn, oTrans);

		//	return errorClass;
		//}

  //      public static void CheckElectronicSignature(PageClass oPage)
  //      {

  //          // JAR - compare user signature against what we are 
  //          // JAR - asking them, on screen, to supply.
  //          string firstName = ((Label)oPage.FindControl("Text_1")).Text.ToUpper();
  //          string lastName  = ((Label)oPage.FindControl("Text_2")).Text.ToUpper();

  //          if (oPage.oFacadeObject.GetValue("cr3_signature_first_name").ToUpper() != firstName
  //              || oPage.oFacadeObject.GetValue("cr3_signature_last_name").ToUpper() != lastName)
  //          {
  //              ErrorClass oEr = new ErrorClass();

  //              oEr.Number = (int)ErrorClass.ErrorNumber.ValidationFailed;
  //              oEr.Level = ErrorClass.ErrorLevel.Error;
  //              oEr.Message = "Signature must match the First Name and Last Name on file.";
  //              Config.LastError = oEr;
  //          }
  //      }

  //      public static void ClearElectronicSignature(PageClass oPage)
  //      {
  //          if (!UtilityFunctions.EmptyString(oPage.oFacadeObject.GetValue("cr3_met_date")))
  //          {
  //              oPage.oFacadeObject.SetValue("cr3_met_date", "");
  //              oPage.oFacadeObject.SetValue("cr3_met_user", "");
  //              oPage.oFacadeObject.SetValue("cr3_signature_first_name", "");
  //              oPage.oFacadeObject.SetValue("cr3_signature_last_name", "");
  //              oPage.oFacadeObject.SetValue("cr3_status", "Pending Submission");

  //              NfDbConnection oConn = DataUtils.GetConnection();
  //              NfDbTransaction oTrans = oConn.BeginTransaction();

  //              oPage.oFacadeObject.GetDataObject("cr3").Update(oConn, oTrans);
  //              oPage.oFacadeObject.SelectByKey(oConn, oTrans);

  //              oTrans.Commit();
  //              oConn.Close();
  //          }
  //      }
    }
}
