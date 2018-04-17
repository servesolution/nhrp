using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_MOTHER_DTL
    {
        public System.String memberId { get; set; }

        public System.String motherFnameEng { get; set; }

        public System.String motherMnameEng { get; set; }

        public System.String motherLnameEng { get; set; }

        public System.String motherFullNameEng { get; set; }

        public System.String motherFnameLoc { get; set; }

        public System.String motherMnameLoc { get; set; }

        public System.String motherLnameLoc { get; set; }

        public System.String motherFullnameLoc { get; set; }

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
