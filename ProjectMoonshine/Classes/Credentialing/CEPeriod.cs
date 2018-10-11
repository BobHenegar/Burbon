namespace ags.credentialing
{
    public class CEPeriod 
    {
  //      public DynamicData _oData;
  //      public DynamicData _oCustomer;
  //      public DynamicData _oIndividual;

		//public CEPeriod()
  //      {
		//	_oData = new DynamicData("ags_ce_period", "ags_ce_period_ext", "ce2", "ce2", "ce2_key", "ce2_key_ext", "ce2_delete_flag", "CEPeriod", null, null, null, null);
  //          AddDataObject("ce2", _oData, "CEPeriod");

  //          _oCustomer = new DynamicData("co_customer", "co_customer_ext", "cst", "cst", "cst_key", "cst_key_ext", "cst_delete_flag", "Customer", null, null, null, null);
  //          AddDataObject("cst", _oCustomer, "Customer");

  //          _oIndividual = new DynamicData("co_individual", "co_individual_ext", "ind", "ind", "ind_cst_key", "ind_cst_key_ext", "ind_delete_flag", "Individual", null, null, null, null);
  //          AddDataObject("ind", _oIndividual, "Individual");

  //          HasProfilePage = true;
  //          SupportsCorrespondence = true;
  //      }

  //      public override void BeforeSave()
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //          }
  //          catch
  //          {
  //          }
  //          finally
  //          {
  //          }
  //      }

  //      public override ErrorClass Insert(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              Guid guid = Guid.NewGuid();
  //              _oData.SetValue("ce2_key", guid.ToString());
  //              _oData.CurrentKey = guid.ToString();
  //              CurrentKey = guid.ToString();

  //              if (errorClass.Number == 0)
  //              {
  //                  errorClass = _oData.Insert(oConn, oTrans);
  //              }

  //              if (errorClass.Number == 0)
  //              {
  //                  LoadRelatedData(oConn, oTrans);
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

  //          return errorClass;
  //      }

  //      public override ErrorClass Update(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
		//		if (errorClass.Number == 0)
		//		{
		//			errorClass = UpdateCEPeriod(oConn, oTrans);
		//		}

  //              if (errorClass.Number == 0)
  //              {
  //                  errorClass = _oData.Update(oConn, oTrans);
  //              }
                
  //              // Update the CE Requirement associated to this CE Period if there is one.
  //              if(errorClass.Number == 0)
  //              {
		//			string strSQL = "SELECT TOP 1 cr3_key"
		//				+ " FROM ags_credential_requirement (NOLOCK)"
		//				+ " WHERE cr3_data_key = '" + GetValue("ce2_key") + "'"
		//				+ " AND cr3_delete_flag = 0";
						
		//			DataSet oDS = DataUtils.GetDataSet(strSQL);
					
		//			if(oDS.Tables[0].Rows.Count == 1)
		//			{
		//				string strRequirementKey = oDS.Tables[0].Rows[0]["cr3_key"].ToString();
						
		//				CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
		//				oRequirement.Update(oConn, oTrans);
						
		//				// Even if the requirement has been met we still need to reevaluate the requirement
		//				// to update balances
		//				if(!UtilityFunctions.EmptyString(oRequirement.GetOriginalValue("cr3_met_date")))
		//				{
		//					oRequirement.Eval(oConn, oTrans);
		//				}
		//			}
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

  //          return errorClass;
  //      }

  //      public override void AfterSave()
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //          }
  //          catch
  //          {
  //          }
  //          finally
  //          {
  //          }
  //      }

  //      public override void BeforeDelete()
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //          }
  //          catch
  //          {
  //          }
  //          finally
  //          {
  //          }
  //      }

  //      public override ErrorClass Delete(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          if (Config.DeleteMethod == "hard")
  //          {
  //              return HardDelete(oConn, oTrans);
  //          }
  //          return SoftDelete(oConn, oTrans);
  //      }

  //      public override ErrorClass HardDelete(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              if (errorClass.Number == 0)
  //              {
  //                  errorClass = _oData.Delete(oConn, oTrans);
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

  //          return errorClass;
  //      }

  //      public override ErrorClass SoftDelete(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              SetValue("ce2_delete_flag", "1");

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

  //      public override void AfterDelete()
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //          }
  //          catch
  //          {
  //          }
  //          finally
  //          {
  //          }
  //      }

  //      public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              _oData.CurrentKey = CurrentKey;
  //              _oData.SelectByKey(oConn, oTrans);

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("ce2_cst_key")))
  //              {
  //                  _oCustomer.CurrentKey = _oData.GetValue("ce2_cst_key");
  //                  _oCustomer.SelectByKey(oConn, oTrans);
  //              }

  //              if (!UtilityFunctions.EmptyString(_oData.GetValue("ce2_cst_key")))
  //              {
  //                  _oIndividual.CurrentKey = _oData.GetValue("ce2_cst_key");
  //                  _oIndividual.SelectByKey(oConn, oTrans);
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

  //      public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          try
  //          {
  //              if (_oCustomer.GetValue("cst_key") != _oData.GetValue("ce2_cst_key"))
  //              {
  //                  _oCustomer.CurrentKey = _oData.GetValue("ce2_cst_key");

  //                  if (UtilityFunctions.EmptyString(_oCustomer.CurrentKey))
  //                  {
  //                      _oCustomer.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oCustomer.SelectByKey(oConn, oTrans);
  //                  }
  //              }

  //              if (_oIndividual.GetValue("ind_cst_key") != _oData.GetValue("ce2_cst_key"))
  //              {
  //                  _oIndividual.CurrentKey = _oData.GetValue("ce2_cst_key");

  //                  if (UtilityFunctions.EmptyString(_oIndividual.CurrentKey))
  //                  {
  //                      _oIndividual.ClearValues();
  //                  }
  //                  else
  //                  {
  //                      _oIndividual.SelectByKey(oConn, oTrans);
  //                  }
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

  //      public ErrorClass UpdateCEPeriod(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          string strSQL = "exec ags_get_ce_period_hours '" + GetValue("ce2_key") + "'";

  //          DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          if (oDS.Tables[0].Rows.Count != 1)
  //          {
  //              errorClass.Number = -1;
  //              errorClass.Message = "Could not get CE Period Hours";
  //              return errorClass;
  //          }

  //          // Update Total hours
  //          decimal dCEPeriodTotalReportedHours = 0;
  //          decimal dCEPeriodTotalRequiredHours = 0;
  //          decimal dCEPeriodTotalBalanceHours = 0;

  //          dCEPeriodTotalReportedHours = CommonUtils.GetDecimalValue(oDS.Tables[0].Rows[0]["ceu_total_reported_hours"].ToString());
  //          SetValue("ce2_total_reported_hours", dCEPeriodTotalReportedHours.ToString());

  //          dCEPeriodTotalRequiredHours = CommonUtils.GetDecimalValue(GetValue("ce2_total_required_hours"));
  //          dCEPeriodTotalBalanceHours = dCEPeriodTotalRequiredHours - dCEPeriodTotalReportedHours;
  //          if (dCEPeriodTotalBalanceHours < 0)
  //          {
  //              dCEPeriodTotalBalanceHours = 0;
  //          }
  //          SetValue("ceu_total_balance_hours", dCEPeriodTotalBalanceHours.ToString());

  //          // Update Ethics hours
  //          decimal dCEPeriodEthicsReportedHours = 0;
  //          decimal dCEPeriodEthicsRequiredHours = 0;
  //          decimal dCEPeriodEthicsBalanceHours = 0;

  //          dCEPeriodEthicsReportedHours = CommonUtils.GetDecimalValue(oDS.Tables[0].Rows[0]["ceu_ethics_reported_hours"].ToString());
  //          SetValue("ce2_ethics_reported_hours", dCEPeriodEthicsReportedHours.ToString());

  //          dCEPeriodEthicsRequiredHours = CommonUtils.GetDecimalValue(GetValue("ce2_ethics_required_hours"));
  //          dCEPeriodEthicsBalanceHours = dCEPeriodEthicsRequiredHours - dCEPeriodEthicsReportedHours;
  //          if (dCEPeriodEthicsBalanceHours < 0)
  //          {
  //              dCEPeriodEthicsBalanceHours = 0;
  //          }
  //          SetValue("ce2_ethics_balance_hours", dCEPeriodEthicsBalanceHours.ToString());


  //          _oData.Update(oConn, oTrans);

  //          return errorClass;
  //      }

	}
}
