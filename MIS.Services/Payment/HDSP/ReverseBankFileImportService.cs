using EntityFramework;
using ExceptionHandler;
using MIS.Models.Payment.HDSP;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;


namespace MIS.Services.Payment.HDSP
{
    public class ReverseBankFileImportService
    {
        public int BulkUploadPSP(string xmlData, out string exc)
        {
            int result = 0;
            exc = string.Empty;
            //connection string
            ServiceFactory service = new ServiceFactory();
            service.Begin();
            var conStr = service.Connection.ConnectionString;
            conStr = conStr + "Pwd=NHRS";

            using (System.Data.OracleClient.OracleConnection con = new System.Data.OracleClient.OracleConnection(conStr))
            {
                con.Open(); //opening database connection 
                System.Data.OracleClient.OracleCommand DbUpdateCommand = con.CreateCommand();
                try
                {
                    DbUpdateCommand.CommandText = "xml_to_bank_table"; //assigning procedure name
                    DbUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    var xmlParam = new System.Data.OracleClient.OracleParameter("p_xml", System.Data.OracleClient.OracleType.Clob); 
                    DbUpdateCommand.Parameters.Add(xmlParam);
                    DbUpdateCommand.Transaction = con.BeginTransaction();

                    var outParam = new System.Data.OracleClient.OracleParameter("p_out_batchId", System.Data.OracleClient.OracleType.Number);
                    DbUpdateCommand.Parameters.Add(outParam).Direction = ParameterDirection.Output;

                    xmlParam.Value = xmlData;
                    DbUpdateCommand.ExecuteNonQuery();
                    result = Convert.ToInt32(DbUpdateCommand.Parameters["p_out_batchId"].Value);
                }
                catch (OracleException oe)
                {
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                }
                catch (Exception ex)
                {
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    DbUpdateCommand.Transaction.Commit();
                    service.End();
                    con.Close();
                }
            }

            return result;
        }
        public int BulkUploadOtherTranche(string xmlData, out string exc)
        {
            int result = 0;
            exc = string.Empty;

            ServiceFactory service = new ServiceFactory();
            service.Begin();
            var conStr = service.Connection.ConnectionString;
            conStr = conStr + "Pwd=NHRS";

            using (System.Data.OracleClient.OracleConnection con = new System.Data.OracleClient.OracleConnection(conStr))
            {
                con.Open(); //opening database connection 
                System.Data.OracleClient.OracleCommand DbUpdateCommand = con.CreateCommand();
                try
                {
                    DbUpdateCommand.CommandText = "PR_PAYMENT_OTHER_BULK"; //assigning procedure name
                    DbUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    var xmlParam = new System.Data.OracleClient.OracleParameter("p_xml", System.Data.OracleClient.OracleType.Clob); 
                    DbUpdateCommand.Parameters.Add(xmlParam);

                    var outParam = new System.Data.OracleClient.OracleParameter("p_out_batchId", System.Data.OracleClient.OracleType.Number);
                    DbUpdateCommand.Parameters.Add(outParam).Direction  = ParameterDirection.Output;

                    DbUpdateCommand.Transaction = con.BeginTransaction();

                    xmlParam.Value = xmlData;
                    DbUpdateCommand.ExecuteNonQuery();
                    result = Convert.ToInt32(DbUpdateCommand.Parameters["p_out_batchId"].Value);
                }
                catch (OracleException oe)
                {
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                }
                catch (Exception ex)
                {
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    DbUpdateCommand.Transaction.Commit();
                    service.End();
                    con.Close();
                }
            }

            return result;
        }
        public Boolean SaveReverseFeedData(DataTable paramTable, string fileName, string bank_cd, out string exc)
        {
            QueryResult qr = null;
            bool res = false;
            exc = string.Empty;
            string batchID = "";
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            int quarter = 0;
            string nepali_activate_Date = "";
            string fiscalyr = "";
            string ac_activate_date = "";
            string deposited_dt = "";
            string cunt = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_REVERSE_FEED_FILE_BATCH",
                                               "I",
                                              DBNull.Value, //dist_cd.ToDecimal(),
                                              DBNull.Value,// vdc,
                                              DBNull.Value,//  ward.ToDecimal(),
                                                bank_cd.ToDecimal(),
                                                 "Completed",
                                                fileName,//filename                                                 
                                                 CurrentDate,
                                                 SessionCheck.getSessionUsername(),
                                                 DBNull.Value,
                                                 DBNull.Value);

                    //Main Table 
                    batchID = qr["p_BATCH_ID"].ConvertToString();
                    string urlIpAddress = CommonVariables.IPAddress;
                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        cunt = i.ToString();
                        #region PA_NO splits
                        if (paramTable.Rows[i][1].ToString() != "")
                        {
                            string Pa_no = paramTable.Rows[i][1].ToString();
                            string[] splits = Pa_no.Split('-');
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
                        #region Quarter
                        if (paramTable.Rows[i][13].ToString() != "")
                        {
                            if (paramTable.Rows[i][16].ToString() != "")
                            {
                                deposited_dt = ((paramTable.Rows[i][16]).ConvertToString("MM/dd/yyyy"));
                            }
                            else
                            {
                                deposited_dt = null;
                            }


                            ac_activate_date = ((paramTable.Rows[i][13]).ConvertToString("MM/dd/yyyy"));
                            fiscalyr = CommonFunction.GetRecentFiscalYear(ac_activate_date);
                            nepali_activate_Date = NepaliDate.getNepaliDate(ac_activate_date);

                            string[] datesplit = nepali_activate_Date.Split('-');
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
                        }
                        #endregion

                        qr = service.SubmitChanges("PR_Inspection",
                                     "I",
                                    DBNull.Value,//PAYROLL_ID
                                    paramTable.Rows[i][1].ConvertToString(),// //NRA_DEFINED_CD
                                    paramTable.Rows[i][2].ToDecimal(),//payment_Batch
                                    DBNull.Value,//Payroll_gen_date_loc
                                    paramTable.Rows[i][7].ConvertToString(), //Recipent_Name
                                    nra_district.ToDecimal(),//district
                                    nra_vdc.ToDecimal(),//vdc
                                    nra_ward.ToDecimal(),//ward_no
                                    paramTable.Rows[i][17].ConvertToString().ToUpper(),//is_card_issued
                                    paramTable.Rows[i][18].ConvertToString(),//card_issued_date                       
                                    bank_cd.ToDecimal(),//Bank cd
                                    paramTable.Rows[i][9].ConvertToString(),//bank_name
                                    paramTable.Rows[i][10].ConvertToString(),//bank_branch
                                    DBNull.Value, //a/c Type
                                    paramTable.Rows[i][12].ConvertToString(),//a/c number
                                    "1",//tranch
                                    "50000",//amount
                                    SessionCheck.getSessionUsername().ConvertToString(),//enteredBy
                                    DateTime.Now.ToShortDateString(),//entered_date
                                    SessionCheck.getSessionUsername().ConvertToString(),//updated_by
                                    DateTime.Now.ToShortDateString(),//updated_by
                                    batchID.ToDecimal(),//file_batch_id
                                    paramTable.Rows[i][19].ConvertToString(),//Remarks
                                    paramTable.Rows[i][8].ConvertToString().ToUpper(),//Gender
                                    fiscalyr
                            );

                        // INSERT INTO NHRS FEED DTL
                        string PayrollID = qr["v_PAYROLL_ID"].ConvertToString();

                        qr = service.SubmitChanges("PR_REVERSE_FEED_DTL",
                           'I',
                           DBNull.Value,//FEED_ID
                           DateTime.Now.ToShortDateString(),//FEED_DT
                           DBNull.Value,//FEED_DT_LOC
                        paramTable.Rows[i][1].ToString(), //NRA_DEFINED_CD
                           SessionCheck.getSessionUserCode(),//FEED_GENERATED_USER
                           urlIpAddress,//URL_IPADDRESS
                            //DBNull.Value,//REVERESE_FEED_GEN
                           DateTime.Now.ToShortDateString(),//reverse feed gen date
                           SessionCheck.getSessionUserCode(),//reverse feed gen user
                           SessionCheck.getSessionUsername(),//entered by
                           DateTime.Now.ToShortDateString(),//entered date
                           SessionCheck.getSessionUsername(),//updated by
                           DateTime.Now.ToShortDateString(),//updated date
                           batchID.ToDecimal(),//file_batch_id
                           paramTable.Rows[i][14].ConvertToString(),//tranch
                           paramTable.Rows[i][2].ToInt64()//payment_batch
                            //PayrollID
                            );

                        //INSERT INTO NHRS_BANK_PAYROLL_DTL
                        qr = service.SubmitChanges("PR_BANK_PAYROLL_DTL",
                            'I',
                            DBNull.Value,//transaction_id
                            deposited_dt,
                            DBNull.Value,// nepali_transac_date.ToString(),//transaction_dt
                            bank_cd.ToDecimal(),//bank_cd
                            paramTable.Rows[i][9].ConvertToString(),  //bank_name
                            paramTable.Rows[i][10].ConvertToString(), //bank_branch
                            DBNull.Value,//a/c type
                            paramTable.Rows[i][12].ConvertToString(),  //a/c number
                            paramTable.Rows[i][1].ToString(),   //PA_No                       
                            //paramTable.Rows[i][13].ConvertToString("MM/dd/yyyy"), //a/c activate date
                            ac_activate_date,
                            //paramTable.Rows[i][13].ToDateTime(),
                            nepali_activate_Date.ToString(),//a/c activate date loc
                            "50000", //cr_amount                        
                            deposited_dt,
                            DBNull.Value,// nepali_deposited_date.ToString(),//deposited date loc
                            DBNull.Value,//  withdraw  date loc
                            DBNull.Value,// withdraw  amount
                            DBNull.Value,//withdraw_dt
                            "1",//tranch
                            SessionCheck.getSessionUsername(),  //ENTERED_BY
                            DateTime.Now.ToShortDateString(),  //ENTER_DT
                            SessionCheck.getSessionUsername(),   //LAST_UPDATED_BY
                            DateTime.Now.ToShortDateString(),    //LAST_UPDATED_DT                         
                            paramTable.Rows[i][19].ConvertToString(), //REMARKS                     
                            batchID.ToDecimal(),   //FEED_BATCH_FILE
                            DBNull.Value,  //   reproting_dt                           
                            DBNull.Value,  //reporting_dt_loc
                            paramTable.Rows[i][2].ToInt64(),   //Batch_Lot
                            PayrollID.ToDecimal(),
                            nra_district.ToDecimal(),//district
                            nra_vdc.ToDecimal(),//vdc
                            nra_ward.ToDecimal(),//ward_no
                            paramTable.Rows[i][17].ConvertToString().ToUpper(),//is_card_issued
                            paramTable.Rows[i][18].ConvertToString(),//card_issued_date 
                            quarter.ToDecimal(),
                            paramTable.Rows[i][11].ConvertToString(),  //branch_cd
                            fiscalyr,
                            paramTable.Rows[i][7].ConvertToString() //Recipent_Name
                        );

                    }

                }

                catch (OracleException oe)
                {
                    res = true;
                    var dv = cunt;
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                    string cmdText = String.Format("update NHRS_REVERSE_FEED_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_FEED_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_BANK_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

                }
                catch (Exception ex)
                {
                    res = true;
                    var dv = cunt;
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);

                    string cmdText = String.Format("update NHRS_REVERSE_FEED_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_FEED_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_BANK_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

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

                var cdf = cunt;
                return res;

            }
        }
        public Boolean SaveSecondTranche(DataTable paramTable, string fileName, string bank_cd, string branch_standard_cd, out string exc)
        {
            QueryResult qr = null;
            bool res = false;
            exc = string.Empty;
            string batchID = "";
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            int quarter = 0;
            string nepali_dep_Date = "";
            string fiscalyr = "";
            string deposited_dt = "";
            string cunt = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_REVERSE_FEED_FILE_BATCH",
                                               "I",
                                              DBNull.Value, //dist_cd.ToDecimal(),
                                              DBNull.Value,// vdc,
                                              DBNull.Value,//  ward.ToDecimal(),
                                              bank_cd.ToDecimal(),
                                              "Completed",
                                              fileName,//filename                                                 
                                              CurrentDate,
                                              SessionCheck.getSessionUsername(),
                                              DBNull.Value,
                                              DBNull.Value);

                    //Main Table 
                    batchID = qr["p_BATCH_ID"].ConvertToString();
                    string urlIpAddress = CommonVariables.IPAddress;
                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        cunt = i.ToString();
                        #region PA_NO splits
                        if (paramTable.Rows[i][2].ToString() != "")
                        {
                            string Pa_no = paramTable.Rows[i][2].ToString();
                            string[] splits = Pa_no.Split('-');
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
                        #region Quarter
                        if (paramTable.Rows[i][8].ToString() != "")
                        {

                            deposited_dt = ((paramTable.Rows[i][8]).ConvertToString("MM/dd/yyyy"));
                            fiscalyr = CommonFunction.GetRecentFiscalYear(deposited_dt);
                            nepali_dep_Date = NepaliDate.getNepaliDate(deposited_dt);

                            string[] datesplit = nepali_dep_Date.Split('-');
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
                        }
                        #endregion



                        //INSERT INTO NHRS_BANK_PAYROLL_DTL
                        qr = service.SubmitChanges("PR_BANK_PAYROLL_DTL",
                            'I',
                            DBNull.Value,//transaction_id
                            deposited_dt,
                            DBNull.Value,// nepali_transac_date.ToString(),//transaction_dt
                            bank_cd.ToDecimal(),//bank_cd
                            DBNull.Value,  //bank_name
                            DBNull.Value, //bank_branch
                            DBNull.Value,//a/c type
                            DBNull.Value,  //a/c number
                            paramTable.Rows[i][2].ToString(),   //PA_No                       
                            //paramTable.Rows[i][13].ConvertToString("MM/dd/yyyy"), //a/c activate date
                            DBNull.Value,
                            //paramTable.Rows[i][13].ToDateTime(),
                            DBNull.Value,//a/c activate date loc
                            "150000", //cr_amount                        
                            deposited_dt,
                            nepali_dep_Date,// nepali_deposited_date.ToString(),//deposited date loc
                            DBNull.Value,//  withdraw  date loc
                            DBNull.Value,// withdraw  amount
                            DBNull.Value,//withdraw_dt
                            "2",//tranch
                            SessionCheck.getSessionUsername(),  //ENTERED_BY
                            DateTime.Now.ToShortDateString(),  //ENTER_DT
                            SessionCheck.getSessionUsername(),   //LAST_UPDATED_BY
                            DateTime.Now.ToShortDateString(),    //LAST_UPDATED_DT                         
                            paramTable.Rows[i][9].ConvertToString(), //REMARKS                     
                            batchID.ToDecimal(),   //FEED_BATCH_FILE
                            DBNull.Value,  //   reproting_dt                           
                            DBNull.Value,  //reporting_dt_loc
                            DBNull.Value,   //Batch_Lot
                            DBNull.Value,
                            nra_district.ToDecimal(),//district
                            nra_vdc.ToDecimal(),//vdc
                            nra_ward.ToDecimal(),//ward_no
                             DBNull.Value,//is_card_issued
                             DBNull.Value,//card_issued_date 
                            quarter.ToDecimal(),
                            branch_standard_cd,  //branch_cd
                            fiscalyr,
                            paramTable.Rows[i][3].ConvertToString() //Recipent_Name
                        );

                    }

                }

                catch (OracleException oe)
                {
                    res = true;
                    var dv = cunt;
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                    string cmdText = String.Format("update NHRS_REVERSE_FEED_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + oe.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_FEED_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_BANK_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

                }
                catch (Exception ex)
                {
                    res = true;
                    var dv = cunt;
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);

                    string cmdText = String.Format("update NHRS_REVERSE_FEED_FILE_BATCH set STATUS='Error',ERROR_MESSAGE='" + ex.Message.Replace('\'', ' ') + "' where BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_FEED_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from NHRS_BANK_PAYROLL_DTL where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

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

                var cdf = cunt;
                return res;

            }
        }

        public Boolean BulkUploadPOData(DataTable paramTable, string fileName, string District_CD, string support_cd, out string exc)
        {
            CommonFunction cf = new CommonFunction();
            QueryResult qr = null;
            bool res = false;
            exc = string.Empty;
            string batchID = "";
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            string mld_district = "";
            string mld_vdc = "";
            string recipient_name = "";


            DateTime dep_dt = DateTime.Now;
            string currentUser = SessionCheck.getSessionUsername();
            string usr_cd = SessionCheck.getSessionUserCode();
            string donor_cd = cf.GetDonorCd(usr_cd).ToString();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    #region Save data into Batch Table
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_DONOR_SUPPORT_DTL";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_DONOR_BATCH",
                                                fileName,
                                                "Completed",
                                                District_CD,
                                                currentUser,
                                                dep_dt,
                                                NepaliDate.getNepaliDate(dep_dt.ToString()),
                                                DBNull.Value,
                                                support_cd,
                                                paramTable.Rows[0][11].ConvertToString());
                    #endregion

                    #region Save data into Main Table
                    //Main Table 
                    batchID = qr["P_BATCH_ID"].ConvertToString();

                    for (int i = 0; i < paramTable.Rows.Count; i++)
                    {
                        #region PA_NO splits
                        if (paramTable.Rows[i][1].ToString() != "")
                        {
                            string Pa_no = paramTable.Rows[i][1].ToString();
                            string[] splits = Pa_no.Split('-');
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

                        #region fetch MLD District Code and MLD VDC code
                        mld_district = cf.GetMLDDistCd(nra_district);
                        mld_vdc = cf.GetVdcMunCd(nra_district, nra_vdc);
                        #endregion

                        if (!string.IsNullOrEmpty(paramTable.Rows[i][3].ConvertToString()))
                        {
                            recipient_name = paramTable.Rows[i][3].ConvertToString();
                        }
                        else
                        {
                            recipient_name = paramTable.Rows[i][2].ConvertToString();
                        }

                        //INSERT INTO NHRS_BANK_PAYROLL_DTL
                        qr = service.SubmitChanges("PR_DONOR_SUPPORT_DTL",
                         'I',
                         batchID,
                         DBNull.Value,
                         mld_district,
                         mld_vdc,
                         nra_ward.ToDecimal(),
                         paramTable.Rows[i][7].ConvertToString(),//Area
                         paramTable.Rows[i][8].ConvertToString(),//Household Id
                         paramTable.Rows[i][9].ConvertToString(),//Nissa Number
                         paramTable.Rows[i][1].ConvertToString(),//NRA Defined CD
                         paramTable.Rows[i][11].ConvertToString(),//Donor CD
                         support_cd,
                         DBNull.Value,//Bank Code
                          DBNull.Value,//Branch Standard Code
                         'N',//Status
                         currentUser,//Entered By
                         dep_dt,//Entered Date
                         NepaliDate.getNepaliDate(dep_dt.ToString()),//Entered Local Date
                         DBNull.Value,//Updated By
                         DBNull.Value,//Updated Dt
                         DBNull.Value,//Update Dt local
                         DBNull.Value,//Approved By
                         DBNull.Value,//Approved Dt
                         DBNull.Value,//Approved Dt local
                         paramTable.Rows[i][2].ConvertToString(),//Beneficiary Name
                         recipient_name,
                         paramTable.Rows[i][13].ConvertToString(),//Support Amount
                         paramTable.Rows[i][14].ConvertToString()//Remarks
                        );

                    }
                    #endregion

                }

                catch (OracleException oe)
                {
                    res = false;
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);

                    string cmdText = String.Format("delete from nhrs_donor_batch where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from nhrs_donor_support_dtl where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

                }
                catch (Exception ex)
                {
                    res = false;
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);

                    string cmdText = String.Format("delete from nhrs_donor_batch where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);
                    cmdText = String.Format("delete from nhrs_donor_support_dtl where FILE_BATCH_ID='" + batchID + "'");
                    SaveErrorMessgae(cmdText);

                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

                if (qr != null)
                {
                    res = qr.IsSuccess;
                }


                return res;

            }
        }

        public int GetInstallmentCd(string support)
        {
            int install_cd = 0;
            if (support.ToString().Trim() == "50000" || support.ToString().Trim() == "50,000")
            {
                install_cd = 1;
            }
            if (support.ToString().Trim() == "150000" || support.ToString().Trim() == "150,000")
            {
                install_cd = 2;
            }
            if (support.ToString().Trim() == "100000" || support.ToString().Trim() == "100,000")
            {
                install_cd = 3;
            }
            if (support.ToString().ToUpper().Trim() == "MATERIAL SUPPORT")
            {
                install_cd = 4;
            }
            if (support.ToString().ToUpper().Trim() == "MODEL HOUSE")
            {
                install_cd = 5;
            }

            return install_cd;
        }

        public string GetConflictedBatchId()
        {
            EnrollmentMou mouenroll = new EnrollmentMou();
            string batchId = "";
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    string cmdText = "SELECT NVL(MAX(CONFLICTED_BATCH_ID), 0) + 1 FROM NHRS_BANKTONRA_CONFLICTED";
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
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
            if (dt.Rows.Count > 0)
            {
                batchId = dt.Rows[0][0].ToString();
            }
            return batchId;
        }
        public bool SaveConflictedRecord(string paNumber, string fileName, string reason, int batchId)
        {
            bool result = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    //SAVE BATCH INFO HERE
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    service.Begin();
                    var CurrentDate = DateTime.Now;
                    qr = service.SubmitChanges("PR_NHRS_BANKTONRA_CONFLICTED ",
                        "I",
                                              DBNull.Value,
                                              fileName,
                                              paNumber,
                                              reason,
                                              System.DateTime.Now,
                                              System.DateTime.Now.ConvertToString(),
                                              SessionCheck.getSessionUsername(),//entered by
                                              batchId
                                       );
                }
                catch (OracleException oe)
                {
                    result = true;
                }
                catch (Exception ex)
                {
                    result = true;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (result)
                {
                    result = false;
                }
                else
                {
                    if (qr != null)
                    {
                        result = qr.IsSuccess;
                    }
                }
            }

            return result;
        }
        public Boolean savedatanotfound(string dist, string vdc, string ward, string benef_name, string reciepient_name, string grand_father_name, string father_name,
                    string bank_name, string branch_name, string Account_type, string pa_num, string is_card_issued, string card_issued_dt, string Account_Activated_dt,
                      string amount, string withdraw_amount, string tranch, string mobile_num, string entered_by, string entered_dt, string remarks,
                      string Account_no, string last_transaction_dt, string deposited_dt, string citz_no, string batch_lot, string File_Name)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qr = null;
            bool res = false;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qr = service.SubmitChanges("PR_BANK_DATA_NOTFOUND",
                                               "I", DBNull.Value, DBNull.Value,
                                               dist,
                                               vdc,
                                               ward,
                                               benef_name,
                                               reciepient_name,
                                               grand_father_name,
                                               father_name,
                                               bank_name,
                                               branch_name,
                                               Account_type,
                                               pa_num,
                                               is_card_issued,
                                               card_issued_dt,
                                               Account_Activated_dt,
                                               amount,
                                               withdraw_amount,
                                               tranch,
                                               mobile_num,
                                               entered_by,
                                               entered_dt,
                                               remarks,
                                               Account_no,
                                               last_transaction_dt,
                                               deposited_dt,
                                               citz_no,
                                               batch_lot,
                                               File_Name

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

        public Boolean saveduplicateinDB(string dist, string vdc, string ward, string benef_name, string reciepient_name,
                   string bank_name, string branch_name, string pa_num, string is_card_issued, string card_issued_dt, string Account_Activated_dt,
                     string amount, string tranch, string mobile_num, string entered_by, string entered_dt, string remarks,
                     string Account_no, string last_transaction_dt, string deposited_dt, string citz_no, string batch_lot, string File_Name, string bankcd, string branchStdCd,
           string fiscalyear)
        {
            CommonFunction common = new CommonFunction();

            QueryResult qr = null;
            bool res = false;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    service.Begin();
                    var CurrentDate = DateTime.Now;


                    //SAVE File info
                    qr = service.SubmitChanges("PR_BANK_DUPLICATE_DATA",
                                               "I", DBNull.Value, DBNull.Value,
                                               dist.ToDecimal(),
                                               vdc.ToDecimal(),
                                               ward.ToDecimal(),
                                               benef_name,
                                               reciepient_name,
                        // grand_father_name,
                        // father_name,
                                               bank_name,
                                               branch_name,
                        // Account_type,
                                               pa_num,
                                               is_card_issued,
                                               card_issued_dt.ToDateTime(),
                                               Account_Activated_dt.ToDateTime(),
                                               amount,
                        //withdraw_amount,
                                               tranch,
                                               mobile_num,
                                               entered_by,
                                               entered_dt,
                                               remarks,
                                               Account_no,
                                               last_transaction_dt,
                                               deposited_dt.ToDateTime(),
                                               citz_no,
                                               batch_lot.ToDecimal(),
                                               File_Name,
                                               bankcd.ToDecimal(),
                                               branchStdCd,
                                               fiscalyear

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
        public bool CheckDuplicate(string NRADefinedCode, string account, string tranch)
        {
            DataTable dt = new DataTable();


            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_BANK_PAYROLL_DTL where 1=1";

                if (NRADefinedCode != "")
                {
                    cmdText += " and upper(NRA_DEFINED_CD) ='" + NRADefinedCode.ToUpper() + "'";
                }

                if (account != "")
                {
                    cmdText += " and upper(ACCOUNT_NUMBER) ='" + account.ToUpper() + "'";
                }
                if (tranch != "")
                {
                    cmdText += " and upper(TRANCH) ='" + tranch.ToUpper() + "'";
                }

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

        public bool CheckDuplicatePAforPO(string NRADefinedCode, string support_cd, string mode)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = true;

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_DONOR_SUPPORT_DTL where 1=1";

                if (NRADefinedCode != "")
                {
                    cmdText += " and upper(NRA_DEFINED_CD) ='" + NRADefinedCode.ToUpper() + "'";
                }

                if (support_cd != "")
                {
                    cmdText += " and upper(PAYROLL_INSTALL_CD) ='" + support_cd.ToUpper() + "'";
                }


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

            if (mode == "")
            {
                mode = "I";
            }

            if (mode.ToUpper() == "U")
            {
                if (dt.Rows.Count > 1)
                {
                    res = false;

                }
            }
            else
            {
                if (dt.Rows.Count >= 1)
                {
                    res = false;

                }
            }

            return res;
        }

        public bool CheckDuplicate01(string NRADefinedCode, string account, string tranch)
        {
            DataTable dt = new DataTable();


            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT * FROM NHRS_BANK_PAYROLL_DTL where 1=1";

                if (NRADefinedCode != "")
                {
                    cmdText += " and upper(NRA_DEFINED_CD) ='" + NRADefinedCode.ToUpper() + "'";
                }

                if (account != "")
                {
                    cmdText += " and upper(ACCOUNT_NUMBER) ='" + account.ToUpper() + "'";
                }
                if (tranch != "")
                {
                    cmdText += " and upper(TRANCH) ='" + tranch.ToUpper() + "'";
                }

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
            if (dt.Rows.Count > 1)
            {
                res = true;

            }

            return res;
        }
        public bool CheckIfPAExists(string NRADefinedCode)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;

            if (NRADefinedCode != "")
            {
                cmdText = "select * from VW_BENEF_ALL WHERE NRA_DEFINED_CD='" + NRADefinedCode + "' ";

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

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
            }

            if (dt.Rows.Count > 0)
            {
                res = true;
            }
            return res;
        }
        public DataTable GetDuplicatePaDetail(string pa_number)
        {
            DataTable dt = new DataTable();
            string cmdText = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();

                try
                {
                    dt = service.GetDataTable(true, "PR_PAYMENT_ROOSTER",
                             pa_number,
                             DBNull.Value);
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

            return dt;
        }
        public DataTable PARoosterPO(string pa_number)
        {
            DataTable dt = new DataTable();
            string cmdText = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from VW_PO_ROOSTER WHERE NRA_DEFINED_CD= '" + pa_number + "' ";
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

            return dt;
        }
        public bool ChkIfBranchCd(string branchstdcode, string bankcd)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_BANK_BRANCH WHERE BRANCH_STD_CD='" + branchstdcode + "' and BANK_CD='" + bankcd + "'";
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
        public bool CheckIfPAExistsInMst(string NRADefinedCode)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            bool res = false;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ENROLLMENT_MST WHERE NRA_DEFINED_CD='" + NRADefinedCode + "'";
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
        public DataTable GetDumpData(string fileName)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * FROM NHRS_BANK_PAYROLL_DUMP_DTL where 1=1 ";


                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY TRANSACTON_ID";
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
        public DataTable GetDuplicateData(string fileName, int batchId)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                string cmd = "SELECT * FROM NHRS_BANKTONRA_CONFLICTED where 1=1 AND CONFLICTED_BATCH_ID =" + batchId;


                if (fileName != null && fileName != "")
                {
                    cmd = cmd + "AND FILE_NAME='" + fileName.ConvertToString() + "'";
                }
                cmd = cmd + "  ORDER BY CONFLICT_ID desc";
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
        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_REVERSE_FEED_FILE_BATCH WHERE STATUS='Completed'";
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
        public System.Data.DataTable GetBankInfoImportFile(string BankCd, string installment)
        {
            DataTable dtbl = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    dtbl = service.GetDataTable(true, "PR_GET_BANK_IMPORT_FILE",
                         BankCd.ToDecimal(),//bankd
                            installment,
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

        public DataTable GetPOUploadFiles(string donor_cd, string payroll_install_cd)
        {
            DataTable dtbl = new DataTable();
            //string query = "Select Distinct ndb.FileName,ndb.BATCH_ID,ndb.ENTERED_BY,ndb.STATUS,ndb.ENTERED_DT from NHRS_DONOR_BATCH ndb " +
            //                "where ndb.PAYROLL_INSTALL_CD = '" + payroll_install_cd + "' AND ndb.DONOR_CD = '" + donor_cd + "' ";


            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    dtbl = service.GetDataTable(true, "PR_GET_PO_FILES",
                                                       payroll_install_cd.ToDecimal(),
                                                       donor_cd.ToDecimal(),
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

        public QueryResult deleteUploadData(string batchid)
        {
            QueryResult qr = new QueryResult();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();

                try
                {
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    qr = service.SubmitChanges("PR_DELETE_IMPORT_FILE", "D",
                         batchid.ToDecimal()//bankd

                        );

                }
                catch (OracleException oe)
                {
                    service.RollBack();
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

                return qr;
            }
        }

        public QueryResult DeletePOUploadedFiles(string batchid)
        {
            QueryResult qr = new QueryResult();
            QueryResult qr1 = new QueryResult();
            string query = "Delete from NHRS_DONOR_BATCH WHERE BATCH_ID = " + batchid;
            string query1 = "Delete from NHRS_DONOR_Support_DTL WHERE BATCH_ID= " + batchid;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(query, null);
                    qr1 = service.SubmitChanges(query1, null);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
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

                return qr;
            }
        }

        public QueryResult DeleteErrorData(string payroll_id, string bank_cd, string payroll_dtl_id, string pa_number)
        {
            QueryResult qr = new QueryResult();
            QueryResult qr1 = new QueryResult();
            QueryResult qr2 = new QueryResult();

            string query = "Delete from NHRS_BANK_PAYROLL_DTL WHERE BANK_PAYROLL_ID = " + payroll_id;
           

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(query, null);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
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

                return qr;
            }
        }

        public QueryResult AddTrancheData(List<BankTrancheData> pa_num)
        {
            QueryResult quryResult = new QueryResult();
            string PA_NUM = "";
            string Deposite_Date = "";
            string tranche = "";
            string approved_by = "";
            DataTable dtt = new DataTable();
            string Dep_Date_LOC = "";
            int quarter = 1;
            string fiscalyr = "";
            string P_ENTERED_BY = CommonVariables.UserName;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    approved_by = SessionCheck.getSessionUsername();

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    foreach (var item in pa_num)
                    {
                        PA_NUM = item.PA_NUM;
                        Deposite_Date = item.DEPOSITE_DATE;
                        tranche = item.Tranche;
                        Dep_Date_LOC = NepaliDate.getNepaliDate(Deposite_Date);
                        fiscalyr = CommonFunction.GetRecentFiscalYear(Deposite_Date);


                        string[] datesplit = Dep_Date_LOC.Split('-');
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

                        quryResult = service.SubmitChanges("PR_ADD_OTH_TRANCHE",
                                                           tranche,
                                                           PA_NUM,
                                                           Deposite_Date.ToDateTime(),
                                                           Dep_Date_LOC,
                                                           fiscalyr,
                                                           P_ENTERED_BY,
                                                           quarter
                                                          );
                    }


                }

                catch (OracleException oe)
                {
                    service.RollBack();
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

            return quryResult;

        }

        public QueryResult UpdateTrancheData(string pa, string depositeDate, string tranche)
        {
            QueryResult quryResult = new QueryResult();
            string PA_NUM = "";
            string Deposite_Date = "";

            string approved_by = "";
            DataTable dtt = new DataTable();
            string Dep_Date_LOC = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    approved_by = SessionCheck.getSessionUsername();

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";

                    PA_NUM = pa;
                    Deposite_Date = depositeDate;

                    Dep_Date_LOC = NepaliDate.getNepaliDate(Deposite_Date);
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    quryResult = service.SubmitChanges("PR_ADD_SECOND_TRANCHE",
                                                       tranche,
                                                       PA_NUM,
                                                       Deposite_Date,
                                                       Dep_Date_LOC
                                                      );
                }




                catch (OracleException oe)
                {
                    service.RollBack();
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

            return quryResult;

        }

        public List<string> POFilesList()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILENAME FROM NHRS_DONOR_BATCH WHERE STATUS='Completed'";
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
        public DataTable GetSecondTrancheError(string bank_cd, string branch_cd,int batchId,string tranche)
        {
            DataTable dt = new DataTable();
            Object P_OUT_RESULT = DBNull.Value;
            Object P_BANK_CD = DBNull.Value;
            Object P_BRANCH_CD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_TRANCHE = DBNull.Value;

            List<string> ErrorFileBatchId = new List<string>();
            List<string> ListErrorFiles = new List<string>();
            QueryResult qr = new QueryResult();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();

            if (bank_cd != "")
            {
                P_BANK_CD = bank_cd;
            }
            if (tranche != "")
            {
                P_TRANCHE = tranche;
            }
            if (branch_cd != "" && branch_cd != "---Select Branch---")
            {
                P_BRANCH_CD = branch_cd;
            }
            if ( batchId > 0 && batchId != null)
            {
                P_BATCH_ID = batchId;
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.SubmitChanges("PR_PAYMENT_OTHER_ERROR",
                                           P_BANK_CD,
                                           P_TRANCHE);

                    service.PackageName = "PKG_REVERSE_FEED";
                    dt = service.GetDataTable(true, "PR_NHRS_GET_ERROR_LIST",
                                                     bank_cd,
                                                     P_BRANCH_CD,
                                                     P_TRANCHE,
                                                     P_BATCH_ID,
                                                     P_OUT_RESULT
                                                     );



                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
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


            return dt;
        }
        
        public int CheckDuplicateData(string bankId)
        {
            int result = 0;
            string cmdTxt = "SELECT count(*) FROM nhrs_bank_payroll_dtl " +
                            "GROUP BY nra_defined_cd, BANK_CD, branch_std_cd, tranch, account_number , " +
                            "ac_activated_dt, deposited_dt, recipient_name having count(nra_defined_cd) > 1";

           var dt = new DataTable();

           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(new
                   {
                       query = cmdTxt,
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
           
           if(dt.Rows.Count != 0)
           {
               result = dt.Rows[0][0].ToInt32();
           }
           else
           {
               result = 0;
           }
           return result;
        }

        public QueryResult DeletePaymentDuplicateData(string bankId)
        {
            QueryResult stat = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "";
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
    }

}


