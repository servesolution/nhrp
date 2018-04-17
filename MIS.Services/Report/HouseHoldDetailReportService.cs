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
    public class HouseHoldDetailReportService
    {
        public DataTable HouseHoldDetailReport(string regionCode, string zoneCode, string districtCode, string vdcMunCode, string wardCode, string areaCode, string langflag)
        {
            DataTable dtbl = null;
            String cmdTxt = "";
            QueryResult qr = new QueryResult();
            Object p_session_id = DBNull.Value;
            Object p_region_cd = DBNull.Value;
            Object p_zone_cd = DBNull.Value;
            Object p_district_cd = DBNull.Value;
            Object p_vdcmun_cd = DBNull.Value;
            Object p_ward_cd = DBNull.Value;
            Object p_area = DBNull.Value;
            Object p_entered_By = CommonVariables.UserName;
            Object p_lang = Utils.ToggleLanguage("E", "N");
            Object p_householdID = DBNull.Value;
            if (!String.IsNullOrEmpty(regionCode))
            {
                p_region_cd = regionCode;
            }
            if (!String.IsNullOrEmpty(zoneCode))
            {
                p_zone_cd = zoneCode;
            }
            if (!String.IsNullOrEmpty(districtCode))
            {
                p_district_cd = districtCode;
            }
            if (!String.IsNullOrEmpty(vdcMunCode))
            {
                p_vdcmun_cd = vdcMunCode;
            }
            if (!String.IsNullOrEmpty(wardCode))
            {
                p_ward_cd = wardCode;
            }
            if (!String.IsNullOrEmpty(areaCode))
            {
                p_area = areaCode;
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                try
                {
                    service.PackageName = "PKG_MIS_REPORT";
                    qr = service.SubmitChanges("PR_RPT_HOUSEHOLD_SUMMARY",
                        p_session_id,
                        p_region_cd,
                        p_zone_cd,
                        p_district_cd,
                        p_vdcmun_cd,
                        p_ward_cd,
                        p_area,
                        p_householdID,
                        p_lang,
                        p_entered_By

                    );
                    if (qr.IsSuccess)
                    {
                        cmdTxt = "SELECT * FROM MIS_RPT_HOUSEHOLD_SUMMARY WHERE SESSION_ID=" + qr["P_SESSION_ID"].ConvertToString();
                        dtbl = service.GetDataTable(cmdTxt, null);
                    }
                    dtbl = service.GetDataTable(cmdTxt, null);

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
