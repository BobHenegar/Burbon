using System;
using System.Data;
using ags.credentialing;


namespace ags.requirements
{
    public class EthicsRequirement : CredentialRequirement
    {
		public EthicsRequirement()
		{
        }

		public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		{
            ErrorClass errorClass = new ErrorClass();

            errorClass = Eval(oConn, oTrans);

            return errorClass;
		}
		
		public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		{
			ErrorClass errorClass = new ErrorClass();

            if (GetValue("cr2_agreement") == "YES" && UtilityFunctions.EmptyString(GetValue("cr2_agreement_date")))
            {
                SetValue("cr2_agreement_date", System.DateTime.Now.ToString());
            }

            if (GetValue("cr2_ethics_disclosure") == "NO" && UtilityFunctions.EmptyString(GetValue("cr3_met_date")))
            {
                SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
                SetValue("cr3_met_date", System.DateTime.Now.ToString());
            }
            else if (GetValue("cr2_ethics_disclosure") == "NO" && !UtilityFunctions.EmptyString("cr2_ethics_cleared_date") && UtilityFunctions.EmptyString(GetValue("cr3_met_date")))
            {
                SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
                SetValue("cr3_met_date", System.DateTime.Now.ToString());
            }

            GetDataObject("cr2").Update(oConn, oTrans);
            GetDataObject("cr3").Update(oConn, oTrans);

			return errorClass;
		}

        [Obsolete("displayEthicsContent is deprecated, please use displayEthicsContentPlus instead and note the signature change.")]
        public static void displayEthicsContent(PageClass oPage){
            // nF extension signature example: Page:Page

            using (NfDbConnection oConn = DataUtils.GetConnection()) {
                NfDbTransaction oTrans = oConn.BeginTransaction();

                string strCr3Key = oPage.Request["key"];
                string strSQL = "select ct3_ethics_statement, ct3_agreement_statement from ags_credential_requirement (nolock) join ags_credential_requirement_type (nolock) on cr3_ct3_requirement = ct3_requirement where cr3_key = '" + strCr3Key + "'";
                DataSet oDS = DataUtils.GetDataSet(strSQL);

                if (oDS.Tables[0].Rows.Count == 1) {
                    Label oLabel = (Label)oPage.FindControl("ct3_ethics_statement");
                    oLabel.Text = oDS.Tables[0].Rows[0]["ct3_ethics_statement"].ToString();
                }
                oTrans.Commit();
            }

        }

        public static void displayEthicsContentPlus(PageClass oPage, string divHeight,string divWidth) {
            // nF extension signature example: Page:Page;String:300;String:950

            using (NfDbConnection oConn = DataUtils.GetConnection()) {
                NfDbTransaction oTrans = oConn.BeginTransaction();

                string strCr3Key = oPage.Request["key"];
                string strSQL = "select ct3_ethics_statement, ct3_agreement_statement from ags_credential_requirement (nolock) join ags_credential_requirement_type (nolock) on cr3_ct3_requirement = ct3_requirement where cr3_key = '" + strCr3Key + "'";
                DataSet oDS = DataUtils.GetDataSet(strSQL);

                if (oDS.Tables[0].Rows.Count == 1) {
                    HtmlGenericControl div = (HtmlGenericControl)oPage.FindControl("DIV_0");
                    string content = oDS.Tables[0].Rows[0]["ct3_ethics_statement"].ToString();

                    div.InnerHtml = @"<div style='height: " + divHeight + "px; width: " + divWidth + "px; overflow: auto;'>" + System.Web.HttpUtility.HtmlDecode(content) + "</div>";

                }
                oTrans.Commit();
            }
        }


        public static void displayAgreementContent(PageClass oPage){
            using (NfDbConnection oConn = DataUtils.GetConnection()) {
                NfDbTransaction oTrans = oConn.BeginTransaction();

                string strCr3Key = oPage.Request["key"];
                string strSQL = "select ct3_agreement_statement from ags_credential_requirement (nolock) join ags_credential_requirement_type (nolock) on cr3_ct3_requirement = ct3_requirement where cr3_key = '" + strCr3Key + "'";
                DataSet oDS = DataUtils.GetDataSet(strSQL);

                if (oDS.Tables[0].Rows.Count == 1) {
                    Label oLabel = (Label)oPage.FindControl("ct3_agreement_statement");
                    oLabel.Text = oDS.Tables[0].Rows[0]["ct3_agreement_statement"].ToString();
                }
                oTrans.Commit();
            }

        }

    }
}
