using System;

namespace ags.requirements
{
    class ExperiencePeriod {

        //public DynamicData _oData;

        //public ExperiencePeriod(){
        //    _oData = new DynamicData("ags_experience_period", "ags_experience_period_ext", "ex2", "ex2", "ex2_key", "ex2_key_ext", "ex2_delete_flag", "ExperiencePeriod", null, null, null, null);
        //    AddDataObject("ex2", _oData, "ExperiencePeriod");
        //}

        //public override ErrorClass Insert(NfDbConnection conn, NfDbTransaction trans) {
        //    ErrorClass errorClass = new ErrorClass();

        //    try {
        //        Guid guid = Guid.NewGuid();
        //        _oData.SetValue("ex2_key", guid.ToString());
        //        _oData.CurrentKey = guid.ToString();
        //        CurrentKey = guid.ToString();

        //        if (errorClass.Number == 0) {
        //            errorClass = _oData.Insert(conn, trans);
        //        }

        //        if (errorClass.Number == 0) {
        //            LoadRelatedData(conn, trans);
        //        }
        //    }
        //    catch (Exception e) {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally {
        //        if (errorClass.Number != 0) {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass Update(NfDbConnection conn, NfDbTransaction trans) {
        //    ErrorClass errorClass = new ErrorClass();

        //    try {
                
        //    }
        //    catch (Exception e) {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally {
        //        if (errorClass.Number != 0) {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}


        //public override void LoadRelatedData(NfDbConnection conn, NfDbTransaction trans) {
        //   // throw new NotImplementedException();
        //}

        //public override void SelectByKey(NfDbConnection conn, NfDbTransaction trans){
        //    ErrorClass errorClass = new ErrorClass();

        //    try {
        //        _oData.CurrentKey = CurrentKey;
        //        _oData.SelectByKey(conn, trans);

        //    }
        //    catch (Exception e) {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally {
        //        if (errorClass.Number != 0) {
        //            Config.LastError = errorClass;
        //        }
        //    }
        //}        

        //public override ErrorClass Delete(NfDbConnection conn, NfDbTransaction trans) {
        //    if (Config.DeleteMethod == "hard") {
        //        return HardDelete(conn, trans);
        //    }
        //    return SoftDelete(conn, trans);
        //}

        //public override ErrorClass HardDelete(NfDbConnection conn, NfDbTransaction trans) {
        //    ErrorClass errorClass = new ErrorClass();

        //    try {
        //        if (errorClass.Number == 0) {
        //            errorClass = _oData.Delete(conn, trans);
        //        }
        //    }

        //    catch (Exception e) {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally {
        //        if (errorClass.Number != 0) {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

        //public override ErrorClass SoftDelete(NfDbConnection conn, NfDbTransaction trans) {
        //    ErrorClass errorClass = new ErrorClass();

        //    try {
        //        SetValue("ex2_delete_flag", "1");

        //        errorClass = _oData.Update(conn, trans);
        //    }

        //    catch (Exception e) {
        //        errorClass.Number = -1;
        //        errorClass.Message = e.Message;
        //    }
        //    finally {
        //        if (errorClass.Number != 0) {
        //            Config.LastError = errorClass;
        //        }
        //    }

        //    return errorClass;
        //}

    }

}
