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
    public class InspectionReportService
    {

        public DataTable GetInspectionSummaryReport(NameValueCollection paramValues)
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
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_INSPECTION_APPROVE_IDENTIFY",
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

        public DataTable GetInspectionDailyReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_USER = DBNull.Value;
            Object P_NRACD = DBNull.Value;
            Object P_DESIGNNO = DBNull.Value;
             Object P_SIZE = DBNull.Value;
             Object P_INDEX = DBNull.Value;


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();           
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["user"].ConvertToString() != string.Empty && paramValues["user"].ConvertToString() != "undefined")
                P_USER = paramValues["user"].ConvertToString();
            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();
            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();
                        try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_INSP_DAILY_REPORT",
                     P_USER,
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,                  
                    P_SIZE,
                    P_INDEX,
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

        public DataTable GetInspectionDailySummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_USER = DBNull.Value;
            Object P_NRACD = DBNull.Value;
            Object P_DESIGNNO = DBNull.Value;
            Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC_CD = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["user"].ConvertToString() != string.Empty && paramValues["user"].ConvertToString() != "undefined")
                P_USER = paramValues["user"].ConvertToString();
            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();
            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_INSP_DAILY_SUM_REPORT",
                     P_USER,
                    P_DISTRICT_CD,
                    P_VDC_CD,
                    P_WARD,
                    P_SIZE,
                    P_INDEX,
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
