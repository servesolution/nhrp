using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Security
{
   public  class Menu
    {
        public string GrpCD { get; set; }
        public string mnuCD { get; set; }
        public string perCD { get; set; }
        public string upperMenuCd { get; set; }
       [Required(ErrorMessage = " ")]
        public string label { get; set; }
        [Required(ErrorMessage = " ")]
        public string labelLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public string linkUrl { get; set; }
        public string iconUrl { get; set; }
        public IList<SelectListItem> iconUrlList { get; set; }
        public bool disabled { get; set; }
        public decimal? menuOrder { get; set; }
        public string callBackFunc { get; set; }
        public string menuType { get; set; }
        public string ipAddress { get; set; }
        public string upperMenuLabel { get; set; }
    }
}
