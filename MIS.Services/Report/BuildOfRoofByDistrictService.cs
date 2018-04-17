using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MIS.Services.Report
{
    public class BuildOfRoofByDistrictService
    {
        public DataTable GetBuildOfRoofCount()
        {
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    string resPackageName = service.PackageName;
                    service.PackageName = "";
                    dtbl = service.GetDataTable(true, "", DBNull.Value);
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
        public DataTable BuiltOfRoof()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS ColTitle FROM NHRS_ROOF_CONSTRUCT_MATERIAL";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
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
    }
}
