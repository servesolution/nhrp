using System;
using EntityFramework;

[Table(Name = "NHRS_CASE_GRIEVIENCE_TYPE")]

    public class CaseGrievanceTypeInfo : EntityBase
    {
    private System.Decimal? casegrievancetypecd = null;

    private System.Decimal? casegrievancedefcd = null;

    private System.String desceng = null;

    private System.String descloc = null;

    private System.String shortname = null;

    private System.String shortnameloc = null;

    private System.Decimal? orderno = null;

    private System.DateTime? lastupdatedate = null;

    private System.String approved = null;

    private System.String approvedby = null;

    private System.DateTime? approveddt = null;

    private System.DateTime? approveddtloc = null;

    private System.String enteredby = null;

    private System.DateTime? entereddt = null;

    private System.DateTime? entereeddtloc = null;


    [Column(Name = "CASE_GRIEVIENCE_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? Casegrievancetypecd
    {
        get { return casegrievancetypecd; }
        set { casegrievancetypecd = value; }
    }

    [Column(Name = "CASE_GRIEVIENCE_DEF_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? Casegrievancedefcd
    {
        get { return casegrievancedefcd; }
        set { casegrievancedefcd = value; }
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
    public System.DateTime? Lastupdatedate
    {
        get { return lastupdatedate; }
        set { lastupdatedate = value; }
    }
        [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
    public System.String Approved
    {
        get { return approved; }
        set { approved = value; }
    }
        [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
    public System.String Approvedby
    {
        get { return approvedby; }
        set { approvedby = value; }
    }
        [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? Approveddt
    {
        get { return approveddt; }
        set { approveddt = value; }
    }
        [Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.DateTime? Approveddtloc
    {
        get { return approveddtloc; }
        set { approveddtloc = value; }
    }
        [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String Enteredby
    {
        get { return enteredby; }
        set { enteredby = value; }
    }
        [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? Entereddt
    {
        get { return entereddt; }
        set { entereddt = value; }
    }
        [Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.DateTime? Entereeddtloc
    {
        get { return entereeddtloc; }
        set { entereeddtloc = value; }
    }


        public CaseGrievanceTypeInfo()
    {

    }
  }

