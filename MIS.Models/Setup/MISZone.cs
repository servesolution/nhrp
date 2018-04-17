using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISZone
    {
        public Decimal? zoneCd { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String shortName { get; set; }
        public String shortNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public Decimal? regStCd { get; set; }
        public String regStName { get; set; }
        public Decimal? orderNo { get; set; }
        public bool disabled = false;
        public bool approved = false;
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String editMode { get; set; }
        public Decimal? zoneCodeCheck { get; set; }
        public String IPAddress { get; set; }
    }
}
