using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Security;
using MIS.Models.Security;
using MIS.Services.Core;
using MIS.Core;
using MIS.Services.Setup;
using System.Data;
using EntityFramework;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Services.AuditTrail;
using MIS.Models.Core;
using MIS.Models.AuditTrail;
using System.Web.Routing;
using MIS.Models.Setup;
using System.IO;
using System.ComponentModel;
using System.Net;
using MIS.Services.Registration.Household;
using System.Configuration;
using System.Web.Script.Serialization;
 
namespace MIS.Controllers
{
    public class HomeController : Controller
    {
        CommonFunction commFunc = null;
        public HomeController()
        {
            commFunc = new CommonFunction();
        }

        public ActionResult Index(string logout)
        {
            try
            {
                Users objUser = new Users();
                Login objLogin = new Login();
                LoginService objLoginService = new LoginService();
                MenuServices objMenuService = new MenuServices();
                string strLoginSerialNo = "";
                string menuid = "";
                DateTime currentDate = System.DateTime.Now;
                DataTable dt = null;
                Utils.GetLanguageList();
                if (Session["LanguageSetting"] == null)
                {
                    Session["LanguageSetting"] = "English";
                }
                // EncryptConnectionString.ProtectConnectionString();
                // EncryptConnectionString.UnprotectConnectionString();                
                if (logout == "true")
                {
                    ViewBag.logout = logout.ConvertToString();
                    //adding the log in the user log table
                    if (Session["loginnumber"] != null)
                    {
                        strLoginSerialNo = Session["loginnumber"].ToString();
                    }
                    if (Session[SessionCheck.sessionName] != null)
                    {
                        objUser = (Users)Session[SessionCheck.sessionName];
                    }
                    if (objUser != null && objUser.usrCd != "admin" && strLoginSerialNo != "")
                    {
                        objLogin = objLoginService.Login_Get(objUser.usrCd, strLoginSerialNo);
                        if (objLogin != null)
                        {
                            objLogin.LogoutDt = currentDate;
                            objLogin.LogoutDtLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", currentDate));
                            objLogin.LogoutHh = currentDate.Hour;
                            objLogin.LogoutMi = currentDate.Minute;
                            objLogin.LogoutSs = currentDate.Second;
                            if (Session["menuCode"] != null)
                            {
                                menuid = Session["menuCode"].ToString().Trim().Replace("'", "");
                            }
                            else
                            {
                                if (Session["PreviousMenuCd"] != null)
                                {
                                    menuid = Session["PreviousMenuCd"].ToString().Trim().Replace("'", "");
                                }
                            }
                            if (menuid != "")
                            {
                                objLogin.MenuCd = menuid;
                                dt = objMenuService.MenuDetailList(menuid);
                                if (dt != null)
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        objLogin.MenuUrl = dt.Rows[0]["link_url"].ToString();
                                    }
                                }
                            }
                            objLoginService.Login_Manage(objLogin, "U");

                        }
                    }
                    //end adding the log in the user log table
                    SessionCheck.EndSession();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.logout = "false";
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
            if (Request.UrlReferrer != null && (Request.UrlReferrer.AbsolutePath != "/" && Request.UrlReferrer.AbsolutePath != "/Home/"))
                ViewBag.SessionCheckScript = new HtmlString("<script type='text/javascript'>$().ready(function () {var hasSessionExpired = '" + Session["SessionExpired"] + "';if (hasSessionExpired == 'True') {  window.location = '" + Url.Action("Index", "Home") + "'; }   });</script>");
            //  Session Check     

            return View();
        }


        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string p = "";
            Users objUser = new Users();
            Login objLogin = new Login();
            Utils.GetLanguageList();
            LoginService objLoginService = new LoginService();
            UsersService objUserService = new UsersService();
            OfficeService objOfficeService = new OfficeService();
            DateTime currentDate = System.DateTime.Now;
            DataTable dt = null;
            string strUsercode = fc["txtUsername"].ConvertToString().ToLower().Trim();
            string strPassword = fc["txtPassword"].ConvertToString().Trim();
            EmailMessage objEmail = new EmailMessage();
            if (fc["btnLogin"] != null)
            {
                 Session["LanguageSetting"] = "English";
                 objUser = objUserService.validateUserLogin(strUsercode, strPassword);

                if (objUser != null)
                {

                    {
                        if (objUser.usrName != null)
                        {
                            string GrpCd = "";
                            CommonVariables.UserName = objUser.usrName;
                            CommonVariables.UserCode = objUser.usrCd;
                            CommonVariables.EmpCode = objUser.empCd;
                            CommonVariables.GroupCD = objUser.GroupCode;
                            CommonVariables.GroupName = objUser.GroupName;

                            Session[SessionCheck.sessionName] = objUser;
                            //Login Information
                            objLogin.UsrCd = objUser.usrCd;
                            objLogin.LoginSno = Decimal.Parse(objLoginService.Login_GetNewLoginID(objUser.usrCd));
                            dt = objOfficeService.GetOfficeListbyUserCode(objLogin.UsrCd);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    objLogin.EmployeeCd = dt.Rows[0]["EMPLOYEE_CD"].ToString();
                                    objLogin.OfficeCd = dt.Rows[0]["OFFICE_CD"].ToString();
                                    Session["UserRegionCD"] = dt.Rows[0]["REG_ST_CD"].ConvertToString();
                                    Session["UserZoneCD"] = dt.Rows[0]["ZONE_CD"].ConvertToString();
                                    Session["UserDistrictCd"] = dt.Rows[0]["DISTRICT_CD"].ConvertToString();
                                    Session["UserGroupFlag"] = "C";
                                    GrpCd = dt.Rows[0]["GRP_CD"].ConvertToString();
                                    Session["liveGrpCd"] = GrpCd;
                                    CommonVariables.OfficeCD = objLogin.OfficeCd;
                                   
                                }
                                else
                                {
                                    objLogin.EmployeeCd = null;
                                    objLogin.OfficeCd = null;
                                    Session["UserRegionCD"] = "2";
                                    Session["UserZoneCD"] = "5";
                                    Session["UserDistrictCd"] = "26";
                                    Session["UserGroupFlag"] = "C";
                                }
                            }
                            else
                            {
                                objLogin.EmployeeCd = null;
                                objLogin.OfficeCd = null;
                            }
                            //  string strDNs = Dns.GetHostName();
                            //CommonVariables.IPAddress = System.Net.Dns.GetHostEntry(strDNs).AddressList[0].ToString();
                            string ip;

                            ip = HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ConvertToString();
                            if (ip == string.Empty)
                            {
                                ip = Request.ServerVariables["REMOTE_ADDR"];
                            }
                            CommonVariables.IPAddress = ip;
                            objLogin.LoginDt = currentDate;
                            objLogin.LoginDtLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", currentDate));
                            objLogin.LoginHh = currentDate.Hour;
                            objLogin.LoginMi = currentDate.Minute;
                            objLogin.LoginSs = currentDate.Second;
                            objLogin.LogoutDt = null;
                            objLogin.LogoutDtLoc = "";
                            objLogin.LogoutHh = null;
                            objLogin.LogoutMi = null;
                            objLogin.LogoutSs = null;
                            objLogin.LoginRemarks = "";
                            objLogin.LogoutRemarks = "";
                            objLogin.ModuleCd = "1";
                            objLogin.MenuCd = null;
                            objLogin.MenuUrl = null;
                            objLogin.IpAddress = CommonVariables.IPAddress;
                            objLoginService.Login_Manage(objLogin, "I");
                            Session["loginnumber"] = objLogin.LoginSno;
                            //End Login Information     
                            Session["UserMenu"] = getUserMenu(objUser.usrCd);
                            Session["KbdType"] = "Traditional";

                            if (GrpCd != null && GrpCd != "")
                            {
                                if (GrpCd == "21")
                                    return RedirectToAction("Index", "DataMisMatch");
                                else
                                {
                                    p = QueryStringEncrypt.EncryptString("/?menuCode=6");
                                    return RedirectToAction("Dashboard", "Dashboard", new { p = p });
                                }
                            }
                            else
                            {
                                p = QueryStringEncrypt.EncryptString("/?menuCode=6");
                                return RedirectToAction("Dashboard", "Dashboard", new { p = p });
                            }

                        }
                    }
                }
                //}
                TempData["Message"] = "Invalid Username Password";
            }
            else if (fc["btnForgotPassword"] != null)
            {

                if (strUsercode != "admin")
                {
                    objUser = objUserService.getUserbyEmail(strUsercode);
                    if (objUser != null)
                    {
                        //objEmail.To = "sunil.bajracharya@spi.com";
                        objEmail.To = objUser.email;
                        objEmail.From = MailSend.adminEmail;
                        objEmail.Subject = "Password Information";
                        objEmail.Body = EmailTemplate.ForgotPassword(objUser);
                        bool mailSend = MailSend.SendMail(objEmail);
                        if (mailSend)
                        {
                            TempData["Message"] = "Password send in your email.";
                        }
                        else
                        {
                            TempData["Message"] = "Please enter your valid email.";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Please enter your valid email.";
                    }
                }
                else
                {
                    TempData["Message"] = "Cannot change admin password.";
                }
            }
            return RedirectToAction("Index");

        }

        public ActionResult ChangeStatus(string Languageset)
        {
            if (Languageset == "English")
            {
                Session["LanguageSetting"] = "Nepali";
            }
            else
            {
                Session["LanguageSetting"] = "English";
            }
            string url = HttpContext.Request.UrlReferrer.AbsoluteUri;
            //return RedirectToAction(CommonVariables.currentAction, CommonVariables.currentController);
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            else if (Request.UrlReferrer.Segments.Length >= 3)
                return RedirectToAction(Request.UrlReferrer.Segments[2].Replace("/", ""), Request.UrlReferrer.Segments[1].Replace("/", ""));
            else
                return RedirectToAction("Dashboard", "Dashboard");
        }


        public ActionResult ChangeKeyboardType(string kbdType)
        {
            if (!String.IsNullOrEmpty(kbdType))
                Session["KbdType"] = kbdType;
            else
                Session["KbdType"] = "Traditional";
            string url = HttpContext.Request.UrlReferrer.AbsoluteUri;
            //return RedirectToAction(CommonVariables.currentAction, CommonVariables.currentController);
            return RedirectToAction(Request.UrlReferrer.Segments[2].Replace("/", ""), Request.UrlReferrer.Segments[1].Replace("/", ""));
        }


        public ActionResult ResetPassword()
        {
            Session["LanguageSetting"] = "English";
            return View();
        }


        [HttpPost]
        public ActionResult ResetPassword(Users users, FormCollection fc)
        {
            Session["LanguageSetting"] = "English";
            UsersService objUserService = new UsersService();
            Users objUser = new Users();
            EmailMessage objEmail = new EmailMessage();
            if (fc["btn_Submit"] != null)
            {
                objUser = objUserService.getUserbyEmail(fc["email"].ConvertToString());
                if (objUser != null)
                {
                    string aa = objUserService.GenerateRandomPassword();
                    objUser.password = Utils.EncryptString(aa);
                    objUser.expiryDtLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today.AddDays(364)));
                    objUser.expiryDt = DateTime.Today.AddDays(364);
                    objUserService.UserUID(objUser, null, "U");
                    objUser = new Users();
                    objUser = objUserService.getUserbyEmail(fc["email"].ConvertToString());
                    objEmail.To = objUser.email;
                    objEmail.From = MailSend.adminEmail;
                    objEmail.Subject = "Password Information";
                    objEmail.Body = EmailTemplate.ForgotPassword(objUser);
                    bool mailSend = MailSend.SendMail(objEmail);
                    if (mailSend)
                    {
                        TempData["Message"] = "Password has been sent in your email.";
                    }
                    else
                    {
                        TempData["Message"] = "Password has been sent in your email.";
                    }
                    ModelState.Clear();
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("ClientName", "Email Address does not Exists!!");
                }
            }
            return View();
        }

        private DataTable getUserMenu(string userCd)
        {
            DataTable dt = new DataTable();

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "";
                if (userCd == null || userCd == "admin")
                {
                    cmdText = "select MENU_CD, UPPER_MENU_CD, LABEL,  LINK_URL,  MENU_ORDER,Icon_url,LABEL_LOC,MENU_TYPE from COM_MENU " +
                                      "START with (UPPER_MENU_CD is null OR UPPER_MENU_CD='ROOT')  " +
                                          "CONNECT by prior MENU_CD=UPPER_MENU_CD order by  MENU_CD,MENU_ORDER";


                }
                else
                {
                    cmdText = " select * from (select a.MENU_CD, a.UPPER_MENU_CD, a.LABEL,  a.LINK_URL,  a.MENU_ORDER,a.Icon_url,a.LABEL_LOC,a.MENU_TYPE from COM_MENU   a " +
                    " ,com_menu_security mnusec where a.disabled='N' " +
                    "AND a.menu_cd = mnusec.menu_cd " +
                    "AND mnusec.grp_cd =  (select wug.GRP_CD from COM_WEB_USR_GRP wug where wug.USR_CD='" + userCd + "') " +
                    "  AND mnusec.perm_cd = '2' )  T1 " +
                    " START with (T1.UPPER_MENU_CD is null OR T1.UPPER_MENU_CD='ROOT') " +
                    "CONNECT by prior T1.MENU_CD=T1.UPPER_MENU_CD order by  T1.MENU_CD,T1.MENU_ORDER";
                }
                try
                {
                    service.Begin();
                    DataTable dtbl = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {
                            // USER_CD = userCd
                        }
                    });
                    dt = dtbl;
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {

                    if (service.Transaction != null && service.Transaction.Connection != null)
                    {
                        service.End();
                    }
                }

            }


            return dt;
        }

        //[CustomAuthorizeAttribute(PermCd = "3")]
        public ActionResult AddUser(string editid, FormCollection fc)
        {
            Session["LanguageSetting"] = "English";
            MISUserRegistration Employee = new MISUserRegistration();
            EmployeeService obj = new EmployeeService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                ViewData["ddl_Districts"] = commFunc.GetDistricts(fc["ddl_Districts"]);
                //ViewData["ddl_RegState"] = GetRegionState(GetData.GetDefinedCodeFor(DataType.Region, fc["ddl_RegState"]));
                //ViewData["ddl_Zone"] = commFunc.GetZone(fc["ddl_Zone"]);

                ViewData["ddl_VDCMun"] = commFunc.GetVDCMunByDistrict(fc["ddl_VDCMun"], fc["ddl_Districts"]);
                ViewData["ddl_Ward"] = commFunc.GetWardByVDCMun(fc["ddl_Ward"], fc["ddl_VDCMun"]);

                //ViewData["ddl_Group"] = commFunc.GetUserGroup(fc["ddl_Group"]);
                ViewData["ddl_Gender"] = commFunc.GetGender(fc["ddl_Gender"]);
                ViewData["ddl_Office"] = commFunc.GetOffice(fc["ddl_Office"]);
                ViewData["ddl_Position"] = commFunc.GetPosition(fc["ddl_Position"]);
                ViewData["ddl_Designation"] = commFunc.GetDesignationByPosition(fc["ddl_Designation"], GetData.GetCodeFor(DataType.Position, fc["ddl_Position"]));
                ViewData["ddl_MaritalStatus"] = commFunc.GetMaritalStatus(fc["ddl_MaritalStatus"]);

                Employee.ImageURL = "../../Files/images/employee/anon1.jpg";

                //Employee.BirthDateLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today));
                //Employee.BirthDate = DateTime.Today;

                Employee.OfficeJoinedDateLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today));
                Employee.OfficeJoinedDate = DateTime.Today;

                Utils.setUrl(this.Url);

                ViewData["step"] = "1";
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            return View(Employee);
        }

        //[CustomAuthorizeAttribute(PermCd = "3")]
        [HttpPost]
        public ActionResult AddUser(MISUserRegistration Employee, FormCollection fc, Container container, HttpPostedFileBase file)
        {
            string step = string.Empty;
            string errMsg = string.Empty;
            EmployeeService employeeService = new EmployeeService();
            ComEmployeeInfo objPosted = (Session["PostedComEmployeeInfo"] != null) ? (ComEmployeeInfo)Session["PostedComEmployeeInfo"] : null;
            try
            {


                string AllCd = Employee.EmployeeCd;
                string usercode = Employee.UserCode;
                Employee.ApprovedBy = SessionCheck.getSessionUsername();
                Employee.ApprovedDate = DateTime.Now;
                Employee.EnteredBy = SessionCheck.getSessionUsername();
                Employee.EnteredDate = DateTime.Now;
                if ((AllCd == "0" || AllCd == "" || AllCd == null))
                {
                    step = (fc["hdnStep"] != null) ? fc["hdnStep"] : "1";

                    if (fc["btn_Back"] == null)
                    {
                        if (step == "1")
                        {
                            if (Session["ImageName"] != null)
                            {
                                Employee.ImageURL = "../../Files/images/employee/" + fc["DefinedCd"].ConvertToString() + "_" + Session["ImageName"];
                                string path = Server.MapPath("~/Files/images/employee/");
                                string Fromfile = path + Session["ImageName"];
                                string Tofile = path + fc["DefinedCd"] + "_" + Session["ImageName"];

                                FileInfo fi = new FileInfo(Fromfile);
                                if (fi.Exists)
                                {
                                    if (System.IO.File.Exists(Tofile))
                                        System.IO.File.Delete(Tofile);
                                    fi.MoveTo(Tofile);
                                }
                            }
                            else
                                Employee.ImageURL = fc["hdnImageURL"].ConvertToString();

                            Employee.Mode = "I";
                            employeeService.ManageUser(Employee, step, out errMsg);

                            if (string.IsNullOrWhiteSpace(errMsg))
                                ViewData["step"] = "2";
                            else
                                ViewData["step"] = step;
                        }
                        else if (step == "2")
                        {
                            Employee.Mode = "I";
                            employeeService.ManageUser(Employee, step, out errMsg);

                            if (string.IsNullOrWhiteSpace(errMsg))
                                ViewData["step"] = "3";
                            else
                                ViewData["step"] = step;
                        }
                        //else if (step == "3")
                        //{
                        //    Employee.Mode = "I";
                        //    employeeService.ManageUser(Employee, step, out errMsg);

                        //    if (string.IsNullOrWhiteSpace(errMsg))
                        //        ViewData["step"] = "4";
                        //    else
                        //        ViewData["step"] = step;
                        //}
                    }
                    else
                    {
                        if (step == "2")
                            ViewData["step"] = "1";
                        //else if (step == "3")
                        //    ViewData["step"] = "2";
                    }

                    if (ViewData["step"].ConvertToString() == "1")
                    {
                        if (objPosted != null)
                        {
                            Employee.FirstNameEng = objPosted.FirstNameEng;
                            Employee.MiddleNameEng = objPosted.MiddleNameEng;
                            Employee.LastNameEng = objPosted.LastNameEng;
                            Employee.FullNameEng = objPosted.FullNameEng;
                            Employee.FirstNameLoc = objPosted.FirstNameLoc;
                            Employee.MiddleNameLoc = objPosted.MiddleNameLoc;
                            Employee.LastNameLoc = objPosted.LastNameLoc;
                            Employee.FullNameLoc = objPosted.FullNameLoc;
                            Employee.ImageURL = objPosted.ImageURL;
                            Employee.GenderCd = GetData.GetDefinedCodeFor(DataType.Gender, objPosted.GenderCd);
                            ViewData["ddl_Gender"] = commFunc.GetGender(GetData.GetDefinedCodeFor(DataType.Gender, objPosted.GenderCd));
                            Employee.BirthDate = objPosted.BirthDt.ToDateTime("dd-MM-yyyy");
                            Employee.BirthDateLoc = objPosted.BirthDtLoc;
                            Employee.PerDistrictCd = GetData.GetDefinedCodeFor(DataType.District, objPosted.PerDistrictCd);
                            ViewData["ddl_Districts"] = commFunc.GetDistricts(GetData.GetDefinedCodeFor(DataType.District, objPosted.PerDistrictCd));
                            //ViewData["ddl_RegState"] = GetRegionState(fc["ddl_RegState"]);
                            //ViewData["ddl_Zone"] = GetZone(fc["ddl_Zone"]);
                            Employee.PerVdcMunCd = GetData.GetDefinedCodeFor(DataType.VdcMun, objPosted.PerVdcMunCd);
                            ViewData["ddl_VDCMun"] = commFunc.GetVDCMunByDistrict(GetData.GetDefinedCodeFor(DataType.VdcMun, objPosted.PerVdcMunCd), GetData.GetDefinedCodeFor(DataType.District, objPosted.PerDistrictCd));
                            Employee.PerWardNo = objPosted.PerWardNo;
                            ViewData["ddl_Ward"] = commFunc.GetWardByVDCMun(objPosted.PerWardNo, GetData.GetDefinedCodeFor(DataType.VdcMun, objPosted.PerVdcMunCd));
                            if (Session["LanguageSetting"] == null || Session["LanguageSetting"].ConvertToString() == "English")
                                Employee.PerStreet = objPosted.PerStreet;
                            else
                                Employee.PerStreetLoc = objPosted.PerStreetLoc;
                            Employee.MaritalStatusCd = GetData.GetDefinedCodeFor(DataType.MaritalStatus, objPosted.MaritalStatusCd);
                            ViewData["ddl_MaritalStatus"] = commFunc.GetMaritalStatus(GetData.GetDefinedCodeFor(DataType.MaritalStatus, objPosted.MaritalStatusCd));

                            Employee.OfficeCd = GetData.GetDefinedCodeFor(DataType.Office, objPosted.OfficeCd);
                            ViewData["ddl_Office"] = commFunc.GetOffice(GetData.GetDefinedCodeFor(DataType.Office, objPosted.OfficeCd));
                            Employee.PositionCd = GetData.GetDefinedCodeFor(DataType.Position, objPosted.PositionCd);
                            ViewData["ddl_Position"] = commFunc.GetPosition(GetData.GetDefinedCodeFor(DataType.Position, objPosted.PositionCd));
                            Employee.DesignationCd = GetData.GetDefinedCodeFor(DataType.Designation, objPosted.DesignationCd, GetData.GetDefinedCodeFor(DataType.Position, objPosted.PositionCd));
                            ViewData["ddl_Designation"] = commFunc.GetDesignationByPosition(GetData.GetDefinedCodeFor(DataType.Designation, objPosted.DesignationCd, GetData.GetDefinedCodeFor(DataType.Position, objPosted.PositionCd)), objPosted.PositionCd);
                            Employee.OfficeJoinedDate = objPosted.OfficeJoinDt.ToDateTime("dd-MM-yyyy");
                            Employee.OfficeJoinedDateLoc = objPosted.OfficeJoinDtLoc;
                            Employee.DefinedCd = objPosted.DefEmployeeCd;
                        }
                        else
                        {
                            ViewData["ddl_Districts"] = commFunc.GetDistricts(fc["ddl_Districts"]);
                            //ViewData["ddl_RegState"] = GetRegionState(fc["ddl_RegState"]);
                            //ViewData["ddl_Zone"] = GetZone(fc["ddl_Zone"]);
                            ViewData["ddl_VDCMun"] = commFunc.GetVDCMunByDistrict(fc["ddl_VDCMun"], fc["ddl_Districts"]);
                            ViewData["ddl_Ward"] = commFunc.GetWardByVDCMun(fc["ddl_Ward"], fc["ddl_VDCMun"]);
                            ViewData["ddl_Gender"] = commFunc.GetGender(fc["ddl_Gender"]);
                            ViewData["ddl_MaritalStatus"] = commFunc.GetMaritalStatus(fc["ddl_MaritalStatus"]);

                            ViewData["ddl_Office"] = commFunc.GetOffice(fc["ddl_Office"]);
                            ViewData["ddl_Position"] = commFunc.GetPosition(fc["ddl_Position"]);
                            ViewData["ddl_Designation"] = commFunc.GetDesignationByPosition(fc["ddl_Designation"], GetData.GetCodeFor(DataType.Position, fc["ddl_Position"]));
                        }
                    }
                    else if (ViewData["step"].ConvertToString() == "2")
                    {
                        //ViewData["ddl_Group"] = commFunc.GetUserGroup(fc["ddl_Group"]);
                    }

                    //}
                    //else
                    //{

                    //    if (Session["ImageName"] != null)
                    //    {
                    //        Employee.ImageURL = "../../Files/images/employee/" + fc["DefinedCd"] + "_" + Session["ImageName"];
                    //    }
                    //    Employee.Mode = "I";
                    //    employeeService.ManageEmployee(Employee);

                    //}
                }

                #region Commented the Edit Portion
                //else
                //{

                //    //if (usercode == "" || usercode == null)
                //    //{
                //    if (Session["ImageName"] != null)
                //    {
                //        Employee.ImageURL = "../../Files/images/employee/" + fc["DefinedCd"] + "_" + Session["ImageName"];

                //        string path = Server.MapPath("~/Files/images/employee/");
                //        string Fromfile = path + Session["ImageName"];
                //        string Tofile = path + fc["DefinedCd"] + "_" + Session["ImageName"];

                //        FileInfo fi = new FileInfo(Fromfile);
                //        if (fi.Exists)
                //        {
                //            fi.MoveTo(Tofile);
                //        }
                //    }
                //    else
                //    {
                //        Employee.ImageURL = Session["UpdatedImageName"].ToString();
                //    }
                //    Employee.Mode = "U";
                //    Employee.EmployeeCd = AllCd;
                //    employeeService.ManageEmployee(Employee);
                //    //}
                //    //else
                //    //{

                //    //        if (Session["ImageName"] != null)
                //    //        {
                //    //            Employee.ImageURL = "../../Files/images/employee/" + fc["DefinedCd"] + "_" + Session["ImageName"];
                //    //        }
                //    //        else
                //    //        {
                //    //            Employee.ImageURL = Session["UpdatedImageName"].ToString();
                //    //        }
                //    //        Employee.Mode = "U";
                //    //        Employee.EmployeeCd = AllCd;
                //    //        employeeService.ManageEmployee(Employee);

                //    //}

                //} 
                #endregion

                Session["ImageName"] = null;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            if (ViewData["step"] == null)
            {
                if (!string.IsNullOrWhiteSpace(errMsg)) TempData["ErrorMessage"] = errMsg;
                return View(Employee);
            }
            else if (ViewData["step"].ConvertToString() == "1" || ViewData["step"].ConvertToString() == "2")
            {
                if (!string.IsNullOrWhiteSpace(errMsg)) TempData["ErrorMessage"] = errMsg;
                return View(Employee);
            }
            else if (ViewData["step"].ConvertToString() == "3")
            {
                TempData["Message"] = "The User has been created successfully";
                Session["PostedComEmployeeInfo"] = null;
                return View("Index");
            }
            else
                return View("Index");
        }

        [HttpPost]
        public JsonResult CheckDuplicateDefinedCode(string id, string empCode)
        {
            bool boolValue = false;
            EmployeeService service = new EmployeeService();
            boolValue = service.CheckDuplicateDefinedCode(id, empCode);
            return new JsonResult { Data = boolValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult CheckDuplicateEmail(string id, string empCode)
        {
            bool boolValue = false;
            EmployeeService service = new EmployeeService();
            boolValue = service.CheckDuplicateUserCode(id, empCode);
            return new JsonResult { Data = boolValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public ContentResult UploadFiles()
        {
            var r = new List<ViewDataUploadFilesResult>();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string savedFileName = Path.Combine(Server.MapPath("~/Files/images/employee"), Path.GetFileName(hpf.FileName));
                if (hpf.ContentLength <= (1024 * 1024))
                {
                    hpf.SaveAs(savedFileName);

                    r.Add(new ViewDataUploadFilesResult()
                    {
                        Name = hpf.FileName,
                        Length = hpf.ContentLength,
                        Type = hpf.ContentType
                    });
                    Session["ImageName"] = hpf.FileName;
                }
            }
            if (hpf.ContentLength <= (1024 * 1024))
            {
                return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            }
            else
            {
                return Content("{\"name\":\"" + "name" + "\",\"type\":\"" + "type" + "\",\"size\":\"" + "size" + "\"}", "application/json");
            }
        }

        #region "jason data"
        public JsonResult CheckEmail(string email)
        {
            UsersService userService = new UsersService();
            string checkEmail = userService.CheckDuplicateEmail(email, "").ToString().ToLower();
            return new JsonResult { Data = new { checkEmail = checkEmail }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        /// <summary>
        /// change date 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeDate(string id)
        {
            string NewNepaliDate = NepaliDate.getNepaliDate(id);
            return new JsonResult { Data = new { newNepaliDate = NewNepaliDate } };

        }

        /// <summary>
        /// change date 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Age(string id)
        {
            string CalAge = CommonFunction.GetAge(id);
            return new JsonResult { Data = new { calAge = CalAge } };

        }

        /// <summary>
        /// change date 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        public JsonResult ChangeDateToEnglish(string id)
        {
            bool isException = false;
            DateTime NewEnglishDate = new DateTime();
            try
            {
                NewEnglishDate = Convert.ToDateTime(NepaliDate.getEnglishDate(id));
            }
            catch (OracleException oe)
            {
                isException = true;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                isException = true;
                ExceptionManager.AppendLog(ex);
            }


            if (isException == true)
            {
                return new JsonResult { Data = new { newEnglishDate = "" } };
            }
            else
            {
                return new JsonResult { Data = new { newEnglishDate = NewEnglishDate.ToString("dd-MMMM-yyyy") } };
                //return new JsonResult { Data = new { newEnglishDate = NewEnglishDate.ToString("dd-MM-yyyy") } };
            }
        }

        //Added By Chandra Prakash 
        [HttpPost]
        public JsonResult DateNp2Eng(string dateNp)
        {
            bool isException = false;
            DateTime NewEnglishDate = new DateTime();
            try
            {
                NewEnglishDate = Convert.ToDateTime(NepaliDate.getEnglishDate(dateNp));
            }
            catch (OracleException oe)
            {
                isException = true;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                isException = true;
                ExceptionManager.AppendLog(ex);
            }


            if (isException == true)
            {
                return new JsonResult { Data = new { newEnglishDate = "" } };
            }
            else
            {
                return new JsonResult { Data = new { newEnglishDate = NewEnglishDate.ToString("dd-MM-yyyy") } };
            }
        }


        /// <summary>
        /// Convert Month 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ConvertMonth(string id)
        {
            string ConvertedMonth = "";
            if (id == "January")
            {
                ConvertedMonth = "01";
            }
            else if (id == "February")
            {
                ConvertedMonth = "02";
            }
            else if (id == "March")
            {
                ConvertedMonth = "03";
            }
            else if (id == "April")
            {
                ConvertedMonth = "04";
            }
            else if (id == "May")
            {
                ConvertedMonth = "05";
            }
            else if (id == "June")
            {
                ConvertedMonth = "06";
            }
            else if (id == "July")
            {
                ConvertedMonth = "07";
            }
            else if (id == "August")
            {
                ConvertedMonth = "08";
            }
            else if (id == "September")
            {
                ConvertedMonth = "09";
            }
            else if (id == "October")
            {
                ConvertedMonth = "10";
            }
            else if (id == "November")
            {
                ConvertedMonth = "11";
            }
            else if (id == "December")
            {
                ConvertedMonth = "12";
            }
            return new JsonResult { Data = new { convertedMonth = ConvertedMonth } };

        }

        
        #endregion

        public JsonResult IndexCheck(string username, string password, string CodeSend)
        {
            try
            {
                string p = "";
                Users objUser = new Users();
                Login objLogin = new Login();
                Utils.GetLanguageList();
                LoginService objLoginService = new LoginService();
                UsersService objUserService = new UsersService();
                OfficeService objOfficeService = new OfficeService();
                DateTime currentDate = System.DateTime.Now;
                DataTable dt = null;
                string strUsercode = username.ToLower().Trim();
                string strPassword = password.ConvertToString().Trim();
                if (Session["LanguageSetting"] == null)
                {
                    Session["LanguageSetting"] = "Nepali";
                }

                objUser = objUserService.validateUserLogin(strUsercode, strPassword);
                //if (objUser != null)
                //{
                    //objUser.VerificationRequired = "N";
                    if (objUser.VerificationRequired == "N" || objUser.VerificationRequired == null)
                    {
                        //do nothing
                    }

                    else
                    {
                        if (objUser.verificationvalidity == null || objUser.verificationvalidity == "" || (System.DateTime.Now.ToString("dd-MMM-yy") != objUser.verificationvalidity))
                        {
                            string cmdtextt = "UPDATE  COM_WEB_USR SET VERIFICATION_VALIDITY='" + System.DateTime.Now.ToString("dd-MMM-yy") + "'   WHERE EMAIL='" + strUsercode.ToLower().Trim() + "' AND PASSWORD='" + Utils.EncryptString(strPassword) + "'  ";
                            UpdateValidationDate(cmdtextt);
                        }
                        if (CodeSend == "Y" && (System.DateTime.Now.ToString("dd-MMM-yy") == objUser.verificationvalidity))
                        {
                            objUser.VerificationRequired = "Y";
                        }
                        if (CodeSend == "Y" && (System.DateTime.Now.ToString("dd-MMM-yy") != objUser.verificationvalidity))
                        {
                            objUser.VerificationRequired = "Y";
                        }
                        if ((CodeSend == "" || CodeSend == null) && (System.DateTime.Now.ToString("dd-MMM-yy") == objUser.verificationvalidity))
                        {
                            objUser.VerificationRequired = "N";
                        }
                        if ((CodeSend == "" || CodeSend == null) && (System.DateTime.Now.ToString("dd-MMM-yy") != objUser.verificationvalidity))
                        {
                            objUser.VerificationRequired = "Y";
                        }
                    }
                //}
                return new JsonResult { Data = objUser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception)
            {
                return new JsonResult { Data ="Exception Error", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
        }
        //validation date update
        public bool UpdateValidationDate(string updateMessage)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(updateMessage);

                }
                catch (OracleException oe)
                {

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {

                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }
        public string SendSMS(string username, string password,string resend)
        {
            string sms = "";
            Users objUser = new Users();
            Utils.GetLanguageList();
            UsersService objUserService = new UsersService();
            string strUsercode = username.ToLower().Trim();
            string strPassword = password.ConvertToString().Trim();
            if (Session["LanguageSetting"] == null)
            {
                Session["LanguageSetting"] = "Nepali";
            }

            objUser = objUserService.GetUserDetail(strUsercode, strPassword);
            Random random = new Random();
            string verifyCode = string.Empty;
            if (resend=="Y")
            {
                  verifyCode = random.Next(0, 9999).ToString("D4");
                //objUserService.UpdateVerificationCode(objUser.usrCd, verifyCode, DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
                sms = PostSendSMS(ConfigurationManager.AppSettings["SMSFrom"], ConfigurationManager.AppSettings["SMSToken"], objUser.mobilenumber, "Your MIS PIN Code for Today is: " + verifyCode);
                
            }
            else
            {
                if (string.IsNullOrEmpty(objUser.verificationvalidity) || Convert.ToDateTime(objUser.verificationvalidity) < DateTime.Now)
                {
                      verifyCode = random.Next(0, 9999).ToString("D4");
                      //sms = PostSendSMS(ConfigurationManager.AppSettings["SMSFrom"], ConfigurationManager.AppSettings["SMSToken"], objUser.mobilenumber, "Your MIS PIN Code for Today is: " + verifyCode);
                    
                }
                //else
                //{
                //    if (Convert.ToDateTime(objUser.verificationvalidity) < DateTime.Now)
                //    {
                //          verifyCode = random.Next(0, 9999).ToString("D4");
                //       // objUserService.UpdateVerificationCode(objUser.usrCd, verifyCode, DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
                //        sms = PostSendSMS(ConfigurationManager.AppSettings["SMSFrom"], ConfigurationManager.AppSettings["SMSToken"], objUser.mobilenumber, "Your MIS PIN Code for Today is: " + verifyCode);
                         
                //    }
                //}
            }
            if(sms.Trim() !="The remote server returned an error: (403) Forbidden.")
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic item = serializer.Deserialize<object>(sms);
                //string sucesscode = item["response_code"].ToString();
                //if (sucesscode == "200")
                //{
                //    objUserService.UpdateVerificationCode(objUser.usrCd, verifyCode, DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"));
                //}
            }
            
            return sms;
        }

        
        private static string PostSendSMS(string from, string token, string to, string text)
        {
            using (var client = new WebClient())
            {
                var values = new System.Collections.Specialized.NameValueCollection();
                values["from"] = from;
                values["token"] = token;
                values["to"] = to;
                values["text"] = text;
                var responseString = "";
                try
                {
                    var response = client.UploadValues("http://api.sparrowsms.com/v2/sms/", "Post", values);
                    responseString = System.Text.Encoding.Default.GetString(response);
                }
               catch(Exception ex)
                {
                    return ex.Message;
                }
                
               // string test = item["test"];
                //responseString = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(responseString));
                return responseString;
            }
        }
    }
}
