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
using MIS.Services.Report;
using ExceptionHandler;
using MIS.Models.Payment.HDSP;
using ClosedXML.Excel;


namespace MIS.Controllers.Report
{
    public class PaymentReportController : BaseController
    {
        CommonFunction common = new CommonFunction();
        public ActionResult GetDetailPaymentReport(string dist, string vdc, string Ward, string Bank,string tranch)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            PSPUploadLog objPSPMod = new PSPUploadLog();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (Bank.ConvertToString() != string.Empty)
                paramValues.Add("BANK_CD", Bank);
           
            if (tranch.ConvertToString() != string.Empty)
                paramValues.Add("Installment", tranch);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            objPSPMod.Districtcd = dist;
            objPSPMod.VDCcd = vdc;
            objPSPMod.Ward = Ward;
            objPSPMod.Bankcd = Bank;
            objPSPMod.Tranche = tranch;

            Session["objPSPMod"] = objPSPMod;
            dt = service.getdetailPaymentReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("PaymentGrantDetailRtp", objPSPMod);
        }

        //start
        //Export PSP  Payment Report

        public DataTable GetDetailPaymentReport1(string dist, string vdc, string Ward, string Bank, string tranch)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            PSPUploadLog objPSPMod = new PSPUploadLog();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (Bank.ConvertToString() != string.Empty)
                paramValues.Add("BANK_CD", Bank);

            if (tranch.ConvertToString() != string.Empty)
                paramValues.Add("Installment", tranch);

            DataTable dt = new DataTable();
            dt = service.getdetailPaymentReport(paramValues);
            dt.Columns.Remove("BANK_CD");
            dt.Columns.Add("DEPOSIT");

            foreach(DataRow dr in dt.Rows)
            {
                if (dr["Installment"].ConvertToString() == "1")
                {
                    dr["DEPOSIT"] = "50,000";
                }
                else if (dr["Installment"].ConvertToString() == "2")
                {
                    dr["DEPOSIT"]  = "1,50,000";
                }
                else
                {
                    dr["DEPOSIT"] = "1,00,000";
                }
                //dt.Rows.Add(dr);
            }


            dt.Columns["NRA_DEFINED_CD"].ColumnName = "PA";
            dt.Columns["BENEFICIARY_NAME"].ColumnName = "Beneficiary(NRA)";
            dt.Columns["RECIPIENT_NAME"].ColumnName = "Beneficiary(Bank)";
            dt.Columns["vdc_mun"].ColumnName = "VDC/MUN";
            dt.Columns["WARD_NO"].ColumnName = "Ward";
            dt.Columns["bank"].ColumnName = "Bank";
            dt.Columns["bank_branch"].ColumnName = "Branch";
            dt.Columns["ACCOUNT_NUMBER"].ColumnName = "AC#";
            dt.Columns["DEPOSIT"].ColumnName = "Deposit";
            dt.Columns["Deposited_dt"].ColumnName = "Deposit Date";

            DataView dv = new DataView(dt);
            DataTable dt1 = dv.ToTable(true, "PA", "Beneficiary(NRA)", "Beneficiary(Bank)", "district", "VDC/MUN", "Ward", "Bank", "Branch" , "Installment", "AC#", "Deposit", "Deposit date");
            return dt1;
        }
        public ActionResult ExportPSPPaymentReport(string dist, string vdc, string Ward, string Bank, string tranch)
        {
            DataTable dt = GetDetailPaymentReport1(dist, vdc, Ward, Bank, tranch);
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= PSP_Payment_Detail_Rpt.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("PaymentReport", "Report");
        }

        // end
        public ActionResult GetSummaryPaymentReport(string dist, string vdc, string Ward)//, string install, string bank, string branch)
        {
            PaymentReportServices service = new PaymentReportServices();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getSummaryPaymentReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("GetSummaryPaymentRpt");
        }
        public ActionResult GetGrantSummaryPaymentReport(string dist, string vdc, string Ward, string Bank,string branch)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            PSPUploadLog objPSPMod = new PSPUploadLog();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (Bank.ConvertToString() != string.Empty)
                paramValues.Add("Bank", Bank);
            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);

            objPSPMod.Districtcd = dist;
            objPSPMod.VDCcd = vdc;
            objPSPMod.Ward = Ward;
            objPSPMod.Bankcd = Bank;
            objPSPMod.Branchcd = branch;
            Session["objPSPMod"] = objPSPMod;
            ViewData["objPSPMod"] = Session["objPSPMod"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getGranctSummaryPaymentReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("GetGrantSummaryPaymentRpt", objPSPMod);
        }
        public ActionResult GetGrantSummaryPaymentRpt(PSPUploadLog objPSPMod)
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "PSP Payment Summary Report";
            return View(objPSPMod);
        }
        public ActionResult PaymentGrantDetailRtp(PSPUploadLog objPSPMod)
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "PSP Payment Detail Report";
            return View(objPSPMod);
        }
        public ActionResult GetSummaryPaymentRpt()
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            return View();
        }
        public ActionResult GetBankClaimSumReport(string dist, string bank, string branch, string approved, string fiscalyr, string Quarter, string installment)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            string startdate = "";
            string enddate = "";
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));

            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);

            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);

            if (Quarter.ConvertToString() != string.Empty)
                paramValues.Add("Quarter", Quarter);

            DataTable dt = new DataTable();
            dt = service.getBankClaimSumReport(paramValues);

            Session["StartDate"] = startdate;
            Session["EndDate"] = enddate;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GetBankClaimSummaryRpt");
        }

        public ActionResult GetClaimSumReportBranchWise(string dist, string bank, string branch, string approved, string fiscalyr, string Quarter, string installment)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            string startdate = "";
            string enddate = "";
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));
            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);



            if (fiscalyr.ConvertToString() != string.Empty && fiscalyr.ConvertToString().ToUpper() != "NULL")
                paramValues.Add("fiscalyr", fiscalyr);

            if (Quarter.ConvertToString() != string.Empty)
                paramValues.Add("Quarter", Quarter);


            DataTable dt = new DataTable();
            dt = service.getClaimSumReportBranchWise(paramValues);



            Session["StartDate"] = startdate;
            Session["EndDate"] = enddate;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GetClaimSummaryRptBranchWise");
        }

        public ActionResult GetClaimSummaryRptBranchWise()
        {
            DataTable dt = null;
            string startdate = Session["StartDate"].ToString();
            string enddate = Session["EndDate"].ToString();
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.date1 = startdate;
            ViewBag.date2 = enddate;
            ViewBag.ReportTitle = "Bank Claim Summary Report";
            ViewData["d1"] = startdate;
            ViewData["d2"] = enddate;
            return View();
        }

        public ActionResult GetBankClaimSummaryRpt()
        {
            DataTable dt = null;
            string startdate = Session["StartDate"].ToString();
            string enddate = Session["EndDate"].ToString();
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.date1 = startdate;
            ViewBag.date2 = enddate;
            ViewBag.ReportTitle = "Bank Claim Summary Report";
            ViewData["d1"] = startdate;
            ViewData["d2"] = enddate;
            return View();
        }

        [HttpPost]
        public ActionResult ExportExcel()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/ClaimReport/") + "BankClaimList_" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/ClaimReport/") + "BankClaimList_" + usercd + ".xls";
            }

            string html = RenderPartialClaimViewToString("~/Views/HtmlReport/_BankClaimSummaryReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Bank Claim List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "BankClaimList.xls");
        }
        protected string RenderPartialClaimViewToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["Results"] = dt;
                ViewData["d1"] = Session["StartDate"].ToString();
                ViewData["d2"] = Session["EndDate"].ToString();


                rptParams = (HtmlReport)Session["BankClaimParams"];
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

        public ActionResult GetBankClaimDtlReport(string bank, string branch, string fiscalyr, string Quarter)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            
            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));
           
            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);
           
            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);

            if (Quarter.ConvertToString() != string.Empty)
                paramValues.Add("Quarter", Quarter);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getBankClaimDtlReport(paramValues);
           
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GetBankClaimdtlRpt");
        }
       
        //Export BankclaimReport
        public DataTable GetBankClaimDtlReport1(string bank, string branch, string fiscalyr, string Quarter)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));
            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);
            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);
            if (Quarter.ConvertToString() != string.Empty)
                paramValues.Add("Quarter", Quarter);

            DataTable dt = new DataTable();
            dt = service.getBankClaimDtlReport(paramValues);
            dt.Columns.Remove("BANK_CD");
            dt.Columns.Remove("BRANCH_STD_CD");
            dt.Columns.Remove("TRANSACTION_DATE_LOC");
            dt.Columns.Remove("AC_ACTIVATED_DT");
            dt.Columns.Remove("RECIPIENT_NAME");
            dt.Columns.Remove("DISTRICT_CD");
            dt.Columns.Remove("vdc_mun_cd");
            dt.Columns.Remove("HOUSE_OWNER_ID");

            dt.Columns["Bank_name"].ColumnName = "PSP";
            dt.Columns["Branch_Name"].ColumnName = "Branch";
            dt.Columns["IS_DIST_HEADQTR"].ColumnName = "HQ";
            dt.Columns["FISCAL_YR"].ColumnName = "F/Y";
            dt.Columns["QUARTER"].ColumnName = "Trimester";
            dt.Columns["AC_ACTIVATED_DT_LOC"].ColumnName = "AC ACTIVATED DATE";
            dt.Columns["BENEF_NAME"].ColumnName = "Beneficiary Name (MIS)";
            dt.Columns["DISTRICT"].ColumnName = "District";
            dt.Columns["VDC_MUN"].ColumnName = "VDC/MUN";
            dt.Columns["WARD_NO"].ColumnName = "Ward No";
            dt.Columns["NRA_DEFINED_CD"].ColumnName = "PA_Num";
            dt.Columns["ACCOUNT_NUMBER"].ColumnName = "ACCOUNT NO";
            dt.Columns["TRANCH"].ColumnName = "Installment";
            dt.Columns["CR_AMOUNT"].ColumnName = "DEPOSIT(NRS.)";
            dt.Columns["IS_ATM_CARD_ISSUED"].ColumnName = "Card Issued";
            dt.Columns["ATM_CARD_ISSUED_DATE"].ColumnName = "Card Issued date";
   

            DataView dv = new DataView(dt);
            DataTable dt1 = dv.ToTable(true, "PSP", "Branch", "HQ", "F/Y", "Trimester", "AC ACTIVATED DATE", "Beneficiary Name (MIS)", "District"
                                            , "VDC/MUN", "Ward No", "PA_Num", "ACCOUNT NO", "Installment", "DEPOSIT(NRS.)", "Card Issued", "Card Issued date","Remarks");
            return dt1;
        }

        public ActionResult ExportBankClaimReport(string bank, string branch, string fiscalyr, string Quarter)
        {
            DataTable dt = GetBankClaimDtlReport1(bank, branch, fiscalyr, Quarter);
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= BankClaimDetailReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("BankClaimReport", "Report");
        }
        public string getquarter(string date)
        {
            return null;
        }
        public ActionResult GetBankClaimdtlRpt()
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Bank Claim Detail Report";
            return View();
        }

        public ActionResult GetPSPUploadLog(string fiscalyr)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            
            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);

            DataTable dt = new DataTable();
            dt = service.getPSPUploadReport(paramValues);

            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GetPSPUploadLogRpt");
        }
        public ActionResult GetPSPUploadLogRpt()
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            return View();
        }

        [HttpPost]
        public ActionResult PSPUploadLog()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/PSPUpload/") + "PSPUploadLog_" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/PSPUpload/") + "PSPUploadLog_" + usercd + ".xls";
            }

            string html = RenderPartialPSPUploadToString("~/Views/HtmlReport/_PSPUploadLogReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("PSP Upload List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "PSPUploadLog.xls");
        }
        protected string RenderPartialPSPUploadToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["Results"] = dt;

                rptParams = (HtmlReport)Session["PSPUplaodParams"];
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

        public ActionResult PSPBankUploadLog(string bank, string branch, string fiscalyr)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            PSPUploadLog objPSPMod = new PSPUploadLog();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            //if (dist.ConvertToString() != string.Empty)
            //    paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));           

            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));
            //if (branch.ConvertToString() != string.Empty)
            //paramValues.Add("branch", common.GetCodeFromDataBase(branch,"nhrs_bank_branch","BRANCH_STD_CD"));
            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);
            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);

            DataTable dt = new DataTable();
            dt = service.getPSPBankUploadReport(paramValues);
            objPSPMod.Bankcd = bank;
            objPSPMod.Branchcd = branch;
            objPSPMod.FiscalYr = fiscalyr;
            Session["objPSPMod"] = objPSPMod;
            ViewData["objPSPMod"] = Session["objPSPMod"];
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GetPSPBankUploadLogRpt", objPSPMod);
        }
        public ActionResult GetPSPBankUploadLogRpt(PSPUploadLog objPSPMod)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewBag.ReportTitle = "PSP Upload Log Report";
            ViewData["Results"] = dt;
            return View(objPSPMod);
        }

        [HttpPost]
        public ActionResult ExportPSPBankUploadLog()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/PSPUpload/") + "PSPBankUploadLog" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/PSPUpload/") + "PSPBankUploadLog" + usercd + ".xls";
            }

            string html = RenderPartialPSPBankUploadToString("~/Views/HtmlReport/_PSPBankUploadLogReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("PSP Upload List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "PSPBankUploadLog.xls");
        }
        protected string RenderPartialPSPBankUploadToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            PSPUploadLog rptParams = new PSPUploadLog();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["Results"] = dt;

                rptParams = (PSPUploadLog)Session["objPSPMod"];
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
        public ActionResult PSPBankFiscalYrUploadLog(string bank, string fiscalyr)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            PSPUploadLog objPSPMod = new PSPUploadLog();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));

            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);

            DataTable dt = new DataTable();
            dt = service.getPSPBankFiscalYrUploadReport(paramValues);
            //objPSPMod.Bankcd = bank;

            //objPSPMod.FiscalYr = fiscalyr;

            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GetPSPBankFiscalUploadLogRpt");
        }
        public ActionResult GetPSPBankFiscalUploadLogRpt()
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            return View();
        }

        public ActionResult ExportPSPBankFiscalYrUploadLog()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/PSPUpload/") + "PSPBankFiscalYrUploadLog" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/PSPUpload/") + "PSPBankFiscalYrUploadLog" + usercd + ".xls";
            }

            string html = RenderPartialPSPBankFiscalYrUploadToString("~/Views/HtmlReport/_PSPBankFiscalYrUploadLogReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("PSP Upload List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "PSPBankFiscalYrUploadLog.xls");
        }
        protected string RenderPartialPSPBankFiscalYrUploadToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            PSPUploadLog rptParams = new PSPUploadLog();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["Results"] = dt;

                rptParams = (PSPUploadLog)Session["objPSPMod"];
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
        public ActionResult GetDetailInstallmentReport(string dist, string vdc, string Ward, string install, string bank, string branch)
        {
            PaymentReportServices service = new PaymentReportServices();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (install.ConvertToString() != string.Empty)
                paramValues.Add("install", install);
            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));
            if (branch.ConvertToString() != string.Empty)
                paramValues.Add("branch", branch);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getdtlInstallmentReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("GetInstallmentdtlRpt");
        }
        public ActionResult GetInstallmentdtlRpt()
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            return View();
        }
        public ActionResult GetDTCOGrantSummaryReport(string dist, string vdc, string Ward, string bank, string quarter, string fiscalyr)
        {
            PaymentReportServices service = new PaymentReportServices();
            NameValueCollection paramValues = new NameValueCollection();
            //PSPUploadLog objPSPMod = new PSPUploadLog();
            PSPUploadLog ObjDtco = new PSPUploadLog();
            ObjDtco.Districtcd = dist;
            ObjDtco.VDCcd = vdc;
            ObjDtco.Ward = Ward;
            ObjDtco.Bankcd = bank;
            ObjDtco.FiscalYr = fiscalyr;
            //ObjDtco.quarter = quarter;
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", bank);
            if (quarter.ConvertToString() != string.Empty)
                paramValues.Add("quarter", quarter);
            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);
            Session["ObjDtco"] = ObjDtco;
            ViewData["ObjDtco"] = Session["ObjDtco"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getDTCOGranctSummaryReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("GetDTCOGrantSummaryRpt", ObjDtco);
        }
        public ActionResult GetDTCOGrantSummaryRpt(PSPUploadLog ObjDTCO)
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "DTCO Grant Summary Report";
            return View(ObjDTCO);
        }
        public ActionResult ExportDTCOGrantSumExcel()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/DTCOReport/") + "DTCOUploadLog" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/DTCOReport/") + "DTCOUploadLog" + usercd + ".xls";
            }

            string html = RenderPartialDTCOUploadToString("~/Views/HtmlReport/_GrantDTCOSummaryReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("DTCO Upload List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "DTCOUploadLog.xls");
        }
        protected string RenderPartialDTCOUploadToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            PSPUploadLog rptParams = new PSPUploadLog();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["Results"] = dt;

                rptParams = (PSPUploadLog)Session["ObjDtco"];
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
        public ActionResult ExportPaymnetDtlExcel()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/PaymentReport/") + "GrantPaymentDtl" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/PaymentReport/") + "GrantPaymentDtl" + usercd + ".xls";
            }

            string html = RenderPartialPaymentDtlToString("~/Views/HtmlReport/_PaymentDetailReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Grant Payment List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "GrantPaymentdtl.xls");
        }
        protected string RenderPartialPaymentDtlToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            PSPUploadLog rptParams = new PSPUploadLog();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];

                ViewData["Results"] = dt;

                rptParams = (PSPUploadLog)Session["objPSPMod"];
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
        public ActionResult GetDTCODetailReport(string dist, string vdc, string Ward, string bank, string quarter, string fiscalyr, string pa, string tranche)
        {
            PaymentReportServices service = new PaymentReportServices();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            //if (installment.ConvertToString() != string.Empty)
            //    paramValues.Add("installment", installment);
            if (bank.ConvertToString() != string.Empty)
                paramValues.Add("bank", common.GetCodeFromDataBase(bank, "NHRS_BANK", "BANK_CD"));
            if (fiscalyr.ConvertToString() != string.Empty)
                paramValues.Add("fiscalyr", fiscalyr);
            if (quarter.ConvertToString() != string.Empty)
                paramValues.Add("quarter", quarter);
            if (pa.ConvertToString() != string.Empty)
                paramValues.Add("PA", pa);
            if (tranche.ConvertToString() != string.Empty)
                paramValues.Add("Installment", tranche);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getDTCOdetailReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("DTCODetailRtp");
        }
        public ActionResult DTCODetailRtp()
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "DTCO Grant Detail Report";
            return View();
        }
        public ActionResult ExportDTCODtlLog()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/DTCOReport/") + "DTCOUploadLog" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/DTCOReport/") + "DTCOUploadLog" + usercd + ".xls";
            }

            string html = RenderPartialDTCODtlToString("~/Views/HtmlReport/_DTCODetailReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("DTCO Upload List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "DTCODtlUploadLog.xls");
        }
        protected string RenderPartialDTCODtlToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            PSPUploadLog rptParams = new PSPUploadLog();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];

                ViewData["Results"] = dt;

                //rptParams = (PSPUploadLog)Session["objPSPMod"];
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
        public ActionResult GetDTCOReconcileReport(string dist, string vdc, string Ward, string installment, string pa, string fy, string trimester, string bank_cd)
        {
            PaymentReportServices service = new PaymentReportServices();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (installment.ConvertToString() != string.Empty)
                paramValues.Add("installment", installment);
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (installment.ConvertToString() != string.Empty)
                paramValues.Add("installment", installment);
            if (pa.ConvertToString() != string.Empty)
                paramValues.Add("pa", pa);
            if (fy.ConvertToString() != string.Empty)
                paramValues.Add("fiscalYear", fy);
            if (trimester.ConvertToString() != string.Empty)
                paramValues.Add("trimester", trimester);
            if (bank_cd.ConvertToString() != string.Empty)
                paramValues.Add("bank_cd", bank_cd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getDTCOReconcileReport(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;

            objreport.CurrentWard = Ward;
            objreport.Installment = installment;
            objreport.PA = pa;
            objreport.FY = fy;
            objreport.Trimester = trimester;
            objreport.BankCd = bank_cd;
            Session["DtcoReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("DTCOReconcileRtp", objreport);
        }
        public ActionResult DTCOReconcileRtp(HtmlReport objreport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Reconcile Detail Report";
            ViewData["Results"] = dt;
            return View(objreport);
        }

        public ActionResult ExportDTCOReconcileDetailReport(string dist, string vdc, string Ward, string installment, string pa, string fy, string trimester, string bank_cd) //export Detail report to excel 
        {
            GetDTCOReconcileReport(dist, vdc, Ward, installment, pa, fy, trimester, bank_cd);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["DtcoReportParams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "DTCOReconcileDetailreport" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "DTCOReconcileDetailreport" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforDTCOReconsile("~/Views/HTMLReport/_DTCOReconcileReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Reconcile Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "DTCOReconcileDetailreport.xls");


        }

        protected string RenderPartialViewToStringforDTCOReconsile(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["DtcoReportParams"];
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
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


        public ActionResult GetDTCOReconcileSumReport(string dist, string vdc, string Ward, string installment, string pa, string fy, string trimester,string bank_name, string bank_cd)
        {
            PaymentReportServices service = new PaymentReportServices();
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
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (installment.ConvertToString() != string.Empty)
                paramValues.Add("installment", installment);

            if (pa.ConvertToString() != string.Empty)
                paramValues.Add("pa", pa);
            if (fy.ConvertToString() != string.Empty)
                paramValues.Add("fiscalYear", fy);
            if (trimester.ConvertToString() != string.Empty)
                paramValues.Add("trimester", trimester);
            if (bank_cd.ConvertToString() != string.Empty)
                paramValues.Add("bank_cd", bank_cd);
            if (bank_name.ConvertToString() != string.Empty)
                paramValues.Add("bank_name", bank_name);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            objreport.BankCd = bank_cd;
            objreport.BankName = bank_name;
            dt = service.getDTCOReconcileSumReport(paramValues);
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            return RedirectToAction("DTCOReconcileSumRtp", objreport);
        }
        public ActionResult DTCOReconcileSumRtp(HtmlReport objreport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Reconcile Summary Report";
            return View(objreport);
        }
        public ActionResult ExportDTCOReconcileLog()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/DTCOReport/") + "DTCOReconcileLog" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/DTCOReport/") + "DTCOReconcileLog" + usercd + ".xls";
            }

            string html = RenderPartialDTCOReconcileToString("~/Views/HtmlReport/_DTCOReconcileReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("DTCO Reconcile List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "DTCOReconcileLog.xls");
        }
        protected string RenderPartialDTCOReconcileToString(string viewName)
        {


            DataTable dtbl = new DataTable();
            PSPUploadLog rptParams = new PSPUploadLog();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];

                ViewData["Results"] = dt;

                //rptParams = (PSPUploadLog)Session["objPSPMod"];
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
