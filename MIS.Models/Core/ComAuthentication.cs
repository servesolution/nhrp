using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts; 

namespace MIS.Models.Core
{
    /// <summary>
    /// This class is responsible for the User Authentication related tasks. 
    /// ======================================
    /// Author:         SSITH Pvt. Ltd.
    /// Created Date:   Mar 20, 2009
    /// Modified Date: 
    /// ======================================
    /// </summary>
    public class ComAuthentication
    {
        /// <summary>
        /// Gets the profile of the current user.
        /// </summary>
        public static UserProfile CurrentUser
        {
            get
            {
                //SCubePrincipal user = HttpContext.Current.User as SCubePrincipal;
                //if (user != null && user.Profile != null)
                //{
                //    if (user.Identity != null && user.Identity.IsAuthenticated)
                //    {
                //        return user.Profile;
                //    }
                //}
                return new UserProfile();
            }
        }

        /// <summary>
        /// Gets the boolean value that indicates whether the user is authenticated or not.
        /// </summary>
        public static bool IsAuthenticated
        {
            get
            {
                //SCubePrincipal user = HttpContext.Current.User as SCubePrincipal;
                //if (user != null)
                //{
                //    if (user.Identity != null)
                //    {
                //        return user.Identity.IsAuthenticated;
                //    }
                //}
                return false;
            }
        }

        /// <summary>
        /// Signs out the user from the application and clears the session information and cache.
        /// </summary>
        /// <param name="controller">Controller</param>
        public static void SignOut(Controller controller)
        {
            //Sign out from cookie
            FormsAuthentication.SignOut();

            //Clear all session related to user
            controller.Session.Clear();
            controller.Session.Abandon();

            //Remove user's profile from cache
            //string key = string.Concat(Constants.USER_PROFILE_KEY, "_", controller.User.Identity.Name);
            //controller.RemoveCache(key);
        }

        /// <summary>
        /// Gets the URL for the login page that the System.Web.Security.FormAuthentication class will redirect to.
        /// </summary>
        public static string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }

    }
}
