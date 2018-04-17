using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using MIS.Models.Enrollment;
using MIS.Models.Payment.HDSP;

namespace MIS.Services.Payment.HDSP
{
   public class GetApprovePaymentListService
    {
       public DataTable getapprovePaymentList(NameValueCollection paramValues)
       {
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           //Object P_Fiscal_Yr = DBNull.Value;
           Object P_bank=DBNull.Value;
           Object P_Bank_Branch = DBNull.Value;
           Object P_SIZE = "100";
           Object P_exportcheck = DBNull.Value;

           if (paramValues["exportcheck"].ConvertToString() != string.Empty && paramValues["exportcheck"].ConvertToString() != "undefined")
               P_exportcheck = paramValues["exportcheck"].ConvertToString();

           if(P_exportcheck.ConvertToString()=="1")
           {
               P_SIZE = "";
           }

           DataTable dt = new DataTable();
           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
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
       //public DataTable getapprovePaymentList(EnrollmentSearch objEnrollmentSearch)
       //{
       //    Object P_Dist = DBNull.Value;
       //    Object P_VDC = DBNull.Value;
       //    Object P_WARD = DBNull.Value;
       //    //Object P_Fiscal_Yr = DBNull.Value;
       //    Object P_bank = DBNull.Value;
       //    Object P_Bank_Branch = DBNull.Value;
       //    DataTable dt = new DataTable();
       //    if (!string.IsNullOrEmpty(objEnrollmentSearch.DistrictCd))
       //    {
       //        P_Dist = Decimal.Parse(objEnrollmentSearch.DistrictCd);
       //    }
       //    if (!string.IsNullOrEmpty(objEnrollmentSearch.VDCMun))
       //    {
       //        P_VDC = Decimal.Parse(objEnrollmentSearch.VDCMun);
       //    }
       //    if (!string.IsNullOrEmpty(objEnrollmentSearch.WardNo))
       //    {
       //        P_WARD = Decimal.Parse(objEnrollmentSearch.WardNo);
       //    }
       //    //if (paramValues["Fiscalyr"].ConvertToString() != string.Empty && paramValues["Fiscalyr"].ConvertToString() != "undefined")
       //    //    P_Fiscal_Yr = paramValues["Fiscalyr"].ConvertToString();

       //    using (ServiceFactory service = new ServiceFactory())
       //    {
       //        try
       //        {
       //            service.Begin();
       //            dt = service.GetDataTable(true, "PR_PAYMENT_FIRST_ENROLL_REPORT",
       //            'E',
       //            P_Dist,
       //            P_VDC,
       //            P_WARD,
       //                // P_Fiscal_Yr,

       //           DBNull.Value
       //           );

       //        }
       //        catch (OracleException oe)
       //        {
       //            service.RollBack();
       //            ExceptionManager.AppendLog(oe);
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

       //    }
       //    return dt;
       //}
       public DataTable getinspectedpaymentList(NameValueCollection paramValues)
       {
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
          // Object P_Fiscal_Yr = DBNull.Value;
           Object P_bank = DBNull.Value;
           Object P_Bank_Branch = DBNull.Value;
           Object P_Installment = DBNull.Value;


           Object P_SIZE = "100";
           Object P_exportcheck = DBNull.Value;

           if (paramValues["exportcheck"].ConvertToString() != string.Empty && paramValues["exportcheck"].ConvertToString() != "undefined")
               P_exportcheck = paramValues["exportcheck"].ConvertToString();

           if (P_exportcheck.ConvertToString() == "1")
           {
               P_SIZE = "";
           }




           DataTable dt = new DataTable();
           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           //if (paramValues["Fiscalyr"].ConvertToString() != string.Empty && paramValues["Fiscalyr"].ConvertToString() != "undefined")
           //    P_Fiscal_Yr = paramValues["Fiscalyr"].ConvertToString();
           if (paramValues["bankname"].ConvertToString() != string.Empty && paramValues["bankname"].ConvertToString() != "undefined")
               P_bank = paramValues["bankname"].ConvertToString();
           if (paramValues["branchname"].ConvertToString() != string.Empty && paramValues["branchname"].ConvertToString() != "undefined")
               P_Bank_Branch = paramValues["branchname"].ConvertToString();
           if (paramValues["installation"].ConvertToString() != string.Empty && paramValues["installation"].ConvertToString() != "undefined")
               P_Installment = paramValues["installation"].ConvertToString();
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "PR_INSPECTED_TERRIS_LIST",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   //P_Fiscal_Yr,
                   P_Installment,
                   P_bank,
                   P_Bank_Branch,
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
       public DataTable getFirstinspectedpaymentList(EnrollmentSearch objEnrollmentSearch)
       {

           DataTable dt = new DataTable();
           String docData = String.Empty;
           Object P_Out_Enrollment = DBNull.Value;

           Object P_district_cd = DBNull.Value;
           Object P_vdc_mun_cd = DBNull.Value;
           Object p_session_id = DBNull.Value;
           Object p_ward_no = DBNull.Value;
           Object p_install_cd = DBNull.Value;


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
           if (!string.IsNullOrEmpty(objEnrollmentSearch.InstallationCd))
           {
               p_install_cd = Decimal.Parse(objEnrollmentSearch.InstallationCd);
           }
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {

                   service.Begin();
                   //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                   dt = service.GetDataTable(true, "PR_INSPECT_SEC_LIST",
                                                   
                                                    P_district_cd,
                                                    P_vdc_mun_cd,
                                                    p_ward_no,
                                                    p_install_cd,
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

       public DataTable  getSecondinspectedpaymentList(EnrollmentSearch objEnrollmentSearch)
       {

           DataTable dt = new DataTable();
           String docData = String.Empty;
           Object P_Out_Enrollment = DBNull.Value;

           Object P_district_cd = DBNull.Value;
           Object P_vdc_mun_cd = DBNull.Value;
           Object p_session_id = DBNull.Value;
           Object p_ward_no = DBNull.Value;
           Object p_install_cd = DBNull.Value;


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
           if (!string.IsNullOrEmpty(objEnrollmentSearch.InstallationCd))
           {
               p_install_cd = Decimal.Parse(objEnrollmentSearch.InstallationCd);
           }
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {

                   service.Begin();
                   //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                   dt = service.GetDataTable(true, "PR_INSPECT_THIRD_LIST",
                                                   
                                                    P_district_cd,
                                                    P_vdc_mun_cd,
                                                    p_ward_no,
                                                    p_install_cd,
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
       public DataTable getInspectedFileList(NRATOBANKDownload objEnrollmentSearch)
       {

           DataTable dt = new DataTable();
           String docData = String.Empty;
           Object P_Out_Enrollment = DBNull.Value;

           Object P_district_cd = DBNull.Value;
           Object P_vdc_mun_cd = DBNull.Value;           
           Object p_ward_no = DBNull.Value;
           Object p_install_cd = DBNull.Value;
           Object p_bank_cd = DBNull.Value;
           Object p_bank_branch_cd = DBNull.Value;
           Object p_transac_dt_from = DBNull.Value;
           Object p_transac_dt_to = DBNull.Value;


           if (!string.IsNullOrEmpty(objEnrollmentSearch.DISTRICT_CD))
           {
               P_district_cd = Decimal.Parse(objEnrollmentSearch.DISTRICT_CD);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.VDC_MUN_CD))
           {
               P_vdc_mun_cd = Decimal.Parse(objEnrollmentSearch.VDC_MUN_CD);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.WARD_NO))
           {
               p_ward_no = Decimal.Parse(objEnrollmentSearch.WARD_NO);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.BANK_CD))
           {
               p_bank_cd = Decimal.Parse(objEnrollmentSearch.BANK_CD);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.BANK_BRANCH_CD))
           {
               p_bank_branch_cd = Decimal.Parse(objEnrollmentSearch.BANK_BRANCH_CD);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.PAYROLL_INSTALL_CD))
           {
               p_install_cd = Decimal.Parse(objEnrollmentSearch.PAYROLL_INSTALL_CD);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.Transaction_Dt_From))
           {
               p_transac_dt_from = (objEnrollmentSearch.Transaction_Dt_From);
                   //DATETIME.Parse(objEnrollmentSearch.Transaction_Dt_From);
           }
           if (!string.IsNullOrEmpty(objEnrollmentSearch.Transaction_Dt_To))
           {
               p_transac_dt_to = (objEnrollmentSearch.Transaction_Dt_To);
               //string TRANSAC_DT = p_transac_dt_to.ToString("YYYY-MMM-DD");
           }
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {

                   service.Begin();
                   //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                   dt = service.GetDataTable(true, "PR_INSPECT_FILE_LIST",

                                                    P_district_cd,
                                                    P_vdc_mun_cd,
                                                    p_ward_no,
                                                    p_install_cd,
                                                    p_bank_cd,
                                                    p_bank_branch_cd,
                                                    p_transac_dt_from,
                                                    p_transac_dt_to,
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
       public DataTable GetTransactionLog(NRATOBANKDownload objEnrollmentSearch)
       {


           DataTable dtbl = null;
           using (ServiceFactory service = new ServiceFactory())
           {
               service.Begin();
               // string cmdText = "SELECT a.*,c.PERM_NAME FROM COM_MENU_SECURITY a,COM_MENU b,COM_WEB_PERMISSION c WHERE a.MENU_CD=b.MENU_CD AND a.PERM_CD=c.PERM_CD AND b.LINK_URL='" + menuName + "' AND a.grp_cd='" + GroupCD + "'";
               string cmdText = "";
               if(objEnrollmentSearch.Transaction_Dt_From!="" && objEnrollmentSearch.Transaction_Dt_To!="")
               {
                    cmdText = "select FILENAME FILENAME,'' APPROVEDDATE ,TO_CHAR(ENTERED_DATE,'mm/dd/yyyy') UPLOADDATE,ENTERED_BY PROCESSEDBY,'UPLOAD'ACTION  FROM nhrs_reverse_feed_file_batch"
                                  + " where ('1'='1" + objEnrollmentSearch.DISTRICT_CD + "' OR DISTRICT_CD = '" + objEnrollmentSearch.DISTRICT_CD + "') AND ( '1'='1" + objEnrollmentSearch.VDC_MUN_CD + "' OR VDC_MUN_CD='" + objEnrollmentSearch.VDC_MUN_CD + "') AND ( '1'='1" + objEnrollmentSearch.WARD_NO + "' OR WARD_NO='" + objEnrollmentSearch.WARD_NO + "')  and ENTERED_DATE between TO_DATE ('" + objEnrollmentSearch.Transaction_Dt_From + "', 'yyyy/mm/dd') AND TO_DATE ('" + objEnrollmentSearch.Transaction_Dt_To + "', 'yyyy/mm/dd'"
                                + " )"
                                + " UNION ALL"
                                + " select FILE_NAME FILENAME,NVL(APPROVED_DT,'') APPROVEDDATE , ''UPLOADDATE,APPROVED_BY PROCESSEDBY,'APPROVED' ACTION  FROM NHRS_SEC_PAYMENT_APPROVE"
                                 + " where ('1'='1" + objEnrollmentSearch.DISTRICT_CD + "' OR DISTRICT_CD = '" + objEnrollmentSearch.DISTRICT_CD + "') AND ( '1'='1" + objEnrollmentSearch.VDC_MUN_CD + "' OR VDC_MUN_CD='" + objEnrollmentSearch.VDC_MUN_CD + "') AND ( '1'='1" + objEnrollmentSearch.WARD_NO + "' OR WARD_NO='" + objEnrollmentSearch.WARD_NO + "') and TRANSACTION_DT between TO_DATE ('" + objEnrollmentSearch.Transaction_Dt_From + "', 'yyyy/mm/dd') AND TO_DATE ('" + objEnrollmentSearch.Transaction_Dt_To + "', 'yyyy/mm/dd'"
                                + " )";
               }
               else
               {
                    cmdText = "select FILENAME FILENAME,'' APPROVEDDATE ,TO_CHAR(ENTERED_DATE,'mm/dd/yyyy') UPLOADDATE,ENTERED_BY PROCESSEDBY,'UPLOAD'ACTION  FROM nhrs_reverse_feed_file_batch"
                                  + " where ('1'='1" + objEnrollmentSearch.DISTRICT_CD + "' OR DISTRICT_CD = '" + objEnrollmentSearch.DISTRICT_CD + "') AND ( '1'='1" + objEnrollmentSearch.VDC_MUN_CD + "' OR VDC_MUN_CD='" + objEnrollmentSearch.VDC_MUN_CD + "') AND ( '1'='1" + objEnrollmentSearch.WARD_NO + "' OR WARD_NO='" + objEnrollmentSearch.WARD_NO + "'  "
                                + " )"
                                + " UNION ALL"
                                + " select FILE_NAME FILENAME,NVL(APPROVED_DT,'') APPROVEDDATE , ''UPLOADDATE,APPROVED_BY PROCESSEDBY,'APPROVED' ACTION  FROM NHRS_SEC_PAYMENT_APPROVE"
                                 + " where ('1'='1" + objEnrollmentSearch.DISTRICT_CD + "' OR DISTRICT_CD = '" + objEnrollmentSearch.DISTRICT_CD + "') AND ( '1'='1" + objEnrollmentSearch.VDC_MUN_CD + "' OR VDC_MUN_CD='" + objEnrollmentSearch.VDC_MUN_CD + "') AND ( '1'='1" + objEnrollmentSearch.WARD_NO + "' OR WARD_NO='" + objEnrollmentSearch.WARD_NO + "'"
                                + " )";

               }

             
               try
               {
                   dtbl = service.GetDataTable(cmdText, null);
               }
               catch (Exception ex)
               {
                   dtbl = null;
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
           return dtbl;


           //DataTable dt = new DataTable();
           //String docData = String.Empty;
           //Object P_Out_Enrollment = DBNull.Value;

           //Object P_district_cd = DBNull.Value;
           //Object P_vdc_mun_cd = DBNull.Value;
           //Object p_ward_no = DBNull.Value;
           //Object p_install_cd = DBNull.Value;
           //Object p_bank_cd = DBNull.Value;
           //Object p_bank_branch_cd = DBNull.Value;
           //Object p_transac_dt_from = DBNull.Value;
           //Object p_transac_dt_to = DBNull.Value;


           //if (!string.IsNullOrEmpty(objEnrollmentSearch.DISTRICT_CD))
           //{
           //    P_district_cd = Decimal.Parse(objEnrollmentSearch.DISTRICT_CD);
           //}
           //if (!string.IsNullOrEmpty(objEnrollmentSearch.VDC_MUN_CD))
           //{
           //    P_vdc_mun_cd = Decimal.Parse(objEnrollmentSearch.VDC_MUN_CD);
           //}
           //if (!string.IsNullOrEmpty(objEnrollmentSearch.WARD_NO))
           //{
           //    p_ward_no = Decimal.Parse(objEnrollmentSearch.WARD_NO);
           //}

           //if (!string.IsNullOrEmpty(objEnrollmentSearch.PAYROLL_INSTALL_CD))
           //{
           //    p_install_cd = Decimal.Parse(objEnrollmentSearch.PAYROLL_INSTALL_CD);
           //}
           //if (!string.IsNullOrEmpty(objEnrollmentSearch.Transaction_Dt_From))
           //{
           //    p_transac_dt_from = (objEnrollmentSearch.Transaction_Dt_From);
           //    //DATETIME.Parse(objEnrollmentSearch.Transaction_Dt_From);
           //}
           //if (!string.IsNullOrEmpty(objEnrollmentSearch.Transaction_Dt_To))
           //{
           //    p_transac_dt_to = (objEnrollmentSearch.Transaction_Dt_To);
           //    //string TRANSAC_DT = p_transac_dt_to.ToString("YYYY-MMM-DD");
           //}
           //using (ServiceFactory service = new ServiceFactory())
           //{
           //    try
           //    {

           //        service.Begin();
           //        //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
           //        dt = service.GetDataTable(true, "PR_TRANSACTION_LOG",

           //                                         P_district_cd,
           //                                         P_vdc_mun_cd,
           //                                         p_ward_no,
           //                                         //p_install_cd,
           //                                         //p_bank_cd,
           //                                         //p_bank_branch_cd,
           //                                         //p_transac_dt_from,
           //                                         //p_transac_dt_to,
           //                                         P_Out_Enrollment
           //                                         );


           //    }

           //    catch (OracleException oe)
           //    {
           //        service.RollBack();
           //        ExceptionManager.AppendLog(oe);
           //    }
           //    catch (Exception ex)
           //    {
           //        dt = null;
           //        ExceptionHandler.ExceptionManager.AppendLog(ex);
           //    }
           //    finally
           //    {
           //        if (service.Transaction != null)
           //        {
           //            service.End();
           //        }
           //    }
           //}

           //return dt;
       }  
    }
}
