using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.CaseGrievence
{
   public class CaseType
    {
        public Decimal? IdTypeCd { get; set; }
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
        public String ApprovedDt { get; set; }
        public String ApprovedDtLoc { get; set; }
        public String EnteredBy { get; set; }
        public String EnteredDt { get; set; }
        public String EnteredDtLoc { get; set; }
        public String UpdatedBy { get; set; }
        public String UpdatedDt { get; set; }
        public String UpdatedDtLoc { get; set; }
        public string Remarks { get; set; }
        public String IPAddress { get; set; }
    }
}
