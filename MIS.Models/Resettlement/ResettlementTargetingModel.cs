using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Resettlement
{
    public class ResettlementTargetingModel
    {
        public string District { get; set; }
        public string VdcMunicipality { get; set; }
        public string Ward { get; set; }
        public string PaNumber { get; set; }
        public string HouseHoldNumber { get; set; }
        public string SlipNumber { get; set; }
        public string HouseOwnerId { get; set; }
        public string RespondentName { get; set; }
        public string BeneficiaryName { get; set; }
        public string Targeted { get; set; }
        public string EligibleSession { get; set; }
        public string Language { get; set; }
        public string MisReview { get; set; }
    }
}
