using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models.Security;
using MIS.Authentication;
using MIS.Models.NHRP.Setup;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Models.Core;
using MIS.Services.Setup;
using System.Data;
using System.Web.Routing;

namespace MIS.Controllers.Setup
{
    public class BankAccountTypeController : BaseController
    {
        //
        // GET: /BankAccountType/

        public ActionResult BankAccountType(string editid)
        {
            CheckPermission();
            BankAccTyp model = new BankAccTyp();
            try
            {
                if(editid !=null)
                {
                    BankAcctypService obj = new BankAcctypService();
                    string id = "";
                    RouteValueDictionary rvd = new RouteValueDictionary();
                    rvd=QueryStringEncrypt.DecryptString(editid);
                        if(rvd!=null)
                        {
                            if(rvd["id"]!=null)
                            {
                                id = rvd["id"].ToString();
                                model.Bank_Acc_Typ_cd = Convert.ToDecimal(id);
                                model = obj.FillBankAccountType(id);
                                model.Mode = "U";
                            }
                        }
                    }
                    else
                    {
                        if(ViewBag.EnableAdd=="false")
                        {
                            TempData["ErrorMessage"] = "You are not authroize to perform this action.Please Login through different account";
                            return RedirectToAction("BankAccountList");

                        }
                    }
                Utils.setUrl(this.Url);
                }
            catch(OracleException oe)
            {
                ExceptionManager.AppendLog(oe);

            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult BankAccountType(FormCollection fc)
        {
            Utils.setUrl(this.Url);
            CheckPermission();
            BankAcctypService serv=new BankAcctypService();
            BankAccTyp bat = new BankAccTyp();
            try
            {
                bat.Bank_Acc_Typ_cd = fc["Bank_Acc_Typ_cd"].ToDecimal();
                bat.Defined_cd = fc["DEFINED_CD"].ConvertToString();
                bat.Desc_Eng = fc["DESC_ENG"].ConvertToString();
                bat.Desc_Loc = fc["DESC_LOC"].ConvertToString();
                bat.Short_Name = fc["SHORT_NAME"].ConvertToString();
                bat.Short_Name_Loc = fc["SHORT_NAME_LOC"].ConvertToString();
               
                if (fc["btn_submit"].ToString() == Utils.GetLabel("Save"))
                {
                    bat.Bank_Acc_Typ_cd = serv.GetMaxvalue().ToDecimal();
                    bat.Mode="I";
                    bat.Approved = "N";

                    serv.SaveBankAccType(bat);
                }

                if (fc["btn_Submit"].ToString() == Utils.GetLabel("Update"))
                {
                    bat.Approved = "N";

                    bat.Mode = "U";
                    serv.SaveBankAccType(bat);

                }
                

            }
            catch(OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch(Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("BankAccountList");

        }

        public ActionResult BankAccountList(string p)
        {
            CheckPermission();
            BankAcctypService serv=new BankAcctypService();
            string initial = "";
            DataTable dt = new DataTable();
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                if(p!=null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if(rvd!=null)
                    {
                        if(rvd["initial"]!=null)
                        {
                            initial = rvd["initial"].ToString();
                            if(initial.Contains("%E0"))
                            {
                                initial = NepaliUnicode.getValue(initial, NepaliUnicode.NepaliCharacters());

                            }
                        }
                    }
                }
                dt = serv.BankTAccType_GetAllCriteriaToTable(initial);
                ViewData["dtBankAccTypList"] = dt;
                ViewBag.initial = initial;
                ViewBag.actionName = "BankAccountList";
                ViewBag.controllerName = "BankAccountType";

            }
            catch(OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch(Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View();
            
        }
        public ActionResult EditBankAccountType(string p, string cmd)
        {
            return RedirectToAction("BankAccountType", new { editid = p });
        }

        public ActionResult DeleteBankAccountType(string p)
        {
            BankAccTyp obj = new BankAccTyp();
            BankAcctypService batService = new BankAcctypService();
            string id = "";
            Boolean Issuccess = false;
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
                            obj.Bank_Acc_Typ_cd = Convert.ToInt32(id);
                            obj.Mode = "D";
                            Issuccess = batService.ManageBankAccTyp(obj);
                        }
                    }
                }
                if (Issuccess)
                {
                    ViewData["Message"] = "Success";
                }
                else if (p == "20099")
                {
                    TempData["ErrorMessage"] = "Cannot delete record being used";
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

            return RedirectToAction("BankAccountList");
        }

        public ActionResult ChangeStatus(string p)
        {
            Users objUsers;
            string strUsername = "";
            string status = "";
            BankAccTyp obj = new BankAccTyp();
            BankAcctypService bnkService = new BankAcctypService();
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
                    obj.Approved = false.ToString();
                    obj.Approved_by = null;
                    obj.Approved_dt = null;
                }
                else if (status == "N")
                {
                    obj.Approved = true.ToString();
                    obj.Approved_by = strUsername;
                    obj.Approved_dt = DateTime.Now.ToString();
                }
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUsername = objUsers.usrName;
                }
                obj.Bank_Acc_Typ_cd = Convert.ToDecimal(id);
                obj.Mode = "A";
                bnkService.ManageBankAccTyp(obj);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("BankAccountList");

        }
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
