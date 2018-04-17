using System;
using EntityFramework;

[Table(Name = "CASE_GRIEVANCE_REG_CHILDREN")]
public class CaseGrievanceRegChildrenInfo : EntityBase
{
	private Decimal grievanceChildCd  = 0;
	private String grievanceChildDefCd  = null;
	private Decimal? grievanceFormCd  = null;
	private Decimal? memberId  = null;
	private String firstNameEng  = null;
	private String middleNameEng  = null;
	private String lastNameEng  = null;
	private String fullNameEng  = null;
	private String firstNameLoc  = null;
	private String middleNameLoc  = null;
	private String lastNameLoc  = null;
	private String fullNameLoc  = null;
	private Decimal? age  = null;
	private Decimal? classTypeCd  = null;
	private String schoolNameEng  = null;
	private String schoolNameLoc  = null;
	private String approved  = null;
	private String approvedBy  = null;
	private DateTime? approvedDt  = null;
	private String approvedDtLoc  = null;
	private String updatedBy  = null;
	private DateTime? updatedDt  = null;
	private String updatedDtLoc  = null;
	private String enteredBy  = null;
	private DateTime? enteredDt  = null;
	private String enteredDtLoc  = null;
    private string ipaddress = null;

	[Column(Name = "GRIEVANCE_CHILD_CD", IsKey = true, SequenceName = "")]
	public Decimal GrievanceChildCd
	{
		get{return grievanceChildCd;}
		set{grievanceChildCd = value;}
	}

	[Column(Name = "GRIEVANCE_CHILD_DEF_CD", IsKey = false, SequenceName = "")]
	public String GrievanceChildDefCd
	{
		get{return grievanceChildDefCd;}
		set{grievanceChildDefCd = value;}
	}

	[Column(Name = "GRIEVANCE_FORM_CD", IsKey = false, SequenceName = "")]
	public Decimal? GrievanceFormCd
	{
		get{return grievanceFormCd;}
		set{grievanceFormCd = value;}
	}

	[Column(Name = "MEMBER_ID", IsKey = false, SequenceName = "")]
	public Decimal? MemberId
	{
		get{return memberId;}
		set{memberId = value;}
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

	[Column(Name = "AGE", IsKey = false, SequenceName = "")]
	public Decimal? Age
	{
		get{return age;}
		set{age = value;}
	}

	[Column(Name = "CLASS_TYPE_CD", IsKey = false, SequenceName = "")]
	public Decimal? ClassTypeCd
	{
		get{return classTypeCd;}
		set{classTypeCd = value;}
	}

	[Column(Name = "SCHOOL_NAME_ENG", IsKey = false, SequenceName = "")]
	public String SchoolNameEng
	{
		get{return schoolNameEng;}
		set{schoolNameEng = value;}
	}

	[Column(Name = "SCHOOL_NAME_LOC", IsKey = false, SequenceName = "")]
	public String SchoolNameLoc
	{
		get{return schoolNameLoc;}
		set{schoolNameLoc = value;}
	}

	[Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
	public String Approved
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
	public DateTime? EnteredDt
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

	

	public CaseGrievanceRegChildrenInfo()
	{}

}
