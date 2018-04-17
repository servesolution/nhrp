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
    public class PlinthLevelHRReportController : Controller
    {
        //
        // GET: /PlinthLevelHRReport/

        CommonFunction common = new CommonFunction();
        public ActionResult PlinthLevelHRReport(HtmlReport objreport)
        { 

            DataTable dt = new DataTable();
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Plinth Level Report";

            ViewData["SummaryResults"] = dt;
            DataColumn col = dt.Columns.Add("Total_Comply", typeof(int));
            if(objreport.District==null)
            {
                col.SetOrdinal(12);
            }
            if (objreport.District != null && objreport.VDC==null)
            {
                col.SetOrdinal(13);
            }
            if (objreport.VDC != null)
            {
                col.SetOrdinal(14);
            }
            int countLength = dt.Rows.Count;
            for (int i = 0; i < countLength; i++)
            {
                int totalComply = 0;
                totalComply = dt.Rows[i]["COMP_C_RCC"].ToInt32() + dt.Rows[i]["COMP_SMM"].ToInt32() +
                       dt.Rows[i]["COMP_SMC"].ToInt32() + dt.Rows[i]["COMP_BMM"].ToInt32() +
                       dt.Rows[i]["COMP_BMC"].ToInt32() + dt.Rows[i]["COMP_AB_TYPE_RCC"].ToInt32();
                dt.Rows[i]["Total_Comply"] = totalComply;
            }


            DataColumn cols = dt.Columns.Add("Total_NonComply", typeof(int));
            if (objreport.District == null)
            {
                cols.SetOrdinal(20);
            }
            if (objreport.District != null && objreport.VDC == null)
            {
                cols.SetOrdinal(21);
            }
            if (  objreport.VDC != null)
            {
                cols.SetOrdinal(22);
            }
            for (int i = 0; i < countLength; i++)
            {
                int totalNComply = 0;
                totalNComply = dt.Rows[i]["NCOMP_C_RCC"].ToInt32() + dt.Rows[i]["NCOMP_SMM"].ToInt32() +
                  dt.Rows[i]["NCOMP_SMC"].ToInt32() + dt.Rows[i]["NCOMP_BMM"].ToInt32() +
                  dt.Rows[i]["NCOMP_BMC"].ToInt32() + dt.Rows[i]["NCOMP_AB_TYPE_RCC"].ToInt32();
                dt.Rows[i]["Total_NonComply"] = totalNComply;

            }
            //dt.Columns.Add(new DataColumn("Total1", typeof(String)));
            //objreport.TOTAL_SURVEYED = Convert.ToInt32(dt.Compute("Sum(TOTAL_SURVEYED)", ""));
            objreport = (HtmlReport)Session["ERPSummary"];
            return View(objreport);
        }
        public ActionResult GetPlinthLevelHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo)
        {

            PlinthLevelHRReportService objService = new PlinthLevelHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
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
            dt = objService.GetPlinthLevelHRReport(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            

            return RedirectToAction("PlinthLevelHRReport", objreport);
        }

    }
}
