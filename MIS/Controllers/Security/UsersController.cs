using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS;
using MIS.Services.Security;
using System.Data;
using MIS.Models.Security;
using System.Web.UI;
using System.Web.Routing;
using MIS.Services.Core;
using MIS.Authentication;
using MIS.Core;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Models.Core;
using System.Web.Security;

namespace MIS.Controllers.Security
{
    public class UsersController : BaseController
    {
        GroupService userGroupService = null;
        UsersService userService = null;
        CommonFunction commonFun = null;
        //Constructor
        public UsersController()
        {
            userGroupService = new GroupService();
            userService = new UsersService();
            commonFun = new CommonFunction();
        }

        //Redirects to UsersList Page with populated list of users
        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult UsersList(FormCollection fc, string p)
        {
            CheckPermission();
            Utils.setUrl(this.Url);
            string initial = "";
            string orderby = "usr_cd";
            string order = "asc";
            IDictionary<string, object> dictionUsrCode = new Dictionary<string, object>();
            IDictionary<string, object> dictionUsrName = new Dictionary<string, object>();
            IDictionary<string, object> dictionEmailAdd = new Dictionary<string, object>();
            IDictionary<string, object> dictionExpDate = new Dictionary<string, object>();
            IDictionary<string, object> dictionGroupName = new Dictionary<string, object>();
            IDictionary<string, object> dictionApproved = new Dictionary<string, object>();
            RouteValueDictionary rvd = new RouteValueDictionary();
            UsersService userService = new UsersService();
            DataTable dt = new DataTable();
            ViewBag.initial = "";
            ViewBag.actionName = "Search";
            commonFun = new CommonFunction();
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["initial"] != null)
                        {
                            initial = rvd["initial"].ToString();
                        }
                        if (rvd["orderby"] != null)
                        {
                            orderby = rvd["orderby"].ToString();
                        }
                        if (rvd["order"] != null)
                        {
                            order = rvd["order"].ToString();
                        }
                    }
                }
                string nextorder = "desc";
                if (initial == null)
                {
                    initial = "";
                }
                else
                {
                    dictionUsrCode.Add("initial", initial);
                    dictionUsrName.Add("initial", initial);
                    dictionEmailAdd.Add("initial", initial);
                    dictionExpDate.Add("initial", initial);
                    dictionGroupName.Add("initial", initial);
                    dictionApproved.Add("initial", initial);
                }
                if (orderby == null)
                {
                    orderby = "usr_name";
                }
                dictionUsrCode.Add("orderby", "usr_cd");
                dictionUsrName.Add("orderby", "usr_name");
                dictionEmailAdd.Add("orderby" ,"email");
                dictionExpDate.Add("orderby", "expiry_dt");
                dictionGroupName.Add("orderby", "grp_name");
                dictionApproved.Add("orderby", "approved");
                if (order == null)
                {
                    order = "asc";
                }
                ViewBag.initial = initial;
                dt = userService.SearchUsers(initial, CommonVariables.SearchLoginName, CommonVariables.SearchUserName, CommonVariables.SearchGroupCode, orderby, order);
                ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(CommonVariables.SearchGroupCode);
                ViewData["gv_FillUser"] = dt;
                ViewData["txt_Email"] = CommonVariables.SearchLoginName;
                ViewData["txt_UserName"] = CommonVariables.SearchUserName;
                ViewBag.actionName = "UsersList";
                ViewData["orderby"] = orderby;
                ViewBag.order = order;
                ViewBag.controllerName = "Users";
                if (order == "desc")
                {
                    nextorder = "asc";
                }
                ViewBag.nextorder = nextorder;
                dictionUsrCode.Add("order", nextorder);
                dictionUsrName.Add("order", nextorder);
                dictionExpDate.Add("order", nextorder);
                dictionGroupName.Add("order", nextorder);
                dictionApproved.Add("order", nextorder);
                RouteValueDictionary routeDictionaryUsrCode = new RouteValueDictionary(dictionUsrCode);
                RouteValueDictionary routeDictionaryUsrName = new RouteValueDictionary(dictionUsrName);
                RouteValueDictionary routeDictionaryExpDate = new RouteValueDictionary(dictionExpDate);
                RouteValueDictionary routeDictionaryGroupName = new RouteValueDictionary(dictionGroupName);
                RouteValueDictionary routeDictionaryApproved = new RouteValueDictionary(dictionApproved);
                ViewBag.RouteUsrCode = QueryStringEncrypt.EncryptString(routeDictionaryUsrCode);
                ViewBag.RouteUsrName = QueryStringEncrypt.EncryptString(routeDictionaryUsrName);
                ViewBag.RouteExpDate = QueryStringEncrypt.EncryptString(routeDictionaryExpDate);
                ViewBag.RouteGroupName = QueryStringEncrypt.EncryptString(routeDictionaryGroupName);
                ViewBag.RouteApproved = QueryStringEncrypt.EncryptString(routeDictionaryApproved);
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

        //Search Users
        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        [HttpPost]
        public ActionResult UsersList(FormCollection fc)
        {
            try
            {
                if (fc["btn_Submit"] == (Utils.GetLabel("Search")))
                {
                    CheckPermission();
                    CommonVariables.SearchUserName = Convert.ToString(fc["txt_UserName"]);
                    CommonVariables.SearchLoginName = Convert.ToString(fc["txt_Email"]);
                    CommonVariables.SearchGroupCode = Convert.ToString(fc["ddl_UserInGroup"]);
                    UsersService userService = new UsersService();
                    DataTable dt = new DataTable();
                    dt = userService.SearchUsers("", HttpUtility.HtmlEncode(CommonVariables.SearchLoginName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchUserName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchGroupCode.Trim()), "", "");
                    ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(fc["ddl_UserInGroup"]);
                    ViewData["gv_FillUser"] = dt;
                    ViewData["txt_Email"] = CommonVariables.SearchLoginName;
                    ViewData["groupCode"] = CommonVariables.SearchGroupCode;
                    ViewData["txt_UserName"] = CommonVariables.SearchUserName;
                    ViewBag.actionName = "UsersList";
                    ViewBag.initial = "";
                }
                else
                {
                    CommonVariables.SearchLoginName = "";
                    CommonVariables.SearchGroupCode = "";
                    CommonVariables.SearchUserName = "";
                    return RedirectToAction("UsersList");
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

        //Add and Edit Users
        [CustomAuthorizeAttribute(PermCd = "3")]
        [HttpPost]
        public ActionResult ManageUser(Users users, FormCollection fc)
        {

            try
            {
                if (fc["btn_Submit"] == (Utils.GetLabel("Submit")))
                {
                    Group group = new Group();
                    string strUserName = string.Empty;
                    string nepexpdt;
                    Users objUsers = new Users();
                    UsersService userService = new UsersService();
                    ViewData["ddl_Districts"] = GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali"));
                    ViewData["ddl_RegState"] = new List<SelectListItem>();
                    ViewData["ddl_Zone"] = new List<SelectListItem>();
                    ViewData["ddl_Office"] = GetData.AllOffices(Utils.ToggleLanguage("english", "nepali"));
                    ViewData["ddl_Position"] = GetData.AllPositions(Utils.ToggleLanguage("english", "nepali"));
                    //ViewData["ddl_PositionSubClass"] = GetData.AllSubClasses(Utils.ToggleLanguage("english", "nepali"));
                    ViewData["ddl_VDCMun"] = new List<SelectListItem>();
                    ViewData["ddl_Ward"] = new List<SelectListItem>();
                    ViewData["ddl_Verification"] = (List<SelectListItem>)commonFun.GetMigration("");
                    ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(fc["ddl_UserInGroup"]).Where(x => x.Value.ToString() != "55");
                    ViewData["ddl_Donor"] = commonFun.getDonorList("");

                    DataTable dt = new DataTable();
                    ViewData["gv_ManageUserList"] = dt;
                    DateTime expDt = users.expiryDt;
                    nepexpdt = users.expiryDtLoc;
                    if (expDt <= DateTime.Today)
                    {
                        ModelState.AddModelError("ClientName", "Expiry date should be greater than today's date!!");
                    }
                    else
                    {
                        if (Session[SessionCheck.sessionName] != null)
                        {
                            objUsers = (Users)Session[SessionCheck.sessionName];
                            strUserName = objUsers.usrName;
                        }
                        TryUpdateModel(users);
                        //users.password = Utils.EncryptString(users.password);
                        if (users.editMode != "Edit")
                        {
                            if (userService.CheckDuplicateEmail(users.email, users.userCodeCheck))
                            {
                                if (userService.CheckDuplicateUsers(users.usrCd.ConvertToString().ToUpper(), users.empCd))
                                {
                                    if(users.isbankUser==true)
                                    {
                                        users.bankCode = users.bankCode;
                                    }
                                    else
                                    {
                                        users.bankCode = "";
                                    }

                                    if (users.isDonor == true)
                                    {
                                        users.donor_cd = users.donor_cd;
                                    }
                                    else
                                    {
                                        users.donor_cd = "";
                                    }
                                    //if(users.bankCode==0)
                                    //{
                                    //    users.bankCode=;
                                    //}
                                    //if ( users.bankCode!=null)
                                    //{
                                    //    users.isbankUser = true;
                                    //}
                                    //else
                                    //{
                                    //    users.isbankUser = "N";
                                    //}
                                    group.EnterBy = strUserName;
                                    users.password = Utils.EncryptString(users.password);
                                    users.status = "E";
                                    users.enteredBy = strUserName;
                                    users.mobilenumber = fc["mobilenumber"].ConvertToString();
                                    string[] mobl = fc["ddl_Verification"].ConvertToString().Split(',');
                                    users.VerificationRequired = mobl[0];
                                    users.GroupCode = users.GroupCode;
                                    userService.UserUID(users, group, "I");
                                    return RedirectToAction("UsersList");
                                }
                                else
                                {
                                    ModelState.AddModelError("ClientName", "User already exists!!");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ClientName", "The email you entered is already taken!!");
                            }
                        }
                        else
                        {
                            if (users.userCodeCheck == users.usrCd && users.empCodeCheck == users.empCd)
                            {
                                if (userService.CheckDuplicateEmail(users.email, users.userCodeCheck))
                                {
                                    if ( string.IsNullOrEmpty( users.password))
                                    {
                                        string pss = userService.GetUserPassword(users.email);
                                        users.password = pss;
                                    }
                                    else
                                    {
                                        users.password = Utils.EncryptString(users.password);
                                    }

                                    if (users.isDonor == true)
                                    {
                                        users.donor_cd = users.donor_cd;
                                    }
                                    else
                                    {
                                        users.donor_cd = "";
                                    }

                                    users.enteredBy = strUserName;
                                    users.VerificationRequired = fc["ddl_Verification"].ConvertToString();
                                    userService.UserUID(users, group, "U");
                                    return RedirectToAction("UsersList");
                                }
                                else
                                {
                                    ModelState.AddModelError("ClientName", "The email you entered is already taken!!");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("ClientName", "User already exists!!");
                                ViewBag.EditMode = 'Y';
                            }
                        }
                    }
                    return View("ManageUser");
                }
                else if (fc["btn_Cancel"] == "Cancel")
                {
                    return RedirectToAction("ManageUser");
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

            return View("UsersList");
        }
        public enum hello
        {
            h=1
        }
        //Open Page ManageUser in either Add or Edit mode
        [CustomAuthorizeAttribute(PermCd = "3")]
        public ActionResult ManageUser(string p, FormCollection fc)
        {
           CommonFunction common=new CommonFunction();
            Users users = new Users();
            Utils.setUrl(this.Url);
            DataTable dt = new DataTable();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                ViewData["gv_ManageUserList"] = dt;
                ViewData["ddl_Districts"] = GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali"));
                ViewData["ddl_RegState"] = new List<SelectListItem>();
                ViewData["ddl_Zone"] = new List<SelectListItem>();
                ViewData["ddl_Office"] = GetData.AllOffices(Utils.ToggleLanguage("english", "nepali"));
                ViewData["ddl_Position"] = GetData.AllPositions(Utils.ToggleLanguage("english", "nepali"));
                ViewData["ddl_bank"] = common.GetBankName("");
                //ViewData["ddl_PositionSubClass"] = GetData.AllSubClasses(Utils.ToggleLanguage("english", "nepali"));
                ViewData["ddl_VDCMun"] = commonFun.GetVDCMunByDistrict("", "");
                ViewData["ddl_Ward"] = commonFun.GetWardByVDCMun("", "");
                ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(fc["ddl_UserInGroup"]);
                ViewData["ddl_Verification"] = (List<SelectListItem>)commonFun.GetMigration("");
                ViewData["ddl_Donor"] = common.getDonorList("");
                ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(fc["ddl_UserInGroup"]).Where(x=>x.Value.ToString() != "55");

                users.expiryDtLoc = NepaliDate.getNepaliDate(String.Format("{0:dd-MMM-yyyy}", DateTime.Today.AddDays(364)));
                users.expiryDt = DateTime.Today.AddDays(364);
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                    }
                }
                if (id != null && id != "")
                {

                   
                    UsersService userService = new UsersService();
                    users = userService.PopulateUserDetails(id);
                    string password = Utils.DecryptString(users.password.ConvertToString());
                    users.password = password;
                    ViewData["ddl_bank"] = common.GetBankName(users.bankCode);
                    
                    ViewData["ddl_UserInGroup"] = commonFun.GetUserGroup(users.GroupCode);
                    ViewData["ddl_Verification"] = (List<SelectListItem>)commonFun.GetYesNo(users.VerificationRequired).Data;
                    users.editMode = "Edit";
                    ViewBag.EditMode = 'Y';
                    users.userCodeCheck = users.usrCd;
                    users.empCodeCheck = users.empCd;
                    return View("ManageUser", users);
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
            return View(users);
        }

        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult Edit(string p)
        {
            return RedirectToAction("ManageUser", new { p = p });
        }

        //Search Users in the jQuery dialog box
        public JsonResult ManageUsersList(string id, string vdcMunCode, string wardCode, string officeCode, string positionCode, string definedCode, string fullName)
        {
            List<Users> lstUsers = new List<Users>();
            DataTable dt = new DataTable();
            try
            {
                if (id != null && id != string.Empty)
                {
                    id = GetData.GetCodeFor(DataType.District, id.ToString());
                }
                if (vdcMunCode != null && vdcMunCode != string.Empty)
                {
                    vdcMunCode = GetData.GetCodeFor(DataType.VdcMun, vdcMunCode.ToString());
                }
                if (wardCode != null && wardCode != string.Empty && wardCode != "null" && wardCode != "0")
                {
                    wardCode = GetData.GetCodeFor(DataType.Ward, wardCode.ToString());
                }
                if (officeCode != null && officeCode != string.Empty)
                {
                    officeCode = GetData.GetCodeFor(DataType.Office, officeCode);
                }
                if (positionCode != null && positionCode != string.Empty)
                {
                    positionCode = GetData.GetCodeFor(DataType.Position, positionCode);
                }
                //if (posSubClassCode != null && posSubClassCode != string.Empty)
                //{
                //    posSubClassCode = GetData.GetCodeFor(DataType.SubClass, posSubClassCode);
                //}
                dt = userService.ManageUsersList(id, vdcMunCode, wardCode, officeCode, positionCode, definedCode, fullName);
                foreach (DataRow dtrow in dt.Rows)
                {
                    Users user = new Users();
                    user.distName = dtrow["DISTNAME"].ToString();
                    user.vdcMunName = dtrow["VDCNAME"].ToString();
                    user.wardCode = ((dtrow["PER_WARD_NO"]) as int?) ?? 0;
                    user.designationName = dtrow["DESIGNATIONNAME"].ToString();
                    user.positionName = dtrow["POSITIONNAME"].ToString();
                    //user.positionSubClassName = dtrow["POSSUBCLASSNAME"].ToString();
                    user.definedCode = dtrow["DEF_EMPLOYEE_CD"].ToString();
                    user.fullName = Utils.ToggleLanguage(dtrow["FULL_NAME"].ConvertToString(), dtrow["FULL_NAME_NEP"].ConvertToString());
                    user.fullNameEng = dtrow["FULL_NAME"].ToString();
                    user.firstName = dtrow["FIRST_NAME"].ToString();
                    user.middleName = dtrow["MIDDLE_NAME"].ToString();
                    user.lastName = dtrow["LAST_NAME"].ToString();
                    user.firstNameLoc = dtrow["FIRST_NAME_LOC"].ToString();
                    user.middleNameLoc = dtrow["MIDDLE_NAME_LOC"].ToString();
                    user.lastNameLoc = dtrow["LAST_NAME_LOC"].ToString();
                    user.empCd = dtrow["EMPLOYEE_CD"].ToString();
                    lstUsers.Add(user);
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
            return new JsonResult { Data = lstUsers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //For Changing Approval Status
        [CustomAuthorizeAttribute(PermCd = "7")]
        public void ChangeApprovalStatus(string p)
        {
            Users objUsers = new Users();
            UsersService userService = new UsersService();
            string strUserName = string.Empty;
            EmailMessage objEmail = new EmailMessage();
            try
            {
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUserName = objUsers.usrName;
                }
                string status = string.Empty;
                string id = string.Empty;
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }
                    }
                }
                objUsers = new Users();
                objUsers.usrCd = id;
                if (status == "Y")
                {
                    objUsers.approved = false;
                }
                else
                {
                    objUsers.approved = true;
                }
                objUsers.enteredBy = strUserName;
                userService.UserUID(objUsers, null, "A");
                objUsers = new Users();
                objUsers = userService.PopulateUserDetails(id);
                if (objUsers.approved == true && objUsers.status == "E")
                {
                    objEmail.To = objUsers.email;
                    objEmail.From = MailSend.adminEmail;
                    objEmail.Subject = "User Approval";
                    objEmail.Body = EmailTemplate.UserApproval(objUsers);
                    MailSend.SendMail(objEmail);
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

        //Delete User
        [CustomAuthorizeAttribute(PermCd = "4")]
        public ActionResult UserDelete(string p)
        {
            Users users = new Users();
            UsersService userService = new UsersService();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                    }
                }
                if (id != "")
                {
                    users.usrCd = id;
                    userService.UserUID(users, null, "D");
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

            return RedirectToAction("UsersList");
        }

        //Change Status of the User
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult ChangeStatus(string p)
        {
            Users users = new Users();
            UsersService userService = new UsersService();
            string strUserName = string.Empty;
            try
            {
                if (Session[SessionCheck.sessionName] != null)
                {
                    users = (Users)Session[SessionCheck.sessionName];
                    strUserName = users.usrName;
                }
                string status = string.Empty;
                string id = string.Empty;
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }
                    }
                }
                users = new Users();
                users.usrCd = id;
                if (status == "E")
                {
                    status = "D";
                }
                else
                {
                    status = "E";
                }
                users.status = status;
                users.enteredBy = strUserName;
                userService.UserUID(users, null, "S");
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("UsersList");
        }

        #region Change Password

        [HttpGet]
        public ActionResult ChangePassword()
        {
            Users objUser = new Users();
            UsersService userService = new UsersService();
            objUser.email = SessionCheck.getSessionUserEmail();
            objUser.usrCd = SessionCheck.getSessionUserCode();
            return View(objUser);
        }

        [HttpPost]
        public ActionResult ChangePassword(Users model)
        {
            UsersService userService = new UsersService();
            Users objUser = new Users();
            string username = model.email;
            string usercd = model.usrCd;
            string password = model.OldPassword;
            string Newpassword = model.NewPassword;
            string confirmPassword = model.ConfirmPassword;

            if (usercd != null && password != null)
            {
                objUser = userService.SelectUserPassword(usercd, Newpassword);
           
            if (Newpassword == confirmPassword && Newpassword != null)
                {
                    objUser = userService.UpdatePassword(usercd, Newpassword);
                }
                else 
                {
                   
                    return View(model); 
                }
            }
            Session.Clear();
            @TempData["Message"] = "Password Changed Successfully";

            return RedirectToAction("Index","Home");
        }


        #endregion

        #region Fill DropDownLists


        public JsonResult FillRegionState(string id)
        {
            id = GetData.GetCodeFor(DataType.Zone, id);
            List<SelectListItem> selLstRegState = new List<SelectListItem>();
            List<Users> lstRegState = new List<Users>();
            try
            {
                lstRegState = userService.GetRegStatebyCodeandDescForZoneCode("", "", id);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return new JsonResult { Data = lstRegState, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FillZone(string id)
        {
            id = GetData.GetCodeFor(DataType.District, id);
            List<SelectListItem> selLstZone = new List<SelectListItem>();
            List<Users> lstZone = new List<Users>();
            try
            {
                lstZone = userService.GetZonebyCodeandDescForDistCode("", "", id);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return new JsonResult { Data = lstZone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FillVDCMun(string id)
        {
            //id = GetData.GetCodeFor(DataType.District, id);
            List<SelectListItem> lstVDCMun = new List<SelectListItem>();
            try
            {
                lstVDCMun = new CommonFunction().GetVDCMunByDistrict("", id);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return new JsonResult { Data = lstVDCMun, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FillWard(string id)
        {
            //id = GetData.GetCodeFor(DataType.VdcMun, id);
            List<SelectListItem> lstWard = new List<SelectListItem>();
            try
            {
                lstWard = new CommonFunction().GetWardByVDCMun("", id);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return new JsonResult { Data = lstWard, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region Get Layer and get filtered data in the dialog box
        public ActionResult getLayer(string id, string districtid, string zoneid, string vdcid)
        {
            List<SelectListItem> listLayer = new List<SelectListItem>();
            List<Users> lstUser = new List<Users>();
            try
            {
                if (id != null)
                {
                    if (id == "District" || id == "School District")
                    {
                        listLayer = GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali"));

                    }
                    else if (id == "Region/State")
                    {
                        if (zoneid == "")
                        {
                            listLayer = new List<SelectListItem>();
                        }
                        else
                        {
                            lstUser = userService.GetRegStatebyCodeandDescForZoneCode("", "", zoneid);
                            foreach (Users user in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(user.regStateCode), Text = user.regStateName });
                            }
                        }
                    }
                    else if (id == "Zone")
                    {
                        if (districtid == "")
                        {
                            listLayer = new List<SelectListItem>();
                        }
                        else
                        {
                            lstUser = userService.GetZonebyCodeandDescForDistCode("", "", districtid);
                            foreach (Users user in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(user.zoneCode), Text = user.zoneName });
                            }
                        }
                    }
                    else if (id == "VDC/Municipality" || id == "School VDC/Municipality")
                    {
                        if (districtid == "")
                        {
                            listLayer = new List<SelectListItem>();
                        }
                        else
                        {
                            lstUser = userService.GetVDCMunbyCodeandDescForDistCode("", "", districtid);
                            foreach (Users user in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(user.vdcMunCode), Text = user.vdcMunName });
                            }
                        }


                    }
                    else if (id == "Ward")
                    {
                        if (vdcid == "")
                        {
                            listLayer = new List<SelectListItem>();
                        }
                        else
                        {
                            lstUser = userService.GetWardbyCodeandDescForVDCMunCode("", "", vdcid);
                            foreach (Users user in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(user.wardCode), Text = user.wardCode.ToString() });
                            }
                        }
                    }
                    else if (id == "Office")
                    {
                        listLayer = GetData.AllOffices(Utils.ToggleLanguage("english", "nepali"));
                    }
                    else if (id == "Position")
                    {
                        listLayer = GetData.AllPositions(Utils.ToggleLanguage("english", "nepali"));
                    }
                    else if (id == "Position Sub Class")
                    {
                        listLayer = GetData.AllSubClasses(Utils.ToggleLanguage("english", "nepali"));
                    }
                }
                ViewBag.layerData = listLayer;
                CommonVariables.LayerName = Utils.GetLabel("Search " + id);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_getLayer");
        }

        public ActionResult getFilteredData(string id, string desc, string layertype, string zoneid, string districtid, string vdcid)
        {
            List<SelectListItem> listLayer = new List<SelectListItem>();
            List<Users> lstUser = new List<Users>();
            try
            {
                if (layertype != null)
                {

                    if (layertype == "District" || layertype == "School District")
                    {
                        lstUser = userService.GetDistrictsbyCodeandDesc(id, desc);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.distCode), Text = userDistrict.distName });
                            }
                        }
                    }
                    if (layertype == "Zone")
                    {
                        lstUser = userService.GetZonebyCodeandDescForDistCode(id, desc, districtid);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.zoneCode), Text = userDistrict.zoneName });
                            }
                        }
                    }
                    if (layertype == "Region/State")
                    {
                        lstUser = userService.GetRegStatebyCodeandDescForZoneCode(id, desc, zoneid);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.regStateCode), Text = userDistrict.regStateName });
                            }
                        }
                    }
                    if (layertype == "VDC/Municipality")
                    {
                        lstUser = userService.GetVDCMunbyCodeandDescForDistCode(id, desc, districtid);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.vdcMunCode), Text = userDistrict.vdcMunName });
                            }
                        }
                    }
                    if (layertype == "Ward")
                    {
                        lstUser = userService.GetWardbyCodeandDescForVDCMunCode(id, desc, vdcid);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.wardCode), Text = userDistrict.wardCode.ToString() });
                            }
                        }
                    }
                    if (layertype == "Office")
                    {
                        lstUser = userService.GetOfficebyCodeandDesc(id, desc);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.officeCode), Text = userDistrict.officeName.ToString() });
                            }
                        }
                    }
                    if (layertype == "Position")
                    {
                        lstUser = userService.GetPositionbyCodeandDesc(id, desc);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.positionCode), Text = userDistrict.positionName.ToString() });
                            }
                        }
                    }
                    if (layertype == "Position Sub Class")
                    {
                        lstUser = userService.GetPositionSubClassbyCodeandDesc(id, desc);
                        if (lstUser != null)
                        {
                            foreach (Users userDistrict in lstUser)
                            {
                                listLayer.Add(new SelectListItem { Value = Convert.ToString(userDistrict.positionSubClassCode), Text = userDistrict.positionSubClassName.ToString() });
                            }
                        }
                    }
                }
                ViewBag.FilteredData = listLayer;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            return PartialView("_getFilteredData");
        }
        #endregion

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