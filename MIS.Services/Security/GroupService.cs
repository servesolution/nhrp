using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using MIS.Models.Security;
using System.Web.Mvc;
using System.Data.OracleClient;
using ExceptionHandler;
using MIS.Services.Core;
namespace MIS.Services.Security
{
    public class GroupService
    {
        #region  "Select operation on Group"

        public List<Group> GetUserGroup()
        {
            DataTable dtbl = null;
            List<Group> lstUserGroup = new List<Group>();
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select GRP_CD, GRP_NAME, STATUS FROM COM_WEB_GRP";
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
            if (dtbl != null)
            {
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstUserGroup.Add(new Group { GrpCode = drow["GRP_CD"].ToString(), GrpName = drow["GRP_NAME"].ToString(), status = Convert.ToChar(drow["STATUS"]) });
                }
            }
            return lstUserGroup;
        }

        public DataTable GetUserGrp()
        {
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select GRP_CD,GRP_NAME,STATUS from COM_WEB_GRP order by(TO_NUMBER(GRP_CD)) ASC";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    dt = null;
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

        public string GetMaxvalue()
        {
            string result = "";
            DataTable dt = null;
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {

                    string cmdText = "select nvl(max(to_number(grp_cd)),0)+1 from com_web_grp";
                    try
                    {
                        service.Begin();
                        dt = service.GetDataTable(new
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
                        dt = null;
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                    if (dt != null)
                    {
                        result = dt.Rows[0][0].ToString();
                    }

                }
            }
            catch (Exception)
            {
                result = "";
            }
            return result;
        }

        public DataTable Group_GetAllCriteriaToTable(string initialStr, string orderby, string order)
        {
            DataTable dtbl = null;
            List<ComWebGrpInfo> WbGroup = new List<ComWebGrpInfo>();
            string cmdText = string.Empty;
            using (ServiceFactory service = new ServiceFactory())
            {

                cmdText = "select * from com_web_grp";
                if ((initialStr != "") && (initialStr != "ALL") && (initialStr != null))
                {
                    cmdText += String.Format(" where Upper(GRP_NAME) like '{0}%'", initialStr);
                }
                if ((orderby != "") && (order != ""))
                {
                    if (orderby == "grp_cd")
                    {
                        cmdText += String.Format(" order by nvl(to_number({0}),0) {1}", orderby, order);
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
                    ExceptionManager.AppendLog(ex);
                    dtbl = null;
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

        public Group FillGroup(string groupCode)
        {
            Group obj = new Group();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string CmdText = "select GRP_CD,GRP_NAME,GRP_DESC,STATUS from COM_WEB_GRP where GRP_CD = '" + groupCode + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    dt = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                try
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            obj.GrpCode = dt.Rows[0]["GRP_CD"].ToString();
                            obj.GrpName = dt.Rows[0]["GRP_NAME"].ToString();
                            obj.GrpDesc = dt.Rows[0]["GRP_DESC"].ToString();
                            obj.status = Convert.ToChar(dt.Rows[0]["STATUS"]);
                        }
                    }
                }
                catch (Exception)
                {
                    obj = null;
                }
            }
            return obj;
        }

        public Group GetGroupbyName(string groupName)
        {
            Group obj = new Group();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string CmdText = "select GRP_CD,GRP_NAME,GRP_DESC,STATUS from COM_WEB_GRP where GRP_NAME = '" + groupName.ToUpper() + "'";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = CmdText,
                        args = new
                        {
                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    dt = null;
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
                try
                {
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            obj.GrpCode = dt.Rows[0]["GRP_CD"].ToString();
                            obj.GrpName = dt.Rows[0]["GRP_NAME"].ToString();
                            obj.GrpDesc = dt.Rows[0]["GRP_DESC"].ToString();
                            obj.status = Convert.ToChar(dt.Rows[0]["STATUS"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    obj = null;
                }
            }
            return obj;
        }

        public Group GetGroupbyCode(string groupCode)
        {
            Group obj = new Group();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "select * from COM_WEB_GRP where GRP_CD = '" + groupCode + "'";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
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
                    if (dt.Rows.Count > 0)
                    {
                        obj.GrpCode = dt.Rows[0]["GRP_CD"].ToString();
                        obj.GrpName = dt.Rows[0]["GRP_NAME"].ToString();
                        obj.GrpDesc = dt.Rows[0]["GRP_DESC"].ToString();
                        obj.status = Convert.ToChar(dt.Rows[0]["STATUS"]);
                        obj.LastUpdDt = Convert.ToDateTime(dt.Rows[0]["LAST_UPDATED_DT"]);
                        obj.LastUpdBy = dt.Rows[0]["LAST_UPDATED_BY"].ToString();
                        obj.EnterBy = dt.Rows[0]["ENTERED_BY"].ToString();
                        obj.EnteredDt = Convert.ToDateTime(dt.Rows[0]["ENTERED_DT"]);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    obj = null;
                }
            }
            return obj;
        }

        public Group GetGroupbyUserCode(string userCode)
        {
            Group obj = new Group();
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                try
                {
                    service.Begin();
                    string CmdText = "SELECT T1.* FROM COM_WEB_GRP T1,COM_WEB_USR_GRP T2 WHERE T1.GRP_CD=T2.grp_cd AND T2.usr_cd='" + userCode.ToUpper().Trim() + "'";
                    try
                    {
                        dt = service.GetDataTable(new
                        {
                            query = CmdText,
                            args = new
                            {
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                        dt = null;
                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        obj.GrpCode = dt.Rows[0]["GRP_CD"].ToString();
                        obj.GrpName = dt.Rows[0]["GRP_NAME"].ToString();
                        obj.GrpDesc = dt.Rows[0]["GRP_DESC"].ToString();
                        obj.status = Convert.ToChar(dt.Rows[0]["STATUS"]);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    obj = null;
                }
            }
            return obj;
        }

        public DataTable GetUserInGroup(Group objGroup)
        {
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string GrpCd = objGroup.GrpCode;
                string cmdText = "SELECT USR_NAME FROM COM_WEB_USR CWU  INNER JOIN COM_WEB_USR_GRP CWUG ON CWU.USR_CD = CWUG.USR_CD  WHERE CWUG.GRP_CD = '" + GrpCd + "' ";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
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
                    dt = null;
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

        public DataTable SearchUser(Group objGroup)
        {
            DataTable dt = null;
            string CmdText = null;
            using (ServiceFactory service = new ServiceFactory())
            {


                string UserCd = objGroup.SearchCd;
                string UserText = objGroup.SearchText;

                if (UserText != "" && UserCd != "")
                {
                    CmdText = "SELECT USR_CD,USR_NAME,EMAIL FROM COM_WEB_USR WHERE UPPER (USR_CD) LIKE '%" + UserCd.ToUpper() + "%' AND UPPER (USR_NAME) LIKE '%" + UserText.ToUpper() + "%' ";

                }
                else if (UserCd != "" && UserCd != null)
                {
                    CmdText = "SELECT USR_CD,USR_NAME,EMAIL FROM COM_WEB_USR WHERE UPPER (USR_CD) LIKE '%" + UserCd.ToUpper() + "%'";

                }

                else if (UserText != "" && UserText != null)
                {

                    CmdText = "SELECT USR_CD,USR_NAME,EMAIL FROM COM_WEB_USR WHERE UPPER (USR_NAME) LIKE '%" + UserText.ToUpper() + "%'";

                }
                else
                {
                    CmdText = "SELECT USR_CD,USR_NAME,EMAIL FROM COM_WEB_USR";
                }

                try
                {
                    service.Begin();
                    dt = service.GetDataTable(CmdText, null);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    dt = null;
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

        public IList<SelectListItem> GetUserListToRemove(string id)
        {
            IList<SelectListItem> UserList = new List<SelectListItem>();
            DataTable dt = new DataTable();
            using (ServiceFactory service = new ServiceFactory())
            {


                string CmdText = "Select CWU.USR_CD,CWU.USR_NAME FROM COM_WEB_USR_GRP CWUG INNER JOIN COM_WEB_USR CWU ON CWUG.USR_CD = CWU.USR_CD where GRP_CD = '" + id + "' ";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(CmdText, null);
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
                try
                {
                    if (dt != null)
                    {
                        foreach (DataRow drow in dt.Rows)
                        {
                            // UserList.Add(new Group { GrpCode = drow["USR_CD"].ToString(), GrpName = drow["USR_NAME"].ToString() });
                            SelectListItem li = new SelectListItem();
                            li.Text = drow["USR_NAME"].ToString();
                            li.Value = drow["USR_CD"].ToString();
                            UserList.Add(li);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                    UserList = new List<SelectListItem>();
                }
            }
            return UserList;
        }

        #endregion

        #region "Insert, Update and Delete Operation"

        public void InsertUser(Group objGroup)
        {
            DataTable dtbl = new DataTable();
            ComWebUsrGrpInfo obj = new ComWebUsrGrpInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                service.PackageName = "PKG_COM_WEB_SECURITY";
                obj.GrpCd = objGroup.GrpCode;
                obj.UsrCd = objGroup.UserCd.ToUpper();
                obj.EnteredBy = strUserName.Trim();
                obj.EnteredDt = objGroup.EnteredDt;
                obj.LastUpdatedBy = objGroup.LastUpdBy;
                obj.LastUpdatedDt = objGroup.LastUpdDt;
                obj.Ipaddress = CommonVariables.IPAddress;
                try
                {

                    string qrtxt = "SELECT GRP_CD FROM COM_WEB_USR_GRP WHERE USR_CD = '" + objGroup.UserCd.ToUpper() + "'";
                    try
                    {
                        service.Begin();
                        dtbl = service.GetDataTable(qrtxt, null);
                    }
                    catch (Exception ex)
                    {
                        dtbl = null;
                        ExceptionManager.AppendLog(ex);
                    }
                    if (dtbl.Rows.Count > 0)
                    {
                        obj.Mode = "U";
                    }
                    else
                    {
                        obj.Mode = "I";
                    }
                    DataTable dt = new DataTable();
                    string CmdText = "SELECT USR_CD FROM COM_WEB_USR_GRP WHERE USR_CD = '" + objGroup.UserCd.ToUpper() + "' AND GRP_CD =  '" + objGroup.GrpCode + "'";
                    try
                    {
                        dt = service.GetDataTable(CmdText, null);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.AppendLog(ex);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        objGroup.ErrFlg = "Error";
                    }
                    else
                    {
                        try
                        {
                            QueryResult qr = service.SubmitChanges(obj, true);
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
                    }
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
        }

        public void UdateUserGroup(Group objGroup)
        {
            QueryResult qr = new QueryResult();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                ComWebGrpInfo obj = new ComWebGrpInfo();
                service.PackageName = "PKG_COM_WEB_SECURITY";
                obj.GrpCd = objGroup.GrpCode.Trim();
                obj.GrpName = objGroup.GrpName.Trim();
                obj.GrpDesc = objGroup.GrpDesc.Trim();
                obj.Status = Convert.ToString(objGroup.status).Trim();
                obj.EnteredDt = objGroup.EnteredDt;
                obj.EnteredBy = strUserName.Trim();
                obj.LastUpdatedBy = objGroup.LastUpdBy.Trim();
                obj.LastUpdatedDt = objGroup.LastUpdDt;
                obj.Ipaddress = CommonVariables.IPAddress;
                obj.Mode = "U";

                try
                {
                    service.Begin();
                    qr = service.SubmitChanges(obj, true);
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

        public void ChangeStatus(Group objGroup)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                string strUserName = string.Empty;
                strUserName = SessionCheck.getSessionUsername();
                string GrpCd = objGroup.GrpCode;
                char status = objGroup.status;
                string lastupdBy = objGroup.LastUpdBy;
                string CmdText = String.Format("UPDATE COM_WEB_GRP SET STATUS = '" + status + "', ENTERED_BY = '" + strUserName.Trim() + "', LAST_UPDATED_BY = '" + lastupdBy.Trim() + "' WHERE GRP_CD = '" + GrpCd + "' ");
                try
                {
                    service.Begin();
                    service.SubmitChanges(CmdText, null);
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

        public void AddUserGroup(Group objGroup)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                ComWebGrpInfo obj = new ComWebGrpInfo();
                service.PackageName = "PKG_COM_WEB_SECURITY";
                string strUserName = string.Empty;
                strUserName = SessionCheck.getSessionUsername();
                obj.GrpCd = objGroup.GrpCode.Trim();
                obj.GrpName = objGroup.GrpName.Trim();

                if (objGroup.GrpDesc == null)
                {
                    obj.GrpDesc = null;
                }
                else
                {
                    obj.GrpDesc = objGroup.GrpDesc.Trim();
                }
                obj.Status = Convert.ToString(objGroup.status).Trim();
                obj.EnteredDt = objGroup.EnteredDt;
                obj.EnteredBy = strUserName.Trim();
                obj.LastUpdatedBy = objGroup.LastUpdBy.Trim();
                obj.LastUpdatedDt = objGroup.LastUpdDt;
                obj.Ipaddress = CommonVariables.IPAddress;
                obj.Mode = "I";
                try
                {
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(obj, true);
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

        public void DeleteUserGroup(Group objGroup)
        {
            using (ServiceFactory service = new ServiceFactory())
            {
                string strUserName = string.Empty;
                strUserName = SessionCheck.getSessionUsername();
                ComWebGrpInfo obj = new ComWebGrpInfo();
                obj.GrpCd = objGroup.GrpCode;
                obj.GrpName = null;
                obj.GrpDesc = null;
                obj.Status = null;
                obj.LastUpdatedBy = null;
                obj.LastUpdatedDt = DateTime.Today;
                obj.EnteredDt = DateTime.Today;
                obj.EnteredBy = strUserName.Trim();
                obj.Ipaddress = CommonVariables.IPAddress;
                //Moving User from the group

                string cmdtxt = "UPDATE COM_WEB_USR_GRP SET GRP_CD = '" + GeneralUser.GeneralUserID.ToString() + "' WHERE GRP_CD = '" + obj.GrpCd + "'";
                try
                {
                    service.Begin();
                    QueryResult qresult = service.SubmitChanges(cmdtxt);
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
                //Delete the group
                service.PackageName = "PKG_COM_WEB_SECURITY";
                service.Begin();
                obj.Mode = "D";
                try
                {
                    QueryResult qr = service.SubmitChanges(obj, true);
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
                    service.End();
                }
            }
        }

        public void RemoveUserFrmGrp(Group objGroup)
        {
            using (ServiceFactory service = new ServiceFactory())
            {

                string CmdText = "DELETE FROM COM_WEB_USR_GRP WHERE USR_CD = '" + objGroup.UserCd + "' AND GRP_CD = '" + objGroup.GrpCode + "' ";
                try
                {
                    service.Begin();
                    QueryResult qr = service.SubmitChanges(CmdText);
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

        #endregion


        #region
        public List<Group> GetUserGroupbyOrder(string value, string order)
        {
            DataTable dtbl = null;
            List<Group> lstUserGroup = new List<Group>();
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select GRP_CD, GRP_NAME, STATUS FROM COM_WEB_GRP order by " + value + " " + order;
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
            if (dtbl != null)
            {
                foreach (DataRow drow in dtbl.Rows)
                {
                    lstUserGroup.Add(new Group { GrpCode = drow["GRP_CD"].ToString(), GrpName = drow["GRP_NAME"].ToString(), status = Convert.ToChar(drow["STATUS"]) });
                }
            }
            return lstUserGroup;
        }
        #endregion

    }


}

