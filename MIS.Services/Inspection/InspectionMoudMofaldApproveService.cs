using EntityFramework;
using ExceptionHandler;
using MIS.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;

namespace MIS.Services.Inspection
{
    public class InspectionMoudMofaldApproveService
    {

        public DataTable GetInspectionMOUDList(InspectionDetailModelClass objInspection, string id, string name, string InspLevel, string Approved, string Batch, string recommendation, string finalDecision)
        {
            DataTable dt = null;
            string District = objInspection.district_Cd.ConvertToString();
            string VDC = objInspection.vdc_mun_cd.ConvertToString();
            string Ward = objInspection.ward_no.ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                service.Begin();

                dt = service.GetDataTable(true, "PR_INSPECTION_MOUD_LIST",
                                           District.ToDecimal(),
                                           VDC.ToDecimal(),
                                           Ward.ToDecimal(),
                                           name.ConvertToString(),
                                           id.ConvertToString(),
                                           InspLevel.ToDecimal(),
                                           Approved.ConvertToString(),
                                           Batch.ConvertToString(),
                                           recommendation.ConvertToString(),
                                           finalDecision.ConvertToString(),
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


        #region Inspection MOUD approve all
        public string MOUDApproveAll(InspectionDesignModelClass ObjDesign, string batch)
        {
            bool res = false;
            DataTable dt = new DataTable();
            DataTable dTt = new DataTable();
              QueryResult qrMOUDApprove = null;


            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                if (batch == "" || batch == null)
                {
                    string cmd = "SELECT NVL(MAX(APPROVE_BATCH_1), 0) + 1  FROM NHRS_INSPECTION_PAPER_DTL";
                    dTt = service.GetDataTable(new
                    {
                        query = cmd,
                        args = new { }
                    });
                }

                if (dTt != null && dTt.Rows.Count > 0)
                {
                    batch = dTt.Rows[0][0].ConvertToString(); 
                }
          
                try
                {

                    service.PackageName = "NHRS.PKG_INSPECTION_APPROVAL";
                    qrMOUDApprove = service.SubmitChanges("PR_INSPECTION_MOUD_APPROVE",
                                         "U",
                                         ObjDesign.NraDefCode.ConvertToString(),
                                         ObjDesign.InspectionLevel.ConvertToString(),
                                         "Y",
                                         batch.ConvertToString(),
                                         ObjDesign.InspectionPaperID.ConvertToString(),
                                         SessionCheck.getSessionUsername(),
                                           DateTime.Now


                                     );

                    //qrMOUDApprove = service.SubmitChanges("PR_INSP_MOUD_APPROVE_BYQU",
                    //                     "U",
                    //                       "Y",
                    //                     batch.ConvertToString(), 
                    //                     SessionCheck.getSessionUsername(),
                    //                       DateTime.Now


                    //                 );
                    

                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    //exc = oe.Code.ConvertToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    res = false;
                    //exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qrMOUDApprove != null)
                {
                    res = qrMOUDApprove.IsSuccess;
                }

            }

            return batch;
        }
        #endregion

        #region get mofald approve list
        public DataTable GetInspectionMOFALDList(InspectionDetailModelClass objInspection, string id, string name, string InspLevel, string Approved, string Batch, string recommendation)
        {
            DataTable dt = null;
            string District = objInspection.district_Cd.ConvertToString();
            string VDC = objInspection.vdc_mun_cd.ConvertToString();
            string Ward = objInspection.ward_no.ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
                service.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                service.Begin();

                dt = service.GetDataTable(true, "PR_INSPECTION_MOFALD_LIST",
                                           District.ToDecimal(),
                                           VDC.ToDecimal(),
                                           Ward.ToDecimal(),
                                           name.ConvertToString(),
                                           id.ConvertToString(),
                                           InspLevel.ToDecimal(),
                                           Approved.ConvertToString(),
                                           Batch.ConvertToString(),
                                           recommendation.ConvertToString(),
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
        #endregion

        #region InspectionMOFALD approve all
        public string InspectionMOFALDApprove(InspectionDesignModelClass ObjDesign, string batch)
        {
            bool res = false;
            DataTable dt = new DataTable();
            DataTable dTt = new DataTable();
             QueryResult qrMOFALDApprove= null;

            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                if (batch == "" || batch == null)
                {
                    string cmd = "SELECT NVL(MAX(APPROVE_BATCH_2), 0) + 1  FROM NHRS_INSPECTION_PAPER_DTL";
                    dTt = service.GetDataTable(new
                    {
                        query = cmd,
                        args = new { }
                    });
                    if (dTt != null && dTt.Rows.Count > 0)
                    {
                        batch = dTt.Rows[0][0].ConvertToString();
                    }
                }

                //string UpdatePaperDtl = ("UPDATE  NHRS_INSPECTION_PAPER_DTL SET FINAL_DECISION_2_APPROVE  ='Y',APPROVE_BATCH_2='" + batch + "'  WHERE INSPECTION_PAPER_ID='" + ObjDesign.InspectionPaperID.ConvertToString() + "'");
                //if (ObjDesign.InspectionLevel == "1")
                //{
                //    UpdateMst = "UPDATE  NHRS_INSPECTION_MST SET INSP_ONE_MOFALD_APPROVE     = 'Y'   WHERE NRA_DEFINED_CD='" + ObjDesign.NraDefCode + "'";
                //}
                //if (ObjDesign.InspectionLevel == "2")
                //{
                //    UpdateMst = "UPDATE  NHRS_INSPECTION_MST SET INSP_TWO_MOFALD_APPROVE     = 'Y'   WHERE NRA_DEFINED_CD='" + ObjDesign.NraDefCode + "'";
                //}
                //if (ObjDesign.InspectionLevel == "3")
                //{
                //    UpdateMst = "UPDATE  NHRS_INSPECTION_MST SET INSP_THREE_MOFALD_APPROVE     = 'Y'   WHERE NRA_DEFINED_CD='" + ObjDesign.NraDefCode + "'";
                //}  

                //string updateHouseowner = "UPDATE  NHRS_HOUSE_OWNER_MST SET INSPECTION_FLAG   = 'Y'   WHERE HOUSE_OWNER_ID='" + ObjDesign.hOwnerId.ConvertToString() + "' ";

                try
                {


                    service.PackageName = "NHRS.PKG_INSPECTION_APPROVAL";
                    qrMOFALDApprove = service.SubmitChanges("PR_INSPECTION_MOFALD_APPROVE",
                                         "U",
                                         ObjDesign.NraDefCode.ConvertToString(),
                                         ObjDesign.InspectionLevel.ConvertToString(),
                                         "Y",
                                         batch.ConvertToString(),
                                         ObjDesign.InspectionPaperID.ConvertToString(),
                                         SessionCheck.getSessionUsername(),
                                        DateTime.Now, 
                                          ObjDesign.hOwnerId.ConvertToString()

                                     );
                    //QueryResult qrapprove = service.SubmitChanges(UpdatePaperDtl);
                    //if (qrapprove != null)
                    //{
                    //    QueryResult payrollApprove = service.SubmitChanges(UpdateMst);
                    //    QueryResult updateHouseMst = service.SubmitChanges(updateHouseowner);
                    //}


                }
                catch (OracleException oe)
                {
                    res = false;
                    service.RollBack();
                    //exc = oe.Code.ConvertToString();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    res = false;
                    //exc = ex.ToString();
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                if (qrMOFALDApprove != null)
                {
                    res = qrMOFALDApprove.IsSuccess;
                }


            }
            return batch;
        }
        #endregion

        #region moud approve vdc wise data
        public DataTable VdcWiseDataMoudApprove(string distCd, string lang, string InspectionLevel)
        {
            DataTable dt = new DataTable(); 
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {

                    dt = sf.GetDataTable(true, "pr_moud_approve",
                                                lang,
                                               distCd.ToDecimal(),
                                               InspectionLevel.ToDecimal(),
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
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
            }

            return dt;
        }
        #endregion

        #region mofald approve vdc wise data
        public DataTable VdcWiseDataMofaldApprove(string distCd, string lang, string InspectionLevel)
        {
            DataTable dt = new DataTable();
            using (ServiceFactory sf = new ServiceFactory())
            {
                sf.Begin();
                try
                {
                  
                    dt = sf.GetDataTable(true, "pr_mofald_approve",
                                                lang,
                                               distCd.ToDecimal(),
                                               InspectionLevel.ToDecimal(),
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
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
            }

            return dt;
        }
        #endregion
    }
}





                