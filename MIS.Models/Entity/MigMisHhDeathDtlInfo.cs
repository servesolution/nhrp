using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HH_DEATH_DTL")]
public class MigMisHhDeathDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String householdId = null;

	private System.String buildingStructureNo = null;

	private System.Decimal? sno = null;

	private System.String memberId = null;

	private System.String firstNameEng = null;

	private System.String middleNameEng = null;

	private System.String lastNameEng = null;

	private System.String fullNameEng = null;

	private System.String firstNameLoc = null;

	private System.String middleNameLoc = null;

	private System.String lastNameLoc = null;

	private System.String fullNameLoc = null;

	private System.Decimal? genderCd = null;

	private System.Decimal? age = null;

	private System.String deathCertificate = null;

	private System.String deathCertificateNo = null;

	private System.Decimal? deathYear = null;

	private System.Decimal? deathMonth = null;

	private System.Decimal? deathDay = null;

	private System.Decimal? deathYearLoc = null;

	private System.Decimal? deathMonthLoc = null;

	private System.Decimal? deathDayLoc = null;

	private System.DateTime? deathDt = null;

	private System.String deathDtLoc = null;

	private System.String deathDoc = null;

	private System.String remarks = null;

	private System.String remarksLoc = null;

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

	private System.Decimal? deathReasonCd = null;

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

    private System.String householdHead = null;

    private System.Decimal? birthYear = null;

    private System.Decimal? birthMonth = null;

    private System.Decimal? birthDay = null;

    private System.String birthYearLoc = null;

    private System.String birthMonthLoc = null;

    private System.String birthDayLoc = null;

    private System.DateTime? birthDt = null;

    private System.String birthDtLoc = null;

    private System.Decimal? casteCd = null;

    private System.Decimal? religionCd = null;

    private System.String literate = null;

    private System.Decimal? educationCd = null;

    private System.Decimal? perDistrictCd = null;

    private System.Decimal? perVdcMunCd = null;

    private System.Decimal? perWardNo = null;

    private System.String perAreaEng = null;

    private System.String perAreaLoc = null;

    private System.Decimal? curDistrictCd = null;

    private System.Decimal? curVdcMunCd = null;

    private System.Decimal? curWardNo = null;

    private System.String curAreaEng = null;

    private System.String curAreaLoc = null;

    private System.Decimal? indentificationTypeCd = null;

    private System.String indentificationOthers = null;

    private System.String indentificationOthersLoc = null;

    private System.String foreignHouseheadCountryEng = null;

    private System.String foreignHouseheadCountryLoc = null;

	private System.String ipAddress = null;

	[Column(Name = "HOUSE_OWNER_ID", IsKey = false, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

	[Column(Name = "HOUSEHOLD_ID", IsKey = true, SequenceName = "")]
	public System.String HouseholdId 
	{ 
		get{return householdId;}
		set{householdId = value;} 
	}

	[Column(Name = "BUILDING_STRUCTURE_NO", IsKey = false, SequenceName = "")]
	public System.String BuildingStructureNo 
	{ 
		get{return buildingStructureNo;}
		set{buildingStructureNo = value;} 
	}

	[Column(Name = "SNO", IsKey = true, SequenceName = "")]
	public System.Decimal? Sno 
	{ 
		get{return sno;}
		set{sno = value;} 
	}

	[Column(Name = "MEMBER_ID", IsKey = false, SequenceName = "")]
	public System.String MemberId 
	{ 
		get{return memberId;}
		set{memberId = value;} 
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

	[Column(Name = "GENDER_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? GenderCd 
	{ 
		get{return genderCd;}
		set{genderCd = value;} 
	}

	[Column(Name = "AGE", IsKey = false, SequenceName = "")]
	public System.Decimal? Age 
	{ 
		get{return age;}
		set{age = value;} 
	}

	[Column(Name = "DEATH_CERTIFICATE", IsKey = false, SequenceName = "")]
	public System.String DeathCertificate 
	{ 
		get{return deathCertificate;}
		set{deathCertificate = value;} 
	}

	[Column(Name = "DEATH_CERTIFICATE_NO", IsKey = false, SequenceName = "")]
	public System.String DeathCertificateNo 
	{ 
		get{return deathCertificateNo;}
		set{deathCertificateNo = value;} 
	}

	[Column(Name = "DEATH_YEAR", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathYear 
	{ 
		get{return deathYear;}
		set{deathYear = value;} 
	}

	[Column(Name = "DEATH_MONTH", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathMonth 
	{ 
		get{return deathMonth;}
		set{deathMonth = value;} 
	}

	[Column(Name = "DEATH_DAY", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathDay 
	{ 
		get{return deathDay;}
		set{deathDay = value;} 
	}

	[Column(Name = "DEATH_YEAR_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathYearLoc 
	{ 
		get{return deathYearLoc;}
		set{deathYearLoc = value;} 
	}

	[Column(Name = "DEATH_MONTH_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathMonthLoc 
	{ 
		get{return deathMonthLoc;}
		set{deathMonthLoc = value;} 
	}

	[Column(Name = "DEATH_DAY_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathDayLoc 
	{ 
		get{return deathDayLoc;}
		set{deathDayLoc = value;} 
	}

	[Column(Name = "DEATH_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? DeathDt 
	{ 
		get{return deathDt;}
		set{deathDt = value;} 
	}

	[Column(Name = "DEATH_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String DeathDtLoc 
	{ 
		get{return deathDtLoc;}
		set{deathDtLoc = value;} 
	}

	[Column(Name = "DEATH_DOC", IsKey = false, SequenceName = "")]
	public System.String DeathDoc 
	{ 
		get{return deathDoc;}
		set{deathDoc = value;} 
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

	[Column(Name = "DEATH_REASON_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DeathReasonCd 
	{ 
		get{return deathReasonCd;}
		set{deathReasonCd = value;} 
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

    [Column(Name = "HOUSEHOLD_HEAD", IsKey = false, SequenceName = "")]
    public System.String HouseholdHead
    {
        get { return householdHead; }
        set { householdHead = value; }
    }

    [Column(Name = "BIRTH_YEAR", IsKey = false, SequenceName = "")]
    public System.Decimal? BirthYear
    {
        get { return birthYear; }
        set { birthYear = value; }
    }

    [Column(Name = "BIRTH_MONTH", IsKey = false, SequenceName = "")]
    public System.Decimal? BirthMonth
    {
        get { return birthMonth; }
        set { birthMonth = value; }
    }

    [Column(Name = "BIRTH_DAY", IsKey = false, SequenceName = "")]
    public System.Decimal? BirthDay
    {
        get { return birthDay; }
        set { birthDay = value; }
    }

    [Column(Name = "BIRTH_YEAR_LOC", IsKey = false, SequenceName = "")]
    public System.String BirthYearLoc
    {
        get { return birthYearLoc; }
        set { birthYearLoc = value; }
    }

    [Column(Name = "BIRTH_MONTH_LOC", IsKey = false, SequenceName = "")]
    public System.String BirthMonthLoc
    {
        get { return birthMonthLoc; }
        set { birthMonthLoc = value; }
    }

    [Column(Name = "BIRTH_DAY_LOC", IsKey = false, SequenceName = "")]
    public System.String BirthDayLoc
    {
        get { return birthDayLoc; }
        set { birthDayLoc = value; }
    }

    [Column(Name = "BIRTH_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? BirthDt
    {
        get { return birthDt; }
        set { birthDt = value; }
    }

    [Column(Name = "BIRTH_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String BirthDtLoc
    {
        get { return birthDtLoc; }
        set { birthDtLoc = value; }
    }

    [Column(Name = "CASTE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CasteCd
    {
        get { return casteCd; }
        set { casteCd = value; }
    }

    [Column(Name = "RELIGION_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? ReligionCd
    {
        get { return religionCd; }
        set { religionCd = value; }
    }

    [Column(Name = "LITERATE", IsKey = false, SequenceName = "")]
    public System.String Literate
    {
        get { return literate; }
        set { literate = value; }
    }

    [Column(Name = "EDUCATION_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? EducationCd
    {
        get { return educationCd; }
        set { educationCd = value; }
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

    [Column(Name = "IDENTIFICATION_TYPE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? IndentificationTypeCd
    {
        get { return indentificationTypeCd; }
        set { indentificationTypeCd = value; }
    }

    [Column(Name = "IDENTIFICATION_OTHERS", IsKey = false, SequenceName = "")]
    public System.String IndentificationOthers
    {
        get { return indentificationOthers; }
        set { indentificationOthers = value; }
    }

    [Column(Name = "IDENTIFICATION_OTHERS_LOC", IsKey = false, SequenceName = "")]
    public System.String IndentificationOthersLoc
    {
        get { return indentificationOthersLoc; }
        set { indentificationOthersLoc = value; }
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

	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigMisHhDeathDtlInfo()
	{}
}