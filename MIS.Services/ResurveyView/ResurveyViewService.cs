using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using MIS.Services.Core;
using System.Text;
using MIS.Models.NHRP.Resurvey;

namespace MIS.Services.ResurveyView
{
    public class ResurveyViewService
    {
        public DataSet ResurveyHouseView(string OwnerId, string BeneficiaryId)
        
        {
            DataSet ds = new DataSet();
            using(ServiceFactory sf = new ServiceFactory())
            {
                //OwnerId = "743";
                try
                {
                    sf.Begin();
                    sf.PackageName = "PKG_NHRS_RESURVEY_VIEW";
                    ds = sf.GetDataSet(true, "PR_BUILDING_DAMAGE_DTL",
                                      DBNull.Value,  //mode
                                      OwnerId.ToDecimal(),
                                      DBNull.Value,//pa
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value,
                                      DBNull.Value
                                      );
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    sf.End();
                }
                 
            }
             
            return ds;
        }

        public DataTable GrievMemDetail(string householdId)
        {
            DataTable ds = null;

            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    ds = service.GetDataTable(true, "GET_RES_MEMBER_DETAIL",



                        householdId,
                        DBNull.Value

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
            }
            return ds;
        }
        public DataTable GetSurveyData(String HouseOwnerId)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    sf.PackageName = "PKG_NHRS_RESURVEY_VIEW";
                    dt = sf.GetDataTable(true, "PR_RESURVEY_SEARCH_BY_ONA_ID",
                                 HouseOwnerId.ToDecimal(),
                                        DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }
        public DataTable GetOldHouseOwnerId(string GID, string SlipNo)
        {
            DataTable dt = new DataTable();
            string cmdText = "";
            if(!string.IsNullOrEmpty(GID))
            {
                  cmdText = "select house_owner_id from nhrs_grievance_registration where GID = " + GID;
            }
            else if (!string.IsNullOrEmpty(SlipNo))
            {
                cmdText = "SELECT HOUSE_OWNER_ID from MIS_HOUSEHOLD_INFO where INSTANCE_UNIQUE_SNO = " + SlipNo;
            }
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }
        public DataTable GetRecommendedData(string HouseOwnerId)
        {
            DataTable dt = new DataTable();
            string cmdText = "Select * from RES_RESURVEY_VERIFICATION where house_owner_id = " + HouseOwnerId + " order by stc_no";
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }
        public DataTable IsOwnerIdVerified(string HouseOwnerId)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(true,"PR_Is_OWNERID_Verified",
                                 HouseOwnerId.ToDecimal(),
                                 DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }

        public DataTable GetResurveyReviewList(ResurveyHouseViewModel ObjModel, string CaseType)
        {
            DataTable dt = new DataTable();
            
          
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                                    
                {
                    sf.Begin();
                    //sf.PackageName = "PKG_NHRS_RESURVEY_VIEW";
                    dt = sf.GetDataTable(true, " ",
                                        DBNull.Value,
                                        ObjModel.OwnDistrictCd.ToDecimal(),
                                        ObjModel.CurrentVdcMunCD.ToDecimal(),
                                        ObjModel.CurrentWard.ToDecimal(),
                                        ObjModel.OwnerFullNameEng.ConvertToString(),
                                        ObjModel.InstanceUniqueSno.ConvertToString(),
                                         
                                        CaseType.ToDecimal(),
                                        ObjModel.GID,
                                        DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }

        public QueryResult InsertResRecommendation(List<ResurveyRecommendation> ObjModel)
        {
            QueryResult qr = null;
            CommonFunction fc = new CommonFunction();
            string entered_by = SessionCheck.getSessionUsername();
            string mode = "";

            

            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    foreach (var item in ObjModel)
                    {
                        if(item.mode.ConvertToString() == "UPDATE")
                        {
                            mode = "U";
                        }
                        else
                        {
                            mode = "I";
                        }
                        if(item.lstGID.Count > 0)
                        {
                            foreach (var x in item.lstGID)
                            {
                                qr = sf.SubmitChanges("PR_RES_VERIFICATION",
                                     mode,
                                     DBNull.Value,
                                     item.hoid.ConvertToString(),
                                     x,
                                     item.mdg.ConvertToString(),
                                     item.pdg.ConvertToString(),
                                     item.rdg.ConvertToString(),
                                     item.rts.ConvertToString(),
                                     item.recommendation.ConvertToString(),
                                     item.remarks.ConvertToString(),
                                     entered_by,
                                     item.stc_cnt.ToDecimal(),
                                     item.Proximity,
                                     item.building_type,
                                     item.major_damage_cd,
                                     item.major_damage_desc,
                                     item.previous_grade,
                                     item.new_grade,
                                     item.reason
                                    );
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
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
               
            }

            return qr;
        }

        //
        public DataTable GetResurveyReviewList(ResurveyHouseViewModel ObjModel)
        {
            DataTable dt = new DataTable();
            Object p_interview_dt_from = DBNull.Value;
            Object p_interview_dt_to = DBNull.Value;
            if (ObjModel.INTERVIEW_DT_FROM.ConvertToString() != "")
            {
                ObjModel.INTERVIEW_DT_FROM.ToDateTime();
            }
            if (ObjModel.INTERVIEW_DT_TO.ConvertToString() != "")
            {
                ObjModel.INTERVIEW_DT_TO.ToDateTime();
            }
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(true, "PR_Resurvey_Review_Search",
                                        ObjModel.OwnDistrictCd.ToDecimal(),
                                        ObjModel.CurrentVdcMunCD.ToDecimal(),
                                        ObjModel.CurrentWard.ToDecimal(),
                                        ObjModel.SurveyType.ConvertToString(),
                                        ObjModel.GID_Status.ConvertToString(),
                                        ObjModel.Current_Recommendation.ConvertToString(),
                                        ObjModel.Previous_Recommendation.ConvertToString(),
                                        ObjModel.Eng_Verification_Status.ConvertToString(),
                                        ObjModel.GID.ConvertToString(),
                                        ObjModel.InstanceUniqueSno.ConvertToString(),
                                        DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }

        public DataTable GetResurveyList(ResurveyHouseViewModel ObjModel)
        {
            DataTable dt = new DataTable();
            Object p_interview_dt_from = DBNull.Value;
            Object p_interview_dt_to = DBNull.Value;
            if (ObjModel.INTERVIEW_DT_FROM.ConvertToString() != "")
            {
                ObjModel.INTERVIEW_DT_FROM.ToDateTime();
            }
            if (ObjModel.INTERVIEW_DT_TO.ConvertToString() != "")
            {
                ObjModel.INTERVIEW_DT_TO.ToDateTime();
            }
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    sf.PackageName = "PKG_NHRS_RESURVEY_VIEW";
                    dt = sf.GetDataTable(true, "PR_RESURVEY_SEARCH_WITH_GID",
                                        DBNull.Value,
                                        ObjModel.HouseOwnerId.ToDecimal(),
                                        ObjModel.OwnDistrictCd.ToDecimal(),
                                        ObjModel.CurrentVdcMunCD.ToDecimal(),
                                        ObjModel.CurrentWard.ToDecimal(),
                                        ObjModel.OwnerFullNameEng.ConvertToString(),
                                        ObjModel.InstanceUniqueSno.ConvertToString(),
                                        ObjModel.TOTAL_OTHER_HOUSE_CNT_FROM.ConvertToString(),
                                        ObjModel.TOTAL_OTHER_HOUSE_CNT_TO.ConvertToString(),
                                        ObjModel.STRUCTURE_COUNT_FROM.ConvertToString(),
                                        ObjModel.STRUCTURE_COUNT_TO.ConvertToString(),
                                        ObjModel.OwnerCount.ConvertToString(),
                                        p_interview_dt_from,
                                        p_interview_dt_to,
                                        ObjModel.MobileNumber,
                                        ObjModel.GID,
                                        DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                sf.End();
            }

            return dt;
        }

        public DataTable GetEngVerificationRpt(ResurveyHouseViewModel ObjModel)
        {
            DataTable dt = new DataTable();
            Object p_district_cd = ObjModel.OwnDistrictCd.ToDecimal();
            Object p_gp_np_cd = ObjModel.CurrentVdcMunCD.ToDecimal();
            Object p_ward = ObjModel.CurrentWard.ToDecimal();
            Object p_house_owner_id = ObjModel.HouseOwnerId;
            Object p_slip_no = ObjModel.InstanceUniqueSno;
            Object p_gid = ObjModel.GID;
            Object p_name = ObjModel.OwnerFullNameEng;
            Object p_type = ObjModel.GID_Status;
           
            using (ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.Begin();
                    dt = sf.GetDataTable(true, "PR_ENG_VERIFICATION_RPT",
                                        p_district_cd,
                                        p_gp_np_cd,
                                        p_ward,
                                        p_house_owner_id,
                                        p_slip_no,
                                        p_gid,
                                        p_name,
                                        p_type,
                                        DBNull.Value);
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }

                finally
                {
                    sf.End();
                }
            }

            return dt;
        }
        //Re-Survey Reconstruction Beneficiary Report
        public DataTable GetReconstructionBeneficiarySummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";




            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_BENEF_SUM_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        public DataTable GetReconstructionBeneficiaryDetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";




            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_BENEF_DTL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        //Re-Survey Building Damage Report
        public DataTable GetReSurveyBuildingDamageSummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;
            Object _pOutResult = "";


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
            if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
                P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
            if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")
                P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_RES_HH_SURVEY_SUMMARY_RPT",
                    paramValues["lang"].ConvertToString(),
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        public DataTable GetReSurveyBuildingDamageDetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_OTHHOUSEFLAG = DBNull.Value;
            Object P_DAMAGE_GRADE = DBNull.Value;
            Object P_TECHNICAL_SOLUTION = DBNull.Value;
            Object P_BUILDING_CONDTION = DBNull.Value;
            Object _pOutResult = "";

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["DamageGrade"].ConvertToString() != string.Empty && paramValues["DamageGrade"].ConvertToString() != "undefined")
                P_DAMAGE_GRADE = paramValues["DamageGrade"].ConvertToString();
            if (paramValues["TechnicalSolution"].ConvertToString() != string.Empty && paramValues["TechnicalSolution"].ConvertToString() != "undefined")
                P_TECHNICAL_SOLUTION = paramValues["TechnicalSolution"].ConvertToString();
            if (paramValues["OthHouse"].ConvertToString() != string.Empty && paramValues["OthHouse"].ConvertToString() != "undefined")
                P_OTHHOUSEFLAG = paramValues["OthHouse"].ConvertToString();
            if (paramValues["BuildingCondition"].ConvertToString() != string.Empty && paramValues["BuildingCondition"].ConvertToString() != "undefined")
                P_BUILDING_CONDTION = paramValues["BuildingCondition"].ConvertToString();


            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "PR_RES_BUIDING_DTL",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_DAMAGE_GRADE,
                    P_TECHNICAL_SOLUTION,
                    _pOutResult);
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

        //Re-Survey Non-Beneficiary Report
        public DataTable GetResurveyNonBeneficiarySummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_NON_BENEF_SUM_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        public DataTable GetResurveyNonBeneficiaryDetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_NON_BENEF_DTL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        //Re-Survey Retrofitting Beneficiary
        public DataTable GetRetrofittingBeneficiarySummaryReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_RETRO_BENEF_SUM_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        public DataTable GetRetrofittingBeneficiaryDetailReport(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_RETRO_BENEF_DTL_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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

        //ReSurvey All combined Detail Data
        public DataTable GetAllResurveyDetailData(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_Recommend = DBNull.Value;
            Object P_RSRv = DBNull.Value;
            Object _pOutResult = "";

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["RecommendationType"].ConvertToString() != string.Empty)
                P_Recommend = paramValues["RecommendationType"].ConvertToString();
            if (paramValues["RSRVtype"].ConvertToString() != string.Empty)
                P_RSRv = paramValues["RSRVtype"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "pr_RES_common_dtl_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_Recommend,
                    P_RSRv,
                    _pOutResult);
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
        //EngVerifn
        public DataTable GetEngVerificationData(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object P_Recommend = DBNull.Value;

            Object p_OutResult = "";

            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();
            if (paramValues["RecommendationType"].ConvertToString() != string.Empty)
                P_Recommend = paramValues["RecommendationType"].ConvertToString();
          

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "pr_RES_RESURVEY_VER_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    P_Recommend,  
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

        public DataTable GetAllResurveySummaryData(NameValueCollection paramValues)
        {
            DataTable dt = null;
            ServiceFactory service = new ServiceFactory();
            Object P_Dist = DBNull.Value;
            Object P_VDC = DBNull.Value;
            Object P_WARD = DBNull.Value;
            Object _pOutResult = "";


            if (paramValues["dist"].ConvertToString() != string.Empty)
                P_Dist = paramValues["dist"].ConvertToString();
            if (paramValues["vdc"].ConvertToString() != string.Empty)
                P_VDC = paramValues["vdc"].ConvertToString();
            if (paramValues["ward"].ConvertToString() != string.Empty)
                P_WARD = paramValues["ward"].ConvertToString();

            try
            {
                service.Begin();
                dt = service.GetDataTable(true, "NHRS.PR_RES_COMMON_sum_REPORT",
                    paramValues["lang"].ConvertToString(),
                    P_Dist,
                    P_VDC,
                    P_WARD,
                    _pOutResult);
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
    }
}
