using EntityFramework;
using ExceptionHandler;
using MIS.Models.NHRP;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;


namespace MIS.Services.NHRP.FileImport
{
    public class ResurveyFileImportService  
    {
        public Boolean SaveDataCSV(string fileName, List<SortedDictionary<dynamic, dynamic>> surveyList, string mode, string btchID, out int surveyCnt, out int houseCnt, out int householdCnt, out int memberCnt, out string exc)
        {
            QueryResult qr = null;
            bool res = false;
            surveyCnt = 0;
            houseCnt = 0;
            householdCnt = 0;
            memberCnt = 0;

            CommonFunction commFunction = new CommonFunction();
            List<string> houseIDListDB = new List<string>();
            exc = "";

            string HouseOwnerIDInserted = String.Empty;
            string HouseholdID = String.Empty;
            string MemberID = String.Empty;
            decimal? batchID = 0;
            List<Dictionary<string, string>> lstmember = new List<Dictionary<string, string>>();

            List<SortedDictionary<dynamic, dynamic>> filteredSurveyList = new List<SortedDictionary<dynamic, dynamic>>();
            ResurveyBatchInfo objBatchInfo = new ResurveyBatchInfo();
            MigResurveyHouseOwnerMstInfo objNHRSHouseOwnerMst = new MigResurveyHouseOwnerMstInfo();
            MigResurveyHouseOwnerDtlInfo objNHRSHouseOwnerDtl = new MigResurveyHouseOwnerDtlInfo();
            MigNhrsRespondentDtlInfo objNHRSRespondentDtl = new MigNhrsRespondentDtlInfo();
            MigResurveyHhOthResidenceDtlInfo objNHRSOtherHouseDtl = new MigResurveyHhOthResidenceDtlInfo();

            RES_NHRS_BUILDING_ASS_MST objNHRSBuildingAssMst = new RES_NHRS_BUILDING_ASS_MST();
            RES_NHRS_BUILDING_ASS_PHOTO objNHRSBuildingAssPhoto = new RES_NHRS_BUILDING_ASS_PHOTO();
            RES_NHRS_BUILDING_ASS_DTL objNHRSBuildingAssDtl = new RES_NHRS_BUILDING_ASS_DTL();
            RES_NHRS_BA_SUPERSTRUCT_MAT objNHRSSuperStructure = new RES_NHRS_BA_SUPERSTRUCT_MAT();
            RES_NHRS_BA_SEC_OCCUPANCY objNHRSSecondaryOccupancy = new RES_NHRS_BA_SEC_OCCUPANCY();
            RES_NHRS_GEOTECHNICAL_RISK objNHRSBaGeotechnicalRisk = new RES_NHRS_GEOTECHNICAL_RISK();
            RES_MIS_HOUSEHOLD_INFO objMisHouseholdInfo = new RES_MIS_HOUSEHOLD_INFO();
            RES_NHRS_HH_WATERSOURCE_DTL objWaterSource = new RES_NHRS_HH_WATERSOURCE_DTL();
            RES_NHRS_HH_FUELSOURCE_DTL objFuelSource = new RES_NHRS_HH_FUELSOURCE_DTL();
            RES_NHRS_HH_LIGHTSOURCE_DTL objLightSource = new RES_NHRS_HH_LIGHTSOURCE_DTL();
            RES_NHRS_HH_TOILET_TYPE objToiletType = new RES_NHRS_HH_TOILET_TYPE();
            RES_NHRS_HH_INDICATOR_DTL objIndicatorDtl = new RES_NHRS_HH_INDICATOR_DTL();
            RES_NHRS_RCVD_EQ_RELIEF_MONEY objMigNhrsRcvdRelief = new RES_NHRS_RCVD_EQ_RELIEF_MONEY();
            RES_MIS_HH_ALLOWANCE_DTL objMisHHAllowanceDtl = new RES_MIS_HH_ALLOWANCE_DTL();
            RES_MIS_MEMBER objMisMemberInfo = new RES_MIS_MEMBER();
            RES_MIS_HH_FAMILY_DTL objMisHHFamilyDtl = new RES_MIS_HH_FAMILY_DTL();
            RES_NHRS_GRIEVANCE_DTL objMISGrievanceDtl = new RES_NHRS_GRIEVANCE_DTL();
            RES_MIS_HH_DEATH_DTL objMisHHDeathDtl = new RES_MIS_HH_DEATH_DTL();
            RES_NHRS_HUMAN_DESTROY_DTL objNHRSHHHumanDestroyDtl = new RES_NHRS_HUMAN_DESTROY_DTL();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    houseIDListDB = new List<string>();
                    houseIDListDB = HouseIDListCSV();
                    filteredSurveyList = surveyList.Where(m => !houseIDListDB.Contains(m["ona_id"])).ToList();
                    if (filteredSurveyList.Count > 0)
                    {
                        surveyCnt = filteredSurveyList.Count;
                        if (string.IsNullOrEmpty(btchID))
                        {
                            #region Batch Info Insert For Start Time
                            objBatchInfo = new ResurveyBatchInfo();
                            objBatchInfo.BatchDate = System.DateTime.Now;
                            objBatchInfo.BatchStartTime = System.DateTime.Now;
                            objBatchInfo.FileName = fileName;
                            objBatchInfo.Mode = mode;
                            objBatchInfo.IsPosted = "N";
                            service.Begin();
                            service.PackageName = "MIGNHRS.PKG_RES_BATCH";
                            //Main Table 
                            qr = service.SubmitChanges(objBatchInfo, true);
                            //SAVE BATCH INFO HERE
                            #endregion
                        }
                        else
                        {
                            batchID = btchID.ToDecimal();
                        }

                        #region Entire CSV Data Import Here

                        //if (qr.IsSuccess)
                        //{
                        if (string.IsNullOrEmpty(btchID))
                        {
                            if (qr.IsSuccess)
                            {
                                batchID = qr["v_BATCH_ID"].ToDecimal();
                            }
                        }
                        //int startWriteSurvey = 0;
                        //int startWriteFamily = 0;
                        //TextWriter tw = File.CreateText("E:/New Repo/branches/MIS/Files/CSV/time_Record.txt");

                        foreach (var survey in filteredSurveyList)
                        {

                            if (survey.ContainsKey("ona_id") && !houseIDListDB.Contains(Utils.ConvertToString(survey.First(s => s.Key == "ona_id").Value)))
                            {
                                #region Owner's Master Data
                                objNHRSHouseOwnerMst = new MigResurveyHouseOwnerMstInfo();
                                objNHRSHouseOwnerMst.DefinedCd = survey.ContainsKey("ona_id") ? Utils.ConvertToString(survey.First(s => s.Key == "ona_id").Value) : null;
                                objNHRSHouseOwnerMst.DistrictCd = survey.ContainsKey("district") ? Utils.ConvertToString(survey.First(s => s.Key == "district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "district").Value)) : null;
                                objNHRSHouseOwnerMst.VdcMunCd = survey.ContainsKey("vdc") ? Utils.ConvertToString(survey.First(s => s.Key == "vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "vdc").Value)) : null;
                                objNHRSHouseOwnerMst.WardNo = survey.ContainsKey("ward") ? Utils.ConvertToString(survey.First(s => s.Key == "ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "ward").Value)) : null;
                                objNHRSHouseOwnerMst.EnumerationArea = survey.ContainsKey("enum_ar") ? Utils.ConvertToString(survey.First(s => s.Key == "enum_ar").Value) : null;
                                objNHRSHouseOwnerMst.EnumeratorId = survey.ContainsKey("enum_id") ? Utils.ConvertToString(survey.First(s => s.Key == "enum_id").Value) : null;
                                objNHRSHouseOwnerMst.AreaEng = survey.ContainsKey("tole") ? Utils.ConvertToString(survey.First(s => s.Key == "tole").Value) : null;
                                objNHRSHouseOwnerMst.HouseFamilyOwnerCnt = survey.ContainsKey("own_num") ? Utils.ConvertToString(survey.First(s => s.Key == "own_num").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "own_num").Value)) : null;
                                objNHRSHouseOwnerMst.ReasonForGrievance = survey.ContainsKey("resn_for_griev") ? Utils.ConvertToString(survey.First(s => s.Key == "resn_for_griev").Value) : null;
                                objNHRSHouseOwnerMst.HasOldSlip = survey.ContainsKey("has_old_idslip") ? Utils.ConvertToString(survey.First(s => s.Key == "has_old_idslip").Value) : null;
                                objNHRSHouseOwnerMst.OldSlipPhoto = survey.ContainsKey("old_idslip_photo") ? Utils.ConvertToString(survey.First(s => s.Key == "old_idslip_photo").Value) : null;
                                objNHRSHouseOwnerMst.ReasonForLeft = survey.ContainsKey("resn_for_left") ? Utils.ConvertToString(survey.First(s => s.Key == "resn_for_left").Value) : null;
                                objNHRSHouseOwnerMst.OtherReason = survey.ContainsKey("resn_oth") ? Utils.ConvertToString(survey.First(s => s.Key == "resn_oth").Value) : null;
                                objNHRSHouseOwnerMst.ReasonForLeft = survey.ContainsKey("resn_for_left") ? Utils.ConvertToString(survey.First(s => s.Key == "resn_for_left").Value) : null;
                                objNHRSHouseOwnerMst.VDCOLD = survey.ContainsKey("vdc_old") ? Utils.ToDecimal(survey.First(s => s.Key == "vdc_old").Value) : null;
                                objNHRSHouseOwnerMst.WardOld = survey.ContainsKey("ward_old") ? Utils.ToDecimal(survey.First(s => s.Key == "ward_old").Value) : null;
                                objNHRSHouseOwnerMst.NhrsUuid = survey.ContainsKey("uuid") ? Utils.ConvertToString(survey.First(s => s.Key == "uuid").Value) : null;
                                objNHRSHouseOwnerMst.InterviewStart = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                objNHRSHouseOwnerMst.InterviewEnd = survey.ContainsKey("en_time") ? Utils.ConvertToString(survey.First(s => s.Key == "en_time").Value) : null;
                                objNHRSHouseOwnerMst.InterviewDt = survey.ContainsKey("today") ? Utils.ConvertToString(survey.First(s => s.Key == "today").Value) : null;
                                objNHRSHouseOwnerMst.Imei = survey.ContainsKey("imei") ? Utils.ConvertToString(survey.First(s => s.Key == "imei").Value) : null;
                                objNHRSHouseOwnerMst.Imsi = survey.ContainsKey("imsi") ? Utils.ConvertToString(survey.First(s => s.Key == "imsi").Value) : null;
                                objNHRSHouseOwnerMst.SimNumber = survey.ContainsKey("sim_tel") ? Utils.ConvertToString(survey.First(s => s.Key == "sim_tel").Value) : null;
                                objNHRSHouseOwnerMst.Sim = survey.ContainsKey("sim") ? Utils.ConvertToString(survey.First(s => s.Key == "sim").Value) : null;
                                objNHRSHouseOwnerMst.SubmissionTime = survey.ContainsKey("sub_time") ? Utils.ConvertToString(survey.First(s => s.Key == "sub_time").Value) : null;
                                objNHRSHouseOwnerMst.SubmissionDate = survey.ContainsKey("sub_date") ? Utils.ConvertToString(survey.First(s => s.Key == "sub_date").Value) : null;
                                objNHRSHouseOwnerMst.Comments = survey.ContainsKey("comment") ? Utils.ConvertToString(survey.First(s => s.Key == "comment").Value) : null;
                                objNHRSHouseOwnerMst.Remarks = survey.ContainsKey("comment") ? Utils.ConvertToString(survey.First(s => s.Key == "comment").Value) : null;
                                objNHRSHouseOwnerMst.RemarksLoc = survey.ContainsKey("comment") ? Utils.ConvertToString(survey.First(s => s.Key == "comment").Value) : null;
                                objNHRSHouseOwnerMst.DistrictName = survey.ContainsKey("district_name") ? Utils.ConvertToString(survey.First(s => s.Key == "district_name").Value) : null;
                                objNHRSHouseOwnerMst.VdcName = survey.ContainsKey("vdc_name") ? Utils.ConvertToString(survey.First(s => s.Key == "vdc_name").Value) : null;
                                objNHRSHouseOwnerMst.OldVdcName = survey.ContainsKey("old_vdc_name") ? Utils.ConvertToString(survey.First(s => s.Key == "old_vdc_name").Value) : null;
                                //objNHRSHouseOwnerMst.IPAddress = CommonVariables.IPAddress;
                                objNHRSHouseOwnerMst.HOUSE_SNO = survey.ContainsKey("house_no") ? Utils.ConvertToString(survey.First(s => s.Key == "house_no").Value) : null;
                                objNHRSHouseOwnerMst.Approved = "Y";
                                objNHRSHouseOwnerMst.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                objNHRSHouseOwnerMst.ApprovedDtLoc = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                objNHRSHouseOwnerMst.ApprovedBy = SessionCheck.getSessionUsername();
                                objNHRSHouseOwnerMst.EnteredBy = SessionCheck.getSessionUsername();
                                objNHRSHouseOwnerMst.EnteredDt = DateTime.Now.ConvertToString();
                                objNHRSHouseOwnerMst.EnteredDtLoc = DateTime.Now.ConvertToString();
                                objNHRSHouseOwnerMst.UpdatedBy = SessionCheck.getSessionUsername();
                                objNHRSHouseOwnerMst.UpdatedDt = DateTime.Now.ConvertToString();
                                objNHRSHouseOwnerMst.UpdatedDtLoc = DateTime.Now.ConvertToString();
                                objNHRSHouseOwnerMst.BatchId = batchID;
                                objNHRSHouseOwnerMst.Mode = mode;
                                service.PackageName = "MIGNHRS.PKG_RES_HOUSEOWNER";
                                //Main Table 
                                qr = service.SubmitChanges(objNHRSHouseOwnerMst, true);

                                #endregion
                                if (qr.IsSuccess)
                                {
                                    HouseOwnerIDInserted = qr["V_HOUSE_OWNER_ID"].ConvertToString();
                                    #region Owners' Detail
                                    foreach (SortedDictionary<dynamic, dynamic> owner in survey.First(s => s.Key == "Owners").Value)
                                    {
                                        objNHRSHouseOwnerDtl = new MigResurveyHouseOwnerDtlInfo();
                                        objNHRSHouseOwnerDtl.FirstNameEng = owner.ContainsKey("name_fst") ? Utils.ConvertToString(owner.First(s => s.Key == "name_fst").Value) : null;
                                        if (objNHRSHouseOwnerDtl.FirstNameEng.ConvertToString() != "")
                                        {
                                            objNHRSHouseOwnerDtl.MiddleNameEng = owner.ContainsKey("name_mid") ? Utils.ConvertToString(owner.First(s => s.Key == "name_mid").Value) : null;
                                            objNHRSHouseOwnerDtl.LastNameEng = owner.ContainsKey("name_lst") ? Utils.ConvertToString(owner.First(s => s.Key == "name_lst").Value) : null;
                                            objNHRSHouseOwnerDtl.FullNameEng = objNHRSHouseOwnerDtl.FirstNameEng.ConvertToString() + (objNHRSHouseOwnerDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objNHRSHouseOwnerDtl.MiddleNameEng) + " ") + objNHRSHouseOwnerDtl.LastNameEng.ConvertToString();
                                            objNHRSHouseOwnerDtl.GenderCd = owner.ContainsKey("gender") ? Utils.ConvertToString(owner.First(s => s.Key == "gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(owner.First(s => s.Key == "gender").Value)) : null;
                                            objNHRSHouseOwnerDtl.HouseOwnerId = HouseOwnerIDInserted;
                                            objNHRSHouseOwnerDtl.OWnerSno = owner.ContainsKey("loi_sn") ? Utils.ConvertToString(owner.First(s => s.Key == "loi_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(owner.First(s => s.Key == "loi_sn").Value)) : null;
                                            objNHRSHouseOwnerDtl.HouseholdinBeneficary = owner.ContainsKey("in_beneficiary") ? (Utils.ConvertToString(owner.First(s => s.Key == "in_beneficiary").Value) == "1" ? "Y" : "N") : "N";
                                            objNHRSHouseOwnerDtl.GrievanceUniqueID = owner.ContainsKey("hh_gid") ? Utils.ConvertToString(owner.First(s => s.Key == "hh_gid").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(owner.First(s => s.Key == "hh_gid").Value)) : null;
                                            objNHRSHouseOwnerDtl.HouseholdSlipNumber = owner.ContainsKey("old_idslip") ? Utils.ConvertToString(owner.First(s => s.Key == "old_idslip").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(owner.First(s => s.Key == "old_idslip").Value)) : null;
                                            objNHRSHouseOwnerDtl.Approved = "Y";
                                            objNHRSHouseOwnerDtl.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                            objNHRSHouseOwnerDtl.ApprovedDtLoc = objNHRSHouseOwnerDtl.ApprovedDt.ConvertToString();
                                            objNHRSHouseOwnerDtl.ApprovedBy = SessionCheck.getSessionUsername();
                                            objNHRSHouseOwnerDtl.EnteredBy = SessionCheck.getSessionUsername();
                                            objNHRSHouseOwnerDtl.EnteredDt = DateTime.Now.ToDateTime();
                                            objNHRSHouseOwnerDtl.EnteredDtLoc = DateTime.Now.ConvertToString();
                                            objNHRSHouseOwnerDtl.UpdatedBy = SessionCheck.getSessionUsername();
                                            objNHRSHouseOwnerDtl.UpdatedDt = DateTime.Now.ToDateTime();
                                            objNHRSHouseOwnerDtl.UpdatedDtLoc = DateTime.Now.ConvertToString();
                                            objNHRSHouseOwnerDtl.BatchId = batchID;
                                            objNHRSHouseOwnerDtl.Mode = mode;
                                            //HouseOwner
                                            qr = service.SubmitChanges(objNHRSHouseOwnerDtl, true);
                                        }
                                    }

                                    #endregion

                                    


                                    #region Other Houses Detail
                                    foreach (SortedDictionary<dynamic, dynamic> house in survey.First(s => s.Key == "OtherHouses").Value)
                                    {
                                        objNHRSOtherHouseDtl = new MigResurveyHhOthResidenceDtlInfo();
                                        objNHRSOtherHouseDtl.HouseOwnerId = HouseOwnerIDInserted;
                                        objNHRSOtherHouseDtl.OtherResidenceId = house.ContainsKey("haop_sn") ? Utils.ConvertToString(house.First(s => s.Key == "haop_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "haop_sn").Value)) : null;
                                        objNHRSOtherHouseDtl.OtherDistrictCd = house.ContainsKey("oth_dist") ? Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value)) : null;
                                        objNHRSOtherHouseDtl.OtherVdcMunCd = house.ContainsKey("oth_vdc") ? Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value)) : null;
                                        objNHRSOtherHouseDtl.OtherWardNo = house.ContainsKey("oth_ward") ? Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value)) : null;
                                        objNHRSOtherHouseDtl.BuildingConditionCd = house.ContainsKey("conditn") ? Utils.ConvertToString(house.First(s => s.Key == "conditn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "conditn").Value)) : null;
                                        objNHRSOtherHouseDtl.DistrictName = house.ContainsKey("district_name") ? Utils.ConvertToString(house.First(s => s.Key == "district_name").Value) : null;
                                        objNHRSOtherHouseDtl.VDCName = house.ContainsKey("vdc_name") ? Utils.ConvertToString(house.First(s => s.Key == "vdc_name").Value) : null;
                                        objNHRSOtherHouseDtl.Approved = "Y";
                                        objNHRSOtherHouseDtl.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                        objNHRSOtherHouseDtl.ApprovedBy = SessionCheck.getSessionUsername();
                                        objNHRSOtherHouseDtl.EnteredBy = SessionCheck.getSessionUsername();
                                        objNHRSOtherHouseDtl.EnteredDt = DateTime.Now;
                                        objNHRSOtherHouseDtl.BatchId = batchID;
                                        objNHRSOtherHouseDtl.Mode = mode;

                                        //not in old
                                        objNHRSOtherHouseDtl.Resnforgriev = house.ContainsKey("resn_for_griev") ? Utils.ConvertToString(house.First(s => s.Key == "resn_for_griev").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "resn_for_griev").Value)) : null;
                                        objNHRSOtherHouseDtl.Vdcold = house.ContainsKey("vdc_old") ? Utils.ConvertToString(house.First(s => s.Key == "vdc_old").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "vdc_old").Value)) : null;

                                        objNHRSOtherHouseDtl.Wardold = house.ContainsKey("ward_old") ? Utils.ConvertToString(house.First(s => s.Key == "ward_old").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "ward_old").Value)) : null;
                                        objNHRSOtherHouseDtl.Loi_sn = house.ContainsKey("loi_sn") ? Utils.ConvertToString(house.First(s => s.Key == "loi_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "loi_sn").Value)) : null;
                                        objNHRSOtherHouseDtl.Grvsn = house.ContainsKey("grv_sn") ? Utils.ConvertToString(house.First(s => s.Key == "grv_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "grv_sn").Value)) : null;
                                        objNHRSOtherHouseDtl.Othdist = house.ContainsKey("oth_dist") ? Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value)) : null;
                                        objNHRSOtherHouseDtl.Othvdc = house.ContainsKey("oth_vdc") ? Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value)) : null;

                                        objNHRSOtherHouseDtl.Othward = house.ContainsKey("oth_ward") ? Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value)) : null;
                                        objNHRSOtherHouseDtl.Enumid = house.ContainsKey("enum_id") ? Utils.ConvertToString(house.First(s => s.Key == "enum_id").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "enum_id").Value)) : null;
                                        objNHRSOtherHouseDtl.Houseno = house.ContainsKey("house_no") ? Utils.ConvertToString(house.First(s => s.Key == "house_no").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "house_no").Value)) : null;
                                        objNHRSOtherHouseDtl.Enumar = house.ContainsKey("enum_ar") ? Utils.ConvertToString(house.First(s => s.Key == "enum_ar").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "enum_ar").Value)) : null;
                                        //end

                                        qr = service.SubmitChanges(objNHRSOtherHouseDtl, true);

                                    }

                                    #endregion
                                    #region Grievance Detail
                                    foreach (SortedDictionary<dynamic, dynamic> grievance in survey.First(s => s.Key == "Grievance").Value)
                                    {
                                        objMISGrievanceDtl = new RES_NHRS_GRIEVANCE_DTL();
                                        objMISGrievanceDtl.HouseOwnerID = HouseOwnerIDInserted;
                                        objMISGrievanceDtl.DistrictCd = grievance.ContainsKey("district") ? Utils.ConvertToString(grievance.First(s => s.Key == "district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(grievance.First(s => s.Key == "district").Value)) : null;
                                        objMISGrievanceDtl.VDCCd = grievance.ContainsKey("vdc") ? Utils.ConvertToString(grievance.First(s => s.Key == "vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(grievance.First(s => s.Key == "vdc").Value)) : null;
                                        objMISGrievanceDtl.WardCD = grievance.ContainsKey("ward") ? Utils.ConvertToString(grievance.First(s => s.Key == "ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(grievance.First(s => s.Key == "ward").Value)) : null;
                                        objMISGrievanceDtl.EnumerationArea = grievance.ContainsKey("enum_ar") ? Utils.ConvertToString(grievance.First(s => s.Key == "enum_ar").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(grievance.First(s => s.Key == "enum_ar").Value)) : null;
                                        objMISGrievanceDtl.HouseSNO = grievance.ContainsKey("house_no") ? Utils.ConvertToString(grievance.First(s => s.Key == "house_no").Value) : null;
                                        objMISGrievanceDtl.EnumeratorID = grievance.ContainsKey("enum_id") ? Utils.ConvertToString(grievance.First(s => s.Key == "enum_id").Value) : null;
                                        objMISGrievanceDtl.ReasonForGrievance = grievance.ContainsKey("resn_for_griev") ? Utils.ConvertToString(grievance.First(s => s.Key == "resn_for_griev").Value) : null;
                                        objMISGrievanceDtl.VDCOld = grievance.ContainsKey("vdc_old") ? Utils.ConvertToString(grievance.First(s => s.Key == "vdc_old").Value) : null;
                                        objMISGrievanceDtl.WardOld = grievance.ContainsKey("ward_old") ? Utils.ConvertToString(grievance.First(s => s.Key == "ward_old").Value) : null;
                                        objMISGrievanceDtl.LOISN = grievance.ContainsKey("loi_sn") ? Utils.ConvertToString(grievance.First(s => s.Key == "loi_sn").Value) : null;
                                        objMISGrievanceDtl.GrievanceSN = grievance.ContainsKey("grv_sn") ? Utils.ConvertToString(grievance.First(s => s.Key == "grv_sn").Value) : null;
                                        objMISGrievanceDtl.RespondentHHOwner = grievance.ContainsKey("own_res") ? Utils.ConvertToString(grievance.First(s => s.Key == "own_res").Value) : null;
                                        objMISGrievanceDtl.RespondentFirstName = grievance.ContainsKey("name_fst") ? Utils.ConvertToString(grievance.First(s => s.Key == "name_fst").Value) : null;
                                        objMISGrievanceDtl.RespondentMiddleName = grievance.ContainsKey("name_mid") ? Utils.ConvertToString(grievance.First(s => s.Key == "name_mid").Value) : null;
                                        objMISGrievanceDtl.RespondentLastName = grievance.ContainsKey("name_lst") ? Utils.ConvertToString(grievance.First(s => s.Key == "name_lst").Value) : null;
                                        objMISGrievanceDtl.RespondentGender = grievance.ContainsKey("gender") ? Utils.ConvertToString(grievance.First(s => s.Key == "gender").Value) : null;
                                        objMISGrievanceDtl.RelationWithHowner = grievance.ContainsKey("relation") ? Utils.ConvertToString(grievance.First(s => s.Key == "relation").Value) : null;
                                        objMISGrievanceDtl.OtherRelationWithHowner = grievance.ContainsKey("relatnod") ? Utils.ConvertToString(grievance.First(s => s.Key == "relatnod").Value) : null;
                                        objMISGrievanceDtl.RespondentPhoto = grievance.ContainsKey("photo") ? Utils.ConvertToString(grievance.First(s => s.Key == "photo").Value) : null;
                                        objMISGrievanceDtl.HouseinSamePlace = grievance.ContainsKey("house_sa") ? Utils.ConvertToString(grievance.First(s => s.Key == "house_sa").Value) : null;
                                        objMISGrievanceDtl.HouseinOtherPlace = grievance.ContainsKey("house_di") ? Utils.ConvertToString(grievance.First(s => s.Key == "house_di").Value) : null;
                                        objMISGrievanceDtl.NotDamageOtherBuildingCount = grievance.ContainsKey("ndam_c") ? Utils.ConvertToString(grievance.First(s => s.Key == "ndam_c").Value) : null;
                                        objMISGrievanceDtl.PartialDamageOtherBuildingCount = grievance.ContainsKey("pdam_c") ? Utils.ConvertToString(grievance.First(s => s.Key == "pdam_c").Value) : null;
                                        objMISGrievanceDtl.OtherBuildingDamageCount = grievance.ContainsKey("cdam_c") ? Utils.ConvertToString(grievance.First(s => s.Key == "cdam_c").Value) : null;
                                        objMISGrievanceDtl.HHOwnerContact = grievance.ContainsKey("tel") ? Utils.ConvertToString(grievance.First(s => s.Key == "tel").Value) : null;
                                        objMISGrievanceDtl.IdentitySlipNumber = grievance.ContainsKey("idsl_no") ? Utils.ConvertToString(grievance.First(s => s.Key == "idsl_no").Value) : null;
                                        objMISGrievanceDtl.IdentitySlipPhoto = grievance.ContainsKey("idsl_ph") ? Utils.ConvertToString(grievance.First(s => s.Key == "idsl_ph").Value) : null;
                                        objMISGrievanceDtl.WardRepresentativeName = grievance.ContainsKey("rep_name") ? Utils.ConvertToString(grievance.First(s => s.Key == "rep_name").Value) : null;
                                        objMISGrievanceDtl.WardPersonnel = grievance.ContainsKey("ward_personnel") ? Utils.ConvertToString(grievance.First(s => s.Key == "ward_personnel").Value) : null;
                                        objMISGrievanceDtl.PhotoOfAgreement = grievance.ContainsKey("agree_photo") ? Utils.ConvertToString(grievance.First(s => s.Key == "agree_photo").Value) : null;
                                        objMISGrievanceDtl.Approved = "Y";
                                        objMISGrievanceDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                        objMISGrievanceDtl.ApprovedDTLoc = objNHRSHouseOwnerDtl.ApprovedDt.ConvertToString();
                                        objMISGrievanceDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                        objMISGrievanceDtl.EnteredBY = SessionCheck.getSessionUsername();
                                        objMISGrievanceDtl.EnteredDT = DateTime.Now.ConvertToString();
                                        objMISGrievanceDtl.EnteredDTLoc = DateTime.Now.ConvertToString();
                                        objMISGrievanceDtl.UpdatedBY = SessionCheck.getSessionUsername();
                                        objMISGrievanceDtl.UpdatedDT = DateTime.Now.ConvertToString();
                                        objMISGrievanceDtl.UpdatedDTLoc = DateTime.Now.ConvertToString();
                                        objMISGrievanceDtl.Batch = batchID.ConvertToString();
                                        objMISGrievanceDtl.Mode = mode;
                                        service.PackageName = "MIGNHRS.PKG_RES_GRIEVANCE_HOUSE_OWNER";
                                        qr = service.SubmitChanges(objMISGrievanceDtl, true);

                                    }

                                    #endregion
                                    #region Buildings' Detail
                                    foreach (SortedDictionary<dynamic, dynamic> building in survey.First(s => s.Key == "Buildings").Value)
                                    {
                                        objNHRSBuildingAssMst = new RES_NHRS_BUILDING_ASS_MST();
                                        //objNHRSBuildingAssMst.HouseOwnerID = HouseOwnerIDInserted;
                                        objNHRSBuildingAssMst.HouseOwnerID = HouseOwnerIDInserted;
                                        objNHRSBuildingAssMst.DistrictCD = building.ContainsKey("district") ? Utils.ConvertToString(building.First(s => s.Key == "district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "district").Value)) : null;
                                        objNHRSBuildingAssMst.HouseLandLegalOwner = building.ContainsKey("legl_own") ? Utils.ConvertToString(building.First(s => s.Key == "legl_own").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "legl_own").Value)) : null;
                                        //if (objNHRSBuildingAssMst.Houselandlegalowner.ConvertToString() != "")
                                        //{
                                        //houseCnt++;
                                        #region Building Assessment Master Data
                                        houseCnt++;
                                        objNHRSBuildingAssMst.BSSN = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;
                                        objNHRSBuildingAssMst.BuildingCondition = building.ContainsKey("house_cd") ? Utils.ConvertToString(building.First(s => s.Key == "house_cd").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "house_cd").Value)) : null;

                                        objNHRSBuildingAssMst.StoreysCntBefore = building.ContainsKey("stcn_pre") ? Utils.ConvertToString(building.First(s => s.Key == "stcn_pre").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "stcn_pre").Value)) : null;

                                        objNHRSBuildingAssMst.StoreysCntAfter = building.ContainsKey("stcn_pos") ? Utils.ConvertToString(building.First(s => s.Key == "stcn_pos").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "stcn_pos").Value)) : null;

                                        objNHRSBuildingAssMst.HouseAge = building.ContainsKey("age") ? Utils.ConvertToString(building.First(s => s.Key == "age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "age").Value)) : null;

                                        objNHRSBuildingAssMst.PlinthArea = building.ContainsKey("pl_area") ? Utils.ConvertToString(building.First(s => s.Key == "pl_area").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "pl_area").Value)) : null;

                                        objNHRSBuildingAssMst.HouseHeightBeforeEQ = building.ContainsKey("hgt_pre") ? Utils.ConvertToString(building.First(s => s.Key == "hgt_pre").Value) : null;

                                        objNHRSBuildingAssMst.HouseHeightAfterEQ = building.ContainsKey("hgt_pos") ? Utils.ConvertToString(building.First(s => s.Key == "hgt_pos").Value) : null;

                                        objNHRSBuildingAssMst.GroundSurfaceCD = building.ContainsKey("surface") ? Utils.ConvertToString(building.First(s => s.Key == "surface").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "surface").Value)) : null;

                                        objNHRSBuildingAssMst.FoundationTypeCD = building.ContainsKey("foundatn") ? Utils.ConvertToString(building.First(s => s.Key == "foundatn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "foundatn").Value)) : null;


                                        objNHRSBuildingAssMst.RoofType = building.ContainsKey("roof") ? Utils.ConvertToString(building.First(s => s.Key == "roof").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "roof").Value)) : null;

                                        objNHRSBuildingAssMst.GroundFloorType = building.ContainsKey("gd_floor") ? Utils.ConvertToString(building.First(s => s.Key == "gd_floor").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "gd_floor").Value)) : null;

                                        objNHRSBuildingAssMst.UPFloor = building.ContainsKey("up_floor") ? Utils.ConvertToString(building.First(s => s.Key == "up_floor").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "up_floor").Value)) : null;

                                        objNHRSBuildingAssMst.HousePosition = building.ContainsKey("positn") ? Utils.ConvertToString(building.First(s => s.Key == "positn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "positn").Value)) : null;

                                        objNHRSBuildingAssMst.PlanConfiguration = building.ContainsKey("pln_conf") ? Utils.ConvertToString(building.First(s => s.Key == "pln_conf").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "pln_conf").Value)) : null;

                                        objNHRSBuildingAssMst.GeoRiskFelt = building.ContainsKey("gersk") ? (Utils.ConvertToString(building.First(s => s.Key == "gersk").Value) == "1" ? "Y" : "N") : "N";

                                        objNHRSBuildingAssMst.AssessedArea = building.ContainsKey("assd_ar") ? Utils.ConvertToString(building.First(s => s.Key == "assd_ar").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "assd_ar").Value)) : null;

                                        objNHRSBuildingAssMst.TechnicalSolution = building.ContainsKey("tech_sol") ? Utils.ConvertToString(building.First(s => s.Key == "tech_sol").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "tech_sol").Value)) : null;

                                        objNHRSBuildingAssMst.DamageGrade = building.ContainsKey("dm_grade") ? Utils.ConvertToString(building.First(s => s.Key == "dm_grade").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "dm_grade").Value)) : null;

                                        objNHRSBuildingAssMst.ReconstructionStarted = building.ContainsKey("repair") ? (Utils.ConvertToString(building.First(s => s.Key == "repair").Value) == "1" ? "Y" : "N") : "N";

                                        objNHRSBuildingAssMst.SecondaryUsed = building.ContainsKey("sec_use") ? (Utils.ConvertToString(building.First(s => s.Key == "sec_use").Value) == "1" ? "Y" : "N") : "N";
                                        objNHRSBuildingAssMst.VdcCD = building.ContainsKey("vdc") ? Utils.ConvertToString(building.First(s => s.Key == "vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "vdc").Value)) : null;
                                        objNHRSBuildingAssMst.Ward = building.ContainsKey("ward") ? Utils.ConvertToString(building.First(s => s.Key == "ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "ward").Value)) : null;
                                        objNHRSBuildingAssMst.VdcOldCD = building.ContainsKey("vdc_old") ? Utils.ConvertToString(building.First(s => s.Key == "vdc_old").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "vdc_old").Value)) : null;
                                        objNHRSBuildingAssMst.wardOld = building.ContainsKey("ward_old") ? Utils.ConvertToString(building.First(s => s.Key == "ward_old").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "ward_old").Value)) : null;
                                        objNHRSBuildingAssMst.OtherRisk = building.ContainsKey("oth_risk") ? Utils.ConvertToString(building.First(s => s.Key == "oth_risk").Value) : null;
                                        objNHRSBuildingAssMst.Preecision = building.ContainsKey("prc") ? Utils.ConvertToString(building.First(s => s.Key == "prc").Value) : null;
                                        objNHRSBuildingAssMst.GeneralComment   = building.ContainsKey("gen_cmnt") ? Utils.ConvertToString(building.First(s => s.Key == "gen_cmnt").Value) : null;

                                        objNHRSBuildingAssMst.Latitude = building.ContainsKey("lat") ? Utils.ConvertToString(building.First(s => s.Key == "lat").Value) : null;
                                        objNHRSBuildingAssMst.Longitude = building.ContainsKey("lon") ? Utils.ConvertToString(building.First(s => s.Key == "lon").Value) : null;
                                        objNHRSBuildingAssMst.Altitude = building.ContainsKey("alt") ? Utils.ConvertToString(building.First(s => s.Key == "alt").Value) : null;
                                        //objNHRSBuildingAssMst.Accuracy = "";
                                        objNHRSBuildingAssMst.Approved = "Y";
                                        objNHRSBuildingAssMst.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                        objNHRSBuildingAssMst.ApprovedBY = SessionCheck.getSessionUsername();
                                        objNHRSBuildingAssMst.ApprovedDTLoc = objNHRSBuildingAssMst.ApprovedDT.ConvertToString();
                                        objNHRSBuildingAssMst.EnteredBY = SessionCheck.getSessionUsername();
                                        objNHRSBuildingAssMst.EnteredDT = DateTime.Now;
                                        objNHRSBuildingAssMst.EnteredDTLoc = DateTime.Now.ConvertToString();
                                        objNHRSBuildingAssMst.UpdatedDTLoc = DateTime.Now.ConvertToString();
                                        //objNHRSBuildingAssMst.IPAddress = CommonVariables.IPAddress;
                                        objNHRSBuildingAssMst.BatchID = batchID;
                                        objNHRSBuildingAssMst.Mode = mode;
                                   
                                        service.PackageName = "MIGNHRS.PKG_RES__BUILDING_ASSESSMENT";
                                        qr = service.SubmitChanges(objNHRSBuildingAssMst, true);  
                                     
                                        #endregion

                                        #region Building Photos
                                        if (qr.IsSuccess)
                                        {
                                            objNHRSBuildingAssPhoto = new RES_NHRS_BUILDING_ASS_PHOTO();
                                            objNHRSBuildingAssPhoto.HouseOwnerID = HouseOwnerIDInserted;
                                            objNHRSBuildingAssPhoto.BuildingStructureNO = objNHRSBuildingAssMst.BSSN;
                                            //objNHRSBuildingAssPhoto.NHRSUUID = objNHRSBuildingAssPhoto.NHRSUUID;

                                            objNHRSBuildingAssPhoto.HousePhotoID = building.ContainsKey("") ? Utils.ConvertToString(building.First(s => s.Key == "surf_con").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "surf_con").Value)) : null;
                                             //objNHRSBuildingAssPhoto.CommentEng = building.ContainsKey("ona_id") ? Utils.ConvertToString(building.First(s => s.Key == "ona_id").Value) : null;

                                            objNHRSBuildingAssPhoto.Approved = "Y";
                                            objNHRSBuildingAssPhoto.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                            objNHRSBuildingAssPhoto.ApprovedBY = SessionCheck.getSessionUsername();
                                            objNHRSBuildingAssPhoto.ApprovedDTLoc = objNHRSBuildingAssMst.ApprovedDT.ConvertToString();
                                            objNHRSBuildingAssPhoto.EnteredBY = SessionCheck.getSessionUsername();
                                            objNHRSBuildingAssPhoto.EnteredDT = DateTime.Now;
                                            objNHRSBuildingAssPhoto.EnteredDTLoc = DateTime.Now.ConvertToString();
                                            objNHRSBuildingAssPhoto.UpdatedDTLoc = DateTime.Now.ConvertToString();
                                            objNHRSBuildingAssPhoto.IPAddress = CommonVariables.IPAddress;
                                            objNHRSBuildingAssPhoto.BatchID = batchID;
                                            objNHRSBuildingAssPhoto.Mode = mode;

                                            service.PackageName = "MIGNHRS.PKG_RES__BUILDING_ASSESSMENT";
                                           
                                            string photoName = String.Empty;

                                            if (building.ContainsKey("photo_bk"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(12);
                                                photoName = building.ContainsKey("photo_bk") ? Utils.ConvertToString(building.First(s => s.Key == "photo_bk").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("photo_fr"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToDecimal(13);
                                                photoName = building.ContainsKey("photo_fr") ? Utils.ConvertToString(building.First(s => s.Key == "photo_fr").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("photo_lt"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(14);
                                                photoName = building.ContainsKey("photo_lt") ? Utils.ConvertToString(building.First(s => s.Key == "photo_lt").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("photo_rt"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(15);
                                                photoName = building.ContainsKey("photo_rt") ? Utils.ConvertToString(building.First(s => s.Key == "photo_rt").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("photo_in"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(16);
                                                photoName = building.ContainsKey("photo_in") ? Utils.ConvertToString(building.First(s => s.Key == "photo_in").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("photo_lo"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(17);
                                                photoName = building.ContainsKey("photo_lo") ? Utils.ConvertToString(building.First(s => s.Key == "photo_lo").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht1"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(18);
                                                photoName = building.ContainsKey("ph_ksht1") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht1").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht2"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(19);
                                                photoName = building.ContainsKey("ph_ksht2") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht2").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht3"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(20);
                                                photoName = building.ContainsKey("ph_ksht3") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht3").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht4"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(20);
                                                photoName = building.ContainsKey("ph_ksht4") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht4").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht5"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(20);
                                                photoName = building.ContainsKey("ph_ksht5") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht5").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht6"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(20);
                                                photoName = building.ContainsKey("ph_ksht6") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht6").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht7"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(20);
                                                photoName = building.ContainsKey("ph_ksht7") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht7").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                            if (building.ContainsKey("ph_ksht8"))
                                            {
                                                objNHRSBuildingAssPhoto.DocTypeCD = Convert.ToInt32(20);
                                                photoName = building.ContainsKey("ph_ksht8") ? Utils.ConvertToString(building.First(s => s.Key == "ph_ksht8").Value) : null;
                                                objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                            }
                                        }

                                        #endregion

                                        #region Building Assessment Detail
                                        if (qr.IsSuccess)
                                        {
                                            objNHRSBuildingAssDtl = new RES_NHRS_BUILDING_ASS_DTL();
                                            objNHRSBuildingAssDtl.HouseOwnerID = HouseOwnerIDInserted;
                                            //objNHRSBuildingAssDtl.BuildingStructureNO = "1";
                                           // objNHRSBuildingAssDtl.HouseOwnerId = HouseOwnerIDInserted;
                                            //objNHRSBuildingAssDtl.DistrictCd = objNHRSBuildingAssMst.DistrictCd;
                                            //objNHRSBuildingAssDtl.BuildingStructureNo = objNHRSBuildingAssMst.BuildingStructureNo;

                                            objNHRSBuildingAssDtl.DistrictCD = survey.ContainsKey("district") ? Utils.ConvertToString(survey.First(s => s.Key == "district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "district").Value)) : null;
                                            objNHRSBuildingAssDtl.VdcCD = survey.ContainsKey("vdc") ? Utils.ConvertToString(survey.First(s => s.Key == "vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "vdc").Value)) : null;
                                            objNHRSBuildingAssDtl.Ward = survey.ContainsKey("ward") ? Utils.ConvertToString(survey.First(s => s.Key == "ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "ward").Value)) : null;
                         


                                            objNHRSBuildingAssDtl.VdcOldCD = survey.ContainsKey("vdc_old") ? Utils.ToDecimal(survey.First(s => s.Key == "vdc_old").Value) : null;
                                            objNHRSBuildingAssDtl.WardOld = survey.ContainsKey("ward_old") ? Utils.ToDecimal(survey.First(s => s.Key == "ward_old").Value) : null;

                                            objNHRSBuildingAssDtl.CommentEng = building.ContainsKey("gen_cmnt") ? Utils.ConvertToString(building.First(s => s.Key == "gen_cmnt").Value) : null;
                                            //objNHRSBuildingAssDtl.CommentLoc = building.ContainsKey("ona_id") ? Utils.ConvertToString(building.First(s => s.Key == "ona_id").Value) : null;
           

                                            objNHRSBuildingAssDtl.Approved = "Y";
                                            objNHRSBuildingAssDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                            objNHRSBuildingAssDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                            objNHRSBuildingAssDtl.ApprovedDTLoc = objNHRSBuildingAssMst.ApprovedDT.ConvertToString();
                                            objNHRSBuildingAssDtl.EnteredBY = SessionCheck.getSessionUsername();
                                            objNHRSBuildingAssDtl.EnteredDT = DateTime.Now;
                                            objNHRSBuildingAssDtl.EnteredDTLoc = DateTime.Now.ConvertToString();
                                            objNHRSBuildingAssDtl.UpdatedDTLoc = DateTime.Now.ConvertToString();
                                            //objNHRSBuildingAssDtl.IPAddress = CommonVariables.IPAddress;
                                            objNHRSBuildingAssDtl.BatchID = batchID;
                                            objNHRSBuildingAssDtl.Mode = mode;
                                             qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                             string damageleveldtl = string.Empty;
                                            if (building.ContainsKey("collapse"))
                                            {
                                                damageleveldtl = building.ContainsKey("collapse") ? Utils.ConvertToString(building.First(s => s.Key == "collapse").Value) : null;

                                                objNHRSBuildingAssDtl.DamageCD = 2;
                                                //objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                //qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);

                                                if (damageleveldtl == "4")
                                                {
                                                    objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                    objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                    qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                }
                                                else
                                                {
                                                    dynamic arrDamageLevelDtl = damageleveldtl.Split(' ');
                                                    foreach (var item in arrDamageLevelDtl)
                                                    {
                                                        if (!string.IsNullOrEmpty(item))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(item);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("leaning"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 3;
                                                damageleveldtl = building.ContainsKey("leaning") ? Utils.ConvertToString(building.First(s => s.Key == "leaning").Value) : null;
                                                //objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                //qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                if (damageleveldtl == "4")
                                                {
                                                    objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                    objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                    qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                }
                                                else
                                                {
                                                    dynamic arrDamageLevelDtl = damageleveldtl.Split(' ');
                                                    foreach (var item in arrDamageLevelDtl)
                                                    {
                                                        if (!string.IsNullOrEmpty(item))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(item);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("adjbld_rsk"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 4;
                                                damageleveldtl = building.ContainsKey("adjbld_rsk") ? Utils.ConvertToString(building.First(s => s.Key == "adjbld_rsk").Value) : null;
                                                //objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                //qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                if (damageleveldtl == "4")
                                                {
                                                    objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                    objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                    qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                }
                                                else
                                                {
                                                    dynamic arrDamageLevelDtl = damageleveldtl.Split(' ');
                                                    foreach (var item in arrDamageLevelDtl)
                                                    {
                                                        if (!string.IsNullOrEmpty(item))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(item);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("dm_fndtn"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 6;
                                                damageleveldtl = building.ContainsKey("dm_fndtn") ? Utils.ConvertToString(building.First(s => s.Key == "dm_fndtn").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("dm_fndtn" + i) ? Utils.ConvertToString(building.First(s => s.Key == "dm_fndtn" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }

                                            if (building.ContainsKey("dm_roof"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 7;
                                                damageleveldtl = building.ContainsKey("dm_roof" ) ? Utils.ConvertToString(building.First(s => s.Key == "dm_roof").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("dm_roof" + i) ? Utils.ConvertToString(building.First(s => s.Key == "dm_roof" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "nhrs_damage_level_dtl", "damage_level_dtl_cd", "damage_level_cd")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "nhrs_damage_level_dtl", "damage_level_dtl_cd", "damage_level_cd")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("corn_sep"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 9;
                                                damageleveldtl = building.ContainsKey("corn_sep") ? Utils.ConvertToString(building.First(s => s.Key == "corn_sep").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("corn_sep" + i) ? Utils.ConvertToString(building.First(s => s.Key == "corn_sep" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("diag_cr"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 10;
                                                damageleveldtl = building.ContainsKey("diag_cr") ? Utils.ConvertToString(building.First(s => s.Key == "diag_cr").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("diag_cr" + i) ? Utils.ConvertToString(building.First(s => s.Key == "diag_cr" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("pl_fail"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 11;
                                                damageleveldtl = building.ContainsKey("pl_fail") ? Utils.ConvertToString(building.First(s => s.Key == "pl_fail").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("pl_fail" + i) ? Utils.ConvertToString(building.First(s => s.Key == "pl_fail" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("op_fail"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 12;
                                                damageleveldtl = building.ContainsKey("op_fail") ? Utils.ConvertToString(building.First(s => s.Key == "op_fail").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("op_fail" + i) ? Utils.ConvertToString(building.First(s => s.Key == "op_fail" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("op_fl_nl"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 13;
                                                damageleveldtl = building.ContainsKey("op_fl_nl") ? Utils.ConvertToString(building.First(s => s.Key == "op_fl_nl").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("op_fl_nl" + i) ? Utils.ConvertToString(building.First(s => s.Key == "op_fl_nl" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("dm_gabl"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 14;
                                                damageleveldtl = building.ContainsKey("dm_gabl") ? Utils.ConvertToString(building.First(s => s.Key == "dm_gabl").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("dm_gabl" + i) ? Utils.ConvertToString(building.First(s => s.Key == "dm_gabl" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("delam"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 15;
                                                damageleveldtl = building.ContainsKey("delam") ? Utils.ConvertToString(building.First(s => s.Key == "delam").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("delam" + i) ? Utils.ConvertToString(building.First(s => s.Key == "delam" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("col_fail"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 17;
                                                damageleveldtl = building.ContainsKey("col_fail") ? Utils.ConvertToString(building.First(s => s.Key == "col_fail").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("col_fail" + i) ? Utils.ConvertToString(building.First(s => s.Key == "col_fail" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("beam_fl"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 18;
                                                damageleveldtl = building.ContainsKey("beam_fl") ? Utils.ConvertToString(building.First(s => s.Key == "beam_fl").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("beam_fl" + i) ? Utils.ConvertToString(building.First(s => s.Key == "beam_fl" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("part_col"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 19;
                                                damageleveldtl = building.ContainsKey("part_col") ? Utils.ConvertToString(building.First(s => s.Key == "part_col").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("part_col" + i) ? Utils.ConvertToString(building.First(s => s.Key == "part_col" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("str_case"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 22;
                                                damageleveldtl = building.ContainsKey("str_case") ? Utils.ConvertToString(building.First(s => s.Key == "str_case").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("str_case" + i) ? Utils.ConvertToString(building.First(s => s.Key == "str_case" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("parapet"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 23;
                                                damageleveldtl = building.ContainsKey("parapet") ? Utils.ConvertToString(building.First(s => s.Key == "parapet").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("parapet" + i) ? Utils.ConvertToString(building.First(s => s.Key == "parapet" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("clad_glz"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 24;
                                                damageleveldtl = building.ContainsKey("clad_glz") ? Utils.ConvertToString(building.First(s => s.Key == "clad_glz").Value) : null;
                                                if (damageleveldtl == "8" || damageleveldtl == "9")
                                                {
                                                    if (damageleveldtl == "8")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(10);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                    else if (damageleveldtl == "9")
                                                    {
                                                        objNHRSBuildingAssDtl.DamageLevelCD = Convert.ToDecimal(11);
                                                        objNHRSBuildingAssDtl.DamageLevelDtlCD = 1;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    }
                                                }
                                                else
                                                {
                                                    for (int i = 1; i <= 3; i++)
                                                    {
                                                        damageleveldtl = building.ContainsKey("clad_glz" + i) ? Utils.ConvertToString(building.First(s => s.Key == "clad_glz" + i).Value) : null;
                                                        if (!string.IsNullOrEmpty(damageleveldtl))
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelDtlCD = Convert.ToDecimal(damageleveldtl);
                                                            objNHRSBuildingAssDtl.DamageLevelCD = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCD.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }

                                                    }
                                                }
                                            }
                                            if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment"))
                                            {
                                                objNHRSBuildingAssDtl.DamageCD = 25;
                                                objNHRSBuildingAssDtl.CommentEng = objNHRSBuildingAssDtl.CommentLoc = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment").Value) : null;
                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                            }
                                        }

                                        
                                        #endregion
                                        #region Super Structure
                                        if (building.ContainsKey("sup_str"))
                                        {
                                            string superStruct = String.Empty;
                                            superStruct = building.ContainsKey("sup_str") ? Utils.ConvertToString(building.First(s => s.Key == "sup_str").Value) : null;
                                            if (superStruct.ConvertToString() != "")
                                            {
                                                dynamic arrSuperStruct = superStruct.Split(' ');
                                                int superStructInc = 1;
                                                foreach (var item in arrSuperStruct)
                                                {
                                                    if (!string.IsNullOrEmpty(item))
                                                    {
                                                        objNHRSSuperStructure = new RES_NHRS_BA_SUPERSTRUCT_MAT();
                                                        objNHRSSuperStructure.HouseOwnerID = HouseOwnerIDInserted;
                                                        objNHRSSuperStructure.buildingStructureNo = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;
                                                        objNHRSSuperStructure.SuperstructureMatId = building.ContainsKey("sup_str" + superStructInc) ? Utils.ConvertToString(building.First(s => s.Key == "sup_str" + superStructInc).Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "sup_str" + superStructInc).Value)) : null;

                                                        objNHRSSuperStructure.Approved = "Y";
                                                        objNHRSSuperStructure.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                        objNHRSSuperStructure.ApprovedBy = SessionCheck.getSessionUsername();
                                                        objNHRSSuperStructure.EnteredBy = SessionCheck.getSessionUsername();
                                                        objNHRSSuperStructure.EnteredDt = DateTime.Now.ConvertToString();
                                                        objNHRSSuperStructure.IpAddress = CommonVariables.IPAddress;
                                                        objNHRSSuperStructure.BatchID = batchID;
                                                        objNHRSSuperStructure.Mode = mode;
                                                        qr = service.SubmitChanges(objNHRSSuperStructure, true);
                                                    }
                                                    superStructInc++;
                                                }
                                            }
                                        }
                                        #endregion
                                        #region Secondary Uses
                                        if (objNHRSBuildingAssMst.SecondaryUsed.ConvertToString() != "" && objNHRSBuildingAssMst.SecondaryUsed.ConvertToString() == "Y")
                                        {
                                            string secUses = String.Empty;
                                            secUses = building.ContainsKey("secuse_ls") ? Utils.ConvertToString(building.First(s => s.Key == "secuse_ls").Value) : null;
                                            if (secUses.ConvertToString() != "")
                                            {
                                                dynamic arrSecUses = secUses.Split(' ');
                                                int secUsesInc = 1;
                                                foreach (var item in arrSecUses)
                                                {
                                                    objNHRSSecondaryOccupancy = new RES_NHRS_BA_SEC_OCCUPANCY();
                                                    objNHRSSecondaryOccupancy.HouseOwnerID = HouseOwnerIDInserted;
                                                    objNHRSSecondaryOccupancy.buildingStructureNo = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;
                                                    objNHRSSecondaryOccupancy.SecOccupancyCD = building.ContainsKey("secuse_ls" + secUsesInc) ? Utils.ConvertToString(building.First(s => s.Key == "secuse_ls" + secUsesInc).Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "secuse_ls" + secUsesInc).Value)) : null;

                                                    objNHRSSecondaryOccupancy.NhrsuuID = objNHRSHouseOwnerMst.NhrsUuid;
                                                    objNHRSSecondaryOccupancy.Approved = "Y";
                                                    objNHRSSecondaryOccupancy.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                    objNHRSSecondaryOccupancy.ApprovedBy = SessionCheck.getSessionUsername();
                                                    objNHRSSecondaryOccupancy.EnteredBy = SessionCheck.getSessionUsername();
                                                    objNHRSSecondaryOccupancy.EnteredDt = DateTime.Now.ConvertToString();
                                                    objNHRSSecondaryOccupancy.BatchID = batchID;
                                                    objNHRSSecondaryOccupancy.Mode = mode;
                                                    qr = service.SubmitChanges(objNHRSSecondaryOccupancy, true);
                                                    secUsesInc++;
                                                }
                                            }
                                        }
                                        #endregion
                                        #region Geo-Technical Risks
                                        //if (objNHRSBuildingAssMst.IsGeotechnicalRisk.ConvertToString() != "" && objNHRSBuildingAssMst.IsGeotechnicalRisk == "Y")
                                        {
                                            string geoRisks = String.Empty;
                                            geoRisks = building.ContainsKey("gersk_ls") ? Utils.ConvertToString(building.First(s => s.Key == "gersk_ls").Value) : null;
                                            if (geoRisks.ConvertToString() != "")
                                            {
                                                dynamic arrGeoRisks = geoRisks.Split(' ');
                                                int geoRisksInc = 1;
                                                foreach (var item in arrGeoRisks)
                                                {
                                                    objNHRSBaGeotechnicalRisk = new RES_NHRS_GEOTECHNICAL_RISK();
                                                    objNHRSBaGeotechnicalRisk.HouseOwnerID = HouseOwnerIDInserted;
                                                    objNHRSBaGeotechnicalRisk.BuildingStructueNO = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;

                                                    objNHRSBaGeotechnicalRisk.GeotechnicalRiskTypeCD = building.ContainsKey("gersk_ls" + geoRisksInc) ? Utils.ConvertToString(building.First(s => s.Key == "gersk_ls" + geoRisksInc).Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "gersk_ls" + geoRisksInc).Value)) : null;

                                                    objNHRSBaGeotechnicalRisk.OtherGeotechRiskEng = objNHRSBaGeotechnicalRisk.OtherGeotechRiskLoc = building.ContainsKey("oth_risk") ? Utils.ConvertToString(building.First(s => s.Key == "oth_risk").Value) : null;
                                                    objNHRSBaGeotechnicalRisk.Approved = "Y";
                                                    objNHRSBaGeotechnicalRisk.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                    objNHRSBaGeotechnicalRisk.ApprovedBy = SessionCheck.getSessionUsername();
                                                    objNHRSBaGeotechnicalRisk.EnteredBy = SessionCheck.getSessionUsername();
                                                    objNHRSBaGeotechnicalRisk.EnteredDt = DateTime.Now.ConvertToString();
                                                    objNHRSBaGeotechnicalRisk.BatchID = batchID;
                                                    objNHRSBaGeotechnicalRisk.Mode = mode;
                                                    qr = service.SubmitChanges(objNHRSBaGeotechnicalRisk, true);
                                                    geoRisksInc++;
                                                }
                                            }
                                        }
                                        #endregion
                                        #region Families
                                        if (building.ContainsKey("Families"))
                                        {
                                            foreach (SortedDictionary<dynamic, dynamic> family in building.First(s => s.Key == "Families").Value)
                                            {
                                                if (Utils.ConvertToString(family.First(s => s.Key == "ona_id").Value) == "88288")
                                                {

                                                }
                                                //if (startWriteFamily==0)
                                                //{
                                                //tw.WriteLine(Utils.ConvertToString(family.First(s => s.Key == "ona_id").Value) + " : Family Start: " + DateTime.Now.ToString());
                                                //}

                                                lstmember = new List<Dictionary<string, string>>();
                                                #region Household Detail
                                                objMisHouseholdInfo = new RES_MIS_HOUSEHOLD_INFO();
                                                objMisHouseholdInfo.HouseOwnerID = HouseOwnerIDInserted;
                                                objMisHouseholdInfo.HouseHoldDefinedCode = family.ContainsKey("loi_sn") ? Utils.ConvertToString(family.First(s => s.Key == "loi_sn").Value) : null;
                                                objMisHouseholdInfo.InstanceUniqueSNO = objMISGrievanceDtl.IdentitySlipNumber;
                                                objMisHouseholdInfo.buildingStructureNo = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;
                                                objMisHouseholdInfo.HouseNO = building.ContainsKey("house_no") ? Utils.ConvertToString(building.First(s => s.Key == "house_no").Value) : null;
                                               
                                                if (objMisHouseholdInfo.HouseHoldDefinedCode.ConvertToString() != "")
                                                {
                                                    householdCnt++;
                                                    #region Insert Household Info
                                                    objMisHouseholdInfo.buildingStructureNo = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;
                                                    if (family.ContainsKey("Members"))
                                                    {
                                                        decimal? relation = 0;
                                                        foreach (SortedDictionary<dynamic, dynamic> member in family.First(s => s.Key == "Members").Value)
                                                        {
                                                            relation = member.ContainsKey("hh_rel") ? Utils.ConvertToString(member.First(s => s.Key == "hh_rel").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "hh_rel").Value)) : null;
                                                            if (relation == 1)
                                                            {
                                                                objMisHouseholdInfo.MemberDefinedCD = member.ContainsKey("mem_sn") ? Utils.ConvertToString(member.First(s => s.Key == "mem_sn").Value) : null;
                                                                objMisHouseholdInfo.MemberID = member.ContainsKey("mem_sn") ? Utils.ConvertToString(member.First(s => s.Key == "mem_sn").Value) : null;
                                                                break;
                                                            }
                                                        }
                                                        if (relation == 0)
                                                        {
                                                            foreach (SortedDictionary<dynamic, dynamic> member in family.First(s => s.Key == "Members").Value)
                                                            {
                                                                string memFirstName = member.ContainsKey("name_fst") ? Utils.ConvertToString(member.First(s => s.Key == "name_fst").Value) : null;
                                                                string memMiddleName = member.ContainsKey("name_mid") ? Utils.ConvertToString(member.First(s => s.Key == "name_mid").Value) : null;
                                                                string memLastName = member.ContainsKey("name_lst") ? Utils.ConvertToString(member.First(s => s.Key == "name_lst").Value) : null;

                                                                objMisHouseholdInfo.FirstNameEng = objMisHouseholdInfo.FirstNameLoc = family.ContainsKey("name_fst") ? Utils.ConvertToString(family.First(s => s.Key == "name_fst").Value) : null;
                                                                objMisHouseholdInfo.MiddleNameEng = objMisHouseholdInfo.MiddleNameLoc = family.ContainsKey("name_mid") ? Utils.ConvertToString(family.First(s => s.Key == "name_mid").Value) : null;
                                                                objMisHouseholdInfo.LastNameEng = objMisHouseholdInfo.LastNameLoc = family.ContainsKey("name_lst") ? Utils.ConvertToString(family.First(s => s.Key == "name_lst").Value) : null;

                                                                objMisHouseholdInfo.FullNameEng = objMisHouseholdInfo.FullNameLoc = objMisHouseholdInfo.FirstNameEng.ConvertToString() + (objMisHouseholdInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisHouseholdInfo.MiddleNameEng) + " ") + objMisHouseholdInfo.LastNameEng.ConvertToString();

                                                                string strMemberFullName = memFirstName.ConvertToString() + (memMiddleName.ConvertToString() == "" ? " " : (" " + memMiddleName.ConvertToString()) + " ") + memLastName.ConvertToString();
                                                                if (objMisHouseholdInfo.FirstNameEng.ToUpper() == strMemberFullName.ToUpper())
                                                                {
                                                                    objMisHouseholdInfo.MemberDefinedCD = member.ContainsKey("mem_sn") ? Utils.ConvertToString(member.First(s => s.Key == "mem_sn").Value) : null;
                                                                    objMisHouseholdInfo.MemberID = member.ContainsKey("mem_sn") ? Utils.ConvertToString(member.First(s => s.Key == "mem_sn").Value) : null;
                                                                    break;
                                                                }

                                                            }


                                                        }
                                                    }
                                                    if (objNHRSHouseOwnerMst.DistrictCd.ConvertToString() == "")
                                                    {
                                                        string aa = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                    }
                                                    objMisHouseholdInfo.PerDistrictCD = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                    objMisHouseholdInfo.PerVdcMunCD = objNHRSHouseOwnerMst.VdcMunCd.ConvertToString();
                                                    objMisHouseholdInfo.PerWardNO = objNHRSHouseOwnerMst.WardNo;
                                                    objMisHouseholdInfo.PerAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                    objMisHouseholdInfo.CurDistrictCD = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                    objMisHouseholdInfo.CurVdcMunCD = objNHRSHouseOwnerMst.VdcMunCd.ConvertToString();
                                                    objMisHouseholdInfo.CurWardNO = objNHRSHouseOwnerMst.WardNo;
                                                    objMisHouseholdInfo.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                    objMisHouseholdInfo.GrievanceSN = family.ContainsKey("grv_sn") ? Utils.ConvertToString(family.First(s => s.Key == "grv_sn").Value) : null;
                                                    //objMisHouseholdInfo.RespondedGenderCD = family.ContainsKey("gender") ? Utils.ConvertToString(family.First(s => s.Key == "gender").Value) : null;
                                                    objMisHouseholdInfo.FirstNameEng = objMisHouseholdInfo.FirstNameLoc = family.ContainsKey("name_fst") ? Utils.ConvertToString(family.First(s => s.Key == "name_fst").Value) : null;
                                                    objMisHouseholdInfo.MiddleNameEng = objMisHouseholdInfo.MiddleNameLoc = family.ContainsKey("name_mid") ? Utils.ConvertToString(family.First(s => s.Key == "name_mid").Value) : null;
                                                    objMisHouseholdInfo.LastNameEng = objMisHouseholdInfo.LastNameLoc = family.ContainsKey("name_lst") ? Utils.ConvertToString(family.First(s => s.Key == "name_lst").Value) : null;
                                                    objMisHouseholdInfo.FullNameEng = objMisHouseholdInfo.FullNameLoc = objMisHouseholdInfo.FirstNameEng.ConvertToString() + (objMisHouseholdInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisHouseholdInfo.MiddleNameEng) + " ") + objMisHouseholdInfo.LastNameEng.ConvertToString();
                                                    objMisHouseholdInfo.ShelterBeforeQuakeCD = family.ContainsKey("respreq") ? Utils.ConvertToString(family.First(s => s.Key == "respreq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "respreq").Value)) : null;
                                                    objMisHouseholdInfo.ShelterSinceQuakeCD = family.ContainsKey("shelter") ? Utils.ConvertToString(family.First(s => s.Key == "shelter").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "shelter").Value)) : null;
                                                    objMisHouseholdInfo.ShelterBeforeEQDistrict = family.ContainsKey("respreqd") ? Utils.ConvertToString(family.First(s => s.Key == "respreqd").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "respreqd").Value)) : null;
                                                    objMisHouseholdInfo.CurrentShelterCD = family.ContainsKey("resposq") ? Utils.ConvertToString(family.First(s => s.Key == "resposq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "resposq").Value)) : null;
                                                    objMisHouseholdInfo.CurrentShelterEQDistrict = family.ContainsKey("resposqd") ? Utils.ConvertToString(family.First(s => s.Key == "resposqd").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "resposqd").Value)) : null;
                                                    objMisHouseholdInfo.EQVictimIdentityCardCD = family.ContainsKey("eqid_typ") ? Utils.ConvertToString(family.First(s => s.Key == "eqid_typ").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "eqid_typ").Value)) : null;
                                                    objMisHouseholdInfo.respondentISHHHead = family.ContainsKey("resn_for_griev") ? Utils.ConvertToString(family.First(s => s.Key == "resn_for_griev").Value) : null;
                                                    objMisHouseholdInfo.EQVictimIdentityCard = "Y";
                                                    objMisHouseholdInfo.ReasonForGrievance = family.ContainsKey("resn_for_griev") ? Utils.ConvertToString(family.First(s => s.Key == "resn_for_griev").Value) : null;
                                                    objMisHouseholdInfo.TelNO = family.ContainsKey("tel") ? Utils.ConvertToString(family.First(s => s.Key == "tel").Value) : null;
                                                    objMisHouseholdInfo.MobileNO = family.ContainsKey("tel") ? Utils.ConvertToString(family.First(s => s.Key == "tel").Value) : null;
                                                    objMisHouseholdInfo.LoiSN = family.ContainsKey("loi_sn") ? Utils.ConvertToString(family.First(s => s.Key == "loi_sn").Value) : null;
                                                    objMisHouseholdInfo.EQVictimIdentityCardNO = family.ContainsKey("edi_num") ? Utils.ConvertToString(family.First(s => s.Key == "edi_num").Value) : null;
                                                    objMisHouseholdInfo.EQVictimIDCardFront = family.ContainsKey("eid_ph_f") ? Utils.ConvertToString(family.First(s => s.Key == "eid_ph_f").Value) : null;
                                                    objMisHouseholdInfo.EQVictimIDCardBack = family.ContainsKey("eid_ph_b") ? Utils.ConvertToString(family.First(s => s.Key == "eid_ph_b").Value) : null;

                                                    objMisHouseholdInfo.MonthlyIncomeCD = family.ContainsKey("income") ? Utils.ConvertToString(family.First(s => s.Key == "income").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "income").Value)) : null;

                                                    objMisHouseholdInfo.StudentSchoolLeft = family.ContainsKey("edrop_tf") ? (Utils.ConvertToString(family.First(s => s.Key == "edrop_tf").Value) == "1" ? "Y" : (Utils.ConvertToString(family.First(s => s.Key == "edrop_tf").Value) == "2" ? "N" : "N")) : null;
                                                    objMisHouseholdInfo.StudentSchoolLeftCnt = family.ContainsKey("edrop_cn") ? Utils.ConvertToString(family.First(s => s.Key == "edrop_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "edrop_cn").Value)) : null;
                                                    objMisHouseholdInfo.PregnantRegularCheckup = family.ContainsKey("pdrop_tf") ? (Utils.ConvertToString(family.First(s => s.Key == "pdrop_tf").Value) == "1" ? "Y" : (Utils.ConvertToString(family.First(s => s.Key == "pdrop_tf").Value) == "2" ? "N" : "N")) : null;
                                                    objMisHouseholdInfo.PregnantCheckupCnt = family.ContainsKey("pdrop_cn") ? Utils.ConvertToString(family.First(s => s.Key == "pdrop_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "pdrop_cn").Value)) : null;
                                                    objMisHouseholdInfo.ChildLeftVaccination = family.ContainsKey("vdrop_tf") ? (Utils.ConvertToString(family.First(s => s.Key == "vdrop_tf").Value) == "1" ? "Y" : (Utils.ConvertToString(family.First(s => s.Key == "vdrop_tf").Value) == "2" ? "N" : "N")) : null;
                                                    objMisHouseholdInfo.ChildLeftVaccinationCount = family.ContainsKey("vdrop_cn") ? Utils.ConvertToString(family.First(s => s.Key == "vdrop_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "vdrop_cn").Value)) : null;
                                                    objMisHouseholdInfo.LeftChangeOccupancy = family.ContainsKey("oc_ch_tf") ? (Utils.ConvertToString(family.First(s => s.Key == "oc_ch_tf").Value) == "1" ? "Y" : (Utils.ConvertToString(family.First(s => s.Key == "oc_ch_tf").Value) == "2" ? "N" : "N")) : null;
                                                    objMisHouseholdInfo.LeftChangeOccupancyCount = family.ContainsKey("oc_ch_cn") ? Utils.ConvertToString(family.First(s => s.Key == "oc_ch_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "oc_ch_cn").Value)) : null;
                                                    objMisHouseholdInfo.respondentISHHHead = family.ContainsKey("f_hh_res") ? ((Utils.ConvertToString(family.First(s => s.Key == "f_hh_res").Value) == "1" || Utils.ConvertToString(family.First(s => s.Key == "f_hh_res").Value) == "") ? "Y" : "N") : "N";
                                                    if (objMisHouseholdInfo.respondentISHHHead.ConvertToString() == "Y")
                                                    {
                                                        objMisHouseholdInfo.RespondentFirstName = objMisHouseholdInfo.RespondentFirstNameLoc = objMisHouseholdInfo.FirstNameEng;
                                                        objMisHouseholdInfo.RespondentMiddleName = objMisHouseholdInfo.RespondentMiddleNameLoc = objMisHouseholdInfo.MiddleNameEng;
                                                        objMisHouseholdInfo.RespondentLastName = objMisHouseholdInfo.RespondentLastNameLoc = objMisHouseholdInfo.LastNameEng;
                                                        objMisHouseholdInfo.RespondedGenderCD = family.ContainsKey("gender") ? Utils.ConvertToString(family.First(s => s.Key == "gender").Value) : null;
                                                        objMisHouseholdInfo.RespondentFullName = objMisHouseholdInfo.RespondentFullNameLoc = objMisHouseholdInfo.RespondentFirstName.ConvertToString() + (objMisHouseholdInfo.RespondentMiddleName.ConvertToString() == "" ? " " : (" " + objMisHouseholdInfo.RespondentMiddleName) + " ") + objMisHouseholdInfo.RespondentLastName.ConvertToString();
                                                        objMisHouseholdInfo.HhRelationTypeCD = 1;
                                                    }
                                                    else
                                                    {
                                                        objMisHouseholdInfo.RespondentFirstName = objMisHouseholdInfo.RespondentFirstNameLoc = family.ContainsKey("res_fn") ? Utils.ConvertToString(family.First(s => s.Key == "res_fn").Value) : null;
                                                        objMisHouseholdInfo.RespondentMiddleName = objMisHouseholdInfo.RespondentMiddleNameLoc = family.ContainsKey("res_mn") ? Utils.ConvertToString(family.First(s => s.Key == "res_mn").Value) : null;
                                                        objMisHouseholdInfo.RespondentLastName = objMisHouseholdInfo.RespondentLastNameLoc = family.ContainsKey("res_ln") ? Utils.ConvertToString(family.First(s => s.Key == "res_ln").Value) : null;
                                                        objMisHouseholdInfo.RespondedGenderCD = family.ContainsKey("gender") ? Utils.ConvertToString(family.First(s => s.Key == "gender").Value) : null;
                                                        objMisHouseholdInfo.HhRelationTypeCD = family.ContainsKey("rel_hres") ? Utils.ConvertToString(family.First(s => s.Key == "rel_hres").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "rel_hres").Value)) : null;
                                                        objMisHouseholdInfo.RespondentFullName = objMisHouseholdInfo.RespondentFullNameLoc = objMisHouseholdInfo.RespondentFirstName.ConvertToString() + (objMisHouseholdInfo.RespondentMiddleName.ConvertToString() == "" ? " " : (" " + objMisHouseholdInfo.RespondentMiddleName) + " ") + objMisHouseholdInfo.RespondentLastName.ConvertToString();
                                                        objMisHouseholdInfo.RespondentPhoto = family.ContainsKey("photo_hh") ? Utils.ConvertToString(family.First(s => s.Key == "photo_hh").Value) : null;
                                                    }

                                                    
                                                    objMisHouseholdInfo.MemberCount = family.ContainsKey("membr_cn") ? Utils.ConvertToString(family.First(s => s.Key == "membr_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "membr_cn").Value)) : 0;
                                                    objMisHouseholdInfo.DeathInYear = family.ContainsKey("death") ? (Utils.ConvertToString(family.First(s => s.Key == "death").Value) == "1" ? "Y" : "N") : "N";
                                                    objMisHouseholdInfo.DeathCount = family.ContainsKey("death_cn") ? Utils.ConvertToString(family.First(s => s.Key == "death_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "death_cn").Value)) : 0;
                                                    objMisHouseholdInfo.HumanDestroyFlag = family.ContainsKey("loss") ? (Utils.ConvertToString(family.First(s => s.Key == "loss").Value) == "1" ? "Y" : "N") : "N";
                                                    objMisHouseholdInfo.HumanDestroyCnt = family.ContainsKey("loss_cn") ? Utils.ConvertToString(family.First(s => s.Key == "loss_cn").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "loss_cn").Value)) : 0;
                                                    objMisHouseholdInfo.HouseholdActive = "Y";
                                                    objMisHouseholdInfo.ChildInSchool = "N";
                                                    objMisHouseholdInfo.SocialAllowance = "N";
                                                    objMisHouseholdInfo.Approved = "Y";
                                                    objMisHouseholdInfo.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                    objMisHouseholdInfo.ApprovedBY = SessionCheck.getSessionUsername();
                                                    objMisHouseholdInfo.EnteredBY = SessionCheck.getSessionUsername();
                                                    objMisHouseholdInfo.EnteredDT = DateTime.Now.ConvertToString();
                                                    objMisHouseholdInfo.IPAddress = CommonVariables.IPAddress;
                                                    objMisHouseholdInfo.BatchID = batchID.ConvertToString();
                                                    //objMisHouseholdInfo.HoDefinedCd = objNHRSHouseOwnerMst.DefinedCd;
                                                    //  objMisHouseholdInfo.InstanceUniqueSno = objMisHouseholdInfo.InstanceUniqueSno; //bjNHRSHouseOwnerMst.InstanceUniqueSno;
                                                    //Facilities Table Here
                                                    //Insert Household Detail Here
                                                    objMisHouseholdInfo.Mode = mode;
                                                    service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                    qr = service.SubmitChanges(objMisHouseholdInfo, true);
                                                    //get HouseholdID
                                                    HouseholdID = qr["V_HOUSEHOLD_ID"].ConvertToString();
                                                    #endregion
                                                    if (!string.IsNullOrEmpty(HouseholdID))
                                                    {
                                                        #region Water Source Detail
                                                        decimal? waterSourceBeforeCd = family.ContainsKey("h2o_pre") ? Utils.ConvertToString(family.First(s => s.Key == "h2o_pre").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "h2o_pre").Value)) : null;
                                                        decimal? waterSourceAfterCd = family.ContainsKey("h2o_pos") ? Utils.ConvertToString(family.First(s => s.Key == "h2o_pos").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "h2o_pos").Value)) : null;
                                                        if (waterSourceBeforeCd.ConvertToString() != "" && waterSourceAfterCd.ConvertToString() != "")
                                                        {
                                                            objWaterSource = new RES_NHRS_HH_WATERSOURCE_DTL();
                                                            objWaterSource.HouseholdID = HouseholdID;
                                                            objWaterSource.HouseOwnerID = HouseOwnerIDInserted;
                                                            objWaterSource.Approved = "Y";
                                                            objWaterSource.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                            objWaterSource.ApprovedBY = SessionCheck.getSessionUsername();
                                                            objWaterSource.EnteredBY = SessionCheck.getSessionUsername();
                                                            objWaterSource.EnteredDT = DateTime.Now.ConvertToString();
                                                            objWaterSource.BatchID = batchID;
                                                            objWaterSource.Mode = mode;
                                                            service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                            if (waterSourceBeforeCd == waterSourceAfterCd)
                                                            {
                                                                objWaterSource.WaterSourceCD = waterSourceBeforeCd;
                                                                objWaterSource.WaterSourceBefore = objWaterSource.WaterSourceAfter = "Y";
                                                                qr = service.SubmitChanges(objWaterSource, true);
                                                            }
                                                            else
                                                            {
                                                                objWaterSource.WaterSourceCD = waterSourceBeforeCd;
                                                                objWaterSource.WaterSourceAfter = "N";
                                                                objWaterSource.WaterSourceBefore = "Y";
                                                                qr = service.SubmitChanges(objWaterSource, true);
                                                                if (qr.IsSuccess)
                                                                {
                                                                    objWaterSource.WaterSourceCD = waterSourceAfterCd;
                                                                    objWaterSource.WaterSourceAfter = "Y";
                                                                    objWaterSource.WaterSourceBefore = "N";
                                                                    qr = service.SubmitChanges(objWaterSource, true);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        #region Fuel Source Detail
                                                        decimal? fuelSourceBeforeCd = family.ContainsKey("fir_pre") ? Utils.ConvertToString(family.First(s => s.Key == "fir_pre").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "fir_pre").Value)) : null;
                                                        decimal? fuelSourceAfterCd = family.ContainsKey("fir_pos") ? Utils.ConvertToString(family.First(s => s.Key == "fir_pos").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "fir_pos").Value)) : null;
                                                        if (fuelSourceBeforeCd.ConvertToString() != "" && fuelSourceAfterCd.ConvertToString() != "")
                                                        {
                                                            objFuelSource = new RES_NHRS_HH_FUELSOURCE_DTL();
                                                            objFuelSource.HouseholdID = HouseholdID;
                                                            objFuelSource.HouseOwnerID = HouseOwnerIDInserted;
                                                            objFuelSource.Approved = "Y";
                                                            objFuelSource.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                            objFuelSource.ApprovedBY = SessionCheck.getSessionUsername();
                                                            objFuelSource.EnteredBY = SessionCheck.getSessionUsername();
                                                            objFuelSource.EnteredDT = DateTime.Now.ConvertToString();
                                                            objFuelSource.BatchID = batchID;
                                                            objFuelSource.Mode = mode;
                                                            service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                            if (fuelSourceBeforeCd == fuelSourceAfterCd)
                                                            {
                                                                objFuelSource.FuelSourceCD = fuelSourceBeforeCd;
                                                                objFuelSource.FuelSourceBefore = objFuelSource.FuelSourceAfter = "Y";
                                                                qr = service.SubmitChanges(objFuelSource, true);
                                                            }
                                                            else
                                                            {
                                                                objFuelSource.FuelSourceCD = fuelSourceBeforeCd;
                                                                objFuelSource.FuelSourceAfter = "N";
                                                                objFuelSource.FuelSourceBefore = "Y";
                                                                qr = service.SubmitChanges(objFuelSource, true);
                                                                if (qr.IsSuccess)
                                                                {
                                                                    objFuelSource.FuelSourceCD = fuelSourceAfterCd;
                                                                    objFuelSource.FuelSourceAfter = "Y";
                                                                    objFuelSource.FuelSourceBefore = "N";
                                                                    qr = service.SubmitChanges(objFuelSource, true);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        #region Light Source Detail
                                                        decimal? LightSourceBeforeCd = family.ContainsKey("lit_pre") ? Utils.ConvertToString(family.First(s => s.Key == "lit_pre").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "lit_pre").Value)) : null;
                                                        decimal? LightSourceAfterCd = family.ContainsKey("lit_pos") ? Utils.ConvertToString(family.First(s => s.Key == "lit_pos").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "lit_pos").Value)) : null;
                                                        if (LightSourceBeforeCd.ConvertToString() != "" && LightSourceAfterCd.ConvertToString() != "")
                                                        {
                                                            objLightSource = new RES_NHRS_HH_LIGHTSOURCE_DTL();
                                                            objLightSource.HouseholdID = HouseholdID;
                                                            objLightSource.HouseOwnerID = HouseOwnerIDInserted;
                                                            objLightSource.Approved = "Y";
                                                            objLightSource.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                            objLightSource.ApprovedBY = SessionCheck.getSessionUsername();
                                                            objLightSource.EnteredBY = SessionCheck.getSessionUsername();
                                                            objLightSource.EnteredDT = DateTime.Now.ConvertToString();
                                                            objLightSource.BatchID = batchID;
                                                            objLightSource.Mode = mode;
                                                            service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                            if (LightSourceBeforeCd == LightSourceAfterCd)
                                                            {
                                                                objLightSource.LightSourceCD = LightSourceBeforeCd;
                                                                objLightSource.LightSourceBefore = objLightSource.LightSourceAfter = "Y";
                                                                qr = service.SubmitChanges(objLightSource, true);
                                                            }
                                                            else
                                                            {
                                                                objLightSource.LightSourceCD = LightSourceBeforeCd;
                                                                objLightSource.LightSourceAfter = "N";
                                                                objLightSource.LightSourceBefore = "Y";
                                                                qr = service.SubmitChanges(objLightSource, true);
                                                                if (qr.IsSuccess)
                                                                {
                                                                    objLightSource.LightSourceCD = LightSourceAfterCd;
                                                                    objLightSource.LightSourceAfter = "Y";
                                                                    objLightSource.LightSourceBefore = "N";
                                                                    qr = service.SubmitChanges(objLightSource, true);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        #region Toilet Type Detail
                                                        decimal? ToiletTypeBeforeCd = family.ContainsKey("excr_pre") ? Utils.ConvertToString(family.First(s => s.Key == "excr_pre").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "excr_pre").Value)) : null;
                                                        decimal? ToiletTypeAfterCd = family.ContainsKey("excr_pos") ? Utils.ConvertToString(family.First(s => s.Key == "excr_pos").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "excr_pos").Value)) : null;
                                                        if (ToiletTypeBeforeCd.ConvertToString() != "" && ToiletTypeAfterCd.ConvertToString() != "")
                                                        {
                                                            objToiletType = new RES_NHRS_HH_TOILET_TYPE();
                                                            objToiletType.HouseholdID = HouseholdID;
                                                            objToiletType.HouseOwnerID = HouseOwnerIDInserted;
                                                            objToiletType.Approved = "Y";
                                                            objToiletType.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                            objToiletType.ApprovedBY = SessionCheck.getSessionUsername();
                                                            objToiletType.EnteredBY = SessionCheck.getSessionUsername();
                                                            objToiletType.EnteredDT = DateTime.Now.ConvertToString();
                                                            objToiletType.BatchID = batchID;
                                                            objToiletType.Mode = mode;
                                                            service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                            if (ToiletTypeBeforeCd == ToiletTypeAfterCd)
                                                            {
                                                                objToiletType.ToiletTypeCD = ToiletTypeBeforeCd;
                                                                objToiletType.ToiletTypeBefore = objToiletType.ToiletTypeAfter = "Y";
                                                                qr = service.SubmitChanges(objToiletType, true);
                                                            }
                                                            else
                                                            {
                                                                objToiletType.ToiletTypeCD = ToiletTypeBeforeCd;
                                                                objToiletType.ToiletTypeAfter = "N";
                                                                objToiletType.ToiletTypeBefore = "Y";
                                                                qr = service.SubmitChanges(objToiletType, true);
                                                                if (qr.IsSuccess)
                                                                {
                                                                    objToiletType.ToiletTypeCD = ToiletTypeAfterCd;
                                                                    objToiletType.ToiletTypeAfter = "Y";
                                                                    objToiletType.ToiletTypeBefore = "N";
                                                                    qr = service.SubmitChanges(objToiletType, true);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        #region Facilities Detail
                                                        string facilitiesBeforeCd = family.ContainsKey("ast_pre") ? Utils.ConvertToString(family.First(s => s.Key == "ast_pre").Value) : null;
                                                        string facilitiesAfterCd = family.ContainsKey("ast_pos") ? Utils.ConvertToString(family.First(s => s.Key == "ast_pos").Value) : null;
                                                        if (facilitiesBeforeCd.ConvertToString() != "" && facilitiesAfterCd.ConvertToString() != "")
                                                        {
                                                            objIndicatorDtl = new RES_NHRS_HH_INDICATOR_DTL();
                                                            objIndicatorDtl.HouseholdID = HouseholdID;
                                                            objIndicatorDtl.HouseOwnerID = HouseOwnerIDInserted;
                                                            objIndicatorDtl.Approved = "Y";
                                                            objIndicatorDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                            objIndicatorDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                                            objIndicatorDtl.EnteredBY = SessionCheck.getSessionUsername();
                                                            objIndicatorDtl.EnteredDT = DateTime.Now.ConvertToString();
                                                            objIndicatorDtl.BatchID = batchID;
                                                            objIndicatorDtl.Mode = mode;
                                                            service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                            List<string> arrFacilitiesBefore = facilitiesBeforeCd.Split(' ').ToList();
                                                            List<string> arrFacilitiesAfter = facilitiesAfterCd.Split(' ').ToList();
                                                            List<string> arrCommon = arrFacilitiesBefore.Intersect(arrFacilitiesAfter).ToList();
                                                            List<string> arrUncommonBefore = arrFacilitiesBefore.Except(arrCommon).ToList();
                                                            List<string> arrUncommonAfter = arrFacilitiesAfter.Except(arrCommon).ToList();

                                                            foreach (var item in arrCommon)
                                                            {
                                                                if (!string.IsNullOrEmpty(item))
                                                                {
                                                                    objIndicatorDtl.IndicatorCD = Convert.ToDecimal(item);
                                                                    objIndicatorDtl.IndicatorBefore = objIndicatorDtl.IndicatorAfter = "Y";
                                                                    qr = service.SubmitChanges(objIndicatorDtl, true);
                                                                }

                                                            }
                                                            foreach (var item in arrUncommonBefore)
                                                            {
                                                                if (!string.IsNullOrEmpty(item))
                                                                {
                                                                    objIndicatorDtl.IndicatorCD = Convert.ToDecimal(item);
                                                                    objIndicatorDtl.IndicatorBefore = "Y";
                                                                    objIndicatorDtl.IndicatorAfter = "N";
                                                                    qr = service.SubmitChanges(objIndicatorDtl, true);
                                                                }
                                                            }
                                                            foreach (var item in arrUncommonAfter)
                                                            {
                                                                if (!string.IsNullOrEmpty(item))
                                                                {
                                                                    objIndicatorDtl.IndicatorCD = Convert.ToDecimal(item);
                                                                    objIndicatorDtl.IndicatorBefore = "N";
                                                                    objIndicatorDtl.IndicatorAfter = "Y";
                                                                    qr = service.SubmitChanges(objIndicatorDtl, true);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        #region Earthquake Relief Money
                                                        string eqReliefMoney = String.Empty;
                                                        eqReliefMoney = family.ContainsKey("rahat") ? Utils.ConvertToString(family.First(s => s.Key == "rahat").Value) : null;
                                                        if (eqReliefMoney.ConvertToString() != "")
                                                        {
                                                            dynamic arrEqReliefMoney = eqReliefMoney.Split(' ');
                                                            foreach (var item in arrEqReliefMoney)
                                                            {
                                                                if (!string.IsNullOrEmpty(item))
                                                                {
                                                                    objMigNhrsRcvdRelief = new RES_NHRS_RCVD_EQ_RELIEF_MONEY();
                                                                    objMigNhrsRcvdRelief.HouseholdID = HouseholdID;
                                                                    objMigNhrsRcvdRelief.HouseOwnerID = HouseOwnerIDInserted;
                                                                    objMigNhrsRcvdRelief.EQReliefMoneyCD = Convert.ToDecimal(item);
                                                                    objMigNhrsRcvdRelief.Approved = "Y";
                                                                    objMigNhrsRcvdRelief.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                    objMigNhrsRcvdRelief.ApprovedBY = SessionCheck.getSessionUsername();
                                                                    objMigNhrsRcvdRelief.EnteredBY = SessionCheck.getSessionUsername();
                                                                    objMigNhrsRcvdRelief.EnteredDT = DateTime.Now.ConvertToString();
                                                                    objMigNhrsRcvdRelief.BatchID = batchID;
                                                                    objMigNhrsRcvdRelief.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                                    qr = service.SubmitChanges(objMigNhrsRcvdRelief, true);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        if (family.ContainsKey("Members"))
                                                        {
                                                            #region Repeat Members
                                                            int i = 0;
                                                            foreach (SortedDictionary<dynamic, dynamic> member in family.First(s => s.Key == "Members").Value)
                                                            {
                                                                if (i >= 0)
                                                                {
                                                                    memberCnt++;
                                                                    #region Household Head Data
                                                                    objMisMemberInfo = new RES_MIS_MEMBER();
                                                                    objMisMemberInfo.HouseOwnerID = HouseOwnerIDInserted;
                                                                    objMisMemberInfo.HouseholdID = HouseholdID;
                                                                    objMisMemberInfo.HouseholdDefinedCD = objMisHouseholdInfo.HouseHoldDefinedCode;
                                                                    objMisMemberInfo.ReasonForGrievance = member.ContainsKey("resn_for_griev") ? Utils.ConvertToString(member.First(s => s.Key == "resn_for_griev").Value) : null;
                                                                    objMisMemberInfo.buildingStructureNo = building.ContainsKey("bs_sn") ? Utils.ConvertToString(building.First(s => s.Key == "bs_sn").Value) : null;
                                                                    objMisMemberInfo.GrievanceSN = member.ContainsKey("grv_sn") ? Utils.ConvertToString(member.First(s => s.Key == "grv_sn").Value) : null;
                                                                    objMisMemberInfo.LoiSN = member.ContainsKey("loi_sn") ? Utils.ConvertToString(member.First(s => s.Key == "loi_sn").Value) : null;
                                                                    objMisMemberInfo.DefinedCd = member.ContainsKey("mem_sn") ? Utils.ConvertToString(member.First(s => s.Key == "mem_sn").Value) : null;
                                                                    if (objMisMemberInfo.DefinedCd.ConvertToString() != "")
                                                                    {
                                                                        objMisMemberInfo.FirstNameEng = objMisMemberInfo.FirstNameLoc = member.ContainsKey("name_fst") ? Utils.ConvertToString(member.First(s => s.Key == "name_fst").Value) : null;
                                                                        objMisMemberInfo.MiddleNameEng = objMisMemberInfo.MiddleNameLoc = member.ContainsKey("name_mid") ? Utils.ConvertToString(member.First(s => s.Key == "name_mid").Value) : null;
                                                                        objMisMemberInfo.LastNameEng = objMisMemberInfo.LastNameLoc = member.ContainsKey("name_lst") ? Utils.ConvertToString(member.First(s => s.Key == "name_lst").Value) : null;
                                                                        objMisMemberInfo.FullNameEng = objMisMemberInfo.FullNameLoc = objMisMemberInfo.FirstNameEng.ConvertToString() + (objMisMemberInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisMemberInfo.MiddleNameEng) + " ") + objMisMemberInfo.LastNameEng.ConvertToString();
                                                                        objMisMemberInfo.GenderCD = member.ContainsKey("gender") ? Utils.ConvertToString(member.First(s => s.Key == "gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "gender").Value)) : null;
                                                                        //objMisMemberInfo.BirthDt = family.ContainsKey("today") ? Utils.ConvertToString(family.First(s => s.Key == "today").Value) == "" ? null : Convert.ToDateTime(Utils.ConvertToString(family.First(s => s.Key == "today").Value)) : null;
                                                                        objMisMemberInfo.BirthYearLoc = member.ContainsKey("dob_yr") ? Utils.ConvertToString(member.First(s => s.Key == "dob_yr").Value) : "9999";
                                                                        objMisMemberInfo.BirthMonthLoc = member.ContainsKey("dob_mnth") ? Utils.ConvertToString(member.First(s => s.Key == "dob_mnth").Value) : "99";
                                                                        objMisMemberInfo.BirthDayLoc = member.ContainsKey("dob_date") ? Utils.ConvertToString(member.First(s => s.Key == "dob_date").Value) : "99";
                                                                        objMisMemberInfo.BirthYear = 9999;
                                                                        objMisMemberInfo.BirthMonth = 99;
                                                                        objMisMemberInfo.BirthDay = 99;
                                                                        objMisMemberInfo.BirthDTLoc = objMisMemberInfo.BirthYearLoc + "-" + objMisMemberInfo.BirthMonthLoc + "-" + objMisMemberInfo.BirthDayLoc;
                                                                        //birthLoc
                                                                        objMisMemberInfo.Age = member.ContainsKey("age") ? Utils.ConvertToString(member.First(s => s.Key == "age").Value) : null;
                                                                        //if (objMisMemberInfo.Age.ConvertToString() == "")
                                                                        //{
                                                                        //    objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_hh_head").Value)) : null;
                                                                        //}
                                                                        objMisMemberInfo.IdentificationTypeCD = family.ContainsKey("id_type") ? Utils.ConvertToString(family.First(s => s.Key == "id_type").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "id_type").Value)) : null;
                                                                        objMisMemberInfo.MemberActive = "Y";
                                                                        objMisMemberInfo.Disability = member.ContainsKey("disablty") ? Utils.ConvertToString(member.First(s => s.Key == "disablty").Value) : null;
                                                                        objMisMemberInfo.CtzIssue = (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "" && objMisMemberInfo.IdentificationTypeCD.ConvertToString() == "1") ? "Y" : "N";
                                                                        //Driving Licence doesnot have corresponding flag
                                                                        objMisMemberInfo.VoterID = (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "" && objMisMemberInfo.IdentificationTypeCD.ConvertToString() == "3") ? "Y" : "N";
                                                                        objMisMemberInfo.SocialAllowance = (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "" && objMisMemberInfo.IdentificationTypeCD.ConvertToString() == "4") ? "Y" : "N";
                                                                        if (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "")
                                                                        {
                                                                            switch (objMisMemberInfo.IdentificationTypeCD.ConvertToString())
                                                                            {
                                                                                case "1":
                                                                                    objMisMemberInfo.CitNO = family.ContainsKey("id_num") ? Utils.ConvertToString(family.First(s => s.Key == "id_num").Value) : null;
                                                                                    objMisMemberInfo.CtzIssueDistrictCD = family.ContainsKey("issue_dist") ? Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value)) : null;
                                                                                    break;
                                                                                case "2":
                                                                                    objMisMemberInfo.DrivingLicenseNO = family.ContainsKey("id_num") ? Utils.ConvertToString(family.First(s => s.Key == "id_num").Value) : null;
                                                                                    objMisMemberInfo.DrivingLicissDistrict = family.ContainsKey("issue_dist") ? Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value)) : null;
                                                                                    break;
                                                                                case "3":
                                                                                    objMisMemberInfo.VoterID = family.ContainsKey("id_num") ? Utils.ConvertToString(family.First(s => s.Key == "id_num").Value) : null;
                                                                                    objMisMemberInfo.VoterIDDistrictCD = family.ContainsKey("issue_dist") ? Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value)) : null;
                                                                                    break;
                                                                                case "4":
                                                                                    objMisMemberInfo.SocialAllowanceID = family.ContainsKey("id_num") ? Utils.ConvertToString(family.First(s => s.Key == "id_num").Value) : null;
                                                                                    objMisMemberInfo.SocialAllIssDistrict = family.ContainsKey("issue_dist") ? Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "issue_dist").Value)) : null;
                                                                                    break;
                                                                                case "6":
                                                                                    objMisMemberInfo.IdentificationOthers = "";
                                                                                    objMisMemberInfo.IdentificationOthersLoc = "";
                                                                                    break;
                                                                                default:
                                                                                    break;
                                                                            }
                                                                            objMisMemberInfo.MemberPhotoID = family.ContainsKey("photo_hh") ? Utils.ConvertToString(family.First(s => s.Key == "photo_hh").Value) : null;
                                                                        }
                                                                        //hh_head_photo
                                                                        objMisMemberInfo.CasteCD = family.ContainsKey("cast") ? Utils.ConvertToString(family.First(s => s.Key == "cast").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "cast").Value)) : null;
                                                                        objMisMemberInfo.EducationCD = member.ContainsKey("edu_levl") ? Utils.ConvertToString(member.First(s => s.Key == "edu_levl").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "edu_levl").Value)) : null;
                                                                        objMisMemberInfo.BankAccountFlag = family.ContainsKey("bank_acc") ? (Utils.ConvertToString(family.First(s => s.Key == "bank_acc").Value) == "1" ? "Y" : "N") : "N";
                                                                        //BANK CODE NOT IN DECIMAL SO COMMENTED TILL CORRECTION
                                                                        //objMisMemberInfo.BankCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_info/hh_head_bankname") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_info/hh_head_bankname").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_info/hh_head_bankname").Value)) : null;

                                                                        //Bank detail not in database

                                                                        objMisMemberInfo.TelNO = objMisMemberInfo.MobileNO = family.ContainsKey("tel") ? Utils.ConvertToString(family.First(s => s.Key == "tel").Value) == "" ? null : Convert.ToDecimal(Utils.ConvertToString(family.First(s => s.Key == "tel").Value)) : null;
                                                                        objMisMemberInfo.Death = "N";
                                                                        objMisMemberInfo.MaritalStatusCD = member.ContainsKey("marital") ? Utils.ConvertToString(member.First(s => s.Key == "marital").Value) == "" ? 99 : Convert.ToDecimal(Utils.ConvertToString(member.First(s => s.Key == "marital").Value)) : 99;

                                                                        objMisMemberInfo.Literate = member.ContainsKey("edu_levl") ? (Utils.ConvertToString(member.First(s => s.Key == "edu_levl").Value) != "" ? "Y" : "N") : "N";
                                                                        objMisMemberInfo.BirthCertificate = member.ContainsKey("brth_reg") ? (Utils.ConvertToString(member.First(s => s.Key == "brth_reg").Value) != "" ? "Y" : "N") : "N";

                                                                        //objMisMemberInfo.PidNo = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid").Value) : null;
                                                                        objMisMemberInfo.PRFhh = member.ContainsKey("pr_fhh") ? Utils.ConvertToString(member.First(s => s.Key == "pr_fhh").Value) : null;
                                                                        objMisMemberInfo.PRMhh = member.ContainsKey("pr_mhh") ? Utils.ConvertToString(member.First(s => s.Key == "pr_mhh").Value) : null;
                                                                        objMisMemberInfo.PRFM = member.ContainsKey("pr_fm") ? Utils.ConvertToString(member.First(s => s.Key == "pr_fm").Value) : null;
                                                                        objMisMemberInfo.PRMM = member.ContainsKey("pr_mm") ? Utils.ConvertToString(member.First(s => s.Key == "pr_mm").Value) : null;
                                                                        //if (objNHRSHouseOwnerMst.DistrictCd.ConvertToString() == "")
                                                                        //{
                                                                        //    string aa = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                                        //}

                                                                        objMisMemberInfo.PerDistrictCD = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                                        objMisMemberInfo.PerVdcMunCD = objNHRSHouseOwnerMst.VdcMunCd.ConvertToString();
                                                                        objMisMemberInfo.PerWardNO = objNHRSHouseOwnerMst.WardNo;
                                                                        objMisMemberInfo.PerAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                        objMisMemberInfo.CurDistrictCD = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                                        objMisMemberInfo.CurVdcMunCD = objNHRSHouseOwnerMst.VdcMunCd.ConvertToString();
                                                                        objMisMemberInfo.CurWardNO = objNHRSHouseOwnerMst.WardNo;
                                                                        objMisMemberInfo.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                      
                                                                        objMisMemberInfo.HouseholdHead = "N";
                                                                        objMisMemberInfo.HODefinedCD = objNHRSHouseOwnerMst.DefinedCd;
                                                                        objMisMemberInfo.InstanceUniqueSno = objMisHouseholdInfo.InstanceUniqueSNO;
                                                                        objMisMemberInfo.Approved = "Y";
                                                                        objMisMemberInfo.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                        objMisMemberInfo.ApprovedBY = SessionCheck.getSessionUsername();
                                                                        objMisMemberInfo.EnteredBY = SessionCheck.getSessionUsername();
                                                                        objMisMemberInfo.EnteredDT = DateTime.Now.ConvertToString();
                                                                        //SAVE HOUSEHOLD HEAD(MEMBER) DETAIL HERE
                                                                        //objMisMemberInfo.IPAddress = CommonVariables.IPAddress;
                                                                        objMisMemberInfo.BatchID = batchID;
                                                                        objMisMemberInfo.Mode = mode;
                                                                        service.PackageName = "MIGNHRS.PKG_RES_MEMBER";
                                                                        qr = service.SubmitChanges(objMisMemberInfo, true);
                                                                        MemberID = qr["V_MEMBER_ID"].ConvertToString();
                                                                        i++;
                                                                        //#region Household Head Photo
                                                                        //if (qr.IsSuccess)
                                                                        //{
                                                                        //    objMisMemberDoc = new MigMisMemberDocInfo();
                                                                        //    objMisMemberDoc.DocTypeCd = 11;
                                                                        //    objMisMemberDoc.MemberId = MemberID;
                                                                        //    objMisMemberDoc.DocId = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_photo") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_photo").Value) : null;
                                                                        //    objMisMemberDoc.Approved = "N";
                                                                        //    objMisMemberDoc.EnteredBy = SessionCheck.getSessionUsername();
                                                                        //    objMisMemberDoc.EnteredDt = DateTime.Now;
                                                                        //    objMisMemberDoc.IPAddress = CommonVariables.IPAddress;
                                                                        //    objMisMemberDoc.BatchId = batchID;
                                                                        //    objMisMemberDoc.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                        //    objMisMemberDoc.Mode = mode;
                                                                        //    service.PackageName = "MIGNHRS.PKG_MEMBER";
                                                                        //    qr = service.SubmitChanges(objMisMemberDoc, true);
                                                                        //}
                                                                        //#endregion
                                                                        #region Allowance Detail
                                                                        if (qr.IsSuccess)
                                                                        {
                                                                            string allowances = String.Empty;
                                                                            allowances = member.ContainsKey("security") ? Utils.ConvertToString(member.First(s => s.Key == "security").Value) : null;
                                                                            if (allowances.ConvertToString() != "")
                                                                            {
                                                                                dynamic arrAllowances = allowances.Split(' ');
                                                                                foreach (var item in arrAllowances)
                                                                                {
                                                                                    if (!string.IsNullOrEmpty(item))
                                                                                    {
                                                                                        objMisHHAllowanceDtl = new RES_MIS_HH_ALLOWANCE_DTL();
                                                                                        objMisHHAllowanceDtl.HouseOwnerID = HouseOwnerIDInserted;
                                                                                        objMisHHAllowanceDtl.buildingStructureNo = objNHRSBuildingAssMst.BSSN;
                                                                                        objMisHHAllowanceDtl.HouseholdID = HouseholdID;
                                                                                        objMisHHAllowanceDtl.MemberID = MemberID;
                                                                                        objMisHHAllowanceDtl.AllowanceTypeCD = Convert.ToDecimal(item);
                                                                                        objMisHHAllowanceDtl.HandiColorCD = member.ContainsKey("disablty") ? Utils.ConvertToString(member.First(s => s.Key == "disablty").Value) : null;
                                                                                        objMisHHAllowanceDtl.AllowanceDay = member.ContainsKey("secu_date") ? Utils.ConvertToString(member.First(s => s.Key == "secu_date").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "secu_date").Value)) : 99;
                                                                                        objMisHHAllowanceDtl.AllowanceMonth = member.ContainsKey("secu_mnth") ? Utils.ConvertToString(member.First(s => s.Key == "secu_mnth").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "secu_mnth").Value)) : 99;
                                                                                        objMisHHAllowanceDtl.AllowanceYears = member.ContainsKey("secu_yr") ? Utils.ConvertToString(member.First(s => s.Key == "secu_yr").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "secu_yr").Value)) : 99;
                                                                                        objMisHHAllowanceDtl.AllowanceSTDTLoc = objMisHHAllowanceDtl.AllowanceYears + "-" + objMisHHAllowanceDtl.AllowanceMonth + "-" + objMisHHAllowanceDtl.AllowanceDay;
                                                                                        objMisHHAllowanceDtl.Approved = "Y";
                                                                                        objMisHHAllowanceDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                                        objMisHHAllowanceDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                                                                        objMisHHAllowanceDtl.EnteredBY = SessionCheck.getSessionUsername();
                                                                                        objMisHHAllowanceDtl.EnteredDT = DateTime.Now.ConvertToString();
                                                                                        objMisHHAllowanceDtl.IPAddress = CommonVariables.IPAddress;
                                                                                        objMisHHAllowanceDtl.BatchID = batchID;
                                                                                        objMisHHAllowanceDtl.Mode = mode;
                                                                                        service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                                                        qr = service.SubmitChanges(objMisHHAllowanceDtl, true);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        #endregion
                                                                    }
                                                                    #endregion
                                                                }
                                                                else
                                                                {
                                                                    #region Other Members
                                                                    objMisMemberInfo = new RES_MIS_MEMBER();
                                                                    objMisMemberInfo.HouseOwnerID = HouseOwnerIDInserted;
                                                                    objMisMemberInfo.HouseholdID = HouseholdID;
                                                                    objMisMemberInfo.DefinedCd = member.ContainsKey("mem_sn") ? Utils.ConvertToString(member.First(s => s.Key == "mem_sn").Value) : null;
                                                                    if (objMisMemberInfo.DefinedCd.ConvertToString() != "")
                                                                    {
                                                                        memberCnt++;
                                                                        objMisMemberInfo.FirstNameEng = objMisMemberInfo.FirstNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_first_name") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_first_name").Value) : null;
                                                                        objMisMemberInfo.LastNameEng = objMisMemberInfo.LastNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_last_name") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_last_name").Value) : null;
                                                                        objMisMemberInfo.MiddleNameEng = objMisMemberInfo.MiddleNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_middle_name") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_middle_name").Value) : null;
                                                                        objMisMemberInfo.FullNameEng = objMisMemberInfo.FullNameLoc = objMisMemberInfo.FirstNameEng.ConvertToString() + (objMisMemberInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisMemberInfo.MiddleNameEng) + " ") + objMisMemberInfo.LastNameEng.ConvertToString();
                                                                        objMisMemberInfo.GenderCD = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_gender") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_gender").Value)) : null;
                                                                        objMisMemberInfo.BirthYearLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_year") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_year").Value) : "9999";
                                                                        objMisMemberInfo.BirthMonthLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_month") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_month").Value) : "99";
                                                                        objMisMemberInfo.BirthDayLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_day") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_day").Value) : "99";
                                                                        objMisMemberInfo.BirthYear = 9999;
                                                                        objMisMemberInfo.BirthMonth = 99;
                                                                        objMisMemberInfo.BirthDay = 99;
                                                                        //birthLoc
                                                                        objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_age_calculated") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_age_calculated").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_age_calculated").Value)) : null;
                                                                        if (objMisMemberInfo.Age.ConvertToString() == "")
                                                                        {
                                                                            objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_age") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_age").Value)) : null;
                                                                        }
                                                                        objMisMemberInfo.MemberActive = "Y";
                                                                        objMisMemberInfo.Disability = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/member_disable") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/member_disable").Value) == "1" ? "Y" : "N") : "N";
                                                                        objMisMemberInfo.CtzIssue = (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "" && objMisMemberInfo.IdentificationTypeCD.ConvertToString() == "1") ? "Y" : "N";
                                                                        //Driving Licence doesnot have corresponding flag
                                                                        objMisMemberInfo.VoterID = (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "" && objMisMemberInfo.IdentificationTypeCD.ConvertToString() == "3") ? "Y" : "N";
                                                                        objMisMemberInfo.SocialAllowance = (objMisMemberInfo.IdentificationTypeCD.ConvertToString() != "" && objMisMemberInfo.IdentificationTypeCD.ConvertToString() == "4") ? "Y" : "N";
                                                                        objMisMemberInfo.ForeignHouseholdCountryEng = objMisMemberInfo.ForeignHouseholdCountryLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/country_if_foreign") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/country_if_foreign").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/country_if_foreign").Value)) : null;
                                                                        objMisMemberInfo.PidNO = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid").Value) : null;
                                                                        objMisMemberInfo.CasteCD = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value)) : null;
                                                                        objMisMemberInfo.EducationCD = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education").Value)) : null;
                                                                        objMisMemberInfo.BankAccountFlag = "N";
                                                                        objMisMemberInfo.Death = "N";
                                                                        objMisMemberInfo.MaritalStatusCD = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_marrital_status") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_marrital_status").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_marrital_status").Value)) : 99;
                                                                        objMisMemberInfo.Literate = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education").Value) != "" ? "Y" : "N") : "N";
                                                                        objMisMemberInfo.BirthCertificate = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/memeber_birth_registered") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/memeber_birth_registered").Value) != "" ? "Y" : "N") : "N";
                                                                        objMisMemberInfo.PerDistrictCD = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                                        objMisMemberInfo.PerVdcMunCD = objNHRSHouseOwnerMst.VdcMunCd.ConvertToString();
                                                                        objMisMemberInfo.PerWardNO = objNHRSHouseOwnerMst.WardNo;
                                                                        objMisMemberInfo.PerAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                        objMisMemberInfo.CurDistrictCD = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                                        objMisMemberInfo.CurVdcMunCD = objNHRSHouseOwnerMst.VdcMunCd.ConvertToString();
                                                                        objMisMemberInfo.CurWardNO = objNHRSHouseOwnerMst.WardNo;
                                                                        objMisMemberInfo.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                        objMisMemberInfo.HouseholdHead = "N";
                                                                        objMisMemberInfo.Approved = "Y";
                                                                        objMisMemberInfo.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                        objMisMemberInfo.ApprovedBY = SessionCheck.getSessionUsername();
                                                                        objMisMemberInfo.EnteredBY = SessionCheck.getSessionUsername();
                                                                        objMisMemberInfo.EnteredDT = DateTime.Now.ConvertToString();
                                                                        objMisMemberInfo.HODefinedCD = objNHRSHouseOwnerMst.DefinedCd;
                                                                        objMisMemberInfo.InstanceUniqueSno = objMisHouseholdInfo.InstanceUniqueSNO;
                                                                        //SAVE MEMBER DETAIL HERE
                                                                        //objMisMemberInfo.IPAddress = CommonVariables.IPAddress;
                                                                        objMisMemberInfo.BatchID = batchID;
                                                                        objMisMemberInfo.Mode = mode;
                                                                        service.PackageName = "MIGNHRS.PKG_RES_MEMBER";
                                                                        qr = service.SubmitChanges(objMisMemberInfo, true);
                                                                        MemberID = qr["V_MEMBER_ID"].ConvertToString();
                                                                        //get Member ID
                                                                        #region Allowance Detail
                                                                        if (qr.IsSuccess)
                                                                        {
                                                                            string allowances = String.Empty;
                                                                            allowances = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_allowances") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_allowances").Value) : null;
                                                                            if (allowances.ConvertToString() != "")
                                                                            {
                                                                                dynamic arrAllowances = allowances.Split(' ');
                                                                                foreach (var item in arrAllowances)
                                                                                {
                                                                                    objMisHHAllowanceDtl = new RES_MIS_HH_ALLOWANCE_DTL();
                                                                                    objMisHHAllowanceDtl.HouseOwnerID = HouseOwnerIDInserted;
                                                                                    objMisHHAllowanceDtl.buildingStructureNo = objNHRSBuildingAssMst.BSSN;
                                                                                    objMisHHAllowanceDtl.HouseholdID = HouseholdID;
                                                                                    objMisHHAllowanceDtl.MemberID = MemberID;
                                                                                    objMisHHAllowanceDtl.AllowanceTypeCD = Convert.ToDecimal(item);
                                                                                    objMisHHAllowanceDtl.AllowanceDay = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_day") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_day").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_day").Value)) : 99;
                                                                                    objMisHHAllowanceDtl.AllowanceMonth = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_month") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_month").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_month").Value)) : 99;
                                                                                    objMisHHAllowanceDtl.AllowanceYears = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_year") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_year").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_year").Value)) : 99;
                                                                                    objMisHHAllowanceDtl.Approved = "Y";
                                                                                    objMisHHAllowanceDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                                    objMisHHAllowanceDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                                                                    objMisHHAllowanceDtl.EnteredBY = SessionCheck.getSessionUsername();
                                                                                    objMisHHAllowanceDtl.EnteredDT = DateTime.Now.ConvertToString();
                                                                                    objMisHHAllowanceDtl.IPAddress = CommonVariables.IPAddress;
                                                                                    objMisHHAllowanceDtl.BatchID = batchID;
                                                                                    objMisHHAllowanceDtl.Mode = mode;
                                                                                    service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                                                    qr = service.SubmitChanges(objMisHHAllowanceDtl, true);
                                                                                }
                                                                            }
                                                                        }
                                                                        #endregion
                                                                    }
                                                                    #endregion
                                                                }
                                                                if (objMisMemberInfo.DefinedCd.ConvertToString() != "")
                                                                {
                                                                    #region Family Detail

                                                                    //SAVE FAMILY HH DTL HERE
                                                                    objMisHHFamilyDtl = new RES_MIS_HH_FAMILY_DTL();
                                                                    objMisHHFamilyDtl.buildingStructureNo = objNHRSBuildingAssMst.BSSN;
                                                                    objMisHHFamilyDtl.HouseOwnerID = HouseOwnerIDInserted;
                                                                    objMisHHFamilyDtl.HouseholdID = HouseholdID;
                                                                    objMisHHFamilyDtl.MemberID = MemberID;
                                                                    objMisHHFamilyDtl.MemberActive = "Y";
                                                                    objMisHHFamilyDtl.RelationTypeCD = member.ContainsKey("hh_rel") ? Utils.ConvertToString(member.First(s => s.Key == "hh_rel").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "hh_rel").Value)) : null;
                                                                    //if (objMisHHFamilyDtl.RelationTypeCd.ConvertToString() == "")
                                                                    //{
                                                                    //    objMisHHFamilyDtl.RelationTypeCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_relation") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_relation").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_relation").Value)) : null;
                                                                    //}
                                                                    objMisHHFamilyDtl.PresenceStatusCD = member.ContainsKey("present") ? Utils.ConvertToString(member.First(s => s.Key == "present").Value) : null;
                                                                    //if (objMisHHFamilyDtl.PresenceStatusCd.ConvertToString() == "")
                                                                    //{
                                                                    //    objMisHHFamilyDtl.PresenceStatusCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_presence") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_presence").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_presence").Value)) : null;
                                                                    //}
                                                                    //if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                                    //{
                                                                    //    string a = "";
                                                                    //}
                                                                    //objMisHHFamilyDtl.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                    objMisHHFamilyDtl.MemberDeath = "N";
                                                                    objMisHHFamilyDtl.MemberMarriage = (objMisMemberInfo.MaritalStatusCD == 99 ? "N" : "Y");
                                                                    objMisHHFamilyDtl.MemberSplit = "N";
                                                                    objMisHHFamilyDtl.TransferHHID = "";
                                                                    objMisHHFamilyDtl.Approved = "Y";
                                                                    objMisHHFamilyDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                    objMisHHFamilyDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                                                    objMisHHFamilyDtl.EnteredBY = SessionCheck.getSessionUsername();
                                                                    objMisHHFamilyDtl.EnteredDT = DateTime.Now.ConvertToString();
                                                                    objMisHHFamilyDtl.BatchID = batchID;
                                                                    objMisHHFamilyDtl.IPAddress = CommonVariables.IPAddress;
                                                                    objMisHHFamilyDtl.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                                    qr = service.SubmitChanges(objMisHHFamilyDtl, true);

                                                                    #endregion
                                                                }
                                                            }
                                                            #endregion
                                                        }
                                                        if (family.ContainsKey("DeadMembers"))
                                                        {
                                                            #region Repeat Deaths
                                                            foreach (SortedDictionary<dynamic, dynamic> deadMember in family.First(s => s.Key == "DeadMembers").Value)
                                                            {
                                                                objMisHHDeathDtl = new RES_MIS_HH_DEATH_DTL();
                                                                //objMisHHDeathDtl.HouseholdHead = "N";
                                                                objMisHHDeathDtl.HouseholdId = HouseholdID;
                                                                objMisHHDeathDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                objMisHHDeathDtl.Sno = deadMember.ContainsKey("death_sn") ? Utils.ConvertToString(deadMember.First(s => s.Key == "death_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "death_sn").Value)) : null;
                                                                objMisHHDeathDtl.BuildingStructureNo = objNHRSBuildingAssMst.BSSN;
                                                                //if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                                //{
                                                                //    string a = "";
                                                                //}
                                                                //objMisHHDeathDtl.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                objMisHHDeathDtl.FirstNameEng = objMisHHDeathDtl.FirstNameLoc = deadMember.ContainsKey("name_fst") ? Utils.ConvertToString(deadMember.First(s => s.Key == "name_fst").Value) : null;
                                                                objMisHHDeathDtl.LastNameEng = objMisHHDeathDtl.LastNameLoc = deadMember.ContainsKey("name_lst") ? Utils.ConvertToString(deadMember.First(s => s.Key == "name_lst").Value) : null;
                                                                objMisHHDeathDtl.MiddleNameEng = objMisHHDeathDtl.MiddleNameLoc = deadMember.ContainsKey("name_mid") ? Utils.ConvertToString(deadMember.First(s => s.Key == "name_mid").Value) : null;
                                                                objMisHHDeathDtl.FullNameEng = objMisHHDeathDtl.FullNameLoc = objMisHHDeathDtl.FirstNameEng.ConvertToString() + (objMisHHDeathDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisHHDeathDtl.MiddleNameEng) + " ") + objMisHHDeathDtl.LastNameEng.ConvertToString();
                                                                objMisHHDeathDtl.GenderCd = deadMember.ContainsKey("gender") ? Utils.ConvertToString(deadMember.First(s => s.Key == "gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "gender").Value)) : null;

                                                                objMisHHDeathDtl.DeathYearLoc = deadMember.ContainsKey("dod_yr") ? Utils.ConvertToString(deadMember.First(s => s.Key == "dod_yr").Value) == "" ? 9999 : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "dod_yr").Value)) : 9999;

                                                                objMisHHDeathDtl.DeathMonthLoc = deadMember.ContainsKey("dod_mnth") ? Utils.ConvertToString(deadMember.First(s => s.Key == "dod_mnth").Value) == "" ? 99 : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "dod_mnth").Value)) : 99;

                                                                objMisHHDeathDtl.DeathDayLoc = deadMember.ContainsKey("dod_date") ? Utils.ConvertToString(deadMember.First(s => s.Key == "dod_date").Value) == "" ? 99 : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "dod_date").Value)) : 99;

                                                                objMisHHDeathDtl.DeathYear = 9999;
                                                                objMisHHDeathDtl.DeathMonth = 99;
                                                                objMisHHDeathDtl.DeathDay = 99;

                                                                objMisHHDeathDtl.Age = deadMember.ContainsKey("age") ? Utils.ConvertToString(deadMember.First(s => s.Key == "age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "age").Value)) : null;

                                                                objMisHHDeathDtl.DeathCertificate = deadMember.ContainsKey("register") ? (Utils.ConvertToString(deadMember.First(s => s.Key == "register").Value) == "1" ? "Y" : "N") : "N";
                                                                if (objMisHHDeathDtl.DeathCertificate == "Y")
                                                                {
                                                                    //provide death certificate and no
                                                                }
                                                                objMisHHDeathDtl.DeathReasonCd = deadMember.ContainsKey("reason") ? Utils.ConvertToString(deadMember.First(s => s.Key == "reason").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "reason").Value)) : null;

                                                                //objMisHHDeathDtl.PerDistrictCd = objMisHHDeathDtl.CurDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                                //objMisHHDeathDtl.PerVdcMunCd = objMisHHDeathDtl.CurVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                                //objMisHHDeathDtl.PerWardNo = objMisHHDeathDtl.CurWardNo = objNHRSHouseOwnerMst.WardNo;
                                                                //objMisHHDeathDtl.PerAreaEng = objMisHHDeathDtl.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                objMisHHDeathDtl.Approved = "Y";
                                                                objMisHHDeathDtl.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                objMisHHDeathDtl.ApprovedBy = SessionCheck.getSessionUsername();
                                                                objMisHHDeathDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                objMisHHDeathDtl.EnteredDt = DateTime.Now.ConvertToString();
                                                                objMisHHDeathDtl.BatchId = batchID;
                                                                objMisHHDeathDtl.Mode = mode;
                                                                //SAVE DEATH DETAIL HERE
                                                                service.PackageName = "MIGNHRS.PKG_RES_HOUSEHOLD";
                                                                qr = service.SubmitChanges(objMisHHDeathDtl, true);
                                                            }
                                                            #endregion
                                                        }
                                                        if (family.ContainsKey("DestructedMembers"))
                                                        {
                                                            #region Repeat Human Destruction
                                                            foreach (SortedDictionary<dynamic, dynamic> destructedMember in family.First(s => s.Key == "DestructedMembers").Value)
                                                            {
                                                                objNHRSHHHumanDestroyDtl = new RES_NHRS_HUMAN_DESTROY_DTL();
                                                                objNHRSHHHumanDestroyDtl.HouseOwnerID = HouseOwnerIDInserted;
                                                                objNHRSHHHumanDestroyDtl.HouseholdID = HouseholdID;
                                                                objNHRSHHHumanDestroyDtl.BuildingStructureNo = objNHRSBuildingAssMst.BSSN;
                                                                objNHRSHHHumanDestroyDtl.MemberID = MemberID;
                                                                objNHRSHHHumanDestroyDtl.Sno = destructedMember.ContainsKey("loi_sn") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "loi_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "loi_sn").Value)) : null;
                                                                objNHRSHHHumanDestroyDtl.FirstNameEng = destructedMember.ContainsKey("name_fst") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "name_fst").Value) : null;
                                                                objNHRSHHHumanDestroyDtl.LastNameEng = destructedMember.ContainsKey("name_lst") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "name_lst").Value) : null;
                                                                objNHRSHHHumanDestroyDtl.MiddleNameEng = destructedMember.ContainsKey("name_mid") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "name_mid").Value) : null;
                                                                objNHRSHHHumanDestroyDtl.FullNameEng = objNHRSHHHumanDestroyDtl.FirstNameEng.ConvertToString() + (objNHRSHHHumanDestroyDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objNHRSHHHumanDestroyDtl.MiddleNameEng) + " ") + objNHRSHHHumanDestroyDtl.LastNameEng.ConvertToString();

                                                                objNHRSHHHumanDestroyDtl.GenderCD = destructedMember.ContainsKey("gender") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "gender").Value)) : null;

                                                                objNHRSHHHumanDestroyDtl.Age = destructedMember.ContainsKey("age") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "age").Value)) : null;

                                                                objNHRSHHHumanDestroyDtl.LossTyp = destructedMember.ContainsKey("loss_typ") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "loss_typ").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "loss_typ").Value)) : null;

                                                                objNHRSHHHumanDestroyDtl.Approved = "Y";
                                                                objNHRSHHHumanDestroyDtl.ApprovedDT = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                                                objNHRSHHHumanDestroyDtl.ApprovedBY = SessionCheck.getSessionUsername();
                                                                objNHRSHHHumanDestroyDtl.EnteredBY = SessionCheck.getSessionUsername();
                                                                objNHRSHHHumanDestroyDtl.EnteredDT = DateTime.Now.ConvertToString();
                                                                objNHRSHHHumanDestroyDtl.BatchID = batchID;
                                                                //SAVE HUMAN DESTRUCTION HERE
                                                                objNHRSHHHumanDestroyDtl.Mode = mode;
                                                                service.PackageName = "MIGNHRS.PKG_RES_HOUSEOWNER";
                                                                qr = service.SubmitChanges(objNHRSHHHumanDestroyDtl, true);
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    else
                                                    {
                                                        #region Batch Info Update For End Time
                                                        objBatchInfo = new ResurveyBatchInfo();
                                                        objBatchInfo.BatchId = batchID;
                                                        objBatchInfo.BatchEndTime = System.DateTime.Now;
                                                        objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                                                        objBatchInfo.HouseholdCnt = householdCnt;
                                                        objBatchInfo.BuildingStructureCnt = houseCnt;
                                                        objBatchInfo.MemberCnt = memberCnt;
                                                        
                                                        objBatchInfo.ErrorMsg = "FAILED";
                                                        objBatchInfo.Mode = "U";
                                                        service.PackageName = "MIGNHRS.PKG_RES_BATCH";
                                                        qr = service.SubmitChanges(objBatchInfo, true);
                                                        #endregion
                                                        DeleteMigrationTableData(batchID);
                                                        service.RollBack();
                                                        return false;
                                                    }


                                                }

                                            }
                                        }
                                                #endregion
                                        //}
                                    }
                                        #endregion
                                    #endregion
                                }
                            }


                        }
                        //tw.Close();
                        //}
                        //#endregion

                        #region Batch Info Update For End Time
                        if (qr.IsSuccess)
                        {
                            objBatchInfo = new ResurveyBatchInfo();
                            objBatchInfo.BatchId = batchID;
                            objBatchInfo.BatchEndTime = System.DateTime.Now;
                            objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                            objBatchInfo.HouseholdCnt = householdCnt;
                            objBatchInfo.BuildingStructureCnt = houseCnt;
                            objBatchInfo.MemberCnt = memberCnt;
                            objBatchInfo.IsPosted = "N";
                            objBatchInfo.Mode = "U";
                            service.PackageName = "MIGNHRS.PKG_RES_BATCH";
                            qr = service.SubmitChanges(objBatchInfo, true);
                        }
                        #endregion
                    }
                    else
                    {

                    }
                }
                catch (OracleException oe)
                {
                    #region Batch Info Update For End Time
                    objBatchInfo = new ResurveyBatchInfo();
                    objBatchInfo.BatchId = batchID;

                    objBatchInfo.BatchEndTime = System.DateTime.Now;
                    objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                    objBatchInfo.HouseholdCnt = householdCnt;
                    objBatchInfo.BuildingStructureCnt = houseCnt;
                    objBatchInfo.PostedError = oe.ConvertToString();
                    objBatchInfo.MemberCnt = memberCnt;
                    objBatchInfo.ErrorMsg = "FAILED";
                    objBatchInfo.IsPosted = "N";
                    objBatchInfo.Mode = "U";
                    UpdateBatch(objBatchInfo);
                    #endregion
                    DeleteMigrationTableData(batchID);

                    service.RollBack();
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    #region Batch Info Update For End Time
                    objBatchInfo = new ResurveyBatchInfo();
                    objBatchInfo.BatchId = batchID;
                    objBatchInfo.BatchEndTime = System.DateTime.Now;
                    objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                    objBatchInfo.HouseholdCnt = householdCnt;
                    objBatchInfo.BuildingStructureCnt = houseCnt;
                    objBatchInfo.MemberCnt = memberCnt;
                    objBatchInfo.ErrorMsg = "FAILED";
                    objBatchInfo.IsPosted = "N";
                    objBatchInfo.Mode = "U";
                    UpdateBatch(objBatchInfo);
                    #endregion
                    DeleteMigrationTableData(batchID);
                    service.RollBack();
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qr != null)
                {
                    res = qr.IsSuccess;
                }
                return res;
            }
                        #endregion
        }
        public Boolean SaveData(string fileName, List<SortedDictionary<dynamic, dynamic>> surveyList, string mode, out int surveyCnt, out int houseCnt, out int householdCnt, out int memberCnt, out string exc)
        {
            QueryResult qr = null;
            bool res = false;
            exc = "";
            surveyCnt = 0;
            houseCnt = 0;
            householdCnt = 0;
            memberCnt = 0;
            CommonFunction commFunction = new CommonFunction();
            List<string> houseIDListDB = new List<string>();
            List<SortedDictionary<dynamic, dynamic>> filteredSurveyList = new List<SortedDictionary<dynamic, dynamic>>();
            BatchInfoInfo objBatchInfo = new BatchInfoInfo();
            MigNhrsHouseOwnerMstInfo objNHRSHouseOwnerMst = new MigNhrsHouseOwnerMstInfo();
            MigNhrsHouseOwnerDtlInfo objNHRSHouseOwnerDtl = new MigNhrsHouseOwnerDtlInfo();
            MigNhrsHhOthResidenceDtlInfo objNHRSOtherHouseDtl = new MigNhrsHhOthResidenceDtlInfo();
            MigNhrsBuildingAssMstInfo objNHRSBuildingAssMst = new MigNhrsBuildingAssMstInfo();
            MigNhrsBuildingAssDtlInfo objNHRSBuildingAssDtl = new MigNhrsBuildingAssDtlInfo();
            MigNhrsBaGeotechnicalRiskInfo objNHRSBaGeotechnicalRisk = new MigNhrsBaGeotechnicalRiskInfo();
            MigNhrsBaSecOccupancyInfo objNHRSSecondaryOccupancy = new MigNhrsBaSecOccupancyInfo();
            MigNhrsBuildingAssPhotoInfo objNHRSBuildingAssPhoto = new MigNhrsBuildingAssPhotoInfo();
            MigNhrsRespondentDtlInfo objNHRSRespondentDtl = new MigNhrsRespondentDtlInfo();
            MigMisHouseholdInfoInfo objMisHouseholdInfo = new MigMisHouseholdInfoInfo();
            MigMisMemberInfo objMisMemberInfo = new MigMisMemberInfo();
            MigMisMemberDocInfo objMisMemberDoc = new MigMisMemberDocInfo();
            NhrsBankInfo objNHRSBankInfo = new NhrsBankInfo();
            MigMisHhFamilyDtlInfo objMisHHFamilyDtl = new MigMisHhFamilyDtlInfo();
            MigMisHhAllowanceDtlInfo objMisHHAllowanceDtl = new MigMisHhAllowanceDtlInfo();
            MigMisHhDeathDtlInfo objMisHHDeathDtl = new MigMisHhDeathDtlInfo();
            MigNhrsHhHumanDestroyDtlInfo objNHRSHHHumanDestroyDtl = new MigNhrsHhHumanDestroyDtlInfo();
            MigNhrsHhSuperstructureMatInfo objNHRSHhSuperStructMatInfo = new MigNhrsHhSuperstructureMatInfo();
            MigNhrsRcvdEqReliefMoneyInfo objMigNhrsRcvdRelief = new MigNhrsRcvdEqReliefMoneyInfo();
            MigNhrsHhWatersourceDtlInfo objWaterSource = new MigNhrsHhWatersourceDtlInfo();
            MigNhrsHhFuelsourceDtlInfo objFuelSource = new MigNhrsHhFuelsourceDtlInfo();
            MigNhrsHhIndicatorDtlInfo objIndicatorDtl = new MigNhrsHhIndicatorDtlInfo();
            MigNhrsHhLightsourceDtlInfo objLightSource = new MigNhrsHhLightsourceDtlInfo();
            MigNhrsHhToiletTypeInfo objToiletType = new MigNhrsHhToiletTypeInfo();

            string HouseOwnerIDInserted = String.Empty;
            string HouseholdID = String.Empty;
            string MemberID = String.Empty;
            decimal? batchID = 0;
            List<Dictionary<string, string>> lstmember = new List<Dictionary<string, string>>();

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    houseIDListDB = new List<string>();
                    houseIDListDB = HouseIDList();
                    filteredSurveyList = surveyList.Where(m => !houseIDListDB.Contains(m["building_damage_assessment/hh_data/hh_address/hh_address_ward/house_ID"].Value)).ToList();
                    if (filteredSurveyList.Count > 0)
                    {
                        surveyCnt = filteredSurveyList.Count;
                        #region Batch Info Insert For Start Time
                        objBatchInfo = new BatchInfoInfo();
                        objBatchInfo.BatchDate = System.DateTime.Now;
                        objBatchInfo.BatchStartTime = System.DateTime.Now;
                        objBatchInfo.FileName = fileName;
                        objBatchInfo.Mode = mode;
                        service.Begin();
                        service.PackageName = "MIGNHRS.PKG_BATCH";
                        //Main Table 
                        qr = service.SubmitChanges(objBatchInfo, true);
                        //SAVE BATCH INFO HERE
                        #endregion
                        #region Entire JSON Data Import Here
                        if (qr.IsSuccess)
                        {
                            batchID = qr["v_BATCH_ID"].ToDecimal();
                            foreach (var survey in filteredSurveyList)
                            {
                                if (survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/house_ID") && !houseIDListDB.Contains(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/house_ID").Value)))
                                {
                                    #region Owner's Master Data
                                    objNHRSHouseOwnerMst = new MigNhrsHouseOwnerMstInfo();
                                    objNHRSHouseOwnerMst.DefinedCd = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/house_ID") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/house_ID").Value) : null;
                                    objNHRSHouseOwnerMst.NhrsUuid = survey.ContainsKey("_uuid") ? Utils.ConvertToString(survey.First(s => s.Key == "_uuid").Value) : null;
                                    objNHRSHouseOwnerMst.Sim = survey.ContainsKey("SIM") ? Utils.ConvertToString(survey.First(s => s.Key == "SIM").Value) : null;
                                    objNHRSHouseOwnerMst.BambooDatasetdi = survey.ContainsKey("_bamboo_dataset_id") ? Utils.ConvertToString(survey.First(s => s.Key == "_bamboo_dataset_id").Value) : null;
                                    objNHRSHouseOwnerMst.Notes = survey.ContainsKey("_notes") ? Utils.ConvertToString(survey.First(s => s.Key == "_notes").Value) : null;
                                    objNHRSHouseOwnerMst.Status = survey.ContainsKey("_status") ? Utils.ConvertToString(survey.First(s => s.Key == "_status").Value) : null;
                                    objNHRSHouseOwnerMst.Submissiontime = survey.ContainsKey("_submission_time") ? Utils.ConvertToString(survey.First(s => s.Key == "_submission_time").Value) : null;
                                    objNHRSHouseOwnerMst.SubmissionBy = survey.ContainsKey("_submitted_by") ? Utils.ConvertToString(survey.First(s => s.Key == "_submitted_by").Value) : null;
                                    objNHRSHouseOwnerMst.Tags = survey.ContainsKey("_tags") ? Utils.ConvertToString(survey.First(s => s.Key == "_tags").Value) : null;
                                    objNHRSHouseOwnerMst.VersionNo = survey.ContainsKey("_version") ? Utils.ConvertToString(survey.First(s => s.Key == "_version").Value) : null;
                                    objNHRSHouseOwnerMst.XformIdString = survey.ContainsKey("_xform_id_string") ? Utils.ConvertToString(survey.First(s => s.Key == "_xform_id_string").Value) : null;
                                    objNHRSHouseOwnerMst.InstanceUniqueSno = survey.ContainsKey("_id") ? Utils.ConvertToString(survey.First(s => s.Key == "_id").Value) : null;
                                    if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                    {
                                        string a = "";
                                    }
                                    objNHRSHouseOwnerMst.InterviewStart = survey.ContainsKey("start") ? Utils.ConvertToString(survey.First(s => s.Key == "start").Value) : null;
                                    objNHRSHouseOwnerMst.InterviewEnd = survey.ContainsKey("end") ? Utils.ConvertToString(survey.First(s => s.Key == "end").Value) : null;
                                    objNHRSHouseOwnerMst.InterviewDt = survey.ContainsKey("today") ? Utils.ConvertToString(survey.First(s => s.Key == "today").Value) == "" ? null : Convert.ToDateTime(Utils.ConvertToString(survey.First(s => s.Key == "today").Value)) : null;
                                    objNHRSHouseOwnerMst.Imei = survey.ContainsKey("imei") ? Utils.ConvertToString(survey.First(s => s.Key == "imei").Value) : null;
                                    objNHRSHouseOwnerMst.Imsi = survey.ContainsKey("imsi") ? Utils.ConvertToString(survey.First(s => s.Key == "imsi").Value) : null;
                                    objNHRSHouseOwnerMst.SimNumber = survey.ContainsKey("SIM") ? Utils.ConvertToString(survey.First(s => s.Key == "SIM").Value) : null;
                                    objNHRSHouseOwnerMst.MobileNumber = survey.ContainsKey("building_damage_assessment/hh_data/hh_owner_contact") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_contact").Value) : null;
                                    objNHRSHouseOwnerMst.EnumeratorId = survey.ContainsKey("building_damage_assessment/start_page/enumerator_id") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/start_page/enumerator_id").Value) : null;
                                    objNHRSHouseOwnerMst.DistrictCd = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_district/district") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_district/district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_district/district").Value)) : null;
                                    objNHRSHouseOwnerMst.VdcMunCd = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_vdc/vdc_code") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_vdc/vdc_code").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_vdc/vdc_code").Value)) : null;
                                    objNHRSHouseOwnerMst.VDC = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_vdc/vdc") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_vdc/vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_vdc/vdc").Value)) : null;
                                    objNHRSHouseOwnerMst.WardNo = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/ward") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/ward").Value)) : null;
                                    objNHRSHouseOwnerMst.EnumerationArea = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/enumeration_area") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/enumeration_area").Value) : null;
                                    objNHRSHouseOwnerMst.AreaEng = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/tole") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/tole").Value) : null;
                                    objNHRSHouseOwnerMst.AreaLoc = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/tole") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/tole").Value) : null;
                                    // objNHRSHouseOwnerMst.HouseSno = survey.ContainsKey("building_damage_assessment/hh_data/hh_address/hh_address_ward/house_no") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/house_no").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_address/hh_address_ward/house_no").Value)) : null;
                                    objNHRSHouseOwnerMst.HouseFamilyOwnerCnt = survey.ContainsKey("building_damage_assessment/hh_data/house_info/house_owner_number") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/house_info/house_owner_number").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/house_info/house_owner_number").Value)) : null;
                                    // objNHRSHouseOwnerMst.IsOwner = survey.ContainsKey("building_damage_assessment/hh_data/respondent_hh_owner") ? (Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent_hh_owner").Value) == "1" ? "Y" : "N") : "N";
                                    //objNHRSHouseOwnerMst.RespondentFirstName = objNHRSHouseOwnerMst.RespondentFirstNameLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info_1/respondent_first_name") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info_1/respondent_first_name").Value) : null;
                                    //objNHRSHouseOwnerMst.RespondentMiddleName = objNHRSHouseOwnerMst.RespondentMiddleNameLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info_1/respondent_middle_name") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info_1/respondent_middle_name").Value) : null;
                                    //objNHRSHouseOwnerMst.RespondentLastName = objNHRSHouseOwnerMst.RespondentLastNameLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info_1/respondent_last_name") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info_1/respondent_last_name").Value) : null;
                                    //objNHRSHouseOwnerMst.RespondentGenderCd = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info/respondent_gender") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_gender").Value)) : null;
                                    //objNHRSHouseOwnerMst.HhRelationTypeCd = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info/respondent_relation_with_house_owner") ? Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_relation_with_house_owner").Value)) : null;
                                    objNHRSHouseOwnerMst.NotInterviwingReasonCd = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info/reason_for_not_interviewing_owner") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/reason_for_not_interviewing_owner").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/reason_for_not_interviewing_owner").Value)) : null;
                                    //objNHRSHouseOwnerMst.RespondentPhoto = survey.ContainsKey("building_damage_assessment/hh_data/respondent_photo") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent_photo").Value) : null;
                                    objNHRSHouseOwnerMst.ElectionCenterOHouseCnt = survey.ContainsKey("building_damage_assessment/hh_data/hh_owner_house/hh_owner_house_number_same_place") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_house/hh_owner_house_number_same_place").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_house/hh_owner_house_number_same_place").Value)) : 0;
                                    objNHRSHouseOwnerMst.NonElectionCenterFHouseCnt = survey.ContainsKey("building_damage_assessment/hh_data/hh_owner_house/hh_owner_house_number_other_place") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_house/hh_owner_house_number_other_place").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_house/hh_owner_house_number_other_place").Value)) : 0;
                                    objNHRSHouseOwnerMst.NonresidNondamageHCnt = survey.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_buildings/hh_owner_other_buildings_not_damaged_number") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_buildings/hh_owner_other_buildings_not_damaged_number").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_buildings/hh_owner_other_buildings_not_damaged_number").Value)) : 0;
                                    objNHRSHouseOwnerMst.NonresidPartialDamageHCnt = survey.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_buildings/hh_owner_other_buildings_partial_damage_number") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_buildings/hh_owner_other_buildings_partial_damage_number").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_buildings/hh_owner_other_buildings_partial_damage_number").Value)) : 0;
                                    objNHRSHouseOwnerMst.NonresidFullDamageHCnt = survey.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_buildings/hh_owner_other_buildings_damaged_number") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_buildings/hh_owner_other_buildings_damaged_number").Value) == "" ? 0 : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_buildings/hh_owner_other_buildings_damaged_number").Value)) : 0;
                                    //objNHRSHouseOwnerMst.acknowledge = survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/acknowledge").ToString();
                                    //objNHRSHouseOwnerMst.ownerContact = survey.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_contact").ToString();
                                    objNHRSHouseOwnerMst.SocialMobilizerPresentFlag = survey.ContainsKey("building_damage_assessment/social_mobilizer_present") ? (Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/social_mobilizer_present").Value) == "1" ? "Y" : "N") : null;
                                    objNHRSHouseOwnerMst.Remarks = survey.ContainsKey("building_damage_assessment/comment_remark") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/comment_remark").Value) : null;
                                    objNHRSHouseOwnerMst.RemarksLoc = survey.ContainsKey("building_damage_assessment/comment_remark") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/comment_remark").Value) : null;
                                    objNHRSHouseOwnerMst.IPAddress = CommonVariables.IPAddress;
                                    objNHRSHouseOwnerMst.ErrorNo = survey.ContainsKey("errorNo") ? Utils.ConvertToString(survey.First(s => s.Key == "errorNo").Value) : null;
                                    objNHRSHouseOwnerMst.ErrorMsg = survey.ContainsKey("errorMsg") ? Utils.ConvertToString(survey.First(s => s.Key == "errorMsg").Value) : null;
                                    objNHRSHouseOwnerMst.RespondentIsHouseOwner = survey.ContainsKey("building_damage_assessment/hh_data/respondent_hh_owner") ? (Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent_hh_owner").Value) == "1" ? "Y" : "N") : "N";
                                    objNHRSHouseOwnerMst.Approved = "N";
                                    objNHRSHouseOwnerMst.EnteredBy = SessionCheck.getSessionUsername();
                                    objNHRSHouseOwnerMst.EnteredDt = DateTime.Now;
                                    objNHRSHouseOwnerMst.BatchId = batchID;
                                    objNHRSHouseOwnerMst.Mode = mode;
                                    service.PackageName = "MIGNHRS.PKG_HOUSEOWNER";
                                    //Main Table 
                                    qr = service.SubmitChanges(objNHRSHouseOwnerMst, true);
                                    #endregion
                                    if (qr.IsSuccess)
                                    {
                                        HouseOwnerIDInserted = qr["V_HOUSE_OWNER_ID"].ConvertToString();
                                        #region Owners' Detail
                                        foreach (SortedDictionary<dynamic, dynamic> owner in survey.First(s => s.Key == "Owners").Value)
                                        {
                                            objNHRSHouseOwnerDtl = new MigNhrsHouseOwnerDtlInfo();
                                            objNHRSHouseOwnerDtl.FirstNameEng = objNHRSHouseOwnerDtl.FirstNameLoc = owner.ContainsKey("building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_first_name") ? Utils.ConvertToString(owner.First(s => s.Key == "building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_first_name").Value) : null;
                                            if (objNHRSHouseOwnerDtl.FirstNameEng.ConvertToString() != "")
                                            {
                                                objNHRSHouseOwnerDtl.MiddleNameEng = objNHRSHouseOwnerDtl.MiddleNameLoc = owner.ContainsKey("building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_middle_name") ? Utils.ConvertToString(owner.First(s => s.Key == "building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_middle_name").Value) : null;
                                                objNHRSHouseOwnerDtl.LastNameEng = objNHRSHouseOwnerDtl.LastNameLoc = owner.ContainsKey("building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_last_name") ? Utils.ConvertToString(owner.First(s => s.Key == "building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_last_name").Value) : null;
                                                objNHRSHouseOwnerDtl.GenderCd = owner.ContainsKey("building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_gender") ? Utils.ConvertToString(owner.First(s => s.Key == "building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(owner.First(s => s.Key == "building_damage_assessment/hh_data/house_owner_info/hh_owner_info_group/hh_owner_gender").Value)) : null;
                                                objNHRSHouseOwnerDtl.MemberPhotoId = "";
                                                // objNHRSHouseOwnerDtl.HouseholdHead = objNHRSHouseOwnerMst.IsOwner;
                                                objNHRSHouseOwnerDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                objNHRSHouseOwnerDtl.Approved = "N";
                                                objNHRSHouseOwnerDtl.EnteredBy = objNHRSHouseOwnerDtl.EnteredByLoc = SessionCheck.getSessionUsername();
                                                objNHRSHouseOwnerDtl.EnteredDt = DateTime.Now;
                                                objNHRSHouseOwnerDtl.BatchId = batchID;
                                                objNHRSHouseOwnerDtl.Mode = mode;
                                                objNHRSHouseOwnerDtl.IPAddress = CommonVariables.IPAddress;
                                                //HouseOwner
                                                qr = service.SubmitChanges(objNHRSHouseOwnerDtl, true);
                                            }
                                        }
                                        #region Respondent Detail
                                        objNHRSRespondentDtl = new MigNhrsRespondentDtlInfo();
                                        objNHRSRespondentDtl.HouseOwnerId = HouseOwnerIDInserted;
                                        if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                        {
                                            string a = "";
                                        }
                                        objNHRSRespondentDtl.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                        objNHRSRespondentDtl.RespondentPhoto = survey.ContainsKey("building_damage_assessment/hh_data/respondent_photo") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent_photo").Value) : null;
                                        if (objNHRSHouseOwnerMst.RespondentIsHouseOwner == "Y") //// Question to be asked is what if more than one owner??
                                        {
                                            if (objNHRSHouseOwnerDtl.FirstNameEng.ConvertToString() != "")
                                            {
                                                objNHRSRespondentDtl.RespondentFirstName = objNHRSRespondentDtl.RespondentFirstNameLoc = objNHRSHouseOwnerDtl.FirstNameEng;
                                                objNHRSRespondentDtl.RespondentMiddleName = objNHRSRespondentDtl.RespondentMiddleNameLoc = objNHRSHouseOwnerDtl.MiddleNameEng;
                                                objNHRSRespondentDtl.RespondentLastName = objNHRSRespondentDtl.RespondentLastNameLoc = objNHRSHouseOwnerDtl.LastNameEng;
                                                objNHRSRespondentDtl.RespondentGenderCd = objNHRSHouseOwnerDtl.GenderCd;
                                                objNHRSRespondentDtl.HhRelationTypeCd = 1;//Convert.ToInt32(commFunction.GetValueFromDataBase(Convert.ToString(ConstantVariables.HouseHoldHeadRelationTypeID), "MIS_RELATION_TYPE", "DEFINED_CD", "RELATION_TYPE_CD"));
                                                objNHRSRespondentDtl.RespondentFullName = objNHRSRespondentDtl.RespondentFullNameLoc = objNHRSRespondentDtl.RespondentFirstName.ConvertToString() + (objNHRSRespondentDtl.RespondentMiddleName.ConvertToString() == "" ? " " : (" " + objNHRSRespondentDtl.RespondentMiddleName) + " ") + objNHRSRespondentDtl.RespondentLastName.ConvertToString();
                                                objNHRSRespondentDtl.OtherRelationType = objNHRSRespondentDtl.OtherRelationTypeLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_other_realtion_with_house_owner") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_other_realtion_with_house_owner").Value) : null;
                                                objNHRSRespondentDtl.RespondentPhoto = survey.ContainsKey("building_damage_assessment/hh_data/respondent_photo") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent_photo").Value) : null;
                                                //objNHRSRespondentDtl.Approved = "N";
                                                //objNHRSRespondentDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                //objNHRSRespondentDtl.EnteredDt = DateTime.Now;
                                                objNHRSRespondentDtl.IPAddress = CommonVariables.IPAddress;
                                                objNHRSRespondentDtl.BatchId = batchID;
                                                objNHRSRespondentDtl.Mode = mode;
                                                qr = service.SubmitChanges(objNHRSRespondentDtl, true);
                                                //SAVE RESPONDENT DETAIL HERE      
                                            }
                                        }
                                        else
                                        {
                                            objNHRSRespondentDtl.RespondentFirstName = objNHRSRespondentDtl.RespondentFirstNameLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respodent_info_1/respondent_first_name") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respodent_info_1/respondent_first_name").Value) : null;
                                            objNHRSRespondentDtl.RespondentMiddleName = objNHRSRespondentDtl.RespondentMiddleNameLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respodent_info_1/respondent_middle_name") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respodent_info_1/respondent_middle_name").Value) : null;
                                            objNHRSRespondentDtl.RespondentLastName = objNHRSRespondentDtl.RespondentLastNameLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respodent_info_1/respondent_last_name") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respodent_info_1/respondent_last_name").Value) : null;
                                            objNHRSRespondentDtl.RespondentGenderCd = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info/respondent_gender") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_gender").Value)) : null;
                                            objNHRSRespondentDtl.HhRelationTypeCd = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_info/respondent_realtion_with_house_owner") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_realtion_with_house_owner").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_info/respondent_realtion_with_house_owner").Value)) : null;
                                            objNHRSRespondentDtl.RespondentFullName = objNHRSRespondentDtl.RespondentFullNameLoc = objNHRSRespondentDtl.RespondentFirstName.ConvertToString() + (objNHRSRespondentDtl.RespondentMiddleName.ConvertToString() == "" ? " " : (" " + objNHRSRespondentDtl.RespondentMiddleName) + " ") + objNHRSRespondentDtl.RespondentLastName.ConvertToString();
                                            objNHRSRespondentDtl.OtherRelationType = objNHRSRespondentDtl.OtherRelationTypeLoc = survey.ContainsKey("building_damage_assessment/hh_data/respondent/respondent_other_realtion_with_house_owner") ? Utils.ConvertToString(survey.First(s => s.Key == "building_damage_assessment/hh_data/respondent/respondent_other_realtion_with_house_owner").Value) : null;
                                            //objNHRSRespondentDtl.Approved = "N";
                                            //objNHRSRespondentDtl.EnteredBy = SessionCheck.getSessionUsername();
                                            //objNHRSRespondentDtl.EnteredDt = DateTime.Now;
                                            objNHRSRespondentDtl.IPAddress = CommonVariables.IPAddress;
                                            objNHRSRespondentDtl.BatchId = batchID;
                                            objNHRSRespondentDtl.Mode = mode;
                                            qr = service.SubmitChanges(objNHRSRespondentDtl, true);
                                            //SAVE RESPONDENT DETAIL HERE   
                                        }
                                        #endregion
                                        #endregion
                                        #region Other Houses Detail
                                        foreach (SortedDictionary<dynamic, dynamic> house in survey.First(s => s.Key == "OtherHouses").Value)
                                        {
                                            objNHRSOtherHouseDtl = new MigNhrsHhOthResidenceDtlInfo();
                                            objNHRSOtherHouseDtl.HouseOwnerId = HouseOwnerIDInserted;
                                            objNHRSOtherHouseDtl.OtherResidenceId = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_id") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_id").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_id").Value)) : null;
                                            //objNHRSOtherHouseDtl.MemberId = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/hh_owner_other_house_district") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/hh_owner_other_house_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/hh_owner_other_house_district").Value)) : null;
                                            objNHRSOtherHouseDtl.FirstNameEng = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_first_name") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_first_name").Value) : null;
                                            if (objNHRSOtherHouseDtl.FirstNameEng.ConvertToString() != "")
                                            {
                                                objNHRSOtherHouseDtl.MiddleNameEng = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_middle_name") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_middle_name").Value) : null;
                                                objNHRSOtherHouseDtl.LastNameEng = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_last_name") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_last_name").Value) : null;
                                                objNHRSOtherHouseDtl.FullNameEng = objNHRSOtherHouseDtl.FirstNameEng.ConvertToString() + (objNHRSOtherHouseDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objNHRSOtherHouseDtl.MiddleNameEng) + " ") + objNHRSOtherHouseDtl.LastNameEng.ConvertToString();
                                                objNHRSOtherHouseDtl.FirstNameLoc = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_first_name") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_first_name").Value) : null;
                                                objNHRSOtherHouseDtl.MiddleNameLoc = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_middle_name") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_middle_name").Value) : null;
                                                objNHRSOtherHouseDtl.LastNameLoc = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_last_name") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/other_house_owner_last_name").Value) : null;
                                                objNHRSOtherHouseDtl.FullNameLoc = objNHRSOtherHouseDtl.FirstNameEng.ConvertToString() + (objNHRSOtherHouseDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objNHRSOtherHouseDtl.MiddleNameEng) + " ") + objNHRSOtherHouseDtl.LastNameEng.ConvertToString();
                                                objNHRSOtherHouseDtl.OtherDistrictCd = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/hh_owner_other_house_district") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/hh_owner_other_house_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_1/hh_owner_other_house_district").Value)) : null;
                                                objNHRSOtherHouseDtl.OtherVdcMunCd = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_vdc_code") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_vdc_code").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_vdc_code").Value)) : null;
                                                objNHRSOtherHouseDtl.OtherWardNo = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_ward") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_ward").Value)) : null;
                                                //objNHRSOtherHouseDtl.OtherAreaEng = ??
                                                //objNHRSOtherHouseDtl.OtherAreaLoc = ??
                                                objNHRSOtherHouseDtl.BuildingConditionCd = house.ContainsKey("building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_condition") ? Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_condition").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "building_damage_assessment/hh_data/hh_owner_other_house_detail/hh_owner_other_house_group/hh_owner_other_house_info_2/hh_owner_other_house_condition").Value)) : null;
                                                objNHRSOtherHouseDtl.Approved = "N";
                                                objNHRSOtherHouseDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                objNHRSOtherHouseDtl.EnteredDt = DateTime.Now;
                                                objNHRSOtherHouseDtl.IPAddress = CommonVariables.IPAddress;
                                                objNHRSOtherHouseDtl.BatchId = batchID;
                                                objNHRSOtherHouseDtl.Mode = mode;
                                                //House other Place
                                                qr = service.SubmitChanges(objNHRSOtherHouseDtl, true);
                                            }
                                        }
                                        #endregion
                                        #region Buildings' Detail
                                        foreach (SortedDictionary<dynamic, dynamic> building in survey.First(s => s.Key == "Buildings").Value)
                                        {
                                            objNHRSBuildingAssMst = new MigNhrsBuildingAssMstInfo();
                                            objNHRSBuildingAssMst.HouseOwnerId = HouseOwnerIDInserted;
                                            objNHRSBuildingAssMst.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;
                                            objNHRSBuildingAssMst.Houselandlegalowner = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_family_count/legal_ownership") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_family_count/legal_ownership").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_family_count/legal_ownership").Value)) : null;
                                            if (objNHRSBuildingAssMst.Houselandlegalowner.ConvertToString() != "")
                                            {
                                                houseCnt++;
                                                #region Building Assessment Master Data
                                                objNHRSBuildingAssMst.DistrictCd = objNHRSHouseOwnerMst.DistrictCd; // Same as of Owner Master's District Code
                                                if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                {
                                                    string a = "";
                                                }
                                                objNHRSBuildingAssMst.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                objNHRSBuildingAssMst.BuildingConditionCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_condition") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_condition").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_condition").Value)) : null;
                                                objNHRSBuildingAssMst.StoreysCntBefore = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/no_of_storey_before_earthquake") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/no_of_storey_before_earthquake").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/no_of_storey_before_earthquake").Value)) : null;
                                                objNHRSBuildingAssMst.StoreysCntAfter = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/no_of_storey_now") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/no_of_storey_now").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/no_of_storey_now").Value)) : null;
                                                objNHRSBuildingAssMst.HouseAge = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/house_age") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/house_age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/house_age").Value)) : null;
                                                objNHRSBuildingAssMst.PlinthAreaCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/plinth_area") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/plinth_area").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/plinth_area").Value)) : null;
                                                objNHRSBuildingAssMst.HouseHeightBeforeEQ = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/height_before_eq") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/height_before_eq").Value) : null;
                                                objNHRSBuildingAssMst.HouseHeightAfterEQ = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/height_after_eq") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/height_after_eq").Value) : null;
                                                objNHRSBuildingAssMst.GroundSurfaceCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/surface_type") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/surface_type").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/hh_detail/surface_type").Value)) : null;
                                                objNHRSBuildingAssMst.FoundationTypeCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/foundation_type") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/foundation_type").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/foundation_type").Value)) : null;
                                                objNHRSBuildingAssMst.RcMaterialCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/roof_type") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/roof_type").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/roof_type").Value)) : null;
                                                objNHRSBuildingAssMst.FcMaterialCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/ground_floor_type") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/ground_floor_type").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_1/ground_floor_type").Value)) : null;
                                                objNHRSBuildingAssMst.ScMaterialCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_2/floor_type") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_2/floor_type").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_2/floor_type").Value)) : null;
                                                objNHRSBuildingAssMst.BuildingPositionCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_3/house_position") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_3/house_position").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_3/house_position").Value)) : null;
                                                objNHRSBuildingAssMst.BuildingPlanCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_3/plan_configuration") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_3/plan_configuration").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_3/plan_configuration").Value)) : null;
                                                objNHRSBuildingAssMst.Householdcntaftereq = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/no_family") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/no_family").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/no_family").Value)) : null;
                                                objNHRSBuildingAssMst.IsGeotechnicalRisk = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/risk_felt") ? (Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/risk_felt").Value) == "1" ? "Y" : "N") : "N";
                                                objNHRSBuildingAssMst.AssessedAreaCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/assessed_area") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/assessed_area").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/assessed_area").Value)) : null;
                                                objNHRSBuildingAssMst.TechsolutionCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/technical_solution_list") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/technical_solution_list").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/technical_solution_list").Value)) : null;
                                                objNHRSBuildingAssMst.DamageGradeCd = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/damage_grade") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/damage_grade").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/assessment_detail/damage_grade").Value)) : null;
                                                objNHRSBuildingAssMst.ReconstructionStarted = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/reconstruction_started") ? (Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/reconstruction_started").Value) == "1" ? "Y" : "N") : "N";
                                                objNHRSBuildingAssMst.IsSecondaryUse = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/secondary_used") ? (Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/secondary_used").Value) == "1" ? "Y" : "N") : "N";
                                                objNHRSBuildingAssMst.HoDefinedCd = objNHRSHouseOwnerMst.DefinedCd;
                                                objNHRSBuildingAssMst.InstanceUniqueSno = objMisHouseholdInfo.InstanceUniqueSno;
                                                objNHRSBuildingAssMst.GeneralComments = objNHRSBuildingAssMst.GeneralCommentsLoc = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment").Value) : null;
                                                //geopoint_details
                                                if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/geo_point"))
                                                {
                                                    string geopoint = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/geo_point") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/geo_point").Value) : null;
                                                    dynamic arrGeopoints = geopoint.Split(' ');
                                                    objNHRSBuildingAssMst.Latitude = Utils.ConvertToString(arrGeopoints[0]);
                                                    objNHRSBuildingAssMst.Longitude = Utils.ConvertToString(arrGeopoints[1]);
                                                    objNHRSBuildingAssMst.Altitude = Utils.ConvertToString(arrGeopoints[2]);
                                                    objNHRSBuildingAssMst.Accuracy = Utils.ConvertToString(arrGeopoints[3]);
                                                }
                                                objNHRSBuildingAssMst.Approved = "N";
                                                objNHRSBuildingAssMst.EnteredBy = SessionCheck.getSessionUsername();
                                                objNHRSBuildingAssMst.EnteredDt = DateTime.Now;
                                                objNHRSBuildingAssMst.IPAddress = CommonVariables.IPAddress;
                                                objNHRSBuildingAssMst.BatchId = batchID;
                                                objNHRSBuildingAssMst.Mode = mode;
                                                service.PackageName = "MIGNHRS.PKG_BUILDING_ASSESSMENT";
                                                qr = service.SubmitChanges(objNHRSBuildingAssMst, true);
                                                #endregion
                                                #region Building Photos
                                                if (qr.IsSuccess)
                                                {
                                                    objNHRSBuildingAssPhoto = new MigNhrsBuildingAssPhotoInfo();
                                                    objNHRSBuildingAssPhoto.HouseOwnerId = HouseOwnerIDInserted;
                                                    objNHRSBuildingAssPhoto.BuildingStructureNo = objNHRSBuildingAssMst.BuildingStructureNo;
                                                    if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                    {
                                                        string a = "";
                                                    }
                                                    objNHRSBuildingAssPhoto.NhrsUuid = objNHRSBuildingAssMst.NhrsUuid;
                                                    objNHRSBuildingAssPhoto.BatchId = batchID;
                                                    objNHRSBuildingAssPhoto.Approved = "N";
                                                    objNHRSBuildingAssPhoto.EnteredBy = SessionCheck.getSessionUsername();
                                                    objNHRSBuildingAssPhoto.EnteredDt = DateTime.Now;
                                                    objNHRSBuildingAssPhoto.IPAddress = CommonVariables.IPAddress;
                                                    objNHRSBuildingAssPhoto.Mode = mode;
                                                    service.PackageName = "MIGNHRS.PKG_BUILDING_ASSESSMENT";
                                                    string photoName = String.Empty;
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/back_face"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "12";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/back_face") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/back_face").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/front_face"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "13";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/front_face") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/front_face").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/left_face"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "14";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/left_face") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/left_face").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/right_face"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "15";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/right_face") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/corner_photo/right_face").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/internal_damage_photo"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "16";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/internal_damage_photo") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/internal_damage_photo").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/photo_rubble"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "17";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/photo_rubble") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/photo_rubble").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/damage_photo_1"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "18";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/damage_photo_1") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/damage_photo_1").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/damage_photo_2"))
                                                    {
                                                        objNHRSBuildingAssPhoto.DocTypeCd = "19";
                                                        photoName = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/damage_photo_2") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/building_photo/other_photo/damage_photo_2").Value) : null;
                                                        objNHRSBuildingAssPhoto.PhotoPath = photoName;
                                                        qr = service.SubmitChanges(objNHRSBuildingAssPhoto, true);
                                                    }
                                                }
                                                #endregion
                                                #region Building Assessment Detail
                                                if (qr.IsSuccess)
                                                {
                                                    objNHRSBuildingAssDtl = new MigNhrsBuildingAssDtlInfo();
                                                    objNHRSBuildingAssDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                    objNHRSBuildingAssDtl.DistrictCd = objNHRSBuildingAssMst.DistrictCd;
                                                    objNHRSBuildingAssDtl.BuildingStructureNo = objNHRSBuildingAssMst.BuildingStructureNo;
                                                    objNHRSBuildingAssDtl.BatchId = batchID;
                                                    objNHRSBuildingAssDtl.Approved = "N";
                                                    objNHRSBuildingAssDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                    objNHRSBuildingAssDtl.EnteredDt = DateTime.Now;
                                                    objNHRSBuildingAssDtl.IPAddress = CommonVariables.IPAddress;
                                                    objNHRSBuildingAssDtl.Mode = mode;
                                                    string damageLevelDtl = string.Empty;
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/collapse"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 2;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/collapse") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/collapse").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/building_leaning"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 3;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/building_leaning") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/building_leaning").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/adjacent_building_hazard"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 4;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/adjacent_building_hazard") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/overall_hazard/adjacent_building_hazard").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/structural_hazard/foundation/foundation_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 6;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/structural_hazard/foundation/foundation_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/structural_hazard/foundation/foundation_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/structural_hazard/floor/floor_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 7;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/structural_hazard/floor/floor_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/structural_hazard/floor/floor_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/corner_separation/corner_separation_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 9;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/corner_separation/corner_separation_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/corner_separation/corner_separation_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/diagonal_cracking/diagonal_cracking_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 10;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/diagonal_cracking/diagonal_cracking_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/diagonal_cracking/diagonal_cracking_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/in_plane_failure_wall/in_plane_failure_wall_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 11;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/in_plane_failure_wall/in_plane_failure_wall_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/in_plane_failure_wall/in_plane_failure_wall_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/out_of_plane_failure_wall_carrying_roof/out_of_plane_failure_wall_carrying_roof_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 12;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/out_of_plane_failure_wall_carrying_roof/out_of_plane_failure_wall_carrying_roof_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/out_of_plane_failure_wall_carrying_roof/out_of_plane_failure_wall_carrying_roof_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/out_of_plane_failure_wall_not_carrying_roof/out_of_plane_failure_wall_not_carrying_roof_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 13;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/out_of_plane_failure_wall_not_carrying_roof/out_of_plane_failure_wall_not_carrying_roof_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/out_of_plane_failure_wall_not_carrying_roof/out_of_plane_failure_wall_not_carrying_roof_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/gable_wall_collapse/gable_wall_collapse_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 14;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/gable_wall_collapse/gable_wall_collapse_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/gable_wall_collapse/gable_wall_collapse_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/delamination/delamination_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 15;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/delamination/delamination_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/mansory_building/delamination/delamination_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/column_failure/column_failure_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 17;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/column_failure/column_failure_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/column_failure/column_failure_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/beam_failure/beam_failure_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 18;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/beam_failure/beam_failure_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/beam_failure/beam_failure_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/infill_wall/infill_wall_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 19;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/infill_wall/infill_wall_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/rc_buildings/infill_wall/infill_wall_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/staircase/staircase_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 22;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/staircase/staircase_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/staircase/staircase_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/parapets/parapets_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 23;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/parapets/parapets_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/parapets/parapets_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/cladding_glazing/cladding_glazing_damage_level"))
                                                    {
                                                        objNHRSBuildingAssDtl.DamageCd = 24;
                                                        damageLevelDtl = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/cladding_glazing/cladding_glazing_damage_level") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/non_structural_hazards/cladding_glazing/cladding_glazing_damage_level").Value) : null;
                                                        if (damageLevelDtl == "10" || damageLevelDtl == "11")
                                                        {
                                                            objNHRSBuildingAssDtl.DamageLevelCd = Convert.ToDecimal(damageLevelDtl);
                                                            qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                        }
                                                        else
                                                        {
                                                            dynamic arrDamageLevelDtl = damageLevelDtl.Split(' ');
                                                            foreach (var item in arrDamageLevelDtl)
                                                            {
                                                                objNHRSBuildingAssDtl.DamageLevelDtlCd = Convert.ToDecimal(item);
                                                                objNHRSBuildingAssDtl.DamageLevelCd = (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")) != "" ? (commFunction.GetValueFromDataBase(objNHRSBuildingAssDtl.DamageLevelDtlCd.ConvertToString(), "NHRS_DAMAGE_LEVEL_DTL", "DAMAGE_LEVEL_DTL_CD", "DAMAGE_LEVEL_CD")).ToDecimal() : null;
                                                                qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                            }
                                                        }
                                                    }
                                                    //if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment"))
                                                    //{
                                                    //    objNHRSBuildingAssDtl.DamageCd = 25;
                                                    //    objNHRSBuildingAssDtl.CommentEng = objNHRSBuildingAssDtl.CommentLoc = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/damage/general_comment").Value) : null;
                                                    //    qr = service.SubmitChanges(objNHRSBuildingAssDtl, true);
                                                    //}
                                                }
                                                #endregion
                                                #region Super Structure
                                                if (building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_2/super_structure"))
                                                {
                                                    string superStruct = String.Empty;
                                                    superStruct = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_2/super_structure") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/house_structure_2/super_structure").Value) : null;
                                                    if (superStruct.ConvertToString() != "")
                                                    {
                                                        dynamic arrSuperStruct = superStruct.Split(' ');
                                                        foreach (var item in arrSuperStruct)
                                                        {
                                                            objNHRSHhSuperStructMatInfo = new MigNhrsHhSuperstructureMatInfo();
                                                            objNHRSHhSuperStructMatInfo.HouseOwnerId = HouseOwnerIDInserted;
                                                            objNHRSHhSuperStructMatInfo.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;
                                                            objNHRSHhSuperStructMatInfo.SuperstructureMatId = Utils.ConvertToString(item) == "" ? null : Convert.ToInt32(Utils.ConvertToString(item));

                                                            if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                            {
                                                                string a = "";
                                                            }
                                                            objNHRSHhSuperStructMatInfo.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                            objNHRSHhSuperStructMatInfo.Approved = "N";
                                                            objNHRSHhSuperStructMatInfo.EnteredBy = SessionCheck.getSessionUsername();
                                                            objNHRSHhSuperStructMatInfo.EnteredDt = DateTime.Now;
                                                            objNHRSHhSuperStructMatInfo.IPAddress = CommonVariables.IPAddress;
                                                            objNHRSHhSuperStructMatInfo.BatchId = batchID;
                                                            objNHRSHhSuperStructMatInfo.Mode = mode;
                                                            qr = service.SubmitChanges(objNHRSHhSuperStructMatInfo, true);
                                                        }
                                                    }
                                                }
                                                #endregion
                                                #region Secondary Uses
                                                if (objNHRSBuildingAssMst.IsSecondaryUse.ConvertToString() != "" && objNHRSBuildingAssMst.IsSecondaryUse == "Y")
                                                {
                                                    string secUses = String.Empty;
                                                    secUses = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/secondary_use") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/secondary_use").Value) : null;
                                                    if (secUses.ConvertToString() != "")
                                                    {
                                                        dynamic arrSecUses = secUses.Split(' ');
                                                        foreach (var item in arrSecUses)
                                                        {
                                                            objNHRSSecondaryOccupancy = new MigNhrsBaSecOccupancyInfo();
                                                            objNHRSSecondaryOccupancy.HouseOwnerId = HouseOwnerIDInserted;
                                                            objNHRSSecondaryOccupancy.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;
                                                            objNHRSSecondaryOccupancy.SecOccupancyCd = Utils.ConvertToString(item) == "" ? null : Convert.ToInt32(Utils.ConvertToString(item));
                                                            if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                            {
                                                                string a = "";
                                                            }
                                                            objNHRSSecondaryOccupancy.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                            objNHRSSecondaryOccupancy.Approved = "N";
                                                            objNHRSSecondaryOccupancy.EnteredBy = SessionCheck.getSessionUsername();
                                                            objNHRSSecondaryOccupancy.EnteredDt = DateTime.Now;
                                                            objNHRSSecondaryOccupancy.IPAddress = CommonVariables.IPAddress;
                                                            objNHRSSecondaryOccupancy.BatchId = batchID;
                                                            objNHRSSecondaryOccupancy.Mode = mode;
                                                            qr = service.SubmitChanges(objNHRSSecondaryOccupancy, true);
                                                        }
                                                    }
                                                }
                                                #endregion
                                                #region Geo-Technical Risks
                                                if (objNHRSBuildingAssMst.IsGeotechnicalRisk.ConvertToString() != "" && objNHRSBuildingAssMst.IsGeotechnicalRisk == "Y")
                                                {
                                                    string geoRisks = String.Empty;
                                                    geoRisks = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/risk") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/risk").Value) : null;
                                                    if (geoRisks.ConvertToString() != "")
                                                    {
                                                        dynamic arrGeoRisks = geoRisks.Split(' ');
                                                        foreach (var item in arrGeoRisks)
                                                        {
                                                            objNHRSBaGeotechnicalRisk = new MigNhrsBaGeotechnicalRiskInfo();
                                                            objNHRSBaGeotechnicalRisk.HouseOwnerId = HouseOwnerIDInserted;
                                                            objNHRSBaGeotechnicalRisk.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;

                                                            objNHRSBaGeotechnicalRisk.GeotechnicalRiskTypeCd = Utils.ConvertToString(item) == "" ? null : Convert.ToInt32(Utils.ConvertToString(item));

                                                            objNHRSBaGeotechnicalRisk.OtherGeotechRiskEng = objNHRSBaGeotechnicalRisk.OtherGeotechRiskLoc = building.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/other_risk") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/other_risk").Value) : null;
                                                            objNHRSBaGeotechnicalRisk.Approved = "N";
                                                            objNHRSBaGeotechnicalRisk.EnteredBy = SessionCheck.getSessionUsername();
                                                            objNHRSBaGeotechnicalRisk.EnteredDt = DateTime.Now;
                                                            objNHRSBaGeotechnicalRisk.BatchId = batchID;
                                                            objNHRSBaGeotechnicalRisk.IPAddress = CommonVariables.IPAddress;
                                                            objNHRSBaGeotechnicalRisk.Mode = mode;
                                                            qr = service.SubmitChanges(objNHRSBaGeotechnicalRisk, true);
                                                        }
                                                    }
                                                }
                                                #endregion
                                                #region Families
                                                if (building.ContainsKey("Families"))
                                                {
                                                    foreach (SortedDictionary<dynamic, dynamic> family in building.First(s => s.Key == "Families").Value)
                                                    {
                                                        lstmember = new List<Dictionary<string, string>>();
                                                        #region Household Detail
                                                        objMisHouseholdInfo = new MigMisHouseholdInfoInfo();
                                                        objMisHouseholdInfo.HouseOwnerId = HouseOwnerIDInserted;
                                                        objMisHouseholdInfo.DefinedCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/hh_family_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/hh_family_number").Value) : null;
                                                        if (objMisHouseholdInfo.DefinedCd.ConvertToString() != "")
                                                        {
                                                            householdCnt++;
                                                            #region Insert Household Info
                                                            objMisHouseholdInfo.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;
                                                            if (family.ContainsKey("Members"))
                                                            {
                                                                foreach (SortedDictionary<dynamic, dynamic> member in family.First(s => s.Key == "Members").Value)
                                                                {
                                                                    decimal? relation = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/relation_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/relation_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/relation_hh_head").Value)) : null;
                                                                    if (relation == 1)
                                                                    {
                                                                        objMisHouseholdInfo.MemberDefinedCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count").Value) : null;
                                                                        objMisHouseholdInfo.MemberId = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count").Value) : null;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            if (objNHRSHouseOwnerMst.DistrictCd.ConvertToString() == "")
                                                            {
                                                                string aa = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                            }
                                                            objMisHouseholdInfo.PerDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                            objMisHouseholdInfo.PerVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                            objMisHouseholdInfo.PerWardNo = objNHRSHouseOwnerMst.WardNo;
                                                            objMisHouseholdInfo.PerAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                            objMisHouseholdInfo.PerAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                            objMisHouseholdInfo.CurDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                            objMisHouseholdInfo.CurVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                            objMisHouseholdInfo.CurWardNo = objNHRSHouseOwnerMst.WardNo;
                                                            objMisHouseholdInfo.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                            objMisHouseholdInfo.CurAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                            objMisHouseholdInfo.FirstNameEng = objMisHouseholdInfo.FirstNameLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info/hh_head_first_name") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info/hh_head_first_name").Value) : null;
                                                            objMisHouseholdInfo.MiddleNameEng = objMisHouseholdInfo.MiddleNameLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info/hh_head_middle_name") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info/hh_head_middle_name").Value) : null;
                                                            objMisHouseholdInfo.LastNameEng = objMisHouseholdInfo.LastNameLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info/hh_head_last_name") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info/hh_head_last_name").Value) : null;
                                                            objMisHouseholdInfo.FullNameEng = objMisHouseholdInfo.FullNameLoc = objMisHouseholdInfo.FirstNameEng.ConvertToString() + (objMisHouseholdInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisHouseholdInfo.MiddleNameEng) + " ") + objMisHouseholdInfo.LastNameEng.ConvertToString();
                                                            objMisHouseholdInfo.ShelterBeforeQuakeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_before_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_before_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_before_eq").Value)) : null;
                                                            objMisHouseholdInfo.ShelterSinceQuakeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_3/after_earthquake_residence") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_3/after_earthquake_residence").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_3/after_earthquake_residence").Value)) : null;
                                                            objMisHouseholdInfo.ShelterBeforeEqothDistrict = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_before_eq_other_district") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_before_eq_other_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_before_eq_other_district").Value)) : null;
                                                            objMisHouseholdInfo.CurrentShelterCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_now") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_now").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_now").Value)) : null;
                                                            objMisHouseholdInfo.CurrentShelterOthDistrict = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_now_other_district") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_now_other_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/residence_now_other_district").Value)) : null;
                                                            objMisHouseholdInfo.EqVictimIdentityCardCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_4/earthquake_victim_id") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_4/earthquake_victim_id").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_4/earthquake_victim_id").Value)) : null;
                                                            objMisHouseholdInfo.EqVictimIdentityCard = "Y";
                                                            objMisHouseholdInfo.EqVictimIdentityCardNo = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/earthquake_victim_id_info/earthquake_victim_id_no") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/earthquake_victim_id_info/earthquake_victim_id_no").Value) : null;
                                                            objMisHouseholdInfo.EqVictimIdcardPhotoFront = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/earthquake_victim_id_info/earthquake_victim_id_photo_front") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/earthquake_victim_id_info/earthquake_victim_id_photo_front").Value) : null;
                                                            objMisHouseholdInfo.EqVictimIdcardPhotoBack = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/earthquake_victim_id_info/earthquake_victim_id_photo_back") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/earthquake_victim_id_info/earthquake_victim_id_photo_back").Value) : null;
                                                            objMisHouseholdInfo.MonthlyIncomeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_5/monthly_income") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_5/monthly_income").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_5/monthly_income").Value)) : null;
                                                            objMisHouseholdInfo.StudentSchoolLeft = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/children_quit_school") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/children_quit_school").Value) == "1" ? "YS" : (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/children_quit_school").Value) == "2" ? "NO" : "NE")) : null;
                                                            objMisHouseholdInfo.StudentSchoolLeftCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/children_quit_school_count") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/children_quit_school_count").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/children_quit_school_count").Value)) : null;
                                                            objMisHouseholdInfo.PregnantRegularCheckup = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_checkup_pregnant_women") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_checkup_pregnant_women").Value) == "1" ? "YS" : (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_checkup_pregnant_women").Value) == "2" ? "NO" : "NE")) : null;
                                                            objMisHouseholdInfo.PregnantRegularCheckupCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_checkup_pregnant_women_count") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_checkup_pregnant_women_count").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_checkup_pregnant_women_count").Value)) : null;
                                                            objMisHouseholdInfo.ChildLeftVacination = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_vaccination_of_child") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_vaccination_of_child").Value) == "1" ? "YS" : (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_vaccination_of_child").Value) == "2" ? "NO" : "NE")) : null;
                                                            objMisHouseholdInfo.ChildLeftVacinationCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_vaccination_of_child_count") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_vaccination_of_child_count").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_vaccination_of_child_count").Value)) : null;
                                                            objMisHouseholdInfo.LeftChangeOccupany = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_job") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_job").Value) == "1" ? "YS" : (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_in_household/quit_job").Value) == "2" ? "NO" : "NE")) : null;
                                                            objMisHouseholdInfo.LeftChangeOccupanyCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_job_count") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_job_count").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/change_count/quit_job_count").Value)) : null;
                                                            objMisHouseholdInfo.RespondentIsHhHead = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info_2/hh_head_respondent") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_info_2/hh_head_respondent").Value) == "1" ? "Y" : "N") : "N";
                                                            if (objMisHouseholdInfo.RespondentIsHhHead.ConvertToString() == "Y")
                                                            {
                                                                objMisHouseholdInfo.RespondentFirstName = objMisHouseholdInfo.RespondentFirstNameLoc = objMisHouseholdInfo.FirstNameEng;
                                                                objMisHouseholdInfo.RespondentMiddleName = objMisHouseholdInfo.RespondentMiddleNameLoc = objMisHouseholdInfo.MiddleNameEng;
                                                                objMisHouseholdInfo.RespondentLastName = objMisHouseholdInfo.RespondentLastNameLoc = objMisHouseholdInfo.LastNameEng;
                                                                objMisHouseholdInfo.HhRelationTypeCd = 1;
                                                            }
                                                            else
                                                            {
                                                                objMisHouseholdInfo.RespondentFirstName = objMisHouseholdInfo.RespondentFirstNameLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/hh_respondent_first_name") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/hh_respondent_first_name").Value) : null;
                                                                objMisHouseholdInfo.RespondentMiddleName = objMisHouseholdInfo.RespondentMiddleNameLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/hh_respondent_middle_name") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/hh_respondent_middle_name").Value) : null;
                                                                objMisHouseholdInfo.RespondentLastName = objMisHouseholdInfo.RespondentLastNameLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/hh_respondent_last_name") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/hh_respondent_last_name").Value) : null;
                                                                objMisHouseholdInfo.HhRelationTypeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/respondent_realtion") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/respondent_realtion").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/respodent_detail/respondent_realtion").Value)) : null;
                                                            }
                                                            objMisHouseholdInfo.RespondentFullName = objMisHouseholdInfo.RespondentFullNameLoc = objMisHouseholdInfo.RespondentFirstName.ConvertToString() + (objMisHouseholdInfo.RespondentMiddleName.ConvertToString() == "" ? " " : (" " + objMisHouseholdInfo.RespondentMiddleName) + " ") + objMisHouseholdInfo.RespondentLastName.ConvertToString();
                                                            objMisHouseholdInfo.RespondentPhoto = family.ContainsKey("building_damage_assessment/hh_data/respondent_photo") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/respondent_photo").Value) : null;
                                                            //GENDER CODE NOT PROVIDED
                                                            objMisHouseholdInfo.MemberCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_1/no_member") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_1/no_member").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_1/no_member").Value)) : 0;
                                                            objMisHouseholdInfo.DeathInAYear = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/member_death") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/member_death").Value) == "1" ? "Y" : "N") : "N";
                                                            objMisHouseholdInfo.DeathCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/death_description_info/death_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/death_description_info/death_number").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/death_description_info/death_number").Value)) : 0;
                                                            objMisHouseholdInfo.HumanDestroyFlag = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction_due_to_earthquake") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction_due_to_earthquake").Value) == "1" ? "Y" : "N") : "N";
                                                            objMisHouseholdInfo.HumanDestroyCnt = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destruction_detail/destruction_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destruction_detail/destruction_number").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destruction_detail/destruction_number").Value)) : 0;
                                                            objMisHouseholdInfo.HouseholdActive = "Y";
                                                            objMisHouseholdInfo.ChildInSchool = "N";
                                                            objMisHouseholdInfo.SocialAllowance = "N";
                                                            objMisHouseholdInfo.Approved = "N";
                                                            objMisHouseholdInfo.EnteredBy = SessionCheck.getSessionUsername();
                                                            objMisHouseholdInfo.EnteredDt = DateTime.Now;
                                                            objMisHouseholdInfo.IPAddress = CommonVariables.IPAddress;
                                                            objMisHouseholdInfo.BatchId = batchID;
                                                            objMisHouseholdInfo.HoDefinedCd = objNHRSHouseOwnerMst.DefinedCd;
                                                            objMisHouseholdInfo.InstanceUniqueSno = family.ContainsKey("idsl_no") ? Utils.ConvertToString(family.First(s => s.Key == "idsl_no").Value) : null;

                                                            //Facilities Table Here
                                                            //Insert Household Detail Here
                                                            objMisHouseholdInfo.Mode = mode;
                                                            service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                            qr = service.SubmitChanges(objMisHouseholdInfo, true);
                                                            //get HouseholdID
                                                            HouseholdID = qr["V_HOUSEHOLD_ID"].ConvertToString();
                                                            if (!string.IsNullOrEmpty(HouseholdID))
                                                            {
                                                            #endregion
                                                                #region Water Source Detail
                                                                decimal? waterSourceBeforeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/water_sources/water_sources_before_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/water_sources/water_sources_before_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/water_sources/water_sources_before_eq").Value)) : null;
                                                                decimal? waterSourceAfterCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/water_sources/water_sources_after_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/water_sources/water_sources_after_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/water_sources/water_sources_after_eq").Value)) : null;
                                                                if (waterSourceBeforeCd.ConvertToString() != "" && waterSourceAfterCd.ConvertToString() != "")
                                                                {
                                                                    objWaterSource = new MigNhrsHhWatersourceDtlInfo();
                                                                    objWaterSource.HouseholdId = HouseholdID;
                                                                    objWaterSource.HouseOwnerId = HouseOwnerIDInserted;
                                                                    objWaterSource.Approved = "N";
                                                                    objWaterSource.EnteredBy = SessionCheck.getSessionUsername();
                                                                    objWaterSource.EnteredDt = DateTime.Now;
                                                                    objWaterSource.IPAddress = CommonVariables.IPAddress;
                                                                    objWaterSource.BatchId = batchID;
                                                                    objWaterSource.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                    if (waterSourceBeforeCd == waterSourceAfterCd)
                                                                    {
                                                                        objWaterSource.WaterSourceCd = waterSourceBeforeCd;
                                                                        objWaterSource.WaterSourceBefore = objWaterSource.WaterSourceAfter = "Y";
                                                                        qr = service.SubmitChanges(objWaterSource, true);
                                                                    }
                                                                    else
                                                                    {
                                                                        objWaterSource.WaterSourceCd = waterSourceBeforeCd;
                                                                        objWaterSource.WaterSourceAfter = "N";
                                                                        objWaterSource.WaterSourceBefore = "Y";
                                                                        qr = service.SubmitChanges(objWaterSource, true);
                                                                        if (qr.IsSuccess)
                                                                        {
                                                                            objWaterSource.WaterSourceCd = waterSourceAfterCd;
                                                                            objWaterSource.WaterSourceAfter = "Y";
                                                                            objWaterSource.WaterSourceBefore = "N";
                                                                            qr = service.SubmitChanges(objWaterSource, true);
                                                                        }
                                                                    }
                                                                }
                                                                #endregion
                                                                #region Fuel Source Detail
                                                                decimal? fuelSourceBeforeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/cooking_sources/cooking_sources_before_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/cooking_sources/cooking_sources_before_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/cooking_sources/cooking_sources_before_eq").Value)) : null;
                                                                decimal? fuelSourceAfterCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/cooking_sources/cooking_sources_after_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/cooking_sources/cooking_sources_after_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/cooking_sources/cooking_sources_after_eq").Value)) : null;
                                                                if (fuelSourceBeforeCd.ConvertToString() != "" && fuelSourceAfterCd.ConvertToString() != "")
                                                                {
                                                                    objFuelSource = new MigNhrsHhFuelsourceDtlInfo();
                                                                    objFuelSource.HouseholdId = HouseholdID;
                                                                    objFuelSource.HouseOwnerId = HouseOwnerIDInserted;
                                                                    objFuelSource.Approved = "N";
                                                                    objFuelSource.EnteredBy = SessionCheck.getSessionUsername();
                                                                    objFuelSource.EnteredDt = DateTime.Now;
                                                                    objFuelSource.IPAddress = CommonVariables.IPAddress;
                                                                    objFuelSource.BatchId = batchID;
                                                                    objFuelSource.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                    if (fuelSourceBeforeCd == fuelSourceAfterCd)
                                                                    {
                                                                        objFuelSource.FuelSourceCd = fuelSourceBeforeCd;
                                                                        objFuelSource.FuelSourceBefore = objFuelSource.FuelSourceAfter = "Y";
                                                                        qr = service.SubmitChanges(objFuelSource, true);
                                                                    }
                                                                    else
                                                                    {
                                                                        objFuelSource.FuelSourceCd = fuelSourceBeforeCd;
                                                                        objFuelSource.FuelSourceAfter = "N";
                                                                        objFuelSource.FuelSourceBefore = "Y";
                                                                        qr = service.SubmitChanges(objFuelSource, true);
                                                                        if (qr.IsSuccess)
                                                                        {
                                                                            objFuelSource.FuelSourceCd = fuelSourceAfterCd;
                                                                            objFuelSource.FuelSourceAfter = "Y";
                                                                            objFuelSource.FuelSourceBefore = "N";
                                                                            qr = service.SubmitChanges(objFuelSource, true);
                                                                        }
                                                                    }
                                                                }
                                                                #endregion
                                                                #region Light Source Detail
                                                                decimal? LightSourceBeforeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/light_sources/light_sources_before_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/light_sources/light_sources_before_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/light_sources/light_sources_before_eq").Value)) : null;
                                                                decimal? LightSourceAfterCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/light_sources/light_sources_after_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/light_sources/light_sources_after_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/light_sources/light_sources_after_eq").Value)) : null;
                                                                if (LightSourceBeforeCd.ConvertToString() != "" && LightSourceAfterCd.ConvertToString() != "")
                                                                {
                                                                    objLightSource = new MigNhrsHhLightsourceDtlInfo();
                                                                    objLightSource.HouseholdId = HouseholdID;
                                                                    objLightSource.HouseOwnerId = HouseOwnerIDInserted;
                                                                    objLightSource.Approved = "N";
                                                                    objLightSource.EnteredBy = SessionCheck.getSessionUsername();
                                                                    objLightSource.EnteredDt = DateTime.Now;
                                                                    objLightSource.IPAddress = CommonVariables.IPAddress;
                                                                    objLightSource.BatchId = batchID;
                                                                    objLightSource.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                    if (LightSourceBeforeCd == LightSourceAfterCd)
                                                                    {
                                                                        objLightSource.LightSourceCd = LightSourceBeforeCd;
                                                                        objLightSource.LightSourceBefore = objLightSource.LightSourceAfter = "Y";
                                                                        qr = service.SubmitChanges(objLightSource, true);
                                                                    }
                                                                    else
                                                                    {
                                                                        objLightSource.LightSourceCd = LightSourceBeforeCd;
                                                                        objLightSource.LightSourceAfter = "N";
                                                                        objLightSource.LightSourceBefore = "Y";
                                                                        qr = service.SubmitChanges(objLightSource, true);
                                                                        if (qr.IsSuccess)
                                                                        {
                                                                            objLightSource.LightSourceCd = LightSourceAfterCd;
                                                                            objLightSource.LightSourceAfter = "Y";
                                                                            objLightSource.LightSourceBefore = "N";
                                                                            qr = service.SubmitChanges(objLightSource, true);
                                                                        }
                                                                    }
                                                                }
                                                                #endregion
                                                                #region Toilet Type Detail
                                                                decimal? ToiletTypeBeforeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/toilets/toilet_before_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/toilets/toilet_before_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/toilets/toilet_before_eq").Value)) : null;
                                                                decimal? ToiletTypeAfterCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/toilets/toilet_after_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/toilets/toilet_after_eq").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/sources/toilets/toilet_after_eq").Value)) : null;
                                                                if (ToiletTypeBeforeCd.ConvertToString() != "" && ToiletTypeAfterCd.ConvertToString() != "")
                                                                {
                                                                    objToiletType = new MigNhrsHhToiletTypeInfo();
                                                                    objToiletType.HouseholdId = HouseholdID;
                                                                    objToiletType.HouseOwnerId = HouseOwnerIDInserted;
                                                                    objToiletType.Approved = "N";
                                                                    objToiletType.EnteredBy = SessionCheck.getSessionUsername();
                                                                    objToiletType.EnteredDt = DateTime.Now;
                                                                    objToiletType.IPAddress = CommonVariables.IPAddress;
                                                                    objToiletType.BatchId = batchID;
                                                                    objToiletType.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                    if (ToiletTypeBeforeCd == ToiletTypeAfterCd)
                                                                    {
                                                                        objToiletType.ToiletTypeCd = ToiletTypeBeforeCd;
                                                                        objToiletType.ToiletTypeBefore = objToiletType.ToiletTypeAfter = "Y";
                                                                        qr = service.SubmitChanges(objToiletType, true);
                                                                    }
                                                                    else
                                                                    {
                                                                        objToiletType.ToiletTypeCd = ToiletTypeBeforeCd;
                                                                        objToiletType.ToiletTypeAfter = "N";
                                                                        objToiletType.ToiletTypeBefore = "Y";
                                                                        qr = service.SubmitChanges(objToiletType, true);
                                                                        if (qr.IsSuccess)
                                                                        {
                                                                            objToiletType.ToiletTypeCd = ToiletTypeAfterCd;
                                                                            objToiletType.ToiletTypeAfter = "Y";
                                                                            objToiletType.ToiletTypeBefore = "N";
                                                                            qr = service.SubmitChanges(objToiletType, true);
                                                                        }
                                                                    }
                                                                }
                                                                #endregion
                                                                #region Facilities Detail
                                                                string facilitiesBeforeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/facilities/facilities_before_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/facilities/facilities_before_eq").Value) : null;
                                                                string facilitiesAfterCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/facilities/facilities_after_eq") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/facilities/facilities_after_eq").Value) : null;
                                                                if (facilitiesBeforeCd.ConvertToString() != "" && facilitiesAfterCd.ConvertToString() != "")
                                                                {
                                                                    objIndicatorDtl = new MigNhrsHhIndicatorDtlInfo();
                                                                    objIndicatorDtl.HouseholdId = HouseholdID;
                                                                    objIndicatorDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                    objIndicatorDtl.Approved = "N";
                                                                    objIndicatorDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                    objIndicatorDtl.EnteredDt = DateTime.Now;
                                                                    objIndicatorDtl.IPAddress = CommonVariables.IPAddress;
                                                                    objIndicatorDtl.BatchId = batchID;
                                                                    objIndicatorDtl.Mode = mode;
                                                                    service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                    List<string> arrFacilitiesBefore = facilitiesBeforeCd.Split(' ').ToList();
                                                                    List<string> arrFacilitiesAfter = facilitiesAfterCd.Split(' ').ToList();
                                                                    List<string> arrCommon = arrFacilitiesBefore.Intersect(arrFacilitiesAfter).ToList();
                                                                    List<string> arrUncommonBefore = arrFacilitiesBefore.Except(arrCommon).ToList();
                                                                    List<string> arrUncommonAfter = arrFacilitiesAfter.Except(arrCommon).ToList();
                                                                    foreach (var item in arrCommon)
                                                                    {
                                                                        objIndicatorDtl.IndicatorCd = Convert.ToDecimal(item);
                                                                        objIndicatorDtl.IndicatorBefore = objIndicatorDtl.IndicatorAfter = "Y";
                                                                        qr = service.SubmitChanges(objIndicatorDtl, true);
                                                                    }
                                                                    foreach (var item in arrUncommonBefore)
                                                                    {
                                                                        objIndicatorDtl.IndicatorCd = Convert.ToDecimal(item);
                                                                        objIndicatorDtl.IndicatorBefore = "Y";
                                                                        objIndicatorDtl.IndicatorAfter = "N";
                                                                        qr = service.SubmitChanges(objIndicatorDtl, true);
                                                                    }
                                                                    foreach (var item in arrUncommonAfter)
                                                                    {
                                                                        objIndicatorDtl.IndicatorCd = Convert.ToDecimal(item);
                                                                        objIndicatorDtl.IndicatorBefore = "N";
                                                                        objIndicatorDtl.IndicatorAfter = "Y";
                                                                        qr = service.SubmitChanges(objIndicatorDtl, true);
                                                                    }
                                                                }
                                                                #endregion
                                                                #region Earthquake Relief Money
                                                                string eqReliefMoney = String.Empty;
                                                                eqReliefMoney = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_4/earthquake_relief_money") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_4/earthquake_relief_money").Value) : null;
                                                                if (eqReliefMoney.ConvertToString() != "")
                                                                {
                                                                    dynamic arrEqReliefMoney = eqReliefMoney.Split(' ');
                                                                    foreach (var item in arrEqReliefMoney)
                                                                    {
                                                                        objMigNhrsRcvdRelief = new MigNhrsRcvdEqReliefMoneyInfo();
                                                                        objMigNhrsRcvdRelief.HouseholdId = HouseholdID;
                                                                        objMigNhrsRcvdRelief.HouseOwnerId = HouseOwnerIDInserted;
                                                                        objMigNhrsRcvdRelief.EqReliefMoneyCd = Convert.ToDecimal(item);
                                                                        objMigNhrsRcvdRelief.Approved = "N";
                                                                        objMigNhrsRcvdRelief.EnteredBy = SessionCheck.getSessionUsername();
                                                                        objMigNhrsRcvdRelief.EnteredDt = DateTime.Now;
                                                                        objMigNhrsRcvdRelief.IPAddress = CommonVariables.IPAddress;
                                                                        objMigNhrsRcvdRelief.BatchId = batchID;
                                                                        objMigNhrsRcvdRelief.Mode = mode;
                                                                        service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                        qr = service.SubmitChanges(objMigNhrsRcvdRelief, true);
                                                                    }
                                                                }
                                                                #endregion
                                                                if (family.ContainsKey("Members"))
                                                                {
                                                                    #region Repeat Members
                                                                    int i = 0;
                                                                    foreach (SortedDictionary<dynamic, dynamic> member in family.First(s => s.Key == "Members").Value)
                                                                    {
                                                                        if (i == 0)
                                                                        {
                                                                            memberCnt++;
                                                                            #region Household Head Data
                                                                            objMisMemberInfo = new MigMisMemberInfo();
                                                                            objMisMemberInfo.HouseOwnerId = HouseOwnerIDInserted;
                                                                            objMisMemberInfo.HouseholdId = HouseholdID;
                                                                            objMisMemberInfo.HouseholdDefinedCd = objMisHouseholdInfo.DefinedCd;
                                                                            objMisMemberInfo.DefinedCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count").Value) : null;
                                                                            if (objMisMemberInfo.DefinedCd.ConvertToString() != "")
                                                                            {
                                                                                objMisMemberInfo.FirstNameEng = objMisMemberInfo.FirstNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/first_name_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/first_name_hh_head").Value) : null;
                                                                                objMisMemberInfo.MiddleNameEng = objMisMemberInfo.MiddleNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/middle_name_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/middle_name_hh_head").Value) : null;
                                                                                objMisMemberInfo.LastNameEng = objMisMemberInfo.LastNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/last_name_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/last_name_hh_head").Value) : null;
                                                                                objMisMemberInfo.FullNameEng = objMisMemberInfo.FullNameLoc = objMisMemberInfo.FirstNameEng.ConvertToString() + (objMisMemberInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisMemberInfo.MiddleNameEng) + " ") + objMisMemberInfo.LastNameEng.ConvertToString();
                                                                                objMisMemberInfo.GenderCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/gender_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/gender_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/gender_hh_head").Value)) : null;
                                                                                //objMisMemberInfo.BirthDt = family.ContainsKey("today") ? Utils.ConvertToString(family.First(s => s.Key == "today").Value) == "" ? null : Convert.ToDateTime(Utils.ConvertToString(family.First(s => s.Key == "today").Value)) : null;
                                                                                objMisMemberInfo.BirthYearLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/year_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/year_hh_head").Value) : "9999";
                                                                                objMisMemberInfo.BirthMonthLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/month_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/month_hh_head").Value) : "99";
                                                                                objMisMemberInfo.BirthDayLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/day_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/day_hh_head").Value) : "99";
                                                                                objMisMemberInfo.BirthYear = 9999;
                                                                                objMisMemberInfo.BirthMonth = 99;
                                                                                objMisMemberInfo.BirthDay = 99;
                                                                                //birthLoc
                                                                                objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_calculated_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_calculated_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_calculated_hh_head").Value)) : null;
                                                                                if (objMisMemberInfo.Age.ConvertToString() == "")
                                                                                {
                                                                                    objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/age_hh_head").Value)) : null;
                                                                                }
                                                                                objMisMemberInfo.IndentificationTypeCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_identification") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_identification").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/hh_head_identification").Value)) : null;
                                                                                objMisMemberInfo.MemberActive = "Y";
                                                                                objMisMemberInfo.Disability = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_head_info_7/hh_head_disabled") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_head_info_7/hh_head_disabled").Value) == "1" ? "Y" : "N") : "N";
                                                                                objMisMemberInfo.CtzIssue = (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "" && objMisMemberInfo.IndentificationTypeCd.ConvertToString() == "1") ? "Y" : "N";
                                                                                //Driving Licence doesnot have corresponding flag
                                                                                objMisMemberInfo.VoterId = (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "" && objMisMemberInfo.IndentificationTypeCd.ConvertToString() == "3") ? "Y" : "N";
                                                                                objMisMemberInfo.SocialAllowance = (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "" && objMisMemberInfo.IndentificationTypeCd.ConvertToString() == "4") ? "Y" : "N";
                                                                                if (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "")
                                                                                {
                                                                                    switch (objMisMemberInfo.IndentificationTypeCd.ConvertToString())
                                                                                    {
                                                                                        case "1":
                                                                                            objMisMemberInfo.CitNo = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number").Value) : null;
                                                                                            objMisMemberInfo.CtzIssueDistrictCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value)) : null;
                                                                                            break;
                                                                                        case "2":
                                                                                            objMisMemberInfo.DrivingLicenseNo = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number").Value) : null;
                                                                                            objMisMemberInfo.DrivingLicIssDistrict = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value)) : null;
                                                                                            break;
                                                                                        case "3":
                                                                                            objMisMemberInfo.VoteridNo = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number").Value) : null;
                                                                                            objMisMemberInfo.VoteridDistrictCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value)) : null;
                                                                                            break;
                                                                                        case "4":
                                                                                            objMisMemberInfo.SocialAllowanceId = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_number").Value) : null;
                                                                                            objMisMemberInfo.SocialAllIssDistrict = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_issue_district").Value)) : null;
                                                                                            break;
                                                                                        case "6":
                                                                                            objMisMemberInfo.IndentificationOthers = "";
                                                                                            objMisMemberInfo.IndentificationOthersLoc = "";
                                                                                            break;
                                                                                        default:
                                                                                            break;
                                                                                    }
                                                                                    objMisMemberInfo.MemberPhotoId = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_photo") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_id_info/hh_head_id_photo").Value) : null;
                                                                                }
                                                                                //hh_head_photo
                                                                                objMisMemberInfo.CasteCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value)) : null;
                                                                                objMisMemberInfo.EducationCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/education_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/education_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/education_hh_head").Value)) : null;
                                                                                objMisMemberInfo.BankAccountFlag = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_acc") ? (Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_acc").Value) == "1" ? "Y" : "N") : "N";
                                                                                //BANK CODE NOT IN DECIMAL SO COMMENTED TILL CORRECTION
                                                                                //objMisMemberInfo.BankCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_info/hh_head_bankname") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_info/hh_head_bankname").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_bank_info/hh_head_bankname").Value)) : null;
                                                                                objMisMemberInfo.TelNo = objMisMemberInfo.MobileNo = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_5/hh_head_contact") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_info_5/hh_head_contact").Value) : null;
                                                                                objMisMemberInfo.Death = "N";
                                                                                objMisMemberInfo.MaritalStatusCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_marital_status") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_marital_status").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_marital_status").Value)) : 99;
                                                                                objMisMemberInfo.Literate = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/education_hh_head") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/education_hh_head").Value) != "" ? "Y" : "N") : "N";
                                                                                objMisMemberInfo.BirthCertificate = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/memeber_birth_registered") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/memeber_birth_registered").Value) != "" ? "Y" : "N") : "N";
                                                                                objMisMemberInfo.PidNo = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid").Value) : null;

                                                                                if (objNHRSHouseOwnerMst.DistrictCd.ConvertToString() == "")
                                                                                {
                                                                                    string aa = objNHRSHouseOwnerMst.DistrictCd.ConvertToString();
                                                                                }
                                                                                objMisMemberInfo.PerDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                                                objMisMemberInfo.PerVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                                                objMisMemberInfo.PerWardNo = objNHRSHouseOwnerMst.WardNo;
                                                                                objMisMemberInfo.PerAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                                objMisMemberInfo.PerAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                                                objMisMemberInfo.CurDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                                                objMisMemberInfo.CurVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                                                objMisMemberInfo.CurWardNo = objNHRSHouseOwnerMst.WardNo;
                                                                                objMisMemberInfo.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                                objMisMemberInfo.CurAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                                                objMisMemberInfo.BuildstructNo = objNHRSBuildingAssMst.BuildingStructureNo;
                                                                                objMisMemberInfo.HouseholdHead = "Y";
                                                                                objMisMemberInfo.HoDefinedCd = objNHRSHouseOwnerMst.DefinedCd;
                                                                                objMisMemberInfo.InstanceUniqueSno = objMisHouseholdInfo.InstanceUniqueSno;
                                                                                objMisMemberInfo.Approved = "N";
                                                                                objMisMemberInfo.EnteredBy = SessionCheck.getSessionUsername();
                                                                                objMisMemberInfo.EnteredDt = DateTime.Now;
                                                                                //SAVE HOUSEHOLD HEAD(MEMBER) DETAIL HERE
                                                                                objMisMemberInfo.IPAddress = CommonVariables.IPAddress;
                                                                                objMisMemberInfo.BatchId = batchID;
                                                                                objMisMemberInfo.Mode = mode;
                                                                                service.PackageName = "MIGNHRS.PKG_MEMBER";
                                                                                qr = service.SubmitChanges(objMisMemberInfo, true);
                                                                                MemberID = qr["V_MEMBER_ID"].ConvertToString();

                                                                                i++;
                                                                                //#region Household Head Photo
                                                                                //if (qr.IsSuccess)
                                                                                //{
                                                                                //    objMisMemberDoc = new MigMisMemberDocInfo();
                                                                                //    objMisMemberDoc.DocTypeCd = 11;
                                                                                //    objMisMemberDoc.MemberId = MemberID;
                                                                                //    objMisMemberDoc.DocId = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_photo") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_photo").Value) : null;
                                                                                //    objMisMemberDoc.Approved = "N";
                                                                                //    objMisMemberDoc.EnteredBy = SessionCheck.getSessionUsername();
                                                                                //    objMisMemberDoc.EnteredDt = DateTime.Now;
                                                                                //    objMisMemberDoc.IPAddress = CommonVariables.IPAddress;
                                                                                //    objMisMemberDoc.BatchId = batchID;
                                                                                //    objMisMemberDoc.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                                //    objMisMemberDoc.Mode = mode;
                                                                                //    service.PackageName = "MIGNHRS.PKG_MEMBER";
                                                                                //    qr = service.SubmitChanges(objMisMemberDoc, true);
                                                                                //}
                                                                                //#endregion
                                                                                #region Allowance Detail
                                                                                if (qr.IsSuccess)
                                                                                {
                                                                                    string allowances = String.Empty;
                                                                                    allowances = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_allowances") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_allowances").Value) : null;
                                                                                    if (allowances.ConvertToString() != "")
                                                                                    {
                                                                                        dynamic arrAllowances = allowances.Split(' ');
                                                                                        foreach (var item in arrAllowances)
                                                                                        {
                                                                                            objMisHHAllowanceDtl = new MigMisHhAllowanceDtlInfo();
                                                                                            objMisHHAllowanceDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                                            objMisHHAllowanceDtl.BuildingStructureNo = objNHRSBuildingAssMst.BuildingStructureNo;
                                                                                            objMisHHAllowanceDtl.HouseholdId = HouseholdID;
                                                                                            objMisHHAllowanceDtl.MemberId = MemberID;
                                                                                            objMisHHAllowanceDtl.AllowanceTypeCd = Convert.ToDecimal(item);
                                                                                            objMisHHAllowanceDtl.AllowanceDay = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_day") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_day").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_day").Value)) : 99;
                                                                                            objMisHHAllowanceDtl.AllowanceMonth = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_month") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_month").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_month").Value)) : 99;
                                                                                            objMisHHAllowanceDtl.AllowanceYears = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_year") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_year").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_info_if_presence/hh_head_date_allowance/hh_head_date_allowance_year").Value)) : 99;
                                                                                            objMisHHAllowanceDtl.Approved = "N";
                                                                                            objMisHHAllowanceDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                                            objMisHHAllowanceDtl.EnteredDt = DateTime.Now;
                                                                                            objMisHHAllowanceDtl.IPAddress = CommonVariables.IPAddress;
                                                                                            objMisHHAllowanceDtl.BatchId = batchID;
                                                                                            objMisHHAllowanceDtl.Mode = mode;
                                                                                            service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                                            qr = service.SubmitChanges(objMisHHAllowanceDtl, true);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                #endregion
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        else
                                                                        {
                                                                            #region Other Members
                                                                            objMisMemberInfo = new MigMisMemberInfo();
                                                                            objMisMemberInfo.HouseOwnerId = HouseOwnerIDInserted;
                                                                            objMisMemberInfo.HouseholdId = HouseholdID;
                                                                            objMisMemberInfo.DefinedCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_count").Value) : null;
                                                                            if (objMisMemberInfo.DefinedCd.ConvertToString() != "")
                                                                            {
                                                                                memberCnt++;
                                                                                objMisMemberInfo.FirstNameEng = objMisMemberInfo.FirstNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_first_name") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_first_name").Value) : null;
                                                                                objMisMemberInfo.LastNameEng = objMisMemberInfo.LastNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_last_name") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_last_name").Value) : null;
                                                                                objMisMemberInfo.MiddleNameEng = objMisMemberInfo.MiddleNameLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_middle_name") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_name/member_middle_name").Value) : null;
                                                                                objMisMemberInfo.FullNameEng = objMisMemberInfo.FullNameLoc = objMisMemberInfo.FirstNameEng.ConvertToString() + (objMisMemberInfo.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisMemberInfo.MiddleNameEng) + " ") + objMisMemberInfo.LastNameEng.ConvertToString();
                                                                                objMisMemberInfo.GenderCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_gender") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_gender").Value)) : null;
                                                                                objMisMemberInfo.BirthYearLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_year") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_year").Value) : "9999";
                                                                                objMisMemberInfo.BirthMonthLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_month") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_month").Value) : "99";
                                                                                objMisMemberInfo.BirthDayLoc = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_day") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_dob_day").Value) : "99";
                                                                                objMisMemberInfo.BirthYear = 9999;
                                                                                objMisMemberInfo.BirthMonth = 99;
                                                                                objMisMemberInfo.BirthDay = 99;
                                                                                //birthLoc
                                                                                objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_age_calculated") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_age_calculated").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_dob/member_age_calculated").Value)) : null;
                                                                                if (objMisMemberInfo.Age.ConvertToString() == "")
                                                                                {
                                                                                    objMisMemberInfo.Age = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_age") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_age").Value)) : null;
                                                                                }
                                                                                objMisMemberInfo.MemberActive = "Y";
                                                                                objMisMemberInfo.Disability = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/member_disable") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/member_disable").Value) == "1" ? "Y" : "N") : "N";
                                                                                objMisMemberInfo.CtzIssue = (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "" && objMisMemberInfo.IndentificationTypeCd.ConvertToString() == "1") ? "Y" : "N";
                                                                                //Driving Licence doesnot have corresponding flag
                                                                                objMisMemberInfo.VoterId = (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "" && objMisMemberInfo.IndentificationTypeCd.ConvertToString() == "3") ? "Y" : "N";
                                                                                objMisMemberInfo.SocialAllowance = (objMisMemberInfo.IndentificationTypeCd.ConvertToString() != "" && objMisMemberInfo.IndentificationTypeCd.ConvertToString() == "4") ? "Y" : "N";
                                                                                objMisMemberInfo.ForeignHouseheadCountryEng = objMisMemberInfo.ForeignHouseheadCountryLoc = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/country_if_foreign") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/country_if_foreign").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/country_if_foreign").Value)) : null;
                                                                                objMisMemberInfo.PidNo = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/pid").Value) : null;
                                                                                objMisMemberInfo.CasteCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value)) : null;
                                                                                objMisMemberInfo.EducationCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education").Value)) : null;
                                                                                objMisMemberInfo.BankAccountFlag = "N";
                                                                                objMisMemberInfo.Death = "N";
                                                                                objMisMemberInfo.MaritalStatusCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_marrital_status") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_marrital_status").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_marrital_status").Value)) : 99;
                                                                                objMisMemberInfo.Literate = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_4/member_education").Value) != "" ? "Y" : "N") : "N";
                                                                                objMisMemberInfo.BirthCertificate = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/memeber_birth_registered") ? (Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/other_member_info_3/memeber_birth_registered").Value) != "" ? "Y" : "N") : "N";
                                                                                objMisMemberInfo.PerDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                                                objMisMemberInfo.PerVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                                                objMisMemberInfo.PerWardNo = objNHRSHouseOwnerMst.WardNo;
                                                                                objMisMemberInfo.PerAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                                objMisMemberInfo.PerAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                                                objMisMemberInfo.CurDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                                                objMisMemberInfo.CurVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                                                objMisMemberInfo.CurWardNo = objNHRSHouseOwnerMst.WardNo;
                                                                                objMisMemberInfo.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                                objMisMemberInfo.CurAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                                                objMisMemberInfo.HouseholdHead = "N";
                                                                                objMisMemberInfo.Approved = "N";
                                                                                objMisMemberInfo.EnteredBy = SessionCheck.getSessionUsername();
                                                                                objMisMemberInfo.EnteredDt = DateTime.Now;
                                                                                objMisMemberInfo.HoDefinedCd = objNHRSHouseOwnerMst.DefinedCd;
                                                                                objMisMemberInfo.InstanceUniqueSno = objMisHouseholdInfo.InstanceUniqueSno;
                                                                                //SAVE MEMBER DETAIL HERE
                                                                                objMisMemberInfo.IPAddress = CommonVariables.IPAddress;
                                                                                objMisMemberInfo.BatchId = batchID;
                                                                                objMisMemberInfo.Mode = mode;
                                                                                service.PackageName = "MIGNHRS.PKG_MEMBER";
                                                                                qr = service.SubmitChanges(objMisMemberInfo, true);
                                                                                MemberID = qr["V_MEMBER_ID"].ConvertToString();
                                                                                //get Member ID
                                                                                #region Allowance Detail
                                                                                if (qr.IsSuccess)
                                                                                {
                                                                                    string allowances = String.Empty;
                                                                                    allowances = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_allowances") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_allowances").Value) : null;
                                                                                    if (allowances.ConvertToString() != "")
                                                                                    {
                                                                                        dynamic arrAllowances = allowances.Split(' ');
                                                                                        foreach (var item in arrAllowances)
                                                                                        {
                                                                                            objMisHHAllowanceDtl = new MigMisHhAllowanceDtlInfo();
                                                                                            objMisHHAllowanceDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                                            objMisHHAllowanceDtl.BuildingStructureNo = objNHRSBuildingAssMst.BuildingStructureNo;
                                                                                            objMisHHAllowanceDtl.HouseholdId = HouseholdID;
                                                                                            objMisHHAllowanceDtl.MemberId = MemberID;
                                                                                            objMisHHAllowanceDtl.AllowanceTypeCd = Convert.ToDecimal(item);
                                                                                            objMisHHAllowanceDtl.AllowanceDay = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_day") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_day").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_day").Value)) : 99;
                                                                                            objMisHHAllowanceDtl.AllowanceMonth = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_month") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_month").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_month").Value)) : 99;
                                                                                            objMisHHAllowanceDtl.AllowanceYears = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_year") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_year").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_if_present/member_date_allowance/member_date_allowance_year").Value)) : 99;
                                                                                            objMisHHAllowanceDtl.Approved = "N";
                                                                                            objMisHHAllowanceDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                                            objMisHHAllowanceDtl.EnteredDt = DateTime.Now;
                                                                                            objMisHHAllowanceDtl.IPAddress = CommonVariables.IPAddress;
                                                                                            objMisHHAllowanceDtl.BatchId = batchID;
                                                                                            objMisHHAllowanceDtl.Mode = mode;
                                                                                            service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                                            qr = service.SubmitChanges(objMisHHAllowanceDtl, true);
                                                                                        }
                                                                                    }
                                                                                }
                                                                                #endregion
                                                                            }
                                                                            #endregion
                                                                        }
                                                                        if (objMisMemberInfo.DefinedCd.ConvertToString() != "")
                                                                        {
                                                                            #region Family Detail
                                                                            //SAVE FAMILY HH DTL HERE
                                                                            objMisHHFamilyDtl = new MigMisHhFamilyDtlInfo();
                                                                            objMisHHFamilyDtl.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;
                                                                            objMisHHFamilyDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                            objMisHHFamilyDtl.HouseholdId = HouseholdID;
                                                                            objMisHHFamilyDtl.MemberId = MemberID;
                                                                            objMisHHFamilyDtl.MemberActive = "Y";
                                                                            objMisHHFamilyDtl.RelationTypeCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/relation_hh_head") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/relation_hh_head").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/member_name_hh_head/relation_hh_head").Value)) : null;
                                                                            if (objMisHHFamilyDtl.RelationTypeCd.ConvertToString() == "")
                                                                            {
                                                                                objMisHHFamilyDtl.RelationTypeCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_relation") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_relation").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_relation").Value)) : null;
                                                                            }
                                                                            objMisHHFamilyDtl.PresenceStatusCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_head_info_7/hh_head_presence") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_head_info_7/hh_head_presence").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/member_hh_head/hh_head_info_7/hh_head_presence").Value)) : null;
                                                                            if (objMisHHFamilyDtl.PresenceStatusCd.ConvertToString() == "")
                                                                            {
                                                                                objMisHHFamilyDtl.PresenceStatusCd = member.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_presence") ? Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_presence").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(member.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/members/repeat_members/member_group/other_member/member_presence").Value)) : null;
                                                                            }
                                                                            if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                                            {
                                                                                string a = "";
                                                                            }
                                                                            objMisHHFamilyDtl.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                            objMisHHFamilyDtl.MemberDeath = "N";
                                                                            objMisHHFamilyDtl.MemberMarriage = (objMisMemberInfo.MaritalStatusCd == 99 ? "N" : "Y");
                                                                            objMisHHFamilyDtl.MemberSplit = "N";
                                                                            objMisHHFamilyDtl.TransferHhId = "";
                                                                            objMisHHFamilyDtl.Approved = "N";
                                                                            objMisHHFamilyDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                            objMisHHFamilyDtl.EnteredDt = DateTime.Now;
                                                                            objMisHHFamilyDtl.IPAddress = CommonVariables.IPAddress;
                                                                            objMisHHFamilyDtl.BatchId = batchID;
                                                                            objMisHHFamilyDtl.Mode = mode;
                                                                            service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                            qr = service.SubmitChanges(objMisHHFamilyDtl, true);
                                                                            #endregion
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                                if (family.ContainsKey("DeadMembers"))
                                                                {
                                                                    #region Repeat Deaths
                                                                    foreach (SortedDictionary<dynamic, dynamic> deadMember in family.First(s => s.Key == "DeadMembers").Value)
                                                                    {
                                                                        objMisHHDeathDtl = new MigMisHhDeathDtlInfo();
                                                                        objMisHHDeathDtl.HouseholdHead = "N";
                                                                        objMisHHDeathDtl.HouseholdId = HouseholdID;
                                                                        objMisHHDeathDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                        objMisHHDeathDtl.Sno = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_sn") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_sn").Value)) : null;
                                                                        objMisHHDeathDtl.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;
                                                                        if (objNHRSHouseOwnerMst.NhrsUuid.ConvertToString() == "")
                                                                        {
                                                                            string a = "";
                                                                        }
                                                                        objMisHHDeathDtl.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                        objMisHHDeathDtl.FirstNameEng = objMisHHDeathDtl.FirstNameLoc = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_1/death_first_name") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_1/death_first_name").Value) : null;
                                                                        objMisHHDeathDtl.LastNameEng = objMisHHDeathDtl.LastNameLoc = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_1/death_last_name") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_1/death_last_name").Value) : null;
                                                                        objMisHHDeathDtl.MiddleNameEng = objMisHHDeathDtl.MiddleNameLoc = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_1/death_middle_name") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_1/death_middle_name").Value) : null;
                                                                        objMisHHDeathDtl.FullNameEng = objMisHHDeathDtl.FullNameLoc = objMisHHDeathDtl.FirstNameEng.ConvertToString() + (objMisHHDeathDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objMisHHDeathDtl.MiddleNameEng) + " ") + objMisHHDeathDtl.LastNameEng.ConvertToString();
                                                                        objMisHHDeathDtl.GenderCd = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_gender") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_gender").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_gender").Value)) : null;
                                                                        objMisHHDeathDtl.DeathYear = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_year") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_year").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_year").Value)) : null;
                                                                        objMisHHDeathDtl.DeathMonth = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_month") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_month").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_month").Value)) : null;
                                                                        objMisHHDeathDtl.DeathDay = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_day") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_day").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_date_day").Value)) : null;
                                                                        objMisHHDeathDtl.Age = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_age") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_age").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_age").Value)) : null;
                                                                        objMisHHDeathDtl.DeathCertificate = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_registered") ? (Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_registered").Value) == "1" ? "Y" : "N") : "N";
                                                                        if (objMisHHDeathDtl.DeathCertificate == "Y")
                                                                        {
                                                                            //provide death certificate and no
                                                                        }
                                                                        objMisHHDeathDtl.DeathReasonCd = deadMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_reason") ? Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_reason").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(deadMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/death_description/repeat_death/death_group/death_info/death_info_2/death_reason").Value)) : null;
                                                                        objMisHHDeathDtl.CasteCd = family.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste") ? Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(family.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/not_for_foreigner_1/hh_head_caste").Value)) : null;
                                                                        objMisHHDeathDtl.PerDistrictCd = objMisHHDeathDtl.CurDistrictCd = objNHRSHouseOwnerMst.DistrictCd;
                                                                        objMisHHDeathDtl.PerVdcMunCd = objMisHHDeathDtl.CurVdcMunCd = objNHRSHouseOwnerMst.VdcMunCd;
                                                                        objMisHHDeathDtl.PerWardNo = objMisHHDeathDtl.CurWardNo = objNHRSHouseOwnerMst.WardNo;
                                                                        objMisHHDeathDtl.PerAreaEng = objMisHHDeathDtl.CurAreaEng = objNHRSHouseOwnerMst.AreaEng;
                                                                        objMisHHDeathDtl.PerAreaLoc = objMisHHDeathDtl.CurAreaLoc = objNHRSHouseOwnerMst.AreaLoc;
                                                                        objMisHHDeathDtl.Approved = "N";
                                                                        objMisHHDeathDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                        objMisHHDeathDtl.EnteredDt = DateTime.Now;
                                                                        objMisHHDeathDtl.IPAddress = CommonVariables.IPAddress;
                                                                        objMisHHDeathDtl.BatchId = batchID;
                                                                        objMisHHDeathDtl.Mode = mode;
                                                                        //SAVE DEATH DETAIL HERE
                                                                        service.PackageName = "MIGNHRS.PKG_HOUSEHOLD";
                                                                        qr = service.SubmitChanges(objMisHHDeathDtl, true);
                                                                    }
                                                                    #endregion
                                                                }
                                                                if (family.ContainsKey("DestructedMembers"))
                                                                {
                                                                    #region Repeat Human Destruction
                                                                    foreach (SortedDictionary<dynamic, dynamic> destructedMember in family.First(s => s.Key == "DestructedMembers").Value)
                                                                    {
                                                                        objNHRSHHHumanDestroyDtl = new MigNhrsHhHumanDestroyDtlInfo();
                                                                        objNHRSHHHumanDestroyDtl.HouseOwnerId = HouseOwnerIDInserted;
                                                                        objNHRSHHHumanDestroyDtl.HouseholdId = HouseholdID;
                                                                        objNHRSHHHumanDestroyDtl.BuildingStructureNo = building.ContainsKey("building_damage_assessment/hh_data/hh_building/building_number") ? Utils.ConvertToString(building.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/building_number").Value) : null;

                                                                        objNHRSHHHumanDestroyDtl.NhrsUuid = objNHRSHouseOwnerMst.NhrsUuid;
                                                                        objNHRSHHHumanDestroyDtl.FirstNameEng = objNHRSHHHumanDestroyDtl.FirstNameLoc = destructedMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/first_name_destruction") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/first_name_destruction").Value) : null;
                                                                        objNHRSHHHumanDestroyDtl.LastNameEng = objNHRSHHHumanDestroyDtl.LastNameLoc = destructedMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/last_name_destruction") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/last_name_destruction").Value) : null;
                                                                        objNHRSHHHumanDestroyDtl.MiddleNameEng = objNHRSHHHumanDestroyDtl.MiddleNameLoc = destructedMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/middle_name_destruction") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/middle_name_destruction").Value) : null;
                                                                        objNHRSHHHumanDestroyDtl.FullNameEng = objNHRSHHHumanDestroyDtl.FullNameLoc = objNHRSHHHumanDestroyDtl.FirstNameEng.ConvertToString() + (objNHRSHHHumanDestroyDtl.MiddleNameEng.ConvertToString() == "" ? " " : (" " + objNHRSHHHumanDestroyDtl.MiddleNameEng) + " ") + objNHRSHHHumanDestroyDtl.LastNameEng.ConvertToString();
                                                                        objNHRSHHHumanDestroyDtl.GenderCd = destructedMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/gender_destruction") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/gender_destruction").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/gender_destruction").Value)) : null;
                                                                        objNHRSHHHumanDestroyDtl.Age = destructedMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/age_destruction") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/age_destruction").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/age_destruction").Value)) : null;
                                                                        objNHRSHHHumanDestroyDtl.HumandestroyTypeCd = destructedMember.ContainsKey("building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/destruction_effect") ? Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/destruction_effect").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(destructedMember.First(s => s.Key == "building_damage_assessment/hh_data/hh_building/hh_building_group/family/group_family/household/members_info_2/destruction/destructions/destruction_group/destruction_effect").Value)) : null;
                                                                        objNHRSHHHumanDestroyDtl.Approved = "N";
                                                                        objNHRSHHHumanDestroyDtl.EnteredBy = SessionCheck.getSessionUsername();
                                                                        objNHRSHHHumanDestroyDtl.EnteredDt = DateTime.Now;
                                                                        objNHRSHHHumanDestroyDtl.IPAddress = CommonVariables.IPAddress;
                                                                        objNHRSHHHumanDestroyDtl.BatchId = batchID;
                                                                        //SAVE HUMAN DESTRUCTION HERE
                                                                        objNHRSHHHumanDestroyDtl.Mode = mode;
                                                                        service.PackageName = "MIGNHRS.PKG_HOUSEOWNER";
                                                                        qr = service.SubmitChanges(objNHRSHHHumanDestroyDtl, true);
                                                                    }
                                                                    #endregion
                                                                }

                                                            }
                                                            else
                                                            {
                                                                #region Batch Info Update For End Time
                                                                objBatchInfo = new BatchInfoInfo();
                                                                objBatchInfo.BatchId = batchID;
                                                                objBatchInfo.BatchEndTime = System.DateTime.Now;
                                                                objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                                                                objBatchInfo.HouseholdCnt = householdCnt;
                                                                objBatchInfo.BuildingStructureCnt = houseCnt;
                                                                objBatchInfo.MemberCnt = memberCnt;
                                                                objBatchInfo.ErrorMsg = "FAILED";
                                                                objBatchInfo.Mode = "U";
                                                                service.PackageName = "MIGNHRS.PKG_BATCH";
                                                                qr = service.SubmitChanges(objBatchInfo, true);
                                                                #endregion
                                                                DeleteMigrationTableData(batchID);
                                                                service.RollBack();
                                                                return res;
                                                            }

                                                        }
                                                        #endregion
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Batch Info Update For End Time
                        if (qr.IsSuccess)
                        {
                            objBatchInfo = new BatchInfoInfo();
                            objBatchInfo.BatchId = batchID;
                            objBatchInfo.BatchEndTime = System.DateTime.Now;
                            objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                            objBatchInfo.HouseholdCnt = householdCnt;
                            objBatchInfo.BuildingStructureCnt = houseCnt;
                            objBatchInfo.MemberCnt = memberCnt;
                            objBatchInfo.Mode = "U";
                            service.PackageName = "MIGNHRS.PKG_BATCH";
                            qr = service.SubmitChanges(objBatchInfo, true);
                        }
                        #endregion
                    }
                }
                catch (OracleException oe)
                {
                    #region Batch Info Update For End Time
                    objBatchInfo = new BatchInfoInfo();
                    objBatchInfo.BatchId = batchID;
                    objBatchInfo.BatchEndTime = System.DateTime.Now;
                    objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                    objBatchInfo.HouseholdCnt = householdCnt;
                    objBatchInfo.BuildingStructureCnt = houseCnt;
                    objBatchInfo.MemberCnt = memberCnt;
                    objBatchInfo.ErrorMsg = "FAILED";
                    objBatchInfo.Mode = "U";
                    service.PackageName = "MIGNHRS.PKG_BATCH";
                    qr = service.SubmitChanges(objBatchInfo, true);
                    #endregion
                    DeleteMigrationTableData(batchID);
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    #region Batch Info Update For End Time
                    objBatchInfo = new BatchInfoInfo();
                    objBatchInfo.BatchId = batchID;
                    objBatchInfo.BatchEndTime = System.DateTime.Now;
                    objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                    objBatchInfo.HouseholdCnt = householdCnt;
                    objBatchInfo.BuildingStructureCnt = houseCnt;
                    objBatchInfo.MemberCnt = memberCnt;
                    objBatchInfo.ErrorMsg = "FAILED";
                    objBatchInfo.Mode = "U";
                    service.PackageName = "MIGNHRS.PKG_BATCH";
                    qr = service.SubmitChanges(objBatchInfo, true);
                    #endregion
                    DeleteMigrationTableData(batchID);
                    service.RollBack();
                    exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qr != null)
                {
                    res = qr.IsSuccess;
                }
                return res;
            }
        }

        public List<string> HouseIDListCSV()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT HOUSE_OWNER_ID AS ona_id FROM MIGNHRS.RES_HOWNER_OTH_RESIDENCE_DTL";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("ona_id")).ToList();
            }
            return lstStr;
        }
        public List<string> HouseIDList()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT DEFINED_CD AS HOUSE_ID FROM MIGNHRS.MIG_NHRS_HOUSE_OWNER_MST";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("HOUSE_ID")).ToList();
            }
            return lstStr;
        }

        public List<string> JSONFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILE_NAME FROM MIGNHRS.RES_BATCH_INFO WHERE ERROR_MSG IS NULL";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILE_NAME")).ToList();
            }
            return lstStr;
        }

        public List<string> JSONErrorFileListInDB()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT FILE_NAME FROM MIGNHRS.RES_BATCH_INFO WHERE ERROR_MSG='FAILED'";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<string>("FILE_NAME")).ToList();
            }
            return lstStr;
        }

        public string GetBatchIDFromFileName(string fileName)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            string lstStr = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT BATCH_ID FROM MIGNHRS.RES_BATCH_INFO WHERE FILE_NAME=" + fileName.Trim();
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.Rows[0]["BATCH_ID"].ToString();
            }
            return lstStr;
        }

        public List<string> GetBatchIDList()
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            List<string> lstStr = new List<string>();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "SELECT BATCH_ID FROM MIGNHRS.RES_BATCH_INFO WHERE IS_POSTED='N' and Batch_end_time is not null";
                try
                {
                    dt = service.GetDataTable(cmdText, null);

                }
                catch (OracleException ox)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ox);
                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                lstStr = dt.AsEnumerable().Select(r => r.Field<decimal>("BATCH_ID").ToString()).ToList();
            }
            return lstStr;
        }

        public Boolean PostToMain()
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "MIGNHRS.PKG_MIGRATION";
                string btcID = "";
                try
                {
                    service.Begin();
                    List<string> batchListInDB = new List<string>();
                    batchListInDB = GetBatchIDList();
                    foreach (string btchId in batchListInDB)
                    {
                        btcID = btchId;
                        qr = service.SubmitChanges("PR_MIGRATION", btchId, CommonVariables.IPAddress);
                        if (qr.IsSuccess)
                        {
                            string cmdText = String.Format("update MIGNHRS.RES_BATCH_INFO set IS_POSTED='Y',POSTED_ERROR='SUCCESS' where BATCH_ID='" + btchId + "'");
                            service.SubmitChanges(cmdText, null);

                        }
                    }
                    //qr = service.SubmitChanges("PR_MIGRATION", "1", CommonVariables.IPAddress);
                }
                catch (OracleException oe)
                {
                    DeleteMainTableData(btcID.ToDecimal());
                    //  string cmdText = String.Format("update MIGNHRS.RES_BATCH_INFO set IS_POSTED='N',POSTED_ERROR='" + oe.Message + "' where BATCH_ID='" + btcID + "'");
                    // service.SubmitChanges(cmdText, null);
                    // service.RollBack();
                    //SaveErrorMessgae(cmdText);
                    string cmdText = String.Format("update MIGNHRS.RES_BATCH_INFO set IS_POSTED='Y',POSTED_ERROR='" + oe.Message + "' where BATCH_ID='" + btcID + "'");
                    // service.SubmitChanges(cmdText, null);
                    // service.RollBack();
                    SaveErrorMessgae(cmdText);
                    ExceptionManager.AppendLog(oe);
                    PostToMain();
                }
                catch (Exception ex)
                {
                    DeleteMainTableData(btcID.ToDecimal());
                    string cmdText = String.Format("update MIGNHRS.RES_BATCH_INFO set IS_POSTED='Y',POSTED_ERROR='" + ex.Message + "' where BATCH_ID='" + btcID + "'");
                    //service.SubmitChanges(cmdText, null);
                    SaveErrorMessgae(cmdText);
                    //service.RollBack();
                    ExceptionManager.AppendLog(ex);
                    PostToMain();
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;
        }

        public bool SaveErrorMessgae(string errorMsg)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(errorMsg, null);

                }
                catch (OracleException oe)
                {

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {

                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }
        public void DeleteMigrationTableData(decimal? batchId)
        {
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "MIGNHRS.PKG_RES_BATCH";
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges("PR_MIG_BATCH_DELETE", batchId);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
        }

        public void DeleteMainTableData(decimal? batchId)
        {
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "NHRS.PKG_RES_NHRS_BATCH";
                try
                {
                    service.Begin();
                    qr = service.SubmitChanges("PR_NHRS_RES_BATCH_DELETE", batchId);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
        }

        public void UpdateBatch(ResurveyBatchInfo objBatch)
        {
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(objBatch, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
        }
        public string GetHouseOWnerID(string HouseOwnerID, string TableName)
        {
            string result = "";
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                //string cmdText = "select HOUSE_OWNER_ID from '" + TableName + "' where HOUSE_OWNER_ID ='" + HouseOwnerID + "'";
                string cmdText = "SELECT DEFINED_CD from " + TableName + " WHERE DEFINED_CD ='" + HouseOwnerID + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        result = dt.Rows[0][0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }


            }

            return result;
        }


        //ReSurvey Other Household 

        public Boolean SaveDataCSVOther(string fileName, List<SortedDictionary<dynamic, dynamic>> surveyList, string mode, string btchID, out int surveyCnt, out int houseCnt, out int householdCnt, out int memberCnt, out string exc)
        {
            QueryResult qr = null;
            bool res = false;
            surveyCnt = 0;
            houseCnt = 0;
            householdCnt = 0;
            memberCnt = 0;

            CommonFunction commFunction = new CommonFunction();
            List<string> houseIDListDB = new List<string>();
            exc = "";

             
            string HouseholdID = String.Empty;
            string MemberID = String.Empty;
            decimal? batchID = 0;
            List<Dictionary<string, string>> lstmember = new List<Dictionary<string, string>>();

            List<SortedDictionary<dynamic, dynamic>> filteredSurveyList = new List<SortedDictionary<dynamic, dynamic>>();
            ResurveyBatchInfo objBatchInfo = new ResurveyBatchInfo();
              MigResurveyHhOthResidenceDtlInfo objNHRSOtherHouseDtl = new MigResurveyHhOthResidenceDtlInfo(); 
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    houseIDListDB = new List<string>();
                    houseIDListDB = HouseIDListCSV();
                    filteredSurveyList = surveyList.Where(m => !houseIDListDB.Contains(m["ona_id"])).ToList();
                    if (filteredSurveyList.Count > 0)
                    {
                        surveyCnt = filteredSurveyList.Count;
                        if (string.IsNullOrEmpty(btchID))
                        {
                            #region Batch Info Insert For Start Time
                            objBatchInfo = new ResurveyBatchInfo();
                            objBatchInfo.BatchDate = System.DateTime.Now;
                            objBatchInfo.BatchStartTime = System.DateTime.Now;
                            objBatchInfo.FileName = fileName;
                            objBatchInfo.Mode = mode;
                            objBatchInfo.IsPosted = "N";
                            service.Begin();
                            service.PackageName = "MIGNHRS.PKG_RES_BATCH";
                            //Main Table 
                            qr = service.SubmitChanges(objBatchInfo, true);
                            //SAVE BATCH INFO HERE
                            #endregion
                        }
                        else
                        {
                            batchID = btchID.ToDecimal();
                        }

                        #region Entire CSV Data Import Here

                        //if (qr.IsSuccess)
                        //{
                        if (string.IsNullOrEmpty(btchID))
                        {
                            if (qr.IsSuccess)
                            {
                                batchID = qr["v_BATCH_ID"].ToDecimal();
                            }
                        }
                        
                        foreach (var survey in filteredSurveyList)
                        {

                            if (survey.ContainsKey("ona_id") && !houseIDListDB.Contains(Utils.ConvertToString(survey.First(s => s.Key == "ona_id").Value)))
                            {  
                                    #region Other Houses Detail
                                    foreach (SortedDictionary<dynamic, dynamic> house in survey.First(s => s.Key == "OtherHouses").Value)
                                    {
                                        objNHRSOtherHouseDtl = new MigResurveyHhOthResidenceDtlInfo();
                                        objNHRSOtherHouseDtl.HouseOwnerId = house.ContainsKey("ona_id") ? Utils.ConvertToString(house.First(s => s.Key == "ona_id").Value) : null; ;
                                        objNHRSOtherHouseDtl.NewONAID = house.ContainsKey("ona_id") ? Utils.ConvertToString(house.First(s => s.Key == "ona_id").Value) : null;
                                        objNHRSOtherHouseDtl.OtherResidenceId = house.ContainsKey("haop_sn") ? Utils.ConvertToString(house.First(s => s.Key == "haop_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "haop_sn").Value)) : null;
                                        objNHRSOtherHouseDtl.OtherDistrictCd = house.ContainsKey("oth_dist") ? Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value)) : null;
                                        objNHRSOtherHouseDtl.OtherVdcMunCd = house.ContainsKey("oth_vdc") ? Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value)) : null;
                                        objNHRSOtherHouseDtl.OtherWardNo = house.ContainsKey("oth_ward") ? Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value)) : null;
                                        objNHRSOtherHouseDtl.BuildingConditionCd = house.ContainsKey("conditn") ? Utils.ConvertToString(house.First(s => s.Key == "conditn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "conditn").Value)) : null;
                                        objNHRSOtherHouseDtl.DistrictName = house.ContainsKey("district_name") ? Utils.ConvertToString(house.First(s => s.Key == "district_name").Value) : null;
                                        objNHRSOtherHouseDtl.VDCName = house.ContainsKey("vdc_name") ? Utils.ConvertToString(house.First(s => s.Key == "vdc_name").Value) : null;
                                        objNHRSOtherHouseDtl.Approved = "Y";
                                        objNHRSOtherHouseDtl.ApprovedDt = survey.ContainsKey("st_time") ? Utils.ConvertToString(survey.First(s => s.Key == "st_time").Value) : null;
                                        objNHRSOtherHouseDtl.ApprovedBy = SessionCheck.getSessionUsername();
                                        objNHRSOtherHouseDtl.EnteredBy = SessionCheck.getSessionUsername();
                                        objNHRSOtherHouseDtl.EnteredDt = DateTime.Now;
                                        objNHRSOtherHouseDtl.BatchId = batchID;
                                        objNHRSOtherHouseDtl.Mode = mode;

                                        //not in old
                                        objNHRSOtherHouseDtl.Resnforgriev = house.ContainsKey("resn_for_griev") ? Utils.ConvertToString(house.First(s => s.Key == "resn_for_griev").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "resn_for_griev").Value)) : null;
                                        objNHRSOtherHouseDtl.Vdcold = house.ContainsKey("vdc_old") ? Utils.ConvertToString(house.First(s => s.Key == "vdc_old").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "vdc_old").Value)) : null;

                                        objNHRSOtherHouseDtl.Wardold = house.ContainsKey("ward_old") ? Utils.ConvertToString(house.First(s => s.Key == "ward_old").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "ward_old").Value)) : null;
                                        objNHRSOtherHouseDtl.Loi_sn = house.ContainsKey("loi_sn") ? Utils.ConvertToString(house.First(s => s.Key == "loi_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "loi_sn").Value)) : null;
                                        objNHRSOtherHouseDtl.Grvsn = house.ContainsKey("grv_sn") ? Utils.ConvertToString(house.First(s => s.Key == "grv_sn").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "grv_sn").Value)) : null;
                                        objNHRSOtherHouseDtl.Othdist = house.ContainsKey("oth_dist") ? Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_dist").Value)) : null;
                                        objNHRSOtherHouseDtl.Othvdc = house.ContainsKey("oth_vdc") ? Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_vdc").Value)) : null;

                                        objNHRSOtherHouseDtl.Othward = house.ContainsKey("oth_ward") ? Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "oth_ward").Value)) : null;
                                        objNHRSOtherHouseDtl.Enumid = house.ContainsKey("enum_id") ? Utils.ConvertToString(house.First(s => s.Key == "enum_id").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "enum_id").Value)) : null;
                                        objNHRSOtherHouseDtl.Houseno = house.ContainsKey("house_no") ? Utils.ConvertToString(house.First(s => s.Key == "house_no").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "house_no").Value)) : null;
                                        objNHRSOtherHouseDtl.Enumar = house.ContainsKey("enum_ar") ? Utils.ConvertToString(house.First(s => s.Key == "enum_ar").Value) == "" ? null : Convert.ToInt32(Utils.ConvertToString(house.First(s => s.Key == "enum_ar").Value)) : null;
                                        //end
                                        service.PackageName = "MIGNHRS.PKG_res_HOUSEOWNER";
                                        qr = service.SubmitChanges(objNHRSOtherHouseDtl, true);

                                    }

                                    #endregion
                                     
                            } 
                        } 
                        #region Batch Info Update For End Time
                        if (qr.IsSuccess)
                        {
                            objBatchInfo = new ResurveyBatchInfo();
                            objBatchInfo.BatchId = batchID;
                            objBatchInfo.BatchEndTime = System.DateTime.Now;
                            objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                            objBatchInfo.HouseholdCnt = householdCnt;
                            objBatchInfo.BuildingStructureCnt = houseCnt;
                            objBatchInfo.MemberCnt = memberCnt;
                            objBatchInfo.IsPosted = "N";
                            objBatchInfo.Mode = "U";
                            service.PackageName = "MIGNHRS.PKG_RES_BATCH";
                            qr = service.SubmitChanges(objBatchInfo, true);
                        }
                        #endregion
                    }
                    else
                    {

                    }
                }
                catch (OracleException oe)
                {
                    #region Batch Info Update For End Time
                    objBatchInfo = new ResurveyBatchInfo();
                    objBatchInfo.BatchId = batchID;

                    objBatchInfo.BatchEndTime = System.DateTime.Now;
                    objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                    objBatchInfo.HouseholdCnt = householdCnt;
                    objBatchInfo.BuildingStructureCnt = houseCnt;
                    objBatchInfo.PostedError = oe.ConvertToString();
                    objBatchInfo.MemberCnt = memberCnt;
                    objBatchInfo.ErrorMsg = "FAILED";
                    objBatchInfo.IsPosted = "N";
                    objBatchInfo.Mode = "U";
                    UpdateBatch(objBatchInfo);
                    #endregion
                    DeleteMigrationTableData(batchID);

                    service.RollBack();
                    exc += oe.Message.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    #region Batch Info Update For End Time
                    objBatchInfo = new ResurveyBatchInfo();
                    objBatchInfo.BatchId = batchID;
                    objBatchInfo.BatchEndTime = System.DateTime.Now;
                    objBatchInfo.HouseOwnerCnt = filteredSurveyList.Count();
                    objBatchInfo.HouseholdCnt = householdCnt;
                    objBatchInfo.BuildingStructureCnt = houseCnt;
                    objBatchInfo.MemberCnt = memberCnt;
                    objBatchInfo.ErrorMsg = "FAILED";
                    objBatchInfo.IsPosted = "N";
                    objBatchInfo.Mode = "U";
                    UpdateBatch(objBatchInfo);
                    #endregion
                    DeleteMigrationTableData(batchID);
                    service.RollBack();
                    exc += ex.Message.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qr != null)
                {
                    res = qr.IsSuccess;
                }
                return res;
            }
                        #endregion
        }
     
        //End
    
    }
}