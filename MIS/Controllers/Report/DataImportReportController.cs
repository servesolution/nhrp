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
    public class DataImportReportController : Controller
    {
        public DataTable dtbl = null;
        //
        // GET: /DataImportReport/

        public ActionResult GetDataImportReport()
        {
            string langFlag = "";
            DataImportReportService objReportService = new DataImportReportService();
            try
            {
                //List<string> jsonFiles = Directory.GetFiles(Server.MapPath("~/Files/Json/"), "*.json")
                //                     .Select(path => Path.GetFileName(path))
                //                     .ToList();
                List<string> jsonFiles = Directory.GetDirectories(Server.MapPath("~/Files/CSV/"), "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();
                ViewBag.ReportTitle = "NHRP Data Import REPORT";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetDataImportReportForDashboard(jsonFiles);
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
            return PartialView("_DataImportReport");
           
        }

       
    }
}
