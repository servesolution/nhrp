﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MIS.Models.Setup
{
    public class MISCountry
    {
        public Decimal? countryCd { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = " ")]
        [Required(ErrorMessage = " ")]
        public String definedCd { get; set; }
        [Required(ErrorMessage = " ")]
        public String descEng { get; set; }
        [Required(ErrorMessage = " ")]
        public String descLoc { get; set; }
        public String shortName { get; set; }
        public String shortNameLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String nationality { get; set; }
        public String nationalityLoc { get; set; }
        [Required(ErrorMessage = " ")]
        public String citizenship { get; set; }
        public String citizenshipLoc { get; set; }
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
        public Decimal? countryCodeCheck { get; set; }
        public String countryNameCheck { get; set; }
        public String countryLocCheck { get; set; }
        public String IPAddress { get; set; }
    }
}