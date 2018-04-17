using System;
using EntityFramework;

[Table(Name = "COM_NEP_MONTH")]
public class ComNepMonthInfo : EntityBase
{
	private Decimal monthCd  = 0;
	private String descEng  = null;
	private String descNep  = null;
	private Decimal fiscalMonth  = 0;
	private bool disabled  = false;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;

	[Column(Name = "MONTH_CD", IsKey = true, SequenceName = "")]
	public Decimal MonthCd
	{
		get{return monthCd;}
		set{monthCd = value;}
	}

	[Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
	public String DescEng
	{
		get{return descEng;}
		set{descEng = value;}
	}

	[Column(Name = "DESC_NEP", IsKey = false, SequenceName = "")]
	public String DescNep
	{
		get{return descNep;}
		set{descNep = value;}
	}

	[Column(Name = "FISCAL_MONTH", IsKey = false, SequenceName = "")]
	public Decimal FiscalMonth
	{
		get{return fiscalMonth;}
		set{fiscalMonth = value;}
	}

	[Column(Name = "DISABLED", IsKey = false, SequenceName = "")]
	public bool Disabled
	{
		get{return disabled;}
		set{disabled = value;}
	}

	[Column(Name = "ENTERED_BY", IsKey = false, SequenceName = "")]
	public String EnteredBy
	{
		get{return enteredBy;}
		set{enteredBy = value;}
	}

	[Column(Name = "ENTERED_DT", IsKey = false, SequenceName = "")]
	public DateTime EnteredDt
	{
		get{return enteredDt;}
		set{enteredDt = value;}
	}

	public ComNepMonthInfo()
	{}

}
