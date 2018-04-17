using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_BUILDING_ASS_DTL
    {
        public System.String houseOwnerId { get; set; }

        public System.Decimal? districtCd { get; set; }

        public System.String buildingStructureNo { get; set; }

        public System.Decimal hhDamageDtlId { get; set; }

        public System.Decimal? damageCd { get; set; }

        public System.Decimal? damageLevelCd { get; set; }

        public System.Decimal? damageLevelDtlCd { get; set; }

        public System.String commentEng { get; set; }

        public System.String commentLoc { get; set; }

        public System.String approved { get; set; }

        public System.String approvedBy { get; set; }

        public System.String approvedByLoc { get; set; }

        public System.DateTime? approvedDt { get; set; }

        public System.String approvedDtLoc { get; set; }

        public System.String updatedBy { get; set; }

        public System.String updatedByLoc { get; set; }

        public System.DateTime? updatedDt { get; set; }

        public System.String updatedDtLoc { get; set; }

        public System.String enteredBy { get; set; }

        public System.String enteredByLoc { get; set; }

        public System.DateTime? enteredDt { get; set; }

        public System.String enteredDtLoc { get; set; }

        public System.String ipAddress { get; set; }
    }
}
