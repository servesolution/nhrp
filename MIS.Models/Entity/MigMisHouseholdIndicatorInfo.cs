using System;
using EntityFramework;

[Table(Name = "MIG_MIS_HOUSEHOLD_INDICATOR")]
public class MigMisHouseholdIndicatorInfo : EntityBase
{

	private System.Decimal? indicatorCd = null;

	private System.String descEng = null;

	private System.String descLoc = null;

	private System.Decimal? orderNo = null;

	private System.String enteredBy = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String definedCd = null;

	private System.String ipAddress = null;

	[Column(Name = "INDICATOR_CD", IsKey = true, SequenceName = "")]
	public System.Decimal? IndicatorCd 
	{ 
		get{return indicatorCd;}
		set{indicatorCd = value;} 
	}

	[Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
	public System.String DescEng 
	{ 
		get{return descEng;}
		set{descEng = value;} 
	}

	[Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
	public System.String DescLoc 
	{ 
		get{return descLoc;}
		set{descLoc = value;} 
	}

	[Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? OrderNo 
	{ 
		get{return orderNo;}
		set{orderNo = value;} 
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

	[Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
	public System.String DefinedCd 
	{ 
		get{return definedCd;}
		set{definedCd = value;} 
	}


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MigMisHouseholdIndicatorInfo()
	{}
}