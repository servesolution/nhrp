using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Security;
using System.Data;
using System.Data.OracleClient;
using MIS.Services.Core;
using ExceptionHandler;
namespace MIS.Services.AuditTrail
{
   public class AuditTrailService
    {
       public DataTable AuditTrail_Search(string initialStr, string orderby, string order, string UserName, string TableType,string TableName,string Operation, string StartDate, string EndDate)
       {
           DataTable dtbl = null;
           string cmdText = "";
           string strType = "";
           using (ServiceFactory service = new ServiceFactory())
           {
               

               if (TableType != "")
               {
                   if (TableType == "AUDIT_TRAIL_COM")
                   { strType = "Common"; }
                   else { strType = "Transaction"; }
                   cmdText = "SELECT AUD_USER,AUD_OPERATION,USER_TABLE_DESC,AUD_DATETIME, COLUMN_NAME,NEW_VALUE,OLD_VALUE,'" + strType + "' as TABLETYPE FROM ";
                   cmdText += String.Format(TableType);
                   cmdText += GetParameter(initialStr,  UserName, TableName, Operation, StartDate, EndDate);
               }
               else 
               {
                   strType = "Common";
                   cmdText = "( SELECT AUD_USER,AUD_OPERATION,USER_TABLE_DESC,AUD_DATETIME, COLUMN_NAME,NEW_VALUE,OLD_VALUE,'" + strType + "' as TABLETYPE FROM AUDIT_TRAIL_COM";                   
                   cmdText += GetParameter(initialStr,  UserName, TableName, Operation, StartDate, EndDate);
                   cmdText += String.Format(" UNION ");
                   strType = "Transaction";
                   cmdText += "SELECT AUD_USER,AUD_OPERATION,USER_TABLE_DESC,AUD_DATETIME, COLUMN_NAME,NEW_VALUE,OLD_VALUE,'" + strType + "' as TABLETYPE FROM AUDIT_TRAIL_TRANS";
                   cmdText += GetParameter(initialStr, UserName, TableName, Operation, StartDate, EndDate) +" ) ";
               }
               if ((orderby != "") && (order != ""))
               {                   
                  cmdText += String.Format(" order by {0} {1}", orderby, order);                   
               }
               try
               {
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
                   dtbl = null;
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
           return dtbl;
       }

       public string GetParameter(string initialStr, string UserName, string TableName, string Operation, string StartDate, string EndDate)
       {
           string cmdText = " WHERE 1=1 ";
           if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
           {
               cmdText += String.Format(" and Upper(AUD_USER) like '{0}%'", initialStr);
           }
           if (UserName != "")
           {
               cmdText += String.Format(" and Upper(AUD_USER) like '%{0}%'", UserName.ToUpper());
           }
           if (TableName != "")
           {
               cmdText += String.Format(" and USER_TABLE_DESC='{0}'", TableName);
           }
           if (Operation != "")
           {
               cmdText += String.Format(" and AUD_OPERATION='{0}'", Operation);
           }
           if (StartDate != "")
           {
               cmdText += String.Format(" and to_date(AUD_DATETIME) >=TO_DATE(to_date('{0}','RRRR-MM-DD'),'DD/MM/RRRR')", StartDate);
           }
           if (EndDate != "")
           {
               cmdText += String.Format(" and to_date(AUD_DATETIME) <=TO_DATE(to_date('{0}','RRRR-MM-DD'),'DD/MM/RRRR')", EndDate);
           }
           
           return cmdText;
       }

       public DataTable GetAuditTrailTable(string TableType)
       {
           DataTable dtbl = null;
           string cmdText = "";
           using (ServiceFactory service = new ServiceFactory())
           {
               

               if (TableType != "")
               {
                   cmdText = "SELECT DISTINCT(USER_TABLE_DESC) FROM ";
                   cmdText += String.Format(TableType);
                   
               }
               else
               {
                   cmdText = "( SELECT DISTINCT(USER_TABLE_DESC) FROM AUDIT_TRAIL_COM";                   
                   cmdText += String.Format(" UNION ");
                   cmdText += "SELECT DISTINCT(USER_TABLE_DESC) FROM AUDIT_TRAIL_TRANS )";
                   
               }
               cmdText += " order by USER_TABLE_DESC";
               try
               {
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
                   dtbl = null;
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
           return dtbl;
       }
    }
}
