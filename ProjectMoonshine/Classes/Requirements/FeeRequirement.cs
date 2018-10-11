using System;
using System.Data;
using ags.credentialing;

namespace ags.requirements
{
    public class FeeRequirement : CredentialRequirement
    {
		public FeeRequirement()
		{
			_oCredentialRequirementData = new DynamicData("ac_invoice", "ac_invoice_ext", "inv", "inv", "inv_key", "inv_key_ext", "inv_delete_flag", "FeeInvoice", null, null, null, null);
			AddDataObject("inv", _oCredentialRequirementData, "FeeInvoice");
		}

		public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
		{
			ErrorClass oErr = new ErrorClass();

            if (GetValue("ct3_generate_detail_flag") == "1" || (Config.IsIweb == true && GetValue("ct3_iweb_generate_invoice_flag") == "1"))
            {
                oErr = GenerateFeeInvoice(oConn, oTrans);

                oErr = Eval(oConn, oTrans);
            }

			return oErr;
		}
		
		public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		{
			ErrorClass errorClass = new ErrorClass();

			if (!UtilityFunctions.EmptyString(GetValue("inv_close_flag")) && GetValue("inv_close_flag") == "1")            
			{
				SetValue("cr3_met_date", System.DateTime.Now.ToString());
				SetValue("cr3_met_user", Config.CurrentUserName);

                _oData.Update(oConn, oTrans);
			}

			return errorClass;
		}

        public ErrorClass GenerateFeeInvoice(NfDbConnection oConn, NfDbTransaction oTrans)
        {
            ErrorClass oErr = new ErrorClass();

            if (!UtilityFunctions.EmptyString(GetValue("ct3_prd_key")) && UtilityFunctions.EmptyString(GetValue("cr3_data_key")))
            {
                FacadeClass oCustomer = null;

                if (GetValue("cst_type") == "Individual")
                {
                    oCustomer = (CO_Individual)DataUtils.InstantiateFacadeObject("Individual");
                }
                else if (GetValue("cst_type") == "Organization")
                {
                    oCustomer = (CO_Organization)DataUtils.InstantiateFacadeObject("Organization");
                }
                else
                {
                    return oErr;
                }

                oCustomer.CurrentKey = GetValue("cr3_cst_key");
                oCustomer.SelectByKey(oConn, oTrans);

                // Get Proforma Batch Key
                string strBatchKey = null;

                if(Config.GetSystemOption("AGSProformaBatchKey") != null)
                {
                    strBatchKey = Config.GetSystemOption("AGSProformaBatchKey");
                }
                else
                {
                    string strBatchSQL = "exec ags_get_application_proforma_batch";
                    DataSet oBatchDS = DataUtils.GetDataSet(strBatchSQL, oConn, oTrans);
                    if (oBatchDS.Tables[0].Rows.Count > 0)
                    {
                        strBatchKey = oBatchDS.Tables[0].Rows[0]["bat_key"].ToString();
                    }
                }

                if(strBatchKey == null)
                {
                    oErr.Number = -1;
                    oErr.Message = "Could not find Applicatcion Proforma Batch";
                    return oErr;
                }

                // Get Invoice Terms Key
                string strTermsSQL = "exec ags_get_application_invoice_terms";
                string strTermsKey = null;
                DataSet oTermsDS = DataUtils.GetDataSet(strTermsSQL, oConn, oTrans);
                if (oTermsDS.Tables[0].Rows.Count > 0)
                {
                    strTermsKey = oTermsDS.Tables[0].Rows[0]["ait_key"].ToString();
                }
                else
                {
                    oErr.Number = -1;
                    oErr.Message = "Could not find Application Invoice Terms";
                    return oErr;
                }

                Invoice oInvoice = null;

                // Get Application Fee Price Key
                string strPriceSQL = "exec sc_merchandise_list"
                    + " @cst_key = '" + GetValue("cr3_cst_key") + "',"
                    + " @eweb = 0,"
                    + " @prd_key = '" + GetValue("ct3_prd_key") + "'";

                string strPriceKey = null;
                DataSet oPriceDS = DataUtils.GetDataSet(strPriceSQL, oConn, oTrans);
                if (oPriceDS.Tables[0].Rows.Count > 0)
                {
                    strPriceKey = oPriceDS.Tables[0].Rows[0]["prc_key"].ToString();
                }
                else
                {
                    oErr.Number = -1;
                    oErr.Message = "Could not find Application Price Key";
                    return oErr;
                }

                oInvoice = AccountingUtils.CreateInvoice(oCustomer, strBatchKey, strTermsKey);
                InvoiceDetail oInvoiceDetail = AccountingUtils.AddInvoiceDetail(oInvoice, strPriceKey);

                oErr = oInvoice.Insert(oConn, oTrans);

                SetValue("cr3_start_date", System.DateTime.Now.ToString());
                SetValue("cr3_due_date", System.DateTime.Now.AddDays(30).ToString());
                SetValue("cr3_data_key", oInvoice.GetValue("inv_key"));

                Update(oConn, oTrans);
                    
                //for eWeb, save invoice key to session to session
                Config.Session["application_inv_key"] = oInvoice.GetValue("inv_key");

                if (oInvoice.GetInvoiceAmount(oConn, oTrans) == 0)
                {
                    SetValue("cr3_met_date", System.DateTime.Now.ToString());
                    SetValue("cr3_met_user", Config.CurrentUserName);
                }
            }

            return oErr;
        }

        #region Deprecated methods. Now using CredentialingUtils.CheckForRequirements();
        //public static void CheckForFeeRequirement(FacadeClass oFacade, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    Invoice oInvoice = (Invoice)oFacade;
        //    FacadeDetails oInvoiceDetails = oInvoice.InvoiceDetails;

        //    foreach (InvoiceDetail oInvoiceDetail in oInvoiceDetails)
        //    {
        //        string strInvoiceKey = oInvoiceDetail.GetValue("ivd_inv_key");

        //        //string strSQL = "SELECT cr3_key"
        //        //    + " FROM ags_credential_requirement (NOLOCK)"
        //        //    + " JOIN ags_credential_requirement_type (NOLOCK) on cr3_ct3_requirement = ct3_requirement"
        //        //    + " WHERE cr3_data_key =  '" + strInvoiceKey + "'"
        //        //    + " AND ct3_class_name = 'AGSCredentialing.CredentialFeeRequirement'"
        //        //    + " AND cr3_delete_flag = 0";
                
        //        string strSQL = "SELECT cr3_key"
        //            + " FROM ags_credential_requirement (NOLOCK)"
        //            + " JOIN ags_credential_requirement_type (NOLOCK) ON cr3_ct3_requirement = ct3_requirement AND ct3_delete_flag = 0"
        //            + " JOIN ags_credential_requirement_template (NOLOCK) ON ct3_ct8_key = ct8_key AND ct8_delete_flag = 0"
        //            + " WHERE cr3_data_key =  '" + strInvoiceKey + "'"
        //            + " AND UPPER(ct8_template) = 'FEE'"
        //            + " AND cr3_delete_flag = 0";

        //        DataSet oDs = DataUtils.GetDataSet(strSQL, oConn, oTrans);
                
        //        if(oDs.Tables[0].Rows.Count > 0)
        //        {
        //            string strFeeRequirementKey = oDs.Tables[0].Rows[0]["cr3_key"].ToString();
					
        //            FeeRequirement oFeeRequirement = new FeeRequirement();
        //            oFeeRequirement.CurrentKey = strFeeRequirementKey;
        //            oFeeRequirement.SelectByKey(oConn, oTrans);

        //            if(UtilityFunctions.EmptyString(oFeeRequirement.GetValue("cr3_met_date")))
        //            {
        //                oFeeRequirement.Update(oConn, oTrans);
        //                return;
        //            }
        //        }

        //        string strProductKey = oInvoiceDetail.GetValue("ivd_prc_prd_key");

        //        //string strSQL2 = "SELECT cr3_key"
        //        //    + " FROM ags_credential_requirement (NOLOCK)"
        //        //    + " JOIN ags_credential_requirement_type (NOLOCK) on cr3_ct3_requirement = ct3_requirement"
        //        //    + " WHERE ct3_prd_key = '" + strProductKey + "'"
        //        //    + " AND cr3_data_key IS NULL"
        //        //    + " AND cr3_met_date IS NULL"
        //        //    + " AND ct3_class_name = 'AGSCredentialing.CredentialFeeRequirement'"
        //        //    + " AND cr3_cst_key = '" + oInvoiceDetail.GetValue("inv_cst_key") + "'"
        //        //    + " AND cr3_delete_flag = 0";

        //        string strSQL2 = "SELECT cr3_key"
        //            + " FROM ags_credential_requirement (NOLOCK)"
        //            + " JOIN ags_credential_requirement_type (NOLOCK) ON cr3_ct3_requirement = ct3_requirement AND ct3_delete_flag = 0"
        //            + " JOIN ags_credential_requirement_template (NOLOCK) ON ct3_ct8_key = ct8_key AND ct8_delete_flag = 0"
        //            + " WHERE ct3_prd_key = '" + strProductKey + "'"
        //            + " AND cr3_data_key IS NULL"
        //            + " AND cr3_met_date IS NULL"
        //            + " AND UPPER(ct8_template) = 'FEE'"
        //            + " AND cr3_cst_key = '" + oInvoiceDetail.GetValue("inv_cst_key") + "'"
        //            + " AND cr3_delete_flag = 0";

        //        DataSet oDs2 = DataUtils.GetDataSet(strSQL2, oConn, oTrans);

        //        if (oDs2.Tables[0].Rows.Count > 0)
        //        {
        //            string strFeeRequirementKey = oDs2.Tables[0].Rows[0]["cr3_key"].ToString();

        //            FeeRequirement oFeeRequirement = new FeeRequirement();
        //            oFeeRequirement.CurrentKey = strFeeRequirementKey;
        //            oFeeRequirement.SelectByKey(oConn, oTrans);

        //            if (UtilityFunctions.EmptyString(oFeeRequirement.GetValue("cr3_met_date")))
        //            {
        //                oFeeRequirement.SetValue("cr3_data_key", strInvoiceKey);
        //                oFeeRequirement.LoadRelatedData(oConn, oTrans);
        //                oFeeRequirement.Update(oConn, oTrans);
        //                return;
        //            }
        //        }            
        //    }
        //}

        //public static void CheckForFeeRequirementPayment(PageClass oPage)
        //{
        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    CheckForFeeRequirementPayment(oPage.oFacadeObject, oConn, oTrans);

        //    oTrans.Commit();
        //    oConn.Close();
        //}

        //public static void CheckForFeeRequirementPayment(FacadeClass oFacade, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    Payment oPayment = (Payment)oFacade;
        //    FacadeDetails oPaymentDetails = oPayment.PaymentDetails;

        //    foreach (PaymentDetail oPaymentDetail in oPaymentDetails)
        //    {
        //        InvoiceDetail oInvoiceDetail = new InvoiceDetail();
        //        oInvoiceDetail.CurrentKey = oPaymentDetail.GetValue("pyd_ivd_key");
        //        oInvoiceDetail.SelectByKey(oConn, oTrans);

        //        string strInvoiceKey = oInvoiceDetail.GetValue("ivd_inv_key");

        //        string strSQL = "SELECT cr3_key"
        //            + " FROM ags_credential_requirement (NOLOCK)"
        //            + " JOIN ags_credential_requirement_type (NOLOCK) on cr3_ct3_requirement = ct3_requirement"
        //            + " WHERE cr3_data_key =  '" + strInvoiceKey + "'"
        //            + " AND cr3_delete_flag = 0";
        //        DataSet oDs = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //        if (oDs.Tables[0].Rows.Count > 0)
        //        {
        //            string strFeeRequirementKey = oDs.Tables[0].Rows[0]["cr3_key"].ToString();

        //            FeeRequirement oFeeRequirement = new FeeRequirement();
        //            oFeeRequirement.CurrentKey = strFeeRequirementKey;
        //            oFeeRequirement.SelectByKey(oConn, oTrans);

        //            if (UtilityFunctions.EmptyString(oFeeRequirement.GetValue("cr3_met_date")))
        //            {
        //                oFeeRequirement.Update(oConn, oTrans);
        //                return;
        //            }
        //        }

        //        string strProductKey = oInvoiceDetail.GetValue("ivd_prc_prd_key");

        //        string strSQL2 = "SELECT cr3_key"
        //            + " FROM ags_credential_requirement (NOLOCK)"
        //            + " JOIN ags_credential_requirement_type (NOLOCK) on cr3_ct3_requirement = ct3_requirement"
        //            + " WHERE ct3_prd_key = '" + strProductKey + "'"
        //            + " AND cr3_data_key IS NULL"
        //            + " AND cr3_met_date IS NULL"
        //            + " AND cr3_cst_key = '" + oInvoiceDetail.GetValue("inv_cst_key") + "'"
        //            + " AND cr3_delete_flag = 0";

        //        DataSet oDs2 = DataUtils.GetDataSet(strSQL2, oConn, oTrans);

        //        if (oDs2.Tables[0].Rows.Count > 0)
        //        {
        //            string strFeeRequirementKey = oDs2.Tables[0].Rows[0]["cr3_key"].ToString();

        //            FeeRequirement oFeeRequirement = new FeeRequirement();
        //            oFeeRequirement.CurrentKey = strFeeRequirementKey;
        //            oFeeRequirement.SelectByKey(oConn, oTrans);

        //            if (UtilityFunctions.EmptyString(oFeeRequirement.GetValue("cr3_met_date")))
        //            {
        //                oFeeRequirement.SetValue("cr3_data_key", strInvoiceKey);
        //                oFeeRequirement.LoadRelatedData(oConn, oTrans);
        //                oFeeRequirement.Update(oConn, oTrans);
        //                return;
        //            }
        //        }
        //    }
        //}
        #endregion 

        public static void AddFeeProductToURL(PageClass oPage)
        {
            if(oPage.Request["cr2_key"] != null && oPage.Request["ivd_prc_prd_key"] == null)
            {
                string strProcessKey = oPage.Request["cr2_key"].ToString();

                string strSQL = "select top 1 ct3_prd_key, cr3_data_key"
                                + " from ags_credential_process (nolock)"
                                + " join ags_credential_process_type (nolock) on cr2_ct2_process = ct2_process"
                                + " join ags_credential_requirement (nolock) on cr2_key = cr3_cr2_key"
                                + " join ags_credential_requirement_type (nolock) on cr3_ct3_requirement = ct3_requirement"
                                + " join ags_credential_requirement_template (nolock) on ct3_ct8_key = ct8_key"
                                + " join ags_credential_process_type_x_credential_requirement_type on ct2_key = ct5_ct2_key and ct3_key = ct5_ct3_key"
                                + " where cr2_key = '" + strProcessKey + "'"
                                + " and ct8_template = 'FEE'"
                                + " and cr3_met_date is null"
                                + " order by ct5_order";

                DataSet oDS = DataUtils.GetDataSet(strSQL);

                if(oDS.Tables[0].Rows.Count == 1)
                {
                    string strCustomerKey = Config.Session["CustomerKey"].ToString();
                    string strInvoiceKey = oDS.Tables[0].Rows[0]["cr3_data_key"].ToString();
                    string strProductKey = oDS.Tables[0].Rows[0]["ct3_prd_key"].ToString();

                    string strRedirectURL = "";

                    bool bInCart = false;

                    if (Config.Session["CentralizedOrderEntry"] != null)
                    {
                        OrderEntry oCOE = (OrderEntry)Config.Session["CentralizedOrderEntry"];

                        foreach(InvoiceDetail oDetail in oCOE.InvoiceDetails)
                        {
                            if (oDetail.GetValue("ivd_prc_prd_key").Equals(strProductKey))
                            {
                                bInCart = true;
                            }
                        }
                    }

                    if(bInCart)
                    {
                        strRedirectURL = "DynamicPage.aspx?webcode=COE";
                    }

                    else if(UtilityFunctions.EmptyString(strInvoiceKey))
                    {
                        strRedirectURL = oPage.Request.RawUrl + "&Action=Add&ObjectKeyFROM=1A83491A-9853-4C87-86A4-F7D95601C2E2&DoNotSave=yes&ParentObject=CentralizedOrderEntry&ParentDataObject=Invoice Detail&ivd_cst_key=" + strCustomerKey + "&ivd_cst_ship_key=" + strCustomerKey + "&ivd_prc_prd_key=" + strProductKey;
                    }

                    else
                    {
                        strRedirectURL = "DynamicPage.aspx?key=" + strInvoiceKey + "&ObjectKeyFrom=1A83491A-9853-4C87-86A4-F7D95601C2E2&WebCode=COEOpenInvoiceAdd&DoNotSave=yes&ParentObject=CentralizedOrderEntry&ParentDataObject=InvoiceToPay";
                    }

                    PageUtils.Redirect(strRedirectURL, true);
                }
            }
        }
    }
}
