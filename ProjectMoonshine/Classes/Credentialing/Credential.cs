namespace ags.credentialing
{
    public class Credential 
    {
		//public DynamicData _oData;
		//public DynamicData _oCredentialType;
  //      public DynamicData _oCurrentProcess;
  //      public DynamicData _oCurrentProcessType;
  //      public DynamicData _oCustomer;
  //      public DynamicData _oCustomerDetail;

		//public Credential()
		//{
		//	_oData = new DynamicData("ags_credential", "ags_credential_ext", "cr1", "cr1", "cr1_key", "cr1_key_ext", "cr1_delete_flag", "Credential", null, null, null, null);
		//	AddDataObject("cr1", _oData, "Credential");

		//	_oCredentialType = new DynamicData("ags_credential_type", "ags_credential_type_ext", "ct1", "ct1", "ct1_key", "ct1_key_ext", "ct1_delete_flag", "CredentialType", null, null, null, null);
		//	AddDataObject("ct1", _oCredentialType, "CredentialType");

  //          _oCurrentProcess = new DynamicData("ags_credential_process", "ags_credential_process_ext", "cr2", "cr2", "cr2_key", "cr2_key_ext", "cr2_delete_flag", "CurrentProcess", null, null, null, null);
  //          AddDataObject("cr2", _oCurrentProcess, "CurrentProcess");

  //          _oCurrentProcessType = new DynamicData("ags_credential_process_type", "ags_credential_process_type_ext", "ct2", "ct2", "ct2_key", "ct2_key_ext", "ct2_delete_flag", "CurrentProcessType", null, null, null, null);
  //          AddDataObject("ct2", _oCurrentProcessType, "CurrentProcessType");

		//	_oCustomer = new DynamicData("co_customer", "co_customer_ext", "cst", "cst", "cst_key", "cst_key_ext", "cst_delete_flag", "Customer", null, null, null, null);
		//	AddDataObject("cst", _oCustomer, "Customer");

		//	HasProfilePage = true;
		//	SupportsCorrespondence = true;
		//}
        
  //      public override void BeforeSave()
  //      {
  //          ErrorClass errorClass = new ErrorClass();
		
		//	try
		//	{
		//	}
		//	catch
		//	{
		//	}
		//	finally
		//	{
		//	}
		//}

		//public override ErrorClass Insert(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//		Guid guid = Guid.NewGuid();
		//		CurrentKey = guid.ToString();
		//		_oData.CurrentKey = guid.ToString();
		//		_oData.SetValue("cr1_key", guid.ToString());

		//		SetValue("cr1_start_date", System.DateTime.Now.ToString());
				
		//		if (errorClass.Number == 0)
		//		{
		//			errorClass = _oData.Insert(oConn, oTrans);
		//		}

		//		if (errorClass.Number == 0)
		//		{
		//			LoadRelatedData(oConn, oTrans);
		//		}

		//		string strCredentialKey = CurrentKey;
		//		string strInitProcessTypeKey = null;
		//		string strInitProcessType = null;
		//		CredentialProcess oInitProcess = null;
				
  //              // First look to see if the Current Process has already been identified
  //              if (!UtilityFunctions.EmptyString(GetValue("cr1_ct2_current_process")))
  //              {
  //                  strInitProcessTypeKey = CredentialingUtils.GetCredentialProcessTypeKey(GetValue("cr1_ct2_current_process"));
  //              }
                
		//		// Second look to see if there is an initial process defined for this credential
		//		else if(!UtilityFunctions.EmptyString(_oCredentialType.GetValue("ct1_init_ct2_key")))
		//		{
		//			strInitProcessTypeKey = _oCredentialType.GetValue("ct1_init_ct2_key");
		//		}
                
		//		// Next look to see if there is a method to determine what the initial process is for this credential
		//		else if(!UtilityFunctions.EmptyString(_oCredentialType.GetValue("ct1_init_ct2_assembly"))
		//			&& !UtilityFunctions.EmptyString(_oCredentialType.GetValue("ct1_init_ct2_class"))
		//			&& !UtilityFunctions.EmptyString(_oCredentialType.GetValue("ct1_init_ct2_method")))
		//		{
		//			string strInitAssembly = _oCredentialType.GetValue("ct1_init_ct2_assembly");
		//			string strInitClass = _oCredentialType.GetValue("ct1_init_ct2_class");
		//			string strInitMethod = _oCredentialType.GetValue("ct1_init_ct2_method");

		//			Assembly oAssembly = Assembly.Load(strInitAssembly);
					
		//			Type oType = oAssembly.GetType(strInitClass);
		//			object[] oArguments = { GetValue("cst_key") };
		//			object oResult = null;
		//			oResult = oType.InvokeMember(strInitMethod, BindingFlags.InvokeMethod, null, oResult, oArguments);

		//			strInitProcessTypeKey = oResult.ToString();
		//		}

		//		else
		//		{
		//			// No initial process defined for this credential.
		//			errorClass.Number = -1;
		//			errorClass.Message = "Could not create Initial Process";
		//			return errorClass;
		//		}

		//		if(errorClass.Number == 0)
		//		{
		//			strInitProcessType = CredentialingUtils.GetCredentialProcessType(strInitProcessTypeKey, oConn, oTrans);
		//			oInitProcess = CredentialingUtils.GetCredentialProcessByType(strInitProcessType);
				
		//			if(errorClass.Number == 0)
		//			{
		//				oInitProcess.SetValue("cr2_cr1_key", CurrentKey);
		//				oInitProcess.SetValue("cr2_ct2_process", strInitProcessType);
		//				oInitProcess.SetValue("cr2_cst_key", _oCustomer.GetValue("cst_key"));
		//				oInitProcess.LoadRelatedData(oConn, oTrans);

		//				errorClass = oInitProcess.Insert(oConn, oTrans);
		//			}

		//			if(errorClass.Number == 0)
		//			{
		//				_oData.SetValue("cr1_current_cr2_key", oInitProcess.CurrentKey);
  //                      _oData.SetValue("cr1_application_cr2_key", oInitProcess.CurrentKey);
		//				SetStatus(_oCredentialType.GetValue("ct1_init_status"), oConn, oTrans);
		//				errorClass = _oData.Update(oConn, oTrans);
		//			}
		//		}

  //              WorkflowUtils.CheckRules((FacadeClass)this, WorkflowUtils.Operation.Insert, oConn, oTrans);
		//	}
		//	catch (Exception e)
		//	{
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
		//	finally
		//	{
  //              Log(oConn, oTrans);
                
  //              if (errorClass.Number != 0)
		//		{
		//			Config.LastError = errorClass;
		//		}
		//	}
			
		//	return errorClass;
		//}

		//public override ErrorClass Update(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
  //              CredentialProcess oProcess = CredentialingUtils.GetCredentialProcessByKey(GetValue("cr1_current_cr2_key"), oConn, oTrans);

		//		if (errorClass.Number == 0)
		//		{
		//			if(GetOriginalValue("cr1_status") != GetValue("cr1_status"))
		//			{
  //                      SetStatus(GetValue("cr1_status"), oConn, oTrans);
		//			}

  //                  if (!String.IsNullOrEmpty(oProcess.GetValue("cr2_termination_date"))) 
  //                  {
  //                      RemoveDesignation(oConn, oTrans);
  //                  }
					
		//			errorClass = _oData.Update(oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(oProcess.GetValue("cr2_completion_date")) && UtilityFunctions.EmptyString(oProcess.GetValue("cr2_termination_date").ToString()))
		//		{
  //                  SetValue("cr1_effective_date", oProcess.GetValue("cr2_completion_date"));

		//		    if (UtilityFunctions.EmptyString(GetValue("cr1_credential_date").ToString()))
		//		    {
		//		        SetValue("cr1_credential_date", oProcess.GetValue("cr2_completion_date"));

  //                      SetValue("cr1_credential_number", GetNextCredentialNumber(oProcess, oConn, oTrans));

		//		        if (GetValue("ct1_cst_type") == "Individual")
		//		        {
  //                          CO_Individual oIndividual = (CO_Individual)DataUtils.InstantiateFacadeObject("Individual");
		//		            oIndividual.CurrentKey = GetValue("cst_key");
  //                          oIndividual.SelectByKey(oConn, oTrans);

		//		            if (!UtilityFunctions.EmptyString(GetValue("ct1_designation")))
		//		            {
		//		                if (UtilityFunctions.EmptyString(GetValue("ind_designation")))
		//		                {
		//		                    oIndividual.SetValue("ind_designation", GetValue("ct1_designation"));
		//		                }
		//		                else
		//		                {
  //                                  oIndividual.SetValue("ind_designation", GetValue("ind_designation") + ", " + GetValue("ct1_designation"));
		//		                }

  //                              oIndividual.Update(oConn, oTrans);
		//		            }
		//		        }
		//		        if (GetValue("ct1_cst_type") == "Organization")
		//		        {
		//		        }
		//		    }

		//		    _oData.Update(oConn, oTrans);

  //                  string strRenewProcessTypeKey = String.Empty;

  //                  if (!UtilityFunctions.EmptyString(_oCurrentProcessType.GetValue("ct2_next_ct2_key")))
  //                  {
  //                      strRenewProcessTypeKey = _oCurrentProcessType.GetValue("ct2_next_ct2_key");
  //                  }

		//			else if (!UtilityFunctions.EmptyString(_oCredentialType.GetValue("ct1_renew_ct2_key")))
		//			{
		//			    strRenewProcessTypeKey = _oCredentialType.GetValue("ct1_renew_ct2_key");
		//			}

		//			else
		//			{
		//				//If cr1_renew_cr2_key is null, then the method defined in cr1_renew_cr2_method is invoked to return the CredentialProcessType to create.					
		//			}

  //                  if (!UtilityFunctions.EmptyString(strRenewProcessTypeKey))
  //                  {
  //                      CredentialProcess oRenewCredentialProcess = null;
  //                      string strCredentialKey = CurrentKey;
  //                      string strRenewCredentialProcessType = null;

  //                      strRenewCredentialProcessType = CredentialingUtils.GetCredentialProcessType(strRenewProcessTypeKey, oConn, oTrans);
  //                      oRenewCredentialProcess = CredentialingUtils.GetCredentialProcessByType(strRenewCredentialProcessType);

  //                      if (errorClass.Number == 0)
  //                      {
  //                          oRenewCredentialProcess.SetValue("cr2_cr1_key", CurrentKey);
  //                          oRenewCredentialProcess.SetValue("cr2_ct2_process", strRenewCredentialProcessType);
  //                          oRenewCredentialProcess.SetValue("cr2_cst_key", _oCustomer.GetValue("cst_key"));

  //                          oRenewCredentialProcess.LoadRelatedData(oConn, oTrans);

  //                          errorClass = oRenewCredentialProcess.Insert(oConn, oTrans);
  //                      }

  //                      if (errorClass.Number == 0)
  //                      {
  //                          _oData.SetValue("cr1_current_cr2_key", oRenewCredentialProcess.CurrentKey);
  //                          _oData.SetValue("cr1_effective_date", System.DateTime.Now.ToString());
  //                          _oData.SetValue("cr1_valid_thru_date", oRenewCredentialProcess.GetValue("cr2_due_date"));

  //                          if (!UtilityFunctions.EmptyString(_oCredentialType.GetValue("ct1_renew_status")))
  //                          {
  //                              SetStatus(_oCredentialType.GetValue("ct1_renew_status"), oConn, oTrans);
  //                          }

  //                          errorClass = _oData.Update(oConn, oTrans);
  //                      }
  //                  }
		//		}

  //              WorkflowUtils.CheckRules((FacadeClass)this, WorkflowUtils.Operation.Update, oConn, oTrans);
		//	}
		//	catch (Exception e)
		//	{
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
		//	finally
		//	{
  //              if (ObjectChanged())
  //              {
  //                  Log(oConn, oTrans);
  //              }
                
  //              if (errorClass.Number != 0)
		//		{
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

  //      private string GetNextCredentialNumber(CredentialProcess oProcess, NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          string strNextNumber = "0";

  //          string credentialNumberSproc = oProcess.GetValue("ct1_set_credential_number_sp");


  //          if (String.IsNullOrEmpty(credentialNumberSproc))
  //          {
  //              string strSQL = "SELECT next_number = max(convert(int, cr1_credential_number)) FROM ags_credential (nolock) WHERE ISNUMERIC(cr1_credential_number) = 1;";

  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //              if (oDS.Tables[0].Rows.Count == 1)
  //              {
  //                  strNextNumber = oDS.Tables[0].Rows[0]["next_number"].ToString();
  //              }
  //          }
  //          else
  //          {
  //              string cr1Key = oProcess.GetValue("cr1_key");
  //              string strSQL = String.Format("EXEC {0} '{1}'", credentialNumberSproc, cr1Key);

  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //              if (oDS.Tables[0].Rows.Count == 1)
  //              {
  //                  strNextNumber = oDS.Tables[0].Rows[0][0].ToString();
  //              }
  //          }

  //          return strNextNumber;
  //      }

		//public override void AfterSave()
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//	}
		//	catch
		//	{
		//	}
		//	finally
		//	{
		//	}
		//}

		//public override void BeforeDelete()
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//	}
		//	catch
		//	{
		//	}
		//	finally
		//	{
		//	}
		//}

		//public override ErrorClass Delete(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	if (Config.DeleteMethod == "hard")
		//	{
		//		return HardDelete(oConn, oTrans);
		//	}
		//	return SoftDelete(oConn, oTrans);
		//}

		//public override ErrorClass HardDelete(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
  //              if (errorClass.Number == 0)
  //              {
  //              }

		//		if (errorClass.Number == 0)
		//		{
		//			errorClass = _oData.Delete(oConn, oTrans);
		//		}
		//	}
			
		//	catch (Exception e)
		//	{
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
		//	finally
		//	{
		//		if (errorClass.Number != 0)
		//		{
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

		//public override ErrorClass SoftDelete(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//		SetValue("cr1_delete_flag", "1");

		//		errorClass = _oData.Update(oConn, oTrans);
		//	}

		//	catch (Exception e)
		//	{
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
		//	finally
		//	{
		//		if (errorClass.Number != 0)
		//		{
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

		//public override void AfterDelete()
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//	}
		//	catch
		//	{
		//	}
		//	finally
		//	{
		//	}
		//}

		//public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//		_oData.CurrentKey = CurrentKey;
		//		_oData.SelectByKey(oConn, oTrans);

		//		if (!UtilityFunctions.EmptyString(_oData.GetValue("cr1_ct1_credential")))
		//		{
		//			_oCredentialType.CurrentKey = GetCredentialTypeKey(_oData.GetValue("cr1_ct1_credential"), oConn, oTrans);
		//			_oCredentialType.SelectByKey(oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("cr1_current_cr2_key")))
  //              {
  //                  _oCurrentProcess.CurrentKey = _oData.GetValue("cr1_current_cr2_key");
  //                  _oCurrentProcess.SelectByKey(oConn, oTrans);
  //              }

  //              if (!UtilityFunctions.EmptyString(_oCurrentProcess.GetValue("cr2_ct2_process")))
  //              {
  //                  _oCurrentProcessType.CurrentKey = CredentialingUtils.GetCredentialProcessTypeKey(_oCurrentProcess.GetValue("cr2_ct2_process"), oConn, oTrans);
  //                  _oCurrentProcessType.SelectByKey(oConn, oTrans);
  //              }

		//		if (!UtilityFunctions.EmptyString(_oData.GetValue("cr1_cst_key")))
		//		{
		//			_oCustomer.CurrentKey = _oData.GetValue("cr1_cst_key");
		//			_oCustomer.SelectByKey(oConn, oTrans);
		//		}

		//		if (!UtilityFunctions.EmptyString(_oData.GetValue("cr1_cst_key")))
		//		{
  //                  if (_oCustomerDetail == null)
  //                  {
  //                      _oCustomerDetail = CredentialingUtils.GetCustomerDetail(_oCustomer, this);
  //                  }
  //                  _oCustomerDetail.CurrentKey = _oData.GetValue("cr1_cst_key");
  //                  _oCustomerDetail.SelectByKey(oConn, oTrans);
		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
		//	finally
		//	{
		//		if (errorClass.Number != 0)
		//		{
		//			Config.LastError = errorClass;
		//		}
		//	}
		//}

		//public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//		if (_oCredentialType.GetValue("ct1_credential") != _oData.GetValue("cr1_ct1_credential") && !UtilityFunctions.EmptyString(_oData.GetValue("cr1_ct1_credential")))
		//		{
		//			_oCredentialType.CurrentKey = GetCredentialTypeKey(_oData.GetValue("cr1_ct1_credential"), oConn, oTrans);
					
		//			if (UtilityFunctions.EmptyString(_oCredentialType.CurrentKey))
		//			{
		//				_oCredentialType.ClearValues();
		//			}
		//			else
		//			{
		//				_oCredentialType.SelectByKey(oConn, oTrans);
		//			}
		//		}

  //              if (_oCurrentProcess.GetValue("cr2_key") != _oData.GetValue("cr1_current_cr2_key"))
  //              {
  //                  _oCurrentProcess.CurrentKey = _oData.GetValue("cr1_current_cr2_key");

  //                  if (UtilityFunctions.EmptyString(_oCurrentProcess.CurrentKey))
  //                  {
  //                      _oCurrentProcess.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oCurrentProcess.SelectByKey(oConn, oTrans);
  //                  }
  //              }

  //              if (_oCurrentProcessType.GetValue("ct2_process") != _oCurrentProcess.GetValue("cr2_ct2_process") && !UtilityFunctions.EmptyString(_oCurrentProcess.GetValue("cr2_ct2_process")))
  //              {
  //                  _oCurrentProcessType.CurrentKey = CredentialingUtils.GetCredentialProcessTypeKey(_oCurrentProcess.GetValue("cr2_ct2_process"), oConn, oTrans);

  //                  if (UtilityFunctions.EmptyString(_oCurrentProcessType.CurrentKey))
  //                  {
  //                      _oCurrentProcessType.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oCurrentProcessType.SelectByKey(oConn, oTrans);
  //                  }
  //              }

  //              if (_oCustomer.GetValue("cst_key") != _oData.GetValue("cr1_cst_key"))
		//		{
		//			_oCustomer.CurrentKey = _oData.GetValue("cr1_cst_key");
					
		//			if (UtilityFunctions.EmptyString(_oCustomer.CurrentKey))
		//			{
		//				_oCustomer.ClearValues();
		//			}
		//			else
		//			{
		//				_oCustomer.SelectByKey(oConn, oTrans);
		//			}
		//		}

  //              if (_oCustomerDetail == null && !UtilityFunctions.EmptyString(_oData.GetValue("cr1_cst_key")))
  //              {
  //                  if (_oCustomerDetail == null)
  //                  {
  //                      _oCustomerDetail = CredentialingUtils.GetCustomerDetail(_oCustomer, this);
  //                  }
  //                  _oCustomerDetail.CurrentKey = _oData.GetValue("cr1_cst_key");
  //                  _oCustomerDetail.SelectByKey(oConn, oTrans);
  //              }

  //              else if (_oCustomerDetail.GetValue("ind_cst_key") != _oData.GetValue("cr1_cst_key") && _oCustomerDetail.GetValue("org_cst_key") != _oData.GetValue("cr1_cst_key"))
  //              {
  //                  _oCustomerDetail.CurrentKey = _oData.GetValue("cr1_cst_key");

  //                  if (UtilityFunctions.EmptyString(_oCustomerDetail.CurrentKey))
  //                  {
  //                      _oCustomerDetail.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oCustomerDetail.SelectByKey(oConn, oTrans);
  //                  }
  //              }
  //          }

		//	catch (Exception e)
		//	{
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}

		//	finally
		//	{
		//		if (errorClass.Number != 0)
		//		{
		//			Config.LastError = errorClass;
		//		}
		//	}
		//}

  //      public override void ClearKeys()
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              if (!UtilityFunctions.EmptyString(CurrentKey))
  //              {
  //                  CurrentKey = "";
  //              }
  //              else
  //              {
  //                  _oData.SetValue("cr1_key", "");
  //                  _oCredentialType.SetValue("ct1_key", "");
  //              }
  //          }

  //          catch (Exception e)
  //          {
  //              errorClass.Number = -1;
  //              errorClass.Message = e.Message;
  //          }
  //          finally
  //          {
  //              if (errorClass.Number != 0)
  //              {
  //                  Config.LastError = errorClass;
  //              }
  //          }
  //      }

  //      public void SetStatus(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          SetValue("cr1_status", strStatus);
  //          SetValue("cr1_status_change_date", System.DateTime.Now.ToString());
  //          SetValue("cr1_status_change_user", Config.CurrentUserName);

  //          GetDataObject("cr1").Update(oConn, oTrans);

  //          Log(oConn, oTrans);
  //      }

  //      public virtual void Log(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass oErr = new ErrorClass();

  //          if (!UtilityFunctions.EmptyString(GetValue("ct1_log_stored_procedure")))
  //          {
  //              oErr = DataUtils.ExecuteSql("exec " + GetValue("ct1_log_stored_procedure") + " @cr1_key = '" + GetValue("cr1_key") + "', @user = '" + Config.CurrentUserName + "'", oConn, oTrans);
  //          }
  //          else
  //          {
  //              oErr = DataUtils.ExecuteSql("exec ags_log_credential @cr1_key = '" + GetValue("cr1_key") + "', @user = '" + Config.CurrentUserName + "'", oConn, oTrans);
  //          } 
  //      }

		//public string GetCredentialTypeKey(string strCredentialType, NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	string strSQL = "SELECT ct1_key FROM ags_credential_type (NOLOCK)"
		//			+ " WHERE ct1_credential = '" + strCredentialType + "'"
		//			+ " AND ct1_delete_flag = 0";
			
		//	DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
			
		//	string strCredentialTypeKey = null;
			
		//	if(oDS.Tables[0].Rows.Count == 1)
		//	{
		//		strCredentialTypeKey = oDS.Tables[0].Rows[0]["ct1_key"].ToString();
		//	}
			
		//	else
		//	{
		//		throw new Exception("Invalid Credential Type");
		//	}
			
		//	return strCredentialTypeKey;
		//}

  //      public CredentialProcess GetPriorCredentialProcess(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          CredentialProcess oPriorProcess = null;

  //          string strSQL = "SELECT TOP 1 cr2_key"
  //                          + " FROM ags_credential_process (NOLOCK)"
  //                          + " WHERE cr2_cr1_key = '" + GetValue("cr1_key") + "'"
  //                          + " AND cr2_key <> '" + GetValue("cr1_current_cr2_key") + "'"
  //                          + " ORDER BY cr2_due_date DESC";

  //          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          if (oDS.Tables[0].Rows.Count > 0)
  //          {
  //              string strProcessKey = oDS.Tables[0].Rows[0]["cr2_key"].ToString();

  //              oPriorProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
  //              oPriorProcess.CurrentKey = strProcessKey;
  //              oPriorProcess.SelectByKey(oConn, oTrans);
  //          }

  //          return oPriorProcess;
  //      }

  //      public CredentialProcess GetCurrentProcess(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          CredentialProcess oProcess = CredentialingUtils.GetCredentialProcessByType(GetValue("cr1_ct2_current_process"));
  //          oProcess.CurrentKey = GetValue("cr1_current_cr2_key");
  //          oProcess.SelectByKey(oConn, oTrans);
  //          return (oProcess);
  //      }

  //      public void RemoveDesignation(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          if (!UtilityFunctions.EmptyString(GetValue("cr1_valid_thru_date")))
  //          {
  //              SetValue("cr1_valid_thru_date", GetValue("cr2_termination_date"));
  //          }

  //          // remove designation
  //          CO_Individual oIndividual = (CO_Individual)DataUtils.InstantiateFacadeObject("Individual");
  //          oIndividual.CurrentKey = GetValue("cst_key");
  //          oIndividual.SelectByKey(oConn, oTrans);

  //          // 11/23/16 spruce US5592: Remove designation when Credential is terminated 
  //          string strDesignation = oIndividual.GetValue("ind_designation");
  //          strDesignation = strDesignation.Replace(", " + GetValue("ct1_designation"), "");
  //          strDesignation = strDesignation.Replace(GetValue("ct1_designation"), "");
  //          oIndividual.SetValue("ind_designation", strDesignation);

  //          oIndividual.Update(oConn, oTrans);
  //      }
    }
}
