using System;
using EntityFramework;

[Table(Name = "RES_NHRS_HOUSE_OWNER_MST")]
public class MigResurveyHouseOwnerMstInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String definedCd = null;

	private System.String enumeratorId = null;

	private System.String interviewDt = null;
	private System.String interviewStart = null;
    private System.String interviewEnd = null;

	private System.Decimal? districtCd = null;

	private System.Decimal? vdcMunCd = null;

	private System.Decimal? wardNo = null;

	private System.String enumerationArea = null;

	private System.String areaEng = null;

	private System.String areaLoc = null;

    private System.Decimal? houseFamilyOwnerCnt = null;

    private System.String reasonforgrievance = null;
    private System.String hasoldslip = null;
    private System.String oldslipphoto = null;
    private System.String reasonforleft = null;
    private System.String otherreason = null;
    private System.Decimal? vdcold = null;
    private System.Decimal? wardold = null;

	private System.String imei = null;

	private System.String imsi = null;

	private System.String slipnumber = null;

	private System.String mobileNumber = null;

	private System.String remarks = null;
    private System.String comments = null;

	private System.String remarksLoc = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.String approvedByLoc = null;

	private System.String approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	private System.String updatedByLoc = null;

	private System.String updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	private System.String enteredByLoc = null;

	private System.String enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String ipAddress = null;
    private System.String houseFamilyCount = null;
    private System.String sim = null;
    private System.String simtel = null;
    private System.String submissiontime = null;
    private System.String submissionDate = null;
    private System.String houseSno = null;
    private System.String districtname = null;
    private System.String vdcname = null;
    private System.String oldvdcname = null;

	[Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

	[Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
	public System.String DefinedCd 
	{ 
		get{return definedCd;}
		set{definedCd = value;} 
	}

	[Column(Name = "ENUMERATOR_ID", IsKey = false, SequenceName = "")]
	public System.String EnumeratorId 
	{ 
		get{return enumeratorId;}
		set{enumeratorId = value;} 
	}

	[Column(Name = "TODAY", IsKey = false, SequenceName = "")]
	public System.String InterviewDt 
	{ 
		get{return interviewDt;}
		set{interviewDt = value;} 
	}

	
	[Column(Name = "ST_TIME", IsKey = false, SequenceName = "")]
	public System.String InterviewStart 
	{ 
		get{return interviewStart;}
		set{interviewStart = value;} 
	}

	
	[Column(Name = "EN_TIME", IsKey = false, SequenceName = "")]
	public System.String InterviewEnd 
	{ 
		get{return interviewEnd;}
		set{interviewEnd = value;} 
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

	[Column(Name = "ENUMERATION_AREA", IsKey = false, SequenceName = "")]
	public System.String EnumerationArea 
	{ 
		get{return enumerationArea;}
		set{enumerationArea = value;} 
	}

	[Column(Name = "TOLE", IsKey = false, SequenceName = "")]
	public System.String AreaEng 
	{ 
		get{return areaEng;}
		set{areaEng = value;} 
	}

	[Column(Name = "OWN_NUM", IsKey = false, SequenceName = "")]
    public System.Decimal? HouseFamilyOwnerCnt 
	{
        get { return houseFamilyOwnerCnt; }
        set { houseFamilyOwnerCnt = value; } 
	}

	[Column(Name = "IMEI", IsKey = false, SequenceName = "")]
	public System.String Imei 
	{ 
		get{return imei;}
		set{imei = value;} 
	}

	[Column(Name = "IMSI", IsKey = false, SequenceName = "")]
	public System.String Imsi 
	{ 
		get{return imsi;}
		set{imsi = value;} 
	}

	[Column(Name = "SIM_TEL", IsKey = false, SequenceName = "")]
	public System.String SimNumber 
	{ 
		get{return simtel;}
        set { simtel = value; } 
	}
    public System.String HouseFamilyCount
    {
        get { return houseFamilyCount; }
        set { houseFamilyCount = value; }
    }

    [Column(Name = "RESN_FOR_GRIEV", IsKey = false, SequenceName = "")]
    public System.String ReasonForGrievance
    {
        get { return reasonforgrievance; }
        set { reasonforgrievance = value; }
    }

    [Column(Name = "HAS_OLD_IDSLIP", IsKey = false, SequenceName = "")]
    public System.String HasOldSlip
    {
        get { return hasoldslip; }
        set { hasoldslip = value; }
    }
    [Column(Name = "OLD_IDSLIP_PHOTO", IsKey = false, SequenceName = "")]
    public System.String OldSlipPhoto
    {
        get { return oldslipphoto; }
        set { oldslipphoto = value; }
    }

    [Column(Name = "RESN_FOR_LEFT", IsKey = false, SequenceName = "")]
    public System.String ReasonForLeft
    {
        get { return reasonforleft; }
        set { reasonforleft = value; }
    }
    [Column(Name = "RESN_OTH", IsKey = false, SequenceName = "")]
    public System.String OtherReason
    {
        get { return otherreason; }
        set { otherreason = value; }
    }
    [Column(Name = "VDC_OLD", IsKey = false, SequenceName = "")]
    public System.Decimal? VDCOLD
    {
        get { return vdcold; }
        set { vdcold = value; }
    }
    [Column(Name = "WARD_OLD", IsKey = false, SequenceName = "")]
    public System.Decimal? WardOld
    {
        get { return wardold; }
        set { wardold = value; }
    }
    [Column(Name = "COMMENTS", IsKey = false, SequenceName = "")]
    public System.String Comments
    {
        get { return comments; }
        set { comments = value; }
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
        get { return remarksLoc; }
        set { remarksLoc = value; }
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
	public System.String ApprovedDt 
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
	public System.String UpdatedDt 
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
	public System.String EnteredDt 
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

	[Column(Name = "UUID", IsKey = false, SequenceName = "")]
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


    [Column(Name = "SIM", IsKey = false, SequenceName = "")]
    public System.String Sim
    {
        get { return sim; }
        set { sim = value; }
    }
    [Column(Name = "DISTRICT_NAME", IsKey = false, SequenceName = "")]
    public System.String DistrictName
    {
        get { return districtname; }
        set { districtname = value; }
    }
    [Column(Name = "VDC_NAME", IsKey = false, SequenceName = "")]
    public System.String VdcName
    {
        get { return vdcname; }
        set { vdcname = value; }
    }
    [Column(Name = "OLD_VDC_NAME", IsKey = false, SequenceName = "")]
    public System.String OldVdcName
    {
        get { return oldvdcname; }
        set { oldvdcname = value; }
    }
    [Column(Name = "SUB_TIME", IsKey = false, SequenceName = "")]
    public System.String SubmissionTime
    {
        get { return submissiontime; }
        set { submissiontime = value; }
    }
    [Column(Name = "SUB_DATE", IsKey = false, SequenceName = "")]
    public System.String SubmissionDate
    {
        get { return submissionDate; }
        set { submissionDate = value; }
    }

    [Column(Name = "HOUSE_SNO", IsKey = false, SequenceName = "")]
    public System.String HOUSE_SNO
    {
        get { return houseSno; }
        set { houseSno = value; }
    }
   
    //[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    //public String IPAddress
    //{
    //    get { return ipAddress; }
    //    set { ipAddress = value; }
    //}

    public MigResurveyHouseOwnerMstInfo()
	{}

   
}