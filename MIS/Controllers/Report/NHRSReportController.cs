using EntityFramework;
using ExceptionHandler;
using MIS.Authentication;
using MIS.Models.Core;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class NHRSReportController : BaseController
    {
        public ActionResult ReportHierarchy(string SearchText)
        {
            Utils.setUrl(this.Url);
            NHRSReportService rs = new NHRSReportService();
            List<TreeItems> trReport = null;
            try
            {
                trReport = rs.GetReportHierarchy(SearchText);
                ViewBag.SearchItemCount = CommonVariables.SearchedItem;
                ViewBag.txtSearch = SearchText;
                ViewBag.ReportHer = trReport;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View("~/Views/NHRSReport/ReportHierarchyDetail.cshtml");
        }

        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult GetReportDetail(string cd, string errorMessage)
        {
            ViewBag.ErrorMsg = errorMessage;
            NHRSReportService objReportServices = new NHRSReportService();
            MISReport obj = new MISReport();
            CommonFunction cm=new CommonFunction();
            DataTable dtbl = null;
            try
            {
                if (cd != null)
                {
                    CommonVariables.SelectedCode = cd;
                }
                dtbl = objReportServices.GetReportDetail(CommonVariables.SelectedCode);

                if (cd == "")
                {
                    obj.reportCd = "";
                    obj.DefinedCD = "";
                    obj.upperReportCd = "";
                    // obj.upperOfficel = "";
                    obj.descEng = "";
                    obj.descLoc = "";
                    obj.groupFlag = "G";

                    obj.Approved = true;
                    CommonVariables.ApprovedCode = obj.Approved;
                }
                else
                {
                    if (dtbl != null && dtbl.Rows.Count > 0)
                    {

                        obj.reportCd = dtbl.Rows[0]["REPORT_CD"].ToString();
                        obj.DefinedCD = dtbl.Rows[0]["DEFINED_CD"].ToString();
                        obj.upperReportCd = dtbl.Rows[0]["UPPER_REPORT_CD"].ToString();
                        obj.descEng = dtbl.Rows[0]["DESC_ENG"].ToString();
                        obj.descLoc = dtbl.Rows[0]["DESC_LOC"].ToString();
                        obj.groupFlag = dtbl.Rows[0]["GROUP_FLAG"].ConvertToString();

                        obj.Approved = (dtbl.Rows[0]["APPROVED"].ToString() == "N") ? false : true;
                        CommonVariables.ApprovedCode = obj.Approved;

                    }
                    else
                    {
                        obj.reportCd = "";
                        obj.DefinedCD = "";
                        obj.upperReportCd = "";
                        // obj.upperOfficel = "";
                        obj.descEng = "";
                        obj.descLoc = "";
                        obj.groupFlag = "G";
                        obj.Approved = true;
                        CommonVariables.ApprovedCode = obj.Approved;
                    }
                }
                if (obj.upperReportCd != "")
                {
                    ViewData["ddl_District"] = cm.GetDistricts("");
                    ViewData["ddl_VDCMun"] = cm.GetVDCMunByDistrict("", "");
                    ViewData["ddl_Ward"] = cm.GetWardByVDCMun("", "");
                    return PartialView("_ReportSearchParam");
                }
                Session["ReportGrpCd"] = obj.groupFlag;
                if (cd != null)
                {
                    return PartialView("_GetReportDetail", obj);
                }
                else
                {
                    objReportServices = new NHRSReportService();
                    List<TreeItems> trReport= objReportServices.GetReportHierarchy("");
                    ViewBag.ReportHer = trReport;
                    return View("ReportDetail", obj);
                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                return RedirectToAction("ReportHierarchy");
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                return RedirectToAction("ReportHierarchy");
            }


        }
        [CustomAuthorizeAttribute(PermCd = PermissionCode.Add)]
        public ActionResult ReportAdd(string p, FormCollection fc)
        {
            string grpFlag = "";
            grpFlag = (Session["ReportGrpCd"] == null) ? "" : Session["ReportGrpCd"].ConvertToString();
            if (grpFlag == "D")
            {
                return RedirectToAction("GetReportDetail");
            }
            else
            {
                NHRSReportService ps = new NHRSReportService();
                List<TreeItems> trReport = null;
                IList<SelectListItem> gList = null;
                MISReport objReport = new MISReport();
                CommonFunction common = new CommonFunction();
                try
                {
                    trReport = ps.GetReportHierarchy("");
                    gList = getGroupData(string.Empty);
                    objReport.reportCd = CommonVariables.SelectedCode;
                    objReport.DefinedCD = common.GetDefinedCodeFromDataBase(objReport.reportCd, "MIS_REPORT", "REPORT_CD");
                    objReport.Approved = CommonVariables.ApprovedCode;
                    ViewBag.ReportHer = trReport;
                    objReport.Continents = gList;
                    ViewData["Group"] = gList;

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
        }

        [CustomAuthorizeAttribute(PermCd = PermissionCode.Edit)]
        public ActionResult ReportEdit(string p, FormCollection fc, string ErrorMsg)
        {
            Utils.setUrl(this.Url);
            NHRSReportService ps = new NHRSReportService();
            DataTable dtbl = null;
            MISReport objReport = new MISReport();
            //string gender = "";
            try
            {
                dtbl = ps.GetReportDetail(CommonVariables.SelectedCode);
                if (dtbl != null)
                {
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        objReport.reportCd = dr["REPORT_CD"].ConvertToString();
                        objReport.DefinedCD = dr["DEFINED_CD"].ConvertToString();
                        objReport.descEng = dr["DESC_ENG"].ConvertToString();
                        objReport.descLoc = dr["DESC_LOC"].ConvertToString();
                        objReport.upperReportCd = dr["UPPER_REPORT_CD"].ConvertToString();
                        objReport.disabled = ((dr["DISABLED"] == null) ? false : ((dr["DISABLED"].ToString().ToUpper() == "Y") ? true : false));
                        objReport.Approved = ((dr["APPROVED"] == null) ? false : ((dr["APPROVED"].ToString().ToUpper() == "Y") ? true : false));
                        objReport.ApprovedBy = dr["APPROVED_BY"].ConvertToString();
                        objReport.ApprovedDt = dr["APPROVED_DT"].ToDateTime("dd-MM-yyyy");
                        objReport.ApprovedDtLoc = dr["APPROVED_DT_LOC"].ConvertToString();
                        objReport.enteredBy = dr["entered_By"].ConvertToString();
                        objReport.enteredDt = dr["entered_Dt"].ToDateTime("dd-MM-yyyy");
                        objReport.groupFlag = dr["group_Flag"].ConvertToString();
                    }
                }
                string createdDate = DateTime.Now.ConvertToString();
                //objReport.upperReportCd = 
                List<TreeItems> trReport = ps.GetReportHierarchy("");
                IList<SelectListItem> gList = getGroupData(objReport.groupFlag);
                ViewData["Group"] = gList;
                //ViewBag.officeType = GetOfficeType(objOffice.officeType);
                ViewBag.ReportHer = trReport;
                ViewBag.Approved = objReport.Approved;
                ViewBag.ErrorMsg = ErrorMsg;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View(objReport);
        }
        public IList<SelectListItem> getGroupData(String GrpId)
        {
            IList<SelectListItem> GroupList = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Value = "G";
            li.Text = Utils.GetLabel("Group");
            if (GrpId == "G")
                li.Selected = true;
            GroupList.Add(li);

            li = new SelectListItem();
            li.Value = "D";
            li.Text = Utils.GetLabel("Details");
            if (GrpId == "D")
                li.Selected = true;
            GroupList.Add(li);
            return GroupList;

        }
        [CustomAuthorizeAttribute(PermCd = PermissionCode.Add)]
        public ActionResult SaveReportDetail(FormCollection fc, MISReport objReport)
        {
            QueryResult qryResult = new QueryResult();
            NHRSReportService objReportServices = new NHRSReportService();
            try
            {
                if (fc["btnCancel"] != null)
                {
                    return RedirectToAction("MenuList");
                }
                TryUpdateModel(objReport);


                objReport.enteredBy = CommonVariables.UserName;
                objReport.groupFlag = fc["Group"];
                if (objReport.groupFlag == "D")
                {
                    objReport.upperReportCd = CommonVariables.GroupCD;
                }
                else
                {
                    
                }
                objReport.enteredDt = DateTime.Now;
                objReport.ApprovedDt = DateTime.Now;
                string exep = "";
                qryResult = objReportServices.ReportUID(objReport, "I", out exep);
                if (qryResult.IsSuccess)
                {
                    ViewBag.SucessMessage = "Success";
                    Session["UpdateGlobalData"] = "true";
                    Session["UpdatedType"] = DataType.Office;
                }
                else
                {
                    
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
            return RedirectToAction("GetReportDetail");
        }

        /// <summary>
        /// Save Edited Office
        /// </summary>
        /// <param name="fc"></param>
        /// <param name="objOffice"></param>
        /// <returns></returns>
        [CustomAuthorizeAttribute(PermCd = PermissionCode.Edit)]
        public ActionResult EditReportDetail(FormCollection fc, MISReport objReport)
        {
            string errorMessage = "";
            QueryResult qryResult = new QueryResult();
            NHRSReportService objReportServices = new NHRSReportService();
            try
            {
                if (fc["btnCancel"] != null)
                {
                    return RedirectToAction("OfficeList");
                }
                TryUpdateModel(objReport);
                objReport.groupFlag = fc["Group"];
                objReport.enteredBy = CommonVariables.UserName;
                objReport.enteredDt = DateTime.Now;
                objReport.ApprovedDt = DateTime.Now;
                objReport.upperReportCd = fc["UpperReportCode"];
                string exep = "";
                if (objReport.groupFlag == "D" && !objReportServices.CheckPresenceOfChildren(objReport.reportCd))
                {
                    errorMessage = "Child records present, could not change the group!!";
                    return RedirectToAction("ReportEdit", new { ErrorMsg = errorMessage });
                }
                else
                {
                    qryResult = objReportServices.ReportUID(objReport, "U", out exep);
                    Session["UpdateGlobalData"] = "true";
                    Session["UpdatedType"] = DataType.Office;
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
            return RedirectToAction("GetReportDetail");
        }
    }
}
