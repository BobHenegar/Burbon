using System.Data;
using System.Linq;
using ags.credentialing;


namespace ags.requirements
{
    class SwitchRequirement : CredentialRequirement {


        public SwitchRequirement(){
            _oCredentialRequirementData = new DynamicData("ags_credential_requirement_type_x_credential_requirement_type", null, "ct0", null, "ct0_key", null, "ct0_delete_flag", "CredReqXCredReq", null, null, null, null);
            AddDataObject("ct0", _oCredentialRequirementData, "CredReqXCredReq");
        }


        public override ErrorClass Init(NfDbConnection oConn, NfDbTransaction oTrans) {
            ErrorClass errorClass = new ErrorClass();

            SetValue("cr3_start_date", System.DateTime.Now.ToString());
            SetValue("cr3_due_date", System.DateTime.Now.ToString());

            errorClass = _oData.Update(oConn, oTrans);

            Eval(oConn, oTrans);

            return errorClass;
        }

        public override ErrorClass Eval(NfDbConnection oConn, NfDbTransaction oTrans){
            ErrorClass errorClass = new ErrorClass();

            string strChoiceKey = null;

            if (!UtilityFunctions.EmptyString(GetValue("ct3_auto_switch_sp"))){
                string strSQL = "exec " + GetValue("ct3_auto_switch_sp") + " @cr3_key='" + GetValue("cr3_key") + "'";

                DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

                if (oDS.Tables[0].Rows.Count == 1){
                    strChoiceKey = oDS.Tables[0].Rows[0]["ct0_key"].ToString();
                    SetValue("cr3_choice_ct3_key", strChoiceKey);
                }
            }

            else if (!UtilityFunctions.EmptyString(GetValue("cr3_choice_ct3_key"))){
                strChoiceKey = GetValue("cr3_choice_ct3_key");
            }

            if (strChoiceKey != null){
                DataSet ds = DataUtils.GetDataSet(string.Format("select ct0_additional_ct3_key, ct0_child_ct3_key " +
                                                                "from ags_credential_requirement_type_x_credential_requirement_type " +
                                                                "where ct0_key ='{0}'", strChoiceKey), oConn, oTrans);


                if (ds.Tables.Cast<DataTable>().Any(table => table.Rows.Count != 0)){

                    string replaceReqirment = ds.Tables[0].Rows[0]["ct0_child_ct3_key"].ToString();
                    string additionalReqirement = ds.Tables[0].Rows[0]["ct0_additional_ct3_key"].ToString();

                    string newRequirementType = CredentialingUtils.GetCredentialRequirementType(replaceReqirment, oConn, oTrans);

                    CredentialRequirement childRequirement = CredentialingUtils.GetCredentialRequirement(newRequirementType);

                    if (childRequirement != null){
                        childRequirement.SetValue("cr3_cr2_key", GetValue("cr3_cr2_key"));
                        childRequirement.SetValue("cr3_cr3_key", CurrentKey);
                        childRequirement.SetValue("cr3_ct3_requirement", newRequirementType);
                        childRequirement.SetValue("cr3_cst_key", _oCustomer.GetValue("cst_key"));

                        childRequirement.LoadRelatedData(oConn, oTrans);
                        errorClass = childRequirement.Insert(oConn, oTrans);
                    }



                    if (!UtilityFunctions.EmptyString(additionalReqirement)){

                        string additionalRequirementType = CredentialingUtils.GetCredentialRequirementType(additionalReqirement, oConn, oTrans);

                        CredentialRequirement addChildRequirement = CredentialingUtils.GetCredentialRequirement(additionalRequirementType);

                        if (addChildRequirement != null) {
                            addChildRequirement.SetValue("cr3_cr2_key", GetValue("cr3_cr2_key"));
                            addChildRequirement.SetValue("cr3_cr3_key", CurrentKey);
                            addChildRequirement.SetValue("cr3_ct3_requirement", additionalRequirementType);
                            addChildRequirement.SetValue("cr3_cst_key", _oCustomer.GetValue("cst_key"));

                            addChildRequirement.LoadRelatedData(oConn, oTrans);
                            errorClass = addChildRequirement.Insert(oConn, oTrans);
                        }
                    }
                }


                SetValue("cr3_hide_online_flag", "1");
                SetValue("cr3_status", GetValue("ct3_cr3_met_status"));
                SetValue("cr3_met_date", System.DateTime.Now.ToString());
                SetValue("cr3_met_user", Config.CurrentUserName);
            }


            errorClass = _oData.Update(oConn, oTrans);

            return errorClass;
        }


    }
}
