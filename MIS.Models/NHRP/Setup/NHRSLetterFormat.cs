using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.NHRP.Setup
{
    public class NHRSLetterFormat
    {
        public String letterNo { get; set; }
        [Required(ErrorMessage = " ")]
        
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String docData  { get; set; }
        public String letterType  { get; set; }
        public String officeCd  { get; set; }
        public Boolean approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String updatedBy { get; set; }
        public String updatedDt { get; set; }
        public String updatedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String Mode { get; set; }
    }
}
