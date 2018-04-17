using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Security
{
   public class MenuSecurity
    {
       public MenuSecurity()
       {

       }

       public String menuCd { get; set; }
       public String grpCd { get; set; }
       public String permCd { get; set; }
       public String enteredBy { get; set; }
       public DateTime enteredDt { get; set; }
       public String lastUpdatedBy { get; set; }
       public DateTime lastUpdatedDt { get; set; }
       public String UsrCd { get; set; }
    }
}
