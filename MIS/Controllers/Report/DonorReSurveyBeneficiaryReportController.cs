using EntityFramework;
using MIS.Models.Report;
using MIS.Services.BusinessCalculation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.IO;
using MIS.Services.Report;

namespace MIS.Controllers.Report
{
    public class DonorReSurveyBeneficiaryReportController : Controller
    {
        //
        // GET: /DonorReSurveyBeneficiaryReport/
        CommonFunction common = new CommonFunction();

        //Resurvey Reconstruction Beneficiary Report
        public ActionResult GetReconstructionBeneficiarySummary(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Batch)
        {

            BeneficiaryReSurveyDonorReportService objBenfservice = new BeneficiaryReSurveyDonorReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", ward);
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();

            objBenfMod.District = dist;
            objBenfMod.VDC = vdc;
            objBenfMod.WARD = ward;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            Session["BenfReportSummaryParams"] = objBenfMod;
            dt = objBenfservice.GetReSurveyBeneficiaryBySummary(paramValues);
            ViewData["ResultsResurveyBeneficiary"] = dt;
            Session["ResultsResurveyBeneficiary"] = dt;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("ReSurveyBenficiaryReportSummary", objBenfMod);

        }

        public ActionResult ReSurveyBenficiaryReportSummary(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["ResultsResurveyBeneficiary"];
            ViewBag.ReportTitle = "Re-Survey Reconstruction Beneficiary Summary Report";
            ViewData["ResultsResurveyBeneficiary"] = dt;
            return View(objBenfMod);
        }
    }
}
