using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISRuleCalc
    {
        public String ruleCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String districtCd { get; set; }
        public String districtName { get; set; }
        [Required(ErrorMessage = " ")]
        public String ruleFlag { get; set; }
        public Decimal? orderNo { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public Decimal? quota { get; set; }
        public String effectiveYear { get; set; }
        public String effectiveMonth { get; set; }
        public String effectiveDay { get; set; }
        public String effectiveYearLoc { get; set; }
        public String effectiveMonthLoc { get; set; }
        public String effectiveDayLoc { get; set; }
        public String effectiveDt { get; set; }
        public String effectiveDtLoc { get; set; }
        public String ipAddress { get; set; }
    }
}
