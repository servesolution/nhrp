using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISDistrict
    {
        public Decimal? DistrictCd { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String DefinedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? ZoneCd { get; set; }
        public String ZoneName { get; set; }
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
        public String editMode { get; set; }
        public Decimal? districtCodeCheck { get; set; }
        public String districtNameCheck { get; set; }
        public Decimal ZoneCode { get; set; }
    }
}
