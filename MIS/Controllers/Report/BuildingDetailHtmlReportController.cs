using EntityFramework;
using MIS.Services.BusinessCalculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Data;
using MIS.Models.Report;
using System.IO;
using ClosedXML.Excel;

namespace MIS.Models.Report
{
    public class BuildingDetailHtmlReportController : Controller
    {
        //
        // GET: /SurveyHtmlReport/
        CommonFunction common = new CommonFunction();

        public ActionResult BuildingDetail()
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Building Structure Detail Report";
            return View("BuildingDetail");
        }
        public ActionResult GetBuildingReport(string dist, string vdc, string ward)
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
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetBuildingReport(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("BuildingDetail");
        }
        //household grade report detail
        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["Results"] = dt;
                rptParams = (HtmlReport)Session["HouseReportParams"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    ViewData.Model = "";
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }

        public ActionResult BuildingStructureSummaryReport(HtmlReport objreport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            return View("BuildingStructureSummaryReport", objreport);
        }
        public ActionResult GetBSSummaryReport(string dist, string vdc, string ward)
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
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            //dt = service.GetBSSummaryReport(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("BuildingStructureSummaryReport");
        }


        public DataTable BuildingDetailReport(string dist, string vdc, string ward)
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
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetBuildingReport(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return dt;
        }


               //End Export to Excel

        public ActionResult ExportBuildingReport(string dist, string vdc, string ward)
        {
            DataTable dt = new DataTable();
            dt = BuildingDetailReport(dist, vdc, ward);
            return ExportData(dt);
            //ViewData["ExportFont"] = "font-size: 13px";
            //BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            //rptParams = (BeneficiaryHtmlReport)Session["BenfReportDetailParams"];
            ////BatchID = rptParams.BATCHID;
            ////dist = rptParams.District;
            ////vdc = rptParams.VDC;
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Detail Report " + "( District " + dist + " ) ( Type" + Type + " ) ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ) ( Coordinate" + Coordinate + " ) ( WardFlag" + WardFlag + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Detail Report " + "( District " + dist + " ) ( Type" + Type + " ) ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ) ( Coordinate" + Coordinate + " ) ( WardFlag" + WardFlag + " ).xls";
            //}
            //string html = RenderDetailPartialViewsToString("~/Views/BeneficiaryHtmlReport/_BeneficiaryHTMLReportDetail.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Reconstruction Beneficiary Detail Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Reconstruction Beneficiary Detail Report" + "( District " + dist + " ) ( Type" + Type + " ) ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " )  ( Coordinate" + Coordinate + " ) ( WardFlag" + WardFlag + " ).xls");
        }

        //Export to Excel
        public ActionResult ExportData(DataTable dt)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                    ws.Cell("B1").Value = "HOUSE OWNER NAME";
                    ws.Cell("C1").Value = "HOUSE SERIAL NO";
                    ws.Cell("G1").Value = "DISTRICT";
                    ws.Cell("I1").Value = "VDC";
                    ws.Cell("E1").Value = "WARD NO";
                    ws.Cell("K1").Value = "BUILDING CONDITION";
                    ws.Cell("A1").Value = "DAMAGE GRADE TYPE";
                    ws.Cell("M1").Value = "FOUNDATION TYPE";
                    ws.Cell("O1").Value = "BUILDING CONDITION";
                    ws.Cell("Q1").Value = "GROUND SURFACE TYPE";
                    ws.Cell("S1").Value = "FLOOR MATERIAL TYPE";
                    ws.Cell("U1").Value = "RECONSTRUCTION MATERIAL TYPE";
                    ws.Cell("W1").Value = "BUILDING POSITION";
                    ws.Cell("Y1").Value = "BUILDING PLAN";
                    ws.Cell("AA1").Value = "ASSESSED AREA";
                    ws.Cell("AE1").Value = "STOREY CONSRUCT MATERIALS";
                    ws.Cell("AF1").Value = "SUPERCONSTRUCT MATERIALS";

                ws.Columns("A,D,F,H,J,L,N,P,R,T,V,X,Z,AB,AC,AD").Delete();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= BuildingStructureReport.xlsx");

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