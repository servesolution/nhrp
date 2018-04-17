using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
   public class MISDocumentType
    {
        public String DocumentType_cd { get; set; }
        public string Defined_cd { get; set; }
        [Required(ErrorMessage = " ")]
        public string Desc_Eng { get; set; }
        [Required(ErrorMessage = " ")]
        public string Desc_Loc { get; set; }
        public string Short_Name { get; set; }
        public string Short_Name_Loc { get; set; }
        public string Approved_By { get; set; }
        public Boolean Approved { get; set; }
        public DateTime Approved_Dt { get; set; }
        public string Approve_Dt_Loc { get; set; }
        public Boolean Disabled { get; set; }
        public string Order_No { get; set; }
        public string Entered_By { get; set; }
        public DateTime Entered_dt { get; set; }
        public string Entered_Dt_Loc { get; set; }
        public String Mode { get; set; }

    }
}
