using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MisPositionSubClass
    {
        public String positionCd { get; set; }
        [Required]
        public String designationCd { get; set; }
        public String posSubClassCd { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String ipAddress { get; set; }

        public String designationDefinedCd { get; set; }
    }
}
