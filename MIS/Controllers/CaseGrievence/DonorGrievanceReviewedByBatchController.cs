using EntityFramework;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.CaseGrievence;
using MIS.Models.Report;
using System.IO;
using ClosedXML.Excel;
namespace MIS.Controllers.CaseGrievence
{
    public class DonorGrievanceReviewedByBatchController : Controller
    {
        CommonFunction common = new CommonFunction();
        //
        // GET: /GrievanceReviewedByBatch/
        public ActionResult GrievanceDetailReportByBatch(HtmlReport objReport)
        {
            int Index = objReport.Index;
            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Grievance Handling Detail Report";
            ViewData["SummaryResults"] = dt;
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            return View(objReport);
        }


        public ActionResult GrievanceDetailReportByBatch2(HtmlReport objReport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Grievance Handling Detail Report";
            ViewData["SummaryResults"] = dt;
            //return View(objReport);
            return PartialView("~/Views/HtmlReport/_GrievanceReviewDtlHTMLReport.cshtml");

            //List<GrievanceDetailReport> list = new List<GrievanceDetailReport>();
            //list = (from DataRow row in dt.Rows

            //        select new GrievanceDetailReport
            //        {
            //            CASE_REGISTRATION_ID = row["CASE_REGISTRATION_ID"].ToString(),
            //            GRIEVANT_NAME = row["GRIEVANT_NAME"].ToString(),
            //            HOUSE_OWNER_NAME_ENG = row["HOUSE_OWNER_NAME_ENG"].ToString(),
            //            HOUSE_OWNER_NAME = row["HOUSE_OWNER_NAME"].ToString(),
            //            HOUSE_OWNER_ID = row["HOUSE_OWNER_ID"].ToString(),
            //            first_owner_NAME = row["first_owner_NAME"].ToString(),
            //            REGISTRATION_DIST_CD = row["REGISTRATION_DIST_CD"].ToString(),
            //            DISTRICT = row["DISTRICT"].ToString(),
            //            REGISTRATION_VDC_MUN_CD = row["REGISTRATION_VDC_MUN_CD"].ToString(),
            //            VDC = row["VDC"].ToString(),


            //            REGISTRATION_WARD_NO = row["REGISTRATION_WARD_NO"].ToString(),
            //            SLIP_NO = row["SLIP_NO"].ToString(),
            //            HOUSE_SNO = row["HOUSE_SNO"].ToString(),
            //            CASE_STATUS = row["CASE_STATUS"].ToString(),
            //            TARGET_BATCH_ID = row["TARGET_BATCH_ID"].ToString(),
            //            NRA_DEFINED_CD = row["NRA_DEFINED_CD"].ToString()
            //            ,
            //            RECOMENDATION_FLAG = row["RECOMENDATION_FLAG"].ToString()
            //            ,
            //            CLARIFICATION_FLAG = row["CLARIFICATION_FLAG"].ToString(),
            //            RETRO_PA = row["RETRO_PA"].ToString(),
            //            GRIEVANCE_PA = row["GRIEVANCE_PA"].ToString(),
            //            ORDER_NO = row["ORDER_NO"].ToString(),
            //            latitude = row["latitude"].ToString(),
            //            Longitude = row["Longitude"].ToString(),
            //            Altitude = row["Altitude"].ToString()
                      
            //        }).ToList();
            ////return Json(dt,JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GrievanceSummaryReportByBatch(HtmlReport objReport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "Grievance Handling Summery Report";
            ViewData["SummaryResults"] = dt;
            return View(objReport);
        }
        public ActionResult GetGrievanceReviewedReportByBatchDetail(string dist, string vdc, string ward, string RecommendationType, string batch,string pagesize,string pageindex)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
            if (batch.ConvertToString() != string.Empty)
                paramValues.Add("batch", batch);

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
            objreport.BATCHID = batch;
            objreport.RecommendType = RecommendationType;
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedDetailReportByBatch(paramValues);

               int tol = 0;
               if (dt != null && dt.Rows.Count > 0)
               {
                   tol = Convert.ToInt32(dt.Rows[0][24]);
                   objreport.Total = Convert.ToInt32(dt.Rows[0][24]);
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
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("GrievanceDetailReportByBatch", objreport);
        }



        public List<SelectListItem> GetIndexNumber(string selectedValue,int index)
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
            foreach (SelectListItem item in selLstCertificate)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstCertificate;
        }





        [HttpGet]
        public ActionResult GetGrievanceReviewedReportByBatchDetail2(string dist, string vdc, string ward, string RecommendationType, string batch, string pagesize, string pageindex)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
            if (batch.ConvertToString() != string.Empty)
                paramValues.Add("batch", batch);

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
            objreport.BATCHID = batch;
            objreport.RecommendType = RecommendationType;
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedDetailReportByBatch(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("GrievanceDetailReportByBatch2", objreport);
        }

        [HttpPost]
        public ActionResult GetGrievanceReviewedReportByBatchDetail3(string dist, string vdc, string ward, string RecommendationType, string batch, string pagesize, string pageindex)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
            if (batch.ConvertToString() != string.Empty)
                paramValues.Add("batch", batch);

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
            objreport.BATCHID = batch;
            objreport.RecommendType = RecommendationType;
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedDetailReportByBatch(paramValues);
            int tol = Convert.ToInt32(dt.Rows[0][24]);
            objreport.Total = Convert.ToInt32(dt.Rows[0][24]);
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
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;
             
            //objBenfMod.ReportType = Rtype;

            ViewData["SummaryResults"] = dt;
            //return View(objReport);
            return PartialView("~/Views/HtmlReport/_GrievanceReviewDtlHTMLReport.cshtml", objreport);
        }
        public ActionResult GetGrievanceReviewedReportByBatchSummary(string dist, string vdc, string ward, string RecommendationType, string batch)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty || vdc.ConvertToString()!="null")
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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
            if (objreport.VDC == "null")
            {
                objreport.VDC = DBNull.Value.ConvertToString();
            }
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

        public ActionResult ExportGrievanceDetailReport(string dist, string vdc, string ward, string Recomendation,string BatchID)
        {
            DataTable dt = new DataTable();
            GetGrievanceReviewedReportByBatchDetailExport(dist, vdc, ward, Recomendation, BatchID);
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_GrievanceReviewDtlHTMLReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Handling Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls");
        }


        public ActionResult ExportGrievanceDetailReportfromreport(string dist, string vdc, string ward, string Recomendation, string batch, string pagesize, string pageindex)
        {
            DataTable dt = new DataTable();
            GetGrievanceReviewedReportByBatchDetailfromreport(dist, vdc, ward, Recomendation, batch, pagesize, pageindex);
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + batch + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + batch + " ).xls";
            }
            string html = RenderPartialViewToString("~/Views/HTMLReport/_GrievanceReviewDtlHTMLReport.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Handling Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + batch + " ).xls");
        }

        //Export-to-Excel Start
        public DataTable GetGrievanceReviewedReportByBatchDetailDatable(string dist, string vdc, string ward, string RecommendationType, string batch)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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


            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedDetailReportByBatch(paramValues);



            return dt;
        }


        public ActionResult ExportData(DataTable dt)
        {
          foreach(DataRow dr in dt.Rows)
          {
              if (dr["RECOMENDATION_FLAG"].ConvertToString() == "NB")
              {
                  dr["RECOMENDATION_FLAG"] = "Non Beneficiary";
              }
              if (dr["RECOMENDATION_FLAG"].ConvertToString() == "LG")
              {
                  dr["RECOMENDATION_FLAG"] = "Local Grievances";
              }
              if (dr["RECOMENDATION_FLAG"].ConvertToString() == "RB")
              {
                  dr["RECOMENDATION_FLAG"] = "Retrofitting Beneficiary";
              }
              if (dr["RECOMENDATION_FLAG"].ConvertToString() == "B")
              {
                  dr["RECOMENDATION_FLAG"] = "Beneficiary";
              }
              if (dr["RECOMENDATION_FLAG"].ConvertToString() == "FO")
              {
                  dr["RECOMENDATION_FLAG"] = "Field Observation";
              }
              if (dr["RECOMENDATION_FLAG"].ConvertToString() == "NF")
              {
                  dr["RECOMENDATION_FLAG"] = "Not Found";
              }
              


                 if (dr["CLARIFICATION_FLAG"].ConvertToString() == "HO")
                        {
                     dr["CLARIFICATION_FLAG"]="Habitable House In Other Place";
                          
                        }
                        if (dr["CLARIFICATION_FLAG"].ConvertToString() == "HU")
                        {
                                   dr["CLARIFICATION_FLAG"]="Habitable House In Usual Place";
                       
                        }
                        if (dr["CLARIFICATION_FLAG"].ConvertToString() == "CB")
                        {
                                   dr["CLARIFICATION_FLAG"]="House Grade Changed to Non Beneficiary";
                        }
                        if (dr["CLARIFICATION_FLAG"].ConvertToString() == "PB")
                        {
                                   dr["CLARIFICATION_FLAG"]="Potential Beneficiary";
                        }
                        if (dr["CLARIFICATION_FLAG"].ConvertToString() == "NB")
                        {
                                   dr["CLARIFICATION_FLAG"]="Probable Beneficiary";
                        }
                        if (dr["CLARIFICATION_FLAG"].ConvertToString() == "FO")
                        {
                                   dr["CLARIFICATION_FLAG"]="Matrix & Photo not cleared or mismatch";
                        }
                        if (dr["CLARIFICATION_FLAG"].ConvertToString() == "NF")
                        {
                                   dr["CLARIFICATION_FLAG"]="Household Not Identified";
                        }



          }
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);
              ws.Columns("A,D,E,F,G,I,N,O,P,S,T,U,V,W").Delete();
               ws.Cell("E1").Value = "WARD";
               ws.Cell("F1").Value = "NISSA_NO";
               ws.Cell("I1").Value = "REMARKS";
               ws.Cell("H1").Value = "RECOMMENDATION";


                //ws.Columns("H,I,J").Delete();
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GrievanceHandlingReport.xlsx");

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

        //Export -to-Excel End



        public DataTable GetGrievanceReviewedReportByBatchDetailExport(string dist, string vdc, string ward, string RecommendationType, string batch)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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
            objreport.BATCHID = batch;
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedDetailReportByBatchWithoutpagination(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return dt;
        }


        public DataTable GetGrievanceReviewedReportByBatchDetailfromreport(string dist, string vdc, string ward, string RecommendationType, string batch, string pagesize, string pageindex)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (RecommendationType.ConvertToString() != string.Empty)
                paramValues.Add("RecommendationType", RecommendationType);
            if (batch.ConvertToString() != string.Empty)
                paramValues.Add("batch", batch);
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
            objreport.BATCHID = batch;
            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedDetailReportByBatchWithoutpagination(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return dt;
        }

        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            if ((DataTable)Session["SummaryResults"] != null)
            {
                DataTable dt = (DataTable)Session["SummaryResults"];
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


        public ActionResult ExportGrievanceSummaryReport(string dist, string vdc, string ward, string Recomendation, string BatchID)
        {
            DataTable dt = new DataTable();
            dt = GetGrievanceReviewedReportByBatchSummaryDataTable(dist, vdc, ward, Recomendation, BatchID);
            return ExportDataSummary(dt);
            //Session.Clear();
            // GetGrievanceReviewedReportByBatchDetail(dist, vdc, ward, Recomendation, BatchID);
            //ViewData["ExportFont"] = "font-size: 13px";
            //string usercd = SessionCheck.getSessionUserCode();
            //string filePath = string.Empty;
            //if (usercd != "")
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            //}
            //else
            //{
            //    filePath = Server.MapPath("/files/Excel/") + "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls";
            //}
            //string html = RenderPartialViewToString("~/Views/HTMLReport/_GrievanceReviewDtlHTMLReport.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Case Grievance Handling Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
            //Utils.CreateFile(html, filePath);
            //return File(filePath, "application/msexcel", "Grievance_Reviewed_Detail" + "( District " + dist + " ) ( Batch" + BatchID + " ).xls");
        }

        //Export-to-Excel Start
        public DataTable GetGrievanceReviewedReportByBatchSummaryDataTable(string dist, string vdc, string ward, string RecommendationType, string batch)
        {

            GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
          

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
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

         
           
            DataTable dt = new DataTable();
            dt = objGrievanceservice.ReviewedSummaryReportByBatch(paramValues);
           
            return dt;
        }


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
                ws.Columns("C,D,M").Delete();
                ws.Cell("C1").Value = "WARD";
                ws.Cell("D1").Value = "BENEFICIARY";
                ws.Cell("E1").Value = "NON_BENEFICIARY";
                ws.Cell("F1").Value = "RETROFITTING_BENEFICIARY";
                ws.Cell("I1").Value = "OTHER_GRIEVANCES";
                ws.Cell("J1").Value = "GRIEVANCE REVIEWED";
                Utils.GetLabel("DISTRICT");

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= GrievanceHandlingReportSummary.xlsx");
          
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
    
    }
}
