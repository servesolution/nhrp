using System;
using EntityFramework;

[Table(Name = "COM_USR_LOGIN_LOG")]
public class ComUsrLoginLogInfo : EntityBase
{
	private String usrCd  = null;
	private Decimal loginSno  = 0;
	private String employeeCd  = "";
	private String officeCd  = "";
	private String loginDt  = DateTime.Now.ToString();
	private String loginDtLoc  = null;
	private Decimal loginHh  = 0;
	private Decimal loginMi  = 0;
	private Decimal loginSs  = 0;
	private String logoutDt  = "";
	private String logoutDtLoc  = null;
	private Decimal? logoutHh  = null;
	private Decimal? logoutMi  = null;
	private Decimal? logoutSs  = null;
	private String loginRemarks  = null;
	private String logoutRemarks  = null;
	private String moduleCd  = null;
	private String menuCd  = null;
	private String menuUrl  = null;
	private String ipAddress  = null;

	[Column(Name = "USR_CD", IsKey = true, SequenceName = "")]
	public String UsrCd
	{
		get{return usrCd;}
		set{usrCd = value;}
	}

	[Column(Name = "LOGIN_SNO", IsKey = true, SequenceName = "")]
	public Decimal LoginSno
	{
		get{return loginSno;}
		set{loginSno = value;}
	}

	[Column(Name = "EMPLOYEE_CD", IsKey = false, SequenceName = "")]
	public String EmployeeCd
	{
		get{return employeeCd;}
		set{employeeCd = value;}
	}

	[Column(Name = "OFFICE_CD", IsKey = false, SequenceName = "")]
	public String OfficeCd
	{
		get{return officeCd;}
		set{officeCd = value;}
	}

	[Column(Name = "LOGIN_DT", IsKey = false, SequenceName = "")]
	public String LoginDt
	{
		get{return loginDt;}
		set{loginDt = value;}
	}

	[Column(Name = "LOGIN_DT_LOC", IsKey = false, SequenceName = "")]
	public String LoginDtLoc
	{
		get{return loginDtLoc;}
		set{loginDtLoc = value;}
	}

	[Column(Name = "LOGIN_HH", IsKey = false, SequenceName = "")]
	public Decimal LoginHh
	{
		get{return loginHh;}
		set{loginHh = value;}
	}

	[Column(Name = "LOGIN_MI", IsKey = false, SequenceName = "")]
	public Decimal LoginMi
	{
		get{return loginMi;}
		set{loginMi = value;}
	}

	[Column(Name = "LOGIN_SS", IsKey = false, SequenceName = "")]
	public Decimal LoginSs
	{
		get{return loginSs;}
		set{loginSs = value;}
	}

	[Column(Name = "LOGOUT_DT", IsKey = false, SequenceName = "")]
	public String LogoutDt
	{
		get{return logoutDt;}
		set{logoutDt = value;}
	}

	[Column(Name = "LOGOUT_DT_LOC", IsKey = false, SequenceName = "")]
	public String LogoutDtLoc
	{
		get{return logoutDtLoc;}
		set{logoutDtLoc = value;}
	}

	[Column(Name = "LOGOUT_HH", IsKey = false, SequenceName = "")]
	public Decimal? LogoutHh
	{
		get{return logoutHh;}
		set{logoutHh = value;}
	}

	[Column(Name = "LOGOUT_MI", IsKey = false, SequenceName = "")]
	public Decimal? LogoutMi
	{
		get{return logoutMi;}
		set{logoutMi = value;}
	}

	[Column(Name = "LOGOUT_SS", IsKey = false, SequenceName = "")]
	public Decimal? LogoutSs
	{
		get{return logoutSs;}
		set{logoutSs = value;}
	}

	[Column(Name = "LOGIN_REMARKS", IsKey = false, SequenceName = "")]
	public String LoginRemarks
	{
		get{return loginRemarks;}
		set{loginRemarks = value;}
	}

	[Column(Name = "LOGOUT_REMARKS", IsKey = false, SequenceName = "")]
	public String LogoutRemarks
	{
		get{return logoutRemarks;}
		set{logoutRemarks = value;}
	}

	[Column(Name = "MODULE_CD", IsKey = false, SequenceName = "")]
	public String ModuleCd
	{
		get{return moduleCd;}
		set{moduleCd = value;}
	}

	[Column(Name = "MENU_CD", IsKey = false, SequenceName = "")]
	public String MenuCd
	{
		get{return menuCd;}
		set{menuCd = value;}
	}

	[Column(Name = "MENU_URL", IsKey = false, SequenceName = "")]
	public String MenuUrl
	{
		get{return menuUrl;}
		set{menuUrl = value;}
	}

	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
	public String IpAddress
	{
		get{return ipAddress;}
		set{ipAddress = value;}
	}

	public ComUsrLoginLogInfo()
	{}

}
