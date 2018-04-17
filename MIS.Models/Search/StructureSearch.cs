using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Search
{
    public class StructureSearch
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

        private string strFullName = string.Empty;
        private string strFullNameLoc = string.Empty;        
        private string strHouseHoldSN = string.Empty;
        private string DamageGrade = string.Empty;
        private string techsolution = string.Empty;
        private string EnumeratorCd = string.Empty;
        private DateTime InterviewDTFrom = DateTime.MaxValue;
        private DateTime InterviewDTTo = DateTime.MaxValue;
        private string HOwnerCount = string.Empty;
        private string NirCd = string.Empty;
        private string HllOwner = string.Empty;

        private string houseAge = string.Empty;
        private string BuildingConditionCd = string.Empty;
        private string GroundSurfaceCd = string.Empty;
        private string FoundationCd = string.Empty;
        private string RCMaterial = string.Empty;
        private string FCMaterial = string.Empty;
        private string SCMaterial = string.Empty;
        private string BuildinPositionCd = string.Empty;
        private string BuildingPlanCd = string.Empty;
        private string AssessedAreaCd = string.Empty;
        private string reconstruction_started = string.Empty;
        
        private string strGender = string.Empty; 
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
        private DateTime buildingCntfrm = DateTime.MaxValue;
        private DateTime buildingCntto = DateTime.MaxValue;
        private DateTime householdcntfrm = DateTime.MaxValue;
        private DateTime householdcntto = DateTime.MaxValue;

        private string houseId = string.Empty;
        private string houseagefrom = string.Empty;
        private string houseageto = string.Empty;
        private string houseLegelOwnerCd = string.Empty;
        private string secondaryuse = string.Empty;

        private DateTime membercntfrm = DateTime.MaxValue;
        private DateTime membercntto = DateTime.MaxValue;
        private string geographicalRisk = string.Empty;
        private string geotechnicalRisk = string.Empty;
        public string geotechnicalRiskcd { get; set; }
            

        public string GEOGRAPHICAL_RISK
        {
            get { return geographicalRisk; }
            set { geographicalRisk = value; }
        }

         public string GEOTECHNICAL_RISK
        {
            get { return geotechnicalRisk; }
            set { geotechnicalRisk = value; }
        }
         public string GEOTECHNICAL_RISK_CD
         {
             get { return geotechnicalRiskcd; }
             set { geotechnicalRiskcd = value; }
         }
        public string DAMAGEGRADE
        {
            get { return DamageGrade; }
            set { DamageGrade = value; }
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
        public string FULL_NAME
        {
            get { return strFullName; }
            set { strFullName = value; }
        }
        public DateTime INTERVIEW_DT_FROM
        {
            get { return InterviewDTFrom; }
            set { InterviewDTFrom = value; }
        }
        public DateTime NTERVIEW_DT_TO
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

        public DateTime BuildingCntfrm
        {
            get { return buildingCntfrm; }
            set { buildingCntfrm = value; }
        }

        public DateTime BuildingCntto
        {
            get { return buildingCntto; }
            set { buildingCntto = value; }
        }

        public DateTime Householdcntfrm
        {
            get { return householdcntfrm; }
            set { householdcntfrm = value; }
        }

        public DateTime Householdcntto
        {
            get { return householdcntto; }
            set { householdcntto = value; }
        }

        public DateTime Membercntfrm
        {
            get { return membercntfrm; }
            set { membercntfrm = value; }
        }
        public DateTime Membercntto
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
        
         public string HOUSE_AGE
        {
            get { return houseAge; }
            set { houseAge = value; }
        }
        public string HOUSE_ID
        {
            get { return houseId; }
            set { houseId = value; }
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
        public string HOUSE_AGE_FROM
        {
            get { return houseagefrom; }
            set { houseagefrom = value; }
        }
        public string HOUSE_AGE_TO
        {
            get { return houseageto; }
            set { houseageto = value; }
        }
        public string HOUSE_LAND_LEGAL_OWNER_CD
        {
            get { return houseLegelOwnerCd; }
            set { houseLegelOwnerCd = value; }
        }
        public string SECONDARY_USE
        {
            get { return secondaryuse; }
            set { secondaryuse = value; }
        }
        public string Reconstruction
        {
            get { return reconstruction_started; }
            set { reconstruction_started = value; }
        }

        public Boolean IS_HOUSEHOLD_HEAD
        {
            get { return isHouseHoldHead; }
            set { isHouseHoldHead = value; }
        }

        public string INSTANCE_UNIQUE_SNO
        {
            get { return iNSTANCE_UNIQUE_SNO; }
            set { iNSTANCE_UNIQUE_SNO = value; }
        }
        public String SEARCH_FROM_HOUSEHOLD
        {
            get { return SearchFromHouseHold; }
            set { SearchFromHouseHold = value; }
        }
        public Boolean Action { get; set; }
    }


}
