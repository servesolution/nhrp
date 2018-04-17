using EntityFramework;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Payment.HDSP;
using MIS.Models.Report;
using System.IO;
using MIS.Models.Setup;

namespace MIS.Controllers.Payment.HDSP
{
    public class NRATOBANKController : BaseController
    {
        //
        // GET: /NRATOBANK/
        CommonFunction common = new CommonFunction();
        public ActionResult GetApprovedEnrollmentList()
         {
             NHRSBankMapping objBankMapping = new NHRSBankMapping();

             //RouteValueDictionary rvd = new RouteValueDictionary();

             ViewData["ddl_District"] = common.GetDistricts("");
             ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objBankMapping.VDC_MUN_CD.ConvertToString(), objBankMapping.DISTRICT_CD.ConvertToString());
             ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankMapping.WARD_NO.ConvertToString(), objBankMapping.VDC_MUN_CD);
             ViewData["ddl_Bank"] = common.GetBankName("");
             ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
             //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
             ViewData["ddl_Installation"] = common.GetInstallation("");
             return View(objBankMapping);

        }
        public ActionResult GetApprovedEnrollList()
        {
            DataTable dtbl = new DataTable();
            dtbl = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dtbl;
            ViewData["BankName"] = Session["bankname"];
            ViewData["BranchName"] = Session["branchname"];
            ViewData["Todaydate"] = Session["todaydate"];
            ViewBag.ReportTitle = "Payment Order 1st Terries";
            return View();
        }
        public ActionResult GetInspectedEnrollList()
        {
            DataTable dtbl = new DataTable();
            dtbl = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dtbl;
            ViewData["BankName"] = Session["bankname"];
            ViewData["BranchName"] = Session["branchname"];
            ViewData["Todaydate"] = Session["todaydate"];
            ViewBag.ReportTitle = "Payment Order List";

            return View();
        }
        [HttpPost]
        public ActionResult EnrollInspectedBankList(string dist, string vdc, string ward, string bankname, string branchname, string installation)
        {
            DataTable dt = new DataTable();
            GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            NameValueCollection paramValues = new NameValueCollection();
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "mis_vdc_municipality", "vdc_mun_cd"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            //if (Fiscalyr.ConvertToString() != string.Empty)
            //    paramValues.Add("Fiscalyr", Fiscalyr);
            if (installation.ConvertToString() != string.Empty)
                paramValues.Add("installation", common.GetCodeFromDataBase(installation, "NHRS_PAYROLL_INSTALLMENT", "PAYROLL_INSTALL_CD"));
            if (bankname.ConvertToString() != string.Empty)
                paramValues.Add("bankname", common.GetCodeFromDataBase(bankname, "nhrs_bank", "bank_cd"));
            if (branchname.ConvertToString() != string.Empty)
                paramValues.Add("branchname", common.GetCodeFromDataBase(branchname, "NHRS_BANK_BRANCH", "BANK_BRANCH_CD"));
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            dt = objpaymentservice.getinspectedpaymentList(paramValues);

            ViewData["SummaryResults"] = dt;

            Session["SummaryResults"] = dt;
            //ViewData["Message"] = fc["hdnMessage"].ConvertToString();

            return PartialView("~/Views/HDSPPaymentProcess/_EnrollInspectedBankList.cshtml");

        }
        public ActionResult GetPaymentApprovedEnrollList(string dist, string vdc, string ward,string bankname, string branchname, string installation,string exportcheck)
        {

            GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            NameValueCollection paramValues = new NameValueCollection();
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "mis_vdc_municipality", "vdc_mun_cd"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (exportcheck.ConvertToString() != string.Empty)
                paramValues.Add("exportcheck", exportcheck);
            //if (Fiscalyr.ConvertToString() != string.Empty)
            //    paramValues.Add("Fiscalyr", Fiscalyr);
            if (installation.ConvertToString() != string.Empty)
                 paramValues.Add("installation", common.GetCodeFromDataBase(installation, "NHRS_PAYROLL_INSTALLMENT", "PAYROLL_INSTALL_CD"));
            if (bankname.ConvertToString() != string.Empty)
                paramValues.Add("bankname", common.GetCodeFromDataBase(bankname, "nhrs_bank", "bank_cd"));
            if (branchname.ConvertToString() != string.Empty)
                paramValues.Add("branchname", common.GetCodeFromDataBase(branchname, "NHRS_BANK_BRANCH", "BANK_BRANCH_CD"));
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            Session["bankname"] = bankname;
            Session["branchname"] = branchname;
            DateTime date = DateTime.UtcNow;
            string todaydate = date.ToShortDateString();

            ViewData["Todaydate"] = todaydate;
            Session["todaydate"] = todaydate;
            DataTable dt = new DataTable();
            if (installation == "0")
            {
                dt = objpaymentservice.getapprovePaymentList(paramValues);
                ViewData["SummaryResults"] = dt;
                Session["SummaryResults"] = dt;
                Session["terriesparams1"] = paramValues;

                //ViewBag.ReportTitle = "Inspection Terries Second Installment";

                return RedirectToAction("GetApprovedEnrollList");
            }
            else if (installation == "1")
            {
                dt = objpaymentservice.getinspectedpaymentList(paramValues);
                ViewData["SummaryResults"] = dt;
                //ViewBag.ReportTitle = "Inspection Terries Third Installment";
                Session["SummaryResults"] = dt;
                Session["terriesparams2"] = paramValues;
                return RedirectToAction("GetInspectedEnrollList");
                //return PartialView("~/Views/HDSPPaymentProcess/_EnrollInspectedBankList.cshtml",paramValues);
            }
            else if (installation == "2")
            {
                dt = objpaymentservice.getinspectedpaymentList(paramValues);
                ViewData["SummaryResults"] = dt;
                //ViewBag.ReportTitle = "Inspection Terries Third Installment";
                Session["SummaryResults"] = dt;
                Session["terriesparams2"] = paramValues;
                return RedirectToAction("GetInspectedEnrollList");
                //return PartialView("~/Views/HDSPPaymentProcess/_EnrollInspectedBankList.cshtml",paramValues);
            }
            return null;
        }
        [HttpPost]
        public ActionResult ExportApproveEnrollPaymentList()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "PaymentOrder1" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "PaymentOrder1.xls";
            }
            string html = RenderPartialPaymentViewToString("~/Views/HDSPPaymentProcess/_ApprovedEnrollPaymentList.cshtml");
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "PaymentOrder1.xls");
        }
        protected string RenderPartialPaymentViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                string bankname = Session["bankname"].ConvertToString();
                string branchname = Session["branchname"].ConvertToString();
                string TodayDate = Session["todaydate"].ConvertToString();
                ViewData["SummaryResults"] = dt;
                ViewData["BankName"] = bankname;
                ViewData["BranchName"] = branchname;
                ViewData["Todaydate"] = TodayDate;
                rptParams = (HtmlReport)Session["ApprovedPaymentParams"];
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
        [HttpPost]
        public ActionResult ExportInspectedEnrollPaymentList()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "InspectedPaymentList" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "InspectedPaymentList.xls";
            }
            string html = RenderPartialInspectedViewToString("~/Views/HDSPPaymentProcess/_InspectedEnrollPaymentList.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Housing Grant Distribution Detail(Terris 2)"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "InspectedPaymentList.xls");
        }
        protected string RenderPartialInspectedViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                string bankname = Session["bankname"].ConvertToString();
                string branchname = Session["branchname"].ConvertToString();
                string TodayDate = Session["todaydate"].ConvertToString();
                ViewData["SummaryResults"] = dt;
                ViewData["BankName"] = bankname;
                ViewData["BranchName"] = branchname;
                ViewData["Todaydate"] = TodayDate;
                rptParams = (HtmlReport)Session["InspectedPaymentParams"];
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

        public ActionResult TerriesExport(string dist, string vdc, string ward, string bankname, string branchname, string installation, string exportcheck)
        {
            GetPaymentApprovedEnrollList(dist, vdc, ward, bankname, branchname, installation, exportcheck);
            DataTable dt = new DataTable();

            NameValueCollection rptParametess = new NameValueCollection();
            if (installation == "1")
            {
                rptParametess = (NameValueCollection)Session["terriesparams1"];

            }
            else if (installation == "2")
            {

                rptParametess = (NameValueCollection)Session["terriesparams2"];
            }
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Terries  Report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Terries  Report" + "( District " + dist + " ).xls";
            }
            string html;
            if (installation == "1")
            {
                html = RenderPartialViewToStringforTerriesReport("~/Views/HDSPPaymentProcess/_ApprovedEnrollPaymentList.cshtml", installation);

            }
            else
            {
                html = RenderPartialViewToStringforTerriesReport("~/Views/HDSPPaymentProcess/_InspectedEnrollPaymentList.cshtml",installation);

            }

            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "TerriesReport.xls");


        }

        protected string RenderPartialViewToStringforTerriesReport(string viewName,string installation)
        {
            DataTable dtbl = new DataTable();
            NameValueCollection rptParams = new NameValueCollection();
            if (installation == "1")
            {
                rptParams = (NameValueCollection)Session["terriesparams1"];

            }
            else if (installation == "2")
            {

                rptParams = (NameValueCollection)Session["terriesparams2"];
            }
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];

                ViewData["SummaryResults"] = dt;
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
