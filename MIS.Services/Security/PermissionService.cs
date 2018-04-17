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

namespace MIS.Services.Security
{
    public class PermissionService
    {
       
        public bool Permission_Manage(Permission objPermission, string mode)
        {

            QueryResult qResult = null;
            ComWebPermissionInfo objPermissionInfo = new ComWebPermissionInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_COM_WEB_SECURITY";
                objPermissionInfo.PermCd = objPermission.PermCd;
                objPermissionInfo.PermName = Utils.ConvertToString(objPermission.PermName);
                objPermissionInfo.PermDesc = Utils.ConvertToString(objPermission.PermDesc);
                objPermissionInfo.EnteredBy = strUserName.Trim();
                objPermissionInfo.EnteredDt = objPermission.EnteredDt;
                objPermissionInfo.LastUpdatedBy = objPermission.LastUpdatedBy.Trim();
                objPermissionInfo.LastUpdatedDt = objPermission.LastUpdatedDt;
                objPermissionInfo.IPAddress = CommonVariables.IPAddress;
                objPermissionInfo.Mode = mode;
                service.Begin();
                try
                {
                    qResult = service.SubmitChanges(objPermissionInfo, true);
                    if (mode == "D")
                    {
                        if (qResult.IsSuccess)
                        {
                            //Delete the permission from the menu
                            string cmdTxt = String.Format("DELETE FROM COM_MENU_SECURITY WHERE PERM_CD=" + objPermission.PermCd.ToString());
                            qResult = service.SubmitChanges(cmdTxt, null);
                        }
                    }
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    qResult = new QueryResult();
                    ExceptionHandler.ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    qResult = new QueryResult();
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
            return qResult.IsSuccess;

        }

        public DataTable Permission_GetAllToTable()
        {
            DataTable dtbl = null;
            List<ComWebPermissionInfo> lstPermission = new List<ComWebPermissionInfo>();
            using (ServiceFactory service = new ServiceFactory())
            {
                

                string cmdText = "select * from com_web_permission";
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                {
                    dtbl = null;
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
            return dtbl;
        }

        public DataTable Permission_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebPermissionInfo> lstPermission = new List<ComWebPermissionInfo>();
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
               
                cmdText = "select * from com_web_permission";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(PERM_NAME) like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
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
            return dtbl;
        }

        public Permission Permission_Get(int PermissionCode)
        {
            Permission obj = new Permission();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
               
                cmdText = String.Format("select * from com_web_permission where PERM_CD={0}", PermissionCode.ToString());
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(cmdText, null);
                }
                catch (Exception ex)
                { 
                    dtbl = null;
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
            if (dtbl.Rows.Count > 0)
            {
                obj.PermCd = dtbl.Rows[0]["PERM_CD"].ToString();
                obj.PermName = dtbl.Rows[0]["PERM_NAME"].ToString();
                obj.PermDesc = dtbl.Rows[0]["PERM_DESC"].ToString();
                obj.EnteredBy = dtbl.Rows[0]["ENTERED_BY"].ToString();
                obj.EnteredDt = DateTime.Parse(dtbl.Rows[0]["ENTERED_DT"].ToString());
                obj.LastUpdatedBy = dtbl.Rows[0]["LAST_UPDATED_BY"].ToString();
                obj.LastUpdatedDt = DateTime.Parse(dtbl.Rows[0]["LAST_UPDATED_DT"].ToString());
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        public string Permission_GetNewID()
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = String.Format("select max(perm_cd)+1 as newid from com_web_permission");
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
            if (dtbl.Rows.Count > 0)
            {
                newID = dtbl.Rows[0]["newid"].ToString();
                if (newID == "")
                {
                    newID = "1";
                }
            }
            else
            {
                newID = "";
            }
            return newID;
        }

    }
}