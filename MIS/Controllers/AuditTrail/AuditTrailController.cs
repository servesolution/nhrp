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
using MIS.Services.AuditTrail;
using MIS.Models.Core;
namespace MIS.Controllers.AuditTrail
{
    public class AuditTrailController : BaseController
    {
        AuditTrailService objAuditTrailService = new AuditTrailService();
        CommonFunction common = new CommonFunction();
        //Get AuditTrailList
        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult AuditTrailList(string p)
        {
            Utils.setUrl(this.Url);
            string initial = "";
            string orderby = "AUD_USER";
            string order = "desc";
            string userCode = "";
            int page = 1;
            IDictionary<string, object> dictionName = new Dictionary<string, object>();
            IDictionary<string, object> dictionDesc = new Dictionary<string, object>();
            IDictionary<string, object> dictionDate = new Dictionary<string, object>();
            IDictionary<string, object> dictionTableName = new Dictionary<string, object>();
            IDictionary<string, object> dictionColumn = new Dictionary<string, object>();
            IDictionary<string, object> dictionTableType = new Dictionary<string, object>();
            RouteValueDictionary routeDictionaryName = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryDesc = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryDate = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryColumn = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryTableName = new RouteValueDictionary();
            RouteValueDictionary routeDictionaryTableType = new RouteValueDictionary();
            RouteValueDictionary rvd = new RouteValueDictionary();
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
                        if (rvd["pageno"] != null)
                        {
                            page = Int32.Parse(rvd["pageno"].ToString());
                        }
                    }
                }

                if (order == "asc")
                {
                    nextorder = "desc";
                }
                if (initial != "")
                {
                    dictionName.Add("initial", initial);
                    dictionDesc.Add("initial", initial);
                    dictionDate.Add("initial", initial);
                    dictionColumn.Add("initial", initial);
                    dictionTableName.Add("initial", initial);
                    dictionTableType.Add("initial", initial);
                }
                dictionName.Add("orderby", "aud_user");
                dictionDesc.Add("orderby", "aud_operation");
                dictionDate.Add("orderby", "aud_datetime");
                dictionColumn.Add("orderby", "Column_name");
                dictionTableName.Add("orderby", "user_table_desc");
                dictionTableType.Add("orderby", "tabletype");
                dictionName.Add("order", nextorder);
                dictionDesc.Add("order", nextorder);
                dictionDate.Add("order", nextorder);
                dictionColumn.Add("order", nextorder);
                dictionTableName.Add("order", nextorder);
                dictionTableType.Add("order", nextorder);
                routeDictionaryName = new RouteValueDictionary(dictionName);
                routeDictionaryDesc = new RouteValueDictionary(dictionDesc);
                routeDictionaryDate = new RouteValueDictionary(dictionDate);
                routeDictionaryColumn = new RouteValueDictionary(dictionColumn);
                routeDictionaryTableName = new RouteValueDictionary(dictionTableName);
                routeDictionaryTableType = new RouteValueDictionary(dictionTableType);
                dt = new DataTable();
                if (Request.UrlReferrer!= null && Request.UrlReferrer.LocalPath != "/AuditTrail/AuditTrailList/")
                {
                    CommonVariables.SearchTableType = String.Empty;
                    CommonVariables.SearchTableName = String.Empty;
                    CommonVariables.SearchOperation = String.Empty;
                    CommonVariables.SearchUserName = String.Empty;
                    CommonVariables.SearchStartDate = String.Empty;
                    CommonVariables.SearchEndDate = String.Empty;
                    CommonVariables.SearchStartDateLoc = String.Empty;
                    CommonVariables.SearchEndDateLoc = String.Empty;
                }
                ViewData["ddTableType"] = common.GetTableType(CommonVariables.SearchTableType);
                ViewData["ddTableName"] = common.GetTableName(CommonVariables.SearchTableName, CommonVariables.SearchTableType);
                ViewData["ddOperation"] = common.GetOperation(CommonVariables.SearchOperation);
                ViewData["txtUserName"] = CommonVariables.SearchUserName;
               
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

                //dt = objAuditTrailService.AuditTrail_Search(initial, orderby, order, CommonVariables.SearchUserName, CommonVariables.SearchTableType, CommonVariables.SearchTableName, CommonVariables.SearchOperation, CommonVariables.SearchStartDate, CommonVariables.SearchEndDate);
                ////paging
                //ViewData["dtAuditTrail"] = pagedDataTable(dt, pageSize, page);
                ViewData["dtAuditTrail"] = dt;
                //ViewBag.pageno = page;
                //ViewBag.PageSize = pageSize;
                //int total = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                //ViewBag.totalpage = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                ////paging
                //ViewBag.initial = initial;
                //ViewData["orderby"] = orderby;
                //ViewBag.order = order;
                ViewBag.actionName = "AuditTrailList";
                ViewBag.controllerName = "AuditTrail";
                ViewBag.nextorder = nextorder;
                ViewBag.RouteName = QueryStringEncrypt.EncryptString(routeDictionaryName);
                ViewBag.RouteDesc = QueryStringEncrypt.EncryptString(routeDictionaryDesc);
                ViewBag.RouteDate = QueryStringEncrypt.EncryptString(routeDictionaryDate);
                ViewBag.RouteColumn = QueryStringEncrypt.EncryptString(routeDictionaryColumn);
                ViewBag.RouteTableName = QueryStringEncrypt.EncryptString(routeDictionaryTableName);
                ViewBag.RouteModuleName = QueryStringEncrypt.EncryptString(routeDictionaryTableType);
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
        public ActionResult AuditTrailList(FormCollection fc)
        {
            try
            {
                //if (fc["btn_Submit"] == (Utils.GetLabel("Search")))
                //{
                    CheckPermission();
                    CommonVariables.SearchUserName = Convert.ToString(fc["txtUserName"]);
                    CommonVariables.SearchTableType = Convert.ToString(fc["ddTableType"]);
                    CommonVariables.SearchTableName = Convert.ToString(fc["ddTableName"]);
                    CommonVariables.SearchOperation = Convert.ToString(fc["ddOperation"]);
                    CommonVariables.SearchStartDate = Convert.ToString(fc["START_DT_FROM"]);
                    CommonVariables.SearchEndDate = Convert.ToString(fc["END_DT_TO"]);
                    CommonVariables.SearchStartDateLoc = Convert.ToString(fc["StartDtLoc"]);
                    CommonVariables.SearchEndDateLoc = Convert.ToString(fc["EndDtLoc"]);
                    CommonVariables.START_DT_FROM = Convert.ToString("StartDTFrom");
                    CommonVariables.END_DT_TO = Convert.ToString("EndDTTO");

                    DataTable dt = new DataTable();
                    List<SelectListItem> test = new List<SelectListItem>();
                    string orderby = "aud_user";
                    string order = "desc";
                    int pageSize = 50;
                    int page = 1;
                    dt = objAuditTrailService.AuditTrail_Search("", orderby, order, HttpUtility.HtmlEncode(CommonVariables.SearchUserName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchTableType.Trim()),  HttpUtility.HtmlEncode(CommonVariables.SearchTableName.Trim()), HttpUtility.HtmlEncode(CommonVariables.SearchOperation.Trim()),CommonVariables.SearchStartDate, CommonVariables.SearchEndDate);

                    //paging
                    //ViewData["dtAuditTrail"] = pagedDataTable(dt, pageSize, page);
                    ViewData["dtAuditTrail"] = dt;
                    //ViewBag.pageno = page;
                    //ViewBag.PageSize = pageSize;
                    //int total = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                    //ViewBag.totalpage = (int)Math.Ceiling((double)dt.Rows.Count / pageSize);
                    ////paging
                    ViewData["ddTableType"] = common.GetTableType(CommonVariables.SearchTableType );
                    ViewData["ddTableName"] = common.GetTableName(CommonVariables.SearchTableName, CommonVariables.SearchTableType);
                    ViewData["ddOperation"] = common.GetOperation(CommonVariables.SearchOperation);
                    ViewData["txtUserName"] = CommonVariables.SearchUserName;
                    ViewData["StartDt"] = CommonVariables.SearchStartDate;
                    ViewData["EndDt"] = CommonVariables.SearchEndDate;
                    ViewData["StartDtLoc"] = CommonVariables.SearchStartDateLoc;
                    ViewData["EndDtLoc"] = CommonVariables.SearchEndDateLoc;
                    ViewData["InterviewDTFrom"] = CommonVariables.START_DT_FROM;
                    ViewData["InterviewDTTO"] = CommonVariables.END_DT_TO;
                    ViewBag.actionName = "AuditTrailList";
                    ViewBag.initial = "";
                    return PartialView("_AuditTrialPartialView");
                //}
                //else
                //{
                //    CommonVariables.SearchTableType = "";
                //    CommonVariables.SearchTableName = "";
                //    CommonVariables.SearchOperation = "";
                //    CommonVariables.SearchUserName = "";
                //    CommonVariables.SearchStartDate = "";
                //    CommonVariables.SearchEndDate = "";
                //    CommonVariables.SearchStartDateLoc = "";
                //    CommonVariables.SearchEndDateLoc = "";
                //    return PartialView("_AuditTrialPartialView");
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
             return PartialView("_AuditTrialPartialView");
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


        public JsonResult ChangeTableType(string id)
        {
            CommonFunction common = new CommonFunction();
            return common.FillTableName(id);

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
