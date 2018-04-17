using System;
using EntityFramework;

[Table(Name = "CASE_GRIEVANCE_DOC_INFO")]
public class CaseGrievanceDocInfoInfo : EntityBase
{
	private Decimal caseGrievanceDocCd  = 0;
	private Decimal? serialNo  = null;
	private Decimal? grievanceDocTypeCd  = null;
	private Decimal? grievanceFormDetailCd  = null;
	private String remarksEng  = null;
	private String remarksLoc  = null;
	private String filePath  = null;
	private String approved  = null;
	private String approvedBy  = null;
	private DateTime? approvedDt  = null;
	private String approvedDtLoc  = null;
	private String updatedBy  = null;
	private DateTime? updatedDt  = null;
	private String updatedDtLoc  = null;
	private String enteredBy  = null;
	private DateTime? enteredDt  = null;
	private String enteredDtLoc  = null;
    private string ipaddress = null;

	[Column(Name = "CASE_GRIEVANCE_DOC_CD", IsKey = true, SequenceName = "")]
	public Decimal CaseGrievanceDocCd
	{
		get{return caseGrievanceDocCd;}
		set{caseGrievanceDocCd = value;}
	}

	[Column(Name = "SERIAL_NO", IsKey = false, SequenceName = "")]
	public Decimal? SerialNo
	{
		get{return serialNo;}
		set{serialNo = value;}
	}

	[Column(Name = "GRIEVANCE_DOC_TYPE_CD", IsKey = false, SequenceName = "")]
	public Decimal? GrievanceDocTypeCd
	{
		get{return grievanceDocTypeCd;}
		set{grievanceDocTypeCd = value;}
	}

	[Column(Name = "GRIEVANCE_FORM_DETAIL_CD", IsKey = false, SequenceName = "")]
	public Decimal? GrievanceFormDetailCd
	{
		get{return grievanceFormDetailCd;}
		set{grievanceFormDetailCd = value;}
	}

	[Column(Name = "REMARKS_ENG", IsKey = false, SequenceName = "")]
	public String RemarksEng
	{
		get{return remarksEng;}
		set{remarksEng = value;}
	}

	[Column(Name = "REMARKS_LOC", IsKey = false, SequenceName = "")]
	public String RemarksLoc
	{
		get{return remarksLoc;}
		set{remarksLoc = value;}
	}

	[Column(Name = "FILE_PATH", IsKey = false, SequenceName = "")]
	public String FilePath
	{
		get{return filePath;}
		set{filePath = value;}
	}

	[Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
	public String Approved
	{
		get{return approved;}
		set{approved = value;}
	}

	[Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
	public String ApprovedBy
	{
		get{return approvedBy;}
		set{approvedBy = value;}
	}

	[Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
	public DateTime? ApprovedDt
	{
		get{return approvedDt;}
		set{approvedDt = value;}
	}

	[Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
	public String ApprovedDtLoc
	{
		get{return approvedDtLoc;}
		set{approvedDtLoc = value;}
	}

	[Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
	public String UpdatedBy
	{
		get{return updatedBy;}
		set{updatedBy = value;}
	}

	[Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
	public DateTime? UpdatedDt
	{
		get{return updatedDt;}
		set{updatedDt = value;}
	}

	[Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
	public String UpdatedDtLoc
	{
		get{return updatedDtLoc;}
		set{updatedDtLoc = value;}
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public String EnteredBy
	{
		get{return enteredBy;}
		set{enteredBy = value;}
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public DateTime? EnteredDt
	{
		get{return enteredDt;}
		set{enteredDt = value;}
	}

	[Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
	public String EnteredDtLoc
	{
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;}
	}

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String Ipaddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }


	
	public CaseGrievanceDocInfoInfo()
	{}

}
