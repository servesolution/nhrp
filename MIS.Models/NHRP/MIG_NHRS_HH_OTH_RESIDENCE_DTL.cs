using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_HH_OTH_RESIDENCE_DTL
    {
        public System.String houseOwnerId { get; set; }

        public System.Decimal otherResidenceId { get; set; }

        public System.Decimal? otherCountryCd { get; set; }

        public System.Decimal? otherRegStCd { get; set; }

        public System.Decimal? otherZoneCd { get; set; }

        public System.Decimal? otherDistrictCd { get; set; }

        public System.Decimal? otherVdcMunCd { get; set; }

        public System.Decimal? otherWardNo { get; set; }

        public System.String otherAreaEng { get; set; }

        public System.String otherAreaLoc { get; set; }

        public System.String houseNo { get; set; }

        public System.Decimal? buildingConditionCd { get; set; }

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
