using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
   public class MisPositionDesignation
    {
       public String positionCd { get; set; }
       public String designationCd { get; set; }
       public bool approved { get; set; }
       public String approvedBy { get; set; }
       public DateTime approvedDt { get; set; }
       public String approvedDtLoc { get; set; }
       public String enteredBy { get; set; }
       public DateTime enteredDt { get; set; }
       public String enteredDtLoc { get; set; }
    }
}
