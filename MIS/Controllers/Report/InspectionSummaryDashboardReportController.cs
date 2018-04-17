using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Report;
using MIS.Models.Report;
using ClosedXML.Excel;
using System.IO;

namespace MIS.Controllers.Report
{
    public class InspectionSummaryDashboardReportController : BaseController
    {
        //
        // GET: /InspectionSummaryDashboardReport/

        [HttpGet]
        public ActionResult GetInspectionSummaryReport()
        {
            string langFlag = "";
            InspectionSummaryDashboardReportService objReportService = new InspectionSummaryDashboardReportService();
            DataTable dtbl = null;
             try
            {
                dtbl = objReportService.InspectionSummary();
                ViewData["InspectionSummary"] = dtbl;
                ViewBag.ReportTitle = "Inspection Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                 

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_InspectionSummaryByDistrit");
        }

        public ActionResult InspectionProgressExportReport1(string dist, string vdc, string ward, string Recomendation, string BatchID)
        {
            DataTable dt = new DataTable();
            InspectionProgressExportDataTable();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "InspectionProgressReport" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "InspectionProgressReport" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/InspectionSummaryDashboardReport/_InspectionSummaryByDistrit.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Handling Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "InspectionProgressReport" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls");
        }

        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
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


        public ActionResult InspectionProgressExportReport()
        {

            DataTable dt = new DataTable();
   
            dt = InspectionProgressExportDataTable();

            return ExportData(dt);

        }
        public DataTable InspectionProgressExportDataTable()
        {
            string langFlag = "";
            InspectionSummaryDashboardReportService objReportService = new InspectionSummaryDashboardReportService();
            DataTable dtbl = null;
            try
            {
                dtbl = objReportService.InspectionSummary();
                Session["SummaryResults"] = dtbl;
                ViewData["InspectionSummary"] = dtbl;
                ViewBag.ReportTitle = "Inspection Report";
                langFlag = Utils.ToggleLanguage("E", "N");


            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            return dtbl;


        }
        public ActionResult ExportData(DataTable dt)
        {

            DataTable dt1 = new DataTable();
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
                Response.AddHeader("content-disposition", "attachment;filename= InspectionProgressReport.xlsx");

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
    }
}
