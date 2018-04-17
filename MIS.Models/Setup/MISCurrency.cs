using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISCurrency
    {
        public String currencyCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String countryCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String remarksEng { get; set; }
        public String remarksLoc { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String currencyCodeCheck { get; set; }
        public String editMode { get; set; }
        public String ipaddress { get; set; }
    }
}
