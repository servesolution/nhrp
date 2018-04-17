using ExceptionHandler;
using MIS.Authentication;
using MIS.Models.Core;
using MIS.Models.Security;
using MIS.Models.Setup;
using MIS.Services.Core;
using MIS.Services.Setup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Setup
{
    public class BankBranchController : BaseController
    {
        CommonFunction common = new CommonFunction();
        public ActionResult ManageBankBranch(string p)
        {
            BankBranchService objBankBranchService = new BankBranchService();
            NHRSBankBranch objBankBranch = new NHRSBankBranch();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            if (p != null)
            {
                if (rvd != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd["id"] != null)
                    {
                        id = rvd["id"].ToString();
                        objBankBranch.BANK_CD = Convert.ToString(id);
                        objBankBranch = objBankBranchService.FillBranchBank(id);
                        objBankBranch.VDC_MUN_CD = common.GetDefinedCodeFromDataBase(objBankBranch.VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                        ViewData["ddl_District"] = common.GetDistricts(objBankBranch.DISTRICT_CD.ConvertToString());
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objBankBranch.VDC_MUN_CD.ConvertToString(), objBankBranch.DISTRICT_CD.ConvertToString());
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankBranch.WARD_NO, "");
                        ViewData["ddl_BankName"] = common.GetBankName(objBankBranch.BANK_CD.ConvertToString());
                        ViewData["ddl_IsdishdQtr"] = (List<SelectListItem>)common.GetYesNo1(objBankBranch.IS_DIST_HEADQTR.ConvertToString()).Data;
                        objBankBranch.Mode = "U";
                        return View(objBankBranch);
                    }
                }
            }
            objBankBranch.Mode = "I";
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objBankBranch.VDC_MUN_CD.ConvertToString(), objBankBranch.DISTRICT_CD.ConvertToString());
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            if (CommonVariables.GroupCD == "50")
            {
                if (CommonVariables.UserCode != "")
                {
                    string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                    ViewData["ddl_BankName"] = common.getBankbyUser(bank_cd);


                }

            }
            else {
                ViewData["ddl_BankName"] = common.GetBankName("", "", "");
            }
           
            ViewData["ddl_IsdishdQtr"] = (List<SelectListItem>)common.GetYesNo1("").Data;
            return View(objBankBranch);
        }
        [HttpPost]
        public ActionResult ManageBankBranch(FormCollection Fc)
        {
            BankBranchService objBankBranchService = new BankBranchService();
            NHRSBankBranch objBankBranchName = new NHRSBankBranch();
            try
            {
                objBankBranchName.BANK_CD = Fc["BANK_CD"].ConvertToString();
                objBankBranchName.BANK_CD = common.GetCodeFromDataBase(objBankBranchName.BANK_CD.ConvertToString(), "NHRS_BANK", "BANK_CD");
                objBankBranchName.BANK_BRANCH_CD = Fc["BANK_BRANCH_CD"].ToDecimal();
                objBankBranchName.DEFINED_CD = Fc["DEFINED_CD"].ConvertToString();
                objBankBranchName.DESC_ENG = Fc["DESC_ENG"].ConvertToString();
                objBankBranchName.DESC_LOC = Fc["DESC_LOC"].ConvertToString();
                objBankBranchName.DISTRICT_CD = Fc["DISTRICT_CD"].ToString();
                objBankBranchName.VDC_MUN_CD = common.GetCodeFromDataBase(Fc["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                objBankBranchName.WARD_NO = Fc["WARD_NO"].ToString();
                objBankBranchName.TELEPHONE_NO = Fc["TELEPHONE_NO"].ConvertToString();
                objBankBranchName.FAX_NO = Fc["FAX_NO"].ConvertToString();
                objBankBranchName.URL = Fc["URL"].ConvertToString();
                objBankBranchName.EMAIL = Fc["EMAIL"].ConvertToString();
                objBankBranchName.ADDRESS_ENG = Fc["ADDRESS_ENG"].ConvertToString();
                objBankBranchName.IS_DIST_HEADQTR = Fc["ddl_IsdishdQtr"].ConvertToString();
                objBankBranchName.BRANCH_STD_CD = Fc["BRANCH_STD_CD"].ConvertToString();
                if (objBankBranchName.IS_DIST_HEADQTR == "Y")
                {
                    objBankBranchName.NUM_CLAIM_LIMIT = "50";
                }
                else
                {
                    objBankBranchName.NUM_CLAIM_LIMIT = "20";
                }
                objBankBranchName.BANK_TYPE = "DAO";
                if (Fc["btn_Submit"].ToString() == "Submit" || Fc["btn_Submit"].ToString() == "पेश गर्नुहोस्")
                {
                    objBankBranchName.Mode = "I";
                    objBankBranchService.insertBankBranchName(objBankBranchName);
                }
                if (Fc["btn_Submit"].ToString() == "Update" || Fc["btn_Submit"].ToString() == "अपडेट")
                {
                    objBankBranchName.Mode = "U";
                    objBankBranchService.insertBankBranchName(objBankBranchName);
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
            return RedirectToAction("ManageBankBranch");
        }
        public ActionResult BankBranchList(string p)
        
        {
            CheckPermission();
            string initial = "";
            BankBranchService objBankBranchService = new BankBranchService();
            DataTable dt = new DataTable();
            CommonFunction common = new CommonFunction();
            RouteValueDictionary rvd = new RouteValueDictionary();
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
                    }
                }
                dt = objBankBranchService.getallBankName(initial);
                ViewData["dtBankName"] = dt;
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("","");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("","");
                if (CommonVariables.GroupCD == "50")
                {
                    if (CommonVariables.UserCode != "")
                    {
                        string bank_cd = CommonFunction.GetBankByUserCode(CommonVariables.UserCode);
                        ViewData["ddl_bankname"] = common.getBankbyUser(bank_cd);


                    }

                }
                else
                {
                    ViewData["ddl_bankname"] = common.GetBankName("", "");
                }
                
                ViewBag.initial = initial;
                ViewBag.actionName = "BankBranchList";
                ViewBag.controllerName = "BankBranch";
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
        [HttpPost]
        [ActionName("BankBranchList")]
        public ActionResult BankBranchList(FormCollection fc)
        {
            NHRSBankBranch objBankBranch = new NHRSBankBranch();
            BankBranchService objBankBranchService = new BankBranchService();
            DataTable dt = new DataTable();
            CheckPermission();

                objBankBranch.DISTRICT_CD=fc["ddl_District"].ConvertToString();
                //objBankBranch.VDC_MUN_CD=fc["ddl_VDCMun"].ConvertToString();
                objBankBranch.VDC_MUN_CD = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                objBankBranch.WARD_NO=fc["ddl_Ward"].ConvertToString();
                objBankBranch.BANK_CD = fc["ddl_bankname"].ConvertToString();
                objBankBranch.BANK_CD = common.GetCodeFromDataBase(objBankBranch.BANK_CD.ConvertToString(), "NHRS_BANK", "BANK_CD");
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddl_bankname"] = common.GetBankName("", "");
                dt = objBankBranchService.getBankBranchList(objBankBranch);

                ViewData["dtBankName"] = dt;
                Session["BankBranchList"] = dt;
                Session["objBankBranch"] = objBankBranch;

                return PartialView("~/Views/BankBranch/_BankBranch.cshtml");
        }
        public ActionResult DeleteBankBranchDetail(string p)
        {
            CheckPermission();
            NHRSBankBranch objBankBranchName = new NHRSBankBranch();

            NHRSBankBranch BankBranchNameSearch = new NHRSBankBranch();

            NhrsBankBranchInfo BankBranchInfo = new NhrsBankBranchInfo();
            BankBranchService objBankBranchService = new BankBranchService();
            string id = "";
            string id2 = "";
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
                        if (rvd["id2"] != null)
                        {
                            id2 = rvd["id2"].ToString();

                        }
                        objBankBranchName.BANK_BRANCH_CD = Convert.ToInt32(id);
                        objBankBranchName.BANK_CD = Convert.ToString(id2);
                        objBankBranchName.Mode = "D";
                        objBankBranchService.insertBankBranchName(objBankBranchName);
                        //BankBranchNameSearch = (NHRSBankBranch)Session["objBankBranch"];
                        //DataTable dt = objBankBranchService.getBankBranchList(BankBranchNameSearch);
                        //ViewData["dtBankName"] = dt;
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

            //return PartialView("~/Views/BankBranch/_BankBranch.cshtml");
            return RedirectToAction("BankBranchList");
        }
        [CustomAuthorizeAttribute(PermCd = "7")]
        public ActionResult ChangeStatus(string p)
        {
            CheckPermission();
            Users obj;
            NHRSBankBranch objBankBranchName = new NHRSBankBranch();
            string strUsername = "";
            string status = "";
            BankBranchService objBankBranchService = new BankBranchService();
            string id = "";
            string id2 = "";
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
                        if (rvd["id2"] != null)
                        {
                            id2 = rvd["id2"].ToString();
                        }
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }

                    }
                }
                objBankBranchName.BANK_CD=id2.ConvertToString();
                if (status == "Y")
                {
                    objBankBranchName.APPROVED = "N";
                    objBankBranchName.APPROVED_BY = null;
                    objBankBranchName.APPROVED_DT = null;
                }
                else if (status == "N")
                {
                    objBankBranchName.APPROVED = "Y";
                    objBankBranchName.APPROVED_BY = strUsername;
                    objBankBranchName.APPROVED_DT = DateTime.Now;
                }
                if (Session[SessionCheck.sessionName] != null)
                {
                    obj = (Users)Session[SessionCheck.sessionName];
                    strUsername = obj.usrName;
                }
                objBankBranchName.BANK_BRANCH_CD = Convert.ToDecimal(id);
                objBankBranchName.BANK_CD = Convert.ToString(id2);
                objBankBranchName.Mode = "A";
                objBankBranchService.insertBankBranchName(objBankBranchName);
                DataTable dt = new DataTable();
                dt = objBankBranchService.getBankBranchList(objBankBranchName);

                //BankBranchList("BankBranchList");
                //dt = objBankBranchService.getBankBranchList(objBankBranchName);

                ViewData["dtBankName"] = dt;
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("~/Views/BankBranch/_BankBranch.cshtml");

        }
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
        public JsonResult CheckDuplicateDefinedCode(string id)
        {
            bool boolValue = false;
            BankBranchService objBankBranchService = new BankBranchService();
            boolValue = objBankBranchService.CheckDuplicateBranchID(id);
            return new JsonResult { Data = boolValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult CheckDuplicateSTDCode(string id, string bankcd)
        {
            bool boolValue = false;
            BankBranchService objBankBranchService = new BankBranchService();
            boolValue = objBankBranchService.CheckDuplicateBranchstdID(id, bankcd);
            return new JsonResult { Data = boolValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}


