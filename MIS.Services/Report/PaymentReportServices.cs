using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using System.Data;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Report
{
    public class PaymentReportServices
    {
        public DataTable getdetailPaymentReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Install = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_LANG = DBNull.Value;
            Object P_BANK_CD = DBNull.Value;
            Object P_Installment = DBNull.Value;

            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_LANG = paramValues["lang"].ConvertToString();

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();

            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();

            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();

            if (paramValues["BANK_CD"].ConvertToString() != string.Empty)
                P_BANK_CD = paramValues["BANK_CD"].ConvertToString();

            if (paramValues["Installment"].ConvertToString() != string.Empty)
                P_Installment = paramValues["Installment"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GRANT_SUM_detail_RPT",
                    P_LANG,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_BANK_CD.ToDecimal(),
                    P_Installment.ToDecimal(),
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getSummaryPaymentReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Install = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_PAYMENT_SUM_REPORT",
                    P_Lang,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),

                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getGranctSummaryPaymentReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Install = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_BANK_CD = DBNull.Value;

            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["Bank"].ConvertToString() != string.Empty)
                P_BANK_CD = paramValues["Bank"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GRANT_SUM_RPT",
                    P_Lang,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_BANK_CD.ToDecimal(),
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getBankClaimSumReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_approved = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            Object P_Installment = DBNull.Value;


            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();
           
            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();
            if (paramValues["branch"].ConvertToString() != string.Empty)
                P_Branch = paramValues["branch"].ConvertToString();

            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();

            if (paramValues["Quarter"].ConvertToString() == "1" || paramValues["Quarter"].ConvertToString() == "2" || paramValues["Quarter"].ConvertToString() == "3")
            {
                P_Quarter = paramValues["Quarter"].ConvertToString();
            }


            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_CLAIM_SUM_REPORT_tst1",
                    P_Lang.ToString(),
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    P_Bank.ToString(),
                    P_Branch.ToString(),
                   DBNull.Value,
                   P_fiscal_yr,
                    P_Quarter,
                     DBNull.Value,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getClaimSumReportBranchWise(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_approved = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            Object P_Installment = DBNull.Value;


            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();

            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();
            if (paramValues["branch"].ConvertToString() != string.Empty)
                P_Branch = paramValues["branch"].ConvertToString();


            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();


            if (paramValues["Quarter"].ConvertToString() == "1" || paramValues["Quarter"].ConvertToString() == "2" || paramValues["Quarter"].ConvertToString() == "3")
            {
                P_Quarter = paramValues["Quarter"].ConvertToString();
            }


            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_CLAIM_SUM_REPORT",
                    'E',
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    P_Bank.ToString(),
                    DBNull.Value,
                   DBNull.Value,
                   P_fiscal_yr,
                    P_Quarter,
                     DBNull.Value,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getBankClaimDtlReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
           
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            

            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();
            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();
            if (paramValues["branch"].ConvertToString() != string.Empty)
                P_Branch = paramValues["branch"].ConvertToString();
            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();

            if (paramValues["Quarter"].ConvertToString() == "1" || paramValues["Quarter"].ConvertToString() == "2" || paramValues["Quarter"].ConvertToString() == "3")
            {
                P_Quarter = paramValues["Quarter"].ConvertToString();
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_PAYMENT_DTL_CLAIM_RPT",
                    P_Lang,
                    DBNull.Value,
                    DBNull.Value,
                    DBNull.Value,
                    P_Branch.ToString(),
                    P_Bank.ToString(),
                    DBNull.Value,
                    P_fiscal_yr.ToString(),
                    P_Quarter.ToString(),
                    DBNull.Value,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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

            return dt;

        }
        public DataTable getPSPUploadReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_approved = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();
          
            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();
            
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_PSP_UPLOAD_BANK_LOG",
                    P_Lang,
                    P_fiscal_yr,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getPSPBankUploadReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_approved = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();


            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();
            if (paramValues["branch"].ConvertToString() != string.Empty)
                P_Branch = paramValues["branch"].ConvertToString();

            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_PSP_COUNT_LOG1",
                    P_Lang,
                    P_fiscal_yr,
                    P_Bank.ToDecimal(),
                    P_Branch.ToString(),
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getPSPBankFiscalYrUploadReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_Lang = DBNull.Value;
            Object P_approved = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();


            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();

            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_GET_BANKFISCLPSP_COUNT_LOG1",
                    P_Lang,
                    P_fiscal_yr,
                    P_Bank.ToDecimal(),

                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getQuarter(string date)
        {
            DataTable dtt = null;
            string cmd = "";
            ServiceFactory service = new ServiceFactory();
            service.Begin();

            if (date != "" && date != null)
            {
                cmd = "SELECT TO_CHAR(TO_DATE('" + date + "', 'mm/dd/yyyy'), 'Q') quarter FROM dual";
            }
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
        public DataTable getdtlInstallmentReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Install = DBNull.Value;
            Object P_Branch = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_LANG = DBNull.Value;
            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_LANG = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["install"].ConvertToString() != string.Empty)
                P_Install = paramValues["install"].ConvertToString();
            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();
            if (paramValues["branch"].ConvertToString() != string.Empty)
                P_Branch = paramValues["branch"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_INSTALLMENT_DTL_RPT",
                  P_LANG,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_Install,
                    P_Branch.ToDecimal(),
                    P_Bank.ToDecimal(),

                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getDTCOGranctSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Install = DBNull.Value;
            Object P_fiscal_yr = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_quarter = DBNull.Value;
            Object P_Lang = DBNull.Value;
            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_Lang = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();

            if (P_Lang.ToString() == "E")
            {
                if (!string.IsNullOrEmpty(paramValues["quarter"].ConvertToString()) && paramValues["quarter"].ConvertToString() != "---Select Quarter---")
                {
                    P_quarter = paramValues["quarter"].ConvertToString();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(paramValues["quarter"].ConvertToString()) && paramValues["quarter"].ConvertToString() != "---चौथाई छान्नुहोस्---")
                {
                    P_quarter = paramValues["quarter"].ConvertToString();
                }
            }
            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscal_yr = paramValues["fiscalyr"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_dtco_SUM_RPT",
                    P_Lang,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_Bank.ToDecimal(),
                    P_fiscal_yr.ConvertToString(),
                    P_quarter.ToDecimal(),
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getDTCOdetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Installment = DBNull.Value;
            Object P_fiscalyr = DBNull.Value;
            Object P_Bank = DBNull.Value;
            Object P_LANG = DBNull.Value;
            Object P_Quarter = DBNull.Value;
            Object P_PA = DBNull.Value;

            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_LANG = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["Installment"].ConvertToString() != string.Empty)
                P_Installment = paramValues["Installment"].ConvertToString();
            if (paramValues["bank"].ConvertToString() != string.Empty)
                P_Bank = paramValues["bank"].ConvertToString();
            if (paramValues["fiscalyr"].ConvertToString() != string.Empty)
                P_fiscalyr = paramValues["fiscalyr"].ConvertToString();
            if (paramValues["PA"].ConvertToString() != string.Empty)
                P_PA = paramValues["PA"].ConvertToString();


            if (P_LANG.ToString() == "E")
            {
                if (!string.IsNullOrEmpty(paramValues["quarter"].ConvertToString()) && paramValues["quarter"].ConvertToString() != "---Select Quarter---")
                {
                    P_Quarter = paramValues["quarter"].ConvertToString();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(paramValues["quarter"].ConvertToString()) && paramValues["quarter"].ConvertToString() != "---चौथाई छान्नुहोस्---")
                {
                    P_Quarter = paramValues["quarter"].ConvertToString();
                }
            }
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_DTCO_detail_RPT",
                  P_LANG, // paramValues["lang"].ConvertToString(),
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_Bank.ToDecimal(),
                    P_fiscalyr.ConvertToString(),
                     P_Quarter.ToDecimal(),
                     P_Installment.ToDecimal(),
                     P_PA,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getDTCOReconcileSumReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Installment = DBNull.Value;
            Object P_Pa = DBNull.Value;
            Object P_FISCALYEAR = DBNull.Value;
            Object P_TRIMESTER = DBNull.Value;
            Object P_LANG = DBNull.Value;
            Object P_BANK = DBNull.Value;

            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_LANG = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["installment"].ConvertToString() != string.Empty)
                P_Installment = paramValues["installment"].ConvertToString();
            if (paramValues["pa"].ConvertToString() != string.Empty)
                P_Pa = paramValues["pa"].ConvertToString();
            if (paramValues["fiscalYear"].ConvertToString() != string.Empty)
                P_FISCALYEAR = paramValues["fiscalYear"].ConvertToString();
            if (paramValues["trimester"].ConvertToString() != string.Empty && paramValues["trimester"].ConvertToString() != "---Select Quarter---")
               
                P_TRIMESTER = paramValues["trimester"].ConvertToString();
            if (paramValues["bank_cd"].ConvertToString() != string.Empty)
                P_BANK = paramValues["bank_cd"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "DTCO_RECONCILE_SUM_REPORT",
                  P_LANG,
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_Pa.ConvertToString(),
                    P_Installment.ToDecimal(),
                    P_FISCALYEAR,
                    P_TRIMESTER,
                    P_BANK,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
        public DataTable getDTCOReconcileReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object P_Installment = DBNull.Value;
            Object P_Pa = DBNull.Value;
            Object P_FISCALYEAR = DBNull.Value;
            Object P_TRIMESTER = DBNull.Value;
            Object P_LANG = DBNull.Value;
            Object P_BANK = DBNull.Value;

            if (paramValues["lang"].ConvertToString() != string.Empty)
                P_LANG = paramValues["lang"].ConvertToString();
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["Ward"].ConvertToString() != string.Empty)
                P_Ward = paramValues["Ward"].ConvertToString();
            if (paramValues["installment"].ConvertToString() != string.Empty)
                P_Installment = paramValues["installment"].ConvertToString();
            if (paramValues["pa"].ConvertToString() != string.Empty)
                P_Pa = paramValues["pa"].ConvertToString();
            if (paramValues["fiscalYear"].ConvertToString() != string.Empty)
                P_FISCALYEAR = paramValues["fiscalYear"].ConvertToString();
            if (paramValues["trimester"].ConvertToString() != string.Empty && paramValues["trimester"].ConvertToString() != "---Select Quarter---")
                P_TRIMESTER = paramValues["trimester"].ConvertToString();
            if (paramValues["bank_cd"].ConvertToString() != string.Empty)
                P_BANK = paramValues["bank_cd"].ConvertToString();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "DTCO_reconcile_report",
                  P_LANG, 
                    P_Dist.ToDecimal(),
                    P_VDC.ToDecimal(),
                    P_Ward.ToDecimal(),
                    P_Pa.ConvertToString(),
                    P_Installment.ToDecimal(),
                    P_FISCALYEAR,
                    P_TRIMESTER,
                    P_BANK,
                    DBNull.Value);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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
            return dt;

        }
    }
}
