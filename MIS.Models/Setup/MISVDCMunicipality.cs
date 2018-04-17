using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISVDCMunicipality
    {
        public Decimal? vdcMunCd { get; set; }
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String shortName { get; set; }
        public String shortNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String districtCd { get; set; }
        public string districtName { get; set; }
        [Required(ErrorMessage = " ")]
        public String vdcMunTypeCd { get; set; }
        public string vdcMunTypeName { get; set; }
        public Decimal? orderNo { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String editMode { get; set; }
        public Decimal? vdcMunCodeCheck { get; set; }
        public String vdcMunNameCheck { get; set; }
    }
}
