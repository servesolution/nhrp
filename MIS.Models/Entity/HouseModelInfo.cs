using System;
using EntityFramework;

[Table(Name = "NHRS_HOUSE_MODEL")]
public class HouseModelInfo : EntityBase
{
    public System.Decimal? model_id = 0;
    public System.String defined_cd = null;
    public System.String name_eng = null;
    public System.String name_loc = null;
    public System.String code_eng = null;
    public System.String code_loc = null;
    public System.String desc_eng = null;
    public System.String desc_loc = null;
    public System.String approved = null;
    public System.String approved_by = null;
    public System.DateTime? approved_dt = DateTime.Now;
    public System.String approved_dt_loc = null;
    public System.String entered_by = null;
    public System.DateTime? entered_dt = DateTime.Now;
    public System.String entered_dt_loc = null;
    public System.String updated_by = null;
    public System.DateTime? updated_dt = DateTime.Now;
    public System.String updated_dt_loc = null;
    public System.String ip_address = null;
    public System.String Design_image = null;
    public System.Decimal? hierarchy_parent_id = null;
    public System.Decimal? previous_design_id = null;
    public System.Decimal? design_number = null;


    [Column(Name = "PREVIOUS_DESIGN_CD", IsKey = false, SequenceName = "")]
    public System.Decimal? Previous_design_id
    {
        get { return previous_design_id; }
        set { previous_design_id = value; }
    }


    [Column(Name = "DESIGN_NUMBER", IsKey = false, SequenceName = "")]
    public System.Decimal? Design_number
    {
        get { return design_number; }
        set { design_number = value; }
    }

        [Column(Name = "HIERARCHY_PARENT_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? Hierarchy_parent_id
    {
        get { return hierarchy_parent_id; }
        set { hierarchy_parent_id = value; }
    }

    [Column(Name = "MODEL_ID", IsKey = true, SequenceName = "")]
    public System.Decimal? ModelId
    {
        get { return model_id; }
        set { model_id = value; }
    }

    [Column(Name = "DEFINED_CD", IsKey = false, SequenceName = "")]
    public System.String DefinedCd
    {
        get { return defined_cd; }
        set { defined_cd = value; }
    }

    [Column(Name = "NAME_ENG", IsKey = false, SequenceName = "")]
    public System.String NameEng 
    {
        get { return name_eng; }
        set { name_eng = value; }
    }

    [Column(Name = "NAME_LOC", IsKey = false, SequenceName = "")]
    public System.String NameLoc 
    {
        get { return name_loc; }
        set { name_loc = value; }
    }

    [Column(Name = "CODE_ENG", IsKey = false, SequenceName = "")]
    public System.String CodeEng 
    {
        get { return code_eng; }
        set { code_eng = value; }
    }

    [Column(Name = "CODE_LOC", IsKey = false, SequenceName = "")]
    public System.String CodeLoc 
    {
        get { return code_loc; }
        set { code_loc = value; }
    }

    [Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
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

    [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? ApprovedDt 
    {
        get { return approved_dt; }
        set { approved_dt = value; }
    }

    [Column(Name = "APPROVED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String ApprovedDtLoc 
    {
        get { return approved_dt_loc; }
        set { approved_dt_loc = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String EnteredBy 
    {
        get { return entered_by; }
        set { entered_by = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? EnteredDt 
    {
        get { return entered_dt; }
        set { entered_dt = value; }
    }

    [Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String EnteredDtLoc 
    {
        get { return entered_dt_loc; }
        set { entered_dt_loc = value; }
    }

    [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
    public System.String UpdatedBy 
    {
        get { return updated_by; }
        set { updated_by = value; }
    }

    [Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? UpdatedDt
    {
        get { return updated_dt; }
        set { updated_dt = value; }
    }

    [Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String UpdatedDtLoc 
    {
        get { return updated_dt_loc; }
        set { updated_dt_loc = value; }
    }

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public System.String IpAddress
    {
        get { return ip_address; }
        set { ip_address = value; }
    }
    [Column(Name = "IMAGE_NAME", IsKey = false, SequenceName = "")]
    public System.String design_image
    {
        get { return Design_image; }
        set { Design_image = value; }
    }
}

