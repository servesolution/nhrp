using System;
using EntityFramework;

[Table(Name = "COM_MESSAGE")]
public class ComMessageInfo : EntityBase
{
	private String sender  = null;
	private String receiver  = null;
	private String message  = null;
	private String messageId  = null;
	private DateTime sentDt  = DateTime.Now;
	private String readFlag  = null;

	[Column(Name = "SENDER", IsKey = false, SequenceName = "")]
	public String Sender
	{
		get{return sender;}
		set{sender = value;}
	}

	[Column(Name = "RECEIVER", IsKey = false, SequenceName = "")]
	public String Receiver
	{
		get{return receiver;}
		set{receiver = value;}
	}

	[Column(Name = "MESSAGE", IsKey = false, SequenceName = "")]
	public String Message
	{
		get{return message;}
		set{message = value;}
	}

	[Column(Name = "MESSAGE_ID", IsKey = true, SequenceName = "")]
	public String MessageId
	{
		get{return messageId;}
		set{messageId = value;}
	}

	[Column(Name = "SENT_DT", IsKey = false, SequenceName = "")]
	public DateTime SentDt
	{
		get{return sentDt;}
		set{sentDt = value;}
	}

	[Column(Name = "READ_FLAG", IsKey = false, SequenceName = "")]
	public String ReadFlag
	{
		get{return readFlag;}
		set{readFlag = value;}
	}

	public ComMessageInfo()
	{}

}
