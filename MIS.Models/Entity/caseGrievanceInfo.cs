using System;
using EntityFramework;

[Table(Name="CM_GRIEVANCE")]

   public class CaseGrievanceInfo: EntityBase
    {
    public String grievanceCd = null;
    public String householdId = null;
    public String regionCd = null;
    public String zoneCd = null;
    public String districtCd = null;
    public String vDCMunCd = null;
    public String wardCd = null;
    public String hhFullName = null;
    public String caseStatus = null;
    public String pinNo = null;
    public String address = null;
    public String phoneNo = null;
    public String grievanceCategory = null;
    public String grievanceCategoryGroup = null;
    public String caseDesc = null;
    public String signApplicant = null;
    public String signAppDate = null;
    public String signDswOfficer = null;
    public String signDswDate = null;
    public String signSeniorOfficer = null;
    public String signSeniorDate = null;
    public String processRemark = null;
    public String finalRemark = null;

        public String approved = null;
        public String approvedBy = null;
        public String approvedDt = null;
        public String enteredBy = null;
        public String enteredDt = null;
        public String updatedBy = null;
        public String updatedDt = null;
        public String ipaddress = null;

        [Column(Name = "GRIEVANCE_CD")]
        public String GrievanceCd
        {
            get { return grievanceCd; }
            set { grievanceCd = value; }
        }
        [Column(Name = "HOUSEHOLD_ID")]
        public String HouseholdId
        {
            get { return householdId; }
            set { householdId = value; }
        }
        [Column(Name = "REGION_CD")]
        public String RegionCd
        {
            get { return regionCd; }
            set { regionCd = value; }
        }
        [Column(Name = "Zone_CD")]
        public String ZoneCd
        {
            get { return zoneCd; }
            set { zoneCd = value; }
        }
        [Column(Name = "District_CD")]
        public String DistrictCd
        {
            get { return districtCd; }
            set { districtCd = value; }
        }
        [Column(Name = "VDCMun_CD")]
        public String VDCMunCd
        {
            get { return vDCMunCd; }
            set { vDCMunCd = value; }
        }
        [Column(Name = "Ward_CD")]
        public String WardCd
        {
            get { return wardCd; }
            set { wardCd = value; }
        }
       
        [Column(Name = "HH_FULL_NAME")]
        public String HhFullName
        {
            get { return hhFullName; }
            set { hhFullName = value; }
        }
        [Column(Name = "CASE_STATUS")]
        public String CaseStatus
        {
            get { return caseStatus; }
            set { caseStatus = value; }
        }
        [Column(Name = "PIN_NO")]
        public String PinNo
        {
            get { return pinNo; }
            set { pinNo = value; }
        }
        [Column(Name = "ADDRESS")]
        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        [Column(Name = "PHONE_NO")]
        public String PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }
        [Column(Name = "GRIEVANCE_CATEGORY")]
        public String GrievanceCategory
        {
            get { return grievanceCategory; }
            set { grievanceCategory = value; }
        }
        [Column(Name = "GRIEVANCE_GRP_CATEGORY")]
        public String GrievanceCategoryGroup
        {
            get { return grievanceCategoryGroup; }
            set { grievanceCategoryGroup = value; }
        }
        [Column(Name = "CASE_DESC")]
        public String CaseDesc
        {
            get { return caseDesc; }
            set { caseDesc = value; }
        }
        [Column(Name = "SIGN_APPLICANT")]
        public String SignApplicant
        {
            get { return signApplicant; }
            set { signApplicant = value; }
        }
        [Column(Name = "SIGNED_APP_DATE")]
        public String SignAppDate
        {
            get { return signAppDate; }
            set { signAppDate = value; }
        }

        [Column(Name = "SIGN_DSW_OFFICER")]
        public String SignDswOfficer
        {
            get { return signDswOfficer; }
            set { signDswOfficer = value; }
        }
        [Column(Name = "SIGNED_DSW_DATE")]
        public String SignDswDate
        {
            get { return signDswDate; }
            set { signDswDate = value; }
        }
        [Column(Name = "SIGN_SENIOR_OFFICER")]
        public String SignSeniorOfficer
        {
            get { return signSeniorOfficer; }
            set { signSeniorOfficer = value; }
        }
        [Column(Name = "SIGNED_SENIOR_DATE")]
        public String SignSeniorDate
        {
            get { return signSeniorDate; }
            set { signSeniorDate = value; }
        }
        [Column(Name = "PROCESS_REMARK")]
        public String ProcessRemark
        {
            get { return processRemark; }
            set { processRemark = value; }
        }
        [Column(Name = "FINAL_REMARK")]
        public String FinalRemark
        {
            get { return finalRemark; }
            set { finalRemark = value; }
        }
        [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
        public String Approved
        {
            get { return approved; }
            set { approved = value; }
        }

        [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
        public String ApprovedBy
        {
            get { return approvedBy; }
            set { approvedBy = value; }
        }

        [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
        public String ApprovedDt
        {
            get { return approvedDt; }
            set { approvedDt = value; }
        }
      

        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
        public String EnteredBy
        {
            get { return enteredBy; }
            set { enteredBy = value; }
        }

        [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
        public String EnteredDt
        {
            get { return enteredDt; }
            set { enteredDt = value; }
        }

        [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
        public String UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        [Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
        public String UpdatedDt
        {
            get { return updatedDt; }
            set { updatedDt = value; }
        }

      

        [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
        public String IpAddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }
    }

