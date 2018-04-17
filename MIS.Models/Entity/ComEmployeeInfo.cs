using System;
using EntityFramework;

[Table(Name = "COM_EMPLOYEE")]
public class ComEmployeeInfo : EntityBase
{
	private String employeeCd  = null;
	private String defEmployeeCd  = null;
	private String firstNameEng  = null;
	private String middleNameEng  = null;
	private String lastNameEng  = null;
	private String fullNameEng  = null;
	private String firstNameLoc  = null;
	private String middleNameLoc  = null;
	private String lastNameLoc  = null;
	private String fullNameLoc  = null;
    private String birthDt = DateTime.Today.ToString("dd-MM-yyyy");
	private String birthDtLoc  = null;
	private String maritalStatusCd  = null;
    private String genderCd = null;
	private String fatherFnameEng  = null;
	private String fatherMnameEng  = null;
	private String fatherLnameEng  = null;
	private String fatherFullNameEng  = null;
	private String fatherFnameLoc  = null;
	private String fatherMnameLoc  = null;
	private String fatherLnameLoc  = null;
	private String fatherFullnameLoc  = null;
	private String motherFnameEng  = null;
	private String motherMnameEng  = null;
	private String motherLnameEng  = null;
	private String motherFullNameEng  = null;
	private String motherFnameLoc  = null;
	private String motherMnameLoc  = null;
	private String motherLnameLoc  = null;
	private String motherFullnameLoc  = null;
	private String gfatherFnameEng  = null;
	private String gfatherMnameEng  = null;
	private String gfatherLnameEng  = null;
	private String gfatherFullNameEng  = null;
	private String gfatherFnameLoc  = null;
	private String gfatherMnameLoc  = null;
	private String gfatherLnameLoc  = null;
	private String gfatherFullnameLoc  = null;
	private String spouseFnameEng  = null;
	private String spouseMnameEng  = null;
	private String spouseLnameEng  = null;
	private String spouseFullNameEng  = null;
	private String spouseFnameLoc  = null;
	private String spouseMnameLoc  = null;
	private String spouseLnameLoc  = null;
	private String spouseFullnameLoc  = null;
	private String citizenshipNo  = null;
	private String citizenIssueDistrict  = null;
    private String citizenIssueDt = DateTime.Today.ToString("dd-MM-yyyy");
	private String citizenIssueDtLoc  = null;
	private String passportNo  = null;
	private String passportIssueDistrict  = null;
    private String passportIssueDt = DateTime.Today.ToString("dd-MM-yyyy");
	private String passportIssueDtLoc  = null;
	private String proFundNo  = null;
	private String citNo  = null;
	private String panNo  = null;
	private String drivingLicenseNo  = null;
    private String perCountryCd = null;
    private String perRegStCd = null;
    private String perZoneCd = null;
    private String perDistrictCd = null;
    private String perVdcMunCd = null;
    private String perWardNo = null;
	private String perStreet  = null;
	private String perStreetLoc  = null;
	private String tmpCountryCd  = null;
	private String tmpRegStCd  = null;
	private String tmpZoneCd  = null;
	private String tmpDistrictCd  = null;
	private String tmpVdcMunCd  = null;
	private String tmpWardNo  = null;
	private String tmpStreet  = null;
	private String tmpStreetLoc  = null;
	private String officeCd  = null;
	private String sectionCd  = null;
    private String officeJoinDt = DateTime.Today.ToString("dd-MM-yyyy");
	private String officeJoinDtLoc  = null;
	private String positionCd  = null;
	private String posSubClassCd  = null;
	private String designationCd  = null;
	private bool retiredFlag  = false;
    private String retiredDt = DateTime.Today.ToString("dd-MM-yyyy");
	private String retiredDtLoc  = null;
	private String mobileNumber  = null;
	private String telephoneNo  = null;
	private String faxNumber  = null;
	private String poBox  = null;
	private String email  = null;
	private String otherContactInfo  = null;
	private String otherContactInfoLoc  = null;
	private String remarks  = null;
	private String remarksLoc  = null;
	private String enteredBy  = null;
    private String enteredDt = DateTime.Today.ToString("dd-MM-yyyy");
	private bool approved  = false;
	private String approvedBy  = null;
    private String approvedDt = DateTime.Today.ToString("dd-MM-yyyy");
	private String approvedDtLoc  = null;
    private String ipaddress = null;

    private String imageURL = null;

	[Column(Name = "EMPLOYEE_CD", IsKey = true, SequenceName = "")]
	public String EmployeeCd
	{
		get{return employeeCd;}
		set{employeeCd = value;}
	}

	[Column(Name = "DEF_EMPLOYEE_CD", IsKey = false, SequenceName = "")]
	public String DefEmployeeCd
	{
		get{return defEmployeeCd;}
		set{defEmployeeCd = value;}
	}

	[Column(Name = "FIRST_NAME_ENG", IsKey = false, SequenceName = "")]
	public String FirstNameEng
	{
		get{return firstNameEng;}
		set{firstNameEng = value;}
	}

	[Column(Name = "MIDDLE_NAME_ENG", IsKey = false, SequenceName = "")]
	public String MiddleNameEng
	{
		get{return middleNameEng;}
		set{middleNameEng = value;}
	}

	[Column(Name = "LAST_NAME_ENG", IsKey = false, SequenceName = "")]
	public String LastNameEng
	{
		get{return lastNameEng;}
		set{lastNameEng = value;}
	}

	[Column(Name = "FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public String FullNameEng
	{
		get{return fullNameEng;}
		set{fullNameEng = value;}
	}

	[Column(Name = "FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
	public String FirstNameLoc
	{
		get{return firstNameLoc;}
		set{firstNameLoc = value;}
	}

	[Column(Name = "MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
	public String MiddleNameLoc
	{
		get{return middleNameLoc;}
		set{middleNameLoc = value;}
	}

	[Column(Name = "LAST_NAME_LOC", IsKey = false, SequenceName = "")]
	public String LastNameLoc
	{
		get{return lastNameLoc;}
		set{lastNameLoc = value;}
	}

	[Column(Name = "FULL_NAME_LOC", IsKey = false, SequenceName = "")]
	public String FullNameLoc
	{
		get{return fullNameLoc;}
		set{fullNameLoc = value;}
	}

	[Column(Name = "BIRTH_DT", IsKey = false, SequenceName = "")]
	public String BirthDt
	{
		get{return birthDt;}
		set{birthDt = value;}
	}

	[Column(Name = "BIRTH_DT_LOC", IsKey = false, SequenceName = "")]
	public String BirthDtLoc
	{
		get{return birthDtLoc;}
		set{birthDtLoc = value;}
	}

	[Column(Name = "MARITAL_STATUS_CD", IsKey = false, SequenceName = "")]
	public String MaritalStatusCd
	{
		get{return maritalStatusCd;}
		set{maritalStatusCd = value;}
	}

	[Column(Name = "GENDER_CD", IsKey = false, SequenceName = "")]
    public String GenderCd
	{
		get{return genderCd;}
		set{genderCd = value;}
	}

	[Column(Name = "FATHER_FNAME_ENG", IsKey = false, SequenceName = "")]
	public String FatherFnameEng
	{
		get{return fatherFnameEng;}
		set{fatherFnameEng = value;}
	}

	[Column(Name = "FATHER_MNAME_ENG", IsKey = false, SequenceName = "")]
	public String FatherMnameEng
	{
		get{return fatherMnameEng;}
		set{fatherMnameEng = value;}
	}

	[Column(Name = "FATHER_LNAME_ENG", IsKey = false, SequenceName = "")]
	public String FatherLnameEng
	{
		get{return fatherLnameEng;}
		set{fatherLnameEng = value;}
	}

	[Column(Name = "FATHER_FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public String FatherFullNameEng
	{
		get{return fatherFullNameEng;}
		set{fatherFullNameEng = value;}
	}

	[Column(Name = "FATHER_FNAME_LOC", IsKey = false, SequenceName = "")]
	public String FatherFnameLoc
	{
		get{return fatherFnameLoc;}
		set{fatherFnameLoc = value;}
	}

	[Column(Name = "FATHER_MNAME_LOC", IsKey = false, SequenceName = "")]
	public String FatherMnameLoc
	{
		get{return fatherMnameLoc;}
		set{fatherMnameLoc = value;}
	}

	[Column(Name = "FATHER_LNAME_LOC", IsKey = false, SequenceName = "")]
	public String FatherLnameLoc
	{
		get{return fatherLnameLoc;}
		set{fatherLnameLoc = value;}
	}

	[Column(Name = "FATHER_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public String FatherFullnameLoc
	{
		get{return fatherFullnameLoc;}
		set{fatherFullnameLoc = value;}
	}

	[Column(Name = "MOTHER_FNAME_ENG", IsKey = false, SequenceName = "")]
	public String MotherFnameEng
	{
		get{return motherFnameEng;}
		set{motherFnameEng = value;}
	}

	[Column(Name = "MOTHER_MNAME_ENG", IsKey = false, SequenceName = "")]
	public String MotherMnameEng
	{
		get{return motherMnameEng;}
		set{motherMnameEng = value;}
	}

	[Column(Name = "MOTHER_LNAME_ENG", IsKey = false, SequenceName = "")]
	public String MotherLnameEng
	{
		get{return motherLnameEng;}
		set{motherLnameEng = value;}
	}

	[Column(Name = "MOTHER_FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public String MotherFullNameEng
	{
		get{return motherFullNameEng;}
		set{motherFullNameEng = value;}
	}

	[Column(Name = "MOTHER_FNAME_LOC", IsKey = false, SequenceName = "")]
	public String MotherFnameLoc
	{
		get{return motherFnameLoc;}
		set{motherFnameLoc = value;}
	}

	[Column(Name = "MOTHER_MNAME_LOC", IsKey = false, SequenceName = "")]
	public String MotherMnameLoc
	{
		get{return motherMnameLoc;}
		set{motherMnameLoc = value;}
	}

	[Column(Name = "MOTHER_LNAME_LOC", IsKey = false, SequenceName = "")]
	public String MotherLnameLoc
	{
		get{return motherLnameLoc;}
		set{motherLnameLoc = value;}
	}

	[Column(Name = "MOTHER_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public String MotherFullnameLoc
	{
		get{return motherFullnameLoc;}
		set{motherFullnameLoc = value;}
	}

	[Column(Name = "GFATHER_FNAME_ENG", IsKey = false, SequenceName = "")]
	public String GfatherFnameEng
	{
		get{return gfatherFnameEng;}
		set{gfatherFnameEng = value;}
	}

	[Column(Name = "GFATHER_MNAME_ENG", IsKey = false, SequenceName = "")]
	public String GfatherMnameEng
	{
		get{return gfatherMnameEng;}
		set{gfatherMnameEng = value;}
	}

	[Column(Name = "GFATHER_LNAME_ENG", IsKey = false, SequenceName = "")]
	public String GfatherLnameEng
	{
		get{return gfatherLnameEng;}
		set{gfatherLnameEng = value;}
	}

	[Column(Name = "GFATHER_FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public String GfatherFullNameEng
	{
		get{return gfatherFullNameEng;}
		set{gfatherFullNameEng = value;}
	}

	[Column(Name = "GFATHER_FNAME_LOC", IsKey = false, SequenceName = "")]
	public String GfatherFnameLoc
	{
		get{return gfatherFnameLoc;}
		set{gfatherFnameLoc = value;}
	}

	[Column(Name = "GFATHER_MNAME_LOC", IsKey = false, SequenceName = "")]
	public String GfatherMnameLoc
	{
		get{return gfatherMnameLoc;}
		set{gfatherMnameLoc = value;}
	}

	[Column(Name = "GFATHER_LNAME_LOC", IsKey = false, SequenceName = "")]
	public String GfatherLnameLoc
	{
		get{return gfatherLnameLoc;}
		set{gfatherLnameLoc = value;}
	}

	[Column(Name = "GFATHER_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public String GfatherFullnameLoc
	{
		get{return gfatherFullnameLoc;}
		set{gfatherFullnameLoc = value;}
	}

	[Column(Name = "SPOUSE_FNAME_ENG", IsKey = false, SequenceName = "")]
	public String SpouseFnameEng
	{
		get{return spouseFnameEng;}
		set{spouseFnameEng = value;}
	}

	[Column(Name = "SPOUSE_MNAME_ENG", IsKey = false, SequenceName = "")]
	public String SpouseMnameEng
	{
		get{return spouseMnameEng;}
		set{spouseMnameEng = value;}
	}

	[Column(Name = "SPOUSE_LNAME_ENG", IsKey = false, SequenceName = "")]
	public String SpouseLnameEng
	{
		get{return spouseLnameEng;}
		set{spouseLnameEng = value;}
	}

	[Column(Name = "SPOUSE_FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public String SpouseFullNameEng
	{
		get{return spouseFullNameEng;}
		set{spouseFullNameEng = value;}
	}

	[Column(Name = "SPOUSE_FNAME_LOC", IsKey = false, SequenceName = "")]
	public String SpouseFnameLoc
	{
		get{return spouseFnameLoc;}
		set{spouseFnameLoc = value;}
	}

	[Column(Name = "SPOUSE_MNAME_LOC", IsKey = false, SequenceName = "")]
	public String SpouseMnameLoc
	{
		get{return spouseMnameLoc;}
		set{spouseMnameLoc = value;}
	}

	[Column(Name = "SPOUSE_LNAME_LOC", IsKey = false, SequenceName = "")]
	public String SpouseLnameLoc
	{
		get{return spouseLnameLoc;}
		set{spouseLnameLoc = value;}
	}

	[Column(Name = "SPOUSE_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public String SpouseFullnameLoc
	{
		get{return spouseFullnameLoc;}
		set{spouseFullnameLoc = value;}
	}

	[Column(Name = "CITIZENSHIP_NO", IsKey = false, SequenceName = "")]
	public String CitizenshipNo
	{
		get{return citizenshipNo;}
		set{citizenshipNo = value;}
	}

	[Column(Name = "CITIZEN_ISSUE_DISTRICT", IsKey = false, SequenceName = "")]
	public String CitizenIssueDistrict
	{
		get{return citizenIssueDistrict;}
		set{citizenIssueDistrict = value;}
	}

	[Column(Name = "CITIZEN_ISSUE_DT", IsKey = false, SequenceName = "")]
	public String CitizenIssueDt
	{
		get{return citizenIssueDt;}
		set{citizenIssueDt = value;}
	}

	[Column(Name = "CITIZEN_ISSUE_DT_LOC", IsKey = false, SequenceName = "")]
	public String CitizenIssueDtLoc
	{
		get{return citizenIssueDtLoc;}
		set{citizenIssueDtLoc = value;}
	}

	[Column(Name = "PASSPORT_NO", IsKey = false, SequenceName = "")]
	public String PassportNo
	{
		get{return passportNo;}
		set{passportNo = value;}
	}

	[Column(Name = "PASSPORT_ISSUE_DISTRICT", IsKey = false, SequenceName = "")]
	public String PassportIssueDistrict
	{
		get{return passportIssueDistrict;}
		set{passportIssueDistrict = value;}
	}

	[Column(Name = "PASSPORT_ISSUE_DT", IsKey = false, SequenceName = "")]
	public String PassportIssueDt
	{
		get{return passportIssueDt;}
		set{passportIssueDt = value;}
	}

	[Column(Name = "PASSPORT_ISSUE_DT_LOC", IsKey = false, SequenceName = "")]
	public String PassportIssueDtLoc
	{
		get{return passportIssueDtLoc;}
		set{passportIssueDtLoc = value;}
	}

	[Column(Name = "PRO_FUND_NO", IsKey = false, SequenceName = "")]
	public String ProFundNo
	{
		get{return proFundNo;}
		set{proFundNo = value;}
	}

	[Column(Name = "CIT_NO", IsKey = false, SequenceName = "")]
	public String CitNo
	{
		get{return citNo;}
		set{citNo = value;}
	}

	[Column(Name = "PAN_NO", IsKey = false, SequenceName = "")]
	public String PanNo
	{
		get{return panNo;}
		set{panNo = value;}
	}

	[Column(Name = "DRIVING_LICENSE_NO", IsKey = false, SequenceName = "")]
	public String DrivingLicenseNo
	{
		get{return drivingLicenseNo;}
		set{drivingLicenseNo = value;}
	}

	[Column(Name = "PER_COUNTRY_CD", IsKey = false, SequenceName = "")]
	public String PerCountryCd
	{
		get{return perCountryCd;}
		set{perCountryCd = value;}
	}

	[Column(Name = "PER_REG_ST_CD", IsKey = false, SequenceName = "")]
	public String PerRegStCd
	{
		get{return perRegStCd;}
		set{perRegStCd = value;}
	}

	[Column(Name = "PER_ZONE_CD", IsKey = false, SequenceName = "")]
	public String PerZoneCd
	{
		get{return perZoneCd;}
		set{perZoneCd = value;}
	}

	[Column(Name = "PER_DISTRICT_CD", IsKey = false, SequenceName = "")]
    public String PerDistrictCd
	{
		get{return perDistrictCd;}
		set{perDistrictCd = value;}
	}

	[Column(Name = "PER_VDC_MUN_CD", IsKey = false, SequenceName = "")]
	public String PerVdcMunCd
	{
		get{return perVdcMunCd;}
		set{perVdcMunCd = value;}
	}

	[Column(Name = "PER_WARD_NO", IsKey = false, SequenceName = "")]
    public String PerWardNo
	{
		get{return perWardNo;}
		set{perWardNo = value;}
	}

	[Column(Name = "PER_STREET", IsKey = false, SequenceName = "")]
	public String PerStreet
	{
		get{return perStreet;}
		set{perStreet = value;}
	}

	[Column(Name = "PER_STREET_LOC", IsKey = false, SequenceName = "")]
	public String PerStreetLoc
	{
		get{return perStreetLoc;}
		set{perStreetLoc = value;}
	}

	[Column(Name = "TMP_COUNTRY_CD", IsKey = false, SequenceName = "")]
    public String TmpCountryCd
	{
		get{return tmpCountryCd;}
		set{tmpCountryCd = value;}
	}

	[Column(Name = "TMP_REG_ST_CD", IsKey = false, SequenceName = "")]
    public String TmpRegStCd
	{
		get{return tmpRegStCd;}
		set{tmpRegStCd = value;}
	}

	[Column(Name = "TMP_ZONE_CD", IsKey = false, SequenceName = "")]
    public String TmpZoneCd
	{
		get{return tmpZoneCd;}
		set{tmpZoneCd = value;}
	}

	[Column(Name = "TMP_DISTRICT_CD", IsKey = false, SequenceName = "")]
    public String TmpDistrictCd
	{
		get{return tmpDistrictCd;}
		set{tmpDistrictCd = value;}
	}

	[Column(Name = "TMP_VDC_MUN_CD", IsKey = false, SequenceName = "")]
    public String TmpVdcMunCd
	{
		get{return tmpVdcMunCd;}
		set{tmpVdcMunCd = value;}
	}

	[Column(Name = "TMP_WARD_NO", IsKey = false, SequenceName = "")]
    public String TmpWardNo
	{
		get{return tmpWardNo;}
		set{tmpWardNo = value;}
	}

	[Column(Name = "TMP_STREET", IsKey = false, SequenceName = "")]
	public String TmpStreet
	{
		get{return tmpStreet;}
		set{tmpStreet = value;}
	}

	[Column(Name = "TMP_STREET_LOC", IsKey = false, SequenceName = "")]
	public String TmpStreetLoc
	{
		get{return tmpStreetLoc;}
		set{tmpStreetLoc = value;}
	}

	[Column(Name = "OFFICE_CD", IsKey = false, SequenceName = "")]
	public String OfficeCd
	{
		get{return officeCd;}
		set{officeCd = value;}
	}

	[Column(Name = "SECTION_CD", IsKey = false, SequenceName = "")]
	public String SectionCd
	{
		get{return sectionCd;}
		set{sectionCd = value;}
	}

	[Column(Name = "OFFICE_JOIN_DT", IsKey = false, SequenceName = "")]
	public String OfficeJoinDt
	{
		get{return officeJoinDt;}
		set{officeJoinDt = value;}
	}

	[Column(Name = "OFFICE_JOIN_DT_LOC", IsKey = false, SequenceName = "")]
	public String OfficeJoinDtLoc
	{
		get{return officeJoinDtLoc;}
		set{officeJoinDtLoc = value;}
	}

	[Column(Name = "POSITION_CD", IsKey = false, SequenceName = "")]
	public String PositionCd
	{
		get{return positionCd;}
		set{positionCd = value;}
	}

	[Column(Name = "POS_SUB_CLASS_CD", IsKey = false, SequenceName = "")]
	public String PosSubClassCd
	{
		get{return posSubClassCd;}
		set{posSubClassCd = value;}
	}

	[Column(Name = "DESIGNATION_CD", IsKey = false, SequenceName = "")]
	public String DesignationCd
	{
		get{return designationCd;}
		set{designationCd = value;}
	}

	[Column(Name = "RETIRED_FLAG", IsKey = false, SequenceName = "")]
	public bool RetiredFlag
	{
		get{return retiredFlag;}
		set{retiredFlag = value;}
	}

	[Column(Name = "RETIRED_DT", IsKey = false, SequenceName = "")]
	public String RetiredDt
	{
		get{return retiredDt;}
		set{retiredDt = value;}
	}

	[Column(Name = "RETIRED_DT_LOC", IsKey = false, SequenceName = "")]
	public String RetiredDtLoc
	{
		get{return retiredDtLoc;}
		set{retiredDtLoc = value;}
	}

	[Column(Name = "MOBILE_NUMBER", IsKey = false, SequenceName = "")]
	public String MobileNumber
	{
		get{return mobileNumber;}
		set{mobileNumber = value;}
	}

	[Column(Name = "TELEPHONE_NO", IsKey = false, SequenceName = "")]
	public String TelephoneNo
	{
		get{return telephoneNo;}
		set{telephoneNo = value;}
	}

	[Column(Name = "FAX_NUMBER", IsKey = false, SequenceName = "")]
	public String FaxNumber
	{
		get{return faxNumber;}
		set{faxNumber = value;}
	}

	[Column(Name = "PO_BOX", IsKey = false, SequenceName = "")]
	public String PoBox
	{
		get{return poBox;}
		set{poBox = value;}
	}

	[Column(Name = "EMAIL", IsKey = false, SequenceName = "")]
	public String Email
	{
		get{return email;}
		set{email = value;}
	}

	[Column(Name = "OTHER_CONTACT_INFO", IsKey = false, SequenceName = "")]
	public String OtherContactInfo
	{
		get{return otherContactInfo;}
		set{otherContactInfo = value;}
	}

	[Column(Name = "OTHER_CONTACT_INFO_LOC", IsKey = false, SequenceName = "")]
	public String OtherContactInfoLoc
	{
		get{return otherContactInfoLoc;}
		set{otherContactInfoLoc = value;}
	}

	[Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
	public String Remarks
	{
		get{return remarks;}
		set{remarks = value;}
	}

	[Column(Name = "REMARKS_LOC", IsKey = false, SequenceName = "")]
	public String RemarksLoc
	{
		get{return remarksLoc;}
		set{remarksLoc = value;}
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public String EnteredBy
	{
		get{return enteredBy;}
		set{enteredBy = value;}
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public String EnteredDt
	{
		get{return enteredDt;}
		set{enteredDt = value;}
	}

	[Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
	public bool Approved
	{
		get{return approved;}
		set{approved = value;}
	}

	[Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
	public String ApprovedBy
	{
		get{return approvedBy;}
		set{approvedBy = value;}
	}

	[Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
	public String ApprovedDt
	{
		get{return approvedDt;}
		set{approvedDt = value;}
	}

	[Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
	public String ApprovedDtLoc
	{
		get{return approvedDtLoc;}
		set{approvedDtLoc = value;}
	}

    [Column(Name = "EMP_PHOTO_ID", IsKey = false, SequenceName = "")]
    public String ImageURL
    {
        get { return imageURL; }
        set { imageURL = value; }
    }

    //ip_address
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

	public ComEmployeeInfo()
	{}

}
