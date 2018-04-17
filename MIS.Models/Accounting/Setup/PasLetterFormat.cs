using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIS.Models.Accounting.Setup
{
    public class PasLetterFormat
    {
        public String letterNo { get; set; }
        public String descEng { get; set; }
        public String descLoc { get; set; }
        [AllowHtml]
        public String docData { get; set; }
        // [Required(ErrorMessage = " ")]
        //public String letterType { get; set; }
         [Required]
         [Display(Name = "LetterTypeContaints")]
         public string letterType { set; get; }
         public IEnumerable<SelectListItem> LetterTypeContaints { set; get; }

         [Required]
         [Display(Name = "OfficeContaints")]
        public String officeCd { get; set; }
         public IEnumerable<SelectListItem> OfficeContaints { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedByLoc { get; set; }
        public String approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String updatedBy { get; set; }
        public String updatedByLoc { get; set; }
        public String updatedDt { get; set; }
        public String updatedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredByLoc { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String ipaddress { get; set; }
        public String mode { get; set; }


        
    }
}