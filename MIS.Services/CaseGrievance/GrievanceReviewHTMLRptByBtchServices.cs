using EntityFramework;
using ExceptionHandler;
using MIS.Services.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
 
namespace MIS.Services.CaseGrievence
{
   public class GrievanceReviewHTMLRptByBtchServices
   {
       CommonFunction common = new CommonFunction();
       public DataTable ReviewedDetailReportByBatch(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_RecommendationType_TYPE = DBNull.Value;
           Object P_batch = DBNull.Value;
           Object P_SIZE = "100";
           //Object P_INDEX="1";
           Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
           Object P_WARD_NO_CURRENT = DBNull.Value;
           //Object P_SIZE = DBNull.Value;
           Object P_INDEX = DBNull.Value;
           Object P_exportcheck = DBNull.Value;

           if (paramValues["exportcheck"].ConvertToString() != string.Empty && paramValues["exportcheck"].ConvertToString() != "undefined")
               P_exportcheck = paramValues["exportcheck"].ConvertToString();
           if (P_exportcheck.ConvertToString() == "1")
           {
               P_SIZE = "";
           }

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           
           if (paramValues["RecommendationType"].ConvertToString() != string.Empty && paramValues["RecommendationType"].ConvertToString() != "undefined")
               P_RecommendationType_TYPE = paramValues["RecommendationType"].ConvertToString();
           if (paramValues["batch"].ConvertToString() != string.Empty && paramValues["batch"].ConvertToString() != "undefined")
               P_batch = paramValues["batch"].ConvertToString();
           if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
               P_INDEX = paramValues["pageindex"].ConvertToString();


           if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
               P_SIZE = paramValues["pagesize"].ConvertToString();


           if (paramValues["pageindex"].ConvertToString() == string.Empty)
           {
                P_INDEX = "1";
           }
           if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
               P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
           if (paramValues["wardCurr"].ConvertToString() != string.Empty)
               P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();
           //if (paramValues["pagesize"].ConvertToString() == string.Empty)
           //{
           //    P_SIZE = "100";
           //}
            
           
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GR_HANDLING_REPORT_BY_BATCH",
                   paramValues["lang"].ConvertToString(),
                   P_Dist, 
                   P_VDC_MUN_CD_CURRENT,
                   P_WARD_NO_CURRENT,
                   P_RecommendationType_TYPE,
                   P_batch.ToDecimal(),
                   P_SIZE,
                   P_INDEX,
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
       public DataTable GetGrievanceReviewedBaseData(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_RecommendationType_TYPE = DBNull.Value;
           Object P_batch = DBNull.Value;
           Object P_SIZE = "100";
           //Object P_INDEX="1";
           Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
           Object P_WARD_NO_CURRENT = DBNull.Value;
           //Object P_SIZE = DBNull.Value;
           Object P_INDEX = DBNull.Value;

           Object P_exportcheck = DBNull.Value;

           if (paramValues["exportcheck"].ConvertToString() != string.Empty && paramValues["exportcheck"].ConvertToString() != "undefined")
               P_exportcheck = paramValues["exportcheck"].ConvertToString();
           if (P_exportcheck.ConvertToString() == "1")
           {
               P_SIZE = "";
           }

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["RecommendationType"].ConvertToString() != string.Empty && paramValues["RecommendationType"].ConvertToString() != "undefined")
               P_RecommendationType_TYPE = paramValues["RecommendationType"].ConvertToString();
           if (paramValues["batch"].ConvertToString() != string.Empty && paramValues["batch"].ConvertToString() != "undefined")
               P_batch = paramValues["batch"].ConvertToString();
           if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
               P_INDEX = paramValues["pageindex"].ConvertToString();


           if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
               P_SIZE = paramValues["pagesize"].ConvertToString();


           if (paramValues["pageindex"].ConvertToString() == string.Empty)
           {
               P_INDEX = "1";
           }


 

           //if (paramValues["pagesize"].ConvertToString() == string.Empty)
           //{
           //    P_SIZE = "100";
           //}


           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GR_REVIEWED_BASE_DATA",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD, 
                   P_RecommendationType_TYPE,
                   P_batch.ToDecimal(),
                   P_SIZE,
                   P_INDEX,
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
       public DataTable GetGrievanceReviewedBaseDataForResurvey(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_RecommendationType_TYPE = DBNull.Value;
           Object P_batch = DBNull.Value;
           Object P_SIZE = "100";
           //Object P_INDEX="1";
           Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
           Object P_WARD_NO_CURRENT = DBNull.Value;
           //Object P_SIZE = DBNull.Value;
           Object P_INDEX = DBNull.Value;
           Object P_exportcheck = DBNull.Value;

           if (paramValues["exportcheck"].ConvertToString() != string.Empty && paramValues["exportcheck"].ConvertToString() != "undefined")
               P_exportcheck = paramValues["exportcheck"].ConvertToString();
           if(P_exportcheck.ConvertToString()=="1")
           {
               P_SIZE = "";
           }


           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["RecommendationType"].ConvertToString() != string.Empty && paramValues["RecommendationType"].ConvertToString() != "undefined")
               P_RecommendationType_TYPE = paramValues["RecommendationType"].ConvertToString();
           if (paramValues["batch"].ConvertToString() != string.Empty && paramValues["batch"].ConvertToString() != "undefined")
               P_batch = paramValues["batch"].ConvertToString();
           if (paramValues["pageindex"].ConvertToString() != string.Empty && paramValues["pageindex"].ConvertToString() != "undefined")
               P_INDEX = paramValues["pageindex"].ConvertToString();


           if (paramValues["pagesize"].ConvertToString() != string.Empty && paramValues["pagesize"].ConvertToString() != "undefined")
               P_SIZE = paramValues["pagesize"].ConvertToString();


           if (paramValues["pageindex"].ConvertToString() == string.Empty)
           {
               P_INDEX = "1";
           }


          

           //if (paramValues["pagesize"].ConvertToString() == string.Empty)
           //{
           //    P_SIZE = "100";
           //}


           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GR_REVIEWED_BASE_RPT_DATA",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD, 
                   P_RecommendationType_TYPE,
                   P_batch.ToDecimal(),
                   P_SIZE,
                   P_INDEX,
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


       public DataTable ReviewedDetailReportByBatchWithoutpagination(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_RecommendationType_TYPE = DBNull.Value;
           Object P_batch = DBNull.Value;
        

          

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           if (paramValues["vdc"].ConvertToString() != string.Empty && paramValues["vdc"].ConvertToString() != "undefined")
               P_VDC = paramValues["vdc"].ConvertToString();
           if (paramValues["ward"].ConvertToString() != string.Empty && paramValues["ward"].ConvertToString() != "undefined")
               P_WARD = paramValues["ward"].ConvertToString();
           if (paramValues["RecommendationType"].ConvertToString() != string.Empty && paramValues["RecommendationType"].ConvertToString() != "undefined")
               P_RecommendationType_TYPE = paramValues["RecommendationType"].ConvertToString();
           if (paramValues["batch"].ConvertToString() != string.Empty && paramValues["batch"].ConvertToString() != "undefined")
               P_batch = paramValues["batch"].ConvertToString();
         


         


           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GR_HANDLING_REPORT_BY_BATCH",
                   paramValues["lang"].ConvertToString(),
                   P_Dist,
                   P_VDC,
                   P_WARD,
                   P_RecommendationType_TYPE,
                   P_batch.ToDecimal(),      
                   DBNull.Value,
                   DBNull.Value,
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


       public DataTable ReviewedSummaryReportByBatch(NameValueCollection paramValues)
       {
           DataTable dt = null;
           Object P_Dist = DBNull.Value;
           Object P_VDC = DBNull.Value;
           Object P_WARD = DBNull.Value;
           Object P_RecommendationType_TYPE = DBNull.Value;
           Object P_batch = DBNull.Value;
           Object P_GENDER = DBNull.Value;

           Object P_VDC_MUN_CD_CURRENT = DBNull.Value;
           Object P_WARD_NO_CURRENT = DBNull.Value;

           if (paramValues["dist"].ConvertToString() != string.Empty && paramValues["dist"].ConvertToString() != "undefined")
               P_Dist = paramValues["dist"].ConvertToString();
           
           if (paramValues["RecommendationType"].ConvertToString() != string.Empty && paramValues["RecommendationType"].ConvertToString() != "undefined")
               P_RecommendationType_TYPE = paramValues["RecommendationType"].ConvertToString();
           if (paramValues["batch"].ConvertToString() != string.Empty && paramValues["batch"].ConvertToString() != "undefined")
               P_batch = paramValues["batch"].ConvertToString();
           if (paramValues["vdcCurr"].ConvertToString() != string.Empty)
               P_VDC_MUN_CD_CURRENT = paramValues["vdcCurr"].ConvertToString();
           if (paramValues["wardCurr"].ConvertToString() != string.Empty)
               P_WARD_NO_CURRENT = paramValues["wardCurr"].ConvertToString();
           using (ServiceFactory service = new ServiceFactory())
           {
               try
               {
                   service.Begin();
                   dt = service.GetDataTable(true, "GR_REVIEWED_SUMMARY_RPT_BATCH",
                   paramValues["lang"].ConvertToString(),
                   P_Dist, 
                   P_VDC_MUN_CD_CURRENT,
                   P_WARD_NO_CURRENT,
                   P_RecommendationType_TYPE.ToDecimal(),
                   P_batch.ToDecimal(),
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
