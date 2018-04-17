using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;
using EntityFramework;


namespace MIS.Services.Report
{
    public class CastewiseProvertyScore
    {
        public DataTable GetCasteGroupWisePovertyReport(string region, string zoneCode, string districtCode, string casteCode, string VdcCd, string ward,string area, string rule,string scoreFrom,string scoreTo)
        {
            DataTable dtbl = null;           
            Object P_session_id = DBNull.Value;
            Object P_region = DBNull.Value;
            Object P_zone = DBNull.Value;         
            Object P_district_cd = DBNull.Value;
            Object P_zone_cd = DBNull.Value;
            Object P_VdcMun = DBNull.Value; ;
            Object P_Ward = DBNull.Value;
            Object p_area = DBNull.Value;
            Object P_caste_cd = DBNull.Value;
            Object P_rule = DBNull.Value;
            Object P_scoreFrom = DBNull.Value;
            Object P_scoreTo = DBNull.Value;
            Object P_lang = Utils.ToggleLanguage("E", "N");

            using (ServiceFactory service = new ServiceFactory())
            {

                if (region != "" && region != null)
                {
                    P_region = region;
                }
                if (zoneCode != "" && zoneCode != null)
                {
                    P_zone_cd = zoneCode;
                }
                if (districtCode != "" && districtCode != null)
                {
                    P_district_cd = districtCode;
                }
                if (VdcCd != "" && VdcCd != null)
                {
                    P_VdcMun = VdcCd;
                }

                if (ward != "" && ward != null)
                {
                    P_Ward = ward;
                }
                if (area != "" && area != null)
                {
                    p_area = area;
                }
                if (casteCode != "" && casteCode != null)
                {
                    P_caste_cd = casteCode;
                }
                if (rule != "" && rule != null)
                {
                    P_rule = rule;
                }
                if (scoreFrom != "" && scoreFrom != null)
                {
                    P_scoreFrom = scoreFrom;
                }
                if (scoreTo != "" && scoreTo != null)
                {
                    P_scoreTo = scoreTo;
                }
                service.Begin();
                try
                {
                    service.PackageName = "PKG_MIS_REPORT";
                    dtbl = service.GetDataTable(true, "PR_MIS_POVERTY_CASTE", 
                        P_region,
                        P_zone,
                        P_district_cd,                     
                        P_VdcMun,
                        P_Ward,
                        p_area,
                        P_caste_cd,
                        P_rule,
                        P_lang,
                        P_scoreFrom,
                        P_scoreTo,
                        DBNull.Value);
                    string truncateQuery = "TRUNCATE TABLE {0}";
                    string dropQuery = "DROP TABLE {0}";
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {
                        truncateQuery = string.Format(truncateQuery, dtbl.Rows[0]["TBL_NAME"].ConvertToString());
                        dropQuery = string.Format(dropQuery, dtbl.Rows[0]["TBL_NAME"].ConvertToString());
                        service.SubmitChanges(truncateQuery, null);
                        service.SubmitChanges(dropQuery, null);

                        dtbl.Columns.Remove("TBL_NAME");
                    }
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
