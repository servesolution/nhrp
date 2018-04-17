using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;


namespace MIS.Controllers.Report
{
    public class EnrollmentHRReportController : Controller
    {
        //
        // GET: /EnrollmentHRReport/

        CommonFunction common = new CommonFunction();
        public ActionResult EnrollmentHRReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Enrollment Report";
            ViewData["SummaryResults"] = dt;
            objreport = (HtmlReport)Session["ERPSummary"];
            return View(objreport);
        }
        public ActionResult GetEnrollmentHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo)
        {

            EnrollmentHRReportService objService = new EnrollmentHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (DateFrom.ConvertToString() != string.Empty)

                paramValues.Add("DateFrom", DateFrom);
            if (DateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", DateTo);


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
            Session["ERPSummary"] = objreport;

            DataTable dt = new DataTable();
            dt = objService.GetEnrollmentHRReport(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("EnrollmentHRReport", objreport);
        }

    }
}
