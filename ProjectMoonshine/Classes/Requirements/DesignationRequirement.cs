using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    public class DesignationRequirement : CredentialRequirement
    {
       
        public DesignationRequirement()
		{
            //_oCredentialRequirementData = new DynamicData("co_individual", "co_individual_ext", "ind", "ind", "ind_key", "ind_key_ext", "ind_delete_flag", "Individual", null, null, null, null);
            //AddDataObject("ind", _oCredentialRequirementData, "Individual");
        }

		//public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
  //          ErrorClass errorClass = new ErrorClass();

  //          errorClass = base.Init(oConn, oTrans);
  //          Eval(oConn, oTrans);
  //          return errorClass;
  //      }
		
		//public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
  //          ErrorClass errorClass = new ErrorClass();

		//    if (ValueChanged("ind_designation"))
		//    {
		//        GetDataObject("ind").Update(oConn, oTrans);
		//    }

  //          // is the current designation requirement met by virtue of
  //          // the designation acronym appearing in the individual's designation field?
  //          if (!UtilityFunctions.EmptyString(GetValue("ct3_designation")) &&
  //              GetValue("ind_designation").Contains(GetValue("ct3_designation")))
  //          {
  //              SetStatus(GetValue("ct3_cr3_met_status"), oConn, oTrans);
  //              SetValue("cr3_met_date", System.DateTime.Now.ToString());
  //          }
  //          else
  //          {
  //              // what if someone has a credential that fufills this designation
  //              // requirement but as part of that credentai they do not
  //              // receive an official acronym after their name like "MD" or "MBA"?
  //              string strGetProcedure = "ags_get_designation_credential";
  //              string strSQL = string.Format("exec {0} @ct3_designation='{1}',  @ind_cst_key='{2}'", 
  //                  strGetProcedure, GetValue("ct3_designation"), GetValue("ind_cst_key"));

  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
  //              if (oDS != null && oDS.Tables[0].Rows.Count > 0)
  //              {
  //                  foreach (DataRow oDR in oDS.Tables[0].Rows)
  //                  {
  //                      SetStatus(GetValue("ct3_cr3_met_status"), oConn, oTrans);
  //                      SetValue("cr3_met_date", System.DateTime.Now.ToString());
  //                  }
  //              }
  //          }
  //          return errorClass;
		//}

  //      public static void CheckDroppedDesignationForDesignationRequirement(FacadeClass oFacade, 
  //                                                      NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          // this procedure is doing double duty.
  //          // oFacade might be an ind object or it might be a cr3 object.

  //          // get all the designation requirements that are completed
  //          string strGetProcedure = "ags_get_completed_designation_requirements";
  //          string strSQL = string.Format("exec {0} @ind_cst_key='{1}'",
  //              strGetProcedure, oFacade.GetValue("ind_cst_key"));

  //          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
  //          if (oDS != null)
  //              foreach (DataRow oDR in oDS.Tables[0].Rows)
  //              {
  //                  string strRequirementKey = oDR["cr3_key"].ToString();
  //                  CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
  //                  if (oRequirement == null) continue ;

  //                  string strDesignation = oDR["ct3_designation"].ToString();
  //                  if (!oFacade.GetValue("ind_designation").Contains(strDesignation)) 
  //                  {
  //                      // if there is a designation for this requirement and 
  //                      // that designation is not present in the ind_designation
  //                      // we need to unmeet the requirement
  //                      oRequirement.LoadRelatedData(oConn, oTrans);
  //                      oRequirement.UndoMetDate("Undesignated", oConn, oTrans);
  //                      oRequirement.Update(oConn, oTrans);
  //                  }
  //              }
  //      }

  //      public static void BeforeSaveUpdateIndividualDesignation(PageClass oPage)
  //      {
  //          if (oPage.oFacadeObject.FieldChanged("ind_designation"))
  //          {
  //              using (NfDbConnection oConn = DataUtils.GetConnection())
  //              {
  //                  using (NfDbTransaction oTrans = oConn.BeginTransaction())
  //                  {
  //                      oPage.oFacadeObject.GetDataObject("ind").Update(oConn, oTrans);
  //                      oTrans.Commit();
  //                  }
  //              }
  //          }
  //      }

  //      public static void AfterSaveUpdateIndividualDesignation(PageClass oPage)
  //      {
  //          if (oPage.oFacadeObject.FieldChanged("ind_designation"))
  //          {
  //              using (NfDbConnection oConn = DataUtils.GetConnection())
  //              {
  //                  using (NfDbTransaction oTrans = oConn.BeginTransaction())
  //                  {
  //                      CheckDroppedDesignationForDesignationRequirement(oPage.oFacadeObject, oConn, oTrans);
  //                      oTrans.Commit();
  //                  }
  //              }
  //          }
  //      }

    }
}
