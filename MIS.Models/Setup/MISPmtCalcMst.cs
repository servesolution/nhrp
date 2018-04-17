using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISPmtCalcMst
    {
        public String pmtCd { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? effectiveYear { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? effectiveMonth { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? effectiveDay { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? effectiveYearLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? effectiveMonthLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? effectiveDayLoc { get; set; }
        public String effectiveDt { get; set; }
        public String effectiveDtLoc { get; set; }
        public Decimal orderNo { get; set; }
        public String remarks { get; set; }
        public String remarksLoc { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String editMode { get; set; }
        public String pmtCodeCheck { get; set; }
        public String defCodeCheck { get; set; }
        public String ipaddress { get; set; }
    }
}
