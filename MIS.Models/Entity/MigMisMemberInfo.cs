using System;
using EntityFramework;

[Table(Name = "MIG_MIS_MEMBER")]
public class MigMisMemberInfo : EntityBase
{

	private System.String memberId = null;

	private System.String definedCd = null;

	private System.String houseId = null;

	private System.String householdId = null;

    private System.String houseOwnerId = null;

    private System.String electionCenterNo = null;

	private System.String householdDefinedCd = null;

	private System.String hhFormNo = null;

	private System.String firstNameEng = null;

	private System.String middleNameEng = null;

	private System.String lastNameEng = null;

	private System.String fullNameEng = null;

	private System.String firstNameLoc = null;

	private System.String middleNameLoc = null;

	private System.String lastNameLoc = null;

	private System.String fullNameLoc = null;

	private System.String memberPhotoId = null;

	private System.Decimal? genderCd = null;

	private System.Decimal? maritalStatusCd = null;

	private System.String householdHead = null;

	private System.Decimal? birthYear = null;

	private System.Decimal? birthMonth = null;

	private System.Decimal? birthDay = null;

	private System.String birthYearLoc = null;

	private System.String birthMonthLoc = null;

	private System.String birthDayLoc = null;

	private System.DateTime? birthDt = null;

	private System.String birthDtLoc = null;

	private System.Decimal? age = null;

	private System.Decimal? casteCd = null;

	private System.Decimal? religionCd = null;

	private System.String literate = null;

	private System.Decimal? educationCd = null;

	private System.String birthCertificate = null;

	private System.String birthCertificateNo = null;

	private System.Decimal? birthCerDistrictCd = null;

	private System.String ctzIssue = null;

	private System.String ctzNo = null;

    private System.Decimal? ctzIssueDistrictCd = null;

	private System.Decimal? ctzIssueYear = null;

	private System.Decimal? ctzIssueMonth = null;

	private System.Decimal? ctzIssueDay = null;

	private System.String ctzIssueYearLoc = null;

	private System.String ctzIssueMonthLoc = null;

	private System.String ctzIssueDayLoc = null;

	private System.DateTime? ctzIssueDt = null;

	private System.String ctzIssueDtLoc = null;

	private System.String voterId = null;

	private System.String voteridNo = null;

	private System.Decimal? voteridDistrictCd = null;

	private System.DateTime? voteridIssueDt = null;

	private System.String voteridIssueDtLoc = null;

	private System.String nidNo = null;

	private System.Decimal? nidDistrictCd = null;

	private System.DateTime? nidIssueDt = null;

	private System.String nidIssueDtLoc = null;

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

	private System.String telNo = null;

	private System.String mobileNo = null;

	private System.String fax = null;

	private System.String email = null;

	private System.String url = null;

	private System.String poBoxNo = null;

	private System.String passportNo = null;

	private System.Decimal? passportIssueDistrict = null;

	private System.String proFundNo = null;

	private System.String citNo = null;

	private System.String panNo = null;

	private System.String drivingLicenseNo = null;

    private System.Decimal? drivingLicIssDistrict = null;

    private System.String socialAllowanceId = null;

    private System.Decimal? socialAllIssDistrict = null;

	private System.String death = null;

	private System.String remarks = null;

	private System.String remarksLoc = null;

	private System.String nidIssue = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.String approvedByLoc = null;

	private System.DateTime? approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	private System.String updatedByLoc = null;

	private System.DateTime? updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	private System.String enteredByLoc = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String disability = null;

	private System.String disabilityCertificateId = null;

    private System.Decimal? disabilityCertIssDistrict = null;

	private System.Decimal? handiColorCd = null;

	private System.String targetFlag = null;

	private System.String regFormNo = null;

	private System.String defRegFormNo = null;

	private System.String regFormType = null;

	private System.String officeCd = null;

	private System.String regno = null;

	private System.String socialAllowance = null;

	private System.Decimal? indentificationTypeCd = null;

	private System.String indentificationOthers = null;

	private System.String indentificationOthersLoc = null;

	private System.String bankAccountFlag = null;

	private System.Decimal? bankCd = null;

	private System.Decimal? bankBranchCd = null;

	private System.String hhMemberBankAccNo = null;

    private System.String foreignHouseheadCountryEng = null;

    private System.String foreignHouseheadCountryLoc = null;

	private System.String memberActive = null;

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

    private System.String pidNo = null;

	private System.String ipAddress = null;

    private System.String buildstructno = null;

    private System.String hoDefinedCd = null;

    private System.String instanceUniqueSno = null;

    private System.String pR_FHH = null;

    private System.String pR_MHH = null;

    private System.String pR_FM = null;

    private System.String pR_MM = null;

	[Column(Name = "MEMBER_ID", IsKey = true, SequenceName = "")]
	public System.String MemberId 
	{ 
		get{return memberId;}
		set{memberId = value;} 
	}

	[Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
	public System.String DefinedCd 
	{ 
		get{return definedCd;}
		set{definedCd = value;} 
	}

	[Column(Name = "HOUSE_ID", IsKey = false, SequenceName = "")]
	public System.String HouseId 
	{ 
		get{return houseId;}
		set{houseId = value;} 
	}

    [Column(Name = "HOUSE_OWNER_ID", IsKey = false, SequenceName = "")]
    public System.String HouseOwnerId
    {
        get { return houseOwnerId; }
        set { houseOwnerId = value; }
    }

    [Column(Name = "ELECTION_CENTER_NO", IsKey = false, SequenceName = "")]
    public System.String ElectionCenterNo
    {
        get { return electionCenterNo; }
        set { electionCenterNo = value; }
    }

	[Column(Name = "HOUSEHOLD_ID", IsKey = false, SequenceName = "")]
	public System.String HouseholdId 
	{ 
		get{return householdId;}
		set{householdId = value;} 
	}

	[Column(Name = "HOUSEHOLD_DEFINED_CD", IsKey = false, SequenceName = "")]
	public System.String HouseholdDefinedCd 
	{ 
		get{return householdDefinedCd;}
		set{householdDefinedCd = value;} 
	}

	[Column(Name = "HH_FORM_NO", IsKey = false, SequenceName = "")]
	public System.String HhFormNo 
	{ 
		get{return hhFormNo;}
		set{hhFormNo = value;} 
	}

	[Column(Name = "FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
	public System.String FirstNameEng 
	{ 
		get{return firstNameEng;}
		set{firstNameEng = value;} 
	}

	[Column(Name = "MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
	public System.String MiddleNameEng 
	{ 
		get{return middleNameEng;}
		set{middleNameEng = value;} 
	}

	[Column(Name = "LAST_NAME_ENG", IsKey = false, SequenceName = "")]
	public System.String LastNameEng 
	{ 
		get{return lastNameEng;}
		set{lastNameEng = value;} 
	}

	[Column(Name = "FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public System.String FullNameEng 
	{ 
		get{return fullNameEng;}
		set{fullNameEng = value;} 
	}

	[Column(Name = "FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String FirstNameLoc 
	{ 
		get{return firstNameLoc;}
		set{firstNameLoc = value;} 
	}

	[Column(Name = "MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String MiddleNameLoc 
	{ 
		get{return middleNameLoc;}
		set{middleNameLoc = value;} 
	}

	[Column(Name = "LAST_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String LastNameLoc 
	{ 
		get{return lastNameLoc;}
		set{lastNameLoc = value;} 
	}

	[Column(Name = "FULL_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String FullNameLoc 
	{ 
		get{return fullNameLoc;}
		set{fullNameLoc = value;} 
	}

	[Column(Name = "MEMBER_PHOTO_ID", IsKey = false, SequenceName = "")]
	public System.String MemberPhotoId 
	{ 
		get{return memberPhotoId;}
		set{memberPhotoId = value;} 
	}

	[Column(Name = "GENDER_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? GenderCd 
	{ 
		get{return genderCd;}
		set{genderCd = value;} 
	}

	[Column(Name = "MARITAL_STATUS_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? MaritalStatusCd 
	{ 
		get{return maritalStatusCd;}
		set{maritalStatusCd = value;} 
	}

	[Column(Name = "HOUSEHOLD_HEAD", IsKey = false, SequenceName = "")]
	public System.String HouseholdHead 
	{ 
		get{return householdHead;}
		set{householdHead = value;} 
	}

	[Column(Name = "BIRTH_YEAR", IsKey = false, SequenceName = "")]
	public System.Decimal? BirthYear 
	{ 
		get{return birthYear;}
		set{birthYear = value;} 
	}

	[Column(Name = "BIRTH_MONTH", IsKey = false, SequenceName = "")]
	public System.Decimal? BirthMonth 
	{ 
		get{return birthMonth;}
		set{birthMonth = value;} 
	}

	[Column(Name = "BIRTH_DAY", IsKey = false, SequenceName = "")]
	public System.Decimal? BirthDay 
	{ 
		get{return birthDay;}
		set{birthDay = value;} 
	}

	[Column(Name = "BIRTH_YEAR_LOC", IsKey = false, SequenceName = "")]
	public System.String BirthYearLoc 
	{ 
		get{return birthYearLoc;}
		set{birthYearLoc = value;} 
	}

	[Column(Name = "BIRTH_MONTH_LOC", IsKey = false, SequenceName = "")]
	public System.String BirthMonthLoc 
	{ 
		get{return birthMonthLoc;}
		set{birthMonthLoc = value;} 
	}

	[Column(Name = "BIRTH_DAY_LOC", IsKey = false, SequenceName = "")]
	public System.String BirthDayLoc 
	{ 
		get{return birthDayLoc;}
		set{birthDayLoc = value;} 
	}

	[Column(Name = "BIRTH_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? BirthDt 
	{ 
		get{return birthDt;}
		set{birthDt = value;} 
	}

	[Column(Name = "BIRTH_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String BirthDtLoc 
	{ 
		get{return birthDtLoc;}
		set{birthDtLoc = value;} 
	}

	[Column(Name = "AGE", IsKey = false, SequenceName = "")]
	public System.Decimal? Age 
	{ 
		get{return age;}
		set{age = value;} 
	}

	[Column(Name = "CASTE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CasteCd 
	{ 
		get{return casteCd;}
		set{casteCd = value;} 
	}

	[Column(Name = "RELIGION_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? ReligionCd 
	{ 
		get{return religionCd;}
		set{religionCd = value;} 
	}

	[Column(Name = "LITERATE", IsKey = false, SequenceName = "")]
	public System.String Literate 
	{ 
		get{return literate;}
		set{literate = value;} 
	}

	[Column(Name = "EDUCATION_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? EducationCd 
	{ 
		get{return educationCd;}
		set{educationCd = value;} 
	}

	[Column(Name = "BIRTH_CERTIFICATE", IsKey = false, SequenceName = "")]
	public System.String BirthCertificate 
	{ 
		get{return birthCertificate;}
		set{birthCertificate = value;} 
	}

	[Column(Name = "BIRTH_CERTIFICATE_NO", IsKey = false, SequenceName = "")]
	public System.String BirthCertificateNo 
	{ 
		get{return birthCertificateNo;}
		set{birthCertificateNo = value;} 
	}

	[Column(Name = "BIRTH_CER_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? BirthCerDistrictCd 
	{ 
		get{return birthCerDistrictCd;}
		set{birthCerDistrictCd = value;} 
	}

	[Column(Name = "CTZ_ISSUE", IsKey = false, SequenceName = "")]
	public System.String CtzIssue 
	{ 
		get{return ctzIssue;}
		set{ctzIssue = value;} 
	}

	[Column(Name = "CTZ_NO", IsKey = false, SequenceName = "")]
	public System.String CtzNo 
	{ 
		get{return ctzNo;}
		set{ctzNo = value;} 
	}

	[Column(Name = "CTZ_ISSUE_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CtzIssueDistrictCd 
	{ 
		get{return ctzIssueDistrictCd;}
		set{ctzIssueDistrictCd = value;} 
	}

	[Column(Name = "CTZ_ISSUE_YEAR", IsKey = false, SequenceName = "")]
	public System.Decimal? CtzIssueYear 
	{ 
		get{return ctzIssueYear;}
		set{ctzIssueYear = value;} 
	}

	[Column(Name = "CTZ_ISSUE_MONTH", IsKey = false, SequenceName = "")]
	public System.Decimal? CtzIssueMonth 
	{ 
		get{return ctzIssueMonth;}
		set{ctzIssueMonth = value;} 
	}

	[Column(Name = "CTZ_ISSUE_DAY", IsKey = false, SequenceName = "")]
	public System.Decimal? CtzIssueDay 
	{ 
		get{return ctzIssueDay;}
		set{ctzIssueDay = value;} 
	}

	[Column(Name = "CTZ_ISSUE_YEAR_LOC", IsKey = false, SequenceName = "")]
	public System.String CtzIssueYearLoc 
	{ 
		get{return ctzIssueYearLoc;}
		set{ctzIssueYearLoc = value;} 
	}

	[Column(Name = "CTZ_ISSUE_MONTH_LOC", IsKey = false, SequenceName = "")]
	public System.String CtzIssueMonthLoc 
	{ 
		get{return ctzIssueMonthLoc;}
		set{ctzIssueMonthLoc = value;} 
	}

	[Column(Name = "CTZ_ISSUE_DAY_LOC", IsKey = false, SequenceName = "")]
	public System.String CtzIssueDayLoc 
	{ 
		get{return ctzIssueDayLoc;}
		set{ctzIssueDayLoc = value;} 
	}

	[Column(Name = "CTZ_ISSUE_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? CtzIssueDt 
	{ 
		get{return ctzIssueDt;}
		set{ctzIssueDt = value;} 
	}

	[Column(Name = "CTZ_ISSUE_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String CtzIssueDtLoc 
	{ 
		get{return ctzIssueDtLoc;}
		set{ctzIssueDtLoc = value;} 
	}

	[Column(Name = "VOTER_ID", IsKey = false, SequenceName = "")]
	public System.String VoterId 
	{ 
		get{return voterId;}
		set{voterId = value;} 
	}

	[Column(Name = "VOTERID_NO", IsKey = false, SequenceName = "")]
	public System.String VoteridNo 
	{ 
		get{return voteridNo;}
		set{voteridNo = value;} 
	}

	[Column(Name = "VOTERID_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? VoteridDistrictCd 
	{ 
		get{return voteridDistrictCd;}
		set{voteridDistrictCd = value;} 
	}

	[Column(Name = "VOTERID_ISSUE_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? VoteridIssueDt 
	{ 
		get{return voteridIssueDt;}
		set{voteridIssueDt = value;} 
	}

	[Column(Name = "VOTERID_ISSUE_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String VoteridIssueDtLoc 
	{ 
		get{return voteridIssueDtLoc;}
		set{voteridIssueDtLoc = value;} 
	}

	[Column(Name = "NID_NO", IsKey = false, SequenceName = "")]
	public System.String NidNo 
	{ 
		get{return nidNo;}
		set{nidNo = value;} 
	}

	[Column(Name = "NID_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? NidDistrictCd 
	{ 
		get{return nidDistrictCd;}
		set{nidDistrictCd = value;} 
	}

	[Column(Name = "NID_ISSUE_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? NidIssueDt 
	{ 
		get{return nidIssueDt;}
		set{nidIssueDt = value;} 
	}

	[Column(Name = "NID_ISSUE_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String NidIssueDtLoc 
	{ 
		get{return nidIssueDtLoc;}
		set{nidIssueDtLoc = value;} 
	}

	[Column(Name = "PER_COUNTRY_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? PerCountryCd 
	{ 
		get{return perCountryCd;}
		set{perCountryCd = value;} 
	}

	[Column(Name = "PER_REG_ST_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? PerRegStCd 
	{ 
		get{return perRegStCd;}
		set{perRegStCd = value;} 
	}

	[Column(Name = "PER_ZONE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? PerZoneCd 
	{ 
		get{return perZoneCd;}
		set{perZoneCd = value;} 
	}

	[Column(Name = "PER_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? PerDistrictCd 
	{ 
		get{return perDistrictCd;}
		set{perDistrictCd = value;} 
	}

	[Column(Name = "PER_VDC_MUN_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? PerVdcMunCd 
	{ 
		get{return perVdcMunCd;}
		set{perVdcMunCd = value;} 
	}

	[Column(Name = "PER_WARD_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? PerWardNo 
	{ 
		get{return perWardNo;}
		set{perWardNo = value;} 
	}

	[Column(Name = "PER_AREA_ENG", IsKey = false, SequenceName = "")]
	public System.String PerAreaEng 
	{ 
		get{return perAreaEng;}
		set{perAreaEng = value;} 
	}

	[Column(Name = "PER_AREA_LOC", IsKey = false, SequenceName = "")]
	public System.String PerAreaLoc 
	{ 
		get{return perAreaLoc;}
		set{perAreaLoc = value;} 
	}

	[Column(Name = "CUR_COUNTRY_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CurCountryCd 
	{ 
		get{return curCountryCd;}
		set{curCountryCd = value;} 
	}

	[Column(Name = "CUR_REG_ST_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CurRegStCd 
	{ 
		get{return curRegStCd;}
		set{curRegStCd = value;} 
	}

	[Column(Name = "CUR_ZONE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CurZoneCd 
	{ 
		get{return curZoneCd;}
		set{curZoneCd = value;} 
	}

	[Column(Name = "CUR_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CurDistrictCd 
	{ 
		get{return curDistrictCd;}
		set{curDistrictCd = value;} 
	}

	[Column(Name = "CUR_VDC_MUN_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CurVdcMunCd 
	{ 
		get{return curVdcMunCd;}
		set{curVdcMunCd = value;} 
	}

	[Column(Name = "CUR_WARD_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? CurWardNo 
	{ 
		get{return curWardNo;}
		set{curWardNo = value;} 
	}

	[Column(Name = "CUR_AREA_ENG", IsKey = false, SequenceName = "")]
	public System.String CurAreaEng 
	{ 
		get{return curAreaEng;}
		set{curAreaEng = value;} 
	}

	[Column(Name = "CUR_AREA_LOC", IsKey = false, SequenceName = "")]
	public System.String CurAreaLoc 
	{ 
		get{return curAreaLoc;}
		set{curAreaLoc = value;} 
	}

	[Column(Name = "TEL_NO", IsKey = false, SequenceName = "")]
	public System.String TelNo 
	{ 
		get{return telNo;}
		set{telNo = value;} 
	}

	[Column(Name = "MOBILE_NO", IsKey = false, SequenceName = "")]
	public System.String MobileNo 
	{ 
		get{return mobileNo;}
		set{mobileNo = value;} 
	}

	[Column(Name = "FAX", IsKey = false, SequenceName = "")]
	public System.String Fax 
	{ 
		get{return fax;}
		set{fax = value;} 
	}

	[Column(Name = "EMAIL", IsKey = false, SequenceName = "")]
	public System.String Email 
	{ 
		get{return email;}
		set{email = value;} 
	}

	[Column(Name = "URL", IsKey = false, SequenceName = "")]
	public System.String Url 
	{ 
		get{return url;}
		set{url = value;} 
	}

	[Column(Name = "PO_BOX_NO", IsKey = false, SequenceName = "")]
	public System.String PoBoxNo 
	{ 
		get{return poBoxNo;}
		set{poBoxNo = value;} 
	}

	[Column(Name = "PASSPORT_NO", IsKey = false, SequenceName = "")]
	public System.String PassportNo 
	{ 
		get{return passportNo;}
		set{passportNo = value;} 
	}

	[Column(Name = "PASSPORT_ISSUE_DISTRICT", IsKey = false, SequenceName = "")]
	public System.Decimal? PassportIssueDistrict 
	{ 
		get{return passportIssueDistrict;}
		set{passportIssueDistrict = value;} 
	}

	[Column(Name = "PRO_FUND_NO", IsKey = false, SequenceName = "")]
	public System.String ProFundNo 
	{ 
		get{return proFundNo;}
		set{proFundNo = value;} 
	}

	[Column(Name = "CIT_NO", IsKey = false, SequenceName = "")]
	public System.String CitNo 
	{ 
		get{return citNo;}
		set{citNo = value;} 
	}

	[Column(Name = "PAN_NO", IsKey = false, SequenceName = "")]
	public System.String PanNo 
	{ 
		get{return panNo;}
		set{panNo = value;} 
	}

	[Column(Name = "DRIVING_LICENSE_NO", IsKey = false, SequenceName = "")]
	public System.String DrivingLicenseNo 
	{ 
		get{return drivingLicenseNo;}
		set{drivingLicenseNo = value;} 
	}

    [Column(Name = "DRIVING_LIC_ISS_DISTRICT", IsKey = false, SequenceName = "")]
    public System.Decimal? DrivingLicIssDistrict 
	{
        get { return drivingLicIssDistrict; }
        set { drivingLicIssDistrict = value; } 
	}

    [Column(Name = "SOCIAL_ALLOWANCE_ID", IsKey = false, SequenceName = "")]
    public System.String SocialAllowanceId
    {
        get { return socialAllowanceId; }
        set { socialAllowanceId = value; }
    }

    [Column(Name = "SOCIAL_ALL_ISS_DISTRICT", IsKey = false, SequenceName = "")]
    public System.Decimal? SocialAllIssDistrict
    {
        get { return socialAllIssDistrict; }
        set { socialAllIssDistrict = value; }
    }

	[Column(Name = "DEATH", IsKey = false, SequenceName = "")]
	public System.String Death 
	{ 
		get{return death;}
		set{death = value;} 
	}

	[Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
	public System.String Remarks 
	{ 
		get{return remarks;}
		set{remarks = value;} 
	}

	[Column(Name = "REMARKS_LOC", IsKey = false, SequenceName = "")]
	public System.String RemarksLoc 
	{ 
		get{return remarksLoc;}
		set{remarksLoc = value;} 
	}

	[Column(Name = "NID_ISSUE", IsKey = false, SequenceName = "")]
	public System.String NidIssue 
	{ 
		get{return nidIssue;}
		set{nidIssue = value;} 
	}

	[Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
	public System.String Approved 
	{ 
		get{return approved;}
		set{approved = value;} 
	}

	[Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
	public System.String ApprovedBy 
	{ 
		get{return approvedBy;}
		set{approvedBy = value;} 
	}

	[Column(Name = "APPROVED_BY_LOC", IsKey = false, SequenceName = "")]
	public System.String ApprovedByLoc 
	{ 
		get{return approvedByLoc;}
		set{approvedByLoc = value;} 
	}

	[Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? ApprovedDt 
	{ 
		get{return approvedDt;}
		set{approvedDt = value;} 
	}

	[Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String ApprovedDtLoc 
	{ 
		get{return approvedDtLoc;}
		set{approvedDtLoc = value;} 
	}

	[Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
	public System.String UpdatedBy 
	{ 
		get{return updatedBy;}
		set{updatedBy = value;} 
	}

	[Column(Name = "UPDATED_BY_LOC", IsKey = false, SequenceName = "")]
	public System.String UpdatedByLoc 
	{ 
		get{return updatedByLoc;}
		set{updatedByLoc = value;} 
	}

	[Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? UpdatedDt 
	{ 
		get{return updatedDt;}
		set{updatedDt = value;} 
	}

	[Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String UpdatedDtLoc 
	{ 
		get{return updatedDtLoc;}
		set{updatedDtLoc = value;} 
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public System.String EnteredBy 
	{ 
		get{return enteredBy;}
		set{enteredBy = value;} 
	}

	[Column(Name = "ENTERED_BY_LOC", IsKey = false, SequenceName = "")]
	public System.String EnteredByLoc 
	{ 
		get{return enteredByLoc;}
		set{enteredByLoc = value;} 
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? EnteredDt 
	{ 
		get{return enteredDt;}
		set{enteredDt = value;} 
	}

	[Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String EnteredDtLoc 
	{ 
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;} 
	}

	[Column(Name = "DISABILITY", IsKey = false, SequenceName = "")]
	public System.String Disability 
	{ 
		get{return disability;}
		set{disability = value;} 
	}

	[Column(Name = "DISABILITY_CERTIFICATE_ID", IsKey = false, SequenceName = "")]
	public System.String DisabilityCertificateId 
	{ 
		get{return disabilityCertificateId;}
		set{disabilityCertificateId = value;} 
	}

    [Column(Name = "DISABILITY_CERT_ISS_DISTRICT", IsKey = false, SequenceName = "")]
	public System.Decimal? DisabilityCertIssDistrict 
	{
        get { return disabilityCertIssDistrict; }
        set { disabilityCertIssDistrict = value; } 
	}

	[Column(Name = "HANDI_COLOR_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? HandiColorCd 
	{ 
		get{return handiColorCd;}
		set{handiColorCd = value;} 
	}

	[Column(Name = "TARGET_FLAG", IsKey = false, SequenceName = "")]
	public System.String TargetFlag 
	{ 
		get{return targetFlag;}
		set{targetFlag = value;} 
	}

	[Column(Name = "REG_FORM_NO", IsKey = false, SequenceName = "")]
	public System.String RegFormNo 
	{ 
		get{return regFormNo;}
		set{regFormNo = value;} 
	}

	[Column(Name = "DEF_REG_FORM_NO", IsKey = false, SequenceName = "")]
	public System.String DefRegFormNo 
	{ 
		get{return defRegFormNo;}
		set{defRegFormNo = value;} 
	}

	[Column(Name = "REG_FORM_TYPE", IsKey = false, SequenceName = "")]
	public System.String RegFormType 
	{ 
		get{return regFormType;}
		set{regFormType = value;} 
	}

	[Column(Name = "OFFICE_CD", IsKey = false, SequenceName = "")]
	public System.String OfficeCd 
	{ 
		get{return officeCd;}
		set{officeCd = value;} 
	}

	[Column(Name = "REGNO", IsKey = false, SequenceName = "")]
	public System.String Regno 
	{ 
		get{return regno;}
		set{regno = value;} 
	}

	[Column(Name = "SOCIAL_ALLOWANCE", IsKey = false, SequenceName = "")]
	public System.String SocialAllowance 
	{ 
		get{return socialAllowance;}
		set{socialAllowance = value;} 
	}

	[Column(Name = "IDENTIFICATION_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? IndentificationTypeCd 
	{ 
		get{return indentificationTypeCd;}
		set{indentificationTypeCd = value;} 
	}

	[Column(Name = "IDENTIFICATION_OTHERS", IsKey = false, SequenceName = "")]
	public System.String IndentificationOthers 
	{ 
		get{return indentificationOthers;}
		set{indentificationOthers = value;} 
	}

	[Column(Name = "IDENTIFICATION_OTHERS_LOC", IsKey = false, SequenceName = "")]
	public System.String IndentificationOthersLoc 
	{ 
		get{return indentificationOthersLoc;}
		set{indentificationOthersLoc = value;} 
	}

	[Column(Name = "BANK_ACCOUNT_FLAG", IsKey = false, SequenceName = "")]
	public System.String BankAccountFlag 
	{ 
		get{return bankAccountFlag;}
		set{bankAccountFlag = value;} 
	}

	[Column(Name = "BANK_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? BankCd 
	{ 
		get{return bankCd;}
		set{bankCd = value;} 
	}

	[Column(Name = "BANK_BRANCH_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? BankBranchCd 
	{ 
		get{return bankBranchCd;}
		set{bankBranchCd = value;} 
	}

	[Column(Name = "HH_MEMBER_BANK_ACC_NO", IsKey = false, SequenceName = "")]
	public System.String HhMemberBankAccNo 
	{ 
		get{return hhMemberBankAccNo;}
		set{hhMemberBankAccNo = value;} 
	}

    [Column(Name = "FOREIGN_HH_COUNTRY_ENG", IsKey = false, SequenceName = "")]
    public System.String ForeignHouseheadCountryEng
    {
        get { return foreignHouseheadCountryEng; }
        set { foreignHouseheadCountryEng = value; }
    }

    [Column(Name = "FOREIGN_HH_COUNTRY_LOC", IsKey = false, SequenceName = "")]
    public System.String ForeignHouseheadCountryLoc
    {
        get { return foreignHouseheadCountryLoc; }
        set { foreignHouseheadCountryLoc = value; }
    }

	[Column(Name = "MEMBER_ACTIVE", IsKey = false, SequenceName = "")]
	public System.String MemberActive 
	{ 
		get{return memberActive;}
		set{memberActive = value;} 
	}

	[Column(Name = "NHRS_UUID", IsKey = false, SequenceName = "")]
	public System.String NhrsUuid 
	{ 
		get{return nhrsUuid;}
		set{nhrsUuid = value;} 
	}

	[Column(Name = "BATCH_ID", IsKey = false, SequenceName = "")]
	public System.Decimal? BatchId 
	{ 
		get{return batchId;}
		set{batchId = value;} 
	}

	[Column(Name = "ERROR_NO", IsKey = false, SequenceName = "")]
	public System.String ErrorNo 
	{ 
		get{return errorNo;}
		set{errorNo = value;} 
	}

	[Column(Name = "ERROR_MSG", IsKey = false, SequenceName = "")]
	public System.String ErrorMsg 
	{ 
		get{return errorMsg;}
		set{errorMsg = value;} 
	}

    [Column(Name = "PID_NO", IsKey = false, SequenceName = "")]
    public System.String PidNo
    {
        get { return pidNo; }
        set { pidNo = value; }
    }


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

    [Column(Name = "BUILDING_STRUCTURE_NO", IsKey = false, SequenceName = "")]
    public String BuildstructNo
    {
        get { return buildstructno; }
        set { buildstructno = value; }
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
     
     [Column(Name = "PR_FHH", IsKey = false, SequenceName = "")]
    public System.String PR_FHH
    {
        get { return pR_FHH; }
        set { pR_FHH = value; }
    }
 
     [Column(Name = "PR_MHH", IsKey = false, SequenceName = "")]
     public System.String PR_MHH
    {
        get { return pR_MHH; }
        set { pR_MHH = value; }
    }
   
     [Column(Name = "PR_FM", IsKey = false, SequenceName = "")]
     public System.String PR_FM
    {
        get { return pR_FM; }
        set { pR_FM = value; }
    }
          
     [Column(Name = "PR_MM", IsKey = false, SequenceName = "")]
     public System.String PR_MM
    {
        get { return pR_MM; }
        set { pR_MM = value; }
    }
	public MigMisMemberInfo()
	{}

   
}