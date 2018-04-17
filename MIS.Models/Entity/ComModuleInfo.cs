using System;
using EntityFramework;

[Table(Name = "COM_MODULE")]
public class ComModuleInfo : EntityBase
{
	private String moduleCd  = null;
	private String moduleName  = null;
	private String moduleDesc  = null;
	private String status  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String lastUpdatedBy  = null;
	private DateTime lastUpdatedDt  = DateTime.Now;
    private String ipaddress = null;
	[Column(Name = "MODULE_CD", IsKey = true, SequenceName = "")]
	public String ModuleCd
	{
		get{return moduleCd;}
		set{moduleCd = value;}
	}

	[Column(Name = "MODULE_NAME", IsKey = false, SequenceName = "")]
	public String ModuleName
	{
		get{return moduleName;}
		set{moduleName = value;}
	}

	[Column(Name = "MODULE_DESC", IsKey = false, SequenceName = "")]
	public String ModuleDesc
	{
		get{return moduleDesc;}
		set{moduleDesc = value;}
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

    //ip_address
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }
	public ComModuleInfo()
	{}

}
