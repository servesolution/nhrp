using System;
using EntityFramework;

[Table(Name = "COM_SESSION_USER")]
public class ComSessionUserInfo : EntityBase
{
	private String userId  = null;
	private DateTime logonTime  = DateTime.Now;

	[Column(Name = "USER_ID", IsKey = false, SequenceName = "")]
	public String UserId
	{
		get{return userId;}
		set{userId = value;}
	}

	[Column(Name = "LOGON_TIME", IsKey = false, SequenceName = "")]
	public DateTime LogonTime
	{
		get{return logonTime;}
		set{logonTime = value;}
	}

	public ComSessionUserInfo()
	{}

}
