using ags.credentialing;

namespace ags.requirements
{
    public class EventRequirement : CredentialRequirement
    {
  //      public DynamicData _oEvent;
 
		//public EventRequirement()
		//{
		//	_oCredentialRequirementData = new DynamicData("ev_registrant", "ev_registrant_ext", "reg", "reg", "reg_key", "reg_key_ext", "reg_delete_flag", "Registration", null, null, null, null);
		//	AddDataObject("reg", _oCredentialRequirementData, "Regsitration");

  //          _oEvent = new DynamicData("ev_event", "ev_event_ext", "evt", "evt", "evt_key", "evt_key_ext", "evt_delete_flag", "Event", null, null, null, null);
  //          AddDataObject("evt", _oEvent, "Event");
		//}

		//public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass oErr = new ErrorClass();          

		//	oErr = Eval(oConn, oTrans);
			
		//	return oErr;
		//}
		
		//public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

  //          if (!UtilityFunctions.EmptyString(GetValue("cr3_data_key")))
  //          {
  //              if(!UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")) && GetValue("ct6_state").Equals("UNLOCKED"))
  //              {
  //                  SetStatus(GetValue("ct3_cr3_complete_status"), oConn, oTrans);
  //              }

  //              string strEndDate = GetValue("evt_end_date");

  //              if(UtilityFunctions.EmptyString(strEndDate))
  //              {
  //                  strEndDate = DateTime.Now.AddDays(-1).ToString();
  //              }

  //              if (DateTime.Parse(strEndDate) < DateTime.Now
  //                  && GetValue("reg_attendance_flag") == "1")
  //              {
  //                  SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
  //                  SetValue("cr3_met_date", System.DateTime.Now.ToString());
  //              }
  //          }

		//	return errorClass;
		//}
       
  //      public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();
  //          try
  //          {
  //              base.SelectByKey(oConn, oTrans);

  //              if (!UtilityFunctions.EmptyString(_oCredentialRequirementData.GetValue("reg_evt_key")))
  //              {
  //                  _oEvent.CurrentKey = _oCredentialRequirementData.GetValue("reg_evt_key");
  //                  _oEvent.SelectByKey(oConn, oTrans);
  //              }
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

  //      }

  //      public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();
  //          try
  //          {
  //              base.LoadRelatedData(oConn, oTrans);

  //              if (_oEvent.GetValue("evt_key") != _oCredentialRequirementData.GetValue("reg_evt_key"))
  //              {
  //                  _oEvent.CurrentKey = _oCredentialRequirementData.GetValue("reg_evt_key");

  //                  if (UtilityFunctions.EmptyString(_oEvent.CurrentKey))
  //                  {
  //                      _oEvent.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oEvent.SelectByKey(oConn, oTrans);
  //                  }
  //              }
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

  //      }

    }
}
