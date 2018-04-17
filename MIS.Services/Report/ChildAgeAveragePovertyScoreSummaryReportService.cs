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
    public class ChildAgeAveragePovertyScoreSummaryReportService
    {
        #region Exporting
        public String getChildAgeAveragePovertyScoreSummaryData(DataTable dtbl, string region, string zone, string strDistrict, string strVdcMun, string strWard, string area, string strFamilyFrm, string strFamilyTo, string strScoreRangeFrm, string strScoreRangeTo, string strRule)
        {
            StringBuilder strData = new StringBuilder();
            int count = 0;
            int columncount = 0;
            if (dtbl != null && dtbl.Columns.Count > 0)
            {
                columncount = dtbl.Columns.Count;
            }
            string[] arr = new string[columncount];
            int totalCount = 0;
            int columnDataCount = 0;
            int colForTotal = 0;
            int grandTotal = 0;
            //Getting the Data for import
            strData.Append("<table width='140%' cellspacing='0' cellpadding='0' border='0' align='center'><tr>");
            if (region != null && region != "")
            {
                strData.Append("<b>&nbsp;&nbsp;" + Utils.GetLabel("Region/State") + "&nbsp;:&nbsp; &nbsp;</b>" + Utils.GetLabel(region.ConvertToString()));
            }
            if (zone != null && zone != "")
            {
                strData.Append("<b>&nbsp;&nbsp;" + Utils.GetLabel("Zone") + "&nbsp;:&nbsp; &nbsp;</b>" + Utils.GetLabel(zone.ConvertToString()));
            }
            if (strDistrict != "" && strDistrict != null)
            {
                strData.Append("<td align='left' colspan='8'><b>" + Utils.GetLabel("District") + "</b>:");
                strData.Append("" + strDistrict + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (strVdcMun != "" && strVdcMun != null)
            {
                strData.Append("<b>" + Utils.GetLabel("VDC/Municipality") + "</b>:");
                strData.Append("" + strVdcMun + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (strWard != "" && strWard != null)
            {
                strData.Append("<b>" + Utils.GetLabel("Ward") + "</b>:" + Utils.GetLabel(strWard) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (area != "" && area != null)
            {
                strData.Append("<b>" + Utils.GetLabel("Area") + "</b>:" + Utils.GetLabel(area) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (strFamilyFrm != "" && strFamilyFrm != null)
            {
                strData.Append("<b>" + Utils.GetLabel("Child Age Range From") + "</b>:");
                strData.Append("" + Utils.GetLabel(strFamilyFrm) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (strFamilyTo != "" && strFamilyTo != null)
            {
                strData.Append("<b>" + Utils.GetLabel("Child Age Range To") + "</b>:");
                strData.Append("" + Utils.GetLabel(strFamilyTo) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (strScoreRangeFrm != "" && strScoreRangeTo != "")
            {
                strData.Append("<td><span style='margin-left: 3px'><b>" + Utils.GetLabel("Score From") + "</b>:</span><span style='margin-left: 6px'>");
                strData.Append("" + Utils.GetLabel(strScoreRangeFrm) + "</span></td>");

                strData.Append("<td><span style='margin-left: 3px'><b>" + Utils.GetLabel("Score To") + "</b>:</span><span style='margin-left: 6px'>");
                strData.Append("" + Utils.GetLabel(strScoreRangeTo) + "</span></td>");
            }
           
            if (strRule != "" && strRule != null)
            {
                strData.Append("<b>" + Utils.GetLabel("Rule") + "</b>:");
                strData.Append("" + Utils.GetLabel(strRule) + "");
            }
            strData.Append("</td>");
            strData.Append("</tr></table>");
            strData.Append("<br/>");
            strData.Append("<table style='width: 100%' cellspacing='0' cellpadding='0' border='1' align='center'>");

            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                strData.Append("<tr>");
                strData.Append("<td align='center' style='width: 15%'>&nbsp;&nbsp;<b>" + Utils.GetLabel("District") + "</b></td><td align='center' style='width: 20%'>");
                strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("VDC/Municipality") + "</b></td><td align='center' style='width: 8%'>");
                strData.Append("&nbsp;&nbsp; <b>" + Utils.GetLabel("Ward") + "</b></td>");
                foreach (DataColumn c in dtbl.Columns)
                {
                    if (c.ColumnName.Contains("-"))
                    {
                        arr[count] = c.ColumnName.ConvertToString();
                        strData.Append("<td align='center' style='width: 8%'>&nbsp;&nbsp;<b>" + Utils.GetLabel(c.ColumnName.ConvertToString()) + "</b></td>");
                        count++;
                    }

                }
                strData.Append("<td align='center' style='width: 5%'>&nbsp;&nbsp; <b>" + Utils.GetLabel("Total") + "</b></td>");
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
                        strData.Append("<tr><td align='left' style='vertical-align:middle' rowspan='" + CountDuplicateDist + "'>");
                        strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].ConvertToString() + "</td>");
                        if (CountDuplicateVDC == 0)
                        {
                            DataRow[] countDuplicateValVDC = dtbl.Select("VDC_ENG='" + dRow["VDC_ENG"].ConvertToString() + "'");
                            CountDuplicateVDC = countDuplicateValVDC.Length;
                            RealCountVDC = CountDuplicateVDC;
                            strData.Append("<td align='left' style='vertical-align:middle' rowspan='" + CountDuplicateVDC + "'>");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ConvertToString() + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td>"); totalCount = 0;
                            for (int i = 0; i < count; i++)
                            {
                                strData.Append("<td align='right'>&nbsp;&nbsp; " + Utils.GetLabel(dRow[arr[i]].ConvertToString()) + "</td>");
                                totalCount = totalCount + (dRow[arr[i]]).ToInt32();
                            }
                            strData.Append("<td align='right'>" + Utils.GetLabel(totalCount.ConvertToString()) + " </td>");
                        }
                        if (CountDuplicateVDC != RealCountVDC)
                        {
                            strData.Append("<td>&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td>");
                            totalCount = 0;
                            for (int i = 0; i < count; i++)
                            {
                                strData.Append("<td align='right'>&nbsp;&nbsp; " + Utils.GetLabel(dRow[arr[i]].ConvertToString()) + "</td>");
                                totalCount = totalCount + (dRow[arr[i]]).ToInt32();
                            }
                            strData.Append("<td align='right'>" + Utils.GetLabel(totalCount.ConvertToString()) + " </td>");
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
                            strData.Append("<td align='left' style='vertical-align:middle' rowspan='" + CountDuplicateVDC + "'>");
                            strData.Append("&nbsp;&nbsp; " + dRow[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ConvertToString() + "</td><td align='right'>");
                            strData.Append("&nbsp;&nbsp; " + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td>"); totalCount = 0;
                            for (int i = 0; i < count; i++)
                            {
                                strData.Append("<td align='right'>&nbsp;&nbsp; " + Utils.GetLabel(dRow[arr[i]].ConvertToString()) + "");
                                strData.Append("</td>");
                                totalCount = totalCount + (dRow[arr[i]]).ToInt32();
                            }
                            strData.Append("<td align='right'>" + Utils.GetLabel(totalCount.ConvertToString()) + " </td>");
                        }
                        if (CountDuplicateVDC != RealCountVDC)
                        {
                            strData.Append("<td align='right'>&nbsp;&nbsp;" + Utils.GetLabel(dRow["PER_WARD_NO"].ConvertToString()) + "</td>");
                            totalCount = 0;
                            for (int i = 0; i < count; i++)
                            {
                                strData.Append("<td align='right'>&nbsp;&nbsp;" + Utils.GetLabel(dRow[arr[i]].ConvertToString()) + "</td>");
                                totalCount = totalCount + (dRow[arr[i]]).ToInt32();
                            }
                            strData.Append("<td align='right'>" + Utils.GetLabel(totalCount.ConvertToString()) + " </td>");
                        }
                        strData.Append("</tr>");
                        CountDuplicateVDC = CountDuplicateVDC - 1;
                    }
                    CountDuplicateDist = CountDuplicateDist - 1;
                }
                strData.Append("<tr><td align='left' colspan='3'>&nbsp;&nbsp;<b>" + Utils.GetLabel("Total") + "</b> </td>");
                foreach (DataColumn c in dtbl.Columns)
                {
                    columnDataCount = 0;
                    colForTotal = dtbl.Columns.IndexOf(c.ColumnName);
                    if (colForTotal > 4)
                    {


                        foreach (DataRow dr in dtbl.Rows)
                        {
                            if (colForTotal > 4)
                            {
                                columnDataCount = columnDataCount + dr[c.ColumnName].ToInt32();

                            }
                        }
                        strData.Append("<td align='right'>" + Utils.GetLabel(columnDataCount.ConvertToString()) + "</td>");
                        grandTotal = grandTotal + columnDataCount;

                    }

                }
                strData.Append("<td align='right'><b>" + Utils.GetLabel(grandTotal.ConvertToString()) + "</b></td></tr>");
            }
            else
            {
                strData.Append("<tr><td height='24px' align='center' colspan='0'><b>&nbsp;" + (Utils.GetLabel("NO RECORD FOUND!")) + "</b></td></tr>");
            }
            strData.Append("</table>");
            return strData.ToString();
        }
        #endregion

        public DataTable FillSearchDataInChildAgeAveragePovertyScoreSummaryRpt(string regioncd, string zonecd, string districtCd, string vdcMunCd, string ward, string area, string FamilyFrm, string FamilyTo, string Interval, string Rule, string ScoreFrom, string ScoreTo)
        {
            string lang = Utils.ToggleLanguage("E", "N");
            DataTable dt = new DataTable();
            Object p_region_cd = DBNull.Value;
            Object p_Zone_cd = DBNull.Value;
            Object p_district_cd = DBNull.Value;
            Object p_vdc_mun_cd = DBNull.Value;
            Object p_ward_no = DBNull.Value;
            Object P_area = DBNull.Value;
            Object p_size_from = DBNull.Value;
            Object p_size_to = DBNull.Value;
            Object p_interval = DBNull.Value;
            Object p_rul = DBNull.Value;
            Object v_val_frm = DBNull.Value;
            Object v_val_to = DBNull.Value;
            Object P_lang = DBNull.Value;
            if (regioncd != "" && regioncd != null)
            {
                p_region_cd = regioncd;
            }
            if (zonecd != "" && zonecd != null)
            {
                p_Zone_cd = zonecd;
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
            if (Interval != "" && Interval != null)
            {
                p_interval = Interval;
            }
            if (Rule != "" && Rule != null)
            {
                p_rul = Rule;
            }
            if (ScoreFrom != "" && ScoreFrom != null)
            {
                v_val_frm = ScoreFrom;
            }
            if (ScoreTo != "" && ScoreTo != null)
            {
                v_val_to = ScoreTo;
            }

            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                try
                {
                    service.PackageName = "PKG_MIS_REPORT";
                    dt = service.GetDataTable(true, "PR_MIS_POVERTY_CHILDAGE_SUM",
                        p_region_cd,
                        p_Zone_cd,
                        p_district_cd,
                        p_vdc_mun_cd,
                        p_ward_no,
                        P_area,
                        p_size_from,
                        p_size_to,
                        p_interval,
                        p_rul,
                        v_val_frm,
                        v_val_to,
                        P_lang,
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
