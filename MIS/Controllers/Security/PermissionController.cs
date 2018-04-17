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
using MIS.Models.Core;

namespace MIS.Controllers.Security
{
    public class PermissionController : BaseController
    {
        PermissionService objPermissionService = new PermissionService();
        Permission objPermission = new Permission();  
                  
        //Get PermissionList
        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult PermissionList( string p)
        {
            try
            {
                Utils.setUrl(this.Url);
                string initial = "";
                string orderby = "perm_name";
                string order = "asc";
                int page = 1;
                IDictionary<string, object> dictionName = new Dictionary<string, object>();
                IDictionary<string, object> dictionDesc = new Dictionary<string, object>();
                RouteValueDictionary routeDictionaryName = new RouteValueDictionary();
                RouteValueDictionary routeDictionaryDesc = new RouteValueDictionary();
                RouteValueDictionary rvd = new RouteValueDictionary();
                string nextorder = "desc";
                int pageSize = 10;
                DataTable dt = null;
               // CheckPermission();
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
                    }
                }
                if (order == "desc")
                {
                    nextorder = "asc";
                }
                if (initial != "")
                {
                    dictionName.Add("initial", initial);
                    dictionDesc.Add("initial", initial);
                }
                dictionName.Add("orderby", "perm_name");
                dictionDesc.Add("orderby", "perm_desc");
                dictionName.Add("order", nextorder);
                dictionDesc.Add("order", nextorder);
                routeDictionaryName = new RouteValueDictionary(dictionName);
                routeDictionaryDesc = new RouteValueDictionary(dictionDesc);
                dt = new DataTable();
                dt = objPermissionService.Permission_GetAllCriteriaToTable(initial, orderby, order);
                ViewData["dtPermission"] = dt;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = pageSize;
                int total = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                ViewBag.TotalPages = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                ViewBag.DataTable = dt;
                ViewBag.initial = initial;
                ViewData["orderby"] = orderby;
                ViewBag.order = order;
                ViewBag.actionName = "PermissionList";
                ViewBag.controllerName = "Permission";
                ViewBag.nextorder = nextorder;
                ViewBag.RouteName = QueryStringEncrypt.EncryptString(routeDictionaryName);
                ViewBag.RouteDesc = QueryStringEncrypt.EncryptString(routeDictionaryDesc);
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
        public DataTable pagedDataTable(DataTable dt, int pagesize, int page)
        {
            DataTable returndt = new DataTable();
            DataTable tempdt = new DataTable();
            int startIndex = 0;
            int endIndex = 0;
            int totalpage = 0;
            if (dt.Rows.Count > 0 && dt.Rows.Count < pagesize)
            {
                return dt;
            }
            else
            {
                totalpage = (int)Math.Ceiling((double)dt.Rows.Count / pagesize);
                returndt = dt.Clone();
                tempdt = dt.Copy();
                if (page == 1)
                {
                    startIndex = 0;                    
                    endIndex =  pagesize;                   
                }
                else
                {
                    startIndex = pagesize * (page-1);
                    if (totalpage > page)
                    {
                        endIndex = startIndex  + pagesize;
                    }
                    else 
                    {
                        endIndex = startIndex + (dt.Rows.Count - startIndex);
                    }
                }                
                for (int i = startIndex; i < endIndex; i++)
                {
                    DataRow dr=tempdt.NewRow();
                    dr  = tempdt.Rows[i];
                    returndt.ImportRow(dr);
                }

            }
            return returndt;

        }
        //Get Insert/Edit Permission
        [HttpGet]       
        public ActionResult ManagePermission(string p)
        {
            try
            {
                string id = "";
                RouteValueDictionary rvd = new RouteValueDictionary();
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
                    }
                }
                if (id != "")
                {
                    if ((ViewBag.EnableEdit == "true"))
                    {
                        objPermission = objPermissionService.Permission_Get(int.Parse(id));
                        return View(objPermission);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";
                        return RedirectToAction("PermissionList");
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
                        return RedirectToAction("PermissionList");
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

        //Post Insert/Edit Permission
        [HttpPost]
        public ActionResult ManagePermission(Permission objPermission, FormCollection fc)
        {
            bool isSuccess = false;
            Users objUsers = new Users();
            string strUsername = "";         
            try
            {
                if (fc["btnCancel"] != null)
                {
                    return RedirectToAction("PermissionList");
                }
                TryUpdateModel(objPermission);
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                objPermission.EnteredBy = strUsername;
                objPermission.EnteredDt = System.DateTime.Now;
                objPermission.LastUpdatedBy = strUsername;
                objPermission.LastUpdatedDt = System.DateTime.Now;
                objPermission.IPAddress = null;
                if (ModelState.IsValid)
                {

                    if (String.IsNullOrEmpty(objPermission.PermCd))
                    {
                        isSuccess = objPermissionService.Permission_Manage(objPermission, "I");

                    }
                    else
                    {

                        isSuccess = objPermissionService.Permission_Manage(objPermission, "U");

                    }
                    if (isSuccess)
                    {
                        ViewData["Message"] = "Success";
                        return RedirectToAction("PermissionList");
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
            return RedirectToAction("PermissionList");
        }

        //Get Delete Permission
        [HttpGet]
        [CustomAuthorizeAttribute(PermCd = PermissionCode.Delete )]
        public ActionResult DeletePermission(string p)
        {
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            CheckPermission();
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
                    objPermission = objPermissionService.Permission_Get(int.Parse(id));
                    if (ViewBag.EnableDelete == "true")
                    {
                        bool isSuccess = objPermissionService.Permission_Manage(objPermission, "D");
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
            return RedirectToAction("PermissionList");
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
                if (objPermissionParam!=null)
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
