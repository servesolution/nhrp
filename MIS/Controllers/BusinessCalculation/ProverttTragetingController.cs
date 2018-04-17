using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Data;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Models.Core;
using MIS.Services.BusinessCalculation;
using MIS.Models.BusinessCalcualtion;
using MIS.Models.NHRP.View;
using System.Web.Routing;
using MIS.Authentication;
using EntityFramework;

namespace MIS.Controllers
{
    public class ProvertyTragetingController : BaseController
    {


        CommonFunction common = new CommonFunction();
        public DataTable lastSearchResults = new DataTable();

        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult Trageting(string p, string DistrictCd)
        {
            TargetingSearch objTargetingSearch = new TargetingSearch();
            string distCode = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string batchId = string.Empty;
            string fiscalYear = string.Empty;
            string districtCd = string.Empty;
            string periodTypeCd = string.Empty;
            string periodCnt = string.Empty;
            string SATId = string.Empty;
            string SPId = String.Empty;
            TargetingServices objTrageting = new TargetingServices();
            PovertyScoringService Comservice = new PovertyScoringService();
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
                                objTargetingSearch.DistrictCd = Districtcode;
                            }
                        }
                        else
                        {
                            ViewData["ddl_District"] = common.GetDistricts(GetData.GetDefinedCodeFor(DataType.District, distCode));
                            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", ViewData["txtDistrict"].ConvertToString());
                        }
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                        ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");
                        ViewData["txtDistrict"] = GetData.GetDefinedCodeFor(DataType.District, distCode);
                         
                        //ViewData["txtBusinessRule"] = Comservice.GetRuleCalc(distCode);
                        //ViewData["ddl_SP"] = common.GetServiceProvider("");
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

                    ViewData["dtForwardFeed"] = lastSearchResults;
                    return View("ProcessDetail");
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

        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        public ActionResult RetrofitingTrageting(string p, string DistrictCd)
        {
            TargetingSearch objTargetingSearch = new TargetingSearch();
            string distCode = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string batchId = string.Empty;
            string fiscalYear = string.Empty;
            string districtCd = string.Empty;
            string periodTypeCd = string.Empty;
            string periodCnt = string.Empty;
            string SATId = string.Empty;
            string SPId = String.Empty;
            string quota = "";
            TargetingServices objTrageting = new TargetingServices();
            PovertyScoringService Comservice = new PovertyScoringService();
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
                            objTargetingSearch.DistrictCd = Districtcode;
                        }
                    }
                    else
                    {
                        ViewData["ddl_District"] = common.GetDistricts(GetData.GetDefinedCodeFor(DataType.District, distCode));
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", ViewData["txtDistrict"].ConvertToString());
                    }
                    ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                    ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");
                    ViewData["txtDistrict"] = GetData.GetDefinedCodeFor(DataType.District, distCode);

                    //ViewData["txtBusinessRule"] = Comservice.GetRuleCalc(distCode);
                    //ViewData["ddl_SP"] = common.GetServiceProvider("");
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

                    ViewData["dtForwardFeed"] = lastSearchResults;
                    return View("ProcessDetail");
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
        public ActionResult RetrofitingTrageting(string p, FormCollection fc, TargetingSearch objTargetingSearch)
        {
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
            objTargetingSearch = new TargetingSearch();
            TargetingServices objTarService = new TargetingServices();
            DataTable result = new DataTable();
            DataTable resultInsert = new DataTable();
            objTargetingSearch.Targeted = fc["ddl_Targeted"].ConvertToString();
            ViewData["Targeted"] = fc["ddl_Targeted"].ConvertToString();
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
                result = objTarService.GetRetrofitingTargetingSearchDetail(objTargetingSearch);
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

                //ViewData["ddl_SP"] = common.GetServiceProvider("");
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
                return PartialView("~/Views/ProvertyTrageting/_RetrofitingTargeting.cshtml", objTargetingSearch);
            }
            if (objTargetingSearch.Targeted == "A")
            {

                objTargetingSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objTargetingSearch.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
                objTargetingSearch.WardNo = fc["WardNo"].ConvertToString();
                result = objTarService.GetRetrofitingNewApplicantSearchDetail(objTargetingSearch);
                //foreach (DataRow dr in result.Rows)
                //{
                //    objTargetingSearch.SESSION_ID = dr["SESSION_ID"].ConvertToString();
                //}

                //  objTarService.TargetingEligible(result);
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


                return PartialView("~/Views/ProvertyTrageting/_RetrofitingTargeting.cshtml", objTargetingSearch);
            }
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
                result = objTarService.GetRetrofitingNONTargetingSearchDetail(objTargetingSearch);
                //objTarService.TargetingEligible(session_id, result);
                objTarService.TargetingEligible(result);

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

                //ViewData["ddl_SP"] = common.GetServiceProvider("");
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


            ViewData["ddl_District"] = common.GetDistricts("");

            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_BusinessRule"] = common.GetBusinessRule("");
            //ViewData["txtBusinessRule"] = common.GetBusinessRule(distCode);
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

            //ViewData["ddl_SP"] = common.GetServiceProvider("");
            string fisc2 = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));
            ViewData["ddl_FiscalYear"] = common.GetFiscalYear(fisc2);
            ViewBag.EducationCd = common.GetEducation("");

            ViewData["ddl_Targeted"] = common.GetTargetType1("");
            //PR_ELIGIBLE_EDU_TARGETING

            return PartialView("~/Views/ProvertyTrageting/_RetrofitingTargeting.cshtml", objTargetingSearch);
            //return View();
        }


        [HttpPost]
        public ActionResult Trageting(string p, FormCollection fc, TargetingSearch objTargetingSearch)
        {


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


            objTargetingSearch = new TargetingSearch();

            TargetingServices objTarService = new TargetingServices();
            DataTable result = new DataTable();
            DataTable resultInsert = new DataTable();
            objTargetingSearch.Targeted = fc["ddl_Targeted"].ConvertToString();
            ViewData["Targeted"] = fc["ddl_Targeted"].ConvertToString();
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
                    if (count1 ==2)
                    {
                        objTargetingSearch.House_Owner_Name = names[0] + "  " + names[1];
                    }

                }

                objTargetingSearch.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
                objTargetingSearch.WardNo = fc["WardNo"].ConvertToString();
                objTargetingSearch.ddl_BusinessRule = fc["ddl_BusinessRule"].ConvertToString();
                result = objTarService.GetTargetingSearchDetail(objTargetingSearch);
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

                //ViewData["ddl_SP"] = common.GetServiceProvider("");
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
                return PartialView("~/Views/ProvertyTrageting/_Targeting.cshtml", objTargetingSearch);
            }
            if (objTargetingSearch.Targeted == "A")
            {

                objTargetingSearch.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                objTargetingSearch.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
                objTargetingSearch.WardNo = fc["WardNo"].ConvertToString();
                result = objTarService.GetNewApplicantSearchDetail(objTargetingSearch);
                //foreach (DataRow dr in result.Rows)
                //{
                //    objTargetingSearch.SESSION_ID = dr["SESSION_ID"].ConvertToString();
                //}

                //  objTarService.TargetingEligible(result);
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


                return PartialView("~/Views/ProvertyTrageting/_Targeting.cshtml", objTargetingSearch);
            }
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
                result = objTarService.GetNONTargetingSearchDetail(objTargetingSearch);
                //objTarService.TargetingEligible(session_id, result);
                objTarService.TargetingEligible(result);

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

                //ViewData["ddl_SP"] = common.GetServiceProvider("");
                string fisc = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));

                ViewBag.EducationCd = common.GetEducation("");

                ViewData["ddl_Targeted"] = common.GetTargetType("");
                //PR_ELIGIBLE_EDU_TARGETING
                Session["EligibleTargetingParam"] = objTargetingSearch;
                 objTargetingSearch.Action = true;
                return PartialView("~/Views/ProvertyTrageting/_Targeting.cshtml",objTargetingSearch);
            }


            ViewData["ddl_District"] = common.GetDistricts("");

            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
            ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");


            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_BusinessRule"] = common.GetBusinessRule("");
            //ViewData["txtBusinessRule"] = common.GetBusinessRule(distCode);
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

            //ViewData["ddl_SP"] = common.GetServiceProvider("");
            string fisc2 = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));
            ViewData["ddl_FiscalYear"] = common.GetFiscalYear(fisc2);
            ViewBag.EducationCd = common.GetEducation("");

            ViewData["ddl_Targeted"] = common.GetTargetType1("");
            //PR_ELIGIBLE_EDU_TARGETING

            return PartialView("~/Views/ProvertyTrageting/_Targeting.cshtml", objTargetingSearch);
            //return View();
        }

        public ActionResult RectofitingTargetingEligible(string p, FormCollection fc)
        {
            TargetingServices objTargetService = new TargetingServices();
            TargetingSearch objTargetingSearch = new TargetingSearch();

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
            success = objTargetService.RectofitingTargetingSearchEligible(session_id, out excout, out TotalTargeted);

            if (success.IsSuccess)
            {
                TempData["Message"] = "You have Successfully Targeted " + TotalTargeted + " Beneficiary";
            }
            if (excout == "20099")
            {
                TempData["ErrMessage"] = "Eligible Beneficiary not found for Targeting.";
            }
            return RedirectToAction("RetrofitingTrageting");
        }
        public ActionResult TargetingEligible(string p, FormCollection fc)
        {
            TargetingServices objTargetService = new TargetingServices();
            TargetingSearch objTargetingSearch = new TargetingSearch();

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
            success = objTargetService.TargetingSearchEligible(session_id, out excout, out TotalTargeted);

            if (success.IsSuccess)
            {
                TempData["Message"] = "You have Successfully Targeted " + TotalTargeted + " Beneficiary";
            }
            if (excout == "20099")
            {
                TempData["ErrMessage"] = "Eligible Beneficiary not found for Targeting.";
            }
            return RedirectToAction("Trageting");
        }
        [HttpPost]
        public JsonResult GetRuleCalcData(FormCollection fc) //string districtDefCd)
        {
            string districdCd = string.Empty, rule = string.Empty, quota = string.Empty;

            if (fc != null && fc["districtDefCd"] != null)
            {
                districdCd = GetData.GetCodeFor(DataType.District, fc["districtDefCd"]);
                TargetingServices objService = new TargetingServices();

                if (!string.IsNullOrWhiteSpace(districdCd))
                    objService.GetRuleCalcData(districdCd, out rule, out quota);

            }
            var result = new { Rule = rule, Quota = quota };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult StructureDetails(TargetingSearch objTargetSearch, string p)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();
            HouseOwnerNameModel objHownerDtl = new HouseOwnerNameModel();

            DataTable dt = new DataTable();
            TargetingServices objBusCal = new TargetingServices();
            string id = string.Empty;
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }

                    }
                }

                DataSet result = new DataSet();
                result = objBusCal.BuildingStructuredDetail(id);
                #region otherHouse
                if (result.Tables[0] != null)
                {
                    List<OtherHousesDamagedModel> OthHouseDtlList = new List<OtherHousesDamagedModel>();
                    int othHouseIncrement = 0;
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        OtherHousesDamagedModel objOthHouse = new OtherHousesDamagedModel();
                        objOthHouse.OTHER_RESIDENCE_ID = dr["OTHER_RESIDENCE_ID"].ConvertToString();
                        objOthHouse.BUILDING_CONDITION_CD = dr["BUILDING_CONDITION_CD"].ConvertToString();
                        objOthHouse.BUILDING_CONDITION_ENG = common.GetDescriptionFromCode(dr["BUILDING_CONDITION_CD"].ConvertToString(), "NHRS_BUILDING_CONDITION", "BUILDING_CONDITION_CD");
                        objOthHouse.BUILDING_CONDITION_LOC = common.GetDescriptionFromCode(dr["BUILDING_CONDITION_CD"].ConvertToString(), "NHRS_BUILDING_CONDITION", "BUILDING_CONDITION_ENG");
                        othHouseIncrement++;
                        OthHouseDtlList.Add(objOthHouse);


                    }
                    objTargetSearch.OtherHouseList = OthHouseDtlList;


                }
                #endregion
                #region owner
                if (result.Tables[1] != null)
                {
                    List<HouseOwnerNameModel> HouseOwnerDetailsList = new List<HouseOwnerNameModel>();
                    int HouseOwnDtlIncrement = 0;
                    foreach (DataRow dr in result.Tables[1].Rows)
                    {
                        HouseOwnerNameModel HownDtl = new HouseOwnerNameModel();
                        HownDtl.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();

                        HownDtl.FIRST_NAME_ENG = dr["FIRST_NAME_ENG"].ConvertToString();
                        HownDtl.FIRST_NAME_LOC = dr["FIRST_NAME_LOC"].ConvertToString();
                        HownDtl.MIDDLE_NAME_ENG = dr["MIDDLE_NAME_ENG"].ConvertToString();
                        HownDtl.MIDDLE_NAME_LOC = dr["MIDDLE_NAME_LOC"].ConvertToString();
                        HownDtl.LAST_NAME_ENG = dr["LAST_NAME_ENG"].ConvertToString();
                        HownDtl.LAST_NAME_LOC = dr["LAST_NAME_LOC"].ConvertToString();
                        HownDtl.GENDER_CD = dr["GENDER_CD"].ConvertToString();
                        HownDtl.GENDER_ENG = common.GetDescriptionFromCode(dr["GENDER_CD"].ConvertToString(), "MIS_GENDER", "GENDER_CD");
                        HownDtl.GENDER_LOC = common.GetDescriptionFromCode(dr["GENDER_CD"].ConvertToString(), "MIS_GENDER", "GENDER_CD");

                        HouseOwnDtlIncrement++;
                        HouseOwnerDetailsList.Add(HownDtl);

                    }
                    objTargetSearch.HouseOwnerDetailsList = HouseOwnerDetailsList;



                }
                #endregion
                #region structure
                if (result.Tables[2] != null)
                {
                    //List<HouseOwnerNameModel> HouseOwnerDetailsList = new List<HouseOwnerNameModel>();
                    int StructureDtlIncrement = 0;
                    List<VW_HOUSE_BUILDING_DTL> BuldingStrDtlList = new List<VW_HOUSE_BUILDING_DTL>();
                    foreach (DataRow dr in result.Tables[2].Rows)
                    {

                        VW_HOUSE_BUILDING_DTL objDmgExtntHme = new VW_HOUSE_BUILDING_DTL();
                        objDmgExtntHme.BUILDING_STRUCTURE_NO = dr["BUILDING_STRUCTURE_NO"].ConvertToString();

                        objDmgExtntHme.DAMAGE_GRADE_ENG = dr["DAMAGE_GRADE_ENG"].ConvertToString();
                        objDmgExtntHme.DAMAGE_GRADE_LOC = dr["DAMAGE_GRADE_LOC"].ConvertToString();

                        objDmgExtntHme.TECHSOLUTION_ENG = dr["TECHSOLUTION_ENG"].ConvertToString();
                        objDmgExtntHme.TECHSOLUTION_LOC = dr["TECHSOLUTION_LOC"].ConvertToString();



                        StructureDtlIncrement++;
                        BuldingStrDtlList.Add(objDmgExtntHme);
                    }
                    objTargetSearch.BulStrDetlList = BuldingStrDtlList;
                }
                #endregion
            }

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            ViewData["result"] = dt;
            return View("~/Views/ProvertyTrageting/TargetedView.cshtml", objTargetSearch);
        }

        #region Export To Excel
        public ActionResult ExportExcel(string searchType)
        {
            DataTable dt = new DataTable();
            if (searchType == "NewApplicant")
            {
                dt = (DataTable)Session["dtTargetyes"];
            }
            if (searchType == "Eligible")
            {
                dt = (DataTable)Session["dtEligible"];
            }
            if (searchType == "NonEligibile")
            {
                dt = (DataTable)Session["dtHouseHold"];
            }
            //dt =  result;
            dt = ColumnsPreparation(dt, searchType);
            dt = SetOrdinals(dt, searchType);
            string filepath = Server.MapPath("~/Excel/xportExl.xls");
            //Utils.ExportToExcel2(dt, filepath);
            ExcelExport(dt);
            return File(filepath, "application/xlsx", "xportExl.xls");
        }

        public DataTable ColumnsPreparation(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();
            List<string> requiredCols = new List<string>();

            requiredCols.Add("HH_SERIAL_NO");
            requiredCols.Add("DISTRICT_ENG");
            requiredCols.Add("VDC_ENG");
            requiredCols.Add("WARD_NO");
            requiredCols.Add("HOUSE_OWNER_NAME_ENG");
            requiredCols.Add("TOTAL_OWNER_CNT");
            requiredCols.Add("TOTAL_OTHER_HOUSE_CNT");
            requiredCols.Add("TOTAL_BUILDING_STUCTURE_CNT");



            foreach (DataColumn dtCol in unFomattedDt.Columns)
            {
                if (!requiredCols.Contains(dtCol.ColumnName))
                {
                    dtModified.Columns.Remove(dtCol.ColumnName);
                }
            }
            return dtModified;
        }

        public DataTable SetOrdinals(DataTable unFomattedDt, string searchType)
        {
            DataTable dtModified = new DataTable();
            dtModified = unFomattedDt.Copy();

            dtModified.Columns["HH_SERIAL_NO"].SetOrdinal(0);
            dtModified.Columns["DISTRICT_ENG"].SetOrdinal(1);
            dtModified.Columns["VDC_ENG"].SetOrdinal(2);
            dtModified.Columns["WARD_NO"].SetOrdinal(3);
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(4);
            dtModified.Columns["TOTAL_OWNER_CNT"].SetOrdinal(5);
            dtModified.Columns["TOTAL_BUILDING_STUCTURE_CNT"].SetOrdinal(6);
            dtModified.Columns["TOTAL_OTHER_HOUSE_CNT"].SetOrdinal(7);
            dtModified.Columns["HH_SERIAL_NO"].ColumnName = Utils.GetLabel("House ID");
            dtModified.Columns["DISTRICT_ENG"].ColumnName = Utils.GetLabel("District");
            dtModified.Columns["VDC_ENG"].ColumnName = Utils.GetLabel("VDC");
            dtModified.Columns["WARD_NO"].ColumnName = Utils.GetLabel("Ward No");
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].ColumnName = Utils.GetLabel("Owner Name");
            dtModified.Columns["TOTAL_OWNER_CNT"].ColumnName = Utils.GetLabel("Total Owner Count");
            dtModified.Columns["TOTAL_BUILDING_STUCTURE_CNT"].ColumnName = Utils.GetLabel("Structure Count");
            dtModified.Columns["TOTAL_OTHER_HOUSE_CNT"].ColumnName = Utils.GetLabel("Other House Count");


            return dtModified;
        }

        protected void ExcelExport(DataTable dtRecords)
        {
            string XlsPath = Server.MapPath(@"~/Excel/TargetingReport.xls");
            string attachment = string.Empty;
            if (XlsPath.IndexOf("\\") != -1)
            {
                string[] strFileName = XlsPath.Split(new char[] { '\\' });
                attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
            }
            else
                attachment = "attachment; filename=" + XlsPath;
            try
            {
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = string.Empty;
                //Added S No for export to excel
                Response.Write(tab + Utils.GetLabel("S NO."));
                tab = "\t";
                //
                foreach (DataColumn datacol in dtRecords.Columns)
                {

                    Response.Write(tab + datacol.ColumnName);
                    tab = "\t";

                }
                Response.Write("\n");

                var rowCount = 0;
                foreach (DataRow dr in dtRecords.Rows)
                {
                    tab = "";
                    //Added Sno for export to excel
                    rowCount++;

                    Response.Write(tab + Utils.GetLabel(Convert.ToString(rowCount)));
                    tab = "\t";
                    //
                    for (int j = 0; j < dtRecords.Columns.Count; j++)
                    {
                        Response.Write(tab + Utils.GetLabel(Convert.ToString(dr[j])));
                        tab = "\t";

                    }

                    Response.Write("\n");
                }
                Response.End();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
            }
        }
        #endregion
    }
}
