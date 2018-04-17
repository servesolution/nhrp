using System;
using EntityFramework;

[Table(Name = "COM_MENU_SECURITY")]
public class ComMenuSecurityInfo : EntityBase
{
	private String menuCd  = null;
	private String grpCd  = null;
	private String permCd  = null;
	private String enteredBy  = null;
    private String ipaddress = null;
	private DateTime enteredDt  = DateTime.Now;
	private String lastUpdatedBy  = null;
	private DateTime lastUpdatedDt  = DateTime.Now;

	[Column(Name = "MENU_CD", IsKey = true, SequenceName = "")]
	public String MenuCd
	{
		get{return menuCd;}
		set{menuCd = value;}
	}

	[Column(Name = "GRP_CD", IsKey = true, SequenceName = "")]
	public String GrpCd
	{
		get{return grpCd;}
		set{grpCd = value;}
	}

	[Column(Name = "PERM_CD", IsKey = true, SequenceName = "")]
	public String PermCd
	{
		get{return permCd;}
		set{permCd = value;}
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

	public ComMenuSecurityInfo()
	{}

}
