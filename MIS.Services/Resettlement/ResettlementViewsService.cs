using EntityFramework;
using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Data.OracleClient;

namespace MIS.Services.Resettlement
{
    public class ResettlementViewsService
    {
        public DataTable getDataById(string id)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {
                    string cmdText = "SELECT "

                   + "NRB.RESETTLEMENT_ID         ,   NRB.DEFINED_CD                , NRB.RESPONDANT_F_NAME         , NRB.RESPONDANT_M_NAME         , NRB.RESPONDANT_L_NAME         ,    "
                   + "NRB.FATHERS_F_NAME            , NRB.FATHERS_M_NAME            , NRB.FATHERS_L_NAME            , NRB.GRAND_FF_NAME             , NRB.GRAND_FM_NAME             ,    "
                   + "NRB.GRAND_FL_NAME             , NRB.BENEFICIARY_F_NAME        , NRB.BENEFICIARY_M_NAME        , NRB.BENEFICIARY_L_NAME        , NRB.AGE                       ,    "
                   + "NRB.FAMILY_MEMBER_CNT         , NRB.DISTRICT_CD               , NRB.VDC_MUN_CD                , NRB.WARD                      , NRB.TOLE                      ,    "
                   + "NRB.CITIZENSHIP_NO            , NRB.NRA_DEFINED_CD            , NRB.ENUMERATION_AREA          , NRB.HH_SN                     , NRB.SLIP_NO                   ,    "
                   + "NRB.MIS_REVIEW                , NRB.PHONE_NUMBER              , NRB.ENGINEERS_REMARKS         , DIS.DESC_ENG as DIST_ENG      , DIS.DESC_LOC as DIST_LOC      ,    "
                   + "VDC.DESC_ENG as VDC_ENG       , VDC.DESC_LOC as VDC_LOC       , NRS.RESETTLEMENT_ID SURVEYED  , NRS.NEW_REMARKS               , NRS.MIS_REVIEW as NEW_REVIEW"
                + " FROM NHRS_RESETTLEMENT_BENF NRB "

                + "left outer join NHRS_RESETTLEMENT_SURVEY NRS ON NRB.RESETTLEMENT_ID= NRS.RESETTLEMENT_ID "
                + "left outer JOIN MIS_DISTRICT DIS ON NRB.DISTRICT_CD = DIS.DISTRICT_CD "
                + "left outer JOIN MIS_VDC_MUNICIPALITY VDC ON NRB.VDC_MUN_CD = VDC.VDC_MUN_CD "
                + "WHERE NRB.RESETTLEMENT_ID='" + id.ConvertToString() + "'";




                    dt = sf.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
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
              
            return dt;

        }


        public bool SaveResettlementComply(string mode, string districtCd, string VdcMunCd, string Ward, string ResettlementId, string HouseOwnerId, string MisReview, string NewRemarks)
        {
            DataTable dt = new DataTable();
            QueryResult  qr=null;
            bool result = false;
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_RESETTLEMENT_BENF";
                    if(mode=="I")
                    {
                        qr = sf.SubmitChanges("GET_RESETTLEMENT_SURV_COMPLY",
                     "I",
                     DBNull.Value,
                     ResettlementId.ToDecimal(),
                     HouseOwnerId,
                     districtCd.ToDecimal(),
                     VdcMunCd.ToDecimal(),
                     Ward.ToDecimal(),
                     DBNull.Value,//status
                     DBNull.Value,//approved
                     DBNull.Value,//approved by
                     DBNull.Value,//approved date
                     DBNull.Value,//approved date loc  
                     SessionCheck.getSessionUsername(),//entered by
                     DateTime.Now,//entered date
                     System.DateTime.Now.ConvertToString(),//entered date loc
                     DBNull.Value,//updated by
                     DBNull.Value,//updated date
                     DBNull.Value,//updated date loc
                     MisReview,
                     NewRemarks
                     );
                    }
                    else
                    {
                        qr = sf.SubmitChanges("GET_RESETTLEMENT_SURV_COMPLY",
                     "U",
                     DBNull.Value,
                     ResettlementId.ToDecimal(),
                     HouseOwnerId,
                     districtCd.ToDecimal(),
                     VdcMunCd.ToDecimal(),
                     Ward.ToDecimal(),
                     DBNull.Value,//status
                     DBNull.Value,//approved
                     DBNull.Value,//approved by
                     DBNull.Value,//approved date
                     DBNull.Value,//approved date loc  
                     
                     DBNull.Value,//entered by
                     DBNull.Value,//entered date
                     DBNull.Value,//enetered date loc

                     SessionCheck.getSessionUsername(),//updated by
                     DateTime.Now,//updated date
                     System.DateTime.Now.ConvertToString(),//updated date loc
                     MisReview,
                     NewRemarks
                     );
                    }
                    

                }
                catch (OracleException oe)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    sf.RollBack();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (qr != null)
                {
                    result = qr.IsSuccess;
                }
            }
                return result;

        }

        public DataTable getSurveyData(string dist, string vdc, string ward, string Lan, string paNumber, string NissaNo)
        {
            DataTable dt = new DataTable();
            string PaType = null;

            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {
                    //if (paNumber != "" && paNumber != null)
                    //{
                        
                    //    string NissaNo1 = null;
                    //    string dist1 = null;
                    //    string vdc1 = null;
                    //    string ward1 = null;
                    //    string[] splitPa = paNumber.Split('-');
                    //    PaType = splitPa[0].ConvertToString();
                        

                    //    dt = sf.GetDataTable(true, "nhrs_resettlement_surv_search",
                    //    Lan,
                    //    dist1.ToDecimal(),
                    //    vdc1.ToDecimal(),
                    //    ward1.ToDecimal(),
                    //    paNumber.ConvertToString(),
                    //    DBNull.Value,
                    //    NissaNo1.ConvertToString(),
                    //    PaType.ConvertToString()

                    //    );
                    //}
                    //if (dt.Rows.Count < 1 && NissaNo != "" && NissaNo!=null)
                    //{
                         
                    //    string paNumber1 = null;
                    //    string dist1 = null;
                    //    string vdc1 = null;
                    //    string ward1 = null;

                    //    dt = sf.GetDataTable(true, "nhrs_resettlement_surv_search",
                    //    Lan,
                    //    dist1.ToDecimal(),
                    //    vdc1.ToDecimal(),
                    //    ward1.ToDecimal(),
                    //    paNumber1.ConvertToString(),
                    //    DBNull.Value,
                    //    NissaNo.ConvertToString(),
                    //    PaType.ConvertToString()

                    //    );
                    //}
                    if (dt.Rows.Count < 1)
                    {
                        //paNumber = null;
                        //NissaNo = null;
                        if (paNumber != "" && paNumber != null)
                        {
                            string[] splitPa = paNumber.Split('-');
                            if(splitPa.Length==6)
                            {
                                PaType = splitPa[0].ConvertToString();
                            }
                           

                        }

                        
                        dt = sf.GetDataTable(true, "nhrs_resettlement_surv_search",
                        Lan,
                        dist.ToDecimal(),
                        vdc.ToDecimal(),
                        ward.ToDecimal(),
                        paNumber.ConvertToString(),
                        DBNull.Value,
                        NissaNo.ConvertToString(),
                        PaType.ConvertToString()

                        );
                    }

                }
                catch (Exception ex)
                {
                    dt = null;
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

            return dt;

        }

        public bool checkIfBenfVerified(string id)
        {
            DataTable dt = new DataTable();
            bool result = false;
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {
                    string cmdText = "SELECT RESETTLEMENT_ID from NHRS_RESETTLEMENT_SURVEY where RESETTLEMENT_ID='" + id + "'"; 
                    dt = sf.GetDataTable(cmdText, null);

                }
                catch (Exception ex)
                {
                    dt = null;
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if(dt.Rows.Count>0)
                {
                    result = true;
                }
            }

            return result;

        }
    }
}

