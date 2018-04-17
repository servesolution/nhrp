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
using ClosedXML.Excel;

using System.IO;

namespace MIS.Controllers.Report
{
    public class DistributionByHouseDamageGradingByDistrictReportController : BaseController
    {
        [HttpGet]
        public ActionResult DamageGradingByDistrict()
        {
            string langFlag = "";
            DistributionByHouseDamageGradingByDistrictService objReportService = new DistributionByHouseDamageGradingByDistrictService();
            DataTable dtbl = null;
            DataTable dtDamageGrade = new DataTable();
            try
            {
                dtDamageGrade = objReportService.DamageGrade();
                ViewData["dtDamageGrade"] = dtDamageGrade;
                ViewBag.ReportTitle = "Distribution by House Damage Grading";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetDamageGradeCount();
                //dtbl.Columns.Remove("Unrecognized");
                ViewData["dtReport"] = dtbl;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_DamageGradingByDistrict");
        }

        public ActionResult GradeWiseDamageAssesmentReport()
        {

            DataTable dt = new DataTable();
            dt = GradeWiseDamageAssesmentDataTable();

            return ExportData(dt);
           
        }
        public DataTable GradeWiseDamageAssesmentDataTable()
        {

            string langFlag = "";
            DistributionByHouseDamageGradingByDistrictService objReportService = new DistributionByHouseDamageGradingByDistrictService();
            DataTable dtbl = null;
            DataTable dtDamageGrade = new DataTable();
            try
            {
                dtDamageGrade = objReportService.DamageGrade();
                ViewData["dtDamageGrade"] = dtDamageGrade;
                ViewBag.ReportTitle = "Distribution by House Damage Grading";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetDamageGradeCount();
                dtbl.Columns.Remove("Unrecognized");
                ViewData["dtReport"] = dtbl;
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


            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Worksheets.Add(dt);
                //wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //wb.Style.Font.Bold = true;

                IXLWorksheet worksheet = wb.AddWorksheet(dt);
                //var range = worksheet.Range("A1", "AB4").Merge();
                //range.Value = string.Format("GOVERNMENT OF NEPAL {0} NATIONAL RECONSTRUCTION AUTHORITY {0} EARTHQUAKE HOUSING RECONSTRUCTION PROGRAME", Environment.NewLine);
                //range.Style.Alignment.WrapText = true;
                //range.Style.Font.Bold = true;
                //range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);



                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GradeWiseDamageAssessment.xlsx");

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
        public string DamageGradingByDistrictGraph(string cType, string rptType, string isHH, string fName)
        {
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            DataTable dtbl = new DataTable();
            DistributionByHouseDamageGradingByDistrictService objReportService = new DistributionByHouseDamageGradingByDistrictService();    
            try
            {

                ViewBag.ReportTitle = "Damage Grade Count Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetDamageGradeCountGraph();
                xmlString = dtbl.GetBarDataChartXml(ChartType.Pie, searchType, "Count", "200", "");


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

    }



     
}
