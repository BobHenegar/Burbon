using System;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    class SiteVisitRequirement : CredentialRequirement {


        //public SiteVisitRequirement(){

        //    _oCredentialRequirementData = new DynamicData("ags_site_visit", null, "sv1", null, "sv1_key", null, null, "Site Visit", null, null, null, null);
        //    AddDataObject("sv1", _oCredentialRequirementData, "SiteVisit");


        //}

        //public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans) {
        //    ErrorClass oErr = new ErrorClass();

        //    oErr = Eval(oConn, oTrans);

        //    return oErr;
        //}

        //public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans) {

        //    ErrorClass errorClass = new ErrorClass();

        //    if (GetValue("sv1_sv6_key").ToUpper() == "20B511B8-CB31-4ADC-B505-786BA181A29F"){
        //        SetValue("cr3_status", "Complete");
        //        SetValue("cr3_met_date", System.DateTime.Now.ToString());
        //        SetValue("cr3_met_user", Config.CurrentUserName);

        //    }
        //    else{
        //        SetValue("cr3_status", GetValue("sv1_sv6_key").ToUpper() == "E0892251-FA5D-4428-80BA-320766A12701" ? "Unscheduled" : "Scheduled");
        //    }





        //    return errorClass;

        //}

        //public static void CheckForSiteVisitRequirements(FacadeClass oSiteVisit, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "exec ags_check_for_site_visit_requirement @sv1_key='" + oSiteVisit.GetValue("sv1_key") + "'";

        //    DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

        //    if (oDT.Rows.Count > 0)
        //    {
        //        string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();

        //        CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
        //        oRequirement.SetValue("cr3_data_key", oSiteVisit.GetValue("sv1_key"));
        //        oRequirement.Update(oConn, oTrans);
        //    }
        //}
    }
    
}
