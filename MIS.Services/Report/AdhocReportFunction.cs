using EntityFramework;
using ExceptionHandler;
using MIS.Models.Core;
using MIS.Models.Report;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MIS.Services.Report
{ 
    public class AdhocReportSummaryService
    {
        CommonService cs = null;
          List<MISCommon> lstCommon = null;
        public DataTable GetOwnerAdhocReport(AdhocHouseOwnerReportModel objmodel)
        {
            DataTable dt = new DataTable();
            Object sessionid = DBNull.Value;
            Object District = DBNull.Value;
            Object VDC = DBNull.Value;
            Object Ward = DBNull.Value;
            Object OwnerCount_Indicator = DBNull.Value;
            Object OwnerCount = DBNull.Value;
            Object Gender = DBNull.Value;
            Object Respondedflag = DBNull.Value;
            Object housewithinEC_Indicator = DBNull.Value;
            Object HousewithinEC = DBNull.Value;
            Object HouseOutOfEC = DBNull.Value;
            Object HouseOutOFEC_Indicator = DBNull.Value;
            Object PhoneNumberFlag = DBNull.Value;
            Object GrievanceRegisteredFlag = DBNull.Value;
            Object BeneficiaryFlag = DBNull.Value;
            Object BeneficiaryType = DBNull.Value;

            if (objmodel.Districtcd != "")
            {
                District = objmodel.Districtcd;
            }
            if (objmodel.VDCMun != "")
            {
                VDC = objmodel.VDCMun;
            }
            if (objmodel.Ward != "")
            {
                Ward = objmodel.Ward;
            }
            if (objmodel.ownercount_Indicator != "")
            {
                OwnerCount_Indicator = objmodel.ownercount_Indicator;
            }
            if (objmodel.ownercount != "")
            {
                OwnerCount = objmodel.ownercount;
            }
            //if (objmodel.gender != "")
            //{
            //    Gender = objmodel.gender;
            //}
            //if (objmodel.isrespondend != "")
            //{
            //    Respondedflag = objmodel.isrespondend;
            //}
            if (objmodel.housewithinEC_Inidcator != "")
            {
                housewithinEC_Indicator = objmodel.housewithinEC_Inidcator;
            }
            if (objmodel.housewithinEC != "")
            {
                HousewithinEC = objmodel.housewithinEC;
            }
            if (objmodel.houseoutofEC != "")
            {
                HouseOutOfEC = objmodel.houseoutofEC;
            } 
            if (objmodel.houseoutofEC_Indicator != "")
            {
                HouseOutOFEC_Indicator = objmodel.houseoutofEC_Indicator;
            }
            //if (objmodel.phonenumberflag != "")
            //{
            //    PhoneNumberFlag = objmodel.phonenumberflag;
            //}
            //if (objmodel.grievanceRegisteredFlag != "")
            //{
            //    GrievanceRegisteredFlag = objmodel.grievanceRegisteredFlag;
            //}
            //if (objmodel.phonenumberflag != "")
            //{
            //    PhoneNumberFlag = objmodel.phonenumberflag;
            //}
            //if (objmodel.BeneficiaryFlaG != "")
            //{
            //    BeneficiaryFlag = objmodel.BeneficiaryFlaG;
            //}
            //if (objmodel.BeneficiaryType != "")
            //{
            //    BeneficiaryType = objmodel.BeneficiaryType;
            //}
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";

                    //dt = service.GetDataTable(true, "PR_NHRS_HOUSEHOLD_SEARCH",
                    //    sessionid,
                       // memberid,
                       // householdid,
                       // houseownerid,
                       // ho_defined_code,
                       // instance_unique_sno,
                       // BuildingStructureNo,
                       // definedcd,
                       // // firstnameeng, 
                       // // middlenameeng,
                       // //lastnameeng, 
                       //fullnameeng,
                       //fullnameloc,
                       // //firstnameloc, 
                       //headgendercd,
                       // //middlenameloc, 
                       // //lastnameloc,  
                       // //RFnameEng, 
                       // //RMnameEng,
                       // //RLnameEng, 
                       //RFullnameEng,
                       // //RFnameLoc, 
                       // //RMnameLoc, 
                       // //RLnameLoc, 
                       //RFullnameLoc,
                       //perdistrictcd,
                       //pervdcmunCd,
                       // //pervdcmun,
                       //perwardno,
                       //perAreaEng,
                       //perAreaLoc,
                       //houseNo,
                       //curDistrict,
                       //curVDCMun,
                       //curWardNo,
                       //curAreaEng,
                       //curAreaLoc,
                       //ShelterSinceQuake,
                       //ShelterBeforeQuake,
                       //CurrentShelter,
                       //EQVictimIDCard,
                       //MonthlyIncomeCd,
                       //DeathCntfrom,
                       //DeathCntTo,
                       //HumanDestroyCntFrom,
                       //HumanDestroyCntTo,
                       //SchoolStudentLeftCnt,
                       //PregnantCheckupCnt,
                       //ChildleftVaccinationCnt,
                       //LeftChangeOccupancyCnt,
                       //FuelSource,
                       //LightSource,
                       //EQreliefFund,
                       //ToiletType,
                       //WaterSource,
                       //MobileNo,
                       //BankAccount,
                       //EnteredBy,
                       //SortBy,
                       //SortOrder,
                       //PageSize,
                       //PageIndex,
                       //DBNull.Value);

                }

                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_ENG"].ConvertToString() != "" ? dr["PER_DISTRICT_ENG"].ConvertToString() : ""), (dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_LOC"].ConvertToString() != "" ? dr["PER_DISTRICT_LOC"].ConvertToString() : ""));
                }
            }
            return dt;

        }

        public DataTable GetStructureAdhocReport(AdhocHouseStructureReport objmodel)
        {
            DataTable dt = new DataTable();
            Object sessionid = DBNull.Value;
            Object District = DBNull.Value;
            Object VDC = DBNull.Value;
            Object Ward = DBNull.Value;
            if (objmodel.Districtcd != "")
            {
                District = objmodel.Districtcd;
            }
            if (objmodel.VDCMun != "")
            {
                VDC = objmodel.VDCMun;
            }
            if (objmodel.Ward != "")
            {
                Ward = objmodel.Ward;
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";

                }

                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_ENG"].ConvertToString() != "" ? dr["PER_DISTRICT_ENG"].ConvertToString() : ""), (dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_LOC"].ConvertToString() != "" ? dr["PER_DISTRICT_LOC"].ConvertToString() : ""));
                }
            }
            return dt;
        }

        public DataTable GetDetailAdhocReport(AdhocHouseDetailReportModel objmodel)
        {
            DataTable dt = new DataTable();
            Object sessionid = DBNull.Value;
            Object District = DBNull.Value;
            Object VDC = DBNull.Value;
            Object Ward = DBNull.Value;
            if (objmodel.Districtcd != "")
            {
                District = objmodel.Districtcd;
            }
            if (objmodel.VDCMun != "")
            {
                VDC = objmodel.VDCMun;
            }
            if (objmodel.Ward != "")
            {
                Ward = objmodel.Ward;
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_SEARCH";

                }

                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionHandler.ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("ADDRESS");
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_ENG"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_ENG"].ConvertToString() != "" ? dr["PER_DISTRICT_ENG"].ConvertToString() : ""), (dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() != "" ? dr["PER_VDCMUNICIPILITY_LOC"].ConvertToString() + ", " : "") + (dr["PER_WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["PER_WARD_NO"].ConvertToString()) + ", " : "") + (dr["PER_DISTRICT_LOC"].ConvertToString() != "" ? dr["PER_DISTRICT_LOC"].ConvertToString() : ""));
                }
            }
            return dt;
        }

        public static JsonResult GetYesNo(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();

            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.GetLabel("SelectAll");
            liii.Value = "null";
            if (liii.Value == selectdYesNo)
                liii.Selected = true;
            liSelect.Add(liii);

            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.GetLabel("No");
            lii.Value = "N";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("Yes");
            li.Value = "Y";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);

            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public static List<SelectListItem> GetAdhocGender()
        {
            List<SelectListItem> selLstGender = new List<SelectListItem>();
            selLstGender.AddRange(GetData.AllGenders(Utils.ToggleLanguage("english", "nepali")));
            selLstGender = GetData.GetSelectedList(selLstGender, "");
            return selLstGender;
        }

        public static List<SelectListItem> LegalOwner(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetHouseLegalOwner(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select House Legal Owner") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
     
        public static List<SelectListItem> GetBuildingCondition(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetBuildingCondition(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Building Condition") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public static List<SelectListItem> getFoundationType(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetFoundationType(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Foundation Type") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public static List<SelectListItem> GetRoofType(string selectedValue)
        {
            List<SelectListItem> selLstRoofType = new List<SelectListItem>();
            //selLstRoofType.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Roof Construction Type") + "---" });
            List<MISCommon> lstRelType = AdhocCommonService.GetRoofTypeByCode("", "");
            foreach (MISCommon relType in lstRelType)
            {
                selLstRoofType.Add(new SelectListItem { Value = relType.DefinedCode, Text = relType.Description });
            }
            foreach (SelectListItem item in selLstRoofType)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstRoofType;
        }
        public static List<SelectListItem> GetFloorMaterial(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetFloorConstructionMaterial(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Floor Material") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }

        //Building material
        public static List<SelectListItem> getBuildingMaterial(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetBuildingmaterial(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Storey Material") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        //Building Position
        public static List<SelectListItem> getBuildingPosition(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetBuildingPosition(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Building Position") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }

        //Building Plan
        public static List<SelectListItem> getPlanConfiguration(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetBuildinPlanConfig(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Plan Configuration") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }

        //Asssess area
        public static List<SelectListItem> GetInspectedHousepart(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetBuildingAssessedArea(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Monitored Part") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public static List<SelectListItem> GetDamageGrade(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetDamageGrade(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Damage Grade") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public static List<SelectListItem> GetTechnicalSolution(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetTechnicalSolution(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Tech Solution") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public static List<SelectListItem> GetDocumentType(string selectedValue)
        {
            List<SelectListItem> selLstDocument = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetDocumentTypebyCodeandDesc("", "");
            //selLstDocument.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Document Type") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                selLstDocument.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in selLstDocument)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstDocument;
        }
        private static List<SelectListItem> GetSelectedList(List<SelectListItem> list, string selectedValue)
        {
            var j = list.Where(x => x.Selected);
            if (selectedValue != null && selectedValue != "")
                ((SelectListItem)(list.Where(s => s.Value == selectedValue).First())).Selected = true;
            return list;
        }
        public static List<SelectListItem> GetDistrictsForUser(string selectedValue, bool ispopup = false)
        {
            string DistDefCd = HttpContext.Current.Session["UserDistrictDefCd"].ConvertToString();
            string UserGroupFlag = HttpContext.Current.Session["UserGroupFlag"].ConvertToString();
            string Zonencd = HttpContext.Current.Session["UserZoneCD"].ConvertToString();
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (ispopup == false && UserGroupFlag == "C")
            {
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            }


            if (UserGroupFlag == "V" || UserGroupFlag == "D")
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts = (selLstDistricts.Where(d => d.Value == DistDefCd)).ToList();
            }
            else if (UserGroupFlag == "C")
            {
                //selLstDistricts.AddRange(GetData.DistrictbyZone(Zonencd, Utils.ToggleLanguage("english", "nepali")).ToList());
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")).ToList());
            }

            selLstDistricts = GetSelectedList(selLstDistricts, selectedValue).ToList();

            return selLstDistricts;
        }

        public static List<SelectListItem> getEthnicity(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> EthnicityList = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.GetAllEthnicity(code, desc);
            //EthnicityList.Add(new SelectListItem { Value = "", Text = "---" + Utils.ToggleLanguage("Select Ethnicity", "जाति समूह छान्नुहोस्") + "---" });
            foreach (MISCommon common in lstCommon)
            {
                EthnicityList.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }
            foreach (SelectListItem item in EthnicityList)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }

            return EthnicityList;
        }
        public static List<SelectListItem> GetEducation(string selectedValue)
        {
            List<SelectListItem> selLstEducation = new List<SelectListItem>();
            //selLstEducation.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Education") + "---" });
            selLstEducation.AddRange(GetData.AllEducations(Utils.ToggleLanguage("english", "nepali")));
            selLstEducation = GetData.GetSelectedList(selLstEducation, selectedValue);
            return selLstEducation;
        }
        public static List<SelectListItem> GetMaritalStatus(string selectedValue)
        {
            List<SelectListItem> selLstMarried = new List<SelectListItem>();
            //selLstMarried.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Marital Status") + "---" });
            selLstMarried.AddRange(GetData.AllMaritalStatus(Utils.ToggleLanguage("english", "nepali")));
            selLstMarried = GetData.GetSelectedList(selLstMarried, selectedValue);
            return selLstMarried;
        }

        public static JsonResult GetYesNo1(string selectdYesNo)
        {
            List<SelectListItem> liSelect = new List<SelectListItem>();
            SelectListItem liii = new SelectListItem();
            liii.Text = Utils.ToggleLanguage("All", "सबै");
            liii.Value = "";
            liSelect.Add(liii);
            SelectListItem li = new SelectListItem();
            li.Text = Utils.ToggleLanguage("No", "छैन");
            li.Value = "N";
            if (li.Value == selectdYesNo)
                li.Selected = true;
            liSelect.Add(li);
            SelectListItem lii = new SelectListItem();
            lii.Text = Utils.ToggleLanguage("Yes", "छ");
            lii.Value = "Y";
            if (lii.Value == selectdYesNo)
                lii.Selected = true;
            liSelect.Add(lii);

            SelectListItem liiii = new SelectListItem();
            liiii.Text = Utils.ToggleLanguage("None", "कुनै पनि");
            liiii.Value = "Nn";
            if (liiii.Value == selectdYesNo)
                liiii.Selected = true;
            liSelect.Add(liiii);
            return new JsonResult { Data = liSelect, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //shelter after earthquake
        public static List<SelectListItem> ShelterAfterEq(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.getShelterAfterEq(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }

        public static List<SelectListItem> ShelterBeforeEq(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.getShelterBeforeEq(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
        public static List<SelectListItem> GetMonthlyIncome(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.getMonthlyIncome(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }
            public static List<SelectListItem> GetSuperStructure(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            List<MISCommon> lstCommon = AdhocCommonService.getSuperStructure(code, desc);
            //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Shelter") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstTargetId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstTargetId;
        }

            //public static List<SelectListItem> GetMaritalStatuss(string selectedValue, string code = "", string desc = "")
            //{
            //    List<SelectListItem> selLstTargetId = new List<SelectListItem>();
            //    List<MISCommon> lstCommon = AdhocCommonService.GetMaritalStatus(code, desc);
            //    //selLstTargetId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Tech Solution") + "---" });
            //    foreach (MISCommon common in lstCommon)
            //    {

            //        selLstTargetId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            //    }

            //    foreach (SelectListItem item in selLstTargetId)
            //    {
            //        if (item.Value == selectedValue)
            //            item.Selected = true;
            //    }
            //    return selLstTargetId;
            //}
    }
}
