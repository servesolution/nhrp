using System;
using EntityFramework;

[Table(Name = "MIS_DESIGNATION")]
public class MisDesignationInfo : EntityBase
{
	private String designationCd  = null;
	private String definedCd  = null;
	private String descEng  = null;
	private String descLoc  = null;
	private String shortName  = null;
	private String shortNameLoc  = null;
	private Decimal orderNo  = 0;
	private String disabled  = null;
	private String approved  = null;
	private String approvedBy  = null;
	private DateTime approvedDt  = DateTime.Now;
	private String approvedDtLoc  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String enteredDtLoc  = null;
    private String ipAddress = null;

	[Column(Name = "DESIGNATION_CD", IsKey = true, SequenceName = "")]
	public String DesignationCd
	{
		get{return designationCd;}
		set{designationCd = value;}
	}

	[Column(Name = "DEFINED_CD", IsKey = true, SequenceName = "")]
	public String DefinedCd
	{
		get{return definedCd;}
		set{definedCd = value;}
	}

	[Column(Name = "DESC_ENG", IsKey = true, SequenceName = "")]
	public String DescEng
	{
		get{return descEng;}
		set{descEng = value;}
	}

	[Column(Name = "DESC_LOC", IsKey = true, SequenceName = "")]
	public String DescLoc
	{
		get{return descLoc;}
		set{descLoc = value;}
	}

	[Column(Name = "SHORT_NAME", IsKey = true, SequenceName = "")]
	public String ShortName
	{
		get{return shortName;}
		set{shortName = value;}
	}

	[Column(Name = "SHORT_NAME_LOC", IsKey = true, SequenceName = "")]
	public String ShortNameLoc
	{
		get{return shortNameLoc;}
		set{shortNameLoc = value;}
	}

	[Column(Name = "ORDER_NO", IsKey = true, SequenceName = "")]
	public Decimal OrderNo
	{
		get{return orderNo;}
		set{orderNo = value;}
	}

	[Column(Name = "DISABLED", IsKey = true, SequenceName = "")]
    public String Disabled
	{
		get{return disabled;}
		set{disabled = value;}
	}

	[Column(Name = "APPROVED", IsKey = true, SequenceName = "")]
	public String Approved
	{
		get{return approved;}
		set{approved = value;}
	}

	[Column(Name = "APPROVED_BY", IsKey = true, SequenceName = "")]
	public String ApprovedBy
	{
		get{return approvedBy;}
		set{approvedBy = value;}
	}

	[Column(Name = "APPROVED_DT", IsKey = true, SequenceName = "")]
	public DateTime ApprovedDt
	{
		get{return approvedDt;}
		set{approvedDt = value;}
	}

	[Column(Name = "APPROVED_DT_LOC", IsKey = true, SequenceName = "")]
	public String ApprovedDtLoc
	{
		get{return approvedDtLoc;}
		set{approvedDtLoc = value;}
	}

	[Column(Name = "ENTERED_BY", IsKey = true, SequenceName = "")]
	public String EnteredBy
	{
		get{return enteredBy;}
		set{enteredBy = value;}
	}

	[Column(Name = "ENTERED_DT", IsKey = true, SequenceName = "")]
	public DateTime EnteredDt
	{
		get{return enteredDt;}
		set{enteredDt = value;}
	}

	[Column(Name = "ENTERED_DT_LOC", IsKey = true, SequenceName = "")]
	public String EnteredDtLoc
	{
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;}
	}

    [Column(Name = "ip_address", IsKey = true, SequenceName = "")]
    public String IpAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MisDesignationInfo()
	{}

}
