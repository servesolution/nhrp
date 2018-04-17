using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Security
{
    public class Group
    {
        public string GrpCode { get; set; }
        [Required(ErrorMessage = "Please Enter Group Name !! ")]
        public string GrpName { get; set; }      
        public string GrpDesc { get; set; }
        public char status { get; set; }
        public string EnterBy { get; set; }
        public DateTime EnteredDt { get; set; }
        public DateTime LastUpdDt { get; set; }
        public string LastUpdBy { get; set; }
        public string EmpName { get; set; }
        public string EmpCd { get; set; }
        public string SearchCd { get; set; }
        public string SearchText { get; set; }
        public string UserCd { get; set; }
        public string username { get; set; }
        public IList<SelectListItem> UserList { get; set; }
        public List<SelectList> UsrTormv { get; set; }
        public string ErrFlg { get; set; }

        public string OldNewFlag { get; set; }
    }
}
