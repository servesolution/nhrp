using System;
using EntityFramework;

[Table(Name = "COM_WEB_USR_GRP")]
public class ComWebUsrGrpInfo : EntityBase
{
	private String usrCd  = null;
	private String grpCd  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String lastUpdatedBy  = null;
	private DateTime lastUpdatedDt  = DateTime.Now;
    private String ipaddress = null;

	[Column(Name = "USR_CD", IsKey = true, SequenceName = "")]
	public String UsrCd
	{
		get{return usrCd;}
		set{usrCd = value;}
	}

	[Column(Name = "GRP_CD", IsKey = true, SequenceName = "")]
	public String GrpCd
	{
		get{return grpCd;}
		set{grpCd = value;}
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
	public ComWebUsrGrpInfo()
	{}

}
