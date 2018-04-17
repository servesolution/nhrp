using System;
using EntityFramework;

[Table(Name = "CASE_GRIEVANCE_STATUS")]
public class CaseGrievanceStatusInfo : EntityBase
{
	private Decimal grievanceStatusCd  = 0;
	private Decimal? definedCd  = null;
	private String descEng  = null;
	private String descLoc  = null;
	private String shortName  = null;
	private String shortNameLoc  = null;
	private Decimal? orderNo  = null;
	private String disabled  = null;
	private String approved  = null;
	private String approvedBy  = null;
	private DateTime? approvedDt  = null;
	private String approvedDtLoc  = null;
	private String enteredBy  = null;
	private DateTime? enteredDt  = null;
	private String enteredDtLoc  = null;
	private String updatedBy  = null;
	private DateTime? updatedDt  = null;
	private String updatedDtLoc  = null;
    private string ipaddress = null;

	[Column(Name = "GRIEVANCE_STATUS_CD", IsKey = true, SequenceName = "")]
	public Decimal GrievanceStatusCd
	{
		get{return grievanceStatusCd;}
		set{grievanceStatusCd = value;}
	}

	[Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
	public Decimal? DefinedCd
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

	[Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
	public Decimal? OrderNo
	{
		get{return orderNo;}
		set{orderNo = value;}
	}

	[Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
	public String Disabled
	{
		get{return disabled;}
		set{disabled = value;}
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

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String Ipaddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

	public CaseGrievanceStatusInfo()
	{}

}
