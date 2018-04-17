using System;
using EntityFramework;

[Table(Name = "CASE_GRIEVANCE_NATURE")]
public class CaseGrievanceNatureInfo : EntityBase
{
	private Decimal? grievanceNatureCd  = 0;
	private String definedCd  = null;
	private String descEng  = null;
	private String descLoc  = null;
	private String shortName  = null;
	private String shortNameLoc  = null;
	private Decimal priorityLevel  = 0;
	private Decimal? orderNo  = null;
	private bool disabled  = false;
	private bool approved  = false;
	private String approvedBy  = null;
	private String approvedDt  = null;
	private String approvedDtLoc  = null;
	private String enteredBy  = null;
	private String enteredDt  = null;
	private String enteredDtLoc  = null;
	private String updatedBy  = null;
	private String updatedDt  = null;
	private String updatedDtLoc  = null;
    private string ipaddress = null;

	[Column(Name = "GRIEVANCE_NATURE_CD", IsKey = true, SequenceName = "")]
	public Decimal? GrievanceNatureCd
	{
		get{return grievanceNatureCd;}
		set{grievanceNatureCd = value;}
	}

	[Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
	public String DefinedCd
	{
		get{return definedCd;}
		set{definedCd = value;}
	}

	[Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
	public String DescEng
	{
		get{return descEng;}
		set{descEng = value;}
	}

	[Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
	public String DescLoc
	{
		get{return descLoc;}
		set{descLoc = value;}
	}

	[Column(Name = "SHORT_NAME", IsKey = false, SequenceName = "")]
	public String ShortName
	{
		get{return shortName;}
		set{shortName = value;}
	}

	[Column(Name = "SHORT_NAME_LOC", IsKey = false, SequenceName = "")]
	public String ShortNameLoc
	{
		get{return shortNameLoc;}
		set{shortNameLoc = value;}
	}

	[Column(Name = "PRIORITY_LEVEL", IsKey = false, SequenceName = "")]
	public Decimal PriorityLevel
	{
		get{return priorityLevel;}
		set{priorityLevel = value;}
	}

	[Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
	public Decimal? OrderNo
	{
		get{return orderNo;}
		set{orderNo = value;}
	}

	[Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
	public bool Disabled
	{
		get{return disabled;}
		set{disabled = value;}
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

	[Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
	public String EnteredDtLoc
	{
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;}
	}

	[Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
	public String UpdatedBy
	{
		get{return updatedBy;}
		set{updatedBy = value;}
	}

	[Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
	public String UpdatedDt
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

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String Ipaddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }


	public CaseGrievanceNatureInfo()
	{}

}
