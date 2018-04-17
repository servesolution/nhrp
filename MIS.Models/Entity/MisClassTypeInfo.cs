using System;
using EntityFramework;

[Table(Name = "MIS_CLASS_TYPE")]
public class MisClassTypeInfo : EntityBase
{
	private Decimal? classTypeCd  = 0;
	private String definedCd  = null;
	private String descEng  = null;
	private String descLoc  = null;
	private String shortName  = null;
	private String shortNameLoc  = null;
	private Decimal? orderNo  = null;
    private String disabled = null;
    private String approved = null;
	private String approvedBy  = null;
    private String approvedDt = DateTime.Now.ToString();
	private String approvedDtLoc  = null;
	private String enteredBy  = null;
    private String enteredDt = DateTime.Now.ToString();
	private String enteredDtLoc  = null;
    private String ipaddress = null;
	[Column(Name = "CLASS_TYPE_CD", IsKey = true, SequenceName = "")]
	public Decimal? ClassTypeCd
	{
		get{return classTypeCd;}
		set{classTypeCd = value;}
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

	[Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
	public Decimal? OrderNo
	{
		get{return orderNo;}
		set{orderNo = value;}
	}

	[Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
	public string Disabled
	{
		get{return disabled;}
		set{disabled = value;}
	}

	[Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
	public string Approved
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
    //ip_address
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

	public MisClassTypeInfo()
	{}

}
