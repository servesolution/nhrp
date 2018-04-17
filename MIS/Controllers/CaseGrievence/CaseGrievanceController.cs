
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Data;
using System.Data.SqlClient;
using MIS.Models.Core;
using ExceptionHandler;
using System.Web.Routing;
using MIS.Authentication;
using System.Text;
using EntityFramework;
using MIS.Services.BusinessCalculation;
using MIS.Models.Security;
using MIS.Models.CaseGrievance;
using MIS.Services.CaseGrievance;
using MIS.Services.Registration.Household;
using MIS.Models.Setup;
using MIS.Models.CaseGrievence;
using System.Data.OracleClient;
using MIS.Models.NHRP;

namespace MIS.Controllers.CaseGrievance
{
    public class CaseGrievanceController : BaseController
    {

        CommonFunction commonFC = new CommonFunction();
        CommonController common = new CommonController();
        HouseholdService househead = new HouseholdService();
        CaseGrievanceModel objCase = new CaseGrievanceModel();
        CaseGrievanceService objCaseGrievanceService = new CaseGrievanceService();
        public ActionResult ListCaseGrievance(string p)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();
            CaseGrievanceModel cgm = new CaseGrievanceModel();

            ViewData["ddl_Category"] = new List<SelectListItem>();
            if (CommonVariables.GroupCD == "42")
            {
                if (CommonVariables.EmpCode != "")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_District"] = commonFC.GetDistrictsByDistrictCode(Districtcode);
                    cgm.DistrictCd = commonFC.GetDefinedCodeFromDataBase(cgm.VDCMun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, Districtcode);
                    cgm.DistrictCd = Districtcode.ConvertToString();
                }
                else
                {
                    ViewData["ddl_District"] = commonFC.GetDistricts(cgm.DistrictCd.ConvertToString());
                    cgm.VDCMun = commonFC.GetDefinedCodeFromDataBase(cgm.VDCMun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, cgm.DistrictCd.ConvertToString());
                }
            }
            else
            {
                ViewData["ddl_District"] = commonFC.GetDistricts(cgm.DistrictCd.ConvertToString());
                ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, cgm.DistrictCd.ConvertToString());
            }
            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");
            ViewData["ddl_CaseGrievanceType"] = commonFC.getcaseGrievanceType("");
            return View("ListCaseGrievance", cgm);
        }

        [HttpPost]
        public ActionResult ListCaseGrievance(FormCollection fc)
        {
            CaseGrievanceModel model = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            model.DistrictCd = fc["DistrictCd"].ConvertToString();
            model.VDCMun = commonFC.GetCodeFromDataBase(fc["VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            model.Ward = fc["Ward"].ConvertToString();
            model.fullName = fc["fullName"].ConvertToString();
            model.CASE_REGISTRATION_ID = fc["CASE_REGISTRATION_ID"].ConvertToString();
            model.REGISTRATION_NO = fc["REGISTRATION_NO"].ConvertToString();
            model.FORM_NO = fc["FORM_NO"].ConvertToString();
            model.CASE_STATUS = fc["caseStatus"].ConvertToString();
            model.Nissa_NO = fc["Nissa_NO"].ConvertToString();
            model.RETRO_FLAG = fc["ddlRetroFlag"].ConvertToString(); 
            string CaseType =fc["ddl_CaseGrievanceType"].ConvertToString();
            DataTable dtResults = objService.GetCaseGrievanceSearch(model, CaseType);
            Session["Param"] = model;
            Session["CaseType"] = CaseType;
            ViewData["result"] = dtResults;
            return PartialView("_CaseGrievienceList");
        }
        //edit case
        [HttpGet]
        [CustomAuthorizeAttribute(PermCd = PermissionCode.Add)]
        public ActionResult ManageCaseGrievance(string p, FormCollection fc)
        {
            string id = "";
            DataTable dtHistory = new DataTable();
            CaseGrievanceModel cgm = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            RouteValueDictionary rvd = new RouteValueDictionary();
            CaseGrievanceModel objCase = new CaseGrievanceModel();
            try
            {
                CheckPermission();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                    }

                }

                if (id.ConvertToString() != "")
                {
                    if ((ViewBag.EnableEdit == "true"))
                    {
                        objCase = objCaseGrievanceService.CaseGrievance_Get(id);

                        if (objCase.caseStatus.ConvertToString() == "P" || objCase.caseStatus.ConvertToString() == "R")
                        {
                            ViewBag.AddMode = 'N';
                        }
                        else if (objCase.caseStatus.ConvertToString() == "S")
                        {
                            ViewBag.AddMode = 'S';
                        }
                        ViewBag.CaseStatus = objCase.caseStatus.ConvertToString();
                        string OfficeCd = CommonVariables.OfficeCD;
                        string DefinedOfficeCD = commonFC.GetDefinedCodeFromDataBase(OfficeCd, "MIS_OFFICE", "OFFICE_CD");
                        ViewData["ddl_Districts"] = commonFC.GetDistricts(objCase.DIST_CD.ConvertToString());
                        objCase.VDC_MUN_CD = commonFC.GetDefinedCodeFromDataBase(objCase.VDC_MUN_CD, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                        ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(objCase.VDC_MUN_CD, objCase.DIST_CD.ConvertToString());
                        ViewData["ddl_WardPer"] = commonFC.GetWardByVDCMun(objCase.WARD_NO.ConvertToString(), objCase.VDC_MUN_CD);
                        ViewData["ddl_RegistrationDistricts"] = commonFC.GetDistricts(objCase.REGISTRATION_DIST_CD.ConvertToString());
                        objCase.REGISTRATION_VDC_MUN_CD = commonFC.GetDefinedCodeFromDataBase(objCase.REGISTRATION_VDC_MUN_CD, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                        ViewData["ddl_RegistrationVDCMun"] = commonFC.GetVDCMunByDistrict(objCase.REGISTRATION_VDC_MUN_CD, objCase.REGISTRATION_DIST_CD.ConvertToString());
                        ViewData["ddl_RegistrationWardPer"] = commonFC.GetWardByVDCMun(objCase.REGISTRATION_WARD_NO.ConvertToString(), objCase.REGISTRATION_VDC_MUN_CD);
                        ViewData["ddl_OTHDistricts"] = commonFC.GetAllDistricts("");
                        objCase.VDCMun_CD = commonFC.GetDefinedCodeFromDataBase(objCase.VDCMun_CD, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                        ViewData["ddl_OTHVDCMun"] = commonFC.GetVDCMunByDistrict(objCase.VDCMun_CD, objCase.DistrictCd);
                        ViewData["ddl_OTHWardPer"] = commonFC.GetWardByVDCMun(objCase.WardCd, objCase.VDCMunCd);
                        ViewData["ddlLegalOwner"] = househead.getHouseholdLegalOwner(objCase.HOUSE_LAND_LEGAL_OWNERCD);
                        ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterEarthQuake("");
                        ViewData["ddl_CaseGrievanceType"] = commonFC.getcaseGrievanceType("");
                        ViewData["ddl_AddressedOffice"] = commonFC.GetAddressedOfficeForCaseGrievance(DefinedOfficeCD);
                        ViewData["ddl_ForwadedOffice"] = commonFC.GetForwadedOfficeForCaseGrievance("", "");
                        ViewData["ddl_OtherHouse"] = (List<SelectListItem>)commonFC.GetYesNo1(objCase.OtherHouse).Data;
                        ViewData["ddl_CaseStatus"] = commonFC.CaseGrievanceStatus(objCase.CASE_STATUS);
                        ViewData["ddl_Status"] = (List<SelectListItem>)commonFC.CaseStatus(objCase.CASE_STATUS.ConvertToString()).Data;
                        objCase.Mode = "U";

                        DataTable dtCaseDetail = objService.GetCaseDetail(id);
                        if (dtCaseDetail != null && dtCaseDetail.Rows.Count > 0)
                        {
                            List<CaseGrievanceDetail> GrievanceDetail = new List<CaseGrievanceDetail>();
                            int houseOwnerIncrement = 0;
                            foreach (DataRow dr in dtCaseDetail.Rows)
                            {
                                CaseGrievanceDetail ObjCaseDetail = new CaseGrievanceDetail();
                                ObjCaseDetail.CASE_REGISTRATION_ID = dr["CASE_REGISTRATION_ID"].ConvertToString();
                                ObjCaseDetail.CASE_GRIEVANCE_TYPE_CD = dr["CASE_GRIEVANCE_TYPE_CD"].ConvertToString();
                                ObjCaseDetail.GRIEVANCE_DETAIL_ID = dr["GRIEVANCE_DETAIL_ID"].ToDecimal();
                                ObjCaseDetail.CASE_STATUS = dr["CASE_STATUS"].ConvertToString();
                                ObjCaseDetail.ADDRESSED_OFFICE = dr["ADDRESSED_OFFICE"].ConvertToString();
                                ObjCaseDetail.FORWARDED_OFFICE = dr["FORWARDED_OFFICE"].ConvertToString();
                                if (dr["ADDRESSED_DATE_ENG"].ConvertToString() != "")
                                {
                                    ObjCaseDetail.ADDRESSED_DATE_ENG = Convert.ToDateTime(dr["ADDRESSED_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                                }

                                ObjCaseDetail.REMARKS = dr["REMARKS"].ConvertToString();
                                ViewData["ddl_AddressedOffice"] = commonFC.GetAddressedOfficeForCaseGrievance(ObjCaseDetail.ADDRESSED_OFFICE).ToList();
                                ViewData["ddl_ForwadedOffice"] = commonFC.GetForwadedOffice(ObjCaseDetail.FORWARDED_OFFICE, ObjCaseDetail.ADDRESSED_OFFICE).ToList();
                                ViewData["ddl_CaseGrievanceType"] = commonFC.getcaseGrievanceType(ObjCaseDetail.CASE_GRIEVANCE_TYPE_CD);
                                //ViewData["ddl_CaseGrievanceType"] = commonFC.getcaseGrievanceType(ObjCaseDetail.CASE_GRIEVANCE_TYPE_CD);
                                if (DefinedOfficeCD == ObjCaseDetail.ADDRESSED_OFFICE)
                                {
                                    objCase.ViewMode = "Y";
                                }
                                GrievanceDetail.Add(ObjCaseDetail);

                                houseOwnerIncrement++;
                            }
                            objCase.CaseGrievanceDtl = GrievanceDetail;
                        }
                        DataTable dtOTHDetail = objService.getOtherHouseDetail(id);
                        if (dtOTHDetail != null && dtOTHDetail.Rows.Count > 0)
                        {
                            List<CaseGrievanceOTHDetail> OtherHouseDetail = new List<CaseGrievanceOTHDetail>();
                            int OtherHouserIncrement = 0;
                            foreach (DataRow dr in dtOTHDetail.Rows)
                            {
                                CaseGrievanceOTHDetail objOTHDetail = new CaseGrievanceOTHDetail();
                                objOTHDetail.CASE_REGISTRATION_ID = dr["CASE_REGISTRATION_ID"].ConvertToString();
                                objOTHDetail.GRIEVANCE_OTH_DETAIL_ID = dr["GRIEVANCE_OTH_DETAIL_ID"].ConvertToString();
                                objOTHDetail.HOUSEHOLD_HEAD_FNAME_ENG = dr["HOUSE_OWNER_FIRST_NAME_ENG"].ConvertToString();
                                objOTHDetail.HOUSEHOLD_HEAD_MNAME_ENG = dr["HOUSE_OWNER_MIDDLE_NAME_ENG"].ConvertToString();
                                objOTHDetail.HOUSEHOLD_HEAD_LNAME_ENG = dr["HOUSE_OWNER_LAST_NAME_ENG"].ConvertToString();
                                objOTHDetail.HOUSE_DIST_CD = dr["HOUSE_DIST_CD"].ToDecimal();
                                objOTHDetail.HOUSE_VDC_MUN_CD = dr["HOUSE_VDC_MUN_CD"].ConvertToString();
                                objOTHDetail.HOUSE_WARD_NO = dr["HOUSE_WARD_NO"].ToDecimal();
                                objOTHDetail.HOUSE_AREA = dr["HOUSE_AREA"].ConvertToString();
                                objOTHDetail.HOUSE_CONDITION_CD = dr["HOUSE_CONDITION_CD"].ToDecimal();
                                objOTHDetail.HOUSE_VDC_MUN_CD = dr["HOUSE_VDC_MUN_CD"].ConvertToString();
                                ViewData["ddl_OTHDistricts"] = commonFC.GetAllDistricts(objOTHDetail.HOUSE_DIST_CD.ConvertToString());
                                objOTHDetail.HOUSE_VDC_MUN_CD = commonFC.GetDefinedCodeFromDataBase(objOTHDetail.HOUSE_VDC_MUN_CD, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                ViewData["ddl_OTHVDCMun"] = commonFC.GetVDCMunByAllDistrict(objOTHDetail.HOUSE_VDC_MUN_CD, objOTHDetail.HOUSE_DIST_CD.ConvertToString());
                                ViewData["ddl_OTHWardPer"] = commonFC.GetWardByVDCMun(objOTHDetail.HOUSE_WARD_NO.ConvertToString(), objOTHDetail.HOUSE_VDC_MUN_CD.ConvertToString());
                                ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterEarthQuake(objOTHDetail.HOUSE_CONDITION_CD.ConvertToString());
                                OtherHouseDetail.Add(objOTHDetail);

                                OtherHouserIncrement++;
                            }
                            objCase.CaseGrievanceOthDtl = OtherHouseDetail;
                        }

                        #region Case Grievance Doc Type
                        DataTable DocumentName = objService.DocumentName();

                        if (DocumentName != null && DocumentName.Rows.Count > 0)
                        {
                            List<NHRSGrievanceDocType> DocumentTypeList = new List<NHRSGrievanceDocType>();
                            foreach (DataRow dr in DocumentName.Rows)
                            {
                                NHRSGrievanceDocType DocumentTypeName = new NHRSGrievanceDocType();
                                DocumentTypeName.definedcd = Convert.ToDecimal(dr["DOC_TYPE_CD"]);
                                DocumentTypeName.desceng = dr["DESC_ENG"].ConvertToString();
                                DocumentTypeName.descloc = dr["DESC_LOC"].ConvertToString();

                                DocumentTypeList.Add(DocumentTypeName);
                            }
                            objCase.DocumentTypeName = DocumentTypeList;
                        }
                        DataTable DocumentChecked = objService.DocumentChecked(id);
                        if (DocumentChecked != null && DocumentChecked.Rows.Count > 0)
                        {
                            List<NHRSGrievanceDocType> DocumentTypeDetails = new List<NHRSGrievanceDocType>();
                            foreach (DataRow dr in DocumentChecked.Rows)
                            {
                                NHRSGrievanceDocType NHRSDocumentTypeName = new NHRSGrievanceDocType();
                                NHRSDocumentTypeName.GRIEVANCE_DOC_DETAIL_ID = dr["GRIEVANCE_DOC_DETAIL_ID"].ConvertToString();
                                NHRSDocumentTypeName.definedcd = Convert.ToDecimal(dr["GRIEVANCE_DOC_TYPE_CD"]);
                                NHRSDocumentTypeName.desceng = dr["DESC_ENG"].ConvertToString();
                                NHRSDocumentTypeName.descloc = dr["DESC_LOC"].ConvertToString();
                                DocumentTypeDetails.Add(NHRSDocumentTypeName);
                            }
                            objCase.DocumentTypeDetail = DocumentTypeDetails;
                        }
                        #endregion

                        #region Case Grievance Registration Type
                        DataTable CaseRegistrationType = objService.CaseRegistrationTYpe();

                        if (CaseRegistrationType != null && CaseRegistrationType.Rows.Count > 0)
                        {
                            List<CaseType> CaseRegistrationTypeList = new List<CaseType>();
                            foreach (DataRow dr in CaseRegistrationType.Rows)
                            {
                                CaseType CaseRegistrationName = new CaseType();
                                CaseRegistrationName.DefinedCd = dr["CASE_GRIEVIENCE_TYPE_CD"].ConvertToString();
                                CaseRegistrationName.DescEng = dr["DESC_ENG"].ConvertToString();
                                CaseRegistrationName.DescLoc = dr["DESC_LOC"].ConvertToString();

                                CaseRegistrationTypeList.Add(CaseRegistrationName);
                            }
                            objCase.CaseRegistrationName = CaseRegistrationTypeList;
                        }
                        DataTable CaseRegistrationChecked = objService.caseRegistrationChecked(id);
                        if (CaseRegistrationChecked != null && CaseRegistrationChecked.Rows.Count > 0)
                        {
                            List<CaseType> CaseRegistrationTypeList = new List<CaseType>();
                            foreach (DataRow dr in CaseRegistrationChecked.Rows)
                            {
                                CaseType CaseRegistrationName = new CaseType();
                                CaseRegistrationName.DefinedCd = dr["CASE_GRIEVANCE_TYPE_CD"].ConvertToString();
                                CaseRegistrationName.DescEng = dr["DESC_ENG"].ConvertToString();
                                CaseRegistrationName.DescLoc = dr["DESC_LOC"].ConvertToString();
                                CaseRegistrationName.Remarks = dr["REMARKS"].ConvertToString();
                                CaseRegistrationTypeList.Add(CaseRegistrationName);
                            }
                            objCase.CaseRegistrationList = CaseRegistrationTypeList;
                        }
                        #endregion


                        return View(objCase);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Transaction failed! You are not authorized to perform this transaction. Please contact your Supervisor.";
                        return RedirectToAction(ViewBag.ListActionName);
                    }
                }
                else
                {
                    string OfficeCd = CommonVariables.OfficeCD;
                    string DefinedOfficeCD = commonFC.GetDefinedCodeFromDataBase(OfficeCd, "MIS_OFFICE", "OFFICE_CD");
                    ViewData["ddl_Districts"] = commonFC.GetDistricts(objCase.DIST_CD.ConvertToString());
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(objCase.VDC_MUN_CD, objCase.DIST_CD.ConvertToString());
                    ViewData["ddl_WardPer"] = commonFC.GetWardByVDCMun(objCase.WardCd, objCase.VDCMunCd);
                    ViewData["ddl_RegistrationDistricts"] = commonFC.GetDistricts(objCase.REGISTRATION_DIST_CD.ConvertToString());

                    ViewData["ddl_RegistrationVDCMun"] = commonFC.GetVDCMunByDistrict(objCase.REGISTRATION_VDC_MUN_CD, objCase.REGISTRATION_DIST_CD.ConvertToString());

                    ViewData["ddl_RegistrationWardPer"] = commonFC.GetWardByVDCMun(objCase.REGISTRATION_WARD_NO.ConvertToString(), objCase.REGISTRATION_VDC_MUN_CD);
                    ViewData["ddl_OTHDistricts"] = commonFC.GetAllDistricts("");
                    ViewData["ddl_OTHVDCMun"] = commonFC.GetVDCMunByAllDistrict(objCase.VDCMun_CD, objCase.DistrictCd);
                    ViewData["ddl_OTHDistricts"] = commonFC.GetAllDistricts("");
                    ViewData["ddl_OTHWardPer"] = commonFC.GetWardByVDCMun(objCase.WardCd, objCase.VDCMunCd);
                    ViewData["ddlLegalOwner"] = househead.getHouseholdLegalOwner("");
                    ViewData["ddl_BuildingCondition"] = househead.GetBuildingConditionAfterEarthQuake("");
                    //ViewData["ddl_AddressedOffice"] = commonFC.GetAddressedOfficeForCaseGrievance(DefinedOfficeCD);
                    //ViewData["ddl_ForwadedOffice"] = commonFC.GetForwadedOffice("", DefinedOfficeCD).ToList();
                    ViewData["ddl_CaseGrievanceType"] = commonFC.getcaseGrievanceType("");
                    ViewData["ddl_Status"] = (List<SelectListItem>)commonFC.CaseStatus("").Data;
                    ViewData["ddl_OtherHouse"] = (List<SelectListItem>)commonFC.GetYesNo1("").Data;
                    //ViewData["ddl_Status"] = commonFC.CaseGrievanceStatus("");
                    ViewBag.HouseholdExists = 'Y';
                    cgm.Mode = "I";
                    #region Case Grievance Doc Type (Binod)
                    // Super Structure Name
                    DataTable DocumentName = objService.DocumentName();

                    if (DocumentName != null && DocumentName.Rows.Count > 0)
                    {
                        List<NHRSGrievanceDocType> DocumentTypeList = new List<NHRSGrievanceDocType>();
                        foreach (DataRow dr in DocumentName.Rows)
                        {
                            NHRSGrievanceDocType DocumentTypeName = new NHRSGrievanceDocType();
                            DocumentTypeName.definedcd = Convert.ToDecimal(dr["DOC_TYPE_CD"]);
                            DocumentTypeName.desceng = dr["DESC_ENG"].ConvertToString();
                            DocumentTypeName.descloc = dr["DESC_LOC"].ConvertToString();

                            DocumentTypeList.Add(DocumentTypeName);
                        }
                        cgm.DocumentTypeName = DocumentTypeList;
                    }
                    #endregion
                    DataTable CaseRegistrationType = objService.CaseRegistrationTYpe();

                    if (CaseRegistrationType != null && CaseRegistrationType.Rows.Count > 0)
                    {
                        List<CaseType> CaseRegistrationTypeList = new List<CaseType>();
                        foreach (DataRow dr in CaseRegistrationType.Rows)
                        {
                            CaseType CaseRegistrationName = new CaseType();
                            CaseRegistrationName.DefinedCd = dr["CASE_GRIEVIENCE_TYPE_CD"].ConvertToString();
                            CaseRegistrationName.DescEng = dr["DESC_ENG"].ConvertToString();
                            CaseRegistrationName.DescLoc = dr["DESC_LOC"].ConvertToString();

                            CaseRegistrationTypeList.Add(CaseRegistrationName);
                        }
                        cgm.CaseRegistrationName = CaseRegistrationTypeList;
                    }
                    if (ViewBag.EnableAdd == "true")
                    {
                        ViewData["ddl_Category"] = new List<SelectListItem>();
                        //changed for NHRS
                        //ViewBag.RegistrationGroupType = commonFC.GetCaseRegistrationGroupTypeCode(string.Empty);
                        ViewData["Readonly"] = true;
                        return View(cgm);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Transaction failed! You are not authorized to perform this transaction. Please contact your Supervisor.";
                        return RedirectToAction(ViewBag.ListActionName);
                    }
                }
            }
            catch (SqlException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View(ViewBag.AddActionName, cgm);
        }

        //Post Insert/Edit CaseGrievance
        [HttpPost]
        public ActionResult ManageCaseGrievance(FormCollection fc)
        {
            CaseGrievanceModel objCaseGrievance = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();

            try
            {
                #region Case Registration
                objCaseGrievance.CASE_REGISTRATION_ID = fc["CASE_REGISTRATION_ID"].ConvertToString();
                objCaseGrievance.REGISTRATION_NO = fc["REGISTRATION_NO"].ConvertToString();
                objCaseGrievance.FORM_NO = fc["FORM_NO"].ConvertToString();
                objCaseGrievance.REGISTRATION_DIST_CD = fc["REGISTRATION_DIST_CD"].ToDecimal();
                objCaseGrievance.REGISTRATION_VDC_MUN_CD = commonFC.GetCodeFromDataBase(fc["REGISTRATION_VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                objCaseGrievance.REGISTRATION_WARD_NO = fc["REGISTRATION_WARD_NO"].ToDecimal();
                objCaseGrievance.REGISTRATION_AREA = fc["REGISTRATION_AREA"].ConvertToString();
                objCaseGrievance.REGISTRATION_DATE_ENG = fc["REGISTRATION_DATE_ENG"].ConvertToString();
                objCaseGrievance.REGISTRATION_DATE_LOC = fc["REGISTRATION_DATE_LOC"].ConvertToString();
                objCaseGrievance.FIRST_NAME_ENG = fc["FIRST_NAME_ENG"].ConvertToString();
                objCaseGrievance.MIDDLE_NAME_ENG = fc["MIDDLE_NAME_ENG"].ConvertToString();
                objCaseGrievance.LAST_NAME_ENG = fc["LAST_NAME_ENG"].ConvertToString();
                objCaseGrievance.LALPURJA_NO = fc["LALPURJA_NO"].ConvertToString();
                objCaseGrievance.LALPURJA_ISSUE_DATE_ENG = fc["LALPURJA_ISSUE_DATE_ENG"].ConvertToString();
                objCaseGrievance.LALPURJA_ISSUE_DATE_LOC = fc["LALPURJA_ISSUE_DATE_LOC"].ConvertToString();
                objCaseGrievance.FATHER_FIRST_NAME_ENG = fc["FATHER_FIRST_NAME_ENG"].ConvertToString();
                objCaseGrievance.FATHER_MIDDLE_NAME_ENG = fc["FATHER_MIDDLE_NAME_ENG"].ConvertToString();
                objCaseGrievance.FATHER_LAST_NAME_ENG = fc["FATHER_LAST_NAME_ENG"].ConvertToString();
                objCaseGrievance.HUSBAND_FIRST_NAME_ENG = fc["HUSBAND_FIRST_NAME_ENG"].ConvertToString();
                objCaseGrievance.HUSBAND_MIDDLE_NAME_ENG = fc["HUSBAND_MIDDLE_NAME_ENG"].ConvertToString();
                objCaseGrievance.HUSBAND_LAST_NAME_ENG = fc["HUSBAND_LAST_NAME_ENG"].ConvertToString();
                objCaseGrievance.CITIZENSHIP_NO = fc["CITIZENSHIP_NO"].ConvertToString();
                objCaseGrievance.CITIZENSHIP_ISSUE_DATE_ENG = fc["CITIZENSHIP_ISSUE_DATE_ENG"].ConvertToString();
                objCaseGrievance.CITIZENSHIP_ISSUE_DATE_LOC = fc["CITIZENSHIP_ISSUE_DATE_LOC"].ConvertToString();
                objCaseGrievance.GFATHER_FIRST_NAME_ENG = fc["GFATHER_FIRST_NAME_ENG"].ConvertToString();
                objCaseGrievance.GFATHER_MIDDLE_NAME_ENG = fc["GFATHER_MIDDLE_NAME_ENG"].ConvertToString();
                objCaseGrievance.GFATHER_LAST_NAME_ENG = fc["GFATHER_LAST_NAME_ENG"].ConvertToString();
                objCaseGrievance.DIST_CD = fc["DIST_CD"].ToDecimal();
                objCaseGrievance.VDC_MUN_CD = commonFC.GetCodeFromDataBase(fc["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                objCaseGrievance.WARD_NO = fc["WARD_NO"].ToDecimal();
                objCaseGrievance.AREA = fc["AREA"].ConvertToString();
                objCaseGrievance.HOUSEHOLD_MEMER_COUNT = fc["HOUSEHOLD_MEMER_COUNT"].ToDecimal();
                objCaseGrievance.BENEFICIARY_ID = fc["BENEFICIARY_ID"].ConvertToString();
                objCaseGrievance.CONTACT_PHONE_NO = fc["CONTACT_PHONE_NO"].ConvertToString();
                objCaseGrievance.HOUSE_LAND_LEGAL_OWNERCD = fc["ddlLegalOwner"].ConvertToString();
                objCaseGrievance.ENUMENATOR_ID = fc["ENUMENATOR_ID"].ConvertToString();
                objCaseGrievance.ENUMENATOR_FIRST_NAME_ENG = fc["ENUMENATOR_FIRST_NAME_ENG"].ConvertToString();
                objCaseGrievance.ENUMENATOR_MIDDLE_NAME_ENG = fc["ENUMENATOR_MIDDLE_NAME_ENG"].ConvertToString();
                objCaseGrievance.ENUMENATOR_LAST_NAME_ENG = fc["ENUMENATOR_LAST_NAME_ENG"].ConvertToString();
                objCaseGrievance.ENUMENATOR_SIGNED_DATE_ENG = fc["ENUMENATOR_SIGNED_DATE_ENG"].ConvertToString();
                objCaseGrievance.ENUMENATOR_SIGNED_DATE_LOC = fc["ENUMENATOR_SIGNED_DATE_LOC"].ConvertToString();
                objCaseGrievance.CASE_SIGNATURE_DATE_ENG = fc["CASE_SIGNATURE_DATE_ENG"].ConvertToString();
                objCaseGrievance.CASE_SIGNATURE_DATE_LOC = fc["CASE_SIGNATURE_DATE_LOC"].ConvertToString();
                objCaseGrievance.CASE_STATUS = fc["CaseStatus"].ConvertToString();
                objCaseGrievance.CASE_ADDRESSED_BY_FNAME_ENG = fc["CASE_ADDRESSED_BY_FNAME_ENG"].ConvertToString();
                objCaseGrievance.CASE_ADDRESSED_BY_MNAME_ENG = fc["CASE_ADDRESSED_BY_MNAME_ENG"].ConvertToString();
                objCaseGrievance.CASE_ADDRESSED_BY_LNAME_ENG = fc["CASE_ADDRESSED_BY_LNAME_ENG"].ConvertToString();
                objCaseGrievance.CASE_ADDRESSED_DATE_LOC = fc["CASE_ADDRESSED_DATE_LOC"].ConvertToString();
                objCaseGrievance.HOUSE_LAND_LEGAL_OTH_COMMENT = fc["HOUSE_LAND_LEGAL_OTH_COMMENT"].ConvertToString();
                objCaseGrievance.DEFINED_CD = objCaseGrievance.REGISTRATION_NO.ConvertToString();
                if (fc["CASE_ADDRESSED_DATE_ENG"].ConvertToString() != "")
                {
                    objCaseGrievance.CASE_ADDRESSED_DATE_ENG = Convert.ToDateTime(fc["CASE_ADDRESSED_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                }
                objCaseGrievance.OtherHouse = fc["OtherHouse"].ConvertToString();

                if (fc["btn_Submit"].ToString() == "Submit" || fc["btn_Submit"].ToString() == "पेश गर्नुहोस्")
                {
                    objCaseGrievance.Mode = "I";
                    objService.SaveCaseRegistration(objCaseGrievance);
                }
                if (fc["btn_Submit"].ToString() == "Update" || fc["btn_Submit"].ToString() == "अपडेट")
                {

                    objCaseGrievance.Mode = "U";
                    objService.SaveCaseRegistration(objCaseGrievance);
                }
                DataTable dt = objService.GetCaseRegistrationID(objCaseGrievance.FORM_NO, objCaseGrievance.REGISTRATION_DIST_CD.ConvertToString(), objCaseGrievance.REGISTRATION_VDC_MUN_CD.ConvertToString(), objCaseGrievance.REGISTRATION_WARD_NO.ConvertToString());
                #endregion

                #region Other House Detail
                CaseGrievanceOTHDetail objOtherHouse = new CaseGrievanceOTHDetail();

                if (dt.Rows.Count > 0)
                {
                    objOtherHouse.CASE_REGISTRATION_ID = dt.Rows[0][0].ConvertToString();
                }
                objOtherHouse.Mode = "D";
                objService.SaveOtherHouseDetail(objOtherHouse);
                objOtherHouse.HOUSEHOLD_HEAD_FNAME_ENG = fc["HouseOwnerFName"].ConvertToString();
                objOtherHouse.HOUSEHOLD_HEAD_MNAME_ENG = fc["HouseOwnerMName"].ConvertToString();
                objOtherHouse.HOUSEHOLD_HEAD_LNAME_ENG = fc["HouseOwnerLName"].ConvertToString();
                objOtherHouse.HOUSE_CONDITION_CD = fc["OTHBuildingCondition"].ToDecimal();
                objOtherHouse.HOUSE_DIST_CD = fc["OTHDistrictCd"].ToDecimal();
                objOtherHouse.HOUSE_VDC_MUN_CD = commonFC.GetCodeFromDataBase(fc["OTHVDCCd"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                objOtherHouse.HOUSE_WARD_NO = fc["OTHWardNo"].ToDecimal();
                objOtherHouse.HOUSE_AREA = fc["OTHArea"].ConvertToString();

                objOtherHouse.Mode = "I";
                objOtherHouse.GRIEVANCE_OTH_DETAIL_ID = fc["OTHDetailID"].ConvertToString();
                objService.SaveOtherHouseDetail(objOtherHouse);


                #endregion
                DataTable dtDocument = objService.DocumentName();
                if (dtDocument != null && dtDocument.Rows.Count > 0)
                {
                    objService = new NHRSCaseGrievanceService();
                    NHRGrievanceDocDetail docdelete = new NHRGrievanceDocDetail();
                    if (dt.Rows.Count > 0)
                    {
                        docdelete.caseregistrationid = dt.Rows[0][0].ConvertToString();
                    }
                    objService.DeleteCaseGrievanceDocDetail(docdelete);
                    foreach (DataRow dr in dtDocument.Rows)
                    {
                        NHRGrievanceDocDetail DocName = new NHRGrievanceDocDetail();
                        if (dt.Rows.Count > 0)
                        {
                            DocName.caseregistrationid = dt.Rows[0][0].ConvertToString();
                        }
                        DocName.grievancedoctypecd = (dr["DOC_TYPE_CD"]).ConvertToString();

                        if (fc["chkDocType_" + dr["DOC_TYPE_CD"].ConvertToString()].Contains("true"))
                        {
                            objService.InsertCaseGrievanceDetail(DocName);
                        }

                    }
                }

                DataTable dtCaseRegistrationType = objService.CaseRegistrationTYpe();
                if (dtCaseRegistrationType != null && dtCaseRegistrationType.Rows.Count > 0)
                {
                    objService = new NHRSCaseGrievanceService();
                    CaseGrievanceRegistrationType CaseTypeDelete = new CaseGrievanceRegistrationType();
                    if (dt.Rows.Count > 0)
                    {
                        CaseTypeDelete.caseregistrationid = dt.Rows[0][0].ConvertToString();
                    }
                    objService.DeleteCaseRegistrationTypeDetail(CaseTypeDelete);
                    foreach (DataRow dr in dtCaseRegistrationType.Rows)
                    {
                        CaseGrievanceRegistrationType CaseName = new CaseGrievanceRegistrationType();
                        if (dt.Rows.Count > 0)
                        {
                            CaseName.caseregistrationid = dt.Rows[0][0].ConvertToString();

                        }
                        CaseName.grievancetypecd = (dr["CASE_GRIEVIENCE_TYPE_CD"]).ConvertToString();
                        CaseName.remarks = fc["chkRemarks_" + dr["CASE_GRIEVIENCE_TYPE_CD"].ConvertToString()];

                        if (fc["chkRegistrationType_" + dr["CASE_GRIEVIENCE_TYPE_CD"].ConvertToString()].Contains("true"))
                        {

                            objService.InsertCaseRegistrationTypeDetail(CaseName);
                        }

                    }
                }
            }
            catch (OracleException oe)
            {
                TempData["ErrorMessage"] = "Grievance Not Registered";
                ExceptionManager.AppendLog(oe);
                return RedirectToAction("ManageCaseGrievance");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Grievance Not Registered";
                ExceptionManager.AppendLog(ex);
                return RedirectToAction("ManageCaseGrievance");
            }
            return RedirectToAction("ManageCaseGrievance");
        }

        //Delete Case
        //[CustomAuthorizeAttribute(PermCd = PermissionCode.Delete)]


        public ActionResult DeleteCaseGrievance(string p)
        {
            CaseGrievanceModel objCaseGrievance = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
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
                            objCaseGrievance.CASE_REGISTRATION_ID = id;
                            objCaseGrievance.Mode = "D";
                            objService.DeleteCaseRegistration(objCaseGrievance);
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

            return RedirectToAction("ListCaseGrievance");
        }

        public ActionResult DataNotFound(string p)
        {
            CaseGrievanceModel objCaseGrievance = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            string id = "";
            var param = Session["Param"];
            CaseGrievanceModel objcaseGrievance = (CaseGrievanceModel)param;
            var CaseType = Session["CaseType"];
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
                            objCaseGrievance.CASE_REGISTRATION_ID = id;
                            objService.UpdateDataNotFound(id);
                             DataTable dtResults=objService.GetCaseGrievanceSearch(objcaseGrievance,CaseType.ConvertToString());
                            ViewData["result"] = dtResults;
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

            return RedirectToAction("ListCaseGrievance");
        }



        //For Changing Approval Status
        [CustomAuthorizeAttribute(PermCd = PermissionCode.Approve)]
        public void ChangeApprovalStatus(string p)
        {
            CaseGrievanceModel objCase = new CaseGrievanceModel();
            Users objUsers = new Users();
            //VDCMunService VDCMunService = new VDCMunService();
            string strUserName = string.Empty;
            if (Session[SessionCheck.sessionName] != null)
            {
                objUsers = (Users)Session[SessionCheck.sessionName];
                strUserName = objUsers.usrName;
            }
            string status = string.Empty;
            string id = String.Empty;
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
                            id = rvd["id"].ConvertToString();
                        }
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ConvertToString();
                        }
                    }
                }
                objCase.grievanceCd = id.ConvertToString();
                if (status == "Y")
                {
                    objCase.approved = "N";
                }
                else
                {
                    objCase.approved = "Y";
                }
                objCase.enteredBy = strUserName;
                string exc = string.Empty;
                objCaseGrievanceService.AddCaseGrievance(objCase, "A", out exc);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

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
            catch (SqlException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


        }
        #endregion


        public JsonResult CheckDuplicateFormNo(string id, string District, string VDC, string Ward)
        {
            bool boolValue = false;
            CommonFunction cnmFunc = new CommonFunction();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            string VDCCode = cnmFunc.GetCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            boolValue = objService.CheckDuplicateFormNo(id, District, VDCCode, Ward);
            return new JsonResult { Data = boolValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult CheckDuplicateRegistrationNo(string id)
        {
            bool boolValue = false;
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            boolValue = objService.CheckDuplicateRegistrationNo(id);
            return new JsonResult { Data = boolValue, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GrievanceHandling(string p)
        {
            NHRSCaseGrievanceService objGrievanceService = new NHRSCaseGrievanceService();
            CaseGrievanceModel obj = new CaseGrievanceModel();
            string id = "";
            DataTable dtHistory = new DataTable();
            RouteValueDictionary rvd = new RouteValueDictionary();
            try
            {
                CheckPermission();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = rvd["id"].ToString();
                        }
                    }

                }
                DataTable CaseRegistrationType = objGrievanceService.CaseRegistrationTYpe();
                if (CaseRegistrationType != null && CaseRegistrationType.Rows.Count > 0)
                {
                    List<CaseType> CaseRegistrationTypeList = new List<CaseType>();
                    foreach (DataRow dr in CaseRegistrationType.Rows)
                    {
                        CaseType CaseRegistrationName = new CaseType();
                        CaseRegistrationName.DefinedCd = dr["CASE_GRIEVIENCE_TYPE_CD"].ConvertToString();
                        CaseRegistrationName.DescEng = dr["DESC_ENG"].ConvertToString();
                        CaseRegistrationName.DescLoc = dr["DESC_LOC"].ConvertToString();

                        CaseRegistrationTypeList.Add(CaseRegistrationName);
                    }
                    objCase.CaseRegistrationName = CaseRegistrationTypeList;
                }

                DataTable DocumentName = objGrievanceService.DocumentName();

                if (DocumentName != null && DocumentName.Rows.Count > 0)
                {
                    List<NHRSGrievanceDocType> DocumentTypeList = new List<NHRSGrievanceDocType>();
                    foreach (DataRow dr in DocumentName.Rows)
                    {
                        NHRSGrievanceDocType DocumentTypeName = new NHRSGrievanceDocType();
                        DocumentTypeName.definedcd = Convert.ToDecimal(dr["DOC_TYPE_CD"]);
                        DocumentTypeName.desceng = dr["DESC_ENG"].ConvertToString();
                        DocumentTypeName.descloc = dr["DESC_LOC"].ConvertToString();

                        DocumentTypeList.Add(DocumentTypeName);
                    }
                    objCase.DocumentTypeName = DocumentTypeList;
                }

                DataSet ds = objGrievanceService.getAllGrievanceHandlingDetail(id);
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows != null)
                {
                    objCase.CASE_REGISTRATION_ID = ds.Tables[0].Rows[0]["CASE_REGISTRATION_ID"].ConvertToString();
                    objCase.CASE_STATUS = ds.Tables[0].Rows[0]["CASE_STATUS"].ConvertToString();
                    objCase.FORM_NO = ds.Tables[0].Rows[0]["FORM_NO"].ConvertToString();
                    objCase.REGISTRATION_NO = ds.Tables[0].Rows[0]["REGISTRATION_NO"].ConvertToString();
                    objCase.REGISTRATION_DIST_CD = ds.Tables[0].Rows[0]["REGISTRATION_DIST_CD"].ToDecimal();
                    objCase.REGISTRATION_DIST_ENG = ds.Tables[0].Rows[0]["REGISTRATION_DISTRICT_ENG"].ConvertToString();
                    objCase.REGISTRATION_DIST_LOC = ds.Tables[0].Rows[0]["REGISTRATION_DISTRICT_LOC"].ConvertToString();
                    objCase.REGISTRATION_VDC_MUN_CD = ds.Tables[0].Rows[0]["REGISTRATION_VDC_MUN_CD"].ConvertToString();
                    objCase.REGISTRATION_VDC_MUN_ENG = ds.Tables[0].Rows[0]["REGISTRATION_VDC_ENG"].ConvertToString();
                    objCase.REGISTRATION_VDC_MUN_LOC = ds.Tables[0].Rows[0]["REGISTRATION_VDC_LOC"].ConvertToString();
                    objCase.REGISTRATION_WARD_NO = ds.Tables[0].Rows[0]["REGISTRATION_WARD_NO"].ToDecimal();
                    objCase.REGISTRATION_AREA = ds.Tables[0].Rows[0]["REGISTRATION_AREA"].ConvertToString();
                    if (ds.Tables[0].Rows[0]["REGISTRATION_DATE_ENG"].ConvertToString() != "")
                    {
                        objCase.REGISTRATION_DATE_ENG = Convert.ToDateTime(ds.Tables[0].Rows[0]["REGISTRATION_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    objCase.REGISTRATION_DATE_LOC = ds.Tables[0].Rows[0]["REGISTRATION_DATE_LOC"].ConvertToString();
                    objCase.FIRST_NAME_ENG = ds.Tables[0].Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                    objCase.MIDDLE_NAME_ENG = ds.Tables[0].Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                    objCase.LAST_NAME_ENG = ds.Tables[0].Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    objCase.FULL_NAME_ENG = objCase.FIRST_NAME_ENG + " " + objCase.MIDDLE_NAME_ENG + " " + objCase.LAST_NAME_ENG;
                    objCase.LALPURJA_NO = ds.Tables[0].Rows[0]["LALPURJA_NO"].ConvertToString();
                    if (ds.Tables[0].Rows[0]["LALPURJA_ISSUE_DATE_ENG"].ConvertToString() != "")
                    {
                        objCase.LALPURJA_ISSUE_DATE_ENG = Convert.ToDateTime(ds.Tables[0].Rows[0]["LALPURJA_ISSUE_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    objCase.LALPURJA_ISSUE_DATE_LOC = ds.Tables[0].Rows[0]["LALPURJA_ISSUE_DATE_LOC"].ConvertToString();
                    objCase.FATHER_FIRST_NAME_ENG = ds.Tables[0].Rows[0]["FATHER_FIRST_NAME_ENG"].ConvertToString();
                    objCase.FATHER_MIDDLE_NAME_ENG = ds.Tables[0].Rows[0]["FATHER_MIDDLE_NAME_ENG"].ConvertToString();
                    objCase.FATHER_LAST_NAME_ENG = ds.Tables[0].Rows[0]["FATHER_LAST_NAME_ENG"].ConvertToString();
                    objCase.FATHER_FULL_NAME_ENG = objCase.FATHER_FIRST_NAME_ENG + " " + objCase.FATHER_MIDDLE_NAME_ENG + " " + objCase.FATHER_LAST_NAME_ENG;


                    objCase.HUSBAND_FIRST_NAME_ENG = ds.Tables[0].Rows[0]["HUSBAND_FIRST_NAME_ENG"].ConvertToString();
                    objCase.HUSBAND_MIDDLE_NAME_ENG = ds.Tables[0].Rows[0]["HUSBAND_MIDDLE_NAME_ENG"].ConvertToString();
                    objCase.HUSBAND_LAST_NAME_ENG = ds.Tables[0].Rows[0]["HUSBAND_LAST_NAME_ENG"].ConvertToString();
                    objCase.HUSBAND_FULL_NAME_ENG = objCase.HUSBAND_FIRST_NAME_ENG + " " + objCase.HUSBAND_MIDDLE_NAME_ENG + " " + objCase.HUSBAND_LAST_NAME_ENG;
                    objCase.CITIZENSHIP_NO = ds.Tables[0].Rows[0]["CITIZENSHIP_NO"].ConvertToString();
                    if (ds.Tables[0].Rows[0]["CITIZENSHIP_ISSUE_DATE_ENG"].ConvertToString() != "")
                    {
                        objCase.CITIZENSHIP_ISSUE_DATE_ENG = Convert.ToDateTime(ds.Tables[0].Rows[0]["CITIZENSHIP_ISSUE_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    objCase.CITIZENSHIP_ISSUE_DATE_LOC = ds.Tables[0].Rows[0]["CITIZENSHIP_ISSUE_DATE_LOC"].ConvertToString();
                    objCase.GFATHER_FIRST_NAME_ENG = ds.Tables[0].Rows[0]["GFATHER_FIRST_NAME_ENG"].ConvertToString();
                    objCase.GFATHER_MIDDLE_NAME_ENG = ds.Tables[0].Rows[0]["GFATHER_MIDDLE_NAME_ENG"].ConvertToString();
                    objCase.GFATHER_LAST_NAME_ENG = ds.Tables[0].Rows[0]["GFATHER_LAST_NAME_ENG"].ConvertToString();
                    objCase.GFATHER_FULL_NAME_ENG = objCase.GFATHER_FIRST_NAME_ENG + "" + objCase.GFATHER_MIDDLE_NAME_ENG + "" + objCase.GFATHER_LAST_NAME_ENG;
                    objCase.HOUSEHOLD_MEMER_COUNT = ds.Tables[0].Rows[0]["HOUSEHOLD_MEMBER_COUNT"].ToDecimal();
                    objCase.BENEFICIARY_ID = ds.Tables[0].Rows[0]["BENEFICIARY_ID"].ConvertToString();
                    objCase.LAST_NAME_ENG = ds.Tables[0].Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    objCase.CONTACT_PHONE_NO = ds.Tables[0].Rows[0]["CONTACT_PHONE_NO"].ConvertToString();
                    objCase.HOUSE_LAND_LEGAL_OWNERCD = ds.Tables[0].Rows[0]["HOUSE_LAND_LEGAL_OWNERCD"].ConvertToString();
                    objCase.LAND_LEGAL_ENG = ds.Tables[0].Rows[0]["LAND_LEGAL_ENG"].ConvertToString();
                    objCase.LAND_LEGAL_LOC = ds.Tables[0].Rows[0]["LAND_LEGAL_LOC"].ConvertToString();
                    objCase.OtherHouse = ds.Tables[0].Rows[0]["HOUSE_IN_OTHER_PLACE"].ConvertToString();
                    objCase.ENUMENATOR_ID = ds.Tables[0].Rows[0]["ENUMENATOR_ID"].ConvertToString();
                    objCase.ENUMENATOR_FIRST_NAME_ENG = ds.Tables[0].Rows[0]["ENUMENATOR_FIRST_NAME_ENG"].ConvertToString();
                    objCase.ENUMENATOR_MIDDLE_NAME_ENG = ds.Tables[0].Rows[0]["ENUMENATOR_MIDDLE_NAME_ENG"].ConvertToString();
                    objCase.ENUMENATOR_LAST_NAME_ENG = ds.Tables[0].Rows[0]["ENUMENATOR_LAST_NAME_ENG"].ConvertToString();
                    objCase.ENUMENATOR_FULL_NAME_ENG = objCase.ENUMENATOR_FIRST_NAME_ENG + " " + objCase.ENUMENATOR_MIDDLE_NAME_ENG + " " + objCase.ENUMENATOR_LAST_NAME_ENG;
                    if (ds.Tables[0].Rows[0]["ENUMENATOR_SIGNED_DATE_ENG"].ConvertToString() != "")
                    {
                        objCase.ENUMENATOR_SIGNED_DATE_ENG = Convert.ToDateTime(ds.Tables[0].Rows[0]["ENUMENATOR_SIGNED_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    objCase.ENUMENATOR_SIGNED_DATE_LOC = ds.Tables[0].Rows[0]["ENUMENATOR_SIGNED_DATE_LOC"].ConvertToString();
                    if (ds.Tables[0].Rows[0]["CASE_SIGNATURE_DATE_ENG"].ConvertToString() != "")
                    {
                        objCase.CASE_SIGNATURE_DATE_ENG = Convert.ToDateTime(ds.Tables[0].Rows[0]["CASE_SIGNATURE_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    objCase.CASE_SIGNATURE_DATE_LOC = ds.Tables[0].Rows[0]["CASE_SIGNATURE_DATE_LOC"].ConvertToString();
                    if (ds.Tables[0].Rows[0]["CASE_ADDRESSED_DATE_ENG"].ConvertToString() != "")
                    {
                        objCase.CASE_ADDRESSED_DATE_ENG = Convert.ToDateTime(ds.Tables[0].Rows[0]["CASE_ADDRESSED_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    objCase.CASE_ADDRESSED_BY_FNAME_ENG = ds.Tables[0].Rows[0]["CASE_ADDRESSED_BY_FNAME_ENG"].ConvertToString();
                    objCase.CASE_ADDRESSED_BY_MNAME_ENG = ds.Tables[0].Rows[0]["CASE_ADDRESSED_BY_MNAME_ENG"].ConvertToString();
                    objCase.CASE_ADDRESSED_BY_LNAME_ENG = ds.Tables[0].Rows[0]["CASE_ADDRESSED_BY_LNAME_ENG"].ConvertToString();
                    objCase.CASE_ADDRESSED_BY_FullNAME_ENG = objCase.CASE_ADDRESSED_BY_FNAME_ENG + " " + objCase.CASE_ADDRESSED_BY_MNAME_ENG + " " + objCase.CASE_ADDRESSED_BY_LNAME_ENG;
                    objCase.CASE_ADDRESSED_DATE_LOC = ds.Tables[0].Rows[0]["CASE_ADDRESSED_DATE_LOC"].ConvertToString();
                    objCase.HOUSE_LAND_LEGAL_OTH_COMMENT = ds.Tables[0].Rows[0]["HOUSE_LAND_LEGAL_OTHER_COMMENT"].ConvertToString();

                    if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows != null)
                    {
                        List<CaseType> lstcaseType = new List<CaseType>();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            CaseType objcaseType = new CaseType();
                            objcaseType.DefinedCd = dr["CASE_GRIEVANCE_TYPE_CD"].ConvertToString();
                            objcaseType.DescEng = dr["DESC_ENG"].ConvertToString();
                            objcaseType.DescLoc = dr["DESC_LOC"].ConvertToString();
                            objcaseType.Remarks = dr["REMARKS"].ConvertToString();
                            lstcaseType.Add(objcaseType);
                        }
                        objCase.CaseRegistrationList = lstcaseType;
                    }
                    if (ds.Tables[2].Rows.Count > 0 && ds.Tables[2].Rows != null)
                    {
                        List<CaseGrievanceOTHDetail> lstOTHDetail = new List<CaseGrievanceOTHDetail>();
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            CaseGrievanceOTHDetail objOthDetail = new CaseGrievanceOTHDetail();
                            objOthDetail.HOUSEHOLD_HEAD_FNAME_ENG = dr["HOUSE_OWNER_FIRST_NAME_ENG"].ConvertToString();
                            objOthDetail.HOUSEHOLD_HEAD_FNAME_LOC = dr["HOUSE_OWNER_FIRST_NAME_LOC"].ConvertToString();
                            objOthDetail.HOUSEHOLD_HEAD_MNAME_ENG = dr["HOUSE_OWNER_MIDDLE_NAME_ENG"].ConvertToString();
                            objOthDetail.HOUSEHOLD_HEAD_MNAME_LOC = dr["HOUSE_OWNER_MIDDLE_NAME_LOC"].ConvertToString();
                            objOthDetail.HOUSEHOLD_HEAD_LNAME_ENG = dr["HOUSE_OWNER_LAST_NAME_ENG"].ConvertToString();
                            objOthDetail.HOUSEHOLD_HEAD_LNAME_LOC = dr["HOUSE_OWNER_LAST_NAME_LOC"].ConvertToString();
                            objOthDetail.HOUSEHOLD_HEAD_FULL_NAME = objOthDetail.HOUSEHOLD_HEAD_FNAME_ENG + " " + objOthDetail.HOUSEHOLD_HEAD_MNAME_ENG + " " + objOthDetail.HOUSEHOLD_HEAD_LNAME_ENG;
                            objOthDetail.HOUSE_DIST_ENG = dr["DISTRICT_ENG"].ConvertToString();
                            objOthDetail.HOUSE_DIST_LOC = dr["DISTRICT_LOC"].ConvertToString();
                            objOthDetail.HOUSE_VDC_ENG = dr["VDC_ENG"].ConvertToString();
                            objOthDetail.HOUSE_VDC_LOC = dr["VDC_LOC"].ConvertToString();
                            objOthDetail.HOUSE_WARD_NO = dr["HOUSE_WARD_NO"].ToDecimal();
                            objOthDetail.HOUSE_AREA = dr["HOUSE_AREA"].ConvertToString();
                            objOthDetail.HOUSE_CONDITION_ENG = dr["BUILDING_CONDITION_ENG"].ConvertToString();
                            objOthDetail.HOUSE_CONDITION_LOC = dr["BUILDING_CONDITION_LOC"].ConvertToString();
                            lstOTHDetail.Add(objOthDetail);
                        }
                        objCase.CaseGrievanceOthDtl = lstOTHDetail;
                    }
                    if (ds.Tables[3].Rows.Count > 0 && ds.Tables[3].Rows != null)
                    {
                        List<NHRSGrievanceDocType> lstDocType = new List<NHRSGrievanceDocType>();
                        foreach (DataRow dr in ds.Tables[3].Rows)
                        {
                            NHRSGrievanceDocType objDocType = new NHRSGrievanceDocType();
                            objDocType.definedcd = dr["GRIEVANCE_DOC_TYPE_CD"].ToDecimal();
                            objDocType.desceng = dr["DESC_ENG"].ConvertToString();
                            objDocType.descloc = dr["DESC_LOC"].ConvertToString();
                            lstDocType.Add(objDocType);
                        }
                        objCase.DocumentTypeDetail = lstDocType;
                    }

                }


            }
            catch (SqlException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View(objCase);
        }
        public ActionResult GetOwner(string District, string VDC, string Ward, string NissaNo, string RegistrationID)
        {
            NHRSCaseGrievanceService objGrievanceService = new NHRSCaseGrievanceService();
            OwnerInfo obj = new OwnerInfo();
            CommonFunction cnmFunc = new CommonFunction();
            DataTable dt = new DataTable();
            string id = string.Empty;
            //dt = objGrievanceService.GetOwnerDetail(District,VDC,Ward,NissaNo,Name,MobileNo);

            if (District != "undefined")
            {
                obj.district_Cd = District;
            }
            else
            {
                obj.district_Cd = DBNull.Value.ConvertToString();
            }
            if (VDC != "undefined")
            {
                obj.vdc_mun_Cd = commonFC.GetDefinedCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            }
            else
            {
                obj.vdc_mun_Cd = DBNull.Value.ConvertToString();
            }
            if (NissaNo != "undefined")
            {
                obj.nissa_no = NissaNo;
            }
            else
            {
                obj.nissa_no = DBNull.Value.ConvertToString();
            }
            if (RegistrationID != "undefined")
            {
                obj.RegistrationID = RegistrationID;
            }
            else
            {
                obj.RegistrationID = DBNull.Value.ConvertToString();
            }
            if (Ward != "undefined")
            {
                obj.ward_no = Ward;
            }
            else
            {
                obj.ward_no = DBNull.Value.ConvertToString();
            }
            ViewData["ddl_Districtss"] = commonFC.GetDistricts(obj.district_Cd.ConvertToString());

            ViewData["ddl_VDCMunn"] = commonFC.GetVDCMunByDistrict(obj.vdc_mun_Cd, obj.district_Cd.ConvertToString());
            ViewData["ddl_WardPer"] = commonFC.GetWardByVDCMun("","");
            return PartialView("~/Views/CaseGrievance/_GetOwnerDetail.cshtml", obj);
        }

        public ActionResult GetOwnerDetail(string District, string VDC, string Ward, string NissaNo, string Name, string MobileNo, string CaseRegistrationID,string GrievanceWard)
        {

            NHRSCaseGrievanceService objGrievanceService = new NHRSCaseGrievanceService();
            OwnerInfo obj = new OwnerInfo();
            CommonFunction cnmFunc = new CommonFunction();
            DataTable dt = new DataTable();
            string id = string.Empty;
            obj.district_Cd = District;
            obj.vdc_mun_Cd = VDC;
            obj.ward_no = Ward;
            obj.nissa_no = NissaNo;
            obj.full_name = Name;
            obj.mobile_no = MobileNo;
            obj.RegistrationID = CaseRegistrationID;
            obj.Grievance_ward = GrievanceWard;
            string VDCCode = commonFC.GetCodeFromDataBase(VDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            dt = objGrievanceService.GetOwnerDetail(District, VDCCode, Ward, NissaNo, Name, MobileNo);
            ViewData["ddl_Districtss"] = commonFC.GetDistricts(obj.district_Cd);
            ViewData["ddl_VDCMunn"] = commonFC.GetVDCMunByDistrict(obj.vdc_mun_Cd, obj.district_Cd);
            ViewData["ddl_WardPer"] = commonFC.GetWardByVDCMun(obj.ward_no, obj.vdc_mun_Cd);
            ViewData["OwnerDetail"] = dt;
            return PartialView("~/Views/CaseGrievance/_GetOwnerDetail.cshtml", obj);
        }

        public ActionResult BeneficiaryList(string p)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();
            CaseGrievanceModel cgm = new CaseGrievanceModel();
            if (CommonVariables.GroupCD == "42")
            {
                if (CommonVariables.EmpCode != "")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_District"] = commonFC.GetDistrictsByDistrictCode(Districtcode);
                    cgm.DistrictCd = commonFC.GetDefinedCodeFromDataBase(cgm.VDCMun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, Districtcode);
                    cgm.DistrictCd = Districtcode.ConvertToString();
                }
                else
                {
                    ViewData["ddl_District"] = commonFC.GetDistricts(cgm.DistrictCd.ConvertToString());
                    cgm.VDCMun = commonFC.GetDefinedCodeFromDataBase(cgm.VDCMun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, cgm.DistrictCd.ConvertToString());
                }
            }
            else
            {
                ViewData["ddl_District"] = commonFC.GetDistricts(cgm.DistrictCd.ConvertToString());
                ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, cgm.DistrictCd.ConvertToString());
            }
            ViewData["ddlApprove"] = commonFC.GetApprove("");
            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");
            ViewData["ddl_CaseGrievanceType"] = commonFC.getcaseGrievanceType("");
            ViewData["BatchID"] = commonFC.GetGrievanceTargetBatchID("", "");
            return View("BeneficiaryList", cgm);
        }


        [HttpPost]
        public ActionResult BeneficiaryList(FormCollection fc)
        {
            CaseGrievanceModel model = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            DataTable searchResult = new DataTable();
            model.DistrictCd = fc["DistrictCd"].ConvertToString();
            model.VDCMun = commonFC.GetCodeFromDataBase(fc["VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            model.Ward = fc["Ward"].ConvertToString();
            model.ddlApprove = fc["ddlApprove"].ConvertToString();
            //string CaseType = fc["CaseType"].ConvertToString();
            if (model.ddlApprove == "Y")
            {
                model.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                model.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                ViewData["BatchID"] = commonFC.GetBatchId("", "");
                ViewData["ddlApprove"] = commonFC.GetApprove("");
                model.BatchID = fc["BatchID"].ConvertToString();
                model.ddlApprove = fc["ddlApprove"].ConvertToString();
                Session["Param"] = model;
                DataTable dtEnroll = new DataTable();
                searchResult = objService.GetBeneficiariesDeatils(model);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
                //dtEnroll = objBeneficiary.GetBeneficiariesDeatilsEnroll(objSearch);
                //Session["dtEnroll"] = dtEnroll;
            }
            if (model.ddlApprove == "N")
            {
                model.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
                model.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMunCd"].ConvertToString());
                ViewData["BatchID"] = commonFC.GetBatchId("", "");
                ViewData["ddlApprove"] = commonFC.GetApprove("");
                model.BatchID = fc["BatchID"].ConvertToString();
                searchResult = objService.GetBeneficiariesDeatils(model);
                ViewData["searchResult"] = searchResult;
                Session["dtBeneficiary"] = searchResult;
            }
            return PartialView("_GrievanceBeneficiaryList",model);
        }

        public ActionResult GrievanceTargeting(string p)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();
            CaseGrievanceModel cgm = new CaseGrievanceModel();
            if (CommonVariables.GroupCD == "42")
            {
                if (CommonVariables.EmpCode != "")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_District"] = commonFC.GetDistrictsByDistrictCode(Districtcode);
                    cgm.DistrictCd = commonFC.GetDefinedCodeFromDataBase(cgm.VDCMun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, Districtcode);
                    cgm.DistrictCd = Districtcode.ConvertToString();
                }
                else
                {
                    ViewData["ddl_District"] = commonFC.GetDistricts(cgm.DistrictCd.ConvertToString());
                    cgm.VDCMun = commonFC.GetDefinedCodeFromDataBase(cgm.VDCMun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                    ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, cgm.DistrictCd.ConvertToString());
                }
            }
            else
            {
                ViewData["ddl_District"] = commonFC.GetDistricts(cgm.DistrictCd.ConvertToString());
                ViewData["ddl_VDCMun"] = commonFC.GetVDCMunByDistrict(cgm.VDCMun, cgm.DistrictCd.ConvertToString());
            }
            ViewData["ddl_Ward"] = commonFC.GetWardByVDCMun("", "");
            ViewData["BatchID"] = commonFC.GetGrievanceBatchID("", "");
            ViewData["ddl_Targeted"] = commonFC.GetTargetType2("");
            return View("GrievanceTargeting", cgm);
        }
        [HttpPost]
        public ActionResult GrievanceTargeting(FormCollection fc)
        {
            CaseGrievanceModel model = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();
            DataTable result = new DataTable();
            model.DistrictCd = fc["DistrictCd"].ConvertToString();
            model.VDCMun = commonFC.GetCodeFromDataBase(fc["VDCMun"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            model.Ward = fc["Ward"].ConvertToString();
            string Targeted = fc["ddl_Targeted"].ConvertToString();
            model.BatchID = fc["Batch"].ConvertToString();
            ViewData["Targeted"] = Targeted;
            //string CaseType = fc["CaseType"].ConvertToString();
            if (Targeted == "A")
            {
                result = objService.GetGrievanceNewApplicantSearch(model);
                ViewData["result"] = result;
                Session["dtEligible"] = result;
                return PartialView("~/Views/ProvertyTrageting/_GrievanceTargeting.cshtml", model);
            }
            if (Targeted == "Y")
            {
                result = objService.GetGrievanceEligibleSearch(model);
                ViewData["result"] = result;
                Session["dtEligible"] = result;
                return PartialView("~/Views/ProvertyTrageting/_GrievanceTargeting.cshtml", model);
            }
            if (Targeted == "N")
            {
                result = objService.GetGrievanceNonEligibleSearch(model);
                ViewData["result"] = result;
                Session["dtEligible"] = result;
                return PartialView("~/Views/ProvertyTrageting/_GrievanceTargeting.cshtml", model);
            }
            
            result = objService.GetGrievanceTargeting(model);
            return PartialView("_GrievanceTargetingList");
        }
        public ActionResult GrievanceTargetingEligible(string p, FormCollection fc)
        {
            CaseGrievanceModel model = new CaseGrievanceModel();
            NHRSCaseGrievanceService objService = new NHRSCaseGrievanceService();

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
            success = objService.GrievanceTargetingSearchEligible(session_id, out excout, out TotalTargeted);

            if (success.IsSuccess)
            {
                TempData["Message"] = "You have Successfully Targeted " + TotalTargeted + " Beneficiary";
            }
            if (excout == "20099")
            {
                TempData["ErrMessage"] = "Eligible Beneficiary not found for Targeting.";
            }
            return RedirectToAction("GrievanceTargeting");
        }

        public ActionResult beneficiaryapproved(string p, FormCollection fc)
        {
            NHRSCaseGrievanceService objCaseService = new NHRSCaseGrievanceService();
          
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
                        success = objCaseService.BeneficiaryApprovedAll(Targeting_batch_id, HouseOwnerID);
                    }
                    else
                    {
                        success = objCaseService.BeneficiaryApproved(Targeting_batch_id, HouseOwnerID);
                    }

                }
            }


            if (success.IsSuccess)
            {
                TempData["ApprovedMessage"] = "Approved Successfully.";
            }
            return RedirectToAction("BeneficiaryList");
        }
    }
}
