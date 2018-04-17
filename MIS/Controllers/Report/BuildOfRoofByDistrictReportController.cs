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

namespace MIS.Controllers.Report
{
    public class BuildOfRoofByDistrictReportController : BaseController
    {
        [HttpGet]
        public ActionResult BuildOfRoofByDistrict()
        {
            string langFlag = "";
            BuildOfRoofByDistrictService objReportService = new BuildOfRoofByDistrictService();
            DataTable dtbl = null;
            DataTable dtBuildOfRoof = new DataTable();
            try
            {
                dtBuildOfRoof = objReportService.BuiltOfRoof();
                ViewData["dtBuildOfRoof"] = dtBuildOfRoof;
                ViewBag.ReportTitle = "Build of Roof by District Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetBuildOfRoofCount();
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
            return PartialView("_BuildOfRoofByDistrict");
        }
    }
}
