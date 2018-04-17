using System;
using EntityFramework;

[Table(Name = "MIG_ERROR_LOG")]
public class MigErrorLogInfo : EntityBase
{

	private System.Decimal? batchId = null;

	private System.DateTime? batchDate = null;

	private System.String fileName = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

	private System.String nhrsUuid = null;

	private System.String tableName = null;

	private System.String columnName = null;

	private System.String ipAddress = null;

	[Column(Name = "BATCH_ID", IsKey = false, SequenceName = "")]
	public System.Decimal? BatchId 
	{ 
		get{return batchId;}
		set{batchId = value;} 
	}

	[Column(Name = "BATCH_DATE", IsKey = false, SequenceName = "")]
	public System.DateTime? BatchDate 
	{ 
		get{return batchDate;}
		set{batchDate = value;} 
	}

	[Column(Name = "FILE_NAME", IsKey = false, SequenceName = "")]
	public System.String FileName 
	{ 
		get{return fileName;}
		set{fileName = value;} 
	}

	[Column(Name = "ERROR_NO", IsKey = false, SequenceName = "")]
	public System.String ErrorNo 
	{ 
		get{return errorNo;}
		set{errorNo = value;} 
	}

	[Column(Name = "ERROR_MSG", IsKey = false, SequenceName = "")]
	public System.String ErrorMsg 
	{ 
		get{return errorMsg;}
		set{errorMsg = value;} 
	}

	[Column(Name = "NHRS_UUID", IsKey = false, SequenceName = "")]
	public System.String NhrsUuid 
	{ 
		get{return nhrsUuid;}
		set{nhrsUuid = value;} 
	}

	[Column(Name = "TABLE_NAME", IsKey = false, SequenceName = "")]
	public System.String TableName 
	{ 
		get{return tableName;}
		set{tableName = value;} 
	}

	[Column(Name = "COLUMN_NAME", IsKey = false, SequenceName = "")]
	public System.String ColumnName 
	{ 
		get{return columnName;}
		set{columnName = value;} 
	}


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigErrorLogInfo()
	{}
}