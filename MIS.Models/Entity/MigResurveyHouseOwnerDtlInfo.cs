using System;
using EntityFramework;

[Table(Name = "RES_NHRS_HOUSE_OWNER_DTL")]
public class MigResurveyHouseOwnerDtlInfo : EntityBase
{

	private System.String houseOwnerId = null;

	private System.Decimal? ownersn = null;

	private System.String firstNameEng = null;

	private System.String middleNameEng = null;

	private System.String lastNameEng = null;

	private System.String fullNameEng = null;

	private System.Decimal? genderCd = null;

	private System.String householdinbeneficiary = null;
    private System.Decimal? oldslipnumber = null;
    private System.Decimal? grievanceUniqueID = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.String approvedByLoc = null;

	private System.String approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String updatedBy = null;

	private System.String updatedByLoc = null;

	private System.DateTime? updatedDt = null;

	private System.String updatedDtLoc = null;

	private System.String enteredBy = null;

	private System.String enteredByLoc = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.Decimal? batchId = null;



	[Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
	public System.String HouseOwnerId 
	{ 
		get{return houseOwnerId;}
		set{houseOwnerId = value;} 
	}
    [Column(Name = "OWNER_SNO", IsKey = true, SequenceName = "")]
    public System.Decimal? OWnerSno
    {
        get { return ownersn; }
        set { ownersn = value; }
    }



    [Column(Name = "HH_OWNER_FIRST_NAME", IsKey = false, SequenceName = "")]
	public System.String FirstNameEng 
	{ 
		get{return firstNameEng;}
		set{firstNameEng = value;} 
	}

    [Column(Name = "HH_OWNER_MIDDLE_NAME", IsKey = false, SequenceName = "")]
	public System.String MiddleNameEng 
	{ 
		get{return middleNameEng;}
		set{middleNameEng = value;} 
	}

    [Column(Name = "HH_OWNER_LAST_NAME", IsKey = false, SequenceName = "")]
	public System.String LastNameEng 
	{ 
		get{return lastNameEng;}
		set{lastNameEng = value;} 
	}

    [Column(Name = "HH_OWNER_FULL_NAME", IsKey = false, SequenceName = "")]
	public System.String FullNameEng 
	{ 
		get{return fullNameEng;}
		set{fullNameEng = value;} 
	}

	[Column(Name = "GENDER_CD", IsKey = false, SequenceName = "")]
	public System.Decimal? GenderCd 
	{ 
		get{return genderCd;}
		set{genderCd = value;} 
	}
    [Column(Name = "HH_IN_BENEFICIARY", IsKey = false, SequenceName = "")]
    public System.String HouseholdinBeneficary
    {
        get { return householdinbeneficiary; }
        set { householdinbeneficiary = value; }
    }
    [Column(Name = "HH_IDENTITY_SLIP_NO", IsKey = false, SequenceName = "")]
    public System.Decimal? HouseholdSlipNumber
    {
        get { return oldslipnumber; }
        set { oldslipnumber = value; }
    }
    [Column(Name = "HH_GID", IsKey = false, SequenceName = "")]
    public System.Decimal? GrievanceUniqueID
    {
        get { return grievanceUniqueID; }
        set { grievanceUniqueID = value; }
    }
    [Column(Name = "GENDER_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? HH
    {
        get { return genderCd; }
        set { genderCd = value; }
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

	public MigResurveyHouseOwnerDtlInfo()
	{}
}