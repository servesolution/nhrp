using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP
{
    public class MIG_MIS_HH_DIVORCE_DTL
    {
        public System.String houseOwnerId { get; set; }

        public System.String buildingStructureNo { get; set; }

        public System.String householdId { get; set; }

        public System.String memberId { get; set; }

        public System.Decimal sno { get; set; }

        public System.String divorceCertificate { get; set; }

        public System.String divorceCertificateNo { get; set; }

        public System.Decimal? divorceYear { get; set; }

        public System.Decimal? divorceMonth { get; set; }

        public System.Decimal? divorceDay { get; set; }

        public System.Decimal? divorceYearLoc { get; set; }

        public System.Decimal? divorceMonthLoc { get; set; }

        public System.Decimal? divorceDayLoc { get; set; }

        public System.DateTime? divorceDt { get; set; }

        public System.String divorceDtLoc { get; set; }

        public System.String divorceDoc { get; set; }

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

        public System.String misliteHouseholdId { get; set; }

        public System.String misliteMemberId { get; set; }

        public System.Decimal? misliteUploadCd { get; set; }

        public System.Decimal? misliteExportCd { get; set; }

        public System.String misliteUploadFlag { get; set; }

        public String ipAddress { get; set; }
    }
}
