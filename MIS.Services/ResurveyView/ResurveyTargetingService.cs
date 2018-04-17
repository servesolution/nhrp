using EntityFramework;
using ExceptionHandler;
using MIS.Models.Resurvey;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace MIS.Services.ResurveyView
{
    public class ResurveyTargetingService
    {
        

        public DataTable GetNewApplicantSurvey(ResurveyTargetingModel objTargetingSearch)
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
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;



            if (objTargetingSearch.DistrictCd != "")
            {

                P_DISTRICT_CD = Decimal.Parse(objTargetingSearch.DistrictCd);
            }
            if (objTargetingSearch.VDCMun != "")
            {

                P_VDC = (objTargetingSearch.CurrentVdcMunCD);
            }
            if (objTargetingSearch.WardNo != "")
            {
                P_WARD_NO = Decimal.Parse(objTargetingSearch.WardNo);
            }

            if (objTargetingSearch.SORT_BY != "")
            {
                SortBy = objTargetingSearch.SORT_BY;
            }
            if (objTargetingSearch.SORT_ORDER != "")
            {
                SortOrder = objTargetingSearch.SORT_ORDER;
            }
            if (objTargetingSearch.PAGE_SIZE != "")
            {
                PageSize = objTargetingSearch.PAGE_SIZE;
            }
            if (objTargetingSearch.PAGE_INDEX != "")
            {
                PageIndex = objTargetingSearch.PAGE_INDEX;
            }
            if (objTargetingSearch.EXPORT_EXCEL != "")
            {
                ExportExcel = objTargetingSearch.EXPORT_EXCEL;
            }
            if (objTargetingSearch.LANG != "")
            {
                Lang = objTargetingSearch.LANG;
            }
            if (objTargetingSearch.FILTER_WORD != "")
            {
                FilterWord = objTargetingSearch.FILTER_WORD;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_RES_BENEF";
                    dt = service.GetDataTable(true, "PR_RESURVEY_NEW_SEARCH",
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



        public DataTable GetRetrofitingTargetingSearchDetail(ResurveyTargetingModel objTargetingSearch)
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



            if (objTargetingSearch.Targetting_ID != "")
            {
                P_TARGETING_ID = Decimal.Parse(objTargetingSearch.Targetting_ID);
            }
            if (objTargetingSearch.House_Owner_ID != "")
            {
                P_HOUSE_OWNER_ID = Decimal.Parse(objTargetingSearch.House_Owner_ID);
            }

            if (objTargetingSearch.House_Owner_Name != "")
            {
                P_HOUSE_OWNER_NAME = (objTargetingSearch.House_Owner_Name);
            }
            if (objTargetingSearch.House_Owner_Name_Loc != "")
            {
                P_HOUSE_OWNER_NAME_LOC = (objTargetingSearch.House_Owner_Name_Loc);
            }
            if (objTargetingSearch.HOUSE_DEFINED_CD != "")
            {
                P_HOUSE_DEFINED_CD = Decimal.Parse(objTargetingSearch.HOUSE_DEFINED_CD);
            }
            if (objTargetingSearch.INSTANCE_UNIQUE_SNO != "")
            {
                P_INSTANCE_UNIQUE_SNO = Decimal.Parse(objTargetingSearch.INSTANCE_UNIQUE_SNO);
            }
            if (objTargetingSearch.Building_Structure_No != "")
            {
                P_BUILDING_STRUCTURE_NO = Decimal.Parse(objTargetingSearch.Building_Structure_No);
            }

            if (objTargetingSearch.DAMAGE_GRADE_CD != "")
            {
                P_DAMAGE_GRADE_CD = Decimal.Parse(objTargetingSearch.DAMAGE_GRADE_CD);
            }
            if (objTargetingSearch.TARGETING_DT_FROM != "")
            {
                P_TARGETING_DT_FROM = Decimal.Parse(objTargetingSearch.TARGETING_DT_FROM);
            }

            if (objTargetingSearch.TARGETING_DT_TO != "")
            {
                P_TARGETING_DT_TO = Decimal.Parse(objTargetingSearch.TARGETING_DT_TO);
            }
            if (objTargetingSearch.BUILDING_CONDITION_CD != "")
            {
                P_BUILDING_CONDITION_CD = Decimal.Parse(objTargetingSearch.BUILDING_CONDITION_CD);
            }
            if (objTargetingSearch.TECHSOLUTION_CD != "")
            {
                P_TECHSOLUTION_CD = Decimal.Parse(objTargetingSearch.TECHSOLUTION_CD);
            }
            if (objTargetingSearch.DistrictCd != "")
            {

                P_District = Decimal.Parse(objTargetingSearch.DistrictCd);
            }
            if (objTargetingSearch.VDCMun != "")
            {

                P_VDC = (objTargetingSearch.VDCMun);
            }
            if (objTargetingSearch.WardNo != "")
            {
                P_WARD_NO = Decimal.Parse(objTargetingSearch.WardNo);
            }

            if (objTargetingSearch.BUSINESS_RULE != "")
            {
                P_BUSINESS_RULE_CD = Decimal.Parse(objTargetingSearch.BUSINESS_RULE);
            }



            if (objTargetingSearch.SORT_BY != "")
            {
                SortBy = objTargetingSearch.SORT_BY;
            }
            if (objTargetingSearch.SORT_ORDER != "")
            {
                SortOrder = objTargetingSearch.SORT_ORDER;
            }
            if (objTargetingSearch.PAGE_SIZE != "")
            {
                PageSize = objTargetingSearch.PAGE_SIZE;
            }
            if (objTargetingSearch.PAGE_INDEX != "")
            {
                PageIndex = objTargetingSearch.PAGE_INDEX;
            }
            if (objTargetingSearch.EXPORT_EXCEL != "")
            {
                ExportExcel = objTargetingSearch.EXPORT_EXCEL;
            }
            if (objTargetingSearch.LANG != "")
            {
                Lang = objTargetingSearch.LANG;
            }
            if (objTargetingSearch.FILTER_WORD != "")
            {
                FilterWord = objTargetingSearch.FILTER_WORD;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_RES_BENEF ";
                    dt = service.GetDataTable(true, "PR_RETROFITING_ELIGIBLE_SEARCH",
                                                P_TARGETING_ID,
                                                P_HOUSE_OWNER_ID,
                                                P_HOUSE_OWNER_NAME,
                                                P_HOUSE_OWNER_NAME_LOC,
                                                P_HOUSE_DEFINED_CD,
                                                P_INSTANCE_UNIQUE_SNO,
                                                P_TARGETING_DT_FROM,
                                                P_TARGETING_DT_TO,
                                                P_District,
                                                P_VDC,
                                                P_WARD_NO,
                                                P_BUILDING_CONDITION_CD,
                                                P_DAMAGE_GRADE_CD,
                                                P_TECHSOLUTION_CD,
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

        public QueryResult TargetingSearchEligible(string session_id, out string excout, out string TotalTargeted)
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
                service.PackageName = "PKG_RES_ENROLL_TARGET";
                service.Begin();


                quryResult = service.SubmitChanges("PR_RES_NHRS_TARGET_BATCH_INS",
                                                  P_SESSION_ID, 
                                                  v_ENTERED_BY,
                                                  DBNull.Value,
                                                  DBNull.Value
                                                  
                                                  );
            }
            catch (OracleException oex)
            {

                error = oex.Code.ConvertToString();
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
            if (searchParamObject.CurrentVdcMunCD != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(searchParamObject.CurrentVdcMunCD);
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
                service.PackageName = "PKG_RES_ENROLL_TARGET";
                service.Begin();
                dt = service.GetDataTable(true, "PR_TARGETED_BENEFICIARY_SEARCH",
                        P_TARGET_BATCH_ID,
                        P_DISTRICT_CD,
                        P_VDC_MUN_CD,
                        P_WARD_NO,
                        P_APPROVED,
                        DBNull.Value,// P_APPROVED_DT,
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
                service.PackageName = "pkg_res_enroll_target";
                service.Begin();
                quryResult = service.SubmitChanges("PR_RES_NHRS_ENROLL_ALL",
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
                service.PackageName = "pkg_res_enroll_target";
                service.Begin();
                quryResult = service.SubmitChanges("PR_RES_NHRS_ENROLL_INSERT",
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



    
    }
}
