using System;
using EntityFramework;

[Table(Name = "COM_WEB_USR")]
public class ComWebUsrInfo : EntityBase
{
    private String usrCd = null;
    private String usrName = null;
    private String password = null;
    private String empCd = null;
    private String status = null;
    private String expiryDt = null;
    private String expiryDtloc = null;
    private bool internalUsrFlag = false;
    private String enteredBy = null;
    private DateTime enteredDt = DateTime.Now;
    private String lastUpdatedBy = null;
    private DateTime lastUpdatedDt = DateTime.Now;
    private String email = null;
    private bool approved = false;
    private String approvedBy = null;
    private String approvedDt = null;
    private String ipaddress = null;
    private String VerificationReruired = null;
    private String VerificationCode = null;
    private String mobileNo = null;
    private String VerificationValidity = null;
    private String isbankuser = null;
    private String bankcd = null;
    private String isDonor = null;
    private String donor_cd = null;
    [Column(Name = "USR_CD", IsKey = true, SequenceName = "")]
    public String UsrCd
    {
        get { return usrCd; }
        set { usrCd = value; }
    }

    [Column(Name = "USR_NAME", IsKey = false, SequenceName = "")]
    public String UsrName
    {
        get { return usrName; }
        set { usrName = value; }
    }

    [Column(Name = "PASSWORD", IsKey = false, SequenceName = "")]
    public String Password
    {
        get { return password; }
        set { password = value; }
    }

    [Column(Name = "EMP_CD", IsKey = false, SequenceName = "")]
    public String EmpCd
    {
        get { return empCd; }
        set { empCd = value; }
    }

    [Column(Name = "STATUS", IsKey = false, SequenceName = "")]
    public String Status
    {
        get { return status; }
        set { status = value; }
    }

    [Column(Name = "EXPIRY_DT", IsKey = false, SequenceName = "")]
    public String ExpiryDt
    {
        get { return expiryDt; }
        set { expiryDt = value; }
    }
    [Column(Name = "EXPIRY_DT_LOC", IsKey = false, SequenceName = "")]
    public String ExpiryDtLoc
    {
        get { return expiryDtloc; }
        set { expiryDtloc = value; }
    }

    [Column(Name = "INTERNAL_USR_FLAG", IsKey = false, SequenceName = "")]
    public bool InternalUsrFlag
    {
        get { return internalUsrFlag; }
        set { internalUsrFlag = value; }
    }

    [Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
    public String EnteredBy
    {
        get { return enteredBy; }
        set { enteredBy = value; }
    }

    [Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
    public DateTime EnteredDt
    {
        get { return enteredDt; }
        set { enteredDt = value; }
    }

    [Column(Name = "LAST_UPDATED_BY", IsKey = false, SequenceName = "")]
    public String LastUpdatedBy
    {
        get { return lastUpdatedBy; }
        set { lastUpdatedBy = value; }
    }

    [Column(Name = "LAST_UPDATED_DT", IsKey = false, SequenceName = "")]
    public DateTime LastUpdatedDt
    {
        get { return lastUpdatedDt; }
        set { lastUpdatedDt = value; }
    }

    [Column(Name = "EMAIL", IsKey = false, SequenceName = "")]
    public String Email
    {
        get { return email; }
        set { email = value; }
    }

    [Column(Name = "APPROVED", IsKey = false, SequenceName = "")]
    public bool Approved
    {
        get { return approved; }
        set { approved = value; }
    }

    [Column(Name = "APPROVED_BY", IsKey = false, SequenceName = "")]
    public String ApprovedBy
    {
        get { return approvedBy; }
        set { approvedBy = value; }
    }

    [Column(Name = "APPROVED_DT", IsKey = false, SequenceName = "")]
    public String ApprovedDt
    {
        get { return approvedDt; }
        set { approvedDt = value; }
    }

    //ip_address
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }
      [Column(Name = "MOBILE_NUMBER", IsKey = false, SequenceName = "")]
    public String Mobile_No
    {
        get { return mobileNo; }
        set { mobileNo = value; }
    }
     [Column(Name = "VERIFICATION_CODE", IsKey = false, SequenceName = "")]
    public String Verification_Code
    {
        get { return VerificationCode; }
        set { VerificationCode = value; }
    }

    [Column(Name = "VERIFICATION_REQUIRED", IsKey = false, SequenceName = "")]
    public String Verification_Required
    {
        get { return VerificationReruired; }
        set { VerificationReruired = value; }
    }

     [Column(Name = "VERIFICATION_VALIDITY", IsKey = false, SequenceName = "")]
    public String Verification_Validity
    {
        get { return VerificationValidity; }
        set { VerificationValidity = value; }
    }
     [Column(Name = "IS_BANK_USER", IsKey = false, SequenceName = "")]
     public String Is_Bank_User
     {
         get { return isbankuser; }
         set { isbankuser = value; }
     }
     [Column(Name = "BANK_CD", IsKey = false, SequenceName = "")]
     public String Bank_CD
     {
         get { return bankcd; }
         set { bankcd = value; }
     }

    

    [Column(Name = "IS_DONOR", IsKey = false, SequenceName = "")]
     public String IsDonor
     {
         get { return isDonor; }
         set { isDonor = value; }
     }
     [Column(Name = "DONOR_CD", IsKey = false, SequenceName = "")]
     public String DONOR_CD
     {
         get { return donor_cd; }
         set { donor_cd = value; }
     }
    public ComWebUsrInfo()
    { }
}