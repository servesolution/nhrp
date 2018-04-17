using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models.Setup;
using MIS.Authentication;
using MIS.Services.Core;
using MIS.Models.Security;
using MIS.Services.Setup;
using System.Web.Routing;
using System.Data;
using MIS.Services.Security;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Models.Core;

namespace MIS.Controllers.Setup
{
    public class CasteController : BaseController
    {

        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        [HttpGet]
        public ActionResult CasteList(string p, MISCaste Caste)
        {
            CheckPermission();
            Utils.setUrl(this.Url);
            string initial = "";
            string orderby = "T1.DEFINED_CD";
            string order = "asc";
            CasteService obj = new CasteService();
            DataTable dt = new DataTable();
            IDictionary<string, object> dictionCode = new Dictionary<string, object>();
            IDictionary<string, object> dictionName = new Dictionary<string, object>();
            IDictionary<string, object> dictionDesc = new Dictionary<string, object>();
            IDictionary<string, object> dictionApprove = new Dictionary<string, object>();
            IDictionary<string, object> dictionCasteGroup = new Dictionary<string, object>();
            IDictionary<string, object> dictionGroup = new Dictionary<string, object>();
            RouteValueDictionary routeDictionaryCode = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryName = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryDesc = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryApprove = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryCasteGroup = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryGroup = new RouteValueDictionary();
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
                if (initial != "")
                {
                    dictionName.Add("initial", initial);
                    dictionDesc.Add("initial", initial);
                    dictionCode.Add("initial", initial);
                    dictionApprove.Add("initial", initial);
                    dictionCasteGroup.Add("initial", initial);
                    dictionGroup.Add("initial", initial);
                }
                dictionCode.Add("orderby", "T1.DEFINED_CD");
                dictionApprove.Add("orderby", "T1.APPROVED");
                dictionGroup.Add("orderby", "T1.GROUP_FLAG");
                dictionCasteGroup.Add("orderby", Utils.ToggleLanguage("T2.DESC_ENG", "T2.DESC_LOC"));
                dictionName.Add("orderby", Utils.ToggleLanguage("T1.SHORT_NAME", "T1.SHORT_NAME_LOC"));
                dictionDesc.Add("orderby", Utils.ToggleLanguage("T1.DESC_ENG", "T1.DESC_LOC"));
                dt = obj.Caste_GetAllCriteriaToTable(initial, orderby, order);
                ViewData["dtCaste"] = dt;
                ViewBag.initial = initial;
                ViewData["orderby"] = orderby;
                ViewBag.order = order;
                ViewBag.actionName = "CasteList";
                ViewBag.controllerName = "Caste";

                if (order == "desc")
                {
                    nextorder = "asc";
                }

                dictionName.Add("order", nextorder);
                dictionDesc.Add("order", nextorder);
                dictionCode.Add("order", nextorder);
                dictionApprove.Add("order", nextorder);
                dictionCasteGroup.Add("order", nextorder);
                dictionGroup.Add("order", nextorder);

                routeDictionaryName = new RouteValueDictionary(dictionName);
                routeDictionaryDesc = new RouteValueDictionary(dictionDesc);
                routeDictionaryCode = new RouteValueDictionary(dictionCode);
                routeDictionaryApprove = new RouteValueDictionary(dictionApprove);
                routeDictionaryCasteGroup = new RouteValueDictionary(dictionCasteGroup);
                routeDictionaryGroup = new RouteValueDictionary(dictionGroup);

                ViewBag.RouteCode = QueryStringEncrypt.EncryptString(routeDictionaryCode);
                ViewBag.RouteName = QueryStringEncrypt.EncryptString(routeDictionaryDesc);
                ViewBag.RouteSName = QueryStringEncrypt.EncryptString(routeDictionaryName);
                ViewBag.RouteCasteGroup = QueryStringEncrypt.EncryptString(routeDictionaryCasteGroup);
                ViewBag.RouteGroup = QueryStringEncrypt.EncryptString(routeDictionaryGroup);
                ViewBag.Approved = QueryStringEncrypt.EncryptString(routeDictionaryApprove);
                ViewBag.nextorder = nextorder;
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

        [CustomAuthorizeAttribute(PermCd = PermissionCode.Add)]
        public ActionResult AddCaste(string editid, FormCollection fc)
        {
            CasteService obj = new CasteService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            MISCaste Caste = new MISCaste();
            try
            {
                ViewData["ddl_Group"] = GetGroup(fc["ddl_Group"]);
                ViewData["ddl_CasteGroup"] = GetCasteGroup(fc["ddl_CasteGroup"]);
                Utils.setUrl(this.Url);
                if (editid != null)
                {
                    string id = "";
                    rvd = QueryStringEncrypt.DecryptString(editid);
                    if (rvd != null)
                    {
                        id = rvd["id"].ToString();
                        Caste.CasteCd = Convert.ToInt32(id);
                        Caste = obj.FillCaste(id);

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
            return View(Caste);
        }

        //[CustomAuthorizeAttribute(PermCd = "3")]
        [HttpPost]
        public ActionResult AddCaste(MISCaste Caste, FormCollection fc)
        {
            CasteService casteService = new CasteService();
            Users objUsers;
            string strUsername = "";
            string exc = "";
            try
            {
                ViewData["ddl_Group"] = GetGroup(fc["ddl_Group"]);
                ViewData["ddl_CasteGroup"] = GetCasteGroup(fc["ddl_CasteGroup"]);
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                int AllCd = Convert.ToInt32(Caste.CasteCd);
                foreach (SelectListItem item in GetGroup(fc["ddl_Group"]))
                {
                    if (item.Selected == true)
                    {
                        Caste.GroupFlag = item.Value;
                    }
                }

                Caste.ApprovedBy = strUsername;
                Caste.ApprovedDt = DateTime.Now;
                Caste.EnteredBy = strUsername;
                Caste.EnteredDt = DateTime.Now;
                if (AllCd == 0)
                {
                    string caste_cd = casteService.GetMaxvalue();
                    if (caste_cd != "")
                    {
                        if (casteService.CheckDuplicateDefinedCode(Caste.DefinedCd, ""))
                        {
                            Caste.Mode = "I";
                            Caste.CasteCd = Convert.ToInt32(caste_cd);
                            casteService.ManageCaste(Caste,out exc);
                            Session["UpdateGlobalData"] = "true";
                            Session["UpdatedType"] = DataType.Caste;
                        }
                        else
                        {
                            ModelState.AddModelError("Defined Code", "Defined Code already exists!!");
                            return View();
                        }
                    }
                }
                else
                {
                    if (casteService.CheckDuplicateDefinedCode(Caste.DefinedCd, AllCd.ToString()))
                    {
                        Caste.Mode = "U";
                        Caste.CasteCd = AllCd;
                        casteService.ManageCaste(Caste,out exc);
                        Session["UpdateGlobalData"] = "true";
                        Session["UpdatedType"] = DataType.Caste;
                    }
                    else
                    {
                        ModelState.AddModelError("Defined Code", "Defined Code already exists!!");
                        return View();
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
            return RedirectToAction("CasteList");
        }

        [CustomAuthorizeAttribute(PermCd = PermissionCode.Edit)]
        [HttpGet]
        public ActionResult EditCaste(string p)
        {
            return RedirectToAction("AddCaste", new { editid = p });
        }

         [CustomAuthorizeAttribute(PermCd = PermissionCode.Delete)]
        public ActionResult DeleteCaste(string p)
        {

            MISCaste obj = new MISCaste();
            CasteService casteService = new CasteService();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string exc = "";
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
                            obj.Mode = "D";
                            obj.CasteCd = Convert.ToInt32(id);
                            obj.EnteredBy = SessionCheck.getSessionUsername();
                            casteService.ManageCaste(obj,out exc);
                            if (exc == "20099")
                            {
                                TempData["ErrorMessage"] = "Cannot delete record being used";
                            }
                            else
                            {
                                Session["UpdateGlobalData"] = "true";
                                Session["UpdatedType"] = DataType.Caste;
                            }
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

            return RedirectToAction("CasteList");
        }

        //Change Caste Status
        [CustomAuthorizeAttribute(PermCd = "7")]
        public ActionResult ChangeStatus(string p)
        {            
            string strUsername = "";
            string status = "";
            MISCaste caste = new MISCaste();
            CasteService obj = new CasteService();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string exc = "";
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
                caste.CasteCd = Convert.ToInt32(id);
                if (status == "Y")
                {
                    caste.Approved = false;
                }
                else if (status == "N")
                {
                    caste.Approved = true;
                }               
                strUsername = SessionCheck.getSessionUsername();
                caste.CasteCd = Convert.ToInt32(id);
                caste.Mode = "A";
                caste.EnteredBy = strUsername;
                caste.ApprovedBy = strUsername;
                obj.ManageCaste(caste,out exc);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("CasteList");

        }

        #region DropDownList Fills
        public List<SelectListItem> GetGroup(string selectedValue)
        {
            List<SelectListItem> selLstGroup = new List<SelectListItem>();
            selLstGroup.Add(new SelectListItem { Value = "", Text = "--" + Utils.GetLabel("Choose an option") + "--" });
            selLstGroup.Add(new SelectListItem { Value = "S", Text = Utils.GetLabel("Marginalized") });
            selLstGroup.Add(new SelectListItem { Value = "O", Text = Utils.GetLabel("Oppressed") });
            foreach (SelectListItem item in selLstGroup)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstGroup;
        }

        public List<SelectListItem> GetCasteGroup(string selectedValue)
        {
            CommonFunction common = new CommonFunction();
            return common.GetCasteGroup("");
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
    }
}
