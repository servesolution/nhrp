using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using MIS.Services.Security;
using MIS.Models.Security;
using System.Data;
using MIS.Services.Core;
using System.Web.Routing;

namespace MIS.Authentication
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string PermCd = "";
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.SkipAuthorization)
            {
               // return true;
            }
            MenuSecurity mnusec = new MenuSecurity();
            RouteValueDictionary rvd = new RouteValueDictionary();
            if (HttpContext.Current.Request.QueryString["p"] != null)
            {
                rvd = QueryStringEncrypt.DecryptString(HttpContext.Current.Request.QueryString["p"]);
                if (rvd != null)
                {
                    if (rvd["menuCode"] != null)
                    {
                        mnusec.menuCd = rvd["menuCode"].ConvertToString();
                        filterContext.HttpContext.Session["menuCode"] = rvd["menuCode"].ConvertToString();
                    }
                    else
                    {
                        string strController = filterContext.RequestContext.RouteData.Values["routeController"].ConvertToString();
                        if (string.IsNullOrEmpty(strController))
                        {
                            strController = filterContext.RequestContext.RouteData.Values["controller"].ConvertToString();
                        }
                        string action = filterContext.RequestContext.RouteData.Values["action"].ConvertToString();
                        string menuUrl = string.Format("/{0}/{1}/", strController, action);
                        var menu = new MenuBuilder().GetUserWiseMenu(menuUrl, CommonVariables.UserCode);

                        HttpContext.Current.Session["sessionAuthorizedMenu"] = menu;
                        mnusec.menuCd = filterContext.HttpContext.Session["CurrentAuthorizeMenuCd"].ConvertToString();
                        filterContext.HttpContext.Session["menuCode"] = filterContext.HttpContext.Session["CurrentAuthorizeMenuCd"].ConvertToString();
                    }
                }
            }
          
            CheckActionPermission objChkAction = new CheckActionPermission();
           
            Users genUsr = new Models.Security.Users();
            string prevMenuCd = (filterContext.HttpContext.Session["PreviousMenuCd"] == null) ? "" : filterContext.HttpContext.Session["PreviousMenuCd"].ToString();
            if (filterContext.HttpContext.Session["menuCode"] != null)
            {
                mnusec.menuCd = (filterContext.HttpContext.Session["menuCode"].ConvertToString() == "") ? prevMenuCd : filterContext.HttpContext.Session["menuCode"].ToString();
            }
            else
            {
                mnusec.menuCd = prevMenuCd;
            }
           
            mnusec.permCd = PermCd;

            if (filterContext.HttpContext.Session["comuser"] != null)
            {
                genUsr = (Users)filterContext.HttpContext.Session["comuser"];
                if (genUsr.usrCd != null && genUsr.usrName!="ADMIN")
                {
                    if (genUsr.usrCd.ConvertToString() != "2502")
                    //if (genUsr.usrName.ToLower() != "sadmin")
                    {
                        mnusec.UsrCd = genUsr.usrCd;
                        // string abc = Myparam;
                        // Do any autorization logic here
                        DataTable dt = objChkAction.checkActionPermission(mnusec);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                // return true;
                            }
                            else
                            {
                                // return false;
                                filterContext.Controller.TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";
                                filterContext.Result = new RedirectResult(filterContext.HttpContext.Session["PreviiousUrl"].ConvertToString());
                            }
                        }
                        else
                        {
                            //return false;
                            filterContext.Controller.TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";
                            filterContext.Result = new RedirectResult(filterContext.HttpContext.Session["PreviiousUrl"].ConvertToString());
                        }
                    }
                }
            }
            
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Controller.TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";

            // calling the base method will actually throw a 401 error that the
            // forms authentication module will intercept and automatically redirect
            // you to the LogOn page that was defined in web.config
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}