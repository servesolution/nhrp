using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Resurvey
{
    public class ResurveyTargetingModel
    {
        public bool IsTargeting { get; set; }
        public String Targeted { get; set; }
        public String DistrictCd { get; set; }
        public string CurrentVdcMunDefCD { get; set; }
        public string CurrentVdcMunCD { get; set; }
        public String Assist_type_cd { get; set; }
        public String VDCMun { get; set; }
        public String WardNo { get; set; }
        public String ddl_FiscalYear { get; set; }
        public String ddl_SP { get; set; }
        public String ddl_BusinessRule { get; set; }
        public String Quota { get; set; }
        public String AreaEng { get; set; }
        public String AreaLoc { get; set; }
        public String BatchId { get; set; }
        public String EducationCd { get; set; }
        public String TargetCd { get; set; }

        public String EnteredBy { get; set; }
        public DateTime EnteredDate { get; set; }
        public DateTime EnteredDateLoc { get; set; }

        private string BuildingStructureNo = string.Empty;
        private string HouseOwnerID = string.Empty;
        private string HouseOwnerName = string.Empty;
        private string HouseOwnerNameLoc = string.Empty;

        private string TargettingID = string.Empty;
        private string TARGETINGDTFROM = string.Empty;
        private string TARGETINGDTTO = string.Empty;
        private string SURVEYINGDTFROM = string.Empty;
        private string SURVEYINGDTTO = string.Empty;
        private string DAMAGEGRADECD = string.Empty;
        private string BUILDINGCONDITIONCD = string.Empty;
        private string INSTANCEUNIQUESNO = string.Empty;
        private string HOUSEID = string.Empty;
        private string HOUSEDEFINEDCD = string.Empty;
        private string HHSERIALNO = string.Empty;
        private string TECHSOLUTIONCD = string.Empty;
        private string DISTRICTCD = string.Empty;
        private string VDCMUNCD = string.Empty;
        private string WARDNO = string.Empty;
        private string FISCALYR = string.Empty;
        private string BUSINESSRULE = string.Empty;
        private string SortBy = string.Empty;
        private string SortOrder = string.Empty;
        private string PageSize = string.Empty;
        private string PageIndex = string.Empty;
        private string ExportExcel = string.Empty;
        private string Lang = string.Empty;
        private string FilterWord = string.Empty;
        private string SessionID = string.Empty;
        public String SESSION_ID
        {
            get { return SessionID; }
            set { SessionID = value; }
        }
        public String Targetting_ID
        {
            get { return TargettingID; }
            set { TargettingID = value; }
        }
        public String House_Owner_ID
        {
            get { return HouseOwnerID; }
            set { HouseOwnerID = value; }
        }
        public String House_Owner_Name
        {
            get { return HouseOwnerName; }
            set { HouseOwnerName = value; }
        }

        public String House_Owner_Name_Loc
        {
            get { return HouseOwnerNameLoc; }
            set { HouseOwnerNameLoc = value; }
        }
        public String Building_Structure_No
        {
            get { return BuildingStructureNo; }
            set { BuildingStructureNo = value; }
        }


        public String SURVEYING_DT_FROM
        {
            get { return SURVEYINGDTFROM; }
            set { SURVEYINGDTFROM = value; }
        }
        public String SURVEYING_DT_TO
        {
            get { return SURVEYINGDTTO; }
            set { SURVEYINGDTTO = value; }
        }
        public String TARGETING_DT_FROM
        {
            get { return TARGETINGDTFROM; }
            set { TARGETINGDTFROM = value; }
        }
        public String TARGETING_DT_TO
        {
            get { return TARGETINGDTTO; }
            set { TARGETINGDTTO = value; }
        }
        public String DAMAGE_GRADE_CD
        {
            get { return DAMAGEGRADECD; }
            set { DAMAGEGRADECD = value; }
        }
        public String BUILDING_CONDITION_CD
        {
            get { return BUILDINGCONDITIONCD; }
            set { BUILDINGCONDITIONCD = value; }
        }


        public String INSTANCE_UNIQUE_SNO
        {
            get { return INSTANCEUNIQUESNO; }
            set { INSTANCEUNIQUESNO = value; }
        }
        public String HOUSE_ID
        {
            get { return HOUSEID; }
            set { HOUSEID = value; }
        }
        public String HOUSE_DEFINED_CD
        {
            get { return HOUSEDEFINEDCD; }
            set { HOUSEDEFINEDCD = value; }
        }
        public String HH_SERIAL_NO
        {
            get { return HHSERIALNO; }
            set { HHSERIALNO = value; }
        }


        public String TECHSOLUTION_CD
        {
            get { return TECHSOLUTIONCD; }
            set { TECHSOLUTIONCD = value; }
        }
        public String DISTRICT_CD
        {
            get { return DISTRICTCD; }
            set { DISTRICTCD = value; }
        }
        public String VDC_MUN_CD
        {
            get { return VDCMUNCD; }
            set { VDCMUNCD = value; }
        }
        public String WARD_NO
        {
            get { return WARDNO; }
            set { WARDNO = value; }
        }


        public String FISCAL_YR
        {
            get { return FISCALYR; }
            set { FISCALYR = value; }
        }
        public String BUSINESS_RULE
        {
            get { return BUSINESSRULE; }
            set { BUSINESSRULE = value; }
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
        public Boolean? Action { get; set; }

        //public List<HouseOwnerNameModel> HouseOwnerDetailsList { get; set; }
        //public List<VW_HOUSE_BUILDING_DTL> BulStrDetlList { get; set; }
        //public List<OtherHousesDamagedModel> OtherHouseList { get; set; }
    }
}
