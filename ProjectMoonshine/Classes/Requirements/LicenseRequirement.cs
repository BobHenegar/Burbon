using ags.credentialing;

namespace ags.requirements
{
    public class LicenseRequirement : CredentialRequirement
    {
  //      public LicenseRequirement()
		//{
  //          _oCredentialRequirementData = new DynamicData("ce_individual_license", "ce_individual_license_ext", "ixl", "ixl", "ixl_key", "ixl_key_ext", "ixl_delete_flag", "License", null, null, null, null);
		//	AddDataObject("ixl", _oCredentialRequirementData, "License");
  //      }
        
  //      public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {                
  //              Eval(oConn, oTrans);
  //          }
  //          catch (Exception e)
  //          {
  //              errorClass.Number = -1;
  //              errorClass.Message = e.Message;
  //          }
  //          finally
  //          {
  //              if (errorClass.Number != 0)
  //              {
  //                  Config.LastError = errorClass;
  //              }
  //          }

  //          return errorClass;
  //      }

  //      public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();
            
  //          if (!UtilityFunctions.EmptyString(GetValue("cr3_data_key")) && GetValue("ct6_state") == "UNLOCKED" && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")))
  //          {
  //              SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
  //          }

  //          return errorClass;
  //      }
        
    }
}
