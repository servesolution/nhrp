using System; 
using EntityFramework;

[Table(Name = "NHRS_GRIEVANCE_TYPE_DETAIL")]

public class CaseGrievanceTypeDetailInfo : EntityBase
{

    private System.Decimal? grievancetypedetailid = null;

    private System.Decimal? caseregistrationid = null;

    private System.Decimal? grievancetypecd = null;

    private System.String enteredby = null;

    private System.DateTime? entereddate = null;

    private System.String lastupdatedby = null;

    private System.DateTime? lastupdateddate = null;
    private System.String remarks=null;

    [Column(Name = "GRIEVANCE_TYPE_DETAIL_ID", IsKey = true, SequenceName = "")]
    public System.Decimal? GRIEVANCE_TYPE_DETAIL_ID
    {
        get { return grievancetypedetailid; }
        set { grievancetypedetailid = value; }
    }

    [Column(Name = "CASE_REGISTRATION_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? Caseregistrationid
    {
        get { return caseregistrationid; }
        set { caseregistrationid = value; }
    }

    [Column(Name = "CASE_GRIEVANCE_TYPE_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? CASE_GRIEVANCE_TYPE_CD
    {
        get { return grievancetypecd; }
        set { grievancetypecd = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String Enteredby
    {
        get { return enteredby; }
        set { enteredby = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? Entereddate
    {
        get { return entereddate; }
        set { entereddate = value; }
    }

    [Column(Name = "LAST_UPDATED_BY", IsKey = false, SequenceName = "")]
    public System.String Lastupdatedby
    {
        get { return lastupdatedby; }
        set { lastupdatedby = value; }
    }

    [Column(Name = "LAST_UPDATED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? Lastupdateddate
    {
        get { return lastupdateddate; }
        set { lastupdateddate = value; }
    }
    
    [Column(Name = "REMARKS", IsKey = false, SequenceName = "")]
    public System.String Remarks
    {
        get { return remarks; }
        set { remarks = value; }
    }
    public CaseGrievanceTypeDetailInfo()
    {

    }
}
