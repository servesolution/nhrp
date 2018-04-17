using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISEducation
    {
        public Decimal? EducationCd {get;set;}
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
        public String Approved { get; set; }
        public String ApprovedBy { get; set; }
        public DateTime ApprovedDt { get; set; }
        public String ApprovedDtLoc { get; set; }
        public String EnteredBy { get; set; }
        public DateTime EnteredDt { get; set; }
        public String EnteredDtLoc { get; set; }
        public String IPAddress { get; set; }
        public String Mode { get; set; }

        //added new
        public String Active { get; set; }

        public String UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
    }
}
