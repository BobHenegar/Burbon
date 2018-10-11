using System;
using System.Collections;
using System.Data;
using System.Linq;

namespace ags.credentialing {
    public class CredentialProcess  {
		//public DynamicData _oData;
		//public DynamicData _oCredentialProcessType;
		//public DynamicData _oCredential;
  //      public DynamicData _oCredentialType;
  //      public DynamicData _oCustomer;
  //      public DynamicData _oCustomerDetail;
  //      public DynamicData _oCredentialStatusType;

  //      public CredentialProcess() {
		//	_oData = new DynamicData("ags_credential_process", "ags_credential_process_ext", "cr2", "cr2", "cr2_key", "cr2_key_ext", "cr2_delete_flag", "CredentialProcess", null, null, null, null);
		//	AddDataObject("cr2", _oData, "CredentialProcess");

		//	_oCredentialProcessType = new DynamicData("ags_credential_process_type", "ags_credential_process_type_ext", "ct2", "ct2", "ct2_key", "ct2_key_ext", "ct2_delete_flag", "CredentialProcessType", null, null, null, null);
		//	AddDataObject("ct2", _oCredentialProcessType, "CredentialProcessType");

		//	_oCredential = new DynamicData("ags_credential", "ags_credential_ext", "cr1", "cr1", "cr1_key", "cr1_key_ext", "cr1_delete_flag", "Credential", null, null, null, null);
		//	AddDataObject("cr1", _oCredential, "Credential");

  //          _oCredentialType = new DynamicData("ags_credential_type", "ags_credential_type_ext", "ct1", "ct1", "ct1_key", "ct1_key_ext", "ct1_delete_flag", "CredentialType", null, null, null, null);
  //          AddDataObject("ct1", _oCredentialType, "CredentialType");

		//	_oCustomer = new DynamicData("co_customer", "co_customer_ext", "cst", "cst", "cst_key", "cst_key_ext", "cst_delete_flag", "Customer", null, null, null, null);
		//	AddDataObject("cst", _oCustomer, "Customer");

  //          _oCredentialStatusType = new DynamicData("ags_credential_status_type", "ags_credential_status_type_ext", "ct6", "ct6", "ct6_key", "ct6_key_ext", "ct6_delete_flag", "Status", null, null, null, null);
  //          AddDataObject("ct6", _oCredentialStatusType, "Status");

		//	HasProfilePage = true;
		//	SupportsCorrespondence = true;
		//}

  //      public override void BeforeSave() {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//	}
  //          catch {
		//	}
  //          finally {
		//	}
		//}

  //      public override ErrorClass Insert(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//		Guid guid = Guid.NewGuid();
		//		_oData.SetValue("cr2_key", guid.ToString());
		//		_oData.CurrentKey = guid.ToString();
		//		CurrentKey = guid.ToString();

  //              if (!UtilityFunctions.EmptyString(GetValue("ct2_cr2_start_status")) && UtilityFunctions.EmptyString(GetValue("cr2_status"))) {
  //                  SetStatus(GetValue("ct2_cr2_start_status"), oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(GetValue("ct2_cr1_start_status")) && GetOriginalValue("cr1_status") == GetValue("cr1_status")) {
  //                  SetCredentialStatus(GetValue("ct2_cr1_start_status"), oConn, oTrans);
		//		}

  //              if (errorClass.Number == 0) {
		//			errorClass = _oData.Insert(oConn, oTrans);
		//		}

  //              if (errorClass.Number == 0) {
		//			LoadRelatedData(oConn, oTrans);
		//		}

  //              if (errorClass.Number == 0) {
		//			Init(oConn, oTrans);
		//		}

  //              if (errorClass.Number == 0) {
  //                  if (UtilityFunctions.EmptyString(GetValue("cr2_start_date")))
  //                  {
  //                      if (!UtilityFunctions.EmptyString(GetValue("ct2_calculate_start_date_sp")))
  //                      {
  //                          string strStartDate = CredentialingUtils.CalculatedProcessDates(GetValue("ct2_calculate_start_date_sp"), GetValue("cr2_key"), oConn, oTrans);
  //                          SetValue("cr2_start_date", strStartDate);
  //                      }

  //                      if (UtilityFunctions.EmptyString(GetValue("cr2_start_date")))
  //                      {
  //                          string strSQL = "SELECT cr2_start_date = DATEADD(DD, 1, MAX(cr2_due_date))"
  //                                          + " FROM ags_credential_process (NOLOCK)"
  //                                          + " WHERE cr2_cst_key = '" + GetValue("cr2_cst_key") + "'"
  //                                          + " AND cr2_ct2_process = '" + GetValue("cr2_ct2_process") + "'"
  //                                          + " AND cr2_key <> '" + GetValue("cr2_key") + "'"
  //                                          + " GROUP BY cr2_cst_key";

  //                          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //                          if(oDS.Tables[0].Rows.Count == 1)
  //                          {
  //                              SetValue("cr2_start_date", oDS.Tables[0].Rows[0]["cr2_start_date"].ToString());
  //                          }

  //                      }

  //                      if (UtilityFunctions.EmptyString(GetValue("cr2_start_date")))
  //                      {
  //                          SetValue("cr2_start_date", System.DateTime.Now.ToString());
  //                      }
  //                  }

  //                  if (UtilityFunctions.EmptyString(GetValue("cr2_due_date")))
  //                  {
  //                      if (!UtilityFunctions.EmptyString(GetValue("ct2_calculate_due_date_sp")))
  //                      {
  //                          string strDueDate = CredentialingUtils.CalculatedProcessDates(GetValue("ct2_calculate_due_date_sp"), GetValue("cr2_key"), oConn, oTrans);
  //                          SetValue("cr2_due_date", strDueDate);
  //                      }

  //                      else if (!UtilityFunctions.EmptyString(GetValue("cr1_valid_thru_date")))
  //                      {
  //                          DateTime dtCredentialValidThruDate = Convert.ToDateTime(GetValue("cr1_valid_thru_date"));
  //                          SetValue("cr2_start_date", dtCredentialValidThruDate.AddDays(1).ToString());
  //                          SetValue("cr2_due_date", CredentialingUtils.CalculateDueDate(dtCredentialValidThruDate, GetValue("ct2_due_date_number"), GetValue("ct2_due_date_unit"), GetValue("ct2_due_date_rounding")));
  //                      }

  //                      else if (GetValue("ct1_sync_flag") == "1")
  //                      {
  //                          SyncCredential(oConn, oTrans);
  //                      }

  //                      else if(!UtilityFunctions.EmptyString(GetValue("ct2_due_date_number")))
  //                      {
  //                          SetValue("cr2_due_date", CredentialingUtils.CalculateDueDate(System.DateTime.Now, GetValue("ct2_due_date_number"), GetValue("ct2_due_date_unit"), GetValue("ct2_due_date_rounding")));
  //                      }

  //                      else
  //                      {
  //                          SetValue("cr2_due_date", System.DateTime.Now.ToString());
  //                      }
  //                  }
                    
  //                  _oData.Update(oConn, oTrans);
  //              }

  //              if (errorClass.Number == 0) {
		//			// A CredentialRequirementObject instance is created for each CredentailRequirementType associated to the CredentialProcessType.
		//			string strSQL = "select ct5_ct3_key"
		//				+ " from ags_credential_process_type_x_credential_requirement_type (nolock)"
		//				+ " where ct5_ct2_key = '" + _oCredentialProcessType.CurrentKey + "'"
  //                      + " and (ct5_start_date is null or ct5_start_date <= getdate())"
  //                      + " and (ct5_end_date is null or ct5_end_date > getdate())"
		//				+ " order by ct5_order";

		//			DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //                  for (int i = 0; i < oDS.Tables[0].Rows.Count; i++) {
  //                      if (errorClass.Number == 0) {
		//					string strCredentialRequirementTypeKey = oDS.Tables[0].Rows[i]["ct5_ct3_key"].ToString();
		//					string strCredentialRequirementType = CredentialingUtils.GetCredentialRequirementType(strCredentialRequirementTypeKey, oConn, oTrans);

		//					CredentialRequirement oCredentialRequirement = CredentialingUtils.GetCredentialRequirement(strCredentialRequirementType); ;

  //                          if (oCredentialRequirement != null) {
		//						oCredentialRequirement.SetValue("cr3_cr2_key", CurrentKey);
		//						oCredentialRequirement.SetValue("cr3_ct3_requirement", strCredentialRequirementType);
		//						oCredentialRequirement.SetValue("cr3_cst_key", _oCustomer.GetValue("cst_key"));

		//						oCredentialRequirement.LoadRelatedData(oConn, oTrans);
		//						errorClass = oCredentialRequirement.Insert(oConn, oTrans);
		//					}
		//				}
		//			}
		//		}

  //              SetValue("cr1_current_cr2_key", GetValue("cr2_key"));
  //              GetDataObject("cr1").Update(oConn, oTrans);

  //              if (errorClass.Number == 0)
  //              {
  //                  if (FieldChanged("cr2_status"))
  //                  {
  //                      LoadRelatedData(oConn, oTrans);

  //                      if (!UtilityFunctions.EmptyString(GetValue("ct6_cct_key")))
  //                      {
  //                          DataUtils.SendTemplate(this, GetValue("ct6_cct_key"), GetValue("cst_eml_address_dn"), null, oConn, oTrans);

  //                          Config.LastError = new ErrorClass();
  //                      }
  //                  }
  //              }

  //              WorkflowUtils.CheckRules((FacadeClass)this, WorkflowUtils.Operation.Insert, oConn, oTrans);
		//	}

  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
  //          finally {
  //              Log(oConn, oTrans);
                
  //              if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

  //      public override ErrorClass Update(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

		//	try
		//	{
		//		if(errorClass.Number == 0)
		//		{
		//			if(FieldChanged("cr2_status"))
		//			{
  //                      SetStatus(GetValue("cr2_status"), oConn, oTrans);
		//			}

		//			errorClass = _oData.Update(oConn, oTrans);
		//		}



  //              if (errorClass.Number == 0) {
		//			errorClass = _oCredential.Update(oConn, oTrans);
		//		}

  //              if (errorClass.Number == 0) {
		//			if(UtilityFunctions.EmptyString(GetValue("cr2_completion_date")))
		//			{
		//				// Call the derived CredentialProcess Class Eval() method
		//				errorClass = Eval(oConn, oTrans);

  //                      if (errorClass.Number == 0) {
		//					errorClass = _oData.Update(oConn, oTrans);
		//					errorClass = _oCredential.Update(oConn, oTrans);

  //                          if (errorClass.Number == 0) {
  //                              if (!UtilityFunctions.EmptyString(GetValue("cr2_completion_date")) && UtilityFunctions.EmptyString(GetValue("cr2_termination_date"))) {
  //                                  if (!UtilityFunctions.EmptyString(GetValue("ct2_cr2_completion_status")) && GetOriginalValue("cr2_status") == GetValue("cr2_status")) {
		//								SetStatus(GetValue("ct2_cr2_completion_status"), oConn, oTrans);
		//							}

  //                                  if (!UtilityFunctions.EmptyString(GetValue("ct2_cr1_completion_status")) && GetOriginalValue("cr1_status") == GetValue("cr1_status")) {
  //                                      SetCredentialStatus(GetValue("ct2_cr1_completion_status"), oConn, oTrans);
		//							}

		//							Credential oCredential = CredentialingUtils.GetCredentialByKey(GetValue("cr2_cr1_key"), oConn, oTrans);
		//							errorClass = oCredential.Update(oConn, oTrans);
		//						}
		//					}
		//				}


  //                      if (errorClass.Number == 0 && (UtilityFunctions.EmptyString(GetValue("cr2_completion_date")) && IsReviewReady(oConn, oTrans))) {

  //                          string sql = string.Format("SELECT ct6_code FROM ags_credential_status_type WHERE ct6_state = 'UNDER REVIEW' AND ct6_type_key = '{0}'", _oCredentialStatusType.GetValue("ct6_type_key"));
  //                          string retval = DataUtils.ExecuteScalar(sql, oConn, oTrans).ToString();

  //                          SetStatus(retval, oConn, oTrans);

  //                          string strSQL = string.Format("exec ags_get_submit_prereq_requirements @cr2Key = '{0}'", GetValue("cr2_key"));

  //                          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //                          for (int i = 0; i < oDS.Tables[0].Rows.Count; i++) {

  //                              string strCredentialRequirementTypeKey = oDS.Tables[0].Rows[i]["ct5_ct3_key"].ToString();
  //                              string strCredentialRequirementType = CredentialingUtils.GetCredentialRequirementType(strCredentialRequirementTypeKey, oConn, oTrans);

  //                              CredentialRequirement oCredentialRequirement = CredentialingUtils.GetCredentialRequirement(strCredentialRequirementType);
  //                              oCredentialRequirement.CurrentKey = oDS.Tables[0].Rows[i]["cr3_key"].ToString();
  //                              oCredentialRequirement.SelectByKey();
  //                              oCredentialRequirement.LoadRelatedData(oConn, oTrans);
  //                              oCredentialRequirement.SetUnderReviewStatus(oConn, oTrans);
  //                              oCredentialRequirement.Update();
  //                          }
  //                      }

  //                      if (errorClass.Number == 0) {
  //                          string strSQL = "exec ags_get_unmet_requirements_by_process @cr2_key = '" + GetValue("cr2_key") + "'";

  //                          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //                          for (int i = 0; i < oDS.Tables[0].Rows.Count; i++) {
  //                              if (errorClass.Number == 0) {
  //                                  string strRequirementKey = oDS.Tables[0].Rows[i]["cr3_key"].ToString();

  //                                  CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);

  //                                  oRequirement.SetValue("cr3_extension_date", GetValue("cr2_extension_date"));
  //                                  oRequirement.SetValue("cr3_extension2_date", GetValue("cr2_extension2_date"));
  //                                  errorClass = oRequirement.Update(oConn, oTrans);
  //                              }
  //                          }
  //                      }
		//			}
  //              }

  //              if (errorClass.Number == 0)
  //              {
  //                  if (FieldChanged("cr2_status"))
  //                  {
  //                      LoadRelatedData(oConn, oTrans);

  //                      if (!UtilityFunctions.EmptyString(GetValue("ct6_cct_key")))
  //                      {
  //                          DataUtils.SendTemplate(this, GetValue("ct6_cct_key"), GetValue("cst_eml_address_dn"), null, oConn, oTrans);

  //                          Config.LastError = new ErrorClass();
  //                      }
  //                  }
  //              }

  //              if (ValueChanged("cr2_termination_date")) {
  //                  Credential oCredential = CredentialingUtils.GetCredentialByKey(GetValue("cr2_cr1_key"), oConn, oTrans);
  //                  oCredential.Update(oConn, oTrans);
  //              }

  //              WorkflowUtils.CheckRules((FacadeClass)this, WorkflowUtils.Operation.Update, oConn, oTrans);
		//	}
  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
  //          finally {
  //              if (ObjectChanged()) {
  //                  Log(oConn, oTrans);
  //              }

  //              if (errorClass.Number == 96) {
		//	        // Don't do anything
  //                  errorClass = new ErrorClass();
		//	    }

  //              else if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

  //      public override void AfterSave() {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//	}
  //          catch {
		//	}
  //          finally {
		//	}
		//}

  //      public override void BeforeDelete() {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//	}
  //          catch {
		//	}
  //          finally {
		//	}
		//}

  //      public override ErrorClass Delete(NfDbConnection oConn, NfDbTransaction oTrans) {
  //          if (Config.DeleteMethod == "hard") {
		//		return HardDelete(oConn, oTrans);
		//	}
		//	return SoftDelete(oConn, oTrans);
		//}

  //      public override ErrorClass HardDelete(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
  //              if (errorClass.Number == 0) {
		//			string strSQL = "select ct5_ct3_key"
		//					+ " from ags_credential_process_type_x_credential_requirement_type (nolock)"
		//					+ " where ct5_ct2_key = '" + _oCredentialProcessType.CurrentKey + "'"
		//					+ " order by ct5_order";

		//			DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //                  for (int i = 0; i < oDS.Tables[0].Rows.Count; i++) {
  //                      if (errorClass.Number == 0) {
		//					string strCredentialRequirementTypeKey = oDS.Tables[0].Rows[i]["ct5_ct3_key"].ToString();
		//					string strCredentialRequirementType = CredentialingUtils.GetCredentialRequirementType(strCredentialRequirementTypeKey, oConn, oTrans);
  //                          CredentialRequirement oCredentialRequirement = GetCredentialRequirement(strCredentialRequirementType, oConn, oTrans);

  //                          if (oCredentialRequirement != null) {
		//						errorClass = oCredentialRequirement.Delete(oConn, oTrans);
		//					}
		//				}
		//			}
		//		}
  //              if (errorClass.Number == 0) {
		//			errorClass = _oData.Delete(oConn, oTrans);
		//		}
		//	}

  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
  //          finally {
  //              if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

  //      public override ErrorClass SoftDelete(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//		SetValue("cr2_delete_flag", "1");

		//		errorClass = _oData.Update(oConn, oTrans);
		//	}

  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
  //          finally {
  //              if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}

		//	return errorClass;
		//}

  //      public override void AfterDelete() {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//	}
  //          catch {
		//	}
  //          finally {
		//	}
		//}

  //      public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//		_oData.CurrentKey = CurrentKey;
		//		_oData.SelectByKey(oConn, oTrans);

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("cr2_ct2_process"))) {
		//			string strCredentialProcessTypeKey = CredentialingUtils.GetCredentialProcessTypeKey(_oData.GetValue("cr2_ct2_process"));

		//			_oCredentialProcessType.CurrentKey = strCredentialProcessTypeKey;
		//			_oCredentialProcessType.SelectByKey(oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("cr2_cr1_key"))) {
		//			_oCredential.CurrentKey = _oData.GetValue("cr2_cr1_key");
		//			_oCredential.SelectByKey(oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(_oCredential.GetValue("cr1_ct1_credential")))
  //              {
  //                  string strCredentialTypeKey = CredentialingUtils.GetCredentialTypeKey(_oCredential.GetValue("cr1_ct1_credential"), oConn, oTrans);

  //                  _oCredentialType.CurrentKey = strCredentialTypeKey;
  //                  _oCredentialType.SelectByKey(oConn, oTrans);
  //              }

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("cr2_cst_key")))
  //              {
		//			_oCustomer.CurrentKey = _oData.GetValue("cr2_cst_key");
		//			_oCustomer.SelectByKey(oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("cr2_cst_key"))) {
  //                  if (_oCustomerDetail == null) {
  //                      _oCustomerDetail = CredentialingUtils.GetCustomerDetail(_oCustomer, this);
  //                  }

  //                  _oCustomerDetail.CurrentKey = _oData.GetValue("cr2_cst_key");
  //                  _oCustomerDetail.SelectByKey(oConn, oTrans);
		//		}

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("cr2_status"))) {
  //                  string strStatusTypeKey = CredentialingUtils.GetCredentialStatusTypeKey(GetValue("cr2_status"), GetValue("ct2_key"));

  //                  _oCredentialStatusType.CurrentKey = strStatusTypeKey;
  //                  _oCredentialStatusType.SelectByKey(oConn, oTrans);
		//	}

  //          }
  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
  //          finally {
  //              if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}
		//}

  //      public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
  //              if (_oCredentialProcessType.GetValue("ct2_process") != _oData.GetValue("cr2_ct2_process")) {
		//			_oCredentialProcessType.CurrentKey = CredentialingUtils.GetCredentialProcessTypeKey(_oData.GetValue("cr2_ct2_process"), oConn, oTrans);

  //                  if (UtilityFunctions.EmptyString(_oCredentialProcessType.CurrentKey)) {
		//				_oCredentialProcessType.ClearValues();
		//			}
  //                  else {
		//				_oCredentialProcessType.SelectByKey(oConn, oTrans);
		//			}
		//		}

  //              if (_oCredential.GetValue("cr1_key") != _oData.GetValue("cr2_cr1_key")) {
		//			_oCredential.CurrentKey = _oData.GetValue("cr2_cr1_key");

  //                  if (UtilityFunctions.EmptyString(_oCredential.CurrentKey)) {
		//				_oCredential.ClearValues();
		//			}
  //                  else {
		//				_oCredential.SelectByKey(oConn, oTrans);
		//			}
		//		}

  //              if (_oCredentialType.GetValue("ct1_credential") != _oCredential.GetValue("cr1_ct1_credential"))
  //              {
  //                  _oCredentialType.CurrentKey = CredentialingUtils.GetCredentialTypeKey(_oCredential.GetValue("cr1_ct1_credential"), oConn, oTrans);

  //                  if (UtilityFunctions.EmptyString(_oCredentialType.CurrentKey))
  //                  {
  //                      _oCredentialType.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oCredentialType.SelectByKey(oConn, oTrans);
  //                  }
  //              }

  //              if (_oCustomer.GetValue("cst_key") != _oData.GetValue("cr2_cst_key")) {
		//			_oCustomer.CurrentKey = _oData.GetValue("cr2_cst_key");

  //                  if (UtilityFunctions.EmptyString(_oCustomer.CurrentKey)) {
		//				_oCustomer.ClearValues();
		//			}
  //                  else {
		//				_oCustomer.SelectByKey(oConn, oTrans);
		//			}
		//		}

  //              if (_oCredentialStatusType.GetValue("ct6_code") != _oData.GetValue("cr2_status")) {
  //                  string strCredentialStatusTypeKey = CredentialingUtils.GetCredentialStatusTypeKey(GetValue("cr2_status"), GetValue("ct2_key"));

  //                  _oCredentialStatusType.CurrentKey = strCredentialStatusTypeKey;

  //                  if (UtilityFunctions.EmptyString(_oCredentialStatusType.CurrentKey)) {
  //                      _oCredentialStatusType.ClearValues();
  //                  }
  //                  else {
  //                      _oCredentialStatusType.SelectByKey(oConn, oTrans);
  //                  }
  //              }

  //              if (_oCustomerDetail == null && !UtilityFunctions.EmptyString(_oData.GetValue("cr2_cst_key"))) {
  //                  if (_oCustomerDetail == null) {
  //                      _oCustomerDetail = CredentialingUtils.GetCustomerDetail(_oCustomer, this);
  //                  }
  //                  _oCustomerDetail.CurrentKey = _oData.GetValue("cr2_cst_key");
  //                  _oCustomerDetail.SelectByKey(oConn, oTrans);
  //              }

  //              else if (_oCustomerDetail.GetValue("ind_cst_key") != _oData.GetValue("cr2_cst_key") && _oCustomerDetail.GetValue("org_cst_key") != _oData.GetValue("cr2_cst_key")) {
  //                  _oCustomerDetail.CurrentKey = _oData.GetValue("cr2_cst_key");

  //                  if (UtilityFunctions.EmptyString(_oCustomerDetail.CurrentKey)) {
  //                      _oCustomerDetail.ClearValues();
  //                  }
  //                  else {
  //                      _oCustomerDetail.SelectByKey(oConn, oTrans);
  //                  }
  //              }
		//	}

  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}

  //          finally {
  //              if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}
		//}

  //      public override void ClearKeys() {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
  //              if (!UtilityFunctions.EmptyString(CurrentKey)) {
		//			CurrentKey = "";
		//		}
  //              else {
		//			_oData.SetValue("cr2_key", "");
		//			_oCredentialProcessType.SetValue("ct2_key", "");
		//		}
		//	}

  //          catch (Exception e) {
		//		errorClass.Number = -1;
		//		errorClass.Message = e.Message;
		//	}
  //          finally {
  //              if (errorClass.Number != 0) {
		//			Config.LastError = errorClass;
		//		}
		//	}
		//}

  //      public void SetStatus(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          SetValue("cr2_status", strStatus);
  //          SetValue("cr2_status_change_date", System.DateTime.Now.ToString());
  //          SetValue("cr2_status_change_user", Config.CurrentUserName);

  //          string strCredentialStatusTypeKey = CredentialingUtils.GetCredentialStatusTypeKey(GetValue("cr2_status"), GetValue("ct2_key"));

  //          GetDataObject("ct6").CurrentKey = strCredentialStatusTypeKey;
  //          GetDataObject("ct6").SelectByKey(oConn, oTrans);

  //          // Set the credential status if specified
  //          if (!UtilityFunctions.EmptyString(GetValue("ct6_parent_ct6_code")))
  //          {
  //              Credential oCredential = CredentialingUtils.GetCredentialByKey(GetValue("cr2_cr1_key"), oConn, oTrans);
  //              oCredential.SetStatus(GetValue("ct6_parent_ct6_code"), oConn, oTrans);
  //          }

  //          if (GetValue("ct6_set_process_owner_flag") == "1")
  //          {
  //              SetValue("cr2_owner", GetCredentialProcessOwner(oConn, oTrans));
  //          }

  //          if (GetValue("ct6_set_credential_number_flag") == "1")
  //          {
  //              SetValue("cr1_credential_number", GetNextCredentialNumber(oConn, oTrans));
  //          }

  //          GetDataObject("cr2").Update(oConn, oTrans);

  //          Log(oConn, oTrans);
  //      }

  //      public string GetCredentialProcessOwner(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          string result = String.Empty;

  //          string cr2Key = GetValue("cr2_key");
  //          var ownerSproc = GetValue("ct2_set_owner_sp");

  //          if (!String.IsNullOrEmpty(ownerSproc) && !String.IsNullOrEmpty(cr2Key))
  //          {
  //              string strSQL = String.Format("EXEC {0} '{1}'", ownerSproc, cr2Key);

  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //              if (oDS.Tables[0].Rows.Count == 1)
  //              {
  //                  result = oDS.Tables[0].Rows[0][0].ToString();
  //              }
  //          }
  //          return result;
  //      }

  //      private string GetNextCredentialNumber(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          string strNextNumber = "0";

  //          string credentialNumberSproc = GetValue("ct1_set_credential_number_sp");


  //          if (String.IsNullOrEmpty(credentialNumberSproc))
  //          {
  //              string strSQL = "SELECT next_number = max(convert(int, cr1_credential_number)) + 1 FROM ags_credential (nolock) WHERE ISNUMERIC(cr1_credential_number) = 1;";

  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //              if (oDS.Tables[0].Rows.Count == 1)
  //              {
  //                  strNextNumber = oDS.Tables[0].Rows[0]["next_number"].ToString();
  //              }
  //          }
  //          else
  //          {
  //              string cr1Key = GetValue("cr1_key");
  //              string strSQL = String.Format("EXEC {0} '{1}'", credentialNumberSproc, cr1Key);

  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //              if (oDS.Tables[0].Rows.Count == 1)
  //              {
  //                  strNextNumber = oDS.Tables[0].Rows[0][0].ToString();
  //              }
  //          }

  //          return strNextNumber;
  //      }

  //      public void SetCredentialStatus(string strStatus, NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          Credential oCredential = CredentialingUtils.GetCredentialByKey(GetValue("cr2_cr1_key"), oConn, oTrans);
  //          oCredential.SetStatus(strStatus, oConn, oTrans);
  //      }

  //      public string GetCredentialRequirementKey(string strCredentialRequirementType, NfDbConnection oConn, NfDbTransaction oTrans) {
		//	string strCredentialProcessKey = CurrentKey;

		//	string strSQL = "select cr3_key"
		//		+ " from ags_credential_requirement (nolock)"
		//		+ " where cr3_cr2_key = '" + CurrentKey + "'"
		//		+ " and cr3_ct3_requirement = '" + strCredentialRequirementType + "'";

		//	DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          if (oDS.Tables[0].Rows.Count == 1) {
		//		return oDS.Tables[0].Rows[0]["cr3_key"].ToString();
		//	}

		//	return null;
		//}

  //      public CredentialRequirement GetCredentialRequirement(string strCredentialRequirementType, NfDbConnection oConn, NfDbTransaction oTrans) {
  //          CredentialRequirement oCredentialRequirement = CredentialingUtils.GetCredentialRequirement(strCredentialRequirementType);

  //          string strCredentialRequirementKey = GetCredentialRequirementKey(strCredentialRequirementType, oConn, oTrans);

  //          if (strCredentialRequirementKey != null) {
  //              oCredentialRequirement.CurrentKey = strCredentialRequirementKey;
  //              oCredentialRequirement.SelectByKey(oConn, oTrans);
  //          }

  //          return oCredentialRequirement;
  //      }

  //      public ArrayList GetCredentialRequirementsArray(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ArrayList requirements = new ArrayList();

		//	string strSQL = "select cr3_key, cr3_ct3_requirement, ct5_order"
		//		+ " from ags_credential_process (nolock)"
		//		+ " join ags_credential_process_type (NOLOCK) ON cr2_ct2_process = ct2_process"
		//		+ " join ags_credential_requirement (nolock) on cr2_key = cr3_cr2_key"
		//		+ " join dbo.ags_credential_requirement_type (NOLOCK) ON cr3_ct3_requirement = ct3_requirement"
		//		+ " join ags_credential_process_type_x_credential_requirement_type (nolock) on ct2_key = ct5_ct2_key and ct3_key = ct5_ct3_key"
		//		+ " where cr2_key = '" + CurrentKey + "'"
		//		+ " order by ct5_order";

		//	DataSet oDS = DataUtils.GetDataSet(strSQL);

  //          if (oDS.Tables[0].Rows.Count > 0) {
  //              for (int i = 0; i < oDS.Tables[0].Rows.Count; i++) {
		//			CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(oDS.Tables[0].Rows[i]["cr3_key"].ToString(), oConn, oTrans);
		//			requirements.Add(oRequirement);
		//		}
		//	}

		//	return requirements;
		//}

  //      public ArrayList GetCredentialRequirements() {
  //          ArrayList requirements = new ArrayList();

  //          NfDbConnection oConn = DataUtils.GetConnection();
  //          NfDbTransaction oTrans = oConn.BeginTransaction();

  //          requirements = GetCredentialRequirements(oConn, oTrans);

  //          oTrans.Commit();
  //          oConn.Close();

  //          return requirements;
  //      }

  //      public ArrayList GetCredentialRequirements(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ArrayList requirements = new ArrayList();

  //          // DJB 2017/04/06 removing type tables since end date on process x requirement has been depreciated, and this will exclude requirements added via switch (that don't have a ct5 record)
  //          string strSQL = "select cr3_key"
  //                          + " from ags_credential_process (nolock)"
  //                          //+ " join ags_credential_process_type (nolock) on cr2_ct2_process = ct2_process"
  //                          + " join ags_credential_requirement (nolock) on cr2_key = cr3_cr2_key"
  //                          //+ " join ags_credential_requirement_type (nolock) on cr3_ct3_requirement = ct3_requirement"
  //                          //+ " join ags_credential_process_type_x_credential_requirement_type (nolock) on ct2_key = ct5_ct2_key and ct3_key = ct5_ct3_key"
  //                          + " where cr2_key = '" + GetValue("cr2_key") + "'";
  //                           // 4/30/14 sspruce: For existing processes we should always load the requirements associated to them even if they have since been end dated.
  //                           //+ " and (ct5_end_date is null or ct5_end_date > getdate())"
		//		            //+ " order by ct5_order";

		//	DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          if (oDS.Tables[0].Rows.Count > 0) {
  //              for (int i = 0; i < oDS.Tables[0].Rows.Count; i++) {
		//			CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(oDS.Tables[0].Rows[i]["cr3_key"].ToString(), oConn, oTrans);
		//			requirements.Add(oRequirement);
		//		}
		//	}

		//	return requirements;
		//}

  //      public virtual ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans) {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              errorClass = _oData.Update(oConn, oTrans);
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

  //          return errorClass;
  //      }

  //      public virtual ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans) {
		//	ErrorClass errorClass = new ErrorClass();

  //          try {
		//		bool bAllRequirementsMet = true;

		//		ArrayList requirements = GetCredentialRequirements(oConn, oTrans);

  //              for (int i = 0; i < requirements.Count; i++) {
		//			CredentialRequirement oRequirement = (CredentialRequirement)requirements[i];

  //                  if (UtilityFunctions.EmptyString(oRequirement.GetValue("cr3_met_date"))) {
		//				oRequirement.Update(oConn, oTrans);

  //                      if (oRequirement.ObjectChanged()) {
		//			        oRequirement.Log(oConn, oTrans);
		//			    }

  //                      if (UtilityFunctions.EmptyString(oRequirement.GetValue("cr3_met_date"))) {
		//					bAllRequirementsMet = false;
		//				}
		//			}
		//		}

  //              if (bAllRequirementsMet == true) {
		//			SetValue("cr2_completion_date", System.DateTime.Now.ToString());
		//		}
  //              else if (string.IsNullOrWhiteSpace(GetValue("cr2_termination_date")) && string.IsNullOrWhiteSpace(GetValue("cr2_completion_date")))
  //              {
  //                  if (requirements.Cast<CredentialRequirement>().Any(oRequirement => oRequirement.GetValue("ct6_state").ToLower().Equals("rejected")))
  //                  {
  //                      SetValue("cr2_termination_date", DateTime.Now.ToString());
  //                      SetStatus("Closed", oConn, oTrans);
  //                      // Commeting out cr2_note statement since Rejected state might not always be caused by too many failed exams.
  //                      //SetValue("cr2_note", string.Format("{0}: Applicant has no more exam attempts remaining and must reapply. {1}", DateTime.Now, GetValue("cr2_note")));
  //                  }
  //              }
		//	}
  //          catch (Exception ex) {
		//		errorClass.Message = ex.Message;
		//	}

		//	return errorClass;
		//}

  //      public virtual void Log(NfDbConnection oConn, NfDbTransaction oTrans) {
  //          if (!UtilityFunctions.EmptyString(GetValue("ct2_log_stored_procedure"))) {
  //              DataUtils.ExecuteSql("exec " + GetValue("ct2_log_stored_procedure") + " @cr2_key = '" + GetValue("cr2_key") + "', @user = '" + Config.CurrentUserName + "'", oConn, oTrans);
  //          }
  //          else {
  //              DataUtils.ExecuteSql("exec ags_log_credential_process @cr2_key = '" + GetValue("cr2_key") + "', @user = '" + Config.CurrentUserName + "'", oConn, oTrans);
  //          }
  //      }

  //      public virtual void Submit(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          SetValue("cr2_submit_date", System.DateTime.Now.ToString());

  //          string strSQL = string.Format("exec ags_get_review_prereqs_requirements @cr2_key = '{0}'", GetValue("cr2_key"));

  //          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          for (int i = 0; i < oDS.Tables[0].Rows.Count; i++)
  //          {
  //              string strRequirementKey = oDS.Tables[0].Rows[i]["cr3_key"].ToString();

  //              CredentialRequirement oCredentialRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
  //              oCredentialRequirement.SetUnderReviewStatus(oConn, oTrans);
  //              oCredentialRequirement.Update(oConn, oTrans);
  //          }

  //          if (!UtilityFunctions.EmptyString(GetValue("ct2_cr2_submitted_status")))
  //          {
  //              SetStatus(GetValue("ct2_cr2_submitted_status"), oConn, oTrans);
  //          }

  //          _oData.Update(oConn, oTrans);
  //      }

  //      private bool IsReviewReady(NfDbConnection conn, NfDbTransaction trans) {

  //          bool result = false;
  //          string sql = string.Format("exec ags_get_review_prereqs_not_completed_count @cr2_key = '{0}'", GetValue("cr2_key"));
  //          var retval = DataUtils.ExecuteScalar(sql, conn, trans).ToString();

  //          if (retval == "0")
  //              result = true;

  //          return result;
  //      }

  //      public void SyncCredential(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          string strSQL = "select top 1 cr1_valid_thru_date, cr1_current_cr2_key"
  //              + " from ags_credential (nolock)"
  //              + " join ags_credential_type (nolock) on cr1_ct1_credential = ct1_credential"
  //              + " where cr1_cst_key = '" + GetValue("cr1_cst_key") + "'"
  //              + " and cr1_valid_thru_date > getdate()";

  //          if(!UtilityFunctions.EmptyString(GetValue("ct1_sync_ct1_key")))
  //          {
  //              strSQL = strSQL + " and ct1_key = '" + GetValue("ct1_sync_ct1_key") + "'";
  //          }

  //          strSQL = strSQL + " order by cr1_valid_thru_date desc";

  //          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          if(oDS.Tables[0].Rows.Count == 1)
  //          {
  //              SetValue("cr2_due_date", oDS.Tables[0].Rows[0]["cr1_valid_thru_date"].ToString());
  //              SetValue("cr2_cr2_key", oDS.Tables[0].Rows[0]["cr1_current_cr2_key"].ToString());
  //          }
  //      }
    }
}
