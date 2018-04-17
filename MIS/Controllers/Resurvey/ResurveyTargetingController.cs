using MIS.Models.Resurvey;
using MIS.Services.Core;
using MIS.Services.ResurveyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;
using System.Data.OracleClient;
using ExceptionHandler;
using EntityFramework;

namespace MIS.Controllers.Resurvey
{
    public class ResurveyTargetingController : BaseController
    {
        //
        // GET: /ResurveyTargeting/
        CommonFunction common = new CommonFunction();
        public DataTable lastSearchResults = new DataTable();
        public ActionResult TargetResurvey(string p, string DistrictCd)
        {
            ResurveyTargetingModel objTargetingSearch = new ResurveyTargetingModel(); 
            RouteValueDictionary rvd = new RouteValueDictionary(); 
            ResurveyTargetingService objTragetingServ = new ResurveyTargetingService();
             string distCode = "";
             string batchId = string.Empty;
            string fiscalYear = string.Empty;
            string districtCd = string.Empty;
            string periodTypeCd = string.Empty;
            string periodCnt = string.Empty;
            string SATId = string.Empty;
            string SPId = String.Empty;
            string quota = "";
            
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {

                    }
                }

                Utils.setUrl(this.Url);
                if (batchId == "")
                {
                    if (string.IsNullOrEmpty(DistrictCd))
                    {
                        if (CommonVariables.UserCode != "admin")
                        {
                            distCode = CommonFunction.GetDistrictByOfficeCode(CommonVariables.EmpCode);
                        }
                    }
                    else
                    {
                        distCode = GetData.GetCodeFor(DataType.District, districtCd);
                    }
                    //get Quota for the district

                    string Quota = common.GetValueFromDataBase(distCode, "MIS_RULE_CALC", "DISTRICT_CD", "QUOTA");
                    objTargetingSearch.Quota = Quota;
                    objTargetingSearch.DistrictCd = distCode;
                    if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1" && CommonVariables.GroupCD != "34")
                    {
                        if (CommonVariables.EmpCode != "")
                        {
                            string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                            ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(Districtcode);
                            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objTargetingSearch.VDCMun, Districtcode);
                            ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictVdc(Districtcode, objTargetingSearch.VDCMun); 
                            objTargetingSearch.DistrictCd = Districtcode;
                        }
                    }
                    else
                    {
                        ViewData["ddl_District"] = common.GetDistricts(GetData.GetDefinedCodeFor(DataType.District, distCode));
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", ViewData["txtDistrict"].ConvertToString());
                        ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictVdc(ViewData["txtDistrict"].ConvertToString(), ""); 

                    }
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewData["ddlTarget"] = (List<SelectListItem>)common.GetResurveyTargetID("");
                    ViewData["txtDistrict"] = GetData.GetDefinedCodeFor(DataType.District, distCode);

                    ViewData["ddl_Targeted"] = common.GetTargetType1("");
                    ViewData["TECHSOLUTION_CD"] = common.GetTechnicalSolution("");
                    ViewData["DAMAGE_GRADE_CD"] = common.GetDamageGrade("");
                    ViewData["BUILDING_CONDITION_CD"] = common.GetBuildingCondition("");
                    lastSearchResults = new DataTable();
                    ViewData["dtForwardFeed"] = lastSearchResults;
                    ViewData["job"] = "";
                    
                }
                else
                {

                    //ViewData["dtForwardFeed"] = lastSearchResults;
                    //return View("ProcessDetail");
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
            return View(objTargetingSearch);
        }

        [HttpPost]
        public ActionResult TargetResurvey(string p, FormCollection fc, ResurveyTargetingModel objTargetingSearch)
        {
            ResurveyTargetingService objTragetingServ = new ResurveyTargetingService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string batchId = string.Empty;
            string fiscalYear = string.Empty;
            string districtCd = string.Empty;
            string periodTypeCd = string.Empty;
            string periodCnt = string.Empty;
            string SATId = string.Empty;
            string SPId = String.Empty;
            string pagerVal = string.Empty;
            string BussinessRule = String.Empty;
            string TargetCode = String.Empty;


 
             DataTable result = new DataTable();
            DataTable resultInsert = new DataTable();
            objTargetingSearch.Targeted = fc["ddl_Targeted"].ConvertToString();
            ViewData["Targeted"] = fc["ddl_Targeted"].ConvertToString();
            #region new applicant
            if (objTargetingSearch.Targeted == "A")
            {

                objTargetingSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objTargetingSearch.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
                objTargetingSearch.CurrentVdcMunCD = common.GetCodeFromDataBase(objTargetingSearch.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");

                objTargetingSearch.WardNo = fc["WardNo"].ConvertToString();
                result = objTragetingServ.GetNewApplicantSurvey(objTargetingSearch);

                ViewData["result"] = result;
                Session["dtTargetyes"] = result;

                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");

                //ViewData["ddl_District"] = common.GetDistricts("");
                //ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_Targeted"] = common.GetTargetType("");
                if (CommonVariables.GroupCD == "1" || CommonVariables.GroupCD == "34")
                {
                    objTargetingSearch.Action = true;
                }
                else
                {
                    objTargetingSearch.Action = false;
                }


                return PartialView("_ResurveyNewApplicantList", objTargetingSearch);
            }
            #endregion

            #region beneficiary
            if (objTargetingSearch.Targeted == "Y")
            {
                objTargetingSearch.Targetting_ID = fc["ddlTarget"].ConvertToString();
                objTargetingSearch.FISCAL_YR = fc["ddl_FiscalYear"].ConvertToString();
                objTargetingSearch.House_Owner_ID = fc["House_Owner_ID"].ConvertToString();
                objTargetingSearch.House_Owner_Name_Loc = fc["House_Owner_Name_Loc"].ConvertToString();
                objTargetingSearch.HOUSE_DEFINED_CD = fc["HH_SERIAL_NO"].ConvertToString();
                objTargetingSearch.INSTANCE_UNIQUE_SNO = fc["INSTANCE_UNIQUE_SNO"].ConvertToString();
                objTargetingSearch.Building_Structure_No = fc["Building_Structure_No"].ConvertToString();
                objTargetingSearch.TARGETING_DT_FROM = fc["TARGETING_DT_FROM"].ConvertToString();
                objTargetingSearch.TARGETING_DT_TO = fc["TARGETING_DT_TO"].ConvertToString();
                objTargetingSearch.DAMAGE_GRADE_CD = fc["DAMAGE_GRADE_CD"].ConvertToString();
                objTargetingSearch.BUILDING_CONDITION_CD = fc["BUILDING_CONDITION_CD"].ConvertToString();
                objTargetingSearch.TECHSOLUTION_CD = fc["TECHSOLUTION_CD"].ConvertToString();
                objTargetingSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objTargetingSearch.House_Owner_Name = fc["House_Owner_Name"].ConvertToString();
                string fullName = objTargetingSearch.House_Owner_Name;
                var names = fullName.Split(' ');

                int count1 = names.Count();
                if (names[0] != "")
                {
                    if (count1 > 2)
                    {
                        objTargetingSearch.House_Owner_Name = names[0] + " " + names[1] + " " + names[2];
                    }
                    if (count1 == 2)
                    {
                        objTargetingSearch.House_Owner_Name = names[0] + "  " + names[1];
                    }

                }

                objTargetingSearch.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
                objTargetingSearch.WardNo = fc["WardNo"].ConvertToString();
                objTargetingSearch.ddl_BusinessRule = fc["ddl_BusinessRule"].ConvertToString();
                result = objTragetingServ.GetRetrofitingTargetingSearchDetail(objTargetingSearch);
                ViewData["result"] = result;
                Session["dtEligible"] = result;
                ViewData["ddl_District"] = common.GetDistricts("");

                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");


                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_BusinessRule"] = common.GetBusinessRule("");
                //ViewData["txtBusinessRule"] = common.GetBusinessRule(distCode);
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

                string fisc = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));
                ViewData["ddl_FiscalYear"] = common.GetFiscalYear(fisc);
                ViewBag.EducationCd = common.GetEducation("");

                ViewData["ddl_Targeted"] = common.GetTargetType("");
                if (CommonVariables.GroupCD == "1" || CommonVariables.GroupCD == "34")
                {
                    objTargetingSearch.Action = true;
                }
                else
                {
                    objTargetingSearch.Action = false;
                }
                return PartialView("_ResurveyNewApplicantList.cshtml", objTargetingSearch);
            }
            #endregion

            #region non beneficiary
            if (objTargetingSearch.Targeted == "N")
            {

                string session_id = String.Empty;

                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        session_id = rvd["session"].ConvertToString();

                    }
                }
                objTargetingSearch.Targetting_ID = fc["ddlTarget"].ConvertToString();

                objTargetingSearch.House_Owner_ID = fc["House_Owner_ID"].ConvertToString();
                objTargetingSearch.House_Owner_Name = fc["House_Owner_Name"].ConvertToString();
                objTargetingSearch.House_Owner_Name_Loc = fc["House_Owner_Name_Loc"].ConvertToString();
                objTargetingSearch.HOUSE_DEFINED_CD = fc["HOUSE_DEFINED_CD"].ConvertToString();
                objTargetingSearch.INSTANCE_UNIQUE_SNO = fc["INSTANCE_UNIQUE_SNO"].ConvertToString();
                objTargetingSearch.Building_Structure_No = fc["Building_Structure_No"].ConvertToString();
                objTargetingSearch.TARGETING_DT_FROM = fc["TARGETING_DT_FROM"].ConvertToString();
                objTargetingSearch.TARGETING_DT_TO = fc["TARGETING_DT_TO"].ConvertToString();
                objTargetingSearch.DAMAGE_GRADE_CD = fc["DAMAGE_GRADE_CD"].ConvertToString();
                objTargetingSearch.BUILDING_CONDITION_CD = fc["BUILDING_CONDITION_CD"].ConvertToString();
                objTargetingSearch.TECHSOLUTION_CD = fc["TECHSOLUTION_CD"].ConvertToString();
                objTargetingSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());

                objTargetingSearch.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
                objTargetingSearch.WardNo = fc["WardNo"].ConvertToString();
                objTargetingSearch.ddl_BusinessRule = fc["ddl_BusinessRule"].ConvertToString();
                //result = objTarService.GetRetrofitingNONTargetingSearchDetail(objTargetingSearch);
                // objTarService.TargetingEligible(result);

                ViewData["result"] = result;
                Session["dtHouseHold"] = result;
                ViewData["ddl_District"] = common.GetDistricts("");

                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");


                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_BusinessRule"] = common.GetBusinessRule("");
                //ViewData["txtBusinessRule"] = common.GetBusinessRule(distCode);
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                string fisc = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));

                ViewBag.EducationCd = common.GetEducation("");

                ViewData["ddl_Targeted"] = common.GetTargetType("");
                //PR_ELIGIBLE_EDU_TARGETING
                Session["EligibleTargetingParam"] = objTargetingSearch;
                if (CommonVariables.GroupCD == "1" || CommonVariables.GroupCD == "34")
                {
                    objTargetingSearch.Action = true;
                }
                else
                {
                    objTargetingSearch.Action = false;
                }


                return PartialView("~/Views/ProvertyTrageting/_RetrofitingTargeting.cshtml", objTargetingSearch);
            }
            #endregion



            ViewData["ddl_District"] = common.GetDistricts("");

            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_BusinessRule"] = common.GetBusinessRule("");
            //ViewData["txtBusinessRule"] = common.GetBusinessRule(distCode);
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            string fisc2 = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));
            ViewData["ddl_FiscalYear"] = common.GetFiscalYear(fisc2);
            ViewBag.EducationCd = common.GetEducation("");

            ViewData["ddl_Targeted"] = common.GetTargetType1("");
            //PR_ELIGIBLE_EDU_TARGETING

            return PartialView("~/Views/ProvertyTrageting/_Targeting.cshtml", objTargetingSearch);
           
        }


        public ActionResult TargetingEligible(string p, FormCollection fc)
        {

            ResurveyTargetingModel objTargetingSearch = new ResurveyTargetingModel();
             ResurveyTargetingService objTragetingServ = new ResurveyTargetingService();

            RouteValueDictionary rvd = new RouteValueDictionary();
            string session_id = String.Empty;

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    session_id = rvd["session"].ConvertToString();

                }
            }
            QueryResult success = new QueryResult();
            string TotalTargeted = string.Empty;
            string excout = string.Empty;
            success = objTragetingServ.TargetingSearchEligible(session_id, out excout, out TotalTargeted);

            if (success.IsSuccess)
            {
                TempData["Message"] = "You have Successfully Targeted " + TotalTargeted + " Beneficiary";
            }
            if (excout == "20099")
            {
                TempData["ErrMessage"] = "Eligible Beneficiary not found for Targeting.";
            }
            return RedirectToAction("TargetResurvey");
        }




        public ActionResult BeneficiaryList()
        {

            string distCode = "", distDefCode = "";
            BeneficiarySearch objSearch = new BeneficiarySearch();
           
            if (CommonVariables.GroupCD != "34" && CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
            {
                if (CommonVariables.EmpCode != "")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(Districtcode);
                    ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objSearch.VDCMunCd, Districtcode);
                    ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictVdc(Districtcode, objSearch.VDCMunCd); 
                    objSearch.DistrictCd = Districtcode;
                }
            }
            else
            {
                ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                ViewData["ddl_currentVDCMun"] = common.GetNewVDCMunByDistrictVdc("", ""); 
            }
            distDefCode = GetData.GetDefinedCodeFor(DataType.District, distCode);

            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_target_lot"] = common.GetResurveyTargetLot("", "");
            ViewData["ddlApprove"] = common.GetApprove("");
 

            return View(objSearch);
        }
        [HttpPost]
        public ActionResult BeneficiaryList(FormCollection fc, BeneficiarySearch objSearch)
        {
            objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            ResurveyTargetingService objTragetingServ = new ResurveyTargetingService();

            DataTable searchResult = new DataTable();
            DataTable searchReport = new DataTable();
            ViewData["ddl_District"] = common.GetDistricts("");
            //objSearch.
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ApproveStatus"] = fc["ddlApprove"].ConvertToString();
            if (CommonVariables.GroupCD == "1" || CommonVariables.GroupCD == "33")
            {

                objSearch.Action = true;
            }
            else
            {
                objSearch.Action = false;
            }

            if (objSearch.ddlApprove == "Y")
            {
                objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objSearch.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                objSearch.CurrentVdcMunCD = common.GetCodeFromDataBase(objSearch.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");

                ViewData["BatchID"] = common.GetBatchId("", "");
                ViewData["ddlApprove"] = common.GetApprove(""); 
                objSearch.BatchID = fc["target_lot"].ConvertToString();
                objSearch.ddlApprove = fc["ddlApprove"].ConvertToString();
                Session["Param"] = objSearch;
                DataTable dtEnroll = new DataTable();
                searchResult = objTragetingServ.GetBeneficiariesDeatilsByDate(objSearch);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
                
            }
            if (objSearch.ddlApprove == "N")
            {
                objSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objSearch.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                objSearch.CurrentVdcMunCD = common.GetCodeFromDataBase(objSearch.CurrentVdcMunDefCD.ConvertToString(), "NHRS_NMUNICIPALITY", "NMUNICIPALITY_CD");
                ViewData["BatchID"] = common.GetBatchId("", "");
                ViewData["ddlApprove"] = common.GetApprove(""); 
                objSearch.BatchID = fc["target_lot"].ConvertToString();
                searchResult = objTragetingServ.GetBeneficiariesDeatilsByDate(objSearch);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
            }

            return PartialView("~/Views/ResurveyTargeting/_ResurveyBeneficiaryView.cshtml", objSearch);
        }



        public ActionResult BeneficiaryApproved(string p, FormCollection fc)
        {
            ResurveyTargetingService objTragetingServ = new ResurveyTargetingService();
            BeneficiarySearch objTargetingSearch = new BeneficiarySearch();
            QueryResult success = new QueryResult();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string Targeting_batch_id = String.Empty;
            string HouseOwnerID = String.Empty;
            string id = string.Empty;

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    Targeting_batch_id = rvd["Targeting_id"].ConvertToString();
                    id = Request.QueryString["houseID"].ConvertToString();
                    HouseOwnerID = id.Split(',')[0];
                    if (HouseOwnerID == "chkall")
                    {
                        success = objTragetingServ.BeneficiaryApprovedAll(Targeting_batch_id, HouseOwnerID);
                    }
                    else
                    {
                        for (int i = 0; i < id.Split(',').Length; i++)
                        {
                            success = objTragetingServ.BeneficiaryApproved(Targeting_batch_id, id.Split(',')[i]);
                        }
                       
                    }

                }
            }


            if (success.IsSuccess)
            {
                TempData["Message"] = "Approved Successfully.";
            }
            return RedirectToAction("BeneficiaryList");
        }

    }
}
