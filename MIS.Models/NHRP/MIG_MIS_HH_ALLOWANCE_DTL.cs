using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_MIS_HH_ALLOWANCE_DTL
    {
        public System.String houseOwnerId { get; set; }

        public System.String buildingStructureNo { get; set; }

        public System.String householdId { get; set; }

        public System.String memberId { get; set; }

        public System.Decimal sno { get; set; }

        public System.Decimal? allSourceCd { get; set; }

        public System.Decimal? allowanceTypeCd { get; set; }

        public System.DateTime? allowanceStDt { get; set; }

        public System.String allowanceStDtLoc { get; set; }

        public System.Decimal? allowanceYears { get; set; }

        public System.Decimal? allowanceMonth { get; set; }

        public System.Decimal? allowanceDay { get; set; }

        public System.Decimal? allowanceLastYr { get; set; }

        public System.Decimal? allowanceAmt { get; set; }

        public System.Decimal? distanceHr { get; set; }

        public System.Decimal? distanceMin { get; set; }

        public System.Decimal? handiColorCd { get; set; }

        public System.String remarks { get; set; }

        public System.String remarksLoc { get; set; }

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

        public String ipAddress { get; set; }
    }
}
