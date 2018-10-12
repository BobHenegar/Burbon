using ags.credentialing;

namespace ags.requirements
{
    public class CourseRequirement : CredentialRequirement
    {
  //      public DynamicData _oCourse;

  //      public CourseRequirement()
		//{
		//	_oCredentialRequirementData = new DynamicData("ce_course_participant", "ce_course_participant_ext", "cup", "cup", "cup_key", "cup_key_ext", "cup_delete_flag", "CourseParticipant", null, null, null, null);
  //          AddDataObject("cup", _oCredentialRequirementData, "CourseParticipant");

  //          _oCourse = new DynamicData("ce_course", "ce_course_ext", "cec", "cec", "cec_key", "cec_key_ext", "cec_delete_flag", "Course", null, null, null, null);
  //          AddDataObject("cec", _oCourse, "Course");
  //      }

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
  //              // JAR -- we've registered, we have a cup_key record, 
  //              // JAR -- we should COMPLETE our requirement.
  //              // JAR -- #US6335, sprint 4 June 2017
  //              if (GetValue("cr3_status").Equals(GetValue("ct3_cr3_start_status")))
  //                  SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));

  //              if (GetValue("cup_success_flag") == "1")
  //              {
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

  //              // JAR -- #US6336 
  //              if (!UtilityFunctions.EmptyString(GetValue("cup_cec_key")))
  //              {
  //                  _oCourse.CurrentKey = GetValue("cup_cec_key");
  //                  _oCourse.SelectByKey(oConn, oTrans);
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

  //              // JAR -- #US6336 
  //              if (_oCourse.GetValue("cec_key") != GetValue("cup_cec_key"))
  //              {
  //                  _oCourse.CurrentKey = GetValue("cup_cec_key");

  //                  if (UtilityFunctions.EmptyString(_oCourse.CurrentKey))
  //                  {
  //                      _oCourse.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oCourse.SelectByKey(oConn, oTrans);
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

  //      public static void CheckForCourseRequirements(FacadeClass oCourseParticipation, NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          string strSQL = "exec ags_check_for_course_requirement @cup_key='" + oCourseParticipation.GetValue("cup_key") + "'";

  //          DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

  //          if (oDT.Rows.Count > 0)
  //          {
  //              string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();

  //              CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
  //              oRequirement.SetValue("cr3_data_key", oCourseParticipation.GetValue("cup_key"));
  //              oRequirement.SetValue("cr3_status", oRequirement.GetValue("ct3_completed_status"));

  //              oRequirement.LoadRelatedData(oConn, oTrans);

  //              oRequirement.Update(oConn, oTrans);
  //          }
  //      }
    }
}
