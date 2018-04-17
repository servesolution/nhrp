using ExceptionHandler;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Highsoft.Web.Mvc.Charts;
using System.Reflection;
using Highsoft.Web.Mvc.Stocks;

namespace MIS.Controllers.Report
{
    public class DataGraphController : BaseController
    {
        //
        // GET: /DataGraph/
        CommonFunction cm = new CommonFunction();
        STEPCSummaryReportService objReportService = new STEPCSummaryReportService();
        public ActionResult GetData()
        {
            ViewData["ddl_District"] = cm.GetDistrictsinEnglish("");
            return View();
        }
        public ActionResult GetMOUDData()
        {
            ViewData["ddl_District"] = cm.GetDistrictsinEnglish("");
            DataTable dt = new DataTable();
            dt = objReportService.GetMOUDDataForGraph("DOLAKHA");
            var categoryList = new List<string>(dt.Rows.Count);

            List<int> Total_household = new List<int>();
            List<int> Total_Beneficiairies = new List<int>();
            List<int> Benef_First_Installment = new List<int>();
            List<int> benef_construction_started = new List<int>();
            List<int> Approved_Second_Installment = new List<int>();
            List<int> not_approved_second_inst = new List<int>();
            List<int> not_able_to_verify_2nd = new List<int>();
            List<int> approved_third_ins = new List<int>();
            List<int> not_approved_third_inst = new List<int>();
            List<int> not_able_to_verify_3rd = new List<int>();
            List<int> construction_completed = new List<int>();
            foreach (DataRow row in dt.Rows)
            {

                Total_household.Add(row["TOTAL_HOUSEHOLD"].ToInt32());
                Benef_First_Installment.Add(row["BENEF_FIRST_INSTALLMENT"].ToInt32());
                Total_Beneficiairies.Add(row["TOTAL_BENEFICIARIES"].ToInt32());
                Approved_Second_Installment.Add(row["APPROVED_SECOND_INS"].ToInt32());
                benef_construction_started.Add(row["BENEF_CONSTRUCTION_STARTED"].ToInt32());
                not_approved_second_inst.Add(row["not_approved_second_inst"].ToInt32());
                not_able_to_verify_2nd.Add(row["not_able_to_verify_2nd"].ToInt32());
                approved_third_ins.Add(row["approved_third_ins"].ToInt32());
                not_approved_third_inst.Add(row["not_approved_third_inst"].ToInt32());
                not_able_to_verify_3rd.Add(row["not_able_to_verify_3rd"].ToInt32());
                construction_completed.Add(row["construction_completed"].ToInt32());

            }
            List<LineSeriesData> household = new List<LineSeriesData>();
            List<LineSeriesData> firstinstallment = new List<LineSeriesData>();
            List<LineSeriesData> totalBenef = new List<LineSeriesData>();
            List<LineSeriesData> approvedSecondInstallment = new List<LineSeriesData>();
            List<LineSeriesData> benefconstructionstarted = new List<LineSeriesData>();
            List<LineSeriesData> notapproved2nd = new List<LineSeriesData>();
            List<LineSeriesData> notabletoverif2nd = new List<LineSeriesData>();
            List<LineSeriesData> approved3rd = new List<LineSeriesData>();
            List<LineSeriesData> notapproved3rd = new List<LineSeriesData>();
            List<LineSeriesData> notabletoverify3rd = new List<LineSeriesData>();
            List<LineSeriesData> constructioncompleted = new List<LineSeriesData>();

            Total_household.ForEach(p => household.Add(new LineSeriesData { Y = p }));
            Total_Beneficiairies.ForEach(p => totalBenef.Add(new LineSeriesData { Y = p }));
            Benef_First_Installment.ForEach(p => firstinstallment.Add(new LineSeriesData { Y = p }));
            Approved_Second_Installment.ForEach(p => approvedSecondInstallment.Add(new LineSeriesData { Y = p }));
            benef_construction_started.ForEach(p => benefconstructionstarted.Add(new LineSeriesData { Y = p }));
            not_approved_second_inst.ForEach(p => notapproved2nd.Add(new LineSeriesData { Y = p }));
            not_able_to_verify_2nd.ForEach(p => notabletoverif2nd.Add(new LineSeriesData { Y = p }));
            approved_third_ins.ForEach(p => approved3rd.Add(new LineSeriesData { Y = p }));
            not_approved_third_inst.ForEach(p => notapproved3rd.Add(new LineSeriesData { Y = p }));
            not_able_to_verify_3rd.ForEach(p => notabletoverify3rd.Add(new LineSeriesData { Y = p }));
            construction_completed.ForEach(p => constructioncompleted.Add(new LineSeriesData { Y = p }));

            ViewData["Total_household"] = household;
            ViewData["Total_Beneficiairies"] = firstinstallment;
            ViewData["Benef_First_Installment"] = totalBenef;
            ViewData["Approved_Second_Installment"] = approvedSecondInstallment;
            ViewData["benefconstructionstarted"] = benefconstructionstarted;
            ViewData["notapproved2nd"] = notapproved2nd;
            ViewData["notabletoverif2nd"] = notabletoverif2nd;
            ViewData["approved3rd"] = approved3rd;
            ViewData["notapproved3rd"] = notapproved3rd;
            ViewData["notabletoverify3rd"] = notabletoverify3rd;
            ViewData["constructioncompleted"] = constructioncompleted;
            return View();
        }
        public ActionResult GetEHRPMOUDData(string District)
        {
            ViewData["ddl_District"] = cm.GetDistrictsinEnglish("");
            DataTable dt = new DataTable();
            dt = objReportService.GetMOUDDataForGraph(District);
            var categoryList = new List<string>(dt.Rows.Count);

            List<int> Total_household = new List<int>();
            List<int> Total_Beneficiairies = new List<int>();
            List<int> Benef_First_Installment = new List<int>();
             List<int> benef_construction_started=new List<int>();
            List<int> Approved_Second_Installment = new List<int>();
            List<int> not_approved_second_inst = new List<int>();
            List<int> not_able_to_verify_2nd = new List<int>();
            List<int> approved_third_ins = new List<int>();
            List<int> not_approved_third_inst = new List<int>();
            List<int> not_able_to_verify_3rd = new List<int>();
            List<int> construction_completed = new List<int>();
            foreach (DataRow row in dt.Rows)
            {

                Total_household.Add(row["TOTAL_HOUSEHOLD"].ToInt32());
                Benef_First_Installment.Add(row["BENEF_FIRST_INSTALLMENT"].ToInt32());
                Total_Beneficiairies.Add(row["TOTAL_BENEFICIARIES"].ToInt32());
                Approved_Second_Installment.Add(row["APPROVED_SECOND_INS"].ToInt32());
                benef_construction_started.Add(row["BENEF_CONSTRUCTION_STARTED"].ToInt32());
                not_approved_second_inst.Add(row["not_approved_second_inst"].ToInt32());
                not_able_to_verify_2nd.Add(row["not_able_to_verify_2nd"].ToInt32());
                approved_third_ins.Add(row["approved_third_ins"].ToInt32());
                not_approved_third_inst.Add(row["not_approved_third_inst"].ToInt32());
                not_able_to_verify_3rd.Add(row["not_able_to_verify_3rd"].ToInt32());
                construction_completed.Add(row["construction_completed"].ToInt32());
             
            }
            List<LineSeriesData> household = new List<LineSeriesData>();
            List<LineSeriesData> firstinstallment = new List<LineSeriesData>();
            List<LineSeriesData> totalBenef = new List<LineSeriesData>();
            List<LineSeriesData> approvedSecondInstallment = new List<LineSeriesData>();
            List<LineSeriesData> benefconstructionstarted = new List<LineSeriesData>();
            List<LineSeriesData> notapproved2nd = new List<LineSeriesData>();
            List<LineSeriesData> notabletoverif2nd = new List<LineSeriesData>();
            List<LineSeriesData> approved3rd = new List<LineSeriesData>();
            List<LineSeriesData> notapproved3rd = new List<LineSeriesData>();
            List<LineSeriesData> notabletoverify3rd = new List<LineSeriesData>();
            List<LineSeriesData> constructioncompleted = new List<LineSeriesData>();

            Total_household.ForEach(p => household.Add(new LineSeriesData { Y = p }));
            Total_Beneficiairies.ForEach(p => totalBenef.Add(new LineSeriesData { Y = p }));
            Benef_First_Installment.ForEach(p => firstinstallment.Add(new LineSeriesData { Y = p }));
            Approved_Second_Installment.ForEach(p => approvedSecondInstallment.Add(new LineSeriesData { Y = p }));
            benef_construction_started.ForEach(p => benefconstructionstarted.Add(new LineSeriesData { Y = p }));
            not_approved_second_inst.ForEach(p => notapproved2nd.Add(new LineSeriesData { Y = p }));
            not_able_to_verify_2nd.ForEach(p => notabletoverif2nd.Add(new LineSeriesData { Y = p }));
            approved_third_ins.ForEach(p => approved3rd.Add(new LineSeriesData { Y = p }));
            not_approved_third_inst.ForEach(p => notapproved3rd.Add(new LineSeriesData { Y = p }));
            not_able_to_verify_3rd.ForEach(p => notabletoverify3rd.Add(new LineSeriesData { Y = p }));
            construction_completed.ForEach(p => constructioncompleted.Add(new LineSeriesData { Y = p }));

            ViewData["Total_household"] = household;
            ViewData["Total_Beneficiairies"] = firstinstallment;
            ViewData["Benef_First_Installment"] = totalBenef;
            ViewData["Approved_Second_Installment"] = approvedSecondInstallment;
            ViewData["benefconstructionstarted"] = benefconstructionstarted;
            ViewData["notapproved2nd"] = notapproved2nd;
            ViewData["notabletoverif2nd"] = notabletoverif2nd;
            ViewData["approved3rd"] = approved3rd;
            ViewData["notapproved3rd"] = notapproved3rd;
            ViewData["notabletoverify3rd"] = notabletoverify3rd;
            ViewData["constructioncompleted"] = constructioncompleted;
            return PartialView("__ReconstructionGraphReport");
        }



        public ActionResult GetDatabyDistrict(string District)
        {

            DataTable dt = new DataTable();
            dt = objReportService.GetMOUDDataForGraph(District);
            return PartialView("_ReconstructionGraphReport");
        }
        public string GetDataGraph(string cType, string rptType, string isHH, string fName, string District)
        {
            DataTable dtbl = null;
            string langFlag = "";
            string searchType = cType;
            string xmlString = "";
      
            try
            {

                ViewBag.ReportTitle = "Graph Report";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objReportService.GetMOUDDataForGraph(District);
                    dtbl.Columns.RemoveAt(0);


                    //xmlString = dtbl.GetLineChartXml(ChartType.Bar2D, District, "Count", "300", "Approved Second Inspection","Monthly Graph");
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {


                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);
                }
            }
            else
            {
                return "false";
            }
            return "true";

        }

    }
}
