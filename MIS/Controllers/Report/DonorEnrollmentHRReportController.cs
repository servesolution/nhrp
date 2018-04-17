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
    public class DonorEnrollmentHRReportController : BaseController
    {
        //
        // GET: /DonorEnrollmentHRReport/

        CommonFunction common = new CommonFunction();
        public ActionResult EnrollmentHRReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Enrollment Report";
            ViewData["SummaryResults"] = dt;
            objreport = (HtmlReport)Session["ERPSummary"];
            return View(objreport);
        }
        public ActionResult GetEnrollmentHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo, FormCollection fc)
        {

            EnrollmentHRReportService objService = new EnrollmentHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();





            //string[] vdcName = fc["vdc_name"].ConvertToString().Split(',');
            //string[] district_Name = fc["district_name"].ConvertToString().Split(',');
            //string[] wardFc = fc["ward"].ConvertToString().Split(',');
            //string wardSelected = "";
            //string vdcSelected = "";
            //string districtSelected = "";

            //if (wardFc[0].ConvertToString() != null && wardFc[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < wardFc.Length; i++)
            //    {
            //        if (wardSelected == "")
            //        {
            //            wardSelected = "'" + wardFc[i].ConvertToString() + "'";

            //        }
            //        else
            //        {
            //            wardSelected = wardSelected + "," + "'" + wardFc[i].ConvertToString() + "'";

            //        }

            //    }
            //}

            //if (vdcName[0].ConvertToString() != null && vdcName[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < vdcName.Length; i++)
            //    {
            //        if (vdcSelected == "")
            //        {
            //            vdcSelected = "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }
            //        else
            //        {
            //            vdcSelected = vdcSelected + "," + "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }

            //    }
            //}

            //if (district_Name[0].ConvertToString() != null && district_Name[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < district_Name.Length; i++)
            //    {
            //        if (districtSelected == "")
            //        {
            //            districtSelected = "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }
            //        else
            //        {
            //            districtSelected = districtSelected + "," + "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }

            //    }
            //}










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
            dt = objService.GetEnrollmentHRReport(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("EnrollmentHRReport", objreport);
        }





        public ActionResult GrievanceTypeHRReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Grievance Type Report";
            ViewData["SummaryResults"] = dt;
            return View(objreport);
        }
        public ActionResult GetGrievanceTypeHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo, FormCollection fc)
        {
            string[] vdcName = fc["vdc_name"].ConvertToString().Split(',');
            string[] district_Name = fc["district_name"].ConvertToString().Split(',');
            string[] wardFc = fc["ward"].ConvertToString().Split(',');
            string wardSelected = "";
            string vdcSelected = "";
            string districtSelected = "";

            if (wardFc.Length > 0)
            {
                for (int i = 0; i < wardFc.Length; i++)
                {
                    if (wardSelected == "")
                    {
                        wardSelected = "'" + wardFc[i].ConvertToString() + "'";

                    }
                    else
                    {
                        wardSelected = wardSelected + "," + "'" + wardFc[i].ConvertToString() + "'";

                    }

                }
            }

            if (vdcName[0].ConvertToString() != null && vdcName[0].ConvertToString() != "")
            {
                for (int i = 0; i < vdcName.Length; i++)
                {
                    if (vdcSelected == "")
                    {
                        vdcSelected = "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

                    }
                    else
                    {
                        vdcSelected = vdcSelected + "," + "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

                    }

                }
            }

            if (district_Name[0].ConvertToString() != null && district_Name[0].ConvertToString() != "")
            {
                for (int i = 0; i < district_Name.Length; i++)
                {
                    if (districtSelected == "")
                    {
                        districtSelected = "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

                    }
                    else
                    {
                        districtSelected = districtSelected + "," + "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

                    }

                }
            }


            GrievanceTypeHRReportService objService = new GrievanceTypeHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);


            //if (districtSelected.ConvertToString() != string.Empty)
            //    paramValues.Add("dist", districtSelected);
            //if (vdcSelected.ConvertToString() != string.Empty)
            //    paramValues.Add("vdc", vdcSelected);
            //if (ward.ConvertToString() != string.Empty)
            //    paramValues.Add("ward", wardSelected);

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
            dt = objService.GetGrievanceTypeHRReport(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("GrievanceTypeHRReport", objreport);
        }





        public ActionResult GrievanceProgressHRReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Grievance Progress";
            ViewData["SummaryResults"] = dt;
            objreport = (HtmlReport)Session["ERPSummary"];
            return View(objreport);
        }
        public ActionResult GetGrievanceProgressHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo, FormCollection fc)
        {
            GrievanceProgressHRReportService objService = new GrievanceProgressHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();




            //string[] vdcName = fc["vdc_name"].ConvertToString().Split(',');
            //string[] district_Name = fc["district_name"].ConvertToString().Split(',');
            //string[] wardFc = fc["ward"].ConvertToString().Split(',');
            //string wardSelected = "";
            //string vdcSelected = "";
            //string districtSelected = "";

            //if (wardFc[0].ConvertToString() != null && wardFc[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < wardFc.Length; i++)
            //    {
            //        if (wardSelected == "")
            //        {
            //            wardSelected = "'" + wardFc[i].ConvertToString() + "'";

            //        }
            //        else
            //        {
            //            wardSelected = wardSelected + "," + "'" + wardFc[i].ConvertToString() + "'";

            //        }

            //    }
            //}

            //if (vdcName[0].ConvertToString() != null && vdcName[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < vdcName.Length; i++)
            //    {
            //        if (vdcSelected == "")
            //        {
            //            vdcSelected = "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }
            //        else
            //        {
            //            vdcSelected = vdcSelected + "," + "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }

            //    }
            //}

            //if (district_Name[0].ConvertToString() != null && district_Name[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < district_Name.Length; i++)
            //    {
            //        if (districtSelected == "")
            //        {
            //            districtSelected = "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }
            //        else
            //        {
            //            districtSelected = districtSelected + "," + "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }

            //    }
            //}


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
            //if (district_Name[1].ConvertToString() != null && district_Name[1].ConvertToString() != "")
            //{
            //    objreport.District = "";
            //}
            objreport.District = dist;
            objreport.VDC = vdc;
            objreport.Ward = ward;
            Session["ERPSummary"] = objreport;

            DataTable dt = new DataTable();
            dt = objService.GetGrievanceProgressHRReport(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;


            return RedirectToAction("GrievanceProgressHRReport", objreport);
        }




        public ActionResult PlinthLevelHRReport(HtmlReport objreport)
        {

            DataTable dt = new DataTable();
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Plinth Level Report";

            ViewData["SummaryResults"] = dt;
            DataColumn col = dt.Columns.Add("Total_Comply", typeof(int));
            if (objreport.District == "" || objreport.District==null)
            {
                col.SetOrdinal(12);
            }
            if ((objreport.District != "" && objreport.District != null) && (objreport.VDC == "" || objreport.VDC == null))
            {
                col.SetOrdinal(13);
            }
            if ( (objreport.VDC != "" && objreport.VDC != null))
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
            if (objreport.District == "" || objreport.District == null)
            {
                cols.SetOrdinal(20);
            }
            if ((objreport.District != "" && objreport.District != null) && (objreport.VDC == "" || objreport.VDC == null))
            {
                cols.SetOrdinal(21);
            }
            if ((objreport.VDC != "" && objreport.VDC != null))
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
        public ActionResult GetPlinthLevelHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo, FormCollection fc)
        {

            PlinthLevelHRReportService objService = new PlinthLevelHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();


            //string[] vdcName = fc["vdc_name"].ConvertToString().Split(',');
            //string[] district_Name = fc["district_name"].ConvertToString().Split(',');
            //string[] wardFc = fc["ward"].ConvertToString().Split(',');
            //string wardSelected = ward;
            //string vdcSelected = common.GetCodeFromDataBase(vdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            //string districtSelected = dist;

            //if (ward.ConvertToString() != null && ward.ConvertToString() != "")
            //{
            //    for (int i = 0; i < wardFc.Length; i++)
            //    {
            //        if (wardSelected == "")
            //        {
            //            wardSelected = "'" + wardFc[i].ConvertToString() + "'";

            //        }
            //        else
            //        {
            //            wardSelected = wardSelected + "," + "'" + wardFc[i].ConvertToString() + "'";

            //        }

            //    }
            //}

            //if (vdc.ConvertToString() != null && vdc.ConvertToString() != "")
            //{
            //    for (int i = 0; i < vdcName.Length; i++)
            //    {
            //        if (vdcSelected == "")
            //        {
            //            vdcSelected = "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }
            //        else
            //        {
            //            vdcSelected = vdcSelected + "," + "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }

            //    }
            //}

            //if (dist.ConvertToString() != null && dist.ConvertToString() != "")
            //{
            //    for (int i = 0; i < district_Name.Length; i++)
            //    {
            //        if (districtSelected == "")
            //        {
            //            districtSelected = "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }
            //        else
            //        {
            //            districtSelected = districtSelected + "," + "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }

            //    }
            //}
            if (dist.ConvertToString() != string.Empty)
                paramValues.Add("dist", common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD"));
            if (vdc.ConvertToString() != string.Empty)
                paramValues.Add("vdc", common.GetCodeFromDataBase(vdc, "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD"));
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);

            //if (dist.ConvertToString() != string.Empty)
            //    paramValues.Add("dist", dist);
            //if (vdc.ConvertToString() != string.Empty)
            //    paramValues.Add("vdc", vdc);
            //if (ward.ConvertToString() != string.Empty)
            //    paramValues.Add("ward", ward);
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




        public ActionResult LintelLevelHRReport(HtmlReport objreport)
        {

            DataTable dt = null;
            dt = (DataTable)Session["SummaryResults"];
            ViewBag.ReportTitle = "House Reconstruction Lintel Level Report";
            ViewData["SummaryResults"] = dt;
            objreport = (HtmlReport)Session["ERPSummary"];
            return View(objreport);
        }
        public ActionResult GetLintelLevelHRReport(string dist, string vdc, string ward, string DateFrom, string DateTo, FormCollection fc)
        {

            LintelLevelHRReportService objService = new LintelLevelHRReportService();
            NameValueCollection paramValues = new NameValueCollection();
            HtmlReport objreport = new HtmlReport();
            //BeneficiaryHtmlReport objBenfMod = new BeneficiaryHtmlReport();





            //string[] vdcName = fc["vdc_name"].ConvertToString().Split(',');
            //string[] district_Name = fc["district_name"].ConvertToString().Split(',');
            //string[] wardFc = fc["ward"].ConvertToString().Split(',');
            //string wardSelected = "";
            //string vdcSelected = "";
            //string districtSelected = "";

            //if (wardFc[0].ConvertToString() != null && wardFc[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < wardFc.Length; i++)
            //    {
            //        if (wardSelected == "")
            //        {
            //            wardSelected = "'" + wardFc[i].ConvertToString() + "'";

            //        }
            //        else
            //        {
            //            wardSelected = wardSelected + "," + "'" + wardFc[i].ConvertToString() + "'";

            //        }

            //    }
            //}

            //if (vdcName[0].ConvertToString() != null && vdcName[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < vdcName.Length; i++)
            //    {
            //        if (vdcSelected == "")
            //        {
            //            vdcSelected = "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }
            //        else
            //        {
            //            vdcSelected = vdcSelected + "," + "'" + common.GetCodeFromDataBase(vdcName[i].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") + "'";

            //        }

            //    }
            //}

            //if (district_Name[0].ConvertToString() != null && district_Name[0].ConvertToString() != "")
            //{
            //    for (int i = 0; i < district_Name.Length; i++)
            //    {
            //        if (districtSelected == "")
            //        {
            //            districtSelected = "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }
            //        else
            //        {
            //            districtSelected = districtSelected + "," + "'" + common.GetCodeFromDataBase(district_Name[i].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") + "'";

            //        }

            //    }
            //}



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
            dt = objService.GetLintelLevelHRReport(paramValues);
            ViewData["SummaryResults"] = dt;
            Session["SummaryResults"] = dt;
            //objBenfMod.District = dist;
            //objBenfMod.VDC = vdc;
            //objBenfMod.Beneficiary = BenfChk;
            //objBenfMod.NonBeneficiary = NonBenfChk;

            //objBenfMod.ReportType = Rtype;

            return RedirectToAction("LintelLevelHRReport", objreport);
        }

    }
}
