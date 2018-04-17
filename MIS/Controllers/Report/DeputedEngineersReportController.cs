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

namespace MIS.Controllers.Report
{
    public class DeputedEngineersReportController : Controller
    {
        //
        // GET: /DeputedEngineersReport/
        CommonFunction common = new CommonFunction();
        public ActionResult GetSummaryReport(string dist, string vdc, string Ward)
        {
            NameValueCollection paramValues = new NameValueCollection();
             HtmlReport objreport = new HtmlReport();
             DeputedEngineersService objService = new DeputedEngineersService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (Ward != string.Empty)
                paramValues.Add("Ward", Ward);
 

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
            objreport.VDC = vdc;
            objreport.Ward = Ward;
            dt = objService.GetEngineersSummaryReport(paramValues); 
            ViewData["ResultsSumEngineers"] = dt;
            Session["ResultsSumEngineers"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("EngineersSummaryReport", objreport);
        }
        public ActionResult EngineersSummaryReport(HtmlReport obj)
        {
            
            DataTable dt = null;
            dt = (DataTable)Session["ResultsSumEngineers"];
            ViewData["ResultsSumEngineers"] = dt;
            ViewBag.ReportTitle = "Engineers Summary Report";
            return View(obj);
        }

        public ActionResult GetDetailReport(string dist, string vdc, string Ward)
        {
            DeputedEngineersService objService = new DeputedEngineersService();

            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (Ward != string.Empty)
                paramValues.Add("Ward", Ward);


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
            objreport.VDC = vdc;
            objreport.Ward = Ward;
            dt = objService.GetEngineersDetailReport(paramValues); 
            ViewData["ResultDetailReport"] = dt;
            Session["ResultDetailReport"] = dt;
            Session["ResultDetailReportObj"] = objreport;
            return RedirectToAction("EngineersDetailReport", objreport);

        }
        public ActionResult EngineersDetailReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultDetailReport"];
            ViewData["ResultDetailReport"] = dt;
            ViewBag.ReportTitle = "Engineers Detail Report";
            return View(obj);
        }

    }
}
