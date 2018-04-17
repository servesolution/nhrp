
using EntityFramework;
using ExceptionHandler;
using MIS.Models.Resettlement;
using MIS.Services.Core;
using System.Data.OracleClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MIS.Services.Resettlement
{
    public class ResettlementTargetingService
    {
        public DataTable getResettlementTargetingNewApplicant(ResettlementTargetingModel objTargetingSearch)
        {
            DataTable dt = new DataTable();
            string enteredBy = null;
            using(ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.PackageName = "PKG_NHRS_RESETTLEMENT_TRGTNG";
                    sf.Begin();
                    dt = sf.GetDataTable(true, "PR_RESETTLEMENT_TARGT_SEARCH",
                        DBNull.Value, //SessionCheck out
                        objTargetingSearch.District.ToDecimal(),
                        objTargetingSearch.VdcMunicipality.ToDecimal(),
                        objTargetingSearch.Ward.ToDecimal(),
                        objTargetingSearch.Language.ConvertToString(),
                        enteredBy,
                        objTargetingSearch.MisReview.ToString(),
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

        #region save resettlement eligible data
        public bool saveEligibleResettlement(string sessionId, string ResettlementId)
        {
            QueryResult qr = null;
            QueryResult qrFileBatch = null;
            string FileBatchId = "";
            bool result = false;
           using(ServiceFactory sf = new ServiceFactory())
           {
               try
               {
                   sf.Begin();
                   sf.PackageName = "NHRS.PKG_RESETTLEMENT_ELIGBL_PRSS";

                   qrFileBatch = sf.SubmitChanges("PR_NHRS_IMPRT_EL_BATCH",     
                                                         
                                                           
                                                          ResettlementId, 
                                                          SessionCheck.getSessionUsername(),//entered by
                                                           
                                                          DBNull.Value,
                                                          sessionId.ToDecimal()
                     );
                   FileBatchId = qrFileBatch["v_FILEBATCH_ID"].ConvertToString();
                   
                       
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
           if (qrFileBatch != null && qrFileBatch.IsSuccess)
           {
               result = qrFileBatch.IsSuccess;
           }



             return result;
        }
        #endregion 

        #region get resettlement targeted data
        public DataTable getResettlementTargetedData(string districtCd, string vdcMunCd, string ward, string fileBatch, string approval, string approvedDate, string misReview)
        {
            DataTable dt = new DataTable();
            using(ServiceFactory sf = new ServiceFactory())
            {
                try
                {
                    sf.PackageName = "NHRS.PKG_RESETTLEMENT_ELIGBL_PRSS";
                    sf.Begin();
                    dt = sf.GetDataTable(true, "PR_RESET_TARGETED_SEARCH",
                        districtCd.ToDecimal(),
                        vdcMunCd.ToDecimal(),
                        ward.ToDecimal(),
                        fileBatch.ToDecimal(),
                        approval.ConvertToString(),
                        DBNull.Value,//date 
                        misReview,
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
                    if (dt.Rows.Count<0)
                    {
                        dt = null;
                    }
                }
            }
            return dt;
        }
        #endregion

        #region approve all resettlement targeted
        public bool TargetedApprovedAll(string targetBatchId)
        {
             bool result = false;
            QueryResult quryResult = null;
            string approved = "Y";
             ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_RESETTLEMENT_ELIGBL_PRSS";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_ELIGIBLE_APPROVE_ALL",
                                                  targetBatchId,
                                                  approved,
                                                  SessionCheck.getSessionUsername(),//updated by
                                                  DateTime.Now,//updated date
                                                  System.DateTime.Now.ConvertToString(),//updated date loc
                                                  DBNull.Value //file batch id
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
            if(quryResult.IsSuccess)
            {
                result = true;
            }

            return true;
        }
        #endregion

        #region approve selected resettlement targeted
        public bool SelectedTargetingApproved(string resettlementEligibleId)
        {
            bool result = false;
            QueryResult quryResult = null;
            string approved = "Y";
            string fileBatchId = null; 
            ServiceFactory service = new ServiceFactory();
            try
            {
                string resPackageName = service.PackageName;
                service.PackageName = "PKG_RESETTLEMENT_ELIGBL_PRSS";
                service.Begin();
                quryResult = service.SubmitChanges("PR_NHRS_ELGBL_APPROVE_SELECTED",
                                                  resettlementEligibleId,
                                                  approved,
                                                  SessionCheck.getSessionUsername(),//updated by
                                                  DateTime.Now,//updated date
                                                  System.DateTime.Now.ConvertToString(),//updated date loc
                                                  fileBatchId
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

            if(quryResult.IsSuccess)
            {
                result = true;
            }
            return result;
        }
        #endregion
    }
}
