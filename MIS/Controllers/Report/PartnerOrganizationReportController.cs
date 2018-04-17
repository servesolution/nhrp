using EntityFramework;
using MIS.Services.Core;
using System.Data;
using MIS.Services.Report;
using System.Web.Mvc;
using MIS.Models.Report;
using MIS.Services.PartnerOrganization;
using System.Data.OracleClient;
using ExceptionHandler;
using System;
using MIS.Models.Setup;
using System.IO;
namespace MIS.Controllers.Report
{
    public class PartnerOrganizationReportController : Controller
    {
        //
        // GET: /PartnerOrganizationReport/
        public ActionResult PartnerOrganizationSuportDetail(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Partner Organization Support Detail Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        public ActionResult GetPartnerOrganizationSupportReport(string dist, string vdc, string ward, string pa, string benefType) //PO support report
        {
            CommonFunction common = new CommonFunction();

            PartnerOrganizationService objPOService = new PartnerOrganizationService();
            NameValueCollection paramValues = new NameValueCollection();
            
            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
           

            if (pa.ConvertToString() != string.Empty && paramValues["pa"].ConvertToString() != "undefined")
                paramValues.Add("pa", pa);

            if (benefType.ConvertToString() != string.Empty && paramValues["benefType"].ConvertToString() != "undefined")
                paramValues.Add("benefType", benefType);


            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objPOService.GetPartnerOrganizationSupportReport(paramValues);
 
            if (dt != null)
            {

                Session["DetailResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward;
                
                objReport.PA = pa;
                objReport.BenefType = benefType;


            }
            return RedirectToAction("PartnerOrganizationSuportDetail", objReport);
        }


        public ActionResult OrganizationSuportSummary(HtmlReport obj)
        {
            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Partner Organization Support Summary Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        public ActionResult GetOrganizationSupportReport(string dist, string vdc, string ward) //PO support report
        {
            CommonFunction common = new CommonFunction();

            PartnerOrganizationService objPOService = new PartnerOrganizationService();
            NameValueCollection paramValues = new NameValueCollection();

            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;


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

            dt = objPOService.GetOrganizationSummaryReport(paramValues);
             
            if (dt != null)
            {

                Session["DetailResults"] = dt;
                objReport.VDC = vdc;
                objReport.Ward = ward;


            }
            return RedirectToAction("OrganizationSuportSummary", objReport);
        }

        //
        public ActionResult PartnerOrganizationDetail(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "PartnerOrganization Detail Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        public ActionResult GetPartnerOrganizationReport(string dist, string vdc, string ward, string donor, string pa )
        {
            CommonFunction common = new CommonFunction();

            PartnerOrganizationService objPOService = new PartnerOrganizationService();
            NameValueCollection paramValues = new NameValueCollection();

            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            objReport.Donor = donor;

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (donor.ConvertToString() != string.Empty && paramValues["donor"].ConvertToString() != "undefined")
                paramValues.Add("donor", donor);
            if (pa.ConvertToString() != string.Empty && paramValues["pa"].ConvertToString() != "undefined")
                paramValues.Add("pa", pa);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            } 
            DataTable dt = new DataTable();

            dt = objPOService.GetPartnerOrganizationReport(paramValues); 

             
            if (dt != null)
            {

                Session["DetailResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward;
                objReport.Donor = donor;
                objReport.PA = pa;

            }
            return RedirectToAction("PartnerOrganizationDetail", objReport);
        }

        //summaryPO
        public ActionResult PartnerOrganizationSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "PartnerOrganization Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(obj);
        }

        public ActionResult GetPartnerOrganizationSummaryReport(string dist, string vdc, string ward, string donor)
        {
            CommonFunction common = new CommonFunction();

            PartnerOrganizationService objPOService = new PartnerOrganizationService();
            NameValueCollection paramValues = new NameValueCollection();

            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            objReport.Donor = donor;

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (donor.ConvertToString() != string.Empty && paramValues["donor"].ConvertToString() != "undefined")
                paramValues.Add("donor", donor);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            DataTable dt = new DataTable();

            dt = objPOService.GetPartnerOrganizationSummaryReport(paramValues);
            if (dt != null)
            {
                Session["SummaryResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward;
                objReport.Donor = donor;
            }
            return RedirectToAction("PartnerOrganizationSummary", objReport);
        }

        public ActionResult ExportSummaryToExcel(string dist, string vdc, string ward, string panum)
        {
            NameValueCollection paramValues = new NameValueCollection();
            CommonFunction common = new CommonFunction();
            DataTable dt = new DataTable();
            PartnerOrganizationService objPOService = new PartnerOrganizationService();
            HtmlReport objReport = new HtmlReport();

            try
            {
                if (dist.ConvertToString() != string.Empty)
                    paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
                if (vdc.ConvertToString() != string.Empty)
                    paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
                if (ward.ConvertToString() != string.Empty)
                    paramValues.Add("ward", ward);

                dt = objPOService.GetOrganizationSummaryReport(paramValues);

                objReport.VDC = vdc;
                objReport.Ward = ward;

                Session["DetailResults"] = dt;
                Session["ParamModel"] = objReport;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/ClaimReport/") + "BankClaimListReport" + usercd + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/ClaimReport/") + "BankClaimListReport" + usercd + "( District " + dist + " ).xls";
            }

            string html = RenderPartialClaimViewToString("~/Views/HtmlReport/_OrganizationSupportHtmlSummary.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Beneficiary Allocation Summary Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "AllocationSummaryReport.xls");
        }

        protected string RenderPartialClaimViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport objReport = new HtmlReport();

            if ((DataTable)Session["DetailResults"] != null)
            {
                DataTable dt = (DataTable)Session["DetailResults"];

                ViewData["DetailResults"] = dt;


                objReport = (HtmlReport)Session["ParamModel"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    ViewData.Model = objReport;
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }

    }
}
