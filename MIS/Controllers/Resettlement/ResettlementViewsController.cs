using ExceptionHandler;
using MIS.Models.NHRP;
using MIS.Models.NHRP.View;
using MIS.Services.Core;
using MIS.Services.NHRP.Edit;
using MIS.Services.NHRP.View;
using MIS.Services.Resettlement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers
{
    public class ResettlementViewsController : BaseController
    {
        //
        // GET: /ResettlementViews/
        CommonFunction common = new CommonFunction();
        public ActionResult ResettlementHouseView(string p)
        {
            DataTable dtResettlementData = new DataTable();
            DataTable dtHouseDetail = new DataTable();
            dtHouseDetail = null;
            DataTable dtHouseOwner = new DataTable();
            dtHouseOwner = null;
            NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();
            ResettlementViewsService objResettlementServ = new ResettlementViewsService();
            VW_HOUSE_OWNER_DTL ownerDtlModel = new VW_HOUSE_OWNER_DTL();
            VW_HOUSE_BUILDING_DTL builidingDtlModel = new VW_HOUSE_BUILDING_DTL();
            string id = string.Empty;
            string ResettlementId = "";
            try
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        id = (rvd["id2"]).ConvertToString();
                        ResettlementId = (rvd["id"]).ConvertToString();
                    }
                } 
                   dtResettlementData = objResettlementServ.getDataById(ResettlementId);
                ViewData["dtResettlementData"] = dtResettlementData;
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
                        if (ownerDtlModel.TARGET_LOT == "4" || ownerDtlModel.TARGET_LOT == "5" || ownerDtlModel.TARGET_LOT == "6" || ownerDtlModel.TARGET_LOT == "7")
                        {
                            ftpPath = @"/PhotO/" + "/17districts/" + ownerDtlModel.KLLDistrictCode + "/" + subDate[0] + "/" + ownerDtlModel.DEFINED_CD + "/";
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
                                //ownerName.G_OWNER_NAME = dr["GRIEVANT_NAME"].ConvertToString();
                                ownerName.GID = dr["GID"].ConvertToString();
                                ownerList.Add(ownerName);
                            }
                            ownerDtlModel.HouseOwnerNamesList = ownerList;
                        }

                        //grievant details
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
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)common.GetResettlementReview(dtResettlementData.Rows[0]["NEW_REVIEW"].ConvertToString()).Data; 
            return View(ownerDtlModel);
        
        }

        [HttpPost]
        public ActionResult GetSurveyPopup(string District, string VDC, string Ward, string NissaNo, string ResettlementId, string PaNumber)
        {
            CommonFunction commonFC = new CommonFunction();
            //Ward = "1";
            ViewData["ddl_Districtss"] = commonFC.GetDistricts(District.ConvertToString());
            VDC = commonFC.GetDefinedCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            if(VDC!="" && VDC!=null)
            {
                ViewData["ddl_currentVDCMun"] = commonFC.GetVDCMunByAllDistrict(VDC.ConvertToString(), District.ConvertToString());
            }
            else
            {
                ViewData["ddl_currentVDCMun"] = commonFC.GetVDCMunByDistrictCode("",District.ConvertToString());

            }
            //ViewData["ddl_currentVDCMun"] = commonFC.GetVDCMunByAllDistrict(VDC.ConvertToString(), District.ConvertToString());
            ViewData["ddl_currentWard"] = commonFC.GetWardByVDCMun(Ward.ConvertToString(), "");
            ViewData["ResettlementId"] = ResettlementId;
            ViewData["NisaNumber"] = NissaNo;
            ViewData["pa_number"] = PaNumber;
            GetSurveyData(District, VDC, Ward, NissaNo, ResettlementId, PaNumber);
            return PartialView("_ResettlementGetSurveyData");
        }

        [HttpPost]
        public ActionResult GetSurveyData(string District, string VDC, string Ward, string NissaNo, string ResettlementId, string paNumber)
        {
            //Ward = "1";
            CommonFunction commonFC = new CommonFunction();
             ResettlementViewsService objResettlementServ = new ResettlementViewsService();
            DataTable dt = new DataTable();
            if (District.ConvertToString() != "" && District.ConvertToString() != null)
            {
                ViewData["ddl_Districtss"] = commonFC.GetDistricts(District.ConvertToString());
            }
            if (VDC.ConvertToString() != "" && VDC.ConvertToString() != "null")
            {
                ViewData["ddl_currentVDCMun"] = commonFC.GetVDCMun(VDC.ConvertToString());
            }
            if (Ward.ConvertToString() != "" && Ward.ConvertToString() != null)
            {
                ViewData["ddl_currentWard"] = commonFC.GetWardByVDCMun(Ward.ConvertToString(), "");
            }
            if(District=="")
            {
                ViewData["ddl_Districtss"] = commonFC.GetDistricts("");

            }
            if ((VDC == "null" || VDC == "") && District != "")
            {
                ViewData["ddl_currentVDCMun"] = commonFC.GetVDCMunByDistrictCode("", District.ConvertToString());
            }
            if ((VDC == "null" || VDC == "") && District == "")
            {
                ViewData["ddl_currentVDCMun"] = commonFC.GetVDCMunByDistrict("", "");
            }
            if(Ward=="")
            {
                ViewData["ddl_currentWard"] = commonFC.GetWardByVDCMun("", "");
            }
            
            ViewData["pa_number"] = paNumber;
            ViewData["NisaNumber"] = NissaNo;
            var Lan = "";
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                Lan = "E";
            }
            else
            {
                Lan = "N";
            }
            VDC = commonFC.GetCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            dt = objResettlementServ.getSurveyData(District, VDC, Ward, Lan, paNumber, NissaNo);

            dt.Columns.Add("RESETTLEMENT_ID", typeof(System.Int32));
            dt.Columns["RESETTLEMENT_ID"].Expression = ResettlementId;
            ViewData["ResettlementId"] = ResettlementId.ConvertToString();
            ViewData["ResettlementSurveyData"] = dt;
            return PartialView("_ResettlementGetSurveyData");
        }


        [HttpPost]
        public ActionResult SaveCompliedResettlement (FormCollection fc)
        {
            string districtCd = "";
            string VdcMunCd = "";
            string Ward = "";
            string MisReview = "";
            string ResettlementId = "";
            string HouseOwnerId = "";
            string mode = "";

            ResettlementViewsService objResettlementServ = new ResettlementViewsService();
            bool result = false;
            string NewRemarks = fc["NewRemarks"].ConvertToString();
            districtCd = fc["District"].ConvertToString();
            VdcMunCd = fc["Vdc"].ConvertToString();
            Ward = fc["Ward"].ConvertToString();
            ResettlementId = fc["resettlement"].ConvertToString();
            HouseOwnerId = fc["HNID"].ConvertToString();
            MisReview = fc["ddl_MIS_review"].ConvertToString();
            if (fc["btn_Submit"].ConvertToString() == "Submit" || fc["btn_Submit"].ConvertToString() == "पेश गर्नुहोस्")
            {
                mode = "I";
                result = objResettlementServ.SaveResettlementComply(mode, districtCd, VdcMunCd, Ward, ResettlementId, HouseOwnerId, MisReview, NewRemarks);

            }
            else
            {
                mode = "U";
                result = objResettlementServ.SaveResettlementComply(mode, districtCd, VdcMunCd, Ward, ResettlementId, HouseOwnerId, MisReview, NewRemarks);

            }
            if(result==true)
            {
                Session["ResettlSurveySave"] = "Success";
            }
            else
            {
                Session["ResettlSurveySave"] = "Failed";
            }
           
            //return View("/Resettlement/ResettlementList/");
            return RedirectToAction("ResettlementList", "Resettlement");
        }


        [HttpPost]
        public bool CheckIfVerified(string District, string VDC, string Ward, string NissaNo, string ResettlementId)
        {
            //Ward = "1";
            bool result = false;
            CommonFunction commonFC = new CommonFunction();
            ResettlementViewsService objResettlementServ = new ResettlementViewsService();
            
            result = objResettlementServ.checkIfBenfVerified(ResettlementId);
 
            return result;
        }

    }
}
