using System;
using EntityFramework;

[Table(Name = "COM_SESSION_INFO")]
public class ComSessionInfoInfo : EntityBase
{
	private String userId  = null;
	private DateTime loginTime  = DateTime.Now;
	private String macAddress  = null;
	private String userSessionId  = null;
	private String sessionIpAddress  = null;
	private String sessionOsUser  = null;
	private String sessionHost  = null;
	private String sessionTerminal  = null;
	private String remarks  = null;

	[Column(Name = "USER_ID", IsKey = false, SequenceName = "")]
	public String UserId
	{
		get{return userId;}
		set{userId = value;}
	}

	[Column(Name = "LOGIN_TIME", IsKey = false, SequenceName = "")]
	public DateTime LoginTime
	{
		get{return loginTime;}
		set{loginTime = value;}
	}

	[Column(Name = "MAC_ADDRESS", IsKey = false, SequenceName = "")]
	public String MacAddress
	{
		get{return macAddress;}
		set{macAddress = value;}
	}

	[Column(Name = "USER_SESSION_ID", IsKey = false, SequenceName = "")]
	public String UserSessionId
	{
		get{return userSessionId;}
		set{userSessionId = value;}
	}

	[Column(Name = "SESSION_IP_ADDRESS", IsKey = false, SequenceName = "")]
	public String SessionIpAddress
	{
		get{return sessionIpAddress;}
		set{sessionIpAddress = value;}
	}

	[Column(Name = "SESSION_OS_USER", IsKey = false, SequenceName = "")]
	public String SessionOsUser
	{
		get{return sessionOsUser;}
		set{sessionOsUser = value;}
	}

	[Column(Name = "SESSION_HOST", IsKey = false, SequenceName = "")]
	public String SessionHost
	{
		get{return sessionHost;}
		set{sessionHost = value;}
	}

	[Column(Name = "SESSION_TERMINAL", IsKey = false, SequenceName = "")]
	public String SessionTerminal
	{
		get{return sessionTerminal;}
		set{sessionTerminal = value;}
	}

	[Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
	public String Remarks
	{
		get{return remarks;}
		set{remarks = value;}
	}

	public ComSessionInfoInfo()
	{}

}
