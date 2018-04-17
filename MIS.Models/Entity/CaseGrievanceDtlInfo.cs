using System;
using EntityFramework;

[Table(Name = "CM_GRIEVANCE_DTL")]

public class CaseGrievanceDtlInfo : EntityBase
{
    public String grievanceCd = null;
    public String sno = null;
    public String processRemark = null;
    public String signDSWOfficer = null;
    public String signedDSWDate = null;
    public String enteredBy = null;
    public String enteredDt = null;
    public String ipaddress = null;

    [Column(Name = "GRIEVANCE_CD")]
    public String GrievanceCd
    {
        get { return grievanceCd; }
        set { grievanceCd = value; }
    }
    [Column(Name = "SNO")]
    public String SNo
    {
        get { return sno; }
        set { sno = value; }
    }
    [Column(Name = "PROCESS_REMARK")]
    public String ProcessRemark
    {
        get { return processRemark; }
        set { processRemark = value; }
    }
    [Column(Name = "SIGN_DSW_OFFICER")]
    public String SignDSWOfficer
    {
        get { return signDSWOfficer; }
        set { signDSWOfficer = value; }
    }
    [Column(Name = "SIGNED_DSW_DATE")]
    public String SignedDSWDate
    {
        get { return signedDSWDate; }
        set { signedDSWDate = value; }
    }
    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public String EnteredBy
    {
        get { return enteredBy; }
        set { enteredBy = value; }
    }
    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public String EnteredDt
    {
        get { return enteredDt; }
        set { enteredDt = value; }
    }
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IpAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }
}

