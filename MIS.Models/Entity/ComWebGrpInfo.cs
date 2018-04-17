using System;
using EntityFramework;

[Table(Name = "COM_WEB_GRP")]
public class ComWebGrpInfo : EntityBase
{
	private String grpCd  = null;
	private String grpName  = null;
	private String grpDesc  = null;
	private String status  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String lastUpdatedBy  = null;
	private DateTime lastUpdatedDt  = DateTime.Now;
    private String ipaddress = null;

	[Column(Name = "GRP_CD", IsKey = true, SequenceName = "")]
	public String GrpCd
	{
		get{return grpCd;}
		set{grpCd = value;}
	}

	[Column(Name = "GRP_NAME", IsKey = false, SequenceName = "")]
	public String GrpName
	{
		get{return grpName;}
		set{grpName = value;}
	}

	[Column(Name = "GRP_DESC", IsKey = false, SequenceName = "")]
	public String GrpDesc
	{
		get{return grpDesc;}
		set{grpDesc = value;}
	}

	[Column(Name = "STATUS", IsKey = false, SequenceName = "")]
	public String Status
	{
		get{return status;}
		set{status = value;}
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

	[Column(Name = "LAST_UPDATED_BY", IsKey = false, SequenceName = "")]
	public String LastUpdatedBy
	{
		get{return lastUpdatedBy;}
		set{lastUpdatedBy = value;}
	}

	[Column(Name = "LAST_UPDATED_DT", IsKey = false, SequenceName = "")]
	public DateTime LastUpdatedDt
	{
		get{return lastUpdatedDt;}
		set{lastUpdatedDt = value;}
	}
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public string Ipaddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

	public ComWebGrpInfo()
	{}

}
