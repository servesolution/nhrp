using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models.NHRP.Resurvey;
using System.Data;
using MIS.Services.ResurveyView;
using MIS.Services.Core;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Services.NHRP.Edit;
using System.Web.Routing;
using EntityFramework;
using MIS.Models.NHRP;
using MIS.Models.NHRP.View;
using MIS.Services.NHRP.View;
using ClosedXML.Excel;
using System.IO;

namespace MIS.Controllers.Resurvey
{
    public class ResurveyViewController : BaseController
    {
        //
        // GET: /ResurveyView/
        CommonFunction common = new CommonFunction();

        public ActionResult ResurveyHouseView(string p)
        {

            ResurveyViewService objService = new ResurveyViewService();
            ResurveyHouseViewModel objModel = new ResurveyHouseViewModel();
            DataSet ds = new DataSet();
            string houseOwnerId = null;
            DataTable isVerified = new DataTable();
            try
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        houseOwnerId = (rvd["id2"]).ConvertToString();
                    }
                }
                isVerified = objService.IsOwnerIdVerified(houseOwnerId);
                if (isVerified != null && isVerified.Rows.Count > 0)
                {
                    ViewData["owner_id_status"] = "VERIFIED";
                    ViewData["Recommended_Data"] = isVerified;
                }
                else
                {
                    ViewData["owner_id_status"] = "UNVERIFIED";
                }

                ds = objService.ResurveyHouseView(houseOwnerId, "");
                DataTable dtSurvey = new DataTable();
                dtSurvey = objService.GetSurveyData(houseOwnerId);
                if (ds != null)
                {
                    DataTable resultDamageDetail = new DataTable();
                    NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
                    resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                    ViewData["resultDamageDetail"] = resultDamageDetail;
                    

                    if (dtSurvey != null && dtSurvey.Rows.Count > 0)
                    {
                        List<SurveyDamageDetail> objSurveyList = new List<SurveyDamageDetail>();
                        foreach (DataRow dr in dtSurvey.Rows)
                        {
                            SurveyDamageDetail objsurvey = new SurveyDamageDetail();
                            objsurvey.House_owner_id = dr["old_ona_id"].ConvertToString();
                            objsurvey.House_owner_name = dr["old_ona_name"].ConvertToString();
                            objsurvey.Damage_grade = dr["damage_grade"].ConvertToString();
                            objsurvey.Technical_Solution = dr["technical_soln"].ConvertToString();
                            objsurvey.Oth_house_condtion = dr["HIOP_COND"].ConvertToString();
                            objSurveyList.Add(objsurvey);
                        }
                        objModel.LstSurveyDmageDetail = objSurveyList;
                    }


                    if (ds.Tables["Table1"] != null && ds.Tables["Table1"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["Table1"].Rows)
                        {
                            objModel.SurveyorCd = dr["ENUMERATOR_ID"].ConvertToString();
                            objModel.SurveyDate = dr["SURVEY_DATE"].ConvertToString();
                            objModel.OwnDistrictEng = dr["OWNER_DIST_ENG"].ConvertToString();
                            objModel.OwnDistrictLoc = dr["OWNER_DIST_LOC"].ConvertToString();
                            objModel.CurrentVdcMun = dr["NEW_VDC_ENG"].ConvertToString();
                            objModel.CurrentVdcMunLoc = dr["NEW_VDC_LOC"].ConvertToString();
                            objModel.CurrentWard = dr["NEW_WARD"].ConvertToString();
                            objModel.OldVdcMun = dr["OLD_VDC_ENG"].ConvertToString();
                            objModel.OldVdcMunCDLoc = dr["OLD_VDC_LOC"].ConvertToString();
                            objModel.OldWard = dr["WARD_OLD"].ConvertToString();
                            objModel.OldTole = dr["TOLE"].ConvertToString();
                            objModel.InstanceUniqueSno = dr["INSTANCE_UNIQUE_SNO"].ConvertToString();
                            objModel.HouseSno = dr["HOUSE_SNO"].ConvertToString();
                            objModel.NumberOfOwnerFamily = dr["NUMBER_OF_OWNER"].ConvertToString();
                            objModel.GID = dr["G_ID"].ConvertToString();
                            objModel.GID_Status = dr["REGISTERED_NEW"].ConvertToString();

                            if (dr["IS_RESPONDENT_OWNER"].ConvertToString() == "1")
                            {
                                objModel.IsRespondantOwner = "Yes";
                                objModel.OwnerFullNameEng = dr["HH_OWNER_FULL_NAME"].ConvertToString();
                            }
                            else
                            {
                                objModel.RespondantFirstNameEng = dr["RES_F_NAME_ENG"].ConvertToString();
                                objModel.RespondantFirstNameLoc = dr["RES_F_NAME_LOC"].ConvertToString();
                                objModel.RespondantMiddleNameEng = dr["RES_M_NAME_ENG"].ConvertToString();
                                objModel.RespondantMiddleNameLoc = dr["RES_M_NAME_LOC"].ConvertToString();
                                objModel.RespondantLastNameEng = dr["RES_L_NAME_ENG"].ConvertToString();
                                objModel.RespondantLastNameLoc = dr["RES_L_NAME_LOC"].ConvertToString();
                                objModel.RespondantFullNameEng = dr["RES_F_NAME_ENG"].ConvertToString() + " " + dr["RES_M_NAME_ENG"].ConvertToString() + " " + dr["RES_L_NAME_ENG"].ConvertToString();
                                objModel.IsRespondantOwner = "No";
                            }
                            objModel.UseableHouseOwnedByOwner = dr["NO_OF_HOUSE"].ConvertToString();
                            objModel.MunRepreFullNameEng = dr["WARD_REPRESENTATIVE_NAME"].ConvertToString();
                            objModel.WardFullNameEng = dr["WARD_PERSONNEL"].ConvertToString();


                            if (dr["HOUSE_IN_OTHER_PLACE"].ToDecimal() > 0)
                            {
                                objModel.HasOtherHouse = "Yes";
                            }
                            else
                            {
                                objModel.HasOtherHouse = "No";
                            }
                            objModel.HouseOwnerId = dr["HOUSE_OWNER_ID"].ConvertToString();

                            objModel.SubmissionDate = dr["SURVEY_DATE"].ConvertToString();
                            objModel.KLLDistrictCode = dr["KLL_DISTRICT_CD"].ConvertToString();
                            objModel.Defined_cd = dr["DEFINED_CD"].ConvertToString();
                            objModel.RespondantOwnerPhotoUrl = dr["RESPONDENT_PHOTO"].ConvertToString();

                        }
                        string respondentPhotoPath = null;
                        FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
                        string ftpPath = string.Empty;
                        string[] subDate = objModel.SubmissionDate.Split(' ');
                        ftpPath = @"/PhotO/" + "lvsp4/" + objModel.KLLDistrictCode + "/" + subDate[0] + "/" + objModel.Defined_cd + "/";

                        respondentPhotoPath = ftpPath + objModel.RespondantOwnerPhotoUrl;
                        if (objModel.RespondantOwnerPhotoUrl != null && objModel.RespondantOwnerPhotoUrl != "")
                        {
                            try
                            {

                                if (fc.FtpFileExists(respondentPhotoPath))
                                {
                                    fc.Download(respondentPhotoPath, Server.MapPath("~/photo/" + objModel.RespondantOwnerPhotoUrl), true);
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

                        if (ds.Tables["Table2"] != null && ds.Tables["Table2"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> objGrievantList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drG in ds.Tables["Table2"].Rows)
                            {
                                ResurveyHouseViewModel objGrievantModel = new ResurveyHouseViewModel();
                                objGrievantModel.GrievantFullName = drG["GRIEVANT_NAME"].ConvertToString();
                                objGrievantModel.owner_sno = drG["owner_sno"].ConvertToString();
                                objGrievantModel.GID = drG["HH_GID"].ConvertToString();
                                objGrievantModel.OwnerFullNameEng = drG["HOUSE_OWNER_NAME"].ConvertToString();
                                objGrievantModel.InstanceUniqueSno = drG["SLIP_NO"].ConvertToString();
                                objGrievantModel.Old_InstanceUniqueSno = drG["old_slip_no"].ConvertToString();
                                objGrievantList.Add(objGrievantModel);
                            }
                            objModel.GrievantListMain = objGrievantList;
                        }

                        if (ds.Tables["Table3"] != null && ds.Tables["Table3"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> ObjOwnerList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drO in ds.Tables["Table3"].Rows)
                            {
                                ResurveyHouseViewModel objOwnerModel = new ResurveyHouseViewModel();
                                objOwnerModel.OwnerFirstNameEng = drO["HH_OWNER_FIRST_NAME"].ConvertToString();
                                objOwnerModel.OwnerMiddleNameEng = drO["HH_OWNER_MIDDLE_NAME"].ConvertToString();
                                objOwnerModel.OwnerLastNameEng = drO["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOwnerModel.OwnerGenderEng = drO["OWNER_GENDER_ENG"].ConvertToString();
                                objOwnerModel.OwnerGenderLoc = drO["OWNER_GENDER_LOC"].ConvertToString();
                                ObjOwnerList.Add(objOwnerModel);
                            }
                            objModel.OwnerListMain = ObjOwnerList;
                        }


                        if (ds.Tables["Table4"] != null && ds.Tables["Table4"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> ObjRespondentList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drR in ds.Tables["Table4"].Rows)
                            {
                                ResurveyHouseViewModel ObjRespondentModel = new ResurveyHouseViewModel();
                                ObjRespondentModel.RespondantFirstNameEng = drR["RES_FIRST_NAME_ENG"].ConvertToString();
                                // ObjRespondentModel.RespondantFirstNameLoc = drR["RES_FIRST_NAME_LOC"].ConvertToString();
                                ObjRespondentModel.RespondantMiddleNameEng = drR["RES_MIDDLE_NAME_ENG"].ConvertToString();
                                // ObjRespondentModel.RespondantMiddleNameLoc = drR["RES_MIDDLE_NAME_LOC"].ConvertToString();
                                ObjRespondentModel.RespondantLastNameEng = drR["RES_LAST_NAME_ENG"].ConvertToString();
                                //  ObjRespondentModel.RespondantLastNameLoc = drR["RES_LAST_NAME_LOC"].ConvertToString();

                                ObjRespondentModel.RespondantGenderEng = drR["RES_GENDER_ENG"].ConvertToString();
                                ObjRespondentModel.RespondantGenderLoc = drR["RES_GENDER_LOC"].ConvertToString();
                                ObjRespondentModel.ResRelationToOwnerEng = drR["RES_RELATION_ENG"].ConvertToString();
                                ObjRespondentModel.ResRelationToOwnerLoc = drR["RES_RELATION_LOC"].ConvertToString();
                                ObjRespondentList.Add(ObjRespondentModel);
                            }
                            objModel.RespondentListMain = ObjRespondentList;
                        }


                        if (ds.Tables["Table5"] != null && ds.Tables["Table5"].Rows.Count > 0)
                        {
                            List<ResurveyHouseBuildingDetail> ObjBuildingDtlList = new List<ResurveyHouseBuildingDetail>();

                            foreach (DataRow drB in ds.Tables["Table5"].Rows)
                            {
                                ResurveyHouseBuildingDetail ObjBuildingDtlModel = new ResurveyHouseBuildingDetail();
                                ObjBuildingDtlModel.BuildCondiAftrQuakeEng = drB["BUILDING_CONDITION_ENG"].ConvertToString();
                                ObjBuildingDtlModel.BuildCondiAftrQuakeLoc = drB["BUILDING_CONDITION_LOC"].ConvertToString();
                                ObjBuildingDtlModel.StoreyBeforeEarthQuake = drB["STOREY_BEFORE_EARTHQUAKE"].ConvertToString();
                                ObjBuildingDtlModel.StoryAfterEarthQuake = drB["STOREY_AFTER_EARTHQUAKE"].ConvertToString();

                                ObjBuildingDtlModel.DamageGradeEng = drB["DAMAGE_GRADE_ENG"].ConvertToString();
                                ObjBuildingDtlModel.DamageGradeLoc = drB["DAMAGE_GRADE_LOC"].ConvertToString();
                                ObjBuildingDtlModel.TechnicalSolutionEng = drB["TECH_SOL_ENG"].ConvertToString();
                                ObjBuildingDtlModel.TechnicalSolutionLoc = drB["TECH_SOL_LOC"].ConvertToString();
                                if (drB["HAS_GEO_TECH_RISK"].ConvertToString() == "Y")
                                {
                                    ObjBuildingDtlModel.IsGeoTechnicalRisk = "Yes";
                                }
                                else
                                {
                                    ObjBuildingDtlModel.IsGeoTechnicalRisk = "No";
                                }
                                if (drB["RECONSTRUCTION_STARTED"].ConvertToString() == "Y")
                                {
                                    ObjBuildingDtlModel.IsReconstructionStarted = "Yes";
                                }
                                else
                                {
                                    ObjBuildingDtlModel.IsReconstructionStarted = "No";
                                }
                                ObjBuildingDtlModel.Longitude = drB["LONGITUDE"].ConvertToString();
                                ObjBuildingDtlModel.Latitude = drB["LATITUDE"].ConvertToString();
                                ObjBuildingDtlModel.Altitude = drB["ALTITUDE"].ConvertToString();

                                ObjBuildingDtlModel.BuildingStructureNumber = drB["BUILDING_STRUCTURE_NO"].ConvertToString();


                                if (ds.Tables["Table6"] != null && ds.Tables["Table6"].Rows.Count > 0)
                                {
                                    foreach (DataRow drP in ds.Tables["Table6"].Rows)
                                    {
                                        if (drP["BUILDING_STRUCTURE_NO"].ConvertToString() == drB["BUILDING_STRUCTURE_NO"].ConvertToString())
                                        {
                                            string docTypeid = drP["DOC_TYPE_CD"].ConvertToString();
                                            string photoPath = drP["PHOTO_PATH"].ConvertToString();
                                            if (photoPath != "")
                                            {
                                                string Ppath = ftpPath + photoPath;
                                                bool isError = false;
                                                try
                                                {

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

                                            if (docTypeid == "12")
                                            {
                                                ObjBuildingDtlModel.PhotosofFrontdirections = photoPath;

                                            }
                                            //13 back
                                            else if (docTypeid == "13")
                                            {
                                                ObjBuildingDtlModel.PhotosofBackdirections = photoPath;

                                            }
                                            //14 left
                                            else if (docTypeid == "14")
                                            {
                                                ObjBuildingDtlModel.PhotosofLeftdirections = photoPath;

                                            }
                                            //15 right
                                            else if (docTypeid == "15")
                                            {
                                                ObjBuildingDtlModel.PhotosofRightdirections = photoPath;

                                            }
                                            //16 internal 
                                            else if (docTypeid == "16")
                                            {
                                                ObjBuildingDtlModel.Photosofinternaldamageofhouse = photoPath;

                                            }
                                            //17 Rebel
                                            else if (docTypeid == "17")
                                            {
                                                ObjBuildingDtlModel.Fullydamagedhouseslocationsphoto = photoPath;
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


                                    }

                                }

                                if (ds.Tables["Table"] != null && ds.Tables["Table"].Rows.Count > 0)
                                {
                                    List<ResurveyHouseBuildingDetail> ObjBuildingDamageList = new List<ResurveyHouseBuildingDetail>();
                                    ResurveyHouseBuildingDetail ObjBuildingDamageModel = new ResurveyHouseBuildingDetail();
                                    List<string> lstDamageChkBoxes = new List<string>();
                                    string damageLevelStr = String.Empty;
                                    foreach (DataRow drD in ds.Tables["Table"].Rows)
                                    {
                                        if (drD["BUILDING_STRUCTURE_NO"].ConvertToString() == drB["BUILDING_STRUCTURE_NO"].ConvertToString())
                                        {
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "1" ? "SE" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "2" ? "MH" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "3" ? "IN" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "10" ? "NO" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "11" ? "NA" : damageLevelStr;
                                            if (damageLevelStr != "NA" && damageLevelStr != "NO")
                                            {
                                                lstDamageChkBoxes.Add("chkDamageDet_" + drD["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr + "_" + drD["DAMAGE_LEVEL_DTL_CD"].ConvertToString());
                                            }
                                            else
                                            {
                                                lstDamageChkBoxes.Add("chkDamageDet_" + drD["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr);
                                            }
                                        }
                                        ViewData["lstDamageCheckBox" + ObjBuildingDtlModel.BuildingStructureNumber] = lstDamageChkBoxes;
                                    }

                                }
                                objModel.HOUSE_BUILDING_DTL_List.Add(ObjBuildingDtlModel);
                            }




                        }

                        if (ds.Tables["Table7"] != null && ds.Tables["Table7"].Rows.Count > 0)
                        {
                            List<ResurveyOtherHousesDamagedModel> ObjOtherHouseDamageModelList = new List<ResurveyOtherHousesDamagedModel>();

                            foreach (DataRow drOth in ds.Tables["Table7"].Rows)
                            {
                                ResurveyOtherHousesDamagedModel objOtherHouse = new ResurveyOtherHousesDamagedModel();

                                objOtherHouse.FullNameEng = drOth["HH_OWNER_FIRST_NAME"].ConvertToString() + " " + drOth["HH_OWNER_MIDDLE_NAME"].ConvertToString() + " " + drOth["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOtherHouse.FullNameLoc = drOth["HH_OWNER_FIRST_NAME"].ConvertToString() + " " + drOth["HH_OWNER_MIDDLE_NAME"].ConvertToString() + " " + drOth["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOtherHouse.DistrictEng = drOth["OWNER_DIST_ENG"].ConvertToString();
                                objOtherHouse.DistrictLoc = drOth["OWNER_DIST_LOC"].ConvertToString();
                                objOtherHouse.VdcMunEng = drOth["NEW_VDC_ENG"].ConvertToString();
                                objOtherHouse.VdcMunLoc = drOth["NEW_VDC_LOC"].ConvertToString();
                                objOtherHouse.OtherWardNo = drOth["NEW_WARD"].ConvertToString();
                                objOtherHouse.BuildingConditionEng = drOth["BUILDING_CONDTN_ENG"].ConvertToString();
                                objOtherHouse.BuildingConditionLoc = drOth["BUILDING_CONDTN_LOC"].ConvertToString();
                                ObjOtherHouseDamageModelList.Add(objOtherHouse);
                            }
                            objModel.OtherHousesDamagedList = ObjOtherHouseDamageModelList;
                        }

                        if (ds.Tables["Table8"] != null && ds.Tables["Table8"].Rows.Count > 0)
                        {
                            List<ResurveyHouseBuildingDetail> ObjGeoTechnicalRiskList = new List<ResurveyHouseBuildingDetail>();
                            foreach (DataRow drOth in ds.Tables["Table8"].Rows)
                            {
                                ResurveyHouseBuildingDetail ObjGroTechRisk = new ResurveyHouseBuildingDetail();

                                ObjGroTechRisk.GeoRiskEng = drOth["GEO_RISK_ENG"].ConvertToString();
                                ObjGroTechRisk.GeoRiskLoc = drOth["GEO_RISK_LOC"].ConvertToString();


                                ObjGeoTechnicalRiskList.Add(ObjGroTechRisk);
                            }
                            objModel.GeoRiskListmain = ObjGeoTechnicalRiskList;
                        }

                        DataTable dtGrievMemberDetail = objService.GrievMemDetail(houseOwnerId);
                        if (dtGrievMemberDetail != null && dtGrievMemberDetail.Rows.Count > 0)
                        {
                            int count = 1;
                            int less = 1;
                            List<GrvDtlToHouseViewModelclass> ownerList = new List<GrvDtlToHouseViewModelclass>();
                            for (int i = 0; i < dtGrievMemberDetail.Rows.Count; i++)
                            {
                                 GrvDtlToHouseViewModelclass MemDtl = new GrvDtlToHouseViewModelclass();

                                   
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


                                   
                                    ownerList.Add(MemDtl);
                                    less++;

                                }
                                objModel.grievDtlList = ownerList;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


            return View(objModel);
        }
        public ActionResult ResurveySearch(string p)
        {
            ResurveyHouseViewModel ObjModel = new ResurveyHouseViewModel();
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrict("", ""); 
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            return View(ObjModel);
        }
        [HttpPost]
        public ActionResult ResurveySearch(MIS.Models.NHRP.Resurvey.ResurveyHouseViewModel ObjModel)
        {
            ResurveyViewService objService = new ResurveyViewService();
            DataTable dt = new DataTable();
            ObjModel.CurrentVdcMunCD = common.GetCodeFromDataBase(ObjModel.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
            dt = objService.GetResurveyList(ObjModel);
            ViewData["result"] = dt;
            return PartialView("_ResurveySearchList");
        }

        public JsonResult ResurveyPhoto()
        {
            ResurveyHouseViewModel objModel = (ResurveyHouseViewModel)Session["RespondentPhoto"];
            DataSet ds = (DataSet)Session["HousePhoto"];
            FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
            string respondentPhotoPath = null;
            string ftpPath = string.Empty;
            string[] subDate = objModel.SubmissionDate.Split(' ');
            ResurveyHouseBuildingDetail ObjBuildingDtlModel = new ResurveyHouseBuildingDetail();
            string BuildingStructureNumber = Session["BuildingStructureNumber"].ConvertToString();

            ftpPath = @"/PhotO/" + "lvsp4/" + objModel.KLLDistrictCode + "/" + subDate[0] + "/" + objModel.Defined_cd + "/";
            respondentPhotoPath = ftpPath + objModel.RespondantOwnerPhotoUrl;

            if (objModel.RespondantOwnerPhotoUrl != null && objModel.RespondantOwnerPhotoUrl != "")
            {
                try
                {

                    if (fc.FtpFileExists(respondentPhotoPath))
                    {
                        fc.Download(respondentPhotoPath, Server.MapPath("~/photo/" + objModel.RespondantOwnerPhotoUrl), true);
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
            Session["RespondentPhotoUrl"] = objModel.RespondantOwnerPhotoUrl;

            if (ds.Tables["Table6"] != null && ds.Tables["Table6"].Rows.Count > 0)
            {
                foreach (DataRow drP in ds.Tables["Table6"].Rows)
                {
                    if (drP["BUILDING_STRUCTURE_NO"].ConvertToString() == BuildingStructureNumber)
                    {
                        string docTypeid = drP["DOC_TYPE_CD"].ConvertToString();
                        string photoPath = drP["PHOTO_PATH"].ConvertToString();
                        if (photoPath != "")
                        {
                            string Ppath = ftpPath + photoPath;
                            bool isError = false;
                            try
                            {

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

                        if (docTypeid == "12")
                        {
                            ObjBuildingDtlModel.PhotosofFrontdirections = photoPath;
                            Session["RespondentFrontPhoto"] = ObjBuildingDtlModel.PhotosofFrontdirections;
                        }
                        //13 back
                        else if (docTypeid == "13")
                        {
                            ObjBuildingDtlModel.PhotosofBackdirections = photoPath;
                            Session["RespondentBackPhoto"] = ObjBuildingDtlModel.PhotosofBackdirections;
                        }
                        //14 left
                        else if (docTypeid == "14")
                        {
                            ObjBuildingDtlModel.PhotosofLeftdirections = photoPath;
                            Session["RespondentLeftPhoto"] = ObjBuildingDtlModel.PhotosofLeftdirections;
                        }
                        //15 right
                        else if (docTypeid == "15")
                        {
                            ObjBuildingDtlModel.PhotosofRightdirections = photoPath;
                            Session["RespondentRightPhoto"] = ObjBuildingDtlModel.PhotosofRightdirections;
                        }
                        //16 internal 
                        else if (docTypeid == "16")
                        {
                            ObjBuildingDtlModel.Photosofinternaldamageofhouse = photoPath;
                            Session["RespondentInternalPhoto"] = ObjBuildingDtlModel.Photosofinternaldamageofhouse;
                        }
                        //17 Rebel
                        else if (docTypeid == "17")
                        {
                            ObjBuildingDtlModel.Fullydamagedhouseslocationsphoto = photoPath;
                            Session["RespondentFullyDamagePhoto"] = ObjBuildingDtlModel.Fullydamagedhouseslocationsphoto;
                        }
                        //18 damage photo1
                        else if (docTypeid == "18")
                        {

                        }
                        //19 damage photo2
                        else if (docTypeid == "19")
                        {

                        }
                    }


                }

            }
                        
            return Json("PA");
        }

        [HttpPost]
        public JsonResult ResurveyRecommendation(List<ResurveyRecommendation> grade)
        {
            ResurveyViewService objService = new ResurveyViewService();
            QueryResult qr = new QueryResult();
            ResurveyRecommendation ObjModel = new ResurveyRecommendation();
           
            qr = objService.InsertResRecommendation(grade);

            return Json("0");
        }

        public ActionResult ResurveyReviewSearch(string p)
        {
            ResurveyHouseViewModel ObjModel = new ResurveyHouseViewModel();
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            return View(ObjModel);
        }
        [HttpPost]
        public ActionResult ResurveyReviewSearch(MIS.Models.NHRP.Resurvey.ResurveyHouseViewModel ObjModel)
        {
            ResurveyViewService objService = new ResurveyViewService();
            DataTable dt = new DataTable();
            ObjModel.CurrentVdcMunCD = common.GetCodeFromDataBase(ObjModel.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
            dt = objService.GetResurveyReviewList(ObjModel);
            ViewData["result"] = dt;
            return PartialView("_ResurveyReview");
        }
        public ActionResult ResurveyReviewHouseView(string p)
        {
            ResurveyViewService objService = new ResurveyViewService();
            ResurveyHouseViewModel objModel = new ResurveyHouseViewModel();
            DataSet ds = new DataSet();
            string houseOwnerId = null;
            DataTable isVerified = new DataTable();
            ViewData["ddl_majorDamage"] = MajorDamage();
            try
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                        rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                         houseOwnerId = (rvd["id2"]).ConvertToString();
                    }
                }
                isVerified = objService.IsOwnerIdVerified(houseOwnerId);
                if (isVerified != null && isVerified.Rows.Count > 0)
                {
                    ViewData["owner_id_status"] = "VERIFIED";
                    ViewData["Recommended_Data"] = isVerified;
                }
                else
                {
                    ViewData["owner_id_status"] = "UNVERIFIED";
                }

                ds = objService.ResurveyHouseView(houseOwnerId, "");
                DataTable dtSurvey = new DataTable();
                dtSurvey = objService.GetSurveyData(houseOwnerId);
                if (ds != null)
                {
                    DataTable resultDamageDetail = new DataTable();
                    NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
                    resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                    ViewData["resultDamageDetail"] = resultDamageDetail;

                    DataTable DamageStructure = new DataTable();
                    DamageStructure = objNHRSHouseDetail.GetAllDamagestructure();

                    List<SelectListItem> damageStc = new List<SelectListItem>();
                    foreach(DataRow item in DamageStructure.Rows)
                    {
                        damageStc.Add(new SelectListItem { Text = item["desc_eng"].ToString(), Value = "" });
                    }

                    ViewData["ddl_structure"] = damageStc;


                    if (dtSurvey != null && dtSurvey.Rows.Count > 0)
                    {
                        List<SurveyDamageDetail> objSurveyList = new List<SurveyDamageDetail>();
                        foreach (DataRow dr in dtSurvey.Rows)
                        {
                            SurveyDamageDetail objsurvey = new SurveyDamageDetail();
                            objsurvey.House_owner_id = dr["old_ona_id"].ConvertToString();
                            objsurvey.House_owner_name = dr["old_ona_name"].ConvertToString();
                            objsurvey.Damage_grade = dr["damage_grade"].ConvertToString();
                            objsurvey.Technical_Solution = dr["technical_soln"].ConvertToString();
                            objsurvey.Oth_house_condtion = dr["HIOP_COND"].ConvertToString();
                            objSurveyList.Add(objsurvey);
                        }
                        objModel.LstSurveyDmageDetail = objSurveyList;
                    }


                    if (ds.Tables["Table1"] != null && ds.Tables["Table1"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["Table1"].Rows)
                        {
                            objModel.SurveyorCd = dr["ENUMERATOR_ID"].ConvertToString();
                            objModel.SurveyDate = dr["SURVEY_DATE"].ConvertToString();
                            objModel.OwnDistrictEng = dr["OWNER_DIST_ENG"].ConvertToString();
                            objModel.OwnDistrictLoc = dr["OWNER_DIST_LOC"].ConvertToString();
                            objModel.CurrentVdcMun = dr["NEW_VDC_ENG"].ConvertToString();
                            objModel.CurrentVdcMunLoc = dr["NEW_VDC_LOC"].ConvertToString();
                            objModel.CurrentWard = dr["NEW_WARD"].ConvertToString();
                            objModel.OldVdcMun = dr["OLD_VDC_ENG"].ConvertToString();
                            objModel.OldVdcMunCDLoc = dr["OLD_VDC_LOC"].ConvertToString();
                            objModel.OldWard = dr["WARD_OLD"].ConvertToString();
                            objModel.OldTole = dr["TOLE"].ConvertToString();
                            objModel.InstanceUniqueSno = dr["INSTANCE_UNIQUE_SNO"].ConvertToString();
                            objModel.HouseSno = dr["HOUSE_SNO"].ConvertToString();
                            objModel.NumberOfOwnerFamily = dr["NUMBER_OF_OWNER"].ConvertToString();
                            objModel.GID = dr["G_ID"].ConvertToString();
                            objModel.GID_Status = dr["REGISTERED_NEW"].ConvertToString();
                            objModel.NewDamageGrade = dr["sdg_sts"].ConvertToString();
                          

                            if (dr["IS_RESPONDENT_OWNER"].ConvertToString() == "1")
                            {
                                objModel.IsRespondantOwner = "Yes";
                                objModel.OwnerFullNameEng = dr["HH_OWNER_FULL_NAME"].ConvertToString();
                            }
                            else
                            {
                                objModel.RespondantFirstNameEng = dr["RES_F_NAME_ENG"].ConvertToString();
                                objModel.RespondantFirstNameLoc = dr["RES_F_NAME_LOC"].ConvertToString();
                                objModel.RespondantMiddleNameEng = dr["RES_M_NAME_ENG"].ConvertToString();
                                objModel.RespondantMiddleNameLoc = dr["RES_M_NAME_LOC"].ConvertToString();
                                objModel.RespondantLastNameEng = dr["RES_L_NAME_ENG"].ConvertToString();
                                objModel.RespondantLastNameLoc = dr["RES_L_NAME_LOC"].ConvertToString();
                                objModel.RespondantFullNameEng = dr["RES_F_NAME_ENG"].ConvertToString() + " " + dr["RES_M_NAME_ENG"].ConvertToString() + " " + dr["RES_L_NAME_ENG"].ConvertToString();
                                objModel.IsRespondantOwner = "No";
                            }
                            objModel.UseableHouseOwnedByOwner = dr["NO_OF_HOUSE"].ConvertToString();
                            objModel.MunRepreFullNameEng = dr["WARD_REPRESENTATIVE_NAME"].ConvertToString();
                            objModel.WardFullNameEng = dr["WARD_PERSONNEL"].ConvertToString();


                            if (dr["HOUSE_IN_OTHER_PLACE"].ToDecimal() > 0)
                            {
                                objModel.HasOtherHouse = "Yes";
                            }
                            else
                            {
                                objModel.HasOtherHouse = "No";
                            }
                            objModel.HouseOwnerId = dr["HOUSE_OWNER_ID"].ConvertToString();

                            objModel.SubmissionDate = dr["SURVEY_DATE"].ConvertToString();
                            objModel.KLLDistrictCode = dr["KLL_DISTRICT_CD"].ConvertToString();
                            objModel.Defined_cd = dr["DEFINED_CD"].ConvertToString();
                            objModel.RespondantOwnerPhotoUrl = dr["RESPONDENT_PHOTO"].ConvertToString();

                        }
                        string respondentPhotoPath = null;
                        FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
                        string ftpPath = string.Empty;
                        string[] subDate = objModel.SubmissionDate.Split(' ');
                        ftpPath = @"/PhotO/" + "lvsp4/" + objModel.KLLDistrictCode + "/" + subDate[0] + "/" + objModel.Defined_cd + "/";

                        respondentPhotoPath = ftpPath + objModel.RespondantOwnerPhotoUrl;
                        if (objModel.RespondantOwnerPhotoUrl != null && objModel.RespondantOwnerPhotoUrl != "")
                        {
                            try
                            {

                                if (fc.FtpFileExists(respondentPhotoPath))
                                {
                                    fc.Download(respondentPhotoPath, Server.MapPath("~/photo/" + objModel.RespondantOwnerPhotoUrl), true);
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

                        if (ds.Tables["Table2"] != null && ds.Tables["Table2"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> objGrievantList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drG in ds.Tables["Table2"].Rows)
                            {
                                ResurveyHouseViewModel objGrievantModel = new ResurveyHouseViewModel();
                                objGrievantModel.GrievantFullName = drG["GRIEVANT_NAME"].ConvertToString();
                                objGrievantModel.owner_sno = drG["owner_sno"].ConvertToString();
                                objGrievantModel.GID = drG["HH_GID"].ConvertToString();
                                objGrievantModel.OwnerFullNameEng = drG["HOUSE_OWNER_NAME"].ConvertToString();
                                objGrievantModel.InstanceUniqueSno = drG["SLIP_NO"].ConvertToString();
                                objGrievantModel.Old_InstanceUniqueSno = drG["old_slip_no"].ConvertToString();
                                objGrievantList.Add(objGrievantModel);
                            }
                            objModel.GrievantListMain = objGrievantList;
                        }

                        if (ds.Tables["Table3"] != null && ds.Tables["Table3"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> ObjOwnerList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drO in ds.Tables["Table3"].Rows)
                            {
                                ResurveyHouseViewModel objOwnerModel = new ResurveyHouseViewModel();
                                objOwnerModel.OwnerFirstNameEng = drO["HH_OWNER_FIRST_NAME"].ConvertToString();
                                objOwnerModel.OwnerMiddleNameEng = drO["HH_OWNER_MIDDLE_NAME"].ConvertToString();
                                objOwnerModel.OwnerLastNameEng = drO["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOwnerModel.OwnerGenderEng = drO["OWNER_GENDER_ENG"].ConvertToString();
                                objOwnerModel.OwnerGenderLoc = drO["OWNER_GENDER_LOC"].ConvertToString();
                                ObjOwnerList.Add(objOwnerModel);
                            }
                            objModel.OwnerListMain = ObjOwnerList;
                        }


                        if (ds.Tables["Table4"] != null && ds.Tables["Table4"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> ObjRespondentList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drR in ds.Tables["Table4"].Rows)
                            {
                                ResurveyHouseViewModel ObjRespondentModel = new ResurveyHouseViewModel();
                                ObjRespondentModel.RespondantFirstNameEng = drR["RES_FIRST_NAME_ENG"].ConvertToString();
                                // ObjRespondentModel.RespondantFirstNameLoc = drR["RES_FIRST_NAME_LOC"].ConvertToString();
                                ObjRespondentModel.RespondantMiddleNameEng = drR["RES_MIDDLE_NAME_ENG"].ConvertToString();
                                // ObjRespondentModel.RespondantMiddleNameLoc = drR["RES_MIDDLE_NAME_LOC"].ConvertToString();
                                ObjRespondentModel.RespondantLastNameEng = drR["RES_LAST_NAME_ENG"].ConvertToString();
                                //  ObjRespondentModel.RespondantLastNameLoc = drR["RES_LAST_NAME_LOC"].ConvertToString();

                                ObjRespondentModel.RespondantGenderEng = drR["RES_GENDER_ENG"].ConvertToString();
                                ObjRespondentModel.RespondantGenderLoc = drR["RES_GENDER_LOC"].ConvertToString();
                                ObjRespondentModel.ResRelationToOwnerEng = drR["RES_RELATION_ENG"].ConvertToString();
                                ObjRespondentModel.ResRelationToOwnerLoc = drR["RES_RELATION_LOC"].ConvertToString();
                                ObjRespondentList.Add(ObjRespondentModel);
                            }
                            objModel.RespondentListMain = ObjRespondentList;
                        }


                        if (ds.Tables["Table5"] != null && ds.Tables["Table5"].Rows.Count > 0)
                        {
                            List<ResurveyHouseBuildingDetail> ObjBuildingDtlList = new List<ResurveyHouseBuildingDetail>();

                            foreach (DataRow drB in ds.Tables["Table5"].Rows)
                            {
                                ResurveyHouseBuildingDetail ObjBuildingDtlModel = new ResurveyHouseBuildingDetail();
                                ObjBuildingDtlModel.BuildCondiAftrQuakeEng = drB["BUILDING_CONDITION_ENG"].ConvertToString();
                                ObjBuildingDtlModel.BuildCondiAftrQuakeLoc = drB["BUILDING_CONDITION_LOC"].ConvertToString();
                                ObjBuildingDtlModel.StoreyBeforeEarthQuake = drB["STOREY_BEFORE_EARTHQUAKE"].ConvertToString();
                                ObjBuildingDtlModel.StoryAfterEarthQuake = drB["STOREY_AFTER_EARTHQUAKE"].ConvertToString();

                                ObjBuildingDtlModel.DamageGradeEng = drB["DAMAGE_GRADE_ENG"].ConvertToString();
                                ObjBuildingDtlModel.DamageGradeLoc = drB["DAMAGE_GRADE_LOC"].ConvertToString();
                                ObjBuildingDtlModel.TechnicalSolutionEng = drB["TECH_SOL_ENG"].ConvertToString();
                                ObjBuildingDtlModel.TechnicalSolutionLoc = drB["TECH_SOL_LOC"].ConvertToString();
                                if (drB["HAS_GEO_TECH_RISK"].ConvertToString() == "Y")
                                {
                                    ObjBuildingDtlModel.IsGeoTechnicalRisk = "Yes";
                                }
                                else
                                {
                                    ObjBuildingDtlModel.IsGeoTechnicalRisk = "No";
                                }
                                if (drB["RECONSTRUCTION_STARTED"].ConvertToString() == "Y")
                                {
                                    ObjBuildingDtlModel.IsReconstructionStarted = "Yes";
                                }
                                else
                                {
                                    ObjBuildingDtlModel.IsReconstructionStarted = "No";
                                }
                                ObjBuildingDtlModel.Longitude = drB["LONGITUDE"].ConvertToString();
                                ObjBuildingDtlModel.Latitude = drB["LATITUDE"].ConvertToString();
                                ObjBuildingDtlModel.Altitude = drB["ALTITUDE"].ConvertToString();

                                ObjBuildingDtlModel.BuildingStructureNumber = drB["BUILDING_STRUCTURE_NO"].ConvertToString();


                                if (ds.Tables["Table6"] != null && ds.Tables["Table6"].Rows.Count > 0)
                                {
                                    foreach (DataRow drP in ds.Tables["Table6"].Rows)
                                    {
                                        if (drP["BUILDING_STRUCTURE_NO"].ConvertToString() == drB["BUILDING_STRUCTURE_NO"].ConvertToString())
                                        {
                                            string docTypeid = drP["DOC_TYPE_CD"].ConvertToString();
                                            string photoPath = drP["PHOTO_PATH"].ConvertToString();
                                            if (photoPath != "")
                                            {
                                                string Ppath = ftpPath + photoPath;
                                                bool isError = false;
                                                try
                                                {

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

                                            if (docTypeid == "12")
                                            {
                                                ObjBuildingDtlModel.PhotosofFrontdirections = photoPath;

                                            }
                                            //13 back
                                            else if (docTypeid == "13")
                                            {
                                                ObjBuildingDtlModel.PhotosofBackdirections = photoPath;

                                            }
                                            //14 left
                                            else if (docTypeid == "14")
                                            {
                                                ObjBuildingDtlModel.PhotosofLeftdirections = photoPath;

                                            }
                                            //15 right
                                            else if (docTypeid == "15")
                                            {
                                                ObjBuildingDtlModel.PhotosofRightdirections = photoPath;

                                            }
                                            //16 internal 
                                            else if (docTypeid == "16")
                                            {
                                                ObjBuildingDtlModel.Photosofinternaldamageofhouse = photoPath;

                                            }
                                            //17 Rebel
                                            else if (docTypeid == "17")
                                            {
                                                ObjBuildingDtlModel.Fullydamagedhouseslocationsphoto = photoPath;
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


                                    }

                                }

                                if (ds.Tables["Table"] != null && ds.Tables["Table"].Rows.Count > 0)
                                {
                                    List<ResurveyHouseBuildingDetail> ObjBuildingDamageList = new List<ResurveyHouseBuildingDetail>();
                                    ResurveyHouseBuildingDetail ObjBuildingDamageModel = new ResurveyHouseBuildingDetail();
                                    List<string> lstDamageChkBoxes = new List<string>();
                                    string damageLevelStr = String.Empty;
                                    foreach (DataRow drD in ds.Tables["Table"].Rows)
                                    {
                                        if (drD["BUILDING_STRUCTURE_NO"].ConvertToString() == drB["BUILDING_STRUCTURE_NO"].ConvertToString())
                                        {
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "1" ? "SE" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "2" ? "MH" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "3" ? "IN" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "10" ? "NO" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "11" ? "NA" : damageLevelStr;
                                            if (damageLevelStr != "NA" && damageLevelStr != "NO")
                                            {
                                                lstDamageChkBoxes.Add("chkDamageDet_" + drD["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr + "_" + drD["DAMAGE_LEVEL_DTL_CD"].ConvertToString());
                                            }
                                            else
                                            {
                                                lstDamageChkBoxes.Add("chkDamageDet_" + drD["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr);
                                            }
                                        }
                                        ViewData["lstDamageCheckBox" + ObjBuildingDtlModel.BuildingStructureNumber] = lstDamageChkBoxes;
                                    }

                                }
                                objModel.HOUSE_BUILDING_DTL_List.Add(ObjBuildingDtlModel);
                            }




                        }

                        if (ds.Tables["Table7"] != null && ds.Tables["Table7"].Rows.Count > 0)
                        {
                            List<ResurveyOtherHousesDamagedModel> ObjOtherHouseDamageModelList = new List<ResurveyOtherHousesDamagedModel>();

                            foreach (DataRow drOth in ds.Tables["Table7"].Rows)
                            {
                                ResurveyOtherHousesDamagedModel objOtherHouse = new ResurveyOtherHousesDamagedModel();

                                objOtherHouse.FullNameEng = drOth["HH_OWNER_FIRST_NAME"].ConvertToString() + " " + drOth["HH_OWNER_MIDDLE_NAME"].ConvertToString() + " " + drOth["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOtherHouse.FullNameLoc = drOth["HH_OWNER_FIRST_NAME"].ConvertToString() + " " + drOth["HH_OWNER_MIDDLE_NAME"].ConvertToString() + " " + drOth["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOtherHouse.DistrictEng = drOth["OWNER_DIST_ENG"].ConvertToString();
                                objOtherHouse.DistrictLoc = drOth["OWNER_DIST_LOC"].ConvertToString();
                                objOtherHouse.VdcMunEng = drOth["NEW_VDC_ENG"].ConvertToString();
                                objOtherHouse.VdcMunLoc = drOth["NEW_VDC_LOC"].ConvertToString();
                                objOtherHouse.OtherWardNo = drOth["NEW_WARD"].ConvertToString();
                                objOtherHouse.BuildingConditionEng = drOth["BUILDING_CONDTN_ENG"].ConvertToString();
                                objOtherHouse.BuildingConditionLoc = drOth["BUILDING_CONDTN_LOC"].ConvertToString();
                                ObjOtherHouseDamageModelList.Add(objOtherHouse);
                            }
                            objModel.OtherHousesDamagedList = ObjOtherHouseDamageModelList;
                        }

                        if (ds.Tables["Table8"] != null && ds.Tables["Table8"].Rows.Count > 0)
                        {
                            List<ResurveyHouseBuildingDetail> ObjGeoTechnicalRiskList = new List<ResurveyHouseBuildingDetail>();
                            foreach (DataRow drOth in ds.Tables["Table8"].Rows)
                            {
                                ResurveyHouseBuildingDetail ObjGroTechRisk = new ResurveyHouseBuildingDetail();

                                ObjGroTechRisk.GeoRiskEng = drOth["GEO_RISK_ENG"].ConvertToString();
                                ObjGroTechRisk.GeoRiskLoc = drOth["GEO_RISK_LOC"].ConvertToString();


                                ObjGeoTechnicalRiskList.Add(ObjGroTechRisk);
                            }
                            objModel.GeoRiskListmain = ObjGeoTechnicalRiskList;
                        }

                        if (ds.Tables["Table9"] != null && ds.Tables["Table9"].Rows.Count > 0)
                        {
                            objModel.Super_Structure = ds.Tables["Table9"].Rows[0]["SUPER_STRUCTURE"].ConvertToString();
                        } 

                        if (ds.Tables["Table10"] != null && ds.Tables["Table10"].Rows.Count > 0)
                        { 
                            objModel.Previous_Damage_Grade = ds.Tables["Table10"].Rows[0]["SDG_STS(P)"].ConvertToString();
                        }

                        DataTable dtGrievMemberDetail = objService.GrievMemDetail(houseOwnerId);
                        if (dtGrievMemberDetail != null && dtGrievMemberDetail.Rows.Count > 0)
                        {
                            int less = 1;
                            List<GrvDtlToHouseViewModelclass> ownerList = new List<GrvDtlToHouseViewModelclass>();
                            for (int i = 0; i < dtGrievMemberDetail.Rows.Count; i++)
                            {
                                GrvDtlToHouseViewModelclass MemDtl = new GrvDtlToHouseViewModelclass();


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



                                ownerList.Add(MemDtl);
                                less++;

                            }
                            objModel.grievDtlList = ownerList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


            return View(objModel);
        }

        public bool GetFtpImages(string accordianId, string model)
        {

            if (string.IsNullOrEmpty(model)) return false;
            var objectData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(model);
            if (objectData == null) return false;
            FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");

            try
            {
                var dd = objectData[0].RespondantOwnerPhotoUrl.ToString();
                switch (accordianId)
                {
                    case "3":
                        string ftpPath = string.Empty;
                        string[] subDate = objectData[0].SubmissionDate.ToString().Split(' ');
                        ftpPath = @"/PhotO/" + "lvsp4/" + objectData[0].KLLDistrictCode.ToString() + "/" + subDate[0] + "/" + objectData[0].Defined_cd.ToString() + "/";

                        ftpPath = ftpPath + objectData[0].RespondantOwnerPhotoUrl.ToString();

                        if (fc.FtpFileExists(ftpPath))
                        {
                            fc.Download(ftpPath, Server.MapPath("~/photo/" + objectData[0].RespondantOwnerPhotoUrl.ToString()), true);
                        }
                        break;

                    case "4":
                        if (fc.FtpFileExists(objectData[0].PhotosofFrontdirections))
                        {
                            fc.Download(objectData[0].PhotosofFrontdirections, Server.MapPath("~/photo/" + objectData[0].PhotosofFrontdirections), true);
                        }
                        if (fc.FtpFileExists(objectData[0].Fullydamagedhouseslocationsphoto))
                        {
                            fc.Download(objectData[0].Fullydamagedhouseslocationsphoto, Server.MapPath("~/photo/" + objectData[0].Fullydamagedhouseslocationsphoto), true);
                        }
                        if (fc.FtpFileExists(objectData[0].Photosofinternaldamageofhouse))
                        {
                            fc.Download(objectData[0].Photosofinternaldamageofhouse, Server.MapPath("~/photo/" + objectData[0].Photosofinternaldamageofhouse), true);
                        }
                        if (fc.FtpFileExists(objectData[0].PhotosofBackdirections))
                        {
                            fc.Download(objectData[0].PhotosofBackdirections, Server.MapPath("~/photo/" + objectData[0].PhotosofBackdirections), true);
                        }
                        if (fc.FtpFileExists(objectData[0].PhotosofLeftdirections))
                        {
                            fc.Download(objectData[0].PhotosofLeftdirections, Server.MapPath("~/photo/" + objectData[0].PhotosofLeftdirections), true);
                        }
                        if (fc.FtpFileExists(objectData[0].PhotosofRightdirections))
                        {
                            fc.Download(objectData[0].PhotosofRightdirections, Server.MapPath("~/photo/" + objectData[0].PhotosofRightdirections), true);
                        }
                        break;
                }

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return false;
            }
            return true;
        }

        public ActionResult GoToSurveyPage(string GID)
        {
            string id = string.Empty;
            ResurveyViewService objService = new ResurveyViewService();
            DataTable house_owner_id = objService.GetOldHouseOwnerId(GID,string.Empty);

            if(house_owner_id != null && house_owner_id.Rows.Count > 0)
            {
                id = house_owner_id.Rows[0][0].ConvertToString();
                DataTable dtHouseDetail = new DataTable();
                dtHouseDetail = null;
                DataTable dtHouseOwner = new DataTable();
                dtHouseOwner = null;
                NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
                VW_HOUSE_OWNER_DTL ownerDtlModel = new VW_HOUSE_OWNER_DTL();
                VW_HOUSE_BUILDING_DTL builidingDtlModel = new VW_HOUSE_BUILDING_DTL();
              
                try
                {
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
                                    ftpPath = @"/PhotO/" + "/17districts/" + "0" + ownerDtlModel.KLLDistrictCode + "/" + subDate[0] + "/" + ownerDtlModel.DEFINED_CD + "/";
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
                return View("_SurveyVerificationView", ownerDtlModel);
            }
            else
            {
                return Json("false");
            }
        }

        public ActionResult ResurveyVerification()
        {
            ResurveyHouseViewModel ObjModel = new ResurveyHouseViewModel();
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            return View(ObjModel);
        }

        [HttpPost]
        public ActionResult ResurveyVerification(MIS.Models.NHRP.Resurvey.ResurveyHouseViewModel ObjModel)
        {
            ResurveyViewService objService = new ResurveyViewService();
            DataTable dt = new DataTable();
            ObjModel.CurrentVdcMunCD = common.GetCodeFromDataBase(ObjModel.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
            dt = objService.GetResurveyList(ObjModel);
            ViewData["result"] = dt;
            return PartialView("_ResurveyVerificationSearch");
        }

        [HttpPost]
        public ActionResult VerificationResurveyHouseView(string ho_id)
        {

            ResurveyViewService objService = new ResurveyViewService();
            ResurveyHouseViewModel objModel = new ResurveyHouseViewModel();
            DataSet ds = new DataSet();
            string houseOwnerId = ho_id;
            DataTable isVerified = new DataTable();
            try
            {
                
                isVerified = objService.IsOwnerIdVerified(houseOwnerId);
                if (isVerified != null && isVerified.Rows.Count > 0)
                {
                    ViewData["owner_id_status"] = "VERIFIED";
                    ViewData["Recommended_Data"] = isVerified;
                }
                else
                {
                    ViewData["owner_id_status"] = "UNVERIFIED";
                }

                ds = objService.ResurveyHouseView(houseOwnerId, "");
                DataTable dtSurvey = new DataTable();
                dtSurvey = objService.GetSurveyData(houseOwnerId);
                if (ds != null)
                {
                    DataTable resultDamageDetail = new DataTable();
                    NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
                    resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                    ViewData["resultDamageDetail"] = resultDamageDetail;

                    if (dtSurvey != null && dtSurvey.Rows.Count > 0)
                    {
                        List<SurveyDamageDetail> objSurveyList = new List<SurveyDamageDetail>();
                        foreach (DataRow dr in dtSurvey.Rows)
                        {
                            SurveyDamageDetail objsurvey = new SurveyDamageDetail();
                            objsurvey.House_owner_id = dr["old_ona_id"].ConvertToString();
                            objsurvey.House_owner_name = dr["old_ona_name"].ConvertToString();
                            objsurvey.Damage_grade = dr["damage_grade"].ConvertToString();
                            objsurvey.Technical_Solution = dr["technical_soln"].ConvertToString();
                            objsurvey.Oth_house_condtion = dr["HIOP_COND"].ConvertToString();
                            objSurveyList.Add(objsurvey);
                        }
                        objModel.LstSurveyDmageDetail = objSurveyList;
                    }


                    if (ds.Tables["Table1"] != null && ds.Tables["Table1"].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables["Table1"].Rows)
                        {
                            objModel.SurveyorCd = dr["ENUMERATOR_ID"].ConvertToString();
                            objModel.SurveyDate = dr["SURVEY_DATE"].ConvertToString();
                            objModel.OwnDistrictEng = dr["OWNER_DIST_ENG"].ConvertToString();
                            objModel.OwnDistrictLoc = dr["OWNER_DIST_LOC"].ConvertToString();
                            objModel.CurrentVdcMun = dr["NEW_VDC_ENG"].ConvertToString();
                            objModel.CurrentVdcMunLoc = dr["NEW_VDC_LOC"].ConvertToString();
                            objModel.CurrentWard = dr["NEW_WARD"].ConvertToString();
                            objModel.OldVdcMun = dr["OLD_VDC_ENG"].ConvertToString();
                            objModel.OldVdcMunCDLoc = dr["OLD_VDC_LOC"].ConvertToString();
                            objModel.OldWard = dr["WARD_OLD"].ConvertToString();
                            objModel.OldTole = dr["TOLE"].ConvertToString();
                            objModel.InstanceUniqueSno = dr["INSTANCE_UNIQUE_SNO"].ConvertToString();
                            objModel.HouseSno = dr["HOUSE_SNO"].ConvertToString();
                            objModel.NumberOfOwnerFamily = dr["NUMBER_OF_OWNER"].ConvertToString();
                            objModel.GID = dr["G_ID"].ConvertToString();
                            objModel.GID_Status = dr["REGISTERED_NEW"].ConvertToString();

                            if (dr["IS_RESPONDENT_OWNER"].ConvertToString() == "1")
                            {
                                objModel.IsRespondantOwner = "Yes";
                                objModel.OwnerFullNameEng = dr["HH_OWNER_FULL_NAME"].ConvertToString();
                            }
                            else
                            {
                                objModel.RespondantFirstNameEng = dr["RES_F_NAME_ENG"].ConvertToString();
                                objModel.RespondantFirstNameLoc = dr["RES_F_NAME_LOC"].ConvertToString();
                                objModel.RespondantMiddleNameEng = dr["RES_M_NAME_ENG"].ConvertToString();
                                objModel.RespondantMiddleNameLoc = dr["RES_M_NAME_LOC"].ConvertToString();
                                objModel.RespondantLastNameEng = dr["RES_L_NAME_ENG"].ConvertToString();
                                objModel.RespondantLastNameLoc = dr["RES_L_NAME_LOC"].ConvertToString();
                                objModel.RespondantFullNameEng = dr["RES_F_NAME_ENG"].ConvertToString() + " " + dr["RES_M_NAME_ENG"].ConvertToString() + " " + dr["RES_L_NAME_ENG"].ConvertToString();
                                objModel.IsRespondantOwner = "No";
                            }
                            objModel.UseableHouseOwnedByOwner = dr["NO_OF_HOUSE"].ConvertToString();
                            objModel.MunRepreFullNameEng = dr["WARD_REPRESENTATIVE_NAME"].ConvertToString();
                            objModel.WardFullNameEng = dr["WARD_PERSONNEL"].ConvertToString();


                            if (dr["HOUSE_IN_OTHER_PLACE"].ToDecimal() > 0)
                            {
                                objModel.HasOtherHouse = "Yes";
                            }
                            else
                            {
                                objModel.HasOtherHouse = "No";
                            }
                            objModel.HouseOwnerId = dr["HOUSE_OWNER_ID"].ConvertToString();

                            objModel.SubmissionDate = dr["SURVEY_DATE"].ConvertToString();
                            objModel.KLLDistrictCode = dr["KLL_DISTRICT_CD"].ConvertToString();
                            objModel.Defined_cd = dr["DEFINED_CD"].ConvertToString();
                            objModel.RespondantOwnerPhotoUrl = dr["RESPONDENT_PHOTO"].ConvertToString();

                        }
                        string respondentPhotoPath = null;
                        FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
                        string ftpPath = string.Empty;
                        string[] subDate = objModel.SubmissionDate.Split(' ');
                        ftpPath = @"/PhotO/" + "lvsp4/" + objModel.KLLDistrictCode + "/" + subDate[0] + "/" + objModel.Defined_cd + "/";

                        respondentPhotoPath = ftpPath + objModel.RespondantOwnerPhotoUrl;
                        if (objModel.RespondantOwnerPhotoUrl != null && objModel.RespondantOwnerPhotoUrl != "")
                        {
                            try
                            {

                                if (fc.FtpFileExists(respondentPhotoPath))
                                {
                                    fc.Download(respondentPhotoPath, Server.MapPath("~/photo/" + objModel.RespondantOwnerPhotoUrl), true);
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

                        if (ds.Tables["Table2"] != null && ds.Tables["Table2"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> objGrievantList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drG in ds.Tables["Table2"].Rows)
                            {
                                ResurveyHouseViewModel objGrievantModel = new ResurveyHouseViewModel();
                                objGrievantModel.GrievantFullName = drG["GRIEVANT_NAME"].ConvertToString();
                                objGrievantModel.owner_sno = drG["owner_sno"].ConvertToString();
                                objGrievantModel.GID = drG["HH_GID"].ConvertToString();
                                objGrievantModel.OwnerFullNameEng = drG["HOUSE_OWNER_NAME"].ConvertToString();
                                objGrievantModel.InstanceUniqueSno = drG["SLIP_NO"].ConvertToString();
                                objGrievantModel.Old_InstanceUniqueSno = drG["old_slip_no"].ConvertToString();
                                objGrievantList.Add(objGrievantModel);
                            }
                            objModel.GrievantListMain = objGrievantList;
                        }

                        if (ds.Tables["Table3"] != null && ds.Tables["Table3"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> ObjOwnerList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drO in ds.Tables["Table3"].Rows)
                            {
                                ResurveyHouseViewModel objOwnerModel = new ResurveyHouseViewModel();
                                objOwnerModel.OwnerFirstNameEng = drO["HH_OWNER_FIRST_NAME"].ConvertToString();
                                objOwnerModel.OwnerMiddleNameEng = drO["HH_OWNER_MIDDLE_NAME"].ConvertToString();
                                objOwnerModel.OwnerLastNameEng = drO["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOwnerModel.OwnerGenderEng = drO["OWNER_GENDER_ENG"].ConvertToString();
                                objOwnerModel.OwnerGenderLoc = drO["OWNER_GENDER_LOC"].ConvertToString();
                                ObjOwnerList.Add(objOwnerModel);
                            }
                            objModel.OwnerListMain = ObjOwnerList;
                        }


                        if (ds.Tables["Table4"] != null && ds.Tables["Table4"].Rows.Count > 0)
                        {
                            List<ResurveyHouseViewModel> ObjRespondentList = new List<ResurveyHouseViewModel>();

                            foreach (DataRow drR in ds.Tables["Table4"].Rows)
                            {
                                ResurveyHouseViewModel ObjRespondentModel = new ResurveyHouseViewModel();
                                ObjRespondentModel.RespondantFirstNameEng = drR["RES_FIRST_NAME_ENG"].ConvertToString();
                                // ObjRespondentModel.RespondantFirstNameLoc = drR["RES_FIRST_NAME_LOC"].ConvertToString();
                                ObjRespondentModel.RespondantMiddleNameEng = drR["RES_MIDDLE_NAME_ENG"].ConvertToString();
                                // ObjRespondentModel.RespondantMiddleNameLoc = drR["RES_MIDDLE_NAME_LOC"].ConvertToString();
                                ObjRespondentModel.RespondantLastNameEng = drR["RES_LAST_NAME_ENG"].ConvertToString();
                                //  ObjRespondentModel.RespondantLastNameLoc = drR["RES_LAST_NAME_LOC"].ConvertToString();

                                ObjRespondentModel.RespondantGenderEng = drR["RES_GENDER_ENG"].ConvertToString();
                                ObjRespondentModel.RespondantGenderLoc = drR["RES_GENDER_LOC"].ConvertToString();
                                ObjRespondentModel.ResRelationToOwnerEng = drR["RES_RELATION_ENG"].ConvertToString();
                                ObjRespondentModel.ResRelationToOwnerLoc = drR["RES_RELATION_LOC"].ConvertToString();
                                ObjRespondentList.Add(ObjRespondentModel);
                            }
                            objModel.RespondentListMain = ObjRespondentList;
                        }


                        if (ds.Tables["Table5"] != null && ds.Tables["Table5"].Rows.Count > 0)
                        {
                            List<ResurveyHouseBuildingDetail> ObjBuildingDtlList = new List<ResurveyHouseBuildingDetail>();

                            foreach (DataRow drB in ds.Tables["Table5"].Rows)
                            {
                                ResurveyHouseBuildingDetail ObjBuildingDtlModel = new ResurveyHouseBuildingDetail();
                                ObjBuildingDtlModel.BuildCondiAftrQuakeEng = drB["BUILDING_CONDITION_ENG"].ConvertToString();
                                ObjBuildingDtlModel.BuildCondiAftrQuakeLoc = drB["BUILDING_CONDITION_LOC"].ConvertToString();
                                ObjBuildingDtlModel.StoreyBeforeEarthQuake = drB["STOREY_BEFORE_EARTHQUAKE"].ConvertToString();
                                ObjBuildingDtlModel.StoryAfterEarthQuake = drB["STOREY_AFTER_EARTHQUAKE"].ConvertToString();

                                ObjBuildingDtlModel.DamageGradeEng = drB["DAMAGE_GRADE_ENG"].ConvertToString();
                                ObjBuildingDtlModel.DamageGradeLoc = drB["DAMAGE_GRADE_LOC"].ConvertToString();
                                ObjBuildingDtlModel.TechnicalSolutionEng = drB["TECH_SOL_ENG"].ConvertToString();
                                ObjBuildingDtlModel.TechnicalSolutionLoc = drB["TECH_SOL_LOC"].ConvertToString();
                                if (drB["HAS_GEO_TECH_RISK"].ConvertToString() == "Y")
                                {
                                    ObjBuildingDtlModel.IsGeoTechnicalRisk = "Yes";
                                }
                                else
                                {
                                    ObjBuildingDtlModel.IsGeoTechnicalRisk = "No";
                                }
                                if (drB["RECONSTRUCTION_STARTED"].ConvertToString() == "Y")
                                {
                                    ObjBuildingDtlModel.IsReconstructionStarted = "Yes";
                                }
                                else
                                {
                                    ObjBuildingDtlModel.IsReconstructionStarted = "No";
                                }
                                ObjBuildingDtlModel.Longitude = drB["LONGITUDE"].ConvertToString();
                                ObjBuildingDtlModel.Latitude = drB["LATITUDE"].ConvertToString();
                                ObjBuildingDtlModel.Altitude = drB["ALTITUDE"].ConvertToString();

                                ObjBuildingDtlModel.BuildingStructureNumber = drB["BUILDING_STRUCTURE_NO"].ConvertToString();


                                if (ds.Tables["Table6"] != null && ds.Tables["Table6"].Rows.Count > 0)
                                {
                                    foreach (DataRow drP in ds.Tables["Table6"].Rows)
                                    {
                                        if (drP["BUILDING_STRUCTURE_NO"].ConvertToString() == drB["BUILDING_STRUCTURE_NO"].ConvertToString())
                                        {
                                            string docTypeid = drP["DOC_TYPE_CD"].ConvertToString();
                                            string photoPath = drP["PHOTO_PATH"].ConvertToString();
                                            if (photoPath != "")
                                            {
                                                string Ppath = ftpPath + photoPath;
                                                bool isError = false;
                                                try
                                                {

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

                                            if (docTypeid == "12")
                                            {
                                                ObjBuildingDtlModel.PhotosofFrontdirections = photoPath;

                                            }
                                            //13 back
                                            else if (docTypeid == "13")
                                            {
                                                ObjBuildingDtlModel.PhotosofBackdirections = photoPath;

                                            }
                                            //14 left
                                            else if (docTypeid == "14")
                                            {
                                                ObjBuildingDtlModel.PhotosofLeftdirections = photoPath;

                                            }
                                            //15 right
                                            else if (docTypeid == "15")
                                            {
                                                ObjBuildingDtlModel.PhotosofRightdirections = photoPath;

                                            }
                                            //16 internal 
                                            else if (docTypeid == "16")
                                            {
                                                ObjBuildingDtlModel.Photosofinternaldamageofhouse = photoPath;

                                            }
                                            //17 Rebel
                                            else if (docTypeid == "17")
                                            {
                                                ObjBuildingDtlModel.Fullydamagedhouseslocationsphoto = photoPath;
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


                                    }

                                }

                                if (ds.Tables["Table"] != null && ds.Tables["Table"].Rows.Count > 0)
                                {
                                    List<ResurveyHouseBuildingDetail> ObjBuildingDamageList = new List<ResurveyHouseBuildingDetail>();
                                    ResurveyHouseBuildingDetail ObjBuildingDamageModel = new ResurveyHouseBuildingDetail();
                                    List<string> lstDamageChkBoxes = new List<string>();
                                    string damageLevelStr = String.Empty;
                                    foreach (DataRow drD in ds.Tables["Table"].Rows)
                                    {
                                        if (drD["BUILDING_STRUCTURE_NO"].ConvertToString() == drB["BUILDING_STRUCTURE_NO"].ConvertToString())
                                        {
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "1" ? "SE" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "2" ? "MH" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "3" ? "IN" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "10" ? "NO" : damageLevelStr;
                                            damageLevelStr = drD["DAMAGE_LEVEL_CD"].ConvertToString() == "11" ? "NA" : damageLevelStr;
                                            if (damageLevelStr != "NA" && damageLevelStr != "NO")
                                            {
                                                lstDamageChkBoxes.Add("chkDamageDet_" + drD["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr + "_" + drD["DAMAGE_LEVEL_DTL_CD"].ConvertToString());
                                            }
                                            else
                                            {
                                                lstDamageChkBoxes.Add("chkDamageDet_" + drD["DAMAGE_CD"].ConvertToString() + "_" + damageLevelStr);
                                            }
                                        }
                                        ViewData["lstDamageCheckBox" + ObjBuildingDtlModel.BuildingStructureNumber] = lstDamageChkBoxes;
                                    }

                                }
                                objModel.HOUSE_BUILDING_DTL_List.Add(ObjBuildingDtlModel);
                            }




                        }

                        if (ds.Tables["Table7"] != null && ds.Tables["Table7"].Rows.Count > 0)
                        {
                            List<ResurveyOtherHousesDamagedModel> ObjOtherHouseDamageModelList = new List<ResurveyOtherHousesDamagedModel>();

                            foreach (DataRow drOth in ds.Tables["Table7"].Rows)
                            {
                                ResurveyOtherHousesDamagedModel objOtherHouse = new ResurveyOtherHousesDamagedModel();

                                objOtherHouse.FullNameEng = drOth["HH_OWNER_FIRST_NAME"].ConvertToString() + " " + drOth["HH_OWNER_MIDDLE_NAME"].ConvertToString() + " " + drOth["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOtherHouse.FullNameLoc = drOth["HH_OWNER_FIRST_NAME"].ConvertToString() + " " + drOth["HH_OWNER_MIDDLE_NAME"].ConvertToString() + " " + drOth["HH_OWNER_LAST_NAME"].ConvertToString();
                                objOtherHouse.DistrictEng = drOth["OWNER_DIST_ENG"].ConvertToString();
                                objOtherHouse.DistrictLoc = drOth["OWNER_DIST_LOC"].ConvertToString();
                                objOtherHouse.VdcMunEng = drOth["NEW_VDC_ENG"].ConvertToString();
                                objOtherHouse.VdcMunLoc = drOth["NEW_VDC_LOC"].ConvertToString();
                                objOtherHouse.OtherWardNo = drOth["NEW_WARD"].ConvertToString();
                                objOtherHouse.BuildingConditionEng = drOth["BUILDING_CONDTN_ENG"].ConvertToString();
                                objOtherHouse.BuildingConditionLoc = drOth["BUILDING_CONDTN_LOC"].ConvertToString();
                                ObjOtherHouseDamageModelList.Add(objOtherHouse);
                            }
                            objModel.OtherHousesDamagedList = ObjOtherHouseDamageModelList;
                        }

                        if (ds.Tables["Table8"] != null && ds.Tables["Table8"].Rows.Count > 0)
                        {
                            List<ResurveyHouseBuildingDetail> ObjGeoTechnicalRiskList = new List<ResurveyHouseBuildingDetail>();
                            foreach (DataRow drOth in ds.Tables["Table8"].Rows)
                            {
                                ResurveyHouseBuildingDetail ObjGroTechRisk = new ResurveyHouseBuildingDetail();

                                ObjGroTechRisk.GeoRiskEng = drOth["GEO_RISK_ENG"].ConvertToString();
                                ObjGroTechRisk.GeoRiskLoc = drOth["GEO_RISK_LOC"].ConvertToString();


                                ObjGeoTechnicalRiskList.Add(ObjGroTechRisk);
                            }
                            objModel.GeoRiskListmain = ObjGeoTechnicalRiskList;
                        }

                        DataTable dtGrievMemberDetail = objService.GrievMemDetail(houseOwnerId);
                        if (dtGrievMemberDetail != null && dtGrievMemberDetail.Rows.Count > 0)
                        {
                            int less = 1;
                            List<GrvDtlToHouseViewModelclass> ownerList = new List<GrvDtlToHouseViewModelclass>();
                            for (int i = 0; i < dtGrievMemberDetail.Rows.Count; i++)
                            {
                                GrvDtlToHouseViewModelclass MemDtl = new GrvDtlToHouseViewModelclass();


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



                                ownerList.Add(MemDtl);
                                less++;

                            }
                            objModel.grievDtlList = ownerList;
                        }
                          }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


            return View("_ResurveyVerificationView", objModel);
        }

        public JsonResult RedericttoSurvey(string GID, string SlipNo)
        {
            ResurveyViewService objService = new ResurveyViewService();
            DataTable house_owner_id = objService.GetOldHouseOwnerId(GID, SlipNo);
           
            if (house_owner_id != null && house_owner_id.Rows.Count > 0 && house_owner_id.Rows[0][0].ConvertToString() != "")
            {
                string param = QueryStringEncrypt.EncryptString("/?id2=" + house_owner_id.Rows[0][0] + "&noise=" + new Random().Next(5000, 10000));
                return Json(param);
            }
            else
            {
                return Json("false");
            }
        }

        public ActionResult EngVerificationReport()
        {
            ResurveyHouseViewModel ObjModel = new ResurveyHouseViewModel();
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            return View(ObjModel);
        }
        [HttpPost]
        public ActionResult EngVerificationReport(ResurveyHouseViewModel ObjModel)
        {
            ResurveyViewService objService = new ResurveyViewService();
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(ObjModel.CurrentVdcMunDefCD))
            {
                 ObjModel.CurrentVdcMunCD = common.GetCodeFromDataBase(ObjModel.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
            }

            dt = objService.GetEngVerificationRpt(ObjModel);
            ViewData["result"] = dt;
            return PartialView("_EngVerificationList");
        }

        public DataTable GetDataforExport(ResurveyHouseViewModel ObjModel)
        {
            ResurveyViewService objService = new ResurveyViewService();
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(ObjModel.CurrentVdcMunDefCD))
            {
                ObjModel.CurrentVdcMunCD = common.GetCodeFromDataBase(ObjModel.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
            }

            dt = objService.GetEngVerificationRpt(ObjModel);
           return dt;
        }

        public ActionResult ExportExcel(string dis,string gp_np,string ward,string gid,string ho_name,string slip_no,string ho_id,string type)
        {
            ResurveyHouseViewModel ObjModel = new ResurveyHouseViewModel();
            ObjModel.OwnDistrictCd = dis;
            ObjModel.CurrentVdcMunDefCD = gp_np;
            ObjModel.CurrentWard = ward;
            ObjModel.GID = gid;
            ObjModel.OwnerFullNameEng = ho_name;
            ObjModel.InstanceUniqueSno = slip_no;
            ObjModel.HouseOwnerId = ho_id;
            ObjModel.GID_Status = type;

            DataTable dt = GetDataforExport(ObjModel);
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
                Response.AddHeader("content-disposition", "attachment;filename= EngVerificationReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("EngVerificationReport", "ResurveyView");
        }
    
        private List<SelectListItem> MajorDamage()
        {
            List<SelectListItem> mjr_dmg = new List<SelectListItem>();
            mjr_dmg.Add(new SelectListItem { Text = "Collapse or Partial Collapse", Value = "1" });
            mjr_dmg.Add(new SelectListItem { Text = "Building or Storey Leaning", Value = "2" });
            mjr_dmg.Add(new SelectListItem { Text = "Adjecent Building Hazard", Value = "3" });
            mjr_dmg.Add(new SelectListItem { Text = "Foundation", Value = "4" });
            mjr_dmg.Add(new SelectListItem { Text = "Roof/Floor Collapse/ Partial Collapse", Value = "" });
            mjr_dmg.Add(new SelectListItem { Text = "Corner Seperation", Value = "6" });
            mjr_dmg.Add(new SelectListItem { Text = "Diagonal Cracking", Value = "7" });
            mjr_dmg.Add(new SelectListItem { Text = "In-Plane-Failure of Walls", Value = "8" });
            mjr_dmg.Add(new SelectListItem { Text = "Out-of-Plane-Failure of Walls carrying floor/roof", Value = "9" });
            mjr_dmg.Add(new SelectListItem { Text = "Out-of-Plane-Failure of Walls not carrying floor/roof	", Value = "10" });
            mjr_dmg.Add(new SelectListItem { Text = "Gable wall Damage/Collapse", Value = "11" });
            mjr_dmg.Add(new SelectListItem { Text = "Declamination", Value = "12" });
            mjr_dmg.Add(new SelectListItem { Text = "Column Failure", Value = "13" });
            mjr_dmg.Add(new SelectListItem { Text = "Beam Failure", Value = "14" });
            mjr_dmg.Add(new SelectListItem { Text = "Infill/Partition walls damage/toppling", Value = "15" });
            mjr_dmg.Add(new SelectListItem { Text = "Roof/Floor Collapse/ Partital Collapse", Value = "16" });
            mjr_dmg.Add(new SelectListItem { Text = "Staircase", Value = "17" });
            mjr_dmg.Add(new SelectListItem { Text = "Parapets", Value = "18" });
            mjr_dmg.Add(new SelectListItem { Text = "Cladding/Glazing	", Value = "19" });
            mjr_dmg.Add(new SelectListItem { Text = "Other", Value = "20" });

            return mjr_dmg;
        }
    
    }
}
