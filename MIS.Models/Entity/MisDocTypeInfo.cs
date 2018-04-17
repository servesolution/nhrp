using System;
using EntityFramework;

[Table(Name = "MIS_DOC_TYPE")]
public class MisDocTypeInfo : EntityBase
{

	private System.Decimal? docTypeCd = null;

	private System.String definedCd = null;

	private System.String descEng = null;

	private System.String descLoc = null;

	private System.String shortName = null;

	private System.String shortNameLoc = null;

	private System.String disabled = null;

	private System.Decimal? orderNo = null;

	private System.String approved = null;

	private System.String approvedBy = null;

	private System.DateTime? approvedDt = null;

	private System.String approvedDtLoc = null;

	private System.String enteredBy = null;

	private System.DateTime? enteredDt = null;

	private System.String enteredDtLoc = null;

	private System.String ipAddress = null;

	[Column(Name = "DOC_TYPE_CD", IsKey = true, SequenceName = "")]
	public System.Decimal? DocTypeCd 
	{ 
		get{return docTypeCd;}
		set{docTypeCd = value;} 
	}

	[Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
	public System.String DefinedCd 
	{ 
		get{return definedCd;}
		set{definedCd = value;} 
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

	[Column(Name = "SHORT_NAME", IsKey = false, SequenceName = "")]
	public System.String ShortName 
	{ 
		get{return shortName;}
		set{shortName = value;} 
	}

	[Column(Name = "SHORT_NAME_LOC", IsKey = false, SequenceName = "")]
	public System.String ShortNameLoc 
	{ 
		get{return shortNameLoc;}
		set{shortNameLoc = value;} 
	}

	[Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
	public System.String Disabled 
	{ 
		get{return disabled;}
		set{disabled = value;} 
	}

	[Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
	public System.Decimal? OrderNo 
	{ 
		get{return orderNo;}
		set{orderNo = value;} 
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


	[Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

	public MisDocTypeInfo()
	{}
}