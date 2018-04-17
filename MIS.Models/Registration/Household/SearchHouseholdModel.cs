using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Registration.Household
{
    public class SearchHouseholdModel
    {
        #region OldCode
        //public String householdId { get; set; }
        //public String definedCd { get; set; }
        //public String formNo { get; set; }
        //public String memmberDefinedCode { get; set; }
        //public String memberId { get; set; }

        //public String memberCnt { get; set; }

        //[RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        //public String interviewYear { get; set; }
        //[RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        //public String interviewMonth { get; set; }
        //[RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        //public String interviewdateDay { get; set; }
        //[RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        //public String interviewYearLoc { get; set; }
        //[RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        //public String interviewMonthLoc { get; set; }
        //[RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        //public String interviewdateDayLoc { get; set; }
        //public String interviewDt { get; set; }
        //public String interviewDtLoc { get; set; }
        //public String interviewDay { get; set; }
        //public String interviewStHh { get; set; }
        //public String interviewStMm { get; set; }
        //public String interviewEndHh { get; set; }
        //public String interviewEndMm { get; set; }
        //public String perCountryCd { get; set; }
        //public String perRegStCd { get; set; }
        //public String perZoneCd { get; set; }
        //public String perDistrictCd { get; set; }
        //public String perVdcMunCd { get; set; }
        //public String perWardNo { get; set; }
        //public String perAreaEng { get; set; }
        //public String perAreaLoc { get; set; }
        //public String curCountryCd { get; set; }
        //public String curRegStCd { get; set; }
        //public String curZoneCd { get; set; }
        //public String curDistrictCd { get; set; }
        //public String curVdcMunCd { get; set; }
        //public String curWardNo { get; set; }
        //public String curAreaEng { get; set; }
        //public String curAreaLoc { get; set; }
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Numbers only!!")]
        //public String houseNo { get; set; }
        //public String telNo { get; set; }
        //public String mobileNo { get; set; }
        //public String fax { get; set; }
        //public String email { get; set; }
        //public String url { get; set; }
        //public String poBoxNo { get; set; }
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Numbers only!!")]
        //public String habitantYear { get; set; }
        //public String habitantMonth { get; set; }
        //public String habitantDay { get; set; }
        //public String ancestralHome { get; set; }
        //public String hospitalDistanceHr { get; set; }
        //public String hospitalDistanceMin { get; set; }
        //public String marketDistanceHr { get; set; }
        //public String marketDistanceMin { get; set; }
        //public String ownDwelling { get; set; }
        //public String bedroom { get; set; }
        //public String bedroomCnt { get; set; }
        //public String exWallsMaterialCdI { get; set; }
        //public String exWallsMaterialCdIi { get; set; }
        //public String roofMaterialCd { get; set; }
        //public String waterSourceCdI { get; set; }
        //public String waterSourceCdIi { get; set; }
        //public String waterDistanceHr { get; set; }
        //public String waterDistanceMin { get; set; }
        //public String toiletTypeCdI { get; set; }
        //public String toiletTypeCdIi { get; set; }
        //public String toiletShared { get; set; }
        //public String lightSourceCdI { get; set; }
        //public String lightSourceCdIi { get; set; }
        //public String fuelSourceCdI { get; set; }
        //public String fuelSourceCdIi { get; set; }
        //public String fan { get; set; }
        //public String heater { get; set; }
        //public String freeze { get; set; }
        //public String tv { get; set; }
        //public String computer { get; set; }
        //public String internet { get; set; }
        //public String landline { get; set; }
        //public String tractor { get; set; }
        //public String cart { get; set; }
        //public String pump { get; set; }
        //public String generator { get; set; }
        //public String bicycle { get; set; }
        //public String bike { get; set; }
        //public String car { get; set; }
        //public String landOwner { get; set; }
        //public String landOwnerCnt { get; set; }
        //public String landInBiga { get; set; }
        //public String landInRopani { get; set; }
        //public String landInKattha { get; set; }
        //public String landInAana { get; set; }
        //public String birds { get; set; }
        //public String sheep { get; set; }
        //public String deathInAYear { get; set; }
        //public String childInSchool { get; set; }
        //public String socialAllowance { get; set; }
        //public String remarks { get; set; }
        //public String remarksLoc { get; set; }
        //public String ruleCalc { get; set; }
        //public String ruleFlag { get; set; }
        //public String extraI { get; set; }
        //public String extraIi { get; set; }
        //public String extraIii { get; set; }
        //public String extraIv { get; set; }
        //public String extraV { get; set; }
        //public String approved { get; set; }
        //public String approvedBy { get; set; }
        //public String approvedByLoc { get; set; }
        //public String approvedDt { get; set; }
        //public String approvedDtLoc { get; set; }
        //public String updatedBy { get; set; }
        //public String updatedByLoc { get; set; }
        //public String updatedDt { get; set; }
        //public String updatedDtLoc { get; set; }
        //public String enteredBy { get; set; }
        //public String enteredByLoc { get; set; }
        //public String enteredDt { get; set; }
        //public String enteredDtLoc { get; set; }
        //public String Mode { get; set; }
        //// public String FirstName

        //public string perDistrict { get; set; }

        //public string perVDc { get; set; }

        //public string perWard { get; set; }


        //public string TempDistrict { get; set; }

        //public string tempVDCmun { get; set; }

        //public string tempWard { get; set; }

        //public string exWallMaterial { get; set; }

        //public string roofMaterial { get; set; }

        //public string sourceDirnkingI { get; set; }

        //public string sourceDrinkingII { get; set; }

        //public string toiletTypeI { get; set; }

        //public string toiletTypeII { get; set; }

        //public string lightSourceI { get; set; }

        //public string lightSourceII { get; set; }

        //public string fuelSourceI { get; set; }

        //public string fuelSourceII { get; set; }

        //public String perCountry { get; set; }

        //public string perReg { get; set; }

        //public string perZone { get; set; }

        //public string tempCountry { get; set; }

        //public string tempReg { get; set; }

        //public string tempZone { get; set; }

        //public string exWallMaterialII { get; set; }

        //public string roofMaterialII { get; set; }

        //public string ImageURL { get; set; }
        //public string ruleValue { get; set; }

        //public String interviewDtto { get; set; }
        //[RegularExpression("[1-9][0-9][0-9][0-9]", ErrorMessage = " ")]
        //public String interviewYearto { get; set; }
        //[RegularExpression("[0-1][0-9]", ErrorMessage = " ")]
        //public String interviewMonthto { get; set; }
        //[RegularExpression("[0-3][0-9]", ErrorMessage = " ")]
        //public String interviewdateDayto { get; set; }


        //public String firstNameEngSearch { get; set; }
        //public String middleNameEngSearch { get; set; }
        //public String lastNameEngSearch { get; set; }
        //public String firstNameLocSearch { get; set; }
        //public String middleNameLocSearch { get; set; }
        //public String lastNameLocSearch { get; set; }
        //public String interviewedBySearch { get; set; }
        #endregion
       
        public String HEAD_DEFINED_CD { get; set; }
        public String HOUSEHOLD_ID { get; set; }
        //Extra Filed added
        public String FORM_NO_FROM { get; set; }
        public String FORM_NO_TO { get; set; }
        //
        public String MEMBER_ID { get; set; }
        public String HEAD_FULL_NAME_ENG { get; set; }
        public String HEAD_FULL_NAME_LOC { get; set; }
        public String BIRTH_DT { get; set; }
        public String BIRTH_DT_LOC { get; set; }
        public String CTZ_NO { get; set; }
        public String FIRST_NAME_ENG { get; set; }
        public String MIDDLE_NAME_ENG { get; set; }
        public String LAST_NAME_ENG { get; set; }
        public String FULL_NAME_ENG { get; set; }
        public String FIRST_NAME_LOC { get; set; }
        public String MIDDLE_NAME_LOC { get; set; }
        public String LAST_NAME_LOC { get; set; }
        public String FULL_NAME_LOC { get; set; }
        public String FATHER_FNAME_ENG { get; set; }
        public String FATHER_MNAME_ENG { get; set; }
        public String FATHER_LNAME_ENG { get; set; }
        public String FATHER_FULL_NAME_ENG { get; set; }
        public String FATHER_FNAME_LOC { get; set; }
        public String FATHER_MNAME_LOC { get; set; }
        public String FATHER_LNAME_LOC { get; set; }
        public String FATHER_FULLNAME_LOC { get; set; }
        public String MOTHER_FNAME_ENG { get; set; }
        public String MOTHER_MNAME_ENG { get; set; }
        public String MOTHER_LNAME_ENG { get; set; }
        public String MOTHER_FULL_NAME_ENG { get; set; }
        public String MOTHER_FNAME_LOC { get; set; }
        public String MOTHER_MNAME_LOC { get; set; }
        public String MOTHER_LNAME_LOC { get; set; }
        public String MOTHER_FULLNAME_LOC { get; set; }
        public String GFATHER_FNAME_ENG { get; set; }
        public String GFATHER_MNAME_ENG { get; set; }
        public String GFATHER_LNAME_ENG { get; set; }
        public String GFATHER_FULL_NAME_ENG { get; set; }
        public String GFATHER_FNAME_LOC { get; set; }
        public String GFATHER_MNAME_LOC { get; set; }
        public String GFATHER_LNAME_LOC { get; set; }
        public String GFATHER_FULLNAME_LOC { get; set; }
        public String SPOUSE_FNAME_ENG { get; set; }
        public String SPOUSE_MNAME_ENG { get; set; }
        public String SPOUSE_LNAME_ENG { get; set; }
        public String SPOUSE_FULL_NAME_ENG { get; set; }
        public String SPOUSE_FNAME_LOC { get; set; }
        public String SPOUSE_MNAME_LOC { get; set; }
        public String SPOUSE_LNAME_LOC { get; set; }
        public String SPOUSE_FULLNAME_LOC { get; set; }
        public String MARITAL_STATUS_ENG { get; set; }
        public String MARITAL_STATUS_LOC { get; set; }
        public String GENDER_ENG { get; set; }
        public String GENDER_LOC { get; set; }
        public String HEAD_AGE { get; set; }
        public String MEMBER_CNT { get; set; }
        public String INTERVIEWED_BY { get; set; }
        public String INTERVIEW_DT_TO { get; set; }
        public String INTERVIEW_DT { get; set; }
        public String INTERVIEW_DT_LOC { get; set; }
        public String PER_DISTRICT_CD { get; set; }
        public String PER_DISTRICT_ENG { get; set; }
        public String PER_DISTRICT_LOC { get; set; }
        public String PER_COUNTRY_ENG { get; set; }
        public String PER_COUNTRY_LOC { get; set; }
        public String PER_REGION_ENG { get; set; }
        public String PER_REGION_LOC { get; set; }
        public String PER_ZONE_ENG { get; set; }
        public String PER_ZONE_LOC { get; set; }
        public String PER_VDCMUNICIPILITY_CD { get; set; }
        public String PER_VDCMUNICIPILITY_ENG { get; set; }
        public String PER_VDCMUNICIPILITY_LOC { get; set; }
        public String PER_WARD_NO { get; set; }
        public String PER_AREA_ENG { get; set; }
        public String PER_AREA_LOC { get; set; }
        public String CUR_DISTRICT_CD { get; set; }
        public String CUR_DISTRICT_ENG { get; set; }
        public String CUR_DISTRICT_LOC { get; set; }
        public String CUR_COUNTRY_ENG { get; set; }
        public String CUR_COUNTRY_LOC { get; set; }
        public String CUR_REGION_STATE_ENG { get; set; }
        public String CUR_REGION_STATE_LOC { get; set; }
        public String CUR_ZONE_ENG { get; set; }
        public String CUR_ZONE_LOC { get; set; }
        public String CUR_VDC_MUNICIPILITY_CD { get; set; }
        public String CUR_VDC_MUNICIPILITY_ENG { get; set; }
        public String CUR_VDC_MUNICIPILITY_LOC { get; set; }
        public String CUR_WARD_NO { get; set; }
        public String RELATION { get; set; }
        public String CUR_AREA_ENG { get; set; }
        public String CUR_AREA_LOC { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String HOUSE_NO { get; set; }
        public String MOBILE_NO { get; set; }
        public String FAX { get; set; }
        public String EMAIL { get; set; }
        public String URL { get; set; }
        public String TEL_NO { get; set; }
        public String PO_BOX_NO { get; set; }
        public String HABITANT_YEAR { get; set; }
        public String HABITANT_MONTH { get; set; }
        public String HABITANT_DAY { get; set; }
        public String ANCESTRAL_HOME { get; set; }
        public String HOSPITAL_DISTANCE_HR { get; set; }
        public String HOSPITAL_DISTANCE_MIN { get; set; }
        public String MARKET_DISTANCE_HR { get; set; }
        public String MARKET_DISTANCE_MIN { get; set; }
        public String OWN_DWELLING { get; set; }
        public String BEDROOM { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String BEDROOM_CNT { get; set; }
        public string SC_MATERIAL_CD { get; set; }
        public String EX_WALLS_MATERIAL_CD_I_CD { get; set; }
        public String EX_WALLS_MATERIAL_CD_I_ENG { get; set; }
        public String EX_WALLS_MATERIAL_CD_I_LOC { get; set; }
        public String EX_WALLS_MATERIAL_CD_II_CD { get; set; }
        public String EX_WALLS_MATERIAL_CD_II_ENG { get; set; }
        public String EX_WALLS_MATERIAL_CD_II_LOC { get; set; }
        public String ROOF_MATERIAL_CD { get; set; }
        public String ROOF_MATERIAL_ENG { get; set; }
        public String ROOF_MATERIAL_LOC { get; set; }
        public String WATER_SOURCE_I_CD { get; set; }
        public String WATER_SOURCE_I_ENG { get; set; }
        public String WATER_SOURCE_I_LOC { get; set; }
        public String WATER_SOURCE_II_CD { get; set; }
        public String WATER_SOURCE_II_ENG { get; set; }
        public String WATER_SOURCE_II_LOC { get; set; }
        public String LIGHT_SOURCE_I_CD { get; set; }
        public String LIGHT_SOURCE_I_ENG { get; set; }
        public String LIGHT_SOURCE_I_LOC { get; set; }
        public String LIGHT_SOURCE_II_CD { get; set; }
        public String LIGHT_SOURCE_II_ENG { get; set; }
        public String LIGHT_SOURCE_II_LOC { get; set; }
        public String FUEL_SOURCE_I_CD { get; set; }
        public String FUEL_SOURCE_I_ENG { get; set; }
        public String FUEL_SOURCE_I_LOC { get; set; }
        public String FUEL_SOURCE_II_CD { get; set; }
        public String FUEL_SOURCE_II_ENG { get; set; }
        public String FUEL_SOURCE_II_LOC { get; set; }
        public String TOILET_TYPE_I_CD { get; set; }
        public String TOILET_TYPE_I_ENG { get; set; }
        public String TOILET_TYPE_I_LOC { get; set; }
        public String TOILET_TYPE_II_ENG { get; set; }
        public String TOILET_TYPE_II_LOC { get; set; }
        public String TOILET_SHARED { get; set; }
        public String WATER_DISTANCE_HR { get; set; }
        public String WATER_DISTANCE_MIN { get; set; }
        public String FAN { get; set; }
        public String HEATER { get; set; }
        public String FREEZE { get; set; }
        public String TV { get; set; }
        public String COMPUTER { get; set; }
        public String INTERNET { get; set; }
        public String LANDLINE { get; set; }
        public String TRACTOR { get; set; }
        public String CART { get; set; }
        public String PUMP { get; set; }
        public String GENERATOR { get; set; }
        public String BICYCLE { get; set; }
        public String BIKE { get; set; }
        public String CAR { get; set; }
        public String LAND_OWNER { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String LAND_OWNER_CNT { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String LAND_IN_BIGA { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String LAND_IN_ROPANI { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String LAND_IN_KATTHA { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String LAND_IN_AANA { get; set; }
        public String BIRDS { get; set; }
        public String SHEEP { get; set; }
        public String DEATH_IN_A_YEAR { get; set; }
        public String CHILD_IN_SCHOOL { get; set; }
        public String SOCIAL_ALLOWANCE { get; set; }
        public String REMARKS { get; set; }
        public String REMARKS_LOC { get; set; }
        public String ENTERED_BY { get; set; }
        public String ENTERED_BY_LOC { get; set; }
        public String ENTERED_DT { get; set; }
        public String APPROVED { get; set; }
        public String Caste_Group_Cd { get; set; }
        public String Interview_From_MIN { get; set; }
        public String Interview_From_HR { get; set; }
        public String Interview_To_MIN { get; set; }
        public String Interview_To_HR { get; set; }
        public String HOSPITAL_DISTANCE_FROM_HR { get; set; }
        public String HOSPITAL_DISTANCE_FROM_MIN { get; set; }
        public String HOSPITAL_DISTANCE_To_HR { get; set; }
        public String HOSPITAL_DISTANCE_To_MIN { get; set; }
        public String MARKET_DISTANCE_FROM_HR { get; set; }
        public String MARKET_DISTANCE_FROM_MIN { get; set; }
        public String MARKET_DISTANCE_TO_HR { get; set; }
        public String MARKET_DISTANCE_TO_MIN { get; set; }
        public String BEDROOM_FROM_CNT { get; set; }
        public String BEDROOM_TO_CNT { get; set; }
        public String WATER_DISTANCE_FROM_HR { get; set; }
        public String WATER_DISTANCE_FROM_MIN { get; set; }
        public String WATER_DISTANCE_TO_HR { get; set; }
        public String WATER_DISTANCE_TO_MIN { get; set; }
        public String LAND_IN_BIGA_From { get; set; }
        public String LAND_IN_BIGA_To { get; set; }
        public String LAND_IN_KATTHA_FROM { get; set; }
        public String LAND_IN_KATTHA_TO { get; set; }
        public String LAND_IN_ROPANI_FROM { get; set; }
        public String LAND_IN_ROPANI_TO { get; set; }
        public String LAND_IN_AANA_FROM { get; set; }
        public String LAND_IN_AANA_TO { get; set; }
        public static List<string> DropdownKeys()
        {
            return new List<string> { 
                    "ddl_Cur_Districts", "ddl_Per_Districts", "ddl_Ctz_Districts",
                    "ddl_Cur_VDCMun", "ddl_Per_VDCMun", "ddl_Ctz_VDCMun", 
                    "ddl_Cur_Ward", "ddl_Per_Ward", "ddl_Ctz_Ward", 
                    "ddl_Gender", "ddl_MaritalStatus", "ddl_Caste", 
                    "ddl_Religion", "ddl_Education", "ddl_Caste_Group"
                };
        }

       
    }
}
