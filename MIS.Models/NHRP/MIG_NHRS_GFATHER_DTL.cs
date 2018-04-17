using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_GFATHER_DTL
    {
        public System.String memberId { get; set; }

        public System.String gfatherFnameEng { get; set; }

        public System.String gfatherMnameEng { get; set; }

        public System.String gfatherLnameEng { get; set; }

        public System.String gfatherFullNameEng { get; set; }

        public System.String gfatherFnameLoc { get; set; }

        public System.String gfatherMnameLoc { get; set; }

        public System.String gfatherLnameLoc { get; set; }

        public System.String gfatherFullnameLoc { get; set; }

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
