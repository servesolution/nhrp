using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Search
{
    public class MemberSearch
    {
        private string SessionID = string.Empty;
        private string MemebrID = string.Empty;
          private string DefinedCd = string.Empty;
          private string strFullName = string.Empty;
          private string strFullNameLoc = string.Empty;
          private string strGender = string.Empty;
          private string BirthDateEngSt = string.Empty;
          private string BirthDateEngTo = string.Empty;
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
          private string EnteredBy = string.Empty;
          private string IdentificationtypeCd = string.Empty;
          private string BankCd = string.Empty;
          private string SearchFromHouseHold = string.Empty;
          private string ageFrom = string.Empty;
          private string ageTo = string.Empty;

          private string Houseid = string.Empty;

          private string HouseHoldId = string.Empty;
          private string HouseHoldIdFrom = string.Empty;
          private string HouseHoldIdTo = string.Empty;
          private Boolean isHouseHoldHead = false;
          private string SortBy = string.Empty;
          private string SortOrder = string.Empty;
          private string PageSize = string.Empty;
          private string PageIndex = string.Empty;
          private string ExportExcel = string.Empty;
          private string Lang = string.Empty;
          private string FilterWord = string.Empty;
        
          
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

        
        public string DEFINED_CD
        {
            get { return DefinedCd; }
            set { DefinedCd = value; }
        }

        public string HOUSE_ID
        {
            get { return Houseid; }
            set { Houseid = value; }
        }

        public string HOUSEHOLD_ID
        {
            get { return HouseHoldId; }
            set { HouseHoldId = value; }
        }
        public string HOUSEHOLD_ID_NO_FROM
        {
            get { return HouseHoldIdFrom; }
            set { HouseHoldIdFrom = value; }
        }

        public string HOUSEHOLD_ID_NO_TO
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

        public string BIRTH_DT_ENG_ST
        {
            get { return BirthDateEngSt; }
            set { BirthDateEngSt = value; }
        }
        public string BIRTH_DT_ENG_TO
        {
            get { return BirthDateEngTo; }
            set { BirthDateEngTo = value; }
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
        public String INSTANCE_UNIQUE_SNO { get; set; }
        public Boolean Action { get; set; }
    }
}
