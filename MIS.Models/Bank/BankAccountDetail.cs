using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Bank
{
    public class BankAccountDetail
    {
        public String EnrollmentID { get; set; }
        public String BeneficiaryName { get; set; }
        public String BeneficiaryID { get; set; }
        public String FatherName { get; set; }
        public String GrandFatherName { get; set; }
        public String AccountNo { get; set; }
        public String BankName { get; set; }
        public String Branch { get; set; }
        public String AccountType { get; set; }

    }
}
