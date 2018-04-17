using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Core;
using MIS.Models.Security;
using MIS.Services.Security;
using System.Data;
using System.Web;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Core
{
    public class PermissionParamService
    {
        public PermissionParam GetPermissionValue()
        {
            Group objGroup = new Group();
            GroupService objGroupService = new GroupService();
            Users objUser = new Users();
            DataTable dt = new DataTable();
            MenuServices obJMenuService = new MenuServices();
            string menuid = "";
            PermissionParam objPermissionParam = new PermissionParam();
            objPermissionParam.EnableEdit = "false";
            objPermissionParam.EnableDelete = "false";
            objPermissionParam.EnableAdd = "false";
            objPermissionParam.EnableApprove = "false";
            objPermissionParam.EnablePrint = "false";
            try
            {
                if (HttpContext.Current.Session["menuCode"] != null)
                {
                    menuid = HttpContext.Current.Session["menuCode"].ToString().Trim().Replace("'", "");
                }
                else
                {
                    menuid = HttpContext.Current.Session["PreviousMenuCd"].ToString().Trim().Replace("'", "");
                }
                if (SessionCheck.CheckSession())
                {
                    objUser = (Users)HttpContext.Current.Session[SessionCheck.sessionName];
                    if (objUser.usrName == "SAdmin")
                    {
                        objPermissionParam.EnableEdit = "true";
                        objPermissionParam.EnableDelete = "true";
                        objPermissionParam.EnableAdd = "true";
                        objPermissionParam.EnableApprove = "true";
                        objPermissionParam.EnablePrint = "true";
                    }
                    else
                    {
                        objGroup = objGroupService.GetGroupbyUserCode(objUser.usrCd);
                        if (objGroup != null)
                        {
                            dt = obJMenuService.MenuPermissionListByGroup(objGroup.GrpCode, menuid);
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string permcd = dt.Rows[i]["PERM_CD"].ToString();
                                    if (permcd == PermissionCode.Edit)
                                    {
                                        objPermissionParam.EnableEdit = "true";
                                    }
                                    if (permcd == PermissionCode.Delete)
                                    {
                                        objPermissionParam.EnableDelete = "true";
                                    }
                                    if (permcd == PermissionCode.Add)
                                    {
                                        objPermissionParam.EnableAdd = "true";
                                    }
                                    if (permcd == PermissionCode.Approve)
                                    {
                                        objPermissionParam.EnableApprove = "true";
                                    }
                                    if (permcd == PermissionCode.Print)
                                    {
                                        objPermissionParam.EnablePrint = "true";
                                    }
                                }
                            }
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
            return objPermissionParam;


        }
    }
}
