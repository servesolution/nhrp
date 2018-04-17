using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.Search
{
    public class SATUploadLog
    {


        public string filetxt { get; set; }
        public string fromDtDD { get; set; }
        public string fromDtMM { get; set; }
        public string fromDtYYYY { get; set; }
        public string fromDtDDLoc { get; set; }
        public string fromDtMMLoc { get; set; }
        public string fromDtYYYYLoc { get; set; }
        public string toDtDD { get; set; }
        public string toDtMM { get; set; }
        public string toDtYYYY { get; set; }
        public string toDtDDLoc { get; set; }
        public string toDtMMLoc { get; set; }
        public string toDtYYYYLoc { get; set; }
        public Decimal? DistrictCd { get; set; }
        public string VDCMun { get; set; }
        public Decimal? WardNo { get; set; }
        public string Bank_CD { get; set; }
        public string Transaction { get; set; }
        public string Transaction_Dt_From { get; set; }
        public string Transaction_Dt_To { get; set; }
        public string PER_VDCMUNICIPILITY_CD { get; set; }
        public string PER_DISTRICT_CD { get; set; }
        public string PaymentCycle { get; set; }
        public string fiscalyear { get; set; }
        public string feed_batch_list { get; set; }
        public Decimal? payroll_install_cd { get; set; }

    }
}
