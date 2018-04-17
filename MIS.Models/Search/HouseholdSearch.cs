using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Search
{
    public class HouseholdSearch
    {
        private string familyDistrict = string.Empty;
        private string familyVDC = string.Empty;
        private string familyWard = string.Empty;
        private string SessionID = string.Empty;
        private string MemebrID = string.Empty;
        private string HouseholdId = string.Empty;
        private string HouseOWnerID = string.Empty;
        private string BuildingStructureNo = string.Empty;
        private string DefinedCd = string.Empty;

        private string FirstNameEng = string.Empty;
        private string FirstNameLoc = string.Empty;
        private string MiddleNameEng = string.Empty;
        private string MiddleNameLOC = string.Empty;
        private string LastNameEng = string.Empty;
        private string LastNameLoc = string.Empty;

        private string strFullName = string.Empty;
        private string strFullNameLoc = string.Empty;

        private string RespondentFName = string.Empty;
        private string RespondentMName = string.Empty;
        private string RespondentLName = string.Empty;
        private string RespondentFullNameEng = string.Empty;
        private string RespondentFNameLoc = string.Empty;
        private string RespondentMNameLoc = string.Empty;
        private string RespondentLNameLoc = string.Empty;
        private string RespondentFullNameLoc = string.Empty;

        private string CurDistrictCd = string.Empty;
        private string CurVDCCd = string.Empty;
        private string CurWardNo = string.Empty;
        private string PerDistrictCd = string.Empty;
        private string perVDCCd = string.Empty;
        private string perWardNo = string.Empty;
        private string perAreaEng = string.Empty;
        private string perAreaLoc = string.Empty;
        private string curAreaEng = string.Empty;
        private string curAreaLoc = string.Empty;
        private string houseNo = string.Empty;

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

        private string LightSourceCdII = string.Empty;
        private string FuelSourceCDII = string.Empty;
        private string EQReliefmoneyCd = string.Empty;
        private string ToiletTypeCdI = string.Empty;
        private string WaterSourceCDII = string.Empty;
        private string MobileNo = string.Empty;
        private string BankAccount = string.Empty;

        private string EnteredBy = string.Empty;
        private string SortBy = string.Empty;
        private string SortOrder = string.Empty;
        private string PageSize = string.Empty;
        private string PageIndex = string.Empty;
        private string ExportExcel = string.Empty;
        private string Lang = string.Empty;
        private string FilterWord = string.Empty;
        private string headgendercd = string.Empty;
        private string ho_defined_cd = string.Empty;
        private string instance_unique_sno = string.Empty;


        public string Bank_Account
        {
            get { return BankAccount; }
            set { BankAccount = value; }
        }
        public string Mobile_Number
        {
            get { return MobileNo; }
            set { MobileNo = value; }
        }
        public string FamilyDistrict
        {
            get { return familyDistrict; }
            set { familyDistrict = value; }
        }

        public string FamilyVDC
        {
            get { return familyVDC; }
            set { familyVDC = value; }
        }

        public string FamilyWard
        {
            get { return familyWard; }
            set { familyWard = value; }
        }
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
            get { return RespondentFullNameLoc; }
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

        public string HouseNo
        {
            get { return houseNo; }
            set { houseNo = value; }
        }

        

        public string HOUSEHOLD_ID
        {
            get { return HouseholdId; }
            set { HouseholdId = value; }
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
        public string HO_DEFINED_CD
        {
            get { return ho_defined_cd; }
            set { ho_defined_cd = value; }
        }

        public string INSTANCE_UNIQUE_SNO
        {
            get { return instance_unique_sno; }
            set { instance_unique_sno = value; }
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


        public string WATER_SOURCE_CD_II
        {
            get { return WaterSourceCDII; }
            set { WaterSourceCDII = value; }
        }

        public string TOILET_TYPE_CD_I
        {
            get { return ToiletTypeCdI; }
            set { ToiletTypeCdI = value; }
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

        public string FULL_NAME_LOC
        {
            get { return strFullNameLoc; }
            set { strFullNameLoc = value; }
        }

        public string StrFullName
        {
            get { return strFullName; }
            set { strFullName = value; }

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
        public string headgender_cd
        {
            get { return headgendercd; }
            set { headgendercd = value; }
        }
        //public string house_id
        //{
        //    get { return houseid; }
        //    set { houseid = value; }
        //}
        public Boolean Action { get; set; }
    }
}
