using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS;
using MIS.Models.Security;
using MIS.Services.Core;
using MIS.Services.Security;
using System.Data;
using System.Web.Routing;
using MIS.Authentication;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Models.Core;

namespace MIS.Controllers.Security
{
    public class GroupController : BaseController
    {
        List<Group> lstgrp = null;
        Group grpUpdate = null;

        // Display Group List 
        [CustomAuthorizeAttribute(PermCd = "2")]
        [HttpGet]
        public ActionResult GroupList(string p, Group objGroup)
        {
            CheckPermission();
            Utils.setUrl(this.Url);
            string initial = "";
            string orderby = "grp_cd";
            string order = "asc";
            GroupService obj = new GroupService();
            DataTable dt = new DataTable();
            IDictionary<string, object> dictionName = new Dictionary<string, object>();
            IDictionary<string, object> dictionDesc = new Dictionary<string, object>();
            RouteValueDictionary routeDictionaryName = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryDesc = new RouteValueDictionary();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string nextorder = "desc";
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
                if (initial != "")
                {
                    dictionName.Add("initial", initial);
                    dictionDesc.Add("initial", initial);
                }
                dictionName.Add("orderby", "grp_name");
                dictionDesc.Add("orderby", "grp_cd");
                dt = obj.Group_GetAllCriteriaToTable(initial, orderby, order);
                ViewData["dtGroup"] = dt;
                ViewBag.initial = initial;
                ViewData["orderby"] = orderby;
                ViewBag.order = order;
                ViewBag.actionName = "GroupList";
                ViewBag.controllerName = "Group";
                if (objGroup.ErrFlg != null)
                {
                    ViewBag.ErrFlag = "Error";
                }
                if (order == "desc")
                {
                    nextorder = "asc";
                }
                ViewBag.nextorder = nextorder;
                dictionName.Add("order", nextorder);
                dictionDesc.Add("order", nextorder);
                routeDictionaryName = new RouteValueDictionary(dictionName);
                routeDictionaryDesc = new RouteValueDictionary(dictionDesc);
                ViewBag.RouteName = QueryStringEncrypt.EncryptString(routeDictionaryName);
                ViewBag.RouteGrpCd = QueryStringEncrypt.EncryptString(routeDictionaryDesc);
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

        #region || Insert,Update and Delete||

        // Add New Group
        [CustomAuthorizeAttribute(PermCd = "3")]
        public ActionResult AddGroup(string EditId)
        {
            Utils.setUrl(this.Url);
            Group objGroup = new Group();
            GroupService obj = new GroupService();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if (EditId != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(EditId);
                    if (rvd != null)
                    {
                        id = rvd["id"].ToString();
                        objGroup.GrpCode = id;
                        objGroup = obj.FillGroup(id);
                        Session["oldGrpname"] = objGroup.GrpName;
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
            return View(objGroup);
        }

        //Add New Group 
        [CustomAuthorizeAttribute(PermCd = "3")]
        [HttpPost]
        public ActionResult AddGroup(Group objGroup, FormCollection fc)
        {
            Group tempGroup = new Group();
            GroupService objGroupService = new GroupService();
            Users objUsers;
            string strUsername = "";
            string sts;
            try
            {
                sts = fc["chkStatus"];
                if (sts == "false")
                {
                    objGroup.status = 'D';
                }
                else
                {
                    objGroup.status = 'E';
                }
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                objGroup.EnteredDt = DateTime.Now;
                objGroup.EnterBy = strUsername;
                objGroup.LastUpdDt = DateTime.Now;
                objGroup.LastUpdBy = strUsername;
                string oldgrpName = Convert.ToString(Session["oldGrpname"]);
                string editgrpcd = objGroup.GrpCode;
                if (editgrpcd != "")
                {
                    if (oldgrpName != objGroup.GrpName)
                    {
                        tempGroup = objGroupService.GetGroupbyName(objGroup.GrpName);
                    }
                }
                else
                {
                    tempGroup = objGroupService.GetGroupbyName(objGroup.GrpName);

                }

                if (tempGroup.GrpName != null)
                {
                    ModelState.AddModelError("GroupAlert", "Group name already exists!!");
                    return View();
                }

                if (objGroup.GrpCode == null)
                {
                    objGroup.GrpCode = objGroupService.GetMaxvalue();
                    objGroupService.AddUserGroup(objGroup);
                }
                else
                {
                    objGroupService.UdateUserGroup(objGroup);
                }
                Session["oldGrpname"] = null;
                Session["editId"] = null;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("GroupList");
        }

        //Delete Group
        [CustomAuthorizeAttribute(PermCd = "4")]
        public ActionResult DeleteGroup(string p)
        {
            Group objGroup = new Group();
            GroupService obj = new GroupService();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        id = rvd["id"].ToString();
                        objGroup.GrpCode = id;
                        obj.DeleteUserGroup(objGroup);
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
            return RedirectToAction("GroupList");

        }

        //Edit Group
        [CustomAuthorizeAttribute(PermCd = "1")]
        [HttpGet]
        public ActionResult EditGroup(string p)
        {

            return RedirectToAction("AddGroup", new { EditId = p });
        }

        //Change Group Status
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult ChangeStatus(string p)
        {
            Users objUsers;
            string strUsername = "";
            Group objGroup = new Group();
            GroupService obj = new GroupService();
            string id = "";
            string status = "";
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
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }

                    }
                }
                objGroup.GrpCode = id;
                if (status == "E")
                {
                    status = "D";
                }
                else
                {
                    status = "E";
                }
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                objGroup.status = Convert.ToChar(status);
                objGroup.LastUpdBy = strUsername;
                obj.ChangeStatus(objGroup);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("GroupList");

        }

        // Add User in Group 
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult AddUserInGroup(Group objGroup, string p)
        {
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        id = rvd["id"].ToString();
                        Session["GroupCode"] = id;
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

            return PartialView("_AddUserInGroup", objGroup);
        }

        // Add Users in Group   
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult InsertUser(Group objGroup)
        {
            Users objUsers;
            string strUsername = "";
            GroupService obj = new GroupService();
            try
            {
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                objGroup.GrpCode = Convert.ToString(Session["GroupCode"]);
                objGroup.EnteredDt = DateTime.Now;
                objGroup.EnterBy = strUsername;
                objGroup.LastUpdDt = DateTime.Now;
                objGroup.LastUpdBy = strUsername;
                obj.InsertUser(objGroup);
                Session["GroupCode"] = null;
                string ErrFlag = objGroup.ErrFlg;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("GroupList", objGroup);

        }

        //Remove User from Group
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult RemoveUser(string p)
        {
            DataTable dt = new DataTable();
            GroupService obj = new GroupService();
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
                            Session["GroupCode"] = id;
                        }
                    }
                }
                Session["RemoveGrpID"] = id;
                ViewData["UserList"] = obj.GetUserListToRemove(id);
                List<SelectListItem> item = new List<SelectListItem>();
                ViewData["UserToRemove"] = item;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_RemoveUser");
        }


        //Update users in Group
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult UpdateUserInGrp(string opt)
        {
            Group objGroup = new Group();
            GroupService obj = new GroupService();
            string[] userlist;
            try
            {
                userlist = opt.Split(',');
                foreach (string code in userlist)
                {
                    string usercode = code;
                    objGroup.UserCd = usercode;
                    objGroup.GrpCode = Convert.ToString(Session["RemoveGrpID"]);
                    obj.RemoveUserFrmGrp(objGroup);
                }
                Session["RemoveGrpID"] = null;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("GroupList");
        }
        #endregion

        #region || Group List and Select Operation||


        // Display Users In Group 
        [HttpGet]
        public ActionResult DisplayUserGroup(string id)
        {
            GroupService obj = new GroupService();
            Group objGroup = new Group();
            DataTable dt = new DataTable();
            try
            {
                objGroup.GrpCode = id;
                dt = obj.GetUserInGroup(objGroup);
                ViewData["UserList"] = dt;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_DisplayGroup");
        }

        // Shows Form For Searching user 
        public ActionResult SearchUser(Group objGroup)
        {
            return PartialView("_SearchUser");
        }

        //  Search User on basis of user code or user Fullname 
        public ActionResult SearchResult(string srcText, string srcCode)
        {
            GroupService obj = new GroupService();
            DataTable dt = new DataTable();
            Group objGroup = new Group();
            try
            {
                objGroup.SearchCd = srcCode;
                objGroup.SearchText = srcText;
                dt = obj.SearchUser(objGroup);
                ViewData["FillSearch"] = dt;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_SearchResult");
        }

        // display User to add in group 
        public ActionResult ShowUser(string id, string UserName)
        {
            Group objGroup = new Group();
            objGroup.UserCd = id;
            objGroup.EmpName = UserName;
            return PartialView("_AddUserInGroup", objGroup);
        }
        #endregion

        // Check User Permission 
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

        # region JqueryDataGrid for Group

        /// <summary>
        /// Load the data in grid.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult testGroup()
        {
            CheckPermission();
            Session["GroupListValue"] = null;
            ViewData["dtTestGroup"] = null;
            //GroupService obj = new GroupService();
            //DataTable dt = new DataTable();
            //dt = obj.GetUserGrp();
            //ViewData["dtTestGroup"] = LoadData();
            return View();
        }

        /// <summary>
        /// Storing the new or updated data on session before inserting or updating the data.
        /// </summary>
        /// <returns></returns>
        public string DataManipulate()
        {
            string code = Request["GrpCode"];
            string name = Request["GrpName"];
            string status = Request["status"];

            if (status == "E")
            {
                status = "D";
            }
            else
            {
                status = "E";
            }

            lstgrp = new List<Group>();

            if (Request.Form["isNewRecord"] == null)
            {
                if (Session["GroupListValue"] == null)
                {
                    Group grp = new Group();
                    grp.GrpCode = code;
                    grp.GrpName = name;
                    grp.status = Convert.ToChar(status);
                    grp.OldNewFlag = "old";
                    lstgrp.Add(grp);
                    Session["GroupListValue"] = lstgrp;
                }
                else
                {
                    lstgrp = (List<Group>)Session["GroupListValue"];
                    Group grp = new Group();
                    grp.GrpCode = code;
                    grp.GrpName = name;
                    grp.status = Convert.ToChar(status);
                    grp.OldNewFlag = "old";
                    lstgrp.Add(grp);
                    Session["GroupListValue"] = lstgrp;
                }
            }
            else
            {
                if (Session["GroupListValue"] == null)
                {
                    Group grp = new Group();
                    grp.GrpCode = code;
                    grp.GrpName = name;
                    grp.status = Convert.ToChar(status);
                    grp.OldNewFlag = "new";
                    lstgrp.Add(grp);
                    Session["GroupListValue"] = lstgrp;
                }
                else
                {
                    lstgrp = (List<Group>)Session["GroupListValue"];
                    Group grp = new Group();
                    grp.GrpCode = code;
                    grp.GrpName = name;
                    grp.status = Convert.ToChar(status);
                    grp.OldNewFlag = "new";
                    lstgrp.Add(grp);
                    Session["GroupListValue"] = lstgrp;
                }
            }
            return "DataManipulate";
        }

        /// <summary>
        /// Delete each row.
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            string id = Request["id"];
            Group gp = new Group();
            GroupService obj = new GroupService();
            gp.GrpCode = id;
            obj.DeleteUserGroup(gp);
            Session["GroupListValue"] = null;
            return RedirectToAction("testGroup");
        }

        /// <summary>
        /// Save or Update the data.
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            lstgrp = (List<Group>)Session["GroupListValue"];
            if (lstgrp != null)
            {
                foreach (Group grp in lstgrp)
                {
                    GroupService obj = new GroupService();
                    if (grp.OldNewFlag == "old")
                    {
                        grpUpdate = new Group();
                        grpUpdate = obj.GetGroupbyCode(grp.GrpCode);
                        grpUpdate.GrpCode = grp.GrpCode;
                        grpUpdate.GrpName = grp.GrpName;
                        grpUpdate.status = grp.status;
                        grpUpdate.GrpDesc = grpUpdate.GrpDesc;
                        grpUpdate.LastUpdDt = DateTime.Now;
                        grpUpdate.LastUpdBy = grpUpdate.LastUpdBy;
                        grpUpdate.EnteredDt = DateTime.Now;
                        grpUpdate.EnterBy = grpUpdate.EnterBy;
                        obj.UdateUserGroup(grpUpdate);
                    }
                    else
                    {
                        grp.EnteredDt = DateTime.Now;
                        grp.EnterBy = Convert.ToString("TADMIN");
                        grp.LastUpdDt = DateTime.Now;
                        grp.LastUpdBy = Convert.ToString("TADMIN");
                        obj.AddUserGroup(grp);
                    }
                }
            }
            Session["GroupListValue"] = null;
            return RedirectToAction("testGroup");
        }

        /// <summary>
        /// Load the data in grid with Json format.
        /// </summary>
        /// <returns></returns>
        public string LoadData()
        {

            string sort = Request.Params["sort"];  // the sort field name
            if (sort == "GrpCode")
            {
                sort = "GRP_CD";
            }
            else if (sort == "GrpName")
            {
                sort = "GRP_NAME";
            }
            string order = Request.Params["order"]; // the sort order,'asc' or 'desc'

            int page = int.Parse(Request.Params["page"]); // the page number
            int rows = int.Parse(Request.Params["rows"]); // the page size

            List<Group> userList = new List<Group>();
            GroupService grpService = new GroupService();
            if (sort == null & order == null)
            {
                userList = grpService.GetUserGroup();
            }
            else
            {
                userList = grpService.GetUserGroupbyOrder(sort, order);
            }

            int totalRecords = userList.Count; // total records

            //var jsonData = new
            //{
            //    total = totalRecords.ToString(),
            //    rows = (from n in userList
            //            select new
            //                {
            //                    GrpCode = n.GrpCode,
            //                    GrpName = n.GrpName,
            //                    status = (char)n.status,
            //                }).ToArray()                
            //    };
            //return Json(jsonData, JsonRequestBehavior.AllowGet); 
            string row = "";
            for (int i = page * rows; i < page * rows + rows; i++)
            {
                if (i - rows == totalRecords)
                {
                    break;
                }
                row += "{\"GrpCode\":\"" + userList[i - rows].GrpCode.ToString() + "\",\"GrpName\":\"" + userList[i - rows].GrpName.ToString() + "\",\"status\":\"" + userList[i - rows].status.ToString() + "\"},";
            }
            string s = "{\"total\":\"" + totalRecords + "\",\"rows\":[" + row;
            s = s.Substring(0, s.Length - 1);
            s += "]}";

            return s;

        }
        # endregion
    }
}
