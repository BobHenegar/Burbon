namespace ags.credentialing
{
    public class CEDetail 
    {
 //       public DynamicData _oData;
 //       public DynamicData _oCEProgram;
 //       public DynamicData _oCEProvider;
 //       public DynamicData _oCustomer;
 //       public DynamicData _oIndividual;
 //       public DynamicData _oOrganization;

	//	public CEDetail()
	//	{
 //           _oData = new DynamicData("ags_ce_detail", "ags_ce_detail_ext", "ce1", "ce1", "ce1_key", "ce1_key_ext", "ce1_delete_flag", "CEDetail", null, null, null, null);
 //           AddDataObject("ce1", _oData, "CEDetail");

 //           _oCEProgram = new DynamicData("ce_course", "ce_course_ext", "cec", "cec", "cec_key", "cec_key_ext", "cec_delete_flag", "CEProgram", null, null, null, null);
 //           AddDataObject("cec", _oCEProgram, "CEProgram");

 //           _oCEProvider = new DynamicData("ce_provider", "ce_provider_ext", "cep", "cep", "cep_key", "cep_key_ext", "cep_delete_flag", "CEProvider", null, null, null, null);
 //           AddDataObject("cep", _oCEProvider, "CEProvider");

	//		_oOrganization = new DynamicData("co_organization", "co_organization_ext", "org", "org", "org_cst_key", "org_cst_key_ext", "org_delete_flag", "Organization", null, null, null, null);
	//		AddDataObject("org", _oOrganization, "Organization");

	//		_oCustomer = new DynamicData("co_customer", "co_customer_ext", "cst", "cst", "cst_key", "cst_key_ext", "cst_delete_flag", "Customer", null, null, null, null);
	//		AddDataObject("cst", _oCustomer, "Customer");

	//		_oIndividual = new DynamicData("co_individual", "co_individual_ext", "ind", "ind", "ind_cst_key", "ind_cst_key_ext", "ind_delete_flag", "Individual", null, null, null, null);
	//		AddDataObject("ind", _oIndividual, "Individual");

	//		HasProfilePage = true;
	//		SupportsCorrespondence = true;
	//	}

 //       public override void BeforeSave()
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
	//		}
 //           catch
 //           {
 //           }
 //           finally
 //           {
 //           }
 //       }

 //       public override ErrorClass Insert(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               Guid guid = Guid.NewGuid();
 //               _oData.SetValue("ce1_key", guid.ToString());
 //               _oData.CurrentKey = guid.ToString();
 //               CurrentKey = guid.ToString();

 //               if (errorClass.Number == 0)
 //               {
 //                   errorClass = _oData.Insert(oConn, oTrans);
 //               }
				
 //               if (errorClass.Number == 0)
 //               {
 //                   LoadRelatedData(oConn, oTrans);
 //               }
 //           }
 //           catch (Exception e)
 //           {
 //               errorClass.Number = -1;
 //               errorClass.Message = e.Message;
 //           }
 //           finally
 //           {
 //               if (errorClass.Number != 0)
 //               {
 //                   Config.LastError = errorClass;
 //               }
 //           }

 //           return errorClass;
 //       }

 //       public override ErrorClass Update(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               if (errorClass.Number == 0)
 //               {
	//				errorClass = _oData.Update(oConn, oTrans);
 //               }
 //           }
 //           catch (Exception e)
 //           {
 //               errorClass.Number = -1;
 //               errorClass.Message = e.Message;
 //           }
 //           finally
 //           {
 //               if (errorClass.Number != 0)
 //               {
 //                   Config.LastError = errorClass;
 //               }
 //           }

 //           return errorClass;
 //       }

 //       public override void AfterSave()
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
	//			NfDbConnection oConn = DataUtils.GetConnection();
	//			NfDbTransaction oTrans = oConn.BeginTransaction();
				
	//			if(UtilityFunctions.EmptyString(GetValue("ce1_ce2_key")))
	//			{
	//				AddCEDetailToCEPeriod(oConn, oTrans);
	//			}
				
	//			else
	//			{
 //                   CEPeriod oCEPeriod = (CEPeriod)DataUtils.InstantiateFacadeObject("AGSCEPeriod");
	//				oCEPeriod.CurrentKey = GetValue("ce1_ce2_key");
	//				oCEPeriod.SelectByKey(oConn, oTrans);

	//				oCEPeriod.Update(oConn, oTrans);
	//			}
				
	//			oTrans.Commit();
	//			oConn.Close();
	//		}
 //           catch
 //           {
 //           }
 //           finally
 //           {
 //           }
 //       }

 //       public override void BeforeDelete()
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //           }
 //           catch
 //           {
 //           }
 //           finally
 //           {
 //           }
 //       }

 //       public override ErrorClass Delete(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           if (Config.DeleteMethod == "hard")
 //           {
 //               return HardDelete(oConn, oTrans);
 //           }
 //           return SoftDelete(oConn, oTrans);
 //       }

 //       public override ErrorClass HardDelete(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               if (errorClass.Number == 0)
 //               {
 //                   errorClass = _oData.Delete(oConn, oTrans);
 //               }
 //           }

 //           catch (Exception e)
 //           {
 //               errorClass.Number = -1;
 //               errorClass.Message = e.Message;
 //           }
 //           finally
 //           {
 //               if (errorClass.Number != 0)
 //               {
 //                   Config.LastError = errorClass;
 //               }
 //           }

 //           return errorClass;
 //       }

 //       public override ErrorClass SoftDelete(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               SetValue("ce1_delete_flag", "1");

 //               errorClass = _oData.Update(oConn, oTrans);
 //           }

 //           catch (Exception e)
 //           {
 //               errorClass.Number = -1;
 //               errorClass.Message = e.Message;
 //           }
 //           finally
 //           {
 //               if (errorClass.Number != 0)
 //               {
 //                   Config.LastError = errorClass;
 //               }
 //           }

 //           return errorClass;
 //       }

 //       public override void AfterDelete()
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               NfDbConnection oConn = DataUtils.GetConnection();
 //               NfDbTransaction oTrans = oConn.BeginTransaction();

 //               CEPeriod oCEPeriod = (CEPeriod)DataUtils.InstantiateFacadeObject("AGSCEPeriod");
 //               oCEPeriod.CurrentKey = GetValue("ce1_ce2_key");
 //               oCEPeriod.SelectByKey(oConn, oTrans);

 //               oCEPeriod.UpdateCEPeriod(oConn, oTrans);
                
 //               oTrans.Commit();
 //               oConn.Close();

 //           }
 //           catch
 //           {
 //           }
 //           finally
 //           {
 //           }
 //       }

 //       public override void SelectByKey(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               _oData.CurrentKey = CurrentKey;
 //               _oData.SelectByKey(oConn, oTrans);

 //               if (!UtilityFunctions.EmptyString(_oData.GetValue("ce1_cec_key")))
 //               {
 //                   _oCEProgram.CurrentKey = _oData.GetValue("ce1_cec_key");
 //                   _oCEProgram.SelectByKey(oConn, oTrans);
 //               }

 //               if (!UtilityFunctions.EmptyString(_oData.GetValue("ce1_cep_key")))
 //               {
 //                   _oCEProvider.CurrentKey = _oData.GetValue("ce1_cep_key");
 //                   _oCEProvider.SelectByKey(oConn, oTrans);
 //               }

	//			if (!UtilityFunctions.EmptyString(_oCEProvider.GetValue("cep_org_cst_key")))
	//			{
	//				_oOrganization.CurrentKey = _oCEProvider.GetValue("cep_org_cst_key");
	//				_oOrganization.SelectByKey(oConn, oTrans);
	//			}

	//			if (!UtilityFunctions.EmptyString(_oData.GetValue("ce1_cst_key")))
 //               {
 //                   _oCustomer.CurrentKey = _oData.GetValue("ce1_cst_key");
 //                   _oCustomer.SelectByKey(oConn, oTrans);
 //               }

 //               if (!UtilityFunctions.EmptyString(_oData.GetValue("ce1_cst_key")))
 //               {
 //                   _oIndividual.CurrentKey = _oData.GetValue("ce1_cst_key");
 //                   _oIndividual.SelectByKey(oConn, oTrans);
 //               }
 //           }
 //           catch (Exception e)
 //           {
 //               errorClass.Number = -1;
 //               errorClass.Message = e.Message;
 //           }
 //           finally
 //           {
 //               if (errorClass.Number != 0)
 //               {
 //                   Config.LastError = errorClass;
 //               }
 //           }
 //       }

 //       public override void LoadRelatedData(NfDbConnection oConn, NfDbTransaction oTrans)
 //       {
 //           ErrorClass errorClass = new ErrorClass();

 //           try
 //           {
 //               if (_oCustomer.GetValue("cst_key") != _oData.GetValue("ce1_cst_key"))
 //               {
 //                   _oCustomer.CurrentKey = _oData.GetValue("ce1_cst_key");

 //                   if (UtilityFunctions.EmptyString(_oCustomer.CurrentKey))
 //                   {
 //                       _oCustomer.ClearValues();
 //                   }
 //                   else
 //                   {
 //                       _oCustomer.SelectByKey(oConn, oTrans);
 //                   }
 //               }

 //               if (_oIndividual.GetValue("ind_cst_key") != _oData.GetValue("ce1_cst_key"))
 //               {
 //                   _oIndividual.CurrentKey = _oData.GetValue("ce1_cst_key");

 //                   if (UtilityFunctions.EmptyString(_oIndividual.CurrentKey))
 //                   {
 //                       _oIndividual.ClearValues();
 //                   }
 //                   else
 //                   {
 //                       _oIndividual.SelectByKey(oConn, oTrans);
 //                   }
 //               }

 //               if (_oCEProgram.GetValue("cec_key") != _oData.GetValue("ce1_cec_key"))
 //               {
 //                   _oCEProgram.CurrentKey = _oData.GetValue("ce1_cec_key");

 //                   if (UtilityFunctions.EmptyString(_oCEProgram.CurrentKey))
 //                   {
 //                       _oCEProgram.ClearValues();
 //                   }
 //                   else
 //                   {
 //                       _oCEProgram.SelectByKey(oConn, oTrans);
 //                   }
 //               }

 //               if (_oCEProvider.GetValue("cep_key") != _oData.GetValue("ce1_cep_key"))
 //               {
 //                   _oCEProvider.CurrentKey = _oData.GetValue("ce1_cep_key");

 //                   if (UtilityFunctions.EmptyString(_oCEProvider.CurrentKey))
 //                   {
 //                       _oCEProvider.ClearValues();
 //                   }
 //                   else
 //                   {
 //                       _oCEProvider.SelectByKey(oConn, oTrans);
 //                   }
 //               }

	//			if (_oOrganization.GetValue("org_cst_key") != _oCEProvider.GetValue("cep_org_cst_key"))
 //               {
	//				_oOrganization.CurrentKey = _oCEProvider.GetValue("cep_org_cst_key");

 //                   if (UtilityFunctions.EmptyString(_oOrganization.CurrentKey))
 //                   {
 //                       _oOrganization.ClearValues();
 //                   }
 //                   else
 //                   {
 //                       _oOrganization.SelectByKey(oConn, oTrans);
 //                   }
 //               }

 //           }

 //           catch (Exception e)
 //           {
 //               errorClass.Number = -1;
 //               errorClass.Message = e.Message;
 //           }

 //           finally
 //           {
 //               if (errorClass.Number != 0)
 //               {
 //                   Config.LastError = errorClass;
 //               }
 //           }
 //       }

	//	public void AddCEDetailToCEPeriod(NfDbConnection oConn, NfDbTransaction oTrans)
	//	{
	//		if (UtilityFunctions.EmptyString(GetValue("ce1_ce2_key")) && !UtilityFunctions.EmptyString(GetValue("ce1_program_date")))
	//		{
	//		}
	//	}		
	}
}
