using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using MIS.Models.Core;
using ExceptionHandler;
namespace MIS.Services.Core
{

    public static class MailSend
    {
        public static string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["STMPServer"].ToString();
        public static string adminEmail = System.Configuration.ConfigurationManager.AppSettings["AdminEmail"].ToString();
        public static bool SendMail(EmailMessage objMessage)
        {
            bool chkSend = false;
            MailMessage msg = new MailMessage();
            SmtpClient SmtpServer1 = new SmtpClient(SmtpServer);
            try
            {
                msg.From = new MailAddress(adminEmail);
                msg.To.Add(new MailAddress(objMessage.To));
                msg.Priority = MailPriority.Normal;
                msg.IsBodyHtml = true;
                msg.Subject = objMessage.Subject;
                msg.Body = objMessage.Body;
                if (!string.IsNullOrEmpty(objMessage.CC))
                    msg.CC.Add(objMessage.CC);
                if (!string.IsNullOrEmpty(objMessage.BCC))
                    msg.Bcc.Add(objMessage.BCC);
                if (!string.IsNullOrEmpty(objMessage.Attachment))
                    msg.Attachments.Add(new Attachment(objMessage.Attachment));

                SmtpServer1.Port = 587;
                SmtpServer1.Credentials = new System.Net.NetworkCredential("secureserveithome@gmail.com", "SSITH@2016");
                SmtpServer1.EnableSsl = true;

                SmtpServer1.Send(msg);
                chkSend = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return chkSend;

        }
        public static bool SendMail(EmailMessage objMessage, List<string> attachments)
        {
            MailMessage msg = new MailMessage();
            try
            {
                MailAddress from = new MailAddress(objMessage.From);
                msg.From = from;
                msg.To.Add(objMessage.To);
                if (!string.IsNullOrEmpty(objMessage.CC))
                    msg.CC.Add(objMessage.CC);
                if (!string.IsNullOrEmpty(objMessage.BCC))
                    msg.Bcc.Add(objMessage.BCC);
                if (!string.IsNullOrEmpty(objMessage.Attachment))
                    msg.Attachments.Add(new Attachment(objMessage.Attachment));
                if (attachments.Count > 0)
                {
                    foreach (string attchment in attachments)
                    {
                        msg.Attachments.Add(new Attachment(attchment));
                    }
                }
                msg.Priority = MailPriority.Normal;
                msg.IsBodyHtml = true;
                msg.Subject = objMessage.Subject;
                msg.Body = objMessage.Body;
                SendEmail(msg);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return false;
            }
            finally
            {
                if (msg != null)
                {
                    msg.Dispose();
                    msg = null;
                }
            }
        }

        public static void SendEmail(MailMessage message)
        {
            SmtpClient mailClient = new SmtpClient();
            mailClient.Host = SmtpServer;
            mailClient.Send(message);
        }
    }
}
