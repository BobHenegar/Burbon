using System;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    public class RequirementUtils
    {
    //    public static string GetCEProgramType(string strCEProgramTypeKey)
    //    {
    //        string strSQL = "select ctt_code from ce_course_type (nolock)"
    //                + " where ctt_key = '" + strCEProgramTypeKey + "'"
    //                + " and ctt_delete_flag = 0";

    //        DataSet oDS = DataUtils.GetDataSet(strSQL);

    //        string strCEProgramType = null;

    //        if (oDS.Tables[0].Rows.Count == 1)
    //        {
    //            strCEProgramType = oDS.Tables[0].Rows[0]["ctt_code"].ToString();
    //        }

    //        else
    //        {
    //            throw new Exception("Invalid CE Program Type");
    //        }

    //        return strCEProgramType;
    //    }

    //    public static string GetCEType(string strCETypeKey)
    //    {
    //        string strSQL = "select cet_type from ce_ceu_type (nolock)"
    //                + " where cet_key = '" + strCETypeKey + "'"
    //                + " and cet_delete_flag = 0";

    //        DataSet oDS = DataUtils.GetDataSet(strSQL);

    //        string strCEType = null;

    //        if (oDS.Tables[0].Rows.Count == 1)
    //        {
    //            strCEType = oDS.Tables[0].Rows[0]["cet_type"].ToString();
    //        }

    //        else
    //        {
    //            throw new Exception("Invalid CE Type");
    //        }

    //        return strCEType;
    //    }

    //    public static void UpdateFeeRequirement(PageClass oPage)
    //    {
    //        NfDbConnection oConn = DataUtils.GetConnection();
    //        NfDbTransaction oTrans = oConn.BeginTransaction();

    //        if ((UtilityFunctions.EmptyString(oPage.oFacadeObject.GetValue("inv_code")))
    //                   && (!UtilityFunctions.EmptyString(oPage.oFacadeObject.GetValue("pay_code"))))
    //        {
    //            string strSQL = "select distinct inv_code"
    //                + " from ac_payment (nolock)"
    //                + " join ac_payment_detail (nolock) on pyd_pay_key = pay_key"
    //                + " join ac_invoice_detail (nolock) on pyd_ivd_key = ivd_key"
    //                + " join ac_invoice (nolock) on inv_key = ivd_inv_key"
    //                + " where pay_code = '" + oPage.oFacadeObject.GetValue("pay_code") + "'"
    //                + " and inv_delete_flag = 0";

    //            DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

    //            for (int i = 0; i < oDS.Tables[0].Rows.Count; i++)
    //            {
    //                UpdateFeeRequirementByInvCode(oDS.Tables[0].Rows[i]["inv_code"].ToString(), oConn, oTrans);
    //            }

    //        }
    //        else
    //        {
    //            UpdateFeeRequirementByInvCode(oPage.oFacadeObject.GetValue("inv_code"), oConn, oTrans);
    //        }

    //        oTrans.Commit();
    //        oConn.Close();

    //        CommonUtils.ClearValues(oPage);
    //    }

    //    public static void UpdateFeeRequirementByInvCode(string strInvCode, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        if (!UtilityFunctions.EmptyString(strInvCode))
    //        {
    //            string strSQL = "select cr3_key"
    //                + " from ags_credential_requirement (nolock)"
    //                + " join ac_invoice (nolock) on cr3_data_key = inv_key"
    //                + " where inv_code = '" + strInvCode + "'"
    //                + " and inv_delete_flag = 0"
    //                + " and cr3_delete_flag = 0";


    //            DataSet oDS = DataUtils.GetDataSet(strSQL);

    //            for (int i = 0; i < oDS.Tables[0].Rows.Count; i++)
    //            {
    //                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(oDS.Tables[0].Rows[i]["cr3_key"].ToString(), oConn, oTrans);

    //                oRequirement.Update();
    //            }
    //        }

    //    }

    //    public static void UnsetRegistrationAttendanceFlag(string strRegistrationKey, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        DynamicData oEventRegistrant = new DynamicData("ev_registrant", "ev_registrant_ext", "reg", "reg", "reg_key", "reg_key_ext", "reg_delete_flag", "Registrant");
    //        oEventRegistrant.CurrentKey = strRegistrationKey;
    //        oEventRegistrant.SelectByKey(oConn, oTrans);
    //        oEventRegistrant.SetValue("reg_attendance_flag", "0");
    //        oEventRegistrant.Update(oConn, oTrans);
    //    }

    //    public static void UnsetRegistrationAttendanceFlag(string strRegistrationKey)
    //    {
    //        NfDbConnection oConn = DataUtils.GetConnection();
    //        NfDbTransaction oTrans = oConn.BeginTransaction();

    //        try
    //        {
    //            UnsetRegistrationAttendanceFlag(strRegistrationKey, oConn, oTrans);
    //        }
    //        catch (Exception ex)
    //        {
    //            oTrans.Rollback();
    //            throw ex;
    //        }
    //        finally
    //        {
    //            oTrans.Commit();
    //            oConn.Close();
    //        }
    //    }

    //    public static void CreateCredentialForEvent(string strEventKey, string strCustomerKey)
    //    {
    //        NfDbConnection oConn = DataUtils.GetConnection();
    //        NfDbTransaction oTrans = oConn.BeginTransaction();

    //        Credential oEventCredential = null;

    //        try
    //        {
    //            DynamicData oEventType = new DynamicData("ev_event", "ev_event_ext", "evt", "evt", "evt_key", "evt_key_ext", "evt_delete_flag", "Event");
    //            oEventType.CurrentKey = strEventKey;
    //            oEventType.SelectByKey(oConn, oTrans);

    //            if (!UtilityFunctions.EmptyString(oEventType.GetValue("evt_ct2_key_ext")))
    //            {
    //                string strEventProcessTypeKey = oEventType.GetValue("evt_ct2_key_ext");

    //                DynamicData oProcessType = new DynamicData("ags_credential_process_type", null, "ct2", null, "ct2_key", null, "ct2_delete_flag", "Process");
    //                oProcessType.CurrentKey = strEventProcessTypeKey;
    //                oProcessType.SelectByKey(oConn, oTrans);

    //                string strEventProcessType = oProcessType.GetValue("ct2_process");
    //                string strEventCredentialType = oProcessType.GetValue("ct2_ct1_credential");

    //                oEventCredential = CredentialingUtils.CreateCredential(strEventCredentialType, strEventProcessType, strCustomerKey);

    //                if (oEventCredential == null)
    //                {
    //                    throw new Exception("Could not create Event Credential");
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            oTrans.Rollback();
    //            throw ex;
    //        }
    //        finally
    //        {
    //            oTrans.Commit();
    //            oConn.Close();
    //        }
    //    }

    //    public static ErrorClass GetPriorCEPeriod(string strCustomerKey, string strCredentialKey, string strCurrentProcessKey, ref CEPeriod oPriorCEPeriod, ref bool bWasOnExtension, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        ErrorClass errorClass = new ErrorClass();

    //        string strSQL = "select top 1 ce2_key, cr2_extension_date"
    //            + " from ags_credential (nolock)"
    //            + " join ags_credential_process (nolock) on cr1_key = cr2_cr1_key"
    //            + " join ags_credential_requirement (nolock) on cr2_key = cr3_cr2_key and cr3_ct3_requirement in ('ReCertCE', 'ReinstateCE', 'ReCertCE(2009)', 'ReinstateCE(2009)')"
    //            + " join ags_ce_period (nolock) on cr3_data_key = ce2_key"
    //            + " where cr1_cst_key = '" + strCustomerKey + "'"
    //            + " and cr1_key = '" + strCredentialKey + "'"
    //            + " and cr2_key <> '" + strCurrentProcessKey + "'"
    //            + " order by cr2_due_date desc";

    //        DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

    //        if (oDS.Tables[0].Rows.Count != 1)
    //        {
    //            errorClass.Number = -1;
    //            errorClass.Message = "Could not locate previous ce period";
    //            return errorClass;
    //        }

    //        oPriorCEPeriod.CurrentKey = oDS.Tables[0].Rows[0]["ce2_key"].ToString();
    //        oPriorCEPeriod.SelectByKey();

    //        bWasOnExtension = false;

    //        if (!UtilityFunctions.EmptyString(oDS.Tables[0].Rows[0]["cr2_extension_date"].ToString()))
    //        {
    //            bWasOnExtension = true;
    //        }

    //        return errorClass;
    //    }

    //    public static void UpdateCERequirement(string strCustomerKey, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        string strSQL = "SELECT cr3_key"
    //            + " FROM ags_credential_requirement (NOLOCK)"
    //            + " WHERE cr3_ct3_requirement = 'Continuing Education'"
    //            + " AND cr3_met_date IS NULL"
    //            + " AND cr3_cst_key = '" + strCustomerKey + "'";

    //        DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

    //        if(oDS.Tables[0].Rows.Count > 0)
    //        {
    //            for(int i=0; i<oDS.Tables[0].Rows.Count; i++)
    //            {
    //                CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(oDS.Tables[0].Rows[i]["cr3_key"].ToString(), oConn, oTrans);
    //                oRequirement.Update(oConn, oTrans);
    //            }
    //        }
    //    }

    //    public static void CheckForCERequirement(FacadeClass oCredit, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        string strCEType = string.Empty;
    //        string strCstKey = string.Empty;
    //        string strCEDate = string.Empty;

    //        if (oCredit.FieldExists("ceu_key"))
    //        {
    //            strCEType = "CEU";
    //            strCstKey = oCredit.GetValue("ceu_ind_cst_key");
    //            strCEDate = oCredit.GetValue("ceu_earned_date");
    //        }
    //        else if (oCredit.FieldExists("sce_key"))
    //        {
    //            strCEType = "SelfReported";
    //            strCstKey = oCredit.GetValue("sce_ind_cst_key");
    //            strCEDate = oCredit.GetValue("sce_activity_date");
    //        }
    //        else
    //        {
    //            return;
    //        }

    //        string strSQL = "SELECT ce2_key, cr3_key"
    //            + " FROM ags_credential_requirement (NOLOCK)"
    //            + " JOIN ags_credential_requirement_type (NOLOCK) on cr3_ct3_requirement = ct3_requirement"
    //            + " JOIN ags_ce_period (NOLOCK) ON cr3_data_key = ce2_key"
    //            + " WHERE ct3_class_name = 'AGSCredentialing.CredentialCERequirement'"
    //            + " AND cr3_met_date IS NULL"
    //            + " AND cr3_cst_key ='" + strCstKey + "'"
    //            + " AND cr3_delete_flag = 0"
    //            + " AND (ce2_start_date IS NULL OR ce2_start_date <='" + strCEDate + "')"
    //            + " AND (ce2_end_date IS NULL OR ce2_end_date >='" + strCEDate + "')"
    //            + " ORDER BY cr3_add_date DESC";

    //        DataSet oDs = DataUtils.GetDataSet(strSQL, oConn, oTrans);

    //        if (oDs.Tables[0].Rows.Count > 0)
    //        {
    //            string strCEPeriodKey = oDs.Tables[0].Rows[0]["ce2_key"].ToString();
    //            string strCERequirementKey = oDs.Tables[0].Rows[0]["cr3_key"].ToString();

    //            if (strCEType == "SelfReported")
    //            {
    //                oCredit.SetValue("sce_ce2_key_ext", strCEPeriodKey);
    //            }
    //            else if (strCEType == "CEU")
    //            {
    //                oCredit.SetValue("ceu_ce2_key_ext", strCEPeriodKey);
    //            }
    //            else
    //            {
    //                return;
    //            }

    //            oCredit.Update(oConn, oTrans);

    //            CERequirement oCERequirement = (CERequirement)DataUtils.InstantiateFacadeObject("CEUCredit");
    //            oCERequirement.CurrentKey = strCERequirementKey;
    //            oCERequirement.SelectByKey(oConn, oTrans);

    //            if (UtilityFunctions.EmptyString(oCERequirement.GetValue("cr3_met_date")))
    //            {
    //                oCERequirement.Update(oConn, oTrans);
    //            }
    //        }
    //    }

    //    public static void CreateExamCECredit(FacadeClass oExamParticipant, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        if (oExamParticipant.GetValue("exs_success_flag") == "1" && !UtilityFunctions.EmptyString(oExamParticipant.GetValue("cex_hours")))
    //        {
    //            DynamicFacade oCECredit = (DynamicFacade)DataUtils.InstantiateFacadeObject("CEUCredit");

    //            oCECredit.SetValue("ceu_ind_cst_key", oExamParticipant.GetValue("exs_cst_key"));
    //            oCECredit.SetValue("ceu_cet_key", oExamParticipant.GetValue("cex_cet_key"));
    //            oCECredit.SetValue("ceu_credit", oExamParticipant.GetValue("cex_hours"));
    //            oCECredit.SetValue("ceu_earned_date", oExamParticipant.GetValue("exs_exam_date"));

    //            oCECredit.Insert(oConn, oTrans);
    //        }
    //    }

    //    public static void CreateEventCECredit(FacadeClass oEventRegistrant, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        if (oEventRegistrant.GetValue("reg_exam_result").ToUpper() == "PASS")
    //        {
    //            string strSQL = "select ece_cet_key, ece_credit"
    //                + " from ev_event_credit (nolock)"
    //                + " where ece_evt_key= '" + oEventRegistrant.GetValue("reg_evt_key") + "'";

    //            DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

    //            if (oDS.Tables[0].Rows.Count > 0)
    //            {
    //                DynamicFacade oCECredit = (DynamicFacade)DataUtils.InstantiateFacadeObject("CEUCredit");

    //                oCECredit.SetValue("ceu_ind_cst_key", oEventRegistrant.GetValue("reg_cst_key"));
    //                oCECredit.SetValue("ceu_cet_key", oDS.Tables[0].Rows[0]["ece_cet_key"].ToString());
    //                oCECredit.SetValue("ceu_credit", oDS.Tables[0].Rows[0]["ece_credit"].ToString());
    //                oCECredit.SetValue("ceu_earned_date", oEventRegistrant.GetValue("evt_end_date"));

    //                oCECredit.Insert(oConn, oTrans);
    //            }
    //        }
    //    }

    //    public static void GenerateFeeInvoice(PageClass oPage, string strFeeRequirementKey)
    //    {
    //        if (oPage.bIsFirstTime == true && !UtilityFunctions.EmptyString(strFeeRequirementKey))
    //        {
    //            NfDbConnection oConn = DataUtils.GetConnection();
    //            NfDbTransaction oTrans = oConn.BeginTransaction();

    //            FeeRequirement oFee = (FeeRequirement)DataUtils.InstantiateFacadeObject("AGSFeeRequirement");
    //            oFee.CurrentKey = strFeeRequirementKey;
    //            oFee.SelectByKey(oConn, oTrans);

    //            if (UtilityFunctions.EmptyString(oFee.GetValue("cr3_data_key")))
    //            {
    //                oFee.GenerateFeeInvoice(oConn, oTrans);
    //                oFee.Update(oConn, oTrans);
    //            }

    //            oTrans.Commit();
    //            oConn.Close();

    //            ((System.Web.UI.WebControls.TextBox)oPage.FindControl("inv_code")).Text = oFee.GetValue("inv_code");
    //        }
    //    }

    //    public static void GenerateFeeInvoice(PageClass oPage, string strProcessKey, string strFeeRequirementType)
    //    {
    //        if (oPage.bIsFirstTime == true && !UtilityFunctions.EmptyString(strProcessKey) && !UtilityFunctions.EmptyString(strFeeRequirementType))
    //        {
    //            NfDbConnection oConn = DataUtils.GetConnection();
    //            NfDbTransaction oTrans = oConn.BeginTransaction();

    //            CredentialProcess oProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
    //            oProcess.CurrentKey = strProcessKey;
    //            oProcess.SelectByKey(oConn, oTrans);

    //            string strFeeRequirementKey = oProcess.GetCredentialRequirementKey(strFeeRequirementType, oConn, oTrans);

    //            oTrans.Commit();
    //            oConn.Close();

    //            GenerateFeeInvoice(oPage, strFeeRequirementKey);
    //        }
    //    }

    //    public static void CreateExamScoreFromEvent(FacadeClass oRegistrant, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        if (!UtilityFunctions.EmptyString(oRegistrant.GetValue("evt_cex_key_ext")))
    //        {
    //            string strExamKey = oRegistrant.GetValue("evt_cex_key_ext");

    //            DynamicFacade oExamScore = (DynamicFacade)DataUtils.InstantiateFacadeObject("ExamScore");
    //            oExamScore.SetValue("exs_cex_key", oRegistrant.GetValue("evt_cex_key_ext"));
    //            oExamScore.SetValue("exs_cst_key", oRegistrant.GetValue("reg_cst_key"));
    //            oExamScore.SetValue("exs_cst_key", oRegistrant.GetValue("reg_cst_key"));
    //            oExamScore.SetValue("exs_reg_key_ext", oRegistrant.GetValue("reg_key"));
    //            oExamScore.Insert(oConn, oTrans);

    //            string strSQL = "SELECT TOP 1 cr3_key"
				//	+ " FROM ags_credential_requirement (NOLOCK)"
    //                + " WHERE cr3_cst_key = '" + oRegistrant.GetValue("reg_cst_key") + "'"
    //                + " AND cr3_ct3_requirement LIKE '%Exam%'"
    //                + " AND cr3_data_key IS NULL"
				//	+ " AND cr3_met_date IS NULL"
				//	+ " ORDER BY cr3_add_date desc";

				//DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

    //            if (oDT.Rows.Count == 1)
    //            {
    //                CredentialRequirement oExamRequirement = (CredentialRequirement)DataUtils.InstantiateFacadeObject("AGSCredentialRequirement");
    //                oExamRequirement.CurrentKey = oDT.Rows[0]["cr3_key"].ToString();
    //                oExamRequirement.SelectByKey(oConn, oTrans);
    //                oExamRequirement.SetValue("cr3_data_key", oExamScore.GetValue("exs_key"));
    //                oExamRequirement.Update(oConn, oTrans);
    //            }

    //            oRegistrant.SetValue("reg_exs_key_ext", oExamScore.GetValue("exs_key"));
    //            oRegistrant.Update(oConn, oTrans);
    //        }
    //    }

    //    public static void CreateCourseParticipantFromEvent(FacadeClass oRegistrant, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        string strSQL = "select cxv_cec_key from ce_course_x_event (nolock) where cxv_evt_key = '" + oRegistrant.GetValue("reg_evt_key") + "'";
    //        DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
    //        if(oDS.Tables[0].Rows.Count > 0)
    //        {
    //            string strCourseKey = oDS.Tables[0].Rows[0]["cxv_cec_key"].ToString();

    //            FacadeClass oCourseParticipant = (FacadeClass)DataUtils.InstantiateFacadeObject("CourseParticipant");
    //            oCourseParticipant.SetValue("cup_cst_key", oRegistrant.GetValue("reg_cst_key"));
    //            oCourseParticipant.SetValue("cup_cec_key", strCourseKey);
    //            oCourseParticipant.Insert(oConn, oTrans);

    //            strSQL = "SELECT cr3_key"
    //                + " FROM ags_credential_requirement (NOLOCK)"
    //                + " JOIN ags_credential_requirement_type (NOLOCK) ON cr3_ct3_requirement = ct3_requirement"
    //                + " WHERE cr3_cst_key = '" + oRegistrant.GetValue("reg_cst_key") + "'"
    //                + " AND ct3_class_name = 'CredentialCourseRequirement'"
    //                + " AND cr3_data_key IS NULL"
    //                + " AND cr3_met_date IS NULL"
    //                + " ORDER BY cr3_add_date desc";

    //            DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

    //            if (oDT.Rows.Count == 1)
    //            {
    //                CredentialRequirement oCourseRequirement = (CredentialRequirement)DataUtils.InstantiateFacadeObject("AGSCredentialRequirement");
    //                oCourseRequirement.CurrentKey = oDT.Rows[0]["cr3_key"].ToString();
    //                oCourseRequirement.SelectByKey(oConn, oTrans);
    //                oCourseRequirement.Update(oConn, oTrans);
    //            }
    //        }
    //    }

    //    public static void CheckForSQLMentorRequirement(FacadeClass oRelationship, NfDbConnection oConn, NfDbTransaction oTrans)
    //    {
    //        string strSQL = "exec ags_check_for_sql_mentor_requirement @cxc_key='" + oRelationship.GetValue("cxc_key") + "'";

    //        DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

    //        if (oDT.Rows.Count > 0)
    //        {
    //            string strRequirementKey = oDT.Rows[0]["cr3_key"].ToString();

    //            CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
    //            oRequirement.SetValue("cr3_data_key", oRelationship.GetValue("cxc_key"));
    //            oRequirement.Update(oConn, oTrans);
    //        }
    //    }

    //    public static void SubmitApplication(PageClass oPage)
    //    {
    //        NfDbConnection oConn = DataUtils.GetConnection();
    //        NfDbTransaction oTrans = oConn.BeginTransaction();

    //        oPage.oFacadeObject.SetValue("cr2_status", CredentialingUtils.GetStatusByState(oPage.oFacadeObject.GetValue("ct2_key"), "UNDER REVIEW"));
    //        oPage.oFacadeObject.SetValue("cr2_submit_date", DateTime.Now.ToString());
    //        oPage.oFacadeObject.GetDataObject("cr2").Update(oConn, oTrans);

    //        string strSQL = "exec ags_check_for_application_requirement @cr2_key='" + oPage.oFacadeObject.GetValue("cr2_key") + "'";

    //        DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

    //        if (oDT.Rows.Count > 0)
    //        {
    //            CredentialRequirement oApplicationRequirement = (CredentialRequirement)DataUtils.InstantiateFacadeObject("AGSCredentialRequirement");
    //            oApplicationRequirement.CurrentKey = oDT.Rows[0]["cr3_key"].ToString();
    //            oApplicationRequirement.SelectByKey(oConn, oTrans);
    //            oApplicationRequirement.Update(oConn, oTrans);
    //        }

    //        oTrans.Commit();
    //        oConn.Close();
    //    }

    //    public static void AfterExperienceDetailSave(FacadeClass experienceDetail, NfDbConnection conn, NfDbTransaction trans) {
    //        string ex2Key = experienceDetail.GetValue("ex1_ex2_key");
            
    //        string sql = string.Format("select cr3_key from ags_credential_requirement where cr3_data_key = '{0}'", ex2Key);


    //            string cr3Key = DataUtils.ExecuteScalar(sql, conn, trans).ToString();


    //            ExperienceRequirement er = (ExperienceRequirement)DataUtils.InstantiateFacadeObject("AGSExperienceRequirement");
    //            er.SelectByKey(cr3Key);
    //            er.Update(conn, trans);


    //        }

    }
}
