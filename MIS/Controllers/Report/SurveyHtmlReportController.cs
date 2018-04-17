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
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;
namespace MIS.Controllers.Report
{
    public class SurveyHtmlReportController : Controller
    {
        //
        // GET: /SurveyHtmlReport/
        CommonFunction common = new CommonFunction(); 

        public ActionResult SurveyHouseDetail(HtmlReport obj)
        {
            int Index = obj.Index; 
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = @Utils.GetLabel("Grade Wise Damage Assessment Detail Report");
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            return View("SurveyHouseDetail", obj);
        }


        public List<SelectListItem> GetIndexNumber(string selectedValue, int index)
        {
            int i;
            List<SelectListItem> selLstCertificate = new List<SelectListItem>();
            //List<MISCommon> lstCommon = commonService.GetCertificate();
            //if (ispopup == false)
            //{
            //selLstCertificate.Add(new SelectListItem { Value = "", Text = "--Select Experience--" });
            // }
            for (i = 1; i <= index; i++)
            {
                selLstCertificate.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            selLstCertificate.Add(new SelectListItem { Value = "ALL", Text = "ALL" });
            foreach (SelectListItem item in selLstCertificate)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstCertificate;
        }
        public ActionResult SurveyHouseSummary(HtmlReport objreport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewBag.ReportTitle = "Grade Wise Damage Assessment Summary Report";
            ViewData["Results"] = dt;
            return View("SurveyHouseSummary", objreport);
        }
        public ActionResult GetHouseDetailReport(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition, string pagesize, string pageindex, string currentVdc, string currentWard,string isexport)
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
            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);

            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);
            else
                paramValues.Add("pagesize", 100);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);



            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);
            if (isexport.ConvertToString() != string.Empty)
                paramValues.Add("isexport", isexport);

            //DataTable dt = new DataTable();
            //DataSet ds = new DataSet();

            //dt = service.GetSurveyHouseDetail(paramValues);
            //int tol = 0;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            //    objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            //    int idx = tol % 100;
            //    if (idx > 0)
            //    {
            //        objreport.Index = (tol / 100) + 1;
            //    }
            //    else
            //    {
            //        objreport.Index = 1;
            //    }



            //    objreport.Remaining = tol % 100;
            //    if (pageindex != null && pageindex != "ALL")
            //    {
            //        objreport.showing = Convert.ToInt32(pageindex) * 100;
            //        if (objreport.showing > tol)
            //        {
            //            objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


            //        }
            //    }
            //    else
            //        if (objreport.Index == 1)
            //        {
            //            if (objreport.showing > tol || objreport.Index == 1)
            //            {
            //                objreport.showing = tol;


            //            }

            //        }
            //        else
            //        {
            //            if (pageindex != "ALL")
            //            {
            //                objreport.showing = Convert.ToInt32(1) * 100;
            //            }
            //            else
            //            {
            //                objreport.showing = objreport.Total;
            //            }

            //        }
            //}
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouseFlag = OthHouse;
            objreport.BuildingCondition = BuildingCondition;
            Session["HouseReportParams"] = objreport;
           // ViewData["Results"] = dt;
            //Session["Results"] = dt;
            return RedirectToAction("SurveyHouseDetail", objreport);
        }

        [HttpPost]
        public JsonResult GetHouseDetailReport3(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition, string currentVdc, string currentWard, int start, int length, int draw)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            //GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);

            if (length.ConvertToString() != string.Empty && length.ConvertToString() != "-1")
                paramValues.Add("pagesize", length);
            else if (length.ConvertToString() != "-1")
                paramValues.Add("pagesize", "ALL");

            if (start.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", start);



            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);
            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            paramValues.Add("draw", draw);
            //if (!string.IsNullOrEmpty(isexport))
            //    paramValues.Add("isexport", "Y");

            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;

            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouse = OthHouse;
            objreport.BuildingCondition = BuildingCondition;
            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            Session["CaseReportSummaryParams"] = objreport;
         
            DataTable dt =  service.GetSurveyHouseDetail(paramValues);
            var recordCount = 0;
            if(dt!=null && dt.Rows.Count>0)
                recordCount=Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            var list = Common.ConvertToList(dt);
           
            var dataToReturn = Json(new
            {
                data = list,
                recordsFiltered = recordCount,
                recordsTotal = recordCount,
                draw
            }, JsonRequestBehavior.AllowGet); 
            dataToReturn.MaxJsonLength = Int32.MaxValue;
            return dataToReturn;
          
        }
      
        public ActionResult GetHouseSummaryReport(string dist, string currentVdc, string currentWard, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            HtmlReport objreport = new HtmlReport();
            NameValueCollection paramValues = new NameValueCollection();
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
            //if (currentVdc.ConvertToString() != string.Empty)
            //    paramValues.Add("vdc", common.GetCodeFromDataBase(currentVdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            //if (currentWard.ConvertToString() != string.Empty)
            //    paramValues.Add("ward", currentWard);
            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetSurveyHouseSummary(paramValues);
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;


            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouseFlag = OthHouse;
            objreport.BuildingCondition = BuildingCondition;

            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("SurveyHouseSummary",objreport);
        }

        //household grade report detail

        public ActionResult HouseholdGradeDetail()
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsGrade"];
            ViewData["Results"] = dt;
            return View("HouseholdGradeDetail");
        }
        public ActionResult GetHouseholdgradeReport(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
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
            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.HouseholdGradeDetail(paramValues);
            ViewData["Results"] = dt;
            Session["ResultsGrade"] = dt;
            return RedirectToAction("HouseholdGradeDetail");
        }
        public ActionResult ExportHouseDamageReport()
        {
            //GetGrievanceHandlingReport(dist, vdc, ward, BatchID, GHDataFlag);
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["HouseReportParams"];
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            string dist = rptParams.District;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "BuildingDamageReport" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "BuildingDamageReport" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_SurveyHouseDetailReport.cshtml.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Building Damage Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "BuildingDamageReport" + "( District " + dist + " ).xls");
        }
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
        public ActionResult ExportHouseDetailReport(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition, string pagesize, string pageindex, string currentVdc, string currentWard,string isexport)
        {
            GetHouseDetailReport(dist, vdc, ward, DamageGrade, TechnicalSolution, OthHouse, BuildingCondition, pagesize, pageindex, currentVdc, currentWard, isexport);
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["HouseReportParams"];
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Grade wise damage assessment" + "( District " + dist + " ) ( vdc" + vdc + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Gradew wise damage assessment" + "( District " + dist + " ) ( vdc" + vdc + " ).xls";
            }
            string html = RenderPartialBDDetailViewToString("~/Views/HTMLReport/_SurveyHouseDetailReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("House Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Gradewisedmgassess" + "( District " + dist + " ) ( vdc" + vdc + " ).xls");
        }

        public ActionResult GetHouseDetailReportForReport(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition)
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
            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);



            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetSurveyHouseDetail(paramValues);

            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouseFlag = OthHouse;
            objreport.BuildingCondition = BuildingCondition;
            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("SurveyHouseDetail", objreport);
        }


        protected string RenderPartialBDDetailViewToString(string viewName)
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
                    ViewData.Model = rptParams;
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }

        public ActionResult ExportGradewisedamage(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition, string pagesize, string pageindex, string currentVdc, string currentWard,string exportvalue)
        {
            GetHouseDetailReport(dist, vdc, ward, DamageGrade, TechnicalSolution, OthHouse, BuildingCondition, pagesize, pageindex, currentVdc, currentWard, exportvalue);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["HouseReportParams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Grade wise damage assessment report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Grade wise damage assessment report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforgradewdar("~/Views/HTMLReport/_SurveyHouseDetailReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "GradewiseDmgAssess.xls");


  }

        protected string RenderPartialViewToStringforgradewdar(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["HouseReportParams"];
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["DetailResults"] = dt;
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
