using System;
using EntityFramework;

[Table(Name = "COM_ENGLISH_NEPALI_DATE")]
public class ComEnglishNepaliDateInfo : EntityBase
{
	private Decimal nepaliYear  = 0;
	private Decimal monthCd  = 0;
	private Decimal noOfDays  = 0;
	private String nepaliStDate  = null;
	private DateTime engStDate  = DateTime.Now;
	private DateTime engEndDate  = DateTime.Now;
	private String fiscalYr  = null;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;

	[Column(Name = "NEPALI_YEAR", IsKey = true, SequenceName = "")]
	public Decimal NepaliYear
	{
		get{return nepaliYear;}
		set{nepaliYear = value;}
	}

	[Column(Name = "MONTH_CD", IsKey = true, SequenceName = "")]
	public Decimal MonthCd
	{
		get{return monthCd;}
		set{monthCd = value;}
	}

	[Column(Name = "NO_OF_DAYS", IsKey = false, SequenceName = "")]
	public Decimal NoOfDays
	{
		get{return noOfDays;}
		set{noOfDays = value;}
	}

	[Column(Name = "NEPALI_ST_DATE", IsKey = false, SequenceName = "")]
	public String NepaliStDate
	{
		get{return nepaliStDate;}
		set{nepaliStDate = value;}
	}

	[Column(Name = "ENG_ST_DATE", IsKey = false, SequenceName = "")]
	public DateTime EngStDate
	{
		get{return engStDate;}
		set{engStDate = value;}
	}

	[Column(Name = "ENG_END_DATE", IsKey = false, SequenceName = "")]
	public DateTime EngEndDate
	{
		get{return engEndDate;}
		set{engEndDate = value;}
	}

	[Column(Name = "FISCAL_YR", IsKey = false, SequenceName = "")]
	public String FiscalYr
	{
		get{return fiscalYr;}
		set{fiscalYr = value;}
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

	public ComEnglishNepaliDateInfo()
	{}

}
