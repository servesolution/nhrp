using EntityFramework;
using ExceptionHandler;
using MIS.Models.Core;
using MIS.Models.Report;
using MIS.Models.Setup;
using MIS.Services.Core;
using MIS.Services.Setup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Setup
{
    public class BankMappingController : BaseController
    {
        //
        // GET: /BankMapping/
        CommonFunction common = new CommonFunction();

        public ActionResult ManageBankMapping()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

            if(Session["MapStatus"].ConvertToString() != "")
            {
                if (Session["MapStatus"].ConvertToString() == "SUCCESS")
                {
                     string count = Session["MapCount"].ConvertToString();
                     ViewData["SuccessStatus"] = "You have successfully mapped " + count + " data!!";
                }
                else
                {
                     ViewData["FailedStatus"] = "Failed!!";
                }
            }
            Session["MapStatus"] = "";

            return View();
        }
        [HttpPost]
        public ActionResult ManageBankMapping(FormCollection fc)
        {
            NHRSBankMapping mapBank = new NHRSBankMapping();
            BankMappingService objService = new BankMappingService();
            DataTable dt = new DataTable();
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

            try
            {
                if (fc != null && fc.Count > 1)
                {
                    mapBank.DISTRICT_CD = (!string.IsNullOrEmpty(fc["ddl_District"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") : null;
                    mapBank.VDC_MUN_CD = (!string.IsNullOrEmpty(fc["ddl_VDCMun"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") : null;
                    mapBank.WARD_NO = (!string.IsNullOrEmpty(fc["ddl_Ward"].ToString())) ? fc["ddl_Ward"].ToString() : null;
                    mapBank.PA_NUMBER = (!string.IsNullOrEmpty(fc["PA_NUMBER"].ToString())) ? fc["PA_NUMBER"].ToString() : null;
                    mapBank.BeneficiaryType = (!string.IsNullOrEmpty(fc["ddl_benef_type"].ToString())) ? fc["ddl_benef_type"].ToString() : null;

                   if(fc["ddl_Status"].ToString() == "1")
                   {
                       mapBank.IS_BANK_MAPPED   = "Y";
                   }
                   else if (fc["ddl_Status"].ToString() == "2")
                   {
                       mapBank.IS_BANK_MAPPED = "N";
                   }

                    dt = objService.GetBeneficiaries(mapBank);

                    ViewData["BenefList"] = dt;

                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            return PartialView("~/Views/BankMapping/_ListBeneficiaries.cshtml");
        }
        [HttpGet]
        public ActionResult BankMappingSearch()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

            return View();
        }
        [HttpPost]
        public ActionResult BankMappingSearch(FormCollection fc)
        {
            NHRSBankMapping mapBank = new NHRSBankMapping();
            BankMappingService objService = new BankMappingService();
            DataTable dt = new DataTable();
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");

            try
            {
                if (fc != null && fc.Count > 1)
                {
                    mapBank.DISTRICT_CD = (!string.IsNullOrEmpty(fc["ddl_District"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") : null;
                    mapBank.VDC_MUN_CD = (!string.IsNullOrEmpty(fc["ddl_VDCMun"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") : null;
                    mapBank.WARD_NO = (!string.IsNullOrEmpty(fc["ddl_Ward"].ToString())) ? fc["ddl_Ward"].ToString() : null;
                    mapBank.PA_NUMBER = (!string.IsNullOrEmpty(fc["PA_NUMBER"].ToString())) ? fc["PA_NUMBER"].ToString() : null;
                    mapBank.BANK_CD = (!string.IsNullOrEmpty(fc["ddl_Bank"].ToString())) ? fc["ddl_Bank"].ToString() : null;
                    mapBank.BANK_BRANCH_CD = (!string.IsNullOrEmpty(fc["ddl_BankBranch"].ToString())) ? fc["ddl_BankBranch"].ToString() : null;
                    mapBank.BeneficiaryType = (!string.IsNullOrEmpty(fc["ddl_benef_type"].ToString())) ? fc["ddl_benef_type"].ToString() : null;

                    if (fc["ddl_Status"].ToString() == "1")
                    {
                        mapBank.IS_BANK_MAPPED = "Y";
                    }
                    else if (fc["ddl_Status"].ToString() == "2")
                    {
                        mapBank.IS_BANK_MAPPED = "N";
                    }

                    dt = objService.GetBeneficiaries(mapBank);

                   

                    ViewData["MappedBenefList"] = dt;

                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            return PartialView("~/Views/BankMapping/_ListMappedBeneficiaries.cshtml"); ;
        }
        public JsonResult MapBankData(List<NHRSBankMapping> bankMap)
        {
            QueryResult qr = null;
            BankMappingService objService = new BankMappingService();
            if(bankMap != null && bankMap.Count > 0)
            {
                qr = objService.MapBankData(bankMap);
            }

            if(qr.IsSuccess == true)
            {
                Session["MapStatus"] = "SUCCESS";
                Session["MapCount"] = bankMap.Count.ToString();
                return Json("SUCCESS");
            }
            else
            {
                Session["MapStatus"] = "FAILED";
                return Json("FAILED");
            }
        }
        public ActionResult UpdateMappedBank(string data)
        {
            DataTable dt = null;
            BankMappingService objService = new BankMappingService();
            NHRSBankMapping bnkMap = new NHRSBankMapping();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string house_owner_id = "";
            string pa_number = "";
            ViewData["ddl_Bank"] = common.GetBankName("");
            ViewData["ddl_BankBranch"] = common.GetBankBranchIdByBankID("", "");


            if (data != null)
            {
                rvd = QueryStringEncrypt.DecryptString(data);
                if (rvd != null)
                {
                  pa_number = rvd["id"].ToString();
                  house_owner_id = rvd["id1"].ToString();

                  bnkMap.PA_NUMBER = pa_number;
                  dt = objService.GetMappedBeneficiaries(bnkMap);
                }
            }
            ViewData["ToUpdatePA"] = dt;

            return PartialView("~/Views/BankMapping/_UpdateMappedBank.cshtml");
        }
        public ActionResult DonorMappingSearch()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Donor"] = common.getDonorList("");
            return View();
        }
        [HttpPost]
        public ActionResult DonorMappingSearch(FormCollection fc)
        {
            NHRSBankMapping mapBank = new NHRSBankMapping();
            BankMappingService objService = new BankMappingService();
            DataTable dt = new DataTable();
          
            try
            {
                if (fc != null && fc.Count > 1)
                {
                    mapBank.DISTRICT_CD = (!string.IsNullOrEmpty(fc["ddl_District"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") : null;
                    mapBank.VDC_MUN_CD = (!string.IsNullOrEmpty(fc["ddl_VDCMun"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") : null;
                    mapBank.WARD_NO = (!string.IsNullOrEmpty(fc["ddl_Ward"].ToString())) ? fc["ddl_Ward"].ToString() : null;
                    mapBank.PA_NUMBER = (!string.IsNullOrEmpty(fc["PA_NUMBER"].ToString())) ? fc["PA_NUMBER"].ToString() : null;
                    //mapBank.Donor_CD = (!string.IsNullOrEmpty(fc["ddl_Donor"].ToString())) ? fc["ddl_Donor"].ToString() : null;
                    mapBank.BeneficiaryType = (!string.IsNullOrEmpty(fc["ddl_benef_type"].ToString())) ? fc["ddl_benef_type"].ToString() : null;

                    if (fc["ddl_Status"].ToString() == "1")
                    {
                        mapBank.IS_DONOR_MAPPED = "Y";
                    }
                    else if (fc["ddl_Status"].ToString() == "2")
                    {
                        mapBank.IS_DONOR_MAPPED = "N";
                    }

                    dt = objService.GetDonorBeneficiaries(mapBank);



                    ViewData["MappedBenefList"] = dt;

                }
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            return PartialView("~/Views/BankMapping/_ListDonorBeneficiaries.cshtml"); ;
        }

        public ActionResult ExportToExcel(string dist, string vdc, string ward, string panum, string BenefType, string status)
        {
            NHRSBankMapping mapBank = new NHRSBankMapping();
            BankMappingService objService = new BankMappingService();
            DataTable dt = new DataTable();

            try
            {
                    mapBank.DISTRICT_CD = common.GetCodeFromDataBase(dist, "MIS_DISTRICT", "DISTRICT_CD");
                    mapBank.VDC_MUN_CD = common.GetCodeFromDataBase(vdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    mapBank.WARD_NO = ward;
                    mapBank.PA_NUMBER = panum;
                    mapBank.BeneficiaryType = BenefType;

                    if (status.ToString() == "1")
                    {
                        mapBank.IS_DONOR_MAPPED = "Y";
                    }
                    else if (status.ToString() == "2")
                    {
                        mapBank.IS_DONOR_MAPPED = "N";
                    }

                    dt = objService.GetDonorBeneficiaries(mapBank);



                    Session["MappedBenefList"] = dt;
                    Session["ParamModel"] = mapBank;

                
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/Files/ClaimReport/") + "BankClaimListReport" + usercd + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/Files/ClaimReport/") + "BankClaimListReport" + usercd + "( District " + dist + " ).xls";
            }

            string html = RenderPartialClaimViewToString("~/Views/BankMapping/_ListDonorBeneficiaries.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Partner Organization Report"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "SupportOrganizationReport.xls");
        }

        protected string RenderPartialClaimViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            NHRSBankMapping rptParams = new NHRSBankMapping();
            if ((DataTable)Session["MappedBenefList"] != null)
            {
                DataTable dt = (DataTable)Session["MappedBenefList"];

                ViewData["MappedBenefList"] = dt;


                rptParams = (NHRSBankMapping)Session["ParamModel"];
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    ViewData.Model = rptParams;
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }
            }
            return null;
        }
        public ActionResult ManageDonorMapping()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByAllDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
          

            if (Session["MapStatus"].ConvertToString() != "")
            {
                if (Session["MapStatus"].ConvertToString() == "SUCCESS")
                {
                    string count = Session["MapCount"].ConvertToString();
                    ViewData["SuccessStatus"] = "You have successfully mapped " + count + " data!!";
                }
                else if (Session["MapStatus"].ConvertToString() == "DELETESUCCESS")
                {
                    ViewData["SuccessStatus"] = "Delete Successful";
                }
                else
                {
                    ViewData["FailedStatus"] = "Failed!!";
                }
            }
            Session["MapStatus"] = "";

            return View();
        }
        
        public ActionResult LoadDonorMapping(FormCollection fc)
        {
            NHRSBankMapping mapBank = new NHRSBankMapping();
            BankMappingService objService = new BankMappingService();
            DataTable dt = new DataTable();
            ViewData["ddl_Donor"] = common.getDonorList("");
            ViewData["ddl_District"] = common.GetDistricts("");

            try
            {
                if (fc != null && fc.Count > 1)
                {
                    mapBank.DISTRICT_CD = (!string.IsNullOrEmpty(fc["ddl_District"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_District"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD") : null;
                    mapBank.VDC_MUN_CD = (!string.IsNullOrEmpty(fc["ddl_VDCMun"].ToString())) ? common.GetCodeFromDataBase(fc["ddl_VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD") : null;
                    mapBank.WARD_NO = (!string.IsNullOrEmpty(fc["ddl_Ward"].ToString())) ? fc["ddl_Ward"].ToString() : null;
                    mapBank.PA_NUMBER = (!string.IsNullOrEmpty(fc["PA_NUMBER"].ToString())) ? fc["PA_NUMBER"].ToString() : null;
                    mapBank.BeneficiaryType = (!string.IsNullOrEmpty(fc["ddl_benef_type"].ToString())) ? fc["ddl_benef_type"].ToString() : null;

                    if (fc["ddl_Status"].ToString() == "1")
                    {
                        mapBank.IS_DONOR_MAPPED = "Y";
                    }
                    else if (fc["ddl_Status"].ToString() == "2")
                    {
                        mapBank.IS_DONOR_MAPPED = "N";
                    }
                }else{
                    NHRSBankMapping modelObj = (NHRSBankMapping)Session["ModelItem"];
                    mapBank.DISTRICT_CD = modelObj.DISTRICT_CD;
                    mapBank.VDC_MUN_CD =   modelObj.VDC_MUN_CD;
                    mapBank.WARD_NO =  modelObj.WARD_NO;
                    mapBank.PA_NUMBER = modelObj.PA_NUMBER;
                    mapBank.BeneficiaryType = modelObj.BeneficiaryType;
                    mapBank.IS_DONOR_MAPPED = modelObj.IS_DONOR_MAPPED;
                    ViewBag.IsSecondLoad = "Y";
                    ViewData["BenefList"] = null;
                }

                    dt = objService.GetDonorBeneficiaries(mapBank);
                    Session["ModelItem"] = mapBank;
                    ViewData["BenefList"] = dt;

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Session["ValidationMessage"] = oe;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Session["ValidationMessage"] = ex;
            }

            return PartialView("~/Views/BankMapping/_ListDonorUnMappedBenef.cshtml");
        }
        public JsonResult MapDonorData(List<NHRSBankMapping> bankMap)
        {
            QueryResult qr = null;
            BankMappingService objService = new BankMappingService();
            if (bankMap != null && bankMap.Count > 0)
            {
                qr = objService.MapDonorData(bankMap);
            }

            if (qr.IsSuccess == true)
            {
                Session["MapStatus"] = "SUCCESS";
                Session["MapCount"] = bankMap.Count.ToString();
                return Json("SUCCESS");
            }
            else
            {
                Session["MapStatus"] = "FAILED";
                return Json("FAILED");
            }
        }

        public JsonResult DeletePO(string PA_NUMBER)
        {
            QueryResult qr = null;
            BankMappingService objService = new BankMappingService();
            if (PA_NUMBER != "")
            {
                qr = objService.DeleteMappedPO(PA_NUMBER);
            }

            if (qr.IsSuccess == true)
            {
                Session["MapStatus"] = "DELETESUCCESS";
                return Json("SUCCESS");
            }
            else
            {
                Session["MapStatus"] = "DELETESUCCESS";
                return Json("FAILED");
            }
        }
        
    }
}
