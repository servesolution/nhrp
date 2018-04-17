
using EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
namespace MIS.Services.Report
{
    public class EnrolledSummaryReportSrvice
    {
        public DataTable EnrollmentSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
            string R = "R";
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_ENROLLED_SUMMARY",
                   paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
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


        public DataTable EnrollmentPAReportExport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
            string R = "R";
            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "ENROLLMENT_PA_DETAIL_REPORT",
                   "R",
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
                    DBNull.Value,
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

        public DataTable EnrollmentDetailReportPA(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
           

            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            


            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "ENROLLMENT_PA_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD, 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
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



        public DataTable EnrollmentSummaryReportPA(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_PA_SUMMARY_RPT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
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
