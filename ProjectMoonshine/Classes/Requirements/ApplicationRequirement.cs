using ags.credentialing;

namespace ags.requirements
{
    public class ApplicationRequirement : CredentialRequirement
    {
		public ApplicationRequirement()
		{
		}

		public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		{
			ErrorClass errorClass = new ErrorClass();

			SetValue("cr3_start_date", System.DateTime.Now.ToString());
			SetValue("cr3_due_date", System.DateTime.Now.ToString());
			//SetValue("cr3_met_date", System.DateTime.Now.ToString());
            //SetValue("cr3_met_user", Config.CurrentUserName);
            //SetValue("cr3_status", GetValue("ct3_cr3_met_status"));

			errorClass = _oData.Update(oConn, oTrans);

			return errorClass;
		}

        public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
        {
            ErrorClass errorClass = new ErrorClass();

            //check for non-null cr2_submit_date on process
            //if non-null, set req cr3_met_date and complete status and make process eval() itself

            if (errorClass.Number == 0 && UtilityFunctions.EmptyString(GetValue("cr3_met_date")))
            {
                if (!UtilityFunctions.EmptyString(GetValue("cr2_submit_date")))
                {
                    SetValue("cr3_met_date", System.DateTime.Now.ToString());
                }
            }

            errorClass = _oData.Update(oConn, oTrans);

            return errorClass;
        }
    }
}
