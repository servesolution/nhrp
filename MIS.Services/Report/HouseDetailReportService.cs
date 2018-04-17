using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Security;
using System.Data;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;

namespace MIS.Services.Report
{
    public class HouseDetailReportService
    {
        public DataTable GetHouseDetailReportServiceForDashboard()
        {
           
            DataTable dtbl = new DataTable();
            string cmdTxt = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                { 
                        cmdTxt = @"select count(house_owner_id) house_cnt, sum(TOTAL_BUILDING_STUCTURE_CNT) building_structure_cnt,
                              sum(TOTAL_HOUSEHOLD_CNT	) family_cnt, sum(TOTAL_MEMBER_CNT) member_cnt,
                              mst.district_cd , dist.desc_eng district_eng,dist.desc_loc district_loc  
                              from NHRS_HOUSE_OWNER_MST mst
                              join mis_district dist on mst.district_cd = dist.district_cd
                              group by mst.district_cd , dist.desc_eng ,dist.desc_loc";
                    
                    dtbl = service.GetDataTable(cmdTxt, null);
                    List<string> result = new List<string>(); 
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

        public DataTable GetHouseDetailReportServiceForDashboardGraph()
        {            
            DataTable dtbl = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_DASHBOARD_REPORT";
                    dtbl = service.GetDataTable(true, "PR_GETHOUSEOWNER_DTL_GRAPH",
                                                Utils.ToggleLanguage("E", "E"),                                                
                                                DBNull.Value
                                               );
                    List<string> result = new List<string>();

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
