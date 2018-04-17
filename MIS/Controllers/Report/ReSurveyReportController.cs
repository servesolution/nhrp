using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Report;
using MIS.Services.ResurveyView;
using System.IO;
using ClosedXML.Excel;

namespace MIS.Controllers.Report
{
    public class ReSurveyReportController : Controller
    {
        //
        // GET: /ReSurveyReport/
        CommonFunction common = new CommonFunction();

        //Resurvey Reconstruction Beneficiary 
        #region

        //Summary Report
        public ActionResult GetReconstructionBeneficiarySummaryReport(string dist, string currentVdc, string currentWard)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetReconstructionBeneficiarySummaryReport(paramValues);
            ViewData["ResultsReconstructionBeneficiary"] = dt;
            Session["ResultsReconstructionBeneficiary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("ReconstructionBeneficiarySummaryReport", objreport);
        }
        public ActionResult ReconstructionBeneficiarySummaryReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsReconstructionBeneficiary"];
            ViewData["ResultsReconstructionBeneficiary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Reconstruction Beneficiary Summary Report";
            return View(obj);
        }

        //Detail Report
        public ActionResult GetReconstructionBeneficiaryDetailReport(string dist, string currentVdc, string currentWard, string isexport)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (isexport.ConvertToString() != string.Empty)
                paramValues.Add("isexport", isexport);

            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetReconstructionBeneficiaryDetailReport(paramValues);
            ViewData["ResultsDetailReconstructionBeneficiary"] = dt;
            Session["ResultsDetailReconstructionBeneficiary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("ReconstructionBeneficiaryDetailReport", objreport);
        }
        public ActionResult ReconstructionBeneficiaryDetailReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsDetailReconstructionBeneficiary"];
            ViewData["ResultsDetailReconstructionBeneficiary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Reconstruction Beneficiary Detail Report";
            return View(obj);
        }

        //Export Resurvey Reconstruction Beneficiary Report
        public ActionResult ExportResurveyReconstructionBeneficiaryReport(string dist, string currentVdc, string currentWard, string isexport)
        {
            GetReconstructionBeneficiaryDetailReport(dist, currentVdc, currentWard, isexport);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Reconstruction Beneficiary report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Reconstruction Beneficiary report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforgradewdar("~/Views/ReSurveyReport/_ReconstructionBeneficiaryDetailReport.cshtml");
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "ReSurveyReconstructionBeneficiary.xls");


        }

        protected string RenderPartialViewToStringforgradewdar(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            if ((DataTable)Session["ResultsDetailReconstructionBeneficiary"] != null)
            {
                DataTable dt = (DataTable)Session["ResultsDetailReconstructionBeneficiary"];

                ViewData["ResultsReSurveyDetail"] = dt;
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
        #endregion


        //Resurvey Retrofitting Beneficiary
        #region
       
        //Summary Report
        public ActionResult GetRetrofittingBeneficiarySummaryReport(string dist, string currentVdc, string currentWard)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetRetrofittingBeneficiarySummaryReport(paramValues);
            ViewData["ResultsRetrofittingBeneficiary"] = dt;
            Session["ResultsRetrofittingBeneficiary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("RetrofittingBeneficiarySummaryReport", objreport);
        }
        public ActionResult RetrofittingBeneficiarySummaryReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsRetrofittingBeneficiary"];
            ViewData["ResultsRetrofittingBeneficiary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Retrofitting Beneficiary Summary Report";
            return View(obj);
        }
        //Detail Report
        public ActionResult GetRetrofittingBeneficiaryDetailReport(string dist, string currentVdc, string currentWard, string isexport)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (isexport.ConvertToString() != string.Empty)
                paramValues.Add("isexport", isexport);

            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetRetrofittingBeneficiaryDetailReport(paramValues);
            ViewData["ResultsDetailRetrofittingBeneficiary"] = dt;
            Session["ResultsDetailRetrofittingBeneficiary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("RetrofittingBeneficiaryDetailReport", objreport);
        }
        public ActionResult RetrofittingBeneficiaryDetailReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsDetailRetrofittingBeneficiary"];
            ViewData["ResultsDetailRetrofittingBeneficiary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Reconstruction Beneficiary Detail Report";
            return View(obj);
        }
        //Export Report
        public ActionResult ExportResurveyRetrofittingBeneficiaryReport(string dist, string currentVdc, string currentWard, string isexport)
        {
            GetRetrofittingBeneficiaryDetailReport(dist, currentVdc, currentWard, isexport);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Retrofitting Beneficiary report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Retrofitting Beneficiary report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforRetroBenef("~/Views/ReSurveyReport/_RetrofittingBeneficiaryDetailReport.cshtml");
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "ReSurveyRetrofittingBeneficiaryReport.xls");


        }

        protected string RenderPartialViewToStringforRetroBenef(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            if ((DataTable)Session["ResultsDetailRetrofittingBeneficiary"] != null)
            {
                DataTable dt = (DataTable)Session["ResultsDetailRetrofittingBeneficiary"];

                ViewData["ResultsReSurveyRetroBeneficiary"] = dt;
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
        #endregion


        //Resurvey Non-Beneficiary
        #region
        
        //Summary Report
        public ActionResult GetNonBeneficiarySummaryReport(string dist, string currentVdc, string currentWard)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetResurveyNonBeneficiarySummaryReport(paramValues);
            ViewData["ResultsNonBeneficiary"] = dt;
            Session["ResultsNonBeneficiary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("NonBeneficiarySummaryReport", objreport);
        }
        public ActionResult NonBeneficiarySummaryReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsNonBeneficiary"];
            ViewData["ResultsNonBeneficiary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Non Beneficiary Summary Report";
            return View(obj);
        }
        //Detail Report
        public ActionResult GetNonBeneficiaryDetailReport(string dist, string currentVdc, string currentWard, string isexport)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (isexport.ConvertToString() != string.Empty)
                paramValues.Add("isexport", isexport);

            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetResurveyNonBeneficiaryDetailReport(paramValues);
            ViewData["ResultsDetailNonBeneficiary"] = dt;
            Session["ResultsDetailNonBeneficiary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("NonBeneficiaryDetailReport", objreport);
        }
        public ActionResult NonBeneficiaryDetailReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsDetailNonBeneficiary"];
            ViewData["ResultsDetailNonBeneficiary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Non Beneficiary Detail Report";
            return View(obj);
        }

        //Export Non-Beneficiary Report
        public ActionResult ExportResurveyNonBeneficiaryReport(string dist, string currentVdc, string currentWard, string isexport)
        {
            GetNonBeneficiaryDetailReport(dist, currentVdc, currentWard, isexport);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Non-Beneficiary report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Non-Beneficiary report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforNonBenef("~/Views/ReSurveyReport/_NonBeneficiaryDetailReport.cshtml");
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "ReSurveyNonBeneficiaryReport.xls");


        }

        protected string RenderPartialViewToStringforNonBenef(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            if ((DataTable)Session["ResultsDetailNonBeneficiary"] != null)
            {
                DataTable dt = (DataTable)Session["ResultsDetailNonBeneficiary"];

                ViewData["ResultsReSurveyNonBeneficiary"] = dt;
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
        #endregion

        //Combined ReSurvey Beneficiary Detail Data
        #region
        //Summary Report
        public ActionResult GetResurveySummaryData(string dist, string currentVdc, string currentWard)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);


            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            dt = objService.GetAllResurveySummaryData(paramValues);
            ViewData["ResultsResurveySummaryData"] = dt;
            Session["ResultsResurveySummaryData"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("ResurveyBeneficiarySummaryData", objreport);
        }
        public ActionResult ResurveyBeneficiarySummaryData(HtmlReport obj)
        {
           
            DataTable dt = null;
            dt = (DataTable)Session["ResultsResurveySummaryData"];
            ViewData["ResultsResurveySummaryData"] = dt;
            ViewBag.ReportTitle = "SUMMARY REPORT (RS/RV)";
            return View(obj);
        }
        //Detail Report
        public ActionResult GetResurveyDetailData(string dist, string currentVdc, string currentWard, string RecommendationType, string RSRVtype, string isexport)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
            if (RSRVtype.ConvertToString() != string.Empty)
                paramValues.Add("RSRVtype", RSRVtype);

            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            


            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            objreport.RecommendType = RecommendationType;
            objreport.RSRVType = RSRVtype;
            dt = objService.GetAllResurveyDetailData(paramValues);
            ViewData["ResultsReSurveyDetailBenef"] = dt;
            Session["ResultsReSurveyDetailBenef"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("ResurveyBeneficiaryDetailData", objreport);
        }
        public ActionResult ResurveyBeneficiaryDetailData(HtmlReport obj)
        {
            string _nam = "";
            string _ReComdtype = "";
            if (obj.RSRVType == "RS") 
            {
                _nam = "Re-Survey";
            }
            if (obj.RSRVType == "RV")  
            {
                _nam = "Re-Verification";
            }

            if (obj.RecommendType == "RCB")
            {
                _ReComdtype = "Reconstruction Beneficiary";
            }
            if (obj.RecommendType == "RTB")
            {
                _ReComdtype = "Retrofitting Beneficiary";
            }
            if (obj.RecommendType == "NB")
            {
                _ReComdtype = "Non-Beneficiary";
            }

            DataTable dt = null;
            dt = (DataTable)Session["ResultsReSurveyDetailBenef"];
            ViewData["ResultsReSurveyDetailBenef"] = dt;
            ViewBag.ReportTitle = _nam + " " +_ReComdtype + " " +"Detail Data";
            return View(obj);
        }

        //Export ReSurvey Detail Report
        public ActionResult ExportResurveyDetailData(string dist, string currentVdc, string currentWard, string RecommendationType, string RSRVtype, string isexport)
        {
            GetResurveyDetailData(dist, currentVdc, currentWard, RecommendationType, RSRVtype, isexport);

            DataTable dt = new DataTable();
            dt = (DataTable)Session["ResultsReSurveyDetailBenef"];
            dt.Columns.Remove("GRIVIENT_NAME");
            dt.Columns.Remove("DISTRICT_CD");
            dt.Columns.Remove("VDC_OLD");
            dt.Columns.Remove("VDC_MUN_CD");
            dt.Columns.Remove("REMARKS");
            dt.Columns.Remove("RESN_FOR_GRIEV");
            dt.Columns.Remove("ENUMERATION_AREA");
            dt.Columns.Remove("GENDER");
            dt.Columns.Remove("TECHNICAL_SOLN");
            dt.Columns.Remove("DAMAGE_GRADE");
            dt.Columns.Remove("TOLE");
            dt.Columns.Remove("WARD_OLD");
            dt.Columns.Remove("VDC");


            dt.Columns["REGISTERED_NEW"].ColumnName = "Type(Registerd/New)";
            dt.Columns["GRIVIENT_NAME_NOT_NULL"].ColumnName = "Grievant Name";
            dt.Columns["NEW_PA"].ColumnName = "PA Number";
            dt.Columns["DAMAGE_GRADE_PREVIOUS"].ColumnName = "SDG(L)";
            dt.Columns["C_HIOP_COND"].ColumnName = "HIOP_COND";

            DataView dv = new DataView(dt);
            DataTable dt1 = dv.ToTable(true, "Type(Registerd/New)", "RS_RV", "GID", "Grievant Name", "HO_COUNT","HOUSE_OWNER_NAME", 
                "PA Number", "DISTRICT","RURAL_URBAN","WARD_NO", "SLIP_NO","HOUSE_OWNER_ID","HH_SN","SC(P)", "sdg_sts(P)",
                "SDG(L)", "SC", "SDG_STS", "SDG_STS_L", "HIOP_ADD(P)", "HIOP_COND(P)", "HIOP_COND","CARD_TYPE", "CARD_NO", "MOBILE_NO", 
                "Recommendation(Previous)", "Clarification(Previous)", "RECOMANDATION", "OTHER_OWNER");

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt1);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= RS_RV Report.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("ReportHierarchy", "Report");
        }

        protected string RenderPartialViewReSurveyDetaildata(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            if ((DataTable)Session["ResultsReSurveyDetailBenef"] != null)
            {
                DataTable dt = (DataTable)Session["ResultsReSurveyDetailBenef"];

                ViewData["ResultsReSurveyDetailBenef"] = dt;
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

        #endregion

        //GetEngVerificationSummaryReport Report
        public ActionResult GetEngVerificationSummaryReport(string dist, string currentVdc, string currentWard, string RecommendationType)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
          

            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            objreport.RecommendType = RecommendationType;

            dt = objService.GetEngVerificationData(paramValues);
            ViewData["ResultsEngVerSummaryData"] = dt;
            Session["ResultsEngVerSummaryData"] = dt;
           
            return RedirectToAction("EngVerificationSummaryReport", objreport);
        }
        public ActionResult EngVerificationSummaryReport(HtmlReport obj)
        {
           
            DataTable dt = null;
            dt = (DataTable)Session["ResultsEngVerSummaryData"];
            ViewData["ResultsEngVerSummaryData"] = dt;
            ViewBag.ReportTitle = "ENGINEER VERIFICATION SUMMARY REPORT";
            return View(obj);
        }
    }
}
