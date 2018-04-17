using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using MIS.Models.Registration.Household;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using System.Web.Mvc;
using System.Reflection;

namespace MIS.Services.Registration.Household
{
    public class HouseholdService
    {
        private SearchHouseholdModel _hhSearchObj = new SearchHouseholdModel();

        public DataTable GetMemberDetail(String MemberID)
        {
            CommonFunction commFunction = new CommonFunction();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_MEMBER T1";
                if (MemberID != null)
                {

                    cmdText = cmdText + " WHERE T1.MEMBER_ID='" + commFunction.GetCodeFromDataBase(MemberID, "MIS_MEMBER", "MEMBER_ID") + "'";
                }
                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
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
            return dtbl;

        }
        public String HouseHoldformNumber(String HouseholdId)
        {
            CommonFunction commFunction = new CommonFunction();
            DataTable dtbl = null;
            string formNo = "";
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select form_No from MIS_Household_info T1";

                cmdText = cmdText + " WHERE T1.Household_ID='" + HouseholdId + "'";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
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
            if (dtbl != null)
                foreach (DataRow dr in dtbl.Rows)
                {
                    formNo = dr["form_No"].ConvertToString();
                }
            return formNo;

        }
        public SearchHouseholdModel FillObject(FormCollection fc)
        {
            _hhSearchObj = new SearchHouseholdModel();
            PropertyInfo[] props = _hhSearchObj.GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                if (p.Name != "DropdownKeys")
                    p.SetValue(_hhSearchObj, (fc.GetValue(p.Name.ToUpper()) == null ? "" : fc.GetValue(p.Name.ToUpper()).AttemptedValue), null);
            }
            return _hhSearchObj;
        }
        public DataTable GetHousehold(SearchHouseholdModel searchParamObject, string pagerVal, string sortBy, string sortOrder, string pageIndex = "1", string pageSize = "100")
        {
            CommonFunction commFunction = new CommonFunction();
            if (pageSize == "")
            {
                pageSize = "100";
            }
            Object p_session_id = DBNull.Value;
            Object v_householdid = DBNull.Value;
            //Extra Field added
            Object v_formNoFrom = DBNull.Value;
            Object v_formNoTo = DBNull.Value;
            //
            Object v_definedCd = DBNull.Value;
            Object v_fnameLoc = DBNull.Value;
            Object v_mnameLoc = DBNull.Value;
            Object v_lnameLoc = DBNull.Value;
            Object v_fnameeng = DBNull.Value;
            Object v_mnameeng = DBNull.Value;
            Object v_lnameeng = DBNull.Value;
            Object v_fullnameLoc = DBNull.Value;
            Object v_fullnameEng = DBNull.Value;
            Object v_memberid = DBNull.Value;
            Object v_interviewby = DBNull.Value;
            Object v_interviewdatefrom = DBNull.Value;
            Object v_interviewdateto = DBNull.Value;
            Object v_perDistrictcd = DBNull.Value;
            Object v_pervdcmuncd = DBNull.Value;
            Object v_perward = DBNull.Value;
            Object v_perAreaEng = DBNull.Value;
            Object v_perArealoc = DBNull.Value;
            Object v_houseno = DBNull.Value;
            Object v_tempDistrictcd = DBNull.Value;
            Object v_tempvdcmuncd = DBNull.Value;
            Object v_tempward = DBNull.Value;
            Object v_tempAreaEng = DBNull.Value;
            Object v_tempArealoc = DBNull.Value;
            Object v_CASTE_GRP_CD = DBNull.Value;
            Object v_hospitalDistanceHR = DBNull.Value;
            Object v_hospitalDistanceMIN = DBNull.Value;
            Object v_marketDistanceHR = DBNull.Value;
            Object v_marketDistanceMIN = DBNull.Value;
            Object v_ownDwelling = DBNull.Value;
            Object v_bedroom = DBNull.Value;
            Object v_bedroomCnt = DBNull.Value;
            Object v_exWallsMaterialCD_I = DBNull.Value;
            Object v_roofMaterialCD = DBNull.Value;
            Object v_waterSourceCD_II = DBNull.Value;
            Object v_waterDistanceHR = DBNull.Value;
            Object v_waterDistanceMIN = DBNull.Value;
            Object v_toiletTypeCD_I = DBNull.Value;
            Object v_toiletShared = DBNull.Value;
            Object v_lightSourceCD_II = DBNull.Value;
            Object v_fuelSourceCD_II = DBNull.Value;
            Object v_fan = DBNull.Value;
            Object v_freeze = DBNull.Value;
            Object v_computer = DBNull.Value;
            Object v_heater = DBNull.Value;
            Object v_tv = DBNull.Value;
            Object v_internet = DBNull.Value;
            Object v_landline = DBNull.Value;
            Object v_tractor = DBNull.Value;
            Object v_cart = DBNull.Value;
            Object v_pump = DBNull.Value;
            Object v_generator = DBNull.Value;
            Object v_bicycle = DBNull.Value;
            Object v_bike = DBNull.Value;
            Object v_car = DBNull.Value;
            Object v_landOwner = DBNull.Value;
            Object v_landOwnerCnt = DBNull.Value;
            Object v_landInBigha = DBNull.Value;
            Object v_landInRopani = DBNull.Value;
            Object v_landInKattha = DBNull.Value;
            Object v_landInAnna = DBNull.Value;
            Object v_birds = DBNull.Value;
            Object v_sheep = DBNull.Value;
            Object v_enteredBy = DBNull.Value;
            Object pagerInitialVal = DBNull.Value;
            ///
            Object V_INTERVIEW_ST_HH_FROM = DBNull.Value;
            Object V_INTERVIEW_ST_MM_FROM = DBNull.Value;
            Object V_INTERVIEW_END_HH_FROM = DBNull.Value;
            Object V_INTERVIEW_END_MM_FROM = DBNull.Value;
            Object V_INTERVIEW_ST_HH_TO = DBNull.Value;
            Object V_INTERVIEW_ST_MM_TO = DBNull.Value;
            Object V_INTERVIEW_END_HH_TO = DBNull.Value;


            Object V_INTERVIEW_END_MM_TO = DBNull.Value;
            Object V_HOSPITAL_DISTANCE_HR_FROM = DBNull.Value;
            Object V_HOSPITAL_DISTANCE_MIN_FROM = DBNull.Value;
            Object V_HOSPITAL_DISTANCE_HR_TO = DBNull.Value;
            Object V_HOSPITAL_DISTANCE_MIN_TO = DBNull.Value;
            Object V_MARKET_DISTANCE_HR_FROM = DBNull.Value;

            Object V_MARKET_DISTANCE_MIN_FROM = DBNull.Value;
            Object V_MARKET_DISTANCE_HR_TO = DBNull.Value;
            Object V_MARKET_DISTANCE_MIN_TO = DBNull.Value;
            Object V_BEDROOM_CNT_FROM = DBNull.Value;
            Object V_BEDROOM_CNT_TO = DBNull.Value;
            Object V_WATER_DISTANCE_HR_FROM = DBNull.Value;

            Object V_WATER_DISTANCE_MIN_FROM = DBNull.Value;
            Object V_WATER_DISTANCE_HR_TO = DBNull.Value;
            Object V_WATER_DISTANCE_MIN_TO = DBNull.Value;
            Object V_LAND_IN_BIGA_FROM = DBNull.Value;
            Object V_LAND_IN_ROPANI_FROM = DBNull.Value;
            Object V_LAND_IN_KATTHA_FROM = DBNull.Value;
            Object V_LAND_IN_AANA_FROM = DBNull.Value;
            Object V_LAND_IN_BIGA_TO = DBNull.Value;

            Object V_LAND_IN_ROPANI_TO = DBNull.Value;
            Object V_LAND_IN_KATTHA_TO = DBNull.Value;
            Object V_LAND_IN_AANA_TO = DBNull.Value;




            if (searchParamObject != null)
            {
                v_definedCd = searchParamObject.HEAD_DEFINED_CD;
                v_householdid = searchParamObject.HOUSEHOLD_ID;
                //Extra Field added
                v_formNoFrom = searchParamObject.FORM_NO_FROM;
                v_formNoTo = searchParamObject.FORM_NO_TO;
                //
                v_fnameLoc = searchParamObject.FIRST_NAME_LOC;
                v_mnameLoc = searchParamObject.MIDDLE_NAME_LOC;
                v_lnameLoc = searchParamObject.LAST_NAME_LOC;
                v_fnameeng = searchParamObject.FIRST_NAME_ENG;
                v_mnameeng = searchParamObject.MIDDLE_NAME_ENG;
                v_lnameeng = searchParamObject.LAST_NAME_ENG;
                v_fullnameLoc = searchParamObject.FULL_NAME_LOC;
                v_fullnameEng = searchParamObject.FULL_NAME_ENG;
                if (!string.IsNullOrWhiteSpace(searchParamObject.MEMBER_ID))
                {
                    v_memberid = commFunction.GetCodeFromDataBase(searchParamObject.MEMBER_ID, "MIS_MEMBER", "MEMBER_ID");
                }
                else
                {
                    v_memberid = DBNull.Value;
                }

                v_interviewby = searchParamObject.INTERVIEWED_BY;

                v_interviewdatefrom = searchParamObject.INTERVIEW_DT;

                v_interviewdateto = searchParamObject.INTERVIEW_DT_TO;

                if (!string.IsNullOrWhiteSpace(searchParamObject.PER_DISTRICT_CD))
                {
                    v_perDistrictcd = GetData.GetCodeFor(DataType.District, searchParamObject.PER_DISTRICT_CD);
                }
                else
                {
                    v_perDistrictcd = DBNull.Value;
                }
                if (!string.IsNullOrWhiteSpace(searchParamObject.PER_VDCMUNICIPILITY_CD))
                {
                    v_pervdcmuncd = GetData.GetCodeFor(DataType.VdcMun, searchParamObject.PER_VDCMUNICIPILITY_CD);
                }
                else
                {
                    v_pervdcmuncd = DBNull.Value;
                }
                v_perward = searchParamObject.PER_WARD_NO;
                v_perAreaEng = searchParamObject.PER_AREA_ENG;
                v_perArealoc = searchParamObject.PER_AREA_LOC;
                v_houseno = searchParamObject.HOUSE_NO;
                if (!string.IsNullOrWhiteSpace(searchParamObject.CUR_DISTRICT_CD))
                {
                    v_tempDistrictcd = GetData.GetCodeFor(DataType.District, searchParamObject.CUR_DISTRICT_CD);
                }
                else
                {
                    v_tempDistrictcd = DBNull.Value;
                }
                if (!string.IsNullOrWhiteSpace(searchParamObject.CUR_VDC_MUNICIPILITY_CD))
                {
                    v_tempvdcmuncd = GetData.GetCodeFor(DataType.VdcMun, searchParamObject.CUR_VDC_MUNICIPILITY_CD);
                }
                else
                {
                    v_tempvdcmuncd = DBNull.Value;
                }
                v_tempward = searchParamObject.CUR_WARD_NO;
                v_tempAreaEng = searchParamObject.CUR_AREA_ENG;
                v_tempArealoc = searchParamObject.CUR_AREA_LOC;
                v_CASTE_GRP_CD = GetData.GetCodeFor(DataType.CasteGroup, searchParamObject.Caste_Group_Cd);


                v_hospitalDistanceHR = searchParamObject.HOSPITAL_DISTANCE_HR;
                v_hospitalDistanceMIN = searchParamObject.HOSPITAL_DISTANCE_MIN;
                v_marketDistanceHR = searchParamObject.MARKET_DISTANCE_HR;
                v_marketDistanceMIN = searchParamObject.MARKET_DISTANCE_MIN;
                v_ownDwelling = searchParamObject.OWN_DWELLING;
                v_bedroom = searchParamObject.BEDROOM;
                v_bedroomCnt = searchParamObject.BEDROOM_CNT;
                v_exWallsMaterialCD_I = searchParamObject.EX_WALLS_MATERIAL_CD_I_CD;
                v_roofMaterialCD = searchParamObject.ROOF_MATERIAL_CD;
                v_waterSourceCD_II = searchParamObject.WATER_SOURCE_I_CD;
                v_waterDistanceHR = searchParamObject.WATER_DISTANCE_MIN;
                v_waterDistanceMIN = searchParamObject.WATER_DISTANCE_MIN;
                v_toiletTypeCD_I = searchParamObject.TOILET_TYPE_I_CD;
                v_toiletShared = searchParamObject.TOILET_SHARED;
                v_lightSourceCD_II = searchParamObject.LIGHT_SOURCE_I_CD;
                v_fuelSourceCD_II = searchParamObject.FUEL_SOURCE_I_CD;
                v_fan = searchParamObject.FAN;
                v_freeze = searchParamObject.FREEZE;
                v_computer = searchParamObject.COMPUTER;
                v_heater = searchParamObject.HEATER;
                v_tv = searchParamObject.TV;
                v_internet = searchParamObject.INTERNET;
                v_landline = searchParamObject.LANDLINE;
                v_tractor = searchParamObject.TRACTOR;
                v_cart = searchParamObject.CART;
                v_pump = searchParamObject.PUMP;
                v_generator = searchParamObject.GENERATOR;
                v_bicycle = searchParamObject.BICYCLE;
                v_bike = searchParamObject.BIKE;
                v_car = searchParamObject.CAR;
                v_landOwner = searchParamObject.LAND_OWNER;
                v_landOwnerCnt = searchParamObject.LAND_OWNER_CNT;
                v_landInBigha = searchParamObject.LAND_IN_BIGA;
                v_landInRopani = searchParamObject.LAND_IN_ROPANI;
                v_landInKattha = searchParamObject.LAND_IN_KATTHA;
                v_landInAnna = searchParamObject.LAND_IN_AANA;
                v_birds = searchParamObject.BIRDS;
                v_sheep = searchParamObject.SHEEP;
                //
                //V_INTERVIEW_ST_HH_FROM = searchParamObject.Interview_From_HR;
                //V_INTERVIEW_ST_MM_FROM = searchParamObject.Interview_From_HR;
                //V_INTERVIEW_END_HH_FROM = searchParamObject.Interview_From_HR;
                //V_INTERVIEW_END_MM_FROM = searchParamObject.Interview_From_HR;
                //V_INTERVIEW_ST_HH_TO = searchParamObject.Interview_From_HR;
                //V_INTERVIEW_ST_MM_TO = searchParamObject.Interview_From_HR;
                //V_INTERVIEW_END_HH_TO = searchParamObject.Interview_From_HR;

                //V_INTERVIEW_END_MM_TO = searchParamObject.Interview_From_HR;
                V_HOSPITAL_DISTANCE_HR_FROM = searchParamObject.HOSPITAL_DISTANCE_FROM_HR;
                V_HOSPITAL_DISTANCE_MIN_FROM = searchParamObject.HOSPITAL_DISTANCE_FROM_MIN;
                V_HOSPITAL_DISTANCE_HR_TO = searchParamObject.HOSPITAL_DISTANCE_To_HR;
                V_HOSPITAL_DISTANCE_MIN_TO = searchParamObject.HOSPITAL_DISTANCE_To_MIN;

                V_MARKET_DISTANCE_HR_FROM = searchParamObject.MARKET_DISTANCE_FROM_HR;
                V_MARKET_DISTANCE_MIN_FROM = searchParamObject.MARKET_DISTANCE_FROM_MIN;
                V_MARKET_DISTANCE_HR_TO = searchParamObject.MARKET_DISTANCE_TO_HR;
                V_MARKET_DISTANCE_MIN_TO = searchParamObject.MARKET_DISTANCE_TO_MIN;

                V_BEDROOM_CNT_FROM = searchParamObject.BEDROOM_FROM_CNT;
                V_BEDROOM_CNT_TO = searchParamObject.BEDROOM_TO_CNT;

                V_WATER_DISTANCE_HR_FROM = searchParamObject.WATER_DISTANCE_FROM_HR;
                V_WATER_DISTANCE_MIN_FROM = searchParamObject.WATER_DISTANCE_FROM_MIN;
                V_WATER_DISTANCE_HR_TO = searchParamObject.WATER_DISTANCE_TO_HR;
                V_WATER_DISTANCE_MIN_TO = searchParamObject.WATER_DISTANCE_TO_MIN;

                V_LAND_IN_BIGA_FROM = searchParamObject.LAND_IN_BIGA_From;
                V_LAND_IN_ROPANI_FROM = searchParamObject.LAND_IN_ROPANI_FROM;
                V_LAND_IN_KATTHA_FROM = searchParamObject.LAND_IN_KATTHA_FROM;
                V_LAND_IN_AANA_FROM = searchParamObject.LAND_IN_AANA_FROM;
                V_LAND_IN_BIGA_TO = searchParamObject.LAND_IN_BIGA_To;
                V_LAND_IN_ROPANI_TO = searchParamObject.LAND_IN_ROPANI_TO;
                V_LAND_IN_KATTHA_TO = searchParamObject.LAND_IN_KATTHA_TO;
                V_LAND_IN_AANA_TO = searchParamObject.LAND_IN_AANA_TO;



                if (!String.IsNullOrWhiteSpace(pagerVal))
                    pagerInitialVal = pagerVal;
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_MIS_REPORT";
                service.Begin();
                dt = service.GetDataTable(true, "PR_MIS_HOUSEHOLD_SEARCH",
                     p_session_id,
                    v_memberid,
                    v_householdid,
                    v_definedCd,
                    v_fnameeng,
                    v_mnameeng,
                    v_lnameeng,
                    v_fullnameEng,
                    v_fnameLoc,
                    v_mnameLoc,
                    v_lnameLoc,
                    v_fullnameLoc,
                    v_interviewby,
                    v_interviewdatefrom,
                    v_interviewdateto,
                    V_INTERVIEW_ST_HH_FROM,
                    V_INTERVIEW_ST_MM_FROM,
                    V_INTERVIEW_END_HH_FROM,
                    V_INTERVIEW_END_MM_FROM,
                    V_INTERVIEW_ST_HH_TO,
                    V_INTERVIEW_ST_MM_TO,
                    V_INTERVIEW_END_HH_TO,
                    V_INTERVIEW_END_MM_TO,
                    v_perDistrictcd,
                    v_pervdcmuncd,
                    v_perward,
                    v_perAreaEng,
                    v_perArealoc,
                    v_houseno,
                    v_tempDistrictcd,
                    v_tempvdcmuncd,
                    v_tempward,
                    v_tempAreaEng,
                    v_tempArealoc,
                    v_CASTE_GRP_CD,
                    V_HOSPITAL_DISTANCE_HR_FROM,
                    V_HOSPITAL_DISTANCE_MIN_FROM,
                    V_HOSPITAL_DISTANCE_HR_TO,
                    V_HOSPITAL_DISTANCE_MIN_TO,
                    V_MARKET_DISTANCE_HR_FROM,
                    V_MARKET_DISTANCE_MIN_FROM,
                    V_MARKET_DISTANCE_HR_TO,
                    V_MARKET_DISTANCE_MIN_TO,
                    v_ownDwelling,
                    v_bedroom,
                    V_BEDROOM_CNT_FROM,
                    V_BEDROOM_CNT_TO,
                    v_exWallsMaterialCD_I,
                    v_roofMaterialCD,
                    v_waterSourceCD_II,
                    V_WATER_DISTANCE_HR_FROM,
                    V_WATER_DISTANCE_MIN_FROM,
                    V_WATER_DISTANCE_HR_TO,
                    V_WATER_DISTANCE_MIN_TO,
                    v_toiletTypeCD_I,
                    v_toiletShared,
                    v_lightSourceCD_II,
                    v_fuelSourceCD_II,
                    v_fan,
                    v_freeze,
                    v_computer,
                    v_heater,
                    v_tv,
                    v_internet,
                    v_landline,
                    v_tractor,
                    v_cart,
                    v_pump,
                    v_generator,
                    v_bicycle,
                    v_bike,
                    v_car,
                    v_landOwner,
                    v_landOwnerCnt,
                    V_LAND_IN_BIGA_FROM,
                    V_LAND_IN_ROPANI_FROM,
                    V_LAND_IN_KATTHA_FROM,
                    V_LAND_IN_AANA_FROM,
                    V_LAND_IN_BIGA_TO,
                    V_LAND_IN_ROPANI_TO,
                    V_LAND_IN_KATTHA_TO,
                    V_LAND_IN_AANA_TO,
                    v_birds,
                    v_sheep,
                    v_enteredBy,
                    sortBy,
                    sortOrder,
                    pageSize,
                    pageIndex,
                    v_formNoFrom,
                    v_formNoTo,
                    System.Web.HttpContext.Current.Session["LanguageSetting"].ConvertToString().Remove(1),
                    pagerVal,
                    DBNull.Value
                    );
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
            return dt;

        }
        public QueryResult AddHousehold(MISHouseholdInfo household)
        {
            QueryResult qr = null;
            CommonFunction commFunction = new CommonFunction();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    MisHouseholdInfoInfo obj = new MisHouseholdInfoInfo();
                    obj.HouseholdId = household.householdId.ConvertToString();
                    obj.DefinedCd = household.definedCd.ConvertToString();
                    obj.FormNo = household.formNo.ConvertToString();
                    obj.MemberId = household.memberId.ConvertToString();
                    obj.FirstNameEng = household.firstNameEng.ConvertToString();
                    obj.MiddleNameEng = household.middleNameEng.ConvertToString();
                    obj.LastNameEng = household.lastNameEng.ConvertToString();
                    //obj.FullNameEng = household.fullNameEng.ConvertToString();
                    if (String.IsNullOrWhiteSpace(obj.MiddleNameEng))
                        obj.FullNameEng = Utils.ConvertToString(obj.FirstNameEng) + " " + Utils.ConvertToString(obj.LastNameEng);
                    else
                        obj.FullNameEng = Utils.ConvertToString(obj.FirstNameEng) + " " + Utils.ConvertToString(obj.MiddleNameEng) + " " + Utils.ConvertToString(obj.LastNameEng);

                    obj.FirstNameLoc = household.firstNameLoc.ConvertToString();
                    obj.MiddleNameLoc = household.middleNameLoc.ConvertToString();
                    obj.LastNameLoc = household.lastNameLoc.ConvertToString();
                    if (String.IsNullOrWhiteSpace(obj.MiddleNameLoc))
                        obj.FullNameLoc = Utils.ConvertToString(obj.FirstNameLoc) + " " + Utils.ConvertToString(obj.LastNameLoc);
                    else
                        obj.FullNameLoc = Utils.ConvertToString(obj.FirstNameLoc) + " " + Utils.ConvertToString(obj.MiddleNameLoc) + " " + Utils.ConvertToString(obj.LastNameLoc);

                    //obj.FullNameLoc = household.fullNameLoc.ConvertToString();
                    obj.MemberCnt = household.memberCnt.ToInt32();
                    obj.InterviewedBy = household.interviewedBy.ConvertToString();

                    if (household.interviewYear == null || household.interviewdateDay == null || household.interviewMonth == null)
                    {
                        obj.InterviewDt = null;
                    }
                    else
                    {
                        string BirthDtValue = household.interviewMonth + "-" + household.interviewdateDay + "-" + household.interviewYear;
                        obj.InterviewDt = Convert.ToDateTime(BirthDtValue).ToString("dd-MMM-yyyy");
                    }
                    if (household.interviewYearLoc == null || household.interviewdateDayLoc == null || household.interviewMonthLoc == null)
                    {
                        obj.InterviewDtLoc = null;
                    }
                    else
                    {
                        obj.InterviewDtLoc = household.interviewYearLoc + "-" + household.interviewMonthLoc + "-" + household.interviewdateDayLoc;
                    }
                    obj.InterviewDay = household.interviewDay.ConvertToString();
                    obj.InterviewStHh = household.interviewStHh.ConvertToString();
                    obj.InterviewStMm = household.interviewStMm.ConvertToString();
                    obj.InterviewEndHh = household.interviewEndHh.ConvertToString();
                    obj.InterviewEndMm = household.interviewEndMm.ConvertToString();
                    obj.PerDistrictCd = household.perDistrictCd.ToInt32();
                    obj.PerVdcMunCd = household.perVdcMunCd.ToInt32();
                    obj.PerWardNo = household.perWardNo.ToInt32();
                    if (household.perDistrictCd != null)
                    {
                        obj.PerZoneCd = Convert.ToInt32(CommonService.GetZone(household.perDistrictCd.ToString()));
                    }
                    else
                    {
                        obj.PerZoneCd = null;
                    }
                    if (obj.PerZoneCd != null)
                    {
                        obj.PerRegStCd = Convert.ToInt32(CommonService.GetRegion(obj.PerZoneCd.ToString()));
                    }
                    else
                    {
                        obj.PerRegStCd = null;
                    }
                    if (obj.PerRegStCd != null)
                    {
                        obj.PerCountryCd = Convert.ToInt32(CommonService.GetCountry(obj.PerRegStCd.ToString()));
                    }
                    else
                    {
                        obj.PerCountryCd = null;
                    }
                    obj.PerAreaEng = household.perAreaEng.ConvertToString();
                    obj.PerAreaLoc = household.perAreaLoc.ConvertToString();
                    obj.CurDistrictCd = household.curDistrictCd.ToInt32();
                    obj.CurVdcMunCd = household.curVdcMunCd.ToInt32();
                    obj.CurWardNo = household.curWardNo.ToInt32();
                    if (household.curDistrictCd != null)
                    {
                        obj.CurZoneCd = Convert.ToInt32(CommonService.GetZone(household.curDistrictCd.ToString()));
                    }
                    else
                    {
                        obj.CurZoneCd = null;
                    }
                    if (obj.CurZoneCd != null)
                    {
                        obj.CurRegStCd = Convert.ToInt32(CommonService.GetRegion(obj.CurZoneCd.ToString()));
                    }
                    else
                    {
                        obj.CurRegStCd = null;
                    }
                    if (obj.CurRegStCd != null)
                    {
                        obj.CurCountryCd = Convert.ToInt32(CommonService.GetCountry(obj.CurRegStCd.ToString()));
                    }
                    else
                    {
                        obj.CurCountryCd = null;
                    }
                    obj.CurAreaEng = household.curAreaEng.ConvertToString();
                    obj.CurAreaLoc = household.curAreaLoc.ConvertToString();
                    obj.HouseNo = household.houseNo.ConvertToString();
                    obj.TelNo = household.telNo.ConvertToString();
                    obj.MobileNo = household.mobileNo.ConvertToString();
                    obj.Fax = household.fax.ConvertToString();
                    obj.Email = household.email.ConvertToString();
                    obj.Url = household.url.ConvertToString();
                    obj.PoBoxNo = household.poBoxNo.ConvertToString();
                    obj.HabitantYear = household.habitantYear.ToInt32(); ;
                    obj.HabitantMonth = household.habitantMonth.ToInt32();
                    obj.HabitantDay = household.habitantDay.ToInt32();
                    obj.AncestralHome = household.ancestralHome.ConvertBoolean();
                    obj.HospitalDistanceHr = household.hospitalDistanceHr.ToInt32();
                    obj.HospitalDistanceMin = household.hospitalDistanceMin.ToInt32();
                    obj.MarketDistanceHr = household.marketDistanceHr.ToInt32();
                    obj.MarketDistanceMin = household.marketDistanceMin.ToInt32();
                    obj.OwnDwelling = household.ownDwelling.ConvertBoolean();
                    obj.Bedroom = household.bedroom.ConvertBoolean();
                    obj.BedroomCnt = household.bedroomCnt.ToInt32();

                    obj.ExWallsMaterialCdI = commFunction.GetCodeFromDataBase(household.exWallsMaterialCdI, "MIS_MATERIAL", "MATERIAL_CD").ToInt32();
                    obj.ExWallsMaterialCdIi = commFunction.GetCodeFromDataBase(household.exWallsMaterialCdIi, "MIS_MATERIAL", "MATERIAL_CD").ToInt32();// household..ToInt32();
                    obj.RoofMaterialCd = commFunction.GetCodeFromDataBase(household.roofMaterialCd, "MIS_ROOF_MATERIAL", "MATERIAL_CD").ToInt32();// household..ToInt32();
                    obj.WaterSourceCdI = commFunction.GetCodeFromDataBase(household.waterSourceCdI, "mis_Water_Source", "WATER_SOURCE_CD").ToInt32();// household..ToInt32();
                    obj.WaterSourceCdIi = commFunction.GetCodeFromDataBase(household.waterSourceCdIi, "mis_Water_Source", "WATER_SOURCE_CD").ToInt32();// household..ToInt32();
                    obj.WaterDistanceHr = household.waterDistanceHr.ToInt32();
                    obj.WaterDistanceMin = household.waterDistanceMin.ToInt32();
                    obj.ToiletTypeCdI = commFunction.GetCodeFromDataBase(household.toiletTypeCdI, "MIS_TOILET_TYPE", "TOILET_TYPE_CD").ToInt32();// household..ToInt32();
                    obj.ToiletTypeCdIi = commFunction.GetCodeFromDataBase(household.toiletTypeCdIi, "MIS_TOILET_TYPE", "TOILET_TYPE_CD").ToInt32();// household..ToInt32();
                    obj.ToiletShared = household.toiletShared.ConvertBoolean();
                    obj.LightSourceCdI = commFunction.GetCodeFromDataBase(household.lightSourceCdI, "mis_light_source", "LIGHT_SOURCE_CD").ToInt32();// household..ToInt32();
                    obj.LightSourceCdIi = commFunction.GetCodeFromDataBase(household.lightSourceCdIi, "mis_light_source", "LIGHT_SOURCE_CD").ToInt32();// household..ToInt32();
                    obj.FuelSourceCdI = commFunction.GetCodeFromDataBase(household.fuelSourceCdI, "mis_fuel_source", "FUEL_SOURCE_CD").ToInt32();// household..ToInt32();
                    obj.FuelSourceCdIi = commFunction.GetCodeFromDataBase(household.fuelSourceCdIi, "mis_fuel_source", "FUEL_SOURCE_CD").ToInt32();// household.fuelSourceCdIi.ToInt32();
                    obj.Fan = household.fan.ConvertBoolean();
                    obj.Heater = household.heater.ConvertBoolean();
                    obj.Freeze = household.freeze.ConvertBoolean();
                    obj.Tv = household.tv.ConvertBoolean();
                    obj.Computer = household.computer.ConvertBoolean();
                    obj.Internet = household.internet.ConvertBoolean();
                    obj.Landline = household.landline.ConvertBoolean();
                    obj.Tractor = household.tractor.ConvertBoolean();
                    obj.Cart = household.cart.ConvertBoolean();
                    obj.Pump = household.pump.ConvertBoolean();
                    obj.Generator = household.generator.ConvertBoolean();
                    obj.Bicycle = household.bicycle.ConvertBoolean();
                    obj.Bike = household.bike.ConvertBoolean();
                    obj.Car = household.car.ConvertBoolean();
                    obj.LandOwner = household.landOwner.ConvertBoolean();
                    obj.LandOwnerCnt = household.landOwnerCnt.ToInt32();
                    obj.LandInBiga = household.landInBiga.ToInt32();
                    obj.LandInRopani = household.landInRopani.ToInt32();
                    obj.LandInKattha = household.landInKattha.ToInt32();
                    obj.LandInAana = household.landInAana.ToInt32();
                    obj.Birds = household.birds.ConvertBoolean();
                    obj.Sheep = household.sheep.ConvertBoolean();
                    obj.DeathInAYear = household.deathInAYear.ConvertBoolean();
                    obj.ChildInSchool = household.childInSchool.ConvertBoolean();
                    obj.SocialAllowance = household.socialAllowance.ConvertBoolean();
                    obj.Remarks = household.remarks;
                    obj.RemarksLoc = household.remarksLoc;
                    obj.RuleCalc = household.ruleCalc.ConvertBoolean();
                    obj.RuleFlag = household.ruleFlag;
                    obj.RuleValue = household.ruleValue.ToInt32();
                    obj.TransId = household.transId;
                    obj.ExtraV = household.extraV;
                    obj.Approved = household.approved.ConvertBoolean();
                    obj.ApprovedBy = household.approvedBy;
                    obj.ApprovedByLoc = household.approvedByLoc;
                    obj.ApprovedDt = household.approvedDt;
                    obj.ApprovedDtLoc = household.approvedDtLoc;
                    obj.UpdatedBy = household.updatedBy;
                    obj.UpdatedByLoc = household.updatedByLoc;
                    obj.UpdatedDt = household.updatedDt; //household.enteredDt;
                    obj.UpdatedDtLoc = household.updatedDtLoc;
                    obj.EnteredBy = SessionCheck.getSessionUsername();//household.enteredBy;
                    obj.EnteredByLoc = household.enteredByLoc;
                    obj.EnteredDt = household.approvedDt; //household.enteredDt;
                    obj.EnteredDtLoc = household.enteredDtLoc;
                    //
                    obj.ExWallsOthEngI = household.ExMaterialOthEngI;
                    obj.ExWallsOthNepI = household.ExMaterialOthNepI;
                    obj.ExWallsOthEngIi = household.ExMaterialOthEngII;
                    obj.ExWallsOthNepIi = household.ExMaterialOthNepII;
                    obj.WaterSourceOthEngI = household.WaterSourceOthEngI;
                    obj.WaterSourceOthNepI = household.WaterSourceOthNepI;
                    obj.WaterSourceOthEngIi = household.WaterSourceOthEngIi;
                    obj.WaterSourceOthNepIi = household.WaterSourceOthNepIi;

                    obj.ToiletTypeOthEngI = household.ToiletTypeOthEngI;
                    obj.ToiletTypeOthNepI = household.ToiletTypeOthNepI;
                    obj.ToiletTypeOthEngIi = household.ToiletTypeOthEngIi;
                    obj.ToiletTypeOthNepIi = household.ToiletTypeOthNepIi;
                    obj.LightSourceOthEngI = household.LightSourceOthEngI;
                    obj.LightSourceOthNepI = household.LightSourceOthNepI;
                    obj.LightSourceOthEngIi = household.LightSourceOthEngII; //household.enteredDt;
                    obj.LightSourceOthNepIi = household.LightSourceOthNepII;

                    obj.FuelSourceOthEngI = household.fuelSourceOthEngI;
                    obj.FuelSourceOthNepI = household.fuelSourceOthNepI;
                    obj.FuelSourceOthEngIi = household.fuelSourceOthEngII; //household.enteredDt;
                    obj.FuelSourceOthNepIi = household.fuelSourceOthNepII;

                    obj.RoofMaterialOthEng = household.roofMaterialOthEngI; //household.enteredDt;
                    obj.RoofMaterialOthNep = household.roofMaterialOthNepI;

                    //
                    obj.IpAddress = CommonVariables.IPAddress;
                    obj.Mode = household.Mode;
                    service.PackageName = "PKG_HOUSEHOLD";

                    service.Begin();
                    qr = service.SubmitChanges(obj, true);
                    //if (household.Mode == "U")
                    //{

                    //}
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
                return qr; //qr["V_HOUSEHOLD_ID"].ToString();
            }
        }
        public void ApproveDelete(MISHouseholdInfo household, out string exc)
        {
            exc = "";
            MisHouseholdInfoInfo obj = new MisHouseholdInfoInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    if (household.Mode == "D")
                    {
                        obj.HouseholdId = household.householdId;
                        obj.EnteredBy = SessionCheck.getSessionUsername();
                        obj.Mode = household.Mode;
                        //obj.IPAddress = null;
                    }
                    else if (household.Mode == "A")
                    {
                        obj.HouseholdId = household.householdId;
                        if (household.approved == "Y")
                        {
                            obj.Approved = false;
                        }
                        else
                        {
                            obj.Approved = true;
                        }
                        obj.EnteredBy = SessionCheck.getSessionUsername();
                        obj.ApprovedBy = household.approvedBy;
                        obj.ApprovedDt = household.approvedDt;
                        obj.ApprovedDtLoc = household.approvedDtLoc;
                        obj.Mode = household.Mode;
                    }
                    service.PackageName = "PKG_HOUSEHOLD";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(obj, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    exc = oe.Code.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction.Connection != null)
                    {
                        service.End();
                    }
                }
            }
        }
        public MISHouseholdInfo ViewHousehold(string householdid)
        {

            MISHouseholdInfo obj = new MISHouseholdInfo();
            DataTable dTable = new DataTable();
            dTable = null;
            using (ServiceFactory Service = new ServiceFactory())
            {
                try
                {
                    Service.Begin();
                    string CmdText = "select * from VW_HOUSEHOLD_DTL V1 where V1.HOUSEHOLD_ID='" + householdid + "'";
                    dTable = Service.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }
                    });

                    if (dTable.Rows.Count > 0)
                    {
                        obj.householdId = dTable.Rows[0]["HOUSEHOLD_ID"].ConvertToString();
                        obj.definedCd = dTable.Rows[0]["H_DEFINED_CD"].ConvertToString();
                        obj.memberId = dTable.Rows[0]["MEMBER_ID"].ConvertToString();
                        obj.formNo = dTable.Rows[0]["FORM_NO"].ToString();
                        obj.fullNameEng = dTable.Rows[0]["H_FULL_NAME_ENG"].ConvertToString();
                        obj.fullNameLoc = dTable.Rows[0]["H_FULL_NAME_LOC"].ConvertToString();
                        obj.memberCnt = dTable.Rows[0]["MEMBER_CNT"].ConvertToString();
                        obj.interviewedBy = dTable.Rows[0]["INTERVIEWED_BY"].ConvertToString();
                        if (dTable.Rows[0]["INTERVIEW_DT"] != DBNull.Value)
                        {
                            if (dTable.Rows[0]["INTERVIEW_DT"] != null)
                                obj.interviewDt = (dTable.Rows[0]["INTERVIEW_DT"].ToString() == "") ? "" : Convert.ToDateTime(dTable.Rows[0]["INTERVIEW_DT"]).ToString("dd-MMM-yyyy");
                        }
                        obj.interviewDtLoc = dTable.Rows[0]["INTERVIEW_DT_LOC"].ConvertToString();


                        obj.interviewDay = dTable.Rows[0]["INTERVIEW_DAY"].ConvertToString();
                        obj.interviewStHh = dTable.Rows[0]["INTERVIEW_ST_HH"].ConvertToString();
                        obj.interviewStMm = dTable.Rows[0]["INTERVIEW_ST_MM"].ConvertToString();
                        obj.interviewEndHh = dTable.Rows[0]["INTERVIEW_END_HH"].ConvertToString();
                        obj.interviewEndMm = dTable.Rows[0]["INTERVIEW_END_MM"].ConvertToString();
                        obj.perCountry = Utils.ToggleLanguage(dTable.Rows[0]["PER_COUNTRY_ENG"].ConvertToString(), dTable.Rows[0]["PER_COUNTRY_LOC"].ConvertToString());
                        obj.perReg = Utils.ToggleLanguage(dTable.Rows[0]["PER_REGION_ENG"].ConvertToString(), dTable.Rows[0]["PER_REGION_ENG"].ConvertToString());
                        obj.perDistrict = Utils.ToggleLanguage(dTable.Rows[0]["PER_DISTRICT_ENG"].ConvertToString(), dTable.Rows[0]["PER_DISTRICT_LOC"].ConvertToString());
                        obj.perDistrictLOC = dTable.Rows[0]["PER_DISTRICT_LOC"].ConvertToString();
                        obj.perDistrictENG = dTable.Rows[0]["PER_DISTRICT_ENG"].ConvertToString();
                        obj.perZone = Utils.ToggleLanguage(dTable.Rows[0]["PER_ZONE_ENG"].ConvertToString(), dTable.Rows[0]["PER_ZONE_LOC"].ConvertToString());

                        obj.perVDc = Utils.ToggleLanguage(dTable.Rows[0]["PER_VDCMUNICIPILITY_ENG"].ConvertToString(), dTable.Rows[0]["PER_VDCMUNICIPILITY_LOC"].ConvertToString());
                        obj.perVDcLOC = dTable.Rows[0]["PER_VDCMUNICIPILITY_LOC"].ConvertToString();
                        obj.perVDcENG = dTable.Rows[0]["PER_VDCMUNICIPILITY_ENG"].ConvertToString();
                        obj.perWard = dTable.Rows[0]["PER_WARD_NO"].ConvertToString();
                        obj.perAreaEng = dTable.Rows[0]["PER_AREA_ENG"].ConvertToString();
                        obj.perAreaLoc = dTable.Rows[0]["PER_AREA_LOC"].ConvertToString();
                        obj.tempCountry = Utils.ToggleLanguage(dTable.Rows[0]["CUR_COUNTRY_ENG"].ConvertToString(), dTable.Rows[0]["CUR_COUNTRY_LOC"].ConvertToString());
                        obj.tempReg = Utils.ToggleLanguage(dTable.Rows[0]["CUR_REGION_STATE_ENG"].ConvertToString(), dTable.Rows[0]["CUR_REGION_STATE_LOC"].ConvertToString());
                        obj.tempZone = Utils.ToggleLanguage(dTable.Rows[0]["CUR_ZONE_ENG"].ConvertToString(), dTable.Rows[0]["CUR_ZONE_ENG"].ConvertToString());
                        obj.TempDistrict = Utils.ToggleLanguage(dTable.Rows[0]["CUR_DISTRICT_ENG"].ConvertToString(), dTable.Rows[0]["CUR_DISTRICT_LOC"].ConvertToString());
                        obj.tempVDCmun = Utils.ToggleLanguage(dTable.Rows[0]["CUR_VDC_MUNICIPILITY_ENG"].ConvertToString(), dTable.Rows[0]["CUR_VDC_MUNICIPILITY_LOC"].ConvertToString());
                        obj.tempWard = dTable.Rows[0]["CUR_WARD_NO"].ConvertToString();
                        obj.curAreaEng = dTable.Rows[0]["CUR_AREA_ENG"].ConvertToString();
                        obj.curAreaLoc = dTable.Rows[0]["CUR_AREA_LOC"].ConvertToString();
                        obj.houseNo = dTable.Rows[0]["HOUSE_NO"].ConvertToString();
                        obj.telNo = dTable.Rows[0]["TEL_NO"].ConvertToString();
                        obj.mobileNo = dTable.Rows[0]["MOBILE_NO"].ConvertToString();
                        obj.fax = dTable.Rows[0]["FAX"].ConvertToString();
                        obj.email = dTable.Rows[0]["EMAIL"].ConvertToString();
                        obj.url = dTable.Rows[0]["URL"].ConvertToString();
                        obj.poBoxNo = dTable.Rows[0]["PO_BOX_NO"].ConvertToString();
                        obj.habitantYear = dTable.Rows[0]["HABITANT_YEAR"].ConvertToString();
                        obj.habitantMonth = dTable.Rows[0]["HABITANT_MONTH"].ConvertToString();
                        obj.habitantDay = dTable.Rows[0]["HABITANT_DAY"].ConvertToString();
                        obj.ancestralHome = dTable.Rows[0]["ANCESTRAL_HOME"].ConvertToString();
                        obj.hospitalDistanceHr = dTable.Rows[0]["HOSPITAL_DISTANCE_HR"].ConvertToString();
                        obj.hospitalDistanceMin = dTable.Rows[0]["HOSPITAL_DISTANCE_MIN"].ConvertToString();
                        obj.marketDistanceHr = dTable.Rows[0]["MARKET_DISTANCE_HR"].ConvertToString();
                        obj.marketDistanceMin = dTable.Rows[0]["MARKET_DISTANCE_MIN"].ConvertToString();
                        obj.ownDwelling = (dTable.Rows[0]["OWNERSHIPOFBUILDING"].ConvertToString());
                        obj.bedroom = dTable.Rows[0]["BEDROOM"].ConvertToString();
                        obj.bedroomCnt = dTable.Rows[0]["NOOFBEDROOM"].ConvertToString();
                        obj.ImageURL = dTable.Rows[0]["MEMBER_PHOTO_ID"].ConvertToString();
                        obj.exWallMaterial = Utils.ToggleLanguage(dTable.Rows[0]["EX_WALLS_MATERIAL_CD_I_ENG"].ConvertToString(), dTable.Rows[0]["EX_WALLS_MATERIAL_CD_I_LOC"].ConvertToString());
                        obj.exWallMaterialII = Utils.ToggleLanguage(dTable.Rows[0]["EX_WALLS_MATERIAL_CD_II_ENG"].ConvertToString(), dTable.Rows[0]["EX_WALLS_MATERIAL_CD_II_LOC"].ConvertToString());
                        obj.roofMaterial = Utils.ToggleLanguage(dTable.Rows[0]["ROOF_MATERIAL_ENG"].ConvertToString(), dTable.Rows[0]["ROOF_MATERIAL_LOC"].ConvertToString());
                        obj.sourceDirnkingI = Utils.ToggleLanguage(dTable.Rows[0]["WATER_SOURCE_I_ENG"].ConvertToString(), dTable.Rows[0]["WATER_SOURCE_I_LOC"].ConvertToString());
                        obj.sourceDrinkingII = Utils.ToggleLanguage(dTable.Rows[0]["WATER_SOURCE_II_ENG"].ConvertToString(), dTable.Rows[0]["WATER_SOURCE_II_LOC"].ConvertToString());
                        obj.waterDistanceHr = dTable.Rows[0]["WATER_DISTANCE_HR"].ConvertToString();
                        obj.waterDistanceMin = dTable.Rows[0]["WATER_DISTANCE_MIN"].ConvertToString();
                        obj.toiletTypeI = Utils.ToggleLanguage(dTable.Rows[0]["TOILET_TYPE_I_ENG"].ConvertToString(), dTable.Rows[0]["TOILET_TYPE_I_LOC"].ConvertToString());
                        obj.toiletTypeII = Utils.ToggleLanguage(dTable.Rows[0]["TOILET_TYPE_II_ENG"].ConvertToString(), dTable.Rows[0]["TOILET_TYPE_II_LOC"].ConvertToString());
                        obj.toiletShared = dTable.Rows[0]["TOILET_SHARED"].ConvertToString();
                        obj.lightSourceI = Utils.ToggleLanguage(dTable.Rows[0]["LIGHT_SOURCE_I_ENG"].ConvertToString(), dTable.Rows[0]["LIGHT_SOURCE_I_LOC"].ConvertToString());
                        obj.lightSourceII = Utils.ToggleLanguage(dTable.Rows[0]["LIGHT_SOURCE_II_ENG"].ConvertToString(), dTable.Rows[0]["LIGHT_SOURCE_II_LOC"].ConvertToString());
                        obj.fuelSourceI = Utils.ToggleLanguage(dTable.Rows[0]["FUEL_SOURCE_I_ENG"].ConvertToString(), dTable.Rows[0]["FUEL_SOURCE_I_LOC"].ConvertToString());
                        obj.fuelSourceII = Utils.ToggleLanguage(dTable.Rows[0]["FUEL_SOURCE_II_ENG"].ConvertToString(), dTable.Rows[0]["FUEL_SOURCE_II_LOC"].ConvertToString());
                        obj.fan = dTable.Rows[0]["FAN"].ConvertToString();
                        obj.heater = dTable.Rows[0]["HEATER"].ConvertToString();
                        obj.freeze = dTable.Rows[0]["FREEZE"].ConvertToString();
                        obj.tv = dTable.Rows[0]["TV"].ConvertToString();
                        obj.computer = dTable.Rows[0]["COMPUTER"].ConvertToString();
                        obj.internet = dTable.Rows[0]["INTERNET"].ConvertToString();
                        obj.landline = dTable.Rows[0]["LANDLINE"].ConvertToString();
                        obj.tractor = dTable.Rows[0]["TRACTOR"].ConvertToString();
                        obj.cart = dTable.Rows[0]["CART"].ConvertToString();
                        obj.pump = dTable.Rows[0]["PUMP"].ConvertToString();
                        obj.generator = dTable.Rows[0]["GENERATOR"].ConvertToString();
                        obj.bicycle = dTable.Rows[0]["BICYCLE"].ConvertToString();
                        obj.bike = dTable.Rows[0]["BIKE"].ConvertToString();
                        obj.car = dTable.Rows[0]["CAR"].ConvertToString();
                        obj.landOwner = dTable.Rows[0]["LAND_OWNER"].ConvertToString();
                        obj.landOwnerCnt = dTable.Rows[0]["LAND_OWNER_CNT"].ConvertToString();
                        obj.landInBiga = dTable.Rows[0]["LAND_IN_BIGA"].ConvertToString();
                        obj.landInRopani = dTable.Rows[0]["LAND_IN_ROPANI"].ConvertToString();
                        obj.landInKattha = dTable.Rows[0]["LAND_IN_KATTHA"].ConvertToString();
                        obj.landInAana = dTable.Rows[0]["LAND_IN_AANA"].ConvertToString();
                        obj.birds = dTable.Rows[0]["BIRDS"].ConvertToString();
                        obj.sheep = dTable.Rows[0]["SHEEP"].ConvertToString();
                        obj.deathInAYear = dTable.Rows[0]["DEATH_IN_A_YEAR"].ConvertToString();
                        obj.childInSchool = dTable.Rows[0]["CHILD_IN_SCHOOL"].ConvertToString();
                        obj.socialAllowance = dTable.Rows[0]["SOCIAL_ALLOWANCE"].ConvertToString();
                        obj.remarks = dTable.Rows[0]["REMARKS"].ConvertToString();
                        obj.remarksLoc = dTable.Rows[0]["REMARKS_LOC"].ConvertToString();



                        obj.fuelSourceOthEngI = dTable.Rows[0]["FUEL_SOURCE_OTH_ENG_I"].ConvertToString();
                        obj.fuelSourceOthNepI = dTable.Rows[0]["FUEL_SOURCE_OTH_NEP_I"].ConvertToString();
                        obj.fuelSourceOthEngII = dTable.Rows[0]["FUEL_SOURCE_OTH_ENG_II"].ConvertToString();
                        obj.fuelSourceOthNepII = dTable.Rows[0]["FUEL_SOURCE_OTH_NEP_II"].ConvertToString();
                        obj.LightSourceOthEngI = dTable.Rows[0]["LIGHT_SOURCE_OTH_ENG_I"].ConvertToString();
                        obj.LightSourceOthNepI = dTable.Rows[0]["LIGHT_SOURCE_OTH_NEP_I"].ConvertToString();
                        obj.LightSourceOthEngII = dTable.Rows[0]["LIGHT_SOURCE_OTH_ENG_II"].ConvertToString();
                        obj.LightSourceOthNepII = dTable.Rows[0]["LIGHT_SOURCE_OTH_NEP_II"].ConvertToString();
                        obj.ExMaterialOthEngI = dTable.Rows[0]["EX_WALLS_OTH_ENG_I"].ConvertToString();
                        obj.ExMaterialOthNepI = dTable.Rows[0]["EX_WALLS_OTH_NEP_I"].ConvertToString();
                        obj.ExMaterialOthEngII = dTable.Rows[0]["EX_WALLS_OTH_ENG_II"].ConvertToString();
                        obj.ExMaterialOthNepII = dTable.Rows[0]["EX_WALLS_OTH_NEP_II"].ConvertToString();
                        // obj.roofMaterialOthEngI=dTable.Rows[0]["REMARKS_LOC"].ConvertToString();
                        //obj.roofMaterialOthNepI=dTable.Rows[0]["REMARKS_LOC"].ConvertToString();

                        obj.WaterSourceOthEngI = dTable.Rows[0]["WATER_SOURCE_OTH_ENG_I"].ConvertToString();
                        obj.WaterSourceOthNepI = dTable.Rows[0]["WATER_SOURCE_OTH_NEP_I"].ConvertToString();
                        obj.WaterSourceOthEngIi = dTable.Rows[0]["WATER_SOURCE_OTH_ENG_II"].ConvertToString();
                        obj.WaterSourceOthNepIi = dTable.Rows[0]["WATER_SOURCE_OTH_NEP_II"].ConvertToString();
                        obj.ToiletTypeOthEngI = dTable.Rows[0]["TOILET_TYPE_OTH_ENG_I"].ConvertToString();
                        obj.ToiletTypeOthNepI = dTable.Rows[0]["TOILET_TYPE_OTH_NEP_I"].ConvertToString();
                        obj.ToiletTypeOthEngIi = dTable.Rows[0]["TOILET_TYPE_OTH_ENG_II"].ConvertToString();
                        obj.ToiletTypeOthNepIi = dTable.Rows[0]["TOILET_TYPE_OTH_NEP_II"].ConvertToString();




                    }

                }
                catch (Exception ex)
                {
                    obj = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (Service.Transaction.Connection != null)
                    {
                        Service.End();
                    }
                }
            }
            return obj;

        }
        public MISHouseholdInfo FillHousehold(string householdId)
        {
            CommonFunction commFunction = new CommonFunction();
            MISHouseholdInfo obj = new MISHouseholdInfo();
            DataTable dTable = new DataTable();
            dTable = null;
            using (ServiceFactory Service = new ServiceFactory())
            {
                try
                {
                    Service.Begin();
                    string CmdText = "select * from MIS_HOUSEHOLD_INFO T1 where T1.HOUSEHOLD_ID='" + householdId + "'";
                    dTable = Service.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }
                    });

                    Dictionary<string, string> colMappings = CreateMappings(dTable);
                    foreach (PropertyInfo p in obj.GetType().GetProperties())
                    {
                        try
                        {
                            string tempKey = UIPropertyMapppings[p.Name];
                            if (colMappings.ContainsKey(tempKey))
                                p.SetValue(obj, dTable.Rows[0][colMappings[tempKey]] == DBNull.Value ? "" : dTable.Rows[0][colMappings[tempKey]].ConvertToString(), null);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.AppendLog(ex);
                        }
                    }

                    obj.exWallsMaterialCdI = commFunction.GetDefinedCodeFromDataBase(obj.exWallsMaterialCdI, "MIS_MATERIAL", "MATERIAL_CD").ConvertToString();
                    obj.exWallsMaterialCdIi = commFunction.GetDefinedCodeFromDataBase(obj.exWallsMaterialCdIi, "MIS_MATERIAL", "MATERIAL_CD").ConvertToString();// household..ToInt32();
                    obj.roofMaterialCd = commFunction.GetDefinedCodeFromDataBase(obj.roofMaterialCd, "MIS_ROOF_MATERIAL", "MATERIAL_CD").ConvertToString();// household..ToInt32();
                    obj.waterSourceCdI = commFunction.GetDefinedCodeFromDataBase(obj.waterSourceCdI, "mis_Water_Source", "WATER_SOURCE_CD").ConvertToString();// household..ToInt32();
                    obj.waterSourceCdIi = commFunction.GetDefinedCodeFromDataBase(obj.waterSourceCdIi, "mis_Water_Source", "WATER_SOURCE_CD").ConvertToString();// household..ToInt32();
                    obj.toiletTypeCdI = commFunction.GetDefinedCodeFromDataBase(obj.toiletTypeCdI, "MIS_TOILET_TYPE", "TOILET_TYPE_CD").ConvertToString();// household..ToInt32();
                    obj.toiletTypeCdIi = commFunction.GetDefinedCodeFromDataBase(obj.toiletTypeCdIi, "MIS_TOILET_TYPE", "TOILET_TYPE_CD").ConvertToString();// household..ToInt32();
                    obj.lightSourceCdI = commFunction.GetDefinedCodeFromDataBase(obj.lightSourceCdI, "mis_light_source", "LIGHT_SOURCE_CD").ConvertToString();// household..ToInt32();
                    obj.lightSourceCdIi = commFunction.GetDefinedCodeFromDataBase(obj.lightSourceCdIi, "mis_light_source", "LIGHT_SOURCE_CD").ConvertToString();// household..ToInt32();
                    obj.fuelSourceCdI = commFunction.GetDefinedCodeFromDataBase(obj.fuelSourceCdI, "mis_fuel_source", "FUEL_SOURCE_CD").ConvertToString();// household..ToInt32();
                    obj.fuelSourceCdIi = commFunction.GetDefinedCodeFromDataBase(obj.fuelSourceCdIi, "mis_fuel_source", "FUEL_SOURCE_CD").ConvertToString();// household.fuelSourceCdIi.ToInt32();

                }
                catch (Exception)
                {
                    obj = null;
                }
                finally
                {
                    if (Service.Transaction.Connection != null)
                    {
                        Service.End();
                    }
                }
            }
            return obj;
        }
        public DataTable GetUserList(string userCode, string userName)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select T1.USR_CD as UserCode, T1.usr_Name as UserName  from COM_WEB_USR T1 ";

                cmdText += " Where Upper(T1.USR_CD) like '%" + userCode.ToUpper() + "%' AND Upper(T1.usr_Name) like '%" + userName.ToUpper() + "%'";


                //where T1.GROUP_FLAG='D' and T1.DISABLED='N'";

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
            return dt;
        }
        private Dictionary<string, string> UIPropertyMapppings = new Dictionary<string, string>();
        public Dictionary<string, string> CreateMappings(DataTable dt)
        {
            Dictionary<string, string> colMappings = new Dictionary<string, string>();
            foreach (DataColumn dc in dt.Columns)
            {
                colMappings.Add(dc.ColumnName.ToLower().Replace("_", ""), dc.ColumnName);
            }
            foreach (PropertyInfo p in typeof(MISHouseholdInfo).GetProperties())
                UIPropertyMapppings.Add(p.Name, p.Name.ToLower());
            return colMappings;
        }
        public string getUserName(string userId)
        {
            DataTable dtbl = null;
            string UserName = "";
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select T1.Usr_Name from COM_WEB_USR T1  WHERE T1.USR_CD='" + userId + "'";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
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
            if (dtbl != null)
            {
                UserName = dtbl.Rows[0][0].ConvertToString();
            }
            return UserName;
        }

        #region DropdownPopulate
        /// <summary>
        /// Get Wall Material
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public List<SelectListItem> GetOuterWallMaterial(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_MATERIAL T1 where T1.GROUP_FLAG='D' and T1.DISABLED='N' order by Material_cd";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Outer Wall Material") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["Defined_cd"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["Defined_cd"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }
        /// <summary>
        /// Get Roof Material
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns>List{defined Code/Description}</returns>
        /// 
        public List<SelectListItem> GetSecondaryOccupancy(string selectedValue)
        {
            List<SelectListItem> listSecondaryOccupancy = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_SECONDARY_OCCUPANCY T1 where T1.DISABLED='N' order by SEC_OCCUPANCY_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listSecondaryOccupancy.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Secondary Occupancy Type") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["SEC_OCCUPANCY_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listSecondaryOccupancy.Add(li);
                        if ((dr["SEC_OCCUPANCY_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listSecondaryOccupancy;
        }

        public List<SelectListItem> GetGeoTechnicalRisk(string selectedValue)
        {
            List<SelectListItem> listTechnicalRisk = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_GEOTECHNICAL_RISK_TYPE T1 where T1.DISABLED='N' order by GEOTECHNICAL_RISK_TYPE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listTechnicalRisk.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Geo Technical Risk Type") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["GEOTECHNICAL_RISK_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listTechnicalRisk.Add(li);
                        if ((dr["GEOTECHNICAL_RISK_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listTechnicalRisk;
        }

        public List<SelectListItem> GetDamageGrade(string selectedValue)
        {
            List<SelectListItem> listDamageGrade = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_DAMAGE_GRADE T1 where T1.DISABLED='N' order by DAMAGE_GRADE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listDamageGrade.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Damage Grade") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DAMAGE_GRADE_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listDamageGrade.Add(li);
                        if ((dr["DAMAGE_GRADE_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listDamageGrade;
        }
        public List<SelectListItem> getHouseholdLegalOwner(string selectedValue)
        {
            List<SelectListItem> lstLandLegalOWner = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_HOUSE_LAND_LEGAL_OWNER T1 where T1.DISABLED='N' order by HOUSE_LAND_LEGAL_OWNERCD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    lstLandLegalOWner.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Household Land Legal OWner Type") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        lstLandLegalOWner.Add(li);
                        if ((dr["HOUSE_LAND_LEGAL_OWNERCD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return lstLandLegalOWner;





        }
        public List<SelectListItem> GetGroundSurfaceType(string selectedValue)
        {
            List<SelectListItem> listGroundSurface = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_GROUND_SURFACE T1 where T1.DISABLED='N' order by GROUND_SURFACE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listGroundSurface.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Ground Surface Type") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["GROUND_SURFACE_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listGroundSurface.Add(li);
                        if ((dr["GROUND_SURFACE_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listGroundSurface;
        }
        public List<SelectListItem> GetFloorsOtherThanGround(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_STOREY_CONSRUCT_MATERIAL T1 where T1.DISABLED='N' order by SC_MATERIAL_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Construction Material of Upper Floors") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["SC_MATERIAL_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["SC_MATERIAL_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }
        public List<SelectListItem> GetTemproraryResidence(string selectedValue)
        {
            List<SelectListItem> listTemproraryResidence = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_SHELTER_SINCE_QUAKE T1 where T1.DISABLED='N' order by SHELTER_SINCE_QUAKE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listTemproraryResidence.Add(li1);
                    listTemproraryResidence.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Current Shelter") + " ---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["SSEQ_DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listTemproraryResidence.Add(li);
                        if ((dr["SSEQ_DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }

                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listTemproraryResidence;
        }
        public List<SelectListItem> GetNonInterviewingReason(string selectedValue)
        {
            List<SelectListItem> listNonInterviewingReason = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_NOT_INTERVIWING_REASON T1 where  T1.DISABLED='N' order by NOT_INTERVIWING_REASON_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listNonInterviewingReason.Add(li1);
                    listNonInterviewingReason.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Non Interview Reason") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["Defined_cd"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listNonInterviewingReason.Add(li);
                        if ((dr["Defined_cd"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listNonInterviewingReason;
        }

        public List<SelectListItem> GetPresenceStatus(string selectedValue)
        {
            List<SelectListItem> listPresenceStatus = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_PRESENCE_STATUS T1 where  T1.DISABLED='N' order by PRESENCE_STATUS_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("-Select Presence Status--");
                    //li1.Value = "";
                    //listPresenceStatus.Add(li1);
                    listPresenceStatus.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Presence Status") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["PRESENCE_STATUS_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listPresenceStatus.Add(li);
                        if ((dr["PRESENCE_STATUS_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listPresenceStatus;
        }

        public List<SelectListItem> GetRoofMaterial(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ROOF_CONSTRUCT_MATERIAL T1 where T1.DISABLED='N' order by RC_MATERIAL_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Roof Construct Material") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["RC_MATERIAL_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["RC_MATERIAL_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }
        public List<SelectListItem> GetHousePosition(string selectedValue)
        {
            List<SelectListItem> listHousePosition = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_BUILDING_POSITION_CONFIG T1 where T1.DISABLED='N' order by BUILDING_POSITION_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listHousePosition.Add(li1);
                    listHousePosition.Add(new SelectListItem { Value="",Text="---" + Utils.GetLabel("Select House Position") + "---"});
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["BUILDING_POSITION_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listHousePosition.Add(li);
                        if ((dr["BUILDING_POSITION_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listHousePosition;
        }

        public List<SelectListItem> GetHouseStructure(string selectedValue)
        {
            List<SelectListItem> listHouseStructure = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_SUPERSTRUCTURE_MATERIAL T1 where T1.DISABLED='N' order by SUPERSTRUCTURE_MATERIAL_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listHouseStructure.Add(li1);
                    listHouseStructure.Add(new SelectListItem {Value="",Text="---" + Utils.GetLabel("Select House Structure") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["SUPERSTRUCTURE_MATERIAL_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listHouseStructure.Add(li);
                        if ((dr["SUPERSTRUCTURE_MATERIAL_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listHouseStructure;
        }

        public List<SelectListItem> GetBuildingConditionAfterQuake(string selectedValue)
        {
            List<SelectListItem> listBuildingCondition = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_BUILDING_CONDITION T1 where T1.DISABLED='N' order by BUILDING_CONDITION_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listBuildingCondition.Add(li1);
                    listBuildingCondition.Add(new SelectListItem {Value="",Text="---" + Utils.GetLabel("Select Building Condition After Quake") + "---"});
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["BUILDING_CONDITION_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listBuildingCondition.Add(li);
                        if ((dr["BUILDING_CONDITION_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listBuildingCondition;
        }
        public List<SelectListItem> GetBuildingConditionAfterEarthQuake(string selectedValue)
        {
            List<SelectListItem> listBuildingCondition = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_OTH_HOUSE_CONDITION T1 order by BUILDING_CONDITION_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listBuildingCondition.Add(li1);
                    listBuildingCondition.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Building Condition After Quake") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["BUILDING_CONDITION_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listBuildingCondition.Add(li);
                        if ((dr["BUILDING_CONDITION_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listBuildingCondition;
        }
        public List<SelectListItem> GetFoundationType(string selectedValue)
        {
            List<SelectListItem> listFoundationType = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_FOUNDATION_TYPE T1 where T1.DISABLED='N' order by FOUNDATION_TYPE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listFoundationType.Add(li1);
                    listFoundationType.Add(new SelectListItem {Value="",Text="---" + Utils.GetLabel("Select Foundation Type") + "---"});
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        //SelectListItem li = new SelectListItem();
                        //li.Value = (dr["FOUNDATION_TYPE_DEF_CD"].ConvertToString());
                        //li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        //listFoundationType.Add(li);
                        //if ((dr["FOUNDATION_TYPE_DEF_CD"].ConvertToString()) == selectedValue)
                        //{
                        //    li.Selected = true;
                        //}
                        listFoundationType.Add(new SelectListItem { Value = (dr["FOUNDATION_TYPE_DEF_CD"].ConvertToString()), 
                                                                    Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString()),
                                                                    Selected=(dr["FOUNDATION_TYPE_DEF_CD"].ConvertToString()) == selectedValue ?true:false });
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listFoundationType;
        }

        public List<SelectListItem> GetFloorMaterial(string selectedValue)
        {
            List<SelectListItem> listFloorMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_FLOOR_CONSTRUCT_MATERIAL T1 where T1.DISABLED='N' order by FC_MATERIAL_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listFloorMaterial.Add(li1);
                    listFloorMaterial.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Floor Material") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["FC_MATERIAL_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listFloorMaterial.Add(li);
                        if ((dr["FC_MATERIAL_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listFloorMaterial;
        }

        public List<SelectListItem> GetHousePlan(string selectedValue)
        {
            List<SelectListItem> listHousePlan = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_BUILDING_PLAN_CONFIG T1 where T1.DISABLED='N' order by BUILDING_PLAN_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listHousePlan.Add(li1);
                    listHousePlan.Add(new SelectListItem { Value="",Text="---" + Utils.GetLabel("Select House Plan") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["BUILDING_PLAN_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listHousePlan.Add(li);
                        if ((dr["BUILDING_PLAN_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listHousePlan;
        }

        public List<SelectListItem> GetTechnicalSolution(string selectedValue)
        {
            List<SelectListItem> listHousePosition = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_TECHNICAL_SOLUTION T1 where T1.DISABLED='N' order by TECHSOLUTION_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listHousePosition.Add(li1);
                    listHousePosition.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Technical Solution") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["TECHSOLUTION_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listHousePosition.Add(li);
                        if ((dr["TECHSOLUTION_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listHousePosition;
        }
        public List<SelectListItem> GetHouseMonitoredPosition(string selectedValue)
        {
            List<SelectListItem> listHousePosition = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_ASSESSED_AREA T1 where T1.DISABLED='N' order by ASSESSED_AREA_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listHousePosition.Add(li1);
                    listHousePosition.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select House Monitored Position") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["ASSESSED_AREA_DEF_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listHousePosition.Add(li);
                        if ((dr["ASSESSED_AREA_DEF_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listHousePosition;
        }

        /// <summary>
        /// Get Water Source
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns>List{defined Code/Description}</returns>
        public List<SelectListItem> GetWaterSource(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_WATER_SOURCE T1 where T1.GROUP_FLAG='D' and T1.DISABLED='N' order by Water_source_cd";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Drinking Water Source") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["Defined_Cd"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["Defined_Cd"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }
        /// <summary>
        /// Get Toilet Type
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns>List{defined Code/Description}</returns>
        public List<SelectListItem> GetToiletType(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_TOILET_TYPE T1 where T1.GROUP_FLAG='D' and T1.DISABLED='N'  order by Toilet_type_Cd";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Toilet Type") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["Defined_cd"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["Defined_cd"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }
        /// <summary>
        /// Get Fuel Source
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns>List{defined Code/Description}</returns>
        public List<SelectListItem> GetFuelSource(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_Fuel_source T1 where T1.GROUP_FLAG='D' and T1.DISABLED='N' order by Fuel_source_cd";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    //SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "--- " + Utils.GetLabel("Select Fuel Source") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["Defined_cd"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["Defined_cd"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }
        /// <summary>
        /// Light Source
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns>List{defined Code/Description}</returns>
        public List<SelectListItem> GetLightSource(string selectedValue)
        {
            List<SelectListItem> listMaterial = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from MIS_Light_source T1 where T1.GROUP_FLAG='D' and T1.DISABLED='N' order by Light_Source_Cd";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    listMaterial.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Light Source") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["Defined_cd"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        listMaterial.Add(li);
                        if ((dr["Defined_cd"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listMaterial;
        }

        /// <summary>
        /// Get Document Type
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns>List{defined Code/Description}</returns>
        public List<SelectListItem> GetDocumentType(string selectedValue)
        {
            List<SelectListItem> listDocument = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select IDENTIFICATION_TYPE_CD," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " DESCRIPTION FROM NHRS_IDENTIFICATION_TYPE where 1=1";


                try
                {
                    dtbl = service.GetDataTable(cmdText, null);

                      foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["IDENTIFICATION_TYPE_CD"].ConvertToString());
                        li.Text = dr["DESCRIPTION"].ConvertToString();
                        listDocument.Add(li);
                        if ((dr["IDENTIFICATION_TYPE_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return listDocument;
        }
        #endregion
        public List<SelectListItem> getShelterBeforeEQ(string selectedValue)
        {
            List<SelectListItem> ListShelter = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_SHELTER_BEFORE_QUAKE T1 where T1.DISABLED='N' order by SHELTER_BEFORE_QUAKE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    //li1.Text = Utils.GetLabel("Please Select");
                    //li1.Value = "";
                    //listMaterial.Add(li1);
                    ListShelter.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter Before Earthquake") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListShelter.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListShelter;
        }

        public List<SelectListItem> getShelterSinceEQ(string selectedValue)
        {
            List<SelectListItem> ListShelter = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_SHELTER_SINCE_QUAKE T1 where T1.DISABLED='N' order by SHELTER_SINCE_QUAKE_CD";
                try
                {            dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListShelter.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter Since Earthquake") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["SSEQ_DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListShelter.Add(li);
                        if ((dr["SSEQ_DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListShelter;
        }

        
        public List<SelectListItem> getEQVictimIdentityCard(string selectedValue)
        {
            List<SelectListItem> ListEQVictimCard = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_EQ_VICTIM_IDENTITY_CARD T1 where T1.DISABLED='N' order by EQ_VICTIM_IDENTITY_CARD_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListEQVictimCard.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Earthquake Victim Identity Card") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListEQVictimCard.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListEQVictimCard;
        }
        public List<SelectListItem> getEQReliefFund(string selectedValue)
        {
            List<SelectListItem> ListEQReliefFund = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_EQ_RELIEF_MONEY T1 where T1.DISABLED='N' order by EQ_RELIEF_MONEY_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListEQReliefFund.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Earthquake Relief Money") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListEQReliefFund.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListEQReliefFund;
        }
        public List<SelectListItem> GetMonthlyIncome(string selectedValue)
        {
            List<SelectListItem> ListMonthlyIncome = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_MONTHLY_INCOME T1 where T1.DISABLED='N' order by MONTHLY_INCOME_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListMonthlyIncome.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Monthly Income") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListMonthlyIncome.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListMonthlyIncome;
        }
        public List<SelectListItem> GetAllBank(string selectedValue)
        {
            List<SelectListItem> ListBank = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_BANK T1 where T1.DISABLED='N' order by BANK_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListBank.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListBank.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListBank;
        }
        public List<SelectListItem> GetAllBankBranch(string selectedValue)
        {
            List<SelectListItem> ListBankBranch = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_BANK_BRANCH order by BANK_BRANCH_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListBankBranch.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank Branch") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListBankBranch.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListBankBranch;
        }

        public List<SelectListItem> GetIdentificationType(string selectedValue)
        {
            List<SelectListItem> ListIdentificationType = new List<SelectListItem>();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                cmdText = "select * from NHRS_IDENTIFICATION_TYPE order by IDENTIFICATION_TYPE_CD";

                try
                {
                    dtbl = service.GetDataTable(cmdText, null);
                    SelectListItem li1 = new SelectListItem();
                    ListIdentificationType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Identification Type") + "---" });
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        SelectListItem li = new SelectListItem();
                        li.Value = (dr["DEFINED_CD"].ConvertToString());
                        li.Text = Utils.ToggleLanguage(dr["DESC_ENG"].ConvertToString(), dr["DESC_LOC"].ConvertToString());
                        ListIdentificationType.Add(li);
                        if ((dr["DEFINED_CD"].ConvertToString()) == selectedValue)
                        {
                            li.Selected = true;
                        }
                    }
                }

                catch (Exception ex)
                {
                    dtbl = null;
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
            return ListIdentificationType;
        }
    }
}
