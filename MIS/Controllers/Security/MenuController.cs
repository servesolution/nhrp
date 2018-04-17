using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Models.Core;
using MIS.Services.Security;
using MIS;
using MIS.Core;
using MIS.Services.Core;
using System.Data;
using MIS.Models.Security;
using System.Collections;
using MIS.Authentication;
using System.IO;
using ExceptionHandler;
using System.Data.OracleClient;

namespace MIS.Controllers.Security
{
     
    public class MenuController : BaseController
    {
        //
        // GET: /Menu/

        [CustomAuthorizeAttribute(PermCd = "2")] 
        public ActionResult MenuList()
        { 
            getMenuListTree();
            ViewBag.mItems = Session["TreeMenu"];
            IList<SelectListItem> MenuUrl = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Text = Utils.GetLabel("None");
            li.Value = "None";
            MenuUrl.Add(li);
            ViewBag.MenuUrl = MenuUrl;
            return View();
        }

        [ChildActionOnly]
        public ActionResult MenuTree()
        {
                 return PartialView("MenuTree");
        }

        /// <summary>
        /// To get menu permission detail
        /// </summary>
        /// <param name="menuCD"></param>
        /// <returns></returns>
        public ActionResult MenuDetail(string menuCD)
        {
            getMenuListTree();
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            Utils.setUrl(this.Url);
            Session["LastMenuCD"] = "1.1.1.2";
            ViewBag.mItems = Session["TreeMenu"];
            if (menuCD != null)
            {
                Session["MenuCD"] = menuCD;
            }
           // menuCD = Session["MenuCD"].ToString();
            MenuServices objMnuSvr = new MenuServices();
            PermissionService objPer = new PermissionService();
            GroupService objGpr = new GroupService();
            DataTable dtComMenuPer = new DataTable();
            DataTable dtGroup = new DataTable();
            DataTable dtPermission = new DataTable();
            dtComMenuPer = objMnuSvr.ListComMenuPermission();
            dtGroup = objGpr.GetUserGrp();
            dtPermission = objPer.Permission_GetAllToTable();

            ViewBag.MenuPermission = dtComMenuPer;
            ViewBag.Permission = dtPermission;
            ViewBag.Group = dtGroup;
            ViewBag.MenuCD = Session["MenuCD"].ToString();           
            if (menuCD != null)
            {
                return PartialView("_MenuDetail");
            }
            else
            {
                return View("_MenuDetailList");
            }
        }

        [CustomAuthorizeAttribute(PermCd = "1")] 
        public void ChangePermission(string menuCD, string GrpCD, string PermCD, string mode)
        {
            MenuSecurity modelMenuSecurity = new MenuSecurity();
            modelMenuSecurity.menuCd = menuCD;
            modelMenuSecurity.permCd = PermCD;
            modelMenuSecurity.grpCd = GrpCD;
            MenuServices objMnuSvr = new MenuServices();
            PermissionService objPer = new PermissionService();
            GroupService objGpr = new GroupService();
            bool res = objMnuSvr.ChangeMenuPermission(modelMenuSecurity,mode);

            //RedirectToAction("MenuDetail", new { menuCD = menuCD });
            DataTable dtComMenuPer = new DataTable();
            DataTable dtGroup = new DataTable();
            DataTable dtPermission = new DataTable();
            dtComMenuPer = objMnuSvr.ListComMenuPermission();
            dtGroup = objGpr.GetUserGrp();
            dtPermission = objPer.Permission_GetAllToTable();

            ViewBag.MenuPermission = dtComMenuPer;
            ViewBag.Permission = dtPermission;
            ViewBag.Group = dtGroup;
            ViewBag.MenuCD = menuCD;
           // return RedirectToAction("MenuDetail");
        }

        /// <summary>
        /// To view the detail of Menu
        /// </summary>
        /// <returns></returns>
        /// 
        [CustomAuthorizeAttribute(PermCd = "2")] 
        public ActionResult MenuDetailList()
        {
            getMenuListTree();
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            Utils.setUrl(this.Url);
            Session["LastMenuCD"] = Session["menuCode"];
            ViewBag.mItems = Session["TreeMenu"]; 
            MenuServices objMenuServices = new MenuServices();
            Menu obj = new Menu();
            DataTable dtbl = objMenuServices.MenuDetailList(Session["MenuCD"].ConvertToString());

            if (Session["MenuCD"].ToString() == "ROOT")
            {
                obj.mnuCD = "ROOT";
                obj.upperMenuCd = "";
                    obj.upperMenuLabel = "";
                    obj.label = "";
                    obj.linkUrl = "";
                    obj.iconUrl = "";
                    obj.disabled = false;
                    obj.menuOrder = 0;
                    obj.callBackFunc = "";
                    obj.labelLoc = "";
                    
            }
            else
            {
                if (dtbl.Rows.Count > 0)
                {
                    obj.mnuCD = dtbl.Rows[0]["MENU_CD"].ToString();
                    obj.upperMenuCd = dtbl.Rows[0]["UPPER_MENU_CD"].ToString();
                    obj.upperMenuLabel = dtbl.Rows[0]["UPPER_LABEL"].ToString();
                    obj.label = dtbl.Rows[0]["LABEL"].ToString();
                    obj.linkUrl = dtbl.Rows[0]["LINK_URL"].ToString();
                    obj.iconUrl = dtbl.Rows[0]["ICON_URL"].ToString();
                    obj.disabled = (dtbl.Rows[0]["DISABLED"].ToString() == "N") ? false : true;
                    obj.menuOrder = Convert.ToDecimal(((dtbl.Rows[0]["MENU_ORDER"] == null) ? "0" : dtbl.Rows[0]["MENU_ORDER"].ToString()) == "" ? "0" : dtbl.Rows[0]["MENU_ORDER"].ToString());
                    obj.callBackFunc = (dtbl.Rows[0]["CALL_BACK_FUNC"] == null) ? "" : dtbl.Rows[0]["CALL_BACK_FUNC"].ToString();
                    obj.labelLoc = (dtbl.Rows[0]["LABEL_LOC"] == null) ? "" : dtbl.Rows[0]["LABEL_LOC"].ToString();
                }
            }
            ViewBag.View = true;
            return View(obj);
        }

        /// <summary>
        /// To add new menu item
        /// </summary>
        /// <returns></returns>
        [CustomAuthorizeAttribute(PermCd = "3")]   
        public ActionResult MenuAdd()
        {
            getMenuListTree();
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            Utils.setUrl(this.Url);
            List<string> menuIcon = getFiles();
            ViewBag.mItems = Session["TreeMenu"];
            IList<SelectListItem> MenuUrl = new List<SelectListItem>();
            foreach (string str in menuIcon)
            {
                SelectListItem li = new SelectListItem();
                li.Text =Utils.GetLabel( str);
                li.Value = str;
                MenuUrl.Add(li);
            }
            SelectListItem lst = new SelectListItem();
            lst.Text = Utils.GetLabel("None");
            lst.Value = "None";
            MenuUrl.Insert(0, lst);
            ViewBag.MenuUrl = MenuUrl;
            MenuServices objMenuServices = new MenuServices();
            Menu obj = new Menu();

            DataTable dtbl = new DataTable();            
                dtbl= objMenuServices.MenuDetailList(Session["MenuCD"].ToString());

                if (Session["MenuCD"].ToString() != "ROOT")
                {
                    if (dtbl.Rows.Count > 0)
                    {
                        obj.upperMenuCd = dtbl.Rows[0]["MENU_CD"].ToString();
                        obj.upperMenuLabel = dtbl.Rows[0]["LABEL"].ToString();
                    }
                }
                else
                {
                     obj.upperMenuCd = "ROOT";
                     obj.upperMenuLabel = "";
                }
            return View("_AddMenu", obj);
        }

        /// <summary>
        /// to edit Menu Item
        /// </summary>
        /// <returns></returns>
         [CustomAuthorizeAttribute(PermCd = "1")] 
        public ActionResult MenuEdit()
        {
            getMenuListTree();
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            Utils.setUrl(this.Url);
            ViewBag.mItems = Session["TreeMenu"];
            
            MenuServices objMenuServices = new MenuServices();
            Menu obj = new Menu();
            DataTable dtbl = objMenuServices.MenuDetailList(Session["MenuCD"].ToString());

            if (dtbl.Rows.Count > 0)
            {
                obj.mnuCD = dtbl.Rows[0]["MENU_CD"].ToString();
                obj.upperMenuCd = dtbl.Rows[0]["UPPER_MENU_CD"].ToString();
                obj.upperMenuLabel = dtbl.Rows[0]["UPPER_LABEL"].ToString();
                obj.label = dtbl.Rows[0]["LABEL"].ToString();
                obj.linkUrl = dtbl.Rows[0]["LINK_URL"].ToString();
                obj.iconUrl = dtbl.Rows[0]["ICON_URL"].ToString();
                obj.disabled = (dtbl.Rows[0]["DISABLED"].ToString() == "N") ? false : true;
                obj.menuOrder = Convert.ToDecimal(((dtbl.Rows[0]["MENU_ORDER"] == null) ? "0" : dtbl.Rows[0]["MENU_ORDER"].ToString()) == "" ? "0" : dtbl.Rows[0]["MENU_ORDER"].ToString());
                obj.callBackFunc = (dtbl.Rows[0]["CALL_BACK_FUNC"] == null) ? "" : dtbl.Rows[0]["CALL_BACK_FUNC"].ToString();
                obj.labelLoc = (dtbl.Rows[0]["LABEL_LOC"] == null) ? "" : dtbl.Rows[0]["LABEL_LOC"].ToString();
            }

            IList<SelectListItem> MenuUrl = new List<SelectListItem>();
            List<string> menuIcon = getFiles();
            foreach (string str in menuIcon)
            {
                SelectListItem li = new SelectListItem();
                li.Text =Utils.GetLabel(str);
                li.Value = str;
                if (str != null)
                {
                    if (str == obj.iconUrl)
                    {
                        li.Selected = true;
                    }
                }
                MenuUrl.Add(li);
            }

            SelectListItem lst = new SelectListItem();
            lst.Text =Utils.GetLabel("None");
            lst.Value = "None";
            MenuUrl.Insert(0, lst);
              ViewBag.MenuUrl = MenuUrl;
            return View("MenuEdit", obj);
        }

        /// <summary>
        /// Arrange Menu Item 
        /// </summary>
        /// <returns></returns>
        [CustomAuthorizeAttribute(PermCd = "1")] 
        public ActionResult ArrangeMenuOrder()
        {
            getMenuListTree();
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            Utils.setUrl(this.Url);
            ViewBag.mItems = Session["TreeMenu"];
            IList<SelectListItem> SubMenuList = new List<SelectListItem>();
            MenuServices objMenuServices = new MenuServices();
            Menu obj = new Menu();
            DataTable dtbl = objMenuServices.ArrangeMenuList(Session["MenuCD"].ToString());
            foreach (DataRow drow in dtbl.Rows)
            {
                SelectListItem li = new SelectListItem();
                li.Text =Utils.GetLabel(drow["LABEL"].ToString());
                li.Value = drow["MENU_CD"].ToString();

                SubMenuList.Add(li);
            }
            List<SelectListItem> SelectedMenuItem = new List<SelectListItem>();
            ViewData["DestinationMenu"] = SelectedMenuItem;
            ViewBag.SubMenuList = SubMenuList;
            return View("ArrangeMenuOrder");
        }

        [CustomAuthorizeAttribute(PermCd = "3")]
        public ActionResult SaveMenu(Menu objMenu, FormCollection fc)
        {
            getMenuListTree();
            ViewBag.mItems = Session["TreeMenu"];
            MenuServices objMenuServices = new MenuServices();
            if (fc["btnCancel"] != null)
            {
                return RedirectToAction("MenuList");
            }
            bool isSuccess = false;
            TryUpdateModel(objMenu);
                   objMenu.iconUrl = fc["MenuUrl"];
            objMenu.ipAddress = null;
            isSuccess = objMenuServices.MenuUID(objMenu, "I");
            if (isSuccess)
            {
                ViewBag.SucessMessage = "Success";
                Session["UserMenu"] = null;
            }
            else
            {
                ViewBag.SucessMessage = "Failed";
            }

            //IList<SelectListItem> MenuUrl = new List<SelectListItem>();
            //List<string> menuIcon = getFiles();
            //foreach (string str in menuIcon)
            //{
            //    SelectListItem li = new SelectListItem();
            //    li.Text = str;
            //    li.Value = str;
            //    MenuUrl.Add(li);
            //}
            //SelectListItem lst = new SelectListItem();
            //lst.Text = "None";
            //lst.Value = "None";
            //MenuUrl.Insert(0, lst);
            //ViewBag.MenuUrl = MenuUrl;
            return RedirectToAction("MenuAdd");
        }

        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult UpdateMenu(Menu objMenu, FormCollection fc)
        {
            getMenuListTree();
            ViewBag.mItems = Session["TreeMenu"];
            MenuServices objMenuServices = new MenuServices();

            if (fc["btnCancel"] != null)
            {
                return RedirectToAction("MenuList");
            }
            bool isSuccess = false;
            TryUpdateModel(objMenu);
            objMenu.iconUrl = fc["MenuUrl"];
            objMenu.ipAddress = null;
            if (ModelState.IsValid)
            {
                isSuccess = objMenuServices.MenuUID(objMenu, "U");
                if (isSuccess)
                {
                    ViewBag.SucessMessage = "Success";
                    Session["UserMenu"] = null;
                }
                else
                {
                    ViewData["Message"] = "Failed";
                }
            }
            return RedirectToAction("MenuEdit");
        }

        /// <summary>
        /// To Arrange Menu
        /// </summary>
        /// <param name="subMenu"></param>
        /// <returns></returns>
        [CustomAuthorizeAttribute(PermCd = "1")]
        public ActionResult ArrangeMenu(string subMenu)
        {
            
            string[] subMenuList;
            subMenuList = subMenu.Split(',');
            MenuServices objMenuServices = new MenuServices();
            Menu objMenu = new Menu();
            bool isSuccess = false;
            TryUpdateModel(objMenu);
            int i = 1;
            foreach (string menu in subMenuList)
            {
                DataTable dtbl = objMenuServices.MenuDetailList(menu);
                if (dtbl.Rows.Count > 0)
                {
                    objMenu.mnuCD = dtbl.Rows[0]["MENU_CD"].ToString();
                    objMenu.upperMenuCd = dtbl.Rows[0]["UPPER_MENU_CD"].ToString();
                    objMenu.upperMenuLabel = dtbl.Rows[0]["UPPER_LABEL"].ToString();
                    objMenu.label = dtbl.Rows[0]["LABEL"].ToString();
                    objMenu.linkUrl = dtbl.Rows[0]["LINK_URL"].ToString();
                    objMenu.iconUrl = dtbl.Rows[0]["ICON_URL"].ToString();
                    objMenu.disabled = (dtbl.Rows[0]["DISABLED"].ToString() == "N") ? false : true;
                    objMenu.callBackFunc = (dtbl.Rows[0]["CALL_BACK_FUNC"] == null) ? "" : dtbl.Rows[0]["CALL_BACK_FUNC"].ToString();
                    objMenu.labelLoc = (dtbl.Rows[0]["LABEL_LOC"] == null) ? "" : dtbl.Rows[0]["LABEL_LOC"].ToString();
                }
                objMenu.menuOrder = i;
                isSuccess = objMenuServices.MenuUID(objMenu, "U");
                i++;
            }
            Session["UserMenu"] = null;
            return RedirectToAction("ArrangeMenuOrder");
        }

        /// <summary>
        /// To change Status of Menu
        /// </summary>
        /// <param name="status"></param>
        [CustomAuthorizeAttribute(PermCd = "1")]
        public void ChangeStatus(string status)
        {
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            MenuServices objMenuServices = new MenuServices();
            Menu objMenu = new Menu();
            DataTable dtbl = objMenuServices.MenuDetailList(Session["MenuCD"].ToString());

            if (dtbl.Rows.Count > 0)
            {
                objMenu.mnuCD = dtbl.Rows[0]["MENU_CD"].ToString();
                objMenu.upperMenuCd = dtbl.Rows[0]["UPPER_MENU_CD"].ToString();
                objMenu.upperMenuLabel = dtbl.Rows[0]["UPPER_LABEL"].ToString();
                objMenu.label = dtbl.Rows[0]["LABEL"].ToString();
                objMenu.linkUrl = dtbl.Rows[0]["LINK_URL"].ToString();
                objMenu.iconUrl = dtbl.Rows[0]["ICON_URL"].ToString();
               objMenu.disabled = (dtbl.Rows[0]["DISABLED"].ToString() == "N") ? false : true;
                objMenu.callBackFunc = (dtbl.Rows[0]["CALL_BACK_FUNC"] == null) ? "" : dtbl.Rows[0]["CALL_BACK_FUNC"].ToString();
                objMenu.labelLoc = (dtbl.Rows[0]["LABEL_LOC"] == null) ? "" : dtbl.Rows[0]["LABEL_LOC"].ToString();
            }
            if (status.ToUpper() == "TRUE")
            {
                objMenu.disabled = false;
            }
            else if (status.ToUpper() == "FALSE")
            {
                objMenu.disabled = true;
            }
            
            bool sucess = objMenuServices.MenuUID(objMenu, "U");
            Session["UserMenu"] = null;
            //return RedirectToAction("MenuDetailList");
        }

        /// <summary>
        /// To delete Menu Item
        /// </summary>
        /// <returns></returns>
        [CustomAuthorizeAttribute(PermCd = "4")]
        public ActionResult MenuDelete()
        {
            getMenuListTree();
            ViewBag.MenuCount = ViewBag.MenuCount - 2;
            ViewBag.mItems = Session["TreeMenu"];
            IList<SelectListItem> MenuUrl = new List<SelectListItem>();
            SelectListItem li = new SelectListItem();
            li.Text = "None";
            li.Value = "None";
            MenuUrl.Add(li);
            MenuServices objMenuServices = new MenuServices();
            Menu obj = new Menu();
            DataTable dtbl = objMenuServices.MenuDetailList(Session["MenuCD"].ToString());

            if (dtbl.Rows.Count > 0)
            {
                obj.mnuCD = dtbl.Rows[0]["MENU_CD"].ToString();
                obj.upperMenuCd = dtbl.Rows[0]["UPPER_MENU_CD"].ToString();
                obj.upperMenuLabel = dtbl.Rows[0]["UPPER_LABEL"].ToString();
                obj.label = dtbl.Rows[0]["LABEL"].ToString();
                obj.linkUrl = dtbl.Rows[0]["LINK_URL"].ToString();
                obj.iconUrl = dtbl.Rows[0]["ICON_URL"].ToString();
                obj.disabled = (dtbl.Rows[0]["DISABLED"].ToString() == "N") ? false : true;
                obj.menuOrder = Convert.ToDecimal(((dtbl.Rows[0]["MENU_ORDER"] == null) ? "0" : dtbl.Rows[0]["MENU_ORDER"].ToString()) == "" ? "0" : dtbl.Rows[0]["MENU_ORDER"].ToString());
                obj.callBackFunc = (dtbl.Rows[0]["CALL_BACK_FUNC"] == null) ? "" : dtbl.Rows[0]["CALL_BACK_FUNC"].ToString();
                obj.labelLoc = (dtbl.Rows[0]["LABEL_LOC"] == null) ? "" : dtbl.Rows[0]["LABEL_LOC"].ToString();
            }
            //MenuItem mnu = new MenuServices().GetMenuTreeMenu();
            ViewBag.MenuUrl = MenuUrl;
            ViewBag.View = true;
            ViewBag.DeletePermission = "Delete";

            return View(obj);
        }

        [CustomAuthorizeAttribute(PermCd = "4")]
        public void MenuItemDelete(Menu objMenu, FormCollection fc)
        {
            MenuServices objMenuServices = new MenuServices();
            objMenu.mnuCD = Session["MenuCD"].ToString();
            bool isSuccess = false;
            TryUpdateModel(objMenu);
            objMenu.ipAddress = null;
            isSuccess = objMenuServices.MenuUID(objMenu, "D");
            if (isSuccess)
            {
                ViewBag.SucessMessage = "Success";
                Session["UserMenu"] = null;
            }
            else
            {
                ViewData["Message"] = "Failed";
            }
        }
        public List<string> getFiles()
        {
            string rootPath = Request.PhysicalApplicationPath;
            string[] filePaths = Directory.GetFiles(Request.PhysicalApplicationPath + "images");
            List<string> fileNames = new List<string>();
            for (int i = 0; i < filePaths.Length; i++)
            {
                FileInfo fInfo = new FileInfo(filePaths[i]);
                string fname = fInfo.Name;
                fileNames.Add(fname);

            }
            return fileNames;

        }
        private void getMenuListTree()
        {
            var menu1 = new MenuBuilder().GetUserWiseMenu("", null);
            foreach (KeyValuePair<Tuple<string,string>, List<MenuItem>> custKeyVal in menu1)
            {
                if (custKeyVal.Key.Item1 == "Level1")
                {
                    List<MenuItem> mItem = new List<MenuItem>(); //custKeyVal.Value;

                    mItem.Add(new MenuItem()
                    {
                        Children = custKeyVal.Value,
                        MenuCd = ("ROOT"),
                        UpperMenuCd = (null),
                        Label = Utils.GetLabel("Tree Hierarchy"),
                        LinkUrl = (""),
                        MenuOrder = (0)

                    });

                    Session["TreeMenu"] = mItem;
                }

            }
        }
    }
}
