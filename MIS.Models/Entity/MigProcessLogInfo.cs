using System;
using EntityFramework;

[Table(Name = "MIG_PROCESS_LOG")]
public class MigProcessLogInfo : EntityBase
{

	private System.Decimal? batchId = null;

	private System.DateTime? batchDate = null;

	private System.Decimal? processLogId = null;

	private System.String processDesc = null;

	private System.String processStartTime = null;

	private System.String processEndTime = null;

	private System.String tableName = null;

	private System.Decimal? insertedRecordCnt = null;

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

	[Column(Name = "PROCESS_LOG_ID", IsKey = false, SequenceName = "")]
	public System.Decimal? ProcessLogId 
	{ 
		get{return processLogId;}
		set{processLogId = value;} 
	}

	[Column(Name = "PROCESS_DESC", IsKey = false, SequenceName = "")]
	public System.String ProcessDesc 
	{ 
		get{return processDesc;}
		set{processDesc = value;} 
	}

	[Column(Name = "PROCESS_START_TIME", IsKey = false, SequenceName = "")]
	public System.String ProcessStartTime 
	{ 
		get{return processStartTime;}
		set{processStartTime = value;} 
	}

	[Column(Name = "PROCESS_END_TIME", IsKey = false, SequenceName = "")]
	public System.String ProcessEndTime 
	{ 
		get{return processEndTime;}
		set{processEndTime = value;} 
	}

	[Column(Name = "TABLE_NAME", IsKey = false, SequenceName = "")]
	public System.String TableName 
	{ 
		get{return tableName;}
		set{tableName = value;} 
	}

	[Column(Name = "INSERTED_RECORD_CNT", IsKey = false, SequenceName = "")]
	public System.Decimal? InsertedRecordCnt 
	{ 
		get{return insertedRecordCnt;}
		set{insertedRecordCnt = value;} 
	}


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigProcessLogInfo()
	{}
}