using System;
using EntityFramework;

[Table(Name = "COM_FISCAL_YEAR")]
public class ComFiscalYearInfo : EntityBase
{
	private String fiscalYr  = null;
	private Decimal? serialNo  = null;
    private String fiscalStartDt = DateTime.Now.ToString("dd-MM-yyyy");
	private String fiscalStartDtLoc  = null;
    private String fiscalEndDt = DateTime.Now.ToString("dd-MM-yyyy");
	private String fiscalEndDtLoc  = null;
	private String status  = null;
	private String provisionCloseBy  = null;
	private DateTime? provisionCloseDt  = null;
	private String provisionCloseDtLoc  = null;
	private String enteredBy  = null;
    private String enteredDtLoc = DateTime.Now.ToString("dd-MM-yyyy");
    private String enteredDt = DateTime.Now.ToString("dd-MM-yyyy");
	private String finalCloseBy  = null;
	private DateTime? finalCloseDt  = null;
	private String finalCloseDtLoc  = null;
    private String ipaddress = null;

	[Column(Name = "FISCAL_YR", IsKey = true, SequenceName = "")]
	public String FiscalYr
	{
		get{return fiscalYr;}
		set{fiscalYr = value;}
	}

	[Column(Name = "SERIAL_NO", IsKey = false, SequenceName = "")]
	public Decimal? SerialNo
	{
		get{return serialNo;}
		set{serialNo = value;}
	}

	[Column(Name = "FISCAL_START_DT", IsKey = false, SequenceName = "")]
	public String FiscalStartDt
	{
		get{return fiscalStartDt;}
		set{fiscalStartDt = value;}
	}

	[Column(Name = "FISCAL_START_DT_LOC", IsKey = false, SequenceName = "")]
	public String FiscalStartDtLoc
	{
		get{return fiscalStartDtLoc;}
		set{fiscalStartDtLoc = value;}
	}

	[Column(Name = "FISCAL_END_DT", IsKey = false, SequenceName = "")]
	public String FiscalEndDt
	{
		get{return fiscalEndDt;}
		set{fiscalEndDt = value;}
	}

	[Column(Name = "FISCAL_END_DT_LOC", IsKey = false, SequenceName = "")]
	public String FiscalEndDtLoc
	{
		get{return fiscalEndDtLoc;}
		set{fiscalEndDtLoc = value;}
	}

	[Column(Name = "STATUS", IsKey = false, SequenceName = "")]
	public String Status
	{
		get{return status;}
		set{status = value;}
	}

	[Column(Name = "PROVISION_CLOSE_BY", IsKey = false, SequenceName = "")]
	public String ProvisionCloseBy
	{
		get{return provisionCloseBy;}
		set{provisionCloseBy = value;}
	}

	[Column(Name = "PROVISION_CLOSE_DT", IsKey = false, SequenceName = "")]
	public DateTime? ProvisionCloseDt
	{
		get{return provisionCloseDt;}
		set{provisionCloseDt = value;}
	}

	[Column(Name = "PROVISION_CLOSE_DT_LOC", IsKey = false, SequenceName = "")]
	public String ProvisionCloseDtLoc
	{
		get{return provisionCloseDtLoc;}
		set{provisionCloseDtLoc = value;}
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public String EnteredBy
	{
		get{return enteredBy;}
		set{enteredBy = value;}
	}

	[Column(Name = "ENTERED_DT_LOC", IsKey = false, SequenceName = "")]
	public String EnteredDtLoc
	{
		get{return enteredDtLoc;}
		set{enteredDtLoc = value;}
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public String EnteredDt
	{
		get{return enteredDt;}
		set{enteredDt = value;}
	}

	[Column(Name = "FINAL_CLOSE_BY", IsKey = false, SequenceName = "")]
	public String FinalCloseBy
	{
		get{return finalCloseBy;}
		set{finalCloseBy = value;}
	}

	[Column(Name = "FINAL_CLOSE_DT", IsKey = false, SequenceName = "")]
	public DateTime? FinalCloseDt
	{
		get{return finalCloseDt;}
		set{finalCloseDt = value;}
	}

	[Column(Name = "FINAL_CLOSE_DT_LOC", IsKey = false, SequenceName = "")]
	public String FinalCloseDtLoc
	{
		get{return finalCloseDtLoc;}
		set{finalCloseDtLoc = value;}
	}

    //ip_address
    [Column(Name = "IP_ADDRESS", IsKey = false, SequenceName = "")]
    public String IPAddress
    {
        get { return ipaddress; }
        set { ipaddress = value; }
    }

	public ComFiscalYearInfo()
	{}

}
