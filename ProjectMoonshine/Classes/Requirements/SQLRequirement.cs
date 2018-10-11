using System;
using ags.credentialing;

namespace ags.requirements
{
    public class SQLRequirement : CredentialRequirement
    {
		//public SQLRequirement()
		//{
  //          if (!UtilityFunctions.EmptyString(GetValue("ct3_mdt_key")))
  //          {
  //              DynamicData oTable = new DynamicData("mdt_table", null, "mdt", null, "mdt_key", null, "mdt_delete_flag", "Table");
  //              oTable.CurrentKey = GetValue("ct3_mdt_key");
  //              oTable.SelectByKey();

  //              _oCredentialRequirementData = new DynamicData();
  //              _oCredentialRequirementData.TableName = oTable.GetValue("mdt_name");
  //              _oCredentialRequirementData.TablePrefix = oTable.GetValue("mdt_prefix");
  //              _oCredentialRequirementData.KeyField = oTable.GetValue("mdt_key_column");
  //              _oCredentialRequirementData.DeleteFlagField = oTable.GetValue("mdt_prefix") + "_delete_flag";
  //              _oCredentialRequirementData.Description = oTable.GetValue("mdt_description");

  //              if (DataUtils.ExecuteSqlScalar("select * from md_column where mdc_name like '" + oTable.GetValue("mdt_prefix") + "%_key'") > 0)
  //              {
  //                  _oCredentialRequirementData.TableNameExt = oTable.GetValue("mdt_name") + "_ext";
  //                  _oCredentialRequirementData.TablePrefixExt = oTable.GetValue("mdt_prefix");
  //                  _oCredentialRequirementData.KeyFieldExt = oTable.GetValue("mdt_key_column") + "_ext";
  //              }

  //              AddDataObject(oTable.GetValue("mdt_prefix"), _oCredentialRequirementData, oTable.GetValue("mdt_description"));
  //          }
  //      }

  //      public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans) {
  //          ErrorClass errorClass = new ErrorClass();

  //          if (!UtilityFunctions.EmptyString(GetValue("ct3_init_stored_procedure"))) {
  //              string res = DataUtils.ExecuteScalar("exec " + GetValue("ct3_eval_stored_procedure") + " @cr3_key='" + GetValue("cr3_key") + "'", oConn, oTrans).ToString();

  //              if (!UtilityFunctions.EmptyString(res)) {
  //                  if (bool.Parse(res)) {
  //                      SetValue("cr3_met_date", DateTime.Now.ToString());
  //                      SetValue("cr3_met_user", Config.CurrentUserName);
  //                  }
  //              }
  //          }

  //          return errorClass;
  //      }

  //      public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans) {
  //          ErrorClass errorClass = new ErrorClass();

  //          if (!UtilityFunctions.EmptyString(GetValue("ct3_eval_stored_procedure"))) {
  //              string res = DataUtils.ExecuteScalar("exec " + GetValue("ct3_eval_stored_procedure") + " @cr3_key='" + GetValue("cr3_key") + "'", oConn, oTrans).ToString();

  //              if (!UtilityFunctions.EmptyString(res)) {
  //                  if (bool.Parse(res)) {
  //                      SetValue("cr3_met_date", DateTime.Now.ToString());
  //                      SetValue("cr3_met_user", Config.CurrentUserName);
  //                  }
  //              }
  //          }

  //          return errorClass;
  //      }
    }
}
