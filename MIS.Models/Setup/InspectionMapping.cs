using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
   public class InspectionMapping
    {
       public Decimal? InspectionMapCd { get; set; }
        public String InspectionMapDefCd { get; set; }
        public Decimal? InspectionMapConsCd { get; set; }
        public Decimal? InspectionMapRoofCd { get; set; }
        public Decimal? InspectionMapHouseCd { get; set; }
        public String InspectionMapEnterBy { get; set; }
        public DateTime? InspectionMapEnterDate { get; set; }
        public String InspectionMapEnterDateLoc { get; set; }
        public String InspectionMapApproved { get; set; }
        public String InspectionMapApprovedBy { get; set; }
        public DateTime? InspectionMapApprovedDate { get; set; }
        public String InspectionMapApprovedDateLoc { get; set; }
        public String InspectionMapUpdatedBy { get; set; }
        public DateTime? InspectionMapUpdatedDate { get; set; }
        public String InspectionMapUpdatedDateLoc { get; set; }
        public String MODE { get; set; }

    }
}
