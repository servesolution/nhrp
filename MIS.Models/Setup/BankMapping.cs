using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Setup
{
    public class NHRSBankMapping
    {
        public string MAPPING_ID { get; set; }
        public string DISTRICT_CD { get; set; }
        public string VDC_MUN_CD { get; set; }
        public string PA_NUMBER { get; set; } 
        public  string WARD_NO { get; set; }
        public string BANK_CD { get; set; }
        public string BANK_BRANCH_CD { get; set; }
        public string HOUSE_OWNER_ID { get; set; }
        public string IS_BANK_MAPPED { get; set; }
        public string IS_DONOR_MAPPED { get; set; }
        public string BENEF_NAME { get; set; }
        public string BFROM { get; set; }
        public string BTO { get; set; }
        public string Transaction_Dt_From { get; set; }
        public string Transaction_Dt_To { get; set; }
        public string IS_ACTIVE { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DT { get; set; }
        public string ENTERED_DT_LOC { get; set; }
        public string MODE { get; set; }
        public string Status { get; set; }
        public string BeneficiaryType { get; set; }
        public string Donor_CD { get; set; }
        public string Address { get; set; }
    }
}
