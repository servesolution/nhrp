using System;
using EntityFramework;

[Table(Name = "COM_SCREEN_NAME")]
public class ComScreenNameInfo : EntityBase
{
	private String tableName  = null;
	private String tableDesc  = null;

	[Column(Name = "TABLE_NAME", IsKey = true, SequenceName = "")]
	public String TableName
	{
		get{return tableName;}
		set{tableName = value;}
	}

	[Column(Name = "TABLE_DESC", IsKey = true, SequenceName = "")]
	public String TableDesc
	{
		get{return tableDesc;}
		set{tableDesc = value;}
	}

	public ComScreenNameInfo()
	{}

}
