using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CEPeriod")]
    public class CEPeriod : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ce2";
        [Key]
        public Guid Ce2Key { get; set; }

        public Guid Ce2CusKey { get; set; }

        public DateTimeOffset Ce2StartDate { get; set; }

        public DateTimeOffset Ce2EndDate { get; set; }

        public DateTimeOffset Ce2AddDate { get; set; }

        public DateTimeOffset? Ce2ChangeDate { get; set; }

        public string Ce2AddUser { get; set; }

        public string Ce2ChangeUser { get; set; }

        [ForeignKey("Ce2CusKey")]
        public virtual Customer Customer { get; set; }


        

              //public void Insert(NfDbConnection oConn, NfDbTransaction oTrans)
              //{
              //    ErrorClass errorClass = new ErrorClass();

              //    try
              //    {
              //        Guid guid = Guid.NewGuid();
              //        _oData.SetValue("ce2_key", guid.ToString());
              //        _oData.CurrentKey = guid.ToString();
              //        CurrentKey = guid.ToString();

              //        if (errorClass.Number == 0)
              //        {
              //            errorClass = _oData.Insert(oConn, oTrans);
              //        }

              //        if (errorClass.Number == 0)
              //        {
              //            LoadRelatedData(oConn, oTrans);
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

          //    public void Update(NfDbConnection oConn, NfDbTransaction oTrans)
          //    {

          //        try
          //        {
        		//if (errorClass.Number == 0)
        		//{
        		//	errorClass = UpdateCEPeriod(oConn, oTrans);
        		//}

          //            if (errorClass.Number == 0)
          //            {
          //                errorClass = _oData.Update(oConn, oTrans);
          //            }

          //            // Update the CE Requirement associated to this CE Period if there is one.
          //            if(errorClass.Number == 0)
          //            {
        		//	string strSQL = "SELECT TOP 1 cr3_key"
        		//		+ " FROM ags_credential_requirement (NOLOCK)"
        		//		+ " WHERE cr3_data_key = '" + GetValue("ce2_key") + "'"
        		//		+ " AND cr3_delete_flag = 0";

        		//	DataSet oDS = DataUtils.GetDataSet(strSQL);

        		//	if(oDS.Tables[0].Rows.Count == 1)
        		//	{
        		//		string strRequirementKey = oDS.Tables[0].Rows[0]["cr3_key"].ToString();

        		//		CredentialRequirement oRequirement = CredentialingUtils.GetCredentialRequirementByKey(strRequirementKey, oConn, oTrans);
        		//		oRequirement.Update(oConn, oTrans);

        		//		// Even if the requirement has been met we still need to reevaluate the requirement
        		//		// to update balances
        		//		if(!UtilityFunctions.EmptyString(oRequirement.GetOriginalValue("cr3_met_date")))
        		//		{
        		//			oRequirement.Eval(oConn, oTrans);
        		//		}
        		//	}
          //            }
          //        }
          //        catch (Exception e)
          //        {
          //            errorClass.Number = -1;
          //            errorClass.Message = e.Message;
          //        }
          //        finally
          //        {
          //            if (errorClass.Number != 0)
          //            {
          //                Config.LastError = errorClass;
          //            }
          //        }

          //        return errorClass;
          //    }

 


             // public void UpdateCEPeriod(NfDbConnection oConn, NfDbTransaction oTrans)
              //{
              //    ErrorClass errorClass = new ErrorClass();

              //    string strSQL = "exec ags_get_ce_period_hours '" + GetValue("ce2_key") + "'";

              //    DataSet oDS = DataUtils.GetDataSet(strSQL, oConn, oTrans);

              //    if (oDS.Tables[0].Rows.Count != 1)
              //    {
              //        errorClass.Number = -1;
              //        errorClass.Message = "Could not get CE Period Hours";
              //        return errorClass;
              //    }

              //    // Update Total hours
              //    decimal dCEPeriodTotalReportedHours = 0;
              //    decimal dCEPeriodTotalRequiredHours = 0;
              //    decimal dCEPeriodTotalBalanceHours = 0;

              //    dCEPeriodTotalReportedHours = CommonUtils.GetDecimalValue(oDS.Tables[0].Rows[0]["ceu_total_reported_hours"].ToString());
              //    SetValue("ce2_total_reported_hours", dCEPeriodTotalReportedHours.ToString());

              //    dCEPeriodTotalRequiredHours = CommonUtils.GetDecimalValue(GetValue("ce2_total_required_hours"));
              //    dCEPeriodTotalBalanceHours = dCEPeriodTotalRequiredHours - dCEPeriodTotalReportedHours;
              //    if (dCEPeriodTotalBalanceHours < 0)
              //    {
              //        dCEPeriodTotalBalanceHours = 0;
              //    }
              //    SetValue("ceu_total_balance_hours", dCEPeriodTotalBalanceHours.ToString());

              //    // Update Ethics hours
              //    decimal dCEPeriodEthicsReportedHours = 0;
              //    decimal dCEPeriodEthicsRequiredHours = 0;
              //    decimal dCEPeriodEthicsBalanceHours = 0;

              //    dCEPeriodEthicsReportedHours = CommonUtils.GetDecimalValue(oDS.Tables[0].Rows[0]["ceu_ethics_reported_hours"].ToString());
              //    SetValue("ce2_ethics_reported_hours", dCEPeriodEthicsReportedHours.ToString());

              //    dCEPeriodEthicsRequiredHours = CommonUtils.GetDecimalValue(GetValue("ce2_ethics_required_hours"));
              //    dCEPeriodEthicsBalanceHours = dCEPeriodEthicsRequiredHours - dCEPeriodEthicsReportedHours;
              //    if (dCEPeriodEthicsBalanceHours < 0)
              //    {
              //        dCEPeriodEthicsBalanceHours = 0;
              //    }
              //    SetValue("ce2_ethics_balance_hours", dCEPeriodEthicsBalanceHours.ToString());


              //    _oData.Update(oConn, oTrans);

              //    return errorClass;
              //}

    }
}
