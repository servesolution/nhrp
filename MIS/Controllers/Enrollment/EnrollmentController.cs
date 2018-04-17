using MIS.Models.Registration.Household;
using MIS.Services;
using MIS.Services.Core;
using MIS.Services.NHRP.FileImport;
using MIS.Services.NHRP.Edit;
using MIS.Services.Registration.Household;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MIS.Models.Search;
using System.Data;
using MIS.Services.NHRP.Search;
using MIS.Services.NHRP.View;
using MIS.Models.NHRP.View;
using MIS.Services.Report;
using MIS.Models.Core;
using MIS.Models.NHRP;
using MIS.Models.Enrollment;
using ExceptionHandler;
using MIS.Services.Enrollment;
using MIS.Services.BusinessCalculation;
using MIS.Models.Security;
using MIS.Services.Security;
using MIS.Authentication;
using MIS.Models.Accounting.Setup;
//using PdfSharp.Pdf;
//using PdfSharp;
using EntityFramework;
using System.Globalization;
using MIS.Models.Setup;
using MIS.Models.Enrollment.EnrollmentPopup;
//using PdfSharp.Drawing;

using System.Diagnostics;
using MIS.Services.Setup;
//using Microsoft.VisualBasic.FileIO;
using System.Text.RegularExpressions;
using System.Text;
using CsvHelper;
using System.Configuration;
using System.Data.OleDb;
using System.Net;
using System.Web.Http;
using MIS.Models.Report;
using MIS.Services.Inspection;
using SelectPdf;
//using System.Web.Mvc.Controller.File;



namespace MIS.Controllers
{

    public class EnrollmentController : BaseController
    {
        CommonFunction common = new CommonFunction();
        HouseholdService househead = new HouseholdService();
        public ActionResult EnrollmentForm(string p)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();
            benificiary objbenifiDtl = new benificiary();

            DataTable dt = new DataTable();
            EnrollmentService objEnrollment = new EnrollmentService();
            string id = string.Empty;
            try
            {
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id2"] != null)
                        {
                            id = (rvd["id2"].ConvertToString());
                        }

                    }

                }

                DataSet result = new DataSet();
                result = objEnrollment.EnrollmentDetail(id);
                if (result.Tables[0] != null)
                {

                    if (result.Tables[0].Rows.Count > 0)
                    {

                        List<HouseOwner> ownerList = new List<HouseOwner>();
                        int benifiIncrement = 0;
                        foreach (DataRow dr in result.Tables[0].Rows)
                        {
                            HouseOwner houseownerlist = new HouseOwner();
                            //benificiary objOthHouse = new benificiary();
                            houseownerlist.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                            houseownerlist.MEMBER_ID = dr["MEMBER_ID"].ConvertToString();
                            houseownerlist.FIRST_NAME_ENG = dr["FIRST_NAME_ENG"].ConvertToString();
                            houseownerlist.MIDDLE_NAME_ENG = dr["MIDDLE_NAME_ENG"].ConvertToString();
                            houseownerlist.SNO = dr["SNO"].ToDecimal();
                            houseownerlist.LAST_NAME_ENG = dr["LAST_NAME_ENG"].ConvertToString();
                            houseownerlist.FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                            houseownerlist.FIRST_NAME_LOC = dr["FIRST_NAME_LOC"].ConvertToString();
                            houseownerlist.MIDDLE_NAME_LOC = dr["MIDDLE_NAME_LOC"].ConvertToString();
                            houseownerlist.LAST_NAME_LOC = dr["LAST_NAME_LOC"].ConvertToString();
                            // houseownerlist.IDENTIFICATION_ISSUE_DT = dr["IDENTIFICATION_ISSUE_DT"].ConvertToString();
                            houseownerlist.MEMBER_PHOTO_ID = dr["MEMBER_PHOTO_ID"].ConvertToString();
                            houseownerlist.GENDER_CD = dr["GENDER_CD"].ToDecimal();
                            houseownerlist.MARITAL_STATUS_CD = dr["MARITAL_STATUS_CD"].ToDecimal();
                            houseownerlist.HOUSEHOLD_HEAD = dr["HOUSEHOLD_HEAD"].ConvertToString();
                            houseownerlist.HOUSEHOLD_ID = dr["HOUSEHOLD_ID"].ConvertToString();
                            houseownerlist.HOUSEHOLD_DEFINED_CD = dr["HOUSEHOLD_DEFINED_CD"].ConvertToString();

                            benifiIncrement++;

                            //ownerList.Add(objbenifiDtl);
                            ownerList.Add(houseownerlist);

                        }
                        objbenifiDtl.houseownerList = ownerList;
                    }
                }


                if (result.Tables[1] != null)
                {
                    if (result.Tables[1].Rows.Count > 0)
                    {
                        List<benificiary> BenfList = new List<benificiary>();
                        int benifiIncrement = 0;
                        foreach (DataRow dr in result.Tables[1].Rows)
                        {
                            benificiary benficiaryList = new benificiary();
                            //benificiary objOthHouse = new benificiary();
                            benficiaryList.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                            benficiaryList.BENEFICIARY_TYPE_CD = dr["BENEFICIARY_TYPE_CD"].ToDecimal();
                            benficiaryList.BENEFICIARY_FNAME_ENG = dr["BENEFICIARY_FNAME_ENG"].ConvertToString();
                            benficiaryList.BENEFICIARY_MNAME_ENG = dr["BENEFICIARY_MNAME_ENG"].ConvertToString();
                            benficiaryList.BENEFICIARY_LNAME_ENG = dr["BENEFICIARY_LNAME_ENG"].ConvertToString();
                            benficiaryList.BENEFICIARY_FULLNAME_ENG = dr["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                            benficiaryList.BENEFICIARY_FNAME_LOC = dr["BENEFICIARY_FNAME_LOC"].ConvertToString();
                            benficiaryList.BENEFICIARY_MNAME_LOC = dr["BENEFICIARY_MNAME_LOC"].ConvertToString();
                            benficiaryList.BENEFICIARY_LNAME_LOC = dr["BENEFICIARY_LNAME_LOC"].ConvertToString();
                            benficiaryList.BENEFICIARY_FULLNAME_LOC = dr["BENEFICIARY_FULLNAME_LOC"].ConvertToString();
                            benficiaryList.FATHER_RELATION_TYPE_CD = dr["FATHER_RELATION_TYPE_CD"].ConvertToString();
                            benficiaryList.FATHER_FULLNAME_ENG = dr["FATHER_FULLNAME_ENG"].ConvertToString();
                            benficiaryList.FATHER_FULLNAME_LOC = dr["FATHER_FULLNAME_LOC"].ConvertToString();

                            benficiaryList.GFATHER_FULLNAME_ENG = dr["GFATHER_FULLNAME_ENG"].ConvertToString();
                            benficiaryList.GFATHER_FULLNAME_LOC = dr["GFATHER_FULLNAME_LOC"].ConvertToString();
                            benficiaryList.ENUMERATOR_ID = dr["ENUMERATOR_ID"].ToDecimal();
                            benficiaryList.BUILDING_AREA_ENG = dr["BUILDING_AREA_ENG"].ConvertToString();
                            benficiaryList.BUILDING_AREA_LOC = dr["BUILDING_AREA_LOC"].ConvertToString();
                            benficiaryList.BUILDING_AREA = dr["BUILDING_AREA"].ConvertToString();
                            benficiaryList.IDENTIFICATION_TYPE_CD = dr["IDENTIFICATION_TYPE_CD"].ToDecimal();
                            benficiaryList.IDENTIFICATION_NO = dr["IDENTIFICATION_NO"].ConvertToString();
                            benficiaryList.IDENTIFICATION_DOCUMENT = dr["IDENTIFICATION_DOCUMENT"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISSUE_DIS_CD = dr["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISSUE_DAY = dr["IDENTIFICATION_ISSUE_DAY"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISSUE_MONTH = dr["IDENTIFICATION_ISSUE_MONTH"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISSUE_YEAR = dr["IDENTIFICATION_ISSUE_YEAR"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISSUE_DT = dr["IDENTIFICATION_ISSUE_DT"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISS_DAY_LOC = dr["IDENTIFICATION_ISS_DAY_LOC"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISS_MNTH_LOC = dr["IDENTIFICATION_ISS_MNTH_LOC"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISS_YEAR_LOC = dr["IDENTIFICATION_ISS_YEAR_LOC"].ConvertToString();
                            benficiaryList.IDENTIFICATION_ISS_DT_LOC = dr["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                            benficiaryList.NOMINEE_FULLNAME_ENG = dr["NOMINEE_FULLNAME_ENG"].ConvertToString();
                            benficiaryList.NOMINEE_FULLNAME_LOC = dr["NOMINEE_FULLNAME_LOC"].ConvertToString();



                            benifiIncrement++;
                            BenfList.Add(benficiaryList);


                        }
                        objbenifiDtl.beneficiaryList = BenfList;
                    }
                }
                if (result.Tables[2] != null)
                {
                    if (result.Tables[2].Rows.Count > 0)
                    {

                        objbenifiDtl.proxyList[0].BENEFICIARY_FULLNAME_ENG = result.Tables[2].Columns["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                        objbenifiDtl.proxyList[0].BENEFICIARY_FULLNAME_ENG = result.Tables[2].Columns["BENEFICIARY_FULLNAME_LOC"].ConvertToString();
                        objbenifiDtl.proxyList[0].FATHER_FULLNAME_ENG = result.Tables[2].Columns["FATHER_FULLNAME_ENG"].ConvertToString();
                        objbenifiDtl.proxyList[0].FATHER_FULLNAME_LOC = result.Tables[2].Columns["FATHER_FULLNAME_LOC"].ConvertToString();
                        objbenifiDtl.proxyList[0].GFATHER_FULLNAME_ENG = result.Tables[2].Columns["GFATHER_FULLNAME_ENG"].ConvertToString();
                        objbenifiDtl.proxyList[0].GFATHER_FULLNAME_LOC = result.Tables[2].Columns["GFATHER_FULLNAME_LOC"].ConvertToString();
                        objbenifiDtl.proxyList[0].IDENTIFICATION_NO = result.Tables[2].Columns["IDENTIFICATION_NO"].ConvertToString();
                        objbenifiDtl.proxyList[0].IDENTIFICATION_ISSUE_DIS_ENG = result.Tables[2].Columns["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString();
                        objbenifiDtl.proxyList[0].IDENTIFICATION_ISSUE_DT = result.Tables[2].Columns["IDENTIFICATION_ISSUE_DT"].ConvertToString();

                        //List<proxy> proxList = new List<proxy>();
                        //int benifiIncrement = 0;
                        //foreach (DataRow dr in result.Tables[2].Rows)
                        //{
                        //    proxy prox = new proxy();
                        //    //benificiary objOthHouse = new benificiary();
                        //    prox.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                        //    prox.BENEFICIARY_TYPE_CD = dr["BENEFICIARY_TYPE_CD"].ToDecimal();
                        //    prox.BENEFICIARY_FNAME_ENG = dr["BENEFICIARY_FNAME_ENG"].ConvertToString();
                        //    prox.BENEFICIARY_MNAME_ENG = dr["BENEFICIARY_MNAME_ENG"].ConvertToString();
                        //    prox.BENEFICIARY_LNAME_ENG = dr["BENEFICIARY_LNAME_ENG"].ConvertToString();
                        //    prox.BENEFICIARY_FULLNAME_ENG = dr["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                        //    prox.BENEFICIARY_FNAME_LOC = dr["BENEFICIARY_FNAME_LOC"].ConvertToString();
                        //    prox.BENEFICIARY_MNAME_LOC = dr["BENEFICIARY_MNAME_LOC"].ConvertToString();
                        //    prox.BENEFICIARY_LNAME_LOC = dr["BENEFICIARY_LNAME_LOC"].ConvertToString();
                        //    prox.BENEFICIARY_FULLNAME_LOC = dr["BENEFICIARY_FULLNAME_LOC"].ConvertToString();
                        //    prox.FATHER_RELATION_TYPE_CD = dr["FATHER_RELATION_TYPE_CD"].ToDecimal();
                        //    prox.FATHER_FULLNAME_ENG = dr["FATHER_FULLNAME_ENG"].ToString();
                        //    prox.FATHER_FULLNAME_LOC = dr["FATHER_FULLNAME_LOC"].ToString();
                        //    prox.GFATHER_FULLNAME_ENG = dr["GFATHER_FULLNAME_ENG"].ConvertToString();
                        //    prox.GFATHER_FULLNAME_LOC = dr["GFATHER_FULLNAME_LOC"].ConvertToString();
                        //    prox.ENUMERATOR_ID = dr["ENUMERATOR_ID"].ConvertToString();
                        //    prox.BUILDING_AREA_ENG = dr["BUILDING_AREA_ENG"].ConvertToString();
                        //    prox.BUILDING_AREA_LOC = dr["BUILDING_AREA_LOC"].ConvertToString();
                        //    prox.BUILDING_AREA = dr["BUILDING_AREA"].ConvertToString();
                        //    prox.IDENTIFICATION_TYPE_CD = dr["IDENTIFICATION_TYPE_CD"].ToDecimal();
                        //    prox.IDENTIFICATION_NO = dr["IDENTIFICATION_NO"].ConvertToString();
                        //    prox.IDENTIFICATION_DOCUMENT = dr["IDENTIFICATION_DOCUMENT"].ConvertToString();
                        //    prox.IDENTIFICATION_ISSUE_DIS_CD = dr["IDENTIFICATION_ISSUE_DIS_CD"].ToDecimal();
                        //    prox.IDENTIFICATION_ISSUE_DAY = dr["IDENTIFICATION_ISSUE_DAY"].ConvertToString();
                        //    prox.IDENTIFICATION_ISSUE_MONTH = dr["IDENTIFICATION_ISSUE_MONTH"].ConvertToString();
                        //    prox.IDENTIFICATION_ISSUE_YEAR = dr["IDENTIFICATION_ISSUE_YEAR"].ConvertToString();
                        //    prox.IDENTIFICATION_ISSUE_DT = dr["IDENTIFICATION_ISSUE_DT"].ConvertToString();
                        //    prox.IDENTIFICATION_ISS_DAY_LOC = dr["IDENTIFICATION_ISS_DAY_LOC"].ConvertToString();
                        //    prox.IDENTIFICATION_ISS_MNTH_LOC = dr["IDENTIFICATION_ISS_MNTH_LOC"].ConvertToString();
                        //    prox.IDENTIFICATION_ISS_YEAR_LOC = dr["IDENTIFICATION_ISS_YEAR_LOC"].ConvertToString();
                        //    prox.IDENTIFICATION_ISS_DT_LOC = dr["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                        //    prox.NOMINEE_FULLNAME_ENG = dr["NOMINEE_FULLNAME_ENG"].ConvertToString();
                        //    prox.NOMINEE_FULLNAME_LOC = dr["NOMINEE_FULLNAME_LOC"].ConvertToString();



                        //    benifiIncrement++;
                        //    proxList.Add(prox);


                        //}
                        //objbenifiDtl.proxyList = proxList;
                    }
                }
                if (result.Tables[3] != null)
                {
                    if (result.Tables[3].Rows.Count > 0)
                    {

                        objbenifiDtl.nomineeList[0].NOMINEE_FULLNAME_ENG = result.Tables[3].Columns["NOMINEE_FULLNAME_ENG"].ConvertToString();
                        objbenifiDtl.nomineeList[0].NOMINEE_FULLNAME_LOC = result.Tables[3].Columns["NOMINEE_FULLNAME_LOC"].ConvertToString();
                        objbenifiDtl.nomineeList[0].NOMINEE_RELATION_TYPE_CD = result.Tables[3].Columns["NOMINEE_RELATION_TYPE_CD"].ToDecimal();
                        //objbenifiDtl.nomineeList[0].NOMINEE_RELATION_TYPE_LOC = result.Tables[3].Columns["NOMINEE_FULLNAME_LOC"].ConvertToString();
                        //List<nominee> nomineeList = new List<nominee>();
                        //int benifiIncrement = 0;
                        //foreach (DataRow dr in result.Tables[3].Rows)
                        //{
                        //    nominee nominee = new nominee();
                        //    nominee.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                        //    nominee.NOMINEE_MEMBER_ID = dr["NOMINEE_MEMBER_ID"].ConvertToString();
                        //    nominee.NOMINEE_FNAME_ENG = dr["NOMINEE_FNAME_ENG"].ConvertToString();
                        //    nominee.NOMINEE_MNAME_ENG = dr["NOMINEE_MNAME_ENG"].ConvertToString();
                        //    nominee.NOMINEE_LNAME_ENG = dr["NOMINEE_LNAME_ENG"].ConvertToString();
                        //    //nominee.NOMINEE_FULLNAME_ENG = dr["NOMINEE_FULLNAME_ENG"].ConvertToString();
                        //    nominee.NOMINEE_FNAME_LOC = dr["NOMINEE_FNAME_LOC"].ConvertToString();
                        //    nominee.NOMINEE_MNAME_LOC = dr["NOMINEE_FNAME_LOC"].ConvertToString();
                        //    nominee.NOMINEE_LNAME_LOC = dr["NOMINEE_FNAME_LOC"].ConvertToString();
                        //   // nominee.NOMINEE_FULLNAME_LOC = dr["NOMINEE_FULLNAME_LOC"].ConvertToString();
                        //    nominee.NOMINEE_RELATION_TYPE_CD = dr["NOMINEE_RELATION_TYPE_CD"].ToDecimal();
                        //    nominee.NOMINEE_FULLNAME_ENG = dr["NOMINEE_FULLNAME_ENG"].ConvertToString();
                        //    nominee.NOMINEE_FULLNAME_LOC = dr["NOMINEE_FULLNAME_LOC"].ConvertToString();
                        //    benifiIncrement++;
                        //    nomineeList.Add(nominee);
                        //}
                        //objbenifiDtl.nomineeList = nomineeList;
                    }
                }

                if (result.Tables[4] != null)
                {
                    if (result.Tables[4].Rows.Count > 0)
                    {
                        objbenifiDtl.bankList[0].BANK_CD = result.Tables[4].Columns["BANK_CD"].ToDecimal();
                        //objbenifiDtl.bankList[0].BANK_LOC = result.Tables[4].Columns["BANK_CD"].ConvertToString();
                        objbenifiDtl.bankList[0].BANK_BRANCH_CD = result.Tables[4].Columns["BANK_BRANCH_CD"].ToDecimal();
                        //objbenifiDtl.bankList[0].BANK_BRANCH_LOC = result.Tables[4].Columns["BANK_CD"].ConvertToString();
                        objbenifiDtl.bankList[0].BANK_ACC_TYPE_CD = result.Tables[4].Columns["BANK_ACC_TYPE_CD"].ToDecimal();
                        //objbenifiDtl.bankList[0].BANK_ACC_TYPE_LOC = result.Tables[4].Columns["BANK_CD"].ConvertToString();
                        objbenifiDtl.bankList[0].BANK_ACC_NO = result.Tables[4].Columns["BANK_ACC_NO"].ConvertToString();

                        //List<bank> bankList = new List<bank>();
                        //int enrollIncrement = 0;
                        //foreach (DataRow dr in result.Tables[4].Rows)
                        //{
                        //    bank bank = new bank();
                        //    bank.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                        //    bank.BANK_CD = dr["BANK_CD"].ToDecimal();
                        //   // bank.BANK_NAME = common.GetDescriptionFromCode(dr["BANK_CD"].ConvertToString(), "NHRS_BANK", "DESC_ENG");
                        //    bank.BANK_BRANCH_CD = dr["BANK_BRANCH_CD"].ToDecimal();
                        //    bank.BANK_ACC_NO = dr["BANK_ACC_NO"].ConvertToString();
                        //    bank.BANK_ACC_TYPE_CD = dr["BANK_ACC_TYPE_CD"].ToDecimal();

                        //    enrollIncrement++;
                        //    bankList.Add(bank);
                        //}
                        //objbenifiDtl.bankList = bankList;
                    }
                }
                if (result.Tables[5] != null)
                {
                    if (result.Tables[5].Rows.Count > 0)
                    {
                        objbenifiDtl.employeeList[0].FULL_NAME_ENG = result.Tables[5].Columns["FULL_NAME_ENG"].ConvertToString();
                        objbenifiDtl.employeeList[0].FULL_NAME_ENG = result.Tables[5].Columns["POSITION_CD"].ConvertToString();


                        //List<employee> employeeList = new List<employee>();
                        //int empIncrement = 0;
                        //foreach (DataRow dr in result.Tables[5].Rows)
                        //{
                        //    employee objemployeelist = new employee();
                        //    objemployeelist.HOUSE_OWNER_ID = dr["HOUSE_OWNER_ID"].ConvertToString();
                        //    objemployeelist.EMPLOYEE_CD = dr["EMPLOYEE_CD"].ToDecimal();
                        //    objemployeelist.DEF_EMPLOYEE_CD = dr["DEF_EMPLOYEE_CD"].ToDecimal();
                        //    objemployeelist.FIRST_NAME_ENG = dr["FIRST_NAME_ENG"].ConvertToString();
                        //    objemployeelist.MIDDLE_NAME_ENG = dr["MIDDLE_NAME_ENG"].ConvertToString();
                        //    objemployeelist.LAST_NAME_ENG = dr["LAST_NAME_ENG"].ConvertToString();
                        //    objemployeelist.FULL_NAME_ENG = dr["FULL_NAME_ENG"].ConvertToString();
                        //    objemployeelist.FIRST_NAME_LOC = dr["FULL_NAME_ENG"].ConvertToString();
                        //    objemployeelist.MIDDLE_NAME_LOC = dr["FULL_NAME_ENG"].ConvertToString();
                        //    objemployeelist.LAST_NAME_LOC = dr["FULL_NAME_ENG"].ConvertToString();
                        //    objemployeelist.FULL_NAME_LOC = dr["FULL_NAME_ENG"].ConvertToString();
                        //    objemployeelist.OFFICE_CD = dr["OFFICE_CD"].ToDecimal();
                        //    objemployeelist.SECTION_CD = dr["SECTION_CD"].ToDecimal();
                        //    objemployeelist.OFFICE_JOIN_DT = dr["OFFICE_JOIN_DT"].ConvertToString();
                        //    objemployeelist.OFFICE_JOIN_DT_LOC = dr["OFFICE_JOIN_DT_LOC"].ConvertToString();
                        //    objemployeelist.POSITION_CD = dr["POSITION_CD"].ConvertToString();
                        //    objemployeelist.POS_SUB_CLASS_CD = dr["POS_SUB_CLASS_CD"].ToDecimal();
                        //    objemployeelist.DESIGNATION_CD = dr["DESIGNATION_CD"].ToDecimal();
                        //    //objemployeelist.POSITION_ENG = common.GetDescriptionFromCode(dr["POSITION_CD"].ConvertToString(), "MIS_POSITION", "DESC_ENG");
                        //    objemployeelist.REMARKS = dr["REMARKS"].ConvertToString();
                        //    objemployeelist.REMARKS_LOC = dr["REMARKS_LOC"].ConvertToString();
                        //    empIncrement++;
                        //    employeeList.Add(objemployeelist);
                        //}
                        //objbenifiDtl.employeeList = employeeList;
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


            return View(objbenifiDtl);
        }
        public ActionResult EnrollmentView()
        {
            enrollmentclass obj = new enrollmentclass();
            //ViewData["ddl_District"] = common.GetDistricts("");
            //ViewData["dl_VDCMunPer"] = common.GetRegionState("");
            ViewData["ddlbeneficiary"] = common.GetBeneficiary("");
            ViewData["ddl_Relation"] = common.GetRelation1("");

            ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(obj.DISTRICT_CD);
            ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(obj.VDC_MUN_CD, obj.DISTRICT_CD);
            ViewData["ddl_WardPer"] = common.GetWardByVdcForUser("");
            ViewData["ddl_MOU"] = (List<SelectListItem>)common.GetYesNo("").Data;
            ViewData["ddl_PS"] = (List<SelectListItem>)common.GetYesNo("").Data;
            ViewData["IDENTIFICATION_TYPE"] = househead.GetIdentificationType("");
            ViewData["ddl_Identification"] = househead.GetIdentificationType("");
            ViewData["ddl_bank_name"] = common.GetBankName("");
            ViewData["ddl_bank_branch_name"] = common.GetBankBranchId("");
            ViewData["ddl_bank_acc_type"] = common.GetBankAccountType("");
            return View();

            //return View(obj);
        }


        public ActionResult EnrollmentList(string p, string DistrictCd)
        {
            DataTable lastSearchResults = new DataTable();
            EnrollmentSearch objEnrollmentSearch = new EnrollmentSearch();

            string distCode = "";
            RouteValueDictionary rvd = new RouteValueDictionary();
            string batchId = string.Empty;
            string fiscalYear = string.Empty;
            string districtCd = string.Empty;
            string periodTypeCd = string.Empty;
            string periodCnt = string.Empty;
            string SATId = string.Empty;
            string SPId = String.Empty;

            EnrollmentService objEnrollment = new EnrollmentService();
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
                        if (CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "1")
                        {
                            string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                            objEnrollmentSearch.DistrictCd = Districtcode;
                        }
                    }
                    else
                    {
                        distCode = GetData.GetCodeFor(DataType.District, districtCd);
                    }
                    //get Quota for the district



                    objEnrollmentSearch.DistrictCd = distCode;
                    if (!string.IsNullOrWhiteSpace(distCode))
                    {
                        string Districtcode = CommonFunction.GetDistrictByOfficeCode(CommonVariables.EmpCode);
                        ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(Districtcode);
                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict(objEnrollmentSearch.VDCMun, Districtcode);
                        ViewData["txtDistrict"] = GetData.GetDefinedCodeFor(DataType.District, distCode);
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                        ViewData["ddlTarget"] = (List<SelectListItem>)common.GetBatchType("");
                        ViewData["ddlMouSigned"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;
                        ViewData["ddlEnrolled"] = (List<SelectListItem>)common.GetEnrollmentStatus("").Data;
                        ViewData["ddlBankname"] = common.GetBankName("", "");
                        ViewData["ddlBranchId"] = common.GetBankBranchId("", "");
                        ViewData["ddlprintStatus"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;
                        ViewData["ddlIsPaymentRecCh"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;
                        ViewData["ddlBankAccountType"] = common.GetBankAccountType("", "");
                        ViewData["ddlapproved"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;

                    }
                    else
                    {
                        ViewData["ddl_District"] = common.GetDistricts("");

                        ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");
                        ViewData["ddl_Ward"] = common.GetWardByVDCMun("", "");
                        ViewData["ddlTarget"] = (List<SelectListItem>)common.GetTargetID("");
                        ViewData["ddlMouSigned"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;
                        ViewData["ddlEnrolled"] = (List<SelectListItem>)common.GetEnrollmentStatus("").Data;
                        ViewData["ddlBankname"] = common.GetBankName("", "");
                        ViewData["ddlBranchId"] = common.GetBankBranchId("", "");
                        ViewData["ddlprintStatus"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;
                        ViewData["ddlIsPaymentRecCh"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;
                        ViewData["ddlBankAccountType"] = common.GetBankAccountType("", "");
                        ViewData["ddlapproved"] = (List<SelectListItem>)common.GetMOUYesNo("").Data;


                    }
                    string fisc = CommonFunction.GetRecentFiscalYear(DateTime.Now.ToString("dd-MMM-yyyy"));
                    ViewData["txtEnrollmentmouDateFrom"] = common.GetFiscalYear(fisc);
                    ViewData["ddlIfPaUpload"] = (List<SelectListItem>)common.GetPaUploadYesNo("").Data;

                }
                else
                {

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
            ViewData["ResearchEnrollment"] = TempData["ResearchEnrollment"];
            ViewData["EnrollmentAddResponse"] = Session["EnrollmentAdded"].ConvertToString();
            ViewData["AddErrorMessage"] = Session["AddErrorMessage"].ConvertToString();
            Session["AddErrorMessage"]=null;
            Session["EnrollmentAdded"] = null;
            ViewData["Message"] = TempData["Message"];
            return View(objEnrollmentSearch);
        }

 
        [HttpPost]
        public ActionResult EnrollmentList(EnrollmentSearch objEnrollment, FormCollection fc)
        {
            CheckPermission();
            EnrollmentService enrollService = new EnrollmentService();
            SearchService serSearch = new SearchService();
            objEnrollment = new EnrollmentSearch();
            DataTable result = new DataTable();

            objEnrollment.Entrollment_ID = fc["Entrollment_ID"].ConvertToString();
            // objEnrollment.houseSerialNO = fc["houseSerialNO"].ConvertToString();

            objEnrollment.targetBatchId = fc["ddlTarget"].ConvertToString();

            objEnrollment.House_Owner_ID = fc["House_Owner_ID"].ConvertToString();

            objEnrollment.House_Owner_Name = fc["House_Owner_Name"].ConvertToString();
            string fullName = objEnrollment.House_Owner_Name;
            var names = fullName.Split(' ');

            int count1 = names.Count();
            if (names[0] != "")
            {
                if (count1 > 2)
                {
                    objEnrollment.House_Owner_Name = names[0] + " " + names[1] + " " + names[2];
                }
                if (count1 == 2)
                {
                    objEnrollment.House_Owner_Name = names[0] + "  " + names[1];
                }

            }

            objEnrollment.Building_Structure_No = fc["Building_Structure_No"].ConvertToString();
            objEnrollment.Enrollment_Status = fc["ddlEnrolled"].ConvertToString();
            if (objEnrollment.Enrollment_Status == "Enrolled")
                objEnrollment.Enrollment_Status = "Y";
            if (objEnrollment.Enrollment_Status == "NotEnrolled")
                objEnrollment.Enrollment_Status = "N";
            if (objEnrollment.Enrollment_Status == "All")
                objEnrollment.Enrollment_Status = "A";


            objEnrollment.Enrollment_Dt_From = fc["Enrollment_Dt_From"].ConvertToString();
            objEnrollment.Enrollment_Dt_To = fc["Enrollment_Dt_To"].ConvertToString();
            //objEnrollment.M_Signed_Status = fc["M_Signed_Status"].ConvertToString();
            //objEnrollment.M_Signed_Status = fc["ddlMouSigned"].ConvertToString();
            //objEnrollment.Approved = fc["ddlapproved"].ConvertToString();
            objEnrollment.Enrollment_M_Dt_From = fc["Enrollment_M_Dt_From"].ConvertToString();
            objEnrollment.Enrollment_M_Dt_To = fc["Enrollment_M_Dt_To"].ConvertToString();
            objEnrollment.DistrictCd = GetData.GetCodeFor(DataType.District, fc["DistrictCd"].ConvertToString());
            objEnrollment.VDCMun = GetData.GetCodeFor(DataType.VdcMun, fc["VDCMun"].ConvertToString());
            objEnrollment.WardNo = fc["WardNo"].ConvertToString();
            objEnrollment.Bank_CD = fc["Bank_CD"].ConvertToString();
            objEnrollment.Bank_Branch_CD = fc["Bank_Branch_CD"].ConvertToString();

            //objEnrollment.BENEFICIARY_ID = fc["TB1"].ConvertToString() + "-" + fc["TB2"].ConvertToString() + "-" + fc["TB3"].ConvertToString() + "-" + fc["TB4"].ConvertToString() + "-" + fc["TB5"].ConvertToString();
            objEnrollment.BENEFICIARY_ID = fc["TB1"].ConvertToString();
            objEnrollment.NISSA_NO = fc["NISSA_NO"].ConvertToString();
            objEnrollment.ENUMERATION_AREA = fc["ENUMERATION_AREA"].ConvertToString();

            if (CommonVariables.GroupCD != "1" && CommonVariables.GroupCD != "15" && CommonVariables.GroupCD != "24" && CommonVariables.GroupCD != "33" && CommonVariables.GroupCD != "39")
            {
                objEnrollment.Action = false;
                ViewBag.PrintStatus = "false";
            }
            else
            {
                objEnrollment.Action = true;
                ViewBag.PrintStatus = "true";
            }
            if (fc["ddlprintStatus"].ConvertToString() == "All")
            {
                objEnrollment.Print_Status = "";
            }
            else
            {
                objEnrollment.Print_Status = fc["ddlprintStatus"].ConvertToString();
            }

            if (fc["ddlMouSigned"].ConvertToString() == "All")
            {
                objEnrollment.M_Signed_Status = "";
            }
            else
            {
                objEnrollment.M_Signed_Status = fc["ddlMouSigned"].ConvertToString();
            }

            if (fc["ddlapproved"].ConvertToString() == "All")
            {
                objEnrollment.Approved = "";
            }
            else
            {
                objEnrollment.Approved = fc["ddlapproved"].ConvertToString();
            }
           string  paUpload = fc["ddlIfPaUpload"].ConvertToString();
           result = enrollService.GetEnrollmentSearchDetail(objEnrollment, paUpload);
            ViewData["result"] = result;
            Session["dtEnrollment"] = result;
            Session["objSession"] = objEnrollment;
            ViewData["Message"] = fc["hdnMessage"].ToString();

            return PartialView("~/Views/Enrollment/_EnrollmentInfo.cshtml", objEnrollment);


        }
        public ActionResult EnrollmentEdit(EnrollmentSearch model, string p)
        {
            DataTable dt = new DataTable();
            string enrollmentid = string.Empty;
            string targetbatchid = string.Empty;
            string targetingid = string.Empty;
            string Houseid = string.Empty;
            string HouseOwnerID = string.Empty;
            string structureNo = string.Empty;
            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            RouteValueDictionary rvd = new RouteValueDictionary();

            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            EnrollmentAdd objenrolladd = new EnrollmentAdd();

            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["enrollmentid"] != null)
                    {
                        enrollmentid = (rvd["enrollmentid"]).ConvertToString();
                    }
                    if (rvd["HouseOwnerID"] != null)
                    {
                        HouseOwnerID = (rvd["HouseOwnerID"]).ConvertToString();
                    }
                }
            }



            dt = objEnrollServices.GetEnrollmentDetailEdit(HouseOwnerID, enrollmentid);

            if (dt != null && dt.Rows.Count > 0)
            {

                objenrolladd.ModeType = "U";
                objenrolladd.DISTRICT_CD = dt.Rows[0]["DISTRICT_CD"].ToDecimal();
                objenrolladd.DISTRICT_NAME = Utils.ToggleLanguage(dt.Rows[0]["DISTRICT_ENG"].ConvertToString(), dt.Rows[0]["DISTRICT_LOC"].ConvertToString());
                objenrolladd.VDC_MUN_CD = dt.Rows[0]["VDC_MUN_CD"].ConvertToString();
                objenrolladd.VDC_MUN_NAME = Utils.ToggleLanguage(dt.Rows[0]["VDC_MUN_ENG"].ConvertToString(), dt.Rows[0]["VDC_MUN_LOC"].ConvertToString());
                objenrolladd.AREA = Utils.ToggleLanguage(dt.Rows[0]["AREA_ENG1"].ConvertToString(), dt.Rows[0]["AREA_LOC"].ConvertToString());
                objenrolladd.AREA_ENG = dt.Rows[0]["AREA_ENG"].ConvertToString();
                objenrolladd.AREA_LOC = dt.Rows[0]["AREA_LOC"].ConvertToString();
                objenrolladd.ENUMERATOR_ID = dt.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                objenrolladd.WARD_NO = dt.Rows[0]["WARD_NO"].ConvertToString();
                objenrolladd.BUILDING_STRUCTURE_NO = dt.Rows[0]["BUILDING_STRUCTURE_NO"].ConvertToString();

                BeneficiaryService objservice = new BeneficiaryService();
                DataTable dtt = new DataTable();
                dtt = objservice.GetOwner(dt.Rows[0]["HOUSE_OWNER_ID"].ConvertToString());
                if (dtt != null)
                {
                    if (dtt.Rows.Count > 0)
                    {
                        objenrolladd.HOUSE_OWNER_FIRST_NAME_ENG = dtt.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_MIDDLE_NAME_ENG = dtt.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_LAST_NAME_ENG = dtt.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_NAME_ENG = dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_FIRST_NAME_LOC = dtt.Rows[0]["FIRST_NAME_LOC"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_MIDDLE_NAME_LOC = dtt.Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_LAST_NAME_LOC = dtt.Rows[0]["LAST_NAME_LOC"].ConvertToString();
                        objenrolladd.HOUSE_OWNER_NAME_LOC = dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString();
                    }
                }
                objenrolladd.HOUSE_ID = dt.Rows[0]["HOUSE_ID"].ConvertToString();
                objenrolladd.HOUSEHOLD_ID = dt.Rows[0]["HOUSEHOLD_ID"].ConvertToString();
                objenrolladd.HOUSE_OWNER_ID = dt.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                objenrolladd.ENROLLMENT_ID = dt.Rows[0]["ENROLLMENT_ID"].ConvertToString();
                objenrolladd.TARGETING_ID = dt.Rows[0]["TARGETING_ID"].ConvertToString();
                objenrolladd.NRA_DEFINED_CD = dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                objenrolladd.TARGET_BATCH_ID = dt.Rows[0]["TARGET_BATCH_ID"].ConvertToString();
                objenrolladd.NO_OF_HOUSE_OWNER = dt.Rows[0]["TOTAL_HOUSE_OWNER_CNT"].ConvertToString();
                objenrolladd.FISCAL_YR = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                objenrolladd.MOU_ID = dt.Rows[0]["MOU_ID"].ConvertToString();
                objenrolladd.SURVEY_NO = dt.Rows[0]["SURVEY_NO"].ConvertToString();

                objenrolladd.BUILDING_DISTRICT_CD = GetData.GetDefinedCodeFor(DataType.District, dt.Rows[0]["BUILDING_DISTRICT_CD"].ConvertToString()).ToDecimal();
                objenrolladd.BUILDING_VDC_MUN_CD = GetData.GetDefinedCodeFor(DataType.VdcMun, dt.Rows[0]["BUILDING_VDC_MUN_CD"].ConvertToString()).ConvertToString();
                objenrolladd.BUILDING_WARD_NO = dt.Rows[0]["BUILDING_WARD_NO"].ConvertToString();

                objenrolladd.BENEFICIARY_FNAME_ENG = dt.Rows[0]["BENEFICIARY_FNAME_ENG"].ConvertToString();
                objenrolladd.BENEFICIARY_MNAME_ENG = dt.Rows[0]["BENEFICIARY_MNAME_ENG"].ConvertToString();
                objenrolladd.BENEFICIARY_LNAME_ENG = dt.Rows[0]["BENEFICIARY_LNAME_ENG"].ConvertToString();
                objenrolladd.BENEFICIARY_FULLNAME_ENG = dt.Rows[0]["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                objenrolladd.BENEFICIARY_FNAME_LOC = dt.Rows[0]["BENEFICIARY_FNAME_LOC"].ConvertToString();
                objenrolladd.BENEFICIARY_MNAME_LOC = dt.Rows[0]["BENEFICIARY_MNAME_LOC"].ConvertToString();
                objenrolladd.BENEFICIARY_LNAME_LOC = dt.Rows[0]["BENEFICIARY_LNAME_LOC"].ConvertToString();
                objenrolladd.BENEFICIARY_FULLNAME_LOC = dt.Rows[0]["BENEFICIARY_FULLNAME_LOC"].ConvertToString();
                objenrolladd.Beneficiary_Photo = dt.Rows[0]["BENEFICIARY_PHOTO"].ConvertToString();
                objenrolladd.BENEFICIARY_TYPE_CD = dt.Rows[0]["BENEFICIARY_TYPE_CD"].ConvertToString();
                objenrolladd.BIRTH_DT = (dt.Rows[0]["BENEFICIARY_DOB_ENG"]).ConvertToString();
                objenrolladd.BIRTH_DT_LOC = dt.Rows[0]["BENEFICIARY_DOB_LOC"].ConvertToString("yyyy-mm-dd");
                objenrolladd.FATHER_MEMBER_ID = dt.Rows[0]["FATHER_MEMBER_ID"].ConvertToString();
                objenrolladd.FATHER_FNAME_ENG = dt.Rows[0]["FATHER_FNAME_ENG"].ConvertToString();
                objenrolladd.FATHER_MNAME_ENG = dt.Rows[0]["FATHER_MNAME_ENG"].ConvertToString();
                objenrolladd.FATHER_LNAME_ENG = dt.Rows[0]["FATHER_LNAME_ENG"].ConvertToString();
                objenrolladd.FATHER_FullNAME_ENG = dt.Rows[0]["FATHER_FULLNAME_ENG"].ConvertToString();
                objenrolladd.FATHER_FNAME_LOC = dt.Rows[0]["FATHER_FNAME_LOC"].ConvertToString();
                objenrolladd.FATHER_MNAME_LOC = dt.Rows[0]["FATHER_MNAME_LOC"].ConvertToString();
                objenrolladd.FATHER_LNAME_LOC = dt.Rows[0]["FATHER_LNAME_LOC"].ConvertToString();
                objenrolladd.FATHER_FullNAME_LOC = dt.Rows[0]["FATHER_FULLNAME_LOC"].ConvertToString();
                objenrolladd.GFATHER_MEMBER_ID = dt.Rows[0]["GFATHER_MEMBER_ID"].ConvertToString();
                objenrolladd.GFATHER_FNAME_ENG = dt.Rows[0]["GFATHER_FNAME_ENG"].ConvertToString();
                objenrolladd.GFATHER_MNAME_ENG = dt.Rows[0]["GFATHER_MNAME_ENG"].ConvertToString();
                objenrolladd.GFATHER_LNAME_ENG = dt.Rows[0]["GFATHER_LNAME_ENG"].ConvertToString();
                objenrolladd.GFATHER_FullNAME_ENG = dt.Rows[0]["GFATHER_FULLNAME_ENG"].ConvertToString();
                objenrolladd.GFATHER_FNAME_LOC = dt.Rows[0]["GFATHER_FNAME_LOC"].ConvertToString();
                objenrolladd.GFATHER_MNAME_LOC = dt.Rows[0]["GFATHER_MNAME_LOC"].ConvertToString();
                objenrolladd.GFATHER_LNAME_LOC = dt.Rows[0]["GFATHER_LNAME_LOC"].ConvertToString();
                objenrolladd.GFATHER_FullNAME_LOC = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();
                objenrolladd.IDENTIFICATION_NO = dt.Rows[0]["IDENTIFICATION_NO"].ConvertToString();
                objenrolladd.IDENTIFICATION_ISSUE_DT = (dt.Rows[0]["IDENTIFICATION_ISSUE_DT"]).ConvertToString();
                objenrolladd.IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                objenrolladd.PROXY_FNAME_ENG = dt.Rows[0]["MANJURINAMA_FNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_MNAME_ENG = dt.Rows[0]["MANJURINAMA_MNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_LNAME_ENG = dt.Rows[0]["MANJURINAMA_LNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_FULLNAME_ENG = dt.Rows[0]["MANJURINAMA_FULLNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_FNAME_LOC = dt.Rows[0]["MANJURINAMA_FNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_MNAME_LOC = dt.Rows[0]["MANJURINAMA_MNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_LNAME_LOC = dt.Rows[0]["MANJURINAMA_LNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_FULLNAME_LOC = dt.Rows[0]["MANJURINAMA_FULLNAME_LOC"].ConvertToString();
                objenrolladd.IDENTIFICATION_ISSUE_DIS_CD = GetData.GetDefinedCodeFor(DataType.AllDistrict, dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString()).ToDecimal(); 

                objenrolladd.BANK_CD = common.GetValueFromDataBase(dt.Rows[0]["BANK_CD"].ConvertToString(), "NHRS_BANK", "BANK_CD", "DEFINED_CD").ConvertToString(); //dt.Rows[0]["BANK_CD"].ToDecimal();
                objenrolladd.BANK_BRANCH_CD = common.GetValueFromDataBase(dt.Rows[0]["BANK_BRANCH_CD"].ConvertToString(), "NHRS_BANK_BRANCH", "BANK_BRANCH_CD", "DEFINED_CD").ConvertToString(); //dt.Rows[0]["BANK_BRANCH_CD"].ToDecimal();
                

                objenrolladd.REMARKS = dt.Rows[0]["REMARKS"].ConvertToString();
                objenrolladd.REMARKS_LOC = dt.Rows[0]["REMARKS_LOC"].ConvertToString(); 
            }
             
            ViewData["ddl_DistrictBenfIdentity"] = common.GetAllDistricts(objenrolladd.IDENTIFICATION_ISSUE_DIS_CD.ConvertToString());
            ViewData["ddlmarriage"] = common.GetMigration("");
            ViewData["ddl_bank_name"] = common.GetBankName(objenrolladd.BANK_CD.ConvertToString());
            ViewData["ddl_bank_branch_name"] = common.GetBankBranchId(objenrolladd.BANK_BRANCH_CD.ConvertToString());
          
            return View("EnrollmentView", objenrolladd);
        }
        public ActionResult EnrollmentAdd(EnrollmentAdd model, string p)
        {

            DataTable dt = new DataTable();
            string enrollmentid = string.Empty;
            string targetbatchid = string.Empty;
            string targetingid = string.Empty;
            string Houseid = string.Empty;
            string structureNo = string.Empty;
            string HouseholdID = string.Empty;
            string HouseOwnerID = string.Empty;
            string mode = string.Empty;
            string PA = string.Empty;
            HouseOwner HO = new HouseOwner();
            proxy pr = new proxy();
            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            EnrollmentAdd objenrolladd = new EnrollmentAdd();
            RouteValueDictionary rvd = new RouteValueDictionary();
            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["Houseownerid"] != null)
                    {
                        HouseOwnerID = (rvd["Houseownerid"]).ConvertToString();
                        Session["HouseOwnerID"] = HouseOwnerID;
                        PA = (rvd["Pa"]).ConvertToString();
                    }

                }
            }
            objenrolladd.ModeType = "I";
            dt = objEnrollAddServices.GetEnrollmentAdd(Session["HouseOwnerID"].ConvertToString(), PA);
            if (dt != null && dt.Rows.Count > 0)
            {
                objenrolladd.FISCAL_YR = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                //TempData["Fiscal_YR"] = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                //Session["Fiscal_YR"] = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                objenrolladd.DISTRICT_CD = dt.Rows[0]["DISTRICT_CD"].ToDecimal();
                objenrolladd.DISTRICT_NAME = Utils.ToggleLanguage(dt.Rows[0]["DISTRICT_ENG"].ConvertToString(), dt.Rows[0]["DISTRICT_LOC"].ConvertToString());
                objenrolladd.VDC_MUN_CD = dt.Rows[0]["VDC_MUN_CD"].ConvertToString();
                objenrolladd.VDC_MUN_NAME = Utils.ToggleLanguage(dt.Rows[0]["VDC_MUN_ENG"].ConvertToString(), dt.Rows[0]["VDC_MUN_LOC"].ConvertToString());
                objenrolladd.AREA = Utils.ToggleLanguage(dt.Rows[0]["AREA_ENG"].ConvertToString(), dt.Rows[0]["AREA_LOC"].ConvertToString());
                objenrolladd.AREA_ENG = dt.Rows[0]["AREA_ENG"].ConvertToString();
                objenrolladd.AREA_LOC = dt.Rows[0]["AREA_LOC"].ConvertToString();
                objenrolladd.ENUMERATOR_ID = dt.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                objenrolladd.WARD_NO = dt.Rows[0]["WARD_NO"].ConvertToString();
                // objenrolladd.BUILDING_STRUCTURE_NO = dt.Rows[0]["BUILDING_STRUCTURE_NO"].ConvertToString();
                objenrolladd.BENEFICIARY_FULLNAME_ENG = dt.Rows[0]["HOUSE_OWNER_NAME_ENG"].ConvertToString();

                BeneficiaryService objservice = new BeneficiaryService();
                DataTable dtt = new DataTable();
              
                objenrolladd.HOUSEHOLD_ID = dt.Rows[0]["HOUSEHOLD_ID"].ConvertToString();
                objenrolladd.HOUSE_OWNER_ID = dt.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                objenrolladd.ENROLLMENT_ID = dt.Rows[0]["ENROLLMENT_ID"].ConvertToString();
                objenrolladd.TARGETING_ID = dt.Rows[0]["TARGETING_ID"].ConvertToString();
                objenrolladd.NRA_DEFINED_CD = dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                objenrolladd.TARGET_BATCH_ID = dt.Rows[0]["TARGET_BATCH_ID"].ConvertToString();
                objenrolladd.NO_OF_HOUSE_OWNER = dt.Rows[0]["TOTAL_HOUSE_OWNER_CNT"].ConvertToString();
                objenrolladd.FISCAL_YR = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                objenrolladd.MOU_ID = dt.Rows[0]["MOU_ID"].ConvertToString();
                objenrolladd.SURVEY_NO = dt.Rows[0]["SURVEY_NO"].ConvertToString();
                objenrolladd.HOUSE_ID = dt.Rows[0]["HOUSE_ID"].ConvertToString();
                objenrolladd.NISSA_NO = dt.Rows[0]["INSTANCE_UNIQUE_SNO"].ConvertToString();
                #region ADD BY NABIN
                // objenrolladd.BIRTH_DT = (dt.Rows[0]["BENEFICIARY_DOB_ENG"]).ToDateTime();
                //objenrolladd.BIRTH_DT_LOC = dt.Rows[0]["BENEFICIARY_DOB_LOC"].ConvertToString();
                //objenrolladd.PHONE_NO = dt.Rows[0]["BENEFICIARY_PHONE"].ConvertToString();
                // objenrolladd.migrationdate = System.Convert.ToDateTime(dt.Rows[0]["BENEFICIARY_MIGRATTION_DT"]).ToString("yyyy-MM-dd");//dt.Rows[0]["BENEFICIARY_MIGRATTION_DT"].ToDateTime();
                // objenrolladd.migrationdateloc = dt.Rows[0]["BENEFICIARY_MIGRATION_DT_LOC"].ConvertToString();
                //objenrolladd.migrationno = dt.Rows[0]["BENEFICIARY_MIGRATION_NO"].ConvertToString();
                // objenrolladd.FATHER_MEMBER_ID = dt.Rows[0]["FATHER_MEMBER_ID"].ConvertToString();
                //  objenrolladd.FATHER_FNAME_ENG = dt.Rows[0]["FATHER_FNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_MNAME_ENG = dt.Rows[0]["FATHER_MNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_LNAME_ENG = dt.Rows[0]["FATHER_LNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_FullNAME_ENG = dt.Rows[0]["FATHER_FULLNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_FNAME_LOC = dt.Rows[0]["FATHER_FNAME_LOC"].ConvertToString();
                //  objenrolladd.FATHER_MNAME_LOC = dt.Rows[0]["FATHER_MNAME_LOC"].ConvertToString();
                //  objenrolladd.FATHER_LNAME_LOC = dt.Rows[0]["FATHER_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.FATHER_FullNAME_LOC = dt.Rows[0]["FATHER_FULLNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_MEMBER_ID = dt.Rows[0]["GFATHER_MEMBER_ID"].ConvertToString();
                //  objenrolladd.GFATHER_FNAME_ENG = dt.Rows[0]["GFATHER_FNAME_ENG"].ConvertToString();
                //  objenrolladd.GFATHER_MNAME_ENG = dt.Rows[0]["GFATHER_MNAME_ENG"].ConvertToString();
                //  objenrolladd.GFATHER_LNAME_ENG = dt.Rows[0]["GFATHER_LNAME_ENG"].ConvertToString();
                // // objenrolladd.GFATHER_FullNAME_ENG = dt.Rows[0]["GFATHER_FULLNAME_ENG"].ConvertToString();
                //  //objenrolladd.GFATHER_FNAME_LOC = dt.Rows[0]["GFATHER_FNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_MNAME_LOC = dt.Rows[0]["GFATHER_MNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_LNAME_LOC = dt.Rows[0]["GFATHER_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_FullNAME_LOC = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();

                //  objenrolladd.husbandfnameeng = dt.Rows[0]["SPOUSE_FIRST_NAME_ENG"].ConvertToString();
                //  objenrolladd.husbandMnameeng = dt.Rows[0]["SPOUSE_MIDDLE_NAME_ENG"].ConvertToString();
                //  objenrolladd.husbandLnameeng = dt.Rows[0]["SPOUSE_LAST_NAME_ENG"].ConvertToString();
                //  //objenrolladd.husbandFullnameEng = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();
                //  objenrolladd.FinlawFnameEng = dt.Rows[0]["FATHER_INLAW_FNAME_ENG"].ConvertToString();
                //  objenrolladd.FinlawMnameEng = dt.Rows[0]["FATHER_INLAW_MNAME_ENG"].ConvertToString();
                //  objenrolladd.FinlawLnameEng = dt.Rows[0]["FATHER_INLAW_LNAME_ENG"].ConvertToString();
                ////  objenrolladd.FinLawFullNameEng = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();

                //  //objenrolladd.BUILDING_KITTA_NUMBER = dt.Rows[0]["BUILDING_KITTA_NUMBER"].ConvertToString();
                //  //objenrolladd.BUILDING_AREA = dt.Rows[0]["BUILDING_AREA"].ConvertToString();

                //  //objenrolladd.BUILDING_AREA_ENG = dt.Rows[0]["BUILDING_AREA_ENG"].ConvertToString();
                //  //objenrolladd.BUILDING_AREA_LOC = dt.Rows[0]["BUILDING_AREA_LOC"].ConvertToString();
                //  //objenrolladd.BUILDING_WALL_OR_PILLAR_TYPE_NO = dt.Rows[0]["BUILDING_PILER_TYPE"].ConvertToString();
                //  //objenrolladd.BUILDING_FLOOR_OR_ROOF_TYPE_NO = dt.Rows[0]["BUILDING_FLOOR_ROOF_TYPE"].ConvertToString();
                //  //objenrolladd.BUILDING_DESIGN_OTHER = dt.Rows[0]["BUILDING_OTHER_DEG"].ConvertToString();

                //  //objenrolladd.NOMINEE_FNAME_ENG = dt.Rows[0]["NOMINEE_FNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_MNAME_ENG = dt.Rows[0]["NOMINEE_MNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_LNAME_ENG = dt.Rows[0]["NOMINEE_LNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_FULLNAME_ENG = dt.Rows[0]["NOMINEE_FULLNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_FNAME_LOC = dt.Rows[0]["NOMINEE_FNAME_LOC"].ConvertToString();
                //  //objenrolladd.NOMINEE_MNAME_LOC = dt.Rows[0]["NOMINEE_MNAME_LOC"].ConvertToString();
                //  //objenrolladd.NOMINEE_LNAME_LOC = dt.Rows[0]["NOMINEE_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.NOMINEE_FULLNAME_LOC = dt.Rows[0]["NOMINEE_FULLNAME_LOC"].ConvertToString();
                //  // objenrolladd.IDENTIFICATION_TYPE_CD = common.GetValueFromDataBase(dt.Rows[0]["IDENTIFICATION_TYPE_CD"].ConvertToString(), "NHRS_IDENTIFICATION_TYPE", "IDENTIFICATION_TYPE_CD", "DEFINED_CD").ToDecimal();//dt.Rows[0]["IDENTIFICATION_TYPE_CD"].ToDecimal();
                //  objenrolladd.IDENTIFICATION_NO = dt.Rows[0]["IDENTIFICATION_NO"].ConvertToString();
                //  // objenrolladd.IDENTIFICATION_DOCUMENT = dt.Rows[0]["IDENTIFICATION_DOCUMENT"].ConvertToString();
                //  objenrolladd.IDENTIFICATION_ISSUE_DIS_CD = GetData.GetDefinedCodeFor(DataType.AllDistrict, dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString()).ToDecimal();//dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"].ToDecimal();
                //  //objenrolladd.IDENTIFICATION_ISSUE_DT = System.Convert.ToDateTime(dt.Rows[0]["IDENTIFICATION_ISSUE_DT"]).ToDateTime("yyyy-MM-dd");//dt.Rows[0]["IDENTIFICATION_ISSUE_DT"].ToDateTime();
                //  objenrolladd.IDENTIFICATION_ISSUE_DT = (dt.Rows[0]["IDENTIFICATION_ISSUE_DT"]).ToDateTime();
                //  objenrolladd.IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                //  //objenrolladd.IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                //  // objenrolladd.PROXY_RELATION_TYPE_CD = common.GetValueFromDataBase(dt.Rows[0]["MANJURINAMA_RELATION_TYPE_CD"].ConvertToString(), "MIS_RELATION_TYPE", "RELATION_TYPE_CD", "DEFINED_CD").ToDecimal();//dt.Rows[0]["MANJURINAMA_RELATION_TYPE_CD"].ToDecimal();
                //  //objenrolladd.PROXY_FNAME_ENG = dt.Rows[0]["MANJURINAMA_FNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_MNAME_ENG = dt.Rows[0]["MANJURINAMA_MNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_LNAME_ENG = dt.Rows[0]["MANJURINAMA_LNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_FULLNAME_ENG = dt.Rows[0]["MANJURINAMA_FULLNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_FNAME_LOC = dt.Rows[0]["MANJURINAMA_FNAME_LOC"].ConvertToString();
                //  //objenrolladd.PROXY_MNAME_LOC = dt.Rows[0]["MANJURINAMA_MNAME_LOC"].ConvertToString();
                //  //objenrolladd.PROXY_LNAME_LOC = dt.Rows[0]["MANJURINAMA_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.PROXY_FULLNAME_LOC = dt.Rows[0]["MANJURINAMA_FULLNAME_LOC"].ConvertToString();
                #endregion
            }

            
            string Districtcode = objenroll.DISTRICT_CD;
            string VdcMun = objenroll.VDC_MUN_CD;
           
            ViewData["ddl_DistrictBenfIdentity"] = common.GetAllDistricts("");
            

            ViewData["ddl_bank_name"] = common.GetBankByAddress("", objenroll.VDC_MUN_CD, objenroll.WARD_NO, objenroll.DISTRICT_CD);
            ViewData["ddl_bank_branch_name"] = common.GetBankBranchId("");
           

            return View("EnrollmentView", objenrolladd);

        }
        
        public bool DeleteEnrollmentDocument(string docType, string HOUSE_OWNER_ID)
        {
            bool retValue = false;
            try
            {
                EnrollmentService objEnrollServices = new EnrollmentService();
                retValue = objEnrollServices.DeleteEnrollmentDocument(docType, HOUSE_OWNER_ID);
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return retValue;
        }


        [HttpPost]
        public void PDFDownload(FormCollection fc, string q)
        {
            EnrollmentService objEnrollServices = new EnrollmentService();
            EnrollmentSearch objenroll = new EnrollmentSearch();
            PasLetterFormat objletter = new PasLetterFormat();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string cutOff = string.Empty;
            string letterFormat = fc["qletterformet"].ConvertToString();
            string hhIds = fc["CheckedHHIds"].ConvertToString();
            string[] arrHHIDs = hhIds.Split(',');
            string[] arrHHIDs1 = hhIds.Split(',');
            string id = string.Empty;
            string id2 = string.Empty;
            string id3 = string.Empty;
            string id4 = string.Empty;
            string id5 = string.Empty;

            string PA = string.Empty;
            //   int i = 0;
            string strHTML = string.Empty;
            string strHTML1 = string.Empty;
            string pdffilepath = string.Empty;
            try
            {
                if (hhIds == "")
                {
                    hhIds = letterFormat;
                    arrHHIDs = hhIds.Split();
                    arrHHIDs1 = hhIds.Split();

                }
                foreach (string HHIDs in arrHHIDs)
                {
                    rvd = QueryStringEncrypt.DecryptString(HHIDs);
                    if (rvd != null)
                    {
                        if (rvd["HouseOwnerID"] != null)
                        {
                            id = (rvd["HouseOwnerID"]).ConvertToString();
                        }
                        if (rvd["targetbatchid"] != null)
                        {
                            id2 = (rvd["targetbatchid"]).ConvertToString();
                        }
                        if (rvd["targetingid"] != null)
                        {
                            id3 = (rvd["targetingid"]).ConvertToString();
                        }
                        if (rvd["Houseid"] != null)
                        {
                            id4 = (rvd["Houseid"]).ConvertToString();
                        }
                        if (rvd["structureNo"] != null)
                        {
                            id5 = (rvd["structureNo"]).ConvertToString();
                        }
                        if (rvd["Pa"] != null)
                        {
                            PA = (rvd["Pa"]).ConvertToString();
                        }
                        
                    }
                    if (id.ConvertToString() != "")
                    {
                        
                        //string qParamEdit = QueryStringEncrypt.EncryptString("/?CutOffCd=" + cutOff.ConvertToString() + "&HouseholdId=" + HHIDs + "&editFlag=U" + "&noise=" + new Random().Next(5000, 10000));
                        objenroll = objEnrollServices.GenerateCertificateData(id, PA);
                        objenroll.docData = objEnrollServices.getPrintStatus("MN");
                        BeneficiaryService objservice = new BeneficiaryService();
                        DataTable dtt = new DataTable();
                        dtt = objservice.GetOwner(objenroll.House_Owner_ID);
                        if (dtt != null)
                        {
                            if (dtt.Rows.Count > 0)
                            {
                                objenroll.House_Owner_Name = dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString();
                                objenroll.House_Owner_Name_Loc = dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString();
                            }
                        }
                        if (CommonVariables.EmpCode != "")
                        {
                            DataTable dtVdcSec = objservice.GetVdcSecretary(CommonVariables.EmpCode);
                            if (dtVdcSec != null)
                            {
                                if (dtVdcSec.Rows.Count > 0)
                                {
                                    objenroll.VDC_SECRETARY_ENG = dtVdcSec.Rows[0]["FIRST_NAME_ENG"].ConvertToString() + " " + dtVdcSec.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString() + " " + dtVdcSec.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                                    objenroll.VDC_SECRETARY_DISTRICT_ENG = dtVdcSec.Rows[0]["DESC_ENG"].ConvertToString();
                                    objenroll.VDC_SECRETARY_VDC_ENG = dtVdcSec.Rows[0]["DESC_ENG1"].ConvertToString();
                                    objenroll.VDC_SECRETARY_WARD_NO = dtVdcSec.Rows[0]["PER_WARD_NO"].ConvertToString();
                                    objenroll.VDC_SECRETARY_POSITION_ENG = dtVdcSec.Rows[0]["DESC_ENG2"].ConvertToString();
                                }
                            }
                        }

                        objletter.docData = Server.HtmlDecode(objenroll.docData);
                        String template = objletter.docData;
                        LatterFormatTemplate report = new LatterFormatTemplate(template);
                        //Newly added parameters for report
                        report.AddAttribute("District", objenroll.DISTRICT_LOC.ConvertToString());
                        report.AddAttribute("VDC", objenroll.VDC_LOC.ConvertToString());
                        report.AddAttribute("Ward", objenroll.WardNo.ConvertToString());
                        report.AddAttribute("Area", objenroll.AREA_LOC.ConvertToString());
                        report.AddAttribute("BeneficiaryNameEng", objenroll.House_Owner_Name.ConvertToString());
                        report.AddAttribute("BeneficiaryNameLoc", objenroll.House_Owner_Name_Loc.ConvertToString());
                        report.AddAttribute("VDCSecretaryName", objenroll.VDC_SECRETARY_ENG.ConvertToString());
                        report.AddAttribute("VDCSecDistrict", objenroll.VDC_SECRETARY_DISTRICT_ENG.ConvertToString());
                        report.AddAttribute("VDCSecVDC", objenroll.VDC_SECRETARY_VDC_ENG.ConvertToString());
                        report.AddAttribute("VDCSecWard", objenroll.VDC_SECRETARY_WARD_NO.ConvertToString());
                        report.AddAttribute("VDCSecPosition", objenroll.VDC_SECRETARY_POSITION_ENG.ConvertToString());
                        report.AddAttribute("HouseID", objenroll.House_Owner_ID.ConvertToString());
                        report.AddAttribute("HouseholdID", objenroll.HOUSEHOLD_ID.ConvertToString());
                        report.AddAttribute("BeneficiaryID", objenroll.BENEFICIARY_ID.ConvertToString());

                        //RESTORE THE VALUE OF SESSIONLANGUAGE TO NEPALI
                        string tempLanguage = Session["LanguageSetting"].ToString();
                        Session["LanguageSetting"] = "Nepali";
                        //RESTORE THE VALUE OF SESSIONLANGUAGE
                        Session["LanguageSetting"] = tempLanguage;
                        objletter.docData = report.ConvertToString();
                        if (objletter.docData.ConvertToString() != "")
                        {
                            objEnrollServices.UpdatePrintStatus("Y", id);
                        }
                        strHTML1 = objletter.docData;

                        //  i++;
                    }
                    if (String.IsNullOrEmpty(strHTML))
                    {
                        strHTML = strHTML1;
                    }
                    else
                    {
                        strHTML = strHTML + "<div style='page-break-before: always'>" + strHTML1 + "</DIV>";
                    }
                }
                string htmlfilepath = Server.MapPath("/Files/html/Enrollment.html");
                pdffilepath = Server.MapPath("/Files/pdf/Enrollment.pdf");


                Utils.CreateFile(strHTML, htmlfilepath);
                PdfGenerator.ConvertToPdf(htmlfilepath, pdffilepath);

                System.Threading.Thread.Sleep(arrHHIDs.Length * 500);
                System.IO.FileInfo file = new System.IO.FileInfo(pdffilepath);
                if ((file.Exists))
                {
                    Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);

                    Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    System.IO.Path.GetFileName(file.Name)));

                    Response.TransmitFile(pdffilepath);
                    Response.Flush();
                    Response.End();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

        }

 

        [HttpPost]
        public ActionResult EnrollmentAdd(EnrollmentAdd model, string p, FormCollection fc)
        {
            DataTable dt = new DataTable();
            string enrollmentid = string.Empty;
            string targetbatchid = string.Empty;
            string targetingid = string.Empty;
            string Houseid = string.Empty;
            string structureNo = string.Empty;
            string HouseholdID = string.Empty;
            string HouseOwnerID = string.Empty;
            string mode = string.Empty;

            HouseOwner HO = new HouseOwner();
            proxy pr = new proxy();
            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            EnrollmentAdd objenrolladd = new EnrollmentAdd();
            RouteValueDictionary rvd = new RouteValueDictionary();

            try
            {
                model.FISCAL_YR = fc["FISCAL_YR"].ConvertToString();
                model.TARGET_BATCH_ID = fc["TARGET_BATCH_ID"].ConvertToString();
                model.TARGETING_ID = fc["TARGETING_ID"].ConvertToString();
                model.ENROLLMENT_ID = fc["ENROLLMENT_ID"].ConvertToString();
                model.MOU_ID = fc["MOU_ID"].ConvertToString();
                if (!string.IsNullOrEmpty(model.MOU_ID))
                {
                    model.ModeType = "U";
                }
                else
                {
                    model.ModeType = "I";
                }
                model.SURVEY_NO = fc["SURVEY_NO"].ConvertToString();
                model.ENROLLMENT_MOU_DAY = DateTime.Now.Day;
                model.ENROLLMENT_MOU_MONTH = DateTime.Now.Month;
                model.ENROLLMENT_MOU_YEAR = DateTime.Now.Year;
                model.ENROLLMENT_MOU_DT = DateTime.Now.ToString("MM/dd/yyyy");
                model.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
                model.HOUSEHOLD_ID = fc["HOUSEHOLD_ID"].ConvertToString();

                model.BENEFICIARY_FNAME_ENG = fc["BENEFICIARY_FNAME_ENG"].ConvertToString();
                model.BENEFICIARY_MNAME_ENG = fc["BENEFICIARY_MNAME_ENG"].ConvertToString();
                model.BENEFICIARY_LNAME_ENG = fc["BENEFICIARY_LNAME_ENG"].ConvertToString();
                model.BENEFICIARY_FULLNAME_ENG = fc["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                model.BENEFICIARY_FNAME_LOC = fc["BENEFICIARY_FNAME_LOC"].ConvertToString();
                model.BENEFICIARY_MNAME_LOC = fc["BENEFICIARY_MNAME_LOC"].ConvertToString();
                model.BENEFICIARY_LNAME_LOC = fc["BENEFICIARY_LNAME_LOC"].ConvertToString();
                model.BENEFICIARY_FULLNAME_LOC = fc["BENEFICIARY_FULLNAME_LOC"].ConvertToString();
                model.DISTRICT_CD = GetData.GetCodeFor(DataType.District, fc["DISTRICT_CD"].ConvertToString()).ToDecimal(); //fc["DISTRICT_CD"].ToDecimal();
                model.VDC_MUN_CD = fc["VDC_MUN_CD"].ConvertToString();
                model.WARD_NO = fc["WARD_NO"].ConvertToString();
                model.AREA_ENG = fc["AREA_ENG"].ConvertToString();
                model.AREA_LOC = fc["AREA_LOC"].ConvertToString();
                model.FATHER_FNAME_ENG = fc["FATHER_FNAME_ENG"].ConvertToString();
                model.FATHER_FNAME_LOC = fc["FATHER_FNAME_LOC"].ConvertToString();
                model.FATHER_MNAME_ENG = fc["FATHER_MNAME_ENG"].ConvertToString();
                model.FATHER_MNAME_LOC = fc["FATHER_MNAME_LOC"].ConvertToString();
                model.FATHER_LNAME_ENG = fc["FATHER_LNAME_ENG"].ConvertToString();
                model.FATHER_LNAME_LOC = fc["FATHER_LNAME_LOC"].ConvertToString();
                model.FATHER_FullNAME_ENG = model.FATHER_FNAME_ENG.ConvertToString() + (model.FATHER_MNAME_ENG.ConvertToString() == "" ? " " : (" " + model.FATHER_MNAME_ENG) + " ") + model.FATHER_LNAME_ENG.ConvertToString();
                model.FATHER_FullNAME_LOC = model.FATHER_FNAME_LOC.ConvertToString() + (model.FATHER_MNAME_LOC.ConvertToString() == "" ? " " : (" " + model.FATHER_MNAME_LOC) + " ") + model.FATHER_LNAME_LOC.ConvertToString();
                model.FATHER_RELATION_TYPE_CD = common.GetValueFromDataBase(fc["ddlRelationFather"].ConvertToString(), "MIS_RELATION_TYPE", "DEFINED_CD", "RELATION_TYPE_CD").ToDecimal(); //fc["ddlRelationFather"].ToDecimal();
                model.GFATHER_FNAME_ENG = fc["GFATHER_FNAME_ENG"].ConvertToString();
                model.GFATHER_FNAME_LOC = fc["GFATHER_FNAME_LOC"].ConvertToString();
                model.GFATHER_MNAME_ENG = fc["GFATHER_MNAME_ENG"].ConvertToString();
                model.GFATHER_MNAME_LOC = fc["GFATHER_MNAME_LOC"].ConvertToString();
                model.GFATHER_LNAME_ENG = fc["GFATHER_LNAME_ENG"].ConvertToString();
                model.GFATHER_LNAME_LOC = fc["GFATHER_LNAME_LOC"].ConvertToString();
                model.GFATHER_FullNAME_ENG = model.GFATHER_FNAME_ENG.ConvertToString() + (model.GFATHER_MNAME_ENG.ConvertToString() == "" ? " " : (" " + model.GFATHER_MNAME_ENG) + " ") + model.GFATHER_LNAME_ENG.ConvertToString();
                model.GFATHER_FullNAME_LOC = model.GFATHER_FNAME_LOC.ConvertToString() + (model.GFATHER_MNAME_LOC.ConvertToString() == "" ? " " : (" " + model.GFATHER_MNAME_LOC) + " ") + model.GFATHER_LNAME_LOC.ConvertToString();
                model.GFATHER_RELATION_TYPE_CD = common.GetValueFromDataBase(fc["ddlRelationGrandFather"].ConvertToString(), "MIS_RELATION_TYPE", "DEFINED_CD", "RELATION_TYPE_CD").ToDecimal(); //fc["ddlRelationGrandFather"].ToDecimal();
                // model.IDENTIFICATION_TYPE_CD = common.GetValueFromDataBase(fc["IDENTIFICATION_TYPE_CD"].ConvertToString(), "NHRS_IDENTIFICATION_TYPE", "DEFINED_CD", "IDENTIFICATION_TYPE_CD").ToDecimal(); //fc["IDENTIFICATION_TYPE_CD"].ToDecimal();
                model.IDENTIFICATION_NO = fc["IDENTIFICATION_NO"].ConvertToString();
                model.IDENTIFICATION_ISSUE_DIS_CD = GetData.GetCodeFor(DataType.AllDistrict, fc["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString()).ToDecimal(); //fc["IDENTIFICATION_ISSUE_DIS_CD"].ToDecimal();
                if (string.IsNullOrEmpty(fc["IDENTIFICATION_ISSUE_DT"]))
                {
                    model.IDENTIFICATION_ISSUE_DT = null;
                }
                else
                {
                    model.IDENTIFICATION_ISSUE_DT = fc["IDENTIFICATION_ISSUE_DT"].ConvertToString();
                }

                model.IDENTIFICATION_ISS_DT_LOC = fc["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                if (string.IsNullOrEmpty(fc["BIRTH_DT"]))
                {
                    model.BIRTH_DT = null;
                }
                else
                {
                    model.BIRTH_DT = fc["BIRTH_DT"].ConvertToString();
                }
                //if(string.IsNullOrEmpty(fc["AGREEMWNT_DT"]))
                //{
                //    model.AGREEMENT_DT = null;
                //}
                //else
                //{
                //    model.AGREEMENT_DT = fc["AGREEMWNT_DT"];
                //}
                //model.AGREEMENT_DT_LOC = fc["AGREEMENT_DT_LOC"].ConvertToString();
                //if (string.IsNullOrEmpty(fc["AGREEMENT_DT_LOC"]))
                //{
                //    model.AGREEMENT_DT_LOC = null;
                //}
                //else
                //{
                //    model.AGREEMENT_DT_LOC = fc["AGREEMENT_DT_LOC"];
                //}
                model.BIRTH_DT_LOC = fc["BIRTH_DT_LOC"].ConvertToString();
                model.FinlawFnameEng = fc["FinlawFnameEng"].ConvertToString();
                model.FinlawFnameLoc = fc["FinlawFnameLoc"].ConvertToString();
                model.FinlawMnameEng = fc["FinlawMnameEng"].ConvertToString();
                model.FinlawMnameLoc = fc["FinlawMnameLoc"].ConvertToString();
                model.FinlawLnameEng = fc["FinlawLnameEng"].ConvertToString();
                model.FinlawLnameLoc = fc["FinlawLnameLoc"].ConvertToString();
                model.FinLawFullNameEng = model.FinlawFnameEng.ConvertToString() + (model.FinlawMnameEng.ConvertToString() == "" ? " " : (" " + model.FinlawMnameEng) + " ") + model.FinlawLnameEng.ConvertToString();
                model.FinLawFullNameLoc = model.FinlawFnameLoc.ConvertToString() + (model.FinlawMnameLoc.ConvertToString() == "" ? " " : (" " + model.FinlawMnameLoc) + " ") + model.FinlawLnameLoc.ConvertToString();
                model.husbandfnameeng = fc["husbandfnameeng"].ConvertToString();
                model.husbandfnameloc = fc["husbandfnameloc"].ConvertToString();
                model.husbandMnameeng = fc["husbandMnameeng"].ConvertToString();
                model.husbandMnameloc = fc["husbandMnameloc"].ConvertToString();
                model.husbandLnameeng = fc["husbandLnameeng"].ConvertToString();
                model.husbandLnameloc = fc["husbandLnameloc"].ConvertToString();
                model.husbandFullnameEng = model.husbandfnameeng.ConvertToString() + (model.husbandMnameeng.ConvertToString() == "" ? " " : (" " + model.husbandMnameeng) + " ") + model.FinlawLnameEng.ConvertToString();
                model.husbandFullnameLoc = model.husbandfnameloc.ConvertToString() + (model.husbandMnameloc.ConvertToString() == "" ? " " : (" " + model.husbandMnameloc) + " ") + model.husbandLnameloc.ConvertToString();
                
                model.PROXY_FNAME_ENG = fc["PROXY_FNAME_ENG"].ConvertToString();
                model.PROXY_FNAME_LOC = fc["PROXY_FNAME_LOC"].ConvertToString();
                model.PROXY_MNAME_ENG = fc["PROXY_MNAME_ENG"].ConvertToString();
                model.PROXY_MNAME_LOC = fc["PROXY_MNAME_LOC"].ConvertToString();
                model.PROXY_LNAME_ENG = fc["PROXY_LNAME_ENG"].ConvertToString();
                model.PROXY_LNAME_LOC = fc["PROXY_LNAME_LOC"].ConvertToString();
                model.PROXY_FULLNAME_ENG = model.PROXY_FNAME_ENG.ConvertToString() + (model.PROXY_MNAME_ENG.ConvertToString() == "" ? " " : (" " + model.PROXY_MNAME_ENG) + " ") + model.PROXY_LNAME_ENG.ConvertToString();
                model.PROXY_FULLNAME_LOC = model.PROXY_FNAME_LOC.ConvertToString() + (model.PROXY_MNAME_LOC.ConvertToString() == "" ? " " : (" " + model.PROXY_MNAME_LOC) + " ") + model.PROXY_LNAME_LOC.ConvertToString();
                
                model.BANK_CD = common.GetValueFromDataBase(fc["BANK_CD"].ConvertToString(), "NHRS_BANK", "DEFINED_CD", "BANK_CD").ConvertToString(); //fc["BANK_CD"].ToDecimal();
                model.BANK_BRANCH_CD = common.GetValueFromDataBase(fc["BANK_BRANCH_CD"].ConvertToString(), "NHRS_BANK_BRANCH", "DEFINED_CD", "BANK_BRANCH_CD").ConvertToString(); //fc["BANK_BRANCH_CD"].ToDecimal();
                 
                model.REMARKS = fc["REMARKS"].ConvertToString();
                model.REMARKS_LOC = fc["REMARKS_LOC"].ConvertToString();
                model.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();
                

                objEnrollAddServices.SaveEnrollmentAdd(model);
 

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("EnrollmentList", "Enrollment");
        }


        [HttpPost]
        public ActionResult EnrollmentAdd1(EnrollmentAdd model, string p, FormCollection fc, HttpPostedFileBase BFile)
        {
            DataTable dt = new DataTable();
            string enrollmentid = string.Empty;
            string targetbatchid = string.Empty;
            string targetingid = string.Empty;
            string Houseid = string.Empty;
            string structureNo = string.Empty;
            string HouseholdID = string.Empty;
            string HouseOwnerID = string.Empty;
            string mode = string.Empty;
            bool result = false;
            string Message = "";
            HouseOwner HO = new HouseOwner();
            proxy pr = new proxy();
            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            EnrollmentAdd objenrolladd = new EnrollmentAdd();
            RouteValueDictionary rvd = new RouteValueDictionary();

            try
            {


                model.FISCAL_YR = fc["FISCAL_YR"].ConvertToString();
 
                model.MOU_ID = fc["MOU_ID"].ConvertToString();
                if (fc["MOU_ID"].ConvertToString() == "")
                {
                    model.ModeType = "I";
                }
                else
                {
                    model.ModeType = "U";
                }
                

                model.BENEFICIARY_FNAME_ENG = fc["BENEFICIARY_FNAME_ENG"].ConvertToString();

                
                model.DISTRICT_CD = fc["DISTRICT_CD"].ToDecimal();
                string VDCmun = fc["ddl_VDC"].ConvertToString();
                model.VDC_MUN_CD = common.GetCodeFromDataBase(VDCmun, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();
                var PVdc = fc["ddl_VDCMunProxy"].ConvertToString();
                model.PROXY_VDC_MUN_CD = common.GetCodeFromDataBase(PVdc, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();
                //string BuildingVDC = fc["ddl_VDCMunBuilding"].ConvertToString();

                string BuildingVDC = fc["ddl_VDCMunBuilding"].ConvertToString();
                model.BUILDING_VDC_MUN_CD = common.GetCodeFromDataBase(BuildingVDC, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();
                //string Ward = fc["ddl_BenfWard"].ConvertToString();
                string BankCd = fc["ddl_bank_name"].ConvertToString();
                model.BANK_CD = common.GetCodeFromDataBase(BankCd, "NHRS_BANK", "BANK_CD").ConvertToString();
                model.IS_MANJURINAMA_AVAILABLE = fc["ddlManjurinama"].ToString();
                model.WARD_NO = fc["WARD_NO"].ConvertToString();
 
                int _min = 0000;
                int _max = 9999;
                Random _rdm = new Random();
                int rno =  _rdm.Next(_min, _max);

                if (BFile != null && BFile.ContentLength > 0)
                {
                    BFile.SaveAs(Server.MapPath("~/Files/images/EnrolledMember/") + Path.GetFileName(rno +""+BFile.FileName));
                    model.Beneficiary_Photo = rno + "" + BFile.FileName;
                }

              
                else
                {
                    model.Beneficiary_Photo = model.Beneficiary_Photo;
                }

                 
                Random _rdmd = new Random();
                int rnod = _rdmd.Next(_min, _max);

                if (BFile != null && BFile.ContentLength > 0)
                {
                    int cnt = 2;
                    for (int i = 1; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];

                        if (file.ContentLength == 0)
                            continue;

                        file.SaveAs(Server.MapPath("~/Files/images/EnrolledMember/") + Path.GetFileName(rnod + "" + file.FileName));
                        if (!string.IsNullOrEmpty(model.DocType))
                        {
                            model.DocType = model.DocType + "," + fc["ddlDocumentListType" + cnt];
                            cnt++;
                        }
                        else
                        {
                            model.DocType = fc["ddlDocumentListType"];
                        }
                        if (!string.IsNullOrEmpty(file.FileName))
                        {
                            model.Docs = model.Docs + "," + rnod + "" + file.FileName;
                        }
                        else
                        {
                            model.Docs = model.DOCUMENTS;
                        }
                        model.CTZN_PIC_NAME = model.Docs;

                    }
                }
                else
                {

                    int cnt = 2;
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                  
                        if (file.ContentLength == 0)                      
                            continue;

                        file.SaveAs(Server.MapPath("~/Files/images/EnrolledMember/") + Path.GetFileName(rnod + "" + file.FileName));
                        if (!string.IsNullOrEmpty(model.DocType))
                        {
                            model.DocType = model.DocType + "," + fc["ddlDocumentListType" + cnt];
                            cnt++;
                        }
                        else
                        {
                            model.DocType = fc["ddlDocumentListType"];
                                                    }
                        if (!string.IsNullOrEmpty(file.FileName))
                        {
                            model.Docs = model.Docs + "," + rnod + "" + file.FileName;
                        }
                        else
                        {
                            model.Docs = rnod + "" + file.FileName;
                        }
                        model.CTZN_PIC_NAME = model.Docs;

                    }
                }


                if (model.IS_BUILDING_DEG_FROM_CAT == "Y")
                {
                    model.IS_BUILDING_DEG_FROM_CAT = "Y";
                    model.BUILDING_DEG_CAT_NO = model.BUILDING_DEG_CAT_NO;
                }
                else
                {
                    model.IS_BUILDING_DEG_FROM_CAT = "N";
                    model.BUILDING_DEG_CAT_NO = model.BUILDING_DEG_CAT_NO;
                }
                if (model.IS_BUILDING_OWN_DESIGN == "Y")
                {
                    model.IS_BUILDING_OWN_DESIGN = "Y";
                    model.BUILDING_PILER_TYPE = model.BUILDING_PILER_TYPE;
                    model.BUILDING_FLOOR_ROOF_TYPE = model.BUILDING_FLOOR_ROOF_TYPE;
                }
                else
                {
                    model.IS_BUILDING_OWN_DESIGN = "N";
                    model.BUILDING_PILER_TYPE = model.BUILDING_PILER_TYPE;
                    model.BUILDING_FLOOR_ROOF_TYPE = model.BUILDING_FLOOR_ROOF_TYPE;
                }

              
                result = objEnrollAddServices.SaveEnrollmentAdd1(model);
               
                //string path = Server.MapPath("~/Files/images/EnrolledMember/");
                //string Fromfile = path + Session["ImageName"];
                //string Tofile = path + fc["NRA_DEFINED_CD"] + "_" + Session["ImageName"];

                //FileInfo fi = new FileInfo(Fromfile);
                //if (fi.Exists)
                //{
                //    fi.MoveTo(Tofile);
                //}


            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
                Message = oe.Message.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
                Message = ex.Message.ToString();
            }
            if (result == true)
            {
                Session["EnrollmentAdded"] = "True";
            }
            else
            {
                Session["EnrollmentAdded"] = "False";
                Session["AddErrorMessage"] = Message;
            }
            return RedirectToAction("EnrollmentList", "Enrollment");
        }


        public ActionResult GetBeneficiaryByHH(string sortBy, string p)
        {
            enrollmentclass objenroll = new enrollmentclass();
            if (sortBy == "N")
            {
                objenroll.FIRST_NAME_ENG = null;
                objenroll.MIDDLE_NAME_ENG = null;
                objenroll.LAST_NAME_ENG = null;
            }
            if (sortBy == "Y")
            {
                objenroll.FIRST_NAME_ENG = null;
                objenroll.MIDDLE_NAME_ENG = null;
                objenroll.LAST_NAME_ENG = null;

            }







            //return PartialView("~/Views/Enrollment/_beneficiaryselection.cshtml");
            return RedirectToAction("EnrollmentView", objenroll);



        }

        public ActionResult GetBeneficiarySelection(EnrollmentSearch model, string p)
        {

            DataTable dt = new DataTable();
            string memberid = string.Empty;
            string HouseID = string.Empty;
            string buildingstrno = string.Empty;
            string HOuseholdid = string.Empty;
            string HouseOwnerID = string.Empty;
            string mode = string.Empty;
            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            RouteValueDictionary rvd = new RouteValueDictionary();
            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["memberid"] != null)
                    {
                        memberid = (rvd["memberid"]).ConvertToString();
                    }
                    if (rvd["HouseID"] != null)
                    {
                        HouseID = (rvd["HouseID"]).ConvertToString();
                    }
                    if (rvd["buildingstrno"] != null)
                    {
                        buildingstrno = (rvd["buildingstrno"]).ConvertToString();
                    }
                    if (rvd["HOuseholdid"] != null)
                    {
                        HOuseholdid = (rvd["HOuseholdid"]).ConvertToString();
                    }
                    if (rvd["Houseownerid"] != null)
                    {
                        HouseOwnerID = (rvd["Houseownerid"]).ConvertToString();
                    }

                }
            }

            dt = objEnrollServices.GetBeneficiarySelection(buildingstrno, HouseOwnerID);

            ViewData["result"] = dt;
            return PartialView("~/Views/Enrollment/_beneficiaryselection.cshtml");
        }

        //public ActionResult GetProxyBeneficiarySelection(EnrollmentSearch model, string p)
        //{

        //    DataTable dt = new DataTable();
        //    string memberid = string.Empty;
        //    string HouseID = string.Empty;
        //    string buildingstrno = string.Empty;
        //    string HOuseholdid = string.Empty;
        //    string HouseOwnerID = string.Empty;
        //    string mode = string.Empty;
        //    EnrollmentService objEnrollServices = new EnrollmentService();
        //    enrollmentclass objenroll = new enrollmentclass();
        //    RouteValueDictionary rvd = new RouteValueDictionary();
        //    if (p != null)
        //    {
        //        rvd = QueryStringEncrypt.DecryptString(p);
        //        if (rvd != null)
        //        {
        //            if (rvd["memberid"] != null)
        //            {
        //                memberid = (rvd["memberid"]).ConvertToString();
        //            }
        //            if (rvd["HouseID"] != null)
        //            {
        //                HouseID = (rvd["HouseID"]).ConvertToString();
        //            }
        //            if (rvd["buildingstrno"] != null)
        //            {
        //                buildingstrno = (rvd["buildingstrno"]).ConvertToString();
        //            }
        //            if (rvd["HOuseholdid"] != null)
        //            {
        //                HOuseholdid = (rvd["HOuseholdid"]).ConvertToString();
        //            }
        //            if (rvd["Houseownerid"] != null)
        //            {
        //                HouseOwnerID = (rvd["Houseownerid"]).ConvertToString();
        //            }

        //        }
        //    }

        //    //dt = objEnrollServices.GetProxyBeneficiarySelection(buildingstrno, HouseOwnerID);

        //    ViewData["result"] = dt;
        //    return PartialView("~/Views/Enrollment/_beneficiaryselection.cshtml");
        //}


        public ActionResult EnrollmentSave(enrollmentclass model, string p, FormCollection fc)
        {

            DataTable dt = new DataTable();

            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            EnrollmentAdd objenrolladd = new EnrollmentAdd();
            RouteValueDictionary rvd = new RouteValueDictionary();

            objenroll.ENROLLMENT_ID = fc["ENROLLMENT_ID"].ConvertToString();
            objenroll.TARGETING_ID = fc["TARGETING_ID"].ConvertToString();
            objenroll.TARGETING_BATCH_ID = fc["TARGETING_BATCH_ID"].ConvertToString();
            objenroll.HOUSE_OWNER_ID = fc["HOUSE_OWNER_ID"].ConvertToString();
            objenroll.BUILDING_STRUCTURE_NO = fc["BUILDING_STRUCTURE_NO"].ConvertToString();
            objenroll.DISTRICT_CD = fc["DISTRICT_CD"].ConvertToString();
            objenroll.VDC_MUN_CD = GetData.GetCodeFor(DataType.VdcMun, fc["VDC_MUNICIPALITY_CD"].ConvertToString());
            objenroll.FIRST_NAME_ENG = fc["FIRST_NAME_ENG"].ConvertToString();
            objenroll.MIDDLE_NAME_ENG = fc["MIDDLE_NAME_ENG"].ConvertToString();
            objenroll.LAST_NAME_ENG = fc["LAST_NAME_ENG"].ConvertToString();
            objenroll.BENEFICIARY_FNAME_ENG = fc["BENEFICIARY_FNAME_ENG"].ConvertToString();
            objenroll.BENEFICIARY_LNAME_ENG = fc["BENEFICIARY_LNAME_ENG"].ConvertToString();
            objenroll.BENEFICIARY_MNAME_ENG = fc["BENEFICIARY_MNAME_ENG"].ConvertToString();
            objenroll.BENEFICIARY_RELATION_TYPE_CD = fc["BENEFICIARY_RELATION_TYPE_CD"].ConvertToString();
            //objenroll.PROXY_FNAME_ENG = fc["PROXY_FNAME_ENG"].ConvertToString();
            //objenroll.PROXY_LNAME_ENG = fc["PROXY_LNAME_ENG"].ConvertToString();
            //objenroll.PROXY_MNAME_ENG = fc["PROXY_MNAME_ENG"].ConvertToString();
            objenroll.BENEFICIARY_FNAME_ENG = fc["BENEFICIARY_FNAME_ENG"].ConvertToString();
            objenroll.PROXY_RELATION_TYPE_CD = fc["PROXY_RELATION_TYPE_CD"].ConvertToString();
            objenroll.BANK_CD = fc["BANK_CD"].ConvertToString();
            objenroll.BANK_BRANCH_CD = fc["BANK_BRANCH_CD"].ConvertToString();
            //objenroll.BANK_ACC_NO = fc["BANK_ACC_NO"].ConvertToString();
            //objenroll.BANK_ACC_TYPE_CD = fc["BANK_ACC_TYPE_CD"].ConvertToString();
            objenroll.IDENTIFICATION_TYPE_CD = fc["IDENTIFICATION_TYPE_CD"].ConvertToString();
            objenroll.IDENTIFICATION_NO = fc["IDENTIFICATION_NO"].ConvertToString();
            objenroll.IDENTIFICATION_DOCUMENT = fc["IDENTIFICATION_DOCUMENT"].ConvertToString();
            objenroll.IDENTIFICATION_ISSUE_DIS_CD = fc["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString();


            QueryResult success = new QueryResult();
            if (fc["btn_Submit"].ToString() == "Save")
            {
                objenroll.MODE = "I";
                if (Session["ImageName"] != null)
                {
                    objenroll.MEMBER_PHOTO_PATH = "../../Files/images/EnrolledMember/" + "_" + Session["ImageName"];
                }

                string path = Server.MapPath("~/Files/images/EnrolledMember/");
                string Fromfile = path + Session["ImageName"];
                string Tofile = path + "_" + Session["ImageName"];

                FileInfo fi = new FileInfo(Fromfile);
                if (fi.Exists)
                {
                    fi.MoveTo(Tofile);
                }
                objEnrollServices.UpdateEnrollmentDetails(objenroll);
                //success = objEnrollServices.GetEnrollmentSaved(objenroll);
                if (success.IsSuccess)
                {

                    TempData["Message"] = "You have Successfully Saved ";
                }
            }
            if (fc["btn_Submit"].ToString() == "Update")
            {
                objenroll.MODE = "U";
                if (Session["ImageName"] != null)
                {
                    objenroll.MEMBER_PHOTO_PATH = "../../Files/images/EnrolledMember/" + "_" + Session["ImageName"];
                }

                string path = Server.MapPath("~/Files/images/EnrolledMember/");
                string Fromfile = path + Session["ImageName"];
                string Tofile = path + "_" + Session["ImageName"];

                FileInfo fi = new FileInfo(Fromfile);
                if (fi.Exists)
                {

                    fi.MoveTo(Tofile);
                }
                objEnrollServices.UpdateEnrollmentDetails(objenroll);
                if (success.IsSuccess)
                {

                    TempData["Message"] = "You have Successfully Updated ";
                }
            }


            if (success.IsSuccess)
            {

                TempData["Message"] = "You have Successfully Saved ";
            }
            GetDropDown();
            TempData["ResearchEnrollment"] = "Y";
            return RedirectToAction("EnrollmentList");
            //return RedirectToAction("EnrollmentView", objenroll);



        }
        public ActionResult EnrollmentAdd1(EnrollmentAdd model, string p)
        {
            DataTable dt = new DataTable();
            DataTable dttt = new DataTable();
            string enrollmentid = string.Empty;
            string targetbatchid = string.Empty;
            string targetingid = string.Empty;
            string Houseid = string.Empty;
            string structureNo = string.Empty;
            string HouseholdID = string.Empty;
            string HouseOwnerID = string.Empty;
            string PA = string.Empty;
            string mode = string.Empty;
            HouseOwner HO = new HouseOwner();
            proxy pr = new proxy();
            EnrollmentService objEnrollServices = new EnrollmentService();
            enrollmentclass objenroll = new enrollmentclass();
            EnrollmentAddService objEnrollAddServices = new EnrollmentAddService();
            EnrollmentAdd objenrolladd = new EnrollmentAdd();
            RouteValueDictionary rvd = new RouteValueDictionary();
            if (p != null)
            {
                rvd = QueryStringEncrypt.DecryptString(p);
                if (rvd != null)
                {
                    if (rvd["Houseownerid"] != null)
                    {
                        HouseOwnerID = (rvd["Houseownerid"]).ConvertToString();
                        Session["HouseOwnerID"] = HouseOwnerID;
                        PA = (rvd["Pa"]).ConvertToString();
                    }

                }
            }
            objenrolladd.ModeType = "I";
            dt = objEnrollAddServices.GetEnrollmentAdd(Session["HouseOwnerID"].ConvertToString(), PA);
            dttt = objEnrollAddServices.GetEnrollmentData(HouseOwnerID,PA);

            if (dttt != null && dttt.Rows.Count > 0)
            {
                foreach (DataRow dr in dttt.Rows)
                {
                    objenrolladd.BENEFICIARY_FNAME_LOC = dr["BENEFICIARY_FNAME_LOC"].ConvertToString();
                    objenrolladd.BENEFICIARY_MNAME_LOC = dr["BENEFICIARY_MNAME_LOC"].ConvertToString();
                    objenrolladd.BENEFICIARY_LNAME_LOC = dr["BENEFICIARY_LNAME_LOC"].ConvertToString();
                    objenrolladd.DISTRICT_CD = dr["DISTRICT_CD"].ToDecimal();
                    //string definedvdccode = common.GetCodeFromDataBase(objenrolladd.VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");


                    //objenrolladd.VDC_MUN_CD=dr["VDC_MUN_CD"].ToDecimal();
                    objenrolladd.WARD_NO = dr["WARD_NO"].ConvertToString();
                    objenrolladd.IDENTIFICATION_NO = dr["IDENTIFICATION_NO"].ConvertToString();
                    objenrolladd.IDENTIFICATION_ISSUE_DIS_CD = dr["IDENTIFICATION_ISSUE_DIS_CD"].ToDecimal();
                    objenrolladd.IDENTIFICATION_ISSUE_DT = dr["IDENTIFICATION_ISSUE_DT"].ConvertToString("yyyy-MM-dd");

                    objenrolladd.IDENTIFICATION_ISS_DT_LOC = dr["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                    objenrolladd.BIRTH_DT = dr["BENEFICIARY_DOB_ENG"].ConvertToString("yyyy-MM-dd");
                    objenrolladd.BIRTH_DT_LOC = dr["BENEFICIARY_DOB_LOC"].ConvertToString();
                    objenrolladd.PHONE_NO = dr["BENEFICIARY_PHONE"].ConvertToString();
                    objenrolladd.migrationno = dr["BENEFICIARY_MIGRATION_NO"].ToDecimal();
                    objenrolladd.migrationdate = dr["BENEFICIARY_MIGRATTION_DT"].ConvertToString("yyyy-MM-dd");
                    objenrolladd.migrationdateloc = dr["BENEFICIARY_MIGRATION_DT_LOC"].ConvertToString();
                    objenrolladd.IS_MANJURINAMA_AVAILABLE = dr["ISMANJURINAMA_AVAIL"].ConvertToString();
                    objenrolladd.PROXY_FNAME_ENG = dr["MANJURINAMA_FNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_MNAME_ENG = dr["MANJURINAMA_MNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_LNAME_ENG = dr["MANJURINAMA_LNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_FNAME_LOC = dr["MANJURINAMA_FNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_MNAME_LOC = dr["MANJURINAMA_MNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_LNAME_LOC = dr["MANJURINAMA_LNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_DISTRICT_CD = dr["MANJURINAMA_DISTRICT_CD"].ToDecimal();
                    //string proxyvdc = dr["MANJURINAMA_DISTRICT_CD"].ConvertToString();
                    //objenrolladd.PROXY_VDC_MUN_CD = common.GetDefinedCodeFromDataBase(proxyvdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();
                    //string vdc = common.GetDefinedCodeFromDataBase(proxyvdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();
                    // objenrolladd.PROXY_VDC_MUN_CD = dr["DEFINED_CD"].ToDecimal();
                    objenrolladd.PROXY_VDC_MUN_CD = dr["MANJURINAMA_VDC_CD"].ConvertToString();
                    objenrolladd.PROXY_WARD_NO = dr["MANJURINAMA_WARD_NO"].ConvertToString();
                    objenrolladd.PROXY_AREA_ENG = dr["MANJURINAMA_AREA_ENG"].ConvertToString();

                    objenrolladd.PROXY_IDENTIFICATION_NO = dr["MANJURINAMA_IDENTITY_NO"].ConvertToString();
                    objenrolladd.PROXY_IDENTIFICATION_ISSUE_DIS_CD = dr["MANJURINAMA_IDENT_ISSUE_DIS_CD"].ToDecimal();
                    objenrolladd.PROXY_IDENTIFICATION_ISSUE_DT = dr["MANJURINAMA_IDENT_ISSUE_DT"].ConvertToString("yyyy-MM-dd");
                    objenrolladd.PROXY_IDENTIFICATION_ISS_DT_LOC = dr["MANJURINAMA_IDENT_ISS_DT_LOC"].ConvertToString();
                    objenrolladd.PROXY_BIRTH_DT = dr["MANJURINAMA_BIRTH_DT"].ConvertToString("yyyy-MM-dd");
                    objenrolladd.PROXY_BIRTH_DT_LOC = dr["MANJURINAMA_BIRTH_DT_LOC"].ConvertToString();

                    objenrolladd.PROXY_GFATHERS_FNAME_ENG = dr["MANJURINAMA_GFATHER_FNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_GFATHERS_MNAME_ENG = dr["MANJURINAMA_GFATHER_MNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_GFATHERS_LNAME_ENG = dr["MANJURINAMA_GFATHER_LNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_GFATHERS_FNAME_LOC = dr["MANJURINAMA_GFATHER_FNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_GFATHERS_MNAME_LOC = dr["MANJURINAMA_GFATHER_MNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_GFATHERS_LNAME_LOC = dr["MANJURINAMA_GFATHER_LNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_FATHERS_FNAME_ENG = dr["MANJURINAMA_FATHER_FNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_FATHERS_MNAME_ENG = dr["MANJURINAMA_FATHER_MNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_FATHERS_LNAME_ENG = dr["MANJURINAMA_FATHER_LNAME_ENG"].ConvertToString();
                    objenrolladd.PROXY_FATHERS_FNAME_LOC = dr["MANJURINAMA_FATHER_FNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_FATHERS_MNAME_LOC = dr["MANJURINAMA_FATHER_MNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_FATHERS_LNAME_LOC = dr["MANJURINAMA_FATHER_LNAME_LOC"].ConvertToString();
                    objenrolladd.PROXY_RELATION_TYPE_CD = dr["MANJURINAMA_RELATION_TYPE_CD"].ToDecimal();
                    objenrolladd.PROXY_PHONE = dr["MANJURINAMA_PHONE"].ConvertToString();

                    objenrolladd.BANK_ACC_NO = dr["BANK_ACC_NO"].ConvertToString();
                    objenrolladd.BANK_CD = dr["BANK_CD"].ConvertToString();
                    objenrolladd.BANK_BRANCH_CD = dr["BANK_BRANCH_CD"].ConvertToString();
                    objenrolladd.ACC_HOLDER_FNAME_ENG = dr["ACC_HOLD_FNAME_ENG"].ConvertToString();
                    objenrolladd.ACC_HOLDER_MNAME_ENG = dr["ACC_HOLD_MNAME_ENG"].ConvertToString();
                    objenrolladd.ACC_HOLDER_LNAME_ENG = dr["ACC_HOLD_LNAME_ENG"].ConvertToString();

                    objenrolladd.BUILDING_KITTA_NUMBER = dr["BUILDING_KITTA_NUMBER"].ConvertToString();
                    objenrolladd.BUILDING_AREA_ENG = dr["BUILDING_AREA_ENG"].ConvertToString();
                    objenrolladd.BUILDING_AREA = dr["BUILDING_AREA"].ConvertToString();
                    objenrolladd.BUILDING_DISTRICT_CD = dr["BUILDING_DISTRICT_CD"].ToDecimal();
                    string buildingvdc = dr["BUILDING_VDC_MUN_CD"].ConvertToString();
                    objenrolladd.BUILDING_VDC_MUN_CD = common.GetDefinedCodeFromDataBase(buildingvdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();


                    //objenrolladd.BUILDING_VDC_MUN_CD = dr["BUILDING_VDC_MUN_CD"].ConvertToString();
                    objenrolladd.BUILDING_WARD_NO = dr["BUILDING_WARD_NO"].ConvertToString();


                    objenrolladd.NOMINEE_FNAME_ENG = dr["NOMINEE_FNAME_ENG"].ConvertToString();
                    objenrolladd.NOMINEE_MNAME_ENG = dr["NOMINEE_MNAME_ENG"].ConvertToString();
                    objenrolladd.NOMINEE_LNAME_ENG = dr["NOMINEE_LNAME_ENG"].ConvertToString();
                    objenrolladd.NOMINEE_RELATION_TYPE_CD = dr["NOMINEE_RELATION_TYPE_CD"].ToDecimal();

                    objenrolladd.Sakshi_FName_ENG = dr["SHAKSHI_FNAME_ENG"].ConvertToString();
                    objenrolladd.Sakshi_MName_ENG = dr["SHAKSHI_MNAME_ENG"].ConvertToString();
                    objenrolladd.Sakshi_LName_ENG = dr["SHAKSHI_LNAME_ENG"].ConvertToString();
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                objenrolladd.FISCAL_YR = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                //TempData["Fiscal_YR"] = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                //Session["Fiscal_YR"] = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                objenrolladd.DISTRICT_CD = dt.Rows[0]["DISTRICT_CD"].ToDecimal();
                objenrolladd.DISTRICT_NAME = Utils.ToggleLanguage(dt.Rows[0]["DISTRICT_ENG"].ConvertToString(), dt.Rows[0]["DISTRICT_LOC"].ConvertToString());

                string benefvdc = dt.Rows[0]["VDC_MUN_CD"].ConvertToString();
                objenrolladd.VDC_MUN_CD = common.GetDefinedCodeFromDataBase(benefvdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();

                //objenrolladd.VDC_MUN_CD = dt.Rows[0]["VDC_MUN_CD"].ToDecimal();
                objenrolladd.VDC_MUN_NAME = Utils.ToggleLanguage(dt.Rows[0]["VDC_MUN_ENG"].ConvertToString(), dt.Rows[0]["VDC_MUN_LOC"].ConvertToString());
                objenrolladd.WARD_NO = dt.Rows[0]["WARD_NO"].ConvertToString();
                objenrolladd.AREA = Utils.ToggleLanguage(dt.Rows[0]["AREA_ENG"].ConvertToString(), dt.Rows[0]["AREA_LOC"].ConvertToString());
                objenrolladd.AREA_ENG = dt.Rows[0]["AREA_ENG"].ConvertToString();
                objenrolladd.AREA_LOC = dt.Rows[0]["AREA_LOC"].ConvertToString();
                objenrolladd.ENUMERATOR_ID = dt.Rows[0]["ENUMERATOR_ID"].ConvertToString();
                objenrolladd.WARD_NO = dt.Rows[0]["WARD_NO"].ConvertToString();
                // objenrolladd.BUILDING_STRUCTURE_NO = dt.Rows[0]["BUILDING_STRUCTURE_NO"].ConvertToString();
                objenrolladd.BENEFICIARY_FULLNAME_ENG = dt.Rows[0]["HOUSE_OWNER_NAME_ENG"].ConvertToString();
                objenrolladd.MOU_ID = dt.Rows[0]["MOU_ID"].ConvertToString();
                string[] name = objenrolladd.BENEFICIARY_FULLNAME_ENG.ConvertToString().Split(' ');


                #region changes




                objenrolladd.OFFICE_VDC_MUN_CD = dt.Rows[0]["OVDCCD"].ConvertToString();
                objenrolladd.OFFICE_VDC_MUN_NAME = common.GetDefinedCodeFromDataBase(objenrolladd.OFFICE_VDC_MUN_CD.ConvertToString(), "OVDCENG", "OVDCLOC").ConvertToString();
                objenrolladd.OFFICE_DISTRICT_CD = dt.Rows[0]["ODISCD"].ToString();
                objenrolladd.OFFICE_DISTRICT_NAME = Utils.ToggleLanguage(dt.Rows[0]["ODISENG"].ConvertToString(), dt.Rows[0]["ODISLOC"].ConvertToString());

                objenrolladd.OFFICE_WARD = dt.Rows[0]["OWARD"].ConvertToString();

                objenrolladd.OFFICE_ENG = dt.Rows[0]["OFFICENAME"].ConvertToString();
                objenrolladd.EMPLOYEE_ENG = dt.Rows[0]["OFFICERNAME"].ConvertToString();
                objenrolladd.POSITION_ENG = dt.Rows[0]["POSITION"].ConvertToString();

                objenrolladd.OFFICE_CD = dt.Rows[0]["OFFICE_CD"].ConvertToString();
                objenrolladd.POSITION_CD = dt.Rows[0]["POSITION_CD"].ToDecimal();
                objenrolladd.EMPLOYEE_CD = dt.Rows[0]["EMPLOYEE_CD"].ConvertToString();






                //objenrolladd.BENEFICIARY_FNAME_LOC = dt.Rows[0]["BENEFICIARY_FNAME_LOC"].ConvertToString();
                //objenrolladd.BENEFICIARY_MNAME_LOC = dt.Rows[0]["BENEFICIARY_MNAME_LOC"].ConvertToString();
                //objenrolladd.BENEFICIARY_LNAME_LOC = dt.Rows[0]["BENEFICIARY_LNAME_LOC"].ConvertToString();
                //objenrolladd.DISTRICT_CD = dt.Rows[0]["DISTRICT_CD"].ToDecimal();
                //string definedvdccode = common.GetCodeFromDataBase(objenrolladd.VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");


                //objenrolladd.VDC_MUN_CD=dr["VDC_MUN_CD"].ToDecimal();
                //objenrolladd.WARD_NO = dt.Rows[0]["WARD_NO"].ConvertToString();

                if (Convert.ToString(dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"]) != "")
                    objenrolladd.IDENTIFICATION_ISSUE_DIS_CD = dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"].ToDecimal();
                objenrolladd.IDENTIFICATION_ISSUE_DT = dt.Rows[0]["IDENTIFICATION_ISSUE_DT"].ConvertToString("yyyy-MM-dd");

                objenrolladd.IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                objenrolladd.BIRTH_DT = dt.Rows[0]["BENEFICIARY_DOB_ENG"].ConvertToString("yyyy-MM-dd");
                objenrolladd.BIRTH_DT_LOC = dt.Rows[0]["BENEFICIARY_DOB_LOC"].ConvertToString();
                objenrolladd.PHONE_NO = dt.Rows[0]["BENEFICIARY_PHONE"].ConvertToString();
                if (Convert.ToString(dt.Rows[0]["BENEFICIARY_MIGRATION_NO"]) != "")
                    objenrolladd.migrationno = dt.Rows[0]["BENEFICIARY_MIGRATION_NO"].ToDecimal();
                objenrolladd.migrationdate = dt.Rows[0]["BENEFICIARY_MIGRATTION_DT"].ConvertToString("yyyy-MM-dd");
                objenrolladd.migrationdateloc = dt.Rows[0]["BENEFICIARY_MIGRATION_DT_LOC"].ConvertToString();
                //objenrolladd.IS_MANJURINAMA_AVAILABLE = dt.Rows[0]["ISMANJURINAMA_AVAIL"].ConvertToString();


                objenrolladd.IS_BUILDING_DEG_FROM_CAT = dt.Rows[0]["IS_BUILDING_DEG_FROM_CAT"].ConvertToString();
                if (Convert.ToString(dt.Rows[0]["BUILDING_DEG_CAT_NO"]) != "")
                    objenrolladd.BUILDING_DEG_CAT_NO = Convert.ToInt32(dt.Rows[0]["BUILDING_DEG_CAT_NO"]);

                objenrolladd.IS_BUILDING_OWN_DESIGN = dt.Rows[0]["IS_BUILDING_OWN_DESIGN"].ConvertToString();
                objenrolladd.BUILDING_PILER_TYPE = dt.Rows[0]["BUILDING_PILER_TYPE"].ConvertToString();
                objenrolladd.BUILDING_FLOOR_ROOF_TYPE = dt.Rows[0]["BUILDING_FLOOR_ROOF_TYPE"].ConvertToString();
                objenrolladd.BUILDING_OTHER_DEG = dt.Rows[0]["BUILDING_OTHER_DEG"].ConvertToString();

                objenrolladd.BUILDING_OTHER_DEG = dt.Rows[0]["BUILDING_OTHER_DEG"].ConvertToString();
                objenrolladd.MAP_APROVED_NO = dt.Rows[0]["MAP_APROVED_NO"].ConvertToString();

                if (dt.Rows[0]["MAP_APROVED_NO"].ConvertToString() != "")
                {
                    objenrolladd.designedNo = "Y";
                }

                else
                {
                    objenrolladd.designedNo = "N";
                }

                objenrolladd.Beneficiary_Photo = dt.Rows[0]["BENEFICIARY_PHOTO"].ConvertToString();
                objenrolladd.DOCUMENTS = dt.Rows[0]["CTZN_PIC_NAME"].ConvertToString();


                string[] namesArray = objenrolladd.DOCUMENTS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries); 
                //string[] namesArray = objenrolladd.DOCUMENTS.Split(',');
                objenrolladd.DOCUMENT = new List<string>(namesArray.Length);
                //List<string> namesList = new List<string>(namesArray.Length);
                //namesList.AddRange(namesArray);
                objenrolladd.DOCUMENT.AddRange(namesArray);
                //namesList.Reverse();
                objenrolladd.DOCUMENT.Reverse();



                objenrolladd.PROXY_FNAME_ENG = dt.Rows[0]["MANJURINAMA_FNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_MNAME_ENG = dt.Rows[0]["MANJURINAMA_MNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_LNAME_ENG = dt.Rows[0]["MANJURINAMA_LNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_FNAME_LOC = dt.Rows[0]["MANJURINAMA_FNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_MNAME_LOC = dt.Rows[0]["MANJURINAMA_MNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_LNAME_LOC = dt.Rows[0]["MANJURINAMA_LNAME_LOC"].ConvertToString();
                if (Convert.ToString(dt.Rows[0]["MANJURINAMA_DISTRICT_CD"]) != "")
                    objenrolladd.PROXY_DISTRICT_CD = dt.Rows[0]["MANJURINAMA_DISTRICT_CD"].ToDecimal();
                objenrolladd.IDENTIFICATION_NO = dt.Rows[0]["IDENTIFICATION_NO"].ConvertToString();
                objenrolladd.BUILDING_AREA_ENG = dt.Rows[0]["BUILDING_AREA_ENG"].ConvertToString();
                string proxyvdc = dt.Rows[0]["MANJURINAMA_DISTRICT_CD"].ConvertToString();

                //objenrolladd.PROXY_VDC_MUN_CD = dt.Rows[0]["DEFINED_CD"].ConvertToString();
                objenrolladd.PROXY_VDC_MUN_CD = dt.Rows[0]["MANJURINAMA_VDC_CD"].ConvertToString();
                string vdc = dt.Rows[0]["MANJURINAMA_VDC_CD"].ConvertToString();

                objenrolladd.PROXY_VDC_MUN_CD = common.GetDefinedCodeFromDataBase(vdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();
                //string vdc = common.GetDefinedCodeFromDataBase(proxyvdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();


                objenrolladd.PROXY_WARD_NO = dt.Rows[0]["MANJURINAMA_WARD_NO"].ConvertToString();
                objenrolladd.PROXY_AREA_ENG = dt.Rows[0]["MANJURINAMA_AREA_ENG"].ConvertToString();

                objenrolladd.PROXY_IDENTIFICATION_NO = dt.Rows[0]["MANJURINAMA_IDENTITY_NO"].ConvertToString();
                objenrolladd.PROXY_IDENTIFICATION_ISSUE_DIS_CD = dt.Rows[0]["MANJURINAMA_IDENT_ISSUE_DIS_CD"].ToDecimal();
                objenrolladd.PROXY_IDENTIFICATION_ISSUE_DT = dt.Rows[0]["MANJURINAMA_IDENT_ISSUE_DT"].ConvertToString("yyyy-MM-dd");
                objenrolladd.PROXY_IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["MANJURINAMA_IDENT_ISS_DT_LOC"].ConvertToString();
                objenrolladd.PROXY_BIRTH_DT = dt.Rows[0]["MANJURINAMA_BIRTH_DT"].ConvertToString("yyyy-MM-dd");
                objenrolladd.PROXY_BIRTH_DT_LOC = dt.Rows[0]["MANJURINAMA_BIRTH_DT_LOC"].ConvertToString();

                objenrolladd.PROXY_GFATHERS_FNAME_ENG = dt.Rows[0]["MANJURINAMA_GFATHER_FNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_GFATHERS_MNAME_ENG = dt.Rows[0]["MANJURINAMA_GFATHER_MNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_GFATHERS_LNAME_ENG = dt.Rows[0]["MANJURINAMA_GFATHER_LNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_GFATHERS_FNAME_LOC = dt.Rows[0]["MANJURINAMA_GFATHER_FNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_GFATHERS_MNAME_LOC = dt.Rows[0]["MANJURINAMA_GFATHER_MNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_GFATHERS_LNAME_LOC = dt.Rows[0]["MANJURINAMA_GFATHER_LNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_FATHERS_FNAME_ENG = dt.Rows[0]["MANJURINAMA_FATHER_FNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_FATHERS_MNAME_ENG = dt.Rows[0]["MANJURINAMA_FATHER_MNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_FATHERS_LNAME_ENG = dt.Rows[0]["MANJURINAMA_FATHER_LNAME_ENG"].ConvertToString();
                objenrolladd.PROXY_FATHERS_FNAME_LOC = dt.Rows[0]["MANJURINAMA_FATHER_FNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_FATHERS_MNAME_LOC = dt.Rows[0]["MANJURINAMA_FATHER_MNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_FATHERS_LNAME_LOC = dt.Rows[0]["MANJURINAMA_FATHER_LNAME_LOC"].ConvertToString();
                objenrolladd.PROXY_RELATION_TYPE_CD = dt.Rows[0]["MANJURINAMA_RELATION_TYPE_CD"].ToDecimal();
                objenrolladd.PROXY_PHONE = dt.Rows[0]["MANJURINAMA_PHONE"].ConvertToString();

                objenrolladd.BANK_ACC_NO = dt.Rows[0]["BANK_ACC_NO"].ConvertToString();
                objenrolladd.BANK_CD = dt.Rows[0]["BANK_CD"].ConvertToString();
                objenrolladd.BANK_BRANCH_CD = dt.Rows[0]["BANK_BRANCH_CD"].ConvertToString();
                objenrolladd.ACC_HOLDER_FNAME_ENG = dt.Rows[0]["ACC_HOLD_FNAME_ENG"].ConvertToString();
                objenrolladd.ACC_HOLDER_MNAME_ENG = dt.Rows[0]["ACC_HOLD_MNAME_ENG"].ConvertToString();
                objenrolladd.ACC_HOLDER_LNAME_ENG = dt.Rows[0]["ACC_HOLD_LNAME_ENG"].ConvertToString();

                objenrolladd.BUILDING_KITTA_NUMBER = dt.Rows[0]["BUILDING_KITTA_NUMBER"].ConvertToString();
                //objenrolladd.BUILDING_AREA_ENG = dt.Rows[0]["BUILDING_AREA_ENG"].ConvertToString();
                objenrolladd.BUILDING_AREA = dt.Rows[0]["BUILDING_AREA"].ConvertToString();
                objenrolladd.BUILDING_DISTRICT_CD = dt.Rows[0]["BUILDING_DISTRICT_CD"].ToDecimal();
                string buildingvdc = dt.Rows[0]["BUILDING_VDC_MUN_CD"].ConvertToString();
                objenrolladd.BUILDING_VDC_MUN_CD = common.GetDefinedCodeFromDataBase(buildingvdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD").ConvertToString();


                //objenrolladd.BUILDING_VDC_MUN_CD = dr["BUILDING_VDC_MUN_CD"].ConvertToString();
                objenrolladd.BUILDING_WARD_NO = dt.Rows[0]["BUILDING_WARD_NO"].ConvertToString();


                objenrolladd.NOMINEE_FNAME_ENG = dt.Rows[0]["NOMINEE_FNAME_ENG"].ConvertToString();
                objenrolladd.NOMINEE_MNAME_ENG = dt.Rows[0]["NOMINEE_MNAME_ENG"].ConvertToString();
                objenrolladd.NOMINEE_LNAME_ENG = dt.Rows[0]["NOMINEE_LNAME_ENG"].ConvertToString();
                objenrolladd.NOMINEE_RELATION_TYPE_CD = dt.Rows[0]["NOMINEE_RELATION_TYPE_CD"].ToDecimal();

                objenrolladd.Sakshi_FName_ENG = dt.Rows[0]["SHAKSHI_FNAME_ENG"].ConvertToString();
                objenrolladd.Sakshi_MName_ENG = dt.Rows[0]["SHAKSHI_MNAME_ENG"].ConvertToString();
                objenrolladd.Sakshi_LName_ENG = dt.Rows[0]["SHAKSHI_LNAME_ENG"].ConvertToString();
                #endregion



                //if (name.Length == 3)
                //{



                    objenrolladd.BENEFICIARY_FNAME_ENG = dt.Rows[0]["BFIRST_NAME_ENG"].ConvertToString();//name[0].ConvertToString();
                    objenrolladd.BENEFICIARY_MNAME_ENG = dt.Rows[0]["BMIDDLE_NAME_ENG"].ConvertToString();// name[1].ConvertToString();
                    objenrolladd.BENEFICIARY_LNAME_ENG = dt.Rows[0]["BLAST_NAME_ENG"].ConvertToString();// name[2].ConvertToString();
               // }


               // if (name.Length == 6)
               // {



                //    objenrolladd.BENEFICIARY_FNAME_ENG = name[0].ConvertToString();
                //    objenrolladd.BENEFICIARY_MNAME_ENG = name[1].ConvertToString();
                //    objenrolladd.BENEFICIARY_LNAME_ENG = name[2].ConvertToString();
                //}



                //if (name.Length == 9)
                //{



                //    objenrolladd.BENEFICIARY_FNAME_ENG = name[0].ConvertToString();
                //    objenrolladd.BENEFICIARY_MNAME_ENG = name[1].ConvertToString();
                //    objenrolladd.BENEFICIARY_LNAME_ENG = name[2].ConvertToString();
                //}



                //if (name.Length == 16)
                //{



                //    objenrolladd.BENEFICIARY_FNAME_ENG = name[0].ConvertToString();
                //    objenrolladd.BENEFICIARY_MNAME_ENG = name[1].ConvertToString();
                //    objenrolladd.BENEFICIARY_LNAME_ENG = name[2].ConvertToString();
                //}

                //if (name.Length == 2)
                //{



                //    objenrolladd.BENEFICIARY_FNAME_ENG = name[0].ConvertToString();

                //    objenrolladd.BENEFICIARY_LNAME_ENG = name[1].ConvertToString();
                //}

                //objenrolladd.BENEFICIARY_FNAME_LOC = dt.Rows[0]["BENEFICIARY_FNAME_LOC"].ConvertToString();



                BeneficiaryService objservice = new BeneficiaryService();
                DataTable dtt = new DataTable();
                dtt = objservice.GetOwner(dt.Rows[0]["HOUSE_OWNER_ID"].ConvertToString());
                //if (dtt != null)
                //{
                //    if (dtt.Rows.Count > 0)
                //    {
                ////        objenrolladd.BENEFICIARY_FNAME_ENG = dtt.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                ////        objenrolladd.BENEFICIARY_MNAME_ENG = dtt.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                ////        objenrolladd.BENEFICIARY_LNAME_ENG = dtt.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                ////        objenrolladd.BENEFICIARY_FULLNAME_ENG = dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString();
                //       objenrolladd.BENEFICIARY_FNAME_LOC = dtt.Rows[0]["BENEFICIARY_FNAME_LOC"].ConvertToString();
                ////        objenrolladd.BENEFICIARY_MNAME_LOC = dtt.Rows[0]["MIDDLE_NAME_LOC"].ConvertToString();
                ////        objenrolladd.BENEFICIARY_LNAME_LOC = dtt.Rows[0]["LAST_NAME_LOC"].ConvertToString();
                ////        objenrolladd.BENEFICIARY_FULLNAME_LOC = dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString();
                //    }
                //}
                objenrolladd.HOUSEHOLD_ID = dt.Rows[0]["HOUSEHOLD_ID"].ConvertToString();
                objenrolladd.HOUSE_OWNER_ID = dt.Rows[0]["HOUSE_OWNER_ID"].ConvertToString();
                objenrolladd.ENROLLMENT_ID = dt.Rows[0]["ENROLLMENT_ID"].ConvertToString();
                objenrolladd.TARGETING_ID = dt.Rows[0]["TARGETING_ID"].ConvertToString();
                objenrolladd.NRA_DEFINED_CD = dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString();
                objenrolladd.TARGET_BATCH_ID = dt.Rows[0]["TARGET_BATCH_ID"].ConvertToString();
                objenrolladd.NO_OF_HOUSE_OWNER = dt.Rows[0]["TOTAL_HOUSE_OWNER_CNT"].ConvertToString();
                objenrolladd.FISCAL_YR = dt.Rows[0]["FISCAL_YR"].ConvertToString();
                objenrolladd.MOU_ID = dt.Rows[0]["MOU_ID"].ConvertToString();
                objenrolladd.SURVEY_NO = dt.Rows[0]["SURVEY_NO"].ConvertToString();
                objenrolladd.HOUSE_ID = dt.Rows[0]["HOUSE_ID"].ConvertToString();
                objenrolladd.NISSA_NO = dt.Rows[0]["INSTANCE_UNIQUE_SNO"].ConvertToString();
                #region ADD BY NABIN
                // objenrolladd.BIRTH_DT = (dt.Rows[0]["BENEFICIARY_DOB_ENG"]).ToDateTime();
                //objenrolladd.BIRTH_DT_LOC = dt.Rows[0]["BENEFICIARY_DOB_LOC"].ConvertToString();
                //objenrolladd.PHONE_NO = dt.Rows[0]["BENEFICIARY_PHONE"].ConvertToString();
                // objenrolladd.migrationdate = System.Convert.ToDateTime(dt.Rows[0]["BENEFICIARY_MIGRATTION_DT"]).ToString("yyyy-MM-dd");//dt.Rows[0]["BENEFICIARY_MIGRATTION_DT"].ToDateTime();
                // objenrolladd.migrationdateloc = dt.Rows[0]["BENEFICIARY_MIGRATION_DT_LOC"].ConvertToString();
                //objenrolladd.migrationno = dt.Rows[0]["BENEFICIARY_MIGRATION_NO"].ConvertToString();
                // objenrolladd.FATHER_MEMBER_ID = dt.Rows[0]["FATHER_MEMBER_ID"].ConvertToString();
                //  objenrolladd.FATHER_FNAME_ENG = dt.Rows[0]["FATHER_FNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_MNAME_ENG = dt.Rows[0]["FATHER_MNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_LNAME_ENG = dt.Rows[0]["FATHER_LNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_FullNAME_ENG = dt.Rows[0]["FATHER_FULLNAME_ENG"].ConvertToString();
                //  objenrolladd.FATHER_FNAME_LOC = dt.Rows[0]["FATHER_FNAME_LOC"].ConvertToString();
                //  objenrolladd.FATHER_MNAME_LOC = dt.Rows[0]["FATHER_MNAME_LOC"].ConvertToString();
                //  objenrolladd.FATHER_LNAME_LOC = dt.Rows[0]["FATHER_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.FATHER_FullNAME_LOC = dt.Rows[0]["FATHER_FULLNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_MEMBER_ID = dt.Rows[0]["GFATHER_MEMBER_ID"].ConvertToString();
                //  objenrolladd.GFATHER_FNAME_ENG = dt.Rows[0]["GFATHER_FNAME_ENG"].ConvertToString();
                //  objenrolladd.GFATHER_MNAME_ENG = dt.Rows[0]["GFATHER_MNAME_ENG"].ConvertToString();
                //  objenrolladd.GFATHER_LNAME_ENG = dt.Rows[0]["GFATHER_LNAME_ENG"].ConvertToString();
                // // objenrolladd.GFATHER_FullNAME_ENG = dt.Rows[0]["GFATHER_FULLNAME_ENG"].ConvertToString();
                //  //objenrolladd.GFATHER_FNAME_LOC = dt.Rows[0]["GFATHER_FNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_MNAME_LOC = dt.Rows[0]["GFATHER_MNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_LNAME_LOC = dt.Rows[0]["GFATHER_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.GFATHER_FullNAME_LOC = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();

                //  objenrolladd.husbandfnameeng = dt.Rows[0]["SPOUSE_FIRST_NAME_ENG"].ConvertToString();
                //  objenrolladd.husbandMnameeng = dt.Rows[0]["SPOUSE_MIDDLE_NAME_ENG"].ConvertToString();
                //  objenrolladd.husbandLnameeng = dt.Rows[0]["SPOUSE_LAST_NAME_ENG"].ConvertToString();
                //  //objenrolladd.husbandFullnameEng = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();
                //  objenrolladd.FinlawFnameEng = dt.Rows[0]["FATHER_INLAW_FNAME_ENG"].ConvertToString();
                //  objenrolladd.FinlawMnameEng = dt.Rows[0]["FATHER_INLAW_MNAME_ENG"].ConvertToString();
                //  objenrolladd.FinlawLnameEng = dt.Rows[0]["FATHER_INLAW_LNAME_ENG"].ConvertToString();
                ////  objenrolladd.FinLawFullNameEng = dt.Rows[0]["GFATHER_FULLNAME_LOC"].ConvertToString();

                //  //objenrolladd.BUILDING_KITTA_NUMBER = dt.Rows[0]["BUILDING_KITTA_NUMBER"].ConvertToString();
                //  //objenrolladd.BUILDING_AREA = dt.Rows[0]["BUILDING_AREA"].ConvertToString();

                //  //objenrolladd.BUILDING_AREA_ENG = dt.Rows[0]["BUILDING_AREA_ENG"].ConvertToString();
                //  //objenrolladd.BUILDING_AREA_LOC = dt.Rows[0]["BUILDING_AREA_LOC"].ConvertToString();
                //  //objenrolladd.BUILDING_WALL_OR_PILLAR_TYPE_NO = dt.Rows[0]["BUILDING_PILER_TYPE"].ConvertToString();
                //  //objenrolladd.BUILDING_FLOOR_OR_ROOF_TYPE_NO = dt.Rows[0]["BUILDING_FLOOR_ROOF_TYPE"].ConvertToString();
                //  //objenrolladd.BUILDING_DESIGN_OTHER = dt.Rows[0]["BUILDING_OTHER_DEG"].ConvertToString();

                //  //objenrolladd.NOMINEE_FNAME_ENG = dt.Rows[0]["NOMINEE_FNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_MNAME_ENG = dt.Rows[0]["NOMINEE_MNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_LNAME_ENG = dt.Rows[0]["NOMINEE_LNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_FULLNAME_ENG = dt.Rows[0]["NOMINEE_FULLNAME_ENG"].ConvertToString();
                //  //objenrolladd.NOMINEE_FNAME_LOC = dt.Rows[0]["NOMINEE_FNAME_LOC"].ConvertToString();
                //  //objenrolladd.NOMINEE_MNAME_LOC = dt.Rows[0]["NOMINEE_MNAME_LOC"].ConvertToString();
                //  //objenrolladd.NOMINEE_LNAME_LOC = dt.Rows[0]["NOMINEE_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.NOMINEE_FULLNAME_LOC = dt.Rows[0]["NOMINEE_FULLNAME_LOC"].ConvertToString();
                //  // objenrolladd.IDENTIFICATION_TYPE_CD = common.GetValueFromDataBase(dt.Rows[0]["IDENTIFICATION_TYPE_CD"].ConvertToString(), "NHRS_IDENTIFICATION_TYPE", "IDENTIFICATION_TYPE_CD", "DEFINED_CD").ToDecimal();//dt.Rows[0]["IDENTIFICATION_TYPE_CD"].ToDecimal();
                //  objenrolladd.IDENTIFICATION_NO = dt.Rows[0]["IDENTIFICATION_NO"].ConvertToString();
                //  // objenrolladd.IDENTIFICATION_DOCUMENT = dt.Rows[0]["IDENTIFICATION_DOCUMENT"].ConvertToString();
                //  objenrolladd.IDENTIFICATION_ISSUE_DIS_CD = GetData.GetDefinedCodeFor(DataType.AllDistrict, dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"].ConvertToString()).ToDecimal();//dt.Rows[0]["IDENTIFICATION_ISSUE_DIS_CD"].ToDecimal();
                //  //objenrolladd.IDENTIFICATION_ISSUE_DT = System.Convert.ToDateTime(dt.Rows[0]["IDENTIFICATION_ISSUE_DT"]).ToDateTime("yyyy-MM-dd");//dt.Rows[0]["IDENTIFICATION_ISSUE_DT"].ToDateTime();
                //  objenrolladd.IDENTIFICATION_ISSUE_DT = (dt.Rows[0]["IDENTIFICATION_ISSUE_DT"]).ToDateTime();
                //  objenrolladd.IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                //  //objenrolladd.IDENTIFICATION_ISS_DT_LOC = dt.Rows[0]["IDENTIFICATION_ISS_DT_LOC"].ConvertToString();
                // objenrolladd.PROXY_RELATION_TYPE_CD = common.GetValueFromDataBase(dt.Rows[0]["MANJURINAMA_RELATION_TYPE_CD"].ConvertToString(), "MIS_RELATION_TYPE", "RELATION_TYPE_CD", "DEFINED_CD").ToDecimal();//dt.Rows[0]["MANJURINAMA_RELATION_TYPE_CD"].ToDecimal();
                //  //objenrolladd.PROXY_FNAME_ENG = dt.Rows[0]["MANJURINAMA_FNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_MNAME_ENG = dt.Rows[0]["MANJURINAMA_MNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_LNAME_ENG = dt.Rows[0]["MANJURINAMA_LNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_FULLNAME_ENG = dt.Rows[0]["MANJURINAMA_FULLNAME_ENG"].ConvertToString();
                //  //objenrolladd.PROXY_FNAME_LOC = dt.Rows[0]["MANJURINAMA_FNAME_LOC"].ConvertToString();
                //  //objenrolladd.PROXY_MNAME_LOC = dt.Rows[0]["MANJURINAMA_MNAME_LOC"].ConvertToString();
                //  //objenrolladd.PROXY_LNAME_LOC = dt.Rows[0]["MANJURINAMA_LNAME_LOC"].ConvertToString();
                //  //objenrolladd.PROXY_FULLNAME_LOC = dt.Rows[0]["MANJURINAMA_FULLNAME_LOC"].ConvertToString();
                #endregion
            }

            //objenroll.ENROLLMENT_ID = rvd.ConvertToString();

            //objenroll.ADD = "ADD";
            ViewData["ddl_BenfDistrict"] = common.GetDistricts(objenrolladd.DISTRICT_CD.ConvertToString());
            ViewData["ddlManjurinama"] = common.GetBeneficiary(objenrolladd.IS_MANJURINAMA_AVAILABLE);
            ViewData["ddlTypeofHouseConstructed"] = common.GetBeneficiary("");
            ViewData["ddlDesignCatalogNo"] = common.GetCatalogNumber("");
            ViewData["ddlHaveOwnDesign"] = common.GetBeneficiary("");
            string Districtcode = objenroll.DISTRICT_CD;
            string VdcMun = objenroll.VDC_MUN_CD;
            ViewData["ddl_DistrictsProxy"] = common.GetDistricts(objenrolladd.PROXY_DISTRICT_CD.ConvertToString());
            ViewData["ddl_DistrictsBuilding"] = common.GetDistricts(objenrolladd.BUILDING_DISTRICT_CD.ConvertToString());
            ViewData["ddl_DistrictBenfIdentity"] = common.GetAllDistricts(objenrolladd.IDENTIFICATION_ISSUE_DIS_CD.ConvertToString());
            //string definedWard = common.GetDefinedCodeFromDataBase(objenrolladd.WARD_NO.ConvertToString(),"MIS_WARD", "WARD_NO");
            string wardcode = objenrolladd.WARD_NO;
            ViewData["ddl_BenfWard"] = common.GetWardByVDCMun(wardcode, objenrolladd.VDC_MUN_CD.ConvertToString());

            ViewData["ddl_DistrictProxyIssue"] = common.GetAllDistricts(objenrolladd.PROXY_IDENTIFICATION_ISSUE_DIS_CD.ConvertToString());
            ViewData["ddl_DistrictsManjurinamaDistrict"] = common.GetAllDistricts("");

            string definedVDCCD = common.GetDefinedCodeFromDataBase(objenrolladd.VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "DEFINED_CD");
            ViewData["ddl_VDC"] = common.GetVDCMunByAllDistrict(definedVDCCD, objenrolladd.DISTRICT_CD.ConvertToString());
            string definedVDCCDProxy = common.GetDefinedCodeFromDataBase(objenrolladd.PROXY_VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "DEFINED_CD").ConvertToString();
            //ViewData["ddl_VDCMunProxy"] = common.GetVDCMunByDistrict("","");

            ViewData["ddl_VDCMunProxy"] = common.GetVDCMunByAllDistrict(definedVDCCDProxy, objenrolladd.PROXY_DISTRICT_CD.ConvertToString());
            string Bwardcode = objenrolladd.BUILDING_WARD_NO;
            ViewData["ddl_WardBuilding"] = common.GetWardByVDCMun(Bwardcode, objenrolladd.BUILDING_VDC_MUN_CD.ConvertToString());
            string Pwardcode = objenrolladd.PROXY_WARD_NO;
            ViewData["ddl_WardProxy"] = common.GetWardByVDCMun(Pwardcode, objenrolladd.PROXY_VDC_MUN_CD.ConvertToString());
            string BenefWardCode = objenrolladd.WARD_NO.ConvertToString();
            ViewData["ddl_Ward"] = common.GetWardByVDCMun(BenefWardCode, objenrolladd.VDC_MUN_CD.ConvertToString());

            //ViewData["ddl_WardProxy"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Relation"] = common.GetRelation(objenrolladd.PROXY_RELATION_TYPE_CD.ConvertToString());

            string Bvdccode = common.GetDefinedCodeFromDataBase(objenrolladd.BUILDING_VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "DEFINED_CD");
            ViewData["ddl_VDCMunBuilding"] = common.GetVDCMunByAllDistrict(Bvdccode, objenrolladd.BUILDING_DISTRICT_CD.ConvertToString());//ddl_VDCMunBuilding
            //ViewData["ddl_bank_name"] = common.GetBankName("");
            //ViewData["ddl_bank_branch_name"] = common.GetBankBranchId("");
            string BankCode = common.GetDefinedCodeFromDataBase(objenrolladd.BANK_CD.ConvertToString(), "NHRS_BANK", "BANK_CD").ConvertToString();
            ViewData["ddl_bank_name"] = common.GetBankName(BankCode);
            // ViewData["ddl_bank_name"] = common.GetBankName(objenrolladd.BANK_CD.ConvertToString());

            // ViewData["ddl_bank_name"] = common.GetBankByAddress(objenrolladd.BANK_CD.ToDe);

            ViewData["ddl_bank_branch_name"] = common.GetBankBranchId(objenrolladd.BANK_BRANCH_CD.ConvertToString());
            ViewData["ddl_bank_acc_type"] = common.GetBankAccountType("");
            ViewData["ddl_IdentificationProxy"] = househead.GetIdentificationType("");
            ViewData["ddl_IdentificationBeneficiary"] = househead.GetIdentificationType("");
            ViewData["ddlmigration"] = common.GetMigration("");
            ViewData["ddlRelationFather"] = common.GetFatherRelation("");
            ViewData["ddlRelationGrandFather"] = common.GetGrandFatherRelation("");
            ViewData["ddl_SecretaryOffice"] = common.GetVDCSecretaryOffice(CommonVariables.EmpCode, "");
            ViewData["ddlNomineeRelationType"] = common.GetRelation(objenrolladd.NOMINEE_RELATION_TYPE_CD.ConvertToString());
            ViewData["ddlDesignfromCatalog"] = common.GetBeneficiary("");
            ViewData["ddlmarriage"] = common.GetMigration("");
            ViewData["ddlDocumentListType"] = common.GetDocumentType("");

            Session["EnrollObj"] = objenrolladd;
            return View(objenrolladd);
            //return RedirectToAction("EnrollmentView", objenroll);



        }

        //Convert HTML to PDF 
        #region HTML TO PDF
        public ActionResult GenPDF()
        {
            HtmlToPdf converter = new HtmlToPdf();
            InspectionDetailService ins = new InspectionDetailService();
            TextWriter writer = new StringWriter();
            string htmlString = RenderViewToString("EnrollmentAdd1");

            string baseUrl = ins.GetBaseUrl();

            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

            byte[] pdf = doc.Save();
            doc.Close();
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "EnrollmentForm.pdf";
            return fileResult;
        }

        protected string RenderViewToString(string viewName)
        {
            ViewData.Model = Session["EnrollObj"];
            EnrollmentAdd objenrolladd = (EnrollmentAdd)Session["EnrollObj"];
            ViewData["ddl_BenfDistrict"] = common.GetDistricts(objenrolladd.DISTRICT_CD.ConvertToString());
            ViewData["ddlManjurinama"] = common.GetBeneficiary(objenrolladd.IS_MANJURINAMA_AVAILABLE);
            ViewData["ddlTypeofHouseConstructed"] = common.GetBeneficiary("");
            ViewData["ddlDesignCatalogNo"] = common.GetCatalogNumber("");
            ViewData["ddlHaveOwnDesign"] = common.GetBeneficiary("");
            ViewData["ddl_DistrictsProxy"] = common.GetDistricts(objenrolladd.PROXY_DISTRICT_CD.ConvertToString());
            ViewData["ddl_DistrictsBuilding"] = common.GetDistricts(objenrolladd.BUILDING_DISTRICT_CD.ConvertToString());
            ViewData["ddl_DistrictBenfIdentity"] = common.GetAllDistricts(objenrolladd.IDENTIFICATION_ISSUE_DIS_CD.ConvertToString());
            //string definedWard = common.GetDefinedCodeFromDataBase(objenrolladd.WARD_NO.ConvertToString(),"MIS_WARD", "WARD_NO");
            string wardcode = objenrolladd.WARD_NO;
            ViewData["ddl_BenfWard"] = common.GetWardByVDCMun(wardcode, objenrolladd.VDC_MUN_CD.ConvertToString());

            ViewData["ddl_DistrictProxyIssue"] = common.GetAllDistricts(objenrolladd.PROXY_IDENTIFICATION_ISSUE_DIS_CD.ConvertToString());
            ViewData["ddl_DistrictsManjurinamaDistrict"] = common.GetAllDistricts("");

            string definedVDCCD = common.GetDefinedCodeFromDataBase(objenrolladd.VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "DEFINED_CD");
            ViewData["ddl_VDC"] = common.GetVDCMunByAllDistrict(definedVDCCD, objenrolladd.DISTRICT_CD.ConvertToString());
            string definedVDCCDProxy = common.GetDefinedCodeFromDataBase(objenrolladd.PROXY_VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "DEFINED_CD").ConvertToString();
            //ViewData["ddl_VDCMunProxy"] = common.GetVDCMunByDistrict("","");

            ViewData["ddl_VDCMunProxy"] = common.GetVDCMunByAllDistrict(definedVDCCDProxy, objenrolladd.PROXY_DISTRICT_CD.ConvertToString());
            string Bwardcode = objenrolladd.BUILDING_WARD_NO;
            ViewData["ddl_WardBuilding"] = common.GetWardByVDCMun(Bwardcode, objenrolladd.BUILDING_VDC_MUN_CD.ConvertToString());
            string Pwardcode = objenrolladd.PROXY_WARD_NO;
            ViewData["ddl_WardProxy"] = common.GetWardByVDCMun(Pwardcode, objenrolladd.PROXY_VDC_MUN_CD.ConvertToString());
            string BenefWardCode = objenrolladd.WARD_NO.ConvertToString();
            ViewData["ddl_Ward"] = common.GetWardByVDCMun(BenefWardCode, objenrolladd.VDC_MUN_CD.ConvertToString());

            //ViewData["ddl_WardProxy"] = common.GetWardByVDCMun("", "");
            ViewData["ddl_Relation"] = common.GetRelation(objenrolladd.PROXY_RELATION_TYPE_CD.ConvertToString());

            string Bvdccode = common.GetDefinedCodeFromDataBase(objenrolladd.BUILDING_VDC_MUN_CD.ConvertToString(), "MIS_VDC_MUNICIPALITY", "DEFINED_CD");
            ViewData["ddl_VDCMunBuilding"] = common.GetVDCMunByAllDistrict(Bvdccode, objenrolladd.BUILDING_DISTRICT_CD.ConvertToString());//ddl_VDCMunBuilding
            //ViewData["ddl_bank_name"] = common.GetBankName("");
            //ViewData["ddl_bank_branch_name"] = common.GetBankBranchId("");
            string BankCode = common.GetDefinedCodeFromDataBase(objenrolladd.BANK_CD.ConvertToString(), "NHRS_BANK", "BANK_CD").ConvertToString();
            ViewData["ddl_bank_name"] = common.GetBankName(BankCode);
            // ViewData["ddl_bank_name"] = common.GetBankName(objenrolladd.BANK_CD.ConvertToString());

            // ViewData["ddl_bank_name"] = common.GetBankByAddress(objenrolladd.BANK_CD.ToDe);

            ViewData["ddl_bank_branch_name"] = common.GetBankBranchId(objenrolladd.BANK_BRANCH_CD.ConvertToString());
            ViewData["ddl_bank_acc_type"] = common.GetBankAccountType("");
            ViewData["ddl_IdentificationProxy"] = househead.GetIdentificationType("");
            ViewData["ddl_IdentificationBeneficiary"] = househead.GetIdentificationType("");
            ViewData["ddlmigration"] = common.GetMigration("");
            ViewData["ddlRelationFather"] = common.GetFatherRelation("");
            ViewData["ddlRelationGrandFather"] = common.GetGrandFatherRelation("");
            ViewData["ddl_SecretaryOffice"] = common.GetVDCSecretaryOffice(CommonVariables.EmpCode, "");
            ViewData["ddlNomineeRelationType"] = common.GetRelation(objenrolladd.NOMINEE_RELATION_TYPE_CD.ConvertToString());
            ViewData["ddlDesignfromCatalog"] = common.GetBeneficiary("");
            ViewData["ddlmarriage"] = common.GetMigration("");
            ViewData["ddlDocumentListType"] = common.GetDocumentType("");

            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, "_Layout");
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }

        }

        #endregion HTML TO PDF
        public ActionResult Edit(string p)
        {
            return RedirectToAction("EnrollmentView", new { p = p });
        }
        //For Changing Approval Status
        [CustomAuthorizeAttribute(PermCd = "7")]
        public void ChangeApprovalStatus(string p)
        {
            Users objUsers = new Users();
            UsersService userService = new UsersService();
            string strUserName = string.Empty;
            EmailMessage objEmail = new EmailMessage();
            try
            {
                if (Session[SessionCheck.sessionName] != null)
                {
                    objUsers = (Users)Session[SessionCheck.sessionName];
                    strUserName = objUsers.usrName;
                }
                string status = string.Empty;
                string id = string.Empty;
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }
                    }
                }
                objUsers = new Users();
                objUsers.usrCd = id;
                if (status == "Y")
                {
                    objUsers.approved = false;
                }
                else
                {
                    objUsers.approved = true;
                }
                objUsers.enteredBy = strUserName;
                userService.UserUID(objUsers, null, "A");
                objUsers = new Users();
                objUsers = userService.PopulateUserDetails(id);
                if (objUsers.approved == true && objUsers.status == "E")
                {
                    objEmail.To = objUsers.email;
                    objEmail.From = MailSend.adminEmail;
                    objEmail.Subject = "User Approval";
                    objEmail.Body = EmailTemplate.UserApproval(objUsers);
                    MailSend.SendMail(objEmail);
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
        public ActionResult approveUnApproveStatus(string p)
        {
            Users obj;
            string strUsername = "";
            string status = "";
            string statusChange = "";
            string houseOwnerId = "";
            EnrollmentSearch objenrollsearch = new EnrollmentSearch();
            EnrollmentService objServ = new EnrollmentService();
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
                    statusChange = "N";

                }
                else if (status == "N")
                {
                    statusChange = "Y";

                }
                if (Session[SessionCheck.sessionName] != null)
                {
                    obj = (Users)Session[SessionCheck.sessionName];
                    strUsername = obj.usrName;
                }
                //objenrollsearch.House_Owner_ID = Convert.ToString(id);
                houseOwnerId = Convert.ToString(id);

                objServ.UpdateApproveUnapproveStatus(statusChange, houseOwnerId);

                //DataTable result = new DataTable();
                //EnrollmentSearch objEnroll = (EnrollmentSearch)Session["objSession"];
                //result = objServ.GetEnrollmentSearchDetail(objEnroll);
                //ViewData["result"] = result;




            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("EnrollmentList");
            //return PartialView("~/Views/Enrollment/_EnrollmentInfo.cshtml");



        }

        public void CheckPermission()
        {
            PermissionParamService objPermissionParamService = new PermissionParamService();
            PermissionParam objPermissionParam = new PermissionParam();
            ViewBag.EnableEdit = "false";
            ViewBag.EnableDelete = "false";
            ViewBag.EnableAdd = "false";
            ViewBag.EnableApprove = "false";
            ViewBag.EnablePrint = "false";
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
                    if (objPermissionParam.EnablePrint == "true")
                    {
                        ViewBag.EnablePrint = "true";
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


        public ActionResult ChangeStatus(string p)
        {
            Users users = new Users();
            UsersService userService = new UsersService();
            string strUserName = string.Empty;
            try
            {
                if (Session[SessionCheck.sessionName] != null)
                {
                    users = (Users)Session[SessionCheck.sessionName];
                    strUserName = users.usrName;
                }
                string status = string.Empty;
                string id = string.Empty;
                RouteValueDictionary rvd = new RouteValueDictionary();
                if (p != null)
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (rvd != null)
                    {
                        if (rvd["id"] != null)
                        {
                            id = (rvd["id"].ConvertToString());
                        }
                        if (rvd["status"] != null)
                        {
                            status = rvd["status"].ToString();
                        }
                    }
                }
                users = new Users();
                users.usrCd = id;
                if (status == "E")
                {
                    status = "D";
                }
                else
                {
                    status = "E";
                }
                users.status = status;
                users.enteredBy = strUserName;
                userService.UserUID(users, null, "S");
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return RedirectToAction("UsersList");
        }

        public void GetDropDown()
        {
            try
            {
                //ViewData["ddl_VDCMunPer"] = common.GetVDCMun("");
                //ViewData["ddl_WardPer"] = common.GetWardByVdcForUser("");
                ViewData["ddl_MOU"] = (List<SelectListItem>)common.GetYesNo("").Data;
                ViewData["ddl_PS"] = (List<SelectListItem>)common.GetYesNo("").Data;

                ViewData["IDENTIFICATION_TYPE"] = househead.GetIdentificationType("");
                ViewData["ddl_Identification"] = househead.GetIdentificationType("");
                ViewData["ddl_bank_name"] = common.GetBankName("");
                ViewData["ddl_bank_branch_name"] = common.GetBankBranchId("");
                ViewData["ddl_bank_acc_type"] = common.GetBankAccountType("");
                //ViewData["ddl_District"] = common.GetDistricts("");
                ViewData["ddl_RegionPer"] = common.GetRegionForUser("");




                ViewData["Residential_house"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Reconstruction"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Geographical_Risk"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Secondary_Use"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Bank_Account"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Citizenship_victim"] = (List<SelectListItem>)common.GetYesNo1("").Data;

                ViewData["Death"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["StudentsLeft"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Pregnant Checkup"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["VaccinationCnt"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ChangedProfession"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["Missing"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_Gender"] = common.GetGender("").ToList();

                ViewData["ddl_householdhead_gender"] = common.GetGender("");
                ViewData["ddl_houseownergender"] = common.GetGender("");
                ViewData["ddl_familymembergender"] = common.GetGender("");
                ViewData["ddlEducation"] = common.GetEducation("");

                ViewData["ddlMaritalStatus"] = common.GetMaritalStatus("");
                ViewData["ddlSocialAllowance"] = common.GetAllowanceType("");
                ViewData["ddlHandicappedness"] = common.GetHandiColor("");
                ViewData["ddlDeathCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddlBirthCertificate"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeasedGender"] = common.GetGender("");
                ViewData["ddl_DeathRegistered"] = (List<SelectListItem>)common.GetYesNo1("").Data;
                ViewData["ddl_DeathCause"] = common.GetDeathReason("");
                ViewData["ddl_EarthquakeVictimGender"] = common.GetGender("");
                ViewData["ddl_TypeOfInjury"] = common.GetHumanLoss("");
                ViewData["ddl_Caste_Group"] = common.GetCasteGroup("");
                ViewData["ddl_Religion"] = common.GetReligion("");
                ViewData["ddl_ClassType"] = common.GetClassType("");
                ViewData["ddl_Relation"] = common.GetRelation1("");
                ViewData["ddl_RelationHousehead"] = common.GetRelation("");
                enrollmentclass objenroll = new enrollmentclass();
                //ViewData["ddl_District"] = common.GetDistricts(objenroll.DISTRICT_CD);
                //ViewData["dl_VDCMunPer"] = common.GetRegionState("");

                ViewData["ddl_District"] = common.GetDistrictsByDistrictCode(objenroll.DISTRICT_CD);
                ViewData["ddl_VDCMunPer"] = common.GetVDCMunByDistrict(objenroll.VDC_MUN_CD, objenroll.DISTRICT_CD);


                ViewData["ddl_WardPer"] = common.GetWardByVdcForUser("");


                //ViewData["ddl_Zone"] = common.GetZone("");
                //ViewData["ddl_VDCMunPer"] = common.GetVDCMun(objenroll.VDC_MUNICIPALITY_CD);
                //ViewData["ddl_WardPer"] = common.GetWardByVdcForUser(objenroll.WARD_NUM_CD);
                ViewData["ddl_MOU"] = (List<SelectListItem>)common.GetYesNo("").Data;
                ViewData["ddl_PS"] = (List<SelectListItem>)common.GetYesNo("").Data;

                ViewData["IDENTIFICATION_TYPE"] = househead.GetIdentificationType("");
                ViewData["ddl_Identification"] = househead.GetIdentificationType("");
                ViewData["ddl_bank_name"] = common.GetBankName("");
                ViewData["ddl_bank_branch_name"] = common.GetBankBranchId("");
                ViewData["ddl_bank_acc_type"] = common.GetBankAccountType("");

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


        #region Import
        public ActionResult OpenImport()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {

                if (Session["ImportMessage"].ConvertToString() == "Success")
                {
                    ViewData["FinalMessage"] = "Data imported successfully.";
                }
                if (Session["ImportMessage"].ConvertToString() == "Mismatch")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "Ward does not matched with PA NO. at row position " + errorrow + ".Please correct the ward.";
                }
                if (Session["ImportMessage"].ConvertToString() == "PADuplicate")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = " PA_NO at row position " + errorrow + " is  already uploaded.Please correct the data.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FinalMessage"] = "Data import process failed.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Data import failed due to duplicate file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "PADoNotExists")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "PA Number at row position " + errorrow + " does not exists.Please review the file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "WrongFormat")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "PA_Number is in wrong format at row position " + errorrow + " .Please review the file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "DuplicateInDatabse")
                {
                    ViewData["FinalMessage"] = "Data imported successfully and found some duplicate records.";

                }

                Session["ImportMessage"] = "";
            }
            return View();
        }
        [HttpGet]
        public ActionResult SaveALLCSVFile()
        {
            ViewData["ddl_District"] = common.GetDistricts("");
            ViewData["ddl_VDCMun"] = common.GetVDCMunByDistrict("", "");

            if (!string.IsNullOrEmpty(Session["ImportMessage"].ConvertToString()))
            {
                if (Session["ImportMessage"].ConvertToString() == "PADuplicate")
                {
                    string errorrow = Session["rowposition"].ToString();
                    ViewData["FinalMessage"] = "Data imported successfully and found some duplicate records.";
                }
                if (Session["ImportMessage"].ConvertToString() == "Success")
                {
                    ViewData["FinalMessage"] = "Data imported successfully.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "Failed")
                {
                    ViewData["FinalMessage"] = Session["exception"].ConvertToString();
                }
                else if (Session["ImportMessage"].ConvertToString() == "Conflicted")
                {
                    ViewData["FinalMessage"] = "Conflicted";
                }
                 
                    
                else if (Session["ImportMessage"].ConvertToString() == "Duplicate File")
                {
                    ViewData["FinalMessage"] = "Data import failed due to duplicate file.";
                }
                else if (Session["ImportMessage"].ConvertToString() == "All duplicate")
                {
                    ViewData["FinalMessage"] = "All duplicate";
                }
                else if (Session["ImportMessage"].ConvertToString() == "PADoNotExists")
                {
                    ViewData["FinalMessage"] = "Invalid PA at row " + Session["rowposition"].ConvertToString();
                }
                else if (Session["ImportMessage"].ConvertToString() == "Import Failed")
                {
                    ViewData["FinalMessage"] = Session["ImportMessage"].ConvertToString();
                }
                
                ViewData["FileName"] = Session["fileNamePA"].ConvertToString();
                Session["ImportMessage"] = "";
            }
            return View();
        }
        //public ActionResult EnrollmentFileImport(NhrsHouseDetail mdl)
        //{

        //    EnrollmentFileImportService objReportService = new EnrollmentFileImportService();
        //    DataTable dtFileStatusReport = new DataTable();
        //    List<string> jsonFiles = Directory.GetDirectories(Server.MapPath("~/Files/CSV_Enrollment/"), "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();

        //    dtFileStatusReport = objReportService.GetDataImportRecord(jsonFiles);
        //    string id = null;
        //    id = mdl.DISTRICT_CD_Defined.ConvertToString();
        //    ViewData["dtFileStatusReport"] = dtFileStatusReport;

        //    ViewData["ddldistrict"] = common.GetDistricts("");
        //    ViewData["dtEnrollmentFileImportRslt"] = dtFileStatusReport;
        //    return View();
        //}

        //public ActionResult GetEnrollmentFileImport(FormCollection fc, string SearchValueDistrict, string SearchValueVdc)
        //{
        //    EnrollmentFileImportService objReportService = new EnrollmentFileImportService();
        //    string id = string.Empty;
        //    string Dist_CD = fc["ddl_District"].ConvertToString();
        //    ViewData["VDC"] = common.GetVDCMunByDistrictCode(SearchValueDistrict, Dist_CD);

        //    try
        //    {
        //        DataTable dtFileStatusReport = new DataTable();
        //        List<string> jsonFiles = new List<string>();
        //        List<string> jsonFiles1 = new List<string>();
        //        List<string> vdc = new List<string>();
        //        string filepath = "";
        //        if (string.IsNullOrEmpty(SearchValueVdc))
        //        {
        //            filepath = Server.MapPath("~/Files/CSV_Enrollment/" + SearchValueDistrict + "/");
        //        }
        //        else
        //        {
        //            filepath = Server.MapPath("~/Files/CSV_Enrollment/" + SearchValueDistrict + "/" + SearchValueVdc + "/");
        //        }

        //        if (Directory.Exists(filepath))
        //        {
        //            if (!string.IsNullOrEmpty(SearchValueVdc))
        //            {
        //                jsonFiles = Directory.GetDirectories(filepath, "*", System.IO.SearchOption.TopDirectoryOnly).Select(path => SearchValueDistrict + "_" + SearchValueVdc + "_" + Path.GetFileName(path)).ToList();

        //                dtFileStatusReport = objReportService.GetDataImportRecordByVdc(jsonFiles);
        //            }
        //            else
        //            {
        //                jsonFiles = Directory.GetDirectories(filepath, "*", System.IO.SearchOption.TopDirectoryOnly).Select(path => Path.GetFileName(path)).ToList();
        //                foreach (string s in jsonFiles)
        //                {
        //                    vdc.Add(s);
        //                    jsonFiles1.AddRange(Directory.GetDirectories(filepath + s + "\\", "*", System.IO.SearchOption.TopDirectoryOnly).Select(path => SearchValueDistrict + "_" + s + "_" + Path.GetFileName(path)).ToList());
        //                }

        //                dtFileStatusReport = objReportService.GetDataImportRecordByDistrict(jsonFiles1, vdc);
        //            }


        //            //jsonFiles = Directory.GetDirectories(filepath, "*", System.IO.SearchOption.TopDirectoryOnly).Select(path => Path.GetFileName(path)).ToList();

        //            //dtFileStatusReport = objReportService.GetDataImportRecord(jsonFiles);
        //        }

        //        ViewData["dtEnrollmentFileImportRslt"] = dtFileStatusReport;

        //    }
        //    catch (OracleException oe)
        //    {
        //        ExceptionManager.AppendLog(oe);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.AppendLog(ex);
        //    }
        //    return PartialView();
        //    //  return View("FileImport");

        //}

        [HttpPost]
        public ActionResult SaveALLCSVFile(string paramFilename, string District, HttpPostedFileBase file)
        {
            int surveyCount  = 0;
            string exc = string.Empty;
            string errormessage=string.Empty;
            EnrollmentFileImportService objService = new EnrollmentFileImportService();
            bool rs = false;
            for (int k = 0; k < Request.Files.Count; k++)
            {
                file = Request.Files[k];
                if (file.ContentLength == 0)
                    continue;
                string FilePath = "~/Files/CSV_Enrollment/" + District + "/";
                if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                }
                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName));
                try
                {



                    List<string> fileListInDB = new List<string>();
                    fileListInDB = objService.EnrollmentPaFile();
                    if (!fileListInDB.Contains(file.FileName))
                    {
                        int lnNum = 0;
                        DataTable oDataTable = new DataTable();
                        //string extension = Path.GetExtension(finalPath);
                        string extension = Path.GetExtension(finalPath);
                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            oDataTable = Import_To_Grid(finalPath, ".xlsx", "Yes");//, " ");
                        }
                        else if (extension.ToLower() == ".csv")
                        {
                            using (StreamReader reader = System.IO.File.OpenText(finalPath))
                            {
                                var csv = new CsvReader(reader);
                                string[] columnsNames = null;
                                string[] ostreamDataValues = null;
                                while (!reader.EndOfStream)
                                {

                                    var oStreamData = reader.ReadLine().Trim();


                                    if (oStreamData.Length > 0)
                                    {
                                        ostreamDataValues = oStreamData.Split(',');
                                        if (lnNum == 0)
                                        {
                                        
                                            lnNum++;
                                            columnsNames = ostreamDataValues;
                                       
                                            foreach (string csbHeader in columnsNames)
                                            {

                                                if (csbHeader.ConvertToString() != "")
                                                {
                                                    DataColumn dtCol = new DataColumn(csbHeader.ToUpper(), typeof(string));
                                                    dtCol.DefaultValue = string.Empty;
                                                    oDataTable.Columns.Add(dtCol);
                                                }
                                                 

                                            }

                                        }
                                        else
                                        {
                                            DataRow oDataRow = oDataTable.NewRow();
                                            for (int i = 0; i < columnsNames.Length; i++)
                                            {
                                                var colname = columnsNames[i];
                                                var oStreamDataValue = ostreamDataValues;
                                                if (colname.ConvertToString() != "")
                                                {
                                                    oDataRow[colname] = string.IsNullOrEmpty(oStreamDataValue[i]) ? string.Empty : oStreamDataValue[i].ToString().TrimStart('0').Length > 0 ? oStreamDataValue[i].TrimStart('0') : "0";
                                                }
                                                

                                            }
                                            oDataTable.Rows.Add(oDataRow);
                                        }
                                    }

                                }
                                reader.Close();
                                reader.Dispose();

                            }
                        }
                        //#region duplicate data
                        //int count = (oDataTable.Rows.Count - 1);
                        ////for (int i = count; i >= 1; i--)
                        //for (int i = count; i >= 1; i--)
                        //{
                        //    if (oDataTable.Rows[i][4].ConvertToString() != "")
                        //    {
                        //        if (objService.CheckDuplicate(oDataTable.Rows[i][4].ToString()))
                        //        {

                        //            string row = oDataTable.Rows[i][0].ToString();
                        //            Session["rowposition"] = row;
                        //            //res = true;
                        //            //oDataTable.Rows.RemoveAt(i);
                        //            ViewData["FinalMessage"] = "PADuplicate";
                        //            Session["ImportMessage"] = ViewData["FinalMessage"];
                        //            return RedirectToAction("SaveALLCSVFile");
                        //        }

                        //    }
                        //    // else
                        //    //  {
                        //    //    if (oDataTable.Rows[i][5] == DBNull.Value)
                        //    //oDataTable.Rows[i].Delete();
                        //    //}
                        //    //oDataTable.AcceptChanges();
                        //}
                        //#endregion


                        #region pa-check
                        int count = (oDataTable.Rows.Count - 1);
                    
                        for (int i = count; i >= 0; i--)
                        {
                        loopstart:
                            if (oDataTable.Rows[i][0].ToDecimal() == null)
                            {
                                oDataTable.Rows.RemoveAt(i);
                                i--;
                                goto loopstart;
                            }
                            if (oDataTable.Rows[i][4].ConvertToString() != "")
                            {
                                string pa = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + oDataTable.Rows[i][4].ConvertToString();
                               

                                #region compare pa
                                string PA_NO_final = "";
                                string value = "";
                                string PA_NO_1 = "";
                                string PA_NO_2 = "";
                                PA_NO_1 = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + oDataTable.Rows[i][4].ToDecimal().ConvertToString();
                                if (oDataTable.Rows[i][4].ConvertToString().Trim().Length == 2)
                                {
                                    PA_NO_2 = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + "0" + oDataTable.Rows[i][4].ToDecimal().ConvertToString();
                                }
                                if (oDataTable.Rows[i][4].ConvertToString().Trim().Length == 1)
                                {
                                    PA_NO_2 = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + "00" + oDataTable.Rows[i][4].ToDecimal().ConvertToString();
                                }

                                DataTable DtNraCode_1 = new DataTable();
                                DataTable DtNraCode_2 = new DataTable();

                                string cmdText_1 = "select NRA_DEFINED_CD FROM VW_BENEF_ALL WHERE NRA_DEFINED_CD ='" + PA_NO_1.ConvertToString() + "'";
                                DtNraCode_1 = objService.getTable(cmdText_1);
                                string cmdText_2 = "select NRA_DEFINED_CD FROM VW_BENEF_ALL WHERE NRA_DEFINED_CD ='" + PA_NO_2.ConvertToString() + "'";
                                DtNraCode_2 = objService.getTable(cmdText_2);


                                if (DtNraCode_1 != null && DtNraCode_1.Rows.Count > 0)
                                {
                                    PA_NO_final = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + oDataTable.Rows[i][4].ToDecimal();
                                    value = oDataTable.Rows[i][4].ToDecimal().ConvertToString();
                                    pa = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + value.ConvertToString();
                                }
                                if (DtNraCode_2 != null && DtNraCode_2.Rows.Count > 0)
                                {
                                    PA_NO_final = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + oDataTable.Rows[i][4].ConvertToString();
                                    if (oDataTable.Rows[i][4].ConvertToString().Trim().Length == 2)
                                    {
                                        value = "0" + oDataTable.Rows[i][4].ConvertToString();
                                    }
                                    if (oDataTable.Rows[i][4].ConvertToString().Trim().Length == 1)
                                    {
                                        value = "00" + oDataTable.Rows[i][4].ConvertToString();
                                    }                                    
                                   
                                    pa = oDataTable.Rows[i][0].ConvertToString() + '-' + oDataTable.Rows[i][1].ConvertToString() + '-' + oDataTable.Rows[i][2].ConvertToString() + '-' + oDataTable.Rows[i][3].ConvertToString() + '-' + value.ConvertToString();
                                }


                                oDataTable.Rows[i][4] = value;
                                #endregion
                                
                                #region check if PA Exists in MST

                                if (!objService.CheckIfPAExists(pa))
                                        {
                                            
                                            //string row = (i+1).ConvertToString();
                                            //Session["rowposition"] = row;
                                            //ViewData["FinalMessage"] = "PADoNotExists";
                                            //Session["ImportMessage"] = ViewData["FinalMessage"];
                                            //return RedirectToAction("SaveALLCSVFile");



                                            string reason = "Pa doesnot exist";
                                            bool saveDuplicate = objService.saveDuplicateEnrollmentPAData(pa,
                                                       oDataTable.Rows[i][14].ConvertToString(),
                                                       oDataTable.Rows[i][0].ConvertToString(),
                                                       oDataTable.Rows[i][1].ConvertToString(),
                                                       oDataTable.Rows[i][2].ConvertToString(),
                                                       oDataTable.Rows[i][3].ConvertToString(),
                                                       oDataTable.Rows[i][4].ConvertToString(), oDataTable.Rows[i][8].ConvertToString(), file.FileName,reason);

                                            string row = i.ToString();
                                            Session["rowposition"] = row;
                                     
                                            ViewData["FinalMessage"] = "PADuplicate";
                                            Session["ImportMessage"] = ViewData["FinalMessage"];
                                            Session["fileNamePA"] = file.FileName;
                                            oDataTable.Rows.RemoveAt(i);

                                        }
                                    
                                 
                                #endregion
                                
                                
                                if (objService.CheckCSVPADuplicate(pa))
                                {

                                   // string row = oDataTable.Rows[i][4].ToString();

                                    string reason = "Duplicate PA";
                                    bool saveDuplicate = objService.saveDuplicateEnrollmentPAData(pa,
                                               oDataTable.Rows[i][14].ConvertToString(),
                                               oDataTable.Rows[i][0].ConvertToString(),
                                               oDataTable.Rows[i][1].ConvertToString(),
                                               oDataTable.Rows[i][2].ConvertToString(),
                                               oDataTable.Rows[i][3].ConvertToString(),
                                               oDataTable.Rows[i][4].ConvertToString(), oDataTable.Rows[i][8].ConvertToString(),file.FileName, reason);

                                    string row = i.ToString();
                                    Session["rowposition"] = row;

                                    //res = true;
                                    //oDataTable.Rows.RemoveAt(i);
                                    ViewData["FinalMessage"] = "PADuplicate";
                                    Session["ImportMessage"] = ViewData["FinalMessage"];
                                    Session["fileNamePA"] = file.FileName;
                               
                                    //string row = oDataTable.Rows[i][0].ToString();
                                    //Session["rowposition"] = row;
                                    //res = true;
                                    oDataTable.Rows.RemoveAt(i);


                                    //return RedirectToAction("SaveALLCSVFile");
                                }
                               
                            }
                              
                        }
                        #endregion

                        string result = "";
                        ViewData["surveyCnt"] = surveyCount;
                        if(oDataTable.Rows.Count>0)
                        {
                            
                            result = objService.SaveData(oDataTable, file.FileName, out exc);
                            errormessage = exc.ConvertToString();
                            if(result=="True")
                            {
                                rs = true;
                            }
                            else
                            {
                                rs = false;
                            }
                        }
                        else
                        {
                             if(ViewData["FinalMessage"].ConvertToString() == "PADuplicate")
                             {
                                Session["ImportMessage"] = "All duplicate";
                                return RedirectToAction("SaveALLCSVFile");
                             }
                        }
                        if (rs)
                        {
                            if (Session["ImportMessage"].ConvertToString() == "PADuplicate")
                            {
                                ViewData["FinalMessage"] = Session["ImportMessage"];
                                Session["ImportMessage"] = null;
                            }
                            else
                            {
                                ViewData["FinalMessage"] = "Success";
                            }
                        }
                        else
                        {
                            //if (Session["ImportMessage"].ConvertToString() == "PADuplicate")
                            //{
                            //    ViewData["FinalMessage"] = Session["ImportMessage"];
                            //    Session["ImportMessage"] = null;
                            //}
                            //else
                            //{
                            ViewData["FinalMessage"] = errormessage;
                            Session["exception"]= result;
                            Session["ImportMessage"] = "Failed";
                                return RedirectToAction("SaveALLCSVFile");
                            //}
                        }
                        //ViewData["FinalMessage"] = "Data imported successfully.";


                    }
                    else
                    {
                        ViewData["FinalMessage"] = "Duplicate File";
                    }




                }
                catch (Exception)
                {
                    ViewData["FinalMessage"] = "Failed.";
                    throw;
                }
                //}

            }
            Session["ImportMessage"] = ViewData["FinalMessage"];
            return RedirectToAction("SaveALLCSVFile");
            //        return RedirectToAction("OpenImport", new RouteValueDictionary(
            //new { controller = "Enrollment", action = "OpenImport", Import = "Success" }));

        }

        public bool SaveCSVFile(string paramFilename, string district, string vdc)
        {
            int surveyCount = 0;
            string exc = string.Empty;
            dynamic AllFiles = null;
            EnrollmentFileImportService objService = new EnrollmentFileImportService();
            string filepath = "";
            string fileName = "";
            if (!string.IsNullOrEmpty(vdc))
            {
                filepath = Server.MapPath("~/Files/CSV_Enrollment/" + district + "/" + vdc + "/" + paramFilename + "/");
                fileName = district + "_" + vdc + "_" + paramFilename;
            }
            else
            {
                filepath = Server.MapPath("~/Files/CSV_Enrollment/" + district + "/" + paramFilename + "/");
                fileName = district + "_" + paramFilename;
            }

            //string filepath = Server.MapPath("~/Files/CSV_Enrollment/" + paramFilename + "/");
            AllFiles = Directory.GetFiles(filepath, "*", System.IO.SearchOption.AllDirectories).ToList();
            //if (filepath.Split('.').LastOrDefault() == string.Empty)
            //    AllFiles = Directory.GetFiles(filepath, "*", System.IO.SearchOption.AllDirectories).ToList();
            //else
            //    AllFiles = Directory.GetFiles(filepath).ToList();



            bool rs = false;


            try
            {
                foreach (var file in AllFiles)
                {
                    string finalFileName = "";
                    if (string.IsNullOrEmpty(vdc))
                    {
                        finalFileName = fileName;
                        string[] spl = file.Split('\\');
                        finalFileName = finalFileName + "_" + spl[spl.Length - 2];
                    }
                    else
                    {
                        finalFileName = fileName;
                    }
                    List<string> fileListInDB = new List<string>();
                    fileListInDB = objService.JSONFileListInDBPA();
                    if (!fileListInDB.Contains(finalFileName))
                    {
                        int lnNum = 0;
                        DataTable oDataTable = new DataTable();
                        string extension = Path.GetExtension(file);
                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {
                            oDataTable = Import_To_Grid(file, ".xlsx", "Yes");//, "");
                        }
                        else if (extension.ToLower() == ".csv")
                        {
                            using (StreamReader reader = System.IO.File.OpenText(file))
                            {
                                var csv = new CsvReader(reader);
                                string[] columnsNames = null;
                                string[] ostreamDataValues = null;
                                while (!reader.EndOfStream)
                                {
                                    var oStreamData = reader.ReadLine().Trim();
                                    if (oStreamData.Length > 0)
                                    {
                                        ostreamDataValues = oStreamData.Split(',');
                                        if (lnNum == 0)
                                        {
                                            lnNum++;
                                            columnsNames = ostreamDataValues;
                                            foreach (string csbHeader in columnsNames)
                                            {
                                                DataColumn dtCol = new DataColumn(csbHeader.ToUpper(), typeof(string));
                                                dtCol.DefaultValue = string.Empty;
                                                oDataTable.Columns.Add(dtCol);
                                                
                                            }
                                        }
                                        else
                                        {
                                            DataRow oDataRow = oDataTable.NewRow();
                                            for (int i = 0; i < columnsNames.Length; i++)
                                            {
                                                var colname = columnsNames[i];
                                                var oStreamDataValue = ostreamDataValues;
                                                oDataRow[colname] = string.IsNullOrEmpty(oStreamDataValue[i]) ? string.Empty : oStreamDataValue[i].ToString().TrimStart('0').Length > 0 ? oStreamDataValue[i].TrimStart('0') : "0";
                                            }
                                            oDataTable.Rows.Add(oDataRow);
                                        }
                                    }

                                }
                                reader.Close();
                                reader.Dispose();

                            }
                        }
                        string result="";
                        ViewData["surveyCnt"] = surveyCount;
                        //string batchID = objService.GetBatchIDFromFileName(finalFileName);
                       // rs = objService.SaveData(oDataTable, finalFileName, out exc);
                        result = objService.SaveData(oDataTable, file.FileName, out exc);
                        if (result == "True")
                        {
                            rs = true;
                        }
                        else
                        {
                            rs = false;
                        }
                        ViewData["FinalMessage"] = "Data imported successfully.";
                    }

                }

            }
            catch (Exception)
            {
                ViewData["FinalMessage"] = "Data import failed.";
                throw;
            }

            return rs;
        }

        //public ActionResult GetEnrollmentFileImport(string SearchValueDistrict, string SearchValueVdc)
        public ActionResult GetEnrollmentFileImport(string SearchValueDistrict, string SearchValueVdc)
        {
            CommonFunction commonFC = new CommonFunction();
            EnrollmentFileImportService objReportService = new EnrollmentFileImportService();
            string id = string.Empty;
            string vdcCD = commonFC.GetCodeFromDataBase(SearchValueVdc.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            Session["distcode"] = SearchValueDistrict;
            Session["vdccode"] = vdcCD;
            DataTable dtresult = objReportService.GetDataImportRecordByDistrict(SearchValueDistrict, vdcCD);
            ViewData["dtEnrollmentFileImportRslt"] = dtresult;

            return PartialView("_GetEnrollmentFileImport");
        }
        public ActionResult GetPAImportList()
        {
            CommonFunction commonFC = new CommonFunction();
            EnrollmentFileImportService objReportService = new EnrollmentFileImportService();
            DataTable dtresult = objReportService.GetDataImportRecordPAByDistrict();
            ViewData["dtEnrollmentFileImportRslt"] = dtresult;

            return View();
        }
        #region DeleteEnrollDetailForShowingZeroRecord

        //public ActionResult DeleteEnrollDetail(string p)
        //{
        //     string id = "";
        //     bool rs = false;
        //     EnrollmentFileImportService objService = new EnrollmentFileImportService();
        //    RouteValueDictionary rvd = new RouteValueDictionary();
        //    try
        //    {
        //        if (p != null)
        //        {
        //            rvd = QueryStringEncrypt.DecryptString(p);
        //            if (rvd != null)
        //            {
        //                if (rvd["id"] != null)
        //                {
        //                    id = rvd["id"].ToString();
        //                 rs=   objService.deleteEnrollmentDetail(id);

        //                }
        //            }
        //        }
        //    }
        //    catch (OracleException oe)
        //    {
        //        ExceptionManager.AppendLog(oe);
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.AppendLog(ex);
        //    }

        //    return RedirectToAction("BankList");
        //}

        #endregion
        [HttpPost]
        public ActionResult OpenImport(FormCollection fc, HttpPostedFileBase file)
        //public ActionResult OpenImport(string paramFilename, string District, string VDC)        
        {

            string exc = string.Empty;
            EnrollmentFileImportService objService = new EnrollmentFileImportService();
            string DistrictCode = fc["ddl_District"].ConvertToString();
            string VDCDefinedCode = fc["ddl_VDCMun"].ConvertToString();
            string VDCCode = common.GetCodeFromDataBase(VDCDefinedCode, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            bool rs = false;
            string NRAward = "";
            if (file != null && file.ContentLength > 0)
            {
                string FilePath = "~/Files/CSV_Enrollment/BrowseImport/";

                if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                {
                    System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_").Split('\\')[0]);
                }

                file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName).Replace(" ", "_").Split('\\')[0]);
                string finalPath = Server.MapPath(FilePath + Path.GetFileName(file.FileName).Replace(" ", "_").Split('\\')[0]);


                try
                {
                    List<string> fileListInDB = new List<string>();
                    fileListInDB = objService.JSONFileListInDB();
                    if (!fileListInDB.Contains(file.FileName.Replace(" ", "_")))
                    {

                        DataTable oDataTable = new DataTable();
                        //string extension = Path.GetExtension(finalPath);
                        string extension = Path.GetExtension(finalPath);

                        if (extension.ToLower() == ".xls" || extension.ToLower() == ".xlsx")
                        {

                            oDataTable = Import_To_Grid(finalPath, ".xlsx", "Yes");//, " ");
                            #region add column in datatable
                            //if (oDataTable.Rows.Count > 0)
                            //{
                            //    oDataTable.Columns.Add("OriginalNRA", typeof(string));
                            //    string originalPA = "";
                            //    int i = 1;
                            //    // for (int i = 1; i < oDataTable.Rows.Count; i++)
                            //    //{                                    
                            //    foreach (DataRow dr in oDataTable.Rows)
                            //    {

                            //        NRAward = oDataTable.Rows[i][5].ToString();
                            //        if (oDataTable.Rows[i][5].ToString() != "")
                            //        {
                            //            //NRAward = oDataTable.Rows[i][5].ToString().TrimEnd('-').Split('-')[2];
                            //            //NRAward = oDataTable.Rows[i][5].ToString();
                            //          //DataRow  currRow = oDataTable.Rows[oDataTable.CurrentCell.RowNumber];                                      

                            //            string[] splits = NRAward.Split('-');
                            //            int cnt = splits.Count();
                            //            if (cnt == 1)
                            //            {
                            //                splits = NRAward.Split('‐');
                            //            }
                            //            string discd = splits[splits.Length - 5].ToString();
                            //            string vdccd = splits[splits.Length - 4].ToString();
                            //            string nrawardno = splits[splits.Length - 3].ToString();
                            //            string EA = splits[splits.Length - 2].ToString();
                            //            string serialno = splits[splits.Length - 1].ToString();
                            //            originalPA = discd + "-" + vdccd + "-" + nrawardno + "-" + EA + "-" + serialno;
                            //            dr["OriginalNRA"] = originalPA;
                            //            //dr["OriginalNRA",DataRowVersion.Current] = originalPA;
                            //        }

                            //        i++;
                            //    }

                            //    //}
                            //}
                            #endregion

                            #region duplicate data
                            int count = (oDataTable.Rows.Count - 1);
                            //for (int i = count; i >= 1; i--)
                            for (int i = count; i >= 1; i--)
                            {
                                if (oDataTable.Rows[i][5].ConvertToString() != "")
                                {
                                    if (objService.CheckDuplicate(oDataTable.Rows[i][5].ToString()))
                                    {
                                        bool saveDuplicate = objService.saveDuplicateEnrollmentData(oDataTable.Rows[i][5].ConvertToString(),
                                                oDataTable.Rows[i][1].ConvertToString(),
                                                oDataTable.Rows[i][2].ConvertToString(),
                                                oDataTable.Rows[i][3].ConvertToString(),
                                                oDataTable.Rows[i][4].ConvertToString(),
                                                oDataTable.Rows[i][10].ConvertToString(), file.FileName);
                                        ViewData["FinalMessage"] = "DuplicateInDatabse";
                                        Session["ImportMessage"] = ViewData["FinalMessage"];
                                        Session["fileName"] = file.FileName;
                                        //string row = oDataTable.Rows[i][0].ToString();
                                        //Session["rowposition"] = row;
                                        //res = true;
                                        oDataTable.Rows.RemoveAt(i);
                                        //ViewData["FinalMessage"] = "PADuplicate";
                                        //Session["ImportMessage"] = ViewData["FinalMessage"];
                                        //return RedirectToAction("OpenImport");
                                    }

                                }
                                else
                                {
                                    if (oDataTable.Rows[i][5] == DBNull.Value)
                                        oDataTable.Rows[i].Delete();
                                }
                                oDataTable.AcceptChanges();
                            }
                            #endregion
                            #region check if PA Exists in MST
                            for (int i = 1; i < oDataTable.Rows.Count; i++)
                            {
                                if (oDataTable.Rows[i][5].ToString() != "")
                                {
                                    if (!objService.CheckIfPAExists(oDataTable.Rows[i][5].ToString()))
                                    {
                                        //string PA_No = oDataTable.Rows[i][5].ToString();
                                        //Session["PA_No"] = PA_No;
                                        string row = oDataTable.Rows[i][0].ToString();
                                        Session["rowposition"] = row;
                                        ViewData["FinalMessage"] = "PADoNotExists";
                                        Session["ImportMessage"] = ViewData["FinalMessage"];
                                        return RedirectToAction("OpenImport");
                                    }
                                }
                            }
                            #endregion

                            #region duplicate data display
                            //for (int r0 = 0; r0 < oDataTable.Rows.Count; r0++)
                            //    {
                            //        for (int r1 = r0 + 1; r1 < oDataTable.Rows.Count; r1++)
                            //          {
                            //             Boolean rowsEqual = true;

                            //                //for (int c = 7; c < oDataTable.Columns.Count; c++)
                            //                    //{
                            //                     if (!Object.Equals(oDataTable.Rows[r0][7], oDataTable.Rows[r1][7]))
                            //                        {
                            //                         rowsEqual = false;
                            //                            break;
                            //                        }
                            //                     //}

                            //              if (rowsEqual)
                            //                 {
                            //                     Console.WriteLine(String.Format("Row {0} is a duplicate of row {1}.", r0, r1));

                            //                }
                            //            }
                            //        }
                            ////for (int i = 1; i < oDataTable.Rows.Count; i++)
                            ////{
                            ////    if (CheckDuplicate(oDataTable.Rows[i][7].ToString()))
                            ////    {

                            ////        string cmdText = String.Format("delete from NHRS_ENROLLMENT_MOU where NRA_DEFINED_CODE='" + oDataTable.Rows[i][7].ToString() + "'");
                            ////    }

                            ////}
                            #endregion
                            string nraward = "";
                            #region split NRA DEFINE CODE
                            for (int i = 1; i < oDataTable.Rows.Count; i++)
                            {

                                if (oDataTable.Rows[i][5].ToString() != "")
                                {
                                    //NRAward = oDataTable.Rows[i][5].ToString().TrimEnd('-').Split('-')[2];
                                    NRAward = oDataTable.Rows[i][5].ToString();
                                    string[] splits = NRAward.Split('-');
                                    int cnt = splits.Count();
                                    if (cnt < 5)
                                    {
                                        //splits = NRAward.Split('‐');
                                        string[] nrasplit = NRAward.Split('‐');
                                        int nracount = nrasplit.Count();
                                        if (nracount < 5)
                                        {
                                            string row = oDataTable.Rows[i][0].ToString();
                                            Session["rowposition"] = row;
                                            ViewData["FinalMessage"] = "WrongFormat";
                                            Session["ImportMessage"] = ViewData["FinalMessage"];
                                            return RedirectToAction("OpenImport");
                                        }
                                        string splitward = nrasplit[nrasplit.Length - 3].ToString();
                                        if (oDataTable.Rows[i][4].ToString() != splitward.ToString())
                                        {

                                            string row = oDataTable.Rows[i][0].ToString();
                                            Session["rowposition"] = row;
                                            ViewData["FinalMessage"] = "Mismatch";
                                            Session["ImportMessage"] = ViewData["FinalMessage"];
                                            return RedirectToAction("OpenImport");
                                        }
                                    }

                                    if (cnt == 5)
                                    {
                                        nraward = splits[splits.Length - 3].ToString();
                                        if (oDataTable.Rows[i][4].ToString() != nraward.ToString())
                                        {

                                            string row = oDataTable.Rows[i][0].ToString();
                                            Session["rowposition"] = row;
                                            ViewData["FinalMessage"] = "Mismatch";
                                            Session["ImportMessage"] = ViewData["FinalMessage"];
                                            return RedirectToAction("OpenImport");
                                        }
                                    }

                                }


                            }
                            #endregion
                        }
                        #region csv
                        //else if (extension.ToLower() == ".csv")
                        //{
                        //    using (StreamReader reader = System.IO.File.OpenText(finalPath))
                        //    {
                        //        var csv = new CsvReader(reader);
                        //        string[] columnsNames = null;
                        //        string[] ostreamDataValues = null;
                        //        while (!reader.EndOfStream)
                        //        {
                        //            var oStreamData = reader.ReadLine().Trim();
                        //            if (oStreamData.Length > 0)
                        //            {
                        //                ostreamDataValues = oStreamData.Split(',');
                        //                if (lnNum == 0)
                        //                {
                        //                    lnNum++;
                        //                    columnsNames = ostreamDataValues;
                        //                    foreach (string csbHeader in columnsNames)
                        //                    {
                        //                        DataColumn dtCol = new DataColumn(csbHeader.ToUpper(), typeof(string));
                        //                        dtCol.DefaultValue = string.Empty;
                        //                        oDataTable.Columns.Add(dtCol);

                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    DataRow oDataRow = oDataTable.NewRow();
                        //                    for (int i = 0; i < columnsNames.Length; i++)
                        //                    {
                        //                        var colname = columnsNames[i];
                        //                        var oStreamDataValue = ostreamDataValues;

                        //                        oDataRow[colname] = string.IsNullOrEmpty(oStreamDataValue[i]) ? string.Empty : oStreamDataValue[i].ToString().TrimStart('0').Length > 0 ? oStreamDataValue[i].TrimStart('0') : "0";
                        //                    }
                        //                    oDataTable.Rows.Add(oDataRow);
                        //                }
                        //            }

                        //        }
                        //        reader.Close();
                        //        reader.Dispose();

                        //    }
                        //}
                        #endregion

                        //ViewData["surveyCnt"] = surveyCount;
                        if (oDataTable.Rows.Count > 1)
                        {

                            rs = objService.SaveDataFromFileBrowse(oDataTable, DistrictCode, VDCCode, file.FileName.Replace(" ", "_").Split('\\').Last(), out exc);
                        }

                        if (rs)
                        {
                            if (Session["ImportMessage"].ConvertToString() == "DuplicateInDatabse")
                            {
                                ViewData["FinalMessage"] = Session["ImportMessage"];
                                Session["ImportMessage"] = null;
                            }
                            else
                            {
                                ViewData["FinalMessage"] = "Success";
                            }

                        }
                        else
                        {
                            if (Session["ImportMessage"].ConvertToString() == "DuplicateInDatabse")
                            {
                                ViewData["FinalMessage"] = Session["ImportMessage"];
                                Session["ImportMessage"] = null;
                            }
                            else
                            {
                                ViewData["FinalMessage"] = "Failed";

                            }

                        }
                    }
                    else
                    {
                        ViewData["FinalMessage"] = "Duplicate File";
                    }

                }
                catch (Exception ex)
                {
                    exc += ex.Message.ToString(); ;
                    ExceptionManager.AppendLog(ex);
                    ViewData["FinalMessage"] = "Failed";
                    Session["ImportMessage"] = ViewData["FinalMessage"];
                    return RedirectToAction("OpenImport");
                    //throw;
                }
            }
            Session["ImportMessage"] = ViewData["FinalMessage"];
            return RedirectToAction("OpenImport");
        }



        //get duplicate data

          
         
        public ActionResult GetDuplicateEnrollmentPAImport(string FileName)
        {
            EnrollmentFileImportService objService = new EnrollmentFileImportService();
            DataTable dt = new DataTable();

            HtmlReport rptParams = new HtmlReport();
           
            string filename = Session["fileNamePA"].ConvertToString();

            rptParams.FileName = filename;
            Session["fileNamePA"] = null;

            try
            {
                dt = objService.GetDuplicateEnrollemntPAImport(FileName);
                ViewData["Duplicate"] = dt;



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_DuplicateinEnrollmentPAFile",rptParams);
        }

        public ActionResult ExportEnrolledPADuplicateReport(string dist, string vdc, string ward)
        {

            //GetEnrolledDetailReport(dist, vdc, ward);
            HtmlReport rptParams = new HtmlReport();
            ViewData["ExportFont"] = "font-size: 13px";
            string usercd = SessionCheck.getSessionUserCode();
            string filePath = string.Empty;

            if (usercd != "")
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            else
            {
                filePath = Server.MapPath("/files/Excel/") + "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls";
            }
            //string html = RenderPartialViewToString("~/Views/HTMLReport/_EnrolledDetailReportPAFull.cshtml");
            //html = string.Format("{0} {1}", ReportTemplate.ReportHeader("Household Detail Report"), html);
            //html = ReportTemplate.GetReportHTML(html);
           // Utils.CreateFile(html, filePath);
            return File(filePath, "application/msexcel", "Enrolled_Summary_REPORT" + "( District " + dist + " ).xls");
        }




        public ActionResult GetEnrollmentRep()
        {
            ViewData["lstUser"] = common.GetEnrollmentPAUser("");
           
            return View();
        }


        //public ActionResult GetEnrollmentDetailRep()
        //{

        //    return View();
        //}







        public ActionResult GetDuplicateImport()
        {
            EnrollmentFileImportService objService = new EnrollmentFileImportService();
            DataTable dt = new DataTable();
            string filename = Session["fileName"].ConvertToString();
            Session["fileName"] = null;

            try
            {
                dt = objService.GetDuplicateImport(filename);
                ViewData["Duplicate"] = dt;



            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_DuplicateInFile");
        }
        //public ActionResult FilterEnrollment()
        //{
        //    //DataTable filterdata= new DataTable();
        //    var oDataTable = TempData["FilterImport"];
        //    ViewData["FilterImport"] = oDataTable;
        //    //return PartialView("_FilterEnrollment");
        //    return PartialView();
        //}
        public DataTable Import_To_Grid(string FilePath, string Extension, string isHDR)//, string sheetName)
        {
            string conStr = "";
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
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            string sheet = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();

            //DataTable dtSheet = new DataTable();
            //dtSheet = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //sheetName = dtSheet.Rows[0]["TABLE_NAME"].ConvertToString();

            //cmdExcel.CommandText = "SELECT * From [" + sheetName + "$]";
            //string query = String.Format("select * from [{0}${1}]", "Sheet1", "A2:ZZ");
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            //cmdExcel.CommandText = String.Format("SELECT * From [{0}${1}]", "SheetName", "A2:ZZ");
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            ViewData["result"] = dt;

            connExcel.Close();
            return dt;
        }


        #endregion
        public ActionResult SelectType()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MANUAL", Value = "0" });

            items.Add(new SelectListItem {  Text = "UPLOAD", Value = "1" } );

             

            ViewBag.Type = items;

            return View();

        }
        #region Excel File Format Download

        //private void DownloadFileFTP()
        //{
        //    //string inputfilepath = @"C:\Temp\FileName.exe";
        //    //string ftphost = "xxx.xx.x.xxx";
        //    string ftpfilepath = "~/Excel/Enrollment.xlsx";

        //    string ftpfullpath = "ftp://"  + ftpfilepath;

        //    using (WebClient request = new WebClient())
        //    {
        //        //request.Credentials = new NetworkCredential("UserName", "P@55w0rd");
        //        byte[] fileData = request.DownloadData(ftpfullpath);

        //        //using (FileStream file = File.Create(ftpfullpath))
        //        //{
        //        //    file.Write(fileData, 0, fileData.Length);
        //        //    file.Close();
        //        //}
        //        //MessageBox.Show("Download Complete");
        //        ViewData["Message"] = "Download Comlplete";
        //    }
        //}


        #endregion

        #region Letter Format
        public void getPrintStatus(string p)
        {
            PasLetterFormat objletter = new PasLetterFormat();
            DataTable dt = new DataTable();
            string id = string.Empty;
            string id2 = string.Empty;
            string id3 = string.Empty;
            string id4 = string.Empty;
            string id5 = string.Empty;
            EnrollmentService objEnrollServices = new EnrollmentService();
            EnrollmentSearch objenroll = new EnrollmentSearch();
            RouteValueDictionary rvd = new RouteValueDictionary();
            string strHTML = String.Empty;
            string htmlfilepath = "";
            string pdffilepath = "";
            string PA = string.Empty;
            if (p != null && p.Count() > 0)
            {
                dynamic lstParam = p.Split(',');

                foreach (string item in lstParam)
                {
                    rvd = QueryStringEncrypt.DecryptString(item);
                    if (rvd != null)
                    {
                        if (rvd["HouseOwnerID"] != null)
                        {
                            id = (rvd["HouseOwnerID"]).ConvertToString();
                        }
                        if (rvd["targetbatchid"] != null)
                        {
                            id2 = (rvd["targetbatchid"]).ConvertToString();
                        }
                        if (rvd["targetingid"] != null)
                        {
                            id3 = (rvd["targetingid"]).ConvertToString();
                        }
                        if (rvd["Houseid"] != null)
                        {
                            id4 = (rvd["Houseid"]).ConvertToString();
                        }
                        if (rvd["structureNo"] != null)
                        {
                            id5 = (rvd["structureNo"]).ConvertToString();
                        }
                        if (rvd["Pa"] != null)
                        {
                            PA = (rvd["Pa"]).ConvertToString();
                        }
                    }
                    if (id.ConvertToString() != "")
                    {
                        objenroll = objEnrollServices.GenerateCertificateData(id,PA);
                        objenroll.docData = objEnrollServices.getPrintStatus("MN");

                        BeneficiaryService objservice = new BeneficiaryService();
                        DataTable dtt = new DataTable();
                        dtt = objservice.GetOwner(objenroll.House_Owner_ID);
                        if (dtt != null)
                        {
                            if (dtt.Rows.Count > 0)
                            {
                                objenroll.House_Owner_Name = dtt.Rows[0]["FULL_NAME_ENG"].ConvertToString();
                                objenroll.House_Owner_Name_Loc = dtt.Rows[0]["FULL_NAME_LOC"].ConvertToString();
                            }
                        }
                    }
                    objletter.docData = Server.HtmlDecode(objenroll.docData);
                    String template = objletter.docData;
                    LatterFormatTemplate report = new LatterFormatTemplate(template);

                    //Newly added parameters for report
                    report.AddAttribute("District", objenroll.DISTRICT_LOC.ConvertToString());
                    report.AddAttribute("VDC", objenroll.VDC_LOC.ConvertToString());
                    report.AddAttribute("Ward", objenroll.WardNo.ConvertToString());
                    report.AddAttribute("Area", objenroll.AREA_LOC.ConvertToString());
                    report.AddAttribute("BeneficiaryNameEng", objenroll.House_Owner_Name.ConvertToString());
                    report.AddAttribute("BeneficiaryNameLoc", objenroll.House_Owner_Name_Loc.ConvertToString());

                    report.AddAttribute("HouseID", objenroll.House_Owner_ID.ConvertToString());
                    report.AddAttribute("HouseholdID", objenroll.HOUSEHOLD_ID.ConvertToString());
                    report.AddAttribute("BeneficiaryID", objenroll.BENEFICIARY_ID.ConvertToString());

                    //RESTORE THE VALUE OF SESSIONLANGUAGE TO NEPALI
                    string tempLanguage = Session["LanguageSetting"].ToString();
                    Session["LanguageSetting"] = "Nepali";
                    //RESTORE THE VALUE OF SESSIONLANGUAGE
                    Session["LanguageSetting"] = tempLanguage;
                    objletter.docData = report.ConvertToString();
                    if (objletter.docData.ConvertToString() != "")
                    {
                        objEnrollServices.UpdatePrintStatus("Y", id);
                    }
                    strHTML = strHTML + objletter.docData;
                }

                htmlfilepath = Server.MapPath("/Files/html/Enrollment.html");
                pdffilepath = Server.MapPath("/Files/pdf/Enrollment.pdf");

                Utils.CreateFile(strHTML, htmlfilepath);
                PdfGenerator.ConvertToPdf(htmlfilepath, pdffilepath);

                System.IO.FileInfo file = new System.IO.FileInfo(pdffilepath);
                if ((file.Exists))
                {
                    Response.ContentType = "Application/pdf";
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + file.Name);

                    Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    System.IO.Path.GetFileName(file.Name)));

                    Response.TransmitFile(pdffilepath);
                    Response.Flush();
                    Response.End();

                }
            }

        }

        #endregion
        #region Duplicate check
        //public bool CheckDuplicate(string NRADefinedCode)
        //{
        //    DataTable dt = new DataTable();
        //    string cmdText = "";
        //    //string lstStr = "";
        //    bool res = false;
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        service.Begin();
        //        cmdText = "select * from NHRS_ENROLLMENT_MOU WHERE NRA_DEFINED_CD='" + NRADefinedCode + "'";
        //        try
        //        {
        //            dt = service.GetDataTable(cmdText, null);

        //        }
        //        catch (Exception ex)
        //        {
        //            dt = null;
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null)
        //            {
        //                service.End();
        //            }
        //        }
        //    }
        //    if (dt.Rows.Count > 0)
        //    {
        //        res = true;

        //    }

        //    return res;
        //}
        #endregion



        #region Export To Excel
        public ActionResult ExportExcel(string searchType)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["dtEnrollment"];

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

            requiredCols.Add("TARGET_BATCH_ID");
            requiredCols.Add("INSTANCE_UNIQUE_SNO");
            requiredCols.Add("HOUSE_OWNER_NAME_ENG");
            requiredCols.Add("ADDRESS");
            requiredCols.Add("MOU SIGNED STATUS");

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

            dtModified.Columns["TARGET_BATCH_ID"].SetOrdinal(0);
            dtModified.Columns["INSTANCE_UNIQUE_SNO"].SetOrdinal(1);
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].SetOrdinal(2);
            dtModified.Columns["ADDRESS"].SetOrdinal(3);
            dtModified.Columns["MOU SIGNED STATUS"].SetOrdinal(4);
            dtModified.Columns["TARGET_BATCH_ID"].ColumnName = Utils.GetLabel("Batch ID");
            dtModified.Columns["INSTANCE_UNIQUE_SNO"].ColumnName = Utils.GetLabel("House ID");
            dtModified.Columns["HOUSE_OWNER_NAME_ENG"].ColumnName = Utils.GetLabel("House Owner Name");
            dtModified.Columns["ADDRESS"].ColumnName = Utils.GetLabel("Address");
            dtModified.Columns["MOU SIGNED STATUS"].ColumnName = Utils.GetLabel("MOU Signed Status");

            return dtModified;
        }

        protected void ExcelExport(DataTable dtRecords)
        {
            string XlsPath = Server.MapPath(@"~/Excel/Enrollment.xls");
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
                Response.Write(ex.Message);
            }
        }
        #endregion


    }
}