using ags.credentialing;


namespace ags.requirements
{
    class DocumentRequirement : CredentialRequirement
	{
  //      public DocumentRequirement()
		//{
  //          _oCredentialRequirementData = new DynamicData("co_document", "co_document_ext", "doc", "doc", "doc_key", "doc_key_ext", "doc_delete_flag", "Document", null, null, null, null);
  //          AddDataObject("doc", _oCredentialRequirementData, "Document");
  //      }

  //      public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans)
  //      {
  //          ErrorClass errorClass = new ErrorClass();

  //          errorClass = base.Init(oConn, oTrans);
  //          Eval(oConn, oTrans);
  //          return errorClass;
  //      }

		//public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans)
		//{
		//	ErrorClass errorClass = new ErrorClass();

  //          if (!UtilityFunctions.EmptyString(GetValue("cr3_data_key")) && GetValue("ct6_state") == "UNLOCKED" && !UtilityFunctions.EmptyString(GetValue("ct3_cr3_complete_status")))
  //          {
  //              SetValue("cr3_status", GetValue("ct3_cr3_complete_status"));
  //          }

		//	return errorClass;
		//}

  //      public static void PopulateDocumentCode(PageClass oPage, string strCategory)
  //      {
  //          if (oPage.bIsFirstTime)
  //          {
  //              FacadeClass oDocument = oPage.oFacadeObject;

  //              string strCustomerKey = Config.IsIweb ? oDocument.GetValue("csd_cst_key").ToString() : Config.Session["CustomerKey"].ToString();

  //              NfDbConnection oConn = DataUtils.GetConnection();
  //              NfDbTransaction oTrans = oConn.BeginTransaction();

  //              string strDocNumber = string.Empty;

  //              string strSQL = "exec ags_get_next_document_number @cst_key='" + strCustomerKey + "', @category='" + strCategory + "'";
  //              DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //              if (oDS.Tables[0].Rows.Count == 1)
  //              {
  //                  strDocNumber = oDS.Tables[0].Rows[0]["doc_number"].ToString();
  //              }
  //              else
  //              {
  //                  strDocNumber = strCustomerKey;
  //              }

  //              oDocument.SetValue("doc_number", strDocNumber);
  //              oDocument.SetValue("doc_category", strCategory);

  //              oTrans.Commit();
  //              oConn.Close();
  //          }
  //      }


  //      [Obsolete("Depricated method, do not use", true)]
  //      public static void UpdateDocumentRequirement(PageClass oPage, string strCategory, string strCustomerKey, string strRequirementKey, string strDocKeyField)
  //      {

  //          // Deprecated code, do not use.
  //          // this has been replaced by workflow and other triggers that set this value elsewhere in the cred engine. - rg 11/16

  //          FacadeClass oDocument = oPage.oFacadeObject;

  //          NfDbConnection oConn = DataUtils.GetConnection();
  //          NfDbTransaction oTrans = oConn.BeginTransaction();

  //          //string strDocNumber = string.Empty;

  //          //string strSQL = "exec ags_get_next_document_number @cst_key='" + strCustomerKey + "', @category='" + strCategory + "'";
  //          //DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

  //          //if (oDS.Tables[0].Rows.Count == 1)
  //          //{
  //          //    strDocNumber = oDS.Tables[0].Rows[0]["doc_number"].ToString();
  //          //}
  //          //else
  //          //{
  //          //    strDocNumber = strCustomerKey;
  //          //}

  //          //oDocument.SetValue("doc_number", strDocNumber);
  //          //oDocument.SetValue("doc_category", strCategory);
  //          //oDocument.GetDataObject("doc").Update(oConn, oTrans);

  //          DynamicData oCustomerDocument = new DynamicData("co_customer_x_document", "co_customer_x_document_ext", "csd", "csd", "csd_key", "csd_key_ext", "csd_delete_flag", "CustomerDocument", null);
  //          oCustomerDocument.SetValue("csd_cst_key", strCustomerKey);
  //          oCustomerDocument.SetValue("csd_doc_key", oDocument.GetValue("doc_key"));
  //          oCustomerDocument.Insert(oConn, oTrans);

  //          CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
  //          oRequirement.SetValue(strDocKeyField, oDocument.GetValue("doc_key"));
  //          oRequirement.LoadRelatedData(oConn, oTrans);
  //          oRequirement.Update(oConn, oTrans);

  //          oTrans.Commit();
  //          oConn.Close();
  //      }
        
    }
}
