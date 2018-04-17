using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MIS.Services.NHRP.Edit;
using MIS.Models.NHRP.View;
using System.Web.Routing;
using MIS.Services.Core;
using MIS.Services.NHRP.View;
using MIS.Models.NHRP;
using System.Data.OracleClient;
using ExceptionHandler;

using MIS.Services.CaseGrievance;
using MIS.Services.Registration.Household;

using MIS.Models.CaseGrievance;
using MIS.Models.Core;

namespace MIS.Controllers.CaseGrievence
{
    public class CaseGrrievanceHandlingController : BaseController
    {
        //
        // GET: /CaseGrrievanceHandling/
        CommonFunction common = null;
        CaseGrievanceHandled objGrievanceHandeledMod = new CaseGrievanceHandled();
        public ActionResult GrievanceHandling(string p)
        {
            DataTable dtHouseDetail = new DataTable();
            dtHouseDetail = null;
            DataTable dtHouseOwner = new DataTable();
            dtHouseOwner = null;

            CommonFunction common = new CommonFunction();
            NHRSInterviewHouseDetailService objNHRSHouseDetail = new NHRSInterviewHouseDetailService();

            CaseGrievanceHandlingService objGrievHandlingServ = new CaseGrievanceHandlingService();
            CaseGrievanceHandlingDropdownService househead = new CaseGrievanceHandlingDropdownService();

            VW_HOUSE_OWNER_DTL ownerDtlModel = new VW_HOUSE_OWNER_DTL();
            VW_HOUSE_BUILDING_DTL builidingDtlModel = new VW_HOUSE_BUILDING_DTL();
            string id = string.Empty;
            string id2 = string.Empty;
            string id3 = string.Empty;
            string id4 = string.Empty;
            string id5 = string.Empty;
            string id6 = string.Empty;
            try
            {
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        id = (rvd["id"]).ConvertToString();
                    }
                    if (rvd != null)
                    {
                        id2 = (rvd["id2"]).ConvertToString();
                    }
                    if (rvd != null)
                    {
                        id3 = (rvd["id3"]).ConvertToString();
                    }
                    if (rvd != null)
                    {
                        id4 = (rvd["id4"]).ConvertToString();
                    }
                    if (rvd != null)
                    {
                        id5 = (rvd["id5"]).ConvertToString();
                    }
                    if (rvd != null)
                    {
                        id6 = (rvd["id6"]).ConvertToString();
                    }
                }

                //DataTable checkNissa = new DataTable();

                //checkNissa = objGrievHandlingServ.CheckNissaNo(id2, id3, id4, id5);
                //if (checkNissa == null || checkNissa.Rows.Count <= 0)
                //{
                //    ViewData["result"] = Session["result"];
                //    TempData["ErrorMessage"] = "<script>alert('Nissa No Not found');</script>";
                //    return RedirectToAction("ListCaseGrievance", "CaseGrievance");
                //    //return PartialView("~/Views/CaseGrievance/_CaseGrievienceList.cshtml");
                //}
                DataTable dtCheck = objGrievHandlingServ.CheckHouseHandled(id, id6);
                if (dtCheck.Rows.Count > 0 && dtCheck != null)
                {
                    TempData["ErrorMessage"] = "This house has been already Verified";
                }
                DataTable resultDamageDetail = new DataTable();
                resultDamageDetail = objNHRSHouseDetail.GetAllDamageDetail();
                ViewData["resultDamageDetail"] = resultDamageDetail;
                if (id != "" && id != null)
                {

                    HouseBuildingDetailViewService objHouseBuildingService = new HouseBuildingDetailViewService();
                    DataTable dtMemberDetail = objHouseBuildingService.GetHouseMemberDetail(id);
                    if (dtMemberDetail != null && dtMemberDetail.Rows.Count > 0)
                    {
                        int count = 1;
                        int less = 1;
                        List<GrievanceMemberDetail> MemberList = new List<GrievanceMemberDetail>();
                        for (int i = 0; i < dtMemberDetail.Rows.Count; i++)
                        {
                            GrievanceMemberDetail MemberName = new GrievanceMemberDetail();
                            MemberName.HOUSE_SNO = dtMemberDetail.Rows[i]["HOUSE_SNO"].ConvertToString();
                            MemberName.HOUSE_OWNER_ID = dtMemberDetail.Rows[i]["HOUSE_OWNER_ID"].ConvertToString();
                            MemberName.BUILDING_STRUCTURE_NO = dtMemberDetail.Rows[i]["BUILDING_STRUCTURE_NO"].ConvertToString();
                            MemberName.FULL_NAME_ENG = dtMemberDetail.Rows[i]["FULL_NAME_ENG"].ConvertToString();
                            MemberName.GENDER_ENG = dtMemberDetail.Rows[i]["GENDER_ENG"].ConvertToString();
                            MemberName.GENDER_LOC = dtMemberDetail.Rows[i]["GENDER_LOC"].ConvertToString();
                            MemberName.AGE = dtMemberDetail.Rows[i]["AGE"].ConvertToString();
                            MemberName.EDUCATION_ENG = dtMemberDetail.Rows[i]["EDUCATION_ENG"].ConvertToString();
                            MemberName.EDUCATION_LOC = dtMemberDetail.Rows[i]["EDUCATION_LOC"].ConvertToString();
                            MemberName.RELATION_ENG = dtMemberDetail.Rows[i]["RELATION_ENG"].ConvertToString();
                            MemberName.RELATION_LOC = dtMemberDetail.Rows[i]["RELATION_LOC"].ConvertToString();
                            MemberName.CITIZENSHIP_NO = dtMemberDetail.Rows[i]["CITIZENSHIP_NO"].ConvertToString();
                            MemberName.HOUSEHOLD_ID = dtMemberDetail.Rows[i]["HOUSEHOLD_ID"].ConvertToString();
                            MemberName.INSTANCE_UNIQUE_SNO = dtMemberDetail.Rows[i]["INSTANCE_UNIQUE_SNO"].ConvertToString();
                            MemberName.MOBILE_NUMBER = dtMemberDetail.Rows[i]["MOBILE_NUMBER"].ConvertToString();

                            MemberName.Family = Convert.ToInt32(dtMemberDetail.Rows[i]["MEMBER_CNT"]);
                            if (less > MemberName.Family)
                            {
                                less = 0;
                            }
                            MemberName.Family = MemberName.Family - less;
                            if (less == 1)
                            {

                                MemberName.FamilyType = "F" + count;
                            }
                            if (MemberName.Family == 0)
                            {
                                MemberName.FamilyType = "F" + count;
                                count = count + 1;
                                less = 0;
                            }
                            else
                            {
                                if (i != 0 && i < dtMemberDetail.Rows.Count - 1)
                                {
                                    if (MemberName.BUILDING_STRUCTURE_NO != dtMemberDetail.Rows[i - 1]["BUILDING_STRUCTURE_NO"].ConvertToString())
                                    {
                                        count = 1;
                                        MemberName.FamilyType = "F" + count;
                                    }
                                    else
                                    {
                                        MemberName.FamilyType = "F" + count;
                                    }
                                }

                            }
                            MemberList.Add(MemberName);
                            less++;
                        }

                        ownerDtlModel.GrievanceMemberList = MemberList;
                    }
                    List<GrievantDetail> GrievantDetail = new List<GrievantDetail>();
                    DataTable dtGrievant = objHouseBuildingService.GetGrievantDetail(id6);
                    if (dtGrievant != null && dtGrievant.Rows.Count > 0)
                    {
                        GrievantDetail GrievantName = new GrievantDetail();
                        GrievantName.CASE_REGISTRATION_ID = dtGrievant.Rows[0]["CASE_REGISTRATION_ID"].ConvertToString();
                        GrievantName.CASE_GRIEVANCE_ENG = dtGrievant.Rows[0]["CASE_GRIEVANCE_ENG"].ConvertToString();
                        GrievantName.CASE_GRIEVANCE_LOC = dtGrievant.Rows[0]["CASE_GRIEVANCE_LOC"].ConvertToString();

                        if (dtGrievant.Rows[0]["GRIEVANT_FULL_NAME"].ConvertToString() != "")
                        {
                            GrievantName.GGRIEVANT_NAME = dtGrievant.Rows[0]["GRIEVANT_FULL_NAME"].ConvertToString();
                        }
                        else
                        {
                            GrievantName.GGRIEVANT_NAME = dtGrievant.Rows[0]["GRIEVANT_NAME"].ConvertToString();
                        }
                    

                        GrievantName.GRIEVANT_DISTRICT_ENG = dtGrievant.Rows[0]["DISTRICT_ENG"].ConvertToString();
                        GrievantName.GRIEVANT_VDC_ENG = dtGrievant.Rows[0]["VDC_ENG"].ConvertToString();
                        GrievantName.GRIEVANT_DISTRICT_LOC = dtGrievant.Rows[0]["DISTRICT_LOC"].ConvertToString();
                        GrievantName.GRIEVANT_VDC_LOC = dtGrievant.Rows[0]["VDC_LOC"].ConvertToString();
                        GrievantName.GRIEVANT_WARD = dtGrievant.Rows[0]["REGISTRATION_WARD_NO"].ConvertToString();
                        GrievantName.GRIEVANT_NISSA_NO = dtGrievant.Rows[0]["NISSA_NO"].ConvertToString();
                        GrievantName.CONTACT_PHONE_NO = dtGrievant.Rows[0]["CONTACT_PHONE_NO"].ConvertToString();
                        GrievantName.GRIEVANT_FATHER_NAME = dtGrievant.Rows[0]["GRIEVANT_FATHER_NAME"].ConvertToString();
                        GrievantDetail.Add(GrievantName);
                        ownerDtlModel.GrievantDetail = GrievantDetail;
                    }

                    //  DataSet ds = objHouseBuildingService.HouseFamilyDetailAll("1-4-5-81132519877", "19", "1");
                    dtHouseOwner = objHouseBuildingService.HouseOwnerDetail(id);
                    if (dtHouseOwner != null && dtHouseOwner.Rows.Count > 0)
                    {
                        ownerDtlModel.HOUSE_OWNER_ID = dtHouseOwner.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                        ownerDtlModel.DEFINED_CD = dtHouseOwner.Rows[0]["DEFINED_CD"].ConvertToString();
                        ownerDtlModel.ENUMERATOR_ID = dtHouseOwner.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                        ownerDtlModel.INTERVIEW_DT = dtHouseOwner.Rows[0]["INTERVIEW_DT"].ToDateTime();
                        ownerDtlModel.INTERVIEW_DT_LOC = dtHouseOwner.Rows[0]["INTERVIEW_DT_LOC"].ConvertToString();
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
                        ownerDtlModel.TARGET_LOT = dtHouseOwner.Rows[0]["TARGET_LOT"].ConvertToString();

                        FTPclient fc = new FTPclient("10.27.27.104", "mofald", "M0faLd#NepaL");
                        //FTPclient fc = new FTPclient("202.79.33.141", "mofald", "M0faLd#NepaL");

                        string[] subDate = ownerDtlModel.SubmissionDate.Split(' ');
                        string ftpPath = string.Empty;
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
                        DataTable dtHouseOwnerNames = objGrievHandlingServ.HouseOwnerDetail(id);
                        if (dtHouseOwnerNames != null && dtHouseOwnerNames.Rows.Count > 0)
                        {
                            List<GrievanceHandling> ownerList = new List<GrievanceHandling>();
                            foreach (DataRow dr in dtHouseOwnerNames.Rows)
                            {
                                GrievanceHandling ownerName = new GrievanceHandling();
                                ownerName.FIRST_NAME_ENG = dr["FIRST_NAME_ENG"].ConvertToString();
                                ownerName.MIDDLE_NAME_ENG = dr["MIDDLE_NAME_ENG"].ConvertToString();
                                ownerName.LAST_NAME_ENG = dr["LAST_NAME_ENG"].ConvertToString();
                                ownerName.FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                                ownerName.FIRST_NAME_LOC = dr["FIRST_NAME_LOC"].ConvertToString();
                                ownerName.MIDDLE_NAME_LOC = dr["MIDDLE_NAME_LOC"].ConvertToString();
                                ownerName.LAST_NAME_LOC = dr["LAST_NAME_LOC"].ConvertToString();
                                ownerName.FULL_NAME_LOC = dr["FULL_NAME_LOC"].ConvertToString();
                                ownerName.GENDER_ENG = dr["GENDER_ENG"].ConvertToString();
                                ownerName.DISTRICT_ENG = dr["DISTRICT_ENG"].ConvertToString();
                                ownerName.DISTRICT_LOC = dr["DISTRICT_LOC"].ConvertToString();
                                ownerName.VDC_LOC = dr["VDC_LOC"].ConvertToString();
                                ownerName.VDC_ENG = dr["VDC_ENG"].ConvertToString();
                                ownerName.WARD_NO = dr["WARD_NO"].ConvertToString();
                                ownerName.AREA_LOC = dr["AREA_LOC"].ConvertToString();
                                ownerName.AREA_ENG = dr["AREA_ENG"].ConvertToString();
                                ownerName.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                                ownerName.INSTANCE_UNIQUE_SNO = id2.ConvertToString();
                                ownerName.SN = dr["HOUSE_SNO"].ConvertToString();
                                ownerName.nraDefCd = dr["NRA_DEFINED_CD"].ConvertToString();
                                ownerName.structureCount = ownerDtlModel.HOUSE_BUILDING_DTL_List.Count();
                                ownerName.House_Owner_SNO = dr["SNO"].ToInt32();
                                ownerList.Add(ownerName);
                            }
                            ownerDtlModel.HouseOwnerDetailList = ownerList;
                        }
                        //other house detail
                        DataTable othHouseDetail = objGrievHandlingServ.getOthHouseDetail(id6);
                        if (othHouseDetail != null && othHouseDetail.Rows.Count > 0)
                        {
                            List<HouseInOtherPlace> othHouseList = new List<HouseInOtherPlace>();
                            foreach (DataRow dr in othHouseDetail.Rows)
                            {
                                HouseInOtherPlace OthHouse = new HouseInOtherPlace();
                                OthHouse.structure = dr["HOUSE_OWNER_LAST_NAME_ENG"].ConvertToString();
                                OthHouse.FIRST_NAME_ENG = dr["HOUSE_OWNER_FIRST_NAME_ENG"].ConvertToString();
                                OthHouse.MIDDLE_NAME_ENG = dr["HOUSE_OWNER_MIDDLE_NAME_ENG"].ConvertToString();
                                OthHouse.LAST_NAME_ENG = dr["HOUSE_OWNER_LAST_NAME_ENG"].ConvertToString();
                                OthHouse.DISTRICT_ENG = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                                OthHouse.VDC_ENG = Utils.ToggleLanguage(dr["DESC_ENG1"].ConvertToString(), dr["DESC_LOC1"].ConvertToString());
                                OthHouse.ward = dr["HOUSE_WARD_NO"].ConvertToString();
                                OthHouse.HOUSE_CONDTION_CD = dr["HOUSE_CONDITION_CD"].ConvertToString();
                                OthHouse.buildingCondition = Utils.ToggleLanguage(dr["DESC_ENG2"].ConvertToString(), dr["DESC_LOC2"].ConvertToString());
                                othHouseList.Add(OthHouse);
                            }
                            ownerDtlModel.OtherHouseListDetail = othHouseList;
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
                                ownerDtlModel.CASE_REGISTRATION_ID = id6;

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
                    //DataTable dt = objGrievHandlingServ.GetGrievanceHandledData(id);

                    DataTable DamageLeveldata = new DataTable();
                    DamageLeveldata = objHouseBuildingService.getDamageLavelDetail();
                    ViewData["dtDamageLevel"] = DamageLeveldata;
                    DataTable dt = objGrievHandlingServ.GetGrievanceHandledData(id);
                    DataTable dtOwner = objGrievHandlingServ.GetGrievanceOwnerName(id6);
                    //if (dt.Rows.Count > 0 && dt != null)
                    //{
                    //    List<CaseGrievanceHandled> GrievantName = new List<CaseGrievanceHandled>();
                    //    CaseGrievanceHandled objGrievanceHandled = new CaseGrievanceHandled();
                    //    objGrievanceHandled.HouseOwnerName = dtOwner.Rows[0]["HOUSE_OWNER_NAME_ENG"].ConvertToString();
                    //    GrievantName.Add(objGrievanceHandled);
                    //    ownerDtlModel.GrievanceHandled = GrievantName;

                    //}
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        int CaseHandled = 0;
                        List<CaseGrievanceHandled> Handled = new List<CaseGrievanceHandled>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            CaseGrievanceHandled GrievanceHandled = new CaseGrievanceHandled();
                            GrievanceHandled.SurveyDamageGradeCd = dr["SURVEYED_DAMAGE_GRADE_CD"].ConvertToString();
                            GrievanceHandled.SurveyTechnicalSolution = dr["SURVEYED_TECHNICAL_SOLUTION_CD"].ConvertToString();
                            GrievanceHandled.PHOTO_GRADE_CD = dr["PHOTO_GRADE_CD"].ConvertToString();
                            GrievanceHandled.OfficersTechnicalSolution = dr["OFFICER_TECHNICAL_SOLUTION_CD"].ConvertToString();
                            GrievanceHandled.OfficerGradeCd = dr["OFFICER_GRADE_CD"].ConvertToString();
                            GrievanceHandled.OfficerRecommendation = dr["OFFICER_RECOMMENDATION"].ConvertToString();
                            GrievanceHandled.MATRIX_GRADE_CD = dr["MATRIX_GRADE_CD"].ConvertToString();
                            GrievanceHandled.status = dr["STATUS"].ConvertToString();
                            GrievanceHandled.REMARKS = dr["REMARKS"].ConvertToString();
                            if (dtOwner.Rows.Count > 0 && dt != null)
                            {
                                GrievanceHandled.HouseOwnerName = dtOwner.Rows[0]["HOUSE_OWNER_NAME_ENG"].ConvertToString();
                            }
                            ViewData[CaseHandled == 0 ? "ddl_damageGrade" : "ddl_damageGrade" + CaseHandled] = common.GetDamageGrade(GrievanceHandled.MATRIX_GRADE_CD);
                            ViewData[CaseHandled == 0 ? "ddl_photoGrade" : "ddl_photoGrade" + CaseHandled] = househead.GetPhotoGrade(GrievanceHandled.PHOTO_GRADE_CD);
                            ViewData[CaseHandled == 0 ? "ddl_aftGrievGrade" : "ddl_aftGrievGrade" + CaseHandled] = househead.GetaftGrievGrade(GrievanceHandled.OfficerGradeCd);
                            ViewData[CaseHandled == 0 ? "ddl_TechnicalRemedy" : "ddl_TechnicalRemedy" + CaseHandled] = househead.GetTechnicalSolution(GrievanceHandled.OfficersTechnicalSolution);
                            ViewData[CaseHandled == 0 ? "Owner" : "Owner" + CaseHandled] = (List<SelectListItem>)househead.GetYesNo(GrievanceHandled.status).Data;
                            Handled.Add(GrievanceHandled);
                            CaseHandled++;
                        }
                        ownerDtlModel.GrievanceHandled = Handled;
                    }
                    else
                    {
                        for (int i = 0; i < ownerDtlModel.HOUSE_BUILDING_DTL_List.Count(); i++)
                        {
                            if (i == 0)
                            {
                                ViewData["ddl_damageGrade"] = househead.GetDamageGrade("");
                                ViewData["ddl_photoGrade"] = househead.GetPhotoGrade("");
                                ViewData["ddl_aftGrievGrade"] = househead.GetaftGrievGrade("");
                                ViewData["ddl_TechnicalRemedy"] = househead.GetTechnicalSolution("");
                                ViewData["Owner"] = (List<SelectListItem>)househead.GetYesNo("").Data;

                            }
                            else
                            {
                                ViewData["ddl_damageGrade" + i] = househead.GetDamageGrade("");
                                ViewData["ddl_photoGrade" + i] = househead.GetPhotoGrade("");
                                ViewData["ddl_aftGrievGrade" + i] = househead.GetaftGrievGrade("");
                                ViewData["ddl_TechnicalRemedy" + i] = househead.GetTechnicalSolution("");
                                ViewData["Owner" + i] = (List<SelectListItem>)househead.GetYesNo("").Data;
                            }
                        }
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

            return View(ownerDtlModel);
        }
        [HttpPost]
        public ActionResult GrievanceHandling(FormCollection fc)
        {
            HouseBuildingDetailViewService objHouseBuildingService = new HouseBuildingDetailViewService();
            CaseGrievanceHandlingService objGrievHandlingServ = new CaseGrievanceHandlingService();
            int MinRecommendedGrade = DBNull.Value.ToInt32();
            string structurecount = fc["structureCount"];
            int count = fc["structureCount"].Split(',').Count();
            List<CaseGrievanceHandled> ObjGrievncHandledList = new List<CaseGrievanceHandled>();

            string instanceUniqueSno = fc["INSTANCE_UNIQUE_SNO"].ConvertToString();

            string CaseRegistrationID = fc["CASE_REGISTRATION_ID"].ConvertToString();
            objGrievanceHandeledMod.caseRegistrationId = CaseRegistrationID.ConvertToString();
            string[] HouseOwnerId = fc["HOUSE_OWNER_ID"].Split(',');
            string FullName = string.Empty;
            objGrievanceHandeledMod.houseOwnerId = HouseOwnerId[0];


            string HouseOwnerName = fc["HOUSE_OWNER_NAME"].ConvertToString();
            if (HouseOwnerName == null || HouseOwnerName == "")
            {
                string[] HouseOWnerName = fc["HouseOWnerName"].Split(',');
                for (int i = 0; i < HouseOWnerName.Count(); i++)
                {
                    HouseOwnerName = HouseOwnerName + "," + HouseOWnerName[i];
                }
            }
            else
            {
                HouseOwnerName = HouseOwnerName.TrimEnd(',');
            }
            objGrievanceHandeledMod.HouseOwnerName = HouseOwnerName;

            objGrievanceHandeledMod.RecommendationGrade = fc["txtRecommendedGrade"].ConvertToString();
            var RecommendationGrade = fc["txtRecommendedGrade"].ConvertToString();
            string[] RecommendationGradeArray = RecommendationGrade.Split(',');
            if (RecommendationGradeArray.Count() > 1)
            {
                MinRecommendedGrade = RecommendationGradeArray.Min().ToInt32();
            }
            else
            {
                MinRecommendedGrade = RecommendationGrade.ToInt32();
            }

            objGrievanceHandeledMod.RecommendationTS = fc["txtRecommendedTS"].ConvertToString();
            var RecommendationTS = fc["txtRecommendedTS"].ConvertToString();
            var GradeIndex = Array.IndexOf(RecommendationGradeArray, RecommendationGradeArray.Where(x => x.Contains(""+MinRecommendedGrade)).FirstOrDefault());
            string[] RecommendationTSArray = RecommendationTS.Split(',');
            string MinRecommendedTS = RecommendationTS.Split(',')[GradeIndex];
            string BuildingCondtion = objHouseBuildingService.leastDamagedHouseCondtion(objGrievanceHandeledMod.houseOwnerId.ConvertToString());
            string HouseCondtionGrievance = fc["HOUSE_CONDTION_CD"].ConvertToString();
            string NRA_DEFINED_CD = fc["nraDefCd"].ConvertToString();
        
            objGrievanceHandeledMod.caseRegistrationId = CaseRegistrationID.ConvertToString();
            objGrievHandlingServ.DeleteGrievanceHandled(CaseRegistrationID);
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    objGrievanceHandeledMod.Mode = "I";

                    objGrievanceHandeledMod.SurveyDamageGradeCd = fc["DAMAGE_GRADE_ENG"].ConvertToString();
                    objGrievanceHandeledMod.OfficersTechnicalSolution = fc["ddl_TechnicalRemedy"].ConvertToString();
                    objGrievanceHandeledMod.SurveyTechnicalSolution = fc["SurveyTechSolution"].ConvertToString();
                    objGrievanceHandeledMod.MATRIX_GRADE_CD = fc["ddl_damageGrade"].ConvertToString();
                    objGrievanceHandeledMod.PHOTO_GRADE_CD = fc["ddl_photoGrade"].ConvertToString();
                    objGrievanceHandeledMod.gradeAfterGrievance = fc["ddl_aftGrievGrade"].ConvertToString();
                    objGrievanceHandeledMod.FieldObservation = fc["Owner"].ConvertToString();
                    objGrievanceHandeledMod.REMARKS = fc["REMARKS"].ConvertToString();
                    objGrievanceHandeledMod.houseStructureNo = fc["BUILDING_STRUCTURE_NO"].ConvertToString();

                }
                else
                {
                    objGrievanceHandeledMod.Mode = "I";

                    objGrievanceHandeledMod.SurveyDamageGradeCd = fc["DAMAGE_GRADE_ENG" + i].ConvertToString();
                    objGrievanceHandeledMod.OfficersTechnicalSolution = fc["ddl_TechnicalRemedy" + i].ConvertToString();
                    objGrievanceHandeledMod.SurveyTechnicalSolution = fc["SurveyTechSolution" + i].ConvertToString();
                    objGrievanceHandeledMod.MATRIX_GRADE_CD = fc["ddl_damageGrade" + i].ConvertToString();
                    objGrievanceHandeledMod.PHOTO_GRADE_CD = fc["ddl_photoGrade" + i].ConvertToString();
                    objGrievanceHandeledMod.gradeAfterGrievance = fc["ddl_aftGrievGrade" + i].ConvertToString();
                    objGrievanceHandeledMod.FieldObservation = fc["Owner" + i].ConvertToString();
                    objGrievanceHandeledMod.REMARKS = fc["REMARKS" + i].ConvertToString();
                    objGrievanceHandeledMod.houseStructureNo = fc["BUILDING_STRUCTURE_NO" + i].ConvertToString();
                }
                objGrievHandlingServ.svaeGrievance(objGrievanceHandeledMod);
            }
            return RedirectToAction("ListCaseGrievance", "CaseGrievance");
        }

        public JsonResult FillDamagedGrade()
        {
            List<SelectListItem> selLstWard = new List<SelectListItem>();
            string code = "";
            string desc = "";
            CommonService commonService = new CommonService();
            CommonFunction comFun = new CommonFunction();
            ViewData["ddl_DamageGrade"] = comFun.GetDamageGrade(code, desc);
            return new JsonResult { Data = ViewData["ddl_DamageGrade"], JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //public List<SelectListItem> GetDamageGrade(string selectedValue, string code = "", string desc = "")
        //{
        //    List<SelectListItem> selLstTargetId = new List<SelectListItem>();
        //    List<MISCommon> lstCommon = commonService.GetDamageGrade(code, desc);
        //    selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Damage Grade") + "---" });
        //    foreach (MISCommon common in lstCommon)
        //    {

        //        selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
        //    }

        //    foreach (SelectListItem item in selLstTargetId)
        //    {
        //        if (item.Value == selectedValue)
        //            item.Selected = true;
        //    }
        //    return selLstTargetId;
        //}

    }
}
