using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
   public class MisPositionSubClassMst
    {
       public String posSubClassCd { get; set; }
       public String definedCd { get; set; }
       [Required(ErrorMessage = " ")]
       public String descEng { get; set; }
       [Required(ErrorMessage = " ")]
       public String descLoc { get; set; }
       public String shortName { get; set; }
       public String shortNameLoc { get; set; }
       public Decimal orderNo { get; set; }
       public bool disabled { get; set; }
       public bool approved { get; set; }
       public String approvedBy { get; set; }
       public DateTime approvedDt { get; set; }
       public String approvedDtLoc { get; set; }
       public String enteredBy { get; set; }
       public DateTime enteredDt { get; set; }
       public String enteredDtLoc { get; set; }
    }
}
