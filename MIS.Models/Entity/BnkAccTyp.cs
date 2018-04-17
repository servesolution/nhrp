using System;
using EntityFramework;
[Table(Name="NHRS_BANK_ACC_TYPE")]

   public class BnkAccTyp:EntityBase
    {
    private System.Decimal? Bank_Acc_Type_CD = null;
    private System.String Defined_CD = null;
    private System.String Desc_ENG = null;
    private System.String Desc_LOC = null;
    private System.String Short_NAME = null;
    private System.String Short_NAME_LOC = null;
    private System.Decimal? Order_NO = null;
    private System.String Disabled = null;
    private System.String Approved = null;
    private System.String Approved_By = null;
    private System.DateTime? Approved_Dt = null;
    private System.String Approved_dt_LOC=null;
    private System.String Entered_BY = null;
    private System.DateTime? Entered_DT = null;
    private System.String Entered_DT_LOC = null;
    private System.String ipaddress = null;

    [Column(Name="BANK_ACC_TYPE_CD",IsKey=true,SequenceName="")]
    public System.Decimal?bank_acc_type_cd
    {
        get { return Bank_Acc_Type_CD;}
        set { Bank_Acc_Type_CD = value; }
    }

    [Column(Name = "DEFINED_CD", IsKey = true, SequenceName = "")]
    public System.String defined_cd
    {
        get { return Defined_CD; }
        set { Defined_CD = value; }
    }

    [Column(Name = "DESC_ENG", IsKey = true, SequenceName = "")]
    public System.String desc_eng
    {
        get { return Desc_ENG; }
        set { Desc_ENG = value; }
    }

    [Column(Name = "DESC_LOC", IsKey = true, SequenceName = "")]
    public System.String desc_loc
    {
        get { return Desc_LOC; }
        set { Desc_LOC = value; }
    }

    [Column(Name = "SHORT_NAME", IsKey = true, SequenceName = "")]
    public System.String short_name
    {
        get { return Short_NAME; }
        set { Short_NAME = value; }
    }

    [Column(Name = "SHORT_NAME_LOC", IsKey = true, SequenceName = "")]
    public System.String short_name_loc
    {
        get { return Short_NAME_LOC; }
        set { Short_NAME_LOC = value; }
    }

    [Column(Name = "ORDER_NO", IsKey = true, SequenceName = "")]
    public System.Decimal? order_no
    {
        get { return Order_NO; }
        set { Order_NO = value; }
    }

    [Column(Name = "DISABLED", IsKey = true, SequenceName = "")]
    public System.String disabled
    {
        get { return Disabled; }
        set { Disabled = value; }
    }

    [Column(Name = "APPROVED", IsKey = true, SequenceName = "")]
    public System.String approved
    {
        get { return Approved; }
        set { Approved = value; }
    }

    [Column(Name = "APPROVED_BY", IsKey = true, SequenceName = "")]
    public System.String approved_by
    {
        get { return Approved_By; }
        set { Approved_By = value; }
    }


    [Column(Name = "APPROVED_DT", IsKey = true, SequenceName = "")]
    public System.DateTime? approved_dt
    {
        get { return Approved_Dt; }
        set { Approved_Dt = value; }
    }

    [Column(Name = "APPROVED_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String approved_dt_loc
    {
        get { return Approved_dt_LOC; }
        set { Approved_dt_LOC = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = true, SequenceName = "")]
    public System.String entered_by
    {
        get { return Entered_BY; }
        set { Entered_BY = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = true, SequenceName = "")]
    public System.DateTime? entered_dt
    {
        get { return Entered_DT; }
        set { Entered_DT = value; }
    }

    [Column(Name = "ENTERED_DT_LOC", IsKey = true, SequenceName = "")]
    public System.String entered_dt_loc
    {
        get { return Entered_DT_LOC; }
        set { Entered_DT_LOC = value; }
    }
    [Column(Name = "ip_address", IsKey = true, SequenceName = "")]
    public System.String Ip_Address
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

    }

