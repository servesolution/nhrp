using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
    public class BankClaim
    {
        public string  Lang { get; set; }
        public string PA_NO { get; set; }
        public decimal? Batch { get; set; }
        public string Beneficiary_Name { get; set; }
        public int Dis_Cd { get; set; }
        public int Vdc_Mun_Cd { get; set; }
        public int Ward_Num { get; set; }
        public string Reciepient_Name { get; set; }
        public int Gender_cd { get; set; }
        public string Bank_Name { get; set; }
        public int Bank_cd { get; set; }
        public string Branch { get; set; }
        public int Branch_Cd { get; set; }
        public string AccountNo { get; set; }
        public string Activation_Date_LOC { get; set; }
        public string Activation_Date { get; set; }
        public string Tranche { get; set; }
        public int Deposite { get; set; }
        public string Deposited_Date_LOC { get; set; }
        public string Deposited_Date { get; set; }
        public string IsCard_Issued{ get; set; }
        public string Card_Iss_Date { get; set; }
        public string Remarks { get; set; }
        public string ApproveStatus { get; set; }
        public string Payroll_install_cd { get; set; }
        public int TRANSACTON_ID { get; set; }
        public string Branch_Std_Cd { get; set; }
        public int Payroll_ID { get; set; }
        public string SecondTrancheApproved { get; set; }
        public string ThirdTrancheApproved { get; set; }
        public int  Bank_Payroll_Id { get; set; }
        public string ChequeNo { get; set; }
        public int DTCO_Payment_Id { get; set; }
        public string Cheque_Tran_EDate { get; set; }
        public string Updated_By { get; set; }
        public DateTime UpdatedDt { get; set; }
        public string Nepali_Cheque_Tran_Date { get; set; }
    }
}
