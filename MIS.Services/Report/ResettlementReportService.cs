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
   public class ResettlementReportService
    {
       //public DataTable ResettlementBySummary(NameValueCollection paramValues)
       //{
       //    DataTable dt = null;
       //    ServiceFactory service = new ServiceFactory();
       //    Object P_DISTRICT_CD = DBNull.Value;
       //    Object P_VDC_MUN_CD = DBNull.Value;
       //    Object P_WARD_NO = DBNull.Value;
       //    if (paramValues["dist"].ConvertToString() != string.Empty)
       //        P_DISTRICT_CD = paramValues["dist"].ConvertToString();
       //    if (paramValues["vdc"].ConvertToString() != string.Empty)
       //        P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
       //    if (paramValues["ward"].ConvertToString() != string.Empty)
       //        P_WARD_NO = paramValues["ward"].ConvertToString();
 
       //    try
       //    {
       //        service.Begin();
       //        dt = service.GetDataTable(true, "pr_RESETTLEMENT_BENF_report",
       //            paramValues["lang"].ConvertToString(),
           
       //            P_DISTRICT_CD,
       //             P_VDC_MUN_CD,
       //            P_WARD_NO,
           
       //            DBNull.Value);
       //    }
       //    catch (OracleException oe)
       //    {
       //        dt = null;
       //        ExceptionManager.AppendLog(oe);
       //    }
       //    catch (Exception ex)
       //    {
       //        dt = null;
       //        ExceptionManager.AppendLog(ex);
       //    }
       //    finally
       //    {
       //        if (service.Transaction != null)
       //        {
       //            service.End();
       //        }
       //    }
       //    return dt;
       //}

        public DataTable GetResettlementReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_ReviewType = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            if (paramValues["ReviewType"].ConvertToString() != string.Empty)
                P_ReviewType = paramValues["ReviewType"].ConvertToString();
        
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_RESETLMENT_DTL_RPRT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,
                   P_ReviewType,
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

        public DataTable GetResettlementNonBenfDetail(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_RESET_NONBENF_DTL_RPT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,

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
        public DataTable GetResettlementSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_ReviewType = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();

            if (paramValues["ReviewType"].ConvertToString() != string.Empty)
                P_ReviewType  = paramValues["ReviewType"].ConvertToString();
        
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_RESETTLEMENT_SUMMARY",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD,
                    P_VDC_MUN_CD,
                    P_WARD_NO,
                   P_ReviewType,
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
        public DataTable GetResettlementNonBenfSummary(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD_NO = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_RESET_NONBENF_SUMMARY",
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
