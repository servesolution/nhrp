using System;
using EntityFramework;

[Table(Name = "COM_AUDIT_TRAIL")]
public class ComAuditTrailInfo : EntityBase
{
	private Decimal audSno  = 0;
	private Decimal sno  = 0;
	private String audUser  = null;
	private String audOperation  = null;
	private DateTime audDatetime  = DateTime.Now;
	private String tableName  = null;
	private String userTableDesc  = null;
	private String audAbbrInfo  = null;
	private String audRecInfo  = null;
	private String columnName  = null;
	private String oldValue  = null;
	private String newValue  = null;
	private String audTerminal  = null;
	private String audHost  = null;
	private String audIpAddress  = null;

	[Column(Name = "AUD_SNO", IsKey = true, SequenceName = "")]
	public Decimal AudSno
	{
		get{return audSno;}
		set{audSno = value;}
	}

	[Column(Name = "SNO", IsKey = true, SequenceName = "")]
	public Decimal Sno
	{
		get{return sno;}
		set{sno = value;}
	}

	[Column(Name = "AUD_USER", IsKey = false, SequenceName = "")]
	public String AudUser
	{
		get{return audUser;}
		set{audUser = value;}
	}

	[Column(Name = "AUD_OPERATION", IsKey = false, SequenceName = "")]
	public String AudOperation
	{
		get{return audOperation;}
		set{audOperation = value;}
	}

	[Column(Name = "AUD_DATETIME", IsKey = false, SequenceName = "")]
	public DateTime AudDatetime
	{
		get{return audDatetime;}
		set{audDatetime = value;}
	}

	[Column(Name = "TABLE_NAME", IsKey = false, SequenceName = "")]
	public String TableName
	{
		get{return tableName;}
		set{tableName = value;}
	}

	[Column(Name = "USER_TABLE_DESC", IsKey = false, SequenceName = "")]
	public String UserTableDesc
	{
		get{return userTableDesc;}
		set{userTableDesc = value;}
	}

	[Column(Name = "AUD_ABBR_INFO", IsKey = false, SequenceName = "")]
	public String AudAbbrInfo
	{
		get{return audAbbrInfo;}
		set{audAbbrInfo = value;}
	}

	[Column(Name = "AUD_REC_INFO", IsKey = false, SequenceName = "")]
	public String AudRecInfo
	{
		get{return audRecInfo;}
		set{audRecInfo = value;}
	}

	[Column(Name = "COLUMN_NAME", IsKey = false, SequenceName = "")]
	public String ColumnName
	{
		get{return columnName;}
		set{columnName = value;}
	}

	[Column(Name = "OLD_VALUE", IsKey = false, SequenceName = "")]
	public String OldValue
	{
		get{return oldValue;}
		set{oldValue = value;}
	}

	[Column(Name = "NEW_VALUE", IsKey = false, SequenceName = "")]
	public String NewValue
	{
		get{return newValue;}
		set{newValue = value;}
	}

	[Column(Name = "AUD_TERMINAL", IsKey = false, SequenceName = "")]
	public String AudTerminal
	{
		get{return audTerminal;}
		set{audTerminal = value;}
	}

	[Column(Name = "AUD_HOST", IsKey = false, SequenceName = "")]
	public String AudHost
	{
		get{return audHost;}
		set{audHost = value;}
	}

	[Column(Name = "AUD_IP_ADDRESS", IsKey = false, SequenceName = "")]
	public String AudIpAddress
	{
		get{return audIpAddress;}
		set{audIpAddress = value;}
	}

	public ComAuditTrailInfo()
	{}

}
