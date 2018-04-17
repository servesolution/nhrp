using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_HH_HUMAN_DESTROY_DTL
    {
        public System.String houseOwnerId { get; set; }

        public System.String householdId { get; set; }

        public System.String buildingStructureNo { get; set; }

        public System.Decimal? humandestroyTypeCd { get; set; }

        public System.Decimal sno { get; set; }

        public System.String firstNameEng { get; set; }

        public System.String middleNameEng { get; set; }

        public System.String lastNameEng { get; set; }

        public System.String fullNameEng { get; set; }

        public System.String firstNameLoc { get; set; }

        public System.String middleNameLoc { get; set; }

        public System.String lastNameLoc { get; set; }

        public System.String fullNameLoc { get; set; }

        public System.Decimal? genderCd { get; set; }

        public System.Decimal? age { get; set; }

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
