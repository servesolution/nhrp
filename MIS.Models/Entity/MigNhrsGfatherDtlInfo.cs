using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_GFATHER_DTL")]
public class MigNhrsGfatherDtlInfo : EntityBase
{

	private System.String memberId = null;

	private System.String gfatherFnameEng = null;

	private System.String gfatherMnameEng = null;

	private System.String gfatherLnameEng = null;

	private System.String gfatherFullNameEng = null;

	private System.String gfatherFnameLoc = null;

	private System.String gfatherMnameLoc = null;

	private System.String gfatherLnameLoc = null;

	private System.String gfatherFullnameLoc = null;

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

	[Column(Name = "GFATHER_FNAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GfatherFnameEng 
	{ 
		get{return gfatherFnameEng;}
		set{gfatherFnameEng = value;} 
	}

	[Column(Name = "GFATHER_MNAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GfatherMnameEng 
	{ 
		get{return gfatherMnameEng;}
		set{gfatherMnameEng = value;} 
	}

	[Column(Name = "GFATHER_LNAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GfatherLnameEng 
	{ 
		get{return gfatherLnameEng;}
		set{gfatherLnameEng = value;} 
	}

	[Column(Name = "GFATHER_FULL_NAME_ENG", IsKey = false, SequenceName = "")]
	public System.String GfatherFullNameEng 
	{ 
		get{return gfatherFullNameEng;}
		set{gfatherFullNameEng = value;} 
	}

	[Column(Name = "GFATHER_FNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GfatherFnameLoc 
	{ 
		get{return gfatherFnameLoc;}
		set{gfatherFnameLoc = value;} 
	}

	[Column(Name = "GFATHER_MNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GfatherMnameLoc 
	{ 
		get{return gfatherMnameLoc;}
		set{gfatherMnameLoc = value;} 
	}

	[Column(Name = "GFATHER_LNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GfatherLnameLoc 
	{ 
		get{return gfatherLnameLoc;}
		set{gfatherLnameLoc = value;} 
	}

	[Column(Name = "GFATHER_FULLNAME_LOC", IsKey = false, SequenceName = "")]
	public System.String GfatherFullnameLoc 
	{ 
		get{return gfatherFullnameLoc;}
		set{gfatherFullnameLoc = value;} 
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

	public MigNhrsGfatherDtlInfo()
	{}
}