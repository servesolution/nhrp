using EntityFramework;
using MIS.Models.Report;
using MIS.Services.BusinessCalculation;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
namespace MIS.Controllers.Report
{
    public class NonBeneficiaryHTMLReportController : BaseController
    {
        CommonFunction common = new CommonFunction();
        public ActionResult NonBeneficiaryDetailReport(NonBeneficiaryReport objreport)
        {

            
            
            
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            return View("NonBeneficiaryDetailReport", objreport);
        }
        public ActionResult GetNonBeneficiaryDetail(string dist, string vdc, string ward, string Type)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            NonBeneficiaryReport objreport = new NonBeneficiaryReport();
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            DataTable dt = new DataTable();
            dt = service.GetNonBeneficiaryDetail(paramValues);
            Session["Results"] = dt;
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.WARD = ward;
            Session["NonBeneficiaryParams"] = objreport;
            return RedirectToAction("NonBeneficiaryDetailReport", objreport);
          
        }
        [HttpPost]
        public ActionResult ExportPdf()
        {
            string usercd = SessionCheck.getSessionUserCode();
            string htmlFilePath = string.Empty, pdfFilePath = string.Empty;
            if (usercd != "")
            {
                htmlFilePath = Server.MapPath("/files/html/") + "NonBeneficiary" + usercd + ".html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "NonBeneficiary" + usercd + ".pdf";
            }
            else
            {
                htmlFilePath = Server.MapPath("/files/html/") + "NonBeneficiary.html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "NonBeneficiary.pdf";
            }

            string html = RenderPartialViewToString("~/Views/HTMLReport/_NonBeneficiaryHTMLReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Non Beneficiary Report"), html);
            html = ReportTemplate.GetReportHTML(html);

            Utils.CreateFile(html, htmlFilePath);
            PdfGenerator.ConvertToPdf(htmlFilePath, pdfFilePath);
            return File(pdfFilePath, "application/pdf", "NonBeneficiaryReport.pdf");
        }

        [HttpPost]
        public ActionResult ExportExcel()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "NonBeneficiary" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "NonBeneficiary.xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_NonBeneficiaryHTMLReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Non Beneficiary Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "NonBeneficiaryReport.xls");
        }

        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            NonBeneficiaryReport rptParams = new NonBeneficiaryReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["Results"] = dt;
                rptParams = (NonBeneficiaryReport)Session["NonBeneficiaryParams"];
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
