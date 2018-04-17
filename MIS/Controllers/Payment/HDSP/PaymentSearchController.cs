using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models.Enrollment;
using MIS.Services.Core;
using MIS.Services.Enrollment;
using System.Data;
using MIS.Models.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using System.Web.Routing;
using EntityFramework;
using MIS.Services.Payment.HDSP;
using MIS.Models.Report;
using System.IO;
using MIS.Models.Setup;
using MIS.Models.Payment.HDSP;
using ClosedXML.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using MIS.Models.Security;
using MIS.Services.Setup;
using MIS.Authentication;


namespace MIS.Controllers.Payment.HDSP
{
    public class PaymentSearchController : BaseController
    {
        //
        // GET: /PaymentSearch/
        CommonFunction commonFC = new CommonFunction();
        public ActionResult EnrollmentSearchBankList()
        {

            CheckPermission();
            EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            CommonFunction common = new CommonFunction();
            //DataTable dataresult = new DataTable();
            //dataresult = (DataTable)Session["dtBankList"];
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_WardNo"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("", "");
            //ViewData["ResearchEnrollment"] = TempData["ResearchEnrollment"];
            //string datacount = (dataresult.Rows.Count).ConvertToString();
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "You have Successfully Approved " + datacount + " data. ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["Message"] = "Approval Fialed ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "WrongUser")
                {
                    ViewData["Message"] = "This user is not Authorized to approve data.";
                }
                Session["ApprovedMessage"] = "";
            }

            return View();
        }
        [HttpPost]
        public ActionResult EnrollmentSearchBankList(FormCollection fc)
        {

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            EnrollmentSearch objEnrollment = new EnrollmentSearch();

            objEnrollment.DistrictCd = fc["DistrictCd"].ConvertToString();// GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            objEnrollment.VDCMun = commonFC.GetCodeFromDataBase(fc["VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objEnrollment.WardNo = fc["WardNo"].ConvertToString();
            objEnrollment.InstallationCd = fc["ddl_Installation"].ConvertToString();
            //if (fc["chkReExport"].Contains("true"))
            //{
            //    List<string> exportedFiles = Directory.GetFiles(Server.MapPath("~/Excel/EnrollmentImportExport/"), "*.xls")
            //                             .Select(path => Path.GetFileName(path))
            //                             .ToList();
            //    DataTable dtbl = new DataTable();
            //    DataRow row;
            //    dtbl.Columns.Add("FILE_NAME", typeof(String));
            //    foreach (var i in exportedFiles)
            //    {
            //        if (i.Contains(fc["hdDistrictVDC"]))
            //        {
            //            row = dtbl.NewRow();
            //            row["FILE_NAME"] = i.ToString();
            //            dtbl.Rows.Add(row);
            //        }
            //    }
            //    ViewData["dtexportedFiles"] = dtbl;
            //    ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            //    return PartialView("~/Views/EnrollmentImportExport/_FileList.cshtml", objEnrollment);
            //}
            if (objEnrollment.InstallationCd == "1")
            {
                result = enrollService.GetPaymentSearchList(objEnrollment);

                ViewData["dtBankListresult"] = result;
                Session["dtBankListresult"] = result;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();
                return PartialView("~/Views/HDSPPaymentProcess/_PaymentSearchList.cshtml", objEnrollment);

            }
            else if (objEnrollment.InstallationCd == "2")
            {
                GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
                result = objpaymentservice.getFirstinspectedpaymentList(objEnrollment);

                ViewData["dtBankListresult"] = result;

                Session["dtBankListresult"] = result;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();

                return PartialView("~/Views/HDSPPaymentProcess/_EnrollInspectedBankList.cshtml");
            }
            // for 3rd installment
            else
            {
                GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
                result = objpaymentservice.getSecondinspectedpaymentList(objEnrollment);

                ViewData["dtBankListresult"] = result;

                Session["dtBankListresult"] = result;
                ViewData["Message"] = fc["hdnMessage"].ConvertToString();

                return PartialView("~/Views/HDSPPaymentProcess/_EnrollSecInspectBankList.cshtml");
            }

        }

        #region Approve Inspection Payment
        //FIRST PAYMENT
        //public ActionResult ApproveEnrollPayment(string p, FormCollection fc)
        public ActionResult ApproveEnrollPayment()
        {
            NameValueCollection paramValues = new NameValueCollection();
            //GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            EnrollmentSearch objEnrollment = new EnrollmentSearch();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string session_id = String.Empty;
            DataTable dataresult = new DataTable();
            dataresult = (DataTable)Session["dtBankListresult"];
            string District = dataresult.Rows[0]["DIST_CD"].ToString();
            string VDC = dataresult.Rows[0]["VDC_CD"].ToString();
            string Ward = dataresult.Rows[0]["WARD"].ToString();
            if (District.ConvertToString() != string.Empty)
                paramValues.Add("dist", District);
            if (VDC.ConvertToString() != string.Empty)
                paramValues.Add("vdc", VDC);
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", Ward);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            //if (p != null)
            //{
            //    rvd = QueryStringEncrypt.DecryptString(p);
            //    if (rvd["districtid"] != null)
            //    {
            //        objEnrollment.DistrictCd = rvd["districtid"].ConvertToString();

            //    }
            //    if (rvd["vdcmuncd"] != null)
            //    {
            //        objEnrollment.VDCMun = rvd["vdcmuncd"].ConvertToString();

            //    }
            //    if (rvd["wardno"] != null)
            //    {
            //        objEnrollment.WardNo = rvd["wardno"].ConvertToString();

            //    }
            //}
            string user_cd = SessionCheck.getSessionUserCode();
            if (user_cd == "2502")
            {
                QueryResult success = new QueryResult();
                string excout = string.Empty;
                string datacount = (dataresult.Rows.Count).ConvertToString();
                Session["ListCount"] = datacount;
                DataTable dataresult2 = enrollService.getapprovePaymentListTEST(paramValues);
                Session["SummaryResults"] = dataresult2;
                string filename = ExportApproveFirstPaymentList(dataresult);
                string File_Destination = Session["file_Destination"].ConvertToString();
                success = enrollService.ApproveEnrollmentPayment(dataresult, filename, File_Destination);
                //success = enrollService.ApproveEnrollmentPayment(session_id, out excout, out TotalTargeted);
                if (success.IsSuccess)
                {
                    ViewData["Message"] = "Success";
                }
                else
                {
                    ViewData["Message"] = "Failed";
                }
            }
            else
            {
                ViewData["Message"] = "WrongUser";
            }

            Session["ApprovedMessage"] = ViewData["Message"];
            return RedirectToAction("EnrollmentSearchBankList");
        }

        public string ExportApproveFirstPaymentList(DataTable dtbl)
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            string district = dtbl.Rows[0]["DistrictName"].ToString();
            string vdc = dtbl.Rows[0]["VDC_CD"].ToString();
            string ward = dtbl.Rows[0]["WARD"].ToString();
            string date_dt = DateTime.UtcNow.ConvertToString();
            string real_dt = date_dt.Replace("/", "-");
            string final_dt = real_dt.Replace(" ", "_");
            string date = final_dt.Replace(":", "-");
            //string date = DateTime.UtcNow.ConvertToString("dd-MMM-yyyy");
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "FirstInstallment_" + district + "_" + vdc + "_" + ward + "_" + usercd + "_" + date + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "FirstInstallment_" + district + "_" + vdc + "_" + ward + "_" + date + ".xls";
            }
            Session["file_Destination"] = filePath;
            string filename = Path.GetFileName(filePath).Replace(" ", "_").Split('\\').Last();
            string html = RenderPartialEnrolledViewToString("~/Views/HDSPPaymentProcess/_ApprovedEnrollPaymentList.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Enrolment Payment List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);

            return filename;
            //return File(filePath, "application/msexcel", "ApprovePaymentList.xls");
        }
        protected string RenderPartialEnrolledViewToString(string viewName)
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
        //SECOND PAYMENT
        public ActionResult ApproveInspectedPaymentList()
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable dataresult = new DataTable();
            dataresult = (DataTable)Session["dtBankListresult"];
            QueryResult success = new QueryResult();
            string TotalTargeted = string.Empty;
            string excout = string.Empty;
            string datacount = (dataresult.Rows.Count).ConvertToString();
            Session["ListCount"] = datacount;
            string user_cd = SessionCheck.getSessionUserCode();
            if (user_cd == "2502")
            {
                string filename = ExportApproveEnrollPaymentList(dataresult);
                string File_Destination = Session["file_Destination"].ConvertToString();
                success = enrollService.ApproveInspecctedPayment(dataresult, filename, File_Destination);
                if (success.IsSuccess)
                {
                    ViewData["Message"] = "Success";
                }
                else
                {
                    ViewData["Message"] = "Failed";
                }
            }
            else
            {
                ViewData["Message"] = "WrongUser";
            }

            Session["ApprovedMessage"] = ViewData["Message"];
            return RedirectToAction("EnrollmentSearchBankList");
            //return null;
        }
        public string ExportApproveEnrollPaymentList(DataTable dtbl)
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            string district = dtbl.Rows[0]["DistrictName"].ToString();
            string vdc = dtbl.Rows[0]["VDC_CD"].ToString();
            string ward = dtbl.Rows[0]["WARD"].ToString();
            string date_dt = DateTime.UtcNow.ConvertToString();
            string real_dt = date_dt.Replace("/", "-");
            string final_dt = real_dt.Replace(" ", "_");
            string date = final_dt.Replace(":", "-");
            //string date = DateTime.UtcNow.ConvertToString("dd-MMM-yyyy");
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "FirstInspected_" + district + "_" + vdc + "_" + ward + "_" + usercd + "_" + date + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "FirstInspected_" + district + "_" + vdc + "_" + ward + "_" + date + ".xls";
            }
            Session["file_Destination"] = filePath;
            string filename = Path.GetFileName(filePath);
            string html = RenderPartialInspectedViewToString("~/Views/HDSPPaymentProcess/_EnrollInspectedBankList.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Enrolment Payment List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);

            return filename;
            //return File(filePath, "application/msexcel", "ApprovePaymentList.xls");
        }
        protected string RenderPartialInspectedViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["dtBankListresult"] != null)
            {
                DataTable dt = (DataTable)Session["dtBankListresult"];
                string bankname = Session["bankname"].ConvertToString();
                string branchname = Session["branchname"].ConvertToString();
                string TodayDate = Session["todaydate"].ConvertToString();
                ViewData["dtBankListresult"] = dt;
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
        //THIRD PAYMENT 
        public ActionResult ApproveSecInspectedPaymentList()
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable dataresult = new DataTable();
            dataresult = (DataTable)Session["dtBankListresult"];
            QueryResult success = new QueryResult();
            string TotalTargeted = string.Empty;
            string excout = string.Empty;
            string datacount = (dataresult.Rows.Count).ConvertToString();
            Session["ListCount"] = datacount;
            string user_cd = SessionCheck.getSessionUserCode();
            if (user_cd == "2502")
            {
                string filename = ExportSecondEnrollPaymentList(dataresult);
                string File_Destination = Session["file_Destination"].ConvertToString();
                success = enrollService.ApproveSecInspecctedPayment(dataresult, filename, File_Destination);
                if (success.IsSuccess)
                {
                    ViewData["Message"] = "Success";
                }
                else
                {
                    ViewData["Message"] = "Failed";
                }
            }
            else
            {
                ViewData["Message"] = "WrongUser";
            }
            Session["ApprovedMessage"] = ViewData["Message"];
            return RedirectToAction("EnrollmentSearchBankList");
        }
        #endregion

        public string ExportSecondEnrollPaymentList(DataTable dtbl)
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            string district = dtbl.Rows[0]["DistrictName"].ToString();
            string vdc = dtbl.Rows[0]["VDC_CD"].ToString();
            string ward = dtbl.Rows[0]["WARD"].ToString();
            string date_dt = DateTime.UtcNow.ConvertToString();
            string real_dt = date_dt.Replace("/", "-");
            string final_dt = real_dt.Replace(" ", "_");
            string date = final_dt.Replace(":", "-");
            //string date = DateTime.UtcNow.ConvertToString("dd-MMM-yyyy");
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "SecondInspected_" + district + "_" + vdc + "_" + ward + "_" + usercd + "_" + date + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "SecondInspected_" + district + "_" + vdc + "_" + ward + "_" + date + ".xls";
            }
            Session["file_Destination"] = filePath;
            string filename = Path.GetFileName(filePath);
            string html = RenderSecPartialInspectedViewToString("~/Views/HDSPPaymentProcess/_EnrollSecInspectBankList.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Enrolment Payment List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);

            return filename;
            //return File(filePath, "application/msexcel", "ApprovePaymentList.xls");
        }
        protected string RenderSecPartialInspectedViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["dtBankListresult"] != null)
            {
                DataTable dt = (DataTable)Session["dtBankListresult"];
                string bankname = Session["bankname"].ConvertToString();
                string branchname = Session["branchname"].ConvertToString();
                string TodayDate = Session["todaydate"].ConvertToString();
                ViewData["dtBankListresult"] = dt;
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

        #region Check Permission
        public void CheckPermission()
        {
            PermissionParamService objPermissionParamService = new PermissionParamService();
            PermissionParam objPermissionParam = new PermissionParam();
            ViewBag.EnableEdit = "false";
            ViewBag.EnableDelete = "false";
            ViewBag.EnableAdd = "false";
            ViewBag.EnableApprove = "false";
            try
            {
                objPermissionParam = objPermissionParamService.GetPermissionValue();
                if (objPermissionParam != null)
                {

                    if (objPermissionParam.EnableAdd == "true")
                    {
                        ViewBag.EnableAdd = "true";
                    }
                    if (objPermissionParam.EnableEdit == "true")
                    {
                        ViewBag.EnableEdit = "true";
                    }
                    if (objPermissionParam.EnableDelete == "true")
                    {
                        ViewBag.EnableDelete = "true";
                    }
                    if (objPermissionParam.EnableApprove == "true")
                    {
                        ViewBag.EnableApprove = "true";
                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


        }
        #endregion
        public ActionResult NRATOBANKDownload()
        {
            CheckPermission();
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            //EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            CommonFunction common = new CommonFunction();
            //ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_District"] = common.GetDistricts(objBankMapping.DISTRICT_CD.ConvertToString());
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objBankMapping.VDC_MUN_CD.ConvertToString(), objBankMapping.DISTRICT_CD.ConvertToString());
            ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankMapping.WARD_NO.ConvertToString(), objBankMapping.VDC_MUN_CD);
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
            ViewData["ddl_Installation"] = common.GetInstallation("", "");
            return View();
        }
        [HttpPost]
        public ActionResult NRATOBANKDownload(FormCollection fc)
        {
            GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            NRATOBANKDownload objDownloadSearch = new NRATOBANKDownload();
            DataTable result = new DataTable();
            //NameValueCollection paramValues = new NameValueCollection();
            //if (Session["LanguageSetting"].ConvertToString() == "English")
            //{
            //    paramValues.Add("lang", "E");
            //}
            //else
            //{
            //    paramValues.Add("lang", "N");
            //}
            //if (fc["DISTRICT_CD"].ConvertToString() != string.Empty)
            //    paramValues.Add("DateFrom", fc["DISTRICT_CD"].ConvertToString());
            //if (DateTo.ConvertToString() != string.Empty)
            //    paramValues.Add("DateTo", DateTo);
            objDownloadSearch.VDC_MUN_CD = commonFC.GetCodeFromDataBase(fc["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objDownloadSearch.DISTRICT_CD = fc["DISTRICT_CD"].ConvertToString();
            objDownloadSearch.WARD_NO = fc["WARD_NO"].ConvertToString();
            objDownloadSearch.BANK_CD = commonFC.GetCodeFromDataBase(fc["BANK_CD"].ConvertToString(), "nhrs_bank", "BANK_CD");
            objDownloadSearch.PAYROLL_INSTALL_CD = fc["ddl_Installation"].ConvertToString();
            objDownloadSearch.BANK_BRANCH_CD = fc["BANK_BRANCH_CD"].ConvertToString();
            objDownloadSearch.Transaction_Dt_From = fc["Transaction_Dt_From"].ConvertToString();
            objDownloadSearch.Transaction_Dt_To = fc["Transaction_Dt_To"].ConvertToString();

            result = objpaymentservice.getInspectedFileList(objDownloadSearch);

            ViewData["dtBankListresult"] = result;

            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();

            // return PartialView("~/Views/HDSPPaymentProcess/_InspectedFileList.cshtml");
            return PartialView("~/Views/HDSPPaymentProcess/_PaymentApproveList.cshtml");
            //return null;
        }
        #region Save Download Log
        //Save Save Download Log
        //public ActionResult Savetransaction(string id)
        //{
        //    EnrollmentImportExport enrollService = new EnrollmentImportExport();
        //    RouteValueDictionary rvd = new RouteValueDictionary();
        //    bool rs = false;
        //    string filename = "";
        //    string transac_id = "";
        //    string dis_cd = "";
        //    string vdcmun_cd = "";
        //    string ward_no = "";
        //    string zone_cd = "";
        //    string installation_cd = "";
        //    if (id != null)
        //    {
        //        rvd = QueryStringEncrypt.DecryptString(id);
        //        if (rvd["filename"] != null)
        //        {
        //            filename = rvd["filename"].ConvertToString();

        //        }
        //        if (rvd["transacid"] != null)
        //        {
        //            transac_id = rvd["transacid"].ConvertToString();

        //        }
        //        if (rvd["distcd"] != null)
        //        {
        //            dis_cd = rvd["distcd"].ConvertToString();

        //        }
        //        if (rvd["vdcCd"] != null)
        //        {
        //            vdcmun_cd = rvd["vdcCd"].ConvertToString();

        //        }
        //        if (rvd["ward"] != null)
        //        {
        //            ward_no = rvd["ward"].ConvertToString();

        //        }
        //        if (rvd["zonecd"] != null)
        //        {
        //            zone_cd = rvd["zonecd"].ConvertToString();

        //        }
        //        if (rvd["installationcd"] != null)
        //        {
        //            installation_cd = rvd["installationcd"].ConvertToString();

        //        }
        //    }

        //    rs = enrollService.SaveDownloadLog(filename, transac_id, zone_cd, dis_cd, vdcmun_cd, ward_no, installation_cd);
        //    if (rs)
        //    {
        //        ViewData["Message"] = "Success";
        //    }
        //    else
        //    {
        //        ViewData["Message"] = "Failed";
        //    }
        //    DataTable dataresult = new DataTable();
        //    dataresult = (DataTable)Session["dtBankListresult"];
        //    // string fileList = Session["dtBankListresult"].ConvertToString();
        //    ViewData["dtBankListresult"] = dataresult;
        //    Session["ApprovedMessage"] = ViewData["Message"];
        //    //return RedirectToAction("NRATOBANKDownload");
        //    return PartialView("~/Views/HDSPPaymentProcess/_InspectedFileList.cshtml");
        //}
        #endregion

        public ActionResult TransactionLog()
        {
            CheckPermission();
            //NHRSBankMapping objBankMapping = new NHRSBankMapping();
            NRATOBANKDownload objDownloadSearch = new NRATOBANKDownload();
            CommonFunction common = new CommonFunction();
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            // ViewData["ddl_Bank"] = common.GetBankName("");
            // ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
            ViewData["ddl_Installation"] = common.GetInstallation("", "");
            return View();
        }
        [HttpPost]
        public ActionResult TransactionLog(FormCollection fc)
        {
            GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            NRATOBANKDownload objDownloadSearch = new NRATOBANKDownload();
            CommonFunction commonfc = new CommonFunction();




            DataTable result = new DataTable();
            objDownloadSearch.VDC_MUN_CD = commonfc.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objDownloadSearch.DISTRICT_CD = fc["ddl_District"].ConvertToString();
            objDownloadSearch.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objDownloadSearch.PAYROLL_INSTALL_CD = fc["ddl_Installation"].ConvertToString();
            objDownloadSearch.Transaction_Dt_From = fc["Transaction_Dt_From"].ConvertToString();
            objDownloadSearch.Transaction_Dt_To = fc["Transaction_Dt_To"].ConvertToString();
            result = objpaymentservice.GetTransactionLog(objDownloadSearch);

            ViewData["dtBankListresult"] = result;

            Session["dtBankListresult"] = result;

            //return View("TransactionLog");
            return PartialView("~/Views/HDSPPaymentProcess/_TransactionLogList1.cshtml");
        }
        #region FIRST PAYMENT
        public ActionResult ApproveEnrollPaymentTest()
        {
            NameValueCollection paramValues = new NameValueCollection();
            //GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            EnrollmentSearch objEnrollment = new EnrollmentSearch();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string session_id = String.Empty;
            DataTable dataresult = new DataTable();
            dataresult = (DataTable)Session["dtBankListresult"];
            string District = dataresult.Rows[0]["DIST_CD"].ToString();
            string VDC = dataresult.Rows[0]["VDC_CD"].ToString();
            string Ward = dataresult.Rows[0]["WARD"].ToString();
            if (District.ConvertToString() != string.Empty)
                paramValues.Add("dist", District);
            if (VDC.ConvertToString() != string.Empty)
                paramValues.Add("vdc", VDC);
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", Ward);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            string usercd = SessionCheck.getSessionUserCode();
            string date = DateTime.UtcNow.ConvertToString("dd-MMM-yyyy");
            QueryResult success = new QueryResult();
            //string TotalTargeted = string.Empty;
            string excout = string.Empty;
            string datacount = (dataresult.Rows.Count).ConvertToString();
            Session["ListCount"] = datacount;
            DataTable datatest = enrollService.getapprovePaymentListTEST(paramValues);
            Session["dtBankListresulttest"] = datatest;
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "FirstInspec" + District + "_" + VDC + "_" + Ward + "_" + usercd + "_" + date + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/NRATOBANK/") + "Inspected1_" + District + "_" + VDC + "_" + Ward + "_" + date + ".xls";
            }
            Session["file_Destination"] = filePath;
            string filename = Path.GetFileName(filePath);
            string html = RenderTESTPartialToString("~/Views/HDSPPaymentProcess/_ApprovedEnrollPaymentList.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Enrolment Payment List"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return null;
        }
        protected string RenderTESTPartialToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["dtBankListresulttest"] != null)
            {
                DataTable dt = (DataTable)Session["dtBankListresulttest"];
                string bankname = Session["bankname"].ConvertToString();
                string branchname = Session["branchname"].ConvertToString();
                string TodayDate = Session["todaydate"].ConvertToString();
                ViewData["dtBankListresulttest"] = dt;
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
        public static bool ExportToExcel(DataTable TESTTBL, string filepath, string filename)
        {
            //int row = 2, column = 2;
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.AddWorksheet("Education");
            //Title Row
            //var range = worksheet.Range("B2", "N4").Merge();
            //range.Value = string.Format("DEPUTY PRIME MINISTER'S OFFICE {0} GOVERNMENT OF SWAZILAND {0} OVC PILOT CASH TRANSFER", Environment.NewLine);
            //range.Style.Alignment.WrapText = true;
            //range.Style.Font.Bold = true;
            //range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            var cell = worksheet.Cell("A1");
            cell.Value = string.Format("SR. #", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("B1");
            cell.Value = string.Format("Beneficiary Name", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;


            cell = worksheet.Cell("C1");
            cell.Value = string.Format("District", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("D1");
            cell.Value = string.Format("VDC/Mun", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("E1");
            cell.Value = string.Format("Ward No.", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("F1");
            cell.Value = string.Format("PA_No.", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("G1");
            cell.Value = string.Format("GrandFather Name", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("H1");
            cell.Value = string.Format("Father Name", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            var range = worksheet.Range("I1", "J1").Merge();
            //cell = worksheet.Cell("K11");
            cell.Value = string.Format("If Married Women", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("I2");
            cell.Value = string.Format("GFather In-Law Name", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("J2");
            cell.Value = string.Format("Husband Name", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            range = worksheet.Range("K1,L1,M1,N1").Merge();
            cell.Value = string.Format("Citizenship Detail", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("K2");
            cell.Value = string.Format("Ctz No.", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("L2");
            cell.Value = string.Format("Issued District", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("M2");
            cell.Value = string.Format("Issued Date", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;

            cell = worksheet.Cell("N2");
            cell.Value = string.Format("DOB", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;


            cell = worksheet.Cell("01");
            cell.Value = string.Format("Remarks", Environment.NewLine);
            cell.Style.Alignment.WrapText = true;
            //Lookup values: 
            //Grade to Update
            //if (dtblGrade != null)
            //{
            //    row = 1;
            //    foreach (DataRow drow in dtblGrade.Rows)
            //    {
            //        worksheet.Range(string.Format("P{0}", row), string.Format("P{0}", row)).Value = drow["GRADE_DESCRIPTION"];
            //        row++;
            //    }
            //    worksheet.Columns("P").Hide();
            //}

            //if (dtblAbsentReason != null)
            //{
            //    row = 1;
            //    foreach (DataRow drow in dtblAbsentReason.Rows)
            //    {
            //        worksheet.Range(string.Format("Q{0}", row), string.Format("Q{0}", row)).Value = drow["REASON_ID"];
            //        row++;
            //    }
            //    worksheet.Columns("Q").Hide();
            //}

            //start writing data
            int row = 3;
            int Sno = 1;
            foreach (DataRow drow in TESTTBL.Rows)
            {
                worksheet.Cell(string.Format("A{0}", row)).Value = "'" + Sno.ConvertToString();
                worksheet.Cell(string.Format("B{0}", row)).Value = drow["REASON_ID"];
                worksheet.Range(string.Format("C{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("D{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("E{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("F{0}", row)).Value = drow["REASON_ID"];

                worksheet.Cell(string.Format("G{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("H{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("I{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("J{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("K{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("L{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("M{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("N{0}", row)).Value = drow["REASON_ID"];
                worksheet.Cell(string.Format("O{0}", row)).Value = drow["REASON_ID"];
                //if (dtblGrade != null)
                //{
                //    worksheet.Cell(row, "K").DataValidation.List(worksheet.Range(string.Format("P1:P{0}", dtblGrade.Rows.Count)));

                //}
                //if (dtblAbsentReason != null)
                //{
                //    worksheet.Cell(row, "M").DataValidation.List(worksheet.Range(string.Format("Q1:Q{0}", dtblAbsentReason.Rows.Count)));
                //}
                //var dv1 = worksheet.Cell(row, "L").DataValidation;
                //dv1.Decimal.Between(0, education.SchoolDays);
                //dv1.ErrorStyle = XLErrorStyle.Information;
                //dv1.ErrorTitle = "Number Out of Range";
                //dv1.ErrorMessage = string.Format("Please enter the number between 0 to {0}(School Days)!", education.SchoolDays);

                worksheet.Range(string.Format("B{0}", row), string.Format("N{0}", row)).Style.Border.OutsideBorder =
                   worksheet.Range(string.Format("B{0}", row), string.Format("N{0}", row)).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                worksheet.Row(row).Style.Font.FontSize = 9;
                Sno++;
                row++;
            }

            //worksheet.Range("J12", string.Format("N{0}", row)).Style.Protection.SetLocked(false);
            //Footer




            row += 2;
            range = worksheet.Range(string.Format("C{0}", row), string.Format("E{0}", row)).Merge();
            range.Style.Protection.SetLocked(false);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range = worksheet.Range(string.Format("I{0}", row), string.Format("J{0}", row)).Merge();
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Protection.SetLocked(false);
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            row++;
            range = worksheet.Range(string.Format("C{0}", row), string.Format("E{0}", row)).Merge();
            range.Value = "NAME OF HEAD TEACHER";
            range.Style.Font.FontSize = 8;
            range.Style.Font.Bold = true;
            range.Style.Font.FontName = "Arial Narrow";
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;


            range = worksheet.Range(string.Format("I{0}", row), string.Format("N{0}", row)).Merge();
            range.Value = "SIGNATURE OF HEAD TEACHER";
            range.Style.Font.FontSize = 8;
            range.Style.Font.Bold = true;
            range.Style.Font.FontName = "Arial Narrow";
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            range.Style.Protection.SetLocked(true);
            //range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

            row++;
            range = worksheet.Range(string.Format("F{0}", row), string.Format("H{0}", row)).Merge();
            range.Style.Protection.SetLocked(false);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            row++;
            range = worksheet.Range(string.Format("F{0}", row), string.Format("H{0}", row)).Merge();
            range.Value = "SCHOOL SEAL AND DATE";
            range.Style.Font.FontSize = 8;
            range.Style.Font.FontName = "Arial Narrow";
            range.Style.Font.Bold = true;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            row++;

            worksheet.Range("B2", string.Format("N{0}", row)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            //worksheet.Protect(Utils.ExcelPassword);
            worksheet.Columns("B", "N").AdjustToContents();
            row -= 1;

            //workbook.ShowGridLines = false;
            worksheet.ShowGridLines = false;
            workbook.SaveAs(string.Format("{0}{1}.xlsx", filepath, filename));
            ////
            return true;
        }

        public JsonResult CheckEditPermission(string approvedStatus)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();
           
            rvd = QueryStringEncrypt.DecryptString(approvedStatus);


            if (rvd["status"].ToString() == "ApprovedStatus")
            {
                return Json("Approved");
            }
            else
            {
                return Json("UnApproved");
            }
            
        }
        public ActionResult GetBankClaimList()
        {
            CheckPermission();
            //EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            ViewBag.IsApproved = "N";
            if (CommonVariables.GroupCD == "50")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                    ViewData["ddl_BankBranch"] = common.GetVDCMunByAllDistrict("","");
                    //ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewData["ddl_Installation"] = common.GetInstallation("");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                ViewData["ddl_Installation"] = common.GetInstallation("");
                ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
              
            }

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "You have Successfully Approved " + datacount + " data. ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["Message"] = "Approval Fialed ";
                }
                //if (Session["ApprovedMessage"].ConvertToString() == "WrongUser")
                //{
                //    ViewData["Message"] = "This user is not Authorized to approve data.";
                //}

                if (Session["ApprovedMessage"].ConvertToString() == "Update Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "Data Successfully Updated ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Update Failed")
                {
                    ViewData["Message"] = "Update Fialed! Please try again!";
                }

                Session["ApprovedMessage"] = "";
            }

            return View(objBankMapping);
        }
        [HttpPost]
        public ActionResult GetBankClaimList(FormCollection fc)
        {
            CheckPermission();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            CommonFunction common = new CommonFunction();
            
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            //objBankPayment.bank_branch_cd = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            //objBankPayment.BANK_BRANCH_CD = commonFC.GetCodeFromDataBase(fc["ddl_BankBranch"].ConvertToString(), "nhrs_bank_branch", "BRANCH_STD_CD");
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.PAYROLL_INSTALL_CD = fc["ddl_installment"].ConvertToString();
            objBankPayment.Approve = "N";
            objBankPayment.SecondTrancheApproved = "N";
            objBankPayment.ThirdTrancheApproved = "N";
            result = enrollService.GetBankClaimList(objBankPayment);

            if (CommonVariables.GroupCD == "50")
            {
                ViewBag.UserStatus = "InvalidUser";
            }
          

            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/HDSPPaymentProcess/_BankClaimSeachList.cshtml");

        }

        public ActionResult GetApprovedBankClaimList()
        {
            CheckPermission();
            //EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            ViewBag.IsApproved = "Y";
            if (CommonVariables.GroupCD == "50")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_Bank"] = common.getBankbyUser(bank_cd);
                    //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                    ViewData["ddl_BankBranch"] = common.GetVDCMunByAllDistrict("", "");
                    ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
                ViewData["ddl_Approved"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                //ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
                ViewData["ddl_Installation"] = common.GetInstallation("");
                ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");


            }

            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "You have Successfully Approved " + datacount + " data. ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["Message"] = "Approval Fialed ";
                }
             
                if (Session["ApprovedMessage"].ConvertToString() == "Update Success")
                {
                    string datacount = Session["ListCount"].ConvertToString();
                    ViewData["Message"] = "Data Successfully Updated ";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Update Failed")
                {
                    ViewData["Message"] = "Update Fialed! Please try again!";
                }

                Session["ApprovedMessage"] = "";
            }

            return View("GetBankClaimList", objBankMapping);
        }
        [HttpPost]
        public ActionResult ApprovedBankClaimList(FormCollection fc)
        {
            CheckPermission();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            CommonFunction common = new CommonFunction();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
            objBankPayment.DISTRICT_CD = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objBankPayment.VDC_MUN_CD = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankPayment.WARD_NO = fc["ddl_Ward"].ConvertToString();
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            //objBankPayment.bank_branch_cd = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = fc["ddl_BankBranch"].ConvertToString();
            //objBankPayment.BANK_BRANCH_CD = commonFC.GetCodeFromDataBase(fc["ddl_BankBranch"].ConvertToString(), "nhrs_bank_branch", "BRANCH_STD_CD");
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            objBankPayment.PAYROLL_INSTALL_CD = fc["ddl_installment"].ConvertToString();
            objBankPayment.Approve = "Y";
            objBankPayment.SecondTrancheApproved = "Y";
            objBankPayment.ThirdTrancheApproved = "Y";
            result = enrollService.GetBankClaimList(objBankPayment);

            if (CommonVariables.GroupCD == "50")
            {
                ViewBag.UserStatus = "InvalidUser";
            }


            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/HDSPPaymentProcess/_BankApprovedClaimSearchList.cshtml");

        }
        public ActionResult ApproveBankClaimList()
        {
            NameValueCollection paramValues = new NameValueCollection();
            //GetApprovePaymentListService objpaymentservice = new GetApprovePaymentListService();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable dataresult = new DataTable();
            DataTable dt = new DataTable();
            if (CommonVariables.GroupCD != "50")
            {
                dataresult = (DataTable)Session["dtBankListresult"];
                QueryResult success = new QueryResult();
                string excout = string.Empty;
                string datacount = (dataresult.Rows.Count).ConvertToString();
                string bank_cd = dataresult.Rows[0]["BANK_CD"].ConvertToString();
                string branch_cd=dataresult.Rows[0]["BRANCH_STD_CD"].ConvertToString();
                string payroll_install_cd = dataresult.Rows[0]["PAYROLL_INSTALL_CD"].ToString();

                Session["ListCount"] = datacount;
                string approved_batch = dataresult.Rows[0][0].ConvertToString();
                success = enrollService.ApproveBankclaimList(dataresult, approved_batch);
                if (success.IsSuccess)
                {
                    ViewData["Message"] = "ApproveSuccess";
                }
                else
                {
                    ViewData["Message"] = "ApproveFailed";
                }
                Session["ApprovedStatus"] = ViewData["Message"];
            }
            return RedirectToAction("GetApprovedBankClaimList");
        }

     
        //public static bool ExportToExcel(DataTable dataTable, String filepath, string sheetName = "Sheet1")
        //{
        //    bool isBroken = false;
        //    if (dataTable != null)
        //    {

        //        object m_objOpt = null;
        //        Excel.Application m_objExcel = null;
        //        Excel.Workbooks m_objBooks = null;
        //        Excel._Workbook m_objBook = null;
        //        Excel.Sheets m_objSheets = null;
        //        Excel._Worksheet m_objSheet = null;
        //        Excel.Range m_objRange = null;
        //        Excel.Font m_objFont = null;
        //        try
        //        {
        //            m_objOpt = System.Reflection.Missing.Value;
        //            m_objExcel = new Excel.Application();
        //            m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
        //            m_objBook = (Excel._Workbook)(m_objBooks.Add(m_objOpt));
        //            m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
        //            m_objSheet = (Excel._Worksheet)(m_objSheets.get_Item(1));
        //            m_objExcel.DisplayAlerts = false;
        //            //Creating Headers value
        //            int columns = dataTable.Columns.Count;
        //            if (columns <= 0)
        //            {
        //                ExceptionManager.AppendLog(new Exception("Datatable should contain at least one column."));
        //            }

        //            object[] objHeaders = new object[columns];
        //            for (int i = 0; i < columns; i++)
        //            { objHeaders[i] = (object)dataTable.Columns[i].ColumnName; }
        //            m_objRange = m_objSheet.get_Range("A1", m_objOpt);
        //            m_objRange = m_objRange.get_Resize(1, columns);
        //            m_objRange.set_Value(m_objOpt, objHeaders);
        //            m_objFont = m_objRange.Font;
        //            m_objFont.Bold = true;
        //            //Creates the data for data cell
        //            int rows = dataTable.Rows.Count;
        //            if (rows > 0)
        //            {
        //                object[,] objData = new object[rows, columns];
        //                for (int i = 0; i < rows; i++)
        //                {
        //                    for (int j = 0; j < columns; j++)
        //                    {
        //                        if (dataTable.Rows[i][j].IsNumeric())
        //                        {
        //                            if (dataTable.Rows[i][j].ConvertToString().Length <= 10)
        //                            {
        //                                objData[i, j] = (object)(dataTable.Rows[i][j].ConvertToString());
        //                            }
        //                            else
        //                            {
        //                                objData[i, j] = string.Concat("'", ((object)((dataTable.Rows[i][j]).ConvertToString())).ConvertToString());
        //                            }
        //                        }
        //                        else if (dataTable.Rows[i][j].IsDate())
        //                        {
        //                            //objData[i, j] = (object)Convert.ToDateTime(dataTable.Rows[i][j]).ToShortDateString();
        //                            objData[i, j] = (object)(Convert.ToDateTime(dataTable.Rows[i][j]).ToString("dd/MMM/yyyy"));
        //                        }
        //                        else
        //                        {
        //                            objData[i, j] = string.Concat("'", ((object)((dataTable.Rows[i][j]).ConvertToString())).ConvertToString());
        //                        }
        //                    }
        //                    if (isBroken)
        //                        break;
        //                }
        //                m_objRange = m_objSheet.get_Range("A2", m_objOpt);
        //                m_objRange = m_objRange.get_Resize(rows, columns);
        //                m_objRange.set_Value(m_objOpt, objData);
        //            }
        //            m_objSheet.Name = sheetName;
        //            m_objBook.SaveAs(fileName, m_objOpt, m_objOpt,
        //                m_objOpt, m_objOpt, m_objOpt, Excel.XlSaveAsAccessMode.xlNoChange,
        //                m_objOpt, m_objOpt, m_objOpt, m_objOpt, m_objOpt);
        //            m_objBook.Close(false, m_objOpt, m_objOpt);
        //            m_objExcel.Quit();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            ExceptionManager.AppendLog(ex);
        //            throw new Exception(ex.Message);
        //        }
        //        finally
        //        {

        //            //Clean-up
        //            if (m_objExcel != null)
        //            {
        //                m_objExcel.Quit();
        //                Marshal.ReleaseComObject(m_objExcel);
        //            }
        //            m_objOpt = null;
        //            m_objFont = null;
        //            m_objRange = null;
        //            m_objSheet = null;
        //            m_objSheets = null;
        //            m_objBooks = null;
        //            m_objBook = null;
        //            m_objExcel = null;
        //            GC.Collect();
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion
        public ActionResult ExportData(DataTable dtbl)
        {
            string district = dtbl.Rows[0]["DistrictName"].ToString();
            string vdc = dtbl.Rows[0]["VDCName"].ToString();
            string ward = dtbl.Rows[0]["WARD"].ToString();
            string date = DateTime.UtcNow.ConvertToString("dd-MMM-yyyy");
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dtbl);
                ws.Columns("A,C,G,M,N,P,T,U,V,W,X,Y,Z").Delete();
                ws.Cell("A1").Value = "RECIPIENT_NAME";
                ws.Cell("B1").Value = "DISTRICT";
                ws.Cell("C1").Value = "VDC/MUN";
                ws.Cell("D1").Value = "WARD";
                ws.Cell("E1").Value = "PA_NUM";
                ws.Cell("F1").Value = "GFATHER_NAME";
                ws.Cell("G1").Value = "FATHER_NAME";
                ws.Cell("H1").Value = "IF MARRIED WOMMEN";
                ws.Cell("H2").Value = "FATHER_INLAW_NAME";
                ws.Cell("I2").Value = "HUSBAND_NAME";
                ws.Cell("J1").Value = "BENEFICIARY_CTZ_DTL";
                ws.Cell("J2").Value = "CTZ_NO.";
                ws.Cell("K2").Value = "ISSUED_DIS";
                ws.Cell("L2").Value = "ISSUED_DATE";
                ws.Cell("L2").Value = "DOB";
                ws.Cell("N1").Value = "REMARKS";



                //ws.Columns("H,I,J").Delete();
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Payment.xlsx");
                // string file_name = Response.Headers;
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }
        public static bool ExportToExcel2(DataTable dataTable, String fileName)
        {
            bool isFileGenerated = false;
            StringBuilder strExcelBuilder = new StringBuilder();
            if (dataTable != null)
            {
                try
                {

                    int columns = dataTable.Columns.Count;
                    if (columns <= 0)
                    {
                        ExceptionManager.AppendLog(new Exception("Datatable should contain at least one column."));
                        return false;
                    }
                    else
                    {
                        strExcelBuilder.Append("<table>");
                        strExcelBuilder.Append("<tr>");
                        //for (int i = 0; i < columns; i++)                        
                        strExcelBuilder.Append("<td >");
                        strExcelBuilder.Append(dataTable.Columns["RECIPIENT_NAME"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["DISTRICT"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["VDC / MUN"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["WARD"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["PA_NUM"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["GFATHER_NAME"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["FATHER_NAME"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td colspan=2>");
                        strExcelBuilder.Append(dataTable.Columns["IF MARRIED WOMMEN"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td colspan=4>");
                        strExcelBuilder.Append(dataTable.Columns["Beneficiary Ctz. Detail"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["REMARKS"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("</tr>");
                        strExcelBuilder.Append("<tr>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["Father Inlaw Name"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["Husband Name"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("</tr>");
                        strExcelBuilder.Append("<tr>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["Ctz. No"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["Issued District"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["Issued Date"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("<td>");
                        strExcelBuilder.Append(dataTable.Columns["DOB"].ToString());
                        strExcelBuilder.Append("</td>");
                        strExcelBuilder.Append("</tr>");

                        //strExcelBuilder.Append("<tr>");
                        //for (int i = 1; i < columns; i++)
                        //{
                        //    strExcelBuilder.Append("<td>");
                        //    strExcelBuilder.Append(dataTable.Columns[i].ToString());
                        //    strExcelBuilder.Append("</td>");
                        //}
                        //strExcelBuilder.Append("</tr>");
                        //if j=14(It is Column number in terris  )
                        if (dataTable.Rows.Count > 0)
                        {
                            for (int i = 1; i < dataTable.Rows.Count; i++)
                            {
                                strExcelBuilder.Append("<tr>");
                                for (int j = 0; j < 14; j++)
                                {
                                    strExcelBuilder.Append("<td>");
                                    strExcelBuilder.Append(dataTable.Rows[i][j].ToString());
                                    strExcelBuilder.Append("</td>");
                                }
                                strExcelBuilder.Append("</tr>");
                            }
                        }
                        strExcelBuilder.Append("</table>");
                    }
                    isFileGenerated = Utils.CreateFile(strExcelBuilder.ToString(), fileName);
                    return isFileGenerated;
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public ActionResult GetBankClaimDuplicateList()
        {
            CheckPermission();
            //EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchId("");
            //ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");
            return View(objBankMapping);
        }
        [HttpPost]
        public ActionResult GetBankClaimDuplicateList(FormCollection fc)
        {

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            DataTable result = new DataTable();
            BankPayrollDetail objBankPayment = new BankPayrollDetail();

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objBankPayment.Lang = "E";
            }
            else
            {
                objBankPayment.Lang = "N";
            }
          
            objBankPayment.BANK_CD = fc["ddl_Bank"].ConvertToString();
            //objBankPayment.bank_branch_cd = fc["ddl_BankBranch"].ConvertToString();
            objBankPayment.BANK_BRANCH_CD = commonFC.GetCodeFromDataBase(fc["ddl_BankBranch"].ConvertToString(), "nhrs_bank_branch", "BRANCH_STD_CD");
            objBankPayment.FISCAL_YR = fc["ddl_FiscalYr"].ConvertToString();
            // objBankPayment.QUARTER = fc["Quarter"].ConvertToString();
            result = enrollService.GetDuplicateBankClaimList(objBankPayment);
            ViewData["dtBankListresult"] = result;
            Session["dtBankListresult"] = result;
            ViewData["Message"] = fc["hdnMessage"].ConvertToString();
            return PartialView("~/Views/HDSPPaymentProcess/_BankClaimDuplicateList.cshtml");

        }
        public JsonResult Fillbranchdropdown(string bankid)
        {

            //com.GetVDCMunByAllDistrict(com.GetDefinedCodeFromDataBase(distid.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"), distid.ConvertToString());
            CommonFunction commF = new CommonFunction();

            //var dist = com.FillVDCMun(distid.ToString());
            //var cities = db.Cities.Where(c => c.StateId == state);
            //return Json(dist, JsonRequestBehavior.AllowGet);

            return commF.FillBranchBybank(bankid.ToString());
        }

         [CustomAuthorizeAttribute(PermCd = "7")]
        public JsonResult ChangeStatus(string p)  
        {
            Users obj;
            BankPayrollDetail payrollDetail = new BankPayrollDetail();
            string strUsername = "";
            string status = "";
            string returnResult = "";
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string AccountNo = "";
            string id = "";
            string id1 = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string stat = "";

            if (Session[SessionCheck.sessionName] != null)
            {
                obj = (Users)Session[SessionCheck.sessionName];
                strUsername = obj.usrName;
            }

            try
            {
                if (CommonVariables.GroupCD != "50")
                {
                    if (p != null)
                    {
                        rvd = QueryStringEncrypt.DecryptString(p);
                        if (rvd != null)
                        {
                            if (rvd["id"] != null)
                            {
                                id = rvd["id"].ToString();
                            }
                            if (rvd["id1"] != null)
                            {
                                id1 = rvd["id1"].ToString();
                            }
                            if (rvd["status"] != null)
                            {
                                status = rvd["status"].ToString();
                            }

                        }
                    }

                    string approve = "";

                    var result = enrollService.GetBankClaimbyId(id, id1);
                    AccountNo = result.AccountNo;
                    if (id1 == "1")
                    {
                        stat = (result.ApproveStatus).ToString();
                    }
                    else if (id1 == "2")
                    {
                        stat = (result.SecondTrancheApproved).ToString();
                    }
                    else
                    {
                        stat = (result.ThirdTrancheApproved).ToString();
                    }

                    if (stat == "Y")
                    {
                        if(id1 == "1")
                        {
                          payrollDetail.Approve = "N";
                          approve = payrollDetail.Approve;
                          payrollDetail.APPROVED_BY = null;
                          payrollDetail.APPROVED_DT = null;
                        }
                        else if (id1 == "2")
                        {
                            payrollDetail.SecondTrancheApproved = "N";
                            approve = payrollDetail.SecondTrancheApproved;
                            payrollDetail.APPROVED_BY = null;
                            payrollDetail.APPROVED_DT = null;
                        }
                        else
                        {
                            payrollDetail.ThirdTrancheApproved = "N";
                            approve = payrollDetail.ThirdTrancheApproved;
                            payrollDetail.APPROVED_BY = null;
                            payrollDetail.APPROVED_DT = null;
                        }
                       

                    }
                    else if (stat == "N")
                    {
                        if (id1 == "1")
                        {
                            payrollDetail.Approve = "Y";
                            approve = payrollDetail.Approve;
                            payrollDetail.APPROVED_BY = strUsername;
                            payrollDetail.APPROVED_DT = DateTime.Now.ToString();

                        }
                        else if (id1 == "2")
                        {
                            payrollDetail.SecondTrancheApproved = "Y";
                            approve = payrollDetail.SecondTrancheApproved;
                            payrollDetail.APPROVED_BY = strUsername;
                            payrollDetail.APPROVED_DT = DateTime.Now.ToString();

                        }
                        else
                        {
                            payrollDetail.ThirdTrancheApproved = "Y";
                            approve = payrollDetail.ThirdTrancheApproved;
                            payrollDetail.APPROVED_BY = strUsername;
                            payrollDetail.APPROVED_DT = DateTime.Now.ToString();

                        }
                        
                    }
                    
                    payrollDetail.bank_payroll_id = Convert.ToString(id);
                    payrollDetail.MODE = "A";
                    payrollDetail.PAYROLL_INSTALL_CD = id1;
                    payrollDetail.Status = approve;
                    payrollDetail.ACCOUNT_NUMBER = AccountNo;
                    enrollService.ApproveIndividualBankClaimList(payrollDetail);

                }
                
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            if (stat == "N")
             { 
                returnResult = "Y";
             }
            else if (stat == "Y")
             { 
                returnResult = "N";
             }
          
             return Json(returnResult);
                
            }
        
         public ActionResult EditBankClaim(FormCollection fc)
         {
             BankClaim objBankClaim = new BankClaim();
             EnrollmentImportExport objBankService = new EnrollmentImportExport();

             string pa_num = "";
             string payroll_installment_cd = "";
             RouteValueDictionary rvd = new RouteValueDictionary();
             string value = "";
             if (fc != null)
             {
                 value = fc["editForm"].ToString();
                 if (rvd != null)
                 {
                     rvd = QueryStringEncrypt.DecryptString(value);
                     if (rvd["id"] != null)
                     {
                         pa_num = rvd["id"].ToString();
                         payroll_installment_cd = "1";
                         objBankClaim.PA_NO = Convert.ToString(pa_num);
                         objBankClaim.Payroll_install_cd = payroll_installment_cd;
                         objBankClaim = objBankService.GetBankClaimbyId(pa_num, payroll_installment_cd);



                         ViewData["ddl_District"] = commonFC.GetDistricts(objBankClaim.Dis_Cd.ConvertToString());
                         ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrictCode(objBankClaim.Vdc_Mun_Cd.ToString(), objBankClaim.Dis_Cd.ToString());
                         ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun(objBankClaim.Ward_Num.ConvertToString(), objBankClaim.Vdc_Mun_Cd.ToString());
                         ViewData["ddl_Approved"] = (List<SelectListItem>)commonFC.GetYesNo1(objBankClaim.IsCard_Issued).Data;
                         if (CommonVariables.GroupCD != "50")
                         {
                             ViewData["ddl_Bank"] = commonFC.GetBankName("");
                             ViewData["ddl_BankBranch"] = commonFC.GetBankBranchIdByBankID("", "", (objBankClaim.Bank_cd).ToString(), "");
                         }
                         else
                         {
                             ViewData["ddl_Bank"] = commonFC.getBankbyUser((objBankClaim.Bank_cd).ToString());
                             ViewData["ddl_BankBranch"] = commonFC.GetBankBranchIdByBankID("", "", (objBankClaim.Bank_cd).ToString(), "");
                         }


                         return View(objBankClaim);
                     }
                 }
             }
             return View(objBankClaim);
         }

        [HttpPost]
        public ActionResult UpdateBankClaim(FormCollection fc)
        {
            BankClaim objBankClaim = new BankClaim();
            EnrollmentImportExport objBankService = new EnrollmentImportExport();
            bool checkUpdate = false;


            objBankClaim.PA_NO = fc["PA_NO"].ConvertToString();
           // objBankClaim.Bank_Name          = fc["BANK_NAME"].ConvertToString();
          //  objBankClaim.Beneficiary_Name   = fc["Beneficiary_Name"].ConvertToString();
            objBankClaim.Dis_Cd             = fc["Dis_Cd"].ToInt32();
            string vdcCode = fc["ddl_VDCMun"].ToString();
            objBankClaim.Ward_Num = fc["Ward_Num"].ToInt32();
            objBankClaim.Reciepient_Name = fc["Reciepient_Name"].ConvertToString();
            //objBankClaim.Gender             = fc["GENDER_CD"].ConvertToString();
            //objBankClaim.Bank_Name           = fc["ddl_Bank"].ConvertToString();
           //objBankClaim.Branch_Cd = fc["Branch_Cd"].ToInt32();
            objBankClaim.AccountNo = fc["AccountNo"].ConvertToString();
            objBankClaim.Branch_Std_Cd = fc["ddl_BankBranch"].ToString();
            objBankClaim.Activation_Date = (fc["Activation_Date"]).ConvertToString("yyyy-MM-dd");
            objBankClaim.Activation_Date_LOC = NepaliDate.getNepaliDate(objBankClaim.Activation_Date.ToString());
            objBankClaim.Tranche = "1";
            objBankClaim.Deposited_Date = (fc["Deposited_Date"]).ConvertToString("yyyy-MM-dd");
            objBankClaim.Deposited_Date_LOC = NepaliDate.getNepaliDate(objBankClaim.Deposited_Date.ConvertToString());
            objBankClaim.Remarks = fc["Remarks"].ConvertToString();
            objBankClaim.IsCard_Issued = fc["ddl_Approved"].ToString();
            objBankClaim.Batch = fc["Batch"].ToInt32();
            //objBankClaim.Branch = fc["ddl_BankBranch"].ToString();
            objBankClaim.Deposite = 50000;
           // objBankClaim.ApproveStatus      = fc["APPROVED"].ToString();
            objBankClaim.Bank_cd = Convert.ToInt32(fc["Bank_cd"]);
            objBankClaim.Card_Iss_Date = Convert.ToString(fc["Card_Iss_Date"]);
            objBankClaim.Branch_Cd = Convert.ToInt32(commonFC.GetBankBranchCdByStdCd((objBankClaim.Branch_Std_Cd).ToString(), (objBankClaim.Bank_cd).ToString()));
            //objBankClaim.Branch = commonFC.GetCodeFromDataBase(objBankClaim.Branch_Cd,"","")

            objBankClaim.Bank_Name = commonFC.GetCodeFromDataBase((objBankClaim.Bank_cd).ToString(),"NHRS_BANK","DESC_ENG");
            objBankClaim.Vdc_Mun_Cd = Convert.ToInt32(commonFC.GetCodeFromDataBase(vdcCode.ToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            objBankClaim.Branch = commonFC.GetCodeFromDataBase(objBankClaim.Branch_Cd.ToString(), "NHRS_BANK_BRANCH", "DESC_ENG");
            objBankClaim.TRANSACTON_ID = fc["TRANSACTON_ID"].ToInt32();
            objBankClaim.Payroll_ID = fc["Payroll_ID"].ToInt32();

            if (fc["btn_Submit"].ToString() == "Update" || fc["btn_Submit"].ToString() == "अपडेट")
            {
               checkUpdate = objBankService.UpdateBankClaimDetail(objBankClaim);
            }
            if (checkUpdate == true)
            {
                ViewData["Message"] = "Update Success";
                
            }
            else {
                ViewData["Message"] = "Update Failed";
                
            }

            Session["ApprovedMessage"] = ViewData["Message"];
            return RedirectToAction("GetBankClaimList");
        }

      
    }

  
}
