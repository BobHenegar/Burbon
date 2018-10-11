using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    class EndorsementRequirement : CredentialRequirement {

        public EndorsementRequirement() {
        }

        public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans) {
            ErrorClass errorClass = new ErrorClass();

            errorClass = Eval(oConn, oTrans);

            return errorClass;
        }

        public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans) {
            ErrorClass errorClass = new ErrorClass();

            return errorClass;
        }

        public static void CheckForEndorsementRequirements(FacadeClass oDocument, NfDbConnection oConn, NfDbTransaction oTrans) {
            string strSQL = "exec ags_check_for_endorsement_requirement @en2_key='" + oDocument.GetValue("en2_key") + "'";

            DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

            if (oDT.Rows.Count > 0) {
                string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();

                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
                oRequirement.SetValue("cr3_data_key", oDocument.GetValue("doc_key"));
                oRequirement.LoadRelatedData(oConn, oTrans);
                oRequirement.Update(oConn, oTrans);
            }
        }
    }
}
