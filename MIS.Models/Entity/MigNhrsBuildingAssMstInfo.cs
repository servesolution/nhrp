using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_BUILDING_ASS_MST")]
public class MigNhrsBuildingAssMstInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String buildingStructureNo = null;

    //private System.Decimal? familyCntBefore = null;

    //private System.Decimal? familyCntAfter = null;

    private System.Decimal? houselandlegalowner = null;

	private System.Decimal? buildingConditionCd = null;

	private System.Decimal? storeysCntBefore = null;

	private System.Decimal? storeysCntAfter = null;

	private System.Decimal? houseAge = null;

	private System.Decimal? plinthAreaCd = null;

	private System.String houseHeightBeforeEQ = null;

    private System.String houseHeightAfterEQ = null;

	private System.Decimal? groundSurfaceCd = null;

	private System.Decimal? foundationTypeCd = null;

	private System.Decimal? rcMaterialCd = null;

	private System.Decimal? fcMaterialCd = null;

	private System.Decimal? scMaterialCd = null;

	//private System.Decimal? superstructureMaterialCd = null;

    private System.Decimal? householdcntaftereq = null;

	private System.Decimal? buildingPositionCd = null;

	private System.Decimal? buildingPlanCd = null;

	private System.Decimal? assessedAreaCd = null;

	private System.Decimal? techsolutionCd = null;

    private System.Decimal? damageGradeCd = null;

	private System.String techsolutionComment = null;

	private System.String techsolutionCommentLoc = null;

	private System.String reconstructionStarted = null;

    private System.String isSecondaryUse = null;

	private System.String isGeotechnicalRisk = null;

    //private System.Decimal? geotechnicalRiskTypeCd = null;

	//private System.Decimal? householdCnt = null;

    private System.Decimal? districtCd = null;

    private System.String latitude = null;

    private System.String longitude = null;

    private System.String altitude = null;

    private System.String accuracy = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	//private System.String approvedByLoc = null;

	private System.DateTime? approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	//private System.String updatedByLoc = null;

	private System.DateTime? updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	//private System.String enteredByLoc = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

    private System.String generalComments = null;

    private System.String generalCommentsLoc = null;

    private System.String hoDefinedCd = null; 

    private System.String instanceUniqueSno = null;
    private System.Decimal? vdcMunCd = null;
    private System.Decimal? wardNo = null;

	private System.String ipAddress = null;

	[Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}

	

	[Column(Name = "BUILDING_STRUCTURE_NO", IsKey = true, SequenceName = "")]
	public System.String BuildingStructureNo 
	{ 
		get{return buildingStructureNo;}
		set{buildingStructureNo = value;} 
	}

    [Column(Name = "HOUSE_LAND_LEGAL_OWNER", IsKey = false, SequenceName = "")]
    public System.Decimal? Houselandlegalowner
    {
        get { return houselandlegalowner; }
        set { houselandlegalowner = value; }
    }

    //[Column(Name = "FAMILY_CNT_BEFORE", IsKey = false, SequenceName = "")]
    //public System.Decimal? FamilyCntBefore 
    //{ 
    //    get{return familyCntBefore;}
    //    set{familyCntBefore = value;} 
    //}

    //[Column(Name = "FAMILY_CNT_AFTER", IsKey = false, SequenceName = "")]
    //public System.Decimal? FamilyCntAfter 
    //{ 
    //    get{return familyCntAfter;}
    //    set{familyCntAfter = value;} 
    //}

    [Column(Name = "BUILDING_CONDITION_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? BuildingConditionCd
    {
        get { return buildingConditionCd; }
        set { buildingConditionCd = value; }
    }

    [Column(Name = "STOREYS_CNT_BEFORE", IsKey = false, SequenceName = "")]
    public System.Decimal? StoreysCntBefore
    {
        get { return storeysCntBefore; }
        set { storeysCntBefore = value; }
    }



    [Column(Name = "STOREYS_CNT_AFTER", IsKey = false, SequenceName = "")]
    public System.Decimal? StoreysCntAfter
    {
        get { return storeysCntAfter; }
        set { storeysCntAfter = value; }
    }

    [Column(Name = "HOUSE_AGE", IsKey = false, SequenceName = "")]
    public System.Decimal? HouseAge
    {
        get { return houseAge; }
        set { houseAge = value; }
    }
   

    //[Column(Name = "HOUSE_LAND_LEGAL_OWNER", IsKey = false, SequenceName = "")]
    //public System.Decimal? Houselandlegalowner
    //{
    //    get { return houselandlegalowner; }
    //    set { houselandlegalowner = value; }
    //}


	[Column(Name = "PLINTH_AREA", IsKey = false, SequenceName = "")]
	public System.Decimal? PlinthAreaCd 
	{ 
		get{return plinthAreaCd;}
		set{plinthAreaCd = value;} 
	}

    [Column(Name = "HOUSE_HEIGHT_BEFORE_EQ", IsKey = false, SequenceName = "")]
	public System.String HouseHeightBeforeEQ 
	{ 
		get{return houseHeightBeforeEQ;}
		set{houseHeightBeforeEQ = value;} 
	}

    [Column(Name = "HOUSE_HEIGHT_AFTER_EQ", IsKey = false, SequenceName = "")]
    public System.String HouseHeightAfterEQ
    {
        get { return houseHeightAfterEQ; }
        set { houseHeightAfterEQ = value; }
    }

	[Column(Name = "GROUND_SURFACE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? GroundSurfaceCd 
	{ 
		get{return groundSurfaceCd;}
		set{groundSurfaceCd = value;} 
	}

	[Column(Name = "FOUNDATION_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? FoundationTypeCd 
	{ 
		get{return foundationTypeCd;}
		set{foundationTypeCd = value;} 
	}

	[Column(Name = "RC_MATERIAL_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? RcMaterialCd 
	{ 
		get{return rcMaterialCd;}
		set{rcMaterialCd = value;} 
	}

	[Column(Name = "FC_MATERIAL_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? FcMaterialCd 
	{ 
		get{return fcMaterialCd;}
		set{fcMaterialCd = value;} 
	}

	[Column(Name = "SC_MATERIAL_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? ScMaterialCd 
	{ 
		get{return scMaterialCd;}
		set{scMaterialCd = value;} 
	}

    //[Column(Name = "SUPERSTRUCTURE_MATERIAL_CD", IsKey = false, SequenceName = "")]
    //public System.Decimal? SuperstructureMaterialCd 
    //{ 
    //    get{return superstructureMaterialCd;}
    //    set{superstructureMaterialCd = value;} 
    //}

	[Column(Name = "BUILDING_POSITION_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? BuildingPositionCd 
	{ 
		get{return buildingPositionCd;}
		set{buildingPositionCd = value;} 
	}

	[Column(Name = "BUILDING_PLAN_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? BuildingPlanCd 
	{ 
		get{return buildingPlanCd;}
		set{buildingPlanCd = value;} 
	}

    [Column(Name = "IS_GEOTECHNICAL_RISK", IsKey = false, SequenceName = "")]
    public System.String IsGeotechnicalRisk
    {
        get { return isGeotechnicalRisk; }
        set { isGeotechnicalRisk = value; }
    }

	[Column(Name = "ASSESSED_AREA_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? AssessedAreaCd 
	{ 
		get{return assessedAreaCd;}
		set{assessedAreaCd = value;} 
	}

	

    [Column(Name = "DAMAGE_GRADE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? DamageGradeCd
    {
        get { return damageGradeCd; }
        set { damageGradeCd = value; }
    }

    [Column(Name = "TECHSOLUTION_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? TechsolutionCd
    {
        get { return techsolutionCd; }
        set { techsolutionCd = value; }
    }

	[Column(Name = "TECHSOLUTION_COMMENT", IsKey = false, SequenceName = "")]
	public System.String TechsolutionComment 
	{ 
		get{return techsolutionComment;}
		set{techsolutionComment = value;} 
	}

	[Column(Name = "TECHSOLUTION_COMMENT_LOC", IsKey = false, SequenceName = "")]
	public System.String TechsolutionCommentLoc 
	{ 
		get{return techsolutionCommentLoc;}
		set{techsolutionCommentLoc = value;} 
	}

	[Column(Name = "RECONSTRUCTION_STARTED", IsKey = false, SequenceName = "")]
	public System.String ReconstructionStarted 
	{ 
		get{return reconstructionStarted;}
		set{reconstructionStarted = value;} 
	}

    [Column(Name = "IS_SECONDARY_USE", IsKey = false, SequenceName = "")]
    public System.String IsSecondaryUse
    {
        get { return isSecondaryUse; }
        set { isSecondaryUse = value; }
    }

	

    //[Column(Name = "GEOTECHNICAL_RISK_TYPE_CD", IsKey = false, SequenceName = "")]
    //public System.Decimal? GeotechnicalRiskTypeCd 
    //{ 
    //    get{return geotechnicalRiskTypeCd;}
    //    set{geotechnicalRiskTypeCd = value;} 
    //}

    //[Column(Name = "HOUSEHOLD_CNT", IsKey = false, SequenceName = "")]
    //public System.Decimal? HouseholdCnt 
    //{ 
    //    get{return householdCnt;}
    //    set{householdCnt = value;} 
    //}

    [Column(Name = "DISTRICT_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? DistrictCd 
	{ 
		get{return districtCd;}
		set{districtCd = value;} 
	}

    [Column(Name = "LATITUDE", IsKey = false, SequenceName = "")]
    public System.String Latitude
    {
        get { return latitude; }
        set { latitude = value; }
    }

    [Column(Name = "LONGITUDE", IsKey = false, SequenceName = "")]
    public System.String Longitude
    {
        get { return longitude; }
        set { longitude = value; }
    }

    [Column(Name = "ALTITUDE", IsKey = false, SequenceName = "")]
    public System.String Altitude
    {
        get { return altitude; }
        set { altitude = value; }
    }

    [Column(Name = "ACCURACY", IsKey = false, SequenceName = "")]
    public System.String Accuracy
    {
        get { return accuracy; }
        set { accuracy = value; }
    }

    [Column(Name = "HOUSEHOLD_CNT_AFTER_EQ", IsKey = false, SequenceName = "")]
    public System.Decimal? Householdcntaftereq
    {
        get { return householdcntaftereq; }
        set { householdcntaftereq = value; }
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

    //[Column(Name = "APPROVED_BY_LOC", IsKey = false, SequenceName = "")]
    //public System.String ApprovedByLoc 
    //{ 
    //    get{return approvedByLoc;}
    //    set{approvedByLoc = value;} 
    //}

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

    //[Column(Name = "UPDATED_BY_LOC", IsKey = false, SequenceName = "")]
    //public System.String UpdatedByLoc 
    //{ 
    //    get{return updatedByLoc;}
    //    set{updatedByLoc = value;} 
    //}

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

    //[Column(Name = "ENTERED_BY_LOC", IsKey = false, SequenceName = "")]
    //public System.String EnteredByLoc 
    //{ 
    //    get{return enteredByLoc;}
    //    set{enteredByLoc = value;} 
    //}

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

    [Column(Name = "GENERAL_COMMENTS", IsKey = false, SequenceName = "")]
    public System.String GeneralComments
    {
        get { return generalComments; }
        set { generalComments = value; }
    }

    [Column(Name = "GENERAL_COMMENTS_LOC", IsKey = false, SequenceName = "")]
    public System.String GeneralCommentsLoc
    {
        get { return generalCommentsLoc; }
        set { generalCommentsLoc = value; }
    }

    [Column(Name = "HO_DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String HoDefinedCd
    {
        get { return hoDefinedCd; }
        set { hoDefinedCd = value; }
    }

    [Column(Name = "INSTANCE_UNIQUE_SNO", IsKey = false, SequenceName = "")]
    public System.String InstanceUniqueSno
    {
        get { return instanceUniqueSno; }
        set { instanceUniqueSno = value; }
    }


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }


    [Column(Name = "VDC_MUN_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? VDC_MUN_CD
    {
        get { return vdcMunCd; }
        set { vdcMunCd = value; }
    }

    [Column(Name = "WARD_NO", IsKey = false, SequenceName = "")]
    public System.Decimal? WARD_NO
    {
        get { return wardNo; }
        set { wardNo = value; }
    }

	public MigNhrsBuildingAssMstInfo()
	{}
}