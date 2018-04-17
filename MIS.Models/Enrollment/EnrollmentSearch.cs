using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Enrollment
{
    public class EnrollmentSearch
    {
        public String docData { get; set; }

        public string memberID { get; set; }
        public string salutation { get; set; }
        public string targetBatchId { get; set; }
        public string fullname { get; set; }
        public string fisrtname { get; set; }
        public string middlename { get; set; }
        public string lastename { get; set; }
        public string relationWithHead { get; set; }
        public string relationedPersonName { get; set; }
        public string nameOfBeneficiary { get; set; }
        public string address { get; set; }
        public string idNumber { get; set; }
        public string surveyId { get; set; }
        public string photoPath { get; set; }
        private string IdentifiicationTypeCd = string.Empty;
        private string approved = string.Empty;
        private string EnrollmentStatus = string.Empty;
        private string CutOff = string.Empty;
        private string PrintStatus = string.Empty;
        private string MSignedStatus = string.Empty;
        private string smssendstatus = null;
        private string BankAccType = string.Empty;
        private string BankBranchCD = string.Empty;
        private string BankCD = string.Empty;
        private string PaymentrecmemId = string.Empty;
        private string WardNO = string.Empty;
        private Boolean IsPayRecChanged = false;
        private string VDCMuN = string.Empty;
        private String DistrictCD = string.Empty;
        private string InstallationCD = string.Empty;
        private string EnrollmentMDtTo = string.Empty;
        private string EnrollmentMDtFrom = string.Empty;
        private string EnrollmentDtTo = string.Empty;
        private string EnrollmentDtFrom = string.Empty;
        private string BuildingStructureNo = string.Empty;
        private string HouseOwnerID = string.Empty;
        private string HouseOwnerName = string.Empty;
        private string HouseOwnerNameloc = string.Empty;
        private string TargettingID = string.Empty;
        private string EntrollmentID = string.Empty;
        private string SortBy = string.Empty;
        private string SortOrder = string.Empty;
        private string PageSize = string.Empty;
        private string PageIndex = string.Empty;
        private string ExportExcel = string.Empty;
        private string Lang = string.Empty;
        private string FilterWord = string.Empty;
        private string DistrictEng = string.Empty;
        private string DistrictLoc = string.Empty;
        private string VDCEng = string.Empty;
        private string VDCLoc = string.Empty;
        private string AreaEng = string.Empty;
        private string AreaLoc = string.Empty;
        private string RespondendtIsHouseOwner = string.Empty;
        private string EnumeratorID = string.Empty;
        private string houseID = string.Empty;
        private string householdID = string.Empty;
        private string beneficiaryID = string.Empty;
        private string NisaaNo = string.Empty;
        private string EnumerationArea = string.Empty;


        public String NISSA_NO
        {
            get { return NisaaNo; }
            set { NisaaNo = value; }
        }
       
        public String ENUMERATION_AREA
        {
            get { return EnumerationArea; }
            set { EnumerationArea = value; }
        }
        public String ENUMERATOR_ID
        {
            get { return EnumeratorID; }
            set { EnumeratorID = value; }
        }
        public String HOUSE_ID
        {
            get { return houseID; }
            set { houseID = value; }
        }
        public String HOUSEHOLD_ID
        {
            get { return householdID; }
            set { householdID = value; }
        }
        public String BENEFICIARY_ID
        {
            get { return beneficiaryID; }
            set { beneficiaryID = value; }
        }
        public String RESPONDENT_IS_HOUSE_OWNER
        {
            get { return RespondendtIsHouseOwner; }
            set { RespondendtIsHouseOwner = value; }
        }
        public String AREA_LOC
        {
            get { return AreaLoc; }
            set { AreaLoc = value; }
        }
        public String AREA_ENG
        {
            get { return AreaEng; }
            set { AreaEng = value; }
        }
        public String VDC_ENG
        {
            get { return VDCEng; }
            set { VDCEng = value; }
        }
        public String VDC_LOC
        {
            get { return VDCLoc; }
            set { VDCLoc = value; }
        }
        public String DISTRICT_LOC
        {
            get { return DistrictLoc; }
            set { DistrictLoc = value; }
        }
        public String DISTRICT_ENG
        {
            get { return DistrictEng; }
            set { DistrictEng = value; }
        }
        public String Entrollment_ID
        {
            get { return EntrollmentID; }
            set { EntrollmentID = value; }
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
            get { return HouseOwnerNameloc; }
            set { HouseOwnerNameloc = value; }
        }
        public String Building_Structure_No
        {
            get { return BuildingStructureNo; }
            set { BuildingStructureNo = value; }
        }
        public String Enrollment_Dt_From
        {
            get { return EnrollmentDtFrom; }
            set { EnrollmentDtFrom = value; }
        }
        public String Enrollment_Dt_To
        {
            get { return EnrollmentDtTo; }
            set { EnrollmentDtTo = value; }
        }
        public String Enrollment_M_Dt_From
        {
            get { return EnrollmentMDtFrom; }
            set { EnrollmentMDtFrom = value; }
        }
        public String Enrollment_M_Dt_To
        {
            get { return EnrollmentMDtTo; }
            set { EnrollmentMDtTo = value; }
        }
        
            public String InstallationCd
        {
            get { return InstallationCD; }
            set { InstallationCD = value; }
        }
        public String DistrictCd
        {
            get { return DistrictCD; }
            set { DistrictCD = value; }
        }

        public String VDCMun
        {
            get { return VDCMuN; }
            set { VDCMuN = value; }
        }
        public String WardNo
        {
            get { return WardNO; }
            set { WardNO = value; }
        }
        public String Payment_rec_mem_Id
        {
            get { return PaymentrecmemId; }
            set { PaymentrecmemId = value; }
        }
        public String Bank_CD
        {
            get { return BankCD; }
            set { BankCD = value; }
        }
        public String Bank_Branch_CD
        {
            get { return BankBranchCD; }
            set { BankBranchCD = value; }
        }
        public String Bank_Acc_Type
        {
            get { return BankAccType; }
            set { BankAccType = value; }
        }
        public String M_Signed_Status
        {
            get { return MSignedStatus; }
            set { MSignedStatus = value; }
        }
        public String SMS_SEND_STATUS
        {
            get { return smssendstatus; }
            set { smssendstatus = value; }
        }
        public String Print_Status
        {
            get { return PrintStatus; }
            set { PrintStatus = value; }
        }
        public Boolean Is_Pay_Rec_Changed
        {
            get { return IsPayRecChanged; }
            set { IsPayRecChanged = value; }
        }
        public String Cut_Off
        {
            get { return CutOff; }
            set { CutOff = value; }
        }
        public String Enrollment_Status
        {
            get { return EnrollmentStatus; }
            set { EnrollmentStatus = value; }
        }
        public String Approved
        {
            get { return approved; }
            set { approved = value; }
        }
        public String Identifiication_Type_Cd
        {
            get { return IdentifiicationTypeCd; }
            set { IdentifiicationTypeCd = value; }
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
        public string VDC_SECRETARY_ENG { get; set; }
        public string VDC_SECRETARY_VDC_ENG { get; set; }
        public string VDC_SECRETARY_DISTRICT_ENG { get; set; }
        public string VDC_SECRETARY_WARD_NO { get; set; }
        public string VDC_SECRETARY_POSITION_ENG { get; set; }
        public Boolean? Action { get; set; }

    }
}
