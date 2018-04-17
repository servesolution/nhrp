using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;
using EntityFramework;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Report
{
    public class ChildAgeAveragePovertyScoreDetailsReportService
    {
        #region Exporting
        public String getChildAgeAveragePovertyScoreDetailsData(DataTable dtbl, string region, string zone, string strDistrict, string strVdcMun, string strWard, string area, string strFamilyFrm, string strFamilyTo, string strHouseHldFrm, string strHouseHldTo, string strScoreRangeFrm, string strScoreRangeTo,string rule)
        {
            StringBuilder strData = new StringBuilder();
            //Getting the Data for import
            strData.Append("<table width='140%' cellspacing='0' cellpadding='0' border='0' align='center'><tr>");
            if (region != "" && region != null)
            {
                strData.Append("<td align='left'><b>" + Utils.GetLabel("Region/State") + "</b>:");
                strData.Append("" + region + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (zone != "" && zone != null)
            {
                strData.Append("<td align='left'><b>" + Utils.GetLabel("Zone") + "</b>:");
                strData.Append("" + zone + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (strDistrict != "" && strDistrict != null)
            {
                strData.Append("<td align='left'><b>" + Utils.GetLabel("District") + "</b>:");
                strData.Append("" + strDistrict + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (strVdcMun != "" && strVdcMun != null)
            {
                strData.Append("<td><b>" + Utils.GetLabel("VDC/Municipality") + "</b>:");
                strData.Append("" + strVdcMun + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><tr><td>");
            }
            if (strWard != "" && strWard != null)
            {
                strData.Append("<b>" + Utils.GetLabel("Ward") + "</b>:" + Utils.GetLabel(strWard) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (area != "" && area != null)
            {
                strData.Append("<td><b>" + Utils.GetLabel("Area") + "</b>:" + Utils.GetLabel(area) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (strFamilyFrm != "" && strFamilyFrm != null)
            {
                strData.Append("<td><b>" + Utils.GetLabel("Age Range From") + "</b>:");
                strData.Append("" + Utils.GetLabel(strFamilyFrm) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (strFamilyTo != "" && strFamilyTo != null)
            {
                strData.Append("<td><b>" + Utils.GetLabel("Age Range To") + "</b>:");
                strData.Append("" + Utils.GetLabel(strFamilyTo) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>");
            }
            if (strHouseHldFrm != "" && strHouseHldFrm != null)
            {
                strData.Append("<tr><td><b>" + Utils.GetLabel("Household From") + "</b>:");
                strData.Append("" + Utils.GetLabel(strHouseHldFrm) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (strHouseHldTo != "" && strHouseHldTo != null)
            {
                strData.Append("<td><b>" + Utils.GetLabel("Household To") + "</b>:");
                strData.Append("" + Utils.GetLabel(strHouseHldTo) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
            }
            if (strScoreRangeFrm != "" && strScoreRangeTo != "")
            {
                strData.Append("<td><span style='margin-left: 3px'><b>" + Utils.GetLabel("Score From") + "</b>:</span><span style='margin-left: 6px'>");
                strData.Append("" + Utils.GetLabel(strScoreRangeFrm) + "</span></td>");

                strData.Append("<td><span style='margin-left: 3px'><b>" + Utils.GetLabel("Score To") + "</b>:</span><span style='margin-left: 6px'>");
                strData.Append("" + Utils.GetLabel(strScoreRangeTo) + "</span></td>");
            }
             
            if (rule != "" && rule != null)
            {
                strData.Append("<td><b>" + Utils.GetLabel("Rule") + "</b>:");
                strData.Append("" + Utils.GetLabel(rule) + "</td>");
            }
            strData.Append("</tr></table>");
            strData.Append("<br/>");
            strData.Append("<table style='width: 100%' align='center' cellspacing='0' cellpadding='0' border='1' align='center'>");
            if (dtbl!=null && dtbl.Rows.Count >0)
            {
                strData.Append("<tr>");
       
            strData.Append("<td style='width: 15%' align='center'>&nbsp;&nbsp;<b>" + Utils.GetLabel("District") + "</b></td><td align='center' style='width: 20%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("VDC/Municipality") + "</b></td><td align='center' style='width: 8%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Ward") + "</b></td><td align='center' style='width: 14%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Household ID") + "</b></td><td align='center' style='width: 20%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Head Name") + "</b></td><td align='center' style='width: 10%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Family Count") + "</b></td><td align='center' style='width: 10%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Poverty Rule") + "</b></td><td align='center' style='width: 10%'>");
            strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Poverty Score") + "</b></td>");
            strData.Append("</tr>");
            }
            if (dtbl != null && dtbl.Rows.Count != 0)
            {
                int CountDuplicateDist = 0;
                int RealCountDist = 0;
                int CountDuplicateVDC = 0;
                int RealCountVDC = 0;
                foreach (DataRow dRow in dtbl.Rows)
                {
                    if (CountDuplicateDist == 0)
                    {
                        DataRow[] countDuplicateValDist = dtbl.Select("DISTRICT_ENG='" + dRow["DISTRICT_ENG"].ConvertToString() + "'");
                        CountDuplicateDist = countDuplicateValDist.Length;
                        RealCountDist = CountDuplicateDist;
                        strData.Append("<tr><td rowspan='" + CountDuplicateDist + "' align='left' style='vertical-align:middle'>");
                        strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].ConvertToString() + "</td>");
                        if (CountDuplicateVDC == 0)
                        {
                            DataRow[] countDuplicateValVDC = dtbl.Select("VDC_ENG='" + dRow["VDC_ENG"].ConvertToString() + "'");
                            CountDuplicateVDC = countDuplicateValVDC.Length;
                            RealCountVDC = CountDuplicateVDC;
                            strData.Append("<td rowspan='" + CountDuplicateVDC + "' align='left' style='vertical-align:middle'> ");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ConvertToString() + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["DEFINED_CD"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp;" + dRow[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].ConvertToString() + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp;" + dRow["Member_Cnt"].ConvertToString() + "</td><td>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_FLAG"].ConvertToString()) + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_VALUE"].ConvertToString()) + "</td>");
                        }
                        if (CountDuplicateVDC != RealCountVDC)
                        {
                            strData.Append("<td align='right'>&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["DEFINED_CD"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].ConvertToString() + "</td><td align='right'>"); strData.Append("&nbsp;&nbsp;" + dRow["Member_Cnt"].ConvertToString() + "</td><td>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_FLAG"].ConvertToString()) + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_VALUE"].ConvertToString()) + "</td>");
                        }
                        strData.Append("</tr>");
                        CountDuplicateVDC = CountDuplicateVDC - 1;
                    }
                    if (CountDuplicateDist != RealCountDist)
                    {
                        strData.Append("<tr>");
                        if (CountDuplicateVDC == 0)
                        {
                            DataRow[] countDuplicateValVDC = dtbl.Select("VDC_ENG='" + dRow["VDC_ENG"].ConvertToString() + "'");
                            CountDuplicateVDC = countDuplicateValVDC.Length;
                            RealCountVDC = CountDuplicateVDC;
                            strData.Append("<td rowspan='" + CountDuplicateVDC + "' align='left' style='vertical-align:middle'>");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ConvertToString() + "");
                            strData.Append("</td><td align='right'>&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "");
                            strData.Append("</td><td align='left'>&nbsp;&nbsp; " + Utils.GetLabel(dRow["DEFINED_CD"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].ConvertToString() + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp;" + dRow["Member_Cnt"].ConvertToString() + "</td><td>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_FLAG"].ConvertToString()) + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_VALUE"].ConvertToString()) + "</td>");
                        }
                        if (CountDuplicateVDC != RealCountVDC)
                        {
                            strData.Append("<td align='right'>&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["DEFINED_CD"].ConvertToString()) + "</td><td align='left'>");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].ConvertToString() + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp;" + dRow["Member_Cnt"].ConvertToString() + "</td><td>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_FLAG"].ConvertToString()) + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["RULE_VALUE"].ConvertToString()) + "</td>");
                        }
                        strData.Append("</tr>");
                        CountDuplicateVDC = CountDuplicateVDC - 1;
                    }
                    CountDuplicateDist = CountDuplicateDist - 1;
                }
            }
            else
            {
                strData.Append("<tr><td height='24px' align='center' colspan='7'><b>&nbsp;" + (Utils.GetLabel("NO RECORD FOUND!")) + "</b></td></tr>");
            }
            strData.Append("</table>");
            return strData.ToString();
        }
        #endregion

        public DataTable FillSearchDataInChildAgeAveragePovertyScoreDetailsRpt(string region,string zone, string districtCd, string vdcMunCd, string ward, string area, string FamilyFrm, string FamilyTo, string HouseHldFrm, string HouseHldTo, string ScoreFrom, string ScoreTo,string rule)
        {
            DataTable dt = new DataTable();
            Object p_region_cd = DBNull.Value;
            Object p_Zone_cd = DBNull.Value;
            Object p_district_cd = DBNull.Value;
            Object p_vdc_mun_cd = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object P_area = DBNull.Value;
            Object p_size_from = DBNull.Value;
            Object p_size_to = DBNull.Value;
            Object p_household_id_frm = DBNull.Value;
            Object p_household_id_to = DBNull.Value;
            Object v_val_frm = DBNull.Value;
            Object v_val_to = DBNull.Value;
            Object v_Rule = DBNull.Value;
            if (region != "" && region != null)
            {
                p_region_cd = region;
            }
            if (zone != "" && zone != null)
            {
                p_Zone_cd = zone;
            }
            if (districtCd != "" && districtCd != null)
            {
                p_district_cd = districtCd;
            }
            if (vdcMunCd != "" && vdcMunCd != null)
            {
                p_vdc_mun_cd = vdcMunCd;
            }
            if (ward != "" && ward != null)
            {
                p_ward_no = ward;
            }
            if (area != "" && area != null)
            {
                P_area = area;
            }
            if (FamilyFrm != "" && FamilyFrm != null)
            {
                p_size_from = FamilyFrm;
            }
            if (FamilyTo != "" && FamilyTo != null)
            {
                p_size_to = FamilyTo;
            }
            if (HouseHldFrm != "" && HouseHldFrm != null)
            {
                p_household_id_frm = HouseHldFrm;
            }
            if (HouseHldTo != "" && HouseHldTo != null)
            {
                p_household_id_to = HouseHldTo;
            }
            if (ScoreFrom != "" && ScoreFrom != null)
            {
                v_val_frm = ScoreFrom;
            }
            if (ScoreTo != "" && ScoreTo != null)
            {
                v_val_to = ScoreTo;
            }

            if (rule != "" && rule != null)
            {
                v_Rule = rule;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                try
                {
                    service.PackageName = "PKG_MIS_REPORT";
                    dt = service.GetDataTable(true, "PR_MIS_POVERTY_CHILDAGE_DTL",
                        p_region_cd,
                        p_Zone_cd,
                        p_district_cd,
                        p_vdc_mun_cd,
                        p_ward_no,
                        P_area,
                        p_size_from,
                        p_size_to,
                        p_household_id_frm,
                        p_household_id_to,
                        v_val_frm,
                        v_val_to,
                        v_Rule,
                        Utils.ToggleLanguage("E","N"),
                        DBNull.Value
                        );
                    //SessionID = qr["P_SESSION_ID"].ConvertToString();
                }

                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
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
