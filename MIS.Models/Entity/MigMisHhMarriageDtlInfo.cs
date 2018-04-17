using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HH_MARRIAGE_DTL")]
public class MigMisHhMarriageDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String buildingStructureNo = null;

	private System.String householdId = null;

	private System.String memberId = null;

	private System.Decimal? sno = null;

	private System.String spouseMemberId = null;

	private System.String marriageCertificate = null;

	private System.String marriageCertificateNo = null;

	private System.Decimal? marriageYear = null;

	private System.Decimal? marriageMonth = null;

	private System.Decimal? marriageDay = null;

	private System.Decimal? marriageYearLoc = null;

	private System.Decimal? marriageMonthLoc = null;

	private System.Decimal? marriageDayLoc = null;

	private System.DateTime? marriageDt = null;

	private System.String marriageDtLoc = null;

	private System.String marriageDoc = null;

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

	private System.String misliteMemberId = null;

	private System.String misliteSpouseId = null;

	private System.Decimal? misliteUploadCd = null;

	private System.Decimal? misliteExportCd = null;

	private System.String misliteUploadFlag = null;

	private System.String misliteHouseholdId = null;

	private System.String memberno = null;

	private System.String spousememberno = null;

	private System.Decimal? vdcNo = null;

	private System.String membernoNew = null;

	private System.String spousemembernoNew = null;

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

	[Column(Name = "SPOUSE_MEMBER_ID", IsKey = false, SequenceName = "")]
	public System.String SpouseMemberId 
	{ 
		get{return spouseMemberId;}
		set{spouseMemberId = value;} 
	}

	[Column(Name = "MARRIAGE_CERTIFICATE", IsKey = false, SequenceName = "")]
	public System.String MarriageCertificate 
	{ 
		get{return marriageCertificate;}
		set{marriageCertificate = value;} 
	}

	[Column(Name = "MARRIAGE_CERTIFICATE_NO", IsKey = false, SequenceName = "")]
	public System.String MarriageCertificateNo 
	{ 
		get{return marriageCertificateNo;}
		set{marriageCertificateNo = value;} 
	}

	[Column(Name = "MARRIAGE_YEAR", IsKey = false, SequenceName = "")]
	public System.Decimal? MarriageYear 
	{ 
		get{return marriageYear;}
		set{marriageYear = value;} 
	}

	[Column(Name = "MARRIAGE_MONTH", IsKey = false, SequenceName = "")]
	public System.Decimal? MarriageMonth 
	{ 
		get{return marriageMonth;}
		set{marriageMonth = value;} 
	}

	[Column(Name = "MARRIAGE_DAY", IsKey = false, SequenceName = "")]
	public System.Decimal? MarriageDay 
	{ 
		get{return marriageDay;}
		set{marriageDay = value;} 
	}

	[Column(Name = "MARRIAGE_YEAR_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? MarriageYearLoc 
	{ 
		get{return marriageYearLoc;}
		set{marriageYearLoc = value;} 
	}

	[Column(Name = "MARRIAGE_MONTH_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? MarriageMonthLoc 
	{ 
		get{return marriageMonthLoc;}
		set{marriageMonthLoc = value;} 
	}

	[Column(Name = "MARRIAGE_DAY_LOC", IsKey = false, SequenceName = "")]
	public System.Decimal? MarriageDayLoc 
	{ 
		get{return marriageDayLoc;}
		set{marriageDayLoc = value;} 
	}

	[Column(Name = "MARRIAGE_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? MarriageDt 
	{ 
		get{return marriageDt;}
		set{marriageDt = value;} 
	}

	[Column(Name = "MARRIAGE_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String MarriageDtLoc 
	{ 
		get{return marriageDtLoc;}
		set{marriageDtLoc = value;} 
	}

	[Column(Name = "MARRIAGE_DOC", IsKey = false, SequenceName = "")]
	public System.String MarriageDoc 
	{ 
		get{return marriageDoc;}
		set{marriageDoc = value;} 
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

	[Column(Name = "MISLITE_MEMBER_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteMemberId 
	{ 
		get{return misliteMemberId;}
		set{misliteMemberId = value;} 
	}

	[Column(Name = "MISLITE_SPOUSE_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteSpouseId 
	{ 
		get{return misliteSpouseId;}
		set{misliteSpouseId = value;} 
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

	[Column(Name = "MISLITE_HOUSEHOLD_ID", IsKey = false, SequenceName = "")]
	public System.String MisliteHouseholdId 
	{ 
		get{return misliteHouseholdId;}
		set{misliteHouseholdId = value;} 
	}

	[Column(Name = "MEMBERNO", IsKey = false, SequenceName = "")]
	public System.String Memberno 
	{ 
		get{return memberno;}
		set{memberno = value;} 
	}

	[Column(Name = "SPOUSEMEMBERNO", IsKey = false, SequenceName = "")]
	public System.String Spousememberno 
	{ 
		get{return spousememberno;}
		set{spousememberno = value;} 
	}

	[Column(Name = "VDC_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? VdcNo 
	{ 
		get{return vdcNo;}
		set{vdcNo = value;} 
	}

	[Column(Name = "MEMBERNO_NEW", IsKey = false, SequenceName = "")]
	public System.String MembernoNew 
	{ 
		get{return membernoNew;}
		set{membernoNew = value;} 
	}

	[Column(Name = "SPOUSEMEMBERNO_NEW", IsKey = false, SequenceName = "")]
	public System.String SpousemembernoNew 
	{ 
		get{return spousemembernoNew;}
		set{spousemembernoNew = value;} 
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

	public MigMisHhMarriageDtlInfo()
	{}
}