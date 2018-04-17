using EntityFramework;
using ExceptionHandler;
using MIS.Models.CaseGrievance;
using MIS.Models.Core;
using MIS.Models.Setup;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace MIS.Services.CaseGrievance
{
    public class NHRSCaseGrievanceService
    {
        CommonFunction commonFC = new CommonFunction();
        public void SaveCaseRegistration(CaseGrievanceModel objCaseGrievance)
        {

            CaseGrievanceRegistrationInfo objCaseInfo = new CaseGrievanceRegistrationInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {


                    objCaseInfo.CASE_REGISTRATION_ID = objCaseGrievance.CASE_REGISTRATION_ID.ToDecimal();
                    objCaseInfo.FORM_NO = objCaseGrievance.FORM_NO.ConvertToString();
                    objCaseInfo.DEFINED_CD = objCaseGrievance.DEFINED_CD;
                    objCaseInfo.REGISTRATION_NO = objCaseGrievance.REGISTRATION_NO.ConvertToString();
                    objCaseInfo.REGISTRATION_DIST_CD = objCaseGrievance.REGISTRATION_DIST_CD.ToDecimal();
                    objCaseInfo.REGISTRATION_VDC_MUN_CD = objCaseGrievance.REGISTRATION_VDC_MUN_CD.ToDecimal();
                    objCaseInfo.REGISTRATION_WARD_NO = objCaseGrievance.REGISTRATION_WARD_NO.ToDecimal();
                    objCaseInfo.REGISTRATION_AREA = objCaseGrievance.REGISTRATION_AREA.ConvertToString();
                    objCaseInfo.REGISTRATION_DATE_ENG = objCaseGrievance.REGISTRATION_DATE_ENG.ToDateTime();
                    objCaseInfo.REGISTRATION_DATE_LOC = objCaseGrievance.REGISTRATION_DATE_LOC.ConvertToString();
                    objCaseInfo.FIRST_NAME_ENG = objCaseGrievance.FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.FIRST_NAME_LOC = objCaseGrievance.FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.MIDDLE_NAME_ENG = objCaseGrievance.MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.MIDDLE_NAME_LOC = objCaseGrievance.MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.LAST_NAME_ENG = objCaseGrievance.LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.LAST_NAME_LOC = objCaseGrievance.LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.FATHER_FIRST_NAME_ENG = objCaseGrievance.FATHER_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.FATHER_FIRST_NAME_LOC = objCaseGrievance.FATHER_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.FATHER_MIDDLE_NAME_ENG = objCaseGrievance.FATHER_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.FATHER_MIDDLE_NAME_LOC = objCaseGrievance.FATHER_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.FATHER_LAST_NAME_ENG = objCaseGrievance.FATHER_LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.FATHER_LAST_NAME_LOC = objCaseGrievance.FATHER_LAST_NAME_ENG.ConvertToString();

                    objCaseInfo.HUSBAND_FIRST_NAME_ENG = objCaseGrievance.HUSBAND_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.HUSBAND_FIRST_NAME_LOC = objCaseGrievance.HUSBAND_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.HUSBAND_MIDDLE_NAME_ENG = objCaseGrievance.HUSBAND_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.HUSBAND_MIDDLE_NAME_LOC = objCaseGrievance.HUSBAND_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.HUSBAND_LAST_NAME_ENG = objCaseGrievance.HUSBAND_LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.HUSBAND_LAST_NAME_LOC = objCaseGrievance.HUSBAND_LAST_NAME_ENG.ConvertToString();


                    objCaseInfo.GFATHER_FIRST_NAME_ENG = objCaseGrievance.GFATHER_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.GFATHER_FIRST_NAME_LOC = objCaseGrievance.GFATHER_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.GFATHER_MIDDLE_NAME_ENG = objCaseGrievance.GFATHER_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.GFATHER_MIDDLE_NAME_LOC = objCaseGrievance.GFATHER_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.GFATHER_LAST_NAME_ENG = objCaseGrievance.GFATHER_LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.GFATHER_LAST_NAME_LOC = objCaseGrievance.GFATHER_LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.HOUSEHOLD_MEMBER_COUNT = objCaseGrievance.HOUSEHOLD_MEMER_COUNT.ToDecimal();
                    objCaseInfo.BENEFICIARY_ID = objCaseGrievance.BENEFICIARY_ID.ConvertToString();
                    objCaseInfo.SURVEY_ID = objCaseGrievance.SURVEY_ID.ConvertToString();
                    objCaseInfo.LALPURJA_NO = objCaseGrievance.LALPURJA_NO.ConvertToString();
                    objCaseInfo.LALPURJA_ISSUE_DATE_ENG = objCaseGrievance.LALPURJA_ISSUE_DATE_ENG.ToDateTime();
                    objCaseInfo.LALPURJA_ISSUE_DATE_LOC = objCaseGrievance.LALPURJA_ISSUE_DATE_LOC.ConvertToString();
                    objCaseInfo.CITIZENSHIP_NO = objCaseGrievance.CITIZENSHIP_NO.ConvertToString();
                    objCaseInfo.CITIZENSHIP_ISSUE_DATE_ENG = objCaseGrievance.CITIZENSHIP_ISSUE_DATE_ENG.ToDateTime();
                    objCaseInfo.CITIZENSHIP_ISSUE_DATE_LOC = objCaseGrievance.CITIZENSHIP_ISSUE_DATE_LOC.ConvertToString();
                    objCaseInfo.DIST_CD = objCaseGrievance.DIST_CD.ToDecimal();
                    objCaseInfo.VDC_MUN_CD = objCaseGrievance.VDC_MUN_CD.ToDecimal();
                    objCaseInfo.WARD_NO = objCaseGrievance.WARD_NO.ToDecimal();
                    objCaseInfo.AREA = objCaseGrievance.AREA.ConvertToString();
                    objCaseInfo.CONTACT_PHONE_NO = objCaseGrievance.CONTACT_PHONE_NO.ConvertToString();
                    objCaseInfo.HOUSE_LAND_LEGAL_OWNERCD = objCaseGrievance.HOUSE_LAND_LEGAL_OWNERCD.ToDecimal();
                    objCaseInfo.HOUSE_IN_OTHER_PLACE = objCaseGrievance.OtherHouse.ConvertToString();
                    objCaseInfo.HOUSE_LAND_LEGAL_OTH_COMMENT = objCaseGrievance.HOUSE_LAND_LEGAL_OTH_COMMENT.ConvertToString();
                    objCaseInfo.CASE_SIGNATURE_DATE_ENG = objCaseGrievance.CASE_SIGNATURE_DATE_ENG.ToDateTime();
                    objCaseInfo.CASE_SIGNATURE_DATE_LOC = objCaseGrievance.CASE_SIGNATURE_DATE_LOC.ConvertToString();
                    objCaseInfo.ENUMENATOR_ID = objCaseGrievance.ENUMENATOR_ID.ToDecimal();
                    objCaseInfo.ENUMENATOR_FIRST_NAME_ENG = objCaseGrievance.ENUMENATOR_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.ENUMENATOR_MIDDLE_NAME_ENG = objCaseGrievance.ENUMENATOR_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.ENUMENATOR_LAST_NAME_ENG = objCaseGrievance.ENUMENATOR_LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.ENUMENATOR_FIRST_NAME_LOC = objCaseGrievance.ENUMENATOR_FIRST_NAME_ENG.ConvertToString();
                    objCaseInfo.ENUMENATOR_MIDDLE_NAME_LOC = objCaseGrievance.ENUMENATOR_MIDDLE_NAME_ENG.ConvertToString();
                    objCaseInfo.ENUMENATOR_LAST_NAME_LOC = objCaseGrievance.ENUMENATOR_LAST_NAME_ENG.ConvertToString();
                    objCaseInfo.ENUMENATOR_SIGNED_DATE_ENG = objCaseGrievance.ENUMENATOR_SIGNED_DATE_ENG.ToDateTime();
                    objCaseInfo.ENUMENATOR_SIGNED_DATE_LOC = objCaseGrievance.ENUMENATOR_SIGNED_DATE_LOC.ConvertToString();
                    objCaseInfo.CASE_STATUS = objCaseGrievance.CASE_STATUS.ConvertToString();
                    objCaseInfo.CASE_ADDRESSED_BY_FNAME_ENG = objCaseGrievance.CASE_ADDRESSED_BY_FNAME_ENG.ConvertToString();
                    objCaseInfo.CASE_ADDRESSED_BY_MNAME_ENG = objCaseGrievance.CASE_ADDRESSED_BY_MNAME_ENG.ConvertToString();
                    objCaseInfo.CASE_ADDRESSED_BY_LNAME_ENG = objCaseGrievance.CASE_ADDRESSED_BY_LNAME_ENG.ConvertToString();
                    objCaseInfo.CASE_ADDRESSED_DATE_ENG = objCaseGrievance.CASE_ADDRESSED_DATE_ENG.ToDateTime();
                    objCaseInfo.CASE_ADDRESSED_DATE_LOC = objCaseGrievance.CASE_ADDRESSED_DATE_LOC;
                    objCaseInfo.ENTERED_BY = SessionCheck.getSessionUsername();
                    objCaseInfo.ENTERED_DT = DateTime.Now.ConvertToString();
                    objCaseInfo.LAST_UPDATED_BY = DBNull.Value.ConvertToString();
                    objCaseInfo.LAST_UPDATED_DT = DateTime.Now.ConvertToString();
                    objCaseInfo.Mode = objCaseGrievance.Mode;
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objCaseInfo, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }

        public void DeleteCaseRegistration(CaseGrievanceModel objCaseGrievance)
        {

            CaseGrievanceRegistrationInfo objCaseInfo = new CaseGrievanceRegistrationInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {


                    objCaseInfo.CASE_REGISTRATION_ID = objCaseGrievance.CASE_REGISTRATION_ID.ToDecimal();
                    objCaseInfo.Mode = objCaseGrievance.Mode;
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objCaseInfo, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }

        public void UpdateDataNotFound(string CaseRegistrationID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "Update NHRS.NHRS_GRIEVANCE_REGISTRATION SET CASE_STATUS='NF' , LAST_UPDATED_BY='" + SessionCheck.getSessionUsername() + "' , LAST_UPDATED_DT=TO_DATE('" + DateTime.Now.ToShortDateString() + "','MM-DD-YYYY') where CASE_REGISTRATION_ID='" + CaseRegistrationID + "'";
                try
                {
                    service.Begin();
                    service.SubmitChanges(cmdText);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
        }

        public void SaveCaseDetail(CaseGrievanceDetail objCaseDetail)
        {
            NHRSCaseDetailInfo objCaseInfo = new NHRSCaseDetailInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    if (objCaseDetail.Mode == "I")
                    {
                        objCaseInfo.CASE_REGISTRATION_ID = objCaseDetail.CASE_REGISTRATION_ID.ToDecimal();
                        objCaseInfo.CASE_GRIEVANCE_TYPE_CD = objCaseDetail.CASE_GRIEVANCE_TYPE_CD.ToDecimal();
                        objCaseInfo.CASE_STATUS = objCaseDetail.CASE_STATUS.ConvertToString();
                        objCaseInfo.ADDRESSED_OFFICE = objCaseDetail.ADDRESSED_OFFICE.ConvertToString();
                        objCaseInfo.FORWARDED_OFFICE = objCaseDetail.FORWARDED_OFFICE.ConvertToString();
                        objCaseInfo.ADDRESSED_DATE_ENG = objCaseDetail.ADDRESSED_DATE_ENG.ToDateTime();
                        objCaseInfo.ADDRESSED_DATE_LOC = objCaseDetail.ADDRESSED_DATE_LOC.ConvertToString();
                        objCaseInfo.REMARKS = objCaseDetail.REMARKS.ConvertToString();
                        objCaseInfo.ENTERED_BY = SessionCheck.getSessionUsername();
                        //objCaseInfo.ENTERED_DT = DateTime.Now.ToString();
                        objCaseInfo.LAST_UPDATED_BY = SessionCheck.getSessionUsername();
                        //objCaseInfo.LAST_UPDATED_DT = DateTime.Now.ToString();
                    }

                    if (objCaseDetail.Mode == "U")
                    {
                        objCaseInfo.CASE_REGISTRATION_ID = objCaseDetail.CASE_REGISTRATION_ID.ToDecimal();
                        objCaseInfo.CASE_GRIEVANCE_TYPE_CD = objCaseDetail.CASE_GRIEVANCE_TYPE_CD.ToDecimal();
                        objCaseInfo.CASE_STATUS = objCaseDetail.CASE_STATUS.ConvertToString();
                        objCaseInfo.ADDRESSED_OFFICE = objCaseDetail.ADDRESSED_OFFICE.ConvertToString();
                        objCaseInfo.FORWARDED_OFFICE = objCaseDetail.FORWARDED_OFFICE.ConvertToString();
                        objCaseInfo.ADDRESSED_DATE_ENG = objCaseDetail.ADDRESSED_DATE_ENG.ToDateTime();
                        objCaseInfo.ADDRESSED_DATE_LOC = objCaseDetail.ADDRESSED_DATE_LOC.ConvertToString();
                        objCaseInfo.GRIEVANCE_DETAIL_ID = objCaseDetail.GRIEVANCE_DETAIL_ID.ConvertToString();
                        objCaseInfo.REMARKS = objCaseDetail.REMARKS.ConvertToString();
                        objCaseInfo.LAST_UPDATED_BY = SessionCheck.getSessionUsername();
                        objCaseInfo.LAST_UPDATED_DT = DateTime.Now.ToString();
                    }


                    objCaseInfo.Mode = objCaseDetail.Mode;
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objCaseInfo, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }
        public void SaveOtherHouseDetail(CaseGrievanceOTHDetail objOtherHouseDetail)
        {
            NhrsCaseGrievanceOTHInfo objCaseOTHInfo = new NhrsCaseGrievanceOTHInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    objCaseOTHInfo.CASE_REGISTRATION_ID = objOtherHouseDetail.CASE_REGISTRATION_ID.ToDecimal();
                    objCaseOTHInfo.GRIEVANCE_OTH_DETAIL_ID = objOtherHouseDetail.GRIEVANCE_OTH_DETAIL_ID.ToDecimal();
                    objCaseOTHInfo.HOUSE_OWNER_FNAME_ENG = objOtherHouseDetail.HOUSEHOLD_HEAD_FNAME_ENG.ConvertToString();
                    objCaseOTHInfo.HOUSE_OWNER_LNAME_ENG = objOtherHouseDetail.HOUSEHOLD_HEAD_LNAME_ENG.ConvertToString();
                    objCaseOTHInfo.HOUSE_OWNER_MNAME_ENG = objOtherHouseDetail.HOUSEHOLD_HEAD_MNAME_ENG.ConvertToString();
                    objCaseOTHInfo.HOUSE_OWNER_FNAME_LOC = objOtherHouseDetail.HOUSEHOLD_HEAD_FNAME_ENG.ConvertToString();
                    objCaseOTHInfo.HOUSE_OWNER_LNAME_LOC = objOtherHouseDetail.HOUSEHOLD_HEAD_LNAME_ENG.ConvertToString();
                    objCaseOTHInfo.HOUSE_OWNER_MNAME_LOC = objOtherHouseDetail.HOUSEHOLD_HEAD_MNAME_ENG.ConvertToString();
                    objCaseOTHInfo.HOUSE_DIST_CD = objOtherHouseDetail.HOUSE_DIST_CD.ToDecimal();
                    objCaseOTHInfo.HOUSE_VDC_MUN_CD = objOtherHouseDetail.HOUSE_VDC_MUN_CD.ToDecimal();
                    objCaseOTHInfo.HOUSE_WARD_NO = objOtherHouseDetail.HOUSE_WARD_NO.ToDecimal();
                    objCaseOTHInfo.HOUSE_AREA = objOtherHouseDetail.HOUSE_AREA.ConvertToString();
                    objCaseOTHInfo.HOUSE_CONDITION_CD = objOtherHouseDetail.HOUSE_CONDITION_CD.ToDecimal();
                    objCaseOTHInfo.ENTERED_BY = SessionCheck.getSessionUsername();
                    objCaseOTHInfo.ENTERED_DT = DateTime.Now.ToString();
                    objCaseOTHInfo.LAST_UPDATED_BY = SessionCheck.getSessionUsername();
                    objCaseOTHInfo.LAST_UPDATED_DT = DateTime.Now.ToString();
                    objCaseOTHInfo.Mode = objOtherHouseDetail.Mode;
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(objCaseOTHInfo, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }

        }
        public DataTable GetCaseRegistrationID(string FormNo, string District, string VDC, string Ward)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT CASE_REGISTRATION_ID FROM NHRS_GRIEVANCE_REGISTRATION where 1=1";

                if (FormNo != "")
                {
                    cmdText += " and upper(FORM_NO) ='" + FormNo.ToUpper() + "'";
                }
                if (District != "")
                {
                    cmdText += " and upper(REGISTRATION_DIST_CD)='" + District.ToUpper() + "'";
                }
                if (VDC != "")
                {
                    cmdText += " and upper(REGISTRATION_VDC_MUN_CD) ='" + VDC.ToUpper() + "'";
                }
                if (Ward != "")
                {
                    cmdText += " and upper(REGISTRATION_WARD_NO) ='" + Ward.ToUpper() + "'";
                }
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }

        #region Case Grievance Doc Type(Binod)
        public DataTable DocumentName()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_GRIEVANCE_DOC_TYPE Order by ORDER_NO";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        #endregion

        public DataTable CaseRegistrationTYpe()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS.NHRS_CASE_GRIEVIENCE_TYPE Order by CASE_GRIEVIENCE_TYPE_CD";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        public DataTable caseRegistrationChecked(string CaseRegistrationID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "select GTD.CASE_GRIEVANCE_TYPE_CD,GTD.CASE_REGISTRATION_ID,GTD.REMARKS,GT.DESC_ENG,GT.DESC_LOC from NHRS_GRIEVANCE_TYPE_DETAIL GTD INNER JOIN NHRS_CASE_GRIEVIENCE_TYPE GT ON GTD.CASE_GRIEVANCE_TYPE_CD=GT.CASE_GRIEVIENCE_TYPE_CD  where GTD.CASE_REGISTRATION_ID='" + CaseRegistrationID + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        public DataTable DocumentChecked(string RegistrationID)
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT DD.GRIEVANCE_DOC_TYPE_CD,DD.GRIEVANCE_DOC_DETAIL_ID ,DT.DESC_ENG,DT.DESC_LOC FROM NHRS_GRIEVANCE_DOC_DETAIL DD INNER JOIN NHRS_GRIEVANCE_DOC_TYPE DT ON DD.GRIEVANCE_DOC_TYPE_CD = DT.DOC_TYPE_CD where CASE_REGISTRATION_ID='" + RegistrationID + "'";

                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        public DataTable Grievancedoctype()
        {
            DataTable dt = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                cmdText = "SELECT * FROM NHRS_GRIEVANCE_DOC_TYPE Order by ORDER_NO";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            return dt;
        }
        public void InsertCaseGrievanceDetail(NHRGrievanceDocDetail ngd)
        {
            NHRSGrievanceDocDetailInfo gdti = new NHRSGrievanceDocDetailInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    gdti.Grievancedocdetailid = ngd.grievancedocdetailid.ToDecimal();
                    gdti.Caseregistrationid = ngd.caseregistrationid.ToDecimal();
                    gdti.Enteredby = SessionCheck.getSessionUsername();
                    gdti.Entereddate = DateTime.Now.ToDateTime();
                    gdti.Grievancedoctypecd = ngd.grievancedoctypecd.ToDecimal();
                    gdti.Lastupdatedby = SessionCheck.getSessionUsername();
                    gdti.Lastupdateddate = DateTime.Now.ToDateTime();
                    gdti.Mode = "I";
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(gdti, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }
        public void DeleteCaseGrievanceDocDetail(NHRGrievanceDocDetail ngd)
        {
            NHRSGrievanceDocDetailInfo gdti = new NHRSGrievanceDocDetailInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    gdti.Caseregistrationid = ngd.caseregistrationid.ToDecimal();
                    //gdti.Enteredby = SessionCheck.getSessionUsername();
                    //gdti.Entereddate = DateTime.Now.ToDateTime();
                    //gdti.Grievancedoctypecd = ngd.grievancedoctypecd.ToDecimal();
                    //gdti.Lastupdatedby = ngd.lastupdatedby;
                    //gdti.Lastupdateddate = DateTime.Now.ToDateTime();
                    gdti.Mode = "D";
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(gdti, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }

        public void InsertCaseRegistrationTypeDetail(CaseGrievanceRegistrationType ngd)
        {
            CaseGrievanceTypeDetailInfo gdti = new CaseGrievanceTypeDetailInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    gdti.GRIEVANCE_TYPE_DETAIL_ID = ngd.grievancetypedetailid.ToDecimal();
                    gdti.Caseregistrationid = ngd.caseregistrationid.ToDecimal();
                    gdti.Enteredby = SessionCheck.getSessionUsername();
                    gdti.Entereddate = DateTime.Now.ToDateTime();
                    gdti.CASE_GRIEVANCE_TYPE_CD = ngd.grievancetypecd.ToDecimal();
                    gdti.Lastupdatedby = SessionCheck.getSessionUsername();
                    gdti.Remarks = ngd.remarks;
                    gdti.Lastupdateddate = DateTime.Now.ToDateTime();
                    gdti.Mode = "I";
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(gdti, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }
        public void DeleteCaseRegistrationTypeDetail(CaseGrievanceRegistrationType ngd)
        {
            CaseGrievanceTypeDetailInfo gdti = new CaseGrievanceTypeDetailInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    gdti.Caseregistrationid = ngd.caseregistrationid.ToDecimal();
                    gdti.Mode = "D";
                    service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(gdti, true);
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
        }
        public DataTable GetCaseGrievanceSearch(CaseGrievanceModel searchParamObject, string CaseType)
        {
            DataTable dt = new DataTable();
            Object District = DBNull.Value;
            Object Vdc = DBNull.Value;
            Object Ward = DBNull.Value;
            Object fullname = DBNull.Value;
            Object CaseRegistrationID = DBNull.Value;
            Object CaseRegistrationNo = DBNull.Value;
            Object FormNo = DBNull.Value;
            Object NissaNo = DBNull.Value;
            Object CaseStatus = DBNull.Value;
            Object RetroFlag=DBNull.Value;
            if (searchParamObject.DistrictCd != "")
            {
                District = searchParamObject.DistrictCd;
            }
            if (searchParamObject.VDCMun != "")
            {
                Vdc = searchParamObject.VDCMun;
            }
            if (searchParamObject.Ward != "")
            {
                Ward = searchParamObject.Ward;
            }
            if (searchParamObject.CASE_REGISTRATION_ID != "")
            {
                CaseRegistrationID = searchParamObject.CASE_REGISTRATION_ID;
            }
            if (searchParamObject.REGISTRATION_NO != "")
            {
                CaseRegistrationNo = searchParamObject.REGISTRATION_NO;
            }
            if (searchParamObject.FORM_NO != "")
            {
                FormNo = searchParamObject.FORM_NO;
            }
            if (searchParamObject.Nissa_NO != "")
            {
                NissaNo = searchParamObject.Nissa_NO;
            }
            if (searchParamObject.CASE_STATUS != "")
            {
                CaseStatus = searchParamObject.CASE_STATUS;
            }

            if (searchParamObject.RETRO_FLAG != "")
            {
                RetroFlag = searchParamObject.RETRO_FLAG;
            }
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_GRIEVANCE_REG_SEARCH",
                        DBNull.Value,
                        CaseRegistrationID,
                        FormNo,
                        CaseRegistrationNo,
                        District,
                        Vdc,
                        Ward,
                        CaseStatus,
                        NissaNo,
                        CaseType.ToDecimal(),
                        DBNull.Value
                    );


            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;

        }
        public DataTable GetCaseDetail(string RegistrationID)
        {
            CaseGrievanceModel obj = new CaseGrievanceModel();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    cmdText = String.Format("Select * from NHRS_GRIEVANCE_DETAIL where CASE_REGISTRATION_ID='" + RegistrationID + "'");
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

                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }
        public DataTable getOtherHouseDetail(string RegistrationID)
        {
            CaseGrievanceModel obj = new CaseGrievanceModel();
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    cmdText = String.Format("Select * from NHRS_GRIEVANCE_OTH_DETAIL where CASE_REGISTRATION_ID='" + RegistrationID + "'");
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

                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }
        public bool CheckDuplicateFormNo(string FormNo, string District, string VDC, string Ward)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;

            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select FORM_NO from NHRS_GRIEVANCE_REGISTRATION where 1=1 ";
                    if (FormNo != "")
                    {
                        cmdText += "AND FORM_NO='" + FormNo.ToDecimal() + "'";
                    }
                    if (District != "")
                    {
                        cmdText += "AND REGISTRATION_DIST_CD='" + District.ToDecimal() + "'";
                    }
                    if (VDC != "")
                    {
                        cmdText += "AND REGISTRATION_VDC_MUN_CD='" + VDC.ToDecimal() + "'";
                    }
                    if (Ward != "")
                    {
                        cmdText += "AND REGISTRATION_WARD_NO='" + Ward.ToDecimal() + "'";
                    }
                    service.Begin();
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
                    if (service.Transaction != null)
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
        public DataSet getAllGrievanceHandlingDetail(string CaseRegistraionID)
        {
            DataSet ds = new DataSet();

            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_NHRS_CASE_GRIEVANCE_DDL";
                service.Begin();
                ds = service.GetDataSet(true, "PR_GRIEVANCE_REGISTRATION_DTL",
                        CaseRegistraionID,
                        DBNull.Value,
                        DBNull.Value,
                        DBNull.Value,
                        DBNull.Value
                    );
            }
            catch (Exception ex)
            {
                ds = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return ds;

        }
        public bool CheckDuplicateRegistrationNo(string RegistrationNo)
        {
            DataTable dtbl = new DataTable();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    cmdText = "select REGISTRATION_NO from NHRS_GRIEVANCE_REGISTRATION where REGISTRATION_NO ='" + RegistrationNo + "'";
                    service.Begin();
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
                    if (service.Transaction != null)
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
        public DataTable GetOwnerDetail(string District, string VDC, string Ward, string NissaNo, string Name, string MobileNo)
        {
            DataTable dt = new DataTable();

            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_OWNER_DETAIL",
                        "E",
                        District,
                        VDC,
                        Ward,
                         Name,
                        NissaNo,
                        MobileNo,
                        DBNull.Value
                    );
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;
        }
        public DataTable GetBeneficiarySearch(CaseGrievanceModel model)
        {
            DataTable dt = new DataTable();
            string District = model.DistrictCd.ConvertToString();
            string VDC = model.VDCMun.ConvertToString();
            string Ward = model.Ward.ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_GRIEVANCE_BENEFICIARY",
                        District,
                        VDC,
                        Ward,
                        DBNull.Value
                    );
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;
        }
        public DataTable GetGrievanceTargeting(CaseGrievanceModel model)
        {
            DataTable dt = new DataTable();
            string District = model.DistrictCd.ConvertToString();
            string VDC = model.VDCMun.ConvertToString();
            string Ward = model.Ward.ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "GET_GRIEVANCE_TARGETING",
                        District,
                        VDC,
                        Ward,
                        DBNull.Value
                    );
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;
        }
        public DataTable GetGrievanceNewApplicantSearch(CaseGrievanceModel model)
        {
            DataTable dt = new DataTable();
            Object P_SESSION_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_ENTERED_BY = SessionCheck.getSessionUsername();
            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = DBNull.Value;
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;



            if (model.DistrictCd != "")
            {

                P_DISTRICT_CD = Decimal.Parse(model.DistrictCd);
            }
            if (model.VDCMun != "")
            {

                P_VDC = (model.VDCMun);
            }
            if (model.Ward != "")
            {
                P_WARD_NO = Decimal.Parse(model.Ward);
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_GR_TARGET_SEARCH";
                    dt = service.GetDataTable(true, "PR_GR_NEW_APPLICANT_SEARCH",
                                                P_SESSION_ID,
                                                P_DISTRICT_CD,
                                                P_VDC,
                                                P_WARD_NO,
                                                SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
                                                P_ENTERED_BY,
                                                DBNull.Value);



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
            return dt;

        }
        public DataTable GetGrievanceEligibleSearch(CaseGrievanceModel model)
        {
            DataTable dt = new DataTable();

            Object P_TARGETING_ID = DBNull.Value;
            Object P_HOUSE_OWNER_ID = DBNull.Value;
            Object P_HOUSE_OWNER_NAME = DBNull.Value;

            Object P_HOUSE_OWNER_NAME_LOC = DBNull.Value;
            Object P_HOUSE_DEFINED_CD = DBNull.Value;
            Object P_INSTANCE_UNIQUE_SNO = DBNull.Value;

            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;
            Object P_TARGETING_DT_FROM = DBNull.Value;
            Object P_TARGETING_DT_TO = DBNull.Value;
            Object P_DAMAGE_GRADE_CD = DBNull.Value;
            Object P_BUILDING_CONDITION_CD = DBNull.Value;
            Object P_TECHSOLUTION_CD = DBNull.Value;
            Object P_District = DBNull.Value;
            Object P_VDC = DBNull.Value;

            Object P_WARD_NO = DBNull.Value;
            Object P_BUSINESS_RULE_CD = DBNull.Value;

            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
            if (model.DistrictCd != "")
            {

                P_District = Decimal.Parse(model.DistrictCd);
            }
            if (model.VDCMun != "")
            {

                P_VDC = (model.VDCMun);
            }
            if (model.Ward != "")
            {
                P_WARD_NO = Decimal.Parse(model.Ward);
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_GR_TARGET_SEARCH ";
                    dt = service.GetDataTable(true, "PR_GRIEVANCE_ELIGIBLE_SEARCH",
                                                P_TARGETING_ID,
                                                P_District,
                                                P_VDC,
                                                P_WARD_NO,
                                                SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
                                                DBNull.Value);



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
            return dt;

        }

        public DataTable GetGrievanceNonEligibleSearch(CaseGrievanceModel model)
        {
            DataTable dt = new DataTable();

            Object P_TARGETING_ID = DBNull.Value;
            Object P_HOUSE_OWNER_ID = DBNull.Value;
            Object P_HOUSE_OWNER_NAME = DBNull.Value;

            Object P_HOUSE_OWNER_NAME_LOC = DBNull.Value;
            Object P_HOUSE_DEFINED_CD = DBNull.Value;
            Object P_INSTANCE_UNIQUE_SNO = DBNull.Value;

            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;
            Object P_TARGETING_DT_FROM = DBNull.Value;
            Object P_TARGETING_DT_TO = DBNull.Value;
            Object P_DAMAGE_GRADE_CD = DBNull.Value;
            Object P_BUILDING_CONDITION_CD = DBNull.Value;
            Object P_TECHSOLUTION_CD = DBNull.Value;
            Object P_District = DBNull.Value;
            Object P_VDC = DBNull.Value;

            Object P_WARD_NO = DBNull.Value;
            Object P_BUSINESS_RULE_CD = DBNull.Value;

            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
            if (model.DistrictCd != "")
            {

                P_District = Decimal.Parse(model.DistrictCd);
            }
            if (model.VDCMun != "")
            {

                P_VDC = (model.VDCMun);
            }
            if (model.Ward != "")
            {
                P_WARD_NO = Decimal.Parse(model.Ward);
            }
            if (model.BatchID != "")
            {
                P_TARGETING_ID = Decimal.Parse(model.BatchID);
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_GR_TARGET_SEARCH ";
                    dt = service.GetDataTable(true, "PR_GR_NON_ELIGIBLE_SEARCH",
                                                P_TARGETING_ID,
                                                P_District,
                                                P_VDC,
                                                P_WARD_NO,
                                                SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
                                                DBNull.Value);



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
            return dt;

        }

        public QueryResult GrievanceTargetingSearchEligible(string session_id, out string excout, out string TotalTargeted)
        {
            QueryResult quryResult = new QueryResult();
            string error = string.Empty;
            CommonFunction commFunction = new CommonFunction();
            Object v_mode = "I";
            Object v_ip_address = CommonVariables.IPAddress;
            Object P_SESSION_ID = DBNull.Value;
            Object P_District = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object v_FISCAL_YR = DBNull.Value;
            Object P_Ward = DBNull.Value;
            Object totalNo = DBNull.Value;



            Object v_TARGET_BATCH_ID = DBNull.Value;

            Object v_ENTERED_BY = CommonVariables.UserName;

            if (session_id != "")
            {
                P_SESSION_ID = Decimal.Parse(session_id);
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_GRIEVANCE_TARGET_ENROLL";
                service.Begin();


                quryResult = service.SubmitChanges("PR_GRIEVANCE_TARGET_BATCH_INS",
                                                 P_SESSION_ID,
                                                 v_ENTERED_BY,
                                                 totalNo,
                                                 v_ip_address
                                                 );
            }
            catch (OracleException oex)
            {

                error = oex.Code.ConvertToString();
                ExceptionManager.AppendLog(oex);
            }

            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);

            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            TotalTargeted = totalNo.ToString();
            excout = error;
            return quryResult;
        }
        public DataTable GetBeneficiariesDeatils(CaseGrievanceModel searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            if (pageSize == "")
            {
                pageSize = "100";
            }

            Object P_TARGET_BATCH_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;


            Object P_APPROVED = searchParamObject.ddlApprove;
            Object P_APPROVED_DT = DBNull.Value;
            Object P_PAGE_SIZE = DBNull.Value;
            Object P_PAGE_INDEX = DBNull.Value;
            Object P_SORT_BY = DBNull.Value;
            Object P_SORT_ORDER = DBNull.Value;
            Object P_EXPORT_EXCEL = "N";
            Object P_LANG = "E";
            Object P_FILTER_WORD = DBNull.Value;
            if (searchParamObject.BatchID != "")
            {

                P_TARGET_BATCH_ID = searchParamObject.BatchID;
            }
            if (searchParamObject.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(searchParamObject.DistrictCd);
            }
            if (searchParamObject.VDCMunCd != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.VDCMunCd);
            }
            if (searchParamObject.Ward != "" && searchParamObject.Ward != null)
            {
                P_WARD_NO = Decimal.Parse(searchParamObject.Ward);
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_GR_TARGET_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_TR_GR_BENEFICIARY_SEARCH",
                       P_TARGET_BATCH_ID,
                       P_DISTRICT_CD,
                       P_VDC_MUN_CD,
                       P_WARD_NO,
                         P_APPROVED,
                         P_APPROVED_DT,
                        P_SORT_BY,
                        P_SORT_ORDER,
                        P_PAGE_SIZE,
                        P_PAGE_INDEX,
                        P_EXPORT_EXCEL,
                        P_LANG,
                        P_FILTER_WORD,
                        DBNull.Value
                    );


            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            return dt;

        }


        public QueryResult BeneficiaryApproved(string Targeting_batch_id, string HouseOwnerID)
        {
            QueryResult quryResult = new QueryResult();

            CommonFunction commFunction = new CommonFunction();
            Object v_mode = "I";
            Object v_ip_address = CommonVariables.IPAddress;

            Object P_SESSION_ID = DBNull.Value;
            Object v_FISCAL_YR = DBNull.Value;

            Object P_TARGET_BATCH_ID = DBNull.Value;

            Object v_ENTERED_BY = CommonVariables.UserName;

            if (Targeting_batch_id != "")
            {
                P_TARGET_BATCH_ID = Decimal.Parse(Targeting_batch_id);
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_GRIEVANCE_TARGET_ENROLL";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_CG_ENROLL_INSERT",
                                                  P_TARGET_BATCH_ID,
                                                  HouseOwnerID,
                                                    v_ENTERED_BY,
                                                    v_ip_address
                                                  );

            }
            catch (OracleException ox)
            {

                ExceptionManager.AppendLog(ox);
            }
            catch (Exception ex)
            {

                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }


            return quryResult;
        }
        public QueryResult BeneficiaryApprovedAll(string Targeting_batch_id, string HouseOwnerID)
        {
            QueryResult quryResult = new QueryResult();

            CommonFunction commFunction = new CommonFunction();
            Object v_mode = "I";
            Object v_ip_address = CommonVariables.IPAddress;
            Object P_TARGET_BATCH_ID = DBNull.Value;

            Object v_ENTERED_BY = CommonVariables.UserName;

            if (Targeting_batch_id != "")
            {
                P_TARGET_BATCH_ID = Decimal.Parse(Targeting_batch_id);
            }

            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_GRIEVANCE_TARGET_ENROLL";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_CG_ENROLL_ALL",
                                                  P_TARGET_BATCH_ID,
                                                    v_ENTERED_BY,
                                                    v_ip_address
                                                  );

            }
            catch (OracleException ox)
            {

                ExceptionManager.AppendLog(ox);
            }
            catch (Exception ex)
            {

                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }


            return quryResult;
        }
    }
}
