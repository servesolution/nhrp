using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{
    public class AdhocReportController : BaseController
    {
        //
        // GET: /AdhocReport/
        CommonFunction cm = new CommonFunction(); 

        public ActionResult HouseOwnerReport()
        {
            AdhocHouseOwnerReportModel objModel = new AdhocHouseOwnerReportModel();
            ViewData["ddl_District"] = cm.GetDistricts("");
            ViewData["ddl_VDCMun"] = cm.GetVDCMunByDistrict("","");
            ViewData["ddl_Ward"] = cm.GetWardByVDCMun("","");
            ViewData["ddl_Indicator"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_Gender"] = cm.GetGender("");
            ViewData["ddl_yesno"] = (List<SelectListItem>)cm.GetYesNoSearch("").Data;
            ViewData["ddl_yesno1"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            ViewData["ddl_benf_typ"] = (List<SelectListItem>)cm.GetBenfType("").Data;
            ViewData["ddlHouseOwnerTable"] = cm.GetHouseownerTable("");
            //Structure
            ViewData["ddl_legalOwner"] = cm.LegalOwner("");
            ViewData["ddl_buildingCondition"] = cm.GetBuildingCondition("");
            ViewData["ddl_foundationType"] = cm.getFoundationType("");
            //ViewData["ddl_roofMaterial"] = cm.GetRoofType("");
            ViewData["ddl_storeyBeforeEQ"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_StoreyAfterEq"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_floorMaterial"] = cm.GetFloorMaterial("");
            ViewData["ddl_buildingMaterial"] = cm.getBuildingMaterial("");
            ViewData["ddl_buildingPosition"] = cm.getBuildingPosition("");
            ViewData["ddl_planConfig"] = cm.getPlanConfiguration("");
            ViewData["ddl_yesnoGeographicRisk"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_inspectedpart"] = cm.GetInspectedHousepart("");
            ViewData["ddl_damageGrade"] = cm.GetDamageGrade("");
            ViewData["ddl_technicalSolution"] = cm.GetTechnicalSolution("");
            ViewData["ddl_reConstrctStrt"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_otherUse"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_Indicator"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            //Household
            ViewData["ddl_gender"] = cm.GetGender("");
            ViewData["ddl_IndicatorAge"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_isOwner"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_relation"] = cm.GetRelation("");
            ViewData["ddl_identificationType"] = cm.GetDocumentType("");
            ViewData["ddl_dist"] = cm.GetDistrictsForUser(Session["UserDistrictDefCd"].ConvertToString());
            ViewData["ddl_ethnicity"] = cm.getEthnicity("");
            ViewData["ddl_education"] = cm.GetEducation("");
            ViewData["ddl_account"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            ViewData["ddl_shelterAEQ"] = cm.ShelterAfterEq("");
            ViewData["ddl_shelterBEQ"] = cm.ShelterBeforeEq("");
            ViewData["ddl_currentPlace"] = cm.ShelterBeforeEq("");
            ViewData["ddl_EqCard"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            ViewData["ddl_monthlyIncome"] = cm.GetMonthlyIncome("");
            ViewData["ddl_phone"] = (List<SelectListItem>)cm.GetYesNo1("").Data;



            return View();
        }
        [HttpPost]
        public ActionResult HouseOwnerReport(FormCollection fc)
        {
            AdhocHouseOwnerReportModel objModel = new AdhocHouseOwnerReportModel();
            AdhocReportSummaryService objService = new AdhocReportSummaryService(); 
            objModel.Districtcd = fc["ddl_District"].ConvertToString();
            objModel.VDCMun = fc["ddl_VDCMun"].ConvertToString();
            objModel.Ward = fc["ddl_Ward"].ConvertToString();
            objModel.ownercount_Indicator = fc["ddl_IndicatorOwnerCount"].ConvertToString();
            objModel.ownercount = fc["ownerCount"].ConvertToString();
            //objModel.gender = fc["ddl_Gender"].ConvertToString();
            //objModel.isrespondend = fc["ddl_yesno"].ConvertToString();
            //objModel.housewithinEC_Inidcator = fc["ddl_IndicatorHouse"].ConvertToString();
            //objModel.housewithinEC = fc["numEC"].ConvertToString();
            //objModel.houseoutofEC_Indicator = fc["ddl_indi_oth_ec"].ConvertToString();
            //objModel.houseoutofEC = fc["numAnotherEc"].ConvertToString();
            //objModel.phonenumberflag = fc["ddl_yesno1"].ConvertToString();
            //objModel.grievanceRegisteredFlag = fc["GrievanceRegisteredFlag"].ConvertToString();
            //objModel.BeneficiaryFlaG = fc["BeneficiaryFlag"].ConvertToString();
            //objModel.BeneficiaryType = fc["ddl_benf_typ"].ConvertToString();  
       

            DataTable dt = objService.GetOwnerAdhocReport(objModel);

            return View();
        }
        public ActionResult HouseStructureReport()
        {
            //AdhocHouseOwnerReport objModel = new AdhocHouseOwnerReport();
            ViewData["ddl_District"] = cm.GetDistricts("");
            ViewData["ddl_VDCMun"] = cm.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = cm.GetWardByVDCMun("", "");
            ViewData["ddl_legalOwner"] = cm.LegalOwner("");
            ViewData["ddl_buildingCondition"] = cm.GetBuildingCondition("");
            ViewData["ddl_foundationType"] = cm.getFoundationType("");
            ViewData["ddl_roofMaterial"] = cm.GetRoofType("");
            ViewData["ddl_storeyBeforeEQ"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_StoreyAfterEq"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_floorMaterial"] = cm.GetFloorMaterial("");
            ViewData["ddl_buildingMaterial"] = cm.getBuildingMaterial("");
            ViewData["ddl_buildingPosition"] = cm.getBuildingPosition("");
            ViewData["ddl_planConfig"] = cm.getPlanConfiguration("");
            ViewData["ddl_yesnoGeographicRisk"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_inspectedpart"] = cm.GetInspectedHousepart("");
            ViewData["ddl_damageGrade"] = cm.GetDamageGrade("");
            ViewData["ddl_technicalSolution"] = cm.GetTechnicalSolution("");
            ViewData["ddl_reConstrctStrt"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_otherUse"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_Indicator"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            return View();
        }
        [HttpPost]
        public ActionResult HouseStructureReport(FormCollection fc)
        {
            AdhocHouseStructureReport objModel = new AdhocHouseStructureReport();
            AdhocReportSummaryService objService = new AdhocReportSummaryService();
            objModel.Districtcd = fc["ddl_District"].ConvertToString();
            objModel.VDCMun = fc["ddl_VDCMun"].ConvertToString();
            objModel.Ward = fc["ddl_Ward"].ConvertToString();
            objModel.LeagelOwner = fc["ddl_legalOwner"].ConvertToString();
                        objModel.BuildingCondition = fc["ddl_buildingCondition"].ConvertToString();
                        objModel.FoundationType = fc["ddl_foundationType"].ConvertToString();
                        objModel.RoofMaterial = fc["ddl_roofMaterial"].ConvertToString();
                        objModel.StoreyBeforEQ = fc["ddl_storeyBeforeEQ"].ConvertToString();
                        objModel.StoreyAfterEQ = fc["ddl_StoreyAfterEq"].ConvertToString();
                        objModel.FloorMaterial = fc["ddl_floorMaterial"].ConvertToString();
                        objModel.BuildingMaterial = fc["ddl_buildingMaterial"].ConvertToString();
                        objModel.BuildingPosition = fc["ddl_buildingPosition"].ConvertToString();
                        objModel.PlanConfig = fc["ddl_planConfig"].ConvertToString();
                        objModel.GeographicRisk = fc["ddl_yesnoGeographicRisk"].ConvertToString();
                        objModel.InspectedPart = fc["ddl_inspectedpart"].ConvertToString();
                        objModel.DamageGrade = fc["ddl_damageGrade"].ConvertToString();
                        objModel.TechnicalSolution = fc["ddl_technicalSolution"].ConvertToString();
                        objModel.ReConstructionStart = fc["ddl_reConstrctStrt"].ConvertToString();
                        objModel.OtherUse = fc["ddl_otherUse"].ConvertToString();
                        objModel.Indicator = fc["ddl_Indicator"].ConvertToString();
            DataTable dt = objService.GetStructureAdhocReport(objModel);

            return View();
        }

        public ActionResult HouseDetailReport()
        {
            AdhocHouseDetailReportModel objModel = new AdhocHouseDetailReportModel();
            ViewData["ddl_District"] = cm.GetDistricts("");
            ViewData["ddl_VDCMun"] = cm.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = cm.GetWardByVDCMun("", "");

            ViewData["ddl_gender"] = cm.GetGender("");
            ViewData["ddl_IndicatorAge"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_isOwner"] = (List<SelectListItem>)cm.GetYesNo("").Data;
            ViewData["ddl_relation"] = cm.GetRelation("");
            ViewData["ddl_identificationType"] = cm.GetDocumentType("");
            ViewData["ddl_dist"] = cm.GetDistrictsForUser(Session["UserDistrictDefCd"].ConvertToString());
            ViewData["ddl_ethnicity"] = cm.getEthnicity("");
            ViewData["ddl_education"] = cm.GetEducation("");
            ViewData["ddl_account"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            ViewData["ddl_shelterAEQ"] = cm.ShelterAfterEq("");
            ViewData["ddl_shelterBEQ"] = cm.ShelterBeforeEq("");
            ViewData["ddl_currentPlace"] = cm.ShelterBeforeEq("");
            ViewData["ddl_EqCard"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            ViewData["ddl_monthlyIncome"] = cm.GetMonthlyIncome("");
            ViewData["ddl_phone"] = (List<SelectListItem>)cm.GetYesNo1("").Data;

            return View();
        }
        [HttpPost]
        public ActionResult HouseDetailReport(FormCollection fc)
        {
            AdhocHouseDetailReportModel objModel = new AdhocHouseDetailReportModel();
            AdhocReportSummaryService objService = new AdhocReportSummaryService();
            objModel.Districtcd = fc["ddl_District"].ConvertToString();
            objModel.VDCMun = fc["ddl_VDCMun"].ConvertToString();
            objModel.Ward = fc["ddl_Ward"].ConvertToString();
            objModel.Gender = fc["ddl_gender"].ConvertToString();
            objModel.IndicatorAge = fc["ddl_IndicatorAge"].ConvertToString();
                        objModel.IsOwner = fc["ddl_isOwner"].ConvertToString(); 
                        objModel.Relation = fc["ddl_relation"].ConvertToString(); 
                        objModel.IdentificationType = fc["ddl_identificationType"].ConvertToString(); 
                        objModel.Dist = fc["ddl_dist"].ConvertToString(); 
                        objModel.Ethnicity = fc["ddl_ethnicity"].ConvertToString(); 
                        objModel.Education = fc["ddl_education"].ConvertToString(); 
                        objModel.Account = fc["ddl_account"].ConvertToString(); 
                        objModel.ShelterAfterEQ = fc["ddl_shelterAEQ"].ConvertToString(); 
                        objModel.ShelterBeforeEq = fc["ddl_shelterBEQ"].ConvertToString(); 
                        objModel.Place = fc["ddl_currentPlace"].ConvertToString(); 
                        objModel.EqCard = fc["ddl_EqCard"].ConvertToString(); 
                        objModel.MonthluIncome = fc["ddl_monthlyIncome"].ConvertToString(); 
                        objModel.PhoneNo = fc["ddl_phone"].ConvertToString(); 
           DataTable dt = objService.GetDetailAdhocReport(objModel);

            return View();
        }

    }
}
