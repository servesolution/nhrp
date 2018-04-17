using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using System.Data;
using System.Web;
using MIS.Models.CaseGrievance;
using MIS.Services.CaseGrievance;
using System.Data.SqlClient;
using MIS.Services.Core;
using ExceptionHandler;
using System.Web.Mvc;

namespace MIS.Services.CaseGrievance
{
    public class CaseGrievanceService
    {
        CommonFunction objCommonFunction = new CommonFunction();
     
        #region Fill For Case Grievance
        public CaseGrievanceModel CaseGrievance_Get(string RegistrationID)
        {
            CaseGrievanceModel obj = new CaseGrievanceModel();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    cmdText = String.Format("Select * from NHRS_GRIEVANCE_REGISTRATION GR where CASE_REGISTRATION_ID='"+RegistrationID+"'");
                    try
                    {
                        dtbl = service.GetDataTable(cmdText, null);
                    }
                    catch (Exception ex)
                    { ExceptionManager.AppendLog(ex); }
                    finally
                    {
                        if (service.Transaction != null && service.Transaction.Connection != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    obj.CASE_REGISTRATION_ID = dtbl.Rows[0]["CASE_REGISTRATION_ID"].ConvertToString();
                    obj.DEFINED_CD = dtbl.Rows[0]["DEFINED_CD"].ConvertToString();
                    obj.CASE_STATUS = dtbl.Rows[0]["CASE_STATUS"].ConvertToString();
                    obj.FORM_NO = dtbl.Rows[0]["FORM_NO"].ConvertToString();
                    obj.grievanceCd = dtbl.Rows[0]["DEFINED_CD"].ConvertToString();
                    obj.REGISTRATION_NO = dtbl.Rows[0]["REGISTRATION_NO"].ConvertToString();
                    obj.DIST_CD = dtbl.Rows[0]["DIST_CD"].ToDecimal();
                    obj.VDC_MUN_CD = dtbl.Rows[0]["VDC_MUN_CD"].ConvertToString();
                    obj.WARD_NO = dtbl.Rows[0]["WARD_NO"].ToDecimal();
                    obj.AREA = dtbl.Rows[0]["AREA"].ConvertToString();
                    obj.REGISTRATION_DIST_CD = dtbl.Rows[0]["REGISTRATION_DIST_CD"].ToDecimal();
                    obj.REGISTRATION_VDC_MUN_CD = dtbl.Rows[0]["REGISTRATION_VDC_MUN_CD"].ConvertToString();
                    obj.REGISTRATION_WARD_NO = dtbl.Rows[0]["REGISTRATION_WARD_NO"].ToDecimal();
                    obj.REGISTRATION_AREA = dtbl.Rows[0]["REGISTRATION_AREA"].ConvertToString();
                    if(dtbl.Rows[0]["REGISTRATION_DATE_ENG"].ConvertToString()!=""){
                        obj.REGISTRATION_DATE_ENG = Convert.ToDateTime(dtbl.Rows[0]["REGISTRATION_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    obj.REGISTRATION_DATE_LOC = dtbl.Rows[0]["REGISTRATION_DATE_LOC"].ConvertToString();
                    obj.FIRST_NAME_ENG = dtbl.Rows[0]["FIRST_NAME_ENG"].ConvertToString();
                    obj.MIDDLE_NAME_ENG = dtbl.Rows[0]["MIDDLE_NAME_ENG"].ConvertToString();
                    obj.LAST_NAME_ENG = dtbl.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    obj.LALPURJA_NO = dtbl.Rows[0]["LALPURJA_NO"].ConvertToString();
                    if (dtbl.Rows[0]["LALPURJA_ISSUE_DATE_ENG"].ConvertToString() != "")
                    {
                        obj.LALPURJA_ISSUE_DATE_ENG = Convert.ToDateTime(dtbl.Rows[0]["LALPURJA_ISSUE_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    obj.LALPURJA_ISSUE_DATE_LOC = dtbl.Rows[0]["LALPURJA_ISSUE_DATE_LOC"].ConvertToString();
                    obj.FATHER_FIRST_NAME_ENG = dtbl.Rows[0]["FATHER_FIRST_NAME_ENG"].ConvertToString();
                    obj.FATHER_MIDDLE_NAME_ENG = dtbl.Rows[0]["FATHER_MIDDLE_NAME_ENG"].ConvertToString();
                    obj.FATHER_LAST_NAME_ENG = dtbl.Rows[0]["FATHER_LAST_NAME_ENG"].ConvertToString();
                    obj.HUSBAND_FIRST_NAME_ENG = dtbl.Rows[0]["HUSBAND_FIRST_NAME_ENG"].ConvertToString();
                    obj.HUSBAND_MIDDLE_NAME_ENG = dtbl.Rows[0]["HUSBAND_MIDDLE_NAME_ENG"].ConvertToString();
                    obj.HUSBAND_LAST_NAME_ENG = dtbl.Rows[0]["HUSBAND_LAST_NAME_ENG"].ConvertToString();
                    obj.CITIZENSHIP_NO = dtbl.Rows[0]["CITIZENSHIP_NO"].ConvertToString();
                    if (dtbl.Rows[0]["CITIZENSHIP_ISSUE_DATE_ENG"].ConvertToString() != "")
                    {
                        obj.CITIZENSHIP_ISSUE_DATE_ENG = Convert.ToDateTime(dtbl.Rows[0]["CITIZENSHIP_ISSUE_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    obj.CITIZENSHIP_ISSUE_DATE_LOC = dtbl.Rows[0]["CITIZENSHIP_ISSUE_DATE_LOC"].ConvertToString();
                    obj.GFATHER_FIRST_NAME_ENG = dtbl.Rows[0]["GFATHER_FIRST_NAME_ENG"].ConvertToString();
                    obj.GFATHER_MIDDLE_NAME_ENG = dtbl.Rows[0]["GFATHER_MIDDLE_NAME_ENG"].ConvertToString();
                    obj.GFATHER_LAST_NAME_ENG = dtbl.Rows[0]["GFATHER_LAST_NAME_ENG"].ConvertToString();
                    obj.HOUSEHOLD_MEMER_COUNT = dtbl.Rows[0]["HOUSEHOLD_MEMBER_COUNT"].ToDecimal();
                    obj.BENEFICIARY_ID = dtbl.Rows[0]["BENEFICIARY_ID"].ConvertToString();
                    obj.LAST_NAME_ENG = dtbl.Rows[0]["LAST_NAME_ENG"].ConvertToString();
                    obj.CONTACT_PHONE_NO = dtbl.Rows[0]["CONTACT_PHONE_NO"].ConvertToString();
                    obj.HOUSE_LAND_LEGAL_OWNERCD = dtbl.Rows[0]["HOUSE_LAND_LEGAL_OWNERCD"].ConvertToString();
                    obj.OtherHouse = dtbl.Rows[0]["HOUSE_IN_OTHER_PLACE"].ConvertToString();
                    obj.ENUMENATOR_ID = dtbl.Rows[0]["ENUMENATOR_ID"].ConvertToString();
                    obj.ENUMENATOR_FIRST_NAME_ENG = dtbl.Rows[0]["ENUMENATOR_FIRST_NAME_ENG"].ConvertToString();
                    obj.ENUMENATOR_MIDDLE_NAME_ENG = dtbl.Rows[0]["ENUMENATOR_MIDDLE_NAME_ENG"].ConvertToString();
                    obj.ENUMENATOR_LAST_NAME_ENG = dtbl.Rows[0]["ENUMENATOR_LAST_NAME_ENG"].ConvertToString();
                    if (dtbl.Rows[0]["ENUMENATOR_SIGNED_DATE_ENG"].ConvertToString() != "")
                    {
                        obj.ENUMENATOR_SIGNED_DATE_ENG = Convert.ToDateTime(dtbl.Rows[0]["ENUMENATOR_SIGNED_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    obj.ENUMENATOR_SIGNED_DATE_LOC = dtbl.Rows[0]["ENUMENATOR_SIGNED_DATE_LOC"].ConvertToString();
                    if (dtbl.Rows[0]["CASE_SIGNATURE_DATE_ENG"].ConvertToString() != "")
                    {
                        obj.CASE_SIGNATURE_DATE_ENG = Convert.ToDateTime(dtbl.Rows[0]["CASE_SIGNATURE_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    obj.CASE_SIGNATURE_DATE_LOC = dtbl.Rows[0]["CASE_SIGNATURE_DATE_LOC"].ConvertToString();
                    if (dtbl.Rows[0]["CASE_ADDRESSED_DATE_ENG"].ConvertToString() != "")
                    {
                        obj.CASE_ADDRESSED_DATE_ENG = Convert.ToDateTime(dtbl.Rows[0]["CASE_ADDRESSED_DATE_ENG"].ConvertToString()).ToString("yyyy-MM-dd");
                    }
                    obj.CASE_ADDRESSED_BY_FNAME_ENG = dtbl.Rows[0]["CASE_ADDRESSED_BY_FNAME_ENG"].ConvertToString();
                    obj.CASE_ADDRESSED_BY_MNAME_ENG = dtbl.Rows[0]["CASE_ADDRESSED_BY_MNAME_ENG"].ConvertToString();
                    obj.CASE_ADDRESSED_BY_LNAME_ENG = dtbl.Rows[0]["CASE_ADDRESSED_BY_LNAME_ENG"].ConvertToString();
                    obj.CASE_ADDRESSED_DATE_LOC = dtbl.Rows[0]["CASE_ADDRESSED_DATE_LOC"].ConvertToString();
                    obj.HOUSE_LAND_LEGAL_OTH_COMMENT = dtbl.Rows[0]["HOUSE_LAND_LEGAL_OTHER_COMMENT"].ConvertToString();
                    
                }
            }
            catch (Exception ex)
            {
                obj = null;
                ExceptionManager.AppendLog(ex);
            }
            return obj;
        }

        public DataTable CaseGrievanceDtl_GetHistory(string caseId)
        {
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    cmdText = String.Format(@"SELECT * 
                                              FROM CM_GRIEVANCE_DTL WHERE GRIEVANCE_CD='" + caseId + "' ORDER BY SNO");
                    try
                    {
                        dtbl = service.GetDataTable(cmdText, null);
                    }
                    catch (Exception ex)
                    { ExceptionManager.AppendLog(ex); }
                    finally
                    {
                        if (service.Transaction != null && service.Transaction.Connection != null)
                        {
                            service.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dtbl = null;
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }
        #endregion
        #region  Add,Edit,Approve and Delete
        public bool AddCaseGrievance(CaseGrievanceModel objCaseGrievance, string Mode, out string exc)
        {
            QueryResult qr = null;
            QueryResult qr2 = null;
            CaseGrievanceInfo objCaseGrievanceInfo = new CaseGrievanceInfo();
            CaseGrievanceDtlInfo objCaseGrievanceDtlInfo = new CaseGrievanceDtlInfo();
            bool res = false;
            exc = "";
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                objCaseGrievanceInfo.grievanceCd = objCaseGrievance.grievanceCd;
                if (!string.IsNullOrEmpty(objCaseGrievance.householdId.ConvertToString()))
                {
                    objCaseGrievanceInfo.householdId = objCaseGrievance.householdId.ConvertToString();
                }
                if (!string.IsNullOrEmpty(objCaseGrievance.PER_REGION_ENG.ConvertToString()))
                {
                    objCaseGrievanceInfo.regionCd = GetData.GetCodeFor(DataType.Region, objCaseGrievance.PER_REGION_ENG);
                }
                if (!string.IsNullOrEmpty(objCaseGrievance.PER_Zone_ENG.ConvertToString()))
                {
                    objCaseGrievanceInfo.ZoneCd = GetData.GetCodeFor(DataType.Zone, objCaseGrievance.PER_Zone_ENG);
                }
                if (!string.IsNullOrEmpty(objCaseGrievance.PER_District_ENG.ConvertToString()))
                {
                    objCaseGrievanceInfo.DistrictCd = GetData.GetCodeFor(DataType.District, objCaseGrievance.PER_District_ENG);
                }
                if (!string.IsNullOrEmpty(objCaseGrievance.VDCMun_CD.ConvertToString()))
                {
                    objCaseGrievanceInfo.VDCMunCd = GetData.GetCodeFor(DataType.VdcMun, objCaseGrievance.VDCMun_CD);
                }
                if (!string.IsNullOrEmpty(objCaseGrievance.PER_WARD_NO.ConvertToString()))
                {
                    objCaseGrievanceInfo.WardCd = GetData.GetCodeFor(DataType.Ward, objCaseGrievance.PER_WARD_NO);
                }
                objCaseGrievanceInfo.HhFullName = objCaseGrievance.hhFullName;
                objCaseGrievanceInfo.CaseStatus = objCaseGrievance.caseStatus.ConvertToString() == "" ? "R" : objCaseGrievance.caseStatus.ConvertToString();
                objCaseGrievanceInfo.pinNo = objCaseGrievance.pinNo;
                objCaseGrievanceInfo.address = objCaseGrievance.address;
                objCaseGrievanceInfo.phoneNo = objCaseGrievance.phoneNo;
                objCaseGrievanceInfo.grievanceCategoryGroup = objCaseGrievance.regTypeGrpCd;
                objCaseGrievanceInfo.grievanceCategory = objCaseGrievance.listCase.ConvertToString();
                objCaseGrievanceInfo.caseDesc = objCaseGrievance.caseDesc;
                objCaseGrievanceInfo.signApplicant = objCaseGrievance.signApplicant;
              
                if (objCaseGrievance.signAppDate != null)
                {
                    string[] date = objCaseGrievance.signAppDate.Split('/');
                    objCaseGrievance.signAppDate = date[1] + "/" + date[0] + "/" + date[2];
                }
                objCaseGrievanceInfo.signAppDate = (objCaseGrievance.signAppDate != null) ? Convert.ToDateTime(objCaseGrievance.signAppDate).ToString("MM-dd-yyyy") : null;
                objCaseGrievanceInfo.processRemark = objCaseGrievance.processRemark;
                objCaseGrievanceInfo.signDswOfficer = objCaseGrievance.signDswOfficer;
                if (objCaseGrievance.signDswDate != null)
                {
                    string[] date = objCaseGrievance.signDswDate.Split('/');
                    objCaseGrievance.signDswDate = date[1] + "/" + date[0] + "/" + date[2];
                }
                objCaseGrievanceInfo.signDswDate = (objCaseGrievance.signDswDate != null) ? Convert.ToDateTime(objCaseGrievance.signDswDate).ToString("MM-dd-yyyy") : null;
                objCaseGrievanceInfo.finalRemark = objCaseGrievance.finalRemark;
                objCaseGrievanceInfo.signSeniorOfficer = objCaseGrievance.signSeniorOfficer;

                if (objCaseGrievance.signSeniorDate != null)
                {
                    string[] date = objCaseGrievance.signSeniorDate.Split('/');
                    objCaseGrievance.signSeniorDate = date[1] + "/" + date[0] + "/" + date[2];
                }
                objCaseGrievanceInfo.signSeniorDate = (objCaseGrievance.signSeniorDate != null) ? Convert.ToDateTime(objCaseGrievance.signSeniorDate).ToString("MM-dd-yyyy") : null;
                objCaseGrievanceInfo.EnteredBy = strUserName.Trim();
                objCaseGrievanceInfo.EnteredDt = objCaseGrievance.enteredDt;
                objCaseGrievanceInfo.UpdatedBy = strUserName.Trim();
                objCaseGrievanceInfo.UpdatedDt = objCaseGrievance.updatedDt;
                objCaseGrievanceInfo.Approved = objCaseGrievance.approved;
                objCaseGrievanceInfo.ApprovedBy = objCaseGrievance.approvedBy;
                objCaseGrievanceInfo.ApprovedDt = objCaseGrievance.approvedDt;
                objCaseGrievanceInfo.IpAddress = CommonVariables.IPAddress;
                objCaseGrievanceInfo.Mode = Mode;
                service.Begin();
                try
                {
                    qr = service.SubmitChanges(objCaseGrievanceInfo, true);
                    if (qr.IsSuccess)
                    {
                        if (objCaseGrievance.caseStatus.ConvertToString() == "P")
                        {
                            objCaseGrievanceDtlInfo.Mode = "I";
                            objCaseGrievanceDtlInfo.grievanceCd = objCaseGrievance.grievanceCd;
                            objCaseGrievanceDtlInfo.processRemark = objCaseGrievance.processRemark;
                            objCaseGrievanceDtlInfo.signDSWOfficer = objCaseGrievance.signDswOfficer;
                            objCaseGrievanceDtlInfo.signedDSWDate = (objCaseGrievance.signDswDate != null) ? Convert.ToDateTime(objCaseGrievance.signDswDate).ToString("MM-dd-yyyy") : null;
                            objCaseGrievanceDtlInfo.EnteredBy = strUserName.Trim();
                            objCaseGrievanceDtlInfo.EnteredDt = objCaseGrievance.enteredDt;
                            qr2 = service.SubmitChanges(objCaseGrievanceDtlInfo, true);
                        }
                        exc = qr["@V_GRIEVANCE_CD"].ConvertToString();
                    }
                }
                catch (SqlException oe)
                {
                    qr = new QueryResult();
                    service.RollBack();
                    exc = oe.Errors.ToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    qr = new QueryResult();
                    service.RollBack();
                    exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null && service.Transaction.Connection != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null)
            {
                res = qr.IsSuccess;
            }
            return res;
        }

        //public bool AddCaseGrievanceDtl(CaseGrievanceModel objCaseGrievance, string Mode, out string exc)
        //{
        //    QueryResult qr = null;
        //    CaseGrievanceDtlInfo objCaseGrievanceInfo = new CaseGrievanceDtlInfo();
        //    bool res = false;
        //    exc = "";
        //    string strUserName = string.Empty;
        //    strUserName = SessionCheck.getSessionUsername();
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        objCaseGrievanceInfo.CaseId = objCaseGrievance.caseId;
        //        objCaseGrievanceInfo.SNo = objCaseGrievance.sno;
        //        objCaseGrievanceInfo.CaseStatusCd = objCaseGrievance.caseStatus.ConvertToString() == "" ? "P" : objCaseGrievance.caseStatus.ConvertToString();
        //        objCaseGrievanceInfo.CaseStatusRemarks = objCaseGrievance.caseStatusRemarks;
        //        objCaseGrievanceInfo.CaseStatusDt = objCaseGrievance.caseStatusDt;
        //        objCaseGrievanceInfo.EnteredBy = strUserName.Trim();
        //        objCaseGrievanceInfo.EnteredDt = objCaseGrievance.enteredDt;
        //        objCaseGrievanceInfo.IpAddress = CommonVariables.IPAddress;
        //        objCaseGrievanceInfo.Mode = Mode;
        //        service.Begin();
        //        try
        //        {
        //            qr = service.SubmitChanges(objCaseGrievanceInfo, true);
        //        }
        //        catch (SqlException oe)
        //        {
        //            qr = new QueryResult();
        //            service.RollBack();
        //            exc = oe.Errors.ToString();
        //            ExceptionManager.AppendLog(oe);
        //        }
        //        catch (Exception ex)
        //        {
        //            qr = new QueryResult();
        //            service.RollBack();
        //            exc = ex.ToString();
        //            ExceptionManager.AppendLog(ex);
        //        }
        //        finally
        //        {
        //            if (service.Transaction != null && service.Transaction.Connection != null)
        //            {
        //                service.End();
        //            }
        //        }
        //    }
        //    if (qr != null)
        //    {
        //        res = qr.IsSuccess;
        //    }
        //    return res;
        //}

        #endregion
        public bool CheckDuplicateCaseTitle(string CaseTitle, string caseId)
        {
            string cmdText = string.Empty;
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                if (caseId != null || caseId != string.Empty)
                {

                    cmdText = "SELECT CASE_ID FROM CM_REGISTRATION WHERE UPPER(CASE_TITLE)='" + CaseTitle.ToUpper() + "' AND CASE_ID != '" + caseId + "'";

                }
                else
                {
                    cmdText = "SELECT CASE_ID FROM CM_REGISTRATION WHERE UPPER(CASE_TITLE)='" + CaseTitle.ToUpper() + "'";
                }
                try
                {
                    dtbl = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null && service.Transaction.Connection != null)
                    {
                        service.End();
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }  

    }
}
