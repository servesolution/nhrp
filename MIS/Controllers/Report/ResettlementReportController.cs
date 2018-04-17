using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models.Report;
//using ClosedXML;
//using ClosedXML.Excel;
using System.IO;

namespace MIS.Controllers.Report
{
    public class ResettlementReportController : BaseController
    {
        //
        // GET: /ResettlementReport/
        CommonFunction common = new CommonFunction();

        //public ActionResult ResettlementResults(HtmlReport obj)
        //{
        //    int Index = obj.Index;

        //    DataTable dt = null;
        //    dt = (DataTable)Session["DetailResults"];
            
        //    ViewBag.ReportTitle = "Grievance Beneficiary Detail Report";
        //    ViewData["DetailResults"] = dt;
        //    return View(obj);
        //}

        public ActionResult ResettlementDetail(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Resettlement Detail Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        public ActionResult ResettlemenNonBenftDetail(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Resettlement Detail Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        //  public ActionResult GrievanceBeneficiarySummary(HtmlReport obj)
        //{

        //    DataTable dt = null;
        //    dt = (DataTable)Session["SummaryResults"];
        //    ViewBag.ReportTitle = "Grievance Beneficiary Summary Report";
        //    ViewData["SummaryResults"] = dt;
        //    return View(obj);
        //}
        public ActionResult GetResettlementReport(string dist, string vdc, string ward, string ReviewType)
        {

            ResettlementReportService objResService = new ResettlementReportService();
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
            if (ReviewType.ConvertToString() != string.Empty)
                paramValues.Add("ReviewType", ReviewType);


            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objResService.GetResettlementReport(paramValues);   
            if (dt != null)
            {               
                Session["DetailResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward;
            }
            return RedirectToAction("ResettlementDetail", objReport);
        }

        public ActionResult GetResettlementDetNonBenf(string dist, string vdc, string ward)
        {

            ResettlementReportService objResService = new ResettlementReportService();
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

            dt = objResService.GetResettlementNonBenfDetail(paramValues); 
            if (dt != null)
            {
                
                Session["DetailResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward;
               



            }
            return RedirectToAction("ResettlemenNonBenftDetail", objReport);
        }
        public ActionResult ResettlementSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = " Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(obj);
        }
        public ActionResult ResettlementSummaryNonBenf(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Nonbeneficiary Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(obj);
        }

        public ActionResult GetResettlementSummaryReport(string dist, string vdc, string ward, string ReviewType)
        {

            ResettlementReportService objResService = new ResettlementReportService();
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
            if (ReviewType.ConvertToString() != string.Empty)
                paramValues.Add("ReviewType", ReviewType);


            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            } 
            DataTable dt = new DataTable();

            dt = objResService.GetResettlementSummaryReport(paramValues); 
            if (dt != null)
            { 
                Session["SummaryResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward; 
            }
            return RedirectToAction("ResettlementSummary", objReport);
        }

        public ActionResult GetResettlementSumNonBenf(string dist, string vdc, string ward)
        {

            ResettlementReportService objResService = new ResettlementReportService();
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

            dt = objResService.GetResettlementNonBenfSummary(paramValues); 
            if (dt != null)
            { 
                Session["SummaryResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward; 
            }
            return RedirectToAction("ResettlementSummaryNonBenf", objReport);
        }
        

    }
}
