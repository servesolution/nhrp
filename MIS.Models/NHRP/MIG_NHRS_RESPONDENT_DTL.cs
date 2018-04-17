using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_NHRS_RESPONDENT_DTL
    {
        public System.String houseOwnerId { get; set; }

        public System.Decimal respondentSno { get; set; }

        public System.String respondentFirstName { get; set; }

        public System.String respondentMiddleName { get; set; }

        public System.String respondentLastName { get; set; }

        public System.String respondentFullName { get; set; }

        public System.String respondentFirstNameLoc { get; set; }

        public System.String respondentMiddleNameLoc { get; set; }

        public System.String respondentLastNameLoc { get; set; }

        public System.String respondentFullNameLoc { get; set; }

        public System.String respondentPhoto { get; set; }

        public System.Decimal? respondentGenderCd { get; set; }

        public System.Decimal? hhRelationTypeCd { get; set; }

        public System.String ipAddress { get; set; }
    }
}
