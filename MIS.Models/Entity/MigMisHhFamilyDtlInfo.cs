using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HH_FAMILY_DTL")]
public class MigMisHhFamilyDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;
	private System.String buildingStructureNo = null;
	private System.String householdId = null;
	private System.String memberId = null;
	private System.Decimal? sno = null;
	private System.Decimal? relationTypeCd = null;
	private System.String remarks = null;
	private System.String remarksLoc = null;
	private System.String memberActive = null;
	private System.String memberDeath = null;
	private System.String memberMarriage = null;
	private System.String memberSplit = null;
	private System.String transferHhId = null;
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
	private System.Decimal? presenceStatusCd = null;
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

	[Column(Name = "RELATION_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? RelationTypeCd 
	{ 
		get{return relationTypeCd;}
		set{relationTypeCd = value;} 
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

	[Column(Name = "MEMBER_ACTIVE", IsKey = false, SequenceName = "")]
	public System.String MemberActive 
	{ 
		get{return memberActive;}
		set{memberActive = value;} 
	}

	[Column(Name = "MEMBER_DEATH", IsKey = false, SequenceName = "")]
	public System.String MemberDeath 
	{ 
		get{return memberDeath;}
		set{memberDeath = value;} 
	}

	[Column(Name = "MEMBER_MARRIAGE", IsKey = false, SequenceName = "")]
	public System.String MemberMarriage 
	{ 
		get{return memberMarriage;}
		set{memberMarriage = value;} 
	}

	[Column(Name = "MEMBER_SPLIT", IsKey = false, SequenceName = "")]
	public System.String MemberSplit 
	{ 
		get{return memberSplit;}
		set{memberSplit = value;} 
	}

	[Column(Name = "TRANSFER_HH_ID", IsKey = false, SequenceName = "")]
	public System.String TransferHhId 
	{ 
		get{return transferHhId;}
		set{transferHhId = value;} 
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

	[Column(Name = "PRESENCE_STATUS_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? PresenceStatusCd 
	{ 
		get{return presenceStatusCd;}
		set{presenceStatusCd = value;} 
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

	public MigMisHhFamilyDtlInfo()
	{}
}