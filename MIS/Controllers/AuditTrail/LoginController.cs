using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MIS.Services.Core;
using MIS.Services.Security;
using MIS.Models.Security;
using System.Web.Routing;
using MIS;
using MIS.Authentication;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Controllers.Security;
using MIS.Services.AuditTrail;
using MIS.Models.AuditTrail;
using MIS.Models.Core;

namespace MIS.Controllers.AuditTrail
{
    public class LoginController : BaseController
    {
        LoginService objLoginService = new LoginService();
        Login objLogin = new Login();

        //Get LoginList
        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult LoginList(string p, FormCollection fc)
        {
            Utils.setUrl(this.Url);
            string initial = "";
            string orderby = "login_dt";
            string order = "desc";
            string userCode = "";
            int page = 1;
            string strLoginSNO = "";
            string strLoginDate = "";
            string strLogoutDate = "";
            string strLoginDateLoc = "";
            string strLogoutDateLoc = "";
            IDictionary<string, object> dictionName = new Dictionary<string, object>();
            IDictionary<string, object> dictionDesc = new Dictionary<string, object>();
            IDictionary<string, object> dictionLoginDate = new Dictionary<string, object>();
            IDictionary<string, object> dictionLogoutDate = new Dictionary<string, object>();
            IDictionary<string, object> dictionGroupName = new Dictionary<string, object>();
            IDictionary<string, object> dictionModuleName = new Dictionary<string, object>();
            RouteValueDictionary routeDictionaryName = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryDesc = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryLoginDate = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryLogoutDate = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryGroupName = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryModuleName = new RouteValueDictionary();
            RouteValueDictionary rvd = new RouteValueDictionary();
            ModuleService objModuleService = new ModuleService();
            CommonFunction common = new CommonFunction();
            Module objModule = new Module();
            string nextorder = "asc";
            int pageSize = 50;
            DataTable dt = null;
            try
            {
                CheckPermission();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["initial"] != null)
                        {
                            initial = rvd["initial"].ToString();
                            if (initial.Contains("%E0"))
                            {
                                initial = NepaliUnicode.getValue(initial, NepaliUnicode.NepaliCharacters());
                            }
                        }
                        if (rvd["orderby"] != null)
                        {
                            orderby = rvd["orderby"].ToString();
                        }
                        if (rvd["order"] != null)
                        {
                            order = rvd["order"].ToString();
                        }
                        if (rvd["id"] != null)
                        {
                            userCode = rvd["id"].ToString();
                        }
                        if (rvd["loginSNO"] != null)
                        {
                            strLoginSNO = rvd["loginSNO"].ToString();
                        }
                        if (rvd["pageno"] != null)
                        {
                            page = Int32.Parse(rvd["pageno"].ToString());
                        }
                    }
                }
                if (userCode == "")
                {
                    if (order == "asc")
                    {
                        nextorder = "desc";
                    }
                    if (initial != "")
                    {
                        dictionName.Add("initial", initial);
                        dictionDesc.Add("initial", initial);
                        dictionLoginDate.Add("initial", initial);
                        dictionLogoutDate.Add("initial", initial);
                        dictionGroupName.Add("initial", initial);
                        dictionModuleName.Add("initial", initial);
                    }
                    dictionName.Add("orderby", "usr_name");
                    dictionDesc.Add("orderby", "login_sno");
                    dictionLoginDate.Add("orderby", Utils.ToggleLanguage("login_dt", "login_dt_loc"));
                    dictionLogoutDate.Add("orderby", Utils.ToggleLanguage("logout_dt", "logout_dt_loc"));
                    dictionGroupName.Add("orderby", "grp_name");
                    dictionModuleName.Add("orderby", "module_name");
                    dictionName.Add("order", nextorder);
                    dictionDesc.Add("order", nextorder);
                    dictionLoginDate.Add("order", nextorder);
                    dictionLogoutDate.Add("order", nextorder);
                    dictionGroupName.Add("order", nextorder);
                    dictionModuleName.Add("order", nextorder);
                    routeDictionaryName = new RouteValueDictionary(dictionName);
                    routeDictionaryDesc = new RouteValueDictionary(dictionDesc);
                    routeDictionaryLoginDate = new RouteValueDictionary(dictionLoginDate);
                    routeDictionaryLogoutDate = new RouteValueDictionary(dictionLogoutDate);
                    routeDictionaryGroupName = new RouteValueDictionary(dictionGroupName);
                    routeDictionaryModuleName = new RouteValueDictionary(dictionModuleName);
                    if (Request.UrlReferrer!=null && Request.UrlReferrer.LocalPath != "/Login/LoginList/")
                    {
                        CommonVariables.SearchLoginName = "";
                        CommonVariables.SearchGroupCode = "";
                        CommonVariables.SearchUserName = "";
                        CommonVariables.SearchStartDate = "";
                        CommonVariables.SearchEndDate = "";
                        CommonVariables.SearchStartDateLoc = "";
                        CommonVariables.SearchEndDateLoc = "";
                    }
                    dt = new DataTable();
                    //dt = objLoginService.Login_Search(initial, orderby, order, CommonVariables.SearchUserName, CommonVariables.SearchGroupCode, CommonVariables.SearchStartDate, CommonVariables.SearchEndDate);
                    ViewData["ddGroup"] = common.GetUserGroup(fc["ddGroup"]);
                    ViewData["groupCode"] = CommonVariables.SearchGroupCode;
                    ViewData["txtUsername"] = CommonVariables.SearchUserName;
                    if (CommonVariables.SearchStartDate != "")
                    {
                        ViewData["StartDt"] = CommonVariables.SearchStartDate;
                    }
                    else
                    {
                        ViewData["StartDt"] = String.Format("{0:d}", DateTime.Today.ToString("dd-MMMM-yyyy"));
                    }
                    if (CommonVariables.SearchEndDate != "")
                    {
                        ViewData["EndDt"] = CommonVariables.SearchEndDate;
                    }
                    else
                    {
                        ViewData["EndDt"] = String.Format("{0:d}", DateTime.Today.ToString("dd-MMMM-yyyy"));
                    }
                    if (CommonVariables.SearchStartDateLoc != "")
                    {
                        ViewData["StartDtLoc"] = CommonVariables.SearchStartDateLoc;
                    }
                    else
                    {
                        ViewData["StartDtLoc"] = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today));
                    }
                    if (CommonVariables.SearchEndDateLoc != "")
                    {
                        ViewData["EndDtLoc"] = CommonVariables.SearchEndDateLoc;
                    }
                    else
                    {
                        ViewData["EndDtLoc"] = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today));
                    }
                    //paging
                    //ViewData["dtLogin"] = pagedDataTable(dt, pageSize, page);
                    ViewData["dtLogin"] = dt;
                    ViewBag.pageno = page;
                    ViewBag.PageSize = pageSize;
                    int total = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                    ViewBag.totalpage = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                    //paging
                    ViewBag.initial = initial;
                    ViewData["orderby"] = orderby;
                    ViewBag.order = order;
                    ViewBag.actionName = "LoginList";
                    ViewBag.controllerName = "Login";
                    ViewBag.nextorder = nextorder;
                    ViewBag.RouteName = QueryStringEncrypt.EncryptString(routeDictionaryName);
                    ViewBag.RouteDesc = QueryStringEncrypt.EncryptString(routeDictionaryDesc);
                    ViewBag.RouteLoginDate = QueryStringEncrypt.EncryptString(routeDictionaryLoginDate);
                    ViewBag.RouteLogoutDate = QueryStringEncrypt.EncryptString(routeDictionaryLogoutDate);
                    ViewBag.RouteGroupName = QueryStringEncrypt.EncryptString(routeDictionaryGroupName);
                    ViewBag.RouteModuleName = QueryStringEncrypt.EncryptString(routeDictionaryModuleName);
                    return View();
                }
                else
                {

                    //For viewing the login information
                    CheckPermission();
                    Utils.setUrl(this.Url);
                    objLogin = objLoginService.Login_Get(userCode, strLoginSNO);
                    if (objLogin.LoginDt != null)
                    {
                        strLoginDate = objLogin.LoginDt.ToString("yyyy-MM-dd") + " ";
                        strLoginDate += NepaliDate.getFormattedTime(objLogin.LoginHh.ToString(), objLogin.LoginMi.ToString(), objLogin.LoginSs.ToString());

                    }
                    objLogin.LoginFullDt = strLoginDate;

                    if (objLogin.LoginDtLoc.ToString() != "")
                    {
                        strLoginDateLoc = DateTime.Parse(objLogin.LoginDtLoc).ToString("yyyy-MM-dd") + " ";
                        strLoginDateLoc += NepaliDate.getFormattedTime(objLogin.LoginHh.ToString(), objLogin.LoginMi.ToString(), objLogin.LoginSs.ToString());

                    }
                    objLogin.LoginFullDtLoc = strLoginDateLoc;

                    if (objLogin.LogoutDt != null)
                    {
                        strLogoutDate = DateTime.Parse(objLogin.LogoutDt.ToString()).ToString("yyyy-MM-dd") + " ";
                        strLogoutDate += NepaliDate.getFormattedTime(objLogin.LogoutHh.ToString(), objLogin.LogoutMi.ToString(), objLogin.LogoutSs.ToString());

                    }
                    objLogin.LogoutFullDt = strLogoutDate;
                    if (objLogin.LogoutDtLoc.ToString() != "")
                    {
                        strLogoutDateLoc = DateTime.Parse(objLogin.LogoutDtLoc).ToString("yyyy-MM-dd") + " ";
                        strLogoutDateLoc += NepaliDate.getFormattedTime(objLogin.LogoutHh.ToString(), objLogin.LogoutMi.ToString(), objLogin.LogoutSs.ToString());

                    }
                    objLogin.LogoutFullDtLoc = strLogoutDateLoc;

                    return View("ManageLogin", objLogin);

                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View();
        }

        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        [HttpPost]
        public ActionResult LoginList(FormCollection fc)
        {
            try
            {
                //if (fc["btn_Submit"] == (Utils.GetLabel("Search")))
                //{
                    CheckPermission();
                    CommonVariables.SearchUserName = Convert.ToString(fc["txtUsername"]);
                    CommonVariables.SearchGroupCode = Convert.ToString(fc["ddGroup"]);
                    CommonVariables.SearchStartDate = Convert.ToString(fc["LOGIN_DT_FROM"]);
                    //CommonVariables.EnrollmentDtFrom = fc["Enrollment_Dt_From"].ConvertToString();
                    //CommonVariables.EnrollmentDtTo = fc["Enrollment_Dt_To"].ConvertToString();
                    CommonVariables.SearchEndDate = Convert.ToString(fc["LOGIN_DT_TO"]);
                    CommonVariables.SearchStartDateLoc = Convert.ToString(fc["StartDtLoc"]);
                    CommonVariables.SearchEndDateLoc = Convert.ToString(fc["EndDtLoc"]);
                    LoginService userService = new LoginService();
                    CommonFunction common = new CommonFunction();
                    DataTable dt = new DataTable();
                    string orderby = "login_dt";
                    string order = "desc";
                    int pageSize = 50;
                    int page = 1;
                    dt = objLoginService.Login_Search("", orderby, order, HttpUtility.HtmlEncode(CommonVariables.SearchUserName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchGroupCode.Trim()), CommonVariables.SearchStartDate, CommonVariables.SearchEndDate);

                    ////paging
                    //ViewData["dtLogin"] = pagedDataTable(dt, pageSize, page);
                    ViewData["dtLogin"] = dt;
                    //ViewBag.pageno = page;
                    //ViewBag.PageSize = pageSize;
                    //int total = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                    //ViewBag.totalpage = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                    ////paging
                    ViewData["ddGroup"] = common.GetUserGroup(fc["ddGroup"]);
                    ViewData["groupCode"] = CommonVariables.SearchGroupCode;
                    ViewData["txtUsername"] = CommonVariables.SearchUserName;
                    ViewData["StartDt"] = CommonVariables.SearchStartDate;
                    ViewData["EndDt"] = CommonVariables.SearchEndDate;
                    ViewData["StartDtLoc"] = CommonVariables.SearchStartDateLoc;
                    ViewData["EndDtLoc"] = CommonVariables.SearchEndDateLoc;
                    //ViewData["Enrollment_Dt_From"]=CommonVariables.EnrollmentDtFrom;
                    //ViewData["Enrollment_Dt_To"]=CommonVariables.EnrollmentDtTo;

                    ViewBag.actionName = "LoginList";
                    ViewBag.initial = "";
                    return PartialView("LoginPartialView");
                //}
                //else
                //{
                //    CommonVariables.SearchLoginName = "";
                //    CommonVariables.SearchGroupCode = "";
                //    CommonVariables.SearchUserName = "";
                //    CommonVariables.SearchStartDate = "";
                //    CommonVariables.SearchEndDate = "";
                //    CommonVariables.SearchStartDateLoc = "";
                //    CommonVariables.SearchEndDateLoc = "";
                //    return PartialView("LoginPartialView");
                //}
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("LoginPartialView");
        }


        public DataTable pagedDataTable(DataTable dt, int pagesize, int page)
        {
            DataTable returndt = null;
            DataTable tempdt = new DataTable();
            int startIndex = 0;
            int endIndex = 0;
            int totalPage = 0;
            int intLow = 0;
            int intHigh = 0;
            int seriespoint = 1;
            try
            {
                if (dt.Rows.Count > 0)
                {
                    ViewBag.totalCount = dt.Rows.Count;
                    if (dt.Rows.Count > 0 && dt.Rows.Count < pagesize)
                    {
                        return dt;
                    }
                    else
                    {
                        totalPage = (int)Math.Ceiling((double)dt.Rows.Count / pagesize);
                        seriespoint = (page - 1) / 5;
                        if (seriespoint == 0)
                        {
                            intLow = 1;
                        }
                        else
                        {
                            intLow = seriespoint * 5 + 1;
                        }
                        if (intLow + 4 > totalPage)
                        {
                            intHigh = totalPage;
                        }
                        else
                        {
                            intHigh = intLow + 4;
                        }
                        returndt = dt.Clone();
                        tempdt = dt.Copy();
                        if (page == 1)
                        {
                            startIndex = 0;
                            endIndex = pagesize;
                        }
                        else
                        {
                            startIndex = pagesize * (page - 1);
                            if (totalPage > page)
                            {
                                endIndex = startIndex + pagesize;
                            }
                            else
                            {
                                endIndex = startIndex + (dt.Rows.Count - startIndex);
                            }
                        }
                        ViewBag.intLow = intLow;
                        ViewBag.intHigh = intHigh;
                        for (int i = startIndex; i < endIndex; i++)
                        {
                            DataRow dr = tempdt.NewRow();
                            dr = tempdt.Rows[i];
                            returndt.ImportRow(dr);
                        }

                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return returndt;
        }

        //Get Insert/Edit Login
       // [CustomAuthorizeAttribute(PermCd = PermissionCode.Add)]
        [HttpGet]
        public ActionResult ManageLogin(string p)
        {
            string id = "";
            string strLoginSNO = "";
            string strLoginDate = "";
            string strLogoutDate = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                CheckPermission();
                Utils.setUrl(this.Url);
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                        if (rvd["loginSNO"] != null)
                        {
                            strLoginSNO = rvd["loginSNO"].ToString();
                        }
                    }
                }
                if (id != "")
                {
                    if ((ViewBag.EnableEdit == "true"))
                    {
                        objLogin = objLoginService.Login_Get(id, strLoginSNO);
                        if (objLogin.LoginDt != null)
                        {
                            strLoginDate = objLogin.LoginDt.ToString("yyyy-MM-dd") + " ";
                            if (objLogin.LoginHh > 12)
                            {
                                strLoginDate += (objLogin.LoginHh - 12).ToString();
                            }
                            else
                            {
                                strLoginDate += objLogin.LoginHh.ToString();
                            }
                            strLoginDate += ":" + objLogin.LoginMi + ":" + objLogin.LoginSs;
                            if (objLogin.LoginHh > 12)
                            {
                                strLoginDate += "PM";
                            }
                            else
                            { strLoginDate += "AM"; }

                        }
                        objLogin.LoginFullDt = strLoginDate;
                        if (objLogin.LogoutDt != null)
                        {
                            strLogoutDate = DateTime.Parse(objLogin.LogoutDt.ToString()).ToString("yyyy-MM-dd") + " ";
                            if (objLogin.LogoutHh > 12)
                            {
                                strLogoutDate += (objLogin.LogoutHh - 12).ToString();
                            }
                            else
                            {
                                strLogoutDate += objLogin.LogoutHh.ToString();
                            }
                            strLogoutDate += ":" + objLogin.LogoutMi + ":" + objLogin.LogoutSs;
                            if (objLogin.LogoutHh > 12)
                            {
                                strLogoutDate += "PM";
                            }
                            else
                            { strLogoutDate += "AM"; }

                        }
                        objLogin.LogoutFullDt = strLogoutDate;
                        return View(objLogin);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";
                        return RedirectToAction("LoginList");
                    }
                }
                else
                {
                    if (ViewBag.EnableAdd == "true")
                    {
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";
                        return RedirectToAction("LoginList");
                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View();

        }

        //Post Insert/Edit Login

        [HttpPost]
        public ActionResult ManageLogin(Login objLogin, FormCollection fc)
        {
            bool isSuccess = false;
            Users objUsers = new Users();
            string strUsername = "";
            try
            {
                //  CheckLogin();
                if (fc["btnCancel"] != null)
                {
                    return RedirectToAction("LoginList");
                }
                TryUpdateModel(objLogin);
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                if (ModelState.IsValid)
                {

                    if (String.IsNullOrEmpty(objLogin.UsrCd))
                    {
                        isSuccess = objLoginService.Login_Manage(objLogin, "I");

                    }
                    else
                    {

                        isSuccess = objLoginService.Login_Manage(objLogin, "U");

                    }
                    if (isSuccess)
                    {
                        ViewData["Message"] = "Success";
                        return RedirectToAction("LoginList");
                    }
                    else
                    {
                        ViewData["Message"] = "Failed";
                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("LoginList");
        }

        //Get Delete Login
        [HttpGet]
        [CustomAuthorizeAttribute(PermCd = "4")]
        public ActionResult DeleteLogin(string p)
        {
            string id = "";
            string strLoginSNO="";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                CheckPermission();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                        if (rvd["id"] != null)
                        {
                            strLoginSNO = rvd["loginSNO"].ToString();
                        }
                    }
                }
                if (id != "")
                {
                    objLogin = objLoginService.Login_Get(id, strLoginSNO);
                    if (ViewBag.EnableDelete == "true")
                    {
                        bool isSuccess = objLoginService.Login_Manage(objLogin, "D");
                        if (isSuccess)
                        {
                            ViewData["Message"] = "Success";

                        }
                        else
                        {
                            ViewData["Message"] = "Failed";
                        }
                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("LoginList");
        }

        //Check Permission 
        #region Check Permission
        public void CheckPermission()
        {
            PermissionParamService objPermissionParamService = new PermissionParamService();
            PermissionParam objPermissionParam = new PermissionParam();
            ViewBag.EnableEdit = "false";
            ViewBag.EnableDelete = "false";
            ViewBag.EnableAdd = "false";
            ViewBag.EnableApprove = "false";
            try
            {
                objPermissionParam = objPermissionParamService.GetPermissionValue();
                if (objPermissionParam != null)
                {

                    if (objPermissionParam.EnableAdd == "true")
                    {
                        ViewBag.EnableAdd = "true";
                    }
                    if (objPermissionParam.EnableEdit == "true")
                    {
                        ViewBag.EnableEdit = "true";
                    }
                    if (objPermissionParam.EnableDelete == "true")
                    {
                        ViewBag.EnableDelete = "true";
                    }
                    if (objPermissionParam.EnableApprove == "true")
                    {
                        ViewBag.EnableApprove = "true";
                    }
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


        }
        #endregion

    }
}
