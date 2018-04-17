using EntityFramework;
using ExceptionHandler;
using MIS.Models.Entity;
using MIS.Models.Report;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Report
{ 
    public class AdhocController : BaseController
    {
        //
        // GET: /Adhoc/
        CommonFunction cm = new CommonFunction();
        [HttpGet]
        public ActionResult ReportSearch()
        {
            AdhocHouseOwnerReportModel objModel = new AdhocHouseOwnerReportModel();
            ViewData["ddl_District"] = cm.GetDistricts("");
            ViewData["ddl_VDCMun"] = cm.GetVDCMunByDistrict("", "");
            ViewData["ddl_Ward"] = cm.GetWardByVDCMun("", "");
            ViewData["ddl_Indicator"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            //  ViewData["ddl_Gender"] = cm.GetGender("");
            ViewData["ddl_yesno"] = (List<SelectListItem>)cm.GetYesNoSearch("").Data;
            ViewData["ddl_yesno1"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            ViewData["ddl_benf_typ"] = (List<SelectListItem>)cm.GetBenfType("").Data;
            ViewData["ddl_oth_house_condtn"] = (List<SelectListItem>)cm.GetOthhousecondth("");

            //Structure
            ViewData["ddl_legalOwner"] = cm.LegalOwner("");
            ViewData["ddl_buildingCondition"] = cm.GetBuildingCondition("");
            ViewData["ddl_foundationType"] = cm.getFoundationType("");
             ViewData["ddl_storeyBeforeEQ"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_StoreyAfterEq"] = (List<SelectListItem>)cm.GetIndicator("").Data;
            ViewData["ddl_floorMaterial"] = cm.GetFloorMaterial("");
            ViewData["ddl_buildingMaterial"] = cm.getBuildingMaterial("");
            ViewData["ddl_buildingPosition"] = cm.getBuildingPosition("");
            ViewData["ddl_planConfig"] = cm.getPlanConfiguration("");
            ViewData["ddl_yesnoGeographicRisk"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
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
            ViewData["ddl_maritalStatus"] = cm.GetMaritalStatus("");

            objModel.SuperStruct = AdhocReportSummaryService.GetSuperStructure("");

            ViewData["ddl_phone"] = (List<SelectListItem>)cm.GetYesNo1("").Data;
            objModel.gender = AdhocReportSummaryService.GetAdhocGender();
            objModel.isrespondend = (List<SelectListItem>)cm.GetYesNo1("").Data;
            objModel.legalOWner = AdhocReportSummaryService.LegalOwner("");
            objModel.BuildingCondition = AdhocReportSummaryService.GetBuildingCondition("");
            //objModel.foundationType = AdhocReportSummaryService.getFoundationType("");
            //objModel.RoofMaterial = AdhocReportSummaryService.GetRoofType("");
            //objModel.FloorMaterial = AdhocReportSummaryService.GetFloorMaterial("");
            //objModel.BuildingMaterial = AdhocReportSummaryService.getBuildingMaterial("");
            //objModel.BuildingPosition = AdhocReportSummaryService.getBuildingPosition("");
            //objModel.PlanConfig = AdhocReportSummaryService.getPlanConfiguration("");
            //objModel.InspectedPart = AdhocReportSummaryService.GetInspectedHousepart("");
            objModel.DamageGrade = AdhocReportSummaryService.GetDamageGrade("");
            objModel.TechnicalSolution = AdhocReportSummaryService.GetTechnicalSolution("");
            //objModel.IdentificationType = AdhocReportSummaryService.GetDocumentType("");
            //objModel.IdentificationIssuDist = AdhocReportSummaryService.GetDistrictsForUser("");
            objModel.Ethnicity = AdhocReportSummaryService.getEthnicity("");
            objModel.Education = AdhocReportSummaryService.GetEducation("");
            objModel.MaritalStatussss = AdhocReportSummaryService.GetMaritalStatus("");
            //objModel.HaveBankAccount = (List<SelectListItem>)AdhocReportSummaryService.GetYesNo1("").Data;
            //objModel.ShelterAfterEQ = AdhocReportSummaryService.ShelterAfterEq("");
            //objModel.ShelterBeforeEQ = AdhocReportSummaryService.ShelterBeforeEq("");
            //objModel.CurrentPlace = AdhocReportSummaryService.ShelterBeforeEq("");
            //objModel.HaveEQVictimCard = (List<SelectListItem>)AdhocReportSummaryService.GetYesNo1("").Data;
            objModel.MonthlyIncome = AdhocReportSummaryService.GetMonthlyIncome("");
            //objModel.PhoneNo = (List<SelectListItem>)AdhocReportSummaryService.GetYesNo1("").Data;


            return View(objModel);
        }
        [HttpPost]
        public ActionResult ReportSearch(Adhoc_ReportSearchEntity objReport, FormCollection fc)
        {
            DataTable dt = new DataTable();
            AdhocCommonService service = new AdhocCommonService();
            #region Form Collection
            int totalparameterscount = 0;

            objReport.District_CD = fc["txtDistrict"];

            string vdcCD = fc["txtvdcmun"];
            objReport.VDC_MUN_CD = cm.GetCodeFromDataBase(vdcCD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objReport.Ward = fc["txtWard"];
            objReport.Beneficiary = fc["BeneficiaryFlag"];

            if (objReport.Beneficiary == "Y")
            {
                objReport.Beneficiary = "EL";

            }
            if (objReport.Beneficiary == "N")
            {
                objReport.Beneficiary = "NE";

            }
            objReport.BeneficiaryType = fc["beneficiary_type"];

            if (objReport.BeneficiaryType == "A")
            {
                objReport.BeneficiaryType = "All";

            }
            if (objReport.BeneficiaryType == "RTF")
            {
                objReport.BeneficiaryType = "Retro";

            }
            if (objReport.BeneficiaryType == "RTC")
            {
                objReport.BeneficiaryType = "Reconstruction";

            }
            if (objReport.BeneficiaryType == "GRC")
            {
                objReport.BeneficiaryType = "Grievance";

            }
            objReport.Grievance = fc["GrievanceRegisteredFlag"];

            if (objReport.Grievance == "Y")
            {


            }
            if (objReport.Grievance == "N")
            {

            }
            objReport.Phone_Number = fc["PhnFlag"];

            if (objReport.Phone_Number == "Y")
            {
                objReport.Phone_Number = " NOT NULL";

            }
            if (objReport.Phone_Number == "N")
            {
                objReport.Phone_Number = " NULL";


            }

            objReport.Otherhouse = fc["OtherHouse"];

            if (objReport.Otherhouse == "Y")
            {
                objReport.Otherhouse = "NOT NULL";

            }
            if (objReport.Otherhouse == "N")
            {
                objReport.Otherhouse = " NULL";

            }


            objReport.HouseOwner = fc["houseowner"];

            objReport.OwnerCountIndicator = fc["ddl_IndicatorOwnerCount"];
            objReport.OwnerCount = fc["OwnerCnt"];
            if (objReport.OwnerCountIndicator.ConvertToString() != "" && objReport.OwnerCount.ConvertToString() != "")
            {

            }

            objReport.Gender_CD = fc["gender"];
            if (objReport.Gender_CD.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.IsRespond = fc["ddl_yesno"];
            if (objReport.IsRespond == "Y")
            {


            }
            if (objReport.IsRespond == "N")
            {

            }

            objReport.BuildingStructure = fc["buildingstructure"];
            objReport.HouseLegalOwner = fc["legalOWner"];
            if (objReport.HouseLegalOwner.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.BuildingCondition = fc["BuildingCondition"];
            if (objReport.BuildingCondition.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.StoreyCountBeforeEQ_Indict = fc["ddl_storeyBeforeEQ"];
            objReport.StoreyCountBefore = fc["txtStoreyCndBEQ"];
            if (objReport.StoreyCountBeforeEQ_Indict.ConvertToString() != "" && objReport.StoreyCountBefore.ConvertToString() != "")
            {

            }
            objReport.StoreyAfterEQ_Indict = fc["ddl_StoreyAfterEq"];
            objReport.StoreyCountAfterEQ = fc["txtStoreyCndAEQ"];
            if (objReport.StoreyAfterEQ_Indict.ConvertToString() != "" && objReport.StoreyCountAfterEQ.ConvertToString() != "")
            {

            }
            objReport.FoundationType = fc["foundationType"];

            objReport.RoofMaterials = fc["RoofMaterial"];
            objReport.FloorMaterial = fc["FloorMaterial"];
            objReport.BuildingMaterial = fc["BuildingMaterial"];
            objReport.BuildingPosition = fc["BuildingPosition"];
            objReport.PlanConfig = fc["PlanConfig"];

            objReport.GeographicalRisk = fc["ddl_yesnoGeographicRisk"];
            if (objReport.GeographicalRisk == "Y")
            {


            }
            if (objReport.GeographicalRisk == "N")
            {

            }
            objReport.InspectedPart = fc["InspectedPart"];
            objReport.Damage_Grade = fc["DamageGrade"];
            if (objReport.Damage_Grade.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.Technical_soln_CD = fc["TechnicalSolution"];
            if (objReport.Technical_soln_CD.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.ReconstructionStarted = fc["ddl_reConstrctStrt"];

            objReport.OtherUseOfHouse = fc["ddl_otherUse"];
            objReport.MemberCntAfterEQ_Indicator = fc["ddl_Indicator"];
            objReport.MemberCountAEQ = fc["MemberCntAfterEq"];
            if (objReport.MemberCntAfterEQ_Indicator.ConvertToString() != "" && objReport.MemberCountAEQ.ConvertToString() != "")
            {

            }
            objReport.SuperStruct_CD = fc["SuperStruct"];

            if (objReport.SuperStruct_CD.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.Education = fc["Education"];
            if (objReport.Education.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.MonthlyIncome = fc["MonthlyIncome"];

            if (objReport.MonthlyIncome.ConvertToString() != "")
            {
                totalparameterscount++;
            }

            objReport.Marital_Status = fc["MaritalStatussss"];

            if (objReport.Marital_Status.ConvertToString() != "")
            {
                totalparameterscount++;
            }
            objReport.Death_Cnt_Indicator = fc["ddl_IndicatorDeathCount"];
            objReport.Death_Count = fc["DeathCnt"];
            if (objReport.Death_Cnt_Indicator.ConvertToString() != "" && objReport.Death_Count.ConvertToString() != "")
            {

            }
            #endregion Form collection

            #region QUERY START
            int parametercountforconcat = 0;
            int parametercountforpermutation = 0;
            var bracketvalue = "";
            var columnname = "";
            string newcmdText = "";
            var totalinnerselect = "";
            string insertinginvalue = "";
            string whereconditions = "WHERE 1=1 ";
            String InnerJOin = "  INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD   INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD";

            newcmdText = " SELECT ";

            if (objReport.BeneficiaryType.ConvertToString() == "Grievance")
            {
                InnerJOin = "";
                InnerJOin = " INNER JOIN MIS_DISTRICT md ON ngem.DISTRICT_CD = md.DISTRICT_CD INNER JOIN MIS_VDC_MUNICIPALITY mvm ON ngem.VDC_MUN_CD = mvm.VDC_MUN_CD";
            }
            if (objReport.BeneficiaryType.ConvertToString() == "Reconstruction")
            {
                InnerJOin = "";
                InnerJOin = " inner JOIN NHRS_ENROLLMENT_MST nem on nhom.HOUSE_OWNER_ID = nem.HOUSE_OWNER_ID    INNER JOIN MIS_DISTRICT md ON nem.DISTRICT_CD = md.DISTRICT_CD INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nem.VDC_MUN_CD = mvm.VDC_MUN_CD";
            }
            if (objReport.BeneficiaryType.ConvertToString() == "Retro")
            {
                InnerJOin = "";
                InnerJOin = "  INNER JOIN NHRS_RETRO_ENROLL_MST nrem on nhom.HOUSE_OWNER_ID = nrem.HOUSE_OWNER_ID    INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD    INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD  ";
            }

            #region for  join

            objReport.HouseOwner = fc["houseowner"];

            objReport.DeathCount = fc["deathcount2"];


            objReport.EducationQuery = fc["education8"];
            objReport.MonthlyIncomeQuery = fc["monthlyincome9"];
            //objReport.Marital_Status = fc["maritalstatuss"];

            objReport.RespondentisHouseowner = fc["respondentishouseowner3"];
            objReport.StoryCountBeforeEarthQuake = fc["storycountbeforeearthquake4"];


            objReport.StoryCountAfterEarthQuake = fc["storycountafterearthquake5"];

            objReport.MemberCountAfterEarthQuake = fc["membercountafterearthquake6"];

            objReport.SuperStructure = fc["Superstructure1"];

            objReport.Gender = fc["gender7"];

            objReport.BuildingStructure = fc["buildingstructure"];

            objReport.HouseHold = fc["Household"];

            if (objReport.Gender == "Y")
            {
                if (objReport.BeneficiaryType.ConvertToString() == "Grievance")
                {
                    InnerJOin = InnerJOin + " INNER JOIN  NHRS_HOUSE_OWNER_DTL nhod ON ngem.HOUSE_OWNER_ID = nhod.HOUSE_OWNER_ID ";

                }
                else
                {
                    InnerJOin = InnerJOin + " INNER JOIN  NHRS_HOUSE_OWNER_DTL nhod ON nhom.HOUSE_OWNER_ID = nhod.HOUSE_OWNER_ID ";

                }
               
            }
            if (objReport.BuildingStructure == "Y")
            {

                InnerJOin = InnerJOin + @" INNER JOIN NHRS_BUILDING_ASSESSMENT_MST nbam On nhom.HOUSE_OWNER_ID = nbam.HOUSE_OWNER_ID 
                        INNER JOIN NHRS_BUILDING_CONDITION nbc ON nbam.BUILDING_CONDITION_CD = nbc.BUILDING_CONDITION_CD  
                        INNER JOIN NHRS_DAMAGE_GRADE ndg ON nbam.DAMAGE_GRADE_CD = ndg.DAMAGE_GRADE_CD   
                        INNER JOIN NHRS_TECHNICAL_SOLUTION nts ON nbam.TECHSOLUTION_CD = nts.TECHSOLUTION_CD  
                  
                        left outer join NHRS_HOUSE_LAND_LEGAL_OWNER nhllo on nbam.HOUSE_LAND_LEGAL_OWNER=nhllo.HOUSE_LAND_LEGAL_OWNERCD ";

                if (objReport.SuperStructure == "Y")
                {
                    InnerJOin = InnerJOin + @"    inner join NHRS_BA_SUPERSTRUCTURE_MAT nbsm ON nhom.HOUSE_OWNER_ID = nbsm.HOUSE_OWNER_ID  
                        AND NBAM.BUILDING_STRUCTURE_NO=NBSM.BUILDING_STRUCTURE_NO 
                        inner join NHRS_SUPERSTRUCTURE_MATERIAL nsm ON nbsm.SUPERSTRUCTURE_MAT_ID = nsm.SUPERSTRUCTURE_MATERIAL_CD   ";
                }
            }
            if (objReport.HouseHold == "Y")
            {
                if (objReport.MonthlyIncomeQuery == "Y")
                {
                    InnerJOin = InnerJOin + @" INNER join MIS_HOUSEHOLD_INFO mhi ON nhom.HOUSE_OWNER_ID = mhi.HOUSE_OWNER_ID                             
                            
                            
                                ";
                }

                if (objReport.EducationQuery == "Y")
                {
                    InnerJOin = InnerJOin + @" 
                            INNER JOIN MIS_MEMBER mm ON nhom.HOUSE_OWNER_ID = mm.HOUSE_OWNER_ID 
                      
                               AND mm.HOUSEHOLD_HEAD='Y'
                           
                                ";
                }
                if(objReport.Marital_Status.ConvertToString()!="")
                {
                    InnerJOin = InnerJOin + @" INNER JOIN MIS_MEMBER mm ON nhom.HOUSE_OWNER_ID = mm.HOUSE_OWNER_ID 
                      
                               AND mm.HOUSEHOLD_HEAD='Y'
                    ";
                }

               

            }





            #endregion

















            #region SELECT PART OF QUERY
            #region DISTRICT SELECT
            if (objReport.District_CD.ConvertToString() == "")
            {
                newcmdText = newcmdText + " DISTRICT, ";

            }

            if (objReport.District_CD.ConvertToString() != "")
            {

                newcmdText = newcmdText + " DISTRICT,VDC_MUN,";


                if (objReport.VDC_MUN_CD.ConvertToString() != "")
                {
                    newcmdText = newcmdText + "WARD_NO, ";

                }


            }
            #endregion DISTRICT SELECT

            #region PARAMETERS MAPPING
            #region gender codes and name mapping

            string[] gendercodes = objReport.Gender_CD.ConvertToString().Split(',');
            string gendervalues = string.Empty;
            string gendername = string.Empty;
            string gendernamecomma = string.Empty;


            foreach (string str in gendercodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;

                if (str.ConvertToString() == "1")
                {
                    tempname = "M";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "F";
                    tempval = "2";

                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "O";
                    tempval = "3";

                }

                if (gendervalues == "")
                {
                    gendervalues = tempval;
                    gendername = tempname;

                }
                else
                {
                    gendervalues = gendervalues + "," + tempval;
                    gendername = gendername + "," + tempname;

                }

                i++;
            }
            if (objReport.Gender_CD.ConvertToString() != "")
            {

                insertinginvalue = insertinginvalue + "_" + objReport.Gender_CD;
                if (totalparameterscount == 1)
                {
                    parametercountforpermutation++;
                }
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    columnname = gendername;
                    insertinginvalue = objReport.Gender_CD;

                }
                else
                {
                    columnname = "_" + gendername;

                }
                if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = "(nhod.gender_cd)";
                }
                else
                {
                    bracketvalue = "nhod.gender_cd,'_')";

                }

            }

            #endregion  gender codes and name mapping



            #region  house_legal_owner codes and name mapping
            string[] house_legal_owner_codes = objReport.HouseLegalOwner.ConvertToString().Split(',');
            string house_legal_owner_values = string.Empty;
            string house_legal_owner_name = string.Empty;

            foreach (string str in house_legal_owner_codes)
            {
                int i = 0;
                string tempname = string.Empty;
                string tempval = string.Empty;
                if (str.ConvertToString() == "1")
                {
                    tempname = "O";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "OH";
                    tempval = "2";
                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "GOP";
                    tempval = "3";
                }
                if (str.ConvertToString() == "4")
                {
                    tempname = "OP";
                    tempval = "4";
                }


                if (house_legal_owner_values == "")
                {
                    house_legal_owner_name = tempname;
                    house_legal_owner_values = tempval;


                }
                else
                {
                    house_legal_owner_values = house_legal_owner_values + "," + tempval;
                    house_legal_owner_name = house_legal_owner_name + "," + tempname;
                }

                i++;
            }

            if (objReport.HouseLegalOwner.ConvertToString() != "")
            {
                insertinginvalue = insertinginvalue + "_" + objReport.HouseLegalOwner;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.HouseLegalOwner;
                    columnname = house_legal_owner_name;

                }
                else
                {
                    columnname = columnname + "_" + house_legal_owner_name;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "nbam.HOUSE_LAND_LEGAL_OWNER,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",nbam.HOUSE_LAND_LEGAL_OWNER)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",nbam.HOUSE_LAND_LEGAL_OWNER),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(nbam.HOUSE_LAND_LEGAL_OWNER)";
                }

            }


            #endregion  house_legal_owner codes and name mapping



            #region  building_condition codes and name mapping
            string[] building_condition_codes = objReport.BuildingCondition.ConvertToString().Split(',');

            string building_condition_values = string.Empty;
            string building_condition_name = string.Empty;
            foreach (string str in building_condition_codes)
            {
                int i = 0;
                string tempname = string.Empty;
                string tempval = string.Empty;
                if (str.ConvertToString() == "1")
                {
                    tempname = "SMSRe";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "DBL";
                    tempval = "2";
                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "Rec";
                    tempval = "3";
                }
                if (str.ConvertToString() == "4")
                {
                    tempname = "DBCNR";
                    tempval = "4";
                }
                if (str.ConvertToString() == "5")
                {
                    tempname = "DBCRAR";
                    tempval = "5";
                }

                if (str.ConvertToString() == "6")
                {
                    tempname = "MDARAS";
                    tempval = "6";
                }
                if (str.ConvertToString() == "7")
                {
                    tempname = "DARAS";
                    tempval = "7";
                }
                if (str.ConvertToString() == "8")
                {
                    tempname = "HNM";
                    tempval = "8";
                }

                if (building_condition_values == "")
                {
                    building_condition_name = tempname;
                    building_condition_values = tempval;


                }
                else
                {
                    building_condition_values = building_condition_values + "," + tempval;
                    building_condition_name = building_condition_name + "," + tempname;
                }

                i++;
            }


            if (objReport.BuildingStructure.ConvertToString() != "")
            {
                if (objReport.BuildingCondition.ConvertToString() != "")
                {

                    insertinginvalue = insertinginvalue + "_" + objReport.BuildingCondition;
                    parametercountforpermutation++;
                    parametercountforconcat++;
                    if (parametercountforconcat == 1)
                    {
                        insertinginvalue = objReport.BuildingCondition;
                        columnname = building_condition_name;
                    }
                    else
                    {
                        columnname = columnname + "_" + building_condition_name;

                    }
                    if (parametercountforconcat == 1)
                    {

                        bracketvalue = "nbc.BUILDING_CONDITION_CD,'_')";
                    }

                    else if (parametercountforconcat == totalparameterscount)
                    {

                        bracketvalue = bracketvalue + ",nbc.BUILDING_CONDITION_CD)";
                    }
                    else
                    {
                        bracketvalue = bracketvalue + ",nbc.BUILDING_CONDITION_CD),'_')";

                    }
                    if (totalparameterscount == 1)
                    {
                        bracketvalue = "(nbc.BUILDING_CONDITION_CD)";
                    }
                }
            }
            #endregion  building_condition codes and name mapping

            #region damage grade code and name mapping

            string[] damagegradecodes = objReport.Damage_Grade.ConvertToString().Split(',');

            string damagegradevalues = string.Empty;
            string damagegradenames = string.Empty;

            foreach (string str in damagegradecodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;

               

                if (str.ConvertToString() == "1")
                {
                    tempname = "G1";
                    tempval = "1";

                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "G2";
                    tempval = "2";


                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "G3";
                    tempval = "3";


                }

                if (str.ConvertToString() == "4")
                {
                    tempname = "G4";
                    tempval = "4";


                }

                if (str.ConvertToString() == "5")
                {
                    tempname = "G5";
                    tempval = "5";


                }

                if (damagegradevalues == "")
                {
                    damagegradevalues = tempval;
                    damagegradenames = tempname;

                }
                else
                {
                    damagegradevalues = damagegradevalues + "," + tempval;
                    damagegradenames = damagegradenames + "," + tempname;
                }

                i++;
            }



            if (objReport.Damage_Grade.ConvertToString() != "")
            {
                insertinginvalue = insertinginvalue + "_" + objReport.Damage_Grade;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.Damage_Grade;
                    columnname = damagegradenames;

                }
                else
                {
                    columnname = columnname + "_" + damagegradenames;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "nbam.DAMAGE_GRADE_CD,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",nbam.DAMAGE_GRADE_CD)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",nbam.DAMAGE_GRADE_CD),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(nbam.DAMAGE_GRADE_CD)";
                }

            }
            #endregion damage grade code and name mapping


            #region  Technical Solution code and name mapping

            string[] technicalsolncodes = objReport.Technical_soln_CD.ConvertToString().Split(',');
            string technicalsolnvalues = string.Empty;
            string technicalsolnnames = string.Empty;

            foreach (string str in technicalsolncodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;

              

                if (str.ConvertToString() == "1")
                {
                    tempname = "NR";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "MIR";
                    tempval = "2";

                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "MJR";
                    tempval = "3";

                }

                if (str.ConvertToString() == "4")
                {
                    tempname = "Rec";
                    tempval = "4";

                }



                if (technicalsolnvalues == "")
                {
                    technicalsolnvalues = tempval;
                    technicalsolnnames = tempname;

                }
                else
                {
                    technicalsolnvalues = technicalsolnvalues + "," + tempval;
                    technicalsolnnames = technicalsolnnames + "," + tempname;
                }

                i++;
            }


            if (objReport.Technical_soln_CD.ConvertToString() != "")
            {
                insertinginvalue = insertinginvalue + "_" + objReport.Technical_soln_CD;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.Technical_soln_CD;
                    columnname = technicalsolnnames;

                }
                else
                {
                    columnname = columnname + "_" + technicalsolnnames;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "nbam.TECHSOLUTION_CD,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",nbam.TECHSOLUTION_CD)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",nbam.TECHSOLUTION_CD),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(nbam.TECHSOLUTION_CD)";
                }

            }
            #endregion Technical Solution code and name mapping


            #region Super structure code and name mapping

            string[] superstructcodes = objReport.SuperStruct_CD.ConvertToString().Split(',');
            string superstructvalues = string.Empty;
            string superstructnnames = string.Empty;

            foreach (string str in superstructcodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;



                if (str.ConvertToString() == "1")
                {
                    tempname = "AMC";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "SM";
                    tempval = "2";

                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "DS";
                    tempval = "3";

                }

                if (str.ConvertToString() == "4")
                {
                    tempname = "SC";
                    tempval = "4";

                }
                if (str.ConvertToString() == "5")
                {
                    tempname = "FBM";
                    tempval = "4";

                }
                if (str.ConvertToString() == "6")
                {
                    tempname = "BC";
                    tempval = "4";

                }
                if (str.ConvertToString() == "7")
                {
                    tempname = "TF";
                    tempval = "4";

                }
                if (str.ConvertToString() == "8")
                {
                    tempname = "BF";
                    tempval = "4";

                }

                if (str.ConvertToString() == "9")
                {
                    tempname = "RCNE";
                    tempval = "4";

                } if (str.ConvertToString() == "10")
                {
                    tempname = "RCE";
                    tempval = "4";

                }
                if (str.ConvertToString() == "11")
                {
                    tempname = "MI";
                    tempval = "4";

                }



                if (superstructvalues == "")
                {
                    superstructvalues = tempval;
                    superstructnnames = tempname;

                }
                else
                {
                    superstructvalues = superstructvalues + "," + tempval;
                    superstructnnames = superstructnnames + "," + tempname;
                }

                i++;
            }

            if (objReport.SuperStruct_CD.ConvertToString() != "")
            {

                insertinginvalue = insertinginvalue + "_" + objReport.SuperStruct_CD;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.SuperStruct_CD;
                    columnname = superstructnnames;

                }
                else
                {
                    columnname = columnname + "_" + superstructnnames;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "nsm.SUPERSTRUCTURE_MATERIAL_CD,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",nsm.SUPERSTRUCTURE_MATERIAL_CD)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",nsm.SUPERSTRUCTURE_MATERIAL_CD),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(nsm.SUPERSTRUCTURE_MATERIAL_CD)";
                }

            }
            #endregion Super Structure code and name mapping

            #region  Education  code and name mapping

            string[] educationcodes = objReport.Education.ConvertToString().Split(',');
            string educationvalues = string.Empty;
            string educationnames = string.Empty;

            foreach (string str in educationcodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;

                if (str.ConvertToString() == "0")
                {
                    tempname = "KNG";
                    tempval = "0";

                }

                if (str.ConvertToString() == "1")
                {
                    tempname = "C1";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "C2";
                    tempval = "2";

                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "C3";
                    tempval = "3";

                }

                if (str.ConvertToString() == "4")
                {
                    tempname = "C4";
                    tempval = "4";

                }
                if (str.ConvertToString() == "5")
                {
                    tempname = "C5";
                    tempval = "5";

                }
                if (str.ConvertToString() == "6")
                {
                    tempname = "C6";
                    tempval = "6";

                }
                if (str.ConvertToString() == "7")
                {
                    tempname = "C7";
                    tempval = "7";

                }
                if (str.ConvertToString() == "8")
                {
                    tempname = "C8";
                    tempval = "8";

                }
                if (str.ConvertToString() == "9")
                {
                    tempname = "C9";
                    tempval = "9";

                }
                if (str.ConvertToString() == "10")
                {
                    tempname = "C10";
                    tempval = "10";

                }


                if (str.ConvertToString() == "12")
                {
                    //tempname = "IN";
                    tempname = "INT";
                    tempval = "12";

                }
                if (str.ConvertToString() == "13")
                {
                    tempname = "BAC";
                    tempval = "13";

                }
                if (str.ConvertToString() == "14")
                {
                    tempname = "MST";
                    tempval = "14";

                }

                if (str.ConvertToString() == "15")
                {
                    tempname = "AH";
                    tempval = "15";

                }
                if (str.ConvertToString() == "98")
                {
                    tempname = "IE";
                    tempval = "98";

                }

                if (str.ConvertToString() == "99")
                {
                    tempname = "DK";
                    tempval = "99";

                } if (str.ConvertToString() == "100")
                {
                    tempname = "ILL";
                    tempval = "100";

                } if (str.ConvertToString() == "101")
                {
                    tempname = "PL";
                    tempval = "101";

                } if (str.ConvertToString() == "102")
                {
                    tempname = "SL";
                    tempval = "102";

                } if (str.ConvertToString() == "20")
                {
                    tempname = "PHD";
                    tempval = "20";

                } if (str.ConvertToString() == "21")
                {
                    tempname = "Lit";
                    tempval = "21";

                } if (str.ConvertToString() == "22")
                {
                    tempname = "SEQU";
                    tempval = "22";

                }




                if (educationvalues == "")
                {
                    educationvalues = tempval;
                    educationnames = tempname;

                }
                else
                {
                    educationvalues = educationvalues + "," + tempval;
                    educationnames = educationnames + "," + tempname;
                }

                i++;
            }


            if (objReport.Education.ConvertToString() != "")
            {

                insertinginvalue = insertinginvalue + "_" + objReport.Education;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.Education;
                    columnname = educationnames;

                }
                else
                {
                    columnname = columnname + "_" + educationnames;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "mm.EDUCATION_CD,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",mm.EDUCATION_CD)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",mm.EDUCATION_CD),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(mm.EDUCATION_CD)";
                }

            }
            #endregion Education code and name mapping

            #region  Monthly Income code and name mapping

            string[] monthlyincomecodes = objReport.MonthlyIncome.ConvertToString().Split(',');
            string monthlyincomevalues = string.Empty;
            string monthlyincomenames = string.Empty;

            foreach (string str in monthlyincomecodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;


                if (str.ConvertToString() == "1")
                {
                    tempname = "B10T";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    //tempname = "10TO20";
                    tempname = "TenTO20";
                    tempval = "2";

                }
                if (str.ConvertToString() == "3")
                {
                    //tempname = "20TO30";
                    tempname = "TwentyTO30";
                    tempval = "3";

                }

                if (str.ConvertToString() == "4")
                {
                    //tempname = "30TO50";
                    tempname = "ThirtyTO50";
                    tempval = "4";

                }
                if (str.ConvertToString() == "5")
                {
                    //tempname = "A50T";
                    tempname = "Above50T";
                    tempval = "5";

                }


                if (monthlyincomevalues == "")
                {
                    monthlyincomevalues = tempval;
                    monthlyincomenames = tempname;

                }
                else
                {
                    monthlyincomevalues = monthlyincomevalues + "," + tempval;
                    monthlyincomenames = monthlyincomenames + "," + tempname;
                }

                i++;
            }


            if (objReport.MonthlyIncome.ConvertToString() != "")
            {

                insertinginvalue = insertinginvalue + "_" + objReport.MonthlyIncome;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.MonthlyIncome;
                    columnname = monthlyincomenames;

                }
                else
                {
                    columnname = columnname + "_" + monthlyincomenames;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "MHI.MONTHLY_INCOME_CD,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",MHI.MONTHLY_INCOME_CD)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",MHI.MONTHLY_INCOME_CD),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(MHI.MONTHLY_INCOME_CD)";
                }

            }
            #endregion Monthly Income code and name mapping


            #region  Marital Status code and name mapping

            string[] maritalstatuscodes = objReport.Marital_Status.ConvertToString().Split(',');
            string maritalstatusvalues = string.Empty;
            string maritalstatusnames = string.Empty;

            foreach (string str in maritalstatuscodes)
            {
                int i = 0;

                string tempval = string.Empty;
                string tempname = string.Empty;



                if (str.ConvertToString() == "1")
                {
                    tempname = "UNM";
                    tempval = "1";
                }
                if (str.ConvertToString() == "2")
                {
                    tempname = "MAR";
                    tempval = "2";

                }
                if (str.ConvertToString() == "3")
                {
                    tempname = "WIDOW";
                    tempval = "3";

                }

                if (str.ConvertToString() == "4")
                {
                    tempname = "DIV";
                    tempval = "4";

                }

                if (str.ConvertToString() == "5")
                {
                    tempname = "SEP";
                    tempval = "4";

                }



                if (maritalstatusvalues == "")
                {
                    maritalstatusvalues = tempval;
                    maritalstatusnames = tempname;

                }
                else
                {
                    maritalstatusvalues = maritalstatusvalues + "," + tempval;
                    maritalstatusnames = maritalstatusnames + "," + tempname;
                }

                i++;
            }


            if (objReport.Marital_Status.ConvertToString() != "")
            {
                insertinginvalue = insertinginvalue + "_" + objReport.Marital_Status;
                parametercountforpermutation++;
                parametercountforconcat++;
                if (parametercountforconcat == 1)
                {
                    insertinginvalue = objReport.Marital_Status;
                    columnname = maritalstatusnames;

                }
                else
                {
                    columnname = columnname + "_" + maritalstatusnames;

                }
                if (parametercountforconcat == 1)
                {

                    bracketvalue = "MARITAL_STATUS_CD,'_')";
                }
                else if (parametercountforconcat == totalparameterscount)
                {

                    bracketvalue = bracketvalue + ",mm.MARITAL_STATUS_CD)";
                }
                else
                {
                    bracketvalue = bracketvalue + ",mm.MARITAL_STATUS_CD),'_')";

                }

                if (totalparameterscount == 1)
                {
                    bracketvalue = "(mm.MARITAL_STATUS_CD)";
                }

            }
            #endregion Marital Status code and name mapping

            #endregion PARAMETERS MAPPING



            string concat = string.Empty;

            #region CONCAT PART
            if (parametercountforconcat <= 2)
            {
                for (int i = 1; i <= parametercountforconcat; i++)
                {
                    concat = concat + "CONCAT(";
                }
                if (totalparameterscount == 1)
                {
                    concat = "";
                }
            }
            else if (totalparameterscount == 3)
            {
                for (int i = 0; i <= parametercountforconcat; i++)
                {
                    concat = concat + "CONCAT(";
                }

            }
            if (totalparameterscount == 4)
            {
                for (int i = 0; i <= parametercountforconcat + 1; i++)
                {
                    concat = concat + "CONCAT(";
                }
            }
            if (totalparameterscount == 5)
            {
                for (int i = 0; i <= parametercountforconcat + 2; i++)
                {
                    concat = concat + "CONCAT(";
                }
            }
            if (totalparameterscount == 6)
            {
                for (int i = 0; i <= parametercountforconcat + 3; i++)
                {
                    concat = concat + "CONCAT(";
                }
            }

            if (totalparameterscount == 7)
            {
                for (int i = 0; i <= parametercountforconcat + 4; i++)
                {
                    concat = concat + "CONCAT(";
                }
            }



            #endregion CONCAT PART

            #endregion SELECT PART OF QUERY

            #region CONDITION PART
            if (objReport.BeneficiaryType.ConvertToString() == "Grievance")
            {
                if (objReport.District_CD.ConvertToString() != "")
                {
                    whereconditions = whereconditions + " AND MD.DISTRICT_CD='" + objReport.District_CD + "'";

                    if (objReport.VDC_MUN_CD.ConvertToString() != "")
                    {
                        whereconditions = whereconditions + " AND mvm.VDC_MUN_CD='" + objReport.VDC_MUN_CD + "'";


                    }
                    if (objReport.Ward.ConvertToString() != "")
                    {
                        whereconditions = whereconditions + " AND  ngem.WARD_NO='" + objReport.Ward + "'";

                    }

                }
            }
            else
            {
                if (objReport.District_CD.ConvertToString() != "")
                {
                    whereconditions = whereconditions + " AND NHOM.DISTRICT_CD='" + objReport.District_CD + "'";
                }
                if (objReport.VDC_MUN_CD.ConvertToString() != "")
                {
                    whereconditions = whereconditions + " AND NHOM.VDC_MUN_CD='" + objReport.VDC_MUN_CD + "'";


                }
                if (objReport.Ward.ConvertToString() != "")
                {
                    whereconditions = whereconditions + " AND  NHOM.WARD_NO='" + objReport.Ward + "'";

                }
            }
            

            if (objReport.Beneficiary.ConvertToString() != "")
            {

                if(objReport.Beneficiary.ConvertToString()=="NE")
                {
                    whereconditions = whereconditions + " AND nhom.TARGET_FLAG='NE' ";

                }
                if (objReport.BeneficiaryType.ConvertToString() != "")
                {


                    if (objReport.BeneficiaryType.ConvertToString() == "Retro")
                    {

                        whereconditions = whereconditions + "  AND nhom.RETROFITING_TARGET_FLAG='EL' AND   nhod.SNO='1' AND  nrem.NRA_DEFINED_CD is not NULL ";


                    }
                    if (objReport.BeneficiaryType.ConvertToString() == "Reconstruction")
                    {

                        whereconditions = whereconditions + " AND nhod.SNO='1' AND nhom.TARGET_FLAG='EL' AND nem.NRA_DEFINED_CD is not NULL";


                    }

                    if (objReport.BeneficiaryType.ConvertToString() == "Grievance")
                    {

                        whereconditions = whereconditions + " AND  nhod.SNO='1' ";



                    }


                    if (objReport.BeneficiaryType.ConvertToString() == "All")
                    {


                        whereconditions = whereconditions + " AND nhom.RETROFITING_TARGET_FLAG='EL' ";


                    }

                }


            }






            if (objReport.Grievance.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND  GRIEVANCE_TARGET_FLAG='" + objReport.Grievance + "'";


            }


            if (objReport.Otherhouse.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND nhom.TOTAL_OTHER_HOUSE_CNT IS " + objReport.Otherhouse + "";


            }

            if (objReport.Phone_Number.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND NHOM.MOBILE_NUMBER IS " + objReport.Phone_Number + "";


            }




            if (objReport.OwnerCountIndicator.ConvertToString() != "" && objReport.OwnerCount.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND NHOM.TOTAL_OWNER_CNT " + objReport.OwnerCountIndicator + objReport.OwnerCount;


            }



            if (objReport.IsRespond.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND  NHOM.RESPONDENT_IS_HOUSE_OWNER='" + objReport.IsRespond + "'";



            }

            if (objReport.StoreyCountBeforeEQ_Indict.ConvertToString() != "" && objReport.StoreyCountBefore.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND STOREYS_CNT_BEFORE" + objReport.StoreyCountBeforeEQ_Indict + objReport.StoreyCountBefore;


            }

            if (objReport.StoreyAfterEQ_Indict.ConvertToString() != "" && objReport.StoreyCountAfterEQ.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND STOREYS_CNT_AFTER" + objReport.StoreyAfterEQ_Indict + objReport.StoreyCountAfterEQ;

            }


            if (objReport.GeographicalRisk.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND IS_GEOTECHNICAL_RISK='" + objReport.GeographicalRisk + "'"; ;


            }

            if (objReport.MemberCntAfterEQ_Indicator.ConvertToString() != "" && objReport.MemberCountAEQ.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND HOUSEHOLD_CNT_AFTER_EQ" + objReport.MemberCntAfterEQ_Indicator + objReport.MemberCountAEQ;


            }

            if (objReport.Death_Cnt_Indicator.ConvertToString() != "" && objReport.Death_Count.ConvertToString() != "")
            {

                whereconditions = whereconditions + " AND MHI.DEATH_CNT" + objReport.Death_Cnt_Indicator + objReport.Death_Count;


            }







            #endregion CONDITION PART

            #region PERMUTATION FOR VALUES
            //IEnumerable<string> namepermutations = null;
            //if (parametercountforpermutation > 1)
            // {


            string[] permutationvalues = new string[parametercountforpermutation];
            int permutationvaluescount = 0;
            if (permutationvaluescount.ConvertToString() != "")
            {
                if (objReport.Gender_CD.ConvertToString() != "")
                {
                    if (totalparameterscount == 1)
                    {
                        permutationvalues[permutationvaluescount] = objReport.Gender_CD;
                    }

                }
                if (objReport.BuildingCondition.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.BuildingCondition;
                    permutationvaluescount++;
                }
                if (objReport.Technical_soln_CD.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.Technical_soln_CD;
                    permutationvaluescount++;
                }
                if (objReport.Damage_Grade.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.Damage_Grade;
                    permutationvaluescount++;
                }
                if (objReport.SuperStruct_CD.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.SuperStruct_CD;
                    permutationvaluescount++;
                }
                if (objReport.HouseLegalOwner.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.HouseLegalOwner;
                    permutationvaluescount++;
                }
                if (objReport.Education.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.Education;
                    permutationvaluescount++;
                }
                if (objReport.MonthlyIncome.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.MonthlyIncome;
                    permutationvaluescount++;
                }
                if (objReport.Marital_Status.ConvertToString() != "")
                {
                    permutationvalues[permutationvaluescount] = objReport.Marital_Status;
                    permutationvaluescount++;
                }


            }



            IEnumerable<string> row = permutationvalues;
            IEnumerable<string> permutations = GetPermutations(row, delimiter: "_");
            List<string> finallistforintvalues = new List<string>();
            if (objReport.Gender_CD.ConvertToString() != "")
            {

                //if (permutations. > 1)
                //{
                //var con = permutations as ICollection;
                foreach (string str1 in permutations)
                {
                    foreach (string str in gendercodes)
                    {
                        //if(con.Count>1)
                        //{ 

                        if (str != "")
                            finallistforintvalues.Add(str + "_" + str1);

                        //else
                        //{
                        //    if (str != "")
                        //        finallistforintvalues.Add(str);

                        //}
                    }
                }
            }
            //else
            //{

            //    foreach (string str1 in permutations)
            //    {
            //        foreach (string str in gendercodes)
            //        {
            //            if (str != "")
            //                finallistforintvalues.Add(str);
            //            + "_" + str1
            //        }
            //    }

            //}




            #endregion PERMUTATION  FOR VALUES

            #region PERMUTATION FOR NAMES

            string[] permutationname = new string[parametercountforpermutation];
            int permutationnamecount = 0;
            if (permutationnamecount.ConvertToString() != "")
            {
                if (objReport.Gender_CD.ConvertToString() != "")
                {
                    if (totalparameterscount == 1)
                    {
                        permutationname[permutationnamecount] = gendername;
                    }
                }
                if (objReport.BuildingCondition.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = building_condition_name;
                    permutationnamecount++;
                }
                if (objReport.Technical_soln_CD.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = technicalsolnnames;
                    permutationnamecount++;
                }
                if (objReport.Damage_Grade.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = damagegradenames;
                    permutationnamecount++;
                }
                if (objReport.SuperStruct_CD.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = superstructnnames;
                    permutationnamecount++;
                }
                if (objReport.HouseLegalOwner.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = house_legal_owner_name;
                    permutationnamecount++;
                }
                if (objReport.Education.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = educationnames;
                    permutationnamecount++;
                }
                if (objReport.MonthlyIncome.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = monthlyincomenames;
                    permutationnamecount++;
                }
                if (objReport.Marital_Status.ConvertToString() != "")
                {
                    permutationname[permutationnamecount] = maritalstatusnames;
                    permutationnamecount++;
                }

            }


            IEnumerable<string> namerow = permutationname;
            IEnumerable<string> namepermutations = GetPermutations(namerow, delimiter: "_");

            //namepermutations = GetPermutations(namerow, delimiter: "_");

            List<string> finalnamepermutations = new List<string>();
            List<string> finalnamepermutationspairsval = new List<string>();
            if (objReport.Gender_CD.ConvertToString() != "")
            {
                string[] gendetstr = gendername.ConvertToString().Split(',');
                foreach (string str1 in namepermutations)
                {
                    foreach (string str in gendetstr)
                    {
                        if (str != "")
                            finalnamepermutations.Add(str + "_" + str1);
                    }
                }

            }







            #endregion PERMUTATION  FOR NAMES


            #region FINAL PARAMETER COMBINATION

            List<string> listofinparameters = new List<string>();
            if (totalparameterscount > 1)
            {
                if (objReport.Gender_CD.ConvertToString() != "" && objReport.Gender_CD.ConvertToString().Split(',').Count() > 1 || objReport.Gender_CD.ConvertToString().Count() == 1)
                {
                    namepermutations = finalnamepermutations;
                }
            }
            if (totalparameterscount >= 1)
            {
                if (objReport.Gender_CD.ConvertToString() != "" && totalparameterscount > 1 && (objReport.Gender_CD.ConvertToString().Split(',').Count() > 1 || objReport.Gender_CD.ConvertToString().Count() == 1))
                {

                    for (int i = 0; i < finalnamepermutations.Count(); i++)
                    {
                        listofinparameters.Add("'" + finallistforintvalues.ToList()[i].ToString() + "'" + " AS " + finalnamepermutations.ToList()[i].ToString());
                    }
                }

                else
                {
                    for (int i = 0; i < permutations.Count(); i++)
                    {
                        listofinparameters.Add("'" + permutations.ToList()[i].ToString() + "'" + " AS " + namepermutations.ToList()[i].ToString());
                    }
                }
            }


            #endregion FINAL PARAMETER COMBINATION

            #region SUM AND INNERSELCT PART

            var totalinparameters = string.Join(",", listofinparameters);
            List<string> Sumlist = new List<string>();

            if (objReport.Gender_CD.ConvertToString() != "" && totalparameterscount > 1 && (objReport.Gender_CD.ConvertToString().Split(',').Count() > 1 || objReport.Gender_CD.ConvertToString().Count() == 1))
            {
                for (int i = 0; i < finalnamepermutations.Count(); i++)
                {
                    Sumlist.Add("SUM" + "(" + finalnamepermutations.ToList()[i].ToString() + ")" + " AS " + finalnamepermutations.ToList()[i].ToString());
                }

            }
            else
            {
                for (int i = 0; i < permutations.Count(); i++)
                {
                    Sumlist.Add("SUM" + "(" + namepermutations.ToList()[i].ToString() + ")" + " AS " + namepermutations.ToList()[i].ToString());
                }
            }
            var totalsumlist = string.Join(",", Sumlist);
            string columnnameuse = "";
            if (namepermutations.Count() >= 1)
            {
                columnnameuse = namepermutations.ToList()[0].ToString();

            }




            //string columnnameuse = finalnamepermutations.ToList()[0].ToString();
            string innerselectvaluefordisvdcward = "";
            if (objReport.District_CD.ConvertToString() == "")
            {
                innerselectvaluefordisvdcward = " MD.DESC_ENG DISTRICT,";
            }
            if (objReport.District_CD.ConvertToString() != "")
            {
                innerselectvaluefordisvdcward = " MD.DESC_ENG  DISTRICT,mvm.DESC_ENG vdc_mun,";
             
                if (objReport.VDC_MUN_CD.ConvertToString() != "")
                {
                    if(objReport.BeneficiaryType.ConvertToString()=="Grievance")
                    {
                        innerselectvaluefordisvdcward = innerselectvaluefordisvdcward + " ngem.ward_no,";

                    }
                    else 
                    { 
                    innerselectvaluefordisvdcward = innerselectvaluefordisvdcward + " NHOM.ward_no,";
                    }
                }
            }
            #endregion SUM AND INNERSELCT PART
            #region MERGING THE ALL QUERY

            string houseownermsttable = " NHOM.HOUSE_OWNER_ID FROM NHRS_HOUSE_OWNER_MST  nhom ";
            string grievanceenrollmsttable = " ngem.HOUSE_OWNER_ID FROM NHRS_GRIEVANCE_ENROLL_MST ngem ";

            if (columnnameuse != "" && totalinparameters != "")
            {
                totalinnerselect = concat + bracketvalue;
                if (objReport.BeneficiaryType.ConvertToString() == "Grievance")
                {
                    newcmdText = newcmdText + totalsumlist + " from ( select * from ( Select " + innerselectvaluefordisvdcward + totalinnerselect + " AS " + columnnameuse + "," + grievanceenrollmsttable + InnerJOin + whereconditions + ")";

                }
                else {
                    newcmdText = newcmdText + totalsumlist + " from ( select * from ( Select " + innerselectvaluefordisvdcward + totalinnerselect + " AS " + columnnameuse + "," + houseownermsttable + InnerJOin + whereconditions + ")";
                }
                newcmdText = newcmdText + "PIVOT(COUNT(HOUSE_OWNER_ID) FOR " + columnnameuse + " IN " + "(" + totalinparameters + ")))";
            }

            else
            {
                if (objReport.DeathCount == "Y")
                {
                    newcmdText = " Select " + innerselectvaluefordisvdcward + " sum(mhi.DEATH_CNT) NOOFCOUNT FROM MIS_HOUSEHOLD_INFO mhi   INNER JOIN MIS_DISTRICT md ON mhi.CUR_DISTRICT_CD = md.DISTRICT_CD   " + whereconditions + "";
                }
                else if (objReport.RespondentisHouseowner == "Y")
                {
                    newcmdText = " Select " + innerselectvaluefordisvdcward + "COUNT(DISTINCT NHOM.HOUSE_OWNER_ID) NOOFCOUNT FROM NHRS_HOUSE_OWNER_MST  nhom  INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD    INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD   " + whereconditions + "";
                }
                else if (objReport.StoryCountBeforeEarthQuake == "Y" || objReport.StoryCountAfterEarthQuake == "Y" || objReport.MemberCountAfterEarthQuake == "Y")
                {
                    newcmdText = " Select " + innerselectvaluefordisvdcward + "COUNT(NHOM.HOUSE_OWNER_ID) NOOFCOUNT FROM NHRS_HOUSE_OWNER_MST  nhom    INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD   INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD  INNER JOIN NHRS_BUILDING_ASSESSMENT_MST nbam On nhom.HOUSE_OWNER_ID = nbam.HOUSE_OWNER_ID " + whereconditions + "";
                }
                else if (objReport.Beneficiary.ConvertToString()!="")
                {
                    if(objReport.Beneficiary.ConvertToString()=="EL")
                    {
                      
                    if (objReport.BeneficiaryType == "Retro")
                    {
                        newcmdText = " Select " + innerselectvaluefordisvdcward + "COUNT(NHOM.HOUSE_OWNER_ID) NOOFCOUNT FROM NHRS_HOUSE_OWNER_MST  nhom     INNER JOIN NHRS_RETRO_ENROLL_MST nrem on nhom.HOUSE_OWNER_ID = nrem.HOUSE_OWNER_ID   INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD    INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD    INNER JOIN  NHRS_HOUSE_OWNER_DTL nhod ON nhom.HOUSE_OWNER_ID = nhod.HOUSE_OWNER_ID " + whereconditions + "";
                    }

                    if (objReport.BeneficiaryType == "Reconstruction")
                    {
                        newcmdText = " Select " + innerselectvaluefordisvdcward + "COUNT(NHOM.HOUSE_OWNER_ID) NOOFCOUNT FROM NHRS_HOUSE_OWNER_MST  nhom   INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD   INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD  inner JOIN NHRS_ENROLLMENT_MST nem on nhom.HOUSE_OWNER_ID = nem.HOUSE_OWNER_ID   inner join NHRS_HOUSE_OWNER_DTL nhod ON nhom.HOUSE_OWNER_ID = nhod.HOUSE_OWNER_ID " + whereconditions + "";
                    }
                    if (objReport.BeneficiaryType == "Grievance")
                    {
                        newcmdText = " Select " + innerselectvaluefordisvdcward + " COUNT(ngem.HOUSE_OWNER_ID) AS  NOOFCOUNT   FROM NHRS_GRIEVANCE_ENROLL_MST ngem   INNER JOIN MIS_DISTRICT md ON ngem.DISTRICT_CD = md.DISTRICT_CD     INNER JOIN MIS_VDC_MUNICIPALITY mvm ON ngem.VDC_MUN_CD = mvm.VDC_MUN_CD    INNER JOIN  NHRS_HOUSE_OWNER_DTL nhod ON ngem.HOUSE_OWNER_ID = nhod.HOUSE_OWNER_ID " + whereconditions + "";

                    }
                          if (objReport.BeneficiaryType == "All")
                        {
                            string districtcondition = String.Empty;
                            string selectvalue = String.Empty;
                            string benefvdcselect = String.Empty;
                            string innerjoinvdc = String.Empty;
                            string vdcconditionforreconstruct = String.Empty;
                            string vdcconditionforretro = String.Empty;
                            string vdcconditionforgrievance = String.Empty;

                            string groupbyvdc = String.Empty;
                            string orderbyvdc = String.Empty;
                            string grpndordrforgrie = String.Empty;
                            string onconditionforretro = String.Empty;
                            string onconditionforgrievance = String.Empty;

                              if(objReport.District_CD.ConvertToString()!="")
                              {
                                  districtcondition = " AND md.DISTRICT_CD=" + objReport.District_CD;
                                  selectvalue = "A.vdc_mun,";
                                  benefvdcselect = "mvm.VDC_MUN_CD, mvm.DESC_ENG vdc_mun,";
                                  vdcconditionforreconstruct = " INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nem.VDC_MUN_CD = mvm.VDC_MUN_CD ";
                                  vdcconditionforretro = "   INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nrem.VDC_MUN_CD = mvm.VDC_MUN_CD ";
                                  vdcconditionforgrievance = " INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD ";

                                  groupbyvdc = ",mvm.VDC_MUN_CD, mvm.DESC_ENG ";
                                  orderbyvdc = " order by md.DISTRICT_CD, mvm.DESC_ENG ";
                                  onconditionforretro = " and A.VDC_MUN_CD=B.VDC_MUN_CD ";
                                  onconditionforgrievance = " and A.VDC_MUN_CD=c.VDC_MUN_CD ";
                              }
                              else
                              {
                                  districtcondition = " ";
                              }

                              if(objReport.VDC_MUN_CD.ConvertToString()!="")
                              {
                                  selectvalue = selectvalue + "A.ward_no,";
                                  benefvdcselect = benefvdcselect + " nhom.ward_no,";
                                  districtcondition = districtcondition + " AND mvm.VDC_MUN_CD=" + objReport.VDC_MUN_CD;
                                  groupbyvdc = groupbyvdc + " ,nhom.ward_no";
                                  orderbyvdc = orderbyvdc + ",nhom.ward_no ";
                                  onconditionforretro = onconditionforretro + " and A.ward_no=B.ward_no ";
                                  onconditionforgrievance = onconditionforgrievance + " and A.ward_no=C.WARD_NO  ORDER BY A.WARD_NO ";
                                  
                              }

                              if(objReport.Ward.ConvertToString()!="")
                              {
                                  districtcondition = districtcondition + " AND nhom.WARD_NO=" + objReport.Ward;
                              }
                              


                       string selectbenef="SELECT   A.district,"+selectvalue+" A.reconstruct, B.retro, C.grievance FROM ";
                       string reconstruct = " ( SELECT   md.DISTRICT_CD,MD.desc_eng district," + benefvdcselect + "COUNT(*) reconstruct    from NHRS_HOUSE_OWNER_MST nhom inner JOIN NHRS_ENROLLMENT_MST nem on nhom.HOUSE_OWNER_ID = nem.HOUSE_OWNER_ID INNER JOIN MIS_DISTRICT md ON nem.DISTRICT_CD = md.DISTRICT_CD " + vdcconditionforreconstruct + " and nhom.TARGET_FLAG='EL' AND nem.NRA_DEFINED_CD is not NULL " + districtcondition + " GROUP by md.DISTRICT_CD,MD.desc_eng" + groupbyvdc + orderbyvdc + ")A";
                       string retro = " LEFT OUTER JOIN (Select md.DISTRICT_CD," + benefvdcselect + "COUNT(*) retro FROM NHRS_HOUSE_OWNER_MST nhom INNER JOIN NHRS_RETRO_ENROLL_MST nrem on nhom.HOUSE_OWNER_ID = nrem.HOUSE_OWNER_ID inner join MIS_DISTRICT md ON nrem.DISTRICT_CD = md.DISTRICT_CD " + vdcconditionforretro + " where nhom.RETROFITING_TARGET_FLAG='EL' AND nrem.NRA_DEFINED_CD is not NULL " + districtcondition + " GROUP by md.DISTRICT_CD,MD.desc_eng" + groupbyvdc + orderbyvdc + ")B ON A.DISTRICT_CD=B.DISTRICT_CD" + onconditionforretro + " ";
                       string grievance = " LEFT OUTER JOIN   (SELECT md.DISTRICT_CD," + benefvdcselect + "COUNT(*) grievance from NHRS_GRIEVANCE_ENROLL_MST nhom  inner join MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD " + vdcconditionforgrievance + districtcondition + " GROUP by md.DISTRICT_CD,MD.DESC_ENG" + groupbyvdc + orderbyvdc + ") C ON A.DISTRICT_CD=C.DISTRICT_CD" + onconditionforgrievance;
                    newcmdText = "";
                    newcmdText = selectbenef + reconstruct + retro + grievance;
                        }
                    }
                    else
                    {
                        newcmdText = " Select " + innerselectvaluefordisvdcward + "COUNT(NHOM.HOUSE_OWNER_ID) NOOFCOUNT FROM NHRS_HOUSE_OWNER_MST  nhom   INNER JOIN MIS_DISTRICT md ON nhom.DISTRICT_CD = md.DISTRICT_CD   INNER JOIN MIS_VDC_MUNICIPALITY mvm ON nhom.VDC_MUN_CD = mvm.VDC_MUN_CD " + whereconditions + "";

                    }

                    if(objReport.Marital_Status.ConvertToString()!="")
                    {

                    }
                }
                else
                {
                    newcmdText = " Select " + innerselectvaluefordisvdcward + "COUNT(NHOM.HOUSE_OWNER_ID) NOOFCOUNT FROM NHRS_HOUSE_OWNER_MST  nhom " + InnerJOin + "  " + whereconditions + "";
                }
            }


            #endregion   MERGING THE ALL QUERY

            #region ORDER BY & GROUP BY
            string groupby = "";
            if (columnnameuse != "" && totalinparameters != "")
            {
                if (objReport.District_CD.ConvertToString() == "")
                {
                    groupby = "t GROUP BY DISTRICT ORDER BY DISTRICT ";
                }
                if (objReport.District_CD.ConvertToString() != "")
                {
                    groupby = "t GROUP BY DISTRICT,VDC_MUN ORDER BY DISTRICT,VDC_MUN";
                    if (objReport.VDC_MUN_CD.ConvertToString() != "")
                    {
                        groupby = "t GROUP BY DISTRICT,VDC_MUN,WARD_NO ORDER BY DISTRICT,VDC_MUN,WARD_NO";
                    }
                }

            }
            else
            {
                if (objReport.District_CD.ConvertToString() == "")
                {
                    groupby = " GROUP BY MD.DESC_ENG order by md.desc_eng";
                }
                if (objReport.District_CD.ConvertToString() != "" && objReport.VDC_MUN_CD.ConvertToString()=="")
                {
                    groupby = " GROUP BY MD.DESC_ENG,mvm.DESC_ENG order by mvm.DESC_ENG ";
                    if (objReport.VDC_MUN_CD.ConvertToString() != "")
                    {
                       
                    }
                }
                if (objReport.District_CD.ConvertToString() != "" && objReport.VDC_MUN_CD.ConvertToString()!="")
                 if(objReport.BeneficiaryType.ConvertToString()=="Grievance")
                        {
                            groupby = groupby + "GROUP BY MD.DESC_ENG,mvm.DESC_ENG,ngem.WARD_NO order by ngem.WARD_NO";

                        }
                 else 
                 { 
                   groupby = groupby + " GROUP BY MD.DESC_ENG,mvm.DESC_ENG,nhom.WARD_NO order by nhom.WARD_NO";
                 }

                if (objReport.BeneficiaryType.ConvertToString() == "All")
                {
                    groupby = " ORDER BY A.district";

                }
              
            }



            #endregion ORDER BY

            #endregion QUERY END
            string finalquery = newcmdText + groupby;



            dt = service.SearchResults(finalquery);
            ViewData["dtsearchresult"] = dt;
            //ViewData["namepermutations"] = namepermutations;
            //Session["namepermutations"] = namepermutations;

            ViewData["namepermutations"] = namepermutations;

            Session["namepermutations"] = namepermutations;

            Session["dtsearchresult"] = dt;
            Session["rptparams"] = objReport;
            return RedirectToAction("Getresult", objReport);
        }

        public ActionResult Getresult(Adhoc_ReportSearchEntity objReport)
        {
            List<Adhoc_ReportSearchEntity> lstCheckedTrainer = new List<Adhoc_ReportSearchEntity>();

            NameValueCollection paramValues = new NameValueCollection();

            DataTable dt = null;
            dt = (DataTable)Session["dtsearchresult"];
            List<string> namepermutations = Session["namepermutations"] as List<string>;

            ViewData["namepermutations"] = namepermutations;

            ViewData["dtsearchresult"] = dt;

            ViewBag.ReportTitle = "Adhoc Report";

            objReport.District_CD = cm.GetDescriptionFromCode(objReport.District_CD.ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objReport.VDC_MUN_CD = cm.GetDescriptionFromCode(objReport.VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");


            return View(objReport);
        }


        public ActionResult checkboxtest()
        {
            var gndr = cm.GetGender("");
            ViewData["ddl_Gender"] = new MultiSelectList(gndr, "Id", "Name");
            return View();
        }

        public IEnumerable<string> GetPermutations(IEnumerable<string> possibleCombos, string delimiter)
        {

            var permutations = new Dictionary<int, List<string>>();
            var comboArray = possibleCombos.ToArray();
            var splitCharArr = new char[] { ',' };

            permutations[0] = new List<string>();
            if (possibleCombos.Count() >= 1)
            //int index = Array.IndexOf(comboArray,);
            // if(comboArray ==null)
            {

                permutations[0].AddRange(
                    possibleCombos
                    .First()
                    .Split(splitCharArr)
                    .Where(x => !string.IsNullOrEmpty(x.Trim()))
                    .Select(x => x.Trim()));
            }
            for (int i = 1; i < comboArray.Length; i++)
            {
                permutations[i] = new List<string>();
                foreach (var permutation in permutations[i - 1])
                {
                    permutations[i].AddRange(
                        comboArray[i].Split(splitCharArr)
                        .Where(x => !string.IsNullOrEmpty(x.Trim()))
                        .Select(x => string.Format("{0}{1}{2}", permutation, delimiter, x.Trim()))
                        );


                }


            }

            return permutations[permutations.Keys.Max()];
        }

        [HttpPost]
        public ActionResult ExportExcel()
        {
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;
            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "AdhocResult" + usercd + ".xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "AdhocResult.xls";
            }
            string html = RenderPartialViewToString("~/Views/Adhoc/_AdhocSearchResult.cshtml");
            html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Adhoc Result"), html);
            html = ReportTemplate.GetReportHTML(html);
            Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "AdhocResult.xls");
        }
        protected string RenderPartialViewToString(string viewName)
        {
            DataTable dtbl = new DataTable();
            Adhoc_ReportSearchEntity rptParams = new Adhoc_ReportSearchEntity();
            List<string> namepermutations = Session["namepermutations"] as List<string>;


            if ((DataTable)Session["dtsearchresult"] != null)
            {
                DataTable dt = (DataTable)Session["dtsearchresult"];
                ViewData["dtsearchresult"] = dt;
                ViewData["namepermutations"] = namepermutations;

                rptParams = (Adhoc_ReportSearchEntity)Session["rptparams"];
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

    }
}
