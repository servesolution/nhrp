using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Models.Enrollment;
using MIS.Services.Core;
using MIS.Models.Payment.HDSP;
using System.Web.Mvc;
using MIS.Models.Core;
using MIS.Services.Payment.HDSP;
using System.Text.RegularExpressions;
using MIS.Models.Security;

namespace MIS.Services.Enrollment
{
    public class EnrollmentImportExport
    {
        public DataTable GetBankList(string DistrictCd, string VDCMun)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;


            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_GET_ENROLLED_DATA",
                                                     DistrictCd,
                                                     VDCMun,
                                                     P_Out_Enrollment
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

        public QueryResult DeleteBranchUser(Users u)
        {
            QueryResult qr1 =  new QueryResult();
            QueryResult qr2 =  new QueryResult();
            QueryResult qr3 = new QueryResult();
            string cmd =  "DELETE FROM NHRS_BANK_USERS WHERE USR_CD = " + u.usrCd;
           // string cmd1 = "DELETE FROM COM_WEB_USR_GRP WHERE USR_CD = " + u.usrCd;
           // string cmd2 = "DELETE FROM COM_WEB_USR WHERE USR_CD = " + u.usrCd;

            using (ServiceFactory service02 = new ServiceFactory())
                {
                    try
                    {
                        service02.Begin();
                        qr1 = service02.SubmitChanges(cmd , null);
                       // qr2 = service02.SubmitChanges(cmd1 , null);
                      //  qr3 = service02.SubmitChanges(cmd2, null);
                    }
                    catch (OracleException oe)
                    {
                        service02.RollBack();
                        ExceptionManager.AppendLog(oe);
                    }
                    catch (Exception ex)
                    {
                        service02.RollBack();
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {

                        if (service02.Transaction != null)
                        {
                            service02.End();
                        }
                    }


                }
            return qr1;
        }

        public QueryResult UpdateTranchePO(PaymentPartnerOrganization objPO)
        {
            QueryResult qr1 =  new QueryResult();
            string cmd = "UPDATE NHRS_DONOR_SUPPORT_DTL SET SUPPORT_AMOUNT = "+objPO.Support_Amount+ " WHERE "+
                         " NRA_DEFINED_CD = '" + objPO.PA_NO +"'  AND PAYROLL_INSTALL_CD = "+objPO.payroll_install_cd;
          
            using (ServiceFactory service02 = new ServiceFactory())
                {
                    try
                    {
                        service02.Begin();
                        qr1 = service02.SubmitChanges(cmd , null);
                    }
                    catch (OracleException oe)
                    {
                        service02.RollBack();
                        ExceptionManager.AppendLog(oe);
                    }
                    catch (Exception ex)
                    {
                        service02.RollBack();
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {

                        if (service02.Transaction != null)
                        {
                            service02.End();
                        }
                    }


                }
            return qr1;
        }
        public QueryResult DeleteTranchePO(string nra_defined_cd,string install_cd)
        {
            QueryResult qr1 = new QueryResult();
          
            using (ServiceFactory service02 = new ServiceFactory())
            {
                try
                {
                    service02.Begin();
                    qr1 = service02.SubmitChanges("PR_DELETE_PO", nra_defined_cd, install_cd);
                
                }
                catch (OracleException oe)
                {
                    service02.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service02.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {

                    if (service02.Transaction != null)
                    {
                        service02.End();
                    }
                }


            }
            return qr1;
        }
        public string GetDonorCdFromUsrCd(string user_cd)
        {
            DataTable dt = new DataTable();
            string cmd = "Select DONOR_CD from COM_WEB_USR where USR_CD =" + user_cd;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
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
            return dt.Rows[0][0].ToString();
        }
        public QueryResult InsertSecondThirdInstallment(List<PaymentPartnerOrganization> packages)
        {
            QueryResult qResult = null;

            CommonFunction fc = new CommonFunction();
            Object P_SUPPORT_CD = DBNull.Value;
            Object P_PAYROLL_INSTALL_CD = DBNull.Value;
            string P_ENTERED_BY = CommonVariables.UserName;
            DateTime P_ENTERED_DT = DateTime.Now;
            string P_ENTERED_DT_LOC = NepaliDate.getNepaliDate(P_ENTERED_DT.ToString());
            Object P_OUT_RESULT = DBNull.Value;
           
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    foreach (var item in packages)
                    {
                        qResult = service.SubmitChanges("PR_PO_TRANCHE",
                            item.Support_CD,
                            item.payroll_install_cd,
                            item.Support_Amount,
                            P_ENTERED_BY,
                            P_ENTERED_DT,
                            P_ENTERED_DT_LOC
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

            return qResult;
        }
        public BankClaim GetBankClaimbyId(string bank_payroll_cd, string installment)
        {
            BankClaim obj = new BankClaim();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_BANK_PAYROLL_DTL where BANK_PAYROLL_ID = '" + bank_payroll_cd + "' and payroll_install_cd=" + installment + " ";
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
                        obj.PA_NO = dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                        obj.Bank_Name = dt.Rows[0]["BANK_NAME"].ConvertToString();
                        obj.Dis_Cd = dt.Rows[0]["DISTRICT_CD"].ToInt32();
                        obj.Vdc_Mun_Cd = dt.Rows[0]["VDC_MUN_CD"].ToInt32();
                        obj.Ward_Num = dt.Rows[0]["WARD_NO"].ToInt32();
                        obj.Reciepient_Name = dt.Rows[0]["RECIPIENT_NAME"].ConvertToString();
                        obj.TRANSACTON_ID = Convert.ToInt32(dt.Rows[0]["TRANSACTON_ID"]);
                        obj.Branch_Cd = dt.Rows[0]["BANK_BRANCH_CD"].ToInt32();
                        obj.AccountNo = dt.Rows[0]["ACCOUNT_NUMBER"].ConvertToString();
                        obj.Activation_Date = (dt.Rows[0]["AC_ACTIVATED_DT"]).ConvertToString("yyyy-MM-dd");
                        obj.Tranche = dt.Rows[0]["TRANCH"].ConvertToString();
                        obj.Bank_Payroll_Id = bank_payroll_cd.ToInt32();

                        if (dt.Rows[0]["DEPOSITED_DT"] != null && dt.Rows[0]["DEPOSITED_DT"].ToString() != "")
                        {
                            obj.Deposited_Date = (dt.Rows[0]["DEPOSITED_DT"]).ConvertToString("yyyy-MM-dd");
                        }

                        obj.Remarks = dt.Rows[0]["REMARKS"].ConvertToString();
                        obj.IsCard_Issued = dt.Rows[0]["IS_ATM_CARD_ISSUED"].ToString();

                        obj.Batch = dt.Rows[0]["BATCH_LOT"].ToDecimal();
                        obj.Branch = dt.Rows[0]["BANK_BRANCH_NAME"].ToString();
                        obj.Deposite = dt.Rows[0]["CR_AMOUNT"].ToInt32();
                        obj.ApproveStatus = dt.Rows[0]["APPROVED"].ToString();
                        obj.Bank_cd = Convert.ToInt32(dt.Rows[0]["BANK_CD"]);
                        obj.Branch_Std_Cd = dt.Rows[0]["BRANCH_STD_CD"].ToString();
                        obj.Card_Iss_Date = (dt.Rows[0]["ATM_CARD_ISSUED_DATE"]).ConvertToString();
                        obj.Payroll_ID = (dt.Rows[0]["PAYROLL_DTL_ID"]).ToInt32();
                        obj.SecondTrancheApproved = (dt.Rows[0]["SECONDTRANCHEAPPROVED"]).ToString();
                        obj.ThirdTrancheApproved = (dt.Rows[0]["THIRDTRANCHEAPPROVED"]).ToString();
                        obj.Activation_Date_LOC = dt.Rows[0]["AC_ACTIVATED_DT_LOC"].ToString();
                        obj.Deposited_Date_LOC = dt.Rows[0]["DEPOSITED_DT_LOC"].ConvertToString();

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

        public DataTable GetPaymentSearchList(EnrollmentSearch objEnrollmentSearch)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;

            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_session_id = DBNull.Value;
            Object p_ward_no = DBNull.Value;


            if (!string.IsNullOrEmpty(objEnrollmentSearch.DistrictCd))
            {
                P_district_cd = Decimal.Parse(objEnrollmentSearch.DistrictCd);
            }
            if (!string.IsNullOrEmpty(objEnrollmentSearch.VDCMun))
            {
                P_vdc_mun_cd = Decimal.Parse(objEnrollmentSearch.VDCMun);
            }
            if (!string.IsNullOrEmpty(objEnrollmentSearch.WardNo))
            {
                p_ward_no = Decimal.Parse(objEnrollmentSearch.WardNo);
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_GET_ENROLLED_DATA",
                                                     DBNull.Value,

                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     P_Out_Enrollment
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

        //public QueryResult ApproveEnrollmentPayment(EnrollmentSearch objenrolllPayment)
        public QueryResult ApproveEnrollmentPayment(DataTable dataresult, string filename, string file_path)
        {
            QueryResult quryResult = new QueryResult();
            QueryResult quryResult1 = new QueryResult();
            string error = string.Empty;
            CommonFunction commFunction = new CommonFunction();
            Object v_mode = "I";
            string House_Owner_ID = "";
            string P_NRA_DEFINED_CD = "";
            Object totalNo = DBNull.Value;
            Object P_Out_Enrollment = DBNull.Value;
            string Zone_CD = "";
            string District = "";
            string VDC = "";
            string Ward = "";
            string Bank_CD = "";
            string Branch_CD = "";
            string Tranch = "1st";
            string Payroll_Install = "50000";
            //Object totalNo = DBNull.Value;
            //Object P_Out_Enrollment = DBNull.Value;
            string Approved_By = SessionCheck.getSessionUsername();
            DateTime Transact_DATE = DateTime.Now;
            string Transaction_Date = Transact_DATE.ToString("yyyy-MM-dd");
            DateTime Approved_DATE = DateTime.Now;
            string Approved_Date = Approved_DATE.ToString("yyyy-MM-dd");
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    service.Begin();

                    Zone_CD = dataresult.Rows[0]["ZONE_CD"].ToString();
                    District = dataresult.Rows[0]["DIST_CD"].ToString();
                    VDC = dataresult.Rows[0]["VDC_CD"].ToString();
                    Ward = dataresult.Rows[0]["WARD"].ToString();
                    //Bank_CD = dataresult.Rows[0]["Bank_Cd"].ToString();
                    //Branch_CD = dataresult.Rows[0]["Branch_Cd"].ToString();
                    //Tranch = dataresult.Rows[0]["TRANCHE"].ToString();
                    Approved_By = SessionCheck.getSessionUsername().ToString();
                    string TransactionDate = Transaction_Date;
                    string ApprovedDate = Approved_Date;
                    //Payroll_Install = dataresult.Rows[0]["AMOUNT"].ToString();
                    quryResult1 = service.SubmitChanges("PR_SEC_PAYMENT_APPROVE",
                                              "I",
                                               DBNull.Value,
                                               DBNull.Value,
                                               Zone_CD,
                                               District,
                                               VDC,
                                               Ward,
                                               DBNull.Value,
                                               DBNull.Value,
                                               filename,
                                               Approved_Date,
                                               Approved_By,
                                               Tranch,
                                               file_path,
                                               DBNull.Value,
                                               TransactionDate,
                                               Payroll_Install,
                                               DBNull.Value

                                                 );
                    string transaction_id = quryResult1["p_TRANSACTION_ID"].ToString();
                    for (int i = 0; i < dataresult.Rows.Count; i++)
                    {
                        P_NRA_DEFINED_CD = dataresult.Rows[i]["AGREEMENT"].ConvertToString();
                        House_Owner_ID = dataresult.Rows[i]["HOUSE_OWNER_ID"].ConvertToString();
                        string resPackageName = service.PackageName;
                        quryResult = service.SubmitChanges("PR_APPROVE_ENROLL_PAYMENT",
                                                           P_NRA_DEFINED_CD,
                                                           House_Owner_ID,
                                                           Approved_By

                                                          );
                    }

                }
                catch (OracleException oex)
                {

                    error = oex.Code.ConvertToString();
                    ExceptionManager.AppendLog(oex);
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
        public QueryResult ApproveInspecctedPayment(DataTable dataresult, string filename, string File_Destination)
        {
            QueryResult quryResult = new QueryResult();
            QueryResult quryResult1 = new QueryResult();
            string error = string.Empty;
            //CommonFunction commFunction = new CommonFunction();
            //Object v_mode = "I";
            //Object v_ip_address = CommonVariables.IPAddress;
            //Object P_SESSION_ID = DBNull.Value;
            string Zone_CD = "";
            string District = "";
            string VDC = "";
            string Ward = "";
            string Bank_CD = "";
            string Branch_CD = "";
            string NRA_Defined_CD = "";
            string House_Owner_ID = "";
            string Tranch = "";
            string Payroll_Install = "";
            Object totalNo = DBNull.Value;
            Object P_Out_Enrollment = DBNull.Value;
            string Approved_By = SessionCheck.getSessionUsername();
            //string Transaction_Date = DateTime.Now.ToShortDateString();
            DateTime Transact_DATE = DateTime.Now;
            string Transaction_Date = Transact_DATE.ToString("yyyy-MM-dd");
            DateTime Approved_DATE = DateTime.Now;
            string Approved_Date = Approved_DATE.ToString("yyyy-MM-dd");
            //string Approved_Date=DateTime.Now.ToShortDateString();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";// "PKG_NHRS_PAYMENT_APPROVAL";
                    service.Begin();

                    Zone_CD = dataresult.Rows[0]["ZONE_CD"].ToString();
                    District = dataresult.Rows[0]["DIST_CD"].ToString();
                    VDC = dataresult.Rows[0]["VDC_CD"].ToString();
                    Ward = dataresult.Rows[0]["WARD"].ToString();
                    Bank_CD = dataresult.Rows[0]["Bank_Cd"].ToString();
                    Branch_CD = dataresult.Rows[0]["Branch_Cd"].ToString();
                    Tranch = dataresult.Rows[0]["TRANCHE"].ToString();
                    Approved_By = SessionCheck.getSessionUsername().ToString();
                    string TransactionDate = Transaction_Date;
                    string ApprovedDate = Approved_Date;
                    Payroll_Install = dataresult.Rows[0]["AMOUNT"].ToString();
                    quryResult1 = service.SubmitChanges("PR_SEC_PAYMENT_APPROVE",
                                              "I",
                                               DBNull.Value,
                                               DBNull.Value,
                                               Zone_CD.ToDecimal(),
                                               District.ToDecimal(),
                                               VDC.ToDecimal(),
                                               Ward.ToDecimal(),
                                               Bank_CD.ToDecimal(),
                                               Branch_CD.ToDecimal(),
                                               filename,
                                               Approved_Date,
                                               Approved_By,
                                               Tranch,
                                               File_Destination,
                                               DBNull.Value,
                                               TransactionDate,
                                               Payroll_Install.ToDecimal(),
                                               DBNull.Value

                                                 );
                    string transaction_id = quryResult1["p_TRANSACTION_ID"].ToString();
                    for (int i = 0; i < dataresult.Rows.Count; i++)
                    {
                        NRA_Defined_CD = dataresult.Rows[i]["Agreement"].ConvertToString();
                        House_Owner_ID = dataresult.Rows[i]["HouseOwnerID"].ConvertToString();
                        string resPackageName = service.PackageName;
                        quryResult = service.SubmitChanges("PR_APPROVE_FIRST_INSPECTION",
                                                            NRA_Defined_CD,
                                                            House_Owner_ID,
                                                            Approved_By

                                                         );
                    }


                }
                catch (OracleException oex)
                {

                    error = oex.Code.ConvertToString();
                    ExceptionManager.AppendLog(oex);
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

        public QueryResult ApproveSecInspecctedPayment(DataTable dataresult, string filename, string File_Destination)
        {
            QueryResult quryResult = new QueryResult();
            QueryResult quryResult1 = new QueryResult();
            string error = string.Empty;
            //CommonFunction commFunction = new CommonFunction();
            //Object v_mode = "I";
            //Object v_ip_address = CommonVariables.IPAddress;
            //Object P_SESSION_ID = DBNull.Value;
            string Zone_CD = "";
            string District = "";
            string VDC = "";
            string Ward = "";
            string Bank_CD = "";
            string Branch_CD = "";
            string NRA_Defined_CD = "";
            string House_Owner_ID = "";
            string Tranch = "";
            string Payroll_Install = "";
            Object totalNo = DBNull.Value;
            Object P_Out_Enrollment = DBNull.Value;
            string Approved_By = SessionCheck.getSessionUsername();
            //string Transaction_Date = DateTime.Now.ToShortDateString();
            DateTime Transact_DATE = DateTime.Now;
            string Transaction_Date = Transact_DATE.ToString("yyyy-MM-dd");
            DateTime Approved_DATE = DateTime.Now;
            string Approved_Date = Approved_DATE.ToString("yyyy-MM-dd");
            //string Approved_Date=DateTime.Now.ToShortDateString();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";// "PKG_NHRS_PAYMENT_APPROVAL";
                    service.Begin();

                    Zone_CD = dataresult.Rows[0]["ZONE_CD"].ToString();
                    District = dataresult.Rows[0]["DIST_CD"].ToString();
                    VDC = dataresult.Rows[0]["VDC_CD"].ToString();
                    Ward = dataresult.Rows[0]["WARD"].ToString();
                    Bank_CD = dataresult.Rows[0]["Bank_Cd"].ToString();
                    Branch_CD = dataresult.Rows[0]["Branch_Cd"].ToString();
                    Tranch = dataresult.Rows[0]["TRANCHE"].ToString();
                    Approved_By = SessionCheck.getSessionUsername().ToString();
                    string TransactionDate = Transaction_Date;
                    string ApprovedDate = Approved_Date;
                    Payroll_Install = dataresult.Rows[0]["AMOUNT"].ToString();
                    quryResult1 = service.SubmitChanges("PR_SEC_PAYMENT_APPROVE",
                                              "I",
                                               DBNull.Value,
                                               DBNull.Value,
                                               Zone_CD.ToDecimal(),
                                               District.ToDecimal(),
                                               VDC.ToDecimal(),
                                               Ward.ToDecimal(),
                                               Bank_CD.ToDecimal(),
                                               Branch_CD.ToDecimal(),
                                               filename,
                                               Approved_Date,
                                               Approved_By,
                                               Tranch,
                                               File_Destination,
                                               DBNull.Value,
                                               TransactionDate,
                                               Payroll_Install.ToDecimal(),
                                               DBNull.Value

                                                 );
                    string transaction_id = quryResult1["p_TRANSACTION_ID"].ToString();
                    for (int i = 0; i < dataresult.Rows.Count; i++)
                    {
                        NRA_Defined_CD = dataresult.Rows[i]["Agreement"].ConvertToString();
                        House_Owner_ID = dataresult.Rows[i]["HouseOwnerID"].ConvertToString();
                        string resPackageName = service.PackageName;
                        quryResult = service.SubmitChanges("PR_APPROVE_SECOND_INSPECTION",
                                                            NRA_Defined_CD,
                                                            House_Owner_ID,
                                                            Approved_By

                                                         );
                    }


                }
                catch (OracleException oex)
                {

                    error = oex.Code.ConvertToString();
                    ExceptionManager.AppendLog(oex);
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

        public DataTable getapprovePaymentListTEST(NameValueCollection paramValues)
        {
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            //Object P_Fiscal_Yr = DBNull.Value;
            Object P_bank = DBNull.Value;
            Object P_Bank_Branch = DBNull.Value;
            DataTable dt = new DataTable();
            if (paramValues["District"].ConvertToString() != string.Empty && paramValues["District"].ConvertToString() != "undefined")
                P_Dist = paramValues["District"].ConvertToString();
            if (paramValues["VDC"].ConvertToString() != string.Empty && paramValues["VDC"].ConvertToString() != "undefined")
                P_VDC = paramValues["VDC"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty && paramValues["Ward"].ConvertToString() != "undefined")
                P_WARD = paramValues["Ward"].ConvertToString();
            //if (paramValues["Fiscalyr"].ConvertToString() != string.Empty && paramValues["Fiscalyr"].ConvertToString() != "undefined")
            //    P_Fiscal_Yr = paramValues["Fiscalyr"].ConvertToString();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "PR_PAYMENT_FIRST_ENROLL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                        // P_Fiscal_Yr,

                   DBNull.Value
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
            return dt;
        }
        #region Save Download Log
        //save download Log
        //public Boolean SaveDownloadLog(string filename, string transactionid, string zonecd, string districtcd, string vdcmunCd, string wardno, string installationcd)
        //{
        //    QueryResult quryResult = new QueryResult();
        //    string error = string.Empty;

        //    Object P_Out_Enrollment = DBNull.Value;
        //    string Downloaded_By = SessionCheck.getSessionUsername();
        //    //string Transaction_Date = DateTime.Now.ToShortDateString();
        //    //DateTime Transact_DATE = DateTime.Now;
        //    //string Transaction_Date = Transact_DATE.ToString("yyyy-MM-dd");
        //    var Download_Date = DateTime.Now;

        //    bool res = false;

        //    string exc = string.Empty;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        try
        //        {
        //            service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
        //            service.Begin();

        //            quryResult = service.SubmitChanges("PR_SAVE_DOWNLOAD_LOG",
        //                                     "I",
        //                                      DBNull.Value,
        //                                      transactionid,
        //                                      zonecd,
        //                                      districtcd,
        //                                      vdcmunCd,
        //                                      wardno,
        //                                      filename,
        //                                      Download_Date,
        //                                      Downloaded_By,
        //                                      installationcd

        //                                        );
        //        }
        //        catch (OracleException oex)
        //        {

        //            error = oex.Code.ConvertToString();
        //            ExceptionManager.AppendLog(oex);
        //        }

        //        catch (Exception ex)
        //        {
        //            ExceptionManager.AppendLog(ex);

        //        }


        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }
        //        if (res)
        //        {
        //            res = false;
        //        }
        //        else
        //        {
        //            if (quryResult != null)
        //            {
        //                res = quryResult.IsSuccess;
        //            }
        //        }

        //        return res;

        //    }


        //}
        #endregion

        public DataTable GetBankBatchStatus(string fileName, string status)
        {

            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_ENROLLMENT_FILE";
                    dt = service.GetDataTable(true, "PR_GET_ENROLL_BANK_FILE_BATCH",
                                                     fileName,
                                                     status,
                                                     DBNull.Value
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
        public DataTable GetBankClaimList(BankPayrollDetail objBankPayment)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;

            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_session_id = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_Fiscal_yr = DBNull.Value;
            Object p_Quarter = DBNull.Value;
            Object P_lang = DBNull.Value;
            Object P_PAYROLL_CD = DBNull.Value;
            Object P_APPROVED = objBankPayment.Approve;

            if (!string.IsNullOrEmpty(objBankPayment.Lang))
            {
                P_lang = objBankPayment.Lang;
            }

            if (!string.IsNullOrEmpty(objBankPayment.DISTRICT_CD))
            {
                P_district_cd = Decimal.Parse(objBankPayment.DISTRICT_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.VDC_MUN_CD))
            {
                P_vdc_mun_cd = Decimal.Parse(objBankPayment.VDC_MUN_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.WARD_NO))
            {
                p_ward_no = Decimal.Parse(objBankPayment.WARD_NO);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_CD))
            {
                p_Bank_cd = Decimal.Parse(objBankPayment.BANK_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_BRANCH_CD))
            {
                p_bank_branch_cd = (objBankPayment.BANK_BRANCH_CD).ToString();
            }
            if (!string.IsNullOrEmpty(objBankPayment.FISCAL_YR))
            {
                p_Fiscal_yr = (objBankPayment.FISCAL_YR);
            }
            if (!string.IsNullOrEmpty(objBankPayment.PAYROLL_INSTALL_CD) && objBankPayment.PAYROLL_INSTALL_CD != "0")
            {
                P_PAYROLL_CD = (objBankPayment.PAYROLL_INSTALL_CD);
            }

            if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "0")
            {
                p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_NHRS_PAYMENT_DTL_CLAIM_LIST",
                                                    P_lang,
                                                     P_APPROVED,
                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     p_bank_branch_cd,
                                                     p_Bank_cd,
                                                     p_Fiscal_yr,
                                                     p_Quarter,
                                                     P_PAYROLL_CD,
                                                     P_Out_Enrollment
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

        public DataTable GetFirstTrancheApprovedList(BankPayrollDetail objBankPayment) //Gets first tranche data
        {
            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;
            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_session_id = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_Fiscal_yr = DBNull.Value;
            Object p_Quarter = DBNull.Value;
            Object P_lang = DBNull.Value;
            Object p_pa_num = DBNull.Value;

            //checks model properties data before setting to variable
            if (!string.IsNullOrEmpty(objBankPayment.Lang))
            {
                P_lang = objBankPayment.Lang;
            }
            if (!string.IsNullOrEmpty(objBankPayment.NRA_DEFINED_CD))
            {
                p_pa_num = objBankPayment.NRA_DEFINED_CD;
            }
            if (!string.IsNullOrEmpty(objBankPayment.DISTRICT_CD))
            {
                P_district_cd = Decimal.Parse(objBankPayment.DISTRICT_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.VDC_MUN_CD))
            {
                P_vdc_mun_cd = Decimal.Parse(objBankPayment.VDC_MUN_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.WARD_NO))
            {
                p_ward_no = Decimal.Parse(objBankPayment.WARD_NO);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_CD))
            {
                p_Bank_cd = Decimal.Parse(objBankPayment.BANK_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_BRANCH_CD))
            {
                p_bank_branch_cd = (objBankPayment.BANK_BRANCH_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.FISCAL_YR))
            {
                p_Fiscal_yr = (objBankPayment.FISCAL_YR);
            }
            if (P_lang.ToString() == "E")
            {
                if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---Select Quarter---")
                {
                    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---चौथाई छान्नुहोस्---")
                {
                    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
                }
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(true, "PR_NHRS_FIRST_TRANCHE", //calling stored procedure
                                                     P_lang,
                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     p_bank_branch_cd,
                                                     p_Bank_cd,
                                                     p_Fiscal_yr,
                                                     p_Quarter,
                                                     p_pa_num,
                                                     P_Out_Enrollment
                                                     );


                }

                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe); //saves exception message to log
                }
                catch (Exception ex)
                {
                    ExceptionHandler.ExceptionManager.AppendLog(ex);//saves exception message to log
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

        public DataTable GetSecondTrancheApprovedList(BankPayrollDetail objBankPayment)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;
            Object p_Is_Uploaded = DBNull.Value;
            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_session_id = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_Fiscal_yr = DBNull.Value;
            Object p_Quarter = DBNull.Value;
            Object P_lang = DBNull.Value;
            Object p_bank_payroll_id = DBNull.Value;
            Object P_NRA_DEFINED_CD = DBNull.Value;

            if (!string.IsNullOrEmpty(objBankPayment.Lang))
            {
                P_lang = objBankPayment.Lang;
            }
            if (objBankPayment.BANK_NEW_PAYROLL_ID > 0)
            {
                p_bank_payroll_id = objBankPayment.BANK_NEW_PAYROLL_ID;
            }
            if (!string.IsNullOrEmpty(objBankPayment.IsUploaded))
            {
                p_Is_Uploaded = objBankPayment.IsUploaded;
            }
            if (!string.IsNullOrEmpty(objBankPayment.NRA_DEFINED_CD))
            {
                P_NRA_DEFINED_CD = objBankPayment.NRA_DEFINED_CD;
            }
            if (!string.IsNullOrEmpty(objBankPayment.DISTRICT_CD))
            {
                P_district_cd = Decimal.Parse(objBankPayment.DISTRICT_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.VDC_MUN_CD))
            {
                P_vdc_mun_cd = Decimal.Parse(objBankPayment.VDC_MUN_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.WARD_NO))
            {
                p_ward_no = Decimal.Parse(objBankPayment.WARD_NO);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_CD))
            {
                p_Bank_cd = Decimal.Parse(objBankPayment.BANK_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_BRANCH_CD))
            {
                p_bank_branch_cd = (objBankPayment.BANK_BRANCH_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.FISCAL_YR))
            {
                p_Fiscal_yr = (objBankPayment.FISCAL_YR);
            }
            if (P_lang.ToString() == "E")
            {
                if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---Select Quarter---")
                {
                    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---चौथाई छान्नुहोस्---")
                {
                    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
                }
            }


            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_NHRS_Second_Tranche",
                                                    P_lang,
                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     p_bank_branch_cd,
                                                     p_Bank_cd,
                                                     p_Fiscal_yr,
                                                     p_Quarter,
                                                     p_bank_payroll_id,
                                                     p_Is_Uploaded,
                                                     P_NRA_DEFINED_CD,
                                                     P_Out_Enrollment
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

        public DataTable GetThirdTrancheApprovedList(BankPayrollDetail objBankPayment)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;
            Object p_Is_Uploaded = DBNull.Value;
            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_session_id = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_Fiscal_yr = DBNull.Value;
            Object p_Quarter = DBNull.Value;
            Object P_lang = DBNull.Value;
            Object p_bank_payroll_id = DBNull.Value;
            Object p_NRA_DEFINED_CD = DBNull.Value;

            if (!string.IsNullOrEmpty(objBankPayment.Lang))
            {
                P_lang = objBankPayment.Lang;
            }

            if (!string.IsNullOrEmpty(objBankPayment.DISTRICT_CD))
            {
                P_district_cd = Decimal.Parse(objBankPayment.DISTRICT_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.VDC_MUN_CD))
            {
                P_vdc_mun_cd = Decimal.Parse(objBankPayment.VDC_MUN_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.WARD_NO))
            {
                p_ward_no = Decimal.Parse(objBankPayment.WARD_NO);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_CD))
            {
                p_Bank_cd = Decimal.Parse(objBankPayment.BANK_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_BRANCH_CD))
            {
                p_bank_branch_cd = (objBankPayment.BANK_BRANCH_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.FISCAL_YR))
            {
                p_Fiscal_yr = (objBankPayment.FISCAL_YR);
            }
            if (objBankPayment.BANK_NEW_PAYROLL_ID > 0)
            {
                p_bank_payroll_id = objBankPayment.BANK_NEW_PAYROLL_ID;
            }
            if (!string.IsNullOrEmpty(objBankPayment.IsUploaded))
            {
                p_Is_Uploaded = objBankPayment.IsUploaded;
            }
            if (!string.IsNullOrEmpty(objBankPayment.NRA_DEFINED_CD))
            {
                p_NRA_DEFINED_CD = objBankPayment.NRA_DEFINED_CD;
            }
            if (P_lang.ToString() == "E")
            {
                if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---Select Quarter---")
                {
                    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---चौथाई छान्नुहोस्---")
                {
                    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
                }
            }


            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_NHRS_THIRD_TRANCHE",
                                                    P_lang,
                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     p_bank_branch_cd,
                                                     p_Bank_cd,
                                                     p_Fiscal_yr,
                                                     p_Quarter,
                                                     p_NRA_DEFINED_CD,
                                                     p_Is_Uploaded,
                                                     p_bank_payroll_id,
                                                     P_Out_Enrollment
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
        public DataTable GetDuplicateBankClaimList(BankPayrollDetail objBankPayment)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;

            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_session_id = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_Fiscal_yr = DBNull.Value;
            Object p_Quarter = DBNull.Value;

            Object P_lang = DBNull.Value;
            if (!string.IsNullOrEmpty(objBankPayment.Lang))
            {
                P_lang = objBankPayment.Lang;
            }


            if (!string.IsNullOrEmpty(objBankPayment.BANK_CD))
            {
                p_Bank_cd = Decimal.Parse(objBankPayment.BANK_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.BANK_BRANCH_CD))
            {
                p_bank_branch_cd = (objBankPayment.BANK_BRANCH_CD);
            }
            if (!string.IsNullOrEmpty(objBankPayment.FISCAL_YR))
            {
                p_Fiscal_yr = (objBankPayment.FISCAL_YR);
            }
            //if (!string.IsNullOrEmpty(objBankPayment.QUARTER) && objBankPayment.QUARTER != "---Select Quarter---")
            //{
            //    p_Quarter = Decimal.Parse(objBankPayment.QUARTER);
            //}
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_NHRS_DUPLICATE_CLAIM_LIST",
                                                    P_lang,


                                                     p_bank_branch_cd,
                                                     p_Bank_cd,
                                                     p_Fiscal_yr,
                        //p_Quarter,
                                                     P_Out_Enrollment
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
        public QueryResult ApproveBankclaimList(DataTable claimdatalist, string Approved_batch)
        {
            QueryResult quryResult = new QueryResult();
            string P_NRA_DEFINED_CD = "";
            string House_Owner_ID = "";
            string installment_cd = "";
            string cmd = "";
            DataTable dtt = new DataTable();

            string approved_by = "";
            // cmd = " SELECT NVL(MAX(BATCH_ID),0)+1 INTO p_BATCH_ID FROM NHRS_REVERSE_FEED_FILE_BATCH;";

            //dtt = service.GetDataTable(cmd, null);

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    approved_by = SessionCheck.getSessionUsername();

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    for (int i = 0; i < claimdatalist.Rows.Count; i++)
                    {
                        P_NRA_DEFINED_CD = claimdatalist.Rows[i]["NRA_DEFINED_CD"].ConvertToString();
                        House_Owner_ID = claimdatalist.Rows[i]["HOUSE_OWNER_ID"].ConvertToString();
                        installment_cd = claimdatalist.Rows[i]["PAYROLL_INSTALL_CD"].ConvertToString();
                        quryResult = service.SubmitChanges("PR_APPROVE_BANK_CLAIM_LIST",
                                                           P_NRA_DEFINED_CD,
                                                           House_Owner_ID,
                                                           approved_by,
                                                           installment_cd,
                                                            Approved_batch

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
        public JsonResult FillBranchBybank(string id)
        {
            CommonService comser = new CommonService();

            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            List<MISCommon> lstCommon = comser.GetRuralUrbanCodeandDescForDistCode("", "", id);
            return new JsonResult { Data = lstCommon, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public DataTable getbatch(string bank_cd, string branch_std_cd)
        {
            DataTable dtt = null;
            string cmd = "";
            ServiceFactory service = new ServiceFactory();
            service.Begin();


            cmd = " SELECT NVL(MAX(APPROVED_BATCH),0)+1  FROM NHRS_BANK_PAYROLL_DTL where BANK_CD=" + bank_cd + "  and BRANCH_STD_CD=" + branch_std_cd + "";
            //cmd = "SELECT TO_CHAR(TO_DATE('" + date + "', 'mm/dd/yyyy'), 'Q') quarter FROM dual";

            try
            {
                dtt = service.GetDataTable(cmd, null);
            }
            catch (Exception ex)
            {
                dtt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dtt;

        }

        public void UpdateEnrollmentDownloadFlag(string NraDefinedCode, string btcid)
        {
            EnrollmentMou mouenroll = new EnrollmentMou();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    string cmdText = String.Format("UPDATE NHRS.NHRS_ENROLLMENT_MOU SET HAS_BANK_GENERATED='Y',BANK_BATCH_ID=" + btcid + " where NRA_DEFINED_CD='" + NraDefinedCode + "'");
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(cmdText, null);
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
        }

        public Boolean UpdateBankClaimDetail(BankClaim objBankClaim)
        {
            BankClaim objBankClaimdtl = new BankClaim();
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    int TRANSACTON_ID = objBankClaim.TRANSACTON_ID;
                    string NRA_Defined_Cd = objBankClaim.PA_NO;
                    //string Beneficiary_Name    =   objBankClaim.Beneficiary_Name  ;
                    int Dis_Cd = objBankClaim.Dis_Cd;
                    int Vdc_Mun_Cd = objBankClaim.Vdc_Mun_Cd;
                    int Ward_Num = objBankClaim.Ward_Num;
                    string Bank_Name = objBankClaim.Bank_Name;
                    string Reciepient_Name = objBankClaim.Reciepient_Name;
                    string Branch_Std_Cd = objBankClaim.Branch_Std_Cd;
                    string AccountNo = objBankClaim.AccountNo;
                    string Activation_Date = objBankClaim.Activation_Date;
                    string Tranche = objBankClaim.Tranche;
                    string Deposited_Date = objBankClaim.Deposited_Date;
                    string Deposited_Date_LOC = objBankClaim.Deposited_Date_LOC;
                    string Activation_Date_LOC = objBankClaim.Activation_Date_LOC;
                    string Remarks = objBankClaim.Remarks;
                    string IsCard_Issued = objBankClaim.IsCard_Issued;
                    decimal? Batch = objBankClaim.Batch;
                    string Branch = objBankClaim.Branch;
                    int Deposite = objBankClaim.Deposite;
                    string Card_Iss_Date = objBankClaim.Card_Iss_Date;
                    int Bank_cd = objBankClaim.Bank_cd;
                    int payroll_id = objBankClaim.Payroll_ID;
                    int bank_payroll_id = objBankClaim.Bank_Payroll_Id;



                    service.Begin();
                    qr = service.SubmitChanges("PR_UPDATE_BANK_CLAIM_DTL",
                                            NRA_Defined_Cd,
                        //Beneficiary_Name  ,  
                                            Dis_Cd,
                                            Vdc_Mun_Cd,
                                            Ward_Num,
                                            Bank_Name,
                                            Reciepient_Name,
                                            Branch_Std_Cd,
                                            AccountNo,
                                            Activation_Date,
                                            Tranche,
                                            Deposited_Date,
                                            Remarks,
                                            IsCard_Issued,
                                            Batch,
                                            Branch,
                                            Deposite,
                                            Card_Iss_Date,
                                            Bank_cd,
                                            TRANSACTON_ID,
                                            payroll_id,
                                            Deposited_Date_LOC,
                                            Activation_Date_LOC,
                                            bank_payroll_id
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

        public Boolean UploadBankClaimDetail(BankClaim objBankClaim)
        {
            BankClaim objBankClaimdtl = new BankClaim();
            bool res = false;

            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            int quarter = 0;
            string nepali_activate_Date = "";
            string fiscalyr = "";
            string ac_activate_date = "";
            string deposited_dt = "";
            string ac_activate_date_eng = "";
            QueryResult qr = null;


            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.PackageName = "NHRS.PKG_REVERSE_FEED";
                    service.Begin();

                    if (objBankClaim.Deposited_Date.ToString() != "")
                    {
                        deposited_dt = (objBankClaim.Deposited_Date).ConvertToString("MM/dd/yyyy");
                    }
                    else
                    {
                        deposited_dt = null;
                    }

                    ac_activate_date = NepaliDate.getEnglishDate((objBankClaim.Activation_Date_LOC).ConvertToString("MM/dd/yyyy"));

                    fiscalyr = CommonFunction.GetRecentFiscalYear(ac_activate_date);
                    nepali_activate_Date = NepaliDate.getNepaliDate(ac_activate_date);
                    //string fiscalyr=comm

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

                    string Pa_no = objBankClaim.PA_NO.ToString();
                    string[] splits = Pa_no.Split('-');
                    int cnt = splits.Count();
                    if (cnt == 5)
                    {
                        nra_district = splits[splits.Length - 5].ToString();
                        nra_vdc = splits[splits.Length - 4].ToString();

                        nra_ward = splits[splits.Length - 3].ToString();
                    }

                    qr = service.SubmitChanges("PR_Inspection",
                                    "I",
                                   DBNull.Value,//PAYROLL_ID
                                   objBankClaim.PA_NO.ConvertToString(),// //NRA_DEFINED_CD
                                   objBankClaim.Batch.ToDecimal(),//payment_Batch
                                   DBNull.Value,//Payroll_gen_date_loc
                                   objBankClaim.Reciepient_Name.ConvertToString(), //Recipent_Name
                                   nra_district.ToDecimal(),//district
                                   nra_vdc.ToDecimal(),//vdc
                                   nra_ward.ToDecimal(),//ward_no
                                   objBankClaim.IsCard_Issued.ConvertToString().ToUpper(),//is_card_issued
                                   objBankClaim.Card_Iss_Date.ConvertToString(),//card_issued_date                       
                                   objBankClaim.Bank_cd.ToDecimal(),//Bank cd
                                   objBankClaim.Bank_Name.ConvertToString(),//bank_name
                                   objBankClaim.Branch.ConvertToString(),//bank_branch
                                   DBNull.Value, //a/c Type
                                   objBankClaim.AccountNo.ConvertToString(),//a/c number
                                   objBankClaim.Tranche.ConvertToString(),//tranch
                                   objBankClaim.Deposite.ConvertToString(),//amount
                                   SessionCheck.getSessionUsername().ConvertToString(),//enteredBy
                                   DateTime.Now.ToShortDateString(),//entered_date
                                   SessionCheck.getSessionUsername().ConvertToString(),//updated_by
                                   DateTime.Now.ToShortDateString(),//updated_by
                                   DBNull.Value,//file_batch_id
                                   objBankClaim.Remarks.ConvertToString(),//Remarks
                                   objBankClaim.Gender_cd.ConvertToString().ToUpper(),//Gender
                                   fiscalyr
                           );
                    string PayrollID = qr["v_PAYROLL_ID"].ConvertToString();

                    qr = service.SubmitChanges("PR_BANK_PAYROLL_DTL",
                       'I',
                       DBNull.Value,//transaction_id
                        //paramTable.Rows[i][16].ConvertToString("MM/dd/yyyy"), 
                        //paramTable.Rows[i][16].ToDateTime(),//daeposited date Transaction_dt
                       deposited_dt,
                       objBankClaim.Deposited_Date_LOC,// nepali_transac_date.ToString(),//transaction_dt
                       objBankClaim.Bank_cd.ToDecimal(),//bank_cd
                       objBankClaim.Bank_Name.ConvertToString(),//bank_name
                        objBankClaim.Branch.ConvertToString(),//bank_branch
                       DBNull.Value,//a/c type
                       objBankClaim.AccountNo.ConvertToString(),//a/c number
                        objBankClaim.PA_NO.ConvertToString(),// //NRA_DEFINED_CD                       
                        //paramTable.Rows[i][13].ConvertToString("MM/dd/yyyy"), //a/c activate date
                       ac_activate_date,
                        //paramTable.Rows[i][13].ToDateTime(),
                         nepali_activate_Date.ToString(),//a/c activate date loc
                       objBankClaim.Deposite.ConvertToString(), //cr_amount                        
                        //paramTable.Rows[i][16].ConvertToString("MM/dd/yyyy"),//deposited date 
                        //paramTable.Rows[i][16].ToDateTime(),
                       objBankClaim.Deposited_Date,
                       objBankClaim.Deposited_Date_LOC,// nepali_deposited_date.ToString(),//deposited date loc
                       DBNull.Value,//  withdraw  date loc
                       DBNull.Value,// withdraw  amount
                       DBNull.Value,//withdraw_dt
                       objBankClaim.Tranche.ConvertToString(),//tranch
                       SessionCheck.getSessionUsername(),  //ENTERED_BY
                       DateTime.Now.ToShortDateString(),  //ENTER_DT
                       SessionCheck.getSessionUsername(),   //LAST_UPDATED_BY
                       DateTime.Now.ToShortDateString(),    //LAST_UPDATED_DT                         
                       objBankClaim.Remarks.ConvertToString(), //REMARKS                     
                       DBNull.Value,   //FEED_BATCH_FILE
                       DBNull.Value,  //   reproting_dt                           
                       DBNull.Value,  //reporting_dt_loc
                       DBNull.Value,   //Batch_Lot
                       PayrollID.ToDecimal(),
                       nra_district.ToDecimal(),//district
                       nra_vdc.ToDecimal(),//vdc
                       nra_ward.ToDecimal(),//ward_no
                       objBankClaim.IsCard_Issued.ConvertToString().ToUpper(),//is_card_issued
                       objBankClaim.Card_Iss_Date.ConvertToString(),//card_issued_date 
                       quarter.ToDecimal(),
                       objBankClaim.Branch_Std_Cd.ConvertToString(),  //branch_cd
                       fiscalyr,
                       objBankClaim.Reciepient_Name

                        );

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
            if (qr != null)
            {
                res = qr.IsSuccess;
            }
            return res;
        }
        public Boolean UpdateEnrollmentBankDetail(string NRADefinedCd, string bankName, string accNum, string accType, string remarks, string bankBatchId)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges("PR_UPDATE_ENROLLED_DATA", NRADefinedCd, bankName, accNum, accType, remarks, bankBatchId);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    //service.RollBack();
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

        public string AddEnrollmentBankBatch(string filename, string distCode, string vdcCode, string errMessage)
        {
            string res = "";
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_ENROLLMENT_FILE";

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges("PR_NHRS_ENROLL_BANK_FILE_BATCH", "I", filename, "N", distCode, vdcCode, errMessage, DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    //service.RollBack();
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
                res = qr["p_OUT_BATCH_ID"].ToString();
            }
            return res;
        }

        public Boolean UpdateEnrollmentBankBatch(string filename, string errMessage, string status)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_ENROLLMENT_FILE";

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges("PR_NHRS_ENROLL_BANK_FILE_BATCH", "U", filename, status, "0", "0", errMessage, DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    //service.RollBack();
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

        public void RollBackEnrollmentBankBatch(string btcid)
        {
            EnrollmentMou mouenroll = new EnrollmentMou();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    string cmdText = String.Format("UPDATE NHRS.NHRS_ENROLLMENT_MOU SET BANK_NAME='',BANK_ACC_NO='',BANK_ACCOUNT_TYPE='',REMARKS='' where BANK_BATCH_ID='" + btcid + "'");
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(cmdText, null);
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
        }

        public QueryResult ApproveIndividualBankClaimList(BankPayrollDetail payrollDetail)
        {
            QueryResult quryResult = new QueryResult();

            DataTable dtt = new DataTable();
            Object P_BANK_PAYROLL_ID = DBNull.Value;
            Object approved_by = DBNull.Value;
            Object status = DBNull.Value;
            Object payroll_install_cd = DBNull.Value;
            Object account_no = DBNull.Value;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    approved_by = payrollDetail.APPROVED_BY;

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";

                    P_BANK_PAYROLL_ID = payrollDetail.bank_payroll_id;
                    payroll_install_cd = payrollDetail.PAYROLL_INSTALL_CD;
                    status = payrollDetail.Status;
                    approved_by = payrollDetail.APPROVED_BY;
                    account_no = payrollDetail.ACCOUNT_NUMBER;

                    quryResult = service.SubmitChanges("PR_APPROVE_IND_BANK_CLAIM_LIST",
                                                           P_BANK_PAYROLL_ID,
                                                           account_no,
                                                           approved_by,
                                                           status

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


        public QueryResult ApproveIndividualPO(PaymentPartnerOrganization objPO)
        {
            QueryResult quryResult = new QueryResult();

            DataTable dtt = new DataTable();
            Object P_SUPPORT_CD = DBNull.Value;
            Object approved_by = DBNull.Value;
            Object status = DBNull.Value;
            Object approved_dt = DBNull.Value;
            Object approved_dt_loc = DBNull.Value;
            string cmd = "";
          
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    approved_by = objPO.APPROVED_BY;

                    service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";

                    P_SUPPORT_CD = objPO.Support_CD;
                    status = objPO.Status;
                    approved_by = objPO.APPROVED_BY;
                    approved_dt = objPO.APPROVED_DT;
                    approved_dt_loc = objPO.APPROVED_DT_LOC;

                    cmd = "Update NHRS_DONOR_SUPPORT_DTL set STATUS = '" + status + "'  where SUPPORT_CD = '" + P_SUPPORT_CD + "' ";
                    quryResult = service.SubmitChanges(cmd, null);
                 

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


        public DataTable GetPaymentErrorList(string bank_cd, string branch_cd, int batchId, string tranche)
        {
            DataTable dt = new DataTable();
            Object P_OUT_RESULT = DBNull.Value;
            Object P_BANK_CD = DBNull.Value;
            Object P_BRANCH_CD = DBNull.Value;
            Object P_TRANCHE = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;

            List<string> ErrorFileBatchId = new List<string>();
            List<string> ListErrorFiles = new List<string>();
            QueryResult qr = new QueryResult();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();

            if(bank_cd != "")
            {
                P_BANK_CD = bank_cd;
            }
        
            if (tranche != "")
            {
                P_TRANCHE = tranche;
            }

            if (branch_cd != "")
            {
                P_BRANCH_CD = branch_cd;
            }


            if (batchId > 0 && batchId != null)
            {
                P_BATCH_ID = batchId;
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                        service.Begin();
                        service.SubmitChanges("PR_PAYMENT_ERROR",
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

        //public DataTable GetPaymentErrorList(List<string> errorFileBatchId,string bank_cd)
        //{
        //    DataTable dt = new DataTable();
            
        //    Object P_FILE_BATCH_ID = DBNull.Value;
        //    Object P_BANK_CD = bank_cd;
        //    Object P_OUT_RESULT = DBNull.Value;

        //    string cmd = "";

        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        try
        //        {
        //            service.Begin();
        //            service.PackageName = "PKG_REVERSE_FEED";

        //                foreach (var item in errorFileBatchId)
        //                {
        //                    string cmd2 = "select BANK_CD,FILE_BATCH_ID from NHRS_PAYMENT_ERROR" +
        //                                  " where BANK_CD = " + bank_cd + " AND FILE_BATCH_ID =" + item;
        //                    DataTable dt2 = service.GetDataTable(cmd2, null);

        //                    if(dt2.Rows.Count == 0)
        //                    { 
        //                        P_FILE_BATCH_ID = item;
        //                        string cmd1 = "select NVL(MAX(ERROR_CD), 0)+1 from NHRS_PAYMENT_ERROR";
        //                        DataTable dt1 = service.GetDataTable(cmd1, null);
        //                        int error_cd = dt1.Rows[0][0].ToInt32();
        //                        cmd = "INSERT INTO NHRS_PAYMENT_ERROR(ERROR_CD,FILE_BATCH_ID,BANK_CD)" +
        //                        "VALUES (" + error_cd + "," + P_FILE_BATCH_ID + "," + P_BANK_CD + ")";

        //                        QueryResult qr = service.SubmitChanges(cmd, null);
        //                    }
        //                }
                    
        //            dt = service.GetDataTable(true, "PR_NHRS_GET_ERROR_LIST",
        //                                            P_BANK_CD,
        //                                            P_OUT_RESULT);
                    
        //        }
        //        catch (OracleException oe)
        //        {
        //            service.RollBack();
        //            ExceptionManager.AppendLog(oe);
        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionHandler.ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }
        //    }

        //    return dt;
        //}

        public QueryResult InsertSinglePODtl(PaymentPartnerOrganization objPayment)
        {
            QueryResult qResult = null;
            CommonFunction fc = new CommonFunction();

            Object P_MODE                =      DBNull .Value;
            Object P_DONOR_CD            =      DBNull.Value;
            Object PA_NO                 =      DBNull.Value;
            Object Reciepient_Name       =      DBNull.Value;
            Object P_BENEF_NAME          =       DBNull.Value;
            Object P_SUPPORT_AMT         =      DBNull.Value;
            Object HouseHold_Id          =      DBNull.Value;
            Object Nissa_No              =      DBNull.Value;
            Object payroll_install_cd    =      DBNull.Value;
            Object Bank_cd               =      DBNull.Value;
            Object Branch_Std_Cd         =      DBNull.Value;
            Object Dis_Cd                =      DBNull.Value;
            Object Vdc_Mun_Cd            =      DBNull.Value;
            Object Ward_Num              =      DBNull.Value;
            Object Area                  =      DBNull.Value;
            Object P_BATCH_ID            =      DBNull.Value;
            Object P_STATUS              =      DBNull.Value;
            Object P_ENTERED_BY          =      DBNull.Value;
            Object P_ENTERED_DT          =      DBNull.Value;
            Object P_ENTERED_DT_LOC      =      DBNull.Value;
            Object P_UPDATED_BY          =      DBNull.Value;
            Object P_UPDATED_DT          =      DBNull.Value;
            Object P_UPDATED_DT_LOC      =      DBNull.Value;
            Object P_APPROVED_BY         =      DBNull.Value;
            Object P_APPROVED_DT         =      DBNull.Value;
            Object P_APPROVED_DT_LOC     =      DBNull.Value;
            Object P_Support_CD          =      DBNull.Value;
            Object P_REMARKS             =      DBNull.Value;

            if (!string.IsNullOrEmpty(objPayment.MODE))
                P_MODE                = objPayment.MODE;

            if (!string.IsNullOrEmpty(objPayment.Donor_CD))
                P_DONOR_CD            = objPayment.Donor_CD;

            if (!string.IsNullOrEmpty(objPayment.PA_NO))
                PA_NO                 = objPayment.PA_NO;

            if (!string.IsNullOrEmpty(objPayment.Reciepient_Name))
                Reciepient_Name       = objPayment.Reciepient_Name;

             if (!string.IsNullOrEmpty(objPayment.Beneficiary_Name))
                P_BENEF_NAME          = objPayment.Beneficiary_Name;

            if (!string.IsNullOrEmpty(objPayment.House_SN))
                HouseHold_Id = objPayment.House_SN;

            if (!string.IsNullOrEmpty(objPayment.Nissa_No))
                Nissa_No = objPayment.Nissa_No;

            if (!string.IsNullOrEmpty(objPayment.payroll_install_cd))
                payroll_install_cd    = objPayment.payroll_install_cd;


            if (!string.IsNullOrEmpty(objPayment.Support_Amount))
                P_SUPPORT_AMT = objPayment.Support_Amount;

            if (!string.IsNullOrEmpty(objPayment.Bank_cd))
                Bank_cd               = objPayment.Bank_cd;

            if (!string.IsNullOrEmpty(objPayment.Branch_Std_Cd))
                Branch_Std_Cd         = objPayment.Branch_Std_Cd;

            if (!string.IsNullOrEmpty(objPayment.Dis_Cd))
                Dis_Cd                = objPayment.Dis_Cd;

            if (!string.IsNullOrEmpty(objPayment.Vdc_Mun_Cd))
                Vdc_Mun_Cd            = objPayment.Vdc_Mun_Cd;

            if (!string.IsNullOrEmpty(objPayment.Ward_Num))
                Ward_Num              = objPayment.Ward_Num;

            if (!string.IsNullOrEmpty(objPayment.Area))
                Area                  = objPayment.Area;

            if (objPayment.BatchId != 0 && objPayment.BatchId != null)
                P_BATCH_ID            = objPayment.BatchId;

                P_STATUS = 'N';
            
                if (!string.IsNullOrEmpty(objPayment.ENTERED_BY))
                    P_ENTERED_BY          = objPayment.ENTERED_BY;

            if (!string.IsNullOrEmpty(objPayment.ENTERED_DT))
                P_ENTERED_DT          = objPayment.ENTERED_DT;

            if (!string.IsNullOrEmpty(objPayment.ENTERED_DT_LOC))
                P_ENTERED_DT_LOC      = objPayment.ENTERED_DT_LOC;

            if (!string.IsNullOrEmpty(objPayment.UPDATED_BY))
                P_UPDATED_BY          = objPayment.UPDATED_BY;

            if (!string.IsNullOrEmpty(objPayment.UPDATED_DT))
                P_UPDATED_DT          = objPayment.UPDATED_DT;

            if (!string.IsNullOrEmpty(objPayment.UPDATED_DT_LOC))
                P_UPDATED_DT_LOC      = objPayment.UPDATED_DT_LOC;

            if (!string.IsNullOrEmpty(objPayment.APPROVED_BY))
                P_APPROVED_BY         = objPayment.APPROVED_BY;

            if (!string.IsNullOrEmpty(objPayment.APPROVED_DT))
                P_APPROVED_DT         = objPayment.APPROVED_DT;

            if (!string.IsNullOrEmpty(objPayment.APPROVED_DT_LOC))
                P_APPROVED_DT_LOC     = objPayment.APPROVED_DT_LOC;

            if (!string.IsNullOrEmpty(objPayment.Remarks))
                P_REMARKS = objPayment.Remarks;

            if (objPayment.Support_CD != 0 && objPayment.Support_CD != null)
                P_Support_CD = objPayment.Support_CD;
           
            

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_DONOR_SUPPORT_DTL";
                    qResult = service.SubmitChanges("PR_DONOR_SUPPORT_DTL",
                                                        P_MODE,                                        
                                                        P_BATCH_ID,    
                                                        P_Support_CD,
                                                        Dis_Cd,                  
                                                        Vdc_Mun_Cd,              
                                                        Ward_Num,                
                                                        Area,                    
                                                        HouseHold_Id,            
                                                        Nissa_No,                
                                                        PA_NO,                   
                                                        P_DONOR_CD,              
                                                        payroll_install_cd,      
                                                        Bank_cd,
                                                        Branch_Std_Cd,       
                                                        P_STATUS,              
                                                        P_ENTERED_BY,          
                                                        P_ENTERED_DT,          
                                                        P_ENTERED_DT_LOC,      
                                                        P_UPDATED_BY,          
                                                        P_UPDATED_DT,          
                                                        P_UPDATED_DT_LOC,      
                                                        P_APPROVED_BY,         
                                                        P_APPROVED_DT,
                                                        P_APPROVED_DT_LOC,
                                                        P_BENEF_NAME,
                                                        Reciepient_Name,
                                                        P_SUPPORT_AMT,
                                                        P_REMARKS
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

            return qResult;
        }

        public PaymentPartnerOrganization GetPODtlbyPA(string support_cd)
        {
            DataTable dt = new DataTable();
            Object P_OUT_RESULT = DBNull.Value;
            Object P_SUPPORT_CD = DBNull.Value;
            string cmd = "";
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            if (support_cd != "" && support_cd != null)
            {
                P_SUPPORT_CD = support_cd;
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    cmd = "Select * from NHRS_DONOR_SUPPORT_DTL where SUPPORT_CD = " + support_cd;
                    dt = service.GetDataTable(new
                    {
                        query = cmd,
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

            if(dt.Rows.Count > 0)
            {
                objPO.Dis_Cd                 =   dt.Rows[0][1].ToString();
                objPO.Vdc_Mun_Cd             =   dt.Rows[0][2].ToString();
                objPO.Ward_Num = dt.Rows[0][3].ToString();
                objPO.Area                   =   dt.Rows[0][4].ToString();
                objPO.House_SN = dt.Rows[0][5].ToString();
                objPO.Nissa_No               =   dt.Rows[0][6].ToString();
                objPO.PA_NO                  =   dt.Rows[0][7].ToString();
                objPO.Donor_CD               =   dt.Rows[0][8].ToString();
                objPO.payroll_install_cd = dt.Rows[0][9].ToString();
                objPO.Bank_cd                =   dt.Rows[0][10].ToString();
                objPO.Branch_Std_Cd          =   dt.Rows[0][11].ToString();
                objPO.Reciepient_Name        =   dt.Rows[0][22].ToString();
                objPO.Beneficiary_Name = dt.Rows[0][24].ToString();
                objPO.Support_Amount = dt.Rows[0][25].ToString();
                objPO.Remarks = dt.Rows[0][26].ToString();
            }

            return objPO;
        }

        public DataTable ListPOData(PaymentPartnerOrganization objPO)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;
            Object P_Donor_cd = DBNull.Value;
            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_pa_num = DBNull.Value;
            Object P_MODE = DBNull.Value;
            Object p_installment_cd = DBNull.Value;
            Object p_donor_cd = DBNull.Value;

            string usr_cd = SessionCheck.getSessionUserCode();
           
            if (!string.IsNullOrEmpty(objPO.Dis_Cd))
            {
                P_district_cd = Decimal.Parse(objPO.Dis_Cd);
            }

            if (!string.IsNullOrEmpty(objPO.Vdc_Mun_Cd))
            {
                P_vdc_mun_cd = Decimal.Parse(objPO.Vdc_Mun_Cd);
            }

            if (!string.IsNullOrEmpty(objPO.Ward_Num))
            {
                p_ward_no = objPO.Ward_Num;
            }

            if (!string.IsNullOrEmpty(objPO.Bank_cd))
            {
                p_Bank_cd = objPO.Bank_cd;
            }

            if (!string.IsNullOrEmpty(objPO.Branch_Std_Cd) && objPO.Branch_Std_Cd.Trim() != "---Select Branch---")
            {
                p_bank_branch_cd = (objPO.Branch_Std_Cd);
            }

            if (!string.IsNullOrEmpty(objPO.PA_NO))
            {
                p_pa_num = objPO.PA_NO;
            }
            if (!string.IsNullOrEmpty(objPO.payroll_install_cd))
            {
                p_installment_cd = objPO.payroll_install_cd;
            }

            if (!string.IsNullOrEmpty(objPO.MODE))
            {
                P_MODE = objPO.MODE;
            }

            if (!string.IsNullOrEmpty(objPO.Donor_CD))
            {
                p_donor_cd = objPO.Donor_CD;
            }
        
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    dt = service.GetDataTable(true, "PR_LIST_DONOR_SUPPORT",
                                                     P_MODE,
                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     p_Bank_cd,
                                                     p_bank_branch_cd,
                                                     p_pa_num,
                                                     p_installment_cd,
                                                     p_donor_cd,
                                                     P_Out_Enrollment
                                                     );


                }

                catch (OracleException oe )
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
                    service.RollBack();
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

        public PaymentPartnerOrganization GetPObyId(string support_cd)
        {
            PaymentPartnerOrganization obj = new PaymentPartnerOrganization();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from NHRS_DONOR_SUPPORT_DTL where SUPPORT_CD = '" + support_cd + "' ";
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
                    
                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
            }
            if (dt.Rows.Count > 0)
            {
                obj.Support_CD = dt.Rows[0]["SUPPORT_CD"].ToInt32();
                obj.PA_NO = dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                obj.Bank_Name = dt.Rows[0]["BANK_CD"].ConvertToString();
                obj.Dis_Cd = dt.Rows[0]["DISTRICT_CD"].ToString();
                obj.Vdc_Mun_Cd = dt.Rows[0]["VDC_MUN_CD"].ToString();
                obj.Ward_Num = dt.Rows[0]["WARD_NUM"].ToString();
                obj.Reciepient_Name = dt.Rows[0]["RECIPIENT_NAME"].ConvertToString();
                obj.Branch_Std_Cd = dt.Rows[0]["BRANCH_STD_CD"].ToString();
                obj.Status = dt.Rows[0]["STATUS"].ToString();
            }


            return obj;
        }


        public QueryResult ApprovePOList(DataTable POList)
        {
            QueryResult qr = new QueryResult();
            DataTable dt = new DataTable();
           
            Object P_Support_Cd = DBNull.Value;
            Object P_APPROVED_BY = SessionCheck.getSessionUsername();
            Object P_APPROVED_DT = DateTime.Now.ToString();
            Object P_APPROVED_DT_LOC = NepaliDate.getNepaliDate(P_APPROVED_DT.ToString());
            string cmd = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    for (int i = 0; i < POList.Rows.Count; i++)
                    {
                        P_Support_Cd = POList.Rows[i]["SUPPORT_CD"].ToInt32();
                        cmd = "UPDATE NHRS_DONOR_SUPPORT_DTL SET STATUS = 'Y', APPROVED_BY = '" + P_APPROVED_BY + "' " +
                              ",APPROVED_DT = '" + P_APPROVED_DT + "',APPROVED_DT_LOC = '" + P_APPROVED_DT_LOC + "' WHERE " +
                              "SUPPORT_CD =" + P_Support_Cd;

                        qr = service.SubmitChanges(cmd, null);
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

            return qr;

        }

        public DataTable AddBranchUser(PaymentPartnerOrganization objPO)
        {

            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;

            Object P_district_cd = DBNull.Value;
            Object P_vdc_mun_cd = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object p_Bank_cd = DBNull.Value;
            Object p_bank_branch_cd = DBNull.Value;
            Object p_pa_num = DBNull.Value;



            if (!string.IsNullOrEmpty(objPO.Dis_Cd))
            {
                P_district_cd = Decimal.Parse(objPO.Dis_Cd);
            }

            if (!string.IsNullOrEmpty(objPO.Vdc_Mun_Cd))
            {
                P_vdc_mun_cd = Decimal.Parse(objPO.Vdc_Mun_Cd);
            }

            if (!string.IsNullOrEmpty(objPO.Ward_Num))
            {
                p_ward_no = objPO.Ward_Num;
            }

            if (!string.IsNullOrEmpty(objPO.Bank_cd))
            {
                p_Bank_cd = objPO.Bank_cd;
            }

            if (!string.IsNullOrEmpty(objPO.Branch_Std_Cd) && objPO.Branch_Std_Cd.Trim() != "---Select Branch---")
            {
                p_bank_branch_cd = (objPO.Branch_Std_Cd);
            }

            if (!string.IsNullOrEmpty(objPO.PA_NO))
            {
                p_pa_num = objPO.PA_NO;
            }



            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    dt = service.GetDataTable(true, "PR_LIST_DONOR_SUPPORT",
                                                     'E',
                                                     P_district_cd,
                                                     P_vdc_mun_cd,
                                                     p_ward_no,
                                                     p_Bank_cd,
                                                     p_bank_branch_cd,
                                                     p_pa_num,
                                                     P_Out_Enrollment
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

        public static string adminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"].ToString();
        public bool UserUID(Users objUsers, MIS.Models.Security.Group objGroup, string mode)
        {
            QueryResult qResult = null;
            ComWebUsrInfo obj = new ComWebUsrInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            string bnkCd = objUsers.bankCode;
            string bnkBranchStd = objUsers.branchStdCode;
            string bnkUsrCd = "";
            string designation = "";

            using (ServiceFactory service = new ServiceFactory())
            {

                service.PackageName = "PKG_COM_WEB_SECURITY";
                obj.UsrCd = objUsers.usrCd.ConvertToString().Trim();
                obj.UsrName = objUsers.usrName.ConvertToString().Trim();
                obj.Password = objUsers.password.ConvertToString().Trim();
                obj.EmpCd = objUsers.empCd.ConvertToString().Trim();
                obj.Status = objUsers.status.ConvertToString().Trim();
                //obj.ExpiryDt = objUsers.expDay + "-" + objUsers.expMonth + "-" + objUsers.expYear;
                obj.ExpiryDt = objUsers.expiryDt.ToString("dd-MM-yyyy");
                //obj.ExpiryDtLoc = objUsers.expiryDtLoc.ToString();
                obj.ExpiryDtLoc = objUsers.expiryDtLoc.ConvertToString();
                obj.EnteredBy = strUserName.Trim();//objUsers.enteredBy.ConvertToString().Trim();
                obj.LastUpdatedBy = objUsers.enteredBy.ConvertToString().Trim();
                obj.Approved = objUsers.approved;
                obj.ApprovedBy = objUsers.enteredBy.ConvertToString().Trim();
                obj.ApprovedDt = DateTime.Now.ToString("dd-MMM-yyyy");
                obj.Email = objUsers.email.ConvertToString().Trim();
                obj.Mobile_No = objUsers.mobilenumber.ConvertToString().Trim();
                obj.Verification_Required = objUsers.VerificationRequired.ConvertToString().Trim();
                

                if (obj.Verification_Required.ConvertToString() == "")
                {
                    obj.Verification_Required = "N";
                }
                obj.IPAddress = CommonVariables.IPAddress;
                obj.Mode = mode;
                
                obj.Is_Bank_User = "Y";
                obj.Bank_CD = objUsers.bankCode.ConvertToString();
               // obj.DONOR_CD = objUsers.donor_cd.ConvertToString();

                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(obj, true);
                    if (qResult.IsSuccess == true && obj.Mode == "I")
                    {
                        string toName = "";
                        toName = obj.UsrName;
                        EmailMessage emMessage = new EmailMessage();

                        emMessage.From = "";
                        emMessage.To = obj.Email;
                        emMessage.Subject = "User Created";
                        emMessage.Body = "Dear " + toName + ",<br> Your user is created as username: " + obj.Email + " password: " + Utils.DecryptString(obj.Password.Trim()) + ". Your account is still not verified please contact administrator for account verification.<br><br> Thank You";
                        Core.MailSend.SendMail(emMessage);

                        ComWebUsrGrpInfo objGrp = new ComWebUsrGrpInfo();
                        objGrp.UsrCd = qResult["V_USR_CD"].ConvertToString();
                        bnkUsrCd = objGrp.UsrCd.ToString();
                        designation = objUsers.Designation.ToString();
                        objGrp.GrpCd = "55";
                        objGrp.EnteredBy = strUserName.Trim();
                        objGrp.LastUpdatedBy = objGroup.EnterBy.ConvertToString().Trim();
                        objGrp.Ipaddress = CommonVariables.IPAddress;
                        objGrp.Mode = "I";
                        QueryResult qrGrp = service.SubmitChanges(objGrp, true);
                       
                    }
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

            if(mode.ToUpper() == "I")
            { 
                using(ServiceFactory service01 = new ServiceFactory())
                {
                    try {
                        service01.Begin();
                        QueryResult saveBranchUser = service01.SubmitChanges("PR_ADD_BRANCH_USER",
                                              bnkCd,
                                              bnkBranchStd,
                                              bnkUsrCd,
                                              designation
                                              );
                   }
                    catch (OracleException oe)
                    {
                        service01.RollBack();
                        ExceptionManager.AppendLog(oe);
                    }
                    catch (Exception ex)
                    {
                        service01.RollBack();
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {

                        if (service01.Transaction != null)
                        {
                            service01.End();
                        }
                    }


                }
            }
            else if (mode.ToUpper() == "U")
            {
                using (ServiceFactory service01 = new ServiceFactory())
                {
                    try
                    {
                        service01.Begin();
                        QueryResult saveBranchUser = service01.SubmitChanges("PR_UPDATE_BRANCH_USER",
                                              bnkCd,
                                              bnkBranchStd,
                                              bnkUsrCd,
                                              designation
                                              );
                    }
                    catch (OracleException oe)
                    {
                        service01.RollBack();
                        ExceptionManager.AppendLog(oe);
                    }
                    catch (Exception ex)
                    {
                        service01.RollBack();
                        ExceptionManager.AppendLog(ex);
                    }
                    finally
                    {

                        if (service01.Transaction != null)
                        {
                            service01.End();
                        }
                    }


                }
            }
           
            return qResult.IsSuccess;
        }

        public bool CheckBankAdminUser(string user_cd)
        {
            bool isAdmin = false;
            DataTable dt = new DataTable();
           
            string cmd = "";

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();

                    cmd = "SELECT BANK_USER_CD from NHRS_BANK_USERS WHERE USR_CD = " + user_cd + " AND ISADMIN = 'Y' ";

                    dt = service.GetDataTable(new
                    {
                        query = cmd,
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

            if(dt.Rows.Count > 0)
            {
                isAdmin = true;
            }

            return isAdmin;

        }
        public string IsAdminBankUser()
        {
            EnrollmentImportExport objService = new EnrollmentImportExport();

            string user_cd = CommonVariables.UserCode.ToString();
            bool result = objService.CheckBankAdminUser(user_cd);
            if (result)
                return "T";
            return "F";
        }
        public DataTable SearchUsers(string initialStr, string email, string userName, string groupCode, string orderby, string order,string bank_cd)
        {
            DataTable dtbl = null;
            string UserStatus = IsAdminBankUser();
            string grp_cd = CommonVariables.GroupCD.ToString();
            using (ServiceFactory service = new ServiceFactory())
            {


                string cmdText = @"select COM_WEB_USR.USR_CD, COM_EMPLOYEE.DEF_EMPLOYEE_CD, COM_WEB_USR.USR_NAME,COM_WEB_USR.EMAIL, COM_WEB_GRP.GRP_NAME, COM_WEB_USR.EXPIRY_DT, 
                                 COM_WEB_USR.STATUS,COM_WEB_USR.APPROVED FROM COM_WEB_USR LEFT JOIN COM_WEB_USR_GRP ON COM_WEB_USR.USR_CD=COM_WEB_USR_GRP.USR_CD LEFT OUTER JOIN 
                                 COM_WEB_GRP ON COM_WEB_USR_GRP.GRP_CD=COM_WEB_GRP.GRP_CD LEFT OUTER JOIN COM_EMPLOYEE ON COM_WEB_USR.EMP_CD=COM_EMPLOYEE.EMPLOYEE_CD 
                                 LEFT OUTER JOIN NHRS_BANK_USERS ON NHRS_BANK_USERS.USR_CD = COM_WEB_USR_GRP.USR_CD WHERE 1=1 AND NHRS_BANK_USERS.ISADMIN = 'N' ";

                if (grp_cd != "1" && grp_cd != "33" && grp_cd != "35")
                {
                    cmdText += String.Format(" AND Upper(COM_WEB_USR.BANK_CD) = '{0}'", bank_cd);
                }
                if (UserStatus != "T" && grp_cd != "1" && grp_cd != "33" && grp_cd != "35")
                {
                    cmdText += String.Format(" AND Upper(COM_WEB_USR.USR_CD) = '{0}'", CommonVariables.UserCode);
                }

                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" AND Upper(COM_WEB_USR.USR_NAME) like '{0}%'", initialStr);
                }
                if (groupCode != null && groupCode != "")
                {
                    cmdText += " AND COM_WEB_USR_GRP.GRP_CD = '" + groupCode.ToUpper() + "'";
                }
                if (userName != "")
                {
                    cmdText += String.Format(" AND UPPER(COM_WEB_USR.USR_NAME) LIKE '%" + userName.ToUpper() + "%'");
                }
                if (email != "")
                {
                    cmdText += String.Format(" AND COM_WEB_USR.EMAIL LIKE '%" + email + "%'");
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby.ToUpper() == "EXPIRY_DT")
                    {
                        cmdText += String.Format(" order by  ({0}) {1}", orderby, order);
                    }
                    else if (orderby.ToUpper() == "USR_CD")
                    {
                        cmdText += String.Format(" order by TO_NUMBER(COM_WEB_USR.USR_CD) " + order + "");
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }
                }
                else
                {
                    cmdText += String.Format(" order by TO_NUMBER(COM_WEB_USR.USR_CD) " + order + "");
                }
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {


                        }
                    });
                }
                catch (Exception)
                {
                    dtbl = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                return dtbl;
            }
        }
        
           
        public DataTable GetIssueList(PaymentPartnerOrganization objPO)
        {
            DataTable dt = new DataTable();
            Object p_donor_cd = DBNull.Value;
            Object p_district_cd = DBNull.Value;
            Object p_vdc_mun_cd = DBNull.Value;
            Object p_ward = DBNull.Value;
            Object p_tranche = DBNull.Value;
            Object P_OUT_RESULT = DBNull.Value;

            if(!string.IsNullOrEmpty(objPO.Donor_CD))
            {
                p_donor_cd = objPO.Donor_CD;
            }
            if (!string.IsNullOrEmpty(objPO.Dis_Cd))
            {
                p_district_cd = objPO.Dis_Cd;
            }
            if (!string.IsNullOrEmpty(objPO.Vdc_Mun_Cd))
            {
                p_vdc_mun_cd = objPO.Vdc_Mun_Cd;
            }
            if (!string.IsNullOrEmpty(objPO.Ward_Num))
            {
                p_ward = objPO.Ward_Num;
            }
            if (!string.IsNullOrEmpty(objPO.payroll_install_cd))
            {
                p_tranche = objPO.payroll_install_cd;
            }

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.SubmitChanges("PR_FLAG_PO_ERROR",
                                              p_donor_cd,
                                              p_tranche);

                    dt = service.GetDataTable(true, "PR_NHRS_GET_PO_ERROR_LIST",
                                                     p_donor_cd,
                                                     p_district_cd,
                                                     p_vdc_mun_cd,
                                                     p_ward,
                                                     p_tranche,
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
    
    }
}
