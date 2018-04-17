using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_GMOTHER_DTL")]
public class MigNhrsGmotherDtlInfo : EntityBase
{

	private System.String memberId = null;

	private System.String gmotherFnameEng = null;

	private System.String gmotherMnameEng = null;

	private System.String gmotherLnameEng = null;

	private System.String gmotherFullNameEng = null;

	private System.String gmotherFnameLoc = null;

	private System.String gmotherMnameLoc = null;

	private System.String gmotherLnameLoc = null;

	private System.String gmotherFullnameLoc = null;

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

	[Column(Name = "MEMBER_ID", IsKey = false, SequenceName = "")]
	public System.String MemberId 
	{ 
		get{return memberId;}
		set{memberId = value;} 
	}

	[Column(Name = "GMOTHER_FNAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GmotherFnameEng 
	{ 
		get{return gmotherFnameEng;}
		set{gmotherFnameEng = value;} 
	}

	[Column(Name = "GMOTHER_MNAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GmotherMnameEng 
	{ 
		get{return gmotherMnameEng;}
		set{gmotherMnameEng = value;} 
	}

	[Column(Name = "GMOTHER_LNAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GmotherLnameEng 
	{ 
		get{return gmotherLnameEng;}
		set{gmotherLnameEng = value;} 
	}

	[Column(Name = "GMOTHER_FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GmotherFullNameEng 
	{ 
		get{return gmotherFullNameEng;}
		set{gmotherFullNameEng = value;} 
	}

	[Column(Name = "GMOTHER_FNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GmotherFnameLoc 
	{ 
		get{return gmotherFnameLoc;}
		set{gmotherFnameLoc = value;} 
	}

	[Column(Name = "GMOTHER_MNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GmotherMnameLoc 
	{ 
		get{return gmotherMnameLoc;}
		set{gmotherMnameLoc = value;} 
	}

	[Column(Name = "GMOTHER_LNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GmotherLnameLoc 
	{ 
		get{return gmotherLnameLoc;}
		set{gmotherLnameLoc = value;} 
	}

	[Column(Name = "GMOTHER_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GmotherFullnameLoc 
	{ 
		get{return gmotherFullnameLoc;}
		set{gmotherFullnameLoc = value;} 
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

	public MigNhrsGmotherDtlInfo()
	{}
}