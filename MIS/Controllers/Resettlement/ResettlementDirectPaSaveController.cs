using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using MIS.Services.Core;
using MIS.Services.Resettlement;
using System.Configuration;
using System.Data.OleDb;
using MIS.Models.ManageResettlement;
using System.Web.Routing;

namespace MIS.Controllers.Resettlement
{
    public class ResettlementDirectPaSaveController : BaseController
    {
        //
        // GET: /ResettlementDirectPaSave/
        CommonFunction com = new CommonFunction();
        public ActionResult ManageResettlementPaGenerate(string p)
        {

            ResettlementModelClass objModel = new ResettlementModelClass();
            ResettlementDirectPaSaveService objService = new ResettlementDirectPaSaveService();
            ViewData["ddl_Districts"] = com.GetDistricts("");
            ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)com.GetResettlementReview("").Data;


            RouteValueDictionary rvd = new RouteValueDictionary();
            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["id"] != null)
                    {
                        objModel.ResettlementId = (rvd["id"]).ConvertToString();
                    }
                    if (rvd["Mode"] != null)
                    {
                        objModel.Mode = (rvd["Mode"]).ConvertToString();
                    }

                }
                if (objModel.Mode == "U")
                {
                    objModel = objService.getResettlementDataById(objModel.ResettlementId.ConvertToString());
                    ViewData["ddl_Districts"] = com.GetDistrictsByDistrictCode(objModel.ResDistrict.ConvertToString());
                    ViewData["ddl_Vdc"] = com.GetVDCMunByDistrictCode(objModel.ResVDCMUN.ConvertToString(), objModel.ResDistrict.ConvertToString());
                    ViewData["ddl_Wards"] = com.GetWardByVDCMun(objModel.ResWard.ConvertToString(), "");
                    objModel.ResVDCMUN = com.GetDefinedCodeFromDataBase(objModel.ResVDCMUN.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

                    ViewData["ddl_MIS_review"] = (List<SelectListItem>)com.GetResettlementReview(objModel.ResMisReview.ConvertToString()).Data;
                    objModel.Mode = "U";
                    return View(objModel);
                }
                if (objModel.Mode == "D")
                {
                    bool res = objService.saveResettlementWithPA(objModel);
                    return RedirectToAction("ResettlementList");
                }

            }
            objModel.Mode = "I";
            return View(objModel);
        }
        [HttpPost]
        public ActionResult ManageResettlementPaGenerate(ResettlementModelClass objModel)
        {
            bool result = false;
            ResettlementDirectPaSaveService objService = new ResettlementDirectPaSaveService();
            objModel.ResVDCMUN = com.GetCodeFromDataBase(objModel.ResVDCMUN, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            result = objService.saveResettlementWithPA(objModel);
            Session["SaveedResult"] = result;
            Session["SaveedMessage"] = objModel.ResFirstName + " " + objModel.ResMiddleName + " " + objModel.ResLastName;
            return RedirectToAction("ResettlementList");
        }




        #region resettlement list
        public ActionResult ResettlementList()
        {

            ViewData["ddl_Districts"] = com.GetDistricts("");
            ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
            ViewData["ddl_MIS_review"] = (List<SelectListItem>)com.GetResettlementReview("").Data;
            ViewData["ResettlSurveySave"] = Session["ResettlSurveySave"].ConvertToString();
            Session["ResettlSurveySave"] = null;

            ViewData["SaveedResult"] = Session["SaveedResult"].ConvertToString();
            ViewData["SaveedMessage"] = Session["SaveedMessage"].ConvertToString();
            Session["SaveedMessage"] = null;
            Session["SaveedResult"] = null;


            return View();
        }
        [HttpPost]
        public ActionResult ResettlementList(FormCollection fc)
        {
            DataTable dt = new DataTable();
            ResettlementDirectPaSaveService objService = new ResettlementDirectPaSaveService();
            ResettlementModelClass objModel = new ResettlementModelClass();
            objModel.ResDistrict = fc["ResDistrict"].ConvertToString();
            objModel.ResVDCMUN = fc["ResVDCMUN"].ConvertToString();
            objModel.ResVDCMUN = com.GetCodeFromDataBase(objModel.ResVDCMUN, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            objModel.ResWard = fc["ward"].ConvertToString();

            objModel.ResMisReview = fc["ddl_MIS_review"].ConvertToString();
            objModel.ResPaNo = fc["ResPaNo"].ConvertToString();
            //objModel.ResFirstName = fc["district"].ConvertToString();

            dt = objService.GetAllResettlement(objModel);
            ViewData["ResettlementData"] = dt;
            return PartialView("_ResettlementDirectPaList");
        }
        #endregion

    }
}
