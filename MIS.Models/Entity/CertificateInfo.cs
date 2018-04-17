using System;
using EntityFramework;

[Table(Name="NHRS_TRAINING_CERTIFICATE")]
public class CertificateInfo:EntityBase
{
    public System.String mode = null;
    public System.Decimal? certificate_cd = null;
    public System.Decimal? defined_cd = null;
    public System.String desc_eng = null;
    public System.String desc_loc = null;
    public System.String description_eng = null;
    public System.String description_loc = null;
    public System.String order_no = null;
    public System.String active = null;
    public System.String entered_by = null;
    public System.String entered_dt = null;
    public System.String entered_dt_loc = null;
    public System.String approved = null;
    public System.String approved_by = null;
    public System.String approved_dt = null;
    public System.String approved_dt_loc = null;
    public System.String updated_by = null;
    public System.String updated_dt = null;
    public System.String updated_dt_loc = null;

    [Column(Name="CERTIFICATE_CD",IsKey=true,SequenceName="")]
    public System.Decimal? CertificateCd
    {
        get { return certificate_cd; }
        set { certificate_cd = value; }
    }

    [Column(Name="DEFINED_CD",IsKey=false,SequenceName="")]
    public System.Decimal? DefinedCd 
    { 
        get { return defined_cd; } 
        set { defined_cd=value; } 
    }

    [Column(Name="DESC_ENG",IsKey=false,SequenceName="")]
    public System.String DescEng 
    {
        get { return desc_eng; }
        set { desc_eng = value; }
    }

    [Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
    public System.String DescLoc 
    {
        get { return desc_loc; }
        set { desc_loc = value; }
    }

    [Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
    public System.String OrderNo 
    {
        get { return order_no; }
        set { order_no = value; }
    }

    [Column(Name = "ACTIVE", IsKey = false, SequenceName = "")]
    public System.String Active 
    {
        get { return active; }
        set { active = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String EnteredBy 
    {
        get { return entered_by; }
        set { entered_by = value; }
    }

    [Column(Name = "ENTERED_DATE", IsKey = false, SequenceName = "")]
    public System.String EnteredDt 
    {
        get { return entered_dt; }
        set { entered_dt = value; }
    }

    [Column(Name = "ENTERED_DATE_LOC", IsKey = false, SequenceName = "")]
    public System.String EnteredDtLoc 
    {
        get { return entered_dt_loc; }
        set { entered_dt_loc = value; }
    }

    [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
    public System.String Approved 
    {
        get { return approved; }
        set { approved = value; }
    }

    [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
    public System.String ApprovedBy 
    {
        get { return approved_by; }
        set { approved_by = value; }
    }

    [Column(Name = "APPROVED_DATE", IsKey = false, SequenceName = "")]
    public System.String ApprovedDt 
    {
        get { return approved_dt; }
        set { approved_dt = value; }
    }

    [Column(Name = "APPROVED_DATE_LOC", IsKey = false, SequenceName = "")]
    public System.String ApprovedDtLoc 
    {
        get { return approved_dt_loc; }
        set { approved_dt_loc = value; }
    }

    [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
    public System.String UpdatedBy 
    {
        get { return updated_by; }
        set { updated_by = value; }
    }

    [Column(Name = "UPDATED_DATE", IsKey = false, SequenceName = "")]
    public System.String UpdatedDt 
    {
        get { return updated_dt; }
        set { updated_dt = value; }
    }

    [Column(Name = "UPDATED_DATE_LOC", IsKey = false, SequenceName = "")]
    public System.String UpdatedDtLoc 
    {
        get { return updated_dt_loc; }
        set { updated_dt_loc = value; }
    }
    [Column(Name = "DESCRIPTION_ENG", IsKey = false, SequenceName = "")]
    public System.String Description_eng
    {
        get { return description_eng; }
        set { description_eng = value; }
    }
    [Column(Name = "DESCRIPTION_LOC", IsKey = false, SequenceName = "")]
    public System.String Description_loc
    {
        get { return description_loc; }
        set { description_loc = value; }
    }
    public CertificateInfo()
    {

    }
}

