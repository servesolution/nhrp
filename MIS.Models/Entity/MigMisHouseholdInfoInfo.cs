using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HOUSEHOLD_INFO")]
public class MigMisHouseholdInfoInfo : EntityBase
{

    private System.String householdId = null;

    private System.String definedCd = null;

    private System.String houseOwnerId = null;

    private System.String buildingStructureNo = null;

    private System.String memberId = null;

    private System.String memberDefinedCd = null;

    private System.String firstNameEng = null;

    private System.String middleNameEng = null;

    private System.String lastNameEng = null;

    private System.String fullNameEng = null;

    private System.String firstNameLoc = null;

    private System.String middleNameLoc = null;

    private System.String lastNameLoc = null;

    private System.String fullNameLoc = null;

    private System.String respondentIsHhHead = null;

    private System.String respondentFirstName = null;

    private System.String respondentMiddleName = null;

    private System.String respondentLastName = null;

    private System.String respondentFullName = null;

    private System.String respondentFirstNameLoc = null;

    private System.String respondentMiddleNameLoc = null;

    private System.String respondentLastNameLoc = null;

    private System.String respondentFullNameLoc = null;

    private System.String respondentPhoto = null;

    private System.Decimal? respondentGenderCd = null;

    private System.Decimal? hhRelationTypeCd = null;

    private System.Decimal? memberCnt = null;

    private System.Decimal? perCountryCd = null;

    private System.Decimal? perRegStCd = null;

    private System.Decimal? perZoneCd = null;

    private System.Decimal? perDistrictCd = null;

    private System.Decimal? perVdcMunCd = null;

    private System.Decimal? perWardNo = null;

    private System.String perAreaEng = null;

    private System.String perAreaLoc = null;

    private System.Decimal? curCountryCd = null;

    private System.Decimal? curRegStCd = null;

    private System.Decimal? curZoneCd = null;

    private System.Decimal? curDistrictCd = null;

    private System.Decimal? curVdcMunCd = null;

    private System.Decimal? curWardNo = null;

    private System.String curAreaEng = null;

    private System.String curAreaLoc = null;

    private System.String houseNo = null;

    private System.String telNo = null;

    private System.String mobileNo = null;

    private System.String fax = null;

    private System.String email = null;

    private System.String url = null;

    private System.String poBoxNo = null;

    private System.Decimal? shelterSinceQuakeCd = null;

    private System.Decimal? shelterBeforeQuakeCd = null;

    private System.Decimal? shelterBeforeEqothDistrict = null;

    private System.Decimal? currentShelterCd = null;

    private System.Decimal? currentShelterOthDistrict = null;

    private System.String eqVictimIdentityCard = null;

    private System.Decimal? eqVictimIdentityCardCd = null;

    private System.String eqVictimIdentityCardNo = null;

    private System.String eqVictimIdcardPhotoFront = null;

    private System.String eqVictimIdcardPhotoBack = null;

    private System.Decimal? monthlyIncomeCd = null;

    private System.String deathInAYear = null;

    private System.Decimal? deathCnt = null;

    private System.String humanDestroyFlag = null;

    private System.Decimal? humanDestroyCnt = null;

    private System.String studentSchoolLeft = null;

    private System.Decimal? studentSchoolLeftCnt = null;

    private System.String pregnantRegularCheckup = null;

    private System.Decimal? pregnantRegularCheckupCnt = null;

    private System.String childLeftVacination = null;

    private System.Decimal? childLeftVacinationCnt = null;

    private System.String leftChangeOccupany = null;

    private System.Decimal? leftChangeOccupanyCnt = null;

    private System.String householdActive = null;

    private System.String childInSchool = null;

    private System.String socialAllowance = null;

    private System.String remarks = null;

    private System.String remarksLoc = null;

    private System.String targetBatchId = null;

    private System.String enrollmentBatchId = null;

    private System.Decimal? caseId = null;

    private System.String caseDefinedCd = null;

    private System.String officeCd = null;

    private System.String nhrsUuid = null;

    private System.String approved = null;

    private System.String approvedBy = null;

    private System.DateTime? approvedDt = null;

    private System.String approvedDtLoc = null;

    private System.String updatedBy = null;

    private System.DateTime? updatedDt = null;

    private System.String updatedDtLoc = null;

    private System.String enteredBy = null;

    private System.DateTime? enteredDt = null;

    private System.String enteredDtLoc = null;

    private System.Decimal? batchId = null;

    private System.String errorNo = null;

    private System.String errorMsg = null;

    private System.String hoDefinedCd = null;

    private System.String instanceUniqueSno = null; 

    private System.String ipAddress = null;

    [Column(Name = "HOUSEHOLD_ID", IsKey = true, SequenceName = "")]
    public System.String HouseholdId
    {
        get { return householdId; }
        set { householdId = value; }
    }

    [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String DefinedCd
    {
        get { return definedCd; }
        set { definedCd = value; }
    }

    [Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
    public System.String HouseOwnerId
    {
        get { return houseOwnerId; }
        set { houseOwnerId = value; }
    }

    [Column(Name = "BUILDING_STRUCTURE_NO", IsKey = false, SequenceName = "")]
    public System.String BuildingStructureNo
    {
        get { return buildingStructureNo; }
        set { buildingStructureNo = value; }
    }

    [Column(Name = "MEMBER_ID", IsKey = false, SequenceName = "")]
    public System.String MemberId
    {
        get { return memberId; }
        set { memberId = value; }
    }

    [Column(Name = "MEMBER_DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String MemberDefinedCd
    {
        get { return memberDefinedCd; }
        set { memberDefinedCd = value; }
    }

    [Column(Name = "FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
    public System.String FirstNameEng
    {
        get { return firstNameEng; }
        set { firstNameEng = value; }
    }

    [Column(Name = "MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
    public System.String MiddleNameEng
    {
        get { return middleNameEng; }
        set { middleNameEng = value; }
    }

    [Column(Name = "LAST_NAME_ENG", IsKey = false, SequenceName = "")]
    public System.String LastNameEng
    {
        get { return lastNameEng; }
        set { lastNameEng = value; }
    }

    [Column(Name = "FULL_NAME_ENG", IsKey = false, SequenceName = "")]
    public System.String FullNameEng
    {
        get { return fullNameEng; }
        set { fullNameEng = value; }
    }

    [Column(Name = "FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String FirstNameLoc
    {
        get { return firstNameLoc; }
        set { firstNameLoc = value; }
    }

    [Column(Name = "MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String MiddleNameLoc
    {
        get { return middleNameLoc; }
        set { middleNameLoc = value; }
    }

    [Column(Name = "LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String LastNameLoc
    {
        get { return lastNameLoc; }
        set { lastNameLoc = value; }
    }

    [Column(Name = "FULL_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String FullNameLoc
    {
        get { return fullNameLoc; }
        set { fullNameLoc = value; }
    }

    [Column(Name = "RESPONDENT_IS_HH_HEAD", IsKey = false, SequenceName = "")]
    public System.String RespondentIsHhHead
    {
        get { return respondentIsHhHead; }
        set { respondentIsHhHead = value; }
    }

    [Column(Name = "RESPONDENT_FIRST_NAME", IsKey = false, SequenceName = "")]
    public System.String RespondentFirstName
    {
        get { return respondentFirstName; }
        set { respondentFirstName = value; }
    }

    [Column(Name = "RESPONDENT_MIDDLE_NAME", IsKey = false, SequenceName = "")]
    public System.String RespondentMiddleName
    {
        get { return respondentMiddleName; }
        set { respondentMiddleName = value; }
    }

    [Column(Name = "RESPONDENT_LAST_NAME", IsKey = false, SequenceName = "")]
    public System.String RespondentLastName
    {
        get { return respondentLastName; }
        set { respondentLastName = value; }
    }

    [Column(Name = "RESPONDENT_FULL_NAME", IsKey = false, SequenceName = "")]
    public System.String RespondentFullName
    {
        get { return respondentFullName; }
        set { respondentFullName = value; }
    }

    [Column(Name = "RESPONDENT_FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String RespondentFirstNameLoc
    {
        get { return respondentFirstNameLoc; }
        set { respondentFirstNameLoc = value; }
    }

    [Column(Name = "RESPONDENT_MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String RespondentMiddleNameLoc
    {
        get { return respondentMiddleNameLoc; }
        set { respondentMiddleNameLoc = value; }
    }

    [Column(Name = "RESPONDENT_LAST_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String RespondentLastNameLoc
    {
        get { return respondentLastNameLoc; }
        set { respondentLastNameLoc = value; }
    }

    [Column(Name = "RESPONDENT_FULL_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String RespondentFullNameLoc
    {
        get { return respondentFullNameLoc; }
        set { respondentFullNameLoc = value; }
    }

    [Column(Name = "RESPONDENT_PHOTO", IsKey = false, SequenceName = "")]
    public System.String RespondentPhoto
    {
        get { return respondentPhoto; }
        set { respondentPhoto = value; }
    }

    [Column(Name = "RESPONDENT_GENDER_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? RespondentGenderCd
    {
        get { return respondentGenderCd; }
        set { respondentGenderCd = value; }
    }

    [Column(Name = "HH_RELATION_TYPE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? HhRelationTypeCd
    {
        get { return hhRelationTypeCd; }
        set { hhRelationTypeCd = value; }
    }

    [Column(Name = "MEMBER_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? MemberCnt
    {
        get { return memberCnt; }
        set { memberCnt = value; }
    }

    [Column(Name = "PER_COUNTRY_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? PerCountryCd
    {
        get { return perCountryCd; }
        set { perCountryCd = value; }
    }

    [Column(Name = "PER_REG_ST_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? PerRegStCd
    {
        get { return perRegStCd; }
        set { perRegStCd = value; }
    }

    [Column(Name = "PER_ZONE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? PerZoneCd
    {
        get { return perZoneCd; }
        set { perZoneCd = value; }
    }

    [Column(Name = "PER_DISTRICT_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? PerDistrictCd
    {
        get { return perDistrictCd; }
        set { perDistrictCd = value; }
    }

    [Column(Name = "PER_VDC_MUN_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? PerVdcMunCd
    {
        get { return perVdcMunCd; }
        set { perVdcMunCd = value; }
    }

    [Column(Name = "PER_WARD_NO", IsKey = false, SequenceName = "")]
    public System.Decimal? PerWardNo
    {
        get { return perWardNo; }
        set { perWardNo = value; }
    }

    [Column(Name = "PER_AREA_ENG", IsKey = false, SequenceName = "")]
    public System.String PerAreaEng
    {
        get { return perAreaEng; }
        set { perAreaEng = value; }
    }

    [Column(Name = "PER_AREA_LOC", IsKey = false, SequenceName = "")]
    public System.String PerAreaLoc
    {
        get { return perAreaLoc; }
        set { perAreaLoc = value; }
    }

    [Column(Name = "CUR_COUNTRY_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CurCountryCd
    {
        get { return curCountryCd; }
        set { curCountryCd = value; }
    }

    [Column(Name = "CUR_REG_ST_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CurRegStCd
    {
        get { return curRegStCd; }
        set { curRegStCd = value; }
    }

    [Column(Name = "CUR_ZONE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CurZoneCd
    {
        get { return curZoneCd; }
        set { curZoneCd = value; }
    }

    [Column(Name = "CUR_DISTRICT_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CurDistrictCd
    {
        get { return curDistrictCd; }
        set { curDistrictCd = value; }
    }

    [Column(Name = "CUR_VDC_MUN_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CurVdcMunCd
    {
        get { return curVdcMunCd; }
        set { curVdcMunCd = value; }
    }

    [Column(Name = "CUR_WARD_NO", IsKey = false, SequenceName = "")]
    public System.Decimal? CurWardNo
    {
        get { return curWardNo; }
        set { curWardNo = value; }
    }

    [Column(Name = "CUR_AREA_ENG", IsKey = false, SequenceName = "")]
    public System.String CurAreaEng
    {
        get { return curAreaEng; }
        set { curAreaEng = value; }
    }

    [Column(Name = "CUR_AREA_LOC", IsKey = false, SequenceName = "")]
    public System.String CurAreaLoc
    {
        get { return curAreaLoc; }
        set { curAreaLoc = value; }
    }

    [Column(Name = "HOUSE_NO", IsKey = false, SequenceName = "")]
    public System.String HouseNo
    {
        get { return houseNo; }
        set { houseNo = value; }
    }

    [Column(Name = "TEL_NO", IsKey = false, SequenceName = "")]
    public System.String TelNo
    {
        get { return telNo; }
        set { telNo = value; }
    }

    [Column(Name = "MOBILE_NO", IsKey = false, SequenceName = "")]
    public System.String MobileNo
    {
        get { return mobileNo; }
        set { mobileNo = value; }
    }

    [Column(Name = "FAX", IsKey = false, SequenceName = "")]
    public System.String Fax
    {
        get { return fax; }
        set { fax = value; }
    }

    [Column(Name = "EMAIL", IsKey = false, SequenceName = "")]
    public System.String Email
    {
        get { return email; }
        set { email = value; }
    }

    [Column(Name = "URL", IsKey = false, SequenceName = "")]
    public System.String Url
    {
        get { return url; }
        set { url = value; }
    }

    [Column(Name = "PO_BOX_NO", IsKey = false, SequenceName = "")]
    public System.String PoBoxNo
    {
        get { return poBoxNo; }
        set { poBoxNo = value; }
    }

    [Column(Name = "SHELTER_SINCE_QUAKE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? ShelterSinceQuakeCd
    {
        get { return shelterSinceQuakeCd; }
        set { shelterSinceQuakeCd = value; }
    }

    [Column(Name = "SHELTER_BEFORE_QUAKE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? ShelterBeforeQuakeCd
    {
        get { return shelterBeforeQuakeCd; }
        set { shelterBeforeQuakeCd = value; }
    }

    [Column(Name = "SHELTER_BEFORE_EQOTH_DIS", IsKey = false, SequenceName = "")]
    public System.Decimal? ShelterBeforeEqothDistrict
    {
        get { return shelterBeforeEqothDistrict; }
        set { shelterBeforeEqothDistrict = value; }
    }

    [Column(Name = "CURRENT_SHELTER_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CurrentShelterCd
    {
        get { return currentShelterCd; }
        set { currentShelterCd = value; }
    }

    [Column(Name = "CURRENT_SHELTER_OTH_DISTRICT", IsKey = false, SequenceName = "")]
    public System.Decimal? CurrentShelterOthDistrict
    {
        get { return currentShelterOthDistrict; }
        set { currentShelterOthDistrict = value; }
    }

    [Column(Name = "EQ_VICTIM_IDENTITY_CARD", IsKey = false, SequenceName = "")]
    public System.String EqVictimIdentityCard
    {
        get { return eqVictimIdentityCard; }
        set { eqVictimIdentityCard = value; }
    }

    [Column(Name = "EQ_VICTIM_IDENTITY_CARD_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? EqVictimIdentityCardCd
    {
        get { return eqVictimIdentityCardCd; }
        set { eqVictimIdentityCardCd = value; }
    }

    [Column(Name = "EQ_VICTIM_IDENTITY_CARD_NO", IsKey = false, SequenceName = "")]
    public System.String EqVictimIdentityCardNo
    {
        get { return eqVictimIdentityCardNo; }
        set { eqVictimIdentityCardNo = value; }
    }

    [Column(Name = "EQ_VICTIM_IDCARD_PHOTO_FRONT", IsKey = false, SequenceName = "")]
    public System.String EqVictimIdcardPhotoFront
    {
        get { return eqVictimIdcardPhotoFront; }
        set { eqVictimIdcardPhotoFront = value; }
    }

    [Column(Name = "EQ_VICTIM_IDCARD_PHOTO_BACK", IsKey = false, SequenceName = "")]
    public System.String EqVictimIdcardPhotoBack
    {
        get { return eqVictimIdcardPhotoBack; }
        set { eqVictimIdcardPhotoBack = value; }
    }

    [Column(Name = "MONTHLY_INCOME_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? MonthlyIncomeCd
    {
        get { return monthlyIncomeCd; }
        set { monthlyIncomeCd = value; }
    }

    [Column(Name = "DEATH_IN_A_YEAR", IsKey = false, SequenceName = "")]
    public System.String DeathInAYear
    {
        get { return deathInAYear; }
        set { deathInAYear = value; }
    }

    [Column(Name = "DEATH_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? DeathCnt
    {
        get { return deathCnt; }
        set { deathCnt = value; }
    }

    [Column(Name = "HUMAN_DESTROY_FLAG", IsKey = false, SequenceName = "")]
    public System.String HumanDestroyFlag
    {
        get { return humanDestroyFlag; }
        set { humanDestroyFlag = value; }
    }

    [Column(Name = "HUMAN_DESTROY_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? HumanDestroyCnt
    {
        get { return humanDestroyCnt; }
        set { humanDestroyCnt = value; }
    }

    [Column(Name = "STUDENT_SCHOOL_LEFT", IsKey = false, SequenceName = "")]
    public System.String StudentSchoolLeft
    {
        get { return studentSchoolLeft; }
        set { studentSchoolLeft = value; }
    }

    [Column(Name = "STUDENT_SCHOOL_LEFT_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? StudentSchoolLeftCnt
    {
        get { return studentSchoolLeftCnt; }
        set { studentSchoolLeftCnt = value; }
    }

    [Column(Name = "PREGNANT_REGULAR_CHECKUP", IsKey = false, SequenceName = "")]
    public System.String PregnantRegularCheckup
    {
        get { return pregnantRegularCheckup; }
        set { pregnantRegularCheckup = value; }
    }

    [Column(Name = "PREGNANT_REGULAR_CHECKUP_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? PregnantRegularCheckupCnt
    {
        get { return pregnantRegularCheckupCnt; }
        set { pregnantRegularCheckupCnt = value; }
    }

    [Column(Name = "CHILD_LEFT_VACINATION", IsKey = false, SequenceName = "")]
    public System.String ChildLeftVacination
    {
        get { return childLeftVacination; }
        set { childLeftVacination = value; }
    }

    [Column(Name = "CHILD_LEFT_VACINATION_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? ChildLeftVacinationCnt
    {
        get { return childLeftVacinationCnt; }
        set { childLeftVacinationCnt = value; }
    }

    [Column(Name = "LEFT_CHANGE_OCCUPANY", IsKey = false, SequenceName = "")]
    public System.String LeftChangeOccupany
    {
        get { return leftChangeOccupany; }
        set { leftChangeOccupany = value; }
    }

    [Column(Name = "LEFT_CHANGE_OCCUPANY_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? LeftChangeOccupanyCnt
    {
        get { return leftChangeOccupanyCnt; }
        set { leftChangeOccupanyCnt = value; }
    }

    [Column(Name = "HOUSEHOLD_ACTIVE", IsKey = false, SequenceName = "")]
    public System.String HouseholdActive
    {
        get { return householdActive; }
        set { householdActive = value; }
    }

    [Column(Name = "CHILD_IN_SCHOOL", IsKey = false, SequenceName = "")]
    public System.String ChildInSchool
    {
        get { return childInSchool; }
        set { childInSchool = value; }
    }

    [Column(Name = "SOCIAL_ALLOWANCE", IsKey = false, SequenceName = "")]
    public System.String SocialAllowance
    {
        get { return socialAllowance; }
        set { socialAllowance = value; }
    }

    [Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
    public System.String Remarks
    {
        get { return remarks; }
        set { remarks = value; }
    }

    [Column(Name = "REMARKS_LOC", IsKey = false, SequenceName = "")]
    public System.String RemarksLoc
    {
        get { return remarksLoc; }
        set { remarksLoc = value; }
    }

    [Column(Name = "TARGET_BATCH_ID", IsKey = false, SequenceName = "")]
    public System.String TargetBatchId
    {
        get { return targetBatchId; }
        set { targetBatchId = value; }
    }

    [Column(Name = "ENROLLMENT_BATCH_ID", IsKey = false, SequenceName = "")]
    public System.String EnrollmentBatchId
    {
        get { return enrollmentBatchId; }
        set { enrollmentBatchId = value; }
    }

    [Column(Name = "CASE_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? CaseId
    {
        get { return caseId; }
        set { caseId = value; }
    }

    [Column(Name = "CASE_DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String CaseDefinedCd
    {
        get { return caseDefinedCd; }
        set { caseDefinedCd = value; }
    }

    [Column(Name = "OFFICE_CD", IsKey = false, SequenceName = "")]
    public System.String OfficeCd
    {
        get { return officeCd; }
        set { officeCd = value; }
    }

    [Column(Name = "NHRS_UUID", IsKey = false, SequenceName = "")]
    public System.String NhrsUuid
    {
        get { return nhrsUuid; }
        set { nhrsUuid = value; }
    }

    [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
    public System.String Approved
    {
        get { return approved; }
        set { approved = value; }
    }

    [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
    public System.String ApprovedBy
    {
        get { return approvedBy; }
        set { approvedBy = value; }
    }

    [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? ApprovedDt
    {
        get { return approvedDt; }
        set { approvedDt = value; }
    }

    [Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String ApprovedDtLoc
    {
        get { return approvedDtLoc; }
        set { approvedDtLoc = value; }
    }

    [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
    public System.String UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }

    [Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? UpdatedDt
    {
        get { return updatedDt; }
        set { updatedDt = value; }
    }

    [Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String UpdatedDtLoc
    {
        get { return updatedDtLoc; }
        set { updatedDtLoc = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String EnteredBy
    {
        get { return enteredBy; }
        set { enteredBy = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? EnteredDt
    {
        get { return enteredDt; }
        set { enteredDt = value; }
    }

    [Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String EnteredDtLoc
    {
        get { return enteredDtLoc; }
        set { enteredDtLoc = value; }
    }

    [Column(Name = "BATCH_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? BatchId
    {
        get { return batchId; }
        set { batchId = value; }
    }

    [Column(Name = "ERROR_NO", IsKey = false, SequenceName = "")]
    public System.String ErrorNo
    {
        get { return errorNo; }
        set { errorNo = value; }
    }

    [Column(Name = "ERROR_MSG", IsKey = false, SequenceName = "")]
    public System.String ErrorMsg
    {
        get { return errorMsg; }
        set { errorMsg = value; }
    }

    [Column(Name = "HO_DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String HoDefinedCd
    {
        get { return hoDefinedCd; }
        set { hoDefinedCd = value; }
    }

    [Column(Name = "INSTANCE_UNIQUE_SNO", IsKey = false, SequenceName = "")]
    public System.String InstanceUniqueSno
    {
        get { return instanceUniqueSno; }
        set { instanceUniqueSno = value; }
    }


    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

    public MigMisHouseholdInfoInfo()
    { }
}