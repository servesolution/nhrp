using EntityFramework;
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
    public class HouseHoldReportController : BaseController
    {
        //
        // GET: /HouseHoldReport/
        CommonFunction common = new CommonFunction();



        public ActionResult HouseholdSummaryReport()
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            return View();
        }
        public ActionResult GetSummaryReportByMonthlyIncome(string dist, string vdc, string ward, string MonthlyIncome)
        {

            HouseholdHtmlReportService objBenfservice = new HouseholdHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (MonthlyIncome.ConvertToString() != string.Empty)
                paramValues.Add("MonthlyIncome", MonthlyIncome);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            dt = objBenfservice.HouseholdDetailReportByMonthlyIncome(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("HouseholdSummaryReport");

        }

    }
}
