using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;
using MIS.Services.Inspection;
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

namespace MIS.Controllers.Inspection
{
    public class InspectionReportController : BaseController   
    {
        //
        // GET: /InspectionReport/
        CommonFunction common = new CommonFunction();

        public ActionResult InspectionManagement()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictForUser(Session["UserVdcMunDefCd"].ConvertToString());

            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
          
            return View();
        }
        public ActionResult InspectionDailyReport()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictForUser(Session["UserVdcMunDefCd"].ConvertToString());
            ViewData["ddl_InspManageUsers"] = common.GetInspectionManageUser("");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            return View();
        }

        public ActionResult LoadInspectionDailyReport(string dist, string vdc, string ward, string user )
        {
            HtmlReport objreport = new Models.Report.HtmlReport();
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.User = user;
            
            return View("InspectionDailyReportDetailView", objreport);
        }
        public ActionResult LoadInspectionDailySummaryReport(string dist, string vdc, string ward, string user)
        {
            HtmlReport objreport = new Models.Report.HtmlReport();
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.User = user;

            return View("InspectionDailyReportSummaryView", objreport);
        }

      
        public ActionResult InspectionSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Inspection Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(obj);

        }
        public ActionResult GetInspectionSummaryReport(string dist, string vdc, string ward)
        {
            CommonFunction common = new CommonFunction();

            InspectionReportService objInsservice = new InspectionReportService();
            NameValueCollection paramValues = new NameValueCollection();

            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;


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
            dt = objInsservice.GetInspectionSummaryReport(paramValues); 
            if (dt != null)
            { 
                Session["SummaryResults"] = dt;
                objReport.District = dist;
                objReport.VDC = vdc;
                objReport.Ward = ward; 
            }
            return RedirectToAction("InspectionSummary", objReport);
        }

        [HttpPost]
        public JsonResult GetInspectionDailyReport(string dist, string vdc, string ward, string user,  int start, int length, int draw)
        {
            CommonFunction common = new CommonFunction();

            InspectionReportService objInsservice = new InspectionReportService();
            NameValueCollection paramValues = new NameValueCollection();           
            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.User=user;  
          

            if (string.IsNullOrEmpty(dist))
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (string.IsNullOrEmpty(vdc))
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (string.IsNullOrEmpty(user))
                paramValues.Add("user", user);                
                paramValues.Add("pagesize", length);           
                paramValues.Add("pageindex", start);

           
           
            DataTable dt = new DataTable();
            dt = objInsservice.GetInspectionDailyReport(paramValues);
            var recordCount = 0;
            if(dt!=null && dt.Rows.Count>0)
                recordCount=Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);

            var listDatat=Common.ConvertToList(dt);
            var dataToReturn = Json(new
            {
                data = listDatat,
                recordsFiltered = recordCount,
                recordsTotal = recordCount,
                draw
            }, JsonRequestBehavior.AllowGet);
            dataToReturn.MaxJsonLength = Int32.MaxValue;
            return dataToReturn;
            
        }

        [HttpPost]
        public JsonResult GetInspectionDailySummaryReport(string dist, string vdc, string ward, string user, int start, int length, int draw)
        {
            CommonFunction common = new CommonFunction();

            InspectionReportService objInsservice = new InspectionReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objReport = new HtmlReport();
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.User = user;


            if (string.IsNullOrEmpty(dist))
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (string.IsNullOrEmpty(vdc))
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (string.IsNullOrEmpty(user))
                paramValues.Add("user", user);
            paramValues.Add("pagesize", length);
            paramValues.Add("pageindex", start);



            DataTable dt = new DataTable();
            dt = objInsservice.GetInspectionDailySummaryReport(paramValues);
            var recordCount = 0;
            if (dt != null && dt.Rows.Count > 0)
                recordCount = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);

            var listDatat = Common.ConvertToList(dt);
            var dataToReturn = Json(new
            {
                data = listDatat,
                recordsFiltered = recordCount,
                recordsTotal = recordCount,
                draw
            }, JsonRequestBehavior.AllowGet);
            dataToReturn.MaxJsonLength = Int32.MaxValue;
            return dataToReturn;

        }
       
    }
}
