using System;
using System;
using EntityFramework;

[Table(Name = "NHRS_GRIEVANCE_DOC_DETAIL")]
public class Grievancedoctypeinfo : EntityBase
{

    private System.Decimal? doctypeCd = null;

    private System.String definedcd = null;

    private System.String desceng = null;

    private System.String descloc = null;

    private System.String shortname = null;

    private System.String shortnameloc = null;

    private System.Decimal? orderno = null;

    private System.Boolean disabled = false;

    private System.Boolean approved = false;

    private System.String approvedBy = null;

    private System.DateTime? approvedDt = null;

    private System.String approvedDtLoc = null;

    private System.String enteredBy = null;

    private System.String enteredDt = null;

    private System.String enteredDtLoc = null;

    private System.String ipAddress = null;
    public String GRIEVANCE_DOC_DETAIL_ID { get; set; }
    public String CASE_REGISTRATION_ID { get; set; }

    [Column(Name = "DOC_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? DoctypeCd
    {
        get { return doctypeCd; }
        set { doctypeCd = value; }
    }

    [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String Definedcd
    {
        get { return definedcd; }
        set { definedcd = value; }
    }

    [Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
    public System.String Desceng
    {
        get { return desceng; }
        set { desceng = value; }
    }

    [Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
    public System.String Descloc
    {
        get { return descloc; }
        set { descloc = value; }
    }

    [Column(Name = "SHORT_NAME", IsKey = false, SequenceName = "")]
    public System.String Shortname
    {
        get { return shortname; }
        set { shortname = value; }
    }

    [Column(Name = "SHORT_NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String Shortnameloc
    {
        get { return shortnameloc; }
        set { shortnameloc = value; }
    }

    [Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
    public System.Decimal? Orderno
    {
        get { return orderno; }
        set { orderno = value; }
    }

    [Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
    public System.Boolean Disabled
    {
        get { return disabled; }
        set { disabled = value; }
    }

    [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
    public bool Approved
    {
        get { return approved; }
        set { approved = value; }
    }

    [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
    public System.String ApprovedBy
    {
        get { return approvedBy; }
        set { approvedBy = value; }
    }

    [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? ApprovedDt
    {
        get { return approvedDt; }
        set { approvedDt = value; }
    }

    [Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String ApprovedDtLoc
    {
        get { return approvedDtLoc; }
        set { approvedDtLoc = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String EnteredBy
    {
        get { return enteredBy; }
        set { enteredBy = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public System.String EnteredDt
    {
        get { return enteredDt; }
        set { enteredDt = value; }
    }

    [Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String EnteredDtLoc
    {
        get { return enteredDtLoc; }
        set { enteredDtLoc = value; }
    }


    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }
    [Column(Name = "GRIEVANCE_DOC_DETAIL_ID", IsKey = false, SequenceName = "")]
    public System.String GrievanceDocDetailId
    {
        get { return GRIEVANCE_DOC_DETAIL_ID; }
        set { GRIEVANCE_DOC_DETAIL_ID = value; }
    }
    [Column(Name = "CASE_REGISTRATION_ID", IsKey = false, SequenceName = "")]
    public System.String CaseRegistrationId
    {
        get { return CASE_REGISTRATION_ID; }
        set { CASE_REGISTRATION_ID = value; }
    }

    public Grievancedoctypeinfo()
    {

    }
}
