using System;
using EntityFramework;

[Table(Name = "COM_MENU_ENTITY")]
public class ComMenuEntityInfo : EntityBase
{
	private String menuCd  = null;
	private String entityCd  = null;
	private String entityName  = null;
	private String entityDesc  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private String lastUpdatedBy  = null;
	private DateTime? lastUpdatedDt  = null;

	[Column(Name = "MENU_CD", IsKey = true, SequenceName = "")]
	public String MenuCd
	{
		get{return menuCd;}
		set{menuCd = value;}
	}

	[Column(Name = "ENTITY_CD", IsKey = true, SequenceName = "")]
	public String EntityCd
	{
		get{return entityCd;}
		set{entityCd = value;}
	}

	[Column(Name = "ENTITY_NAME", IsKey = false, SequenceName = "")]
	public String EntityName
	{
		get{return entityName;}
		set{entityName = value;}
	}

	[Column(Name = "ENTITY_DESC", IsKey = false, SequenceName = "")]
	public String EntityDesc
	{
		get{return entityDesc;}
		set{entityDesc = value;}
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
	public DateTime? LastUpdatedDt
	{
		get{return lastUpdatedDt;}
		set{lastUpdatedDt = value;}
	}

	public ComMenuEntityInfo()
	{}

}
