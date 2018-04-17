using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFramework;
using MIS.Models.Security;
using System.Data;
using System.Data.OracleClient;
using MIS.Services .Core;
using ExceptionHandler;
using MIS.Models.AuditTrail;
namespace MIS.Services.AuditTrail
{
  
    public class LoginService
    {
        public bool Login_Manage(Login objLogin, string mode)
        {

            QueryResult qResult = null;
            ComUsrLoginLogInfo objLoginInfo = new ComUsrLoginLogInfo();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_SETUP";
                objLoginInfo.UsrCd = Utils.ConvertToString(objLogin.UsrCd);
                objLoginInfo.LoginSno = objLogin.LoginSno;
                objLoginInfo.EmployeeCd = Utils.ConvertToString(objLogin.EmployeeCd);
                objLoginInfo.OfficeCd = Utils.ConvertToString(objLogin.OfficeCd);
                objLoginInfo.LoginDt = objLogin.LoginDt.ToString("dd-MM-yyyy");
                objLoginInfo.LoginDtLoc = objLogin.LoginDtLoc;
                objLoginInfo.LoginHh = objLogin.LoginHh;
                objLoginInfo.LoginMi = objLogin.LoginMi;
                objLoginInfo.LoginSs = objLogin.LoginSs;
                if (objLogin.LogoutDt != null)
                {
                    objLoginInfo.LogoutDt =DateTime.Parse(objLogin.LogoutDt.ToString()).ToString("dd-MM-yyyy");
                }
                else
                {
                    objLoginInfo.LogoutDt = null;
                }
                objLoginInfo.LogoutDtLoc = objLogin.LogoutDtLoc;
                objLoginInfo.LogoutHh = objLogin.LogoutHh;
                objLoginInfo.LogoutMi = objLogin.LogoutMi;
                objLoginInfo.LogoutSs = objLogin.LogoutSs;
                objLoginInfo.LoginRemarks = objLogin.LoginRemarks;
                objLoginInfo.LogoutRemarks = objLogin.LogoutRemarks;
                objLoginInfo.ModuleCd = objLogin.ModuleCd;
                objLoginInfo.MenuCd = objLogin.MenuCd;
                objLoginInfo.MenuUrl = objLogin.MenuUrl;
                objLoginInfo.IpAddress = CommonVariables.IPAddress;              
                objLoginInfo.Mode = mode;
                
                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objLoginInfo, true);                   
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

        public DataTable Login_GetAllToTable()
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                try
                {
                    service.Begin();
                    cmdText = "select * from COM_USR_LOGIN_LOG";
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

        public DataTable Login_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;           
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {                         

                cmdText = "SELECT T1.*,T2.MODULE_NAME,T4.GRP_CD FROM COM_USR_LOGIN_LOG T1"
                            +" LEFT OUTER JOIN COM_MODULE T2 on T1.MODULE_CD=T2.MODULE_CD"
                            +" LEFT OUTER JOIN COM_WEB_USR_GRP T3 on T1.USR_CD=T3.USR_CD"
                            +" LEFT OUTER JOIN COM_WEB_GRP T4 on T3.GRP_CD=T4.GRP_CD";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(USR_CD) like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby == "login_sno")
                    {
                        cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
                    }
                    else if (orderby == "login_dt" || orderby == "login_dt_loc" || orderby == "logout_dt" || orderby == "logout_dt_loc")
                    {
                        if (orderby == "login_dt_loc")
                        {
                            orderby = "login_dt";
                        }
                        if (orderby == "logout_dt_loc")
                        {
                            orderby = "logout_dt";
                        }
                        cmdText += String.Format(" order by to_date({0}) {1}", orderby, order);
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }
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
        public DataTable Login_Search(string initialStr, string orderby, string order, string Username, string GroupCode, string StartDate,string EndDate)
        {
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                

                cmdText = "SELECT T1.*,T5.USR_NAME,T2.MODULE_NAME,T4.GRP_CD,T4.GRP_NAME FROM COM_USR_LOGIN_LOG T1"
                            + " LEFT OUTER JOIN COM_WEB_USR T5 on T5.USR_CD=T1.USR_CD"
                            + " LEFT OUTER JOIN COM_MODULE T2 on T1.MODULE_CD=T2.MODULE_CD"
                            + " LEFT OUTER JOIN COM_WEB_USR_GRP T3 on T1.USR_CD=T3.USR_CD"
                            + " LEFT OUTER JOIN COM_WEB_GRP T4 on T3.GRP_CD=T4.GRP_CD where Upper(T1.USR_CD)!='ADMIN' ";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" and Upper(T5.USR_NAME) like '{0}%'", initialStr);
                }
                if (Username != "")
                {
                    cmdText += String.Format(" and Upper(T5.USR_NAME) like '%{0}%'", Username.ToUpper());
                }
                if (GroupCode != "")
                {
                    cmdText += String.Format(" and GRP_CD={0}", GroupCode);
                }                
                if (StartDate != "")
                {
                    cmdText += String.Format(" and to_date(LOGIN_DT) >=TO_DATE(to_date('{0}','RRRR-MM-DD'),'DD/MM/RRRR')", StartDate);
                }
                if (EndDate != "")
                {
                    cmdText += String.Format(" and to_date(LOGIN_DT) <=TO_DATE(to_date('{0}','RRRR-MM-DD'),'DD/MM/RRRR')", EndDate);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby == "login_sno")
                    {
                        cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
                    }
                    else if (orderby == "login_dt" || orderby == "login_dt_loc" || orderby == "logout_dt" || orderby == "logout_dt_loc")
                    {
                        if (orderby == "login_dt_loc" || orderby == "login_dt")
                        {
                            
                            if (order == "desc")
                            {
                                cmdText += String.Format(" order by to_date(login_dt) desc , login_hh  desc, login_mi  desc, login_ss desc ");
                            }
                            else
                            {
                                cmdText += String.Format(" order by to_date(login_dt) asc , login_hh  asc, login_mi  asc, login_ss asc ");
                            }  
                        }
                        if (orderby == "logout_dt_loc" || orderby == "logout_dt")
                        {
                           
                            if (order == "desc")
                            {
                                cmdText += String.Format(" order by to_date(logout_dt) desc , logout_hh  desc, logout_mi  desc, logout_ss desc ");
                            }
                            else
                            {
                                cmdText += String.Format(" order by to_date(logout_dt) asc , logout_hh  asc, logout_mi  asc, logout_ss asc ");
                            }  
                        }
                                             
                    }
                    else
                    {
                        cmdText += String.Format(" order by Upper({0}) {1}", orderby, order);
                    }
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


        public Login Login_Get(string UserCode, string loginSerialNo)
        {
            Login obj = new Login();
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = String.Format("SELECT T1.*,T6.USR_NAME USR_NAME,T2.MODULE_NAME MODULE_NAME," + Utils.ToggleLanguage("T3.DESC_ENG", "T3.DESC_LOC") + " OFFICE_NAME,T5.GRP_NAME GROUP_NAME FROM COM_USR_LOGIN_LOG  T1" +
                                        " LEFT OUTER JOIN  COM_WEB_USR T6 ON T6.USR_CD =T1.USR_CD" +
                                        " LEFT OUTER JOIN  COM_MODULE T2 ON T1.MODULE_CD =T2.MODULE_CD"+
                                        " LEFT OUTER JOIN  MIS_OFFICE T3 ON T1.OFFICE_CD = T3.OFFICE_CD"+
                                        " LEFT OUTER JOIN COM_WEB_USR_GRP T4 on T1.USR_CD=T4.USR_CD"+
                                        " LEFT OUTER JOIN COM_WEB_GRP T5 on T4.GRP_CD=T5.GRP_CD"+
                                        " WHERE T6.USR_CD='{0}' AND LOGIN_SNO={1}", UserCode.ToString(), loginSerialNo.ToString());
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
                obj.UsrCd = dtbl.Rows[0]["USR_CD"].ToString();
                obj.UsrName = dtbl.Rows[0]["USR_NAME"].ToString();
                obj.LoginSno = Decimal.Parse(dtbl.Rows[0]["LOGIN_SNO"].ToString());
                obj.EmployeeCd = dtbl.Rows[0]["EMPLOYEE_CD"].ToString();
                obj.OfficeCd = dtbl.Rows[0]["OFFICE_CD"].ToString();
                obj.OfficeName = dtbl.Rows[0]["OFFICE_NAME"].ToString();
                obj.GroupName = dtbl.Rows[0]["GROUP_NAME"].ToString();
                obj.LoginDt = DateTime.Parse( dtbl.Rows[0]["LOGIN_DT"].ToString());
                obj.LoginDtLoc = dtbl.Rows[0]["LOGIN_DT_LOC"].ToString();
                obj.LoginHh = Decimal.Parse( dtbl.Rows[0]["LOGIN_HH"].ToString());
                obj.LoginMi = Decimal.Parse(dtbl.Rows[0]["LOGIN_MI"].ToString());
                obj.LoginSs = Decimal.Parse(dtbl.Rows[0]["LOGIN_SS"].ToString());
                if (dtbl.Rows[0]["LOGOUT_DT"] != null)
                {
                    if (dtbl.Rows[0]["LOGOUT_DT"].ToString().Trim() != "")
                    {
                        obj.LogoutDt = DateTime.Parse(dtbl.Rows[0]["LOGOUT_DT"].ToString());
                    }
                }
                obj.LogoutDtLoc = dtbl.Rows[0]["LOGOUT_DT_LOC"].ToString();
                if (dtbl.Rows[0]["LOGOUT_HH"].ToString() != "")
                {
                    obj.LogoutHh = Decimal.Parse(dtbl.Rows[0]["LOGOUT_HH"].ToString());
                }
                if (dtbl.Rows[0]["LOGOUT_MI"].ToString() != "")
                {
                    obj.LogoutMi = Decimal.Parse(dtbl.Rows[0]["LOGOUT_MI"].ToString());
                }
                if (dtbl.Rows[0]["LOGOUT_SS"].ToString() != "")
                {
                    obj.LogoutSs = Decimal.Parse(dtbl.Rows[0]["LOGOUT_SS"].ToString());
                }
                obj.LoginRemarks = dtbl.Rows[0]["LOGIN_REMARKS"].ToString();
                obj.LogoutRemarks = dtbl.Rows[0]["LOGOUT_REMARKS"].ToString();
                obj.ModuleCd = dtbl.Rows[0]["MODULE_CD"].ToString();
                obj.ModuleName = dtbl.Rows[0]["MODULE_NAME"].ToString();
                obj.MenuCd = dtbl.Rows[0]["MENU_CD"].ToString();
                obj.MenuUrl = dtbl.Rows[0]["MENU_URL"].ToString();
                obj.IpAddress = dtbl.Rows[0]["IP_ADDRESS"].ToString();
            }
            else
            {
                obj = null;
            }
            return obj;
        }

        public string Login_GetNewLoginID(string UserCode)
        {
            string newID = "";
            DataTable dtbl = null;
            string cmdText = "";
            using (ServiceFactory service = new ServiceFactory())
            {
                
                cmdText = String.Format("select max(LOGIN_SNO)+1 as newid from COM_USR_LOGIN_LOG where USR_CD='{0}'", UserCode);
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
