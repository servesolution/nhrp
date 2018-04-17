using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISWard
    {
        [Required(ErrorMessage = " ")]
        public String vdcMunCd { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? wardNo { get; set; }
        public Decimal? orderNo { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String IPAddress { get; set; }
        public string vdcMunName { get; set; }
    }
}
