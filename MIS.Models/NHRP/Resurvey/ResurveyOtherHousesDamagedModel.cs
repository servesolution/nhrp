using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.Resurvey
{
    public class ResurveyOtherHousesDamagedModel
    {
        public string FullNameEng { get; set; }
        public string FullNameLoc { get; set; } 
        public string District { get; set; }
        public string DistrictEng { get; set; }
        public string DistrictLoc { get; set; }
        public string VdcMun { get; set; }
        public string VdcMunEng { get; set; }
        public string VdcMunLoc { get; set; } 
        public string OtherWardNo { get; set; }
        public string BuildingCondition { get; set; }
        public string BuildingConditionEng { get; set; }
        public string BuildingConditionLoc { get; set; }
    }
}
