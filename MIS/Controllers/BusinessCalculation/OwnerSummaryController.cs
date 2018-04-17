using EntityFramework;
using MIS.Models;
using MIS.Models.AuditTrail;
using MIS.Models.Report;
using MIS.Models.Security;
using MIS.Services.AuditTrail;
using MIS.Services.BusinessCalculation;
using MIS.Services.Core;
using MIS.Services.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MIS.Controllers.BusinessCalculation
{
    public class OwnerSummaryController : Controller
    {
        CommonFunction common = new CommonFunction();

        //
        // GET: /OwnerSummary/

        public ActionResult GetSummaryReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (GrievanceType.ConvertToString() != string.Empty)
                paramValues.Add("CaseType", GrievanceType);
            if (OtherHouse.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouse", OtherHouse);
            if (LegalOWner.ConvertToString() != string.Empty)
                paramValues.Add("LegalOWner", LegalOWner);
            if (DocType.ConvertToString() != string.Empty)
                paramValues.Add("DocType", DocType);
            if (CaseTypeFlag.ConvertToString() != string.Empty)
                paramValues.Add("CaseTypeFlag", CaseTypeFlag);
            if (LegalOwnerFlag.ConvertToString() != string.Empty)
                paramValues.Add("LegalOwnerFlag", LegalOwnerFlag);
            if (DocTypeFlag.ConvertToString() != string.Empty)
                paramValues.Add("DocTypeFlag", DocTypeFlag);
            if (OtherHouseFlag.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouseFlag", OtherHouseFlag);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetGrievanceSummaryReport(paramValues);
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr["DISTRICT"].ConvertToString() == "" || dr["LAND_LEGAL_OWNER"] == null)
                    dr.Delete();
            }
            objreport.CaseType = GrievanceType;
            objreport.LegalOwner = LegalOWner;
            objreport.OtherHouse = OtherHouseFlag;
            objreport.DocType = DocType;
            if (CaseTypeFlag == "Y")
            {
                objreport.CaseTypeFlag = "Y";
            }
            if (LegalOwnerFlag == "Y")
            {
                objreport.LegalOwnerFlag = "Y";
            }
            if (OtherHouseFlag == "Y")
            {
                objreport.OtherHouseFlag = "Y";
            }
            if (DocTypeFlag == "Y")
            {
                objreport.DocTypeFlag = "Y";
            }
        
            ViewData["SummaryResults"] = dt;
            return PartialView("~/Views/HtmlReport/_SummaryReport.cshtml", objreport);
        }

        public ActionResult GetGrievanceDetailReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (GrievanceType.ConvertToString() != string.Empty)
                paramValues.Add("CaseType", GrievanceType);
            if (OtherHouse.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouse", OtherHouse);
            if (LegalOWner.ConvertToString() != string.Empty)
                paramValues.Add("LegalOWner", LegalOWner);
            if (DocType.ConvertToString() != string.Empty)
                paramValues.Add("DocType", DocType);
            if (CaseTypeFlag.ConvertToString() != string.Empty)
                paramValues.Add("CaseTypeFlag", CaseTypeFlag);
            if (LegalOwnerFlag.ConvertToString() != string.Empty)
                paramValues.Add("LegalOwnerFlag", LegalOwnerFlag);
            if (DocTypeFlag.ConvertToString() != string.Empty)
                paramValues.Add("DocTypeFlag", DocTypeFlag);
            if (OtherHouseFlag.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouseFlag", OtherHouseFlag);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GrievanceDetail(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            ViewBag.actionName = "CaseGrievanceReport";
            ViewBag.controllerName = "OwnerSummary";
            objreport.CaseType = GrievanceType;
            objreport.LegalOwner = LegalOWner;
            objreport.OtherHouse = OtherHouseFlag;
            objreport.DocType = DocType;
            if (CaseTypeFlag == "Y")
            {
                objreport.CaseTypeFlag = "Y";
            }
            if (LegalOwnerFlag == "Y")
            {
                objreport.LegalOwnerFlag = "Y";
            }
            if (OtherHouseFlag == "Y")
            {
                objreport.OtherHouseFlag = "Y";
            }
            if (DocTypeFlag == "Y")
            {
                objreport.DocTypeFlag = "Y";
            }
            ViewData["DetailResults"] = dt;
            Session["CaseReportParams"] = objreport;
            return RedirectToAction("CaseGrievanceReport", objreport);
        }

        public ActionResult CaseGrievanceReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["DetailResults"] = dt;
            return View("CaseGrievanceReport", objreport);
        }
        [HttpPost]
        public ActionResult CaseGrievanceReport(FormCollection fc, HtmlReport objreport)
        {
            if (fc["Excel"] == "ExportExcel")
            {
                //  DownloadExcel();
            }
            else if (fc["Pdf"] == "ExportPDF")
            {
                // DownloadPdf();
            }
            return View(objreport);
        }


        [HttpPost]
        public ActionResult ExportExcel()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "CaseGrievance" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "CaseGrievance.xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_CaseDetailReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "CaseGrievance.xls");
        }

        [HttpPost]
        public ActionResult ExportPdf()
        {
            string usercd = SessionCheck.getSessionUserCode();
            string htmlFilePath = string.Empty, pdfFilePath = string.Empty;
            if (usercd != "")
            {
                htmlFilePath = Server.MapPath("/files/html/") + "CaseGrievance" + usercd + ".html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "CaseGrievance" + usercd + ".pdf";
            }
            else
            {
                htmlFilePath = Server.MapPath("/files/html/") + "CaseGrievance.html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "CaseGrievance.pdf";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_CaseDetailReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Report"), html);
            html = ReportTemplate.GetReportHTML(html);

            Utils.CreateFile(html, htmlFilePath);
            PdfGenerator.ConvertToPdf(htmlFilePath, pdfFilePath);
            return File(pdfFilePath, "application/pdf", "CaseGrievanceReport.pdf");
        }
        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                 ViewData["DetailResults"] = dt;
                 rptParams = (HtmlReport)Session["CaseReportParams"];
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

        public ActionResult Index()
        {

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMun("");
            return View();
        }

    }
}
