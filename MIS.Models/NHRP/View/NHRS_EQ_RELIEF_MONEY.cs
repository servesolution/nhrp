using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIS.Models.NHRP.View
{
    public class NHRS_EQ_RELIEF_MONEY
    {
        public decimal EQ_RELIEF_MONEY_CD { get; set; }

        public string HOUSEHOLD_ID { get; set; }  //1
        public string HOUSE_OWNER_ID { get; set; }
        public string DEFINED_CD { get; set; }
        public string EQ_RELIEF_MONEY_ENG { get; set; }
        public string EQ_RELIEF_MONEY_LOC { get; set; }
        public decimal? ORDER_NO { get; set; }

    }
}
