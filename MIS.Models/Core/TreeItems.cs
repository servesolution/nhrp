using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Core
{
   public class TreeItems
   {
       /// <summary>
       /// Gets or sets the menu code. 
       /// </summary>
       public string Cd { get; set; }
       /// <summary>
       /// Gets or sets the Define Code. 
       /// </summary>
       public string DefineCode { get; set; }
       /// <summary>
       /// Gets or sets the SNo. 
       /// </summary>
       public string Cd1 { get; set; }
       /// <summary>
       /// Gets or sets the upper menu code. 
       /// </summary>
       public string UpperCd { get; set; }
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
       public int Order { get; set; }
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
       /// Gets or sets the value that is to be earched
       /// </summary>
       public string SearchItem { get; set; }
       /// <summary>
       /// Gets or sets the parent of the menu item. 
       /// </summary>
       public TreeItems Parent { get; set; }

       /// <summary>
       /// Gets or sets the List of children of the menu item. 
       /// </summary>
       public List<TreeItems> Children { get; set; }

       /// <summary>
       /// Gets or sets the List of children of the menu item. 
       /// </summary>
       public string ReportLink { get; set; }
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
       public TreeItems()
       {
           Children = new List<TreeItems>();
       }
   }
}
