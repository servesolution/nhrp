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
using MIS.Services.ResurveyView;
using System.IO;

namespace MIS.Controllers.Report
{
    public class ReSurveyBuildingDamageController : Controller
    {
        //
        // GET: /ReSurveyBuildingDamage/

        CommonFunction common = new CommonFunction();

        //Re-Survey Building Damage Summary Report
        public ActionResult GetReSurveyBuildingDamageSummaryReport(string dist, string currentVdc, string currentWard, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);

            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);

            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);

            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);

            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);

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
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouseFlag = OthHouse;
            objreport.BuildingCondition = BuildingCondition;

            dt = objService.GetReSurveyBuildingDamageSummaryReport(paramValues);
            ViewData["ResultsReSurveyBuildingDamageSummary"] = dt;
            Session["ResultsReSurveyBuildingDamageSummary"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("ReSurveyBuildingDamageSummaryReport", objreport);
        }
        public ActionResult ReSurveyBuildingDamageSummaryReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsReSurveyBuildingDamageSummary"];
            ViewData["ResultsReSurveyBuildingDamageSummary"] = dt;
            ViewBag.ReportTitle = "Re-Survey Building Damage Summary Report";
            return View(obj);
        }

        //Re-Survey Building Damage Detail Report

        public ActionResult GetReSurveyBuildingDamageDetailReport(string dist, string currentVdc, string currentWard, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition, string isexport)
        {
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            ResurveyViewService objService = new ResurveyViewService();
            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            if (currentVdc != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (currentWard != string.Empty)
                paramValues.Add("Ward", currentWard);

            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);

            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);

            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);

            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);

            if (Session["LanguageSetting"].ToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (isexport.ConvertToString() != string.Empty)
                paramValues.Add("isexport", isexport);

            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouseFlag = OthHouse;
            objreport.BuildingCondition = BuildingCondition;

            dt = objService.GetReSurveyBuildingDamageDetailReport(paramValues);
            ViewData["ResultsReSurveyBuildingDamageDetail"] = dt;
            Session["ResultsReSurveyBuildingDamageDetail"] = dt;
            Session["participantparams"] = objreport;



            return RedirectToAction("ReSurveyBuildingDamageDetailReport", objreport);
        }
        public ActionResult ReSurveyBuildingDamageDetailReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsReSurveyBuildingDamageDetail"];
            ViewData["ResultsReSurveyBuildingDamageDetail"] = dt;
            ViewBag.ReportTitle = "Re-Survey Building Damage Detail Report";
            return View(obj);
        }

        //Export - Re-Survey Building Damage Report
        public ActionResult ExportBuildingDamageReport(string dist, string currentVdc, string currentWard, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition, string isexport)
        {
            GetReSurveyBuildingDamageDetailReport(dist, currentVdc, currentWard, DamageGrade, TechnicalSolution, OthHouse, BuildingCondition, isexport);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Building Damage report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Re-Survey Building Damage report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforgradewdar("~/Views/ReSurveyBuildingDamage/_ReSurveyBuildingDamageDetailReport.cshtml");
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "ReSurveyBuildingDamage.xls");


        }

        protected string RenderPartialViewToStringforgradewdar(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["participantparams"];
            if ((DataTable)Session["ResultsReSurveyBuildingDamageDetail"] != null)
            {
                DataTable dt = (DataTable)Session["ResultsReSurveyBuildingDamageDetail"];

                ViewData["ReSurveyDetailResults"] = dt;
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
