using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HH_ALLOWANCE_DTL")]
public class MigMisHhAllowanceDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String buildingStructureNo = null;

	private System.String householdId = null;

	private System.String memberId = null;

	private System.Decimal? sno = null;

	private System.Decimal? allSourceCd = null;

	private System.Decimal? allowanceTypeCd = null;

	private System.DateTime? allowanceStDt = null;

	private System.String allowanceStDtLoc = null;

	private System.Decimal? allowanceYears = null;

	private System.Decimal? allowanceMonth = null;

	private System.Decimal? allowanceDay = null;

	private System.Decimal? allowanceLastYr = null;

	private System.Decimal? allowanceAmt = null;

	private System.Decimal? distanceHr = null;

	private System.Decimal? distanceMin = null;

	private System.Decimal? handiColorCd = null;

	private System.String remarks = null;

	private System.String remarksLoc = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.DateTime? approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	private System.DateTime? updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

	private System.String ipAddress = null;

	[Column(Name = "HOUSE_OWNER_ID", IsKey = false, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

	[Column(Name = "BUILDING_STRUCTURE_NO", IsKey = false, SequenceName = "")]
	public System.String BuildingStructureNo 
	{ 
		get{return buildingStructureNo;}
		set{buildingStructureNo = value;} 
	}

	[Column(Name = "HOUSEHOLD_ID", IsKey = true, SequenceName = "")]
	public System.String HouseholdId 
	{ 
		get{return householdId;}
		set{householdId = value;} 
	}

	[Column(Name = "MEMBER_ID", IsKey = true, SequenceName = "")]
	public System.String MemberId 
	{ 
		get{return memberId;}
		set{memberId = value;} 
	}

	[Column(Name = "SNO", IsKey = true, SequenceName = "")]
	public System.Decimal? Sno 
	{ 
		get{return sno;}
		set{sno = value;} 
	}

	[Column(Name = "ALL_SOURCE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? AllSourceCd 
	{ 
		get{return allSourceCd;}
		set{allSourceCd = value;} 
	}

	[Column(Name = "ALLOWANCE_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? AllowanceTypeCd 
	{ 
		get{return allowanceTypeCd;}
		set{allowanceTypeCd = value;} 
	}

	[Column(Name = "ALLOWANCE_ST_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? AllowanceStDt 
	{ 
		get{return allowanceStDt;}
		set{allowanceStDt = value;} 
	}

	[Column(Name = "ALLOWANCE_ST_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String AllowanceStDtLoc 
	{ 
		get{return allowanceStDtLoc;}
		set{allowanceStDtLoc = value;} 
	}

	[Column(Name = "ALLOWANCE_YEARS", IsKey = false, SequenceName = "")]
	public System.Decimal? AllowanceYears 
	{ 
		get{return allowanceYears;}
		set{allowanceYears = value;} 
	}

	[Column(Name = "ALLOWANCE_MONTH", IsKey = false, SequenceName = "")]
	public System.Decimal? AllowanceMonth 
	{ 
		get{return allowanceMonth;}
		set{allowanceMonth = value;} 
	}

	[Column(Name = "ALLOWANCE_DAY", IsKey = false, SequenceName = "")]
	public System.Decimal? AllowanceDay 
	{ 
		get{return allowanceDay;}
		set{allowanceDay = value;} 
	}

	[Column(Name = "ALLOWANCE_LAST_YR", IsKey = false, SequenceName = "")]
	public System.Decimal? AllowanceLastYr 
	{ 
		get{return allowanceLastYr;}
		set{allowanceLastYr = value;} 
	}

	[Column(Name = "ALLOWANCE_AMT", IsKey = false, SequenceName = "")]
	public System.Decimal? AllowanceAmt 
	{ 
		get{return allowanceAmt;}
		set{allowanceAmt = value;} 
	}

	[Column(Name = "DISTANCE_HR", IsKey = false, SequenceName = "")]
	public System.Decimal? DistanceHr 
	{ 
		get{return distanceHr;}
		set{distanceHr = value;} 
	}

	[Column(Name = "DISTANCE_MIN", IsKey = false, SequenceName = "")]
	public System.Decimal? DistanceMin 
	{ 
		get{return distanceMin;}
		set{distanceMin = value;} 
	}

	[Column(Name = "HANDI_COLOR_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? HandiColorCd 
	{ 
		get{return handiColorCd;}
		set{handiColorCd = value;} 
	}

	[Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
	public System.String Remarks 
	{ 
		get{return remarks;}
		set{remarks = value;} 
	}

	[Column(Name = "REMARKS_LOC", IsKey = false, SequenceName = "")]
	public System.String RemarksLoc 
	{ 
		get{return remarksLoc;}
		set{remarksLoc = value;} 
	}

	[Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
	public System.String Approved 
	{ 
		get{return approved;}
		set{approved = value;} 
	}

	[Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
	public System.String ApprovedBy 
	{ 
		get{return approvedBy;}
		set{approvedBy = value;} 
	}

	[Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? ApprovedDt 
	{ 
		get{return approvedDt;}
		set{approvedDt = value;} 
	}

	[Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String ApprovedDtLoc 
	{ 
		get{return approvedDtLoc;}
		set{approvedDtLoc = value;} 
	}

	[Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
	public System.String UpdatedBy 
	{ 
		get{return updatedBy;}
		set{updatedBy = value;} 
	}

	[Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? UpdatedDt 
	{ 
		get{return updatedDt;}
		set{updatedDt = value;} 
	}

	[Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String UpdatedDtLoc 
	{ 
		get{return updatedDtLoc;}
		set{updatedDtLoc = value;} 
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public System.String EnteredBy 
	{ 
		get{return enteredBy;}
		set{enteredBy = value;} 
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? EnteredDt 
	{ 
		get{return enteredDt;}
		set{enteredDt = value;} 
	}

	[Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String EnteredDtLoc 
	{ 
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;} 
	}

	[Column(Name = "NHRS_UUID", IsKey = false, SequenceName = "")]
	public System.String NhrsUuid 
	{ 
		get{return nhrsUuid;}
		set{nhrsUuid = value;} 
	}

	[Column(Name = "BATCH_ID", IsKey = false, SequenceName = "")]
	public System.Decimal? BatchId 
	{ 
		get{return batchId;}
		set{batchId = value;} 
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


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigMisHhAllowanceDtlInfo()
	{}
}