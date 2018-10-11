using System;
using System.Data;

namespace ags.credentialing
{
    public class PopulateLogBatchProcess : BatchProcess
    {
        public override ErrorClass Get()
        {
            NfDbConnection oConn = DataUtils.GetConnection();
            NfDbTransaction oTrans = oConn.BeginTransaction();

            string strBatchId = BatchUtils.GetNextBatchID(oConn, oTrans);

            SetValue("bp2_batch_id", strBatchId);

            string strSQL = "select count(*) from ags_credential_process (nolock) where cr2_delete_flag = 0";
            DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
            SetValue("bp2_count", oDS.Tables[0].Rows[0][0].ToString());

            Update(oConn, oTrans);

            oTrans.Commit();
            oConn.Close();
            
            return new ErrorClass();
        }

        public override ErrorClass Run()
        {
            ErrorClass oErr = new ErrorClass();

            string strBatchKey = GetValue("bp2_key");

            NfDbConnection oConn = DataUtils.GetConnection();
            NfDbTransaction oTrans = null;

            string strSQL = null;
            DataSet oDS = null;

            strSQL = "select count(*) from ags_batch_process_detail where bp3_bp2_key = '" + strBatchKey + "'";
            oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
            int iBatchCount = Convert.ToInt16(oDS.Tables[0].Rows[0][0].ToString());

            if (iBatchCount == 0)
            {
                oTrans = oConn.BeginTransaction();
                strSQL = "delete from ags_credential_log";
                oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);
                oTrans.Commit();
            }

            if (iBatchCount == 0)
            {
                strSQL = "select distinct cr2_cst_key from ags_credential_process (nolock) where cr2_delete_flag = 0";
            }
            else
            {
                strSQL = "select distinct cr2_cst_key from ags_credential_process (nolock) where cr2_key in (select bp3_detail_key from ags_batch_process_detail (nolock) where bp3_bp2_key = '" + strBatchKey + "')";
            }

            oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

            foreach (DataRow oRow in oDS.Tables[0].Rows)
            {
                oTrans = oConn.BeginTransaction();

                CO_Individual oIndividual = (CO_Individual)DataUtils.InstantiateFacadeObject("Individual");
                oIndividual.CurrentKey = oRow["cr2_cst_key"].ToString();
                oIndividual.GetDataObject("ind").SelectByKey(oConn, oTrans);
                oIndividual.GetDataObject("cst").SelectByKey(oConn, oTrans);

                CommonUtils.LogIndividual(oIndividual, oConn, oTrans);

                oTrans.Commit();
            }

            if (iBatchCount == 0)
            {
                strSQL = "select distinct cr2_cr1_key from ags_credential_process (nolock) where cr2_delete_flag = 0";
            }
            else
            {
                strSQL = "select distinct cr2_cr1_key from ags_credential_process (nolock) where cr2_key in (select bp3_detail_key from ags_batch_process_detail (nolock) where bp3_bp2_key = '" + strBatchKey + "')";
            }

            oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

            foreach (DataRow oRow in oDS.Tables[0].Rows)
            {
                oTrans = oConn.BeginTransaction();

                Credential oCredential = (Credential)DataUtils.InstantiateFacadeObject("AGSCredential");
                oCredential.CurrentKey = oRow["cr2_cr1_key"].ToString();
                oCredential.GetDataObject("cr1").SelectByKey(oConn, oTrans);
                oCredential.Log(oConn, oTrans);

                oTrans.Commit();
            }

            if (iBatchCount == 0)
            {
                strSQL = "select distinct cr2_key from ags_credential_process (nolock) where cr2_delete_flag = 0";
            }
            else
            {
                strSQL = "select distinct cr2_key from ags_credential_process (nolock) where cr2_key in (select bp3_detail_key from ags_batch_process_detail (nolock) where bp3_bp2_key = '" + strBatchKey + "')";
            }

            oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

            foreach (DataRow oRow in oDS.Tables[0].Rows)
            {
                oTrans = oConn.BeginTransaction();

                CredentialProcess oCredentialProcess = CredentialingUtils.GetCredentialProcessByKey(oRow["cr2_key"].ToString(), oConn, oTrans);
                oCredentialProcess.Log(oConn, oTrans);

                oTrans.Commit();
            }

            if (iBatchCount == 0)
            {
                strSQL = "select distinct cr3_key from ags_credential_requirement (nolock) join ags_credential_process on cr3_cr2_key = cr2_key where cr2_delete_flag = 0 and cr3_delete_flag = 0";
            }
            else
            {
                strSQL = "select distinct cr3_key from ags_credential_requirement (nolock) where cr3_cr2_key in (select bp3_detail_key from ags_batch_process_detail (nolock) where bp3_bp2_key = '" + strBatchKey + "')";
            }

            oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

            foreach (DataRow oRow in oDS.Tables[0].Rows)
            {
                oTrans = oConn.BeginTransaction();

                CredentialRequirement oCredentialRequirement = CredentialingUtils.GetCredentialRequirementByKey(oRow["cr3_key"].ToString(), oConn, oTrans);
                oCredentialRequirement.Log(oConn, oTrans);

                oTrans.Commit();
            }

            oConn.Close();

            return oErr;
        }
    }
}
