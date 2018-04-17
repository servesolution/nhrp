using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Search
{
    public class NHRSSearch
    {
        private string strRegion = string.Empty;
        private string strZone = string.Empty;
        private string strDistrict = string.Empty;
        private string strVDC = string.Empty;
        private string strWard = string.Empty;
        private string strFullName = string.Empty;
        private string strFullNameLoc = string.Empty;
        private string strSearchMode = string.Empty;
        private string strHouseHoldSN = string.Empty;
        private string strGender = string.Empty;
        private string ageFrom = string.Empty;
        private string ageTo = string.Empty;
        private string damagegrade = string.Empty;
        private string techsolution = string.Empty;
        private string IMEI = string.Empty;
        private string mobile = string.Empty;
        private string strFullNameLOC = string.Empty;
        private string InterviewDTFrom = string.Empty;
        private string InterviewDTTo = string.Empty;
        private string RCMaterial = string.Empty;
        private string FCMaterial = string.Empty;
        private string SCMaterial = string.Empty;
        private string DamageGrade = string.Empty;
        private string DamageResolution = string.Empty;
        private string EnumeratorCd = string.Empty;
        private string HOwnerCount = string.Empty;
        private string NirCd = string.Empty;
        private string HllOwner = string.Empty;
        private string BuildingConditionCd = string.Empty;
        private string GroundSurfaceCd = string.Empty;
        private string FoundationCd = string.Empty;
        private string BuildinPositionCd = string.Empty;
        private string BuildingPlanCd = string.Empty;
        private string AssessedAreaCd = string.Empty;
        private string HouseholdId = string.Empty;
 	    private string FirstNameEng = string.Empty;
        private string FirstNameLoc = string.Empty;
        private string MiddleNameEng = string.Empty;
        private string MiddleNameLOC = string.Empty;
        private string LastNameEng = string.Empty;
        private string LastNameLoc = string.Empty;
        private string InterviewedBy = string.Empty;
        private string perAreaEng = string.Empty;
        private string perAreaLoc = string.Empty;
        private string curAreaEng = string.Empty;
        private string curAreaLoc = string.Empty;
        private string HouseNo = string.Empty;
        private string ExWallsMateriaCdI = string.Empty;
        private string WaterSourceCDII = string.Empty;
        private string RoofMaterialCd = string.Empty;
        private string WaterDistanceHr = string.Empty;
        private string WaterDistanceMin = string.Empty;
        private string ToiletTypeCdI = string.Empty;
        private string ToiletShared = string.Empty;
        private string LightSourceCdII = string.Empty;
        private string FuelSourceCDII = string.Empty;
        private string SessionID = string.Empty;
        private string MemebrID = string.Empty;
        private string HouseOWnerID = string.Empty;
        private string DefinedCd = string.Empty;
        private string BuildingStructureNo=string.Empty;
        private string RespondentFName = string.Empty;
        private string RespondentMName = string.Empty;
        private string RespondentLName = string.Empty;
        private string RespondentFullNameEng = string.Empty;
        private string RespondentFNameLoc = string.Empty;
        private string RespondentMNameLoc = string.Empty;
        private string RespondentLNameLoc = string.Empty;
        private string RespondentFullNameLoc = string.Empty;
        private string ShelterSinceQuakeCd = string.Empty;
        private string ShelterBeforeQuakeCd = string.Empty;
        private string CurrentShelterCd = string.Empty;
        private string EQvictimCardCd = string.Empty;
        private string MonthlyIncomeCd = string.Empty;
        private string DeathCntFrom = string.Empty;
        private string DeathCntTo = string.Empty;
        private string HumanDestroyCntFrom = string.Empty;
        private string HumanDestroyCntTo = string.Empty;
        private string StudentSchoolLeftCount = string.Empty;
        private string PregnantRegularCheckupCnt = string.Empty;
        private string ChildLeftVaccinationCount = string.Empty;
        private string LeftChangeOccupancyCount = string.Empty;
        private string EQReliefmoneyCd = string.Empty;
        private string IdentificationtypeCd = string.Empty;
        private string BankCd = string.Empty;
        private string BirthDateFrom = string.Empty;
        private string BirthDateTo = string.Empty;
        private string MaritalStatusCd = string.Empty;
        private string Age = string.Empty;
        private string CasteCd = string.Empty;
        private string ReligionCd = string.Empty;
        private string EducationCd = string.Empty;
        private string CtizenshipNo = string.Empty;
        private string CitizenshipIssueDistrict = string.Empty;
        private string CitizenshipIssueDateEng = string.Empty;
        private string CitizenshipIssueDateLoc = string.Empty;
        private string CurDistrictCd = string.Empty;
        private string CurVDCCd = string.Empty;
        private string CurWardNo = string.Empty;
        private string PerDistrictCd = string.Empty;
        private string perVDCCd = string.Empty;
        private string perWardNo = string.Empty;
        private string HouseHoldIdFrom = string.Empty;
        private string HouseHoldIdTo = string.Empty;
        private string HouseholdformnoFrom = string.Empty;
        private string HouseholdformnoTo = string.Empty;
        private string EnteredBy = string.Empty;
        private string SortBy = string.Empty;
        private string SortOrder = string.Empty;
        private string PageSize = string.Empty;
        private string PageIndex = string.Empty;
        private string ExportExcel = string.Empty;
        private string Lang = string.Empty;
        private string FilterWord = string.Empty;
        private Boolean isHouseHoldHead = false;
        private string SearchFromHouseHold = string.Empty;
        private String editMode = string.Empty;
        private string buildingCntfrm = string.Empty;
        private string buildingCntto = string.Empty;
        private string householdcntfrm = string.Empty;
        private string householdcntto = string.Empty;
        private string iNSTANCE_UNIQUE_SNO = string.Empty;
        private string sIM_NUMBER = string.Empty;
        private string membercntfrm = string.Empty;
        private string membercntto = string.Empty;
        public string BUILDING_STRUCTURE_NO
        {
            get { return BuildingStructureNo; }
            set { BuildingStructureNo = value; }
        }
        public string HOUSE_OWNER_ID 
        {
            get { return HouseOWnerID; }
            set { HouseOWnerID = value; }
        }
        public string IDENTIFICATION_TYPE_CD
        {
            get { return IdentificationtypeCd; }
            set { IdentificationtypeCd = value; }
        }
        public string BANK_CD
        {
            get { return BankCd; }
            set { BankCd = value; }
        }

        public string RESPONDENT_FNAME_ENG 
        {
            get { return RespondentFName; }
            set { RespondentFName = value; }
        }
        public string RESPONDENT_MNAME_ENG
        {
            get { return RespondentMName; }
            set { RespondentMName = value; }
        }
        public string RESPONDENT_LNAME_ENG 
        {
            get { return RespondentLName; }
            set { RespondentLName = value; }
        }
        public string RESPONDENT_FULLNAME_ENG  
        {
            get { return RespondentFullNameEng; }
            set { RespondentFullNameEng = value; }
        }
        public string RESPONDENT_FNAME_LOC 
        {
            get { return RespondentLNameLoc; }
            set { RespondentLNameLoc = value; }
        }
        public string RESPONDENT_MNAME_LOC  
        {
            get { return RespondentMNameLoc; }
            set { RespondentMNameLoc = value; }
        }
        public string RESPONDENT_LNAME_LOC 
        {
            get { return RespondentLNameLoc; }
            set { RespondentLNameLoc = value; }
        }
        public string RESPONDENT_FULLNAME_LOC
        {
            get { return RespondentFullNameLoc ; }
            set { RespondentFullNameLoc = value; }
        }
        public string SHELTER_SINCE_QUAKE_CD 
        {
            get { return ShelterSinceQuakeCd; }
            set { ShelterSinceQuakeCd = value; }
        }
        public string SHELTER_BEFORE_QUAKE_CD
        {
            get { return ShelterBeforeQuakeCd; }
            set { ShelterBeforeQuakeCd = value; }
        }
        public string CURRENT_SHELTER_CD 
        {
            get { return CurrentShelterCd; }
            set { CurrentShelterCd = value; }
        }
        public string EQ_VICTIM_IDENTITY_CARD_CD 
        {
            get { return EQvictimCardCd; }
            set { EQvictimCardCd = value; }
        }
        public string MONTHLY_INCOME_CD  
        {
            get { return MonthlyIncomeCd; }
            set { MonthlyIncomeCd = value; }
        }
        public string DEATH_CNT_FROM
        {
            get { return DeathCntFrom; }
            set { DeathCntFrom = value; }
        }

        public string DEATH_CNT_TO
        {
            get { return DeathCntTo; }
            set { DeathCntTo = value; }
        }

        public string HUMAN_DESTROY_CNT_FROM
        {
            get { return HumanDestroyCntFrom; }
            set { HumanDestroyCntFrom = value; }
        }

        public string HUMAN_DESTROY_CNT_TO
        {
            get { return HumanDestroyCntTo; }
            set { HumanDestroyCntTo = value; }
        }

        public string STUDENT_SCHOOL_LEFT_CNT
        {
            get { return StudentSchoolLeftCount; }
            set { StudentSchoolLeftCount = value; }
        }

        public string PREGNANT_REGULAR_CHECKUP_CNT
        {
            get { return PregnantRegularCheckupCnt; }
            set { PregnantRegularCheckupCnt = value; }
        }

        public string CHILD_LEFT_VACINATION_CNT
        {
            get { return ChildLeftVaccinationCount; }
            set { ChildLeftVaccinationCount = value; }
        }

        public string LEFT_CHANGE_OCCUPANY_CNT
        {
            get { return LeftChangeOccupancyCount; }
            set { LeftChangeOccupancyCount = value; }
        }

        public string EQ_RELIEF_MONEY_CD
        {
            get { return EQReliefmoneyCd; }
            set { EQReliefmoneyCd = value; }
        }

        public string DEFINED_CD
        {
            get { return DefinedCd; }
            set { DefinedCd = value; }
        }
        public string DAMAGEGRADE
        {
            get { return DamageGrade; }
            set { DamageGrade = value; }
        }
        public string DAMAGERESOLUTION
        {
            get { return DamageResolution; }
            set { DamageResolution = value; }
        }
        public string HOUSEHOLD_ID 
        {
            get { return HouseholdId; }
            set { HouseholdId = value; }
        }

        public string HOUSEHOLDIDFRM
        {
            get { return HouseholdformnoFrom; }
            set { HouseholdformnoFrom = value; }
        }

        public string HOUSEHOLDIDTO
        {
            get { return HouseholdformnoTo; }
            set { HouseholdformnoTo = value; }
        }

        public string HOUSEHOLD_FORM_NO_FROM
        {
            get { return HouseHoldIdFrom; }
            set { HouseHoldIdFrom = value; }
        }

        public string HOUSEHOLD_FORM_NO_TO
        {
            get { return HouseHoldIdTo; }
            set { HouseHoldIdTo = value; }
        }

        public string SESSION_ID  
        {
            get { return SessionID; }
            set { SessionID = value; }
        }

        public string MEMBER_ID  
        {
            get { return MemebrID; }
            set { MemebrID = value; }
        }
        public string BIRTH_DT_ST
        {
            get { return BirthDateFrom; }
            set { BirthDateFrom = value; }
        }
        public string BIRTH_DT_TO
        {
            get { return BirthDateTo; }
            set { BirthDateTo = value; }
        }
        
        public string MARITAL_STATUS_CD
        {
            get { return MaritalStatusCd; }
            set { MaritalStatusCd = value; }
        }
        
        public string AGE
        {
            get { return Age; }
            set { Age = value; }
        }
        
        public string CASTE_CD
        {
            get { return CasteCd; }
            set { CasteCd = value; }
        }
        
        public string RELIGION_CD
        {
            get { return ReligionCd; }
            set { ReligionCd = value; }
        }

        public string EDUCATION_CD
        {
            get { return EducationCd; }
            set { EducationCd = value; }
        }

        public string CTZ_NO
        {
            get { return CtizenshipNo; }
            set { CtizenshipNo = value; }
        }

        public string CTZ_ISSUE_DISTRICT_CD
        {
            get { return CitizenshipIssueDistrict; }
            set { CitizenshipIssueDistrict = value; }
        }

        public string CTZ_ISSUE_DT
        {
            get { return CitizenshipIssueDateEng; }
            set { CitizenshipIssueDateEng = value; }
        }

        public string CTZ_ISSUE_DT_LOC
        {
            get { return CitizenshipIssueDateLoc; }
            set { CitizenshipIssueDateLoc = value; }
        }

        public string CUR_DISTRICT_CD
        {
            get { return CurDistrictCd; }
            set { CurDistrictCd = value; }
        }

        public string CUR_VDC_MUN_CD
        {
            get { return CurVDCCd; }
            set { CurVDCCd = value; }
        }

        public string CUR_WARD_NO
        {
            get { return CurWardNo; }
            set { CurWardNo = value; }
        }

        public string PER_DISTRICT_CD
        {
            get { return PerDistrictCd; }
            set { PerDistrictCd = value; }
        }

        public string PER_VDC_MUN_CD
        {
            get { return perVDCCd; }
            set { perVDCCd = value; }
        }

        public string PER_WARD_NO
        {
            get { return perWardNo; }
            set { perWardNo = value; }
        }

        public string ENTERED_BY 
        {
            get { return EnteredBy; }
            set { EnteredBy = value; }
        }



         
     public string FIRST_NAME_ENG
        {
            get { return FirstNameEng; }
            set { FirstNameEng = value; }
        }
        public string MIDDLE_NAME_ENG
        {
            get { return MiddleNameEng; }
            set { MiddleNameEng = value; }
        }
        public string LAST_NAME_ENG
        {
            get { return LastNameEng; }
            set { LastNameEng = value; }
        }
        public string FIRST_NAME_LOC
        {
            get { return FirstNameLoc; }
            set { FirstNameLoc = value; }
        }
        public string MIDDLE_NAME_LOC
        {
            get { return MiddleNameLOC; }
            set { MiddleNameLOC = value; }
        }
        public string LAST_NAME_LOC
        {
            get { return LastNameLoc; }
            set { LastNameLoc = value; }
        }
        public string INTERVIEWED_BY
        {
            get { return InterviewedBy; }
            set { InterviewedBy = value; }
        }
        public string PER_AREA_ENG
        {
            get { return perAreaEng; }
            set { perAreaEng = value; }
        }
        public string PER_AREA_LOC
        {
            get { return perAreaLoc; }
            set { perAreaLoc = value; }
        }
        public string HOUSE_NO
        {
            get { return HouseNo; }
            set { HouseNo = value; }
        }
        public string CUR_AREA_ENG
        {
            get { return curAreaEng; }
            set { curAreaEng = value; }
        }

        public string CUR_AREA_LOC
        {
            get { return curAreaLoc; }
            set { curAreaLoc = value; }
        }
     
        public string EX_WALLS_MATERIAL_CD_I
        {
            get { return ExWallsMateriaCdI; }
            set { ExWallsMateriaCdI = value; }
        }
        public string ROOF_MATERIAL_CD
        {
            get { return RoofMaterialCd; }
            set { RoofMaterialCd = value; }
        }

        public string WATER_SOURCE_CD_II
        {
            get { return WaterSourceCDII; }
            set { WaterSourceCDII = value; }
        }
        public string WATER_DISTANCE_HR
        {
            get { return WaterDistanceHr; }
            set { WaterDistanceHr = value; }
        }
        public string WATER_DISTANCE_MIN
        {
            get { return WaterDistanceMin; }
            set { WaterDistanceMin = value; }
        }
        public string TOILET_TYPE_CD_I
        {
            get { return ToiletTypeCdI; }
            set { ToiletTypeCdI = value; }
        }
        public string TOILET_SHARED
        {
            get { return ToiletShared; }
            set { ToiletShared = value; }
        }
        public string LIGHT_SOURCE_CD_II
        {
            get { return LightSourceCdII; }
            set { LightSourceCdII = value; }
        }
        public string FUEL_SOURCE_CD_II
        {
            get { return FuelSourceCDII; }
            set { FuelSourceCDII = value; }
        }
      
        public string Imei
        {
            get { return IMEI; }
            set { IMEI = value; }
        }
        public string MOBILE_NUMBER
        {
            get { return mobile; }
            set { mobile = value; }
        }
        public string FULL_NAME_LOC
        {
            get { return strFullNameLoc; }
            set { strFullNameLoc = value; }
        }
        public string INTERVIEW_DT_FROM
        {
            get { return InterviewDTFrom; }
            set { InterviewDTFrom = value; }
        }
        public string INTERVIEW_DT_TO
        {
            get { return InterviewDTTo; }
            set { InterviewDTTo = value; }
        }
        public string RC_MATERIAL_CD
        {
            get { return RCMaterial; }
            set { RCMaterial = value; }
        }
        public string FC_MATERIAL_CD
        {
            get { return FCMaterial; }
            set { FCMaterial = value; }
        }
        public string SC_MATERIAL_CD
        {
            get { return SCMaterial; }
            set { SCMaterial = value; }
        }

        public string StrRegion
        {
            get { return strRegion; }
            set { strRegion = value; }

        }
        public string StrZone
        {
            get { return strZone; }
            set { strZone = value; }

        }
        public string StrDistrict
        {
            get { return strDistrict; }
            set { strDistrict = value; }

        }
        public string StrVDC
        {
            get { return strVDC; }
            set { strVDC = value; }

        }
        public string StrWard
        {
            get { return strWard; }
            set { strWard = value; }

        }
        //public string StrHouseNo
        //{
        //    get { return strHouseNo; }
        //    set { strHouseNo = value; }

        //}
        public string StrFullName
        {
            get { return strFullName; }
            set { strFullName = value; }

        }

        public string StrHouseHoldSN
        {
            get { return strHouseHoldSN; }
            set { strHouseHoldSN = value; }

        }
        public string StrGender
        {
            get { return strGender; }
            set { strGender = value; }

        }
        public string AgeFrom
        {
            get { return ageFrom; }
            set { ageFrom = value; }

        }
        public string AgeTo
        {
            get { return ageTo; }
            set { ageTo = value; }

        }
        public string DAMAGE_GRADE_CD
        {
            get { return damagegrade; }
            set { damagegrade = value; }

        }
        public string TECHSOLUTION_CD
        {
            get { return techsolution; }
            set { techsolution = value; }

        }

        public string ENUMERATOR_ID
        {
            get { return EnumeratorCd; }
            set { EnumeratorCd = value; }

        }
        public string HOUSE_OWNER_CNT
        {
            get { return HOwnerCount; }
            set { HOwnerCount = value; }
        }

      

        public string BuildingCntfrm
        {
            get { return buildingCntfrm; }
            set { buildingCntfrm = value; }
        }

        public string BuildingCntto
        {
            get { return buildingCntto; }
            set { buildingCntto = value; }
        }

        public string Householdcntfrm
        {
            get { return householdcntfrm; }
            set { householdcntfrm = value; }
        }

        public string Householdcntto
        {
            get { return householdcntto; }
            set { householdcntto = value; }
        }

        public string INSTANCE_UNIQUE_SNO
        {
            get { return iNSTANCE_UNIQUE_SNO; }
            set { iNSTANCE_UNIQUE_SNO = value; }
        }

        public string SIM_NUMBER
        {
            get { return sIM_NUMBER; }
            set { sIM_NUMBER = value; }
        }
        public string Membercntfrm
        {
            get { return membercntfrm; }
            set { membercntfrm = value; }
        }
        public string Membercntto
        {
            get { return membercntto; }
            set { membercntto = value; }
        }
        public string NOT_INTERVIWING_REASON_CD
        {
            get { return NirCd; }
            set { NirCd = value; }
        }

        public string HOUSE_LEGALOWNER
        {
            get { return HllOwner; }
            set { HllOwner = value; }
        }

        public string BUILDING_CONDITION_CD
        {
            get { return BuildingConditionCd; }
            set { BuildingConditionCd = value; }
        }

        public string GROUND_SURFACE_CD
        {
            get { return GroundSurfaceCd; }
            set { GroundSurfaceCd = value; }
        }

        public string FOUNDATION_TYPE_CD
        {
            get { return FoundationCd; }
            set { FoundationCd = value; }
        }
        public string BUILDING_POSITION_CD
        {
            get { return BuildinPositionCd; }
            set { BuildinPositionCd = value; }
        }

        public string BUILDING_PLAN_CD
        {
            get { return BuildingPlanCd; }
            set { BuildingPlanCd = value; }
        }
        public string ASSESSED_AREA_CD
        {
            get { return AssessedAreaCd; }
            set { AssessedAreaCd = value; }
        }
        public string StrSearchMode
        {
            get { return strSearchMode; }
            set { strSearchMode = value; }
        }
        public string SORT_BY
        {
            get { return SortBy; }
            set { SortBy = value; }
        }
        public string SORT_ORDER
        {
            get { return SortOrder; }
            set { SortOrder = value; }
        }
        public string PAGE_SIZE
        {
            get { return PageSize; }
            set { PageSize = value; }
        }
        public string PAGE_INDEX
        {
            get { return PageIndex; }
            set { PageIndex = value; }
        }
        public string EXPORT_EXCEL
        {
            get { return ExportExcel; }
            set { ExportExcel = value; }
        }
        public string LANG
        {
            get { return Lang; }
            set { Lang = value; }
        }
        public string FILTER_WORD
        {
            get { return FilterWord; }
            set { FilterWord = value; }
        }
        public Boolean IS_HOUSEHOLD_HEAD
        {
            get { return isHouseHoldHead; }
            set { isHouseHoldHead = value; }
        }
        public String SEARCH_FROM_HOUSEHOLD
        {
            get { return SearchFromHouseHold; }
            set { SearchFromHouseHold = value; }
        }
       
    }
}
