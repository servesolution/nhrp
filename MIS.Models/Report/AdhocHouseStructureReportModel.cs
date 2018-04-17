using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Report
{
    public class AdhocHouseStructureReport
    {
        public string Districtcd { get; set; }

        public string VDCMun { get; set; }

        public string Ward { get; set; }

        public string LeagelOwner { get; set; }

        public string BuildingCondition { get; set; }

        public string FoundationType { get; set; }

        public string RoofMaterial { get; set; }

        public string StoreyBeforEQ { get; set; }

        public string StoreyAfterEQ { get; set; }

        public string FloorMaterial { get; set; }

        public string BuildingMaterial { get; set; }

        public string BuildingPosition { get; set; }

        public string PlanConfig { get; set; }

        public string GeographicRisk { get; set; }

        public string InspectedPart { get; set; }

        public string DamageGrade { get; set; }

        public string TechnicalSolution { get; set; }

        public string ReConstructionStart { get; set; }

        public string OtherUse { get; set; }

        public string Indicator { get; set; }
    }
}