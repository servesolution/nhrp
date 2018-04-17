using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Services.Report;
using MIS.Services.Core;
using EntityFramework;
using MIS.Models.Report;
using System.IO;
using ClosedXML.Excel;

namespace MIS.Controllers.Report 
{

    public class SurveySummaryReportController : Controller
    {
        CommonFunction common = new CommonFunction();
        //
        // GET: /SurveySummaryReport/
        public DataTable dtbl = null;
        public ActionResult GetSurveySummaryReport()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();

            try
            {
                ViewBag.ReportTitle = "SurveySummary Report";
                ds = objReportService.getSTEPCCountForDashboard();
                dt = ds.Tables[0];
                drptParams.TOTAL_SURVEYED = Convert.ToInt32(dt.Compute("Sum(TOTAL_SURVEYED)", ""));
                drptParams.TARGETED = Convert.ToInt32(dt.Compute("Sum(TARGETED)", ""));
                drptParams.RETROFITTING_BENEF = Convert.ToInt32(dt.Compute("Sum(RETROFITTING_BENEF)", ""));
                drptParams.GRIEVANCE_BENEF = Convert.ToInt32(dt.Compute("Sum(GRIEVANCE_BENEF)", ""));
                drptParams.CASE_GRIEVANCE = Convert.ToInt32(dt.Compute("Sum(CASE_GRIEVANCE)", ""));
                drptParams.CASE_VERIFIED = Convert.ToInt32(dt.Compute("Sum(CASE_VERIFIED)", ""));



                decimal btotalnew = 0;
                decimal Sbtotalnew = 0;

                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {


                        btotalnew = btotalnew + Convert.ToDecimal(
                                        dr["TARGETED"].ToDecimal() + dr["RETROFITTING_BENEF"].ToDecimal() +
                                        Convert.ToDecimal(dr["GRIEVANCE_BENEF"].ToDecimal().ConvertToString() == ""
                                            ? "0"
                                            : dr["GRIEVANCE_BENEF"].ToDecimal().ConvertToString()));
                    }


                    drptParams.totalnew = Convert.ToInt32(btotalnew);


                    dt1 = ds.Tables[1];
                    if (dt1.Compute("Sum(TOTAL_SURVEYED)", "").ToString() != "")
                        drptParams.STOTAL_SURVEYED = Convert.ToInt32(dt1.Compute("Sum(TOTAL_SURVEYED)", ""));
                    if (dt1.Compute("Sum(TARGETED)", "").ToString() != "")
                        drptParams.STARGETED = Convert.ToInt32(dt1.Compute("Sum(TARGETED)", ""));
                    if (dt1.Compute("Sum(RETROFITTING_BENEF)", "").ToString() != "")
                        drptParams.SRETROFITTING_BENEF = Convert.ToInt32(dt1.Compute("Sum(RETROFITTING_BENEF)", ""));
                    if (dt1.Compute("Sum(GRIEVANCE_BENEF)", "").ToString() != "")
                        drptParams.SGRIEVANCE_BENEF = Convert.ToInt32(dt1.Compute("Sum(GRIEVANCE_BENEF)", ""));
                    if (dt1.Compute("Sum(CASE_GRIEVANCE)", "").ToString() != "")
                        drptParams.SCASE_GRIEVANCE = Convert.ToInt32(dt1.Compute("Sum(CASE_GRIEVANCE)", ""));
                    if (dt1.Compute("Sum(CASE_VERIFIED)", "").ToString() != "")
                        drptParams.SCASE_VERIFIED = Convert.ToInt32(dt1.Compute("Sum(CASE_VERIFIED)", ""));



                   



                    foreach (DataRow dr1 in ds.Tables[1].Rows)
                    {


                        Sbtotalnew = Sbtotalnew + Convert.ToDecimal(
                                         dr1["TARGETED"].ToDecimal() + dr1["RETROFITTING_BENEF"].ToDecimal() +
                                         Convert.ToDecimal(dr1["GRIEVANCE_BENEF"].ToDecimal().ConvertToString() == ""
                                             ? "0"
                                             : dr1["GRIEVANCE_BENEF"].ToDecimal().ConvertToString()));
                    }
                }

                drptParams.Stotalnew = Convert.ToInt32(Sbtotalnew);




                //ds.Tables[0].Columns.Add("Total_Benef");
                //DataRow dr = ds.Tables[0].NewRow();
                //dr["Total_Benef"] = dr["TARGETED"].ConvertToString() + dr["RETROFITTING_BENEF"].ToDecimal() +dr["Grievance_benef"].ToDecimal();
                ViewData["dtbl"] = ds;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_STEPCReport", drptParams);

        }

        public ActionResult ProjectHeighlightExportReport()
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = ProjectHeighlightExportDataTable();

            return ExportData(ds);

        }
        public DataSet ProjectHeighlightExportDataTable()
        {

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();

            try
            {
                ViewBag.ReportTitle = "SurveySummary Report";
                ds = objReportService.getSTEPCCountForDashboard();
                dt = ds.Tables[0];



                ViewData["dtbl"] = ds;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return ds;


        }
        public ActionResult ExportData(DataSet ds)
        {

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = ds.Tables[0];
            dt2 = ds.Tables[1];
            using (XLWorkbook wb = new XLWorkbook())
            {

                IXLWorksheet worksheet = wb.AddWorksheet(dt1);
                IXLWorksheet worksheet1 = wb.AddWorksheet(dt2);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= ProjectHeighLightReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

           
            return RedirectToAction("Index", "ExportData");
        }

        public ActionResult GetTrainingSummaryReport()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {
                ViewBag.ReportTitle = "Training Summary Report";
                ds = objReportService.GetTrainingReport();
                dt = ds.Tables[0];
                decimal btotalnew = 0;


                drptParams.totalnew = Convert.ToInt32(btotalnew);

                ViewData["dtbl"] = ds;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_trainingreport", drptParams);

        }
        public ActionResult GetDonorSupportReport()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {
                ViewBag.ReportTitle = "Donor Support Report";
                ds = objReportService.GetDonorSupportDetail();
                dt = ds.Tables[0];
                ViewData["dtbl"] = ds;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_DonorSupportReport", drptParams);

        }

        public ActionResult GetPaymentReport()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {
                ViewBag.ReportTitle = "Payment Report";
                dt = objReportService.GetPaymentSumReport();
                ViewData["dtbl"] = dt;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_PaymentReport", drptParams);

        }

        public ActionResult GetGrantReportTbl()
        {
            DataTable dt = new DataTable();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {
                ViewBag.ReportTitle = "Grant Distribution Report";
                dt = objReportService.getDTCOGranctSummaryReporTbl();
                ViewData["dtbl"] = dt;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_GrantDistributionReport", drptParams);

        }



        public ActionResult GetGrievanceHandlingSummaryReport()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();

            try
            {
                ViewBag.ReportTitle = "GrievanceSummary Report";
                ds = objReportService.GetGrievanceForReport();
                dt = ds.Tables[0];
                //drptParams.TOTAL_SURVEYED = Convert.ToInt32(dt.Compute("Sum(TOTAL_SURVEYED)", ""));
                drptParams.RE_SURVEY = Convert.ToInt32(dt.Compute("Sum(RE_SURVEY)", ""));
                drptParams.FIELD_OBS = Convert.ToInt32(dt.Compute("Sum(FIELD_OBS)", ""));
                drptParams.NON_BENEFECIARY = Convert.ToInt32(dt.Compute("Sum(NON_BENEFECIARY)", ""));
                drptParams.BENEFICIARY = Convert.ToInt32(dt.Compute("Sum(BENEFICIARY)", ""));
                //drptParams.CASE_VERIFIED = Convert.ToInt32(dt.Compute("Sum(CASE_VERIFIED)", ""));



                decimal btotalnew = 0;



                foreach (DataRow dr in ds.Tables[0].Rows)
                {


                    btotalnew = btotalnew + Convert.ToDecimal(dr["RE_SURVEY"].ToDecimal() + dr["FIELD_OBS"].ToDecimal() + dr["NON_BENEFECIARY"].ToDecimal() + dr["BENEFICIARY"].ToDecimal());
                }


                drptParams.totalnew = Convert.ToInt32(btotalnew);


                //dt1 = ds.Tables[1];
                //if (dt1.Compute("Sum(TOTAL_SURVEYED)", "").ToString()!="")
                //drptParams.STOTAL_SURVEYED = Convert.ToInt32(dt1.Compute("Sum(TOTAL_SURVEYED)", ""));
                //if (dt1.Compute("Sum(TARGETED)", "").ToString() != "")
                //drptParams.STARGETED = Convert.ToInt32(dt1.Compute("Sum(TARGETED)", ""));
                //if (dt1.Compute("Sum(RETROFITTING_BENEF)", "").ToString() != "")
                //drptParams.SRETROFITTING_BENEF = Convert.ToInt32(dt1.Compute("Sum(RETROFITTING_BENEF)", ""));
                //if (dt1.Compute("Sum(GRIEVANCE_BENEF)", "").ToString() != "")
                //drptParams.SGRIEVANCE_BENEF = Convert.ToInt32(dt1.Compute("Sum(GRIEVANCE_BENEF)", ""));
                //if (dt1.Compute("Sum(CASE_GRIEVANCE)", "").ToString() != "")
                //drptParams.SCASE_GRIEVANCE = Convert.ToInt32(dt1.Compute("Sum(CASE_GRIEVANCE)", ""));
                //if (dt1.Compute("Sum(CASE_VERIFIED)", "").ToString() != "")
                //drptParams.SCASE_VERIFIED = Convert.ToInt32(dt1.Compute("Sum(CASE_VERIFIED)", ""));



                //decimal Sbtotalnew = 0;



                //foreach (DataRow dr1 in ds.Tables[1].Rows)
                //{


                //    Sbtotalnew = Sbtotalnew + Convert.ToDecimal(dr1["TARGETED"].ToDecimal() + dr1["RETROFITTING_BENEF"].ToDecimal() + Convert.ToDecimal(dr1["GRIEVANCE_BENEF"].ToDecimal().ConvertToString() == "" ? "0" : dr1["GRIEVANCE_BENEF"].ToDecimal().ConvertToString()));
                //}


                //drptParams.Stotalnew = Convert.ToInt32(Sbtotalnew);




                //ds.Tables[0].Columns.Add("Total_Benef");
                //DataRow dr = ds.Tables[0].NewRow();
                //dr["Total_Benef"] = dr["TARGETED"].ConvertToString() + dr["RETROFITTING_BENEF"].ToDecimal() +dr["Grievance_benef"].ToDecimal();
                ViewData["dtbl"] = ds;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_GrievanceHandlingSummaryReport", drptParams);

        }

        public ActionResult GrievanceHandlingExportReport()
        {

            DataTable dt = new DataTable();

            dt = GrievanceHandlingExportDataTable();

            return ExportData(dt);

        }
        public DataTable GrievanceHandlingExportDataTable()
        {

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            HtmlReport drptParams = new HtmlReport();
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();

            try
            {
                ViewBag.ReportTitle = "Grievance Handling Report";
                ds = objReportService.GetGrievanceForReport();
                dt = ds.Tables[0];
              


                ViewData["dtbl"] = ds;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
           
            return dt;


        }
        public ActionResult ExportData(DataTable dt)
        {

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = dt;

            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Worksheets.Add(dt);
                //wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //wb.Style.Font.Bold = true;

                IXLWorksheet worksheet = wb.AddWorksheet(dt1);


                //var range = worksheet.Range("A1", "AB4").Merge();
                //range.Value = string.Format("GOVERNMENT OF NEPAL {0} NATIONAL RECONSTRUCTION AUTHORITY {0} EARTHQUAKE HOUSING RECONSTRUCTION PROGRAME", Environment.NewLine);
                //range.Style.Alignment.WrapText = true;
                //range.Style.Font.Bold = true;
                //range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);



                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GrievanceHandlingReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }

        public string GetSummaryDetailReportGraph(string cType, string rptType, string isHH, string fName)
        {
            DataSet ds = new DataSet();
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {

                ViewBag.ReportTitle = "HouseDetail Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                ds = objReportService.getSTEPCCountForDashboard();
                xmlString = dtbl.GetBarDataChartXml(ChartType.Pie, searchType, "Count", "300", "");



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {


                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);
                }
            }
            else
            {
                return "false";
            }
            return "true";

        }

        public string GetBeneficiaryGraphReport(string cType, string rptType, string isHH, string fName)
        {
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {

                ViewBag.ReportTitle = "Beneficiary Graph Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.getBenefSummaryReport();
                xmlString = dtbl.GetBarDataChartXml(ChartType.Bar2D, @Utils.GetLabel("District"), "No of Beneficiaries", "300", "");



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {


                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);
                }
            }
            else
            {
                return "false";
            }
            return "true";

        }
        public string GetGrievanceSummaryReport(string cType, string rptType, string isHH, string fName)
        {
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {

                ViewBag.ReportTitle = "Grievance Graph Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.getGrievanceSummaryReport();
                xmlString = dtbl.GetBarDataChartXml(ChartType.Bar2D, @Utils.GetLabel("District"), "No of Grievances", "300", "");



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {


                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);
                }
            }
            else
            {
                return "false";
            }
            return "true";

        }

        public string GetGrantDistributionRpt(string cType, string fName)
        {
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            string xmlBarString = "";
            var dtToReturn = new DataTable();

            DataRow workRow =  dtToReturn.NewRow(); 
            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {

                ViewBag.ReportTitle = "Grant Distribution Graph Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.getDTCOGranctSummaryReport();



                xmlBarString = dtbl.GetMultiSeriesBarChartXml("Total Installment ", "Grant Distribution", "Date", "No. of Beneficiaries");

                decimal fistIsnt = 0;
                decimal secondtIsnt = 0;
                decimal thirdIsnt = 0;
                foreach (DataRow item in dtbl.Rows)
                {
                    fistIsnt += Convert.ToDecimal(item[1]);
                    secondtIsnt += Convert.ToDecimal(item[2]);
                    thirdIsnt += Convert.ToDecimal(item[3]);
                }
                for (int i = 0; i <= 3; i++)
                {
                    if (i == 0)
                    {
                        dtToReturn.Columns.Add(new DataColumn());
                        workRow[i] = fistIsnt.ConvertToString();
                    }
                        if (i == 1)
                        {
                            dtToReturn.Columns.Add(new DataColumn());
                            workRow[i] = secondtIsnt.ConvertToString();
                        }
                        if (i == 2)
                        {
                            dtToReturn.Columns.Add(new DataColumn());
                            workRow[i] = thirdIsnt.ConvertToString();
                        }
                   
                }
                dtToReturn.Rows.Add(workRow);

                ViewData["installments"] = Newtonsoft.Json.JsonConvert.SerializeObject(dtToReturn);

                var newDt2 = objReportService.getDTCOGranctDetailReport(dtbl.Rows[0][0].ConvertToString());


                xmlString = newDt2.GetMultiLineChartXml("Total Installment", "Grant Distribution", "Installments", "0");

                

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {
                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    fName = fName + "Bar";
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName , xmlBarString, utf8WithoutBom);

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);
                    var splitedFName = fName.Split('.');
                    fName = splitedFName[0] + "Bar." + splitedFName[1];

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlBarString);
                }
            }

            //return dtToReturn;
            else
            {
                return "false";
            }
            return "true";
        }

        public string GetGrantDistributionDetailRpt(string cType, string fName, string year)
        {
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            string xmlBarString = "";
            
            

            STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
            try
            {

                ViewBag.ReportTitle = "Grant Distribution Detail Graph Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.getDTCOGranctDetailReport(year);
               
            

                xmlString = dtbl.GetMultiLineChartXml("Total Installment", "Grant Distribution", year, "0");
               

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {
                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);
                   

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);                   
                }
            }

            else
            {
                return "false";
            }
            return "true";

        }
        public ActionResult DetailReportByCaste()
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            return View();
        }
        public ActionResult SummaryReportByCaste()
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            return View();
        }
        public ActionResult SurveyDetailReportByEducation()
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            return View();
        }
        public ActionResult SurveySummaryReportByEducation()
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            return View();
        }
        public ActionResult GetCasteWiseSurveyDetail(string dist, string vdc, string ward, string caste, string maritalstatus, string gender)
        {

            SurveyHTMLReportServices objSurveyservice = new SurveyHTMLReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (caste.ConvertToString() != string.Empty)
                paramValues.Add("caste", caste);
            if (maritalstatus.ConvertToString() != string.Empty)
                paramValues.Add("maritalstatus", maritalstatus);
            if (gender.ConvertToString() != string.Empty)
                paramValues.Add("gender", gender);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            dt = objSurveyservice.SurveyReportByCaste(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("DetailReportByCaste");
        }
        public ActionResult GetCasteWiseSurveySummaryReport(string dist, string vdc, string ward, string caste, string maritalstatus, string gender)
        {

            SurveyHTMLReportServices objSurveyservice = new SurveyHTMLReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (caste.ConvertToString() != string.Empty)
                paramValues.Add("caste", caste);
            if (maritalstatus.ConvertToString() != string.Empty)
                paramValues.Add("maritalstatus", maritalstatus);
            if (gender.ConvertToString() != string.Empty)
                paramValues.Add("gender", gender);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            dt = objSurveyservice.SurveySummaryReportByCaste(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("SummaryReportByCaste");
        }
        public ActionResult GetSurveyDetailByEducation(string dist, string vdc, string ward, string literatechk, string educationcd, string gender)
        {

            SurveyHTMLReportServices objSurveyservice = new SurveyHTMLReportServices();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (literatechk.ConvertToString() != string.Empty)
                paramValues.Add("literatechk", literatechk);
            if (educationcd.ConvertToString() != string.Empty)
                paramValues.Add("educationcd", common.GetCodeFromDataBase(educationcd, "MIS_CLASS_TYPE", "CLASS_TYPE_CD"));
            if (gender.ConvertToString() != string.Empty)
                paramValues.Add("gender", gender);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            dt = objSurveyservice.SurveyDetailReportByEducation(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;


            return RedirectToAction("SurveyDetailReportByEducation");
        }
        public ActionResult GetSurveySummaryByEducation(string dist, string vdc, string ward, string literatechk, string educationcd, string gender)
        {

            SurveyHTMLReportServices objSurveyservice = new SurveyHTMLReportServices();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (literatechk.ConvertToString() != string.Empty)
                paramValues.Add("literatechk", literatechk);
            if (educationcd.ConvertToString() != string.Empty)
                paramValues.Add("educationcd", common.GetCodeFromDataBase(educationcd, "MIS_CLASS_TYPE", "CLASS_TYPE_CD"));
            if (gender.ConvertToString() != string.Empty)
                paramValues.Add("gender", gender);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }


            if (literatechk == "Y" && educationcd != "")//&& educationcd=="")
            {
                DataTable dt = new DataTable();
                dt = objSurveyservice.SurveySummaryReportByEducation(paramValues);
                ViewData["SummaryResults"] = dt;
                Session["SummaryResults"] = dt;
            }
            else if (literatechk == "N" && educationcd == "")
            {
                DataTable dt = new DataTable();
                dt = objSurveyservice.SurveySumReportByNonEducation(paramValues);
                ViewData["SummaryResults"] = dt;
                Session["SummaryResults"] = dt;
            }
            return RedirectToAction("SurveySummaryReportByEducation");
        }
        [HttpPost]
        public ActionResult ExportSurveyDetailByCaste()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "SurveyCasteReport" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "SurveyCasteReport.xls";
            }
            string html = RenderPartialViewToString("~/Views/HtmlReport/_SurveyCasteWiseHTMLReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Survey Detail Report By Caste "), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "SurveyCasteReport.xls");
        }
        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                ViewData["SummaryResults"] = dt;
                rptParams = (HtmlReport)Session["SurveyReportDetailParams"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    ViewData.Model = rptParams;
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }
        [HttpPost]
        public ActionResult ExportSurveyDetailByEducation()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "SurveyEducationReport" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "SurveyEducationReport.xls";
            }
            string html = RenderPartialEduViewToString("~/Views/HtmlReport/_SurveyDetailHTMLReportByEducation.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Survey Detail Report By Education "), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "SurveyEducationReport.xls");
        }
        protected string RenderPartialEduViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                ViewData["SummaryResults"] = dt;
                rptParams = (HtmlReport)Session["SurveyReportDetailParams"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    ViewData.Model = rptParams;
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }
    }
}
