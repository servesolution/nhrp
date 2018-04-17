using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class DonorEnrolledSummaryReportController : Controller
    {
        //
        // GET: /EnrollmentReport/
        CommonFunction common = new CommonFunction();
        public ActionResult EnrolledSummaryReport(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Terries Upload Report";
            return View(ObjReport);
        }
        public ActionResult GetEnrolledSummaryReport(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
            paramValues.Add("vdcCurr", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", ward);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            dt = objEnrollService.EnrollmentSummaryReport(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrolledSummaryReport",objReport);

        }

        public ActionResult EnrolledDetailReportPA(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Enrollment Detail PA Report";
            return View(ObjReport);
        }
        public ActionResult GetEnrolledDetailReportPA(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

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
            dt = objEnrollService.EnrollmentDetailReportPA(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrolledDetailReportPA", objReport);

        }

        public ActionResult EnrolledSummaryReportPA(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Enrollment Summary PA Report";
            return View(ObjReport);
        }

        public ActionResult GetEnrolledSummaryReportPA(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

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
            dt = objEnrollService.EnrollmentSummaryReportPA(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrolledSummaryReportPA", objReport);

        }

        public ActionResult ExportEnrolledSummaryReport(string dist, string vdc, string ward)
        {

            GetEnrolledSummaryReport(dist, vdc, ward);
            HtmlReport rptParams = new HtmlReport();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_EnrolledSummaryReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls");
        }

        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["Results"] = dt;
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