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
   public class ModuleService
    {      
        public bool Module_Manage(Module objModule, string mode)
        {

            QueryResult qResult = null;
            ComModuleInfo objModuleInfo = new ComModuleInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_COM_WEB_SECURITY";
                objModuleInfo.ModuleCd = objModule.ModuleCd;
                objModuleInfo.ModuleName = Utils.ConvertToString( objModule.ModuleName);
                objModuleInfo.ModuleDesc = Utils.ConvertToString(objModule.ModuleDesc);
                objModuleInfo.EnteredBy = strUserName.Trim();//Utils.ConvertToString(objModule.EnteredBy);
                objModuleInfo.EnteredDt = objModule.EnteredDt;
                objModuleInfo.LastUpdatedBy = objModule.LastUpdatedBy.Trim();
                objModuleInfo.LastUpdatedDt = objModule.LastUpdatedDt;
                objModuleInfo.IPAddress = CommonVariables.IPAddress;
                objModuleInfo.Mode = mode;                
                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objModuleInfo, true);                   
                }
                catch (OracleException oe)
                {
                    service.RollBack();
                    qResult = new QueryResult();
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    service.RollBack();
                    qResult = new QueryResult();
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
            return qResult.IsSuccess;

        }

        public DataTable Module_GetAllToTable()
        {
            DataTable dtbl = null;            
            using (ServiceFactory service = new ServiceFactory())
            {
               

                string cmdText = "select * from COM_MODULE";
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(cmdText, null);
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

        public DataTable Module_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;         
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = "select * from COM_MODULE";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(MODULE_NAME) like '{0}%'", initialStr);
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

        public Module Module_Get(int ModuleCode)
        {
            Module obj = new Module();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = String.Format("select * from com_Module where MODULE_CD={0}", ModuleCode.ToString());
                try
                {
                    service.Begin();
                    dtbl = service.GetDataTable(cmdText, null);
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
            if (dtbl.Rows.Count > 0)
            {
                obj.ModuleCd = dtbl.Rows[0]["MODULE_CD"].ToString();
                obj.ModuleName = dtbl.Rows[0]["MODULE_NAME"].ToString();
                obj.ModuleDesc = dtbl.Rows[0]["MODULE_DESC"].ToString();
                obj.Status = dtbl.Rows[0]["STATUS"].ToString();
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

        public string Module_GetNewID()
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = String.Format("select max(MODULE_CD)+1 as newid from COM_MODULE");
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
                newID = "1";
            }
            return newID;
        }
    }
}
