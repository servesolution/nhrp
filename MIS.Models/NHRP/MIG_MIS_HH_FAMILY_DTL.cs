using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_MIS_HH_FAMILY_DTL
    {
        public System.String householdId { get; set; }

        public System.String memberId { get; set; }

        public System.Decimal sno { get; set; }

        public System.Decimal? relationTypeCd { get; set; }

        public System.String remarks { get; set; }

        public System.String remarksLoc { get; set; }

        public System.String memberActive { get; set; }

        public System.String memberDeath { get; set; }

        public System.String memberMarriage { get; set; }

        public System.String memberSplit { get; set; }

        public System.String transferHhId { get; set; }

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
