using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_HOUSE_OWNER_MST")]
public class MigNhrsHouseOwnerMstInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.String definedCd = null;

	private System.String enumeratorId = null;

	private System.DateTime? interviewDt = null;

	private System.String interviewDtLoc = null;

	private System.String interviewStart = null;

	private System.String interviewStHh = null;

	private System.String interviewStMm = null;

	private System.String interviewStSs = null;

	private System.String interviewStMs = null;

	private System.String interviewEnd = null;

	private System.String interviewEndHh = null;

	private System.String interviewEndMm = null;

	private System.String interviewEndSs = null;

	private System.String interviewEndMs = null;

	private System.String interviewGmt = null;

	private System.String geopoint = null;

	private System.Decimal? countryCd = null;

	private System.Decimal? regStCd = null;

	private System.Decimal? zoneCd = null;

	private System.Decimal? districtCd = null;

	private System.Decimal? vdcMunCd = null;

	private System.Decimal? wardNo = null;

	private System.String enumerationArea = null;

	private System.String areaEng = null;

	private System.String areaLoc = null;

	//private System.Decimal? houseSno = null;

    private System.Decimal? houseFamilyOwnerCnt = null;

    private System.Decimal? electionCenterOHouseCnt = null;

    private System.Decimal? nonElectionCenterFHouseCnt = null;

    private System.Decimal? othElectionCenterHouseCnt = null;

	//private System.String isOwner = null;

	private System.String respondentIsHouseOwner = null;

	private System.Decimal? notInterviwingReasonCd = null;

    //private System.String anyOtherHouse = null;

    //private System.Decimal? samePlaceOtherHouseCnt = null;

   private System.Decimal? otherPlaceHouseCnt = null;

	private System.Decimal? nonresidNondamageHCnt = null;

	private System.Decimal? nonresidPartialDamageHCnt = null;

	private System.Decimal? nonresidFullDamageHCnt = null;

	private System.String socialMobilizerPresentFlag = null;

	private System.String socialMobilizerName = null;

	private System.String socialMobilizerNameLoc = null;

	private System.String imei = null;

	private System.String imsi = null;

	private System.String simNumber = null;

	private System.String mobileNumber = null;

	private System.String houseownerActive = null;

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

	private System.String nhrsUuid = null;

	private System.Decimal? batchId = null;

	private System.String errorNo = null;

	private System.String errorMsg = null;

	private System.String ipAddress = null;
    private System.String houseFamilyCount = null;
    private System.String sim = null;
    private System.String attachment = null;
    private System.String bambooDatasetdi = null;
    private System.String notes = null;
    private System.String status = null;
    private System.String submissiontime = null;
    private System.String submissionBy = null;
    private System.String tags = null;
    private System.String versionNo = null;
    private System.String xformIdString = null;
    private System.String geolocation = null;
    private System.String instanceUniqueSno = null;
    private System.String houseSno = null;
    private System.String eX_SURVEY_ID = null;
    private System.Decimal? vDC = null;

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

	[Column(Name = "INTERVIEW_DT", IsKey = false, SequenceName = "")]
	public System.DateTime? InterviewDt 
	{ 
		get{return interviewDt;}
		set{interviewDt = value;} 
	}

	[Column(Name = "INTERVIEW_DT_LOC", IsKey = false, SequenceName = "")]
	public System.String InterviewDtLoc 
	{ 
		get{return interviewDtLoc;}
		set{interviewDtLoc = value;} 
	}

	[Column(Name = "INTERVIEW_START", IsKey = false, SequenceName = "")]
	public System.String InterviewStart 
	{ 
		get{return interviewStart;}
		set{interviewStart = value;} 
	}

	[Column(Name = "INTERVIEW_ST_HH", IsKey = false, SequenceName = "")]
	public System.String InterviewStHh 
	{ 
		get{return interviewStHh;}
		set{interviewStHh = value;} 
	}

	[Column(Name = "INTERVIEW_ST_MM", IsKey = false, SequenceName = "")]
	public System.String InterviewStMm 
	{ 
		get{return interviewStMm;}
		set{interviewStMm = value;} 
	}

	[Column(Name = "INTERVIEW_ST_SS", IsKey = false, SequenceName = "")]
	public System.String InterviewStSs 
	{ 
		get{return interviewStSs;}
		set{interviewStSs = value;} 
	}

	[Column(Name = "INTERVIEW_ST_MS", IsKey = false, SequenceName = "")]
	public System.String InterviewStMs 
	{ 
		get{return interviewStMs;}
		set{interviewStMs = value;} 
	}

	[Column(Name = "INTERVIEW_END", IsKey = false, SequenceName = "")]
	public System.String InterviewEnd 
	{ 
		get{return interviewEnd;}
		set{interviewEnd = value;} 
	}

	[Column(Name = "INTERVIEW_END_HH", IsKey = false, SequenceName = "")]
	public System.String InterviewEndHh 
	{ 
		get{return interviewEndHh;}
		set{interviewEndHh = value;} 
	}

	[Column(Name = "INTERVIEW_END_MM", IsKey = false, SequenceName = "")]
	public System.String InterviewEndMm 
	{ 
		get{return interviewEndMm;}
		set{interviewEndMm = value;} 
	}

	[Column(Name = "INTERVIEW_END_SS", IsKey = false, SequenceName = "")]
	public System.String InterviewEndSs 
	{ 
		get{return interviewEndSs;}
		set{interviewEndSs = value;} 
	}

	[Column(Name = "INTERVIEW_END_MS", IsKey = false, SequenceName = "")]
	public System.String InterviewEndMs 
	{ 
		get{return interviewEndMs;}
		set{interviewEndMs = value;} 
	}

	[Column(Name = "INTERVIEW_GMT", IsKey = false, SequenceName = "")]
	public System.String InterviewGmt 
	{ 
		get{return interviewGmt;}
		set{interviewGmt = value;} 
	}

	[Column(Name = "GEOPOINT", IsKey = false, SequenceName = "")]
	public System.String Geopoint 
	{ 
		get{return geopoint;}
		set{geopoint = value;} 
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

	[Column(Name = "ENUMERATION_AREA", IsKey = false, SequenceName = "")]
	public System.String EnumerationArea 
	{ 
		get{return enumerationArea;}
		set{enumerationArea = value;} 
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

    //[Column(Name = "HOUSE_SNO", IsKey = false, SequenceName = "")]
    //public System.Decimal? HouseSno 
    //{ 
    //    get{return houseSno;}
    //    set{houseSno = value;} 
    //}

	[Column(Name = "HOUSE_FAMILY_OWNER_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? HouseFamilyOwnerCnt 
	{
        get { return houseFamilyOwnerCnt; }
        set { houseFamilyOwnerCnt = value; } 
	}

    [Column(Name = "ELECTIONCENTER_OHOUSE_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? ElectionCenterOHouseCnt
    {
        get { return electionCenterOHouseCnt; }
        set { electionCenterOHouseCnt = value; }
    }

    [Column(Name = "NONELECTIONCENTER_FHOUSE_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? NonElectionCenterFHouseCnt
    {
        get { return nonElectionCenterFHouseCnt; }
        set { nonElectionCenterFHouseCnt = value; }
    }

    //[Column(Name = "IS_OWNER", IsKey = false, SequenceName = "")]
    //public System.String IsOwner 
    //{ 
    //    get{return isOwner;}
    //    set{isOwner = value;} 
    //}

	[Column(Name = "RESPONDENT_IS_HOUSE_OWNER", IsKey = false, SequenceName = "")]
	public System.String RespondentIsHouseOwner 
	{ 
		get{return respondentIsHouseOwner;}
		set{respondentIsHouseOwner = value;} 
	}

	[Column(Name = "NOT_INTERVIWING_REASON_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? NotInterviwingReasonCd 
	{ 
		get{return notInterviwingReasonCd;}
		set{notInterviwingReasonCd = value;} 
	}

    //[Column(Name = "ANY_OTHER_HOUSE", IsKey = false, SequenceName = "")]
    //public System.String AnyOtherHouse 
    //{ 
    //    get{return anyOtherHouse;}
    //    set{anyOtherHouse = value;} 
    //}

    //[Column(Name = "SAME_PLACE_OTHER_HOUSE_CNT", IsKey = false, SequenceName = "")]
    //public System.Decimal? SamePlaceOtherHouseCnt 
    //{ 
    //    get{return samePlaceOtherHouseCnt;}
    //    set{samePlaceOtherHouseCnt = value;} 
    //}

    [Column(Name = "OTHER_PLACE_HOUSE_CNT", IsKey = false, SequenceName = "")]
    public System.Decimal? OtherPlaceHouseCnt
    {
        get { return otherPlaceHouseCnt; }
        set { otherPlaceHouseCnt = value; }
    }

	[Column(Name = "NONRESID_NONDAMAGE_CNT", IsKey = false, SequenceName = "")]
	public System.Decimal? NonresidNondamageHCnt 
	{ 
		get{return nonresidNondamageHCnt;}
		set{nonresidNondamageHCnt = value;} 
	}

	[Column(Name = "NONRESID_PARTIAL_DAMAGE_CNT", IsKey = false, SequenceName = "")]
	public System.Decimal? NonresidPartialDamageHCnt 
	{ 
		get{return nonresidPartialDamageHCnt;}
		set{nonresidPartialDamageHCnt = value;} 
	}

	[Column(Name = "NONRESID_FULL_DAMAGE_CNT", IsKey = false, SequenceName = "")]
	public System.Decimal? NonresidFullDamageHCnt 
	{ 
		get{return nonresidFullDamageHCnt;}
		set{nonresidFullDamageHCnt = value;} 
	}

	[Column(Name = "SOCIAL_MOBILIZER_PRESENT", IsKey = false, SequenceName = "")]
	public System.String SocialMobilizerPresentFlag 
	{ 
		get{return socialMobilizerPresentFlag;}
		set{socialMobilizerPresentFlag = value;} 
	}

	[Column(Name = "SOCIAL_MOBILIZER_NAME", IsKey = false, SequenceName = "")]
	public System.String SocialMobilizerName 
	{ 
		get{return socialMobilizerName;}
		set{socialMobilizerName = value;} 
	}

	[Column(Name = "SOCIAL_MOBILIZER_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String SocialMobilizerNameLoc 
	{ 
		get{return socialMobilizerNameLoc;}
		set{socialMobilizerNameLoc = value;} 
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

	[Column(Name = "SIM_NUMBER", IsKey = false, SequenceName = "")]
	public System.String SimNumber 
	{ 
		get{return simNumber;}
		set{simNumber = value;} 
	}

	[Column(Name = "MOBILE_NUMBER", IsKey = false, SequenceName = "")]
	public System.String MobileNumber 
	{ 
		get{return mobileNumber;}
		set{mobileNumber = value;} 
	}

	[Column(Name = "HOUSEOWNER_ACTIVE", IsKey = false, SequenceName = "")]
	public System.String HouseownerActive 
	{ 
		get{return houseownerActive;}
		set{houseownerActive = value;} 
	}

    [Column(Name = "HOUSE_FAMILY_CNT", IsKey = false, SequenceName = "")]
    public System.String HouseFamilyCount
    {
        get { return houseFamilyCount; }
        set { houseFamilyCount = value; }
    }


    [Column(Name = "VDC", IsKey = false, SequenceName = "")]
    public System.Decimal? VDC
    {
        get { return vDC; }
        set { vDC = value; }
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

    [Column(Name = "SIM", IsKey = false, SequenceName = "")]
    public System.String Sim
    {
        get { return sim; }
        set { sim = value; }
    }

    [Column(Name = "ATTACHMENTS", IsKey = false, SequenceName = "")]
    public System.String Attachment
    {
        get { return attachment; }
        set { attachment = value; }
    }

    [Column(Name = "BAMBOO_DATASETDI", IsKey = false, SequenceName = "")]
    public System.String BambooDatasetdi
    {
        get { return bambooDatasetdi; }
        set { bambooDatasetdi = value; }
    }

    [Column(Name = "NOTES", IsKey = false, SequenceName = "")]
    public System.String Notes
    {
        get { return notes; }
        set { notes = value; }
    }

    [Column(Name = "STATUS", IsKey = false, SequenceName = "")]
    public System.String Status
    {
        get { return status; }
        set { status = value; }
    }

    [Column(Name = "SUBMISSIONTIME", IsKey = false, SequenceName = "")]
    public System.String Submissiontime
    {
        get { return submissiontime; }
        set { submissiontime = value; }
    }

    [Column(Name = "SUBMISSION_BY", IsKey = false, SequenceName = "")]
    public System.String SubmissionBy
    {
        get { return submissionBy; }
        set { submissionBy = value; }
    }

    [Column(Name = "TAGS", IsKey = false, SequenceName = "")]
    public System.String Tags
    {
        get { return tags; }
        set { tags = value; }
    }

    [Column(Name = "VERSION_NO", IsKey = false, SequenceName = "")]
    public System.String VersionNo
    {
        get { return versionNo; }
        set { versionNo = value; }
    }

    [Column(Name = "XFORMID_STRING", IsKey = false, SequenceName = "")]
    public System.String XformIdString
    {
        get { return xformIdString; }
        set { xformIdString = value; }
    }

    [Column(Name = "GEOLOCATION", IsKey = false, SequenceName = "")]
    public System.String Geolocation
    {
        get { return geolocation; }
        set { geolocation = value; }
    }


    [Column(Name = "INSTANCE_UNIQUE_SNO", IsKey = false, SequenceName = "")]
    public System.String InstanceUniqueSno
    {
        get { return instanceUniqueSno; }
        set { instanceUniqueSno = value; }
    }
    //HOUSE_SNO
    [Column(Name = "HOUSE_SNO", IsKey = false, SequenceName = "")]
    public System.String HOUSE_SNO
    {
        get { return houseSno; }
        set { houseSno = value; }
    }
    

    [Column(Name = "EX_SURVEY_ID", IsKey = false, SequenceName = "")]
    public String EX_SURVEY_ID
    {
        get { return eX_SURVEY_ID; }
        set { eX_SURVEY_ID = value; }
    }
	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigNhrsHouseOwnerMstInfo()
	{}

   
}