using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;
using MIS.Services.Enrollment;
using System;
using MIS.Services.Core;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class EnrollmentReportController : Controller
    {
        //
        // GET: /EnrollmentReport/
        CommonFunction common = new CommonFunction();
        public ActionResult EnrollementDetailReport(HtmlReport obj)
        {
            int Index = obj.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewBag.ReportTitle = "Retrofitting Enrollment Report";
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewData["Results"] = dt;
            return View(obj);
        }

        //
        public ActionResult EnrollmentPADailyDetailReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Detail Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        public ActionResult GetEnrollmentPADailyDetailReport(string dateTo, string dateFrom, string user)
        {
            CommonFunction common = new CommonFunction();

            EnrollmentService objEnrService = new EnrollmentService();
            NameValueCollection paramValues = new NameValueCollection();

            HtmlReport objReport = new HtmlReport();
            objReport.DateTo = dateTo;
            objReport.DateFrom = dateFrom;
            objReport.User = user;

          
            if (user.ConvertToString() != string.Empty)
                paramValues.Add("user", user);

            if (dateFrom.ConvertToString() != string.Empty)
                paramValues.Add("DateFrom", dateFrom);

            if (dateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", dateTo);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objEnrService.GetEntollmentPADetailReport(paramValues);
            if (dt != null)
            {
                Session["DetailResults"] = dt;                
                objReport.DateTo = dateTo;
                objReport.DateFrom = dateFrom;
                objReport.User = user;
            }
            return RedirectToAction("EnrollmentPADailyDetailReport", objReport);
        }

        //
        public ActionResult EnrollmentPADailySummaryReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(obj);
        }

        
        // For Daily Enrollment PA summay Report
        public ActionResult GetEnrollmentPADailySummaryReport(string dateTo, string dateFrom, string user, string selectedType)
        {
            CommonFunction common = new CommonFunction();

            EnrollmentService objEnrService = new EnrollmentService();
            NameValueCollection paramValues = new NameValueCollection();

            HtmlReport objReport = new HtmlReport();
            objReport.DateTo = dateTo;
            objReport.DateFrom = dateFrom;
            objReport.User = user;
            objReport.SelectedType = selectedType;

            if (user.ConvertToString() != string.Empty)
                paramValues.Add("user", user);

            if (user.ConvertToString() != string.Empty)
                paramValues.Add("selectedType", selectedType);

            if (dateFrom.ConvertToString() != string.Empty)
                paramValues.Add("DateFrom", dateFrom);

            if (dateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", dateTo);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objEnrService.GetEntollmentPASummaryReport(paramValues); 
            if (dt != null)
            {
                Session["SummaryResults"] = dt;
                objReport.DateTo = dateTo;
                objReport.DateFrom = dateFrom;
                objReport.User = user;
                objReport.SelectedType = selectedType;

            }
            return RedirectToAction("EnrollmentPADailySummaryReport", objReport);
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
        public ActionResult GetEnrollmentDetailReport(string dist, string vdc, string ward, string pagesize, string pageindex, string currentVdc, string currentWard,string exportcheck)
        {

            EnrollmentDetailReportService objEnrollService = new EnrollmentDetailReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);

            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);
            if (exportcheck.ConvertToString() != string.Empty)
                paramValues.Add("exportcheck", exportcheck);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);


            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;


            dt = objEnrollService.EnrollmentDetailReport(paramValues);
                int tol = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    int idx = tol % 100;
                    if (idx > 0)
                    {
                        objreport.Index = (tol / 100) + 1;
                    }
                    else
                    {
                        objreport.Index = 1;
                    }



                    objreport.Remaining = tol % 100;
                    if (pageindex != null)
                    {
                        objreport.showing = Convert.ToInt32(pageindex) * 100;
                        if (objreport.showing > tol)
                        {
                            objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                        }
                    }
                    else
                        if (objreport.Index == 1)
                        {
                            if (objreport.showing > tol || objreport.Index == 1)
                            {
                                objreport.showing = tol;


                            }

                        }
                        else
                        {
                            objreport.showing = Convert.ToInt32(1) * 100;

                        }
                }
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            Session["reconstenrollmentparams"] = objreport;
            return RedirectToAction("EnrollementDetailReport", objreport);

        }


        [HttpPost]
        public ActionResult GetEnrollmentDetailReport3(string dist, string vdc, string ward, string pagesize, string pageindex)
        {
            //OwnerSummaryService service = new OwnerSummaryService();
            EnrollmentDetailReportService objEnrollService = new EnrollmentDetailReportService();
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

            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;

            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objEnrollService.EnrollmentDetailReport(paramValues);
            int tol = Convert.ToInt32(dt.Rows[0][27]);
            objreport.Total = Convert.ToInt32(dt.Rows[0][27]);
            int idx = tol % 100;
            if (idx > 0)
            {
                objreport.Index = (tol / 100) + 1;
            }
            else
            {
                objreport.Index = (tol / 100);
            }



            objreport.Remaining = tol % 100;
            if (pageindex != null && pageindex !="ALL")
            {
                objreport.showing = Convert.ToInt32(pageindex) * 100;
                if (objreport.showing > tol)
                {
                    objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                }
            }
            else
            {
                if(pageindex !="ALL")
                { 
                objreport.showing = Convert.ToInt32(1) * 100;
                }
                else
                {
                    objreport.showing = objreport.Total;
                }
            }
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            ViewData["Results"] = dt;
            //return View(objReport);
            return PartialView("~/Views/HtmlReport/_EnrollmentDetailReport.cshtml", objreport);
        }
        public ActionResult ExportEnrollmentDetailReport(string dist, string vdc, string ward)
        {

            GetEnrollmentDetailReportForExport(dist, vdc, ward);
            HtmlReport rptParams = new HtmlReport();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrollment_Detail_REPORT" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrollment_Detail_REPORT" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_EnrollmentDetailReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Enrollment Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Enrollment_Detail_REPORT" + "( District " + dist + " ).xls");
        }



        public ActionResult GetEnrollmentDetailReportForExport(string dist, string vdc, string ward)
        {

            EnrollmentDetailReportService objEnrollService = new EnrollmentDetailReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
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
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            dt = objEnrollService.EnrollmentDetailReportForExport(paramValues);
           
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrollementDetailReport", objreport);

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

        public ActionResult ReconstruEnrollDetailExport(string dist, string vdc, string ward, string pagesize, string pageindex, string currentVdc, string currentWard, string exportcheck)
        {
            GetEnrollmentDetailReport(dist, vdc, ward, pagesize, pageindex, currentVdc, currentWard, exportcheck);
            DataTable dt = new DataTable();
            HtmlReport rptParametess = new HtmlReport();
            rptParametess = (HtmlReport)Session["reconstenrollmentparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Enrollment  Detail report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Enrollment report" + "( District " + dist + " ).xls";
            }

            string html = RenderPartialViewToStringforTrainingreportDetail("~/Views/HTMLReport/_EnrollmentDetailReport.cshtml");



            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Reconstructionenrolleddetail.xls");


        }

        protected string RenderPartialViewToStringforTrainingreportDetail(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["reconstenrollmentparams"];
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