using System;
using EntityFramework;

[Table(Name = "COM_CONTROL")]
public class ComControlInfo : EntityBase
{
	private String companyCd  = null;
	private String gonNameEng  = null;
	private String gonNameLoc  = null;
	private String companyNameEng  = null;
	private String companyNameLoc  = null;
	private String departmentNameEng  = null;
	private String departmentNameLoc  = null;
	private String addressEng  = null;
	private String addressLoc  = null;
	private String phoneNo  = null;
	private String eMail  = null;

	[Column(Name = "COMPANY_CD", IsKey = true, SequenceName = "")]
	public String CompanyCd
	{
		get{return companyCd;}
		set{companyCd = value;}
	}

	[Column(Name = "GON_NAME_ENG", IsKey = false, SequenceName = "")]
	public String GonNameEng
	{
		get{return gonNameEng;}
		set{gonNameEng = value;}
	}

	[Column(Name = "GON_NAME_LOC", IsKey = false, SequenceName = "")]
	public String GonNameLoc
	{
		get{return gonNameLoc;}
		set{gonNameLoc = value;}
	}

	[Column(Name = "COMPANY_NAME_ENG", IsKey = false, SequenceName = "")]
	public String CompanyNameEng
	{
		get{return companyNameEng;}
		set{companyNameEng = value;}
	}

	[Column(Name = "COMPANY_NAME_LOC", IsKey = false, SequenceName = "")]
	public String CompanyNameLoc
	{
		get{return companyNameLoc;}
		set{companyNameLoc = value;}
	}

	[Column(Name = "DEPARTMENT_NAME_ENG", IsKey = false, SequenceName = "")]
	public String DepartmentNameEng
	{
		get{return departmentNameEng;}
		set{departmentNameEng = value;}
	}

	[Column(Name = "DEPARTMENT_NAME_LOC", IsKey = false, SequenceName = "")]
	public String DepartmentNameLoc
	{
		get{return departmentNameLoc;}
		set{departmentNameLoc = value;}
	}

	[Column(Name = "ADDRESS_ENG", IsKey = false, SequenceName = "")]
	public String AddressEng
	{
		get{return addressEng;}
		set{addressEng = value;}
	}

	[Column(Name = "ADDRESS_LOC", IsKey = false, SequenceName = "")]
	public String AddressLoc
	{
		get{return addressLoc;}
		set{addressLoc = value;}
	}

	[Column(Name = "PHONE_NO", IsKey = false, SequenceName = "")]
	public String PhoneNo
	{
		get{return phoneNo;}
		set{phoneNo = value;}
	}

	[Column(Name = "E_MAIL", IsKey = false, SequenceName = "")]
	public String EMail
	{
		get{return eMail;}
		set{eMail = value;}
	}

	public ComControlInfo()
	{}

}
