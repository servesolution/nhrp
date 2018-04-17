using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HH_SCHOOL_DTL")]
public class MigMisHhSchoolDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String buildingStructureNo = null;

	private System.String householdId = null;

	private System.String memberId = null;

	private System.Decimal? sno = null;

	private System.String schoolName = null;

	private System.String schoolNameLoc = null;

	private System.Decimal? classTypeCd = null;

	private System.Decimal? schoolTypeCd = null;

	private System.Decimal? countryCd = null;

	private System.Decimal? regStCd = null;

	private System.Decimal? zoneCd = null;

	private System.Decimal? districtCd = null;

	private System.Decimal? vdcMunCd = null;

	private System.Decimal? wardNo = null;

	private System.String areaEng = null;

	private System.String areaLoc = null;

	private System.String remarks = null;

	private System.String remarksLoc = null;

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

	private System.String misliteUploadFlag = null;

	private System.Decimal? misliteExportCd = null;

	private System.Decimal? misliteUploadCd = null;

	private System.String misliteMemberId = null;

	private System.String misliteHouseholdId = null;

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

	[Column(Name = "SCHOOL_NAME", IsKey = false, SequenceName = "")]
	public System.String SchoolName 
	{ 
		get{return schoolName;}
		set{schoolName = value;} 
	}

	[Column(Name = "SCHOOL_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String SchoolNameLoc 
	{ 
		get{return schoolNameLoc;}
		set{schoolNameLoc = value;} 
	}

	[Column(Name = "CLASS_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? ClassTypeCd 
	{ 
		get{return classTypeCd;}
		set{classTypeCd = value;} 
	}

	[Column(Name = "SCHOOL_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? SchoolTypeCd 
	{ 
		get{return schoolTypeCd;}
		set{schoolTypeCd = value;} 
	}

	[Column(Name = "COUNTRY_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? CountryCd 
	{ 
		get{return countryCd;}
		set{countryCd = value;} 
	}

	[Column(Name = "REG_ST_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? RegStCd 
	{ 
		get{return regStCd;}
		set{regStCd = value;} 
	}

	[Column(Name = "ZONE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? ZoneCd 
	{ 
		get{return zoneCd;}
		set{zoneCd = value;} 
	}

	[Column(Name = "DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DistrictCd 
	{ 
		get{return districtCd;}
		set{districtCd = value;} 
	}

	[Column(Name = "VDC_MUN_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? VdcMunCd 
	{ 
		get{return vdcMunCd;}
		set{vdcMunCd = value;} 
	}

	[Column(Name = "WARD_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? WardNo 
	{ 
		get{return wardNo;}
		set{wardNo = value;} 
	}

	[Column(Name = "AREA_ENG", IsKey = false, SequenceName = "")]
	public System.String AreaEng 
	{ 
		get{return areaEng;}
		set{areaEng = value;} 
	}

	[Column(Name = "AREA_LOC", IsKey = false, SequenceName = "")]
	public System.String AreaLoc 
	{ 
		get{return areaLoc;}
		set{areaLoc = value;} 
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

	[Column(Name = "MISLITE_UPLOAD_FLAG", IsKey = false, SequenceName = "")]
	public System.String MisliteUploadFlag 
	{ 
		get{return misliteUploadFlag;}
		set{misliteUploadFlag = value;} 
	}

	[Column(Name = "MISLITE_EXPORT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? MisliteExportCd 
	{ 
		get{return misliteExportCd;}
		set{misliteExportCd = value;} 
	}

	[Column(Name = "MISLITE_UPLOAD_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? MisliteUploadCd 
	{ 
		get{return misliteUploadCd;}
		set{misliteUploadCd = value;} 
	}

	[Column(Name = "MISLITE_MEMBER_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteMemberId 
	{ 
		get{return misliteMemberId;}
		set{misliteMemberId = value;} 
	}

	[Column(Name = "MISLITE_HOUSEHOLD_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteHouseholdId 
	{ 
		get{return misliteHouseholdId;}
		set{misliteHouseholdId = value;} 
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

	public MigMisHhSchoolDtlInfo()
	{}
}