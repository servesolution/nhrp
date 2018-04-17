using System;
using EntityFramework;

[Table(Name = "COM_SESSION_TRANS_ID")]
public class ComSessionTransIdInfo : EntityBase
{
	private Decimal transId  = 0;

	[Column(Name = "TRANS_ID", IsKey = false, SequenceName = "")]
	public Decimal TransId
	{
		get{return transId;}
		set{transId = value;}
	}

	public ComSessionTransIdInfo()
	{}

}
