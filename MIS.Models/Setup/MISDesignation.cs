using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
   public class MISDesignation
    {
       public String designationCd { get; set; }
       public String definedCd { get; set; }
       [Required(ErrorMessage = " ")]
       public String descEng { get; set; }
       [Required(ErrorMessage = " ")]
       public String descLoc { get; set; }
       public String shortName { get; set; }
       public String shortNameLoc { get; set; }
       public String hasSubClass { get; set; }
       public String ignoreUppPosition { get; set; }
       public Decimal retirementAge { get; set; }
       public Decimal? servicePeriod { get; set; }
       public Decimal orderNo { get; set; }
       public String disabled { get; set; }
       public String approved { get; set; }
       public String approvedBy { get; set; }
       public DateTime approvedDt { get; set; }
       public String approvedDtLoc { get; set; }
       public String enteredBy { get; set; }
       public DateTime enteredDt { get; set; }
       public String enteredDtLoc { get; set; }
    }
}
