using EntityFramework;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Data;
using MIS.Services.BusinessCalculation;
using MIS.Models.Report;
using System.IO;
using ClosedXML.Excel;

namespace MIS.Models.Report
{
    public class HouseholdHTMLReportController : Controller
    {
        //
        // GET: /HouseholdHTMLReport/
        CommonFunction common = new CommonFunction();


        public ActionResult HouseholdDetailReport(HtmlReport obj)
        {
            int Index = obj.Index;
            DataTable dt = null;
            dt = (DataTable)Session["Results"];
            ViewData["DetailResults"] = dt;
            ViewData["ddlIndex"] = GetIndexNumber("", Index);
            ViewBag.ReportTitle = @Utils.GetLabel("Household Detail Report");
            return View(obj);
        }

        public List<SelectListItem> GetIndexNumber(string selectedValue, int index)
        {
            int i;
            List<SelectListItem> selLstCertificate = new List<SelectListItem>();
            
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
        public ActionResult GetHouseholdDetail(string dist, string currentVdc, string currentWard)
        {

            NameValueCollection paramValues = new NameValueCollection();
            OwnerSummaryService objService = new OwnerSummaryService();

            HtmlReport objreport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            
            
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
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            dt = objService.GetHouseHoldDetail(paramValues);
             
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            Session["HouseDetailParams"] = objreport;
          
            return RedirectToAction("HouseholdDetailReport", objreport);
           
        }

        [HttpPost]
        public ActionResult GetHouseholdDetail3(string dist, string vdc, string ward, string pagesize, string pageindex,  string currentVdc, string currentWard)
        {
            OwnerSummaryService objService = new OwnerSummaryService();
            //GrievanceReviewHTMLRptByBtchServices objGrievanceservice = new GrievanceReviewHTMLRptByBtchServices();
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

            if (currentVdc.ConvertToString() != string.Empty)
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);

            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            Session["CaseReportSummaryParams"] = objreport;

            DataTable dt = new DataTable();
            dt = objService.GetHouseHoldDetail(paramValues);
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
            ViewData["DetailResults"] = dt;
            Session["SummaryResults"] = dt;
             
            return PartialView("~/Views/HtmlReport/_HouseholdDetailReport.cshtml", objreport);
        }
        public DataTable ExportHouseholdDetail(string dist, string vdc, string Ward)
        {

            NameValueCollection paramValues = new NameValueCollection();
            OwnerSummaryService objService = new OwnerSummaryService();
            HtmlReport objreport = new HtmlReport();
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD"));
            if (Ward.ConvertToString() != string.Empty)
                paramValues.Add("Ward", Ward);
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }
            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = Ward;
            dt = objService.GetHouseHoldDetailForExport(paramValues);
            ViewData["Results"] = dt;
            Session["Results"] = dt;
            //ViewData["DetailResults"] = dt;
            return dt;
            //return PartialView("HouseholdDetailReport", objreport);
        }

        public DataTable GetDataforHouseholdDetailReport(string dist, string currentVdc, string currentWard) //returns datatable to ExportHouseholdDetailReport 
        {
            NameValueCollection paramValues = new NameValueCollection();
            OwnerSummaryService objService = new OwnerSummaryService();

            HtmlReport objreport = new HtmlReport();

            if (dist.ConvertToString() != string.Empty && dist.ConvertToString() != "undefined")
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));


            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                paramValues.Add("lang", "E");
            }
            else
            {
                paramValues.Add("lang", "N");
            }

            if (currentVdc.ConvertToString() != string.Empty && currentVdc.ConvertToString() != "undefined")
                paramValues.Add("vdcCurr", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (currentWard.ConvertToString() != string.Empty)
                paramValues.Add("wardCurr", currentWard);


            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.VDC = currentVdc;
            objreport.Ward = currentWard;

            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;

            dt = objService.GetHouseHoldDetail(paramValues);

            ViewData["Results"] = dt;
            
            return dt;
        }

        public ActionResult ExportHouseholdDetailReport(string dist,string currentVdc, string currentWard)
        {
            DataTable dt = GetDataforHouseholdDetailReport(dist, currentVdc, currentWard);
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Household_Detail_Report.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("HouseholdSurveyReport", "Report");
        }

        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            HtmlReport rptParams = new HtmlReport();
            rptParams = (HtmlReport)Session["HouseDetailParams"];
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
        public ActionResult ExportData(DataTable dt)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt);

                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";

                ws.Cell("G1").Value = "Household Head Name";
                ws.Cell("R1").Value = "Caste";
                ws.Cell("H1").Value = "District";
                ws.Cell("I1").Value = "VDC";
                ws.Cell("J1").Value = "Ward";
                ws.Cell("K1").Value = "Area";
                ws.Cell("M1").Value = "Gender";
                ws.Cell("P1").Value = "Age";
                ws.Cell("W1").Value = "Education";
                ws.Cell("O1").Value = "Marital Status";
                ws.Cell("X1").Value = "Disability";
                ws.Cell("AD1").Value = "Allowance Type";
                ws.Cell("AF1").Value = "Identification Type";
                ws.Cell("E1").Value = "House Serial No";
                ws.Cell("D1").Value = "Slip Number";
                ws.Cell("Z1").Value = "Mobile No";
                ws.Cell("AC1").Value = "Member Count";
                ws.Cell("AK1").Value = "Monthly Income";
                ws.Cell("AL1").Value = "Death Count";
                ws.Cell("AI1").Value = "Shelter Since Quake";



                ws.Columns("A,B,C,D,E,F,L,N,Q,S,T,U,V,Y,AA,AB,AE,AG,AH,AJ,AM").Delete();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= HouseholdDetailReport.xlsx");

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
