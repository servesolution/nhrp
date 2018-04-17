using ExceptionHandler;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class HouseDetailReportController : Controller
    {
        public DataTable dtbl = null; 
        //
        // GET: /HouseholdHeadReport/

        public ActionResult GetHouseDetailReport()
        {
            string langFlag = "";
            HouseDetailReportService objReportService = new HouseDetailReportService();
            try
            {
                ViewBag.ReportTitle = "HouseDetail Report";                
                dtbl = objReportService.GetHouseDetailReportServiceForDashboard();
                ViewData["dtbl"] = dtbl;                
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_HouseDetailReport");

        }

        public string GetHouseDetailReportGraph(string cType, string rptType, string isHH, string fName)
        {
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
            HouseDetailReportService objReportService = new HouseDetailReportService();
            try
            {

                ViewBag.ReportTitle = "HouseDetail Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetHouseDetailReportServiceForDashboardGraph();
                dtbl.Columns.Remove("MEMBER_COUNT");
                xmlString = dtbl.GetBarDataChartXml(ChartType.Bar3D, searchType, "Count", "300", "");
               


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
