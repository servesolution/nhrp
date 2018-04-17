using System;
using EntityFramework;

[Table(Name = "MIG_BATCH_INFO")]
public class BatchInfoInfo : EntityBase
{

	private System.Decimal? batchId = null;

	private System.DateTime? batchDate = null;

    private System.DateTime? batchStartTime = null;

    private System.DateTime? batchEndTime = null;

	private System.String fileName = null;

    private System.Decimal? houseOwnerCnt = null;

    private System.Decimal? buildingStructureCnt = null;

    private System.Decimal? householdCnt = null;
    
    private System.Decimal? memberCnt = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

    private string isPosted = null;

    private string postedError = null;
    //private System.String ipAddress = null;

	[Column(Name = "BATCH_ID", IsKey = true, SequenceName = "")]
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

    [Column(Name = "BATCH_START_TIME", IsKey = false, SequenceName = "")]
    public System.DateTime? BatchStartTime
    {
        get { return batchStartTime; }
        set { batchStartTime = value; }
    }

    [Column(Name = "BATCH_END_TIME", IsKey = false, SequenceName = "")]
    public System.DateTime? BatchEndTime
    {
        get { return batchEndTime; }
        set { batchEndTime = value; }
    }

	[Column(Name = "FILE_NAME", IsKey = false, SequenceName = "")]
	public System.String FileName 
	{ 
		get{return fileName;}
		set{fileName = value;} 
	}

    [Column(Name = "HOUSE_OWNER_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? HouseOwnerCnt
    {
        get { return houseOwnerCnt; }
        set { houseOwnerCnt = value; }
    }

    [Column(Name = "BUILDING_STRUCTURE_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? BuildingStructureCnt
    {
        get { return buildingStructureCnt; }
        set { buildingStructureCnt = value; }
    }

    [Column(Name = "HOUSEHOLD_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? HouseholdCnt
    {
        get { return householdCnt; }
        set { householdCnt = value; }
    }

    [Column(Name = "MEMBER_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? MemberCnt
    {
        get { return memberCnt; }
        set { memberCnt = value; }
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

    [Column(Name = "IS_POSTED", IsKey = false, SequenceName = "")]
    public System.String IsPosted
    {
        get { return isPosted; }
        set { isPosted = value; }
    }

    [Column(Name = "POSTED_ERROR", IsKey = false, SequenceName = "")]
    public System.String PostedError
    {
        get { return postedError; }
        set { postedError = value; }
    }
    //[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    //public String IPAddress
    //{
    //    get { return ipAddress; }
    //    set { ipAddress = value; }
    //}

	public BatchInfoInfo()
	{}

   
}