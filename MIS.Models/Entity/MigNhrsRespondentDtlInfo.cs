using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_RESPONDENT_DTL")]
public class MigNhrsRespondentDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.Decimal? respondentSno = null;

	private System.String respondentFirstName = null;

	private System.String respondentMiddleName = null;

	private System.String respondentLastName = null;

	private System.String respondentFullName = null;

	private System.String respondentFirstNameLoc = null;

	private System.String respondentMiddleNameLoc = null;

	private System.String respondentLastNameLoc = null;

	private System.String respondentFullNameLoc = null;

	private System.String respondentPhoto = null;

	private System.Decimal? respondentGenderCd = null;

	private System.Decimal? hhRelationTypeCd = null;

    private System.String otherRelationType = null;

    private System.String otherRelationTypeLoc = null;

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

	[Column(Name = "RESPONDENT_SNO", IsKey = true, SequenceName = "")]
	public System.Decimal? RespondentSno 
	{ 
		get{return respondentSno;}
		set{respondentSno = value;} 
	}

	[Column(Name = "RESPONDENT_FIRST_NAME", IsKey = false, SequenceName = "")]
	public System.String RespondentFirstName 
	{ 
		get{return respondentFirstName;}
		set{respondentFirstName = value;} 
	}

	[Column(Name = "RESPONDENT_MIDDLE_NAME", IsKey = false, SequenceName = "")]
	public System.String RespondentMiddleName 
	{ 
		get{return respondentMiddleName;}
		set{respondentMiddleName = value;} 
	}

	[Column(Name = "RESPONDENT_LAST_NAME", IsKey = false, SequenceName = "")]
	public System.String RespondentLastName 
	{ 
		get{return respondentLastName;}
		set{respondentLastName = value;} 
	}

	[Column(Name = "RESPONDENT_FULL_NAME", IsKey = false, SequenceName = "")]
	public System.String RespondentFullName 
	{ 
		get{return respondentFullName;}
		set{respondentFullName = value;} 
	}

	[Column(Name = "RESPONDENT_FIRST_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String RespondentFirstNameLoc 
	{ 
		get{return respondentFirstNameLoc;}
		set{respondentFirstNameLoc = value;} 
	}

	[Column(Name = "RESPONDENT_MIDDLE_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String RespondentMiddleNameLoc 
	{ 
		get{return respondentMiddleNameLoc;}
		set{respondentMiddleNameLoc = value;} 
	}

	[Column(Name = "RESPONDENT_LAST_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String RespondentLastNameLoc 
	{ 
		get{return respondentLastNameLoc;}
		set{respondentLastNameLoc = value;} 
	}

	[Column(Name = "RESPONDENT_FULL_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String RespondentFullNameLoc 
	{ 
		get{return respondentFullNameLoc;}
		set{respondentFullNameLoc = value;} 
	}

	[Column(Name = "RESPONDENT_PHOTO", IsKey = false, SequenceName = "")]
	public System.String RespondentPhoto 
	{ 
		get{return respondentPhoto;}
		set{respondentPhoto = value;} 
	}

	[Column(Name = "RESPONDENT_GENDER_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? RespondentGenderCd 
	{ 
		get{return respondentGenderCd;}
		set{respondentGenderCd = value;} 
	}

	[Column(Name = "HH_RELATION_TYPE_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? HhRelationTypeCd 
	{ 
		get{return hhRelationTypeCd;}
		set{hhRelationTypeCd = value;} 
	}

    [Column(Name = "OTHER_RELATION_TYPE", IsKey = false, SequenceName = "")]
    public System.String OtherRelationType
    {
        get { return otherRelationType; }
        set { otherRelationType = value; }
    }

    [Column(Name = "OTHER_RELATION_TYPE_LOC", IsKey = false, SequenceName = "")]
    public System.String OtherRelationTypeLoc
    {
        get { return otherRelationTypeLoc; }
        set { otherRelationTypeLoc = value; }
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

	public MigNhrsRespondentDtlInfo()
	{}
}