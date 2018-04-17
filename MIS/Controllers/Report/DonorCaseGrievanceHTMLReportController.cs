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
//using ClosedXML.Excel;
using MIS.Services.CaseGrievence;
using MIS.Services.Report;
namespace MIS.Controllers.Report
{
    public class DonorCaseGrievanceHTMLReportController : BaseController
    {
        //
        // GET: /DonorCaseGrievanceHTMLReport/
        CommonFunction common = new CommonFunction();

        public ActionResult CaseGrievanceSummaryReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            ViewBag.ReportTitle = "Case Registration Summary Report";
            return View("CaseGrievanceSummaryReport", objreport);
        }
        public ActionResult GetSummaryReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            objreport.District = dist;
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", ward);
            if (GrievanceType.ConvertToString() != string.Empty)
                paramValues.Add("CaseType", GrievanceType);
            if (OtherHouse.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouse", OtherHouse);
            if (LegalOWner.ConvertToString() != string.Empty)
                paramValues.Add("LegalOWner", LegalOWner);
            if (DocType.ConvertToString() != string.Empty)
                paramValues.Add("DocType", DocType);
            if (CaseTypeFlag.ConvertToString() != string.Empty)
                paramValues.Add("CaseTypeFlag", CaseTypeFlag);
            if (LegalOwnerFlag.ConvertToString() != string.Empty)
                paramValues.Add("LegalOwnerFlag", LegalOwnerFlag);
            if (DocTypeFlag.ConvertToString() != string.Empty)
                paramValues.Add("DocTypeFlag", DocTypeFlag);
            if (OtherHouseFlag.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouseFlag", OtherHouseFlag);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetGrievanceSummaryReport(paramValues);
            Session["SummaryResults"] = dt;
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.CaseType = GrievanceType;
            objreport.LegalOwner = LegalOWner;
            objreport.OtherHouse = OtherHouse;
            objreport.DocType = DocType;

            if (CaseTypeFlag == "Y")
            {
                objreport.CaseTypeFlag = "Y";
            }
            if (LegalOwnerFlag == "Y")
            {
                objreport.LegalOwnerFlag = "Y";
            }
            if (OtherHouseFlag == "Y")
            {
                objreport.OtherHouseFlag = "Y";
            }
            if (DocTypeFlag == "Y")
            {
                objreport.DocTypeFlag = "Y";
            }

            ViewData["SummaryResults"] = dt;
            Session["CaseReportSummaryParams"] = objreport;
            return RedirectToAction("CaseGrievanceSummaryReport", objreport);
        }


        public ActionResult HandlingSummaryReport(HtmlReport objreport)
        {
            DataSet ds = null;
            ds = (DataSet)Session["Results"];
            ViewData["Results"] = ds;
            ViewBag.ReportTitle = "Grievance Review Summary Report";
            return View("HandlingSummaryReport", objreport);
        }
        public ActionResult GetGHSummaryReport(string dist, string vdc, string ward, string BatchID)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("CurVDC", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("CurWard", ward);
            if (BatchID.ConvertToString() != string.Empty)
                paramValues.Add("BatchID", BatchID);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.BATCHID = BatchID;
            Session["CaseReportSummaryParams"] = objreport;
            ds = service.GrievanceHandlingSummaryReport(paramValues);
            ViewData["Results"] = ds;
            Session["Results"] = ds;
            return RedirectToAction("HandlingSummaryReport", objreport);
        }


        public ActionResult GrievanceSummaryReportByBatch(HtmlReport objReport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Grievance Handling Summery Report";
            ViewData["SummaryResults"] = dt;
            return View(objReport);
        }
        public ActionResult GetGrievanceReviewedReportByBatchSummary(string dist, string vdc, string ward, string RecommendationType, string batch)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", ward);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
            if (batch.ConvertToString() != string.Empty)
                paramValues.Add("batch", batch);

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
            objreport.BATCHID = batch;
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedSummaryReportByBatch(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("GrievanceSummaryReportByBatch", objreport);
        }


        public ActionResult AllReportTypeSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Grievance Beneficiary";
            return View("AllReportTypeSummary", obj);
        }

        public ActionResult GrievanceBenfAllReportTypeSummary(string dist, string vdc, string ward)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport obj = new HtmlReport();
            obj.District = dist;
            obj.VDC = vdc;
            obj.Ward = ward;

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", ward);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetGrievanceBenefSummaryByAllRptType(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    obj.TotalSumEL = Convert.ToInt32(dt.Compute("Sum(TOTAL_EL)", ""));

            //}
            //else
            //{
            //    obj.TotalSumEL = "".ToInt32();

            //}
            ViewData["DetailResults"] = dt;
            return RedirectToAction("AllReportTypeSummary", obj);
        }




        public ActionResult GrievanceBenefSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Grievance Beneficiary Report";
            return View("GrievanceBenefSummary", obj);
        }

        public ActionResult GrievanceBeneficiarySummary(string dist, string vdc, string ward, string Batch, string currentVdc, string currentWard)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport obj = new HtmlReport();
            obj.District = dist;
            obj.VDC = vdc;
            obj.Ward = ward;
            if (vdc.ConvertToString() == "")
            {
                obj.VDC = null;
            }
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetGrievanceBenefSummary(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.TotalSumEL = Convert.ToInt32(dt.Compute("Sum(TOTAL_EL)", ""));

            }
            else
            {
                obj.TotalSumEL = "".ToInt32();

            }
            ViewData["DetailResults"] = dt;


            return RedirectToAction("GrievanceBenefSummary", obj);
        }




        public ActionResult BeneficiaryNonBeneficiaryReportSummary(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Reconstruction Beneficiary Summery Report";
            ViewData["DetailResults"] = dt;
            return View(objBenfMod);
        }
        public ActionResult getBeneficiaryBySummary(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Batch)
        {
            
            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
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
            dt = objBenfservice.BeneficiaryBySummary(paramValues);
            ViewData["DetailResults"] = dt;
            Session["DetailResults"] = dt;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("BeneficiaryNonBeneficiaryReportSummary", objBenfMod);

        }





        public ActionResult OwnerDetailSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Retrofitting Beneficiary Report";
            return View("OwnerDetailSummary", obj);
        }
        public ActionResult RetrofittingBeneficiarySummary(string dist, string vdc, string ward, string OthHouse)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport obj = new HtmlReport();

  
            obj.District = dist;
            obj.VDC = vdc;
            obj.Ward = ward;
            if (obj.VDC == "null")
            {
                obj.VDC = null;
            }
            
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", ward);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetRetrofittingBeneficiarySummary(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.TotalSumEL = Convert.ToInt32(dt.Compute("Sum(TOTAL_EL)", ""));

            }
            else
            {
                obj.TotalSumEL = "".ToInt32();

            }
            ViewData["DetailResults"] = dt;
            return RedirectToAction("OwnerDetailSummary", obj);
        }





        public ActionResult SurveyHouseSummary()
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewBag.ReportTitle = "Building Damage Report";
            ViewData["Results"] = dt;
            return View("SurveyHouseSummary");
        }
        public ActionResult GetHouseSummaryReport(string dist, string vdc, string ward, string DamageGrade, string TechnicalSolution, string OthHouse, string BuildingCondition)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            HtmlReport objreport = new HtmlReport();
            NameValueCollection paramValues = new NameValueCollection();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (DamageGrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", DamageGrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (BuildingCondition.ConvertToString() != string.Empty)
                paramValues.Add("BuildingCondition", BuildingCondition);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.DonorGetSurveyHouseSummary(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            objreport.TechnicalSolution = TechnicalSolution;
            objreport.DamageGrade = DamageGrade;
            objreport.OtherHouseFlag = OthHouse;
            objreport.BuildingCondition = BuildingCondition;
            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("SurveyHouseSummary");
        }


        public ActionResult HouseholdSummaryRpt(HtmlReport objreport)
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewBag.ReportTitle = "Household Summary Report";
            ViewData["Results"] = dt;
            return View("HouseholdSummaryRpt", objreport);
        }
        public ActionResult GetHouseholdSummaryRpt(string dist, string vdc, string ward )
        {
             HtmlReport objreport = new HtmlReport(); 
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.DonorHouseholdSummary(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
        
            Session["HouseReportParams"] = objreport;
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            return RedirectToAction("HouseholdSummaryRpt", objreport);
        }
    
    
    
    }
}
