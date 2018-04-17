using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Models.Payment.HDSP;

namespace MIS.Services.Payment.HDSP
{
    public class DTCOImportService
    {
        public Boolean SaveDTCOPaymentData(DataTable paramTable, string fileName, string dist_cd, string vdc_cd, out string exc)
        {
            QueryResult qr = null;
            bool res = false;
            exc = string.Empty;
            string batchID = "";
            int chkPoint = 0;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_DTCO_PAYMENT1";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_DTCO_FILE_BATCH",
                                               "I",
                                              dist_cd,
                                              vdc_cd,
                                                 "Completed",
                                                fileName,
                                                 CurrentDate,
                                                 SessionCheck.getSessionUsername(),
                                                 DBNull.Value,
                                                 DBNull.Value);

                    batchID = qr["p_BATCH_ID"].ConvertToString();
                    string urlIpAddress = CommonVariables.IPAddress;
                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        chkPoint = i;
                        int quarter = 0;
                        string nepali_cheque_tran_dt = "";
                        string fiscalyr = "";
                        string cheque_tran_date = "";
                        string nra_district = "";
                        string nra_vdc = "";
                        string nra_ward = "";
                        string pa_no = paramTable.Rows[i]["PA_NO"].ToString().TrimEnd();
                        #region PA_NO splits
                        if (pa_no != "")
                        {
                            string[] splits = pa_no.Split('-');
                            int cnt = splits.Count();
                            if (cnt == 5)
                            {
                                nra_district = splits[splits.Length - 5].ToString();
                                nra_vdc = splits[splits.Length - 4].ToString();
                                nra_ward = splits[splits.Length - 3].ToString();
                            }
                            else if (cnt == 6)
                            {
                                nra_district = splits[splits.Length - 5].ToString();
                                nra_vdc = splits[splits.Length - 4].ToString();
                                nra_ward = splits[splits.Length - 3].ToString();
                            }
                            else if (cnt == 7)
                            {
                                nra_district = splits[splits.Length - 6].ToString();
                                nra_vdc = splits[splits.Length - 5].ToString();
                                nra_ward = splits[splits.Length - 4].ToString();
                            }
                        }

                        #endregion
                        cheque_tran_date = ((paramTable.Rows[i]["CHEQUE_TRAN_EDATE_(AD)"]).ConvertToString("MM/dd/yyyy"));
                        fiscalyr = CommonFunction.GetRecentFiscalYear(cheque_tran_date);
                        nepali_cheque_tran_dt = NepaliDate.getNepaliDate(cheque_tran_date);

                        string[] datesplit = nepali_cheque_tran_dt.Split('-');
                        string thismonth = datesplit[datesplit.Length - 2].ToString();
                        if (thismonth == "04" || thismonth == "05" || thismonth == "06" || thismonth == "07")
                        {
                            quarter = 1;
                        }
                        if (thismonth == "08" || thismonth == "09" || thismonth == "10" || thismonth == "11")
                        {
                            quarter = 2;
                        }
                        if (thismonth == "12" || thismonth == "01" || thismonth == "02" || thismonth == "03")
                        {
                            quarter = 3;
                        }
                        //INSERT NHRS_DTCO_PAYMENT_DTL

                        qr = service.SubmitChanges("PR_DTCO_PAYMENT_DATA",
                         "I",
                         DBNull.Value,
                         paramTable.Rows[i]["BANK_CODE (As per MIS)"].ConvertToString().Trim(),
                         pa_no.ConvertToString(),
                         paramTable.Rows[i]["BENEFICIARY_NAME"].ConvertToString(),
                         "",//tole
                         0,//paramTable.Rows[i]["BATCH"].ToDecimal(),
                         0,//dcode
                         0,//vcode
                         paramTable.Rows[i]["VDC/MUN"].ConvertToString(),
                         nra_district.ToDecimal(),
                         nra_vdc.ToDecimal(),
                         nra_ward.ToDecimal(),
                         cheque_tran_date,
                         0,//PO_REQUEST_NO
                         paramTable.Rows[i]["BANK_NAME"].ConvertToString(),
                         paramTable.Rows[i]["BANK_CODE (As per MIS)"].ConvertToString().Trim(),
                         paramTable.Rows[i]["AMOUNT"].ConvertToString(),
                         paramTable.Rows[i]["CHEQUE_NO"].ConvertToString(),
                         0,//doc_cd
                         "",//doc_name
                         "",//account_name
                         batchID.ToDecimal(),
                         SessionCheck.getSessionUsername().ConvertToString(),
                         DateTime.Now.ToShortDateString(),
                         fiscalyr.ToString(),
                         nepali_cheque_tran_dt.ConvertToString(),
                         quarter.ToDecimal()
                         );

                    }

                }

                catch (OracleException oe)
                {
                    int errorPoint = chkPoint;
                    res = true;
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);
                    string cmdText = String.Format("update NHRS_DTCO_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_DTCO_PAYMENT_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                }
                catch (Exception ex)
                {
                    int errorPoint = chkPoint;
                    res = true;
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);

                    string cmdText = String.Format("update NHRS_DTCO_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_DTCO_PAYMENT_DTL where FILE_BATCH_ID='" + batchID + "'");
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }
                return res;
            }
        }

        public QueryResult DeleteDTCODupData()
        {
            QueryResult stat = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "DELETE FROM nhrs_dtco_payment_dtl WHERE rowid not in " +
                          " (SELECT MIN(rowid) FROM nhrs_dtco_payment_dtl " +
                          "  GROUP BY VICTIM_CODE, BANK_CD, cheque_tran_edate,CHEQUE_NO,payroll_install_cd) ";
                try
                {
                    stat = service.SubmitChanges(cmdText, null);

                }
                catch (Exception ex)
                {
                    stat = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return stat;
        }
        public DataTable GetImportFileByDistrictVDC(string district)
        {
            DataTable dtbl = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "NHRS.PKG_DTCO_PAYMENT1";
                    dtbl = service.GetDataTable(true, "PR_GET_DTCO_UPLOAD_FILE",
                         district.ToDecimal(),
                        DBNull.Value);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }
        public QueryResult DeleteErrorData(int dtco_payment_id)
        {
            QueryResult qr = new QueryResult();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "NHRS.PKG_DTCO_PAYMENT1";

                    qr = service.SubmitChanges("PR_DELETE_ERROR_DATA",
                                           dtco_payment_id
                                         );

                }
                catch (OracleException ox)
                {
                    ExceptionHandler.ExceptionManager.AppendLog(ox);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }

                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return qr;
        }
        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_DTCO_FILE_BATCH WHERE STATUS='Completed'";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILENAME")).ToList();
            }
            return lstStr;
        }
        public List<string> JSONdbcolumnInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT  column_name FROM USER_TAB_COLUMNS WHERE table_name = 'NHRS_DTCO_PAYMENT_DTL'";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("column_name")).ToList();
            }
            return lstStr;
        }
        public bool CheckIfPAExists(string NRADefinedCode)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();

                cmdText = "select * from VW_BENEF_ALL WHERE NRA_DEFINED_CD='" + NRADefinedCode + "' ";

                try
                {
                    dt = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }
            return res;
        }
        public bool CheckDuplicate(string NRADefinedCode, string amount)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_DTCO_PAYMENT_DTL where 1=1";

                if (NRADefinedCode != "")
                {
                    cmdText += " and upper(VICTIM_CODE) ='" + NRADefinedCode.ToUpper() + "'";
                }

                if (amount != "")
                {
                    cmdText += " and upper(AMOUNT) ='" + amount.ToUpper() + "'";
                }
                //if (tranch != "")
                //{
                //    cmdText += " and upper(TRANCH) ='" + tranch.ToUpper() + "'";
                //}

                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                res = true;

            }

            return res;
        }
        public Boolean saveduplicateinDB(string nra_district, string nra_vdc, string nra_ward, string batch, string beneficiary_name, string cheque_tran_date, string po_request_no,
                                        string bank_cd, string bank_name, string pa_no, string tole_area, string cheque_no, string account_holder, string doc_cd, string amount,
                                        string doc_no, string entered_by, string entered_dt, string File_Name, string d_code, string v_code, string fiscal_yr)
        {

            QueryResult qr = null;
            bool res = false;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_DTCO_PAYMENT1";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qr = service.SubmitChanges("PR_DTCO_DUPLICATE_DATA",
                                          "I", DBNull.Value, bank_cd.ToDecimal(), pa_no,
                                          beneficiary_name,
                                          tole_area, batch.ToDecimal(), d_code.ToDecimal(), v_code.ToDecimal(), nra_district.ToDecimal(),
                                          nra_vdc.ToDecimal(), nra_ward.ToDecimal(), cheque_tran_date,
                                          po_request_no,
                                          bank_name, bank_cd.ToDecimal(), amount, cheque_no,
                                          doc_cd.ToDecimal(), doc_no,
                                          account_holder, File_Name, entered_by,
                                          entered_dt, fiscal_yr

                                         );

                }


                catch (OracleException oe)
                {
                    res = true;
                }
                catch (Exception ex)
                {
                    res = true;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }

                return res;

            }
        }
        public Boolean saveDuplicateFromFile(string beneficiary_name, string cheque_tran_date, string po_request_no,
                                            string bank_cd, string bank_name, string pa_num, string cheque_no, string account_holder, string amount, string entered_by,
                                              string entered_dt, string File_Name)
        {

            QueryResult qr = null;
            bool res = false;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.PackageName = "NHRS.PKG_DTCO_PAYMENT1";
                    service.Begin();
                    var CurrentDate = DateTime.Now;

                    qr = service.SubmitChanges("PR_DTCO_DUP_FILE_DATA",
                                      "I", DBNull.Value, bank_cd.ToDecimal(), pa_num,
                                      beneficiary_name, cheque_tran_date, po_request_no,
                                      bank_name, bank_cd.ToDecimal(), amount, cheque_no, account_holder,
                                     File_Name, entered_by,
                                          entered_dt

                                    );
                }

                catch (OracleException oe)
                {
                    res = true;
                }
                catch (Exception ex)
                {
                    res = true;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }

                return res;

            }
        }
        public DataTable GetDuplicateData(string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * FROM NHRS_DTCO_DUP_PAYMENT_DTL where 1=1 ";


                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY DTCO_DUP_PAYMENT_ID";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        public DataTable GetDuplicateDataFromFile(string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * FROM NHRS_DTCO_DUP_FILE_DATA where 1=1 ";


                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY DTCO_DUP_ID";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
                        nargs = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        public bool SaveErrorMessgae(string errorMsg)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(errorMsg, null);

                }
                catch (OracleException oe)
                {

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {

                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }

        public DataTable GetErrorList(string district, string vdc)
        {
            DataTable dtbl = new DataTable();

            Object P_DISTRICT = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;

            if (!string.IsNullOrEmpty(district))
            {
                P_DISTRICT = district;
            }

            if (!string.IsNullOrEmpty(vdc))
            {
                P_VDC = vdc;
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "NHRS.PKG_DTCO_PAYMENT1";

                    service.SubmitChanges("PR_TARGET_DTCO_ERROR",
                                               P_DISTRICT.ToDecimal());

                    dtbl = service.GetDataTable(true, "PR_GET_ERROR_LIST",
                                                    P_DISTRICT.ToDecimal(),
                                                    P_VDC.ToDecimal(),
                                                    P_OUT_RESULT
                                                );

                }
                catch (OracleException ox)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ox);
                }
                catch (Exception ex)
                {
                    dtbl = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }

                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }

            return dtbl;
        }

        public BankClaim GetDTCOById(int dtco_payment_id)
        {
            BankClaim obj = new BankClaim();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from nhrs_dtco_payment_dtl where dtco_payment_id = " + dtco_payment_id;
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        obj = null;
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.PA_NO = dt.Rows[0]["VICTIM_CODE"].ConvertToString();
                        obj.Beneficiary_Name = dt.Rows[0]["BENEFICIARY_NAME"].ConvertToString();
                        obj.Dis_Cd = dt.Rows[0]["DISTRICT_CD"].ToInt32();
                        obj.Vdc_Mun_Cd = dt.Rows[0]["VDC_MUN_CD"].ToInt32();
                        obj.Ward_Num = dt.Rows[0]["WARD_NO"].ToInt32();
                        obj.ChequeNo = dt.Rows[0]["CHEQUE_NO"].ConvertToString();
                        obj.Payroll_install_cd = dt.Rows[0]["PAYROLL_INSTALL_CD"].ConvertToString();
                        obj.Bank_cd = Convert.ToInt32(dt.Rows[0]["BANK_CD"]);
                        obj.Cheque_Tran_EDate = dt.Rows[0]["CHEQUE_TRAN_EDATE"].ConvertToString("yyyy-MM-dd");
                        obj.Deposite = Convert.ToInt32(dt.Rows[0]["AMOUNT"]);
                        obj.DTCO_Payment_Id = dtco_payment_id;
                    }

                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            return obj;
        }

        public Boolean UpdateDTCODtl(BankClaim objBankClaim)
        {
            BankClaim objBankClaimdtl = new BankClaim();
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    string NRA_Defined_Cd = objBankClaim.PA_NO;
                    string Beneficiary_Name = objBankClaim.Beneficiary_Name;
                    int Dis_Cd = objBankClaim.Dis_Cd;
                    int Vdc_Mun_Cd = objBankClaim.Vdc_Mun_Cd;
                    int Ward_Num = objBankClaim.Ward_Num;
                    string Tranche = objBankClaim.Payroll_install_cd;
                    DateTime cheque_tran_date = Convert.ToDateTime(objBankClaim.Cheque_Tran_EDate);

                    int Bank_cd = objBankClaim.Bank_cd;
                    string cheque_no = objBankClaim.ChequeNo;
                    int dtco_id = objBankClaim.DTCO_Payment_Id;
                    string updated_by = objBankClaim.Updated_By;
                    DateTime updated_dt = objBankClaim.UpdatedDt;

                    service.Begin();
                    qr = service.SubmitChanges("PR_UPDATE_DTCO_DTL",
                                            NRA_Defined_Cd,
                                            Beneficiary_Name,
                                            Dis_Cd,
                                            Vdc_Mun_Cd,
                                            Ward_Num,
                                            Tranche,
                                            cheque_no,
                                            cheque_tran_date,
                                            Bank_cd,
                                            dtco_id,
                                            updated_by,
                                            updated_dt
                                        );

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;
        }

        public bool deleteUploadData(string batchId)
        {
            bool res = false;
            QueryResult qr = null;
            string cmdText = "Delete from nhrs_dtco_file_batch where batch_id = " + batchId;
            string cmdText1 = "Delete from nhrs_dtco_payment_dtl where file_batch_id = " + batchId;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(cmdText, null);
                    qr = service.SubmitChanges(cmdText1, null);
                }
                catch (OracleException oe)
                {

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {

                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }
    }
}
