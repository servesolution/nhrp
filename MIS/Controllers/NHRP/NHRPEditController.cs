using ExceptionHandler;
using MIS.Models.Core;
using MIS.Models.NHRP;
using MIS.Models.NHRP.View;
using MIS.Models.Registration.Household;
using MIS.Models.Search;
using MIS.Services.Core;
using MIS.Services.NHRP.Edit;
using MIS.Services.NHRP.Search;
using MIS.Services.NHRP.View;
using MIS.Services.Registration.Household;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.NHRP
{
    public class NHRPEditController : BaseController
    
    {
     
        CommonFunction common = null;
        SearchHouseholdModel household = new SearchHouseholdModel();
        MIG_MIS_HOUSEHOLD_INFO house = new MIG_MIS_HOUSEHOLD_INFO();
        HouseholdService househead = new HouseholdService();
       
        public NHRPEditController()
        {
            common = new CommonFunction();

            household = new SearchHouseholdModel();
            house = new MIG_MIS_HOUSEHOLD_INFO();
            househead = new HouseholdService();

        }
        public ActionResult NHRSInterviewHouseDetail(string p)
        {
            GetDropDown();
            CheckPermission();
            NHRS_HOUSE_BUILDING_DTL objHouseDetail = new NHRS_HOUSE_BUILDING_DTL();
            NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
            NHRSInterviewService objNHRS = new NHRSInterviewService();
            NHRSSocioEconomicDetailService objHousehold = new NHRSSocioEconomicDetailService();
            DataTable dt = new DataTable();
            string id = string.Empty;
            string id2 = string.Empty;
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {

                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }
                        if (rvd["id2"] != null)
                        {
                            id2 = (rvd["id2"].ConvertToString());
                        }
                    }
                }
                DataTable dthouseid = objNHRS.getHouseID(id2);
                if (dthouseid != null && dthouseid.Rows.Count > 0)
                {
                    objHouseDetail.HOUSE_ID = dthouseid.Rows[0]["DEFINED_CD"].ConvertToString();
                }
                DataTable resultDamageDetail = new DataTable();
                resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                ViewData["resultDamageDetail"] = resultDamageDetail;
                if (id != "" && id != null && id2 != "" && id2 != null)
                {

                    DataTable dtStructureInfo = objNHRSHouseDetail.BuildingStructureDetail(id, id2);
                    if (dtStructureInfo.Rows.Count > 0 && dtStructureInfo != null)
                    {
                        objHouseDetail.HOUSE_OWNER_ID = dtStructureInfo.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                        objHouseDetail.BUILDING_STRUCTURE_NO = dtStructureInfo.Rows[0]["BUILDING_STRUCTURE_NO"].ConvertToString();
                        objHouseDetail.HOUSE_LAND_LEGAL_OWNER_CD = dtStructureInfo.Rows[0]["HOUSE_LAND_LEGAL_OWNER"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["HOUSE_LAND_LEGAL_OWNER"].ConvertToString(), "NHRS_HOUSE_LAND_LEGAL_OWNER", "HOUSE_LAND_LEGAL_OWNERCD", "DEFINED_CD") : null;
                        objHouseDetail.HOUSE_LAND_LEGAL_OWNER = common.GetDescriptionFromCode("DESC_ENG", "NHRS_HOUSE_LAND_LEGAL_OWNER", "HOUSE_LAND_LEGAL_OWNERCD");

                        objHouseDetail.BUILDING_CONDITION_CD = dtStructureInfo.Rows[0]["BUILDING_CONDITION_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["BUILDING_CONDITION_CD"].ConvertToString(), "NHRS_BUILDING_CONDITION", "BUILDING_CONDITION_CD", "BUILDING_CONDITION_DEF_CD") : null;

                        objHouseDetail.BUILDING_CONDITION_ENG = dtStructureInfo.Rows[0]["BUILDING_CONDITION_ENG"].ConvertToString();
                        objHouseDetail.BUILDING_CONDITION_LOC = dtStructureInfo.Rows[0]["BUILDING_CONDITION_LOC"].ConvertToString();
                        objHouseDetail.STOREYS_CNT_BEFORE = dtStructureInfo.Rows[0]["STOREYS_CNT_BEFORE"].ToDecimal();
                        objHouseDetail.STOREYS_CNT_AFTER = dtStructureInfo.Rows[0]["STOREYS_CNT_AFTER"].ToDecimal();
                        objHouseDetail.PLINTH_AREA = dtStructureInfo.Rows[0]["PLINTH_AREA"].ConvertToString();
                        objHouseDetail.HOUSE_AGE = dtStructureInfo.Rows[0]["HOUSE_AGE"].ToDecimal();
                        objHouseDetail.HOUSE_HEIGHT_BEFORE_EQ = dtStructureInfo.Rows[0]["HOUSE_HEIGHT_BEFORE_EQ"].ConvertToString();
                        objHouseDetail.HOUSE_HEIGHT_AFTER_EQ = dtStructureInfo.Rows[0]["HOUSE_HEIGHT_AFTER_EQ"].ConvertToString();
                        objHouseDetail.GROUND_SURFACE_CD = dtStructureInfo.Rows[0]["GROUND_SURFACE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["GROUND_SURFACE_CD"].ConvertToString(), "NHRS_GROUND_SURFACE", "GROUND_SURFACE_CD", "GROUND_SURFACE_DEF_CD") : null;
                        objHouseDetail.GROUND_SURFACE_ENG = dtStructureInfo.Rows[0]["GROUND_SURFACE_ENG"].ConvertToString();
                        objHouseDetail.GROUND_SURFACE_LOC = dtStructureInfo.Rows[0]["GROUND_SURFACE_LOC"].ConvertToString();
                        objHouseDetail.FOUNDATION_TYPE_CD = dtStructureInfo.Rows[0]["FOUNDATION_TYPE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["FOUNDATION_TYPE_CD"].ConvertToString(), "NHRS_FOUNDATION_TYPE", "FOUNDATION_TYPE_CD", "FOUNDATION_TYPE_DEF_CD") : null;
                        objHouseDetail.RC_MATERIAL_CD = dtStructureInfo.Rows[0]["RC_MATERIAL_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["RC_MATERIAL_CD"].ConvertToString(), "NHRS_ROOF_CONSTRUCT_MATERIAL", "RC_MATERIAL_CD", "RC_MATERIAL_DEF_CD") : null;
                        objHouseDetail.RC_MATERIAL_ENG = dtStructureInfo.Rows[0]["RC_MATERIAL_ENG"].ConvertToString();
                        objHouseDetail.RC_MATERIAL_LOC = dtStructureInfo.Rows[0]["RC_MATERIAL_LOC"].ConvertToString();
                        objHouseDetail.FC_MATERIAL_CD = dtStructureInfo.Rows[0]["FC_MATERIAL_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["FC_MATERIAL_CD"].ConvertToString(), "NHRS_FLOOR_CONSTRUCT_MATERIAL", "FC_MATERIAL_CD", "FC_MATERIAL_DEF_CD") : null;
                        objHouseDetail.FC_MATERIAL_ENG = dtStructureInfo.Rows[0]["FC_MATERIAL_ENG"].ConvertToString();
                        objHouseDetail.SC_MATERIAL_CD = dtStructureInfo.Rows[0]["SC_MATERIAL_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["SC_MATERIAL_CD"].ConvertToString(), "NHRS_STOREY_CONSRUCT_MATERIAL", "SC_MATERIAL_CD", "SC_MATERIAL_DEF_CD") : null;
                        objHouseDetail.BUILDING_POSITION_CD = dtStructureInfo.Rows[0]["BUILDING_POSITION_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["BUILDING_POSITION_CD"].ConvertToString(), "NHRS_BUILDING_POSITION_CONFIG", "BUILDING_POSITION_CD", "BUILDING_POSITION_DEF_CD") : null;
                        objHouseDetail.BUILDING_POSITION_ENG = dtStructureInfo.Rows[0]["BUILDING_POSITION_ENG"].ConvertToString();
                        objHouseDetail.BUILDING_POSITION_LOC = dtStructureInfo.Rows[0]["BUILDING_POSITION_LOC"].ConvertToString();
                        objHouseDetail.BUILDING_PLAN_CD = dtStructureInfo.Rows[0]["BUILDING_PLAN_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["BUILDING_PLAN_CD"].ConvertToString(), "NHRS_BUILDING_PLAN_CONFIG", "BUILDING_PLAN_CD", "BUILDING_PLAN_DEF_CD") : null;
                        objHouseDetail.BUILDING_PLAN_ENG = dtStructureInfo.Rows[0]["BUILDING_PLAN_ENG"].ConvertToString();
                        objHouseDetail.BUILDING_PLAN_LOC = dtStructureInfo.Rows[0]["BUILDING_PLAN_LOC"].ConvertToString();
                        objHouseDetail.IS_GEOTECHNICAL_RISK = dtStructureInfo.Rows[0]["IS_GEOTECHNICAL_RISK"].ConvertToString();
                        objHouseDetail.ASSESSED_AREA_CD = dtStructureInfo.Rows[0]["ASSESSED_AREA_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["ASSESSED_AREA_CD"].ConvertToString(), "NHRS_ASSESSED_AREA", "ASSESSED_AREA_CD", "ASSESSED_AREA_DEF_CD") : null;
                        objHouseDetail.ASSESSED_AREA_ENG = dtStructureInfo.Rows[0]["ASSESSED_AREA_ENG"].ConvertToString();
                        objHouseDetail.ASSESSED_AREA_LOC = dtStructureInfo.Rows[0]["ASSESSED_AREA_LOC"].ConvertToString();
                        objHouseDetail.DAMAGE_GRADE_CD = dtStructureInfo.Rows[0]["DAMAGE_GRADE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["DAMAGE_GRADE_CD"].ConvertToString(), "NHRS_DAMAGE_GRADE", "DAMAGE_GRADE_CD", "DAMAGE_GRADE_DEF_CD") : null;
                        objHouseDetail.DAMAGE_GRADE_LOC = dtStructureInfo.Rows[0]["DAMAGE_GRADE_LOC"].ConvertToString();
                        objHouseDetail.TECHSOLUTION_CD = dtStructureInfo.Rows[0]["TECHSOLUTION_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dtStructureInfo.Rows[0]["TECHSOLUTION_CD"].ConvertToString(), "NHRS_TECHNICAL_SOLUTION", "TECHSOLUTION_CD", "TECHSOLUTION_DEF_CD") : null;
                        objHouseDetail.TECHSOLUTION_ENG = dtStructureInfo.Rows[0]["TECHSOLUTION_ENG"].ConvertToString();
                        objHouseDetail.TECHSOLUTION_LOC = dtStructureInfo.Rows[0]["TECHSOLUTION_LOC"].ConvertToString();
                        objHouseDetail.TECHSOLUTION_COMMENT = dtStructureInfo.Rows[0]["TECHSOLUTION_COMMENT"].ConvertToString();
                        objHouseDetail.TECHSOLUTION_COMMENT_LOC = dtStructureInfo.Rows[0]["TECHSOLUTION_COMMENT_LOC"].ConvertToString();
                        objHouseDetail.RECONSTRUCTION_STARTED = dtStructureInfo.Rows[0]["RECONSTRUCTION_STARTED"].ConvertToString();
                        objHouseDetail.IS_SECONDARY_USE = dtStructureInfo.Rows[0]["IS_SECONDARY_USE"].ConvertToString();
                        objHouseDetail.LATITUDE = dtStructureInfo.Rows[0]["LATITUDE"].ConvertToString();
                        objHouseDetail.LONGITUDE = dtStructureInfo.Rows[0]["LONGITUDE"].ConvertToString();
                        objHouseDetail.ALTITUDE = dtStructureInfo.Rows[0]["ALTITUDE"].ConvertToString();
                        objHouseDetail.ACCURACY = dtStructureInfo.Rows[0]["ACCURACY"].ConvertToString();
                        objHouseDetail.HOUSEHOLD_CNT_AFTER_EQ = dtStructureInfo.Rows[0]["HOUSEHOLD_CNT_AFTER_EQ"].ToDecimal();
                        objHouseDetail.FOUNDATION_TYPE_ENG = dtStructureInfo.Rows[0]["FOUNDATION_TYPE_ENG"].ConvertToString();
                        objHouseDetail.FOUNDATION_TYPE_LOC = dtStructureInfo.Rows[0]["FOUNDATION_TYPE_LOC"].ConvertToString();
                        objHouseDetail.GENERAL_COMMENTS = dtStructureInfo.Rows[0]["GENERAL_COMMENTS"].ConvertToString();
                        objHouseDetail.DISTRICT_CD = dtStructureInfo.Rows[0]["DISTRICT_CD"].ConvertToString();
                        //objHouseDetail.GENERAL_COMMENTS_LOC = dtStructureInfo.Rows[0]["GENERAL_COMMENTS_LOC"].ConvertToString();
                        objHouseDetail.NHRS_UUID = dtStructureInfo.Rows[0]["NHRS_UUID"].ConvertToString();
                        objHouseDetail.BATCH_ID = dtStructureInfo.Rows[0]["BATCH_ID"].ToDecimal();
                        ViewData["BUILDING_CONDITION_ENG"] = househead.GetBuildingConditionAfterQuake(objHouseDetail.BUILDING_CONDITION_CD.ConvertToString());
                        ViewData["BUILDING_POSITION_ENG"] = househead.GetHousePosition(objHouseDetail.BUILDING_POSITION_CD.ConvertToString());
                        ViewData["GROUND_SURFACE_ENG"] = househead.GetGroundSurfaceType(objHouseDetail.GROUND_SURFACE_CD.ConvertToString());
                        ViewData["BUILDING_PLAN_ENG"] = househead.GetHousePlan(objHouseDetail.BUILDING_PLAN_CD.ConvertToString());
                        ViewData["RC_MATERIAL_ENG"] = househead.GetRoofMaterial(objHouseDetail.RC_MATERIAL_CD.ConvertToString());
                        ViewData["FC_MATERIAL_ENG"] = househead.GetFloorMaterial(objHouseDetail.FC_MATERIAL_CD.ConvertToString());
                        ViewData["SC_MATERIAL_ENG"] = househead.GetFloorsOtherThanGround(objHouseDetail.SC_MATERIAL_CD.ConvertToString());
                        ViewData["FOUNDATION_TYPE_ENG"] = househead.GetFoundationType(objHouseDetail.FOUNDATION_TYPE_CD.ConvertToString());
                        ViewData["TECHSOLUTION_ENG"] = househead.GetTechnicalSolution(objHouseDetail.TECHSOLUTION_CD.ConvertToString());
                        ViewData["ASSESSED_AREA_ENG"] = househead.GetHouseMonitoredPosition(objHouseDetail.ASSESSED_AREA_CD.ConvertToString());
                        ViewData["RECONSTRUCTION_STARTED"] = (List<SelectListItem>)common.GetYesNo1(objHouseDetail.RECONSTRUCTION_STARTED).Data;
                        ViewData["IS_GEOTECHNICAL_RISK"] = (List<SelectListItem>)common.GetYesNo1(objHouseDetail.IS_GEOTECHNICAL_RISK).Data;
                        ViewData["IS_SECONDARY_USE"] = (List<SelectListItem>)common.GetYesNo1(objHouseDetail.IS_SECONDARY_USE).Data;
                        ViewData["DAMAGE_GRADE_ENG"] = househead.GetDamageGrade(objHouseDetail.DAMAGE_GRADE_CD.ConvertToString());
                        ViewData["HOUSE_LAND_LEGAL_OWNER"] = househead.getHouseholdLegalOwner(objHouseDetail.HOUSE_LAND_LEGAL_OWNER_CD.ConvertToString());


                        #region SuperStructure Checkbox
                        // Super Structure Checkbox tick
                        DataTable dtSuperStructure = objNHRSHouseDetail.SuperStructureofHouse(id2, id);
                        if (dtSuperStructure != null && dtSuperStructure.Rows.Count > 0)
                        {
                            List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT> superStructureList = new List<MIG_NHRS_HH_SUPERSTRUCTURE_MAT>();
                            foreach (DataRow dr in dtSuperStructure.Rows)
                            {
                                MIG_NHRS_HH_SUPERSTRUCTURE_MAT superStructureName = new MIG_NHRS_HH_SUPERSTRUCTURE_MAT();
                                superStructureName.superstructureMatId = Convert.ToDecimal(dr["SUPERSTRUCTURE_MAT_ID"]);
                                superStructureName.superstructureMatEng = dr["SUPERSTRUCTURE_MAT_ENG"].ConvertToString();
                                superStructureName.superstructureMatLoc = dr["SUPERSTRUCTURE_MAT_LOC"].ConvertToString();

                                superStructureList.Add(superStructureName);
                            }
                            objHouseDetail.lstSuperStructureDetail = superStructureList;
                        }
                        #endregion

                        #region Geographical Risk CheckBox

                        // Geographical Risk Checkbox Name tick
                        DataTable dtGeographicalRiskDetail = objNHRSHouseDetail.GeoTechnicalDetail(id2, id);
                        if (dtGeographicalRiskDetail != null && dtGeographicalRiskDetail.Rows.Count > 0)
                        {
                            List<NHRS_GEOTECHNICAL_RISK_TYPE> geographicalRiskDetailList = new List<NHRS_GEOTECHNICAL_RISK_TYPE>();
                            foreach (DataRow dr in dtGeographicalRiskDetail.Rows)
                            {
                                NHRS_GEOTECHNICAL_RISK_TYPE geotechnicalName = new NHRS_GEOTECHNICAL_RISK_TYPE();
                                geotechnicalName.GEOTECHNICAL_RISK_TYPE_CD = Convert.ToDecimal(dr["GEOTECHNICAL_RISK_TYPE_CD"]);
                                geotechnicalName.GEOTECHNICAL_RISK_ENG = dr["GEOTECHNICAL_RISK_ENG"].ConvertToString();
                                geotechnicalName.GEOTECHNICAL_RISK_LOC = dr["GEOTECHNICAL_RISK_LOC"].ConvertToString();

                                geographicalRiskDetailList.Add(geotechnicalName);
                            }
                            objHouseDetail.lstGeoTechnicalDetail = geographicalRiskDetailList;
                        }
                        #endregion

                        #region SecondaryOccupany Checkbox
                        //Secondary Occupancy Checkbox Name Tick
                        DataTable dtSecondaryOccupancyDetail = objNHRSHouseDetail.SecondaryUseDetail(id2, id);
                        if (dtSecondaryOccupancyDetail != null && dtSecondaryOccupancyDetail.Rows.Count > 0)
                        {
                            List<NHRS_SECONDARY_OCCUPANCY> secondaryOccupancyDetailList = new List<NHRS_SECONDARY_OCCUPANCY>();
                            foreach (DataRow dr in dtSecondaryOccupancyDetail.Rows)
                            {
                                NHRS_SECONDARY_OCCUPANCY SecondaryOccupancyDetail = new NHRS_SECONDARY_OCCUPANCY();
                                SecondaryOccupancyDetail.SEC_OCCUPANCY_CD = Convert.ToDecimal(dr["SEC_OCCUPANCY_CD"]);
                                SecondaryOccupancyDetail.SECONDARY_OCCUPANCY_ENG = dr["SECONDARY_OCCUPANCY_ENG"].ConvertToString();
                                SecondaryOccupancyDetail.SECONDARY_OCCUPANCY_LOC = dr["SECONDARY_OCCUPANCY_LOC"].ConvertToString();
                                secondaryOccupancyDetailList.Add(SecondaryOccupancyDetail);
                            }
                            objHouseDetail.lstsecondaryUseDetail = secondaryOccupancyDetailList;
                        }
                        #endregion
                        #region SuperStructure Name
                        // Super Structure Name
                        DataTable dtSuperStructureName = objNHRSHouseDetail.SuperStructureofHouseName();
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
                            objHouseDetail.lstSuperStructureName = superStructureList;
                        }
                        #endregion

                        #region Geographical Risk Name
                        // Geographical Risk Checkbox Name
                        DataTable dtGeographicalRiskName = objNHRSHouseDetail.GeoTechnicalName();
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
                            objHouseDetail.lstGeoTechnicalName = geographicalRiskNameList;
                        }
                        #endregion

                        #region SecondaryOccupany Name
                        //Secondary Occupance
                        DataTable dtSecondaryUse = objNHRSHouseDetail.SecondaryUseName();
                        if (dtSecondaryUse != null && dtSecondaryUse.Rows.Count > 0)
                        {
                            List<NHRS_SECONDARY_OCCUPANCY> secondaryOccupancyList = new List<NHRS_SECONDARY_OCCUPANCY>();
                            foreach (DataRow dr in dtSecondaryUse.Rows)
                            {
                                NHRS_SECONDARY_OCCUPANCY SecondaryOccupancyName = new NHRS_SECONDARY_OCCUPANCY();
                                SecondaryOccupancyName.SEC_OCCUPANCY_CD = Convert.ToDecimal(dr["SEC_OCCUPANCY_CD"]);
                                SecondaryOccupancyName.SECONDARY_OCCUPANCY_ENG = dr["DESC_ENG"].ConvertToString();
                                SecondaryOccupancyName.SECONDARY_OCCUPANCY_LOC = dr["DESC_LOC"].ConvertToString();
                                secondaryOccupancyList.Add(SecondaryOccupancyName);
                            }
                            objHouseDetail.lstsecondaryUseName = secondaryOccupancyList;
                        }
                        #endregion

                        #region Damage Detail Checkbox
                        //// DataTable resultDamageDetail = new DataTable();
                        // resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                        // ViewData["resultDamageDetail"] = resultDamageDetail;
                        DataSet damageAssessmentOfHouse = new DataSet();
                        damageAssessmentOfHouse = objNHRSHouseDetail.GetBuildingAssessmentDetail(id2, id);
                        if (damageAssessmentOfHouse != null && damageAssessmentOfHouse.Tables.Count > 0)
                        {
                            DataTable dtDamageDetail = damageAssessmentOfHouse.Tables["Table1"];
                            List<string> lstDamageChkBoxes = new List<string>();
                            string damageLevelStr = String.Empty;
                            foreach (DataRow dr in dtDamageDetail.Rows)
                            {
                                damageLevelStr = dr["DAMAGE_LEVEL_CD"].ConvertToString() == "1" ? "SE" : damageLevelStr;
                                damageLevelStr = dr["DAMAGE_LEVEL_CD"].ConvertToString() == "2" ? "MH" : damageLevelStr;
                                damageLevelStr = dr["DAMAGE_LEVEL_CD"].ConvertToString() == "3" ? "IN" : damageLevelStr;
                                damageLevelStr = dr["DAMAGE_LEVEL_CD"].ConvertToString() == "10" ? "NO" : damageLevelStr;
                                damageLevelStr = dr["DAMAGE_LEVEL_CD"].ConvertToString() == "11" ? "NA" : damageLevelStr;
                                if (damageLevelStr != "NA" && damageLevelStr != "NO")
                                {
                                    lstDamageChkBoxes.Add("chkDamageDet_" + dr["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr + "_" + dr["DAMAGE_LEVEL_DTL_CD"].ConvertToString());
                                }
                                else
                                {
                                    lstDamageChkBoxes.Add("chkDamageDet_" + dr["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr);
                                }
                            }
                            ViewData["lstDamageCheckBox"] = lstDamageChkBoxes;
                        }
                        #endregion
                    }

                    DataTable result = new DataTable();
                    result = objHousehold.GetStructureFamilyDetail(id, id2);
                    ViewData["result"] = result;
                    objHouseDetail.editMode = "Edit";
                    ViewBag.EditMode = 'Y';
                    objHouseDetail.HOUSE_OWNER_ID = id2;
                    return View("NHRSInterviewHouseDetail", objHouseDetail);
                }
                else
                {
                    #region SuperStructure Name
                    // Super Structure Name
                    DataTable dtSuperStructureName = objNHRSHouseDetail.SuperStructureofHouseName();
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
                        objHouseDetail.lstSuperStructureName = superStructureList;
                    }
                    #endregion

                    #region Geographical Risk Name
                    // Geographical Risk Checkbox Name
                    DataTable dtGeographicalRiskName = objNHRSHouseDetail.GeoTechnicalName();
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
                        objHouseDetail.lstGeoTechnicalName = geographicalRiskNameList;
                    }
                    #endregion

                    #region SecondaryOccupany Name
                    //Secondary Occupance
                    DataTable dtSecondaryUse = objNHRSHouseDetail.SecondaryUseName();
                    if (dtSecondaryUse != null && dtSecondaryUse.Rows.Count > 0)
                    {
                        List<NHRS_SECONDARY_OCCUPANCY> secondaryOccupancyList = new List<NHRS_SECONDARY_OCCUPANCY>();
                        foreach (DataRow dr in dtSecondaryUse.Rows)
                        {
                            NHRS_SECONDARY_OCCUPANCY SecondaryOccupancyName = new NHRS_SECONDARY_OCCUPANCY();
                            SecondaryOccupancyName.SEC_OCCUPANCY_CD = Convert.ToDecimal(dr["SEC_OCCUPANCY_CD"]);
                            SecondaryOccupancyName.SECONDARY_OCCUPANCY_ENG = dr["DESC_ENG"].ConvertToString();
                            SecondaryOccupancyName.SECONDARY_OCCUPANCY_LOC = dr["DESC_LOC"].ConvertToString();
                            secondaryOccupancyList.Add(SecondaryOccupancyName);
                        }
                        objHouseDetail.lstsecondaryUseName = secondaryOccupancyList;
                    }
                    #endregion

                    #region Damage Detail Checkbox
                    //// DataTable resultDamageDetail = new DataTable();
                    // resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                    // ViewData["resultDamageDetail"] = resultDamageDetail;
                    #endregion

                    ViewData["BUILDING_CONDITION_ENG"] = househead.GetBuildingConditionAfterQuake("");
                    ViewData["BUILDING_POSITION_ENG"] = househead.GetHousePosition("");
                    ViewData["GROUND_SURFACE_ENG"] = househead.GetGroundSurfaceType("");
                    ViewData["BUILDING_PLAN_ENG"] = househead.GetHousePlan("");
                    ViewData["RC_MATERIAL_ENG"] = househead.GetRoofMaterial("");
                    ViewData["FC_MATERIAL_ENG"] = househead.GetFloorMaterial("");
                    ViewData["SC_MATERIAL_ENG"] = househead.GetFloorsOtherThanGround("");
                    ViewData["FOUNDATION_TYPE_ENG"] = househead.GetFoundationType("");
                    ViewData["TECHSOLUTION_ENG"] = househead.GetTechnicalSolution("");
                    ViewData["ASSESSED_AREA_ENG"] = househead.GetHouseMonitoredPosition("");
                    ViewData["RECONSTRUCTION_STARTED"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["IS_GEOTECHNICAL_RISK"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["IS_SECONDARY_USE"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["DAMAGE_GRADE_ENG"] = househead.GetDamageGrade("");
                    ViewData["HOUSE_LAND_LEGAL_OWNER"] = househead.getHouseholdLegalOwner("");
                    string userCode = string.Empty;
                    userCode = SessionCheck.getSessionUserCode();
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
            return View(objHouseDetail);
        }

        [HttpPost]
        public ActionResult NHRSInterviewHouseDetail(NHRS_HOUSE_BUILDING_DTL objHouseDetail, FormCollection fc)
        {
            GetDropDown();
            CheckPermission();
            NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
            Dictionary<string, string> lstChkSuperStruct = new Dictionary<string, string>();
            Dictionary<string, string> lstChkDamageDet = new Dictionary<string, string>();
            Dictionary<string, string> lstChkGeoTech = new Dictionary<string, string>();
            Dictionary<string, string> lstChkSecUse = new Dictionary<string, string>();
            try
            {
                objHouseDetail.HOUSE_LAND_LEGAL_OWNER_CD = objHouseDetail.HOUSE_LAND_LEGAL_OWNER_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.HOUSE_LAND_LEGAL_OWNER_CD.ConvertToString(), "NHRS_HOUSE_LAND_LEGAL_OWNER", "DEFINED_CD", "HOUSE_LAND_LEGAL_OWNERCD") : null;
                objHouseDetail.BUILDING_CONDITION_CD = objHouseDetail.BUILDING_CONDITION_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.BUILDING_CONDITION_CD.ConvertToString(), "NHRS_BUILDING_CONDITION", "BUILDING_CONDITION_DEF_CD", "BUILDING_CONDITION_CD") : null;
                objHouseDetail.GROUND_SURFACE_CD = objHouseDetail.GROUND_SURFACE_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.GROUND_SURFACE_CD.ConvertToString(), "NHRS_GROUND_SURFACE", "GROUND_SURFACE_DEF_CD", "GROUND_SURFACE_CD") : null;
                objHouseDetail.FOUNDATION_TYPE_CD = objHouseDetail.FOUNDATION_TYPE_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.FOUNDATION_TYPE_CD.ConvertToString(), "NHRS_FOUNDATION_TYPE", "FOUNDATION_TYPE_DEF_CD", "FOUNDATION_TYPE_CD") : null;
                objHouseDetail.RC_MATERIAL_CD = objHouseDetail.RC_MATERIAL_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.RC_MATERIAL_CD.ConvertToString(), "NHRS_ROOF_CONSTRUCT_MATERIAL", "RC_MATERIAL_DEF_CD", "RC_MATERIAL_CD") : null;
                objHouseDetail.FC_MATERIAL_CD = objHouseDetail.FC_MATERIAL_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.FC_MATERIAL_CD.ConvertToString(), "NHRS_FLOOR_CONSTRUCT_MATERIAL", "FC_MATERIAL_DEF_CD", "FC_MATERIAL_CD") : null;
                objHouseDetail.SC_MATERIAL_CD = objHouseDetail.SC_MATERIAL_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.SC_MATERIAL_CD.ConvertToString(), "NHRS_STOREY_CONSRUCT_MATERIAL", "SC_MATERIAL_DEF_CD", "SC_MATERIAL_CD") : null;
                objHouseDetail.BUILDING_POSITION_CD = objHouseDetail.BUILDING_POSITION_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.BUILDING_POSITION_CD.ConvertToString(), "NHRS_BUILDING_POSITION_CONFIG", "BUILDING_POSITION_DEF_CD", "BUILDING_POSITION_CD") : null;
                objHouseDetail.BUILDING_PLAN_CD = objHouseDetail.BUILDING_PLAN_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.BUILDING_PLAN_CD.ConvertToString(), "NHRS_BUILDING_PLAN_CONFIG", "BUILDING_PLAN_DEF_CD", "BUILDING_PLAN_CD") : null;
                objHouseDetail.ASSESSED_AREA_CD = objHouseDetail.ASSESSED_AREA_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.ASSESSED_AREA_CD.ConvertToString(), "NHRS_ASSESSED_AREA", "ASSESSED_AREA_DEF_CD", "ASSESSED_AREA_CD") : null;
                objHouseDetail.DAMAGE_GRADE_CD = objHouseDetail.DAMAGE_GRADE_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.DAMAGE_GRADE_CD.ConvertToString(), "NHRS_DAMAGE_GRADE", "DAMAGE_GRADE_DEF_CD", "DAMAGE_GRADE_CD") : null;
                objHouseDetail.TECHSOLUTION_CD = objHouseDetail.TECHSOLUTION_CD.ConvertToString() != "" ? common.GetValueFromDataBase(objHouseDetail.TECHSOLUTION_CD.ConvertToString(), "NHRS_TECHNICAL_SOLUTION", "TECHSOLUTION_DEF_CD", "TECHSOLUTION_CD") : null;
                dynamic keysSuperStruct = fc.AllKeys.Where(s => s.IndexOf("chkSuperStruct") >= 0);
                dynamic keysDamageDet = fc.AllKeys.Where(s => s.IndexOf("chkDamageDet") >= 0);
                dynamic keysGeoTech = fc.AllKeys.Where(s => s.IndexOf("chkGeoTech") >= 0);
                dynamic keysSecUse = fc.AllKeys.Where(s => s.IndexOf("chkSecUse") >= 0);
                foreach (var item in keysSuperStruct)
                {
                    lstChkSuperStruct.Add(item, fc[item] != "false" ? "Y" : "N");
                }
                foreach (var item in keysDamageDet)
                {
                    lstChkDamageDet.Add(item, fc[item] != "false" ? "Y" : "N");
                }
                foreach (var item in keysGeoTech)
                {
                    lstChkGeoTech.Add(item, fc[item] != "false" ? "Y" : "N");
                }
                foreach (var item in keysSecUse)
                {
                    lstChkSecUse.Add(item, fc[item] != "false" ? "Y" : "N");
                }
                objNHRSHouseDetail.UpdateNHRSInterviewHouseDetail(objHouseDetail, lstChkSuperStruct, lstChkDamageDet, lstChkGeoTech, lstChkSecUse);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("HouseHoldInfoSearch","NHRP");
        }       

        public ActionResult SocioEconomicDetail(string p)
        {
            GetDropDown();
            CheckPermission();
            NHRS_HOUSE_BUILDING_DTL objHouseDetail = new NHRS_HOUSE_BUILDING_DTL();
            NHRS_HOUSEHOLD_MEMBER houseHoldMemberObj = new NHRS_HOUSEHOLD_MEMBER();
            NHRSInterviewService objNHRS = new NHRSInterviewService();
            NHRSSocioEconomicDetailService objHousehold = new NHRSSocioEconomicDetailService();
            HouseBuildingDetailViewService objHouseholdDetail = new HouseBuildingDetailViewService();
            DataTable dt = new DataTable();
            string id = string.Empty;
            string id2 = string.Empty;
            string id3 = string.Empty;
            string id4 = string.Empty;
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }
                        if (rvd["id2"] != null)
                        {
                            id2 = (rvd["id2"].ConvertToString());
                        }
                        if (rvd["id3"] != null)
                        {
                            id3 = (rvd["id3"].ConvertToString());
                        }
                    }
                }
                DataTable dthouseid = objNHRS.getHouseID(id2);
                if (dthouseid != null && dthouseid.Rows.Count > 0)
                {
                    objHouseDetail.HOUSE_ID = dthouseid.Rows[0]["DEFINED_CD"].ConvertToString();
                }
                if (id != "" && id != null && id2 != "" && id2 != null && id3 != "" && id3 != null)
                {
                    DataSet ds = objHousehold.GetHouseholdFamilyDetail(id2, id3,id);
                    if (ds != null)
                    {
                        #region Household Detail
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {

                            //houseHoldMemberObj.HOUSEHOLD_ID = id;
                            houseHoldMemberObj.MEMBER_ID = ds.Tables[0].Rows[0]["MEMBER_ID"].ConvertToString();
                            houseHoldMemberObj.MEMBER_DEFINED_CD = ds.Tables[0].Rows[0]["MEMBER_DEFINED_CD"].ConvertToString();
                            houseHoldMemberObj.HOUSE_OWNER_ID = id2;
                            houseHoldMemberObj.BUILDING_STRUCTURE_NO = id3;
                            houseHoldMemberObj.HOUSEHOLD_ID = ds.Tables[0].Rows[0]["HOUSEHOLD_ID"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_DEFINED_CD = ds.Tables[0].Rows[0]["HOUSEHOLD_DEFINED_CD"].ConvertToString();

                            houseHoldMemberObj.FIRST_NAME_ENG = ds.Tables[0].Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.MIDDLE_NAME_ENG = ds.Tables[0].Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.LAST_NAME_ENG = ds.Tables[0].Rows[0]["LAST_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.FULL_NAME_ENG = ds.Tables[0].Rows[0]["FULL_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.FIRST_NAME_LOC = ds.Tables[0].Rows[0]["FIRST_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.MIDDLE_NAME_LOC = ds.Tables[0].Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.LAST_NAME_LOC = ds.Tables[0].Rows[0]["LAST_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.FULL_NAME_LOC = ds.Tables[0].Rows[0]["FULL_NAME_LOC"].ConvertToString();

                            houseHoldMemberObj.PER_COUNTRY_CD = ds.Tables[0].Rows[0]["PER_COUNTRY_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["PER_COUNTRY_CD"].ConvertToString(), "MIS_COUNTRY", "COUNTRY_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.PER_REG_ST_CD = ds.Tables[0].Rows[0]["PER_REG_ST_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["PER_REG_ST_CD"].ConvertToString(), "MIS_REGION_STATE", "REG_ST_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.PER_ZONE_CD = ds.Tables[0].Rows[0]["PER_ZONE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["PER_ZONE_CD"].ConvertToString(), "MIS_ZONE", "ZONE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.PER_DISTRICT_CD = ds.Tables[0].Rows[0]["PER_DISTRICT_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["PER_DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD", "DEFINED_CD") : null;
                            houseHoldMemberObj.PER_VDC_MUN_CD = ds.Tables[0].Rows[0]["PER_VDC_MUN_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["PER_VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD", "DEFINED_CD") : null;
                            houseHoldMemberObj.PER_WARD_NO = ds.Tables[0].Rows[0]["PER_WARD_NO"].ConvertToString();
                            houseHoldMemberObj.PER_AREA_ENG = ds.Tables[0].Rows[0]["PER_AREA_ENG"].ConvertToString();
                            houseHoldMemberObj.PER_AREA_LOC = ds.Tables[0].Rows[0]["PER_AREA_LOC"].ConvertToString();
                            houseHoldMemberObj.PER_DISTRICT_ENG = ds.Tables[0].Rows[0]["PER_DISTRICT_ENG"].ConvertToString();
                            houseHoldMemberObj.PER_DISTRICT_LOC = ds.Tables[0].Rows[0]["PER_DISTRICT_LOC"].ConvertToString();
                            houseHoldMemberObj.PER_COUNTRY_ENG = ds.Tables[0].Rows[0]["PER_COUNTRY_ENG"].ConvertToString();
                            houseHoldMemberObj.PER_COUNTRY_LOC = ds.Tables[0].Rows[0]["PER_COUNTRY_LOC"].ConvertToString();
                            houseHoldMemberObj.PER_REGION_ENG = ds.Tables[0].Rows[0]["PER_REGION_ENG"].ConvertToString();
                            houseHoldMemberObj.PER_REGION_LOC = ds.Tables[0].Rows[0]["PER_REGION_LOC"].ConvertToString();
                            houseHoldMemberObj.PER_ZONE_ENG = ds.Tables[0].Rows[0]["PER_ZONE_ENG"].ConvertToString();
                            houseHoldMemberObj.PER_ZONE_LOC = ds.Tables[0].Rows[0]["PER_ZONE_LOC"].ConvertToString();
                            houseHoldMemberObj.PER_VDC_ENG = ds.Tables[0].Rows[0]["PER_VDC_ENG"].ConvertToString();
                            houseHoldMemberObj.PER_VDC_LOC = ds.Tables[0].Rows[0]["PER_VDC_LOC"].ConvertToString();
                            houseHoldMemberObj.CUR_COUNTRY_CD = ds.Tables[0].Rows[0]["CUR_COUNTRY_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["CUR_COUNTRY_CD"].ConvertToString(), "MIS_COUNTRY", "COUNTRY_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.CUR_REG_ST_CD = ds.Tables[0].Rows[0]["CUR_REG_ST_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["CUR_REG_ST_CD"].ConvertToString(), "MIS_REGION_STATE", "REG_ST_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.MEMBER_PHOTO_ID = ds.Tables[0].Rows[0]["MEMBER_PHOTO_ID"].ConvertToString();
                            houseHoldMemberObj.GENDER_CD = ds.Tables[0].Rows[0]["GENDER_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["GENDER_CD"].ConvertToString(), "MIS_GENDER", "GENDER_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.MARITAL_STATUS_CD = ds.Tables[0].Rows[0]["MARITAL_STATUS_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["MARITAL_STATUS_CD"].ConvertToString(), "MIS_MARITAL_STATUS", "MARITAL_STATUS_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.BIRTH_DT = ds.Tables[0].Rows[0]["BIRTH_DT"].ConvertToString();
                            houseHoldMemberObj.BIRTH_MONTH = ds.Tables[0].Rows[0]["BIRTH_MONTH"].ConvertToString() == "99" ? "0" : ds.Tables[0].Rows[0]["BIRTH_MONTH"].ConvertToString();
                            houseHoldMemberObj.BIRTH_DAY = ds.Tables[0].Rows[0]["BIRTH_DAY"].ConvertToString() == "99" ? "0" : ds.Tables[0].Rows[0]["BIRTH_DAY"].ConvertToString();
                            houseHoldMemberObj.BIRTH_DT_LOC = ds.Tables[0].Rows[0]["BIRTH_DT_LOC"].ConvertToString();
                            houseHoldMemberObj.BIRTH_YEAR = ds.Tables[0].Rows[0]["BIRTH_YEAR"].ConvertToString() == "9999" ? "0" : ds.Tables[0].Rows[0]["BIRTH_YEAR"].ConvertToString();
                            //houseHoldMemberObj.BIRTH_DAY = ds.Tables[0].Rows[0]["BIRTH_DAY"].ConvertToString();
                            houseHoldMemberObj.BIRTH_YEAR_LOC = ds.Tables[0].Rows[0]["BIRTH_YEAR_LOC"].ConvertToString();
                            houseHoldMemberObj.BIRTH_MONTH_LOC = ds.Tables[0].Rows[0]["BIRTH_MONTH_LOC"].ConvertToString();
                            houseHoldMemberObj.BIRTH_DAY_LOC = ds.Tables[0].Rows[0]["BIRTH_DAY_LOC"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_FULL_NAME_ENG = ds.Tables[0].Rows[0]["RESPONDENT_FULL_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_FIRST_NAME = ds.Tables[0].Rows[0]["RESPONDENT_FIRST_NAME"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_FIRST_NAME_LOC = ds.Tables[0].Rows[0]["RESPONDENT_FIRST_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_MIDDLE_NAME = ds.Tables[0].Rows[0]["RESPONDENT_MIDDLE_NAME"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_MIDDLE_NAME_LOC = ds.Tables[0].Rows[0]["RESPONDENT_MIDDLE_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_LAST_NAME = ds.Tables[0].Rows[0]["RESPONDENT_LAST_NAME"].ConvertToString();
                            houseHoldMemberObj.RESPONDENT_LAST_NAME_LOC = ds.Tables[0].Rows[0]["RESPONDENT_LAST_NAME_LOC"].ConvertToString();

                            houseHoldMemberObj.RESPONDENT_GENDER_CD = ds.Tables[0].Rows[0]["RESPONDENT_GENDER_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["RESPONDENT_GENDER_CD"].ConvertToString(), "MIS_GENDER", "GENDER_CD", "DEFINED_CD") : null;

                            houseHoldMemberObj.IDENTIFICATION_TYPE_CD = ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE_CD"].ConvertToString(), "NHRS_IDENTIFICATION_TYPE_CD", "IDENTIFICATION_TYPE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.BANK_CD = ds.Tables[0].Rows[0]["BANK_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["BANK_CD"].ConvertToString(), "NHRS_BANK", "BANK_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.TEL_NO = ds.Tables[0].Rows[0]["TEL_NO"].ConvertToString();
                            houseHoldMemberObj.MOBILE_NO = ds.Tables[0].Rows[0]["MOBILE_NO"].ConvertToString();
                            houseHoldMemberObj.BIRTH_DT = ds.Tables[0].Rows[0]["BIRTH_DT"].ConvertToString();
                            houseHoldMemberObj.BIRTH_DT_LOC = ds.Tables[0].Rows[0]["BIRTH_DT_LOC"].ConvertToString();
                            houseHoldMemberObj.AGE = ds.Tables[0].Rows[0]["AGE"].ConvertToString();
                            houseHoldMemberObj.CASTE_CD = ds.Tables[0].Rows[0]["CASTE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["CASTE_CD"].ConvertToString(), "MIS_CASTE", "CASTE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.CASTE_ENG = ds.Tables[0].Rows[0]["CASTE_ENG"].ConvertToString();
                            houseHoldMemberObj.CASTE_LOC = ds.Tables[0].Rows[0]["CASTE_LOC"].ConvertToString();
                            houseHoldMemberObj.RELIGION_CD = ds.Tables[0].Rows[0]["RELIGION_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["RELIGION_CD"].ConvertToString(), "MIS_RELIGION", "RELIGION_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.RELIGION_ENG = ds.Tables[0].Rows[0]["RELIGION_ENG"].ConvertToString();
                            houseHoldMemberObj.RELIGION_LOC = ds.Tables[0].Rows[0]["RELIGION_LOC"].ConvertToString();
                            houseHoldMemberObj.LITERATE = ds.Tables[0].Rows[0]["LITERATE"].ConvertToString();
                            houseHoldMemberObj.EDUCATION_CD = ds.Tables[0].Rows[0]["EDUCATION_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["EDUCATION_CD"].ConvertToString(), "MIS_CLASS_TYPE", "CLASS_TYPE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.EDUCATION_ENG = ds.Tables[0].Rows[0]["EDUCATION_ENG"].ConvertToString();
                            houseHoldMemberObj.EDUCATION_LOC = ds.Tables[0].Rows[0]["EDUCATION_LOC"].ConvertToString();
                            houseHoldMemberObj.BIRTH_CERTIFICATE = ds.Tables[0].Rows[0]["BIRTH_CERTIFICATE"].ConvertToString();
                            houseHoldMemberObj.BIRTH_CERTIFICATE_NO = ds.Tables[0].Rows[0]["BIRTH_CERTIFICATE_NO"].ConvertToString();
                            houseHoldMemberObj.BIRTH_CER_DISTRICT_CD = ds.Tables[0].Rows[0]["BIRTH_CER_DISTRICT_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["BIRTH_CER_DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD", "DEFINED_CD") : null;
                            houseHoldMemberObj.CTZ_ISSUE = ds.Tables[0].Rows[0]["CTZ_ISSUE"].ConvertToString();
                            houseHoldMemberObj.CTZ_NO = ds.Tables[0].Rows[0]["CTZ_NO"].ConvertToString();

                            houseHoldMemberObj.CTZ_ISSUE_DISTRICT_CD = ds.Tables[0].Rows[0]["CTZ_ISSUE_DISTRICT_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["CTZ_ISSUE_DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD", "DEFINED_CD") : null;
                            houseHoldMemberObj.CTZ_ISS_DISTRICT_ENG = ds.Tables[0].Rows[0]["CTZ_ISS_DISTRICT_ENG"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_YEAR = ds.Tables[0].Rows[0]["CTZ_ISSUE_YEAR"].ConvertToString() == "9999" ? "0" : ds.Tables[0].Rows[0]["CTZ_ISSUE_YEAR"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_MONTH = ds.Tables[0].Rows[0]["CTZ_ISSUE_MONTH"].ConvertToString() == "99" ? "0" : ds.Tables[0].Rows[0]["CTZ_ISSUE_MONTH"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_DAY = ds.Tables[0].Rows[0]["CTZ_ISSUE_DAY"].ConvertToString() == "99" ? "0" : ds.Tables[0].Rows[0]["CTZ_ISSUE_DAY"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_YEAR_LOC = ds.Tables[0].Rows[0]["CTZ_ISSUE_YEAR_LOC"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_MONTH_LOC = ds.Tables[0].Rows[0]["CTZ_ISSUE_MONTH_LOC"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_DAY_LOC = ds.Tables[0].Rows[0]["CTZ_ISSUE_DAY_LOC"].ConvertToString();
                            houseHoldMemberObj.CTZ_ISSUE_DT = ds.Tables[0].Rows[0]["CTZ_ISSUE_DT"].ToDateTime();
                            houseHoldMemberObj.VOTER_ID = ds.Tables[0].Rows[0]["VOTER_ID"].ConvertToString();
                            houseHoldMemberObj.VOTERID_NO = ds.Tables[0].Rows[0]["VOTERID_NO"].ConvertToString();
                            houseHoldMemberObj.VOTERID_DISTRICT_CD = ds.Tables[0].Rows[0]["VOTERID_DISTRICT_CD"].ConvertToString();
                            houseHoldMemberObj.VOTERID_ISSUE_DT = ds.Tables[0].Rows[0]["VOTERID_ISSUE_DT"].ToDateTime();
                            houseHoldMemberObj.VOTERID_ISSUE_DT_LOC = ds.Tables[0].Rows[0]["VOTERID_ISSUE_DT_LOC"].ConvertToString();
                            houseHoldMemberObj.NID_NO = ds.Tables[0].Rows[0]["NID_NO"].ConvertToString();
                            houseHoldMemberObj.NID_DISTRICT_CD = ds.Tables[0].Rows[0]["NID_DISTRICT_CD"].ConvertToString();
                            houseHoldMemberObj.NID_ISSUE_DT = ds.Tables[0].Rows[0]["NID_ISSUE_DT"].ToDateTime();
                            houseHoldMemberObj.NID_ISSUE_DT_LOC = ds.Tables[0].Rows[0]["NID_ISSUE_DT_LOC"].ConvertToString();
                            houseHoldMemberObj.DISABILITY = ds.Tables[0].Rows[0]["DISABILITY"].ConvertToString();
                            houseHoldMemberObj.TEL_NO = ds.Tables[0].Rows[0]["TEL_NO"].ConvertToString();
                            houseHoldMemberObj.MOBILE_NO = ds.Tables[0].Rows[0]["MOBILE_NO"].ConvertToString();
                            houseHoldMemberObj.FAX = ds.Tables[0].Rows[0]["FAX"].ConvertToString();
                            houseHoldMemberObj.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ConvertToString();
                            houseHoldMemberObj.URL = ds.Tables[0].Rows[0]["URL"].ConvertToString();
                            houseHoldMemberObj.PO_BOX_NO = ds.Tables[0].Rows[0]["PO_BOX_NO"].ConvertToString();
                            houseHoldMemberObj.PASSPORT_NO = ds.Tables[0].Rows[0]["PASSPORT_NO"].ConvertToString();
                            houseHoldMemberObj.PASSPORT_ISSUE_DISTRICT = ds.Tables[0].Rows[0]["PASSPORT_ISSUE_DISTRICT"].ConvertToString();
                            houseHoldMemberObj.PRO_FUND_NO = ds.Tables[0].Rows[0]["PRO_FUND_NO"].ConvertToString();
                            houseHoldMemberObj.CIT_NO = ds.Tables[0].Rows[0]["CIT_NO"].ConvertToString();
                            houseHoldMemberObj.PAN_NO = ds.Tables[0].Rows[0]["PAN_NO"].ConvertToString();
                            houseHoldMemberObj.DRIVING_LICENSE_NO = ds.Tables[0].Rows[0]["DRIVING_LICENSE_NO"].ConvertToString();
                            houseHoldMemberObj.DEATH = ds.Tables[0].Rows[0]["DEATH"].ConvertToString();
                            houseHoldMemberObj.MEMBER_REMARKS = ds.Tables[0].Rows[0]["MEMBER_REMARKS"].ConvertToString();
                            houseHoldMemberObj.MEMBER_REMARKS_LOC = ds.Tables[0].Rows[0]["MEMBER_REMARKS_LOC"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_HEAD = ds.Tables[0].Rows[0]["HOUSEHOLD_HEAD"].ConvertToString();
                            houseHoldMemberObj.NID_ISSUE = ds.Tables[0].Rows[0]["NID_ISSUE"].ConvertToString();
                            houseHoldMemberObj.MEMBER_CNT = ds.Tables[0].Rows[0]["MEMBER_CNT"].ConvertToString();
                            houseHoldMemberObj.HOUSE_NO = ds.Tables[0].Rows[0]["HOUSE_NO"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_TEL_NO = ds.Tables[0].Rows[0]["HOUSEHOLD_TEL_NO"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_MOBILE_NO = ds.Tables[0].Rows[0]["HOUSEHOLD_MOBILE_NO"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_FAX = ds.Tables[0].Rows[0]["HOUSEHOLD_FAX"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_EMAIL = ds.Tables[0].Rows[0]["HOUSEHOLD_EMAIL"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_URL = ds.Tables[0].Rows[0]["HOUSEHOLD_URL"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_PO_BOX_NO = ds.Tables[0].Rows[0]["HOUSEHOLD_PO_BOX_NO"].ConvertToString();
                            houseHoldMemberObj.DEATH_IN_A_YEAR = ds.Tables[0].Rows[0]["DEATH_IN_A_YEAR"].ConvertToString();
                            houseHoldMemberObj.SOCIAL_ALLOWANCE = ds.Tables[0].Rows[0]["SOCIAL_ALLOWANCE"].ConvertToString();
                            houseHoldMemberObj.ALLOWANCE_TYPE_CD = ds.Tables[0].Rows[0]["ALLOWANCE_TYPE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["ALLOWANCE_TYPE_CD"].ConvertToString(), "MIS_ALLOWANCE_TYPE", "ALLOWANCE_TYPE_CD", "DEFINED_CD") : null;
                            houseHoldMemberObj.ALLOWANCE_TYPE_ENG = ds.Tables[0].Rows[0]["ALLOWANCE_TYPE_ENG"].ConvertToString();
                            houseHoldMemberObj.ALLOWANCE_TYPE_LOC = ds.Tables[0].Rows[0]["ALLOWANCE_TYPE_LOC"].ConvertToString();
                            houseHoldMemberObj.SOCIAL_ALL_ISS_DISTRICT = ds.Tables[0].Rows[0]["SOCIAL_ALL_ISS_DISTRICT"].ConvertToString();
                            houseHoldMemberObj.IDENTIFICATION_TYPE_CD = ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE_CD"].ConvertToString(), "NHRS_IDENTIFICATION_TYPE", "IDENTIFICATION_TYPE_CD", "DEFINED_CD") : null;
                            houseHoldMemberObj.IDENTIFICATION_TYPE_ENG = ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE_ENG"].ConvertToString();
                            houseHoldMemberObj.IDENTIFICATION_TYPE_LOC = ds.Tables[0].Rows[0]["IDENTIFICATION_TYPE_LOC"].ConvertToString();
                            houseHoldMemberObj.IDENTIFICATION_OTHERS = ds.Tables[0].Rows[0]["IDENTIFICATION_OTHERS"].ConvertToString();
                            houseHoldMemberObj.IDENTIFICATION_OTHERS_LOC = ds.Tables[0].Rows[0]["IDENTIFICATION_OTHERS_LOC"].ConvertToString();
                            houseHoldMemberObj.FOREIGN_HOUSEHEAD_COUNTRY_ENG = ds.Tables[0].Rows[0]["FOREIGN_HOUSEHEAD_COUNTRY_ENG"].ConvertToString();
                            houseHoldMemberObj.FOREIGN_HOUSEHEAD_COUNTRY_LOC = ds.Tables[0].Rows[0]["FOREIGN_HOUSEHEAD_COUNTRY_LOC"].ConvertToString();
                            houseHoldMemberObj.BANK_ACCOUNT_FLAG = ds.Tables[0].Rows[0]["BANK_ACCOUNT_FLAG"].ConvertToString();// == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                            houseHoldMemberObj.BANK_NAME_ENG = ds.Tables[0].Rows[0]["BANK_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.BANK_NAME_LOC = ds.Tables[0].Rows[0]["BANK_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.BANK_BRANCH_CD = ds.Tables[0].Rows[0]["BANK_BRANCH_CD"].ConvertToString();
                            houseHoldMemberObj.BANK_BRANCH_NAME_ENG = ds.Tables[0].Rows[0]["BANK_BRANCH_NAME_ENG"].ConvertToString();
                            houseHoldMemberObj.BANK_BRANCH_NAME_LOC = ds.Tables[0].Rows[0]["BANK_BRANCH_NAME_LOC"].ConvertToString();
                            houseHoldMemberObj.HH_MEMBER_BANK_ACC_NO = ds.Tables[0].Rows[0]["HH_MEMBER_BANK_ACC_NO"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_REMARKS = ds.Tables[0].Rows[0]["HOUSEHOLD_REMARKS"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_REMARKS_LOC = ds.Tables[0].Rows[0]["HOUSEHOLD_REMARKS_LOC"].ConvertToString();
                            houseHoldMemberObj.SHELTER_SINCE_QUAKE_CD = ds.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_CD"].ConvertToString(), "NHRS_SHELTER_SINCE_QUAKE", "SHELTER_SINCE_QUAKE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.SHELTER_SINCE_QUAKE_ENG = ds.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_ENG"].ConvertToString();
                            houseHoldMemberObj.SHELTER_SINCE_QUAKE_LOC = ds.Tables[0].Rows[0]["SHELTER_SINCE_QUAKE_LOC"].ConvertToString();
                            houseHoldMemberObj.SHELTER_SINCE_QUAKE_CD = ds.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_CD"].ConvertToString(), "NHRS_SHELTER_BEFORE_QUAKE", "SHELTER_BEFORE_QUAKE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.SHELTER_BEFORE_QUAKE_ENG = ds.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_ENG"].ConvertToString();
                            houseHoldMemberObj.SHELTER_BEFORE_QUAKE_LOC = ds.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_LOC"].ConvertToString();
                            houseHoldMemberObj.SHELTER_BEFORE_QUAKE_CD = ds.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["SHELTER_BEFORE_QUAKE_CD"].ConvertToString(), "NHRS_SHELTER_BEFORE_QUAKE", "SHELTER_BEFORE_QUAKE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.CURRENT_SHELTER_CD = ds.Tables[0].Rows[0]["CURRENT_SHELTER_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["CURRENT_SHELTER_CD"].ConvertToString(), "NHRS_SHELTER_BEFORE_QUAKE", "SHELTER_BEFORE_QUAKE_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.CURRENT_SHELTER_ENG = ds.Tables[0].Rows[0]["CURRENT_SHELTER_ENG"].ConvertToString();
                            houseHoldMemberObj.CURRENT_SHELTER_LOC = ds.Tables[0].Rows[0]["CURRENT_SHELTER_LOC"].ConvertToString();
                            houseHoldMemberObj.EQ_VICTIM_IDENTITY_CARD = ds.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD"].ConvertToString();
                            houseHoldMemberObj.EQ_VICTIM_IDENTITY_CARD_CD = ds.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_CD"].ConvertToString(), "NHRS_EQ_VICTIM_IDENTITY_CARD", "EQ_VICTIM_IDENTITY_CARD_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.EQ_VICTIM_IDENTITY_CARD_ENG = ds.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_ENG"].ConvertToString();
                            houseHoldMemberObj.EQ_VICTIM_IDENTITY_CARD_LOC = ds.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_LOC"].ConvertToString();
                            houseHoldMemberObj.EQ_VICTIM_IDENTITY_CARD_NO = ds.Tables[0].Rows[0]["EQ_VICTIM_IDENTITY_CARD_NO"].ConvertToString();
                            houseHoldMemberObj.MONTHLY_INCOME_CD = ds.Tables[0].Rows[0]["MONTHLY_INCOME_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(ds.Tables[0].Rows[0]["MONTHLY_INCOME_CD"].ConvertToString(), "NHRS_MONTHLY_INCOME", "MONTHLY_INCOME_CD", "DEFINED_CD") : null;
                            ;
                            houseHoldMemberObj.HOUSEHOLD_REMARKS_LOC = ds.Tables[0].Rows[0]["HOUSEHOLD_REMARKS_LOC"].ConvertToString();
                            houseHoldMemberObj.MONTHLY_INCOME_ENG = ds.Tables[0].Rows[0]["MONTHLY_INCOME_ENG"].ConvertToString();
                            houseHoldMemberObj.MONTHLY_INCOME_LOC = ds.Tables[0].Rows[0]["MONTHLY_INCOME_LOC"].ConvertToString();
                            houseHoldMemberObj.DEATH_IN_A_YEAR = ds.Tables[0].Rows[0]["DEATH_IN_A_YEAR"].ConvertToString();
                            houseHoldMemberObj.DEATH_CNT = ds.Tables[0].Rows[0]["DEATH_CNT"].ConvertToString();
                            houseHoldMemberObj.HUMAN_DESTROY_FLAG = ds.Tables[0].Rows[0]["HUMAN_DESTROY_FLAG"].ConvertToString();
                            houseHoldMemberObj.HUMAN_DESTROY_CNT = ds.Tables[0].Rows[0]["HUMAN_DESTROY_CNT"].ConvertToString();
                            houseHoldMemberObj.STUDENT_SCHOOL_LEFT = ds.Tables[0].Rows[0]["STUDENT_SCHOOL_LEFT"].ConvertToString();
                            houseHoldMemberObj.PREGNANT_REGULAR_CHECKUP = ds.Tables[0].Rows[0]["PREGNANT_REGULAR_CHECKUP"].ConvertToString();
                            houseHoldMemberObj.PREGNANT_REGULAR_CHECKUP_CNT = ds.Tables[0].Rows[0]["PREGNANT_REGULAR_CHECKUP_CNT"].ConvertToString();
                            houseHoldMemberObj.CHILD_LEFT_VACINATION = ds.Tables[0].Rows[0]["CHILD_LEFT_VACINATION"].ConvertToString();
                            houseHoldMemberObj.CHILD_LEFT_VACINATION_CNT = ds.Tables[0].Rows[0]["CHILD_LEFT_VACINATION_CNT"].ConvertToString();
                            houseHoldMemberObj.LEFT_CHANGE_OCCUPANY = ds.Tables[0].Rows[0]["LEFT_CHANGE_OCCUPANY"].ConvertToString();
                            houseHoldMemberObj.LEFT_CHANGE_OCCUPANY_CNT = ds.Tables[0].Rows[0]["LEFT_CHANGE_OCCUPANY_CNT"].ConvertToString();
                            houseHoldMemberObj.HOUSEHOLD_ACTIVE = ds.Tables[0].Rows[0]["HOUSEHOLD_ACTIVE"].ConvertToString();
                            ViewData["ddl_RespondentGender"] = common.GetGender(houseHoldMemberObj.RESPONDENT_GENDER_CD.ConvertToString());

                            houseHoldMemberObj.RESPONDENT_RELATION_WITH_HH = ds.Tables[0].Rows[0]["RESPONDENT_RELATION_WITH_HH"].ConvertToString();

                            ViewData["GENDER_ENG"] = common.GetGender(houseHoldMemberObj.GENDER_CD.ConvertToString());
                            ViewData["IS_RESPONDENT_HOUSEHEAD"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.IS_RESPONDENT_HOUSEHEAD.ConvertToString()).Data;
                            ViewData["RESPONDENT_RELATION_WITH_HH_ENG"] = common.GetRelation(houseHoldMemberObj.RESPONDENT_RELATION_WITH_HH.ConvertToString());
                            ViewData["IDENTIFICATION_TYPE_ENG"] = househead.GetIdentificationType(houseHoldMemberObj.IDENTIFICATION_TYPE_CD.ConvertToString());
                            ViewData["CTZ_ISS_DISTRICT_ENG"] = common.GetDistricts(houseHoldMemberObj.CTZ_ISSUE_DISTRICT_CD.ConvertToString());
                            ViewData["CASTE_ENG"] = common.GetCaste(houseHoldMemberObj.CASTE_CD.ConvertToString());
                            ViewData["EDUCATION_ENG"] = common.GetEducation(houseHoldMemberObj.EDUCATION_CD.ConvertToString());
                            ViewData["BANK_ACCOUNT_FLAG"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.BANK_ACCOUNT_FLAG).Data;
                            ViewData["BANK_NAME_ENG"] = househead.GetAllBank(houseHoldMemberObj.BANK_CD.ConvertToString());
                            ViewData["BANK_BRANCH_NAME_ENG"] = househead.GetAllBankBranch(houseHoldMemberObj.BANK_BRANCH_CD.ConvertToString());
                            ViewData["SHELTER_SINCE_QUAKE_ENG"] = househead.getShelterSinceEQ(houseHoldMemberObj.SHELTER_SINCE_QUAKE_CD.ConvertToString());
                            ViewData["SHELTER_BEFORE_QUAKE_ENG"] = househead.getShelterBeforeEQ(houseHoldMemberObj.SHELTER_BEFORE_QUAKE_CD.ConvertToString());
                            ViewData["CURRENT_SHELTER_ENG"] = househead.getShelterBeforeEQ(houseHoldMemberObj.SHELTER_BEFORE_QUAKE_CD.ConvertToString());
                            ViewData["EQ_VICTIM_IDENTITY_CARD_ENG"] = househead.getEQVictimIdentityCard(houseHoldMemberObj.EQ_VICTIM_IDENTITY_CARD_CD.ConvertToString());
                            ViewData["MONTHLY_INCOME_ENG"] = househead.GetMonthlyIncome(houseHoldMemberObj.MONTHLY_INCOME_CD.ConvertToString());
                            ViewData["DEATH_IN_A_YEAR"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.DEATH_IN_A_YEAR).Data;
                            ViewData["HUMAN_DESTROY_FLAG"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.HUMAN_DESTROY_FLAG).Data;
                            ViewData["STUDENT_SCHOOL_LEFT"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.STUDENT_SCHOOL_LEFT).Data;
                            ViewData["PREGNANT_REGULAR_CHECKUP"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.PREGNANT_REGULAR_CHECKUP).Data;
                            ViewData["CHILD_LEFT_VACINATION"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.CHILD_LEFT_VACINATION).Data;
                            ViewData["LEFT_CHANGE_OCCUPANY"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.LEFT_CHANGE_OCCUPANY).Data;
                            ViewData["STUDENT_SCHOOL_LEFT"] = (List<SelectListItem>)common.GetYesNo1(houseHoldMemberObj.STUDENT_SCHOOL_LEFT).Data;
                            ViewData["ddl_SocialAllowance"] = common.GetAllowanceType(houseHoldMemberObj.ALLOWANCE_TYPE_CD.ConvertToString());


                        }
                        #endregion
                        #region Member Detail
                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            List<NHRS_HOUSEHOLD_MEMBER> memberList = new List<NHRS_HOUSEHOLD_MEMBER>();
                            int MemberDetailincrement = 0;
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {

                                NHRS_HOUSEHOLD_MEMBER memberDtl = new NHRS_HOUSEHOLD_MEMBER();
                                memberDtl.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                                memberDtl.HOUSEHOLD_DEFINED_CD = dr["HOUSEHOLD_DEFINED_CD"].ConvertToString();
                                memberDtl.MEMBER_ID = dr["MEMBER_ID"].ConvertToString();
                                memberDtl.MEMBER_DEFINED_CD = dr["MEMBER_DEFINED_CD"].ConvertToString();
                                memberDtl.FIRST_NAME_ENG = dr["FIRST_NAME_ENG"].ConvertToString();
                                memberDtl.MIDDLE_NAME_ENG = dr["MIDDLE_NAME_ENG"].ConvertToString();
                                memberDtl.LAST_NAME_ENG = dr["LAST_NAME_ENG"].ConvertToString();
                                memberDtl.FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                                memberDtl.FIRST_NAME_LOC = dr["FIRST_NAME_LOC"].ConvertToString();
                                memberDtl.MIDDLE_NAME_LOC = dr["MIDDLE_NAME_LOC"].ConvertToString();
                                memberDtl.LAST_NAME_LOC = dr["LAST_NAME_LOC"].ConvertToString();
                                memberDtl.LAST_NAME_LOC = dr["LAST_NAME_LOC"].ConvertToString();
                                memberDtl.PER_COUNTRY_CD = dr["PER_COUNTRY_CD"].ConvertToString();
                                memberDtl.PER_REG_ST_CD = dr["PER_REG_ST_CD"].ConvertToString();
                                memberDtl.PER_ZONE_CD = dr["PER_ZONE_CD"].ConvertToString();
                                memberDtl.PER_DISTRICT_CD = dr["PER_DISTRICT_CD"].ConvertToString();
                                memberDtl.PER_VDC_MUN_CD = dr["PER_VDC_MUN_CD"].ConvertToString();
                                memberDtl.PER_WARD_NO = dr["PER_WARD_NO"].ConvertToString();
                                memberDtl.PER_AREA_ENG = dr["PER_AREA_ENG"].ConvertToString();
                                memberDtl.PER_AREA_LOC = dr["PER_AREA_LOC"].ConvertToString();
                                memberDtl.PER_DISTRICT_ENG = dr["PER_DISTRICT_ENG"].ConvertToString();
                                memberDtl.PER_DISTRICT_LOC = dr["PER_DISTRICT_LOC"].ConvertToString();
                                memberDtl.PER_COUNTRY_ENG = dr["PER_COUNTRY_ENG"].ConvertToString();
                                memberDtl.PER_COUNTRY_LOC = dr["PER_COUNTRY_LOC"].ConvertToString();
                                memberDtl.PER_REGION_ENG = dr["PER_REGION_ENG"].ConvertToString();
                                memberDtl.PER_REGION_LOC = dr["PER_REGION_LOC"].ConvertToString();
                                memberDtl.PER_ZONE_ENG = dr["PER_ZONE_ENG"].ConvertToString();
                                memberDtl.PER_ZONE_LOC = dr["PER_ZONE_LOC"].ConvertToString();
                                memberDtl.PER_VDC_ENG = dr["PER_VDC_ENG"].ConvertToString();
                                memberDtl.PER_VDC_LOC = dr["PER_VDC_LOC"].ConvertToString();
                                memberDtl.CUR_COUNTRY_CD = dr["CUR_COUNTRY_CD"].ConvertToString();
                                memberDtl.CUR_REG_ST_CD = dr["CUR_REG_ST_CD"].ConvertToString();
                                memberDtl.MEMBER_PHOTO_ID = dr["MEMBER_PHOTO_ID"].ConvertToString();
                                memberDtl.GENDER_CD = dr["GENDER_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dr["GENDER_CD"].ConvertToString(), "MIS_GENDER", "GENDER_CD", "DEFINED_CD") : null;
                                ;
                                memberDtl.MARITAL_STATUS_CD = dr["MARITAL_STATUS_CD"].ConvertToString();
                                memberDtl.BIRTH_YEAR = dr["BIRTH_YEAR"].ConvertToString() == "9999" ? "0" : dr["BIRTH_YEAR"].ConvertToString();
                                memberDtl.BIRTH_DAY = dr["BIRTH_DAY"].ConvertToString() == "99" ? "0" : dr["BIRTH_DAY"].ConvertToString();
                                memberDtl.BIRTH_YEAR_LOC = dr["BIRTH_YEAR_LOC"].ConvertToString();
                                memberDtl.BIRTH_MONTH = dr["BIRTH_MONTH"].ConvertToString() == "99" ? "0" : dr["BIRTH_MONTH"].ConvertToString();
                                memberDtl.BIRTH_MONTH_LOC = dr["BIRTH_MONTH_LOC"].ConvertToString();
                                memberDtl.BIRTH_DAY_LOC = dr["BIRTH_DAY_LOC"].ConvertToString();
                                memberDtl.IDENTIFICATION_TYPE_CD = dr["IDENTIFICATION_TYPE_CD"].ConvertToString();
                                memberDtl.BANK_CD = dr["BANK_CD"].ConvertToString();
                                memberDtl.TEL_NO = dr["TEL_NO"].ConvertToString();
                                memberDtl.MOBILE_NO = dr["MOBILE_NO"].ConvertToString();
                                memberDtl.BIRTH_DT = dr["BIRTH_DT"].ConvertToString();
                                memberDtl.BIRTH_DT_LOC = dr["BIRTH_DT_LOC"].ConvertToString();
                                memberDtl.AGE = dr["AGE"].ConvertToString();
                                memberDtl.CASTE_CD = dr["CASTE_CD"].ConvertToString();
                                memberDtl.CASTE_ENG = dr["CASTE_ENG"].ConvertToString();
                                memberDtl.CASTE_LOC = dr["CASTE_LOC"].ConvertToString();
                                memberDtl.RELIGION_CD = dr["RELIGION_CD"].ConvertToString();
                                memberDtl.RELIGION_ENG = dr["RELIGION_ENG"].ConvertToString();
                                memberDtl.RELIGION_LOC = dr["RELIGION_LOC"].ConvertToString();
                                memberDtl.LITERATE = dr["LITERATE"].ConvertToString();

                                memberDtl.EDUCATION_CD = dr["EDUCATION_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dr["EDUCATION_CD"].ConvertToString(), "MIS_CLASS_TYPE", "CLASS_TYPE_CD", "DEFINED_CD") : null;
                                ;
                                memberDtl.EDUCATION_ENG = dr["EDUCATION_ENG"].ConvertToString();
                                memberDtl.EDUCATION_LOC = dr["EDUCATION_LOC"].ConvertToString();
                                memberDtl.BIRTH_CERTIFICATE = dr["BIRTH_CERTIFICATE"].ConvertToString();
                                memberDtl.BIRTH_CERTIFICATE_NO = dr["BIRTH_CERTIFICATE_NO"].ConvertToString();
                                memberDtl.BIRTH_CER_DISTRICT_CD = dr["BIRTH_CER_DISTRICT_CD"].ConvertToString();
                                memberDtl.CTZ_ISSUE = dr["CTZ_ISSUE"].ConvertToString();
                                memberDtl.CTZ_NO = dr["CTZ_NO"].ConvertToString();
                                memberDtl.CTZ_ISSUE_DISTRICT_CD = dr["CTZ_ISSUE_DISTRICT_CD"].ConvertToString();
                                memberDtl.CTZ_ISSUE_YEAR = dr["CTZ_ISSUE_YEAR"].ConvertToString() == "9999" ? "0" : dr["CTZ_ISSUE_YEAR"].ConvertToString();
                                memberDtl.CTZ_ISSUE_MONTH = dr["CTZ_ISSUE_MONTH"].ConvertToString() == "99" ? "0" : dr["CTZ_ISSUE_MONTH"].ConvertToString();
                                memberDtl.CTZ_ISSUE_DAY = dr["CTZ_ISSUE_DAY"].ConvertToString() == "99" ? "0" : dr["CTZ_ISSUE_DAY"].ConvertToString();
                                memberDtl.CTZ_ISSUE_YEAR_LOC = dr["CTZ_ISSUE_YEAR_LOC"].ConvertToString();
                                memberDtl.CTZ_ISSUE_MONTH_LOC = dr["CTZ_ISSUE_MONTH_LOC"].ConvertToString();
                                memberDtl.CTZ_ISSUE_DAY_LOC = dr["CTZ_ISSUE_DAY_LOC"].ConvertToString();
                                memberDtl.CTZ_ISSUE_DT = dr["CTZ_ISSUE_DT"].ToDateTime();
                                memberDtl.VOTER_ID = dr["VOTER_ID"].ConvertToString();
                                memberDtl.VOTERID_NO = dr["VOTERID_NO"].ConvertToString();
                                memberDtl.VOTERID_DISTRICT_CD = dr["VOTERID_DISTRICT_CD"].ConvertToString();
                                memberDtl.VOTERID_ISSUE_DT = dr["VOTERID_ISSUE_DT"].ToDateTime();
                                memberDtl.VOTERID_ISSUE_DT_LOC = dr["VOTERID_ISSUE_DT_LOC"].ConvertToString();
                                memberDtl.NID_NO = dr["NID_NO"].ConvertToString();
                                memberDtl.NID_DISTRICT_CD = dr["NID_DISTRICT_CD"].ConvertToString();
                                memberDtl.NID_ISSUE_DT = dr["NID_ISSUE_DT"].ToDateTime();
                                memberDtl.NID_ISSUE_DT_LOC = dr["NID_ISSUE_DT_LOC"].ConvertToString();
                                memberDtl.DISABILITY = dr["DISABILITY"].ConvertToString();
                                memberDtl.TEL_NO = dr["TEL_NO"].ConvertToString();
                                memberDtl.MOBILE_NO = dr["MOBILE_NO"].ConvertToString();
                                memberDtl.FAX = dr["FAX"].ConvertToString();
                                memberDtl.EMAIL = dr["EMAIL"].ConvertToString();
                                memberDtl.URL = dr["URL"].ConvertToString();
                                memberDtl.PO_BOX_NO = dr["PO_BOX_NO"].ConvertToString();
                                memberDtl.PASSPORT_NO = dr["PASSPORT_NO"].ConvertToString();
                                memberDtl.PASSPORT_ISSUE_DISTRICT = dr["PASSPORT_ISSUE_DISTRICT"].ConvertToString();
                                memberDtl.PRO_FUND_NO = dr["PRO_FUND_NO"].ConvertToString();
                                memberDtl.CIT_NO = dr["CIT_NO"].ConvertToString();
                                memberDtl.PAN_NO = dr["PAN_NO"].ConvertToString();
                                memberDtl.DRIVING_LICENSE_NO = dr["DRIVING_LICENSE_NO"].ConvertToString();
                                memberDtl.DEATH = dr["DEATH"].ConvertToString();
                                memberDtl.MEMBER_REMARKS = dr["MEMBER_REMARKS"].ConvertToString();
                                memberDtl.MEMBER_REMARKS_LOC = dr["MEMBER_REMARKS_LOC"].ConvertToString();
                                memberDtl.HOUSEHOLD_HEAD = dr["HOUSEHOLD_HEAD"].ConvertToString();
                                memberDtl.NID_ISSUE = dr["NID_ISSUE"].ConvertToString();
                                memberDtl.MEMBER_CNT = dr["MEMBER_CNT"].ConvertToString();
                                memberDtl.HOUSE_NO = dr["HOUSE_NO"].ConvertToString();
                                memberDtl.HOUSEHOLD_TEL_NO = dr["HOUSEHOLD_TEL_NO"].ConvertToString();
                                memberDtl.HOUSEHOLD_MOBILE_NO = dr["HOUSEHOLD_MOBILE_NO"].ConvertToString();
                                memberDtl.HOUSEHOLD_FAX = dr["HOUSEHOLD_FAX"].ConvertToString();
                                memberDtl.HOUSEHOLD_EMAIL = dr["HOUSEHOLD_EMAIL"].ConvertToString();
                                memberDtl.HOUSEHOLD_URL = dr["HOUSEHOLD_URL"].ConvertToString();
                                memberDtl.HOUSEHOLD_PO_BOX_NO = dr["HOUSEHOLD_PO_BOX_NO"].ConvertToString();
                                memberDtl.DEATH_IN_A_YEAR = dr["DEATH_IN_A_YEAR"].ConvertToString();
                                memberDtl.SOCIAL_ALLOWANCE = dr["SOCIAL_ALLOWANCE"].ConvertToString();
                                memberDtl.ALLOWANCE_TYPE_CD = dr["ALLOWANCE_TYPE_CD"].ConvertToString() 
                                    != "" ? common.GetValueFromDataBase(dr["ALLOWANCE_TYPE_CD"].ConvertToString(), "MIS_ALLOWANCE_TYPE", "ALLOWANCE_TYPE_CD", "DEFINED_CD") : null;
                                memberDtl.ALLOWANCE_TYPE_ENG = dr["ALLOWANCE_TYPE_ENG"].ConvertToString();
                                memberDtl.ALLOWANCE_TYPE_LOC = dr["ALLOWANCE_TYPE_LOC"].ConvertToString();
                                memberDtl.SOCIAL_ALL_ISS_DISTRICT = dr["SOCIAL_ALL_ISS_DISTRICT"].ConvertToString();
                                memberDtl.IDENTIFICATION_TYPE_CD = dr["IDENTIFICATION_TYPE_CD"].ConvertToString();
                                memberDtl.IDENTIFICATION_TYPE_ENG = dr["IDENTIFICATION_TYPE_ENG"].ConvertToString();
                                memberDtl.IDENTIFICATION_TYPE_LOC = dr["IDENTIFICATION_TYPE_LOC"].ConvertToString();
                                memberDtl.IDENTIFICATION_OTHERS = dr["IDENTIFICATION_OTHERS"].ConvertToString();
                                memberDtl.IDENTIFICATION_OTHERS_LOC = dr["IDENTIFICATION_OTHERS_LOC"].ConvertToString();
                                memberDtl.FOREIGN_HOUSEHEAD_COUNTRY_ENG = dr["FOREIGN_HOUSEHEAD_COUNTRY_ENG"].ConvertToString();
                                memberDtl.FOREIGN_HOUSEHEAD_COUNTRY_LOC = dr["FOREIGN_HOUSEHEAD_COUNTRY_LOC"].ConvertToString();
                                memberDtl.BANK_ACCOUNT_FLAG = dr["BANK_ACCOUNT_FLAG"].ConvertToString();// == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                                memberDtl.HH_MEMBER_BANK_ACC_NO = dr["HH_MEMBER_BANK_ACC_NO"].ConvertToString();
                                memberDtl.HOUSEHOLD_REMARKS = dr["HOUSEHOLD_REMARKS"].ConvertToString();
                                memberDtl.HOUSEHOLD_REMARKS_LOC = dr["HOUSEHOLD_REMARKS_LOC"].ConvertToString();
                                memberDtl.SHELTER_SINCE_QUAKE_CD = dr["SHELTER_SINCE_QUAKE_CD"].ConvertToString();
                                memberDtl.SHELTER_SINCE_QUAKE_ENG = dr["SHELTER_SINCE_QUAKE_ENG"].ConvertToString();
                                memberDtl.SHELTER_SINCE_QUAKE_LOC = dr["SHELTER_SINCE_QUAKE_LOC"].ConvertToString();
                                memberDtl.SHELTER_BEFORE_QUAKE_CD = dr["SHELTER_BEFORE_QUAKE_CD"].ConvertToString();
                                memberDtl.SHELTER_BEFORE_QUAKE_ENG = dr["SHELTER_BEFORE_QUAKE_ENG"].ConvertToString();
                                memberDtl.SHELTER_BEFORE_QUAKE_LOC = dr["SHELTER_BEFORE_QUAKE_LOC"].ConvertToString();
                                memberDtl.CURRENT_SHELTER_CD = dr["CURRENT_SHELTER_CD"].ConvertToString();
                                memberDtl.CURRENT_SHELTER_ENG = dr["CURRENT_SHELTER_ENG"].ConvertToString();
                                houseHoldMemberObj.CURRENT_SHELTER_LOC = dr["CURRENT_SHELTER_LOC"].ConvertToString();
                                memberDtl.EQ_VICTIM_IDENTITY_CARD = dr["EQ_VICTIM_IDENTITY_CARD"].ConvertToString();
                                memberDtl.EQ_VICTIM_IDENTITY_CARD_CD = dr["EQ_VICTIM_IDENTITY_CARD_CD"].ConvertToString();
                                memberDtl.EQ_VICTIM_IDENTITY_CARD_ENG = dr["EQ_VICTIM_IDENTITY_CARD_ENG"].ConvertToString();
                                memberDtl.EQ_VICTIM_IDENTITY_CARD_LOC = dr["EQ_VICTIM_IDENTITY_CARD_LOC"].ConvertToString();
                                memberDtl.EQ_VICTIM_IDENTITY_CARD_NO = dr["EQ_VICTIM_IDENTITY_CARD_NO"].ConvertToString();
                                memberDtl.MONTHLY_INCOME_CD = dr["MONTHLY_INCOME_CD"].ConvertToString();
                                memberDtl.HOUSEHOLD_REMARKS_LOC = dr["HOUSEHOLD_REMARKS_LOC"].ConvertToString();
                                memberDtl.MONTHLY_INCOME_ENG = dr["MONTHLY_INCOME_ENG"].ConvertToString();
                                memberDtl.MONTHLY_INCOME_LOC = dr["MONTHLY_INCOME_LOC"].ConvertToString();
                                memberDtl.DEATH_IN_A_YEAR = dr["DEATH_IN_A_YEAR"].ConvertToString();
                                memberDtl.DEATH_CNT = dr["DEATH_CNT"].ConvertToString();
                                memberDtl.ALLOWANCE_DAY = dr["ALLOWANCE_DAY"].ConvertToString() == "99" ? "0" : dr["ALLOWANCE_DAY"].ConvertToString();
                                memberDtl.ALLOWANCE_MONTH = dr["ALLOWANCE_MONTH"].ConvertToString() == "99" ? "0" : dr["ALLOWANCE_MONTH"].ConvertToString();
                                memberDtl.ALLOWANCE_YEARS = dr["ALLOWANCE_YEARS"].ConvertToString() == "9999" ? "0" : dr["ALLOWANCE_YEARS"].ConvertToString();
                                memberDtl.PRESENCE_STATUS = dr["PRESENCE_STATUS_ENG"].ConvertToString();
                                memberDtl.PRESENCE_STATUS_CD = dr["PRESENCE_STATUS_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dr["PRESENCE_STATUS_CD"].ConvertToString(), "NHRS_PRESENCE_STATUS", "PRESENCE_STATUS_CD", "PRESENCE_STATUS_DEF_CD") : null;
                                memberDtl.MARITAL_STATUS_CD = dr["MARITAL_STATUS_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dr["MARITAL_STATUS_CD"].ConvertToString(), "MIS_MARITAL_STATUS", "MARITAL_STATUS_CD", "DEFINED_CD") : null;
                                ;
                                memberDtl.MARITAL_STATUS_ENG = dr["MARITAL_STATUS_ENG"].ConvertToString();
                                memberDtl.HANDI_COLOR_CD = dr["HANDI_COLOR_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dr["HANDI_COLOR_CD"].ConvertToString(), "MIS_HANDI_COLOR", "HANDI_COLOR_CD", "DEFINED_CD") : null;
                                ;
                                memberDtl.HANDI_COLOR_ENG = dr["HANDI_COLOR_ENG"].ConvertToString();
                                memberDtl.RELATION_CD = dr["RELATION_TYPE_CD"].ConvertToString() != "" ? common.GetValueFromDataBase(dr["RELATION_TYPE_CD"].ConvertToString(), "MIS_RELATION_TYPE", "RELATION_TYPE_CD", "DEFINED_CD") : null;
                                memberDtl.RELATION_ENG = dr["RELATION_ENG"].ConvertToString();
                                ViewData[MemberDetailincrement == 0 ? "ddl_Gender" : "ddl_Gender" + MemberDetailincrement] = common.GetGender(memberDtl.GENDER_CD.ConvertToString());
                                ViewData[MemberDetailincrement == 0 ? "ddl_Relation" : "ddl_Relation" + MemberDetailincrement] = common.GetRelation(memberDtl.RELATION_CD.ConvertToString());
                                ViewData[MemberDetailincrement == 0 ? "ddl_PresenceStatus" : "ddl_PresenceStatus" + MemberDetailincrement] = househead.GetPresenceStatus(memberDtl.PRESENCE_STATUS_CD.ConvertToString());
                                ViewData[MemberDetailincrement == 0 ? "ddl_HandiColor" : "ddl_HandiColor" + MemberDetailincrement] = common.GetHandiColor(memberDtl.HANDI_COLOR_CD.ConvertToString());
                                ViewData[MemberDetailincrement == 0 ? "ddl_BirthCertificate" : "ddl_BirthCertificate" + MemberDetailincrement] = (List<SelectListItem>)common.GetYesNo1(memberDtl.BIRTH_CERTIFICATE).Data;
                                ViewData[MemberDetailincrement == 0 ? "ddl_Education" : "ddl_Education" + MemberDetailincrement] = common.GetEducation(memberDtl.EDUCATION_CD.ConvertToString());
                                ViewData[MemberDetailincrement == 0 ? "ddl_MaritalStatus" : "ddl_MaritalStatus" + MemberDetailincrement] = common.GetMaritalStatus(memberDtl.MARITAL_STATUS_CD.ConvertToString());
                                ViewData[MemberDetailincrement == 0 ? "ddl_SocialAllowance" : "ddl_SocialAllowance" + MemberDetailincrement] = common.GetAllowanceType(memberDtl.ALLOWANCE_TYPE_CD.ConvertToString());
                                MemberDetailincrement++;
                                memberList.Add(memberDtl);

                            }
                            houseHoldMemberObj.MemberDetailInfo = memberList;
                        }
                        #endregion
                        #region Earthquake Relief Fund
                        //Names of Relief Money 
                        DataTable dtReliefMoneyNames = objHousehold.ReliefMoneyNames();
                        if (dtReliefMoneyNames != null && dtReliefMoneyNames.Rows.Count > 0)
                        {
                            List<NHRS_EQ_RELIEF_MONEY> MoneyList = new List<NHRS_EQ_RELIEF_MONEY>();
                            foreach (DataRow dr in dtReliefMoneyNames.Rows)
                            {
                                NHRS_EQ_RELIEF_MONEY MoneyName = new NHRS_EQ_RELIEF_MONEY();
                                MoneyName.EQ_RELIEF_MONEY_CD = Convert.ToDecimal(dr["EQ_RELIEF_MONEY_CD"]);
                                MoneyName.EQ_RELIEF_MONEY_ENG = dr["DESC_ENG"].ConvertToString();
                                MoneyName.EQ_RELIEF_MONEY_LOC = dr["DESC_LOC"].ConvertToString();
                                MoneyList.Add(MoneyName);
                            }
                            houseHoldMemberObj.EarthquakeReliefMoneyName = MoneyList;
                        }
                        //Relief Money  Checkbox
                        if (ds != null && ds.Tables[5].Rows.Count > 0)
                        {
                            List<NHRS_EQ_RELIEF_MONEY> EQMoneyDetailList = new List<NHRS_EQ_RELIEF_MONEY>();
                            foreach (DataRow dr in ds.Tables[5].Rows)
                            {
                                NHRS_EQ_RELIEF_MONEY EQMoneyName = new NHRS_EQ_RELIEF_MONEY();
                                EQMoneyName.EQ_RELIEF_MONEY_CD = Convert.ToDecimal(dr["EQ_RELIEF_MONEY_CD"]);
                                EQMoneyName.EQ_RELIEF_MONEY_ENG = dr["EQ_RELIEF_MONEY_ENG"].ConvertToString();
                                EQMoneyName.EQ_RELIEF_MONEY_LOC = dr["EQ_RELIEF_MONEY_LOC"].ConvertToString();
                                EQMoneyDetailList.Add(EQMoneyName);
                            }
                            houseHoldMemberObj.EarthquakeReliefMoneyDetail = EQMoneyDetailList;
                        }

                        #endregion

                        #region Member Death Detail
                        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        {
                            List<VW_MEMBER_DEATH_DTL> memberDeathList = new List<VW_MEMBER_DEATH_DTL>();
                            int memberDeathCount = 0;
                            foreach (DataRow dr in ds.Tables[3].Rows)
                            {
                                VW_MEMBER_DEATH_DTL memberDeath = new VW_MEMBER_DEATH_DTL();
                                memberDeath.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                                memberDeath.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                                memberDeath.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                                memberDeath.M_DEFINED_CD = dr["M_DEFINED_CD"].ConvertToString();
                                memberDeath.M_SNO = dr["M_SNO"].ToDecimal();
                                memberDeath.M_FIRST_NAME_ENG = dr["M_FIRST_NAME_ENG"].ConvertToString();
                                memberDeath.M_MIDDLE_NAME_ENG = dr["M_MIDDLE_NAME_ENG"].ConvertToString();
                                memberDeath.M_LAST_NAME_ENG = dr["M_LAST_NAME_ENG"].ConvertToString();
                                memberDeath.M_FULL_NAME_ENG = dr["M_FULL_NAME_ENG"].ConvertToString();
                                memberDeath.M_FIRST_NAME_LOC = dr["M_FIRST_NAME_LOC"].ConvertToString();
                                memberDeath.M_MIDDLE_NAME_LOC = dr["M_MIDDLE_NAME_LOC"].ConvertToString();
                                memberDeath.M_LAST_NAME_LOC = dr["M_LAST_NAME_LOC"].ConvertToString();
                                memberDeath.M_FULL_NAME_LOC = dr["M_FULL_NAME_LOC"].ConvertToString();
                                memberDeath.GENDER_CD = dr["GENDER_CD"].ConvertToString();
                                memberDeath.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                                memberDeath.GENDER_LOC = dr["GENDER_LOC"].ConvertToString();
                                memberDeath.MARITAL_STATUS_ENG = dr["MARITAL_STATUS_ENG"].ConvertToString();
                                memberDeath.MARITAL_STATUS_LOC = dr["MARITAL_STATUS_LOC"].ConvertToString();
                                memberDeath.DISTRICT_ENG = dr["DISTRICT_ENG"].ConvertToString();
                                memberDeath.DISTRICT_LOC = dr["DISTRICT_LOC"].ConvertToString();
                                memberDeath.COUNTRY_ENG = dr["COUNTRY_ENG"].ConvertToString();
                                memberDeath.COUNTRY_LOC = dr["COUNTRY_LOC"].ConvertToString();
                                memberDeath.REGION_ENG = dr["REGION_ENG"].ConvertToString();
                                memberDeath.REGION_LOC = dr["REGION_LOC"].ConvertToString();
                                memberDeath.ZONE_ENG = dr["ZONE_ENG"].ConvertToString();
                                memberDeath.ZONE_LOC = dr["ZONE_LOC"].ConvertToString();
                                memberDeath.VDC_ENG = dr["VDC_ENG"].ConvertToString();
                                memberDeath.VDC_LOC = dr["VDC_LOC"].ConvertToString();
                                memberDeath.PER_WARD_NO = dr["PER_WARD_NO"].ToDecimal();
                                memberDeath.PER_AREA_ENG = dr["PER_AREA_ENG"].ConvertToString();
                                memberDeath.PER_AREA_LOC = dr["PER_AREA_LOC"].ConvertToString();
                                memberDeath.DEATH_YEAR = dr["DEATH_YEAR"].ConvertToString() == "9999" ? "0" : dr["DEATH_YEAR"].ConvertToString();
                                memberDeath.DEATH_MONTH = dr["DEATH_MONTH"].ConvertToString() == "99" ? "0" : dr["DEATH_MONTH"].ConvertToString();
                                memberDeath.DEATH_DAY = dr["DEATH_DAY"].ConvertToString() == "99" ? "0" : dr["DEATH_DAY"].ConvertToString();
                                memberDeath.DEATH_YEAR_LOC = dr["DEATH_YEAR_LOC"].ConvertToString();
                                memberDeath.DEATH_MONTH_LOC = dr["DEATH_MONTH_LOC"].ConvertToString();
                                memberDeath.DEATH_DAY_LOC = dr["DEATH_DAY_LOC"].ConvertToString();
                                memberDeath.AGE = dr["AGE"].ConvertToString();
                                memberDeath.DEATH_REASON_CD = dr["DEATH_REASON_CD"].ConvertToString();
                                memberDeath.DEATH_REASON_ENG = dr["DEATH_REASON_ENG"].ConvertToString();
                                memberDeath.DEATH_REASON_LOC = dr["DEATH_REASON_LOC"].ConvertToString();
                                memberDeath.DEATH_CERTIFICATE = dr["DEATH_CERTIFICATE"].ConvertToString() == "Y" ? Utils.ToggleLanguage("Yes", "छ") : Utils.ToggleLanguage("No", "छैन");
                                memberDeath.DEATH_CERTIFICATE_NO = dr["DEATH_CERTIFICATE_NO"].ConvertToString();
                                ViewData[memberDeathCount == 0 ? "ddlDeathGender" : "ddlDeathGender" + memberDeathCount] = common.GetGender(memberDeath.GENDER_CD.ConvertToString());
                                ViewData[memberDeathCount == 0 ? "ddlDeathCertificate" : "ddlDeathCertificate" + memberDeathCount] = (List<SelectListItem>)common.GetYesNo1(dr["DEATH_CERTIFICATE"].ConvertToString()).Data;
                                ViewData[memberDeathCount == 0 ? "ddlDeathReason" : "ddlDeathReason" + memberDeathCount] = common.GetDeathReason(memberDeath.DEATH_REASON_CD.ConvertToString());
                                memberDeathCount++;
                                memberDeathList.Add(memberDeath);
                            }
                            houseHoldMemberObj.MemberDeathInfoList = memberDeathList;
                        }
                        #endregion

                        #region Memebr Destroy Detail
                        if (ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        {
                            List<VW_MEMBER_HUMAN_DISTROY_DTL> MemberDistroyList = new List<VW_MEMBER_HUMAN_DISTROY_DTL>();
                            int memberDistroyCount = 0;
                            foreach (DataRow dr in ds.Tables[4].Rows)
                            {
                                VW_MEMBER_HUMAN_DISTROY_DTL memberDistroy = new VW_MEMBER_HUMAN_DISTROY_DTL();
                                memberDistroy.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                                memberDistroy.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                                memberDistroy.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                                memberDistroy.M_DEFINED_CD = dr["M_DEFINED_CD"].ConvertToString();
                                memberDistroy.M_SNO = dr["M_SNO"].ToDecimal();
                                memberDistroy.M_FIRST_NAME_ENG = dr["M_FIRST_NAME_ENG"].ConvertToString();
                                memberDistroy.M_MIDDLE_NAME_ENG = dr["M_MIDDLE_NAME_ENG"].ConvertToString();
                                memberDistroy.M_LAST_NAME_ENG = dr["M_LAST_NAME_ENG"].ConvertToString();
                                memberDistroy.M_FULL_NAME_ENG = dr["M_FULL_NAME_ENG"].ConvertToString();
                                memberDistroy.M_FIRST_NAME_LOC = dr["M_FIRST_NAME_LOC"].ConvertToString();
                                memberDistroy.M_MIDDLE_NAME_LOC = dr["M_MIDDLE_NAME_LOC"].ConvertToString();
                                memberDistroy.M_LAST_NAME_LOC = dr["M_LAST_NAME_LOC"].ConvertToString();
                                memberDistroy.M_FULL_NAME_LOC = dr["M_FULL_NAME_LOC"].ConvertToString();
                                memberDistroy.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                                memberDistroy.GENDER_LOC = dr["GENDER_LOC"].ConvertToString();
                                memberDistroy.DISTRICT_ENG = dr["DISTRICT_ENG"].ConvertToString();
                                memberDistroy.DISTRICT_LOC = dr["DISTRICT_LOC"].ConvertToString();
                                memberDistroy.COUNTRY_ENG = dr["COUNTRY_ENG"].ConvertToString();
                                memberDistroy.COUNTRY_LOC = dr["COUNTRY_LOC"].ConvertToString();
                                memberDistroy.REGION_ENG = dr["REGION_ENG"].ConvertToString();
                                memberDistroy.REGION_LOC = dr["REGION_LOC"].ConvertToString();
                                memberDistroy.ZONE_ENG = dr["ZONE_ENG"].ConvertToString();
                                memberDistroy.ZONE_LOC = dr["ZONE_LOC"].ConvertToString();
                                memberDistroy.VDC_ENG = dr["VDC_ENG"].ConvertToString();
                                memberDistroy.VDC_LOC = dr["VDC_LOC"].ConvertToString();
                                memberDistroy.PER_WARD_NO = dr["PER_WARD_NO"].ToDecimal();
                                memberDistroy.PER_AREA_ENG = dr["PER_AREA_ENG"].ConvertToString();
                                memberDistroy.PER_AREA_LOC = dr["PER_AREA_LOC"].ConvertToString();
                                memberDistroy.AGE = dr["AGE"].ToDecimal();
                                memberDistroy.HUMAN_DESTROY_ENG = dr["HUMAN_DESTROY_ENG"].ConvertToString();
                                memberDistroy.HUMAN_DESTROY_LOC = dr["HUMAN_DESTROY_LOC"].ConvertToString();
                                ViewData[memberDistroyCount == 0 ? "ddlHumanDestroyGender" : "ddlHumanDestroyGender" + memberDistroyCount] = common.GetGender(memberDistroy.GENDER_CD.ConvertToString());
                                ViewData[memberDistroyCount == 0 ? "ddlHumanDestroyEng" : "ddlHumanDestroyEng" + memberDistroyCount] = common.GetHumanLoss(memberDistroy.HUMAN_DESTROY_CD.ConvertToString());
                                memberDistroyCount++;
                                MemberDistroyList.Add(memberDistroy);
                            }
                            houseHoldMemberObj.HumanDestroyInfoList = MemberDistroyList;
                        }
                        #endregion

                        #region WaterSource
                        Session["WaterSourceId"] = "";
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            houseHoldMemberObj.WATER_SOURCE_CD = common.GetDefinedCodeFromDataBase(ds.Tables[6].Rows[0]["WATER_SOURCE_CD"].ConvertToString(),"MIS_WATER_SOURCE","WATER_SOURCE_CD");
                            Session["WaterSourceId"] = houseHoldMemberObj.WATER_SOURCE_CD.ConvertToString();
                            houseHoldMemberObj.WATER_SOURCE_ENG = ds.Tables[6].Rows[0]["WATER_SOURCE_ENG"].ConvertToString();
                            houseHoldMemberObj.WATER_SOURCE_LOC = ds.Tables[6].Rows[0]["WATER_SOURCE_LOC"].ConvertToString();
                            houseHoldMemberObj.WATER_SOURCE_BEFORE = ds.Tables[6].Rows[0]["WATER_SOURCE_BEFORE"].ConvertToString();
                            houseHoldMemberObj.WATER_SOURCE_AFTER = ds.Tables[6].Rows[0]["WATER_SOURCE_AFTER"].ConvertToString();


                        }
                        ViewData["WATER_SOURCE_ENG"] = househead.GetWaterSource(houseHoldMemberObj.WATER_SOURCE_CD);
                        #endregion
                        #region FuelSource
                        Session["FuelSourceId"] = "";
                        if (ds.Tables[7].Rows.Count > 0)
                        {
                            houseHoldMemberObj.FUEL_SOURCE_CD = common.GetDefinedCodeFromDataBase(ds.Tables[7].Rows[0]["FUEL_SOURCE_CD"].ConvertToString(), "MIS_FUEL_SOURCE", "FUEL_SOURCE_CD");
                            Session["FuelSourceId"] = houseHoldMemberObj.FUEL_SOURCE_CD.ConvertToString();
                            
                            houseHoldMemberObj.FUEL_SOURCE_ENG = ds.Tables[7].Rows[0]["FUEL_SOURCE_ENG"].ConvertToString();
                            houseHoldMemberObj.FUEL_SOURCE_LOC = ds.Tables[7].Rows[0]["FUEL_SOURCE_LOC"].ConvertToString();
                            houseHoldMemberObj.FUEL_SOURCE_BEFORE = ds.Tables[7].Rows[0]["FUEL_SOURCE_BEFORE"].ConvertToString();
                            houseHoldMemberObj.FUEL_SOURCE_AFTER = ds.Tables[7].Rows[0]["FUEL_SOURCE_AFTER"].ConvertToString();

                        }
                        ViewData["FUEL_SOURCE_ENG"] = househead.GetFuelSource(houseHoldMemberObj.FUEL_SOURCE_CD);
                        #endregion
                        #region LightSource
                        Session["LightSourceId"] = "";
                        if (ds.Tables[8].Rows.Count > 0)
                        {
                            houseHoldMemberObj.LIGHT_SOURCE_CD = common.GetDefinedCodeFromDataBase(ds.Tables[8].Rows[0]["LIGHT_SOURCE_CD"].ConvertToString(), "MIS_LIGHT_SOURCE", "LIGHT_SOURCE_CD");
                            Session["LightSourceId"] = houseHoldMemberObj.LIGHT_SOURCE_CD.ConvertToString();
                            houseHoldMemberObj.LIGHT_SOURCE_ENG = ds.Tables[8].Rows[0]["LIGHT_SOURCE_ENG"].ConvertToString();
                            houseHoldMemberObj.LIGHT_SOURCE_LOC = ds.Tables[8].Rows[0]["LIGHT_SOURCE_LOC"].ConvertToString();
                            houseHoldMemberObj.LIGHT_SOURCE_BEFORE = ds.Tables[8].Rows[0]["LIGHT_SOURCE_BEFORE"].ConvertToString();
                            houseHoldMemberObj.LIGHT_SOURCE_AFTER = ds.Tables[8].Rows[0]["LIGHT_SOURCE_AFTER"].ConvertToString();

                        }
                        ViewData["LIGHT_SOURCE_ENG"] = househead.GetLightSource(houseHoldMemberObj.LIGHT_SOURCE_CD);
                        #endregion
                        #region ToiletType
                        Session["ToiletTypeId"] = "";
                        if (ds.Tables[9].Rows.Count > 0)
                        {
                            houseHoldMemberObj.TOILET_TYPE_CD = common.GetDefinedCodeFromDataBase(ds.Tables[9].Rows[0]["TOILET_TYPE_CD"].ConvertToString(), "MIS_TOILET_TYPE", "TOILET_TYPE_CD");
                            
                            Session["ToiletTypeId"] = houseHoldMemberObj.TOILET_TYPE_CD.ConvertToString();
                            houseHoldMemberObj.TOILET_TYPE_ENG = ds.Tables[9].Rows[0]["TOILET_TYPE_ENG"].ConvertToString();
                            houseHoldMemberObj.TOILET_TYPE_LOC = ds.Tables[9].Rows[0]["TOILET_TYPE_LOC"].ConvertToString();
                            houseHoldMemberObj.TOILET_TYPE_BEFORE = ds.Tables[9].Rows[0]["TOILET_TYPE_BEFORE"].ConvertToString();
                            houseHoldMemberObj.TOILET_TYPE_AFTER = ds.Tables[9].Rows[0]["TOILET_TYPE_AFTER"].ConvertToString();

                        }
                        ViewData["TOILET_TYPE_ENG"] = househead.GetToiletType(houseHoldMemberObj.TOILET_TYPE_CD);
                        #endregion

                        #region Household Indicator
                        // names of Household Indicator 
                        DataTable dtHouseIndicatorNames = objHousehold.HouseIndicatorNames();
                        if (dtHouseIndicatorNames != null && dtHouseIndicatorNames.Rows.Count > 0)
                        {
                            List<MIG_MIS_HOUSEHOLD_INDICATOR> IndicatorList = new List<MIG_MIS_HOUSEHOLD_INDICATOR>();
                            foreach (DataRow dr in dtHouseIndicatorNames.Rows)
                            {
                                MIG_MIS_HOUSEHOLD_INDICATOR IndicatorName = new MIG_MIS_HOUSEHOLD_INDICATOR();
                                IndicatorName.indicatorCd = Convert.ToDecimal(dr["INDICATOR_CD"]);
                                IndicatorName.descEng = dr["DESC_ENG"].ConvertToString();
                                IndicatorName.descLoc = dr["DESC_LOC"].ConvertToString();
                                IndicatorName.orderNo = dr["ORDER_NO"].ToDecimal();
                                IndicatorList.Add(IndicatorName);
                            }
                            houseHoldMemberObj.HouseholdIndicatorName = IndicatorList;
                        }
                        // Household Indicator  Checkbox
                        if (ds != null && ds.Tables[10].Rows.Count > 0)
                        {
                            List<MIG_MIS_HOUSEHOLD_INDICATOR> IndicatorDetailList = new List<MIG_MIS_HOUSEHOLD_INDICATOR>();
                            foreach (DataRow dr in ds.Tables[10].Rows)
                            {
                                MIG_MIS_HOUSEHOLD_INDICATOR IndicatorDetail = new MIG_MIS_HOUSEHOLD_INDICATOR();
                                IndicatorDetail.indicatorCd = Convert.ToDecimal(dr["INDICATOR_CD"]);
                                IndicatorDetail.descEng = dr["INDICATOR_ENG"].ConvertToString();
                                IndicatorDetail.descLoc = dr["INDICATOR_LOC"].ConvertToString();
                                IndicatorDetail.INDICATOR_BEFORE = dr["INDICATOR_BEFORE"].ConvertToString();
                                IndicatorDetail.INDICATOR_AFTER = dr["INDICATOR_AFTER"].ConvertToString();
                                IndicatorDetailList.Add(IndicatorDetail);
                            }
                            houseHoldMemberObj.HouseholdIndicatorDetail = IndicatorDetailList;
                        }
                        #endregion
                    }

                    DataTable result = new DataTable();
                    ViewData["result"] = result;
                    houseHoldMemberObj.EDIT_MODE = "Edit";
                    ViewBag.EditMode = 'Y';

                    return View(houseHoldMemberObj);
                }
                else
                {

                    #region Household Indicator
                    // names of Household Indicator 
                    DataTable dtHouseIndicatorNames = objHousehold.HouseIndicatorNames();
                    if (dtHouseIndicatorNames != null && dtHouseIndicatorNames.Rows.Count > 0)
                    {
                        List<MIG_MIS_HOUSEHOLD_INDICATOR> IndicatorList = new List<MIG_MIS_HOUSEHOLD_INDICATOR>();
                        foreach (DataRow dr in dtHouseIndicatorNames.Rows)
                        {
                            MIG_MIS_HOUSEHOLD_INDICATOR IndicatorName = new MIG_MIS_HOUSEHOLD_INDICATOR();
                            IndicatorName.indicatorCd = Convert.ToDecimal(dr["INDICATOR_CD"]);
                            IndicatorName.descEng = dr["DESC_ENG"].ConvertToString();
                            IndicatorName.descLoc = dr["DESC_LOC"].ConvertToString();
                            IndicatorList.Add(IndicatorName);
                        }
                        houseHoldMemberObj.HouseholdIndicatorName = IndicatorList;
                    }
                    #endregion
                    #region Earthquake Relief Fund
                    //Names of Relief Money 
                    DataTable dtReliefMoneyNames = objHousehold.ReliefMoneyNames();
                    if (dtReliefMoneyNames != null && dtReliefMoneyNames.Rows.Count > 0)
                    {
                        List<NHRS_EQ_RELIEF_MONEY> MoneyList = new List<NHRS_EQ_RELIEF_MONEY>();
                        foreach (DataRow dr in dtReliefMoneyNames.Rows)
                        {
                            NHRS_EQ_RELIEF_MONEY MoneyName = new NHRS_EQ_RELIEF_MONEY();
                            MoneyName.EQ_RELIEF_MONEY_CD = Convert.ToDecimal(dr["EQ_RELIEF_MONEY_CD"]);
                            MoneyName.EQ_RELIEF_MONEY_ENG = dr["DESC_ENG"].ConvertToString();
                            MoneyName.EQ_RELIEF_MONEY_LOC = dr["DESC_LOC"].ConvertToString();
                            MoneyList.Add(MoneyName);
                        }
                        houseHoldMemberObj.EarthquakeReliefMoneyName = MoneyList;
                    }
                    #endregion
                    ViewData["WATER_SOURCE_ENG"] = househead.GetWaterSource("");
                    ViewData["LIGHT_SOURCE_ENG"] = househead.GetLightSource("");
                    ViewData["TOILET_TYPE_ENG"] = househead.GetToiletType("");
                    ViewData["FUEL_SOURCE_ENG"] = househead.GetFuelSource("");
                    ViewData["GENDER_ENG"] = common.GetGender("");
                    ViewData["ddl_RespondentGender"] = common.GetGender("");
                    ViewData["ddl_SocialAllowance"] = common.GetAllowanceType("");
                    ViewData["IS_RESPONDENT_HOUSEHEAD"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["RESPONDENT_RELATION_WITH_HH_ENG"] = common.GetRelation("");
                    ViewData["IDENTIFICATION_TYPE_ENG"] = househead.GetIdentificationType("");
                    ViewData["CTZ_ISS_DISTRICT_ENG"] = common.GetDistricts("");
                    ViewData["CASTE_ENG"] = common.GetCaste("");
                    ViewData["EDUCATION_ENG"] = common.GetEducation("");
                    ViewData["BANK_ACCOUNT_FLAG"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["BANK_NAME_ENG"] = househead.GetAllBank("");
                    ViewData["BANK_BRANCH_NAME_ENG"] = househead.GetAllBankBranch("");
                    ViewData["SHELTER_SINCE_QUAKE_ENG"] = househead.getShelterSinceEQ("");
                    ViewData["SHELTER_BEFORE_QUAKE_ENG"] = househead.getShelterBeforeEQ("");
                    ViewData["CURRENT_SHELTER_ENG"] = househead.getShelterBeforeEQ("");
                    ViewData["EQ_VICTIM_IDENTITY_CARD_ENG"] = househead.getEQVictimIdentityCard("");
                    ViewData["MONTHLY_INCOME_ENG"] = househead.GetMonthlyIncome("");
                    ViewData["DEATH_IN_A_YEAR"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["HUMAN_DESTROY_FLAG"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["STUDENT_SCHOOL_LEFT"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["PREGNANT_REGULAR_CHECKUP"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["CHILD_LEFT_VACINATION"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["LEFT_CHANGE_OCCUPANY"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["STUDENT_SCHOOL_LEFT"] = (List<SelectListItem>)common.GetYesNo1("").Data;

                    //Member Details
                    ViewData["MemberDetailInfoList.GENDER_ENG"] = common.GetGender("");
                    ViewData["MemberDetailInfoList.RELATION_ENG"] = common.GetRelation("");
                    ViewData["MemberDetailInfoList.PRESENCE_STATUS_ENG"] = househead.GetPresenceStatus("");
                    ViewData["MemberDetailInfoList.HANDI_COLOR_ENG"] = common.GetHandiColor("");
                    ViewData["MemberDetailInfoList.BIRTH_CERTIFICATE"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["MemberDetailInfoList.EDUCATION_ENG"] = common.GetEducation("");
                    ViewData["MemberDetailInfoList.MARITAL_STATUS_ENG"] = common.GetMaritalStatus("");
                    ViewData["MemberDetailInfoList.SOCIAL_ALLOWANCE_ENG"] = common.GetAllowanceType("");

                    //Death Details
                    ViewData["MemberDeathInfoList.GENDER_ENG"] = common.GetGender("");
                    ViewData["MemberDeathInfoList.DEATH_CERTIFICATE"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                    ViewData["MemberDeathInfoList.DEATH_REASON_ENG"] = common.GetDeathReason("");

                    //Disabled/Missing/Human Destroy
                    ViewData["HumanDestroyInfoList.GENDER_ENG"] = common.GetGender("");
                    ViewData["HumanDestroyInfoList.HUMAN_DESTROY_ENG"] = common.GetHumanLoss("");
                    string userCode = string.Empty;
                    userCode = SessionCheck.getSessionUserCode();
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
            return View(houseHoldMemberObj);
        }

        [HttpPost]
        public ActionResult SocioEconomicDetail(NHRS_HOUSEHOLD_MEMBER householdinfo, FormCollection fc)
        {
            CheckPermission();
            NHRSSocioEconomicDetailService socioService = new NHRSSocioEconomicDetailService();
            try
            {
                #region householdinfo

                householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                householdinfo.HOUSEHOLD_DEFINED_CD = fc["HOUSEHOLD_DEFINED_CD"].ConvertToString();
                householdinfo.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();
                householdinfo.MEMBER_ID = fc["MEMBER_ID"].ConvertToString();
                householdinfo.MEMBER_DEFINED_CD = fc["MEMBER_DEFINED_CD"].ConvertToString();
                householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                householdinfo.FIRST_NAME_ENG = fc["FIRST_NAME_ENG"].ConvertToString();
                householdinfo.FIRST_NAME_LOC = householdinfo.FIRST_NAME_ENG;
                householdinfo.MIDDLE_NAME_ENG = fc["MIDDLE_NAME_ENG"].ConvertToString();
                householdinfo.MIDDLE_NAME_LOC = householdinfo.MIDDLE_NAME_ENG;
                householdinfo.LAST_NAME_ENG = fc["LAST_NAME_ENG"].ConvertToString();
                householdinfo.LAST_NAME_LOC = householdinfo.LAST_NAME_ENG;
                householdinfo.FULL_NAME_ENG = householdinfo.FIRST_NAME_ENG.ConvertToString() + (householdinfo.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + householdinfo.MIDDLE_NAME_ENG) + " ") + householdinfo.LAST_NAME_ENG.ConvertToString();
                householdinfo.FULL_NAME_LOC = householdinfo.FULL_NAME_ENG.ConvertToString();

                householdinfo.IS_RESPONDENT_HOUSEHEAD = fc["IS_RESPONDENT_HOUSEHEAD"].ConvertToString();

                householdinfo.RESPONDENT_FIRST_NAME = fc["RESPONDENT_FIRST_NAME"].ConvertToString();
                householdinfo.RESPONDENT_FIRST_NAME_LOC = householdinfo.RESPONDENT_FIRST_NAME;
                householdinfo.RESPONDENT_MIDDLE_NAME = fc["RESPONDENT_MIDDLE_NAME"].ConvertToString();
                householdinfo.RESPONDENT_MIDDLE_NAME_LOC = householdinfo.RESPONDENT_MIDDLE_NAME;
                householdinfo.RESPONDENT_LAST_NAME = fc["RESPONDENT_LAST_NAME"].ConvertToString();
                householdinfo.RESPONDENT_LAST_NAME_LOC = householdinfo.RESPONDENT_LAST_NAME;

                householdinfo.RESPONDENT_FULL_NAME_ENG = householdinfo.RESPONDENT_FIRST_NAME.ConvertToString() + (householdinfo.RESPONDENT_MIDDLE_NAME.ConvertToString() == "" ? " " : (" " + householdinfo.RESPONDENT_MIDDLE_NAME) + " ") + householdinfo.RESPONDENT_LAST_NAME.ConvertToString();
                householdinfo.RESPONDENT_FULL_NAME_LOC = householdinfo.RESPONDENT_FULL_NAME_ENG;
                
                householdinfo.RESPONDENT_GENDER_CD = fc["RESPONDENT_GENDER_CD"] == "" ? null : fc["RESPONDENT_GENDER_CD"];
                householdinfo.RESPONDENT_RELATION_WITH_HH = fc["RESPONDENT_RELATION_WITH_HH"].ConvertToString() == "" ? null : fc["RESPONDENT_RELATION_WITH_HH"].ConvertToString();

                householdinfo.MEMBER_CNT = fc["MEMBER_CNT"].ConvertToString() == "" ? "0" : fc["MEMBER_CNT"].ConvertToString();

               


                householdinfo.SHELTER_BEFORE_QUAKE_CD = fc["SHELTER_BEFORE_QUAKE_CD"].ConvertToString() == "" ? null : fc["SHELTER_BEFORE_QUAKE_CD"].ConvertToString();
                householdinfo.SHELTER_SINCE_QUAKE_CD = fc["SHELTER_SINCE_QUAKE_CD"].ConvertToString() == "" ? null : fc["SHELTER_SINCE_QUAKE_CD"].ConvertToString();
                householdinfo.CURRENT_SHELTER_CD = fc["CURRENT_SHELTER_CD"].ConvertToString() == "" ? null : fc["CURRENT_SHELTER_CD"].ConvertToString();

                householdinfo.EQ_VICTIM_IDENTITY_CARD_CD = fc["EQ_VICTIM_IDENTITY_CARD_CD"].ConvertToString() == "" ? null : fc["EQ_VICTIM_IDENTITY_CARD_CD"].ConvertToString();
                householdinfo.EQ_VICTIM_IDENTITY_CARD_NO = fc["EQ_VICTIM_IDENTITY_CARD_NO"].ConvertToString() == "" ? null : fc["EQ_VICTIM_IDENTITY_CARD_NO"].ConvertToString();
                if (!string.IsNullOrEmpty(householdinfo.EQ_VICTIM_IDENTITY_CARD_CD))
                {
                    householdinfo.EQ_VICTIM_IDENTITY_CARD = "Y";
                }
                else
                {
                    householdinfo.EQ_VICTIM_IDENTITY_CARD = "N";
                }
                householdinfo.MONTHLY_INCOME_CD = fc["MONTHLY_INCOME_CD"] == "" ? null : fc["MONTHLY_INCOME_CD"].ConvertToString();
                householdinfo.TEL_NO = fc["TEL_NO"].ConvertToString() == "" ? null : fc["TEL_NO"].ConvertToString();
                householdinfo.MOBILE_NO = fc["MOBILE_NO"].ConvertToString() == "" ? null : fc["MOBILE_NO"].ConvertToString();

                householdinfo.DEATH_IN_A_YEAR = fc["DEATH_IN_A_YEAR"].ConvertToString();
                householdinfo.DEATH_CNT = fc["DEATH_CNT"].ConvertToString() == "" ? "0" : fc["DEATH_CNT"].ConvertToString();


                householdinfo.HUMAN_DESTROY_FLAG = fc["HUMAN_DESTROY_FLAG"].ConvertToString();
                householdinfo.HUMAN_DESTROY_CNT = fc["HUMAN_DESTROY_CNT"].ConvertToString() == "" ? "0" : fc["HUMAN_DESTROY_CNT"].ConvertToString();


                householdinfo.STUDENT_SCHOOL_LEFT = fc["STUDENT_SCHOOL_LEFT"].ConvertToString();
                householdinfo.STUDENT_SCHOOL_LEFT_CNT = fc["STUDENT_SCHOOL_LEFT_CNT"].ConvertToString() == "" ? "0" : fc["STUDENT_SCHOOL_LEFT_CNT"].ConvertToString();


                householdinfo.PREGNANT_REGULAR_CHECKUP = fc["PREGNANT_REGULAR_CHECKUP"].ConvertToString();
                householdinfo.PREGNANT_REGULAR_CHECKUP_CNT = fc["PREGNANT_REGULAR_CHECKUP_CNT"].ConvertToString() == "" ? "0" : fc["PREGNANT_REGULAR_CHECKUP_CNT"].ConvertToString();


                householdinfo.CHILD_LEFT_VACINATION = fc["CHILD_LEFT_VACINATION"].ConvertToString();
                householdinfo.CHILD_LEFT_VACINATION_CNT = fc["CHILD_LEFT_VACINATION_CNT"].ConvertToString() == "" ? "0" : fc["CHILD_LEFT_VACINATION_CNT"].ConvertToString();


                householdinfo.LEFT_CHANGE_OCCUPANY = fc["LEFT_CHANGE_OCCUPANY"].ConvertToString();
                householdinfo.LEFT_CHANGE_OCCUPANY_CNT = fc["LEFT_CHANGE_OCCUPANY_CNT"].ConvertToString() == "" ? "0" : fc["LEFT_CHANGE_OCCUPANY_CNT"].ConvertToString();
                socioService.UpdateHouseholdMember(householdinfo);

                
                #endregion

                #region RELIEF

                DataTable dtReliefMoneyNames = socioService.ReliefMoneyNames();
                if (dtReliefMoneyNames != null && dtReliefMoneyNames.Rows.Count > 0)
                {
                    socioService = new NHRSSocioEconomicDetailService();
                    NHRS_EQ_RELIEF_MONEY MoneyNameDelete = new NHRS_EQ_RELIEF_MONEY();
                    MoneyNameDelete.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                    MoneyNameDelete.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                    socioService.DeleteHouseholdReliefMoney(MoneyNameDelete);
                    foreach (DataRow dr in dtReliefMoneyNames.Rows)
                    {
                        NHRS_EQ_RELIEF_MONEY MoneyName = new NHRS_EQ_RELIEF_MONEY();
                        MoneyName.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        MoneyName.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        MoneyName.EQ_RELIEF_MONEY_CD = Convert.ToDecimal(dr["EQ_RELIEF_MONEY_CD"].ConvertToString());

                        if (fc["chkReliefMoney_" + dr["EQ_RELIEF_MONEY_CD"].ConvertToString()].Contains("true"))
                        {
                            socioService.InsertHouseholdReliefMoney(MoneyName);
                        }
                        
                    }
                }

                #endregion

                #region IDENTIFICATION

                //NHRS_HH_IDENTIFICATION_DTL identification = new NHRS_HH_IDENTIFICATION_DTL();
                //identification.ID_TYPE_CD = fc["IDENTIFICATION_TYPE_CD"].ConvertToString();
                //identification.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                //identification.MEMBER_ID = fc["MEMBER_ID"].ConvertToString();
                //identification.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();

                //if (identification.ID_TYPE_CD == "1")
                //{
                //    identification.ID_NO = fc["CTZ_NO"].ConvertToString();
                //    identification.ID_ISS_DISTRICT_CD = fc["CTZ_ISSUE_DISTRICT_CD"].ConvertToString();

                //    identification.ID_ISSUE_YEAR = fc["CTZ_ISSUE_YEAR"].ConvertToString();
                //    identification.ID_ISSUE_MONTH = fc["CTZ_ISSUE_MONTH"].ConvertToString();
                //    identification.ID_ISSUE_DAY = fc["CTZ_ISSUE_DAY"].ConvertToString();

                //    identification.ID_ISSUE_YEAR_LOC = fc["CTZ_ISSUE_YEAR"].ConvertToString();
                //    identification.ID_ISSUE_MONTH_LOC = fc["CTZ_ISSUE_MONTH"].ConvertToString();
                //    identification.ID_ISSUE_DAY_LOC = fc["CTZ_ISSUE_DAY"].ConvertToString();

                //    identification.ID_ISSUE_DT = identification.ID_ISSUE_YEAR + "-" + identification.ID_ISSUE_MONTH + "-" + identification.ID_ISSUE_DAY;


                //}
                //else if (identification.ID_TYPE_CD == "2")
                //{

                //    identification.ID_NO = fc["DRIVING_LICENSE_NO"].ConvertToString();
                //    identification.ID_ISS_DISTRICT_CD = fc["DRIVING_LICENSE_ISSUE_DISTRICT_CD"].ConvertToString();

                //}
                //else if (identification.ID_TYPE_CD == "3")
                //{
                //    identification.ID_NO = fc["VOTERID_NO"].ConvertToString();
                //    identification.ID_ISS_DISTRICT_CD = fc["VOTERID_DISTRICT_CD"].ConvertToString();
                //    identification.ID_ISSUE_DT = fc["VOTERID_ISSUE_DT"].ConvertToString();

                //}
                //else if (identification.ID_TYPE_CD == "4")
                //{
                //    identification.ID_NO = fc["SOCIAL_ALLOWANCE_NO"].ConvertToString();
                //    identification.ID_ISS_DISTRICT_CD = fc["SOCIAL_ALL_ISS_DISTRICT"].ConvertToString();
                //    identification.ID_ISSUE_DT = fc["SOCIAL_ALL_ISS_DATE"].ConvertToString();

                //}
                //else if (identification.ID_TYPE_CD == "6")
                //{
                //    identification.ID_OTHERS = fc["IDENTIFICATION_OTHERS"].ConvertToString();
                //    identification.ID_OTHERS_LOC = fc["IDENTIFICATION_OTHERS"].ConvertToString();
                //}  

                #endregion

                #region HH Member Section

                if (!string.IsNullOrEmpty(householdinfo.MEMBER_CNT) && householdinfo.MEMBER_CNT != "0")
                {
                    socioService = new NHRSSocioEconomicDetailService();
                    for (int i = 0; i < householdinfo.MEMBER_CNT.ToDecimal(); i++)
                    {
                        NHRS_HOUSEHOLD_MEMBER hhMember = new NHRS_HOUSEHOLD_MEMBER();
                        hhMember.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        hhMember.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();
                        hhMember.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        hhMember.HOUSEHOLD_DEFINED_CD = fc["HOUSEHOLD_DEFINED_CD"].ConvertToString();
                        if (i == 0)
                        {
                            hhMember.MEMBER_ID = fc["MemberID"].ConvertToString();
                            hhMember.MEMBER_DEFINED_CD = fc["MemberDefinedCode"].ConvertToString();
                            hhMember.FIRST_NAME_ENG = fc["MemberFirstName"].ConvertToString();
                            hhMember.MIDDLE_NAME_ENG = fc["MemberMiddleName"].ConvertToString();
                            hhMember.LAST_NAME_ENG = fc["MemberLastName"].ConvertToString();
                            hhMember.FIRST_NAME_LOC = fc["MemberFirstName"].ConvertToString();
                            hhMember.MIDDLE_NAME_LOC = fc["MemberMiddleName"].ConvertToString();
                            hhMember.LAST_NAME_LOC = fc["MemberLastName"].ConvertToString();
                            hhMember.FULL_NAME_ENG = hhMember.FIRST_NAME_ENG.ConvertToString() + (hhMember.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + hhMember.MIDDLE_NAME_ENG) + " ") + hhMember.LAST_NAME_ENG.ConvertToString();
                            hhMember.FULL_NAME_LOC = hhMember.FULL_NAME_ENG;

                            if (!string.IsNullOrEmpty(fc["MemberGenderCode"].ConvertToString()))
                            {
                                hhMember.GENDER_CD = common.GetCodeFromDataBase(fc["MemberGenderCode"].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                            }
                            else
                            {
                                hhMember.GENDER_CD = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MaritalStatusCode"].ConvertToString()))
                            {
                                hhMember.MARITAL_STATUS_CD = common.GetCodeFromDataBase(fc["MaritalStatusCode"].ConvertToString(), "MIS_MARITAL_STATUS", "MARITAL_STATUS_CD");
                            }
                            else
                            {
                                hhMember.MARITAL_STATUS_CD = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MemberPresenceCode"].ConvertToString()))
                            {

                                hhMember.PRESENCE_STATUS_CD = common.GetValueFromDataBase((fc["MemberPresenceCode"].ConvertToString()), "NHRS_PRESENCE_STATUS", "PRESENCE_STATUS_DEF_CD", "PRESENCE_STATUS_CD");
                            }
                            else
                            {
                                hhMember.PRESENCE_STATUS_CD = null;
                            }
                            hhMember.RELATION_CD = common.GetCodeFromDataBase(fc["MemberRelationCode"].ConvertToString(), "MIS_RELATION_TYPE", "RELATION_TYPE_CD");
                            if (fc["MemberRelationCode"].ConvertToString() == "1")
                            {
                                hhMember.HOUSEHOLD_HEAD = "Y";
                             
                            }
                            else
                            {
                                hhMember.HOUSEHOLD_HEAD = "N";
                            }
                            if (!string.IsNullOrEmpty(fc["MemberBirthYear"].ConvertToString()))
                            {
                                hhMember.BIRTH_YEAR = fc["MemberBirthYear"].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_YEAR = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MemberBirthMonth"].ConvertToString()))
                            {
                                hhMember.BIRTH_MONTH = fc["MemberBirthMonth"].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_MONTH = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MemberBirthDay"].ConvertToString()))
                            {
                                hhMember.BIRTH_DAY = fc["MemberBirthDay"].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_DAY = null;
                            }
                            
                            hhMember.BIRTH_YEAR_LOC = fc["MemberBirthYear"].ConvertToString();
                            hhMember.BIRTH_MONTH_LOC = fc["MemberBirthMonth"].ConvertToString();
                            hhMember.BIRTH_DAY_LOC = fc["MemberBirthDay"].ConvertToString();
                            hhMember.BIRTH_DT = hhMember.BIRTH_DAY + "/" + hhMember.BIRTH_MONTH + "/" + hhMember.BIRTH_YEAR;
                            hhMember.BIRTH_DT_LOC = hhMember.BIRTH_YEAR + "-" + hhMember.BIRTH_MONTH + "-" + hhMember.BIRTH_DAY;
                            if (!string.IsNullOrEmpty(fc["MemberAge"].ConvertToString()))
                            {
                                hhMember.AGE = fc["MemberAge"].ConvertToString();
                            }
                            else
                            {
                                hhMember.AGE = null;
                            }
                            if (!string.IsNullOrEmpty(fc["EducationCode"].ConvertToString()))
                            {
                                hhMember.EDUCATION_CD = common.GetCodeFromDataBase(fc["EducationCode"].ConvertToString(), "MIS_CLASS_TYPE", "CLASS_TYPE_CD");
                            }
                            else
                            {
                                hhMember.EDUCATION_CD = null;
                            }
                            
                            if (!string.IsNullOrEmpty(hhMember.EDUCATION_CD))
                            {
                                hhMember.LITERATE = "Y";
                            }
                            else
                            {
                                hhMember.LITERATE = "N";
                            }
                            if (!string.IsNullOrEmpty(fc["BirthCertificate"].ConvertToString()))
                            {
                                hhMember.BIRTH_CERTIFICATE = fc["BirthCertificate"].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_CERTIFICATE = "N";
                            }
                            
                            if (hhMember.HOUSEHOLD_HEAD == "Y")
                            {
                                if (!string.IsNullOrEmpty(fc["CASTE_CD"].ConvertToString()))
                                {
                                    hhMember.CASTE_CD = common.GetCodeFromDataBase(fc["CASTE_CD"].ConvertToString(), "MIS_CASTE", "CASTE_CD");
                                }
                                else
                                {
                                    hhMember.CASTE_CD = null;
                                }
                                

                                hhMember.TEL_NO = fc["TEL_NO"].ConvertToString();
                                hhMember.MOBILE_NO = fc["MOBILE_NO"].ConvertToString();
                                hhMember.BANK_BRANCH_CD = common.GetCodeFromDataBase(fc["BANK_BRANCH_CD"].ConvertToString(), "NHRS_BANK_BRANCH", "BANK_BRANCH_CD");
                                hhMember.BANK_CD = common.GetCodeFromDataBase(fc["BANK_CD"].ConvertToString(), "NHRS_BANK", "BANK_CD");
                                hhMember.HH_MEMBER_BANK_ACC_NO = fc["HH_MEMBER_BANK_ACC_NO"].ConvertToString();
                                if (string.IsNullOrEmpty(hhMember.BANK_CD) || string.IsNullOrEmpty(hhMember.BANK_BRANCH_CD) || string.IsNullOrEmpty(hhMember.HH_MEMBER_BANK_ACC_NO))
                                {
                                    hhMember.BANK_ACCOUNT_FLAG = "N";
                                }
                                else
                                {
                                    hhMember.BANK_ACCOUNT_FLAG = "Y";
                                }
                            }
                            else
                            {
                                hhMember.CASTE_CD = null;
                                hhMember.TEL_NO = null;
                                hhMember.MOBILE_NO = null;
                                hhMember.BANK_BRANCH_CD = null;
                                hhMember.BANK_CD = null;
                                hhMember.HH_MEMBER_BANK_ACC_NO = null;
                                hhMember.BANK_ACCOUNT_FLAG = "N";
                            }
                            hhMember.ALLOWANCE_TYPE_CD = common.GetCodeFromDataBase(fc["SocialAllowanceCode"].ConvertToString(), "MIS_ALLOWANCE_TYPE", "ALLOWANCE_TYPE_CD");
                            if (!string.IsNullOrEmpty(hhMember.ALLOWANCE_TYPE_CD))
                            {
                                hhMember.SOCIAL_ALLOWANCE = "Y";
                                hhMember.ALLOWANCE_DAY = fc["AllowanceDay"].ConvertToString();
                                hhMember.ALLOWANCE_MONTH = fc["AllowanceMonth"].ConvertToString();
                                hhMember.ALLOWANCE_YEARS = fc["AllowanceYear"].ConvertToString();
                            }
                            else
                            {
                                hhMember.SOCIAL_ALLOWANCE = "N";
                            }
                            hhMember.HANDI_COLOR_CD = common.GetCodeFromDataBase(fc["HandiColorCode"].ConvertToString(), "MIS_HANDI_COLOR", "HANDI_COLOR_CD");
                            if (!string.IsNullOrEmpty(hhMember.HANDI_COLOR_CD))
                            {
                                hhMember.DISABILITY = "Y";
                            }
                            else
                            {
                                hhMember.DISABILITY = "N";
                            }
                        }
                        else
                        {
                            hhMember.MEMBER_ID = fc["MemberID" + i].ConvertToString();
                            hhMember.MEMBER_DEFINED_CD = fc["MemberDefinedCode" + i].ConvertToString();
                            hhMember.FIRST_NAME_ENG = fc["MemberFirstName" + i].ConvertToString();
                            hhMember.MIDDLE_NAME_ENG = fc["MemberMiddleName" + i].ConvertToString();
                            hhMember.LAST_NAME_ENG = fc["MemberLastName" + i].ConvertToString();
                            hhMember.FIRST_NAME_LOC = fc["MemberFirstName" + i].ConvertToString();
                            hhMember.MIDDLE_NAME_LOC = fc["MemberMiddleName" + i].ConvertToString();
                            hhMember.LAST_NAME_LOC = fc["MemberLastName" + i].ConvertToString();
                            hhMember.FULL_NAME_ENG = hhMember.FIRST_NAME_ENG.ConvertToString() + (hhMember.MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + hhMember.MIDDLE_NAME_ENG) + " ") + hhMember.LAST_NAME_ENG.ConvertToString();
                            hhMember.FULL_NAME_LOC = hhMember.FULL_NAME_ENG;
                            if (!string.IsNullOrEmpty(fc["MemberGenderCode" + i].ConvertToString()))
                            {
                                hhMember.GENDER_CD = common.GetCodeFromDataBase(fc["MemberGenderCode" + i].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                            }
                            else
                            {
                                hhMember.GENDER_CD = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MaritalStatusCode" + i].ConvertToString()))
                            {
                                hhMember.MARITAL_STATUS_CD = common.GetCodeFromDataBase(fc["MaritalStatusCode" + i].ConvertToString(), "MIS_MARITAL_STATUS", "MARITAL_STATUS_CD");
                            }
                            else
                            {
                                hhMember.MARITAL_STATUS_CD = null;
                            }
                            hhMember.RELATION_CD = common.GetCodeFromDataBase(fc["MemberRelationCode" + i].ConvertToString(), "MIS_RELATION_TYPE", "RELATION_TYPE_CD");
                            if (fc["MemberRelationCode" + i].ConvertToString() == "1")
                            {
                                hhMember.HOUSEHOLD_HEAD = "Y";
                            }
                            else
                            {
                                hhMember.HOUSEHOLD_HEAD = "N";
                            }
                            if (!string.IsNullOrEmpty(fc["MemberPresenceCode"+i].ConvertToString()))
                            {

                                hhMember.PRESENCE_STATUS_CD = common.GetValueFromDataBase((fc["MemberPresenceCode"+i].ConvertToString()), "NHRS_PRESENCE_STATUS", "PRESENCE_STATUS_DEF_CD", "PRESENCE_STATUS_CD");
                            }
                            else
                            {
                                hhMember.PRESENCE_STATUS_CD = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MemberBirthYear" + i].ConvertToString()))
                            {
                                hhMember.BIRTH_YEAR = fc["MemberBirthYear" + i].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_YEAR = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MemberBirthMonth" + i].ConvertToString()))
                            {
                                hhMember.BIRTH_MONTH = fc["MemberBirthMonth" + i].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_MONTH = null;
                            }
                            if (!string.IsNullOrEmpty(fc["MemberBirthDay" + i].ConvertToString()))
                            {
                                hhMember.BIRTH_DAY = fc["MemberBirthDay" + i].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_DAY = null;
                            }
                            
                            hhMember.BIRTH_YEAR_LOC = fc["MemberBirthYear" + i].ConvertToString();
                            hhMember.BIRTH_MONTH_LOC = fc["MemberBirthMonth" + i].ConvertToString();
                            hhMember.BIRTH_DAY_LOC = fc["MemberBirthDay" + i].ConvertToString();
                            hhMember.BIRTH_DT = hhMember.BIRTH_DAY + "/" + hhMember.BIRTH_MONTH + "/" + hhMember.BIRTH_YEAR;
                            hhMember.BIRTH_DT_LOC = hhMember.BIRTH_YEAR + "-" + hhMember.BIRTH_MONTH + "-" + hhMember.BIRTH_DAY;
                            if (!string.IsNullOrEmpty(fc["MemberAge" + i].ConvertToString()))
                            {
                                hhMember.AGE = fc["MemberAge" + i].ConvertToString();
                            }
                            else
                            {
                                hhMember.AGE = null;
                            }
                            if (!string.IsNullOrEmpty(fc["EducationCode" + i].ConvertToString()))
                            {
                                hhMember.EDUCATION_CD = common.GetCodeFromDataBase(fc["EducationCode" + i].ConvertToString(), "MIS_CLASS_TYPE", "CLASS_TYPE_CD");
                            }
                            else
                            {
                                hhMember.EDUCATION_CD = null;
                            }
                            
                            if (!string.IsNullOrEmpty(hhMember.EDUCATION_CD))
                            {
                                hhMember.LITERATE = "Y";
                            }
                            else
                            {
                                hhMember.LITERATE = "N";
                            }
                            if (!string.IsNullOrEmpty(fc["BirthCertificate" + i].ConvertToString()))
                            {
                                hhMember.BIRTH_CERTIFICATE = fc["BirthCertificate" + i].ConvertToString();
                            }
                            else
                            {
                                hhMember.BIRTH_CERTIFICATE = null;
                            }
                            
                            if (hhMember.HOUSEHOLD_HEAD == "Y")
                            {
                                hhMember.CASTE_CD = common.GetCodeFromDataBase(fc["CASTE_CD"].ConvertToString(), "MIS_CASTE", "CASTE_CD");
                                hhMember.TEL_NO = fc["TEL_NO"].ConvertToString();
                                hhMember.MOBILE_NO = fc["MOBILE_NO"].ConvertToString();
                                hhMember.BANK_BRANCH_CD = common.GetCodeFromDataBase(fc["BANK_BRANCH_CD"].ConvertToString(), "NHRS_BANK_BRANCH", "BANK_BRANCH_CD");
                                hhMember.BANK_CD = common.GetCodeFromDataBase(fc["BANK_CD"].ConvertToString(), "NHRS_BANK", "BANK_CD");
                                hhMember.HH_MEMBER_BANK_ACC_NO = fc["HH_MEMBER_BANK_ACC_NO"].ConvertToString();
                                if (string.IsNullOrEmpty(hhMember.BANK_CD) || string.IsNullOrEmpty(hhMember.BANK_BRANCH_CD) || string.IsNullOrEmpty(hhMember.HH_MEMBER_BANK_ACC_NO))
                                {
                                    hhMember.BANK_ACCOUNT_FLAG = "N";
                                }
                                else
                                {
                                    hhMember.BANK_ACCOUNT_FLAG = "Y";
                                }
                            }
                            else
                            {
                                hhMember.CASTE_CD = null;
                                hhMember.TEL_NO = null;
                                hhMember.MOBILE_NO = null;
                                hhMember.BANK_BRANCH_CD = null;
                                hhMember.BANK_CD = null;
                                hhMember.HH_MEMBER_BANK_ACC_NO = null;
                                hhMember.BANK_ACCOUNT_FLAG = "N";
                            }
                            if (!string.IsNullOrEmpty(fc["SocialAllowanceCode" + i].ConvertToString()))
                            {
                                hhMember.ALLOWANCE_TYPE_CD = common.GetCodeFromDataBase(fc["SocialAllowanceCode" + i].ConvertToString(), "MIS_ALLOWANCE_TYPE", "ALLOWANCE_TYPE_CD");
                              
                            }
                            else
                            {
                                hhMember.ALLOWANCE_TYPE_CD = null;
                            }
                            
                            if (!string.IsNullOrEmpty(hhMember.ALLOWANCE_TYPE_CD))
                            {
                                hhMember.SOCIAL_ALLOWANCE = "Y";
                                hhMember.ALLOWANCE_DAY = fc["AllowanceDay"+i].ConvertToString();
                                hhMember.ALLOWANCE_MONTH = fc["AllowanceMonth"+i].ConvertToString();
                                hhMember.ALLOWANCE_YEARS = fc["AllowanceYear"+i].ConvertToString();
                            }
                            else
                            {
                                hhMember.SOCIAL_ALLOWANCE = "N";
                            }
                            if (!string.IsNullOrEmpty(fc["HandiColorCode" + i].ConvertToString()))
                            {
                                hhMember.HANDI_COLOR_CD = common.GetCodeFromDataBase(fc["HandiColorCode" + i].ConvertToString(), "MIS_HANDI_COLOR", "HANDI_COLOR_CD");
                            }
                            else
                            {
                                hhMember.HANDI_COLOR_CD = null;
                            }
                            
                            if (!string.IsNullOrEmpty(hhMember.HANDI_COLOR_CD))
                            {
                                hhMember.DISABILITY = "Y";
                            }
                            else
                            {
                                hhMember.DISABILITY = "N";
                            }
                        }

                        socioService.UpdateMember(hhMember);
                    }
                }
                #endregion

                #region Death Section
                //Calling DEATH_CNT procedure
                if (householdinfo.DEATH_IN_A_YEAR == "Y")
                {
                    if (!string.IsNullOrEmpty(householdinfo.DEATH_CNT) && householdinfo.DEATH_CNT != "0")
                    {
                        socioService = new NHRSSocioEconomicDetailService();
                        for (int i = 0; i <= householdinfo.DEATH_CNT.ToDecimal(); i++)
                        {
                            VW_MEMBER_DEATH_DTL memDeath = new VW_MEMBER_DEATH_DTL();

                            memDeath.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                            memDeath.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();
                            memDeath.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                            if (i == 0)
                            {
                                memDeath.M_SNO = fc["SN"].ToDecimal();
                                memDeath.M_DEFINED_CD = fc["DeathMemberID"].ConvertToString();
                                memDeath.M_FIRST_NAME_ENG = fc["DeathFName"].ConvertToString();
                                memDeath.M_MIDDLE_NAME_ENG = fc["DeathMName"].ConvertToString();
                                memDeath.M_LAST_NAME_ENG = fc["DeathLName"].ConvertToString();
                                memDeath.M_FIRST_NAME_LOC = memDeath.M_FIRST_NAME_ENG;
                                memDeath.M_MIDDLE_NAME_LOC = memDeath.M_MIDDLE_NAME_ENG;
                                memDeath.M_LAST_NAME_LOC = memDeath.M_LAST_NAME_ENG;
                                memDeath.M_FULL_NAME_ENG = memDeath.M_FIRST_NAME_ENG.ConvertToString() + (memDeath.M_MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + memDeath.M_MIDDLE_NAME_ENG) + " ") + memDeath.M_LAST_NAME_ENG.ConvertToString();
                                memDeath.M_FULL_NAME_LOC = memDeath.M_FULL_NAME_ENG;
                                if (!string.IsNullOrEmpty(fc["DeathGender"].ConvertToString()))
                                {
                                    memDeath.GENDER_CD = common.GetCodeFromDataBase(fc["DeathGender"].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                                }
                                else
                                {
                                    memDeath.GENDER_CD = null;
                                }
                                if (!string.IsNullOrEmpty(fc["MemberDeathDay"].ConvertToString()))
                                {
                                    memDeath.DEATH_DAY = fc["MemberDeathDay"].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_DAY = null;
                                }
                                if (!string.IsNullOrEmpty(fc["MemberDeathMonth"].ConvertToString()))
                                {
                                    memDeath.DEATH_MONTH = fc["MemberDeathMonth"].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_MONTH = null;
                                }
                                if (!string.IsNullOrEmpty(fc["MemberDeathYear"].ConvertToString()))
                                {
                                    memDeath.DEATH_YEAR = fc["MemberDeathYear"].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_YEAR = null;
                                }

                                memDeath.DEATH_DAY_LOC = memDeath.DEATH_DAY.ConvertToString();
                                memDeath.DEATH_MONTH_LOC = memDeath.DEATH_MONTH.ConvertToString();
                                memDeath.DEATH_YEAR_LOC = memDeath.DEATH_YEAR;
                                if (!string.IsNullOrEmpty(memDeath.DEATH_YEAR) && !string.IsNullOrEmpty(memDeath.DEATH_MONTH.ConvertToString()) && !string.IsNullOrEmpty(memDeath.DEATH_DAY.ConvertToString()))
                                {
                                    memDeath.DEATH_DT = (memDeath.DEATH_YEAR + "-" + memDeath.DEATH_MONTH + "-" + memDeath.DEATH_DAY).ConvertToString();
                                    memDeath.DEATH_YEAR_LOC = memDeath.DEATH_YEAR + "-" + memDeath.DEATH_MONTH.ConvertToString() + "-" + memDeath.DEATH_DAY.ConvertToString();
                                }
                                if (!string.IsNullOrEmpty(fc["DeathAge"].ConvertToString()))
                                {
                                    memDeath.AGE = fc["DeathAge"].ConvertToString();
                                }
                                else
                                {
                                    memDeath.AGE = null;
                                }
                                if (!string.IsNullOrEmpty(fc["DeathReason"].ConvertToString()))
                                {
                                    memDeath.DEATH_REASON_CD = common.GetValueFromDataBase((fc["DeathReason"].ConvertToString()), "NHRS_DEATH_REASON", "DEATH_REASON_DEF_CD", "DEATH_REASON_CD");
                                    //memDeath.DEATH_REASON_CD = common.GetCodeFromDataBase((fc["DeathReason"].ConvertToString(), "NHRS_DEATH_REASON", "DEATH_REASON_CD");
                                }
                                else
                                {
                                    memDeath.DEATH_REASON_CD = null;
                                }
                                if (!string.IsNullOrEmpty(fc["DeathCertificate"].ConvertToString()))
                                {
                                    memDeath.DEATH_CERTIFICATE = fc["DeathCertificate"].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_CERTIFICATE = null;
                                }

                            }
                            else
                            {
                                memDeath.M_DEFINED_CD = fc["DeathMemberID" + i].ConvertToString();
                                memDeath.M_FIRST_NAME_ENG = fc["DeathFName" + i].ConvertToString();
                                memDeath.M_MIDDLE_NAME_ENG = fc["DeathMName" + i].ConvertToString();
                                memDeath.M_LAST_NAME_ENG = fc["DeathLName" + i].ConvertToString();
                                memDeath.M_FIRST_NAME_LOC = memDeath.M_FIRST_NAME_ENG;
                                memDeath.M_MIDDLE_NAME_LOC = memDeath.M_MIDDLE_NAME_ENG;
                                memDeath.M_LAST_NAME_LOC = memDeath.M_LAST_NAME_ENG;
                                memDeath.M_FULL_NAME_ENG = memDeath.M_FIRST_NAME_ENG.ConvertToString() + (memDeath.M_MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + memDeath.M_MIDDLE_NAME_ENG) + " ") + memDeath.M_LAST_NAME_ENG.ConvertToString();
                                memDeath.M_FULL_NAME_LOC = memDeath.M_FULL_NAME_ENG;
                                if (!string.IsNullOrEmpty(fc["DeathGender" + i].ConvertToString()))
                                {
                                    memDeath.GENDER_CD = common.GetCodeFromDataBase(fc["DeathGender" + i].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                                }
                                else
                                {
                                    memDeath.GENDER_CD = null;
                                }
                                if (!string.IsNullOrEmpty(fc["MemberDeathDay" + i].ConvertToString()))
                                {
                                    memDeath.DEATH_DAY = fc["MemberDeathDay" + i].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_DAY = null;
                                }
                                if (!string.IsNullOrEmpty(fc["MemberDeathMonth" + i].ConvertToString()))
                                {
                                    memDeath.DEATH_MONTH = fc["MemberDeathMonth" + i].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_MONTH = null;
                                }
                                if (!string.IsNullOrEmpty(fc["MemberDeathYear" + i].ConvertToString()))
                                {
                                    memDeath.DEATH_YEAR = fc["MemberDeathYear" + i].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_YEAR = null;
                                }
                                memDeath.DEATH_DAY_LOC = memDeath.DEATH_DAY.ConvertToString();
                                memDeath.DEATH_MONTH_LOC = memDeath.DEATH_MONTH.ConvertToString();
                                memDeath.DEATH_YEAR_LOC = memDeath.DEATH_YEAR;
                                if (!string.IsNullOrEmpty(memDeath.DEATH_YEAR) && !string.IsNullOrEmpty(memDeath.DEATH_MONTH.ConvertToString()) && !string.IsNullOrEmpty(memDeath.DEATH_DAY.ConvertToString()))
                                {
                                    memDeath.DEATH_DT = (memDeath.DEATH_YEAR + "-" + memDeath.DEATH_MONTH + "-" + memDeath.DEATH_DAY).ConvertToString();
                                    memDeath.DEATH_YEAR_LOC = memDeath.DEATH_YEAR + "-" + memDeath.DEATH_MONTH.ConvertToString() + "-" + memDeath.DEATH_DAY.ConvertToString();
                                }

                                if (!string.IsNullOrEmpty(fc["DeathAge" + i].ConvertToString()))
                                {
                                    memDeath.AGE = fc["DeathAge" + i].ConvertToString();
                                }
                                else
                                {
                                    memDeath.AGE = null;
                                }
                                if (!string.IsNullOrEmpty(fc["DeathReason" + i].ConvertToString()))
                                {
                                    memDeath.DEATH_REASON_CD = common.GetCodeFromDataBase(fc["DeathReason" + i].ConvertToString(), "NHRS_DEATH_REASON", "DEATH_REASON_CD");
                                }
                                else
                                {
                                    memDeath.DEATH_REASON_CD = null;
                                }
                                if (!string.IsNullOrEmpty(fc["DeathCertificate" + i].ConvertToString()))
                                {
                                    memDeath.DEATH_CERTIFICATE = fc["DeathCertificate" + i].ConvertToString();
                                }
                                else
                                {
                                    memDeath.DEATH_CERTIFICATE = null;
                                }
                            }
                            socioService.UpdateMemberDeath(memDeath);
                        }
                    }
                }

                #endregion

                    #region Human Destroy
                    //calling HUMAN_DESTROY_CNT procedure
                    if (householdinfo.HUMAN_DESTROY_FLAG == "Y")
                    {
                        if (!string.IsNullOrEmpty(householdinfo.HUMAN_DESTROY_CNT) && householdinfo.HUMAN_DESTROY_CNT != "0")
                        {
                            socioService = new NHRSSocioEconomicDetailService();
                            for (int i = 0; i <= householdinfo.HUMAN_DESTROY_CNT.ToDecimal(); i++)
                            {
                                VW_MEMBER_HUMAN_DISTROY_DTL memDistroy = new VW_MEMBER_HUMAN_DISTROY_DTL();

                                memDistroy.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                                memDistroy.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();
                                memDistroy.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                                if (i == 0)
                                {
                                    memDistroy.HUMAN_DESTROY_CD = common.GetCodeFromDataBase(fc["HUMAN_DESTROY_CD"].ConvertToString(), "NHRS_HUMAN_DESTROY_TYPE", "DESTROY_TYPE_CD");
                                    memDistroy.M_SNO = fc["DistroySN"].ToDecimal();
                                    memDistroy.M_DEFINED_CD = fc["DistroyMemberID"].ConvertToString();
                                    memDistroy.M_FIRST_NAME_ENG = fc["DistroyFName"].ConvertToString();
                                    memDistroy.M_MIDDLE_NAME_ENG = fc["DistroyMName"].ConvertToString();
                                    memDistroy.M_LAST_NAME_ENG = fc["DistroyLName"].ConvertToString();
                                    memDistroy.M_FIRST_NAME_LOC = memDistroy.M_FIRST_NAME_ENG;
                                    memDistroy.M_MIDDLE_NAME_LOC = memDistroy.M_MIDDLE_NAME_ENG;
                                    memDistroy.M_LAST_NAME_LOC = memDistroy.M_LAST_NAME_ENG;
                                    memDistroy.M_FULL_NAME_ENG = memDistroy.M_FIRST_NAME_ENG.ConvertToString() + (memDistroy.M_MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + memDistroy.M_MIDDLE_NAME_ENG) + " ") + memDistroy.M_LAST_NAME_ENG.ConvertToString();
                                    memDistroy.M_FULL_NAME_LOC = memDistroy.M_FULL_NAME_ENG;
                                    memDistroy.GENDER_CD = common.GetCodeFromDataBase(fc["EarthquakeVictimGender"].ConvertToString() == "" ? null : fc["EarthquakeVictimGender"].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                                    memDistroy.AGE = fc["DistroyAge"] == "" ? null : fc["DistroyAge"].ToDecimal();
                                }
                                else
                                {
                                    memDistroy.HUMAN_DESTROY_CD = common.GetCodeFromDataBase(fc["HUMAN_DESTROY_CD" + i].ConvertToString(), "NHRS_HUMAN_DESTROY_TYPE", "DESTROY_TYPE_CD");
                                    memDistroy.M_SNO = fc["DistroySN" + i].ToDecimal();
                                    memDistroy.M_DEFINED_CD = fc["DistroyMemberID" + i].ConvertToString();
                                    memDistroy.M_FIRST_NAME_ENG = fc["DistroyFName" + i].ConvertToString();
                                    memDistroy.M_MIDDLE_NAME_ENG = fc["DistroyMName" + i].ConvertToString();
                                    memDistroy.M_LAST_NAME_ENG = fc["DistroyLName" + i].ConvertToString();
                                    memDistroy.M_FIRST_NAME_LOC = memDistroy.M_FIRST_NAME_ENG;
                                    memDistroy.M_MIDDLE_NAME_LOC = memDistroy.M_MIDDLE_NAME_ENG;
                                    memDistroy.M_LAST_NAME_LOC = memDistroy.M_LAST_NAME_ENG;
                                    memDistroy.M_FULL_NAME_ENG = memDistroy.M_FIRST_NAME_ENG.ConvertToString() + (memDistroy.M_MIDDLE_NAME_ENG.ConvertToString() == "" ? " " : (" " + memDistroy.M_MIDDLE_NAME_ENG) + " ") + memDistroy.M_LAST_NAME_ENG.ConvertToString();
                                    memDistroy.M_FULL_NAME_LOC = memDistroy.M_FULL_NAME_ENG;
                                    memDistroy.GENDER_CD = common.GetCodeFromDataBase(fc["EarthquakeVictimGender" + i].ConvertToString() == "" ? null : fc["EarthquakeVictimGender" + i].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                                    memDistroy.AGE = fc["DistroyAge" + i] == "" ? null : fc["DistroyAge" + i].ToDecimal();
                                }
                                socioService.UpdateMemberDistroy(memDistroy);
                            }
                        }
                    }
                    #endregion

                    #region Water Source
                    socioService = new NHRSSocioEconomicDetailService();
                    if (!string.IsNullOrEmpty(fc["WATER_SOURCE_ENG"].ConvertToString()))
                    {
                        householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        householdinfo.WATER_SOURCE_CD = fc["WATER_SOURCE_ENG"].ConvertToString();

                        if (fc["chkWaterBefore"].Contains("true"))
                        {
                            householdinfo.WATER_SOURCE_BEFORE = "Y";
                        }
                        else
                        {
                            householdinfo.WATER_SOURCE_BEFORE = "N";
                        }
                        if (fc["chkWaterAfter"].Contains("true"))
                        {
                            householdinfo.WATER_SOURCE_AFTER = "Y";
                        }
                        else
                        {
                            householdinfo.WATER_SOURCE_AFTER = "N";
                        }
                        if (!string.IsNullOrEmpty(Session["WaterSourceId"].ConvertToString()))
                        {
                            socioService.UpdateHouseholdWater(householdinfo);
                        }
                        else
                        {
                            socioService.InsertHouseholdWater(householdinfo);
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["WaterSourceId"].ConvertToString()))
                        {
                            householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                            householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                            householdinfo.WATER_SOURCE_CD = Session["WaterSourceId"].ConvertToString();
                            socioService.DeleteHouseholdWater(householdinfo);
                        }
                    }

                    #endregion

                    #region Fuel Source
                    socioService = new NHRSSocioEconomicDetailService();
                    if (!string.IsNullOrEmpty(fc["FUEL_SOURCE_ENG"].ConvertToString()))
                    {
                        householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        householdinfo.FUEL_SOURCE_CD = fc["FUEL_SOURCE_ENG"].ConvertToString();

                        if (fc["chkFuelBefore"].Contains("true"))
                        {
                            householdinfo.FUEL_SOURCE_BEFORE = "Y";
                        }
                        else
                        {
                            householdinfo.FUEL_SOURCE_BEFORE = "N";
                        }
                        if (fc["chkFuelAfter"].Contains("true"))
                        {
                            householdinfo.FUEL_SOURCE_AFTER = "Y";
                        }
                        else
                        {
                            householdinfo.FUEL_SOURCE_AFTER = "N";
                        }
                        if (!string.IsNullOrEmpty(Session["FuelSourceId"].ConvertToString()))
                        {
                            socioService.UpdateHouseholdFuel(householdinfo);
                        }
                        else
                        {
                            socioService.InsertHouseholdFuel(householdinfo);
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["FuelSourceId"].ConvertToString()))
                        {
                            householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                            householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                            householdinfo.FUEL_SOURCE_CD = Session["FuelSourceId"].ConvertToString();
                            socioService.DeleteHouseholdFuel(householdinfo);
                        }
                    }



                    #endregion

                    #region Light Source
                    socioService = new NHRSSocioEconomicDetailService();
                    if (!string.IsNullOrEmpty(fc["LIGHT_SOURCE_ENG"].ConvertToString()))
                    {
                        householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        householdinfo.LIGHT_SOURCE_CD = fc["LIGHT_SOURCE_ENG"].ConvertToString();

                        if (fc["chkLightBefore"].Contains("true"))
                        {
                            householdinfo.LIGHT_SOURCE_BEFORE = "Y";
                        }
                        else
                        {
                            householdinfo.LIGHT_SOURCE_BEFORE = "N";
                        }
                        if (fc["chkLightAfter"].Contains("true"))
                        {
                            householdinfo.LIGHT_SOURCE_AFTER = "Y";
                        }
                        else
                        {
                            householdinfo.LIGHT_SOURCE_AFTER = "N";
                        }
                        if (!string.IsNullOrEmpty(Session["LightSourceId"].ConvertToString()))
                        {
                            socioService.UpdateHouseholdLight(householdinfo);
                        }
                        else
                        {
                            socioService.InsertHouseholdLight(householdinfo);
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["LightSourceId"].ConvertToString()))
                        {
                            householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                            householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                            householdinfo.FUEL_SOURCE_CD = Session["LightSourceId"].ConvertToString();
                            socioService.DeleteHouseholdLight(householdinfo);
                        }
                    }


                    #endregion

                    #region Toilet Source
                    socioService = new NHRSSocioEconomicDetailService();
                    if (!string.IsNullOrEmpty(fc["TOILET_TYPE_ENG"].ConvertToString()))
                    {
                        householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        householdinfo.TOILET_TYPE_CD = fc["TOILET_TYPE_ENG"].ConvertToString();

                        if (fc["chkToiletBefore"].Contains("true"))
                        {
                            householdinfo.TOILET_TYPE_BEFORE = "Y";
                        }
                        else
                        {
                            householdinfo.TOILET_TYPE_BEFORE = "N";
                        }
                        if (fc["chkToiletAfter"].Contains("true"))
                        {
                            householdinfo.TOILET_TYPE_AFTER = "Y";
                        }
                        else
                        {
                            householdinfo.TOILET_TYPE_AFTER = "N";
                        }
                        if (!string.IsNullOrEmpty(Session["ToiletTypeId"].ConvertToString()))
                        {
                            socioService.UpdateHouseholdToilet(householdinfo);
                        }
                        else
                        {
                            socioService.InsertHouseholdToilet(householdinfo);
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["ToiletTypeId"].ConvertToString()))
                        {
                            householdinfo.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                            householdinfo.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                            householdinfo.FUEL_SOURCE_CD = Session["ToiletTypeId"].ConvertToString();
                            socioService.DeleteHouseholdToilet(householdinfo);
                        }
                    }


                    #endregion

                    #region Indicator
                    socioService = new NHRSSocioEconomicDetailService();
                    DataTable dtHouseIndicatorNames = socioService.HouseIndicatorNames();
                    if (dtHouseIndicatorNames != null && dtHouseIndicatorNames.Rows.Count > 0)
                    {
                        MIG_MIS_HOUSEHOLD_INDICATOR indicDelete = new MIG_MIS_HOUSEHOLD_INDICATOR();
                        indicDelete.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                        indicDelete.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                        socioService.DeleteHouseholdIndicator(indicDelete);

                        foreach (DataRow dr in dtHouseIndicatorNames.Rows)
                        {
                            MIG_MIS_HOUSEHOLD_INDICATOR indic = new MIG_MIS_HOUSEHOLD_INDICATOR();
                            indic.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();
                            indic.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                            indic.indicatorCd = Convert.ToDecimal(dr["INDICATOR_CD"].ConvertToString());
                            if (fc["chkhouseIndicatorName1_" + dr["INDICATOR_CD"].ConvertToString()].Contains("true"))
                            {
                                indic.INDICATOR_BEFORE = "Y";
                            }
                            else
                            {
                                indic.INDICATOR_BEFORE = "N";
                            }
                            if (fc["chkhouseIndicatorName2_" + dr["INDICATOR_CD"].ConvertToString()].Contains("true"))
                            {
                                indic.INDICATOR_AFTER = "Y";
                            }
                            else
                            {
                                indic.INDICATOR_AFTER = "N";
                            }
                            if (indic.INDICATOR_BEFORE == "Y" || indic.INDICATOR_AFTER == "Y")
                            {
                                socioService.InsertHouseholdIndicator(indic);
                            }
                        }
                    }
                    #endregion
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("HouseHoldInfoSearch", "NHRP");
        }

        public ActionResult MemberAdd()
        {

            return View();
        }

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

        # region Dropdown

        public void GetHouseDropDown()
        {
            try
            {

                ViewData["ddl_District"] = common.GetDistricts("");
                //ViewData["ddl_RegionPer"] = common.GetRegionForUser("");
                //ViewData["ddl_ZonePer"] = common.GetZonebyRegion(household.PER_ZONE_ENG, household.PER_REGION_ENG);
                ViewData["ddl_DistrictsPer"] = common.GetDistrictsbyZone(household.PER_DISTRICT_CD, household.PER_ZONE_ENG);
                ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(household.PER_VDCMUNICIPILITY_CD, household.PER_DISTRICT_CD);
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


        public void GetDropDown()
        {
            try
            {

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
     

    }
}
