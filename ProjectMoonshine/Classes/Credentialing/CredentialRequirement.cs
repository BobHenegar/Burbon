namespace ags.credentialing
{
    public class CredentialRequirement 
    {
        //public DynamicData _oData;
        //public DynamicData _oCredentialRequirementType;
        //public DynamicData _oCredentialRequirementTemplate;
        //public DynamicData _oCredentialProcess;
        //public DynamicData _oCredential;
        //public DynamicData _oCredentialStatusType;
        //public DynamicData _oCustomer;
        //public DynamicData _oCustomerDetail;
        //public DynamicData _oCustomerXAddress;
        //public DynamicData _oAddress;
        //public DynamicData _oCredentialRequirementData;


        //protected bool _bUndoMetDate = false;

        //public CredentialRequirement()
        //{
        //    _oData = new DynamicData("ags_credential_requirement", "ags_credential_requirement_ext", "cr3", "cr3", "cr3_key", "cr3_key_ext", "cr3_delete_flag", "CredentialRequirement", null, null, null, null);
        //    AddDataObject("cr3", _oData, "CredentialRequirement");

        //    _oCredentialRequirementType = new DynamicData("ags_credential_requirement_type", "ags_credential_requirement_type_ext", "ct3", "ct3", "ct3_key", "ct3_key_ext", "ct3_delete_flag", "CredentialRequirementType", null, null, null, null);
        //    AddDataObject("ct3", _oCredentialRequirementType, "CredentialRequirementType");

        //    _oCredentialRequirementTemplate = new DynamicData("ags_credential_requirement_template", "ags_credential_requirement_template_ext", "ct8", "ct8", "ct8_key", "ct8_key_ext", "ct8_delete_flag", "CredentialRequirementTemplate", null, null, null, null);
        //    AddDataObject("ct8", _oCredentialRequirementTemplate, "CredentialRequirementTemplate");

        //    _oCredentialProcess = new DynamicData("ags_credential_process", "ags_credential_process_ext", "cr2", "cr2", "cr2_key", "cr2_key_ext", "cr2_delete_flag", "CredentialProcess", null, null, null, null);
        //    AddDataObject("cr2", _oCredentialProcess, "CredentialProcess");

        //    _oCredential = new DynamicData("ags_credential", "ags_credential_ext", "cr1", "cr1", "cr1_key", "cr1_key_ext", "cr1_delete_flag", "Credential", null, null, null, null);
        //    AddDataObject("cr1", _oCredential, "Credential");

        //    _oCredentialStatusType = new DynamicData("ags_credential_status_type", "ags_credential_status_type_ext", "ct6", "ct6", "ct6_key", "ct6_key_ext", "ct6_delete_flag", "Status", null, null, null, null);
        //    AddDataObject("ct6", _oCredentialStatusType, "Status");

        //    _oCustomer = new DynamicData("co_customer", "co_customer_ext", "cst", "cst", "cst_key", "cst_key_ext", "cst_delete_flag", "Customer", null, null, null, null);
        //    AddDataObject("cst", _oCustomer, "Customer");

        //    _oCustomerXAddress = new DynamicData("co_customer_x_address", "co_customer_x_address_ext", "cxa", "cxa", "cxa_key", "cxa_key_ext", "cxa_delete_flag", "CustomerXAddress", null, null, null, null);
        //    AddDataObject("cxa", _oCustomerXAddress, "CustomerXAddress");

        //    _oAddress = new DynamicData("co_address", "co_address_ext", "adr", "adr", "adr_key", "adr_key_ext", "adr_delete_flag", "Address", null, null, null, null);
        //    AddDataObject("adr", _oAddress, "Address");

        //    HasProfilePage = true;
        //    SupportsCorrespondence = true;
        //}

        //public override ErrorClass Insert(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        Guid guid = Guid.NewGuid();
        //        _oData.CurrentKey = guid.ToString();
        //        _oData.SetValue("cr3_key", guid.ToString());

        //        CurrentKey = guid.ToString();

        //        if (errorClass.Number == 0)
        //        {
        //            LoadRelatedData(oConn, oTrans);
        //        }

        //        if (UtilityFunctions.EmptyString(GetValue("cr3_status")) && GetValue("ct3_has_required_parents_flag") == "1" && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_dependent_status")))
        //        {
        //            SetValue("cr3_status", GetValue("ct3_cr3_dependent_status"));
        //        }

        //        else if (UtilityFunctions.EmptyString(GetValue("cr3_status")) && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_start_status")))
        //        {
        //            SetValue("cr3_status", GetValue("ct3_cr3_start_status"));
        //        }

        //        if (!UtilityFunctions.EmptyString(GetValue("ct3_cr2_start_status")) && GetOriginalValue("cr2_status") == GetValue("cr2_status"))
        //        {
        //            SetProcessStatus(GetValue("ct3_cr2_start_status"), oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            errorClass = _oData.Insert(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0) 
        //        {
        //            FindRequirementData(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            LoadRelatedData(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            Init(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            if (UtilityFunctions.EmptyString(GetValue("cr3_start_date")))
        //            {
        //                if (!UtilityFunctions.EmptyString(GetValue("ct3_calculate_start_date_sp")))
        //                {
        //                    string strStartDate = CredentialingUtils.CalculatedRequirementDates(GetValue("ct3_calculate_start_date_sp"), GetValue("cr3_key"), oConn, oTrans);
        //                    SetValue("cr3_start_date", strStartDate);
        //                }
                        
        //                else
        //                {
        //                    SetValue("cr3_start_date", GetValue("cr2_start_date"));
        //                }
        //            }

        //            if (UtilityFunctions.EmptyString(GetValue("cr3_due_date")))
        //            {
        //                if(GetValue("ct3_sync_process_dates_flag") == "1")
        //                {
        //                    SetValue("cr3_due_date", GetValue("cr2_due_date"));
        //                }

        //                else if (!UtilityFunctions.EmptyString(GetValue("ct3_calculate_due_date_sp")))
        //                {
        //                    string strDueDate = CredentialingUtils.CalculatedRequirementDates(GetValue("ct3_calculate_due_date_sp"), GetValue("cr3_key"), oConn, oTrans);
        //                    SetValue("cr3_due_date", strDueDate);
        //                }
                        
        //                else if (!UtilityFunctions.EmptyString(GetValue("ct3_due_date_number")))
        //                {
        //                    SetValue("cr3_due_date", CredentialingUtils.CalculateDueDate(GetValue("ct3_due_date_number"), GetValue("ct3_due_date_unit"), GetValue("ct3_due_date_rounding")));
        //                }

        //                else
        //                {
        //                    SetValue("cr3_due_date", System.DateTime.Now.ToString());
        //                }

        //            }

        //            _oData.Update(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            GetDataObject("ct6").SelectByKey(oConn, oTrans);

        //            if (!UtilityFunctions.EmptyString(GetValue("ct6_cct_key")))
        //            {
        //                DataUtils.SendTemplate(this, GetValue("ct6_cct_key"), GetValue("cst_eml_address_dn"), null, oConn, oTrans);
        //            }
        //        }

        //        WorkflowUtils.CheckRules((FacadeClass)this, WorkflowUtils.Operation.Insert, oConn, oTrans);
        //    }
        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        Log(oConn, oTrans);

        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass Update(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (errorClass.Number == 0)
        //        {
        //            if (Config.IsIweb != true && GetValue("ct6_state") == "LOCKED" && GetValue("ct3_has_required_parents_flag") == "1")
        //            {
        //                if (AllRequiredParentRequirementsDone(oConn, oTrans))
        //                {
        //                    if (!UtilityFunctions.EmptyString(GetValue("ct3_cr3_start_status")))
        //                    {
        //                        SetStatus(GetValue("ct3_cr3_start_status"), oConn, oTrans);
        //                    }
        //                }

        //                else
        //                {
        //                    // All prereqs have not been met so do not proceed
        //                    return errorClass;
        //                }
        //            }
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            if (FieldChanged("cr3_status"))
        //            {
        //                SetStatus(GetValue("cr3_status"), oConn, oTrans);
        //            }

        //            errorClass = _oData.Update(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            if (_oCredentialRequirementData != null)
        //            {
        //                errorClass = _oCredentialRequirementData.Update(oConn, oTrans);
        //            }
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            FindRequirementData(oConn, oTrans);
            
        //            if (UtilityFunctions.EmptyString(GetValue("cr3_met_date")) && GetValue("ct6_state").Equals("MET"))
        //            {
        //                SetValue("cr3_met_date", System.DateTime.Now.ToString());
        //                SetValue("cr3_met_user", Config.CurrentUserName);

        //                GetDataObject("cr3").Update(oConn, oTrans);

        //                if (errorClass.Number == 0 && !UtilityFunctions.EmptyString(GetValue("cr3_cr2_key")))
        //                {
        //                    CredentialProcess oProcess = CredentialingUtils.GetCredentialProcessByKey(GetValue("cr3_cr2_key"), oConn, oTrans);
        //                    errorClass = oProcess.Update(oConn, oTrans);
        //                }

        //                if (errorClass.Number == 0 && !UtilityFunctions.EmptyString(GetValue("cr3_cr3_key")))
        //                {
        //                    CredentialRequirement oParentRequirement = CredentialingUtils.GetCredentialRequirementByKey(GetValue("cr3_cr3_key"), oConn, oTrans);
        //                    errorClass = oParentRequirement.Update(oConn, oTrans);
        //                }
        //            }

        //            else if (UtilityFunctions.EmptyString(GetOriginalValue("cr3_met_date")) || _bUndoMetDate == true)
        //            {
        //                errorClass = Eval(oConn, oTrans);

        //                if (errorClass.Number == 0)
        //                {
        //                    if (!UtilityFunctions.EmptyString(GetValue("cr3_met_date")))
        //                    {
        //                        if (UtilityFunctions.EmptyString(GetValue("cr3_met_user")))
        //                        {
        //                            if (Config.IsIweb)
        //                                SetValue("cr3_met_user", Config.CurrentUserName);
        //                            else if (Config.IsEweb)
        //                                SetValue("cr3_met_user", Config.Session["eWebCurrentUserName"].ToString());
        //                        }

        //                        if (UtilityFunctions.EmptyString(GetValue("cr3_met_reason")))
        //                        {
        //                            SetValue("cr3_met_reason", "Automated");
        //                        }

        //                        if (!FieldChanged("cr3_status"))
        //                        {
        //                            if (!UtilityFunctions.EmptyString(GetValue("ct3_cr3_met_status")))
        //                            {
        //                                SetStatus(GetValue("ct3_cr3_met_status"), oConn, oTrans);
        //                            }
        //                        }

        //                        if (!UtilityFunctions.EmptyString(GetValue("ct3_cr2_met_status")) && !FieldChanged("cr2_status"))
        //                        {
        //                            SetProcessStatus(GetValue("ct3_cr2_met_status"), oConn, oTrans);
        //                        }
                              
        //                        if (GetValue("ct3_submit_process_flag") == "1")
        //                        {
        //                            CredentialProcess oProcess = CredentialingUtils.GetCredentialProcessByKey(GetValue("cr3_cr2_key"), oConn, oTrans);

        //                            oProcess.Submit(oConn, oTrans);
        //                            oProcess.Update(oConn, oTrans);
        //                        }

        //                        errorClass = _oData.Update(oConn, oTrans);

        //                        if (errorClass.Number == 0)
        //                        {
        //                            if (_oCredentialRequirementData != null)
        //                            {
        //                                errorClass = _oCredentialRequirementData.Update(oConn, oTrans);
        //                            }
        //                        }

        //                        if (errorClass.Number == 0 && !UtilityFunctions.EmptyString(GetValue("cr3_cr3_key")))
        //                        {
        //                            CredentialRequirement oParentRequirement = CredentialingUtils.GetCredentialRequirementByKey(GetValue("cr3_cr3_key"), oConn, oTrans);

        //                            if (oParentRequirement.GetValue("ct8_template").Equals("CHECKLIST") && UtilityFunctions.EmptyString(oParentRequirement.GetValue("cr3_met_date")))
        //                            {
        //                                errorClass = oParentRequirement.Update(oConn, oTrans);
        //                            }
        //                        }

        //                        if (errorClass.Number == 0 && !UtilityFunctions.EmptyString(GetValue("cr3_cr2_key")))
        //                        {
        //                            CredentialProcess oProcess = CredentialingUtils.GetCredentialProcessByKey(GetValue("cr3_cr2_key"), oConn, oTrans);
        //                            errorClass = oProcess.Update(oConn, oTrans);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        errorClass = _oData.Update(oConn, oTrans);

        //                        if (_oCredentialRequirementData != null)
        //                        {
        //                            errorClass = _oCredentialRequirementData.Update(oConn, oTrans);
        //                        }
        //                    }

        //                    if (GetValue("ct6_state").Equals("DENIED") && GetValue("ct6_terminate_process_flag").Equals("1") && UtilityFunctions.EmptyString(GetValue("cr2_termination_date")))
        //                    {
        //                        SetValue("cr2_termination_date", DateTime.Now.ToString());
        //                        GetDataObject("cr2").Update(oConn, oTrans);
        //                    }

        //                }
        //            }

        //            if (errorClass.Number == 0)
        //            {
        //                if (FieldChanged("cr3_status"))
        //                {
        //                    GetDataObject("ct6").SelectByKey(oConn, oTrans);

        //                    if (!UtilityFunctions.EmptyString(GetValue("ct6_cct_key")))
        //                    {
        //                        DataUtils.SendTemplate(this, GetValue("ct6_cct_key"), GetValue("cst_eml_address_dn"), null, oConn, oTrans);

        //                        Config.LastError = new ErrorClass();
        //                    }
        //                }
        //            }

        //            // 08/25/17 sspruce: US6740 - Additional Error Handling.
        //            if (errorClass.Number == 0)
        //            {
        //                CheckForDependentRequirements(oConn, oTrans);

        //                CheckForParentRequirements(oConn, oTrans);
        //            }
        //        }

        //        WorkflowUtils.CheckRules((FacadeClass)this, WorkflowUtils.Operation.Update, oConn, oTrans);
        //    }

        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }

        //    finally
        //    {
        //        if (ObjectChanged())
        //        {
        //            Log(oConn, oTrans);
        //        }

        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public void SetStatus(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    if (string.IsNullOrEmpty(strStatus)) return;

        //    SetValue("cr3_status", strStatus);
        //    SetValue("cr3_status_change_date", System.DateTime.Now.ToString());
        //    SetValue("cr3_status_change_user", Config.CurrentUserName);

        //    string strCredentialStatusTypeKey = CredentialingUtils.GetCredentialStatusTypeKey(GetValue("cr3_status"), GetValue("ct3_key"));

        //    GetDataObject("ct6").CurrentKey = strCredentialStatusTypeKey;
        //    GetDataObject("ct6").SelectByKey(oConn, oTrans);

        //    // Set the process status if specified
        //    if (!UtilityFunctions.EmptyString(GetValue("ct6_parent_ct6_code")))
        //    {
        //        SetProcessStatus(GetValue("ct6_parent_ct6_code"), oConn, oTrans);
        //    }

        //    GetDataObject("cr3").Update(oConn, oTrans);

        //    Log(oConn, oTrans);
        //}

        //public void SetProcessStatus(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    CredentialProcess oProcess = CredentialingUtils.GetCredentialProcessByKey(GetValue("cr3_cr2_key"), oConn, oTrans);
        //    oProcess.SetStatus(strStatus, oConn, oTrans);
        //}

        //public void SetCredentialStatus(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    Credential oCredential = CredentialingUtils.GetCredentialByKey(GetValue("cr2_cr1_key"), oConn, oTrans);
        //    oCredential.SetStatus(strStatus, oConn, oTrans);
        //}

        //public override void BeforeSave()
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //    }
        //}

        //public override void AfterSave()
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //    }
        //}

        //public override void BeforeDelete()
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //    }
        //}

        //public override ErrorClass Delete(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    if (Config.DeleteMethod == "hard")
        //    {
        //        return HardDelete(oConn, oTrans);
        //    }
        //    return SoftDelete(oConn, oTrans);
        //}

        //public override ErrorClass HardDelete(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (errorClass.Number == 0)
        //        {
        //            errorClass = _oCredentialRequirementData.Delete(oConn, oTrans);
        //        }

        //        if (errorClass.Number == 0)
        //        {
        //            errorClass = _oData.Delete(oConn, oTrans);
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass SoftDelete(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        SetValue("cr3_delete_flag", "1");

        //        errorClass = _oData.Update(oConn, oTrans);
        //    }

        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public override void AfterDelete()
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //    }
        //}

        //public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (UtilityFunctions.EmptyString(CurrentKey) && _oCredentialRequirementData != null)
        //        {
        //            if (Config.Session[_oCredentialRequirementData.Description] != null)
        //            {
        //                CurrentKey = Config.Session[_oCredentialRequirementData.Description].ToString();
        //            }
        //        }

        //        _oData.CurrentKey = CurrentKey;
        //        _oData.SelectByKey(oConn, oTrans);

        //        if (!UtilityFunctions.EmptyString(_oData.GetValue("cr3_data_key")))
        //        {
        //            if (_oCredentialRequirementData != null)
        //            {
        //                _oCredentialRequirementData.CurrentKey = _oData.GetValue("cr3_data_key");
        //                _oCredentialRequirementData.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (!UtilityFunctions.EmptyString(_oData.GetValue("cr3_ct3_requirement")))
        //        {
        //            _oCredentialRequirementType.CurrentKey = CredentialingUtils.GetCredentialRequirementTypeKey(_oData.GetValue("cr3_ct3_requirement"), oConn, oTrans);
        //            _oCredentialRequirementType.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oCredentialRequirementType.GetValue("ct3_ct8_key")))
        //        {
        //            _oCredentialRequirementTemplate.CurrentKey = _oCredentialRequirementType.GetValue("ct3_ct8_key");
        //            _oCredentialRequirementTemplate.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oData.GetValue("cr3_cr2_key")))
        //        {
        //            _oCredentialProcess.CurrentKey = _oData.GetValue("cr3_cr2_key");
        //            _oCredentialProcess.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oCredentialProcess.GetValue("cr2_cr1_key")))
        //        {
        //            _oCredential.CurrentKey = _oCredentialProcess.GetValue("cr2_cr1_key");
        //            _oCredential.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oData.GetValue("cr3_status")))
        //        {
        //            string strStatusTypeKey = CredentialingUtils.GetCredentialStatusTypeKey(GetValue("cr3_status"), GetValue("ct3_key"));

        //            _oCredentialStatusType.CurrentKey = strStatusTypeKey;
        //            _oCredentialStatusType.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oData.GetValue("cr3_cst_key")))
        //        {
        //            _oCustomer.CurrentKey = _oData.GetValue("cr3_cst_key");
        //            _oCustomer.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oData.GetValue("cr3_cst_key")))
        //        {
        //            if (_oCustomerDetail == null)
        //            {
        //                _oCustomerDetail = CredentialingUtils.GetCustomerDetail(_oCustomer, this);
        //            }
        //            _oCustomerDetail.CurrentKey = _oData.GetValue("cr3_cst_key");
        //            _oCustomerDetail.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oCustomer.GetValue("cst_cxa_key")))
        //        {
        //            _oCustomerXAddress.CurrentKey = _oCustomer.GetValue("cst_cxa_key");
        //            _oCustomerXAddress.SelectByKey(oConn, oTrans);
        //        }

        //        if (!UtilityFunctions.EmptyString(_oCustomerXAddress.GetValue("cxa_adr_key")))
        //        {
        //            _oAddress.CurrentKey = _oCustomerXAddress.GetValue("cxa_adr_key");
        //            _oAddress.SelectByKey(oConn, oTrans);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }
        //}

        //public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (UtilityFunctions.EmptyString(CurrentKey) && _oCredentialRequirementData != null)
        //        {
        //            if (Config.Session[_oCredentialRequirementData.Description] != null)
        //            {
        //                CurrentKey = Config.Session[_oCredentialRequirementData.Description].ToString();
        //            }
        //        }

        //        if (_oData.CurrentKey != CurrentKey)
        //        {
        //            _oData.CurrentKey = CurrentKey;

        //            if (UtilityFunctions.EmptyString(_oData.CurrentKey))
        //            {
        //                _oData.ClearValues();
        //            }
        //            else
        //            {
        //                _oData.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCredentialRequirementType.GetValue("ct3_requirement") != _oData.GetValue("cr3_ct3_requirement"))
        //        {
        //            if (UtilityFunctions.EmptyString(_oData.GetValue("cr3_ct3_requirement")))
        //            {
        //                _oCredentialRequirementType.CurrentKey = _oCredentialRequirementType.GetValue("ct3_key");
        //            }
        //            else
        //            {
        //                _oCredentialRequirementType.CurrentKey =
        //                    CredentialingUtils.GetCredentialRequirementTypeKey(_oData.GetValue("cr3_ct3_requirement"),
        //                                                                       oConn, oTrans);
        //            }

        //            if (UtilityFunctions.EmptyString(_oCredentialRequirementType.CurrentKey))
        //            {
        //                _oCredentialRequirementType.ClearValues();
        //            }
        //            else
        //            {
        //                _oCredentialRequirementType.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCredentialRequirementTemplate.GetValue("ct8_key") != _oCredentialRequirementType.GetValue("ct3_ct8_key"))
        //        {
        //            _oCredentialRequirementTemplate.CurrentKey = _oCredentialRequirementType.GetValue("ct3_ct8_key");

        //            if (UtilityFunctions.EmptyString(_oCredentialRequirementTemplate.CurrentKey))
        //            {
        //                _oCredentialRequirementTemplate.ClearValues();
        //            }
        //            else
        //            {
        //                _oCredentialRequirementTemplate.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCredentialProcess.GetValue("cr2_key") != _oData.GetValue("cr3_cr2_key"))
        //        {
        //            _oCredentialProcess.CurrentKey = _oData.GetValue("cr3_cr2_key");

        //            if (UtilityFunctions.EmptyString(_oCredentialProcess.CurrentKey))
        //            {
        //                _oCredentialProcess.ClearValues();
        //            }
        //            else
        //            {
        //                _oCredentialProcess.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCredential.GetValue("cr1_key") != _oCredentialProcess.GetValue("cr2_cr1_key"))
        //        {
        //            _oCredential.CurrentKey = _oCredentialProcess.GetValue("cr2_cr1_key");

        //            if (UtilityFunctions.EmptyString(_oCredential.CurrentKey))
        //            {
        //                _oCredential.ClearValues();
        //            }
        //            else
        //            {
        //                _oCredential.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCredentialStatusType.GetValue("ct6_code") != _oData.GetValue("cr3_status"))
        //        {
        //            string strCredentialStatusTypeKey = CredentialingUtils.GetCredentialStatusTypeKey(GetValue("cr3_status"), GetValue("ct3_key"));

        //            _oCredentialStatusType.CurrentKey = strCredentialStatusTypeKey;

        //            if (UtilityFunctions.EmptyString(_oCredentialStatusType.CurrentKey))
        //            {
        //                _oCredentialStatusType.ClearValues();
        //            }
        //            else
        //            {
        //                _oCredentialStatusType.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCustomer.GetValue("cst_key") != _oData.GetValue("cr3_cst_key"))
        //        {
        //            _oCustomer.CurrentKey = _oData.GetValue("cr3_cst_key");

        //            if (UtilityFunctions.EmptyString(_oCustomer.CurrentKey))
        //            {
        //                _oCustomer.ClearValues();
        //            }
        //            else
        //            {
        //                _oCustomer.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCustomerDetail == null && !UtilityFunctions.EmptyString(_oData.GetValue("cr3_cst_key")))
        //        {
        //            if (_oCustomerDetail == null)
        //            {
        //                _oCustomerDetail = CredentialingUtils.GetCustomerDetail(_oCustomer, this);
        //            }
        //            _oCustomerDetail.CurrentKey = _oData.GetValue("cr3_cst_key");
        //            _oCustomerDetail.SelectByKey(oConn, oTrans);
        //        }

        //        else if (_oCustomerDetail != null && _oCustomerDetail.GetValue("ind_cst_key") != _oData.GetValue("cr3_cst_key") && _oCustomerDetail.GetValue("org_cst_key") != _oData.GetValue("cr3_cst_key"))
        //        {
        //            _oCustomerDetail.CurrentKey = _oData.GetValue("cr3_cst_key");

        //            if (UtilityFunctions.EmptyString(_oCustomerDetail.CurrentKey))
        //            {
        //                _oCustomerDetail.ClearValues();
        //            }
        //            else
        //            {
        //                _oCustomerDetail.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCustomerXAddress.GetValue("cxa_key") != _oCustomer.GetValue("cst_cxa_key"))
        //        {
        //            _oCustomerXAddress.CurrentKey = _oCustomer.GetValue("cst_cxa_key");

        //            if (UtilityFunctions.EmptyString(_oCustomerXAddress.CurrentKey))
        //            {
        //                _oCustomerXAddress.ClearValues();
        //            }
        //            else
        //            {
        //                _oCustomerXAddress.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oAddress.GetValue("cxa_key") != _oCustomerXAddress.GetValue("cxa_adr_key"))
        //        {
        //            _oAddress.CurrentKey = _oCustomerXAddress.GetValue("cxa_adr_key");

        //            if (UtilityFunctions.EmptyString(_oAddress.CurrentKey))
        //            {
        //                _oAddress.ClearValues();
        //            }
        //            else
        //            {
        //                _oAddress.SelectByKey(oConn, oTrans);
        //            }
        //        }

        //        if (_oCredentialRequirementData != null && !UtilityFunctions.EmptyString(_oData.GetValue("cr3_data_key")) && _oCredentialRequirementData.CurrentKey != _oData.GetValue("cr3_data_key"))
        //        {
        //            _oCredentialRequirementData.CurrentKey = _oData.GetValue("cr3_data_key");

        //            if (UtilityFunctions.EmptyString(_oCredentialRequirementData.CurrentKey))
        //            {
        //                _oCredentialRequirementData.ClearValues();
        //            }
        //            else
        //            {
        //                _oCredentialRequirementData.SelectByKey(oConn, oTrans);
        //            }
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }

        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }
        //}

        //public override void ClearKeys()
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try
        //    {
        //        if (!UtilityFunctions.EmptyString(CurrentKey))
        //        {
        //            CurrentKey = "";
        //        }
        //        else
        //        {
        //            _oData.SetValue("cr3_key", "");
        //            _oCredentialRequirementType.SetValue("ct3_key", "");
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }
        //}

        //public virtual ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try 
        //    {               
        //        errorClass = _oData.Update(oConn, oTrans);                      
        //    }
        //    catch (Exception e) 
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally 
        //    {
        //        if (errorClass.Number != 0) 
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }
            
        //    return errorClass;
        //}

        //public virtual ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    try 
        //    {
        //        if (string.IsNullOrWhiteSpace("cr3_data_key")) 
        //        {
        //            FindRequirementData(oConn, oTrans);
        //        }
        //        if (UtilityFunctions.EmptyString(GetValue("cr3_met_date")) && GetValue("ct6_state").Equals("MET"))
        //        {
        //            SetValue("cr3_met_date", System.DateTime.Now.ToString());
        //        }

        //        if (!UtilityFunctions.EmptyString(GetValue("cr3_met_date")))
        //        {
        //            SetValue("cr3_met_user", Config.CurrentUserName);
        //        }
        //    }
        //    catch (Exception e) 
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public virtual ErrorClass FindRequirementData(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    var errorClass = new ErrorClass();

        //    try
        //    {           
        //        if (string.IsNullOrWhiteSpace(GetValue("cr3_data_key"))
        //            && !string.IsNullOrWhiteSpace(GetValue("ct3_find_procedure"))
        //            )
        //        {
        //            var sql = "exec " + GetValue("ct3_find_procedure") + " "
        //                + DataUtils.ValuePrep(GetValue("cr3_key"), DataUtils.AVType.av_key, false);

        //            DataTable oDT = DataUtils.GetDataSet(sql, oConn, oTrans).Tables[0];
        //            if (oDT.Columns.Contains("data_key")
        //                && oDT.Rows.Count > 0
        //                )
        //            {
        //                var dataKey = oDT.Rows[0]["data_key"].ToString();
        //                if (!string.IsNullOrWhiteSpace(dataKey)
        //                    && GetValue("ct3_set_data_key_flag") == "1"
        //                    && dataKey.ToUpper() != GetValue("cr3_data_key").ToUpper()
        //                    )
        //                {
        //                    SetValue("cr3_data_key", dataKey);
        //                    GetDataObject("cr3").Update(oConn, oTrans);
        //                    LoadRelatedData(oConn, oTrans);
        //                }
        //            }
        //        }  
        //    }
        //    catch (Exception e)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally
        //    {
        //        if (errorClass.Number != 0)
        //        {
        //            Config.LastError = errorClass;
        //        }
        //    }
        //    return errorClass;
        //}

        //public virtual void UndoMetDate(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    SetValue("cr3_met_date", "");
        //    SetValue("cr3_met_user", "");
        //    SetValue("cr3_status", strStatus);

        //    GetDataObject("cr3").Update(oConn, oTrans);

        //    LoadRelatedData(oConn, oTrans);

        //    _bUndoMetDate = true;
        //}

        //public virtual void Log(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    if (!UtilityFunctions.EmptyString(GetValue("ct3_log_stored_procedure")))
        //    {
        //        DataUtils.ExecuteSql("exec " + GetValue("ct3_log_stored_procedure") + " @cr3_key = '" + GetValue("cr3_key") + "', @user = '" + Config.CurrentUserName + "'", oConn, oTrans);
        //    }
        //    else
        //    {
        //        DataUtils.ExecuteSql("exec " + Config.GetSystemOption("AGSDefaultLogReqirementStoredProcedure") + " @cr3_key = '" + GetValue("cr3_key") + "', @user = '" + Config.CurrentUserName + "'", oConn, oTrans);
        //    }
        //}

        //public int GetProcessOrder()
        //{
        //    string strSQL = "select ct5_order"
        //        + " from dbo.ags_credential_requirement (NOLOCK)"
        //        + " JOIN dbo.ags_credential_requirement_type (NOLOCK) ON cr3_ct3_requirement = ct3_requirement"
        //        + " JOIN ags_credential_process (nolock) ON cr3_cr2_key = cr2_key"
        //        + " JOIN ags_credential_process_type (NOLOCK) ON cr2_ct2_process = ct2_process"
        //        + " join ags_credential_process_type_x_credential_requirement_type (nolock) on ct2_key = ct5_ct2_key and ct3_key = ct5_ct3_key"
        //        + " where cr3_key = '" + CurrentKey + "'"
        //        + " and cr3_delete_flag = 0"
        //        + " and cr2_delete_flag = 0"
        //        + " and ct3_delete_flag = 0"
        //        + " and ct2_delete_flag = 0"
        //        + " and ct5_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    int iProcessOrder = 0;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        iProcessOrder = Convert.ToInt32(oDS.Tables[0].Rows[0]["ct5_order"].ToString());
        //    }

        //    return iProcessOrder;
        //}

        //public virtual void SetUnderReviewStatus(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    if (!UtilityFunctions.EmptyString(GetValue("ct3_cr3_submit_status")) && UtilityFunctions.EmptyString(GetValue("cr3_met_date")))
        //    {
        //        SetValue("cr3_status", GetValue("ct3_cr3_submit_status"));
        //        _oData.Update(oConn, oTrans);
        //    }
        //}

        //public bool AllRequiredParentRequirementsDone(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    bool bAllRequiredParentRequirementsDone = false;

        //    // Not all requirements know who the process is
        //    if (UtilityFunctions.EmptyString(GetValue("cr3_cr2_key")))
        //    {
        //        return bAllRequiredParentRequirementsDone;
        //    }

        //    string strSQL = "select cr3_key"
        //        + " from ags_credential_requirement (nolock)"
        //        + " JOIN dbo.ags_credential_requirement_type (NOLOCK) ON cr3_ct3_requirement = ct3_requirement"
        //        + " join ags_credential_requirement_type_x_credential_requirement_type on ct3_key = ct0_parent_ct3_key"
        //        + " join ags_credential_status_type on cr3_status = ct6_code and ct3_key = ct6_type_key"
        //        + " where cr3_cr2_key = '" + GetValue("cr3_cr2_key") + "'"
        //        + " and ct0_child_ct3_key = '" + GetValue("ct3_key") + "'"
        //        + " and ct6_state <> ct0_parent_ct3_state";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    if (oDS.Tables[0].Rows.Count == 0)
        //    {
        //        bAllRequiredParentRequirementsDone = true;
        //    }

        //    return bAllRequiredParentRequirementsDone;
        //}

        //public void CheckForDependentRequirements(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "select child_cr3.cr3_key"
        //        + " from ags_credential_requirement parent_cr3"
        //        + " join ags_credential_requirement_type parent_ct3 on parent_cr3.cr3_ct3_requirement = parent_ct3.ct3_requirement"
        //        + " join ags_credential_requirement_type_x_credential_requirement_type on parent_ct3.ct3_key = ct0_parent_ct3_key"
        //        + " join ags_credential_requirement_type child_ct3 on ct0_child_ct3_key = child_ct3.ct3_key"
        //        + " join ags_credential_requirement child_cr3 on child_ct3.ct3_requirement = child_cr3.cr3_ct3_requirement"
        //        + " where parent_cr3.cr3_key = '" + GetValue("cr3_key") + "'"
        //        + " and ct0_type = 'REQUIRED'"
        //        + " and (child_cr3.cr3_cr2_key = parent_cr3.cr3_cr2_key or child_cr3.cr3_cr3_key = parent_cr3.cr3_key)"
        //        + " order by ct0_order";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    foreach (DataRow oRow in oDS.Tables[0].Rows)
        //    {
        //        string strDependentRequirementKey = oRow["cr3_key"].ToString();

        //        CredentialRequirement oDependentRequirement = CredentialingUtils.GetCredentialRequirementByKey(strDependentRequirementKey, oConn, oTrans);

        //        oDependentRequirement.Update(oConn, oTrans);
        //    }
        //}

        //public void CheckForParentRequirements(NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "select parent_cr3.cr3_key"
        //        + " from ags_credential_requirement child_cr3"
        //        + " join ags_credential_requirement_type child_ct3 on child_cr3.cr3_ct3_requirement = child_ct3.ct3_requirement"
        //        + " join ags_credential_requirement_type_x_credential_requirement_type on child_ct3.ct3_key = ct0_child_ct3_key"
        //        + " join ags_credential_requirement_type parent_ct3 on ct0_parent_ct3_key = parent_ct3.ct3_key"
        //        + " join ags_credential_requirement parent_cr3 on parent_ct3.ct3_requirement = parent_cr3.cr3_ct3_requirement"
        //        + " where child_cr3.cr3_key = '" + GetValue("cr3_key") + "'"
        //        + " and ct0_type = 'CHECKLIST'"
        //        + " and child_cr3.cr3_cr3_key = parent_cr3.cr3_key"
        //        + " and parent_cr3.cr3_met_date is null"
        //        + " order by ct0_order";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    foreach (DataRow oRow in oDS.Tables[0].Rows)
        //    {
        //        string strParentRequirementKey = oRow["cr3_key"].ToString();

        //        CredentialRequirement oParentRequirement = CredentialingUtils.GetCredentialRequirementByKey(strParentRequirementKey, oConn, oTrans);

        //        oParentRequirement.Update(oConn, oTrans);
        //    }
        //}
    }
}
    

