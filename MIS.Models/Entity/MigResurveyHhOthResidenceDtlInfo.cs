using System;
using EntityFramework;

[Table(Name = "RES_HOWNER_OTH_RESIDEN_DTL")]
public class MigResurveyHhOthResidenceDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.Decimal? otherResidenceId = null;

	private System.Decimal? otherDistrictCd = null;

	private System.Decimal? otherVdcMunCd = null;

	private System.Decimal? otherWardNo = null;

	private System.Decimal? buildingConditionCd = null;
    private System.String district = null;
    private System.String vdcname = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.String approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	private System.DateTime? updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;


	private System.Decimal? batchId = null;

    private decimal? resnforgriev { get; set; }

    private decimal? vdcold { get; set; }

    private decimal? wardold { get; set; }

    private decimal? loi_sn { get; set; }

    private decimal? grvsn { get; set; }

    private decimal? othdist { get; set; }

    private decimal? othvdc { get; set; }

    private decimal? othward { get; set; }

    private decimal? enumid { get; set; }

    private decimal? houseno { get; set; }

    private decimal? enumar { get; set; }

	[Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

    [Column(Name = "NEW_ONA_ID", IsKey = true, SequenceName = "")]
    public System.String NewONAID
    {
        get { return newONAID; }
        set { newONAID = value; }
    }

    [Column(Name = "HAOP_SN", IsKey = true, SequenceName = "")]
	public System.Decimal? OtherResidenceId 
	{ 
		get{return otherResidenceId;}
		set{otherResidenceId = value;} 
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

    [Column(Name = "BUILDING_CONDITION_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? BuildingConditionCd
    {
        get { return buildingConditionCd; }
        set { buildingConditionCd = value; }
    }

    [Column(Name = "DIST_NAME", IsKey = false, SequenceName = "")]
    public System.String DistrictName
    {
        get { return district; }
        set { district = value; }
    }

    [Column(Name = "VDC_NAME", IsKey = false, SequenceName = "")]
    public System.String VDCName
    {
        get { return vdcname; }
        set { vdcname = value; }
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
		get{return batchId;}
		set{batchId = value;} 
	}

    [Column(Name = "resn_for_griev", IsKey = false, SequenceName = "")]
    public System.Decimal? Resnforgriev
    {
        get { return resnforgriev; }
        set { resnforgriev = value; }
    }
    [Column(Name = "vdc_old", IsKey = false, SequenceName = "")]
    public System.Decimal? Vdcold
    {
        get { return vdcold; }
        set { vdcold = value; }
    }
    [Column(Name = "ward_old", IsKey = false, SequenceName = "")]
    public System.Decimal? Wardold
    {
        get { return wardold; }
        set { wardold = value; }
    }
    [Column(Name = "Loi_sn", IsKey = false, SequenceName = "")]
    public System.Decimal? Loi_sn
    {
        get { return loi_sn; }
        set { loi_sn = value; }
    }
    [Column(Name = "grv_sn", IsKey = false, SequenceName = "")]
    public System.Decimal? Grvsn
    {
        get { return grvsn; }
        set { grvsn = value; }
    }
    [Column(Name = "oth_dist", IsKey = false, SequenceName = "")]
    public System.Decimal? Othdist
    {
        get { return othdist; }
        set { othdist = value; }
    }
    [Column(Name = "oth_vdc", IsKey = false, SequenceName = "")]
    public System.Decimal? Othvdc
    {
        get { return othvdc; }
        set { othvdc = value; }
    }
    [Column(Name = "oth_ward", IsKey = false, SequenceName = "")]
    public System.Decimal? Othward
    {
        get { return othward; }
        set { othward = value; }
    }
    [Column(Name = "enum_id", IsKey = false, SequenceName = "")]
    public System.Decimal? Enumid
    {
        get { return enumid; }
        set { enumid = value; }
    }
    [Column(Name = "house_no", IsKey = false, SequenceName = "")]
    public System.Decimal? Houseno
    {
        get { return houseno; }
        set { houseno = value; }
    }
    [Column(Name = "enum_ar", IsKey = false, SequenceName = "")]
    public System.Decimal? Enumar
    {
        get { return enumar; }
        set { enumar = value; }
    }

    public MigResurveyHhOthResidenceDtlInfo()
	{}



    private string newONAID { get; set; }
}