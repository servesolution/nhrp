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
    public class EnrollmentDetailReportService
    {
        public DataTable EnrollmentDetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_MONTHLY_INCOME = DBNull.Value;
            Object P_SIZE = "100";
            Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
            Object P_WARD_NO_CURRENT = DBNull.Value;

            //Object P_INDEX="1";

            //Object P_SIZE = DBNull.Value;
            Object P_INDEX = DBNull.Value;

            Object P_exportcheck = DBNull.Value;

            if (paramValues["exportcheck"].ConvertToString() != string.Empty)
                P_exportcheck = paramValues["exportcheck"].ConvertToString();

            if(P_exportcheck.ConvertToString()=="1")
            {
                P_SIZE = "";
            }

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_DISTRICT_CD = paramValues["dist"].ConvertToString();
 


            if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
                P_INDEX = paramValues["pageindex"].ConvertToString();


            if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
                P_SIZE = paramValues["pagesize"].ConvertToString();


            if (paramValues["pageindex"].ConvertToString() == string.Empty)
            {
                P_INDEX = "1";
            }
            if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
                P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
            if (paramValues["wardCurr"].ConvertToString() != string.Empty)
                P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_ENROLLMENT_DETAIL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_DISTRICT_CD, 
                    P_VDC_MUN_CD_CURRENT,
                    P_WARD_NO_CURRENT,
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


        public DataTable EnrollmentDetailReportForExport(NameValueCollection paramValues)
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
                dt = service.GetDataTable(true, "PR_ENROLLMENT_DETAIL_RPT",
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
