using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_BUILDG_ASS_DTL")]
public class MigNhrsBuildgAssDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.Decimal? districtCd = null;

	private System.String buildingStructureNo = null;

	private System.Decimal? hhDamageDtlId = null;

	private System.Decimal? damageCd = null;

	private System.Decimal? damageLevelCd = null;

	private System.Decimal? damageLevelDtlCd = null;

	private System.String commentEng = null;

	private System.String commentLoc = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.String approvedByLoc = null;

	private System.DateTime? approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	private System.String updatedByLoc = null;

	private System.DateTime? updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	private System.String enteredByLoc = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

	private System.String ipAddress = null;

	[Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

	[Column(Name = "DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DistrictCd 
	{ 
		get{return districtCd;}
		set{districtCd = value;} 
	}

	[Column(Name = "BUILDING_STRUCTURE_NO", IsKey = true, SequenceName = "")]
	public System.String BuildingStructureNo 
	{ 
		get{return buildingStructureNo;}
		set{buildingStructureNo = value;} 
	}

	[Column(Name = "HH_DAMAGE_DTL_ID", IsKey = true, SequenceName = "")]
	public System.Decimal? HhDamageDtlId 
	{ 
		get{return hhDamageDtlId;}
		set{hhDamageDtlId = value;} 
	}

	[Column(Name = "DAMAGE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DamageCd 
	{ 
		get{return damageCd;}
		set{damageCd = value;} 
	}

	[Column(Name = "DAMAGE_LEVEL_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DamageLevelCd 
	{ 
		get{return damageLevelCd;}
		set{damageLevelCd = value;} 
	}

	[Column(Name = "DAMAGE_LEVEL_DTL_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DamageLevelDtlCd 
	{ 
		get{return damageLevelDtlCd;}
		set{damageLevelDtlCd = value;} 
	}

	[Column(Name = "COMMENT_ENG", IsKey = false, SequenceName = "")]
	public System.String CommentEng 
	{ 
		get{return commentEng;}
		set{commentEng = value;} 
	}

	[Column(Name = "COMMENT_LOC", IsKey = false, SequenceName = "")]
	public System.String CommentLoc 
	{ 
		get{return commentLoc;}
		set{commentLoc = value;} 
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

	[Column(Name = "APPROVED_BY_LOC", IsKey = false, SequenceName = "")]
	public System.String ApprovedByLoc 
	{ 
		get{return approvedByLoc;}
		set{approvedByLoc = value;} 
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

	[Column(Name = "UPDATED_BY_LOC", IsKey = false, SequenceName = "")]
	public System.String UpdatedByLoc 
	{ 
		get{return updatedByLoc;}
		set{updatedByLoc = value;} 
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

	[Column(Name = "ENTERED_BY_LOC", IsKey = false, SequenceName = "")]
	public System.String EnteredByLoc 
	{ 
		get{return enteredByLoc;}
		set{enteredByLoc = value;} 
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

	public MigNhrsBuildgAssDtlInfo()
	{}
}