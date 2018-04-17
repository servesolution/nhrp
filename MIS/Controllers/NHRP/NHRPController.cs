using EntityFramework;
using ExceptionHandler;
using MIS.Models;
using MIS.Models.NHRP;
using MIS.Models.Registration.Household;
using MIS.Services;
using MIS.Services.Core;
using MIS.Services.NHRP.FileImport;
using MIS.Services.NHRP.Edit;
using MIS.Services.Registration.Household;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MIS.Models.Search;
using System.Data;
using MIS.Services.NHRP.Search;
using MIS.Services.NHRP.View;
using MIS.Models.NHRP.View;
using MIS.Services.Report;
using MIS.Models.Core;
using System.Reflection;
using MIS.Models.Security;
using System.Text;
using MIS.Authentication;
using System.Web.UI.WebControls;
using MIS.Core;

namespace MIS.Controllers.NHRP
{
    public class NHRPController : BaseController
    {
        CommonFunction common = null;
        SearchHouseholdModel household = new SearchHouseholdModel();
        MIG_MIS_HOUSEHOLD_INFO house = new MIG_MIS_HOUSEHOLD_INFO();
        HouseholdService househead = new HouseholdService();


        public NHRPController()
        {
            common = new CommonFunction();

            household = new SearchHouseholdModel();
            house = new MIG_MIS_HOUSEHOLD_INFO();
            househead = new HouseholdService();

        }
        /// <summary>
        /// File Import Functionality
        /// </summary>
        /// <returns></returns>


        #region FileImport

        #endregion

        /// <summary>
        /// File Search Functionality
        /// </summary>
        /// <returns></returns>
        #region Search
        /// <summary>
        /// Get Search Mode(House/Structure/Family/Member
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSearchMode(string SearchModeval)
        {
            List<SelectListItem> HouseNo = new List<SelectListItem>();
            HouseNo.Add(new SelectListItem { Text = "--- " + Utils.GetLabel("Select House No") + " ---", Value = "" });
            ViewData["HouseNo"] = HouseNo;

            if (SearchModeval.ConvertToString() == "House")
            {
                GetHouseDropDown();
                HouseSearch objModel = new HouseSearch();
                if (CommonVariables.GroupCD == "15")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    objModel.StrDistrict = Districtcode;
                }
                if (CommonVariables.GroupCD == "24")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    string VDCCode = CommonFunction.GetVDCByEmployeeCode(CommonVariables.EmpCode);
                    string vdcDefinedCD = common.GetDefinedCodeFromDataBase(VDCCode, "mis_vdc_municipality", "vdc_mun_cd");
                    objModel.StrDistrict = Districtcode;
                    objModel.StrVDC = vdcDefinedCD;

                }
                return PartialView("~/Views/NHRP/_HouseSearch.cshtml", objModel);
            }
            else if (SearchModeval.ConvertToString() == "Structure")
            {
                GetStructureDropDown();
                StructureSearch objModel = new StructureSearch();
                //if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                //{
                //    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                //    objModel.StrDistrict = Districtcode;
                //}
                return PartialView("~/Views/NHRP/_StructureSearch.cshtml", objModel);
            }
            else if (SearchModeval.ConvertToString() == "Family")
            {
                GetFamilyDropDown();
                HouseholdSearch objModel = new HouseholdSearch();
                //if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                //{
                //    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                //    objModel.FamilyDistrict = Districtcode;
                //}
                return PartialView("~/Views/NHRP/_HouseholdSearch.cshtml", objModel);
            }
            else if (SearchModeval.ConvertToString() == "Member")
            {
                GetMemberDropDown();
                MemberSearch objModel = new MemberSearch();
                //if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                //{
                //    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                //    objModel.PER_DISTRICT_CD = Districtcode;
                //}
                return PartialView("~/Views/NHRP/_MemberSearch.cshtml", objModel);
            }
            else
            {
                //by default open Housesearch
                GetHouseDropDown();
                HouseSearch objModel = new HouseSearch();
                return PartialView("~/Views/NHRP/_HouseSearch.cshtml", objModel);
            }
        }
        /// <summary>
        /// House Search
        /// </summary>
        /// <param name="objHousesearch"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        /// 


        public ActionResult HouseHoldInfoSearch()
        {


            List<SelectListItem> HouseNo = new List<SelectListItem>();
            HouseNo.Add(new SelectListItem { Text = "--- " + Utils.GetLabel("Select House No") + " ---", Value = "" });
            ViewData["HouseNo"] = HouseNo;

            List<SelectListItem> SearchMode = new List<SelectListItem>();
            SearchMode.Add(new SelectListItem { Text = Utils.GetLabel("House"), Value = "0" });
            SearchMode.Add(new SelectListItem { Text = Utils.GetLabel("Household"), Value = "1" });
            SearchMode.Add(new SelectListItem { Text = Utils.GetLabel("Member"), Value = "2" });
            ViewData["SearchMode"] = SearchMode;
            ViewData["ddlmobile"] = common.GetMobileNo("");

            GetDropDown();

            return View();
        }
        [HttpPost]
        public ActionResult HouseSearch(HouseSearch objHousesearch, FormCollection fc)
        {
            SearchService serSearch = new SearchService();
            // objHousesearch = new HouseSearch();
            DataTable result = new DataTable();
            //objNHRS.StrRegion = GetData.GetCodeFor(DataType.Region, fc["StrRegion"].ConvertToString());
            //objNHRS.StrZone = GetData.GetCodeFor(DataType.Zone, fc["StrZone"].ConvertToString());
            objHousesearch.StrDistrict = GetData.GetCodeFor(DataType.District, fc["StrDistrict"].ConvertToString());
            objHousesearch.StrWard = fc["StrWard"].ConvertToString();
            //ViewData["ddlmobile"] = common.GetMobileYesNo("");
            objHousesearch.STRUCTURE_COUNT_FROM = fc["STRUCTURE_COUNT_FROM"].ConvertToString();
            objHousesearch.STRUCTURE_COUNT_TO = fc["STRUCTURE_COUNT_TO"].ConvertToString();
            objHousesearch.TOTAL_OTHER_HOUSE_CNT_FROM = fc["TOTAL_OTHER_HOUSE_CNT_FROM"].ConvertToString();
            objHousesearch.TOTAL_OTHER_HOUSE_CNT_TO = fc["TOTAL_OTHER_HOUSE_CNT_TO"].ConvertToString();
            //objNHRS.DAMAGERESOLUTION = common.GetDescriptionFromCode(fc["TECHSOLUTION_CD"], "NHRS_TECHNICAL_SOLUTION", "DESC_ENG");
            objHousesearch.StrVDC = GetData.GetCodeFor(DataType.VdcMun, fc["StrVDC"].ConvertToString());
            objHousesearch.FULL_NAME_ENG = fc["FULL_NAME_ENG"].ConvertToString();
            string fullName = objHousesearch.FULL_NAME_ENG;
            var names = fullName.Split(' ');

            int count1 = names.Count();
            if (names[0] != "")
            {
                if (count1 > 2)
                {
                    objHousesearch.FULL_NAME_ENG = names[0] + " " + names[1] + " " + names[2];
                }
                if (count1 == 2)
                {
                    objHousesearch.FULL_NAME_ENG = names[0] + "  " + names[1];
                }

            }

            objHousesearch.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
            objHousesearch.INSTANCE_UNIQUE_SNO = fc["INSTANCE_UNIQUE_SNO"].ConvertToString();
            objHousesearch.INTERVIEW_DT_FROM = fc["INTERVIEW_DT_FROM"].ConvertToString();
            objHousesearch.INTERVIEW_DT_TO = fc["INTERVIEW_DT_TO"].ConvertToString();
            objHousesearch.StrHouseHoldSN = fc["StrHouseHoldSN"].ConvertToString();
            objHousesearch.ENUMERATOR_ID = fc["ENUMERATOR_ID"].ConvertToString();
            objHousesearch.MOBILE_NUMBER = fc["MOBILE_NUMBER"].ConvertToString();
            objHousesearch.HOUSE_OWNER_CNT = fc["HOUSE_OWNER_CNT"].ConvertToString();
            objHousesearch.NOT_INTERVIWING_REASON_CD = common.GetCodeFromDataBase(fc["ddl_NotInterviewingReason"].ConvertToString(), "NHRS_NOT_INTERVIWING_REASON", "NOT_INTERVIWING_REASON_CD");
            objHousesearch.Imei = fc["Imei"].ConvertToString();
            result = serSearch.getHouseSearchDetail(objHousesearch);
            if (CommonVariables.GroupCD != "1")
            {
                objHousesearch.Action = false;
            }
            else
            {
                objHousesearch.Action = true;
            }
            ViewData["result"] = result;
            Session["dtHouse"] = result;
            return PartialView("~/Views/NHRP/_HouseHoldInfobyHouse.cshtml", objHousesearch);

        }

        /// <summary>
        /// Structure Search
        /// </summary>
        /// <param name="objStructuresearch"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StructureSearch(StructureSearch objStructuresearch, FormCollection fc)
        {
            SearchService serSearch = new SearchService();
            // objHousesearch = new HouseSearch();
            DataTable result = new DataTable();
            //objNHRS.StrRegion = GetData.GetCodeFor(DataType.Region, fc["StrRegion"].ConvertToString());
            //objNHRS.StrZone = GetData.GetCodeFor(DataType.Zone, fc["StrZone"].ConvertToString());
            objStructuresearch.StrDistrict = GetData.GetCodeFor(DataType.District, fc["StrDistrict"].ConvertToString());
            objStructuresearch.StrWard = fc["StrWard"].ConvertToString();
            //objNHRS.DAMAGERESOLUTION = common.GetDescriptionFromCode(fc["TECHSOLUTION_CD"], "NHRS_TECHNICAL_SOLUTION", "DESC_ENG");
            objStructuresearch.StrVDC = GetData.GetCodeFor(DataType.VdcMun, fc["StrVDC"].ConvertToString());
            objStructuresearch.HOUSE_LEGALOWNER = (fc["ddl_houseLegalOwn"].ConvertToString());
            objStructuresearch.ENUMERATOR_ID = fc["ENUMERATOR_ID"].ConvertToString();
            objStructuresearch.MOBILE_NUMBER = fc["MOBILE_NUMBER"].ConvertToString();
            objStructuresearch.DAMAGEGRADE = common.GetValueFromDataBase((fc["DAMAGEGRADE"].ConvertToString()), "NHRS_DAMAGE_GRADE", "DAMAGE_GRADE_DEF_CD", "DAMAGE_GRADE_CD");

            objStructuresearch.StrFullName = fc["StrFullName"].ConvertToString();
            string fullName = objStructuresearch.StrFullName;
            var names = fullName.Split(' ');

            int count1 = names.Count();
            if (names[0] != "")
            {
                if (count1 > 2)
                {
                    objStructuresearch.StrFullName = names[0] + " " + names[1] + " " + names[2];
                }
                if (count1 == 2)
                {
                    objStructuresearch.StrFullName = names[0] + "  " + names[1];
                }

            }
            objStructuresearch.StrHouseHoldSN = fc["StrHouseHoldSN"].ConvertToString();
            objStructuresearch.INSTANCE_UNIQUE_SNO = fc["INSTANCE_UNIQUE_SNO"].ConvertToString();
            objStructuresearch.TECHSOLUTION_CD = common.GetValueFromDataBase((fc["TECHSOLUTION_CD"].ConvertToString()), "NHRS_TECHNICAL_SOLUTION", "TECHSOLUTION_DEF_CD", "TECHSOLUTION_CD");

            objStructuresearch.HOUSE_LAND_LEGAL_OWNER_CD = common.GetValueFromDataBase((fc["HOUSE_LAND_LEGAL_OWNER_CD"].ConvertToString()), "NHRS_HOUSE_LAND_LEGAL_OWNER", "DEFINED_CD", "HOUSE_LAND_LEGAL_OWNER_CD");

            objStructuresearch.HOUSE_AGE_FROM = fc["HOUSE_AGE_FROM"].ConvertToString();
            objStructuresearch.HOUSE_AGE_TO = fc["HOUSE_AGE_TO"].ConvertToString();


            //advanced search
            objStructuresearch.BUILDING_POSITION_CD = common.GetValueFromDataBase((fc["BUILDING_POSITION_CD"].ConvertToString()), "NHRS_BUILDING_POSITION_CONFIG", "BUILDING_POSITION_DEF_CD", "BUILDING_POSITION_CD");

            objStructuresearch.BUILDING_PLAN_CD = common.GetValueFromDataBase((fc["BUILDING_PLAN_CD"].ConvertToString()), "NHRS_BUILDING_PLAN_CONFIG", "BUILDING_PLAN_DEF_CD", "BUILDING_PLAN_CD");

            objStructuresearch.SECONDARY_USE = fc["SECONDARY_USE"].ConvertToString();
            objStructuresearch.Reconstruction = fc["Reconstruction"].ConvertToString();
            objStructuresearch.GEOTECHNICAL_RISK = fc["GeographicalRisk"].ConvertToString();
            objStructuresearch.Imei = fc["Imei"].ConvertToString();
            //objStructuresearch.BUILDING_CONDITION_CD = fc["BUILDING_CONDITION_CD"].ConvertToString();
            objStructuresearch.GROUND_SURFACE_CD = common.GetValueFromDataBase((fc["GROUND_SURFACE_CD"].ConvertToString()), "NHRS_GROUND_SURFACE", "GROUND_SURFACE_DEF_CD", "GROUND_SURFACE_CD");

            objStructuresearch.FOUNDATION_TYPE_CD = common.GetValueFromDataBase((fc["FOUNDATION_TYPE_CD"].ConvertToString()), "NHRS_FOUNDATION_TYPE", "FOUNDATION_TYPE_DEF_CD", "FOUNDATION_TYPE_CD");

            objStructuresearch.RC_MATERIAL_CD = common.GetValueFromDataBase((fc["RC_MATERIAL_CD"].ConvertToString()), "NHRS_ROOF_CONSTRUCT_MATERIAL", "RC_MATERIAL_DEF_CD", "RC_MATERIAL_CD");

            objStructuresearch.FC_MATERIAL_CD = common.GetValueFromDataBase((fc["FC_MATERIAL_CD"].ConvertToString()), "NHRS_FLOOR_CONSTRUCT_MATERIAL", "FC_MATERIAL_DEF_CD", "FC_MATERIAL_CD");


            objStructuresearch.SC_MATERIAL_CD = common.GetValueFromDataBase((fc["SC_MATERIAL_CD"].ConvertToString()), "NHRS_STOREY_CONSRUCT_MATERIAL",
            "SC_MATERIAL_DEF_CD", "SC_MATERIAL_CD");


            objStructuresearch.ASSESSED_AREA_CD = common.GetValueFromDataBase((fc["ASSESSED_AREA_CD"].ConvertToString()), "NHRS_ASSESSED_AREA",
           "ASSESSED_AREA_DEF_CD", "ASSESSED_AREA_CD");



            result = serSearch.getStructureSearchDetail(objStructuresearch);
            if (CommonVariables.GroupCD != "1")
            {
                objStructuresearch.Action = false;
            }
            else
            {
                objStructuresearch.Action = true;
            }
            ViewData["result"] = result;
            Session["dtStructure"] = result;
            return PartialView("~/Views/NHRP/_HouseholdInfobyStructure.cshtml", objStructuresearch);

        }

        /// <summary>
        /// Family Search
        /// </summary>
        /// <param name="objHouseHoldsearch"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HouseholdSearch(HouseholdSearch objHouseHoldsearch, FormCollection fc)
        {
            SearchService serSearch = new SearchService();  //Family search
            // objHousesearch = new HouseSearch();
            //HouseholdSearch objHouseHoldsearch = new HouseholdSearch();
            DataTable result = new DataTable();
            objHouseHoldsearch.StrFullName = fc["StrFullName"].ConvertToString();
            objHouseHoldsearch.SESSION_ID = fc["SESSION_ID"].ConvertToString();
            objHouseHoldsearch.MEMBER_ID = fc["MEMBER_ID"].ConvertToString();
            objHouseHoldsearch.DEFINED_CD = fc["DEFINED_CD"].ConvertToString();
            objHouseHoldsearch.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
            objHouseHoldsearch.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();

            objHouseHoldsearch.CUR_DISTRICT_CD = GetData.GetCodeFor(DataType.District, fc["CUR_DISTRICT_CD"].ConvertToString());
            objHouseHoldsearch.CUR_VDC_MUN_CD = GetData.GetCodeFor(DataType.VdcMun, fc["CUR_VDC_MUN_CD"].ConvertToString());
            objHouseHoldsearch.CUR_WARD_NO = fc["CUR_WARD_NO"].ConvertToString();
            objHouseHoldsearch.CUR_AREA_ENG = fc["CUR_AREA_ENG"].ConvertToString();
            objHouseHoldsearch.PER_DISTRICT_CD = GetData.GetCodeFor(DataType.District, fc["FamilyDistrict"].ConvertToString());
            objHouseHoldsearch.PER_VDC_MUN_CD = GetData.GetCodeFor(DataType.VdcMun, fc["FamilyVDC"].ConvertToString());
            objHouseHoldsearch.PER_WARD_NO = fc["FamilyWard"].ConvertToString();
            objHouseHoldsearch.ENTERED_BY = fc["ENTERED_BY"].ConvertToString();

            objHouseHoldsearch.FIRST_NAME_ENG = fc["FIRST_NAME_ENG"].ConvertToString();
            objHouseHoldsearch.MIDDLE_NAME_ENG = fc["MIDDLE_NAME_ENG"].ConvertToString();
            objHouseHoldsearch.LAST_NAME_ENG = fc["LAST_NAME_ENG"].ConvertToString();

            objHouseHoldsearch.EQ_VICTIM_IDENTITY_CARD_CD = common.GetValueFromDataBase((fc["EQ_VICTIM_IDENTITY_CARD_CD"].ConvertToString()), "NHRS_EQ_VICTIM_IDENTITY_CARD",
           "DEFINED_CD", "EQ_VICTIM_IDENTITY_CARD_CD");

            objHouseHoldsearch.CURRENT_SHELTER_CD = common.GetValueFromDataBase((fc["CurrentShelter"].ConvertToString()), "NHRS_SHELTER_SINCE_QUAKE",
           "SSEQ_DEFINED_CD", "SHELTER_SINCE_QUAKE_CD");

            objHouseHoldsearch.MONTHLY_INCOME_CD = common.GetValueFromDataBase((fc["MonthlyIncome"].ConvertToString()), "NHRS_MONTHLY_INCOME", "DEFINED_CD", "MONTHLY_INCOME_CD");

            objHouseHoldsearch.SHELTER_SINCE_QUAKE_CD = common.GetValueFromDataBase((fc["SHELTER_SINCE_QUAKE_CD"].ConvertToString()), "NHRS_SHELTER_SINCE_QUAKE",
           "SSEQ_DEFINED_CD", "SHELTER_SINCE_QUAKE_CD");
            objHouseHoldsearch.SHELTER_BEFORE_QUAKE_CD = common.GetValueFromDataBase((fc["SHELTER_BEFORE_QUAKE_CD"].ConvertToString()), "NHRS_SHELTER_BEFORE_QUAKE",
         "DEFINED_CD", "SHELTER_BEFORE_QUAKE_CD");
            objHouseHoldsearch.EQ_RELIEF_MONEY_CD = common.GetValueFromDataBase((fc["EQ_RELIEF_MONEY_CD"].ConvertToString()), "NHRS_EQ_RELIEF_MONEY",
         "DEFINED_CD", "EQ_RELIEF_MONEY_CD");

            objHouseHoldsearch.WATER_SOURCE_CD_II = common.GetValueFromDataBase((fc["ddl_WaterSource"].ConvertToString()), "MIS_WATER_SOURCE",
           "DEFINED_CD", "WATER_SOURCE_CD");

            objHouseHoldsearch.FUEL_SOURCE_CD_II = common.GetValueFromDataBase((fc["FUEL_SOURCE_CD_II"].ConvertToString()), "MIS_FUEL_SOURCE",
          "DEFINED_CD", "FUEL_SOURCE_CD");


            objHouseHoldsearch.LIGHT_SOURCE_CD_II = common.GetValueFromDataBase((fc["LIGHT_SOURCE_CD_II"].ConvertToString()), "MIS_LIGHT_SOURCE",
          "DEFINED_CD", "LIGHT_SOURCE_CD");
            objHouseHoldsearch.TOILET_TYPE_CD_I = common.GetValueFromDataBase((fc["TOILET_TYPE_CD_I"].ConvertToString()), "MIS_TOILET_TYPE",
          "DEFINED_CD", "TOILET_TYPE_CD");
            objHouseHoldsearch.Bank_Account = fc["ddl_BankAccNo"].ConvertToString();
            //objHouseHoldsearch.Mobile_Number = fc["Mobile_Number"].ConvertToString();
            objHouseHoldsearch.Mobile_Number = fc["ddl_mobileno"].ConvertToString();

            result = serSearch.getHouseholdSearchDetail(objHouseHoldsearch);
            if (CommonVariables.GroupCD != "1")
            {
                objHouseHoldsearch.Action = false;
            }
            else
            {
                objHouseHoldsearch.Action = true;
            }
            ViewData["result"] = result;
            Session["dtHousehold"] = result;
            return PartialView("~/Views/NHRP/_HouseHoldInfobyHouseHold.cshtml", objHouseHoldsearch); // family serach

        }

        /// <summary>
        /// Member Search
        /// </summary>
        /// <param name="objMembersearch"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MemberSearch(MemberSearch objMembersearch, FormCollection fc)
        {
            SearchService serSearch = new SearchService();
            // objHousesearch = new HouseSearch();
            DataTable result = new DataTable();

            objMembersearch.StrFullName = fc["StrFullName"].ConvertToString();
            objMembersearch.StrGender = fc["StrGender"].ConvertToString();
            objMembersearch.AgeFrom = fc["AgeFrom"].ConvertToString();
            objMembersearch.AgeTo = fc["AgeTo"].ConvertToString();

            objMembersearch.SESSION_ID = fc["SESSION_ID"].ConvertToString();
            objMembersearch.MEMBER_ID = fc["MEMBER_ID"].ConvertToString();
            objMembersearch.DEFINED_CD = fc["DEFINED_CD"].ConvertToString();
            objMembersearch.BIRTH_DT_ENG_ST = fc["BIRTH_DT_ENG_ST"].ConvertToString();
            objMembersearch.BIRTH_DT_ENG_TO = fc["BIRTH_DT_ENG_TO"].ConvertToString();
            objMembersearch.BIRTH_DT_ST = fc["BIRTH_DT_ST"].ConvertToString();
            objMembersearch.BIRTH_DT_TO = fc["BIRTH_DT_TO"].ConvertToString();
            objMembersearch.MARITAL_STATUS_CD = common.GetValueFromDataBase((fc["MARITAL_STATUS_CD"].ConvertToString()), "MIS_MARITAL_STATUS",
         "DEFINED_CD", "MARITAL_STATUS_CD");
            objMembersearch.CASTE_CD = common.GetValueFromDataBase((fc["CASTE_CD"].ConvertToString()), "MIS_CASTE",
         "DEFINED_CD", "CASTE_CD");

            objMembersearch.RELIGION_CD = common.GetValueFromDataBase((fc["RELIGION_CD"].ConvertToString()), "MIS_RELIGION",
         "DEFINED_CD", "RELIGION_CD");
            objMembersearch.EDUCATION_CD = common.GetValueFromDataBase((fc["EDUCATION_CD"].ConvertToString()), "MIS_CLASS_TYPE",
         "DEFINED_CD", "CLASS_TYPE_CD");
            objMembersearch.CTZ_NO = fc["CTZ_NO"].ConvertToString();
            objMembersearch.CTZ_ISSUE_DISTRICT_CD = GetData.GetCodeFor(DataType.District, fc["CTZ_ISSUE_DISTRICT_CD"].ConvertToString());
            objMembersearch.CTZ_ISSUE_DT = fc["CTZ_ISSUE_DT"].ConvertToString();
            objMembersearch.CTZ_ISSUE_DT_LOC = fc["CTZ_ISSUE_DT_LOC"].ConvertToString();
            objMembersearch.CUR_DISTRICT_CD = GetData.GetCodeFor(DataType.District, fc["CUR_DISTRICT_CD"].ConvertToString());
            objMembersearch.CUR_VDC_MUN_CD = GetData.GetCodeFor(DataType.VdcMun, fc["CUR_VDC_MUN_CD"].ConvertToString());
            objMembersearch.CUR_WARD_NO = fc["CUR_WARD_NO"].ConvertToString();
            objMembersearch.PER_DISTRICT_CD = GetData.GetCodeFor(DataType.District, fc["PER_DISTRICT_CD"].ConvertToString());
            objMembersearch.PER_VDC_MUN_CD = GetData.GetCodeFor(DataType.VdcMun, fc["PER_VDC_MUN_CD"].ConvertToString());
            objMembersearch.PER_WARD_NO = fc["PER_WARD_NO"].ConvertToString();
            objMembersearch.ENTERED_BY = fc["ENTERED_BY"].ConvertToString();

            objMembersearch.AGE = fc["AGE"].ConvertToString();

            objMembersearch.IDENTIFICATION_TYPE_CD = common.GetValueFromDataBase((fc["IDENTIFICATION_TYPE_CD"].ConvertToString()), "NHRS_IDENTIFICATION_TYPE",
         "DEFINED_CD", "IDENTIFICATION_TYPE_CD");
            objMembersearch.BANK_CD = common.GetValueFromDataBase((fc["BANK_CD"].ConvertToString()), "NHRS_BANK",
         "DEFINED_CD", "BANK_CD");
            result = serSearch.getMemberSearchDetail(objMembersearch);
            if (CommonVariables.GroupCD != "1")
            {
                objMembersearch.Action = false;
            }
            else
            {
                objMembersearch.Action = true;
            }
            ViewData["Member_Result"] = result;
            Session["dtMember"] = result;
            return PartialView("~/Views/NHRP/_HouseHoldInfobyMember.cshtml", objMembersearch);

        }


        #endregion

        # region Dropdown

        public void GetHouseDropDown()
        {
            HouseSearch objhousesearch = new HouseSearch();
            try
            {

                if (CommonVariables.EmpCode != "")
                {
                    if (CommonVariables.GroupCD == "15")
                    {
                        string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                        ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(Districtcode);
                        ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, Districtcode);
                        objhousesearch.StrDistrict = Districtcode;
                    }
                    if (CommonVariables.GroupCD == "24")
                    {
                        string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                        string VDCCode = CommonFunction.GetVDCByEmployeeCode(CommonVariables.EmpCode);
                        string vdcDefinedCD = common.GetDefinedCodeFromDataBase(VDCCode, "mis_vdc_municipality", "vdc_mun_cd");
                        ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(Districtcode);
                        ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(vdcDefinedCD, Districtcode);
                        objhousesearch.StrDistrict = Districtcode;
                        objhousesearch.StrVDC = vdcDefinedCD;
                    }
                    else
                    {
                        //ViewData["ddlmobile"] = common.GetMobileYesNo("");

                        ViewData["ddl_District"] = common.GetDistricts("");
                        ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);

                    }

                }
                else
                {
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
                }




                ViewData["ddl_mobileno"] = common.GetMobileNo("");
                ViewData["ddl_DistrictsPer"] = common.GetDistrictsbyZone(household.PER_DISTRICT_CD, household.PER_ZONE_ENG);
                ViewData["ddl_WardPer"] = common.GetWardByVDCMun(household.PER_WARD_NO, household.PER_VDCMUNICIPILITY_CD);
                ViewData["ddl_NotInterviewingReason"] = househead.GetNonInterviewingReason("");

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

        public void GetStructureDropDown()
        {
            StructureSearch objStructureSearch = new StructureSearch();
            try
            {
                if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                {
                   
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
                }
                else
                {
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
                } 

                ViewData["ddl_RegionPer"] = common.GetRegionForUser("");
                ViewData["ddl_ZonePer"] = common.GetZonebyRegion(household.PER_ZONE_ENG, household.PER_REGION_ENG);
                ViewData["ddl_DistrictsPer"] = common.GetDistrictsbyZone(household.PER_DISTRICT_CD, household.PER_ZONE_ENG);
                ViewData["ddl_WardPer"] = common.GetWardByVDCMun(household.PER_WARD_NO, household.PER_VDCMUNICIPILITY_CD);
                ViewData["ddl_VDCMunCur"] = common.GetVDCMunByDistrict(household.CUR_VDC_MUNICIPILITY_CD, household.CUR_DISTRICT_CD);
                ViewData["ddl_WardCur"] = common.GetWardByVDCMun(household.CUR_WARD_NO, household.CUR_VDC_MUNICIPILITY_CD);
                ViewData["ddl_Relation"] = common.GetRelation("");
                ViewData["ddl_NotInterviewingReason"] = househead.GetNonInterviewingReason("");
                ViewData["ddl_RoofType"] = househead.GetRoofMaterial("");
                ViewData["ddl_HouseMaterial"] = househead.GetFloorsOtherThanGround("");
                ViewData["ddl_HousePosition"] = househead.GetHousePosition("");
                ViewData["ddl_HousePlan"] = househead.GetHousePlan("");
                ViewData["ddl_HouseStructure"] = househead.GetHouseStructure("");
                ViewData["ddl_HouseFoundation"] = househead.GetFoundationType("");
                ViewData["ddl_FloorMaterial"] = househead.GetFloorMaterial("");
                ViewData["ddl_WaterSource"] = househead.GetWaterSource("");
                ViewData["ddl_FuelSource"] = househead.GetFuelSource("");
                ViewData["ddl_LightSource"] = househead.GetLightSource("");
                ViewData["ddl_ToiletType"] = househead.GetToiletType("");
                ViewData["ddl_DocumentType"] = househead.GetDocumentType("");
                ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterQuake("");
                ViewData["Owner"] = (List<SelectListItem>)common.GetYesNo("").Data;
                ViewData["ddl_houseLegalOwn"] = (List<SelectListItem>)common.GetYesNoSearch("").Data;

                ViewData["Residential_house"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Reconstruction"] = (List<SelectListItem>)common.GetYesNo1Search("").Data;
                ViewData["Geographical_Risk"] = (List<SelectListItem>)common.GetYesNo1Search("").Data;
                ViewData["Secondary_Use"] = (List<SelectListItem>)common.GetYesNo1Search("").Data;
                ViewData["Bank_Account"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Citizenship_victim"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Relief_fund"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_GetHouseMonitoredPosition"] = househead.GetHouseMonitoredPosition("");
                ViewData["ddl_TechnicalRemedy"] = househead.GetTechnicalSolution("");
                ViewData["Death"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["StudentsLeft"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Pregnant Checkup"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["VaccinationCnt"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ChangedProfession"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Missing"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_Gender"] = common.GetGender("").ToList();
                ViewData["ddl_temprorary_residence"] = househead.GetTemproraryResidence("");
                ViewData["ddl_householdhead_gender"] = common.GetGender("");
                ViewData["ddl_houseownergender"] = common.GetGender("");
                ViewData["ddl_familymembergender"] = common.GetGender("");
                ViewData["ddlEducation"] = common.GetEducation("");
                ViewData["ddlPresence"] = househead.GetPresenceStatus("");
                ViewData["ddlMaritalStatus"] = common.GetMaritalStatus("");
                ViewData["ddlSocialAllowance"] = common.GetAllowanceType("");
                ViewData["ddlHandicappedness"] = common.GetHandiColor("");
                ViewData["ddlDeathCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddlBirthCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeasedGender"] = common.GetGender("");
                ViewData["ddl_DeathRegistered"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeathCause"] = common.GetDeathReason("");
                ViewData["ddl_EarthquakeVictimGender"] = common.GetGender("");
                ViewData["ddl_TypeOfInjury"] = common.GetHumanLoss("");
                ViewData["ddl_Caste_Group"] = common.GetCasteGroup("");
                ViewData["ddl_Religion"] = common.GetReligion("");
                ViewData["ddl_ClassType"] = common.GetClassType("");
                ViewData["ddl_GeoTechnicalRisk"] = househead.GetGeoTechnicalRisk("");
                ViewData["ddl_SecondaryOccupancy"] = househead.GetSecondaryOccupancy("");
                ViewData["ddl_GroundSurface"] = househead.GetGroundSurfaceType("");
                ViewData["ddl_HLLOwner"] = househead.getHouseholdLegalOwner("");
                ViewData["ddl_damageGrade"] = househead.GetDamageGrade("");
                ViewData["ddl_Bedroom"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ToiletShared"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ShelterBeforeEQ"] = househead.getShelterBeforeEQ("");
                ViewData["ddl_EQVictimIDCard"] = househead.getEQVictimIdentityCard("");
                ViewData["ddl_EQReliefFund"] = househead.getEQReliefFund("");
                ViewData["ddl_Bank"] = househead.GetAllBank("");
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

        public void GetFamilyDropDown()
        {
            HouseholdSearch objHouseholdSearch = new HouseholdSearch();
            try
            {
                if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                {
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);

                }
                else
                {
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
                }
                ViewData["ddl_VDCMunCur"] = common.GetVDCMun(household.CUR_DISTRICT_CD);
                ViewData["ddl_DistrictCur"] = common.GetDistricts("");
                ViewData["ddl_RegionPer"] = common.GetRegionForUser("");
                ViewData["ddl_ZonePer"] = common.GetZonebyRegion(household.PER_ZONE_ENG, household.PER_REGION_ENG);
                ViewData["ddl_DistrictsPer"] = common.GetDistrictsbyZone(household.PER_DISTRICT_CD, household.PER_ZONE_ENG);
                ViewData["ddl_WardPer"] = common.GetWardByVDCMun(household.PER_WARD_NO, household.PER_VDCMUNICIPILITY_CD);
                ViewData["ddl_VDCMunCur"] = common.GetVDCMunByDistrict(household.CUR_VDC_MUNICIPILITY_CD, household.CUR_DISTRICT_CD);
                ViewData["ddl_WardCur"] = common.GetWardByVDCMun(household.CUR_WARD_NO, household.CUR_VDC_MUNICIPILITY_CD);
                ViewData["ddl_Relation"] = common.GetRelation("");
                ViewData["ddl_NotInterviewingReason"] = househead.GetNonInterviewingReason("");

                ViewData["ddl_MonthlyIncome"] = househead.GetMonthlyIncome("");
                ViewData["ddl_RoofType"] = househead.GetRoofMaterial("");
                ViewData["ddl_HouseMaterial"] = househead.GetFloorsOtherThanGround("");
                ViewData["ddl_HousePosition"] = househead.GetHousePosition("");
                ViewData["ddl_HousePlan"] = househead.GetHousePlan("");
                ViewData["ddl_HouseStructure"] = househead.GetHouseStructure("");
                ViewData["ddl_HouseFoundation"] = househead.GetFoundationType("");
                ViewData["ddl_FloorMaterial"] = househead.GetFloorMaterial("");
                ViewData["ddl_WaterSource"] = househead.GetWaterSource("");
                ViewData["ddl_FuelSource"] = househead.GetFuelSource("");
                ViewData["ddl_LightSource"] = househead.GetLightSource("");
                ViewData["ddl_ToiletType"] = househead.GetToiletType("");
                ViewData["ddl_DocumentType"] = househead.GetDocumentType("");
                ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterQuake("");
                ViewData["Owner"] = (List<SelectListItem>)common.GetYesNo("").Data;



                ViewData["Residential_house"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Reconstruction"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Geographical_Risk"] = (List<SelectListItem>)common.GetYesNo1Search("").Data;
                ViewData["Secondary_Use"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Bank_Account"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Citizenship_victim"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Relief_fund"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_GetHouseMonitoredPosition"] = househead.GetHouseMonitoredPosition("");
                ViewData["ddl_TechnicalRemedy"] = househead.GetTechnicalSolution("");
                ViewData["Death"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["StudentsLeft"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Pregnant Checkup"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["VaccinationCnt"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ChangedProfession"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Missing"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_Gender"] = common.GetGender("").ToList();
                ViewData["ddl_temprorary_residence"] = househead.GetTemproraryResidence("");
                ViewData["ddl_householdhead_gender"] = common.GetGender("");
                ViewData["ddl_houseownergender"] = common.GetGender("");
                ViewData["ddl_familymembergender"] = common.GetGender("");
                ViewData["ddlEducation"] = common.GetEducation("");
                ViewData["ddlPresence"] = househead.GetPresenceStatus("");
                ViewData["ddlMaritalStatus"] = common.GetMaritalStatus("");
                ViewData["ddlSocialAllowance"] = common.GetAllowanceType("");
                ViewData["ddlHandicappedness"] = common.GetHandiColor("");
                ViewData["ddlDeathCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddlBirthCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeasedGender"] = common.GetGender("");
                ViewData["ddl_DeathRegistered"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeathCause"] = common.GetDeathReason("");
                ViewData["ddl_EarthquakeVictimGender"] = common.GetGender("");
                ViewData["ddl_TypeOfInjury"] = common.GetHumanLoss("");
                ViewData["ddl_Caste_Group"] = common.GetCasteGroup("");
                ViewData["ddl_Religion"] = common.GetReligion("");
                ViewData["ddl_ClassType"] = common.GetClassType("");
                ViewData["ddl_GeoTechnicalRisk"] = househead.GetGeoTechnicalRisk("");
                ViewData["ddl_SecondaryOccupancy"] = househead.GetSecondaryOccupancy("");
                ViewData["ddl_GroundSurface"] = househead.GetGroundSurfaceType("");
                ViewData["ddl_HLLOwner"] = househead.getHouseholdLegalOwner("");
                ViewData["ddl_damageGrade"] = househead.GetDamageGrade("");
                ViewData["ddl_Bedroom"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ToiletShared"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ShelterBeforeEQ"] = househead.getShelterBeforeEQ("");
                ViewData["ddl_EQVictimIDCard"] = househead.getEQVictimIdentityCard("");
                ViewData["ddl_EQReliefFund"] = househead.getEQReliefFund("");
                ViewData["ddl_Bank"] = househead.GetAllBank("");
                ViewData["ddl_mobileno"] = (List<SelectListItem>)common.GetYesNo1Search("").Data;
                ViewData["ddl_BankAccNo"] = (List<SelectListItem>)common.GetYesNo1Search("").Data;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

        public void GetMemberDropDown()
        {
            MemberSearch objMemberSearch = new MemberSearch();
            try
            {
                if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                { 
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);

                }
                else
                {
                    ViewData["ddl_District"] = common.GetDistricts("");
                    ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
                }
                ViewData["ddl_DistrictCur"] = common.GetDistricts("");
                ViewData["ddl_RegionPer"] = common.GetRegionForUser("");
                ViewData["ddl_VDCMunCur"] = common.GetVDCMunByDistrict(household.CUR_VDC_MUNICIPILITY_CD, household.CUR_DISTRICT_CD);
                ViewData["ddl_ZonePer"] = common.GetZonebyRegion(household.PER_ZONE_ENG, household.PER_REGION_ENG);
                ViewData["ddl_DistrictsPer"] = common.GetDistrictsbyZone(household.PER_DISTRICT_CD, household.PER_ZONE_ENG);
                ViewData["ddl_WardPer"] = common.GetWardByVDCMun(household.PER_WARD_NO, household.PER_VDCMUNICIPILITY_CD);
                //ViewData["ddl_VDCMunCur"] = common.GetVDCMunByDistrict(household.CUR_VDC_MUNICIPILITY_CD, household.CUR_DISTRICT_CD);
                ViewData["ddl_WardCur"] = common.GetWardByVDCMun(household.CUR_WARD_NO, household.CUR_VDC_MUNICIPILITY_CD);
                ViewData["ddl_Relation"] = common.GetRelation("");
                ViewData["ddl_NotInterviewingReason"] = househead.GetNonInterviewingReason("");
                ViewData["ddl_RoofType"] = househead.GetRoofMaterial("");
                ViewData["ddl_HouseMaterial"] = househead.GetFloorsOtherThanGround("");
                ViewData["ddl_HousePosition"] = househead.GetHousePosition("");
                ViewData["ddl_HousePlan"] = househead.GetHousePlan("");
                ViewData["ddl_HouseStructure"] = househead.GetHouseStructure("");
                ViewData["ddl_HouseFoundation"] = househead.GetFoundationType("");
                ViewData["ddl_FloorMaterial"] = househead.GetFloorMaterial("");
                ViewData["ddl_WaterSource"] = househead.GetWaterSource("");
                ViewData["ddl_FuelSource"] = househead.GetFuelSource("");
                ViewData["ddl_LightSource"] = househead.GetLightSource("");
                ViewData["ddl_ToiletType"] = househead.GetToiletType("");
                ViewData["ddl_DocumentType"] = househead.GetDocumentType("");
                ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterQuake("");
                ViewData["Owner"] = (List<SelectListItem>)common.GetYesNo("").Data;



                ViewData["Residential_house"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Reconstruction"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Geographical_Risk"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Secondary_Use"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Bank_Account"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Citizenship_victim"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Relief_fund"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_GetHouseMonitoredPosition"] = househead.GetHouseMonitoredPosition("");
                ViewData["ddl_TechnicalRemedy"] = househead.GetTechnicalSolution("");
                ViewData["Death"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["StudentsLeft"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Pregnant Checkup"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["VaccinationCnt"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ChangedProfession"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Missing"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_Gender"] = common.GetGender("").ToList();
                ViewData["ddl_temprorary_residence"] = househead.GetTemproraryResidence("");
                ViewData["ddl_householdhead_gender"] = common.GetGender("");
                ViewData["ddl_houseownergender"] = common.GetGender("");
                ViewData["ddl_familymembergender"] = common.GetGender("");
                ViewData["ddlEducation"] = common.GetEducation("");
                ViewData["ddlPresence"] = househead.GetPresenceStatus("");
                ViewData["ddlMaritalStatus"] = common.GetMaritalStatus("");
                ViewData["ddlSocialAllowance"] = common.GetAllowanceType("");
                ViewData["ddlHandicappedness"] = common.GetHandiColor("");
                ViewData["ddlDeathCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddlBirthCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeasedGender"] = common.GetGender("");
                ViewData["ddl_DeathRegistered"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeathCause"] = common.GetDeathReason("");
                ViewData["ddl_EarthquakeVictimGender"] = common.GetGender("");
                ViewData["ddl_TypeOfInjury"] = common.GetHumanLoss("");
                ViewData["ddl_Caste_Group"] = common.GetCasteGroup("");
                ViewData["ddl_Religion"] = common.GetReligion("");
                ViewData["ddl_ClassType"] = common.GetClassType("");
                ViewData["ddl_GeoTechnicalRisk"] = househead.GetGeoTechnicalRisk("");
                ViewData["ddl_SecondaryOccupancy"] = househead.GetSecondaryOccupancy("");
                ViewData["ddl_GroundSurface"] = househead.GetGroundSurfaceType("");
                ViewData["ddl_HLLOwner"] = househead.getHouseholdLegalOwner("");
                ViewData["ddl_damageGrade"] = househead.GetDamageGrade("");
                ViewData["ddl_Bedroom"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ToiletShared"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ShelterBeforeEQ"] = househead.getShelterBeforeEQ("");
                ViewData["ddl_EQVictimIDCard"] = househead.getEQVictimIdentityCard("");
                ViewData["ddl_EQReliefFund"] = househead.getEQReliefFund("");
                ViewData["ddl_Bank"] = househead.GetAllBank("");
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

        public void GetDropDown()
        {
            try
            {
                //ViewData["ddlmobile"] = common.GetMobileYesNo("");
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_RegionPer"] = common.GetRegionForUser("");
                ViewData["ddl_ZonePer"] = common.GetZonebyRegion(household.PER_ZONE_ENG, household.PER_REGION_ENG);
                ViewData["ddl_DistrictsPer"] = common.GetDistrictsbyZone(household.PER_DISTRICT_CD, household.PER_ZONE_ENG);
                ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
                ViewData["ddl_WardPer"] = common.GetWardByVDCMun(household.PER_WARD_NO, household.PER_VDCMUNICIPILITY_CD);
                ViewData["ddl_VDCMunCur"] = common.GetVDCMunByDistrict(household.CUR_VDC_MUNICIPILITY_CD, household.CUR_DISTRICT_CD);
                ViewData["ddl_WardCur"] = common.GetWardByVDCMun(household.CUR_WARD_NO, household.CUR_VDC_MUNICIPILITY_CD);
                ViewData["ddl_Relation"] = common.GetRelation("");
                ViewData["ddl_NotInterviewingReason"] = househead.GetNonInterviewingReason("");

                ViewData["ddl_RoofType"] = househead.GetRoofMaterial("");
                ViewData["ddl_HouseMaterial"] = househead.GetFloorsOtherThanGround("");
                ViewData["ddl_HousePosition"] = househead.GetHousePosition("");
                ViewData["ddl_HousePlan"] = househead.GetHousePlan("");
                ViewData["ddl_HouseStructure"] = househead.GetHouseStructure("");
                ViewData["ddl_HouseFoundation"] = househead.GetFoundationType("");
                ViewData["ddl_FloorMaterial"] = househead.GetFloorMaterial("");
                ViewData["ddl_WaterSource"] = househead.GetWaterSource("");
                ViewData["ddl_FuelSource"] = househead.GetFuelSource("");
                ViewData["ddl_LightSource"] = househead.GetLightSource("");
                ViewData["ddl_ToiletType"] = househead.GetToiletType("");
                ViewData["ddl_DocumentType"] = househead.GetDocumentType("");
                ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterQuake("");
                ViewData["Owner"] = (List<SelectListItem>)common.GetYesNo("").Data;

                ViewData["Residential_house"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Reconstruction"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Geographical_Risk"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Secondary_Use"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Bank_Account"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Citizenship_victim"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Relief_fund"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_GetHouseMonitoredPosition"] = househead.GetHouseMonitoredPosition("");
                ViewData["ddl_TechnicalRemedy"] = househead.GetTechnicalSolution("");
                ViewData["Death"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["StudentsLeft"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Pregnant Checkup"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["VaccinationCnt"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ChangedProfession"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Missing"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_Gender"] = common.GetGender("").ToList();
                ViewData["ddl_temprorary_residence"] = househead.GetTemproraryResidence("");
                ViewData["ddl_householdhead_gender"] = common.GetGender("");
                ViewData["ddl_houseownergender"] = common.GetGender("");
                ViewData["ddl_familymembergender"] = common.GetGender("");
                ViewData["ddlEducation"] = common.GetEducation("");
                ViewData["ddlPresence"] = househead.GetPresenceStatus("");
                ViewData["ddlMaritalStatus"] = common.GetMaritalStatus("");
                ViewData["ddlSocialAllowance"] = common.GetAllowanceType("");
                ViewData["ddlHandicappedness"] = common.GetHandiColor("");
                ViewData["ddlDeathCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddlBirthCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeasedGender"] = common.GetGender("");
                ViewData["ddl_DeathRegistered"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeathCause"] = common.GetDeathReason("");
                ViewData["ddl_EarthquakeVictimGender"] = common.GetGender("");
                ViewData["ddl_TypeOfInjury"] = common.GetHumanLoss("");
                ViewData["ddl_Caste_Group"] = common.GetCasteGroup("");
                ViewData["ddl_Religion"] = common.GetReligion("");
                ViewData["ddl_ClassType"] = common.GetClassType("");
                ViewData["ddl_GeoTechnicalRisk"] = househead.GetGeoTechnicalRisk("");
                ViewData["ddl_SecondaryOccupancy"] = househead.GetSecondaryOccupancy("");
                ViewData["ddl_GroundSurface"] = househead.GetGroundSurfaceType("");
                ViewData["ddl_HLLOwner"] = househead.getHouseholdLegalOwner("");
                ViewData["ddl_damageGrade"] = househead.GetDamageGrade("");
                ViewData["ddl_Bedroom"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ToiletShared"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_ShelterBeforeEQ"] = househead.getShelterBeforeEQ("");
                ViewData["ddl_EQVictimIDCard"] = househead.getEQVictimIdentityCard("");
                ViewData["ddl_EQReliefFund"] = househead.getEQReliefFund("");
                ViewData["ddl_Bank"] = househead.GetAllBank("");
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

        #endregion

        public void CheckPermission()
        {
            PermissionParamService objPermissionParamService = new PermissionParamService();
            PermissionParam objPermissionParam = new PermissionParam();
            ViewBag.EnableEdit = "false";
            ViewBag.EnableDelete = "false";
            ViewBag.EnableAdd = "false";
            ViewBag.EnableApprove = "false";
            ViewBag.EnablePrint = "false";
            try
            {
                objPermissionParam = objPermissionParamService.GetPermissionValue();
                if (objPermissionParam != null)
                {

                    if (objPermissionParam.EnableAdd == "true")
                    {
                        ViewBag.EnableAdd = "true";
                    }
                    if (objPermissionParam.EnableEdit == "true")
                    {
                        ViewBag.EnableEdit = "true";
                    }
                    if (objPermissionParam.EnableDelete == "true")
                    {
                        ViewBag.EnableDelete = "true";
                    }
                    if (objPermissionParam.EnableApprove == "true")
                    {
                        ViewBag.EnableApprove = "true";
                    }
                    if (objPermissionParam.EnablePrint == "true")
                    {
                        ViewBag.EnablePrint = "true";
                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }
        [HttpGet]


        public ActionResult HouseView(string p)
        {
            DataTable dtHouseDetail = new DataTable();
            dtHouseDetail = null;
            DataTable dtHouseOwner = new DataTable();
            dtHouseOwner = null;
            NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
            VW_HOUSE_OWNER_DTL ownerDtlModel = new VW_HOUSE_OWNER_DTL();
            VW_HOUSE_BUILDING_DTL builidingDtlModel = new VW_HOUSE_BUILDING_DTL();
            string id = string.Empty;
            try
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        id = (rvd["id2"]).ConvertToString();
                    }
                }
                DataTable resultDamageDetail = new DataTable();
                resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                ViewData["resultDamageDetail"] = resultDamageDetail;
                if (id != "" && id != null)
                {
                    HouseBuildingDetailViewService objHouseBuildingService = new HouseBuildingDetailViewService();
                    //  DataSet ds = objHouseBuildingService.HouseFamilyDetailAll("1-4-5-81132519877", "19", "1");


                    DataTable dtGrievMemberDetail = objHouseBuildingService.GrievMemDetail(id);
                    if (dtGrievMemberDetail != null && dtGrievMemberDetail.Rows.Count > 0)
                    {
                        int count = 1;
                        int less = 1;
                        List<GrvDtlToHouseViewModelclass> ownerList = new List<GrvDtlToHouseViewModelclass>();
                        for (int i = 0; i < dtGrievMemberDetail.Rows.Count; i++)
                        {
                            GrvDtlToHouseViewModelclass MemDtl = new GrvDtlToHouseViewModelclass();
                            MemDtl.houseSN = dtGrievMemberDetail.Rows[i]["HOUSE_SNO"].ConvertToString();

                            MemDtl.buildingSn = dtGrievMemberDetail.Rows[i]["BUILDING_STRUCTURE_NO"].ConvertToString();
                            //MemDtl.familySn = dr["household_id"].ToDecimal();
                            MemDtl.nissaNo = dtGrievMemberDetail.Rows[i]["INSTANCE_UNIQUE_SNO"].ConvertToString();
                            MemDtl.memFullname = dtGrievMemberDetail.Rows[i]["FULL_NAME_ENG"].ConvertToString();
                            MemDtl.memeFullNameLoc = dtGrievMemberDetail.Rows[i]["FULL_NAME_LOC"].ConvertToString();
                            MemDtl.gender = dtGrievMemberDetail.Rows[i]["GENDER_ENG"].ConvertToString();
                            MemDtl.genderLoc = dtGrievMemberDetail.Rows[i]["GENDER_LOC"].ConvertToString();

                            MemDtl.relationship = dtGrievMemberDetail.Rows[i]["RELATION_ENG"].ConvertToString();
                            MemDtl.relationshipLoc = dtGrievMemberDetail.Rows[i]["RELATION_ENG"].ConvertToString();

                            MemDtl.age = dtGrievMemberDetail.Rows[i]["AGE"].ConvertToString();
                            MemDtl.education = dtGrievMemberDetail.Rows[i]["EDUCATION_ENG"].ConvertToString();
                            MemDtl.educationLoc = dtGrievMemberDetail.Rows[i]["EDUCATION_LOC"].ConvertToString();

                            MemDtl.citizenshipNo = dtGrievMemberDetail.Rows[i]["CITIZENSHIP_NO"].ConvertToString();
                            MemDtl.phoneNo = dtGrievMemberDetail.Rows[i]["MOBILE_NUMBER"].ConvertToString();





                            MemDtl.familySn = Convert.ToInt32(dtGrievMemberDetail.Rows[i]["member_cnt"]);
                            if (less > MemDtl.familySn)
                            {
                                less = 0;
                            }
                            MemDtl.familySn = MemDtl.familySn - less;
                            if (less == 1)
                            {

                                MemDtl.familyType = "F" + count;
                            }
                            if (MemDtl.familySn == 0)
                            {
                                MemDtl.familyType = "F" + count;
                                count = count + 1;
                                less = 0;
                            }
                            else
                            {
                                if (i != 0 && i < dtGrievMemberDetail.Rows.Count - 1)
                                {
                                    if (MemDtl.buildingSn != dtGrievMemberDetail.Rows[i - 1]["BUILDING_STRUCTURE_NO"].ConvertToString())
                                    {
                                        count = 1;
                                        MemDtl.familyType = "F" + count;
                                    }
                                    else
                                    {
                                        MemDtl.familyType = "F" + count;
                                    }
                                }

                            }
                            ownerList.Add(MemDtl);
                            less++;

                        }
                        ownerDtlModel.grievDtlList = ownerList;
                    }

                    DataTable dtOthHouseDtl = objHouseBuildingService.GetOthLivableHouseDtl(id);
                    if (dtOthHouseDtl != null && dtOthHouseDtl.Rows.Count > 0)
                    {
                        List<OtherLiveableHouseDtlModelClass> HouseDtlList = new List<OtherLiveableHouseDtlModelClass>();
                        foreach (DataRow dr in dtOthHouseDtl.Rows)
                        {
                            OtherLiveableHouseDtlModelClass HouseDtl = new OtherLiveableHouseDtlModelClass();
                            HouseDtl.structure = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                            HouseDtl.surveyorGrade = dr["SURVEYED_DAMAGE_GRADE_CD"].ConvertToString();
                            HouseDtl.surveyorTechnicalSoltn = dr["SURVEYED_TECHNICAL_SOLUTION_CD"].ConvertToString();
                            HouseDtl.surveyorTechnicalSoltnEng = common.GetDescriptionFromCode(HouseDtl.surveyorTechnicalSoltn.ConvertToString(), "NHRS_TECHNICAL_SOLUTION", "TECHSOLUTION_CD");

                            HouseDtl.metrixGrade = dr["MATRIX_GRADE_CD"].ConvertToString();
                            HouseDtl.photoGrade = dr["PHOTO_GRADE_CD"].ConvertToString();
                            HouseDtl.SifarisGrade = dr["OFFICER_GRADE_CD"].ConvertToString();
                            HouseDtl.technicalsolution = dr["OFFICER_TECHNICAL_SOLUTION_CD"].ConvertToString();
                            HouseDtl.technicalsolutionDes = common.GetDescriptionFromCode(HouseDtl.technicalsolution.ConvertToString(), "NHRS_TECHNICAL_SOLUTION", "TECHSOLUTION_CD");

                            HouseDtl.fieldInspection = dr["STATUS"].ConvertToString();
                            HouseDtl.Recomendation_flag = dr["Recomendation_flag"].ConvertToString();
                            HouseDtl.Clarification_flag = dr["Clarification_flag"].ConvertToString();
                            HouseDtl.GRIEVANT_NAME = dr["FULL_NAME_ENG"].ConvertToString();
                            HouseDtl.remarks = dr["REMARKS"].ConvertToString();
                            HouseDtl.remarksLoc = dr["REMARKS_LOC"].ConvertToString();
                            HouseDtl.updatedBy = dr["LAST_UPDATED_BY"].ConvertToString();
                            HouseDtl.upDatedDate = dr["LAST_UPDATED_DT"].ConvertToString();


                            HouseDtlList.Add(HouseDtl);
                        }
                        ownerDtlModel.LiveablehouseDtlList = HouseDtlList;
                    }






                    dtHouseOwner = objHouseBuildingService.HouseOwnerDetail(id);
                    if (dtHouseOwner != null && dtHouseOwner.Rows.Count > 0)
                    {
                        ownerDtlModel.HOUSE_OWNER_ID = dtHouseOwner.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                        ownerDtlModel.DEFINED_CD = dtHouseOwner.Rows[0]["DEFINED_CD"].ConvertToString();
                        ownerDtlModel.ENUMERATOR_ID = dtHouseOwner.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                        ownerDtlModel.INTERVIEW_DT = dtHouseOwner.Rows[0]["INTERVIEW_DT"].ToDateTime();
                        ownerDtlModel.INTERVIEW_DT_LOC = dtHouseOwner.Rows[0]["INTERVIEW_DT"].ToDateTime().ConvertToString();
                        ownerDtlModel.INTERVIEW_START = dtHouseOwner.Rows[0]["INTERVIEW_START"].ConvertToString();
                        ownerDtlModel.INTERVIEW_ST_HH = dtHouseOwner.Rows[0]["INTERVIEW_ST_HH"].ConvertToString();
                        ownerDtlModel.INTERVIEW_ST_MM = dtHouseOwner.Rows[0]["INTERVIEW_ST_MM"].ConvertToString();
                        ownerDtlModel.INTERVIEW_ST_SS = dtHouseOwner.Rows[0]["INTERVIEW_ST_SS"].ConvertToString();
                        ownerDtlModel.INTERVIEW_ST_MS = dtHouseOwner.Rows[0]["INTERVIEW_ST_MS"].ConvertToString();
                        ownerDtlModel.INTERVIEW_END = dtHouseOwner.Rows[0]["INTERVIEW_END"].ConvertToString();
                        ownerDtlModel.INTERVIEW_END_HH = dtHouseOwner.Rows[0]["INTERVIEW_END_HH"].ConvertToString();
                        ownerDtlModel.INTERVIEW_END_MM = dtHouseOwner.Rows[0]["INTERVIEW_END_MM"].ConvertToString();
                        ownerDtlModel.INTERVIEW_END_SS = dtHouseOwner.Rows[0]["INTERVIEW_END_SS"].ConvertToString();
                        ownerDtlModel.INTERVIEW_END_MS = dtHouseOwner.Rows[0]["INTERVIEW_END_MS"].ConvertToString();
                        ownerDtlModel.INTERVIEW_GMT = dtHouseOwner.Rows[0]["INTERVIEW_GMT"].ConvertToString();
                        ownerDtlModel.KLLDistrictCode = dtHouseOwner.Rows[0]["KLL_DISTRICT_CD"].ConvertToString();
                        ownerDtlModel.SubmissionDate = dtHouseOwner.Rows[0]["SUBMISSIONTIME"].ConvertToString();
                        ownerDtlModel.SubmissionDate = System.Convert.ToDateTime(dtHouseOwner.Rows[0]["SUBMISSIONTIME"]).ToString("yyyy-MM-dd");
                        ownerDtlModel.COUNTRY_CD = dtHouseOwner.Rows[0]["COUNTRY_CD"].ToDecimal();
                        ownerDtlModel.REG_ST_CD = dtHouseOwner.Rows[0]["REG_ST_CD"].ToDecimal();
                        ownerDtlModel.ZONE_CD = dtHouseOwner.Rows[0]["ZONE_CD"].ToDecimal();
                        ownerDtlModel.DISTRICT_CD = dtHouseOwner.Rows[0]["DISTRICT_CD"].ToDecimal();
                        ownerDtlModel.VDC_MUN_CD = dtHouseOwner.Rows[0]["VDC_MUN_CD"].ToDecimal();
                        ownerDtlModel.WARD_NO = dtHouseOwner.Rows[0]["WARD_NO"].ToDecimal();
                        ownerDtlModel.ENUMERATION_AREA = dtHouseOwner.Rows[0]["ENUMERATION_AREA"].ConvertToString();
                        ownerDtlModel.AREA_ENG = dtHouseOwner.Rows[0]["AREA_ENG"].ConvertToString();
                        ownerDtlModel.AREA_LOC = dtHouseOwner.Rows[0]["AREA_LOC"].ConvertToString();
                        ownerDtlModel.DISTRICT_ENG = dtHouseOwner.Rows[0]["DISTRICT_ENG"].ConvertToString();
                        ownerDtlModel.DISTRICT_LOC = dtHouseOwner.Rows[0]["DISTRICT_LOC"].ConvertToString();
                        ownerDtlModel.COUNTRY_ENG = dtHouseOwner.Rows[0]["COUNTRY_ENG"].ConvertToString();
                        ownerDtlModel.COUNTRY_LOC = dtHouseOwner.Rows[0]["COUNTRY_LOC"].ConvertToString();
                        ownerDtlModel.REGION_LOC = dtHouseOwner.Rows[0]["REGION_LOC"].ConvertToString();
                        ownerDtlModel.ZONE_ENG = dtHouseOwner.Rows[0]["ZONE_ENG"].ConvertToString();
                        ownerDtlModel.ZONE_LOC = dtHouseOwner.Rows[0]["ZONE_LOC"].ConvertToString();
                        ownerDtlModel.VDC_ENG = dtHouseOwner.Rows[0]["VDC_ENG"].ConvertToString();
                        ownerDtlModel.VDC_LOC = dtHouseOwner.Rows[0]["VDC_LOC"].ConvertToString();
                        ownerDtlModel.HOUSE_FAMILY_OWNER_CNT = dtHouseOwner.Rows[0]["HOUSE_FAMILY_OWNER_CNT"].ToDecimal();
                        ownerDtlModel.RESPONDENT_IS_HOUSE_OWNER = dtHouseOwner.Rows[0]["RESPONDENT_IS_HOUSE_OWNER"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "हो") : Utils.ToggleLanguage("No", "होइन");
                        ownerDtlModel.NOT_INTERVIWING_REASON_CD = dtHouseOwner.Rows[0]["NOT_INTERVIWING_REASON_CD"].ToDecimal();
                        ownerDtlModel.NOT_INTERVIWING_REASON = dtHouseOwner.Rows[0]["NOT_INTERVIWING_REASON"].ConvertToString();
                        ownerDtlModel.NOT_INTERVIWING_REASON_LOC = dtHouseOwner.Rows[0]["NOT_INTERVIWING_REASON_LOC"].ConvertToString();
                        ownerDtlModel.ELECTIONCENTER_OHOUSE_CNT = dtHouseOwner.Rows[0]["ELECTIONCENTER_OHOUSE_CNT"].ToDecimal();
                        ownerDtlModel.NONELECTIONCENTER_FHOUSE_CNT = dtHouseOwner.Rows[0]["NONELECTIONCENTER_FHOUSE_CNT"].ToDecimal();
                        ownerDtlModel.NONRESID_NONDAMAGE_H_CNT = dtHouseOwner.Rows[0]["NONRESID_NONDAMAGE_H_CNT"].ToDecimal();
                        ownerDtlModel.NONRESID_PARTIAL_DAMAGE_H_CNT = dtHouseOwner.Rows[0]["NONRESID_PARTIAL_DAMAGE_H_CNT"].ToDecimal();
                        ownerDtlModel.NONRESID_FULL_DAMAGE_H_CNT = dtHouseOwner.Rows[0]["NONRESID_FULL_DAMAGE_H_CNT"].ToDecimal();
                        ownerDtlModel.SOCIAL_MOBILIZER_PRESENT_FLAG = dtHouseOwner.Rows[0]["SOCIAL_MOBILIZER_PRESENT_FLAG"].ConvertToString();
                        ownerDtlModel.SOCIAL_MOBILIZER_NAME = dtHouseOwner.Rows[0]["SOCIAL_MOBILIZER_NAME"].ConvertToString();
                        ownerDtlModel.SOCIAL_MOBILIZER_NAME_LOC = dtHouseOwner.Rows[0]["SOCIAL_MOBILIZER_NAME_LOC"].ConvertToString();
                        ownerDtlModel.IMEI = dtHouseOwner.Rows[0]["IMEI"].ConvertToString();
                        ownerDtlModel.IMSI = dtHouseOwner.Rows[0]["IMSI"].ConvertToString();
                        ownerDtlModel.SIM_NUMBER = dtHouseOwner.Rows[0]["SIM_NUMBER"].ConvertToString();
                        ownerDtlModel.MOBILE_NUMBER = dtHouseOwner.Rows[0]["MOBILE_NUMBER"].ConvertToString();
                        ownerDtlModel.HOUSEOWNER_ACTIVE = dtHouseOwner.Rows[0]["HOUSEOWNER_ACTIVE"].ConvertToString();
                        ownerDtlModel.REMARKS = dtHouseOwner.Rows[0]["REMARKS"].ConvertToString();
                        ownerDtlModel.REMARKS_LOC = dtHouseOwner.Rows[0]["REMARKS_LOC"].ConvertToString();
                        ownerDtlModel.FIRST_NAME_ENG = dtHouseOwner.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                        ownerDtlModel.MIDDLE_NAME_ENG = dtHouseOwner.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                        ownerDtlModel.LAST_NAME_ENG = dtHouseOwner.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                        ownerDtlModel.FULL_NAME_ENG = dtHouseOwner.Rows[0]["FULL_NAME_ENG"].ConvertToString();
                        ownerDtlModel.FIRST_NAME_LOC = dtHouseOwner.Rows[0]["FIRST_NAME_LOC"].ConvertToString();
                        ownerDtlModel.MIDDLE_NAME_LOC = dtHouseOwner.Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                        ownerDtlModel.LAST_NAME_LOC = dtHouseOwner.Rows[0]["LAST_NAME_LOC"].ConvertToString();
                        ownerDtlModel.FULL_NAME_LOC = dtHouseOwner.Rows[0]["FULL_NAME_LOC"].ConvertToString();
                        ownerDtlModel.MEMBER_PHOTO_ID = dtHouseOwner.Rows[0]["MEMBER_PHOTO_ID"].ConvertToString();
                        ownerDtlModel.GENDER_CD = dtHouseOwner.Rows[0]["GENDER_CD"].ToDecimal();
                        ownerDtlModel.GENDER_ENG = dtHouseOwner.Rows[0]["GENDER_ENG"].ConvertToString();
                        ownerDtlModel.GENDER_LOC = dtHouseOwner.Rows[0]["GENDER_LOC"].ConvertToString();
                        ownerDtlModel.MARITAL_STATUS_ENG = dtHouseOwner.Rows[0]["MARITAL_STATUS_ENG"].ConvertToString();
                        ownerDtlModel.MARITAL_STATUS_LOC = dtHouseOwner.Rows[0]["MARITAL_STATUS_LOC"].ConvertToString();
                        ownerDtlModel.HOUSEHOLD_HEAD = dtHouseOwner.Rows[0]["HOUSEHOLD_HEAD"].ConvertToString();
                        ownerDtlModel.HOUSE_SNO = dtHouseOwner.Rows[0]["HOUSE_SNO"].ConvertToString();
                        ownerDtlModel.TARGET_LOT = dtHouseOwner.Rows[0]["TARGET_LOT"].ConvertToString();
                        ownerDtlModel.NRA_DEFINED_CD = dtHouseOwner.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                        ownerDtlModel.RETRO_PA = dtHouseOwner.Rows[0]["RETRO_PA"].ConvertToString();
                        ownerDtlModel.GRIEVANCE_PA = dtHouseOwner.Rows[0]["GRIEVANCE_PA"].ConvertToString();

                        FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
                        //FTPclient fc = new FTPclient("202.79.33.141", "mofald", "M0faLd#NepaL");
                        string ftpPath = string.Empty;
                        string[] subDate = ownerDtlModel.SubmissionDate.Split(' ');
                        if (ownerDtlModel.TARGET_LOT == "2")
                        {
                            ftpPath = @"/PhotO/" + "/ktmvalley/" + ownerDtlModel.KLLDistrictCode + "/" + subDate[0] + "/" + ownerDtlModel.DEFINED_CD + "/";
                        }
                        if (ownerDtlModel.TARGET_LOT == "1" || ownerDtlModel.TARGET_LOT == "3")
                        {
                            ftpPath = @"/PhotO/" + ownerDtlModel.KLLDistrictCode + "/" + subDate[0] + "/" + ownerDtlModel.DEFINED_CD + "/";
                        }
                        if (ownerDtlModel.TARGET_LOT == "4" || ownerDtlModel.TARGET_LOT == "5" || ownerDtlModel.TARGET_LOT == "6" || ownerDtlModel.TARGET_LOT == "7" || ownerDtlModel.TARGET_LOT == "8")
                        {
                            if (ownerDtlModel.KLLDistrictCode == "9" || ownerDtlModel.KLLDistrictCode == "7")
                            {
                                ftpPath = @"/PhotO/" + "/17districts/" + "0"+ ownerDtlModel.KLLDistrictCode + "/" + subDate[0] + "/" + ownerDtlModel.DEFINED_CD + "/";
                            }
                            else
                            {
                                ftpPath = @"/PhotO/" + "/17districts/" + ownerDtlModel.KLLDistrictCode + "/" + subDate[0] + "/" + ownerDtlModel.DEFINED_CD + "/";
                            }
                           
                        }
                    

                        // names of multiple owners 
                        DataTable dtHouseOwnerNames = objHouseBuildingService.HouseOwnerNames(id);
                        if (dtHouseOwnerNames != null && dtHouseOwnerNames.Rows.Count > 0)
                        {
                            List<HouseOwnerNameModel> ownerList = new List<HouseOwnerNameModel>();
                            foreach (DataRow dr in dtHouseOwnerNames.Rows)
                            {
                                HouseOwnerNameModel ownerName = new HouseOwnerNameModel();

                                ownerName.FULL_NAME_LOC = dr["HOUSE_OWNER_NAME"].ConvertToString();
                                ownerName.FULL_NAME_ENG = dr["HOUSE_OWNER_NAME"].ConvertToString();
                                ownerName.NRA_DEFINED_CD = dr["NRA_DEFINED_CD"].ConvertToString();
                                ownerName.SN = dr["SNO"].ConvertToString();
                                ownerList.Add(ownerName);
                            }
                            ownerDtlModel.HouseOwnerNamesList = ownerList;
                        }
                        DataTable dtGrievanceDetail = objHouseBuildingService.GrievanceHouseOwnerNames(id);
                        if (dtGrievanceDetail != null && dtGrievanceDetail.Rows.Count > 0)
                        {
                            List<GrievanceOwner> ownerList = new List<GrievanceOwner>();
                            foreach (DataRow dr in dtGrievanceDetail.Rows)
                            {
                                GrievanceOwner GrievanceOwnerName = new GrievanceOwner();

                                GrievanceOwnerName.House_owner_name = dr["HOUSE_OWNER_NAME_ENG"].ConvertToString();
                                GrievanceOwnerName.NRA_DEFINED_CD = dr["NRA_DEFINED_CD"].ConvertToString();
                                GrievanceOwnerName.GrievantName = dr["GRIEVANT_NAME"].ConvertToString();
                                GrievanceOwnerName.GID = dr["GID"].ConvertToString();
                                ownerList.Add(GrievanceOwnerName);
                            }
                            ownerDtlModel.GrievanceOWner = ownerList;
                        }
                        // names of multiple Respondent 
                        DataTable dtRespondentNames = objHouseBuildingService.HouseRespondentNames(id);
                        if (dtRespondentNames != null && dtRespondentNames.Rows.Count > 0)
                        {
                            List<VW_HOUSE_RESPONDENT_DETAIL> respondentList = new List<VW_HOUSE_RESPONDENT_DETAIL>();
                            foreach (DataRow dr in dtRespondentNames.Rows)
                            {
                                VW_HOUSE_RESPONDENT_DETAIL respondentName = new VW_HOUSE_RESPONDENT_DETAIL();
                                respondentName.FIRST_NAME_ENG = dr["RESPONDENT_FIRST_NAME"].ConvertToString();
                                respondentName.MIDDLE_NAME_ENG = dr["RESPONDENT_MIDDLE_NAME"].ConvertToString();
                                respondentName.LAST_NAME_ENG = dr["RESPONDENT_LAST_NAME"].ConvertToString();
                                respondentName.FULL_NAME_ENG = dr["RESPONDENT_FULL_NAME"].ConvertToString();
                                respondentName.FIRST_NAME_LOC = dr["RESPONDENT_FIRST_NAME_LOC"].ConvertToString();
                                respondentName.MIDDLE_NAME_LOC = dr["RESPONDENT_MIDDLE_NAME_LOC"].ConvertToString();
                                respondentName.LAST_NAME_LOC = dr["RESPONDENT_LAST_NAME_LOC"].ConvertToString();
                                respondentName.FULL_NAME_LOC = dr["RESPONDENT_FULL_NAME_LOC"].ConvertToString();
                                respondentName.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                                respondentName.GENDER_LOC = dr["GENDER_LOC"].ConvertToString();
                                respondentName.RELATION_ENG = dr["RELATION_ENG"].ConvertToString();
                                respondentName.RELATION_LOC = dr["RELATION_LOC"].ConvertToString();
                                //TPclient fc = new FTPclient("202.79.33.141", "mofald", "M0faLd#NepaL");
                                if (dr["RESPONDENT_PHOTO"].ConvertToString() != "")
                                {
                                    string Ppath = ftpPath + dr["RESPONDENT_PHOTO"].ConvertToString();
                                    bool isError = false;
                                    try
                                    {
                                        //if (Utils.CheckIfFileExistsOnServer(Ppath, "10.27.27.104", "mofald", "M0faLd#NepaL"))
                                        //{
                                        if (fc.FtpFileExists(Ppath))
                                        {
                                            fc.Download(Ppath, Server.MapPath("~/photo/" + dr["RESPONDENT_PHOTO"].ConvertToString()), true);
                                            System.Threading.Thread.Sleep(5000);
                                        }

                                    }
                                    catch (OracleException oe)
                                    {
                                        isError = true;
                                        ExceptionManager.AppendLog(oe);
                                    }
                                    catch (Exception ex)
                                    {
                                        isError = true;
                                        ExceptionManager.AppendLog(ex);
                                    }
                                    if (isError) break;
                                }

                                ownerDtlModel.ImagePath = dr["RESPONDENT_PHOTO"].ConvertToString();
                                ////}
                                respondentList.Add(respondentName);
                            }
                            ownerDtlModel.lstRespondentDetail = respondentList;
                        }
                        // other damaged houses
                        DataTable dtOtherHousesDamaged = objHouseBuildingService.OtherHousesDamaged(id);
                        if (dtOtherHousesDamaged != null && dtOtherHousesDamaged.Rows.Count > 0)
                        {
                            List<OtherHousesDamagedModel> damagedHouses = new List<OtherHousesDamagedModel>();
                            foreach (DataRow dr in dtOtherHousesDamaged.Rows)
                            {
                                OtherHousesDamagedModel otherDamagedHouse = new OtherHousesDamagedModel();
                                otherDamagedHouse.FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                                otherDamagedHouse.FULL_NAME_LOC = dr["FULL_NAME_LOC"].ConvertToString();
                                otherDamagedHouse.OTHER_DISTRICT_CD = dr["OTHER_DISTRICT_CD"].ToDecimal();
                                otherDamagedHouse.district = Utils.ToggleLanguage(GetData.GetDescEngFor(DataType.District, otherDamagedHouse.OTHER_DISTRICT_CD.ConvertToString()), GetData.GetDescLocFor(DataType.District, otherDamagedHouse.OTHER_DISTRICT_CD.ConvertToString()));

                                otherDamagedHouse.OTHER_VDC_MUN_CD = dr["OTHER_VDC_MUN_CD"].ToDecimal();
                                otherDamagedHouse.vdc_mun = Utils.ToggleLanguage(GetData.GetDescEngFor(DataType.VdcMun, otherDamagedHouse.OTHER_VDC_MUN_CD.ConvertToString()), GetData.GetDescLocFor(DataType.VdcMun, otherDamagedHouse.OTHER_VDC_MUN_CD.ConvertToString()));
                                otherDamagedHouse.OTHER_WARD_NO = dr["OTHER_WARD_NO"].ToDecimal();
                                otherDamagedHouse.BUILDING_CONDITION_CD = dr["BUILDING_CONDITION_CD"].ConvertToString();
                                otherDamagedHouse.buildingCondition = Utils.ToggleLanguage(dr["TechnicalSol_ENG"].ConvertToString(), dr["TechnicalSol_LOC"].ConvertToString());

                                //common.GetDescriptionFromCode(otherDamagedHouse.BUILDING_CONDITION_CD.ConvertToString(), "NHRS_BUILDING_CONDITION", "BUILDING_CONDITION_CD");
                                damagedHouses.Add(otherDamagedHouse);
                            }
                            ownerDtlModel.OtherHousesDamagedList = damagedHouses;
                        }


                        //for list of structures
                        dtHouseDetail = objHouseBuildingService.HouseBuildingDetail(id);
                        if (dtHouseDetail != null && dtHouseDetail.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtHouseDetail.Rows)
                            {
                                VW_HOUSE_BUILDING_DTL building = new VW_HOUSE_BUILDING_DTL();

                                building.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                                building.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                                building.HOUSE_LAND_LEGAL_OWNER = dr["HOUSE_OWNER_ID"].ToDecimal();
                                building.LEGAL_OWNER_ENG = dr["LEGAL_OWNER_ENG"].ConvertToString();
                                building.LEGAL_OWNER_LOC = dr["LEGAL_OWNER_LOC"].ConvertToString();
                                building.BUILDING_CONDITION_CD = dr["BUILDING_CONDITION_CD"].ToDecimal();
                                building.BUILDING_CONDITION_ENG = dr["BUILDING_CONDITION_ENG"].ConvertToString();
                                building.BUILDING_CONDITION_LOC = dr["BUILDING_CONDITION_LOC"].ConvertToString();
                                building.STOREYS_CNT_BEFORE = dr["STOREYS_CNT_BEFORE"].ToDecimal();
                                building.STOREYS_CNT_AFTER = dr["STOREYS_CNT_AFTER"].ToDecimal();
                                building.HOUSE_AGE = dr["HOUSE_AGE"].ToDecimal();
                                building.HOUSE_HEIGHT_BEFORE_EQ = dr["HOUSE_HEIGHT_BEFORE_EQ"].ConvertToString();
                                building.HOUSE_HEIGHT_AFTER_EQ = dr["HOUSE_HEIGHT_AFTER_EQ"].ConvertToString();
                                building.PLINTH_AREA = dr["PLINTH_AREA"].ConvertToString();
                                building.GROUND_SURFACE_CD = dr["GROUND_SURFACE_CD"].ToDecimal();
                                building.GROUND_SURFACE_ENG = dr["GROUND_SURFACE_ENG"].ConvertToString();
                                building.GROUND_SURFACE_LOC = dr["GROUND_SURFACE_LOC"].ConvertToString();
                                building.FOUNDATION_TYPE_CD = dr["FOUNDATION_TYPE_CD"].ToDecimal();
                                building.RC_MATERIAL_CD = dr["RC_MATERIAL_CD"].ToDecimal();
                                building.RC_MATERIAL_ENG = dr["RC_MATERIAL_ENG"].ConvertToString();
                                building.RC_MATERIAL_LOC = dr["RC_MATERIAL_LOC"].ConvertToString();
                                building.FC_MATERIAL_CD = dr["FC_MATERIAL_CD"].ToDecimal();
                                building.FC_MATERIAL_ENG = dr["FC_MATERIAL_ENG"].ConvertToString();
                                building.FC_MATERIAL_LOC = dr["FC_MATERIAL_LOC"].ConvertToString();
                                building.SC_MATERIAL_CD = dr["SC_MATERIAL_CD"].ToDecimal();
                                building.BUILDING_POSITION_CD = dr["BUILDING_POSITION_CD"].ToDecimal();
                                building.BUILDING_POSITION_ENG = dr["BUILDING_POSITION_ENG"].ConvertToString();
                                building.BUILDING_POSITION_LOC = dr["BUILDING_POSITION_LOC"].ConvertToString();
                                building.BUILDING_PLAN_CD = dr["BUILDING_PLAN_CD"].ToDecimal();
                                building.BUILDING_PLAN_ENG = dr["BUILDING_PLAN_ENG"].ConvertToString();
                                building.BUILDING_PLAN_LOC = dr["BUILDING_PLAN_LOC"].ConvertToString();
                                building.IS_GEOTECHNICAL_RISK = dr["IS_GEOTECHNICAL_RISK"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                                building.ASSESSED_AREA_CD = dr["ASSESSED_AREA_CD"].ToDecimal();
                                building.ASSESSED_AREA_ENG = dr["ASSESSED_AREA_ENG"].ConvertToString();
                                building.ASSESSED_AREA_LOC = dr["ASSESSED_AREA_LOC"].ConvertToString();
                                building.DAMAGE_GRADE_CD = dr["DAMAGE_GRADE_CD"].ToDecimal();
                                building.DAMAGE_GRADE_ENG = dr["DAMAGE_GRADE_ENG"].ConvertToString();
                                building.DAMAGE_GRADE_LOC = dr["DAMAGE_GRADE_LOC"].ConvertToString();
                                building.TECHSOLUTION_CD = dr["TECHSOLUTION_CD"].ToDecimal();
                                building.TECHSOLUTION_ENG = dr["TECHSOLUTION_ENG"].ConvertToString();
                                building.TECHSOLUTION_LOC = dr["TECHSOLUTION_LOC"].ConvertToString();
                                building.TECHSOLUTION_COMMENT = dr["TECHSOLUTION_COMMENT"].ConvertToString();
                                building.TECHSOLUTION_COMMENT_LOC = dr["TECHSOLUTION_COMMENT_LOC"].ConvertToString();
                                building.RECONSTRUCTION_STARTED = dr["RECONSTRUCTION_STARTED"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                                building.IS_SECONDARY_USE = dr["IS_SECONDARY_USE"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "थियो/छ") : Utils.ToggleLanguage("No", "थिएन/छैन");
                                building.LATITUDE = dr["LATITUDE"].ConvertToString();
                                building.LONGITUDE = dr["LONGITUDE"].ConvertToString();
                                building.ALTITUDE = dr["ALTITUDE"].ConvertToString();
                                building.ACCURACY = dr["ACCURACY"].ConvertToString();
                                building.HOUSEHOLD_CNT_AFTER_EQ = dr["HOUSEHOLD_CNT_AFTER_EQ"].ToDecimal();
                                building.FOUNDATION_TYPE_ENG = dr["FOUNDATION_TYPE_ENG"].ConvertToString();
                                building.FOUNDATION_TYPE_LOC = dr["FOUNDATION_TYPE_LOC"].ConvertToString();
                                building.GENERAL_COMMENTS = dr["GENERAL_COMMENTS"].ConvertToString();
                                DataTable dtphoto = new DataTable();
                                dtphoto = objHouseBuildingService.GetPhotoDetail(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);
                                foreach (DataRow drphoto in dtphoto.Rows)
                                {
                                    string docTypeid = drphoto["DOC_TYPE_CD"].ConvertToString();
                                    string photoPath = drphoto["PHOTO_PATH"].ConvertToString();
                                    if (photoPath != "")
                                    {
                                        string Ppath = ftpPath + photoPath;
                                        bool isError = false;
                                        try
                                        {
                                            //if (Utils.CheckIfFileExistsOnServer(Ppath, "10.27.27.104", "mofald", "M0faLd#NepaL"))
                                            //{
                                            if (fc.FtpFileExists(Ppath))
                                            {
                                                fc.Download(Ppath, Server.MapPath("~/photo/" + photoPath), true);
                                            }

                                        }
                                        catch (OracleException oe)
                                        {
                                            isError = true;
                                            ExceptionManager.AppendLog(oe);
                                        }
                                        catch (Exception ex)
                                        {
                                            isError = true;
                                            ExceptionManager.AppendLog(ex);
                                        }
                                        if (isError) break;
                                    }


                                    //if (docTypeid == "11")
                                    //{
                                    //    ownerDtlModel.ImagePath = photoPath;

                                    //}
                                    ////13 back
                                    //else 

                                    if (docTypeid == "12")
                                    {
                                        building.PhotosofFrontdirections = photoPath;

                                    }
                                    //13 back
                                    else if (docTypeid == "13")
                                    {
                                        building.PhotosofBackdirections = photoPath;

                                    }
                                    //14 left
                                    else if (docTypeid == "14")
                                    {
                                        building.PhotosofLeftdirections = photoPath;

                                    }
                                    //15 right
                                    else if (docTypeid == "15")
                                    {
                                        building.PhotosofRightdirections = photoPath;

                                    }
                                    //16 internal 
                                    else if (docTypeid == "16")
                                    {
                                        building.Photosofinternaldamageofhouse = photoPath;

                                    }
                                    //17 Rebel
                                    else if (docTypeid == "17")
                                    {
                                        building.Fullydamagedhouseslocationsphoto = photoPath;
                                    }
                                    //18 damage photo1
                                    else if (docTypeid == "18")
                                    {

                                    }
                                    //19 damage photo2
                                    else if (docTypeid == "19")
                                    {

                                    }

                                    // building.PhotosofHouse = "";

                                }

                                DataSet damageAssessmentOfHouse = new DataSet();

                                damageAssessmentOfHouse = objNHRSHouseDetail.GetBuildingAssessmentDetail(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);
                                if (damageAssessmentOfHouse != null && damageAssessmentOfHouse.Tables.Count > 0)
                                {
                                    DataTable dtDamageDetail = damageAssessmentOfHouse.Tables["Table1"];
                                    List<string> lstDamageChkBoxes = new List<string>();
                                    string damageLevelStr = String.Empty;
                                    foreach (DataRow drow in dtDamageDetail.Rows)
                                    {
                                        damageLevelStr = drow["DAMAGE_LEVEL_CD"].ConvertToString() == "1" ? "SE" : damageLevelStr;
                                        damageLevelStr = drow["DAMAGE_LEVEL_CD"].ConvertToString() == "2" ? "MH" : damageLevelStr;
                                        damageLevelStr = drow["DAMAGE_LEVEL_CD"].ConvertToString() == "3" ? "IN" : damageLevelStr;
                                        damageLevelStr = drow["DAMAGE_LEVEL_CD"].ConvertToString() == "10" ? "NO" : damageLevelStr;
                                        damageLevelStr = drow["DAMAGE_LEVEL_CD"].ConvertToString() == "11" ? "NA" : damageLevelStr;
                                        if (damageLevelStr != "NA" && damageLevelStr != "NO")
                                        {
                                            lstDamageChkBoxes.Add("chkDamageDet_" + drow["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr + "_" + drow["DAMAGE_LEVEL_DTL_CD"].ConvertToString());
                                        }
                                        else
                                        {
                                            lstDamageChkBoxes.Add("chkDamageDet_" + drow["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr);
                                        }
                                    }
                                    ViewData["lstDamageCheckBox" + building.BUILDING_STRUCTURE_NO] = lstDamageChkBoxes;
                                }


                                building.lstFamily = objHouseBuildingService.GetStructureFamilyDetail(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);
                                //ownerDtlModel.HOUSE_BUILDING_DTL_List.Add(building);


                                // Super Structure Checkbox tick
                                building.lstSuperStructureDetail1 = objHouseBuildingService.SuperStructureofHouse(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);
                                //ownerDtlModel.HOUSE_BUILDING_DTL_List.Add(building);


                                // Damage Extent of home
                                building.lstDamageExtentHome1 = objHouseBuildingService.DamageExtentofHouse(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);
                                //ownerDtlModel.HOUSE_BUILDING_DTL_List.Add(building);

                                // Geographical Risk Checkbox Name tick
                                building.lstGeoTechnicalDetail1 = objHouseBuildingService.GeoTechnicalDetail(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);
                                //ownerDtlModel.HOUSE_BUILDING_DTL_List.Add(building);

                                // Secondary Use Checkbox Name tick
                                building.lstsecondaryUseDetail1 = objHouseBuildingService.SecondaryUseDetail(building.HOUSE_OWNER_ID, building.BUILDING_STRUCTURE_NO);



                                ownerDtlModel.HOUSE_BUILDING_DTL_List.Add(building);

                            }
                        }

                        // Super Structure Name

                        DataTable dtSuperStructureName = objHouseBuildingService.SuperStructureofHouseName();
                        if (dtSuperStructureName != null && dtSuperStructureName.Rows.Count > 0)
                        {
                            List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> superStructureList = new List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT>();
                            foreach (DataRow dr in dtSuperStructureName.Rows)
                            {
                                MIG_NHRS_HH_SUPERSTRUCTURE_MAT superStructureName = new MIG_NHRS_HH_SUPERSTRUCTURE_MAT();
                                superStructureName.superstructureMatId = Convert.ToDecimal(dr["SUPERSTRUCTURE_MATERIAL_CD"]);
                                superStructureName.superstructureMatEng = dr["DESC_ENG"].ConvertToString();
                                superStructureName.superstructureMatLoc = dr["DESC_LOC"].ConvertToString();

                                superStructureList.Add(superStructureName);
                            }
                            ownerDtlModel.lstSuperStructureName = superStructureList;
                        }
                        //Damage Extent of Home Name
                        DataTable dtDamageExtentName = objHouseBuildingService.DamageExtentofHouseName();
                        if (dtDamageExtentName != null && dtDamageExtentName.Rows.Count > 0)
                        {
                            List<VW_DamageExtentHome> DamageExtentHomeNameList = new List<VW_DamageExtentHome>();
                            foreach (DataRow dr in dtDamageExtentName.Rows)
                            {
                                VW_DamageExtentHome damageExtentHome = new VW_DamageExtentHome();

                                damageExtentHome.DAMAGE_CD = dr["DAMAGE_CD"].ToDecimal();
                                damageExtentHome.DAMAGE_ENG = dr["DESC_ENG"].ConvertToString();
                                damageExtentHome.DAMAGE_LOC = dr["DESC_LOC"].ConvertToString();
                                damageExtentHome.UPPER_DAMAGE_CD = dr["UPPER_DAMAGE_CD"].ToDecimal();
                                damageExtentHome.GROUP_FLAG = dr["GROUP_FLAG"].ConvertToString();
                                damageExtentHome.GROUP_FLAG_VALUE = dr["GROUP_FLAG_VALUE"].ConvertToString();


                                DamageExtentHomeNameList.Add(damageExtentHome);
                            }
                            ownerDtlModel.lstDamageExtentHomeName = DamageExtentHomeNameList;
                        }
                        //Damage Level
                        DataTable dtDamageLevel = objHouseBuildingService.DamageLevelName();
                        if (dtDamageLevel != null && dtDamageLevel.Rows.Count > 0)
                        {
                            List<VW_DamageExtentHome> DamageLevelList = new List<VW_DamageExtentHome>();
                            foreach (DataRow dr1 in dtDamageLevel.Rows)
                            {
                                VW_DamageExtentHome damageLevel = new VW_DamageExtentHome();

                                damageLevel.DAMAGE_LEVEL_CD = dr1["DAMAGE_LEVEL_CD"].ToDecimal();
                                damageLevel.DAMAGE_LEVEL_ENG = dr1["DESC_ENG"].ConvertToString();
                                damageLevel.DAMAGE_LEVEL_LOC = dr1["DESC_LOC"].ConvertToString();


                                DamageLevelList.Add(damageLevel);
                            }
                            ownerDtlModel.lstDamageLevelName = DamageLevelList;
                        }
                        // Geographical Risk Checkbox Name
                        DataTable dtGeographicalRiskName = objHouseBuildingService.GeoTechnicalName();
                        if (dtGeographicalRiskName != null && dtGeographicalRiskName.Rows.Count > 0)
                        {
                            List<NHRS_GEOTECHNICAL_RISK_TYPE> geographicalRiskNameList = new List<NHRS_GEOTECHNICAL_RISK_TYPE>();
                            foreach (DataRow dr in dtGeographicalRiskName.Rows)
                            {
                                NHRS_GEOTECHNICAL_RISK_TYPE geotechnicalName = new NHRS_GEOTECHNICAL_RISK_TYPE();
                                geotechnicalName.GEOTECHNICAL_RISK_TYPE_CD = Convert.ToDecimal(dr["GEOTECHNICAL_RISK_TYPE_CD"]);
                                geotechnicalName.GEOTECHNICAL_RISK_ENG = dr["DESC_ENG"].ConvertToString();
                                geotechnicalName.GEOTECHNICAL_RISK_LOC = dr["DESC_LOC"].ConvertToString();

                                geographicalRiskNameList.Add(geotechnicalName);
                            }
                            ownerDtlModel.lstGeoTechnicalName = geographicalRiskNameList;
                        }
                        // Secondary Use Checkbox Name
                        DataTable dtSecondaryUseName = objHouseBuildingService.SecondaryUseName();
                        if (dtSecondaryUseName != null && dtSecondaryUseName.Rows.Count > 0)
                        {
                            List<NHRS_SECONDARY_OCCUPANCY> secondaryUseNameList = new List<NHRS_SECONDARY_OCCUPANCY>();
                            foreach (DataRow dr in dtSecondaryUseName.Rows)
                            {
                                NHRS_SECONDARY_OCCUPANCY secUseName = new NHRS_SECONDARY_OCCUPANCY();
                                secUseName.SEC_OCCUPANCY_CD = Convert.ToDecimal(dr["SEC_OCCUPANCY_CD"]);
                                secUseName.SECONDARY_OCCUPANCY_ENG = dr["DESC_ENG"].ConvertToString();
                                secUseName.SECONDARY_OCCUPANCY_LOC = dr["DESC_LOC"].ConvertToString();

                                secondaryUseNameList.Add(secUseName);
                            }
                            ownerDtlModel.lstsecondaryUseName = secondaryUseNameList;
                        }


                    }
                    DataTable DamageLeveldata = new DataTable();
                    DamageLeveldata = objHouseBuildingService.getDamageLavelDetail();
                    ViewData["dtDamageLevel"] = DamageLeveldata;
                }


            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            ViewBag.HouseOwner = ownerDtlModel.FIRST_NAME_ENG.ToUpper();
            return View(ownerDtlModel);
        }

        public ActionResult HouseSocioEcoView(string p)
        {
            DataSet dtHouseHoldMember = new DataSet();
            dtHouseHoldMember = null;
            VW_HOUSEHOLD_MEMBER houseHoldMemberDtlModel = new VW_HOUSEHOLD_MEMBER();
            string id = string.Empty;
            string houseOwnerid = string.Empty;
            string strNum = string.Empty;
            try
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        id = (rvd["id"]).ConvertToString();
                        if (string.IsNullOrEmpty(id))
                        {
                            if (!string.IsNullOrEmpty(Session["SocioHouseholdID"].ToString()))
                            {
                                id = Session["SocioHouseholdID"].ToString();
                            }
                        }
                        Session["SocioHouseholdID"] = id;

                        houseOwnerid = (rvd["id2"]).ConvertToString();
                        if (string.IsNullOrEmpty(houseOwnerid))
                        {
                            if (!string.IsNullOrEmpty(Session["SociohouseOwnerID"].ToString()))
                            {
                                houseOwnerid = Session["SociohouseOwnerID"].ToString();
                            }
                        }
                        Session["SociohouseOwnerID"] = houseOwnerid;

                        strNum = (rvd["id3"]).ConvertToString();
                        if (string.IsNullOrEmpty(strNum))
                        {
                            if (!string.IsNullOrEmpty(Session["SociostructureNO"].ToString()))
                            {
                                strNum = Session["SociostructureNO"].ToString();
                            }
                        }
                        Session["SociostructureNO"] = strNum;
                    }

                }
                HouseHoldMemberViewService objHouseHoldMemberService = new HouseHoldMemberViewService();
                dtHouseHoldMember = objHouseHoldMemberService.HouseHoldMemberDetail(id, houseOwnerid, strNum);
                if (dtHouseHoldMember != null && dtHouseHoldMember.Tables[0].Rows.Count > 0)
                {
                    houseHoldMemberDtlModel.HOUSEHOLD_ID = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_ID"].ConvertToString();
                    //houseHoldMemberDtlModel.HOUSEHOLD_DEFINED_CD = dtHouseHoldMember.Tables[0].Rows[0]["H_DEFINED_CD"].ConvertToString();
                    houseHoldMemberDtlModel.MEMBER_ID = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_ID"].ConvertToString();
                    houseHoldMemberDtlModel.MEMBER_DEFINED_CD = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_DEFINED_CD"].ConvertToString();
                    houseHoldMemberDtlModel.FIRST_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.MIDDLE_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.LAST_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.FULL_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["FULL_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.FIRST_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["FIRST_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.MIDDLE_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.LAST_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["LAST_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.FULL_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["FULL_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.KLLDistrictCD = dtHouseHoldMember.Tables[0].Rows[0]["KLL_DISTRICT_CD"].ConvertToString();
                    houseHoldMemberDtlModel.SubmissionDate = dtHouseHoldMember.Tables[0].Rows[0]["SUBMISSIONTIME"].ConvertToString();
                    houseHoldMemberDtlModel.RESPONDENT_FULL_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["RESPONDENT_FULL_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.RESPONDENT_FULL_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["RESPONDENT_FULL_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.RESPONDENT_RELATION_WITH_HH = dtHouseHoldMember.Tables[0].Rows[0]["RESPONDENT_RELATION_WITH_HH"].ConvertToString();
                    houseHoldMemberDtlModel.RELATION_WITH_HH_ENG = dtHouseHoldMember.Tables[0].Rows[0]["RELATION_WITH_HH_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.RELATION_WITH_HH_LOC = dtHouseHoldMember.Tables[0].Rows[0]["RELATION_WITH_HH_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.RESPONDENT_IS_HH_HEAD = dtHouseHoldMember.Tables[0].Rows[0]["RESPONDENT_IS_HH_HEAD"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.PER_COUNTRY_CD = dtHouseHoldMember.Tables[0].Rows[0]["PER_COUNTRY_CD"].ToDecimal();
                    houseHoldMemberDtlModel.PER_REG_ST_CD = dtHouseHoldMember.Tables[0].Rows[0]["PER_REG_ST_CD"].ToDecimal();
                    houseHoldMemberDtlModel.PER_ZONE_CD = dtHouseHoldMember.Tables[0].Rows[0]["PER_ZONE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.PER_DISTRICT_CD = dtHouseHoldMember.Tables[0].Rows[0]["PER_DISTRICT_CD"].ToDecimal();
                    houseHoldMemberDtlModel.PER_VDC_MUN_CD = dtHouseHoldMember.Tables[0].Rows[0]["PER_VDC_MUN_CD"].ToDecimal();
                    houseHoldMemberDtlModel.PER_WARD_NO = dtHouseHoldMember.Tables[0].Rows[0]["PER_WARD_NO"].ToDecimal();
                    houseHoldMemberDtlModel.PER_AREA_ENG = dtHouseHoldMember.Tables[0].Rows[0]["PER_AREA_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.PER_AREA_LOC = dtHouseHoldMember.Tables[0].Rows[0]["PER_AREA_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.PER_DISTRICT_ENG = dtHouseHoldMember.Tables[0].Rows[0]["PER_DISTRICT_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.PER_DISTRICT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["PER_DISTRICT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.PER_COUNTRY_ENG = dtHouseHoldMember.Tables[0].Rows[0]["PER_COUNTRY_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.PER_COUNTRY_LOC = dtHouseHoldMember.Tables[0].Rows[0]["PER_COUNTRY_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.PER_REGION_ENG = dtHouseHoldMember.Tables[0].Rows[0]["PER_REGION_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.PER_REGION_LOC = dtHouseHoldMember.Tables[0].Rows[0]["PER_REGION_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.PER_ZONE_ENG = dtHouseHoldMember.Tables[0].Rows[0]["PER_ZONE_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.PER_ZONE_LOC = dtHouseHoldMember.Tables[0].Rows[0]["PER_ZONE_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.PER_VDC_ENG = dtHouseHoldMember.Tables[0].Rows[0]["PER_VDC_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.PER_VDC_LOC = dtHouseHoldMember.Tables[0].Rows[0]["PER_VDC_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.CUR_COUNTRY_CD = dtHouseHoldMember.Tables[0].Rows[0]["CUR_COUNTRY_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CUR_REG_ST_CD = dtHouseHoldMember.Tables[0].Rows[0]["CUR_REG_ST_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CUR_ZONE_CD = dtHouseHoldMember.Tables[0].Rows[0]["CUR_ZONE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CUR_DISTRICT_CD = dtHouseHoldMember.Tables[0].Rows[0]["CUR_DISTRICT_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CUR_VDC_MUN_CD = dtHouseHoldMember.Tables[0].Rows[0]["CUR_VDC_MUN_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CUR_WARD_NO = dtHouseHoldMember.Tables[0].Rows[0]["CUR_WARD_NO"].ToDecimal();
                    houseHoldMemberDtlModel.CUR_AREA_ENG = dtHouseHoldMember.Tables[0].Rows[0]["CUR_AREA_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.CUR_AREA_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CUR_AREA_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.CUR_DISTRICT_ENG = dtHouseHoldMember.Tables[0].Rows[0]["CUR_DISTRICT_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.CUR_DISTRICT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CUR_DISTRICT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_LOC = dtHouseHoldMember.Tables[0].Rows[0]["DESC_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_ENG = dtHouseHoldMember.Tables[0].Rows[0]["DESC_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_LOC1 = dtHouseHoldMember.Tables[0].Rows[0]["DESC_LOC1"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_ENG1 = dtHouseHoldMember.Tables[0].Rows[0]["DESC_ENG1"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_LOC2 = dtHouseHoldMember.Tables[0].Rows[0]["DESC_LOC2"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_ENG2 = dtHouseHoldMember.Tables[0].Rows[0]["DESC_ENG2"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_LOC3 = dtHouseHoldMember.Tables[0].Rows[0]["DESC_LOC3"].ConvertToString();
                    houseHoldMemberDtlModel.DESC_ENG3 = dtHouseHoldMember.Tables[0].Rows[0]["DESC_ENG3"].ConvertToString();
                    houseHoldMemberDtlModel.MEMBER_PHOTO_ID = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_PHOTO_ID"].ConvertToString();
                    houseHoldMemberDtlModel.GENDER_CD = dtHouseHoldMember.Tables[0].Rows[0]["GENDER_CD"].ToDecimal();
                    houseHoldMemberDtlModel.GENDER_ENG = dtHouseHoldMember.Tables[0].Rows[0]["GENDER_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.GENDER_LOC = dtHouseHoldMember.Tables[0].Rows[0]["GENDER_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.MARITAL_STATUS_CD = dtHouseHoldMember.Tables[0].Rows[0]["MARITAL_STATUS_CD"].ToDecimal();
                    houseHoldMemberDtlModel.BIRTH_YEAR = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_YEAR"].ToDecimal();
                    houseHoldMemberDtlModel.BIRTH_MONTH = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_MONTH"].ToDecimal();
                    houseHoldMemberDtlModel.BIRTH_DAY = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_DAY"].ToDecimal();
                    houseHoldMemberDtlModel.BIRTH_YEAR_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_YEAR_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_MONTH_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_MONTH_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_DAY_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_DAY_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_DT = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_DT"].ToDateTime();
                    houseHoldMemberDtlModel.BIRTH_DT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_DT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.AGE = dtHouseHoldMember.Tables[0].Rows[0]["AGE"].ConvertToString();
                    houseHoldMemberDtlModel.CASTE_CD = dtHouseHoldMember.Tables[0].Rows[0]["CASTE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CASTE_ENG = dtHouseHoldMember.Tables[0].Rows[0]["CASTE_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.CASTE_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CASTE_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.RELIGION_CD = dtHouseHoldMember.Tables[0].Rows[0]["RELIGION_CD"].ToDecimal();
                    houseHoldMemberDtlModel.RELIGION_ENG = dtHouseHoldMember.Tables[0].Rows[0]["RELIGION_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.RELIGION_LOC = dtHouseHoldMember.Tables[0].Rows[0]["RELIGION_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.LITERATE = dtHouseHoldMember.Tables[0].Rows[0]["LITERATE"].ConvertToString();
                    houseHoldMemberDtlModel.EDUCATION_CD = dtHouseHoldMember.Tables[0].Rows[0]["EDUCATION_CD"].ToDecimal();
                    houseHoldMemberDtlModel.EDUCATION_ENG = dtHouseHoldMember.Tables[0].Rows[0]["EDUCATION_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.EDUCATION_LOC = dtHouseHoldMember.Tables[0].Rows[0]["EDUCATION_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_CERTIFICATE = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_CERTIFICATE"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_CERTIFICATE_NO = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_CERTIFICATE_NO"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_CER_DISTRICT_CD = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_CER_DISTRICT_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CTZ_ISSUE = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE"].ConvertToString();
                    houseHoldMemberDtlModel.CTZ_NO = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_NO"].ConvertToString();
                    houseHoldMemberDtlModel.CTZ_ISSUE_DISTRICT_CD = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_DISTRICT_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CTZ_ISSUE_YEAR = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_YEAR"].ToDecimal();
                    houseHoldMemberDtlModel.CTZ_ISSUE_MONTH = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_MONTH"].ToDecimal();
                    houseHoldMemberDtlModel.CTZ_ISSUE_DAY = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_DAY"].ToDecimal();
                    houseHoldMemberDtlModel.CTZ_ISSUE_YEAR_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_YEAR_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.CTZ_ISSUE_MONTH_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_MONTH_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.CTZ_ISSUE_DAY_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_DAY_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.CTZ_ISSUE_DT = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_DT"].ToDateTime();
                    houseHoldMemberDtlModel.CTZ_ISSUE_DT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISSUE_DT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.VOTER_ID = dtHouseHoldMember.Tables[0].Rows[0]["VOTER_ID"].ConvertToString();
                    houseHoldMemberDtlModel.VOTERID_NO = dtHouseHoldMember.Tables[0].Rows[0]["VOTERID_NO"].ConvertToString();
                    houseHoldMemberDtlModel.VOTERID_DISTRICT_CD = dtHouseHoldMember.Tables[0].Rows[0]["VOTERID_DISTRICT_CD"].ToDecimal();
                    houseHoldMemberDtlModel.VOTERID_ISSUE_DT = dtHouseHoldMember.Tables[0].Rows[0]["VOTERID_ISSUE_DT"].ToDateTime();
                    houseHoldMemberDtlModel.VOTERID_ISSUE_DT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["VOTERID_ISSUE_DT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.NID_NO = dtHouseHoldMember.Tables[0].Rows[0]["NID_NO"].ConvertToString();
                    houseHoldMemberDtlModel.NID_DISTRICT_CD = dtHouseHoldMember.Tables[0].Rows[0]["NID_DISTRICT_CD"].ToDecimal();
                    houseHoldMemberDtlModel.NID_ISSUE_DT = dtHouseHoldMember.Tables[0].Rows[0]["NID_ISSUE_DT"].ToDateTime();
                    houseHoldMemberDtlModel.NID_ISSUE_DT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["NID_ISSUE_DT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.DISABILITY = dtHouseHoldMember.Tables[0].Rows[0]["DISABILITY"].ConvertToString();
                    houseHoldMemberDtlModel.TEL_NO = dtHouseHoldMember.Tables[0].Rows[0]["TEL_NO"].ConvertToString();
                    houseHoldMemberDtlModel.MOBILE_NO = dtHouseHoldMember.Tables[0].Rows[0]["MOBILE_NO"].ConvertToString();
                    houseHoldMemberDtlModel.FAX = dtHouseHoldMember.Tables[0].Rows[0]["FAX"].ConvertToString();
                    houseHoldMemberDtlModel.EMAIL = dtHouseHoldMember.Tables[0].Rows[0]["EMAIL"].ConvertToString();
                    houseHoldMemberDtlModel.URL = dtHouseHoldMember.Tables[0].Rows[0]["URL"].ConvertToString();
                    houseHoldMemberDtlModel.PO_BOX_NO = dtHouseHoldMember.Tables[0].Rows[0]["PO_BOX_NO"].ConvertToString();
                    houseHoldMemberDtlModel.PASSPORT_NO = dtHouseHoldMember.Tables[0].Rows[0]["PASSPORT_NO"].ConvertToString();
                    houseHoldMemberDtlModel.PASSPORT_ISSUE_DISTRICT = dtHouseHoldMember.Tables[0].Rows[0]["PASSPORT_ISSUE_DISTRICT"].ToDecimal();
                    houseHoldMemberDtlModel.PRO_FUND_NO = dtHouseHoldMember.Tables[0].Rows[0]["PRO_FUND_NO"].ConvertToString();
                    houseHoldMemberDtlModel.CIT_NO = dtHouseHoldMember.Tables[0].Rows[0]["CIT_NO"].ConvertToString();
                    houseHoldMemberDtlModel.PAN_NO = dtHouseHoldMember.Tables[0].Rows[0]["PAN_NO"].ConvertToString();
                    houseHoldMemberDtlModel.DRIVING_LICENSE_NO = dtHouseHoldMember.Tables[0].Rows[0]["DRIVING_LICENSE_NO"].ConvertToString();
                    houseHoldMemberDtlModel.DEATH = dtHouseHoldMember.Tables[0].Rows[0]["DEATH"].ConvertToString();
                    houseHoldMemberDtlModel.MEMBER_REMARKS = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_REMARKS"].ConvertToString();
                    houseHoldMemberDtlModel.MEMBER_REMARKS_LOC = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_REMARKS_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_HEAD = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_HEAD"].ConvertToString();
                    houseHoldMemberDtlModel.NID_ISSUE = dtHouseHoldMember.Tables[0].Rows[0]["NID_ISSUE"].ConvertToString();
                    houseHoldMemberDtlModel.MEMBER_CNT = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.HOUSE_NO = dtHouseHoldMember.Tables[0].Rows[0]["HOUSE_NO"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_TEL_NO = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_TEL_NO"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_MOBILE_NO = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_MOBILE_NO"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_FAX = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_FAX"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_EMAIL = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_EMAIL"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_URL = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_URL"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_PO_BOX_NO = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_PO_BOX_NO"].ConvertToString();
                    houseHoldMemberDtlModel.SOCIAL_ALLOWANCE = dtHouseHoldMember.Tables[0].Rows[0]["SOCIAL_ALLOWANCE"].ConvertToString();
                    houseHoldMemberDtlModel.ALLOWANCE_TYPE_CD = dtHouseHoldMember.Tables[0].Rows[0]["ALLOWANCE_TYPE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.ALLOWANCE_TYPE_ENG = dtHouseHoldMember.Tables[0].Rows[0]["ALLOWANCE_TYPE_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.ALLOWANCE_TYPE_LOC = dtHouseHoldMember.Tables[0].Rows[0]["ALLOWANCE_TYPE_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.SOCIAL_ALL_ISS_DISTRICT = dtHouseHoldMember.Tables[0].Rows[0]["SOCIAL_ALL_ISS_DISTRICT"].ToDecimal();
                    houseHoldMemberDtlModel.SOCIAL_ALL_DISTRICT_ENG = dtHouseHoldMember.Tables[0].Rows[0]["SOCIAL_ALL_DISTRICT_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.SOCIAL_ALL_DISTRICT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["SOCIAL_ALL_DISTRICT_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.IDENTIFICATION_TYPE_CD = dtHouseHoldMember.Tables[0].Rows[0]["IDENTIFICATION_TYPE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.IDENTIFICATION_TYPE_ENG = dtHouseHoldMember.Tables[0].Rows[0]["IDENTIFICATION_TYPE_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.IDENTIFICATION_TYPE_LOC = dtHouseHoldMember.Tables[0].Rows[0]["IDENTIFICATION_TYPE_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.IDENTIFICATION_OTHERS = dtHouseHoldMember.Tables[0].Rows[0]["IDENTIFICATION_OTHERS"].ConvertToString();
                    houseHoldMemberDtlModel.IDENTIFICATION_OTHERS_LOC = dtHouseHoldMember.Tables[0].Rows[0]["IDENTIFICATION_OTHERS_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.FOREIGN_HOUSEHEAD_COUNTRY_ENG = dtHouseHoldMember.Tables[0].Rows[0]["FOREIGN_HOUSEHEAD_COUNTRY_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.FOREIGN_HOUSEHEAD_COUNTRY_LOC = dtHouseHoldMember.Tables[0].Rows[0]["FOREIGN_HOUSEHEAD_COUNTRY_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.BANK_ACCOUNT_FLAG = dtHouseHoldMember.Tables[0].Rows[0]["BANK_ACCOUNT_FLAG"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.BANK_CD = dtHouseHoldMember.Tables[0].Rows[0]["BANK_CD"].ToDecimal();
                    houseHoldMemberDtlModel.BANK_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["BANK_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.BANK_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BANK_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.BANK_BRANCH_CD = dtHouseHoldMember.Tables[0].Rows[0]["BANK_BRANCH_CD"].ToDecimal();
                    houseHoldMemberDtlModel.BANK_BRANCH_NAME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["BANK_BRANCH_NAME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.BANK_BRANCH_NAME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BANK_BRANCH_NAME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.HH_MEMBER_BANK_ACC_NO = dtHouseHoldMember.Tables[0].Rows[0]["HH_MEMBER_BANK_ACC_NO"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_REMARKS = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_REMARKS"].ConvertToString();
                    houseHoldMemberDtlModel.HOUSEHOLD_REMARKS_LOC = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_REMARKS_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.SHELTER_SINCE_QUAKE_CD = dtHouseHoldMember.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.SHELTER_SINCE_QUAKE_ENG = dtHouseHoldMember.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.SHELTER_SINCE_QUAKE_LOC = dtHouseHoldMember.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.SHELTER_BEFORE_QUAKE_CD = dtHouseHoldMember.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_CD"].ToDecimal();
                    houseHoldMemberDtlModel.SHELTER_BEFORE_QUAKE_ENG = dtHouseHoldMember.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.SHELTER_BEFORE_QUAKE_LOC = dtHouseHoldMember.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.CURRENT_SHELTER_CD = dtHouseHoldMember.Tables[0].Rows[0]["CURRENT_SHELTER_CD"].ToDecimal();
                    houseHoldMemberDtlModel.CURRENT_SHELTER_ENG = dtHouseHoldMember.Tables[0].Rows[0]["CURRENT_SHELTER_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.CURRENT_SHELTER_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CURRENT_SHELTER_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.EQ_VICTIM_IDENTITY_CARD = dtHouseHoldMember.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.EQ_VICTIM_IDENTITY_CARD_CD = dtHouseHoldMember.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_CD"].ToDecimal();
                    houseHoldMemberDtlModel.EQ_VICTIM_IDENTITY_CARD_ENG = dtHouseHoldMember.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.EQ_VICTIM_IDENTITY_CARD_LOC = dtHouseHoldMember.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.EQ_VICTIM_IDENTITY_CARD_NO = dtHouseHoldMember.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_NO"].ConvertToString();
                    houseHoldMemberDtlModel.MONTHLY_INCOME_CD = dtHouseHoldMember.Tables[0].Rows[0]["MONTHLY_INCOME_CD"].ToDecimal();
                    houseHoldMemberDtlModel.MONTHLY_INCOME_ENG = dtHouseHoldMember.Tables[0].Rows[0]["MONTHLY_INCOME_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.MONTHLY_INCOME_LOC = dtHouseHoldMember.Tables[0].Rows[0]["MONTHLY_INCOME_LOC"].ConvertToString();
                    houseHoldMemberDtlModel.DEATH_IN_A_YEAR = dtHouseHoldMember.Tables[0].Rows[0]["DEATH_IN_A_YEAR"].ConvertToString();
                    houseHoldMemberDtlModel.DEATH_CNT = dtHouseHoldMember.Tables[0].Rows[0]["DEATH_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.HUMAN_DESTROY_FLAG = dtHouseHoldMember.Tables[0].Rows[0]["HUMAN_DESTROY_FLAG"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.HUMAN_DESTROY_CNT = dtHouseHoldMember.Tables[0].Rows[0]["HUMAN_DESTROY_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.STUDENT_SCHOOL_LEFT = dtHouseHoldMember.Tables[0].Rows[0]["STUDENT_SCHOOL_LEFT"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.STUDENT_SCHOOL_LEFT_CNT = dtHouseHoldMember.Tables[0].Rows[0]["STUDENT_SCHOOL_LEFT_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.PREGNANT_REGULAR_CHECKUP = dtHouseHoldMember.Tables[0].Rows[0]["PREGNANT_REGULAR_CHECKUP"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.PREGNANT_REGULAR_CHECKUP_CNT = dtHouseHoldMember.Tables[0].Rows[0]["PREGNANT_REGULAR_CHECKUP_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.CHILD_LEFT_VACINATION = dtHouseHoldMember.Tables[0].Rows[0]["CHILD_LEFT_VACINATION"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.CHILD_LEFT_VACINATION_CNT = dtHouseHoldMember.Tables[0].Rows[0]["CHILD_LEFT_VACINATION_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.LEFT_CHANGE_OCCUPANY = dtHouseHoldMember.Tables[0].Rows[0]["LEFT_CHANGE_OCCUPANY"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                    houseHoldMemberDtlModel.LEFT_CHANGE_OCCUPANY_CNT = dtHouseHoldMember.Tables[0].Rows[0]["LEFT_CHANGE_OCCUPANY_CNT"].ToDecimal();
                    houseHoldMemberDtlModel.HOUSEHOLD_ACTIVE = dtHouseHoldMember.Tables[0].Rows[0]["HOUSEHOLD_ACTIVE"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");

                    houseHoldMemberDtlModel.CTZ_ISS_DISTRICT_ENG = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISS_DISTRICT_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.CTZ_ISS_DISTRICT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["CTZ_ISS_DISTRICT_LOC"].ConvertToString();

                    houseHoldMemberDtlModel.BIRTH_CER_DISTRICT_ENG = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_CER_DISTRICT_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.BIRTH_CER_DISTRICT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_CER_DISTRICT_LOC"].ConvertToString();

                    houseHoldMemberDtlModel.DRIVING_LIC_DISTRICT_ENG = dtHouseHoldMember.Tables[0].Rows[0]["DRIVING_LIC_DISTRICT_ENG"].ConvertToString();
                    houseHoldMemberDtlModel.DRIVING_LIC_DISTRICT_LOC = dtHouseHoldMember.Tables[0].Rows[0]["DRIVING_LIC_DISTRICT_LOC"].ConvertToString();
                  //FTPclient fc = new FTPclient("202.79.33.141", "mofald", "M0faLd#NepaL");
                    //FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
                    string[] subDate = houseHoldMemberDtlModel.SubmissionDate.Split(' ');
                    string ftpPath = @"/PhotO/" + houseHoldMemberDtlModel.KLLDistrictCD + "/" + subDate[0] + "/" + houseHoldMemberDtlModel.HOUSEHOLD_DEFINED_CD + "/";

                    string Ppath = ftpPath + dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_PHOTO_ID"].ConvertToString();
                    ////if (Utils.CheckIfFileExistsOnServer(Ppath, "10.27.27.104", "mofald", "M0faLd#NepaL"))
                    ////{
                    // fc.Download(Ppath, Server.MapPath("~/photo/" + dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_PHOTO_ID"].ConvertToString()), true);
                    //houseHoldMemberDtlModel.ImagePath = dtHouseHoldMember.Tables[0].Rows[0]["MEMBER_PHOTO_ID"].ConvertToString();
                    ////}
                    //respondentList.Add(respondentName);


                    if (dtHouseHoldMember.Tables[6].Rows.Count > 0)
                    {
                        houseHoldMemberDtlModel.WATER_SOURCE_ENG = dtHouseHoldMember.Tables[6].Rows[0]["WATER_SOURCE_ENG"].ConvertToString();
                        houseHoldMemberDtlModel.WATER_SOURCE_LOC = dtHouseHoldMember.Tables[6].Rows[0]["WATER_SOURCE_LOC"].ConvertToString();
                        houseHoldMemberDtlModel.WATER_SOURCE_BEFORE = dtHouseHoldMember.Tables[6].Rows[0]["WATER_SOURCE_BEFORE"].ConvertToString();
                        houseHoldMemberDtlModel.WATER_SOURCE_AFTER = dtHouseHoldMember.Tables[6].Rows[0]["WATER_SOURCE_AFTER"].ConvertToString();
                    }
                    if (dtHouseHoldMember.Tables[7].Rows.Count > 0)
                    {
                        houseHoldMemberDtlModel.FUEL_SOURCE_ENG = dtHouseHoldMember.Tables[7].Rows[0]["FUEL_SOURCE_ENG"].ConvertToString();
                        houseHoldMemberDtlModel.FUEL_SOURCE_LOC = dtHouseHoldMember.Tables[7].Rows[0]["FUEL_SOURCE_LOC"].ConvertToString();
                        houseHoldMemberDtlModel.FUEL_SOURCE_BEFORE = dtHouseHoldMember.Tables[7].Rows[0]["FUEL_SOURCE_BEFORE"].ConvertToString();
                        houseHoldMemberDtlModel.FUEL_SOURCE_AFTER = dtHouseHoldMember.Tables[7].Rows[0]["FUEL_SOURCE_AFTER"].ConvertToString();
                    }
                    if (dtHouseHoldMember.Tables[8].Rows.Count > 0)
                    {
                        houseHoldMemberDtlModel.LIGHT_SOURCE_ENG = dtHouseHoldMember.Tables[8].Rows[0]["LIGHT_SOURCE_ENG"].ConvertToString();
                        houseHoldMemberDtlModel.LIGHT_SOURCE_LOC = dtHouseHoldMember.Tables[8].Rows[0]["LIGHT_SOURCE_LOC"].ConvertToString();
                        houseHoldMemberDtlModel.LIGHT_SOURCE_BEFORE = dtHouseHoldMember.Tables[8].Rows[0]["LIGHT_SOURCE_BEFORE"].ConvertToString();
                        houseHoldMemberDtlModel.LIGHT_SOURCE_AFTER = dtHouseHoldMember.Tables[8].Rows[0]["LIGHT_SOURCE_AFTER"].ConvertToString();
                    }
                    if (dtHouseHoldMember.Tables[9].Rows.Count > 0)
                    {
                        houseHoldMemberDtlModel.TOILET_TYPE_ENG = dtHouseHoldMember.Tables[9].Rows[0]["TOILET_TYPE_ENG"].ConvertToString();
                        houseHoldMemberDtlModel.TOILET_TYPE_LOC = dtHouseHoldMember.Tables[9].Rows[0]["TOILET_TYPE_LOC"].ConvertToString();
                        houseHoldMemberDtlModel.TOILET_TYPE_BEFORE = dtHouseHoldMember.Tables[9].Rows[0]["TOILET_TYPE_BEFORE"].ConvertToString();
                        houseHoldMemberDtlModel.TOILET_TYPE_AFTER = dtHouseHoldMember.Tables[9].Rows[0]["TOILET_TYPE_AFTER"].ConvertToString();
                    }



                    //2. Household Members' Detail:

                    if (dtHouseHoldMember != null && dtHouseHoldMember.Tables[1].Rows.Count > 0)
                    {
                        List<VW_HOUSEHOLD_MEMBER> memlist = new List<VW_HOUSEHOLD_MEMBER>();
                        foreach (DataRow dr in dtHouseHoldMember.Tables[1].Rows)
                        {
                            VW_HOUSEHOLD_MEMBER member = new VW_HOUSEHOLD_MEMBER();
                            member.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                            member.HOUSEHOLD_DEFINED_CD = dr["HOUSEHOLD_DEFINED_CD"].ConvertToString();
                            member.MEMBER_ID = dr["MEMBER_ID"].ConvertToString();
                            member.MEMBER_DEFINED_CD = dr["MEMBER_DEFINED_CD"].ConvertToString();
                            member.FIRST_NAME_ENG = dr["FIRST_NAME_ENG"].ConvertToString();
                            member.MIDDLE_NAME_ENG = dr["MIDDLE_NAME_ENG"].ConvertToString();
                            member.LAST_NAME_ENG = dr["LAST_NAME_ENG"].ConvertToString();
                            member.FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                            member.FIRST_NAME_LOC = dr["FIRST_NAME_LOC"].ConvertToString();
                            member.MIDDLE_NAME_LOC = dr["MIDDLE_NAME_LOC"].ConvertToString();
                            member.LAST_NAME_LOC = dr["LAST_NAME_LOC"].ConvertToString();
                            member.FULL_NAME_LOC = dr["FULL_NAME_LOC"].ConvertToString();
                            member.RESPONDENT_FULL_NAME_ENG = dr["RESPONDENT_FULL_NAME_ENG"].ConvertToString();
                            member.RESPONDENT_FULL_NAME_LOC = dr["RESPONDENT_FULL_NAME_LOC"].ConvertToString();
                            member.RESPONDENT_RELATION_WITH_HH = dr["RESPONDENT_RELATION_WITH_HH"].ConvertToString();
                            member.RELATION_WITH_HH_ENG = dr["RELATION_ENG"].ConvertToString();
                            member.RELATION_WITH_HH_LOC = dr["RELATION_LOC"].ConvertToString();
                            //member.RESPONDENT_IS_HH_HEAD = dr["RESPONDENT_IS_HH_HEAD"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.PER_COUNTRY_CD = dr["PER_COUNTRY_CD"].ToDecimal();
                            member.PER_REG_ST_CD = dr["PER_REG_ST_CD"].ToDecimal();
                            member.PER_ZONE_CD = dr["PER_ZONE_CD"].ToDecimal();
                            member.PER_DISTRICT_CD = dr["PER_DISTRICT_CD"].ToDecimal();
                            member.PER_VDC_MUN_CD = dr["PER_VDC_MUN_CD"].ToDecimal();
                            member.PER_WARD_NO = dr["PER_WARD_NO"].ToDecimal();
                            member.PER_AREA_ENG = dr["PER_AREA_ENG"].ConvertToString();
                            member.PER_AREA_LOC = dr["PER_AREA_LOC"].ConvertToString();
                            member.PER_DISTRICT_ENG = dr["PER_DISTRICT_ENG"].ConvertToString();
                            member.PER_DISTRICT_LOC = dr["PER_DISTRICT_LOC"].ConvertToString();
                            member.PER_COUNTRY_ENG = dr["PER_COUNTRY_ENG"].ConvertToString();
                            member.PER_COUNTRY_LOC = dr["PER_COUNTRY_LOC"].ConvertToString();
                            member.PER_REGION_ENG = dr["PER_REGION_ENG"].ConvertToString();
                            member.PER_REGION_LOC = dr["PER_REGION_LOC"].ConvertToString();
                            member.PER_ZONE_ENG = dr["PER_ZONE_ENG"].ConvertToString();
                            member.PER_ZONE_LOC = dr["PER_ZONE_LOC"].ConvertToString();
                            member.PER_VDC_ENG = dr["PER_VDC_ENG"].ConvertToString();
                            member.PER_VDC_LOC = dr["PER_VDC_LOC"].ConvertToString();
                            member.CUR_COUNTRY_CD = dr["CUR_COUNTRY_CD"].ToDecimal();
                            member.CUR_REG_ST_CD = dr["CUR_REG_ST_CD"].ToDecimal();
                            member.CUR_ZONE_CD = dr["CUR_ZONE_CD"].ToDecimal();
                            member.CUR_DISTRICT_CD = dr["CUR_DISTRICT_CD"].ToDecimal();
                            member.CUR_VDC_MUN_CD = dr["CUR_VDC_MUN_CD"].ToDecimal();
                            member.CUR_WARD_NO = dr["CUR_WARD_NO"].ToDecimal();
                            member.CUR_AREA_ENG = dr["CUR_AREA_ENG"].ConvertToString();
                            member.CUR_AREA_LOC = dr["CUR_AREA_LOC"].ConvertToString();
                            member.CUR_DISTRICT_ENG = dr["CUR_DISTRICT_ENG"].ConvertToString();
                            member.CUR_DISTRICT_LOC = dr["CUR_DISTRICT_LOC"].ConvertToString();
                            member.DESC_LOC = dr["DESC_LOC"].ConvertToString();
                            member.DESC_ENG = dr["DESC_ENG"].ConvertToString();
                            member.DESC_LOC1 = dr["DESC_LOC1"].ConvertToString();
                            member.DESC_ENG1 = dr["DESC_ENG1"].ConvertToString();
                            member.DESC_LOC2 = dr["DESC_LOC2"].ConvertToString();
                            member.DESC_ENG2 = dr["DESC_ENG2"].ConvertToString();
                            member.DESC_LOC3 = dr["DESC_LOC3"].ConvertToString();
                            member.DESC_ENG3 = dr["DESC_ENG3"].ConvertToString();
                            member.MEMBER_PHOTO_ID = dr["MEMBER_PHOTO_ID"].ConvertToString();
                            member.GENDER_CD = dr["GENDER_CD"].ToDecimal();
                            member.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                            member.GENDER_LOC = dr["GENDER_LOC"].ConvertToString();
                            member.MARITAL_STATUS_CD = dr["MARITAL_STATUS_CD"].ToDecimal();
                            member.BIRTH_YEAR = dr["BIRTH_YEAR"].ToDecimal();
                            member.BIRTH_MONTH = dr["BIRTH_MONTH"].ToDecimal();
                            member.BIRTH_DAY = dr["BIRTH_DAY"].ToDecimal();
                            member.BIRTH_YEAR_LOC = dr["BIRTH_YEAR_LOC"].ConvertToString();
                            member.BIRTH_MONTH_LOC = dr["BIRTH_MONTH_LOC"].ConvertToString();
                            member.BIRTH_DAY_LOC = dr["BIRTH_DAY_LOC"].ConvertToString();
                            member.BIRTH_DT = dr["BIRTH_DT"].ToDateTime();
                            member.BIRTH_DT_LOC = dr["BIRTH_DT_LOC"].ConvertToString();
                            member.AGE = dr["AGE"].ConvertToString();
                            member.CASTE_CD = dr["CASTE_CD"].ToDecimal();
                            member.CASTE_ENG = dr["CASTE_ENG"].ConvertToString();
                            member.CASTE_LOC = dr["CASTE_LOC"].ConvertToString();
                            member.RELIGION_CD = dr["RELIGION_CD"].ToDecimal();
                            member.RELIGION_ENG = dr["RELIGION_ENG"].ConvertToString();
                            member.RELIGION_LOC = dr["RELIGION_LOC"].ConvertToString();
                            member.LITERATE = dr["LITERATE"].ConvertToString();
                            member.EDUCATION_CD = dr["EDUCATION_CD"].ToDecimal();
                            member.EDUCATION_ENG = dr["EDUCATION_ENG"].ConvertToString();
                            member.EDUCATION_LOC = dr["EDUCATION_LOC"].ConvertToString();
                            member.BIRTH_CERTIFICATE = dtHouseHoldMember.Tables[0].Rows[0]["BIRTH_CERTIFICATE"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.BIRTH_CERTIFICATE_NO = dr["BIRTH_CERTIFICATE_NO"].ConvertToString();
                            member.BIRTH_CER_DISTRICT_CD = dr["BIRTH_CER_DISTRICT_CD"].ToDecimal();
                            member.CTZ_ISSUE = dr["CTZ_ISSUE"].ConvertToString();
                            member.CTZ_NO = dr["CTZ_NO"].ConvertToString();
                            member.CTZ_ISSUE_DISTRICT_CD = dr["CTZ_ISSUE_DISTRICT_CD"].ToDecimal();
                            member.CTZ_ISSUE_YEAR = dr["CTZ_ISSUE_YEAR"].ToDecimal();
                            member.CTZ_ISSUE_MONTH = dr["CTZ_ISSUE_MONTH"].ToDecimal();
                            member.CTZ_ISSUE_DAY = dr["CTZ_ISSUE_DAY"].ToDecimal();
                            member.CTZ_ISSUE_YEAR_LOC = dr["CTZ_ISSUE_YEAR_LOC"].ConvertToString();
                            member.CTZ_ISSUE_MONTH_LOC = dr["CTZ_ISSUE_MONTH_LOC"].ConvertToString();
                            member.CTZ_ISSUE_DAY_LOC = dr["CTZ_ISSUE_DAY_LOC"].ConvertToString();
                            member.CTZ_ISSUE_DT = dr["CTZ_ISSUE_DT"].ToDateTime();
                            member.CTZ_ISSUE_DT_LOC = dr["CTZ_ISSUE_DT_LOC"].ConvertToString();
                            member.VOTER_ID = dr["VOTER_ID"].ConvertToString();
                            member.VOTERID_NO = dr["VOTERID_NO"].ConvertToString();
                            member.VOTERID_DISTRICT_CD = dr["VOTERID_DISTRICT_CD"].ToDecimal();
                            member.VOTERID_ISSUE_DT = dr["VOTERID_ISSUE_DT"].ToDateTime();
                            member.VOTERID_ISSUE_DT_LOC = dr["VOTERID_ISSUE_DT_LOC"].ConvertToString();
                            member.NID_NO = dr["NID_NO"].ConvertToString();
                            member.NID_DISTRICT_CD = dr["NID_DISTRICT_CD"].ToDecimal();
                            member.NID_ISSUE_DT = dr["NID_ISSUE_DT"].ToDateTime();
                            member.NID_ISSUE_DT_LOC = dr["NID_ISSUE_DT_LOC"].ConvertToString();
                            member.DISABILITY = dr["DISABILITY"].ConvertToString();
                            member.TEL_NO = dr["TEL_NO"].ConvertToString();
                            member.MOBILE_NO = dr["MOBILE_NO"].ConvertToString();
                            member.FAX = dr["FAX"].ConvertToString();
                            member.EMAIL = dr["EMAIL"].ConvertToString();
                            member.URL = dr["URL"].ConvertToString();
                            member.PO_BOX_NO = dr["PO_BOX_NO"].ConvertToString();
                            member.PASSPORT_NO = dr["PASSPORT_NO"].ConvertToString();
                            member.PASSPORT_ISSUE_DISTRICT = dr["PASSPORT_ISSUE_DISTRICT"].ToDecimal();
                            member.PRO_FUND_NO = dr["PRO_FUND_NO"].ConvertToString();
                            member.CIT_NO = dr["CIT_NO"].ConvertToString();
                            member.PAN_NO = dr["PAN_NO"].ConvertToString();
                            member.DRIVING_LICENSE_NO = dr["DRIVING_LICENSE_NO"].ConvertToString();
                            member.DEATH = dr["DEATH"].ConvertToString();
                            member.MEMBER_REMARKS = dr["MEMBER_REMARKS"].ConvertToString();
                            member.MEMBER_REMARKS_LOC = dr["MEMBER_REMARKS_LOC"].ConvertToString();
                            member.HOUSEHOLD_HEAD = dr["HOUSEHOLD_HEAD"].ConvertToString();
                            member.NID_ISSUE = dr["NID_ISSUE"].ConvertToString();
                            member.MEMBER_CNT = dr["MEMBER_CNT"].ToDecimal();
                            member.HOUSE_NO = dr["HOUSE_NO"].ConvertToString();
                            member.HOUSEHOLD_TEL_NO = dr["HOUSEHOLD_TEL_NO"].ConvertToString();
                            member.HOUSEHOLD_MOBILE_NO = dr["HOUSEHOLD_MOBILE_NO"].ConvertToString();
                            member.HOUSEHOLD_FAX = dr["HOUSEHOLD_FAX"].ConvertToString();
                            member.HOUSEHOLD_EMAIL = dr["HOUSEHOLD_EMAIL"].ConvertToString();
                            member.HOUSEHOLD_URL = dr["HOUSEHOLD_URL"].ConvertToString();
                            member.HOUSEHOLD_PO_BOX_NO = dr["HOUSEHOLD_PO_BOX_NO"].ConvertToString();
                            member.SOCIAL_ALLOWANCE = dr["SOCIAL_ALLOWANCE"].ConvertToString();
                            member.ALLOWANCE_TYPE_CD = dr["ALLOWANCE_TYPE_CD"].ToDecimal();
                            member.ALLOWANCE_TYPE_ENG = dr["ALLOWANCE_TYPE_ENG"].ConvertToString();
                            member.ALLOWANCE_TYPE_LOC = dr["ALLOWANCE_TYPE_LOC"].ConvertToString();
                            member.SOCIAL_ALL_ISS_DISTRICT = dr["SOCIAL_ALL_ISS_DISTRICT"].ToDecimal();
                            member.IDENTIFICATION_TYPE_CD = dr["IDENTIFICATION_TYPE_CD"].ToDecimal();
                            member.IDENTIFICATION_TYPE_ENG = dr["IDENTIFICATION_TYPE_ENG"].ConvertToString();
                            member.IDENTIFICATION_TYPE_LOC = dr["IDENTIFICATION_TYPE_LOC"].ConvertToString();
                            member.IDENTIFICATION_OTHERS = dr["IDENTIFICATION_OTHERS"].ConvertToString();
                            member.IDENTIFICATION_OTHERS_LOC = dr["IDENTIFICATION_OTHERS_LOC"].ConvertToString();
                            member.FOREIGN_HOUSEHEAD_COUNTRY_ENG = dr["FOREIGN_HOUSEHEAD_COUNTRY_ENG"].ConvertToString();
                            member.FOREIGN_HOUSEHEAD_COUNTRY_LOC = dr["FOREIGN_HOUSEHEAD_COUNTRY_LOC"].ConvertToString();
                            member.BANK_ACCOUNT_FLAG = dr["BANK_ACCOUNT_FLAG"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.BANK_CD = dr["BANK_CD"].ToDecimal();
                            member.BANK_NAME_ENG = dr["BANK_NAME_ENG"].ConvertToString();
                            member.BANK_NAME_LOC = dr["BANK_NAME_LOC"].ConvertToString();
                            member.BANK_BRANCH_CD = dr["BANK_BRANCH_CD"].ToDecimal();
                            member.BANK_BRANCH_NAME_ENG = dr["BANK_BRANCH_NAME_ENG"].ConvertToString();
                            member.BANK_BRANCH_NAME_LOC = dr["BANK_BRANCH_NAME_LOC"].ConvertToString();
                            member.HH_MEMBER_BANK_ACC_NO = dr["HH_MEMBER_BANK_ACC_NO"].ConvertToString();
                            member.HOUSEHOLD_REMARKS = dr["HOUSEHOLD_REMARKS"].ConvertToString();
                            member.HOUSEHOLD_REMARKS_LOC = dr["HOUSEHOLD_REMARKS_LOC"].ConvertToString();
                            member.SHELTER_SINCE_QUAKE_CD = dr["SHELTER_SINCE_QUAKE_CD"].ToDecimal();
                            member.SHELTER_SINCE_QUAKE_ENG = dr["SHELTER_SINCE_QUAKE_ENG"].ConvertToString();
                            member.SHELTER_SINCE_QUAKE_LOC = dr["SHELTER_SINCE_QUAKE_LOC"].ConvertToString();
                            member.SHELTER_BEFORE_QUAKE_CD = dr["SHELTER_BEFORE_QUAKE_CD"].ToDecimal();
                            member.SHELTER_BEFORE_QUAKE_ENG = dr["SHELTER_BEFORE_QUAKE_ENG"].ConvertToString();
                            member.SHELTER_BEFORE_QUAKE_LOC = dr["SHELTER_BEFORE_QUAKE_LOC"].ConvertToString();
                            member.CURRENT_SHELTER_CD = dr["CURRENT_SHELTER_CD"].ToDecimal();
                            member.CURRENT_SHELTER_ENG = dr["CURRENT_SHELTER_ENG"].ConvertToString();
                            member.CURRENT_SHELTER_LOC = dr["CURRENT_SHELTER_LOC"].ConvertToString();
                            member.EQ_VICTIM_IDENTITY_CARD = dr["EQ_VICTIM_IDENTITY_CARD"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.EQ_VICTIM_IDENTITY_CARD_CD = dr["EQ_VICTIM_IDENTITY_CARD_CD"].ToDecimal();
                            member.EQ_VICTIM_IDENTITY_CARD_ENG = dr["EQ_VICTIM_IDENTITY_CARD_ENG"].ConvertToString();
                            member.EQ_VICTIM_IDENTITY_CARD_LOC = dr["EQ_VICTIM_IDENTITY_CARD_LOC"].ConvertToString();
                            member.EQ_VICTIM_IDENTITY_CARD_NO = dr["EQ_VICTIM_IDENTITY_CARD_NO"].ConvertToString();
                            member.MONTHLY_INCOME_CD = dr["MONTHLY_INCOME_CD"].ToDecimal();
                            member.MONTHLY_INCOME_ENG = dr["MONTHLY_INCOME_ENG"].ConvertToString();
                            member.MONTHLY_INCOME_LOC = dr["MONTHLY_INCOME_LOC"].ConvertToString();
                            member.DEATH_IN_A_YEAR = dr["DEATH_IN_A_YEAR"].ConvertToString();
                            member.DEATH_CNT = dr["DEATH_CNT"].ToDecimal();
                            member.HUMAN_DESTROY_FLAG = dr["HUMAN_DESTROY_FLAG"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.HUMAN_DESTROY_CNT = dr["HUMAN_DESTROY_CNT"].ToDecimal();
                            member.STUDENT_SCHOOL_LEFT = dr["STUDENT_SCHOOL_LEFT"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.STUDENT_SCHOOL_LEFT_CNT = dr["STUDENT_SCHOOL_LEFT_CNT"].ToDecimal();
                            member.PREGNANT_REGULAR_CHECKUP = dr["PREGNANT_REGULAR_CHECKUP"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.PREGNANT_REGULAR_CHECKUP_CNT = dr["PREGNANT_REGULAR_CHECKUP_CNT"].ToDecimal();
                            member.CHILD_LEFT_VACINATION = dr["CHILD_LEFT_VACINATION"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.CHILD_LEFT_VACINATION_CNT = dr["CHILD_LEFT_VACINATION_CNT"].ToDecimal();
                            member.LEFT_CHANGE_OCCUPANY = dr["LEFT_CHANGE_OCCUPANY"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            member.LEFT_CHANGE_OCCUPANY_CNT = dr["LEFT_CHANGE_OCCUPANY_CNT"].ToDecimal();
                            member.HOUSEHOLD_ACTIVE = dr["HOUSEHOLD_ACTIVE"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");

                            member.PRESENCE_STATUS_ENG = dr["PRESENCE_STATUS_ENG"].ConvertToString();
                            member.PRESENCE_STATUS_LOC = dr["PRESENCE_STATUS_LOC"].ConvertToString();
                            member.HANDI_COLOR_ENG = dr["HANDI_COLOR_ENG"].ConvertToString();
                            member.HANDI_COLOR_LOC = dr["HANDI_COLOR_LOC"].ConvertToString();
                            member.MARITAL_STATUS_ENG = dr["MARITAL_STATUS_ENG"].ConvertToString();
                            member.MARITAL_STATUS_LOC = dr["MARITAL_STATUS_LOC"].ConvertToString();

                            member.ALLOWANCE_YEARS = dr["ALLOWANCE_YEARS"].ToDecimal();
                            member.ALLOWANCE_MONTH = dr["ALLOWANCE_MONTH"].ToDecimal();
                            member.ALLOWANCE_DAY = dr["ALLOWANCE_DAY"].ToDecimal();
                            member.ALLOWANCE_YEARS_LOC = dr["ALLOWANCE_YEARS_LOC"].ConvertToString();
                            member.ALLOWANCE_MONTH_LOC = dr["ALLOWANCE_MONTH_LOC"].ConvertToString();
                            member.ALLOWANCE_DAY_LOC = dr["ALLOWANCE_DAY_LOC"].ConvertToString();

                            memlist.Add(member);
                        }
                        houseHoldMemberDtlModel.MemberDetailsList = memlist;
                    }

                    //3.Household Member's disappeared or disabled
                    if (dtHouseHoldMember != null && dtHouseHoldMember.Tables[3].Rows.Count > 0)
                    {
                        List<VW_MEMBER_HUMAN_DISTROY_DTL> destroylist = new List<VW_MEMBER_HUMAN_DISTROY_DTL>();
                        foreach (DataRow dr in dtHouseHoldMember.Tables[3].Rows)
                        {
                            VW_MEMBER_HUMAN_DISTROY_DTL memberDestroyDtl = new VW_MEMBER_HUMAN_DISTROY_DTL();
                            memberDestroyDtl.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                            memberDestroyDtl.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                            memberDestroyDtl.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                            memberDestroyDtl.M_DEFINED_CD = dr["M_DEFINED_CD"].ConvertToString();
                            memberDestroyDtl.M_SNO = dr["M_SNO"].ToDecimal();
                            memberDestroyDtl.M_FIRST_NAME_ENG = dr["M_FIRST_NAME_ENG"].ConvertToString();
                            memberDestroyDtl.M_MIDDLE_NAME_ENG = dr["M_MIDDLE_NAME_ENG"].ConvertToString();
                            memberDestroyDtl.M_LAST_NAME_ENG = dr["M_LAST_NAME_ENG"].ConvertToString();
                            memberDestroyDtl.M_FULL_NAME_ENG = dr["M_FULL_NAME_ENG"].ConvertToString();
                            memberDestroyDtl.M_FIRST_NAME_LOC = dr["M_FIRST_NAME_LOC"].ConvertToString();
                            memberDestroyDtl.M_MIDDLE_NAME_LOC = dr["M_MIDDLE_NAME_LOC"].ConvertToString();
                            memberDestroyDtl.M_LAST_NAME_LOC = dr["M_LAST_NAME_LOC"].ConvertToString();
                            memberDestroyDtl.M_FULL_NAME_LOC = dr["M_FULL_NAME_LOC"].ConvertToString();
                            memberDestroyDtl.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                            memberDestroyDtl.GENDER_LOC = dr["GENDER_LOC"].ConvertToString();
                            memberDestroyDtl.DISTRICT_ENG = dr["DISTRICT_ENG"].ConvertToString();
                            memberDestroyDtl.DISTRICT_LOC = dr["DISTRICT_LOC"].ConvertToString();
                            memberDestroyDtl.COUNTRY_ENG = dr["COUNTRY_ENG"].ConvertToString();
                            memberDestroyDtl.COUNTRY_LOC = dr["COUNTRY_LOC"].ConvertToString();
                            memberDestroyDtl.REGION_ENG = dr["REGION_ENG"].ConvertToString();
                            memberDestroyDtl.REGION_LOC = dr["REGION_LOC"].ConvertToString();
                            memberDestroyDtl.ZONE_ENG = dr["ZONE_ENG"].ConvertToString();
                            memberDestroyDtl.ZONE_LOC = dr["ZONE_LOC"].ConvertToString();
                            memberDestroyDtl.VDC_ENG = dr["VDC_ENG"].ConvertToString();
                            memberDestroyDtl.VDC_LOC = dr["VDC_LOC"].ConvertToString();
                            memberDestroyDtl.PER_WARD_NO = dr["PER_WARD_NO"].ToDecimal();
                            memberDestroyDtl.PER_AREA_ENG = dr["PER_AREA_ENG"].ConvertToString();
                            memberDestroyDtl.PER_AREA_LOC = dr["PER_AREA_LOC"].ConvertToString();
                            memberDestroyDtl.AGE = dr["AGE"].ToDecimal();
                            memberDestroyDtl.HUMAN_DESTROY_ENG = dr["HUMAN_DESTROY_ENG"].ConvertToString();
                            memberDestroyDtl.HUMAN_DESTROY_LOC = dr["HUMAN_DESTROY_LOC"].ConvertToString();



                            destroylist.Add(memberDestroyDtl);
                        }
                        houseHoldMemberDtlModel.MemberDisabledDisappearedList = destroylist;
                    }

                    //4.Household Member's death Details
                    if (dtHouseHoldMember != null && dtHouseHoldMember.Tables[4].Rows.Count > 0)
                    {
                        List<VW_MEMBER_DEATH_DTL> deathlist = new List<VW_MEMBER_DEATH_DTL>();
                        foreach (DataRow dr in dtHouseHoldMember.Tables[4].Rows)
                        {
                            VW_MEMBER_DEATH_DTL memberDeathDtl = new VW_MEMBER_DEATH_DTL();
                            memberDeathDtl.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                            memberDeathDtl.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                            memberDeathDtl.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                            memberDeathDtl.M_DEFINED_CD = dr["M_DEFINED_CD"].ConvertToString();
                            memberDeathDtl.M_SNO = dr["M_SNO"].ToDecimal();
                            memberDeathDtl.M_FIRST_NAME_ENG = dr["M_FIRST_NAME_ENG"].ConvertToString();
                            memberDeathDtl.M_MIDDLE_NAME_ENG = dr["M_MIDDLE_NAME_ENG"].ConvertToString();
                            memberDeathDtl.M_LAST_NAME_ENG = dr["M_LAST_NAME_ENG"].ConvertToString();
                            memberDeathDtl.M_FULL_NAME_ENG = dr["M_FULL_NAME_ENG"].ConvertToString();
                            memberDeathDtl.M_FIRST_NAME_LOC = dr["M_FIRST_NAME_LOC"].ConvertToString();
                            memberDeathDtl.M_MIDDLE_NAME_LOC = dr["M_MIDDLE_NAME_LOC"].ConvertToString();
                            memberDeathDtl.M_LAST_NAME_LOC = dr["M_LAST_NAME_LOC"].ConvertToString();
                            memberDeathDtl.M_FULL_NAME_LOC = dr["M_FULL_NAME_LOC"].ConvertToString();
                            memberDeathDtl.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                            memberDeathDtl.GENDER_LOC = dr["GENDER_LOC"].ConvertToString();
                            memberDeathDtl.MARITAL_STATUS_ENG = dr["MARITAL_STATUS_ENG"].ConvertToString();
                            memberDeathDtl.MARITAL_STATUS_LOC = dr["MARITAL_STATUS_LOC"].ConvertToString();
                            memberDeathDtl.DISTRICT_ENG = dr["DISTRICT_ENG"].ConvertToString();
                            memberDeathDtl.DISTRICT_LOC = dr["DISTRICT_LOC"].ConvertToString();
                            memberDeathDtl.COUNTRY_ENG = dr["COUNTRY_ENG"].ConvertToString();
                            memberDeathDtl.COUNTRY_LOC = dr["COUNTRY_LOC"].ConvertToString();
                            memberDeathDtl.REGION_ENG = dr["REGION_ENG"].ConvertToString();
                            memberDeathDtl.REGION_LOC = dr["REGION_LOC"].ConvertToString();
                            memberDeathDtl.ZONE_ENG = dr["ZONE_ENG"].ConvertToString();
                            memberDeathDtl.ZONE_LOC = dr["ZONE_LOC"].ConvertToString();
                            memberDeathDtl.VDC_ENG = dr["VDC_ENG"].ConvertToString();
                            memberDeathDtl.VDC_LOC = dr["VDC_LOC"].ConvertToString();
                            memberDeathDtl.PER_WARD_NO = dr["PER_WARD_NO"].ToDecimal();
                            memberDeathDtl.PER_AREA_ENG = dr["PER_AREA_ENG"].ConvertToString();
                            memberDeathDtl.PER_AREA_LOC = dr["PER_AREA_LOC"].ConvertToString();
                            memberDeathDtl.DEATH_YEAR = dr["DEATH_YEAR"].ConvertToString();
                            memberDeathDtl.DEATH_MONTH = dr["DEATH_MONTH"].ConvertToString();
                            memberDeathDtl.DEATH_DAY = dr["DEATH_DAY"].ConvertToString();
                            memberDeathDtl.DEATH_YEAR_LOC = dr["DEATH_YEAR_LOC"].ConvertToString();
                            memberDeathDtl.DEATH_MONTH_LOC = dr["DEATH_MONTH_LOC"].ConvertToString();
                            memberDeathDtl.DEATH_DAY_LOC = dr["DEATH_DAY_LOC"].ConvertToString();
                            memberDeathDtl.AGE = dr["AGE"].ConvertToString();
                            memberDeathDtl.DEATH_REASON_ENG = dr["DEATH_REASON_ENG"].ConvertToString();
                            memberDeathDtl.DEATH_REASON_LOC = dr["DEATH_REASON_LOC"].ConvertToString();
                            memberDeathDtl.DEATH_CERTIFICATE = dr["DEATH_CERTIFICATE"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            memberDeathDtl.DEATH_CERTIFICATE_NO = dr["DEATH_CERTIFICATE_NO"].ConvertToString();
                            deathlist.Add(memberDeathDtl);
                        }
                        houseHoldMemberDtlModel.MemberDeathDetailsList = deathlist;
                    }

                    // 5. names of Relief Money 
                    DataTable dtReliefMoneyNames = objHouseHoldMemberService.ReliefMoneyNames();
                    if (dtReliefMoneyNames != null && dtReliefMoneyNames.Rows.Count > 0)
                    {
                        List<NHRS_EQ_RELIEF_MONEY> ownerList = new List<NHRS_EQ_RELIEF_MONEY>();
                        foreach (DataRow dr in dtReliefMoneyNames.Rows)
                        {
                            NHRS_EQ_RELIEF_MONEY ownerName = new NHRS_EQ_RELIEF_MONEY();
                            ownerName.EQ_RELIEF_MONEY_CD = Convert.ToDecimal(dr["EQ_RELIEF_MONEY_CD"]);
                            ownerName.EQ_RELIEF_MONEY_ENG = dr["DESC_ENG"].ConvertToString();
                            ownerName.EQ_RELIEF_MONEY_LOC = dr["DESC_LOC"].ConvertToString();
                            //ownerName.ORDER_NO = dr["ORDER_NO"].ToDecimal();
                            ownerList.Add(ownerName);
                        }
                        houseHoldMemberDtlModel.NHRS_EQ_RELIEF_MONEY_Names = ownerList;
                    }
                    //5.1. Relief Money  Checkbox
                    if (dtHouseHoldMember != null && dtHouseHoldMember.Tables[5].Rows.Count > 0)
                    {
                        List<NHRS_EQ_RELIEF_MONEY> ownerList = new List<NHRS_EQ_RELIEF_MONEY>();
                        foreach (DataRow dr in dtHouseHoldMember.Tables[5].Rows)
                        {
                            NHRS_EQ_RELIEF_MONEY ownerName = new NHRS_EQ_RELIEF_MONEY();
                            ownerName.EQ_RELIEF_MONEY_CD = Convert.ToDecimal(dr["EQ_RELIEF_MONEY_CD"]);
                            ownerName.EQ_RELIEF_MONEY_ENG = dr["EQ_RELIEF_MONEY_ENG"].ConvertToString();
                            ownerName.EQ_RELIEF_MONEY_LOC = dr["EQ_RELIEF_MONEY_LOC"].ConvertToString();
                            //ownerName.ORDER_NO = dr["ORDER_NO"].ToDecimal();
                            ownerList.Add(ownerName);
                        }
                        houseHoldMemberDtlModel.NHRS_EQ_RELIEF_MONEY_Detail = ownerList;
                    }

                    // 10. names of Household Indicator 
                    DataTable dtHouseIndicatorNames = objHouseHoldMemberService.HouseIndicatorNames();
                    if (dtHouseIndicatorNames != null && dtHouseIndicatorNames.Rows.Count > 0)
                    {
                        List<MIG_MIS_HOUSEHOLD_INDICATOR> ownerList = new List<MIG_MIS_HOUSEHOLD_INDICATOR>();
                        foreach (DataRow dr in dtHouseIndicatorNames.Rows)
                        {
                            MIG_MIS_HOUSEHOLD_INDICATOR ownerName = new MIG_MIS_HOUSEHOLD_INDICATOR();
                            ownerName.indicatorCd = Convert.ToDecimal(dr["INDICATOR_CD"]);
                            ownerName.descEng = dr["DESC_ENG"].ConvertToString();
                            ownerName.descLoc = dr["DESC_LOC"].ConvertToString();
                            //ownerName.orderNo = dr["ORDER_NO"].ToDecimal();
                            ownerList.Add(ownerName);
                        }
                        houseHoldMemberDtlModel.MIG_MIS_HOUSEHOLD_INDICATOR_Names = ownerList;
                    }
                    //10.1. Household Indicator  Checkbox
                    if (dtHouseHoldMember != null && dtHouseHoldMember.Tables[10].Rows.Count > 0)
                    {
                        List<MIG_MIS_HOUSEHOLD_INDICATOR> ownerList = new List<MIG_MIS_HOUSEHOLD_INDICATOR>();
                        foreach (DataRow dr in dtHouseHoldMember.Tables[10].Rows)
                        {
                            MIG_MIS_HOUSEHOLD_INDICATOR ownerName = new MIG_MIS_HOUSEHOLD_INDICATOR();
                            ownerName.indicatorCd = Convert.ToDecimal(dr["INDICATOR_CD"]);
                            ownerName.descEng = dr["INDICATOR_ENG"].ConvertToString();
                            ownerName.descLoc = dr["INDICATOR_LOC"].ConvertToString();
                            //ownerName.orderNo = dr["ORDER_NO"].ToDecimal();
                            ownerName.INDICATOR_BEFORE = dr["INDICATOR_BEFORE"].ConvertToString();
                            ownerName.INDICATOR_AFTER = dr["INDICATOR_AFTER"].ConvertToString();
                            ownerList.Add(ownerName);
                        }
                        houseHoldMemberDtlModel.MIG_MIS_HOUSEHOLD_INDICATOR_Detail = ownerList;
                    }
                    // Other Owner Detail
                    DataTable dtOwnrDtl = new DataTable();
                    dtOwnrDtl = objHouseHoldMemberService.GetOwnerOtherDetail(houseOwnerid);
                    foreach (DataRow dr in dtOwnrDtl.Rows)
                    {
                        houseHoldMemberDtlModel.ONA_ID = dr["ONA_ID"].ConvertToString();
                        houseHoldMemberDtlModel.SubmissionDate = dr["SUBMISSIONTIME"].ConvertToString();
                        houseHoldMemberDtlModel.KLLDistrictCD = dr["KLL_DISTRICT_CD"].ConvertToString();
                    }
                }

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View(houseHoldMemberDtlModel);
        }

        //[HttpPost]
        //public ActionResult HouseHoldInfoSearchResult(NHRSSearch objNHRS, FormCollection fc)
        //{
        //    SearchService serSearch = new SearchService();
        //    objNHRS = new NHRSSearch();
        //    DataTable result = new DataTable();
        //    //objNHRS.StrRegion = GetData.GetCodeFor(DataType.Region, fc["StrRegion"].ConvertToString());
        //    //objNHRS.StrZone = GetData.GetCodeFor(DataType.Zone, fc["StrZone"].ConvertToString());
        //    objNHRS.StrDistrict = GetData.GetCodeFor(DataType.District, fc["StrDistrict"].ConvertToString());
        //    objNHRS.StrWard = fc["StrWard"].ConvertToString();
        //    //objNHRS.DAMAGERESOLUTION = common.GetDescriptionFromCode(fc["TECHSOLUTION_CD"], "NHRS_TECHNICAL_SOLUTION", "DESC_ENG");
        //    objNHRS.StrVDC = GetData.GetCodeFor(DataType.VdcMun, fc["StrVDC"].ConvertToString());
        //    objNHRS.StrFullName = fc["StrFullName"].ConvertToString();
        //    objNHRS.StrGender = fc["StrGender"].ConvertToString();
        //    objNHRS.AgeFrom = fc["AgeFrom"].ConvertToString();
        //    objNHRS.AgeTo = fc["AgeTo"].ConvertToString();
        //    objNHRS.INTERVIEW_DT_FROM = fc["INTERVIEW_DT_FROM"].ConvertToString();
        //    objNHRS.INTERVIEW_DT_TO = fc["INTERVIEW_DT_TO"].ConvertToString();
        //    objNHRS.StrHouseHoldSN = fc["StrHouseHoldSN"].ConvertToString();
        //    objNHRS.ASSESSED_AREA_CD = fc["ASSESSED_AREA_CD"].ConvertToString();
        //    objNHRS.ENUMERATOR_ID = fc["ENUMERATOR_ID"].ConvertToString();
        //    objNHRS.HOUSE_OWNER_CNT = fc["HOUSE_OWNER_CNT"].ConvertToString();
        //    objNHRS.NOT_INTERVIWING_REASON_CD = fc["NOT_INTERVIWING_REASON_CD"].ConvertToString();
        //    objNHRS.Imei = fc["Imei"].ConvertToString();
        //    objNHRS.HOUSE_LEGALOWNER = fc["HOUSE_LEGALOWNER"].ConvertToString();
        //    objNHRS.BUILDING_CONDITION_CD = fc["BUILDING_CONDITION_CD"].ConvertToString();
        //    objNHRS.GROUND_SURFACE_CD = fc["GROUND_SURFACE_CD"].ConvertToString();
        //    objNHRS.FOUNDATION_TYPE_CD = fc["FOUNDATION_TYPE_CD"].ConvertToString();
        //    objNHRS.RC_MATERIAL_CD = fc["RC_MATERIAL_CD"].ConvertToString();
        //    objNHRS.FC_MATERIAL_CD = fc["FC_MATERIAL_CD"].ConvertToString();
        //    objNHRS.SC_MATERIAL_CD = fc["SC_MATERIAL_CD"].ConvertToString();
        //    objNHRS.BUILDING_POSITION_CD = fc["BUILDING_POSITION_CD"].ConvertToString();
        //    objNHRS.BUILDING_PLAN_CD = fc["BUILDING_PLAN_CD"].ConvertToString();
        //    objNHRS.DAMAGE_GRADE_CD = fc["DAMAGE_GRADE_CD"].ConvertToString();
        //    objNHRS.TECHSOLUTION_CD = fc["TECHSOLUTION_CD"].ConvertToString();
        //    objNHRS.SESSION_ID = fc["SESSION_ID"].ConvertToString();
        //    objNHRS.MEMBER_ID = fc["MEMBER_ID"].ConvertToString();
        //    objNHRS.DEFINED_CD = fc["DEFINED_CD"].ConvertToString();
        //    objNHRS.BIRTH_DT_ST = fc["BIRTH_DT_ST"].ConvertToString();
        //    objNHRS.BIRTH_DT_TO = fc["BIRTH_DT_TO"].ConvertToString();
        //    objNHRS.MARITAL_STATUS_CD = fc["MARITAL_STATUS_CD"].ConvertToString();
        //    objNHRS.AGE = fc["AGE"].ConvertToString();
        //    objNHRS.CASTE_CD = fc["CASTE_CD"].ConvertToString();
        //    objNHRS.RELIGION_CD = fc["RELIGION_CD"].ConvertToString();
        //    objNHRS.EDUCATION_CD = fc["EDUCATION_CD"].ConvertToString();
        //    objNHRS.CTZ_NO = fc["CTZ_NO"].ConvertToString();
        //    objNHRS.CTZ_ISSUE_DT = fc["CTZ_ISSUE_DT"].ConvertToString();
        //    objNHRS.CTZ_ISSUE_DT_LOC = fc["CTZ_ISSUE_DT_LOC"].ConvertToString();
        //    objNHRS.CUR_DISTRICT_CD = fc["CUR_DISTRICT_CD"].ConvertToString();
        //    objNHRS.CUR_VDC_MUN_CD = fc["CUR_VDC_MUN_CD"].ConvertToString();
        //    objNHRS.CUR_WARD_NO = fc["CUR_WARD_NO"].ConvertToString();
        //    objNHRS.CUR_AREA_ENG = fc["CUR_AREA_ENG"].ConvertToString();
        //    objNHRS.PER_DISTRICT_CD = fc["PER_DISTRICT_CD"].ConvertToString();
        //    objNHRS.PER_VDC_MUN_CD = fc["PER_VDC_MUN_CD"].ConvertToString();
        //    objNHRS.PER_WARD_NO = fc["PER_WARD_NO"].ConvertToString();
        //    objNHRS.ENTERED_BY = fc["ENTERED_BY"].ConvertToString();
        //    objNHRS.HOUSEHOLD_FORM_NO_FROM = fc["HOUSEHOLD_FORM_NO_FROM"].ConvertToString();
        //    objNHRS.HOUSEHOLD_FORM_NO_TO = fc["HOUSEHOLD_FORM_NO_TO"].ConvertToString();
        //    objNHRS.HOUSEHOLDIDFRM = fc["HOUSEHOLDIDFRM"].ConvertToString();
        //    objNHRS.HOUSEHOLDIDTO = fc["HOUSEHOLDIDTO"].ConvertToString();
        //    objNHRS.AGE = fc["AGE"].ConvertToString();
        //    objNHRS.FIRST_NAME_ENG = fc["FIRST_NAME_ENG"].ConvertToString();
        //    objNHRS.MIDDLE_NAME_ENG = fc["MIDDLE_NAME_ENG"].ConvertToString();
        //    objNHRS.LAST_NAME_ENG = fc["LAST_NAME_ENG"].ConvertToString();
        //    objNHRS.IDENTIFICATION_TYPE_CD = fc["IDENTIFICATION_TYPE_CD"].ConvertToString();
        //    objNHRS.BANK_CD = fc["BANK_CD"].ConvertToString();
        //    objNHRS.WATER_SOURCE_CD_II = fc["WATER_SOURCE_CD_II"].ConvertToString();
        //    objNHRS.FUEL_SOURCE_CD_II = fc["FUEL_SOURCE_CD_II"].ConvertToString();
        //    objNHRS.LIGHT_SOURCE_CD_II = fc["LIGHT_SOURCE_CD_II"].ConvertToString();
        //    objNHRS.TOILET_TYPE_CD_I = fc["TOILET_TYPE_CD_I"].ConvertToString();
        //    objNHRS.WATER_DISTANCE_HR = fc["WATER_DISTANCE_HR"].ConvertToString();
        //    objNHRS.WATER_DISTANCE_MIN = fc["WATER_DISTANCE_MIN"].ConvertToString();
        //    objNHRS.TOILET_SHARED = fc["TOILET_SHARED"].ConvertToString();

        //    objNHRS.INTERVIEWED_BY = fc["INTERVIEWED_BY"].ConvertToString();
        //    if (fc["SearchMode"] == "0")
        //    {
        //        //result = serSearch.getHouseSearchDetail(objNHRS);
        //        result = serSearch.getHouseSearchDetail(objNHRS);
        //        ViewData["result"] = result;
        //        return PartialView("~/Views/NHRP/_HouseHoldInfobyHouse.cshtml");
        //    }
        //    else if (fc["SearchMode"] == "1")
        //    {
        //        result = serSearch.getHouseholdSearchDetail(objNHRS);
        //        ViewData["Household_Result"] = result;
        //        return PartialView("~/Views/NHRP/_HouseHoldInfobyHouseHold.cshtml");
        //    }
        //    else if (fc["SearchMode"] == "2")
        //    {
        //        result = serSearch.getMemberSearchDetail(objNHRS);
        //        ViewData["Member_Result"] = result;
        //        return PartialView("~/Views/NHRP/_HouseHoldInfobyMember.cshtml");
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #region Export To Excel
        public ActionResult ExportToExcel(string searchType)
        {
            DataTable dt = new DataTable();
            dt = searchType == "house" ? (DataTable)Session["dtHouse"] : (searchType == "household" ? (DataTable)Session["dtHousehold"] : (searchType == "member" ? (DataTable)Session["dtMember"] : (searchType == "structure" ? (DataTable)Session["dtStructure"] : null)));
            dt = ColumnsPreparation(dt, searchType);
            dt = SetOrdinals(dt, searchType);
            string filepath = Server.MapPath("~/Excel/xportExl.xls");
            //Utils.ExportToExcel2(dt, filepath);
            ExcelExport(dt, searchType);
            return File(filepath, "application/xlsx", "xportExl.xls");
        }
        public ActionResult ExportToExcel1(string searchType)
        {
            DataTable dt = new DataTable();
            dt = (searchType == "structure" ? (DataTable)Session["dtStructure"] : null);
            dt = ColumnsPreparation(dt, searchType);
            dt = SetOrdinals(dt, searchType);
            string filepath = Server.MapPath("~/Excel/xportExl.xls");
            //Utils.ExportToExcel2(dt, filepath);
            ExcelExport(dt, searchType);
            return File(filepath, "application/xlsx", "xportExl.xls");
        }
        public DataTable ColumnsPreparation(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();
            if (searchType == "house")
            {
                requiredCols.Add("HOUSE_ID");
                requiredCols.Add("HH_SERIAL_NO");
                requiredCols.Add(Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC"));
                requiredCols.Add("ADDRESS");
                requiredCols.Add("TOTAL_BUILDING_STUCTURE_CNT");
                requiredCols.Add("TOTAL_HOUSEHOLD_CNT");
                requiredCols.Add("TOTAL_MEMBER_CNT");
            }
            if (searchType == "household")
            {
                requiredCols.Add("HOUSEHOLD_ID");
                requiredCols.Add(Utils.ToggleLanguage("HEAD_FULL_NAME_ENG", "HEAD_FULL_NAME_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("HEAD_GENDER_NAME_ENG", "HEAD_GENDER_NAME_LOC"));
                requiredCols.Add("MEMBER_CNT");
            }
            if (searchType == "member")
            {
                requiredCols.Add("H_DEFINED_CD");
                requiredCols.Add("DEFINED_CD");
                requiredCols.Add(Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC"));
                requiredCols.Add("HOUSEHOLD_HEAD");
                requiredCols.Add("MOBILE_NO");
                requiredCols.Add(Utils.ToggleLanguage("BIRTH_DT", "BIRTH_DT_LOC"));
            }
            if (searchType == "structure")
            {
                requiredCols.Add("HOUSE_ID");
                requiredCols.Add("ADDRESS");
                requiredCols.Add(Utils.ToggleLanguage("OWNER_FULL_NAME_ENG", "OWNER_FULL_NAME_LOC"));
                requiredCols.Add("BUILDING_STRUCTURE_NO");
                requiredCols.Add(Utils.ToggleLanguage("DAMAGE_GRADE_NAME_ENG", "DAMAGE_GRADE_NAME_LOC"));
                requiredCols.Add(Utils.ToggleLanguage("TECHSOLUTION_NAME_ENG", "TECHSOLUTION_NAME_LOC"));
            }
            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable SetOrdinals(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            if (searchType == "house")
            {
                dtModified.Columns["HOUSE_ID"].SetOrdinal(0);
                dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(1);
                dtModified.Columns[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].SetOrdinal(2);
                dtModified.Columns["ADDRESS"].SetOrdinal(3);
                dtModified.Columns["TOTAL_BUILDING_STUCTURE_CNT"].SetOrdinal(4);
                dtModified.Columns["TOTAL_HOUSEHOLD_CNT"].SetOrdinal(5);
                dtModified.Columns["TOTAL_MEMBER_CNT"].SetOrdinal(6);
                dtModified.Columns["HOUSE_ID"].ColumnName = Utils.GetLabel("HOUSE ID");
                dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("HOUSE SERIAL NO");
                dtModified.Columns[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].ColumnName = Utils.GetLabel("OWNER'S NAME");
                dtModified.Columns["TOTAL_BUILDING_STUCTURE_CNT"].ColumnName = Utils.GetLabel("STRUCTURE");
                dtModified.Columns["ADDRESS"].ColumnName = Utils.GetLabel("ADDRESS");
                dtModified.Columns["TOTAL_HOUSEHOLD_CNT"].ColumnName = Utils.GetLabel("FAMILY");
                dtModified.Columns["TOTAL_MEMBER_CNT"].ColumnName = Utils.GetLabel("MEMBER");
            }
            if (searchType == "household")
            {
                dtModified.Columns["HOUSEHOLD_ID"].SetOrdinal(0);
                dtModified.Columns[Utils.ToggleLanguage("HEAD_FULL_NAME_ENG", "HEAD_FULL_NAME_LOC")].SetOrdinal(1);
                dtModified.Columns[Utils.ToggleLanguage("HEAD_GENDER_NAME_ENG", "HEAD_GENDER_NAME_LOC")].SetOrdinal(2);
                dtModified.Columns["MEMBER_CNT"].SetOrdinal(3);
                dtModified.Columns["HOUSEHOLD_ID"].ColumnName = Utils.GetLabel("HOUSEHOLD ID");
                dtModified.Columns[Utils.ToggleLanguage("HEAD_FULL_NAME_ENG", "HEAD_FULL_NAME_LOC")].ColumnName = Utils.GetLabel("HEAD'S NAME");
                dtModified.Columns[Utils.ToggleLanguage("HEAD_GENDER_NAME_ENG", "HEAD_GENDER_NAME_LOC")].ColumnName = Utils.GetLabel("GENDER");
                dtModified.Columns["MEMBER_CNT"].ColumnName = Utils.GetLabel("MEMBER");
            }
            if (searchType == "member")
            {
                dtModified.Columns["H_DEFINED_CD"].SetOrdinal(0);
                dtModified.Columns["DEFINED_CD"].SetOrdinal(1);
                dtModified.Columns[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].SetOrdinal(2);
                dtModified.Columns["HOUSEHOLD_HEAD"].SetOrdinal(3);
                dtModified.Columns["MOBILE_NO"].SetOrdinal(4);
                dtModified.Columns[Utils.ToggleLanguage("BIRTH_DT", "BIRTH_DT_LOC")].SetOrdinal(5);
                dtModified.Columns["H_DEFINED_CD"].ColumnName = Utils.GetLabel("HOUSEHOLD ID");
                dtModified.Columns["DEFINED_CD"].ColumnName = Utils.GetLabel("MEMBER ID");
                dtModified.Columns[Utils.ToggleLanguage("FULL_NAME_ENG", "FULL_NAME_LOC")].ColumnName = Utils.GetLabel("MEMBER NAME");
                dtModified.Columns["HOUSEHOLD_HEAD"].ColumnName = Utils.GetLabel("IS HOUSEHOLD HEAD");
                dtModified.Columns["MOBILE_NO"].ColumnName = Utils.GetLabel("MOBILE NO");
                dtModified.Columns[Utils.ToggleLanguage("BIRTH_DT", "BIRTH_DT_LOC")].ColumnName = Utils.GetLabel("DATE OF BIRTH");
            }
            if (searchType == "structure")
            {
                dtModified.Columns["HOUSE_ID"].SetOrdinal(0);
                dtModified.Columns["ADDRESS"].SetOrdinal(1);
                dtModified.Columns["BUILDING_STRUCTURE_NO"].SetOrdinal(2);
                dtModified.Columns[Utils.ToggleLanguage("OWNER_FULL_NAME_ENG", "OWNER_FULL_NAME_LOC")].SetOrdinal(3);
                dtModified.Columns[Utils.ToggleLanguage("DAMAGE_GRADE_NAME_ENG", "DAMAGE_GRADE_NAME_LOC")].SetOrdinal(4);
                dtModified.Columns[Utils.ToggleLanguage("TECHSOLUTION_NAME_ENG", "TECHSOLUTION_NAME_LOC")].SetOrdinal(5);
                dtModified.Columns["HOUSE_ID"].ColumnName = Utils.GetLabel("HOUSE ID");
                dtModified.Columns[Utils.ToggleLanguage("OWNER_FULL_NAME_ENG", "OWNER_FULL_NAME_LOC")].ColumnName = Utils.GetLabel("OWNER'S NAME");
                dtModified.Columns["ADDRESS"].ColumnName = Utils.GetLabel("ADDRESS");
                dtModified.Columns["BUILDING_STRUCTURE_NO"].ColumnName = Utils.GetLabel("STRUCTURE NO");
                dtModified.Columns[Utils.ToggleLanguage("TECHSOLUTION_NAME_ENG", "TECHSOLUTION_NAME_LOC")].ColumnName = Utils.GetLabel("TECHNICAL SOLUTION");
                dtModified.Columns[Utils.ToggleLanguage("DAMAGE_GRADE_NAME_ENG", "DAMAGE_GRADE_NAME_LOC")].ColumnName = Utils.GetLabel("DAMAGE GRADE");
            }
            return dtModified;
        }

        protected void ExcelExport(DataTable dtRecords, string searchType)
        {
            string XlsPath = searchType == "house" ? Server.MapPath(@"~/Excel/HouseReport.xls") : (searchType == "household" ? Server.MapPath(@"~/Excel/HouseholdReport.xls") : (searchType == "member" ? Server.MapPath(@"~/Excel/MemberReport.xls") : (searchType == "structure" ? Server.MapPath(@"~/Excel/StructureReport.xls") : null)));
            //string XlsPath = Server.MapPath(@"~/Excel/HouseholdReport.xls");
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
        #endregion
    }
}


