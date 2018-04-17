using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HH_DIVORCE_DTL")]
public class MigMisHhDivorceDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String buildingStructureNo = null;

	private System.String householdId = null;

	private System.String memberId = null;

	private System.Decimal? sno = null;

	private System.String divorceCertificate = null;

	private System.String divorceCertificateNo = null;

	private System.Decimal? divorceYear = null;

	private System.Decimal? divorceMonth = null;

	private System.Decimal? divorceDay = null;

	private System.Decimal? divorceYearLoc = null;

	private System.Decimal? divorceMonthLoc = null;

	private System.Decimal? divorceDayLoc = null;

	private System.DateTime? divorceDt = null;

	private System.String divorceDtLoc = null;

	private System.String divorceDoc = null;

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

	private System.String misliteHouseholdId = null;

	private System.String misliteMemberId = null;

	private System.Decimal? misliteUploadCd = null;

	private System.Decimal? misliteExportCd = null;

	private System.String misliteUploadFlag = null;

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

	[Column(Name = "DIVORCE_CERTIFICATE", IsKey = false, SequenceName = "")]
	public System.String DivorceCertificate 
	{ 
		get{return divorceCertificate;}
		set{divorceCertificate = value;} 
	}

	[Column(Name = "DIVORCE_CERTIFICATE_NO", IsKey = false, SequenceName = "")]
	public System.String DivorceCertificateNo 
	{ 
		get{return divorceCertificateNo;}
		set{divorceCertificateNo = value;} 
	}

	[Column(Name = "DIVORCE_YEAR", IsKey = false, SequenceName = "")]
	public System.Decimal? DivorceYear 
	{ 
		get{return divorceYear;}
		set{divorceYear = value;} 
	}

	[Column(Name = "DIVORCE_MONTH", IsKey = false, SequenceName = "")]
	public System.Decimal? DivorceMonth 
	{ 
		get{return divorceMonth;}
		set{divorceMonth = value;} 
	}

	[Column(Name = "DIVORCE_DAY", IsKey = false, SequenceName = "")]
	public System.Decimal? DivorceDay 
	{ 
		get{return divorceDay;}
		set{divorceDay = value;} 
	}

	[Column(Name = "DIVORCE_YEAR_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? DivorceYearLoc 
	{ 
		get{return divorceYearLoc;}
		set{divorceYearLoc = value;} 
	}

	[Column(Name = "DIVORCE_MONTH_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? DivorceMonthLoc 
	{ 
		get{return divorceMonthLoc;}
		set{divorceMonthLoc = value;} 
	}

	[Column(Name = "DIVORCE_DAY_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? DivorceDayLoc 
	{ 
		get{return divorceDayLoc;}
		set{divorceDayLoc = value;} 
	}

	[Column(Name = "DIVORCE_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? DivorceDt 
	{ 
		get{return divorceDt;}
		set{divorceDt = value;} 
	}

	[Column(Name = "DIVORCE_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String DivorceDtLoc 
	{ 
		get{return divorceDtLoc;}
		set{divorceDtLoc = value;} 
	}

	[Column(Name = "DIVORCE_DOC", IsKey = false, SequenceName = "")]
	public System.String DivorceDoc 
	{ 
		get{return divorceDoc;}
		set{divorceDoc = value;} 
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

	[Column(Name = "MISLITE_HOUSEHOLD_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteHouseholdId 
	{ 
		get{return misliteHouseholdId;}
		set{misliteHouseholdId = value;} 
	}

	[Column(Name = "MISLITE_MEMBER_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteMemberId 
	{ 
		get{return misliteMemberId;}
		set{misliteMemberId = value;} 
	}

	[Column(Name = "MISLITE_UPLOAD_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? MisliteUploadCd 
	{ 
		get{return misliteUploadCd;}
		set{misliteUploadCd = value;} 
	}

	[Column(Name = "MISLITE_EXPORT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? MisliteExportCd 
	{ 
		get{return misliteExportCd;}
		set{misliteExportCd = value;} 
	}

	[Column(Name = "MISLITE_UPLOAD_FLAG", IsKey = false, SequenceName = "")]
	public System.String MisliteUploadFlag 
	{ 
		get{return misliteUploadFlag;}
		set{misliteUploadFlag = value;} 
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

	public MigMisHhDivorceDtlInfo()
	{}
}