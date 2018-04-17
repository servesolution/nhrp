using ClosedXML.Excel;
using EntityFramework;
using MIS.Models.Report;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class GrievanceDamageGradeEvaluationController : Controller
    {
        //
        // GET: /GrievanceDamageGradeEvaluation/
        CommonFunction common = new CommonFunction();
        public ActionResult Index()
        {
            return View();
        }
        //Grievance Damage Grade Evaluation Report
        public ActionResult GetDetailReport(string dist, string HouseOwnerId, string SlipNo, string gid, string currentVdc, string currentWard,string rs_rv_type,string rec)
        {
            NameValueCollection paramValues = new NameValueCollection();
            MIS.Services.Report.GrievanceDamageEvaluationService objService = new MIS.Services.Report.GrievanceDamageEvaluationService();
            HtmlReport objreport = new HtmlReport();

            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            if (HouseOwnerId != string.Empty)
                paramValues.Add("HouseOwner", HouseOwnerId);

            if (SlipNo != string.Empty)
                paramValues.Add("SlipNo", SlipNo);

            if (gid != string.Empty)
                paramValues.Add("Gid", gid);

            if (currentVdc != string.Empty)
                paramValues.Add("currentVdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (currentWard != string.Empty)
                paramValues.Add("currentWard", currentWard);

            if (rs_rv_type != string.Empty)
                paramValues.Add("rs_rv_type", rs_rv_type);

            if (rec != string.Empty)
                paramValues.Add("rec", rec);

            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;
            objreport.HouseOwnerId = HouseOwnerId;
            objreport.SlipNo = SlipNo;
            objreport.gid = gid;
            objreport.RecommendType = rec;

            dt = objService.GetDetailDamageGradeEvaluationReport(paramValues);

            ViewData["DamagegradeEvaluationReport"] = dt;
            Session["DamagegradeEvaluationReport"] = dt;

            return RedirectToAction("DetailDamagegradeEvaluationReportReport", objreport);
        }
        public ActionResult DetailDamagegradeEvaluationReportReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DamagegradeEvaluationReport"];
            ViewData["DamagegradeEvaluationReport"] = dt;
            ViewBag.ReportTitle = "GRIEVANCE-DAMAGE GRADE EVALUATION REPORT";
            return View(obj);
        }

        public DataTable GetDataForExport(string dist, string HouseOwnerId, string SlipNo, string gid, string currentVdc, string currentWard, string rs_rv_type)
        {
            NameValueCollection paramValues = new NameValueCollection();
            MIS.Services.Report.GrievanceDamageEvaluationService objService = new MIS.Services.Report.GrievanceDamageEvaluationService();
           

            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            if (HouseOwnerId != string.Empty)
                paramValues.Add("HouseOwner", HouseOwnerId);

            if (SlipNo != string.Empty)
                paramValues.Add("SlipNo", SlipNo);

            if (gid != string.Empty)
                paramValues.Add("Gid", gid);

            if (currentVdc != string.Empty)
                paramValues.Add("currentVdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (currentWard != string.Empty)
                paramValues.Add("currentWard", currentWard);

            if (rs_rv_type != string.Empty)
                paramValues.Add("rs_rv_type", rs_rv_type);

            DataTable dt = new DataTable();

            dt = objService.GetDetailDamageGradeEvaluationReport(paramValues);

            dt.Columns.Remove("VERIFICATION_ID");
            dt.Columns.Remove("OLD_SLIP_NO");
            dt.Columns.Remove("ENTERED_BY");
            dt.Columns.Remove("ENTERED_DATE");
            dt.Columns.Remove("DISTRICT_CD");
            dt.Columns.Remove("GP_NP_CD");

            dt.Columns["HOUSE_OWNER_ID"].ColumnName = "HO_ID";
            dt.Columns["DESC_ENG"].ColumnName = "District";
            dt.Columns["RURAL_URBAN"].ColumnName = "GP/NP";
            dt.Columns["MAJOR_DAMAGE_DESC"].ColumnName = "MAJOR_DAMAGE";

            return dt;
        }
        public ActionResult ExportDetailReport(string dist, string HouseOwnerId, string SlipNo, string gid, string currentVdc, string currentWard, string rs_rv_type)
        {
            DataTable dt = GetDataForExport(dist, HouseOwnerId, SlipNo, gid, currentVdc, currentWard, rs_rv_type);
            ViewData["ExportFont"] = "font-size: 13px";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Grievance-Damage Grade Evalutaion Report.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("ReportHierarchy", "Report");
        }

        public ActionResult GetSummaryReport(string dist, string currentVdc, string currentWard, string rs_rv_type)
        {
            NameValueCollection paramValues = new NameValueCollection();
            MIS.Services.Report.GrievanceDamageEvaluationService objService = new MIS.Services.Report.GrievanceDamageEvaluationService();
            HtmlReport objreport = new HtmlReport();

            if (dist != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));

            if (currentVdc != string.Empty)
                paramValues.Add("currentVdc", common.GetCodeFromDataBase(currentVdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));

            if (currentWard != string.Empty)
                paramValues.Add("currentWard", currentWard);

            if (rs_rv_type != string.Empty)
                paramValues.Add("rs_rv_type", rs_rv_type);

            DataTable dt = new DataTable();
            objreport.District = dist;
            objreport.CurrentVDC = currentVdc;
            objreport.CurrentWard = currentWard;
           
            dt = objService.GetDetailDamageGradeEvaluationSumReport(paramValues);

            ViewData["DamagegradeEvaluationSumReport"] = dt;
            Session["DamagegradeEvaluationSumReport"] = dt;

            return RedirectToAction("DetailDamagegradeEvaluationSumReport", objreport);
        }

        public ActionResult DetailDamagegradeEvaluationSumReport(HtmlReport obj)
        {

            DataTable dt = null;
            dt = (DataTable)Session["DamagegradeEvaluationSumReport"];
            ViewData["DamagegradeEvaluationSumReport"] = dt;
            ViewBag.ReportTitle = "GRIEVANCE-DAMAGE GRADE EVALUATION SUMMARY REPORT";
            return View(obj);
        }
    
    }
}
