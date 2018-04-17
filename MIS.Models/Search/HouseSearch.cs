using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Search
{
    public class HouseSearch
    {
        private string strRegion = string.Empty;
        private string strZone = string.Empty;
        private string strDistrict = string.Empty;
        private string strVDC = string.Empty;
        private string strWard = string.Empty;
        private string iNSTANCE_UNIQUE_SNO = string.Empty;
        private string sIM_NUMBER = string.Empty; 
        private string IMEI = string.Empty;
        private string mobile = string.Empty;
        private string strHouseHoldSN = string.Empty;
        private string strFullName = string.Empty;

        private string strFullNameEng = string.Empty;
        private string strFullNameLoc = string.Empty;
        private string EnumeratorCd = string.Empty; 
        private string InterviewDTFrom = string.Empty;
        private string InterviewDTTo = string.Empty;
        private string InterviewDTFromloc = string.Empty;
        private string InterviewDTToloc = string.Empty;
        private string HOwnerCount = string.Empty;
        private string NirCd = string.Empty;
        private string buildingCntfrm = string.Empty;
        private string buildingCntto = string.Empty;
        private string householdcntfrm = string.Empty;
        private string householdcntto = string.Empty;
        private string membercntfrm = string.Empty;
        private string membercntto = string.Empty;           
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
        private String StrCNt = string.Empty;
        
        private string structurecntfrom = string.Empty;
        private string structurecntto = string.Empty;
        private string nraDefinedCd = string.Empty;


        public string NRA_DEFINED_CD
        {
            get { return nraDefinedCd; }
            set { nraDefinedCd = value; }
        }

        public string STRUCTURE_COUNT_FROM
        {
            get { return structurecntfrom; }
            set { structurecntfrom = value; }
        }

        public string STRUCTURE_COUNT_TO
        {
            get { return structurecntto; }
            set { structurecntto = value; }
        }
   
                    
        public string STRUCTURE_COUNT
        {
            get { return StrCNt; }
            set { StrCNt = value; }
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

        public string FULL_NAME_ENG
        {
            get { return strFullNameEng; }
            set { strFullNameEng = value; }
        }
        public string FULL_NAME_LOC
        {
            get { return strFullNameLoc; }
            set { strFullNameLoc = value; }
        }

        public string INTERVIEW_DT_FROM_LOC
        {
            get { return InterviewDTFromloc; }
            set { InterviewDTFromloc = value; }
        }
        public string INTERVIEW_DT_TO_LOC
        {
            get { return InterviewDTToloc; }
            set { InterviewDTToloc = value; }
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
        public string HOUSE_OWNER_ID { get; set; }
        public string TOTAL_OTHER_HOUSE_CNT_FROM { get; set; }
        public string TOTAL_OTHER_HOUSE_CNT_TO { get; set; }
        public Boolean Action { get; set; }
    }
}
