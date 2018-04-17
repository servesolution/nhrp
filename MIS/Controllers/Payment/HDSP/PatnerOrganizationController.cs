
using ClosedXML.Excel;
using EntityFramework;
using ExceptionHandler;
using MIS.Authentication;
using MIS.Models.Payment.HDSP;
using MIS.Models.Security;
using MIS.Models.Setup;
using MIS.Services.Core;
using MIS.Services.Enrollment;
using MIS.Services.Payment.HDSP;
using MIS.Services.Vulnerability;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Payment.HDSP
{
    public class PatnerOrganizationController : BaseController
    {
        CommonFunction common = new CommonFunction();
        //public JsonResult DeleteDuplicateData()
        //{
        //    EnrollmentImportExport enrollService = new EnrollmentImportExport();
        //    QueryResult status = null;

        //    status = enrollService.DeletePODupData();
        //    if (status.IsSuccess)
        //    {
        //        return Json("Success");
        //    }
        //    return Json("failed");
        //}

        //public ActionResult GetPOFirstTrancheError()
        // {
        //    ViewData["ddl_District"] = common.GetDistricts("");
        //    ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
        //    ViewData["ddl_Donor"] = common.getDonorList("");
        //    #region display message
        //    if (!string.IsNullOrEmpty(Session["Message"].ConvertToString()))
        //    {

        //        if (Session["Message"].ConvertToString() == "DeleteSuccess")
        //        {
        //            ViewData["DeleteSuccess"] = "Success!";
        //        }
        //        if (Session["Message"].ConvertToString() == "DeleteFailed")
        //        {
        //            ViewData["DeleteFailed"] = "Failed!";
        //        }

        //        if (Session["Message"].ConvertToString() == "UpdateSuccess")
        //        {
        //            ViewData["UpdateSuccess"] = "Success!";
        //        }
        //        if (Session["Message"].ConvertToString() == "UpdateFailed")
        //        {
        //            ViewData["UpdateFailed"] = "Failed!";
        //        }

        //        Session["ApprovedMessage"] = "";
        //    }
        //    #endregion
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult GetPOFirstTrancheError(FormCollection fc)
        //{
        //    EnrollmentImportExport enrollService = new EnrollmentImportExport();

        //    CommonFunction common = new CommonFunction();

        //    DataTable errorList = new DataTable();
        //    string district = fc["ddl_District"].ToString();
        //   // string vdc = common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
        //    string donorcd = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);

        //    if (!string.IsNullOrEmpty(district))
        //    {
        //        try
        //        {
        //            errorList = enrollService.GetErrorList(donorcd, district);
        //        }
        //        catch (Exception ex)
        //        {
        //            errorList = null;
        //            ExceptionHandler.ExceptionManager.AppendLog(ex);
        //        }

        //    }
        //    Session["ErrorFiles"] = errorList;
        //    ViewData["ErrorFiles"] = errorList;

        //    return PartialView("~/Views/PartnerOrganization/_POFirstTrancheErrorList.cshtml");
        //}

        public ActionResult ListPaymentPO()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult ListPaymentPO(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            objPO.PA_NO = fc["panum"].ToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = "1".ToString();

            result = enrollService.ListPOData(objPO);

            if (CommonVariables.GroupCD.ToString() != "1" && CommonVariables.GroupCD.ToString() != "33")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewData["dtPOList"] = result;
            Session["dtPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListPOData.cshtml");

        }
        public ActionResult ListSecondPaymentPO()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "RemoveSuccess")
                {
                    ViewData["RemoveSuccess"] = "Success!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "RemoveFailed")
                {
                    ViewData["RemoveFailed"] = "Failed!";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            return View();
        }
        public ActionResult ListThirdPaymentPO()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "RemoveSuccess")
                {
                    ViewData["RemoveSuccess"] = "Success!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "RemoveFailed")
                {
                    ViewData["RemoveFailed"] = "Failed!";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult ListThirdPaymentPO(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.PA_NO = fc["panum"].ToString();
            objPO.MODE = "L";
            objPO.payroll_install_cd = "3";
            result = enrollService.ListPOData(objPO);

            if (CommonVariables.GroupCD.ToString() != "1" && CommonVariables.GroupCD.ToString() != "33")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewData["dtPOList"] = result;
            Session["dtPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListThirdPaymentPOData.cshtml");

        }
        [HttpPost]
        public ActionResult ListSecondPaymentPO(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            objPO.PA_NO = fc["panum"].ToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = "2";
            objPO.MODE = "L";
            result = enrollService.ListPOData(objPO);

            if (CommonVariables.GroupCD.ToString() != "1" && CommonVariables.GroupCD.ToString() != "33")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewData["dtPOList"] = result;
            Session["dtPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListSecondPaymentPOData.cshtml");

        }
        public ActionResult ListMSPaymentPO()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult ListMSPaymentPO(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            objPO.PA_NO = fc["panum"].ToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = "4".ToString();

            result = enrollService.ListPOData(objPO);

            if (CommonVariables.GroupCD.ToString() != "1" && CommonVariables.GroupCD.ToString() != "33")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewData["dtPOList"] = result;
            Session["dtPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListMSPaymentPOData.cshtml");

        }
        public ActionResult UpdatePOFirst(FormCollection fc)
        {
            string support_cd = fc["editForm"];
            return RedirectToAction("AddPaymentPOSupportedBenef", new RouteValueDictionary(
             new { controller = "PatnerOrganization", action = "AddPaymentPOSupportedBenef", Id = support_cd }));
        }
        public ActionResult UpdatePOMS(FormCollection fc)
        {
            string support_cd = fc["editForm"];
            return RedirectToAction("AddMSPaymentPOSupportedBenef", new RouteValueDictionary(
             new { controller = "PatnerOrganization", action = "AddMSPaymentPOSupportedBenef", Id = support_cd }));
        }
        public ActionResult UpdatePOMH(FormCollection fc)
        {
            string support_cd = fc["editForm"];
            return RedirectToAction("AddModelHousePaymentPOSupportedBenef", new RouteValueDictionary(
             new { controller = "PatnerOrganization", action = "AddModelHousePaymentPOSupportedBenef", Id = support_cd }));
        }

        public ActionResult UpdatePOTS(FormCollection fc)
        {
            string support_cd = fc["editForm"];
            return RedirectToAction("AddPOTransportationSupport", new RouteValueDictionary(
             new { controller = "PatnerOrganization", action = "AddPOTransportationSupport", Id = support_cd }));
        }

        public ActionResult UpdateTranchePO(PaymentPartnerOrganization objPO)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            QueryResult qr = enrollService.UpdateTranchePO(objPO);
            if (qr.IsSuccess == true)
            {
                Session["ApprovedMessage"] = "Success";
            }
            else
            {
                Session["ApprovedMessage"] = "Failed";
            }

            return Json('0');
        }

        public ActionResult AddPaymentPOSupportedBenef(string Id)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            VulnerabilityService objVul = new VulnerabilityService();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();
            string support_cd = "";

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            if (Id != "" && Id != null)
            {
                support_cd = Id;
                var charToRemove = new string[] { "x", "A", "v", "t", "k", "d", "f", "k", "t", "h", "f", "g" };

                foreach (var c in charToRemove)
                {
                    support_cd = support_cd.Replace(c, string.Empty);
                }

                objPO = enrollService.GetPODtlbyPA(support_cd);

                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }


                ViewData["ddl_Bank"] = common.getBankbyUser((objPO.Bank_cd).ToString());
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "", (objPO.Bank_cd).ToString(), "");
                ViewData["ddl_District"] = common.GetDistricts(objPO.Dis_Cd.ToString());
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode(objPO.Vdc_Mun_Cd.ToString(), objPO.Dis_Cd.ToString());
                ViewData["ddl_Ward"] = common.GetWardByVDCMun(objPO.Ward_Num.ConvertToString(), objPO.Vdc_Mun_Cd.ToString());
                ViewData["ddl_Support"] = common.GetInstallation(objPO.payroll_install_cd.ToString());

                objPO.MODE = "U";
                objPO.Support_CD = support_cd.ToInt32();

                return View(objPO);
            }
            else
            {
                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }

                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

                ViewData["ddl_Support"] = common.GetInstallation("").Where(x => x.Value.ToString() == "1"); ;

                objPO.MODE = "I";
                return View(objPO);
            }

        }
        public ActionResult ListModelHousePaymentPO()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult ListModelHousePaymentPO(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = "5".ToString();
            objPO.PA_NO = fc["panum"].ToString();

            result = enrollService.ListPOData(objPO);

            if (CommonVariables.GroupCD.ToString() != "1" && CommonVariables.GroupCD.ToString() != "33")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewData["dtPOList"] = result;
            Session["dtPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListModelHousePaymentPOData.cshtml");

        }

        public ActionResult ListPOTransportationSupport()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();

            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");
            ViewData["ddl_Installation"] = common.GetInstallation("");
            ViewData["ddl_FiscalYr"] = common.GetFiscalYear("");

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            return View();
        }
        [HttpPost]
        public ActionResult ListPOTransportationSupport(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = "6".ToString();
            objPO.PA_NO = fc["panum"].ToString();

            result = enrollService.ListPOData(objPO);

            if (CommonVariables.GroupCD.ToString() != "1" && CommonVariables.GroupCD.ToString() != "33")
            {
                ViewBag.UserStatus = "InvalidUser";
            }

            ViewData["dtPOList"] = result;
            Session["dtPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListPOTranspotationSupport.cshtml");

        }

        [HttpPost]
        public ActionResult AddPaymentPOSupportedBenef(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            QueryResult qr = new QueryResult();
            string user_cd = SessionCheck.getSessionUserCode();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            string support_cd = "1";
            if (fc != null)
            {
                objPO.PA_NO = fc["PA_NO"].ToString();

                objPO.Beneficiary_Name = fc["Beneficiary_Name"].ToString();

                if (!string.IsNullOrEmpty(fc["Reciepient_Name"].ToString()))
                {
                    objPO.Reciepient_Name = fc["Reciepient_Name"].ToString();
                }
                else
                {
                    objPO.Reciepient_Name = fc["Beneficiary_Name"].ToString();
                }


                objPO.House_SN = fc["House_SN"].ToString();

                objPO.Nissa_No = fc["Nissa_No"].ToString();

                objPO.Donor_CD = fc["ddl_Donor"].ToString();

                objPO.payroll_install_cd = fc["ddl_Support"].ToString();

                objPO.Support_Amount = fc["Support_Amount"].ToString();

                objPO.Remarks = fc["Remarks"].ToString();

                objPO.MODE = fc["MODE"].ToString();

                objPO.Area = fc["Area"].ToString();


                #region check if PA is Valid
                bool isValidPA = objService.CheckIfPAExists(objPO.PA_NO);
                if (!isValidPA)
                {
                    Session["ApprovedMessage"] = "Invalid PA!";
                    return RedirectToAction("AddPaymentPOSupportedBenef");
                }
                #endregion

                #region check if PA already exists in database
                bool doesExistPA = objService.CheckDuplicatePAforPO(objPO.PA_NO, support_cd, objPO.MODE.ToString());
                if (!doesExistPA)
                {
                    Session["ApprovedMessage"] = "PA Exists!";
                    return RedirectToAction("AddPaymentPOSupportedBenef");
                }
                #endregion

                #region PA_NO splits
                if (objPO.PA_NO.ToString().Trim() != "")
                {
                    string Pa_no = objPO.PA_NO.ToString();
                    string[] splits = Pa_no.Split('-');
                    string vdc_cd = "";
                    string district_cd = "";
                    int cnt = splits.Count();
                    if (cnt == 5)
                    {

                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 6)
                    {
                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 7)
                    {
                        district_cd = splits[splits.Length - 6].ToString();
                        vdc_cd = splits[splits.Length - 5].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 4].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }



                }
                #endregion

                if (objPO.MODE == "I")
                {
                    objPO.ENTERED_BY = SessionCheck.getSessionUsername();
                    objPO.ENTERED_DT = DateTime.Now.ToString();
                    objPO.ENTERED_DT_LOC = NepaliDate.getNepaliDate(objPO.ENTERED_DT);
                }

                if (objPO.MODE == "U")
                {
                    objPO.Support_CD = fc["Support_CD"].ToInt32();
                    objPO.UPDATED_BY = SessionCheck.getSessionUsername();
                    objPO.UPDATED_DT = DateTime.Now.ToString();
                    objPO.UPDATED_DT_LOC = NepaliDate.getNepaliDate(objPO.UPDATED_DT);
                }

                qr = enrollService.InsertSinglePODtl(objPO);

                if (qr.IsSuccess == true)
                {
                    Session["ApprovedMessage"] = "Success";
                }
                else
                {
                    Session["ApprovedMessage"] = "Failed";
                }

            }

            return RedirectToAction("ListPaymentPO");
        }

        public JsonResult DeleteTranchePO(PaymentPartnerOrganization poObj)
        {
            QueryResult qr = new QueryResult();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            string support_cd = poObj.Support_CD.ToString();
            string install_cd = poObj.payroll_install_cd;

            qr = enrollService.DeleteTranchePO(support_cd, install_cd);
            if (qr.IsSuccess)
            {
                Session["ApprovedMessage"] = "RemoveSuccess";
            }
            else
            {
                Session["ApprovedMessage"] = "RemoveFailed";
            }

            return Json("Success");
        }
        public ActionResult AddSecondPaymentPOSupportedBenef()
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            VulnerabilityService objVul = new VulnerabilityService();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();
            string support_cd = "";



            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                Session["ApprovedMessage"] = "";
            }
            #endregion

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

            objPO.Support_CD = support_cd.ToInt32();

            return View();

        }

        [HttpPost]
        public ActionResult AddSecondPaymentPOSupportedBenef(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            objPO.PA_NO = fc["panum"].ConvertToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = "1";
            objPO.MODE = "A";
            result = enrollService.ListPOData(objPO);

            ViewData["dtSecondPOList"] = result;
            Session["dtSecondPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_AddSecondPOData.cshtml");
        }

        public ActionResult AddThirdPaymentPOSupportedBenef()
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            VulnerabilityService objVul = new VulnerabilityService();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();
            string support_cd = "";

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                Session["ApprovedMessage"] = "";
            }
            #endregion

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

            objPO.Support_CD = support_cd.ToInt32();

            return View();

        }
        [HttpPost]
        public ActionResult AddThirdPaymentPOSupportedBenef(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            objPO.Bank_cd = fc["ddl_Bank"].ConvertToString();
            objPO.Branch_Std_Cd = fc["ddl_BankBranch"].ConvertToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.PA_NO = fc["panum"].ConvertToString();
            objPO.payroll_install_cd = "2";
            objPO.MODE = "A";
            result = enrollService.ListPOData(objPO);

            ViewData["dtThirdPOList"] = result;
            Session["dtThirdPOList"] = result;

            return PartialView("~/Views/PatnerOrganization/_AddThirdPOData.cshtml");
        }

        public ActionResult AddMSPaymentPOSupportedBenef(string Id)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            VulnerabilityService objVul = new VulnerabilityService();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();
            string support_cd = "";

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            if (Id != "" && Id != null)
            {
                support_cd = Id;
                var charToRemove = new string[] { "x", "A", "v", "t", "k", "d", "f", "k", "t", "h", "f", "g" };

                foreach (var c in charToRemove)
                {
                    support_cd = support_cd.Replace(c, string.Empty);
                }

                objPO = enrollService.GetPODtlbyPA(support_cd);

                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }


                ViewData["ddl_Bank"] = common.getBankbyUser((objPO.Bank_cd).ToString());
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "", (objPO.Bank_cd).ToString(), "");
                ViewData["ddl_District"] = common.GetDistricts(objPO.Dis_Cd.ToString());
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode(objPO.Vdc_Mun_Cd.ToString(), objPO.Dis_Cd.ToString());
                ViewData["ddl_Ward"] = common.GetWardByVDCMun(objPO.Ward_Num.ConvertToString(), objPO.Vdc_Mun_Cd.ToString());
                ViewData["ddl_Support"] = common.GetInstallation(objPO.payroll_install_cd).Where(x => x.Value.ToString() == "4");

                objPO.MODE = "U";
                objPO.Support_CD = support_cd.ToInt32();

                return View(objPO);
            }
            else
            {
                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }

                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

                ViewData["ddl_Support"] = common.GetInstallation("").Where(x => x.Value.ToString() == "4");

                objPO.MODE = "I";
                return View(objPO);
            }

        }
        [HttpPost]
        public ActionResult AddMSPaymentPOSupportedBenef(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            QueryResult qr = new QueryResult();
            string user_cd = SessionCheck.getSessionUserCode();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            string support_cd = "4";
            if (fc != null)
            {
                objPO.PA_NO = fc["PA_NO"].ToString();

                objPO.Beneficiary_Name = fc["Beneficiary_Name"].ToString();

                if (!string.IsNullOrEmpty(fc["Reciepient_Name"].ToString()))
                {
                    objPO.Reciepient_Name = fc["Reciepient_Name"].ToString();
                }
                else
                {
                    objPO.Reciepient_Name = fc["Beneficiary_Name"].ToString();
                }

                objPO.House_SN = fc["House_SN"].ToString();

                objPO.Nissa_No = fc["Nissa_No"].ToString();

                objPO.Donor_CD = fc["ddl_Donor"].ToString();

                objPO.payroll_install_cd = fc["ddl_Support"].ToString();

                objPO.Support_Amount = fc["Support_Amount"].ToString();

                objPO.Remarks = fc["Remarks"].ToString();

                objPO.MODE = fc["MODE"].ToString();

                objPO.Area = fc["Area"].ToString();


                #region check if PA is Valid
                bool isValidPA = objService.CheckIfPAExists(objPO.PA_NO);
                if (!isValidPA)
                {
                    Session["ApprovedMessage"] = "Invalid PA!";
                    return RedirectToAction("AddMSPaymentPOSupportedBenef");
                }
                #endregion

                #region check if PA already exists in database
                bool doesExistPA = objService.CheckDuplicatePAforPO(objPO.PA_NO, support_cd, objPO.MODE.ToString());
                if (!doesExistPA)
                {
                    Session["ApprovedMessage"] = "PA Exists!";
                    return RedirectToAction("AddMSPaymentPOSupportedBenef");
                }
                #endregion

                #region PA_NO splits
                if (objPO.PA_NO.ToString().Trim() != "")
                {
                    string Pa_no = objPO.PA_NO.ToString();
                    string[] splits = Pa_no.Split('-');
                    string vdc_cd = "";
                    string district_cd = "";
                    int cnt = splits.Count();
                    if (cnt == 5)
                    {

                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 6)
                    {
                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 7)
                    {
                        district_cd = splits[splits.Length - 6].ToString();
                        vdc_cd = splits[splits.Length - 5].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 4].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }



                }
                #endregion

                if (objPO.MODE == "I")
                {
                    objPO.ENTERED_BY = SessionCheck.getSessionUsername();
                    objPO.ENTERED_DT = DateTime.Now.ToString();
                    objPO.ENTERED_DT_LOC = NepaliDate.getNepaliDate(objPO.ENTERED_DT);
                }

                if (objPO.MODE == "U")
                {
                    objPO.Support_CD = fc["Support_CD"].ToInt32();
                    objPO.UPDATED_BY = SessionCheck.getSessionUsername();
                    objPO.UPDATED_DT = DateTime.Now.ToString();
                    objPO.UPDATED_DT_LOC = NepaliDate.getNepaliDate(objPO.UPDATED_DT);
                }

                qr = enrollService.InsertSinglePODtl(objPO);

                if (qr.IsSuccess == true)
                {
                    Session["ApprovedMessage"] = "Success";
                }
                else
                {
                    Session["ApprovedMessage"] = "Failed";
                }

            }

            return RedirectToAction("ListMSPaymentPO");
        }

        public ActionResult AddModelHousePaymentPOSupportedBenef(string Id)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            VulnerabilityService objVul = new VulnerabilityService();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();
            string support_cd = "";

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            if (Id != "" && Id != null)
            {
                support_cd = Id;
                var charToRemove = new string[] { "x", "A", "v", "t", "k", "d", "f", "k", "t", "h", "f", "g" };

                foreach (var c in charToRemove)
                {
                    support_cd = support_cd.Replace(c, string.Empty);
                }

                objPO = enrollService.GetPODtlbyPA(support_cd);

                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }


                ViewData["ddl_Bank"] = common.getBankbyUser((objPO.Bank_cd).ToString());
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "", (objPO.Bank_cd).ToString(), "");
                ViewData["ddl_District"] = common.GetDistricts(objPO.Dis_Cd.ToString());
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode(objPO.Vdc_Mun_Cd.ToString(), objPO.Dis_Cd.ToString());
                ViewData["ddl_Ward"] = common.GetWardByVDCMun(objPO.Ward_Num.ConvertToString(), objPO.Vdc_Mun_Cd.ToString());
                ViewData["ddl_Support"] = common.GetInstallation(objPO.payroll_install_cd.ToString()).Where(x => x.Value.ToString() == "5");

                objPO.MODE = "U";
                objPO.Support_CD = support_cd.ToInt32();

                return View(objPO);
            }
            else
            {
                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }

                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

                ViewData["ddl_Support"] = common.GetInstallation("").Where(x => x.Value.ToString() == "5");
                objPO.MODE = "I";
                return View(objPO);
            }

        }
        [HttpPost]
        public ActionResult AddModelHousePaymentPOSupportedBenef(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            QueryResult qr = new QueryResult();
            string user_cd = SessionCheck.getSessionUserCode();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            string support_cd = "5";
            if (fc != null)
            {
                objPO.PA_NO = fc["PA_NO"].ToString();

                objPO.Beneficiary_Name = fc["Beneficiary_Name"].ToString();

                if (!string.IsNullOrEmpty(fc["Reciepient_Name"].ToString()))
                {
                    objPO.Reciepient_Name = fc["Reciepient_Name"].ToString();
                }
                else
                {
                    objPO.Reciepient_Name = fc["Beneficiary_Name"].ToString();
                }

                objPO.House_SN = fc["House_SN"].ToString();

                objPO.Nissa_No = fc["Nissa_No"].ToString();

                objPO.Donor_CD = fc["ddl_Donor"].ToString();

                objPO.payroll_install_cd = fc["ddl_Support"].ToString();

                objPO.Support_Amount = fc["Support_Amount"].ToString();

                objPO.Remarks = fc["Remarks"].ToString();

                objPO.MODE = fc["MODE"].ToString();

                objPO.Area = fc["Area"].ToString();


                #region check if PA is Valid
                bool isValidPA = objService.CheckIfPAExists(objPO.PA_NO);
                if (!isValidPA)
                {
                    Session["ApprovedMessage"] = "Invalid PA!";
                    return RedirectToAction("AddModelHousePaymentPOSupportedBenef");
                }
                #endregion

                #region check if PA already exists in database
                bool doesExistPA = objService.CheckDuplicatePAforPO(objPO.PA_NO, support_cd, objPO.MODE.ToString());
                if (!doesExistPA)
                {
                    Session["ApprovedMessage"] = "PA Exists!";
                    return RedirectToAction("AddModelHousePaymentPOSupportedBenef");
                }
                #endregion

                #region PA_NO splits
                if (objPO.PA_NO.ToString().Trim() != "")
                {
                    string Pa_no = objPO.PA_NO.ToString();
                    string[] splits = Pa_no.Split('-');
                    string vdc_cd = "";
                    string district_cd = "";
                    int cnt = splits.Count();
                    if (cnt == 5)
                    {

                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 6)
                    {
                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 7)
                    {
                        district_cd = splits[splits.Length - 6].ToString();
                        vdc_cd = splits[splits.Length - 5].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 4].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }



                }
                #endregion

                if (objPO.MODE == "I")
                {
                    objPO.ENTERED_BY = SessionCheck.getSessionUsername();
                    objPO.ENTERED_DT = DateTime.Now.ToString();
                    objPO.ENTERED_DT_LOC = NepaliDate.getNepaliDate(objPO.ENTERED_DT);
                }

                if (objPO.MODE == "U")
                {
                    objPO.Support_CD = fc["Support_CD"].ToInt32();
                    objPO.UPDATED_BY = SessionCheck.getSessionUsername();
                    objPO.UPDATED_DT = DateTime.Now.ToString();
                    objPO.UPDATED_DT_LOC = NepaliDate.getNepaliDate(objPO.UPDATED_DT);
                }

                qr = enrollService.InsertSinglePODtl(objPO);

                if (qr.IsSuccess == true)
                {
                    Session["ApprovedMessage"] = "Success";
                }
                else
                {
                    Session["ApprovedMessage"] = "Failed";
                }

            }

            return RedirectToAction("ListModelHousePaymentPO");
        }
        public ActionResult AddPOTransportationSupport(string Id)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            VulnerabilityService objVul = new VulnerabilityService();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();
            string support_cd = "";

            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "Success")
                {
                    ViewData["SuccessMessage"] = "Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FailedMessage"] = "Fialed!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "Invalid PA!")
                {
                    ViewData["FailedMessage"] = "Invalid PA!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "PA Exists!")
                {
                    ViewData["FailedMessage"] = "PA already exists in Database";
                }

                Session["ApprovedMessage"] = "";
            }
            #endregion

            if (Id != "" && Id != null)
            {
                support_cd = Id;
                var charToRemove = new string[] { "x", "A", "v", "t", "k", "d", "f", "k", "t", "h", "f", "g" };

                foreach (var c in charToRemove)
                {
                    support_cd = support_cd.Replace(c, string.Empty);
                }

                objPO = enrollService.GetPODtlbyPA(support_cd);

                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }


                ViewData["ddl_Bank"] = common.getBankbyUser((objPO.Bank_cd).ToString());
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "", (objPO.Bank_cd).ToString(), "");
                ViewData["ddl_District"] = common.GetDistricts(objPO.Dis_Cd.ToString());
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrictCode(objPO.Vdc_Mun_Cd.ToString(), objPO.Dis_Cd.ToString());
                ViewData["ddl_Ward"] = common.GetWardByVDCMun(objPO.Ward_Num.ConvertToString(), objPO.Vdc_Mun_Cd.ToString());
                ViewData["ddl_Support"] = common.GetInstallation(objPO.payroll_install_cd.ToString()).Where(x => x.Value.ToString() == "6");

                objPO.MODE = "U";
                objPO.Support_CD = support_cd.ToInt32();

                return View(objPO);
            }
            else
            {
                if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
                {
                    donor_cd = objVul.GetDonorCdFromUsrCd(user_cd);
                    ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
                }
                else
                {
                    ViewData["ddl_Donor"] = common.getDonorList("");
                }

                ViewData["ddl_Bank"] = common.GetBankName("");
                ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

                ViewData["ddl_Support"] = common.GetInstallation("").Where(x => x.Value.ToString() == "6");
                objPO.MODE = "I";
                return View(objPO);
            }

        }
        [HttpPost]
        public ActionResult AddPOTransportationSupport(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            QueryResult qr = new QueryResult();
            string user_cd = SessionCheck.getSessionUserCode();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            string support_cd = "6";
            if (fc != null)
            {
                objPO.PA_NO = fc["PA_NO"].ToString();

                objPO.Beneficiary_Name = fc["Beneficiary_Name"].ToString();

                if (!string.IsNullOrEmpty(fc["Reciepient_Name"].ToString()))
                {
                    objPO.Reciepient_Name = fc["Reciepient_Name"].ToString();
                }
                else
                {
                    objPO.Reciepient_Name = fc["Beneficiary_Name"].ToString();
                }

                objPO.House_SN = fc["House_SN"].ToString();

                objPO.Nissa_No = fc["Nissa_No"].ToString();

                objPO.Donor_CD = fc["ddl_Donor"].ToString();

                objPO.payroll_install_cd = fc["ddl_Support"].ToString();

                objPO.Support_Amount = fc["Support_Amount"].ToString();

                objPO.Remarks = fc["Remarks"].ToString();

                objPO.MODE = fc["MODE"].ToString();

                objPO.Area = fc["Area"].ToString();


                #region check if PA is Valid
                bool isValidPA = objService.CheckIfPAExists(objPO.PA_NO);
                if (!isValidPA)
                {
                    Session["ApprovedMessage"] = "Invalid PA!";
                    return RedirectToAction("AddPOTransportationSupport");
                }
                #endregion

                #region check if PA already exists in database
                bool doesExistPA = objService.CheckDuplicatePAforPO(objPO.PA_NO, support_cd, objPO.MODE.ToString());
                if (!doesExistPA)
                {
                    Session["ApprovedMessage"] = "PA Exists!";
                    return RedirectToAction("AddPOTransportationSupport");
                }
                #endregion

                #region PA_NO splits
                if (objPO.PA_NO.ToString().Trim() != "")
                {
                    string Pa_no = objPO.PA_NO.ToString();
                    string[] splits = Pa_no.Split('-');
                    string vdc_cd = "";
                    string district_cd = "";
                    int cnt = splits.Count();
                    if (cnt == 5)
                    {

                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 6)
                    {
                        district_cd = splits[splits.Length - 5].ToString();
                        vdc_cd = splits[splits.Length - 4].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 3].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }
                    else if (cnt == 7)
                    {
                        district_cd = splits[splits.Length - 6].ToString();
                        vdc_cd = splits[splits.Length - 5].ToString();
                        objPO.Dis_Cd = common.GetMLDDistCd(district_cd);
                        objPO.Ward_Num = splits[splits.Length - 4].ToString();
                        objPO.Vdc_Mun_Cd = common.GetVdcMunCd(district_cd, vdc_cd).ToString();
                    }



                }
                #endregion

                if (objPO.MODE == "I")
                {
                    objPO.ENTERED_BY = SessionCheck.getSessionUsername();
                    objPO.ENTERED_DT = DateTime.Now.ToString();
                    objPO.ENTERED_DT_LOC = NepaliDate.getNepaliDate(objPO.ENTERED_DT);
                }

                if (objPO.MODE == "U")
                {
                    objPO.Support_CD = fc["Support_CD"].ToInt32();
                    objPO.UPDATED_BY = SessionCheck.getSessionUsername();
                    objPO.UPDATED_DT = DateTime.Now.ToString();
                    objPO.UPDATED_DT_LOC = NepaliDate.getNepaliDate(objPO.UPDATED_DT);
                }

                qr = enrollService.InsertSinglePODtl(objPO);

                if (qr.IsSuccess == true)
                {
                    Session["ApprovedMessage"] = "Success";
                }
                else
                {
                    Session["ApprovedMessage"] = "Failed";
                }

            }

            return RedirectToAction("ListPOTransportationSupport");
        }
        public ActionResult BulkMSUploadPaymentPOSupportedBenef()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");

            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["CONFLICTMESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["CONFLICTMESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";
            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "APPROVED")
                {
                    ViewData["SuccessMessage"] = "APPROVE SUCCESSFUL!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "APPROVEDFAILED")
                {
                    ViewData["FailedMessage"] = "APPROVE FIALED!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteSuccess")
                {
                    ViewData["SuccessMessage"] = "Delete Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteFailed")
                {
                    ViewData["FailedMessage"] = "Delete Failed!";
                }
                Session["ApprovedMessage"] = "";
            }
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult BulkMSUploadPaymentPOSupportedBenef(HttpPostedFileBase file, FormCollection fc)
        {
            DataTable filteredDt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            bool rs = false;
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            string support_cd = "4";

            int batchId;
            string DISTRICT_CD = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            string VDC_CD = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["PO_CONFLICT"] = "";
            List<string> ErrorList = new List<string>();

            batchId = (objService.GetConflictedBatchId()).ToInt32();

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/PartnerOrganization/";



                if (file.FileName.Contains("-"))
                {

                    Session["PO_CONFLICT"] = "File Name invalid! Remove symbols";
                    return RedirectToAction("BulkMSUploadPaymentPOSupportedBenef");
                }


                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string File_Name = file.FileName.Replace(" ", "_");
                Session["fileName"] = File_Name;
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.POFilesList();
                    if (!fileListInDB.Contains(File_Name))
                    {

                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");

                            if (oDataTable.Rows.Count > 0)
                            {
                                filteredDt = oDataTable.Clone();

                                #region duplicate within the file

                                DataTable distinct = oDataTable.DefaultView.ToTable(true, "VICTIM_CODE(PA_NO)");
                                if (distinct.Rows.Count == oDataTable.Rows.Count)
                                {
                                    //there are no duplicates
                                }
                                else
                                {
                                    var duplicates = oDataTable.AsEnumerable()
                                        .GroupBy(r => new { Col1 = r["VICTIM_CODE(PA_NO)"] })
                                        .SelectMany(grp => grp.Skip(1));

                                    foreach (var item in duplicates)
                                    {
                                        string faultPA = item.ItemArray[1].ToString();
                                        ErrorList.Add("Upload Failed! Duplicate PA within file!");

                                    }

                                }
                                #endregion

                                for (int i = 0; i < oDataTable.Rows.Count; i++)
                                {
                                    #region check if district is same as dropdown
                                    if (oDataTable.Rows[i][1].ToString() != "")
                                    {
                                        string Pa_no = oDataTable.Rows[i][1].ToString();
                                        string[] splits = Pa_no.Split('-');
                                        int cnt = splits.Count();
                                        if (cnt == 5)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 6)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 7)
                                        {
                                            nra_district = splits[splits.Length - 6].ToString();
                                            nra_vdc = splits[splits.Length - 5].ToString();
                                            nra_ward = splits[splits.Length - 4].ToString();
                                        }

                                    }
                                    #endregion
                                    #region check if required fields are empty
                                    if (oDataTable.Rows[i][2].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Beneficiary Name empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][11].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Donor Code empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][12].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Support Package empty at row position " + (i + 1));
                                    }


                                    #endregion

                                    #region Check if PA is valid
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (!objService.CheckIfPAExists(oDataTable.Rows[i][1].ToString()))
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " does not exists");
                                        }
                                    }
                                    else
                                    {
                                        ErrorList.Add("PA Number at row position " + (i + 1) + " is empty.");
                                    }
                                    #endregion

                                    #region Check if PA already exists in DB
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (objService.CheckDuplicatePAforPO(oDataTable.Rows[i][1].ToString(), support_cd, "I") == false)
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " already exists.Please review the file.");
                                        }
                                    }

                                    #endregion
                                    filteredDt.ImportRow(oDataTable.Rows[i]);

                                }

                            }

                            if (filteredDt.Rows.Count > 0)
                            {
                                if (ErrorList.Count > 0)
                                {
                                    Session["PO_CONFLICTList"] = ErrorList;
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                    return RedirectToAction("BulkMSUploadPaymentPOSupportedBenef");
                                }
                                else if (ErrorList.Count < 1)
                                {
                                    rs = objService.BulkUploadPOData(oDataTable, file.FileName.Replace(" ", "_"), DISTRICT_CD, support_cd, out exc);
                                }

                            }
                            else
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                return RedirectToAction("BulkMSUploadPaymentPOSupportedBenef");
                            }

                            if (rs == false)
                            {
                                Session["PO_CONFLICT"] = "Upload Failed!";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                return RedirectToAction("BulkMSUploadPaymentPOSupportedBenef");
                            }
                            else
                            {

                                Session["PO_CONFLICT"] = "Successful!";
                            }
                        }
                    }
                    else
                    {
                        Session["PO_CONFLICT"] = "Upload Failed!File with same name already exists!";
                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                    }
                }
                catch (Exception)
                {
                    Session["PO_CONFLICT"] = "Upload Failed";
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                    return RedirectToAction("BulkMSUploadPaymentPOSupportedBenef");

                }
            }
            return RedirectToAction("BulkMSUploadPaymentPOSupportedBenef");
        }
        public ActionResult BulkModelHouseUploadPaymentPOSupportedBenef()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");

            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["CONFLICTMESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["CONFLICTMESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";
            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "APPROVED")
                {
                    ViewData["SuccessMessage"] = "APPROVE SUCCESSFUL!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "APPROVEDFAILED")
                {
                    ViewData["FailedMessage"] = "APPROVE FIALED!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteSuccess")
                {
                    ViewData["SuccessMessage"] = "Delete Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteFailed")
                {
                    ViewData["FailedMessage"] = "Delete Failed!";
                }
                Session["ApprovedMessage"] = "";
            }
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult BulkModelHouseUploadPaymentPOSupportedBenef(HttpPostedFileBase file, FormCollection fc)
        {
            DataTable filteredDt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            bool rs = false;
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            string support_cd = "5";

            int batchId;
            string DISTRICT_CD = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            string VDC_CD = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["PO_CONFLICT"] = "";
            List<string> ErrorList = new List<string>();

            batchId = (objService.GetConflictedBatchId()).ToInt32();

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/PartnerOrganization/";


                if (file.FileName.Contains("-"))
                {

                    Session["PO_CONFLICT"] = "File Name invalid! Remove symbols";
                    return RedirectToAction("BulkModelHouseUploadPaymentPOSupportedBenef");
                }


                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string File_Name = file.FileName.Replace(" ", "_");
                Session["fileName"] = File_Name;
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.POFilesList();
                    if (!fileListInDB.Contains(File_Name))
                    {

                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");

                            if (oDataTable.Rows.Count > 0)
                            {
                                filteredDt = oDataTable.Clone();

                                #region duplicate within the file

                                DataTable distinct = oDataTable.DefaultView.ToTable(true, "VICTIM_CODE(PA_NO)");
                                if (distinct.Rows.Count == oDataTable.Rows.Count)
                                {
                                    //there are no duplicates
                                }
                                else
                                {
                                    var duplicates = oDataTable.AsEnumerable()
                                        .GroupBy(r => new { Col1 = r["VICTIM_CODE(PA_NO)"] })
                                        .SelectMany(grp => grp.Skip(1));

                                    foreach (var item in duplicates)
                                    {
                                        string faultPA = item.ItemArray[1].ToString();
                                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                        ErrorList.Add("Upload Failed! Duplicate PA within file!");

                                    }
                                    //return RedirectToAction("BulkUploadPaymentPOSupportedBenef");

                                }
                                #endregion

                                for (int i = 0; i < oDataTable.Rows.Count; i++)
                                {
                                    #region check if district is same as dropdown
                                    if (oDataTable.Rows[i][1].ToString() != "")
                                    {
                                        string Pa_no = oDataTable.Rows[i][1].ToString();
                                        string[] splits = Pa_no.Split('-');
                                        int cnt = splits.Count();
                                        if (cnt == 5)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 6)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 7)
                                        {
                                            nra_district = splits[splits.Length - 6].ToString();
                                            nra_vdc = splits[splits.Length - 5].ToString();
                                            nra_ward = splits[splits.Length - 4].ToString();
                                        }


                                    }
                                    #endregion
                                    #region check if required fields are empty
                                    if (oDataTable.Rows[i][2].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Beneficiary Name empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][11].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Donor Code empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][12].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Support Package empty at row position " + (i + 1));
                                    }


                                    #endregion

                                    #region Check if PA is valid
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (!objService.CheckIfPAExists(oDataTable.Rows[i][1].ToString()))
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " does not exists");
                                        }
                                    }
                                    else
                                    {
                                        ErrorList.Add("PA Number at row position " + (i + 1) + " is empty.");
                                    }
                                    #endregion

                                    #region Check if PA already exists in DB
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (objService.CheckDuplicatePAforPO(oDataTable.Rows[i][1].ToString(), support_cd, "I") == false)
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " already exists.Please review the file.");
                                        }
                                    }

                                    #endregion

                                    filteredDt.ImportRow(oDataTable.Rows[i]);

                                }

                            }

                            if (filteredDt.Rows.Count > 0)
                            {
                                if (ErrorList.Count > 0)
                                {
                                    Session["PO_CONFLICTList"] = ErrorList;
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                    return RedirectToAction("BulkModelHouseUploadPaymentPOSupportedBenef");
                                }
                                else if (ErrorList.Count < 1)
                                {
                                    rs = objService.BulkUploadPOData(oDataTable, file.FileName.Replace(" ", "_"), DISTRICT_CD, support_cd, out exc);
                                }

                            }
                            else
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                return RedirectToAction("BulkModelHouseUploadPaymentPOSupportedBenef");
                            }

                            if (rs == false)
                            {
                                Session["PO_CONFLICT"] = "Upload Failed!";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                return RedirectToAction("BulkModelHouseUploadPaymentPOSupportedBenef");
                            }
                            else
                            {

                                Session["PO_CONFLICT"] = "Successful!";
                            }
                        }
                    }
                    else
                    {

                        Session["PO_CONFLICT"] = "Upload Failed!File with same name already exists!";
                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                    }
                }
                catch (Exception)
                {
                    Session["PO_CONFLICT"] = "Upload Failed";
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                    return RedirectToAction("BulkModelHouseUploadPaymentPOSupportedBenef");

                }
            }
            return RedirectToAction("BulkModelHouseUploadPaymentPOSupportedBenef");
        }
        public ActionResult BulkUploadPaymentPOSupportedBenef()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");

            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["CONFLICTMESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["CONFLICTMESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";
            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "APPROVED")
                {
                    ViewData["SuccessMessage"] = "APPROVE SUCCESSFUL!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "APPROVEDFAILED")
                {
                    ViewData["FailedMessage"] = "APPROVE FIALED!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteSuccess")
                {
                    ViewData["SuccessMessage"] = "Delete Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteFailed")
                {
                    ViewData["FailedMessage"] = "Delete Failed!";
                }
                Session["ApprovedMessage"] = "";
            }
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult BulkUploadPaymentPOSupportedBenef(HttpPostedFileBase file, FormCollection fc)
        {
            DataTable filteredDt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            bool rs = false;
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            string support_cd = "1";

            int batchId;
            string DISTRICT_CD = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            string VDC_CD = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["PO_CONFLICT"] = "";
            List<string> ErrorList = new List<string>();

            batchId = (objService.GetConflictedBatchId()).ToInt32();

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/PartnerOrganization/";


                if (file.FileName.Contains("-"))
                {

                    Session["PO_CONFLICT"] = "File Name invalid! Remove symbols";
                    return RedirectToAction("BulkUploadPaymentPOSupportedBenef");
                }


                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string File_Name = file.FileName.Replace(" ", "_");
                Session["fileName"] = File_Name;
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.POFilesList();
                    if (!fileListInDB.Contains(File_Name))
                    {

                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");

                            if (oDataTable.Rows.Count > 0)
                            {
                                filteredDt = oDataTable.Clone();

                                #region duplicate within the file

                                DataTable distinct = oDataTable.DefaultView.ToTable(true, "VICTIM_CODE(PA_NO)");
                                if (distinct.Rows.Count == oDataTable.Rows.Count)
                                {
                                    //there are no duplicates
                                }
                                else
                                {
                                    var duplicates = oDataTable.AsEnumerable()
                                        .GroupBy(r => new { Col1 = r["VICTIM_CODE(PA_NO)"] })
                                        .SelectMany(grp => grp.Skip(1));

                                    foreach (var item in duplicates)
                                    {
                                        string faultPA = item.ItemArray[1].ToString();
                                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name));
                                        ErrorList.Add("Upload Failed! Duplicate PA within file!");

                                    }
                                    //return RedirectToAction("BulkUploadPaymentPOSupportedBenef");

                                }
                                #endregion

                                for (int i = 0; i < oDataTable.Rows.Count; i++)
                                {
                                    #region Check if PA is valid
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (!objService.CheckIfPAExists(oDataTable.Rows[i][1].ToString()))
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " does not exists");
                                        }
                                    }
                                    else
                                    {
                                        ErrorList.Add("PA Number at row position " + (i + 1) + " is empty.");
                                    }
                                    #endregion
                                    #region check if district is same as dropdown
                                    if (oDataTable.Rows[i][1].ToString() != "")
                                    {
                                        string Pa_no = oDataTable.Rows[i][1].ToString();
                                        string[] splits = Pa_no.Split('-');
                                        int cnt = splits.Count();
                                        if (cnt == 5)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 6)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 7)
                                        {
                                            nra_district = splits[splits.Length - 6].ToString();
                                            nra_vdc = splits[splits.Length - 5].ToString();
                                            nra_ward = splits[splits.Length - 4].ToString();
                                        }
                                        
                                      
                                    }
                                    #endregion
                                    #region check if required fields are empty
                                    if (oDataTable.Rows[i][2].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Beneficiary Name empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][11].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Donor Code empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][12].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Support Package empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][13].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Support Amount empty at row position " + (i + 1));
                                    }


                                    #endregion
                                    #region Check if PA already exists in DB
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (objService.CheckDuplicatePAforPO(oDataTable.Rows[i][1].ToString(), support_cd, "I") == false)
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " already exists.Please review the file.");
                                        }
                                    }

                                    #endregion

                                    filteredDt.ImportRow(oDataTable.Rows[i]);
                                }
                            }

                            if (filteredDt.Rows.Count > 0)
                            {
                                if (ErrorList.Count > 0)
                                {
                                    Session["PO_CONFLICTList"] = ErrorList;
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name));
                                    return RedirectToAction("BulkUploadPaymentPOSupportedBenef");
                                }
                                else if (ErrorList.Count < 1)
                                {
                                    rs = objService.BulkUploadPOData(oDataTable, file.FileName.Replace(" ", "_"), DISTRICT_CD, support_cd, out exc);
                                }

                            }
                            else
                            {
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name));
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                return RedirectToAction("BulkUploadPaymentPOSupportedBenef");
                            }

                            if (rs == false)
                            {
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name));
                                Session["PO_CONFLICT"] = "Upload Failed!";
                                return RedirectToAction("BulkUploadPaymentPOSupportedBenef");
                            }
                            else
                            {

                                Session["PO_CONFLICT"] = "Successful!";
                            }
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name));
                        Session["PO_CONFLICT"] = "Upload Failed!File with same name already exists!";
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    Session["PO_CONFLICT"] = "Upload Failed";
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(File_Name));
                    return RedirectToAction("BulkUploadPaymentPOSupportedBenef");

                }
            }
            return RedirectToAction("BulkUploadPaymentPOSupportedBenef");

        }
        public ActionResult BulkUploadPOTransSupport()
        {
            NHRSBankMapping objBankMapping = new NHRSBankMapping();
            CommonFunction common = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");

            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }


            if (Session["PO_CONFLICT"].ConvertToString() != "")
            {
                ViewData["CONFLICTMESSAGE"] = Session["PO_CONFLICT"].ToString();
            }
            else
            {
                ViewData["CONFLICTMESSAGE"] = "";
            }

            List<string> ErrorList = (List<string>)Session["PO_CONFLICTList"];
            if (ErrorList != null)
            {
                if (ErrorList.Count > 0)
                {
                    ViewData["POConflictList"] = ErrorList;
                }
            }

            Session["PO_CONFLICTList"] = null;
            Session["PO_CONFLICT"] = "";
            #region display message
            if (!string.IsNullOrEmpty(Session["ApprovedMessage"].ConvertToString()))
            {

                if (Session["ApprovedMessage"].ConvertToString() == "APPROVED")
                {
                    ViewData["SuccessMessage"] = "APPROVE SUCCESSFUL!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "APPROVEDFAILED")
                {
                    ViewData["FailedMessage"] = "APPROVE FIALED!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteSuccess")
                {
                    ViewData["SuccessMessage"] = "Delete Successful!";
                }
                if (Session["ApprovedMessage"].ConvertToString() == "DeleteFailed")
                {
                    ViewData["FailedMessage"] = "Delete Failed!";
                }
                Session["ApprovedMessage"] = "";
            }
            #endregion
            return View();
        }
        [HttpPost]
        public ActionResult BulkUploadPOTransSupport(HttpPostedFileBase file, FormCollection fc)
        {
            DataTable filteredDt = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            string exc = string.Empty;
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            bool rs = false;
            string nra_district = "";
            string nra_vdc = "";
            string nra_ward = "";
            string support_cd = "6";

            int batchId;
            string DISTRICT_CD = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            string VDC_CD = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["PO_CONFLICT"] = "";
            List<string> ErrorList = new List<string>();

            batchId = (objService.GetConflictedBatchId()).ToInt32();

            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/PartnerOrganization/";



                if (file.FileName.Contains("-"))
                {

                    Session["PO_CONFLICT"] = "File Name invalid! Remove symbols";
                    return RedirectToAction("BulkUploadPOTransSupport");
                }


                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_"));
                string File_Name = file.FileName.Replace(" ", "_");
                Session["fileName"] = File_Name;
                try
                {
                    List<string> fileListInDB = new List<string>();

                    fileListInDB = objService.POFilesList();
                    if (!fileListInDB.Contains(File_Name))
                    {

                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, extension, "Yes");

                            if (oDataTable.Rows.Count > 0)
                            {
                                filteredDt = oDataTable.Clone();

                                #region duplicate within the file

                                DataTable distinct = oDataTable.DefaultView.ToTable(true, "VICTIM_CODE(PA_NO)");
                                if (distinct.Rows.Count == oDataTable.Rows.Count)
                                {
                                    //there are no duplicates
                                }
                                else
                                {
                                    var duplicates = oDataTable.AsEnumerable()
                                        .GroupBy(r => new { Col1 = r["VICTIM_CODE(PA_NO)"] })
                                        .SelectMany(grp => grp.Skip(1));

                                    foreach (var item in duplicates)
                                    {
                                        string faultPA = item.ItemArray[1].ToString();
                                        ErrorList.Add("Upload Failed! Duplicate PA within file!");

                                    }
                                    //return RedirectToAction("BulkUploadPaymentPOSupportedBenef");

                                }
                                #endregion

                                for (int i = 0; i < oDataTable.Rows.Count; i++)
                                {
                                    #region check if district is same as dropdown
                                    if (oDataTable.Rows[i][1].ToString() != "")
                                    {
                                        string Pa_no = oDataTable.Rows[i][1].ToString();
                                        string[] splits = Pa_no.Split('-');
                                        int cnt = splits.Count();
                                        if (cnt == 5)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 6)
                                        {
                                            nra_district = splits[splits.Length - 5].ToString();
                                            nra_vdc = splits[splits.Length - 4].ToString();
                                            nra_ward = splits[splits.Length - 3].ToString();
                                        }
                                        else if (cnt == 7)
                                        {
                                            nra_district = splits[splits.Length - 6].ToString();
                                            nra_vdc = splits[splits.Length - 5].ToString();
                                            nra_ward = splits[splits.Length - 4].ToString();
                                        }


                                    }
                                    #endregion
                                    #region check if required fields are empty
                                    if (oDataTable.Rows[i][2].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Beneficiary Name empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][11].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Donor Code empty at row position " + (i + 1));
                                    }

                                    if (oDataTable.Rows[i][12].ToString().Trim() == "")
                                    {
                                        ErrorList.Add("Support Package empty at row position " + (i + 1));
                                    }


                                    #endregion

                                    #region Check if PA is valid
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (!objService.CheckIfPAExists(oDataTable.Rows[i][1].ToString()))
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " does not exists");
                                        }
                                    }
                                    else
                                    {
                                        ErrorList.Add("PA Number at row position " + (i + 1) + " is empty.");
                                    }
                                    #endregion

                                    #region Check if PA already exists in DB
                                    if (oDataTable.Rows[i][1].ToString().Trim() != "")
                                    {
                                        if (objService.CheckDuplicatePAforPO(oDataTable.Rows[i][1].ToString(), support_cd, "I") == false)
                                        {
                                            ErrorList.Add("PA Number at row position " + (i + 1) + " already exists.Please review the file.");
                                        }
                                    }

                                    #endregion
                                    //all filtered data are moved in this table
                                    filteredDt.ImportRow(oDataTable.Rows[i]);

                                }

                            }

                            if (filteredDt.Rows.Count > 0)
                            {
                                if (ErrorList.Count > 0)
                                {
                                    Session["PO_CONFLICTList"] = ErrorList;
                                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                    return RedirectToAction("BulkUploadPOTransSupport");
                                }
                                else if (ErrorList.Count < 1)
                                {
                                    rs = objService.BulkUploadPOData(oDataTable, file.FileName.Replace(" ", "_"), DISTRICT_CD, support_cd, out exc);
                                }

                            }
                            else
                            {
                                Session["PO_CONFLICT"] = "Upload Failed! No Items in Excel Sheet";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                return RedirectToAction("BulkUploadPOTransSupport");
                            }

                            if (rs == false)
                            {
                                Session["PO_CONFLICT"] = "Upload Failed!";
                                System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                                return RedirectToAction("BulkUploadPOTransSupport");
                            }
                            else
                            {

                                Session["PO_CONFLICT"] = "Successful!";
                            }
                        }
                    }
                    else
                    {
                        Session["PO_CONFLICT"] = "Upload Failed!File with same name already exists!";
                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                    }
                }
                catch (Exception)
                {
                    Session["PO_CONFLICT"] = "Upload Failed";
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_"));
                    return RedirectToAction("BulkUploadPOTransSupport");

                }
            }
            return RedirectToAction("BulkUploadPOTransSupport");
        }
        private DataTable Import_To_Grid(string FilePaths, string Extension, string isHDR)
        {
            string conStr = "";
            Extension = Extension.ToLower();
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePaths, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string toReadSheet = "";

            foreach (DataRow row in dtExcelSchema.Rows)
            {
                if (!row["TABLE_NAME"].ToString().Contains("FilterDatabase"))
                {
                    toReadSheet = row["TABLE_NAME"].ToString();
                }

            }
            
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + toReadSheet + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();


            return dt;
        }

        public JsonResult CheckEditPermission(string approvedStatus)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();

            rvd = QueryStringEncrypt.DecryptString(approvedStatus);


            if (rvd["status"].ToString() == "ApprovedStatus")
            {
                return Json("Approved");
            }
            else
            {
                return Json("UnApproved");
            }

        }
        [CustomAuthorizeAttribute(PermCd = "7")]
        public JsonResult ChangeStatus(string p)
        {
            Users obj;
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            string strUsername = "";
            string status = "";
            string returnResult = "";
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string id = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string stat = "";

            if (Session[SessionCheck.sessionName] != null)
            {
                obj = (Users)Session[SessionCheck.sessionName];
                strUsername = obj.usrName;
            }

            try
            {
                if (CommonVariables.GroupCD != "53" || CommonVariables.GroupCD != "49")
                {
                    ViewBag.UserStatus = "ValidUser";
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

                    string approve = "";

                    var result = enrollService.GetPObyId(id);
                    stat = (result.Status).ToString();

                    if (stat == "Y")
                    {
                        objPO.Status = "N";
                        approve = objPO.Status;
                        objPO.APPROVED_BY = null;
                        objPO.APPROVED_DT = null;

                    }
                    else if (stat == "N")
                    {
                        objPO.Status = "Y";
                        approve = objPO.Status;
                        objPO.APPROVED_BY = strUsername;
                        objPO.APPROVED_DT = DateTime.Now.ToString();
                        objPO.APPROVED_DT_LOC = NepaliDate.getNepaliDate(objPO.APPROVED_DT);
                    }

                    objPO.Support_CD = id.ToInt32();
                    objPO.Status = approve;
                    enrollService.ApproveIndividualPO(objPO);
                }
                else
                {
                    ViewBag.UserStatus = "InvalidUser";
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            if (stat == "N")
            {
                returnResult = "Y";
            }
            else if (stat == "Y")
            {
                returnResult = "N";
            }

            return Json(returnResult);

        }
        public ActionResult ApprovePOList()
        {
            QueryResult qr = null;
            DataTable dt01 = new DataTable();
            NameValueCollection paramValues = new NameValueCollection();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            if (CommonVariables.GroupCD != "49" && CommonVariables.GroupCD != "53")
            {
                dt01 = (DataTable)Session["dtPOList"];
                qr = enrollService.ApprovePOList(dt01);
            }
            if (qr.IsSuccess)
            {
                Session["ApprovedMessage"] = "APPROVED";
            }
            else
            {
                Session["ApprovedMessage"] = "APPROVEDFAILED";
            }

            return RedirectToAction("ListPaymentPO");
        }
        public ActionResult PartnerOrganizationReport()
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictForUser(Session["UserVdcMunDefCd"].ConvertToString());
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            string donor_cd = "";
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }

            ViewData["ddl_Support_Pkg"] = common.GetDonorPkgSupportList();
            return View();

        }
        public ActionResult GetPOUploadFiles(string donor_cd, string payroll_install_cd)
        {
            CommonFunction commonFC = new CommonFunction();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();

            DataTable dtresult = objService.GetPOUploadFiles(donor_cd, payroll_install_cd);
            ViewData["dtPOUploadFiles"] = dtresult;

            return PartialView("_ListPOUploadFiles");
        }
        public ActionResult DeleteUploadData(string p)
        {
            ReverseBankFileImportService objService = new ReverseBankFileImportService();
            QueryResult success = new QueryResult();
            string id = "";
            string fileName = "";
            string redirect = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            if (CommonVariables.GroupCD == "1")
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                            fileName = rvd["file"].ToString();
                            string path = Server.MapPath("~/Files/PartnerOrganization/" + fileName);
                            FileInfo file = new FileInfo(path);
                            if (file.Exists)//check file exsit or not
                            {
                                file.Delete();

                            }

                            success = objService.DeletePOUploadedFiles(id);
                        }
                    }
                }
                if (success.IsSuccess)
                {
                    Session["ApprovedMessage"] = "Success";
                }
                else
                {
                    Session["ApprovedMessage"] = "Failed";
                }
            }
            else
            {
                Session["ApprovedMessage"] = "WrongUser";
            }

            return RedirectToAction("BulkUploadPaymentPOSupportedBenef");
        }
        public JsonResult InsertSecondThirdInstallment(List<PaymentPartnerOrganization> packages)
        {
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            QueryResult qr = new QueryResult();
            if (packages.Count > 0)
            {
                qr = enrollService.InsertSecondThirdInstallment(packages);
            }
            if (qr.IsSuccess == true)
            {
                Session["ApprovedMessage"] = "Success";
            }
            else
            {
                Session["ApprovedMessage"] = "Failed";
            }

            return Json("Success");
        }
        public ActionResult CheckPaNum(string pa_number)
        {
            DataTable dt = new DataTable();
            ReverseBankFileImportService objService = new ReverseBankFileImportService();

            var result = objService.PARoosterPO(pa_number);
            if (result.Rows.Count > 0)
            {
                ViewData["DupPANumberDtl"] = result;
                return PartialView("~/Views/PatnerOrganization/_PARoosterPO.cshtml");
            }
            else
            {
                return Json("EMPTY");
            }

        }

        public ActionResult POFirstTrancheIssue()
        {
            string donor_cd = "";
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

            return View();
        }
        [HttpPost]
        public ActionResult POIssue(FormCollection fc)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            DataTable result = new DataTable();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();


            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = fc["ddl_Ward"].ToString();
            if (fc.AllKeys.Contains("ddl_Donor"))
            {
                objPO.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString()) ? fc["ddl_Donor"].ToString() : null);
            }
            objPO.payroll_install_cd = fc["tranche"].ToString();

            result = enrollService.GetIssueList(objPO);


            ViewData["dtIssueList"] = result;
            Session["dtIssueList"] = result;

            return PartialView("~/Views/PatnerOrganization/_ListPOIssue.cshtml");
        }

        public ActionResult POSecondTrancheIssue()
        {
            string donor_cd = "";
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

            return View();
        }
        public ActionResult POThirdTrancheIssue()
        {
            string donor_cd = "";
            EnrollmentImportExport enrollService = new EnrollmentImportExport();
            string user_cd = SessionCheck.getSessionUserCode();

            if (CommonVariables.GroupCD == "53" || CommonVariables.GroupCD == "49")
            {
                donor_cd = enrollService.GetDonorCdFromUsrCd(user_cd);
                ViewData["ddl_Donor"] = common.getDonorList(donor_cd);
            }
            else
            {
                ViewData["ddl_Donor"] = common.getDonorList("");
            }

            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");

            return View();
        }

        public ActionResult ExportExcel(string po,string dis,string vdc,string ward,string tranche)
        {
            PaymentPartnerOrganization objPO = new PaymentPartnerOrganization();
            CommonFunction commonFC = new CommonFunction();
            EnrollmentImportExport enrollService = new EnrollmentImportExport();

            objPO.Dis_Cd = commonFC.GetCodeFromDataBase(dis, "MIS_DISTRICT", "DISTRICT_CD");
            objPO.Vdc_Mun_Cd = commonFC.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objPO.Ward_Num = ward;
            objPO.Donor_CD = po;
            objPO.payroll_install_cd = tranche;

            DataTable dt = enrollService.GetIssueList(objPO);


            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= PO_Issue.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

            return RedirectToAction("PatnerOrganization", "Vulnerability");
        }
    }
}
