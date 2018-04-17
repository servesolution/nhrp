using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Web.Routing;
using EntityFramework;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ExceptionHandler;
using System.Text;
using System.IO;

using MIS.Services.Core;
using MIS.Models.BusinessCalcualtion;
using MIS.Services.BusinessCalculation;

namespace MIS.Controllers.BusinessCalculation
{
    public class BeneficiaryController : BaseController
    {
        //
        // GET: /Benefiery/
        CommonFunction commonFC = new CommonFunction();
        BeneficiaryService objService = new BeneficiaryService();
        List<SelectListItem> lstBatch = new List<SelectListItem>();
        [HttpGet]
        public ActionResult BeneficiaryList()
        {

            string distCode = "", distDefCode = "";
            BeneficiarySearch objSearch = new BeneficiarySearch();
            //if (CommonVariables.UserCode != "2502")
            //{
            //    distCode = CommonFunction.GetDistrictByOfficeCode(CommonVariables.EmpCode);
            //}
            if (CommonVariables.GroupCD != "34" && CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
            {
                if (CommonVariables.EmpCode != "")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_District"] = commonFC.GetDistrictsByDistrictCode(Districtcode);
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(objSearch.VDCMunCd, Districtcode);
                    objSearch.DistrictCd = Districtcode;
                }
            }
            else
            {
                ViewData["ddl_District"] = commonFC.GetDistricts("");
                ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict("", "");
            }
            distDefCode = GetData.GetDefinedCodeFor(DataType.District, distCode);

            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");

            ViewData["TargetType"] = commonFC.Trageted("");

            ViewData["BatchID"] = commonFC.GetBatchId("", "");
            ViewData["ddl_target_lot"] = commonFC.GetTargetLot("", "");
            ViewData["ddlApprove"] = commonFC.GetApprove("");
            ViewData["Order_By"] = commonFC.GetOrderValue("");

            //Changed for NHRS
            // lstBatch.AddRange(commonFC.GetEducationTargetBatch("", fisc, distCode));
            //ViewData["BatchID"] = lstBatch;

            return View(objSearch);
        }
        [HttpPost]
        public ActionResult BeneficiaryList(FormCollection fc, BeneficiarySearch objSearch)
        {
            objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            BeneficiaryService objBeneficiary = new BeneficiaryService();
            DataTable searchResult = new DataTable();
            DataTable searchReport = new DataTable();
            ViewData["ddl_District"] = commonFC.GetDistricts("");
            //objSearch.
            ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");
            ViewData["ApproveStatus"] = fc["ddlApprove"].ConvertToString();
            if (CommonVariables.GroupCD == "1" || CommonVariables.GroupCD =="33")
            {

                objSearch.Action = true;
            }
            else
            {
                objSearch.Action = false;
            }

            if (objSearch.ddlApprove == "Y")
            {
                objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objSearch.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                ViewData["BatchID"] = commonFC.GetBatchId("", "");
                ViewData["ddlApprove"] = commonFC.GetApprove("");
                //objSearch.BatchID = fc["BatchID"].ConvertToString();
                objSearch.BatchID = fc["target_lot"].ConvertToString();
                objSearch.ddlApprove = fc["ddlApprove"].ConvertToString();
                Session["Param"] = objSearch;
                DataTable dtEnroll = new DataTable();
                searchResult = objBeneficiary.GetBeneficiariesDeatilsByDate(objSearch);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
                //dtEnroll = objBeneficiary.GetBeneficiariesDeatilsEnroll(objSearch);
                //Session["dtEnroll"] = dtEnroll;
            }
            if (objSearch.ddlApprove == "N")
            {
                objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objSearch.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                ViewData["BatchID"] = commonFC.GetBatchId("", "");
                ViewData["ddlApprove"] = commonFC.GetApprove("");
               // objSearch.BatchID = fc["BatchID"].ConvertToString();
                objSearch.BatchID = fc["target_lot"].ConvertToString();
                searchResult = objBeneficiary.GetBeneficiariesDeatils(objSearch);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
            }

            return PartialView("~/Views/Beneficiary/_BeneficiaryView.cshtml", objSearch);
        }

        [HttpGet]
        public ActionResult RetroBeneficiaryList()
        {

            string distCode = "", distDefCode = "";
            BeneficiarySearch objSearch = new BeneficiarySearch();
            //if (CommonVariables.UserCode != "2502")
            //{
            //    distCode = CommonFunction.GetDistrictByOfficeCode(CommonVariables.EmpCode);
            //}
            if (CommonVariables.GroupCD != "34" && CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
            {
                if (CommonVariables.EmpCode != "")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_District"] = commonFC.GetDistrictsByDistrictCode(Districtcode);
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(objSearch.VDCMunCd, Districtcode);
                    objSearch.DistrictCd = Districtcode;
                }
            }
            else
            {
                ViewData["ddl_District"] = commonFC.GetDistricts("");
                ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict("", "");
            }
            distDefCode = GetData.GetDefinedCodeFor(DataType.District, distCode);

            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");

            ViewData["TargetType"] = commonFC.Trageted("");

            ViewData["BatchID"] = commonFC.GetRetrofittingBatchId("", "");
            ViewData["ddlApprove"] = commonFC.GetApprove("");
            ViewData["Order_By"] = commonFC.GetOrderValue("");

            //Changed for NHRS
            // lstBatch.AddRange(commonFC.GetEducationTargetBatch("", fisc, distCode));
            //ViewData["BatchID"] = lstBatch;

            return View(objSearch);
        }
        [HttpPost]
        public ActionResult RetroBeneficiaryList(FormCollection fc, BeneficiarySearch objSearch)
        {
            objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            BeneficiaryService objBeneficiary = new BeneficiaryService();
            DataTable searchResult = new DataTable();
            DataTable searchReport = new DataTable();
            ViewData["ddl_District"] = commonFC.GetDistricts("");
            //objSearch.
            ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");
            ViewData["ApproveStatus"] = fc["ddlApprove"].ConvertToString();
            if (CommonVariables.GroupCD == "1" || CommonVariables.GroupCD == "33")
            {

                objSearch.Action = true;
            }
            else
            {
                objSearch.Action = false;
            }

            if (objSearch.ddlApprove == "Y")
            {
                objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objSearch.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                ViewData["BatchID"] = commonFC.GetBatchId("", "");
                ViewData["ddlApprove"] = commonFC.GetApprove("");
                objSearch.BatchID = fc["BatchID"].ConvertToString();
                objSearch.ddlApprove = fc["ddlApprove"].ConvertToString();
                Session["Param"] = objSearch;
                DataTable dtEnroll = new DataTable();
                searchResult = objBeneficiary.GetRetroBeneficiariesDeatilsByDate(objSearch);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
                //dtEnroll = objBeneficiary.GetBeneficiariesDeatilsEnroll(objSearch);
                //Session["dtEnroll"] = dtEnroll;
            }
            if (objSearch.ddlApprove == "N")
            {
                objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objSearch.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                ViewData["BatchID"] = commonFC.GetBatchId("", "");
                ViewData["ddlApprove"] = commonFC.GetApprove("");
                objSearch.BatchID = fc["BatchID"].ConvertToString();
                searchResult = objBeneficiary.GetRetroBeneficiariesDeatils(objSearch);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
            }

            return PartialView("~/Views/Beneficiary/_RetroBeneficiaryView.cshtml", objSearch);
        }

        public ActionResult BeneficiaryList1()
        {
            ViewData["BeneficiaryList1"] = Session["dtBeneficiary"];
            return View();
        }
        public ActionResult BeneficiaryApproved(string p, FormCollection fc)
        {
            TargetingServices objTargetService = new TargetingServices();
            TargetingSearch objTargetingSearch = new TargetingSearch();
            QueryResult success = new QueryResult();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string Targeting_batch_id = String.Empty;
            string HouseOwnerID = String.Empty;
            string id = string.Empty;

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    Targeting_batch_id = rvd["Targeting_id"].ConvertToString();
                    id = Request.QueryString["houseID"].ConvertToString();
                    HouseOwnerID = id.Split(',')[0];
                    if (HouseOwnerID == "chkall")
                    {
                        success = objTargetService.BeneficiaryApprovedAll(Targeting_batch_id, HouseOwnerID);
                    }
                    else
                    {
                        success = objTargetService.BeneficiaryApproved(Targeting_batch_id, HouseOwnerID);
                    }

                }
            }
           

            if (success.IsSuccess)
            {
                TempData["Message"] = "Approved Successfully.";
            }
            return RedirectToAction("RetroBeneficiaryList");
        }
        public ActionResult BeneficiaryRetroApproved(string p, FormCollection fc)
        {
            TargetingServices objTargetService = new TargetingServices();
            TargetingSearch objTargetingSearch = new TargetingSearch();
            QueryResult success = new QueryResult();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string Targeting_batch_id = String.Empty;
            string HouseOwnerID = String.Empty;
            string id = string.Empty;

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    Targeting_batch_id = rvd["Targeting_id"].ConvertToString();
                    id = Request.QueryString["houseID"].ConvertToString();
                    HouseOwnerID = id.Split(',')[0];
                    if (HouseOwnerID == "chkall")
                    {
                        success = objTargetService.BeneficiaryRetroApprovedAll(Targeting_batch_id, HouseOwnerID);
                    }
                    else
                    {
                        success = objTargetService.BeneficiaryRetroApproved(Targeting_batch_id, HouseOwnerID);
                    }

                }
            }


            if (success.IsSuccess)
            {
                TempData["Message"] = "Approved Successfully.";
            }
            return RedirectToAction("RetroBeneficiaryList");
        }

        #region Export To Excel


        public DataTable ColumnsPreparation(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();
            requiredCols.Add(Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC"));
            requiredCols.Add(Utils.ToggleLanguage("VDC_ENG", "VDC_LOC"));
            requiredCols.Add("WARD_NO");
            requiredCols.Add(Utils.ToggleLanguage("AREA_ENG", "AREA_LOC"));
            requiredCols.Add("HH_SERIAL_NO");
            //  requiredCols.Add("INSTANCE_UNIQUE_SNO");            
            requiredCols.Add("HOUSE_OWNER_NAME_ENG");
            requiredCols.Add("HOUSE_OWNER_NAME_LOC");
            //requiredCols.Add("Beneficiary SN");
            // requiredCols.Add("ID");
            // requiredCols.Add("ID No");
            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable ColumnsEnrollPreparation(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();
            if (searchType == "Ho")
            {
                requiredCols.Add(Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("VDC_ENG", "VDC_LOC"));
                requiredCols.Add("WARD_NO");
                requiredCols.Add(Utils.ToggleLanguage("AREA_ENG", "AREA_LOC"));
                requiredCols.Add("HH_SERIAL_NO");
                requiredCols.Add("HOUSE_OWNER_ID");
                requiredCols.Add("BENEFICIARY_ID");
            }
            else if (searchType == "Ho1")
            {
                requiredCols.Add(Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("VDC_ENG", "VDC_LOC"));
                requiredCols.Add("WARD_NO");
                requiredCols.Add(Utils.ToggleLanguage("AREA_ENG", "AREA_LOC"));
                requiredCols.Add("HH_SERIAL_NO");
                requiredCols.Add("HOUSE_OWNER_ID");
                requiredCols.Add("BENEFICIARY_ID");
            }
            else if (searchType == "Ho2")
            {
                requiredCols.Add(Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("VDC_ENG", "VDC_LOC"));
                requiredCols.Add("WARD_NO");
                requiredCols.Add(Utils.ToggleLanguage("AREA_ENG", "AREA_LOC"));
                requiredCols.Add("HH_SERIAL_NO");
                requiredCols.Add("HOUSE_OWNER_ID");
                requiredCols.Add("BENEFICIARY_ID");
                requiredCols.Add("BENEFICIARY_ID");
                requiredCols.Add("BENEFICIARY_ID");
                requiredCols.Add("BENEFICIARY_ID");
                requiredCols.Add("BENEFICIARY_ID");

            }

            // requiredCols.Add("HOUSE_OWNER_NAME_ENG");
            //requiredCols.Add("HOUSE_OWNER_NAME_LOC");
            //requiredCols.Add("Beneficiary SN");
            // requiredCols.Add("ID");
            // requiredCols.Add("ID No");
            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable SetEnrollOrdinals(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();

            dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].SetOrdinal(0);
            dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].SetOrdinal(1);
            dtModified.Columns["WARD_NO"].SetOrdinal(2);
            dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].SetOrdinal(3);
            dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(4);
            dtModified.Columns["HOUSE_OWNER_ID"].SetOrdinal(5);
            dtModified.Columns["BENEFICIARY_ID"].SetOrdinal(6);
            // dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(5);
            // dtModified.Columns["HOUSE_OWNER_NAME_LOC"].SetOrdinal(6);
            dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].ColumnName = Utils.GetLabel("District");
            dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ColumnName = Utils.GetLabel("VDC");
            dtModified.Columns["WARD_NO"].ColumnName = Utils.GetLabel("Ward No");
            dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].ColumnName = Utils.GetLabel("Tole");
            dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("Survey No");
            dtModified.Columns["HOUSE_OWNER_ID"].ColumnName = Utils.GetLabel("House No");
            dtModified.Columns["BENEFICIARY_ID"].ColumnName = Utils.GetLabel("Beneficiary ID");
            // dtModified.Columns["HOUSE_OWNER_NAME_ENG"].ColumnName = Utils.GetLabel("House Owner Name (ENG)");
            // dtModified.Columns["HOUSE_OWNER_NAME_LOC"].ColumnName = Utils.GetLabel("House Owner Name (NEP)");
            return dtModified;
        }

        #region VDC Reference Excel
        public DataTable ColumnsVDCEnrollPreparation(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();
            if (searchType == "Ho1")
            {
                requiredCols.Add(Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("VDC_ENG", "VDC_LOC"));
                requiredCols.Add("WARD_NO");
                requiredCols.Add(Utils.ToggleLanguage("AREA_ENG", "AREA_LOC"));
                requiredCols.Add("HH_SERIAL_NO");
                requiredCols.Add("HOUSE_OWNER_ID");

            }
            else
            {
                requiredCols.Add(Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("VDC_ENG", "VDC_LOC"));
                requiredCols.Add("WARD_NO");
                requiredCols.Add(Utils.ToggleLanguage("AREA_ENG", "AREA_LOC"));
                requiredCols.Add("HH_SERIAL_NO");
                requiredCols.Add("HOUSE_OWNER_ID");
                requiredCols.Add("BENEFICIARY_ID");
                requiredCols.Add("TARGETED_OTHERHOUSE_CNT");

            }
            // requiredCols.Add("HOUSE_OWNER_NAME_ENG");
            //requiredCols.Add("HOUSE_OWNER_NAME_LOC");
            //requiredCols.Add("Beneficiary SN");
            // requiredCols.Add("ID");
            // requiredCols.Add("ID No");
            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable SetVDCEnrollOrdinals(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            if (searchType == "Ho1")
            {
                dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].SetOrdinal(0);
                dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].SetOrdinal(1);
                dtModified.Columns["WARD_NO"].SetOrdinal(2);
                dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].SetOrdinal(3);
                dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(4);
                dtModified.Columns["HOUSE_OWNER_ID"].SetOrdinal(5);

                // dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(5);
                // dtModified.Columns["HOUSE_OWNER_NAME_LOC"].SetOrdinal(6);
                dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].ColumnName = Utils.GetLabel("District");
                dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ColumnName = Utils.GetLabel("VDC");
                dtModified.Columns["WARD_NO"].ColumnName = Utils.GetLabel("Ward No");
                dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].ColumnName = Utils.GetLabel("Tole");
                dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("Survey No");
                dtModified.Columns["HOUSE_OWNER_ID"].ColumnName = Utils.GetLabel("House No");
            }
            else
            {

                dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].SetOrdinal(0);
                dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].SetOrdinal(1);
                dtModified.Columns["WARD_NO"].SetOrdinal(2);
                dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].SetOrdinal(3);
                dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(4);
                dtModified.Columns["HOUSE_OWNER_ID"].SetOrdinal(5);
                dtModified.Columns["BENEFICIARY_ID"].SetOrdinal(6);
                dtModified.Columns["TARGETED_OTHERHOUSE_CNT"].SetOrdinal(7);

                // dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(5);
                // dtModified.Columns["HOUSE_OWNER_NAME_LOC"].SetOrdinal(6);
                dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].ColumnName = Utils.GetLabel("District");
                dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ColumnName = Utils.GetLabel("VDC");
                dtModified.Columns["WARD_NO"].ColumnName = Utils.GetLabel("Ward No");
                dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].ColumnName = Utils.GetLabel("Tole");
                dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("Survey No");
                dtModified.Columns["HOUSE_OWNER_ID"].ColumnName = Utils.GetLabel("House No");
                dtModified.Columns["BENEFICIARY_ID"].ColumnName = Utils.GetLabel("Beneficiary ID");
                dtModified.Columns["TARGETED_OTHERHOUSE_CNT"].ColumnName = Utils.GetLabel("No of Alt House");
                // dtModified.Columns["HOUSE_OWNER_NAME_ENG"].ColumnName = Utils.GetLabel("House Owner Name (ENG)");
                // dtModified.Columns["HOUSE_OWNER_NAME_LOC"].ColumnName = Utils.GetLabel("House Owner Name (NEP)");
            }
            return dtModified;
        }
        #endregion
        public DataTable SetOrdinals(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();

            dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].SetOrdinal(0);
            dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].SetOrdinal(1);
            dtModified.Columns["WARD_NO"].SetOrdinal(2);
            dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].SetOrdinal(3);
            dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(4);
            //  dtModified.Columns["INSTANCE_UNIQUE_SNO"].SetOrdinal(4);
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(5);
            dtModified.Columns["HOUSE_OWNER_NAME_LOC"].SetOrdinal(6);
            dtModified.Columns[Utils.ToggleLanguage("DISTRICT_ENG", "DISTRICT_LOC")].ColumnName = Utils.GetLabel("District");
            dtModified.Columns[Utils.ToggleLanguage("VDC_ENG", "VDC_LOC")].ColumnName = Utils.GetLabel("VDC");
            dtModified.Columns["WARD_NO"].ColumnName = Utils.GetLabel("Ward No");
            dtModified.Columns[Utils.ToggleLanguage("AREA_ENG", "AREA_LOC")].ColumnName = Utils.GetLabel("Tole");
            dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("Survey No");
            // dtModified.Columns["INSTANCE_UNIQUE_SNO"].ColumnName = Utils.GetLabel("Family No");
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].ColumnName = Utils.GetLabel("House Owner Name (ENG)");
            dtModified.Columns["HOUSE_OWNER_NAME_LOC"].ColumnName = Utils.GetLabel("House Owner Name (NEP)");
            return dtModified;
        }
        public DataTable ColumnsPreparationVDC(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();

            requiredCols.Add("VDC_ENG");
            requiredCols.Add("WARD_NO");
            requiredCols.Add("AREA_ENG");
            requiredCols.Add("HH_SERIAL_NO");
            requiredCols.Add("FULL_NAME_ENG");
            requiredCols.Add("FULL_NAME_LOC");
            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable SetOrdinalsVDC(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();

            dtModified.Columns["VDC_ENG"].SetOrdinal(0);
            dtModified.Columns["WARD_NO"].SetOrdinal(1);
            dtModified.Columns["AREA_ENG"].SetOrdinal(2);
            dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(3);
            dtModified.Columns["FULL_NAME_ENG"].SetOrdinal(4);
            dtModified.Columns["FULL_NAME_LOC"].SetOrdinal(5);
            dtModified.Columns["VDC_ENG"].ColumnName = Utils.GetLabel("VDC");
            dtModified.Columns["WARD_NO"].ColumnName = Utils.GetLabel("Ward No");
            dtModified.Columns["AREA_ENG"].ColumnName = Utils.GetLabel("Area");
            dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("House Serial No");
            dtModified.Columns["FULL_NAME_ENG"].ColumnName = Utils.GetLabel("House Owner Name (ENG)");
            dtModified.Columns["FULL_NAME_LOC"].ColumnName = Utils.GetLabel("House Owner Name (NEP)");
            return dtModified;
        }

        protected void ExcelExport(DataTable dtRecords)
        {
            string XlsPath = Server.MapPath(@"~/Excel/BeneficiaryList.xls");
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string tab = string.Empty;
                //Added S No for export to excel
                Response.Write(tab + Utils.GetLabel("S NO."));
                tab = "\t";
                //
                foreach (DataColumn datacol in dtRecords.Columns)
                {

                    Response.Write(tab + datacol.ColumnName);
                    tab = "\t";

                }
                Response.Write("\n");

                var rowCount = 0;
                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    //Added Sno for export to excel
                    rowCount++;

                    Response.Write(tab + Utils.GetLabel(Convert.ToString(rowCount)));
                    tab = "\t";
                    //
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        Response.Write(tab + Utils.GetLabel(Convert.ToString(dr[j])));
                        tab = "\t";

                    }


                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }
        protected void ExcelEPDF3(DataTable dtRecords)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            StringBuilder strHTML = new StringBuilder();
            string htmlfilepath = "";
            string pdffilepath = "";
            try
            {
                Document document = new Document();
                //  PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("d://sample1.pdf", FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(document, System.Web.HttpContext.Current.Response.OutputStream);
                document.Open();
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                int ColSpanCount = dtRecords.Columns.Count + 4;

                PdfPTable table = new PdfPTable(dtRecords.Columns.Count + 4);
                float[] widths = new float[dtRecords.Columns.Count + 4];
                string VDC = string.Empty;
                for (int i = 0; i < dtRecords.Columns.Count + 4; i++)
                {
                    widths[i] = 4f;
                }
                table.SetWidths(widths);

                table.WidthPercentage = 100;


                //cell.setHorizontalAlignment(Element.ALIGN_CENTER);

                // table.AddCell(GetPDFCell(Utils.GetLabel("Government of Nepal"), colSpan, border, horizontalAlign, padding));
                // cell.a = new Phrase("");
                int countPage = 0;
                foreach (DataRow r in dtRecords.Rows)
                {
                    if (dtRecords.Rows.Count > 0)
                    {

                        if (r[Utils.GetLabel("VDC")].ConvertToString() != VDC)
                        {
                            strHTML.Append(GetHeader(strHTML, dtRecords, r[Utils.GetLabel("VDC")].ConvertToString(), r[Utils.GetLabel("District")].ConvertToString(), font5));
                        }

                        VDC = r[Utils.GetLabel("VDC")].ConvertToString();
                        DataTable dtt = new DataTable();
                        dtt = objservice.GetOwner(r[Utils.GetLabel("House No")].ConvertToString());
                        if (countPage == 0)
                        {
                            strHTML.Append(GetTableHeader(strHTML, dtRecords, VDC, r[Utils.GetLabel("District")].ConvertToString(), font5));
                        }
                        strHTML.Append("	<tr>	");
                        //if(countPage==)
                        for (int j = 0; j < dtRecords.Columns.Count; j++)
                        {

                            strHTML.Append("    <td style='width:141px;height:20px;'> " + r[j].ConvertToString() + " </td>");
                        }
                        strHTML.Append("    <td style='width:141px;height:20px;'> " + dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString() + " </td>");
                        strHTML.Append("    <td style='width:141px;height:20px;'> " + dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString() + " </td>");
                        strHTML.Append("    <td style='width:141px;height:20px;'> " + Utils.ToggleLanguage(dtt.Rows[0]["IdentificationType_ENG"].ConvertToString(), dtt.Rows[0]["IdentificationType_LOC"].ConvertToString()) + " </td>");
                        strHTML.Append("    <td style='width:141px;height:20px;'> " + Utils.GetLabel(dtt.Rows[0]["CIT_NO"].ConvertToString()) + " </td>");

                        strHTML.Append("	</tr>	");
                        if (countPage < 16)
                            countPage++;
                        else
                        {
                            strHTML.Append("	 </tbody></table></div>	");
                            countPage = 0;
                        }

                    }
                }

                //  Random ram =new Random(1000, 9999);
                Random random = new Random();
                int ran = random.Next(1000, 9999);
                string svrfilepath = Server.MapPath("/Files/html");
                svrfilepath += htmlfilepath + "/Beneficiary" + ran.ConvertToString() + ".pdf";
                htmlfilepath = Server.MapPath("/Files/html/Beneficiary.html");

                pdffilepath = svrfilepath;// Server.MapPath("/Files/pdf/Beneficiary.pdf");
                //Landscape
                Utils.CreateFile(strHTML, htmlfilepath);
                PdfGenerator.ConvertToPdf(htmlfilepath, pdffilepath, "Landscape");
                System.Threading.Thread.Sleep(ColSpanCount * 1000);
                System.IO.FileInfo file = new System.IO.FileInfo(pdffilepath);
                if ((file.Exists))
                {
                    Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);

                    Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    System.IO.Path.GetFileName(file.Name)));

                    Response.TransmitFile(pdffilepath);
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }



        }
        protected void ExcelEPDF1_1(DataTable dtRecords, string srchtype)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            String strHTML = string.Empty;
            string htmlfilepath = "";
            string pdffilepath = "";
            try
            {
                Document document = new Document();
                //  PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("d://sample1.pdf", FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(document, System.Web.HttpContext.Current.Response.OutputStream);
                document.Open();
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                int ColSpanCount = dtRecords.Columns.Count + 4;

                PdfPTable table = new PdfPTable(dtRecords.Columns.Count + 4);
                float[] widths = new float[dtRecords.Columns.Count + 4];
                string VDC = string.Empty;
                for (int i = 0; i < dtRecords.Columns.Count + 4; i++)
                {
                    widths[i] = 4f;
                }
                table.SetWidths(widths);

                table.WidthPercentage = 100;
                int countPage = 0;
                int countp = 0;
                bool firstPage = true;
                foreach (DataRow r in dtRecords.Rows)
                {
                    if (dtRecords.Rows.Count > 0)
                    {
                        //if (countp < 10)
                        //{

                        if (r[Utils.GetLabel("VDC")].ConvertToString() != VDC)
                        {
                            strHTML += (GetHeader(strHTML, dtRecords, r[Utils.GetLabel("VDC")].ConvertToString(), r[Utils.GetLabel("District")].ConvertToString(), font5, srchtype));
                            firstPage = true;
                        }

                        VDC = r[Utils.GetLabel("VDC")].ConvertToString();
                        DataTable dtt = new DataTable();
                        dtt = objservice.GetOwner(r[Utils.GetLabel("House No")].ConvertToString());
                        if (countPage == 0 && firstPage == false)
                        {
                            strHTML += " <div style='page-break-before: always'>";
                            strHTML += GetTableHeader("", dtRecords, VDC, r[Utils.GetLabel("District")].ConvertToString(), font5, srchtype);
                        }
                        strHTML += "	<tr>	";
                        //if(countPage==)
                        for (int j = 0; j < dtRecords.Columns.Count; j++)
                        {

                            strHTML += "    <td style='width:141px;height:20px;'> " + r[j].ConvertToString() + " </td>";
                        }
                        strHTML += "    <td style='width:141px;height:20px;'> " + dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString() + " </td>";
                        strHTML += "    <td style='width:141px;height:20px;'> " + dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString() + " </td>";

                        strHTML += "	</tr>	";
                        if (firstPage)
                        {
                            if (countPage < 20)
                            {
                                countPage++;

                            }
                            else
                            {
                                strHTML += "	 </tbody></table>	";
                                countPage = 0;
                                firstPage = false;
                            }

                        }
                        else
                        {
                            if (countPage < 30)
                                countPage++;
                            else
                            {
                                //  strHTML += "	 </tbody></table>	";
                                strHTML += "	 </tbody></table></div>	";
                                countPage = 0;
                            }
                        }

                        //    countp++;
                        //   // countPage++;
                        //}
                    }
                }

                //  Random ram =new Random(1000, 9999);
                Random random = new Random();
                int ran = random.Next(1000, 9999);
                string svrfilepath = Server.MapPath("/Files/html");
                svrfilepath += htmlfilepath + "/Beneficiary" + ran.ConvertToString() + ".pdf";
                htmlfilepath = Server.MapPath("/Files/html/Beneficiary.html");

                pdffilepath = svrfilepath;// Server.MapPath("/Files/pdf/Beneficiary.pdf");
                //Landscape
                Utils.CreateFile(strHTML, htmlfilepath);
                PdfGenerator.ConvertToPdf(htmlfilepath, pdffilepath, "Landscape");
                System.Threading.Thread.Sleep(ColSpanCount * 1000);
                System.IO.FileInfo file = new System.IO.FileInfo(pdffilepath);
                if ((file.Exists))
                {
                    Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);

                    Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    System.IO.Path.GetFileName(file.Name)));

                    Response.TransmitFile(pdffilepath);
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }



        }

        protected void ExcelEPDF1(DataTable dtRecords)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            String strHTML = string.Empty;
            string htmlfilepath = "";
            string pdffilepath = "";
            try
            {
                Document document = new Document();
                //  PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("d://sample1.pdf", FileMode.Create));
                PdfWriter writer = PdfWriter.GetInstance(document, System.Web.HttpContext.Current.Response.OutputStream);
                document.Open();
                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                int ColSpanCount = dtRecords.Columns.Count + 4;

                PdfPTable table = new PdfPTable(dtRecords.Columns.Count + 4);
                float[] widths = new float[dtRecords.Columns.Count + 4];
                string VDC = string.Empty;
                for (int i = 0; i < dtRecords.Columns.Count + 4; i++)
                {
                    widths[i] = 4f;
                }
                table.SetWidths(widths);

                table.WidthPercentage = 100;


                //cell.setHorizontalAlignment(Element.ALIGN_CENTER);

                // table.AddCell(GetPDFCell(Utils.GetLabel("Government of Nepal"), colSpan, border, horizontalAlign, padding));
                // cell.a = new Phrase("");
                int countPage = 0;
                int countp = 0;
                bool firstPage = true;
                foreach (DataRow r in dtRecords.Rows)
                {
                    if (dtRecords.Rows.Count > 0)
                    {
                        //if (countp < 10)
                        //{

                        if (r[Utils.GetLabel("VDC")].ConvertToString() != VDC)
                        {
                            strHTML += (GetHeader(strHTML, dtRecords, r[Utils.GetLabel("VDC")].ConvertToString(), r[Utils.GetLabel("District")].ConvertToString(), font5));
                            firstPage = true;
                        }

                        VDC = r[Utils.GetLabel("VDC")].ConvertToString();
                        DataTable dtt = new DataTable();
                        dtt = objservice.GetOwner(r[Utils.GetLabel("House No")].ConvertToString());
                        if (countPage == 0 && firstPage == false)
                        {
                            strHTML += " <div style='page-break-before: always'>";
                            strHTML += GetTableHeader("", dtRecords, VDC, r[Utils.GetLabel("District")].ConvertToString(), font5);
                        }
                        strHTML += "	<tr>	";
                        //if(countPage==)
                        for (int j = 0; j < dtRecords.Columns.Count; j++)
                        {

                            strHTML += "    <td style='width:141px;height:20px;'> " + r[j].ConvertToString() + " </td>";
                        }
                        strHTML += "    <td style='width:141px;height:20px;'> " + dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString() + " </td>";
                        strHTML += "    <td style='width:141px;height:20px;'> " + dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString() + " </td>";
                        strHTML += "    <td style='width:141px;height:20px;'> " + Utils.ToggleLanguage(dtt.Rows[0]["IdentificationType_ENG"].ConvertToString(), dtt.Rows[0]["IdentificationType_LOC"].ConvertToString()) + " </td>";
                        strHTML += "    <td style='width:141px;height:20px;'> " + Utils.GetLabel(dtt.Rows[0]["CIT_NO"].ConvertToString()) + " </td>";

                        strHTML += "	</tr>	";
                        if (firstPage)
                        {
                            if (countPage < 20)
                            {
                                countPage++;

                            }
                            else
                            {
                                strHTML += "	 </tbody></table>	";
                                countPage = 0;
                                firstPage = false;
                            }

                        }
                        else
                        {
                            if (countPage < 30)
                                countPage++;
                            else
                            {
                                //  strHTML += "	 </tbody></table>	";
                                strHTML += "	 </tbody></table></div>	";
                                countPage = 0;
                            }
                        }

                        //    countp++;
                        //   // countPage++;
                        //}
                    }
                }

                //  Random ram =new Random(1000, 9999);
                Random random = new Random();
                int ran = random.Next(1000, 9999);
                string svrfilepath = Server.MapPath("/Files/html");
                svrfilepath += htmlfilepath + "/Beneficiary" + ran.ConvertToString() + ".pdf";
                htmlfilepath = Server.MapPath("/Files/html/Beneficiary.html");

                pdffilepath = svrfilepath;// Server.MapPath("/Files/pdf/Beneficiary.pdf");
                //Landscape
                Utils.CreateFile(strHTML, htmlfilepath);
                PdfGenerator.ConvertToPdf(htmlfilepath, pdffilepath, "Landscape");
                System.Threading.Thread.Sleep(ColSpanCount * 1000);
                System.IO.FileInfo file = new System.IO.FileInfo(pdffilepath);
                if ((file.Exists))
                {
                    Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);

                    Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    System.IO.Path.GetFileName(file.Name)));

                    Response.TransmitFile(pdffilepath);
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }



        }

        private String GetHeader(String srtReport, DataTable dt, string VDC, string District, iTextSharp.text.Font font5)
        {
            int colSpan = dt.Columns.Count + 4;
            string urllocation = Request.Url.Host;
            srtReport += "   <span > ";
            srtReport += " <img src='" + urllocation + "/MIS/Content/Images/GovLogo.png' style='width:150px;'> ";
            srtReport += " </span>";
            srtReport += " <div style='text-align: center;width:98%>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>नेपाल सरकार</strong></span></span></div>	<div style='text-align: center;width:98%><span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>संघीय मामिला तथा स्थानीय मन्त्रालय</strong></span></span></div>	";
            srtReport += " <div style='text-align: center;width:98%><p style='text-align: center;width:98%;margin-top: 1px; margin-bottom: 1px;'>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>" + VDC + " गाउ विकास समतिको कार्यालय</strong></span></span>&nbsp;</p></div><div style='text-align: center;width:98%> <span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>" + VDC + ", " + District + "</strong></span></span></div>";
            srtReport += " <div style='text-align: center;width:98%>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>आवासीय भवन पुनर्निर्माणका लागि छनौट भएका लाभाग्राहिहरुको सुची</strong></span></span></div> ";

            srtReport += " <table align='left' border='1' cellpadding='0' cellspacing='0' height='137' width='100%'>	<thead>	";
            foreach (DataColumn c in dt.Columns)
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + c.ColumnName + " </th>";

            }
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>";
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>";
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID") + " </th>";
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID No") + " </th>";
            srtReport += "	</thead> <tbody>";
            // srtReport += "	<thead>	";
            return srtReport;
        }

        private String GetHeader(String srtReport, DataTable dt, string VDC, string District, iTextSharp.text.Font font5, string srchType)
        {
            int colSpan = dt.Columns.Count + 4;
            string urllocation = Request.Url.Host;
            srtReport += "   <span > ";
            srtReport += " <img src='" + urllocation + "/MIS/Content/Images/GovLogo.png' style='width:150px;'> ";
            srtReport += " </span>";
            srtReport += " <div style='text-align: center;width:98%>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>नेपाल सरकार</strong></span></span></div>	<div style='text-align: center;width:98%><span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>संघीय मामिला तथा स्थानीय मन्त्रालय</strong></span></span></div>	";
            srtReport += " <div style='text-align: center;width:98%><p style='text-align: center;width:98%;margin-top: 1px; margin-bottom: 1px;'>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>" + VDC + " गाउ विकास समतिको कार्यालय</strong></span></span>&nbsp;</p></div><div style='text-align: center;width:98%> <span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>" + VDC + ", " + District + "</strong></span></span></div>";
            srtReport += " <div style='text-align: center;width:98%>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>आवासीय भवन पुनर्निर्माणका लागि छनौट भएका लाभाग्राहिहरुको सुची</strong></span></span></div> ";

            srtReport += " <table align='left' border='1' cellpadding='0' cellspacing='0' height='137' width='100%'>	<thead>	";
            foreach (DataColumn c in dt.Columns)
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + c.ColumnName + " </th>";

            }
            if (srchType == "Ho1")
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>";
            }
            else
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID No") + " </th>";
            }
            srtReport += "	</thead> <tbody>";
            // srtReport += "	<thead>	";
            return srtReport;
        }
        private String GetTableHeader(String srtReport, DataTable dt, string VDC, string District, iTextSharp.text.Font font5)
        {
            // srtReport += " <div style='page-break-before: always'>";

            srtReport += " <table align='left' border='1' cellpadding='0' cellspacing='0' height='137' width='100%'>	<thead>	";
            foreach (DataColumn c in dt.Columns)
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + c.ColumnName + " </th>";

            }
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>";
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>";
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID") + " </th>";
            srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID No") + " </th>";
            srtReport += "	</thead> <tbody>";
            return srtReport;
        }
        private String GetTableHeader(String srtReport, DataTable dt, string VDC, string District, iTextSharp.text.Font font5, string srchtype)
        {
            // srtReport += " <div style='page-break-before: always'>";

            srtReport += " <table align='left' border='1' cellpadding='0' cellspacing='0' height='137' width='100%'>	<thead>	";
            foreach (DataColumn c in dt.Columns)
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + c.ColumnName + " </th>";

            }
            if (srchtype == "Ho1")
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>";
            }
            else
            {
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID") + " </th>";
                srtReport += "	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID No") + " </th>";
            }
            srtReport += "	</thead> <tbody>";
            return srtReport;
        }
        private StringBuilder GetHeader(StringBuilder srtReport, DataTable dt, string VDC, string District, iTextSharp.text.Font font5)
        {
            int colSpan = dt.Columns.Count + 4;
            string urllocation = Request.Url.Host;
            srtReport.Append("   <span > ");
            srtReport.Append(" <img src='" + urllocation + "/MIS/Content/Images/GovLogo.png' style='width:150px;'> ");
            srtReport.Append(" </span>");
            srtReport.Append(" <div style='text-align: center;width:98%>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>नेपाल सरकार</strong></span></span></div>	<div style='text-align: center;width:98%><span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>संघीय मामिला तथा स्थानीय मन्त्रालय</strong></span></span></div>	");
            srtReport.Append(" <div style='text-align: center;width:98%><p style='text-align: center;width:98%;margin-top: 1px; margin-bottom: 1px;'>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>" + VDC + " गाउ विकास समतिको कार्यालय</strong></span></span>&nbsp;</p></div><div style='text-align: center;width:98%> <span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>" + VDC + ", " + District + "</strong></span></span></div>");
            srtReport.Append(" <div style='text-align: center;width:98%>	<span style='font-size:32px;'><span style='color: rgb(178, 34, 34);'><strong>आवासीय भवन पुनर्निर्माणका लागि छनौट भएका लाभाग्राहिहरुको सुची</strong></span></span></div> ");
            srtReport.Append(" <div style='page-break-before: always'>");

            srtReport.Append(" <table align='left' border='1' cellpadding='0' cellspacing='0' height='137' width='100%'>	<thead>	");
            foreach (DataColumn c in dt.Columns)
            {
                srtReport.Append("	<th style='width:141px;height:20px;'> " + c.ColumnName + " </th>");

            }
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>");
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>");
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID") + " </th>");
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID No") + " </th>");
            srtReport.Append("	</thead> <tbody>");
            // srtReport += "	<thead>	";
            return srtReport;
        }
        private StringBuilder GetTableHeader(StringBuilder srtReport, DataTable dt, string VDC, string District, iTextSharp.text.Font font5)
        {
            srtReport.Append(" <div style='page-break-before: always'>");

            srtReport.Append(" <table align='left' border='1' cellpadding='0' cellspacing='0' height='137' width='100%'>	<thead>	");
            foreach (DataColumn c in dt.Columns)
            {
                srtReport.Append("	<th style='width:141px;height:20px;'> " + c.ColumnName + " </th>");

            }
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (ENG)") + " </th>");
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("House Owner Name (NEP)") + " </th>");
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID") + " </th>");
            srtReport.Append("	<th style='width:141px;height:20px;'> " + Utils.GetLabel("ID No") + " </th>");
            srtReport.Append("	</thead> <tbody>");
            return srtReport;
        }
        protected void ExcelEPDF(DataTable dtRecords)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            Document document = new Document();
            //  PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("d://sample1.pdf", FileMode.Create));
            PdfWriter writer = PdfWriter.GetInstance(document, System.Web.HttpContext.Current.Response.OutputStream);
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


            PdfPTable table = new PdfPTable(dtRecords.Columns.Count + 4);
            float[] widths = new float[dtRecords.Columns.Count + 4];
            string VDC = string.Empty;
            for (int i = 0; i < dtRecords.Columns.Count + 4; i++)
            {
                widths[i] = 4f;
            }
            table.SetWidths(widths);

            table.WidthPercentage = 100;


            //cell.setHorizontalAlignment(Element.ALIGN_CENTER);

            // table.AddCell(GetPDFCell(Utils.GetLabel("Government of Nepal"), colSpan, border, horizontalAlign, padding));
            // cell.a = new Phrase("");

            foreach (DataRow r in dtRecords.Rows)
            {
                if (dtRecords.Rows.Count > 0)
                {
                    if (r[Utils.GetLabel("VDC")].ConvertToString() != VDC)
                    {
                        table = GetHeader(table, dtRecords, r[Utils.GetLabel("VDC")].ConvertToString(), r[Utils.GetLabel("District")].ConvertToString(), font5);
                    }
                    VDC = r[Utils.GetLabel("VDC")].ConvertToString();
                    DataTable dtt = new DataTable();
                    dtt = objservice.GetOwner(r[Utils.GetLabel("House No")].ConvertToString());
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        table.AddCell(new Phrase(r[j].ToString(), font5));
                    }
                    table.AddCell(new Phrase(dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString(), font5));
                    table.AddCell(new Phrase(dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString(), font5));
                    table.AddCell(new Phrase(Utils.ToggleLanguage(dtt.Rows[0]["IdentificationType_ENG"].ConvertToString(), dtt.Rows[0]["IdentificationType_LOC"].ConvertToString()), font5));
                    table.AddCell(new Phrase(Utils.GetLabel(dtt.Rows[0]["CIT_NO"].ConvertToString()), font5));


                }
            }

            document.Add(table);
            document.Close();
            Response.ContentType = "application/pdf";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.AddHeader("content-disposition", "attachment; filename= EnrollmentHouseholdReport.pdf");
            System.Web.HttpContext.Current.Response.Write(document);
            Response.Flush();
            Response.End();

        }


        private PdfPTable GetHeader(PdfPTable table, DataTable dt, string VDC, string District, iTextSharp.text.Font font5)
        {


            int colSpan = dt.Columns.Count + 4;
            int border = Rectangle.NO_BORDER;
            int horizontalAlign = Element.ALIGN_CENTER;
            float padding = 5.0f;

            table.AddCell(GetPDFCell("नेपाल सरकार", colSpan, border, horizontalAlign, padding));
            table.AddCell(GetPDFCell(Utils.GetLabel("संघीय मामिला तथा स्थानीय मन्त्रालय"), colSpan, border, horizontalAlign, padding));
            table.AddCell(GetPDFCell(Utils.GetLabel(VDC + " गाउ विकास समतिको कार्यालय"), colSpan, border, horizontalAlign, padding));
            table.AddCell(GetPDFCell(Utils.GetLabel(VDC + " " + District), colSpan, border, horizontalAlign, padding));
            table.AddCell(GetPDFCell(Utils.GetLabel("आवासीय भवन पुनर्निर्माणका लागि छनौट भएका लाभाग्राहिहरुको सुची"), colSpan, border, horizontalAlign, padding));
            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            table.AddCell(new Phrase(Utils.GetLabel("House Owner Name (ENG)"), font5));
            table.AddCell(new Phrase(Utils.GetLabel("House Owner Name (NEP)"), font5));
            table.AddCell(new Phrase(Utils.GetLabel("ID"), font5));
            table.AddCell(new Phrase(Utils.GetLabel("ID No"), font5));
            return table;
        }

        private PdfPCell GetPDFCell(string strText, int colSpan, int border, int alignment, float padding)
        {
            string sylfaenpath = @"C:\\Windows\\Fonts\\PARCHM.TTF";
            BaseFont sylfaen = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font head = new Font(sylfaen, 12f, Font.NORMAL, BaseColor.BLUE);
            Font normal = new Font(sylfaen, 10f, Font.NORMAL, BaseColor.BLACK);
            Font underline = new Font(sylfaen, 10f, Font.UNDERLINE, BaseColor.BLACK);
            PdfPCell cell1 = new PdfPCell(new Phrase(strText, head));
            cell1.Colspan = colSpan;
            cell1.Border = border;
            cell1.HorizontalAlignment = alignment;
            cell1.Padding = padding;
            return cell1;
        }
        protected void ExcelExportCustomized1(DataTable dtRecords)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            string XlsPath = Server.MapPath(@"~/Excel/BeneficiaryList.xls");
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string tab = string.Empty;
                //Added S No for export to excel
                Response.Write(tab + Utils.GetLabel("SNo"));
                tab = "\t";
                //
                foreach (DataColumn datacol in dtRecords.Columns)
                {

                    Response.Write(tab + datacol.ColumnName);
                    tab = "\t";

                }
                Response.Write(tab + Utils.GetLabel("House Owner Name (ENG)"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("House Owner Name (NEP)"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("ID"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("ID No"));
                tab = "\t";
                Response.Write("\n");

                var rowCount = 0;
                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    //Added Sno for export to excel
                    rowCount++;

                    Response.Write(tab + Utils.GetLabel(Convert.ToString(rowCount)));
                    tab = "\t";
                    //
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        Response.Write(tab + Utils.GetLabel(Convert.ToString(dr[j])));
                        tab = "\t";

                    }
                    DataTable dtt = new DataTable();
                    dtt = objservice.GetOwner(dr[Utils.GetLabel("House No")].ConvertToString());
                    //foreach(DataRow drr in dtt.Rows)
                    //{


                    //}
                    Response.Write(tab + dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString());
                    tab = "\t";
                    Response.Write(tab + dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString());
                    tab = "\t";
                    Response.Write(tab + Utils.ToggleLanguage(dtt.Rows[0]["IdentificationType_ENG"].ConvertToString(), dtt.Rows[0]["IdentificationType_LOC"].ConvertToString()));
                    tab = "\t";
                    Response.Write(tab + Utils.GetLabel(dtt.Rows[0]["CIT_NO"].ConvertToString()));
                    tab = "\t";
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }
        protected void ExcelExportCustomized2(DataTable dtRecords)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            string XlsPath = Server.MapPath(@"~/Excel/BeneficiaryList.xls");
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string tab = string.Empty;
                //Added S No for export to excel
                Response.Write(tab + Utils.GetLabel("SNo"));
                tab = "\t";
                //
                foreach (DataColumn datacol in dtRecords.Columns)
                {

                    Response.Write(tab + datacol.ColumnName);
                    tab = "\t";

                }
                Response.Write(tab + Utils.GetLabel("House Owner Name (ENG)"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("House Owner Name (NEP)"));
                tab = "\t";
                //Response.Write(tab + Utils.GetLabel("ID"));
                //tab = "\t";
                //Response.Write(tab + Utils.GetLabel("ID No"));
                //tab = "\t";
                Response.Write("\n");

                var rowCount = 0;
                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    //Added Sno for export to excel
                    rowCount++;

                    Response.Write(tab + Utils.GetLabel(Convert.ToString(rowCount)));
                    tab = "\t";
                    //
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        Response.Write(tab + Utils.GetLabel(Convert.ToString(dr[j])));
                        tab = "\t";

                    }
                    DataTable dtt = new DataTable();
                    dtt = objservice.GetOwner(dr[Utils.GetLabel("House No")].ConvertToString());
                    //foreach(DataRow drr in dtt.Rows)
                    //{


                    //}
                    Response.Write(tab + dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString());
                    tab = "\t";
                    Response.Write(tab + dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString());
                    tab = "\t";
                    //Response.Write(tab + Utils.ToggleLanguage(dtt.Rows[0]["IdentificationType_ENG"].ConvertToString(), dtt.Rows[0]["IdentificationType_LOC"].ConvertToString()));
                    //tab = "\t";
                    //Response.Write(tab + Utils.GetLabel(dtt.Rows[0]["CIT_NO"].ConvertToString()));
                    //tab = "\t";
                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }

        protected void ExcelExportCustomized(DataTable dtRecords)
        {
            BeneficiaryService objservice = new BeneficiaryService();
            string XlsPath = Server.MapPath(@"~/Excel/BeneficiaryList.xls");
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                string tab = string.Empty;
                //Added S No for export to excel
                Response.Write(tab + Utils.GetLabel("SNo"));
                tab = "\t";
                //
                foreach (DataColumn datacol in dtRecords.Columns)
                {

                    Response.Write(tab + datacol.ColumnName);
                    tab = "\t";

                }
                Response.Write(tab + Utils.GetLabel("1st Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("2nd Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("3rd Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("4th Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("5th Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("6th Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("7th Owner"));
                tab = "\t";
                Response.Write(tab + Utils.GetLabel("8th Owner"));
                tab = "\t";


                Response.Write("\n");

                var rowCount = 0;
                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    //Added Sno for export to excel
                    rowCount++;

                    Response.Write(tab + Utils.GetLabel(Convert.ToString(rowCount)));
                    tab = "\t";
                    //
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        Response.Write(tab + Utils.GetLabel(Convert.ToString(dr[j])));
                        tab = "\t";

                    }
                    DataTable dtt = new DataTable();
                    dtt = objservice.GetOwner(dr[Utils.GetLabel("House No")].ConvertToString());
                    int i = 0;
                    foreach (DataRow drr in dtt.Rows)
                    {
                        Response.Write(tab + drr["FULL_NAME_ENG"].ConvertToString());
                        tab = "\t";
                        //Response.Write(tab + drr["FULL_NAME_LOC"].ConvertToString());
                        //tab = "\t";
                        //Response.Write(tab + drr["IdentificationType"].ConvertToString());
                        //tab = "\t";
                        //Response.Write(tab + Utils.GetLabel(drr["CIT_NO"].ConvertToString()));
                        //tab = "\t";

                        i++;
                    }
                    for (int j = i; j < 8; j++)
                    {
                        Response.Write(tab + "");
                        tab = "\t";
                        //Response.Write(tab + "");
                        //tab = "\t";
                        //Response.Write(tab + "");
                        //tab = "\t";
                        //Response.Write(tab + "");
                        //tab = "\t";

                    }

                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }

        #endregion
    }
}
