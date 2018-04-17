using EntityFramework;
using MIS.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Data;
using MIS.Services.Report;

namespace MIS.Controllers.Report
{
    public class GrievanceSurveyReportController : BaseController
    {
        //
        // GET: /GrievanceSurveyReport/

        CommonFunction common = new CommonFunction();

        public ActionResult GrievanceSurveyReport(HtmlReport ObjReport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Grievance Survey Report";
            return View(ObjReport);
        }
        public ActionResult GetGrievanceSurveyDetail(string dist, string vdc, string ward, string currentVdc, string currentWard)
        {
            HtmlReport objReport = new HtmlReport();
            GreivanceSurveyReportService objService = new GreivanceSurveyReportService();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);

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
            DataTable dt = new DataTable();
            dt = objService.GrievanceSurveyDetailReport(paramValues);
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;

            objReport.CurrentVDC = currentVdc;
            objReport.CurrentWard = currentWard;

            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("GrievanceSurveyReport", objReport);

        }

    }
}