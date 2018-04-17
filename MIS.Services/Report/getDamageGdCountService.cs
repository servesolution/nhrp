using EntityFramework;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MIS.Services.Report
{
    public class getDamageGdCountService
    {
        public DataTable getDamageCount()
        {
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataTable(true, "PR_DASHBOARD_GRADEC_REPORT",
                        Utils.ToggleLanguage("E", "N"),
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
    }
}
