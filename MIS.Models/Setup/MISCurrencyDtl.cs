using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISCurrencyDtl
    {
        [Required(ErrorMessage = " ")]
        public String currencyCd { get; set; }
        public String effectiveDt { get; set; }
        [Required(ErrorMessage = " ")]
        public String exchangeRate { get; set; }
        [Required(ErrorMessage = " ")]
        public String denominationValue { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public String approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public String enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String editMode { get; set; }
        public String effYear { get; set; }
        public String effMonth { get; set; }
        public String effDay { get; set; }
        public String effYearLoc { get; set; }
        public String effMonthLoc { get; set; }
        public String effDayLoc { get; set; }
        public String currencyCdCheck { get; set; }
        public String effectiveDtCheck { get; set; }
        public String ipaddress { get; set; }
    }
}
