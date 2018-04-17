using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Security;

namespace MIS.Services.Core
{
    public class EmailTemplate
    {
        public static string ForgotPassword(Users objUser)
        {
            StringBuilder strTemplate = new StringBuilder();
            if (objUser!=null)
            {
                
                strTemplate.Append(@"<table cellspacing='0' cellpadding='0' border='0' align='center'  width='100%'>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Hello "+objUser.usrName+",</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Please check your username and password as below.</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ><b><u>User Information</u></b></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'><b>Email :</b> "+objUser.email.ToString()+"</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'><b>Password :</b> " + Utils.DecryptString(objUser.password.ToString()) + "</td></tr>");
                strTemplate.Append(@"<tr><td style='height:30px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Thank you</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"</table>");
            }
            return strTemplate.ToString();
        }


        public static string UserRegistration(Users objUser)
        {
            StringBuilder strTemplate = new StringBuilder();
            if (objUser != null)
            {

                strTemplate.Append(@"<table cellspacing='0' cellpadding='0' border='0' align='center'  width='100%'>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Hello " + objUser.usrName + ",</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thank you for registering in our site.");
                strTemplate.Append(@"You are not allowed to login to our site for now.");
                strTemplate.Append(@"Please wait for our next mail for approval.Then, you can login to our site.</td></tr>");
                strTemplate.Append(@"<tr><td style='height:30px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Thank you</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"</table>");
            }
            return strTemplate.ToString();
        }

        public static string UserApproval(Users objUser)
        {
            StringBuilder strTemplate = new StringBuilder();
            if (objUser != null)
            {

                strTemplate.Append(@"<table cellspacing='0' cellpadding='0' border='0' align='center'  width='100%'>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Hello " + objUser.usrName + ",</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;You have been approved to use the EHRP MIS.You can login the site.Please check your username and password as below.</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ><b><u>User Information</u></b></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'><b>Email :</b> " + objUser.email.ToString() + "</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'><b>Password :</b> " + Utils.DecryptString(objUser.password.ToString()) + "</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left'><b>URL :</b> https://ehrpmis.nra.gov.np</td></tr>");
                strTemplate.Append(@"<tr><td style='height:30px;' align='left' ></td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Thank you!!</td></tr>");
                strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
                strTemplate.Append(@"</table>");
            }
            return strTemplate.ToString();
        }

        public static string ExcelExport()
        {
            StringBuilder strTemplate = new StringBuilder();
            strTemplate.Append(@"<table cellspacing='0' cellpadding='0' border='0' align='center'  width='100%'>");
            strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
            strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Hello </td></tr>");
            strTemplate.Append(@"<tr><td style='height:10px;' align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Here,we have attached the xls file along with this mail.Please check the xls file.</td></tr>");
            strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
            strTemplate.Append(@"<tr><td style='height:30px;' align='left' ></td></tr>");
            strTemplate.Append(@"<tr><td style='height:10px;' align='left' >Thank you</td></tr>");
            strTemplate.Append(@"<tr><td style='height:10px;' align='left' ></td></tr>");
            strTemplate.Append(@"</table>");
            return strTemplate.ToString();
        }
    }
}
