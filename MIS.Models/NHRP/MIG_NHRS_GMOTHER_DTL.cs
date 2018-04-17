using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_GMOTHER_DTL
    {
        public System.String memberId { get; set; }

        public System.String gmotherFnameEng { get; set; }

        public System.String gmotherMnameEng { get; set; }

        public System.String gmotherLnameEng { get; set; }

        public System.String gmotherFullNameEng { get; set; }

        public System.String gmotherFnameLoc { get; set; }

        public System.String gmotherMnameLoc { get; set; }

        public System.String gmotherLnameLoc { get; set; }

        public System.String gmotherFullnameLoc { get; set; }

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
