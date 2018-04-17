using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISToiletType
    {
        public String ToiletTypeCd { get; set; }
         [Required(ErrorMessage = " ")]
        public String DefinedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String DescEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String DescLoc { get; set; }
        public String ShortName { get; set; }
        public String ShortNameLoc { get; set; }
        public Decimal? OrderNo { get; set; }
        public bool Disabled { get; set; }
        public bool Approved { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime ApprovedDt { get; set; }
        public String ApprovedDtLoc { get; set; }
        public String EnteredBy { get; set; }
        public DateTime EnteredDt { get; set; }
        public String EnteredDtLoc { get; set; }
        public String IPAddress { get; set; }
        [Required(ErrorMessage = " ")]
        public String groupFlag { get; set; }
        public String UpperTtypeCd { get; set; }
    }
}
