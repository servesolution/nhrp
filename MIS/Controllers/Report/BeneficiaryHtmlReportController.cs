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
using ClosedXML;
using ClosedXML.Excel;
using System.IO;

namespace MIS.Controllers.Report
{
    public class BeneficiaryHtmlReportController : Controller
    {
        //
        // GET: /BeneficiaryHtmlReport/
        CommonFunction common = new CommonFunction();

        public ActionResult BeneficiaryNonBeneficiaryReport(BeneficiaryHtmlReport objBenfMod)
        {
            int Index = objBenfMod.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["DetailResults"] = dt;
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewBag.ReportTitle = "Reconstruction Beneficiary Detail Report";
            return View(objBenfMod);
        }

        public ActionResult TotalBeneficiaryDetail(BeneficiaryHtmlReport objBenfMod)
        {
            int Index = objBenfMod.Index;
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["DetailResults"] = dt;
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewBag.ReportTitle = "Total Beneficiary Detail Report";
            return View(objBenfMod);
        }

        public ActionResult TotalBeneficiarySummary(BeneficiaryHtmlReport objBenfMod)
        {
            int Index = objBenfMod.Index;
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["DetailResults"] = dt;
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewBag.ReportTitle = "Total Beneficiary Summary Report";
            return View(objBenfMod);


          
        }

        public ActionResult TotalBeneficiaryDetailReport(string dist, string Batch, string currentVdc, string currentWard )
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
            HtmlReport objReport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty && Batch.ConvertToString() != "undefined" && Batch.ConvertToString() != "0" && Batch.ConvertToString() != "-1")
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            if (Batch.ConvertToString() != string.Empty && Batch.ConvertToString() !="undefined")
                paramValues.Add("Batch", Batch);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            DataTable dt = new DataTable();

            dt = objBenfservice.GetTotalBeneficiaryDetailReport(paramValues);
            Session["SummaryResults"] = dt;
            objReport.District = dist;
            objReport.VDC = currentVdc;
            objReport.Ward = currentWard;
            //objReport.NraDefinedCD = nraDefinedCd;
            objReport.BATCHID = Batch;
            objReport.CurrentVDC = currentVdc;
            objReport.CurrentWard = currentWard;
            
            return RedirectToAction("TotalBeneficiaryDetail", objReport);
        }

        public ActionResult TotalBeneficiarySummaryReport(string dist, string Batch, string currentVdc, string currentWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
            HtmlReport objReport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            //if (Batch.ConvertToString() != string.Empty)
            //    paramValues.Add("Batch", Batch);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            DataTable dt = new DataTable();

            dt = objBenfservice.GetTotalBeneficiarySummaryReport(paramValues);
            Session["SummaryResults"] = dt;
            objReport.District = dist;
            objReport.VDC = currentVdc;
            objReport.Ward = currentWard;
            objReport.BATCHID = Batch;
            objReport.CurrentVDC = currentVdc;
            objReport.CurrentWard = currentWard;
        

            return RedirectToAction("TotalBeneficiarySummary", objReport);
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

        public ActionResult GrievanceBeneficiaryResults(HtmlReport obj)
        {
            int Index = obj.Index;

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewData["ddlIndex"] = GetIndexNumber("", Index);

            ViewBag.ReportTitle = "Grievance Beneficiary Detail Report";
            ViewData["DetailResults"] = dt;
            return View(obj);
        }
        public ActionResult GrievanceBeneficiarySummary(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Grievance Beneficiary Summary Report";
            ViewData["SummaryResults"] = dt;
            return View(obj);
        }
        public ActionResult BeneficiaryNonBeneficiaryReportSummary(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DetailResults"];
            ViewBag.ReportTitle = "Reconstruction Beneficiary Summary Report";
            ViewData["DetailResults"] = dt;
            return View(objBenfMod);
        }

        public ActionResult NonBeneficiaryReportDetail(BeneficiaryHtmlReport objBenfMod)
        {
            int Index = objBenfMod.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewBag.ReportTitle = "Reconstruction Non Beneficiary Detail Report";
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewData["DetailResults"] = dt;
            return View(objBenfMod);
        }

        public ActionResult SummaryReportByWard(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            ViewBag.ReportTitle = "Reconstruction Summary Report By Ward";
            return View(objBenfMod);
        }
        public ActionResult BenefDtlReportByGender(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            ViewBag.ReportTitle = "Reconstruction Beneficiary Report By Gender";
            return View(objBenfMod);
        }
        public ActionResult NonBenefDtlReportByGender(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewData["SummaryResults"] = dt;
            ViewBag.ReportTitle = "Reconstruction Non Beneficiary Report By Gender";
            return View(objBenfMod);
        }
        public ActionResult BenefNonBenefSumReportByGender(BeneficiaryHtmlReport objBenfMod)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            // string Gender = Session["Gender"].ConvertToString();
            ViewBag.ReportTitle = "Reconstruction Summery Report By Gender";
            ViewData["SummaryResults"] = dt;
            return View(objBenfMod);
        }
        //public ActionResult NonBenefSumReportByGender(BeneficiaryHtmlReport objBenfMod)
        //{

        //    DataTable dt = null;
        //    dt = (DataTable)Session["SummaryResults"];
        //    ViewData["SummaryResults"] = dt;
        //    return View(objBenfMod);
        //}
        public ActionResult getBeneficiaryBySummary(string dist, string Type, string BenfChk, string NonBenfChk, string Batch, string currentVdc, string currentWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            //if (vdc.ConvertToString() != string.Empty)
            //    paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            //if (ward.ConvertToString() != string.Empty)
            //    paramValues.Add("ward", ward);

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
            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            DataTable dt = new DataTable();

            objBenfMod.District = dist;
            objBenfMod.VDC = currentVdc;
            objBenfMod.WARD = currentWard;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            objBenfMod.CurrentVDC = currentVdc;
            objBenfMod.CurrentWard = currentWard;
            Session["BenfReportSummaryParams"] = objBenfMod;
            Session["reconstructsummaryparams"] = objBenfMod;
            dt = objBenfservice.BeneficiaryBySummary(paramValues);
            ViewData["DetailResults"] = dt;
            Session["DetailResults"] = dt;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("BeneficiaryNonBeneficiaryReportSummary", objBenfMod);

        }

        public ActionResult GetSummaryReportByWard(string dist, string BenfChk, string NonBenfChk, string currentVdc, string currentWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            //if (vdc.ConvertToString() != string.Empty)
            //    paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);


            DataTable dt = new DataTable();
            dt = objBenfservice.SummaryReportByWard(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            objBenfMod.CurrentVDC = currentVdc;
            objBenfMod.CurrentWard = currentWard;
            Session["BenfSummaryByWardParams"] = objBenfMod;


            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("SummaryReportByWard", objBenfMod);

        }
        public ActionResult GetBeneficiaryByDetail(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch, string pagesize, string pageindex , string currentVdc, string currentWard,string checkexport)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Coordinate.ConvertToString() != string.Empty)
                paramValues.Add("Coordinate", Coordinate);
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);
            if (WardFlag.ConvertToString() != string.Empty)
                paramValues.Add("WardFlag", WardFlag);

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

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            if (checkexport.ConvertToString() != string.Empty)
                paramValues.Add("checkexport", checkexport);

            DataTable dt = new DataTable();
            if (BenfChk == "Y")
            {
                dt = objBenfservice.BeneficiaryByDetail(paramValues);
                int tol = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    objBenfMod.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    int idx = tol % 100;
                    if (idx > 0)
                    {
                        objBenfMod.Index = (tol / 100) + 1;
                    }
                    else
                    {
                        objBenfMod.Index = 1;
                    }


                    objBenfMod.Remaining = tol % 100;
                    if (pageindex != null)
                    {
                        objBenfMod.showing = Convert.ToInt32(pageindex) * 100;
                        if (objBenfMod.showing > tol)
                        {
                            objBenfMod.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objBenfMod.Remaining;


                        }
                    }
                    else
                    {
                        if (objBenfMod.Index == 1)
                        {
                            if (objBenfMod.showing > tol || objBenfMod.Index == 1)
                            {
                                objBenfMod.showing = tol;


                            }

                        }
                        else
                        {
                            objBenfMod.showing = Convert.ToInt32(1) * 100;

                        }
                    }
                }




                Session["Results"] = dt;
                objBenfMod.District = dist;
                objBenfMod.VDC = currentVdc;
                objBenfMod.WARD = currentWard;
                objBenfMod.Beneficiary = BenfChk;
                objBenfMod.NonBeneficiary = NonBenfChk;
                objBenfMod.Coordinate = Coordinate;
                objBenfMod.WardFlag = WardFlag;
                objBenfMod.Batch = Batch;

                objBenfMod.CurrentVDC = currentVdc;
                objBenfMod.CurrentWard = currentWard;

                Session["BenfReportDetailParams"] = objBenfMod;
                return RedirectToAction("BeneficiaryNonBeneficiaryReport", objBenfMod);
            }

            if (NonBenfChk == "Y")
            {
                dt = objBenfservice.NonBeneficiaryByDetail(paramValues);
                int tol = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    objBenfMod.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                    int idx = tol % 100;
                    if (idx > 0)
                    {
                        objBenfMod.Index = (tol / 100) + 1;
                    }
                    else
                    {
                        objBenfMod.Index = 1;
                    }



                    objBenfMod.Remaining = tol % 100;
                    if (pageindex != null)
                    {
                        objBenfMod.showing = Convert.ToInt32(pageindex) * 100;
                        if (objBenfMod.showing > tol)
                        {
                            objBenfMod.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objBenfMod.Remaining;


                        }
                    }
                    else
                        if (objBenfMod.Index == 1)
                        {
                            if (objBenfMod.showing > tol || objBenfMod.Index == 1)
                            {
                                objBenfMod.showing = tol;


                            }

                        }
                        else
                        {
                            objBenfMod.showing = Convert.ToInt32(1) * 100;

                        }
                }
                Session["Results"] = dt;
                objBenfMod.District = dist;
                objBenfMod.VDC = currentVdc;
                objBenfMod.WARD = currentWard;
                objBenfMod.Beneficiary = BenfChk;
                objBenfMod.NonBeneficiary = NonBenfChk;
                objBenfMod.Coordinate = Coordinate;

                objBenfMod.CurrentVDC = currentVdc;
                objBenfMod.CurrentWard = currentWard;

                Session["BenfReportDetailParams"] = objBenfMod;
                return RedirectToAction("NonBeneficiaryReportDetail", objBenfMod);
            }

            return View();
        }

        [HttpPost]
        public ActionResult GetBeneficiaryByDetail3(string dist, string vdc, string ward, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch, string pagesize, string pageindex, string CurrVdc, string CurrWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            //HtmlReport objreport = new HtmlReport();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Coordinate.ConvertToString() != string.Empty)
                paramValues.Add("Coordinate", Coordinate);
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);
            if (WardFlag.ConvertToString() != string.Empty)
                paramValues.Add("WardFlag", WardFlag);
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
            if (CurrVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(CurrVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (CurrWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", CurrWard);

            objBenfMod.District = dist;
            objBenfMod.VDC = vdc;
            objBenfMod.WARD = ward;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            objBenfMod.Coordinate = Coordinate;
            objBenfMod.WardFlag = WardFlag;
            objBenfMod.Batch = Batch;
            objBenfMod.CurrentVDC = CurrVdc;
            objBenfMod.CurrentWard = CurrWard;
            Session["CaseReportSummaryParams"] = objBenfMod;

            DataTable dt = new DataTable();
            dt = objBenfservice.BeneficiaryByDetail(paramValues);
            int tol = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                objBenfMod.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                int idx = tol % 100;
                if (idx > 0)
                {
                    objBenfMod.Index = (tol / 100) + 1;
                }
                else
                {
                    objBenfMod.Index = (tol / 100);
                }



                objBenfMod.Remaining = tol % 100;
                if (pageindex != null && pageindex != "ALL")
                {
                    objBenfMod.showing = Convert.ToInt32(pageindex) * 100;
                    if (objBenfMod.showing > tol)
                    {
                        objBenfMod.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objBenfMod.Remaining;


                    }
                }
                else
                {
                    if (pageindex != "ALL")
                    {
                        objBenfMod.showing = Convert.ToInt32(1) * 100;
                    }
                    else
                    {
                        objBenfMod.showing = objBenfMod.Total;
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
            return PartialView("~/Views/BeneficiaryHtmlReport/_BeneficiaryHTMLReportDetail.cshtml", objBenfMod);
        }


        [HttpPost]
        public ActionResult GetNonBeneficiaryByDetail3(string dist, string vdc, string ward, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch, string pagesize, string pageindex, string CurrVdc, string CurrWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            //HtmlReport objreport = new HtmlReport();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Coordinate.ConvertToString() != string.Empty)
                paramValues.Add("Coordinate", Coordinate);
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);
            if (WardFlag.ConvertToString() != string.Empty)
                paramValues.Add("WardFlag", WardFlag);
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

            if (CurrVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(CurrVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (CurrWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", CurrWard);


            objBenfMod.District = dist;
            objBenfMod.VDC = vdc;
            objBenfMod.WARD = ward;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            objBenfMod.Coordinate = Coordinate;
            objBenfMod.WardFlag = WardFlag;
            objBenfMod.Batch = Batch;
            objBenfMod.CurrentVDC = CurrVdc;
            objBenfMod.CurrentWard = CurrWard;
            Session["CaseReportSummaryParams"] = objBenfMod;

            DataTable dt = new DataTable();
            dt = objBenfservice.NonBeneficiaryByDetail(paramValues);
            int tol = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                objBenfMod.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
                int idx = tol % 100;
                if (idx > 0)
                {
                    objBenfMod.Index = (tol / 100) + 1;
                }
                else
                {
                    objBenfMod.Index = (tol / 100);
                }



                objBenfMod.Remaining = tol % 100;
                if (pageindex != null && pageindex != "ALL")
                {
                    objBenfMod.showing = Convert.ToInt32(pageindex) * 100;
                    if (objBenfMod.showing > tol)
                    {
                        objBenfMod.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objBenfMod.Remaining;


                    }
                }
                else
                {
                    if (pageindex != "ALL")
                    {
                        objBenfMod.showing = Convert.ToInt32(1) * 100;
                    }
                    else
                    {
                        objBenfMod.showing = objBenfMod.Total;
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
            return PartialView("~/Views/BeneficiaryHtmlReport/_NonBeneficiaryHTMLReport.cshtml", objBenfMod);
        }

        //Export to Excel
        public DataTable GetBeneficiaryByDetailDataTable(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Coordinate.ConvertToString() != string.Empty)
                paramValues.Add("Coordinate", Coordinate);
            if (WardFlag.ConvertToString() != string.Empty)
                paramValues.Add("WardFlag", WardFlag);
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
            if (BenfChk == "Y")
            {
                dt = objBenfservice.BeneficiaryByDetail(paramValues);
                //Session["Results"] = dt;
                //objBenfMod.District = dist;
                //objBenfMod.VDC = vdc;
                //objBenfMod.WARD = ward;
                //objBenfMod.Beneficiary = BenfChk;
                //objBenfMod.NonBeneficiary = NonBenfChk;
                //objBenfMod.Coordinate = Coordinate;
                //Session["BenfReportDetailParams"] = objBenfMod;
                // return dt;
            }

            if (NonBenfChk == "Y")
            {
                dt = objBenfservice.NonBeneficiaryByDetail(paramValues);
                //Session["Results"] = dt;
                //objBenfMod.District = dist;
                //objBenfMod.VDC = vdc;
                //objBenfMod.WARD = ward;
                //objBenfMod.Beneficiary = BenfChk;
                //objBenfMod.NonBeneficiary = NonBenfChk;
                //objBenfMod.Coordinate = Coordinate;
                //Session["BenfReportDetailParams"] = objBenfMod;
                // return dt;
            }

            return dt;
        }

        //End Export to Excel

        public ActionResult GetBenefNonBenefDetailByGender(string dist, string vdc, string ward, string BenfChk, string NonBenfChk, string gender, string Coordinate)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (gender.ConvertToString() != string.Empty)
                paramValues.Add("gender", gender);
            if (Coordinate.ConvertToString() != string.Empty)
                paramValues.Add("Coordinate", Coordinate);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            if (BenfChk == "Y")
            {
                dt = objBenfservice.BeneficiaryDetailByGender(paramValues);
                Session["SummaryResults"] = dt;
                objBenfMod.District = dist;
                objBenfMod.VDC = vdc;
                objBenfMod.WARD = ward;
                objBenfMod.Beneficiary = BenfChk;
                objBenfMod.NonBeneficiary = NonBenfChk;
                objBenfMod.Coordinate = Coordinate;
                Session["coordinateforExport"] = Coordinate;
                return RedirectToAction("BenefDtlReportByGender", objBenfMod);
            }

            if (NonBenfChk == "Y")
            {
                dt = objBenfservice.NonBeneficiaryDetailByGender(paramValues);
                Session["SummaryResults"] = dt;
                objBenfMod.District = dist;
                objBenfMod.VDC = vdc;
                objBenfMod.WARD = ward;
                objBenfMod.Beneficiary = BenfChk;
                objBenfMod.NonBeneficiary = NonBenfChk;
                objBenfMod.Coordinate = Coordinate;
                Session["coordinateforExport"] = Coordinate;
                return RedirectToAction("NonBenefDtlReportByGender", objBenfMod);
            }

            return View();
        }
        public ActionResult GetBenefNonBenefSummaryByGender(string dist, string vdc, string ward, string BenfChk, string NonBenfChk, string gender)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (gender.ConvertToString() != string.Empty)
                paramValues.Add("gender", gender);
            //if (Coordinate.ConvertToString() != string.Empty)
            //    paramValues.Add("Coordinate", Coordinate);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objBenfservice.BenefNonBenefSummaryByGender(paramValues);
            Session["SummaryResults"] = dt;
            // Session["Gender"] = gender;
            objBenfMod.District = dist;
            objBenfMod.VDC = vdc;
            objBenfMod.WARD = ward;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            //objBenfMod.Coordinate = Coordinate;
            return RedirectToAction("BenefNonBenefSumReportByGender", objBenfMod);


            //if (NonBenfChk == "Y")
            //{
            //    dt = objBenfservice.NonBeneficiarySummaryByGender(paramValues);
            //    Session["SummaryResults"] = dt;
            //    objBenfMod.District = dist;
            //    objBenfMod.VDC = vdc;
            //    objBenfMod.WARD = ward;
            //    objBenfMod.Beneficiary = BenfChk;
            //    objBenfMod.NonBeneficiary = NonBenfChk;
            //    objBenfMod.Coordinate = Coordinate;
            //    return RedirectToAction("NonBenefSumReportByGender", objBenfMod);
            //}

            // return View();
        }
        public ActionResult GetGrievanceBeneficiarySummaryReport(string dist,  string Batch, string currentVdc, string currentWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
            HtmlReport objReport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
          
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

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            DataTable dt = new DataTable();

            dt = objBenfservice.GetGrievanceBeneficiarySummaryReport(paramValues);
            Session["SummaryResults"] = dt;
            objReport.District = dist;
            objReport.VDC = currentVdc;
            objReport.Ward = currentWard;
            objReport.BATCHID = Batch;
            objReport.CurrentVDC = currentVdc;
            objReport.CurrentWard = currentWard;
            Session["grievancebenefparams"] = objReport;

            return RedirectToAction("GrievanceBeneficiarySummary", objReport);
        }

        public ActionResult GetGrievanceBeneficiaryReport(string dist,  string Batch, string pagesize, string pageindex, string currentVdc, string currentWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
            HtmlReport objReport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
           
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);

            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);


            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            //if (checkexport.ConvertToString() != string.Empty)
            //    paramValues.Add("checkexport", checkexport);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");   
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objBenfservice.GetGrievanceBeneficiaryReport(paramValues);
            //int tol = 0;
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            //    objReport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            //    int idx = tol % 100;
            //    if (idx > 0)
            //    {
            //        objReport.Index = (tol / 100) + 1;
            //    }
            //    else
            //    {
            //        objReport.Index = 1;
            //    }



            //    objReport.Remaining = tol % 100;
            //    if (pageindex != null)
            //    {
            //        objReport.showing = Convert.ToInt32(pageindex) * 100;
            //        if (objReport.showing > tol)
            //        {
            //            objReport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objReport.Remaining;


            //        }
            //    }
            //    else
            //        if (objReport.Index == 1)
            //        {
            //            if (objReport.showing > tol || objReport.Index == 1)
            //            {
            //                objReport.showing = tol;


            //            }

            //        }
            //        else
            //        {
            //            objReport.showing = Convert.ToInt32(1) * 100;

            //        }
            //}



            Session["DetailResults"] = dt;
            objReport.District = dist;
            objReport.VDC = currentVdc;
            objReport.Ward = currentWard;
            objReport.CurrentVDC = currentVdc;
            objReport.CurrentWard = currentWard;
            Session["grievancebenefpararms"] = objReport;
            ViewData["DetailResults"] = dt;

            return RedirectToAction("GrievanceBeneficiaryResults", objReport);
        }


        public ActionResult GetGrievanceBeneficiaryReport2(string dist, string vdc, string ward, string Batch, string pagesize, string pageindex, string currentVdc, string currentWard)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
            HtmlReport objReport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);

            if (pagesize.ConvertToString() != string.Empty)
                paramValues.Add("pagesize", pagesize);

            if (pageindex.ConvertToString() != string.Empty)
                paramValues.Add("pageindex", pageindex);


            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            //if (checkexport.ConvertToString() != string.Empty)
            //    paramValues.Add("checkexport", checkexport);

            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();

            dt = objBenfservice.GetGrievanceBeneficiaryReport(paramValues);
            int tol = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            objReport.Total = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);
            int idx = tol % 100;
            if (idx > 0)
            {
                objReport.Index = (tol / 100) + 1;
            }
            else
            {
                objReport.Index = (tol / 100);
            }



            objReport.Remaining = tol % 100;
            if (pageindex != null && pageindex != "ALL")
            {
                objReport.showing = Convert.ToInt32(pageindex) * 100;
                if (objReport.showing > tol)
                {
                    objReport.showing = ((Convert.ToInt32(pageindex) - 1) * 100) + objReport.Remaining;


                }
            }
            else
            {
                if (pageindex != "ALL")
                {
                    objReport.showing = Convert.ToInt32(1) * 100;
                }
                else
                {
                    objReport.showing = objReport.Total;
                }
            }

            Session["DetailResults"] = dt;
            objReport.District = dist;
            objReport.VDC = vdc;
            objReport.Ward = ward;
            objReport.CurrentVDC = currentVdc;
            objReport.CurrentWard = currentWard;
            Session["grievancebenefpararms"] = objReport;
            ViewData["DetailResults"] = dt;

            return PartialView("~/Views/BeneficiaryHtmlReport/_GrievanceBeneficiaryReport.cshtml", objReport); ;

        }

        [HttpPost]
        public ActionResult ExportBeneficiaryDetailByGender(BeneficiaryHtmlReport objBenfMod)
        {
            objBenfMod.Coordinate = Session["coordinateforExport"].ConvertToString();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "BenefdtlReportByGender" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "BenefdtlReportByGender.xls";
            }
            string html = RenderPartialViewToString("~/Views/BeneficiaryHtmlReport/_BenefDtlHTMLReportByGender.cshtml", objBenfMod);
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Beneficiary Detail Report By Gender "), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "BenefdtlReportByGender.xls");
        }
        protected string RenderPartialViewToString(string viewName, BeneficiaryHtmlReport objBenfMod)
        {

            //objBenfMod.Coordinate = Session["coordinateforExport"].ConvertToString();
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                ViewData["SummaryResults"] = dt;

                // rptParams = (HtmlReport)Session["BenefReportDetailParams"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    //ViewData.Model = rptParams;
                    ViewData.Model = objBenfMod;
                    //ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }
        public ActionResult ExportNonBeneficiaryDetailByGender(BeneficiaryHtmlReport objBenfMod)
        {
            objBenfMod.Coordinate = Session["coordinateforExport"].ConvertToString();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "NonBenefdtlReportByGender" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "NonBenefdtlReportByGender.xls";
            }
            string html = RenderNonBenefPartialViewToString("~/Views/BeneficiaryHtmlReport/_NonBenefDtlHTMLReportByGender.cshtml", objBenfMod);
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Non-Beneficiary Detail Report By Gender "), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "NonBenefdtlReportByGender.xls");
        }
        protected string RenderNonBenefPartialViewToString(string viewName, BeneficiaryHtmlReport objBenfMod)
        {

            //objBenfMod.Coordinate = Session["coordinateforExport"].ConvertToString();
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                ViewData["SummaryResults"] = dt;

                // rptParams = (HtmlReport)Session["BenefReportDetailParams"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    //ViewData.Model = rptParams;
                    ViewData.Model = objBenfMod;
                    //ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }


        public DataTable ExportBeneficiaryBySummary(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
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

            return dt;

        }

        public ActionResult ExportBeneficiarySummaryReport(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk)
        {
            DataTable dt = new DataTable();
            dt = ExportBeneficiaryBySummary(dist, vdc, ward, Type, BenfChk, NonBenfChk);
            return ExportDataSummary(dt);
            //ViewData["ExportFont"] = "font-size: 13px";
            //BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            //rptParams = (BeneficiaryHtmlReport)Session["BenfReportSummaryParams"];
            ////BatchID = rptParams.BATCHID;
            ////dist = rptParams.District;
            ////vdc = rptParams.VDC;
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Summary Report " + "( District " + dist + " ) ( Type" + Type + " ) ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Summary Report " + "( District " + dist + " ) ( Type" + Type + " ) ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ).xls";
            //}
            //string html = RenderSummaryPartialViewsToString("~/Views/BeneficiaryHtmlReport/_BeneficiaryHtmlSummaryReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Reconstruction Beneficiary Summary Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Reconstruction Beneficiary Summary Report" + "( District " + dist + " ) ( Type" + Type + " ) ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ).xls");
        }
        protected string RenderSummaryPartialViewsToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            if ((DataTable)Session["DetailResults"] != null)
            {
                DataTable dt = (DataTable)Session["DetailResults"];
                ViewData["DetailResults"] = dt;

                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");
                rptParams = (BeneficiaryHtmlReport)Session["BenfReportSummaryParams"];
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

        public DataTable ExportSummaryByWard(string dist, string vdc, string BenfChk, string NonBenfChk)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            dt = objBenfservice.SummaryReportByWard(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            objBenfMod.District = dist;
            objBenfMod.VDC = vdc;
            objBenfMod.Beneficiary = BenfChk;
            objBenfMod.NonBeneficiary = NonBenfChk;
            Session["BenfSummaryByWardParams"] = objBenfMod;


            //objBenfMod.ReportType = Rtype;

            return dt;

        }
        public ActionResult ExportSummaryReportByWard(string dist, string vdc, string BenfChk, string NonBenfChk)
        {
            DataTable dt = new DataTable();
            dt = ExportSummaryByWard(dist, vdc, BenfChk, NonBenfChk);
            return ExportDataByWard(dt);
            //ViewData["ExportFont"] = "font-size: 13px";
            //BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            //rptParams = (BeneficiaryHtmlReport)Session["BenfSummaryByWardParams"];
            ////BatchID = rptParams.BATCHID;
            ////dist = rptParams.District;
            ////vdc = rptParams.VDC;
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Summary Report By Ward " + "( District " + dist + " )  ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Summary Report By Ward " + "( District " + dist + " )  ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ).xls";
            //}
            //string html = RenderSummaryByWardPartialViewsToString("~/Views/BeneficiaryHtmlReport/_SummaryReportByWard.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Reconstruction Beneficiary Summary Report By Ward "), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Reconstruction Beneficiary Summary Report By Ward" + "( District " + dist + " )  ( BenfChk" + BenfChk + " ) ( NonBenfChk" + NonBenfChk + " ).xls");
        }
        protected string RenderSummaryByWardPartialViewsToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
                ViewData["SummaryResults"] = dt;

                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");
                rptParams = (BeneficiaryHtmlReport)Session["BenfSummaryByWardParams"];
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
        public ActionResult ExportBeneficiaryByDetail(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch)
        {
            DataTable dt = new DataTable();
            //dt = GetBeneficiaryByDetailDataTable(dist, vdc, ward, Type, BenfChk, NonBenfChk, Coordinate, WardFlag,Batch);
            //return ExportDataDetail(dt);
            GetBeneficiaryByDetailForExport(dist, vdc, ward, Type, BenfChk, NonBenfChk, Coordinate, WardFlag, Batch);
            ViewData["ExportFont"] = "font-size: 13px";
            BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            rptParams = (BeneficiaryHtmlReport)Session["BenfReportDetailParams"];
            //BatchID = rptParams.BATCHID;
            //dist = rptParams.District;
            //vdc = rptParams.VDC;
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Detail Report " + "( District " + dist + " ) .xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Detail Report " + "( District " + dist + " ) .xls";
            }
            string html = RenderDetailPartialViewsToString("~/Views/BeneficiaryHtmlReport/_BeneficiaryHTMLReportDetail.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Reconstruction Beneficiary Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Reconstruction Beneficiary Detail Report" + "( District " + dist + " ).xls");
        }



        public ActionResult GetBeneficiaryByDetailForExport(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch)
        {

            BeneficiaryHtmlReportService objBenfservice = new BeneficiaryHtmlReportService();
            NameValueCollection paramValues = new NameValueCollection();
            BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            //if (Rtype.ConvertToString() != string.Empty)
            //    paramValues.Add("Rtype", Rtype);
            if (BenfChk.ConvertToString() != string.Empty)
                paramValues.Add("BenfChk", BenfChk);
            if (NonBenfChk.ConvertToString() != string.Empty)
                paramValues.Add("NonBenfChk", NonBenfChk);
            if (Coordinate.ConvertToString() != string.Empty)
                paramValues.Add("Coordinate", Coordinate);
            if (Batch.ConvertToString() != string.Empty)
                paramValues.Add("Batch", Batch);
            if (WardFlag.ConvertToString() != string.Empty)
                paramValues.Add("WardFlag", WardFlag);



            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }



            DataTable dt = new DataTable();
            if (BenfChk == "Y")
            {
                dt = objBenfservice.BeneficiaryByDetailForExport(paramValues);




                Session["Results"] = dt;
                objBenfMod.District = dist;
                objBenfMod.VDC = vdc;
                objBenfMod.WARD = ward;
                objBenfMod.Beneficiary = BenfChk;
                objBenfMod.NonBeneficiary = NonBenfChk;
                objBenfMod.Coordinate = Coordinate;
                objBenfMod.WardFlag = WardFlag;
                objBenfMod.Batch = Batch;
                Session["BenfReportDetailParams"] = objBenfMod;
                return RedirectToAction("BeneficiaryNonBeneficiaryReport", objBenfMod);
            }

            if (NonBenfChk == "Y")
            {
                dt = objBenfservice.NonBeneficiaryByDetailForExport(paramValues);



                Session["Results"] = dt;
                objBenfMod.District = dist;
                objBenfMod.VDC = vdc;
                objBenfMod.WARD = ward;
                objBenfMod.Beneficiary = BenfChk;
                objBenfMod.NonBeneficiary = NonBenfChk;
                objBenfMod.Coordinate = Coordinate;
                Session["BenfReportDetailParams"] = objBenfMod;
                return RedirectToAction("NonBeneficiaryReportDetail", objBenfMod);
            }

            return View();
        }

        //Export to Excel
        public ActionResult ExportDataSummary(DataTable dt)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                ws.Cell("D1").Value = "Beneficiary Count";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= ReconstructionSummeryReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

                return RedirectToAction("Index", "ExportDataSummary");
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
        //End Export

        public ActionResult ExportDataByWard(DataTable dt)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {

                var ws = wb.Worksheets.Add("House");

                //Title Row
                var wsReportNameHeaderRange = ws.Range("B2", "L4").Merge();
                wsReportNameHeaderRange.Value = string.Format("Government of Nepal {0} National Reconstruction Authority {0} SinghDurbar,Kathmandu", Environment.NewLine);
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                //Form name
                wsReportNameHeaderRange = ws.Range("D5", "L5").Merge();
                wsReportNameHeaderRange.Value = "Reconstruction Beneficiary, Non Beneficiary Report";
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                //var dataTable = dt();
                var rangeWithData = ws.Cell(9, 1).InsertData(dt.AsEnumerable());

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                ws.Cell("B7").Value = "DISTRICT";
                ws.Cell("D7").Value = "VDC";
                ws.Cell("E7").Value = "WARD NO 1";
                ws.Cell("F7").Value = "WARD NO 2";
                ws.Cell("G7").Value = "WARD NO 3";
                ws.Cell("H7").Value = "WARD NO 4";
                ws.Cell("I7").Value = "WARD NO 5";
                ws.Cell("J7").Value = "WARD NO 6";
                ws.Cell("K7").Value = "WARD NO 7";
                ws.Cell("L7").Value = "WARD NO 8";
                ws.Cell("M7").Value = "WARD NO 9";

                ws.Columns("A,C").Delete();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= ReconstructionReportByWard.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportDataByWard");
        }

        public ActionResult ExportDataDetail(DataTable dt)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";


                ws.Cell("R1").Value = "PA Number";
                ws.Cell("C1").Value = "VDC";
                ws.Cell("D1").Value = "WARD";
                ws.Cell("E1").Value = "AREA/TOLE";
                ws.Cell("H1").Value = "HOUSEHOLD SNO";
                ws.Cell("L1").Value = "SLIP NO";
                ws.Cell("S1").Value = "HOUSEOWNER NAME";
                ws.Cell("M1").Value = "IDENTIFACATION TYPE";
                ws.Cell("N1").Value = "IDENTIFICATION NO";
                ws.Cell("F1").Value = "MOBILE NO";


                ws.Columns("B,G,I,J,K,O,P,Q,T,U,V,W").Delete();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= ReconstructionDetailReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);

                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                return RedirectToAction("Index", "ExportDataDetail");
            }
        }

        protected string RenderDetailPartialViewsToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            if ((DataTable)Session["Results"] != null)
            {
                DataTable dt = (DataTable)Session["Results"];
                ViewData["DetailResults"] = dt;

                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");
                rptParams = (BeneficiaryHtmlReport)Session["BenfReportDetailParams"];
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

        public ActionResult GrievanceBeneficiarySummaryExport(string dist, string vdc, string ward, string Batch, string currentVdc, string currentWard)
        {
            GetGrievanceBeneficiarySummaryReport(dist,   Batch, currentVdc, currentWard);
            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["grievancebenefparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance Beneficiary Summary report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance Beneficiary Summary report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforgrievanceSummary("~/Views/BeneficiaryHtmlReport/_GrievancBeneficiarySummaryReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Grievancebenefsummary.xls");


        }

        protected string RenderPartialViewToStringforgrievanceSummary(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["grievancebenefparams"];
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];

                ViewData["SummaryResults"] = dt;
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

        public ActionResult GetGrievanceBeneficiaryReportExport(string dist, string vdc, string ward, string Batch, string pagesize, string pageindex, string currentVdc, string currentWard)
        {
            GetGrievanceBeneficiaryReport(dist,   Batch, pagesize, pageindex, currentVdc, currentWard);
            DataTable dt = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["grievancebenefpararms"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance Beneficiary Detail report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance Beneficiary Detail report" + "( District " + dist + " ).xls";
            }
            string html = RenderPartialViewToStringforgrievancedetail("~/Views/BeneficiaryHtmlReport/_GrievanceBeneficiaryReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Grievancebenefdetail.xls");


        }

        protected string RenderPartialViewToStringforgrievancedetail(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["grievancebenefpararms"];
            if ((DataTable)Session["DetailResults"] != null)
            {
                DataTable dt = (DataTable)Session["DetailResults"];

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

        public ActionResult ReconstructionBeneficiarySummaryExport(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Batch, string currentVdc, string currentWard)
        {
            getBeneficiaryBySummary(dist,Type, BenfChk, NonBenfChk, Batch,currentVdc, currentWard);
            DataTable dt = new DataTable();
            BeneficiaryHtmlReport rptParametess = new BeneficiaryHtmlReport();
            rptParametess = (BeneficiaryHtmlReport)Session["reconstructsummaryparams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Summary report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Summary report" + "( District " + dist + " ).xls";
            }
            string html;
            if(NonBenfChk=="Y")
            { 
                html = RenderPartialViewToStringforReconstructionSummary("~/Views/BeneficiaryHtmlReport/_BeneficiaryHTMLSummaryReport.cshtml");
            }
            else{
                html = RenderPartialViewToStringforReconstructionSummary("~/Views/BeneficiaryHtmlReport/_BeneficiaryHTMLSummaryReport.cshtml");
            }
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Reconstuctbenef.xls");


        }

        protected string RenderPartialViewToStringforReconstructionSummary(string viewName)
        {
            DataTable dtbl = new DataTable();
            BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            rptParams = (BeneficiaryHtmlReport)Session["BenfReportSummaryParams"];
            if ((DataTable)Session["DetailResults"] != null)
            {
                DataTable dt = (DataTable)Session["DetailResults"];

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
        public ActionResult ReconstructionBeneficiaryDetailReport(string dist, string vdc, string ward, string Type, string BenfChk, string NonBenfChk, string Coordinate, string WardFlag, string Batch, string pagesize, string pageindex , string currentVdc, string currentWard,string checkexport)         
        {
            GetBeneficiaryByDetail( dist,  vdc,  ward,  Type,  BenfChk,  NonBenfChk,  Coordinate,  WardFlag,  Batch,  pagesize,  pageindex ,  currentVdc,  currentWard, checkexport) ;         
            DataTable dt = new DataTable();
            BeneficiaryHtmlReport rptParametess = new BeneficiaryHtmlReport();
            rptParametess = (BeneficiaryHtmlReport)Session["BenfReportDetailParams"];
            ViewData["ExportFont"] = "font-size: 13px";


            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Detail report" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Reconstruction Beneficiary Detail report" + "( District " + dist + " ).xls";
            }
            string html;
            if (NonBenfChk == "Y")
            {
                html = RenderPartialViewToStringforReconstructionDetail("~/Views/BeneficiaryHtmlReport/_NonBeneficiaryHTMLReport.cshtml");

            }
            else
            {
                 html = RenderPartialViewToStringforReconstructionDetail("~/Views/BeneficiaryHtmlReport/_BeneficiaryHtmlReportDetail.cshtml");

            }
          
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Reconstuctbenefdetail.xls");


        }

        protected string RenderPartialViewToStringforReconstructionDetail(string viewName)
        {
            DataTable dtbl = new DataTable();
            BeneficiaryHtmlReport rptParams = new BeneficiaryHtmlReport();
            rptParams = (BeneficiaryHtmlReport)Session["BenfReportDetailParams"];
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


    }
}
