using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
    public class PaymentPartnerOrganization
    {
        public int BatchId { get; set; }
        public int Support_CD { get; set; }
        public string PA_NO { get; set; }
        public string Dis_Cd { get; set; }
        public string Vdc_Mun_Cd { get; set; }
        public string Ward_Num { get; set; }
        public string Reciepient_Name { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_cd { get; set; }
        public string Area { get; set; }
        public string House_SN { get; set; }
        public string Nissa_No { get; set; }
        public string Branch { get; set; }
        public string Branch_Std_Cd { get; set; }
        public string Tranche { get; set; }
        public string Status { get; set; }
        public string payroll_install_cd { get; set; }
        public string Donor_CD { get; set; }
        public string APPROVED_BY { get; set; }
        public string APPROVED_DT { get; set; }
        public string APPROVED_DT_LOC { get; set; }
        public string ENTERED_BY { get; set; }
        public string ENTERED_DT { get; set; }
        public string ENTERED_DT_LOC { get; set; }
        public string UPDATED_BY { get; set; }
        public string UPDATED_DT { get; set; }
        public string UPDATED_DT_LOC { get; set; }
        public string MODE { get; set; }
        public string Support_Amount { get; set; }
        public string Remarks { get; set; }
        public string Branch_cd { get; set; }
        public string Beneficiary_Name { get; set; }
    }
}
