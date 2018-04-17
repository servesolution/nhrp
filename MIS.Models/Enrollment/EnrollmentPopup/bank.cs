using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.Enrollment.EnrollmentPopup
{
    public class bank
    {
        public String HOUSE_OWNER_ID { get; set; }
        public Decimal? BANK_CD { get; set; }
        public String BANK_NAME { get; set; }
        public String BANK_ENG { get; set; }
        public String BANK_LOC { get; set; }
        public Decimal? BANK_BRANCH_CD { get; set; }
        public String BANK_BRANCH_ENG { get; set; }
        public String BANK_BRANCH_LOC { get; set; }
        public String BANK_ACC_NO { get; set; }
        public Decimal? BANK_ACC_TYPE_CD { get; set; }
        public String BANK_ACC_TYPE_ENG { get; set; }
        public String BANK_ACC_TYPE_LOC { get; set; }
    }
}
