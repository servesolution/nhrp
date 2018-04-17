using System;
using EntityFramework;

[Table(Name = "COM_MENU")]
public class ComMenuInfo : EntityBase
{
	private String menuCd  = null;
	private String upperMenuCd  = null;
	private String label  = null;
	private String labelLoc  = null;
	private String linkUrl  = null;
	private String iconUrl  = null;
    private String ipAddress = null;
    private String enteredBY = null;
	private bool disabled  = false;
	private Decimal? menuOrder  = null;
	private String callBackFunc  = null;
	private String menuType  = null;

	[Column(Name = "MENU_CD", IsKey = true, SequenceName = "")]
	public String MenuCd
	{
		get{return menuCd;}
		set{menuCd = value;}
	}

	[Column(Name = "UPPER_MENU_CD", IsKey = false, SequenceName = "")]
	public String UpperMenuCd
	{
		get{return upperMenuCd;}
		set{upperMenuCd = value;}
	}

	[Column(Name = "LABEL", IsKey = false, SequenceName = "")]
	public String Label
	{
		get{return label;}
		set{label = value;}
	}

	[Column(Name = "LABEL_LOC", IsKey = false, SequenceName = "")]
	public String LabelLoc
	{
		get{return labelLoc;}
		set{labelLoc = value;}
	}

	[Column(Name = "LINK_URL", IsKey = false, SequenceName = "")]
	public String LinkUrl
	{
		get{return linkUrl;}
		set{linkUrl = value;}
	}

	[Column(Name = "ICON_URL", IsKey = false, SequenceName = "")]
	public String IconUrl
	{
		get{return iconUrl;}
		set{iconUrl = value;}
	}

	[Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
	public bool Disabled
	{
		get{return disabled;}
		set{disabled = value;}
	}

	[Column(Name = "MENU_ORDER", IsKey = false, SequenceName = "")]
	public Decimal? MenuOrder
	{
		get{return menuOrder;}
		set{menuOrder = value;}
	}

	[Column(Name = "CALL_BACK_FUNC", IsKey = false, SequenceName = "")]
	public String CallBackFunc
	{
		get{return callBackFunc;}
		set{callBackFunc = value;}
	}

	[Column(Name = "MENU_TYPE", IsKey = false, SequenceName = "")]
	public String MenuType
	{
		get{return menuType;}
		set{menuType = value;}
	}

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IpAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public String EnteredBY
    {
        get { return enteredBY; }
        set { enteredBY = value; }
    }

	public ComMenuInfo()
	{}

}
