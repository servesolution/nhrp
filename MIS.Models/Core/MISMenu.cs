using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
    /// <summary>
    /// This class is responsible for the menu. 
    /// It inherits the class System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;.
    /// ======================================
    /// Author:         SSITH Pvt. Ltd.
    /// Created Date:   April 26, 2009
    /// Modified Date: 
    /// ======================================
    /// </summary>
    public class MISMenu : Dictionary<Tuple<string, string>, List<MenuItem>>
    {
        /// <summary>
        /// Creates a new instance of the SCube.Core.SCubeMenu class. 
        /// </summary>
        public MISMenu()
        {

        }
    }

    /// <summary>
    /// This class is responsible for the menu item. 
    /// ======================================
    /// Author:         SSITH Pvt. Ltd.
    /// Created Date:   April 26, 2009
    /// Modified Date: 
    /// ======================================
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Gets or sets the menu code. 
        /// </summary>
        public string MenuCd { get; set; }
        /// <summary>
        /// Gets or sets the upper menu code. 
        /// </summary>
        public string UpperMenuCd { get; set; }
        /// <summary>
        /// Gets or sets the label English. 
        /// </summary>
        public string Label_Eng { get; set; }
        /// <summary>
        /// Gets or sets the label. 
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the label Local. 
        /// </summary>
        public string Label_LOC { get; set; }
        /// <summary>
        /// Gets or sets the Url of the link. 
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// Gets or sets the Url of the icon used for menu item. 
        /// </summary>
        public string IconUrl { get; set; }
        /// <summary>
        /// Gets or sets the order of the menu item. 
        /// </summary>
        public int MenuOrder { get; set; }
        /// <summary>
        /// Gets or sets the level of the menu item. 
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Gets or sets the value that indicates whether the menu item is selected. 
        /// </summary>
        public bool IsCurrent { get; set; }
        /// <summary>
        /// Gets or sets the value that indicates whether the menu item is selected. 
        /// </summary>
        public string SelectedItem { get; set; }
        /// <summary>
        /// Gets or sets the value that indicates whether the menu item is selected. 
        /// </summary>
        public string ExpandCss { get; set; }
        /// <summary>
        /// Gets or sets the parent of the menu item. 
        /// </summary>
        public MenuItem Parent { get; set; }
        /// <summary>
        /// Gets or sets the Menu Type. 
        /// </summary>
        public string MenuType { get; set; }
        /// <summary>
        /// Gets or sets the Menu Type. 
        /// </summary>
        public string MenuStyle { get; set; }
        /// <summary>
        /// Gets or sets the List of children of the menu item. 
        /// </summary>
        public List<MenuItem> Children { get; set; }

        /// <summary>
        /// Gets or sets value indicating whether the menu item has any children. 
        /// </summary>
        public bool HasChildren
        {
            get
            {
                return (this.Children.Count > 0);
            }
        }

        /// <summary>
        /// Creates a new instance of the SCube.Core.MenuItem class. 
        /// </summary>
        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
    }
}
