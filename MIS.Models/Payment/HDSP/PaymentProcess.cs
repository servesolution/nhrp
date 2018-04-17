using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Payment.HDSP
{
    public class PaymentProcess
    {

        public String District { get; set; }
        public String DistrictCd { get; set; }

        public String VdcMun { get; set; }
        public String VdcMunCd { get; set; }
        public String Ward { get; set; }
        public String WardCd { get; set; }
        public String FiscalYear { get; set; }
        public String Installation { get; set; }
        public String Bank { get; set; }
        public String BankLoc { get; set; }
        public String Action { get; set; }
        public String InstallationCd { get; set; }
        public String Ins_eng { get; set; }
        public String Ins_loc { get; set; }
        public String SessionId { get; set; }
        public String PayrollGenDt { get; set; }
        public String PayrollGentDtLoc { get; set; }
        public String enterdBy { get; set; }
        public String Sucess { get; set; }
        public String Generate { get; set; }
        public List<PaymentDetail> PaymentDetail { get; set; }
    }
}
