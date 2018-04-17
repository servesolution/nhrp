
using MIS.Models.Resettlement;
using MIS.Services.Core;
using MIS.Services.Resettlement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Resettlement
{
    public class ResettlementTargetingController : BaseController
    {
        //
        // GET: /ResettlementTargeting/
        CommonFunction common = new CommonFunction();
        public ActionResult ResettlementTargetingSearch()
        {

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Wards"] = common.GetWardByVDCMun("", "");
            ViewData["ddlTarget"] = common.GetTargetType1("");
            ViewData["ResettlementEligbllProcess"] = Session["ResettlementEligbllProcess"].ConvertToString();
            Session["ResettlementEligbllProcess"] = null;
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)common.GetResettlementReview("").Data;


            return View();
        }


        [HttpPost]
        public ActionResult ResettlementTargetingSearch(FormCollection fc, ResettlementTargetingModel objTargetingSearch)
        {
            DataTable dt = new DataTable();
            ResettlementTargetingService objService = new ResettlementTargetingService();
            objTargetingSearch.MisReview = fc["ddl_MIS_review"].ConvertToString();
            if (Session["LanguageSetting"].ConvertToString() == "English")
            {
                objTargetingSearch.Language= "E";
            }
            else
            {
                objTargetingSearch.Language = "N";
            }
            //objTargetingSearch.Targeted = fc["ddlTarget"].ConvertToString();
            //if (objTargetingSearch.Targeted == "N")
            //{
                dt = objService.getResettlementTargetingNewApplicant(objTargetingSearch);
            //}
            ViewData["result"] = dt;
            return PartialView("_ResettlementSearchResult", objTargetingSearch);
             
        }

        public ActionResult saveEligibleResettlement(string p)
        {
            ResettlementTargetingService objService = new ResettlementTargetingService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            bool result = false;
            string session_id = "";
            string ResettlementId = "";
            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    session_id = rvd["session"].ConvertToString();
                   // ResettlementId = rvd["ResettlementId"].ConvertToString();
                }
            }
            result = objService.saveEligibleResettlement(session_id, ResettlementId);
            if(result==true)
            {
                Session["ResettlementEligbllProcess"] = "Success";
            }
            else
            {
                Session["ResettlementEligbllProcess"] = "Failed";
            }
            return RedirectToAction("ResettlementTargetingSearch");
        }


        #region resettlement targeted benf Search
        public ActionResult ResettlementTargetedBenficiary()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_target_lot"] = common.GetTargetBatch("", "");
            ViewData["ddlApprove"] = common.GetApprove("");
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)common.GetResettlementReview("").Data;
            return View();
        }
        [HttpPost]
        public ActionResult ResettlementTargetedBenficiary(FormCollection fc)
        {
            string districtCd = null;
            string vdcMunCd = null;
            string ward = null;
            string fileBatch = null;
            string approval = null;
            string approvedDate = null;
            DataTable dt = new DataTable(); 
            ResettlementTargetingService objService = new ResettlementTargetingService();

            districtCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            vdcMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
            string misReview =  fc["ddl_MIS_review"].ConvertToString();
            ward = fc["ddl_Ward"].ConvertToString();
            fileBatch = fc["target_lot"].ConvertToString();
            approval = fc["ddlApprove"].ConvertToString();
            approvedDate = fc["approveDate"].ConvertToString();
            dt = objService.getResettlementTargetedData(districtCd, vdcMunCd, ward, fileBatch, approval, approvedDate, misReview);
            ViewData["TargetedData"] = dt;
            ViewData["IfApproved"] = approval;
            return PartialView("_ResettlementTargetedBenficiary");
        }
        #endregion
        
        #region approve targeted


        //public ActionResult ResettlementTargetedApprove(string p, FormCollection fc)
        //{
        //    ResettlementTargetingService objService = new ResettlementTargetingService();
        //    RouteValueDictionary rvd = new RouteValueDictionary();
        //    bool result = false;
        //    string resettlementEligibleId = String.Empty;
        //    string HouseOwnerID = String.Empty;
        //    string checkedid = string.Empty;

        //    if (p != null)
        //    {
        //        rvd = QueryStringEncrypt.DecryptString(p);
        //        if (rvd != null)
        //        {
        //            resettlementEligibleId = rvd["resettlementEligibleId"].ConvertToString();
        //            checkedid = Request.QueryString["checkedid"].ConvertToString();

        //            //if (checkedid == "All")
        //            //{
        //            //    result = objService.TargetedApprovedAll(resettlementEligibleId);
        //            //}
        //            //else
        //            //{
        //            //    result = objService.SelectedTargetingApproved(checkedid);
        //            //}

        //        }
        //    }


        //    if (result)
        //    {
        //        TempData["Message"] = "Approved Successfully.";
        //    }
        //    return RedirectToAction("RetroBeneficiaryList");
        //}



        public ActionResult ResettlementTargetedApprove(string allFlag, string IdAll, string checkedid)
        {
            ResettlementTargetingService objService = new ResettlementTargetingService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            bool result = false;


            //if (allFlag != null)
            //{ checkedid = Request.QueryString["checkedid"].ConvertToString();

            if (allFlag == "Y")
            {
                result = objService.TargetedApprovedAll(IdAll);
            }
            else
            {
                //string[] selectedId = checkedid.Split(',');
                //int length = selectedId.Length;
                //for (int i = 0; i < length;i++ )
                //{
                result = objService.SelectedTargetingApproved(checkedid);
                //}

                   
                IdAll = checkedid;
            }

                
            //}


            if (result)
            {
                TempData["Message"] = "Approved Successfully.";
                
            }

            return RedirectToAction("ResettlementTargetedBenficiary");
        }
        #endregion

    }
}
