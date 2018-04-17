using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIS.Models.Setup
{  
    public class MisPosition
    {
        public String positionCd { get; set; }
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String shortName { get; set; }
        public String shortNameLoc { get; set; }
        public String upperPositionCd { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]       
        public String creationDt { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("^(19|20)\\d\\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = " ")]       
        public String creationDtLoc { get; set; }
         public String sourceOfInsertion { get; set; }
        public Decimal orderNo { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        [Required]
        [Display(Name = "PositionGroup")]
        public String groupFlag { set; get; }
        public IEnumerable<SelectListItem> PositionGroup { set; get; }
       // public String groupFlag { get; set; }

        public String upperPositionDefinedCd { get; set; }
    }
}
