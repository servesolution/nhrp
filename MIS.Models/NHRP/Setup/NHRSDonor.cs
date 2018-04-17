﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.Setup
{
    public class NHRSDonor
    {
        public Decimal? DonorCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public string districtcd { get; set; }
        public string vdccd { get; set; }
        public string wardno { get; set; }
        public String shortName { get; set; }
        public String shortNameLoc { get; set; }
        public Decimal? orderNo { get; set; }
        public bool disabled { get; set; }
        public bool approved { get; set; }
        public String approvedBy { get; set; }
        public DateTime? approvedDt { get; set; }
        public String approvedDtLoc { get; set; }
        public String enteredBy { get; set; }
        public DateTime? enteredDt { get; set; }
        public String enteredDtLoc { get; set; }
        public String ipAddress { get; set; }
        public String Mode { get; set; }
    }
}
