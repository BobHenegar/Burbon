using ags.credentialing;


namespace ags.requirements
{
    public class AttestmentRequirement : CredentialRequirement
    {
		public AttestmentRequirement()
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

            if (GetValue("cr3_attestment_flag") == "1")
            {
                SetValue("cr3_met_date", System.DateTime.Now.ToString());
            }

            GetDataObject("cr3").Update(oConn, oTrans);

			return errorClass;
		}
    }
}
