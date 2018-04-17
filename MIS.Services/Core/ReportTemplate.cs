using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ExceptionHandler;
using System.Web.Mvc;

namespace MIS.Services.Core
{
    public static class ReportTemplate
    {
        public static string GetReportTemplate(string strData, string reportHeader)
        {
            StringBuilder strTemplate = new StringBuilder();
            try
            {
                strTemplate.Append(@"<table cellspacing='0' cellpadding='0' border='0' align='center'  width='100%'>");
                strTemplate.Append(@"<tr><td style='height:30px;' align='left'></td></tr>");
                strTemplate.Append(@"<tr><td align='center'  style='font-size:16px;font-weight:bold;'>" + Utils.GetLabel("Government of Nepal").ToString() + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:18px;font-weight:bold;'>" + Utils.GetLabel("Ministry of Federal Affairs and Local Development") + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:14px;font-weight:bold;'>" + Utils.GetLabel("SinghDurbar,Kathmandu") + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:14px;font-weight:bold;'>&nbsp;</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:14px;font-weight:bold;'>" + reportHeader + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' height='30px'><div style='border:1px solid #000;'></div></td></tr>  ");
                strTemplate.Append(@"<tr><td>" + strData + "</td></tr>");
                strTemplate.Append(@"</table>");
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return strTemplate.ToString();

        }
        public static string ReportHeader(string rptName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<img src='~/Content/images/GovLogo.png' width='120px'/>"));
            builder.Append(string.Format("<div {0}>{1}</div>", "style='text-align:center;font-size:16px;font-weight:bold;color:#B22222'", Utils.GetLabel("Government of Nepal").ToString()));
            builder.Append(string.Format("<div {0}>{1}</div>", "style='text-align:center;font-size:18px;font-weight:bold;color:#B22222'", Utils.GetLabel("National Reconstruction Authority").ToString()));
            builder.Append(string.Format("<div {0}>{1}</div>", "style='text-align:center;font-size:14px;font-weight:bold;color:#B22222'", Utils.GetLabel("SinghDurbar,Kathmandu").ToString()));
            builder.Append(string.Format("<div {0}>{1}</div>", "style='text-align:center;font-size:14px;font-weight:bold'", Utils.GetLabel(rptName)));
            return builder.ToString();
        }
        //Sunil
        public static string GetReportHTML(string rptTemplate)
        {
            StringBuilder strHTML = new StringBuilder();
            try
            {
                strHTML.Append("<html>");
                strHTML.Append("<body>");
                if (rptTemplate != "")
                {
                    strHTML.Append(rptTemplate);
                }
                strHTML.Append("</body>");
                strHTML.Append("</html>");
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);

            }
            return strHTML.ToString();

        }

        public static string GetReportData(DataTable dtbl, List<string> reportTypeList, List<string> genderList, List<SelectListItem> optionalValues = null,string columnMain="Zone",bool genderDetail=true)
        {
            StringBuilder strData = new StringBuilder();
            int genderCount = 0;
            int grandTotal = 0;
            try
            {
                if (genderList != null && genderList.Count > 0)
                {
                    genderCount = genderList.Count;
                }
                //Getting the Data for import
                if (optionalValues != null)
                {
                    strData.Append("<table width='100%' cellspacing='0' cellpadding='0' border='0' align='center'>");
                    strData.Append("<tr><td colspan='10'>");
                    foreach (SelectListItem item in optionalValues)
                    {
                        if (item.Value != null && item.Value != "")
                        {
                            strData.Append("<b>" + Utils.GetLabel(item.Text) + "</b>&nbsp;");
                            strData.Append(":&nbsp;" + Utils.GetLabel(item.Value) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                        }
                    }
                    strData.Append("</td><td></td></tr></table>");
                }

                strData.Append("<table width='100%' cellspacing='0' cellpadding='0' border='1' align='center' class='searchBox'>");
                strData.Append("<tbody>");
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    
                    //Header Row
                    
                    strData.Append("<tr class='trTitle'>");
                    if (genderDetail == true)
                    {
                        strData.Append("<td rowspan='2'>");
                        strData.Append("<div style='width: 200px;'>");
                        strData.Append("<b>&nbsp;" + Utils.GetLabel(columnMain) + "</b></div>");
                        strData.Append("</td>");
                        if (reportTypeList != null && reportTypeList.Count > 0)
                        {
                            foreach (string reportType in reportTypeList)
                            {
                                strData.Append("<td align='center' colspan='" + genderCount.ToString() + "'>");
                                strData.Append("<div style='width: 300px;'>");
                                strData.Append("<b>" + reportType + "</b></div>");
                                strData.Append("</td>");
                            }
                            strData.Append("<td rowspan='2' align='center'><b>" + Utils.GetLabel("Total") + "</b></td>");
                        }
                        strData.Append("</tr>");
                        strData.Append("<tr class='trTitle'>");
                        if (reportTypeList != null && reportTypeList.Count > 0)
                        {
                            foreach (string reportType in reportTypeList)
                            {
                                if (genderList != null && genderList.Count > 0)
                                {
                                    foreach (string gender in genderList)
                                    {
                                        strData.Append("<td align='center'>");
                                        strData.Append(" <div style='min-width: 150px;'>");
                                        strData.Append("  <b>" + Utils.GetLabel(gender) + "</b></div>");
                                        strData.Append("</td>");
                                    }

                                }
                            }
                        }
                    }
                    else 
                    {

                        strData.Append("<td>");
                        strData.Append("<div style='width: 150px;'>");
                        strData.Append("<b>&nbsp;" + Utils.GetLabel(columnMain) + "</b></div>");
                        strData.Append("</td>");
                        if (reportTypeList != null && reportTypeList.Count > 0)
                        {
                            foreach (string reportType in reportTypeList)
                            {
                                strData.Append("<td align='center' colspan='" + genderCount.ToString() + "'>");
                                strData.Append("<div style='width: 150px;'>");
                                strData.Append("<b>" + reportType + "</b></div>");
                                strData.Append("</td>");
                            }
                            strData.Append("<td align='center'><b>" + Utils.GetLabel("Total") + "</b></td>");
                        }                    
                       
 
                    }
                    strData.Append("</tr>");
                    //End Hader Rows
                    //DataRows
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        grandTotal = 0;
                        strData.Append("<tr>");
                        if (dr["IsZone"].ToString().Trim() == "1")
                        {
                            strData.Append("<td height='24px'>");
                            strData.Append("&nbsp;<b>" + dr[columnMain].ToString() + "</b>");
                            strData.Append("</td>");
                        }
                        else
                        {
                            strData.Append("<td height='24px'>");
                            strData.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr[columnMain].ToString());
                            strData.Append("</td>");
                        }
                        if (reportTypeList != null && reportTypeList.Count > 0)
                        {
                            foreach (string reportType in reportTypeList)
                            {
                                if (genderList != null && genderList.Count > 0)
                                {
                                    foreach (string gender in genderList)
                                    {
                                        string columnname = reportType + '_' + gender;
                                        strData.Append("<td align='right'>");
                                        if (gender == "TOTAL")
                                        {
                                            grandTotal = grandTotal + dr[columnname].ToInt32();
                                        }
                                        strData.Append(" <div>" + Utils.GetLabel(dr[columnname].ConvertToString()) + "</div>");
                                        strData.Append("</td> ");
                                    }
                                }

                            }
                            strData.Append("<td align='right'>" + Utils.GetLabel((grandTotal).ConvertToString()) + "</td>");
                        }
                        strData.Append("</tr>");
                        //End DataRows
                    }

                }
                else
                {
                    //Data Not Found
                    strData.Append("<tr>");
                    strData.Append("<td height='24px' align='center'>");
                    strData.Append("<b>&nbsp;" + Utils.GetLabel("NO RECORD FOUND!") + "</b>");
                    strData.Append("</td>");
                    strData.Append("</tr>");
                }
                strData.Append("</tbody>");
                strData.Append("</table>");
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return strData.ToString();
        }
        public static string GetReportHTML(DataTable dtbl, List<string> reportTypeList, List<string> genderList, string reportHeader)
        {
            StringBuilder strHTML = new StringBuilder();
            strHTML.Append("<html>");
            strHTML.Append("<body>");
            strHTML.Append(GetReportTemplate(GetReportData(dtbl, reportTypeList, genderList), reportHeader));
            strHTML.Append("</body>");
            strHTML.Append("</html>");
            return strHTML.ToString();

        }
        public static string GetReportTemplateForBigger(string strData, string reportHeader)
        {
            StringBuilder strTemplate = new StringBuilder();
            try
            {
                strTemplate.Append(@"<table cellspacing='0' cellpadding='0' border='0' align='center'  width='200%'>");
                strTemplate.Append(@"<tr><td style='height:30px;' align='left'></td></tr>");
                strTemplate.Append(@"<tr><td align='center'  style='font-size:16px;font-weight:bold;'>" + Utils.GetLabel("Government of Nepal").ToString() + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:18px;font-weight:bold;'>" + Utils.GetLabel("Ministry of Federal Affairs and Local Development") + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:14px;font-weight:bold;'>" + Utils.GetLabel("Singh Durbar,Kathmandu") + "</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:14px;font-weight:bold;'>&nbsp;</td></tr>");
                strTemplate.Append(@"<tr><td align='center' style='font-size:14px;font-weight:bold;'>" + reportHeader + "</td></tr>");

                strTemplate.Append(@"<tr><td align='center' height='30px'><div style='border:1px solid #000;'></div></td></tr>  ");

                strTemplate.Append(@"<tr><td>" + strData + "</td></tr>");
                strTemplate.Append(@"</table>");
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return strTemplate.ToString();

        }

        public static string GetPovertyReportData(DataTable dtbl, List<string> reportTypeList, List<string> genderList)
        {
            StringBuilder strData = new StringBuilder();

            try
            {
                strData.Append("<table width='100%' cellspacing='0' cellpadding='0' border='1' align='center' class='searchBox'>");
                strData.Append("<tbody>");
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    //Header Row
                    strData.Append("<tr class='trTitle'>");
                    strData.Append("<td rowspan='2'>");
                    strData.Append("<div style='width: 200px;'>");
                    strData.Append("<b>&nbsp;" + Utils.GetLabel("Zone") + "</b></div>");
                    strData.Append("</td>");
                    if (reportTypeList != null && reportTypeList.Count > 0)
                    {
                        foreach (string reportType in reportTypeList)
                        {
                            strData.Append("<td align='center' colspan='2'>");
                            strData.Append("<div style='width: 300px;'>");
                            strData.Append("<b>[" + reportType + "]</b></div>");
                            strData.Append("</td>");
                        }

                    }
                    strData.Append("</tr>");
                    strData.Append("<tr class='trTitle'>");
                    strData.Append("</tr>");
                    //End Hader Rows
                    //DataRows
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        strData.Append("<tr>");
                        if (dr["IsZone"].ToString().Trim() == "1")
                        {
                            strData.Append("<td height='24px'>");
                            strData.Append("&nbsp;<b>" + dr["ZONE"].ToString() + "</b>");
                            strData.Append("</td>");
                        }
                        else
                        {
                            strData.Append("<td height='24px'>");
                            strData.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["ZONE"].ToString());
                            strData.Append("</td>");
                        }

                        if (reportTypeList != null && reportTypeList.Count > 0)
                        {
                            foreach (string caste in reportTypeList)
                            {

                                string columnname = caste;
                                strData.Append(" <td colspan='2' align='center'>");
                                strData.Append(dr[columnname]);
                                strData.Append("</td>");


                            }

                        }
                        strData.Append("</tr>");
                        //End DataRows
                    }

                }
                else
                {
                    //Data Not Found
                    strData.Append("<tr>");
                    strData.Append("<td height='24px' align='center'>");
                    strData.Append("<b>&nbsp;" + Utils.GetLabel("NO RECORD FOUND!") + "</b>");
                    strData.Append("</td>");
                    strData.Append("</tr>");
                }
                strData.Append("</tbody>");
                strData.Append("</table>");
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return strData.ToString();
        }

    }
}
