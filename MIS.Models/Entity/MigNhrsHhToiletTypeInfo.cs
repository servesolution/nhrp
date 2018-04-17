using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_HH_TOILET_TYPE")]
public class MigNhrsHhToiletTypeInfo : EntityBase
{

	private System.String householdId = null;

	private System.String houseOwnerId = null;

	private System.Decimal? toiletTypeCd = null;

	private System.String toiletTypeBefore = null;

	private System.String toiletTypeAfter = null;

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

    private System.Decimal? batchId = null;

	private System.String ipAddress = null;

	[Column(Name = "HOUSEHOLD_ID", IsKey = true, SequenceName = "")]
	public System.String HouseholdId 
	{ 
		get{return householdId;}
		set{householdId = value;} 
	}

	[Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

	[Column(Name = "TOILET_TYPE_CD", IsKey = true, SequenceName = "")]
	public System.Decimal? ToiletTypeCd 
	{ 
		get{return toiletTypeCd;}
		set{toiletTypeCd = value;} 
	}

	[Column(Name = "TOILET_TYPE_BEFORE", IsKey = false, SequenceName = "")]
	public System.String ToiletTypeBefore 
	{ 
		get{return toiletTypeBefore;}
		set{toiletTypeBefore = value;} 
	}

	[Column(Name = "TOILET_TYPE_AFTER", IsKey = false, SequenceName = "")]
	public System.String ToiletTypeAfter 
	{ 
		get{return toiletTypeAfter;}
		set{toiletTypeAfter = value;} 
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

    [Column(Name = "BATCH_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? BatchId
    {
        get { return batchId; }
        set { batchId = value; }
    }

	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigNhrsHhToiletTypeInfo()
	{}
}