using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MIS.Models.Core;
using System.Web;
using EntityFramework;

namespace MIS.Services.Core
{
    public class BuildTreeStructure
    {
        public BuildTreeStructure()
        {
            CommonVariables.SearchedItem = 0;
        }

        /// <summary>
        /// Create menu for scube userwise 
        /// </summary>
        /// <param name="controller">current controller</param>
        /// <param name="userCd">user code</param>
        /// <returns>MISMenu</returns>
        public TreeItems GetTreeHierarch(DataTable dtTree, string SearchCriteria)
        {

            var treeItem = this.CreateMenu(dtTree, SearchCriteria);
            //var tree = new TreeItems();
            //=========================================            
            // this.Create(tree, treeItem.Children);

            return treeItem;
        }

        /// <summary>
        /// Create sub menu 
        /// </summary>
        /// <param name="menuUrl">Menu URL</param>
        /// <param name="userCd">User code</param>
        /// <returns></returns>
        private TreeItems CreateMenu(DataTable dtTree, string SearchCriteria)
        {


            var treeItems = new List<TreeItems>();
            foreach (DataRow dr in dtTree.Rows)
            {
                string strLabel_eng = (!dr.IsNull("Desc_ENG") ? dr["Desc_ENG"].ToString() : null);
                string strLabel_Loc = (!dr.IsNull("DESC_LOC") ? dr["DESC_LOC"].ToString() : null);
                string strReportLink = "";
                string strCd1 = "";
                if (dr.Table.Columns.Contains("REPORT_LINK"))
                {
                    strReportLink = (!dr.IsNull("REPORT_LINK") ? dr["REPORT_LINK"].ToString() : null);
                }
                if (dr.Table.Columns.Contains("CD1"))
                {
                    strCd1 = (!dr.IsNull("CD1") ? dr["CD1"].ToString() : null);
                }
                strLabel_eng = Utils.ToggleLanguage(strLabel_eng, strLabel_Loc);
                treeItems.Add(new TreeItems()
                {
                    Cd = (!dr.IsNull("CD") ? dr["CD"].ToString() : null),
                    UpperCd = (!dr.IsNull("UpperCd") ? dr["UpperCd"].ToString() : ""),
                    Label = strLabel_eng,
                    Order = (!dr.IsNull("ORDER_NO") ? Convert.ToInt32(dr["ORDER_NO"]) : 0),
                    Label_LOC = (!dr.IsNull("DESC_LOC") ? dr["DESC_LOC"].ToString() : null),
                    ReportLink = strReportLink,
                    Cd1 = strCd1
                    //  DefineCode=
                });

            }

            //========================================
            var tree = new HierarchicalTree();

            var hMenu = tree.CreateHierarchyTree(treeItems, SearchCriteria);

            var curItem = tree.lastCurrentItem;
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
        ///// <param name="items">items</param>
        //private void Create(TreeItems tree, List<TreeItems> treeItems)
        //{
        //    treeItems = (from item in treeItems
        //             orderby item.Order ascending
        //             select item).ToList();

        //    treeItems.ForEach(delegate(TreeItems item)
        //    {
        //        if (!tree.Keys.Contains("Level" + item.Level))
        //        {
        //            tree.Add(("Level" + item.Level), treeItems);
        //        }
        //        if (item.IsCurrent)
        //        {
        //            List<TreeItems> navTreeItems = new List<TreeItems>();
        //            navTreeItems.Add(item);
        //            if (!tree.Keys.Contains("Nav" + item.Level))
        //            {
        //                tree.Add(("Nav" + item.Level), navTreeItems);
        //            }
        //            Create(tree, item.Children);
        //        }
        //    });
        //}
    }

    /// <summary>
    /// Prepared hierarchical menu (there are 5 level of menu)
    /// </summary>
    public class HierarchicalTree
    {
        public TreeItems lastCurrentItem = null;

        /// <summary>
        /// constructor that create Hierarchy menu
        /// </summary>
        /// <param name="menuItems">menu items</param>
        /// <param name="menuUrl">Url</param>
        /// <returns>Menu item</returns>
        public TreeItems CreateHierarchyTree(List<TreeItems> treeItems, string SearchText)
        {

            var parentItems = treeItems.Where(p => p.UpperCd == "").OrderBy(k => k.Order).ToList();

            var root = new TreeItems();
            foreach (var trItem in parentItems)
            {
                trItem.Level = 1;
                trItem.Parent = root;
                string Code = CommonVariables.SelectedCode;
                if (trItem.Cd == Code)
                {
                    trItem.SelectedItem = "jstree-clicked";
                    trItem.ExpandCss = "jstree-open";
                }
                if (SearchText != "" && SearchText != null)
                {

                    if (trItem.Label.ToUpper().IndexOf(SearchText.ToUpper(), StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        trItem.SelectedItem = "jstree-clicked";
                        trItem.ExpandCss = "jstree-open";
                        CommonVariables.SearchedItem = CommonVariables.SearchedItem + 1;
                    }
                }

                //if (trItem.LinkUrl.ToString().ToLower() == menuUrl.ToString().ToLower()) //"security/users/listusers.php")
                //{
                //    HttpContext.Current.Session["PreviousMenuCd"] = trItem.Cd;
                //    trItem.IsCurrent = true;
                //    //que.Enqueue(menuItem);
                //    lastCurrentItem = trItem;
                //}
                BuildTree(trItem, SearchText, p => p.Children = treeItems.Where(child => p.Cd == child.UpperCd).ToList(), 2);
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
        void BuildTree(TreeItems parent, string SearchText, Func<TreeItems, List<TreeItems>> func, int level)
        {
            foreach (var child in func(parent))
            {
                child.Level = level;
                child.Parent = parent;
                string Code = CommonVariables.SelectedCode;
                if (child.Cd == Code)
                {
                    child.SelectedItem = "jstree-clicked";
                    child.ExpandCss = "jstree-open";
                }
                if (SearchText != "" && SearchText != null)
                {
                    if (child.Label.ToUpper().IndexOf(SearchText.ToUpper(), StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        child.SelectedItem = "jstree-clicked";
                        child.ExpandCss = "jstree-open";
                        CommonVariables.SearchedItem = CommonVariables.SearchedItem + 1;
                    }
                }
                //if (child.LinkUrl.ToString().ToLower() == menuUrl.ToString().ToLower()) //"security/users/listusers.php")
                //{
                //    HttpContext.Current.Session["PreviousMenuCd"] = child.Cd;
                //    child.IsCurrent = true;
                //    //que.Enqueue(child);
                //    lastCurrentItem = child;
                //}
                BuildTree(child, SearchText, func, level + 1);
            }
        }
    }
}
