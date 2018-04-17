using System;
using EntityFramework;

[Table(Name = "CASE_GRIEVANCE_REG_DTL")]
public class CaseGrievanceRegDtlInfo : EntityBase
{
	private Decimal grievanceFormDtlCd  = 0;
	private Decimal? serialNo  = null;
	private Decimal? grievanceFormCd  = null;
	private String usrCd  = null;
	private Decimal? grievanceNatureCd  = null;
	private Decimal? grievanceStatusCd  = null;
	private Decimal? escLevelCd  = null;
	private String commentsEng  = null;
	private String commentsLoc  = null;
	private String verifiedByFnameEng  = null;
	private String verifiedByMnameEng  = null;
	private String verifiedByLnameEng  = null;
	private String verifiedByFullnameEng  = null;
	private String verifiedByFnameLoc  = null;
	private String verifiedByMnameLoc  = null;
	private String verifiedByLnameLoc  = null;
	private String verifiedByFullnameLoc  = null;
	private String verifiedFunctionEng  = null;
	private String verifiedFunctionLoc  = null;
	private String verifiedInstitutionEng  = null;
	private String verifiedInstitutionLoc  = null;
	private Decimal verifiedYearEng  = 0;
	private Decimal verifiedMonthEng  = 0;
	private Decimal verifiedDayEng  = 0;
	private DateTime verifiedDt  = DateTime.Now;
	private String verifiedYearLoc  = null;
	private String verifiedMonthLoc  = null;
	private String verifiedDayLoc  = null;
	private String verifiedDtLoc  = null;
	private bool approved  = false;
	private String approvedBy  = null;
	private DateTime? approvedDt  = null;
	private String approvedDtLoc  = null;
	private String updatedBy  = null;
	private DateTime? updatedDt  = null;
	private String updatedDtLoc  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String enteredDtLoc  = null;
    private string ipaddress = null;

	[Column(Name = "GRIEVANCE_FORM_DTL_CD", IsKey = true, SequenceName = "")]
	public Decimal GrievanceFormDtlCd
	{
		get{return grievanceFormDtlCd;}
		set{grievanceFormDtlCd = value;}
	}

	[Column(Name = "SERIAL_NO", IsKey = false, SequenceName = "")]
	public Decimal? SerialNo
	{
		get{return serialNo;}
		set{serialNo = value;}
	}

	[Column(Name = "GRIEVANCE_FORM_CD", IsKey = false, SequenceName = "")]
	public Decimal? GrievanceFormCd
	{
		get{return grievanceFormCd;}
		set{grievanceFormCd = value;}
	}

	[Column(Name = "USR_CD", IsKey = false, SequenceName = "")]
	public String UsrCd
	{
		get{return usrCd;}
		set{usrCd = value;}
	}

	[Column(Name = "GRIEVANCE_NATURE_CD", IsKey = false, SequenceName = "")]
	public Decimal? GrievanceNatureCd
	{
		get{return grievanceNatureCd;}
		set{grievanceNatureCd = value;}
	}

	[Column(Name = "GRIEVANCE_STATUS_CD", IsKey = false, SequenceName = "")]
	public Decimal? GrievanceStatusCd
	{
		get{return grievanceStatusCd;}
		set{grievanceStatusCd = value;}
	}

	[Column(Name = "ESC_LEVEL_CD", IsKey = false, SequenceName = "")]
	public Decimal? EscLevelCd
	{
		get{return escLevelCd;}
		set{escLevelCd = value;}
	}

	[Column(Name = "COMMENTS_ENG", IsKey = false, SequenceName = "")]
	public String CommentsEng
	{
		get{return commentsEng;}
		set{commentsEng = value;}
	}

	[Column(Name = "COMMENTS_LOC", IsKey = false, SequenceName = "")]
	public String CommentsLoc
	{
		get{return commentsLoc;}
		set{commentsLoc = value;}
	}

	[Column(Name = "VERIFIED_BY_FNAME_ENG", IsKey = false, SequenceName = "")]
	public String VerifiedByFnameEng
	{
		get{return verifiedByFnameEng;}
		set{verifiedByFnameEng = value;}
	}

	[Column(Name = "VERIFIED_BY_MNAME_ENG", IsKey = false, SequenceName = "")]
	public String VerifiedByMnameEng
	{
		get{return verifiedByMnameEng;}
		set{verifiedByMnameEng = value;}
	}

	[Column(Name = "VERIFIED_BY_LNAME_ENG", IsKey = false, SequenceName = "")]
	public String VerifiedByLnameEng
	{
		get{return verifiedByLnameEng;}
		set{verifiedByLnameEng = value;}
	}

	[Column(Name = "VERIFIED_BY_FULLNAME_ENG", IsKey = false, SequenceName = "")]
	public String VerifiedByFullnameEng
	{
		get{return verifiedByFullnameEng;}
		set{verifiedByFullnameEng = value;}
	}

	[Column(Name = "VERIFIED_BY_FNAME_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedByFnameLoc
	{
		get{return verifiedByFnameLoc;}
		set{verifiedByFnameLoc = value;}
	}

	[Column(Name = "VERIFIED_BY_MNAME_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedByMnameLoc
	{
		get{return verifiedByMnameLoc;}
		set{verifiedByMnameLoc = value;}
	}

	[Column(Name = "VERIFIED_BY_LNAME_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedByLnameLoc
	{
		get{return verifiedByLnameLoc;}
		set{verifiedByLnameLoc = value;}
	}

	[Column(Name = "VERIFIED_BY_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedByFullnameLoc
	{
		get{return verifiedByFullnameLoc;}
		set{verifiedByFullnameLoc = value;}
	}

	[Column(Name = "VERIFIED_FUNCTION_ENG", IsKey = false, SequenceName = "")]
	public String VerifiedFunctionEng
	{
		get{return verifiedFunctionEng;}
		set{verifiedFunctionEng = value;}
	}

	[Column(Name = "VERIFIED_FUNCTION_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedFunctionLoc
	{
		get{return verifiedFunctionLoc;}
		set{verifiedFunctionLoc = value;}
	}

	[Column(Name = "VERIFIED_INSTITUTION_ENG", IsKey = false, SequenceName = "")]
	public String VerifiedInstitutionEng
	{
		get{return verifiedInstitutionEng;}
		set{verifiedInstitutionEng = value;}
	}

	[Column(Name = "VERIFIED_INSTITUTION_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedInstitutionLoc
	{
		get{return verifiedInstitutionLoc;}
		set{verifiedInstitutionLoc = value;}
	}

	[Column(Name = "VERIFIED_YEAR_ENG", IsKey = false, SequenceName = "")]
	public Decimal VerifiedYearEng
	{
		get{return verifiedYearEng;}
		set{verifiedYearEng = value;}
	}

	[Column(Name = "VERIFIED_MONTH_ENG", IsKey = false, SequenceName = "")]
	public Decimal VerifiedMonthEng
	{
		get{return verifiedMonthEng;}
		set{verifiedMonthEng = value;}
	}

	[Column(Name = "VERIFIED_DAY_ENG", IsKey = false, SequenceName = "")]
	public Decimal VerifiedDayEng
	{
		get{return verifiedDayEng;}
		set{verifiedDayEng = value;}
	}

	[Column(Name = "VERIFIED_DT", IsKey = false, SequenceName = "")]
	public DateTime VerifiedDt
	{
		get{return verifiedDt;}
		set{verifiedDt = value;}
	}

	[Column(Name = "VERIFIED_YEAR_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedYearLoc
	{
		get{return verifiedYearLoc;}
		set{verifiedYearLoc = value;}
	}

	[Column(Name = "VERIFIED_MONTH_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedMonthLoc
	{
		get{return verifiedMonthLoc;}
		set{verifiedMonthLoc = value;}
	}

	[Column(Name = "VERIFIED_DAY_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedDayLoc
	{
		get{return verifiedDayLoc;}
		set{verifiedDayLoc = value;}
	}

	[Column(Name = "VERIFIED_DT_LOC", IsKey = false, SequenceName = "")]
	public String VerifiedDtLoc
	{
		get{return verifiedDtLoc;}
		set{verifiedDtLoc = value;}
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
	public DateTime? ApprovedDt
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

	[Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
	public String UpdatedBy
	{
		get{return updatedBy;}
		set{updatedBy = value;}
	}

	[Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
	public DateTime? UpdatedDt
	{
		get{return updatedDt;}
		set{updatedDt = value;}
	}

	[Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
	public String UpdatedDtLoc
	{
		get{return updatedDtLoc;}
		set{updatedDtLoc = value;}
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public String EnteredBy
	{
		get{return enteredBy;}
		set{enteredBy = value;}
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public DateTime EnteredDt
	{
		get{return enteredDt;}
		set{enteredDt = value;}
	}

	[Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
	public String EnteredDtLoc
	{
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;}
	}

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String Ipaddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }


	public CaseGrievanceRegDtlInfo()
	{}

}
