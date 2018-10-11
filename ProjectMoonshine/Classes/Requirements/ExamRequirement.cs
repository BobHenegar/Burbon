using System;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    public class ExamRequirement : CredentialRequirement
    {
        public DynamicData _oExam;

		public ExamRequirement()
		{
			_oCredentialRequirementData = new DynamicData("ce_exam_score", "ce_exam_score_ext", "exs", "exs", "exs_key", "exs_key_ext", "exs_delete_flag", "Exam Score");
			AddDataObject("exs", _oCredentialRequirementData, "Exam Score");

            _oExam = new DynamicData("ce_exam", "ce_exam_ext", "cex", "cex", "cex_key", "cex_key_ext", "cex_delete_flag", "Exam");
            AddDataObject("cex", _oExam, "Exam");
        }

		public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		{
			ErrorClass oErr = new ErrorClass();

            if (GetValue("ct3_generate_detail_flag") == "1")
            {
                string strExamScoreKey = System.Guid.NewGuid().ToString();

                SetValue("exs_key", strExamScoreKey);
                SetValue("exs_cst_key", GetValue("cr3_cst_key"));
                SetValue("exs_cex_key", GetValue("ct3_cex_key"));
                _oCredentialRequirementData.Insert(oConn, oTrans);

                SetValue("cr3_data_key", strExamScoreKey);
                _oData.Update(oConn, oTrans);
            }

			oErr = Eval(oConn, oTrans);
			
			return oErr;
		}
		
		public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		{
			ErrorClass errorClass = new ErrorClass();

            if (GetValue("exs_success_flag") == "1")
            {
                SetValue("cr3_met_date", System.DateTime.Now.ToString());
                SetValue("cr3_met_user", Config.CurrentUserName);
            }

            else if (!UtilityFunctions.EmptyString(GetValue("exs_raw_score")) && !UtilityFunctions.EmptyString(GetValue("ct3_min_exam_score")))
            {
                decimal dMinExamScore = Convert.ToDecimal(GetValue("ct3_min_exam_score"));
                decimal dExamScore = Convert.ToDecimal(GetValue("exs_raw_score"));

                if(dExamScore >= dMinExamScore)
                {
                    SetValue("cr3_met_date", System.DateTime.Now.ToString());
                    SetValue("cr3_met_user", Config.CurrentUserName);
                }
                else
                {
                    int attemptLimit;
                    if (!int.TryParse(GetValue("ct3_exam_max_attempts"), out attemptLimit)) attemptLimit = 0;

                    string strSQL = "select attempts = count(exs_key) from ce_exam_score where exs_cst_key ='" + GetValue("cr3_cst_key") + "' and exs_raw_score is not null";
                    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
                    int attempts = Convert.ToInt16(oDS.Tables[0].Rows[0]["attempts"]);

                    SetValue("cr3_exam_attempts", attempts.ToString());

                    if (attempts >= attemptLimit)
                        SetStatus(!UtilityFunctions.EmptyString(GetValue("ct3_cr3_denied_status")) ? GetValue("ct3_cr3_denied_status") : "Max Exams", oConn, oTrans);
                }
            }

            GetDataObject("cr3").Update(oConn, oTrans);

			return errorClass;
		}

        public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
        {
            ErrorClass errorClass = new ErrorClass();
            try
            {
                base.SelectByKey(oConn, oTrans);

                if (!UtilityFunctions.EmptyString(_oCredentialRequirementData.GetValue("exs_cex_key")))
                {
                    _oExam.CurrentKey = _oCredentialRequirementData.GetValue("exs_cex_key");
                    _oExam.SelectByKey(oConn, oTrans);
                }
            }
            catch (Exception e)
            {
                errorClass.Number = -1;
                errorClass.Message = e.Message;
            }
            finally
            {
                if (errorClass.Number != 0)
                {
                    Config.LastError = errorClass;
                }
            }

        }

        public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
        {
            ErrorClass errorClass = new ErrorClass();
            try
            {
                base.LoadRelatedData(oConn, oTrans);

                if (_oExam.GetValue("cex_key") != _oCredentialRequirementData.GetValue("exs_cex_key"))
                {
                    _oExam.CurrentKey = _oCredentialRequirementData.GetValue("exs_cex_key");

                    if (UtilityFunctions.EmptyString(_oExam.CurrentKey))
                    {
                        _oExam.ClearValues();
                    }
                    else
                    {
                        _oExam.SelectByKey(oConn, oTrans);
                    }
                }
            }
            catch (Exception e)
            {
                errorClass.Number = -1;
                errorClass.Message = e.Message;
            }
            finally
            {
                if (errorClass.Number != 0)
                {
                    Config.LastError = errorClass;
                }
            }
        }

        public static void CheckForExamRequirements(FacadeClass oExamScore, NfDbConnection oConn, NfDbTransaction oTrans)
        {
            string strSQL = "exec ags_check_for_exam_score_requirement @ExamScoreKey='" + oExamScore.GetValue("exs_key") + "'";

            DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

            if (oDT.Rows.Count > 0)
            {
                string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();
                string strExamMetDate = DateTime.Now.ToString();

                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
                oRequirement.SetValue("cr3_data_key", oExamScore.GetValue("exs_key"));
                oRequirement.Update(oConn, oTrans);
            }
        }

        public static void CheckForExamRequirementFromRegistration(FacadeClass oRegistration, NfDbConnection oConn, NfDbTransaction oTrans)
        {
            string strSQL1 = "select cr3_key"
                + " from ags_credential_requirement (nolock)"
                + " where cr3_data_key = '" + oRegistration.GetValue("reg_key") + "'"
                + " and cr3_ct3_requirement like '%Exam%'";

            DataTable oDT1 = DataUtils.GetDataSet(strSQL1, oConn, oTrans).Tables[0];

            if (oDT1.Rows.Count > 0)
            {
                string strRequirementKey = oDT1.Rows[0]["cr3_key"].ToString();

                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
                oRequirement.Update(oConn, oTrans);

                return;
            }

            string strSQL2 = "select cr3_key"
                + " from ev_registrant (nolock)"
                + " join ev_event (nolock) on reg_evt_key = evt_key"
                + " join ev_event_ext (nolock) on evt_key = evt_key_ext"
                + " join ce_exam (nolock) on evt_cex_key_ext = cex_key"
                + " join ags_credential_requirement_type (nolock) on cex_key = ct3_cex_key"
                + " join ags_credential_requirement (nolock) on ct3_requirement = cr3_ct3_requirement"
                + " where reg_key = '" + oRegistration.GetValue("reg_key") + "'"
                + " and cr3_cst_key = '" + oRegistration.GetValue("reg_cst_key") + "'"
                + " and cr3_met_date is null";

            DataTable oDT2 = DataUtils.GetDataSet(strSQL2, oConn, oTrans).Tables[0];

            if (oDT2.Rows.Count > 0)
            {
                string strRequirementKey = oDT2.Rows[0]["cr3_key"].ToString();

                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
                oRequirement.CurrentKey = strRequirementKey;
                oRequirement.SelectByKey(oConn, oTrans);
                oRequirement.SetValue("cr3_data_key", oRegistration.GetValue("reg_key"));
                oRequirement.LoadRelatedData(oConn, oTrans);
                oRequirement.Update(oConn, oTrans);

                return;
            }
        }

        public static void CheckForExamRequirementFromExamScore(FacadeClass oExamScore, NfDbConnection oConn, NfDbTransaction oTrans)
        {
            string strSQL = "exec ags_check_for_exam_score_requirement @exs_key='" + oExamScore.GetValue("exs_key") + "'";

            DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

            if (oDT.Rows.Count > 0)
            {
                string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();

                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
                oRequirement.SetValue("cr3_data_key", oExamScore.GetValue("exs_key"));
                oRequirement.LoadRelatedData(oConn, oTrans);
                oRequirement.Update(oConn, oTrans);
            }
        }
    }
}
