using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace ags.credentialing
{
    public class CredentialingUtils
    {
        //public static void BeforeSaveUpdateCredentialIndName(PageClass oPage)
        //{
        //    // JAR - allow people to supply their legal first & last name
        //    // JAR - for a specific credential process
        //    // JAR - #US6367
        //    if (oPage.oFacadeObject.FieldChanged("cr1_legal_first_name") ||
        //                    oPage.oFacadeObject.FieldChanged("cr1_legal_last_name"))
        //    {
        //        using (NfDbConnection oConn = DataUtils.GetConnection())
        //        {
        //            using (NfDbTransaction oTrans = oConn.BeginTransaction())
        //            {
        //                oPage.oFacadeObject.GetDataObject("cr1").Update(oConn, oTrans);
        //                oTrans.Commit();
        //            }
        //        }
        //    }
        //}

        //public static Credential CreateCredential(string strCredentialType, string strCredentialProcessType, string strCustomerKey, string strRedirectURL)
        //{
        //    Credential oCredential = (Credential)DataUtils.InstantiateFacadeObject("AGSCredential");

        //    oCredential = CreateCredential(strCredentialType, strCredentialProcessType, strCustomerKey);

        //    PageUtils.Redirect(strRedirectURL, true);

        //    return oCredential;
        //}

        //public static Credential CreateCredential(string strCredentialType, string strCredentialProcessType, string strCustomerKey)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    Credential oCredential = (Credential)DataUtils.InstantiateFacadeObject("AGSCredential");

        //    try
        //    {
        //        oCredential = CreateCredential(strCredentialType, strCredentialProcessType, strCustomerKey, oConn, oTrans);
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

        //        oTrans.Commit();
        //        oConn.Close();
        //    }

        //    return oCredential;
        //}

        //public static Credential CreateCredential(string strCredentialType, string strCredentialProcessType, string strCustomerKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    Credential oCredential = (Credential)DataUtils.InstantiateFacadeObject("AGSCredential");

        //    if (errorClass.Number == 0)
        //    {
        //        oCredential.SetValue("cr1_ct1_credential", strCredentialType);
        //        oCredential.SetValue("cr1_ct2_current_process", strCredentialProcessType);
        //        oCredential.SetValue("cr1_cst_key", strCustomerKey);
        //        errorClass = oCredential.Insert(oConn, oTrans);
        //    }

        //    return oCredential;
        //}

        //public static Credential CreateCredential(string strCredentialType, string strCustomerKey)
        //{
        //    return (CreateCredential(strCredentialType, null, strCustomerKey));
        //}

        //public static Credential GetCredentialByKey(string strCredentialKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{

        //    Credential oBaseCredential = (Credential)DataUtils.InstantiateFacadeObject("AGSCredential");
        //    oBaseCredential.CurrentKey = strCredentialKey;
        //    oBaseCredential.SelectByKey(oConn, oTrans);

        //    oBaseCredential.ObjectKey = "7C419370-0EB9-43BA-9260-79D7A8F6CBC4";
        //    oBaseCredential.bWorkflowEnabled = true;

        //    return (oBaseCredential);
        //}

        //public static string GetCredentialTypeKey(string strCredentialType, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "SELECT ct1_key from ags_credential_type (NOLOCK)"
        //            + " WHERE ct1_credential = '" + strCredentialType + "'"
        //            + " AND ct1_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    string strCredentialTypeKey = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialTypeKey = oDS.Tables[0].Rows[0]["ct1_key"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Type");
        //    }

        //    return strCredentialTypeKey;
        //}

        //public static string GetCredentialType(string strCredentialTypeKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "SELECT ct1_credential from ags_credential_type (NOLOCK)"
        //            + " WHERE ct1_key = '" + strCredentialTypeKey + "'"
        //            + " AND ct1_delete_flag = 0"; ;

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    string strCredentialType = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialType = oDS.Tables[0].Rows[0]["ct1_credential"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Type");
        //    }

        //    return strCredentialType;
        //}

        //public static string GetCredentialProcessTypeKey(string strCredentialProcessType, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "SELECT ct2_key from ags_credential_process_type (NOLOCK)"
        //            + " WHERE ct2_process = '" + strCredentialProcessType + "'"
        //            + " AND ct2_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    string strCredentialProcessTypeKey = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialProcessTypeKey = oDS.Tables[0].Rows[0]["ct2_key"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Process Type");
        //    }

        //    return strCredentialProcessTypeKey;
        //}

        //public static string GetCredentialProcessTypeKey(string strCredentialProcessType)
        //{
        //    string strSQL = "SELECT ct2_key from ags_credential_process_type (NOLOCK)"
        //            + " WHERE ct2_process = '" + strCredentialProcessType + "'"
        //            + " AND ct2_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    string strCredentialProcessTypeKey = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialProcessTypeKey = oDS.Tables[0].Rows[0]["ct2_key"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Process Type");
        //    }

        //    return strCredentialProcessTypeKey;
        //}

        //public static string GetCredentialProcessType(string strCredentialProcessTypeKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "SELECT ct2_process from ags_credential_process_type (NOLOCK)"
        //            + " WHERE ct2_key = '" + strCredentialProcessTypeKey + "'"
        //            + " AND ct2_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    string strCredentialProcessType = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialProcessType = oDS.Tables[0].Rows[0]["ct2_process"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Process Type");
        //    }

        //    return strCredentialProcessType;
        //}

        //public static ArrayList GetCredentialRequirementTypes(string strCredentialProcessType)
        //{
        //    ArrayList requirementTypes = new ArrayList();

        //    string strSQL = "select ct3_requirement"
        //        + " from ags_credential_process_type (nolock)"
        //        + " join ags_credential_process_type_x_credential_requirement_type (nolock) on ct2_key = ct5_ct2_key"
        //        + " join ags_credential_requirement_type (nolock) on ct5_ct3_key = ct3_key"
        //        + " where ct2_process = '" + strCredentialProcessType + "'"
        //        + " and ct2_delete_flag = 0"
        //        + " and ct3_delete_flag = 0"
        //        + " order by ct5_order";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    if (oDS.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < oDS.Tables[0].Rows.Count; i++)
        //        {
        //            requirementTypes.Add(oDS.Tables[0].Rows[i]["ct3_requirement"].ToString());
        //        }
        //    }

        //    return requirementTypes;
        //}

        //public static CredentialProcess GetCredentialProcessByType(string strCredentialProcessType)
        //{
        //    string strCredentialProcessTypeKey = GetCredentialProcessTypeKey(strCredentialProcessType);

        //    DynamicData oCredentialProcessType = new DynamicData("ags_credential_process_type", null, "ct2", null, "ct2_key", null, "ct2_delete_flag", "CredentialProcessType");
        //    oCredentialProcessType.CurrentKey = strCredentialProcessTypeKey;
        //    oCredentialProcessType.SelectByKey();

        //    string strClassAssembly = oCredentialProcessType.GetValue("ct2_class_assembly");
        //    string strClassName = oCredentialProcessType.GetValue("ct2_class_name");

        //    object obj = null;

        //    if (!UtilityFunctions.EmptyString(strClassAssembly) && !UtilityFunctions.EmptyString(strClassName))
        //    {
        //        Assembly oAssembly = Assembly.Load(strClassAssembly);
        //        obj = oAssembly.CreateInstance(strClassName);
        //    }

        //    else
        //    {
        //        obj = new SimpleCredentialProcess();
        //    }

        //    ((CredentialProcess)obj).ObjectKey = "7E929134-89F9-42F5-BDDE-5F5942C1F4C1";
        //    ((CredentialProcess)obj).bWorkflowEnabled = true;

        //    return ((CredentialProcess)obj);
        //}

        //public static CredentialProcess GetCredentialProcessByKey(string strCredentialProcessKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    // 08/28/17 sspruce: US6740 - Modified to instantiate object to enable workflow.
        //    CredentialProcess oBaseProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
        //    oBaseProcess.CurrentKey = strCredentialProcessKey;
        //    oBaseProcess.SelectByKey(oConn, oTrans);

        //    string strCredentialProcessType = oBaseProcess.GetValue("cr2_ct2_process");

        //    CredentialProcess oDerivedProcess = GetCredentialProcessByType(strCredentialProcessType);
        //    oDerivedProcess.CurrentKey = strCredentialProcessKey;
        //    oDerivedProcess.SelectByKey(oConn, oTrans);

        //    return (oDerivedProcess);
        //}

        //public static string GetCredentialRequirementType(string strCredentialRequirementTypeKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "select ct3_requirement from ags_credential_requirement_type (nolock)"
        //            + " where ct3_key = '" + strCredentialRequirementTypeKey + "'"
        //            + " and ct3_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    string strCredentialRequirementType = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialRequirementType = oDS.Tables[0].Rows[0]["ct3_requirement"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Requirement Type");
        //    }

        //    return strCredentialRequirementType;
        //}

        //public static string GetCredentialRequirementTypeKey(string strCredentialRequirementType, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "select ct3_key from ags_credential_requirement_type (nolock)"
        //            + " where ct3_requirement = '" + strCredentialRequirementType + "'"
        //            + " and ct3_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

        //    string strCredentialRequirementTypeKey = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialRequirementTypeKey = oDS.Tables[0].Rows[0]["ct3_key"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Requirement Type");
        //    }

        //    return strCredentialRequirementTypeKey;
        //}

        //public static string GetCredentialRequirementTypeKey(string strCredentialRequirementType)
        //{
        //    string strSQL = "select ct3_key from ags_credential_requirement_type (nolock)"
        //            + " where ct3_requirement = '" + strCredentialRequirementType + "'"
        //            + " and ct3_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    string strCredentialRequirementTypeKey = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialRequirementTypeKey = oDS.Tables[0].Rows[0]["ct3_key"].ToString();
        //    }

        //    else
        //    {
        //        throw new Exception("Invalid Credential Requirement Type");
        //    }

        //    return strCredentialRequirementTypeKey;
        //}

        //public static CredentialRequirement GetCredentialRequirementByKey(string strCredentialRequirementKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    CredentialRequirement oBaseRequirement = (CredentialRequirement)DataUtils.InstantiateFacadeObject("AGSCredentialRequirement");
        //    oBaseRequirement.CurrentKey = strCredentialRequirementKey;
        //    oBaseRequirement.SelectByKey(oConn, oTrans);

        //    string strCredentialRequirementType = oBaseRequirement.GetValue("cr3_ct3_requirement");

        //    CredentialRequirement oDerivedRequirement = GetCredentialRequirement(strCredentialRequirementType);
        //    oDerivedRequirement.CurrentKey = strCredentialRequirementKey;
        //    oDerivedRequirement.SelectByKey(oConn, oTrans);

        //    return (oDerivedRequirement);
        //}

        //public static CredentialRequirement GetCredentialRequirement(string strCredentialRequirementType)
        //{
        //    string strCredentialRequirementTypeKey = GetCredentialRequirementTypeKey(strCredentialRequirementType);

        //    DynamicData oCredentialRequirementType = new DynamicData("ags_credential_requirement_type", null, "ct3", null, "ct3_key", null, "ct3_delete_flag", "CredentialRequirementType");
        //    oCredentialRequirementType.CurrentKey = strCredentialRequirementTypeKey;
        //    oCredentialRequirementType.SelectByKey();

        //    string strObjectKey = oCredentialRequirementType.GetValue("ct3_obj_key");

        //    DynamicData oObjectData = new DynamicData("md_object", null, "obj", null, "obj_key", null, "obj_delete_flag", "Object");
        //    oObjectData.CurrentKey = strObjectKey;
        //    oObjectData.SelectByKey();

        //    string strClassAssembly = oObjectData.GetValue("obj_assembly");
        //    string strClassName = oObjectData.GetValue("obj_typename");

        //    object obj = null;

        //    if (!UtilityFunctions.EmptyString(strClassAssembly) && !UtilityFunctions.EmptyString(strClassName))
        //    {
        //        Assembly oAssembly = Assembly.Load(strClassAssembly);
        //        obj = oAssembly.CreateInstance(strClassName);
        //    }

        //    else
        //    {
        //        obj = new SimpleCredentialRequirement();
        //    }

        //    ((CredentialRequirement)obj).ObjectKey = "B0E1AD32-3DAC-434D-80FF-3F73918270BE";
        //    ((CredentialRequirement)obj).bWorkflowEnabled = true;

        //    return ((CredentialRequirement)obj);
        //}

        //public static string GetCredentialStatusTypeKey(string strStatus, string strTypeKey)
        //{
        //    string strSQL = "select ct6_key from ags_credential_status_type (nolock)"
        //            + " where ct6_code = '" + strStatus + "'"
        //            + " and ct6_type_key = '" + strTypeKey + "'"
        //            + " and ct6_delete_flag = 0";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    string strCredentialStatusTypeKey = null;

        //    if (oDS.Tables[0].Rows.Count == 1)
        //    {
        //        strCredentialStatusTypeKey = oDS.Tables[0].Rows[0]["ct6_key"].ToString();
        //    }

        //    else
        //    {
        //        //throw new Exception("Invalid Credential Status Type");
        //    }

        //    return strCredentialStatusTypeKey;

        //}

        //public static void PopulateSessionWithCredentialRequirements(PageClass oPage)
        //{
        //    string strProcessKey = oPage.oFacadeObject.GetValue("cr1_current_cr2_key");
        //    CredentialProcess oProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
        //    oProcess.CurrentKey = strProcessKey;
        //    oProcess.SelectByKey();

        //    ArrayList requirements = oProcess.GetCredentialRequirements();

        //    for (int i = 0; i < requirements.Count; i++)
        //    {
        //        CredentialRequirement oRequirement = (CredentialRequirement)requirements[i];

        //        if (oRequirement._oCredentialRequirementData != null)
        //        {
        //            string strRequirementObjectType = oRequirement._oCredentialRequirementData.Description;
        //            Config.Session.Add(strRequirementObjectType, oRequirement.CurrentKey);
        //        }
        //    }
        //}

        //public static void PopulateSessionWithCredentialProcess(PageClass oPage)
        //{
        //    string strProcessKey = oPage.oFacadeObject.GetValue("cr1_current_cr2_key");
        //    CredentialProcess oProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
        //    oProcess.CurrentKey = strProcessKey;
        //    oProcess.SelectByKey();

        //    Config.Session.Add("process", oProcess.GetValue("cr2_ct2_process"));
        //}

        //public static void ClearSessionOfCredentialRequirements(PageClass oPage)
        //{
        //    string strProcessKey = oPage.oFacadeObject.GetValue("cr3_cr2_key").ToString();
        //    CredentialProcess oProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
        //    oProcess.CurrentKey = strProcessKey;
        //    oProcess.SelectByKey();

        //    ArrayList requirements = oProcess.GetCredentialRequirements();

        //    for (int i = 0; i < requirements.Count; i++)
        //    {
        //        CredentialRequirement oRequirement = (CredentialRequirement)requirements[i];

        //        if (oRequirement._oCredentialRequirementData != null)
        //        {
        //            string strRequirementObjectType = oRequirement._oCredentialRequirementData.Description;
        //            Config.Session.Remove(strRequirementObjectType);
        //        }
        //    }
        //}

        //public static void ClearSessionofCredentialProcess(PageClass oPage)
        //{
        //    Config.Session.Remove("process");
        //}

        //public static void PopulateCredentialRequirementFromURL(PageClass oPage)
        //{
        //    if (oPage.Request["type"] == null)
        //    {
        //        return;
        //    }

        //    string strRequirementType = oPage.Request["type"].ToString();

        //    PopulateCredentialRequirement(oPage, strRequirementType);
        //}

        //public static void PopulateCredentialRequirement(PageClass oPage, string strRequirementType)
        //{
        //    if (oPage.bIsFirstTime)
        //    {
        //        string strProcessKey = string.Empty; 
                
        //        if(oPage.Request["cr3_cr2_key"] != null)
        //        {
        //            strProcessKey = oPage.Request["cr3_cr2_key"].ToString();
        //        }
        //        else if (oPage.Request["cr2_key"] != null)
        //        {
        //            strProcessKey = oPage.Request["cr2_key"].ToString();
        //        }
        //        else
        //        {
        //            return;
        //        }

        //        bool bPopulateCredentialRequirement = false;

        //        string strObjectName = oPage.oFacadeObject.ObjectName;

        //        // First time loading form based on this requirement type
        //        if (oPage.oFacadeObject.CurrentKey == null)
        //        {
        //            bPopulateCredentialRequirement = true;
        //        }

        //        // Look at any old versions of the object that may be lingering in the session
        //        else if (oPage.Session[strObjectName] != null)
        //        {
        //            FacadeClass oSessionFacadeObject = (FacadeClass)oPage.Session[strObjectName];
        //            if (oSessionFacadeObject.CurrentKey != oPage.oFacadeObject.CurrentKey)
        //            {
        //                bPopulateCredentialRequirement = true;
        //            }
        //        }

        //        // Look if object in session is fo the same requirement type
        //        else if (oPage.Session[strObjectName] != null)
        //        {
        //            FacadeClass oSessionFacadeObject = (FacadeClass)oPage.Session[strObjectName];
        //            if (oSessionFacadeObject.GetValue("ct3_requirement") != oPage.Request["type"])
        //            {
        //                bPopulateCredentialRequirement = true;
        //            }
        //        }

        //        // There have been wierd situations where the CurrentKey is not a GUID but the ObjectClassName
        //        // So check to see if CurrentKey is a GUID and if not repopulate the object.
        //        else
        //        {
        //            try
        //            {
        //                Guid guid = new Guid(oPage.oFacadeObject.CurrentKey);
        //            }
        //            catch
        //            {
        //                bPopulateCredentialRequirement = true;
        //            }
        //        }

        //        CredentialProcess oProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
        //        oProcess.CurrentKey = strProcessKey;
        //        oProcess.SelectByKey();

        //        // Sometimes the requirement is populated but from a previous customer
        //        // so look to see if the requirement and the process are for the same customer
        //        if (bPopulateCredentialRequirement == false)
        //        {
        //            if (oPage.oFacadeObject.GetValue("cr3_cst_key") != oProcess.GetValue("cr2_cst_key"))
        //            {
        //                bPopulateCredentialRequirement = true;
        //            }
        //        }

        //        if (bPopulateCredentialRequirement == true)
        //        {
        //            // Using the process key and type, find the requirement instance and load it into oFacadeObject
        //            NfDbConnection oConn = DataUtils.GetConnection();
        //            NfDbTransaction oTrans = oConn.BeginTransaction();

        //            string strRequirementKey = oProcess.GetCredentialRequirementKey(strRequirementType, oConn, oTrans);
        //            oPage.oFacadeObject.CurrentKey = strRequirementKey;
        //            oPage.oFacadeObject.SelectByKey();

        //            Config.Session[strObjectName] = oPage.oFacadeObject;

        //            oTrans.Commit();
        //            oConn.Close();
        //        }
        //    }
        //}

        //public static void EvalRequirement(PageClass oPage, string strOnPostback)
        //{
        //    if ((strOnPostback == "true" && oPage.IsPostBack == true) || (strOnPostback != "true"))
        //    {
        //        NfDbConnection oConn = DataUtils.GetConnection();
        //        NfDbTransaction oTrans = oConn.BeginTransaction();

        //        oPage.oFacadeObject.Update(oConn, oTrans);
        //        oPage.oFacadeObject.SelectByKey(oConn, oTrans);

        //        oTrans.Commit();
        //        oConn.Close();
        //    }
        //}

        //public static void EvalCredentialProcess(string strProcessKey)
        //{
        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    CredentialProcess oProcess = GetCredentialProcessByKey(strProcessKey, oConn, oTrans);
        //    oProcess.Update(oConn, oTrans);

        //    oTrans.Commit();
        //    oConn.Close();
        //}

        //public static DynamicData GetCustomerDetail(DynamicData oCustomer, FacadeClass oFacadeClass)
        //{
        //    DynamicData oCustomerDetail = null;



        //    if (oCustomer.GetValue("cst_type") == "Individual" && oFacadeClass.GetDataObject("ind") == null)
        //    {
        //        oCustomerDetail = new DynamicData("co_individual", "co_individual_ext", "ind", "ind", "ind_cst_key", "ind_cst_key_ext", "ind_delete_flag", "Individual", null, null, null, null);
        //        oFacadeClass.AddDataObject("ind", oCustomerDetail, "Individual");
        //    }
        //    else if (oCustomer.GetValue("cst_type") == "Organization" && oFacadeClass.GetDataObject("org") == null)
        //    {
        //        oCustomerDetail = new DynamicData("co_organization", "co_organization_ext", "org", "org", "org_cst_key", "org_cst_key_ext", "org_delete_flag", "Organization", null, null, null, null);
        //        oFacadeClass.AddDataObject("org", oCustomerDetail, "Organization");
        //    }
        //    else if (oCustomer.GetValue("cst_type") == "Chapter/Affiliate" && oFacadeClass.GetDataObject("chp") == null)
        //    {
        //        oCustomerDetail = new DynamicData("co_chapter", "co_chapter_ext", "chp", "chp", "chp_cst_key", "chp_cst_key_ext", "chp_delete_flag", "Chapter/Affiliate", null, null, null, null);
        //        oFacadeClass.AddDataObject("chp", oCustomerDetail, "Chapter/Affiliate");
        //    }
        //    else if(oFacadeClass.GetDataObject("ind") == null) // Assume Individual
        //    {
        //        oCustomerDetail = new DynamicData("co_individual", "co_individual_ext", "ind", "ind", "ind_cst_key", "ind_cst_key_ext", "ind_delete_flag", "Individual", null, null, null, null);
        //        oFacadeClass.AddDataObject("ind", oCustomerDetail, "Individual");
        //    }

        //    return oCustomerDetail;
        //}

        //public static string CalculateDueDate(string strDueDateNumber, string strDueDateUnit, string strDueDateRounding)
        //{
        //    return CalculateDueDate(System.DateTime.Now, strDueDateNumber, strDueDateUnit, strDueDateRounding);
        //}

        //public static string CalculateDueDate(DateTime dtDueDate, string strDueDateNumber, string strDueDateUnit, string strDueDateRounding)
        //{
        //    int iDueDateNumber;
        //    if (string.IsNullOrWhiteSpace(strDueDateNumber) 
        //        || string.IsNullOrWhiteSpace(strDueDateUnit)
        //        || !int.TryParse(strDueDateNumber, out iDueDateNumber)
        //        )
        //        return dtDueDate.ToString();

        //    if (strDueDateUnit.Equals("Day", StringComparison.OrdinalIgnoreCase))
        //    {
        //        dtDueDate = dtDueDate.AddDays(iDueDateNumber);
        //    }
        //    else if (strDueDateUnit.Equals("Month", StringComparison.OrdinalIgnoreCase))
        //    {
        //        dtDueDate = dtDueDate.AddMonths(iDueDateNumber);
        //    }
        //    else if (strDueDateUnit.Equals("Year", StringComparison.OrdinalIgnoreCase))
        //    {
        //        dtDueDate = dtDueDate.AddYears(iDueDateNumber);
        //    }

        //    if (string.IsNullOrWhiteSpace(strDueDateRounding)) return dtDueDate.ToString();

        //    if (strDueDateRounding.Equals("FirstOfMonth", StringComparison.OrdinalIgnoreCase))
        //    {
        //        dtDueDate = new DateTime(dtDueDate.Year, dtDueDate.Month, 1);
        //    }

        //    else if (strDueDateRounding.Equals("EndOfMonth", StringComparison.OrdinalIgnoreCase))
        //    {
        //        dtDueDate = new DateTime(dtDueDate.Year, dtDueDate.Month, 1).AddMonths(1).AddDays(-1);
        //    }

        //    return dtDueDate.ToString();
        //}

        //public static string CalculatedProcessDates(string strSP, string strProcessKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "exec " + strSP + " @cr2_key = '" + strProcessKey + "'";

        //    string strDate = DataUtils.ExecuteScalar(strSQL, oConn, oTrans).ToString();

        //    return strDate;
        //}

        //public static string CalculatedRequirementDates(string strSP, string strRequirementKey, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    string strSQL = "exec " + strSP + " @cr3_key = '" + strRequirementKey + "'";

        //    string strDate = DataUtils.ExecuteScalar(strSQL, oConn, oTrans).ToString();

        //    return strDate;
        //}

        //public static ErrorClass GetPriorCredentialProcess(string strCustomerKey, string strCredentialKey, ref CredentialProcess oPriorCredentialProcess, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    // Get the Previous Process Information					
        //    string strSQL = "select top 1 cr2_key, cr2_ct2_process, cr2_due_date, cr2_extension_date"
        //        + " from ags_credential (nolock)"
        //        + " join ags_credential_process (nolock) on cr1_key = cr2_cr1_key"
        //        + " where cr1_cst_key = '" + strCustomerKey + "'"
        //        + " and cr1_key = '" + strCredentialKey + "'"
        //        + " and cr2_completion_date is not null"
        //        + " order by cr2_completion_date desc, cr2_add_date desc";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    if (oDS.Tables[0].Rows.Count != 1)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = "Could not locate prior credential process";
        //        return errorClass;
        //    }
        //    oPriorCredentialProcess.CurrentKey = oDS.Tables[0].Rows[0]["cr2_key"].ToString();
        //    oPriorCredentialProcess.SelectByKey();

        //    return errorClass;
        //}

        //public static ErrorClass GetCurrentCredentialProcess(string strCustomerKey, string strCredentialKey, ref CredentialProcess oCredentialProcess, NfDbConnection oConn, NfDbTransaction oTrans)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    // Get the Previous Process Information					
        //    string strSQL = "select top 1 cr2_key, cr2_ct2_process, cr2_due_date, cr2_extension_date"
        //        + " from ags_credential (nolock)"
        //        + " join ags_credential_process (nolock) on cr1_current_cr2_key = cr2_key"
        //        + " where cr1_cst_key = '" + strCustomerKey + "'"
        //        + " and cr1_key = '" + strCredentialKey + "'";

        //    DataSet oDS = DataUtils.GetDataSet(strSQL);

        //    if (oDS.Tables[0].Rows.Count != 1)
        //    {
        //        errorClass.Number = -1;
        //        errorClass.Message = "Could not locate current credential process";
        //        return errorClass;
        //    }
        //    oCredentialProcess.CurrentKey = oDS.Tables[0].Rows[0]["cr2_key"].ToString();
        //    oCredentialProcess.SelectByKey();

        //    return errorClass;
        //}

        //public static void SetProcessOwner(PageClass oPage)
        //{
        //    if (oPage.bIsFirstTime)
        //    {
        //        oPage.oFacadeObject.SetValue("cr2_owner", Config.CurrentUserName);
        //    }
        //}

        ////helper method to avoid hardcoding in next line: CredentialingUtils.GetStatusByState(oPage.oFacadeObject.GetValue("ct2_key"), "UNDER REVIEW")
        //public static string GetStatusByState(string sTypeKey, string sState)
        //{
        //    string sStatus = string.Empty;

        //    string sSQL = "exec ags_get_status_by_state @type_key='" + sTypeKey + "', @state='" + sState + "'";

        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    DataSet oDS = DataUtils.GetDataSet(sSQL, oConn, oTrans);

        //    oTrans.Commit();
        //    oConn.Close();

        //    if (oDS.Tables[0].Rows.Count != 1)
        //    {
        //        sStatus = sState;
        //    }
        //    else
        //    {
        //        sStatus = oDS.Tables[0].Rows[0]["status_code"].ToString();
        //    }

        //    return sStatus;
        //}

        //public static void DisplayStatementContent(PageClass oPage)
        //{
        //    Label oLabel = (Label)oPage.FindControl("ct3_statement_content");

        //    if (oLabel != null) {
        //        string _statement = (!string.IsNullOrEmpty(oPage.oFacadeObject.GetValue("ct3_statement_content"))) ? oPage.oFacadeObject.GetValue("ct3_statement_content") : "";
        //        oLabel.Text = _statement; //  oPage.oFacadeObject.GetValue("ct3_statement_content");
        //    }
        //}

        //public static void CheckForRequirements(FacadeClass oFacade, NfDbConnection oConn, NfDbTransaction oTrans, string strCheckProcedure, string strSetDataKey)
        //{
        //    string strSQL = "exec " + strCheckProcedure + " '" + oFacade.CurrentKey + "'";

        //    DataTable oDT = DataUtils.GetDataSet(strSQL, oConn, oTrans).Tables[0];

        //    foreach (DataRow oDR in oDT.Rows)
        //    {
        //        string strRequirementKey = oDR["cr3_key"].ToString();
        //        string setDataKey = oFacade.CurrentKey;
        //        string dataKey = null;
        //        if (oDR.Table.Columns.Contains("data_key")) 
        //        {
        //            dataKey = oDR["data_key"].ToString();
        //            if (!string.IsNullOrEmpty(dataKey))
        //            { 
        //                setDataKey = dataKey; 
        //            }    
        //        }                                                             

        //        CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);

        //        if (strSetDataKey == "1")
        //        {
        //            oRequirement.SetValue("cr3_data_key", setDataKey);
        //        }

        //        oRequirement.LoadRelatedData(oConn, oTrans);

        //        oRequirement.Update(oConn, oTrans);
        //    }
        //}

        //public static void CheckForRequirements(PageClass oPage, string strCheckProcedure, string strSetDataKey)
        //{
        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    CheckForRequirements(oPage.oFacadeObject, oConn, oTrans, strCheckProcedure, strSetDataKey);

        //    oTrans.Commit();
        //    oConn.Close();        
        //}

        //public static void TransferCredentialProcessEWeb(PageClass oPage)
        //{
        //    if (Config.IsEweb)
        //    {
        //        CredentialProcess oCurrentProcess;
        //        string strOldCr2Status = "";

        //        TerminateCredentialProcessOnTransfer(oPage, out oCurrentProcess, out strOldCr2Status);

        //        //Redirect to Cert Apps
        //        //oPage.Response.Redirect("DynamicPage.aspx?webcode=AGSCertApps&cr2_key=" + oCurrentProcess.GetValue("cr2_key") + "");
                
        //    }
        //    else
        //        return;
        //}

        //public static void TransferCredentialProcess(PageClass oPage)
        //{
        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    string strCustomerKey = oPage.oFacadeObject.GetValue("cr2_cst_key");
        //    string strCurrentProcess = oPage.oFacadeObject.GetValue("cr2_ct2_process");
        //    string strCurrentProcessStartDate = oPage.oFacadeObject.GetValue("cr2_start_date");

        //    // Label ddlTransferProcess = (Label)oPage.FindControl("cr1_ct2_current_process");
        //    DropDownList ddlTransferProcess = (DropDownList)oPage.FindControl("cr1_ct2_current_process");
        //    // HtmlInputHidden ddlTransferProcess = (HtmlInputHidden)oPage.FindControl("cr1_ct2_current_process");
        //    string strTransferProcess = ddlTransferProcess.SelectedItem.Text; //  ddlTransferProcess.Text; // ddlTransferProcess.Value; 

        //    if (strCurrentProcess != strTransferProcess)
        //    {
        //        CredentialProcess oCurrentProcess;
        //        string strOldCr2Status;
        //        string strCredentialKey;
        //        CredentialProcess oInitProcess;

        //        TerminateCredentialProcessOnTransfer(oPage, out oCurrentProcess, out strOldCr2Status);

        //        strCredentialKey = oPage.oFacadeObject.GetValue("cr2_cr1_key");
        //        oInitProcess = null;                

        //        //create new process
        //        oInitProcess = CredentialingUtils.GetCredentialProcessByType(strTransferProcess);

        //        oInitProcess.SetValue("cr2_cr1_key", strCredentialKey);
        //        oInitProcess.SetValue("cr2_ct2_process", strTransferProcess);
        //        oInitProcess.SetValue("cr2_cst_key", strCustomerKey);
        //        oInitProcess.SetValue("cr2_status", strOldCr2Status); //  oCurrentProcess.GetValue("cr2_status"));
        //        oInitProcess.SetValue("cr2_start_date", strCurrentProcessStartDate);
        //        oInitProcess.SetValue("cr2_due_date", oCurrentProcess.GetValue("cr2_due_date"));
        //        oInitProcess.LoadRelatedData(oConn, oTrans);

        //        oInitProcess.Insert(oConn, oTrans);

        //        Credential oCredential =
        //            CredentialingUtils.GetCredentialByKey(strCredentialKey, oConn, oTrans);

        //        oCredential.SetValue("cr1_current_cr2_key", oInitProcess.CurrentKey);
        //        oCredential.SetValue("cr1_application_cr2_key", oInitProcess.CurrentKey);
        //        oCredential.Update(oConn, oTrans);
        //    }

        //    oTrans.Commit();
        //    oConn.Close();
        //}

        //private static void TerminateCredentialProcessOnTransfer(PageClass oPage, out CredentialProcess oCurrentProcess, out string strOldCr2Status)
        //{
        //    NfDbConnection oConn = DataUtils.GetConnection();
        //    NfDbTransaction oTrans = oConn.BeginTransaction();

        //    string strProcessKey = oPage.oFacadeObject.GetValue("cr2_key");

        //    if (UtilityFunctions.EmptyString(strProcessKey))
        //    {
        //        strProcessKey = oPage.Request["cr2_key"].ToString();
        //    }

        //    //terminate the current process
        //    oCurrentProcess = (CredentialProcess)DataUtils.InstantiateFacadeObject("AGSCredentialProcess");
        //    oCurrentProcess.CurrentKey = strProcessKey;
        //    oCurrentProcess.SelectByKey();

        //    strOldCr2Status = oCurrentProcess.GetValue("cr2_status");

        //    oCurrentProcess.SetValue("cr2_termination_date", DateTime.Now.ToLongDateString());
        //    oCurrentProcess.SetValue("cr2_status", "Transferred");

        //    // 4/24/18 sspruce: Do not do a full update
        //    //oCurrentProcess.Update();
        //    oCurrentProcess.GetDataObject("cr2").Update(oConn, oTrans);

        //    oTrans.Commit();
        //    oConn.Close();
        //}

        //public static ErrorClass InitCopyCredentialProcessType(PageClass oPage)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    string ct2Key = null;
        //    if (oPage.Request["ct2_key_og"] != null)
        //    {
        //        ct2Key = oPage.Request["ct2_key_og"].ToString();
        //    }

        //    if (!oPage.bIsFirstTime || String.IsNullOrEmpty(ct2Key))
        //    {
        //        return errorClass;
        //    }

        //    using (NfDbConnection oConn = DataUtils.GetConnection())
        //    {
        //        using (NfDbTransaction oTrans = oConn.BeginTransaction())
        //        {
        //            try
        //            {
        //                string strSQL = "SELECT mdc_name, mdc_control_class FROM md_column (NOLOCK) WHERE mdc_table_name = 'ags_credential_process_type'";
        //                string ct2SQL = String.Format("SELECT TOP 1 * FROM ags_credential_process_type WHERE ct2_key = {0}", DataUtils.ValuePrep(ct2Key, DataUtils.AVType.av_key, false));

        //                DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
        //                DataSet ct2DS = DataUtils.GetDataSet(ct2SQL, oConn, oTrans);

        //                var rows = oDS.Tables[0].Rows;

        //                foreach (DataRow row in rows)
        //                {
        //                    string rowName = row["mdc_name"].ToString();
        //                    string rowType = row["mdc_control_class"].ToString();

        //                    string value = ct2DS.Tables[0].Rows[0][rowName].ToString();
        //                    Control genericControl = oPage.FindControl(rowName);

        //                    if (genericControl == null)
        //                    {
        //                        continue;
        //                    }

        //                    string controlType = genericControl.GetType().Name;

        //                    switch (controlType)
        //                    {
        //                        case "RichTextBox":
        //                            Avectra.netForum.Controls.RichTextBox richTextBox = (Avectra.netForum.Controls.RichTextBox)oPage.FindControl(rowName);
        //                            richTextBox.Text = value;
        //                            break;
        //                        case "TextBox":
        //                            if (rowType == "DateTextBox")
        //                            {
        //                                DateTime date;
        //                                bool result = DateTime.TryParse(value, out date);
        //                                if (result)
        //                                {
        //                                    TextBox textBox = (TextBox)oPage.FindControl(rowName);
        //                                    textBox.Text = date.ToShortDateString();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                TextBox textBox = (TextBox)oPage.FindControl(rowName);
        //                                textBox.Text = value;
        //                            }
        //                            break;
        //                        case "DropDownList":
        //                            DropDownList dropDownList = (DropDownList)oPage.FindControl(rowName);
        //                            dropDownList.Text = value;
        //                            break;
        //                        case "CheckBox":
        //                            CheckBox checkBox = (CheckBox)oPage.FindControl(rowName);

        //                            if (value == "1")
        //                            {
        //                                checkBox.Checked = true;
        //                            }
        //                            else
        //                            {
        //                                checkBox.Checked = false;
        //                            }
        //                            break;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                errorClass.Number = -1;
        //                errorClass.Message = "AGS Copy Credential Process Type Failed.  Message: " + ex.Message;
        //            }

        //            return errorClass;
        //        }
        //    }
        //}

        //public static ErrorClass CopyCredentialProcessType(PageClass oPage)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    using (NfDbConnection oConn = DataUtils.GetConnection())
        //    {
        //        using (NfDbTransaction oTrans = oConn.BeginTransaction())
        //        {
        //            try
        //            {
        //                string ct2KeyCopy = oPage.oFacadeObject.GetValue("ct2_key");
        //                string originalCt2Key = oPage.oFacadeObject.GetValue("ct2_key_og");

        //                string sql = String.Format("exec ags_credential_process_type_copy @ct2_key='{0}', @ct2_key_copy='{1}'", originalCt2Key, ct2KeyCopy);

        //                var result = DataUtils.ExecuteSql(sql, oConn, oTrans);

        //                oTrans.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                errorClass.Number = -1;
        //                errorClass.Message = "AGS Copy Credential Process Type Failed.  Message: " + ex.Message;
        //            }

        //            return errorClass;
        //        }
        //    }
        //}


        //public static ErrorClass InitCopyCredentialRequirementType(PageClass oPage)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    string ct3Key = null;

        //    if (oPage.Request["ct3_key_original"] != null)
        //    {
        //        ct3Key = oPage.Request["ct3_key_original"].ToString();
        //    }

        //    if (!oPage.bIsFirstTime || String.IsNullOrEmpty(ct3Key))
        //    {
        //        return errorClass;
        //    }

        //    using (NfDbConnection oConn = DataUtils.GetConnection())
        //    {
        //        using (NfDbTransaction oTrans = oConn.BeginTransaction())
        //        {
        //            try
        //            {
        //                string strSQL = "SELECT mdc_name, mdc_control_class FROM md_column (NOLOCK) WHERE mdc_table_name = 'ags_credential_requirement_type'";
        //                string ct2SQL = String.Format("SELECT TOP 1 * FROM ags_credential_requirement_type WHERE ct3_key = {0}", DataUtils.ValuePrep(ct3Key, DataUtils.AVType.av_key, false));

        //                DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
        //                DataSet ct2DS = DataUtils.GetDataSet(ct2SQL, oConn, oTrans);

        //                var rows = oDS.Tables[0].Rows;

        //                foreach (DataRow row in rows)
        //                {
        //                    string rowName = row["mdc_name"].ToString();
        //                    string rowType = row["mdc_control_class"].ToString();

        //                    string value = ct2DS.Tables[0].Rows[0][rowName].ToString();
        //                    Control genericControl = oPage.FindControl(rowName);

        //                    if (genericControl == null)
        //                    {
        //                        continue;
        //                    }

        //                    string controlType = genericControl.GetType().Name;

        //                    switch (controlType)
        //                    {
        //                        case "RichTextBox":
        //                            Avectra.netForum.Controls.RichTextBox richTextBox = (Avectra.netForum.Controls.RichTextBox)oPage.FindControl(rowName);
        //                            richTextBox.Text = value;
        //                            break;
        //                        case "TextBox":
        //                            if (rowType == "DateTextBox")
        //                            {
        //                                DateTime date;
        //                                bool result = DateTime.TryParse(value, out date);
        //                                if (result)
        //                                {
        //                                    TextBox textBox = (TextBox)oPage.FindControl(rowName);
        //                                    textBox.Text = date.ToShortDateString();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                TextBox textBox = (TextBox)oPage.FindControl(rowName);
        //                                textBox.Text = value;
        //                            }
        //                            break;
        //                        case "DropDownList":
        //                            DropDownList dropDownList = (DropDownList)oPage.FindControl(rowName);
        //                            dropDownList.Text = value;
        //                            break;
        //                        case "CheckBox":
        //                            CheckBox checkBox = (CheckBox)oPage.FindControl(rowName);

        //                            if (value == "1")
        //                            {
        //                                checkBox.Checked = true;
        //                            }
        //                            else
        //                            {
        //                                checkBox.Checked = false;
        //                            }
        //                            break;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                errorClass.Number = -1;
        //                errorClass.Message = "AGS Copy Credential Requirement Type Failed.  Message: " + ex.Message;
        //            }

        //            return errorClass;
        //        }
        //    }
        //}

        //public static ErrorClass CopyCredentialRequirementType(PageClass oPage)
        //{
        //    ErrorClass errorClass = new ErrorClass();

        //    using (NfDbConnection oConn = DataUtils.GetConnection())
        //    {
        //        using (NfDbTransaction oTrans = oConn.BeginTransaction())
        //        {
        //            try
        //            {
        //                string originalCt3Key = null;
        //                if (oPage.Request["ct3_key_original"] != null)
        //                {
        //                    originalCt3Key = oPage.Request["ct3_key_original"].ToString();
        //                }

        //                string ct3KeyCopy = oPage.oFacadeObject.GetValue("ct3_key");

        //                if (String.IsNullOrEmpty(originalCt3Key) || String.IsNullOrEmpty(ct3KeyCopy))
        //                {
        //                    throw new System.ArgumentNullException("Arguments ct3_key_original or ct3_key cannot be null");
        //                }

        //                string sql = String.Format("exec ags_credential_requirement_type_copy @ct3_key_original={0}, @ct3_key_copy={1}", DataUtils.ValuePrep(originalCt3Key, DataUtils.AVType.av_key, false), DataUtils.ValuePrep(ct3KeyCopy, DataUtils.AVType.av_key, false));

        //                var result = DataUtils.ExecuteSql(sql, oConn, oTrans);

        //                oTrans.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                errorClass.Number = -1;
        //                errorClass.Message = "AGS Copy Credential Process Type Failed.  Message: " + ex.Message;
        //            }

        //            return errorClass;
        //        }
        //    }
        //}
    }
}
