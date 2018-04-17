using System;
using EntityFramework;

[Table(Name = "COM_REPORT")]
public class ComReportInfo : EntityBase
{
	private String reportCd  = null;
	private String upperReportCd  = null;
	private String descEng  = null;
	private String descLoc  = null;
	private String reportLink  = null;
	private String reportTitle  = null;
	private String reportTitleLoc  = null;
	private Decimal? orderNo  = null;
	private bool disabled  = false;
	private String enteredBy  = null;
	private DateTime enteredDt  = DateTime.Now;
	private DateTime lastUpdatedDt  = DateTime.Now;
	private String lastUpdatedBy  = null;

	[Column(Name = "REPORT_CD", IsKey = true, SequenceName = "")]
	public String ReportCd
	{
		get{return reportCd;}
		set{reportCd = value;}
	}

	[Column(Name = "UPPER_REPORT_CD", IsKey = false, SequenceName = "")]
	public String UpperReportCd
	{
		get{return upperReportCd;}
		set{upperReportCd = value;}
	}

	[Column(Name = "DESC_ENG", IsKey = false, SequenceName = "")]
	public String DescEng
	{
		get{return descEng;}
		set{descEng = value;}
	}

	[Column(Name = "DESC_LOC", IsKey = false, SequenceName = "")]
	public String DescLoc
	{
		get{return descLoc;}
		set{descLoc = value;}
	}

	[Column(Name = "REPORT_LINK", IsKey = false, SequenceName = "")]
	public String ReportLink
	{
		get{return reportLink;}
		set{reportLink = value;}
	}

	[Column(Name = "REPORT_TITLE", IsKey = false, SequenceName = "")]
	public String ReportTitle
	{
		get{return reportTitle;}
		set{reportTitle = value;}
	}

	[Column(Name = "REPORT_TITLE_LOC", IsKey = false, SequenceName = "")]
	public String ReportTitleLoc
	{
		get{return reportTitleLoc;}
		set{reportTitleLoc = value;}
	}

	[Column(Name = "ORDER_NO", IsKey = false, SequenceName = "")]
	public Decimal? OrderNo
	{
		get{return orderNo;}
		set{orderNo = value;}
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

	[Column(Name = "LAST_UPDATED_DT", IsKey = false, SequenceName = "")]
	public DateTime LastUpdatedDt
	{
		get{return lastUpdatedDt;}
		set{lastUpdatedDt = value;}
	}

	[Column(Name = "LAST_UPDATED_BY", IsKey = false, SequenceName = "")]
	public String LastUpdatedBy
	{
		get{return lastUpdatedBy;}
		set{lastUpdatedBy = value;}
	}

	public ComReportInfo()
	{}

}
