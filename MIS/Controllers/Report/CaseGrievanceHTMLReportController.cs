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
using ClosedXML.Excel;

namespace MIS.Controllers.Report
{
    public class CaseGrievanceHTMLReportController : BaseController
    {
        
        // GET: /CaseGrievanceHTMLReport/
        CommonFunction common = new CommonFunction(); 
        public ActionResult GrievanceDailyStatus(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Daily Grievance Review Summary Status Report";
            return View("GrievanceDailyStatus", objreport);
        }
        public ActionResult GrievanceDailyStatusDtl(HtmlReport objreport)
        {
            int Index = objreport.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewBag.ReportTitle = "Daily Grievance Review Detail Status Report";
            return View("GrievanceDailyStatusDtl", objreport);
        }
        public ActionResult CaseGrievanceDetailReport(HtmlReport objreport)
        {
            int Index = objreport.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["DetailResults"] = dt;
            ViewBag.ReportTitle = "Case Registration Detail Report";
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            return View("CaseGrievanceDetailReport", objreport);
        }

        public List<SelectListItem> GetIndexNumber(string selectedValue, int index)
        {
            int i;
            List<SelectListItem> selLstCertificate = new List<SelectListItem>();
            //List<MISCommon> lstCommon = commonService.GetCertificate();
            //if (ispopup == false)
            //{
            //selLstCertificate.Add(new SelectListItem { Value = "", Text = "--Select Experience--" });
            // }
            for (i = 1; i <= index; i++)
            {
                selLstCertificate.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }
            selLstCertificate.Add(new SelectListItem { Value = "ALL", Text = "ALL" });
            foreach (SelectListItem item in selLstCertificate)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstCertificate;
        }
        public ActionResult RetrofittingBeneficiary(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["DetailResults"] = dt;
            ViewBag.ReportTitle = "Retrofitting Beneficiary";
            return View("RetrofittingBeneficiary", objreport);
        }
        public ActionResult CaseGrievanceSummaryReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            ViewBag.ReportTitle = "Case Registration Summary Report";
            return View("CaseGrievanceSummaryReport", objreport);
        }

        public ActionResult OwnerDetail(HtmlReport obj)
        {
            int Index = obj.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Retrofitting Beneficiary Detail Report";
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            return View("OwnerDetail", obj);
        }


        public ActionResult OwnerDetailSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Retrofitting Beneficiary Summary Report";
            return View("OwnerDetailSummary", obj);
        }
        public ActionResult GrievanceBenefSummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Grievance Beneficiary Summary Report";
            return View("GrievanceBeneficiarySummary", obj);
        }


        public ActionResult Grievancestatus()
        {
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            return View("Grievancestatus");
        }

        public ActionResult GrievanceHandlingReport(HtmlReport objreport)
        {
            int Index = objreport.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["Results"] = dt;
            ViewBag.ReportTitle = "Grievance Review Detail Report";
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            return View("GrievanceHandlingReport", objreport);
        }
        public ActionResult HandlingSummaryReport(HtmlReport objreport)
        {
            DataSet ds = null;
            ds = (DataSet)Session["Results"];
            ViewData["Results"] = ds;
            ViewBag.ReportTitle = "Grievance Review Summary Report";
            return View("HandlingSummaryReport", objreport);
        }
        public ActionResult GrievanceHandlingSummaryReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["SummaryResults"] = dt;
            return View("GrievanceHandlingSummaryReport", objreport);
        }
        public ActionResult GetSummaryReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag, string currentVdc, string currentWard)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;
            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;
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
            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetGrievanceSummaryReport(paramValues);
            Session["SummaryResults"] = dt;
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

        public ActionResult GetGrievanceDetailReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag, string pagesize, string pageindex, string currentVdc, string currentWard,string exportcheck)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            if (exportcheck.ConvertToString() != string.Empty)
                paramValues.Add("exportcheck", exportcheck);




            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            dt = service.GrievanceDetail(paramValues);

            int tol = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                int idx = tol % 100;
                if (idx > 0)
                {
                    objreport.Index = (tol / 100) + 1;
                }
                else
                {
                    objreport.Index = 1;
                }



                objreport.Remaining = tol % 100;
                if (pageindex != null && pageindex!="ALL")
                {
                    objreport.showing = Convert.ToInt32(pageindex) * 100;
                    if (objreport.showing > tol)
                    {
                        objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                    }
                }
                else
                    if (objreport.Index == 1)
                    {
                        if (objreport.showing > tol || objreport.Index == 1)
                        {
                            objreport.showing = tol;


                        }

                    }
                    else
                    {
                        if(pageindex !="ALL")
                        { 
                        objreport.showing = Convert.ToInt32(1) * 100;
                        }
                        else
                        {
                            objreport.showing = objreport.Total;

                        }

                    }

            }

            ViewData["Results"] = dt;
            Session["Results"] = dt;
            objreport.CaseType = GrievanceType;
            objreport.LegalOwner = LegalOWner;
            objreport.OtherHouse = OtherHouseFlag;
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
            ViewData["DetailResults"] = dt;
            Session["CaseReportParams"] = objreport;
            return RedirectToAction("CaseGrievanceDetailReport", objreport);
        }

        [HttpPost]
        public ActionResult GetGrievanceDetailReport3(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag, string pagesize, string pageindex)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            //BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);

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
            objreport.CaseType = GrievanceType;
            objreport.LegalOwner = LegalOWner;
            objreport.OtherHouse = OtherHouseFlag;
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
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = service.GrievanceDetail(paramValues);
               int tol = 0;
               if (dt != null && dt.Rows.Count > 0)
               {
                    tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                   int idx = tol % 100;
                   if (idx > 0)
                   {
                       objreport.Index = (tol / 100) + 1;
                   }
                   else
                   {
                       objreport.Index = (tol / 100);
                   }



                   objreport.Remaining = tol % 100;
                   if (pageindex != null && pageindex !="ALL")
                   {
                       objreport.showing = Convert.ToInt32(pageindex) * 100;
                       if (objreport.showing > tol)
                       {
                           objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                       }
                   }
                   else
                   {
                       if(pageindex != "ALL")
                       { 
                       objreport.showing = Convert.ToInt32(1) * 100;
                       }
                       else
                       {
                           objreport.showing = objreport.Total;
                       }
                   }
               }
            ViewData["DetailResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            ViewData["DetailResults"] = dt;
            //return View(objReport);
            return PartialView("~/Views/HTMLReport/_CaseDetailReport.cshtml", objreport);
        }
        public ActionResult GetGrievanceHandlingReport(string dist, string vdc, string ward, string BatchID, string GHDataFlag, string CurVDC, string CurWard, string pagesize, string pageindex)
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
            if (BatchID.ConvertToString() != string.Empty)
                paramValues.Add("BatchID", BatchID);
            if (GHDataFlag.ConvertToString() != string.Empty)
                paramValues.Add("GHDataFlag", GHDataFlag);
            if (CurVDC.ConvertToString() != string.Empty)
                paramValues.Add("CurVDC", common.GetCodeFromDataBase(CurVDC, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (CurWard.ConvertToString() != string.Empty)
                paramValues.Add("CurWard", CurWard);
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);
            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            objreport.District = dist;
            objreport.VDC = CurVDC;
            objreport.Ward = CurWard;
            objreport.BATCHID = BatchID;
            objreport.CurrentVDC = CurVDC;
            objreport.CurrentWard = CurWard;

            Session["CaseReportSummaryParams"] = objreport;
            dt = service.GrievanceHandlingDetail(paramValues);
            int tol = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                int idx = tol % 100;
                if (idx > 0)
                {
                    objreport.Index = (tol / 100) + 1;
                }
                else
                {
                    objreport.Index = 1;
                }



                objreport.Remaining = tol % 100;
                if (pageindex != null)
                {
                    objreport.showing = Convert.ToInt32(pageindex) * 100;
                    if (objreport.showing > tol)
                    {
                        objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                    }
                }
                else
                    if (objreport.Index == 1)
                    {
                        if (objreport.showing > tol || objreport.Index == 1)
                        {
                            objreport.showing = tol;


                        }

                    }
                    else
                    {
                        objreport.showing = Convert.ToInt32(1) * 100;

                    }

            }
            ViewData["Results"] = dt;
            Session["Results"] = dt;

            ViewData["DetailResults"] = dt;
            return RedirectToAction("GrievanceHandlingReport", objreport);
        }


        [HttpPost]
        public ActionResult GetGrievanceHandlingReport3(string dist, string vdc, string ward, string BatchID, string GHDataFlag, string pagesize, string pageindex)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            //BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BatchID.ConvertToString() != string.Empty)
                paramValues.Add("BatchID", BatchID);
            if (GHDataFlag.ConvertToString() != string.Empty)
                paramValues.Add("GHDataFlag", GHDataFlag);
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);

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
            objreport.BATCHID = BatchID;


            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = service.GrievanceHandlingDetail(paramValues);
            int tol = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                int idx = tol % 100;
                if (idx > 0)
                {
                    objreport.Index = (tol / 100) + 1;
                }
                else
                {
                    objreport.Index = (tol / 100);
                }



                objreport.Remaining = tol % 100;
                if (pageindex != null && pageindex != "ALL")
                {
                    objreport.showing = Convert.ToInt32(pageindex) * 100;
                    if (objreport.showing > tol)
                    {
                        objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                    }
                }
                else
                {
                    if (pageindex != "ALL")
                    {
                        objreport.showing = Convert.ToInt32(1) * 100;
                    }
                    else
                    {
                        objreport.showing = objreport.Total;

                    }
                }
            }
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            

            ViewData["Results"] = dt;
            
            return PartialView("~/Views/HTMLReport/_GrievanceHandlingHTMLReport.cshtml", objreport);
        }
        public ActionResult RetrofittingBeneficiaryDetail(string dist,  string OthHouse, string pagesize, string pageindex, string currentVdc, string currentWard, string Batch,string checkexport)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport obj = new HtmlReport();
            obj.District = dist;
            obj.VDC = currentVdc;
            obj.Ward = currentWard;

            if (Batch.ConvertToString() == "0")
                Batch = "";

            obj.BATCHID = Batch;

            obj.CurrentVDC = currentVdc;
            obj.CurrentWard = currentWard;

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
      
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);

         

            
            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);
            if (checkexport.ConvertToString() != string.Empty)
                paramValues.Add("checkexport", checkexport);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetRetrofittingBeneficiaryDetail(paramValues);
 
            ViewData["Results"] = dt;
            Session["Results"] = dt; 
            ViewData["DetailResults"] = dt;
            Session["retroparams"] = obj;
            return RedirectToAction("OwnerDetail", obj);
        }

        [HttpPost]
        public ActionResult RetrofittingBeneficiaryDetail3(string dist, string vdc, string ward, string Batch, string pagesize, string pageindex)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            //BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);

            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);

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
            objreport.BATCHID = Batch;


            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = service.GetRetrofittingBeneficiaryDetail(paramValues);
            int tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            objreport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            int idx = tol % 100;
            if (idx > 0)
            {
                objreport.Index = (tol / 100) + 1;
            }
            else
            {
                objreport.Index = (tol / 100);
            }



            objreport.Remaining = tol % 100;
            if (pageindex != null && pageindex !="ALL")
            {
                objreport.showing = Convert.ToInt32(pageindex) * 100;
                if (objreport.showing > tol)
                {
                    objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                }
            }
            else
            {
                if(pageindex !="ALL")
                { 
                objreport.showing = Convert.ToInt32(1) * 100;
                }
                else
                {
                    objreport.showing = objreport.Total;
                }
            }
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            ViewData["Results"] = dt;
            //return View(objReport);
            return PartialView("~/Views/HTMLReport/_GrievanceNonBeneficiaryHtmlReport.cshtml", objreport);
        }
        public ActionResult RetrofittingBeneficiarySummary(string dist,string Batch, string currentVdc, string currentWard)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport obj = new HtmlReport();
            obj.District = dist;
            obj.VDC = currentVdc;
            obj.Ward = currentWard;
            obj.CurrentVDC = currentVdc;
            obj.CurrentWard = currentWard;

           

            if (Batch.ConvertToString() == "0")
                Batch = "";

            obj.BATCHID = Batch;
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
             
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);

           

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

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
            Session["retrosummaryparams"] = obj;
            return RedirectToAction("OwnerDetailSummary", obj);
        }
        public ActionResult GrievanceBeneficiarySummary(string dist, string vdc, string ward)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
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


        public DataTable GetGrievanceNonBeneficiaryDataTable(string dist, string vdc, string ward, string OthHouse)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (OthHouse.ConvertToString() != string.Empty)
                paramValues.Add("OthHouse", OthHouse);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GetRetrofittingBeneficiaryDetailForExport(paramValues);
            //ViewData["Results"] = dt;
            //Session["Results"] = dt;

            //ViewData["DetailResults"] = dt;
            return dt;
            //return RedirectToAction("OwnerDetail", obj);
        }


        public ActionResult GetGrievanceStatus(string dist, string vdc, string ward)
        {
            OwnerSummaryService service = new OwnerSummaryService();
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
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.getGrievanceStatus(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;

            ViewData["DetailResults"] = dt;
            return RedirectToAction("GrievanceStatus");
        }
        public ActionResult GetDailyGrievanceStatus(string DateFrom, string DateTo)
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
            if (DateFrom.ConvertToString() != string.Empty)

                paramValues.Add("DateFrom", DateFrom);
            if (DateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", DateTo);
            DataTable dt = new DataTable();

            dt = service.getDaliyGrievanceStatus(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;


            ViewData["DetailResults"] = dt;
            return RedirectToAction("GrievanceDailyStatus");
        }
        public ActionResult GetDailyGrievanceStatusDtl(string DateFrom, string DateTo, string pagesize, string pageindex,string exportcheck)
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
            if (DateFrom.ConvertToString() != string.Empty)
                paramValues.Add("DateFrom", DateFrom);
            if (DateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", DateTo);
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);

            if (exportcheck.ConvertToString() != string.Empty)
                paramValues.Add("exportcheck", exportcheck);

            objreport.DateFrom = DateFrom;
            objreport.DateTo = DateTo;

            DataTable dt = new DataTable();
            dt = service.getDaliyGrievanceStatusDtl(paramValues);

            int tol = Convert.ToInt32(dt.Rows[0][13]);
            objreport.Total = Convert.ToInt32(dt.Rows[0][13]);
            int idx = tol % 100;
            if (idx > 0)
            {
                objreport.Index = (tol / 100) + 1;
            }
            else
            {
                objreport.Index = (tol / 100);
            }



            objreport.Remaining = tol % 100;
            if (pageindex != null)
            {
                objreport.showing = Convert.ToInt32(pageindex) * 100;
                if (objreport.showing > tol)
                {
                    objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                }
            }
            else
            {
                objreport.showing = Convert.ToInt32(1) * 100;
            }

            ViewData["Results"] = dt;
            Session["Results"] = dt;

            ViewData["DetailResults"] = dt;
            Session["dailygrievanceparams"] = objreport;
            return RedirectToAction("GrievanceDailyStatusDtl", objreport);
        }

        [HttpPost]
        public ActionResult GetDailyGrievanceStatusDtl3(string DateFrom, string DateTo, string pagesize, string pageindex)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            //BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (DateFrom.ConvertToString() != string.Empty)
                paramValues.Add("DateFrom", DateFrom);
            if (DateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", DateTo);
            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);



            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            objreport.DateFrom = DateFrom;
            objreport.DateTo = DateTo;


            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = service.getDaliyGrievanceStatusDtl(paramValues);
            int tol = Convert.ToInt32(dt.Rows[0][13]);
            objreport.Total = Convert.ToInt32(dt.Rows[0][13]);
            int idx = tol % 100;
            if (idx > 0)
            {
                objreport.Index = (tol / 100) + 1;
            }
            else
            {
                objreport.Index = (tol / 100);
            }



            objreport.Remaining = tol % 100;
            if (pageindex != null)
            {
                objreport.showing = Convert.ToInt32(pageindex) * 100;
                if (objreport.showing > tol)
                {
                    objreport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objreport.Remaining;


                }
            }
            else
            {
                objreport.showing = Convert.ToInt32(1) * 100;
            }
            ViewData["Results"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            ViewData["Results"] = dt;
            //return View(objReport);
            return PartialView("~/Views/HTMLReport/_GrievanceDailyStatusDetailReport.cshtml", objreport);
        }

        public ActionResult GetGHSummaryReport(string dist, string vdc, string ward, string BatchID, string CurVDC, string CurWard)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BatchID.ConvertToString() != string.Empty)
                paramValues.Add("BatchID", BatchID);
            if (CurVDC.ConvertToString() != string.Empty)
                paramValues.Add("CurVDC", common.GetCodeFromDataBase(CurVDC, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (CurWard.ConvertToString() != string.Empty)
                paramValues.Add("CurWard", CurWard);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            objreport.District = dist;
            objreport.VDC = CurVDC;
            objreport.Ward = CurWard;
            objreport.BATCHID = BatchID;
            objreport.CurrentVDC = CurVDC;
            objreport.CurrentWard = CurWard;

            Session["CaseReportSummaryParams"] = objreport;
            ds = service.GrievanceHandlingSummaryReport(paramValues);
            ViewData["Results"] = ds;
            Session["Results"] = ds;
            return RedirectToAction("HandlingSummaryReport", objreport);
        }

        public ActionResult GetGrievanceHandlingSummaryReport(string dist, string vdc, string ward, string NissaNo, string GrievantName, string HouseSerialNo, string FormNo, string RegistrationNo, string GrievanceType, string OtherHouse,
           string OtherHouseCondition, string damagegrade, string TechnicalSolution, string PANum, string SurveyedGrade, string PhotoGrade, string MatrixGarde, string RecommendedGrade,
           string SurveyedTechnicalSolution, string RecommendedTechnicalSolution, string DG1, string DG2, string DG3, string DG4, string DG5, string DG6, string TS1, string TS2, string TS3, string TS4, string TS5)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (NissaNo.ConvertToString() != string.Empty)
                paramValues.Add("nissa", NissaNo);
            if (GrievantName.ConvertToString() != string.Empty)
                paramValues.Add("grievantname", GrievantName);
            if (HouseSerialNo.ConvertToString() != string.Empty)
                paramValues.Add("houseserial", HouseSerialNo);
            if (FormNo.ConvertToString() != "")
                paramValues.Add("formno", FormNo);
            if (RegistrationNo.ConvertToString() != "")
                paramValues.Add("registerno", RegistrationNo);
            if (GrievanceType.ConvertToString() != string.Empty)
                paramValues.Add("CaseType", GrievanceType);
            if (OtherHouse.ConvertToString() != string.Empty)
                paramValues.Add("OtherHouse", OtherHouse);
            if (OtherHouseCondition.ConvertToString() != string.Empty)
                paramValues.Add("OthHouseCondition", OtherHouseCondition);
            if (damagegrade.ConvertToString() != string.Empty)
                paramValues.Add("DamageGrade", damagegrade);
            if (TechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("TechnicalSolution", TechnicalSolution);
            if (PANum.ConvertToString() != string.Empty)
                paramValues.Add("PANum", PANum);
            if (SurveyedGrade.ConvertToString() != string.Empty)
                paramValues.Add("SurveyedGrade", SurveyedGrade);
            if (PhotoGrade.ConvertToString() != string.Empty)
                paramValues.Add("PhotoGrade", PhotoGrade);
            if (MatrixGarde.ConvertToString() != string.Empty)
                paramValues.Add("MatrixGarde", MatrixGarde);
            if (RecommendedGrade.ConvertToString() != string.Empty)
                paramValues.Add("RecommendedGrade", RecommendedGrade);
            if (SurveyedTechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("SurveyedTechnicalSolution", SurveyedTechnicalSolution);
            if (RecommendedTechnicalSolution.ConvertToString() != string.Empty)
                paramValues.Add("RecommendedTechnicalSolution", RecommendedTechnicalSolution);
            if (DG1.ConvertToString() != string.Empty)
                paramValues.Add("DG1", DG1);
            if (DG2.ConvertToString() != string.Empty)
                paramValues.Add("DG2", DG2);
            if (DG3.ConvertToString() != string.Empty)
                paramValues.Add("DG3", DG3);
            if (DG4.ConvertToString() != string.Empty)
                paramValues.Add("DG4", DG4);
            if (DG5.ConvertToString() != string.Empty)
                paramValues.Add("DG5", DG5);
            if (DG5.ConvertToString() != string.Empty)
                paramValues.Add("DG5", DG5);
            if (DG6.ConvertToString() != string.Empty)
                paramValues.Add("DG6", DG6);
            if (TS1.ConvertToString() != string.Empty)
                paramValues.Add("TS1", TS1);
            if (TS2.ConvertToString() != string.Empty)
                paramValues.Add("TS2", TS2);
            if (TS3.ConvertToString() != string.Empty)
                paramValues.Add("TS3", TS3);
            if (TS4.ConvertToString() != string.Empty)
                paramValues.Add("TS4", TS4);
            if (TS5.ConvertToString() != string.Empty)
                paramValues.Add("TS5", TS5);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            dt = service.GrievanceHandlingSummary(paramValues);
            if (damagegrade != "undefined")
            {
                string[] DamageGrade = damagegrade.Split(',');
                objreport.GradeCount = DamageGrade.Count();
            }
            if (TechnicalSolution != "undefined")
            {
                string[] TS = TechnicalSolution.Split(',');
                objreport.TSCount = TS.Count();
            }
            if (damagegrade != "")
            {
                objreport.DamageGrade = "Y";

            }
            if (TechnicalSolution != "")
            {
                objreport.TechnicalSolution = "Y";

            }
            if (SurveyedGrade == "Y")
            {
                objreport.SurveyedGrade = "Y";
            }
            if (PhotoGrade == "Y")
            {
                objreport.PhotoGrade = "Y";
            }
            if (MatrixGarde == "Y")
            {
                objreport.MatrixGrade = "Y";
            }
            if (RecommendedGrade == "Y")
            {
                objreport.OfficeGrade = "Y";
            }
            if (SurveyedTechnicalSolution == "Y")
            {
                objreport.SurveyedTS = "Y";
            }
            if (RecommendedTechnicalSolution == "Y")
            {
                objreport.RecommendedTS = "Y";
            }

            if (DG1 != "")
            {
                objreport.Grade1 = DG1;
            }
            if (DG2 != "")
            {
                objreport.Grade2 = DG2;
            }
            if (DG3 != "")
            {
                objreport.Grade3 = DG3;
            }
            if (DG4 != "")
            {
                objreport.Grade4 = DG4;
            }
            if (DG5 != "")
            {
                objreport.Grade5 = DG5;
            }
            if (DG6 != "")
            {
                objreport.Grade6 = DG6;
            }
            if (TS1 != "")
            {
                objreport.TS1 = TS1;
            }
            if (TS2 != "")
            {
                objreport.TS2 = TS2;
            }
            if (TS3 != "")
            {
                objreport.TS3 = TS3;
            }
            if (TS4 != "")
            {
                objreport.TS4 = TS4;
            }
            if (TS5 != "")
            {
                objreport.TS5 = TS5;
            }

            ViewData["Results"] = dt;
            Session["Results"] = dt;

            ViewData["DetailResults"] = dt;
            return RedirectToAction("GrievanceHandlingSummaryReport", objreport);
        }


        [HttpPost]
        public ActionResult ExportPdf()
        {
            string usercd = SessionCheck.getSessionUserCode();
            string htmlFilePath = string.Empty, pdfFilePath = string.Empty;
            if (usercd != "")
            {
                htmlFilePath = Server.MapPath("/files/html/") + "CaseGrievance" + usercd + ".html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "CaseGrievance" + usercd + ".pdf";
            }
            else
            {
                htmlFilePath = Server.MapPath("/files/html/") + "CaseGrievance.html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "CaseGrievance.pdf";
            }

            string html = RenderDetailPartialViewToString("~/Views/HTMLReport/_CaseDetailReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Report"), html);
            html = ReportTemplate.GetReportHTML(html);

            Utils.CreateFile(html, htmlFilePath);
            PdfGenerator.ConvertToPdf(htmlFilePath, pdfFilePath);
            return File(pdfFilePath, "application/pdf", "CaseGrievanceReport.pdf");
        }

        protected string RenderDetailPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["DetailResults"] = dt;
                rptParams = (HtmlReport)Session["CaseReportParams"];
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

        public ActionResult ExportGrievanceHandlingDetail(string dist, string vdc, string ward, string BatchID, string GHDataFlag)
        {

            DataTable dt = new DataTable();
            //dt = GetGrievanceHandlingReportDataTable(dist, vdc, ward, BatchID, GHDataFlag);
            //return ExportGrievanceHandlingDetailExcel(dt);
            GetGrievanceHandlingReportDataTable(dist, vdc, ward, BatchID, GHDataFlag);
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["CaseReportSummaryParams"];
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "GrievanceReviewedDetail.xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "GrievanceReviewedDetail.xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_GrievanceHandlingHTMLReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Handling Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "GrievanceReviewedData.xls" );
        }
        //Export to excel
        public DataTable GetGrievanceHandlingReportDataTable(string dist, string vdc, string ward, string BatchID, string GHDataFlag)
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
            if (BatchID.ConvertToString() != string.Empty)
                paramValues.Add("BatchID", BatchID);
            if (GHDataFlag.ConvertToString() != string.Empty)
                paramValues.Add("GHDataFlag", GHDataFlag);
            DataTable dt = new DataTable();

            dt = service.GrievanceHandlingDetail(paramValues);
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.BATCHID = BatchID;
            Session["CaseReportSummaryParams"] = objreport;
            Session["Results"] = dt;
            return dt;
        }



        public ActionResult ExportGrievanceHandlingDetailExcel(DataTable dt)
        {
            foreach (DataRow drr in dt.Rows)
            {
                if (drr["RECOMENDATION_FLAG"].ConvertToString() == "NB")
                {
                    drr["RECOMENDATION_FLAG"] = "Non Beneficiary";
                }
                if (drr["RECOMENDATION_FLAG"].ConvertToString() == "LG")
                {
                    drr["RECOMENDATION_FLAG"] = "Local Grievances";
                }
                if (drr["RECOMENDATION_FLAG"].ConvertToString() == "RB")
                {
                    drr["RECOMENDATION_FLAG"] = "Retrofitting Beneficiary";
                }
                if (drr["RECOMENDATION_FLAG"].ConvertToString() == "B")
                {
                    drr["RECOMENDATION_FLAG"] = "Beneficiary";
                }
                if (drr["RECOMENDATION_FLAG"].ConvertToString() == "FO")
                {
                    drr["RECOMENDATION_FLAG"] = "Field Observation";
                }
                if (drr["RECOMENDATION_FLAG"].ConvertToString() == "NF")
                {
                    drr["RECOMENDATION_FLAG"] = "Not Found";
                }



                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "HO")
                {
                    drr["CLARIFICATION_FLAG"] = "Habitable House In Other Place";

                }
                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "HU")
                {
                    drr["CLARIFICATION_FLAG"] = "Habitable House In Usual Place";

                }
                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "CB")
                {
                    drr["CLARIFICATION_FLAG"] = "House Grade Changed to Non Beneficiary";
                }
                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "PB")
                {
                    drr["CLARIFICATION_FLAG"] = "Potential Beneficiary";
                }
                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "NB")
                {
                    drr["CLARIFICATION_FLAG"] = "Probable Beneficiary";
                }
                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "FO")
                {
                    drr["CLARIFICATION_FLAG"] = "Matrix & Photo not cleared or mismatch";
                }
                if (drr["CLARIFICATION_FLAG"].ConvertToString() == "NF")
                {
                    drr["CLARIFICATION_FLAG"] = "Household Not Identified";
                }
                if (drr["RETROFITING_TARGET_FLAG"].ConvertToString() == "EL")
                {
                    drr["RETROFITING_TARGET_FLAG"] = "Yes";
                }
                if (drr["RETROFITING_TARGET_FLAG"].ConvertToString() == "NE")
                {
                    drr["RETROFITING_TARGET_FLAG"] = "No";
                }
            }

            //DataRow dr = dt.NewRow();
            //DataRow dr1 = dt.NewRow();
            //DataRow dr2 = dt.NewRow();
            using (XLWorkbook wb = new XLWorkbook())
            {

                //for(int i=0;i<3;i++)
                //{

                //dt.Rows.InsertAt(dr,1);
                //dt.Rows.InsertAt(dr1, 0);
                //dt.Rows.InsertAt(dr2, 0);
                //dt.Rows.Add(dr, 4);

                //}
                //DataRow newrow = dt.NewRow();
                //newrow[0] = "0";
                //dt.Rows.InsertAt(newrow, 0);
                ////DataRow newrow1 = dt.NewRow();
                //newrow1[1] = "1";
                //dt.Rows.InsertAt(newrow, 1);
                ////DataRow newrow2 = dt.NewRow();
                ////newrow2[2] = "2";
                //dt.Rows.InsertAt(newrow, 2);

                var ws = wb.Worksheets.Add(dt);

                ws.Columns("A,D,F,H,N,O,V,X,BD,BC,BB,BA,AZ,AB,AC,AD,AE,AF,AG,AH,AI,AJ,AK,AL").Delete();
                ws.Columns("I,S,V,W,X,R").Delete();
                // ws.Columns("B,V,W,X,AA,R").Delete();
                //ws.Row(132).Height = 90;
                //ws.Row(132).Merge();
                //ws.Row(132).Style.Font.FontColor = XLColor.Red;
                //ws.Row(132).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                //ws.Row(132).Style.Font.FontSize = 20;
                //ws.Row(132).Value = "Government of Nepal";


                //ws.Row(1).Value = "Government of Nepal";
                //ws.Row(1).Merge();
                //ws.Row(1).Style.Font.FontColor = XLColor.Red;
                //ws.Row(2).Value = "National Reconstruction Authority";
                //ws.Row(2).Merge();
                //ws.Row(2).Style.Font.FontColor = XLColor.Red;
                //ws.Row(3).Value = "SinghDurbar,Kathmandu";
                //ws.Row(3).Merge();
                //ws.Row(3).Style.Font.FontColor = XLColor.Red;
                //ws.Row(1).Style.Font.FontSize = 20;
                //ws.Row(2).Style.Font.FontSize = 20;
                //ws.Row(3).Style.Font.FontSize = 20;
                //ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                //ws.Row(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                //ws.Row(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                //ws.Column("Q").Delete();
                ws.Cell("F1").Value = "WARD";
                ws.Cell("J1").Value = "STRUCTURE_COUNT";
                ws.Cell("I1").Value = "GRIEVANCE_TYPE";
                ws.Cell("K1").Value = "SURVEYED_DETAIL_DAMAGE_GRADE";

                ws.Cell("O1").Value = "DATA_GRADE_AFTER_OBSERVATION";
                ws.Cell("P1").Value = "DATA_TECHNICAL_SOLUTION";
                ws.Cell("Y1").Value = "RECOMMENTDATION";
                ws.Cell("Z1").Value = "CLARIFICATION";
                ws.Cell("X1").Value = "RETROFITTING";






                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GrievanceReviewReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportGrievanceHandlingDetailExcel");
        }

        //Export to excel end

        public ActionResult ExportGrievanceHandlingSummary(string dist, string vdc, string ward, string BatchID, string GHDataFlag)
        {
            DataSet ds = new DataSet();
            ds = GetGHSummaryReportDatatable(dist, vdc, ward, BatchID);
            return ExportDataGrievanceHandling(ds);
            // GetGHSummaryReport(dist, vdc, ward, BatchID);
            //ViewData["ExportFont"] = "font-size: 13px";
            ////HtmlReport rptParams = new HtmlReport();
            ////rptParams = (HtmlReport)Session["CaseReportSummaryParams"];
            ////BatchID = rptParams.BATCHID;
            ////dist = rptParams.District;
            ////vdc = rptParams.VDC;
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "GrievanceReviewedSummary" +"( District "+ dist +" ) ( Batch"+BatchID+ " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "GrievanceReviewedSummary" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            //}
            //string html = RenderSummaryPartialViewToString("~/Views/HTMLReport/_GrievanceHandlingTotalSummary.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Grievance Reviewed Summary Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "GrievanceReviewedSummary" +"( District "+ dist +" ) ( Batch"+BatchID+ " ).xls");
        }

        //Export to excel Start
        public DataSet GetGHSummaryReportDatatable(string dist, string vdc, string ward, string BatchID)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BatchID.ConvertToString() != string.Empty)
                paramValues.Add("BatchID", BatchID);
            DataSet ds = new DataSet();
            ds = service.GrievanceHandlingSummaryReport(paramValues);

            return ds;
        }
        public ActionResult ExportDataGrievanceHandling(DataSet ds)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(ds);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GrievanceReviewReportSummary.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportDataGrievanceHandling");
        }
        // Export to excel End


        [HttpPost]
        public ActionResult ExportPdfSummaryResults()
        {
            string usercd = SessionCheck.getSessionUserCode();
            string htmlFilePath = string.Empty, pdfFilePath = string.Empty;
            if (usercd != "")
            {
                htmlFilePath = Server.MapPath("/files/html/") + "CaseGrievanceSummary" + usercd + ".html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "CaseGrievanceSummary" + usercd + ".pdf";
            }
            else
            {
                htmlFilePath = Server.MapPath("/files/html/") + "CaseGrievanceSummary.html";
                pdfFilePath = Server.MapPath("/files/pdf/") + "CaseGrievanceSummary.pdf";
            }

            string html = RenderPartialViewToString("~/Views/HTMLReport/_SummaryReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Report"), html);
            html = ReportTemplate.GetReportHTML(html);

            Utils.CreateFile(html, htmlFilePath);
            PdfGenerator.ConvertToPdf(htmlFilePath, pdfFilePath);
            return File(pdfFilePath, "application/pdf", "CaseGrievanceSummaryReport.pdf");
        }

        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["Results"] = dt;
                rptParams = (HtmlReport)Session["CaseReportSummaryParams"];
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

        protected string RenderSummaryPartialViewToString(string viewName)
        {
            DataSet dtbl = new DataSet();
            HtmlReport rptParams = new HtmlReport();
            if ((DataSet)Session["Results"] != null)
            {
                DataSet dt = (DataSet)Session["Results"];
                ViewData["Results"] = dt;
                rptParams = (HtmlReport)Session["CaseReportSummaryParams"];
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
        public ActionResult GetGrievanceRetrofittingBeneficiary(string dist, string vdc, string ward)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            DataTable dt = new DataTable();
            dt = service.GrievanceRetrofittingBeneficiary(paramValues);
            Session["Results"] = dt;
            return RedirectToAction("RetrofittingBeneficiary", objreport);
        }
        public ActionResult ExportDailyGrievanceStatusReport(string DateFrom, string DateTo)
        {
            DataTable dt = new DataTable();
            dt = GetDailyGrievanceStatusDataTable(DateFrom, DateTo);
            return ExportDataDailyGrievanceReviewSummary(dt);

            //GetDailyGrievanceStatus(DateFrom, DateTo);
            //ViewData["ExportFont"] = "font-size: 13px";
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Daily_Grievance_Status_Summary" + "( DateFrom " + DateFrom + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Daily_Grievance_Status_Summary" + "( DateTo " + DateTo + " ) .xls";
            //}
            //string html = RenderPartialViewsToString("~/Views/HTMLReport/_GrievanceDailyStatusReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Daily Grievance Status Summary Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Daily_Grievance_Status_Summary" + "( DateFrom " + DateFrom + " ).xls");
        }

        //Export to excel Start

        public DataTable GetDailyGrievanceStatusDataTable(string DateFrom, string DateTo)
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
            if (DateFrom.ConvertToString() != string.Empty)

                paramValues.Add("DateFrom", DateFrom);
            if (DateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", DateTo);
            DataTable dt = new DataTable();

            dt = service.getDaliyGrievanceStatus(paramValues);

            return dt;
        }


        public ActionResult ExportDataDailyGrievanceReviewSummary(DataTable dt)
        {
            foreach (DataRow drr in dt.Rows)
            {
                if (drr["NF"].ConvertToString() == "NF")
                {
                    drr["NF"] = "Data Not Found";
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);
                ws.Columns("E,F,G,H,I,J,K").Delete();
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                ws.Cell("A1").Value = "DATA NOT FOUND";
                ws.Cell("B1").Value = "REVIEWED";

                //  ws.Column("C").CopyTo[]
                //ws.Cell("A21").Value = ws.Cell("C1");
                //ws.Cell("B21").Value = ws.Cell("C2");
                //ws.Column("C").Delete();
                //ws.Cell("A22").Value = ws.Cell("C1");
                //ws.Cell("B22").Value = ws.Cell("C2");
                //ws.Column("C").Delete();

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= DailyGrievanceReviewStatus.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportDataDailyGrievanceReviewSummary");
        }
        //Export to excel End

        protected string RenderPartialViewsToString(string viewName)
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

        public ActionResult ExportDailyGrievanceStatusDtlReport(string DateFrom, string DateTo)
        {
            DataTable dt = new DataTable();
            dt = GetDailyGrievanceStatusDtlDataTable(DateFrom, DateTo);
            return ExportDataDailyGrievanceReview(dt);

            //GetDailyGrievanceStatusDtl(DateFrom, DateTo);
            //ViewData["ExportFont"] = "font-size: 13px";
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Daily_Grievance_Status_Detail" + "( DateFrom " + DateFrom + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Daily_Grievance_Status_Detail" + "( DateTo " + DateTo + " ) .xls";
            //}
            //string html = RenderPartialViewssToString("~/Views/HTMLReport/_GrievanceDailyStatusDetailReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Daily Grievance Status Detail  Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Daily_Grievance_Status_Detail" + "( DateFrom " + DateFrom + " ).xls");
        }
        //Export to Excel Start
        public DataTable GetDailyGrievanceStatusDtlDataTable(string DateFrom, string DateTo)
        {
            OwnerSummaryService service = new OwnerSummaryService();
            NameValueCollection paramValues = new NameValueCollection();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            if (DateFrom.ConvertToString() != string.Empty)
                paramValues.Add("DateFrom", DateFrom);
            if (DateTo.ConvertToString() != string.Empty)
                paramValues.Add("DateTo", DateTo);
            DataTable dt = new DataTable();
            dt = service.getDaliyGrievanceStatusDtl(paramValues);

            return dt;
        }


        public ActionResult ExportDataDailyGrievanceReview(DataTable dt)
        {
            foreach (DataRow drr in dt.Rows)
            {
                if (drr["CASE_STATUS"].ConvertToString() == "GH")
                {
                    drr["CASE_STATUS"] = "Reviewed";

                }
                else
                {
                    drr["CASE_STATUS"] = "Data Not Found";
                }
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);
                ws.Columns("A,C,E").Delete();
                ws.Cell("D1").Value = "WARD";
                ws.Cell("H1").Value = "REVIEWED_TYPE";
                ws.Cell("F1").Value = "REVIEWED_BY";
                ws.Cell("G1").Value = "REVIEWED_DATE";

                // wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= DailyGrievanceReviewStatus.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportDataDailyGrievanceReview");
        }

        //Export to excel End
        protected string RenderPartialViewssToString(string viewName)
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
        public ActionResult ExportGrievanceNonBeneficiaryReport(string dist, string vdc, string ward, string OthHouse)
        {

            DataTable dt = new DataTable();
            dt = GetGrievanceNonBeneficiaryDataTable(dist, vdc, ward, OthHouse);

            return ExportData(dt);
            //ViewData["ExportFont"] = "font-size: 13px";
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Grievance_Non_Beneficiary_Report" + "( District " + dist + " ) ( Vdc " + vdc + " ) ( Ward " + ward + " ) ( OthHouse " + OthHouse + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Grievance_Non_Beneficiary_Report" + "( District " + dist + " ) ( Vdc " + vdc + " ) ( Ward " + ward + " ) ( OthHouse " + OthHouse + " ) .xls";
            //}
            //string html = RenderNonBenfPartialViewToString("~/Views/HTMLReport/_GrievanceNonBeneficiaryHtmlReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Grievance_Non_Beneficiary_Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Grievance_Non_Beneficiary_Report" + "( District " + dist + " ) ( Vdc " + vdc + " ) ( Ward " + ward + " ) ( OthHouse " + OthHouse + " ).xls");
        }



        public ActionResult ExportData(DataTable dt)
        {


            using (XLWorkbook wb = new XLWorkbook())
            {
                //wb.Worksheets.Add(dt);
                //wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //wb.Style.Font.Bold = true;

                IXLWorksheet worksheet = wb.AddWorksheet(dt);
                //var range = worksheet.Range("A1", "AB4").Merge();
                //range.Value = string.Format("GOVERNMENT OF NEPAL {0} NATIONAL RECONSTRUCTION AUTHORITY {0} EARTHQUAKE HOUSING RECONSTRUCTION PROGRAME", Environment.NewLine);
                //range.Style.Alignment.WrapText = true;
                //range.Style.Font.Bold = true;
                //range.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);



                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= CaseGrievanceHTMLReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }





        protected string RenderNonBenfPartialViewToString(string viewName)
        {

            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["DetailResults"] = dt;

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

        public ActionResult ExportCGummaryReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
        {
            DataTable dt = new DataTable();
            dt = GetSummaryReportDatable(dist, vdc, ward, LegalOWner, GrievanceType, OtherHouse, Type, CaseTypeFlag, DocType, LegalOwnerFlag, OtherHouseFlag, DocTypeFlag);
            return ExportData(dt);
            // GetSummaryReport(dist, vdc, ward, LegalOWner, GrievanceType,OtherHouse,Type,CaseTypeFlag,DocType,LegalOwnerFlag,OtherHouseFlag,DocTypeFlag);
            //HtmlReport rptParams = new HtmlReport();
            //rptParams = (HtmlReport)Session["CaseReportSummaryParams"];
            //ViewData["ExportFont"] = "font-size: 13px";
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "CaseGrievanceSummaryReport" + "( District " + dist + " ) ( vdc" + vdc + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "CaseGrievanceSummaryReport" + "( District " + dist + " ) ( vdc" + vdc + " ).xls";
            //}
            //string html = RenderPartialCGSummaryViewToString("~/Views/HTMLReport/_SummaryReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("CaseGrievanceSummaryReport"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "CaseGrievanceSummaryReport" +  "( District " + dist + " ) ( vdc" + vdc + " ).xls");
        }
        //Export to excel Start
        public DataTable GetSummaryReportDatable(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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

            return dt;
        }


        //Export to excel End
        protected string RenderPartialCGSummaryViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                ViewData["SummaryResults"] = dt;
                rptParams = (HtmlReport)Session["CaseReportSummaryParams"];
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

        public ActionResult ExportGrievanceDetailReport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
        {
            DataTable dt = new DataTable();
            dt = GetGrievanceByDetailDatatable(dist, vdc, ward, LegalOWner, GrievanceType, OtherHouse, Type, CaseTypeFlag, DocType, LegalOwnerFlag, OtherHouseFlag, DocTypeFlag);
            return ExportDataSummary(dt);
            //GetGrievanceDetailReport(dist, vdc, ward, LegalOWner, GrievanceType, OtherHouse, Type, CaseTypeFlag, DocType, LegalOwnerFlag, OtherHouseFlag, DocTypeFlag);
            //HtmlReport rptParams = new HtmlReport();
            //rptParams = (HtmlReport)Session["CaseReportParams"];
            //ViewData["ExportFont"] = "font-size: 13px";
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "CaseGrievanceDetailReport" + "( District " + dist + " ) ( vdc" + vdc + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "CaseGrievanceDetailReport" + "( District " + dist + " ) ( vdc" + vdc + " ).xls";
            //}
            //string html = RenderPartialCaseDetailViewToString("~/Views/HTMLReport/_CaseDetailReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("CaseGrievanceDetailReport"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Case Grievance Detail Report" + "( District " + dist + " ) ( vdc" + vdc + " ).xls");
        }

        //Export to Excel
        public DataTable GetGrievanceByDetailDatatable(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag)
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
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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

            dt = service.GrievanceDetailFOREXPORT(paramValues);

            return dt;
        }


        public ActionResult ExportDataSummary(DataTable dt)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= CaseGrievanceRegistrationSummary.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportDataSummary");
        }


        //End Export to Excel



        //public ActionResult ExportData(DataTable dt)
        //{

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //        wb.Style.Font.Bold = true;

        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.Charset = "";
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;filename= CaseGrievanceRegistration.xlsx");

        //        using (MemoryStream MyMemoryStream = new MemoryStream())
        //        {
        //            wb.SaveAs(MyMemoryStream, false);
        //            MyMemoryStream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //    return RedirectToAction("Index", "ExportData");
        //}




        protected string RenderPartialCaseDetailViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["DetailResults"] = dt;
                rptParams = (HtmlReport)Session["CaseReportParams"];
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
        public ActionResult RetrofittingBeneficiarySummaryExport(string dist, string vdc, string ward, string Batch, string currentVdc, string currentWard)
        {
            RetrofittingBeneficiarySummary(dist,   Batch, currentVdc, currentWard);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["retrosummaryparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Retrofitting Beneficiary Summary report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Retrofitting Beneficiary Summary report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforretrofittingSummary("~/Views/HTMLReport/_GrievanceNonBeneficiarySummaryHtmlReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Retrobenefsummaryreport.xls");


        }

        protected string RenderPartialViewToStringforretrofittingSummary(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["retrosummaryparams"];
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["DetailResults"] = dt;
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

        public ActionResult RetrofittingBeneficiaryDetailExport(string dist, string vdc, string ward, string OthHouse, string pagesize, string pageindex, string currentVdc, string currentWard, string Batch, string checkexport)
        {
            RetrofittingBeneficiaryDetail(dist, OthHouse, pagesize, pageindex, currentVdc, currentWard,Batch, checkexport);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["retroparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Retrofitting Beneficiary Detail report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Retrofitting Beneficiary Detail report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforretrofitting("~/Views/HTMLReport/_GrievanceNonBeneficiaryHtmlReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Retrobenefreport.xls");


        }

        protected string RenderPartialViewToStringforretrofitting(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["retroparams"];
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["DetailResults"] = dt;
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

        public ActionResult CaseRegistrationDetailExport(string dist, string vdc, string ward, string LegalOWner, string GrievanceType, string OtherHouse, string Type, string CaseTypeFlag, string DocType, string LegalOwnerFlag, string OtherHouseFlag, string DocTypeFlag, string pagesize, string pageindex, string currentVdc, string currentWard,string exportcheck)
        {
            GetGrievanceDetailReport(dist, vdc, ward, LegalOWner, GrievanceType, OtherHouse, Type, CaseTypeFlag, DocType, LegalOwnerFlag, OtherHouseFlag, DocTypeFlag, pagesize, pageindex, currentVdc, currentWard, exportcheck);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["CaseReportParams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Case Registration Detail report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Case Registration Detail report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforcaseregistration("~/Views/HTMLReport/_CaseDetailReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Caseregistrationdetailreport.xls");


        }

        protected string RenderPartialViewToStringforcaseregistration(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["CaseReportParams"];
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];

                ViewData["DetailResults"] = dt;
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
        public ActionResult DailyGrievanceStatusExport(string DateFrom, string DateTo, string pagesize, string pageindex,string exportcheck)
        {
            GetDailyGrievanceStatusDtl(DateFrom, DateTo, pagesize, pageindex, exportcheck);

            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["dailygrievanceparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Daily Grievance Review Status report" + "( Datefrom " + DateFrom + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Daily Grievance Review Status report" + "( Datefrom " + DateFrom + " ).xls";
            }
            string html = RenderPartialViewToStringfordailygrievance("~/Views/HTMLReport/_GrievanceDailyStatusDetailReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "dailygrievancereviewstatusreport.xls");


        }

        protected string RenderPartialViewToStringfordailygrievance(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["dailygrievanceparams"];
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

    }
}
