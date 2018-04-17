using EntityFramework;
using MIS.Models.Enrollment;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Enrollment;
using MIS.Services.Inspection;
using MIS.Services.Report;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class EnrolledSummaryReportController : Controller
    {
        //
        // GET: /EnrollmentReport/
        CommonFunction common = new CommonFunction();
        public ActionResult EnrolledSummaryReport(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Reconstruction Enrollment Report";
            return View(ObjReport);
        }
        public ActionResult GetEnrolledSummaryReport(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
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
            dt = objEnrollService.EnrollmentSummaryReport(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrolledSummaryReport",objReport);

        }


        public ActionResult GetEnrolledDetailReport(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

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
            dt = objEnrollService.EnrollmentPAReportExport(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrolledSummaryReport", objReport);

        }

        public ActionResult EnrolledDetailReportPA(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Enrollment Detail PA Report";
            return View(ObjReport);
        }
        public ActionResult GetEnrolledDetailReportPA(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
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
            dt = objEnrollService.EnrollmentDetailReportPA(paramValues);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("DOC0");
                dt.Columns.Add("DOC1");
                foreach(DataRow dr in dt.Rows)
                {
                    
                    if(dr["CTZN_PIC_NAME"].ConvertToString()!=null)
                    {
                        string[] Document = dr["CTZN_PIC_NAME"].ConvertToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        int DocLength = Document.Length;
                        for (int i = 0; i < DocLength; i++)
                        {
                            if (i == 2)
                            {
                                break;
                            }
                            dr["DOC" + i] = Document[i].ConvertToString();
                           
                        }
                    }
                     
                }

                
               
                
            }

            objReport.District = dist;
            objReport.VDC = vdc;
           

            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            Session["enrolledpaparams"] = objReport;
            return RedirectToAction("EnrolledDetailReportPA", objReport);

        }

        public ActionResult EnrolledSummaryReportPA(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Enrollment Summary PA Report";
            return View(ObjReport);
        }

        public ActionResult GetEnrolledSummaryReportPA(string dist, string vdc, string ward)
        {
            HtmlReport objReport = new HtmlReport();
            EnrolledSummaryReportSrvice objEnrollService = new EnrolledSummaryReportSrvice();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
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
            dt = objEnrollService.EnrollmentSummaryReportPA(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("EnrolledSummaryReportPA", objReport);

        }

        public ActionResult ExportEnrolledSummaryReport(string dist, string vdc, string ward)
        {

            GetEnrolledSummaryReport(dist, vdc, ward);
            HtmlReport rptParams = new HtmlReport();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_EnrolledSummaryReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls");
        }




        public ActionResult ExportEnrolledDetailReport(string dist, string vdc, string ward)
        {

            GetEnrolledDetailReport(dist, vdc, ward);
            HtmlReport rptParams = new HtmlReport();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_EnrolledDetailReportPAFull.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Enroll Detail Report PA to Excel"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Enrolled_Detail_REPORT_PA" + "( District " + dist + " ).xls");
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

        public ActionResult EnrolledDetailPAExport(string dist, string vdc, string ward )
        {
            GetEnrolledDetailReportPA( dist,  vdc,  ward );
            DataTable dt = new DataTable();
            HtmlReport rptParametess = new HtmlReport();
            rptParametess = (HtmlReport)Session["enrolledpaparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled PA Detail report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled PA Detail report" + "( District " + dist + " ).xls";
            }

            string html = RenderPartialViewToStringforEnrolledreportDetail("~/Views/HTMLReport/_EnrolledDetailReportPA.cshtml");



            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "EnrolledPAdetail.xls");


        }

        protected string RenderPartialViewToStringforEnrolledreportDetail(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["enrolledpaparams"];
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
        #region HTML TO PDF
        public ActionResult GenPDF(string HouseOwnerID, string PA)
        {
            DataTable dt = new DataTable();
            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            //string HouseOwnerID, PA = string.Empty;
            EnrollmentAdd objenrolladd = new EnrollmentAdd();
            dt = objEnrollAddServices.GetPAPrintView(HouseOwnerID, PA);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    objenrolladd.NRA_DEFINED_CD = dr["NRA_DEFINED_CD"].ConvertToString();
                    objenrolladd.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                    objenrolladd.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                    objenrolladd.DISTRICT_NAME = dr["DISTRICT"].ConvertToString();
                    objenrolladd.VDC_MUN_NAME = dr["VDC_MUN"].ConvertToString();
                    objenrolladd.WARD_NO = dr["WARD_NO"].ConvertToString();
                    objenrolladd.AREA_ENG = dr["AREA_ENG"].ConvertToString();
                    objenrolladd.ENUMERATION_AREA = dr["EA"].ConvertToString();
                    objenrolladd.BENEFICIARY_FULLNAME_ENG = dr["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                    objenrolladd.BIRTH_DT = dr["BENEFICIARY_DOB_ENG"].ConvertToString();
                    objenrolladd.IDENTIFICATION_ISSUE_DT = dr["BENEFICIARY_CTZ_ISSUE_DT"].ConvertToString();
                    objenrolladd.BENEFICIARY_CTZ_NO = dr["BENEFICIARY_CTZ_NO"].ConvertToString();
                    objenrolladd.IDENTIFICATION_ISSUE_DIS = dr["CTZ_ISSUED_DIST"].ConvertToString();
                    objenrolladd.PHONE_NO = dr["BENEFICIARY_PHONE"].ConvertToString();
                    objenrolladd.migrationdateloc = dr["BENEFICIARY_MIGRATION_DT_LOC"].ConvertToString();
                    objenrolladd.migrationno = dr["BENEFICIARY_MIGRATION_NO"].ToDecimal();
                    objenrolladd.FATHER_FullNAME_ENG = dr["FATHER_FULLNAME_ENG"].ConvertToString();
                    objenrolladd.GFATHER_FullNAME_ENG = dr["GFATHER_FULLNAME_ENG"].ConvertToString();
                    objenrolladd.BUILDING_KITTA_NUMBER = dr["PLOT_NO"].ConvertToString();
                    objenrolladd.BUILDING_AREA = dr["BUILDING_AREA"].ConvertToString();
                    objenrolladd.BUILDING_DISTRICT = dr["BUILDING_DISTRICT"].ConvertToString();
                    objenrolladd.BUILDING_VDC = dr["BUILDING_VDC"].ConvertToString();
                    objenrolladd.BUILDING_WARD_NO = dr["BUILDING_WARD_NO"].ConvertToString();
                    objenrolladd.PROXY_FULLNAME_ENG = dr["MANJURINAMA_FULLNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_RELATION = dr["MANJURINAMA_RELATION"].ConvertToString();
                    objenrolladd.NOMINEE_FULLNAME_ENG = dr["NOMINEE_FULLNAME_ENG"].ConvertToString();
                    objenrolladd.NOMINEE_RELATION = dr["NOMINEE_RELATION"].ConvertToString();
                    objenrolladd.OFFICE_ENG = dr["OFFICE_NAME"].ConvertToString();
                    objenrolladd.EMPLOYEE_ENG = dr["OFFICIAL"].ConvertToString();
                    objenrolladd.POSITION_ENG = dr["POSITION"].ConvertToString();
                    objenrolladd.ENROLLMENT_MOU_DT = dr["AGGREMENT_DATE"].ConvertToString();
                    objenrolladd.Beneficiary_Photo = dr["BENEFICIARY_PHOTO"].ConvertToString();
                }
            }
            Session["EnrollmentObject"] = objenrolladd;

            HtmlToPdf converter = new HtmlToPdf();
            InspectionDetailService ins = new InspectionDetailService();
            TextWriter writer = new StringWriter();
            string htmlString = RenderViewToString("EnrollmentPrintView");

            string baseUrl = ins.GetBaseUrl();

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

            byte[] pdf = doc.Save();
            doc.Close();
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "EnrollmentPA'" + PA + "'.pdf";
            return fileResult;
        }

        protected string RenderViewToString(string viewName)
        {
            ViewData.Model = Session["EnrollmentObject"];
            EnrollmentAdd objInspectn = (EnrollmentAdd)Session["EnrollmentObject"];
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            using (StringWriter sw = new StringWriter())
            {
                ViewData.Model = objInspectn;
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, "");
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }

        }

        #endregion HTML TO PDF


       

    }
}