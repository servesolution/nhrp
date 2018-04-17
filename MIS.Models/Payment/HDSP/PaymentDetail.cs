using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
     public  class PaymentDetail
    {
        public String PayrollBatchId { get; set; }
        public String TargetBatchId { get; set; }
        public String targettingId { get; set; }
        public String enrollmentId { get; set; }
        public String mouId { get; set; }
        public String houseOwnerId { get; set; }
        public String beneficiaryid { get; set; }
        public String NraDefCD { get; set; }
        public String benfFullNameEng { get; set; }
        public String benfFullNameLoc { get; set; }
        public String zoneCd { get; set; }
        public String DistrictCd { get; set; }
        public String VdcCd { get; set; }
        public String WardCd { get; set; }
        public String enumerationAreaCd { get; set; }
        public String FiscalYr { get; set; }



        public String PayrolAprovDt { get; set; }
        public String PayrolAprovDtLoc { get; set; }
        public String enterdBy { get; set; }
        public String installationCd { get; set; }
        public String excelDestination { get; set; }

    }
}
