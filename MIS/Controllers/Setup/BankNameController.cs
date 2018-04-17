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
    public class BankNameController : BaseController
    {
        CommonFunction common = new CommonFunction();
        public ActionResult ManageBankName(string p)
        {
            BankNameService objBankService = new BankNameService();
            NHRPBankName objBankName = new NHRPBankName();
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
                        objBankName.BANK_CD = Convert.ToString(id);
                        objBankName = objBankService.FillBank(id);
                        ViewData["ddl_District"] = common.GetDistricts(objBankName.DISTRICT_CD.ConvertToString());
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objBankName.VDC_MUN_CD.ConvertToString(), objBankName.DISTRICT_CD.ConvertToString());
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankName.WARD_NO.ConvertToString(),objBankName.VDC_MUN_CD);
                        objBankName.MODE = "U";
                        return View(objBankName);
                    }
                }
            }
            objBankName.MODE = "I";
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objBankName.VDC_MUN_CD.ConvertToString(), objBankName.DISTRICT_CD.ConvertToString());
            ViewData["ddl_Ward"] = common.GetWardByVDCMun(objBankName.WARD_NO.ConvertToString(), objBankName.VDC_MUN_CD);
            return View(objBankName);
        }
        [HttpPost]
        public ActionResult ManageBankName(FormCollection Fc)
        {
            BankNameService objBankService = new BankNameService();
            NHRPBankName objBankName = new NHRPBankName();
           // objBankName.BANK_CD = Fc["BANK_CD"].ConvertToString();
          //  objBankName.DEFINED_CD = Fc["DEFINED_CD"].ConvertToString();
            objBankName.DESC_ENG = Fc["DESC_ENG"].ConvertToString();
            objBankName.DESC_LOC = Fc["DESC_LOC"].ConvertToString();
            objBankName.DISTRICT_CD = Fc["DISTRICT_CD"].ToDecimal();
            objBankName.VDC_MUN_CD = common.GetCodeFromDataBase(Fc["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objBankName.WARD_NO = Fc["WARD_NO"].ToDecimal();
            objBankName.TELEPHONE_NO = Fc["TELEPHONE_NO"].ConvertToString();
            objBankName.FAX_NO = Fc["FAX_NO"].ConvertToString();
            objBankName.URL = Fc["URL"].ConvertToString();
            objBankName.EMAIL = Fc["EMAIL"].ConvertToString();
            objBankName.ADDRESS_ENG = Fc["ADDRESS_ENG"].ConvertToString();
            objBankName.ADDRESS_LOC = Fc["ADDRESS_LOC"].ConvertToString();
            objBankName.APPROVED = "N";
            objBankName.BANK_TYPE = "DAO";
            if (Fc["btn_Submit"].ToString() == "Submit" || Fc["btn_Submit"].ToString() == "पेश गर्नुहोस्")
            {
                objBankName.BANK_CD=objBankService.GetMaxvalue().ConvertToString();
                objBankName.MODE = "I";
                objBankService.insertBankName(objBankName);
            }
            if (Fc["btn_Submit"].ToString() == "Update" || Fc["btn_Submit"].ToString() == "अपडेट")
            {
                objBankName.MODE = "U";
                objBankService.insertBankName(objBankName);
            }
            return RedirectToAction("BankList");
        }
        public ActionResult BankList(string p)
        {
            CheckPermission();
            string initial = "";
            BankNameService objBankService = new BankNameService();
            DataTable dt = new DataTable();
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
                //ViewData["ddl_bankname"] = common.GetBankName("");
                Session["ddl_bankname"] = ViewData["ddl_bankname"];
                dt = objBankService.getallBankName(initial);
                ViewData["dtBankName"] = dt;
                Session["BankNameLst"] = dt;
                ViewBag.initial = initial;
                ViewBag.actionName = "BankList";
                ViewBag.controllerName = "BankName";
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
        public ActionResult BankList(FormCollection  fc)
        {
            CheckPermission();
            NHRPBankName objBankName = new NHRPBankName();
            BankNameService objBankService = new BankNameService();
            DataTable dt = new DataTable();
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                
                objBankName.BANK_CD = fc["ddl_bankname"].ConvertToString();
                objBankName.BANK_CD = common.GetCodeFromDataBase(objBankName.BANK_CD.ConvertToString(), "NHRS_BANK", "BANK_CD");
                dt = objBankService.getallBankNameList(objBankName);
                ViewData["dtBankName"] = dt;
                Session["BankNameLst"] = dt;
                ViewData["ddl_bankname"]=Session["ddl_bankname"] ;

               
               
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View(objBankName);
        }

        public ActionResult DeleteBankDetail(string p)
        {
            NHRPBankName objBankName = new NHRPBankName();
            BankNameService objBankService = new BankNameService();
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
                            objBankName.BANK_CD = Convert.ToString(id);
                            objBankName.MODE = "D";
                            objBankService.insertBankName(objBankName);
                            ViewData["dtBankName"] = (DataTable)Session["BankNameLst"];
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

            return RedirectToAction("BankList");
        }
        [CustomAuthorizeAttribute(PermCd = "7")]
        public ActionResult ChangeStatus(string p)
        {
            Users obj;
            NHRPBankName objBankName = new NHRPBankName();
            string strUsername = "";
            string status = "";
            
            BankNameService objBankService = new BankNameService();
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
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }

                    }
                }
                if (status == "Y")
                {
                    objBankName.APPROVED = "N";
                    objBankName.APPROVED_BY = null;
                    objBankName.APPROVED_DT = null;
                }
                else if (status == "N")
                {
                    objBankName.APPROVED = "Y";
                    objBankName.APPROVED_BY = strUsername;
                    objBankName.APPROVED_DT = DateTime.Now.ToString() ;
                }
                if (Session[SessionCheck.sessionName] != null)
                {
                    obj = (Users)Session[SessionCheck.sessionName];
                    strUsername = obj.usrName;
                }
                objBankName.BANK_CD = Convert.ToString(id);
                objBankName.MODE = "A";
                objBankService.insertBankName(objBankName);
                
                //ViewData["dtBankName"] = (DataTable)Session["BankNameLst"]; 
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            //return PartialView("~/Views/BankName/BankList.cshtml");
            return RedirectToAction("BankList");


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
    }
}
