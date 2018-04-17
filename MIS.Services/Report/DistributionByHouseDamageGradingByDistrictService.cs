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
    public class DistributionByHouseDamageGradingByDistrictService
    {
        public DataTable GetDamageGradeCount()
        {
            DataTable dtbl = null;
            Object pSessionId = DBNull.Value;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    string resPackageName = service.PackageName;
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataTable(true, "PR_DB_DAMAGE_GRADE1", pSessionId, Utils.ToggleLanguage("E", "N"), DBNull.Value);
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

        public DataTable GetDamageGradeCountGraph()
        {
            DataTable dtbl = null;
            Object pSessionId = DBNull.Value;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    string resPackageName = service.PackageName;
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataTable(true, "PR_DB_DAMAGE_GRADE", pSessionId, Utils.ToggleLanguage("E", "E"), DBNull.Value);
                    dtbl.Columns.Remove("Unrecognized");
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
        public DataTable DamageGrade()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS ColTitle FROM NHRS_DAMAGE_GRADE where DAMAGE_GRADE_CD!='0'";
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
