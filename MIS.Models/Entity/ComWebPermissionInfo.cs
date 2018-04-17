using System;
using EntityFramework;

[Table(Name = "COM_WEB_PERMISSION")]
public class ComWebPermissionInfo : EntityBase
{
	private String permCd  = null;
	private String permName  = null;
	private String permDesc  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String lastUpdatedBy  = null;
	private DateTime lastUpdatedDt  = DateTime.Now;
    private String ipaddress = null;

	[Column(Name = "PERM_CD", IsKey = true, SequenceName = "")]
	public String PermCd
	{
		get{return permCd;}
		set{permCd = value;}
	}

	[Column(Name = "PERM_NAME", IsKey = false, SequenceName = "")]
	public String PermName
	{
		get{return permName;}
		set{permName = value;}
	}

	[Column(Name = "PERM_DESC", IsKey = false, SequenceName = "")]
	public String PermDesc
	{
		get{return permDesc;}
		set{permDesc = value;}
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

    //ip_address
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

	public ComWebPermissionInfo()
	{}

}
