using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;

namespace MIS.Services.Report
{
    public class DeputedEngineersService
    {

        public DataTable GetEngineersSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
             

      
            

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
             
 
            try
            {
                service.Begin();
                service.PackageName = "PKG_NHRS_DEPUTED_ENGINEER_INFO";
                dt = service.GetDataTable(true, "pr_engineer_sum_rpt",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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


        public DataTable GetEngineersDetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;

             

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString(); 
            try
            {
                service.Begin();
                service.PackageName = "PKG_NHRS_DEPUTED_ENGINEER_INFO";
                dt = service.GetDataTable(true, "pr_engineer_dtl_rpt",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
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
    }
}
