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
    public class EnrollmentReconstructionPaymentSummaryController : Controller
    {
        //
        // GET: /EnrollmentReconstructionPaymentSummary/
        CommonFunction common = new CommonFunction();
        public ActionResult ERPSummary(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Enrollment Reconstruction Payment Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(objreport);
        }
        public ActionResult GetEnrollmentReconstructionPaymentSummary(string dist, string vdc, string ward, string DateFrom, string DateTo)
        {

            ERPSummaryServices objService = new ERPSummaryServices();
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
            dt = objService.GetERPSummary(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("ERPSummary", objreport);
        }
       

    }
}
