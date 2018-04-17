using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Services.Core;
using MIS.Models.BusinessCalcualtion;
using ExceptionHandler;
using EntityFramework;
using System.Data.OracleClient;


namespace MIS.Services.BusinessCalculation
{
    public class BeneficiaryService
    {
        CommonFunction commFunction = new CommonFunction();



        public DataTable GetBeneficiariesDeatilsByDate(BeneficiarySearch searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            if (pageSize == "")
            {
                pageSize = "100";
            }

            Object P_TARGET_BATCH_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;


            Object P_APPROVED = DBNull.Value;
            Object P_APPROVED_DT= DBNull.Value;

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
            if (searchParamObject.ddlApprove != "")
            {
                P_APPROVED = searchParamObject.ddlApprove;
            }
            if (searchParamObject.approve_date != "")
            {
                P_APPROVED_DT = searchParamObject.approve_date.ToDateTime("dd-mm-yy");
            }
            if (searchParamObject.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(searchParamObject.DistrictCd);
            }
            if (searchParamObject.VDCMunCd != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.VDCMunCd);
            }
            if (searchParamObject.WardNo != "" && searchParamObject.WardNo != null)
            {
                P_WARD_NO = Decimal.Parse(searchParamObject.WardNo);
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_TARGETED_BENEFICIARY_SEARCH",
                        P_TARGET_BATCH_ID,
                        P_DISTRICT_CD,
                        P_VDC_MUN_CD,
                        P_WARD_NO,
                        P_APPROVED,
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

        public DataTable GetRetroBeneficiariesDeatilsByDate(BeneficiarySearch searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            if (pageSize == "")
            {
                pageSize = "100";
            }

            Object P_TARGET_BATCH_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;


            Object P_APPROVED = DBNull.Value;
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
            if (searchParamObject.ddlApprove != "")
            {
                P_APPROVED = searchParamObject.ddlApprove;
            }
            if (searchParamObject.approve_date != "")
            {
                P_APPROVED_DT = searchParamObject.approve_date.ToDateTime("dd-mm-yy");
            }
            if (searchParamObject.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(searchParamObject.DistrictCd);
            }
            if (searchParamObject.VDCMunCd != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.VDCMunCd);
            }
            if (searchParamObject.WardNo != "" && searchParamObject.WardNo != null)
            {
                P_WARD_NO = Decimal.Parse(searchParamObject.WardNo);
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_NHRS_RETRO_ENROLL_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_RETRO_TARGETED_BENE_SEARCH",
                        P_TARGET_BATCH_ID,
                        P_DISTRICT_CD,
                        P_VDC_MUN_CD,
                        P_WARD_NO,
                        P_APPROVED,
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
        public DataTable GetOwner(string House_Owner_ID)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_NHRS_GET_OWNERDETAIL",
                        House_Owner_ID,
                        DBNull.Value
                    );


            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
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

        public DataTable GetBeneficiariesDeatilsEnroll(BeneficiarySearch searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            if (pageSize == "")
            {
                pageSize = "100";
            }

            Object P_TARGET_BATCH_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;


            Object P_APPROVED = DBNull.Value;
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
            if (searchParamObject.ddlApprove != "")
            {
                P_APPROVED = searchParamObject.ddlApprove;
            }
            if (searchParamObject.approve_date != "")
            {
                P_APPROVED_DT = (searchParamObject.approve_date.ToDateTime("YYYY-MM-DD"));
            }
            if (searchParamObject.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(searchParamObject.DistrictCd);
            }
            if (searchParamObject.VDCMunCd != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.VDCMunCd);
            }
            if (searchParamObject.WardNo != "" && searchParamObject.WardNo != null)
            {
                P_WARD_NO = Decimal.Parse(searchParamObject.WardNo);
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_ENROLL_BENEFICIARY_SEARCH",
                        P_TARGET_BATCH_ID,
                        P_DISTRICT_CD,
                        P_VDC_MUN_CD,
                        P_WARD_NO,
                        P_APPROVED,
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
        public DataTable GetBeneficiariesDeatils(BeneficiarySearch searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            if (pageSize == "")
            {
                pageSize = "100";
            }

            Object P_TARGET_BATCH_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;


            Object P_APPROVED = searchParamObject.ddlApprove ;
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
            if (searchParamObject.approve_date != "")
            {
                P_APPROVED_DT = searchParamObject.approve_date;
            }
            if (searchParamObject.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(searchParamObject.DistrictCd);
            }
            if (searchParamObject.VDCMunCd != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.VDCMunCd);
            }
            if (searchParamObject.WardNo != "" && searchParamObject.WardNo != null)
            {
                P_WARD_NO = Decimal.Parse(searchParamObject.WardNo);
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_TARGETED_BENEFICIARY_SEARCH",
                       P_TARGET_BATCH_ID,
                       P_DISTRICT_CD,
                       P_VDC_MUN_CD,
                       P_WARD_NO,
                         P_APPROVED,
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
        public DataTable GetRetroBeneficiariesDeatils(BeneficiarySearch searchParamObject, string pageIndex = "1", string pageSize = "100")
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
            if (searchParamObject.approve_date != "")
            {
                P_APPROVED_DT = searchParamObject.approve_date;
            }
            if (searchParamObject.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(searchParamObject.DistrictCd);
            }
            if (searchParamObject.VDCMunCd != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.VDCMunCd);
            }
            if (searchParamObject.WardNo != "" && searchParamObject.WardNo != null)
            {
                P_WARD_NO = Decimal.Parse(searchParamObject.WardNo);
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_NHRS_RETRO_ENROLL_SEARCH";
                service.Begin();
                dt = service.GetDataTable(true, "PR_RETRO_TARGETED_BENE_SEARCH",
                       P_TARGET_BATCH_ID,
                       P_DISTRICT_CD,
                       P_VDC_MUN_CD,
                       P_WARD_NO,
                         P_APPROVED,
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
        public DataTable GetBeneficiaries(BeneficiarySearch searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            if (pageSize == "")
            {
                pageSize = "100";
            }
            Object v_fiscal_yr = DBNull.Value;
            Object V_ASSIST_TYPE_CD = DBNull.Value;
            Object V_BATCH_ID = DBNull.Value;
            Object V_DISTRICT_CD = DBNull.Value;
            Object V_VDC_CD = DBNull.Value;
            Object V_WARD_NO = DBNull.Value;
            Object V_HOUSEHOLD_ID_FROM = DBNull.Value;
            Object V_HOUSEHOLD_ID_TO = DBNull.Value;
            Object V_FORM_NO_FROM = DBNull.Value;
            Object V_FORM_NO_TO = DBNull.Value;
            Object V_MEMBER_ID = DBNull.Value;
            Object V_BENIFICIARY_FLAG = "N";
            Object V_PAGE_SIZE = pageSize;
            Object V_PAGE_INDEX = pageIndex;
            Object V_SORT_BY = "HOUSEHOLD_ID";
            Object V_SORT_ORDER = "ASC";
            if (searchParamObject != null)
            {
                v_fiscal_yr = searchParamObject.FiscalYear;
                V_ASSIST_TYPE_CD = ConstantVariables.EducationGrantSATID;
                V_BATCH_ID = searchParamObject.BatchID;
                V_DISTRICT_CD = GetData.GetCodeFor(DataType.District, searchParamObject.DistrictCd);
                V_VDC_CD = GetData.GetCodeFor(DataType.VdcMun, searchParamObject.VDCMunCd);
                V_WARD_NO = searchParamObject.WardNo;
                V_HOUSEHOLD_ID_FROM = commFunction.GetCodeFromDataBase(searchParamObject.HouseholdIDFrom, "MIS_HOUSEHOLD_INFO", "HOUSEHOLD_ID");
                V_HOUSEHOLD_ID_TO = commFunction.GetCodeFromDataBase(searchParamObject.HouseholdIDTo, "MIS_HOUSEHOLD_INFO", "HOUSEHOLD_ID");
                V_FORM_NO_FROM = searchParamObject.FormNoFrom;
                V_FORM_NO_TO = searchParamObject.FormNoTo;
                V_MEMBER_ID = searchParamObject.MemberID;
                V_BENIFICIARY_FLAG = searchParamObject.TargetType;
                V_PAGE_SIZE = searchParamObject.PageSize.ConvertToString() == "" ? pageSize : searchParamObject.PageSize.ConvertToString();
                V_SORT_ORDER = searchParamObject.Order_By.ConvertToString() == "" ? "ASC" : searchParamObject.Order_By.ConvertToString();
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_SAT";
                service.Begin();
                dt = service.GetDataTable(true, "PR_EDU_BENIFICIARY_SEARCH",
                        v_fiscal_yr,
                        V_ASSIST_TYPE_CD,
                        V_BATCH_ID,
                        V_DISTRICT_CD,
                        V_VDC_CD,
                        V_WARD_NO,
                        V_HOUSEHOLD_ID_FROM,
                        V_HOUSEHOLD_ID_TO,
                        V_FORM_NO_FROM,
                        V_FORM_NO_TO,
                        V_MEMBER_ID,
                        V_BENIFICIARY_FLAG,
                        V_PAGE_SIZE,
                        V_PAGE_INDEX,
                        V_SORT_BY,
                        V_SORT_ORDER,
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
 
        public DataTable getBeneficiaryReport(BeneficiarySearch objSearch)
        {
            Object P_DISTRICT_CD=DBNull.Value;
            Object P_VDC_CD=DBNull.Value;
            Object P_WARD=DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            DataTable dt = new DataTable();
            if (objSearch.DistrictCd != "")
            {
                P_DISTRICT_CD=objSearch.DistrictCd;
            }
            if (objSearch.VDCMunCd != "")
            {
                P_VDC_CD = objSearch.VDCMunCd;
            }
            if (objSearch.WardNo != "")
            {
                P_WARD = objSearch.WardNo;
            }
            if (objSearch.BatchID != "")
            {
                P_BATCH_ID = objSearch.BatchID;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_BENEFICIARY_REPORT";
                    dt = service.GetDataTable(true, "PR_BENEFICIARY_LIST_VDC",
                                                P_DISTRICT_CD,
                                                P_VDC_CD,
                                                P_WARD,
                                                P_BATCH_ID,
                                                DBNull.Value
                                               );

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
        public DataTable GetVdcSecretary(string EmployeeCode)
        {
            DataTable dtbl = null;
            try
            {

                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    string cmdText = String.Format("select SEC.EMPLOYEE_CD, SEC.FIRST_NAME_ENG, SEC.MIDDLE_NAME_ENG, SEC.LAST_NAME_ENG , SEC.FULL_NAME_ENG,EMP.PER_DISTRICT_CD, DIS.DESC_ENG, EMP.PER_VDC_MUN_CD, VDC.DESC_ENG, EMP.PER_WARD_NO,PS.DESC_ENG FROM NHRS_VDC_SECRETARY SEC JOIN COM_EMPLOYEE EMP ON SEC.EMPLOYEE_CD=EMP.EMPLOYEE_CD JOIN MIS_DISTRICT DIS ON EMP.PER_DISTRICT_CD=DIS.DISTRICT_CD JOIN MIS_VDC_MUNICIPALITY VDC ON EMP.PER_VDC_MUN_CD=VDC.VDC_MUN_CD INNER JOIN MIS_POSITION PS ON PS.POSITION_CD=EMP.POSITION_CD where SEC.EMPLOYEE_CD='" + EmployeeCode + "'");
                    try
                    {
                        dtbl = service.GetDataTable(cmdText, null);
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
            catch (Exception ex)
            {
               
                ExceptionManager.AppendLog(ex);
            }
            return dtbl;
        }

        #region Report
        public DataTable GetBeneficiaryDataForReport(BeneficiarySearch objSearch)
        {
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_CD = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_BATCH_ID = DBNull.Value;
            Object P_APPROVE = DBNull.Value;
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(objSearch.DistrictCd))
            {
                P_DISTRICT_CD = objSearch.DistrictCd;
            }
            if (!string.IsNullOrEmpty(objSearch.VDCMunCd))
            {
                P_VDC_CD = objSearch.VDCMunCd;
            }
            if (!string.IsNullOrEmpty(objSearch.WardNo))
            {
                P_WARD = objSearch.WardNo;
            }
            if (!string.IsNullOrEmpty(objSearch.BatchID ))
            {
                P_BATCH_ID = objSearch.BatchID;
            }
            if (!string.IsNullOrEmpty(objSearch.ddlApprove))
            {
                P_APPROVE = objSearch.ddlApprove;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                   // service.PackageName = "PKG_BENEFICIARY_REPORT";
                    dt = service.GetDataTable(true, "PR_GET_HOUSE_OWNER_DETAIL",
                                                Utils.ToggleLanguage("E", "N"),
                                                P_DISTRICT_CD,
                                                P_VDC_CD,
                                                P_WARD,
                                                P_BATCH_ID,
                                                 P_APPROVE,
                                                DBNull.Value
                                               );

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
        #endregion
    }
}
