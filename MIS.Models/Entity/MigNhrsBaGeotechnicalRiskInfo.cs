using System;
using EntityFramework;

[Table(Name = "MIG_NHRS_GEOTECHNICAL_RISK")]
public class MigNhrsBaGeotechnicalRiskInfo : EntityBase
{

    private System.String houseOwnerId = null;

    private System.String buildingStructureNo = null;

    private System.Decimal? geotechnicalRiskTypeCd = null;

    private System.String otherGeotechRiskEng = null;

    private System.String otherGeotechRiskLoc = null;

    private System.String approved = null;

    private System.String approvedBy = null;

    private System.DateTime? approvedDt = null;

    private System.String approvedDtLoc = null;

    private System.String updatedBy = null;

    private System.DateTime? updatedDt = null;

    private System.String updatedDtLoc = null;

    private System.String enteredBy = null;

    private System.DateTime? enteredDt = null;

    private System.String enteredDtLoc = null;

    private System.Decimal? batchId = null;

    private System.String ipAddress = null;

    [Column(Name = "HOUSE_OWNER_ID", IsKey = true, SequenceName = "")]
    public System.String HouseOwnerId
    {
        get { return houseOwnerId; }
        set { houseOwnerId = value; }
    }

    [Column(Name = "BUILDING_STRUCTURE_NO", IsKey = true, SequenceName = "")]
    public System.String BuildingStructureNo
    {
        get { return buildingStructureNo; }
        set { buildingStructureNo = value; }
    }

    [Column(Name = "GEOTECHNICAL_RISK_TYPE_CD", IsKey = true, SequenceName = "")]
    public System.Decimal? GeotechnicalRiskTypeCd
    {
        get { return geotechnicalRiskTypeCd; }
        set { geotechnicalRiskTypeCd = value; }
    }

    [Column(Name = "OTHER_GEOTECH_RISK_ENG", IsKey = false, SequenceName = "")]
    public System.String OtherGeotechRiskEng
    {
        get { return otherGeotechRiskEng; }
        set { otherGeotechRiskEng = value; }
    }

    [Column(Name = "OTHER_GEOTECH_RISK_LOC", IsKey = false, SequenceName = "")]
    public System.String OtherGeotechRiskLoc
    {
        get { return otherGeotechRiskLoc; }
        set { otherGeotechRiskLoc = value; }
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

    [Column(Name = "UPDATED_BY", IsKey = false, SequenceName = "")]
    public System.String UpdatedBy
    {
        get { return updatedBy; }
        set { updatedBy = value; }
    }

    [Column(Name = "UPDATED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? UpdatedDt
    {
        get { return updatedDt; }
        set { updatedDt = value; }
    }

    [Column(Name = "UPDATED_DT_LOC", IsKey = false, SequenceName = "")]
    public System.String UpdatedDtLoc
    {
        get { return updatedDtLoc; }
        set { updatedDtLoc = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public System.String EnteredBy
    {
        get { return enteredBy; }
        set { enteredBy = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public System.DateTime? EnteredDt
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

    [Column(Name = "BATCH_ID", IsKey = false, SequenceName = "")]
    public System.Decimal? BatchId
    {
        get { return batchId; }
        set { batchId = value; }
    }

    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipAddress; }
        set { ipAddress = value; }
    }

    public MigNhrsBaGeotechnicalRiskInfo()
    { }
}