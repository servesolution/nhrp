using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MIS.Services.Report
{
    public class GetSummaryReportService
    {
        public DataTable GetSummaryReport(string lan)
        {
            

            DataTable dtbl = null;
            Object pSessionId = DBNull.Value;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    string resPackageName = service.PackageName;

                    dtbl = service.GetDataTable(true, "GET_DASHBOARD_REPORT", lan, DBNull.Value, DBNull.Value, DBNull.Value);
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
    }
}