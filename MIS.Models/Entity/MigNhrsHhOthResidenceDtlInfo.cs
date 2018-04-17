using System;
using EntityFramework;

[Table(Name = "HOWNER_OTH_RESIDENCE_DTL")]
public class MigNhrsHhOthResidenceDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.Decimal? otherResidenceId = null;

    private System.String memberId = null;

    private System.String firstNameEng = null;

    private System.String middleNameEng = null;

    private System.String lastNameEng = null;

    private System.String fullNameEng = null;

    private System.String firstNameLoc = null;

    private System.String middleNameLoc = null;

    private System.String lastNameLoc = null;

    private System.String fullNameLoc = null;

	private System.Decimal? otherCountryCd = null;

	private System.Decimal? otherRegStCd = null;

	private System.Decimal? otherZoneCd = null;

	private System.Decimal? otherDistrictCd = null;

	private System.Decimal? otherVdcMunCd = null;

	private System.Decimal? otherWardNo = null;

	private System.String otherAreaEng = null;

	private System.String otherAreaLoc = null;

	private System.Decimal? buildingConditionCd = null;

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

	[Column(Name = "OTHER_RESIDENCE_ID", IsKey = true, SequenceName = "")]
	public System.Decimal? OtherResidenceId 
	{ 
		get{return otherResidenceId;}
		set{otherResidenceId = value;} 
	}

    [Column(Name = "MEMBER_ID", IsKey = true, SequenceName = "")]
    public System.String MemberId
    {
        get { return memberId; }
        set { memberId = value; }
    }

    [Column(Name = "FIRST_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FirstNameEng
    {
        get { return firstNameEng; }
        set { firstNameEng = value; }
    }

    [Column(Name = "MIDDLE_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String MiddleNameEng
    {
        get { return middleNameEng; }
        set { middleNameEng = value; }
    }

    [Column(Name = "LAST_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String LastNameEng
    {
        get { return lastNameEng; }
        set { lastNameEng = value; }
    }

    [Column(Name = "FULL_NAME_ENG", IsKey = true, SequenceName = "")]
    public System.String FullNameEng
    {
        get { return fullNameEng; }
        set { fullNameEng = value; }
    }

    [Column(Name = "FIRST_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FirstNameLoc
    {
        get { return firstNameLoc; }
        set { firstNameLoc = value; }
    }

    [Column(Name = "MIDDLE_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String MiddleNameLoc
    {
        get { return middleNameLoc; }
        set { middleNameLoc = value; }
    }

    [Column(Name = "LAST_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String LastNameLoc
    {
        get { return lastNameLoc; }
        set { lastNameLoc = value; }
    }

    [Column(Name = "FULL_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String FullNameLoc
    {
        get { return fullNameLoc; }
        set { fullNameLoc = value; }
    }

	[Column(Name = "OTHER_COUNTRY_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? OtherCountryCd 
	{ 
		get{return otherCountryCd;}
		set{otherCountryCd = value;} 
	}

	[Column(Name = "OTHER_REG_ST_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? OtherRegStCd 
	{ 
		get{return otherRegStCd;}
		set{otherRegStCd = value;} 
	}

	[Column(Name = "OTHER_ZONE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? OtherZoneCd 
	{ 
		get{return otherZoneCd;}
		set{otherZoneCd = value;} 
	}

	[Column(Name = "OTHER_DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? OtherDistrictCd 
	{ 
		get{return otherDistrictCd;}
		set{otherDistrictCd = value;} 
	}

	[Column(Name = "OTHER_VDC_MUN_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? OtherVdcMunCd 
	{ 
		get{return otherVdcMunCd;}
		set{otherVdcMunCd = value;} 
	}

	[Column(Name = "OTHER_WARD_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? OtherWardNo 
	{ 
		get{return otherWardNo;}
		set{otherWardNo = value;} 
	}

	[Column(Name = "OTHER_AREA_ENG", IsKey = false, SequenceName = "")]
	public System.String OtherAreaEng 
	{ 
		get{return otherAreaEng;}
		set{otherAreaEng = value;} 
	}

	[Column(Name = "OTHER_AREA_LOC", IsKey = false, SequenceName = "")]
	public System.String OtherAreaLoc 
	{ 
		get{return otherAreaLoc;}
		set{otherAreaLoc = value;} 
	}

	[Column(Name = "BUILDING_CONDITION_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? BuildingConditionCd 
	{ 
		get{return buildingConditionCd;}
		set{buildingConditionCd = value;} 
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

	public MigNhrsHhOthResidenceDtlInfo()
	{}
}