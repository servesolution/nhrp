using EntityFramework;
using ExceptionHandler;
using MIS.Models.Enrollment;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MIS.Services.Enrollment
{
    public class EnrollmentService
    {
        public DataSet EnrollmentDetail(string HouseDefinedCd)
        {
            DataSet dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    var hcd=HouseDefinedCd == string.Empty ? 0 : HouseDefinedCd.ToDecimal();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataSetOracle(true, "PR_NHRS_ENROLLMENT_POPUP", hcd, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value);                  

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

        public DataTable GetEntollmentPADetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object p_ENTERED_BY = DBNull.Value;
            Object p_DT_FROM = DBNull.Value;
            Object p_DT_TO = DBNull.Value;

            if (paramValues["user"].ConvertToString() != string.Empty)
                p_ENTERED_BY = paramValues["user"].ConvertToString();
            if (paramValues["dateFrom"].ConvertToString() != string.Empty)
                p_DT_FROM = paramValues["dateFrom"].ConvertToString();
            if (paramValues["dateTo"].ConvertToString() != string.Empty && paramValues["dateTo"].ConvertToString() != "undefined")
                p_DT_TO = paramValues["dateTo"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_ENROLLMENT_PA_DAILY_STATUS",
                    p_ENTERED_BY,
                    p_DT_FROM,
                    p_DT_TO,
                   
                    DBNull.Value);
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

        //Enrlment PA Daily summary 
        public DataTable GetEntollmentPASummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object p_ENTERED_BY = DBNull.Value;
            Object p_DT_FROM = DBNull.Value;
            Object p_DT_TO = DBNull.Value;
            Object p_Type = DBNull.Value;

            if (paramValues["user"].ConvertToString() != string.Empty)
                p_ENTERED_BY = paramValues["user"].ConvertToString();

            if (paramValues["selectedType"].ConvertToString() != string.Empty)
                p_Type = paramValues["selectedType"].ConvertToString();

            if (paramValues["dateFrom"].ConvertToString() != string.Empty)
                p_DT_FROM = paramValues["dateFrom"].ConvertToString();

            if (paramValues["dateTo"].ConvertToString() != string.Empty && paramValues["dateTo"].ConvertToString() != "undefined")
                p_DT_TO = paramValues["dateTo"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_enroll_PA_daily_sts_summary",
                    p_ENTERED_BY,
                    p_DT_FROM,
                    p_DT_TO,
                    p_Type,
                    DBNull.Value);
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






        public DataTable GetEnrollmentSearchDetail(EnrollmentSearch objSearch, string paUpload)
        {
            DataTable dt = new DataTable();
            Object P_ENROLLMENT_ID = DBNull.Value;
            Object P_TARGETING_ID = DBNull.Value;
            Object P_HOUSE_OWNER_ID = DBNull.Value;
            Object P_House_Owner_Name = DBNull.Value;
            Object P_TARGET_BATCH_ID = DBNull.Value;
            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;
            Object P_ENROLLMENT_DT_FROM = DBNull.Value;
            Object P_ENROLLMENT_DT_TO = DBNull.Value;
            Object P_HOUSE_ID = DBNull.Value;
            Object P_ENROLLMENT_MOU_DT_FROM = DBNull.Value;
            Object P_ENROLLMENT_MOU_DT_TO = DBNull.Value;
            Object P_DISTRICT_CD = DBNull.Value;
            Object P_VDC_MUN_CD = DBNull.Value;
            Object P_WARD_NO = DBNull.Value;
            Object P_PAYMENT_RECEIVER_MEMBER_ID = DBNull.Value;

            Object P_BANK_CD = DBNull.Value;
            Object P_BANK_BRANCH_CD = DBNull.Value;
            Object P_BANK_ACC_TYPE_CD = DBNull.Value;
            Object P_MOU_SIGNED_STATUS = DBNull.Value;
            Object P_SMS_SEND_STATUS = DBNull.Value;
            Object P_PRINT_STATUS = DBNull.Value;
            Object P_IS_PAYMENT_RECEIVER_CHANGED = DBNull.Value;

            Object P_CUTOFF_CD = DBNull.Value;
            Object P_ENROLLMENT_STATUS = DBNull.Value;
            Object P_APPROVED = DBNull.Value;
            Object P_IDENTIFICATION_TYPE_CD = DBNull.Value;

            Object P_ENUMERATION_AREA = DBNull.Value;
            Object P_BENEFICIARY_ID = DBNull.Value;
            Object P_INSTANCE_UNIQUE_SNO = DBNull.Value;

            


            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
           
            if (objSearch.ENUMERATION_AREA != "")
            {
                P_ENUMERATION_AREA = Decimal.Parse(objSearch.ENUMERATION_AREA);
            }
            if (objSearch.BENEFICIARY_ID != "----")
            {
                P_BENEFICIARY_ID = objSearch.BENEFICIARY_ID;
            }
            if (objSearch.NISSA_NO != "")
            {
                P_INSTANCE_UNIQUE_SNO = Decimal.Parse(objSearch.NISSA_NO);
            }

            if (objSearch.Entrollment_ID != "" )
            {
                P_ENROLLMENT_ID = Decimal.Parse(objSearch.Entrollment_ID);
            }
            if (objSearch.targetBatchId != "")
            {
                P_TARGET_BATCH_ID = Decimal.Parse(objSearch.targetBatchId);
            }

            if (objSearch.Targetting_ID != "")
            {
                P_TARGETING_ID = Decimal.Parse(objSearch.Targetting_ID);
            }
            if (objSearch.House_Owner_ID != "")
            {
                P_HOUSE_OWNER_ID = Decimal.Parse(objSearch.House_Owner_ID);
            }
            if (objSearch.HOUSE_ID != "")
            {
                P_HOUSE_ID = Decimal.Parse(objSearch.HOUSE_ID);
            }
            //if (objSearch.House_Owner_Name != "")
            //{
            //    v_House_Owner_Name = Decimal.Parse(objSearch.House_Owner_ID);
            //}
            if (objSearch.House_Owner_Name != "")
            {
                P_House_Owner_Name = objSearch.House_Owner_Name;
            }
            if (objSearch.Building_Structure_No != "")
            {
                P_BUILDING_STRUCTURE_NO = Decimal.Parse(objSearch.Building_Structure_No);
            }
            if (objSearch.Enrollment_Dt_From != "")
            {
                P_ENROLLMENT_DT_FROM = (objSearch.Enrollment_Dt_From);
            }
            if (objSearch.Enrollment_Dt_To != "")
            {
                P_ENROLLMENT_DT_TO = (objSearch.Enrollment_Dt_To);
            }
            if (objSearch.Enrollment_M_Dt_From != "")
            {
                P_ENROLLMENT_MOU_DT_FROM = (objSearch.Enrollment_M_Dt_From);
            }
            if (objSearch.Enrollment_M_Dt_To != "")
            {
                P_ENROLLMENT_MOU_DT_TO = (objSearch.Enrollment_M_Dt_To);
            }
            if (objSearch.DistrictCd != "")
            {
                P_DISTRICT_CD = Decimal.Parse(objSearch.DistrictCd);
            }
            if (objSearch.VDCMun != "")
            {
                P_VDC_MUN_CD = Decimal.Parse(objSearch.VDCMun);
            }
            if (objSearch.WardNo != "")
            {
                P_WARD_NO = Decimal.Parse(objSearch.WardNo);
            }
            if (objSearch.Payment_rec_mem_Id != "")
            {
                P_PAYMENT_RECEIVER_MEMBER_ID = Decimal.Parse(objSearch.Payment_rec_mem_Id);
            }
            if (objSearch.Bank_CD != "")
            {
                P_BANK_CD = Decimal.Parse(objSearch.Bank_CD);
            }
            if (objSearch.Bank_Branch_CD != "")
            {
                P_BANK_BRANCH_CD = (objSearch.Bank_Branch_CD);
            }
            if (objSearch.Bank_Acc_Type != "")
            {
                P_BANK_ACC_TYPE_CD = Decimal.Parse(objSearch.Bank_Acc_Type);
            }
            if (objSearch.M_Signed_Status != "")
            {
                P_MOU_SIGNED_STATUS = (objSearch.M_Signed_Status);
            }
            if (objSearch.Print_Status != "")
            {
                P_PRINT_STATUS = (objSearch.Print_Status);
            }

            if (objSearch.SMS_SEND_STATUS != "")
            {
                P_SMS_SEND_STATUS = (objSearch.SMS_SEND_STATUS);
            }

            if (objSearch.Cut_Off != "")
            {
                P_CUTOFF_CD = Decimal.Parse(objSearch.Cut_Off);
            }
            if (objSearch.Enrollment_Status != "")
            {
                P_ENROLLMENT_STATUS = objSearch.Enrollment_Status;
            }
            if (objSearch.Approved != "")
            {
                P_APPROVED = (objSearch.Approved);
            }
            if (objSearch.Identifiication_Type_Cd != "")
            {
                P_IDENTIFICATION_TYPE_CD = Decimal.Parse(objSearch.Identifiication_Type_Cd);
            }
            if (objSearch.SORT_BY != "")
            {
                SortBy = objSearch.SORT_BY;
            }
            if (objSearch.SORT_ORDER != "")
            {
                SortOrder = objSearch.SORT_ORDER;
            }
            if (objSearch.PAGE_SIZE != "")
            {
                PageSize = objSearch.PAGE_SIZE;
            }
            if (objSearch.PAGE_INDEX != "")
            {
                PageIndex = objSearch.PAGE_INDEX;
            }
            if (objSearch.EXPORT_EXCEL != "")
            {
                ExportExcel = objSearch.EXPORT_EXCEL;
            }
            if (objSearch.LANG != "")
            {
                Lang = objSearch.LANG;
            }
            if (objSearch.FILTER_WORD != "")
            {
                FilterWord = objSearch.FILTER_WORD;
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_TARGET_ENROLLMENT_SEARCH ";
                    //dt = service.GetDataTable(true, "PR_NHRS_ENROLLMENT_SEARCH",

                    dt = service.GetDataTable(true, "PR_ENRLL_SEARCH_FROM_VIEW",
                                                P_ENROLLMENT_ID,
                                                P_HOUSE_ID,
                                                P_House_Owner_Name,
                                                P_TARGET_BATCH_ID,
                                                P_TARGETING_ID,
                                                P_HOUSE_OWNER_ID,
                                                P_BUILDING_STRUCTURE_NO,
                                                P_ENROLLMENT_DT_FROM,
                                                P_ENROLLMENT_DT_TO,
                                                P_DISTRICT_CD,
                                                P_VDC_MUN_CD,
                                                P_WARD_NO,
                                                P_MOU_SIGNED_STATUS,
                                                P_ENROLLMENT_STATUS,
                                                P_PRINT_STATUS,
                                                P_SMS_SEND_STATUS,                                               
                                                P_CUTOFF_CD,
                                                P_APPROVED,
                                                P_BENEFICIARY_ID,
                                                P_INSTANCE_UNIQUE_SNO,
                                               SortBy,
                                                SortOrder,
                                                PageSize,
                                                PageIndex,
                                                ExportExcel,
                                                Lang,
                                                FilterWord,
                                                DBNull.Value,
                                                paUpload);






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
                    dr["ADDRESS"] = Utils.ToggleLanguage((dr["VDC_ENG"].ConvertToString() != "" ? dr["VDC_ENG"].ConvertToString() + ", " : "") + (dr["WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["WARD_NO"].ConvertToString()) + ", " : "") + (dr["DISTRICT_ENG"].ConvertToString() != "" ? dr["DISTRICT_ENG"].ConvertToString() : ""), (dr["VDC_LOC"].ConvertToString() != "" ? dr["VDC_LOC"].ConvertToString() + ", " : "") + (dr["WARD_NO"].ConvertToString() != "" ? Utils.GetLabel(dr["WARD_NO"].ConvertToString()) + ", " : "") + (dr["DISTRICT_LOC"].ConvertToString() != "" ? dr["DISTRICT_LOC"].ConvertToString() : ""));
                }
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                
                dt.Columns.Add("MOU SIGNED STATUS");
                dt.Columns.Add("BUILDING_STRUCTURE_NO");
                
                dt.Columns.Add("PA_FLAG");
                dt.Columns.Add("MOU_SIGNED_STATUS");
                dt.Columns.Add("PRINT_STATUS");
                dt.Columns.Add("APPROVED");
                
                foreach (DataRow dr in dt.Rows)
                {
                    //if (dr["REC_MOU_SIGNED_STATUS"].ConvertToString() == "N" || dr["RET_MOU_SIGNED_STATUS"].ConvertToString() == "N" || dr["GRI_MOU_SIGNED_STATUS"].ConvertToString() == "N")
                    //{
                    //    dr["MOU SIGNED STATUS"] = "Not Signed";
                    //}
                    //else
                    //{
                    //    dr["MOU SIGNED STATUS"] = "Not Signed";
                    //}

                    if (dr["REC_DEFINED_ID"].ConvertToString() != null && dr["REC_DEFINED_ID"].ConvertToString() != "")
                    {
                        dr["BUILDING_STRUCTURE_NO"] = dr["REC_BUILDING_STRUCTURE_NO"].ConvertToString();
                         dr["PA_FLAG"] = dr["REC_PA_FLAG"].ConvertToString();
                        dr["MOU_SIGNED_STATUS"] = dr["REC_MOU_SIGNED_STATUS"].ConvertToString();
                        dr["PRINT_STATUS"] = dr["REC_PRINT_STATUS"].ConvertToString();
                        dr["APPROVED"] = dr["REC_APPROVED"].ConvertToString();
                    }
                    else 
                    {
                        if (dr["RET_DEFINED_ID"].ConvertToString() != null && dr["RET_DEFINED_ID"].ConvertToString() != "")
                        {
                            dr["BUILDING_STRUCTURE_NO"] = dr["GRI_BUILDING_STRUCTURE_NO"].ConvertToString();
                             dr["MOU_SIGNED_STATUS"] = dr["RET_MOU_SIGNED_STATUS"].ConvertToString();
                            dr["PRINT_STATUS"] = dr["RET_PRINT_STATUS"].ConvertToString();
                            dr["APPROVED"] = dr["RET_APPROVED"].ConvertToString();
                            dr["PA_FLAG"] = dr["RET_PA_FLAG"].ConvertToString();
                        }
                        else
                        {
                            dr["BUILDING_STRUCTURE_NO"] = dr["GRI_BUILDING_STRUCTURE_NO"].ConvertToString();
                             dr["MOU_SIGNED_STATUS"] = dr["GRI_MOU_SIGNED_STATUS"].ConvertToString();
                            dr["PRINT_STATUS"] = dr["GRI_PRINT_STATUS"].ConvertToString();
                            dr["APPROVED"] = dr["GRI_APPROVED"].ConvertToString();
                            dr["PA_FLAG"] = dr["GRI_PA_FLAG"].ConvertToString();
                        }
                    }



                }
            }


                return dt;

            
        }

        public enrollmentclass GetEnrollmentDetailByID(string enrollmentid, string targetbatchid, string targetingid, string HouseOwnerID, string structureNo)
        {
            
            enrollmentclass objEnrl = new enrollmentclass();
            
            String docData = String.Empty;
            QueryResult qr = new QueryResult();
            Object v_MEMBER_ID = DBNull.Value;
            Object V_FIRST_NAME_ENG = DBNull.Value;
            Object V_MIDDLE_NAME_ENG = DBNull.Value;
            Object V_LAST_NAME_ENG = DBNull.Value;
            Object V_FULL_NAME_ENG = DBNull.Value;
            Object V_FIRST_NAME_LOC = DBNull.Value;
            Object V_MIDDLE_NAME_LOC = DBNull.Value;
            Object V_LAST_NAME_LOC = DBNull.Value;
            Object V_FULL_NAME_LOC = DBNull.Value;
            Object V_GENDER = DBNull.Value;
            Object v_DISTRICT_CD = DBNull.Value;
            Object V_DISTRICT_ENG = DBNull.Value;
            Object V_DISTRICT_LOC = DBNull.Value;
            Object v_VDC_MUN_CD = DBNull.Value;
            Object V_VDC_MUN_ENG = DBNull.Value;
            Object V_VDC_MUN_LOC = DBNull.Value;
            Object V_WARD_NO = DBNull.Value;
            Object V_AREA_ENG = DBNull.Value;
            Object V_AREA_LOC = DBNull.Value;
            Object V_ENUMERATOR_ID = DBNull.Value;
            Object V_PHOTO_PATH = DBNull.Value;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    qr = service.SubmitChanges("PR_NHRS_ENROLLMENT_VIEW",
                                                     enrollmentid,
                                                     targetbatchid,
                                                     targetingid,
                                                     HouseOwnerID,
                                                     structureNo,
                                                     v_MEMBER_ID,
                                                     V_FIRST_NAME_ENG,
                                                     V_MIDDLE_NAME_ENG,
                                                     V_LAST_NAME_ENG,
                                                     V_FULL_NAME_ENG,
                                                     V_FIRST_NAME_LOC,
                                                     V_MIDDLE_NAME_LOC,
                                                     V_LAST_NAME_LOC,
                                                     V_FULL_NAME_LOC,
                                                     V_GENDER,
                                                     v_DISTRICT_CD,
                                                     V_DISTRICT_ENG,
                                                     V_DISTRICT_LOC,
                                                     v_VDC_MUN_CD,
                                                     V_VDC_MUN_ENG,
                                                     V_VDC_MUN_LOC,
                                                     V_WARD_NO,
                                                     V_AREA_ENG,
                                                     V_AREA_LOC,
                                                     V_ENUMERATOR_ID,
                                                     V_PHOTO_PATH
                                                     );
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    qr = null;
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
            if (qr != null && qr.IsSuccess)
            {
                objEnrl.ENROLLMENT_ID =enrollmentid;
                objEnrl.TARGETING_ID = targetingid;
                objEnrl.HOUSE_OWNER_ID = HouseOwnerID;
                objEnrl.BUILDING_STRUCTURE_NO = structureNo;
                objEnrl.TARGETING_BATCH_ID = targetbatchid;

                objEnrl.FIRST_NAME_ENG = Utils.ToggleLanguage(qr["V_FIRST_NAME"].ToString(), qr["V_FIRST_NAME_LOC"].ToString());
                objEnrl.MIDDLE_NAME_ENG = Utils.ToggleLanguage(qr["V_MIDDLE_NAME"].ToString(), qr["V_MIDDLE_NAME_LOC"].ToString());
                objEnrl.LAST_NAME_ENG = Utils.ToggleLanguage(qr["V_LAST_NAME"].ToString(), qr["V_LAST_NAME_LOC"].ToString());
                //objEnrl. = qr["v_GENDER_CD"].ToString();
                //objEnrl.fu = Utils.ToggleLanguage(qr["v_FULL_NAME"].ToString(), qr["v_FULL_NAME_LOC"].ToString());
                objEnrl.DISTRICT_CD = (qr["V_DISTRICT_CD"].ToString());
                objEnrl.DISTRICT_ENG = (qr["V_DISTRICT_ENG"].ToString());
                objEnrl.VDC_MUN_CD = GetData.GetDefinedCodeFor(DataType.VdcMun, qr["v_VDC_MUN_CD"].ConvertToString());
                objEnrl.VDC_MUN_ENG = (qr["V_VDC_MUN_ENG"].ToString());
                //objEnrl.VDC_MUNICIPALITY_CD = (qr["v_VDC_MUN_CD"].ConvertToString());
                objEnrl.WARD_NO = qr["V_WARD_NO"].ConvertToString();
                //objEnrl.idNumber = "......";

                //objEnrl.surveyId = qr["v_ENUMURATOR_ID"].ToString();
            }
            return objEnrl;

        }
        public QueryResult GetEnrollmentSaved(enrollmentclass objSearch)
        {
            enrollmentclass objEnrl = new enrollmentclass();

            String docData = String.Empty;
            QueryResult qr = new QueryResult();
            Object v_mode = DBNull.Value;
            Object v_TARGET_BATCH_ID = DBNull.Value;
            Object v_TARGETING_ID = DBNull.Value;
            Object v_ENROLLMENT_ID = DBNull.Value;
            Object v_MOU_ID = DBNull.Value;
            Object v_ENROLLMENT_MOU_DT = DBNull.Value;
            Object v_ENROLLMENT_MOU_DT_LOC = DBNull.Value;
            Object v_HOUSE_OWNER_ID = DBNull.Value;
            Object v_HO_MEMBER_ID = DBNull.Value;
            Object v_BUILDING_STRUCTURE_NO = DBNull.Value;
            Object v_HOUSEHOLD_ID = DBNull.Value;
            Object v_HOUSEHOLD_DEFINED_CD = DBNull.Value;
            Object v_HH_MEMBER_ID = DBNull.Value;
            Object v_FIRST_NAME_ENG = DBNull.Value;
            Object v_MIDDLE_NAME_ENG = DBNull.Value;
            Object v_LAST_NAME_ENG = DBNull.Value;
            Object v_FULL_NAME_ENG = DBNull.Value;
            Object v_FIRST_NAME_LOC = DBNull.Value;
            Object v_MIDDLE_NAME_LOC = DBNull.Value;
            Object v_LAST_NAME_LOC = DBNull.Value;
            Object v_FULL_NAME_LOC = DBNull.Value;
            Object v_MEMBER_PHOTO_PATH = DBNull.Value;
            Object v_IDENTIFICATION_TYPE_CD = DBNull.Value;
            Object v_IDENTIFICATION_NO = DBNull.Value;
            Object v_IDENTIFICATION_DOCUMENT = DBNull.Value;
            Object v_IDENTIFICATION_ISSUE_DIS_CD = DBNull.Value;
            Object v_COUNTRY_CD = DBNull.Value;
            Object v_REGION_STATE_CD = DBNull.Value;
            Object v_ZONE_CD = DBNull.Value;
            Object v_DISTRICT_CD = DBNull.Value;
            Object v_VDC_MUN_CD = DBNull.Value;
            Object v_WARD_NO = DBNull.Value;
            Object v_ENUMERATION_AREA = DBNull.Value;
            Object v_AREA_ENG = DBNull.Value;
            Object v_AREA_LOC = DBNull.Value;
            Object v_BANK_CD = DBNull.Value;
            Object v_BANK_BRANCH_CD = DBNull.Value;
            Object v_BANK_ACC_NO = DBNull.Value;
            Object v_BANK_ACC_TYPE_CD = DBNull.Value;
            Object v_IS_PAYMENT_RECEIVER_CHANGED = DBNull.Value;
            Object v_CHANGED_REASON_ENG = DBNull.Value;
            Object v_CHANGED_REASON_LOC = DBNull.Value;
            Object v_BENEFICIARY_MEMBER_ID = DBNull.Value;
            Object v_BENEFICIARY_FNAME_ENG = DBNull.Value;
            Object v_BENEFICIARY_MNAME_ENG = DBNull.Value;
            Object v_BENEFICIARY_LNAME_ENG = DBNull.Value;
            Object v_BENEFICIARY_FULLNAME_ENG = DBNull.Value;
            Object v_BENEFICIARY_FNAME_LOC = DBNull.Value;
            Object v_BENEFICIARY_MNAME_LOC = DBNull.Value;
            Object v_BENEFICIARY_LNAME_LOC = DBNull.Value;
            Object v_BENEFICIARY_FULLNAME_LOC = DBNull.Value;
            Object v_BENEFICIARY_RELATION_TYPE_CD = DBNull.Value;
            Object v_PROXY_MEMBER_ID = DBNull.Value;
            Object v_PROXY_FNAME_ENG = DBNull.Value;
            Object v_PROXY_MNAME_ENG = DBNull.Value;
            Object v_PROXY_LNAME_ENG  = DBNull.Value;
            Object v_PROXY_FULLNAME_ENG = DBNull.Value;
            Object v_PROXY_FNAME_LOC = DBNull.Value;
            Object v_PROXY_MNAME_LOC = DBNull.Value;
            Object v_PROXY_LNAME_LOC = DBNull.Value;
            Object v_PROXY_FULLNAME_LOC = DBNull.Value;
            Object v_PROXY_RELATION_TYPE_CD = DBNull.Value;
            Object v_EMPLOYEE_ID = DBNull.Value;
            Object v_APPROVED ="Y";
            Object v_APPROVED_BY = CommonVariables.UserName;
            Object v_APPROVED_DT = System.DateTime.Now;
            Object v_APPROVED_DT_LOC = DBNull.Value;
            Object v_UPDATED_BY = CommonVariables.UserName;
            Object v_UPDATED_DT = System.DateTime.Now;
            Object v_UPDATED_DT_LOC = DBNull.Value;
            Object v_ENTERED_BY = CommonVariables.UserName;
            Object v_ENTERED_DT = System.DateTime.Now;
            Object v_ENTERED_DT_LOC = DBNull.Value;
            Object v_ip_address = CommonVariables.IPAddress;


            if (objSearch.MODE != "")
            {
                v_mode = (objSearch.MODE);
            }
            if (objSearch.TARGETING_BATCH_ID != "")
            {
                v_TARGET_BATCH_ID = Decimal.Parse(objSearch.TARGETING_BATCH_ID);
            }
            if (objSearch.TARGETING_ID != "")
            {
                v_TARGETING_ID = Decimal.Parse(objSearch.TARGETING_ID);
            }
            if (objSearch.ENROLLMENT_ID != "")
            {
                v_ENROLLMENT_ID = Decimal.Parse(objSearch.ENROLLMENT_ID);
            }
            if (objSearch.MOU_ID != "")
            {
                v_MOU_ID = (objSearch.MOU_ID);
            }
            if (objSearch.ENROLLMENT_MOU_DT != "")
            {
                v_ENROLLMENT_MOU_DT = (objSearch.ENROLLMENT_MOU_DT);
            }

            if (objSearch.ENROLLMENT_MOU_DT_LOC != "")
            {
                v_ENROLLMENT_MOU_DT_LOC = (objSearch.ENROLLMENT_MOU_DT_LOC);
            }
           
            if (objSearch.HOUSE_OWNER_ID != "")
            {
                v_HOUSE_OWNER_ID = Decimal.Parse(objSearch.HOUSE_OWNER_ID);
            }
            if (objSearch.HO_MEMBER_ID != "")
            {
                v_HO_MEMBER_ID = (objSearch.HO_MEMBER_ID);
            }

            if (objSearch.BUILDING_STRUCTURE_NO != "")
            {
                v_BUILDING_STRUCTURE_NO = (objSearch.BUILDING_STRUCTURE_NO);
            }
            if (objSearch.HOUSEHOLD_ID != "")
            {
                v_HOUSEHOLD_ID = (objSearch.HOUSEHOLD_ID);
            }

            if (objSearch.HOUSEHOLD_DEFINED_CD != "")
            {
                v_HOUSEHOLD_DEFINED_CD = (objSearch.HOUSEHOLD_DEFINED_CD);
            }

            if (objSearch.HH_MEMBER_ID != "")
            {
                v_HH_MEMBER_ID = (objSearch.HH_MEMBER_ID);
            }
            if (objSearch.FIRST_NAME_ENG != "")
            {
                v_FIRST_NAME_ENG = (objSearch.FIRST_NAME_ENG);
            }
            if (objSearch.MIDDLE_NAME_ENG != "")
            {
                v_MIDDLE_NAME_ENG = (objSearch.MIDDLE_NAME_ENG);
            }
            if (objSearch.LAST_NAME_ENG != "")
            {
                v_LAST_NAME_ENG = (objSearch.LAST_NAME_ENG);
            }
            if (objSearch.FULL_NAME_ENG != "")
            {
                v_FULL_NAME_ENG = (objSearch.FULL_NAME_ENG);
            }
            if (objSearch.FIRST_NAME_LOC != "")
            {
                v_FIRST_NAME_LOC = (objSearch.FIRST_NAME_LOC);
            }
            if (objSearch.MIDDLE_NAME_LOC != "")
            {
                v_MIDDLE_NAME_LOC = (objSearch.MIDDLE_NAME_LOC);
            }
            if (objSearch.LAST_NAME_LOC != "")
            {
                v_LAST_NAME_LOC = (objSearch.LAST_NAME_LOC);
            }
            if (objSearch.FULL_NAME_LOC != "")
            {
                v_FULL_NAME_LOC = (objSearch.FULL_NAME_LOC);
            }
             if (objSearch.MEMBER_PHOTO_PATH != "")
            {
                v_MEMBER_PHOTO_PATH = (objSearch.MEMBER_PHOTO_PATH);
            }
             if (objSearch.IDENTIFICATION_TYPE_CD != "")
             {
                 v_IDENTIFICATION_TYPE_CD = (objSearch.IDENTIFICATION_TYPE_CD);
             }
             if (objSearch.IDENTIFICATION_NO != "")
             {
                 v_IDENTIFICATION_TYPE_CD = (objSearch.IDENTIFICATION_NO);
             }
             if (objSearch.IDENTIFICATION_DOCUMENT != "")
             {
                 v_IDENTIFICATION_DOCUMENT = (objSearch.IDENTIFICATION_DOCUMENT);
             }
             if (objSearch.IDENTIFICATION_ISSUE_DIS_CD != "")
             {
                 v_IDENTIFICATION_ISSUE_DIS_CD = (objSearch.IDENTIFICATION_ISSUE_DIS_CD);
             }





             if (objSearch.COUNTRY_CD != "")
             {
                 v_COUNTRY_CD = (objSearch.COUNTRY_CD);
             }
             if (objSearch.REGION_STATE_CD != "")
             {
                 v_REGION_STATE_CD = (objSearch.REGION_STATE_CD);
             }
             if (objSearch.ZONE_CD != "")
             {
                 v_ZONE_CD = (objSearch.ZONE_CD);
             }
            if (objSearch.DISTRICT_CD != "")
            {
                v_DISTRICT_CD =(objSearch.DISTRICT_CD);
            }
            if (objSearch.VDC_MUN_CD != "")
            {
                v_VDC_MUN_CD = (objSearch.VDC_MUN_CD);
            }
            if (objSearch.WARD_NO  != "")
            {
                v_WARD_NO = (objSearch.WARD_NO);
            }
    
            if (objSearch.ENUMERATION_AREA != "")
            {
                v_ENUMERATION_AREA = (objSearch.ENUMERATION_AREA);
            }
            if (objSearch.AREA_ENG != "")
            {
                v_AREA_ENG = (objSearch.AREA_ENG);
            }
            if (objSearch.AREA_LOC != "")
            {
                v_AREA_LOC = (objSearch.AREA_LOC);
            }
            if (objSearch.BANK_CD != "")
            {
                v_BANK_CD = (objSearch.BANK_CD);
            }
            if (objSearch.BANK_BRANCH_CD != "")
            {
                v_BANK_BRANCH_CD = (objSearch.BANK_BRANCH_CD);
            }
            if (objSearch.BANK_ACC_NO != "")
            {
                v_BANK_ACC_NO = (objSearch.BANK_ACC_NO);
            }
            if (objSearch.BANK_ACC_TYPE_CD != "")
            {
                v_BANK_ACC_TYPE_CD = (objSearch.BANK_ACC_TYPE_CD);
            }
            if (objSearch.IS_PAYMENT_RECEIVER_CHANGED != "")
            {
                v_IS_PAYMENT_RECEIVER_CHANGED = (objSearch.IS_PAYMENT_RECEIVER_CHANGED);
            }
            if (objSearch.CHANGED_REASON_ENG != "")
             {
                 v_CHANGED_REASON_ENG = (objSearch.CHANGED_REASON_ENG);
             }
            if (objSearch.CHANGED_REASON_LOC != "")
             {
                 v_CHANGED_REASON_LOC = (objSearch.CHANGED_REASON_LOC);
             }
            if (objSearch.BENEFICIARY_MEMBER_ID != "")
             {
                 v_BENEFICIARY_MEMBER_ID = (objSearch.BENEFICIARY_MEMBER_ID);
             }
            if (objSearch.BENEFICIARY_FNAME_ENG != "")
             {
                 v_BENEFICIARY_FNAME_ENG = (objSearch.BENEFICIARY_FNAME_ENG);
             }
            if (objSearch.BENEFICIARY_MNAME_ENG != "")
             {
                 v_BENEFICIARY_MNAME_ENG = (objSearch.BENEFICIARY_MNAME_ENG);
             }
            if (objSearch.BENEFICIARY_MNAME_ENG != "")
            {
                v_BENEFICIARY_MNAME_ENG = (objSearch.BENEFICIARY_MNAME_ENG);
            }
            if (objSearch.BENEFICIARY_MNAME_ENG != "")
            {
                v_BENEFICIARY_MNAME_ENG = (objSearch.BENEFICIARY_MNAME_ENG);
            }
            if (objSearch.BENEFICIARY_LNAME_ENG != "")
            {
                v_BENEFICIARY_LNAME_ENG = (objSearch.BENEFICIARY_LNAME_ENG);
            }
            if (objSearch.BENEFICIARY_FULLNAME_ENG != "")
            {
                v_BENEFICIARY_FULLNAME_ENG = (objSearch.BENEFICIARY_FULLNAME_ENG);
            }
            if (objSearch.BENEFICIARY_FNAME_LOC != "")
            {
                v_BENEFICIARY_FNAME_LOC = (objSearch.BENEFICIARY_FNAME_LOC);
            }
            if (objSearch.BENEFICIARY_FNAME_LOC != "")
            {
                v_BENEFICIARY_FNAME_LOC = (objSearch.BENEFICIARY_FNAME_LOC);
            }
            if (objSearch.BENEFICIARY_MNAME_LOC != "")
            {
                v_BENEFICIARY_MNAME_LOC = (objSearch.BENEFICIARY_MNAME_LOC);
            }
            if (objSearch.BENEFICIARY_LNAME_LOC != "")
            {
            v_BENEFICIARY_LNAME_LOC = (objSearch.BENEFICIARY_LNAME_LOC);
            }
            if (objSearch.BENEFICIARY_FULLNAME_LOC != "")
            {
                v_BENEFICIARY_FULLNAME_LOC = (objSearch.BENEFICIARY_FULLNAME_LOC);
            }
            if (objSearch.BENEFICIARY_RELATION_TYPE_CD != "")
            {
                v_BENEFICIARY_RELATION_TYPE_CD = (objSearch.BENEFICIARY_RELATION_TYPE_CD);
            }
            if (objSearch.PROXY_MEMBER_ID != "")
            {
                v_PROXY_MEMBER_ID = (objSearch.PROXY_MEMBER_ID);
            }
            if (objSearch.PROXY_FNAME_ENG != "")
            {
                v_PROXY_FNAME_ENG = (objSearch.PROXY_FNAME_ENG);
            }
            if (objSearch.PROXY_MNAME_ENG != "")
            {
                v_PROXY_MNAME_ENG = (objSearch.PROXY_MNAME_ENG);
            }
            if (objSearch.PROXY_LNAME_ENG != "")
            {
                v_PROXY_LNAME_ENG = (objSearch.PROXY_LNAME_ENG);
            }
            if (objSearch.PROXY_FULLNAME_ENG != "")
            {
                v_PROXY_FULLNAME_ENG = (objSearch.PROXY_FULLNAME_ENG);
            }
            if (objSearch.PROXY_FNAME_LOC != "")
            {
                v_PROXY_FNAME_LOC = (objSearch.PROXY_FNAME_LOC);
            }
            if (objSearch.PROXY_MNAME_LOC != "")
            {
                v_PROXY_MNAME_LOC = (objSearch.PROXY_MNAME_LOC);
            }
            if (objSearch.PROXY_LNAME_LOC != "")
            {
                v_PROXY_LNAME_LOC = (objSearch.PROXY_LNAME_LOC);
            }
            if (objSearch.PROXY_LNAME_LOC != "")
            {
                v_PROXY_FULLNAME_LOC = (objSearch.PROXY_FULLNAME_LOC);
            }
            if (objSearch.PROXY_RELATION_TYPE_CD != "")
            {
                v_PROXY_RELATION_TYPE_CD = (objSearch.PROXY_RELATION_TYPE_CD);
            }
            if (objSearch.EMPLOYEE_ID != "")
            {
                v_EMPLOYEE_ID = (objSearch.EMPLOYEE_ID);
            }
            if (objSearch.APPROVED != "")
            {
                v_APPROVED = (objSearch.APPROVED);
            }
            if (objSearch.APPROVED_BY != "")
            {
                v_APPROVED_BY = (objSearch.APPROVED_BY);
            }
            if (objSearch.APPROVED_DT != "")
            {
                v_APPROVED_DT = (objSearch.APPROVED_DT);
            }
            if (objSearch.APPROVED_DT_LOC != "")
            {
                v_APPROVED_DT_LOC = (objSearch.APPROVED_DT_LOC);
            }
            if (objSearch.UPDATED_BY != "")
            {
                v_UPDATED_BY = (objSearch.UPDATED_BY);
            }
            if (objSearch.UPDATED_DT != "")
            {
                v_UPDATED_DT = (objSearch.UPDATED_DT);
            }
            if (objSearch.UPDATED_DT_LOC != "")
            {
                v_UPDATED_DT_LOC = (objSearch.UPDATED_DT_LOC);
            }
            if (objSearch.ENTERED_BY != "")
            {
                v_ENTERED_BY = (objSearch.ENTERED_BY);
            }
            if (objSearch.ENTERED_DT != "")
            {
                v_ENTERED_DT = (objSearch.ENTERED_DT);
            }
            if (objSearch.ENTERED_DT_LOC != "")
            {
                v_ENTERED_DT_LOC = (objSearch.ENTERED_DT_LOC);
            }
            if (objSearch.ip_address != "")
            {
                v_ip_address = (objSearch.ip_address);
            }
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    qr = service.SubmitChanges("PR_NHRS_ENROLLMENT_MOU",
                                                     v_mode , 
                                                     v_TARGET_BATCH_ID,
                                                     v_TARGETING_ID,
                                                     v_ENROLLMENT_ID,
                                                     v_MOU_ID,
                                                     v_ENROLLMENT_MOU_DT,
                                                     v_ENROLLMENT_MOU_DT_LOC,
                                                     v_HOUSE_OWNER_ID,
                                                     v_HO_MEMBER_ID,
                                                     v_BUILDING_STRUCTURE_NO,
                                                     v_HOUSEHOLD_ID,
                                                     v_HOUSEHOLD_DEFINED_CD,
                                                     v_HH_MEMBER_ID,
                                                     v_FIRST_NAME_ENG,
                                                     v_MIDDLE_NAME_ENG,
                                                     v_LAST_NAME_ENG,
                                                     v_FULL_NAME_ENG,
                                                     v_FIRST_NAME_LOC,
                                                     v_MIDDLE_NAME_LOC,
                                                     v_LAST_NAME_LOC,
                                                     v_FULL_NAME_LOC,
                                                     v_MEMBER_PHOTO_PATH,
                                                     v_IDENTIFICATION_TYPE_CD,
                                                     v_IDENTIFICATION_NO,
                                                     v_IDENTIFICATION_DOCUMENT,
                                                     v_IDENTIFICATION_ISSUE_DIS_CD,
                                                     v_COUNTRY_CD,
                                                     v_REGION_STATE_CD,
                                                     v_ZONE_CD,
                                                     v_DISTRICT_CD,
                                                     v_VDC_MUN_CD,
                                                     v_WARD_NO,
                                                     v_ENUMERATION_AREA,
                                                     v_AREA_ENG,
                                                     v_AREA_LOC,
                                                     v_BANK_CD,
                                                     v_BANK_BRANCH_CD,
                                                     v_BANK_ACC_NO,
                                                     v_BANK_ACC_TYPE_CD,
                                                     v_IS_PAYMENT_RECEIVER_CHANGED,
                                                     v_CHANGED_REASON_ENG,
                                                     v_CHANGED_REASON_LOC,
                                                     v_BENEFICIARY_MEMBER_ID,
                                                     v_BENEFICIARY_FNAME_ENG,
                                                     v_BENEFICIARY_MNAME_ENG,
                                                     v_BENEFICIARY_LNAME_ENG,
                                                     v_BENEFICIARY_FULLNAME_ENG,
                                                     v_BENEFICIARY_FNAME_LOC,
                                                     v_BENEFICIARY_MNAME_LOC,
                                                     v_BENEFICIARY_LNAME_LOC,
                                                     v_BENEFICIARY_FULLNAME_LOC,
                                                     v_BENEFICIARY_RELATION_TYPE_CD,
                                                     v_PROXY_MEMBER_ID,
                                                     v_PROXY_FNAME_ENG,
                                                     v_PROXY_MNAME_ENG,
                                                     v_PROXY_LNAME_ENG,
                                                     v_PROXY_FULLNAME_ENG,
                                                     v_PROXY_FNAME_LOC,
                                                     v_PROXY_MNAME_LOC,
                                                     v_PROXY_LNAME_LOC,
                                                     v_PROXY_FULLNAME_LOC,
                                                     v_PROXY_RELATION_TYPE_CD,
                                                     v_EMPLOYEE_ID,
                                                     v_APPROVED,
                                                     v_APPROVED_BY,
                                                     v_APPROVED_DT,
                                                     v_APPROVED_DT_LOC,
                                                     v_UPDATED_BY,
                                                     v_UPDATED_DT,
                                                     v_UPDATED_DT_LOC,
                                                     v_ENTERED_BY,
                                                     v_ENTERED_DT,
                                                     v_ENTERED_DT_LOC,
                                                     v_ip_address 




              );
   
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    qr = null;
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
            if (qr != null && qr.IsSuccess)
            {
              
               
            }
            return qr;

        }


        public DataTable GetBeneficiarySelection(string Houseownerid, string buildingstrno, string memberid)
        {
            DataTable dt = new DataTable();
            Object P_HOUSEOWNER_ID = DBNull.Value;
            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;
            Object P_HOUSEHOLD_ID = DBNull.Value;
            Object P_MEMBER_ID = DBNull.Value;
            Object P_Out_BeneficiaryList = DBNull.Value;
            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "100";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
            if (Houseownerid != "")
            {
                P_HOUSEOWNER_ID = Decimal.Parse(Houseownerid);
            }
            if (buildingstrno != "")
            {
                P_BUILDING_STRUCTURE_NO = Decimal.Parse(buildingstrno);
            }
            if (memberid != "")
            {
                P_MEMBER_ID = Decimal.Parse(memberid);
            }
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_EN_BENEFICIARY_SELECTION",
                                                P_HOUSEOWNER_ID,
                                                P_BUILDING_STRUCTURE_NO,
                                                P_HOUSEHOLD_ID,
                                                P_MEMBER_ID,
                                            P_Out_BeneficiaryList
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
        public DataTable GetBeneficiarySelection(string buildingstrno, string Houseownerid)
        {
            DataTable dt = new DataTable();
            Object P_HOUSEOWNER_ID = DBNull.Value;
            Object P_BUILDING_STRUCTURE_NO = DBNull.Value;
            Object P_HOUSEHOLD_ID = DBNull.Value;
            Object P_MEMBER_ID = DBNull.Value;
            Object P_Out_BeneficiaryList = DBNull.Value;
            Object SortBy = DBNull.Value;
            Object SortOrder = DBNull.Value;
            Object PageSize = "100";
            Object PageIndex = "1";
            Object ExportExcel = "N";
            Object Lang = "E";
            Object FilterWord = DBNull.Value;
            if (Houseownerid != "")
            {
                P_HOUSEOWNER_ID = Decimal.Parse(Houseownerid);
            }
            if (buildingstrno != "")
            {
                P_BUILDING_STRUCTURE_NO = Decimal.Parse(buildingstrno);
            }
            //if (memberid != "")
            //{
            //    P_MEMBER_ID = Decimal.Parse(memberid);
            //}
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true, "PR_EN_BENEFICIARY_SELECTION",
                                                P_HOUSEOWNER_ID,
                                                P_BUILDING_STRUCTURE_NO,
                                                 DBNull.Value,
                                                DBNull.Value,
                                            P_Out_BeneficiaryList
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


        //public DataTable GetProxyBeneficiarySelection(string buildingstrno, string HouseOwnerID)
        //{
        //    enrollmentclass obj = new enrollmentclass();
        //    DataTable dtbl = null;
        //    try
        //    {
                
        //        using (ServiceFactory service = new ServiceFactory())
        //        {
        //            service.Begin();

        //            string cmdText = String.Format("SELECT * FROM MIS_MEMBER WHERE BUILDING_STRUCTURE_NO = '1' and HOUSE_OWNER_ID='" + HouseOwnerID + "'"); 
        //            try
        //            {
        //                dtbl = service.GetDataTable(cmdText, null);
        //            }
        //            catch (Exception ex)
        //            {
        //                ExceptionManager.AppendLog(ex);
        //            }
        //            finally
        //            {

        //                if (service.Transaction != null)
        //                {
        //                    service.End();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj = null;
        //        ExceptionManager.AppendLog(ex);
        //    }
        //    return dtbl;
        //}

        //public DataTable GetBeneficiarySelection(string buildingstrno, string HouseOwnerID)
        //{
        //    enrollmentclass obj = new enrollmentclass();
        //    DataTable dtbl = null;
        //    try
        //    {

        //        using (ServiceFactory service = new ServiceFactory())
        //        {
        //            service.Begin();

        //            string cmdText = String.Format("SELECT * FROM MIS_MEMBER WHERE BUILDING_STRUCTURE_NO = '1'  and HOUSE_OWNER_ID='" + HouseOwnerID + "'");
        //            try
        //            {
        //                dtbl = service.GetDataTable(cmdText, null);
        //            }
        //            catch (Exception ex)
        //            {
        //                ExceptionManager.AppendLog(ex);
        //            }
        //            finally
        //            {

        //                if (service.Transaction != null)
        //                {
        //                    service.End();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj = null;
        //        ExceptionManager.AppendLog(ex);
        //    }
        //    return dtbl;



        //}

        public DataTable GetEnrollmentDetailEdit(string HouseOwnerID, string enrollmentid)
        {
            enrollmentclass objEnrl = new enrollmentclass();
            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;
              
            
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    dt = service.GetDataTable(true,"PR_ENROLLMENT_EDIT",
                                                     HouseOwnerID,
                                                     enrollmentid,
                                                     P_Out_Enrollment
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
        public DataTable GetEnrollmentDetailEditDocument(string HouseOwnerID)
        {
            enrollmentclass objEnrl = new enrollmentclass();
            DataTable dt = new DataTable();
            String docData = String.Empty;
            Object P_Out_Enrollment = DBNull.Value;


            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    service.Begin();
                    service.PackageName = "PKG_HH_DOC_DTL";
                    dt = service.GetDataTable(true, "PR_GET_HH_DOC_DTL",
                                                     HouseOwnerID,
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

        public bool DeleteEnrollmentDocument(string docType, string HOUSE_OWNER_ID)
        {
            bool retValue = false;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = String.Format("delete from MIS_HH_DOC_DTL where HOUSE_OWNER_ID='" + HOUSE_OWNER_ID + "' and DOC_TYPE_CD='" + docType + "'");

                    try
                    {
                        service.SubmitChanges(cmdText, null);
                        retValue = true;
                    }
                    catch (Exception)
                    {

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
            return retValue;
        }
        public EnrollmentSearch GenerateCertificateData(string id, string pa)
        {
            EnrollmentSearch objEnrl = new EnrollmentSearch();
            String docData = String.Empty;
            DataTable dt = new DataTable();
            Object V_MEMBER_ID = DBNull.Value;
            Object V_FIRST_NAME_ENG = DBNull.Value;
            Object V_MIDDLE_NAME_ENG = DBNull.Value;
            Object V_LAST_NAME_ENG = DBNull.Value;
            Object V_FULL_NAME_ENG = DBNull.Value;
            Object V_FIRST_NAME_LOC = DBNull.Value;
            Object V_MIDDLE_NAME_LOC = DBNull.Value;
            Object V_LAST_NAME_LOC = DBNull.Value;
            Object V_FULL_NAME_LOC = DBNull.Value;
            Object V_GENDER = DBNull.Value;
            Object V_DISTRICT_CD = DBNull.Value;
            Object V_DISTRICT_ENG = DBNull.Value;
            Object V_DISTRICT_LOC = DBNull.Value;
            Object V_VDC_CD = DBNull.Value;
            Object V_VDC_MUN_ENG = DBNull.Value;
            Object V_VDC_MUN_LOC = DBNull.Value;
            Object V_WARD_NO = DBNull.Value;
            Object V_AREA_ENG = DBNull.Value;
            Object V_AREA_LOC = DBNull.Value;
            Object V_ENUMERATOR_ID = DBNull.Value;
            Object V_PHOTO_PATH = DBNull.Value;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {

                    string[] PaSplit = pa.Split('-');
                    service.Begin();
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    
                    if (PaSplit[0] == "R")
                    {
                        dt = service.GetDataTable(true, "PR_NHRS_ENROL_RETRO_PRINT_VIEW",
                                               id,
                                               pa,
                                               DBNull.Value
                                              );
                    }
                    else if (PaSplit[0] == "G")
                    {
                        dt = service.GetDataTable(true, "PR_NHRS_ENROL_GRIE_PRINT_VIEW",
                                               id,
                                               pa,
                                               DBNull.Value
                                              );
                    }
                    else
                    {
                        dt = service.GetDataTable(true, "PR_NHRS_ENROLLMENT_PRINT_VIEW",
                                               id.ToDecimal(),
                                               pa.ConvertToString(),
                                               DBNull.Value
                                              );
                    }


                    //service.Begin();
                    //service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    //dt = service.GetDataTable(true, "PR_NHRS_ENROLLMENT_PRINT_VIEW",
                    //                            id,  
                    //                            pa,
                    //                            DBNull.Value);
                   // dt = service.GetDataTable(true, "PR_NHRS_ENROLLMENT_PRINT_VIEW", id, DBNull.Value);
                                                   
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    
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
            if (dt != null &&dt.Rows.Count>0)
            {
                objEnrl.WardNo = Utils.GetLabel(dt.Rows[0]["WARD_NO"].ConvertToString());
                objEnrl.DISTRICT_ENG = dt.Rows[0]["DISTRICT_ENG"].ConvertToString();
                objEnrl.DISTRICT_LOC = dt.Rows[0]["DISTRICT_LOC"].ConvertToString();
                objEnrl.AREA_ENG = dt.Rows[0]["AREA_ENG"].ConvertToString();
                objEnrl.AREA_LOC = dt.Rows[0]["AREA_LOC"].ConvertToString();
                objEnrl.RESPONDENT_IS_HOUSE_OWNER = dt.Rows[0]["RESPONDENT_IS_HOUSE_OWNER"].ConvertToString();
                objEnrl.ENUMERATOR_ID = Utils.GetLabel(dt.Rows[0]["ENUMERATOR_ID"].ConvertToString());
                objEnrl.VDC_ENG = dt.Rows[0]["VDC_MUN_ENG"].ConvertToString();
                objEnrl.VDC_LOC = dt.Rows[0]["VDC_MUN_LOC"].ConvertToString();
                objEnrl.House_Owner_Name = dt.Rows[0]["HOUSE_OWNER_NAME_ENG"].ConvertToString();
                objEnrl.House_Owner_Name_Loc = dt.Rows[0]["HOUSE_OWNER_NAME_LOC"].ConvertToString();
                objEnrl.House_Owner_ID = Utils.GetLabel(dt.Rows[0]["HOUSE_OWNER_ID"].ConvertToString());

                objEnrl.HOUSE_ID = Utils.GetLabel(dt.Rows[0]["HOUSE_ID"].ConvertToString());
                objEnrl.HOUSEHOLD_ID = Utils.GetLabel(dt.Rows[0]["HOUSEHOLD_ID"].ConvertToString());
                objEnrl.BENEFICIARY_ID = Utils.GetLabel(dt.Rows[0]["NRA_DEFINED_CD"].ConvertToString());
            }
            return objEnrl;
        }
        public String getPrintStatus(string LetterType)
        {
            String docData = String.Empty;
            DataTable dtbl = null;
            string cmdText = "";
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();

                    cmdText = "select * from NHRS_LETTER_FORMAT  where 1=1";
                    if (!string.IsNullOrWhiteSpace(LetterType))
                    {
                        cmdText = cmdText + " and LETTER_TYPE='" + LetterType + "'";
                    }
                    try
                    {
                        dtbl = service.GetDataTable(cmdText, null);
                    }
                    catch (Exception ex)
                    { ExceptionManager.AppendLog(ex); }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl.Rows.Count > 0)
                {
                    docData = dtbl.Rows[0]["DOC_DATA"].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return docData;
        }

        public bool UpdatePrintStatus(string status, string houseOwnerID)
        {
            bool retValue = false;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = String.Format("update NHRS_ENROLLMENT_MST set PRINT_STATUS='" + status + "' where HOUSE_OWNER_ID='" + houseOwnerID + "'");

                    try
                    {
                        service.SubmitChanges(cmdText, null);
                        retValue = true;
                    }
                    catch (Exception)
                    {

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
            return retValue;
        }
        public bool UpdateApproveUnapproveStatus(string status, string houseOwnerID)
        {
            bool retValue = false;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = String.Format("update NHRS_ENROLLMENT_MST set APPROVED='" + status + "' where HOUSE_OWNER_ID='" + houseOwnerID + "'");

                    try
                    {
                        service.SubmitChanges(cmdText, null);
                        retValue = true;
                    }
                    catch (Exception)
                    {

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
            return retValue;
        }

        public void UpdateEnrollmentDetails(enrollmentclass objenroll)
        {
            enrollmentInfoclass enroll = new enrollmentInfoclass();
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    enroll.ENROLLMENT_ID = objenroll.ENROLLMENT_ID.ToDecimal();
                    enroll.TARGETING_ID = objenroll.TARGETING_ID.ToDecimal();
                    enroll.HOUSE_OWNER_ID = objenroll.HOUSE_OWNER_ID;
                    enroll.TARGET_BATCH_ID = objenroll.TARGETING_BATCH_ID.ToDecimal();
                    enroll.BUILDING_STRUCTURE_NO = objenroll.BUILDING_STRUCTURE_NO;
                    enroll.DISTRICT_CD = objenroll.DISTRICT_CD.ToDecimal();
                    enroll.VDC_MUN_CD = objenroll.VDC_MUN_CD;
                    enroll.FIRST_NAME_ENG = objenroll.FIRST_NAME_ENG;
                    enroll.MIDDLE_NAME_ENG = objenroll.MIDDLE_NAME_ENG;
                    enroll.LAST_NAME_ENG = objenroll.LAST_NAME_ENG;
                    enroll.MEMBER_PHOTO_PATH = objenroll.MEMBER_PHOTO_PATH;
                    enroll.BENEFICIARY_FNAME_ENG = objenroll.BENEFICIARY_FNAME_ENG;
                    enroll.BENEFICIARY_LNAME_ENG = objenroll.BENEFICIARY_LNAME_ENG;
                    enroll.BENEFICIARY_MNAME_ENG = objenroll.BENEFICIARY_MNAME_ENG;
                    enroll.BENEFICIARY_RELATION_TYPE_CD = objenroll.BENEFICIARY_RELATION_TYPE_CD.ToDecimal();
                    enroll.PROXY_FNAME_ENG = objenroll.PROXY_FNAME_ENG;
                    enroll.PROXY_LNAME_ENG = objenroll.PROXY_LNAME_ENG;
                    enroll.PROXY_MNAME_ENG = objenroll.PROXY_MNAME_ENG;
                    enroll.PROXY_RELATION_TYPE_CD = objenroll.PROXY_RELATION_TYPE_CD.ToDecimal();
                    enroll.BANK_CD = objenroll.BANK_CD.ToDecimal();
                    enroll.BANK_BRANCH_CD = objenroll.BANK_BRANCH_CD.ToDecimal();
                    enroll.BANK_ACC_NO = objenroll.BANK_ACC_NO;
                    enroll.BANK_ACC_TYPE_CD = objenroll.BANK_ACC_TYPE_CD;
                    enroll.IDENTIFICATION_TYPE_CD = objenroll.IDENTIFICATION_TYPE_CD;
                    enroll.IDENTIFICATION_NO = objenroll.IDENTIFICATION_NO;
                    enroll.IDENTIFICATION_DOCUMENT = objenroll.IDENTIFICATION_DOCUMENT;
                    enroll.IDENTIFICATION_ISSUE_DIS_CD = objenroll.IDENTIFICATION_ISSUE_DIS_CD;
                    enroll.UPDATED_BY = SessionCheck.getSessionUsername();
                    enroll.Mode = objenroll.MODE;
                    service.PackageName = "PKG_NHRS_TARGET_ENROLLMENT";
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(enroll, true);

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
    }
}
