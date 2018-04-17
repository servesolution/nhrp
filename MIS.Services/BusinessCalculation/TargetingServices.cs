using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using MIS.Models.BusinessCalcualtion;
using MIS.Services.Core;
using System.Data.OracleClient;
using ExceptionHandler;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using System.IO;

namespace MIS.Services.BusinessCalculation
{
    public class TargetingServices
    {

        public DataSet BuildingStructuredDetail(string HouseDefinedCd)
        {
            DataSet dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataSetOracle(true, "PR_NHRS_TARGETING_POPUP", HouseDefinedCd.ToDecimal(),DBNull.Value,DBNull.Value,DBNull.Value);

                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
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
       //public DataTable OtherHousesDetail(string HouseDefCd)
       // {
       //     DataTable dt = null;
       //     string cmdText = "";
       //     using (ServiceFactory service = new ServiceFactory())
       //     {
       //         //cmdText = "SELECT OTHER_HOUSE_ID, BUILDING_CONDITION_CD, FROM NHRS_OTHER_HOUSE_DTL WHERE HOUSE_ID='" + HouseDefCd + "'";
       //         cmdText="SELECT OTHER_RESIDENCE_ID,OTHER_DISTRICT_CD, DIS.DESC_ENG DISTRICT_ENG , DIS.DESC_LOC DISTRICT_LOC ,OTHER_VDC_MUN_CD,VDC.DESC_ENG VDC_MUN_ENG , VDC.DESC_LOC VDC_MUN_LOC , OTHER_WARD_NO ,OTHER_AREA_ENG, OTHER_AREA_LOC,BUILDING_CONDITION_CD FROM NHRS.NHRS_HOWNER_OTH_RESIDENCE_DTL MST JOIN MIS_DISTRICT DIS ON MST.DISTRICT_CD = DIS.DISTRICT_CD JOIN MIS_VDC_MUNICIPALITY VDC  ON MST.VDC_MUN_CD = VDC.VDC_MUN_CD WHERE 1=1 AND HOUSE_OWNER_ID = '||P_HOUSE_OWNER_ID ";
               
       //         try
       //         {
       //             service.Begin();
       //             dt = service.GetDataTable(new
       //             {
       //                 query = cmdText,
       //                 args = new
       //                 {

       //                 }
       //             });
       //         }
       //         catch (Exception ex)
       //         {
       //             ExceptionManager.AppendLog(ex);
       //         }
       //         finally
       //         {
       //             if (service.Transaction != null)
       //             {
       //                 service.End();
       //             }
       //         }
       //     }
       //     return dt;
       // }
        //public DataTable HownerDtl(string HownId)
        //{
        //    DataTable dt = null;
        //    string cmdText = "";
        //    using (ServiceFactory service = new ServiceFactory())
        //    {
        //        cmdText = "SELECT HOUSE_OWNER_ID, FIRST_NAME_ENG,MIDDLE_NAME_ENG,LAST_NAME_ENG,GENDER_CD FROM NHRS_HOUSE_OWNER_DTL WHERE HOUSE_OWNER_ID='" + HownId + "'";
        //        //mdText = "SELECT HOUSE_OWNER_ID ,SNO ,MEMBER_ID,FIRST_NAME_ENG, MIDDLE_NAME_ENG,LAST_NAME_ENG ,FULL_NAME_ENG ,FIRST_NAME_LOC ,MIDDLE_NAME_LOC, LAST_NAME_LOC ,FULL_NAME_LOC  ,MEMBER_PHOTO_ID ,GENDER_CD  , MARITAL_STATUS_CD , HOUSEHOLD_HEAD, HOUSEHOLD_ID    HOUSEHOLD_DEFINED_CD FROM NHRS.NHRS_HOUSE_OWNER_DTL HOD WHERE HOUSE_OWNER_ID = '||P_HOUSE_OWNER_ID; DBMS_OUTPUT.PUT_LINE (vSQL_OWNER); OPEN P_Out_OWNER for vSQL_OWNER;

        //        try
        //        {
        //            service.Begin();
        //            dt = service.GetDataTable(new
        //            {
        //                query = cmdText,
        //                args = new
        //                {

        //                }
        //            });
        //        }
        //        catch (Exception ex)
        //        {
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
        //    return dt;
        //}

        public DataTable GetTargetingSearchDetail(TargetingSearch objTargetingSearch)
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
                    service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH ";
                    dt = service.GetDataTable(true, "PR_NHRS_ELIGIBLE_SEARCH",
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


        public DataTable GetRetrofitingTargetingSearchDetail(TargetingSearch objTargetingSearch)
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
                    service.PackageName = "PKG_NHRS_RETRO_ENROLL_SEARCH ";
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


        public DataTable GetRetrofitingNewApplicantSearchDetail(TargetingSearch objTargetingSearch)
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

                P_VDC = (objTargetingSearch.VDCMun);
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
                    service.PackageName = "PKG_NHRS_RETRO_ENROLL_SEARCH ";
                    dt = service.GetDataTable(true, "PR_RETROFITING_NEW_SEARCH",
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
        public DataTable GetNewApplicantSearchDetail(TargetingSearch objTargetingSearch)
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

                P_VDC = (objTargetingSearch.VDCMun);
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
                    service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH ";
                    dt = service.GetDataTable(true, "PR_NHRS_NEW_APPLICANT_SEARCH",
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


        public DataTable GetNONTargetingSearchDetail(TargetingSearch objTargetingSearch)
        {
            DataTable dt = new DataTable();

            Object P_TARGETING_ID = DBNull.Value;
            Object P_HOUSE_OWNER_ID = DBNull.Value;
            Object P_HOUSE_OWNER_NAME = DBNull.Value;

            Object P_HOUSE_OWNER_NAME_LOC = DBNull.Value;
            Object P_HOUSE_DEFINED_CD = DBNull.Value;
            Object P_INSTANCE_UNIQUE_SNO = DBNull.Value;

            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;

            Object P_DAMAGE_GRADE_CD = DBNull.Value;
            Object P_BUILDING_CONDITION_CD = DBNull.Value;
            Object P_TECHSOLUTION_CD = DBNull.Value;

            Object P_District = DBNull.Value;
            Object P_VDC = DBNull.Value;

            Object P_Ward = DBNull.Value;
            Object P_INTERVIEW_DT_TO = DBNull.Value;
            Object P_INTERVIEW_DT_FROM = DBNull.Value;
            Object P_BUSINESS_RULE_CD = DBNull.Value;

            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
            Object P_ENTERED_BY = CommonVariables.UserName;
            Object P_SESSION_ID = DBNull.Value;

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
                P_Ward = Decimal.Parse(objTargetingSearch.WardNo);
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
            if (objTargetingSearch.TARGETING_DT_TO != "")
            {
                P_INTERVIEW_DT_TO = objTargetingSearch.TARGETING_DT_TO;
            }
            if (objTargetingSearch.TARGETING_DT_FROM != "")
            {
                P_INTERVIEW_DT_FROM = objTargetingSearch.TARGETING_DT_FROM;
            }
            
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH";
                    dt = service.GetDataTable(true, "PR_NHRS_NON_ELIGIBLE_SEARCH",

                                                P_SESSION_ID,
                                                P_HOUSE_OWNER_ID,
                                                P_HOUSE_OWNER_NAME,
                                                P_HOUSE_OWNER_NAME_LOC,
                                                P_HOUSE_DEFINED_CD,
                                                P_INSTANCE_UNIQUE_SNO,
                                                P_INTERVIEW_DT_FROM,
                                                P_INTERVIEW_DT_TO,
                                                 P_District,
                                                P_VDC,
                                                P_Ward,
                                                SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
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
        public DataTable GetRetrofitingNONTargetingSearchDetail(TargetingSearch objTargetingSearch)
        {
            DataTable dt = new DataTable();

            Object P_TARGETING_ID = DBNull.Value;
            Object P_HOUSE_OWNER_ID = DBNull.Value;
            Object P_HOUSE_OWNER_NAME = DBNull.Value;

            Object P_HOUSE_OWNER_NAME_LOC = DBNull.Value;
            Object P_HOUSE_DEFINED_CD = DBNull.Value;
            Object P_INSTANCE_UNIQUE_SNO = DBNull.Value;

            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;

            Object P_DAMAGE_GRADE_CD = DBNull.Value;
            Object P_BUILDING_CONDITION_CD = DBNull.Value;
            Object P_TECHSOLUTION_CD = DBNull.Value;

            Object P_District = DBNull.Value;
            Object P_VDC = DBNull.Value;

            Object P_Ward = DBNull.Value;
            Object P_INTERVIEW_DT_TO = DBNull.Value;
            Object P_INTERVIEW_DT_FROM = DBNull.Value;
            Object P_BUSINESS_RULE_CD = DBNull.Value;

            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
            Object P_ENTERED_BY = CommonVariables.UserName;
            Object P_SESSION_ID = DBNull.Value;

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
                P_Ward = Decimal.Parse(objTargetingSearch.WardNo);
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
            if (objTargetingSearch.TARGETING_DT_TO != "")
            {
                P_INTERVIEW_DT_TO = objTargetingSearch.TARGETING_DT_TO;
            }
            if (objTargetingSearch.TARGETING_DT_FROM != "")
            {
                P_INTERVIEW_DT_FROM = objTargetingSearch.TARGETING_DT_FROM;
            }

            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_RETRO_ENROLL_SEARCH";
                    dt = service.GetDataTable(true, "PR_RETROFITING_NON_ELIGIBLE_SEARCH",

                                                P_SESSION_ID,
                                                P_HOUSE_OWNER_ID,
                                                P_HOUSE_OWNER_NAME,
                                                P_HOUSE_OWNER_NAME_LOC,
                                                P_HOUSE_DEFINED_CD,
                                                P_INSTANCE_UNIQUE_SNO,
                                                P_INTERVIEW_DT_FROM,
                                                P_INTERVIEW_DT_TO,
                                                 P_District,
                                                P_VDC,
                                                P_Ward,
                                                SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
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
                service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_ENROLLMENT_INSERT",
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
                service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_ENROLLMENT_INSERT_ALL",
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

        public QueryResult BeneficiaryRetroApproved(string Targeting_batch_id, string HouseOwnerID)
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
                service.PackageName = "PKG_NHRS_RETRO_TARGET_ENROLL";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_RETRO_ENROLL",
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
        public QueryResult BeneficiaryRetroApprovedAll(string Targeting_batch_id, string HouseOwnerID)
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
                service.PackageName = "PKG_NHRS_RETRO_TARGET_ENROLL";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_RETRO_ENROLL_ALL",
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
                service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                service.Begin();


                quryResult = service.SubmitChanges("PR_NHRS_TARGET_BATCH_INSERT",
                                                  P_SESSION_ID,
                                                  v_ENTERED_BY,
                                                  totalNo,
                                                  v_ip_address
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

        public QueryResult RectofitingTargetingSearchEligible(string session_id, out string excout, out string TotalTargeted)
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
                service.PackageName = "PKG_NHRS_RETRO_TARGET_ENROLL";
                service.Begin();


                quryResult = service.SubmitChanges("PR_NHRS_RETRO_TARGET_BATCH_INS",
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

        public QueryResult TargetingEligible(DataTable result)
        {
            QueryResult quryResult = new QueryResult();
            foreach (DataRow dtTargeting in result.Rows)
            {
                CommonFunction commFunction = new CommonFunction();
                Object v_IP_Address = CommonVariables.IPAddress;
                Object v_mode = "I";
                Object v_SESSION_ID = DBNull.Value;
                Object v_FISCAL_YR = DBNull.Value;
                Object P_HOUSE_OWNER_ID = DBNull.Value;
                Object P_HOUSE_DEFINED_CD = DBNull.Value;
                Object P_INSTANCE_UNIQUE_SNO = DBNull.Value;
                Object P_BUILDING_STRUCTURE_NO = DBNull.Value;
                Object P_DAMAGE_GRADE_CD = DBNull.Value;
                Object P_TECHSOLUTION_CD = DBNull.Value;
                Object p_BUILDING_CONDITION_CD = DBNull.Value;
                Object v_ENTERED_DT = DBNull.Value;
                Object v_ENTERED_DT_LOC = DBNull.Value;
                Object v_ENTERED_BY = CommonVariables.UserName;
                //if (session_id != "")
                //{
                //    v_SESSION_ID = Decimal.Parse(session_id);
                //}
                //if (dtTargeting[0].ToString() != "")
                //{
                //    v_SESSION_ID = (dtTargeting[0].ToString());
                //}
                if (dtTargeting[0].ToString() != "")
                {
                    P_HOUSE_OWNER_ID = (dtTargeting[0].ToString());
                }

                if (dtTargeting[1].ToString() != "")
                {
                    P_HOUSE_DEFINED_CD = (dtTargeting[1].ToString());
                }
                if (dtTargeting[3].ToString() != "")
                {
                    P_INSTANCE_UNIQUE_SNO = (dtTargeting[3].ToString());
                }

                if (dtTargeting[21].ToString() != "")
                {
                    P_BUILDING_STRUCTURE_NO = (dtTargeting[21].ToString());
                }
                //if (dtTargeting[9].ToString() != "")
                //{
                //    P_DAMAGE_GRADE_CD = (dtTargeting[9].ToString());
                //}
                //if (dtTargeting[15].ToString() != "")
                //{
                //    P_TECHSOLUTION_CD = (dtTargeting[15].ToString());
                //}
                //if (dtTargeting[12].ToString() != "")
                //{
                //    p_BUILDING_CONDITION_CD = (dtTargeting[12].ToString());
                //}

                ServiceFactory service = new ServiceFactory();
                try
                {
                    string resPackageName = service.PackageName;
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    service.Begin();


                    quryResult = service.SubmitChanges("PR_NHRS_TARGET_TEMP",
                                                         v_mode,
                                                       //v_SESSION_ID,
                                                        P_HOUSE_OWNER_ID,
                                                       P_HOUSE_DEFINED_CD,
                                                        P_INSTANCE_UNIQUE_SNO,
                                                        P_BUILDING_STRUCTURE_NO,
                                                        DBNull.Value,
                                                        //DBNull.Value,
                                                        //DBNull.Value,
                                                        //P_DAMAGE_GRADE_CD,
                                                       // P_TECHSOLUTION_CD,
                                                        //p_BUILDING_CONDITION_CD,
                                                        DBNull.Value,
                                                       v_ENTERED_BY,
                                                        v_ENTERED_DT,
                                                        v_ENTERED_DT_LOC,
                                                        v_IP_Address.ToString()
                                                        );
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
            return quryResult;
        }

        public DataTable GetUntargeted(TargetingSearch searchParamObject, string pageIndex = "1", string pageSize = "100")
        {
            CommonFunction commFunction = new CommonFunction();
            if (pageSize == "")
            {
                pageSize = "100";
            }
            Object v_fiscal_yr = DBNull.Value;
            Object V_ASSIST_TYPE_CD = DBNull.Value;
            Object V_RULE_FLAG = DBNull.Value;
            Object V_DISTRICT_CD = DBNull.Value;
            Object V_Entered_By = CommonVariables.UserName;
            Object V_TARGET_CD = DBNull.Value;
            if (searchParamObject != null)
            {
                v_fiscal_yr = searchParamObject.ddl_FiscalYear;
                V_ASSIST_TYPE_CD = ConstantVariables.EducationGrantSATID; //searchParamObject.Assist_type_cd;
                V_DISTRICT_CD = searchParamObject.DistrictCd;
                V_RULE_FLAG = searchParamObject.ddl_BusinessRule;
                V_Entered_By = CommonVariables.UserName;
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_SAT";
                service.Begin();
                dt = service.GetDataTable(true, "PR_ELIGIBLE_UNTARGET_SEARCH",
                     v_fiscal_yr,
                    V_ASSIST_TYPE_CD,
                    V_DISTRICT_CD,
                    V_RULE_FLAG,
                    V_Entered_By,
                    pageSize,
                    pageIndex,
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

        public QueryResult SaveTargets(TargetingSearch searchParamObject)
        {
            CommonFunction commFunction = new CommonFunction();
            Object v_fiscal_yr = DBNull.Value;
            Object V_ASSIST_TYPE_CD = DBNull.Value;
            Object V_RULE_FLAG = DBNull.Value;
            Object V_DISTRICT_CD = DBNull.Value;
            Object V_ENTERED_BY = DBNull.Value;//CommonVariables.UserName; 
            Object P_TARGET_SNO = DBNull.Value;


            if (searchParamObject != null)
            {
                v_fiscal_yr = searchParamObject.ddl_FiscalYear;
                V_ASSIST_TYPE_CD = ConstantVariables.EducationGrantSATID; //searchParamObject.Assist_type_cd;
                V_DISTRICT_CD = searchParamObject.DistrictCd;
                V_ENTERED_BY = CommonVariables.UserName;
                V_RULE_FLAG = searchParamObject.ddl_BusinessRule;




                //if (!String.IsNullOrWhiteSpace(pagerVal))

                //pagerInitialVal = pagerVal;
            }

            QueryResult quryResult = new QueryResult();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_SAT";
                service.Begin();

                //quryResult=service.SubmitChanges(
                quryResult = service.SubmitChanges("PR_ELIGIBLE_EDU_TARGETING",
                      v_fiscal_yr,
                    V_ASSIST_TYPE_CD,
                    V_DISTRICT_CD,
                    V_RULE_FLAG, null,
                    V_ENTERED_BY, 100, 1, DBNull.Value
                    );
            }
            //V_TOTAL
            catch (Exception ex)
            {
                //dt = null;
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

        public DataTable GetTargetedList(TargetingSearch searchParamObject, string pageIndex = "1", string pageSize = "100", string orderby = "PER_DISTRICT_CD", string Order = "ASC")
        {
            CommonFunction commFunction = new CommonFunction();
            if (pageSize == "")
            {
                pageSize = "100";
            }
            Object v_fiscal_yr = DBNull.Value;
            Object V_ASSIST_TYPE_CD = DBNull.Value;
            Object V_DISTRICT_CD = DBNull.Value;
            Object V_VDCMUN_CD = DBNull.Value;
            Object V_WARD_NO = DBNull.Value;
            Object V_AREA_ENG = DBNull.Value;
            Object V_AREA_LOC = DBNull.Value;
            Object V_BATCH_ID = DBNull.Value;
            Object V_EDUCATION_CD = DBNull.Value;

            if (searchParamObject != null)
            {
                v_fiscal_yr = searchParamObject.ddl_FiscalYear;
                V_ASSIST_TYPE_CD = "1"; //searchParamObject.Assist_type_cd;
                V_DISTRICT_CD = searchParamObject.DistrictCd;
                V_VDCMUN_CD = searchParamObject.VDCMun;
                V_WARD_NO = searchParamObject.WardNo;
                V_AREA_ENG = searchParamObject.AreaEng;
                V_AREA_LOC = searchParamObject.AreaLoc;
                V_DISTRICT_CD = searchParamObject.DistrictCd;
                V_BATCH_ID = searchParamObject.BatchId;
                V_EDUCATION_CD = searchParamObject.EducationCd;
            }

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_SAT";
                service.Begin();
                dt = service.GetDataTable(true, "PR_EDU_TARGET_SEARCH",
                     v_fiscal_yr,
                    V_ASSIST_TYPE_CD,
                    V_BATCH_ID,
                    V_DISTRICT_CD,
                    V_VDCMUN_CD,
                    V_EDUCATION_CD,
                    V_WARD_NO,
                    V_AREA_ENG,
                    V_AREA_LOC,
                    pageSize,
                    pageIndex,
                    orderby,
                    Order,
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
        public void GetRuleCalcData(string districtCd, out string rule, out string quota)
        {
            rule = string.Empty; quota = string.Empty;

            DataTable dt = new DataTable();
            ServiceFactory service = new ServiceFactory();
            string resPackageName = service.PackageName;
            try
            {
                string query = string.Format("SELECT * FROM {0} WHERE DISTRICT_CD = {1} AND EFFECTIVE_DT = (SELECT MAX(EFFECTIVE_DT) FROM {0} WHERE DISTRICT_CD = {1})", "MIS_RULE_CALC", districtCd);
                service.PackageName = "PKG_SAT";
                service.Begin();
                dt = service.GetDataTable(query, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    rule = dt.Rows[0]["RULE_FLAG"].ConvertToString();
                    quota = dt.Rows[0]["QUOTA"].ConvertToString();
                }
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
        }


        public bool GenerateTargetExcel(out string filename, out string error, string fiscalyear, string batchId, string districtCd, string vdcmunCd, string wardNo, string educationCd, string targetCd, string spCd)
        {
            DataTable dt = null;
            string dir = HttpContext.Current.Server.MapPath("~/Files/Target/");
            if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
            filename = System.IO.Path.Combine(dir, string.Concat("TARGET_", batchId, ".xlsx"));
            error = string.Empty;
            bool success = false;
            dt = GetExcelData(fiscalyear, batchId, districtCd, vdcmunCd, wardNo, educationCd, targetCd, spCd);
            if (dt != null && dt.Rows.Count > 0)
            {
                success = this.ExportFeedToExcel(dt, filename);
            }

            return success;
        }

        public bool ExportFeedToExcel(DataTable dt, string filename)
        {
            StringBuilder sb = new StringBuilder();
            int uniqueHHCount = 1;
            DataRow[] UniqueHHRow;
            string preHouseholdID = "";
            int hhCount = 0;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    sb.Append("<table border='1' cellspacinng='0' cellpadding='0'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("S. No") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Form Serial No") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Household ID") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("HH Head Name") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("District") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("VDC") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("WARD") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Area") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Eligible Child count") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Eligible Child Member Id") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Eligible Child Name") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Actual Eligible child Member Id") + "</div></th>");
                    sb.Append("<th><div style='width:150px;'>" + Utils.GetLabel("Remarks") + "</div></th>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tbody>");
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("<tr>");
                        if (dr["HOUSEHOLD_DEFINED_CD"].ConvertToString() != preHouseholdID)
                        {
                            hhCount += 1;
                            UniqueHHRow = dt.Select("HOUSEHOLD_DEFINED_CD='" + dr["HOUSEHOLD_DEFINED_CD"].ConvertToString() + "'");
                            uniqueHHCount = UniqueHHRow.Length;
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(hhCount.ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["FORM_NO"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["HOUSEHOLD_DEFINED_CD"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["HEAD_NAME"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["DISTRICT_NAME"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["VDC_MUN_NAME"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["PER_WARD_NO"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["AREA"].ConvertToString());
                            sb.Append("</div></td>");
                            sb.Append("<td rowspan='" + uniqueHHCount.ToString() + "'><div style='width:150px;'>");
                            sb.Append(dr["ELIGILBLE_CHILD"].ConvertToString());
                            sb.Append("</div></td>");
                        }
                        sb.Append("<td><div style='width:150px;'>");
                        sb.Append(dr["MEMBER_DEFINED_CD"].ConvertToString());
                        sb.Append("</div></td>");
                        sb.Append("<td><div style='width:150px;'>");
                        sb.Append(dr["CHILD_NAME"].ConvertToString());
                        sb.Append("</div></td>");
                        sb.Append("<td><div style='width:150px;'>");
                        sb.Append("</div></td>");
                        sb.Append("<td><div style='width:150px;'>");
                        sb.Append("</div></td>");
                        preHouseholdID = dr["HOUSEHOLD_DEFINED_CD"].ConvertToString();
                        sb.Append("</tr>");
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }
                Utils.CreateFile(ReportTemplate.GetReportHTML(sb.ToString()), filename.Replace(".xlsx", ".html"));
                Utils.CreateFile(ReportTemplate.GetReportHTML(sb.ToString()), filename);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ExceptionManager.AppendLog(ex);
                return false;
            }
            return true;
        }
        public DataTable GetExcelData(string fiscalyear, string batchId, string districtCd, string vdcmunCd, string wardNo, string educationCd, string targetCd, string spCd)
        {
            DataTable dt = null;
            QueryResult qr = new QueryResult();
            Object P_TARGET_CD = DBNull.Value;
            Object P_ASSIST_TYPE_CD = ConstantVariables.EducationGrantSATID;
            Object P_BATCH_ID = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDCMUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_SERVICE_PROVIDER_CD = DBNull.Value;
            Object P_EDUCATION_CD = DBNull.Value;
            Object P_ENTERED_BY = SessionCheck.getSessionUsername();
            Object P_LANG = Utils.ToggleLanguage("E", "N");
            if (targetCd.ConvertToString() != "")
                P_TARGET_CD = targetCd.ConvertToString();
            if (batchId.ConvertToString() != "")
                P_BATCH_ID = batchId.ConvertToString();
            if (districtCd.ConvertToString() != "")
                P_DISTRICT_CD = districtCd.ConvertToString();
            if (vdcmunCd.ConvertToString() != "")
                P_VDCMUN_CD = vdcmunCd.ConvertToString();
            if (wardNo.ConvertToString() != "")
                P_WARD_NO = wardNo.ConvertToString();
            if (educationCd.ConvertToString() != "")
                P_EDUCATION_CD = educationCd.ConvertToString();
            if (spCd.ConvertToString() != "")
                P_SERVICE_PROVIDER_CD = spCd.ConvertToString();
            using (ServiceFactory service = new ServiceFactory())
            {

                service.Begin();
                service.PackageName = "PKG_SAT";
                try
                {
                    dt = service.GetDataTable(true, "PR_ELIGIBLE_EDU_EXPORT_XL",
                         P_TARGET_CD,
                         P_ASSIST_TYPE_CD,
                         P_BATCH_ID,
                         P_DISTRICT_CD,
                         P_VDCMUN_CD,
                         P_WARD_NO,
                         P_SERVICE_PROVIDER_CD,
                         P_EDUCATION_CD,
                         P_ENTERED_BY,
                         P_LANG,
                        DBNull.Value);
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
            }
            return dt;
        }

    }
}
