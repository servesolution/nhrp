using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
  public  class NRATOBANKDownload
    {
        public string MAPPING_ID { get; set; }
        public string ZONE_CD { get; set; }
        public string DISTRICT_CD { get; set; }
        public string VDC_MUN_CD { get; set; }
        public string WARD_NO { get; set; }
        public string BANK_CD { get; set; }
        public string BANK_BRANCH_CD { get; set; }
        public string BFROM { get; set; }
        public string BTO { get; set; }
        public string Transaction_Dt_From { get; set; }
        public string Transaction_Dt_To { get; set; }
        public string IS_ACTIVE { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DT { get; set; }
        public string ENTERED_DT_LOC { get; set; }
        public string FILE_NAME { get; set; }
        public string APPROVED_DT { get; set; }
        public string APPROVED_DT_LOC { get; set; }
        public string APPROVED_BY { get; set; }
        public string TRANCH { get; set; }
        public string EXCEL_DESTINATION { get; set; }
        public string REMARKS { get; set; }
        public string REMARKS_LOC { get; set; }
        public string TRANSACTION_DT { get; set; }
        public string TRANSACTION_DATE_LOC { get; set; }
        public string PAYROLL_INSTALL_CD { get; set; }
        public string DOWNLOADED_OR_NOT { get; set; }
        public string MODE { get; set; }
    }
}
