using System;
using EntityFramework;

[Table(Name = "mis_district")]
public class DistrictSetupInfo : EntityBase
{
    //public string mode { get; set; }
    private System.Decimal? districtCd = null;
    private System.String definedCd = null;
    private System.String descEng = null;
    private System.String descLoc = null;
    private System.String shortName = null;
    private System.String shortNameLoc = null;
    private System.Decimal? zoneCd = null;
    private System.Decimal? orderNo = null;
    private System.String Disabled = null;
    private System.String Approved = null;
    private System.String approvedBy = null;
    private System.DateTime? approvedDt = null;
    private System.String approvedDtLoc = null;
    private System.String enteredBy = null;
    private System.DateTime? enteredDt = null;
    private System.String enteredDtLoc = null;
    private System.String ipAddress = null;

    [Column(Name = "district_cd", IsKey = true, SequenceName = "")]
    public System.Decimal? DistrictCd
    {
        get { return districtCd; }
        set { districtCd = value; }
    }

    [Column(Name = "defined_cd", IsKey = false, SequenceName = "")]
    public System.String DefinedCd
    {
        get { return definedCd; }
        set { definedCd = value; }
    }

    [Column(Name="desc_eng",IsKey=false,SequenceName="")]
    public System.String DescEng
    {
        get { return descEng; }
        set { descEng = value; }
    }

    [Column(Name="desc_loc",IsKey=false,SequenceName="")]
    public System.String DescLoc
    {
        get { return descLoc; }
        set { descLoc = value; }
    }

    [Column(Name = "short_name", IsKey = false, SequenceName = "")]
    public System.String ShortName
    {
        get { return shortName; }
        set { shortName = value; }
    }

    [Column(Name="short_name_loc",IsKey=false,SequenceName="")]
    public System.String ShortNameLoc
    {
        get { return shortNameLoc; }
        set { shortNameLoc = value; }
    }

    [Column(Name = "zone_cd", IsKey = false, SequenceName = "")]
    public System.Decimal? ZoneCd
    {
        get { return zoneCd; }
        set { zoneCd = value; }
    }

    [Column(Name="order_no",IsKey=false,SequenceName="")]
    public System.Decimal? OrderNo
    {
        get { return orderNo; }
        set { orderNo = value; }
    }

    [Column(Name="disabled",IsKey=false,SequenceName="")]
    public System.String disabled
    {
        get { return Disabled; }
        set { Disabled = value; }
    }

    [Column(Name = "approved", IsKey = false, SequenceName = "")]
    public System.String approved
    {
        get { return Approved; }
        set { Approved = value; }
    }

    [Column(Name="approved_by",IsKey=false,SequenceName="")]
    public System.String ApprovedBy
    {
        get { return approvedBy; }
        set { approvedBy = value; }
    }

    [Column(Name = "approved_dt", IsKey = false, SequenceName = "")]
    public System.DateTime? ApprovedDt
    {
        get { return approvedDt; }
        set { approvedDt = value; }
    }

    [Column(Name="approved_dt_loc",IsKey=false,SequenceName="")]
    public System.String ApprovedDtLoc
    {
        get { return approvedDtLoc; }
        set { approvedDtLoc = value; }
    }

    [Column(Name="entered_by",IsKey=false,SequenceName="")]
    public System.String EnteredBy
    {
        get { return enteredBy; }
        set { enteredBy = value; }
    }

    [Column(Name="entered_dt",IsKey=false,SequenceName="")]
    public System.DateTime? EnteredDt
    {
        get { return enteredDt; }
        set { enteredDt = value; }
    }

    [Column(Name="entered_dt_loc",IsKey=false,SequenceName="")]
    public System.String EnteredDtLoc
    {
        get { return enteredDtLoc; }
        set { enteredDtLoc = value; }
    }

    [Column(Name="ip_address",IsKey=false,SequenceName="")]
    public System.String IpAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

    public DistrictSetupInfo()
    {

    }
}