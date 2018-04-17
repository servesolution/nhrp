using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MIS.Models.Security;
namespace MIS.Services.Core
{
    public static class SessionCheck
    {
        public static string sessionName = "comuser";
        public static bool CheckSession()
        {
            bool hasSession = false;
            if (HttpContext.Current.Session[sessionName] != null)
            {
                hasSession = true;
                
            }
            return hasSession;
        }
        public static void EndSession()
        {
          
            HttpContext.Current.Session.Clear();
        }
        public static string getSessionUsername()
        {
            string strUsername = "";
            if (HttpContext.Current.Session[sessionName] != null)
            {
                Users objUser = (Users)HttpContext.Current.Session[sessionName];
                if (objUser != null)
                {
                    strUsername = objUser.usrName.ConvertToString();
                }
            }
            return strUsername;
        }
        public static string getSessionUserEmail()
        {
            string strUsername = "";
            if (HttpContext.Current.Session[sessionName] != null)
            {
                Users objUser = (Users)HttpContext.Current.Session[sessionName];
                if (objUser != null)
                {
                    strUsername = objUser.email.ConvertToString();
                }
            }
            return strUsername;
        }
        public static string getSessionUserCode()
        {
            string strUserCode = "";
            if (HttpContext.Current.Session[sessionName] != null)
            {
                Users objUser = (Users)HttpContext.Current.Session[sessionName];
                if (objUser != null)
                {
                    strUserCode = objUser.usrCd;
                }
            }
            return strUserCode;
        }
    }
}
