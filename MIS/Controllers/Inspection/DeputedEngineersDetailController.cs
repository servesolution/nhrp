using ExceptionHandler;
using MIS.Models.Core;
using MIS.Models.Inspection;
using MIS.Services.Core;
using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Inspection
{
    public class DeputedEngineersDetailController : BaseController
    {
        //
        // GET: /DeputedEngineersDetail/
        CommonFunction com = new CommonFunction();

        #region insert/update/delete deputed engineers informatin
            public ActionResult ManageEngineersInformation(string p)
            {
                DeputedEngineersModelClass objModel = new DeputedEngineersModelClass();
                RouteValueDictionary rvd = new RouteValueDictionary();
                DeputedEngineersDetailService objService = new DeputedEngineersDetailService();
               
                 if (p != null)
                 {
                     rvd = QueryStringEncrypt.DecryptString(p);

                     if (rvd != null)
                     {
                         if (rvd["cd"] != null)
                         {
                             objModel.EngineerId = rvd["cd"].ConvertToString();
                             if (rvd["cd"] != null)
                             { 
                                 if (rvd["cmd"].ConvertToString() == "D")
                                 {
                                     objModel.Mode = "D";
                                     bool result = objService.CRUDEngineersInformation(objModel);//delete
                                     return RedirectToAction("DeputedEngineersList");
                                 }
                             } 
                             
                             objModel = objService.getEngineersById(objModel.EngineerId);//get data by id (edit)
                             ViewData["ddl_Districts"] = com.GetDistricts(objModel.DistrictCd.ConvertToString());
                             objModel.VdcMunDefCd = com.GetDefinedCodeFromDataBase(objModel.VdcMunCd.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
                             //ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objModel.VdcMunDefCd.ConvertToString(), objModel.DistrictCd.ConvertToString());
                             ViewData["ddl_currentVDCMun"] = com.GetNewVDCMunByDistrictVdc(objModel.VdcMunDefCd.ConvertToString(), objModel.DistrictCd.ConvertToString());
                             ViewData["ddl_Wards"] = com.GetWardByVDCMun(objModel.Ward.ConvertToString(), "");
                             ViewData["ddl_designation"] = objService.GetInspectionEngineerDesignation(objModel.DesignationCd.ConvertToString());
                             ViewData["ddl_office"] = objService.GetInspectionEngineerOffice(objModel.OfficeCd.ConvertToString());
                             ViewData["ddl_tab"] = (List<SelectListItem>)com.GetYesNo5(objModel.HasTab.ConvertToString()).Data;
                             ViewData["ddl_tab_function"] = (List<SelectListItem>)com.GetYesNo1(objModel.IsTabFunctional.ConvertToString()).Data;


                             objModel.Mode = "U";
                             return View(objModel);
                         }
                     }
                 }
                 objModel.Mode = "I";

                 ViewData["ddl_Districts"] = com.GetDistricts("");
                  //ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("","");
                  ViewData["ddl_currentVDCMun"] = com.GetNewVDCMunByDistrict("", "");
                 ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
                 ViewData["ddl_designation"] = objService.GetInspectionEngineerDesignation("");
                 ViewData["ddl_office"] = objService.GetInspectionEngineerOffice("");
                 ViewData["ddl_tab"] = (List<SelectListItem>)com.GetYesNo5("N").Data;
                 ViewData["ddl_tab_function"] = (List<SelectListItem>)com.GetYesNo1("").Data;


                 return View(objModel);
            }
            [HttpPost]
            public ActionResult ManageEngineersInformation(FormCollection fc, DeputedEngineersModelClass objModel)
            {
                DeputedEngineersDetailService objService = new DeputedEngineersDetailService();
                objModel.VdcMunCd = com.GetCodeFromDataBase(objModel.VdcMunDefCd.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
                try
                {
                    bool SaveResult = objService.CRUDEngineersInformation(objModel);
                }
                catch(Exception ex)
                {
                    
                }
                return RedirectToAction("DeputedEngineersList");
            }
        #endregion


        #region check duplicate engineers
            public bool CheckDuplicateEngineers(DeputedEngineersModelClass objModel)
            {
                DeputedEngineersDetailService objService = new DeputedEngineersDetailService();
                bool result = false;
                objModel.VdcMunCd = com.GetCodeFromDataBase(objModel.VdcMunDefCd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                result = objService.checkDuplicateEnginers(objModel);
                return result;
            }
        #endregion

            #region engineers list
            public ActionResult DeputedEngineersList()
            {
                DeputedEngineersDetailService objService = new DeputedEngineersDetailService();
                DeputedEngineersModelClass objModel = new DeputedEngineersModelClass();
                ViewData["ddl_Districts"] = com.GetDistricts(""); 
                ViewData["ddl_currentVDCMun"] = com.GetNewVDCMunByDistrict("", "");
                ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
                ViewData["ddl_designation"] = objService.GetInspectionEngineerDesignation("");
                ViewData["ddl_office"] = objService.GetInspectionEngineerOffice("");

                return View();
            }
        [HttpPost]
            public ActionResult DeputedEngineersList(FormCollection fc, DeputedEngineersModelClass objModel)
            {
                checkPermission();
                DeputedEngineersDetailService objService = new DeputedEngineersDetailService();
                DataTable dt = new DataTable();
                objModel.VdcMunCd = com.GetCodeFromDataBase(objModel.VdcMunDefCd.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
                dt = objService.getAllEngineerList(objModel);
                ViewData["EngineersDt"] = dt;
                return PartialView("_DeputedEngineerList");
            }
            #endregion

            public void checkPermission()
            {
                PermissionParamService objPermissionParamService = new PermissionParamService();
                PermissionParam objPermissionParam = new PermissionParam();
                ViewBag.EnableEdit = "false";
                ViewBag.EnableDelete = "false";
                try
                {
                    objPermissionParam = objPermissionParamService.GetPermissionValue();
                    if (objPermissionParam != null)
                    {
                        if (objPermissionParam.EnableEdit == "true")
                        {
                            ViewBag.EnableEdit = "true";
                        }
                        if (objPermissionParam.EnableDelete == "true")
                        {
                            ViewBag.EnableDelete = "true";
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

    }
}
