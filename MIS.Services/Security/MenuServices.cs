using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EntityFramework;
using MIS.Models.Core;
using MIS.Models.Security;
using System.Web;
using MIS.Services.Core;
using ExceptionHandler;
namespace MIS.Services.Security
{
    public class MenuServices
    {

        /// <summary>
        /// Get Menu Tree Menu
        /// </summary>
        /// <returns>Menu Item</returns>
        public MenuItem GetMenuTreeMenu()
        {
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "select MENU_CD, UPPER_MENU_CD, LABEL,  LINK_URL,  MENU_ORDER from COM_MENU " +
                                    "START with UPPER_MENU_CD is null " +
                                        "CONNECT by prior MENU_CD=UPPER_MENU_CD";
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
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }


            var menuItems = new List<MenuItem>();
            foreach (DataRow dr in dt.Rows)
            {
                menuItems.Add(new MenuItem()
                {
                    MenuCd = (!dr.IsNull(0) ? dr[0].ToString() : null),
                    UpperMenuCd = (!dr.IsNull(1) ? dr[1].ToString() : null),
                    Label = (!dr.IsNull(2) ? dr[2].ToString() : null),
                    LinkUrl = (!dr.IsNull(3) ? dr[3].ToString() : null),
                    MenuOrder = (!dr.IsNull(4) ? Convert.ToInt32(dr[4]) : 0),
                });
            }

            var menu = new HierarchicalMenuTree();
            var hMenu = menu.CreateHierarchy(menuItems);

            var curItem = menu.lastCurrentItem;
            if (curItem != null)
            {
                while (curItem.Level != 0)
                {
                    curItem = curItem.Parent;
                    curItem.IsCurrent = true;
                }
            }
            return hMenu;
        }

        /// <summary>
        /// Get Menu Items
        /// </summary>
        /// <returns>Datatabel</returns>
        public DataTable GetMenuItems()
        {

            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "SELECT" +
                " ROWNUM" +
                " ,cmenu.menu_cd" +
                " ,jcmenuupper.label as upper_label" +
                " ,cmenu.upper_menu_cd" +
                " ,cmenu.label as label" +
                " ,cmenu.label_loc" +
                " ,cmenu.link_url" +
                " ,cmenu.icon_url" +
                " ,cmenu.disabled" +
                " ,cmenu.menu_order" +
                " ,cmenu.call_back_func" +
                " ,NVL(checker.flag,0) flag" +
             " FROM " +
                " com_menu cmenu" +
                " ,com_menu jcmenuupper" +
                " ,(SELECT " +
                     "  COUNT(*) flag," +
                      " a.menu_cd " +
                " FROM " +
                    " com_menu a," +
                     " com_menu b" +
                " WHERE " +
                    "   a.menu_cd=b.upper_menu_cd" +
                " GROUP BY" +
                "	  a.menu_cd) checker";
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(cmdText, null);
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
            }

            return dt;
        }

        /// <summary>
        /// List Permission of menu 
        /// </summary>
        /// <returns>Datatable</returns>
        public DataTable ListComMenuPermission()
        {
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = " SELECT " +
                " menu_cd " +
                ",grp_cd " +
                ",perm_cd " +
                ",entered_by " +
                ",entered_dt " +
                ",last_updated_by " +
                ",last_updated_dt " +
                "FROM  " +
                "com_menu_security";

                try
                {
                    service.Begin();
                    dt = service.GetDataTable(cmdText, null);
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
            }
            return dt;

        }

        /// <summary>
        /// Change Permission of menu
        /// </summary>
        /// <param name="MenuSecurity">Menu Security</param>
        /// <param name="Mode">submission mode</param>
        /// <returns>bool</returns>
        public bool ChangeMenuPermission(MenuSecurity mnuSecurity, string mode)
        {
            QueryResult qResult = null;
            ComMenuSecurityInfo objMenuSecurityInfo = new ComMenuSecurityInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                //if (HttpContext.Current.Session[SessionCheck.sessionName] != null)
                //{
                //    Users usrval = (Users)HttpContext.Current.Session[SessionCheck.sessionName];
                objMenuSecurityInfo.EnteredBy = strUserName.Trim();
                //}
                service.PackageName = "PKG_COM_WEB_SECURITY";
                objMenuSecurityInfo.PermCd = mnuSecurity.permCd;
                objMenuSecurityInfo.GrpCd = mnuSecurity.grpCd;
                objMenuSecurityInfo.MenuCd = mnuSecurity.menuCd;
                // objMenuSecurityInfo.EnteredBy = mnuSecurity.enteredBy;
                // objMenuSecurityInfo.EnteredBy = "SAdmin";
                objMenuSecurityInfo.EnteredDt = mnuSecurity.enteredDt;
                objMenuSecurityInfo.LastUpdatedBy = mnuSecurity.lastUpdatedBy;
                objMenuSecurityInfo.LastUpdatedDt = mnuSecurity.lastUpdatedDt;
                objMenuSecurityInfo.IPAddress = CommonVariables.IPAddress;
                objMenuSecurityInfo.Mode = mode;

                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objMenuSecurityInfo, true);
                }
                catch (Exception ex)
                {
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

        /// <summary>
        /// Menu Updtae/Insert/Delete operations
        /// </summary>
        /// <param name="Menu">objMenu</param>
        /// <param name="string">mode</param>
        /// <returns>bool</returns>
        public bool MenuUID(Menu objMenu, string mode)
        {

            QueryResult qResult = null;
            ComMenuInfo objMenuEntity = new ComMenuInfo();
            string strUserName = string.Empty;
            strUserName = SessionCheck.getSessionUsername();
            using (ServiceFactory service = new ServiceFactory())
            {
                //if (HttpContext.Current.Session[SessionCheck.sessionName] != null)
                //{
                //    Users usrval = (Users)HttpContext.Current.Session[SessionCheck.sessionName];
                objMenuEntity.EnteredBY = strUserName.Trim();
                //}
                service.PackageName = "PKG_COM_WEB_SECURITY";
                objMenuEntity.MenuCd = objMenu.mnuCD;
                objMenuEntity.UpperMenuCd = objMenu.upperMenuCd;
                objMenuEntity.Label = objMenu.label;
                objMenuEntity.LabelLoc = objMenu.labelLoc;
                objMenuEntity.LinkUrl = objMenu.linkUrl;
                objMenuEntity.IconUrl = objMenu.iconUrl;
                objMenuEntity.Disabled = objMenu.disabled;
                objMenuEntity.MenuOrder = objMenu.menuOrder;
                objMenuEntity.CallBackFunc = objMenu.callBackFunc;
                //objMenuEntity.EnteredBY = "SAdmin";
                objMenuEntity.IpAddress = CommonVariables.IPAddress;
                objMenuEntity.Mode = mode;


                try
                {
                    service.Begin();
                    qResult = service.SubmitChanges(objMenuEntity, true);
                }
                catch (Exception ex)
                {
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

        /// <summary>
        /// Get Menu Detail List
        /// </summary>
        /// <param name="String">MenuCd</param> 
        /// <returns>Datatable</returns>
        public DataTable MenuDetailList(string MenuCd)
        {
            DataTable dtbl = null;

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "SELECT " +
                                    " ROWNUM " +
                                    " ,cmenu.menu_cd " +
                                    " ,jcmenuupper.label as upper_label " +
                                    " ,cmenu.upper_menu_cd " +
                                    " ,cmenu.label as label " +
                                    " ,cmenu.label_loc " +
                                    " ,cmenu.link_url " +
                                    " ,cmenu.icon_url " +
                                    " ,cmenu.disabled " +
                                    " ,cmenu.menu_order " +
                                    " ,cmenu.call_back_func " +
                                    " ,NVL(checker.flag,0) flag " +
                                " FROM  " +
                                "	com_menu cmenu " +
                                "	,com_menu jcmenuupper " +
                                 "	,(SELECT " +
                                    "	   COUNT(*) flag," +
                                    "	   a.menu_cd " +
                                "	FROM " +
                                        " com_menu a," +
                                        " com_menu b" +
                                "	WHERE " +
                                         " a.menu_cd=b.upper_menu_cd" +
                                    " GROUP BY" +
                                         " a.menu_cd) checker " +
                                     "  WHERE	cmenu.upper_menu_cd=jcmenuupper.menu_cd(+)   " +
                                  " AND cmenu.menu_cd=checker.menu_cd(+) ";

                if (MenuCd == "ROOT" || MenuCd == null)
                {
                    cmdText = cmdText + " AND cmenu.upper_menu_cd IS NULL";
                }
                else
                {
                    cmdText = cmdText + " AND cmenu.menu_cd='" + MenuCd + "'";
                }
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

        /// <summary>
        /// Arrange Menu List
        /// </summary>
        /// <param name="string">MenuCd</param>
        /// <param name="userCd">user code</param>
        /// <returns>Datatable</returns>
        public DataTable ArrangeMenuList(string MenuCd)
        {
            DataTable dtbl = null;

            using (ServiceFactory service = new ServiceFactory())
            {

                string cmdText = "SELECT " +
                                    " ROWNUM " +
                                    " ,cmenu.menu_cd " +
                                    " ,jcmenuupper.label as upper_label " +
                                    " ,cmenu.upper_menu_cd " +
                                    " ,cmenu.label as label " +
                                    " ,cmenu.label_loc " +
                                    " ,cmenu.link_url " +
                                    " ,cmenu.icon_url " +
                                    " ,cmenu.disabled " +
                                    " ,cmenu.menu_order " +
                                    " ,cmenu.call_back_func " +
                                    " ,NVL(checker.flag,0) flag " +
                                " FROM  " +
                                "	com_menu cmenu " +
                                "	,com_menu jcmenuupper " +
                                 "	,(SELECT " +
                                    "	   COUNT(*) flag," +
                                    "	   a.menu_cd " +
                                "	FROM " +
                                        " com_menu a," +
                                        " com_menu b" +
                                "	WHERE " +
                                         " a.menu_cd=b.upper_menu_cd" +
                                    " GROUP BY" +
                                         " a.menu_cd) checker " +
                                     "  WHERE	cmenu.upper_menu_cd=jcmenuupper.menu_cd(+)   " +
                                  " AND cmenu.menu_cd=checker.menu_cd(+) ";


                if (MenuCd == "ROOT" || MenuCd == null)
                {
                    cmdText = cmdText + " AND (cmenu.upper_menu_cd IS NULL OR cmenu.upper_menu_cd='ROOT')";
                }
                else
                {
                    cmdText = cmdText + " AND cmenu.upper_menu_cd='" + MenuCd + "'";
                }
                cmdText = cmdText + " order by cmenu.menu_order";
                try
                {
                    //String.Format("select * from com_web_permission where PERM_CD={0}", PermissionCode.ToString());
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

        /// <summary>
        /// Create menu for scube userwise 
        /// </summary>
        /// <param name="controller">current controller</param>
        /// <param name="userCd">user code</param>
        /// <returns>SCubeMenu</returns>
        public DataTable MenuPermissionListByGroup(string GroupCD, string menuName)
        {
            DataTable dtbl = null;
            using (ServiceFactory service = new ServiceFactory())
            {
                service.Begin();
                // string cmdText = "SELECT a.*,c.PERM_NAME FROM COM_MENU_SECURITY a,COM_MENU b,COM_WEB_PERMISSION c WHERE a.MENU_CD=b.MENU_CD AND a.PERM_CD=c.PERM_CD AND b.LINK_URL='" + menuName + "' AND a.grp_cd='" + GroupCD + "'";
                string cmdText = "SELECT a.*,c.PERM_NAME FROM COM_MENU_SECURITY a,COM_MENU b,COM_WEB_PERMISSION c WHERE a.MENU_CD=b.MENU_CD AND a.PERM_CD=c.PERM_CD AND a.MENU_CD='" + menuName + "' AND a.grp_cd='" + GroupCD + "'";
                try
                {
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
    }

    /// <summary>
    /// Prepared hierarchical menu (there are 5 level of menu)
    /// </summary>
    public class HierarchicalMenuTree
    {
        public MenuItem lastCurrentItem = null;

        /// <summary>
        /// constructor that create Hierarchy menu
        /// </summary>
        /// <param name="menuItems">menu items</param>
        /// <param name="menuUrl">Url</param>
        /// <returns>Menu item</returns>
        public MenuItem CreateHierarchy(List<MenuItem> menuItems)
        {
            var parentItems = menuItems.Where(p => p.UpperMenuCd == null).OrderBy(k => k.MenuOrder).ToList();

            var root = new MenuItem();
            foreach (var menuItem in parentItems)
            {
                menuItem.Level = 1;
                menuItem.Parent = root;

                BuildTree(menuItem, p => p.Children = menuItems.Where(child => p.MenuCd == child.UpperMenuCd).ToList(), 2);
            }
            root.Children = parentItems;
            return root;
        }

        /// <summary>
        /// It build tree structure menu as per parent
        /// </summary>
        /// <param name="parent">perent menu</param>
        /// <param name="menuUrl">URL</param>
        /// <param name="func">class</param>
        /// <param name="level">menu level</param>
        void BuildTree(MenuItem parent, Func<MenuItem, List<MenuItem>> func, int level)
        {
            foreach (var child in func(parent))
            {
                child.Level = level;
                child.Parent = parent;
                BuildTree(child, func, level + 1);
            }
        }


    }

}



