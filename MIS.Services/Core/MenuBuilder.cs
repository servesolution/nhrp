using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIS.Models.Core;
using MIS.Services.Core;
using System.Data;
using EntityFramework;
using MIS;
using System.Web;
using System.Web.UI;
using System.Data.OracleClient;
using ExceptionHandler;

namespace MIS.Services.Core
{
    /// <summary>
    /// Menu biuler class that generate menu for scube system
    /// /// ======================================
    /// Author:         SSITH Pvt. Ltd.
    /// Created Date:   Dec 03, 2009
    /// Modified Date: 
    /// ======================================
    /// </summary>
    public class MenuBuilder
    {
        public MenuBuilder()
        { }

        /// <summary>
        /// Create menu for scube userwise 
        /// </summary>
        /// <param name="controller">current controller</param>
        /// <param name="userCd">user code</param>
        /// <returns>SCubeMenu</returns>
        public MISMenu GetUserWiseMenu(string menuUrl, string userCd)
        {
            var menItem = this.CreateMenu(menuUrl, userCd);
            var menu = new MISMenu();
            //=========================================            
            this.Create(menu, menItem.Children);

            return menu;
        }

        /// <summary>
        /// Create sub menu 
        /// </summary>
        /// <param name="menuUrl">Menu URL</param>
        /// <param name="userCd">User code</param>
        /// <returns></returns>
        private MenuItem CreateMenu(string menuUrl, string userCd)
        {
            #region Database Data Fetch

            DataTable dt = null;
            if (HttpContext.Current.Session["UserMenu"] != null)
            {
                dt = (DataTable)HttpContext.Current.Session["UserMenu"];
            }
            else
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    string cmdText = "";
                    if (userCd == null || userCd == "admin")
                    {
                        cmdText = "select MENU_CD, UPPER_MENU_CD, LABEL,  LINK_URL,  MENU_ORDER,Icon_url,LABEL_LOC,MENU_TYPE from COM_MENU " +
                                          "START with (UPPER_MENU_CD is null OR UPPER_MENU_CD='ROOT')  " +
                                              "CONNECT by prior MENU_CD=UPPER_MENU_CD order by MENU_CD,MENU_ORDER";


                    }
                    else
                    {
                        cmdText = " select * from (select a.MENU_CD, a.UPPER_MENU_CD, a.LABEL,  a.LINK_URL,  a.MENU_ORDER,a.Icon_url,a.LABEL_LOC,a.MENU_TYPE from COM_MENU   a " +
                        " ,com_menu_security mnusec where a.disabled='N' " +
                        "AND a.menu_cd = mnusec.menu_cd " +
                        "AND mnusec.grp_cd =  (select wug.GRP_CD from COM_WEB_USR_GRP wug where wug.USR_CD='" + userCd + "') " +
                        "  AND mnusec.perm_cd = '2' )  T1 " +
                        " START with (T1.UPPER_MENU_CD is null OR T1.UPPER_MENU_CD='ROOT') " +
                        "CONNECT by prior T1.MENU_CD=T1.UPPER_MENU_CD order by T1.MENU_CD,T1.MENU_ORDER";
                    }
                    try
                    {
                        DataTable dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {
                                // USER_CD = userCd
                            }
                        });
                        dt = dtbl;
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

            var menuItems = new List<MenuItem>();
            foreach (DataRow dr in dt.Rows)
            {
                string strLabel_eng = (!dr.IsNull(2) ? dr[2].ToString() : null);
                string strLabel_Loc = (!dr.IsNull(6) ? dr[6].ToString() : null);
                strLabel_eng = Utils.ToggleLanguage(strLabel_eng, strLabel_Loc);
                string menuType = (!dr.IsNull(7) ? dr[7].ToString() : null);
                string menuStyle = "";
                if (menuType != null)
                    if (menuType.ToLower() == "list")
                        menuStyle = "list-bar";
                    else if (menuType.ToLower() == "view" || menuType.ToLower() == "add" || menuType.ToLower() == "edit" || menuType.ToLower() == "delete")
                        menuStyle = "action-listbar";

                menuItems.Add(new MenuItem()
              {
                  MenuCd = (!dr.IsNull(0) ? dr[0].ToString() : null),
                  UpperMenuCd = (!dr.IsNull(1) ? dr[1].ToString() : "ROOT"),
                  Label = strLabel_eng,
                  LinkUrl = (!dr.IsNull(3) ? dr[3].ToString() : null),
                  MenuOrder = (!dr.IsNull(4) ? Convert.ToInt32(dr[4]) : 0),
                  IconUrl = (!dr.IsNull(5) ? dr[5].ToString() : null),
                  Label_Eng = (!dr.IsNull(2) ? dr[2].ToString() : null),
                  Label_LOC = (!dr.IsNull(6) ? dr[6].ToString() : null),
                  MenuType = menuType,
                  MenuStyle = menuStyle,
              });
            }

            //========================================
            var menu = new HierarchicalMenuTree();

            var hMenu = menu.CreateHierarchy(menuItems, menuUrl);

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
        /// Create child label menu from list of menu
        /// </summary>
        /// <param name="menu">main menu</param>
        /// <param name="items">items</param>
        private void Create(MISMenu menu, List<MenuItem> items)
        {
            items = (from item in items
                     orderby item.MenuOrder ascending
                     select item).ToList();

            items.ForEach(delegate(MenuItem item)
            {
                //Tuple<string, string> tpl = new Tuple<string,string>(menu.Keys);
                string chkCode = "Level" + item.Level;
                string chkCode1 = "Nav" + item.Level;
                if (!menu.Keys.Any(x => x.Item1 == chkCode))
                {
                    menu.Add(new Tuple<string, string>(chkCode, item.MenuStyle), items);
                }
                //if (!menu.Keys[0].Contains("Level" + item.Level))
                //{
                //    menu.Add(("Level" + item.Level), items);
                //}
                if (item.IsCurrent)
                {
                    List<MenuItem> navMenuItems = new List<MenuItem>();
                    navMenuItems.Add(item);
                    if (!menu.Keys.Any(x => x.Item1 == chkCode1))
                    {
                        menu.Add(new Tuple<string, string>(chkCode1, item.MenuStyle), items);
                        //menu.Add(("Nav" + item.Level), navMenuItems);
                    }
                    Create(menu, item.Children);
                }
            });
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
        public MenuItem CreateHierarchy(List<MenuItem> menuItems, string menuUrl)
        {
            var root = new MenuItem();
            try
            {
                var parentItems = menuItems.Where(p => p.UpperMenuCd == "ROOT").OrderBy(k => k.MenuOrder).ToList();

               
                foreach (var menuItem in parentItems)
                {
                    menuItem.Level = 1;
                    menuItem.Parent = root;
                    string mnuCode = (HttpContext.Current.Session["MenuCD"] == null) ? "" : HttpContext.Current.Session["MenuCD"].ToString();
                    if (menuItem.MenuCd == mnuCode)
                    {
                        menuItem.SelectedItem = "jstree-clicked";
                        menuItem.ExpandCss = "jstree-open";
                    }

                    if (menuItem.LinkUrl.ToString().ToLower() == menuUrl.ToString().ToLower()) //"security/users/listusers.php")
                    {
                        HttpContext.Current.Session["PreviousMenuCd"] = menuItem.MenuCd;
                        HttpContext.Current.Session["CurrentAuthorizeMenuCd"] = menuItem.MenuCd;
                        menuItem.IsCurrent = true;
                        //que.Enqueue(menuItem);
                        lastCurrentItem = menuItem;
                    }
                    BuildTree(menuItem, menuUrl, p => p.Children = menuItems.Where(child => p.MenuCd == child.UpperMenuCd).ToList(), 2);
                }
                root.Children = parentItems;
            }
            catch(Exception ex)
            {

            }
           
            return root;
        }

        /// <summary>
        /// It build tree structure menu as per parent
        /// </summary>
        /// <param name="parent">perent menu</param>
        /// <param name="menuUrl">URL</param>
        /// <param name="func">class</param>
        /// <param name="level">menu level</param>
        void BuildTree(MenuItem parent, string menuUrl, Func<MenuItem, List<MenuItem>> func, int level)
        {
            try
            {
                foreach (var child in func(parent))
                {
                    child.Level = level;
                    child.Parent = parent;
                    string mnuCode = (HttpContext.Current.Session["MenuCD"] == null) ? "" : HttpContext.Current.Session["MenuCD"].ToString();
                    if (child.MenuCd == mnuCode)
                    {
                        child.SelectedItem = "jstree-clicked";
                        child.ExpandCss = "jstree-open";
                    }

                    if (child.LinkUrl.ToString().ToLower() == menuUrl.ToString().ToLower()) //"security/users/listusers.php")
                    {
                        HttpContext.Current.Session["PreviousMenuCd"] = child.MenuCd;
                        HttpContext.Current.Session["CurrentAuthorizeMenuCd"] = child.MenuCd;
                        child.IsCurrent = true;
                        //que.Enqueue(child);
                        lastCurrentItem = child;
                    }
                    BuildTree(child, menuUrl, func, level + 1);
                }
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}
